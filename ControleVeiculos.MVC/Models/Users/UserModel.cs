using FluentValidation.Attributes;
using ControleVeiculos.MVC.Validations.Users;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace ControleVeiculos.MVC.Models.Users
{
    [Validator(typeof(UserValidator))]
    public class UserModel
    {
        public UserModel()
        {
            this.LoadDepartamentos = new List<SelectListItem>();
            this.LoadStats = new List<SelectListItem>();
        }


        [Key]
        public int UserID { get; set; }

        //consultas
        [DisplayName("Nome do Usuário")]
        public string SearchUserName { get; set; }

        [DisplayName("E-mail do Usuário")]
        public string SearchEmail { get; set; }
        
        [DisplayName("Nome do Departamento")]
        public string SearchDepartamentoID { get; set; }
        public IList<SelectListItem> SearchLoadDepartamentos { get; set; }


        [DisplayName("Nome")]
        public string UserName { get; set; }

        [DisplayName("E-mail")]
        public string Email { get; set; }

        [DisplayName("Senha")]
        public string Password { get; set; }

        [DisplayName("Celular")]
        public string CellNumber { get; set; }
        
        [DisplayName("Departamento")]
        public string DepartamentoID { get; set; }
        public IList<SelectListItem> LoadDepartamentos { get; set; }
        
        [DisplayName("Descrição")]
        public string Description { get; set; }

        [DisplayName("Primerio acesso")]
        public string FirstAccess { get; set; }

        [DisplayName("Administrador")]
        public bool IsAdmin { get; set; }
        
        [DisplayName("Usuário Ativo")]
        public bool IsActive { get; set; }
        
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