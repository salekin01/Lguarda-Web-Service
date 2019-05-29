using Model.EntityModel.Common;
using Model.EntityModel.LGModel;
using Model.EntityModel.TwoFactorAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Mvc;

namespace LgurdaService
{
    public class LgurdaService : ILgurdaService
    {
        #region Application Setup

        public string AddApplication(string pAPPLICATION_NAME, string psession_user, string APP_TYPE_ID)
        {
            return LG_FNR_APPLICATION_MAP.AddApplication(pAPPLICATION_NAME, psession_user, APP_TYPE_ID);
        }

        public string UpdateApplication(string pAPPLICATION_ID, string pAPPLICATION_NAME, string psession_user, string APP_TYPE_ID)
        {
            return LG_FNR_APPLICATION_MAP.UpdateApplication(pAPPLICATION_ID, pAPPLICATION_NAME, psession_user, APP_TYPE_ID);
        }

        public string DeleteApplication(string pAPPLICATION_ID, string psession_user)
        {
            return LG_FNR_APPLICATION_MAP.DeleteApplication(pAPPLICATION_ID, psession_user);
        }

        public IEnumerable<LG_FNR_APPLICATION_MAP> GetApplications()
        {
            return LG_FNR_APPLICATION_MAP.GetApplications();
        }

        public LG_FNR_APPLICATION_MAP GetApplicationByAppId(string pAPPLICATION_ID)
        {
            return LG_FNR_APPLICATION_MAP.GetApplicationByAppId(pAPPLICATION_ID);
        }

        #endregion Application Setup

        #region Service Setup

        public string AddService(string pSERVICE_NM, string pSERVICE_SH_NM, string pAPPLICATION_ID, string psession_user)
        {
            return LG_FNR_SERVICE_MAP.AddService(pSERVICE_NM, pSERVICE_SH_NM, pAPPLICATION_ID, psession_user);
        }

        public string UpdateService(string pSERVICE_ID, string pSERVICE_NM, string pSERVICE_SH_NM, string pAPPLICATION_ID, string psession_user)
        {
            return LG_FNR_SERVICE_MAP.UpdateService(pSERVICE_ID, pSERVICE_NM, pSERVICE_SH_NM, pAPPLICATION_ID, psession_user);
        }

        public string DeleteService(string pSERVICE_ID, string psession_user)
        {
            return LG_FNR_SERVICE_MAP.DeleteService(pSERVICE_ID, psession_user);
        }

        public LG_FNR_SERVICE_MAP GetServiceByServiceId(string pSERVICE_ID, string pAPPLICATION_ID)
        {
            return LG_FNR_SERVICE_MAP.GetServiceByServiceId(pSERVICE_ID, pAPPLICATION_ID);
        }

        public IEnumerable<LG_FNR_SERVICE_MAP> GetServices()
        {
            return LG_FNR_SERVICE_MAP.GetServices();
        }

        #endregion Service Setup

        #region Module Setup

        public string AddModule(string pMODULE_NM, string pMODULE_SH_NM, string pAPPLICATION_ID, string pSERVICE_ID, string psession_user)
        {
            return LG_FNR_MODULE_MAP.AddModule(pMODULE_NM, pMODULE_SH_NM, pAPPLICATION_ID, pSERVICE_ID, psession_user);
        }

        public string UpdateModule(string pMODULE_ID, string pMODULE_NM, string pMODULE_SH_NM, string pAPPLICATION_ID, string pSERVICE_ID, string psession_user)
        {
            return LG_FNR_MODULE_MAP.UpdateModule(pMODULE_ID, pMODULE_NM, pMODULE_SH_NM, pAPPLICATION_ID, pSERVICE_ID, psession_user);
        }

        public string DeleteModule(string pMODULE_ID, string psession_user)
        {
            return LG_FNR_MODULE_MAP.DeleteModule(pMODULE_ID, psession_user);
        }

        public LG_FNR_MODULE_MAP GetModuleByServiceIdAndModuleId(string pSERVICE_ID, string pMODULE_ID, string pAPPLICATION_ID)
        {
            return LG_FNR_MODULE_MAP.GetModuleByServiceIdAndModuleId(pSERVICE_ID, pMODULE_ID, pAPPLICATION_ID);
        }

        public IEnumerable<LG_FNR_MODULE_MAP> GetModules()
        {
            return LG_FNR_MODULE_MAP.GetModules();
        }

        #endregion Module Setup

        #region Function Setup

        public string AddFunction(string pFUNCTION_NM, string pAPPLICATION_ID, string pSERVICE_ID, string pMODULE_ID, string pITEM_TYPE, string pMAINT_CRT_FLAG_B, string pMAINT_EDT_FLAG_B, string pMAINT_DEL_FLAG_B, string pMAINT_DTL_FLAG_B, string pMAINT_INDX_FLAG_B, string pMAINT_OTP_FLAG_B, string pMAINT_2FA_FLAG_B, string pMAINT_2FA_HARD_FLAG_B, string pMAINT_2FA_SOFT_FLAG_B, string pREPORT_VIEW_FLAG_B, string pREPORT_PRINT_FLAG_B, string pREPORT_GEN_FLAG_B, string pAUTH_LEVEL, string pMAINT_AUTH_FLAG_B, string psession_user, string pMAINT_BIO_FLAG_B, string pPROCESS_FLAG_B, string pHO_FUNCTION_FLAG_B, string pFAST_PATH_NO, string pTARGET_PATH, string pDB_ROLE_NAME, string pENABLED_FLAG_B)
        {
            return LG_FNR_FUNCTION_MAP.AddFunction(pFUNCTION_NM, pAPPLICATION_ID, pSERVICE_ID, pMODULE_ID, pITEM_TYPE, pMAINT_CRT_FLAG_B, pMAINT_EDT_FLAG_B, pMAINT_DEL_FLAG_B, pMAINT_DTL_FLAG_B, pMAINT_INDX_FLAG_B, pMAINT_OTP_FLAG_B, pMAINT_2FA_FLAG_B, pMAINT_2FA_HARD_FLAG_B, pMAINT_2FA_SOFT_FLAG_B, pREPORT_VIEW_FLAG_B, pREPORT_PRINT_FLAG_B, pREPORT_GEN_FLAG_B, pAUTH_LEVEL, pMAINT_AUTH_FLAG_B, psession_user, pMAINT_BIO_FLAG_B, pPROCESS_FLAG_B, pHO_FUNCTION_FLAG_B, pFAST_PATH_NO, pTARGET_PATH, pDB_ROLE_NAME, pENABLED_FLAG_B);
        }

