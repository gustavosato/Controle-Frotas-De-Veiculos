﻿namespace Lean.Test.Cloud.Domain.Entities.Contracts
{
        public class Contract
        {
            public int contractID { get; set; }
            public string oportunityID { get; set; }
            public string contractTypeID { get; set; }
            public string contractorCustomerID { get; set; }
            public string contractingCustomerID { get; set; }
            public string objectContract { get; set; }
            public string startDate { get; set; }
            public string endDate { get; set; }
            public string periodValidityID { get; set; }
            public string extencionID { get; set; }
            public string extencionPeriodID { get; set; }
            public string resetModalityID { get; set; }
            public string billingCondition { get; set; }
            public string createdByID { get; set; }
            public string creationDate { get; set; }
            public string modifiedByID { get; set; }
            public string lastModifiedDate { get; set; }
        }
    }
