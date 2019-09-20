using FluentValidation.Attributes;
using ControleVeiculos.MVC.Validations.MovimentEmployee;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ControleVeiculos.MVC.Models.MovimentEmployees
{
    [Validator(typeof(MovimentEmployeeValidator))]
    public class MovimentEmployeeModel
    {
        public MovimentEmployeeModel()
        {

            this.SearchLoadEmployees = new List<SelectListItem>();
            this.SearchLoadStatus = new List<SelectListItem>();
            this.SearchLoadMovimentEmployeeTypes = new List<SelectListItem>();

            this.LoadEmployees = new List<SelectListItem>();
            this.LoadStatus = new List<SelectListItem>();
            this.LoadMovimentEmployeeTypes = new List<SelectListItem>();

        }

        //filter
        [DisplayName("Funcionário")]
        public string SearchEmployeeID{ get; set; }
        public IList<SelectListItem> SearchLoadEmployees { get; set; }

        [DisplayName("Status")]
        public string SearchStatusID { get; set; }
        public IList<SelectListItem> SearchLoadStatus { get; set; }

        [DisplayName("Tipo de Movimento")]
        public string SearchMovimentEmployeeTypeID { get; set; }
        public IList<SelectListItem> SearchLoadMovimentEmployeeTypes { get; set; }

        [DisplayName("Data de Início")]
        public string SearchStartDate { get; set; }

        [DisplayName("Data de Término")]
        public string SearchEndDate { get; set; }

        //crud
        [Key]
        public int MovimentEmployeeID { get; set; }

        [DisplayName("Colaborador")]
        public string EmployeeID { get; set; }
        public IList<SelectListItem> LoadEmployees { get; set; }

        [DisplayName("Início")]
        public string StartDate { get; set; }

        [DisplayName("Término")]
        public string EndDate { get; set; }

        [DisplayName("Status")]
        public string StatusID { get; set; }
        public IList<SelectListItem> LoadStatus { get; set; }

        [DisplayName("Tipo de Movimento")]
        public string MovimentEmployeeTypeID { get; set; }
        public IList<SelectListItem> LoadMovimentEmployeeTypes { get; set; }

        [DisplayName("Data de Aprovação")]
        public string ApprovedDate { get; set; }
        
        [DisplayName("Aprovado por ")]
        public string ApprovedByID { get; set; }
        public IList<SelectListItem> LoadApprovedS { get; set; }

        [DisplayName("Descrição")]
        public string Description { get; set; }


        [DisplayName("Data de Criação")]
        public string CreationDate { get; set; }

        [DisplayName("Criado Por")]
        public string CreatedByID { get; set; }

        [DisplayName("Modificado por")]
        public string ModifiedByID { get; set; }
        
        [DisplayName("Data da última modificação")]
        public string LastModifiedDate { get; set; }


    }
}