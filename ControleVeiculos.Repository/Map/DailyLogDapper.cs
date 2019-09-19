﻿using Dapper.Contrib.Extensions;
namespace Lean.Test.Cloud.Repository.Map
{
    [Table("DailyLogs")]
    public class DailyLogDapper
    {
        [ExplicitKey]
        public int dailyLogID { get; set; }
        public string description { get; set; }
        public string demandID { get; set; }
        public bool isInternal { get; set; }
        public string createdByID { get; set; }
        public string creationDate { get; set; }
        public string modifiedByID { get; set; }
        public string lastModifiedDate { get; set; }
    }
}