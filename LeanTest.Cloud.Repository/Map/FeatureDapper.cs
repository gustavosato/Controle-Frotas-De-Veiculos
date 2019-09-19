﻿using Dapper.Contrib.Extensions;
namespace Lean.Test.Cloud.Repository.Map
{
    [Table("Features")]
    public class FeatureDapper
    {
        [ExplicitKey]
        public int featureID { get; set; }
        public string featureName { get; set; }
        public string statusID { get; set; }
        public string description { get; set; }
        public string applicationSystemID { get; set; }
        public string developerID { get; set; }
        public string featureTypeID { get; set; }
        public string metaScript { get; set; }
        public string automationScript { get; set; }
        public string testPoints { get; set; }
        public string targetDate { get; set; }
        public string timeEffort { get; set; }
        public string createdByID { get; set; }
        public string creationDate { get; set; }
        public string modifiedByID { get; set; }
        public string lastModifiedDate { get; set; } 
    }
}
