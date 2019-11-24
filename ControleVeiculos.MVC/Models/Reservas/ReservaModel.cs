using FluentValidation.Attributes;
using ControleVeiculos.MVC.Validations.Reservas;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ControleVeiculos.MVC.Models.Reservas
{
    [Validator(typeof(ReservaValidator))]
    public class ReservaModel
    {
        public ReservaModel()
        {
            this.SearchLoadDataReserva = new List<SelectListItem>();
            this.SearchLoadDestino = new List<SelectListItem>();
            this.SearchLoadFuncionario = new List<SelectListItem>();
            this.SearchLoadVeiculo = new List<SelectListItem>();

            this.LoadDataReserva = new List<SelectListItem>();
            this.LoadDestino = new List<SelectListItem>();
            this.LoadFuncionario = new List<SelectListItem>();
            this.LoadVeiculo = new List<SelectListItem>();

        }

        //filter
        [DisplayName("Data da Reserva")]
        public string SearchDataReserva { get; set; }
        public IList<SelectListItem> SearchLoadDataReserva { get; set; }
        
        [DisplayName("Destino")]
        public string SearchDestino { get; set; }
        public IList<SelectListItem> SearchLoadDestino { get; set; }

        [DisplayName("Funcionário")]
        public string SearchFuncionarioID { get; set; }
        public IList<SelectListItem> SearchLoadFuncionario { get; set; }

        [DisplayName("Veículo")]
        public string SearchVeiculoID { get; set; }
        public IList<SelectListItem> SearchLoadVeiculo { get; set; }

        //crud
        [Key]
        public int ReservaID { get; set; }

        [DisplayName("Data da Reserva")]
        public string DataReserva { get; set; }
        public IList<SelectListItem> LoadDataReserva { get; set; }

        [DisplayName("Finalidade")]
        public string Finalidade { get; set; }

        [DisplayName("Funcionário")]
        public string FuncionarioID { get; set; }
        public IList<SelectListItem> LoadFuncionario { get; set; }

        [DisplayName("Destino")]
        public string Destino { get; set; }
        public IList<SelectListItem> LoadDestino { get; set; }

        [DisplayName("Numero da CNH")]
        public string NumeroCnh { get; set; }

        [DisplayName("Veículo")]
        public string VeiculoID { get; set; }
        public IList<SelectListItem> LoadVeiculo { get; set; }
        
    }
}