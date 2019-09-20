using FluentValidation.Attributes;
using ControleVeiculos.MVC.Validations.DailyLog;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Collections.Generic;


namespace ControleVeiculos.MVC.Models.DailyLogs
{
    [Validator(typeof(DailyLogValidator))]
    public class DailyLogModel
    {
        public DailyLogModel()
        {
            this.SearchLoadDemands = new List<SelectListItem>();
            this.LoadDemands = new List<SelectListItem>();
            this.SearchLoadCreateds = new List<SelectListItem>();
        }

        [Key]
        public int DailyLogID { get; set; }

        //filter
        [DisplayName("Descrição")]
        public string SearchDescription { get; set; }

        [DisplayName("Demanda")]
        public string SearchDemandID { get; set; }
        public IList<SelectListItem> SearchLoadDemands { get; set; }

        [DisplayName("Criado por")]
        public string SearchCreatedID{ get; set; }
        public IList<SelectListItem> SearchLoadCreateds { get; set; }

        //crud
        [DisplayName("Descrição")]
        public string Description { get; set; }

        [DisplayName("Descrição")]
        public string Description1 { get; set; }

        [DisplayName("Uso Interno")]
        public bool IsInternal { get; set; }

        [DisplayName("Criado por")]
        public string CreatedByID { get; set; }

        [DisplayName("Data de criação")]
        public string CreationDate { get; set; }

        [DisplayName("Modificado por")]
        public string ModifiedByID { get; set; }

        [DisplayName("Data da última modificação")]
        public string LastModifiedDate { get; set; }

        [DisplayName("Demanda")]
        public string DemandID { get; set; }
        public IList<SelectListItem> LoadDemands { get; set; }

    }
}