using Model.EDMX;
using Model.EntityModel.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EntityModel.LGModel;
using System.Runtime.Serialization;
using System.Configuration;

namespace Model.EntityModel.LGModel
{
    [DataContract]
    public class LG_CRD_PASSWORD_MODIFY_MAP
    {
        #region Property

        public string SL_NO { get; set; }

        public string USER_ID { get; set; }

        public string APPLICATION_ID { get; set; }

        public string APPLICATION_NAME { get; set; }

        public string PASSWORD_STRING { get; set; }

        public Nullable<System.DateTime> MAKE_DT { get; set; }

        #endregion Property

        #region Events

        #region Change Password

        public static string ChangePassword(string pAPPLICATION_ID, string pUSER_ID, string pNEW_PASSWORD, string pCURRENT_PASSWORD)
        {
            string SMS_TEXT = ConfigurationManager.AppSettings["SMS_TEXT_FOR_PASSWORD_RESET"].ToString() + pNEW_PASSWORD;

            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            LG_CRD_PASSWORD_POLICY OBJ_LG_CRD_PASSWORD_POLICY = new LG_CRD_PASSWORD_POLICY();
            LG_USER_SETUP_PROFILE OBJ_LG_USER_SETUP_PROFILE = new LG_USER_SETUP_PROFILE();
            List<LG_CRD_PASSWORD_MODIFY_MAP> LIST_LG_CRD_PASSWORD_MODIFY_MAP = new List<LG_CRD_PASSWORD_MODIFY_MAP>();

            string result = string.Empty;
            string FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "PasswordChange").Select(x => x.FUNCTION_ID).SingleOrDefault();

            result = LG_CRD_PASSWORD_POLICY_MAP.ValidatePasswordPolicyOnCreation(pAPPLICATION_ID, pNEW_PASSWORD);
            //pCURRENT_PASSWORD = Pass_Encryp.MD5Hash(pCURRENT_PASSWORD);
            //pNEW_PASSWORD = Pass_Encryp.MD5Hash(pNEW_PASSWORD);

            pNEW_PASSWORD = Security.GetEncryptedText(pNEW_PASSWORD);