        public string UpdateFunction(string pFUNCTION_ID, string pFUNCTION_NM, string pAPPLICATION_ID, string pSERVICE_ID, string pMODULE_ID, string pITEM_TYPE, string pMAINT_CRT_FLAG_B, string pMAINT_EDT_FLAG_B, string pMAINT_DEL_FLAG_B, string pMAINT_DTL_FLAG_B, string pMAINT_INDX_FLAG_B, string pMAINT_OTP_FLAG_B, string pMAINT_2FA_FLAG_B, string pMAINT_2FA_HARD_FLAG_B, string pMAINT_2FA_SOFT_FLAG_B, string pREPORT_VIEW_FLAG_B, string pREPORT_PRINT_FLAG_B, string pREPORT_GEN_FLAG_B, string pAUTH_LEVEL, string pMAINT_AUTH_FLAG_B, string psession_user, string pMAINT_BIO_FLAG_B, string pPROCESS_FLAG_B, string pHO_FUNCTION_FLAG_B, string pFAST_PATH_NO, string pTARGET_PATH, string pDB_ROLE_NAME, string pENABLED_FLAG_B)
        {
            return LG_FNR_FUNCTION_MAP.UpdateFunction(pFUNCTION_ID, pFUNCTION_NM, pAPPLICATION_ID, pSERVICE_ID, pMODULE_ID, pITEM_TYPE, pMAINT_CRT_FLAG_B, pMAINT_EDT_FLAG_B, pMAINT_DEL_FLAG_B, pMAINT_DTL_FLAG_B, pMAINT_INDX_FLAG_B, pMAINT_OTP_FLAG_B, pMAINT_2FA_FLAG_B, pMAINT_2FA_HARD_FLAG_B, pMAINT_2FA_SOFT_FLAG_B, pREPORT_VIEW_FLAG_B, pREPORT_PRINT_FLAG_B, pREPORT_GEN_FLAG_B, pAUTH_LEVEL, pMAINT_AUTH_FLAG_B, psession_user, pMAINT_BIO_FLAG_B, pPROCESS_FLAG_B, pHO_FUNCTION_FLAG_B, pFAST_PATH_NO, pTARGET_PATH, pDB_ROLE_NAME, pENABLED_FLAG_B);
        }

        public string DeleteFunction(string pFUNCTION_ID, string psession_user)
        {
            return LG_FNR_FUNCTION_MAP.DeleteFunction(pFUNCTION_ID, psession_user);
        }

        public List<LG_FNR_FUNCTION_MAP> GetFunctions()
        {
            return LG_FNR_FUNCTION_MAP.GetFunctions();
        }

        public LG_FNR_FUNCTION_MAP GetFunctionByFunctionId(string pFUNCTION_ID)
        {
            return LG_FNR_FUNCTION_MAP.GetFunctionByFunctionId(pFUNCTION_ID);
        }
        public LG_FNR_FUNCTION_MAP GetFunctionDetailsByFunctionId(string pFUNCTION_ID)
        {
            return LG_FNR_FUNCTION_MAP.GetFunctionDetailsByFunctionId(pFUNCTION_ID);
        }

        public IEnumerable<LG_FNR_FUNCTION_MAP> GetAllFunctionsByAppId(string pAPPLICATION_ID, string pUSER_ID, string pPROCESS_FLAG)
        {
            return LG_FNR_FUNCTION_MAP.GetAllFunctionsByAppId(pAPPLICATION_ID, pUSER_ID, pPROCESS_FLAG);
        }

        public IEnumerable<LG_FNR_FUNCTION_MAP> GetAllFunctions_ByFunctionGroupId(string pAPPLICATION_ID, string pGROUP_ID)
        {
            return LG_FNR_FUNCTION_MAP.GetAllFunctions_ByFunctionGroupId(pAPPLICATION_ID, pGROUP_ID);
        }

        public int? GetAppTypeByAppId(string pAPPLICATION_ID)
        {
            return LG_FNR_FUNCTION_MAP.GetAppTypeByAppId(pAPPLICATION_ID);
        }

        public LG_FNR_FUNCTION_MAP GetPermittedFunctionsListByUser(string pUSER_ID, string pAPP_ID, string pFUNCTION_GROUP_ID)
        {
            return LG_FNR_FUNCTION_MAP.GetPermittedFunctionsListByUser(pUSER_ID, pAPP_ID, pFUNCTION_GROUP_ID);
        }
        public string GetAuthPermissionByFunctionId(string pFUNCTION_ID, string Function_Name)
        {
            return LG_FNR_FUNCTION_MAP.GetAuthPermissionByFunctionId(pFUNCTION_ID, Function_Name);
        }

        public IEnumerable<LG_REPORT_MANU_MAP> GetReportFunctionIds(string pAPPLICATION_ID)
        {
            return LG_REPORT_MANU_MAP.GetReportFunctionIds(pAPPLICATION_ID);
        }
        #endregion Function Setup

        #region Role Setup

        public string AddRole(string pROLE_NAME, string pROLE_DESCRIP, string psession_user)
        {
            return LG_FNR_ROLE_MAP.AddRole(pROLE_NAME, pROLE_DESCRIP, psession_user);
        }

        public string UpdateRole(string pROLE_ID, string pROLE_NAME, string pROLE_DESCRIP, string psession_user)
        {
            return LG_FNR_ROLE_MAP.UpdateRole(pROLE_ID, pROLE_NAME, pROLE_DESCRIP, psession_user);
        }

