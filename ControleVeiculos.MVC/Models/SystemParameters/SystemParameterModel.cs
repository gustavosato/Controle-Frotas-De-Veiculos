using FluentValidation.Attributes;
using ControleVeiculos.MVC.Validations.SystemParameters;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ControleVeiculos.MVC.Models.SystemParameter
{
    [Validator(typeof(SystemParameterValidator))]
    public class SystemParameterModel
    {
        public SystemParameterModel()
        {
           
        }

        //consultas
        [DisplayName("Nome")]
        public string SearchParamterName { get; set; }

        //crud
        [Key]
        public int ParameterID { get; set; }

        [DisplayName("Nome")]
        public string ParamterName { get; set; }

        [DisplayName("Valor")]
        public string ParamterValue { get; set; }

        [DisplayName("Valor padrão")]
        public string ParamterDefaultValue { get; set; }

        [DisplayName("Criado por")]
        public string CreatedByID { get; set; }

        [DisplayName("Data de criação")]
        public string CreationDate { get; set; }

        [DisplayName("Modificado por")]
        public string ModifiedByID { get; set; }

        [DisplayName("Data da última modificação")]
        public string LastModifiedDate { get; set; }

        
    }
}