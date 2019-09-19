﻿using Lean.Test.Cloud.Domain.Entities.AnnexContracts;
using Lean.Test.Cloud.MVC.Models.AnnexContracts;

namespace Lean.Test.Cloud.MVC.Extensions
{
    public static class AnnexContractMappingExtensions
    {
        public static AnnexContractModel ToModel(this AnnexContract entity)
        {
            if (entity == null)
                return null;

            var model = new AnnexContractModel
            {
                AnnexID = entity.annexID,
                ContractID = entity.contractID,
                OportunityID = entity.oportunityID,
                AnnexObject = entity.annexObject,
                Summary = entity.summary,
                StartDate = entity.startDate,
                EndDate = entity.endDate,
                ExtencionPeriodID = entity.extencionPeriodID,
                CreatedByID = entity.createdByID,
                CreationDate = entity.creationDate,
                ModifiedByID = entity.modifiedByID,
                LastModifiedDate = entity.lastModifiedDate
            };

            return model;
        }
    }
}