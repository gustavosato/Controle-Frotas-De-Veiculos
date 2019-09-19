SELECT e.TaregetDate, e.Planned, e.Executed, RePlaned
FROM ElaborationPlanned e
WHERE demandID = @demandID AND TypeID = 1
