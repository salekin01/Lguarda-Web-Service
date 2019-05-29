using ActiveDirectory;
using Model.EDMX;
using Model.EntityModel.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.DirectoryServices.AccountManagement;

namespace Model.EntityModel.LGModel
{
    public class LG_SYS_LOGIN
    {
        public static string VerifyUserAndPasswordForLogin(string puser_id, string pPassword, string pSessionID, string pIPaddress, string pApplicationID)
        {
            string out_result = "0";
            string user_type = string.Empty;
            string pDOMAIN_NAME = string.Empty;
            string pD_ENABLE_FLAG = string.Empty;
            bool is_aduser = false;
            try
            {

                pD_ENABLE_FLAG = ConfigurationManager.AppSettings["AD_ENABLE_FLAG"].ToString();
                if (!string.IsNullOrWhiteSpace(pD_ENABLE_FLAG) && pD_ENABLE_FLAG == "1")
                {
                    #region AD Check

                    pDOMAIN_NAME = ConfigurationManager.AppSettings["DOMAIN_NAME"].ToString();
                   // is_aduser = ADVerification.DomainUserCheck(puser_id, pPassword, pDOMAIN_NAME);

                    using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, pDOMAIN_NAME))
                    {
                        // validate the credentials
                        is_aduser = pc.ValidateCredentials(puser_id, pPassword);
                    }
                    if (is_aduser == true)
                    {
                        DBModelEntities Obj_DBModelEntities1 = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
                        LG_USER_AD_BINDING OBJ_LG_USER_AD_BINDING = Obj_DBModelEntities1.LG_USER_AD_BINDING
                                                                   .Where(m => m.DOMAIN_ID == puser_id &&
                                                                               m.AD_ACTIVE_FLAG == 1 &&
                                                                               m.AUTH_STATUS_ID == "A" &&
                                                                               m.LAST_ACTION != "DEL").SingleOrDefault();
                        if (OBJ_LG_USER_AD_BINDING == null)
                        {
                            return out_result = "7";  //domain user is not binded.
                        }

                        LG_USER_SETUP_PROFILE_MAP OBJ_LG_USER_SETUP_PROFILE_MAP_01 = (from m in Obj_DBModelEntities1.LG_USER_ROLE_ASSIGN
                                                                                      where m.USER_ID == OBJ_LG_USER_AD_BINDING.USER_ID &&
                                                                                            m.APPLICATION_ID == pApplicationID
                                                                                      group m by new { m.APPLICATION_ID, m.USER_ID } into T
                                                                                      select new LG_USER_SETUP_PROFILE_MAP
                                                                                      {
                                                                                          USER_ID = T.Key.USER_ID,
                                                                                      }).FirstOrDefault();

                        if (OBJ_LG_USER_SETUP_PROFILE_MAP_01 == null)
                        {
                            out_result = "6";  //can't find any assigned role
                            return out_result;
                        }

                        LG_USER_SETUP_PROFILE_MAP OBJ_LG_USER_SETUP_PROFILE_MAP = GetUserSetupInfoByUserId(OBJ_LG_USER_AD_BINDING.USER_ID, "1");
                        if (pApplicationID != "01")
                        {
                            if (OBJ_LG_USER_SETUP_PROFILE_MAP.USER_CLASSIFICATION_ID == "1")
                            {
                                user_type = "," + OBJ_LG_USER_SETUP_PROFILE_MAP.USER_CLASSIFICATION_ID + "," + OBJ_LG_USER_SETUP_PROFILE_MAP.USER_AREA_ID_VALUE + "," + OBJ_LG_USER_SETUP_PROFILE_MAP.BRANCH_ID.PadLeft(4, '0') + "," + OBJ_LG_USER_SETUP_PROFILE_MAP.USER_ID;
                            }
                            if (OBJ_LG_USER_SETUP_PROFILE_MAP.USER_CLASSIFICATION_ID == "2")
                            {
                                user_type = "," + OBJ_LG_USER_SETUP_PROFILE_MAP.USER_CLASSIFICATION_ID + "," + OBJ_LG_USER_SETUP_PROFILE_MAP.USER_AREA_ID_VALUE + "," + OBJ_LG_USER_SETUP_PROFILE_MAP.BRANCH_ID.PadLeft(4, '0') + "," + OBJ_LG_USER_SETUP_PROFILE_MAP.USER_ID;
                            }
                            if (OBJ_LG_USER_SETUP_PROFILE_MAP.USER_CLASSIFICATION_ID == "3")
                            {
                                user_type = "," + OBJ_LG_USER_SETUP_PROFILE_MAP.USER_CLASSIFICATION_ID + "," + OBJ_LG_USER_SETUP_PROFILE_MAP.USER_AREA_ID_VALUE + "," + OBJ_LG_USER_SETUP_PROFILE_MAP.BRANCH_ID.PadLeft(4, '0') + "," + OBJ_LG_USER_SETUP_PROFILE_MAP.USER_ID;
                            }
                        }
                        out_result = "1";
                        return out_result = ((out_result + user_type));
                    }

                    #endregion AD Check
                }

