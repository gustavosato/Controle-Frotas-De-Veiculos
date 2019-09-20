using FluentValidation.Attributes;
using ControleVeiculos.MVC.Validations.Contract;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ControleVeiculos.MVC.Models.Contracts
{
    [Validator(typeof(ContractValidator))]
    public class ContractModel
    {
        public ContractModel()
        {
            this.SearchLoadOportunity = new List<SelectListItem>();
            this.SearchLoadContractType = new List<SelectListItem>();
            this.SearchLoadContractorCustomer = new List<SelectListItem>();
            this.SearchLoadContractingCustomer = new List<SelectListItem>();
            this.SearchLoadPeriodValidity = new List<SelectListItem>();
            this.SearchLoadExtencion = new List<SelectListItem>();
            this.SearchLoadExtencionPeriod = new List<SelectListItem>();
            this.SearchLoadResetModality = new List<SelectListItem>();


            this.LoadOportunity = new List<SelectListItem>();
            this.LoadContractType = new List<SelectListItem>();
            this.LoadContractorCustomer = new List<SelectListItem>();
            this.LoadContractingCustomer = new List<SelectListItem>();
            this.LoadPeriodValidity = new List<SelectListItem>();
            this.LoadExtencion = new List<SelectListItem>();
            this.LoadExtencionPeriod = new List<SelectListItem>();
            this.LoadResetModality = new List<SelectListItem>();


        }

        //filter

        [DisplayName("Contrato RP")]
        public string SearchOportunityID { get; set; }
        public IList<SelectListItem> SearchLoadOportunity { get; set; }

        [DisplayName("Tipo de Contrato")]
        public string SearchContractTypeID{ get; set; }
        public IList<SelectListItem> SearchLoadContractType { get; set; }

        [DisplayName("Contratado")]
        public string SearchContractorCustomerID { get; set; }
        public IList<SelectListItem> SearchLoadContractorCustomer { get; set; }

        [DisplayName("Contratante")]
        public string SearchContractingCustomerID { get; set; }
        public IList<SelectListItem> SearchLoadContractingCustomer { get; set; }

        [DisplayName("Data de Início")]
        public string SearchStartDate { get; set; }

        [DisplayName("Data de Término")]
        public string SearchEndDate { get; set; }

        [DisplayName("Periodo de Validade")]
        public string SearchPeriodValidityID { get; set; }
        public IList<SelectListItem> SearchLoadPeriodValidity { get; set; }

        [DisplayName("Extensão")]
        public string SearchExtencionID { get; set; }
        public IList<SelectListItem> SearchLoadExtencion { get; set; }

        [DisplayName("Periodo de Extensão")]
        public string SearchExtencionPeriodID { get; set; }
        public IList<SelectListItem> SearchLoadExtencionPeriod { get; set; }

        [DisplayName("Redefinir Modalidade")]
        public string SearchResetModalityID { get; set; }
        public IList<SelectListItem> SearchLoadResetModality { get; set; }

        //crud
        [Key]
        public int ContractID { get; set; }

        [DisplayName("Contrato RP")]
        public string OportunityID { get; set; }
        public IList<SelectListItem> LoadOportunity { get; set; }

        [DisplayName("Tipo de Contrato")]
        public string ContractTypeID { get; set; }
        public IList<SelectListItem> LoadContractType { get; set; }

        [DisplayName("Contratado")]
        public string ContractorCustomerID { get; set; }
        public IList<SelectListItem> LoadContractorCustomer { get; set; }

        [DisplayName("Contratante")]
        public string ContractingCustomerID { get; set; }
        public IList<SelectListItem> LoadContractingCustomer { get; set; }

        [DisplayName("Objeto de Contrato")]
        public string ObjectContract { get; set; }

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