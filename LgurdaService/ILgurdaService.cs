using Model.EntityModel.LGModel;
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
    [ServiceContract]
    public interface ILgurdaService
    {
        #region Application Setup

        [OperationContract]
        [WebGet(UriTemplate = "/Get_Applications", BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<LG_FNR_APPLICATION_MAP> GetApplications();

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Add_Application/{pAPPLICATION_NAME}/{psession_user}/{APP_TYPE_ID}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        string AddApplication(string pAPPLICATION_NAME, string psession_user, string APP_TYPE_ID);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Update_Application/{pAPPLICATION_ID}/{pAPPLICATION_NAME}/{psession_user}/{APP_TYPE_ID}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        string UpdateApplication(string pAPPLICATION_ID, string pAPPLICATION_NAME, string psession_user, string APP_TYPE_ID);

        [OperationContract]
        [WebGet(UriTemplate = "/Get_Application_ByAppId/{pAPPLICATION_ID}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        LG_FNR_APPLICATION_MAP GetApplicationByAppId(string pAPPLICATION_ID);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Delete_Application/{pAPPLICATION_ID}/{psession_user}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        string DeleteApplication(string pAPPLICATION_ID, string psession_user);

        #endregion Application Setup

        #region Service Setup

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Add_Service/{pSERVICE_NM}/{pSERVICE_SH_NM}/{pAPPLICATION_ID}/{psession_user}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        string AddService(string pSERVICE_NM, string pSERVICE_SH_NM, string pAPPLICATION_ID, string psession_user);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Update_Service/{pSERVICE_ID}/{pSERVICE_NM}/{pSERVICE_SH_NM}/{pAPPLICATION_ID}/{psession_user}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        string UpdateService(string pSERVICE_ID, string pSERVICE_NM, string pSERVICE_SH_NM, string pAPPLICATION_ID, string psession_user);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Delete_Service/{pSERVICE_ID}/{psession_user}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        string DeleteService(string pSERVICE_ID, string psession_user);

        [OperationContract]
        [WebGet(UriTemplate = "/Get_Services", BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<LG_FNR_SERVICE_MAP> GetServices();

        [OperationContract]
        [WebGet(UriTemplate = "/Get_Service_ByServiceId/{pSERVICE_ID}/{pAPPLICATION_ID}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        LG_FNR_SERVICE_MAP GetServiceByServiceId(string pSERVICE_ID, string pAPPLICATION_ID);

        #endregion Service Setup

        #region Module Setup

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/AddModule/{pMODULE_NM}/{pMODULE_SH_NM}/{pAPPLICATION_ID}/{pSERVICE_ID}/{psession_user}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        string AddModule(string pMODULE_NM, string pMODULE_SH_NM, string pAPPLICATION_ID, string pSERVICE_ID, string psession_user);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Update_Module/{pMODULE_ID}/{pMODULE_NM}/{pMODULE_SH_NM}/{pAPPLICATION_ID}/{pSERVICE_ID}/{psession_user}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        string UpdateModule(string pMODULE_ID, string pMODULE_NM, string pMODULE_SH_NM, string pAPPLICATION_ID, string pSERVICE_ID, string psession_user);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Delete_Module/{pMODULE_ID}/{psession_user}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        string DeleteModule(string pMODULE_ID, string psession_user);

        [OperationContract]
        [WebGet(UriTemplate = "/Get_Modules", BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<LG_FNR_MODULE_MAP> GetModules();

        [OperationContract]
        [WebGet(UriTemplate = "/Get_Module_ByServiceId_AndModuleId/{pSERVICE_ID}/{pMODULE_ID}/{pAPPLICATION_ID}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        LG_FNR_MODULE_MAP GetModuleByServiceIdAndModuleId(string pSERVICE_ID, string pMODULE_ID, string pAPPLICATION_ID);

        #endregion Module Setup

        #region Function Setup

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Add_Function/{pFUNCTION_NM}/{pAPPLICATION_ID}/{pSERVICE_ID}/{pMODULE_ID}/{pITEM_TYPE}/{pMAINT_CRT_FLAG_B}/{pMAINT_EDT_FLAG_B}/{pMAINT_DEL_FLAG_B}/{pMAINT_DTL_FLAG_B}/{pMAINT_INDX_FLAG_B}/{pMAINT_OTP_FLAG_B}/{pMAINT_2FA_FLAG_B}/{pMAINT_2FA_HARD_FLAG_B}/{pMAINT_2FA_SOFT_FLAG_B}/{pREPORT_VIEW_FLAG_B}/{pREPORT_PRINT_FLAG_B}/{pREPORT_GEN_FLAG_B}/{pAUTH_LEVEL}/{pMAINT_AUTH_FLAG_B}/{psession_user}/{pMAINT_BIO_FLAG_B}/{pPROCESS_FLAG_B}/{pHO_FUNCTION_FLAG_B}/{pFAST_PATH_NO}/{pTARGET_PATH}/{pDB_ROLE_NAME}/{pENABLED_FLAG_B}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        string AddFunction(string pFUNCTION_NM, string pAPPLICATION_ID, string pSERVICE_ID, string pMODULE_ID, string pITEM_TYPE, string pMAINT_CRT_FLAG_B, string pMAINT_EDT_FLAG_B, string pMAINT_DEL_FLAG_B, string pMAINT_DTL_FLAG_B, string pMAINT_INDX_FLAG_B, string pMAINT_OTP_FLAG_B, string pMAINT_2FA_FLAG_B, string pMAINT_2FA_HARD_FLAG_B, string pMAINT_2FA_SOFT_FLAG_B, string pREPORT_VIEW_FLAG_B, string pREPORT_PRINT_FLAG_B, string pREPORT_GEN_FLAG_B, string pAUTH_LEVEL, string pMAINT_AUTH_FLAG_B, string psession_user, string pMAINT_BIO_FLAG_B, string pPROCESS_FLAG_B, string pHO_FUNCTION_FLAG_B, string pFAST_PATH_NO, string pTARGET_PATH, string pDB_ROLE_NAME,string pENABLED_FLAG_B);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Update_Function/{pFUNCTION_ID}/{pFUNCTION_NM}/{pAPPLICATION_ID}/{pSERVICE_ID}/{pMODULE_ID}/{pITEM_TYPE}/{pMAINT_CRT_FLAG_B}/{pMAINT_EDT_FLAG_B}/{pMAINT_DEL_FLAG_B}/{pMAINT_DTL_FLAG_B}/{pMAINT_INDX_FLAG_B}/{pMAINT_OTP_FLAG_B}/{pMAINT_2FA_FLAG_B}/{pMAINT_2FA_HARD_FLAG_B}/{pMAINT_2FA_SOFT_FLAG_B}/{pREPORT_VIEW_FLAG_B}/{pREPORT_PRINT_FLAG_B}/{pREPORT_GEN_FLAG_B}/{pAUTH_LEVEL}/{pMAINT_AUTH_FLAG_B}/{psession_user}/{pMAINT_BIO_FLAG_B}/{pPROCESS_FLAG_B}/{pHO_FUNCTION_FLAG_B}/{pFAST_PATH_NO}/{pTARGET_PATH}/{pDB_ROLE_NAME}/{pENABLED_FLAG_B}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        string UpdateFunction(string pFUNCTION_ID, string pFUNCTION_NM, string pAPPLICATION_ID, string pSERVICE_ID, string pMODULE_ID, string pITEM_TYPE, string pMAINT_CRT_FLAG_B, string pMAINT_EDT_FLAG_B, string pMAINT_DEL_FLAG_B, string pMAINT_DTL_FLAG_B, string pMAINT_INDX_FLAG_B, string pMAINT_OTP_FLAG_B, string pMAINT_2FA_FLAG_B, string pMAINT_2FA_HARD_FLAG_B, string pMAINT_2FA_SOFT_FLAG_B, string pREPORT_VIEW_FLAG_B, string pREPORT_PRINT_FLAG_B, string pREPORT_GEN_FLAG_B, string pAUTH_LEVEL, string pMAINT_AUTH_FLAG_B, string psession_user, string pMAINT_BIO_FLAG_B, string pPROCESS_FLAG_B, string pHO_FUNCTION_FLAG_B, string pFAST_PATH_NO, string pTARGET_PATH, string pDB_ROLE_NAME, string pENABLED_FLAG_B);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Delete_Function/{pFUNCTION_ID}/{psession_user}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        string DeleteFunction(string pFUNCTION_ID, string psession_user);

        [OperationContract]
        [WebGet(UriTemplate = "/Get_Functions", BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<LG_FNR_FUNCTION_MAP> GetFunctions();

        [OperationContract]
        [WebGet(UriTemplate = "/Get_Function_ByFunctionId/{pFUNCTION_ID}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        LG_FNR_FUNCTION_MAP GetFunctionByFunctionId(string pFUNCTION_ID);

        [OperationContract]
        [WebGet(UriTemplate = "/GetFunctionDetailsByFunctionId/{pFUNCTION_ID}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        LG_FNR_FUNCTION_MAP GetFunctionDetailsByFunctionId(string pFUNCTION_ID);

        [OperationContract]
        [WebGet(UriTemplate = "/Get_AllFunctions_ByAppId/{pAPPLICATION_ID}/{pUSER_ID}/{pPROCESS_FLAG}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<LG_FNR_FUNCTION_MAP> GetAllFunctionsByAppId(string pAPPLICATION_ID, string pUSER_ID, string pPROCESS_FLAG);

        [OperationContract]
        [WebGet(UriTemplate = "/GetAllFunctions_ByFunctionGroupId/{pAPPLICATION_ID}/{pGROUP_ID}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<LG_FNR_FUNCTION_MAP> GetAllFunctions_ByFunctionGroupId(string pAPPLICATION_ID, string pGROUP_ID);

        [OperationContract]
        [WebGet(UriTemplate = "/GetAppTypeByAppId/{pAPPLICATION_ID}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        int? GetAppTypeByAppId(string pAPPLICATION_ID);

        [OperationContract]
        [WebGet(UriTemplate = "/GetPermittedFunctionsListByUser/{pUSER_ID}/{pAPP_ID}/{pFUNCTION_GROUP_ID}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        LG_FNR_FUNCTION_MAP GetPermittedFunctionsListByUser(string pUSER_ID, string pAPP_ID, string pFUNCTION_GROUP_ID);

        [OperationContract]
        [WebGet(UriTemplate = "/GetAuthPermissionByFunctionId/{pFUNCTION_ID}/{Function_Name}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        string GetAuthPermissionByFunctionId(string pFUNCTION_ID, string Function_Name);

        [OperationContract]
        [WebGet(UriTemplate = "/GetReportFunctionIds/{pAPPLICATION_ID}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<LG_REPORT_MANU_MAP> GetReportFunctionIds(string pAPPLICATION_ID);

        #endregion Function Setup

        #region Role Setup

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Add_Role/{pROLE_NAME}/{pROLE_DESCRIP}/{psession_user}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        string AddRole(string pROLE_NAME, string pROLE_DESCRIP, string psession_user);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Update_Role/{pROLE_ID}/{pROLE_NAME}/{pROLE_DESCRIP}/{psession_user}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        string UpdateRole(string pROLE_ID, string pROLE_NAME, string pROLE_DESCRIP, string psession_user);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Delete_Role/{pROLE_ID}/{psession_user}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        string DeleteRole(string pROLE_ID, string psession_user);

        [OperationContract]
        [WebGet(UriTemplate = "/Get_Roles", BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<LG_FNR_ROLE_MAP> GetRoles();

        [OperationContract]
        [WebGet(UriTemplate = "/Get_Role_ByRoleId/{pROLE_ID}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        LG_FNR_ROLE_MAP GetRoleByRoleId(string pROLE_ID);

        #endregion Role Setup

        #region Role Define

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Add_RoleDefine", BodyStyle = WebMessageBodyStyle.Bare)]
        string AddRoleDefine(LG_FNR_ROLE_DEFINE_MAP pLG_FNR_ROLE_DEFINE_MAP);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Update_RoleDefine", BodyStyle = WebMessageBodyStyle.Bare)]
        string UpdateRoleDefine(LG_FNR_ROLE_DEFINE_MAP pLG_FNR_ROLE_DEFINE_MAP);

        [OperationContract]
        [WebGet(UriTemplate = "/Get_AllRoleDefinedInfo", BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<LG_FNR_ROLE_DEFINE_MAP> GetAllRoleDefinedInfo();

        [OperationContract]
        [WebGet(UriTemplate = "/Get_Role_ByRoleName/{pROLE_NAME}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        LG_FNR_ROLE_MAP GetRoleByRoleName(string pROLE_NAME);

        [OperationContract]
        [WebGet(UriTemplate = "/Get_Functions_ByModuleIdAndItemtype/{app_id}/{service_id}/{module_id}/{item_type}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<LG_FNR_ROLE_DEFINE_MAP> GetFunctionsByModuleIdAndItemtype(string app_id, string service_id, string module_id, string item_type);

        [OperationContract]
        [WebGet(UriTemplate = "/Get_RoleDefineInfo_ByRoleId/{pROLE_ID}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        LG_FNR_ROLE_DEFINE_MAP GetRoleDefineInfoByRoleId(string pROLE_ID);

        [OperationContract]
        [WebGet(UriTemplate = "/Get_SelectedFunctions_ByRoleId/{pROLE_ID}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<LG_FNR_ROLE_DEFINE_MAP> GetSelectedFunctionsByRoleId(string pROLE_ID);

        [OperationContract]
        [WebGet(UriTemplate = "/Get_AllAndSelectedFunctions_ByRoleId/{app_id}/{service_id}/{module_id}/{item_type}/{pROLE_ID}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<LG_FNR_ROLE_DEFINE_MAP> GetAllAndSelectedFunctionsByRoleId(string app_id, string service_id, string module_id, string item_type, string pROLE_ID);

        [OperationContract]
        [WebGet(UriTemplate = "/Get_FunctionIds_ByRoleID/{pROLE_ID}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<string> GetFunctionIdsByRoleID(string pROLE_ID);

        #endregion Role Define

        #region Role Assign

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Add_RoleAssign/{ROLE_ID_FOR_IND_USER}/{pROLE_ASSIGN_COMMAND}/{pUSER_ID}/{pMAKE_BY}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        string AddRoleAssign(string ROLE_ID_FOR_IND_USER, string pROLE_ASSIGN_COMMAND, string pUSER_ID, string pMAKE_BY);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Update_RoleAssign/{pROLE_ID_FOR_IND_USER}/{pROLE_ASSIGN_COMMAND}/{pUSER_ID}/{pMAKE_BY}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        string UpdateRoleAssign(string pROLE_ID_FOR_IND_USER, string pROLE_ASSIGN_COMMAND, string pUSER_ID, string pMAKE_BY);

        [OperationContract]
        [WebGet(UriTemplate = "/Get_Roles_ByApplicationID/{pAPPLICATION_ID}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<SelectListItem> GetRolesByApplicationID(string pAPPLICATION_ID);

        [OperationContract]
        [WebGet(UriTemplate = "/Get_AllRoles", BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<SelectListItem> GetAllRoles();

        [OperationContract]
        [WebGet(UriTemplate = "/Get_AllAuthorizedRoles_ByUserId/{pUSER_ID}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<SelectListItem> Get_AllAuthorizedRoles_ByUserId(string pUSER_ID);

        [OperationContract]
        [WebGet(UriTemplate = "/Get_UserInfo_ByUserId/{pUSER_ID}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        LG_USER_ROLE_ASSIGN_MAP GetUserInfoByUserId(string pUSER_ID);

        [OperationContract]
        [WebGet(UriTemplate = "/Get_AllRoleAssignedInfo", BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<LG_USER_ROLE_ASSIGN_MAP> GetAllRoleAssignedInfo();

        [OperationContract]
        [WebGet(UriTemplate = "/Get_RoleIds_ByUserID/{pUSER_ID}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<string> GetRoleIdsByUserID(string pUSER_ID);

        [OperationContract]
        [WebGet(UriTemplate = "/Get_RoleAssignInfo_ByUserId/{pUSER_ID}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        LG_USER_ROLE_ASSIGN_MAP GetRoleAssignInfoByUserId(string pUSER_ID);

        #endregion Role Assign

        #region OTP

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Add_OtpConfig/{pAPPLICATION_ID}/{pMAIL_FLAG}/{pSMS_FLAG}/{pVALIDITY_PERIOD}/{pNO_OF_OTP_DIGIT}/{pOTP_FORMAT_ID}/{pMAKE_BY}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        string AddOtpConfig(string pAPPLICATION_ID, string pMAIL_FLAG, string pSMS_FLAG, string pVALIDITY_PERIOD, string pNO_OF_OTP_DIGIT, string pOTP_FORMAT_ID, string pMAKE_BY);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Update_OtpConfig/{pAPPLICATION_ID}/{pMAIL_FLAG}/{pSMS_FLAG}/{pVALIDITY_PERIOD}/{pNO_OF_OTP_DIGIT}/{pOTP_FORMAT_ID}/{pMAKE_BY}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        string UpdateOtpConfig(string pAPPLICATION_ID, string pMAIL_FLAG, string pSMS_FLAG, string pVALIDITY_PERIOD, string pNO_OF_OTP_DIGIT, string pOTP_FORMAT_ID, string pMAKE_BY);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Delete_OtpConfig/{pAPPLICATION_ID}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        string DeleteOtpConfig(string pAPPLICATION_ID);

        [OperationContract]
        [WebGet(UriTemplate = "/Get_OtpConfig", BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<LG_2FA_OTP_CONFIG_MAP> GetOtpConfig();

        [OperationContract]
        [WebGet(UriTemplate = "/Get_OtpConfig_ByAppId/{pAPPLICATION_ID}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        LG_2FA_OTP_CONFIG_MAP GetOtpConfigByAppId(string pAPPLICATION_ID);

        [OperationContract]
        //[WebGet(UriTemplate = "/Generate_Token/{user_id}/{session_id}/{terminal_ip}/{function_type}/{from_branch}/{from_ac_no}/{to_branch}/{to_ac_no}/{bill_id_no}/{trans_amount}/{app_id}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        [WebInvoke(Method = "POST", UriTemplate = "/Generate_Token/{user_id}/{session_id}/{terminal_ip}/{function_type}/{from_branch}/{from_ac_no}/{to_branch}/{to_ac_no}/{bill_id_no}/{trans_amount}/{app_id}/{card_no}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        string GenerateToken(string user_id, string session_id, string terminal_ip, string function_type, string from_branch, string from_ac_no, string to_branch, string to_ac_no, string bill_id_no, string trans_amount, string app_id, string card_no);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Verify_Token/{user_id}/{session_id}/{terminal_ip}/{function_type}/{from_branch}/{from_ac_no}/{to_branch}/{to_ac_no}/{bill_id_no}/{trans_amount}/{token1}/{token2}/{app_id}/{card_no}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        string VerifyToken(string user_id, string session_id, string terminal_ip, string function_type, string from_branch, string from_ac_no, string to_branch, string to_ac_no, string bill_id_no, string trans_amount, string token1, string token2, string app_id, string card_no);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Register_User/{puser_id}/{pcust_id}/{pgroup_id}/{pcell_no}/{pemail_id}/{preg_dt}/{psec_cell_no}/{app_id}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        string RegisterUser(string puser_id, string pcust_id, string pgroup_id, string pcell_no, string pemail_id, string preg_dt, string psec_cell_no, string app_id);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Update_User/{puser_id}/{pcust_id}/{pgroup_id}/{pcell_no}/{pemail_id}/{preg_dt}/{psec_cell_no}/{app_id}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        string UpdateUser(string puser_id, string pcust_id, string pgroup_id, string pcell_no, string pemail_id, string preg_dt, string psec_cell_no, string app_id);

        #endregion OTP

        #region Mail Server Config

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/AddMailServerConfig/{pAPPLICATION_ID}/{pMAIL_SENDER_IP}/{pMAIL_SENDER_ADDRESS}/{pMAIL_SENDER_PASSWORD}/{pMAIL_SENDER_NAME}/{pSESSION_USER_ID}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        string AddMailServerConfig(string pAPPLICATION_ID, string pMAIL_SENDER_IP, string pMAIL_SENDER_ADDRESS, string pMAIL_SENDER_PASSWORD, string pMAIL_SENDER_NAME, string pSESSION_USER_ID);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/UpdateMailServerConfig/{pMAIL_ID}/{pAPPLICATION_ID}/{pMAIL_SENDER_IP}/{pMAIL_SENDER_ADDRESS}/{pMAIL_SENDER_PASSWORD}/{pMAIL_SENDER_NAME}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        string UpdateMailServerConfig(string pMAIL_ID, string pAPPLICATION_ID, string pMAIL_SENDER_IP, string pMAIL_SENDER_ADDRESS, string pMAIL_SENDER_PASSWORD, string pMAIL_SENDER_NAME);

        [OperationContract]
        [WebGet(UriTemplate = "/GetMailServerConfigById/{pMAIL_ID}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        LG_SYS_MAIL_SERVER_CONFIG_MAP GetMailServerConfigById(string pMAIL_ID);

        [OperationContract]
        [WebGet(UriTemplate = "/Get_MailServerConfigs", BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<LG_SYS_MAIL_SERVER_CONFIG_MAP> GetMailServerConfigs();

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/DeleteMailServer/{pMAIL_ID}/{pSESSION_USER_ID}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        string DeleteMailServer(string pMAIL_ID, string pSESSION_USER_ID);

        #endregion Mail Server Config

        #region Password Policy Setup

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Add_Password_Policy/{p_user_id}/{pPASS_MAX_LENGTH}/{pPASS_MIN_LENGTH}/{pNUMERIC_CHAR_MIN}/{pPASS_HIS_PERIOD}/{pMIN_CAPS_LETTER}/{pMIN_SMALL_LETTER}/{pMIN_NUMERIC_CHAR}/{pMIN_CONS_USE_PASS}/{pPASS_REPEAT}/{pPASS_CHANGED_EXPIRY}/{pPASS_AUTO_CREATION}/{pPASS_CHANGE_BY_ADMIN}/{pPASS_CHANGE_AT_FIRST_LOGIN}/{pAPPLICATION_ID}/{pPASS_REUSE_MAX}/{pFAILED_LOGIN_ATTEMT}/{pPASS_EXP_PERIOD}/{pPASS_EXP_ALERT}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        string AddPasswordPolicy(string p_user_id, string pPASS_MAX_LENGTH, string pPASS_MIN_LENGTH, string pNUMERIC_CHAR_MIN, string pPASS_HIS_PERIOD, string pMIN_CAPS_LETTER, string pMIN_SMALL_LETTER, string pMIN_NUMERIC_CHAR, string pMIN_CONS_USE_PASS, string pPASS_REPEAT, string pPASS_CHANGED_EXPIRY, string pPASS_AUTO_CREATION, string pPASS_CHANGE_BY_ADMIN, string pPASS_CHANGE_AT_FIRST_LOGIN, string pAPPLICATION_ID, string pPASS_REUSE_MAX, string pFAILED_LOGIN_ATTEMT, string pPASS_EXP_PERIOD, string pPASS_EXP_ALERT);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Update_Password_Policy/{p_user_id}/{pPASS_MAX_LENGTH}/{pPASS_MIN_LENGTH}/{pNUMERIC_CHAR_MIN}/{pPASS_HIS_PERIOD}/{pMIN_CAPS_LETTER}/{pMIN_SMALL_LETTER}/{pMIN_NUMERIC_CHAR}/{pMIN_CONS_USE_PASS}/{pPASS_REPEAT}/{pPASS_CHANGED_EXPIRY}/{pPASS_AUTO_CREATION}/{pPASS_CHANGE_BY_ADMIN}/{pPASS_CHANGE_AT_FIRST_LOGIN}/{pAPPLICATION_ID}/{pPASS_REUSE_MAX}/{pFAILED_LOGIN_ATTEMT}/{pPASS_EXP_PERIOD}/{pPASS_EXP_ALERT}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        string UpdatePasswordPolicy(string p_user_id, string pPASS_MAX_LENGTH, string pPASS_MIN_LENGTH, string pNUMERIC_CHAR_MIN, string pPASS_HIS_PERIOD, string pMIN_CAPS_LETTER, string pMIN_SMALL_LETTER, string pMIN_NUMERIC_CHAR, string pMIN_CONS_USE_PASS, string pPASS_REPEAT, string pPASS_CHANGED_EXPIRY, string pPASS_AUTO_CREATION, string pPASS_CHANGE_BY_ADMIN, string pPASS_CHANGE_AT_FIRST_LOGIN, string pAPPLICATION_ID, string pPASS_REUSE_MAX, string pFAILED_LOGIN_ATTEMT, string pPASS_EXP_PERIOD, string pPASS_EXP_ALERT);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Delete_PasswordPolicy/{p_user_id}/{pAPPLICATION_ID}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        string DeletePasswordPolicy(string p_user_id, string pAPPLICATION_ID);

        [OperationContract]
        [WebGet(UriTemplate = "/Get_Password_Policy", BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<LG_CRD_PASSWORD_POLICY_MAP> GetPasswordPolicy();

        [OperationContract]
        [WebGet(UriTemplate = "/Get_Password_Policy_ByAppId/{pAPPLICATION_ID}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        LG_CRD_PASSWORD_POLICY_MAP GetPasswordPolicyByAppId(string pAPPLICATION_ID);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Validate_PasswordPolicy_On_Creation/{pAPPLICATION_ID}/{pPASSWORD}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        string ValidatePasswordPolicyOnCreation(string pAPPLICATION_ID, string pPASSWORD);

        #endregion Password Policy Setup

        #region Modify Password

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Change_Password/{pAPPLICATION_ID}/{pUSER_ID}/{pNEW_PASSWORD}/{pCURRENT_PASSWORD}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        string ChangePassword(string pAPPLICATION_ID, string pUSER_ID, string pNEW_PASSWORD, string pCURRENT_PASSWORD);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Reset_Password/{p_user_id}/{pUSER_ID}/{pAPPLICATION_ID}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        string ResetPassword(string p_user_id, string pUSER_ID, string pAPPLICATION_ID);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Encrypt_Password", BodyStyle = WebMessageBodyStyle.Wrapped)]
        string EncryptPassword();

        #endregion Modify Password

        #region User Profile setup

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Add_UserProfile/{USER_ID}/{pUSER_SESSION_ID}/{PCLASSIFICATION_ID}/{pAREA_ID}/{pAREA_ID_VALUE}/{pUSER_NAME}/{pUSER_DESCRIPTION}/{pBRANCH_ID}/{pACC_NO}/{pFATHERS_NAME}/{pMOTHERS_NAME}/{pDOB}/{pMAIL_ADDRESS}/{pMOB_NO}/{pAUTHENTICATION_ID}/{pTERMINAL_IP}/{pSTART_TIME}/{pEND_TIME}/{pWORKING_HOUR}/{pAPPLICATION_ID}/{pAUTH_STATUS_ID}/{pROLE_ID}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        string AddUserProfile(string USER_ID, string pUSER_SESSION_ID, string PCLASSIFICATION_ID, string pAREA_ID, string pAREA_ID_VALUE, string pUSER_NAME, string pUSER_DESCRIPTION, string pBRANCH_ID, string pACC_NO, string pFATHERS_NAME, string pMOTHERS_NAME, string pDOB, string pMAIL_ADDRESS, string pMOB_NO, string pAUTHENTICATION_ID, string pTERMINAL_IP, string pSTART_TIME, string pEND_TIME, string pWORKING_HOUR, string pAPPLICATION_ID, string pAUTH_STATUS_ID, string pROLE_ID);

        //[OperationContract]
        //[WebInvoke(Method = "POST", UriTemplate = "/Add_UserProfile", BodyStyle = WebMessageBodyStyle.Bare)]
        //string AddUserProfile(LG_USER_SETUP_PROFILE_MAP pLG_USER_SETUP_PROFILE_MAP);

        [OperationContract]
        [WebGet(UriTemplate = "/GetAllNewlyCreatedUser", BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<LG_USER_SETUP_PROFILE_MAP> GetAllNewlyCreatedUser();

       

        [OperationContract]
        [WebGet(UriTemplate = "/Get_AllUserSetupInfo", BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<LG_USER_SETUP_PROFILE_MAP> GetAllUserSetupInfo();

        [OperationContract]
        [WebGet(UriTemplate = "/Get_UserSetupInfoByUserId/{puser_id}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        LG_USER_SETUP_PROFILE_MAP GetUserSetupInfoByUserId(string puser_id);

        [OperationContract]
        //[WebGet(UriTemplate = "/Get_UserSetupInfo_ByUserId_ForRBAC/{pUSER_ID}/{pAPP_ID}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        //LG_USER_SETUP_PROFILE_MAP GetUserSetupInfoByUserIdForRBAC(string pUSER_ID, string pAPP_ID);
        [WebGet(UriTemplate = "/GetPermittedFunctionsByUser/{pUSER_ID}/{pAPP_ID}/{pFUNCTION_GROUP_ID}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        LG_USER_SETUP_PROFILE_MAP GetPermittedFunctionsByUser(string pUSER_ID, string pAPP_ID, string pFUNCTION_GROUP_ID);

        [OperationContract]
        [WebGet(UriTemplate = "/GetAllUserId", BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<SelectListItem> GetAllUserId();

        [OperationContract]
        [WebGet(UriTemplate = "/GetAllActiveUser/{Users}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<string> GetAllActiveUser(string Users);

        #endregion User Profile setup

        #region User File Upload

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Add_File", BodyStyle = WebMessageBodyStyle.Bare)]
        string AddFile(LG_USER_FILE_UPLOAD_MAP pLG_USER_FILE_UPLOAD_MAP);

        [OperationContract]
        [WebGet(UriTemplate = "/Get_UserInfoByUserId/{pUSER_ID}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        LG_USER_FILE_UPLOAD_MAP Get_UserInfoByUserId(string pUSER_ID);

        [OperationContract]
        [WebGet(UriTemplate = "/Get_UserUploadFile_ByUserId/{pUSER_ID}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        LG_USER_FILE_UPLOAD_MAP Get_UserUploadFile_ByUserId(string pUSER_ID);

        #endregion User File Upload

        #region Login

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Verify_User_And_Password_For_login/{pUSER_ID}/{pPASSWORD}/{pUSER_SESSION_ID}/{pIPADDRESS}/{pAPPLICATION_ID}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        string VerifyUserAndPasswordForLogin(string pUSER_ID, string pPASSWORD, string pUSER_SESSION_ID, string pIPADDRESS, string pAPPLICATION_ID);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Lock_User_ID/{pUSER_ID}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        string LockUserID(string pUSER_ID);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Logout_user/{pUSER_ID}/{pUSER_SESSION_ID}/{pIPADDRESS}/{pAPPLICATION_ID}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        string Logoutuser(string pUSER_ID, string pUSER_SESSION_ID, string pIPADDRESS, string pAPPLICATION_ID);

        #endregion Login

        #region Authorization Log

        [OperationContract]
        [WebInvoke(Method = "POST",
            UriTemplate =
            "/Add_Nft_Auth_Log/{pFUNCTION_ID}/{pTABLE_NAME}/{pTABLE_PK_COL_NM}/{pTABLE_PK_COL_VAL}/{pOLD_VALUE}/{pNEW_VALUE}/{pACTION_STATUS}/{pREMARKS}/{pPRIMARY_TABLE_FLAG}/{pPARENT_TABLE_PK_VAL}/{pAUTH_STATUS_ID}/{pAUTH_LEVEL_MAX}/{pAUTH_LEVEL_PENDING}/{pREASON_DECLINE}/{pMAKE_BY}/{pMAKE_DT}",
            BodyStyle = WebMessageBodyStyle.Wrapped)]
        string AddNftAuthLog(string pFUNCTION_ID, string pTABLE_NAME, string pTABLE_PK_COL_NM,
            string pTABLE_PK_COL_VAL, string pOLD_VALUE, string pNEW_VALUE, string pACTION_STATUS,
            string pREMARKS, string pPRIMARY_TABLE_FLAG, string pPARENT_TABLE_PK_VAL, string pAUTH_STATUS_ID,
            string pAUTH_LEVEL_MAX, string pAUTH_LEVEL_PENDING, string pREASON_DECLINE, string pMAKE_BY, string pMAKE_DT);

        [OperationContract]
        [WebGet(UriTemplate = "/Get_Nft_Auth_Logs_By_FunctionID/{pFUNCTION_ID}/{pMakeBy}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<LG_AA_NFT_AUTH_LOG_MAP> GetNftAuthLogsByFunctionID(string pFUNCTION_ID, string pMakeBy);

        [OperationContract]
        [WebGet(UriTemplate = "/Get_Nft_Auth_Logs_By_LogID/{pLOG_ID}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<LG_AA_NFT_AUTH_LOG_MAP> GetNftAuthLogsByLogID(string pLOG_ID);

        [OperationContract]
        [WebInvoke(Method = "POST",
            UriTemplate =
                "/Add_Nft_Auth_Log_Dtls/{pLOG_ID}/{pAUTH_BY}/{pAUTH_DT}/{pAUTH_STATUS_ID}/{preasonDecline}",
            BodyStyle = WebMessageBodyStyle.Wrapped)]
        string AddNftAuthLogDtls(string pLOG_ID, string pAUTH_BY, string pAUTH_DT, string pAUTH_STATUS_ID,
            string preasonDecline);

        [OperationContract]
        [WebGet(UriTemplate = "/Get_Auth_History/{pLOG_ID}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<LG_AA_NFT_AUTH_LOG_DTLS_MAP> GetAuthHistory(string pLOG_ID);

        [OperationContract]
        [WebGet(UriTemplate = "/Get_Nft_Auth_Log_Val_By_LogID/{pLOG_ID}/{pUSER_ID}/{pFUNCTION_ID}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<LG_AA_NFT_AUTH_LOG_VAL_MAP> GetNftAuthLogValByLogID(string pLOG_ID, string pUSER_ID, string pFUNCTION_ID);

        [OperationContract]
        [WebGet(UriTemplate = "/GetNftAuthLevelMaxFromFunction/{functionId}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        string GetNftAuthLevelMaxFromFunction(string functionId);

        #endregion Authorization Log

        #region Dropdown

        [OperationContract]
        [WebGet(UriTemplate = "/Get_Application_For_DD", BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<SelectListItem> GetApplicationForDD();

        [OperationContract]
        [WebGet(UriTemplate = "/Get_Service_For_DD", BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<SelectListItem> GetServiceForDD();

        [OperationContract]
        [WebGet(UriTemplate = "/Get_Module_For_DD", BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<SelectListItem> GetModuleForDD();

        [OperationContract]
        [WebGet(UriTemplate = "/Get_Service_By_AppId_For_DD/{id}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<SelectListItem> GetServiceByAppIdForDD(string id);

        [OperationContract]
        [WebGet(UriTemplate = "/Get_Module_By_ServiceId_For_DD/{service_id}/{app_id}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<SelectListItem> GetModuleByServiceIdForDD(string service_id, string app_id);

        [OperationContract]
        [WebGet(UriTemplate = "/Get_Function_By_ModuleId_For_DD/{id}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<SelectListItem> GetFunctionByModuleIdForDD(string id);

        [OperationContract]
        [WebGet(UriTemplate = "/Get_Function_ByModuleIdAndItemtype_ForDD/{module_id}/{item_type}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<SelectListItem> GetFunctionByModuleIdAndItemtypeForDD(string module_id, string item_type);

        [OperationContract]
        [WebGet(UriTemplate = "/Get_Item_type_For_DD", BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<SelectListItem> GetItemtypeForDD();

        [OperationContract]
        [WebGet(UriTemplate = "/GetAppTypesForDD", BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<SelectListItem> GetAppTypesForDD();

        /* User Profile */

        [OperationContract]
        [WebGet(UriTemplate = "/Get_User_Classification_For_DD", BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<SelectListItem> GetUserClassificationForDD();

        [OperationContract]
        [WebGet(UriTemplate = "/Get_User_AreaId_For_DD/{id}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<SelectListItem> GetUserAreaIdForDD(string id);

        [OperationContract]
        [WebGet(UriTemplate = "/Get_All_User_AreaId_For_DD", BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<SelectListItem> GetAllUserAreaForDD();

        [OperationContract]
        [WebGet(UriTemplate = "/Get_Branch_For_DD", BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<SelectListItem> GetBranchForDD();

        [OperationContract]
        [WebGet(UriTemplate = "/Get_Authentication_Type_For_DD", BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<SelectListItem> GetAuthenticationTypeForDD();

        [OperationContract]
        [WebGet(UriTemplate = "/Get_Work_Hour_Type_For_DD", BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<SelectListItem> GetWorkHourTypeForDD();

        [OperationContract]
        [WebGet(UriTemplate = "/Get_Two_FA_type_For_DD", BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<SelectListItem> GetTwoFAtypeForDD();

        [OperationContract]
        [WebGet(UriTemplate = "/Get_OTP_Format_For_DD", BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<SelectListItem> GetOTPFormatForDD();

        [OperationContract]
        [WebGet(UriTemplate = "/Get_Functions_From_Auth_Log_For_DD", BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<SelectListItem> GetFunctionsFromAuthLogForDD();

        //file upload
        [OperationContract]
        [WebGet(UriTemplate = "/Get_All_User_Id_For_DD", BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<SelectListItem> GetAllUseIdForDD();

        [OperationContract]
        [WebGet(UriTemplate = "/Get_UserUploadFileType_ForDD", BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<SelectListItem> GetUserUploadFileTypeForDD();

        #endregion Dropdown

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Test", BodyStyle = WebMessageBodyStyle.Bare)]
        string Test(Test pTest);

        #region Mandate User

        [OperationContract]
        [WebGet(UriTemplate = "/Get_UserSetupInfoByUserName/{puser_name}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        LG_USER_SETUP_PROFILE_MAP GetUserSetupInfoByUserName(string puser_name);

        [OperationContract]
        [WebGet(UriTemplate = "/Get_UserSetupInfoByUserAccountNo/{puser_account_no}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        LG_USER_SETUP_PROFILE_MAP GetUserSetupInfoByUserAccountNo(string puser_account_no);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Activate_User/{pUSER_ID}/{pAPPLICATION_ID}/{pMAKE_BY}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        string ActivateUser(string pUSER_ID, string pAPPLICATION_ID, string pMAKE_BY);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/InActivate_User/{pUSER_ID}/{pAPPLICATION_ID}/{pMAKE_BY}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        string InActivateUser(string pUSER_ID, string pAPPLICATION_ID, string pMAKE_BY);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Lock_User/{pUSER_ID}/{pAPPLICATION_ID}/{pMAKE_BY}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        string LockUser(string pUSER_ID, string pAPPLICATION_ID, string pMAKE_BY);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Unlock_User/{pUSER_ID}/{pAPPLICATION_ID}/{pMAKE_BY}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        string UnLockUser(string pUSER_ID, string pAPPLICATION_ID, string pMAKE_BY);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Deactivate_User/{pUSER_ID}/{pAPPLICATION_ID}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        string DeactivateUser(string pUSER_ID, string pAPPLICATION_ID);

        #endregion Mandate User

        #region User Activity Log

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Insert_To_Log", BodyStyle = WebMessageBodyStyle.Bare)]
        string InsertToLog(LG_USER_ACTIVITY_LOG_MAP pLG_USER_ACTIVITY_LOG_MAP);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Insert_To_session_tracker/{session_user}/{Application_id}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        string Insert_To_session_tracker(string session_user, string Application_id);

        [OperationContract]
        [WebGet(UriTemplate = "/Get_User_Activity_Log/{pUSER_ID}/{pSTART_DATE}/{pEND_DATE}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<LG_USER_ACTIVITY_LOG_MAP> GetUserActivityLog(string pUSER_ID, string pSTART_DATE, string pEND_DATE);

        #endregion User Activity Log

        #region Error Log

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Add_Error_Log/{FUNCTION_ID}/{ERR_SOURCE}/{ERR_APP_TYPE}/{ERR_METHOD}/{ERR_CODE}/{MESSEGE}/{PREVIEW_MESSEGE}/{STACK_TRACE}/{MAKE_BY}/{MAKE_DT}", BodyStyle = WebMessageBodyStyle.Bare)]
        string Add_Error_Log(string FUNCTION_ID, string ERR_SOURCE, string ERR_APP_TYPE, string ERR_METHOD, string ERR_CODE, string MESSEGE, string PREVIEW_MESSEGE, string STACK_TRACE, string MAKE_BY, string MAKE_DT);

        #endregion Error Log

        #region Calendar Type Setup

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/AddCalendarType/{PCLD_TYPE_NM}/{PDEFAULT_CLD}/{PBASED_ON_CLD}/{PMAKE_BY}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        string AddCalendarType(string PCLD_TYPE_NM, string PDEFAULT_CLD, string PBASED_ON_CLD, string PMAKE_BY);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Update_Calendar/{pCALENDAR_ID}/{PCLD_TYPE_NM}/{PDEFAULT_CLD}/{PBASED_ON_CLD}/{psession_user}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        string UpdateCalendarType(string pCALENDAR_ID, string PCLD_TYPE_NM, string PDEFAULT_CLD, string PBASED_ON_CLD, string psession_user);

        [OperationContract]
        [WebGet(UriTemplate = "/Get_AllCalendarType", BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<LG_SYS_CLD_TYPE_MAP> GetAllCalendarType();

        [OperationContract]
        [WebGet(UriTemplate = "/Get_CalendarByCalendarlId/{pCALENDAR_ID}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        LG_SYS_CLD_TYPE_MAP GetCalendarByCalendarlId(string pCALENDAR_ID);

        [OperationContract]
        [WebGet(UriTemplate = "/GetCalendarNameForDD", BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<SelectListItem> GetCalendarNameForDD();

        #endregion Calendar Type Setup

        #region Holiday Type Setup

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/AddHolidayType/{PHOLIDAY_TYPE_NM}/{WEEKEND_B}/{PMAKE_BY}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        string AddHoliday(string PHOLIDAY_TYPE_NM, string WEEKEND_B, string PMAKE_BY);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/UpdateHolidayType/{pHOLIDAY_TYPE_ID}/{pHOLIDAY_TYPE_NM}/{pWEEKEND_B}/{psession_user}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        string UpdateHolidayType(string pHOLIDAY_TYPE_ID, string pHOLIDAY_TYPE_NM, string pWEEKEND_B, string psession_user);

        [OperationContract]
        [WebGet(UriTemplate = "/Get_AllHoliday", BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<LG_SYS_HOLIDAY_TYPE_MAP> GetAllHoliday();

        [OperationContract]
        [WebGet(UriTemplate = "/Get_HolidayById/{pHOLIDAY_TYPE_ID}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        LG_SYS_HOLIDAY_TYPE_MAP GetHolidayById(string pHOLIDAY_TYPE_ID);

        #endregion Holiday Type Setup

        #region session initialize

        [OperationContract]
        [WebGet(UriTemplate = "/Get_user_session/{pSession_User}/{pAPPLICATION_ID}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<LG_USER_SETUP_PROFILE_MAP> GetUserSession(string pSession_User, string pAPPLICATION_ID);

        [OperationContract]
        [WebGet(UriTemplate = "/Get_SpecificUserSession/{puser_id}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        LG_USER_SETUP_PROFILE_MAP GetSpecificUserSession(string puser_id);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/UpdateUserSession/{puser_id}/{psession_user}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        string UpdateUserSession(string puser_id, string psession_user);

        #endregion session initialize

        #region BIND AD USER

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/Bind_AD_User/{pUSER_ID}/{pDOMAIN_ID}/{pDOMAIN}/{pAD_ACTIVE_FLAG}/{Psession_user}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        string BindADUser(string pUSER_ID, string pDOMAIN_ID, string pDOMAIN, string pAD_ACTIVE_FLAG, string Psession_user);

        [OperationContract]
        [WebGet(UriTemplate = "/Get_allAd_User", BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<LG_USER_AD_BINDING_MAP> GetallAdUser();

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/UpdateADUser/{SL}/{pDOMAIN_ID}/{pUSER_ID}/{pDOMAIN}/{pAD_ACTIVE_FLAG}/{psession_user}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        string UpdateADUser(string SL, string pDOMAIN_ID, string pUSER_ID, string pDOMAIN, string pAD_ACTIVE_FLAG, string psession_user);

        [OperationContract]
        [WebGet(UriTemplate = "/GetBindUser/{SL}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        LG_USER_AD_BINDING_MAP GetBindUser(string SL);

        [OperationContract]
        [WebGet(UriTemplate = "/GetBindUserByDomainId/{pDOMAIN_ID}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        string GetBindUserByDomainId(string pDOMAIN_ID);

        #endregion BIND AD USER

        #region sms
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/SendSMS/{RecieverCellno}/{SMS_TEXT}/", BodyStyle = WebMessageBodyStyle.Wrapped)]
        bool SendSMS(string RecieverCellno, string SMS_TEXT);
        #endregion
        
        #region mtaka Menu
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetMFSMenu/{APP_ID}", BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<LG_MENU_MAP> GetMFSMenu(string APP_ID);
        #endregion





    }
}