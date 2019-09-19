
select t.registerDate,  
u.userName,
u.totalCost,
t.startWork, 
t.endWork, 

d.demandCode + ' - ' + d.demandName as Demand,
v.parameterValue as Activity,
c.customerName as Empresa,
t.isApproved as [Status],
t.description
from TimeReleases t
join Users u on t.createByID = u.userID
join Demands d on t.demandID = d.demandID
join ParameterValues v on t.activityID = v.parameterValueID
join Customers c on c.customerID = d.customerID
where c.customerID in (3) and t.registerDate >= '2019-01-01' and t.registerDate <= '2019-01-31' order by 1 desc

