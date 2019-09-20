namespace ControleVeiculos.Domain.Entities.Parameters
{
    public class Parameter
    {
        public int parameterID { get; set; }
        public string parameterName { get; set; }
        public string systemFeatureID { get; set; }
        public string createdByID { get; set; }
        public string creationDate { get; set; }
        public string modifiedByID { get; set; }
        public string lastModifiedDate { get; set; } 
    }
}
