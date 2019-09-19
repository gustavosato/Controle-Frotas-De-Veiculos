select p.Value [Status], count(d.IDDefect) Amount 
from Defects d join ParameterValues p on d.statusID = p.valueID 
where d.demandID in (@demandID) group by p.valueID, p.Value order by p.valueID
