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


                    //Menu for users
                    Console.WriteLine(" Please select 1 to add an entry. \n Please select 2 to update an entry \n Please select 3 to delete an entry \n Please select 4 to show all entries \n Please select 0 to exit");
                    int optionChosen = Convert.ToInt32(Console.ReadLine());
                    switch (optionChosen)
                    {
                        case 1: InputInfo(); break;
                        case 2: Update();  break;
                        case 3: Delete();  break;
                        case 4: DisplayAll(); break;
                        default:
                            conn.Close();
                            break;
                    }
                    
                    
                    
                    


                    //Deleting the latest entry
                    //cmd.CommandText = "DELETE FROM MyTable WHERE ID=(SELECT MAX(id) FROM MyTable)";
                    //cmd.ExecuteNonQuery();






                    conn.Close();

                }




            }
        }
        static void InputInfo()
        {
            using (System.Data.SQLite.SQLiteConnection conn = new System.Data.SQLite.SQLiteConnection("data source=test1.db3"))
            {
                using (System.Data.SQLite.SQLiteCommand cmd = new System.Data.SQLite.SQLiteCommand(conn))
                {
                    conn.Open();
                    //Inputing information
                    Console.WriteLine("what is your name?");
                    string name = Console.ReadLine();
                    Console.WriteLine("What is your gender?");
                    string gender = Console.ReadLine();

                    cmd.CommandText = "INSERT INTO MyTable(Name,Gender) values(@namevalue,@gendervalue)";
                    cmd.Parameters.AddWithValue("@namevalue", name);
                    cmd.Parameters.AddWithValue("@gendervalue", gender);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
        static void Delete()
        {
            using (System.Data.SQLite.SQLiteConnection conn = new System.Data.SQLite.SQLiteConnection("data source=test1.db3"))
            {
                using (System.Data.SQLite.SQLiteCommand cmd = new System.Data.SQLite.SQLiteCommand(conn))
                {
                    conn.Open();
                    //Deleting a specific entry
                    Console.WriteLine("What entry do you want to delete?");
                    int id = Convert.ToInt32(Console.ReadLine());
                    cmd.CommandText = "DELETE FROM MyTable WHERE Id=@idvalue";
                    cmd.Parameters.AddWithValue("@idvalue", id);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
        static void Update()
        {
            using (System.Data.SQLite.SQLiteConnection conn = new System.Data.SQLite.SQLiteConnection("data source=test1.db3"))
            {
                using (System.Data.SQLite.SQLiteCommand cmd = new System.Data.SQLite.SQLiteCommand(conn))
                {
                    conn.Open();
                    //Updating an entry
                    Console.WriteLine("What entry do you want to update?");
                    int idUpdate = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("What is the new Gender?");
                    string genderUpdate = Console.ReadLine();
                    cmd.CommandText = "UPDATE MyTable set Gender = @newGender WHERE Id = @idvalue";
                    cmd.Parameters.AddWithValue("@idvalue", idUpdate);
                    cmd.Parameters.AddWithValue("@newGender", genderUpdate);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
        static void DisplayAll()
        {
            using (System.Data.SQLite.SQLiteConnection conn = new System.Data.SQLite.SQLiteConnection("data source=test1.db3"))
            {
                using (System.Data.SQLite.SQLiteCommand cmd = new System.Data.SQLite.SQLiteCommand(conn))
                {
                    conn.Open();
                    //Displays all records
                    cmd.CommandText = "SELECT * from MyTable";
                    using (System.Data.SQLite.SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine(reader["Id"] + ":" + reader["Name"] + ":" + reader["Gender"]);
                        }
                        
                    }
                    conn.Close();
                }
            }
        }
    }
}
