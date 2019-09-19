Select d.demandID, c.customerName,
d.demandCode + ' - ' + d.demandName as demandName, 
CAST(d.managementEffort as float) + CAST(d.planningEffort as float) + CAST(d.executionEffort as float) as Total,
SUM(DATEDIFF(MINUTE, t.startWork , t.endWork )) /60 as [Horas Apropriadas]

From TimeReleases t
INNER JOIN Demands d on t.demandID = d.demandID
INNER JOIN Customers c on d.customerID = c.customerID
Where t.registerDate >= '2019-01-01' And t.registerDate <= '2019-01-31' and c.customerID = 3
Group By d.demandID, c.customerName, d.demandCode + ' - ' + d.demandName, d.managementEffort, d.planningEffort, d.executionEffort
Order By d.demandID Desc
