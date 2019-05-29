
using Model.EDMX;
using Model.EntityModel.Common;
using Model.EntityModel.LGModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.EntityModel.TwoFactorAuth
{
    public class OTP
    {
        public static string FUNC_ID = "010105002";
        public static string GenerateToken(string user_id, string session_id, string terminal_ip, string function_type, string from_branch, string from_ac_no, string to_branch, string to_ac_no, string bill_id_no, string trans_amount, string app_id, string card_no)
        {
            try
            {
                LG_2FA_OTP_CONFIG_MAP OBJ_LG_2FA_OTP_CONFIG_MAP = LG_2FA_OTP_CONFIG_MAP.GetOtpConfigByAppId(app_id);

                string puser_id = "", cell_no = "", email_id = "";
                Random r = new Random();

                string str = string.Empty;
                // OPT Format select:  1:Number Only  2:Alphanumneric  3:Alphanumeric with special charecter 
                if (OBJ_LG_2FA_OTP_CONFIG_MAP.OTP_FORMAT_ID == "1")
                {
                    str = "123456789";
                }
                if (OBJ_LG_2FA_OTP_CONFIG_MAP.OTP_FORMAT_ID == "2")
                {
                    str = "abcdefghijkmnpqrstuvwxyz23456789";
                }
                if (OBJ_LG_2FA_OTP_CONFIG_MAP.OTP_FORMAT_ID == "3")
                {
                    str = "abcdefghijkmnpqrstuvwxyz23456789@&$";
                }

                StringBuilder sb = new StringBuilder();
                int len = Convert.ToInt32(OBJ_LG_2FA_OTP_CONFIG_MAP.NO_OF_OTP_DIGIT); //OTP digit length
                while ((len--) > 0)
                    sb.Append(str[(int)(r.NextDouble() * str.Length)]);
                


                string vText_NotEncrypted = sb.ToString();
                string vText = string.Empty;
                string allowEncryptedOTP = "YES"; //configurable key
                if (allowEncryptedOTP.ToUpper() == "YES")
                {
                    string vText_Encrypted = Security.GetEncryptedText(vText_NotEncrypted);
                    vText = vText_Encrypted;
                }
                else
                    vText = vText_NotEncrypted;


                string vResult = LG_2FA_OTP_CONFIG_MAP.GetUserMobileAndEmail(user_id);
                if (vResult == "0")
                {
                    return "0";
                }
                else
                {
                    string[] vStr = vResult.Split(',');
                    puser_id = vStr[0].ToString();
                    cell_no = vStr[1].ToString();
                    email_id = vStr[2].ToString();
                }

                if (puser_id != user_id)
                {
                    return "0";
                }
                /*
                if (function_type == "1" && (string.IsNullOrEmpty(to_branch) || (string.IsNullOrEmpty(to_ac_no))))
                {
                    return "0";
                }
                if (function_type == "2" && (string.IsNullOrEmpty(bill_id_no)))
                {
                    return "0";
                }
                if (function_type == "3" && (string.IsNullOrEmpty(user_id)))
                {
                    return "0";
                } */

                LG_2FA_OTP_CONFIG_MAP.UpdateTokenGenLogActiveFlag(user_id, app_id);
                string retValue = LG_2FA_OTP_CONFIG_MAP.GenerateToken(user_id, session_id, terminal_ip, function_type, from_branch, from_ac_no, to_branch, to_ac_no, bill_id_no, trans_amount, vText, cell_no, cell_no, vText, app_id, card_no);

                if (retValue == "1")
                {
                    
                    if (OBJ_LG_2FA_OTP_CONFIG_MAP.SMS_FLAG == 1) //SMS
                    {
                        //To save token as Plain text in DB
                        if (function_type == "3")
                        {
                            string msg_txt = "Please use this pin no for Login: " + vText_NotEncrypted + " Thanks.";
                            string result = LG_2FA_OTP_CONFIG_MAP.CreateSmsTxt(from_branch, from_ac_no, null, cell_no, null, msg_txt);
                            if(result != "1")
                            {
                                return "0";
                            }
                        }
                        else
                        {
                            string msg_txt = "Please use this pin no for fund transfer: " + vText_NotEncrypted + " Thanks.";
                            string result = LG_2FA_OTP_CONFIG_MAP.CreateSmsTxt(from_branch, from_ac_no, null, cell_no, null, msg_txt);
                            if (result != "1")
                            {
                                return "0";
                            }
                        }
                    }

                    if(OBJ_LG_2FA_OTP_CONFIG_MAP.MAIL_FLAG == 1) //MAIL
                    {
                        //For send mail for OTP token
                        if (!string.IsNullOrEmpty(email_id))
                        {
                            string result = SendEmailAfterOTPGenerate(user_id, email_id, vText_NotEncrypted, app_id);
                        }
                    }
                }

                return retValue;  //return 1=successful, 0=unsuccessful
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);
                LG_ERROR_LOG_MAP.Add_Error_Log(FUNC_ID, ex.Source, "ERR_APP_TYPE", "GenerateToken",
                                          "0000000000", ex.Message, inner4, ex.StackTrace, session_id, dateTime);

                return "0";
            }
        }
        public static string VerifyToken(string puser_id, string psession_id, string pterminal_ip, string pfunction_type, string pfrom_branch, string pfrom_ac_no, string pto_branch, string pto_ac_no, string pbill_id_no, string ptrans_amount, string token1, string token2, string papp_id, string pcard_no)
        {
            string out_result = "0";
            try
            {
                string ptoken1 = string.Empty;
                string ptoken2 = string.Empty;
                string pneEncrypted = string.Empty;
                string pneDecrypted = string.Empty;
                int function_type = Convert.ToInt16(pfunction_type);
                string AllowEncryptedOTP = "YES"; //configurable
                 
                if(AllowEncryptedOTP.ToUpper() == "YES")
                {
                    DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
                    pneEncrypted = (Obj_DBModelEntities.LG_2FA_TOKEN_GEN_LOG
                                    .Where(u => u.USER_ID == puser_id &&
                                                u.SESSION_ID == psession_id &&
                                                u.TERMINAL_IP == pterminal_ip &&
                                                u.FUNCTION_TYPE == function_type &&
                                                u.ACTIVE_FLAG == 1)
                                    .Select(u => u.PN)).SingleOrDefault();

                    pneDecrypted = Security.GetPlainText(pneEncrypted);

                    if (token1 == pneDecrypted)
                    {
                        ptoken1 = ptoken2 = pneEncrypted;
                    }
                    else
                        return "0";
                }
                if (AllowEncryptedOTP.ToUpper() == "NO")
                {
                    DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
                    pneDecrypted = (Obj_DBModelEntities.LG_2FA_TOKEN_GEN_LOG
                                    .Where(u => u.USER_ID == puser_id &&
                                                u.SESSION_ID == psession_id &&
                                                u.TERMINAL_IP == pterminal_ip &&
                                                u.FUNCTION_TYPE == function_type &&
                                                u.ACTIVE_FLAG == 1)
                                    .Select(u => u.PN)).SingleOrDefault();

                    if (token1 == pneDecrypted)
                    {
                        ptoken1 = ptoken2 = pneDecrypted;
                    }
                    else
                        return "0";
                }
                


                var v_token_log = LG_2FA_OTP_CONFIG_MAP.GetActiveTokenGenLogForSpecificUser(puser_id, psession_id, pterminal_ip, pfunction_type, ptoken1, ptoken2, papp_id);

                if (v_token_log != null)
                {
                    //Validity period logic
                    LG_2FA_OTP_CONFIG_MAP OBJ_LG_2FA_OTP_CONFIG_MAP = LG_2FA_OTP_CONFIG_MAP.GetOtpConfigByAppId(papp_id);
                    double validity_period_in_sec = Convert.ToDouble(OBJ_LG_2FA_OTP_CONFIG_MAP.VALIDITY_PERIOD) * 60;
                    var elasped_time = (System.DateTime.Now - v_token_log.GEN_TIME);
                    double elasped_time_in_sec = ((elasped_time).Days * 3600 * 24) + ((elasped_time).Hours * 3600) + ((elasped_time).Minutes * 60) + (elasped_time).Seconds;
                    if (elasped_time_in_sec > validity_period_in_sec)
                    {
                        LG_2FA_OTP_CONFIG_MAP.UpdateTokenGenLogForTokenValidityPeriodExpired(puser_id, psession_id, pterminal_ip, pfunction_type, ptoken1, ptoken2, papp_id);
                        return out_result;
                    }


                    /*
                    if (pfunction_type == "1")  //Fund Transfer
                    {
                        if (v_token_log.FROM_BRANCH != pfrom_branch || v_token_log.FROM_AC_NO != pfrom_ac_no)  //source account mismatch
                        {
                            LG_2FA_OTP_CONFIG_MAP.UpdateTokenGenLogForSourceAccMismatch(puser_id, psession_id, pterminal_ip, pfunction_type, ptoken1, ptoken2, papp_id);
                            return out_result;
                        }
                        if (v_token_log.TO_BRANCH != pto_branch || v_token_log.TO_AC_NO != pto_ac_no)  //destination account mismatch
                        {
                            LG_2FA_OTP_CONFIG_MAP.UpdateTokenGenLogForDestinationAccMismatch(puser_id, psession_id, pterminal_ip, pfunction_type, ptoken1, ptoken2, papp_id);
                            return out_result;
                        }
                        if (v_token_log.TRANS_AMOUNT != Convert.ToDecimal(ptrans_amount))  //amount mismatch
                        {
                            LG_2FA_OTP_CONFIG_MAP.UpdateTokenGenLogForAmountMismatch(puser_id, psession_id, pterminal_ip, pfunction_type, ptoken1, ptoken2, papp_id);
                            return out_result;
                        }
                    }
                    else if (pfunction_type == "2")  //BILL
                    {
                        if (v_token_log.FROM_BRANCH != pfrom_branch || v_token_log.FROM_AC_NO != pfrom_ac_no)  //source account mismatch
                        {
                            LG_2FA_OTP_CONFIG_MAP.UpdateTokenGenLogForSourceAccMismatch(puser_id, psession_id, pterminal_ip, pfunction_type, ptoken1, ptoken2, papp_id);
                            return out_result;
                        }
                        if (v_token_log.BILL_ID_NO != pbill_id_no)  //destination bill id mismatch
                        {
                            LG_2FA_OTP_CONFIG_MAP.UpdateTokenGenLogForDestinationBillIdMismatch(puser_id, psession_id, pterminal_ip, pfunction_type, ptoken1, ptoken2, papp_id);
                            return out_result;
                        }
                        if (v_token_log.TRANS_AMOUNT != Convert.ToDecimal(ptrans_amount))  //amount mismatch
                        {
                            LG_2FA_OTP_CONFIG_MAP.UpdateTokenGenLogForAmountMismatch(puser_id, psession_id, pterminal_ip, pfunction_type, ptoken1, ptoken2, papp_id);
                            return out_result;
                        }
                    }
                    else if (pfunction_type == "3") //Authentication type
                    {
                        if (v_token_log.USER_ID != puser_id)  //source account mismatch
                        {
                            LG_2FA_OTP_CONFIG_MAP.UpdateTokenGenLogForSourceAccMismatch(puser_id, psession_id, pterminal_ip, pfunction_type, ptoken1, ptoken2, papp_id);
                            return out_result;
                        }
                    }
                    else if (pfunction_type == "4") //Card
                    {
                        if (v_token_log.USER_ID != puser_id)  //source account mismatch
                        {
                            LG_2FA_OTP_CONFIG_MAP.UpdateTokenGenLogForSourceAccMismatch(puser_id, psession_id, pterminal_ip, pfunction_type, ptoken1, ptoken2, papp_id);
                            return out_result;
                        }
                        if (v_token_log.CARD_NO != pcard_no)  //card no mismatch
                        {
                            LG_2FA_OTP_CONFIG_MAP.UpdateTokenGenLogForCardNoMismatch(puser_id, psession_id, pterminal_ip, pfunction_type, ptoken1, ptoken2, papp_id);
                            return out_result;
                        }
                    }*/

                    out_result = "1";
                    LG_2FA_OTP_CONFIG_MAP.UpdateTokenGenLogForSuccessfulToken(puser_id, psession_id, pterminal_ip, pfunction_type, ptoken1, ptoken2, papp_id);
                    return out_result;
                }

                return out_result;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);
                LG_ERROR_LOG_MAP.Add_Error_Log(FUNC_ID, ex.Source, "ERR_APP_TYPE", "GenerateToken",
                                          "0000000000", ex.Message, inner4, ex.StackTrace, psession_id, dateTime);

                return out_result;
            }
        }
        public static string RegisterUser(string puser_id, string pcust_id, string pgroup_id, string pcell_no, string pemail_id, string preg_dt, string psec_cell_no, string papp_id)
        {
            return LG_2FA_OTP_CONFIG_MAP.RegisterUser(puser_id, pcust_id, pgroup_id, pcell_no, pemail_id, preg_dt, psec_cell_no, papp_id);
        }
        public static string UpdateUser(string puser_id, string pcust_id, string pgroup_id, string pcell_no, string pemail_id, string preg_dt, string psec_cell_no, string papp_id)
        {
            try
            {
                string out_result = string.Empty;
                var v_count = LG_2FA_OTP_CONFIG_MAP.GetUserInfoByUserId(puser_id, papp_id);
                if (v_count.Count() == 1)
                {
                    out_result = "1";
                }
                else
                    out_result = "0";

                if (out_result == "1")
                {
                    return LG_2FA_OTP_CONFIG_MAP.UpdateUserInfo(puser_id, pcust_id, pgroup_id, pcell_no, pemail_id, preg_dt, psec_cell_no, papp_id);
                }
                else
                    return LG_2FA_OTP_CONFIG_MAP.RegisterUser(puser_id, pcust_id, pgroup_id, pcell_no, pemail_id, preg_dt, psec_cell_no, papp_id);
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);
                LG_ERROR_LOG_MAP.Add_Error_Log(FUNC_ID, ex.Source, "ERR_APP_TYPE", "UpdateUser",
                                          "0000000000", ex.Message, inner4, ex.StackTrace, null, dateTime);

                return "0";
            }

        }
        public static string SendEmailAfterOTPGenerate(string user_id, string email_id, string vText_NotEncrypted, string app_id)
        {
            string success = string.Empty;

            if (string.IsNullOrEmpty(email_id))
            {
                success = "Empty Email Address";
                return success;
            }

            //string success = string.Empty;
            string to_user_id = string.Empty;
            string to_email_id = string.Empty;
            string emailfooter = string.Empty;
            string password = string.Empty;

            try
            {

                //For Email
                string ToDisplayName = string.Empty;
                string ToAdr = string.Empty;
                string FromDisplayName = string.Empty;
                string FromAdr = string.Empty;
                string CcDisplayName = string.Empty;
                string CcAdr = string.Empty;
                string BccAdr = string.Empty;
                string Subject = string.Empty;
                string BodyText = string.Empty;
                string file_path = string.Empty;

                string EmailFooterFileName = "EMAIL_FOOTER_FOR_OTP.txt";
                string EmailBODYFileName = "EMAIL_BODY_FOR_OTP.txt";
                string LS_IBU_USER_DOC = ConfigurationManager.AppSettings["IBU_USER_DOC"].ToString();
                StringBuilder TextMessage = new StringBuilder();
                //  Random MyRandomNumber;
                string tmpFileName = string.Empty;
                string random = string.Empty;
                #region email sending
                // if EmailSendiing is flag is ON


                ToDisplayName = user_id;
                ToAdr = email_id;

                if (string.IsNullOrEmpty(ToAdr))
                {
                    success = "0";
                    return success;
                }
                FromDisplayName = string.Empty;
                CcDisplayName = string.Empty;
                CcAdr = string.Empty;
                BccAdr = string.Empty;
                Subject = ConfigurationManager.AppSettings["OTP_MAIL_SUBJECT"].ToString();

                BodyText = string.Empty;

                file_path = LS_IBU_USER_DOC + @"\" + EmailBODYFileName;
                BodyText = File.ReadAllText(file_path);

                TextMessage.AppendLine(BodyText);

                BodyText = string.Empty;
                //to_user_id = string.Empty;
                to_email_id = string.Empty;
                //to_user_id = user_id;
                to_email_id = ToAdr;
                TextMessage.AppendLine("------------------------------" + "<br/>");

                TextMessage.AppendLine("User ID:     " + user_id + "<br/>");
                TextMessage.AppendLine("Token:     " + vText_NotEncrypted + "<br/>");

                TextMessage.AppendLine("------------------------------" + "<br/>");


                file_path = LS_IBU_USER_DOC + @"\" + EmailFooterFileName;
                emailfooter = File.ReadAllText(file_path);

                TextMessage.AppendLine(emailfooter);
                BodyText = TextMessage.ToString();
                string result  = LS_EMail.BasicSendMail(
                                        ToDisplayName
                                        , ToAdr
                                        , Subject
                                        , BodyText
                                        , app_id);
                if (result != "1")
                {
                    return "0";
                }
                #endregion
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);
                LG_ERROR_LOG_MAP.Add_Error_Log(FUNC_ID, ex.Source, "ERR_APP_TYPE", "SendEmailAfterOTPGenerate",
                                          "0000000000", ex.Message, inner4, ex.StackTrace, null, dateTime);

                //ClsIBULog.SaveErrorLogInDb(to_user_id, "Email Sending Failed for Activation:" + to_email_id + " " + ex.Message, "user_activation_email");
                //DO not DO Anything
                return success = "0";
            }
            return success = "1";
        }
        public static string SendEmailAfterPasswordChange(string user_id, string email_id, string password, string app_id)
        {
            string success = string.Empty;

            if (string.IsNullOrEmpty(email_id))
            {
                success = "Empty Email Address";
                return success;
            }

            //string success = string.Empty;
            string to_user_id = string.Empty;
            string to_email_id = string.Empty;
            string emailfooter = string.Empty;

            try
            {

                //For Email
                string ToDisplayName = string.Empty;
                string ToAdr = string.Empty;
                string FromDisplayName = string.Empty;
                string FromAdr = string.Empty;
                string CcDisplayName = string.Empty;
                string CcAdr = string.Empty;
                string BccAdr = string.Empty;
                string Subject = string.Empty;
                string BodyText = string.Empty;
                string file_path = string.Empty;

                string EmailFooterFileName = "EMAIL_FOOTER_FOR_PASSWORD_CHANGE.txt";
                string EmailBODYFileName = "EMAIL_BODY_FOR_PASSWORD_CHANGE.txt";
                string LS_IBU_USER_DOC = ConfigurationManager.AppSettings["IBU_USER_DOC"].ToString();
                StringBuilder TextMessage = new StringBuilder();
                //  Random MyRandomNumber;
                string tmpFileName = string.Empty;
                string random = string.Empty;
                #region email sending
                // if EmailSendiing is flag is ON


                ToDisplayName = user_id;
                ToAdr = email_id;

                if (string.IsNullOrEmpty(ToAdr))
                {
                    success = "0";
                    return success;
                }
                FromDisplayName = string.Empty;
                CcDisplayName = string.Empty;
                CcAdr = string.Empty;
                BccAdr = string.Empty;
                Subject = ConfigurationManager.AppSettings["PASSWORD_CHANGE_MAIL_SUBJECT"].ToString();

                BodyText = string.Empty;

                //file_path = LS_IBU_USER_DOC + EmailBODYFileName ;
                file_path = System.IO.Path.Combine(LS_IBU_USER_DOC, EmailBODYFileName);
                BodyText = File.ReadAllText(file_path);

                TextMessage.AppendLine(BodyText);

                BodyText = string.Empty;
                //to_user_id = string.Empty;
                to_email_id = string.Empty;
                //to_user_id = user_id;
                to_email_id = ToAdr;
                TextMessage.AppendLine("------------------------------" + "<br/>");

                TextMessage.AppendLine("User ID:     " + user_id + "<br/>");
                TextMessage.AppendLine("Password:     " + password + "<br/>");

                TextMessage.AppendLine("------------------------------" + "<br/>");


                file_path = LS_IBU_USER_DOC + EmailFooterFileName;
                emailfooter = File.ReadAllText(file_path);

                TextMessage.AppendLine(emailfooter);
                BodyText = TextMessage.ToString();
                string result = LS_EMail.BasicSendMail(
                                        ToDisplayName
                                        , ToAdr
                                        , Subject
                                        , BodyText
                                        , app_id);
                if (result != "1")
                {
                    return "0";
                }
                #endregion
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);
                LG_ERROR_LOG_MAP.Add_Error_Log(FUNC_ID, ex.Source, "ERR_APP_TYPE", "SendEmailAfterPasswordChange",
                                          "0000000000", ex.Message, inner4, ex.StackTrace, null, dateTime);

                //ClsIBULog.SaveErrorLogInDb(to_user_id, "Email Sending Failed for Activation:" + to_email_id + " " + ex.Message, "user_activation_email");
                //DO not DO Anything
                return success = "0";
            }
            return success = "1";
        }
        public static string SendEmailAfterUserCreate(string user_id, string email_id, string password, string app_id)
        {
            string success = string.Empty;

            if (string.IsNullOrEmpty(email_id))
            {
                success = "Empty Email Address";
                return success;
            }

            //string success = string.Empty;
            string to_user_id = string.Empty;
            string to_email_id = string.Empty;
            string emailfooter = string.Empty;

            try
            {

                //For Email
                string ToDisplayName = string.Empty;
                string ToAdr = string.Empty;
                string FromDisplayName = string.Empty;
                string FromAdr = string.Empty;
                string CcDisplayName = string.Empty;
                string CcAdr = string.Empty;
                string BccAdr = string.Empty;
                string Subject = string.Empty;
                string BodyText = string.Empty;
                string file_path = string.Empty;

                string EmailFooterFileName = "EMAIL_FOOTER_FOR_USER_CREATE.txt";
                string EmailBODYFileName = "EMAIL_BODY_FOR_USER_CREATE.txt";
                string LS_IBU_USER_DOC = ConfigurationManager.AppSettings["IBU_USER_DOC"].ToString();
                StringBuilder TextMessage = new StringBuilder();
                //  Random MyRandomNumber;
                string tmpFileName = string.Empty;
                string random = string.Empty;
                #region email sending
                // if EmailSendiing is flag is ON


                ToDisplayName = user_id;
                ToAdr = email_id;

                if (string.IsNullOrEmpty(ToAdr))
                {
                    success = "0";
                    return success;
                }
                FromDisplayName = string.Empty;
                CcDisplayName = string.Empty;
                CcAdr = string.Empty;
                BccAdr = string.Empty;
                Subject = ConfigurationManager.AppSettings["USER_CREATE_MAIL_SUBJECT"].ToString();

                BodyText = string.Empty;

                //file_path = LS_IBU_USER_DOC + EmailBODYFileName ;
                file_path = System.IO.Path.Combine(LS_IBU_USER_DOC, EmailBODYFileName);
                BodyText = File.ReadAllText(file_path);

                TextMessage.AppendLine(BodyText);

                BodyText = string.Empty;
                //to_user_id = string.Empty;
                to_email_id = string.Empty;
                //to_user_id = user_id;
                to_email_id = ToAdr;
                TextMessage.AppendLine("------------------------------" + "<br/>");

                TextMessage.AppendLine("User ID:     " + user_id + "<br/>");
                TextMessage.AppendLine("Password:     " + password + "<br/>");

                TextMessage.AppendLine("------------------------------" + "<br/>");


                file_path = LS_IBU_USER_DOC + EmailFooterFileName;
                emailfooter = File.ReadAllText(file_path);

                TextMessage.AppendLine(emailfooter);
                BodyText = TextMessage.ToString();
                string result = LS_EMail.BasicSendMail(
                                        ToDisplayName
                                        , ToAdr
                                        , Subject
                                        , BodyText
                                        , app_id);
                if (result != "1")
                {
                    return "0";
                }
                #endregion
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);
                LG_ERROR_LOG_MAP.Add_Error_Log(FUNC_ID, ex.Source, "ERR_APP_TYPE", "SendEmailAfterUserCreate",
                                          "0000000000", ex.Message, inner4, ex.StackTrace, null, dateTime);

                //ClsIBULog.SaveErrorLogInDb(to_user_id, "Email Sending Failed for Activation:" + to_email_id + " " + ex.Message, "user_activation_email");
                //DO not DO Anything
                return success = "0";
            }
            return success = "1";
        }        
    }
}
