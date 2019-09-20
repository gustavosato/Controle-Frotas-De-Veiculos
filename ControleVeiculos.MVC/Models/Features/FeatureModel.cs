using FluentValidation.Attributes;
using ControleVeiculos.MVC.Validations.Features;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ControleVeiculos.MVC.Models.Features
{
    [Validator(typeof(FeatureValidator))]
    public class FeatureModel
    {
        public FeatureModel()
        {
            this.LoadApplications = new List<SelectListItem>();
            this.LoadDevelopers = new List<SelectListItem>();
            this.LoadFeatureTypes = new List<SelectListItem>();
            this.LoadStatus = new List<SelectListItem>();
        }
        //filter
        [DisplayName("Aplicação")]
        public string SearchApplicationSystemID { get; set; }
        public IList<SelectListItem> SearchLoadApplications { get; set; }

        [DisplayName("Nome da funcionalidade")]
        public string SearchFeatureName { get; set; }

        //crud
        [Key]
        public int FeatureID { get; set; }

        [DisplayName("Nome da Funcionalidade")]
        public string FeatureName { get; set; }

        [DisplayName("Descrição")]
        public string Description { get; set; }

        [DisplayName("Status")]
        public string StatusID { get; set; }
        public IList<SelectListItem> LoadStatus { get; set; }

        [DisplayName("Aplicação")]
        public string ApplicationSystemID { get; set; }
        public IList<SelectListItem> LoadApplications { get; set; }

        [DisplayName("Automatizador")]
        public string DeveloperID { get; set; }
        public IList<SelectListItem> LoadDevelopers { get; set; }


        [DisplayName("Tipo de Funcionalidae")]
        public string FeatureTypeID { get; set; }
        public IList<SelectListItem> LoadFeatureTypes { get; set; }

        [DisplayName("Meta Script")]
        public string MetaScript { get; set; }

        [DisplayName("TestPoints")]
        public string TestPoints { get; set; }

        [DisplayName("Script de Automação")]
        public string AutomationScript { get; set; }

        [DisplayName("Esforço")]
        public string TimeEffort { get; set; }

        [DisplayName("Prazo")]
        public string TargetDate { get; set; }


        [DisplayName("Criado por")]
        public string CreatedByID { get; set; }

        [DisplayName("Data de Criação")]
        public string CreationDate { get; set; }

        [DisplayName("Modificado por")]
        public string ModifiedByID { get; set; }

        [DisplayName("Data da última modificação")]
        public string LastModifiedDate { get; set; }

        public string CustomerID { get; set; }

        public string TestScenarioID { get; set; }

        public string ExecutionOrder { get; set; }

        public bool IsLoop { get; set; }

        public string ToolsTestID { get; set; }

        public string TestScenarioFeatureID { get; set; }

    }
}