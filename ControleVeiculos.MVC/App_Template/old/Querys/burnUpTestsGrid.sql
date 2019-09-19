SELECT 
CONVERT(VARCHAR(11), e.TaregetDate, 103) + ', ' + DATENAME(dw, e.TaregetDate) as [Tareget Date],
e.Planned as [Planned],
convert(varchar, left(e.AccumulatedPercentageOfPlanned * 100, 4)) + '%' AS [% Planned],
e.Executed as [Executed], 
convert(varchar, left(e.AccumulatedPercentageOfExecuted * 100, 4)) + '%' AS [% Executed],
e.RePlaned as [Re-Planned], 
convert(varchar, left(e.AccumulatedPercentageOfRePlanned * 100, 4)) + '%' AS [% Re-Planed]
FROM ElaborationPlanned e
WHERE demandID = @demandID and TypeID = 1
