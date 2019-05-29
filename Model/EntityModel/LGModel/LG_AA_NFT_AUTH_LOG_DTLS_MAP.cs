using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using Model.EDMX;
using Model.EntityModel.Common;
using System.Configuration;
using System.Transactions;

namespace Model.EntityModel.LGModel
{
    [DataContract]
    public class LG_AA_NFT_AUTH_LOG_DTLS_MAP
    {
        #region Properties

        [DataMember]
        [Required(ErrorMessage = "Log Details ID is required")]
        public Int64 LOG_DETAILS_ID { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Log ID is required")]
        public Int64 LOG_ID { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Auth By is required")]
        public string AUTH_OR_DEC_BY { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Auth Date is required")]
        public DateTime AUTH_OR_DEC_DT { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Auth Level is required")]
        public int AUTH_LEVEL { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Auth Status ID is required")]
        public string AUTH_STATUS_ID { get; set; }

        [DataMember]
        public string ERROR { get; set; }
        
        #endregion


        #region Methods
        
        public static string AddNftAuthLogDtls(Int64 LOG_ID, string AUTH_BY, DateTime AUTH_DT, short AUTH_LEVEL, string AUTH_STATUS_ID)
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            string result = string.Empty;

            try
            {
                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                Int64 id = Convert.ToInt64(Obj_DBModelEntities.LG_AA_NFT_AUTH_LOG_DTLS.Max(x => x.LOG_DETAILS_ID)) + 1;

                LG_AA_NFT_AUTH_LOG_DTLS OBJ_LG_AA_NFT_AUTH_LOG_DTLS = new LG_AA_NFT_AUTH_LOG_DTLS();
                OBJ_LG_AA_NFT_AUTH_LOG_DTLS.LOG_DETAILS_ID = id;
                OBJ_LG_AA_NFT_AUTH_LOG_DTLS.LOG_ID = LOG_ID;
                OBJ_LG_AA_NFT_AUTH_LOG_DTLS.AUTH_OR_DEC_BY = AUTH_BY;
                OBJ_LG_AA_NFT_AUTH_LOG_DTLS.AUTH_OR_DEC_DT = AUTH_DT;
                OBJ_LG_AA_NFT_AUTH_LOG_DTLS.AUTH_LEVEL = AUTH_LEVEL;
                OBJ_LG_AA_NFT_AUTH_LOG_DTLS.AUTH_STATUS_ID = AUTH_STATUS_ID;

                Obj_DBModelEntities.LG_AA_NFT_AUTH_LOG_DTLS.Add(OBJ_LG_AA_NFT_AUTH_LOG_DTLS);
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
                        result = "Can't Add Application(Db).";
                    }
                }

                LG_ERROR_LOG_MAP.Add_Error_Log("010106001", dbEx.Source, "ERR_APP_TYPE", "AddNftAuthLogDtls",
                                             "0000000000", dbEx.Message, inner, dbEx.StackTrace, AUTH_BY, dateTime);

                return result;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                LG_ERROR_LOG_MAP.Add_Error_Log("010106001", ex.Source, "ERR_APP_TYPE", "AddNftAuthLogDtls",
                                                 "0000000000", ex.Message, inner4, ex.StackTrace, AUTH_BY, dateTime);

                result = "Can't Add Application.";
                return result;
            }
        }

        public static string AddNftAuthLogDtls(string LOG_ID, string AUTH_BY, string AUTH_DT, string AUTH_STATUS_ID, string reasonDecline)
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            string result = string.Empty;
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {
                    Int64 LOG_ID_temp = Convert.ToInt64(LOG_ID);
                    string format = OutgoingResponseFormat.GetFormat();
                    OutgoingResponseFormat.SetResponseFormat(format);

                    var LG_AA_NFT_AUTH_LOG_DTLS_MAP_LIST = (from a in Obj_DBModelEntities.LG_AA_NFT_AUTH_LOG_DTLS
                                                            where a.LOG_ID == LOG_ID_temp
                                                            select new LG_AA_NFT_AUTH_LOG_DTLS_MAP
                                                            {
                                                                LOG_ID = a.LOG_ID,
                                                                AUTH_LEVEL = a.AUTH_LEVEL,
                                                                AUTH_OR_DEC_DT = a.AUTH_OR_DEC_DT,
                                                                LOG_DETAILS_ID = a.LOG_DETAILS_ID,
                                                                AUTH_OR_DEC_BY = a.AUTH_OR_DEC_BY
                                                            }).ToList();

                    List<LG_AA_NFT_AUTH_LOG_DTLS_MAP> dtls = (List<LG_AA_NFT_AUTH_LOG_DTLS_MAP>)LG_AA_NFT_AUTH_LOG_DTLS_MAP_LIST.Where(x => x.AUTH_OR_DEC_BY == AUTH_BY).ToList();
                    int count = dtls.Count();

                    if (LG_AA_NFT_AUTH_LOG_DTLS_MAP_LIST == null || dtls == null || count == 0)
                    {
                        Int64 id = Convert.ToInt64(Obj_DBModelEntities.LG_AA_NFT_AUTH_LOG_DTLS.DefaultIfEmpty()
                                                 .Max(p => p == null ? 0 : p.LOG_DETAILS_ID)) + 1;

                        LG_AA_NFT_AUTH_LOG_DTLS OBJ_LG_AA_NFT_AUTH_LOG_DTLS = new LG_AA_NFT_AUTH_LOG_DTLS();
                        OBJ_LG_AA_NFT_AUTH_LOG_DTLS.LOG_DETAILS_ID = id;
                        OBJ_LG_AA_NFT_AUTH_LOG_DTLS.LOG_ID = Convert.ToInt64(LOG_ID);
                        OBJ_LG_AA_NFT_AUTH_LOG_DTLS.AUTH_STATUS_ID = AUTH_STATUS_ID;
                        OBJ_LG_AA_NFT_AUTH_LOG_DTLS.AUTH_OR_DEC_BY = AUTH_BY;
                        OBJ_LG_AA_NFT_AUTH_LOG_DTLS.AUTH_OR_DEC_DT = DateTime.Now;

                        Int16 AUTH_LEVEL = GetAuthLevelFromLogDetails(Convert.ToInt64(LOG_ID));
                        Int16 AUTH_LEVEL_MAX = LG_AA_NFT_AUTH_LOG_MAP.GetNftAuthLevelMaxFromAuthLog(LOG_ID);
                        if (AUTH_LEVEL < AUTH_LEVEL_MAX)
                        {
                            AUTH_LEVEL = Convert.ToInt16(AUTH_LEVEL + 1);
                            OBJ_LG_AA_NFT_AUTH_LOG_DTLS.AUTH_LEVEL = AUTH_LEVEL;

                            Obj_DBModelEntities.LG_AA_NFT_AUTH_LOG_DTLS.Add(OBJ_LG_AA_NFT_AUTH_LOG_DTLS);
                            Obj_DBModelEntities.SaveChanges();

                            if (AUTH_STATUS_ID == "A")
                            {
                                if ((AUTH_LEVEL) == AUTH_LEVEL_MAX)
                                {
                                    string updateResult1 = LG_AA_NFT_AUTH_LOG_MAP.UpdateNftAuthLog(LOG_ID, AUTH_STATUS_ID, reasonDecline);
                                    string updateResultForObjectTable = LG_AA_NFT_AUTH_LOG_MAP.UpdateNftAuthLogObjectTable(LOG_ID, AUTH_STATUS_ID, AUTH_BY, Obj_DBModelEntities);
                                }
                                else
                                {
                                    string updateResult2 = LG_AA_NFT_AUTH_LOG_MAP.UpdateNftAuthLogPending(LOG_ID);
                                }
                            }
                            else if (AUTH_STATUS_ID == "D")
                            {
                                string updateResult1 = LG_AA_NFT_AUTH_LOG_MAP.UpdateNftAuthLog(LOG_ID, AUTH_STATUS_ID, reasonDecline);
                                string updateResultForObjectTable = LG_AA_NFT_AUTH_LOG_MAP.UpdateNftAuthLogObjectTable(LOG_ID, AUTH_STATUS_ID, AUTH_BY, Obj_DBModelEntities);
                            }
                            else
                            {
                                string updateResult1 = LG_AA_NFT_AUTH_LOG_MAP.UpdateNftAuthLog(LOG_ID, AUTH_STATUS_ID, reasonDecline);
                                string updateResultForObjectTable = LG_AA_NFT_AUTH_LOG_MAP.UpdateNftAuthLogObjectTable(LOG_ID, AUTH_STATUS_ID, AUTH_BY, Obj_DBModelEntities);
                            }
                            result = "True";
                        }
                        Obj_DBModelEntities.SaveChanges();
                        transactionScope.Complete();
                    }
                    else
                    {
                        transactionScope.Dispose();
                        return "Do not have permission to authorize.You have already authorized once.";
                    }
                    return result;
                }
                catch (DbEntityValidationException dbEx)
                {
                    transactionScope.Dispose();
                    string dateTime = Convert.ToString(System.DateTime.Now);
                    string inner = "";

                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                            inner = validationError.PropertyName + "::" + validationError.ErrorMessage + ";;" + inner;
                            result = "Can't Add Authorization Log Details(Db).";
                        }
                    }

                    LG_ERROR_LOG_MAP.Add_Error_Log("010106001", dbEx.Source, "ERR_APP_TYPE", "AddNftAuthLogDtls(string,string,string,string,string)",
                                                  "0000000000", dbEx.Message, inner, dbEx.StackTrace, AUTH_BY, dateTime);

                    return result;
                }
                catch (Exception ex)
                {
                    transactionScope.Dispose();
                    string dateTime = Convert.ToString(System.DateTime.Now);
                    string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                    LG_ERROR_LOG_MAP.Add_Error_Log("010106001", ex.Source, "ERR_APP_TYPE", "AddNftAuthLogDtls(string,string,string,string,string)",
                                                  "0000000000", ex.Message, inner4, ex.StackTrace, null, dateTime);

                    result = "Can't Add Authorization Log Details.";
                    return result;
                }
            }
        }
        /* Old AddNftAuthLogDtls
        public static string AddNftAuthLogDtls(string LOG_ID, string AUTH_BY, string AUTH_DT, string AUTH_STATUS_ID, string reasonDecline)
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            string result = string.Empty;
            try
            {
                Int64 LOG_ID_temp = Convert.ToInt64(LOG_ID);
                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                LG_AA_NFT_AUTH_LOG obj_LG_AA_NFT_AUTH_LOG = Obj_DBModelEntities.LG_AA_NFT_AUTH_LOG.Where(m => m.LOG_ID == LOG_ID_temp).SingleOrDefault();

                if (obj_LG_AA_NFT_AUTH_LOG.MAKE_BY == AUTH_BY)
                {
                    result = "Maker Checker can't be same";
                    return result;
                }
                Int64 id =
                    Convert.ToInt64(
                        Obj_DBModelEntities.LG_AA_NFT_AUTH_LOG_DTLS.DefaultIfEmpty().Max(p => p == null ? 0 : p.LOG_DETAILS_ID)) +
                    1;

                LG_AA_NFT_AUTH_LOG_DTLS OBJ_LG_AA_NFT_AUTH_LOG_DTLS = new LG_AA_NFT_AUTH_LOG_DTLS();
                OBJ_LG_AA_NFT_AUTH_LOG_DTLS.LOG_DETAILS_ID = id;
                OBJ_LG_AA_NFT_AUTH_LOG_DTLS.LOG_ID = Convert.ToInt64(LOG_ID);
                OBJ_LG_AA_NFT_AUTH_LOG_DTLS.AUTH_STATUS_ID = AUTH_STATUS_ID;
                OBJ_LG_AA_NFT_AUTH_LOG_DTLS.AUTH_OR_DEC_BY = AUTH_BY;
                //OBJ_LG_AA_NFT_AUTH_LOG_DTLS.AUTH_OR_DEC_DT = Convert.ToDateTime(AUTH_DT);
                OBJ_LG_AA_NFT_AUTH_LOG_DTLS.AUTH_OR_DEC_DT = DateTime.Now;

                Int16 AUTH_LEVEL = GetAuthLevelFromLogDetails(Convert.ToInt64(LOG_ID));
                Int16 AUTH_LEVEL_MAX = LG_AA_NFT_AUTH_LOG_MAP.GetNftAuthLevelMaxFromAuthLog(LOG_ID);
                if (AUTH_LEVEL < AUTH_LEVEL_MAX)
                {
                    AUTH_LEVEL = Convert.ToInt16(AUTH_LEVEL + 1);
                    OBJ_LG_AA_NFT_AUTH_LOG_DTLS.AUTH_LEVEL = AUTH_LEVEL;

                    Obj_DBModelEntities.LG_AA_NFT_AUTH_LOG_DTLS.Add(OBJ_LG_AA_NFT_AUTH_LOG_DTLS);
                    Obj_DBModelEntities.SaveChanges();

                    if (AUTH_STATUS_ID == "A")
                    {
                        if ((AUTH_LEVEL) == AUTH_LEVEL_MAX)
                        {
                            string updateResult1 = LG_AA_NFT_AUTH_LOG_MAP.UpdateNftAuthLog(LOG_ID, AUTH_STATUS_ID,
                                reasonDecline);

                            string updateResultForObjectTable = LG_AA_NFT_AUTH_LOG_MAP.UpdateNftAuthLogObjectTable(LOG_ID, AUTH_STATUS_ID);
                        }
                        else
                        {
                            string updateResult2 = LG_AA_NFT_AUTH_LOG_MAP.UpdateNftAuthLogPending(LOG_ID);
                        }
                    }
                    else
                    {
                        string updateResult1 = LG_AA_NFT_AUTH_LOG_MAP.UpdateNftAuthLog(LOG_ID, AUTH_STATUS_ID,
                                reasonDecline);

                        string updateResultForObjectTable = LG_AA_NFT_AUTH_LOG_MAP.UpdateNftAuthLogObjectTable(LOG_ID, AUTH_STATUS_ID);
                    }
                }

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
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName,
                            validationError.ErrorMessage);
                        inner = validationError.PropertyName + "::" + validationError.ErrorMessage + ";;" + inner;
                        result = "Can't Add Authorization Log Details(Db).";
                    }
                }

                LG_ERROR_LOG_MAP.Add_Error_Log(null, dbEx.Source, "ERR_APP_TYPE", "AddNftAuthLogDtls(string,string,string,string,string)",
                                                 "0000000000", dbEx.Message, inner, dbEx.StackTrace, null, dateTime);

                return result;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner1;
                string inner2;
                string inner3;
                string inner4;
                if (ex.InnerException != null)
                {
                    inner1 = ex.InnerException.ToString() + ";;";
                }
                else
                {
                    inner1 = ";;";
                }
                if (ex.InnerException != null)
                {
                    inner2 = inner1 + ex.InnerException.InnerException.ToString() + ";;";
                }
                else
                {
                    inner2 = inner1 + ";;";
                }
                if (ex.InnerException != null)
                {
                    inner3 = inner2 + ex.InnerException.InnerException.InnerException.ToString() + ";;";
                }
                else
                {
                    inner3 = inner2 + ";;";
                }
                if (ex.InnerException != null)
                {
                    inner4 = inner3 + ex.InnerException.InnerException.InnerException.InnerException.ToString() + ";;";
                }
                else
                {
                    inner4 = inner3 + ";;";
                }

                LG_ERROR_LOG_MAP.Add_Error_Log(null, ex.Source, "ERR_APP_TYPE", "AddNftAuthLogDtls(string,string,string,string,string)",
                                                 "0000000000", ex.Message, inner4, ex.StackTrace, null, dateTime);

                result = "Can't Add Authorization Log Details.";
                return result;
            }
        }*/

        public static short GetAuthLevelFromLogDetails(Int64 LOG_ID)
        {
            try
            {
                DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
                Int16 AUTH_LEVEL = Convert.ToInt16(Obj_DBModelEntities.LG_AA_NFT_AUTH_LOG_DTLS
                                                 .Where(p => p.LOG_ID.Equals(LOG_ID)).DefaultIfEmpty()
                                                 .Max(p => p == null ? 0 : p.AUTH_LEVEL));

                return AUTH_LEVEL;
            }
            catch(DbEntityValidationException dbEx)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner = "";

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                        inner = validationError.PropertyName + "::" + validationError.ErrorMessage + ";;" + inner;
                        string result = "Can't get auth level(Db).";
                    }
                }

                LG_ERROR_LOG_MAP.Add_Error_Log("010106001", dbEx.Source, "ERR_APP_TYPE", "GetAuthLevelFromLogDetails",
                                               "0000000000", dbEx.Message, inner, dbEx.StackTrace, null, dateTime);
                return -2;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                LG_ERROR_LOG_MAP.Add_Error_Log("010106001", ex.Source, "ERR_APP_TYPE", "GetAuthLevelFromLogDetails",
                                               "0000000000", ex.Message, inner4, ex.StackTrace, null, dateTime);

                string result = "Can't get auth level.";
                return -1;
            }         
        }

        public static List<LG_AA_NFT_AUTH_LOG_DTLS_MAP> GetAuthHistory(string pLOG_ID)
        {
            Int64 LOG_ID = Convert.ToInt64(pLOG_ID);

            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            List<LG_AA_NFT_AUTH_LOG_DTLS_MAP> LIST_LG_AA_NFT_AUTH_LOG_DTLS_MAP = null;
            LG_AA_NFT_AUTH_LOG_DTLS_MAP OBJ_LG_AA_NFT_AUTH_LOG_DTLS_MAP = new LG_AA_NFT_AUTH_LOG_DTLS_MAP();

            try
            {
                Obj_DBModelEntities.Configuration.LazyLoadingEnabled = false;
                Obj_DBModelEntities.Configuration.ProxyCreationEnabled = false;

                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);


                LIST_LG_AA_NFT_AUTH_LOG_DTLS_MAP = (from ald in Obj_DBModelEntities.LG_AA_NFT_AUTH_LOG_DTLS
                                                    join al in Obj_DBModelEntities.LG_AA_NFT_AUTH_LOG
                                                    on ald.LOG_ID equals al.LOG_ID
                                                    where ald.LOG_ID == LOG_ID
                                                    orderby ald.LOG_DETAILS_ID ascending
                                                    select new LG_AA_NFT_AUTH_LOG_DTLS_MAP
                                                    {
                                                        LOG_DETAILS_ID = ald.LOG_DETAILS_ID,
                                                        LOG_ID = ald.LOG_ID,
                                                        AUTH_OR_DEC_BY = ald.AUTH_OR_DEC_BY,
                                                        AUTH_OR_DEC_DT = ald.AUTH_OR_DEC_DT,
                                                        AUTH_LEVEL = ald.AUTH_LEVEL,
                                                        AUTH_STATUS_ID = ald.AUTH_STATUS_ID
                                                    }).ToList();

                return LIST_LG_AA_NFT_AUTH_LOG_DTLS_MAP;
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
                        OBJ_LG_AA_NFT_AUTH_LOG_DTLS_MAP.ERROR = validationError.ErrorMessage;
                        LIST_LG_AA_NFT_AUTH_LOG_DTLS_MAP.Add(OBJ_LG_AA_NFT_AUTH_LOG_DTLS_MAP);
                    }
                }

                LG_ERROR_LOG_MAP.Add_Error_Log("010106001", dbEx.Source, "ERR_APP_TYPE", "GetAuthHistory",
                                               "0000000000", dbEx.Message, inner, dbEx.StackTrace, null, dateTime);

                return LIST_LG_AA_NFT_AUTH_LOG_DTLS_MAP;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                LG_ERROR_LOG_MAP.Add_Error_Log("010106001", ex.Source, "ERR_APP_TYPE", "GetAuthHistory",
                                               "0000000000", ex.Message, inner4, ex.StackTrace, null, dateTime);

                OBJ_LG_AA_NFT_AUTH_LOG_DTLS_MAP.ERROR = ex.Message;
                LIST_LG_AA_NFT_AUTH_LOG_DTLS_MAP.Add(OBJ_LG_AA_NFT_AUTH_LOG_DTLS_MAP);
                return LIST_LG_AA_NFT_AUTH_LOG_DTLS_MAP;
            }
        }

        #endregion
    }
}
