namespace ControleVeiculos.Domain.Command.Seguros
{
    public class MaintenanceSeguroCommand
    {
        public int SeguroID { get; set; }
        public string Apolice { get; set; }
        public string Seguradora { get; set; }
        public string Franquia { get; set; }
        public string TipoSeguro { get; set; }
        public string DataContratacao { get; set; }
        public string Vigencia { get; set; }
        public string FimContratacao { get; set; }
        public string Renovacao { get; set; }
        public string TelefoneSeguradora { get; set; }
        public string PeriodoCarencia { get; set; }
        public string Indenizacao { get; set; }
        public string SinistroID { get; set; }
        public string VeiculoID { get; set; }

    }
}
