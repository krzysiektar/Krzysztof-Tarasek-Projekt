using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.Gms.Common;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DataBase.Model;

namespace Krzysztof_Tarasek_Projekt.Pages
{
    public class MainPage : Fragment, IOnMapReadyCallback
    {
        public Database db;
        public MainPage(Database db1) { db = db1; }

        private GoogleMap googleMap;
        private MapView mapView;
        private bool mapsSupported = true;

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

            mapView.GetMapAsync(this);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.MapPage, container, false);
            mapView = view.FindViewById<MapView>(Resource.Id.googlemap);

            return view;
        }

        public void OnMapReady(GoogleMap map)
        {
            googleMap = map;

            googleMap.MapType = GoogleMap.MapTypeNormal;
            googleMap.MyLocationEnabled = false;
            googleMap.UiSettings.CompassEnabled = true;
            googleMap.UiSettings.MyLocationButtonEnabled = true;
            googleMap.UiSettings.ZoomControlsEnabled = true;
            googleMap.UiSettings.SetAllGesturesEnabled(true);

            googleMap.Clear();
            var tab = db.selectTablePoints();
            if (tab.Count() > 0)
            {
                foreach (var s in tab)
                {
                    LatLng latlng = new LatLng(s.Lat, s.Long);
                    BitmapDescriptor color;

                    switch (s.Color)
                    {
                        case "Czerwony":
                            color = BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueRed);
                            break;
                        case "Niebieski":
                            color = BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueBlue);
                            break;
                        case "Zółty":
                            color = BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueYellow);
                            break;
                        case "Zielony":
                            color = BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueGreen);
                            break;
                        case "Pomarańczowy":
                            color = BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueOrange);
                            break;
                        case "Fioletowy":
                            color = BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueViolet);
                            break;
                        default:
                            color = BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueYellow);
                            break;
                    }

                    googleMap.AddMarker(new MarkerOptions()
                     .SetPosition(latlng)
                     .SetTitle(s.Name)
                     .SetSnippet(s.Description)
                     .SetIcon(color));
                }
            }

            Marker rynek = googleMap.AddMarker(new MarkerOptions()
                .SetPosition(Location_Start)
                .SetTitle("Nowy Sącz")
                .Draggable(true)
                .SetSnippet("Rynek")
                .SetIcon(BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueYellow)));
            
            googleMap.MapLongClick += (sender, e) =>
                googleMap.AnimateCamera(CameraUpdateFactory.ZoomOut(), 1000, null);

            CameraUpdate update = CameraUpdateFactory.NewLatLngZoom(Location_Start, 13);
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