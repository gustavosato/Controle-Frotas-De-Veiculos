using FluentValidation.Attributes;
using ControleVeiculos.MVC.Validations.Veiculos;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ControleVeiculos.MVC.Models.Veiculos
{
    [Validator(typeof(VeiculoValidator))]
    public class VeiculoModel
    {
        public VeiculoModel()
        {
            this.SearchLoadModelo = new List<SelectListItem>();
            this.SearchLoadStatus = new List<SelectListItem>();
            this.SearchLoadAno = new List<SelectListItem>();
            this.SearchLoadMotor = new List<SelectListItem>();

            this.LoadModelo = new List<SelectListItem>();
            this.LoadStatus = new List<SelectListItem>();
            this.LoadAno = new List<SelectListItem>();
            this.LoadMotor = new List<SelectListItem>();

        }

        //filter
        [DisplayName("Modelo")]
        public string SearchModelo { get; set; }
        public IList<SelectListItem> SearchLoadModelo { get; set; }
        
        [DisplayName("Status do Veículo")]
        public string SearchStatus { get; set; }
        public IList<SelectListItem> SearchLoadStatus { get; set; }

        [DisplayName("Ano do Veículo")]
        public string SearchAno { get; set; }
        public IList<SelectListItem> SearchLoadAno { get; set; }

        [DisplayName("Motor")]
        public string SearchMotor { get; set; }
        public IList<SelectListItem> SearchLoadMotor { get; set; }

        //crud
        [Key]
        public int VeiculoID { get; set; }

        [DisplayName("Modelo")]
        public string Modelo { get; set; }
        public IList<SelectListItem> LoadModelo { get; set; }

        [DisplayName("Cor")]
        public string Cor { get; set; }

        [DisplayName("Placa")]
        public string Placa { get; set; }

        [DisplayName("Status do Veículo")]
        public string Status { get; set; }
        public IList<SelectListItem> LoadStatus { get; set; }

        [DisplayName("Ano do Veículo")]
        public string Ano { get; set; }
        public IList<SelectListItem> LoadAno { get; set; }

        [DisplayName("Manutenção")]
        public string ManutencaoID { get; set; }

        [DisplayName("Abastecimento")]
        public string AbastecimentoID { get; set; }

        [DisplayName("Número do Chassi")]
        public string NumeroChassi { get; set; }

        [DisplayName("Motor")]
        public string Motor { get; set; }
        public IList<SelectListItem> LoadMotor { get; set; }

    }
}