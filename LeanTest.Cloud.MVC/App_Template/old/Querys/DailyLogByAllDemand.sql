select de.externalCode + ' - ' + de.DemandName as Demand, d.Summary as [Summary daily log], u.UserName + ' (' + u.Login + ')' as [Create by], 
d. DisplayedStatusReport as [Displayed Status Report], d.CreationDate as [Creation Date]
from DailyLogs d
inner join Users u on d.CreatedByID = u.userID
inner join Demands de on d.demandID = de.demandID
Where demandID in (@demandID)
order by Demand, d.CreationDate desc