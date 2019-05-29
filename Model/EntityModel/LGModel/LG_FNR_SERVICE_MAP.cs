using Model.EDMX;
using Model.EntityModel.Common;
using Model.Mapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Web.Mvc;

namespace Model.EntityModel.LGModel
{
    [DataContract]
    public class LG_FNR_SERVICE_MAP
    {
        #region Properties
        [DataMember]
        public string APPLICATION_ID { get; set; }
        [DataMember]
        public string APPLICATION_NAME { get; set; }
        [DataMember]
        public string SERVICE_ID { get; set; }
        [DataMember]
        public string SERVICE_NM { get; set; }
        [DataMember]
        public string SERVICE_SH_NM { get; set; }
        [DataMember]
        public string MAKE_BY { get; set; }
        [DataMember]
        public DateTime MAKE_DT { get; set; }
        [DataMember]
        public string AUTH_STATUS_ID { get; set; }
        [DataMember]
        public string LAST_ACTION { get; set; }
        [DataMember]
        public DateTime? LAST_UPDATE_DT { get; set; }
        [DataMember]
        public string ERROR { get; set; }
        [DataMember]
        public IEnumerable<SelectListItem> APPLICATION_LIST_FOR_DD { get; set; }
        #endregion


        #region Events

        public static string FUNCTION_ID = "010101002";

        #region Add New
        public static string AddService(string pSERVICE_NM, string pSERVICE_SH_NM, string pAPPLICATION_ID, string psession_user)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
                string result = string.Empty;
                try
                {
                    string format = OutgoingResponseFormat.GetFormat();
                    OutgoingResponseFormat.SetResponseFormat(format);

                    LG_FNR_SERVICE OBJ_LG_FNR_SERVICE = new LG_FNR_SERVICE();
                    OBJ_LG_FNR_SERVICE = Obj_DBModelEntities.LG_FNR_SERVICE
                                        .Where(s => s.SERVICE_NM == pSERVICE_NM).SingleOrDefault();

                    if (OBJ_LG_FNR_SERVICE != null)
                    {
                        return "Service name already exists";
                    }

                    //int id = (Obj_DBModelEntities.LG_FNR_SERVICE
                    //         .Select(i => i.SERVICE_ID).Cast<int?>().Max() ?? 0) + 1;

                    int id = (Obj_DBModelEntities.LG_FNR_SERVICE
                             .Where(i => i.APPLICATION_ID == pAPPLICATION_ID)
                             .Select(i => i.SERVICE_ID).Cast<int?>().Max() ?? 0) + 1;

                    OBJ_LG_FNR_SERVICE = new LG_FNR_SERVICE();
                    OBJ_LG_FNR_SERVICE.SERVICE_ID = id.ToString().PadLeft(2, '0');
                    OBJ_LG_FNR_SERVICE.SERVICE_NM = pSERVICE_NM;
                    OBJ_LG_FNR_SERVICE.SERVICE_SH_NM = pSERVICE_SH_NM;
                    OBJ_LG_FNR_SERVICE.APPLICATION_ID = pAPPLICATION_ID;
                    OBJ_LG_FNR_SERVICE.AUTH_STATUS_ID = "U";
                    OBJ_LG_FNR_SERVICE.LAST_ACTION = "ADD";
                    OBJ_LG_FNR_SERVICE.MAKE_DT = System.DateTime.Now;
                    Obj_DBModelEntities.LG_FNR_SERVICE.Add(OBJ_LG_FNR_SERVICE);
                    Obj_DBModelEntities.SaveChanges();

                    LG_FNR_SERVICE_MAP OBJ_LG_FNR_SERVICE_MAP = new LG_FNR_SERVICE_MAP();
                    Class_Conversion.LG_FNR_SERVICE_REVERSE_CON(OBJ_LG_FNR_SERVICE_MAP, OBJ_LG_FNR_SERVICE);

                    #region Auth log
                    LG_AA_NFT_AUTH_LOG_MAP OBJ_LG_AA_NFT_AUTH_LOG_MAP = new LG_AA_NFT_AUTH_LOG_MAP();
                    FUNCTION_ID = OBJ_LG_AA_NFT_AUTH_LOG_MAP.FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "Service").Select(x => x.FUNCTION_ID).SingleOrDefault();
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_NAME = "LG_FNR_SERVICE";
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_NM = "APPLICATION_ID;;SERVICE_ID";
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_VAL = OBJ_LG_FNR_SERVICE.APPLICATION_ID + ";;" + OBJ_LG_FNR_SERVICE.SERVICE_ID;
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
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.MAKE_BY = psession_user;
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.MAKE_DT = DateTime.Now;
                    LG_AA_NFT_AUTH_LOG_MAP.AddNftAuthLog(OBJ_LG_FNR_SERVICE_MAP, OBJ_LG_AA_NFT_AUTH_LOG_MAP);
                    #endregion

