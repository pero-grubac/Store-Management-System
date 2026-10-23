using Prodavnica.App;
using Prodavnica.Language;
using Prodavnica.Shell;
using Prodavnica.UI;

namespace Prodavnica
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            LanguageHelper.ChangeLanguage("Srpski(Latinica)");
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += (_, e) => Dialogs.Error(Form.ActiveForm, T.Error(e.Exception));
            Application.Run(new AppContext());
        }

        // Login → main window → (sign out) → login again; closing the main window ends the app.
        private sealed class AppContext : ApplicationContext
        {
            public AppContext()
            {
                ShowLogin();
            }

            private void ShowLogin()
            {
                var login = new LoginForm();
                login.SignedIn += (_, _) =>
                {
                    var main = new MainForm();
                    main.FormClosed += (_, _) =>
                    {
                        if (main.SignedOut)
                        {
                            Session.End();
                            ShowLogin();
                        }
                        else
                        {
                            ExitThread();
                        }
                    };
                    MainForm = main;
                    main.Show();
                    login.Hide();
                    login.Dispose();
                };
                login.FormClosed += (_, _) => { if (!login.Succeeded) ExitThread(); };
                MainForm = login;
                login.Show();
            }
        }
    }
}
