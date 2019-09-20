using ControleVeiculos.Domain.Entities.Users;
using ControleVeiculos.Domain.Entities.Demands;
using ControleVeiculos.Domain.Entities.ParameterValues;
using ControleVeiculos.Domain.Entities.ApplicationSystems;
using ControleVeiculos.Domain.Entities.Attachments;
using ControleVeiculos.Domain.Entities.AccountingEntries;
using ControleVeiculos.Domain.Entities.ChangeRequests;
using ControleVeiculos.Domain.Entities.DailyLogComments;
using ControleVeiculos.Domain.Entities.DailyLogs;
using ControleVeiculos.Domain.Entities.DemandsUsers;
using ControleVeiculos.Domain.Entities.Customers;
using ControleVeiculos.Domain.Entities.CustomersUsers;
using ControleVeiculos.Domain.Entities.Elements;
using ControleVeiculos.Domain.Entities.SystemFeatures;
using ControleVeiculos.Domain.Entities.Expenses;
using ControleVeiculos.Domain.Entities.Features;
using ControleVeiculos.Domain.Entities.Groups;
using ControleVeiculos.Domain.Entities.Historicals;
using ControleVeiculos.Domain.Entities.Parameters;
using ControleVeiculos.Domain.Entities.Profiles;
using ControleVeiculos.Domain.Entities.SystemMenus;
using ControleVeiculos.Domain.Entities.SystemParameters;
using ControleVeiculos.Domain.Entities.Tasks;
using ControleVeiculos.Domain.Entities.Templates;
using ControleVeiculos.Domain.Entities.TestCases;
using ControleVeiculos.Domain.Entities.TestLogs;
using ControleVeiculos.Domain.Entities.TestPackages;
using ControleVeiculos.Domain.Entities.TestScenarios;
using ControleVeiculos.Domain.Entities.TestScenarioFeatures;
using ControleVeiculos.Domain.Entities.TimeReleases;
using ControleVeiculos.Domain.Entities.GroupsUsers;
using ControleVeiculos.Domain.Entities.Workflows;
using ControleVeiculos.Domain.Entities.Licenses;
using ControleVeiculos.Domain.Entities.EquipmentAccessories;
using ControleVeiculos.Domain.Entities.Issues;
using ControleVeiculos.Domain.Entities.Defects;
using ControleVeiculos.Domain.Entities.MovimentEmployees;
using ControleVeiculos.Domain.Entities.Pipelines;
using ControleVeiculos.Domain.Entities.Resumes;
using ControleVeiculos.Domain.Entities.PipelineEvents;
using ControleVeiculos.Domain.Entities.Vacancies;
using ControleVeiculos.Domain.Entities.Contracts;
using ControleVeiculos.Domain.Entities.AnnexContracts;
using ControleVeiculos.Domain.Entities.ContractAdditives;
using ControleVeiculos.Domain.Entities.Contacts;
using ControleVeiculos.Domain.Entities.PositionsSalaries;
using ControleVeiculos.Domain.Entities.Skills;
using ControleVeiculos.Domain.Entities.Dashboards;
using ControleVeiculos.Domain.Entities.Supports;
using ControleVeiculos.Domain.Entities.VacanciesResumes;
using ControleVeiculos.Domain.Entities.ResumeVacancies;

namespace ControleVeiculos.Repository.Map
{
    public static class DapperUtils
    {
        public static DemandDapper Map(this Demand demand, int primaryKey)
        {
            DemandDapper demandDapper = new DemandDapper();

            demandDapper.demandID = primaryKey;
            demandDapper.demandName = demand.demandName;
            demandDapper.typeID = demand.typeID;
            demandDapper.statusID = demand.statusID;
            demandDapper.scope = demand.scope;
            demandDapper.externalCode = demand.externalCode;
            demandDapper.demandCode = demand.demandCode;
            demandDapper.responsibleID = demand.responsibleID;
            demandDapper.assignToTargetID = demand.assignToTargetID;
            demandDapper.planningStartDate = demand.planningStartDate;
            demandDapper.planningEndDate = demand.planningEndDate;
            demandDapper.managementEffort = demand.managementEffort;
            demandDapper.planningEffort = demand.planningEffort;
            demandDapper.executionEffort = demand.executionEffort;
            demandDapper.description = demand.description;
            demandDapper.customerID = demand.customerID;
            demandDapper.serviceID = demand.serviceID;
            demandDapper.oportunityID = demand.oportunityID;
            demandDapper.isActive = demand.isActive;
            demandDapper.creationDate = demand.creationDate;
            demandDapper.createdByID = demand.createdByID;
            demandDapper.lastModifiedDate = demand.lastModifiedDate;
            demandDapper.modifiedByID = demand.modifiedByID;

            return demandDapper;
        }

        public static PositionsSalarieDapper Map(this PositionsSalarie positionsSalarie, int primaryKey)
        {
            PositionsSalarieDapper positionsSalarieDapper = new PositionsSalarieDapper();

            positionsSalarieDapper.positionsSalarieID = primaryKey;
            positionsSalarieDapper.functionID = positionsSalarie.functionID;
            positionsSalarieDapper.levelID = positionsSalarie.levelID;
            positionsSalarieDapper.classificationID = positionsSalarie.classificationID;
            positionsSalarieDapper.amountPJ = positionsSalarie.amountPJ;
            positionsSalarieDapper.amountCLT = positionsSalarie.amountCLT;
            positionsSalarieDapper.amountCLTFLEX = positionsSalarie.amountCLTFLEX;
            positionsSalarieDapper.creationDate = positionsSalarie.creationDate;
            positionsSalarieDapper.createdByID = positionsSalarie.createdByID;
            positionsSalarieDapper.lastModifiedDate = positionsSalarie.lastModifiedDate;
            positionsSalarieDapper.modifiedByID = positionsSalarie.modifiedByID;
            positionsSalarieDapper.startingDate = positionsSalarie.startingDate;
            positionsSalarieDapper.closingDate = positionsSalarie.closingDate;



            return positionsSalarieDapper;
        }

        public static ParameterValueDapper Map(this ParameterValue parameterValue, int primaryKey)
        {
            ParameterValueDapper parameterValueDapper = new ParameterValueDapper();

            parameterValueDapper.parameterValueID = primaryKey;
            parameterValueDapper.parameterValue = parameterValue.parameterValue;
            parameterValueDapper.parameterID = parameterValue.parameterID;
            parameterValueDapper.parentID = parameterValue.parentID;
            parameterValueDapper.isSystem = parameterValue.isSystem;
            parameterValueDapper.description = parameterValue.description;
            parameterValueDapper.creationDate = parameterValue.creationDate;
            parameterValueDapper.createdByID = parameterValue.createdByID;
            parameterValueDapper.lastModifiedDate = parameterValue.lastModifiedDate;
            parameterValueDapper.modifiedByID = parameterValue.modifiedByID;

            return parameterValueDapper;
        }

