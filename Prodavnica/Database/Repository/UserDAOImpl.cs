using Microsoft.VisualBasic.ApplicationServices;
using MySql.Data.MySqlClient;
using OnlineKupovinaGUI;
using Prodavnica.Database.DAO;
using Prodavnica.Database.DTO;
using Prodavnica.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Prodavnica.Database.Repository
{
    public class UserDAOImpl : IUser
    {
        public void ChangeLanguage(ref DTO.User user, int idLanguage)
        {
            using (var connection = DBUtil.GetConnection())
            {
                try
                {
                    connection.Open();
                    string query = "UPDATE korisnik SET idJezik = @idLanguage WHERE idKorisnik = @id";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@idLanguage", idLanguage);
                    cmd.Parameters.AddWithValue("@id", user.Id);
                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        user.IdLangugae = idLanguage;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public void ChangePassword(ref DTO.User user, string newPassword)
        {
            using (var connection = DBUtil.GetConnection())
            {
                try
                {
                    connection.Open();
                    string query = "UPDATE korisnik SET Lozinka = @newPassword WHERE KorisnickoIme = @username";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@username", user.UserName);
                    string hash = Password.HashValue(newPassword);
                    cmd.Parameters.AddWithValue("@newPassword", hash);
                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        user.Password = hash;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }


        public DTO.User FindById(int id)
        {
            using (var connection = DBUtil.GetConnection())
            {
                try
                {
                    connection.Open();
                    string query = "SELECT idKorisnik, Ime, Prezime, Email, Lozinka, KorisnickoIme, BrojTelefona, isAdmin, idTema, idJezik FROM korisnik WHERE idKorisnik = @id";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@id", id);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            DTO.User user = new DTO.User
                            {
                                Id = reader.GetInt32("idKorisnik"),
                                FirstName = reader.GetString("Ime"),
                                LastName = reader.GetString("Prezime"),
                                Email = reader.GetString("Email"),
                                Password = reader.GetString("Lozinka"),
                                UserName = reader.GetString("KorisnickoIme"),
                                PhoneNumber = reader.GetString("BrojTelefona"),
                                IsAdmin = reader.GetBoolean("isAdmin"),
                                IdTheme = reader.GetInt32("idTema"),
                                IdLangugae = reader.GetInt32("idJezik")
                            };
                            return user;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }

            return null;
        }

        public DTO.User GetUser(string username, string password)
        {
            using (var connection = DBUtil.GetConnection())
            {
                try
                {
                    connection.Open();
                    string query = "SELECT idKorisnik, Ime, Prezime, Email, Lozinka, KorisnickoIme, BrojTelefona, isAdmin, idTema, idJezik FROM korisnik WHERE KorisnickoIme = @username";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@username", username);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            DTO.User user = new DTO.User
                            {
                                Id = reader.GetInt32("idKorisnik"),
                                FirstName = reader.GetString("Ime"),
                                LastName = reader.GetString("Prezime"),
                                Email = reader.GetString("Email"),
                                Password = reader.GetString("Lozinka"),
                                UserName = reader.GetString("KorisnickoIme"),
                                PhoneNumber = reader.GetString("BrojTelefona"),
                                IsAdmin = reader.GetBoolean("isAdmin"),
                                IdTheme = reader.GetInt32("idTema"),
                                IdLangugae = reader.GetInt32("idJezik")
                            };


                            string storedPassword = user.Password;
                            string hashedInputPassword = Password.HashValue(password);

                            if (Password.Verify(password, storedPassword))
                            {
                                return user;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // TODO napravi logger 
                    MessageBox.Show("Error: " + ex.Message);
                }
            }

            return null;
        }

        public void SaveUser(DTO.User user)
        {
            using (var connection = DBUtil.GetConnection())
            {
                try
                {
                    connection.Open();
                    string query = "UPDATE korisnik SET KorisnickoIme = @newUsername, Ime = @newFirstName, Prezime = @newLastName, BrojTelefona = @newPhoneNumber, Email = @newEmail WHERE idKorisnik = @userId";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@newUsername", user.UserName);
                    command.Parameters.AddWithValue("@newFirstName", user.FirstName);
                    command.Parameters.AddWithValue("@newLastName", user.LastName);
                    command.Parameters.AddWithValue("@newPhoneNumber", user.PhoneNumber);
                    command.Parameters.AddWithValue("@newEmail", user.Email);
                    command.Parameters.AddWithValue("@userId", user.Id);

                    int rowsAffected = command.ExecuteNonQuery();   
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);    
                }
            }
        }
    }
}
