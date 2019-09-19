
SELECT timeReleaseID, registerDate, startWork, endWork, 
CONVERT(VARCHAR(5), DATEADD(Minute, DATEDIFF(MINUTE, startWork, endWork), 0), 114) as TotalTime, 
c.customerName as customerID, (d.demandCode + ' - ' + d.demandName) as demandID, isApproved, v.parameterValue as activityID, u.userName as createdByID 
FROM TimeReleases t 
INNER JOIN Demands d on t.demandID = d.demandID 
INNER JOIN Customers c on c.customerID = d.customerID 
INNER JOIN ParameterValues v on t.activityID = v.parameterValueID 
INNER JOIN Users u on t.createdByID = u.userID 
WHERE 1 = 1 AND Convert(date, t.registerDate, 103) >= Convert(date, '01/02/2019', 103) AND Convert(date, t.registerDate, 103) <= Convert(date, '28/02/2019', 103) 
ORDER BY Convert(datetime, registerDate, 103) Desc, t.StartWork ASC

select 
u.userName as Nome,
replace(cast(Convert(decimal, SUM(DATEDIFF(MINUTE, t.startWork , t.endWork )), 114) /60 as decimal(18,2)), '.', ',') as [Horas Apropriadas]
from TimeReleases t
join Users u on t.createdByID = u.userID
join Demands d on t.demandID = d.demandID
join ParameterValues v on t.activityID = v.parameterValueID
join Customers c on c.customerID = d.customerID
where Convert(datetime, t.registerDate, 103) between Convert(datetime, '01/04/2019', 103) AND Convert(datetime, '30/04/2019', 103)  
Group By u.userName
Order By 1

select d.demandID, d.demandCode, d.demandName, d.planningStartDate, d.planningEndDate, u.userName as assignToTargetID, pv.parameterValue as typeID,
pv1.parameterValue as statusID, pv2.parameterValue as serviceID, c.customerName as customerID,  u1.userName as createdByID, d.creationDate,
totalAppropriateHours = (SELECT replace(cast(Convert(decimal, SUM(DATEDIFF(MINUTE, t.startWork , t.endWork )), 114) /60 as decimal(18,2)), '.', ',') as [Horas Apropriadas]
FROM TimeReleases t WHERE t.demandID = d.demandID and Convert(datetime, registerDate, 103) between Convert(datetime, '01/08/2018', 103) AND Convert(datetime, '30/04/2019', 103))
from Demands d
inner join users u on d.assignToTargetID = u.userID
inner join ParameterValues pv on d.typeID = pv.parameterValueID
inner join ParameterValues pv1 on d.statusID = pv1.parameterValueID
inner join ParameterValues pv2 on d.serviceID = pv2.parameterValueID
inner join Customers c on d.customerID = c.customerID
inner join users u1 on d.createdByID = u1.userID