        public string DeleteRole(string pROLE_ID, string psession_user)
        {
            return LG_FNR_ROLE_MAP.DeleteRole(pROLE_ID, psession_user);
        }

        public IEnumerable<LG_FNR_ROLE_MAP> GetRoles()
        {
            return LG_FNR_ROLE_MAP.GetRoles();
        }

        public LG_FNR_ROLE_MAP GetRoleByRoleId(string pROLE_ID)
        {
            return LG_FNR_ROLE_MAP.GetRoleByRoleId(pROLE_ID);
        }

        #endregion Role Setup

        #region Role Define

        public LG_FNR_ROLE_MAP GetRoleByRoleName(string pROLE_NAME)
        {
            return LG_FNR_ROLE_DEFINE_MAP.GetRoleByRoleName(pROLE_NAME);
        }

        public IEnumerable<LG_FNR_ROLE_DEFINE_MAP> GetFunctionsByModuleIdAndItemtype(string app_id, string service_id, string module_id, string item_type)
        {
            return LG_FNR_ROLE_DEFINE_MAP.GetFunctionsByModuleIdAndItemtype(app_id, service_id, module_id, item_type);
        }

        public List<LG_FNR_ROLE_DEFINE_MAP> GetSelectedFunctionsByRoleId(string pROLE_ID) //method for gridEdit ajax call
        {
            return LG_FNR_ROLE_DEFINE_MAP.GetSelectedFunctionsByRoleId(pROLE_ID);
        }

        public string AddRoleDefine(LG_FNR_ROLE_DEFINE_MAP pLG_FNR_ROLE_DEFINE_MAP)
        {
            return LG_FNR_ROLE_DEFINE_MAP.AddRoleDefine(pLG_FNR_ROLE_DEFINE_MAP);
        }

        public IEnumerable<LG_FNR_ROLE_DEFINE_MAP> GetAllRoleDefinedInfo()
        {
            return LG_FNR_ROLE_DEFINE_MAP.GetAllRoleDefinedInfo();
        }

        public LG_FNR_ROLE_DEFINE_MAP GetRoleDefineInfoByRoleId(string pROLE_ID) //Edit Role
        {
            return LG_FNR_ROLE_DEFINE_MAP.GetRoleDefineInfoByRoleId(pROLE_ID);
        }

        public List<LG_FNR_ROLE_DEFINE_MAP> GetAllAndSelectedFunctionsByRoleId(string app_id, string service_id, string module_id, string item_type, string pROLE_ID)
        {
            return LG_FNR_ROLE_DEFINE_MAP.GetAllAndSelectedFunctionsByRoleId(app_id, service_id, module_id, item_type, pROLE_ID);
        }

        public List<string> GetFunctionIdsByRoleID(string pROLE_ID)
        {
            return LG_FNR_ROLE_DEFINE_MAP.GetFunctionIdsByRoleID(pROLE_ID);
        }

        public string UpdateRoleDefine(LG_FNR_ROLE_DEFINE_MAP pLG_FNR_ROLE_DEFINE_MAP)
        {
            return LG_FNR_ROLE_DEFINE_MAP.UpdateRoleDefine(pLG_FNR_ROLE_DEFINE_MAP);
        }

        #endregion Role Define

        #region Role Assign

        public string AddRoleAssign(string ROLE_ID_FOR_IND_USER, string pROLE_ASSIGN_COMMAND, string pUSER_ID, string pMAKE_BY)
        {
            return LG_USER_ROLE_ASSIGN_MAP.AddRoleAssign(ROLE_ID_FOR_IND_USER, pROLE_ASSIGN_COMMAND, pUSER_ID, pMAKE_BY);
        }

        public string UpdateRoleAssign(string pROLE_ID_FOR_IND_USER, string pROLE_ASSIGN_COMMAND, string pUSER_ID, string pMAKE_BY)
        {
            return LG_USER_ROLE_ASSIGN_MAP.UpdateRoleAssign(pROLE_ID_FOR_IND_USER, pROLE_ASSIGN_COMMAND, pUSER_ID, pMAKE_BY);
        }

        public IEnumerable<LG_USER_ROLE_ASSIGN_MAP> GetAllRoleAssignedInfo()
        {
            return LG_USER_ROLE_ASSIGN_MAP.GetAllRoleAssignedInfo();
        }

        public IEnumerable<SelectListItem> GetRolesByApplicationID(string pAPPLICATION_ID)
        {
            return LG_USER_ROLE_ASSIGN_MAP.GetRolesByApplicationID(pAPPLICATION_ID);
        }

        public IEnumerable<SelectListItem> GetAllRoles()
        {
            return LG_USER_ROLE_ASSIGN_MAP.GetAllRoles();
        }

        public IEnumerable<SelectListItem> Get_AllAuthorizedRoles_ByUserId(string pUSER_ID)
        {
            return LG_USER_ROLE_ASSIGN_MAP.Get_AllAuthorizedRoles_ByUserId(pUSER_ID);
        }

        public LG_USER_ROLE_ASSIGN_MAP GetUserInfoByUserId(string pUSER_ID)
        {
            return LG_USER_ROLE_ASSIGN_MAP.GetUserInfoByUserId(pUSER_ID);
        }

        public List<string> GetRoleIdsByUserID(string pUSER_ID)
        {
            return LG_USER_ROLE_ASSIGN_MAP.GetRoleIdsByUserID(pUSER_ID);
        }

        public LG_USER_ROLE_ASSIGN_MAP GetRoleAssignInfoByUserId(string pUSER_ID)
        {
            return LG_USER_ROLE_ASSIGN_MAP.GetRoleAssignInfoByUserId(pUSER_ID);
        }

        #endregion Role Assign

        #region OTP

        public string AddOtpConfig(string pAPPLICATION_ID, string pMAIL_FLAG, string pSMS_FLAG, string pVALIDITY_PERIOD, string pNO_OF_OTP_DIGIT, string pOTP_FORMAT_ID, string pMAKE_BY)
        {
            return LG_2FA_OTP_CONFIG_MAP.AddOtpConfig(pAPPLICATION_ID, pMAIL_FLAG, pSMS_FLAG, pVALIDITY_PERIOD, pNO_OF_OTP_DIGIT, pOTP_FORMAT_ID, pMAKE_BY);
        }

