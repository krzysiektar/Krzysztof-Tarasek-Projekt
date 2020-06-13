using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;
using Android.Gms.Common;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using DataBase.Model;

namespace Krzysztof_Tarasek_Projekt.Pages
{
    public class AddPoint : Fragment, IOnMapReadyCallback
    {
        public Database db;

        public AddPoint(Database db1) { db = db1; }

        private List<KeyValuePair<string, string>> categories;
        private List<KeyValuePair<string, string>> colors;

        private GoogleMap googleMap;
        private MapView mapView;
        private bool mapsSupported = true;
        private LatLng pos;
        private string category;
        private string color;

        static readonly LatLng Location_Start = new LatLng(49.624920, 20.691170);

        public override void OnActivityCreated(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            try
            {
                MapsInitializer.Initialize(Activity);
            }
            catch (GooglePlayServicesNotAvailableException e)
            {
                mapsSupported = false;
            }

            if (mapView != null)
            {
                mapView.OnCreate(savedInstanceState);
            }

            //initialize Map
            mapView.GetMapAsync(this);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var rootView = inflater.Inflate(Resource.Layout.add_point, container, false);
            mapView = rootView.FindViewById<MapView>(Resource.Id.googlemap2);

            categories = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Rondo", ""),
                new KeyValuePair<string, string>("Skrzyżowanie", ""),
                new KeyValuePair<string, string>("Światła", ""),
                new KeyValuePair<string, string>("Parking", ""),
                new KeyValuePair<string, string>("Plac" , ""),
                new KeyValuePair<string, string>("Wzniesienie", ""),
                new KeyValuePair<string, string>("Niebezpieczeństwo", ""),
            };
            colors = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Czerwony", ""),
                new KeyValuePair<string, string>("Niebieski", ""),
                new KeyValuePair<string, string>("Zółty", ""),
                new KeyValuePair<string, string>("Zielony", ""),
                new KeyValuePair<string, string>("Pomarańczowy" , ""),
                new KeyValuePair<string, string>("Fioletowy", ""),
            };

            List<string> categoriesNames = new List<string>();
            /*foreach (var item in categories)
                categoriesNames.Add(item.Key);
            */
            var tab = db.selectTableCategories();
            foreach (var s in tab)
            {
                categoriesNames.Add(s.Name);
            }

            List<string> colorsNames = new List<string>();
            foreach (var item in colors)
                colorsNames.Add(item.Key);

            Spinner spinner = rootView.FindViewById<Spinner>(Resource.Id.spinner1);
            spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);

            Spinner spinner2 = rootView.FindViewById<Spinner>(Resource.Id.spinner2);
            spinner2.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner2_ItemSelected);

            var adapter = new ArrayAdapter<string>(this.Activity, Android.Resource.Layout.SimpleSpinnerItem, categoriesNames);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;

            var adapter2 = new ArrayAdapter<string>(this.Activity, Android.Resource.Layout.SimpleSpinnerItem, colorsNames);
            adapter2.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner2.Adapter = adapter2;

            var button = rootView.FindViewById<Button>(Resource.Id.btnAdd_Point);

            var edtName = rootView.FindViewById<EditText>(Resource.Id.point_name);
            var edtDescription = rootView.FindViewById<EditText>(Resource.Id.point_description);
            var edtPos = rootView.FindViewById<EditText>(Resource.Id.marker_position);

            button.Click += delegate
            {
                if (pos != null && edtName.Text != "")
                {
                    DataBase.Tables.Points points = new DataBase.Tables.Points()
                    {
                        Name = edtName.Text,
                        Description = edtDescription.Text,
                        Lat = pos.Latitude,
                        Long = pos.Longitude,
                        Category = category,
                        Color = color
                    };
                    db.insertIntoTablePoints(points);

                    var transaction = FragmentManager.BeginTransaction();
                    transaction.Replace(Resource.Id.container, new Points(db));
                    transaction.Commit();
                }
                else 
                {
                    Toast.MakeText(this.Activity, "Wypełnij wszystkie wymagane pola!", ToastLength.Short).Show();
                }
            };

            return rootView;
        }

        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            string toast = string.Format("Wybrana kategoria: {0}", spinner.GetItemAtPosition(e.Position));
            Toast.MakeText(this.Activity, toast, ToastLength.Short).Show();
            category = spinner.GetItemAtPosition(e.Position).ToString();
        }

        private void spinner2_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            string toast = string.Format("Wybrany kolor: {0}", spinner.GetItemAtPosition(e.Position));
            Toast.MakeText(this.Activity, toast, ToastLength.Short).Show();
            color = spinner.GetItemAtPosition(e.Position).ToString();
        }

        public void OnMapReady(GoogleMap map)
        {
            var editText = this.Activity.FindViewById<EditText>(Resource.Id.marker_position);
           
            googleMap = map;

            googleMap.MapType = GoogleMap.MapTypeNormal;
            googleMap.MyLocationEnabled = false;
            googleMap.UiSettings.CompassEnabled = true;
            googleMap.UiSettings.MyLocationButtonEnabled = true;
            googleMap.UiSettings.ZoomControlsEnabled = false;
            googleMap.UiSettings.SetAllGesturesEnabled(true);

            googleMap.MapLongClick += (object sender, GoogleMap.MapLongClickEventArgs e) =>
            {
                googleMap.Clear();
                pos = e.Point;
                using (var markerOption = new MarkerOptions())
                {
                    markerOption.SetPosition(e.Point);
                    var marker = googleMap.AddMarker(markerOption);
                    editText.Text = "Lat: "+e.Point.Latitude.ToString()+"; Long: "+ e.Point.Longitude.ToString();
                }
            };


            CameraUpdate update = CameraUpdateFactory.NewLatLngZoom(Location_Start, 14);
            googleMap.MoveCamera(update);
        }


        public override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            mapView.OnSaveInstanceState(outState);
        }

        public override void OnResume()
        {
            base.OnResume();
            mapView.OnResume();
        }

        public override void OnPause()
        {
            base.OnPause();
            mapView.OnPause();
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            mapView.OnDestroy();
        }

        public override void OnLowMemory()
        {
            base.OnLowMemory();
            mapView.OnLowMemory();
        }
    }
}