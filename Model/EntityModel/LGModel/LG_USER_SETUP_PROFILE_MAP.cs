using Model.EDMX;
using Model.EntityModel.Common;
using Model.Mapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Web;
using System.Web.Mvc;

namespace Model.EntityModel.LGModel
{
    [DataContract]
    public class LG_USER_SETUP_PROFILE_MAP
    {
        #region Properties

        [DataMember]
        [Required(ErrorMessage = "User ID is required")]
        public string USER_ID { get; set; }

        public string USER_ID_LOCK { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Classification ID is required")]
        public string USER_CLASSIFICATION_ID { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Area ID is required")]
        public string USER_AREA_ID { get; set; }

        [DataMember]
        public string USER_AREA_ID_VALUE { get; set; }

        [DataMember]
        public string USER_NAME { get; set; }

        [DataMember]
        public string AUTH_STATUS_ID { get; set; }

        public DateTime? MAKE_DT { get; set; }

        [DataMember]
        public string USER_DESCRIPTION { get; set; }

        [DataMember]
        public string BRANCH_ID { get; set; }

        [DataMember]
        [MaxLength(15, ErrorMessage = "Acc. No must be within 15 digits")]
        public string ACC_NO { get; set; }

        [DataMember]
        public string FATHERS_NAME { get; set; }

        [DataMember]
        public string MOTHERS_NAME { get; set; }

        [DataMember]
        public DateTime? DOB { get; set; }

        [DataMember]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Email is not in a valid format.")]
        public string MAIL_ADDRESS { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Mob No is required")]
        public string MOB_NO { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Authentication ID is required")]
        public string AUTHENTICATION_ID { get; set; }

        [DataMember]
        public string TERMINAL_IP { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Working Hour Type ID is required")]
        public short? WORKING_HOUR { get; set; }

        [DataMember]
        public string START_TIME { get; set; }

        [DataMember]
        public string END_TIME { get; set; }

        [DataMember]
        public string MAKE_BY { get; set; }

        // for image
        [DataMember]
        public byte[] imageByte { get; set; }

        [DataMember]
        public HttpPostedFileBase File { get; set; }

        [DataMember]
        public string PASSWORD { get; set; }

        [DataMember]
        public string APPLICATION_ID { get; set; }

        [DataMember]
        public string APPLICATION_NAME { get; set; }

        [DataMember]
        public string CLASSIFICATION_NAME { get; set; }

        [DataMember]
        public string AREA_NAME { get; set; }

        [DataMember]
        public string BRANCH_NAME { get; set; }

        [DataMember]
        public string AUTHENTICATION_NAME { get; set; }

        [DataMember]
        public string ERROR { get; set; }

        [DataMember]
        public string TWO_FA_TYPE_ID { get; set; }

        [DataMember]
        public string TWO_FA_TYPE_NAME { get; set; }

        [DataMember]
        public string AUTH_STATU_ID { get; set; }

        [DataMember]
        public string LAST_ACTION { get; set; }

        // for login validation
        [DataMember]
        public short ACTIVE_FLAG_INACTV_USER { get; set; }

        [DataMember]
        public short USER_ID_LOCK_WRNG_ATM { get; set; }

        [DataMember]
        public short ACTIVE_FLAG_MULTI_LOGIN { get; set; }

        [DataMember]
        public string USER_SESSION_ID { get; set; }

        [DataMember]
        public DateTime? USER_START_TIME { get; set; }

        [DataMember]
        public DateTime? USER_LAST_TIME { get; set; }

        [DataMember]
        public short FIRST_LOGIN_FLAG { get; set; }

        [DataMember]
        public string IP_ADDRESS { get; set; }

        [DataMember]
        public int HR { get; set; }

        [DataMember]
        public int MIN { get; set; }

        [DataMember]
        public string FAILED_LOGIN_ATTEMPT { get; set; }
        // END for login validation

        [DataMember]
        public List<SelectListItem> LIST_USER_CLASSIFICATION_FOR_DD { get; set; }

        [DataMember]
        public List<SelectListItem> LIST_USER_AREA_FOR_DD { get; set; }

        [DataMember]
        public List<SelectListItem> LIST_BRANCH_FOR_DD { get; set; }

        [DataMember]
        public List<SelectListItem> LIST_AUTHENTICATION_TYPE_FOR_DD { get; set; }

        [DataMember]
        public List<SelectListItem> LIST_WORKING_HOUR_FOR_DD { get; set; }

        [DataMember]
        public List<SelectListItem> LIST_TWO_FA_TYPE_FOR_DD { get; set; }

        //salekin added bellow
        //public virtual ICollection<LG_FNR_ROLE_DEFINE_MAP> ROLES { get; set; }
        [DataMember]
        public ICollection<LG_USER_ROLE_ASSIGN_MAP> ROLES { get; set; }

        [DataMember]
        public List<LG_FNR_ROLE_PERMISSION_DETAILS_MAP> PERMISSIONS { get; set; }

        [DataMember]
        public List<LG_FNR_MODULE_MAP> LIST_MODULES_FOR_SELECTED_ROLE { get; set; }

        [DataMember]
        public List<LG_MENU_MAP> LIST_MENU_MAP { get; set; }

        [DataMember]
        public List<LG_FNR_FUNCTION_MAP> GetPermittedFunctionsList { get; set; } //add to give the permitted function list for a user for webform application

        public string POLICY_ID { get; set; }

        [DataMember]
        public string MSG { get; set; }

        [DataMember]
        public string LAST_TIME { get; set; }

        [DataMember]

        #endregion Properties

        #region event
             


        public static string FUNCTION_ID = "010102001";

        #region Add

        public static string AddUserProfile(string USER_ID, string pUSER_SESSION_ID, string PCLASSIFICATION_ID, string pAREA_ID, string pAREA_ID_VALUE, string pUSER_NAME, string pUSER_DESCRIPTION, string pBRANCH_ID, string pACC_NO, string pFATHERS_NAME, string pMOTHERS_NAME, string pDOB, string pMAIL_ADDRESS, string pMOB_NO, string pAUTHENTICATION_ID, string pTERMINAL_IP, string pSTART_TIME, string pEND_TIME, string pWORKING_HOUR, string pAPPLICATION_ID, string pAUTH_STATUS_ID,string pROLE_ID)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
                string FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "UserProfileSetup").Select(x => x.FUNCTION_ID).SingleOrDefault();
                LG_USER_SETUP_PROFILE OBJ_LG_USER_SETUP_PROFILE = new LG_USER_SETUP_PROFILE();

                string result = string.Empty;
                string emailSend = string.Empty;

                try
                {
                    string format = OutgoingResponseFormat.GetFormat();
                    OutgoingResponseFormat.SetResponseFormat(format);

                    var exist_user = Obj_DBModelEntities.LG_USER_SETUP_PROFILE.Where(a => a.USER_ID == USER_ID).SingleOrDefault();
                    if (exist_user != null)
                    {
                        return "User already exist";
                    }

                    int serial_no = Convert.ToInt32(Obj_DBModelEntities.LG_USER_SETUP_PROFILE
                                       .Max(x => (int?)x.SL_ID) ?? 0) + 1;

                    OBJ_LG_USER_SETUP_PROFILE = new LG_USER_SETUP_PROFILE();
                    OBJ_LG_USER_SETUP_PROFILE.SL_ID = serial_no;
                    OBJ_LG_USER_SETUP_PROFILE.USER_ID = USER_ID;
                    OBJ_LG_USER_SETUP_PROFILE.USER_CLASSIFICATION_ID = PCLASSIFICATION_ID;
                    OBJ_LG_USER_SETUP_PROFILE.USER_AREA_ID = pAREA_ID;
                    OBJ_LG_USER_SETUP_PROFILE.USER_AREA_ID_VALUE = pAREA_ID_VALUE;
                    OBJ_LG_USER_SETUP_PROFILE.USER_NM = pUSER_NAME;
                    OBJ_LG_USER_SETUP_PROFILE.USER_DESCRIP = pUSER_DESCRIPTION;
                    OBJ_LG_USER_SETUP_PROFILE.BRANCH_ID = (string.IsNullOrWhiteSpace(pBRANCH_ID) || pBRANCH_ID == "null") ? "0000" : pBRANCH_ID;
                    OBJ_LG_USER_SETUP_PROFILE.ACC_NO = pACC_NO;
                    OBJ_LG_USER_SETUP_PROFILE.FATHERS_NM = pFATHERS_NAME;
                    OBJ_LG_USER_SETUP_PROFILE.MOTHERS_NM = pMOTHERS_NAME;
                    if (String.IsNullOrWhiteSpace(pDOB) || pDOB == "null")
                    {
                        OBJ_LG_USER_SETUP_PROFILE.DOB = null;
                    }
                    else
                    {
                        OBJ_LG_USER_SETUP_PROFILE.DOB = Convert.ToDateTime(pDOB);
                    }
                    OBJ_LG_USER_SETUP_PROFILE.MAIL_ADDRESS = pMAIL_ADDRESS;
                    OBJ_LG_USER_SETUP_PROFILE.MOB_NO = pMOB_NO;
                    OBJ_LG_USER_SETUP_PROFILE.AUTHENTICATION_ID = pAUTHENTICATION_ID;
                    OBJ_LG_USER_SETUP_PROFILE.TERMINAL_IP = pTERMINAL_IP;
                    if (!string.IsNullOrWhiteSpace(pWORKING_HOUR) && pWORKING_HOUR != "null")
                    {
                        OBJ_LG_USER_SETUP_PROFILE.WORKING_HOUR = Convert.ToInt16(pWORKING_HOUR);
                    }
                    OBJ_LG_USER_SETUP_PROFILE.START_TIME = pSTART_TIME;
                    OBJ_LG_USER_SETUP_PROFILE.END_TIME = pEND_TIME;

                    //StringBuilder Pass = new StringBuilder();
                    //Random r = new Random();
                    //string str = "abcdefghijkmnpqrstuvwxyz23456789";

                    //int len = 6;
                    //while ((len--) > 0)
                    //    Pass.Append(str[(int)(r.NextDouble() * str.Length)]);

                    //string Pass = System.Web.Security.Membership.GeneratePassword(6, 0);
                    //OBJ_LG_USER_SETUP_PROFILE.PASSWORD = Pass_Encryp.MD5Hash(Pass.ToString());
                    OBJ_LG_USER_SETUP_PROFILE.PASSWORD = Security.GetEncryptedText(); // Mehedi added new encryption for password

                    string Pass = Security.GetPlainText(OBJ_LG_USER_SETUP_PROFILE.PASSWORD);
                    string SMS_TEXT = ConfigurationManager.AppSettings["SMS_TEXT_FOR_NEW_ACCOUNT"].ToString() + Pass.ToString();

                    if (!string.IsNullOrWhiteSpace(pAUTH_STATUS_ID) && pAUTH_STATUS_ID == "A") //User Create without authorization
                    {
                        OBJ_LG_USER_SETUP_PROFILE.AUTH_STATUS_ID = "A";
                    }
                    else
                        OBJ_LG_USER_SETUP_PROFILE.AUTH_STATUS_ID = "U";

                    OBJ_LG_USER_SETUP_PROFILE.LAST_ACTION = "ADD";
                    OBJ_LG_USER_SETUP_PROFILE.MAKE_DT = System.DateTime.Now;
                    OBJ_LG_USER_SETUP_PROFILE.MAKE_BY = pUSER_SESSION_ID;
                    OBJ_LG_USER_SETUP_PROFILE.FAILED_LOGIN_ATTEMPT = "0";
                    OBJ_LG_USER_SETUP_PROFILE.FIRST_LOGIN_FLAG = (short)(!string.IsNullOrWhiteSpace(pAUTH_STATUS_ID) && pAUTH_STATUS_ID == "A" ? 0 : 1); ;
                    OBJ_LG_USER_SETUP_PROFILE.ACTIVE_FLAG_INACTV_USER = (short)(!string.IsNullOrWhiteSpace(pAUTH_STATUS_ID) && pAUTH_STATUS_ID == "A" ? 1 : 0);
                    Obj_DBModelEntities.LG_USER_SETUP_PROFILE.Add(OBJ_LG_USER_SETUP_PROFILE);
                    Obj_DBModelEntities.SaveChanges();
                    LG_USER_SETUP_PROFILE_MAP OBJ_LG_USER_SETUP_PROFILE_MAP = new LG_USER_SETUP_PROFILE_MAP();
                    Class_Conversion.LG_USER_SETUP_PROFILE_REVERSE_CON(OBJ_LG_USER_SETUP_PROFILE_MAP, OBJ_LG_USER_SETUP_PROFILE);

                    if (ConfigurationManager.AppSettings["SMS__NOTIFICATION_ENABLE"].ToString() == "1")
                    {
                        SMS_API.PushSMS(OBJ_LG_USER_SETUP_PROFILE.MOB_NO, SMS_TEXT);
                    }


                    if (pAUTH_STATUS_ID != "A")
                    {
                        #region Auth log

                        LG_AA_NFT_AUTH_LOG_MAP OBJ_LG_AA_NFT_AUTH_LOG_MAP = new LG_AA_NFT_AUTH_LOG_MAP();
                        OBJ_LG_AA_NFT_AUTH_LOG_MAP.FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "UserProfileSetup").Select(x => x.FUNCTION_ID).SingleOrDefault();
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
                        OBJ_LG_AA_NFT_AUTH_LOG_MAP.MAKE_BY = pUSER_SESSION_ID;
                        OBJ_LG_AA_NFT_AUTH_LOG_MAP.MAKE_DT = DateTime.Now;
                        LG_AA_NFT_AUTH_LOG_MAP.AddNftAuthLog(OBJ_LG_USER_SETUP_PROFILE_MAP, OBJ_LG_AA_NFT_AUTH_LOG_MAP);

                        #endregion Auth log
                    }
                    

                    //if (USER_ID != null && pMAIL_ADDRESS != null)
                    //{
                    //    //System.Net.ServicePointManager.ServerCertificateValidationCallback += delegate { return true; };
                    //    EntityModel.TwoFactorAuth.OTP.SendEmailAfterUserCreate(USER_ID, pMAIL_ADDRESS, Pass.ToString(), pAPPLICATION_ID);
                    //}




                    if (!string.IsNullOrWhiteSpace(pAUTH_STATUS_ID) && pAUTH_STATUS_ID == "A" && !string.IsNullOrWhiteSpace(pROLE_ID) && pROLE_ID != "null")   //Role Creation & Email send For Creating authorized user
                    {
                        result = LG_USER_ROLE_ASSIGN_MAP.AddRoleAssignForAuthorizedUser(pROLE_ID, "save", USER_ID, pUSER_SESSION_ID, Obj_DBModelEntities); //Role Creation For authorized user
                        if (string.IsNullOrWhiteSpace(result) || result.ToLower() != "true")
                            return "False";
                        
                        //user create with sending mail
                        if (ConfigurationManager.AppSettings["Send_Email_After_UserCreate"] != null && ConfigurationManager.AppSettings["Send_Email_After_UserCreate"].ToString() == "1" && !string.IsNullOrEmpty(USER_ID) && !string.IsNullOrEmpty(pMAIL_ADDRESS))
                        {
                            emailSend = EntityModel.TwoFactorAuth.OTP.SendEmailAfterUserCreate(USER_ID, pMAIL_ADDRESS, Pass.ToString(), pAPPLICATION_ID);
                            if (emailSend == "1")
                            {
                                ts.Complete();
                                return result = "True";
                            }
                            else if (emailSend == "0")
                                return result = "False";
                            else
                                return emailSend;
                        }
                    }

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
                            result = "Can't Add User Profile(Db).";
                        }
                    }

                    LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "AddUserProfile",
                    "0000000000", dbEx.Message, inner, dbEx.StackTrace, pUSER_SESSION_ID, dateTime);

                    return result;
                }
                catch (Exception ex)
                {
                    ts.Dispose();
                    string dateTime = Convert.ToString(System.DateTime.Now);
                    string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                    LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "AddUserProfile",
                    "0000000000", ex.Message, inner4, ex.StackTrace, pUSER_SESSION_ID, dateTime);