        public string UpdateOtpConfig(string pAPPLICATION_ID, string pMAIL_FLAG, string pSMS_FLAG, string pVALIDITY_PERIOD, string pNO_OF_OTP_DIGIT, string pOTP_FORMAT_ID, string pMAKE_BY)
        {
            return LG_2FA_OTP_CONFIG_MAP.UpdateOtpConfig(pAPPLICATION_ID, pMAIL_FLAG, pSMS_FLAG, pVALIDITY_PERIOD, pNO_OF_OTP_DIGIT, pOTP_FORMAT_ID, pMAKE_BY);
        }

        public string DeleteOtpConfig(string pAPPLICATION_ID)
        {
            return LG_2FA_OTP_CONFIG_MAP.DeleteOtpConfig(pAPPLICATION_ID);
        }

        public IEnumerable<LG_2FA_OTP_CONFIG_MAP> GetOtpConfig()
        {
            return LG_2FA_OTP_CONFIG_MAP.GetOtpConfig();
        }

        public LG_2FA_OTP_CONFIG_MAP GetOtpConfigByAppId(string pAPPLICATION_ID)
        {
            return LG_2FA_OTP_CONFIG_MAP.GetOtpConfigByAppId(pAPPLICATION_ID);
        }

        public string GenerateToken(string user_id, string session_id, string terminal_ip, string function_type, string from_branch, string from_ac_no, string to_branch, string to_ac_no, string bill_id_no, string trans_amount, string app_id, string card_no)
        {
            return OTP.GenerateToken(user_id, session_id, terminal_ip, function_type, from_branch, from_ac_no, to_branch, to_ac_no, bill_id_no, trans_amount, app_id, card_no);
        }

        public string VerifyToken(string puser_id, string psession_id, string pterminal_ip, string pfunction_type, string pfrom_branch, string pfrom_ac_no, string pto_branch, string pto_ac_no, string pbill_id_no, string ptrans_amount, string ptoken1, string ptoken2, string papp_id, string pcard_no)
        {
            return OTP.VerifyToken(puser_id, psession_id, pterminal_ip, pfunction_type, pfrom_branch, pfrom_ac_no, pto_branch, pto_ac_no, pbill_id_no, ptrans_amount, ptoken1, ptoken2, papp_id, pcard_no);
        }

        public string RegisterUser(string puser_id, string pcust_id, string pgroup_id, string pcell_no, string pemail_id, string preg_dt, string psec_cell_no, string papp_id)
        {
            return OTP.RegisterUser(puser_id, pcust_id, pgroup_id, pcell_no, pemail_id, preg_dt, psec_cell_no, papp_id);
        }

        public string UpdateUser(string puser_id, string pcust_id, string pgroup_id, string pcell_no, string pemail_id, string preg_dt, string psec_cell_no, string papp_id)
        {
            return OTP.UpdateUser(puser_id, pcust_id, pgroup_id, pcell_no, pemail_id, preg_dt, psec_cell_no, papp_id);
        }

        #endregion OTP

        #region Mail Server Config

        public string AddMailServerConfig(string pAPPLICATION_ID, string pMAIL_SENDER_IP, string pMAIL_SENDER_ADDRESS, string pMAIL_SENDER_PASSWORD, string pMAIL_SENDER_NAME, string pSESSION_USER_ID)
        {
            return LG_SYS_MAIL_SERVER_CONFIG_MAP.AddMailServerConfig(pAPPLICATION_ID, pMAIL_SENDER_IP, pMAIL_SENDER_ADDRESS, pMAIL_SENDER_PASSWORD, pMAIL_SENDER_NAME, pSESSION_USER_ID);
        }

        public string UpdateMailServerConfig(string pMAIL_ID, string pAPPLICATION_ID, string pMAIL_SENDER_IP, string pMAIL_SENDER_ADDRESS, string pMAIL_SENDER_PASSWORD, string pMAIL_SENDER_NAME)
        {
            return LG_SYS_MAIL_SERVER_CONFIG_MAP.UpdateMailServerConfig(pMAIL_ID, pAPPLICATION_ID, pMAIL_SENDER_IP, pMAIL_SENDER_ADDRESS, pMAIL_SENDER_PASSWORD, pMAIL_SENDER_NAME);
        }

        public LG_SYS_MAIL_SERVER_CONFIG_MAP GetMailServerConfigById(string pMAIL_ID)
        {
            return LG_SYS_MAIL_SERVER_CONFIG_MAP.GetMailServerConfigById(pMAIL_ID);
        }

        public IEnumerable<LG_SYS_MAIL_SERVER_CONFIG_MAP> GetMailServerConfigs()
        {
            return LG_SYS_MAIL_SERVER_CONFIG_MAP.GetMailServerConfigs();
        }

        public string DeleteMailServer(string pMAIL_ID, string pSESSION_USER_ID)
        {
            return LG_SYS_MAIL_SERVER_CONFIG_MAP.DeleteMailServer(pMAIL_ID, pSESSION_USER_ID);
        }

        #endregion Mail Server Config

        #region Password Policy Setup

        public IEnumerable<Model.EntityModel.LGModel.LG_CRD_PASSWORD_POLICY_MAP> GetPasswordPolicy()
        {
            return LG_CRD_PASSWORD_POLICY_MAP.GetPasswordPolicy();
        }

