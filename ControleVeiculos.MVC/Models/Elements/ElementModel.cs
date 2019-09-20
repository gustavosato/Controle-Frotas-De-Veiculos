using FluentValidation.Attributes;
using ControleVeiculos.MVC.Validations.Elements;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ControleVeiculos.MVC.Models.Elements
{
    [Validator(typeof(ElementValidator))]
    public class ElementModel
    {
        public ElementModel()
        {
            this.SearchLoadApplications = new List<SelectListItem>();

            this.SearchLoadFeatures= new List<SelectListItem>();
        }

        //Filter
        [DisplayName("Nome")]
        public string SearchElementName { get; set; }

        [DisplayName("Aplicação")]
        public string SearchApplicatioinID { get; set; }
        public IList<SelectListItem> SearchLoadApplications { get; set; }

        [DisplayName("Funcionalidade")]
        public string SearchFeatureID { get; set; }
        public IList<SelectListItem> SearchLoadFeatures { get; set; }

        //Crud
        [Key]
        [DisplayName("Adicionar")]
        public int ElementID { get; set; }

        [DisplayName("Nome")]
        public string Element { get; set; }

        [DisplayName("Ação")]
        public string ActionID { get; set; }
        public IList<SelectListItem> LoadActions { get; set; }

        [DisplayName("Valor Padrão")]
        public string DefaultValue { get; set; }

        [DisplayName("Dominios")]
        public string Domains { get; set; }

        [DisplayName("ID Elemento")]
        public string AutomationID { get; set; }
        public IList<SelectListItem> LoadFunction { get; set; }

        [DisplayName("Tipo de Identificação")]
        public string TypeIdentificationID { get; set; }
        public IList<SelectListItem> LoadTypeIdentification { get; set; }

        [DisplayName("Funcionalidade")]
        public string FeatureID { get; set; }

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