using FluentValidation.Attributes;
using Lean.Test.Cloud.MVC.Validations.ParameterValues;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Lean.Test.Cloud.MVC.Models.ParameterValues
{
    [Validator(typeof(ParameterValueValidator))]
    public class ParameterValueModel
    {
        public ParameterValueModel()
        {
            this.SearchLoadParameters = new List<SelectListItem>();

            this.LoadParameters = new List<SelectListItem>();
        }

        //Filter
        
        [DisplayName("Parâmetro ID")]
        public string SearchParameterID { get; set; }
        public IList<SelectListItem> SearchLoadParameters { get; set; }

        [DisplayName("Valor")]
        public string SearchParameterValue { get; set; }

        //Crud
        [Key]
        public int ParameterValueID { get; set; }

        [DisplayName("Parâmetro ID")]
        public string ParameterID { get; set; }
        public IList<SelectListItem> LoadParameters { get; set; }

        [DisplayName("Valor do Parâmetro")]
        public string ParameterValue { get; set; }

        [DisplayName("Ordenação")]
        public string ParentID { get; set; }
        
        [DisplayName("Parâmetro Padrão de Sistema")]
        public string IsSystem { get; set; }

        [DisplayName("Descrição")]
        public string Description { get; set; }

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