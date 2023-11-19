using MySql.Data.MySqlClient;
using OnlineKupovinaGUI;
using Prodavnica.Database.DAO;
using Prodavnica.Database.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    cmd.Parameters.AddWithValue("@colorName", newTheme.colorName);
                    cmd.Parameters.AddWithValue("@fontName", newTheme.fontName);
                    cmd.Parameters.AddWithValue("@size", newTheme.size);
                    cmd.Parameters.AddWithValue("@fontStyle", newTheme.fontStyle);
                    cmd.Parameters.AddWithValue("@isStrikeout", newTheme.isStrikeout);
                    cmd.Parameters.AddWithValue("@isUnderline", newTheme.isUnderline);
                    cmd.Parameters.AddWithValue("@themeId", newTheme.id);

                    cmd.ExecuteNonQuery();
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
                                id = Convert.ToInt32(reader["idTema"]),
                                colorName = reader["boja"].ToString(),
                                fontName = reader["font"].ToString(),
                                size = Convert.ToInt32(reader["veličina"]),
                                fontStyle = reader["fontStil"].ToString(),
                                isStrikeout = Convert.ToBoolean(reader["strikeout"]),
                                isUnderline = Convert.ToBoolean(reader["underline"])
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
            string[] colorValues = theme.colorName.Split(',');
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
                if (Enum.TryParse(theme.fontStyle, true, out FontStyle style))
                {
                    fs = style;
                }
                if (theme.isStrikeout)
                {
                    fs |= FontStyle.Strikeout;
                }
                if (theme.isUnderline)
                {
                    fs |= FontStyle.Underline;
                }
                font = new Font(theme.fontName, theme.size, fs, GraphicsUnit.Point, 0, false);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return font;
        }
    }
}
