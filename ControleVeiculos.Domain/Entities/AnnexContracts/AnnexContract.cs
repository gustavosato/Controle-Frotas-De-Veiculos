namespace ControleVeiculos.Domain.Entities.AnnexContracts
{
        public class AnnexContract
        {
            public int annexID { get; set; }
            public string contractID { get; set; }
            public string oportunityID { get; set; }
            public string summary { get; set; }
            public string annexObject { get; set; }
            public string startDate { get; set; }
            public string endDate { get; set; }
            public string extencionPeriodID { get; set; }
            public string createdByID { get; set; }
            public string creationDate { get; set; }
            public string modifiedByID { get; set; }
            public string lastModifiedDate { get; set; }
        }
    }
