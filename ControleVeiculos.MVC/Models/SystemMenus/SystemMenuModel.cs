using FluentValidation.Attributes;
using ControleVeiculos.MVC.Validations.SystemMenus;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ControleVeiculos.MVC.Models.SystemMenus
{
    [Validator(typeof(SystemMenuValidator))]
    public class SystemMenuModel
    {
        public SystemMenuModel()
        {
            this.SearchLoadSystemFeatures = new List<SelectListItem>();
            this.LoadSystemFeatures = new List<SelectListItem>();
        }

        //consultas
        [DisplayName("Funcionalidade")]
        public string SearchSystemFeature { get; set; }
        public IList<SelectListItem> SearchLoadSystemFeatures { get; set; }

        //crud
        [Key]
        public int MenuID { get; set; }

        [DisplayName("Nome do Menu")]
        public string TextMenu { get; set; }
        

        [DisplayName("Descrição")]
        public string Description { get; set; }

        [DisplayName("Ordem")]
        public string Ordem { get; set; }

        [DisplayName("UrlAction")]
        public string UrlAction { get; set; }
        
        [DisplayName("Controller")]
        public string Controller { get; set; }

        [DisplayName("Icone")]
        public string Icon { get; set; }
        
        [DisplayName("Administrador")]
        public bool ItsAdmin { get; set; }

        [DisplayName("Funcionalidade")]
        public string SystemFeatureID { get; set; }
        public IList<SelectListItem> LoadSystemFeatures { get; set; }

        [DisplayName("Criado por")]
        public string CreatedByID { get; set; }

        [DisplayName("Data de criação")]
        public string CreationDate { get; set; }

        [DisplayName("Modificado por")]
        public string ModifiedByID { get; set; }

        [DisplayName("Data da última modificação")]
        public string LastModifiedDate { get; set; }

        
    }
}