        public string AddPasswordPolicy(string p_user_id, string pPASS_MAX_LENGTH, string pPASS_MIN_LENGTH, string pNUMERIC_CHAR_MIN, string pPASS_HIS_PERIOD, string pMIN_CAPS_LETTER, string pMIN_SMALL_LETTER, string pMIN_NUMERIC_CHAR, string pMIN_CONS_USE_PASS, string pPASS_REPEAT, string pPASS_CHANGED_EXPIRY, string pPASS_AUTO_CREATION, string pPASS_CHANGE_BY_ADMIN, string pPASS_CHANGE_AT_FIRST_LOGIN, string pAPPLICATION_ID, string pPASS_REUSE_MAX, string pFAILED_LOGIN_ATTEMT, string pPASS_EXP_PERIOD, string pPASS_EXP_ALERT)
        {
            return LG_CRD_PASSWORD_POLICY_MAP.AddPasswordPolicy(p_user_id, pPASS_MAX_LENGTH, pPASS_MIN_LENGTH, pNUMERIC_CHAR_MIN, pPASS_HIS_PERIOD, pMIN_CAPS_LETTER, pMIN_SMALL_LETTER, pMIN_NUMERIC_CHAR, pMIN_CONS_USE_PASS, pPASS_REPEAT, pPASS_CHANGED_EXPIRY, pPASS_AUTO_CREATION, pPASS_CHANGE_BY_ADMIN, pPASS_CHANGE_AT_FIRST_LOGIN, pAPPLICATION_ID, pPASS_REUSE_MAX, pFAILED_LOGIN_ATTEMT, pPASS_EXP_PERIOD, pPASS_EXP_ALERT);
        }

        public string UpdatePasswordPolicy(string p_user_id, string pPASS_MAX_LENGTH, string pPASS_MIN_LENGTH, string pNUMERIC_CHAR_MIN, string pPASS_HIS_PERIOD, string pMIN_CAPS_LETTER, string pMIN_SMALL_LETTER, string pMIN_NUMERIC_CHAR, string pMIN_CONS_USE_PASS, string pPASS_REPEAT, string pPASS_CHANGED_EXPIRY, string pPASS_AUTO_CREATION, string pPASS_CHANGE_BY_ADMIN, string pPASS_CHANGE_AT_FIRST_LOGIN, string pAPPLICATION_ID, string pPASS_REUSE_MAX, string pFAILED_LOGIN_ATTEMT, string pPASS_EXP_PERIOD, string pPASS_EXP_ALERT)
        {
            return LG_CRD_PASSWORD_POLICY_MAP.UpdatePasswordPolicy(p_user_id, pPASS_MAX_LENGTH, pPASS_MIN_LENGTH, pNUMERIC_CHAR_MIN, pPASS_HIS_PERIOD, pMIN_CAPS_LETTER, pMIN_SMALL_LETTER, pMIN_NUMERIC_CHAR, pMIN_CONS_USE_PASS, pPASS_REPEAT, pPASS_CHANGED_EXPIRY, pPASS_AUTO_CREATION, pPASS_CHANGE_BY_ADMIN, pPASS_CHANGE_AT_FIRST_LOGIN, pAPPLICATION_ID, pPASS_REUSE_MAX, pFAILED_LOGIN_ATTEMT, pPASS_EXP_PERIOD, pPASS_EXP_ALERT);
        }

        public string DeletePasswordPolicy(string p_user_id, string pAPPLICATION_ID)
        {
            return LG_CRD_PASSWORD_POLICY_MAP.DeletePasswordPolicy(p_user_id, pAPPLICATION_ID);
        }

        public LG_CRD_PASSWORD_POLICY_MAP GetPasswordPolicyByAppId(string pAPPLICATION_ID)
        {
            return LG_CRD_PASSWORD_POLICY_MAP.GetPasswordPolicyByAppId(pAPPLICATION_ID);
        }

        public string ValidatePasswordPolicyOnCreation(string pAPPLICATION_ID, string pPASSWORD)
        {
            return LG_CRD_PASSWORD_POLICY_MAP.ValidatePasswordPolicyOnCreation(pAPPLICATION_ID, pPASSWORD);
        }

        #endregion Password Policy Setup

        #region Modify Password

        public string ChangePassword(string pAPPLICATION_ID, string pUSER_ID, string pNEW_PASSWORD, string pCURRENT_PASSWORD)
        {
            return LG_CRD_PASSWORD_MODIFY_MAP.ChangePassword(pAPPLICATION_ID, pUSER_ID, pNEW_PASSWORD, pCURRENT_PASSWORD);
        }

        public string ResetPassword(string p_user_id, string pUSER_ID, string pAPPLICATION_ID)
        {
            return LG_CRD_PASSWORD_MODIFY_MAP.ResetPassword(p_user_id, pUSER_ID, pAPPLICATION_ID);
        }

        public string EncryptPassword()
        {
            return LG_CRD_PASSWORD_MODIFY_MAP.EncryptPassword();
        }

        #endregion Modify Password

        #region user profile

        public string AddUserProfile(string USER_ID, string pUSER_SESSION_ID, string PCLASSIFICATION_ID, string pAREA_ID, string pAREA_ID_VALUE, string pUSER_NAME, string pUSER_DESCRIPTION, string pBRANCH_ID, string pACC_NO, string pFATHERS_NAME, string pMOTHERS_NAME, string pDOB, string pMAIL_ADDRESS, string pMOB_NO, string pAUTHENTICATION_ID, string pTERMINAL_IP, string pSTART_TIME, string pEND_TIME, string pWORKING_HOUR, string pAPPLICATION_ID, string pAUTH_STATUS_ID, string pROLE_ID)
        {
            return LG_USER_SETUP_PROFILE_MAP.AddUserProfile(USER_ID, pUSER_SESSION_ID, PCLASSIFICATION_ID, pAREA_ID, pAREA_ID_VALUE, pUSER_NAME, pUSER_DESCRIPTION, pBRANCH_ID, pACC_NO, pFATHERS_NAME, pMOTHERS_NAME, pDOB, pMAIL_ADDRESS, pMOB_NO, pAUTHENTICATION_ID, pTERMINAL_IP, pSTART_TIME, pEND_TIME, pWORKING_HOUR, pAPPLICATION_ID, pAUTH_STATUS_ID, pROLE_ID);
        }

        //public string AddUserProfile(LG_USER_SETUP_PROFILE_MAP pLG_USER_SETUP_PROFILE_MAP)
        //{
        //    return LG_USER_SETUP_PROFILE_MAP.AddUserProfile(pLG_USER_SETUP_PROFILE_MAP);
        //}