        public static UserDapper Map(this User user, int primaryKey)
        {
            UserDapper userDapper = new UserDapper();

            userDapper.userID = primaryKey;
            userDapper.userName = user.userName;
            userDapper.email = user.email;
            userDapper.password = user.password;
            userDapper.cellNumber = user.cellNumber;
            userDapper.functionID = user.functionID;
            userDapper.functionLevelID = user.functionLevelID;
            userDapper.levelClassificationID = user.levelClassificationID;
            userDapper.departmentID = user.departmentID;
            userDapper.totalCost = user.totalCost;
            userDapper.supervisorID = user.supervisorID;
            userDapper.description = user.description;
            userDapper.firstAccess = user.firstAccess;
            userDapper.isAdmin = user.isAdmin;
            userDapper.lastAccessDate = user.lastAccessDate;
            userDapper.lastIPAccess = user.lastIPAccess;
            userDapper.isActive = user.isActive;
            userDapper.accessToDate = user.accessToDate;
            userDapper.updateRecordTo = user.updateRecordTo;
            userDapper.releaseDateUpdateRecordTo = user.releaseDateUpdateRecordTo;
            userDapper.startJob = user.startJob;
            userDapper.endJob = user.endJob;
            userDapper.contractTypeID = user.contractTypeID;
            userDapper.hourTypeID = user.hourTypeID;
            userDapper.rg = user.rg;
            userDapper.cpf = user.cpf;
            userDapper.dateOfBirth = user.dateOfBirth;
            userDapper.homeAddress = user.homeAddress;
            userDapper.cep = user.cep;
            userDapper.district = user.district;
            userDapper.city = user.city;
            userDapper.state = user.state;
            userDapper.homePhone = user.homePhone;
            userDapper.typeBankAccountID = user.typeBankAccountID;
            userDapper.typePersonID = user.typePersonID;
            userDapper.agency = user.agency;
            userDapper.bankAccount = user.bankAccount;
            userDapper.bankName = user.bankName;
            userDapper.socialReason = user.socialReason;
            userDapper.cnpj = user.cnpj;
            userDapper.optingSimple = user.optingSimple;
            userDapper.registeredCity = user.registeredCity;
            userDapper.isEmployee = user.isEmployee;
            userDapper.createdByID = user.createdByID;
            userDapper.creationDate = user.creationDate;
            userDapper.modifiedByID = user.modifiedByID;
            userDapper.lastModifiedDate = user.lastModifiedDate;

            return userDapper;
        }

        public static ApplicationSystemDapper Map(this ApplicationSystem applicationSystem, int primaryKey)
        {
            ApplicationSystemDapper aplicationSystemDapper = new ApplicationSystemDapper();

            aplicationSystemDapper.applicationSystemID = primaryKey;
            aplicationSystemDapper.applicationSystemName = applicationSystem.applicationSystemName;
            aplicationSystemDapper.description = applicationSystem.description;
            aplicationSystemDapper.applicationTypeID = applicationSystem.applicationTypeID;
            aplicationSystemDapper.customerID = applicationSystem.customerID;
            aplicationSystemDapper.createdByID = applicationSystem.createdByID;
            aplicationSystemDapper.creationDate = applicationSystem.creationDate;
            aplicationSystemDapper.lastModifiedDate = applicationSystem.lastModifiedDate;
            aplicationSystemDapper.modifiedByID = applicationSystem.modifiedByID;

            return aplicationSystemDapper;

        }

        public static AttachmentDapper Map(this Attachment attachment, int primaryKey)
        {
            AttachmentDapper attachmentDapper = new AttachmentDapper();

            attachmentDapper.attachmentID = primaryKey;
            attachmentDapper.fileName = attachment.fileName;
            attachmentDapper.description = attachment.description;
            attachmentDapper.binaryFile = attachment.binaryFile;
            attachmentDapper.pathFile = attachment.pathFile;
            attachmentDapper.sizeFile = attachment.sizeFile;
            attachmentDapper.recordID = attachment.recordID;
            attachmentDapper.systemFeatureID = attachment.systemFeatureID;
            attachmentDapper.createdByID = attachment.createdByID;
            attachmentDapper.creationDate = attachment.creationDate;
            attachmentDapper.lastModifiedDate = attachment.lastModifiedDate;
            attachmentDapper.modifiedByID = attachment.modifiedByID;

            return attachmentDapper;
        }

        public static AccountingEntrieDapper Map(this AccountingEntrie accountingEntrie, int primaryKey)
        {
            AccountingEntrieDapper accountingEntrieDapper = new AccountingEntrieDapper();

            accountingEntrieDapper.accountingEntrieID = primaryKey;
            accountingEntrieDapper.classID = accountingEntrie.classID;
            accountingEntrieDapper.categoryID = accountingEntrie.categoryID;
            accountingEntrieDapper.subCategoryID = accountingEntrie.subCategoryID;
            accountingEntrieDapper.accountID = accountingEntrie.accountID;
            accountingEntrieDapper.statusID = accountingEntrie.statusID;
            accountingEntrieDapper.valueToBeRealized = accountingEntrie.valueToBeRealized;
            accountingEntrieDapper.competitionDate = accountingEntrie.competitionDate;
            accountingEntrieDapper.realizedValue = accountingEntrie.realizedValue;
            accountingEntrieDapper.dueDate = accountingEntrie.dueDate;
            accountingEntrieDapper.interest = accountingEntrie.interest;
            accountingEntrieDapper.invoiceNumber = accountingEntrie.invoiceNumber;
            accountingEntrieDapper.documentNumber = accountingEntrie.documentNumber;
            accountingEntrieDapper.customerID = accountingEntrie.customerID;
            accountingEntrieDapper.description = accountingEntrie.description;
            accountingEntrieDapper.demandID = accountingEntrie.demandID;
            accountingEntrieDapper.employeeID = accountingEntrie.employeeID;
            accountingEntrieDapper.createdByID = accountingEntrie.createdByID;
            accountingEntrieDapper.creationDate = accountingEntrie.creationDate;
            accountingEntrieDapper.lastModifiedDate = accountingEntrie.lastModifiedDate;
            accountingEntrieDapper.modifiedByID = accountingEntrie.modifiedByID;

            return accountingEntrieDapper;

        }

        public static ChangeRequestDapper Map(this ChangeRequest changeRequest, int primaryKey)
        {
            ChangeRequestDapper changeRequestDapper = new ChangeRequestDapper();

            changeRequestDapper.changeRequestID = primaryKey;
            changeRequestDapper.summary = changeRequest.summary;
            changeRequestDapper.managementEffort = changeRequest.managementEffort;
            changeRequestDapper.planningEffort = changeRequest.planningEffort;
            changeRequestDapper.executionEffort = changeRequest.executionEffort;
            changeRequestDapper.statusID = changeRequest.statusID;
            changeRequestDapper.targetDate = changeRequest.targetDate;
            changeRequestDapper.approvedDate = changeRequest.approvedDate;
            changeRequestDapper.description = changeRequest.description;
            changeRequestDapper.demandID = changeRequest.demandID;
            changeRequestDapper.requestByID = changeRequest.requestByID;
            changeRequestDapper.createdByID = changeRequest.createdByID;
            changeRequestDapper.creationDate = changeRequest.creationDate;
            changeRequestDapper.lastModifiedDate = changeRequest.lastModifiedDate;
            changeRequestDapper.modifiedByID = changeRequest.modifiedByID;

            return changeRequestDapper;
        }

