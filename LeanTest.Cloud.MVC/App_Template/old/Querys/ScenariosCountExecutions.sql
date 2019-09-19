--declare @environmentID int
--set @environmentID = 2
Create Table #@Temp ([Demand] varchar (400), Scenario varchar(400), [Amount Tests] int, [Amount Runs] int, [Number of Executions of the Scenario] float)
insert into #@Temp

SELECT DISTINCT
D.externalCode + ' - ' + D.DemandName AS [Demand],
s.Scenario,
[Amount Tests] = (SELECT COUNT(*) FROM ScenariosTestPlan ST where st.IDScenario = s.IDScenario),
[Amount Runs] = (SELECT COUNT(*) FROM Runs R WHERE ST.IDScenarioTestPlan = R.IDSuiteTestTestPlan),
case when convert(float, (SELECT COUNT(*) FROM Runs R WHERE ST.IDScenarioTestPlan = R.IDSuiteTestTestPlan)) = 0 then
	0
else
	convert(float, (SELECT COUNT(*) FROM Runs R WHERE ST.IDScenarioTestPlan = R.IDSuiteTestTestPlan)) / convert(float, (SELECT COUNT(*) FROM ScenariosTestPlan ST where st.IDScenario = s.IDScenario))
end
as  [Number of Executions of the Scenario]
FROM Scenarios S

LEFT JOIN ScenariosTestPlan ST on s.IDScenario = ST.IDScenario
INNER JOIN Demands D on S.demandID = D.demandID
WHERE S.ISFolder = 0 and D.environmentID = @environmentID

Create Table #@Temp1 ([Demand] varchar (400), Scenario varchar(400), [Amount Tests] int, [Amount Runs] int)
insert into #@Temp1

select Demand, Scenario, max([Amount Tests]) as [Amount Tests], sum([Amount Runs]) as [Amount Runs]
from #@Temp
Group by Demand, Scenario


Create Table #@Temp2 ([Demand] varchar (400), Scenario varchar(400), [Amount Tests] int, [Amount Runs] int)
insert into #@Temp2


select demand, count(Scenario) as [Total of Scenarios], sum([Amount Tests]) as [Total of Tests], sum([Amount Runs]) as [Total of Runs]
from #@Temp1
Group By Demand

select demand, Scenario as [Total of Scenarios], [Amount Tests] as [Total of Tests], [Amount Runs] as [Total of Runs],
round(avg(convert(float,[Amount Runs]) / convert(float,[Amount Tests])), 2) as [Average Number of Scenario Executions]
from #@Temp2
Where [Amount Tests] > 0
Group By Demand, Scenario, [Amount Tests], [Amount Runs]

drop table #@Temp
drop table #@Temp1
drop table #@Temp2