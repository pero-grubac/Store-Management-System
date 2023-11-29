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
    public class SupplierDAOImpl : ISupplier
    {
        public void AddSupplier(Supplier supplier)
        {
            using (var connection = DBUtil.GetConnection())
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO dobavljac (Ime) VALUES (@Name)";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Name", supplier.Name);
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

        public List<Supplier> GetAll()
        {
            List<Supplier> suppliers = new List<Supplier>();
            using (var connection = DBUtil.GetConnection())
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM dobavljac";
                    MySqlCommand mySqlCommand = new MySqlCommand(query, connection);
                    using (MySqlDataReader reader = mySqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Supplier supplier = new Supplier();
                            supplier.Name = reader.GetString("Ime");
                            supplier.Email = reader.GetString("Email");
                            supplier.Id = reader.GetInt32("idDobavljac");
                            suppliers.Add(supplier);
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
            return suppliers;
        }
    }
}
