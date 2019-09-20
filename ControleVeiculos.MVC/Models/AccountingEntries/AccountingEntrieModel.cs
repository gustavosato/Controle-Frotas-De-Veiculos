using FluentValidation.Attributes;
using ControleVeiculos.MVC.Validations.AccountingEntries;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ControleVeiculos.MVC.Models.AccountingEntries
{
    [Validator(typeof(AccountingEntrieValidator))]
    public class AccountingEntrieModel
    {

        public AccountingEntrieModel()
        {
            this.LoadClass = new List<SelectListItem>();
            this.LoadCategorys = new List<SelectListItem>();
            this.LoadSubCategorys = new List<SelectListItem>();
            this.LoadAccounts = new List<SelectListItem>();
            this.LoadCustomers = new List<SelectListItem>();
            this.LoadDemands = new List<SelectListItem>();
            this.LoadStatus = new List<SelectListItem>();
            this.LoadEmployees = new List<SelectListItem>();

            this.LoadSearchCustomers = new List<SelectListItem>();
            this.LoadSearchDemands = new List<SelectListItem>();
            this.LoadSearchStatus = new List<SelectListItem>();

        }

        //filters
        [DisplayName("Data inicial da Competência")]
        public string SearchCompetitionStartDate { get; set; }

        [DisplayName("Data Final da Competência")]
        public string SearchCompetitionEndDate { get; set; }

        [DisplayName("Numero da Nota Fiscal")]
        public string SearchInvoiceNumber { get; set; }

        [DisplayName("Valor a ser realizado")]
        public string SearchValueToBeRealized { get; set; }

        [DisplayName("Valor Realizado")]
        public string SearchRealizedValue { get; set; }

        [DisplayName("Demanda")]
        public string SearchDemandID { get; set; }
        public IList<SelectListItem> LoadSearchDemands { get; set; }

        [DisplayName("Empresa")]
        public string SearchCustomerID { get; set; }
        public IList<SelectListItem> LoadSearchCustomers { get; set; }

        [DisplayName("Status")]
        public string SearchStatusID { get; set; }
        public IList<SelectListItem> LoadSearchStatus { get; set; }


        //crud
        [Key]
        public int AccountingEntrieID { get; set; }

        [DisplayName("Tipo de Conta")]
        public string ClassID { get; set; }
        public IList<SelectListItem> LoadClass { get; set; }

        [DisplayName("Categoria")]
        public string CategoryID { get; set; }
        public IList<SelectListItem> LoadCategorys { get; set; }

        [DisplayName("Sub Categoria")]
        public string SubCategoryID { get; set; }
        public IList<SelectListItem> LoadSubCategorys { get; set; }

        [DisplayName("Conta")]
        public string AccountID { get; set; }
        public IList<SelectListItem> LoadAccounts { get; set; }

        [DisplayName("Status")]
        public string StatusID { get; set; }
        public IList<SelectListItem> LoadStatus { get; set; }

        [DisplayName("Valor a ser Realizado")]
        public string ValueToBeRealized { get; set; }

        [DisplayName("Data da Competência")]
        public string CompetitionDate { get; set; }

        [DisplayName("Valor Realizado")]
        public string RealizedValue { get; set; }

        [DisplayName("Data Realizada")]
        public string RealizedDate { get; set; }

        [DisplayName("Data do Vencimento")]
        public string DueDate { get; set; }

        [DisplayName("Juros R$")]
        public string Interest { get; set; }

        [DisplayName("Número da Nota Fiscal")]
        public string InvoiceNumber { get; set; }

        [DisplayName("Número do Documento")]
        public string DocumentNumber { get; set; }

        [DisplayName("Demanda")]
        public string DemandID { get; set; }
        public IList<SelectListItem> LoadDemands { get; set; }

        [DisplayName("Empresa")]
        public string CustomerID { get; set; }
        public IList<SelectListItem> LoadCustomers { get; set; }

        [DisplayName("Colaborador")]
        public string EmployeeID { get; set; }
        public IList<SelectListItem> LoadEmployees { get; set; }

        [DisplayName("Descrição")]
        public string Description { get; set; }

        [DisplayName("Criado por")]
        public string CreatedByID { get; set; }

        [DisplayName("Data de Criação")]
        public string CreationDate { get; set; }

        public string ModifiedByID { get; set; }
        public string LastModifiedDate { get; set; }

        [DisplayName("Data Inicial da Competência")]
        public string CompetitionStartDate { get; set; }

        [DisplayName("Data Final da Competência")]
        public string CompetitionEndDate { get; set; }

        [DisplayName("Data Inicial do Vencimento")]
        public string StartDueDate { get; set; }

        [DisplayName("Data Final do Vencimento")]
        public string EndDueDate { get; set; }

        [DisplayName("Data Inicial Realizada")]
        public string StartDateRealized { get; set; }

        [DisplayName("Data Final Realizada")]
        public string EndDateRealized { get; set; }
    }
}