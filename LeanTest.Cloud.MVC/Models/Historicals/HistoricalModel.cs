using FluentValidation.Attributes;
using Lean.Test.Cloud.MVC.Validations.Historicals;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Lean.Test.Cloud.MVC.Models.Historicals
{
    [Validator(typeof(HistoricalValidator))]
    public class HistoricalModel
    {
        public HistoricalModel()
        {
           
        }

        [Key]
        public int HistoricalID { get; set; }

        [DisplayName("Funcionalidade")]
        public string SystemFeatureID { get; set; }
        public IList<SelectListItem> LoadsystemFeatures { get; set; }

        [DisplayName("ID Registro")]
        public string RecordID { get; set; }

        [DisplayName("ActionID")]
        public string ActionID { get; set; }

        [DisplayName("Valor Anterior")]
        public string OldValue { get; set; }

        [DisplayName("Valor Atual")]
        public string NewValue { get; set; }

        [DisplayName("Nome do Campo")]
        public string FieldName { get; set; }

        [DisplayName("Criado por")]
        public string CreatedByID { get; set; }
        public IList<SelectListItem> LoadCreatedBys { get; set; }

        [DisplayName("Data de criação")]
        public string CreationDate { get; set; }

        [DisplayName("Modificado por")]
        public string ModifiedByID { get; set; }

        [DisplayName("Data da última modificação")]
        public string LastModifiedDate { get; set; }

    }
}