using MySql.Data.MySqlClient;
using OnlineKupovinaGUI;
using Prodavnica.Database.DAO;
using Prodavnica.Database.DTO;

namespace Prodavnica.Database.Repository
{
    public class ThemeDAOImpl : ITheme
    {
        public void ChangeTheme(Theme newTheme)
        {
            using (var connection = DBUtil.GetConnection())
            {
                try
                {
                    connection.Open();
                    string query = "UPDATE tema SET boja = @colorName, font = @fontName, veličina = @size, fontStil = @fontStyle, strikeout = @isStrikeout, underline = @isUnderline WHERE idTema = @themeId";

                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@colorName", newTheme.ColorName);
                    cmd.Parameters.AddWithValue("@fontName", newTheme.FontName);
                    cmd.Parameters.AddWithValue("@size", newTheme.Size);
                    cmd.Parameters.AddWithValue("@fontStyle", newTheme.FontStyle);
                    cmd.Parameters.AddWithValue("@isStrikeout", newTheme.IsStrikeout);
                    cmd.Parameters.AddWithValue("@isUnderline", newTheme.IsUnderline);
                    cmd.Parameters.AddWithValue("@themeId", newTheme.Id);

                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public void CreateTheme(ref Theme theme)
        {
            using (var connection = DBUtil.GetConnection())
            {
                try
                {
                    connection.Open();

                    string query = @"INSERT INTO tema (boja, font, veličina, fontStil, strikeout, underline) 
                                     VALUES (@ColorName, @FontName, @Size, @FontStyle, @IsStrikeout, @IsUnderline);
                                     SELECT LAST_INSERT_ID();";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ColorName", theme.ColorName);
                    command.Parameters.AddWithValue("@FontName", theme.FontName);
                    command.Parameters.AddWithValue("@FontStyle", theme.FontStyle);
                    command.Parameters.AddWithValue("@Size", theme.Size);
                    command.Parameters.AddWithValue("@IsStrikeout", theme.IsStrikeout);
                    command.Parameters.AddWithValue("@IsUnderline", theme.IsUnderline);
                    theme.Id = Convert.ToInt32(command.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public Theme FindById(int id)
        {
            Theme theme = null;
            using (var connection = DBUtil.GetConnection())
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM tema WHERE idTema = @themeId";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@themeId", id);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            theme = new Theme
                            {
                                Id = Convert.ToInt32(reader["idTema"]),
                                ColorName = reader["boja"].ToString(),
                                FontName = reader["font"].ToString(),
                                Size = Convert.ToInt32(reader["veličina"]),
                                FontStyle = reader["fontStil"].ToString(),
                                IsStrikeout = Convert.ToBoolean(reader["strikeout"]),
                                IsUnderline = Convert.ToBoolean(reader["underline"])
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            return theme;
        }

        public Color GetColor(Theme theme)
        {
            string[] colorValues = theme.ColorName.Split(',');
            if (colorValues.Length == 4)
            {
                int alpha = int.Parse(colorValues[0]);
                int red = int.Parse(colorValues[1]);
                int green = int.Parse(colorValues[2]);
                int blue = int.Parse(colorValues[3]);

                return Color.FromArgb(alpha, red, green, blue);
            }

            return Color.Black;
        }

        public Font GetFont(Theme theme)
        {
            Font font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            try
            {
                FontStyle fs = FontStyle.Regular;
                if (Enum.TryParse(theme.FontStyle, true, out FontStyle style))
                {
                    fs = style;
                }
                if (theme.IsStrikeout)
                {
                    fs |= FontStyle.Strikeout;
                }
                if (theme.IsUnderline)
                {
                    fs |= FontStyle.Underline;
                }
                font = new Font(theme.FontName, theme.Size, fs, GraphicsUnit.Point, 0, false);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return font;
        }
    }
}
