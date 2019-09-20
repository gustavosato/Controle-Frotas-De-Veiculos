﻿using Dapper.Contrib.Extensions;
namespace ControleVeiculos.Repository.Map
{
    [Table("PipelineEvents")]
    public class PipelineEventDapper
    {
        [ExplicitKey]
        public int saleEventID { get; set; }
        public string registerDate { get; set; }
        public string typeID { get; set; }
        public string nextStepID { get; set; }
        public string targetDate { get; set; }
        public string description { get; set; }
        public string oportunityID { get; set; }
        public string createdByID { get; set; }
        public string creationDate { get; set; }
        public string modifiedByID { get; set; }
        public string lastModifiedDate { get; set; }
    }
}
