using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation.Attributes;
using Lean.Test.Cloud.MVC.Validations.Profiles;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Lean.Test.Cloud.MVC.Models.Profiles
{
    [Validator(typeof(ProfileValidator))]
    public class ProfileModel
    {
        public ProfileModel()
        {
            this.SearchLoadGroup = new List<SelectListItem>();
            this.SearchLoadSystemFeature = new List<SelectListItem>();
            this.LoadGroup = new List<SelectListItem>();
            this.LoadSystemFeature = new List<SelectListItem>();
        }
        //filter
        [DisplayName("Grupo")]
        public string SearchGroupID { get; set; }
        public IList<SelectListItem> SearchLoadGroup { get; set; }

        [DisplayName("Funcionalidade")]
        public string SearchSystemFeatureID { get; set; }
        public IList<SelectListItem> SearchLoadSystemFeature { get; set; }

        [DisplayName("Usuário")]
        public string SearchUserID { get; set; }
        
        //crud
        [Key]
        public int ProfileID { get; set; }

        [DisplayName("Grupo")]
        public string GroupID { get; set; }
        public IList<SelectListItem> LoadGroup { get; set; }

        [DisplayName("Funcionalidade do Sistema")]
        public string SystemFeatureID { get; set; }
        public IList<SelectListItem> LoadSystemFeature { get; set; }

        [DisplayName("Permitir Ver")]
        public bool AllowView { get; set; }

        [DisplayName("Permitir Adicionar")]
        public bool AllowAdd { get; set; }

        [DisplayName("Permitir Atualizar")]
        public bool AllowUpdate { get; set; }

        [DisplayName("Permitir Remover")]
        public bool AllowDelete { get; set; }

        [DisplayName("Permitir Alterar Status")]
        public bool AllowChangeStatus { get; set; }

        [DisplayName("Permitir Adicionar e Remover")]
        public bool AllowAddRemove { get; set; }

        [DisplayName("Permitir Exportar para Excel")]
        public bool AllowExportExcel { get; set; }

        [DisplayName("Permitir Visualizar Relatório")]
        public bool AllowReportView { get; set; }
        
        [DisplayName("ID do Criador")]
        public string CreatedByID { get; set; }

        [DisplayName("Data da Criação")]
        public string CreationDate { get; set; }

        [DisplayName("ID de quem modificador")]
        public string ModifiedByID { get; set; }

        [DisplayName("Última modificação")]
        public string LastModifiedDate { get; set; }
    }
}