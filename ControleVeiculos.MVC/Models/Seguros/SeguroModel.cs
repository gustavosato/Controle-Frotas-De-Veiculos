using FluentValidation.Attributes;
using ControleVeiculos.MVC.Validations.Seguros;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ControleVeiculos.MVC.Models.Seguros
{
    [Validator(typeof(SeguroValidator))]
    public class SeguroModel
    {
        public SeguroModel()
        {
            this.SearchLoadApolice = new List<SelectListItem>();
            this.SearchLoadVeiculo = new List<SelectListItem>();
            this.SearchLoadSeguradora = new List<SelectListItem>();
            this.SearchLoadFranquia = new List<SelectListItem>();
            this.SearchLoadTipoSeguro = new List<SelectListItem>();
            this.SearchLoadDataContratacao = new List<SelectListItem>();
            this.SearchLoadVigencia = new List<SelectListItem>();
            this.SearchLoadFimContratacao = new List<SelectListItem>();


            this.LoadApolice = new List<SelectListItem>();
            this.LoadApolice = new List<SelectListItem>();
            this.LoadSeguradora = new List<SelectListItem>();
            this.LoadFranquia = new List<SelectListItem>();
            this.LoadTipoSeguro = new List<SelectListItem>();
            this.LoadDataContratacao = new List<SelectListItem>();
            this.LoadVigencia = new List<SelectListItem>();
            this.LoadFimContratacao = new List<SelectListItem>();

        }

        //filter
        [DisplayName("Apolice")]
        public string SearchApolice { get; set; }
        public IList<SelectListItem> SearchLoadApolice { get; set; }

        [DisplayName("Veículo")]
        public string SearchVeiculo { get; set; }
        public IList<SelectListItem> SearchLoadVeiculo { get; set; }

        [DisplayName("Seguradora")]
        public string SearchSeguradora { get; set; }
        public IList<SelectListItem> SearchLoadSeguradora { get; set; }

        [DisplayName("Franquia")]
        public string SearchFranquia { get; set; }
        public IList<SelectListItem> SearchLoadFranquia { get; set; }

        [DisplayName("Tipo de Seguro")]
        public string SearchTipoSeguro { get; set; }
        public IList<SelectListItem> SearchLoadTipoSeguro { get; set; }

        [DisplayName("Data da Contratação")]
        public string SearchDataContratacao { get; set; }
        public IList<SelectListItem> SearchLoadDataContratacao { get; set; }

        [DisplayName("Vigencia")]
        public string SearchVigencia { get; set; }
        public IList<SelectListItem> SearchLoadVigencia { get; set; }

        [DisplayName("Término do Contrato")]
        public string SearchFimContratacao { get; set; }
        public IList<SelectListItem> SearchLoadFimContratacao { get; set; }


        //crud
        [Key]
        public int SeguroID { get; set; }

        [DisplayName("Veículo")]
        public string VeiculoID { get; set; }
        public IList<SelectListItem> LoadVeiculo { get; set; }

        [DisplayName("Apolice")]
        public string Apolice { get; set; }
        public IList<SelectListItem> LoadApolice { get; set; }

        [DisplayName("Seguradora")]
        public string Seguradora { get; set; }
        public IList<SelectListItem> LoadSeguradora { get; set; }

        [DisplayName("Franquia")]
        public string Franquia { get; set; }
        public IList<SelectListItem> LoadFranquia { get; set; }

        [DisplayName("Tipo de Seguro")]
        public string TipoSeguro { get; set; }
        public IList<SelectListItem> LoadTipoSeguro { get; set; }

        [DisplayName("Data da Contratação")]
        public string DataContratacao { get; set; }
        public IList<SelectListItem> LoadDataContratacao { get; set; }

        [DisplayName("Vigencia")]
        public string Vigencia { get; set; }
        public IList<SelectListItem> LoadVigencia { get; set; }

        [DisplayName("Término do Contrato")]
        public string FimContratacao { get; set; }
        public IList<SelectListItem> LoadFimContratacao { get; set; }

        [DisplayName("Renovaçãp")]
        public string Renovacao { get; set; }

        [DisplayName("Tel. Seguradora")]
        public string TelefoneSeguradora { get; set; }

        [DisplayName("Período de Carência")]
        public string PeriodoCarencia { get; set; }

        [DisplayName("Indenização")]
        public string Indenizacao { get; set; }
    }
}