        public static DailyLogCommentDapper Map(this DailyLogComment dailyLogComment, int primaryKey)
        {
            DailyLogCommentDapper dailyLogCommentDapper = new DailyLogCommentDapper();

            dailyLogCommentDapper.dailyLogsCommentID = primaryKey;
            dailyLogCommentDapper.descrition = dailyLogComment.descrition;
            dailyLogCommentDapper.createdByID = dailyLogComment.createdByID;
            dailyLogCommentDapper.creationDate = dailyLogComment.creationDate;
            dailyLogCommentDapper.lastModifiedDate = dailyLogComment.lastModifiedDate;
            dailyLogCommentDapper.modifiedByID = dailyLogComment.modifiedByID;

            return dailyLogCommentDapper;
        }

        public static DailyLogDapper Map(this DailyLog dailyLog, int primaryKey)

        {
            DailyLogDapper dailyLogDapper = new DailyLogDapper();

            dailyLogDapper.dailyLogID = primaryKey;
            dailyLogDapper.description = dailyLog.description;
            dailyLogDapper.demandID = dailyLog.demandID;
            dailyLogDapper.isInternal = dailyLog.isInternal;
            dailyLogDapper.createdByID = dailyLog.createdByID;
            dailyLogDapper.creationDate = dailyLog.creationDate;
            dailyLogDapper.lastModifiedDate = dailyLog.lastModifiedDate;
            dailyLogDapper.modifiedByID = dailyLog.modifiedByID;

            return dailyLogDapper;
        }

        public static DemandUserDapper Map(this DemandUser demandUser)
        {
            DemandUserDapper demandUserDapper = new DemandUserDapper();

            demandUserDapper.demandID = demandUser.demandID;
            demandUserDapper.userID = demandUser.userID;

            return demandUserDapper;
        }

        public static CustomerDapper Map(this Customer customer, int primaryKey)
        {
            CustomerDapper customerDapper = new CustomerDapper();

            customerDapper.customerID = primaryKey;
            customerDapper.customerName = customer.customerName;
            customerDapper.description = customer.description;
            customerDapper.isActive = customer.isActive;
            customerDapper.segmentID = customer.segmentID;
            customerDapper.typeID = customer.typeID;
            customerDapper.site = customer.site;
            customerDapper.address = customer.address;
            customerDapper.createdByID = customer.createdByID;
            customerDapper.creationDate = customer.creationDate;
            customerDapper.lastModifiedDate = customer.lastModifiedDate;
            customerDapper.modifiedByID = customer.modifiedByID;

            return customerDapper;
        }

        public static CustomerUserDapper Map(this CustomerUser customerUser)
        {
            CustomerUserDapper customerUserDapper = new CustomerUserDapper();

            customerUserDapper.customerID = customerUser.customerID;
            customerUserDapper.userID = customerUser.userID;

            return customerUserDapper;
        }

        public static ElementDapper Map(this Element element, int primaryKey)
        {
            ElementDapper elementDapper = new ElementDapper();

            elementDapper.elementID = primaryKey;
            elementDapper.element = element.element;
            elementDapper.actionID = element.actionID;
            elementDapper.defaultValue = element.defaultValue;
            elementDapper.valuePerKilometer = element.valuePerKilometer;
            elementDapper.domains = element.domains;
            elementDapper.automationID = element.automationID;
            elementDapper.typeIdentificationID = element.typeIdentificationID;
            elementDapper.featureID = element.featureID;
            elementDapper.createdByID = element.createdByID;
            elementDapper.creationDate = element.creationDate;
            elementDapper.lastModifiedDate = element.lastModifiedDate;
            elementDapper.modifiedByID = element.modifiedByID;

            return elementDapper;
        }

        public static SystemFeatureDapper Map(this SystemFeature systemFeature, int primaryKey)
        {
            SystemFeatureDapper systemFeatureDapper = new SystemFeatureDapper();

            systemFeatureDapper.systemFeatureID = primaryKey;
            systemFeatureDapper.systemFeatureName = systemFeature.systemFeatureName;
            systemFeatureDapper.systemFeatureTypeID = systemFeature.systemFeatureTypeID;
            systemFeatureDapper.createdByID = systemFeature.createdByID;
            systemFeatureDapper.creationDate = systemFeature.creationDate;
            systemFeatureDapper.lastModifiedDate = systemFeature.lastModifiedDate;
            systemFeatureDapper.modifiedByID = systemFeature.modifiedByID;

            return systemFeatureDapper;
        }


        public static PipelineDapper Map(this Pipeline pipeline, int primaryKey)
        {
            PipelineDapper pipelineDapper = new PipelineDapper();

            pipelineDapper.oportunityID = primaryKey;
            pipelineDapper.customerID = pipeline.customerID;
            pipelineDapper.oportunityCode = pipeline.oportunityCode;
            pipelineDapper.summary = pipeline.summary;
            pipelineDapper.description = pipeline.description;
            pipelineDapper.priorityID = pipeline.priorityID;
            pipelineDapper.faseID = pipeline.faseID;
            pipelineDapper.ownerID = pipeline.ownerID;
            pipelineDapper.saleManagerID = pipeline.saleManagerID;
            pipelineDapper.preSalesID = pipeline.preSalesID;
            pipelineDapper.operationManagerID = pipeline.operationManagerID;
            pipelineDapper.typeID = pipeline.typeID;
            pipelineDapper.costCenterID = pipeline.costCenterID;
            pipelineDapper.offerID = pipeline.offerID;
            pipelineDapper.sponsor = pipeline.sponsor;
            pipelineDapper.powerSponsor = pipeline.powerSponsor;
            pipelineDapper.expectedValue = pipeline.expectedValue;
            pipelineDapper.targetDate = pipeline.targetDate;
            pipelineDapper.statusID = pipeline.statusID;
            pipelineDapper.probability = pipeline.probability;
            pipelineDapper.billed = pipeline.billed;
            pipelineDapper.comments = pipeline.comments;
            pipelineDapper.closingDate = pipeline.closingDate;
            pipelineDapper.frequencyOfInteractionID = pipeline.frequencyOfInteractionID;
            pipelineDapper.approvedByID = pipeline.approvedByID;
            pipelineDapper.approvedDate = pipeline.approvedDate;
            pipelineDapper.quarter1 = pipeline.quarter1;
            pipelineDapper.quarter2 = pipeline.quarter2;
            pipelineDapper.quarter3 = pipeline.quarter3;
            pipelineDapper.quarter4 = pipeline.quarter4;
            pipelineDapper.createdByID = pipeline.createdByID;
            pipelineDapper.creationDate = pipeline.creationDate;
            pipelineDapper.lastModifiedDate = pipeline.lastModifiedDate;
            pipelineDapper.modifiedByID = pipeline.modifiedByID;

            return pipelineDapper;
        }


