select 
d.demandID as ID,
d.externalCode + ' - ' + d.DemandName as Demand,
p.Value as [Status],
u.UserName + ' (' + u.Login + ')' as [Assign To],
convert( date, d.PlanningStartDate, 103) as [Planning Start Date],
convert( date, d.PlanningEndDate, 103) as [Planning End Date],
d.DesignStartEstiamte as [Design Start  Date],
d.DesignEndEstimate as [Design End Date],
d.ExecutionStartEstimate as [Execution Start Date],
d.ExecutionEndEstimate as [Execution End Date],
isnull(d.EstimatedScenariosEstimate, 0 ) as [Test Scenarios Estimate],
[Test Scenarios Defined] = (select count(*) from Scenarios s where d.demandID = s.demandID)
from demands d
inner join ParameterValues p on d.statusID = p.valueID
inner join Users u on u.userID = d.assignToTargetID
where d.environmentID = @environmentID Order By Demand Desc
