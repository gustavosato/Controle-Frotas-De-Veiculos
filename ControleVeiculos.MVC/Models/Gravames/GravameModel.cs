//using FluentValidation.Attributes;
//using ControleVeiculos.MVC.Validations.Gravames;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.ComponentModel.DataAnnotations;
//using System.Web.Mvc;

//namespace ControleVeiculos.MVC.Models.Gravames
//{
//    [Validator(typeof(GravameValidator))]
//    public class GravameModel
//    {
//        public GravameModel()
//        {
//            this.UFsPlaca = new List<SelectListItem>();
//        }

//        public int Codigo { get; set; }
//        public string Chassi { get; set; }
//        [DisplayName("Placa")]
//        public string Placa { get; set; }
//        [DisplayName("Renavam")]
//        public string Renavam { get; set; }
//        [DisplayName("Ano modelo")]
//        public string AnoModelo { get; set; }
//        [DisplayName("Ano fabricação")]        
//        public string AnoFabricacao { get; set; }
//        [DisplayName("Núm. Contrato")]
//        public string NumContrato { get; set; }
//        [DisplayName("Núm. Gravame")]
//        public string NumGravame { get; set; }
//        [DisplayName("Código Agente")]
//        public string CodAgente { get; set; }
//        [DisplayName("CNPJ Agente")]
//        public string CnpjAgente { get; set; }
//        [DisplayName("Nome Financiado")]
//        public string NomeFinanciador { get; set; }
//        [DisplayName("UF Placa")]
//        public string UFPlaca { get; set; }
//        public IList<SelectListItem> UFsPlaca { get; set; }
//        public bool IsDisponivel { get; set; }
//        public bool SomenteLeitura { get; set; }
//    }
//}