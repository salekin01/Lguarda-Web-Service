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
    public class LG_FNR_MODULE_MAP
    {
        #region Properties
        [DataMember]
        public string MODULE_ID { get; set; }
        [DataMember]
        public string MODULE_NM { get; set; }
        [DataMember]
        public string MODULE_SH_NM { get; set; }
        [DataMember]
        public string MAKE_BY { get; set; }
        [DataMember]
        public DateTime? MAKE_DT { get; set; }
        [DataMember]
        public string APPLICATION_ID { get; set; }
        [DataMember]
        public string APPLICATION_NAME { get; set; }
        [DataMember]
        public string SERVICE_ID { get; set; }
        [DataMember]
        public string SERVICE_NM { get; set; }
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
        [DataMember]
        public IEnumerable<SelectListItem> SERVICE_LIST_FOR_DD { get; set; }
        #endregion


        #region Events

        public static string FUNCTION_ID = "010101003";

        #region Add New
        public static string AddModule(string pMODULE_NM, string pMODULE_SH_NM, string pAPPLICATION_ID, string pSERVICE_ID, string psession_user)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
                LG_FNR_MODULE OBJ_LG_FNR_MODULE = new LG_FNR_MODULE();
                string result = string.Empty;
                try
                {
                    string format = OutgoingResponseFormat.GetFormat();
                    OutgoingResponseFormat.SetResponseFormat(format);

                    OBJ_LG_FNR_MODULE = Obj_DBModelEntities.LG_FNR_MODULE
                                       .Where(m => m.MODULE_NM == pMODULE_NM).SingleOrDefault();
                    if (OBJ_LG_FNR_MODULE != null)
                    {
                        return "Moulde name already exists";
                    }

                    int id = (Obj_DBModelEntities.LG_FNR_MODULE
                             .Where(i => i.SERVICE_ID == pSERVICE_ID &&
                                         i.APPLICATION_ID == pAPPLICATION_ID)
                             .Select(i => i.MODULE_ID).Cast<int?>().Max() ?? 0) + 1;

                    OBJ_LG_FNR_MODULE = new LG_FNR_MODULE();
                    OBJ_LG_FNR_MODULE.MODULE_ID = id.ToString().PadLeft(2, '0');
                    OBJ_LG_FNR_MODULE.MODULE_NM = pMODULE_NM;
                    OBJ_LG_FNR_MODULE.MODULE_SH_NM = pMODULE_SH_NM;
                    OBJ_LG_FNR_MODULE.SERVICE_ID = pSERVICE_ID;
                    OBJ_LG_FNR_MODULE.APPLICATION_ID = pAPPLICATION_ID;
                    OBJ_LG_FNR_MODULE.AUTH_STATUS_ID = "U";
                    OBJ_LG_FNR_MODULE.LAST_ACTION = "ADD";
                    OBJ_LG_FNR_MODULE.MAKE_DT = System.DateTime.Now;
                    Obj_DBModelEntities.LG_FNR_MODULE.Add(OBJ_LG_FNR_MODULE);
                    Obj_DBModelEntities.SaveChanges();

                    LG_FNR_MODULE_MAP OBJ_LG_FNR_MODULE_MAP = new LG_FNR_MODULE_MAP();
                    Class_Conversion.LG_FNR_MODULE_REVERSE_CON(OBJ_LG_FNR_MODULE_MAP, OBJ_LG_FNR_MODULE);

                    #region Auth log
                    LG_AA_NFT_AUTH_LOG_MAP OBJ_LG_AA_NFT_AUTH_LOG_MAP = new LG_AA_NFT_AUTH_LOG_MAP();
                    FUNCTION_ID = OBJ_LG_AA_NFT_AUTH_LOG_MAP.FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "Module").Select(x => x.FUNCTION_ID).SingleOrDefault();
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_NAME = "LG_FNR_MODULE";
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_NM = "APPLICATION_ID;;SERVICE_ID;;MODULE_ID";
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_VAL = OBJ_LG_FNR_MODULE.APPLICATION_ID + ";;" + OBJ_LG_FNR_MODULE.SERVICE_ID + ";;" + OBJ_LG_FNR_MODULE.MODULE_ID;
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
                    LG_AA_NFT_AUTH_LOG_MAP.AddNftAuthLog(OBJ_LG_FNR_MODULE_MAP, OBJ_LG_AA_NFT_AUTH_LOG_MAP);
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
                            result = "Can't Add Module(Db) ";
                        }
                    }
                    LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "AddModule",
                                                  "0000000000", dbEx.Message, inner, dbEx.StackTrace, psession_user, dateTime);
                    return result;
                }
                catch (Exception ex)
                {
                    ts.Dispose();
                    string dateTime = Convert.ToString(System.DateTime.Now);
                    string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                    LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "AddModule",
                                                  "0000000000", ex.Message, inner4, ex.StackTrace, psession_user, dateTime);
                    result = "Can't Add Module.";
                    return result;
                }
            }
        }
        #endregion

        #region Update
        public static string UpdateModule(string pMODULE_ID, string pMODULE_NM, string pMODULE_SH_NM, string pAPPLICATION_ID, string pSERVICE_ID, string psession_user)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
                string result = string.Empty;
                LG_FNR_MODULE_MAP OBJ_LG_FNR_MODULE_MAP_OLD = new LG_FNR_MODULE_MAP();
                LG_FNR_MODULE_MAP OBJ_LG_FNR_MODULE_MAP_NEW = new LG_FNR_MODULE_MAP();
                try
                {
                    string format = OutgoingResponseFormat.GetFormat();
                    OutgoingResponseFormat.SetResponseFormat(format);

                    LG_FNR_MODULE OBJ_LG_FNR_MODULE = Obj_DBModelEntities.LG_FNR_MODULE
                                                     .Where(s => s.MODULE_ID == pMODULE_ID && s.SERVICE_ID == pSERVICE_ID && s.APPLICATION_ID == pAPPLICATION_ID).SingleOrDefault();

                    if (OBJ_LG_FNR_MODULE == null)
                    {
                        return "False";
                    }

                    if (OBJ_LG_FNR_MODULE.MODULE_NM == pMODULE_NM && OBJ_LG_FNR_MODULE.MODULE_SH_NM == pMODULE_SH_NM && OBJ_LG_FNR_MODULE.SERVICE_ID == pSERVICE_ID && OBJ_LG_FNR_MODULE.APPLICATION_ID == pAPPLICATION_ID)
                    {
                        return "no changes made";
                    }
                    Class_Conversion.LG_FNR_MODULE_REVERSE_CON(OBJ_LG_FNR_MODULE_MAP_OLD, OBJ_LG_FNR_MODULE);

                    OBJ_LG_FNR_MODULE.AUTH_STATUS_ID = "U";
                    OBJ_LG_FNR_MODULE.LAST_ACTION = "EDT";
                    OBJ_LG_FNR_MODULE.LAST_UPDATE_DT = System.DateTime.Now;
                    Obj_DBModelEntities.SaveChanges();
                    Class_Conversion.LG_FNR_MODULE_REVERSE_CON(OBJ_LG_FNR_MODULE_MAP_NEW, OBJ_LG_FNR_MODULE);
                    OBJ_LG_FNR_MODULE_MAP_NEW.MODULE_NM = pMODULE_NM;
                    OBJ_LG_FNR_MODULE_MAP_NEW.MODULE_SH_NM = pMODULE_SH_NM;
                    OBJ_LG_FNR_MODULE_MAP_NEW.SERVICE_ID = pSERVICE_ID;
                    OBJ_LG_FNR_MODULE_MAP_NEW.APPLICATION_ID = pAPPLICATION_ID;

                    #region Auth log
                    LG_AA_NFT_AUTH_LOG_MAP OBJ_LG_AA_NFT_AUTH_LOG_MAP = new LG_AA_NFT_AUTH_LOG_MAP();
                    FUNCTION_ID = OBJ_LG_AA_NFT_AUTH_LOG_MAP.FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "Module").Select(x => x.FUNCTION_ID).SingleOrDefault();
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_NAME = "LG_FNR_MODULE";
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_NM = "APPLICATION_ID;;SERVICE_ID;;MODULE_ID";
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_VAL = OBJ_LG_FNR_MODULE.APPLICATION_ID + ";;" + OBJ_LG_FNR_MODULE.SERVICE_ID + ";;" + OBJ_LG_FNR_MODULE.MODULE_ID;
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
                    LG_AA_NFT_AUTH_LOG_MAP.AddNftAuthLog(OBJ_LG_FNR_MODULE_MAP_OLD, OBJ_LG_FNR_MODULE_MAP_NEW, OBJ_LG_AA_NFT_AUTH_LOG_MAP);
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
                            result = "Can't update Module(Db)";
                        }
                    }
                    LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "UpdateModule",
                                                  "0000000000", dbEx.Message, inner, dbEx.StackTrace, psession_user, dateTime);
                    return result;
                }
                catch (Exception ex)
                {
                    ts.Dispose();
                    string dateTime = Convert.ToString(System.DateTime.Now);
                    string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                    LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "UpdateModule",
                                                  "0000000000", ex.Message, inner4, ex.StackTrace, psession_user, dateTime);
                    result = "Can't update Module";
                    return result;
                }
            }
        }
        #endregion

        #region Delete
        public static string DeleteModule(string pMODULE_ID, string psession_user)
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            string result = string.Empty;
            try
            {
                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                LG_FNR_MODULE OBJ_LG_FNR_MODULE = (from m in Obj_DBModelEntities.LG_FNR_MODULE
                                                   where !(from f in Obj_DBModelEntities.LG_FNR_FUNCTION
                                                           select f.MODULE_ID).Contains(m.MODULE_ID)
                                                   && m.MODULE_ID == pMODULE_ID
                                                   select m).SingleOrDefault();
                if (OBJ_LG_FNR_MODULE != null)
                {
                    Obj_DBModelEntities.LG_FNR_MODULE.Remove(OBJ_LG_FNR_MODULE);
                    Obj_DBModelEntities.SaveChanges();

                    #region Auth log
                    LG_AA_NFT_AUTH_LOG_MAP OBJ_LG_AA_NFT_AUTH_LOG_MAP = new LG_AA_NFT_AUTH_LOG_MAP();
                    FUNCTION_ID = OBJ_LG_AA_NFT_AUTH_LOG_MAP.FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "Module").Select(x => x.FUNCTION_ID).SingleOrDefault();
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_NAME = "LG_FNR_MODULE";
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_NM = "MODULE_ID";
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_VAL = OBJ_LG_FNR_MODULE.MODULE_ID;
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
                    LG_AA_NFT_AUTH_LOG_MAP.AddNftAuthLog(OBJ_LG_FNR_MODULE, OBJ_LG_AA_NFT_AUTH_LOG_MAP);
                    #endregion

                    result = "True";
                    return result;
                }
                else
                    return "Can't delete as this Module contains Functions.";
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
                        result = "Can't Delete Module(Db) " ;
                    }
                }
                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "DeleteModule",
                       "0000000000", dbEx.Message, inner, dbEx.StackTrace, psession_user, dateTime);
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


                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "DeleteModule",
                       "0000000000", ex.Message, inner4, ex.StackTrace, psession_user, dateTime);
                result = "Can't Delete Module " ;
                return result;
            }
        }
        #endregion

        #region Fetch Single
        public static LG_FNR_MODULE_MAP GetModuleByServiceIdAndModuleId(string pSERVICE_ID, string pMODULE_ID, string pAPPLICATION_ID)
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            LG_FNR_MODULE_MAP OBJ_LG_FNR_MODULE_MAP = new LG_FNR_MODULE_MAP();

            try
            {
                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                OBJ_LG_FNR_MODULE_MAP = (from m in Obj_DBModelEntities.LG_FNR_MODULE
                                              where m.MODULE_ID == pMODULE_ID &&
                                                    m.SERVICE_ID == pSERVICE_ID &&
                                                    m.APPLICATION_ID == pAPPLICATION_ID
                                              select new LG_FNR_MODULE_MAP
                                              {
                                                  MODULE_ID = m.MODULE_ID,
                                                  MODULE_NM = m.MODULE_NM,
                                                  MODULE_SH_NM = m.MODULE_SH_NM,
                                                  AUTH_STATUS_ID = m.AUTH_STATUS_ID,
                                                  LAST_ACTION = m.LAST_ACTION,
                                                  LAST_UPDATE_DT = m.LAST_UPDATE_DT,
                                                  MAKE_DT = m.MAKE_DT,
                                                  APPLICATION_ID = m.APPLICATION_ID,
                                                  //APPLICATION_NAME = a.APPLICATION_NAME,
                                                  SERVICE_ID = m.SERVICE_ID,
                                                  //SERVICE_NM = s.SERVICE_NM
                                              }).SingleOrDefault();

                OBJ_LG_FNR_MODULE_MAP.APPLICATION_NAME = Obj_DBModelEntities.LG_FNR_APPLICATION.Where(x => x.APPLICATION_ID == OBJ_LG_FNR_MODULE_MAP.APPLICATION_ID).Select(x => x.APPLICATION_NAME).SingleOrDefault();
                OBJ_LG_FNR_MODULE_MAP.SERVICE_NM = Obj_DBModelEntities.LG_FNR_SERVICE.Where(x => x.SERVICE_ID == OBJ_LG_FNR_MODULE_MAP.SERVICE_ID && x.APPLICATION_ID == OBJ_LG_FNR_MODULE_MAP.APPLICATION_ID).Select(x => x.SERVICE_NM).SingleOrDefault();

                OBJ_LG_FNR_MODULE_MAP.APPLICATION_LIST_FOR_DD = DropDown.GetApplicationForDD();
                OBJ_LG_FNR_MODULE_MAP.SERVICE_LIST_FOR_DD = DropDown.GetServiceForDD();
                return OBJ_LG_FNR_MODULE_MAP;
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
                        OBJ_LG_FNR_MODULE_MAP.ERROR = validationError.ErrorMessage;
                    }
                }
                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "GetModuleByModuleId",
                                              "0000000000", dbEx.Message, inner, dbEx.StackTrace, null, dateTime);
                return OBJ_LG_FNR_MODULE_MAP;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "GetModuleByModuleId",
                                              "0000000000", ex.Message, inner4, ex.StackTrace, null, dateTime);
                OBJ_LG_FNR_MODULE_MAP.ERROR = ex.Message;
                return OBJ_LG_FNR_MODULE_MAP;
            }
        }
        #endregion

        #region Fetch all
        public static IEnumerable<LG_FNR_MODULE_MAP> GetModules()
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            List<LG_FNR_MODULE_MAP> LIST_LG_FNR_MODULE_MAP = null;
            LG_FNR_MODULE_MAP OBJ_LG_FNR_MODULE_MAP = new LG_FNR_MODULE_MAP();

            try
            {
                Obj_DBModelEntities.Configuration.LazyLoadingEnabled = false;
                Obj_DBModelEntities.Configuration.ProxyCreationEnabled = false;

                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);


                LIST_LG_FNR_MODULE_MAP = (from m in Obj_DBModelEntities.LG_FNR_MODULE
                                          where m.AUTH_STATUS_ID != "U" && m.LAST_ACTION!="DEL"
                                          orderby m.APPLICATION_ID, m.SERVICE_ID, m.MODULE_ID // by default ascending
                                          select new LG_FNR_MODULE_MAP
                                          {
                                              MODULE_ID = m.MODULE_ID,
                                              MODULE_NM = m.MODULE_NM,
                                              MODULE_SH_NM = m.MODULE_SH_NM,
                                              AUTH_STATUS_ID = m.AUTH_STATUS_ID,
                                              LAST_ACTION = m.LAST_ACTION,
                                              LAST_UPDATE_DT = m.LAST_UPDATE_DT,
                                              MAKE_DT = m.MAKE_DT,
                                              APPLICATION_ID = m.APPLICATION_ID,
                                              //APPLICATION_NAME = a.APPLICATION_NAME,
                                              SERVICE_ID = m.SERVICE_ID,
                                              //SERVICE_NM = s.SERVICE_NM
                                          }).ToList();
                LIST_LG_FNR_MODULE_MAP.ForEach(a =>
                    {
                        a.APPLICATION_NAME = Obj_DBModelEntities.LG_FNR_APPLICATION.Where(x => x.APPLICATION_ID == a.APPLICATION_ID).Select(x => x.APPLICATION_NAME).SingleOrDefault();
                        a.SERVICE_NM = Obj_DBModelEntities.LG_FNR_SERVICE.Where(x => x.SERVICE_ID == a.SERVICE_ID && x.APPLICATION_ID == a.APPLICATION_ID).Select(x => x.SERVICE_NM).SingleOrDefault();
                    });             
                    

                return LIST_LG_FNR_MODULE_MAP;
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
                        OBJ_LG_FNR_MODULE_MAP.ERROR = validationError.ErrorMessage;
                        LIST_LG_FNR_MODULE_MAP.Add(OBJ_LG_FNR_MODULE_MAP);
                    }
                }
                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "GetModules",
                                              "0000000000", dbEx.Message, inner, dbEx.StackTrace, null, dateTime);
                return LIST_LG_FNR_MODULE_MAP;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "GetModuleByModuleId",
                                              "0000000000", ex.Message, inner4, ex.StackTrace, null, dateTime);
                OBJ_LG_FNR_MODULE_MAP.ERROR = ex.Message;
                LIST_LG_FNR_MODULE_MAP.Add(OBJ_LG_FNR_MODULE_MAP);
                return LIST_LG_FNR_MODULE_MAP;
            }
        }
        #endregion

        #region Validate

        #endregion
        #endregion 
    }
}
