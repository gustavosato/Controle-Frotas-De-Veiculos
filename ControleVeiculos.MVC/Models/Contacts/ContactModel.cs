using FluentValidation.Attributes;
using ControleVeiculos.MVC.Validations.Contacts;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ControleVeiculos.MVC.Models.Contacts
{
    [Validator(typeof(ContactValidator))]
    public class ContactModel
    {
        public ContactModel()
        {
            this.SearchLoadCustomer = new List<SelectListItem>();

            this.LoadCustomer = new List<SelectListItem>();

        }

        //Filter

        [DisplayName("Nome")]
        public string SearchContactName { get; set; }

        [DisplayName("E-mail")]
        public string SearchEmail { get; set; }

        [DisplayName("Empresa")]
        public string SearchCustomerID { get; set; }
        public IList<SelectListItem> SearchLoadCustomer { get; set; }


        //Crud
        [Key]
        public int ContactID { get; set; }

        [DisplayName("Nome do Contato")]
        public string ContactName { get; set; }

        [DisplayName("E-mail")]
        public string Email { get; set; }

        [DisplayName("Celular")]
        public string CellNumber { get; set; }

        [DisplayName("Telefone")]
        public string TelNumber { get; set; }

        [DisplayName("Função")]
        public string FunctionID { get; set; }
        public IList<SelectListItem> LoadFunction { get; set; }

        [DisplayName("Cliente")]
        public string CustomerID { get; set; }
        public IList<SelectListItem> LoadCustomer { get; set; }

        [DisplayName("Descrição")]
        public string Description { get; set; }

        [DisplayName("Características")]
        public string Feature { get; set; }

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