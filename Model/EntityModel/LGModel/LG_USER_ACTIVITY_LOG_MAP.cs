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
    public class LG_USER_ACTIVITY_LOG_MAP
    {
        #region Property

        public int SL_ID { get; set; }
        [DataMember]
        public string USER_ID { get; set; }
        [DataMember]
        public string ACCOUNT_NO { get; set; }
        [DataMember]
        public string BRANCH_ID { get; set; }
        [DataMember]
        public string IP_ADDRESS { get; set; }
        [DataMember]
        public string ACTION { get; set; }
        [DataMember]
        public string PARAMETERS { get; set; }
        [DataMember]
        public string CURRENT_PAGE { get; set; }

        public string ERROR { get; set; }

        public DateTime? DATE_TIME { get; set; }

        public string APPLICATION_ID { get; set; }

        #endregion

        #region Event

        #region Insert to Log

        public static string pUSER_ID = "";

        public static string InsertToLog(LG_USER_ACTIVITY_LOG_MAP pLG_USER_ACTIVITY_LOG_MAP)
        {

            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            string result = string.Empty;

            try
            {

                //int id = Convert.ToInt32(Obj_DBModelEntities.LG_USER_ACTIVITY_LOG
                //                        .Max(x => (int?)x.SL_ID) ?? 0) + 1;

                int id = (Obj_DBModelEntities.LG_USER_ACTIVITY_LOG
                           .Select(i => i.SL_ID).Cast<int?>().Max() ?? 0) + 1;

                LG_USER_ACTIVITY_LOG OBJ_LG_USER_ACTIVITY_LOG = new LG_USER_ACTIVITY_LOG();

                OBJ_LG_USER_ACTIVITY_LOG.SL_ID = id;
                pUSER_ID = OBJ_LG_USER_ACTIVITY_LOG.USER_ID = pLG_USER_ACTIVITY_LOG_MAP.USER_ID;
                OBJ_LG_USER_ACTIVITY_LOG.ACCOUNT_NO = pLG_USER_ACTIVITY_LOG_MAP.ACCOUNT_NO;
                OBJ_LG_USER_ACTIVITY_LOG.BRANCH_ID = pLG_USER_ACTIVITY_LOG_MAP.BRANCH_ID;
                OBJ_LG_USER_ACTIVITY_LOG.IP_ADDRESS = pLG_USER_ACTIVITY_LOG_MAP.IP_ADDRESS;
                OBJ_LG_USER_ACTIVITY_LOG.ACTION = pLG_USER_ACTIVITY_LOG_MAP.ACTION;
                OBJ_LG_USER_ACTIVITY_LOG.PARAMETERS = (pLG_USER_ACTIVITY_LOG_MAP.PARAMETERS != null && pLG_USER_ACTIVITY_LOG_MAP.PARAMETERS.Length > 1099) ? pLG_USER_ACTIVITY_LOG_MAP.PARAMETERS.Substring(0, 1099) : pLG_USER_ACTIVITY_LOG_MAP.PARAMETERS;
                OBJ_LG_USER_ACTIVITY_LOG.CURRENT_PAGE = pLG_USER_ACTIVITY_LOG_MAP.CURRENT_PAGE;
                OBJ_LG_USER_ACTIVITY_LOG.DATE_TIME = System.DateTime.Now;

                Obj_DBModelEntities.LG_USER_ACTIVITY_LOG.Add(OBJ_LG_USER_ACTIVITY_LOG);
                Obj_DBModelEntities.SaveChanges();

             

                result = "True";

                return result;
            }

            catch (DbEntityValidationException dbEx)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner="";                

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                        inner = validationError.PropertyName + "::" + validationError.ErrorMessage + ";;" + inner;
                        result = "Can't store activity to history log(Db).";
                    }
                }

                LG_ERROR_LOG_MAP.Add_Error_Log(null, dbEx.Source, "ERR_APP_TYPE", "InsertToLog",
                                              "0000000000", dbEx.Message, inner, dbEx.StackTrace, pUSER_ID, dateTime);

                return result;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);
                while (ex != null);

                LG_ERROR_LOG_MAP.Add_Error_Log(null, ex.Source, "ERR_APP_TYPE", "InsertToLog",
                                              "0000000000", ex.Message, inner4, ex.StackTrace, pUSER_ID, dateTime);

                result = "Can't store activity to history log";
                return result;
            }

        }

        public static string Insert_To_session_tracker(string puser_id, string pApplication_id)
        {

            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            string result = string.Empty;

            try
            {

                LG_SYS_SESSION_TRACKER OBJ_LG_SYS_SESSION_TRACKER = new LG_SYS_SESSION_TRACKER();

                var date = (Obj_DBModelEntities.LG_SYS_SESSION_TRACKER
                                   .Where(st => st.USER_ID == puser_id &&
                                                st.ACTIVE_FLAG_FOR_MULTI_LOGIN == 1 && st.APPLICATION_ID == pApplication_id)
                                   .Select(t => t.LAST_ACCESS_TIME).Max());

                //LG_SYS_SESSION_TRACKER OBJ_LG_SYS_SESSION_TRACKER_01 = (from t in Obj_DBModelEntities.LG_SYS_SESSION_TRACKER

                //                              where t.USER_ID == puser_id && t.ACTIVE_FLAG_MULTI_LOGIN == 1 && t.LAST_ACCESS_TIME == date && t.APPLICATION_ID == pApplication_id

                //                              select new LG_SYS_SESSION_TRACKER()
                //                              {
                //                                  USER_ID = t.USER_ID,
                //                                  APPLICATION_ID = t.APPLICATION_ID,

                //                                  ACTIVE_FLAG_MULTI_LOGIN = t.ACTIVE_FLAG_MULTI_LOGIN,

                //                                  SESSION_ID = t.SESSION_ID,
                //                                  START_TIME = t.START_TIME,
                //                                  LAST_ACCESS_TIME = t.LAST_ACCESS_TIME,
                //                                  IP_ADDRESS = t.IP_ADDRESS,


                //                              }).SingleOrDefault();

                OBJ_LG_SYS_SESSION_TRACKER = Obj_DBModelEntities.LG_SYS_SESSION_TRACKER
                                                          .Where(t => t.USER_ID == puser_id && t.ACTIVE_FLAG_FOR_MULTI_LOGIN == 1 && t.LAST_ACCESS_TIME == date && t.APPLICATION_ID == pApplication_id).SingleOrDefault();

                OBJ_LG_SYS_SESSION_TRACKER.LAST_ACCESS_TIME = System.DateTime.Now;
                Obj_DBModelEntities.SaveChanges();
                result = "True";

                return result;
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
                        result = "Can't store activity to history log(Db).";
                    }
                }

                LG_ERROR_LOG_MAP.Add_Error_Log(null, dbEx.Source, "ERR_APP_TYPE", "InsertToLog",
       "0000000000", dbEx.Message, inner, dbEx.StackTrace, pUSER_ID, dateTime);

                return result;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);
            
                LG_ERROR_LOG_MAP.Add_Error_Log(null, ex.Source, "ERR_APP_TYPE", "InsertToLog",
                       "0000000000", ex.Message, inner4, ex.StackTrace, pUSER_ID, dateTime);

                result = "Can't store activity to history log";
                return result;
            }

        }

        #endregion

        #region Show Log

        public static IEnumerable<LG_USER_ACTIVITY_LOG_MAP> GetUserActivityLog(string pUSER_ID, string pSTART_DATE, string pEND_DATE)
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            List<LG_USER_ACTIVITY_LOG_MAP> LIST_LG_USER_ACTIVITY_LOG_MAP = null;
            LG_USER_ACTIVITY_LOG_MAP OBJ_LG_USER_ACTIVITY_LOG_MAP = new LG_USER_ACTIVITY_LOG_MAP();

            try
            {
                Obj_DBModelEntities.Configuration.LazyLoadingEnabled = false;
                Obj_DBModelEntities.Configuration.ProxyCreationEnabled = false;

                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                pSTART_DATE = pSTART_DATE.Replace("-","/").Replace(";",":");
                pEND_DATE = pEND_DATE.Replace("-","/").Replace(";",":");

                if (pUSER_ID != null && pSTART_DATE != null && pEND_DATE != null)
                {
                    DateTime START_DATE = Convert.ToDateTime(pSTART_DATE);
                    DateTime END_DATE = Convert.ToDateTime(pEND_DATE);

                    LIST_LG_USER_ACTIVITY_LOG_MAP = (from a in Obj_DBModelEntities.LG_USER_ACTIVITY_LOG
                                                     where a.USER_ID == pUSER_ID && (a.DATE_TIME >= START_DATE && a.DATE_TIME <= END_DATE)
                                                     orderby a.DATE_TIME descending
                                                     select new LG_USER_ACTIVITY_LOG_MAP
                                                {
                                                    SL_ID = a.SL_ID,
                                                    USER_ID = a.USER_ID,
                                                    ACCOUNT_NO = a.ACCOUNT_NO,
                                                    BRANCH_ID = a.BRANCH_ID,
                                                    DATE_TIME = a.DATE_TIME,
                                                    ACTION = a.ACTION,
                                                    IP_ADDRESS = a.IP_ADDRESS,
                                                    PARAMETERS = a.PARAMETERS,
                                                    CURRENT_PAGE = a.CURRENT_PAGE,

                                                }).ToList();
                    return LIST_LG_USER_ACTIVITY_LOG_MAP;
                }
                else
                {
                    OBJ_LG_USER_ACTIVITY_LOG_MAP.ERROR = "All Search parameters are not set properly.";
                    LIST_LG_USER_ACTIVITY_LOG_MAP.Add(OBJ_LG_USER_ACTIVITY_LOG_MAP);
                    return LIST_LG_USER_ACTIVITY_LOG_MAP;
                }

            }

            catch (DbEntityValidationException dbEx)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner="";

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                        inner = validationError.PropertyName + "::" + validationError.ErrorMessage + ";;" + inner;
                        OBJ_LG_USER_ACTIVITY_LOG_MAP.ERROR = validationError.ErrorMessage;
                        LIST_LG_USER_ACTIVITY_LOG_MAP.Add(OBJ_LG_USER_ACTIVITY_LOG_MAP);
                    }
                }

                LG_ERROR_LOG_MAP.Add_Error_Log(null, dbEx.Source, "ERR_APP_TYPE", "GetUserActivityLog",
       "0000000000", dbEx.Message, inner, dbEx.StackTrace, "LogRequestFor:" + pUSER_ID, dateTime);

                return LIST_LG_USER_ACTIVITY_LOG_MAP;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                LG_ERROR_LOG_MAP.Add_Error_Log(null, ex.Source, "ERR_APP_TYPE", "GetUserActivityLog",
                       "0000000000", ex.Message, inner4, ex.StackTrace, "LogRequestFor:" + "LogRequestId:" + pUSER_ID, dateTime);
                
                OBJ_LG_USER_ACTIVITY_LOG_MAP.ERROR = ex.Message;
                LIST_LG_USER_ACTIVITY_LOG_MAP.Add(OBJ_LG_USER_ACTIVITY_LOG_MAP);
                return LIST_LG_USER_ACTIVITY_LOG_MAP;
            }
        }


        #endregion

        #endregion

    }
}
