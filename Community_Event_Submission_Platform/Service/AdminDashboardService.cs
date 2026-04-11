using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace Community_Event_Submission_Platform.Service
{
    public class AdminDashboardService
    {
        public static Dictionary<string, string> parameters = new Dictionary<string, string>();

        // ── Total users ──────────────────────────────────────────────────
        public static int GetTotalUsers()
        {
            try
            {
                parameters.Clear();
                var query = "SELECT COUNT(*) AS total FROM users WHERE deleted_date IS NULL";
                DataTable result = SqlRequest.ExecuteQuery(query, parameters);
                return Convert.ToInt32(result.Rows[0]["total"]);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // ── Total events ─────────────────────────────────────────────────
        public static int GetTotalEvents()
        {
            try
            {
                parameters.Clear();
                var query = "SELECT COUNT(*) AS total FROM event_submission";
                DataTable result = SqlRequest.ExecuteQuery(query, parameters);
                return Convert.ToInt32(result.Rows[0]["total"]);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // ── User growth % vs previous month ─────────────────────────────
        public static int GetUserGrowthPercent()
        {
            try
            {
                parameters.Clear();
                var query = @"
                    SELECT CASE WHEN lastMonth = 0 THEN 0
                                ELSE CAST((thisMonth - lastMonth) * 100.0 / lastMonth AS SIGNED)
                           END AS growth
                    FROM (
                        SELECT
                            SUM(CASE WHEN MONTH(created_date) = MONTH(CURDATE())
                                      AND YEAR(created_date)  = YEAR(CURDATE())
                                 THEN 1 ELSE 0 END) AS thisMonth,
                            SUM(CASE WHEN MONTH(created_date) = MONTH(DATE_SUB(CURDATE(), INTERVAL 1 MONTH))
                                      AND YEAR(created_date)  = YEAR(DATE_SUB(CURDATE(), INTERVAL 1 MONTH))
                                 THEN 1 ELSE 0 END) AS lastMonth
                        FROM users
                        WHERE deleted_date IS NULL
                    ) AS counts";

                DataTable result = SqlRequest.ExecuteQuery(query, parameters);
                object val = result.Rows[0]["growth"];
                return val == DBNull.Value || val == null ? 0 : Convert.ToInt32(val);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // ── Chart labels: last 6 months ──────────────────────────────────
        public static string GetChartLabels()
        {
            var labels = new List<string>();
            for (int i = 5; i >= 0; i--)
                labels.Add(DateTime.Now.AddMonths(-i).ToString("MMM"));

            return new JavaScriptSerializer().Serialize(labels);
        }

        // ── Monthly user counts (cumulative) ─────────────────────────────
        public static string GetMonthlyUserCounts()
        {
            try
            {
                parameters.Clear();
                var query = @"
                    SELECT MONTH(created_date) AS m, YEAR(created_date) AS y, COUNT(*) AS cnt
                    FROM users
                    WHERE deleted_date IS NULL
                      AND created_date >= DATE_SUB(DATE_FORMAT(CURDATE(), '%Y-%m-01'), INTERVAL 5 MONTH)
                    GROUP BY YEAR(created_date), MONTH(created_date)
                    ORDER BY y, m";

                DataTable result = SqlRequest.ExecuteQuery(query, parameters);

                var dict = new Dictionary<string, int>();
                foreach (DataRow row in result.Rows)
                    dict[$"{row["y"]}-{row["m"]}"] = Convert.ToInt32(row["cnt"]);

                var values = new List<int>();
                int running = 0;
                for (int i = 5; i >= 0; i--)
                {
                    var d = DateTime.Now.AddMonths(-i);
                    string key = $"{d.Year}-{d.Month}";
                    running += dict.ContainsKey(key) ? dict[key] : 0;
                    values.Add(running);
                }

                return new JavaScriptSerializer().Serialize(values);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // ── Monthly event counts ─────────────────────────────────────────
        public static string GetMonthlyEventCounts()
        {
            try
            {
                parameters.Clear();
                var query = @"
                    SELECT MONTH(created_date) AS m, YEAR(created_date) AS y, COUNT(*) AS cnt
                    FROM event_submission
                    WHERE created_date >= DATE_SUB(DATE_FORMAT(CURDATE(), '%Y-%m-01'), INTERVAL 5 MONTH)
                    GROUP BY YEAR(created_date), MONTH(created_date)
                    ORDER BY y, m";

                DataTable result = SqlRequest.ExecuteQuery(query, parameters);

                var dict = new Dictionary<string, int>();
                foreach (DataRow row in result.Rows)
                    dict[$"{row["y"]}-{row["m"]}"] = Convert.ToInt32(row["cnt"]);

                var values = new List<int>();
                for (int i = 5; i >= 0; i--)
                {
                    var d = DateTime.Now.AddMonths(-i);
                    string key = $"{d.Year}-{d.Month}";
                    values.Add(dict.ContainsKey(key) ? dict[key] : 0);
                }

                return new JavaScriptSerializer().Serialize(values);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // ── All events (for table) ────────────────────────────────────────
        public static DataTable GetAllEvents()
        {
            try
            {
                parameters.Clear();
                var query = @"
                    SELECT title, event_date,
                           CASE WHEN CAST(event_date AS DATE) >= CAST(CURDATE() AS DATE)
                                THEN 'Upcoming' ELSE 'Completed' END AS status
                    FROM event_submission
                    ORDER BY event_date DESC";

                return SqlRequest.ExecuteQuery(query, parameters);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
