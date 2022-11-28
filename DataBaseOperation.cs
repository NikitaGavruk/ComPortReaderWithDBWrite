using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ComportWriteInBD
{
    
    public class DataBaseOperation
    {
        static string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=adonetdb;Integrated Security=True";

        public static void WriteToDB(string component)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DateTime dateTime = DateTime.Now;
                string part = EditPart();
                connection.Open();
                try
                {
                    var count = new SqlCommand($"SELECT Count FROM COMDataBase WHERE Part='{part}'", connection).ExecuteScalar();
                    UpdateObject(part, dateTime, count);

                }
                    catch(Exception ex)
                {
                    SqlCommand command = new SqlCommand($"INSERT INTO COMDataBase (Part, Descr, Count, [Last accesed]) VALUES ('{part}', '{component}', '{1}', '{dateTime}')", connection);
                    int number = command.ExecuteNonQuery();
                    Console.WriteLine("Objects wrote: {0}", number);
                }

            }
            Console.Read();
        }
        private static void UpdateObject(string part, DateTime dateTime, int count)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                count++;
                SqlCommand command = new SqlCommand($"UPDATE COMDataBase SET Count='{count}', [Last accesed]='{dateTime}' WHERE Part='{part}'", connection);
                int number = command.ExecuteNonQuery();
                Console.WriteLine("Objects updated: {0}", number);
            }
        }
        private static string EditPart()
        {
            Console.Write("Write part: ");
            return Console.ReadLine();
        }
    }
}
