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
    public class ProductsDAOImpl : IProducts
    {
        public List<Product> GetAll()
        {
            List<Product> products = new List<Product>();
            using (var connection = DBUtil.GetConnection())
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM proizvod";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DTO.Product product = new DTO.Product();
                            product.Id = reader.GetInt32("idProizvod");
                            product.Name = reader.GetString("Naziv");
                            product.Price = reader.GetDecimal("Cijena");
                            product.Supplies = reader.GetInt32("Zalihe");
                            product.ExpirationDate = reader.GetDateTime("DatumIsteka");
                            product.BarCode = reader.GetString("BarKod");
                            product.IdManufacturer = reader.GetInt32("idProizvodjac");
                            product.Description = reader.GetString("Opis");
                            product.IdCategory = reader.GetInt32("idKategorija");

                            products.Add(product);
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
            return products;
        }
    }
}
