using FluentValidation.Attributes;
using Lean.Test.Cloud.MVC.Validations.ContractAdditive;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Lean.Test.Cloud.MVC.Models.ContractAdditives
{
    [Validator(typeof(ContractAdditiveValidator))]
    public class ContractAdditiveModel
    {
        public ContractAdditiveModel()
        {

            this.SearchLoadPeriodValidity = new List<SelectListItem>();
            this.SearchLoadExtencion = new List<SelectListItem>();
            this.SearchLoadExtencionPeriod = new List<SelectListItem>();
            this.SearchLoadResetModality = new List<SelectListItem>();


            this.LoadPeriodValidity = new List<SelectListItem>();
            this.LoadExtencion = new List<SelectListItem>();
            this.LoadExtencionPeriod = new List<SelectListItem>();
            this.LoadResetModality = new List<SelectListItem>();
            this.LoadOportunityID = new List<SelectListItem>();
        }

        //filter
        [DisplayName("Periodo de Validade")]
        public string SearchPeriodValidity { get; set; }
        public IList<SelectListItem> SearchLoadPeriodValidity { get; set; }

        [DisplayName("Extensão")]
        public string SearchExtencion { get; set; }
        public IList<SelectListItem> SearchLoadExtencion { get; set; }

        [DisplayName("Data de Início")]
        public string SearchStartDate { get; set; }

        [DisplayName("Data de Término")]
        public string SearchEndDate { get; set; }

        [DisplayName("Periodo de Extensão")]
        public string SearchExtencionPeriodID { get; set; }
        public IList<SelectListItem> SearchLoadExtencionPeriod { get; set; }

        [DisplayName("Redefinir Modalidade")]
        public string SearchResetModalityID { get; set; }
        public IList<SelectListItem> SearchLoadResetModality { get; set; }

        

        //crud
        [Key]
        public int AdditiveID { get; set; }

        [DisplayName("Contrato")]
        public string ContractID { get; set; }
        public IList<SelectListItem> LoadEmployees { get; set; }

        [DisplayName("Adicionar Objeto")]
        public string AdditiveObject { get; set; }

        [DisplayName("Início")]
        public string StartDate { get; set; }

        [DisplayName("Término")]
        public string EndDate { get; set; }

        [DisplayName("Periodo de Validade")]
        public string PeriodValidityID { get; set; }
        public IList<SelectListItem> LoadPeriodValidity { get; set; }

        [DisplayName("Extensão")]
        public string ExtencionID { get; set; }
        public IList<SelectListItem> LoadExtencion { get; set; }

        [DisplayName("Periodo de Extensão")]
        public string ExtencionPeriodID { get; set; }
        public IList<SelectListItem> LoadExtencionPeriod { get; set; }

        [DisplayName("Redefinir Modalidade")]
        public string ResetModalityID { get; set; }
        public IList<SelectListItem> LoadResetModality { get; set; }

        [DisplayName("Condição de Faturamento")]
        public string BillingCondition { get; set; }

        [DisplayName("Código da Oportunidade")]
        public string OportunityID { get; set; }
        public IList<SelectListItem> LoadOportunityID { get; set; }

        [DisplayName("Criado Por")]
        public string CreatedByID { get; set; }

        [DisplayName("Data de Criação")]
        public string CreationDate { get; set; }

        [DisplayName("Modificado por")]
        public string ModifiedByID { get; set; }
        
        [DisplayName("Data da última modificação")]
        public string LastModifiedDate { get; set; }


    }
}