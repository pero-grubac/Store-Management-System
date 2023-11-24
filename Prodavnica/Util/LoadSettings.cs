using Org.BouncyCastle.Asn1.Crmf;
using Org.BouncyCastle.Tls;
using Prodavnica.Database.DTO;
using Prodavnica.Database.Repository;
using Prodavnica.Language;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prodavnica.Util
{
    public class LoadSettings
    {

        public static void ApplySettins(User user, Form form)
        {
            ThemeDAOImpl themeDAO = new ThemeDAOImpl();
            Theme theme = themeDAO.FindById(user.Id);
            Color color = themeDAO.GetColor(theme);
            Font font = themeDAO.GetFont(theme);

            LanguageDAOImpl languageDAO = new LanguageDAOImpl();
            Database.DTO.Language language = languageDAO.GetLanguageById(user.IdLangugae);
            LanguageHelper.ChangeLanguage(language.Name);
            ChangeColor(form.Controls, color);
            ChangeFont(form.Controls, font);
        }

        private static void ChangeColor(Control.ControlCollection controls, Color color)
        {
            foreach (Control control in controls)
            {
                if (control is Button button)
                {
                    button.BackColor = color;
                }
                else if (control is TableLayoutPanel tbl)
                {
                    tbl.BackColor = color;
                    ChangeColor(tbl.Controls, color);
                }
                else if (control is GroupBox || control is Panel)
                {
                    ChangeColor(control.Controls, color);
                }
                else if (control is TabControl tabControl)
                {
                    tabControl.BackColor = color;
                    foreach (TabPage tabPage in tabControl.TabPages)
                    {
                        tabPage.BackColor = color;
                        ChangeColor(tabPage.Controls, color);
                    }
                }
                else if(control is DataGridView dgv)
                {
                    dgv.DefaultCellStyle.SelectionBackColor = color;
                    dgv.Refresh();
                }
            }
        }
        private static void ChangeFont(Control.ControlCollection controls, Font font)
        {
            foreach (Control control in controls)
            {
                if (control is Button button)
                {
                    button.Font = font;
                }
                else if (control is TableLayoutPanel tbl)
                {
                    tbl.Font = font;
                    ChangeFont(tbl.Controls, font);
                }
                else if (control is GroupBox || control is Panel)
                {
                    ChangeFont(control.Controls, font);
                }
                else if (control is TabControl tabControl)
                {
                    tabControl.Font = font;
                    foreach (TabPage tabPage in tabControl.TabPages)
                    {
                        tabPage.Font = font;
                        ChangeFont(tabPage.Controls, font);
                    }
                }
            }
        }
    }
}
