using FluentValidation.Attributes;
using ControleVeiculos.MVC.Validations.Financas;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ControleVeiculos.MVC.Models.Financas
{
    [Validator(typeof(FinancaValidator))]
    public class FinancaModel
    {
        public FinancaModel()
        {
            this.SearchLoadValorCarro = new List<SelectListItem>();
            this.SearchLoadValorSeguro = new List<SelectListItem>();
            this.SearchLoadValorAgua = new List<SelectListItem>();
            this.SearchLoadValorLuz = new List<SelectListItem>();

            this.LoadValorCarro = new List<SelectListItem>();
            this.LoadValorSeguro = new List<SelectListItem>();
            this.LoadValorAgua = new List<SelectListItem>();
            this.LoadValorLuz = new List<SelectListItem>();

        }

        //filter
        [DisplayName("Valor do Carro")]
        public string SearchValorCarro { get; set; }
        public IList<SelectListItem> SearchLoadValorCarro { get; set; }
        
        [DisplayName("Valor do Seguro")]
        public string SearchValorSeguro { get; set; }
        public IList<SelectListItem> SearchLoadValorSeguro { get; set; }

        [DisplayName("Conta de Água")]
        public string SearchValorAgua { get; set; }
        public IList<SelectListItem> SearchLoadValorAgua { get; set; }

        [DisplayName("Conta de Luz")]
        public string SearchValorLuz { get; set; }
        public IList<SelectListItem> SearchLoadValorLuz { get; set; }

        //crud
        [Key]
        public int FinancaID { get; set; }

        [DisplayName("Valor do Carro")]
        public string ValorCarro { get; set; }
        public IList<SelectListItem> LoadValorCarro { get; set; }

        [DisplayName("Valor do Seguro")]
        public string ValorSeguro { get; set; }
        public IList<SelectListItem> LoadValorSeguro { get; set; }

        [DisplayName("Conta de Água")]
        public string ValorAgua { get; set; }
        public IList<SelectListItem> LoadValorAgua { get; set; }

        [DisplayName("Conta de Luz")]
        public string ValorLuz { get; set; }
        public IList<SelectListItem> LoadValorLuz { get; set; }

        [DisplayName("Conta de Internet")]
        public string ValorInternet { get; set; }

        [DisplayName("Valor das Manutenções")]
        public string ValorManutencao { get; set; }

        [DisplayName("Salários")]
        public string Salarios { get; set; }

        [DisplayName("Gastos Extras")]
        public string GastosExtras { get; set; }

    }
}