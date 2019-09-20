﻿using Dapper.Contrib.Extensions;
namespace ControleVeiculos.Repository.Map
{
    [Table("AnnexContracts")]
    public class AnnexContractDapper
    {
        [ExplicitKey]
        public int annexID  { get; set;}
        public string contractID { get; set;}
        public string oportunityID { get; set; }
        public string summary { get; set; }
        public string annexObject { get; set;}
        public string startDate { get; set; }
        public string endDate { get; set;}
        public string extencionPeriodID { get; set; }
        public string createdByID {get; set;}
        public string creationDate{get; set;}
        public string modifiedByID {get; set;}
        public string lastModifiedDate {get; set;}
    }
}
