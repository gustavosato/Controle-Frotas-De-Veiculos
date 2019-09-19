SELECT S.IDScenario as [ID Scen], S.Scenario, T.TestPlanName as [Test Case],    
                              T.Descriptions, T.PreCondition as [Pre Condition], STE.StepName as [Step],    
                              STE.Descriptions as [Step Description], STE.PosCondition [Step Expected Resut]   
                              FROM Scenarios S   
                              INNER JOIN ScenariosTestPlan ST ON S.IDScenario = ST.IDScenario   
                              LEFT JOIN TestPlan T ON ST.IDTestPlan = T.IDTestPlan    
                              LEFT JOIN StepsTest STE ON ST.IDTestPlan = STE.IDTestPlan   
                              WHERE s.demandID = @demandID  ORDER BY S.Scenario, STE.Ordem, ST.Ordem 
