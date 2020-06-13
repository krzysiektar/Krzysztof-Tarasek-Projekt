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
using DataBase.Tables;

namespace Krzysztof_Tarasek_Projekt.Pages
{
    public class Categories : Fragment
    {
        public Database db;

        public Categories(Database db1) { db = db1; }

        private ListView lstViewData;
        private List<DataBase.Tables.Categories> listSource = new List<DataBase.Tables.Categories>();

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var rootView = inflater.Inflate(Resource.Layout.categories, container, false);
            var button = rootView.FindViewById<Button>(Resource.Id.btnAddCategory);
            lstViewData = rootView.FindViewById<ListView>(Resource.Id.listView);
            
            LoadData();

            lstViewData.ItemClick += (s, e) =>
            {
                LoadData();
            };

            button.Click += delegate
            {
                var transaction = FragmentManager.BeginTransaction();
                transaction.Replace(Resource.Id.container, new AddCategory(db));
                transaction.Commit();
            };

            return rootView;
        }
        private void LoadData()
        {
            listSource = db.selectTableCategories();
            var adapter = new ListViewAdapterCategories(this.Activity, listSource, db);
            lstViewData.Adapter = adapter;
        }
    }
}