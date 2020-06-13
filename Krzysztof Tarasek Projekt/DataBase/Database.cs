using Android.Util;
using DataBase.Tables;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataBase.Model
{
    public class Database
    {
        string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        public bool createTableCategories()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Prawo_Jazdy.db")))
                {
                    connection.CreateTable<Categories>();
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }

        public bool createTablePoints()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Prawo_Jazdy.db")))
                {
                    connection.CreateTable<Points>();
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }
        //Add or Insert Operation  

        public bool insertIntoTable(Categories categories)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Prawo_Jazdy.db")))
                {
                    connection.Insert(categories);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }

        public bool insertIntoTablePoints(Points points)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Prawo_Jazdy.db")))
                {
                    connection.Insert(points);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }

        public void selectItem()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Prawo_Jazdy.db")))
                {
                    //return connection.Table<Categories>().First().ToString();
                    Console.WriteLine("Reading data");
                    var table = connection.Table<Categories>();
                    foreach (var s in table)
                    {
                        Console.WriteLine(s.Id + " " + s.Name);
                    }

                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                //return null;
            }
        }


        public List<Categories> selectTableCategories()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Prawo_Jazdy.db")))
                {
                    return connection.Table<Categories>().ToList();
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return null;
            }
        }

        public List<Points> selectTablePoints()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Prawo_Jazdy.db")))
                {
                    return connection.Table<Points>().ToList();
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return null;
            }
        }
        //Edit Operation  

        public bool updateTable(Categories categories)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Prawo_Jazdy.db")))
                {
                    connection.Query<Categories>("UPDATE Categories set Name=?, Description=? Where Id=?", categories.Name, categories.Description, categories.Id);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }
        //Delete Data Operation  

        public bool removeTable(Categories categories)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Prawo_Jazdy.db")))
                {
                    connection.Delete(categories);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }
        //Select Operation  

        public bool selectTable(int Id)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Prawo_Jazdy.db")))
                {
                    connection.Query<Categories>("SELECT * FROM Categories Where Id=?", Id);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }

        public bool deleteItemPoint(int Id)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Prawo_Jazdy.db")))
                {
                    connection.Query<Points>("DELETE FROM Points Where Id=?", Id);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }

        public bool deleteItemCategory(int Id)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Prawo_Jazdy.db")))
                {
                    connection.Query<Points>("DELETE FROM Categories Where Id=?", Id);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }
    }
}