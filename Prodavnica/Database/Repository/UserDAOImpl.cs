using MySql.Data.MySqlClient;
using OnlineKupovinaGUI;
using Prodavnica.Database.DAO;
using Prodavnica.Util;

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
                        user.IdLanguage = idLanguage;
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

        public void CreateUser(DTO.User user)
        {
            using (var connection = DBUtil.GetConnection())
            {
                try
                {
                    connection.Open();
                    string query = @"INSERT INTO korisnik (Ime, Prezime, Email, Lozinka, KorisnickoIme, BrojTelefona, isAdmin, idTema, idJezik) 
                                    VALUES (@firstName, @lastName, @email, @password,@userName, @phoneNumber, @isAdmin, @idTheme, @idLanguage);";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@firstName", user.FirstName);
                    command.Parameters.AddWithValue("@lastName", user.LastName);
                    command.Parameters.AddWithValue("@email", user.Email);
                    string hash = Password.HashValue(user.Password);
                    command.Parameters.AddWithValue("@password", hash);
                    command.Parameters.AddWithValue("@userName", user.UserName);
                    command.Parameters.AddWithValue("@phoneNumber", user.PhoneNumber);
                    command.Parameters.AddWithValue("@isAdmin", user.IsAdmin);
                    command.Parameters.AddWithValue("@idTheme", user.IdTheme);
                    command.Parameters.AddWithValue("@idLanguage", user.IdLanguage);
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
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
                                IdLanguage = reader.GetInt32("idJezik")
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

        public List<DTO.User> GetAll(int id)
        {
            List<DTO.User> users = new List<DTO.User>();
            using (var connection = DBUtil.GetConnection())
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM korisnik WHERE idKorisnik != @userIdToExclude";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@userIdToExclude", id);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
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
                                IdLanguage = reader.GetInt32("idJezik")
                            };
                            users.Add(user);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            return users;
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
                                IdLanguage = reader.GetInt32("idJezik")
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
