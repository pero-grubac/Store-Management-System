using MySql.Data.MySqlClient;
using OnlineKupovinaGUI;
using Prodavnica.Database.DAO;
using Prodavnica.Database.DTO;
using Prodavnica.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Prodavnica.Database.Repository
{
    public class UserDAOImpl : IUser
    {
        public void ChangePassword(User user, string newPassword)
        {
            using (var connection = DBUtil.GetConnection())
            {
                try
                {
                    connection.Open();
                    string query = "UPDATE korisnik SET Lozinka = @newPassword WHERE KorisnickoIme = @username";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@username", user.userName);
                    string hash = Password.HashValue(newPassword);
                    cmd.Parameters.AddWithValue("@newPassword", hash);
                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        user.password = hash;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public void changePassword(User user, string newPassword)
        {
            throw new NotImplementedException();
        }

        public User FindById(int id)
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
                            User user = new User
                            {
                                id = reader.GetInt32("idKorisnik"),
                                firstName = reader.GetString("Ime"),
                                lastName = reader.GetString("Prezime"),
                                email = reader.GetString("Email"),
                                password = reader.GetString("Lozinka"),
                                userName = reader.GetString("KorisnickoIme"),
                                phoneNumber = reader.GetString("BrojTelefona"),
                                isAdmin = reader.GetBoolean("isAdmin"),
                                idTheme = reader.GetInt32("idTema"),
                                idLangugae = reader.GetInt32("idJezik")
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

        public User GetUser(string username, string password)
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
                            User user = new User
                            {
                                id = reader.GetInt32("idKorisnik"),
                                firstName = reader.GetString("Ime"),
                                lastName = reader.GetString("Prezime"),
                                email = reader.GetString("Email"),
                                password = reader.GetString("Lozinka"),
                                userName = reader.GetString("KorisnickoIme"),
                                phoneNumber = reader.GetString("BrojTelefona"),
                                isAdmin = reader.GetBoolean("isAdmin"),
                                idTheme = reader.GetInt32("idTema"),
                                idLangugae = reader.GetInt32("idJezik")
                            };


                            string storedPassword = user.password;
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
    }
}
