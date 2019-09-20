using FluentValidation.Attributes;
using ControleVeiculos.MVC.Validations.Supports;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ControleVeiculos.MVC.Models.Supports
{
    [Validator(typeof(SupportValidator))]
    public class SupportModel
    {
        public SupportModel()
        {
            this.SearchLoadCustomer = new List<SelectListItem>();
            this.SearchLoadSeverity = new List<SelectListItem>();
            this.SearchLoadPriority = new List<SelectListItem>();
            this.SearchLoadAssingTo = new List<SelectListItem>();
            this.SearchLoadType = new List<SelectListItem>();
            this.SearchLoadCreatedBy = new List<SelectListItem>();
            this.SearchLoadStatus = new List<SelectListItem>();

            this.LoadCustomer = new List<SelectListItem>();
            this.LoadSeverity = new List<SelectListItem>();
            this.LoadPriority = new List<SelectListItem>();
            this.LoadAssingTo = new List<SelectListItem>();
            this.LoadType = new List<SelectListItem>();
            this.LoadCreatedBy = new List<SelectListItem>();
            this.LoadStatus = new List<SelectListItem>();

        }

        //Filter

        [DisplayName("Sumário")]
        public string SearchSummary { get; set; }
              
        [DisplayName("Empresa")]
        public string SearchCustomerID { get; set; }
        public IList<SelectListItem> SearchLoadCustomer { get; set; }

        [DisplayName("Status")]
        public string SearchStatusID { get; set; }
        public IList<SelectListItem> SearchLoadStatus { get; set; }

        [DisplayName("Severidade")]
        public string SearchSeverityID { get; set; }
        public IList<SelectListItem> SearchLoadSeverity { get; set; }

        [DisplayName("Prioridade")]
        public string SearchPriorityID { get; set; }
        public IList<SelectListItem> SearchLoadPriority { get; set; }

        [DisplayName("Tipo")]
        public string SearchTypeID { get; set; }
        public IList<SelectListItem> SearchLoadType { get; set; }

        [DisplayName("Associado")]
        public string SearchAssingToID { get; set; }
        public IList<SelectListItem> SearchLoadAssingTo { get; set; }

        [DisplayName("Criado por")]
        public string SearchCreatedByID { get; set; }
        public IList<SelectListItem> SearchLoadCreatedBy { get; set; }

        //Crud
        [Key]
        public int SupportID { get; set; }

        [DisplayName("Sumário")]
        public string Summary { get; set; }

        [DisplayName("Descrição")]
        public string Description { get; set; }

        [DisplayName("Severidade")]
        public string SeverityID { get; set; }
        public IList<SelectListItem> LoadSeverity { get; set; }

        [DisplayName("Status")]
        public string StatusID { get; set; }
        public IList<SelectListItem> LoadStatus { get; set; }

        [DisplayName("Prioridade")]
        public string PriorityID { get; set; }
        public IList<SelectListItem> LoadPriority { get; set; }

        [DisplayName("Tipo")]
        public string TypeID { get; set; }
        public IList<SelectListItem> LoadType { get; set; }

        [DisplayName("Associar")]
        public string AssingToID { get; set; }
        public IList<SelectListItem> LoadAssingTo { get; set; }

        [DisplayName("Data de Resolução")]
        public string ResolutionDate { get; set; }

        [DisplayName("Empresa")]
        public string CustomerID { get; set; }
        public IList<SelectListItem> LoadCustomer { get; set; }

        [DisplayName("Criado por")]
        public string CreatedByID { get; set; }
        public IList<SelectListItem> LoadCreatedBy { get; set; }

        [DisplayName("Modificado")]
        public string ModifiedByID { get; set; }
        public IList<SelectListItem> LoadModifiedBy { get; set; }

        [DisplayName("Data de Criação")]
        public string CreationDate { get; set; }

        [DisplayName("Última Modificação")]
        public string LastModifiedDate { get; set; }
    }
}