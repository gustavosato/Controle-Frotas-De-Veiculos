using FluentValidation.Attributes;
using ControleVeiculos.MVC.Validations.PositionsSalaries;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ControleVeiculos.MVC.Models.PositionsSalaries
{
    [Validator(typeof(PositionsSalarieValidator))]
    public class PositionsSalarieModel
    {
        public PositionsSalarieModel()
        {
            this.SearchLoadFunction = new List<SelectListItem>();
            this.LoadLevel = new List<SelectListItem>();
            this.LoadClassification = new List<SelectListItem>();
            this.LoadFunction = new List<SelectListItem>();

        }

        //Filter

        [DisplayName("Função")]
        public string SearchFunctionID { get; set; }
        public IList<SelectListItem> SearchLoadFunction { get; set; }

    
        //Crud
        [Key]
        public int PositionsSalarieID { get; set; }
               
        [DisplayName("Função")]
        public string FunctionID { get; set; }
        public IList<SelectListItem> LoadFunction { get; set; }

        [DisplayName("Classificação")]
        public string ClassificationID { get; set; }
        public IList<SelectListItem> LoadClassification { get; set; }

        [DisplayName("Nível")]
        public string LevelID { get; set; }
        public IList<SelectListItem> LoadLevel { get; set; }       

        [DisplayName("Valor PJ")]
        public string AmountPJ { get; set; }
                      
        [DisplayName("Valor CLT")]
        public string AmountCLT { get; set; }

        [DisplayName("Valor CLT Flex")]
        public string AmountCLTFLEX { get; set; }

        [DisplayName("Criado")]
        public string CreatedByID { get; set; }
        public IList<SelectListItem> LoadCreatedBy { get; set; }

        [DisplayName("Data de Criação")]
        public string CreationDate { get; set; }

        [DisplayName("Modificado")]
        public string ModifiedByID { get; set; }
        public IList<SelectListItem> LoadModifiedBy { get; set; }

        [DisplayName("Última Modificação")]
        public string LastModifiedDate { get; set; }

        [DisplayName("Início de Vigência")]
        public string StartingDate { get; set; }

        [DisplayName("Término de Vigência")]
        public string ClosingDate { get; set; }

    }
}