                    ts.Complete();
                    result = "True";
                    return result;
                }
                catch (DbEntityValidationException dbEx)
                {
                    ts.Dispose();
                    string dateTime = Convert.ToString(System.DateTime.Now);
                    string inner = "";

                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                            inner = validationError.PropertyName + "::" + validationError.ErrorMessage + ";;" + inner;
                            result = "Can't Add Service(Db).";
                        }
                    }
                    LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "AddService",
                                                  "0000000000", dbEx.Message, inner, dbEx.StackTrace, psession_user, dateTime);
                    return result;
                }
                catch (Exception ex)
                {
                    ts.Dispose();
                    string dateTime = Convert.ToString(System.DateTime.Now);
                    string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                    LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "AddService",
                                                  "0000000000", ex.Message, inner4, ex.StackTrace, psession_user, dateTime);
                    result = "Can't Add Service.";
                    return result;
                }
            }
        }
        #endregion

        #region Update
        public static string UpdateService(string pSERVICE_ID, string pSERVICE_NM, string pSERVICE_SH_NM, string pAPPLICATION_ID, string psession_user)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
                string result = string.Empty;
                LG_FNR_SERVICE_MAP OBJ_LG_FNR_SERVICE_MAP_OLD = new LG_FNR_SERVICE_MAP();
                LG_FNR_SERVICE_MAP OBJ_LG_FNR_SERVICE_MAP_NEW = new LG_FNR_SERVICE_MAP();
                try
                {
                    string format = OutgoingResponseFormat.GetFormat();
                    OutgoingResponseFormat.SetResponseFormat(format);

                    LG_FNR_SERVICE OBJ_LG_FNR_SERVICE = Obj_DBModelEntities.LG_FNR_SERVICE
                                                        .Where(s => s.SERVICE_ID == pSERVICE_ID &&
                                                                    s.APPLICATION_ID == pAPPLICATION_ID).SingleOrDefault();

                    if (OBJ_LG_FNR_SERVICE == null)
                    {
                        return "false";
                    }
                    if (OBJ_LG_FNR_SERVICE.SERVICE_NM == pSERVICE_NM)
                    {
                        return "no changes made";
                    }
                    Class_Conversion.LG_FNR_SERVICE_REVERSE_CON(OBJ_LG_FNR_SERVICE_MAP_OLD, OBJ_LG_FNR_SERVICE);

                    OBJ_LG_FNR_SERVICE.AUTH_STATUS_ID = "U";
                    OBJ_LG_FNR_SERVICE.LAST_ACTION = "EDT";
                    OBJ_LG_FNR_SERVICE.LAST_UPDATE_DT = System.DateTime.Now;
                    Obj_DBModelEntities.SaveChanges();
                    Class_Conversion.LG_FNR_SERVICE_REVERSE_CON(OBJ_LG_FNR_SERVICE_MAP_NEW, OBJ_LG_FNR_SERVICE);
                    OBJ_LG_FNR_SERVICE_MAP_NEW.SERVICE_NM = pSERVICE_NM;
                    OBJ_LG_FNR_SERVICE_MAP_NEW.SERVICE_SH_NM = pSERVICE_SH_NM;
                    OBJ_LG_FNR_SERVICE_MAP_NEW.APPLICATION_ID = pAPPLICATION_ID;

                    #region Auth log
                    LG_AA_NFT_AUTH_LOG_MAP OBJ_LG_AA_NFT_AUTH_LOG_MAP = new LG_AA_NFT_AUTH_LOG_MAP();
                    FUNCTION_ID = OBJ_LG_AA_NFT_AUTH_LOG_MAP.FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "Service").Select(x => x.FUNCTION_ID).SingleOrDefault();
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_NAME = "LG_FNR_SERVICE";
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_NM = "APPLICATION_ID;;SERVICE_ID";
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_VAL = OBJ_LG_FNR_SERVICE.APPLICATION_ID + ";;" + OBJ_LG_FNR_SERVICE.SERVICE_ID;
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
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.MAKE_BY = psession_user;
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.MAKE_DT = DateTime.Now;
                    LG_AA_NFT_AUTH_LOG_MAP.AddNftAuthLog(OBJ_LG_FNR_SERVICE_MAP_OLD, OBJ_LG_FNR_SERVICE_MAP_NEW, OBJ_LG_AA_NFT_AUTH_LOG_MAP);
                    #endregion

                    ts.Complete();
                    result = "True";
                    return result;
                }
                catch (DbEntityValidationException dbEx)
                {
                    ts.Dispose();
                    string dateTime = Convert.ToString(System.DateTime.Now);
                    string inner = "";

                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                            inner = validationError.PropertyName + "::" + validationError.ErrorMessage + ";;" + inner;
                            result = "Can't Update Service(Db) ";
                        }
                    }
                    LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "UpdateService",
                                                  "0000000000", dbEx.Message, inner, dbEx.StackTrace, psession_user, dateTime);
                    return result;
                }
                catch (Exception ex)
                {
                    ts.Dispose();
                    string dateTime = Convert.ToString(System.DateTime.Now);
                    string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                    LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "UpdateService",
                                                  "0000000000", ex.Message, inner4, ex.StackTrace, psession_user, dateTime);
                    result = "Can't Update Service.";
                    return result;
                }
            }

            
        }
        #endregion

        #region Delete
        public static string DeleteService(string pSERVICE_ID, string psession_user)
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            string result = string.Empty;
            try
            {
                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                LG_FNR_SERVICE OBJ_LG_FNR_SERVICE = (from s in Obj_DBModelEntities.LG_FNR_SERVICE
                                                     where !(from m in Obj_DBModelEntities.LG_FNR_MODULE
                                                             select m.SERVICE_ID).Contains(s.SERVICE_ID)
                                                             && s.SERVICE_ID == pSERVICE_ID
                                                     select s).SingleOrDefault();
                if (OBJ_LG_FNR_SERVICE != null)
                {
                    Obj_DBModelEntities.LG_FNR_SERVICE.Remove(OBJ_LG_FNR_SERVICE);
                    Obj_DBModelEntities.SaveChanges();

                    #region Auth log
                    LG_AA_NFT_AUTH_LOG_MAP OBJ_LG_AA_NFT_AUTH_LOG_MAP = new LG_AA_NFT_AUTH_LOG_MAP();
                    FUNCTION_ID = OBJ_LG_AA_NFT_AUTH_LOG_MAP.FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "Service").Select(x => x.FUNCTION_ID).SingleOrDefault();
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_NAME = "LG_FNR_SERVICE";
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_NM = "SERVICE_ID";
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_VAL = OBJ_LG_FNR_SERVICE.SERVICE_ID;
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
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.MAKE_BY = psession_user;
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.MAKE_DT = DateTime.Now;
                    LG_AA_NFT_AUTH_LOG_MAP.AddNftAuthLog(OBJ_LG_FNR_SERVICE, OBJ_LG_AA_NFT_AUTH_LOG_MAP);
                    #endregion

                    result = "True";
                    return result;
                }
                else
                    return "Can't delete as this Service contains Modules.";
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
                        result = "Can't Delete Service(Db) " ;
                    }
                }
                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "DeleteService",
                       "0000000000", dbEx.Message, inner, dbEx.StackTrace, psession_user, dateTime);

 
                return result;
            }
            catch (Exception ex)
            {
                result = "Can't Delete Service ";
                return result;
            }
        }
        #endregion

        #region Fetch Single
        public static LG_FNR_SERVICE_MAP GetServiceByServiceId(string pSERVICE_ID, string pAPPLICATION_ID)
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            LG_FNR_SERVICE_MAP OBJ_LG_FNR_SERVICE_MAP = new LG_FNR_SERVICE_MAP();

            try
            {
                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                OBJ_LG_FNR_SERVICE_MAP = (from s in Obj_DBModelEntities.LG_FNR_SERVICE
                                          join a in Obj_DBModelEntities.LG_FNR_APPLICATION
                                          on s.APPLICATION_ID equals a.APPLICATION_ID
                                          where s.SERVICE_ID == pSERVICE_ID &&
                                                s.APPLICATION_ID == pAPPLICATION_ID
                                          orderby s.SERVICE_NM ascending
                                          select new LG_FNR_SERVICE_MAP
                                          {
                                              APPLICATION_ID = s.APPLICATION_ID,
                                              APPLICATION_NAME = a.APPLICATION_NAME,
                                              SERVICE_ID = s.SERVICE_ID,
                                              SERVICE_NM = s.SERVICE_NM,
                                              SERVICE_SH_NM = s.SERVICE_SH_NM,
                                              AUTH_STATUS_ID = s.AUTH_STATUS_ID,
                                              LAST_ACTION = s.LAST_ACTION,
                                              LAST_UPDATE_DT = s.LAST_UPDATE_DT,
                                              MAKE_DT = s.MAKE_DT
                                          }).SingleOrDefault();
                OBJ_LG_FNR_SERVICE_MAP.APPLICATION_LIST_FOR_DD = DropDown.GetApplicationForDD();
                return OBJ_LG_FNR_SERVICE_MAP;
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
                        OBJ_LG_FNR_SERVICE_MAP.ERROR = validationError.ErrorMessage;
                    }
                }
                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "GetServiceByServiceId",
                      "0000000000", dbEx.Message, inner, dbEx.StackTrace, null, dateTime);
                return OBJ_LG_FNR_SERVICE_MAP;
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


                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "GetServiceByServiceId",
                       "0000000000", ex.Message, inner4, ex.StackTrace, null, dateTime);

                OBJ_LG_FNR_SERVICE_MAP.ERROR = ex.Message;
                return OBJ_LG_FNR_SERVICE_MAP;
            }
        }
        #endregion

        #region Fetch all
        public static IEnumerable<LG_FNR_SERVICE_MAP> GetServices()
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            List<LG_FNR_SERVICE_MAP> LIST_LG_FNR_SERVICE_MAP = null;
            LG_FNR_SERVICE_MAP OBJ_LG_FNR_SERVICE_MAP = new LG_FNR_SERVICE_MAP();

            try
            {
                Obj_DBModelEntities.Configuration.LazyLoadingEnabled = false;
                Obj_DBModelEntities.Configuration.ProxyCreationEnabled = false;

                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                
                LIST_LG_FNR_SERVICE_MAP = (from s in Obj_DBModelEntities.LG_FNR_SERVICE
                                           where ( s.AUTH_STATUS_ID == "A" && (s.LAST_ACTION != "DEL" ))
                                           join a in Obj_DBModelEntities.LG_FNR_APPLICATION
                                           on s.APPLICATION_ID equals a.APPLICATION_ID
                                           orderby s.SERVICE_ID descending
                                           select new LG_FNR_SERVICE_MAP
                                           {
                                               APPLICATION_ID = s.APPLICATION_ID,
                                               APPLICATION_NAME = a.APPLICATION_NAME,
                                               SERVICE_ID = s.SERVICE_ID,
                                               SERVICE_NM = s.SERVICE_NM,
                                               SERVICE_SH_NM = s.SERVICE_SH_NM,
                                               AUTH_STATUS_ID = s.AUTH_STATUS_ID,
                                               LAST_ACTION = s.LAST_ACTION,
                                               LAST_UPDATE_DT = s.LAST_UPDATE_DT,
                                               MAKE_DT = s.MAKE_DT
                                           }).ToList(); 

                return LIST_LG_FNR_SERVICE_MAP;
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
                        OBJ_LG_FNR_SERVICE_MAP.ERROR = validationError.ErrorMessage;
                        LIST_LG_FNR_SERVICE_MAP.Add(OBJ_LG_FNR_SERVICE_MAP);
                    }
                }
                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "GetServices",
                      "0000000000", dbEx.Message, inner, dbEx.StackTrace, null, dateTime);

                return LIST_LG_FNR_SERVICE_MAP;
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


                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "GetServices",
                       "0000000000", ex.Message, inner4, ex.StackTrace, null, dateTime);

                OBJ_LG_FNR_SERVICE_MAP.ERROR = ex.Message;
                LIST_LG_FNR_SERVICE_MAP.Add(OBJ_LG_FNR_SERVICE_MAP);
                return LIST_LG_FNR_SERVICE_MAP;
            }
        }
        #endregion

        #region Validate
        #endregion

        #endregion
    }
}
