select p.Value [Status], count(t.timeReleaseID) Amount 
from TimeSheets t join ParameterValues p on t.StatusID = p.valueID 
where t.demandID in (@demandID) group by p.valueID, p.Value order by p.valueID
