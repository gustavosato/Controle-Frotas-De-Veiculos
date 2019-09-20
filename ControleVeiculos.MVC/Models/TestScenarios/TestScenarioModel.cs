using FluentValidation.Attributes;
using ControleVeiculos.MVC.Validations.TestScenarios;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ControleVeiculos.MVC.Models.TestScenarios
{
    [Validator(typeof(TestScenarioValidator))]
    public class TestScenarioModel
    {
        public TestScenarioModel()
        {
            this.SearchLoadTestType = new List<SelectListItem>();
            this.SearchLoadExecutionType = new List<SelectListItem>();
            this.SearchLoadStatus = new List<SelectListItem>();

            this.LoadTestType = new List<SelectListItem>();
            this.LoadExecutionType = new List<SelectListItem>();
            this.LoadStatus = new List<SelectListItem>();

        }

        //filter

        [DisplayName("Tipo de Teste")]
        public string SearchTestTypeID { get; set; }
        public IList<SelectListItem> SearchLoadTestType { get; set; }

        [DisplayName("Tipo de Execução")]
        public string SearchExecutionTypeID { get; set; }
        public IList<SelectListItem> SearchLoadExecutionType { get; set; }

        [DisplayName("Status")]
        public string SearchStatusID { get; set; }
        public IList<SelectListItem> SearchLoadStatus { get; set; }

        [DisplayName("Cenário de Teste")]
        public string SearchTestScenario { get; set; }

        //crud
        [Key]
        public int TestScenarioID { get; set; }

        [DisplayName("Cenário de Teste")]
        public string TestScenario { get; set; }

        [DisplayName("Descrição")]
        public string Description { get; set; }

        [DisplayName("Status")]
        public string StatusID { get; set; }
        public IList<SelectListItem> LoadStatus { get; set; }

        [DisplayName("Ordem de Execução")]
        public string ExecutionOrder { get; set; }

        [DisplayName("Inicio da Execução")]
        public string StartExecution { get; set; }

        [DisplayName("Final da Execução")]
        public string EndExecution { get; set; }

        [DisplayName("Tempo de Execução")]
        public string TimeExecution { get; set; }

        [DisplayName("Tipo de Teste")]
        public string TestTypeID { get; set; }
        public IList<SelectListItem> LoadTestType { get; set; }

        [DisplayName("Tipo de Execução")]
        public string ExecutionTypeID { get; set; }
        public IList<SelectListItem> LoadExecutionType { get; set; }
        
        [DisplayName("Criado por")]
        public string CreatedByID { get; set; }

        [DisplayName("Data de Criação")]
        public string CreationDate { get; set; }

        [DisplayName("Modificado por")]
        public string ModifiedByID { get; set; }
        
        [DisplayName("Data da última modificação")]
        public string LastModifiedDate { get; set; }

        [DisplayName("Pacote")]
        public string TestPackageID { get; set; }

    }
}