                    result = "Can't Add  User Profile ";
                    return result;
                }
            }
        }

        //public static string AddUserProfile(LG_USER_SETUP_PROFILE_MAP pLG_USER_SETUP_PROFILE_MAP)
        //{
        //    DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
        //    string result = string.Empty;
        //    try
        //    {
        //        //int Pid = Convert.ToInt32(Obj_DBModelEntities.LG_USER_SETUP_PROFILE
        //        //                        .Max(x => x.USER_ID)) + 1;

        //        LG_USER_SETUP_PROFILE OBJ_LG_USER_SETUP_PROFILE = new LG_USER_SETUP_PROFILE();
        //        OBJ_LG_USER_SETUP_PROFILE.USER_ID = pLG_USER_SETUP_PROFILE_MAP.USER_ID;
        //        OBJ_LG_USER_SETUP_PROFILE.CLASSIFICATION_ID = pLG_USER_SETUP_PROFILE_MAP.CLASSIFICATION_ID;
        //        OBJ_LG_USER_SETUP_PROFILE.AREA_ID = pLG_USER_SETUP_PROFILE_MAP.AREA_ID;
        //        OBJ_LG_USER_SETUP_PROFILE.USER_NAME = pLG_USER_SETUP_PROFILE_MAP.USER_NAME;
        //        OBJ_LG_USER_SETUP_PROFILE.USER_DESCRIPTION = pLG_USER_SETUP_PROFILE_MAP.USER_DESCRIPTION;
        //        OBJ_LG_USER_SETUP_PROFILE.EMPLOYEE_ID = pLG_USER_SETUP_PROFILE_MAP.EMPLOYEE_ID;
        //        OBJ_LG_USER_SETUP_PROFILE.BRANCH_ID = pLG_USER_SETUP_PROFILE_MAP.BRANCH_ID;
        //        OBJ_LG_USER_SETUP_PROFILE.CUSTOMER_ID = pLG_USER_SETUP_PROFILE_MAP.CUSTOMER_ID;
        //        OBJ_LG_USER_SETUP_PROFILE.AGENT_ID = pLG_USER_SETUP_PROFILE_MAP.AGENT_ID;
        //        OBJ_LG_USER_SETUP_PROFILE.ACC_NO = pLG_USER_SETUP_PROFILE_MAP.ACC_NO;
        //        OBJ_LG_USER_SETUP_PROFILE.FATHERS_NAME = pLG_USER_SETUP_PROFILE_MAP.FATHERS_NAME;
        //        OBJ_LG_USER_SETUP_PROFILE.MOTHERS_NAME = pLG_USER_SETUP_PROFILE_MAP.MOTHERS_NAME;
        //        //if (String.IsNullOrWhiteSpace(pDOB))
        //        //{
        //        //    OBJ_LG_USER_SETUP_PROFILE.DOB = null;
        //        //}
        //        //else
        //        //{
        //        //    OBJ_LG_USER_SETUP_PROFILE.DOB = Convert.ToDateTime(pDOB);
        //        //}

        //        OBJ_LG_USER_SETUP_PROFILE.DOB = pLG_USER_SETUP_PROFILE_MAP.DOB;
        //        OBJ_LG_USER_SETUP_PROFILE.MAIL_ADDRESS = pLG_USER_SETUP_PROFILE_MAP.MAIL_ADDRESS;
        //        OBJ_LG_USER_SETUP_PROFILE.MOB_NO = pLG_USER_SETUP_PROFILE_MAP.MOB_NO;
        //        OBJ_LG_USER_SETUP_PROFILE.AUTHENTICATION_ID = pLG_USER_SETUP_PROFILE_MAP.AUTHENTICATION_ID;
        //        OBJ_LG_USER_SETUP_PROFILE.TERMINAL_IP = pLG_USER_SETUP_PROFILE_MAP.TERMINAL_IP;
        //        OBJ_LG_USER_SETUP_PROFILE.WORKING_HOUR = Convert.ToInt16(pLG_USER_SETUP_PROFILE_MAP.WORKING_HOUR);
        //        OBJ_LG_USER_SETUP_PROFILE.START_TIME = pLG_USER_SETUP_PROFILE_MAP.START_TIME;
        //        OBJ_LG_USER_SETUP_PROFILE.END_TIME = pLG_USER_SETUP_PROFILE_MAP.END_TIME;
        //        OBJ_LG_USER_SETUP_PROFILE.PASSWORD = "pass";

        //        OBJ_LG_USER_SETUP_PROFILE.AUTH_STATUS_ID = "U";
        //        OBJ_LG_USER_SETUP_PROFILE.LAST_ACTION = "ADD";

        //        OBJ_LG_USER_SETUP_PROFILE.MAKE_DT = System.DateTime.Now;

        //        OBJ_LG_USER_SETUP_PROFILE.FAILED_LOGIN_ATTEMPT = "0";

        //        Obj_DBModelEntities.LG_USER_SETUP_PROFILE.Add(OBJ_LG_USER_SETUP_PROFILE);

        //        Obj_DBModelEntities.SaveChanges();

        //        #region Auth log
        //        LG_AA_NFT_AUTH_LOG_MAP OBJ_LG_AA_NFT_AUTH_LOG_MAP = new LG_AA_NFT_AUTH_LOG_MAP();
        //        OBJ_LG_AA_NFT_AUTH_LOG_MAP.FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "UserProfileSetup").Select(x => x.FUNCTION_ID).SingleOrDefault();
        //        OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_NAME = "LG_USER_SETUP_PROFILE";
        //        OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_NM = "USER_ID";
        //        OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_VAL = OBJ_LG_USER_SETUP_PROFILE.USER_ID;
        //        OBJ_LG_AA_NFT_AUTH_LOG_MAP.ACTION_STATUS = "ADD";
        //        OBJ_LG_AA_NFT_AUTH_LOG_MAP.REMARKS = "";
        //        OBJ_LG_AA_NFT_AUTH_LOG_MAP.PRIMARY_TABLE_FLAG = 0;
        //        OBJ_LG_AA_NFT_AUTH_LOG_MAP.PARENT_TABLE_PK_VAL = null;
        //        OBJ_LG_AA_NFT_AUTH_LOG_MAP.AUTH_STATUS_ID = "U";
        //        int? auth_level_max = LG_AA_NFT_AUTH_LOG_MAP.GetNftAuthLevelMaxFromFunction(OBJ_LG_AA_NFT_AUTH_LOG_MAP.FUNCTION_ID);
        //        if (auth_level_max.HasValue)
        //        {
        //            OBJ_LG_AA_NFT_AUTH_LOG_MAP.AUTH_LEVEL_MAX = (short)auth_level_max;
        //        }
        //        else
        //            OBJ_LG_AA_NFT_AUTH_LOG_MAP.AUTH_LEVEL_MAX = 0;
        //        OBJ_LG_AA_NFT_AUTH_LOG_MAP.AUTH_LEVEL_PENDING = OBJ_LG_AA_NFT_AUTH_LOG_MAP.AUTH_LEVEL_MAX;
        //        OBJ_LG_AA_NFT_AUTH_LOG_MAP.REASON_DECLINE = "";
        //        OBJ_LG_AA_NFT_AUTH_LOG_MAP.MAKE_BY = pLG_USER_SETUP_PROFILE_MAP.USER_SESSION_ID;
        //        OBJ_LG_AA_NFT_AUTH_LOG_MAP.MAKE_DT = DateTime.Now;
        //        LG_AA_NFT_AUTH_LOG_MAP.AddNftAuthLog(OBJ_LG_USER_SETUP_PROFILE, OBJ_LG_AA_NFT_AUTH_LOG_MAP);
        //        #endregion

        //        result = "True";

        //        return result;

        //    }
        //    catch (DbEntityValidationException dbEx)
        //    {
        //        foreach (var validationErrors in dbEx.EntityValidationErrors)
        //        {
        //            foreach (var validationError in validationErrors.ValidationErrors)
        //            {
        //                Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
        //                result = "Can't Add User Profile(Db) " + validationError.ErrorMessage;
        //            }
        //        }
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        result = "Can't Add  User Profile " + ex.Message;
        //        if (ex.InnerException != null)
        //        { result = "Can't Add  User Profile " + ex.InnerException.ToString(); }
        //        return result;
        //    }

        //}

        #endregion Add

        #region fetch single

        public static LG_USER_SETUP_PROFILE_MAP GetUserSetupInfoByUserId(string puser_id)
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
                                             where m.USER_ID != null && m.USER_ID == puser_id && m.LAST_ACTION != "DEL"
                                             select new LG_USER_SETUP_PROFILE_MAP()
                                             {
                                                 USER_ID = m.USER_ID,
                                                 USER_CLASSIFICATION_ID = m.USER_CLASSIFICATION_ID,
                                                 CLASSIFICATION_NAME = s.CLASSIFICATION_NAME,
                                                 USER_AREA_ID = m.USER_AREA_ID,
                                                 USER_AREA_ID_VALUE = m.USER_AREA_ID_VALUE,
                                                 AREA_NAME = a.AREA_NAME,
                                                 USER_NAME = m.USER_NM,
                                                 USER_DESCRIPTION = m.USER_DESCRIP,
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
                                                 ACTIVE_FLAG_INACTV_USER = m.ACTIVE_FLAG_INACTV_USER,
                                                 AUTH_STATU_ID = m.AUTH_STATUS_ID,
                                                 USER_ID_LOCK_WRNG_ATM = m.USER_ID_LOCK_WRNG_ATM
                                                 //  MAKE_DT = m.MAKE_DT
                                             }).SingleOrDefault();

                //if (OBJ_LG_USER_SETUP_PROFILE != null && OBJ_LG_USER_SETUP_PROFILE.AUTH_STATU_ID == "U")
                //{
                //    OBJ_LG_USER_SETUP_PROFILE = new LG_USER_SETUP_PROFILE_MAP();
                //    OBJ_LG_USER_SETUP_PROFILE.AUTH_STATU_ID = "U";
                //    OBJ_LG_USER_SETUP_PROFILE.MSG = "UserId is UnAuthorised.";
                //}
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
                       "0000000000", dbEx.Message, inner, dbEx.StackTrace, "RequestedUserId:" + puser_id, dateTime);

                return OBJ_LG_USER_SETUP_PROFILE;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);
                LG_ERROR_LOG_MAP.Add_Error_Log(null, ex.Source, "ERR_APP_TYPE", "GetUserSetupInfoByUserId",
                       "0000000000", ex.Message, inner4, ex.StackTrace, "RequestedUserId:" + puser_id, dateTime);

                OBJ_LG_USER_SETUP_PROFILE.ERROR = ex.Message;
                return OBJ_LG_USER_SETUP_PROFILE;
            }
        }

        public static LG_USER_SETUP_PROFILE_MAP GetUserSetupInfoByUserIdForRBAC(string pUSER_ID, string pAPP_ID, string pFUNCTION_GROUP_ID)
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            LG_USER_SETUP_PROFILE_MAP OBJ_LG_USER_SETUP_PROFILE = new LG_USER_SETUP_PROFILE_MAP();
            List<LG_MENU_MAP> LIST_LG_MENU_MAP_ALL = new List<LG_MENU_MAP>();
            List<LG_MENU_MAP> LIST_LG_MENU_MAP_SELECTED = new List<LG_MENU_MAP>();
            try
            {
                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                //var obj_lg_user_setup_profile = (from u in Obj_DBModelEntities.LG_USER_SETUP_PROFILE
                //                                 where u.USER_ID == pUSER_ID &&
                //                                       u.AUTH_STATUS_ID != "U" &&
                //                                       u.LAST_ACTION != "DEL"
                //                                 select new LG_USER_SETUP_PROFILE_MAP()
                //                                 {
                //                                     USER_ID = u.USER_ID,
                //                                     USER_NAME = u.USER_NM,
                //                                 });
                //if (obj_lg_user_setup_profile == null)
                //{
                //    return OBJ_LG_USER_SETUP_PROFILE;
                //}

                OBJ_LG_USER_SETUP_PROFILE = (from u in Obj_DBModelEntities.LG_USER_SETUP_PROFILE
                                             where u.USER_ID == pUSER_ID &&
                                                   u.AUTH_STATUS_ID != "U" &&
                                                   u.LAST_ACTION != "DEL"
                                             select new LG_USER_SETUP_PROFILE_MAP()
                                             {
                                                 USER_ID = u.USER_ID,
                                                 USER_NAME = u.USER_NM,
                                                 FIRST_LOGIN_FLAG = u.FIRST_LOGIN_FLAG,
                                             }).FirstOrDefault();
                if (OBJ_LG_USER_SETUP_PROFILE == null)
                {
                    return OBJ_LG_USER_SETUP_PROFILE;
                }

                OBJ_LG_USER_SETUP_PROFILE.ROLES = (from role_a in Obj_DBModelEntities.LG_USER_ROLE_ASSIGN
                                                   where role_a.USER_ID == pUSER_ID &&
                                                         role_a.ROLE_ASSIGN_FLAG == 1 &&
                                                         role_a.AUTH_STATUS_ID != "U" &&
                                                         role_a.LAST_ACTION != "DEL"
                                                   select new LG_USER_ROLE_ASSIGN_MAP
                                                   {
                                                       ROLE_ID = role_a.ROLE_ID,
                                                       ROLE_NAME = role_a.ROLE_NAME,
                                                   }).ToList();

                #region For Web Form Applications

                var app_type = Obj_DBModelEntities.LG_FNR_APPLICATION.Where(x => x.APPLICATION_ID == pAPP_ID).Select(x => x.APP_TYPE_ID).SingleOrDefault();

                if (app_type == 2 && OBJ_LG_USER_SETUP_PROFILE.ROLES.Count() > 0)
                {
                    List<LG_FNR_FUNCTION_MAP> LIST_LG_FNR_FUNCTION_MAP = new List<LG_FNR_FUNCTION_MAP>();
                    bool exists1, first_time1 = true;
                    foreach (LG_USER_ROLE_ASSIGN_MAP OBJ_LG_USER_ROLE_ASSIGN_MAP in OBJ_LG_USER_SETUP_PROFILE.ROLES)
                    {
                        var List_lg_fnr_function_map = (from role_d in Obj_DBModelEntities.LG_FNR_ROLE_DEFINE
                                                        join f in Obj_DBModelEntities.LG_FNR_FUNCTION
                                                        on role_d.FUNCTION_ID equals f.FUNCTION_ID
                                                        join menu in Obj_DBModelEntities.LG_MENU
                                                        on f.FUNCTION_ID equals menu.FUNCTION_ID
                                                        where role_d.ROLE_ID == OBJ_LG_USER_ROLE_ASSIGN_MAP.ROLE_ID &&
                                                              role_d.AUTH_STATUS_ID != "U" && role_d.LAST_ACTION != "DEL" && role_d.ROLE_DEFINE_FLAG == 1 &&
                                                              f.APPLICATION_ID == pAPP_ID && 
                                                              //f.ENABLED_FLAG == 1 &&  //salekin commented
                                                              f.AUTH_STATUS_ID != "U" && f.LAST_ACTION != "DEL" &&
                                                              f.FUNCTION_GROUP_ID == pFUNCTION_GROUP_ID &&
                                                              menu.MENU_ENABLE_FLAG == 1
                                                        select new LG_FNR_FUNCTION_MAP
                                                        {
                                                            MENU_ID = menu.MENU_ID,
                                                            ITEM_TYPE = f.ITEM_TYPE,
                                                            MENU_NM = menu.NAME,
                                                            MENU_LEVEL = menu.MENU_LEVEL,
                                                            PARENT_MENU_ID = menu.PARENTID,
                                                            FUNCTION_ASSIGN_FLAG = menu.FUNCTION_ASSIGN_FLAG,
                                                            FAST_PATH_NO = f.FAST_PATH_NO,
                                                            FUNCTION_ID = role_d.FUNCTION_ID,
                                                            FUNCTION_NM = f.FUNCTION_NM,
                                                            HO_FUNCTION_FLAG = f.HO_FUNCTION_FLAG,
                                                            TARGET_PATH = f.TARGET_PATH,
                                                            DB_ROLE_NAME = f.DB_ROLE_NAME,
                                                            SERVICE_ID = f.SERVICE_ID,
                                                            MODULE_ID = f.MODULE_ID,
                                                            MAINT_CRT_FLAG = role_d.MAINT_CRT_FLAG, //create == add
                                                            MAINT_EDT_FLAG = role_d.MAINT_EDT_FLAG,
                                                            MAINT_DEL_FLAG = role_d.MAINT_DEL_FLAG,
                                                            MAINT_INDX_FLAG = role_d.MAINT_INDX_FLAG, // index == view
                                                            MAINT_AUTH_FLAG = role_d.MAINT_AUTH_FLAG,
                                                            PROCESS_FLAG = f.PROCESS_FLAG,
                                                            REPORT_VIEW_FLAG = role_d.REPORT_VIEW_FLAG,
                                                            REPORT_PRINT_FLAG = role_d.REPORT_PRINT_FLAG,
                                                            REPORT_GEN_FLAG = role_d.REPORT_GEN_FLAG
                                                        }).ToList();

                        //First time all function will be added to List
                        if (first_time1 == true && List_lg_fnr_function_map.Count > 0)
                        {
                            OBJ_LG_USER_SETUP_PROFILE.GetPermittedFunctionsList = List_lg_fnr_function_map;
                        }

                        //From Second time if function doesn't exists to List then add
                        if (first_time1 == false && List_lg_fnr_function_map.Count() > 0)
                        {
                            for (int i = 0; i < List_lg_fnr_function_map.Count(); i++)
                            {
                                exists1 = false;
                                for (int j = 0; j < OBJ_LG_USER_SETUP_PROFILE.GetPermittedFunctionsList.Count(); j++)
                                {
                                    if (List_lg_fnr_function_map[i].FUNCTION_ID == OBJ_LG_USER_SETUP_PROFILE.GetPermittedFunctionsList[j].FUNCTION_ID)  //Bug for AuditTrail as same name exists in 2 application
                                    {
                                        exists1 = true;
                                        break;
                                    }
                                }
                                if (!exists1)
                                {
                                    OBJ_LG_USER_SETUP_PROFILE.GetPermittedFunctionsList.Add(List_lg_fnr_function_map[i]);
                                }
                            }
                        }
                        if (first_time1 == true && List_lg_fnr_function_map.Count > 0)
                        {
                            first_time1 = false;
                        }
                    }
                }

                #endregion For Web Form Applications

                else
                {
                    var permisstion_list = (from u in Obj_DBModelEntities.LG_USER_SETUP_PROFILE
                                            join role_a in Obj_DBModelEntities.LG_USER_ROLE_ASSIGN
                                            on u.USER_ID equals role_a.USER_ID
                                            join role_d in Obj_DBModelEntities.LG_FNR_ROLE_DEFINE
                                            on role_a.ROLE_ID equals role_d.ROLE_ID
                                            join permission in Obj_DBModelEntities.LG_FNR_ROLE_PERMISSION
                                            on role_a.ROLE_ID equals permission.ROLE_ID
                                            join permission_d in Obj_DBModelEntities.LG_FNR_ROLE_PERMISSION_DETAILS
                                            on permission.PERMISSION_ID equals permission_d.PERMISSION_ID
                                            join function in Obj_DBModelEntities.LG_FNR_FUNCTION
                                            on permission_d.FUNCTION_ID equals function.FUNCTION_ID
                                            where
                                                  u.USER_ID == pUSER_ID && u.AUTH_STATUS_ID != "U" && u.LAST_ACTION != "DEL" &&
                                                  role_a.ROLE_ASSIGN_FLAG == 1 && role_a.AUTH_STATUS_ID != "U" && role_a.LAST_ACTION != "DEL" &&
                                                  role_d.ROLE_DEFINE_FLAG == 1 && role_d.AUTH_STATUS_ID != "U" && role_d.LAST_ACTION != "DEL" &&
                                                  permission.ROLE_DEFINE_PERMISSION_FLAG == 1 && permission.AUTH_STATUS_ID != "U" && permission.LAST_ACTION != "DEL" &&
                                                  permission_d.FUNCTION_PERMISSION_FLAG == 1 && permission_d.AUTH_STATUS_ID != "U" && permission_d.LAST_ACTION != "DEL" &&
                                                  function.APPLICATION_ID == pAPP_ID && //Newly added by salekin
                                                  permission_d.FUNCTION_ID == role_d.FUNCTION_ID   //salekin added 20.12.2017
                                            group permission_d by new { permission_d.PERMISSION_ID, permission_d.PERMISSION_DETAILS } into T
                                            select new LG_FNR_ROLE_PERMISSION_DETAILS_MAP
                                            {
                                                PERMISSION_ID = T.Key.PERMISSION_ID,
                                                PERMISSION_DETAILS = T.Key.PERMISSION_DETAILS,
                                            });
                    OBJ_LG_USER_SETUP_PROFILE.PERMISSIONS = permisstion_list.ToList();

                    #region Get ModuleIds For all selected roles(ModuleIds = Menu permission list)

                    bool exists, first_time = true;
                    foreach (LG_USER_ROLE_ASSIGN_MAP OBJ_LG_USER_ROLE_ASSIGN_MAP in OBJ_LG_USER_SETUP_PROFILE.ROLES)
                    {
                        var List_lg_fnr_module_map = (from role_a in Obj_DBModelEntities.LG_USER_ROLE_ASSIGN
                                                      join role_d in Obj_DBModelEntities.LG_FNR_ROLE_DEFINE
                                                      on role_a.ROLE_ID equals role_d.ROLE_ID
                                                      join f in Obj_DBModelEntities.LG_FNR_FUNCTION
                                                      on role_d.FUNCTION_ID equals f.FUNCTION_ID
                                                      join m in Obj_DBModelEntities.LG_FNR_MODULE
                                                      on f.MODULE_ID equals m.MODULE_ID
                                                      join s in Obj_DBModelEntities.LG_FNR_SERVICE
                                                      on f.SERVICE_ID equals s.SERVICE_ID

                                                      where role_a.ROLE_ID == OBJ_LG_USER_ROLE_ASSIGN_MAP.ROLE_ID &&
                                                            f.SERVICE_ID == m.SERVICE_ID &&
                                                            f.APPLICATION_ID == s.APPLICATION_ID &&
                                                            m.AUTH_STATUS_ID != "U" && m.LAST_ACTION != "DEL" &&
                                                            role_d.AUTH_STATUS_ID != "U" && role_d.LAST_ACTION != "DEL" && role_d.ROLE_DEFINE_FLAG == 1 &&
                                                            m.APPLICATION_ID == pAPP_ID && role_d.APPLICATION_ID == pAPP_ID //added by salekin-27.11.2017
                                                      group m by new { m.MODULE_ID, m.MODULE_NM } into T
                                                      select new LG_FNR_MODULE_MAP
                                                      {
                                                          MODULE_ID = T.Key.MODULE_ID,
                                                          MODULE_NM = T.Key.MODULE_NM
                                                      });
                        List<LG_FNR_MODULE_MAP> LIST_LG_FNR_MODULE_MAP = List_lg_fnr_module_map.ToList();

                        //First time all module will be added to List
                        if (first_time == true && LIST_LG_FNR_MODULE_MAP.Count > 0)
                        {
                            OBJ_LG_USER_SETUP_PROFILE.LIST_MODULES_FOR_SELECTED_ROLE = LIST_LG_FNR_MODULE_MAP;
                        }

                        //From Second time if module doesn't exists to List then add
                        if (first_time == false && LIST_LG_FNR_MODULE_MAP.Count() > 0)
                        {
                            for (int i = 0; i < LIST_LG_FNR_MODULE_MAP.Count(); i++)
                            {
                                exists = false;
                                for (int j = 0; j < OBJ_LG_USER_SETUP_PROFILE.LIST_MODULES_FOR_SELECTED_ROLE.Count(); j++)
                                {
                                    if (LIST_LG_FNR_MODULE_MAP[i].MODULE_NM == OBJ_LG_USER_SETUP_PROFILE.LIST_MODULES_FOR_SELECTED_ROLE[j].MODULE_NM)  //Bug for AuditTrail as same name exists in 2 application
                                    {
                                        exists = true;
                                        break;
                                    }
                                }
                                if (!exists)
                                {
                                    OBJ_LG_USER_SETUP_PROFILE.LIST_MODULES_FOR_SELECTED_ROLE.Add(LIST_LG_FNR_MODULE_MAP[i]);
                                }
                            }
                        }
                        if (first_time == true && LIST_LG_FNR_MODULE_MAP.Count > 0)
                        {
                            first_time = false;
                        }
                    }

                    #endregion Get ModuleIds For all selected roles(ModuleIds = Menu permission list)

                    #region Dynamic menu

                    /*
                var Permitted_functions = (from u in Obj_DBModelEntities.LG_USER_SETUP_PROFILE
                                           join role_a in Obj_DBModelEntities.LG_USER_ROLE_ASSIGN
                                           on u.USER_ID equals role_a.USER_ID
                                           join role_d in Obj_DBModelEntities.LG_FNR_ROLE_DEFINE
                                           on role_a.ROLE_ID equals role_d.ROLE_ID
                                           join permission in Obj_DBModelEntities.LG_FNR_ROLE_PERMISSION
                                           on role_a.ROLE_ID equals permission.ROLE_ID
                                           join permission_d in Obj_DBModelEntities.LG_FNR_ROLE_PERMISSION_DETAILS
                                           on permission.PERMISSION_ID equals permission_d.PERMISSION_ID
                                           join menu in Obj_DBModelEntities.LG_MENU
                                           on permission_d.FUNCTION_ID equals menu.FUNCTION_ID
                                           where
                                                 u.USER_ID == pUSER_ID && u.AUTH_STATUS_ID != "U" && u.LAST_ACTION != "DEL" &&
                                                 role_a.ROLE_ASSIGN_FLAG == 1 && role_a.AUTH_STATUS_ID != "U" && role_a.LAST_ACTION != "DEL" &&
                                                 role_d.ROLE_DEFINE_FLAG == 1 && role_d.AUTH_STATUS_ID != "U" && role_d.LAST_ACTION != "DEL" &&
                                                 permission.ROLE_DEFINE_PERMISSION_FLAG == 1 && permission.AUTH_STATUS_ID != "U" && permission.LAST_ACTION != "DEL" &&
                                                 permission_d.FUNCTION_PERMISSION_FLAG == 1 && permission_d.AUTH_STATUS_ID != "U" && permission_d.LAST_ACTION != "DEL" &&
                                                 menu.APP_ID == pAPP_ID
                                           group menu by new { menu.FUNCTION_ID } into T
                                           select new LG_MENU_MAP
                                           {
                                               MENU_ID = T.FirstOrDefault().MENU_ID,
                                               NAME = T.FirstOrDefault().NAME,
                                               DESCRIPTION = T.FirstOrDefault().DESCRIPTION,
                                               CONTROLLER = T.FirstOrDefault().CONTROLLER,
                                               ACTION = T.FirstOrDefault().ACTION,
                                               URL = T.FirstOrDefault().URL,
                                               PARENTID = T.FirstOrDefault().PARENTID,
                                               FUNCTION_ID = T.FirstOrDefault().FUNCTION_ID,
                                           }); */

                    var List_lg_menu_map_all = (from menu in Obj_DBModelEntities.LG_MENU
                                                where menu.APP_ID == pAPP_ID &&
                                                      menu.MENU_ENABLE_FLAG == 1
                                                select new LG_MENU_MAP
                                                {
                                                    MENU_ID = menu.MENU_ID,
                                                    NAME = menu.NAME,
                                                    DESCRIPTION = menu.DESCRIPTION,
                                                    CONTROLLER = menu.CONTROLLER,
                                                    ACTION = menu.ACTION,
                                                    URL = menu.URL,
                                                    PARENTID = menu.PARENTID,
                                                    FUNCTION_ID = menu.FUNCTION_ID,
                                                });

                    LIST_LG_MENU_MAP_ALL = List_lg_menu_map_all.ToList();

                    if (LIST_LG_MENU_MAP_ALL != null && OBJ_LG_USER_SETUP_PROFILE.LIST_MODULES_FOR_SELECTED_ROLE != null)
                    {
                        for (int i = 0; i < OBJ_LG_USER_SETUP_PROFILE.LIST_MODULES_FOR_SELECTED_ROLE.Count(); i++) //Module Check - Main Menu header
                        {
                            for (int j = 0; j < LIST_LG_MENU_MAP_ALL.Count(); j++)
                            {
                                if (LIST_LG_MENU_MAP_ALL[j].NAME.ToLower() == OBJ_LG_USER_SETUP_PROFILE.LIST_MODULES_FOR_SELECTED_ROLE[i].MODULE_NM.ToLower())
                                {
                                    var match = LIST_LG_MENU_MAP_SELECTED.FirstOrDefault(x => x.NAME.ToLower().Contains(LIST_LG_MENU_MAP_ALL[j].NAME.ToLower()));
                                    if (match == null)
                                    {
                                        LIST_LG_MENU_MAP_SELECTED.Add(LIST_LG_MENU_MAP_ALL[j]);
                                    }
                                }
                            }
                        }
                    }

                    var Permitted_functions = (from u in Obj_DBModelEntities.LG_USER_SETUP_PROFILE
                                               join role_a in Obj_DBModelEntities.LG_USER_ROLE_ASSIGN
                                               on u.USER_ID equals role_a.USER_ID
                                               join role_d in Obj_DBModelEntities.LG_FNR_ROLE_DEFINE
                                               on role_a.ROLE_ID equals role_d.ROLE_ID
                                               join permission in Obj_DBModelEntities.LG_FNR_ROLE_PERMISSION
                                               on role_a.ROLE_ID equals permission.ROLE_ID
                                               join permission_d in Obj_DBModelEntities.LG_FNR_ROLE_PERMISSION_DETAILS
                                               on permission.PERMISSION_ID equals permission_d.PERMISSION_ID
                                               join menu in Obj_DBModelEntities.LG_MENU
                                               on permission_d.FUNCTION_ID equals menu.FUNCTION_ID  
                                               where
                                                     u.USER_ID == pUSER_ID && u.AUTH_STATUS_ID != "U" && u.LAST_ACTION != "DEL" &&
                                                     role_a.ROLE_ASSIGN_FLAG == 1 && role_a.AUTH_STATUS_ID != "U" && role_a.LAST_ACTION != "DEL" &&
                                                     role_d.ROLE_DEFINE_FLAG == 1 && role_d.AUTH_STATUS_ID == "A" &&
                                                     permission.ROLE_DEFINE_PERMISSION_FLAG == 1 && permission.AUTH_STATUS_ID != "U" && permission.LAST_ACTION != "DEL" &&
                                                     permission_d.FUNCTION_PERMISSION_FLAG == 1 && permission_d.AUTH_STATUS_ID != "U" && permission_d.LAST_ACTION != "DEL" &&
                                                     menu.APP_ID == pAPP_ID && menu.MENU_ENABLE_FLAG == 1 && role_d.FUNCTION_ID == menu.FUNCTION_ID
                                               group menu by new { menu.FUNCTION_ID } into T
                                               select T.Key.FUNCTION_ID);

                    var Permitted_functions1 = Permitted_functions.ToList();
                    if (LIST_LG_MENU_MAP_ALL != null && Permitted_functions1 != null)
                    {
                        for (int i = 0; i < Permitted_functions1.Count(); i++)
                        {
                            for (int j = 0; j < LIST_LG_MENU_MAP_ALL.Count(); j++)
                            {
                                if (LIST_LG_MENU_MAP_ALL[j].FUNCTION_ID == Permitted_functions1[i])
                                {
                                    string test = Permitted_functions1[i];
                                    var match = LIST_LG_MENU_MAP_SELECTED.FirstOrDefault(x => x.FUNCTION_ID != null && x.FUNCTION_ID.Contains(LIST_LG_MENU_MAP_ALL[j].FUNCTION_ID));
                                    if (match == null)
                                    {
                                        LIST_LG_MENU_MAP_SELECTED.Add(LIST_LG_MENU_MAP_ALL[j]);
                                    }
                                }
                            }
                        }
                    }

                    if (LIST_LG_MENU_MAP_SELECTED != null)
                    {
                        OBJ_LG_USER_SETUP_PROFILE.LIST_MENU_MAP = LIST_LG_MENU_MAP_SELECTED.OrderBy(x => x.MENU_ID).ToList();
                    }

                    /*
                    var menusource = new List<LG_MENU_MAP>();
                    menusource.Add(new LG_MENU_MAP { MENU_ID = 1, NAME = "Access Control", DESCRIPTION = null, ACTION = "Index", CONTROLLER = "Home", URL = null, PARENTID = 0 });
                    menusource.Add(new LG_MENU_MAP { MENU_ID = 2, NAME = "User Management", DESCRIPTION = null, ACTION = "Index", CONTROLLER = "Home", URL = null, PARENTID = 0 });
                    menusource.Add(new LG_MENU_MAP { MENU_ID = 3, NAME = "Credential Management", DESCRIPTION = null, ACTION = "Index", CONTROLLER = "Home", URL = null, PARENTID = 0 });
                    menusource.Add(new LG_MENU_MAP { MENU_ID = 4, NAME = "Audit Trail", DESCRIPTION = null, ACTION = "Index", CONTROLLER = "Home", URL = null, PARENTID = 0 });
                    menusource.Add(new LG_MENU_MAP { MENU_ID = 5, NAME = "Configure Settings", DESCRIPTION = null, ACTION = "Index", CONTROLLER = "Home", URL = null, PARENTID = 0 });
                    menusource.Add(new LG_MENU_MAP { MENU_ID = 6, NAME = "Approval Authentication", DESCRIPTION = null, ACTION = "Index", CONTROLLER = "Home", URL = null, PARENTID = 0 });
                    menusource.Add(new LG_MENU_MAP { MENU_ID = 7, NAME = "Application", DESCRIPTION = null, ACTION = "Index", CONTROLLER = "Application", URL = null, PARENTID = 1 });
                    menusource.Add(new LG_MENU_MAP { MENU_ID = 8, NAME = "Service", DESCRIPTION = null, ACTION = "Index", CONTROLLER = "Service", URL = null, PARENTID = 1 });
                    menusource.Add(new LG_MENU_MAP { MENU_ID = 9, NAME = "Module", DESCRIPTION = null, ACTION = "Index", CONTROLLER = "Module", URL = null, PARENTID = 1 });
                    menusource.Add(new LG_MENU_MAP { MENU_ID = 10, NAME = "Function", DESCRIPTION = null, ACTION = "Index", CONTROLLER = "Function", URL = null, PARENTID = 1 });
                    menusource.Add(new LG_MENU_MAP { MENU_ID = 11, NAME = "Role", DESCRIPTION = null, ACTION = "Index", CONTROLLER = "Role", URL = null, PARENTID = 1 });
                    menusource.Add(new LG_MENU_MAP { MENU_ID = 12, NAME = "Role Define", DESCRIPTION = null, ACTION = "Index", CONTROLLER = "RoleDefine", URL = null, PARENTID = 1 });

                    menusource.Add(new LG_MENU_MAP { MENU_ID = 13, NAME = "User Profile Setup", DESCRIPTION = null, ACTION = "Index", CONTROLLER = "UserProfileSetup", URL = null, PARENTID = 2 });
                    menusource.Add(new LG_MENU_MAP { MENU_ID = 14, NAME = "Role Assign", DESCRIPTION = null, ACTION = "Index", CONTROLLER = "RoleAssign", URL = null, PARENTID = 2 });
                    menusource.Add(new LG_MENU_MAP { MENU_ID = 15, NAME = "User Status", DESCRIPTION = null, ACTION = "Index", CONTROLLER = "UserStatus", URL = null, PARENTID = 2 });
                    menusource.Add(new LG_MENU_MAP { MENU_ID = 16, NAME = "User File Upload", DESCRIPTION = null, ACTION = "Index", CONTROLLER = "UserFileUpload", URL = null, PARENTID = 2 });
                    menusource.Add(new LG_MENU_MAP { MENU_ID = 17, NAME = "Session Initialize", DESCRIPTION = null, ACTION = "Index", CONTROLLER = "SessionInitialize", URL = null, PARENTID = 2 });
                    menusource.Add(new LG_MENU_MAP { MENU_ID = 18, NAME = "Bind AD User", DESCRIPTION = null, ACTION = "Index", CONTROLLER = "BindADUser", URL = null, PARENTID = 2 });
                    menusource.Add(new LG_MENU_MAP { MENU_ID = 19, NAME = "Password Policy", DESCRIPTION = null, ACTION = "Index", CONTROLLER = "PasswordPolicy", URL = null, PARENTID = 3 });
                    menusource.Add(new LG_MENU_MAP { MENU_ID = 20, NAME = "Password Change", DESCRIPTION = null, ACTION = "Index", CONTROLLER = "PasswordChange", URL = null, PARENTID = 3 });
                    menusource.Add(new LG_MENU_MAP { MENU_ID = 21, NAME = "Password Reset", DESCRIPTION = null, ACTION = "Index", CONTROLLER = "PasswordReset", URL = null, PARENTID = 3 });
                    menusource.Add(new LG_MENU_MAP { MENU_ID = 22, NAME = "Audit Trail", DESCRIPTION = null, ACTION = "Index", CONTROLLER = "AuditTrail", URL = null, PARENTID = 4 });
                    menusource.Add(new LG_MENU_MAP { MENU_ID = 23, NAME = "Mail Server", DESCRIPTION = null, ACTION = "Index", CONTROLLER = "MailServer", URL = null, PARENTID = 5 });
                    menusource.Add(new LG_MENU_MAP { MENU_ID = 24, NAME = "OTP", DESCRIPTION = null, ACTION = "Index", CONTROLLER = "OTP", URL = null, PARENTID = 5 });
                    menusource.Add(new LG_MENU_MAP { MENU_ID = 25, NAME = "Calendar Type", DESCRIPTION = null, ACTION = "Index", CONTROLLER = "CalendarType", URL = null, PARENTID = 5 });
                    menusource.Add(new LG_MENU_MAP { MENU_ID = 26, NAME = "Holiday Type", DESCRIPTION = null, ACTION = "Index", CONTROLLER = "HolidayType", URL = null, PARENTID = 5 });
                    menusource.Add(new LG_MENU_MAP { MENU_ID = 27, NAME = "Authorization", DESCRIPTION = null, ACTION = "Index", CONTROLLER = "Authorization", URL = null, PARENTID = 6 });
                    OBJ_LG_USER_SETUP_PROFILE.LIST_MENU_MAP = menusource;
                    */

                    #endregion Dynamic menu
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

                LG_ERROR_LOG_MAP.Add_Error_Log(null, dbEx.Source, "ERR_APP_TYPE", "GetUserSetupInfoByUserIdForRBAC",
       "0000000000", dbEx.Message, inner, dbEx.StackTrace, "RequestedUserId:" + pUSER_ID, dateTime);

                return OBJ_LG_USER_SETUP_PROFILE;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                LG_ERROR_LOG_MAP.Add_Error_Log(null, ex.Source, "ERR_APP_TYPE", "GetUserSetupInfoByUserIdForRBAC",
                       "0000000000", ex.Message, inner4, ex.StackTrace, "RequestedUserId:" + pUSER_ID, dateTime);

                OBJ_LG_USER_SETUP_PROFILE.ERROR = ex.Message;
                return OBJ_LG_USER_SETUP_PROFILE;
            }
        }

        #endregion fetch single

        #region fetch all

        public static IEnumerable<LG_USER_SETUP_PROFILE_MAP> GetAllUserSetupInfo()
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            List<LG_USER_SETUP_PROFILE_MAP> LIST_LG_USER_SETUP_PROFILE = null;
            LG_USER_SETUP_PROFILE_MAP OBJ_LG_USER_SETUP_PROFILE = new LG_USER_SETUP_PROFILE_MAP();

            try
            {
                Obj_DBModelEntities.Configuration.LazyLoadingEnabled = false;
                Obj_DBModelEntities.Configuration.ProxyCreationEnabled = false;

                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                LIST_LG_USER_SETUP_PROFILE = (from m in Obj_DBModelEntities.LG_USER_SETUP_PROFILE
                                              join s in Obj_DBModelEntities.LG_USER_CLASSIFACTION
                                              on m.USER_CLASSIFICATION_ID equals s.CLASSIFICATION_ID
                                              join a in Obj_DBModelEntities.LG_USER_AREA
                                              on m.USER_AREA_ID equals a.AREA_ID
                                              join c in Obj_DBModelEntities.LG_SYS_BRANCH_HOME_BANK
                                              on m.BRANCH_ID equals c.BRANCH_ID
                                              join d in Obj_DBModelEntities.LG_AA_AUTHENTICATION_TYPE
                                              on m.AUTHENTICATION_ID equals d.AUTHENTICATION_ID
                                              where m.LAST_ACTION != "DEL" && m.AUTH_STATUS_ID == "A"
                                              orderby m.MAKE_DT descending

                                              select new LG_USER_SETUP_PROFILE_MAP
                                              {
                                                  USER_ID = m.USER_ID,
                                                  USER_CLASSIFICATION_ID = m.USER_CLASSIFICATION_ID,
                                                  CLASSIFICATION_NAME = s.CLASSIFICATION_NAME,
                                                  USER_AREA_ID = m.USER_AREA_ID,
                                                  USER_AREA_ID_VALUE = m.USER_AREA_ID_VALUE,
                                                  AREA_NAME = a.AREA_NAME,
                                                  USER_NAME = m.USER_NM,
                                                  USER_DESCRIPTION = m.USER_DESCRIP,
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
                                                  WORKING_HOUR = m.WORKING_HOUR,
                                                  START_TIME = m.START_TIME,
                                                  END_TIME = m.END_TIME,
                                                  // MAKE_DT = m.MAKE_DT
                                              }).ToList();
                return LIST_LG_USER_SETUP_PROFILE;
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
                        LIST_LG_USER_SETUP_PROFILE.Add(OBJ_LG_USER_SETUP_PROFILE);
                    }
                }

                LG_ERROR_LOG_MAP.Add_Error_Log(null, dbEx.Source, "ERR_APP_TYPE", "GetAllUserSetupInfo",
                                       "0000000000", dbEx.Message, inner, dbEx.StackTrace, null, dateTime);

                return LIST_LG_USER_SETUP_PROFILE;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                LG_ERROR_LOG_MAP.Add_Error_Log(null, ex.Source, "ERR_APP_TYPE", "GetAllUserSetupInfo",
                                       "0000000000", ex.Message, inner4, ex.StackTrace, null, dateTime);

                //OBJ_LG_USER_SETUP_PROFILE.ERROR = ex.Message;
                LIST_LG_USER_SETUP_PROFILE.Add(OBJ_LG_USER_SETUP_PROFILE);
                return LIST_LG_USER_SETUP_PROFILE;
            }
        }

        public static IEnumerable<LG_USER_SETUP_PROFILE_MAP> GetAllUserSession(string pSession_User, string pAPPLICATION_ID)
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            List<LG_USER_SETUP_PROFILE_MAP> LIST_LG_USER_SETUP_PROFILE = new List<LG_USER_SETUP_PROFILE_MAP>();
            List<LG_USER_SETUP_PROFILE_MAP> LIST_LG_USER_SETUP_PROFILE1 = new List<LG_USER_SETUP_PROFILE_MAP>();
            LG_USER_SETUP_PROFILE_MAP OBJ_LG_USER_SETUP_PROFILE = new LG_USER_SETUP_PROFILE_MAP();
            LG_SYS_SESSION_TRACKER OBJ_LG_SYS_SESSION_TRACKER = new LG_SYS_SESSION_TRACKER();

            try
            {
                Obj_DBModelEntities.Configuration.LazyLoadingEnabled = false;
                Obj_DBModelEntities.Configuration.ProxyCreationEnabled = false;

                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                LIST_LG_USER_SETUP_PROFILE = (from u in Obj_DBModelEntities.LG_USER_SETUP_PROFILE
                                              join s in Obj_DBModelEntities.LG_SYS_SESSION_TRACKER
                                              on u.USER_ID equals s.USER_ID
                                              where u.LAST_ACTION != "DEL" && u.AUTH_STATUS_ID == "A" &&
                                                    s.APPLICATION_ID == pAPPLICATION_ID &&
                                                    u.USER_ID != pSession_User &&
                                                    u.ACTIVE_FLAG_MULTI_LOGIN == 1
                                              group s by s.USER_ID into T
                                              //orderby s.LAST_ACCESS_TIME descending
                                              select new LG_USER_SETUP_PROFILE_MAP
                                              {
                                                  USER_ID = T.Key
                                              }).ToList();

                foreach (var LG_USER_SETUP_PROFILE_MAP in LIST_LG_USER_SETUP_PROFILE.ToList())
                {
                    OBJ_LG_SYS_SESSION_TRACKER = new LG_SYS_SESSION_TRACKER();
                    OBJ_LG_SYS_SESSION_TRACKER = (from s in Obj_DBModelEntities.LG_SYS_SESSION_TRACKER
                                                  where s.USER_ID == LG_USER_SETUP_PROFILE_MAP.USER_ID &&
                                                        s.APPLICATION_ID == pAPPLICATION_ID
                                                        // s.ACTIVE_FLAG_FOR_MULTI_LOGIN == 1 //commented by shohan (04-02-2018)
                                                  orderby s.LAST_ACCESS_TIME descending
                                                  select s).FirstOrDefault();

                    if (OBJ_LG_SYS_SESSION_TRACKER != null)
                    {
                        OBJ_LG_USER_SETUP_PROFILE = new LG_USER_SETUP_PROFILE_MAP();
                        OBJ_LG_USER_SETUP_PROFILE.USER_ID = OBJ_LG_SYS_SESSION_TRACKER.USER_ID;
                        OBJ_LG_USER_SETUP_PROFILE.IP_ADDRESS = OBJ_LG_SYS_SESSION_TRACKER.IP_ADDRESS;
                        OBJ_LG_USER_SETUP_PROFILE.START_TIME = OBJ_LG_SYS_SESSION_TRACKER.START_TIME.ToString();
                        OBJ_LG_USER_SETUP_PROFILE.USER_LAST_TIME = OBJ_LG_SYS_SESSION_TRACKER.LAST_ACCESS_TIME;
                        OBJ_LG_USER_SETUP_PROFILE.LAST_TIME = OBJ_LG_SYS_SESSION_TRACKER.LAST_ACCESS_TIME.ToString();
                        LIST_LG_USER_SETUP_PROFILE1.Add(OBJ_LG_USER_SETUP_PROFILE);
                    }
                }

                //========================
                //var m = from s in Obj_DBModelEntities.LG_SYS_SESSION_TRACKER
                //                             join r in Obj_DBModelEntities.LG_USER_SETUP_PROFILE
                //                             on s.USER_ID equals r.USER_ID
                //                             group s by s.USER_ID into g
                //        select new LG_USER_SETUP_PROFILE_MAP {
                //            USER_LAST_TIME =

                //                                 //g.OrderByDescending(r => r.LAST_ACCESS_TIME)
                //                             }.ToList().First();

                //LG_USER_SETUP_PROFILE List = new LG_USER_SETUP_PROFILE();
                //List.Add(m);
                //========================

                return LIST_LG_USER_SETUP_PROFILE1;
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
                        LIST_LG_USER_SETUP_PROFILE.Add(OBJ_LG_USER_SETUP_PROFILE);
                    }
                }

                LG_ERROR_LOG_MAP.Add_Error_Log(null, dbEx.Source, "ERR_APP_TYPE", "GetUserSession",
                                       "0000000000", dbEx.Message, inner, dbEx.StackTrace, null, dateTime);

                return LIST_LG_USER_SETUP_PROFILE;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                LG_ERROR_LOG_MAP.Add_Error_Log(null, ex.Source, "ERR_APP_TYPE", "GetUserSession",
                                       "0000000000", ex.Message, inner4, ex.StackTrace, null, dateTime);

                //OBJ_LG_USER_SETUP_PROFILE.ERROR = ex.Message;
                LIST_LG_USER_SETUP_PROFILE.Add(OBJ_LG_USER_SETUP_PROFILE);
                return LIST_LG_USER_SETUP_PROFILE;
            }
        }

        public static LG_USER_SETUP_PROFILE_MAP GetSpecificUserSession(string puser_id)
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            LG_USER_SETUP_PROFILE_MAP OBJ_LG_USER_SETUP_PROFILE = new LG_USER_SETUP_PROFILE_MAP();

            try
            {
                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                OBJ_LG_USER_SETUP_PROFILE = (from m in Obj_DBModelEntities.LG_USER_SETUP_PROFILE

                                             where m.USER_ID == puser_id && m.ACTIVE_FLAG_MULTI_LOGIN == 1 &&
                                                   m.AUTH_STATUS_ID == "A" && m.LAST_ACTION != "DEL"
                                             select new LG_USER_SETUP_PROFILE_MAP()
                                             {
                                                 USER_ID = m.USER_ID,
                                                 ACTIVE_FLAG_MULTI_LOGIN = m.ACTIVE_FLAG_MULTI_LOGIN
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

                LG_ERROR_LOG_MAP.Add_Error_Log(null, dbEx.Source, "ERR_APP_TYPE", "GetUserSetupInfoByUserId",
                       "0000000000", dbEx.Message, inner, dbEx.StackTrace, "RequestedUserId:" + puser_id, dateTime);

                return OBJ_LG_USER_SETUP_PROFILE;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);
                LG_ERROR_LOG_MAP.Add_Error_Log(null, ex.Source, "ERR_APP_TYPE", "GetUserSetupInfoByUserId",
                       "0000000000", ex.Message, inner4, ex.StackTrace, "RequestedUserId:" + puser_id, dateTime);

                OBJ_LG_USER_SETUP_PROFILE.ERROR = ex.Message;
                return OBJ_LG_USER_SETUP_PROFILE;
            }
        }


        public static IEnumerable<LG_USER_SETUP_PROFILE_MAP> GetAllNewlyCreatedUser()
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            List<LG_USER_SETUP_PROFILE_MAP> LIST_LG_USER_SETUP_PROFILE = null;
            LG_USER_SETUP_PROFILE_MAP OBJ_LG_USER_SETUP_PROFILE = new LG_USER_SETUP_PROFILE_MAP();

            try
            {
                Obj_DBModelEntities.Configuration.LazyLoadingEnabled = false;
                Obj_DBModelEntities.Configuration.ProxyCreationEnabled = false;

                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                LIST_LG_USER_SETUP_PROFILE = (from m in Obj_DBModelEntities.LG_USER_SETUP_PROFILE                                              
                                              where 
                                              m.AUTH_STATUS_ID == "A" &&
                                              m.FIRST_LOGIN_FLAG == 1 &&
                                              m.USER_CLASSIFICATION_ID == "3"
                                              orderby m.USER_ID ascending
                                              select new LG_USER_SETUP_PROFILE_MAP
                                              {
                                                  USER_ID = m.USER_ID,
                                                  USER_CLASSIFICATION_ID = m.USER_CLASSIFICATION_ID,                                                  
                                                  USER_AREA_ID = m.USER_AREA_ID,
                                                  USER_AREA_ID_VALUE = m.USER_AREA_ID_VALUE,                                                  
                                                  USER_NAME = m.USER_NM,
                                                  USER_DESCRIPTION = m.USER_DESCRIP,
                                                  BRANCH_ID = m.BRANCH_ID,                                                  
                                                  ACC_NO = m.ACC_NO,
                                                  FATHERS_NAME = m.FATHERS_NM,
                                                  MOTHERS_NAME = m.MOTHERS_NM,
                                                  DOB = m.DOB,
                                                  MAIL_ADDRESS = m.MAIL_ADDRESS,
                                                  MOB_NO = m.MOB_NO,
                                                  AUTHENTICATION_ID = m.AUTHENTICATION_ID,                                                  
                                                  TERMINAL_IP = m.TERMINAL_IP,
                                                  WORKING_HOUR = m.WORKING_HOUR,
                                                  START_TIME = m.START_TIME,
                                                  END_TIME = m.END_TIME,                                                  
                                              }).ToList();
                return LIST_LG_USER_SETUP_PROFILE;
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
                        LIST_LG_USER_SETUP_PROFILE.Add(OBJ_LG_USER_SETUP_PROFILE);
                    }
                }

                LG_ERROR_LOG_MAP.Add_Error_Log(null, dbEx.Source, "ERR_APP_TYPE", "GetAllUserSetupInfo",
                                       "0000000000", dbEx.Message, inner, dbEx.StackTrace, null, dateTime);

                return LIST_LG_USER_SETUP_PROFILE;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                LG_ERROR_LOG_MAP.Add_Error_Log(null, ex.Source, "ERR_APP_TYPE", "GetAllUserSetupInfo",
                                       "0000000000", ex.Message, inner4, ex.StackTrace, null, dateTime);

                //OBJ_LG_USER_SETUP_PROFILE.ERROR = ex.Message;
                LIST_LG_USER_SETUP_PROFILE.Add(OBJ_LG_USER_SETUP_PROFILE);
                return LIST_LG_USER_SETUP_PROFILE;
            }
        }
        #endregion fetch all

        #region dropdown

        public static IEnumerable<SelectListItem> GetUserClassificationForDD()
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);

            string format = OutgoingResponseFormat.GetFormat();
            OutgoingResponseFormat.SetResponseFormat(format);
            var List_Sys_User_Classification = (from m in Obj_DBModelEntities.LG_USER_CLASSIFACTION
                                                select m);
            var selectList = new List<SelectListItem>();
            foreach (var element in List_Sys_User_Classification)
            {
                selectList.Add(new SelectListItem
                {
                    Value = element.CLASSIFICATION_ID.ToString(),
                    Text = element.CLASSIFICATION_NAME
                });
            }
            if (selectList != null)
                return selectList;
            else
                throw new Exception("Invalid");
        }

        public static IEnumerable<SelectListItem> GetUserAreaIdForDD(string id)
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);

            string format = OutgoingResponseFormat.GetFormat();
            OutgoingResponseFormat.SetResponseFormat(format);
            string pid = id;
            var List_Sys_User_Area = (from s in Obj_DBModelEntities.LG_USER_AREA
                                      where s.CLASSIFICATION_ID == pid
                                      select s);
            var selectList = new List<SelectListItem>();
            foreach (var element in List_Sys_User_Area)
            {
                selectList.Add(new SelectListItem
                {
                    Value = element.AREA_ID.ToString(),
                    Text = element.AREA_NAME
                });
            }
            if (selectList != null)
                return selectList;
            else
                throw new Exception("Invalid");
        }

        public static IEnumerable<SelectListItem> GetAllUserAreaForDD()
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);

            string format = OutgoingResponseFormat.GetFormat();
            OutgoingResponseFormat.SetResponseFormat(format);
            var List_Sys_All_User_Area = (from m in Obj_DBModelEntities.LG_USER_AREA
                                          select m);
            var selectList = new List<SelectListItem>();
            foreach (var element in List_Sys_All_User_Area)
            {
                selectList.Add(new SelectListItem
                {
                    Value = element.AREA_ID.ToString(),
                    Text = element.AREA_NAME
                });
            }
            if (selectList != null)
                return selectList;
            else
                throw new Exception("Invalid");
        }

        public static IEnumerable<SelectListItem> GetBranchForDD()
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);

            string format = OutgoingResponseFormat.GetFormat();
            OutgoingResponseFormat.SetResponseFormat(format);
            var List_Sys_Branch = (from m in Obj_DBModelEntities.LG_SYS_BRANCH_HOME_BANK
                                   select m);
            var selectList = new List<SelectListItem>();
            foreach (var element in List_Sys_Branch)
            {
                selectList.Add(new SelectListItem
                {
                    Value = element.BRANCH_ID.ToString(),
                    Text = element.BRANCH_NM
                });
            }
            if (selectList != null)
                return selectList;
            else
                throw new Exception("Invalid");
        }

        public static IEnumerable<SelectListItem> GetAuthenticationTypeForDD()
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);

            string format = OutgoingResponseFormat.GetFormat();
            OutgoingResponseFormat.SetResponseFormat(format);
            var List_Sys_Authentication_Type = (from m in Obj_DBModelEntities.LG_AA_AUTHENTICATION_TYPE
                                                select m);
            var selectList = new List<SelectListItem>();
            foreach (var element in List_Sys_Authentication_Type)
            {
                selectList.Add(new SelectListItem
                {
                    Value = element.AUTHENTICATION_ID,
                    Text = element.AUTHENTICATION_NAME
                });
            }
            if (selectList != null)
                return selectList;
            else
                throw new Exception("Invalid");
        }

        public static IEnumerable<SelectListItem> GetWorkHourTypeForDD()
        {
            var selectList = new List<SelectListItem>();
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);

            string format = OutgoingResponseFormat.GetFormat();
            OutgoingResponseFormat.SetResponseFormat(format);

            selectList.Add(new SelectListItem { Value = "1", Text = "Fixed" });
            selectList.Add(new SelectListItem { Value = "2", Text = "Day Long" });
            return selectList;
        }

        public static IEnumerable<SelectListItem> GetTwoFAtypeForDD()
        {
            var selectList = new List<SelectListItem>();

            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);

            string format = OutgoingResponseFormat.GetFormat();
            OutgoingResponseFormat.SetResponseFormat(format);

            selectList.Add(new SelectListItem { Value = "1", Text = "Software" });
            selectList.Add(new SelectListItem { Value = "2", Text = "Hardware" });
            return selectList;
        }

        public static IEnumerable<SelectListItem> GetAllUserId()
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);

            string format = OutgoingResponseFormat.GetFormat();
            OutgoingResponseFormat.SetResponseFormat(format);
            var List_User = (from u in Obj_DBModelEntities.LG_USER_SETUP_PROFILE
                             select u);
            var selectList = new List<SelectListItem>();
            foreach (var element in List_User)
            {
                selectList.Add(new SelectListItem
                {
                    Value = element.USER_ID,
                    Text = element.USER_ID
                });
            }
            if (selectList != null)
                return selectList;
            else
                throw new Exception("Invalid");
        }

        public static IEnumerable<string> GetAllActiveUser(string Users)
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);

            string format = OutgoingResponseFormat.GetFormat();
            OutgoingResponseFormat.SetResponseFormat(format);

            var req_users = Users.Split(',');


            List<string> ActiveUser = new List<string>();
            try
            {
                foreach (var user in req_users)
                {
                    var User_status = (from u in Obj_DBModelEntities.LG_USER_SETUP_PROFILE
                                       where u.USER_ID == user
                                       select u.ACTIVE_FLAG_MULTI_LOGIN).SingleOrDefault();
                    if (User_status == 1)
                    {
                        ActiveUser.Add(user);
                    }
                }
                return ActiveUser;
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

                    LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "UpdateUserProfile",
                                                  "0000000000", dbEx.Message, inner, dbEx.StackTrace, "null", dateTime);

                    return ActiveUser;
                }
                catch (Exception ex)
                {
                    string dateTime = Convert.ToString(System.DateTime.Now);
                    string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);
                    LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "UpdateUserProfile",
                                                  "0000000000", ex.Message, inner4, ex.StackTrace, "null", dateTime);
                    return ActiveUser;
                }
        }

        #endregion dropdown

        #region update user session

        public static string UpdateUserSession(string USER_ID, string pUSER_SESSION_ID)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                LG_USER_SETUP_PROFILE_MAP OBJ_LG_USER_SETUP_PROFILE_MAP_OLD = new LG_USER_SETUP_PROFILE_MAP();
                LG_USER_SETUP_PROFILE_MAP OBJ_LG_USER_SETUP_PROFILE_MAP_NEW = new LG_USER_SETUP_PROFILE_MAP();
                string result = string.Empty;
                try
                {
                    string format = OutgoingResponseFormat.GetFormat();
                    OutgoingResponseFormat.SetResponseFormat(format);
                    DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);

                    LG_USER_SETUP_PROFILE OBJ_LG_USER_SETUP_PROFILE_OLD = Obj_DBModelEntities.LG_USER_SETUP_PROFILE.Where(m => m.USER_ID == USER_ID).SingleOrDefault();
                    Class_Conversion.LG_USER_SETUP_PROFILE_REVERSE_CON(OBJ_LG_USER_SETUP_PROFILE_MAP_OLD, OBJ_LG_USER_SETUP_PROFILE_OLD);

                    OBJ_LG_USER_SETUP_PROFILE_OLD.AUTH_STATUS_ID = "U";
                    OBJ_LG_USER_SETUP_PROFILE_OLD.LAST_ACTION = "EDT";
                    OBJ_LG_USER_SETUP_PROFILE_OLD.LAST_UPDATE_DT = System.DateTime.Now;
                    Obj_DBModelEntities.SaveChanges();
                    Class_Conversion.LG_USER_SETUP_PROFILE_REVERSE_CON(OBJ_LG_USER_SETUP_PROFILE_MAP_NEW, OBJ_LG_USER_SETUP_PROFILE_OLD);
                    OBJ_LG_USER_SETUP_PROFILE_MAP_NEW.ACTIVE_FLAG_MULTI_LOGIN = 0;

                    #region Auth log

                    LG_AA_NFT_AUTH_LOG_MAP OBJ_LG_AA_NFT_AUTH_LOG_MAP = new LG_AA_NFT_AUTH_LOG_MAP();
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "SessionInitialize").Select(x => x.FUNCTION_ID).SingleOrDefault();
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
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.MAKE_BY = pUSER_SESSION_ID;
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.MAKE_DT = DateTime.Now;
                    LG_AA_NFT_AUTH_LOG_MAP.AddNftAuthLog(OBJ_LG_USER_SETUP_PROFILE_MAP_OLD, OBJ_LG_USER_SETUP_PROFILE_MAP_NEW, OBJ_LG_AA_NFT_AUTH_LOG_MAP); //updated

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
                            result = "Can't Add User Profile(Db).";
                        }
                    }

                    LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "UpdateUserProfile",
                                                  "0000000000", dbEx.Message, inner, dbEx.StackTrace, pUSER_SESSION_ID, dateTime);

                    return result;
                }
                catch (Exception ex)
                {
                    ts.Dispose();
                    string dateTime = Convert.ToString(System.DateTime.Now);
                    string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);
                    LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "UpdateUserProfile",
                                                  "0000000000", ex.Message, inner4, ex.StackTrace, pUSER_SESSION_ID, dateTime);
                    result = "Can't Add  User Profile ";
                    return result;
                }
            }
        }

        #endregion update user session

        #endregion event

        /*

        #region Properties

        [DataMember]
        [Required(ErrorMessage = "User ID is required")]
        public string USER_ID { get; set; }

        public string USER_ID_LOCK { get; set; }
        [DataMember]
        [Required(ErrorMessage = "Classification ID is required")]
        public string CLASSIFICATION_ID { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Area ID is required")]
        public string AREA_ID { get; set; }

        [DataMember]
        public string USER_NAME { get; set; }

        [DataMember]
        public string AUTH_STATUS_ID { get; set; }

        public DateTime MAKE_DT { get; set; }

        [DataMember]
        public string USER_DESCRIPTION { get; set; }

        [DataMember]
        //[RegularExpression(@"^\d{6}$", ErrorMessage = "Employee ID must be contain 6 digits.")]
        [MaxLength(6, ErrorMessage = "Employee ID length must be within 6 digits")]
        public string EMPLOYEE_ID { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Branch ID is required")]
        public string BRANCH_ID { get; set; }
        [DataMember]
        //[RegularExpression(@"^\d{10}$", ErrorMessage = "Customer ID must be contain 10 digits.")]
        [MaxLength(10, ErrorMessage = "Customer ID length must be within 10 digits")]
        public string CUSTOMER_ID { get; set; }
        [DataMember]
        //[RegularExpression(@"^\d{5}$", ErrorMessage = "Agent ID must be contain 5 digits.")]
        [MaxLength(5, ErrorMessage = "Agent ID length must be within 5 digits")]
        public string AGENT_ID { get; set; }
        [DataMember]
        [Required(ErrorMessage = "Acc No is required")]
        //[RegularExpression(@"^\d{11}$", ErrorMessage = "Acc No must be contain 11 digits.")]
        [MaxLength(15, ErrorMessage = "Acc. No must be within 15 digits")]
        public string ACC_NO { get; set; }

        [DataMember]
        public string FATHERS_NAME { get; set; }
        [DataMember]
        public string MOTHERS_NAME { get; set; }
        [DataMember]
        public DateTime? DOB { get; set; }

        [DataMember]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Email is not in a valid format.")]
        public string MAIL_ADDRESS { get; set; }
        [DataMember]
        [Required(ErrorMessage = "Mob No is required")]
        public string MOB_NO { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Authentication ID is required")]
        public string AUTHENTICATION_ID { get; set; }
        [DataMember]
        public string TERMINAL_IP { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Working Hour Type ID is required")]
        public short WORKING_HOUR { get; set; }
        [DataMember]
        public string START_TIME { get; set; }

        [DataMember]
        public string END_TIME { get; set; }
        [DataMember]
        public string MAKE_BY { get; set; }

        // for image
        [DataMember]
        public byte[] imageByte { get; set; }

        [DataMember]
        public HttpPostedFileBase File { get; set; }

        [DataMember]
        public string PASSWORD { get; set; }

        [DataMember]
        public string APPLICATION_ID { get; set; }

        [DataMember]
        public string APPLICATION_NAME { get; set; }

        [DataMember]
        public string CLASSIFICATION_NAME { get; set; }

        [DataMember]
        public string AREA_NAME { get; set; }

        [DataMember]
        public string BRANCH_NAME { get; set; }

        [DataMember]
        public string AUTHENTICATION_NAME { get; set; }
        [DataMember]
        public string ERROR { get; set; }
        [DataMember]
        public string TWO_FA_TYPE_ID { get; set; }
        [DataMember]
        public string TWO_FA_TYPE_NAME { get; set; }

        [DataMember]
        public string AUTH_STATU_ID { get; set; }

        [DataMember]
        public string LAST_ACTION { get; set; }

        // for login validation
        [DataMember]
        public short ACTIVE_FLAG_INACTV_USER { get; set; }

        [DataMember]
        public short USER_ID_LOCK_WRNG_ATM { get; set; }

        [DataMember]
        public short ACTIVE_FLAG_MULTI_LOGIN { get; set; }

        [DataMember]
        public string USER_SESSION_ID { get; set; }
        [DataMember]
        public DateTime? USER_START_TIME { get; set; }
        [DataMember]
        public DateTime? USER_LAST_TIME { get; set; }
        [DataMember]
        public short FIRST_LOGIN_FLAG { get; set; }
        [DataMember]
        public string IP_ADDRESS { get; set; }
        [DataMember]
        public int HR { get; set; }
        [DataMember]
        public int MIN { get; set; }

        public string FAILED_LOGIN_ATTEMPT { get; set; }
        // END for login validation

        [DataMember]
        public List<SelectListItem> LIST_USER_CLASSIFICATION_FOR_DD { get; set; }
        [DataMember]
        public List<SelectListItem> LIST_USER_AREA_FOR_DD { get; set; }
        [DataMember]
        public List<SelectListItem> LIST_BRANCH_FOR_DD { get; set; }
        [DataMember]
        public List<SelectListItem> LIST_AUTHENTICATION_TYPE_FOR_DD { get; set; }
        [DataMember]
        public List<SelectListItem> LIST_WORKING_HOUR_FOR_DD { get; set; }
        [DataMember]
        public List<SelectListItem> LIST_TWO_FA_TYPE_FOR_DD { get; set; }

        //salekin added bellow
        //public virtual ICollection<LG_FNR_ROLE_DEFINE_MAP> ROLES { get; set; }
        [DataMember]
        public ICollection<LG_USER_ROLE_ASSIGN_MAP> ROLES { get; set; }
        [DataMember]
        public List<LG_FNR_ROLE_PERMISSION_DETAILS_MAP> PERMISSIONS { get; set; }
        [DataMember]
        public List<LG_FNR_MODULE_MAP> LIST_MODULES_FOR_SELECTED_ROLE { get; set; }
        [DataMember]
        public List<LG_MENU_MAP> LIST_MENU_MAP { get; set; }

        public string POLICY_ID { get; set; }

        #endregion Properties

        */
    }
}