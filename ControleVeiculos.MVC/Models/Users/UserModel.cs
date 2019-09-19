using FluentValidation.Attributes;
using Lean.Test.Cloud.MVC.Validations.Users;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Lean.Test.Cloud.MVC.Models.Users
{
    [Validator(typeof(UserValidator))]
    public class UserModel
    {
        public UserModel()
        {
            this.LoadFunctions = new List<SelectListItem>();
            this.LoadCustomers = new List<SelectListItem>();
            this.LoadContractTypes = new List<SelectListItem>();
            this.LoadDepartments = new List<SelectListItem>();
            this.LoadFunctionLevels = new List<SelectListItem>();
            this.LoadHourTypes = new List<SelectListItem>();
            this.LoadLevelClassifications = new List<SelectListItem>();
            this.LoadSupervisors = new List<SelectListItem>();
            this.LoadStats = new List<SelectListItem>();
            this.LoadTypeBankAccounts = new List<SelectListItem>();
            this.LoadTypePersons = new List<SelectListItem>();
        }


        [Key]
        public int UserID { get; set; }

        //consultas
        [DisplayName("Nome do Usuário")]
        public string SearchUserName { get; set; }

        [DisplayName("E-mail do Usuário")]
        public string SearchEmail { get; set; }

        [DisplayName("Função")]
        public string SearchFunctionID { get; set; }
        public IList<SelectListItem> SearchLoadFunctions { get; set; }

        [DisplayName("Nome do Departamento")]
        public string SearchDepartmentID { get; set; }
        public IList<SelectListItem> SearchLoadDepartments { get; set; }


        [DisplayName("Nome")]
        public string UserName { get; set; }

        [DisplayName("E-mail")]
        public string Email { get; set; }

        [DisplayName("Senha")]
        public string Password { get; set; }

        [DisplayName("Celular")]
        public string CellNumber { get; set; }
        
        [DisplayName("Função")]
        public string FunctionID { get; set; }
        public IList<SelectListItem> LoadFunctions { get; set; }

        [DisplayName("Nível")]
        public string FunctionLevelID { get; set; }
        public IList<SelectListItem> LoadFunctionLevels { get; set; }

        [DisplayName("Classificação")]
        public string LevelClassificationID { get; set; }
        public IList<SelectListItem> LoadLevelClassifications { get; set; }

        [DisplayName("Departamento")]
        public string DepartmentID { get; set; }
        public IList<SelectListItem> LoadDepartments { get; set; }

        [DisplayName("Custo Total")]
        public string TotalCost { get; set; }

        [DisplayName("Supervisor")]
        public string SupervisorID { get; set; }
        public IList<SelectListItem> LoadSupervisors { get; set; }

        [DisplayName("Descrição")]
        public string Description { get; set; }

        [DisplayName("Primerio acesso")]
        public string FirstAccess { get; set; }

        [DisplayName("Administrador")]
        public bool IsAdmin { get; set; }

        [DisplayName("Funcionário")]
        public bool IsEmployee{ get; set; }

        [DisplayName("Data do Último Acesso")]
        public string LastAccessDate { get; set; }

        [DisplayName("IP do Último Acesso")]
        public string LastIPAccess { get; set; }

        [DisplayName("Usuário Ativo")]
        public bool IsActive { get; set; }

        [DisplayName("Data Limite de Acesso")]
        public string AccessToDate { get; set; }

        [DisplayName("Data Retroativa Limite")]
        public string UpdateRecordTo { get; set; }

        [DisplayName("Validade da Liberação")]
        public string ReleaseDateUpdateRecordTo { get; set; }

        [DisplayName("Data da Contratação")]
        public string StartJob { get; set; }

        [DisplayName("Data do Desligamento")]
        public string EndJob { get; set; }

        [DisplayName("Tipo de Contrato")]
        public string ContractTypeID { get; set; }
        public IList<SelectListItem> LoadContractTypes { get; set; }

        [DisplayName("Tipo de Hora")]
        public string HourTypeID { get; set; }
        public IList<SelectListItem>LoadHourTypes { get; set; }

        [DisplayName("Empresa")]
        public string CustomerID { get; set; }
        public IList<SelectListItem> LoadCustomers { get; set; }

        [DisplayName("Grupo")]
        public string GroupID { get; set; }
        public IList<SelectListItem> LoadGroups { get; set; }

        [DisplayName("Criado por")]
        public string CreatedByID { get; set; }

        [DisplayName("Data de criação")]
        public string CreationDate { get; set; }

        [DisplayName("Modificado por")]
        public string ModifiedByID { get; set; }

        [DisplayName("Data da Última Modificação")]
        public string LastModifiedDate { get; set; }

        [DisplayName("Numero do RG")]
        public string RG { get; set; }

        [DisplayName("CPF")]
        public string CPF { get; set; }

        [DisplayName("Data de Nascimento")]
        public string DateOfBirth { get; set; }

        [DisplayName("Endereço Residêncial")]
        public string HomeAddress { get; set; }

        [DisplayName("CEP")]
        public string CEP { get; set; }

        [DisplayName("Bairro")]
        public string District { get; set; }

        [DisplayName("Cidade")]
        public string City { get; set; }

        [DisplayName("Estado")]
        public string State { get; set; }
        public IList<SelectListItem> LoadStats{ get; set; }


        [DisplayName("Telefone Residêncial")]
        public string HomePhone { get; set; }

        [DisplayName("Tipo de Conta Bancária")]
        public string TypeBankAccount { get; set; }
        public IList<SelectListItem> LoadTypeBankAccounts { get; set; }

        [DisplayName("Nome do Banco")]
        public string BankName { get; set; }

        [DisplayName("Tipo de Pessoa")]
        public string TypePerson { get; set; }
        public IList<SelectListItem> LoadTypePersons { get; set; }

        [DisplayName("Agência")]
        public string Agency { get; set; }

        [DisplayName("Número da Conta Bancária")]
        public string BankAccount { get; set; }

        [DisplayName("Razão Social")]
        public string SocialReason { get; set; }

        [DisplayName("CNPJ")]
        public string CNPJ { get; set; }

        [DisplayName("Optante pelo Simples")]
        public bool OptingSimple { get; set; }

        [DisplayName("Cidade de Registro")]
        public string RegisteredCity { get; set; }

        public string DemandID { get; set; }

        //change password
        [DisplayName("Senha")]
        public string PasswordNew { get; set; }

        [DisplayName("Confirmar Nova Senha")]
        public string PasswordNewConfirm { get; set; }

        [DisplayName("E-mail")]
        public string EmailNew { get; set; }

        [DisplayName("Confirmar E-mail")]
        public string EmailNewConfirm { get; set; }

    }
}