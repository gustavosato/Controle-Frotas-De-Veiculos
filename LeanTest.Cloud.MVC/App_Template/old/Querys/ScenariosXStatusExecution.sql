select p.Value [Status Execution], count(s.IDScenario) Amount 
from Scenarios s inner join 
ParameterValues p on s.IDStatusExecution = p.valueID 
where s.demandID in (@demandID) group by p.valueID, p.Value order by p.valueID
