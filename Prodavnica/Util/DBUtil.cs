using MySql.Data.MySqlClient;
namespace OnlineKupovinaGUI
{
    public class DBUtil
    {
        // TO DO 
        // napravi config fajl i iz njega citaj podatke
        private static string databaseFile = "..//..//..//Resources//db.txt";
        private static string connectioString = "Server=localhost; Port=3306; Database=prodavnica; User Id=root; Password=root";
        private static string ReadFIle()
        {
            if (File.Exists(databaseFile))
            {
                try
                {
                    return File.ReadAllText(databaseFile);
                }
                catch (IOException ex)
                {
                    Console.WriteLine("Error reading the file: " + ex.Message);
                }
                catch (UnauthorizedAccessException ex)
                {
                    Console.WriteLine("Unauthorized access to the file: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            return connectioString;

        }
        public static MySqlConnection GetConnection()
        {
            string connection = ReadFIle();
            return new MySqlConnection(connection);
        }
    }
}
