select d.DailyLogsID as ID, 
d.Summary, u.UserName + ' (' + u.Login + ')' as [Create by], 
d.CreationDate as [Creation Date]
from DailyLogs d
inner join Users u on d.CreatedByID = u.userID
inner join Demands de on d.demandID = de.demandID
Where demandID in (@demandID) AND d.DisplayedStatusReport = 'True'
order by d.CreationDate desc