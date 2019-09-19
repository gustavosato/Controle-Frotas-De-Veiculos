select u.UserName [User Name], count(t.timeReleaseID) Amount 
from TimeSheets t inner join Users u on t.CreateByID = u.userID 
where t.demandID in (@demandID) group by u.UserName order by u.UserName
