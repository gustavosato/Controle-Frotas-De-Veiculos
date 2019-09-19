select p.Value [Severity], count(d.IDDefect) Amount 
from Defects d join ParameterValues p on d.IDSeverity = p.valueID 
where d.demandID in (@demandID) group by p.valueID, p.Value order by p.valueID
