using FluentValidation.Attributes;
using Lean.Test.Cloud.MVC.Validations.TestPackages;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Lean.Test.Cloud.MVC.Models.TestPackages
{
    [Validator(typeof(TestPackageValidator))]
    public class TestPackageModel
    {
        public TestPackageModel()
        {
            this.SearchLoadTecnology = new List<SelectListItem>();
            this.SearchLoadBrowser = new List<SelectListItem>();
            this.SearchLoadDevice = new List<SelectListItem>();
            this.SearchLoadPlatformName = new List<SelectListItem>();
            this.SearchLoadMethodology = new List<SelectListItem>();
            this.SearchLoadStatus = new List<SelectListItem>();
            this.SearchLoadDemand = new List<SelectListItem>();


            this.LoadTecnology = new List<SelectListItem>();
            this.LoadBrowser = new List<SelectListItem>();
            this.LoadDevice = new List<SelectListItem>();
            this.LoadPlatformName = new List<SelectListItem>();
            this.LoadMethodology = new List<SelectListItem>();
            this.LoadStatus = new List<SelectListItem>();
            this.LoadDemand = new List<SelectListItem>();

        }

        //filter
       
        [DisplayName("Tecnologia")]
        public string SearchTecnologyID { get; set; }
        public IList<SelectListItem> SearchLoadTecnology { get; set; }

        [DisplayName("Navegador")]
        public string SearchBrowserID { get; set; }
        public IList<SelectListItem> SearchLoadBrowser { get; set; }

        [DisplayName("Dispositivo")]
        public string SearchDeviceID { get; set; }
        public IList<SelectListItem> SearchLoadDevice { get; set; }

        [DisplayName("Navegador")]
        public string SearchPlatformNameID { get; set; }
        public IList<SelectListItem> SearchLoadPlatformName { get; set; }

        [DisplayName("Tipo de Metodologia")]
        public string SearchMethodologyID { get; set; }
        public IList<SelectListItem> SearchLoadMethodology { get; set; }

        [DisplayName("Status")]
        public string SearchStatusID { get; set; }
        public IList<SelectListItem> SearchLoadStatus { get; set; }

        [DisplayName("Demanda")]
        public string SearchDemandID { get; set; }
        public IList<SelectListItem> SearchLoadDemand { get; set; }

        //crud
        [Key]
        public int TestPackageID { get; set; }

        [DisplayName("Nome do Pacote")]
        public string PackageName { get; set; }

        [DisplayName("Demanda")]
        public string DemandID { get; set; }
        public IList<SelectListItem> LoadDemand { get; set; }

        [DisplayName("Status")]
        public string StatusID { get; set; }
        public IList<SelectListItem> LoadStatus { get; set; }

        [DisplayName("Release")]
        public string Release { get; set; }

        [DisplayName("Ciclo")]
        public string Cycle { get; set; }

        [DisplayName("Descrição")]
        public string Description { get; set; }

        [DisplayName("Enviar reports por e-mail")]
        public string EmailsToSendReport { get; set; }

        [DisplayName("Tecnologia")]
        public string TecnologyID { get; set; }
        public IList<SelectListItem> LoadTecnology { get; set; }

        [DisplayName("Navegador")]
        public string BrowserID { get; set; }
        public IList<SelectListItem> LoadBrowser { get; set; }

        [DisplayName("Velocidade de Execução")]
        public string ExecutionSpeedy { get; set; }

        [DisplayName("Resetar Aplicação")]
        public bool ResetApp { get; set; }

        [DisplayName("HighLight")]
        public bool HighLight { get; set; }

        [DisplayName("HighLightOut")]
        public bool HighLightOut { get; set; }

        [DisplayName("Dispositivo")]
        public string DeviceID { get; set; }
        public IList<SelectListItem> LoadDevice { get; set; }

        [DisplayName("Plataforma")]
        public string PlatformNameID { get; set; }
        public IList<SelectListItem> LoadPlatformName { get; set; }

        [DisplayName("Enviar E-mail")]
        public bool SendEmail { get; set; }

        [DisplayName("Gerar Log")]
        public bool GenerateLog { get; set; }

        [DisplayName("Log em Html")]
        public bool LogHtml { get; set; }

        [DisplayName("Metodologia")]
        public string MethodologyID { get; set; }
        public IList<SelectListItem> LoadMethodology { get; set; }

        [DisplayName("Solução")]
        public string SolutionPath { get; set; }

        [DisplayName("Leantest Variable")]
        public string LeantestVariable { get; set; }

        [DisplayName("Exportar Evidencias")]
        public string SaveEvidenceToExternalPath { get; set; }

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