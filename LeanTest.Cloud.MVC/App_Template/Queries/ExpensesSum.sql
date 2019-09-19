select u.userName as createdByID,
'R$ ' + replace(Convert(varchar, SUM(cast(Convert(decimal, replace(e.AmountExpense, ',' , '.'), 114) as decimal(18,2)))), '.', ',') as AmountExpense,
pv.parameterValue as statusID
from Expenses e
inner join Users u on e.createdByID = u.userID
inner join ParameterValues pv on e.statusID = pv.parameterValueID
where Convert(datetime, e.registerDate, 103) between Convert(datetime, '01/04/2019', 103) AND Convert(datetime, '30/04/2019', 103) 
Group By u.userName, pv.parameterValue
Order By 1
