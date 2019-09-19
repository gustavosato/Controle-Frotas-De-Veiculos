using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using Lean.Test.Cloud.Domain.Services;
using Lean.Test.Cloud.Domain.Command.Defects;
using Lean.Test.Cloud.Domain.Entities.Defects;
using Lean.Test.Cloud.Domain;
using Lean.Test.Cloud.WebApi.Models.Defects;
using Lean.Test.Cloud.WebApi.Auth;

namespace Lean.Test.Cloud.WebApi.Controllers
{
    //[Authorize]
    public class DefectController : ApiController
    {
        private readonly IDefectService _defectService;

        public DefectController(IDefectService defectService)
        {
            _defectService = defectService;
        }

        // GET: api/Defect
        [JwtAuthentication]
        public List<Defect> Get()
        {
            var defects = _defectService.ApiGetAll();

            return defects;
        }

        // GET: api/Defect/5
        //[JwtAuthentication]
        public Result<Defect> GetbyID(int id)
        {
            //var model = new DefectModel();

            Result<Defect> defect = _defectService.GetByID(id);

            return defect;
        }

        // POST: api/Defect
        //Request:
        //{
        //    "StatusID":"", 
        //    "SeverityID":"", 
        //    "PriorityID":"", 
        //    "AssingToID":"", 
        //    "TypeID":"", 
        //    "ApplicationSystemID":"", 
        //    "FeatureID":"", 
        //    "ResolutionID":"", 
        //    "CreatedByID":"", 
        //    "ModifiedByID":"", 
        //    "LoadModifiedByID":"", 
        //    "Summary":"", 
        //    "Description":"", 
        //    "Resolution":"", 
        //    "ResolutionDate":"", 
        //}
        //[JwtAuthentication]
        public string Post(DefectModel model)
        {
            if (ModelState.IsValid)
            {
                var command = MaintenanceDefectCommand(model);

                string recordID = _defectService.Add(command);

                return recordID;
            }
            else
            {
                return "erro";
            }
        }

        // PUT: api/Defect/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Defect/5
        public void Delete(int id)
        {
        }

        private MaintenanceDefectCommand MaintenanceDefectCommand(DefectModel model)
        {
            MaintenanceDefectCommand command = new MaintenanceDefectCommand();

            command.DefectID = model.DefectID;
            command.Summary = model.Summary;
            command.Description = model.Description;
            command.StatusID = model.StatusID;
            command.SeverityID = model.SeverityID;
            command.PriorityID = model.PriorityID;
            command.AssingToID = model.AssingToID;
            command.TypeID = model.TypeID;
            command.ResolutionID = model.ResolutionID;
            command.Resolution = model.Resolution;
            command.ResolutionDate = model.ResolutionDate;
            command.ApplicationSystemID = model.ApplicationSystemID;
            command.FeatureID = model.FeatureID;
            command.CreatedByID = model.CreatedByID;
            command.CreationDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            command.ModifiedByID = model.ModifiedByID;
            command.LastModifiedDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            return command;
        }
    }
}
