select  i.IDIssue as ID, i.Summary, 
(select convert(varchar, i1.IDIssue) + ' - ' + i1.summary from Issues i1 where i1.IDIssue = i.Ordem) as [Task Assign], 
v1.Value as [Status], CONVERT(varchar, i.ProgressIssue) + '%' as [Progress],
convert(varchar,i.DurationIssue) + ' days' as [Duration] , CONVERT(varchar, i.effort) + ' hrs' as [Effort], 
convert(varchar, i.WorkIssue) + ' hrs' as [Work],
u.UserName + ' (' + u.Login + ')' as [Assingn To],
DATEADD(d, DATEDIFF(d, 0, i.StartDatePlan), 0) as [Start Date Plan], 
DATEADD(d, DATEDIFF(d, 0, i.EndDatePlan), 0) as [End Date Plan], 
DATEADD(d, DATEDIFF(d, 0, i.StartDateReal), 0) as [Start Date Real], 
DATEADD(d, DATEDIFF(d, 0, i.EndDateReal), 0) as [End Date Real]
from Issues i 
inner join ParameterValues v1 on i.statusID = v1.valueID
inner join Users u on i.IDAssignTo = u.userID
where demandID = @demandID and ISFolder = 0 and IDIssueType = 145
order by 1	
