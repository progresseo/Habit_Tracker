﻿using System;
using System.Data.SQLite;
using System.Globalization;

namespace Habit_Tracker
{
    class Program
    {
        static void Main(string[] args)
        {

            string createQuery = @"CREATE TABLE IF NOT EXISTS
                                [MyTable] (
                                [Id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                                [Date] TEXT unique,
                                [Quantity] INTEGER)";
            
            //System.Data.SQLite.SQLiteConnection.CreateFile("test1.db3");
            using (SQLiteConnection conn = new SQLiteConnection("data source=test1.db3"))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(conn))
                {
                    conn.Open();
                    cmd.CommandText = createQuery;
                    cmd.ExecuteNonQuery();

                    bool closeConnection = false;
                    while(closeConnection == false)
                    {
                        //Menu for users
                        Console.WriteLine(" Please enter 1 to add an entry. \n Please enter 2 to update an entry \n Please enter 3 to delete an entry \n Please enter 4 to show all entries \n Please select 0 to exit");
                        //
                        //int optionChosen = Convert.ToInt32(Console.ReadLine());
                        string optionChosen = Console.ReadLine();
                        bool conversionSuccess = int.TryParse(optionChosen, out int input);
                        if (conversionSuccess == true)
                        {
                            switch (input)
                            {
                                case 0: closeConnection = true; break;
                                case 1: InputInfo(); break;
                                case 2: Update(); break;
                                case 3: Delete(); break;
                                case 4: DisplayAll(); break;
                                default:
                                    Console.WriteLine("Please enter numbers between 0-4");
                                    break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Please enter a  number between 0 -4 e.g 1"); 
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
            using (SQLiteConnection conn = new SQLiteConnection("data source=test1.db3"))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(conn))
                {

                    conn.Open();
                    //Inputing information
                    Console.WriteLine("Enter the date in the format  e.g 21 June 2022");
                    string date = Console.ReadLine();

                    bool conversion = DateTime.TryParseExact(date, "dd MMMM yyyy", CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out DateTime dateToInput);
                    if (conversion == true)
                    {
                        cmd.Parameters.AddWithValue("@datevalue", date);
                    }
                    else
                    {
                        while (conversion == false)

                        {
                            Console.WriteLine("Please make sure the date is in the correct format dd MMMM yyyy e.g 21 Aug 2022");
                            string date2 = Console.ReadLine();
                            if (DateTime.TryParseExact(date2, "dd MMMM yyyy", CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out DateTime dateToInput2))
                            {
                                cmd.Parameters.AddWithValue("@datevalue", date2);
                                conversion = true;

                            }

                        }
                    }
                    
                   
                    Console.WriteLine("How many glasses of water did you drink?");
                    string quantity = Console.ReadLine();
                    bool conversionQuantity = int.TryParse(quantity, out int quntityToInput);
                    if (conversionQuantity == true)
                    {
                        cmd.Parameters.AddWithValue("@quantityvalue", quantity);
                    }
                    else
                    {
                        while (conversionQuantity == false)
                        {
                            Console.WriteLine("Please make sure to enter a number e.g 5, no decimals allowed");
                            string quantity2 = Console.ReadLine();
                            if (int.TryParse(quantity2, out int quantityToInput2))
                            {
                                cmd.Parameters.AddWithValue("@quantityvalue", quantity);
                                conversionQuantity = true;
                            }
                        }
                    }
                   
                    
                   
                    cmd.CommandText = "INSERT INTO MyTable(Date,Quantity) values(@datevalue,@quantityvalue)";

                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch
                    {
                        Console.WriteLine("An entry for that date already exists.");
                    }
                    
                    
                    conn.Close();

                }
            }
        }
        static void Delete()
        {
            using (SQLiteConnection conn = new SQLiteConnection("data source=test1.db3"))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(conn))
                {
                    conn.Open();
                    //Deleting a specific entry
                    Console.WriteLine("What entry do you want to delete?");
                    string date = Console.ReadLine();
                    bool conversion = DateTime.TryParseExact(date, "dd MMMM yyyy", CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out DateTime dateToDelete);
                    if (conversion == true)
                    {
                        cmd.CommandText = "SELECT * FROM MyTable";
                        using (System.Data.SQLite.SQLiteDataReader reader = cmd.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                if (reader["Date"].ToString() == date)
                                {
                                    Console.WriteLine("The entry has been found and deleted.");
                                    cmd.Parameters.AddWithValue("@dateValue", date);
                                    break;
                                }
                                

                            }
                        }
                        
                    }
                    else
                    {
                        while (conversion == false)

                        {
                            Console.WriteLine("Please make sure the date is in the correct format dd MMMM yyyy e.g 21 Aug 2022");
                            string date2 = Console.ReadLine();

                            if (DateTime.TryParseExact(date2, "dd MMMM yyyy", CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out DateTime dateToDelete2))
                            {
                                
                                cmd.CommandText = "SELECT * FROM MyTable";
                                using (System.Data.SQLite.SQLiteDataReader reader = cmd.ExecuteReader())
                                {

                                    while (reader.Read())
                                    {
                                        if (reader["Date"].ToString() == date2)
                                        {
                                            Console.WriteLine("The entry has been found and deleted.");
                                            cmd.Parameters.AddWithValue("@dateValue", date2);
                                            break;
                                        }
                                       

                                    }
                                }


                                conversion = true;

                            }

                        }
                    }
                    cmd.CommandText = "DELETE FROM MyTable WHERE Date=@dateValue";


                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch
                    {
                        Console.WriteLine("The entry for this date does not exist.\n");
                    }


                    conn.Close();

                }
            }
        }
        