        public static ExpenseDapper Map(this Expense expense, int primaryKey)
        {
            ExpenseDapper expenseDapper = new ExpenseDapper();

            expenseDapper.expenseID = primaryKey;
            expenseDapper.description = expense.description;
            expenseDapper.registerDate = expense.registerDate;
            expenseDapper.typeExpenseID = expense.typeExpenseID;
            expenseDapper.demandID = expense.demandID;
            expenseDapper.statusID = expense.statusID;
            expenseDapper.customerID = expense.customerID;
            expenseDapper.departmentID = expense.departmentID;
            expenseDapper.subTotal = expense.subTotal;
            expenseDapper.Kilometer = expense.kilometer;
            expenseDapper.amountExpense = expense.amountExpense;
            expenseDapper.refundable = expense.refundable;
            expenseDapper.approvedByID = expense.approvedByID;
            expenseDapper.approvedDate = expense.approvedDate;
            expenseDapper.createdByID = expense.createdByID;
            expenseDapper.creationDate = expense.creationDate;
            expenseDapper.lastModifiedDate = expense.lastModifiedDate;
            expenseDapper.modifiedByID = expense.modifiedByID;

            return expenseDapper;
        }

        public static FeatureDapper Map(this Feature feature, int primaryKey)
        {
            FeatureDapper featureDapper = new FeatureDapper();

            featureDapper.featureID = primaryKey;
            featureDapper.featureName = feature.featureName;
            featureDapper.statusID = feature.statusID;
            featureDapper.description = feature.description;
            featureDapper.applicationSystemID = feature.applicationSystemID;
            featureDapper.developerID = feature.developerID;
            featureDapper.featureTypeID = feature.featureTypeID;
            featureDapper.metaScript = feature.metaScript;
            featureDapper.automationScript = feature.automationScript;
            featureDapper.testPoints = feature.testPoints;
            featureDapper.targetDate = feature.targetDate;
            featureDapper.timeEffort = feature.timeEffort;
            featureDapper.createdByID = feature.createdByID;
            featureDapper.creationDate = feature.creationDate;
            featureDapper.lastModifiedDate = feature.lastModifiedDate;
            featureDapper.modifiedByID = feature.modifiedByID;

            return featureDapper;
        }

        public static GroupDapper Map(this Group group, int primaryKey)
        {
            GroupDapper groupDapper = new GroupDapper();

            groupDapper.groupID = primaryKey;
            groupDapper.groupName = group.groupName;
            groupDapper.isSystem = group.isSystem;
            groupDapper.description = group.description;
            groupDapper.createdByID = group.createdByID;
            groupDapper.creationDate = group.creationDate;
            groupDapper.lastModifiedDate = group.lastModifiedDate;
            groupDapper.modifiedByID = group.modifiedByID;

            return groupDapper;
        }

        public static HistoricalDapper Map(this Historical historical, int primaryKey)
        {
            HistoricalDapper historicalDapper = new HistoricalDapper();

            historicalDapper.historicalID = primaryKey;
            historicalDapper.systemFeatureID = historical.systemFeatureID;
            historicalDapper.recordID = historical.recordID;
            historicalDapper.actionID = historical.actionID;
            historicalDapper.oldValue = historical.oldValue;
            historicalDapper.newValue = historical.newValue;
            historicalDapper.fieldName = historical.fieldName;
            historicalDapper.createdByID = historical.createdByID;
            historicalDapper.creationDate = historical.creationDate;
            historicalDapper.lastModifiedDate = historical.lastModifiedDate;
            historicalDapper.modifiedByID = historical.modifiedByID;

            return historicalDapper;
        }

        public static ParameterDapper Map(this Parameter parameter, int primaryKey)
        {
            ParameterDapper parameterDapper = new ParameterDapper();

            parameterDapper.parameterID = primaryKey;
            parameterDapper.parameterName = parameter.parameterName;
            parameterDapper.systemFeatureID = parameter.systemFeatureID;
            parameterDapper.createdByID = parameter.createdByID;
            parameterDapper.creationDate = parameter.creationDate;
            parameterDapper.lastModifiedDate = parameter.lastModifiedDate;
            parameterDapper.modifiedByID = parameter.modifiedByID;

            return parameterDapper;
        }

        public static ProfileDapper Map(this Profile profile, int primaryKey)
        {
            ProfileDapper profileDapper = new ProfileDapper();

            profileDapper.profileID = primaryKey;
            profileDapper.groupID = profile.GroupID;
            profileDapper.systemFeatureID = profile.SystemFeatureID;
            profileDapper.allowView = profile.AllowView;
            profileDapper.allowAdd = profile.AllowAdd;
            profileDapper.allowUpdate = profile.AllowUpdate;
            profileDapper.allowDelete = profile.AllowDelete;
            profileDapper.allowChangeStatus = profile.AllowChangeStatus;
            profileDapper.allowAddRemove = profile.AllowAddRemove;
            profileDapper.allowExportExcel = profile.AllowExportExcel;
            profileDapper.allowReportView = profile.AllowReportView;
            profileDapper.createdByID = profile.CreatedByID;
            profileDapper.creationDate = profile.CreationDate;
            profileDapper.lastModifiedDate = profile.LastModifiedDate;
            profileDapper.modifiedByID = profile.ModifiedByID;

            return profileDapper;
        }

        public static SystemMenuDapper Map(this SystemMenu systemMenu, int primaryKey)
        {
            SystemMenuDapper systemMenuDapper = new SystemMenuDapper();

            systemMenuDapper.menuID = primaryKey;
            systemMenuDapper.textMenu = systemMenu.textMenu;
            systemMenuDapper.description = systemMenu.description;
            systemMenuDapper.ordem = systemMenu.ordem;
            systemMenuDapper.urlAction = systemMenu.urlAction;
            systemMenuDapper.controller = systemMenu.controller;
            systemMenuDapper.icon = systemMenu.icon;
            systemMenuDapper.itsAdmin = systemMenu.itsAdmin;
            systemMenuDapper.systemFeatureID = systemMenu.systemFeatureID;
            systemMenuDapper.createdByID = systemMenu.createdByID;
            systemMenuDapper.creationDate = systemMenu.creationDate;
            systemMenuDapper.lastModifiedDate = systemMenu.lastModifiedDate;
            systemMenuDapper.modifiedByID = systemMenu.modifiedByID;

            return systemMenuDapper;
        }

        public static SystemParameterDapper Map(this SystemParameter systemParameter, int primaryKey)
        {
            SystemParameterDapper systemParameterDapper = new SystemParameterDapper();

            systemParameterDapper.parameterID = primaryKey;
            systemParameterDapper.paramterName = systemParameter.paramterName;
            systemParameterDapper.paramterValue = systemParameter.paramterValue;
            systemParameterDapper.paramterDefaultValue = systemParameter.paramterDefaultValue;
            systemParameterDapper.createdByID = systemParameter.createdByID;
            systemParameterDapper.creationDate = systemParameter.creationDate;
            systemParameterDapper.lastModifiedDate = systemParameter.lastModifiedDate;
            systemParameterDapper.modifiedByID = systemParameter.modifiedByID;

            return systemParameterDapper;
        }

