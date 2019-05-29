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

namespace Model.EntityModel.LGModel
{
    [DataContract]
    public class LG_FNR_ROLE_MAP
    {
        #region Properties
        [DataMember]
        public string ROLE_ID { get; set; }
        [DataMember]
        public string ROLE_NAME { get; set; }
        [DataMember]
        public string ROLE_DESCRIP { get; set; }
        [DataMember]
        public short IS_SYS_ADMIN { get; set; }
        [DataMember]
        public string AUTH_STATUS_ID { get; set; }
        [DataMember]
        public string LAST_ACTION { get; set; }
        [DataMember]
        public DateTime? LAST_UPDATE_DT { get; set; }
        [DataMember]
        public DateTime MAKE_DT { get; set; }
        [DataMember]
        public string ERROR { get; set; }
        #endregion


        #region Events

        public static string FUNCTION_ID = "010101005";

        #region Add New
        public static string AddRole(string pROLE_NAME, string pROLE_DESCRIP, string psession_user)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
                LG_FNR_ROLE OBJ_LG_FNR_ROLE = new LG_FNR_ROLE();
                string result = string.Empty;
                try
                {
                    string format = OutgoingResponseFormat.GetFormat();
                    OutgoingResponseFormat.SetResponseFormat(format);

                    OBJ_LG_FNR_ROLE = Obj_DBModelEntities.LG_FNR_ROLE
                                     .Where(a => a.ROLE_NAME == pROLE_NAME).SingleOrDefault();
                    if (OBJ_LG_FNR_ROLE != null)
                    {
                        return "Role name already exists";
                    }

                    OBJ_LG_FNR_ROLE = new LG_FNR_ROLE();
                    OBJ_LG_FNR_ROLE.ROLE_ID = ((Obj_DBModelEntities.LG_FNR_ROLE.Select(i => i.ROLE_ID).Cast<int?>().Max() ?? 0) + 1).ToString();
                    OBJ_LG_FNR_ROLE.ROLE_NAME = pROLE_NAME;
                    OBJ_LG_FNR_ROLE.ROLE_DESCRIP = pROLE_DESCRIP;
                    OBJ_LG_FNR_ROLE.AUTH_STATUS_ID = "U";
                    OBJ_LG_FNR_ROLE.LAST_ACTION = "ADD";
                    OBJ_LG_FNR_ROLE.MAKE_DT = System.DateTime.Now;
                    Obj_DBModelEntities.LG_FNR_ROLE.Add(OBJ_LG_FNR_ROLE);
                    Obj_DBModelEntities.SaveChanges();
                    LG_FNR_ROLE_MAP OBJ_LG_FNR_ROLE_MAP = new LG_FNR_ROLE_MAP();
                    Class_Conversion.LG_FNR_ROLE_REVERSE_CON(OBJ_LG_FNR_ROLE_MAP, OBJ_LG_FNR_ROLE);


                    #region Auth log
                    LG_AA_NFT_AUTH_LOG_MAP OBJ_LG_AA_NFT_AUTH_LOG_MAP = new LG_AA_NFT_AUTH_LOG_MAP();
                    FUNCTION_ID = OBJ_LG_AA_NFT_AUTH_LOG_MAP.FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "Role").Select(x => x.FUNCTION_ID).SingleOrDefault();
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_NAME = "LG_FNR_ROLE";
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_NM = "ROLE_ID";
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_VAL = OBJ_LG_FNR_ROLE.ROLE_ID.ToString();
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
                    LG_AA_NFT_AUTH_LOG_MAP.AddNftAuthLog(OBJ_LG_FNR_ROLE_MAP, OBJ_LG_AA_NFT_AUTH_LOG_MAP);
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
                            result = "Can't Add Role(Db)";
                        }
                    }

                    LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "AddRole",
                            "0000000000", dbEx.Message, inner, dbEx.StackTrace, psession_user, dateTime);
                    return result;
                }
                catch (Exception ex)
                {
                    ts.Dispose();
                    string dateTime = Convert.ToString(System.DateTime.Now);
                    string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);
                    LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "AddRole",
                                                  "0000000000", ex.Message, inner4, ex.StackTrace, psession_user, dateTime);
                    result = "Can't Add Role";
                    return result;
                }
            }
        }
        #endregion

        #region Update
        public static string UpdateRole(string pROLE_ID, string pROLE_NAME, string pROLE_DESCRIP, string psession_user)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                string result = string.Empty;
                LG_FNR_ROLE_MAP OBJ_LG_FNR_ROLE_MAP_OLD = new LG_FNR_ROLE_MAP();
                LG_FNR_ROLE_MAP OBJ_LG_FNR_ROLE_MAP_NEW = new LG_FNR_ROLE_MAP();
                try
                {
                    string format = OutgoingResponseFormat.GetFormat();
                    OutgoingResponseFormat.SetResponseFormat(format);

                    DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
                   
                    LG_FNR_ROLE OBJ_LG_FNR_ROLE_OLD = Obj_DBModelEntities.LG_FNR_ROLE.Where(r => r.ROLE_ID == pROLE_ID).SingleOrDefault();
                  
                    Class_Conversion.LG_FNR_ROLE_REVERSE_CON(OBJ_LG_FNR_ROLE_MAP_OLD, OBJ_LG_FNR_ROLE_OLD);

                    OBJ_LG_FNR_ROLE_OLD.AUTH_STATUS_ID = "U";
                    OBJ_LG_FNR_ROLE_OLD.LAST_ACTION = "EDT";
                    OBJ_LG_FNR_ROLE_OLD.LAST_UPDATE_DT = System.DateTime.Now;
                    Obj_DBModelEntities.SaveChanges();

                    Class_Conversion.LG_FNR_ROLE_REVERSE_CON(OBJ_LG_FNR_ROLE_MAP_NEW, OBJ_LG_FNR_ROLE_OLD);

                    OBJ_LG_FNR_ROLE_MAP_NEW.ROLE_NAME = pROLE_NAME;
                    OBJ_LG_FNR_ROLE_MAP_NEW.ROLE_DESCRIP = pROLE_DESCRIP;

                    #region Auth log
                    LG_AA_NFT_AUTH_LOG_MAP OBJ_LG_AA_NFT_AUTH_LOG_MAP = new LG_AA_NFT_AUTH_LOG_MAP();
                    FUNCTION_ID = OBJ_LG_AA_NFT_AUTH_LOG_MAP.FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "Role").Select(x => x.FUNCTION_ID).SingleOrDefault();
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_NAME = "LG_FNR_ROLE";
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_NM = "ROLE_ID";
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_VAL = OBJ_LG_FNR_ROLE_OLD.ROLE_ID;
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
                    LG_AA_NFT_AUTH_LOG_MAP.AddNftAuthLog(OBJ_LG_FNR_ROLE_MAP_OLD, OBJ_LG_FNR_ROLE_MAP_NEW, OBJ_LG_AA_NFT_AUTH_LOG_MAP);
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
                            result = "Can't Update Role(Db) ";
                        }
                    }
                    LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "UpdateRole",
                                                  "0000000000", dbEx.Message, inner, dbEx.StackTrace, psession_user, dateTime);

                    return result;
                }
                catch (Exception ex)
                {
                    ts.Dispose();
                    string dateTime = Convert.ToString(System.DateTime.Now);
                    string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);
                    LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "UpdateRole",
                                                  "0000000000", ex.Message, inner4, ex.StackTrace, psession_user, dateTime);
                    result = "Can't Update Role " + ex.Message;
                    return result;
                }
            }
        }
        #endregion

        #region Delete
        public static string DeleteRole(string pROLE_ID, string psession_user)
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            string result = string.Empty;
            try
            {
                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                LG_FNR_ROLE OBJ_LG_FNR_ROLE = (from role in Obj_DBModelEntities.LG_FNR_ROLE
                                               where !(from rold_def in Obj_DBModelEntities.LG_FNR_ROLE_DEFINE
                                                       select rold_def.ROLE_ID).Contains(role.ROLE_ID)
                                               && role.ROLE_ID == pROLE_ID
                                               select role).SingleOrDefault();
                if (OBJ_LG_FNR_ROLE != null)
                {
                    OBJ_LG_FNR_ROLE.AUTH_STATUS_ID = "U";
                    OBJ_LG_FNR_ROLE.LAST_ACTION = "DEL";
                    OBJ_LG_FNR_ROLE.LAST_UPDATE_DT = System.DateTime.Now;
                    Obj_DBModelEntities.SaveChanges();

                    #region Auth log
                    LG_AA_NFT_AUTH_LOG_MAP OBJ_LG_AA_NFT_AUTH_LOG_MAP = new LG_AA_NFT_AUTH_LOG_MAP();
                    FUNCTION_ID = OBJ_LG_AA_NFT_AUTH_LOG_MAP.FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "Role").Select(x => x.FUNCTION_ID).SingleOrDefault();
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_NAME = "LG_FNR_ROLE";
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_NM = "ROLE_ID";
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_VAL = OBJ_LG_FNR_ROLE.ROLE_ID;
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
                    LG_AA_NFT_AUTH_LOG_MAP.AddNftAuthLog(OBJ_LG_FNR_ROLE, OBJ_LG_AA_NFT_AUTH_LOG_MAP);
                    #endregion

                    result = "True";
                    return result;
                }
                else
                    return "Can't delete as this Role contains functions.";
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
                        result = "Can't Delete Role(Db). ";
                    }
                }

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "DeleteRole",
                       "0000000000", dbEx.Message, inner, dbEx.StackTrace, psession_user, dateTime);

                return result;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);
                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "DeleteRole",
                                              "0000000000", ex.Message, inner4, ex.StackTrace, psession_user, dateTime);
                result = "Can't Delete Role. " + ex.Message;
                return result;
            }
        }
        #endregion

        #region Fetch Single
        public static LG_FNR_ROLE_MAP GetRoleByRoleId(string pROLE_ID)
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            LG_FNR_ROLE_MAP OBJ_LG_FNR_ROLE_MAP = new LG_FNR_ROLE_MAP();

            try
            {
                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                OBJ_LG_FNR_ROLE_MAP = (from r in Obj_DBModelEntities.LG_FNR_ROLE
                                       where r.ROLE_ID == pROLE_ID
                                       select new LG_FNR_ROLE_MAP
                                       {
                                           ROLE_ID = r.ROLE_ID,
                                           ROLE_NAME = r.ROLE_NAME,
                                           ROLE_DESCRIP = r.ROLE_DESCRIP,
                                           AUTH_STATUS_ID = r.AUTH_STATUS_ID,
                                           LAST_ACTION = r.LAST_ACTION,
                                           LAST_UPDATE_DT = r.LAST_UPDATE_DT,
                                           MAKE_DT = r.MAKE_DT
                                       }).SingleOrDefault();
                return OBJ_LG_FNR_ROLE_MAP;
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
                        OBJ_LG_FNR_ROLE_MAP.ERROR = validationError.ErrorMessage;
                    }
                }
                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "GetRoleByRoleId",
                                              "0000000000", dbEx.Message, inner, dbEx.StackTrace, null, dateTime);

                return OBJ_LG_FNR_ROLE_MAP;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);
                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "GetRoleByRoleId",
                                              "0000000000", ex.Message, inner4, ex.StackTrace, null, dateTime);
                OBJ_LG_FNR_ROLE_MAP.ERROR = ex.Message;
                return OBJ_LG_FNR_ROLE_MAP;
            }
        }
        #endregion

        #region Fetch all
        public static IEnumerable<LG_FNR_ROLE_MAP> GetRoles()
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            List<LG_FNR_ROLE_MAP> LIST_LG_FNR_ROLE_MAP = null;
            LG_FNR_ROLE_MAP OBJ_LG_FNR_ROLE_MAP = new LG_FNR_ROLE_MAP();

            try
            {
                Obj_DBModelEntities.Configuration.LazyLoadingEnabled = false;
                Obj_DBModelEntities.Configuration.ProxyCreationEnabled = false;

                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);


                LIST_LG_FNR_ROLE_MAP = (from r in Obj_DBModelEntities.LG_FNR_ROLE
                                        where (r.ROLE_ID != null && r.AUTH_STATUS_ID == "A" && (r.LAST_ACTION != "DEL" ))
                                        orderby r.MAKE_DT descending
                                        select new LG_FNR_ROLE_MAP
                                        {
                                            ROLE_ID = r.ROLE_ID,
                                            ROLE_NAME = r.ROLE_NAME,
                                            ROLE_DESCRIP = r.ROLE_DESCRIP,
                                            AUTH_STATUS_ID = r.AUTH_STATUS_ID,
                                            LAST_ACTION = r.LAST_ACTION,
                                            LAST_UPDATE_DT = r.LAST_UPDATE_DT,
                                            MAKE_DT = r.MAKE_DT
                                        }).ToList();
                return LIST_LG_FNR_ROLE_MAP;
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
                        OBJ_LG_FNR_ROLE_MAP.ERROR = validationError.ErrorMessage;
                        LIST_LG_FNR_ROLE_MAP.Add(OBJ_LG_FNR_ROLE_MAP);
                    }
                }
                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "GetRoles",
                      "0000000000", dbEx.Message, inner, dbEx.StackTrace, null, dateTime);

                return LIST_LG_FNR_ROLE_MAP;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);
                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "GetRoles",
                                              "0000000000", ex.Message, inner4, ex.StackTrace, null, dateTime);
                OBJ_LG_FNR_ROLE_MAP.ERROR = ex.Message;
                LIST_LG_FNR_ROLE_MAP.Add(OBJ_LG_FNR_ROLE_MAP);
                return LIST_LG_FNR_ROLE_MAP;
            }
        }
        #endregion

        #region Validate
        #endregion

        #endregion
    }
}
