namespace ControleVeiculos.Domain.Command.Emprestimos
{
    public class MaintenanceEmprestimoCommand
    {
        public int EmprestimoID { get; set; }
        public string KmInicial { get; set; }
        public string KmFinal { get; set; }
        public string DataSaida { get; set; }
        public string DataRetorno { get; set; }
        public string Destino { get; set; }
        public string FuncionarioID { get; set; }
        public string VeiculoID { get; set; }

    }
}
