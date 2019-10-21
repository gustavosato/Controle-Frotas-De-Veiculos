namespace ControleVeiculos.Domain.Entities.Users
{
    public class User
    {
        public int userID { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string cellNumber { get; set; }
        public string departamentoID { get; set; }
        public string description { get; set; }  
        public string firstAccess { get; set; }
        public bool isAdmin { get; set; }
        public bool isActive { get; set; }
        public string rg { get; set; }
        public string cpf { get; set; }
        public string dateOfBirth { get; set; }
        public string homeAddress { get; set; }
        public string cep { get; set; }
        public string district { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string homePhone { get; set; }
    }
}