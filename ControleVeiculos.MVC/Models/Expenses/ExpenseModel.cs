using FluentValidation.Attributes;
using ControleVeiculos.MVC.Validations.Expenses;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ControleVeiculos.MVC.Models.Expenses
{
    [Validator(typeof(ExpenseValidator))]
    public class ExpenseModel
    {

        public ExpenseModel()
        {
            this.LoadStatus = new List<SelectListItem>();
            this.LoadDemands = new List<SelectListItem>();
            this.LoadTypeExpenses = new List<SelectListItem>();
            this.LoadCustomers = new List<SelectListItem>();
            this.LoadApproveds = new List<SelectListItem>();
            this.LoadCreateds = new List<SelectListItem>();
            this.LoadApproveds = new List<SelectListItem>();
            this.LoadDepartments = new List<SelectListItem>();


            this.SearchLoadStatus = new List<SelectListItem>();
            this.SearchLoadDemands = new List<SelectListItem>();
            this.SearchLoadCustomers = new List<SelectListItem>();
            this.SearchLoadCreateds = new List<SelectListItem>();
            this.SearchLoadTypeExpenses = new List<SelectListItem>();
            this.SearchLoadDepartments = new List<SelectListItem>();
        }

        //filter
        [DisplayName("Data Inicial")]
        public string SearchRegisterDateFrom { get; set; }

        [DisplayName("Data Final")]
        public string SearchRegisterDateTo { get; set; }

        [DisplayName("Data Inicial")]
        public string SearchStartDateReport { get; set; }

        [DisplayName("Data Final")]
        public string SearchEndDateReport { get; set; }

        [DisplayName("Descrição")]
        public string SearchDescription { get; set; }

        [DisplayName("Tipo de Despesa")]
        public string SearchTypeExpenseID { get; set; }
        public IList<SelectListItem> SearchLoadTypeExpenses { get; set; }

        [DisplayName("Empresa")]
        public string SearchCustomerID { get; set; }
        public IList<SelectListItem> SearchLoadCustomers { get; set; }

        [DisplayName("Demanda")]
        public string SearchDemandID{ get; set; }
        public IList<SelectListItem> SearchLoadDemands { get; set; }

        [DisplayName("Departamento")]
        public string SearchDepartmentID { get; set; }
        public IList<SelectListItem> SearchLoadDepartments { get; set; }

        [DisplayName("Criado por")]
        public string SearchCreatedByID { get; set; }
        public IList<SelectListItem> SearchLoadCreateds { get; set; }

        [DisplayName("Status")]
        public string SearchStatusID { get; set; }
        public IList<SelectListItem> SearchLoadStatus { get; set; }
        
        //form
        [Key]
        public int ExpenseID { get; set; }

        [DisplayName("Data da Despesa")]
        public string RegisterDate { get; set; }

        [DisplayName("Descrição")]
        public string Description { get; set; }

        [DisplayName("Tipo de Despesa")]
        public string TypeExpenseID { get; set; }
        public IList<SelectListItem> LoadTypeExpenses { get; set; }

        [DisplayName("Demanda")]
        public string DemandID { get; set; }
        public IList<SelectListItem> LoadDemands { get; set; }

        [DisplayName("Status")]
        public string StatusID { get; set; }
        public IList<SelectListItem> LoadStatus { get; set; }

        [DisplayName("Empresa")]
        public string CustomerID { get; set; }
        public IList<SelectListItem> LoadCustomers { get; set; }

        [DisplayName("Departamento")]
        public string DepartmentID { get; set; }
        public IList<SelectListItem> LoadDepartments { get; set; }

        [DisplayName("Valor R$")]
        public string SubTotal { get; set; }

        [DisplayName("Kilometragem")]
        public string Kilometer { get; set; }

        [DisplayName("Valor Total R$")]
        public string AmountExpense { get; set; }

        [DisplayName("Reenbolsável?")]
        public string Refundable { get; set; }

        [DisplayName("Aprovado por")]
        public string ApprovedByID { get; set; }
        public IList<SelectListItem> LoadApproveds { get; set; }
        
        [DisplayName("Data da Aprovação")]
        public string ApprovedDate { get; set; }

        [DisplayName("Criado por")]
        public string CreatedByID { get; set; }
        public IList<SelectListItem> LoadCreateds { get; set; }
        
        [DisplayName("Data de Criação")]
        public string CreationDate { get; set; }

        public string ModifiedByID { get; set; }
        public string LastModifiedDate { get; set; }

    }
}