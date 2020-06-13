using SQLite;
namespace DataBase.Tables
{
    public class Categories
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class Points
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Color { get; set; }
    }
}