select p.Value as Status,  count(*) as Total  from issues i, ParameterValues p
where i.statusID = p.valueID and i.demandID = @demandID Group by p.Value
union

select 'Total',  count(*) as 'Count' from issues where demandID = @demandID
