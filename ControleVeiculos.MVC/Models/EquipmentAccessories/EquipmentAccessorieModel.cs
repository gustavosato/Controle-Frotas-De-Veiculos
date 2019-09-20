using FluentValidation.Attributes;
using ControleVeiculos.MVC.Validations.EquipmentAccessories;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ControleVeiculos.MVC.Models.EquipmentAccessories
{
    [Validator(typeof(EquipmentAccessorieValidator))]
    public class EquipmentAccessorieModel
    {
        public EquipmentAccessorieModel()
        {
            this.SearchLoadAssignTo = new List<SelectListItem>();
            this.SearchLoadTypes = new List<SelectListItem>();


            this.LoadAssignTo = new List<SelectListItem>();
            this.LoadTypes = new List<SelectListItem>();


        }

        //filter

        [DisplayName("Associado a")]
        public string SearchAssignToID { get; set; }
        public IList<SelectListItem> SearchLoadAssignTo { get; set; }

        [DisplayName("Tipo")]
        public string SearchTypeID { get; set; }
        public IList<SelectListItem> SearchLoadTypes { get; set; }

        //crud
        [Key]
        public int EquipmentAccessorieID { get; set; }

        [DisplayName("Descrição")]
        public string Description { get; set; }

        [DisplayName("Número de Série")]
        public string SerialNumbers { get; set; }

        [DisplayName("Nome do Modelo")]
        public string ModelNames { get; set; }

        [DisplayName("Associado a")]
        public string AssignToID { get; set; }
        public IList<SelectListItem> LoadAssignTo { get; set; }

        [DisplayName("Tipo")]
        public string TypeID { get; set; }
        public IList<SelectListItem> LoadTypes { get; set; }

        [DisplayName("Faturável")]
        public bool Invoicing { get; set; }

        [DisplayName("Valor de Faturamento R$")]
        public string AmountInvoicing { get; set; }

        [DisplayName("Criado por")]
        public string CreatedByID { get; set; }
        public IList<SelectListItem> LoadCreatedBy { get; set; }

        [DisplayName("Data de Criação")]
        public string CreationDate { get; set; }

        [DisplayName("Modificado por")]
        public string ModifiedByID { get; set; }
        public IList<SelectListItem> LoadModifiedBy { get; set; }
    
        [DisplayName("Data da última modificação")]
        public string LastModifiedDate { get; set; }

        [DisplayName("Início da Vigência")]
        public string StartDate { get; set; }

        [DisplayName("Término da Vigência")]
        public string EndDate { get; set; }
    }
}