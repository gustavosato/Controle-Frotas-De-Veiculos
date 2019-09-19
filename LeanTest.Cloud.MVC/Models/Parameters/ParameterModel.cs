using FluentValidation.Attributes;
using Lean.Test.Cloud.MVC.Validations.Parameters;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Lean.Test.Cloud.MVC.Models.Parameters
{
    [Validator(typeof(ParameterValidator))]
    public class ParameterModel
    {
        public ParameterModel()
        {
            this.SearchLoadSystemFeatures = new List<SelectListItem>();
            this.LoadSystemFeatures = new List<SelectListItem>();
        }

        //Filter
        [DisplayName("Nome")]
        public string SearchParameterName { get; set; }

        [DisplayName("Funcionalidade")]
        public string SearchSystemFeatureID { get; set; }
        public IList<SelectListItem> SearchLoadSystemFeatures { get; set; }

        //Crud
        [Key]
        public int ParameterID { get; set; }

        [DisplayName("Nome")]
        public string ParameterName { get; set; }

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