                LG_USER_SETUP_PROFILE_MAP v_user_info = new LG_USER_SETUP_PROFILE_MAP();
                LG_USER_SETUP_PROFILE_MAP v_user_info_lock = new LG_USER_SETUP_PROFILE_MAP();

                if (is_aduser == false)
                {
                    v_user_info_lock = Verify_if_lock_User(puser_id);
                }

                if (v_user_info_lock.USER_ID_LOCK_WRNG_ATM == 0)
                {
                    v_user_info = VerifyUserAndPassword(puser_id, pPassword, pApplicationID);

                    if (v_user_info != null)
                    {
                        if (v_user_info.ERROR != null)
                        {
                            if (v_user_info.ERROR.Contains("Invalid"))
                            {
                                return out_result = "0";
                            }
                        }
                        if (v_user_info.ERROR == null || v_user_info.ERROR == "" || v_user_info.ERROR == string.Empty)
                        {
                            LG_USER_SETUP_PROFILE_MAP OBJ_LG_USER_SETUP_PROFILE_MAP = GetUserSetupInfoByUserId(puser_id, "1");

                            if (pApplicationID != "01")
                            {
                                if (OBJ_LG_USER_SETUP_PROFILE_MAP.USER_CLASSIFICATION_ID == "1")
                                {
                                    user_type = "," + OBJ_LG_USER_SETUP_PROFILE_MAP.USER_CLASSIFICATION_ID + "," + OBJ_LG_USER_SETUP_PROFILE_MAP.USER_AREA_ID_VALUE + "," + OBJ_LG_USER_SETUP_PROFILE_MAP.BRANCH_ID.PadLeft(4, '0') + "," + OBJ_LG_USER_SETUP_PROFILE_MAP.USER_ID;
                                }
                                if (OBJ_LG_USER_SETUP_PROFILE_MAP.USER_CLASSIFICATION_ID == "2")
                                {
                                    user_type = "," + OBJ_LG_USER_SETUP_PROFILE_MAP.USER_CLASSIFICATION_ID + "," + OBJ_LG_USER_SETUP_PROFILE_MAP.USER_AREA_ID_VALUE + "," + OBJ_LG_USER_SETUP_PROFILE_MAP.BRANCH_ID.PadLeft(4, '0') + "," + OBJ_LG_USER_SETUP_PROFILE_MAP.USER_ID;
                                }
                                if (OBJ_LG_USER_SETUP_PROFILE_MAP.USER_CLASSIFICATION_ID == "3")
                                {
                                    user_type = "," + OBJ_LG_USER_SETUP_PROFILE_MAP.USER_CLASSIFICATION_ID + "," + OBJ_LG_USER_SETUP_PROFILE_MAP.USER_AREA_ID_VALUE + "," + OBJ_LG_USER_SETUP_PROFILE_MAP.BRANCH_ID.PadLeft(4, '0') + "," + OBJ_LG_USER_SETUP_PROFILE_MAP.USER_ID;
                                }
                            }

                            if (OBJ_LG_USER_SETUP_PROFILE_MAP.ACTIVE_FLAG_MULTI_LOGIN == 1)
                            {
                                LG_USER_SETUP_PROFILE_MAP OBJ_LG_USER_SETUP_PROFILE_MAP_NEW = GetUserSetupInfoByUserId(puser_id, "2"); //1 = session not checked, 2 = session checking

                                if (OBJ_LG_USER_SETUP_PROFILE_MAP_NEW.IP_ADDRESS != pIPaddress)
                                {
                                    out_result = "2";
                                    return out_result;
                                }

                                if (OBJ_LG_USER_SETUP_PROFILE_MAP_NEW.IP_ADDRESS == pIPaddress)
                                {
                                    string pSESSION_CHK = ConfigurationManager.AppSettings["SESSION_CHK"].ToString();
                                    if (pSESSION_CHK == "1")
                                    {
                                        if (OBJ_LG_USER_SETUP_PROFILE_MAP_NEW.USER_SESSION_ID != pSessionID)
                                        {
                                            int current_hr = (System.DateTime.Now).Hour;
                                            int current_min = (System.DateTime.Now).Minute;

                                            if (current_hr > OBJ_LG_USER_SETUP_PROFILE_MAP_NEW.HR)
                                            {
                                                out_result = (("1" + user_type));
                                                return (out_result + user_type);
                                            }
                                            if (current_hr == OBJ_LG_USER_SETUP_PROFILE_MAP_NEW.HR)
                                            {
                                                if ((current_min - OBJ_LG_USER_SETUP_PROFILE_MAP_NEW.MIN) > 2)
                                                {
                                                    out_result = (("1" + user_type));
                                                    return out_result;
                                                }
                                                else
                                                {
                                                    out_result = "2";
                                                    return out_result;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            out_result = "2";
                                            return out_result;
                                        }
                                    }
                                }
                            }

                            if (OBJ_LG_USER_SETUP_PROFILE_MAP.ACTIVE_FLAG_INACTV_USER == 0)
                            {
                                out_result = "3";
                                return out_result;
                            }

                            if (OBJ_LG_USER_SETUP_PROFILE_MAP.FIRST_LOGIN_FLAG == 1)
                            {
                                out_result = "4";
                                return out_result;
                            }

                            out_result = "1";

                            if (out_result == "1")
                            {
                                Initializesessiontracker(puser_id, pSessionID, pApplicationID, pIPaddress);

                                if (OBJ_LG_USER_SETUP_PROFILE_MAP.ACTIVE_FLAG_MULTI_LOGIN == 0)
                                {
                                    DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);

                                    LG_USER_SETUP_PROFILE OBJ_LG_USER_SETUP_PROFILE = Obj_DBModelEntities.LG_USER_SETUP_PROFILE
                                                                          .Where(m => m.USER_ID == puser_id).SingleOrDefault();

                                    OBJ_LG_USER_SETUP_PROFILE.ACTIVE_FLAG_MULTI_LOGIN = 1;  // user login flag became 1 as he or she  logged in and will autmatically be 0 when logout.

                                    Obj_DBModelEntities.SaveChanges();
                                }
                            }

                            return out_result = ((out_result + user_type));
                        }
                        else
                        {
                            if (v_user_info.ERROR.Contains("no role"))
                            {
                                out_result = "6";
                                Initializesessiontracker(puser_id, pSessionID, pApplicationID, pIPaddress);

                                /*
                                DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
                                LG_USER_SETUP_PROFILE OBJ_LG_USER_SETUP_PROFILE = Obj_DBModelEntities.LG_USER_SETUP_PROFILE
                                                                      .Where(m => m.USER_ID == puser_id).SingleOrDefault();
                                OBJ_LG_USER_SETUP_PROFILE.ACTIVE_FLAG_MULTI_LOGIN = 1;  // user login flag became 1 as he or she  logged in and will autmatically be 0 when logout.
                                Obj_DBModelEntities.SaveChanges();
                                */
                            }
                            else
                            {
                                out_result = "0";

                                DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);

                                LG_USER_SETUP_PROFILE OBJ_LG_USER_SETUP_PROFILE = Obj_DBModelEntities.LG_USER_SETUP_PROFILE
                                                                      .Where(m => m.USER_ID == puser_id).SingleOrDefault();

                                if (OBJ_LG_USER_SETUP_PROFILE != null)
                                {
                                    OBJ_LG_USER_SETUP_PROFILE.FAILED_LOGIN_ATTEMPT = ((Obj_DBModelEntities.LG_USER_SETUP_PROFILE.Select(i => i.FAILED_LOGIN_ATTEMPT).Cast<int?>().Max() ?? 0) + 1).ToString();
                                                                                   //((Obj_DBModelEntities.LG_USER_SETUP_PROFILE.Max(i => i.FAILED_LOGIN_ATTEMPT).Cast<int?>() ?? 0) + 1).ToString();
                                    
                                    Obj_DBModelEntities.SaveChanges();
                                    TrackFailedLoginAttempts(puser_id, pSessionID, pApplicationID, pIPaddress);
                                }

                                return out_result;
                            }
                        }
                    }
                }
                else
                {
                    return out_result = "8";
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
                    }
                }

