namespace ControleVeiculos.Domain.Entities.SystemParameters
{
    public class SystemParameter
    {
        public int parameterID { get; set; }
        public string paramterName { get; set; }
        public string paramterValue { get; set; }
        public string paramterDefaultValue { get; set; }
        public string createdByID { get; set; }
        public string creationDate { get; set; }
        public string modifiedByID { get; set; }
        public string lastModifiedDate { get; set; } 
    }
}
