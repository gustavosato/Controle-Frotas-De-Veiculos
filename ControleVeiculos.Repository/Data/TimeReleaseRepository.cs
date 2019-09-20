using ControleVeiculos.Domain.Entities.TimeReleases;
using ControleVeiculos.Domain.Repositories;
using System.Data;
using Dapper;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using ControleVeiculos.Repository.Map;
using Dapper.Contrib.Extensions;
using ControleVeiculos.Domain.Command.TimeReleases;
using System;

namespace ControleVeiculos.Repository.Data
{
    public class TimeReleaseRepository : BaseRepository, ITimeReleaseRepository
    {
        public string Add(TimeRelease timeRelease)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT ISNULL(MAX(CAST(timeReleaseID AS INT))+1,1) FROM dbo.TimeReleases");
                int primaryKey = conn.Query<int>(sql).FirstOrDefault();
                TimeReleaseDapper timeReleaseDapper = timeRelease.Map(primaryKey);
                try
                { 
                conn.Insert<TimeReleaseDapper>(timeReleaseDapper);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
              return primaryKey.ToString();
            }
        }

        public void Update(TimeRelease timeRelease)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                TimeReleaseDapper timeReleaseDapper = timeRelease.Map(timeRelease.timeReleaseID);

                conn.Update<TimeReleaseDapper>(timeReleaseDapper);
            }
        }

        public TimeRelease GetByID(int timeReleaseID)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT * FROM dbo.TimeReleases WHERE timeReleaseID = '{0}'", timeReleaseID);

                return conn.Query<TimeRelease>(sql).FirstOrDefault();
            }
        }

        public string GetAbsence(int userID, string registerDate)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("select count(*) as TotalTime from TimeReleases where startWork = '00:00' AND endWork = '00:00' AND createdByID = {0} AND registerDate = '{1}'", userID, registerDate);

                return conn.Query<string>(sql).FirstOrDefault();
            }
        }

        public string GetNotAbsence(int userID, string registerDate)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("select count(*) as TotalTime from TimeReleases where createdByID = {0} AND registerDate = '{1}'", userID, registerDate);

                return conn.Query<string>(sql).FirstOrDefault();
            }
        }

        public string GetApropriateByRangeTime(int timeReleaseID, int createdByID, string registerDate, string startWork, string endWork)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string sql = string.Format("SELECT timeReleaseID FROM TimeReleases " +
                                           "WHERE createdByID = {0} AND registerDate = '{1}' AND '{3}' > startWork AND endWork > '{2}'" +
                                           "AND timeReleaseID <> {4}", createdByID, registerDate, startWork, endWork, timeReleaseID);



                //"SELECT timeReleaseID FROM TimeReleases " +
                //"WHERE createdByID = {0} AND registerDate = '{1}' AND startWork between '{2}' AND '{3}' AND timeReleaseID <> {4} " +
                //"OR createdByID = {0} AND registerDate = '{1}' AND endWork between '{2}' AND '{3}' AND timeReleaseID <> {4} ", createByID, registerDate, startDate, endDate, timeReleaseID);


                return conn.Query<string>(sql).FirstOrDefault();
            }
        }



        public List<TimeRelease> GetAll(FilterTimeReleaseCommand command)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT timeReleaseID, FORMAT(Convert(datetime, registerDate, 103), 'ddd dd/MM/yyyy') as registerDate, startWork, endWork, " +
                    "CONVERT(VARCHAR(5), DATEADD(Minute, DATEDIFF(MINUTE, startWork, endWork), 0), 114) as TotalTime, " +
                    "c.customerName as customerID, ISNULL((d.demandCode + ' - ' + d.demandName), 'Registro de Ausência') as demandID, isApproved, v.parameterValue as activityID, u.userName as createdByID " +
                    "FROM TimeReleases t " +
                    "LEFT JOIN Demands d on t.demandID = d.demandID " +
                    "LEFT JOIN Customers c on c.customerID = d.customerID " +
                    "LEFT JOIN ParameterValues v on t.activityID = v.parameterValueID " +
                    "INNER JOIN Users u on t.createdByID = u.userID " +
                    "WHERE 1 = 1 ");

                if (!string.IsNullOrEmpty(command.ActivityID))
                    sql += string.Format("AND t.activityID = '{0}' ", command.ActivityID);

                if (!string.IsNullOrEmpty(command.DemandID)) 
                    sql += string.Format("AND t.demandID = '{0}' ", command.DemandID);

                if (!string.IsNullOrEmpty(command.RegisterDate))
                    sql += string.Format("AND Convert(date, t.registerDate, 103) = Convert(date, '{0}', 103) ", command.RegisterDate);

                if (!string.IsNullOrEmpty(command.StartDate))
                    sql += string.Format("AND Convert(date, t.registerDate, 103) >= Convert(date, '{0}', 103) ", command.StartDate);

                if (!string.IsNullOrEmpty(command.EndDate))
                    sql += string.Format("AND Convert(date, t.registerDate, 103) <= Convert(date, '{0}', 103) ", command.EndDate);

                if (!string.IsNullOrEmpty(command.CreatedByID))
                    sql += string.Format("AND t.createdByID = '{0}' ", command.CreatedByID);

                if (!string.IsNullOrEmpty(command.CustomerID))
                    sql += string.Format("AND c.customerID = '{0}' ", command.CustomerID);
                
                sql += "ORDER BY Convert(datetime, registerDate, 103) Desc, t.StartWork ASC";

                return conn.Query<TimeRelease> (sql).ToList();
            }
        }

        public string GetTotalHours(string userID, string startDate, string endDate)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("SELECT SUM(DATEDIFF(MINUTE, startWork, endWork))  as TotalTime " +
                                           "FROM TimeReleases t " +
                                           "WHERE t.createdByID = {0} AND  Convert(datetime, registerDate, 103) between Convert(datetime, '{1}', 103) AND Convert(datetime, '{2}', 103) ", userID, startDate, endDate);

                sql = (conn.Query<string>(sql).FirstOrDefault() != null ? conn.Query<string>(sql).FirstOrDefault() : "0");

                int totalminutes = Convert.ToInt32(sql);

                string hours = (totalminutes / 60).ToString();

                string minutes = (totalminutes % 60).ToString();

                hours = (hours.Length > 1 ? hours : "0" + hours);

                minutes = (minutes.Length > 1 ? minutes : "0" + minutes);

                return string.Format("{0}:{1}", hours, minutes);

            }
        }

        public void Delete(int timeReleaseID)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("DELETE FROM dbo.TimeReleases WHERE timeReleaseID = '{0}'", timeReleaseID);
                conn.ExecuteScalar(sql);
            }
        }

        //report
        public List<TimeRelease> GetTotalByUsers(FilterTimeReleaseCommand command)
        {
            using (IDbConnection conn = new SqlConnection())
            {
                conn.ConnectionString = this.ConnectionString;
                conn.Open();

                string sql = string.Format("select u.userName as createdByID, t.isApproved, " +
                                           "replace(cast(Convert(decimal, SUM(DATEDIFF(MINUTE, t.startWork, t.endWork)), 114) / 60 as decimal(18, 2)), '.', ',') as totalTime " +
                                           "from TimeReleases t " +
                                           "join Users u on t.createdByID = u.userID " +
                                           "join Demands d on t.demandID = d.demandID " +
                                           "join ParameterValues v on t.activityID = v.parameterValueID " +
                                           "join Customers c on c.customerID = d.customerID " +
                                           "where 1 = 1 ");

                if (!string.IsNullOrEmpty(command.ActivityID))
                    sql += string.Format("AND t.activityID = '{0}' ", command.ActivityID);

                if (!string.IsNullOrEmpty(command.DemandID))
                    sql += string.Format("AND t.demandID = '{0}' ", command.DemandID);

                if (!string.IsNullOrEmpty(command.RegisterDate))
                    sql += string.Format("AND Convert(date, t.registerDate, 103) = Convert(date, '{0}', 103) ", command.RegisterDate);

                if (!string.IsNullOrEmpty(command.StartDate))
                    sql += string.Format("AND Convert(date, t.registerDate, 103) >= Convert(date, '{0}', 103) ", command.StartDate);

                if (!string.IsNullOrEmpty(command.EndDate))
                    sql += string.Format("AND Convert(date, t.registerDate, 103) <= Convert(date, '{0}', 103) ", command.EndDate);

                if (!string.IsNullOrEmpty(command.CreatedByID))
                    sql += string.Format("AND t.createdByID = '{0}' ", command.CreatedByID);

                if (!string.IsNullOrEmpty(command.CustomerID))
                    sql += string.Format("AND c.customerID = '{0}' ", command.CustomerID);

                sql += "Group By u.userName, t.isApproved Order By 1";

                return conn.Query<TimeRelease>(sql).ToList();
            }
        }

        public List<TimeRelease> GetTotalByUsersNoPage(FilterTimeReleaseCommand command)
        {
           using (IDbConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = this.ConnectionString;
                    conn.Open();

                    string sql = string.Format("select u.userName as createdByID," +
                                                "CAST(SUM(CASE WHEN t.isApproved = 0 THEN (Convert(decimal,DATEDIFF(MINUTE, t.startWork, t.endWork),114)/60) ELSE 0 END)AS decimal(18, 2)) startWork, " +
                                                "CAST(SUM(CASE WHEN t.isApproved = 1 THEN (Convert(decimal,DATEDIFF(MINUTE, t.startWork, t.endWork),114)/60) ELSE 0 END)AS decimal(18, 2)) endWork, " +
                                                "replace(cast(Convert(decimal, SUM(DATEDIFF(MINUTE, t.startWork, t.endWork)), 114) / 60 as decimal(18, 2)), '.', ',') as totalTime " +
                                                "from TimeReleases t " +
                                                "join Users u on t.createdByID = u.userID " +
                                                "where 1 = 1 ");

                    if (!string.IsNullOrEmpty(command.StartDate))
                        sql += string.Format("AND Convert(date, t.registerDate, 103) >= Convert(date, '{0}', 103) ", command.StartDate);

                    if (!string.IsNullOrEmpty(command.EndDate))
                        sql += string.Format("AND Convert(date, t.registerDate, 103) <= Convert(date, '{0}', 103) ", command.EndDate);

                    if (!string.IsNullOrEmpty(command.CreatedByID))
                        sql += string.Format("AND t.createdByID = '{0}' ", command.CreatedByID);

                    if (!string.IsNullOrEmpty(command.CustomerID))
                        sql += string.Format("AND c.customerID = '{0}' ", command.CustomerID);

                    sql += "Group By u.userName Order By 1";

                    return conn.Query<TimeRelease>(sql).ToList();
            }
        }

    }
}