                LG_ERROR_LOG_MAP.Add_Error_Log(null, dbEx.Source, "ERR_APP_TYPE", "VerifyUserAndPasswordForLogin",
                                            "0000000000", dbEx.Message, inner, dbEx.StackTrace, null, dateTime);

                return out_result;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                LG_ERROR_LOG_MAP.Add_Error_Log(null, ex.Source, "ERR_APP_TYPE", "VerifyUserAndPasswordForLogin",
                                       "0000000000", ex.Message, inner4, ex.StackTrace, pSessionID, dateTime);
                return out_result;
            }
            return out_result;
        }

        public static LG_USER_SETUP_PROFILE_MAP VerifyUserAndPassword(string puser_id, string pPassword, string pApplicationID)
        {
            try
            {
                DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);

                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                //string input_pass = Pass_Encryp.MD5Hash(pPassword);
                //string input_pass = Security.GetEncryptedText(pPassword);

                LG_USER_SETUP_PROFILE_MAP OBJ_LG_USER_SETUP_PROFILE_MAP = (from m in Obj_DBModelEntities.LG_USER_SETUP_PROFILE

                                                                           where m.USER_ID == puser_id

                                                                           select new LG_USER_SETUP_PROFILE_MAP

                                                                           {
                                                                               USER_ID = m.USER_ID,
                                                                               PASSWORD = m.PASSWORD,
                                                                               USER_CLASSIFICATION_ID = m.USER_CLASSIFICATION_ID,
                                                                           }).SingleOrDefault();
                string input_pass = Security.GetPlainText(OBJ_LG_USER_SETUP_PROFILE_MAP.PASSWORD);
              
                    if (OBJ_LG_USER_SETUP_PROFILE_MAP != null)
                    {
                        if (input_pass == pPassword)
                        {
                        List<LG_USER_SETUP_PROFILE_MAP> LIST_LG_USER_SETUP_PROFILE_MAP_01 = (from m in Obj_DBModelEntities.LG_USER_ROLE_ASSIGN
                                                                                             join n in Obj_DBModelEntities.LG_FNR_ROLE_DEFINE
                                                                                             on m.ROLE_ID equals n.ROLE_ID
                                                                                             where m.USER_ID == puser_id &&
                                                                                                   m.ROLE_ASSIGN_FLAG == 1
                                                                                             group n by new { n.ROLE_ID, n.APPLICATION_ID } into T
                                                                                             select new LG_USER_SETUP_PROFILE_MAP
                                                                                             {
                                                                                                 APPLICATION_ID = T.Key.APPLICATION_ID,
                                                                                             }).ToList();

                        if (LIST_LG_USER_SETUP_PROFILE_MAP_01 == null || LIST_LG_USER_SETUP_PROFILE_MAP_01.Count == 0)
                        {
                            LG_USER_SETUP_PROFILE_MAP OBJ_LG_USER_SETUP_PROFILE_MAP_new = new LG_USER_SETUP_PROFILE_MAP();
                            OBJ_LG_USER_SETUP_PROFILE_MAP_new.ERROR = "You have no role assigned for this application";
                            return OBJ_LG_USER_SETUP_PROFILE_MAP_new;
                        }
                        else
                        {
                            bool role_defined = false;
                            foreach (var item in LIST_LG_USER_SETUP_PROFILE_MAP_01)
                            {
                                if (item.APPLICATION_ID == pApplicationID)
                                {
                                    role_defined = true;
                                    break;
                                }
                            }
                            if (role_defined == false)
                            {
                                LG_USER_SETUP_PROFILE_MAP OBJ_LG_USER_SETUP_PROFILE_MAP_new = new LG_USER_SETUP_PROFILE_MAP();
                                OBJ_LG_USER_SETUP_PROFILE_MAP_new.ERROR = "You have no role defined for this application";
                                return OBJ_LG_USER_SETUP_PROFILE_MAP_new;
                            }
                        }

                        return OBJ_LG_USER_SETUP_PROFILE_MAP;
                        }
                        else
                        {
                            LG_USER_SETUP_PROFILE_MAP Obj_LG_USER_SETUP_PROFILE_MAP1 = new LG_USER_SETUP_PROFILE_MAP();
                            Obj_LG_USER_SETUP_PROFILE_MAP1.ERROR = "Invalid User Id Or Password";
                            return Obj_LG_USER_SETUP_PROFILE_MAP1;
                        }
                    }
                    else
                    {
                        LG_USER_SETUP_PROFILE_MAP Obj_LG_USER_SETUP_PROFILE_MAP1 = new LG_USER_SETUP_PROFILE_MAP();
                        Obj_LG_USER_SETUP_PROFILE_MAP1.ERROR = "Invalid User Id Or Password";
                        return Obj_LG_USER_SETUP_PROFILE_MAP1;
                    }
                
                //LG_USER_SETUP_PROFILE_MAP OBJ_LG_USER_SETUP_PROFILE_MAP = (from m in Obj_DBModelEntities.LG_USER_SETUP_PROFILE

                //                                                           where m.USER_ID == puser_id && m.PASSWORD == input_pass

                //                                                           select new LG_USER_SETUP_PROFILE_MAP

                //                                                           {
                //                                                               USER_ID = m.USER_ID,
                //                                                               PASSWORD = m.PASSWORD,
                //                                                               USER_CLASSIFICATION_ID = m.USER_CLASSIFICATION_ID,
                //                                                           }).SingleOrDefault();

                
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
                    }
                }

                LG_ERROR_LOG_MAP.Add_Error_Log(null, dbEx.Source, "ERR_APP_TYPE", "VerifyUserAndPassword",
                                            "0000000000", dbEx.Message, inner, dbEx.StackTrace, null, dateTime);

                return null;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                LG_ERROR_LOG_MAP.Add_Error_Log(null, ex.Source, "ERR_APP_TYPE", "VerifyUserAndPassword",
                                               "0000000000", ex.Message, inner4, ex.StackTrace, puser_id, dateTime);

                LG_USER_SETUP_PROFILE_MAP Obj_LG_USER_SETUP_PROFILE_MAP1 = new LG_USER_SETUP_PROFILE_MAP();
                Obj_LG_USER_SETUP_PROFILE_MAP1.ERROR = ex.Message;
                return Obj_LG_USER_SETUP_PROFILE_MAP1;
            }
        }

        public static LG_USER_SETUP_PROFILE_MAP Verify_if_lock_User(string puser_id)
        {
            try
            {
                DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);

                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                LG_USER_SETUP_PROFILE_MAP OBJ_LG_USER_SETUP_PROFILE_MAP = (from m in Obj_DBModelEntities.LG_USER_SETUP_PROFILE
                                                                           where m.USER_ID == puser_id
                                                                           select new LG_USER_SETUP_PROFILE_MAP
                                                                           {
                                                                               USER_ID = m.USER_ID,
                                                                               USER_ID_LOCK_WRNG_ATM = m.USER_ID_LOCK_WRNG_ATM
                                                                           }).SingleOrDefault();

                return OBJ_LG_USER_SETUP_PROFILE_MAP;

                /*  //salekin commented
                if (OBJ_LG_USER_SETUP_PROFILE_MAP != null)
                {
                    LG_USER_SETUP_PROFILE_MAP OBJ_LG_USER_SETUP_PROFILE_MAP_01 = (from m in Obj_DBModelEntities.LG_USER_ROLE_ASSIGN
                                                                                  where m.USER_ID == puser_id
                                                                                  group m by new { m.USER_ID } into T
                                                                                  select new LG_USER_SETUP_PROFILE_MAP
                                                                                  {
                                                                                      USER_ID = T.Key.USER_ID,
                                                                                  }).SingleOrDefault();
                    if (OBJ_LG_USER_SETUP_PROFILE_MAP_01 == null)
                    {
                        LG_USER_SETUP_PROFILE_MAP OBJ_LG_USER_SETUP_PROFILE_MAP_new = new LG_USER_SETUP_PROFILE_MAP();
                        OBJ_LG_USER_SETUP_PROFILE_MAP_new.ERROR = "You are locked 3:) !!!! Please Call Khaled immediately :P";
                        return OBJ_LG_USER_SETUP_PROFILE_MAP_new;
                    }
                    return OBJ_LG_USER_SETUP_PROFILE_MAP;
                }
                else
                {
                    LG_USER_SETUP_PROFILE_MAP Obj_LG_USER_SETUP_PROFILE_MAP1 = new LG_USER_SETUP_PROFILE_MAP();
                    Obj_LG_USER_SETUP_PROFILE_MAP1.ERROR = "Invalid User Id ";
                    return Obj_LG_USER_SETUP_PROFILE_MAP1;
                }  */
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
                    }
                }

                LG_ERROR_LOG_MAP.Add_Error_Log(null, dbEx.Source, "ERR_APP_TYPE", "Verify_if_lock_User",
                                            "0000000000", dbEx.Message, inner, dbEx.StackTrace, null, dateTime);

                return null;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                LG_ERROR_LOG_MAP.Add_Error_Log(null, ex.Source, "ERR_APP_TYPE", "Verify_if_lock_User",
                                                   "0000000000", ex.Message, inner4, ex.StackTrace, puser_id, dateTime);

                LG_USER_SETUP_PROFILE_MAP Obj_LG_USER_SETUP_PROFILE_MAP1 = new LG_USER_SETUP_PROFILE_MAP();
                Obj_LG_USER_SETUP_PROFILE_MAP1.ERROR = ex.Message;
                return Obj_LG_USER_SETUP_PROFILE_MAP1;
            }
        }

        public static string LockUserID(string pUserID)
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            string result = string.Empty;
            try
            {
                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                LG_USER_SETUP_PROFILE OBJ_LG_USER_SETUP_PROFILE = Obj_DBModelEntities.LG_USER_SETUP_PROFILE
                                                           .Where(m => m.USER_ID == pUserID).SingleOrDefault();

                OBJ_LG_USER_SETUP_PROFILE.USER_ID_LOCK_WRNG_ATM = 1;
                Obj_DBModelEntities.SaveChanges();

                result = "5";
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
                        result = "Can't Update Application(Db) " + validationError.ErrorMessage;
                    }
                }

                LG_ERROR_LOG_MAP.Add_Error_Log(null, dbEx.Source, "ERR_APP_TYPE", "LockUserID",
                                            "0000000000", dbEx.Message, inner, dbEx.StackTrace, null, dateTime);

                return result;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                LG_ERROR_LOG_MAP.Add_Error_Log(null, ex.Source, "ERR_APP_TYPE", "LockUserID",
                                              "0000000000", ex.Message, inner4, ex.StackTrace, null, dateTime);

                result = "Can't Update Application.";
                return result;
            }
        }

        public static LG_USER_SETUP_PROFILE_MAP GetUserSetupInfoByUserId(string puser_id, string session_chk)
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            LG_USER_SETUP_PROFILE_MAP OBJ_LG_USER_SETUP_PROFILE = new LG_USER_SETUP_PROFILE_MAP();

            try
            {
                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                if (session_chk == "1")
                {
                    OBJ_LG_USER_SETUP_PROFILE = (from m in Obj_DBModelEntities.LG_USER_SETUP_PROFILE

                                                 where m.USER_ID == puser_id
                                                 select new LG_USER_SETUP_PROFILE_MAP()
                                                 {
                                                     USER_ID = m.USER_ID,
                                                     // APPLICATION_ID = s.APPLICATION_ID,
                                                     ACTIVE_FLAG_INACTV_USER = m.ACTIVE_FLAG_INACTV_USER,
                                                     USER_ID_LOCK_WRNG_ATM = m.USER_ID_LOCK_WRNG_ATM,
                                                     ACTIVE_FLAG_MULTI_LOGIN = m.ACTIVE_FLAG_MULTI_LOGIN,
                                                     FIRST_LOGIN_FLAG = m.FIRST_LOGIN_FLAG,
                                                     USER_CLASSIFICATION_ID = m.USER_CLASSIFICATION_ID,
                                                     USER_AREA_ID_VALUE = m.USER_AREA_ID_VALUE,
                                                     BRANCH_ID = m.BRANCH_ID
                                                 }).SingleOrDefault();
                    return OBJ_LG_USER_SETUP_PROFILE;
                }

                if (session_chk == "2")
                {
                    OBJ_LG_USER_SETUP_PROFILE.USER_LAST_TIME = (Obj_DBModelEntities.LG_SYS_SESSION_TRACKER
                                                               .Where(st => st.USER_ID == puser_id &&
                                                                            st.ACTIVE_FLAG_FOR_MULTI_LOGIN == 1)
                                                              .Select(t => t.LAST_ACCESS_TIME).Max());
                    var date = OBJ_LG_USER_SETUP_PROFILE.USER_LAST_TIME;

                    //OBJ_LG_USER_SETUP_PROFILE = (from t in Obj_DBModelEntities.LG_SYS_SESSION_TRACKER

                    //                             where innerQuery.(t.LAST_ACCESS_TIME) && t.USER_ID == puser_id && t.ACTIVE_FLAG_MULTI_LOGIN == 1

                    //                             select new LG_USER_SETUP_PROFILE_MAP()
                    //                             {
                    //                                 //USER_ID = t.USER_ID,
                    //                                 //APPLICATION_ID = t.APPLICATION_ID,

                    //                                 ACTIVE_FLAG_MULTI_LOGIN = t.ACTIVE_FLAG_MULTI_LOGIN,

                    //                                 USER_SESSION_ID = t.SESSION_ID,
                    //                                 USER_START_TIME = t.START_TIME,
                    //                                 USER_LAST_TIME = t.LAST_ACCESS_TIME,
                    //                                 IP_ADDRESS = t.IP_ADDRESS,
                    //                                 // date = EntityFunctions.CreateTime(t.LAST_ACCESS_TIME.Hour,t.LAST_ACCESS_TIME.Minute,t.LAST_ACCESS_TIME.Second)  -- to pick time(hr,min,sec) from date
                    //                                 HR = t.LAST_ACCESS_TIME.Hour,
                    //                                 MIN = t.LAST_ACCESS_TIME.Minute,

                    //                             }).SingleOrDefault();

                    OBJ_LG_USER_SETUP_PROFILE = (from t in Obj_DBModelEntities.LG_SYS_SESSION_TRACKER

                                                 where t.USER_ID == puser_id && t.ACTIVE_FLAG_FOR_MULTI_LOGIN == 1 && t.LAST_ACCESS_TIME == date

                                                 select new LG_USER_SETUP_PROFILE_MAP()
                                                 {
                                                     USER_ID = t.USER_ID,
                                                     APPLICATION_ID = t.APPLICATION_ID,

                                                     ACTIVE_FLAG_MULTI_LOGIN = t.ACTIVE_FLAG_FOR_MULTI_LOGIN,

                                                     USER_SESSION_ID = t.SESSION_ID,
                                                     USER_START_TIME = t.START_TIME,
                                                     USER_LAST_TIME = t.LAST_ACCESS_TIME,
                                                     IP_ADDRESS = t.IP_ADDRESS,
                                                     // date = EntityFunctions.CreateTime(t.LAST_ACCESS_TIME.Hour,t.LAST_ACCESS_TIME.Minute,t.LAST_ACCESS_TIME.Second)  -- to pick time(hr,min,sec) from date
                                                     HR = t.LAST_ACCESS_TIME.Hour,
                                                     MIN = t.LAST_ACCESS_TIME.Minute,
                                                 }).SingleOrDefault();
                    return OBJ_LG_USER_SETUP_PROFILE;
                }
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

                LG_ERROR_LOG_MAP.Add_Error_Log(null, dbEx.Source, "ERR_APP_TYPE", "GetUserSetupInfoByUserId",
                                            "0000000000", dbEx.Message, inner, dbEx.StackTrace, null, dateTime);

                return OBJ_LG_USER_SETUP_PROFILE;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                LG_ERROR_LOG_MAP.Add_Error_Log(null, ex.Source, "ERR_APP_TYPE", "GetUserSetupInfoByUserId",
                                              "0000000000", ex.Message, inner4, ex.StackTrace, null, dateTime);

                OBJ_LG_USER_SETUP_PROFILE.ERROR = ex.Message;
                return OBJ_LG_USER_SETUP_PROFILE;
            }
        }

        public static string Initializesessiontracker(string pUserID, string pSessionID, string pApplicationID, string pIPaddress)
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            string result = string.Empty;
            try
            {
                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                LG_SYS_SESSION_TRACKER OBJ_LG_SYS_SESSION_TRACKER = new LG_SYS_SESSION_TRACKER();
                List<LG_SYS_SESSION_TRACKER> LIST_OBJ_LG_SYS_SESSION_TRACKER = new List<LG_SYS_SESSION_TRACKER>();

                LIST_OBJ_LG_SYS_SESSION_TRACKER = Obj_DBModelEntities.LG_SYS_SESSION_TRACKER.Where(i => i.USER_ID == pUserID).ToList();

                if (LIST_OBJ_LG_SYS_SESSION_TRACKER != null)
                {
                    LIST_OBJ_LG_SYS_SESSION_TRACKER.ForEach(a =>
                    {
                        a.ACTIVE_FLAG_FOR_MULTI_LOGIN = 0;
                    });
                    Obj_DBModelEntities.SaveChanges();
                }

                OBJ_LG_SYS_SESSION_TRACKER.SL_NO = ((Obj_DBModelEntities.LG_SYS_SESSION_TRACKER.Select(i => i.SL_NO).Cast<int?>().Max() ?? 0) + 1).ToString();
                OBJ_LG_SYS_SESSION_TRACKER.USER_ID = pUserID;
                OBJ_LG_SYS_SESSION_TRACKER.SESSION_ID = pSessionID;
                OBJ_LG_SYS_SESSION_TRACKER.START_TIME = System.DateTime.Now;
                OBJ_LG_SYS_SESSION_TRACKER.LAST_ACCESS_TIME = System.DateTime.Now;
                OBJ_LG_SYS_SESSION_TRACKER.IP_ADDRESS = pIPaddress;
                OBJ_LG_SYS_SESSION_TRACKER.ACTIVE_FLAG_FOR_MULTI_LOGIN = 1;
                OBJ_LG_SYS_SESSION_TRACKER.REMARKS = "Login in Application";
                OBJ_LG_SYS_SESSION_TRACKER.SESSION_DT = System.DateTime.Now;

                OBJ_LG_SYS_SESSION_TRACKER.APPLICATION_ID = pApplicationID;

                Obj_DBModelEntities.LG_SYS_SESSION_TRACKER.Add(OBJ_LG_SYS_SESSION_TRACKER);
                Obj_DBModelEntities.SaveChanges();

                result = "5";
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
                        result = "Can't Update Application(Db).";
                    }
                }

                LG_ERROR_LOG_MAP.Add_Error_Log(null, dbEx.Source, "ERR_APP_TYPE", "Initializesessiontracker",
                                            "0000000000", dbEx.Message, inner, dbEx.StackTrace, pSessionID, dateTime);

                return result;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                LG_ERROR_LOG_MAP.Add_Error_Log(null, ex.Source, "ERR_APP_TYPE", "Initializesessiontracker",
                                              "0000000000", ex.Message, inner4, ex.StackTrace, pSessionID, dateTime);

                result = "Can't Update Application.";
                return result;
            }
        }

        public static string TrackFailedLoginAttempts(string pUserID, string pSessionID, string pApplicationID, string pIPaddress)
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            string result = string.Empty;
            try
            {
                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                LG_SYS_FAILED_LOGIN_ATTEMPT OBJ_LG_SYS_FAILED_LOGIN_ATTEPT = new LG_SYS_FAILED_LOGIN_ATTEMPT();

                OBJ_LG_SYS_FAILED_LOGIN_ATTEPT.SL_NO = ((Obj_DBModelEntities.LG_SYS_FAILED_LOGIN_ATTEMPT.Select(i => i.SL_NO).Cast<int?>().Max() ?? 0) + 1).ToString();
                OBJ_LG_SYS_FAILED_LOGIN_ATTEPT.USER_ID = pUserID;
                OBJ_LG_SYS_FAILED_LOGIN_ATTEPT.SESSION_ID = pSessionID;

                OBJ_LG_SYS_FAILED_LOGIN_ATTEPT.LAST_ACCESS_TIME = System.DateTime.Now;
                OBJ_LG_SYS_FAILED_LOGIN_ATTEPT.IP_ADDRESS = pIPaddress;

                OBJ_LG_SYS_FAILED_LOGIN_ATTEPT.REMARKS = " Login Failed due to password mismatch.";
                OBJ_LG_SYS_FAILED_LOGIN_ATTEPT.SESSION_DT = System.DateTime.Now;

                OBJ_LG_SYS_FAILED_LOGIN_ATTEPT.APPLICATION_ID = pApplicationID;

                Obj_DBModelEntities.LG_SYS_FAILED_LOGIN_ATTEMPT.Add(OBJ_LG_SYS_FAILED_LOGIN_ATTEPT);
                Obj_DBModelEntities.SaveChanges();

                result = "5";
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
                        result = "Can't Update Application(Db).";
                    }
                }

                LG_ERROR_LOG_MAP.Add_Error_Log(null, dbEx.Source, "ERR_APP_TYPE", "TrackFailedLoginAttempts",
                                            "0000000000", dbEx.Message, inner, dbEx.StackTrace, pSessionID, dateTime);

                return result;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                LG_ERROR_LOG_MAP.Add_Error_Log(null, ex.Source, "ERR_APP_TYPE", "TrackFailedLoginAttempts",
                                              "0000000000", ex.Message, inner4, ex.StackTrace, pSessionID, dateTime);

                result = "Can't Update Application.";
                return result;
            }
        }

        public static string LogOutuser(string pUserID, string pSessionID, string pIPaddress, string pApplicationID)
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            string result = string.Empty;
            try
            {
                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                LG_USER_SETUP_PROFILE OBJ_LG_USER_SETUP_PROFILE = Obj_DBModelEntities.LG_USER_SETUP_PROFILE
                                                           .Where(m => m.USER_ID == pUserID).SingleOrDefault();

                OBJ_LG_USER_SETUP_PROFILE.ACTIVE_FLAG_MULTI_LOGIN = 0;
                Obj_DBModelEntities.SaveChanges();

                LG_SYS_SESSION_TRACKER OBJ_LG_SYS_SESSION_TRACKER = new LG_SYS_SESSION_TRACKER();

                OBJ_LG_SYS_SESSION_TRACKER.LAST_ACCESS_TIME = (Obj_DBModelEntities.LG_SYS_SESSION_TRACKER
                                 .Where(st => st.USER_ID == pUserID &&
                                              st.ACTIVE_FLAG_FOR_MULTI_LOGIN == 1)
                                 .Select(t => t.LAST_ACCESS_TIME).Max());

                var date = OBJ_LG_SYS_SESSION_TRACKER.LAST_ACCESS_TIME;

                OBJ_LG_SYS_SESSION_TRACKER = Obj_DBModelEntities.LG_SYS_SESSION_TRACKER
                                                          .Where(m => m.USER_ID == pUserID && m.SESSION_ID == pSessionID && m.IP_ADDRESS == pIPaddress
                                                              && m.APPLICATION_ID == pApplicationID && m.LAST_ACCESS_TIME == date).SingleOrDefault();

                OBJ_LG_SYS_SESSION_TRACKER.ACTIVE_FLAG_FOR_MULTI_LOGIN = 0;
                OBJ_LG_SYS_SESSION_TRACKER.REMARKS = "Log Out From Application";
                OBJ_LG_SYS_SESSION_TRACKER.LAST_ACCESS_TIME = System.DateTime.Now;
                Obj_DBModelEntities.SaveChanges();

                result = "1";
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
                        result = "Can't Update Application(Db).";
                    }
                }

                LG_ERROR_LOG_MAP.Add_Error_Log(null, dbEx.Source, "ERR_APP_TYPE", "LogOutuser",
                            "0000000000", dbEx.Message, inner, dbEx.StackTrace, pSessionID, dateTime);

                return result;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                LG_ERROR_LOG_MAP.Add_Error_Log(null, ex.Source, "ERR_APP_TYPE", "LogOutuser",
                                              "0000000000", ex.Message, inner4, ex.StackTrace, pSessionID, dateTime);

                result = "Can't Update Application.";
                return result;
            }
        }
    }
}