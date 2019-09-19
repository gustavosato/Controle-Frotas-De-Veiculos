select  i.IDIssue as ID, i.Summary, v1.Value as [Status], CONVERT(varchar, i.ProgressIssue) + '%' as [Progress],
--convert(varchar,i.DurationIssue) + ' days' as [Duration] , CONVERT(varchar, i.effort) + ' hrs' as [Effort], 
--convert(varchar, i.WorkIssue) + ' hrs' as [Work],
DATEADD(d, DATEDIFF(d, 0, i.StartDatePlan), 0) as [Start Date Plan], 
DATEADD(d, DATEDIFF(d, 0, i.EndDatePlan), 0) as [End Date Plan], 
DATEADD(d, DATEDIFF(d, 0, i.StartDateReal), 0) as [Start Date Real], 
DATEADD(d, DATEDIFF(d, 0, i.EndDateReal), 0) as [End Date Real]
from Issues i inner join ParameterValues v1 on i.statusID = v1.valueID
where demandID = @demandID and ISFolder = 0 and IDIssueType = 144
order by 1