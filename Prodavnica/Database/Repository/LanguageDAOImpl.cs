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
    public class LanguageDAOImpl : ILanguage
    {
        public List<DTO.Language> GetAll()
        {
            List<DTO.Language> languages = new List<DTO.Language>();
            using (var connection = DBUtil.GetConnection())
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM jezik";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DTO.Language language = new DTO.Language();
                            language.Id = Convert.ToInt32(reader["idJezik"]);
                            language.Name = reader["Naziv"].ToString();

                            languages.Add(language);
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
            return languages;
        }

        public DTO.Language GetLanguageById(int id)
        {
            DTO.Language language = new DTO.Language();
            using (var connection = DBUtil.GetConnection())
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM jezik WHERE idJezik = @id";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@id", id);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            language.Id = Convert.ToInt32(reader["idJezik"]);
                            language.Name = reader["Naziv"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            return language;
        }

        public List<string> GetLanguages()
        {
            List<DTO.Language> languages = GetAll();
            List<string> list = languages.Select(lang => lang.Name).ToList();
            return list;
        }

        public string GetSelectedLanguageName(int id)
        {
            string language = "";
            using (var connection = DBUtil.GetConnection())
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM jezik WHERE idJezik = @id";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@id", id);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            language = reader["Naziv"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            return language;
        }
    }
}
