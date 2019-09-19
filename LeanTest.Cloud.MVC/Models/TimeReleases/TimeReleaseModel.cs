using FluentValidation.Attributes;
using Lean.Test.Cloud.MVC.Validations.TimeRelease;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Lean.Test.Cloud.MVC.Models.TimeReleases
{
    [Validator(typeof(TimeReleaseValidator))]
    public class TimeReleaseModel
    {
        public TimeReleaseModel()
        {
            this.SearchLoadCustomers = new List<SelectListItem>();
            this.SearchLoadDemands = new List<SelectListItem>();
            this.SearchLoadCreateds = new List<SelectListItem>();
            this.LoadDemands = new List<SelectListItem>();
            this.LoadActivitys = new List<SelectListItem>();
            this.LoadCollaborators = new List<SelectListItem>();
        }

        [Key]
        public int TimeReleaseID { get; set; }

        //filter
        [DisplayName("Data de Início")]
        public string SearchStartDate { get; set; }

        [DisplayName("Data de Término")]
        public string SearchEndDate{ get; set; }

        //report
        [DisplayName("Data de Início")]
        public string SearchStartDateReport { get; set; }

        [DisplayName("Data de Término")]
        public string SearchEndDateReport { get; set; }

        [DisplayName("Demanda")]
        public string SearchDemandID { get; set; }
        public IList<SelectListItem> SearchLoadDemands { get; set; }

        [DisplayName("Empresa")]
        public string SearchCustomerID { get; set; }
        public IList<SelectListItem> SearchLoadCustomers { get; set; }

        [DisplayName("Atividade")]
        public string SearchActivityID { get; set; }
        public IList<SelectListItem> SearchLoadActivitys { get; set; }

        [DisplayName("Criado por")]
        public string SearchCreatedByID { get; set; }
        public IList<SelectListItem> SearchLoadCreateds { get; set; }

        [DisplayName("Pendentes de Aprovação")]
        public bool SearchIsApproved { get; set; }


        //crud
        [DisplayName("Data do registro ")]
        public string RegisterDate { get; set; }

        [DisplayName("Início do Trabalho")]
        public string StartWork { get; set; }

        [DisplayName("Término do Trabalho")]
        public string EndWork { get; set; }

        [DisplayName("Demanda")]
        public string DemandID { get; set; }
        public IList<SelectListItem> LoadDemands { get; set; }

        [DisplayName("Status da Aprovação")]
        public bool IsApproved { get; set; }

        [DisplayName("Atividade")]
        public string ActivityID { get; set; }
        public IList<SelectListItem> LoadActivitys{ get; set; }

        [DisplayName("Aprovado por")]
        public string ApprovedByID { get; set; }
        public IList<SelectListItem> LoadApproveds { get; set; }

        [DisplayName("Data da aprovação")]
        public string ApprovedDate { get; set; }

        [DisplayName("Descrição")]
        public string Description { get; set; }

        [DisplayName("Justificativas")]
        public string ReasonChange { get; set; }

        [DisplayName("Criado por")]
        public string CreatedByID { get; set; }

        [DisplayName("Colaborador")]
        public string CollaboratorID { get; set; }
        public IList<SelectListItem> LoadCollaborators { get; set; }

        [DisplayName("Data de criação")]
        public string CreationDate { get; set; }

        [DisplayName("Modificado por")]
        public string ModifiedByID { get; set; }

        [DisplayName("Data da última modificação")]
        public string LastModifiedDate { get; set; }

        [DisplayName("Horas")]
        public string TotalTime { get; set; }

        [DisplayName("Dia")]
        public string DayTotal { get; set; }

        [DisplayName("Mês")]
        public string MounthTotal { get; set; }

        [DisplayName("Empresa")]
        public string CustomerID { get; set; }

    }
}