        public static TaskDapper Map(this Task task, int primaryKey)
        {
            TaskDapper taskDapper = new TaskDapper();

            taskDapper.taskID = primaryKey;
            taskDapper.summary = task.summary;
            taskDapper.description = task.description;
            taskDapper.assignToID = task.assignToID;
            taskDapper.demandID = task.demandID;
            taskDapper.customerID = task.customerID;
            taskDapper.statusID = task.statusID;
            taskDapper.targetDate = task.targetDate;
            taskDapper.closingDate = task.closingDate;
            taskDapper.createdByID = task.createdByID;
            taskDapper.creationDate = task.creationDate;
            taskDapper.lastModifiedDate = task.lastModifiedDate;
            taskDapper.modifiedByID = task.modifiedByID;

            return taskDapper;
        }

        public static TemplateDapper Map(this Template template, int primaryKey)
        {
            TemplateDapper templateDapper = new TemplateDapper();

            templateDapper.templateID = primaryKey;
            templateDapper.templateName = template.templateName;
            templateDapper.description = template.description;
            templateDapper.domainID = template.domainID;
            templateDapper.createdByID = template.createdByID;
            templateDapper.creationDate = template.creationDate;
            templateDapper.modifiedByID = template.modifiedByID;
            templateDapper.lastModifiedDate = template.lastModifiedDate;

            return templateDapper;
        }

        public static ResumeDapper Map(this Resume resume, int primaryKey)
        {
            ResumeDapper resumeDapper = new ResumeDapper();

            resumeDapper.resumeID = primaryKey;
            resumeDapper.summary = resume.summary;
            resumeDapper.functionID = resume.functionID;
            resumeDapper.description = resume.description;
            resumeDapper.genderID = resume.genderID;
            resumeDapper.age = resume.age;
            resumeDapper.timeExperience = resume.timeExperience;
            resumeDapper.functionLevelID = resume.functionLevelID;
            resumeDapper.statusRhID = resume.statusRhID;
            resumeDapper.approvedDateRh = resume.approvedDateRh;
            resumeDapper.statusManagerID = resume.statusManagerID;
            resumeDapper.approvedDateManager = resume.approvedDateManager;
            resumeDapper.statusClientID = resume.statusClientID;
            resumeDapper.approvedDateClient = resume.approvedDateClient;
            resumeDapper.expectedSalary = resume.expectedSalary;
            resumeDapper.contractTypeID = resume.contractTypeID;
            resumeDapper.isEmployee = resume.isEmployee;
            resumeDapper.willingToTravel = resume.willingToTravel;
            resumeDapper.maritalStatusID = resume.maritalStatusID;
            resumeDapper.haveChildren = resume.haveChildren;
            resumeDapper.isSmoker = resume.isSmoker;
            resumeDapper.availabilityToStart = resume.availabilityToStart;
            resumeDapper.observation = resume.observation;
            resumeDapper.createdByID = resume.createdByID;
            resumeDapper.creationDate = resume.creationDate;
            resumeDapper.modifiedByID = resume.modifiedByID;
            resumeDapper.lastModifiedDate = resume.lastModifiedDate;
            resumeDapper.resultRh = resume.resultRh;
            resumeDapper.resultManager = resume.resultManager;
            resumeDapper.resultClient = resume.resultClient;

            return resumeDapper;
        }

        public static TestCaseDapper Map(this TestCase testCase, int primaryKey)
        {
            TestCaseDapper testCaseDapper = new TestCaseDapper();

            testCaseDapper.testCaseID = primaryKey;
            testCaseDapper.statusID = testCase.statusID;
            testCaseDapper.testCase = testCase.testCase;
            testCaseDapper.description = testCase.description;
            testCaseDapper.precondition = testCase.precondition;
            testCaseDapper.expectedResult = testCase.expectedResult;
            testCaseDapper.featureID = testCase.featureID;
            testCaseDapper.testScenarioID = testCase.testScenarioID;
            testCaseDapper.executionOrder = testCase.executionOrder;
            testCaseDapper.flowTestID = testCase.flowTestID;
            testCaseDapper.startExecution = testCase.startExecution;
            testCaseDapper.endExecution = testCase.endExecution;
            testCaseDapper.timeExecution = testCase.timeExecution;
            testCaseDapper.release = testCase.release;
            testCaseDapper.cycle = testCase.cycle;
            testCaseDapper.testTypeID = testCase.testTypeID;
            testCaseDapper.createdByID = testCase.createdByID;
            testCaseDapper.creationDate = testCase.creationDate;
            testCaseDapper.modifiedByID = testCase.modifiedByID;
            testCaseDapper.lastModifiedDate = testCase.lastModifiedDate;

            return testCaseDapper;
        }

        public static TestLogDapper Map(this TestLog testLog, int primaryKey)
        {
            TestLogDapper testLogDapper = new TestLogDapper();

            testLogDapper.logID = primaryKey;
            testLogDapper.testID = testLog.testID;
            testLogDapper.statusID = testLog.statusID;
            testLogDapper.stepName = testLog.stepName;
            testLogDapper.expectedResult = testLog.expectedResult;
            testLogDapper.actualResult = testLog.actualResult;
            testLogDapper.pathEvidence = testLog.pathEvidence;
            testLogDapper.createdByID = testLog.createdByID;
            testLogDapper.creationDate = testLog.creationDate;
            testLogDapper.modifiedByID = testLog.modifiedByID;
            testLogDapper.lastModifiedDate = testLog.lastModifiedDate;

            return testLogDapper;
        }

        public static TestPackageDapper Map(this TestPackage testPackage, int primaryKey)
        {
            TestPackageDapper testPackageDapper = new TestPackageDapper();

            testPackageDapper.testPackageID = primaryKey;
            testPackageDapper.packageName = testPackage.packageName;
            testPackageDapper.description = testPackage.description;
            testPackageDapper.demandID = testPackage.demandID;
            testPackageDapper.statusID = testPackage.statusID;
            testPackageDapper.release = testPackage.release;
            testPackageDapper.cycle = testPackage.cycle;
            testPackageDapper.emailsToSendReport = testPackage.emailsToSendReport;
            testPackageDapper.tecnologyID = testPackage.tecnologyID;
            testPackageDapper.browserID = testPackage.browserID;
            testPackageDapper.executionSpeedy = testPackage.executionSpeedy;
            testPackageDapper.resetApp = testPackage.resetApp;
            testPackageDapper.highLight = testPackage.highLight;
            testPackageDapper.highLightOut = testPackage.highLightOut;
            testPackageDapper.deviceID = testPackage.deviceID;
            testPackageDapper.platformNameID = testPackage.platformNameID;
            testPackageDapper.sendEmail = testPackage.sendEmail;
            testPackageDapper.generateLog = testPackage.generateLog;
            testPackageDapper.logHtml = testPackage.logHtml;
            testPackageDapper.methodologyID = testPackage.methodologyID;
            testPackageDapper.solutionPath = testPackage.solutionPath;
            testPackageDapper.leantestVariable = testPackage.leantestVariable;
            testPackageDapper.saveEvidenceToExternalPath = testPackage.saveEvidenceToExternalPath;
            testPackageDapper.createdByID = testPackage.createdByID;
            testPackageDapper.creationDate = testPackage.creationDate;
            testPackageDapper.modifiedByID = testPackage.modifiedByID;
            testPackageDapper.lastModifiedDate = testPackage.lastModifiedDate;

            return testPackageDapper;
        }

