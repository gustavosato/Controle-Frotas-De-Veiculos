using FluentValidation.Attributes;
using ControleVeiculos.MVC.Validations.Demands;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System;

namespace ControleVeiculos.MVC.Models.Demands
{
    [Validator(typeof(DemandValidator))]
    public class DemandModel
    {

        public DemandModel()
        {
            this.LoadAssingToTarget = new List<SelectListItem>();
            this.LoadServices = new List<SelectListItem>();
            this.LoadStatus = new List<SelectListItem>();
            this.LoadTypes = new List<SelectListItem>();
            this.LoadOportunity = new List<SelectListItem>();
                      
            this.SearchLoadServices = new List<SelectListItem>();
            this.SearchLoadStatus = new List<SelectListItem>();
            this.SearchLoadTypes = new List<SelectListItem>();
            this.SearchLoadAssingToTarget = new List<SelectListItem>();
            this.SearchLoadResponsible = new List<SelectListItem>();
            this.SearchLoadCreateds = new List<SelectListItem>();
        }

        //filter
        [DisplayName("Data inícial")]
        public string SearchStartDateReport { get; set; }

        [DisplayName("Data da Término")]
        public string SearchEndDateReport { get; set; }

        [DisplayName("Nome da Demanda")]
        public string SearchDemandName { get; set; }

        [DisplayName("Código da Demanda")]
        public string SearchDemandCode { get; set; }

        [DisplayName("Código Externo")]
        public string SearchExternalCode { get; set; }

        [DisplayName("Status da Demanda")]
        public string SearchStatusID { get; set; }
        public IList<SelectListItem> SearchLoadStatus { get; set; }

        [DisplayName("Tipo de Serviço")]
        public string SearchServiceID { get; set; }
        public IList<SelectListItem> SearchLoadServices { get; set; }

        [DisplayName("Início Planejado")]
        public string SearchPlanningStartDate { get; set; }

        [DisplayName("Término Planejado")]
        public string SearchPlanningEndDate { get; set; }

        [DisplayName("Tipo de Solicitação")]
        public string SearchTypeID { get; set; }
        public IList<SelectListItem> SearchLoadTypes { get; set; }

        [DisplayName("Reponsável Interno")]
        public string SearchAssignToTargetID { get; set; }
        public IList<SelectListItem> SearchLoadAssingToTarget { get; set; }

        [DisplayName("Responsável Externo")]
        public string SearchResponsibleID { get; set; }
        public IList<SelectListItem> SearchLoadResponsible { get; set; }

        [DisplayName("Código da Ordem de Serviço")]
        public string SearchOportunityID { get; set; }
        public IList<SelectListItem> SearchLoadOportunity { get; set; }

        [DisplayName("Criado Por")]
        public string SearchCreatedByID{ get; set; }
        public IList<SelectListItem> SearchLoadCreateds { get; set; }

        //crud
        [Key]       
        public int DemandID { get; set; }

        [DisplayName("Nome da Demanda")]
        public string DemandName { get; set; }

        [DisplayName("Código Externo")]
        public string ExternalCode { get; set; }

        [DisplayName("Código da Demanda")]
        public string DemandCode { get; set; }

        [DisplayName("Status da Demanda")]
        public string StatusID { get; set; }
        public IList<SelectListItem> LoadStatus { get; set; }

        [DisplayName("Tipo de Solicitação")]
        public string TypeID { get; set; }
        public IList<SelectListItem> LoadTypes { get; set; }

        [DisplayName("Tipo de Serviço")]
        public string ServiceID { get; set; }
        public IList<SelectListItem> LoadServices { get; set; }

        [DisplayName("Responsável Interno")]
        public string AssignToTargetID { get; set; }
        public IList<SelectListItem> LoadAssingToTarget { get; set; }

        [DisplayName("Responsável Externo")]
        public string ResponsibleID { get; set; }
        public IList<SelectListItem> LoadResponsible { get; set; }

        [DisplayName("Código da Ordem de Serviço")]
        public string OportunityID { get; set; }
        public IList<SelectListItem> LoadOportunity { get; set; }

        [DisplayName("Início Planejado")]
        public string PlanningStartDate { get; set; }

        [DisplayName("Término Planejado")]
        public string PlanningEndDate { get; set; }

        [DisplayName("Esforço de Gestão")]
        public string ManagementEffort { get; set; }

        [DisplayName("Esforço Planejamento")]
        public string PlanningEffort { get; set; }

        [DisplayName("Esforço de Execução")]
        public string ExecutionEffort { get; set; }

        [DisplayName("Descrição")]
        public string Descriptions { get; set; }

        [DisplayName("Escopo")]
        public string Scope { get; set; }

        [DisplayName("Esforço Total")]
        public string TotalEffort { get; set; }

        [DisplayName("Horas Apropriadas")]
        public string TotalTime { get; set; }

        [DisplayName("Status de Ativação")]
        public bool IsActive { get; set; }

        [DisplayName("Criado por")]
        public string CreatedByID { get; set; }

        [DisplayName("Data de Criação")]
        public string CreationDate { get; set; }

        public string CustomerID { get; set; }

        public string ModifiedByID { get; set; }

        public string LastModifiedDate { get; set; }
    }
}