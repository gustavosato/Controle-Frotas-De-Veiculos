using FluentValidation.Attributes;
using ControleVeiculos.MVC.Validations.Pipelines;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ControleVeiculos.MVC.Models.Pipelines
{
    [Validator(typeof(PipelineValidator))]
    public class PipelineModel
    {
        public PipelineModel()
        {
            this.SearchLoadCustomer = new List<SelectListItem>();
            this.SearchLoadPriority = new List<SelectListItem>();
            this.SearchLoadFase = new List<SelectListItem>();
            this.SearchLoadOwner = new List<SelectListItem>();
            this.SearchLoadSaleManager = new List<SelectListItem>();
            this.SearchLoadPreSales = new List<SelectListItem>();
            this.SearchLoadOperationManager = new List<SelectListItem>();
            this.SearchLoadType = new List<SelectListItem>();
            this.SearchLoadCostCenter = new List<SelectListItem>();
            this.SearchLoadOffer = new List<SelectListItem>();
            this.SearchLoadStatus = new List<SelectListItem>();

            this.LoadCustomer = new List<SelectListItem>();
            this.LoadPriority = new List<SelectListItem>();
            this.LoadFase = new List<SelectListItem>();
            this.LoadOwner = new List<SelectListItem>();
            this.LoadSaleManager = new List<SelectListItem>();
            this.LoadPreSales = new List<SelectListItem>();
            this.LoadOperationManager = new List<SelectListItem>();
            this.LoadType = new List<SelectListItem>();
            this.LoadCostCenter = new List<SelectListItem>();
            this.LoadOffer = new List<SelectListItem>();
            this.LoadStatus = new List<SelectListItem>();
            this.LoadFrequencyOfInteractions= new List<SelectListItem>();
            this.LoadApprovedBy = new List<SelectListItem>();


        }

        //Filter

        [DisplayName("Empresa")]
        public string SearchCustomerID { get; set; }
        public IList<SelectListItem> SearchLoadCustomer { get; set; }

        [DisplayName("Prioridade")]
        public string SearchPriorityID { get; set; }
        public IList<SelectListItem> SearchLoadPriority { get; set; }

        [DisplayName("Fase")]
        public string SearchFaseID { get; set; }
        public IList<SelectListItem> SearchLoadFase { get; set; }

        [DisplayName("Proprietário")]
        public string SearchOwnerID { get; set; }
        public IList<SelectListItem> SearchLoadOwner { get; set; }

        [DisplayName("Gerente de Vendas")]
        public string SearchSaleManagerID { get; set; }
        public IList<SelectListItem> SearchLoadSaleManager { get; set; }

        [DisplayName("Pré Vendas")]
        public string SearchPreSalesID { get; set; }
        public IList<SelectListItem> SearchLoadPreSales { get; set; }

        [DisplayName("Gerente de Operações")]
        public string SearchOperationManagerID { get; set; }
        public IList<SelectListItem> SearchLoadOperationManager { get; set; }

        [DisplayName("Tipo")]
        public string SearchTypeID { get; set; }
        public IList<SelectListItem> SearchLoadType { get; set; }

        [DisplayName("Centro de Custo")]
        public string SearchCostCenterID { get; set; }
        public IList<SelectListItem> SearchLoadCostCenter { get; set; }

        [DisplayName("Oferta")]
        public string SearchOfferID { get; set; }
        public IList<SelectListItem> SearchLoadOffer { get; set; }

        [DisplayName("Status")]
        public string SearchStatusID { get; set; }
        public IList<SelectListItem> SearchLoadStatus { get; set; }

        //Crud
        [Key]
        public int OportunityID { get; set; }

        [DisplayName("Empresa")]
        public string CustomerID { get; set; }
        public IList<SelectListItem> LoadCustomer { get; set; }

        [DisplayName("Prioridade")]
        public string PriorityID { get; set; }
        public IList<SelectListItem> LoadPriority { get; set; }

        [DisplayName("Fase")]
        public string FaseID { get; set; }
        public IList<SelectListItem> LoadFase { get; set; }

        [DisplayName("Proprietário")]
        public string OwnerID { get; set; }
        public IList<SelectListItem> LoadOwner { get; set; }

        [DisplayName("Gerente de Vendas")]
        public string SaleManagerID { get; set; }
        public IList<SelectListItem> LoadSaleManager { get; set; }

        [DisplayName("Pré Venda")]
        public string PreSalesID { get; set; }
        public IList<SelectListItem> LoadPreSales { get; set; }

        [DisplayName("Gerente de Operações")]
        public string OperationManagerID { get; set; }
        public IList<SelectListItem> LoadOperationManager { get; set; }

        [DisplayName("Tipo")]
        public string TypeID { get; set; }
        public IList<SelectListItem> LoadType { get; set; }

        [DisplayName("Centro de Custo")]
        public string CostCenterID { get; set; }
        public IList<SelectListItem> LoadCostCenter { get; set; }

        [DisplayName("Oferta")]
        public string OfferID { get; set; }
        public IList<SelectListItem> LoadOffer { get; set; }

        [DisplayName("Status")]
        public string StatusID { get; set; }
        public IList<SelectListItem> LoadStatus { get; set; }

        [DisplayName("Código da Oportunidade")]
        public string OportunityCode { get; set; }

        [DisplayName("Descrição")]
        public string Description { get; set; }

        [DisplayName("Sumário")]
        public string Summary { get; set; }

        [DisplayName("Sponsor")]
        public string Sponsor { get; set; }

        [DisplayName("Power Sponsor")]
        public string PowerSponsor { get; set; }

        [DisplayName("Valor Esperado")]
        public string ExpectedValue { get; set; }

        [DisplayName("Data Limite")]
        public string TargetDate { get; set; }

        [DisplayName("Probabilidade")]
        public string Probability { get; set; }

        [DisplayName("Valor Fechado")]
        public string Billed { get; set; }

        [DisplayName("Comentários")]
        public string Comments { get; set; }
        
        [DisplayName("Criado")]
        public string CreatedByID { get; set; }
        public IList<SelectListItem> LoadCreatedBy { get; set; }

        [DisplayName("Data de Criação")]
        public string CreationDate { get; set; }

        [DisplayName("Modificado")]
        public string ModifiedByID { get; set; }
        public IList<SelectListItem> LoadModifiedBy { get; set; }

        [DisplayName("Última Modificação")]
        public string LastModifiedDate { get; set; }

        [DisplayName("Data de Fechamento")]
        public string ClosingDate { get; set; }

        [DisplayName("Frequência das Interações")]
        public string FrequencyOfInteractionID { get; set; }
        public IList<SelectListItem> LoadFrequencyOfInteractions { get; set; }

        [DisplayName("Aprovado por")]
        public string ApprovedByID { get; set; }
        public IList<SelectListItem> LoadApprovedBy { get; set; }

        [DisplayName("Data de Aprovação")]
        public string ApprovedDate { get; set; }

        [DisplayName("1º Trimestre")]
        public string Quarter1 { get; set; }

        [DisplayName("2º Trimestre")]
        public string Quarter2 { get; set; }

        [DisplayName("3º Trimestre")]
        public string Quarter3 { get; set; }

        [DisplayName("4º Trimestre")]
        public string Quarter4 { get; set; }

    }
}