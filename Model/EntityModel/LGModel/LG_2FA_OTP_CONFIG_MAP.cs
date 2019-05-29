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
    public class LG_2FA_OTP_CONFIG_MAP
    {
        #region Properties
        [DataMember]
        public string APPLICATION_ID { get; set; }
        [DataMember]
        public int MAIL_FLAG { get; set; }
        [DataMember]
        public int SMS_FLAG { get; set; }
        [DataMember]
        public string VALIDITY_PERIOD { get; set; }
        [DataMember]
        public string NO_OF_OTP_DIGIT { get; set; }
        [DataMember]
        public string OTP_FORMAT_ID { get; set; }
        [DataMember]
        public string MAKE_BY { get; set; }
        [DataMember]
        public DateTime MAKE_DT { get; set; }
        [DataMember]
        public string OTP_ID { get; set; }
        [DataMember]
        public string ERROR { get; set; }
        [DataMember]
        public string AUTH_STATUS_ID { get; set; }
        [DataMember]
        public string LAST_ACTION { get; set; }
        [DataMember]
        public DateTime? LAST_UPDATE_DT { get; set; }


        [DataMember]
        public string APPLICATION_NAME { get; set; }
        [DataMember]
        public bool MAIL_FLAG_B { get; set; }
        [DataMember]
        public bool SMS_FLAG_B { get; set; }
        [DataMember]
        public IEnumerable<SelectListItem> OTP_FORMATE_LIST_FOR_DD { get; set; }
        [DataMember]
        public IEnumerable<SelectListItem> APPLICATION_LIST_FOR_DD { get; set; }

        public static string FUNCTION_ID;

        #endregion


        #region Events

        #region Add New
        public static string AddOtpConfig(string pAPPLICATION_ID, string pMAIL_FLAG, string pSMS_FLAG, string pVALIDITY_PERIOD, string pNO_OF_OTP_DIGIT, string pOTP_FORMAT_ID, string pMAKE_BY)
        {
            using (TransactionScope ts = new TransactionScope())
            {

                DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
                FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "OTP").Select(x => x.FUNCTION_ID).SingleOrDefault();
                LG_2FA_OTP_CONFIG OBJ_LG_2FA_OTP_CONFIG = new LG_2FA_OTP_CONFIG();
             
                string result = string.Empty;
                try
                {
                    string format = OutgoingResponseFormat.GetFormat();
                    OutgoingResponseFormat.SetResponseFormat(format);

                    OBJ_LG_2FA_OTP_CONFIG = Obj_DBModelEntities.LG_2FA_OTP_CONFIG.Where(otp => otp.APPLICATION_ID == pAPPLICATION_ID).SingleOrDefault();
                    if (OBJ_LG_2FA_OTP_CONFIG != null)
                    {
                        return "OTP configuation already exists for this application.";
                    }

                    int id = (Obj_DBModelEntities.LG_2FA_OTP_CONFIG.Select(i => i.OTP_ID).Cast<int?>().Max() ?? 0) + 1;
                    
                    OBJ_LG_2FA_OTP_CONFIG = new LG_2FA_OTP_CONFIG();
                    OBJ_LG_2FA_OTP_CONFIG.OTP_ID = id.ToString();
                    OBJ_LG_2FA_OTP_CONFIG.APPLICATION_ID = pAPPLICATION_ID;
                    OBJ_LG_2FA_OTP_CONFIG.MAIL_FLAG = Convert.ToInt16(pMAIL_FLAG);
                    OBJ_LG_2FA_OTP_CONFIG.SMS_FLAG = Convert.ToInt16(pSMS_FLAG);
                    OBJ_LG_2FA_OTP_CONFIG.VALIDITY_PERIOD = pVALIDITY_PERIOD;
                    OBJ_LG_2FA_OTP_CONFIG.NO_OF_OTP_DIGIT = pNO_OF_OTP_DIGIT;
                    OBJ_LG_2FA_OTP_CONFIG.OTP_FORMAT_ID = pOTP_FORMAT_ID;
                    OBJ_LG_2FA_OTP_CONFIG.AUTH_STATUS_ID = "U";
                    OBJ_LG_2FA_OTP_CONFIG.LAST_ACTION = "ADD";
                    OBJ_LG_2FA_OTP_CONFIG.MAKE_DT = System.DateTime.Now;
                    Obj_DBModelEntities.LG_2FA_OTP_CONFIG.Add(OBJ_LG_2FA_OTP_CONFIG);
                    Obj_DBModelEntities.SaveChanges();
                    LG_2FA_OTP_CONFIG_MAP OBJ_LG_2FA_OTP_CONFIG_MAP = new LG_2FA_OTP_CONFIG_MAP();
                    Class_Conversion.LG_2FA_OTP_CONFIG_MAP_REVERSE_CON(OBJ_LG_2FA_OTP_CONFIG_MAP, OBJ_LG_2FA_OTP_CONFIG);

                    #region Auth log
                    LG_AA_NFT_AUTH_LOG_MAP OBJ_LG_AA_NFT_AUTH_LOG_MAP = new LG_AA_NFT_AUTH_LOG_MAP();
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "OTP").Select(x => x.FUNCTION_ID).SingleOrDefault();
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_NAME = "LG_2FA_OTP_CONFIG";
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_NM = "OTP_ID";
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_VAL = OBJ_LG_2FA_OTP_CONFIG.OTP_ID;
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
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.MAKE_BY = pMAKE_BY;
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.MAKE_DT = DateTime.Now;
                    LG_AA_NFT_AUTH_LOG_MAP.AddNftAuthLog(OBJ_LG_2FA_OTP_CONFIG_MAP, OBJ_LG_AA_NFT_AUTH_LOG_MAP);
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
                            result = "Can't Add OTP Config(Db).";
                        }
                    }

                    LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "AddOtpConfig",
                    "0000000000", dbEx.Message, inner, dbEx.StackTrace, pMAKE_BY, dateTime);

                    return result;
                }
                catch (Exception ex)
                {
                    ts.Dispose();
                    string dateTime = Convert.ToString(System.DateTime.Now);
                    string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                    LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "AddOtpConfig",
                    "0000000000", ex.Message, inner4, ex.StackTrace, pMAKE_BY, dateTime);

                    result = "Can't Add OTP Config.";
                    return result;
                }
            }
        }

        public static string CreateSmsTxt(string pfrom_branch, string pfrom_ac_no, string pcustomer_id, string pcell_no, string psms_fnc_id, string msg_txt)
        {

            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            string FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "OTP").Select(x => x.FUNCTION_ID).SingleOrDefault();
            string result = string.Empty;
            try
            {
                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                string v_sms_id = string.Empty;
                int id = Convert.ToInt32(Obj_DBModelEntities.LG_2FA_OTP_SMS_MSG
                      .Max(x => (int?)x.SMS_ID) ?? 0) + 1;

                LG_2FA_OTP_SMS_MSG OBJ_LG_2FA_OTP_SMS_MSG = new LG_2FA_OTP_SMS_MSG();

                OBJ_LG_2FA_OTP_SMS_MSG.SMS_ID = id;
                OBJ_LG_2FA_OTP_SMS_MSG.BRANCH_ID = pfrom_branch;
                OBJ_LG_2FA_OTP_SMS_MSG.ACCOUNT_NO = pfrom_ac_no;
                OBJ_LG_2FA_OTP_SMS_MSG.CUSTOMER_ID = pcustomer_id;
                OBJ_LG_2FA_OTP_SMS_MSG.CELL_NO = pcell_no;
                OBJ_LG_2FA_OTP_SMS_MSG.MSG_TXT = msg_txt;
                OBJ_LG_2FA_OTP_SMS_MSG.MSG_SENT_FLAG = 0;
                OBJ_LG_2FA_OTP_SMS_MSG.SMS_FNC_ID = Convert.ToInt16(psms_fnc_id);
                OBJ_LG_2FA_OTP_SMS_MSG.SMS_INIT_TIME = System.DateTime.Now;
                Obj_DBModelEntities.LG_2FA_OTP_SMS_MSG.Add(OBJ_LG_2FA_OTP_SMS_MSG);
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
                        result = "Can't create sms text(Db).";
                    }
                }

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "CreateSmsTxt",
                                                "0000000000", dbEx.Message, inner, dbEx.StackTrace, null, dateTime);

                return result;
            }
            catch (Exception ex)
            {
               
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "CreateSmsTxt",
                                                 "0000000000", ex.Message, inner4, ex.StackTrace, null, dateTime);

                result = "Can't create sms text " + ex.Message;
                return result;
            }
        }
        public static string GenerateToken(string puser_id, string psession_id, string pterminal_ip, string pfunction_type, string pfrom_branch, string pfrom_ac_no, string pto_branch, string pto_ac_no, string pbill_id_no, string ptrans_amount, string ppn, string pcell_no, string psec_cell_no, string ppne, string papp_id, string pcard_no)
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            try
            {
                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                LG_2FA_TOKEN_GEN_LOG OBJ_LG_2FA_TOKEN_GEN_LOG = new LG_2FA_TOKEN_GEN_LOG();

                OBJ_LG_2FA_TOKEN_GEN_LOG.USER_ID = puser_id;
                OBJ_LG_2FA_TOKEN_GEN_LOG.SESSION_ID = psession_id;
                OBJ_LG_2FA_TOKEN_GEN_LOG.TERMINAL_IP = pterminal_ip;
                OBJ_LG_2FA_TOKEN_GEN_LOG.FUNCTION_TYPE = Convert.ToInt16(pfunction_type);
                OBJ_LG_2FA_TOKEN_GEN_LOG.FROM_BRANCH = pfrom_branch;
                OBJ_LG_2FA_TOKEN_GEN_LOG.FROM_AC_NO = pfrom_ac_no;
                OBJ_LG_2FA_TOKEN_GEN_LOG.TO_BRANCH = pto_branch;
                OBJ_LG_2FA_TOKEN_GEN_LOG.TO_AC_NO = pto_ac_no;
                OBJ_LG_2FA_TOKEN_GEN_LOG.BILL_ID_NO = pbill_id_no;
                if (!string.IsNullOrEmpty(ptrans_amount))
                {
                    OBJ_LG_2FA_TOKEN_GEN_LOG.TRANS_AMOUNT = Convert.ToDecimal(ptrans_amount);
                }
                OBJ_LG_2FA_TOKEN_GEN_LOG.PN = ppn;

                OBJ_LG_2FA_TOKEN_GEN_LOG.ACTIVE_FLAG = 1;
                OBJ_LG_2FA_TOKEN_GEN_LOG.GEN_TIME = System.DateTime.Now;
                OBJ_LG_2FA_TOKEN_GEN_LOG.VERI_FLAG = 0;
                OBJ_LG_2FA_TOKEN_GEN_LOG.VERI_TIME = null;
                OBJ_LG_2FA_TOKEN_GEN_LOG.VERI_RESULT = 0;
                OBJ_LG_2FA_TOKEN_GEN_LOG.FAIL_REASON_ID = 0;

                OBJ_LG_2FA_TOKEN_GEN_LOG.CELL_NO = pcell_no;
                OBJ_LG_2FA_TOKEN_GEN_LOG.SEC_CELL_NO = psec_cell_no;
                OBJ_LG_2FA_TOKEN_GEN_LOG.PNE = ppne;
                OBJ_LG_2FA_TOKEN_GEN_LOG.APPLICATION_ID = papp_id;
                OBJ_LG_2FA_TOKEN_GEN_LOG.CARD_NO = pcard_no;
                Obj_DBModelEntities.LG_2FA_TOKEN_GEN_LOG.Add(OBJ_LG_2FA_TOKEN_GEN_LOG);
                Obj_DBModelEntities.SaveChanges();
                return "1";
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "GenerateToken",
                                                  "0000000000", ex.Message, inner4, ex.StackTrace, psession_id, dateTime);

                return "0";
            }
        }
        public static string RegisterUser(string puser_id, string pcust_id, string pgroup_id, string pcell_no, string pemail_id, string preg_dt, string psec_cell_no, string papp_id)
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            try
            {
                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                LG_USER_INFO OBJ_LG_USER_INFO = new LG_USER_INFO();

                OBJ_LG_USER_INFO.USER_ID = puser_id;
                OBJ_LG_USER_INFO.CUST_ID = pcust_id;
                OBJ_LG_USER_INFO.GROUP_ID = pgroup_id;
                OBJ_LG_USER_INFO.CELL_NO = pcell_no;
                OBJ_LG_USER_INFO.EMAIL_ID = pemail_id;
                OBJ_LG_USER_INFO.REG_DT = System.DateTime.Now;
                OBJ_LG_USER_INFO.ACTIVE_FLAG = 1;
                OBJ_LG_USER_INFO.SEC_CELL_NO = psec_cell_no;
                OBJ_LG_USER_INFO.APPLICATION_ID = papp_id;
                Obj_DBModelEntities.LG_USER_INFO.Add(OBJ_LG_USER_INFO);
                Obj_DBModelEntities.SaveChanges();
                return "1";
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

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "RegisterUser",
                                                  "0000000000", ex.Message, inner4, ex.StackTrace, null, dateTime);

                return "0";
            }
        }
        #endregion

        #region Update
        public static string UpdateOtpConfig(string pAPPLICATION_ID, string pMAIL_FLAG, string pSMS_FLAG, string pVALIDITY_PERIOD, string pNO_OF_OTP_DIGIT, string pOTP_FORMAT_ID, string pMAKE_BY)
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            DBModelEntities Obj_DBModelEntitiesOLD = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "OTP").Select(x => x.FUNCTION_ID).SingleOrDefault();
            string result = string.Empty;
            try
            {
                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                LG_2FA_OTP_CONFIG OBJ_LG_2FA_OTP_CONFIG_OLD = Obj_DBModelEntitiesOLD.LG_2FA_OTP_CONFIG
                                                         .Where(otp => otp.APPLICATION_ID == pAPPLICATION_ID).SingleOrDefault();

                LG_2FA_OTP_CONFIG OBJ_LG_2FA_OTP_CONFIG = Obj_DBModelEntities.LG_2FA_OTP_CONFIG
                                                         .Where(otp => otp.APPLICATION_ID == pAPPLICATION_ID).SingleOrDefault();

                BooleanConversion.LG_2FA_OTP_CONFIG_MAP_BOOL_TO_INT(pMAIL_FLAG, pSMS_FLAG, OBJ_LG_2FA_OTP_CONFIG);

                OBJ_LG_2FA_OTP_CONFIG.VALIDITY_PERIOD = pVALIDITY_PERIOD;
                OBJ_LG_2FA_OTP_CONFIG.NO_OF_OTP_DIGIT = pNO_OF_OTP_DIGIT;
                OBJ_LG_2FA_OTP_CONFIG.OTP_FORMAT_ID = pOTP_FORMAT_ID;
                OBJ_LG_2FA_OTP_CONFIG.AUTH_STATUS_ID = "U";
                OBJ_LG_2FA_OTP_CONFIG.LAST_ACTION = "EDT";
                OBJ_LG_2FA_OTP_CONFIG.LAST_UPDATE_DT = System.DateTime.Now;
                Obj_DBModelEntities.SaveChanges();


                #region Auth log
                LG_AA_NFT_AUTH_LOG_MAP OBJ_LG_AA_NFT_AUTH_LOG_MAP = new LG_AA_NFT_AUTH_LOG_MAP();
                OBJ_LG_AA_NFT_AUTH_LOG_MAP.FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "OTP").Select(x => x.FUNCTION_ID).SingleOrDefault();
                OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_NAME = "LG_2FA_OTP_CONFIG";
                OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_NM = "OTP_ID";
                OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_VAL = OBJ_LG_2FA_OTP_CONFIG.OTP_ID;
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
                OBJ_LG_AA_NFT_AUTH_LOG_MAP.MAKE_BY = pMAKE_BY;
                OBJ_LG_AA_NFT_AUTH_LOG_MAP.MAKE_DT = DateTime.Now;
                LG_AA_NFT_AUTH_LOG_MAP.AddNftAuthLog(OBJ_LG_2FA_OTP_CONFIG_OLD, OBJ_LG_2FA_OTP_CONFIG, OBJ_LG_AA_NFT_AUTH_LOG_MAP);
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
                        result = "Can't Update OTP Config(Db).";
                    }
                }

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "UpdateOtpConfig",
                                               "0000000000", dbEx.Message, inner, dbEx.StackTrace, pMAKE_BY, dateTime);

                return result;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);
            

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "UpdateOtpConfig",
                                                 "0000000000", ex.Message, inner4, ex.StackTrace, null, dateTime);

                result = "Can't Update OTP Config.";
                return result;
            }
        }

        public static string UpdateTokenGenLogActiveFlag(string puser_id, string papp_id)
        {
            try
            {
                DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);

                var OBJ_TOKEN_GEN_LOG = (from t in Obj_DBModelEntities.LG_2FA_TOKEN_GEN_LOG
                                         where t.USER_ID == puser_id && t.APPLICATION_ID == papp_id
                                         select t).ToList();


                if (OBJ_TOKEN_GEN_LOG.Count != 0)
                {
                    OBJ_TOKEN_GEN_LOG.ForEach(t => t.ACTIVE_FLAG = 0);
                    Obj_DBModelEntities.SaveChanges();
                }
                return "1";
            }
            catch (Exception ex)
            {
              
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "UpdateTokenGenLogActiveFlag",
                                                  "0000000000", ex.Message, inner4, ex.StackTrace, null, dateTime);

                return "0";
            }
        }
        public static string UpdateTokenGenLogForTokenValidityPeriodExpired(string puser_id, string psession_id, string pterminal_ip, string pfunction_type, string ptoken1, string ptoken2, string papp_id)
        {
            try
            {
                DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                int function_type = Convert.ToInt16(pfunction_type);
                var OBJ_TOKEN_GEN_LOG = Obj_DBModelEntities.LG_2FA_TOKEN_GEN_LOG
                                            .Where(t => t.USER_ID == puser_id &&
                                                        t.SESSION_ID == psession_id &&
                                                        t.TERMINAL_IP == pterminal_ip &&
                                                        t.FUNCTION_TYPE == function_type &&
                                                        t.PN == ptoken1 &&
                                                        t.PNE == ptoken2 &&
                                                        t.ACTIVE_FLAG == 1 &&
                                                        t.APPLICATION_ID == papp_id).SingleOrDefault();


                if (OBJ_TOKEN_GEN_LOG != null)
                {
                    OBJ_TOKEN_GEN_LOG.VERI_FLAG = 1;
                    OBJ_TOKEN_GEN_LOG.VERI_TIME = System.DateTime.Now;
                    OBJ_TOKEN_GEN_LOG.VERI_RESULT = 1;
                    OBJ_TOKEN_GEN_LOG.FAIL_REASON_ID = 5;
                    OBJ_TOKEN_GEN_LOG.ACTIVE_FLAG = 0;
                    Obj_DBModelEntities.SaveChanges();
                }
                return "1";
            }
            catch (Exception ex)
            {
               
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "UpdateTokenGenLogForTokenValidityPeriodExpired",
                                                  "0000000000", ex.Message, inner4, ex.StackTrace, psession_id, dateTime);

                return "0";
            }
        }
        public static string UpdateTokenGenLogForSourceAccMismatch(string puser_id, string psession_id, string pterminal_ip, string pfunction_type, string ptoken1, string ptoken2, string papp_id)
        {
            try
            {
                DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);

                int function_type = Convert.ToInt16(pfunction_type);
                var OBJ_TOKEN_GEN_LOG = Obj_DBModelEntities.LG_2FA_TOKEN_GEN_LOG
                                            .Where(t => t.USER_ID == puser_id &&
                                                        t.SESSION_ID == psession_id &&
                                                        t.TERMINAL_IP == pterminal_ip &&
                                                        t.FUNCTION_TYPE == function_type &&
                                                        t.PN == ptoken1 &&
                                                        t.PNE == ptoken2 &&
                                                        t.ACTIVE_FLAG == 1 &&
                                                        t.APPLICATION_ID == papp_id).SingleOrDefault();


                if (OBJ_TOKEN_GEN_LOG != null)
                {
                    OBJ_TOKEN_GEN_LOG.VERI_FLAG = 1;
                    OBJ_TOKEN_GEN_LOG.VERI_TIME = System.DateTime.Now;
                    OBJ_TOKEN_GEN_LOG.VERI_RESULT = 0;
                    OBJ_TOKEN_GEN_LOG.FAIL_REASON_ID = 1;
                    Obj_DBModelEntities.SaveChanges();
                }
                return "1";
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);
              

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "UpdateTokenGenLogForSourceAccMismatch",
                                                  "0000000000", ex.Message, inner4, ex.StackTrace, psession_id, dateTime);

                return "0";
            }
        }
        public static string UpdateTokenGenLogForDestinationAccMismatch(string puser_id, string psession_id, string pterminal_ip, string pfunction_type, string ptoken1, string ptoken2, string papp_id)
        {
            try
            {
                DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);

                int function_type = Convert.ToInt16(pfunction_type);
                var OBJ_TOKEN_GEN_LOG = Obj_DBModelEntities.LG_2FA_TOKEN_GEN_LOG
                                            .Where(t => t.USER_ID == puser_id &&
                                                        t.SESSION_ID == psession_id &&
                                                        t.TERMINAL_IP == pterminal_ip &&
                                                        t.FUNCTION_TYPE == function_type &&
                                                        t.PN == ptoken1 &&
                                                        t.PNE == ptoken2 &&
                                                        t.ACTIVE_FLAG == 1 &&
                                                        t.APPLICATION_ID == papp_id).SingleOrDefault();


                if (OBJ_TOKEN_GEN_LOG != null)
                {
                    OBJ_TOKEN_GEN_LOG.VERI_FLAG = 1;
                    OBJ_TOKEN_GEN_LOG.VERI_TIME = System.DateTime.Now;
                    OBJ_TOKEN_GEN_LOG.VERI_RESULT = 0;
                    OBJ_TOKEN_GEN_LOG.FAIL_REASON_ID = 2;
                    Obj_DBModelEntities.SaveChanges();
                }
                return "1";
            }
            catch (Exception ex)
            {
               
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "UpdateTokenGenLogForDestinationAccMismatch",
                                                  "0000000000", ex.Message, inner4, ex.StackTrace, psession_id, dateTime);

                return "0";
            }
        }
        public static string UpdateTokenGenLogForAmountMismatch(string puser_id, string psession_id, string pterminal_ip, string pfunction_type, string ptoken1, string ptoken2, string papp_id)
        {
            try
            {
                DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);

                int function_type = Convert.ToInt16(pfunction_type);
                var OBJ_TOKEN_GEN_LOG = Obj_DBModelEntities.LG_2FA_TOKEN_GEN_LOG
                                            .Where(t => t.USER_ID == puser_id &&
                                                        t.SESSION_ID == psession_id &&
                                                        t.TERMINAL_IP == pterminal_ip &&
                                                        t.FUNCTION_TYPE == function_type &&
                                                        t.PN == ptoken1 &&
                                                        t.PNE == ptoken2 &&
                                                        t.ACTIVE_FLAG == 1 &&
                                                        t.APPLICATION_ID == papp_id).SingleOrDefault();


                if (OBJ_TOKEN_GEN_LOG != null)
                {
                    OBJ_TOKEN_GEN_LOG.VERI_FLAG = 1;
                    OBJ_TOKEN_GEN_LOG.VERI_TIME = System.DateTime.Now;
                    OBJ_TOKEN_GEN_LOG.VERI_RESULT = 0;
                    OBJ_TOKEN_GEN_LOG.FAIL_REASON_ID = 4;
                    Obj_DBModelEntities.SaveChanges();
                }
                return "1";
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "UpdateTokenGenLogForAmountMismatch",
                                                  "0000000000", ex.Message, inner4, ex.StackTrace, psession_id, dateTime);

                return "0";
            }
        }
        public static string UpdateTokenGenLogForDestinationBillIdMismatch(string puser_id, string psession_id, string pterminal_ip, string pfunction_type, string ptoken1, string ptoken2, string papp_id)
        {
            try
            {
                DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);

                int function_type = Convert.ToInt16(pfunction_type);
                var OBJ_TOKEN_GEN_LOG = Obj_DBModelEntities.LG_2FA_TOKEN_GEN_LOG
                                            .Where(t => t.USER_ID == puser_id &&
                                                        t.SESSION_ID == psession_id &&
                                                        t.TERMINAL_IP == pterminal_ip &&
                                                        t.FUNCTION_TYPE == function_type &&
                                                        t.PN == ptoken1 &&
                                                        t.PNE == ptoken2 &&
                                                        t.ACTIVE_FLAG == 1 &&
                                                        t.APPLICATION_ID == papp_id).SingleOrDefault();


                if (OBJ_TOKEN_GEN_LOG != null)
                {
                    OBJ_TOKEN_GEN_LOG.VERI_FLAG = 1;
                    OBJ_TOKEN_GEN_LOG.VERI_TIME = System.DateTime.Now;
                    OBJ_TOKEN_GEN_LOG.VERI_RESULT = 0;
                    OBJ_TOKEN_GEN_LOG.FAIL_REASON_ID = 3;
                    Obj_DBModelEntities.SaveChanges();
                }
                return "1";
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "UpdateTokenGenLogForDestinationBillIdMismatch",
                                                  "0000000000", ex.Message, inner4, ex.StackTrace, psession_id, dateTime);

                return "0";
            }
        }
        public static string UpdateTokenGenLogForSuccessfulToken(string puser_id, string psession_id, string pterminal_ip, string pfunction_type, string ptoken1, string ptoken2, string papp_id)
        {
            try
            {
                DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                int function_type = Convert.ToInt16(pfunction_type);
                var OBJ_TOKEN_GEN_LOG = Obj_DBModelEntities.LG_2FA_TOKEN_GEN_LOG
                                            .Where(t => t.USER_ID == puser_id &&
                                                        t.SESSION_ID == psession_id &&
                                                        t.TERMINAL_IP == pterminal_ip &&
                                                        t.FUNCTION_TYPE == function_type &&
                                                        t.PN == ptoken1 &&
                                                        t.PNE == ptoken2 &&
                                                        t.ACTIVE_FLAG == 1 &&
                                                        t.APPLICATION_ID == papp_id).SingleOrDefault();


                if (OBJ_TOKEN_GEN_LOG != null)
                {
                    OBJ_TOKEN_GEN_LOG.VERI_FLAG = 1;
                    OBJ_TOKEN_GEN_LOG.VERI_TIME = System.DateTime.Now;
                    OBJ_TOKEN_GEN_LOG.VERI_RESULT = 1;
                    OBJ_TOKEN_GEN_LOG.FAIL_REASON_ID = 0;
                    OBJ_TOKEN_GEN_LOG.ACTIVE_FLAG = 0;
                    Obj_DBModelEntities.SaveChanges();
                }
                return "1";
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "UpdateTokenGenLogForSuccessfulToken",
                                                  "0000000000", ex.Message, inner4, ex.StackTrace, psession_id, dateTime);

                return "0";
            }
        }
        public static string UpdateTokenGenLogForCardNoMismatch(string puser_id, string psession_id, string pterminal_ip, string pfunction_type, string ptoken1, string ptoken2, string papp_id)
        {
            try
            {
                DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);

                int function_type = Convert.ToInt16(pfunction_type);
                var OBJ_TOKEN_GEN_LOG = Obj_DBModelEntities.LG_2FA_TOKEN_GEN_LOG
                                            .Where(t => t.USER_ID == puser_id &&
                                                        t.SESSION_ID == psession_id &&
                                                        t.TERMINAL_IP == pterminal_ip &&
                                                        t.FUNCTION_TYPE == function_type &&
                                                        t.PN == ptoken1 &&
                                                        t.PNE == ptoken2 &&
                                                        t.ACTIVE_FLAG == 1 &&
                                                        t.APPLICATION_ID == papp_id).SingleOrDefault();


                if (OBJ_TOKEN_GEN_LOG != null)
                {
                    OBJ_TOKEN_GEN_LOG.VERI_FLAG = 1;
                    OBJ_TOKEN_GEN_LOG.VERI_TIME = System.DateTime.Now;
                    OBJ_TOKEN_GEN_LOG.VERI_RESULT = 0;
                    OBJ_TOKEN_GEN_LOG.FAIL_REASON_ID = 6;
                    Obj_DBModelEntities.SaveChanges();
                }
                return "1";
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "UpdateTokenGenLogForCardNoMismatch",
                                                  "0000000000", ex.Message, inner4, ex.StackTrace, psession_id, dateTime);

                return "0";
            }
        }
        public static string UpdateUserInfo(string puser_id, string pcust_id, string pgroup_id, string pcell_no, string pemail_id, string preg_dt, string psec_cell_no, string papp_id)
        {
            try
            {
                DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                var OBJ_LG_USER_INFO = Obj_DBModelEntities.LG_USER_INFO
                                        .Where(t => t.USER_ID == puser_id && t.APPLICATION_ID == papp_id).SingleOrDefault();


                if (!string.IsNullOrEmpty(pcell_no) && (!string.IsNullOrEmpty(pemail_id)))
                {
                    OBJ_LG_USER_INFO.CUST_ID = pcust_id;
                    OBJ_LG_USER_INFO.GROUP_ID = pgroup_id;
                    OBJ_LG_USER_INFO.CELL_NO = pcell_no;
                    OBJ_LG_USER_INFO.EMAIL_ID = pemail_id;
                    OBJ_LG_USER_INFO.REG_DT = System.DateTime.Now;
                    OBJ_LG_USER_INFO.SEC_CELL_NO = psec_cell_no;
                }
                if (string.IsNullOrEmpty(pcell_no) && (!string.IsNullOrEmpty(pemail_id)))
                {
                    OBJ_LG_USER_INFO.CUST_ID = pcust_id;
                    OBJ_LG_USER_INFO.GROUP_ID = pgroup_id;
                    OBJ_LG_USER_INFO.EMAIL_ID = pemail_id;
                    OBJ_LG_USER_INFO.REG_DT = System.DateTime.Now;
                    OBJ_LG_USER_INFO.SEC_CELL_NO = psec_cell_no;
                }
                if (!string.IsNullOrEmpty(pcell_no) && (string.IsNullOrEmpty(pemail_id)))
                {
                    OBJ_LG_USER_INFO.CUST_ID = pcust_id;
                    OBJ_LG_USER_INFO.GROUP_ID = pgroup_id;
                    OBJ_LG_USER_INFO.CELL_NO = pcell_no;
                    OBJ_LG_USER_INFO.REG_DT = System.DateTime.Now;
                    OBJ_LG_USER_INFO.SEC_CELL_NO = psec_cell_no;
                }

                Obj_DBModelEntities.SaveChanges();
                return "1";
            }
            catch (Exception ex)
            {
                return "0";
            }
        }
        #endregion

        #region Delete
        public static string DeleteOtpConfig(string pAPPLICATION_ID)
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "OTP").Select(x => x.FUNCTION_ID).SingleOrDefault();
            string result = string.Empty;
            try
            {
                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                LG_2FA_OTP_CONFIG OBJ_LG_2FA_OTP_CONFIG = (from otp in Obj_DBModelEntities.LG_2FA_OTP_CONFIG
                                                           where otp.APPLICATION_ID == pAPPLICATION_ID
                                                           select otp).SingleOrDefault();
                if (OBJ_LG_2FA_OTP_CONFIG != null)
                {
                    Obj_DBModelEntities.LG_2FA_OTP_CONFIG.Remove(OBJ_LG_2FA_OTP_CONFIG);
                    Obj_DBModelEntities.SaveChanges();


                    #region Auth log
                    LG_AA_NFT_AUTH_LOG_MAP OBJ_LG_AA_NFT_AUTH_LOG_MAP = new LG_AA_NFT_AUTH_LOG_MAP();
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.FUNCTION_ID = "1";
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_NAME = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "OTP Configuration").Select(x => x.FUNCTION_ID).SingleOrDefault();
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_NM = "OTP_ID";
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_VAL = OBJ_LG_2FA_OTP_CONFIG.OTP_ID;
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
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.MAKE_BY = "salekin";
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.MAKE_DT = DateTime.Now;
                    LG_AA_NFT_AUTH_LOG_MAP.AddNftAuthLog(OBJ_LG_2FA_OTP_CONFIG, OBJ_LG_AA_NFT_AUTH_LOG_MAP);
                    #endregion


                    result = "True";
                    return result;
                }
                else
                    return "Can't delete, Please try again.";
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
                        result = "Can't Delete OTP Config(Db) " + validationError.ErrorMessage;
                    }
                }

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "DeleteOtpConfig",
                                                    "0000000000", dbEx.Message, inner, dbEx.StackTrace, null, dateTime);

                return result;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "DeleteOtpConfig",
                                                 "0000000000", ex.Message, inner4, ex.StackTrace, null, dateTime);

                result = "Can't Delete OTP Config.";
                return result;
            }
        }
        #endregion

        #region Fetch Single
        public static LG_2FA_OTP_CONFIG_MAP GetOtpConfigByAppId(string pAPPLICATION_ID)
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            LG_2FA_OTP_CONFIG_MAP OBJ_LG_2FA_OTP_CONFIG_MAP = new LG_2FA_OTP_CONFIG_MAP();

            try
            {
                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                OBJ_LG_2FA_OTP_CONFIG_MAP = (from otp in Obj_DBModelEntities.LG_2FA_OTP_CONFIG
                                             join a in Obj_DBModelEntities.LG_FNR_APPLICATION
                                             on otp.APPLICATION_ID equals a.APPLICATION_ID
                                             where otp.APPLICATION_ID == pAPPLICATION_ID
                                             select new LG_2FA_OTP_CONFIG_MAP
                                             {
                                                 APPLICATION_ID = otp.APPLICATION_ID,
                                                 APPLICATION_NAME = a.APPLICATION_NAME,
                                                 MAIL_FLAG = otp.MAIL_FLAG,
                                                 SMS_FLAG = otp.SMS_FLAG,
                                                 VALIDITY_PERIOD = otp.VALIDITY_PERIOD,
                                                 NO_OF_OTP_DIGIT = otp.NO_OF_OTP_DIGIT,
                                                 OTP_FORMAT_ID = otp.OTP_FORMAT_ID,
                                                 MAKE_DT = a.MAKE_DT
                                             }).SingleOrDefault();
                BooleanConversion.LG_2FA_OTP_CONFIG_MAP_INT_TO_BOOL(OBJ_LG_2FA_OTP_CONFIG_MAP);
                OBJ_LG_2FA_OTP_CONFIG_MAP.APPLICATION_LIST_FOR_DD = DropDown.GetApplicationForDD();
                OBJ_LG_2FA_OTP_CONFIG_MAP.OTP_FORMATE_LIST_FOR_DD = DropDown.GetOTPFormatForDD();
                return OBJ_LG_2FA_OTP_CONFIG_MAP;
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
                        OBJ_LG_2FA_OTP_CONFIG_MAP.ERROR = validationError.ErrorMessage;
                    }
                }

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "GetOtpConfigByAppId",
                                                    "0000000000", dbEx.Message, inner, dbEx.StackTrace, null, dateTime);

                return OBJ_LG_2FA_OTP_CONFIG_MAP;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "GetOtpConfigByAppId",
                                                 "0000000000", ex.Message, inner4, ex.StackTrace, null, dateTime);

                OBJ_LG_2FA_OTP_CONFIG_MAP.ERROR = ex.Message;
                return OBJ_LG_2FA_OTP_CONFIG_MAP;
            }
        }
        public static string GetCustCellNo(string puser_id, string papp_id)
        {
            try
            {
                DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);

                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                var Obj_Cust_Cell_No = (from u in Obj_DBModelEntities.LG_USER_INFO
                                        where u.USER_ID == puser_id
                                        select new { u.USER_ID, u.CELL_NO, u.SEC_CELL_NO }).SingleOrDefault();

                string vResult = "0";
                if (!string.IsNullOrEmpty(Obj_Cust_Cell_No.USER_ID))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(Obj_Cust_Cell_No.USER_ID);
                    sb.Append(',');
                    sb.Append(Obj_Cust_Cell_No.CELL_NO);
                    sb.Append(',');
                    sb.Append(Obj_Cust_Cell_No.SEC_CELL_NO);
                    vResult = sb.ToString();
                    return vResult;
                }
                else
                    return vResult;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "GetCustCellNo",
                                                  "0000000000", ex.Message, inner4, ex.StackTrace, null, dateTime);

                return "0";
            }


        }
        public static LG_2FA_TOKEN_GEN_LOG GetActiveTokenGenLogForSpecificUser(string puser_id, string psession_id, string pterminal_ip, string pfunction_type, string ptoken1, string ptoken2, string papp_id)
        {
            LG_2FA_TOKEN_GEN_LOG OBJ_LG_2FA_TOKEN_GEN_LOG = null;
            try
            {
                DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);

                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                int function_type = Convert.ToInt16(pfunction_type);
                OBJ_LG_2FA_TOKEN_GEN_LOG = Obj_DBModelEntities.LG_2FA_TOKEN_GEN_LOG
                                           .Where(t => t.USER_ID == puser_id &&
                                           t.SESSION_ID == psession_id &&
                                           t.TERMINAL_IP == pterminal_ip &&
                                           t.FUNCTION_TYPE == function_type &&
                                           t.PN == ptoken1 &&
                                           t.PNE == ptoken2 &&
                                           t.ACTIVE_FLAG == 1 &&
                                           t.APPLICATION_ID == papp_id).SingleOrDefault();

                return OBJ_LG_2FA_TOKEN_GEN_LOG;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);
                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "GetActiveTokenGenLogForSpecificUser",
                                                  "0000000000", ex.Message, inner4, ex.StackTrace, psession_id, dateTime);

                return OBJ_LG_2FA_TOKEN_GEN_LOG;
            }

        }
        public static string GetUserMobileAndEmail(string puser_id)
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            var user_info = (Obj_DBModelEntities.LG_USER_SETUP_PROFILE
                           .Where(u => u.USER_ID == puser_id)
                           .Select(u => new { u.USER_ID, u.MOB_NO, u.MAIL_ADDRESS })).SingleOrDefault();

            string MOB_NO = user_info.MOB_NO;
            string MAIL_ADDRESS = user_info.MAIL_ADDRESS;

            if (CheckifEncypted(user_info.MOB_NO)) //true means encrypted
            {
                MOB_NO = Security.GetPlainText(user_info.MOB_NO);
            }
            if(CheckifEncypted(user_info.MAIL_ADDRESS))
            {
                MAIL_ADDRESS = Security.GetPlainText(user_info.MAIL_ADDRESS);
            }

            string vResult = "0";
            if (!string.IsNullOrEmpty(user_info.USER_ID))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(user_info.USER_ID);
                sb.Append(',');
                sb.Append(MOB_NO);
                sb.Append(',');
                sb.Append(MAIL_ADDRESS);
                vResult = sb.ToString();
                return vResult;
            }
            else
                return vResult;
        }
        public static bool CheckifEncypted(string pvalue)
        {
            //when value is encrypted
            if (pvalue[pvalue.Length - 1] == '=')  //lass character of pvalue
            {
                return true;
            }
            //when value is not encrypted
            else
                return false;
        }
        #endregion

        #region Fetch all
        public static IEnumerable<LG_2FA_OTP_CONFIG_MAP> GetOtpConfig()
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            List<LG_2FA_OTP_CONFIG_MAP> LIST_LG_2FA_OTP_CONFIG_MAP = null;
            LG_2FA_OTP_CONFIG_MAP OBJ_LG_2FA_OTP_CONFIG_MAP = new LG_2FA_OTP_CONFIG_MAP();

            try
            {
                Obj_DBModelEntities.Configuration.LazyLoadingEnabled = false;
                Obj_DBModelEntities.Configuration.ProxyCreationEnabled = false;

                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);


                LIST_LG_2FA_OTP_CONFIG_MAP = (from otp in Obj_DBModelEntities.LG_2FA_OTP_CONFIG
                                              join a in Obj_DBModelEntities.LG_FNR_APPLICATION
                                              on otp.APPLICATION_ID equals a.APPLICATION_ID
                                              where otp.AUTH_STATUS_ID !="U" && otp.LAST_ACTION!="DEL"
                                              orderby a.APPLICATION_NAME ascending
                                              select new LG_2FA_OTP_CONFIG_MAP
                                              {
                                                  APPLICATION_ID = otp.APPLICATION_ID,
                                                  APPLICATION_NAME = a.APPLICATION_NAME,
                                                  MAIL_FLAG = otp.MAIL_FLAG,
                                                  SMS_FLAG = otp.SMS_FLAG,
                                                  VALIDITY_PERIOD = otp.VALIDITY_PERIOD,
                                                  NO_OF_OTP_DIGIT = otp.NO_OF_OTP_DIGIT,
                                                  OTP_FORMAT_ID = otp.OTP_FORMAT_ID,
                                                  MAKE_DT = otp.MAKE_DT,
                                                  OTP_ID = otp.OTP_ID,
                                                  AUTH_STATUS_ID = otp.AUTH_STATUS_ID
                                              }).ToList();
                return LIST_LG_2FA_OTP_CONFIG_MAP;
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
                        OBJ_LG_2FA_OTP_CONFIG_MAP.ERROR = validationError.ErrorMessage;
                        LIST_LG_2FA_OTP_CONFIG_MAP.Add(OBJ_LG_2FA_OTP_CONFIG_MAP);
                    }
                }

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "GetOtpConfig",
                                                  "0000000000", dbEx.Message, inner, dbEx.StackTrace, null, dateTime);

                return LIST_LG_2FA_OTP_CONFIG_MAP;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);
                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "GetOtpConfig",
                                                 "0000000000", ex.Message, inner4, ex.StackTrace, null, dateTime);

                OBJ_LG_2FA_OTP_CONFIG_MAP.ERROR = ex.Message;
                LIST_LG_2FA_OTP_CONFIG_MAP.Add(OBJ_LG_2FA_OTP_CONFIG_MAP);
                return LIST_LG_2FA_OTP_CONFIG_MAP;
            }
        }
        public static IEnumerable<LG_USER_INFO> GetUserInfoByUserId(string puser_id, string papp_id)
        {
            IEnumerable<LG_USER_INFO> LIST_LG_USER_INFO = null;
            try
            {
                DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);

                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                LIST_LG_USER_INFO = Obj_DBModelEntities.LG_USER_INFO
                                     .Where(u => u.USER_ID == puser_id && u.APPLICATION_ID == papp_id).ToList();

                return LIST_LG_USER_INFO;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "GetUserInfoByUserId",
                                                 "0000000000", ex.Message, inner4, ex.StackTrace, null, dateTime);

                return LIST_LG_USER_INFO;
            }
        }
        #endregion

        #endregion
    }
}
