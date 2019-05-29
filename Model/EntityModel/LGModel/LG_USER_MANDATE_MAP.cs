using Model.EDMX;
using Model.EntityModel.Common;
using Model.Mapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Model.EntityModel.LGModel
{
    public class LG_USER_MANDATE_MAP
    {
        public static string FUNCTION_ID = "010102003";

        #region Activate User

        public static string ActivateUser(string pUSER_ID, string pAPPLICATION_ID, string PMAKE_BY)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                LG_USER_SETUP_PROFILE_MAP OBJ_LG_USER_SETUP_PROFILE_MAP_NEW = new LG_USER_SETUP_PROFILE_MAP();
                LG_USER_SETUP_PROFILE_MAP OBJ_LG_USER_SETUP_PROFILE_MAP_OLD = new LG_USER_SETUP_PROFILE_MAP();
                string result = string.Empty;

                try
                {
                    string format = OutgoingResponseFormat.GetFormat();
                    OutgoingResponseFormat.SetResponseFormat(format);

                    DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);

                    string FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "UserStatus").Select(x => x.FUNCTION_ID).SingleOrDefault();

                    LG_USER_SETUP_PROFILE OBJ_LG_USER_SETUP_PROFILE_OLD = Obj_DBModelEntities.LG_USER_SETUP_PROFILE.Where(m => m.USER_ID == pUSER_ID).SingleOrDefault();
                    Class_Conversion.LG_USER_SETUP_PROFILE_REVERSE_CON(OBJ_LG_USER_SETUP_PROFILE_MAP_OLD, OBJ_LG_USER_SETUP_PROFILE_OLD);

                    if (OBJ_LG_USER_SETUP_PROFILE_OLD == null)
                    {
                        return "User is not found in the system.";
                    }

                    OBJ_LG_USER_SETUP_PROFILE_OLD.LAST_UPDATE_DT = System.DateTime.Now;
                    OBJ_LG_USER_SETUP_PROFILE_OLD.AUTH_STATUS_ID = "U";
                    OBJ_LG_USER_SETUP_PROFILE_OLD.LAST_ACTION = "EDT";
                    Obj_DBModelEntities.SaveChanges();
                    Class_Conversion.LG_USER_SETUP_PROFILE_REVERSE_CON(OBJ_LG_USER_SETUP_PROFILE_MAP_NEW, OBJ_LG_USER_SETUP_PROFILE_OLD);

                    //if (OBJ_LG_USER_SETUP_PROFILE_MAP_NEW.FIRST_LOGIN_FLAG == 0) //salekin commented
                    //{
                    //    OBJ_LG_USER_SETUP_PROFILE_MAP_NEW.FIRST_LOGIN_FLAG = 1;
                    //}
                    OBJ_LG_USER_SETUP_PROFILE_MAP_NEW.ACTIVE_FLAG_INACTV_USER = 1;

                    #region Auth log

                    LG_AA_NFT_AUTH_LOG_MAP OBJ_LG_AA_NFT_AUTH_LOG_MAP = new LG_AA_NFT_AUTH_LOG_MAP();
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "UserStatus").Select(x => x.FUNCTION_ID).SingleOrDefault();
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_NAME = "LG_USER_SETUP_PROFILE";
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_NM = "USER_ID";
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_VAL = OBJ_LG_USER_SETUP_PROFILE_OLD.USER_ID;
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
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.MAKE_BY = PMAKE_BY;
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.MAKE_DT = DateTime.Now;
                    LG_AA_NFT_AUTH_LOG_MAP.AddNftAuthLog(OBJ_LG_USER_SETUP_PROFILE_MAP_OLD, OBJ_LG_USER_SETUP_PROFILE_MAP_NEW, OBJ_LG_AA_NFT_AUTH_LOG_MAP);

                    #endregion Auth log

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
                            result = "Can't Activate User(Db).";
                        }
                    }

                    LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "ActivateUser",
                           "0000000000", dbEx.Message, inner, dbEx.StackTrace, PMAKE_BY, dateTime);

                    return result;
                }
                catch (Exception ex)
                {
                    ts.Dispose();
                    string dateTime = Convert.ToString(System.DateTime.Now);
                    string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);
                    LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "ActivateUser",
                           "0000000000", ex.Message, inner4, ex.StackTrace, PMAKE_BY, dateTime);

                    result = "Can't Activate User.";
                    return result;
                }
            }
        }

        #endregion Activate User

        #region InActivate User

        public static string InActivateUser(string pUSER_ID, string pAPPLICATION_ID, string PMAKE_BY)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                LG_USER_SETUP_PROFILE_MAP OBJ_LG_USER_SETUP_PROFILE_MAP_NEW = new LG_USER_SETUP_PROFILE_MAP();
                LG_USER_SETUP_PROFILE_MAP OBJ_LG_USER_SETUP_PROFILE_MAP_OLD = new LG_USER_SETUP_PROFILE_MAP();
                string result = string.Empty;

                try
                {
                    string format = OutgoingResponseFormat.GetFormat();
                    OutgoingResponseFormat.SetResponseFormat(format);

                    DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);

                    string FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "UserStatus").Select(x => x.FUNCTION_ID).SingleOrDefault();

                    LG_USER_SETUP_PROFILE OBJ_LG_USER_SETUP_PROFILE_OLD = Obj_DBModelEntities.LG_USER_SETUP_PROFILE.Where(m => m.USER_ID == pUSER_ID).SingleOrDefault();
                    Class_Conversion.LG_USER_SETUP_PROFILE_REVERSE_CON(OBJ_LG_USER_SETUP_PROFILE_MAP_OLD, OBJ_LG_USER_SETUP_PROFILE_OLD);

                    if (OBJ_LG_USER_SETUP_PROFILE_OLD == null)
                    {
                        return "User is not found in the system.";
                    }

                    OBJ_LG_USER_SETUP_PROFILE_OLD.LAST_UPDATE_DT = System.DateTime.Now;
                    OBJ_LG_USER_SETUP_PROFILE_OLD.AUTH_STATUS_ID = "U";
                    OBJ_LG_USER_SETUP_PROFILE_OLD.LAST_ACTION = "EDT";
                    Obj_DBModelEntities.SaveChanges();
                    Class_Conversion.LG_USER_SETUP_PROFILE_REVERSE_CON(OBJ_LG_USER_SETUP_PROFILE_MAP_NEW, OBJ_LG_USER_SETUP_PROFILE_OLD);

                    //if (OBJ_LG_USER_SETUP_PROFILE_MAP_NEW.FIRST_LOGIN_FLAG == 1) //salekin commented
                    //{
                    //    OBJ_LG_USER_SETUP_PROFILE_MAP_NEW.FIRST_LOGIN_FLAG = 0;
                    //}
                    OBJ_LG_USER_SETUP_PROFILE_MAP_NEW.ACTIVE_FLAG_INACTV_USER = 0;

                    #region Auth log

                    LG_AA_NFT_AUTH_LOG_MAP OBJ_LG_AA_NFT_AUTH_LOG_MAP = new LG_AA_NFT_AUTH_LOG_MAP();
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "UserStatus").Select(x => x.FUNCTION_ID).SingleOrDefault();
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_NAME = "LG_USER_SETUP_PROFILE";
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_NM = "USER_ID";
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_VAL = OBJ_LG_USER_SETUP_PROFILE_OLD.USER_ID;
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
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.MAKE_BY = PMAKE_BY;
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.MAKE_DT = DateTime.Now;
                    LG_AA_NFT_AUTH_LOG_MAP.AddNftAuthLog(OBJ_LG_USER_SETUP_PROFILE_MAP_OLD, OBJ_LG_USER_SETUP_PROFILE_MAP_NEW, OBJ_LG_AA_NFT_AUTH_LOG_MAP);

                    #endregion Auth log

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
                            result = "Can't INActivate User(Db).";
                        }
                    }

                    LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "InActivateUser",
                           "0000000000", dbEx.Message, inner, dbEx.StackTrace, PMAKE_BY, dateTime);

                    return result;
                }
                catch (Exception ex)
                {
                    ts.Dispose();
                    string dateTime = Convert.ToString(System.DateTime.Now);
                    string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);
                    LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "InActivateUser",
                           "0000000000", ex.Message, inner4, ex.StackTrace, PMAKE_BY, dateTime);

                    result = "Can't Inactivate User.";
                    return result;
                }
            }
        }

        #endregion InActivate User

        #region Lock User

        public static string LockUser(string pUSER_ID, string pAPPLICATION_ID, string PMAKE_BY)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                LG_USER_SETUP_PROFILE_MAP OBJ_LG_USER_SETUP_PROFILE_MAP_NEW = new LG_USER_SETUP_PROFILE_MAP();
                LG_USER_SETUP_PROFILE_MAP OBJ_LG_USER_SETUP_PROFILE_MAP_OLD = new LG_USER_SETUP_PROFILE_MAP();
                string result = string.Empty;

                try
                {
                    string format = OutgoingResponseFormat.GetFormat();
                    OutgoingResponseFormat.SetResponseFormat(format);

                    DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);

                    string FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "UserStatus").Select(x => x.FUNCTION_ID).SingleOrDefault();

                    LG_USER_SETUP_PROFILE OBJ_LG_USER_SETUP_PROFILE_OLD = Obj_DBModelEntities.LG_USER_SETUP_PROFILE.Where(m => m.USER_ID == pUSER_ID).SingleOrDefault();
                    Class_Conversion.LG_USER_SETUP_PROFILE_REVERSE_CON(OBJ_LG_USER_SETUP_PROFILE_MAP_OLD, OBJ_LG_USER_SETUP_PROFILE_OLD);

                    if (OBJ_LG_USER_SETUP_PROFILE_OLD == null)
                    {
                        return "User is not found in the system.";
                    }

                    OBJ_LG_USER_SETUP_PROFILE_OLD.LAST_UPDATE_DT = System.DateTime.Now;
                    OBJ_LG_USER_SETUP_PROFILE_OLD.AUTH_STATUS_ID = "U";
                    OBJ_LG_USER_SETUP_PROFILE_OLD.LAST_ACTION = "EDT";
                    Obj_DBModelEntities.SaveChanges();
                    Class_Conversion.LG_USER_SETUP_PROFILE_REVERSE_CON(OBJ_LG_USER_SETUP_PROFILE_MAP_NEW, OBJ_LG_USER_SETUP_PROFILE_OLD);

                    //if (OBJ_LG_USER_SETUP_PROFILE_MAP_NEW.FIRST_LOGIN_FLAG == 0)
                    //{
                    //    OBJ_LG_USER_SETUP_PROFILE_MAP_NEW.FIRST_LOGIN_FLAG = 1;
                    //}
                    OBJ_LG_USER_SETUP_PROFILE_MAP_NEW.USER_ID_LOCK_WRNG_ATM = 1;

                    #region Auth log

                    LG_AA_NFT_AUTH_LOG_MAP OBJ_LG_AA_NFT_AUTH_LOG_MAP = new LG_AA_NFT_AUTH_LOG_MAP();
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "UserStatus").Select(x => x.FUNCTION_ID).SingleOrDefault();
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_NAME = "LG_USER_SETUP_PROFILE";
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_NM = "USER_ID";
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_VAL = OBJ_LG_USER_SETUP_PROFILE_OLD.USER_ID;
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
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.MAKE_BY = PMAKE_BY;
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.MAKE_DT = DateTime.Now;
                    LG_AA_NFT_AUTH_LOG_MAP.AddNftAuthLog(OBJ_LG_USER_SETUP_PROFILE_MAP_OLD, OBJ_LG_USER_SETUP_PROFILE_MAP_NEW, OBJ_LG_AA_NFT_AUTH_LOG_MAP);

                    #endregion Auth log

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
                            result = "Can't Activate User(Db).";
                        }
                    }

                    LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "LockUser",
                           "0000000000", dbEx.Message, inner, dbEx.StackTrace, PMAKE_BY, dateTime);

                    return result;
                }
                catch (Exception ex)
                {
                    ts.Dispose();
                    string dateTime = Convert.ToString(System.DateTime.Now);
                    string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);
                    LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "lockUser",
                           "0000000000", ex.Message, inner4, ex.StackTrace, PMAKE_BY, dateTime);

                    result = "Can't Activate User.";
                    return result;
                }
            }
        }

        #endregion Lock User

        #region UnLock User

        public static string UnLockUser(string pUSER_ID, string pAPPLICATION_ID, string PMAKE_BY)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                LG_USER_SETUP_PROFILE_MAP OBJ_LG_USER_SETUP_PROFILE_MAP_NEW = new LG_USER_SETUP_PROFILE_MAP();
                LG_USER_SETUP_PROFILE_MAP OBJ_LG_USER_SETUP_PROFILE_MAP_OLD = new LG_USER_SETUP_PROFILE_MAP();
                string result = string.Empty;

                try
                {
                    string format = OutgoingResponseFormat.GetFormat();
                    OutgoingResponseFormat.SetResponseFormat(format);

                    DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);

                    string FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "UserStatus").Select(x => x.FUNCTION_ID).SingleOrDefault();

                    LG_USER_SETUP_PROFILE OBJ_LG_USER_SETUP_PROFILE_OLD = Obj_DBModelEntities.LG_USER_SETUP_PROFILE.Where(m => m.USER_ID == pUSER_ID).SingleOrDefault();
                    Class_Conversion.LG_USER_SETUP_PROFILE_REVERSE_CON(OBJ_LG_USER_SETUP_PROFILE_MAP_OLD, OBJ_LG_USER_SETUP_PROFILE_OLD);

                    if (OBJ_LG_USER_SETUP_PROFILE_OLD == null)
                    {
                        return "User is not found in the system.";
                    }

                    OBJ_LG_USER_SETUP_PROFILE_OLD.LAST_UPDATE_DT = System.DateTime.Now;
                    OBJ_LG_USER_SETUP_PROFILE_OLD.AUTH_STATUS_ID = "U";
                    OBJ_LG_USER_SETUP_PROFILE_OLD.LAST_ACTION = "EDT";
                    Obj_DBModelEntities.SaveChanges();
                    Class_Conversion.LG_USER_SETUP_PROFILE_REVERSE_CON(OBJ_LG_USER_SETUP_PROFILE_MAP_NEW, OBJ_LG_USER_SETUP_PROFILE_OLD);

                    //if (OBJ_LG_USER_SETUP_PROFILE_MAP_NEW.FIRST_LOGIN_FLAG == 0)
                    //{
                    //    OBJ_LG_USER_SETUP_PROFILE_MAP_NEW.FIRST_LOGIN_FLAG = 1;
                    //}
                    OBJ_LG_USER_SETUP_PROFILE_MAP_NEW.USER_ID_LOCK_WRNG_ATM = 0;

                    #region Auth log

                    LG_AA_NFT_AUTH_LOG_MAP OBJ_LG_AA_NFT_AUTH_LOG_MAP = new LG_AA_NFT_AUTH_LOG_MAP();
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "UserStatus").Select(x => x.FUNCTION_ID).SingleOrDefault();
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_NAME = "LG_USER_SETUP_PROFILE";
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_NM = "USER_ID";
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_VAL = OBJ_LG_USER_SETUP_PROFILE_OLD.USER_ID;
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
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.MAKE_BY = PMAKE_BY;
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.MAKE_DT = DateTime.Now;
                    LG_AA_NFT_AUTH_LOG_MAP.AddNftAuthLog(OBJ_LG_USER_SETUP_PROFILE_MAP_OLD, OBJ_LG_USER_SETUP_PROFILE_MAP_NEW, OBJ_LG_AA_NFT_AUTH_LOG_MAP);

                    #endregion Auth log

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
                            result = "Can't Activate User(Db).";
                        }
                    }

                    LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "unLockUser",
                           "0000000000", dbEx.Message, inner, dbEx.StackTrace, PMAKE_BY, dateTime);

                    return result;
                }
                catch (Exception ex)
                {
                    ts.Dispose();
                    string dateTime = Convert.ToString(System.DateTime.Now);
                    string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);
                    LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "unlockUser",
                           "0000000000", ex.Message, inner4, ex.StackTrace, PMAKE_BY, dateTime);

                    result = "Can't Activate User.";
                    return result;
                }
            }
        }

        #endregion UnLock User

        #region Deactivate User

        public static string DeactivateUser(string pUSER_ID, string pAPPLICATION_ID)
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            LG_USER_SETUP_PROFILE OBJ_LG_USER_SETUP_PROFILE = new LG_USER_SETUP_PROFILE();
            string FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "UserStatus").Select(x => x.FUNCTION_ID).SingleOrDefault();
            string result = string.Empty;

            try
            {
                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                OBJ_LG_USER_SETUP_PROFILE = Obj_DBModelEntities.LG_USER_SETUP_PROFILE
                                             .Where(a => a.USER_ID == pUSER_ID).SingleOrDefault();

                if (OBJ_LG_USER_SETUP_PROFILE == null)
                {
                    return "User is not found in the system.";
                }

                Obj_DBModelEntities.LG_USER_SETUP_PROFILE.Attach(OBJ_LG_USER_SETUP_PROFILE);

                //Don't know which flag to change
                //OBJ_LG_USER_SETUP_PROFILE.ACTIVE_FLAG = 1;

                Obj_DBModelEntities.SaveChanges();

                #region Auth log

                LG_AA_NFT_AUTH_LOG_MAP OBJ_LG_AA_NFT_AUTH_LOG_MAP = new LG_AA_NFT_AUTH_LOG_MAP();
                OBJ_LG_AA_NFT_AUTH_LOG_MAP.FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "Mandate User").Select(x => x.FUNCTION_ID).SingleOrDefault();
                OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_NAME = "LG_USER_SETUP_PROFILE";
                OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_NM = "USER_ID";
                OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_VAL = OBJ_LG_USER_SETUP_PROFILE.USER_ID;
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
                OBJ_LG_AA_NFT_AUTH_LOG_MAP.MAKE_BY = "salekin";
                OBJ_LG_AA_NFT_AUTH_LOG_MAP.MAKE_DT = DateTime.Now;
                LG_AA_NFT_AUTH_LOG_MAP.AddNftAuthLog(OBJ_LG_USER_SETUP_PROFILE, OBJ_LG_AA_NFT_AUTH_LOG_MAP);

                #endregion Auth log

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
                        result = "Can't Deactivate User(Db).";
                    }
                }

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "DeactivateUser",
      "0000000000", dbEx.Message, inner, dbEx.StackTrace, null, dateTime);

                return result;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "DeactivateUser",
                     "0000000000", ex.Message, inner4, ex.StackTrace, null, dateTime);

                result = "Can't Deactivate User";
                return result;
            }
        }

        #endregion Deactivate User

        #region Fetch Single

        public static LG_USER_SETUP_PROFILE_MAP GetUserSetupInfoByUserName(string puser_name)
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            LG_USER_SETUP_PROFILE_MAP OBJ_LG_USER_SETUP_PROFILE = new LG_USER_SETUP_PROFILE_MAP();

            try
            {
                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                OBJ_LG_USER_SETUP_PROFILE = (from m in Obj_DBModelEntities.LG_USER_SETUP_PROFILE

                                             join s in Obj_DBModelEntities.LG_USER_CLASSIFACTION
                                                           on m.USER_CLASSIFICATION_ID equals s.CLASSIFICATION_ID
                                             join a in Obj_DBModelEntities.LG_USER_AREA
                                             on m.USER_AREA_ID equals a.AREA_ID
                                             join c in Obj_DBModelEntities.LG_SYS_BRANCH_HOME_BANK
                                             on m.BRANCH_ID equals c.BRANCH_ID
                                             join d in Obj_DBModelEntities.LG_AA_AUTHENTICATION_TYPE
                                             on m.AUTHENTICATION_ID equals d.AUTHENTICATION_ID
                                             where m.USER_ID != null && m.USER_NM == puser_name
                                             select new LG_USER_SETUP_PROFILE_MAP()
                                             {
                                                 USER_ID = m.USER_ID,
                                                 USER_CLASSIFICATION_ID = m.USER_CLASSIFICATION_ID,
                                                 CLASSIFICATION_NAME = s.CLASSIFICATION_NAME,
                                                 USER_AREA_ID = m.USER_AREA_ID,
                                                 AREA_NAME = a.AREA_NAME,
                                                 USER_NAME = m.USER_NM,
                                                 USER_DESCRIPTION = m.USER_DESCRIP,
                                                 USER_AREA_ID_VALUE = m.USER_AREA_ID_VALUE,
                                                 BRANCH_ID = m.BRANCH_ID,
                                                 BRANCH_NAME = c.BRANCH_NM,
                                                 ACC_NO = m.ACC_NO,
                                                 FATHERS_NAME = m.FATHERS_NM,
                                                 MOTHERS_NAME = m.MOTHERS_NM,
                                                 DOB = m.DOB,
                                                 MAIL_ADDRESS = m.MAIL_ADDRESS,
                                                 MOB_NO = m.MOB_NO,
                                                 AUTHENTICATION_ID = m.AUTHENTICATION_ID,
                                                 AUTHENTICATION_NAME = d.AUTHENTICATION_NAME,
                                                 TERMINAL_IP = m.TERMINAL_IP,
                                                 // WORKING_HOUR = (m.WORKING_HOUR).ToString(),
                                                 START_TIME = m.START_TIME,
                                                 END_TIME = m.END_TIME,

                                                 //  MAKE_DT = m.MAKE_DT
                                             }).SingleOrDefault();
                return OBJ_LG_USER_SETUP_PROFILE;
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
                        OBJ_LG_USER_SETUP_PROFILE.ERROR = validationError.ErrorMessage;
                    }
                }

                LG_ERROR_LOG_MAP.Add_Error_Log(null, dbEx.Source, "ERR_APP_TYPE", "GetUserSetupInfoByUserName",
       "0000000000", dbEx.Message, inner, dbEx.StackTrace, "UserInfoFor:" + puser_name, dateTime);

                return OBJ_LG_USER_SETUP_PROFILE;
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

                LG_ERROR_LOG_MAP.Add_Error_Log(null, ex.Source, "ERR_APP_TYPE", "UnLockUser",
                       "0000000000", ex.Message, inner4, ex.StackTrace, "UserInfoFor(Name):" + puser_name, dateTime);

                OBJ_LG_USER_SETUP_PROFILE.ERROR = ex.Message;
                return OBJ_LG_USER_SETUP_PROFILE;
            }
        }

        public static LG_USER_SETUP_PROFILE_MAP GetUserSetupInfoByUserAccountNo(string puser_account_no)
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            LG_USER_SETUP_PROFILE_MAP OBJ_LG_USER_SETUP_PROFILE = new LG_USER_SETUP_PROFILE_MAP();

            try
            {
                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                OBJ_LG_USER_SETUP_PROFILE = (from m in Obj_DBModelEntities.LG_USER_SETUP_PROFILE

                                             join s in Obj_DBModelEntities.LG_USER_CLASSIFACTION
                                                           on m.USER_CLASSIFICATION_ID equals s.CLASSIFICATION_ID
                                             join a in Obj_DBModelEntities.LG_USER_AREA
                                             on m.USER_AREA_ID equals a.AREA_ID
                                             join c in Obj_DBModelEntities.LG_SYS_BRANCH_HOME_BANK
                                             on m.BRANCH_ID equals c.BRANCH_ID
                                             join d in Obj_DBModelEntities.LG_AA_AUTHENTICATION_TYPE
                                             on m.AUTHENTICATION_ID equals d.AUTHENTICATION_ID
                                             where m.USER_ID != null && m.ACC_NO == puser_account_no
                                             select new LG_USER_SETUP_PROFILE_MAP()
                                             {
                                                 USER_ID = m.USER_ID,
                                                 USER_CLASSIFICATION_ID = m.USER_CLASSIFICATION_ID,
                                                 CLASSIFICATION_NAME = s.CLASSIFICATION_NAME,
                                                 USER_AREA_ID = m.USER_AREA_ID,
                                                 AREA_NAME = a.AREA_NAME,
                                                 USER_NAME = m.USER_NM,
                                                 USER_DESCRIPTION = m.USER_DESCRIP,
                                                 USER_AREA_ID_VALUE = m.USER_AREA_ID_VALUE,
                                                 BRANCH_ID = m.BRANCH_ID,
                                                 BRANCH_NAME = c.BRANCH_NM,
                                                 ACC_NO = m.ACC_NO,
                                                 FATHERS_NAME = m.FATHERS_NM,
                                                 MOTHERS_NAME = m.MOTHERS_NM,
                                                 DOB = m.DOB,
                                                 MAIL_ADDRESS = m.MAIL_ADDRESS,
                                                 MOB_NO = m.MOB_NO,
                                                 AUTHENTICATION_ID = m.AUTHENTICATION_ID,
                                                 AUTHENTICATION_NAME = d.AUTHENTICATION_NAME,
                                                 TERMINAL_IP = m.TERMINAL_IP,
                                                 // WORKING_HOUR = (m.WORKING_HOUR).ToString(),
                                                 START_TIME = m.START_TIME,
                                                 END_TIME = m.END_TIME,

                                                 //  MAKE_DT = m.MAKE_DT
                                             }).SingleOrDefault();
                return OBJ_LG_USER_SETUP_PROFILE;
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
                        OBJ_LG_USER_SETUP_PROFILE.ERROR = validationError.ErrorMessage;
                    }
                }

                LG_ERROR_LOG_MAP.Add_Error_Log(null, dbEx.Source, "ERR_APP_TYPE", "GetUserSetupInfoByUserAccountNo",
       "0000000000", dbEx.Message, inner, dbEx.StackTrace, "UserInfoFor(Acc_No):" + puser_account_no, dateTime);

                return OBJ_LG_USER_SETUP_PROFILE;
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

                LG_ERROR_LOG_MAP.Add_Error_Log(null, ex.Source, "ERR_APP_TYPE", "GetUserSetupInfoByUserAccountNo",
                       "0000000000", ex.Message, inner4, ex.StackTrace, "UserInfoFor(Acc_No):" + puser_account_no, dateTime);

                OBJ_LG_USER_SETUP_PROFILE.ERROR = ex.Message;
                return OBJ_LG_USER_SETUP_PROFILE;
            }
        }

        //public static LG_USER_SETUP_PROFILE_MAP GetUserSetupInfoByUserId(string puser_id)
        //{
        //    DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
        //    LG_USER_SETUP_PROFILE_MAP OBJ_LG_USER_SETUP_PROFILE = new LG_USER_SETUP_PROFILE_MAP();

        //    try
        //    {
        //        string format = OutgoingResponseFormat.GetFormat();
        //        OutgoingResponseFormat.SetResponseFormat(format);

        //        OBJ_LG_USER_SETUP_PROFILE = (from m in Obj_DBModelEntities.LG_USER_SETUP_PROFILE

        //                                     join s in Obj_DBModelEntities.LG_USER_CLASSIFACTION
        //                                                   on m.CLASSIFICATION_ID equals s.CLASSIFICATION_ID
        //                                     join a in Obj_DBModelEntities.LG_USER_AREA
        //                                     on m.AREA_ID equals a.AREA_ID
        //                                     join c in Obj_DBModelEntities.LG_SYS_BRANCH_HOME_BANK
        //                                     on m.BRANCH_ID equals c.BRANCH_ID
        //                                     join d in Obj_DBModelEntities.LG_AA_AUTHENTICATION_TYPE
        //                                     on m.AUTHENTICATION_ID equals d.AUTHENTICATION_ID
        //                                     where m.USER_ID != null && m.USER_ID == puser_id
        //                                     select new LG_USER_SETUP_PROFILE_MAP()
        //                                     {
        //                                         USER_ID = m.USER_ID,
        //                                         CLASSIFICATION_ID = m.CLASSIFICATION_ID,
        //                                         CLASSIFICATION_NAME = s.CLASSIFICATION_NAME,
        //                                         AREA_ID = m.AREA_ID,
        //                                         AREA_NAME = a.AREA_NAME,
        //                                         USER_NAME = m.USER_NAME,
        //                                         USER_DESCRIPTION = m.USER_DESCRIPTION,
        //                                         EMPLOYEE_ID = m.EMPLOYEE_ID,
        //                                         BRANCH_ID = m.BRANCH_ID,
        //                                         BRANCH_NAME = c.BRANCH_NM,
        //                                         CUSTOMER_ID = m.CUSTOMER_ID,
        //                                         AGENT_ID = m.AGENT_ID,
        //                                         ACC_NO = m.ACC_NO,
        //                                         FATHERS_NAME = m.FATHERS_NAME,
        //                                         MOTHERS_NAME = m.MOTHERS_NAME,
        //                                         DOB = m.DOB,
        //                                         MAIL_ADDRESS = m.MAIL_ADDRESS,
        //                                         MOB_NO = m.MOB_NO,
        //                                         AUTHENTICATION_ID = m.AUTHENTICATION_ID,
        //                                         AUTHENTICATION_NAME = d.AUTHENTICATION_NAME,
        //                                         TERMINAL_IP = m.TERMINAL_IP,
        //                                         WORKING_HOUR = m.WORKING_HOUR,
        //                                         START_TIME = m.START_TIME,
        //                                         END_TIME = m.END_TIME,
        //                                         ACTIVE_FLAG_FOR_INACTIVE_USER = m.ACTIVE_FLAG_INACTV_USER,
        //                                         LOCK_FLAG_FOR_WRONG_ATTM = m.USER_ID_LOCK_WRNG_ATM
        //                                         //  MAKE_DT = m.MAKE_DT
        //                                     }).SingleOrDefault();
        //        return OBJ_LG_USER_SETUP_PROFILE;
        //    }

        //    catch (DbEntityValidationException dbEx)
        //    {
        //        foreach (var validationErrors in dbEx.EntityValidationErrors)
        //        {
        //            foreach (var validationError in validationErrors.ValidationErrors)
        //            {
        //                Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
        //                OBJ_LG_USER_SETUP_PROFILE.ERROR = validationError.ErrorMessage;
        //            }
        //        }
        //        return OBJ_LG_USER_SETUP_PROFILE;
        //    }
        //    catch (Exception ex)
        //    {
        //        OBJ_LG_USER_SETUP_PROFILE.ERROR = ex.Message;
        //        return OBJ_LG_USER_SETUP_PROFILE;
        //    }
        //}

        #endregion Fetch Single
    }
}