using FluentValidation.Attributes;
using Lean.Test.Cloud.MVC.Validations.TestCases;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Lean.Test.Cloud.MVC.Models.TestCases
{
    [Validator(typeof(TestCaseValidator))]
    public class TestCaseModel
    {
        public TestCaseModel()
        {
            this.SearchLoadStatus = new List<SelectListItem>();
            this.SearchLoadFeature = new List<SelectListItem>();
            this.SearchLoadTestScenario = new List<SelectListItem>();
            this.SearchLoadFlowTest = new List<SelectListItem>();
            this.SearchLoadTestType = new List<SelectListItem>();

            this.LoadStatus = new List<SelectListItem>();
            this.LoadFeature = new List<SelectListItem>();
            this.LoadTestScenario = new List<SelectListItem>();
            this.LoadFlowTest = new List<SelectListItem>();
            this.LoadTestType = new List<SelectListItem>();

        }

        //filter
        [DisplayName("Status")]
        public string SearchStatusID { get; set; }
        public IList<SelectListItem> SearchLoadStatus { get; set; }

        [DisplayName("Funcionalidade")]
        public string SearchFeatureID { get; set; }
        public IList<SelectListItem> SearchLoadFeature { get; set; }

        [DisplayName("Cenário de Teste")]
        public string SearchTestScenarioID { get; set; }
        public IList<SelectListItem> SearchLoadTestScenario { get; set; }

        [DisplayName("Fluxo de Teste")]
        public string SearchFlowTestID { get; set; }
        public IList<SelectListItem> SearchLoadFlowTest { get; set; }

        [DisplayName("Tipo de Teste")]
        public string SearchTestTypeID { get; set; }
        public IList<SelectListItem> SearchLoadTestType { get; set; }

        [DisplayName("Caso de Teste")]
        public string SearchTestCase { get; set; }


        //crud
        [Key]
        public int TestCaseID { get; set; }

        [DisplayName("Status")]
        public string StatusID { get; set; }
        public IList<SelectListItem> LoadStatus { get; set; }

        [DisplayName("Caso de Teste")]
        public string TestCase { get; set; }

        [DisplayName("Descrição")]
        public string Description { get; set; }

        [DisplayName("Pré Condição")]
        public string Precondition { get; set; }

        [DisplayName("Resultado Esperado")]
        public string ExpectedResult { get; set; }

        [DisplayName("Funcionalidade")]
        public string FeatureID { get; set; }
        public IList<SelectListItem> LoadFeature { get; set; }

        [DisplayName("Cenário de Teste")]
        public string TestScenarioID { get; set; }
        public IList<SelectListItem> LoadTestScenario { get; set; }

        [DisplayName("Ordem de Execução")]
        public string ExecutionOrder { get; set; }

        [DisplayName("Fluxo de Teste")]
        public string FlowTestID { get; set; }
        public IList<SelectListItem> LoadFlowTest { get; set; }

        [DisplayName("Inicio da Execução")]
        public string StartExecution { get; set; }

        [DisplayName("Fim da Execução")]
        public string EndExecution { get; set; }

        [DisplayName("Tempo de Execução")]
        public string TimeExecution { get; set; }

        [DisplayName("Release")]
        public string Release { get; set; }

        [DisplayName("Ciclo")]
        public string Cycle { get; set; }

        [DisplayName("Tipo de Teste")]
        public string TestTypeID { get; set; }
        public IList<SelectListItem> LoadTestType { get; set; }

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