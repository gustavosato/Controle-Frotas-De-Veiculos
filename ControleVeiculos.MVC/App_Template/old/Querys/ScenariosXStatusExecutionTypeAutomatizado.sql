--status execution cenarios type execution manual 
select p.Value [Status Execution], count(s.IDScenario) Amount 
from Scenarios s inner join 
ParameterValues p on s.IDStatusExecution = p.valueID 
where s.demandID in (@demandID) and s.IDTypeExecution = 88 group by p.valueID, p.Value order by p.valueID
