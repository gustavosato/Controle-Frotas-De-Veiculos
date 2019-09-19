<<<<<<< HEAD
<<<<<<< HEAD
select t.timeReleaseID, u1.userName as createdByID, t.registerDate, t.startWork, t.endWork, ISNULL(d.demandCode + ' - ' + d.demandName, 'Registro de ausÃªncia') as demandID, 
t.isApproved, pv.parameterValue as activityID, u.userName as approvedByID, t.approvedDate, t.description, t.creationDate
=======
select t.timeReleaseID, t.registerDate, t.startWork, t.endWork, ISNULL(d.demandCode + ' - ' + d.demandName, 'Registro de ausência') as demandID, 
t.isApproved, pv.parameterValue as activityID, u.userName as approvedByID, t.approvedDate, t.description, u1.userName as createdByID, t.creationDate
>>>>>>> parent of 23d3dc2... update
=======
select t.timeReleaseID, t.registerDate, t.startWork, t.endWork, ISNULL(d.demandCode + ' - ' + d.demandName, 'Registro de ausência') as demandID, 
t.isApproved, pv.parameterValue as activityID, u.userName as approvedByID, t.approvedDate, t.description, u1.userName as createdByID, t.creationDate
>>>>>>> parent of 23d3dc2... update
from TimeReleases t
left join Demands d on t.demandID = d.demandID
left join ParameterValues pv on t.activityID = pv.parameterValueID
left join Users u on t.approvedByID = u.userID
left join Users u1 on t.createdByID = u1.userID
where Convert(datetime, registerDate, 103) between Convert(datetime, '01/04/2019', 103) and Convert(datetime, '30/04/2019', 103)


update TimeReleases set approvedDate = Convert(datetime, lastModifiedDate, 103) where approvedDate is null

<<<<<<< HEAD
<<<<<<< HEAD
select Convert(datetime, registerDate, 115) from TimeReleases
=======
select Convert(datetime, registerDate, 115) from TimeReleases
>>>>>>> parent of 23d3dc2... update
=======
select Convert(datetime, registerDate, 115) from TimeReleases
>>>>>>> parent of 23d3dc2... update
