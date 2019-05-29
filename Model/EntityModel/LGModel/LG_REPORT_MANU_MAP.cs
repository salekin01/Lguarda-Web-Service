using Model.EDMX;
using Model.EntityModel.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model.EntityModel.LGModel
{
    [DataContract]
    public class LG_REPORT_MANU_MAP
    {
        [DataMember]
        public string FUNCTION_ID { get; set; }

        [DataMember]
        public string FUNCTION_NM { get; set; }

        public static IEnumerable<LG_REPORT_MANU_MAP> GetReportFunctionIds(string pAPPLICATION_ID)
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            List<LG_REPORT_MANU_MAP> LIST_LG_FNR_FUNCTION_MAP = new List<LG_REPORT_MANU_MAP>();

            try
            {
                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                LIST_LG_FNR_FUNCTION_MAP = (from f in Obj_DBModelEntities.LG_FNR_FUNCTION
                                            where f.APPLICATION_ID == pAPPLICATION_ID &&
                                                  f.ITEM_TYPE == "R" &&
                                                  f.AUTH_STATUS_ID == "A" &&
                                                  f.LAST_ACTION != "DEL"
                                            select new LG_REPORT_MANU_MAP
                                            {
                                                FUNCTION_ID = f.FUNCTION_ID,
                                                FUNCTION_NM = f.FUNCTION_NM,
                                            }).ToList();
                return LIST_LG_FNR_FUNCTION_MAP;
            }
            catch (DbEntityValidationException dbEx)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner = "";
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                        inner = validationError.PropertyName + "::" + validationError.ErrorMessage + ";;" + inner;
                    }
                }
                LG_ERROR_LOG_MAP.Add_Error_Log(null, dbEx.Source, "ERR_APP_TYPE", "GetReportFunctionIds",
                                               "0000000000", dbEx.Message, inner, dbEx.StackTrace, null, dateTime);
                return LIST_LG_FNR_FUNCTION_MAP;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);
                LG_ERROR_LOG_MAP.Add_Error_Log(null, ex.Source, "ERR_APP_TYPE", "GetReportFunctionIds",
                                               "0000000000", ex.Message, inner4, ex.StackTrace, null, dateTime);
                return LIST_LG_FNR_FUNCTION_MAP;
            }
        }
    }
}
