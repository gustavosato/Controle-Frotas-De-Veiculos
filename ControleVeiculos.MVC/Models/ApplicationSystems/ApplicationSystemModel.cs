using FluentValidation.Attributes;
using ControleVeiculos.MVC.Validations.ApplicationSystem;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ControleVeiculos.MVC.Models.ApplicationSystems
{
    [Validator(typeof(ApplicationSystemValidator))]
    public class ApplicationSystemModel
    {
        public ApplicationSystemModel()
        {
            this.LoadApplicationType = new List<SelectListItem>();
        }

        [Key]
        public int ApplicationSystemID { get; set; }

        //filter
        [DisplayName("Nome da Aplicação")]
        public string SearchApplicationSystemName{ get; set; }

        //crud
        [DisplayName("Nome da Aplicação")]
        public string ApplicationSystemName { get; set; }

        [DisplayName("Descrição")]
        public string Description { get; set; }

        [DisplayName("Tipo de Aplicação")]
        public string ApplicationTypeID { get; set; }
        public IList<SelectListItem> LoadApplicationType { get; set; }

        [DisplayName("Criado por")]
        public string CreatedByID { get; set; }

        [DisplayName("Data de criação")]
        public string CreationDate { get; set; }

        [DisplayName("Modificado por")]
        public string ModifiedByID { get; set; }

        [DisplayName("Data da última modificação")]
        public string LastModifiedDate { get; set; }

        public string CustomerID { get; set; }


    }
}