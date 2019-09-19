using FluentValidation.Attributes;
using Lean.Test.Cloud.MVC.Validations.Defects;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Lean.Test.Cloud.MVC.Models.Defects
{
    [Validator(typeof(DefectValidator))]
    public class DefectModel
    {
        public DefectModel()
        {
            this.SearchLoadStatus = new List<SelectListItem>();
            this.SearchLoadSeverity = new List<SelectListItem>();
            this.SearchLoadPriority = new List<SelectListItem>();
            this.SearchLoadAssingTo = new List<SelectListItem>();
            this.SearchLoadType = new List<SelectListItem>();
            this.SearchLoadResolution = new List<SelectListItem>();
            this.SearchLoadCreatedBy = new List<SelectListItem>();
            this.SearchLoadModifiedBy = new List<SelectListItem>();
            this.SearchLoadApplicationSystems = new List<SelectListItem>();
            this.SearchLoadFeatures = new List<SelectListItem>();

            this.LoadStatus = new List<SelectListItem>();
            this.LoadSeverity = new List<SelectListItem>();
            this.LoadPriority = new List<SelectListItem>();
            this.LoadAssingTo = new List<SelectListItem>();
            this.LoadType = new List<SelectListItem>();
            this.LoadResolution = new List<SelectListItem>();
            this.LoadCreatedBy = new List<SelectListItem>();
            this.LoadModifiedBy = new List<SelectListItem>();
            this.LoadApplicationSystems = new List<SelectListItem>();
            this.LoadFeatures = new List<SelectListItem>();
        }

        //Filter

        [DisplayName("Sumário")]
        public string SearchSummary { get; set; }

        [DisplayName("Status")]
        public string SearchStatusID { get; set; }
        public IList<SelectListItem> SearchLoadStatus { get; set; }

        [DisplayName("Aplicação")]
        public string SearchApplicationSystemID { get; set; }
        public IList<SelectListItem> SearchLoadApplicationSystems { get; set; }

        [DisplayName("Funcionalidade")]
        public string SearchFeatureID { get; set; }
        public IList<SelectListItem> SearchLoadFeatures { get; set; }

        [DisplayName("Severidade")]
        public string SearchSeverityID { get; set; }
        public IList<SelectListItem> SearchLoadSeverity { get; set; }

        [DisplayName("Prioridade")]
        public string SearchPriorityID { get; set; }
        public IList<SelectListItem> SearchLoadPriority { get; set; }

        [DisplayName("Associado")]
        public string SearchAssingToID { get; set; }
        public IList<SelectListItem> SearchLoadAssingTo { get; set; }

        [DisplayName("Tipo")]
        public string SearchTypeID { get; set; }
        public IList<SelectListItem> SearchLoadType { get; set; }

        [DisplayName("Resolução")]
        public string SearchResolutionID { get; set; }
        public IList<SelectListItem> SearchLoadResolution { get; set; }

        [DisplayName("Criado por")]
        public string SearchCreatedByID { get; set; }
        public IList<SelectListItem> SearchLoadCreatedBy { get; set; }

        [DisplayName("Modificado")]
        public string SearchModifiedByID { get; set; }
        public IList<SelectListItem> SearchLoadModifiedBy { get; set; }

        //Crud
        [Key]
        [DisplayName("ID")]
        public int DefectID { get; set; }

        [DisplayName("Status")]
        public string StatusID { get; set; }
        public IList<SelectListItem> LoadStatus { get; set; }

        [DisplayName("Severidade")]
        public string SeverityID { get; set; }
        public IList<SelectListItem> LoadSeverity { get; set; }

        [DisplayName("Prioridade")]
        public string PriorityID { get; set; }
        public IList<SelectListItem> LoadPriority { get; set; }

        [DisplayName("Associar")]
        public string AssingToID { get; set; }
        public IList<SelectListItem> LoadAssingTo { get; set; }

        [DisplayName("Tipo")]
        public string TypeID { get; set; }
        public IList<SelectListItem> LoadType { get; set; }

        [DisplayName("Aplicação")]
        public string ApplicationSystemID { get; set; }
        public IList<SelectListItem> LoadApplicationSystems { get; set; }

        [DisplayName("Funcionalidade")]
        public string FeatureID { get; set; }
        public IList<SelectListItem> LoadFeatures { get; set; }

        [DisplayName("Resolução")]
        public string ResolutionID { get; set; }
        public IList<SelectListItem> LoadResolution { get; set; }

        [DisplayName("Criado por")]
        public string CreatedByID { get; set; }
        public IList<SelectListItem> LoadCreatedBy { get; set; }

        [DisplayName("Status")]
        public string ModifiedByID { get; set; }
        public IList<SelectListItem> LoadCreated { get; set; }

        [DisplayName("Modificado")]
        public string LoadModifiedByID { get; set; }
        public IList<SelectListItem> LoadModifiedBy { get; set; }
                

        [DisplayName("Sumário")]
        public string Summary { get; set; }

        [DisplayName("Descrição")]
        public string Description { get; set; }

        [DisplayName("Resolução")]
        public string Resolution { get; set; }

        [DisplayName("Data de Resolução")]
        public string ResolutionDate { get; set; }

        [DisplayName("Data de Criação")]
        public string CreationDate { get; set; }

        [DisplayName("Última Modificação")]
        public string LastModifiedDate { get; set; }
    }
}