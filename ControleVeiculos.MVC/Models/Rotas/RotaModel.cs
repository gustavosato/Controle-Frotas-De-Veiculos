using FluentValidation.Attributes;
using ControleVeiculos.MVC.Validations.Rotas;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ControleVeiculos.MVC.Models.Rotas
{
    [Validator(typeof(RotaValidator))]
    public class RotaModel
    {
        public RotaModel()
        {
            this.SearchLoadCidade = new List<SelectListItem>();
            this.SearchLoadEstado = new List<SelectListItem>();
            this.SearchLoadDataIda = new List<SelectListItem>();
            this.SearchLoadDataVolta = new List<SelectListItem>();
            this.SearchLoadPedagio = new List<SelectListItem>();

            this.LoadCidade = new List<SelectListItem>();
            this.LoadEstado = new List<SelectListItem>();
            this.LoadDataIda = new List<SelectListItem>();
            this.LoadDataVolta = new List<SelectListItem>();
            this.LoadPedagio = new List<SelectListItem>();

        }

        //filter
        [DisplayName("Cidade")]
        public string SearchCidade { get; set; }
        public IList<SelectListItem> SearchLoadCidade { get; set; }
        
        [DisplayName("Estado")]
        public string SearchEstado { get; set; }
        public IList<SelectListItem> SearchLoadEstado { get; set; }

        [DisplayName("Data de Saída")]
        public string SearchDataIda { get; set; }
        public IList<SelectListItem> SearchLoadDataIda { get; set; }

        [DisplayName("Data de Retorno")]
        public string SearchDataVolta { get; set; }
        public IList<SelectListItem> SearchLoadDataVolta { get; set; }

        [DisplayName("Pedágio")]
        public string SearchPedagio { get; set; }
        public IList<SelectListItem> SearchLoadPedagio { get; set; }


        //crud
        [Key]
        public int RotaID { get; set; }

        [DisplayName("Cidade")]
        public string Cidade { get; set; }
        public IList<SelectListItem> LoadCidade { get; set; }

        [DisplayName("Estado")]
        public string Estado { get; set; }
        public IList<SelectListItem> LoadEstado { get; set; }

        [DisplayName("Distancia")]
        public string Distancia { get; set; }

        [DisplayName("Pedágio")]
        public bool Pedagio { get; set; }
        public IList<SelectListItem> LoadPedagio { get; set; }

        [DisplayName("Data de Ida")]
        public string DataIda { get; set; }
        public IList<SelectListItem> LoadDataIda { get; set; }

        [DisplayName("Data de Retorno")]
        public string DataVolta { get; set; }
        public IList<SelectListItem> LoadDataVolta { get; set; }

    }
}