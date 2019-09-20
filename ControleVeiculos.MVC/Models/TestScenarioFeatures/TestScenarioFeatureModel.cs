using FluentValidation.Attributes;
using ControleVeiculos.MVC.Validations.TestScenarioFeatures;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ControleVeiculos.MVC.Models.TestScenarioFeatures
{
    [Validator(typeof(TestScenarioFeatureValidator))]
    public class TestScenarioFeatureModel
    {
        public TestScenarioFeatureModel()
        {
            //this.SearchLoadTestType = new List<SelectListItem>();
            
            //this.LoadStatus = new List<SelectListItem>();

        }

        //filter
        [DisplayName("Cenário de Teste")]
        public string SearchTestScenarioID { get; set; }

        [DisplayName("Funcionalidade")]
        public string SearchFeatureName { get; set; }


        //crud
        [Key]
        public int TestScenarioFeatureID { get; set; }

        [DisplayName("Cenário de Teste")]
        public int TestScenarioID { get; set; }

        [DisplayName("Funcionalidade")]
        public int FeatureID { get; set; }

        [DisplayName("Ordem de Execução")]
        public string ExecutionOrder { get; set; }

        [DisplayName("IsLoop")]
        public bool IsLoop { get; set; }

        [DisplayName("Status")]
        public string StatusID { get; set; }

        [DisplayName("ToolsTest")]
        public string ToolsTestID { get; set; }

        [DisplayName("Tipo de Teste")]
        public string TestID { get; set; }

        [DisplayName("Criado por")]
        public string CreatedByID { get; set; }

        [DisplayName("Data de Criação")]
        public string CreationDate { get; set; }

        [DisplayName("Modificado por")]
        public string ModifiedByID { get; set; }
        
        [DisplayName("Data da última modificação")]
        public string LastModifiedDate { get; set; }

        [DisplayName("Funcionalidade")]
        public string FeatureName { get; set; }

    }
}