        static void Update()
        {
            using (SQLiteConnection conn = new SQLiteConnection("data source=test1.db3"))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(conn))
                {
                    conn.Open();
                    //Updating an entry
                    Console.WriteLine("Enter the date for the entry you want to update in the format  e.g 21 June 2022");
                    string date = Console.ReadLine();

                    bool conversion = DateTime.TryParseExact(date, "dd MMMM yyyy", CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out DateTime dateToUpdate);
                    if (conversion == true)
                    {
                        cmd.CommandText = "SELECT * FROM MyTable";
                        using (System.Data.SQLite.SQLiteDataReader reader = cmd.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                if (reader["Date"].ToString() == date)
                                {
                                    Console.WriteLine("The entry has been found and deleted.");
                                    cmd.Parameters.AddWithValue("@dateValue", date);
                                    break;
                                }


                            }
                        }
                    }
                    else
                    {
                        while (conversion == false)

                        {
                            Console.WriteLine("Please make sure the date is in the correct format dd MMMM yyyy e.g 21 Aug 2022");
                            string date2 = Console.ReadLine();
                            if (DateTime.TryParseExact(date2, "dd MMMM yyyy", CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out DateTime dateToUpdate2))
                            {
                                cmd.CommandText = "SELECT * FROM MyTable";
                                using (System.Data.SQLite.SQLiteDataReader reader = cmd.ExecuteReader())
                                {

                                    while (reader.Read())
                                    {
                                        if (reader["Date"].ToString() == date2)
                                        {
                                            Console.WriteLine("The entry has been found and deleted.");
                                            cmd.Parameters.AddWithValue("@dateValue", date2);
                                            break;
                                        }


                                    }
                                }
                                
                                conversion = true;

                            }

                        }
                    }


                    Console.WriteLine("How many glasses of water did you drink?");
                    string quantity = Console.ReadLine();
                    bool conversionQuantity = int.TryParse(quantity, out int quantityToUpdate);
                    if (conversionQuantity == true)
                    {
                        cmd.Parameters.AddWithValue("@newQuantity", quantity);
                    }
                    else
                    {
                        while (conversionQuantity == false)
                        {
                            Console.WriteLine("Please make sure to enter a number e.g 5, no decimals allowed");
                            string quantity2 = Console.ReadLine();
                            if (int.TryParse(quantity2, out int quantityToUpdate2))
                            {
                                cmd.Parameters.AddWithValue("@newQuantity", quantity2);
                                conversionQuantity = true;
                            }
                        }
                    }
                    cmd.CommandText = "UPDATE MyTable set Quantity = @newQuantity WHERE Date = @dateValue";

                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch
                    {
                        Console.WriteLine("The entry for this date does not exist.\n");
                    }
                    conn.Close();
                }
            }
        }
        static void DisplayAll()
        {
            Console.Clear();
            using (SQLiteConnection conn = new SQLiteConnection("data source=test1.db3"))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(conn))
                {
                    conn.Open();
                    //Displays all records
                    cmd.CommandText = "SELECT * from MyTable";
                    using (System.Data.SQLite.SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        Console.WriteLine(reader.GetName(0) + ":" + reader.GetName(1) + ":" + reader.GetName(2));
                        while (reader.Read())
                        {
                            // Console.WriteLine(reader["Id"] + ":" + reader["Name"] + ":" + reader["Gender"]);
                            
                            Console.WriteLine(reader.GetValue(0) + ":" + reader.GetValue(1) + ":" + reader.GetValue(2));

                        }
                        
                    }
                    conn.Close();
                }
            }
        }
    }
}