        public static TestScenarioDapper Map(this TestScenario testScenario, int primaryKey)
        {
            TestScenarioDapper testScenarioDapper = new TestScenarioDapper();

            testScenarioDapper.testScenarioID = primaryKey;
            testScenarioDapper.testScenario = testScenario.testScenario;
            testScenarioDapper.description = testScenario.description;
            testScenarioDapper.statusID = testScenario.statusID;
            testScenarioDapper.executionOrder = testScenario.executionOrder;
            testScenarioDapper.startExecution = testScenario.startExecution;
            testScenarioDapper.endExecution = testScenario.endExecution;
            testScenarioDapper.timeExecution = testScenario.timeExecution;
            testScenarioDapper.testTypeID = testScenario.testTypeID;
            testScenarioDapper.executionTypeID = testScenario.executionTypeID;
            testScenarioDapper.createdByID = testScenario.createdByID;
            testScenarioDapper.creationDate = testScenario.creationDate;
            testScenarioDapper.modifiedByID = testScenario.modifiedByID;
            testScenarioDapper.lastModifiedDate = testScenario.lastModifiedDate;
            testScenarioDapper.testPackageID = testScenario.testPackageID;

            return testScenarioDapper;
        }

        public static TestScenarioFeatureDapper Map(this TestScenarioFeature testScenarioFeature, int primaryKey)
        {
            TestScenarioFeatureDapper testScenarioFeatureDapper = new TestScenarioFeatureDapper();

            testScenarioFeatureDapper.testScenarioFeatureID = primaryKey;
            testScenarioFeatureDapper.testScenarioID = testScenarioFeature.testScenarioID;
            testScenarioFeatureDapper.featureID = testScenarioFeature.featureID;
            testScenarioFeatureDapper.executionOrder = testScenarioFeature.executionOrder;
            testScenarioFeatureDapper.isLoop = testScenarioFeature.isLoop;
            testScenarioFeatureDapper.statusID = testScenarioFeature.statusID;
            testScenarioFeatureDapper.toolsTestID = testScenarioFeature.toolsTestID;
            testScenarioFeatureDapper.testID = testScenarioFeature.testID;
            testScenarioFeatureDapper.createdByID = testScenarioFeature.createdByID;
            testScenarioFeatureDapper.creationDate = testScenarioFeature.creationDate;
            testScenarioFeatureDapper.modifiedByID = testScenarioFeature.modifiedByID;
            testScenarioFeatureDapper.lastModifiedDate = testScenarioFeature.lastModifiedDate;

            return testScenarioFeatureDapper;
        }

        public static TimeReleaseDapper Map(this TimeRelease timeRelease, int primaryKey)
        {
            TimeReleaseDapper timeReleaseDapper = new TimeReleaseDapper();

            timeReleaseDapper.timeReleaseID = primaryKey;
            timeReleaseDapper.registerDate = timeRelease.registerDate;
            timeReleaseDapper.startWork = timeRelease.startWork;
            timeReleaseDapper.endWork = timeRelease.endWork;
            timeReleaseDapper.demandID = timeRelease.demandID;
            timeReleaseDapper.isApproved = timeRelease.isApproved;
            timeReleaseDapper.activityID = timeRelease.activityID;
            timeReleaseDapper.approvedByID = timeRelease.approvedByID;
            timeReleaseDapper.approvedDate = timeRelease.approvedDate;
            timeReleaseDapper.description = timeRelease.description;
            timeReleaseDapper.reasonChange = timeRelease.reasonChange;
            timeReleaseDapper.createdByID = timeRelease.createdByID;
            timeReleaseDapper.creationDate = timeRelease.creationDate;
            timeReleaseDapper.modifiedByID = timeRelease.modifiedByID;
            timeReleaseDapper.lastModifiedDate = timeRelease.lastModifiedDate;

            return timeReleaseDapper;
        }

        public static GroupUserDapper Map(this GroupUser groupUser)
        {
            GroupUserDapper groupUserDapper = new GroupUserDapper();

            groupUserDapper.groupID = groupUser.groupID;
            groupUserDapper.userID = groupUser.userID;

            return groupUserDapper;
        }

        public static VacancieResumeDapper Map(this VacancieResume vacancieResume)
        {
            VacancieResumeDapper vacancieResumeDapper = new VacancieResumeDapper();

            vacancieResumeDapper.vacancieID = vacancieResume.vacancieID;
            vacancieResumeDapper.resumeID = vacancieResume.resumeID;

            return vacancieResumeDapper;
        }

        public static ResumeVacancieDapper Map(this ResumeVacancie resumeVacancie)
        {
            ResumeVacancieDapper resumeVacancieDapper = new ResumeVacancieDapper();

            resumeVacancieDapper.resumeID = resumeVacancie.resumeID;
            resumeVacancieDapper.vacancieID = resumeVacancie.vacancieID;

            return resumeVacancieDapper;
        }
        public static WorkflowDapper Map(this Workflow workflow, int primaryKey)
        {
            WorkflowDapper workflowDapper = new WorkflowDapper();

            workflowDapper.workflowID = primaryKey;
            workflowDapper.systemFeatureID = workflow.systemFeatureID;
            workflowDapper.groupID = workflow.groupID;
            workflowDapper.statusID = workflow.statusID;
            workflowDapper.statusToID = workflow.statusToID;
            workflowDapper.createdByID = workflow.createdByID;
            workflowDapper.creationDate = workflow.creationDate;
            workflowDapper.modifiedByID = workflow.modifiedByID;
            workflowDapper.lastModifiedDate = workflow.lastModifiedDate;

            return workflowDapper;
        }

        public static LicenseDapper Map(this License license, int primaryKey)
        {
            LicenseDapper licenseDapper = new LicenseDapper();

            licenseDapper.licenseID = primaryKey;
            licenseDapper.licenseCode = license.licenseCode;
            licenseDapper.customerID = license.customerID;
            licenseDapper.expirationDate = license.expirationDate;
            licenseDapper.licenseTypeID = license.licenseTypeID;
            licenseDapper.hostName = license.hostName;
            licenseDapper.macAddress = license.macAddress;
            licenseDapper.description = license.description;
            licenseDapper.license = license.license;
            licenseDapper.approvedByID = license.approvedByID;
            licenseDapper.approvedDate = license.approvedDate;
            licenseDapper.createdByID = license.createdByID;
            licenseDapper.creationDate = license.creationDate;
            licenseDapper.modifiedByID = license.modifiedByID;
            licenseDapper.lastModifiedDate = license.lastModifiedDate;

            return licenseDapper;
        }

