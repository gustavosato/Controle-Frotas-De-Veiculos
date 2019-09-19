//using Lean.Test.Cloud.Domain;
//using Lean.Test.Cloud.Domain.Command.Contratos;
//using Lean.Test.Cloud.Domain.Entities.Contratos;
//using Lean.Test.Cloud.Domain.Services;
//using Lean.Test.Cloud.Domain.Validations.Contratos;
//using Lean.Test.Cloud.SharedKernel.Common;
//using Lean.Test.Cloud.WebApi.Controllers;
//using Lean.Test.Cloud.WebApi.Infrastrucure;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Threading.Tasks;
//using System.Web;
//using System.Web.Http;

//namespace Lean.Test.Cloud.WebApi.Controllers
//{
//    [RoutePrefix("api/demand")]
//    public class DemandController : BaseController
//    {
//        private readonly IContratoService _contratoService;

//        public DemandController(IContratoService contratoService)
//        {
//            _contratoService = contratoService;
//        }

//        /// <summary>
//        /// Realizar o registro de contrato de financiamento de veiculos do estado do Amazonas.
//        /// </summary>
//        /// <param name="contratoModel">string com o os dados parao registro de contrato.</param>
//        /// <returns></returns>
//        [HttpPost]
//        public async Task<HttpResponseMessage> RegistrarContrato([FromBody]string contratoModel)
//        {
//            if (string.IsNullOrEmpty(contratoModel))
//                throw new ArgumentNullException(nameof(contratoModel));

//            if (!StringUtility.Equal(455, contratoModel.Length))
//                return await CreateResponse(HttpStatusCode.BadRequest, "LAYOUT INVÁLIDO");

//            RegistrarContratoCommand command = Mapper.PrepareRegistrarContratoCommand(contratoModel);

//            Result<Contrato> contrato = _contratoService.RegistrarContrato(command, new ValidationContratoAMFactory());

//            return await CreateResponse(HttpStatusCode.OK, contrato);
//        }

        
//    }
//}