        public IEnumerable<LG_USER_SETUP_PROFILE_MAP> GetAllUserSetupInfo()
        {
            return LG_USER_SETUP_PROFILE_MAP.GetAllUserSetupInfo();        
        }

        public IEnumerable<LG_USER_SETUP_PROFILE_MAP> GetAllNewlyCreatedUser()
        {
            return LG_USER_SETUP_PROFILE_MAP.GetAllNewlyCreatedUser();        
        }
         

        public LG_USER_SETUP_PROFILE_MAP GetUserSetupInfoByUserId(string puser_id)
        {
            return LG_USER_SETUP_PROFILE_MAP.GetUserSetupInfoByUserId(puser_id);
        }

        public LG_USER_SETUP_PROFILE_MAP GetPermittedFunctionsByUser(string pUSER_ID, string pAPP_ID, string pFUNCTION_GROUP_ID)
        {
            return LG_USER_SETUP_PROFILE_MAP.GetUserSetupInfoByUserIdForRBAC(pUSER_ID, pAPP_ID, pFUNCTION_GROUP_ID);
        }

        public IEnumerable<LG_USER_SETUP_PROFILE_MAP> GetUserSession(string pSession_User, string pAPPLICATION_ID)
        {
            return LG_USER_SETUP_PROFILE_MAP.GetAllUserSession(pSession_User, pAPPLICATION_ID);
        }

        public LG_USER_SETUP_PROFILE_MAP GetSpecificUserSession(string puser_id)
        {
            return LG_USER_SETUP_PROFILE_MAP.GetSpecificUserSession(puser_id);
        }

        public string UpdateUserSession(string pUSER_ID, string pUSER_SESSION_ID)
        {
            return LG_USER_SETUP_PROFILE_MAP.UpdateUserSession(pUSER_ID, pUSER_SESSION_ID);
        }

        #endregion user profile

        #region Add File

        public string AddFile(LG_USER_FILE_UPLOAD_MAP pLG_USER_FILE_UPLOAD_MAP)
        {
            return LG_USER_FILE_UPLOAD_MAP.AddFile(pLG_USER_FILE_UPLOAD_MAP);
        }

        public LG_USER_FILE_UPLOAD_MAP Get_UserInfoByUserId(string pUSER_ID)
        {
            return LG_USER_FILE_UPLOAD_MAP.Get_UserInfoByUserId(pUSER_ID);
        }

        public LG_USER_FILE_UPLOAD_MAP Get_UserUploadFile_ByUserId(string pUSER_ID)
        {
            return LG_USER_FILE_UPLOAD_MAP.Get_UserUploadFile_ByUserId(pUSER_ID);
        }

        #endregion Add File

        #region login

        public string VerifyUserAndPasswordForLogin(string user_id, string pPassword, string SessionID, string pIPaddress, string ApplicationID)
        {
            return LG_SYS_LOGIN.VerifyUserAndPasswordForLogin(user_id, pPassword, SessionID, pIPaddress, ApplicationID);
        }

        public string LockUserID(string user_id)
        {
            return LG_SYS_LOGIN.LockUserID(user_id);
        }

        public string Logoutuser(string user_id, string pSessionID, string pIPaddress, string ApplicationID)
        {
            return LG_SYS_LOGIN.LogOutuser(user_id, pSessionID, pIPaddress, ApplicationID);
        }

        #endregion login

        #region Authorization Log

        public IEnumerable<LG_AA_NFT_AUTH_LOG_MAP> GetNftAuthLogsByFunctionID(string pFUNCTION_ID, string pMakeBy)
        {
            return LG_AA_NFT_AUTH_LOG_MAP.GetNftAuthLogsByFunctionID(pFUNCTION_ID, pMakeBy);
        }

        public IEnumerable<LG_AA_NFT_AUTH_LOG_MAP> GetNftAuthLogsByLogID(string pLOG_ID)
        {
            return LG_AA_NFT_AUTH_LOG_MAP.GetNftAuthLogsByLogID(pLOG_ID);
        }

        public string AddNftAuthLog(string pFUNCTION_ID, string pTABLE_NAME, string pTABLE_PK_COL_NM,
            string pTABLE_PK_COL_VAL, string pOLD_VALUE, string pNEW_VALUE, string pACTION_STATUS,
            string pREMARKS, string pPRIMARY_TABLE_FLAG, string pPARENT_TABLE_PK_VAL, string pAUTH_STATUS_ID,
            string pAUTH_LEVEL_MAX, string pAUTH_LEVEL_PENDING, string pREASON_DECLINE, string pMAKE_BY, string pMAKE_DT)
        {
            return LG_AA_NFT_AUTH_LOG_MAP.AddNftAuthLog(pFUNCTION_ID, pTABLE_NAME, pTABLE_PK_COL_NM,
            pTABLE_PK_COL_VAL, pOLD_VALUE, pNEW_VALUE, pACTION_STATUS,
            pREMARKS, pPRIMARY_TABLE_FLAG, pPARENT_TABLE_PK_VAL, pAUTH_STATUS_ID,
            pAUTH_LEVEL_MAX, pAUTH_LEVEL_PENDING, pREASON_DECLINE, pMAKE_BY, pMAKE_DT);
        }

        public string AddNftAuthLogDtls(string pLOG_ID, string pAUTH_BY, string pAUTH_DT, string pAUTH_STATUS_ID,
            string preasonDecline)
        {
            return LG_AA_NFT_AUTH_LOG_DTLS_MAP.AddNftAuthLogDtls(pLOG_ID, pAUTH_BY, pAUTH_DT, pAUTH_STATUS_ID,
                preasonDecline);
        }

        public IEnumerable<LG_AA_NFT_AUTH_LOG_DTLS_MAP> GetAuthHistory(string pLOG_ID)
        {
            return LG_AA_NFT_AUTH_LOG_DTLS_MAP.GetAuthHistory(pLOG_ID);
        }