        public static EquipmentAccessorieDapper Map(this EquipmentAccessorie equipmentAccessorie, int primaryKey)
        {
            EquipmentAccessorieDapper equipmentAccessorieDapper = new EquipmentAccessorieDapper();

            equipmentAccessorieDapper.equipmentAccessorieID = primaryKey;
            equipmentAccessorieDapper.description = equipmentAccessorie.description;
            equipmentAccessorieDapper.serialNumber = equipmentAccessorie.serialNumber;
            equipmentAccessorieDapper.modelName = equipmentAccessorie.modelName;
            equipmentAccessorieDapper.assignToID = equipmentAccessorie.assignToID;
            equipmentAccessorieDapper.typeID = equipmentAccessorie.typeID;
            equipmentAccessorieDapper.invoicing = equipmentAccessorie.invoicing;
            equipmentAccessorieDapper.amountInvoicing = equipmentAccessorie.amountInvoicing;
            equipmentAccessorieDapper.createdByID = equipmentAccessorie.createdByID;
            equipmentAccessorieDapper.creationDate = equipmentAccessorie.creationDate;
            equipmentAccessorieDapper.modifiedByID = equipmentAccessorie.modifiedByID;
            equipmentAccessorieDapper.lastModifiedDate = equipmentAccessorie.lastModifiedDate;
            equipmentAccessorieDapper.startDate = equipmentAccessorie.startDate;
            equipmentAccessorieDapper.endDate = equipmentAccessorie.endDate;


            return equipmentAccessorieDapper;

        }
        public static IssueDapper Map(this Issue issue, int primaryKey)
        {
            IssueDapper issueDapper = new IssueDapper();

            issueDapper.issueID = primaryKey;
            issueDapper.summary = issue.summary;
            issueDapper.description = issue.description;
            issueDapper.statusID = issue.statusID;
            issueDapper.severityID = issue.severityID;
            issueDapper.priorityID = issue.priorityID;
            issueDapper.assingToID = issue.assingToID;
            issueDapper.typeID = issue.typeID;
            issueDapper.resolutionID = issue.resolutionID;
            issueDapper.resolution = issue.resolution;
            issueDapper.resolutionDate = issue.resolutionDate;
            issueDapper.createdByID = issue.createdByID;
            issueDapper.creationDate = issue.creationDate;
            issueDapper.modifiedByID = issue.modifiedByID;
            issueDapper.lastModifiedDate = issue.lastModifiedDate;

            return issueDapper;
        }

        public static DefectDapper Map(this Defect defect, int primaryKey)
        {
            DefectDapper defectDapper = new DefectDapper();

            defectDapper.defectID = primaryKey;
            defectDapper.summary = defect.summary;
            defectDapper.description = defect.description;
            defectDapper.statusID = defect.statusID;
            defectDapper.severityID = defect.severityID;
            defectDapper.priorityID = defect.priorityID;
            defectDapper.assingToID = defect.assingToID;
            defectDapper.typeID = defect.typeID;
            defectDapper.resolutionID = defect.resolutionID;
            defectDapper.resolution = defect.resolution;
            defectDapper.resolutionDate = defect.resolutionDate;
            defectDapper.applicationSystemID = defect.applicationSystemID;
            defectDapper.featureID = defect.featureID;
            defectDapper.createdByID = defect.createdByID;
            defectDapper.creationDate = defect.creationDate;
            defectDapper.modifiedByID = defect.modifiedByID;
            defectDapper.lastModifiedDate = defect.lastModifiedDate;

            return defectDapper;
        }

        public static PipelineEventDapper Map(this PipelineEvent pipelineEvent, int primaryKey)
        {
            PipelineEventDapper pipelineEventDapper = new PipelineEventDapper();

            pipelineEventDapper.saleEventID = primaryKey;
            pipelineEventDapper.registerDate = pipelineEvent.registerDate;
            pipelineEventDapper.typeID = pipelineEvent.typeID;
            pipelineEventDapper.nextStepID = pipelineEvent.nextStepID;
            pipelineEventDapper.targetDate = pipelineEvent.targetDate;
            pipelineEventDapper.description = pipelineEvent.description;
            pipelineEventDapper.oportunityID = pipelineEvent.oportunityID;
            pipelineEventDapper.createdByID = pipelineEvent.createdByID;
            pipelineEventDapper.creationDate = pipelineEvent.creationDate;
            pipelineEventDapper.lastModifiedDate = pipelineEvent.lastModifiedDate;
            pipelineEventDapper.modifiedByID = pipelineEvent.modifiedByID;

            return pipelineEventDapper;
        }

        public static MovimentEmployeeDapper Map(this MovimentEmployee movimentEmployee, int primaryKey)
        {
            MovimentEmployeeDapper movimentEmployeeDapper = new MovimentEmployeeDapper();

            movimentEmployeeDapper.movimentEmployeeID = primaryKey;
            movimentEmployeeDapper.employeeID = movimentEmployee.employeeID;
            movimentEmployeeDapper.startDate = movimentEmployee.startDate;
            movimentEmployeeDapper.endDate = movimentEmployee.endDate;
            movimentEmployeeDapper.statusID = movimentEmployee.statusID;
            movimentEmployeeDapper.movimentEmployeeTypeID = movimentEmployee.movimentEmployeeTypeID;
            movimentEmployeeDapper.approvedDate = movimentEmployee.approvedDate;
            movimentEmployeeDapper.approvedByID = movimentEmployee.approvedByID;
            movimentEmployeeDapper.description = movimentEmployee.description;
            movimentEmployeeDapper.createdByID = movimentEmployee.createdByID;
            movimentEmployeeDapper.creationDate = movimentEmployee.creationDate;
            movimentEmployeeDapper.modifiedByID = movimentEmployee.modifiedByID;
            movimentEmployeeDapper.lastModifiedDate = movimentEmployee.lastModifiedDate;

            return movimentEmployeeDapper;
        }

        public static VacancieDapper Map(this Vacancie vacancie, int primaryKey)
        {
            VacancieDapper vacancieDapper = new VacancieDapper();

            vacancieDapper.vacancieID = primaryKey;
            vacancieDapper.vacanciesTypeID = vacancie.vacanciesTypeID;
            vacancieDapper.summary = vacancie.summary;
            vacancieDapper.description = vacancie.description;
            vacancieDapper.customerID = vacancie.customerID;
            vacancieDapper.internalApplicantID = vacancie.internalApplicantID;
            vacancieDapper.externalApplicantID = vacancie.externalApplicantID;
            vacancieDapper.assignToID = vacancie.assignToID;
            vacancieDapper.contractTypeID = vacancie.contractTypeID;
            vacancieDapper.validityID = vacancie.validityID;
            vacancieDapper.statusID = vacancie.statusID;
            vacancieDapper.openingDate = vacancie.openingDate;
            vacancieDapper.closingDate = vacancie.closingDate;
            vacancieDapper.expectedStartDate = vacancie.expectedStartDate;
            vacancieDapper.maximumValue = vacancie.maximumValue;
            vacancieDapper.closedValue = vacancie.closedValue;
            vacancieDapper.workPlace = vacancie.workPlace;
            vacancieDapper.resumeSelectedID = vacancie.resumeSelectedID;
            vacancieDapper.createdByID = vacancie.createdByID;
            vacancieDapper.creationDate = vacancie.creationDate;
            vacancieDapper.modifiedByID = vacancie.modifiedByID;
            vacancieDapper.lastModifiedDate = vacancie.lastModifiedDate;

            return vacancieDapper;
        }

