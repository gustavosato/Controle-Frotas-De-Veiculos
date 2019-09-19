select 
u.userName as createdByID,
CAST(SUM(CASE WHEN t.isApproved = 1 THEN (Convert(decimal,DATEDIFF(MINUTE, t.startWork, t.endWork),114)/60) ELSE 0 END)AS decimal(18, 2)) aprovado,
CAST(SUM(CASE WHEN t.isApproved = 0 THEN (Convert(decimal,DATEDIFF(MINUTE, t.startWork, t.endWork),114)/60) ELSE 0 END)AS decimal(18, 2)) desaprovado,
replace(cast(Convert(decimal, SUM(DATEDIFF(MINUTE, t.startWork, t.endWork)), 114) / 60 as decimal(18, 2)), '.', ',') as totalTime
from TimeReleases t
join Users u on t.createdByID = u.userID
where 1 = 1
Group By u.userName
Order By 1