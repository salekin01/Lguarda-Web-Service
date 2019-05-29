using Model.EDMX;
using Model.EntityModel.Common;
using Model.Mapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Model.EntityModel.LGModel
{
    [DataContract]
    public class LG_CRD_PASSWORD_POLICY_MAP
    {

        #region Properties

        [DataMember]
        public string SL_NO { get; set; }
        [DataMember]
        public string PASS_MAX_LENGTH { get; set; }
        [DataMember]
        public string PASS_MIN_LENGTH { get; set; }
        [DataMember]
        public short NUMERIC_CHAR_MIN { get; set; }
        [DataMember]
        public string PASS_HIS_PERIOD { get; set; }
        [DataMember]
        public short PASS_REUSE_MAX { get; set; }
        [DataMember]
        public short FAILED_LOGIN_ATTEMT { get; set; }
        [DataMember]
        public short PASS_EXP_PERIOD { get; set; }
        [DataMember]
        public short MIN_CAPS_LETTER { get; set; }
        [DataMember]
        public short MIN_SMALL_LETTER { get; set; }
        [DataMember]
        public short MIN_NUMERIC_CHAR { get; set; }
        [DataMember]
        public string MIN_CONS_USE_PASS { get; set; }
        [DataMember]
        public string PASS_REPEAT { get; set; }
        [DataMember]
        public int PASS_CHANGED_EXPIRY { get; set; }
        [DataMember]
        public short PASS_EXP_ALERT { get; set; }
        [DataMember]
        public bool PASS_CHANGED_EXPIRY_B { get; set; }
        [DataMember]
        public bool PASS_CHANG_AT_FIRST_LOGIN_B { get; set; }
        [DataMember]
        public bool PASS_CHANGE_BY_ADMIN_B { get; set; }
        [DataMember]
        public bool PASS_AUTO_CREATION_B { get; set; }

        [DataMember]
        public short PASS_CHANG_AT_FIRST_LOGIN { get; set; }
        [DataMember]
        public short PASS_CHANGE_BY_ADMIN { get; set; }
        [DataMember]
        public short PASS_AUTO_CREATION { get; set; }

        [DataMember]
        public string AUTH_STATUS_ID { get; set; }
        [DataMember]
        public string LAST_ACTION { get; set; }
        [DataMember]
        public Nullable<System.DateTime> LAST_UPDATE_DT { get; set; }
        [DataMember]
        public Nullable<System.DateTime> MAKE_DT { get; set; }
        [DataMember]
        public string ERROR { get; set; }
        [DataMember]
        public string APPLICATION_ID { get; set; }

        public static string FUNCTION_ID;
        #endregion

        #region Events


        #region Add New
        public static string AddPasswordPolicy(string p_user_id, string pPASS_MAX_LENGTH, string pPASS_MIN_LENGTH, string pNUMERIC_CHAR_MIN, string pPASS_HIS_PERIOD, string pMIN_CAPS_LETTER, string pMIN_SMALL_LETTER, string pMIN_NUMERIC_CHAR, string pMIN_CONS_USE_PASS, string pPASS_REPEAT, string pPASS_CHANGED_EXPIRY, string pPASS_AUTO_CREATION, string pPASS_CHANGE_BY_ADMIN, string pPASS_CHANGE_AT_FIRST_LOGIN, string pAPPLICATION_ID, string pPASS_REUSE_MAX, string pFAILED_LOGIN_ATTEMT, string pPASS_EXP_PERIOD, string pPASS_EXP_ALERT)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
                LG_CRD_PASSWORD_POLICY OBJ_LG_CRD_PASSWORD_POLICY = new LG_CRD_PASSWORD_POLICY();
                string result = string.Empty;
                try
                {
                    string format = OutgoingResponseFormat.GetFormat();
                    OutgoingResponseFormat.SetResponseFormat(format);

                    OBJ_LG_CRD_PASSWORD_POLICY = Obj_DBModelEntities.LG_CRD_PASSWORD_POLICY
                                                 .Where(a => a.APPLICATION_ID == pAPPLICATION_ID).SingleOrDefault();
                    if (OBJ_LG_CRD_PASSWORD_POLICY != null)
                    {
                        return "Password policy already exists for this application.";
                    }
                    int id = (Obj_DBModelEntities.LG_CRD_PASSWORD_POLICY
                                               .Select(i => i.SL_NO).Cast<int?>().Max() ?? 0) + 1;


                    OBJ_LG_CRD_PASSWORD_POLICY = new LG_CRD_PASSWORD_POLICY();
                    OBJ_LG_CRD_PASSWORD_POLICY.SL_NO = id.ToString();
                    OBJ_LG_CRD_PASSWORD_POLICY.APPLICATION_ID = pAPPLICATION_ID;
                    OBJ_LG_CRD_PASSWORD_POLICY.PASS_MAX_LENGTH = pPASS_MAX_LENGTH;
                    OBJ_LG_CRD_PASSWORD_POLICY.PASS_MIN_LENGTH = pPASS_MIN_LENGTH;
                    OBJ_LG_CRD_PASSWORD_POLICY.NUMERIC_CHAR_MIN = Convert.ToInt16(pNUMERIC_CHAR_MIN);
                    OBJ_LG_CRD_PASSWORD_POLICY.PASS_HIS_PERIOD = pPASS_HIS_PERIOD;
                    OBJ_LG_CRD_PASSWORD_POLICY.MIN_CAPS_LETTER = Convert.ToInt16(pMIN_CAPS_LETTER);
                    OBJ_LG_CRD_PASSWORD_POLICY.MIN_SMALL_LETTER = Convert.ToInt16(pMIN_SMALL_LETTER);
                    OBJ_LG_CRD_PASSWORD_POLICY.MIN_NUMERIC_CHAR = Convert.ToInt16(pMIN_NUMERIC_CHAR);
                    OBJ_LG_CRD_PASSWORD_POLICY.MIN_CONS_USE_PASS = pMIN_CONS_USE_PASS;
                    OBJ_LG_CRD_PASSWORD_POLICY.PASS_REPEAT = pPASS_REPEAT;
                    OBJ_LG_CRD_PASSWORD_POLICY.PASS_CHANGED_EXPIRY = Convert.ToInt16(pPASS_CHANGED_EXPIRY);
                    OBJ_LG_CRD_PASSWORD_POLICY.PASS_REUSE_MAX = Convert.ToInt16(pPASS_REUSE_MAX);
                    OBJ_LG_CRD_PASSWORD_POLICY.FAILED_LOGIN_ATTEMT = Convert.ToInt16(pFAILED_LOGIN_ATTEMT);
                    OBJ_LG_CRD_PASSWORD_POLICY.PASS_EXP_PERIOD = Convert.ToInt16(pPASS_EXP_PERIOD);
                    OBJ_LG_CRD_PASSWORD_POLICY.PASS_EXP_ALERT = Convert.ToInt16(pPASS_EXP_ALERT);
                    OBJ_LG_CRD_PASSWORD_POLICY.PASS_AUTO_CREATION = Convert.ToInt16(pPASS_AUTO_CREATION);
                    OBJ_LG_CRD_PASSWORD_POLICY.PASS_CHANGE_BY_ADMIN = Convert.ToInt16(pPASS_CHANGE_BY_ADMIN);
                    OBJ_LG_CRD_PASSWORD_POLICY.PASS_CHANGE_AT_FIRST_LOGIN = Convert.ToInt16(pPASS_CHANGE_AT_FIRST_LOGIN);
                    OBJ_LG_CRD_PASSWORD_POLICY.MAKE_DT = System.DateTime.Now;
                    OBJ_LG_CRD_PASSWORD_POLICY.AUTH_STATUS_ID = "U";
                    OBJ_LG_CRD_PASSWORD_POLICY.LAST_ACTION = "ADD";
                    Obj_DBModelEntities.LG_CRD_PASSWORD_POLICY.Add(OBJ_LG_CRD_PASSWORD_POLICY);
                    Obj_DBModelEntities.SaveChanges();

                    #region Auth log
                    LG_AA_NFT_AUTH_LOG_MAP OBJ_LG_AA_NFT_AUTH_LOG_MAP = new LG_AA_NFT_AUTH_LOG_MAP();
                    FUNCTION_ID = OBJ_LG_AA_NFT_AUTH_LOG_MAP.FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "PasswordPolicy").Select(x => x.FUNCTION_ID).SingleOrDefault();
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_NAME = "LG_CRD_PASSWORD_POLICY";
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_NM = "SL_NO";
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_VAL = OBJ_LG_CRD_PASSWORD_POLICY.SL_NO;
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
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.MAKE_BY = p_user_id;
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.MAKE_DT = DateTime.Now;
                    LG_AA_NFT_AUTH_LOG_MAP.AddNftAuthLog(OBJ_LG_CRD_PASSWORD_POLICY, OBJ_LG_AA_NFT_AUTH_LOG_MAP);
                    #endregion

                    result = "True";
                    ts.Complete();

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
                            result = "Can't Add Password Policy(Db).";
                        }
                    }

                    LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "AddPasswordPolicy",
                           "0000000000", dbEx.Message, inner, dbEx.StackTrace, p_user_id, dateTime);

                    return result;
                }
                catch (Exception ex)
                {
                    ts.Dispose();
                    string dateTime = Convert.ToString(System.DateTime.Now);
                    string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                    LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "AddPasswordPolicy",
                           "0000000000", ex.Message, inner4, ex.StackTrace, p_user_id, dateTime);

                    result = "Can't Add Password Policy.";
                    return result;
                }
            }
        }

        #endregion

        #region Update

        public static string UpdatePasswordPolicy(string p_user_id, string pPASS_MAX_LENGTH, string pPASS_MIN_LENGTH, string pNUMERIC_CHAR_MIN, string pPASS_HIS_PERIOD, string pMIN_CAPS_LETTER, string pMIN_SMALL_LETTER, string pMIN_NUMERIC_CHAR, string pMIN_CONS_USE_PASS, string pPASS_REPEAT, string pPASS_CHANGED_EXPIRY, string pPASS_AUTO_CREATION, string pPASS_CHANGE_BY_ADMIN, string pPASS_CHANGE_AT_FIRST_LOGIN, string pAPPLICATION_ID, string pPASS_REUSE_MAX, string pFAILED_LOGIN_ATTEMT, string pPASS_EXP_PERIOD, string pPASS_EXP_ALERT)
        {

            using (TransactionScope ts = new TransactionScope())
            {
                DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
               

                LG_CRD_PASSWORD_POLICY_MAP LG_CRD_PASSWORD_POLICY_MAP_OLD = new LG_CRD_PASSWORD_POLICY_MAP();
                LG_CRD_PASSWORD_POLICY_MAP LG_CRD_PASSWORD_POLICY_MAP_NEW = new LG_CRD_PASSWORD_POLICY_MAP();

                
                string result = string.Empty;
                try
                {
                    string format = OutgoingResponseFormat.GetFormat();
                    OutgoingResponseFormat.SetResponseFormat(format);


                    LG_CRD_PASSWORD_POLICY OBJ_LG_CRD_PASSWORD_POLICY = (from p in Obj_DBModelEntities.LG_CRD_PASSWORD_POLICY where p.APPLICATION_ID == pAPPLICATION_ID select p).First();
                    Class_Conversion.LG_CRD_PASSWORD_POLICY_REVERSE_CON(LG_CRD_PASSWORD_POLICY_MAP_OLD, OBJ_LG_CRD_PASSWORD_POLICY);

                    Class_Conversion.LG_CRD_PASSWORD_POLICY_REVERSE_CON(LG_CRD_PASSWORD_POLICY_MAP_NEW, OBJ_LG_CRD_PASSWORD_POLICY);


                    LG_CRD_PASSWORD_POLICY_MAP_NEW.APPLICATION_ID = pAPPLICATION_ID;
                    LG_CRD_PASSWORD_POLICY_MAP_NEW.PASS_MAX_LENGTH = pPASS_MAX_LENGTH;
                    LG_CRD_PASSWORD_POLICY_MAP_NEW.PASS_MIN_LENGTH = pPASS_MIN_LENGTH;
                    LG_CRD_PASSWORD_POLICY_MAP_NEW.NUMERIC_CHAR_MIN = Convert.ToInt16(pNUMERIC_CHAR_MIN);
                    LG_CRD_PASSWORD_POLICY_MAP_NEW.PASS_HIS_PERIOD = pPASS_HIS_PERIOD;
                    LG_CRD_PASSWORD_POLICY_MAP_NEW.MIN_CAPS_LETTER = Convert.ToInt16(pMIN_CAPS_LETTER);
                    LG_CRD_PASSWORD_POLICY_MAP_NEW.MIN_SMALL_LETTER = Convert.ToInt16(pMIN_SMALL_LETTER);
                    LG_CRD_PASSWORD_POLICY_MAP_NEW.MIN_NUMERIC_CHAR = Convert.ToInt16(pMIN_NUMERIC_CHAR);
                    LG_CRD_PASSWORD_POLICY_MAP_NEW.MIN_CONS_USE_PASS = pMIN_CONS_USE_PASS;
                    LG_CRD_PASSWORD_POLICY_MAP_NEW.PASS_REPEAT = pPASS_REPEAT;
                    LG_CRD_PASSWORD_POLICY_MAP_NEW.PASS_CHANGED_EXPIRY = Convert.ToInt16(pPASS_CHANGED_EXPIRY);
                    LG_CRD_PASSWORD_POLICY_MAP_NEW.PASS_REUSE_MAX = Convert.ToInt16(pPASS_REUSE_MAX);
                    LG_CRD_PASSWORD_POLICY_MAP_NEW.FAILED_LOGIN_ATTEMT = Convert.ToInt16(pFAILED_LOGIN_ATTEMT);
                    LG_CRD_PASSWORD_POLICY_MAP_NEW.PASS_EXP_PERIOD = Convert.ToInt16(pPASS_EXP_PERIOD);
                    LG_CRD_PASSWORD_POLICY_MAP_NEW.PASS_EXP_ALERT = Convert.ToInt16(pPASS_EXP_ALERT);
                    LG_CRD_PASSWORD_POLICY_MAP_NEW.PASS_AUTO_CREATION = Convert.ToInt16(pPASS_AUTO_CREATION);
                    LG_CRD_PASSWORD_POLICY_MAP_NEW.PASS_CHANGE_BY_ADMIN = Convert.ToInt16(pPASS_CHANGE_BY_ADMIN);
                    LG_CRD_PASSWORD_POLICY_MAP_NEW.PASS_CHANG_AT_FIRST_LOGIN = Convert.ToInt16(pPASS_CHANGE_AT_FIRST_LOGIN);

                    OBJ_LG_CRD_PASSWORD_POLICY.LAST_UPDATE_DT = System.DateTime.Now;
                    OBJ_LG_CRD_PASSWORD_POLICY.AUTH_STATUS_ID = "U";
                    OBJ_LG_CRD_PASSWORD_POLICY.LAST_ACTION = "EDT";
                    Obj_DBModelEntities.SaveChanges();

                    #region Auth log
                    LG_AA_NFT_AUTH_LOG_MAP OBJ_LG_AA_NFT_AUTH_LOG_MAP = new LG_AA_NFT_AUTH_LOG_MAP();
                    FUNCTION_ID = OBJ_LG_AA_NFT_AUTH_LOG_MAP.FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "PasswordPolicy").Select(x => x.FUNCTION_ID).SingleOrDefault();
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_NAME = "LG_CRD_PASSWORD_POLICY";
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_NM = "SL_NO";
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_VAL = OBJ_LG_CRD_PASSWORD_POLICY.SL_NO;
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
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.MAKE_BY = p_user_id;
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.MAKE_DT = DateTime.Now;
                    LG_AA_NFT_AUTH_LOG_MAP.AddNftAuthLog(LG_CRD_PASSWORD_POLICY_MAP_OLD, LG_CRD_PASSWORD_POLICY_MAP_NEW, OBJ_LG_AA_NFT_AUTH_LOG_MAP);
                    #endregion

                    result = "True";

                    ts.Complete();

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
                            result = "Can't Update Password Policy(Db).";
                        }
                    }

                    LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "UpdatePasswordPolicy",
           "0000000000", dbEx.Message, inner, dbEx.StackTrace, p_user_id, dateTime);

                    return result;
                }
                catch (Exception ex)
                {
                    ts.Dispose();
                    string dateTime = Convert.ToString(System.DateTime.Now);
                    string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                    LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "UpdatePasswordPolicy",
                           "0000000000", ex.Message, inner4, ex.StackTrace, p_user_id, dateTime);

                    result = "Can't Update Password Policy.";
                    return result;
                }
            }
        }

        #endregion

        #region Delete

        public static string DeletePasswordPolicy(string p_user_id, string pAPPLICATION_ID)
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            string result = string.Empty;
            try
            {
                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);


                LG_CRD_PASSWORD_POLICY OBJ_LG_CRD_PASSWORD_POLICY = (from a in Obj_DBModelEntities.LG_CRD_PASSWORD_POLICY
                                                                     where a.APPLICATION_ID == pAPPLICATION_ID
                                                                     select a).SingleOrDefault();
                if (OBJ_LG_CRD_PASSWORD_POLICY != null)
                {
                    OBJ_LG_CRD_PASSWORD_POLICY.AUTH_STATUS_ID = "U";
                    OBJ_LG_CRD_PASSWORD_POLICY.LAST_ACTION = "DEL";

                    //Obj_DBModelEntities.LG_CRD_PASSWORD_POLICY.Remove(OBJ_LG_CRD_PASSWORD_POLICY);
                    Obj_DBModelEntities.SaveChanges();

                    #region Auth log
                    LG_AA_NFT_AUTH_LOG_MAP OBJ_LG_AA_NFT_AUTH_LOG_MAP = new LG_AA_NFT_AUTH_LOG_MAP();
                    FUNCTION_ID = OBJ_LG_AA_NFT_AUTH_LOG_MAP.FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "PasswordPolicy").Select(x => x.FUNCTION_ID).SingleOrDefault();
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_NAME = "LG_CRD_PASSWORD_POLICY";
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_NM = "SL_NO";
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_VAL = OBJ_LG_CRD_PASSWORD_POLICY.SL_NO;
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
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.MAKE_BY = p_user_id;
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.MAKE_DT = DateTime.Now;
                    LG_AA_NFT_AUTH_LOG_MAP.AddNftAuthLog(OBJ_LG_CRD_PASSWORD_POLICY, OBJ_LG_AA_NFT_AUTH_LOG_MAP);
                    #endregion

                    result = "True";
                    return result;
                }
                else
                    return "Can't delete this Password Policy.";
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
                        result = "Can't Delete Password Policy(Db)";
                    }
                }

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "DeletePasswordPolicy",
       "0000000000", dbEx.Message, inner, dbEx.StackTrace, p_user_id, dateTime);

                return result;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);

                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "DeletePasswordPolicy",
                       "0000000000", ex.Message, inner4, ex.StackTrace, p_user_id, dateTime);

                result = "Can't Delete Password Policy ";
                return result;
            }
        }

        #endregion

        #region Fetch Single

        public static LG_CRD_PASSWORD_POLICY_MAP GetPasswordPolicyByAppId(string pAPPLICATION_ID)
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            LG_CRD_PASSWORD_POLICY_MAP OBJ_LG_CRD_PASSWORD_POLICY = new LG_CRD_PASSWORD_POLICY_MAP();

            string FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "PasswordPolicy").Select(x => x.FUNCTION_ID).SingleOrDefault();

            try
            {
                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);


                OBJ_LG_CRD_PASSWORD_POLICY = (from a in Obj_DBModelEntities.LG_CRD_PASSWORD_POLICY
                                              where a.APPLICATION_ID == pAPPLICATION_ID
                                              orderby a.SL_NO ascending
                                              select new LG_CRD_PASSWORD_POLICY_MAP
                                              {
                                                  SL_NO = a.SL_NO,
                                                  APPLICATION_ID = a.APPLICATION_ID,
                                                  //APPLICATION_NAME = a.APPLICATION_NAME,
                                                  PASS_MAX_LENGTH = a.PASS_MAX_LENGTH,
                                                  PASS_MIN_LENGTH = a.PASS_MIN_LENGTH,
                                                  NUMERIC_CHAR_MIN = a.NUMERIC_CHAR_MIN,
                                                  PASS_HIS_PERIOD = a.PASS_HIS_PERIOD,
                                                  PASS_REUSE_MAX = a.PASS_REUSE_MAX,
                                                  FAILED_LOGIN_ATTEMT = a.FAILED_LOGIN_ATTEMT,
                                                  PASS_EXP_PERIOD = a.PASS_EXP_PERIOD,
                                                  PASS_EXP_ALERT = a.PASS_EXP_ALERT,
                                                  MIN_CAPS_LETTER = a.MIN_CAPS_LETTER,
                                                  MIN_SMALL_LETTER = a.MIN_SMALL_LETTER,
                                                  MIN_NUMERIC_CHAR = a.MIN_NUMERIC_CHAR,
                                                  MIN_CONS_USE_PASS = a.MIN_CONS_USE_PASS,
                                                  PASS_REPEAT = a.PASS_REPEAT,
                                                  PASS_CHANGED_EXPIRY = a.PASS_CHANGED_EXPIRY,
                                                  //PASS_REUSE_MAX = a.PASS_REUSE_MAX,
                                                  PASS_AUTO_CREATION = a.PASS_AUTO_CREATION,
                                                  PASS_CHANG_AT_FIRST_LOGIN = a.PASS_CHANGE_AT_FIRST_LOGIN,
                                                  PASS_CHANGE_BY_ADMIN = a.PASS_CHANGE_BY_ADMIN,
                                                  AUTH_STATUS_ID = a.AUTH_STATUS_ID,
                                                  LAST_ACTION = a.LAST_ACTION,
                                                  LAST_UPDATE_DT = a.LAST_UPDATE_DT,
                                                  MAKE_DT = a.MAKE_DT
                                              }).SingleOrDefault();
                return OBJ_LG_CRD_PASSWORD_POLICY;
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
                        OBJ_LG_CRD_PASSWORD_POLICY.ERROR = validationError.ErrorMessage;
                    }
                }

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "GetPasswordPolicyByAppId",
                       "0000000000", dbEx.Message, inner, dbEx.StackTrace, "ApplicationId:" + pAPPLICATION_ID, dateTime);

                return OBJ_LG_CRD_PASSWORD_POLICY;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);


                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "AddPasswordPolicy",
                       "0000000000", ex.Message, inner4, ex.StackTrace, "ApplicationId:" + pAPPLICATION_ID, dateTime);

                OBJ_LG_CRD_PASSWORD_POLICY.ERROR = ex.Message;
                return OBJ_LG_CRD_PASSWORD_POLICY;
            }
        }

        #endregion

        #region Fetch all

        public static IEnumerable<LG_CRD_PASSWORD_POLICY_MAP> GetPasswordPolicy()
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            List<LG_CRD_PASSWORD_POLICY_MAP> LIST_PASSWORD_POLICY_MAP = null;
            LG_CRD_PASSWORD_POLICY_MAP OBJ_LG_CRD_PASSWORD_POLICY_MAP = new LG_CRD_PASSWORD_POLICY_MAP();
            string FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "PasswordPolicy").Select(x => x.FUNCTION_ID).SingleOrDefault();

            try
            {
                Obj_DBModelEntities.Configuration.LazyLoadingEnabled = false;
                Obj_DBModelEntities.Configuration.ProxyCreationEnabled = false;

                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);


                LIST_PASSWORD_POLICY_MAP = (from a in Obj_DBModelEntities.LG_CRD_PASSWORD_POLICY
                                            where (a.SL_NO != null && a.AUTH_STATUS_ID == "A" && (a.LAST_ACTION != "DEL"))
                                            orderby a.SL_NO ascending
                                            select new LG_CRD_PASSWORD_POLICY_MAP
                                            {
                                                SL_NO = a.SL_NO,
                                                APPLICATION_ID = a.APPLICATION_ID,
                                                PASS_MAX_LENGTH = a.PASS_MAX_LENGTH,
                                                PASS_MIN_LENGTH = a.PASS_MIN_LENGTH,
                                                NUMERIC_CHAR_MIN = a.NUMERIC_CHAR_MIN,
                                                PASS_HIS_PERIOD = a.PASS_HIS_PERIOD,
                                                PASS_REUSE_MAX = a.PASS_REUSE_MAX,
                                                FAILED_LOGIN_ATTEMT = a.FAILED_LOGIN_ATTEMT,
                                                PASS_EXP_PERIOD = a.PASS_EXP_PERIOD,
                                                PASS_EXP_ALERT = a.PASS_EXP_ALERT,
                                                MIN_CAPS_LETTER = a.MIN_CAPS_LETTER,
                                                MIN_SMALL_LETTER = a.MIN_SMALL_LETTER,
                                                MIN_NUMERIC_CHAR = a.MIN_NUMERIC_CHAR,
                                                MIN_CONS_USE_PASS = a.MIN_CONS_USE_PASS,
                                                PASS_REPEAT = a.PASS_REPEAT,
                                                PASS_CHANGED_EXPIRY = a.PASS_CHANGED_EXPIRY,
                                                PASS_AUTO_CREATION = a.PASS_AUTO_CREATION,
                                                PASS_CHANGE_BY_ADMIN = a.PASS_CHANGE_BY_ADMIN,
                                                PASS_CHANG_AT_FIRST_LOGIN = a.PASS_CHANGE_AT_FIRST_LOGIN,
                                                AUTH_STATUS_ID = a.AUTH_STATUS_ID,
                                                LAST_ACTION = a.LAST_ACTION,
                                                LAST_UPDATE_DT = a.LAST_UPDATE_DT,
                                                MAKE_DT = a.MAKE_DT,


                                            }).ToList();
                return LIST_PASSWORD_POLICY_MAP;
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
                        OBJ_LG_CRD_PASSWORD_POLICY_MAP.ERROR = validationError.ErrorMessage;
                        LIST_PASSWORD_POLICY_MAP.Add(OBJ_LG_CRD_PASSWORD_POLICY_MAP);
                    }
                }

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "GetPasswordPolicy",
       "0000000000", dbEx.Message, inner, dbEx.StackTrace, null, dateTime);

                return LIST_PASSWORD_POLICY_MAP;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "GetPasswordPolicy",
                       "0000000000", ex.Message, inner4, ex.StackTrace, null, dateTime);

                OBJ_LG_CRD_PASSWORD_POLICY_MAP.ERROR = ex.Message;
                LIST_PASSWORD_POLICY_MAP.Add(OBJ_LG_CRD_PASSWORD_POLICY_MAP);
                return LIST_PASSWORD_POLICY_MAP;
            }
        }

        #endregion

        #region Validate

        public static string ValidatePasswordPolicyOnCreation(string pAPPLICATION_ID, string pPASSWORD)
        {

            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            LG_CRD_PASSWORD_POLICY OBJ_LG_CRD_PASSWORD_POLICY = new LG_CRD_PASSWORD_POLICY();

            string result = string.Empty;
            string FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "PasswordPolicy").Select(x => x.FUNCTION_ID).SingleOrDefault();
            #region FetchSomePasswordProperty

            string passwordString = pPASSWORD;

            byte[] pass_byte = Encoding.ASCII.GetBytes(passwordString);

            int arrLength = pass_byte.Length;

            for (int i = 0; i < (arrLength - 1); i++)
            {
                if (pass_byte[i] == pass_byte[i + 1])
                {
                    return "Password Can't contain successive identical characters.";
                }

            }


            int numberOfUpperCaseAlphabet = 0;
            int numberOfLowerCaseAlphabet = 0;
            int numberOfNumericCharacter = 0;
            int lengthOfString = passwordString.Length;
            int numberOfNonAlphaNumericCharacter = 0;


            foreach (int val in pass_byte)
            {
                int flag = 0;
                if (val > 64 && val < 91) { numberOfUpperCaseAlphabet++; flag = 1; }
                if (val > 96 && val < 123) { numberOfLowerCaseAlphabet++; flag = 1; }
                if (val > 47 && val < 58) { numberOfNumericCharacter++; flag = 1; }
                if (flag == 0) { numberOfNonAlphaNumericCharacter++; }
            }

            #endregion

            try
            {


                OBJ_LG_CRD_PASSWORD_POLICY = (from p in Obj_DBModelEntities.LG_CRD_PASSWORD_POLICY
                                              where p.APPLICATION_ID == pAPPLICATION_ID
                                              select p).First();


                if (OBJ_LG_CRD_PASSWORD_POLICY != null)
                {

                    if (Convert.ToInt16(OBJ_LG_CRD_PASSWORD_POLICY.PASS_MAX_LENGTH) < lengthOfString)
                    {
                        string template = "Password maximum length should be within {0} characters.";
                        string data = OBJ_LG_CRD_PASSWORD_POLICY.PASS_MAX_LENGTH.ToString();
                        result = string.Format(template, data);
                        return result;
                    }

                    if (Convert.ToInt16(OBJ_LG_CRD_PASSWORD_POLICY.PASS_MIN_LENGTH) > lengthOfString)
                    {
                        string template = "Password minimum length should be at least {0} characters.";
                        string data = OBJ_LG_CRD_PASSWORD_POLICY.PASS_MIN_LENGTH.ToString();
                        result = string.Format(template, data);
                        return result;
                    }


                    //if (Convert.ToInt16(OBJ_LG_CRD_PASSWORD_POLICY.PASS_HIS_PERIOD) < Convert.ToInt16(pPASS_HIS_PERIOD))
                    //{
                    //    string template = "Password History Period should be within {0} characters.";
                    //    string data = OBJ_LG_CRD_PASSWORD_POLICY.PASS_HIS_PERIOD.ToString();
                    //    result = string.Format(template, data);
                    //    return result;
                    //}

                    if (Convert.ToInt16(OBJ_LG_CRD_PASSWORD_POLICY.MIN_CAPS_LETTER) > numberOfUpperCaseAlphabet)
                    {
                        string template = "Password should contain at least {0} capital letters.";
                        string data = OBJ_LG_CRD_PASSWORD_POLICY.MIN_CAPS_LETTER.ToString();
                        result = string.Format(template, data);
                        return result;
                    }

                    if (Convert.ToInt16(OBJ_LG_CRD_PASSWORD_POLICY.MIN_SMALL_LETTER) > numberOfLowerCaseAlphabet)
                    {
                        string template = "Password should contain at least {0} small letters.";
                        string data = OBJ_LG_CRD_PASSWORD_POLICY.MIN_SMALL_LETTER.ToString();
                        result = string.Format(template, data);
                        return result;
                    }

                    if (Convert.ToInt16(OBJ_LG_CRD_PASSWORD_POLICY.MIN_NUMERIC_CHAR) > numberOfNumericCharacter)
                    {
                        string template = "Password should contain at least {0} numeric characters.";
                        string data = OBJ_LG_CRD_PASSWORD_POLICY.MIN_NUMERIC_CHAR.ToString();
                        result = string.Format(template, data);
                        return result;
                    }

                    if (Convert.ToInt16(OBJ_LG_CRD_PASSWORD_POLICY.NUMERIC_CHAR_MIN) > numberOfNonAlphaNumericCharacter)
                    {
                        string template = "Password should contain at least {0} non alpha numeric characters.";
                        string data = OBJ_LG_CRD_PASSWORD_POLICY.NUMERIC_CHAR_MIN.ToString();
                        result = string.Format(template, data);
                        return result;
                    }

                    //        if (Convert.ToInt16(OBJ_LG_CRD_PASSWORD_POLICY.PASS_REPEAT) < Convert.ToInt16(pPASS_REPEAT))
                    //        {
                    //            string template = "Same password shouldn't be repeated {0} times.";
                    //            string data = OBJ_LG_CRD_PASSWORD_POLICY.PASS_REPEAT.ToString();
                    //            result = string.Format(template, data);
                    //            return result;
                    //        }




                    //if (Convert.ToInt16(OBJ_LG_CRD_PASSWORD_POLICY.PASS_CHANGED_EXPIRY) < Convert.ToInt16(pPASS_CHANGED_EXPIRY))
                    //{
                    //    string template = "(..........................)";
                    //    string data = OBJ_LG_CRD_PASSWORD_POLICY.PASS_CHANGED_EXPIRY.ToString();
                    //    result = string.Format(template, data);
                    //    return result;
                    //}
                }
                else
                {
                    result = "No policy was set up for this application.";
                    return result;
                }
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
                        result = validationError.ErrorMessage.ToString();
                        return result;
                    }
                }

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "ValidatePasswordPolicyOnCreation",
                       "0000000000", dbEx.Message, inner, dbEx.StackTrace, "ApplicationId:" + pAPPLICATION_ID, dateTime);
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "ValidatePasswordPolicyOnCreation",
                       "0000000000", ex.Message, inner4, ex.StackTrace, null, dateTime);

                result = ex.Message;
                return result;
            }


            result = "Valid";
            return result;
        }



        //public static string ValidatePasswordPolicyOnLogin(string pUSER_ID, string pAPPLICATION_ID, string pPASSWORD)
        //{
        //    DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
        //    LG_CRD_PASSWORD_POLICY OBJ_LG_CRD_PASSWORD_POLICY = new LG_CRD_PASSWORD_POLICY();

        //    string result = string.Empty;

        //    try
        //    {
        //        OBJ_LG_CRD_PASSWORD_POLICY = (from p in Obj_DBModelEntities.LG_CRD_PASSWORD_POLICY
        //                                      where p.APPLICATION_ID == pAPPLICATION_ID
        //                                      select p).First();



        //        if (Convert.ToInt16(OBJ_LG_CRD_PASSWORD_POLICY.FAILED_LOGIN_ATTEMT) < Convert.ToInt16(pFAILED_LOGIN_ATTEMT))
        //        {
        //            string template = "After {0} failed attempt you can't log in.";
        //            string data = OBJ_LG_CRD_PASSWORD_POLICY.FAILED_LOGIN_ATTEMT.ToString();
        //            result = string.Format(template, data);
        //            return result;
        //        }

        //        if (Convert.ToInt16(OBJ_LG_CRD_PASSWORD_POLICY.PASS_EXP_PERIOD) < Convert.ToInt16(pPASS_EXP_PERIOD))
        //        {
        //            string template = "Password is expired.";
        //            string data = OBJ_LG_CRD_PASSWORD_POLICY.PASS_EXP_PERIOD.ToString();
        //            result = string.Format(template, data);
        //            return result;
        //        }

        //        if (Convert.ToInt16(OBJ_LG_CRD_PASSWORD_POLICY.PASS_EXP_ALERT) < Convert.ToInt16(pPASS_EXP_ALERT))
        //        {
        //            string template = "Password will be expired soon";
        //            string data = OBJ_LG_CRD_PASSWORD_POLICY.PASS_EXP_ALERT.ToString();
        //            result = string.Format(template, data);
        //            return result;
        //        }


        //    }

        //    catch (DbEntityValidationException dbEx)
        //    {
        //        foreach (var validationErrors in dbEx.EntityValidationErrors)
        //        {
        //            foreach (var validationError in validationErrors.ValidationErrors)
        //            {
        //                Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
        //                result = validationError.ErrorMessage.ToString();
        //                return result;
        //            }
        //        }
        //    }

        //    catch (Exception ex)
        //    {
        //        result = ex.Message;
        //        return result;
        //    }


        //    return result;
        //}


        //public static string ValidatePasswordPolicyOnChangeOrResetPassword(string pUSER_ID, string pAPPLICATION_ID, string pPASSWORD)
        //{
        //    DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
        //    LG_CRD_PASSWORD_POLICY OBJ_LG_CRD_PASSWORD_POLICY = new LG_CRD_PASSWORD_POLICY();

        //    string result = string.Empty;

        //    try
        //    {
        //        OBJ_LG_CRD_PASSWORD_POLICY = (from p in Obj_DBModelEntities.LG_CRD_PASSWORD_POLICY
        //                                      where p.APPLICATION_ID == pAPPLICATION_ID
        //                                      select p).First();



        //        if (Convert.ToInt16(OBJ_LG_CRD_PASSWORD_POLICY.MIN_CONS_USE_PASS) < Convert.ToInt16(pMIN_CONS_USE_PASS))
        //        {
        //            string template = "Minimum consequtive of using password should be within {0}";
        //            string data = OBJ_LG_CRD_PASSWORD_POLICY.MIN_CONS_USE_PASS.ToString();
        //            result = string.Format(template, data);
        //            return result;
        //        }



        //    }

        //    catch (DbEntityValidationException dbEx)
        //    {
        //        foreach (var validationErrors in dbEx.EntityValidationErrors)
        //        {
        //            foreach (var validationError in validationErrors.ValidationErrors)
        //            {
        //                Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
        //                result = validationError.ErrorMessage.ToString();
        //                return result;
        //            }
        //        }
        //    }

        //    catch (Exception ex)
        //    {
        //        result = ex.Message;
        //        return result;
        //    }

        //    return result;
        //}


        //enum PasswordScore
        //{
        //    Blank = 0,
        //    VeryWeak = 1,
        //    Weak = 2,
        //    Medium = 3,
        //    Strong = 4,
        //    VeryStrong = 5
        //}


        #endregion

        #endregion


    }

}
