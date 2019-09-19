SELECT
    COUNT(*) AS Total
FROM
    Scenarios
WHERE CONVERT(DATE, CONVERT(VARCHAR(10), StartExecution, 103), 103) <= CONVERT(DATE, '@date', 103)
AND IDStatusExecution in (@idStatusExecution) AND ISFolder = 0 AND demandID = @demandID

