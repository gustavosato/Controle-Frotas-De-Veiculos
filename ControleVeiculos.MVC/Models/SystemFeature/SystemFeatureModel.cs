using FluentValidation.Attributes;
using ControleVeiculos.MVC.Validations.SystemFeatures;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ControleVeiculos.MVC.Models.SystemFeatures
{
    [Validator(typeof(SystemFeatureValidator))]
    public class SystemFeatureModel
    {
        public SystemFeatureModel()
        {
            this.LoadSystemFeatureType = new List<SelectListItem>();
            this.SearchLoadSystemFeatureType = new List<SelectListItem>();
        }

        //consultas
        [DisplayName("Nome")]
        public string SearchSystemFeature { get; set; }

        [DisplayName("Tipo de funcionalidade")]
        public string SearchSystemFeatureTypeID { get; set; }
        public IList<SelectListItem> SearchLoadSystemFeatureType { get; set; }

        //crud
        [Key]
        public int SystemFeatureID { get; set; }

        [DisplayName("Nome")]
        public string SystemFeatureName { get; set; }

        [DisplayName("Tipo de funcionalidade")]
        public string SystemFeatureTypeID { get; set; }
        public IList<SelectListItem> LoadSystemFeatureType { get; set; }


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