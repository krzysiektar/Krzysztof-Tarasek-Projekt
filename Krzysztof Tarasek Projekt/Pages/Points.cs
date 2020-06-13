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
using DataBase.Model;

namespace Krzysztof_Tarasek_Projekt.Pages
{
    public class Points : Fragment
    {
        public Database db;

        public Points(Database db1) { db = db1; }

        private ListView lstViewData;
        private List<DataBase.Tables.Points> listSource = new List<DataBase.Tables.Points>();


        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var rootView = inflater.Inflate(Resource.Layout.points, container, false);
            var button = rootView.FindViewById<Button>(Resource.Id.btnAddPoint);

            lstViewData = rootView.FindViewById<ListView>(Resource.Id.listView2);
            LoadData();

            lstViewData.ItemClick += (s, e) =>
            {
                LoadData();
            };


            button.Click += delegate
            {               
                var transaction = FragmentManager.BeginTransaction();
                transaction.Replace(Resource.Id.container, new AddPoint(db));
                transaction.Commit();
            };

            return rootView;
        }
        public void LoadData()
        {
            listSource = db.selectTablePoints();
            var adapter = new ListViewAdapterPoints(this.Activity, listSource, db);
            lstViewData.Adapter = adapter;

        }
    };
}
