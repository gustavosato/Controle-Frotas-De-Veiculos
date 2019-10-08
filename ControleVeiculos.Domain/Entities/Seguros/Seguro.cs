namespace ControleVeiculos.Domain.Entities.Seguros
{
    public class Seguro
    {
        public int seguroID { get; set; }
        public string apolice { get; set; }
        public string seguradora { get; set; }
        public string franquia { get; set; }
        public string tipoSeguro { get; set; }
        public string dataContratacao { get; set; }
        public string vigencia { get; set; }
        public string fimContratacao { get; set; }
        public string renovacao { get; set; }
        public string telefoneSeguradora { get; set; }
        public string periodoCarencia { get; set; }
        public string indenizacao { get; set; }
        public string sinistroID { get; set; }
        public string veiculoID { get; set; }
    }
}
