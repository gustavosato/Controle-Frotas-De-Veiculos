using FluentValidation.Attributes;
using ControleVeiculos.MVC.Validations.PipelineEvents;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ControleVeiculos.MVC.Models.PipelineEvents
{
    [Validator(typeof(PipelineEventValidator))]
    public class PipelineEventModel
    {
        public PipelineEventModel()
        {
            this.SearchLoadType = new List<SelectListItem>();
            this.SearchLoadNextStep = new List<SelectListItem>();
            this.SearchLoadOportunity = new List<SelectListItem>();            

            this.LoadType = new List<SelectListItem>();
            this.LoadNextStep = new List<SelectListItem>();
            this.LoadOportunity = new List<SelectListItem>();
            this.LoadCreateds = new List<SelectListItem>();
        }

        //Filter
        [DisplayName("Descrição")]
        public string SearchDescription { get; set; }

        [DisplayName("Data de Registro")]
        public string SearchRegisterDate { get; set; }

        [DisplayName("Tipo")]
        public string SearchTypeID { get; set; }
        public IList<SelectListItem> SearchLoadType { get; set; }

        [DisplayName("Próximo Passo")]
        public string SearchNextStepID { get; set; }
        public IList<SelectListItem> SearchLoadNextStep { get; set; }
      
        [DisplayName("Oportunidade")]
        public string SearchOportunityID { get; set; }
        public IList<SelectListItem> SearchLoadOportunity { get; set; }

        [DisplayName("Criado por")]
        public string SearchCreatedID { get; set; }
        public IList<SelectListItem> SearchLoadCreateds { get; set; }

        //Crud
        [Key]
        public int SaleEventID { get; set; }

        [DisplayName("Data do Evento")]
        public string RegisterDate { get; set; }

        [DisplayName("Tipo de Contato")]
        public string TypeID { get; set; }
        public IList<SelectListItem> LoadType { get; set; }

        [DisplayName("Próximo Passo")]
        public string NextStepID { get; set; }
        public IList<SelectListItem> LoadNextStep { get; set; }

        [DisplayName("Data Alvo")]
        public string TargetDate { get; set; }

        [DisplayName("Descrição")]
        public string Description { get; set; }

        [DisplayName("Oportunidade")]
        public string OportunityID { get; set; }
        public IList<SelectListItem> LoadOportunity { get; set; }

        [DisplayName("Criado Por")]
        public string CreatedByID { get; set; }
        public IList<SelectListItem> LoadCreateds { get; set; }

        [DisplayName("Data de Criação")]
        public string CreationDate { get; set; }

        [DisplayName("Modificado")]
        public string ModifiedByID { get; set; }

        [DisplayName("Última Modificação")]
        public string LastModifiedDate { get; set; }
    }
}