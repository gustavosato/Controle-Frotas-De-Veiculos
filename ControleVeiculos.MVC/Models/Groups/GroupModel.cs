using FluentValidation.Attributes;
using ControleVeiculos.MVC.Validations.Groups;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ControleVeiculos.MVC.Models.Groups
{
    [Validator(typeof(GroupValidator))]
    public class GroupModel
    {
        public GroupModel()
        {
            
        }

        //Filter

        [DisplayName("Nome do Grupo")]
        public string SearchGroupName { get; set; }

        //Crud

        [Key]
        public int GroupID { get; set; }

        [DisplayName("Nome do Grupo")]
        public string GroupName { get; set; }

        [DisplayName("Sistema")]
        public bool IsSystem { get; set; }

        [DisplayName("Domínio")]
        public string DomainID { get; set; }
        public IList<SelectListItem> LoadDomain { get; set; }
        
        [DisplayName("Descrição")]
        public string Description { get; set; }

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

        public string UserID { get; set; }


    }
}