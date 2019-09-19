using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Attributes;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Lean.Test.Cloud.WebApi.Models.Defects
{
    public class DefectModel
    {
        public int DefectID { get; set; }
        public string StatusID { get; set; }
        public string SeverityID { get; set; }
        public string PriorityID { get; set; }
        public string AssingToID { get; set; }
        public string TypeID { get; set; }
        public string ApplicationSystemID { get; set; }
        public string FeatureID { get; set; }
        public string ResolutionID { get; set; }
        public string CreatedByID { get; set; }
        public string ModifiedByID { get; set; }
        public string LoadModifiedByID { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string Resolution { get; set; }
        public string ResolutionDate { get; set; }
        public string CreationDate { get; set; }
        public string LastModifiedDate { get; set; }
    }
}