        public IEnumerable<LG_AA_NFT_AUTH_LOG_VAL_MAP> GetNftAuthLogValByLogID(string pLOG_ID, string pUSER_ID, string pFUNCTION_ID)
        {
            return LG_AA_NFT_AUTH_LOG_VAL_MAP.GetNftAuthLogValByLogID(pLOG_ID, pUSER_ID, pFUNCTION_ID);
        }

        public string GetNftAuthLevelMaxFromFunction(string functionId)
        {
            return LG_AA_NFT_AUTH_LOG_MAP.GetNftAuthLevelMaxFromFunction(functionId).ToString();
        }
        #endregion Authorization Log

        #region DropDown

        public IEnumerable<SelectListItem> GetApplicationForDD()
        {
            return DropDown.GetApplicationForDD();
        }

        public IEnumerable<SelectListItem> GetServiceForDD()
        {
            return DropDown.GetServiceForDD();
        }

        public IEnumerable<SelectListItem> GetModuleForDD()
        {
            return DropDown.GetModuleForDD();
        }

        public IEnumerable<SelectListItem> GetServiceByAppIdForDD(string id)
        {
            return DropDown.GetServiceByAppIdForDD(id);
        }

        public IEnumerable<SelectListItem> GetModuleByServiceIdForDD(string service_id, string app_id)
        {
            return DropDown.GetModuleByServiceIdForDD(service_id, app_id);
        }

        public IEnumerable<SelectListItem> GetFunctionByModuleIdForDD(string id)
        {
            return DropDown.GetFunctionByModuleIdForDD(id);
        }

        public IEnumerable<SelectListItem> GetFunctionByModuleIdAndItemtypeForDD(string module_id, string item_type)
        {
            return DropDown.GetFunctionByModuleIdAndItemtypeForDD(module_id, item_type);
        }

        public IEnumerable<SelectListItem> GetItemtypeForDD()
        {
            return DropDown.GetItemtypeForDD();
        }

        public IEnumerable<SelectListItem> GetAppTypesForDD()
        {
            return DropDown.GetAppTypesForDD();
        }

        /* User Profile*/

        public IEnumerable<SelectListItem> GetUserClassificationForDD()
        {
            return DropDown.GetUserClassificationForDD();
        }

        public IEnumerable<SelectListItem> GetUserAreaIdForDD(string id)
        {
            return DropDown.GetUserAreaIdForDD(id);
        }

        public IEnumerable<SelectListItem> GetAllUserAreaForDD()
        {
            return DropDown.GetAllUserAreaForDD();
        }

        public IEnumerable<SelectListItem> GetBranchForDD()
        {
            return DropDown.GetBranchForDD();
        }

        public IEnumerable<SelectListItem> GetAuthenticationTypeForDD()
        {
            return DropDown.GetAuthenticationTypeForDD();
        }

        public IEnumerable<SelectListItem> GetWorkHourTypeForDD()
        {
            return DropDown.GetWorkHourTypeForDD();
        }

        public IEnumerable<SelectListItem> GetTwoFAtypeForDD()
        {
            return DropDown.GetTwoFAtypeForDD();
        }

        public IEnumerable<SelectListItem> GetAllUserId()
        {
            return LG_USER_SETUP_PROFILE_MAP.GetAllUserId();
        }

        public IEnumerable<string> GetAllActiveUser(string Users)
        {
            return LG_USER_SETUP_PROFILE_MAP.GetAllActiveUser(Users);
        }

        public IEnumerable<SelectListItem> GetOTPFormatForDD()
        {
            return DropDown.GetOTPFormatForDD();
        }

        public IEnumerable<SelectListItem> GetFunctionsFromAuthLogForDD()
        {
            return DropDown.GetFunctionsFromAuthLogForDD();
        }

        /* File upload */

        public IEnumerable<SelectListItem> GetAllUseIdForDD()
        {
            return LG_USER_FILE_UPLOAD_MAP.GetAllUseIdForDD();
        }

        public IEnumerable<SelectListItem> GetUserUploadFileTypeForDD()
        {
            return DropDown.GetUserUploadFileTypeForDD();
        }

        #endregion DropDown

        #region Mandate User

        public LG_USER_SETUP_PROFILE_MAP GetUserSetupInfoByUserName(string puser_name)
        {
            return LG_USER_MANDATE_MAP.GetUserSetupInfoByUserName(puser_name);
        }

        public LG_USER_SETUP_PROFILE_MAP GetUserSetupInfoByUserAccountNo(string puser_account_no)
        {
            return LG_USER_MANDATE_MAP.GetUserSetupInfoByUserAccountNo(puser_account_no);
        }

        public string ActivateUser(string pUSER_ID, string pAPPLICATION_ID, string pMAKE_BY)
        {
            return LG_USER_MANDATE_MAP.ActivateUser(pUSER_ID, pAPPLICATION_ID, pMAKE_BY);
        }

        public string InActivateUser(string pUSER_ID, string pAPPLICATION_ID, string pMAKE_BY)
        {
            return LG_USER_MANDATE_MAP.InActivateUser(pUSER_ID, pAPPLICATION_ID, pMAKE_BY);
        }

        public string LockUser(string pUSER_ID, string pAPPLICATION_ID, string pMAKE_BY)
        {
            return LG_USER_MANDATE_MAP.LockUser(pUSER_ID, pAPPLICATION_ID, pMAKE_BY);
        }

        public string UnLockUser(string pUSER_ID, string pAPPLICATION_ID, string pMAKE_BY)
        {
            return LG_USER_MANDATE_MAP.UnLockUser(pUSER_ID, pAPPLICATION_ID, pMAKE_BY);
        }

        public string DeactivateUser(string pUSER_ID, string pAPPLICATION_ID)
        {
            return LG_USER_MANDATE_MAP.DeactivateUser(pUSER_ID, pAPPLICATION_ID);
        }

        #endregion Mandate User

        #region User Activity Log

