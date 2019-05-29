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
using System.Web.Mvc;

namespace Model.EntityModel.LGModel
{
    [DataContract]
    public class LG_SYS_MAIL_SERVER_CONFIG_MAP
    {
        #region Properties
        [DataMember]
        public string MAIL_ID { get; set; }
        [DataMember]
        public string MAIL_SENDER_IP { get; set; }
        [DataMember]
        public string MAIL_SENDER_ADDRESS { get; set; }
        [DataMember]
        public string MAIL_SENDER_PASSWORD { get; set; }
        [DataMember]
        public string MAIL_SENDER_NAME { get; set; }
        [DataMember]
        public string APPLICATION_ID { get; set; }
        [DataMember]
        public string MAKE_BY { get; set; }
        [DataMember]
        public DateTime MAKE_DT { get; set; }
        [DataMember]
        public string APPLICATION_NAME { get; set; }
        [DataMember]
        public string ERROR { get; set; }
        [DataMember]
        public string AUTH_STATUS_ID { get; set; }
        [DataMember]
        public string LAST_ACTION { get; set; }
        [DataMember]
        public DateTime? LAST_UPDATE_DT { get; set; }

        public List<SelectListItem> APPLICATION_LIST_FOR_DD { get; set; }
        #endregion


        #region Events