        public static ContractDapper Map(this Contract contract, int primaryKey)
        {
            ContractDapper contractDapper = new ContractDapper();

            contractDapper.contractID = primaryKey;
            contractDapper.oportunityID = contract.oportunityID;
            contractDapper.contractTypeID = contract.contractTypeID;
            contractDapper.contractorCustomerID = contract.contractorCustomerID;
            contractDapper.contractingCustomerID = contract.contractingCustomerID;
            contractDapper.objectContract = contract.objectContract;
            contractDapper.startDate = contract.startDate;
            contractDapper.endDate = contract.endDate;
            contractDapper.periodValidityID = contract.periodValidityID;
            contractDapper.extencionID = contract.extencionID;
            contractDapper.extencionPeriodID = contract.extencionPeriodID;
            contractDapper.resetModalityID = contract.resetModalityID;
            contractDapper.billingCondition = contract.billingCondition;
            contractDapper.createdByID = contract.createdByID;
            contractDapper.creationDate = contract.creationDate;
            contractDapper.modifiedByID = contract.modifiedByID;
            contractDapper.lastModifiedDate = contract.lastModifiedDate;

            return contractDapper;
        }

        public static AnnexContractDapper Map(this AnnexContract annexContract, int primaryKey)
        {
            AnnexContractDapper annexContractDapper = new AnnexContractDapper();

            annexContractDapper.annexID = primaryKey;
            annexContractDapper.oportunityID = annexContract.oportunityID;
            annexContractDapper.contractID = annexContract.contractID;
            annexContractDapper.summary = annexContract.summary;
            annexContractDapper.annexObject = annexContract.annexObject;
            annexContractDapper.startDate = annexContract.startDate;
            annexContractDapper.endDate = annexContract.endDate;
            annexContractDapper.extencionPeriodID = annexContract.extencionPeriodID;
            annexContractDapper.createdByID = annexContract.createdByID;
            annexContractDapper.creationDate = annexContract.creationDate;
            annexContractDapper.modifiedByID = annexContract.modifiedByID;
            annexContractDapper.lastModifiedDate = annexContract.lastModifiedDate;

            return annexContractDapper;
        }

        public static ContractAdditiveDapper Map(this ContractAdditive contractAdditive, int primaryKey)
        {
            ContractAdditiveDapper contractAdditiveDapper = new ContractAdditiveDapper();

            contractAdditiveDapper.additiveID = primaryKey;
            contractAdditiveDapper.contractID = contractAdditive.contractID;
            contractAdditiveDapper.additiveObject = contractAdditive.additiveObject;
            contractAdditiveDapper.startDate = contractAdditive.startDate;
            contractAdditiveDapper.endDate = contractAdditive.endDate;
            contractAdditiveDapper.periodValidityID = contractAdditive.periodValidityID;
            contractAdditiveDapper.extencionID = contractAdditive.extencionID;
            contractAdditiveDapper.extencionPeriodID = contractAdditive.extencionPeriodID;
            contractAdditiveDapper.resetModalityID = contractAdditive.resetModalityID;
            contractAdditiveDapper.billingCondition = contractAdditive.billingCondition;
            contractAdditiveDapper.oportunityID = contractAdditive.oportunityID;
            contractAdditiveDapper.createdByID = contractAdditive.createdByID;
            contractAdditiveDapper.creationDate = contractAdditive.creationDate;
            contractAdditiveDapper.modifiedByID = contractAdditive.modifiedByID;
            contractAdditiveDapper.lastModifiedDate = contractAdditive.lastModifiedDate;

            return contractAdditiveDapper;
        }

        public static ContactDapper Map(this Contact contact, int primaryKey)
        {
            ContactDapper contactDapper = new ContactDapper();

            contactDapper.contactID = primaryKey;
            contactDapper.contactName = contact.contactName;
            contactDapper.email = contact.email;
            contactDapper.cellNumber = contact.cellNumber;
            contactDapper.telNumber = contact.telNumber;
            contactDapper.functionID = contact.functionID;
            contactDapper.customerID = contact.customerID;
            contactDapper.description = contact.description;
            contactDapper.feature = contact.feature;
            contactDapper.createdByID = contact.createdByID;
            contactDapper.creationDate = contact.creationDate;
            contactDapper.modifiedByID = contact.modifiedByID;
            contactDapper.lastModifiedDate = contact.lastModifiedDate;

            return contactDapper;
        }

        public static SupportDapper Map(this Support support, int primaryKey)
        {
            SupportDapper supportDapper = new SupportDapper();

            supportDapper.supportID = primaryKey;
            supportDapper.summary = support.summary;
            supportDapper.description = support.description;
            supportDapper.severityID = support.severityID;
            supportDapper.statusID = support.statusID;
            supportDapper.priorityID = support.priorityID;
            supportDapper.typeID = support.typeID;
            supportDapper.assingToID = support.assingToID;
            supportDapper.resolutionDate = support.resolutionDate;
            supportDapper.customerID = support.customerID;
            supportDapper.createdByID = support.createdByID;
            supportDapper.creationDate = support.creationDate;
            supportDapper.modifiedByID = support.modifiedByID;
            supportDapper.lastModifiedDate = support.lastModifiedDate;

            return supportDapper;
        }

        public static SkillDapper Map(this Skill skill, int primaryKey)
		{
			SkillDapper skillDapper = new SkillDapper();

			skillDapper.skillID = primaryKey;
			skillDapper.summary = skill.summary;
			skillDapper.skillTypeID = skill.skillTypeID;
			skillDapper.description = skill.description;
			skillDapper.createdByID = skill.createdByID;
			skillDapper.creationDate = skill.creationDate;
			skillDapper.modifiedByID = skill.modifiedByID;
			skillDapper.lastModifiedDate = skill.lastModifiedDate;

			return skillDapper;
		}

        public static DashboardDapper Map(this Dashboard dashboard, int primaryKey)
        {
            DashboardDapper dashboardDapper = new DashboardDapper();

            dashboardDapper.dashboardID = primaryKey;
            dashboardDapper.item1 = dashboard.item1;
            dashboardDapper.item2 = dashboard.item2;
            dashboardDapper.item3 = dashboard.item3;
            dashboardDapper.item4 = dashboard.item4;
            dashboardDapper.item5 = dashboard.item5;
            dashboardDapper.item6 = dashboard.item6;
            dashboardDapper.item7 = dashboard.item7;
            dashboardDapper.item8 = dashboard.item8;
            dashboardDapper.item9 = dashboard.item9;
            dashboardDapper.item10 = dashboard.item10;
            dashboardDapper.item11 = dashboard.item11;
            dashboardDapper.item12 = dashboard.item12;
            dashboardDapper.item13 = dashboard.item13;
            dashboardDapper.item14 = dashboard.item14;
            dashboardDapper.item15 = dashboard.item15;
            dashboardDapper.item16 = dashboard.item16;
            dashboardDapper.item17 = dashboard.item17;
            dashboardDapper.item18 = dashboard.item18;
            dashboardDapper.item19 = dashboard.item19;
            dashboardDapper.item20 = dashboard.item20;
            dashboardDapper.createdByID = dashboard.createdByID;
            dashboardDapper.creationDate = dashboard.creationDate;
            dashboardDapper.modifiedByID = dashboard.modifiedByID;
            dashboardDapper.lastModifiedDate = dashboard.lastModifiedDate;

            return dashboardDapper;
        }
    } 
}
