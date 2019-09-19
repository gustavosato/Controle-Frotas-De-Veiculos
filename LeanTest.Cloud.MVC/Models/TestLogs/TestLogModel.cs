using FluentValidation.Attributes;
using Lean.Test.Cloud.MVC.Validations.TestLogs;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Lean.Test.Cloud.MVC.Models.TestLogs
{
    [Validator(typeof(TestLogValidator))]
    public class TestLogModel
    {
        public TestLogModel()
        {
            this.SearchLoadStatus = new List<SelectListItem>();

            this.LoadStatus = new List<SelectListItem>();

        }

        //filter
               
        [DisplayName("StatusID")]
        public string SearchStatusID { get; set; }
        public IList<SelectListItem> SearchLoadStatus { get; set; }

        //crud
        [Key]
        public int LogID { get; set; }

        [DisplayName("TestID")]
        public string TestID { get; set; }

        [DisplayName("Status")]
        public string StatusID { get; set; }
        public IList<SelectListItem> LoadStatus { get; set; }

        [DisplayName("StepName")]
        public string StepName { get; set; }

        [DisplayName("Resultado Esperado")]
        public string ExpectedResult { get; set; }

        [DisplayName("Resultado Atual")]
        public string ActualResult { get; set; }

        [DisplayName("Caminho da Evidência")]
        public string PathEvidence { get; set; }

        [DisplayName("Criado por")]
        public string CreatedByID { get; set; }

        [DisplayName("Data de Criação")]
        public string CreationDate { get; set; }

        [DisplayName("Modificado por")]
        public string ModifiedByID { get; set; }
        
        [DisplayName("Data da última modificação")]
        public string LastModifiedDate { get; set; }

        

    }
}