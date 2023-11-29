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
        public void CreateProduct(Product product)
        {
            using (var connection = DBUtil.GetConnection())
            {
                try
                {
                    connection.Open();

                    string query = @"
                INSERT INTO proizvod (BarKod, Naziv, DatumIsteka, idKategorija, Cijena, Zalihe, idProizvodjac, Opis)
                VALUES (@BarCode, @Name, @ExpirationDate, @IdCategory, @Price, @Supplies, @IdManufacturer, @Description);";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@BarCode", product.BarCode);
                    command.Parameters.AddWithValue("@Name", product.Name);
                    command.Parameters.AddWithValue("@ExpirationDate", product.ExpirationDate);
                    command.Parameters.AddWithValue("@IdCategory", product.IdCategory);
                    command.Parameters.AddWithValue("@Price", product.Price);
                    command.Parameters.AddWithValue("@Supplies", product.Supplies);
                    command.Parameters.AddWithValue("@IdManufacturer", product.IdManufacturer);
                    command.Parameters.AddWithValue("@Description", product.Description);

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

        public void DeleteProduct(int id)
        {
            using (var connection = DBUtil.GetConnection())
            {
                try
                {
                    connection.Open();
                    string query = "DELETE FROM proizvod WHERE idProizvod=@id";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
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

        public void UpdateProduct(Product product)
        {
            using (var connection = DBUtil.GetConnection())
            {
                try
                {
                    connection.Open();
                    string query = @"
                                    UPDATE proizvod 
                                    SET 
                                       BarKod = @BarCode,
                                       Naziv = @Name,
                                       DatumIsteka = @ExpirationDate,
                                       idKategorija = @IdCategory,
                                       Cijena = @Price,
                                       Zalihe = @Supplies,
                                       idProizvodjac = @IdManufacturer,
                                       Opis = @Description
                                    WHERE idProizvod = @Id;";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@BarCode", product.BarCode);
                    command.Parameters.AddWithValue("@Name", product.Name);
                    command.Parameters.AddWithValue("@ExpirationDate", product.ExpirationDate);
                    command.Parameters.AddWithValue("@IdCategory", product.IdCategory);
                    command.Parameters.AddWithValue("@Price", product.Price);
                    command.Parameters.AddWithValue("@Supplies", product.Supplies);
                    command.Parameters.AddWithValue("@IdManufacturer", product.IdManufacturer);
                    command.Parameters.AddWithValue("@Description", product.Description);
                    command.Parameters.AddWithValue("@Id", product.Id);

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
    }
}
