namespace ControleVeiculos.Domain.Command.Users
{
    public class FilterUserCommand
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string DepartamentoID { get; set; }
        public string IsAdmin { get; set; }
        public string IsActive { get; set; }

    }
}
