using FluentValidation.Attributes;
using ControleVeiculos.MVC.Validations.Sinistros;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ControleVeiculos.MVC.Models.Sinistros
{
    [Validator(typeof(SinistroValidator))]
    public class SinistroModel
    {
        public SinistroModel()
        {
            this.SearchLoadApolice = new List<SelectListItem>();
            this.SearchLoadFranquia = new List<SelectListItem>();
            this.SearchLoadTipoSinistro = new List<SelectListItem>();

            this.LoadApolice = new List<SelectListItem>();
            this.LoadFranquia = new List<SelectListItem>();
            this.LoadTipoSinistro = new List<SelectListItem>();

        }

        //filter
        [DisplayName("Apólice")]
        public string SearchApolice { get; set; }
        public IList<SelectListItem> SearchLoadApolice { get; set; }
        
        [DisplayName("Franquia")]
        public string SearchFranquia { get; set; }
        public IList<SelectListItem> SearchLoadFranquia { get; set; }

        [DisplayName("Tipo de Sinistro")]
        public string SearchTipoSinistro { get; set; }
        public IList<SelectListItem> SearchLoadTipoSinistro { get; set; }
        
        //crud
        [Key]
        public int SinistroID { get; set; }

        [DisplayName("Apólice")]
        public string Apolice { get; set; }
        public IList<SelectListItem> LoadApolice { get; set; }

        [DisplayName("Franquia")]
        public string Franquia { get; set; }
        public IList<SelectListItem> LoadFranquia { get; set; }

        [DisplayName("Tipo de Sinistro")]
        public string TipoSinistro { get; set; }
        public IList<SelectListItem> LoadTipoSinistro { get; set; }

        
    }
}