using FluentValidation.Attributes;
using Lean.Test.Cloud.MVC.Validations.AnnexContract;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Lean.Test.Cloud.MVC.Models.AnnexContracts
{
    [Validator(typeof(AnnexContractValidator))]
    public class AnnexContractModel
    {
        public AnnexContractModel() 
        {

            this.SearchLoadExtencionPeriod = new List<SelectListItem>();
            this.SearchLoadContract = new List<SelectListItem>();


            this.LoadExtencionPeriod = new List<SelectListItem>();
            this.LoadContract = new List<SelectListItem>();


        }

        //filter

        [DisplayName("Periodo de Extensão")]
        public string SearchExtencionPeriodID { get; set; }
        public IList<SelectListItem> SearchLoadExtencionPeriod { get; set; }

        [DisplayName("Contrato")]
        public string SearchContractID { get; set; }
        public IList<SelectListItem> SearchLoadContract { get; set; }

        [DisplayName("Data de Início")]
        public string SearchStartDate { get; set; }

        [DisplayName("Data de Término")]
        public string SearchEndDate { get; set; }

        //crud
        [Key]
        public int AnnexID { get; set; }

        [DisplayName("Contrato")]
        public string ContractID { get; set; }
        public IList<SelectListItem> LoadContract { get; set; }

        [DisplayName("Objeto Anexo")]
        public string AnnexObject { get; set; }

        [DisplayName("Código da Oportunidade")]
        public string OportunityID { get; set; }
        public IList<SelectListItem> LoadOportunityID { get; set; }

        [DisplayName("Sumário")]
        public string Summary { get; set; }

        [DisplayName("Inicio")]
        public string StartDate { get; set; }

        [DisplayName("Término")]
        public string EndDate { get; set; }

        [DisplayName("Periodo de Extensão")]
        public string ExtencionPeriodID { get; set; }
        public IList<SelectListItem> LoadExtencionPeriod { get; set; }

        [DisplayName("Criado Por")]
        public string CreatedByID { get; set; }

        [DisplayName("Data de Criação")]
        public string CreationDate { get; set; }

        [DisplayName("Modificado por")]
        public string ModifiedByID { get; set; }
        
        [DisplayName("Data da última modificação")]
        public string LastModifiedDate { get; set; }


    }
}