            try
            {
                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                OBJ_LG_USER_SETUP_PROFILE = Obj_DBModelEntities.LG_USER_SETUP_PROFILE
                                            .Where(a => a.USER_ID == pUSER_ID).SingleOrDefault();

                if (OBJ_LG_USER_SETUP_PROFILE == null)
                {
                    return "User doesn't exists.";
                }

                string current_pass = Security.GetPlainText(OBJ_LG_USER_SETUP_PROFILE.PASSWORD);

                if (current_pass == pCURRENT_PASSWORD)
                {
                    if (result == "Valid")
                    {
                        // get all the encrypted password from password history log by user_id

                        LIST_LG_CRD_PASSWORD_MODIFY_MAP = (from a in Obj_DBModelEntities.LG_USER_PASS_HISTORY
                                                           where a.USER_ID == pUSER_ID
                                                           select new LG_CRD_PASSWORD_MODIFY_MAP
                                                           {
                                                               SL_NO = a.SL_NO,
                                                               USER_ID = a.USER_ID,
                                                               APPLICATION_ID = a.APPLICATION_ID,
                                                               APPLICATION_NAME = a.APPLICATION_NAME,
                                                               PASSWORD_STRING = a.PASSWORD_STRING,
                                                               MAKE_DT = a.MAKE_DT,
                                                           }).ToList();

                        OBJ_LG_CRD_PASSWORD_POLICY = (from p in Obj_DBModelEntities.LG_CRD_PASSWORD_POLICY
                                                      where p.APPLICATION_ID == pAPPLICATION_ID
                                                      select p).First();

                        if (LIST_LG_CRD_PASSWORD_MODIFY_MAP.Count != 0)
                        {
                            //Prevent Repeatation Of Password

                            var lastChange = LIST_LG_CRD_PASSWORD_MODIFY_MAP.OrderByDescending(t => t.MAKE_DT).First();

                            string decryptedLastPass = lastChange.PASSWORD_STRING;
                            // string decryptedLastPass = Security.GetPlainText(lastChange.PASSWORD_STRING);

                            if (decryptedLastPass == pNEW_PASSWORD)
                            {
                                result = "You can't repeat your last password.";
                                return result;
                            }

                            //Prevent Reuse Of Password

                            string decryptedPass = "";
                            int passReused = 0;

                            foreach (var item in LIST_LG_CRD_PASSWORD_MODIFY_MAP)
                            {
                                decryptedPass = item.PASSWORD_STRING;

                                if (decryptedPass == pNEW_PASSWORD)
                                {
                                    passReused++;
                                }
                            }

                            if (OBJ_LG_CRD_PASSWORD_POLICY != null)
                            {
                                if (Convert.ToInt16(OBJ_LG_CRD_PASSWORD_POLICY.PASS_REUSE_MAX) <= passReused)
                                {
                                    string template = "Same password shouldn't be reused more than {0} times.";
                                    string data = OBJ_LG_CRD_PASSWORD_POLICY.PASS_REUSE_MAX.ToString();
                                    result = string.Format(template, data);
                                    return result;
                                }
                            }
                        }

                        // Password change/update query

                        OBJ_LG_USER_SETUP_PROFILE = (from p in Obj_DBModelEntities.LG_USER_SETUP_PROFILE
                                                     where p.USER_ID == pUSER_ID
                                                     select p).First();

                        if (OBJ_LG_USER_SETUP_PROFILE.USER_NM == pNEW_PASSWORD)
                        {
                            result = "Password can't be same as user name.";
                            return result;
                        }

                        // Store Old password to history log

                        string encryptedPassword = OBJ_LG_USER_SETUP_PROFILE.PASSWORD;

                        LG_CRD_PASSWORD_MODIFY_MAP OBJ_LG_CRD_PASSWORD_MODIFY_MAP = new LG_CRD_PASSWORD_MODIFY_MAP();

                        string logResponse = OBJ_LG_CRD_PASSWORD_MODIFY_MAP.StorePassword(pUSER_ID, encryptedPassword, pAPPLICATION_ID);

                        if (logResponse != "True")
                        {
                            result = logResponse;
                            return result;
                        }

                        OBJ_LG_USER_SETUP_PROFILE.PASSWORD = pNEW_PASSWORD;
                        OBJ_LG_USER_SETUP_PROFILE.LAST_UPDATE_DT = System.DateTime.Now;
                        OBJ_LG_USER_SETUP_PROFILE.FIRST_LOGIN_FLAG = 0;
                        Obj_DBModelEntities.SaveChanges();



                        if (ConfigurationManager.AppSettings["SMS__NOTIFICATION_ENABLE"].ToString() == "1")
                        {
                            if (SMS_API.PushSMS(OBJ_LG_USER_SETUP_PROFILE.MOB_NO, SMS_TEXT))
                            {
                                result = "True";
                            }
                            else
                            {
                                result = "False";
                            }
                        }
                        else
                            result = "True";





                        //var query = from LG_SYS_MAIL_SERVER_CONFIG in Obj_DBModelEntities.LG_SYS_MAIL_SERVER_CONFIG
                        //            join LG_FNR_APPLICATION in Obj_DBModelEntities.LG_FNR_APPLICATION
                        //              on LG_SYS_MAIL_SERVER_CONFIG.APPLICATION_ID equals LG_FNR_APPLICATION.APPLICATION_ID
                        //            join LG_USER_ROLE_ASSIGN in Obj_DBModelEntities.LG_USER_ROLE_ASSIGN
                        //              on LG_FNR_APPLICATION.APPLICATION_ID equals LG_USER_ROLE_ASSIGN.APPLICATION_ID
                        //            where LG_USER_ROLE_ASSIGN.APPLICATION_ID == pAPPLICATION_ID
                        //            select new
                        //            {
                        //                LG_SYS_MAIL_SERVER_CONFIG.MAIL_ID

                        //            };

                        return result;
                    }
                    else
                    {
                        return result;
                    }
                }
                else
                {
                    return "Incorrect Password.";
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
                        result = "Can't Change Password(Db).";
                    }
                }

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "ChangePassword",
                                              "0000000000", dbEx.Message, inner, dbEx.StackTrace, pUSER_ID, dateTime);

