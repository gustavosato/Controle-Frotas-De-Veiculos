namespace ControleVeiculos.Domain.Command.Financas
{
    public class MaintenanceFinancaCommand
    {
        public int FinancaID { get; set; }
        public string ValorCarro { get; set; }
        public string ValorSeguro { get; set; }
        public string ValorAgua { get; set; }
        public string ValorLuz { get; set; }
        public string ValorInternet { get; set; }
        public string ValorManutencao { get; set; }
        public string Salarios { get; set; }
        public string GastosExtras { get; set; }
        
    }
}
