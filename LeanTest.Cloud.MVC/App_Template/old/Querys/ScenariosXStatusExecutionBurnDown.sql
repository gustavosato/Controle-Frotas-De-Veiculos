

Create Table #Temp ([Date] date, [Passed] int, [Others] int)
insert into #Temp

select
ISNULL(DATEADD(d, DATEDIFF(d, 0, s.StartExecution), 0), (DATEADD(d, DATEDIFF(d, 0, s.CreateDate), 0))) as [Date], count(*) as [Passed], 0 as [Others]
from Scenarios s where  demandID in (@demandID) and s.IDStatusExecution = 59 group by ISNULL(DATEADD(d, DATEDIFF(d, 0, s.StartExecution), 0), (DATEADD(d, DATEDIFF(d, 0, s.CreateDate), 0)))
union

select
ISNULL(DATEADD(d, DATEDIFF(d, 0, s.StartExecution), 0), (DATEADD(d, DATEDIFF(d, 0, s.CreateDate), 0))) as [Date],  0 as [Passed], count(*) as [Others]
from Scenarios s where s.IDStatusExecution <> 0 and demandID in (@demandID) group by ISNULL(DATEADD(d, DATEDIFF(d, 0, s.StartExecution), 0), (DATEADD(d, DATEDIFF(d, 0, s.CreateDate), 0)))

Create Table #Temp1 ([Date] date, [Passed] int, [Others] int)
insert into #Temp1

select [Date], sum([Passed]) as [Passed], sum([Others]) as [Others]
from #Temp
Group by [Date]


SELECT [Date],
SUM([Passed]) OVER(ORDER BY [Date] ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS [Passed],
SUM([Others]) OVER(ORDER BY [Date] ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS [Others]
FROM #Temp1
Group by [Date], [Passed], [Others]

drop table #Temp
drop table #Temp1


