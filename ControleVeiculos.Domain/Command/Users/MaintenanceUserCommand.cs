namespace Lean.Test.Cloud.Domain.Command.Users
{
    public class MaintenanceUserCommand
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string CellNumber { get; set; }  
        public string FunctionID { get; set; }
        public string FunctionLevelID { get; set; }
        public string LevelClassificationID { get; set; }
        public string DepartmentID { get; set; }
        public string TotalCost { get; set; }
        public string SupervisorID { get; set; }
        public string Description { get; set; }
        public string FirstAccess { get; set; }
        public bool IsAdmin { get; set; }
        public string LastAccessDate { get; set; }
        public string LastIPAccess { get; set; }
        public bool IsActive { get; set; }
        public string AccessToDate { get; set; } 
        public string UpdateRecordTo { get; set; }
        public string ReleaseDateUpdateRecordTo { get; set; }
        public string StartJob { get; set; }
        public string EndJob { get; set; }
        public string ContractTypeID { get; set; }
        public string HourTypeID { get; set; }
        public string RG { get; set; }
        public string CPF { get; set; }
        public string DateOfBirth { get; set; }
        public string HomeAddress { get; set; }
        public string CEP { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string HomePhone { get; set; }
        public string TypeBankAccountID { get; set; }
        public string BankName { get; set; }
        public string TypePersonID { get; set; }
        public string Agency { get; set; }
        public string BankAccount { get; set; }
        public string SocialReason { get; set; }
        public string CNPJ { get; set; }
        public bool OptingSimple { get; set; }
        public bool IsEmployee { get; set; }
        public string RegisteredCity { get; set; }
        public string CreatedByID { get; set; }
        public string CreationDate { get; set; }
        public string ModifiedByID { get; set; }
        public string LastModifiedDate { get; set; }
    }
}
