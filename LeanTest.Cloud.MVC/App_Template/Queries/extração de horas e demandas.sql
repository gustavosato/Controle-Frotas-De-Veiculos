SELECT d.demandID, d.isActive, demandName, demandCode, externalCode, p.parameterValue as statusID, p1.parameterValue as typeID, 
p2.parameterValue as serviceID, 
FORMAT(Convert(datetime, planningStartDate, 103), 'ddd dd/MM/yyyy') as planningStartDate, 
FORMAT(Convert(datetime, planningEndDate, 103), 'ddd dd/MM/yyyy') as planningEndDate, u.userName as assignToTargetID, 
co.contactName as responsibleID, d.managementEffort, d.planningEffort, d.executionEffort,
PlanningEffort = (Convert(int,d.managementEffort) + Convert(int,d.planningEffort) + Convert(int,d.executionEffort)),
totalTime = (SELECT SUM(DATEDIFF(MINUTE, t.startWork, t.endWork)/60) as TotalTime FROM TimeReleases t INNER JOIN Demands d1 on t.demandID = d1.demandID WHERE d1.demandID = d.demandID )
FROM Demands d Join ParameterValues p on d.statusID = p.parameterValueID 
Join ParameterValues p1 on d.typeID = p1.parameterValueID 
Inner Join ParameterValues p2 on d.serviceID = p2.parameterValueID 
join Users u on d.assignToTargetID = u.userID 
Inner Join customers c on d.customerID = c.customerID 
left join Contacts co on d.responsibleID = co.contactID 
Where c.customerID = 3 ORDER BY d.demandID DESC