                return result;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "ChangePassword",
                                              "0000000000", ex.Message, inner4, ex.StackTrace, pUSER_ID, dateTime);

                result = "Can't Change Password";
                return result;
            }
        }

        #endregion Change Password

        #region Encrypt password

        public static string EncryptPassword()
        {
            string pNEW_PASSWORD;
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            LG_CRD_PASSWORD_POLICY OBJ_LG_CRD_PASSWORD_POLICY = new LG_CRD_PASSWORD_POLICY();
            LG_USER_SETUP_PROFILE OBJ_LG_USER_SETUP_PROFILE = new LG_USER_SETUP_PROFILE(); List<LG_USER_SETUP_PROFILE_MAP> LIST_LG_USER_SETUP_PROFILE_MAP = new List<LG_USER_SETUP_PROFILE_MAP>();

            try
            {
                LIST_LG_USER_SETUP_PROFILE_MAP = (from c in Obj_DBModelEntities.LG_USER_SETUP_PROFILE
                                                  select new
                                                  LG_USER_SETUP_PROFILE_MAP
                                                  {
                                                      USER_ID = c.USER_ID,
                                                      PASSWORD = c.PASSWORD
                                                  }).ToList();
                foreach (var Userinfo in LIST_LG_USER_SETUP_PROFILE_MAP)
                {
                    var Is_MD5 = IsValidMD5(Userinfo.PASSWORD);
                    if (Is_MD5 == false)
                    {
                        pNEW_PASSWORD = Pass_Encryp.MD5Hash(Userinfo.PASSWORD);
                        OBJ_LG_USER_SETUP_PROFILE = (from p in Obj_DBModelEntities.LG_USER_SETUP_PROFILE
                                                     where p.USER_ID == Userinfo.USER_ID
                                                     select p).First();
                        OBJ_LG_USER_SETUP_PROFILE.PASSWORD = pNEW_PASSWORD;
                        Obj_DBModelEntities.SaveChanges();
                    }
                }
                return "Password Encryption Successfull";
            }
            catch (Exception ex)
            {
                return "Password Encryption Failed";
            }
        }

        #endregion Encrypt password

        #region md5 validate

        private static bool IsValidMD5(string md5)
        {
            if (md5 == null || md5.Length != 32) return false;
            foreach (var x in md5)
            {
                if ((x < '0' || x > '9') && (x < 'a' || x > 'f') && (x < 'A' || x > 'F'))
                {
                    return false;
                }
            }
            return true;
        }

        #endregion md5 validate

        #region ResetPassword

        public static string ResetPassword(string p_user_id, string pUSER_ID, string pAPPLICATION_ID)
        {
            // p_user_id will be stored in authorize log



            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            DBModelEntities Obj_DBModelEntities1 = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);

            LG_USER_SETUP_PROFILE OBJ_LG_USER_SETUP_PROFILE = new LG_USER_SETUP_PROFILE();
            LG_USER_SETUP_PROFILE OBJ_LG_USER_SETUP_PROFILE_OLD = new LG_USER_SETUP_PROFILE();

            string result = string.Empty;
            string FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "PasswordReset").Select(x => x.FUNCTION_ID).SingleOrDefault();
            try
            {
                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                OBJ_LG_USER_SETUP_PROFILE = Obj_DBModelEntities.LG_USER_SETUP_PROFILE
                                            .Where(a => a.USER_ID == pUSER_ID).SingleOrDefault();

                OBJ_LG_USER_SETUP_PROFILE_OLD = Obj_DBModelEntities1.LG_USER_SETUP_PROFILE
                                            .Where(a => a.USER_ID == pUSER_ID).SingleOrDefault();

                if (OBJ_LG_USER_SETUP_PROFILE == null)
                {
                    return "User doesn't exists.";
                }
                else
                {
                    // Random generated password creation and update in db

                    //       string pNEW_PASSWORD = System.Web.Security.Membership.GeneratePassword(10,0);

                    // another custom random string generator
                    //int length = 12;

                    //const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
                    //StringBuilder res = new StringBuilder();
                    //Random rnd = new Random();
                    //while (0 < length--)
                    //{
                    //    res.Append(valid[rnd.Next(valid.Length)]);
                    //}
                    //string Pass = System.Web.Security.Membership.GeneratePassword(6, 0);
                    //string pNEW_PASSWORD = Pass;

                    //string encryptedPassword = Pass_Encryp.MD5Hash(pNEW_PASSWORD);

                    string encryptedPassword = Security.GetEncryptedText();
                    string Pass = Security.GetPlainText(encryptedPassword);

                    // Password change/update query

                    OBJ_LG_USER_SETUP_PROFILE = (from p in Obj_DBModelEntities.LG_USER_SETUP_PROFILE
                                                 where p.USER_ID == pUSER_ID
                                                 select p).First();

                    OBJ_LG_USER_SETUP_PROFILE.PASSWORD = encryptedPassword;
                    OBJ_LG_USER_SETUP_PROFILE.AUTH_STATUS_ID = "U";
                    OBJ_LG_USER_SETUP_PROFILE.LAST_ACTION = "EDT";
                    OBJ_LG_USER_SETUP_PROFILE.LAST_UPDATE_DT = System.DateTime.Now;
                    OBJ_LG_USER_SETUP_PROFILE.FIRST_LOGIN_FLAG = 1;
                    Obj_DBModelEntities.SaveChanges();

                    if (ConfigurationManager.AppSettings["SMS__NOTIFICATION_ENABLE"].ToString() == "1")
                    {
                        string SMS_TEXT = ConfigurationManager.AppSettings["SMS_TEXT_FOR_PASSWORD_RESET"].ToString() + Pass;
                        SMS_API.PushSMS(OBJ_LG_USER_SETUP_PROFILE.MOB_NO, SMS_TEXT);
                    }


                    // string encryptedPassword = Security.GetEncryptedText(OBJ_LG_USER_SETUP_PROFILE.PASSWORD);

                    LG_CRD_PASSWORD_MODIFY_MAP OBJ_LG_CRD_PASSWORD_MODIFY_MAP = new LG_CRD_PASSWORD_MODIFY_MAP();

                    string logResponse = OBJ_LG_CRD_PASSWORD_MODIFY_MAP.StorePassword(pUSER_ID, encryptedPassword, pAPPLICATION_ID);

                    #region Auth log

                    LG_AA_NFT_AUTH_LOG_MAP OBJ_LG_AA_NFT_AUTH_LOG_MAP = new LG_AA_NFT_AUTH_LOG_MAP();
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "PasswordReset").Select(x => x.FUNCTION_ID).SingleOrDefault();
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_NAME = "LG_USER_SETUP_PROFILE";
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_NM = "USER_ID";
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_VAL = OBJ_LG_USER_SETUP_PROFILE.USER_ID;
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
                    LG_AA_NFT_AUTH_LOG_MAP.AddNftAuthLog(OBJ_LG_USER_SETUP_PROFILE_OLD, OBJ_LG_USER_SETUP_PROFILE, OBJ_LG_AA_NFT_AUTH_LOG_MAP);

                    #endregion Auth log

                    result = "True";

                    //try
                    //{
                    //    //string email_id = from c in Obj_DBModelEntities.LG_SYS_MAIL_SERVER_CONFIG
                    //    //               where c.APPLICATION_ID == pAPPLICATION_ID
                    //    //               select c.MAIL_ID;
                    //    string email_id = Obj_DBModelEntities.LG_USER_SETUP_PROFILE.FirstOrDefault(m => m.USER_ID == pUSER_ID).MAIL_ADDRESS;

                    //    bool res1 = Convert.ToBoolean(result);
                    //    if (res1 != false)
                    //    {
                    //        EntityModel.TwoFactorAuth.OTP.SendEmailAfterPasswordChange(pUSER_ID, email_id.ToString(), Pass, pAPPLICATION_ID);
                    //    }
                    //}
                    //catch (Exception ex)
                    //{
                    //    Console.Write(ex.Message + ex.StackTrace);
                    //}

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
                        result = "Can't Reset Password(Db).";
                    }
                }

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "ResetPassword",
                     "0000000000", dbEx.Message, inner, dbEx.StackTrace, pUSER_ID, dateTime);

                return result;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "ResetPassword",
                       "0000000000", ex.Message, inner4, ex.StackTrace, pUSER_ID, dateTime);

                result = "Can't Reset Password";
                return result;
            }
        }

        #endregion ResetPassword

        #region Password History Log

        public string StorePassword(string pUSER_ID, string pPASSWORD, string pAPPLICATION_ID)
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            string result = string.Empty;

            try
            {
                //int id = Convert.ToInt32(Obj_DBModelEntities.LG_USER_PASS_HISTORY
                //                        .Max(x => x.SL_NO)) + 1;

                int id = (Obj_DBModelEntities.LG_USER_PASS_HISTORY
                         .Select(i => i.SL_NO).Cast<int?>().Max() ?? 0) + 1;

                LG_USER_PASS_HISTORY OBJ_LG_USER_PASS_HISTORY = new LG_USER_PASS_HISTORY();
                OBJ_LG_USER_PASS_HISTORY.APPLICATION_ID = pAPPLICATION_ID;
                OBJ_LG_USER_PASS_HISTORY.APPLICATION_NAME = Obj_DBModelEntities.LG_FNR_APPLICATION.Where(a => a.APPLICATION_ID == pAPPLICATION_ID).Select(a => a.APPLICATION_NAME).FirstOrDefault();
                OBJ_LG_USER_PASS_HISTORY.USER_ID = pUSER_ID;
                OBJ_LG_USER_PASS_HISTORY.PASSWORD_STRING = pPASSWORD;
                OBJ_LG_USER_PASS_HISTORY.SL_NO = id.ToString();
                OBJ_LG_USER_PASS_HISTORY.MAKE_DT = System.DateTime.Now;
                Obj_DBModelEntities.LG_USER_PASS_HISTORY.Add(OBJ_LG_USER_PASS_HISTORY);
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
                        result = "Can't store password to history log(Db) " + validationError.ErrorMessage;
                    }
                }

                LG_ERROR_LOG_MAP.Add_Error_Log(null, dbEx.Source, "ERR_APP_TYPE", "StorePassword",
                                              "0000000000", dbEx.Message, inner, dbEx.StackTrace, pUSER_ID, dateTime);

                return result;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                LG_ERROR_LOG_MAP.Add_Error_Log(null, ex.Source, "ERR_APP_TYPE", "StorePassword",
                                              "0000000000", ex.Message, inner4, ex.StackTrace, pUSER_ID, dateTime);

                result = "Can't store password to history log";
                return result;
            }
        }

        #endregion Password History Log

        #region random password

        private string Pass = System.Web.Security.Membership.GeneratePassword(12, 6);

        #endregion random password

        #endregion Events
    }
}