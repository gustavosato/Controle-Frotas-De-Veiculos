select p.Value [Priority], count(d.IDDefect) Amount 
from Defects d join ParameterValues p on d.priorityID = p.valueID 
where d.demandID in (@demandID) group by p.valueID, p.Value order by p.valueID
