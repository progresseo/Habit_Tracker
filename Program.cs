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
                                [Date] TEXT,
                                [Quantity] INTEGER)";
            //System.Data.SQLite.SQLiteConnection.CreateFile("test1.db3");
            using (System.Data.SQLite.SQLiteConnection conn = new System.Data.SQLite.SQLiteConnection("data source=test1.db3"))
            {
                using (System.Data.SQLite.SQLiteCommand cmd = new System.Data.SQLite.SQLiteCommand(conn))
                {
                    conn.Open();
                    cmd.CommandText = createQuery;
                    cmd.ExecuteNonQuery();

                    bool closeConnection = false;
                    while(closeConnection == false)
                    {
                        //Menu for users
                        Console.WriteLine(" Please enter 1 to add an entry. \n Please enter 2 to update an entry \n Please enter 3 to delete an entry \n Please enter 4 to show all entries \n Please select 0 to exit");
                        int optionChosen = Convert.ToInt32(Console.ReadLine());
                        switch (optionChosen)
                        {
                            case 0: closeConnection = true;   break;
                            case 1: InputInfo(); break;
                            case 2: Update(); break;
                            case 3: Delete(); break;
                            case 4: DisplayAll(); break;
                            default:
                                Console.WriteLine("Please enter numbers between 0-4");
                                break;
                        }
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
                    Console.WriteLine("Enter the date");
                    string date = Console.ReadLine();
                    //if (DateTime.TryParseExact(date, "dd MMM yyyy", out DateTime result))
                    //{

                    //}else
                    //{

                    //}
                        Console.WriteLine("How many glasses of water did you drink?");
                    string quantity = Console.ReadLine();
                    
                    if (int.TryParse(quantity, out int result1))
                    {
                        cmd.CommandText = "INSERT INTO MyTable(Date,Quantity) values(@datevalue,@quantityvalue)";
                        cmd.Parameters.AddWithValue("@datevalue", date);
                        cmd.Parameters.AddWithValue("@quantityvalue", quantity);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                    else
                    {
                        Console.WriteLine("Please make sure to enter a number e.g 5, no decimals allowed");
                       
                    }

                    
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
                    Console.WriteLine("How many glasses of water did you drink?");
                    string quantityUpdate = Console.ReadLine();
                    cmd.CommandText = "UPDATE MyTable set Quantity = @newQuantity WHERE Id = @idvalue";
                    cmd.Parameters.AddWithValue("@idvalue", idUpdate);
                    cmd.Parameters.AddWithValue("@newQuantity", quantityUpdate);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
        static void DisplayAll()
        {
            Console.Clear();
            using (System.Data.SQLite.SQLiteConnection conn = new System.Data.SQLite.SQLiteConnection("data source=test1.db3"))
            {
                using (System.Data.SQLite.SQLiteCommand cmd = new System.Data.SQLite.SQLiteCommand(conn))
                {
                    conn.Open();
                    //Displays all records
                    cmd.CommandText = "SELECT * from MyTable";
                    using (System.Data.SQLite.SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        Console.WriteLine(reader.GetName(0) + ":" + reader.GetName(1) + ":" + reader.GetName(2));
                        while (reader.Read())
                        {
                            // Console.WriteLine(readerreader["Id"] + ":" + reader["Name"] + ":" + reader["Gender"]);
                            
                            Console.WriteLine(reader.GetValue(0) + ":" + reader.GetValue(1) + ":" + reader.GetValue(2));

                        }
                        
                    }
                    conn.Close();
                }
            }
        }
    }
}
