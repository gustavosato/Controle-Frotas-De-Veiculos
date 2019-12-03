using FluentValidation.Attributes;
using ControleVeiculos.MVC.Validations.Multas;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ControleVeiculos.MVC.Models.Multas
{
    [Validator(typeof(MultaValidator))]
    public class MultaModel
    {
        public MultaModel()
        {
            this.SearchLoadVeiculo = new List<SelectListItem>();
            this.SearchLoadFuncionario = new List<SelectListItem>();
            
            this.LoadVeiculo = new List<SelectListItem>();
            this.LoadFuncionario = new List<SelectListItem>();
            
        }

        //filter
        [DisplayName("Veículo")]
        public string SearchVeiculoID { get; set; }
        public IList<SelectListItem> SearchLoadVeiculo { get; set; }
        
        [DisplayName("Funcionário")]
        public string SearchFuncionarioID { get; set; }
        public IList<SelectListItem> SearchLoadFuncionario { get; set; }

        //crud
        [Key]
        public int MultaID { get; set; }

        [DisplayName("Veículo")]
        public string VeiculoID { get; set; }
        public IList<SelectListItem> LoadVeiculo { get; set; }

        [DisplayName("Funcionário")]
        public string FuncionarioID { get; set; }
        public IList<SelectListItem> LoadFuncionario { get; set; }
        
    }
}