namespace ControleVeiculos.Domain.Command.Departamentos
{
    public class MaintenanceDepartamentoCommand
    {
        public int DepartamentoID { get; set; }
        public string NomeDepartamento { get; set; }
        public string Descricao { get; set; }
        public string FuncionarioID { get; set; }
        
    }
}
