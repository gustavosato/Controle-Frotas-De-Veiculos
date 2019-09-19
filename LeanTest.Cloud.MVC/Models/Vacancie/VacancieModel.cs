using FluentValidation.Attributes;
using Lean.Test.Cloud.MVC.Validations.Vacancies;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Lean.Test.Cloud.MVC.Models.Vacancies
{
    [Validator(typeof(VacancieValidator))]
    public class VacancieModel
    {
        public VacancieModel()
        {
            this.SearchLoadVacanciesType = new List<SelectListItem>();
            this.SearchLoadCustomer = new List<SelectListItem>();
            this.SearchLoadInternalApplicant = new List<SelectListItem>();
            this.SearchLoadExternalApplicant = new List<SelectListItem>();
            this.SearchLoadAssignTo = new List<SelectListItem>();
            this.SearchLoadContractType = new List<SelectListItem>();
            this.SearchLoadValidity = new List<SelectListItem>();
            this.SearchLoadStatus = new List<SelectListItem>();
            this.SearchLoadCreatedBy = new List<SelectListItem>();


            this.LoadVacanciesType = new List<SelectListItem>();
            this.LoadCustomer = new List<SelectListItem>();
            this.LoadInternalApplicant = new List<SelectListItem>();
            this.LoadExternalApplicant = new List<SelectListItem>();
            this.LoadAssignTo = new List<SelectListItem>();
            this.LoadContractType = new List<SelectListItem>();
            this.LoadValidity = new List<SelectListItem>();
            this.LoadStatus = new List<SelectListItem>();
            this.LoadCreatedBy = new List<SelectListItem>();
            this.LoadResumeSelected = new List<SelectListItem>();

        }

        //Filter

        [DisplayName("Sumário")]
        public string SearchSummary { get; set; }

        [DisplayName("Tipo da Vaga")]
        public string SearchVacanciesTypeID { get; set; }
        public IList<SelectListItem> SearchLoadVacanciesType { get; set; }

        [DisplayName("Empresa")]
        public string SearchCustomerID { get; set; }
        public IList<SelectListItem> SearchLoadCustomer { get; set; }

        [DisplayName("Solicitante Interno")]
        public string SearchInternalApplicantID { get; set; }
        public IList<SelectListItem> SearchLoadInternalApplicant { get; set; }

        [DisplayName("Solicitante Externo")]
        public string SearchExternalApplicantID { get; set; }
        public IList<SelectListItem> SearchLoadExternalApplicant { get; set; }

        [DisplayName("Associado á")]
        public string SearchAssignToID { get; set; }
        public IList<SelectListItem> SearchLoadAssignTo { get; set; }

        [DisplayName("Tipo de Contratação")]
        public string SearchContractTypeID { get; set; }
        public IList<SelectListItem> SearchLoadContractType { get; set; }

        [DisplayName("Vigência")]
        public string SearchValidityID { get; set; }
        public IList<SelectListItem> SearchLoadValidity { get; set; }

        [DisplayName("Status")]
        public string SearchStatusID { get; set; }
        public IList<SelectListItem> SearchLoadStatus { get; set; }
        
        [DisplayName("Local de Trabalho")]
        public string SearchWorkPlace { get; set; }

        [DisplayName("Criado por")]
        public string SearchCreatedByID { get; set; }
        public IList<SelectListItem> SearchLoadCreatedBy { get; set; }

        //Crud
        [Key]
        public int VacancieID { get; set; }

        [DisplayName("Sumário")]
        public string Summary { get; set; }

        [DisplayName("Tipo da Vaga")]
        public string VacanciesTypeID { get; set; }
        public IList<SelectListItem> LoadVacanciesType { get; set; }

        [DisplayName("Descrição")]
        public string Description { get; set; }

        [DisplayName("Empresa")]
        public string CustomerID { get; set; }
        public IList<SelectListItem> LoadCustomer { get; set; }

        [DisplayName("Solicitante Interno")]
        public string InternalApplicantID { get; set; }
        public IList<SelectListItem> LoadInternalApplicant { get; set; }

        [DisplayName("Solicitante Externo")]
        public string ExternalApplicantID { get; set; }
        public IList<SelectListItem> LoadExternalApplicant { get; set; }

        [DisplayName("Associado á")]
        public string AssignToID { get; set; }
        public IList<SelectListItem> LoadAssignTo { get; set; }

        [DisplayName("Tipo de Contratação")]
        public string ContractTypeID { get; set; }
        public IList<SelectListItem> LoadContractType { get; set; }

        [DisplayName("Vigência")]
        public string ValidityID { get; set; }
        public IList<SelectListItem> LoadValidity { get; set; }

        [DisplayName("Status")]
        public string StatusID { get; set; }
        public IList<SelectListItem> LoadStatus { get; set; }

        [DisplayName("Data da Abertura")]
        public string OpeningDate { get; set; }

        [DisplayName("Limite para Fechamento")]
        public string ClosingDate { get; set; }

        [DisplayName("Início Previsto")]
        public string ExpectedStartDate { get; set; }

        [DisplayName("Valor Máximo")]
        public string MaximumValue { get; set; }

        [DisplayName("Valor Fechado")]
        public string ClosedValue { get; set; }

        [DisplayName("Local de Trabalho")]
        public string WorkPlace { get; set; }

        [DisplayName("Currículo Selecionado")]
        public string ResumeSelectedID { get; set; }
        public IList<SelectListItem> LoadResumeSelected { get; set; }

        [DisplayName("Criado por")]
        public string CreatedByID { get; set; }
        public IList<SelectListItem> LoadCreatedBy { get; set; }

        [DisplayName("Data de Criação")]
        public string CreationDate { get; set; }

        [DisplayName("Modificado por")]
        public string ModifiedByID { get; set; }
        public IList<SelectListItem> LoadModifiedBy { get; set; }

        [DisplayName("Última Modificação")]
        public string LastModifiedDate { get; set; }

        public string ResumeID { get; set; }

    }
}