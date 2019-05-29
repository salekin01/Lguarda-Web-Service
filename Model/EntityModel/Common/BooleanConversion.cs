using Model.EDMX;
using Model.EntityModel.LGModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.EntityModel.Common
{
    public class BooleanConversion
    {
        public static LG_FNR_FUNCTION_MAP LG_FNR_FUNCTION_MAP_BOOL_TO_INT(string pMAINT_CRT_FLAG_B, string pMAINT_EDT_FLAG_B, string pMAINT_DEL_FLAG_B, string pMAINT_DTL_FLAG_B, string pMAINT_INDX_FLAG_B, string pMAINT_OTP_FLAG_B, string pMAINT_2FA_FLAG_B, string pMAINT_2FA_HARD_FLAG_B, string pMAINT_2FA_SOFT_FLAG_B, string pREPORT_VIEW_FLAG_B, string pREPORT_PRINT_FLAG_B, string pREPORT_GEN_FLAG_B, string pMAINT_AUTH_FLAG_B, string pMAINT_BIO_FLAG_B, string pPROCESS_FLAG_B, string pHO_FUNCTION_FLAG_B, string pENABLED_FLAG_B)
        {
            LG_FNR_FUNCTION_MAP OBJ_LG_FNR_FUNCTION_MAP = new LG_FNR_FUNCTION_MAP();
            if (pMAINT_CRT_FLAG_B.ToLower() == "true")
            {
                OBJ_LG_FNR_FUNCTION_MAP.MAINT_CRT_FLAG = 1;
            }
            else
                OBJ_LG_FNR_FUNCTION_MAP.MAINT_CRT_FLAG = 0;
            if (pMAINT_EDT_FLAG_B.ToLower() == "true")
            {
                OBJ_LG_FNR_FUNCTION_MAP.MAINT_EDT_FLAG = 1;
            }
            else
                OBJ_LG_FNR_FUNCTION_MAP.MAINT_EDT_FLAG = 0;
            if (pMAINT_DEL_FLAG_B.ToLower() == "true")
            {
                OBJ_LG_FNR_FUNCTION_MAP.MAINT_DEL_FLAG = 1;
            }
            else
                OBJ_LG_FNR_FUNCTION_MAP.MAINT_DEL_FLAG = 0;
            if (pMAINT_DTL_FLAG_B.ToLower() == "true")
            {
                OBJ_LG_FNR_FUNCTION_MAP.MAINT_DTL_FLAG = 1;
            }
            else
                OBJ_LG_FNR_FUNCTION_MAP.MAINT_DTL_FLAG = 0;
            if (pMAINT_INDX_FLAG_B.ToLower() == "true")
            {
                OBJ_LG_FNR_FUNCTION_MAP.MAINT_INDX_FLAG = 1;
            }
            else
                OBJ_LG_FNR_FUNCTION_MAP.MAINT_INDX_FLAG = 0;
            if (pMAINT_OTP_FLAG_B.ToLower() == "true")
            {
                OBJ_LG_FNR_FUNCTION_MAP.MAINT_OTP_FLAG = 1;
            }
            else
                OBJ_LG_FNR_FUNCTION_MAP.MAINT_OTP_FLAG = 0;
            if (pMAINT_2FA_FLAG_B.ToLower() == "true")
            {
                OBJ_LG_FNR_FUNCTION_MAP.MAINT_2FA_FLAG = 1;
            }
            else
                OBJ_LG_FNR_FUNCTION_MAP.MAINT_2FA_FLAG = 0;
            if (pMAINT_2FA_HARD_FLAG_B.ToLower() == "true")
            {
                OBJ_LG_FNR_FUNCTION_MAP.MAINT_2FA_HARD_FLAG = 1;
            }
            else
                OBJ_LG_FNR_FUNCTION_MAP.MAINT_2FA_HARD_FLAG = 0;
            if (pMAINT_2FA_SOFT_FLAG_B.ToLower() == "true")
            {
                OBJ_LG_FNR_FUNCTION_MAP.MAINT_2FA_SOFT_FLAG = 1;
            }
            else
                OBJ_LG_FNR_FUNCTION_MAP.MAINT_2FA_SOFT_FLAG = 0;
            if (pREPORT_VIEW_FLAG_B.ToLower() == "true")
            {
                OBJ_LG_FNR_FUNCTION_MAP.REPORT_VIEW_FLAG = 1;
            }
            else
                OBJ_LG_FNR_FUNCTION_MAP.REPORT_VIEW_FLAG = 0;
            if (pREPORT_PRINT_FLAG_B.ToLower() == "true")
            {
                OBJ_LG_FNR_FUNCTION_MAP.REPORT_PRINT_FLAG = 1;
            }
            else
                OBJ_LG_FNR_FUNCTION_MAP.REPORT_PRINT_FLAG = 0;
            if (pREPORT_GEN_FLAG_B.ToLower() == "true")
            {
                OBJ_LG_FNR_FUNCTION_MAP.REPORT_GEN_FLAG = 1;
            }
            else
                OBJ_LG_FNR_FUNCTION_MAP.REPORT_GEN_FLAG = 0;
            if (pMAINT_AUTH_FLAG_B.ToLower() == "true")
            {
                OBJ_LG_FNR_FUNCTION_MAP.MAINT_AUTH_FLAG = 1;
            }
            else
                OBJ_LG_FNR_FUNCTION_MAP.MAINT_AUTH_FLAG = 0;
            if (pMAINT_BIO_FLAG_B.ToLower() == "true")
            {
                OBJ_LG_FNR_FUNCTION_MAP.MAINT_BIO_FLAG = 1;
            }
            else
                OBJ_LG_FNR_FUNCTION_MAP.MAINT_BIO_FLAG = 0;
            if (pPROCESS_FLAG_B.ToLower() == "true")
            {
                OBJ_LG_FNR_FUNCTION_MAP.PROCESS_FLAG = 1;
            }
            else
                OBJ_LG_FNR_FUNCTION_MAP.PROCESS_FLAG = 0;
            if (pHO_FUNCTION_FLAG_B.ToLower() == "true")
            {
                OBJ_LG_FNR_FUNCTION_MAP.HO_FUNCTION_FLAG = 1;
            }
            else
                OBJ_LG_FNR_FUNCTION_MAP.HO_FUNCTION_FLAG = 0;
            if (pENABLED_FLAG_B.ToLower() == "true")
            {
                OBJ_LG_FNR_FUNCTION_MAP.ENABLED_FLAG = 1;
            }
            else
                OBJ_LG_FNR_FUNCTION_MAP.ENABLED_FLAG = 0;
            return OBJ_LG_FNR_FUNCTION_MAP;
        }
        public static void LG_FNR_FUNCTION_MAP_INT_TO_BOOL(LG_FNR_FUNCTION_MAP OBJ_LG_FNR_FUNCTION_MAP)
        {
            if (OBJ_LG_FNR_FUNCTION_MAP.MAINT_CRT_FLAG == 1)
            {
                OBJ_LG_FNR_FUNCTION_MAP.MAINT_CRT_FLAG_B = true;
            }
            else
                OBJ_LG_FNR_FUNCTION_MAP.MAINT_CRT_FLAG_B = false;
            if (OBJ_LG_FNR_FUNCTION_MAP.MAINT_EDT_FLAG == 1)
            {
                OBJ_LG_FNR_FUNCTION_MAP.MAINT_EDT_FLAG_B = true;
            }
            else
                OBJ_LG_FNR_FUNCTION_MAP.MAINT_EDT_FLAG_B = false;
            if (OBJ_LG_FNR_FUNCTION_MAP.MAINT_DEL_FLAG == 1)
            {
                OBJ_LG_FNR_FUNCTION_MAP.MAINT_DEL_FLAG_B = true;
            }
            else
                OBJ_LG_FNR_FUNCTION_MAP.MAINT_DEL_FLAG_B = false;
            if (OBJ_LG_FNR_FUNCTION_MAP.MAINT_DTL_FLAG == 1)
            {
                OBJ_LG_FNR_FUNCTION_MAP.MAINT_DTL_FLAG_B = true;
            }
            else
                OBJ_LG_FNR_FUNCTION_MAP.MAINT_DTL_FLAG_B = false;
            if (OBJ_LG_FNR_FUNCTION_MAP.MAINT_INDX_FLAG == 1)
            {
                OBJ_LG_FNR_FUNCTION_MAP.MAINT_INDX_FLAG_B = true;
            }
            else
                OBJ_LG_FNR_FUNCTION_MAP.MAINT_INDX_FLAG_B = false;
            if (OBJ_LG_FNR_FUNCTION_MAP.MAINT_OTP_FLAG == 1)
            {
                OBJ_LG_FNR_FUNCTION_MAP.MAINT_OTP_FLAG_B = true;
            }
            else
                OBJ_LG_FNR_FUNCTION_MAP.MAINT_OTP_FLAG_B = false;
            if (OBJ_LG_FNR_FUNCTION_MAP.MAINT_2FA_FLAG == 1)
            {
                OBJ_LG_FNR_FUNCTION_MAP.MAINT_2FA_FLAG_B = true;
            }
            else
                OBJ_LG_FNR_FUNCTION_MAP.MAINT_2FA_FLAG_B = false;
            if (OBJ_LG_FNR_FUNCTION_MAP.MAINT_2FA_HARD_FLAG == 1)
            {
                OBJ_LG_FNR_FUNCTION_MAP.MAINT_2FA_HARD_FLAG_B = true;
            }
            else
                OBJ_LG_FNR_FUNCTION_MAP.MAINT_2FA_HARD_FLAG_B = false;
            if (OBJ_LG_FNR_FUNCTION_MAP.MAINT_2FA_SOFT_FLAG == 1)
            {
                OBJ_LG_FNR_FUNCTION_MAP.MAINT_2FA_SOFT_FLAG_B = true;
            }
            else
                OBJ_LG_FNR_FUNCTION_MAP.MAINT_2FA_SOFT_FLAG_B = false;
            if (OBJ_LG_FNR_FUNCTION_MAP.REPORT_VIEW_FLAG == 1)
            {
                OBJ_LG_FNR_FUNCTION_MAP.REPORT_VIEW_FLAG_B = true;
            }
            else
                OBJ_LG_FNR_FUNCTION_MAP.REPORT_VIEW_FLAG_B = false;
            if (OBJ_LG_FNR_FUNCTION_MAP.REPORT_PRINT_FLAG == 1)
            {
                OBJ_LG_FNR_FUNCTION_MAP.REPORT_PRINT_FLAG_B = true;
            }
            else
                OBJ_LG_FNR_FUNCTION_MAP.REPORT_PRINT_FLAG_B = false;
            if (OBJ_LG_FNR_FUNCTION_MAP.REPORT_GEN_FLAG == 1)
            {
                OBJ_LG_FNR_FUNCTION_MAP.REPORT_GEN_FLAG_B = true;
            }
            else
                OBJ_LG_FNR_FUNCTION_MAP.REPORT_GEN_FLAG_B = false;
            if (OBJ_LG_FNR_FUNCTION_MAP.MAINT_AUTH_FLAG == 1)
            {
                OBJ_LG_FNR_FUNCTION_MAP.MAINT_AUTH_FLAG_B = true;
            }
            else
                OBJ_LG_FNR_FUNCTION_MAP.MAINT_AUTH_FLAG_B = false;
            if (OBJ_LG_FNR_FUNCTION_MAP.MAINT_BIO_FLAG == 1)
            {
                OBJ_LG_FNR_FUNCTION_MAP.MAINT_BIO_FLAG_B = true;
            }
            else
                OBJ_LG_FNR_FUNCTION_MAP.MAINT_BIO_FLAG_B = false;
            if (OBJ_LG_FNR_FUNCTION_MAP.PROCESS_FLAG == 1)
            {
                OBJ_LG_FNR_FUNCTION_MAP.PROCESS_FLAG_B = true;
            }
            else
                OBJ_LG_FNR_FUNCTION_MAP.PROCESS_FLAG_B = false;
            if (OBJ_LG_FNR_FUNCTION_MAP.HO_FUNCTION_FLAG == 1)
            {
                OBJ_LG_FNR_FUNCTION_MAP.HO_FUNCTION_FLAG_B = true;
            }
            else
                OBJ_LG_FNR_FUNCTION_MAP.HO_FUNCTION_FLAG_B = false;
            if (OBJ_LG_FNR_FUNCTION_MAP.ENABLED_FLAG == 1)
            {
                OBJ_LG_FNR_FUNCTION_MAP.ENABLED_FLAG_B = true;
            }
            else
                OBJ_LG_FNR_FUNCTION_MAP.ENABLED_FLAG_B = false;
        }

        public static void LG_FNR_ROLE_DEFINE_MAP_INT_TO_BOOL(LG_FNR_ROLE_DEFINE_MAP OBJ_LG_FNR_ROLE_DEFINE_MAP)
        {
            if (OBJ_LG_FNR_ROLE_DEFINE_MAP.MAINT_CRT_FLAG == 1)
            {
                OBJ_LG_FNR_ROLE_DEFINE_MAP.MAINT_CRT_FLAG_B = true;
            }
            else
                OBJ_LG_FNR_ROLE_DEFINE_MAP.MAINT_CRT_FLAG_B = false;
            if (OBJ_LG_FNR_ROLE_DEFINE_MAP.MAINT_EDT_FLAG == 1)
            {
                OBJ_LG_FNR_ROLE_DEFINE_MAP.MAINT_EDT_FLAG_B = true;
            }
            else
                OBJ_LG_FNR_ROLE_DEFINE_MAP.MAINT_EDT_FLAG_B = false;
            if (OBJ_LG_FNR_ROLE_DEFINE_MAP.MAINT_DEL_FLAG == 1)
            {
                OBJ_LG_FNR_ROLE_DEFINE_MAP.MAINT_DEL_FLAG_B = true;
            }
            else
                OBJ_LG_FNR_ROLE_DEFINE_MAP.MAINT_DEL_FLAG_B = false;
            if (OBJ_LG_FNR_ROLE_DEFINE_MAP.MAINT_DTL_FLAG == 1)
            {
                OBJ_LG_FNR_ROLE_DEFINE_MAP.MAINT_DTL_FLAG_B = true;
            }
            else
                OBJ_LG_FNR_ROLE_DEFINE_MAP.MAINT_DTL_FLAG_B = false;
            if (OBJ_LG_FNR_ROLE_DEFINE_MAP.MAINT_INDX_FLAG == 1)
            {
                OBJ_LG_FNR_ROLE_DEFINE_MAP.MAINT_INDX_FLAG_B = true;
            }
            else
                OBJ_LG_FNR_ROLE_DEFINE_MAP.MAINT_INDX_FLAG_B = false;
            if (OBJ_LG_FNR_ROLE_DEFINE_MAP.MAINT_OTP_FLAG == 1)
            {
                OBJ_LG_FNR_ROLE_DEFINE_MAP.MAINT_OTP_FLAG_B = true;
            }
            else
                OBJ_LG_FNR_ROLE_DEFINE_MAP.MAINT_OTP_FLAG_B = false;
            if (OBJ_LG_FNR_ROLE_DEFINE_MAP.MAINT_2FA_FLAG == 1)
            {
                OBJ_LG_FNR_ROLE_DEFINE_MAP.MAINT_2FA_FLAG_B = true;
            }
            else
                OBJ_LG_FNR_ROLE_DEFINE_MAP.MAINT_2FA_FLAG_B = false;
            if (OBJ_LG_FNR_ROLE_DEFINE_MAP.MAINT_2FA_HARD_FLAG == 1)
            {
                OBJ_LG_FNR_ROLE_DEFINE_MAP.MAINT_2FA_HARD_FLAG_B = true;
            }
            else
                OBJ_LG_FNR_ROLE_DEFINE_MAP.MAINT_2FA_HARD_FLAG_B = false;
            if (OBJ_LG_FNR_ROLE_DEFINE_MAP.MAINT_2FA_SOFT_FLAG == 1)
            {
                OBJ_LG_FNR_ROLE_DEFINE_MAP.MAINT_2FA_SOFT_FLAG_B = true;
            }
            else
                OBJ_LG_FNR_ROLE_DEFINE_MAP.MAINT_2FA_SOFT_FLAG_B = false;
            if (OBJ_LG_FNR_ROLE_DEFINE_MAP.REPORT_VIEW_FLAG == 1)
            {
                OBJ_LG_FNR_ROLE_DEFINE_MAP.REPORT_VIEW_FLAG_B = true;
            }
            else
                OBJ_LG_FNR_ROLE_DEFINE_MAP.REPORT_VIEW_FLAG_B = false;
            if (OBJ_LG_FNR_ROLE_DEFINE_MAP.REPORT_PRINT_FLAG == 1)
            {
                OBJ_LG_FNR_ROLE_DEFINE_MAP.REPORT_PRINT_FLAG_B = true;
            }
            else
                OBJ_LG_FNR_ROLE_DEFINE_MAP.REPORT_PRINT_FLAG_B = false;
            if (OBJ_LG_FNR_ROLE_DEFINE_MAP.REPORT_GEN_FLAG == 1)
            {
                OBJ_LG_FNR_ROLE_DEFINE_MAP.REPORT_GEN_FLAG_B = true;
            }
            else
                OBJ_LG_FNR_ROLE_DEFINE_MAP.REPORT_GEN_FLAG_B = false;
            if (OBJ_LG_FNR_ROLE_DEFINE_MAP.MAINT_AUTH_FLAG == 1)
            {
                OBJ_LG_FNR_ROLE_DEFINE_MAP.MAINT_AUTH_FLAG_B = true;
            }
            else
                OBJ_LG_FNR_ROLE_DEFINE_MAP.MAINT_AUTH_FLAG_B = false;
        }

        public static void LG_2FA_OTP_CONFIG_MAP_INT_TO_BOOL(LG_2FA_OTP_CONFIG_MAP OBJ_LG_2FA_OTP_CONFIG_MAP)
        {
            if (OBJ_LG_2FA_OTP_CONFIG_MAP.MAIL_FLAG == 1)
            {
                OBJ_LG_2FA_OTP_CONFIG_MAP.MAIL_FLAG_B = true;
            }
            else
                OBJ_LG_2FA_OTP_CONFIG_MAP.MAIL_FLAG_B = false;
            if (OBJ_LG_2FA_OTP_CONFIG_MAP.SMS_FLAG == 1)
            {
                OBJ_LG_2FA_OTP_CONFIG_MAP.SMS_FLAG_B = true;
            }
            else
                OBJ_LG_2FA_OTP_CONFIG_MAP.SMS_FLAG_B = false;
        }
        public static LG_2FA_OTP_CONFIG LG_2FA_OTP_CONFIG_MAP_BOOL_TO_INT(string pMAIL_FLAG, string pSMS_FLAG, LG_2FA_OTP_CONFIG pOBJ_LG_2FA_OTP_CONFIG)
        {
            if (pMAIL_FLAG.ToLower() == "true")
            {
                pOBJ_LG_2FA_OTP_CONFIG.MAIL_FLAG = 1;
            }
            else
                pOBJ_LG_2FA_OTP_CONFIG.MAIL_FLAG = 0;
            if (pSMS_FLAG.ToLower() == "true")
            {
                pOBJ_LG_2FA_OTP_CONFIG.SMS_FLAG = 1;
            }
            else
                pOBJ_LG_2FA_OTP_CONFIG.SMS_FLAG = 0;

            return pOBJ_LG_2FA_OTP_CONFIG;
        }

      
        
        #region BIND AD USER COMBOBOX
        public static LG_USER_AD_BINDING LG_USER_AD_BINDING_MAP_BOOL_TO_INT(string pAD_ACTIVE_FLAG_B, LG_USER_AD_BINDING pLG_USER_AD_BINDING)
        {
            if (pAD_ACTIVE_FLAG_B.ToLower() == "true")
            {
                pLG_USER_AD_BINDING.AD_ACTIVE_FLAG = 1;
            }
            else
                pLG_USER_AD_BINDING.AD_ACTIVE_FLAG = 0;

            return pLG_USER_AD_BINDING;
        }

        
        public static void LG_USER_AD_BINDING_MAP_INT_TO_BOOL(LG_USER_AD_BINDING_MAP OBJ_LG_USER_AD_BINDING_MAP)
        {
            if (OBJ_LG_USER_AD_BINDING_MAP.AD_ACTIVE_FLAG == 1)
            {
                OBJ_LG_USER_AD_BINDING_MAP.AD_ACTIVE_FLAG_B = true;
            }
            else
                OBJ_LG_USER_AD_BINDING_MAP.AD_ACTIVE_FLAG_B = false;          
        }
        #endregion



    }
}
