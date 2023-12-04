using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using OnlineKupovinaGUI;
using Prodavnica.Database.DAO;
using Prodavnica.Database.DTO;
using Prodavnica.Database.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prodavnica.Database.Repository
{
    public class BillDAOImpl : IBill
    {
        public void Create(Bill bill, List<BillItem> billItems)
        {
            using (var connection = DBUtil.GetConnection())
            {
                try
                {
                    connection.Open();
                    using (var command = new MySqlCommand("InsertRacunWithStavka", connection))
                    {
                        string items = JsonConvert.SerializeObject(billItems);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@DatumKupovine_param", bill.Date);
                        command.Parameters.AddWithValue("@stavke_list", items);
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        public List<Bill> GetAllBill()
        {
            List<Bill> list = new List<Bill>();
            using (var connection = DBUtil.GetConnection())
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM racun";
                    MySqlCommand mySqlCommand = new MySqlCommand(query, connection);
                    using (MySqlDataReader reader = mySqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Bill bill = new Bill
                            {
                                Date = reader.GetDateTime("DatumKupovine"),
                                Id=reader.GetInt32("idRacun"),
                            };
                            list.Add(bill);
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
            return list;
        }

        public List<BillItem> GetAllBillItem()
        {
            List<BillItem> list = new List<BillItem>();
            using (var connection = DBUtil.GetConnection())
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM stavka";
                    MySqlCommand mySqlCommand = new MySqlCommand(query, connection);
                    using (MySqlDataReader reader = mySqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            BillItem billItem = new BillItem
                            {
                                Amount = reader.GetDecimal("Kolicina"),
                                IdBill = reader.GetInt32("idRacun"),
                                IdProduct = reader.GetInt32("idProizvod"),
                                Price = reader.GetDecimal("Cijena")
                            };
                            list.Add(billItem);
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
            return list;
        }
    }
}
