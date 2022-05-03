using System;
using System.Data.SQLite;

namespace Habit_Tracker
{
    class Program
    {
        static void Main(string[] args)
        {

            string createQuery = @"CREATE TABLE IF NOT EXISTS
                                [MyTable] (
                                [Id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                                [Name] NVARCHAR(2048) NULL,
                                [Gender] NVARCHAR(2048) NULL)";
            //System.Data.SQLite.SQLiteConnection.CreateFile("test1.db3");
            using (System.Data.SQLite.SQLiteConnection conn = new System.Data.SQLite.SQLiteConnection("data source=test1.db3"))
            {
                using (System.Data.SQLite.SQLiteCommand cmd = new System.Data.SQLite.SQLiteCommand(conn))
                {
                    conn.Open();
                    cmd.CommandText = createQuery;
                    cmd.ExecuteNonQuery();
                    

                    //Inputing information
                    Console.WriteLine("what is your name?");
                    string name = Console.ReadLine();
                    Console.WriteLine("What is your gender?");
                    string gender = Console.ReadLine();

                    cmd.CommandText = "INSERT INTO MyTable(Name,Gender) values(@namevalue,@gendervalue)";
                    cmd.Parameters.AddWithValue("@namevalue", name);
                    cmd.Parameters.AddWithValue("@gendervalue", gender);
                    cmd.ExecuteNonQuery();


                    //Deleting the latest entry
                     cmd.CommandText = "DELETE FROM MyTable WHERE ID=(SELECT MAX(id) FROM MyTable)";
                     cmd.ExecuteNonQuery();

                    //Deleting a specific entry
                    Console.WriteLine("What entry do you want to delete?");
                    int id = Convert.ToInt32(Console.ReadLine());
                    cmd.CommandText = "DELETE FROM MyTable WHERE Id=@idvalue.ToString()";
                    cmd.Parameters.AddWithValue("@idvalue", id);
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "SELECT * from MyTable";
                    using (System.Data.SQLite.SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine( reader["Name"] + ":" + reader["Gender"]);
                        }
                        conn.Close();
                    }

                }




            }
        }
    }
}
