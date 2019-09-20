namespace ControleVeiculos.Domain.Entities.ParameterValues
{
    public class ParameterValue
    {
        public int parameterValueID { get; set; }
        public string parameterValue { get; set; }
        public string systemFeatureID { get; set; }
        public string parameterID { get; set; }
        public string parentID { get; set; }
        public string isSystem { get; set; }
        public string description { get; set; }
        public string createdByID { get; set; }
        public string creationDate { get; set; }
        public string modifiedByID { get; set; }
        public string lastModifiedDate { get; set; } 
    }
}