using FluentValidation.Attributes;
using ControleVeiculos.MVC.Validations.Funcionarios;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ControleVeiculos.MVC.Models.Funcionarios
{
    [Validator(typeof(FuncionarioValidator))]
    public class FuncionarioModel
    {
        public FuncionarioModel()
        {
            this.SearchLoadNomeFuncionario = new List<SelectListItem>();
            this.SearchLoadCPF = new List<SelectListItem>();
            this.SearchLoadFuncao = new List<SelectListItem>();
            this.SearchLoadSetor = new List<SelectListItem>();

            this.LoadNomeFuncionario = new List<SelectListItem>();
            this.LoadCPF = new List<SelectListItem>();
            this.LoadFuncao = new List<SelectListItem>();
            this.LoadSetor = new List<SelectListItem>();

        }

        //filter
        [DisplayName("Nome do Funcionário")]
        public string SearchNomeFuncionario { get; set; }
        public IList<SelectListItem> SearchLoadNomeFuncionario { get; set; }
        
        [DisplayName("CPF")]
        public string SearchCPF { get; set; }
        public IList<SelectListItem> SearchLoadCPF { get; set; }

        [DisplayName("Função")]
        public string SearchFuncao { get; set; }
        public IList<SelectListItem> SearchLoadFuncao { get; set; }

        [DisplayName("Setor")]
        public string SearchSetor { get; set; }
        public IList<SelectListItem> SearchLoadSetor { get; set; }

        //crud
        [Key]
        public int FuncionarioID { get; set; }

        [DisplayName("Nome do Funcionario")]
        public string NomeFuncionario { get; set; }
        public IList<SelectListItem> LoadNomeFuncionario { get; set; }

        [DisplayName("Endereço")]
        public string Endereco { get; set; }

        [DisplayName("CPF")]
        public string CPF { get; set; }
        public IList<SelectListItem> LoadCPF { get; set; }

        [DisplayName("Função")]
        public string Funcao { get; set; }
        public IList<SelectListItem> LoadFuncao { get; set; }

        [DisplayName("Setor")]
        public string Setor { get; set; }
        public IList<SelectListItem> LoadSetor { get; set; }

        [DisplayName("Telefone")]
        public string Telefone { get; set; }

        [DisplayName("Número da CNH")]
        public string NumeroCnh { get; set; }

    }
}