using FluentValidation.Attributes;
using ControleVeiculos.MVC.Validations.Customer;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ControleVeiculos.MVC.Models.Customers
{
    [Validator(typeof(CustomerValidator))]
    public class CustomerModel
    {
        public CustomerModel()
        {
            this.SearchLoadSegments = new List<SelectListItem>();
            this.SearchLoadTypes = new List<SelectListItem>();

            this.LoadSegments = new List<SelectListItem>();
            this.LoadTypes = new List<SelectListItem>();

        }

        [Key]
        public int CustomerID { get; set; }

        //filter
        [DisplayName("Empresa")]
        public string SearchCustomerName { get; set; }

        [DisplayName("Segmento")]
        public string SearchSegmentID { get; set; }
        public IList<SelectListItem> SearchLoadSegments { get; set; }

        [DisplayName("Tipo")]
        public string SearchTypeID { get; set; }
        public IList<SelectListItem> SearchLoadTypes { get; set; }

        //crud
        [DisplayName("Nome da Empresa")]
        public string CustomerName { get; set; }

        [DisplayName("Descrição")]
        public string Description { get; set; }

        [DisplayName("Ativo")]
        public string IsActive { get; set; }

        [DisplayName("Segmento")]
        public string SegmentID { get; set; }
        public IList<SelectListItem> LoadSegments { get; set; }

        [DisplayName("Tipo")]
        public string TypeID { get; set; }
        public IList<SelectListItem> LoadTypes { get; set; }

        [DisplayName("Site")]
        public string Site { get; set; }

        [DisplayName("Endereço")]
        public string Address { get; set; }

        [DisplayName("Criado por")]
        public string CreatedByID { get; set; }

        [DisplayName("Data de criação")]
        public string CreationDate { get; set; }

        [DisplayName("Modificado por")]
        public string ModifiedByID { get; set; }

        [DisplayName("Data da última modificação")]
        public string LastModifiedDate { get; set; }

        public string UserID { get; set; }
    }
}