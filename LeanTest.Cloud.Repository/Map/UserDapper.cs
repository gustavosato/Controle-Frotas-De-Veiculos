using Dapper.Contrib.Extensions;

namespace Lean.Test.Cloud.Repository.Map
{
    [Table("Users")]
    public class UserDapper
    {
        [ExplicitKey]
        public int userID { get; set; }
        public string userName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string cellNumber { get; set; }
        public string functionID { get; set; }
        public string functionLevelID { get; set; }
        public string levelClassificationID { get; set; }
        public string departmentID { get; set; }
        public string totalCost { get; set; }
        public string supervisorID { get; set; }
        public string description { get; set; }
        public string firstAccess { get; set; }
        public bool isAdmin { get; set; }
        public string lastAccessDate { get; set; }
        public string lastIPAccess { get; set; }
        public bool isActive { get; set; }
        public string accessToDate { get; set; }
        public string updateRecordTo { get; set; }
        public string releaseDateUpdateRecordTo { get; set; } 
        public string startJob { get; set; }
        public string endJob { get; set; }
        public string contractTypeID { get; set; }
        public string hourTypeID { get; set; }
        public string rg { get; set; }
        public string cpf { get; set; }
        public string dateOfBirth { get; set; }
        public string homeAddress { get; set; }
        public string cep { get; set; }
        public string district { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string homePhone { get; set; }
        public string typeBankAccountID { get; set; }
        public string bankName { get; set; }
        public string typePersonID { get; set; }
        public string agency { get; set; }
        public string bankAccount { get; set; }
        public string socialReason { get; set; }
        public string cnpj { get; set; }
        public bool optingSimple { get; set; }
        public bool isEmployee { get; set; }
        public string registeredCity { get; set; }
        public string createdByID { get; set; }
        public string creationDate { get; set; }
        public string modifiedByID { get; set; }
        public string lastModifiedDate { get; set; }
    }
}
