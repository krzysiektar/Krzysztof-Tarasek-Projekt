using Android;
using Android.App;
using Android.Views;
using Android.Widget;
using DataBase.Tables;
using System.Collections.Generic;
namespace DataBase.Model
{
    public class ListViewAdapterPoints : BaseAdapter
    {
        private Activity activity;
        private List<Points> listPoints;
        private Database db;
        public ListViewAdapterPoints(Activity activity, List<Points> listPoints, Database db)
        {
            this.activity = activity;
            this.listPoints = listPoints;
            this.db = db;
        }
        public override int Count
        {
            get { return listPoints.Count; }
        }
        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }
        public override long GetItemId(int position)
        {
            return listPoints[position].Id;
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? activity.LayoutInflater.Inflate(Krzysztof_Tarasek_Projekt.Resource.Layout.list_view_Point, parent, false);
            var listview_name = view.FindViewById<TextView>(Krzysztof_Tarasek_Projekt.Resource.Id.listviewPoint_name);
            var listview_description = view.FindViewById<TextView>(Krzysztof_Tarasek_Projekt.Resource.Id.listviewPoint_Description);
            var listview_pos = view.FindViewById<TextView>(Krzysztof_Tarasek_Projekt.Resource.Id.listviewPoint_pos);
            var listview_color = view.FindViewById<TextView>(Krzysztof_Tarasek_Projekt.Resource.Id.listviewPoint_color);
            var listview_category = view.FindViewById<TextView>(Krzysztof_Tarasek_Projekt.Resource.Id.listviewPoint_category);

            var listview_btn = view.FindViewById<ImageView>(Krzysztof_Tarasek_Projekt.Resource.Id.delete_img_point);

            listview_name.Text = listPoints[position].Name;
            listview_description.Text = listPoints[position].Description;
            listview_pos.Text = listPoints[position].Lat+", "+ listPoints[position].Long;
            listview_color.Text = listPoints[position].Color;
            listview_category.Text = listPoints[position].Category;
            
            listview_btn.Click += delegate
            {
                db.deleteItemPoint(listPoints[position].Id);
            };
            
            return view;
        }
    }
}