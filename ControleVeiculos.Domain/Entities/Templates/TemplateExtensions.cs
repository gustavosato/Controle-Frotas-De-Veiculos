using Lean.Test.Cloud.Domain.Command.Templates;
using System;

namespace Lean.Test.Cloud.Domain.Entities.Templates
{
    public static class TemplateExtensions
    {
        public static Result<Template> GetTemplate(this Template template)
        {
            return Result.Ok(0, "", template);
        }

        public static Template Map(this Template template, MaintenanceTemplateCommand command)
        {

            template.templateID = command.TemplateID;
            template.templateName = command.TemplateName;
            template.description = command.Description;
            template.domainID = command.DomainID;
            template.createdByID = command.CreatedByID;
            template.creationDate = command.CreationDate;
            template.modifiedByID = command.ModifiedByID;
            template.lastModifiedDate = command.LastModifiedDate;

            return template;
        }
    }
}
