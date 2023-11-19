﻿using Org.BouncyCastle.Asn1.Crmf;
using Org.BouncyCastle.Tls;
using Prodavnica.Database.DTO;
using Prodavnica.Database.Repository;
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
            Theme theme = themeDAO.FindById(user.id);
            Color color = themeDAO.GetColor(theme);
            Font font = themeDAO.GetFont(theme);

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
                else if (control is TableLayoutPanel || control is GroupBox || control is Panel)
                {
                    ChangeColor(control.Controls, color);
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
                /*  else if (control is Label label)
                  {
                      label.Font = font;
                  }
                */
                else if (control is TableLayoutPanel || control is GroupBox || control is Panel)
                {
                    ChangeFont(control.Controls, font);
                }
            }
        }
    }
}
