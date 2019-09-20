namespace ControleVeiculos.Domain.Command.Skills
{
    public class MaintenanceSkillCommand
    {
        public int SkillID { get; set; }
        public string Summary { get; set; }
        public string SkillTypeID { get; set; }
        public string Description { get; set; }
        public string CreatedByID { get; set; }
        public string CreationDate { get; set; }
        public string ModifiedByID { get; set; }
        public string LastModifiedDate { get; set; }
    }
}


   