using FluentValidation.Attributes;
using Lean.Test.Cloud.MVC.Validations.Attachments;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Lean.Test.Cloud.MVC.Models.Attachments
{
    [Validator(typeof(AttachmentValidator))]
    public class AttachmentModel
    {
        public AttachmentModel()
        {
            this.SearchLoadSystemFeature = new List<SelectListItem>();
            this.LoadSystemFeature = new List<SelectListItem>();
        }

        //filter
        [DisplayName("Nome")]
        public string SearchFileName { get; set; }
        public string SearchDescription { get; set; }
        public string SearchRecordID { get; set; }

        [DisplayName("Criado por")]
        public string SearchCreatedByID { get; set; }
        public IList<SelectListItem> SearchLoadCreateds { get; set; }

        [DisplayName("Funcionalidade")]
        public string SearchSystemFeatureID { get; set; }
        public IList<SelectListItem> SearchLoadSystemFeature { get; set; }

        //crud
        [Key]
        public int AttachmentID { get; set; }

        [DisplayName("Nome do Arquivo")]
        public string FileName { get; set; }

        [DisplayName("Descrição")]
        public string Description { get; set; }

        [DisplayName("Caminho do arquivo")]
        public string PathFile { get; set; }

        [DisplayName("Tamanho do arquivo")]
        public string SizeFile { get; set; }

        [DisplayName("ID do Registro")]
        public string RecordID { get; set; }

        [DisplayName("Funcionalidade")]
        public string SystemFeatureID { get; set; }
        public IList<SelectListItem> LoadSystemFeature { get; set; }

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