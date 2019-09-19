using FluentValidation.Attributes;
using Lean.Test.Cloud.MVC.Validations.Licenses;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Lean.Test.Cloud.MVC.Models.Licenses
{
    [Validator(typeof(LicenseValidator))]
    public class LicenseModel
    {
        public LicenseModel()
        {
            this.LoadSCustomers = new List<SelectListItem>();
            this.LoadApprovedBys = new List<SelectListItem>();
            this.LoadLicenseTypes = new List<SelectListItem>();
            this.LoadCreatedBys = new List<SelectListItem>();
        }

        [Key]
        public int LicenseID { get; set; }

        [DisplayName("Code")]
        public string LicenseCode { get; set; }

        [DisplayName("Descrição")]
        public string Description { get; set; }

        [DisplayName("Licença")]
        public string License { get; set; }

        [DisplayName("Empresa")]
        public string CustomerID { get; set; }
        public IList<SelectListItem> LoadSCustomers { get; set; }

        [DisplayName("Data de Expiração")]
        public string ExpirationDate { get; set; }

        [DisplayName("Tipo de Licença")]
        public string LicenseTypeID { get; set; }
        public IList<SelectListItem> LoadLicenseTypes { get; set; }

        [DisplayName("Nome da Máquina")]
        public string HostName { get; set; }

        [DisplayName("ID de Rede")]
        public string MacAddress { get; set; }

        [DisplayName("Aprovado por")]
        public string ApprovedByID { get; set; }
        public IList<SelectListItem> LoadApprovedBys { get; set; }

        [DisplayName("Data da Aprovação")]
        public string ApprovedDate { get; set; }

        
        [DisplayName("Criado por")]
        public string CreatedByID { get; set; }
        public IList<SelectListItem> LoadCreatedBys { get; set; }

        [DisplayName("Data de criação")]
        public string CreationDate { get; set; }

        [DisplayName("Modificado por")]
        public string ModifiedByID { get; set; }

        [DisplayName("Data da última modificação")]
        public string LastModifiedDate { get; set; }

    }
}