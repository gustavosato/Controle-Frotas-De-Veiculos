using ControleVeiculos.Domain.Entities.Demands;
using ControleVeiculos.Domain.Repositories;
using System.Data;
using Dapper;
using System.Linq;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using ControleVeiculos.Repository.Map;
using Dapper.Contrib.Extensions;
using ControleVeiculos.Domain.Command.Demands;
using ControleVeiculos.Domain.Entities.DemandsUsers;

namespace ControleVeiculos.Repository.Data
{
    public class DemandRepository : BaseRepository, IDemandRepository
    {
        public string Add(int userID, Demand demand)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT ISNULL(MAX(CAST(demandID AS INT))+1, 1) FROM dbo.Demands");

                int primaryKey = conn.Query<int>(sql).FirstOrDefault();

                string tempCode = Convert.ToString(primaryKey);

                tempCode = new string('0', 5 - tempCode.Length) + primaryKey;

                demand.demandCode = "RPLT_" + Convert.ToDateTime(DateTime.Today).ToString("yy.MM." + tempCode);

                DemandDapper demandDapper = demand.Map(primaryKey);

                conn.Insert<DemandDapper>(demandDapper);

                //include user created by new demand
                DemandUser demandUser = new DemandUser();

                //associate the creator demand
                demandUser.demandID = primaryKey;
                demandUser.userID = userID;

                DemandUserDapper demandUserDapper = demandUser.Map();

                conn.Insert<DemandUserDapper>(demandUserDapper);

                //associate the associate target
                if (userID != Convert.ToInt32(demand.assignToTargetID))
                {
                    demandUser.demandID = primaryKey;
                    demandUser.userID = Convert.ToInt32(demand.assignToTargetID);

                    demandUserDapper = demandUser.Map();

                    conn.Insert<DemandUserDapper>(demandUserDapper);
                }

