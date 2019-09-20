using FluentValidation.Attributes;
using ControleVeiculos.MVC.Validations.Tasks;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ControleVeiculos.MVC.Models.Tasks
{
    [Validator(typeof(TaskValidator))]
    public class TaskModel
    {
        public TaskModel()
        {
            this.SearchLoadAssignTo = new List<SelectListItem>();
            this.SearchLoadDemand = new List<SelectListItem>();
            this.SearchLoadCustomer = new List<SelectListItem>();
            this.SearchLoadStatus = new List<SelectListItem>();
            this.SearchLoadCreatedBy = new List<SelectListItem>();

            this.LoadAssignTo = new List<SelectListItem>();
            this.LoadDemand = new List<SelectListItem>();
            this.LoadCustomer = new List<SelectListItem>();
            this.LoadStatus = new List<SelectListItem>();
            this.LoadCreatedBy = new List<SelectListItem>();

        }

        //Filter

        [DisplayName("Sumário")]
        public string SearchSummary { get; set; }

        [DisplayName("Designado a")]
        public string SearchAssignToID { get; set; }
        public IList<SelectListItem> SearchLoadAssignTo { get; set; }

        [DisplayName("Cliente")]
        public string SearchCustomerID { get; set; }
        public IList<SelectListItem> SearchLoadCustomer { get; set; }

        [DisplayName("Demanda")]
        public string SearchDemandID { get; set; }
        public IList<SelectListItem> SearchLoadDemand { get; set; }


        [DisplayName("Status")]
        public string SearchStatusID { get; set; }
        public IList<SelectListItem> SearchLoadStatus { get; set; }

        [DisplayName("Criado por")]
        public string SearchCreatedByID { get; set; }
        public IList<SelectListItem> SearchLoadCreatedBy { get; set; }



        //Crud
        [Key]
        public int TaskID { get; set; }
               
        [DisplayName("Sumário")]
        public string Summary { get; set; }

        [DisplayName("Descrição")]
        public string Description { get; set; }

        [DisplayName("Designado a")]
        public string AssignToID { get; set; }
        public IList<SelectListItem> LoadAssignTo { get; set; }

        [DisplayName("Demanda")]
        public string DemandID { get; set; }
        public IList<SelectListItem> LoadDemand { get; set; }

        [DisplayName("Cliente")]
        public string CustomerID { get; set; }
        public IList<SelectListItem> LoadCustomer { get; set; }

        [DisplayName("Status")]
        public string StatusID { get; set; }
        public IList<SelectListItem> LoadStatus { get; set; }
                
        [DisplayName("Data Alvo")]
        public string TargetDate { get; set; }
       
        [DisplayName("Data de conclusão")]
        public string ClosingDate { get; set; }
               
        [DisplayName("Criado")]
        public string CreatedByID { get; set; }
        public IList<SelectListItem> LoadCreatedBy { get; set; }

        [DisplayName("Data de Criação")]
        public string CreationDate { get; set; }

        [DisplayName("Modificado")]
        public string ModifiedByID { get; set; }
        public IList<SelectListItem> LoadModifiedBy { get; set; }

        [DisplayName("Última Modificação")]
        public string LastModifiedDate { get; set; }
    }
}
