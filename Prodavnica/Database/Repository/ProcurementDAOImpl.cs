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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Prodavnica.Database.Repository
{
    public class ProcurementDAOImpl : IProcurement
    {
        public void Create(Procurement procurement, List<ProcurementItem> procurementItems)
        {
            using (var connection = DBUtil.GetConnection())
            {
                try
                {
                    connection.Open();
                    using (var command = new MySqlCommand("InsertNabavka", connection))
                    {
                        string items = JsonConvert.SerializeObject(procurementItems);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@idDobavljac_param", procurement.IdSupplier);
                        command.Parameters.AddWithValue("@DatumKupovine_param", procurement.Date);
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

        public List<Procurement> GetAll()
        {
            List<Procurement> list = new List<Procurement>();
            using (var connection = DBUtil.GetConnection())
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM nabavka";
                    MySqlCommand mySqlCommand = new MySqlCommand(query, connection);
                    using (MySqlDataReader reader = mySqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Procurement procurement = new Procurement
                            {
                                Id = reader.GetInt32("idNabavka"),
                                IdSupplier = reader.GetInt32("idDobavljač"),
                                Date = reader.GetDateTime("DatumKupovine"),
                            };
                            list.Add(procurement);
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

        public List<ProcurementItem> GetItems()
        {
            List<ProcurementItem> list = new List<ProcurementItem>();
            using (var connection = DBUtil.GetConnection())
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM stavka_nabvke";
                    MySqlCommand mySqlCommand = new MySqlCommand(query, connection);
                    using (MySqlDataReader reader = mySqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ProcurementItem procurementItem = new ProcurementItem
                            {
                                IdProcurement = reader.GetInt32("idNabavka"),
                                IdManufacturer = reader.GetInt32("idDobavljač"),
                                IdProduct = reader.GetInt32("idProizvod"),
                                Amount = reader.GetDecimal("Kolicina"),
                                Price = reader.GetDecimal("Cijena")
                            };
                            list.Add(procurementItem);
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
