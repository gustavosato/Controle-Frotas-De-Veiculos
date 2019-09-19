select DATEADD(d, DATEDIFF(d, 0, t.RegisterDate), 0) as [Date], u.UserName,  
SUM(DATEDIFF(MINUTE, StartWork , EndWork)) /60 as [Launched Hours]

from TimeSheets t inner join ParameterValues v on t.ActivityID = v.valueID
inner join Users u on t.CreateByID = u.userID
where  t.demandID in (@demandID) group by u.UserName, DATEADD(d, DATEDIFF(d, 0, t.RegisterDate), 0)
