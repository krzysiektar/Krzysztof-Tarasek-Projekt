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
    public class AddCategory : Fragment
    {
        public Database db;

        public AddCategory(Database db1) { db = db1; }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var rootView = inflater.Inflate(Resource.Layout.add_category, container, false);
            

            var button = rootView.FindViewById<Button>(Resource.Id.btnAddCat);

            var edtName = rootView.FindViewById<EditText>(Resource.Id.catName);
            var edtDescription = rootView.FindViewById<EditText>(Resource.Id.catDescription);

            button.Click += delegate
            {
                if (edtName.Text != "")
                {
                    DataBase.Tables.Categories categories = new DataBase.Tables.Categories()
                    {
                        Name = edtName.Text,
                        Description = edtDescription.Text
                    };
                    db.insertIntoTable(categories);

                    var transaction = FragmentManager.BeginTransaction();
                    transaction.Replace(Resource.Id.container, new Categories(db));
                    transaction.Commit();
                }
                else
                {
                    Toast.MakeText(this.Activity, "Wypełnij wszystkie wymagane pola!", ToastLength.Short).Show();
                }
            };

            return rootView;
        }
    }
}