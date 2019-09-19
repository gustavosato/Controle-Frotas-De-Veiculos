--status execution tests type execution manual 
select p.Value [Status Execution], COUNT(s.IDScenario) Amount 
from ScenariosTestPlan st inner join 
Scenarios s on st.IDScenario = s.IDScenario inner join 
ParameterValues p on s.IDStatusExecution = p.valueID 
where s.demandID in (@demandID) and s.IDTypeExecution = 88 Group by p.valueID, p.Value order by p.valueID
