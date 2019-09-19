select 
d.demandID as ID,
d.externalCode + ' - ' + d.DemandName as Demand,
p.Value as [Status],
u.UserName + ' (' + u.Login + ')' as [Assign To],
convert(varchar, isnull(d.TotalEffortEstimate, 0)) + ' hrs' as [Estimate Effort],
[Appropriate Hours] = convert(varchar,(select count(*) from TimeSheets t where t.demandID = d.demandID)) + ' hrs',
d.Work,
d.ActualWork as [Actual Work],
d.Impact,
convert(varchar,(isnull(d.TotalChangeRequest, 0))) + ' hrs' as [Total Change Request],
isnull(d.EstimatedScenariosEstimate, 0 ) as [Test Scenarios Estimate],
[Test Scenarios Defined] = (select count(*) from Scenarios s where d.demandID = s.demandID),
[Tasks] = (select count(*) from Issues i where i.demandID = d.demandID and i.ISFolder = 0 and i.IDIssueType = 144)
from demands d
inner join ParameterValues p on d.statusID = p.valueID
inner join Users u on u.userID = d.assignToTargetID
where d.environmentID = @environmentID Order By Demand Desc
