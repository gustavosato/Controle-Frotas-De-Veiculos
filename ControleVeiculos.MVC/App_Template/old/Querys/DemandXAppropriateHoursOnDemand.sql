select DATEADD(d, DATEDIFF(d, 0, t.RegisterDate), 0) as [Date], SUM(DATEDIFF(MINUTE, StartWork , EndWork )) /60 as [Appropriated Hours]
from TimeSheets t where  t.demandID in (@demandID) group by DATEADD(d, DATEDIFF(d, 0, t.RegisterDate), 0)
