namespace OnlineKupovinaGUI
{
    public static class GUIUtil
    {
        // Dozvoljava vracanje unazad
        public static void HideCurrentShowNewForm(System.Windows.Forms.Form currentForm, System.Windows.Forms.Form newForm)
        {
            currentForm.Hide();
            newForm.Show();
            newForm.Closed += (s, e) => currentForm.Show();
        }

        // Ne dozvoljava vracanje unazad
        public static void ShowNextForm(System.Windows.Forms.Form currentForm, System.Windows.Forms.Form newForm)
        {
            currentForm.Hide();
            newForm.Show();
        }

    }
}
