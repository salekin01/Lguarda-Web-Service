using Model.EDMX;
using Model.EntityModel.Common;
using Model.EntityModel.LGModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Mapper
{
    public class Class_Conversion
    {
        public static LG_FNR_FUNCTION LG_FNR_FUNCTION_CON(LG_FNR_FUNCTION OBJ_LG_FNR_FUNCTION, LG_FNR_FUNCTION_MAP OBJ_LG_FNR_FUNCTION_MAP)
        {
            OBJ_LG_FNR_FUNCTION.MAINT_CRT_FLAG = OBJ_LG_FNR_FUNCTION_MAP.MAINT_CRT_FLAG;
            OBJ_LG_FNR_FUNCTION.MAINT_EDT_FLAG = OBJ_LG_FNR_FUNCTION_MAP.MAINT_EDT_FLAG;
            OBJ_LG_FNR_FUNCTION.MAINT_DEL_FLAG = OBJ_LG_FNR_FUNCTION_MAP.MAINT_DEL_FLAG;
            OBJ_LG_FNR_FUNCTION.MAINT_DTL_FLAG = OBJ_LG_FNR_FUNCTION_MAP.MAINT_DTL_FLAG;
            OBJ_LG_FNR_FUNCTION.MAINT_INDX_FLAG = OBJ_LG_FNR_FUNCTION_MAP.MAINT_INDX_FLAG;
            OBJ_LG_FNR_FUNCTION.MAINT_AUTH_FLAG = OBJ_LG_FNR_FUNCTION_MAP.MAINT_AUTH_FLAG;

            OBJ_LG_FNR_FUNCTION.MAINT_OTP_FLAG = OBJ_LG_FNR_FUNCTION_MAP.MAINT_OTP_FLAG;
            OBJ_LG_FNR_FUNCTION.MAINT_BIO_FLAG = OBJ_LG_FNR_FUNCTION_MAP.MAINT_BIO_FLAG;
            OBJ_LG_FNR_FUNCTION.MAINT_2FA_FLAG = OBJ_LG_FNR_FUNCTION_MAP.MAINT_2FA_FLAG;
            OBJ_LG_FNR_FUNCTION.MAINT_2FA_HARD_FLAG = OBJ_LG_FNR_FUNCTION_MAP.MAINT_2FA_HARD_FLAG;
            OBJ_LG_FNR_FUNCTION.MAINT_2FA_SOFT_FLAG = OBJ_LG_FNR_FUNCTION_MAP.MAINT_2FA_SOFT_FLAG;

            OBJ_LG_FNR_FUNCTION.REPORT_VIEW_FLAG = OBJ_LG_FNR_FUNCTION_MAP.REPORT_VIEW_FLAG;
            OBJ_LG_FNR_FUNCTION.REPORT_PRINT_FLAG = OBJ_LG_FNR_FUNCTION_MAP.REPORT_PRINT_FLAG;
            OBJ_LG_FNR_FUNCTION.REPORT_GEN_FLAG = OBJ_LG_FNR_FUNCTION_MAP.REPORT_GEN_FLAG;

            OBJ_LG_FNR_FUNCTION.PROCESS_FLAG = OBJ_LG_FNR_FUNCTION_MAP.PROCESS_FLAG;
            OBJ_LG_FNR_FUNCTION.ENABLED_FLAG = OBJ_LG_FNR_FUNCTION_MAP.ENABLED_FLAG;
            OBJ_LG_FNR_FUNCTION.HO_FUNCTION_FLAG = OBJ_LG_FNR_FUNCTION_MAP.HO_FUNCTION_FLAG;
            return OBJ_LG_FNR_FUNCTION;
        }
        public static LG_FNR_FUNCTION_MAP LG_FNR_FUNCTION_REVERSE_CON(LG_FNR_FUNCTION_MAP OBJ_LG_FNR_FUNCTION_MAP, LG_FNR_FUNCTION OBJ_LG_FNR_FUNCTION)
        {
            OBJ_LG_FNR_FUNCTION_MAP.MAINT_CRT_FLAG = OBJ_LG_FNR_FUNCTION.MAINT_CRT_FLAG;
            OBJ_LG_FNR_FUNCTION_MAP.MAINT_EDT_FLAG = OBJ_LG_FNR_FUNCTION.MAINT_EDT_FLAG;
            OBJ_LG_FNR_FUNCTION_MAP.MAINT_DEL_FLAG = OBJ_LG_FNR_FUNCTION.MAINT_DEL_FLAG;
            OBJ_LG_FNR_FUNCTION_MAP.MAINT_DTL_FLAG = OBJ_LG_FNR_FUNCTION.MAINT_DTL_FLAG;
            OBJ_LG_FNR_FUNCTION_MAP.MAINT_INDX_FLAG = OBJ_LG_FNR_FUNCTION.MAINT_INDX_FLAG;
            OBJ_LG_FNR_FUNCTION_MAP.MAINT_AUTH_FLAG = OBJ_LG_FNR_FUNCTION.MAINT_AUTH_FLAG;

            OBJ_LG_FNR_FUNCTION_MAP.MAINT_OTP_FLAG = OBJ_LG_FNR_FUNCTION.MAINT_OTP_FLAG;
            OBJ_LG_FNR_FUNCTION_MAP.MAINT_BIO_FLAG = OBJ_LG_FNR_FUNCTION.MAINT_BIO_FLAG;
            OBJ_LG_FNR_FUNCTION_MAP.MAINT_2FA_FLAG = OBJ_LG_FNR_FUNCTION.MAINT_2FA_FLAG;
            OBJ_LG_FNR_FUNCTION_MAP.MAINT_2FA_HARD_FLAG = OBJ_LG_FNR_FUNCTION.MAINT_2FA_HARD_FLAG;
            OBJ_LG_FNR_FUNCTION_MAP.MAINT_2FA_SOFT_FLAG = OBJ_LG_FNR_FUNCTION.MAINT_2FA_SOFT_FLAG;

            OBJ_LG_FNR_FUNCTION_MAP.REPORT_VIEW_FLAG = OBJ_LG_FNR_FUNCTION.REPORT_VIEW_FLAG;
            OBJ_LG_FNR_FUNCTION_MAP.REPORT_PRINT_FLAG = OBJ_LG_FNR_FUNCTION.REPORT_PRINT_FLAG;
            OBJ_LG_FNR_FUNCTION_MAP.REPORT_GEN_FLAG = OBJ_LG_FNR_FUNCTION.REPORT_GEN_FLAG;

            OBJ_LG_FNR_FUNCTION_MAP.FUNCTION_ID = OBJ_LG_FNR_FUNCTION.FUNCTION_ID;
            OBJ_LG_FNR_FUNCTION_MAP.FUNCTION_NM = OBJ_LG_FNR_FUNCTION.FUNCTION_NM;
            OBJ_LG_FNR_FUNCTION_MAP.APPLICATION_ID = OBJ_LG_FNR_FUNCTION.APPLICATION_ID;
            OBJ_LG_FNR_FUNCTION_MAP.SERVICE_ID = OBJ_LG_FNR_FUNCTION.SERVICE_ID;
            OBJ_LG_FNR_FUNCTION_MAP.MODULE_ID = OBJ_LG_FNR_FUNCTION.MODULE_ID;
            OBJ_LG_FNR_FUNCTION_MAP.ITEM_TYPE = OBJ_LG_FNR_FUNCTION.ITEM_TYPE;
            OBJ_LG_FNR_FUNCTION_MAP.AUTH_LEVEL = OBJ_LG_FNR_FUNCTION.AUTH_LEVEL;
            OBJ_LG_FNR_FUNCTION_MAP.AUTH_STATUS_ID = OBJ_LG_FNR_FUNCTION.AUTH_STATUS_ID;
            OBJ_LG_FNR_FUNCTION_MAP.LAST_ACTION = OBJ_LG_FNR_FUNCTION.LAST_ACTION;
            OBJ_LG_FNR_FUNCTION_MAP.MAKE_DT = OBJ_LG_FNR_FUNCTION.MAKE_DT;

            OBJ_LG_FNR_FUNCTION_MAP.PROCESS_FLAG = OBJ_LG_FNR_FUNCTION.PROCESS_FLAG;
            OBJ_LG_FNR_FUNCTION_MAP.ENABLED_FLAG = OBJ_LG_FNR_FUNCTION.ENABLED_FLAG;
            OBJ_LG_FNR_FUNCTION_MAP.HO_FUNCTION_FLAG = OBJ_LG_FNR_FUNCTION.HO_FUNCTION_FLAG;
            OBJ_LG_FNR_FUNCTION_MAP.FAST_PATH_NO = OBJ_LG_FNR_FUNCTION.FAST_PATH_NO;
            OBJ_LG_FNR_FUNCTION_MAP.TARGET_PATH = OBJ_LG_FNR_FUNCTION.TARGET_PATH;
            OBJ_LG_FNR_FUNCTION_MAP.DB_ROLE_NAME = OBJ_LG_FNR_FUNCTION.DB_ROLE_NAME;

            return OBJ_LG_FNR_FUNCTION_MAP;
        }
       
        public static LG_FNR_ROLE_DEFINE LG_FNR_ROLE_DEFINE_CON(LG_FNR_ROLE_DEFINE OBJ_LG_FNR_ROLE_DEFINE, LG_FNR_ROLE_DEFINE_MAP OBJ_LG_FNR_ROLE_DEFINE_MAP)
        {
            OBJ_LG_FNR_ROLE_DEFINE.MAINT_CRT_FLAG = OBJ_LG_FNR_ROLE_DEFINE_MAP.MAINT_CRT_FLAG;
            OBJ_LG_FNR_ROLE_DEFINE.MAINT_EDT_FLAG = OBJ_LG_FNR_ROLE_DEFINE_MAP.MAINT_EDT_FLAG;
            OBJ_LG_FNR_ROLE_DEFINE.MAINT_DEL_FLAG = OBJ_LG_FNR_ROLE_DEFINE_MAP.MAINT_DEL_FLAG;
            OBJ_LG_FNR_ROLE_DEFINE.MAINT_DTL_FLAG = OBJ_LG_FNR_ROLE_DEFINE_MAP.MAINT_DTL_FLAG;
            OBJ_LG_FNR_ROLE_DEFINE.MAINT_INDX_FLAG = OBJ_LG_FNR_ROLE_DEFINE_MAP.MAINT_INDX_FLAG;
            OBJ_LG_FNR_ROLE_DEFINE.MAINT_AUTH_FLAG = OBJ_LG_FNR_ROLE_DEFINE_MAP.MAINT_AUTH_FLAG;

            OBJ_LG_FNR_ROLE_DEFINE.MAINT_OTP_FLAG = OBJ_LG_FNR_ROLE_DEFINE_MAP.MAINT_OTP_FLAG;
            OBJ_LG_FNR_ROLE_DEFINE.MAINT_2FA_FLAG = OBJ_LG_FNR_ROLE_DEFINE_MAP.MAINT_2FA_FLAG;
            OBJ_LG_FNR_ROLE_DEFINE.MAINT_2FA_HARD_FLAG = OBJ_LG_FNR_ROLE_DEFINE_MAP.MAINT_2FA_HARD_FLAG;
            OBJ_LG_FNR_ROLE_DEFINE.MAINT_2FA_SOFT_FLAG = OBJ_LG_FNR_ROLE_DEFINE_MAP.MAINT_2FA_SOFT_FLAG;

            OBJ_LG_FNR_ROLE_DEFINE.REPORT_VIEW_FLAG = OBJ_LG_FNR_ROLE_DEFINE_MAP.REPORT_VIEW_FLAG;
            OBJ_LG_FNR_ROLE_DEFINE.REPORT_PRINT_FLAG = OBJ_LG_FNR_ROLE_DEFINE_MAP.REPORT_PRINT_FLAG;
            OBJ_LG_FNR_ROLE_DEFINE.REPORT_GEN_FLAG = OBJ_LG_FNR_ROLE_DEFINE_MAP.REPORT_GEN_FLAG;

            OBJ_LG_FNR_ROLE_DEFINE.FUNCTION_ID = OBJ_LG_FNR_ROLE_DEFINE_MAP.FUNCTION_ID;
            if (OBJ_LG_FNR_ROLE_DEFINE_MAP.AUTH_LEVEL != null)
            {
                OBJ_LG_FNR_ROLE_DEFINE.AUTH_LEVEL = (short)OBJ_LG_FNR_ROLE_DEFINE_MAP.AUTH_LEVEL;
            }
            else
                OBJ_LG_FNR_ROLE_DEFINE.AUTH_LEVEL = 0;
            OBJ_LG_FNR_ROLE_DEFINE.APPLICATION_ID = OBJ_LG_FNR_ROLE_DEFINE_MAP.APPLICATION_ID;
            return OBJ_LG_FNR_ROLE_DEFINE;
        }
        public static LG_FNR_ROLE_DEFINE_MAP LG_FNR_ROLE_DEFINE_REVERSE_CON(LG_FNR_ROLE_DEFINE_MAP OBJ_LG_FNR_ROLE_DEFINE_MAP, LG_FNR_ROLE_DEFINE OBJ_LG_FNR_ROLE_DEFINE)
        {
            OBJ_LG_FNR_ROLE_DEFINE_MAP.MAINT_CRT_FLAG = OBJ_LG_FNR_ROLE_DEFINE.MAINT_CRT_FLAG;
            OBJ_LG_FNR_ROLE_DEFINE_MAP.MAINT_EDT_FLAG = OBJ_LG_FNR_ROLE_DEFINE.MAINT_EDT_FLAG;
            OBJ_LG_FNR_ROLE_DEFINE_MAP.MAINT_DEL_FLAG = OBJ_LG_FNR_ROLE_DEFINE.MAINT_DEL_FLAG;
            OBJ_LG_FNR_ROLE_DEFINE_MAP.MAINT_DTL_FLAG = OBJ_LG_FNR_ROLE_DEFINE.MAINT_DTL_FLAG;
            OBJ_LG_FNR_ROLE_DEFINE_MAP.MAINT_INDX_FLAG = OBJ_LG_FNR_ROLE_DEFINE.MAINT_INDX_FLAG;
            OBJ_LG_FNR_ROLE_DEFINE_MAP.MAINT_AUTH_FLAG = OBJ_LG_FNR_ROLE_DEFINE.MAINT_AUTH_FLAG;

            OBJ_LG_FNR_ROLE_DEFINE_MAP.MAINT_OTP_FLAG = OBJ_LG_FNR_ROLE_DEFINE.MAINT_OTP_FLAG;
            OBJ_LG_FNR_ROLE_DEFINE_MAP.MAINT_2FA_FLAG = OBJ_LG_FNR_ROLE_DEFINE.MAINT_2FA_FLAG;
            OBJ_LG_FNR_ROLE_DEFINE_MAP.MAINT_2FA_HARD_FLAG = OBJ_LG_FNR_ROLE_DEFINE.MAINT_2FA_HARD_FLAG;
            OBJ_LG_FNR_ROLE_DEFINE_MAP.MAINT_2FA_SOFT_FLAG = OBJ_LG_FNR_ROLE_DEFINE.MAINT_2FA_SOFT_FLAG;

            OBJ_LG_FNR_ROLE_DEFINE_MAP.REPORT_VIEW_FLAG = OBJ_LG_FNR_ROLE_DEFINE.REPORT_VIEW_FLAG;
            OBJ_LG_FNR_ROLE_DEFINE_MAP.REPORT_PRINT_FLAG = OBJ_LG_FNR_ROLE_DEFINE.REPORT_PRINT_FLAG;
            OBJ_LG_FNR_ROLE_DEFINE_MAP.REPORT_GEN_FLAG = OBJ_LG_FNR_ROLE_DEFINE.REPORT_GEN_FLAG;

            OBJ_LG_FNR_ROLE_DEFINE_MAP.FUNCTION_ID = OBJ_LG_FNR_ROLE_DEFINE.FUNCTION_ID;
            if (OBJ_LG_FNR_ROLE_DEFINE.AUTH_LEVEL != null)
            {
                OBJ_LG_FNR_ROLE_DEFINE_MAP.AUTH_LEVEL = OBJ_LG_FNR_ROLE_DEFINE.AUTH_LEVEL;
            }
            else
                OBJ_LG_FNR_ROLE_DEFINE_MAP.AUTH_LEVEL = 0;
            return OBJ_LG_FNR_ROLE_DEFINE_MAP;
        }
        

        public static LG_FNR_APPLICATION_MAP LG_FNR_APPLICATION_REVERSE_CON(LG_FNR_APPLICATION_MAP OBJ_LG_FNR_APPLICATION_MAP, LG_FNR_APPLICATION OBJ_LG_FNR_APPLICATION)
        {
            OBJ_LG_FNR_APPLICATION_MAP.APPLICATION_ID = OBJ_LG_FNR_APPLICATION.APPLICATION_ID;
            OBJ_LG_FNR_APPLICATION_MAP.APPLICATION_NAME = OBJ_LG_FNR_APPLICATION.APPLICATION_NAME;
            OBJ_LG_FNR_APPLICATION_MAP.APP_TYPE_ID = OBJ_LG_FNR_APPLICATION.APP_TYPE_ID;
            OBJ_LG_FNR_APPLICATION_MAP.AUTH_STATUS_ID = OBJ_LG_FNR_APPLICATION.AUTH_STATUS_ID;
            OBJ_LG_FNR_APPLICATION_MAP.LAST_ACTION = OBJ_LG_FNR_APPLICATION.LAST_ACTION;
            OBJ_LG_FNR_APPLICATION_MAP.MAKE_DT = OBJ_LG_FNR_APPLICATION.MAKE_DT;
            OBJ_LG_FNR_APPLICATION_MAP.LAST_UPDATE_DT = OBJ_LG_FNR_APPLICATION.LAST_UPDATE_DT;
            return OBJ_LG_FNR_APPLICATION_MAP;
        }
        public static LG_FNR_APPLICATION LG_FNR_APPLICATION_CON(LG_FNR_APPLICATION OBJ_LG_FNR_APPLICATION, LG_FNR_APPLICATION_MAP OBJ_LG_FNR_APPLICATION_MAP)
        {
            OBJ_LG_FNR_APPLICATION.APPLICATION_ID = OBJ_LG_FNR_APPLICATION_MAP.APPLICATION_ID;
            OBJ_LG_FNR_APPLICATION.APPLICATION_NAME = OBJ_LG_FNR_APPLICATION_MAP.APPLICATION_NAME;
            OBJ_LG_FNR_APPLICATION.AUTH_STATUS_ID = OBJ_LG_FNR_APPLICATION_MAP.AUTH_STATUS_ID;
            OBJ_LG_FNR_APPLICATION.LAST_ACTION = OBJ_LG_FNR_APPLICATION_MAP.LAST_ACTION;
            OBJ_LG_FNR_APPLICATION.MAKE_DT = OBJ_LG_FNR_APPLICATION_MAP.MAKE_DT;
            OBJ_LG_FNR_APPLICATION.LAST_UPDATE_DT = OBJ_LG_FNR_APPLICATION_MAP.LAST_UPDATE_DT;
            return OBJ_LG_FNR_APPLICATION;
        }


        public static LG_FNR_SERVICE_MAP LG_FNR_SERVICE_REVERSE_CON(LG_FNR_SERVICE_MAP OBJ_LG_FNR_SERVICE_MAP, LG_FNR_SERVICE OBJ_LG_FNR_SERVICE)
        {
            OBJ_LG_FNR_SERVICE_MAP.SERVICE_ID = OBJ_LG_FNR_SERVICE.SERVICE_ID;
            OBJ_LG_FNR_SERVICE_MAP.SERVICE_NM = OBJ_LG_FNR_SERVICE.SERVICE_NM;
            OBJ_LG_FNR_SERVICE_MAP.SERVICE_SH_NM = OBJ_LG_FNR_SERVICE.SERVICE_SH_NM;
            OBJ_LG_FNR_SERVICE_MAP.APPLICATION_ID = OBJ_LG_FNR_SERVICE.APPLICATION_ID;
            OBJ_LG_FNR_SERVICE_MAP.AUTH_STATUS_ID = OBJ_LG_FNR_SERVICE.AUTH_STATUS_ID;
            OBJ_LG_FNR_SERVICE_MAP.LAST_ACTION = OBJ_LG_FNR_SERVICE.LAST_ACTION;
            OBJ_LG_FNR_SERVICE_MAP.MAKE_DT = OBJ_LG_FNR_SERVICE.MAKE_DT;
            OBJ_LG_FNR_SERVICE_MAP.LAST_UPDATE_DT = OBJ_LG_FNR_SERVICE.LAST_UPDATE_DT;
            return OBJ_LG_FNR_SERVICE_MAP;
        }
        public static LG_FNR_SERVICE LG_FNR_SERVICE_CON(LG_FNR_SERVICE OBJ_LG_FNR_SERVICE, LG_FNR_SERVICE_MAP OBJ_LG_FNR_SERVICE_MAP)
        {
            OBJ_LG_FNR_SERVICE.SERVICE_ID = OBJ_LG_FNR_SERVICE_MAP.SERVICE_ID;
            OBJ_LG_FNR_SERVICE.SERVICE_NM = OBJ_LG_FNR_SERVICE_MAP.SERVICE_NM;
            OBJ_LG_FNR_SERVICE.SERVICE_SH_NM = OBJ_LG_FNR_SERVICE_MAP.SERVICE_SH_NM;
            OBJ_LG_FNR_SERVICE.APPLICATION_ID = OBJ_LG_FNR_SERVICE_MAP.APPLICATION_ID;
            OBJ_LG_FNR_SERVICE.AUTH_STATUS_ID = OBJ_LG_FNR_SERVICE_MAP.AUTH_STATUS_ID;
            OBJ_LG_FNR_SERVICE.LAST_ACTION = OBJ_LG_FNR_SERVICE_MAP.LAST_ACTION;
            OBJ_LG_FNR_SERVICE.MAKE_DT = OBJ_LG_FNR_SERVICE_MAP.MAKE_DT;
            OBJ_LG_FNR_SERVICE.LAST_UPDATE_DT = OBJ_LG_FNR_SERVICE_MAP.LAST_UPDATE_DT;
            return OBJ_LG_FNR_SERVICE;
        }


        public static LG_USER_AD_BINDING_MAP LG_USER_AD_BINDING_REVERSE_CON(LG_USER_AD_BINDING_MAP OBJ_LG_USER_AD_BINDING_MAP, LG_USER_AD_BINDING OBJ_LG_USER_AD_BINDING)
        {
            OBJ_LG_USER_AD_BINDING_MAP.SL = (int)OBJ_LG_USER_AD_BINDING.SL;
            OBJ_LG_USER_AD_BINDING_MAP.DOMAIN = OBJ_LG_USER_AD_BINDING.DOMAIN;
            OBJ_LG_USER_AD_BINDING_MAP.DOMAIN_ID = OBJ_LG_USER_AD_BINDING.DOMAIN_ID;
            OBJ_LG_USER_AD_BINDING_MAP.USER_ID = OBJ_LG_USER_AD_BINDING.USER_ID;
            OBJ_LG_USER_AD_BINDING_MAP.AD_ACTIVE_FLAG = OBJ_LG_USER_AD_BINDING.AD_ACTIVE_FLAG;
            OBJ_LG_USER_AD_BINDING_MAP.AUTH_STATUS_ID = OBJ_LG_USER_AD_BINDING.AUTH_STATUS_ID;
            OBJ_LG_USER_AD_BINDING_MAP.LAST_ACTION = OBJ_LG_USER_AD_BINDING.LAST_ACTION;
            OBJ_LG_USER_AD_BINDING_MAP.MAKE_DT = (DateTime)OBJ_LG_USER_AD_BINDING.MAKE_DT;
            OBJ_LG_USER_AD_BINDING_MAP.LAST_UPDATE_DT = OBJ_LG_USER_AD_BINDING.LAST_UPDATE_DT;
            if (OBJ_LG_USER_AD_BINDING.AD_ACTIVE_FLAG == 1)
            {
                OBJ_LG_USER_AD_BINDING_MAP.AD_ACTIVE_FLAG_B = true;
            }
            else
                OBJ_LG_USER_AD_BINDING_MAP.AD_ACTIVE_FLAG_B = false;
            return OBJ_LG_USER_AD_BINDING_MAP;
        }
        public static LG_USER_AD_BINDING LG_USER_AD_BINDING_CON(LG_USER_AD_BINDING OBJ_LG_USER_AD_BINDING, LG_USER_AD_BINDING_MAP OBJ_LG_USER_AD_BINDING_MAP)
        {
            OBJ_LG_USER_AD_BINDING.SL = OBJ_LG_USER_AD_BINDING_MAP.SL;
            OBJ_LG_USER_AD_BINDING.DOMAIN = OBJ_LG_USER_AD_BINDING_MAP.DOMAIN;
            OBJ_LG_USER_AD_BINDING.DOMAIN_ID = OBJ_LG_USER_AD_BINDING_MAP.DOMAIN_ID;
            OBJ_LG_USER_AD_BINDING.USER_ID = OBJ_LG_USER_AD_BINDING_MAP.USER_ID;
            OBJ_LG_USER_AD_BINDING.AD_ACTIVE_FLAG = (short)OBJ_LG_USER_AD_BINDING_MAP.AD_ACTIVE_FLAG;
            OBJ_LG_USER_AD_BINDING.AUTH_STATUS_ID = OBJ_LG_USER_AD_BINDING_MAP.AUTH_STATUS_ID;
            OBJ_LG_USER_AD_BINDING.LAST_ACTION = OBJ_LG_USER_AD_BINDING_MAP.LAST_ACTION;
            OBJ_LG_USER_AD_BINDING.LAST_UPDATE_DT = OBJ_LG_USER_AD_BINDING_MAP.LAST_UPDATE_DT;
            OBJ_LG_USER_AD_BINDING.MAKE_DT = OBJ_LG_USER_AD_BINDING_MAP.MAKE_DT;
            return OBJ_LG_USER_AD_BINDING;
        }

        public static LG_FNR_MODULE_MAP LG_FNR_MODULE_REVERSE_CON(LG_FNR_MODULE_MAP OBJ_LG_FNR_MODULE_MAP, LG_FNR_MODULE OBJ_LG_FNR_MODULE)
        {
            OBJ_LG_FNR_MODULE_MAP.APPLICATION_ID = OBJ_LG_FNR_MODULE.APPLICATION_ID;
            OBJ_LG_FNR_MODULE_MAP.SERVICE_ID = OBJ_LG_FNR_MODULE.SERVICE_ID;
            OBJ_LG_FNR_MODULE_MAP.MODULE_ID = OBJ_LG_FNR_MODULE.MODULE_ID;
            OBJ_LG_FNR_MODULE_MAP.MODULE_NM = OBJ_LG_FNR_MODULE.MODULE_NM;
            OBJ_LG_FNR_MODULE_MAP.MODULE_SH_NM = OBJ_LG_FNR_MODULE.MODULE_SH_NM;
            OBJ_LG_FNR_MODULE_MAP.AUTH_STATUS_ID = OBJ_LG_FNR_MODULE.AUTH_STATUS_ID;
            OBJ_LG_FNR_MODULE_MAP.LAST_ACTION = OBJ_LG_FNR_MODULE.LAST_ACTION;
            OBJ_LG_FNR_MODULE_MAP.LAST_UPDATE_DT = OBJ_LG_FNR_MODULE.LAST_UPDATE_DT;
            OBJ_LG_FNR_MODULE.MAKE_DT = System.DateTime.Now;
            return OBJ_LG_FNR_MODULE_MAP;
        }
        public static LG_FNR_MODULE LG_FNR_MODULE_CON(LG_FNR_MODULE OBJ_LG_FNR_MODULE, LG_FNR_MODULE_MAP OBJ_LG_FNR_MODULE_MAP)
        {
            OBJ_LG_FNR_MODULE.APPLICATION_ID = OBJ_LG_FNR_MODULE_MAP.APPLICATION_ID;
            OBJ_LG_FNR_MODULE.SERVICE_ID = OBJ_LG_FNR_MODULE_MAP.SERVICE_ID;
            OBJ_LG_FNR_MODULE.MODULE_ID = OBJ_LG_FNR_MODULE_MAP.MODULE_ID;
            OBJ_LG_FNR_MODULE.MODULE_NM= OBJ_LG_FNR_MODULE_MAP.MODULE_NM;
            OBJ_LG_FNR_MODULE.MODULE_SH_NM = OBJ_LG_FNR_MODULE_MAP.MODULE_SH_NM;
            OBJ_LG_FNR_MODULE.AUTH_STATUS_ID = OBJ_LG_FNR_MODULE_MAP.AUTH_STATUS_ID;
            OBJ_LG_FNR_MODULE.LAST_ACTION = OBJ_LG_FNR_MODULE_MAP.LAST_ACTION;
            OBJ_LG_FNR_MODULE.LAST_UPDATE_DT = OBJ_LG_FNR_MODULE_MAP.LAST_UPDATE_DT;
            //OBJ_LG_FNR_MODULE.MAKE_DT = OBJ_LG_FNR_MODULE_MAP.MAKE_DT;
            
            return OBJ_LG_FNR_MODULE;
        }

        public static LG_FNR_ROLE_MAP LG_FNR_ROLE_REVERSE_CON(LG_FNR_ROLE_MAP OBJ_LG_FNR_ROLE_MAP, LG_FNR_ROLE OBJ_LG_FNR_ROLE)
        {
            OBJ_LG_FNR_ROLE_MAP.ROLE_ID = OBJ_LG_FNR_ROLE.ROLE_ID;
            OBJ_LG_FNR_ROLE_MAP.ROLE_NAME = OBJ_LG_FNR_ROLE.ROLE_NAME;
            OBJ_LG_FNR_ROLE_MAP.ROLE_DESCRIP = OBJ_LG_FNR_ROLE.ROLE_DESCRIP;
            OBJ_LG_FNR_ROLE_MAP.IS_SYS_ADMIN = OBJ_LG_FNR_ROLE.IS_SYS_ADMIN;
            OBJ_LG_FNR_ROLE_MAP.AUTH_STATUS_ID = OBJ_LG_FNR_ROLE.AUTH_STATUS_ID;
            OBJ_LG_FNR_ROLE_MAP.LAST_ACTION = OBJ_LG_FNR_ROLE.LAST_ACTION;
            OBJ_LG_FNR_ROLE_MAP.LAST_UPDATE_DT = OBJ_LG_FNR_ROLE.LAST_UPDATE_DT;
            OBJ_LG_FNR_ROLE_MAP.MAKE_DT = OBJ_LG_FNR_ROLE.MAKE_DT;
            return OBJ_LG_FNR_ROLE_MAP;
        }
        public static LG_FNR_ROLE LG_FNR_ROLE_CON(LG_FNR_ROLE OBJ_LG_FNR_ROLE, LG_FNR_ROLE_MAP OBJ_LG_FNR_ROLE_MAP)
        {
            OBJ_LG_FNR_ROLE.ROLE_ID = OBJ_LG_FNR_ROLE_MAP.ROLE_ID;
            OBJ_LG_FNR_ROLE.ROLE_NAME = OBJ_LG_FNR_ROLE_MAP.ROLE_NAME;
            OBJ_LG_FNR_ROLE.ROLE_DESCRIP = OBJ_LG_FNR_ROLE_MAP.ROLE_DESCRIP;
            OBJ_LG_FNR_ROLE.AUTH_STATUS_ID = OBJ_LG_FNR_ROLE_MAP.AUTH_STATUS_ID;
            OBJ_LG_FNR_ROLE.LAST_ACTION = OBJ_LG_FNR_ROLE_MAP.LAST_ACTION;
            OBJ_LG_FNR_ROLE.LAST_UPDATE_DT = OBJ_LG_FNR_ROLE_MAP.LAST_UPDATE_DT;
            OBJ_LG_FNR_ROLE.MAKE_DT = OBJ_LG_FNR_ROLE_MAP.MAKE_DT;
            return OBJ_LG_FNR_ROLE;
        }


        public static LG_USER_SETUP_PROFILE_MAP LG_USER_SETUP_PROFILE_REVERSE_CON(LG_USER_SETUP_PROFILE_MAP OBJ_LG_USER_SETUP_PROFILE_MAP, LG_USER_SETUP_PROFILE OBJ_LG_USER_SETUP_PROFILE)
        {
            OBJ_LG_USER_SETUP_PROFILE_MAP.USER_ID = OBJ_LG_USER_SETUP_PROFILE.USER_ID;
            OBJ_LG_USER_SETUP_PROFILE_MAP.USER_CLASSIFICATION_ID = OBJ_LG_USER_SETUP_PROFILE.USER_CLASSIFICATION_ID;
            OBJ_LG_USER_SETUP_PROFILE_MAP.USER_NAME = OBJ_LG_USER_SETUP_PROFILE.USER_NM;
            OBJ_LG_USER_SETUP_PROFILE_MAP.USER_DESCRIPTION = OBJ_LG_USER_SETUP_PROFILE.USER_DESCRIP;
            OBJ_LG_USER_SETUP_PROFILE_MAP.USER_AREA_ID_VALUE = OBJ_LG_USER_SETUP_PROFILE.USER_AREA_ID_VALUE;
            OBJ_LG_USER_SETUP_PROFILE_MAP.BRANCH_ID = OBJ_LG_USER_SETUP_PROFILE.BRANCH_ID;
            OBJ_LG_USER_SETUP_PROFILE_MAP.ACC_NO = OBJ_LG_USER_SETUP_PROFILE.ACC_NO;
            OBJ_LG_USER_SETUP_PROFILE_MAP.FATHERS_NAME = OBJ_LG_USER_SETUP_PROFILE.FATHERS_NM;
            OBJ_LG_USER_SETUP_PROFILE_MAP.MOTHERS_NAME = OBJ_LG_USER_SETUP_PROFILE.MOTHERS_NM;
            OBJ_LG_USER_SETUP_PROFILE_MAP.DOB = OBJ_LG_USER_SETUP_PROFILE.DOB;
            OBJ_LG_USER_SETUP_PROFILE_MAP.MAIL_ADDRESS = OBJ_LG_USER_SETUP_PROFILE.MAIL_ADDRESS;
            OBJ_LG_USER_SETUP_PROFILE_MAP.MOB_NO = OBJ_LG_USER_SETUP_PROFILE.MOB_NO;
            OBJ_LG_USER_SETUP_PROFILE_MAP.AUTHENTICATION_ID = OBJ_LG_USER_SETUP_PROFILE.AUTHENTICATION_ID;
            OBJ_LG_USER_SETUP_PROFILE_MAP.TERMINAL_IP = OBJ_LG_USER_SETUP_PROFILE.TERMINAL_IP;
            OBJ_LG_USER_SETUP_PROFILE_MAP.WORKING_HOUR = OBJ_LG_USER_SETUP_PROFILE.WORKING_HOUR;
            OBJ_LG_USER_SETUP_PROFILE_MAP.START_TIME = OBJ_LG_USER_SETUP_PROFILE.START_TIME;
            OBJ_LG_USER_SETUP_PROFILE_MAP.END_TIME = OBJ_LG_USER_SETUP_PROFILE.END_TIME;
            OBJ_LG_USER_SETUP_PROFILE_MAP.PASSWORD = OBJ_LG_USER_SETUP_PROFILE.PASSWORD;
            OBJ_LG_USER_SETUP_PROFILE_MAP.AUTH_STATUS_ID = OBJ_LG_USER_SETUP_PROFILE.AUTH_STATUS_ID;
            OBJ_LG_USER_SETUP_PROFILE_MAP.LAST_ACTION = OBJ_LG_USER_SETUP_PROFILE.LAST_ACTION;
            OBJ_LG_USER_SETUP_PROFILE_MAP.MAKE_DT = OBJ_LG_USER_SETUP_PROFILE.MAKE_DT;
            OBJ_LG_USER_SETUP_PROFILE_MAP.FAILED_LOGIN_ATTEMPT = OBJ_LG_USER_SETUP_PROFILE.FAILED_LOGIN_ATTEMPT;
            OBJ_LG_USER_SETUP_PROFILE_MAP.USER_ID = OBJ_LG_USER_SETUP_PROFILE.USER_ID;
            OBJ_LG_USER_SETUP_PROFILE_MAP.ACTIVE_FLAG_MULTI_LOGIN = OBJ_LG_USER_SETUP_PROFILE.ACTIVE_FLAG_MULTI_LOGIN;
            OBJ_LG_USER_SETUP_PROFILE_MAP.FIRST_LOGIN_FLAG = OBJ_LG_USER_SETUP_PROFILE.FIRST_LOGIN_FLAG;
            OBJ_LG_USER_SETUP_PROFILE_MAP.USER_ID_LOCK_WRNG_ATM = OBJ_LG_USER_SETUP_PROFILE.USER_ID_LOCK_WRNG_ATM;
            OBJ_LG_USER_SETUP_PROFILE_MAP.ACTIVE_FLAG_INACTV_USER = OBJ_LG_USER_SETUP_PROFILE.ACTIVE_FLAG_INACTV_USER;

            return OBJ_LG_USER_SETUP_PROFILE_MAP;
        }
        public static LG_USER_SETUP_PROFILE LG_USER_SETUP_PROFILE_CON(LG_USER_SETUP_PROFILE OBJ_LG_USER_SETUP_PROFILE, LG_USER_SETUP_PROFILE_MAP OBJ_LG_USER_SETUP_PROFILE_MAP)
        {

            OBJ_LG_USER_SETUP_PROFILE.USER_ID = OBJ_LG_USER_SETUP_PROFILE_MAP.USER_ID ;
            OBJ_LG_USER_SETUP_PROFILE.USER_CLASSIFICATION_ID = OBJ_LG_USER_SETUP_PROFILE_MAP.USER_CLASSIFICATION_ID;
            OBJ_LG_USER_SETUP_PROFILE.USER_NM = OBJ_LG_USER_SETUP_PROFILE_MAP.USER_NAME ;
            OBJ_LG_USER_SETUP_PROFILE.USER_DESCRIP = OBJ_LG_USER_SETUP_PROFILE_MAP.USER_DESCRIPTION;
            OBJ_LG_USER_SETUP_PROFILE.USER_AREA_ID_VALUE = OBJ_LG_USER_SETUP_PROFILE_MAP.USER_AREA_ID_VALUE;
            OBJ_LG_USER_SETUP_PROFILE.BRANCH_ID = OBJ_LG_USER_SETUP_PROFILE_MAP.BRANCH_ID;
            OBJ_LG_USER_SETUP_PROFILE.ACC_NO = OBJ_LG_USER_SETUP_PROFILE_MAP.ACC_NO;
            OBJ_LG_USER_SETUP_PROFILE.FATHERS_NM = OBJ_LG_USER_SETUP_PROFILE_MAP.FATHERS_NAME;
            OBJ_LG_USER_SETUP_PROFILE.MOTHERS_NM = OBJ_LG_USER_SETUP_PROFILE_MAP.MOTHERS_NAME;
            OBJ_LG_USER_SETUP_PROFILE.DOB = OBJ_LG_USER_SETUP_PROFILE_MAP.DOB;
            OBJ_LG_USER_SETUP_PROFILE.MAIL_ADDRESS = OBJ_LG_USER_SETUP_PROFILE_MAP.MAIL_ADDRESS;
            OBJ_LG_USER_SETUP_PROFILE.MOB_NO = OBJ_LG_USER_SETUP_PROFILE_MAP.MOB_NO;
            OBJ_LG_USER_SETUP_PROFILE.AUTHENTICATION_ID = OBJ_LG_USER_SETUP_PROFILE_MAP.AUTHENTICATION_ID;
            OBJ_LG_USER_SETUP_PROFILE.TERMINAL_IP = OBJ_LG_USER_SETUP_PROFILE_MAP.TERMINAL_IP;
            OBJ_LG_USER_SETUP_PROFILE.WORKING_HOUR = OBJ_LG_USER_SETUP_PROFILE_MAP.WORKING_HOUR;
            OBJ_LG_USER_SETUP_PROFILE.START_TIME = OBJ_LG_USER_SETUP_PROFILE_MAP.START_TIME;
            OBJ_LG_USER_SETUP_PROFILE.END_TIME = OBJ_LG_USER_SETUP_PROFILE_MAP.END_TIME;
            OBJ_LG_USER_SETUP_PROFILE.PASSWORD = OBJ_LG_USER_SETUP_PROFILE_MAP.PASSWORD;
            OBJ_LG_USER_SETUP_PROFILE.AUTH_STATUS_ID = OBJ_LG_USER_SETUP_PROFILE_MAP.AUTH_STATUS_ID;
            OBJ_LG_USER_SETUP_PROFILE.LAST_ACTION = OBJ_LG_USER_SETUP_PROFILE_MAP.LAST_ACTION;
            OBJ_LG_USER_SETUP_PROFILE.MAKE_DT = OBJ_LG_USER_SETUP_PROFILE_MAP.MAKE_DT;
            OBJ_LG_USER_SETUP_PROFILE.FAILED_LOGIN_ATTEMPT = OBJ_LG_USER_SETUP_PROFILE_MAP.FAILED_LOGIN_ATTEMPT;
            OBJ_LG_USER_SETUP_PROFILE.USER_ID = OBJ_LG_USER_SETUP_PROFILE_MAP.USER_ID;
            OBJ_LG_USER_SETUP_PROFILE.ACTIVE_FLAG_MULTI_LOGIN = OBJ_LG_USER_SETUP_PROFILE_MAP.ACTIVE_FLAG_MULTI_LOGIN;
            OBJ_LG_USER_SETUP_PROFILE.FIRST_LOGIN_FLAG = OBJ_LG_USER_SETUP_PROFILE_MAP.FIRST_LOGIN_FLAG;
            OBJ_LG_USER_SETUP_PROFILE.USER_ID_LOCK_WRNG_ATM = OBJ_LG_USER_SETUP_PROFILE_MAP.USER_ID_LOCK_WRNG_ATM;


            return OBJ_LG_USER_SETUP_PROFILE;
        }


        public static LG_CRD_PASSWORD_POLICY_MAP LG_CRD_PASSWORD_POLICY_REVERSE_CON(LG_CRD_PASSWORD_POLICY_MAP OBJ_LG_CRD_PASSWORD_POLICY_MAP, LG_CRD_PASSWORD_POLICY OBJ_LG_CRD_PASSWORD_POLICY)
        {

            OBJ_LG_CRD_PASSWORD_POLICY_MAP.APPLICATION_ID = OBJ_LG_CRD_PASSWORD_POLICY.APPLICATION_ID;
            OBJ_LG_CRD_PASSWORD_POLICY_MAP.PASS_MAX_LENGTH = OBJ_LG_CRD_PASSWORD_POLICY.PASS_MAX_LENGTH;
            OBJ_LG_CRD_PASSWORD_POLICY_MAP.PASS_MIN_LENGTH = OBJ_LG_CRD_PASSWORD_POLICY.PASS_MIN_LENGTH;
            OBJ_LG_CRD_PASSWORD_POLICY_MAP.NUMERIC_CHAR_MIN = OBJ_LG_CRD_PASSWORD_POLICY.NUMERIC_CHAR_MIN;
            OBJ_LG_CRD_PASSWORD_POLICY_MAP.PASS_HIS_PERIOD = OBJ_LG_CRD_PASSWORD_POLICY.PASS_HIS_PERIOD;
            OBJ_LG_CRD_PASSWORD_POLICY_MAP.MIN_CAPS_LETTER = OBJ_LG_CRD_PASSWORD_POLICY.MIN_CAPS_LETTER;
            OBJ_LG_CRD_PASSWORD_POLICY_MAP.MIN_SMALL_LETTER = OBJ_LG_CRD_PASSWORD_POLICY.MIN_SMALL_LETTER;
            OBJ_LG_CRD_PASSWORD_POLICY_MAP.MIN_NUMERIC_CHAR = OBJ_LG_CRD_PASSWORD_POLICY.MIN_NUMERIC_CHAR;
            OBJ_LG_CRD_PASSWORD_POLICY_MAP.MIN_CONS_USE_PASS = OBJ_LG_CRD_PASSWORD_POLICY.MIN_CONS_USE_PASS ;
            OBJ_LG_CRD_PASSWORD_POLICY_MAP.PASS_REPEAT = OBJ_LG_CRD_PASSWORD_POLICY.PASS_REPEAT;
            OBJ_LG_CRD_PASSWORD_POLICY_MAP.PASS_CHANGED_EXPIRY = OBJ_LG_CRD_PASSWORD_POLICY.PASS_CHANGED_EXPIRY;
            OBJ_LG_CRD_PASSWORD_POLICY_MAP.PASS_REUSE_MAX = OBJ_LG_CRD_PASSWORD_POLICY.PASS_REUSE_MAX;
            OBJ_LG_CRD_PASSWORD_POLICY_MAP.FAILED_LOGIN_ATTEMT = OBJ_LG_CRD_PASSWORD_POLICY.FAILED_LOGIN_ATTEMT;
            OBJ_LG_CRD_PASSWORD_POLICY_MAP.PASS_EXP_PERIOD = OBJ_LG_CRD_PASSWORD_POLICY.PASS_EXP_ALERT;
            OBJ_LG_CRD_PASSWORD_POLICY_MAP.PASS_EXP_ALERT = OBJ_LG_CRD_PASSWORD_POLICY.PASS_EXP_ALERT;
            OBJ_LG_CRD_PASSWORD_POLICY_MAP.PASS_AUTO_CREATION =OBJ_LG_CRD_PASSWORD_POLICY.PASS_AUTO_CREATION;
            OBJ_LG_CRD_PASSWORD_POLICY_MAP.PASS_CHANGE_BY_ADMIN = OBJ_LG_CRD_PASSWORD_POLICY.PASS_CHANGE_BY_ADMIN;
            OBJ_LG_CRD_PASSWORD_POLICY_MAP.PASS_CHANG_AT_FIRST_LOGIN = OBJ_LG_CRD_PASSWORD_POLICY.PASS_CHANGE_AT_FIRST_LOGIN;
   
            return OBJ_LG_CRD_PASSWORD_POLICY_MAP;
        }
        public static LG_CRD_PASSWORD_POLICY_MAP LG_CRD_PASSWORD_POLICY_CON(LG_CRD_PASSWORD_POLICY OBJ_LG_CRD_PASSWORD_POLICY, LG_CRD_PASSWORD_POLICY_MAP OBJ_LG_CRD_PASSWORD_POLICY_MAP)
        {

            OBJ_LG_CRD_PASSWORD_POLICY.APPLICATION_ID = OBJ_LG_CRD_PASSWORD_POLICY_MAP.APPLICATION_ID;
            OBJ_LG_CRD_PASSWORD_POLICY.PASS_MAX_LENGTH = OBJ_LG_CRD_PASSWORD_POLICY_MAP.PASS_MAX_LENGTH;
            OBJ_LG_CRD_PASSWORD_POLICY.PASS_MIN_LENGTH = OBJ_LG_CRD_PASSWORD_POLICY_MAP.PASS_MIN_LENGTH;
            OBJ_LG_CRD_PASSWORD_POLICY.NUMERIC_CHAR_MIN = OBJ_LG_CRD_PASSWORD_POLICY_MAP.NUMERIC_CHAR_MIN;
            OBJ_LG_CRD_PASSWORD_POLICY.PASS_HIS_PERIOD = OBJ_LG_CRD_PASSWORD_POLICY_MAP.PASS_HIS_PERIOD;
            OBJ_LG_CRD_PASSWORD_POLICY.MIN_CAPS_LETTER = OBJ_LG_CRD_PASSWORD_POLICY_MAP.MIN_CAPS_LETTER;
            OBJ_LG_CRD_PASSWORD_POLICY.MIN_SMALL_LETTER = OBJ_LG_CRD_PASSWORD_POLICY_MAP.MIN_SMALL_LETTER;
            OBJ_LG_CRD_PASSWORD_POLICY.MIN_NUMERIC_CHAR = OBJ_LG_CRD_PASSWORD_POLICY_MAP.MIN_NUMERIC_CHAR;
            OBJ_LG_CRD_PASSWORD_POLICY.MIN_CONS_USE_PASS = OBJ_LG_CRD_PASSWORD_POLICY_MAP.MIN_CONS_USE_PASS;
            OBJ_LG_CRD_PASSWORD_POLICY.PASS_REPEAT = OBJ_LG_CRD_PASSWORD_POLICY_MAP.PASS_REPEAT;
            OBJ_LG_CRD_PASSWORD_POLICY.PASS_CHANGED_EXPIRY = Convert.ToInt16( OBJ_LG_CRD_PASSWORD_POLICY_MAP.PASS_CHANGED_EXPIRY);
            OBJ_LG_CRD_PASSWORD_POLICY.PASS_REUSE_MAX = OBJ_LG_CRD_PASSWORD_POLICY_MAP.PASS_REUSE_MAX;
            OBJ_LG_CRD_PASSWORD_POLICY.FAILED_LOGIN_ATTEMT = OBJ_LG_CRD_PASSWORD_POLICY_MAP.FAILED_LOGIN_ATTEMT;
            OBJ_LG_CRD_PASSWORD_POLICY.PASS_EXP_PERIOD = OBJ_LG_CRD_PASSWORD_POLICY_MAP.PASS_EXP_ALERT;
            OBJ_LG_CRD_PASSWORD_POLICY.PASS_EXP_ALERT = OBJ_LG_CRD_PASSWORD_POLICY_MAP.PASS_EXP_ALERT;
            OBJ_LG_CRD_PASSWORD_POLICY.PASS_AUTO_CREATION = OBJ_LG_CRD_PASSWORD_POLICY_MAP.PASS_AUTO_CREATION;
            OBJ_LG_CRD_PASSWORD_POLICY.PASS_CHANGE_BY_ADMIN = OBJ_LG_CRD_PASSWORD_POLICY_MAP.PASS_CHANGE_BY_ADMIN;
            OBJ_LG_CRD_PASSWORD_POLICY.PASS_CHANGE_AT_FIRST_LOGIN = OBJ_LG_CRD_PASSWORD_POLICY_MAP.PASS_CHANG_AT_FIRST_LOGIN;


            return OBJ_LG_CRD_PASSWORD_POLICY_MAP;
        }



        public static LG_2FA_OTP_CONFIG_MAP LG_2FA_OTP_CONFIG_MAP_REVERSE_CON(LG_2FA_OTP_CONFIG_MAP OBJ_LG_2FA_OTP_CONFIG_MAP, LG_2FA_OTP_CONFIG OBJ_LG_2FA_OTP_CONFIG)
        {

            OBJ_LG_2FA_OTP_CONFIG_MAP.APPLICATION_ID = OBJ_LG_2FA_OTP_CONFIG.APPLICATION_ID;
            OBJ_LG_2FA_OTP_CONFIG_MAP.MAIL_FLAG = OBJ_LG_2FA_OTP_CONFIG.MAIL_FLAG;
            OBJ_LG_2FA_OTP_CONFIG_MAP.SMS_FLAG = OBJ_LG_2FA_OTP_CONFIG.SMS_FLAG;
            OBJ_LG_2FA_OTP_CONFIG_MAP.VALIDITY_PERIOD = OBJ_LG_2FA_OTP_CONFIG.VALIDITY_PERIOD;
            OBJ_LG_2FA_OTP_CONFIG_MAP.NO_OF_OTP_DIGIT = OBJ_LG_2FA_OTP_CONFIG.NO_OF_OTP_DIGIT;
            OBJ_LG_2FA_OTP_CONFIG_MAP.OTP_FORMAT_ID = OBJ_LG_2FA_OTP_CONFIG.OTP_FORMAT_ID;
            OBJ_LG_2FA_OTP_CONFIG_MAP.MAKE_DT = OBJ_LG_2FA_OTP_CONFIG.MAKE_DT;
            OBJ_LG_2FA_OTP_CONFIG_MAP.OTP_ID = OBJ_LG_2FA_OTP_CONFIG.OTP_ID;
            OBJ_LG_2FA_OTP_CONFIG_MAP.AUTH_STATUS_ID = OBJ_LG_2FA_OTP_CONFIG.AUTH_STATUS_ID;
            OBJ_LG_2FA_OTP_CONFIG_MAP.LAST_ACTION = OBJ_LG_2FA_OTP_CONFIG.LAST_ACTION;
            OBJ_LG_2FA_OTP_CONFIG_MAP.LAST_UPDATE_DT = OBJ_LG_2FA_OTP_CONFIG.LAST_UPDATE_DT;

            return OBJ_LG_2FA_OTP_CONFIG_MAP;
        }
        public static LG_2FA_OTP_CONFIG_MAP LG_2FA_OTP_CONFIG_MAP_CON(LG_2FA_OTP_CONFIG OBJ_LG_2FA_OTP_CONFIG, LG_2FA_OTP_CONFIG_MAP OBJ_LG_2FA_OTP_CONFIG_MAP)
        {

            OBJ_LG_2FA_OTP_CONFIG.APPLICATION_ID = OBJ_LG_2FA_OTP_CONFIG_MAP.APPLICATION_ID;
            OBJ_LG_2FA_OTP_CONFIG.MAIL_FLAG = (short) OBJ_LG_2FA_OTP_CONFIG_MAP.MAIL_FLAG;
            OBJ_LG_2FA_OTP_CONFIG.SMS_FLAG = (short) OBJ_LG_2FA_OTP_CONFIG_MAP.SMS_FLAG;
            OBJ_LG_2FA_OTP_CONFIG.VALIDITY_PERIOD = OBJ_LG_2FA_OTP_CONFIG_MAP.VALIDITY_PERIOD;
            OBJ_LG_2FA_OTP_CONFIG.NO_OF_OTP_DIGIT = OBJ_LG_2FA_OTP_CONFIG_MAP.NO_OF_OTP_DIGIT;
            OBJ_LG_2FA_OTP_CONFIG.OTP_FORMAT_ID = OBJ_LG_2FA_OTP_CONFIG_MAP.OTP_FORMAT_ID;
            OBJ_LG_2FA_OTP_CONFIG.MAKE_DT = OBJ_LG_2FA_OTP_CONFIG_MAP.MAKE_DT;
            OBJ_LG_2FA_OTP_CONFIG.OTP_ID = OBJ_LG_2FA_OTP_CONFIG_MAP.OTP_ID;
            OBJ_LG_2FA_OTP_CONFIG.AUTH_STATUS_ID = OBJ_LG_2FA_OTP_CONFIG_MAP.AUTH_STATUS_ID;
            OBJ_LG_2FA_OTP_CONFIG.LAST_ACTION = OBJ_LG_2FA_OTP_CONFIG_MAP.LAST_ACTION;
            OBJ_LG_2FA_OTP_CONFIG.LAST_UPDATE_DT = OBJ_LG_2FA_OTP_CONFIG_MAP.LAST_UPDATE_DT;


            return OBJ_LG_2FA_OTP_CONFIG_MAP;
        }




        //public DBModelEntities(string connstring) : base(connstring)
        //        {
        //        }
    }
}








