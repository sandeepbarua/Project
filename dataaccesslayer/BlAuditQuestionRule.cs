using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiddleLayer;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.WebControls;


namespace DataAccessLayer
{
    public class BlAuditQuestionRule
    {
        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["DBCon"].ConnectionString;
        }

        public List<MlAuditQuestionRule> getAuditQuestionRule(MlAuditQuestionRule Adq)
        {
            SqlConnection SqlConn = new SqlConnection();
            SqlConn.ConnectionString = GetConnectionString();
            List<MlAuditQuestionRule> lstMlAuditQuestionRule = new List<MlAuditQuestionRule>();
            string SqlQuery = null;
            if (Adq.AuditRuleClassificationID != 0)
            {
                SqlQuery = @"SELECT   * from AuditRuleClassification WHERE AuditRuleClassification = " + Adq.AuditRuleClassificationID + "";

            }
            


            using (SqlCommand SqlCom = new SqlCommand(SqlQuery, SqlConn))
            {
                try
                {
                    SqlConn.Open();
                    SqlDataReader reader = SqlCom.ExecuteReader();
                    while (reader.Read())
                    {
                        MlAuditQuestionRule setData = new MlAuditQuestionRule();

                        setData.AuditRuleClassificationID = Convert.ToInt32(reader["AuditRuleClassification"]);
                        setData.AuditQuestionId = Convert.ToInt32(reader["AuditQuestionId"]);
                        setData.DynamicControlId = Convert.ToInt32(reader["DynamicControlId"]);
                        setData.RuleExpression = Convert.ToString(reader["RuleExpression"]);
                        setData.RuleType = Convert.ToString(reader["RuleType"]);
                        
                        lstMlAuditQuestionRule.Add(setData);

                    }

                    SqlConn.Close();
                    return lstMlAuditQuestionRule;

                }
                catch (Exception e)
                {
                    return lstMlAuditQuestionRule;

                }

            }
        }

        public void updateAuditRuleClassificationnd(int AuditRuleClassification,string RuleExpression, string RuleType, string DynamicControlLabel)
        {
            try
            {
                BLConsumeApi objApi = new BLConsumeApi();

                objApi.Sp_UpdateAuditRuleClassification(RuleExpression, RuleType, DynamicControlLabel, AuditRuleClassification);
            }
            catch(Exception ex)
            {
                ex.ToString();
            }
        }

    }
}
