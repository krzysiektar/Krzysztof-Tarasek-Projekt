using Android;
using Android.App;
using Android.Views;
using Android.Widget;
using DataBase.Tables;
using System.Collections.Generic;
namespace DataBase.Model
{
    public class ListViewAdapterCategories : BaseAdapter
    {
        private Activity activity;
        private List<Categories> listCategory;
        private Database db;
        public ListViewAdapterCategories(Activity activity, List<Categories> listCategory, Database db)
        {
            this.activity = activity;
            this.listCategory = listCategory;
            this.db = db;
        }
        public override int Count
        {
            get { return listCategory.Count; }
        }
        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }
        public override long GetItemId(int position)
        {
            return listCategory[position].Id;
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? activity.LayoutInflater.Inflate(Krzysztof_Tarasek_Projekt.Resource.Layout.list_view_Category, parent, false);
            var listviewCategories_name = view.FindViewById<TextView>(Krzysztof_Tarasek_Projekt.Resource.Id.listview_name);
            var listviewCategories_description = view.FindViewById<TextView>(Krzysztof_Tarasek_Projekt.Resource.Id.listview_description);
            var listview_btn = view.FindViewById<Button>(Krzysztof_Tarasek_Projekt.Resource.Id.delete_btn_category);

            listviewCategories_name.Text = listCategory[position].Name;
            listviewCategories_description.Text = listCategory[position].Description;

            listview_btn.Click += delegate
            {
                db.deleteItemCategory(listCategory[position].Id);
            };

            return view;
        }
    }
}