                return primaryKey.ToString();
            }
        }

        public void Update(Demand demand)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                DemandDapper demandDapper = demand.Map(demand.demandID);

                conn.Update<DemandDapper>(demandDapper);
            }
        }

        public Demand GetByID(int demandID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT * FROM dbo.Demands WHERE demandID = '{0}'", demandID);

                return conn.Query<Demand>(sql).FirstOrDefault();
            }
        }

        public List<Demand> GetAll(string customerID, FilterDemandCommand command)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql;
                if (!string.IsNullOrEmpty(customerID))
                {
                    sql = string.Format("SELECT d.demandID, d.isActive, demandName, demandCode, externalCode, p.parameterValue as statusID, p1.parameterValue as typeID, p2.parameterValue as serviceID, " +
                                        "FORMAT(Convert(datetime, planningStartDate, 103), 'dd/MM/yyyy') as planningStartDate, " +
                                        "FORMAT(Convert(datetime, planningEndDate, 103), 'dd/MM/yyyy') as planningEndDate, " +
                                        "u.userName as assignToTargetID, co.contactName as responsibleID, " +
                                        "PlanningEffort = (Convert(int,d.managementEffort) + Convert(int,d.planningEffort) + Convert(int,d.executionEffort)), " +
                                        "totalTime = (SELECT SUM(DATEDIFF(MINUTE, t.startWork, t.endWork)/60) as TotalTime FROM TimeReleases t INNER JOIN Demands d1 on t.demandID = d1.demandID WHERE d1.demandID = d.demandID )" +
                                        "FROM Demands d Join ParameterValues p on d.statusID = p.parameterValueID Join ParameterValues p1 on d.typeID = p1.parameterValueID " +
                                        "Inner Join ParameterValues p2 on d.serviceID = p2.parameterValueID join Users u on d.assignToTargetID = u.userID " +
                                        "Inner Join customers c on d.customerID = c.customerID " +
                                        "left join Contacts co on d.responsibleID = co.contactID " +
                                        "Where c.customerID = {0} ", customerID);

                    if (!string.IsNullOrEmpty(command.DemandName))
                        sql += string.Format("AND d.demandName LIKE '%{0}%' ", command.DemandName);

                    if (!string.IsNullOrEmpty(command.StatusID))
                        sql += string.Format("AND d.statusID = '{0}' ", command.StatusID);

                    if (!string.IsNullOrEmpty(command.DemandCode))
                        sql += string.Format("AND d.demandCode LIKE '%{0}%' ", command.DemandCode);

                    if (!string.IsNullOrEmpty(command.TypeID))
                        sql += string.Format("AND d.typeID = '{0}' ", command.TypeID);

                    if (!string.IsNullOrEmpty(command.ServiceID))
                        sql += string.Format("AND d.serviceID = '{0}' ", command.ServiceID);

                    if (!string.IsNullOrEmpty(command.ExternalCode))
                        sql += string.Format("AND d.externalCode LIKE '%{0}%' ", command.ExternalCode);

                    if (!string.IsNullOrEmpty(command.ResponsibleID))
                        sql += string.Format("AND d.responsibleID = '{0}' ", command.ResponsibleID);

                    if (!string.IsNullOrEmpty(command.PlanningStartDate))
                        sql += string.Format("AND Convert(date, d.planningStartDate, 103) >= Convert(date, '{0}', 103) ", command.PlanningStartDate);

                    if (!string.IsNullOrEmpty(command.PlanningEndDate))
                        sql += string.Format("AND Convert(date, d.planningEndDate, 103) <= Convert(date, '{0}', 103) ", command.PlanningEndDate);
                           
                    if (!string.IsNullOrEmpty(command.AssignToTargetID))
                        sql += string.Format("AND d.assignToTargetID = '{0}'", command.AssignToTargetID);

                    if (command.IsActive)
                        sql += string.Format("AND d.statusID <> '{0}' ", "200200206"); //status encerrado

                    sql += "ORDER BY d.demandID DESC";
                }
                else
                {
                    sql = "select * from demands where demandID = 0 ";
                }
                return conn.Query<Demand>(sql).ToList();
            }
        }

        public void Delete(int demandID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("DELETE FROM dbo.Demands WHERE demandID = '{0}'", demandID.ToString());
                conn.ExecuteScalar(sql);
            }
        }

        public string GetDemandNameByID(int demandID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT userName FROM dbo.Demands WHERE userID = {0}", demandID);

                return conn.Query<string>(sql).FirstOrDefault();
            }
        }

        public List<Demand> GetAll(string customerID, string demandID, string userID, bool isAssociated)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = null;
                //only associated demands users
                if (isAssociated) demandID = "0";
                if (!string.IsNullOrEmpty(customerID))
                {
                    if (demandID == null)
                    {
                        sql = string.Format("Select Distinct d.demandID, demandCode + ' - ' + demandName as demandName " +
                                            "From Demands d " +
                                            "Inner Join DemandsUsers du on d.demandID = du.demandID " +
                                            "Where d.isActive = 'True'and d.customerID = {0} And du.userID = {1} And d.statusID <> '200200206' ", customerID, userID);
                    }
                    else
                    {
                        sql = string.Format("Select Distinct d.demandID, demandCode + ' - ' + demandName as demandName " +
                                            "From Demands d " +
                                            "Inner Join DemandsUsers du on d.demandID = du.demandID " +
                                            "Where d.isActive = 'True' and d.customerID = {0} And du.userID = {2} And d.statusID <> '200200206' Or d.demandID = {1} ", customerID, demandID, userID);
                    }
                }
                else
                {
                    sql = string.Format("Select Distinct d.demandID, demandCode + ' - ' + demandName as demandName " +
                                    "From Demands d " +
                                    "Inner Join DemandsUsers du on d.demandID = du.demandID " +
                                    "Where d.isActive = 'True' and d.customerID = {0} And du.userID = {2}  And d.statusID <> '200200206' Or d.demandID = {1}  ", "0", demandID, userID);

                }


                sql += "ORDER BY 2 DESC";

                return conn.Query<Demand>(sql).ToList();
            }
        }

        public List<Demand> GetAllByCustomerID(string customerID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql;

                sql = string.Format("Select d.demandID, demandCode + ' - ' + demandName as demandName From Demands d " +
                                    "Join customers c on d.customerID = c.customerID " +
                                    "Where c.customerID = '{0}'  And d.statusID <> '200200206' ", customerID);

                sql += "ORDER BY demandCode DESC";

                return conn.Query<Demand>(sql).ToList();
            }
        }

        public string GetTotalHoursByDemandID(string demandID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT SUM(DATEDIFF(MINUTE, startWork, endWork)) as TotalTime " +
                                            "FROM TimeReleases t " +
                                            "INNER JOIN Demands d on t.demandID = d.demandID " +
                                            "WHERE d.demandID = {0} ", demandID);

                sql = conn.Query<string>(sql).FirstOrDefault();

                if (sql == null) sql = "0";
                int totalminutes = Convert.ToInt32(sql);
                string hours = (totalminutes / 60).ToString();
                string minutes = (totalminutes % 60).ToString();
                hours = (hours.Length > 1 ? hours : "0" + hours);
                minutes = (minutes.Length > 1 ? minutes : "0" + minutes);

                return string.Format("{0}:{1}:00", hours, minutes);
            }
        }

        public List<Demand> GetAllByTimeReleaseByContact(string customerID, FilterDemandCommand command)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql;
                if (!string.IsNullOrEmpty(customerID))
                {
                    sql = string.Format("SELECT c.customerName as customerID, co.contactName as responsibleID, (d.demandCode + ' - ' + d.demandName) as demandName, " +
                                        "u.userName as createdByID, FORMAT(Convert(datetime, registerDate, 103), 'ddd dd/MM/yyyy') as CreationDate, startWork as PlanningStartDate, endWork as PlanningEndDate, " +
                                        "CONVERT(VARCHAR(5), DATEADD(Minute, DATEDIFF(MINUTE, startWork, endWork), 0), 114) as TotalTime, isApproved, v.parameterValue as typeID, " +
                                        "t.description " +
                                        "FROM TimeReleases t " +
                                        "INNER JOIN Demands d on t.demandID = d.demandID " +
                                        "INNER JOIN Customers c on c.customerID = d.customerID " +
                                        "INNER JOIN ParameterValues v on t.activityID = v.parameterValueID " +
                                        "LEFT JOIN contacts co on d.responsibleID = co.contactID " +
                                        "LEFT JOIN users u on t.createdByID = u.userID " +
                                        "WHERE 1 = 1  AND c.customerID = {0} ", customerID);

                    if (!string.IsNullOrEmpty(command.ResponsibleID))
                        sql += string.Format("AND d.responsibleID = '{0}' ", command.ResponsibleID);

                    if (!string.IsNullOrEmpty(command.CreatedByID))
                        sql += string.Format("AND d.createdByID = '{0}' ", command.CreatedByID);

                    if (!string.IsNullOrEmpty(command.RegisterDateFrom))
                        sql += string.Format("AND Convert(date, t.registerDate, 103) >= Convert(date, '{0}', 103) ", command.RegisterDateFrom);

                    if (!string.IsNullOrEmpty(command.RegisterDateTo))
                        sql += string.Format("AND Convert(date, t.registerDate, 103) <= Convert(date, '{0}', 103) ", command.RegisterDateTo);

                    if (command.IsActive)
                        sql += string.Format("AND d.statusID <> '{0}' ", "200200206"); //status encerrado

                    sql += "ORDER BY co.contactName, d.demandCode, Convert(date, t.registerDate, 103)";
                }
                else
                {
                    sql = "select * from demands where demandID = 0 ";
                }
                return conn.Query<Demand>(sql).ToList();
            }
        }

        public List<Demand> GetAllGantt(string customerID, FilterDemandCommand command)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql;
                if (!string.IsNullOrEmpty(customerID))
                {
                    sql = string.Format("SELECT d.demandID, demandName, demandCode, externalCode, p.parameterValue as statusID, p1.parameterValue as typeID, p2.parameterValue as serviceID, " +
                                        "u.userName as assignToTargetID, planningStartDate, planningEndDate, co.contactName as responsibleID " +
                                        "FROM Demands d Join ParameterValues p on d.statusID = p.parameterValueID Join ParameterValues p1 on d.typeID = p1.parameterValueID " +
                                        "Inner Join ParameterValues p2 on d.serviceID = p2.parameterValueID join Users u on d.assignToTargetID = u.userID " +
                                        "Inner Join customers c on d.customerID = c.customerID " +
                                        "left join Contacts co on d.responsibleID = co.contactID " +
                                        "Where c.customerID = {0} ", customerID);

                    if (!string.IsNullOrEmpty(command.DemandName))
                        sql += string.Format("AND d.demandName LIKE '%{0}%' ", command.DemandName);

                    if (!string.IsNullOrEmpty(command.StatusID))
                        sql += string.Format("AND d.statusID = '{0}' ", command.StatusID);

                    if (!string.IsNullOrEmpty(command.DemandCode))
                        sql += string.Format("AND d.demandCode LIKE '%{0}%' ", command.DemandCode);

                    if (!string.IsNullOrEmpty(command.TypeID))
                        sql += string.Format("AND d.typeID = '{0}' ", command.TypeID);

                    if (!string.IsNullOrEmpty(command.ServiceID))
                        sql += string.Format("AND d.serviceID = '{0}' ", command.ServiceID);

                    if (!string.IsNullOrEmpty(command.ExternalCode))
                        sql += string.Format("AND d.externalCode LIKE '%{0}%' ", command.ExternalCode);

                    if (!string.IsNullOrEmpty(command.ResponsibleID))
                        sql += string.Format("AND d.responsibleID = '{0}' ", command.ResponsibleID);


                    if (command.IsActive)
                        sql += string.Format("AND d.statusID <> '{0}' ", "200200206"); //status encerrado

                    sql += "ORDER BY d.demandID DESC";
                }
                else
                {
                    sql = "select * from demands where demandID = 0 ";
                }
                return conn.Query<Demand>(sql).ToList();
            }
        }
    }
}
