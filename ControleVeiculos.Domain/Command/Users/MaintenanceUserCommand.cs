namespace ControleVeiculos.Domain.Command.Users
{
    public class MaintenanceUserCommand
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string CellNumber { get; set; }  
        public string DepartamentoID { get; set; }
        public string Description { get; set; }
        public string FirstAccess { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsActive { get; set; }
        public string RG { get; set; }
        public string CPF { get; set; }
        public string DateOfBirth { get; set; }
        public string HomeAddress { get; set; }
        public string CEP { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string HomePhone { get; set; }
    }
}
