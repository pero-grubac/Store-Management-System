namespace Prodavnica.Database.DTO
{
    public class User
    {
        public int Id { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }
        public String UserName { get; set; }
        public String PhoneNumber { get; set; }
        public Boolean IsAdmin { get; set; }
        public int IdTheme { get; set; }
        public int IdLanguage { get; set; }

    }
}
