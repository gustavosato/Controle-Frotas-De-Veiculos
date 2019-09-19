using FluentValidation.Attributes;
using Lean.Test.Cloud.MVC.Validations.Skills;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Lean.Test.Cloud.MVC.Models.Skills
{
    [Validator(typeof(SkillValidator))]
    public class SkillModel
    {
        public SkillModel()
        {

            this.SearchLoadSkillType = new List<SelectListItem>();


            this.LoadSkillType = new List<SelectListItem>();

        }

        //filter
        [DisplayName("Sumário")]
        public string SearchSummary { get; set; }

        [DisplayName("Tipo")]
        public string SearchSkillTypeID { get; set; }
        public IList<SelectListItem> SearchLoadSkillType { get; set; }

        //crud
        [Key]
        public int SkillID { get; set; }

        [DisplayName("Sumário")]
        public string Summary { get; set; }
        
        [DisplayName("Tipo do Registro")]
        public string SkillTypeID { get; set; }
        public IList<SelectListItem> LoadSkillType { get; set; }

        [DisplayName("Descrição")]
        public string Description { get; set; }

        [DisplayName("Criado Por")]
        public string CreatedByID { get; set; }
        public IList<SelectListItem> LoadCreatedBy { get; set; }

        [DisplayName("Data de Criação")]
        public string CreationDate { get; set; }

        [DisplayName("Modificado por")]
        public string ModifiedByID { get; set; }
        public IList<SelectListItem> LoadModifiedBy { get; set; }

        [DisplayName("Data da última modificação")]
        public string LastModifiedDate { get; set; }

    }
}