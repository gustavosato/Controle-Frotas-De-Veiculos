select c.ChangeRequestID as ID, c.Summary, CONVERT(varchar, c.Effort) + ' hrs' as Effort, v1.Value as [Status], 
c.TargetDate as [Target Date], u.UserName + ' (' + u.Login + ')' as [Request By],
c.ApprovedDate as [Approved Date],
u1.UserName + ' (' + u1.Login + ')' as [Create By],
c.CreationDate as [Creation Date] 
from ChangeRequests c
inner join ParameterValues v1 on c.StatusID = v1.valueID
inner join Users u on c.RequestByID = u.userID
inner join Users u1 on c.RequestByID = u1.userID
Where c.demandID in (@demandID)
order by ID desc