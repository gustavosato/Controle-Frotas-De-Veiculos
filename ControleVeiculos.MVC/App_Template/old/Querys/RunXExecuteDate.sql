select DATEADD(d, DATEDIFF(d, 0, r.StartExecution), 0) as [Execution date], count(*) as Amount 
from Runs r 
join ScenariosTestPlan st on r.IDSuiteTestTestPlan = st.IDScenarioTestPlan 
join Scenarios s on st.IDScenario = s.IDScenario 
where s.demandID not in (@demandID) and s.IDStatusExecution in(59, 60, 62) 
group by DATEADD(d, DATEDIFF(d, 0, r.StartExecution), 0) Order by 1 Asc
