select p.Value [Activity Type], count(t.timeReleaseID) Amount 
from TimeSheets t join ParameterValues p on t.ActivityID = p.valueID 
where t.demandID in (@demandID) group by p.valueID, p.Value order by p.valueID
