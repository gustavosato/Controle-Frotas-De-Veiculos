using FluentValidation.Attributes;
using Lean.Test.Cloud.MVC.Validations.Resumes;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Lean.Test.Cloud.MVC.Models.Resumes
{
    [Validator(typeof(ResumeValidator))]
    public class ResumeModel
    {
        public ResumeModel()
        {
            //this.SearchLoadSummary = new List<SelectListItem>();
            this.SearchLoadFunction = new List<SelectListItem>();
            this.SearchLoadFunctionLevel = new List<SelectListItem>();
            this.SearchLoadStatusRh = new List<SelectListItem>();
            this.SearchLoadStatusManager = new List<SelectListItem>();
            this.SearchLoadStatusClient = new List<SelectListItem>();
            this.SearchLoadContractType = new List<SelectListItem>();



            //this.LoadSummary = new List<SelectListItem>();
            this.LoadGender = new List<SelectListItem>();
            this.LoadFunction = new List<SelectListItem>();
            this.LoadTimeExperience = new List<SelectListItem>();
            this.LoadFunctionLevel = new List<SelectListItem>();
            this.LoadStatusRh = new List<SelectListItem>();
            this.LoadStatusManager = new List<SelectListItem>();
            this.LoadStatusClient = new List<SelectListItem>();
            this.LoadContractType = new List<SelectListItem>();
            this.LoadMaritalStatus = new List<SelectListItem>();

        }

        //Filter

        [DisplayName("Candidato")]
        public string SearchSummary { get; set; }
        //public IList<SelectListItem> SearchLoadSummary { get; set; }

        [DisplayName("Função")]
        public string SearchFunctionID { get; set; }
        public IList<SelectListItem> SearchLoadFunction { get; set; }

        [DisplayName("Tempo de Experiência")]
        public string SearchTimeExperience { get; set; }
       
        [DisplayName("Nível")]
        public string SearchFunctionLevelID { get; set; }
        public IList<SelectListItem> SearchLoadFunctionLevel { get; set; }

        [DisplayName("Status do RH")]
        public string SearchStatusRhID { get; set; }
        public IList<SelectListItem> SearchLoadStatusRh { get; set; }

        [DisplayName("Status do Gestor")]
        public string SearchStatusManagerID { get; set; }
        public IList<SelectListItem> SearchLoadStatusManager { get; set; }

        [DisplayName("Status do Cliente")]
        public string SearchStatusClientID { get; set; }
        public IList<SelectListItem> SearchLoadStatusClient { get; set; }

        [DisplayName("Tipo de Contrato")]
        public string SearchContractTypeID { get; set; }
        public IList<SelectListItem> SearchLoadContractType { get; set; }


        //Crud
        [Key]
        public int ResumeID { get; set; }

        [DisplayName("Nome do Candidato")]
        public string Summary { get; set; }
        //public IList<SelectListItem> LoadSummary { get; set; }

        [DisplayName("Função")]
        public string FunctionID { get; set; }
        public IList<SelectListItem> LoadFunction { get; set; }

        [DisplayName("Descrição")]
        public string Description { get; set; }

        [DisplayName("Gênero")]
        public string GenderID { get; set; }
        public IList<SelectListItem> LoadGender { get; set; }

        [DisplayName("Idade")]
        public string Age { get; set; }

        [DisplayName("Tempo de Experiência")]
        public string TimeExperience { get; set; }
        public IList<SelectListItem> LoadTimeExperience { get; set; }

        [DisplayName("Nível")]
        public string FunctionLevelID { get; set; }
        public IList<SelectListItem> LoadFunctionLevel { get; set; }

        [DisplayName("Status do RH")]
        public string StatusRhID { get; set; }
        public IList<SelectListItem> LoadStatusRh { get; set; }

        [DisplayName("Data Aprovação RH")]
        public string ApprovedDateRh { get; set; }

        [DisplayName("Status do Gestor")]
        public string StatusManagerID { get; set; }
        public IList<SelectListItem> LoadStatusManager { get; set; }

        [DisplayName("Data Aprovação Gestor")]
        public string ApprovedDateManager { get; set; }

        [DisplayName("Status do Cliente")]
        public string StatusClientID { get; set; }
        public IList<SelectListItem> LoadStatusClient { get; set; }

        [DisplayName("Data Aprovação Cliente")]
        public string ApprovedDateClient { get; set; }

        [DisplayName("Pretensão Salarial")]
        public string ExpectedSalary { get; set; }

        [DisplayName("Tipo de Contrato")]
        public string ContractTypeID { get; set; }
        public IList<SelectListItem> LoadContractType { get; set; }

        [DisplayName("Já foi funcionário?")]
        public bool IsEmployee { get; set; }

        [DisplayName("Disponível para viajar")]
        public bool WillingToTravel { get; set; }

        [DisplayName("Estado Civil")]
        public string MaritalStatusID { get; set; }
        public IList<SelectListItem> LoadMaritalStatus { get; set; }

        [DisplayName("Filhos?")]
        public bool HaveChildren { get; set; }

        [DisplayName("Fumante?")]
        public bool IsSmoker { get; set; }

        [DisplayName("Disponibilidade para Início")]
        public string AvailabilityToStart { get; set; }

        [DisplayName("Observações")]
        public string Observation { get; set; }

        [DisplayName("Criado")]
        public string CreatedByID { get; set; }
        public IList<SelectListItem> LoadCreatedBy { get; set; }

        [DisplayName("Data de Criação")]
        public string CreationDate { get; set; }      
       
        [DisplayName("Modificado")]
        public string ModifiedByID { get; set; }    
                                      
        [DisplayName("Última Modificação")]
        public string LastModifiedDate { get; set; }

        [DisplayName("Análise do RH")]
        public string ResultRh { get; set; }

        [DisplayName("Análise do Gestor")]
        public string ResultManager { get; set; }

        [DisplayName("Análise do Cliente")]
        public string ResultClient { get; set; }

        public string VacancieID { get; set; }
    }
}