        #region Add New
        public static string AddMailServerConfig(string pAPPLICATION_ID, string pMAIL_SENDER_IP, string pMAIL_SENDER_ADDRESS, string pMAIL_SENDER_PASSWORD, string pMAIL_SENDER_NAME, string pSESSION_USER_ID)
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            LG_SYS_MAIL_SERVER_CONFIG OBJ_LG_SYS_MAIL_SERVER_CONFIG = new LG_SYS_MAIL_SERVER_CONFIG();
            string result = string.Empty;
            string FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "MailServer").Select(x => x.FUNCTION_ID).SingleOrDefault();
            try
            {
                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                OBJ_LG_SYS_MAIL_SERVER_CONFIG = Obj_DBModelEntities.LG_SYS_MAIL_SERVER_CONFIG
                                               .Where(m => m.APPLICATION_ID == pAPPLICATION_ID).SingleOrDefault();
                if (OBJ_LG_SYS_MAIL_SERVER_CONFIG != null)
                {
                    return "Mail server configuration for this application already exists";
                }

                int id = (Obj_DBModelEntities.LG_SYS_MAIL_SERVER_CONFIG
                          .Select(m => m.MAIL_ID).Cast<int?>().Max() ?? 0) + 1;

                OBJ_LG_SYS_MAIL_SERVER_CONFIG = new LG_SYS_MAIL_SERVER_CONFIG();
                OBJ_LG_SYS_MAIL_SERVER_CONFIG.MAIL_ID = id.ToString().PadLeft(2,'0');
                OBJ_LG_SYS_MAIL_SERVER_CONFIG.APPLICATION_ID = pAPPLICATION_ID;
                OBJ_LG_SYS_MAIL_SERVER_CONFIG.MAIL_SENDER_IP = pMAIL_SENDER_IP;
                OBJ_LG_SYS_MAIL_SERVER_CONFIG.MAIL_SENDER_ADDRESS = pMAIL_SENDER_ADDRESS;
                OBJ_LG_SYS_MAIL_SERVER_CONFIG.MAIL_SENDER_PASSWORD = pMAIL_SENDER_PASSWORD;
                OBJ_LG_SYS_MAIL_SERVER_CONFIG.MAIL_SENDER_NAME = pMAIL_SENDER_NAME;
                OBJ_LG_SYS_MAIL_SERVER_CONFIG.AUTH_STATUS_ID = "U";
                OBJ_LG_SYS_MAIL_SERVER_CONFIG.LAST_ACTION = "ADD";
                OBJ_LG_SYS_MAIL_SERVER_CONFIG.MAKE_DT = System.DateTime.Now;
                Obj_DBModelEntities.LG_SYS_MAIL_SERVER_CONFIG.Add(OBJ_LG_SYS_MAIL_SERVER_CONFIG);
                Obj_DBModelEntities.SaveChanges();

                #region Auth log
                LG_AA_NFT_AUTH_LOG_MAP OBJ_LG_AA_NFT_AUTH_LOG_MAP = new LG_AA_NFT_AUTH_LOG_MAP();
                OBJ_LG_AA_NFT_AUTH_LOG_MAP.FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "MailServer").Select(x => x.FUNCTION_ID).SingleOrDefault();
                OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_NAME = "LG_SYS_MAIL_SERVER_CONFIG";
                OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_NM = "MAIL_ID";
                OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_VAL = OBJ_LG_SYS_MAIL_SERVER_CONFIG.MAIL_ID;
                OBJ_LG_AA_NFT_AUTH_LOG_MAP.ACTION_STATUS = "ADD";
                OBJ_LG_AA_NFT_AUTH_LOG_MAP.REMARKS = "";
                OBJ_LG_AA_NFT_AUTH_LOG_MAP.PRIMARY_TABLE_FLAG = 0;
                OBJ_LG_AA_NFT_AUTH_LOG_MAP.PARENT_TABLE_PK_VAL = null;
                OBJ_LG_AA_NFT_AUTH_LOG_MAP.AUTH_STATUS_ID = "U";
                int? auth_level_max = LG_AA_NFT_AUTH_LOG_MAP.GetNftAuthLevelMaxFromFunction(OBJ_LG_AA_NFT_AUTH_LOG_MAP.FUNCTION_ID);
                if (auth_level_max.HasValue)
                {
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.AUTH_LEVEL_MAX = (short)auth_level_max;
                }
                else
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.AUTH_LEVEL_MAX = 0;
                OBJ_LG_AA_NFT_AUTH_LOG_MAP.AUTH_LEVEL_PENDING = OBJ_LG_AA_NFT_AUTH_LOG_MAP.AUTH_LEVEL_MAX;
                OBJ_LG_AA_NFT_AUTH_LOG_MAP.REASON_DECLINE = "";
                OBJ_LG_AA_NFT_AUTH_LOG_MAP.MAKE_BY = pSESSION_USER_ID;
                OBJ_LG_AA_NFT_AUTH_LOG_MAP.MAKE_DT = DateTime.Now;
                LG_AA_NFT_AUTH_LOG_MAP.AddNftAuthLog(OBJ_LG_SYS_MAIL_SERVER_CONFIG, OBJ_LG_AA_NFT_AUTH_LOG_MAP);
                #endregion

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
                        result = "Can't Add Mail Server(Db).";
                    }
                }

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "AddMailServerConfig",
                                              "0000000000", dbEx.Message, inner, dbEx.StackTrace, pSESSION_USER_ID, dateTime);

                return result;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "AddMailServerConfig",
                                              "0000000000", ex.Message, inner4, ex.StackTrace, pSESSION_USER_ID, dateTime);

                result = "Can't Add Mail Server.";
                return result;
            }
        }
        #endregion

        #region Decryption of pass
        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
        #endregion

        #region Update
        public static string UpdateMailServerConfig(string pMAIL_ID, string pAPPLICATION_ID, string pMAIL_SENDER_IP, string pMAIL_SENDER_ADDRESS, string pMAIL_SENDER_PASSWORD, string pMAIL_SENDER_NAME)
        {
            pMAIL_SENDER_PASSWORD = Base64Decode(pMAIL_SENDER_PASSWORD);

            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            string result = string.Empty;
            string FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "MailServer").Select(x => x.FUNCTION_ID).SingleOrDefault();
            try
            {
                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                LG_SYS_MAIL_SERVER_CONFIG OBJ_LG_SYS_MAIL_SERVER_CONFIG = Obj_DBModelEntities.LG_SYS_MAIL_SERVER_CONFIG
                                                                         .Where(m => m.MAIL_ID == pMAIL_ID).SingleOrDefault();

                LG_SYS_MAIL_SERVER_CONFIG OBJ_LG_SYS_MAIL_SERVER_CONFIG_OLD = new LG_SYS_MAIL_SERVER_CONFIG();
                OBJ_LG_SYS_MAIL_SERVER_CONFIG_OLD = OBJ_LG_SYS_MAIL_SERVER_CONFIG;

                OBJ_LG_SYS_MAIL_SERVER_CONFIG.APPLICATION_ID = pAPPLICATION_ID;
                OBJ_LG_SYS_MAIL_SERVER_CONFIG.MAIL_SENDER_IP = pMAIL_SENDER_IP;
                OBJ_LG_SYS_MAIL_SERVER_CONFIG.MAIL_SENDER_ADDRESS = pMAIL_SENDER_ADDRESS;
                OBJ_LG_SYS_MAIL_SERVER_CONFIG.MAIL_SENDER_PASSWORD = pMAIL_SENDER_PASSWORD;
                OBJ_LG_SYS_MAIL_SERVER_CONFIG.MAIL_SENDER_NAME = pMAIL_SENDER_NAME;
                OBJ_LG_SYS_MAIL_SERVER_CONFIG.AUTH_STATUS_ID = "A";
                OBJ_LG_SYS_MAIL_SERVER_CONFIG.LAST_ACTION = "EDT";
                OBJ_LG_SYS_MAIL_SERVER_CONFIG.LAST_UPDATE_DT = System.DateTime.Now;
                Obj_DBModelEntities.SaveChanges();

                #region Auth log
                LG_AA_NFT_AUTH_LOG_MAP OBJ_LG_AA_NFT_AUTH_LOG_MAP = new LG_AA_NFT_AUTH_LOG_MAP();
                OBJ_LG_AA_NFT_AUTH_LOG_MAP.FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "Mail Server").Select(x => x.FUNCTION_ID).SingleOrDefault();
                OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_NAME = "LG_SYS_MAIL_SERVER_CONFIG";
                OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_NM = "MAIL_ID";
                OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_VAL = OBJ_LG_SYS_MAIL_SERVER_CONFIG.MAIL_ID;
                OBJ_LG_AA_NFT_AUTH_LOG_MAP.ACTION_STATUS = "EDT";
                OBJ_LG_AA_NFT_AUTH_LOG_MAP.REMARKS = "";
                OBJ_LG_AA_NFT_AUTH_LOG_MAP.PRIMARY_TABLE_FLAG = 0;
                OBJ_LG_AA_NFT_AUTH_LOG_MAP.PARENT_TABLE_PK_VAL = null;
                OBJ_LG_AA_NFT_AUTH_LOG_MAP.AUTH_STATUS_ID = "U";
                int? auth_level_max = LG_AA_NFT_AUTH_LOG_MAP.GetNftAuthLevelMaxFromFunction(OBJ_LG_AA_NFT_AUTH_LOG_MAP.FUNCTION_ID);
                if (auth_level_max.HasValue)
                {
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.AUTH_LEVEL_MAX = (short)auth_level_max;
                }
                else
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.AUTH_LEVEL_MAX = 0;
                OBJ_LG_AA_NFT_AUTH_LOG_MAP.AUTH_LEVEL_PENDING = OBJ_LG_AA_NFT_AUTH_LOG_MAP.AUTH_LEVEL_MAX;
                OBJ_LG_AA_NFT_AUTH_LOG_MAP.REASON_DECLINE = "";
                OBJ_LG_AA_NFT_AUTH_LOG_MAP.MAKE_BY = "salekin";
                OBJ_LG_AA_NFT_AUTH_LOG_MAP.MAKE_DT = DateTime.Now;
                LG_AA_NFT_AUTH_LOG_MAP.AddNftAuthLog(OBJ_LG_SYS_MAIL_SERVER_CONFIG_OLD, OBJ_LG_SYS_MAIL_SERVER_CONFIG, OBJ_LG_AA_NFT_AUTH_LOG_MAP);
                #endregion

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
                        result = "Can't Update Mail Server(Db).";
                    }
                }

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "UpdateMailServerConfig",
                                              "0000000000", dbEx.Message, inner, dbEx.StackTrace, null, dateTime);

                return result;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "UpdateMailServerConfig",
                                              "0000000000", ex.Message, inner4, ex.StackTrace, null , dateTime);

                result = "Can't Update Mail Server.";
                return result;
            }
        }
        #endregion

        #region Delete

        public static string DeleteMailServer(string pMAIL_ID, string pSESSION_USER_ID)
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            string result = string.Empty;
            string FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "MailServer").Select(x => x.FUNCTION_ID).SingleOrDefault();
            try
            {
                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                LG_SYS_MAIL_SERVER_CONFIG OBJ_LG_SYS_MAIL_SERVER_CONFIG = (from m in Obj_DBModelEntities.LG_SYS_MAIL_SERVER_CONFIG
                                                                           where m.MAIL_ID == pMAIL_ID
                                                                           select m).SingleOrDefault();
                if (OBJ_LG_SYS_MAIL_SERVER_CONFIG != null)
                {
                    OBJ_LG_SYS_MAIL_SERVER_CONFIG.AUTH_STATUS_ID = "U";
                    OBJ_LG_SYS_MAIL_SERVER_CONFIG.LAST_ACTION = "DEL";
                    OBJ_LG_SYS_MAIL_SERVER_CONFIG.LAST_UPDATE_DT = System.DateTime.Now;
                    Obj_DBModelEntities.SaveChanges();

                    #region Auth log
                    LG_AA_NFT_AUTH_LOG_MAP OBJ_LG_AA_NFT_AUTH_LOG_MAP = new LG_AA_NFT_AUTH_LOG_MAP();
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "Mail Server").Select(x => x.FUNCTION_ID).SingleOrDefault();
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_NAME = "LG_SYS_MAIL_SERVER_CONFIG";
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_NM = "MAIL_ID";
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_VAL = OBJ_LG_SYS_MAIL_SERVER_CONFIG.MAIL_ID;
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.ACTION_STATUS = "DEL";
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.REMARKS = "";
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.PRIMARY_TABLE_FLAG = 0;
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.PARENT_TABLE_PK_VAL = null;
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.AUTH_STATUS_ID = "U";
                    int? auth_level_max = LG_AA_NFT_AUTH_LOG_MAP.GetNftAuthLevelMaxFromFunction(OBJ_LG_AA_NFT_AUTH_LOG_MAP.FUNCTION_ID);
                    if (auth_level_max.HasValue)
                    {
                        OBJ_LG_AA_NFT_AUTH_LOG_MAP.AUTH_LEVEL_MAX = (short)auth_level_max;
                    }
                    else
                        OBJ_LG_AA_NFT_AUTH_LOG_MAP.AUTH_LEVEL_MAX = 0;
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.AUTH_LEVEL_PENDING = OBJ_LG_AA_NFT_AUTH_LOG_MAP.AUTH_LEVEL_MAX;
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.REASON_DECLINE = "";
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.MAKE_BY = "pSESSION_USER_ID";
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.MAKE_DT = DateTime.Now;
                    LG_AA_NFT_AUTH_LOG_MAP.AddNftAuthLog(OBJ_LG_SYS_MAIL_SERVER_CONFIG, OBJ_LG_AA_NFT_AUTH_LOG_MAP);
                    #endregion

                    result = "True";
                    return result;
                }
                else
                    return "Can't delete mail server id.";
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
                        result = "Can't Delete Mail Server(Db).";
                    }
                }

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "DeleteMailServer",
                                              "0000000000", dbEx.Message, inner, dbEx.StackTrace, pSESSION_USER_ID, dateTime);

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


                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "DeleteMailServer",
                                                "0000000000", ex.Message, inner4, ex.StackTrace, pSESSION_USER_ID, dateTime);

                result = "Can't Delete Mail Server.";
                return result;
            }
        }
        #endregion

        #region Fetch Single

        public static LG_SYS_MAIL_SERVER_CONFIG_MAP GetMailServerConfigById(string pMAIL_ID)
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            LG_SYS_MAIL_SERVER_CONFIG_MAP OBJ_LG_SYS_MAIL_SERVER_CONFIG_MAP = new LG_SYS_MAIL_SERVER_CONFIG_MAP();

            try
            {
                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                OBJ_LG_SYS_MAIL_SERVER_CONFIG_MAP = (from m in Obj_DBModelEntities.LG_SYS_MAIL_SERVER_CONFIG
                                                     where m.MAIL_ID == pMAIL_ID
                                                     select new LG_SYS_MAIL_SERVER_CONFIG_MAP
                                                   {
                                                       MAIL_ID = m.MAIL_ID,
                                                       MAIL_SENDER_IP = m.MAIL_SENDER_IP,
                                                       MAIL_SENDER_ADDRESS = m.MAIL_SENDER_ADDRESS,
                                                       MAIL_SENDER_PASSWORD = m.MAIL_SENDER_PASSWORD,
                                                       MAIL_SENDER_NAME = m.MAIL_SENDER_NAME,
                                                       APPLICATION_ID = m.APPLICATION_ID,
                                                      
                                                       MAKE_DT = m.MAKE_DT
                                                   }).SingleOrDefault();
                return OBJ_LG_SYS_MAIL_SERVER_CONFIG_MAP;
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
                        OBJ_LG_SYS_MAIL_SERVER_CONFIG_MAP.ERROR = validationError.ErrorMessage;
                    }
                }

                LG_ERROR_LOG_MAP.Add_Error_Log(null, dbEx.Source, "ERR_APP_TYPE", "GetMailServerConfigById",
                                                "0000000000", dbEx.Message, inner, dbEx.StackTrace, null , dateTime);

                return OBJ_LG_SYS_MAIL_SERVER_CONFIG_MAP;
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


                LG_ERROR_LOG_MAP.Add_Error_Log(null, ex.Source, "ERR_APP_TYPE", "GetMailServerConfigById",
                                                "0000000000", ex.Message, inner4, ex.StackTrace, null , dateTime);

                OBJ_LG_SYS_MAIL_SERVER_CONFIG_MAP.ERROR = ex.Message;
                return OBJ_LG_SYS_MAIL_SERVER_CONFIG_MAP;
            }
        }
        #endregion

        #region Fetch all
        public static IEnumerable<LG_SYS_MAIL_SERVER_CONFIG_MAP> GetMailServerConfigs()
        {
            List<LG_SYS_MAIL_SERVER_CONFIG_MAP> LIST_LG_SYS_MAIL_SERVER_CONFIG_MAP = null;
            LG_SYS_MAIL_SERVER_CONFIG_MAP OBJ_LG_SYS_MAIL_SERVER_CONFIG_MAP = new LG_SYS_MAIL_SERVER_CONFIG_MAP();
            try
            {
                DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
                Obj_DBModelEntities.Configuration.LazyLoadingEnabled = false;
                Obj_DBModelEntities.Configuration.ProxyCreationEnabled = false;

                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                LIST_LG_SYS_MAIL_SERVER_CONFIG_MAP = Obj_DBModelEntities.LG_SYS_MAIL_SERVER_CONFIG
                                                .Join(Obj_DBModelEntities.LG_FNR_APPLICATION, m => m.APPLICATION_ID, a => a.APPLICATION_ID, (m, a) => new { m, a })
                                                .Where(T => T.m.MAIL_ID != null && 
                                                            T.m.AUTH_STATUS_ID == "A" && 
                                                            T.m.LAST_ACTION != "DEL")
                                                .Select(T =>
                                                new LG_SYS_MAIL_SERVER_CONFIG_MAP
                                                {
                                                    MAIL_ID = T.m.MAIL_ID,
                                                    MAIL_SENDER_IP = T.m.MAIL_SENDER_IP,
                                                    MAIL_SENDER_ADDRESS = T.m.MAIL_SENDER_ADDRESS,
                                                    MAIL_SENDER_PASSWORD = T.m.MAIL_SENDER_PASSWORD,
                                                    MAIL_SENDER_NAME = T.m.MAIL_SENDER_NAME,
                                                    APPLICATION_ID = T.m.APPLICATION_ID,
                                                    APPLICATION_NAME = T.a.APPLICATION_NAME,
                                                    AUTH_STATUS_ID=T.a.AUTH_STATUS_ID,
                                                    MAKE_DT = T.m.MAKE_DT
                                                }).ToList();
                return LIST_LG_SYS_MAIL_SERVER_CONFIG_MAP;
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
                        OBJ_LG_SYS_MAIL_SERVER_CONFIG_MAP.ERROR = validationError.ErrorMessage;
                        LIST_LG_SYS_MAIL_SERVER_CONFIG_MAP.Add(OBJ_LG_SYS_MAIL_SERVER_CONFIG_MAP);
                    }
                }

                LG_ERROR_LOG_MAP.Add_Error_Log(null, dbEx.Source, "ERR_APP_TYPE", "GetMailServerConfigs",
                                              "0000000000", dbEx.Message, inner, dbEx.StackTrace, null, dateTime);

                return LIST_LG_SYS_MAIL_SERVER_CONFIG_MAP;
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

                LG_ERROR_LOG_MAP.Add_Error_Log(null, ex.Source, "ERR_APP_TYPE", "GetMailServerConfigs",
                                             "0000000000", ex.Message, inner4, ex.StackTrace, null , dateTime);

                OBJ_LG_SYS_MAIL_SERVER_CONFIG_MAP.ERROR = ex.Message;
                LIST_LG_SYS_MAIL_SERVER_CONFIG_MAP.Add(OBJ_LG_SYS_MAIL_SERVER_CONFIG_MAP);
                return LIST_LG_SYS_MAIL_SERVER_CONFIG_MAP;
            }
        }
        #endregion

        #region Validate
        #endregion

        #endregion
    }
}
