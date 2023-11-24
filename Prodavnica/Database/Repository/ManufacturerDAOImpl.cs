using MySql.Data.MySqlClient;
using OnlineKupovinaGUI;
using Prodavnica.Database.DAO;
using Prodavnica.Database.DTO;
using Prodavnica.Database.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prodavnica.Database.Repository
{
    public class ManufacturerDAOImpl : IManufacturer
    {
        public void AddManufactuer(Manufacturer manufacturer)
        {
            using (var connection = DBUtil.GetConnection())
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO proizvodjac (Ime, Email) VALUES (@Ime, @Email)";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Ime", manufacturer.Name);
                    command.Parameters.AddWithValue("@Email", manufacturer.Email); 
                    command.ExecuteNonQuery();
                }
                catch (DBException e)
                {
                    MessageBox.Show(e.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public List<Manufacturer> GetAll()
        {
            List<Manufacturer> manufacturers = new List<Manufacturer>();
            using (var connection = DBUtil.GetConnection())
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM proizvodjac";
                    MySqlCommand mySqlCommand = new MySqlCommand(query, connection);
                    using (MySqlDataReader reader = mySqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Manufacturer manufacturer = new Manufacturer();
                            manufacturer.Name = reader.GetString("Ime");
                            manufacturer.Email= reader.GetString("Email");
                            manufacturer.id= reader.GetInt32("idProizvodjac");
                            manufacturers.Add(manufacturer);
                        }
                    }
                }
                catch (DBException e)
                {
                    MessageBox.Show(e.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            return manufacturers;
        }
    }
}