        public string InsertToLog(LG_USER_ACTIVITY_LOG_MAP pLG_USER_ACTIVITY_LOG_MAP)
        {
            return LG_USER_ACTIVITY_LOG_MAP.InsertToLog(pLG_USER_ACTIVITY_LOG_MAP);
        }

        public string Insert_To_session_tracker(string session_user, string Application_id)
        {
            return LG_USER_ACTIVITY_LOG_MAP.Insert_To_session_tracker(session_user, Application_id);
        }

        public IEnumerable<LG_USER_ACTIVITY_LOG_MAP> GetUserActivityLog(string pUSER_ID, string pSTART_DATE, string pEND_DATE)
        {
            return LG_USER_ACTIVITY_LOG_MAP.GetUserActivityLog(pUSER_ID, pSTART_DATE, pEND_DATE);
        }

        #endregion User Activity Log

        #region Error Log

        public string Add_Error_Log(string FUNCTION_ID, string ERR_SOURCE, string ERR_APP_TYPE, string ERR_METHOD, string ERR_CODE, string MESSEGE, string PREVIEW_MESSEGE, string STACK_TRACE, string MAKE_BY, string MAKE_DT)
        {
            return LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ERR_SOURCE, ERR_APP_TYPE, ERR_METHOD, ERR_CODE, MESSEGE, PREVIEW_MESSEGE, STACK_TRACE, MAKE_BY, MAKE_DT);
        }

        #endregion Error Log

        #region calendar type

        public string AddCalendarType(string pCALENDAR_NM, string PDEFAULT_CLD, string pBASED_ON_CALENDAR, string pMAKE_BY)
        {
            return LG_SYS_CLD_TYPE_MAP.AddCalendarType(pCALENDAR_NM, PDEFAULT_CLD, pBASED_ON_CALENDAR, pMAKE_BY);
        }

        public IEnumerable<LG_SYS_CLD_TYPE_MAP> GetAllCalendarType()
        {
            return LG_SYS_CLD_TYPE_MAP.GetAllCalendarType();
        }

        public string UpdateCalendarType(string pCALENDAR_ID, string PCLD_TYPE_NM, string PDEFAULT_CLD, string PBASED_ON_CLD, string psession_user)
        {
            return LG_SYS_CLD_TYPE_MAP.UpdateCalendarType(pCALENDAR_ID, PCLD_TYPE_NM, PDEFAULT_CLD, PBASED_ON_CLD, psession_user);
        }

        //public string DeleteModule(string pMODULE_ID, string psession_user)
        //{
        //    return LG_FNR_MODULE_MAP.DeleteModule(pMODULE_ID, psession_user);
        //}
        public LG_SYS_CLD_TYPE_MAP GetCalendarByCalendarlId(string pCALENDAR_ID)
        {
            return LG_SYS_CLD_TYPE_MAP.GetCalendarByCalendarlId(pCALENDAR_ID);
        }

        public IEnumerable<SelectListItem> GetCalendarNameForDD()
        {
            return LG_SYS_CLD_TYPE_MAP.GetCalendarNameForDD();
        }

        #endregion calendar type

        #region Holiday type

        public string AddHoliday(string PHOLIDAY_TYPE_NM, string WEEKEND_B, string PMAKE_BY)
        {
            return LG_SYS_HOLIDAY_TYPE_MAP.AddHoliday(PHOLIDAY_TYPE_NM, WEEKEND_B, PMAKE_BY);
        }

        public IEnumerable<LG_SYS_HOLIDAY_TYPE_MAP> GetAllHoliday()
        {
            return LG_SYS_HOLIDAY_TYPE_MAP.GetAllHoliday();
        }

        public string UpdateHolidayType(string HOLIDAY_TYPE_ID, string HOLIDAY_TYPE_NM, string WEEKEND_B, string psession_user)
        {
            return LG_SYS_HOLIDAY_TYPE_MAP.UpdateHolidayType(HOLIDAY_TYPE_ID, HOLIDAY_TYPE_NM, WEEKEND_B, psession_user);
        }

        public LG_SYS_HOLIDAY_TYPE_MAP GetHolidayById(string pHOLIDAY_TYPE_ID)
        {
            return LG_SYS_HOLIDAY_TYPE_MAP.GetHolidayById(pHOLIDAY_TYPE_ID);
        }

        #endregion Holiday type

        #region BIND AD USER

        public string BindADUser(string pUSER_ID, string pDOMAIN_ID, string pDOMAIN, string pAD_ACTIVE_FLAG, string Psession_user)
        {
            return LG_USER_AD_BINDING_MAP.BindADUser(pUSER_ID, pDOMAIN_ID, pDOMAIN, pAD_ACTIVE_FLAG, Psession_user);
        }

        public string UpdateADUser(string SL, string pDOMAIN_ID, string pUSER_ID, string pDOMAIN, string pAD_ACTIVE_FLAG, string psession_user)
        {
            return LG_USER_AD_BINDING_MAP.UpdateADUser(SL, pDOMAIN_ID, pUSER_ID, pDOMAIN, pAD_ACTIVE_FLAG, psession_user);
        }

        public IEnumerable<LG_USER_AD_BINDING_MAP> GetallAdUser()
        {
            return LG_USER_AD_BINDING_MAP.GetallAdUser();
        }

        public LG_USER_AD_BINDING_MAP GetBindUser(string SL)
        {
            return LG_USER_AD_BINDING_MAP.GetBindUser(SL);
        }

        public string GetBindUserByDomainId(string pDOMAIN_ID)
        {
            return LG_USER_AD_BINDING_MAP.GetBindUserByDomainId(pDOMAIN_ID);
        }

        #endregion BIND AD USER

        #region SMS
        public bool SendSMS(string RecieverCellno, string SMS_TEXT)
        {
            return SMS_API.PushSMS(RecieverCellno, SMS_TEXT);
        }
        #endregion

        #region Mtaka menu

        public IEnumerable<LG_MENU_MAP> GetMFSMenu(string APP_ID)
        {
            return LG_MENU_MAP.GetMFSMenu(APP_ID);
        }

        #endregion


        public string Test(Test pTest)
        {
            return "True";
        }



    }
}