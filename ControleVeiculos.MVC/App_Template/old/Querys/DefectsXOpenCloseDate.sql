Create Table #Temp ([Date] date, [Open] int, Closed int)
insert into #Temp

select
DATEADD(d, DATEDIFF(d, 0, d.CreationDate), 0) as [Date], count(*) as [Open], 0 as [Closed]
from Defects d where  demandID in (@demandID) and d.statusID <> 112 group by DATEADD(d, DATEDIFF(d, 0, d.CreationDate), 0)
union
select
DATEADD(d, DATEDIFF(d, 0, d.CloseDate), 0) as [Date],  0 as [Open], count(*) as [Closed]
from Defects d where d.statusID = 112 and demandID in (@demandID) group by DATEADD(d, DATEDIFF(d, 0, d.CloseDate), 0)

Create Table #Temp1 ([Date] date, [Open] int, Closed int)
insert into #Temp1

select [Date], sum([Open]) as [Open], sum(Closed) as [Closed]
from #Temp
Group by [Date]


SELECT [Date],
SUM([Open]) OVER(ORDER BY [Date] ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS [Open],
SUM([Closed]) OVER(ORDER BY [Date] ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS [Closed]
FROM #Temp1
Group by [Date], [Open], [Closed]

drop table #Temp
drop table #Temp1


