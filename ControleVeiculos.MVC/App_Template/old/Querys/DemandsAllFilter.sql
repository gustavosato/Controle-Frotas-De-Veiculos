Select d.demandID as ID,  d.externalCode + ' - ' + d.DemandName as Demand
From Demands d
Where d.environmentID = @environmentID --@where
order by Demand desc