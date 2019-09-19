select p.Value as Status,  count(*) as Total  from defects d, ParameterValues p 
where d.statusID = p.valueID and d.demandID = @demandID Group by p.Value
union

select 'Total',  count(*) as 'Count' from defects where demandID = @demandID
