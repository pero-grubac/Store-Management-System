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
    public class CategoryDAOImpl : ICategory
    {
        public List<Category> GetAll()
        {
            List<Category> categories = new List<Category>();
            using (var connection = DBUtil.GetConnection())
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM kategorija";
                    MySqlCommand mySqlCommand = new MySqlCommand(query, connection);
                    using (MySqlDataReader reader = mySqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Category category = new Category();
                            category.Name = reader.GetString("Naziv");
                            category.Id = reader.GetInt32("idKategorija");
                            categories.Add(category);
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
            return categories;
        }
    }
}
