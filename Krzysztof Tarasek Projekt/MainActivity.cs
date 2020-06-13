using System;
using Android;
using Android.Accounts;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;
using DataBase.Model;
//using DataBase.Tables;
using Krzysztof_Tarasek_Projekt.Pages;

namespace Krzysztof_Tarasek_Projekt
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener
    {
        public Database db;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            db = new Database();
            db.createTableCategories();
            db.createTablePoints();

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawer, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            drawer.AddDrawerListener(toggle);
            toggle.SyncState();

            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.SetNavigationItemSelectedListener(this);
            
            var transaction = FragmentManager.BeginTransaction();
            transaction.Replace(Resource.Id.container, new MainPage(db));
            transaction.Commit();            
        }

        public override void OnBackPressed()
        {
            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            if(drawer.IsDrawerOpen(GravityCompat.Start))
            {
                drawer.CloseDrawer(GravityCompat.Start);
            }
            else
            {
                base.OnBackPressed();
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            View view = (View) sender;
            Snackbar.Make(view, "Tu wyszukasz wszystkie interesujące miejsca :)", Snackbar.LengthLong)
                .SetAction("Action", (View.IOnClickListener)null).Show();
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            int id = item.ItemId;

            if (id == Resource.Id.nav_main)
            {
                var transaction = FragmentManager.BeginTransaction();
                transaction.Replace(Resource.Id.container, new MainPage(db));
                transaction.Commit();
            }

            else if (id == Resource.Id.nav_places)
            {
                var transaction = FragmentManager.BeginTransaction();
                transaction.Replace(Resource.Id.container, new Points(db));
                transaction.Commit();
            }
            else if (id == Resource.Id.nav_categories)
            {
                var transaction = FragmentManager.BeginTransaction();
                transaction.Replace(Resource.Id.container, new Categories(db));
                transaction.Commit();
            }
            else if (id == Resource.Id.nav_comments)
            {
                var transaction = FragmentManager.BeginTransaction();
                transaction.Replace(Resource.Id.container, new Comments());
                transaction.Commit();
            }
            else if (id == Resource.Id.nav_search)
            {
                var transaction = FragmentManager.BeginTransaction();
                transaction.Replace(Resource.Id.container, new Search());
                transaction.Commit();
            }
            else if (id == Resource.Id.nav_settings)
            {
                var transaction = FragmentManager.BeginTransaction();
                transaction.Replace(Resource.Id.container, new UserAccount());
                transaction.Commit();
            }
            else if (id == Resource.Id.nav_login)
            {
                var activity = (Activity)this;
                activity.FinishAffinity();
            }

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            drawer.CloseDrawer(GravityCompat.Start);
            return true;
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

