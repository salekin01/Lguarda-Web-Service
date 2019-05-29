using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Model.EDMX;
using Model.EntityModel.Common;
using Model.Mapper;
using System.Configuration;
using System.Transactions;

namespace Model.EntityModel.LGModel
{
    [DataContract]
    public class LG_FNR_FUNCTION_MAP
    {
        #region Properties

        [DataMember]
        public string FUNCTION_ID { get; set; }

        [DataMember]
        public string FUNCTION_NM { get; set; }

        [DataMember]
        public string SERVICE_ID { get; set; }

        [DataMember]
        public string MODULE_ID { get; set; }

        [DataMember]
        public short MAINT_CRT_FLAG { get; set; }

        [DataMember]
        public short MAINT_EDT_FLAG { get; set; }

        [DataMember]
        public short MAINT_DEL_FLAG { get; set; }

        [DataMember]
        public short MAINT_DTL_FLAG { get; set; }

        [DataMember]
        public short MAINT_INDX_FLAG { get; set; }

        [DataMember]
        public short? MAINT_AUTH_FLAG { get; set; }

        [DataMember]
        public short MAINT_OTP_FLAG { get; set; }

        [DataMember]
        public short? MAINT_BIO_FLAG { get; set; }

        [DataMember]
        public short MAINT_2FA_FLAG { get; set; }

        [DataMember]
        public short MAINT_2FA_HARD_FLAG { get; set; }

        [DataMember]
        public short MAINT_2FA_SOFT_FLAG { get; set; }

        [DataMember]
        public short REPORT_VIEW_FLAG { get; set; }

        [DataMember]
        public short REPORT_PRINT_FLAG { get; set; }

        [DataMember]
        public short REPORT_GEN_FLAG { get; set; }

        [DataMember]
        public string MAKE_BY { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public DateTime MAKE_DT { get; set; }

        [DataMember]
        public string ITEM_TYPE { get; set; }

        [DataMember]
        public string AUTH_STATUS_ID { get; set; }

        [DataMember]
        public string LAST_ACTION { get; set; }

        [DataMember]
        public DateTime? LAST_UPDATE_DT { get; set; }

        [DataMember]
        public int? AUTH_LEVEL { get; set; }

        [DataMember]
        public short? PROCESS_FLAG { get; set; }

        [DataMember]
        public short? ENABLED_FLAG { get; set; }

        [DataMember]
        public short? HO_FUNCTION_FLAG { get; set; }

        [DataMember]
        public string FAST_PATH_NO { get; set; }

        [DataMember]
        public string TARGET_PATH { get; set; }

        [DataMember]
        public string DB_ROLE_NAME { get; set; }

        [DataMember]
        public decimal? MENU_ID { get; set; }

        [DataMember]
        public string MENU_NM { get; set; }

        [DataMember]
        public int? MENU_LEVEL { get; set; }

        [DataMember]
        public decimal? PARENT_MENU_ID { get; set; }

        [DataMember]
        public short? FUNCTION_ASSIGN_FLAG { get; set; }

        [DataMember]
        public string APPLICATION_ID { get; set; }

        [DataMember]
        public string APPLICATION_NAME { get; set; }

        [DataMember]
        public string MODULE_NM { get; set; }

        [DataMember]
        public string SERVICE_NM { get; set; }

        [DataMember]
        public string ERROR { get; set; }

        [DataMember]
        public IEnumerable<SelectListItem> APPLICATION_LIST_FOR_DD { get; set; }

        [DataMember]
        public IEnumerable<SelectListItem> SERVICE_LIST_FOR_DD { get; set; }

        [DataMember]
        public IEnumerable<SelectListItem> MODULE_LIST_FOR_DD { get; set; }

        [DataMember]
        public IEnumerable<SelectListItem> FUNCTION_GROUP_LIST_FOR_DD { get; set; }

        [DataMember]
        public IEnumerable<SelectListItem> ITEM_TYPE_LIST_FOR_DD { get; set; }

        //additional parameter for Boolen
        [DataMember]
        public bool MAINT_CRT_FLAG_B { get; set; }

        [DataMember]
        public bool MAINT_EDT_FLAG_B { get; set; }

        [DataMember]
        public bool MAINT_DEL_FLAG_B { get; set; }

        [DataMember]
        public bool MAINT_DTL_FLAG_B { get; set; }

        [DataMember]
        public bool MAINT_INDX_FLAG_B { get; set; }

        [DataMember]
        public bool MAINT_AUTH_FLAG_B { get; set; }

        [DataMember]
        public bool MAINT_OTP_FLAG_B { get; set; }

        [DataMember]
        public bool MAINT_2FA_FLAG_B { get; set; }

        [DataMember]
        public bool MAINT_2FA_HARD_FLAG_B { get; set; }

        [DataMember]
        public bool MAINT_2FA_SOFT_FLAG_B { get; set; }

        [DataMember]
        public bool REPORT_VIEW_FLAG_B { get; set; }

        [DataMember]
        public bool REPORT_PRINT_FLAG_B { get; set; }

        [DataMember]
        public bool REPORT_GEN_FLAG_B { get; set; }

        [DataMember]
        public bool MAINT_BIO_FLAG_B { get; set; }

        [DataMember]
        public bool PROCESS_FLAG_B { get; set; } 

        [DataMember]
        public bool ENABLED_FLAG_B { get; set; }

        [DataMember]
        public bool HO_FUNCTION_FLAG_B { get; set; }

        [DataMember]
        public List<LG_FNR_FUNCTION_MAP> GetPermittedFunctionsList { get; set; }

        #endregion Properties

        #region Events

        public static string FUNC_ID = "010101002";

        #region Add New

        public static string AddFunction(string pFUNCTION_NM, string pAPPLICATION_ID, string pSERVICE_ID, string pMODULE_ID, string pITEM_TYPE, string pMAINT_CRT_FLAG_B, string pMAINT_EDT_FLAG_B, string pMAINT_DEL_FLAG_B, string pMAINT_DTL_FLAG_B, string pMAINT_INDX_FLAG_B, string pMAINT_OTP_FLAG_B, string pMAINT_2FA_FLAG_B, string pMAINT_2FA_HARD_FLAG_B, string pMAINT_2FA_SOFT_FLAG_B, string pREPORT_VIEW_FLAG_B, string pREPORT_PRINT_FLAG_B, string pREPORT_GEN_FLAG_B, string pAUTH_LEVEL, string pMAINT_AUTH_FLAG_B, string psession_user, string pMAINT_BIO_FLAG_B, string pPROCESS_FLAG_B, string pHO_FUNCTION_FLAG_B, string pFAST_PATH_NO, string pTARGET_PATH, string pDB_ROLE_NAME, string pENABLED_FLAG_B)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
                LG_FNR_FUNCTION OBJ_LG_FNR_FUNCTION = new LG_FNR_FUNCTION();
                LG_FNR_FUNCTION_MAP OBJ_LG_FNR_FUNCTION_MAP = new LG_FNR_FUNCTION_MAP();
                string result = string.Empty;
                try
                {
                    string format = OutgoingResponseFormat.GetFormat();
                    OutgoingResponseFormat.SetResponseFormat(format);

                    OBJ_LG_FNR_FUNCTION = Obj_DBModelEntities.LG_FNR_FUNCTION
                                         .Where(f => f.FUNCTION_NM == pFUNCTION_NM &&
                                                     f.APPLICATION_ID == pAPPLICATION_ID).SingleOrDefault();
                    if (OBJ_LG_FNR_FUNCTION != null)
                    {
                        return "Function name already exists";
                    }

                    //int function_id = Convert.ToInt32(Obj_DBModelEntities.LG_FNR_FUNCTION
                    //                 .Max(x => x.FUNCTION_ID)) + 1;

                    int? app_type_id = Obj_DBModelEntities.LG_FNR_APPLICATION.Where(a => a.APPLICATION_ID == pAPPLICATION_ID).Select(a => a.APP_TYPE_ID).SingleOrDefault();

                    string prefix = string.Empty;
                    //if(app_type_id == 2)
                    //{
                    //    prefix = pSERVICE_ID + pMODULE_ID;
                    //}
                    //else
                    //{
                    prefix = pAPPLICATION_ID + pSERVICE_ID + pMODULE_ID;
                    //}

                    int function_id1 = (Obj_DBModelEntities.LG_FNR_FUNCTION
                                      .Where(i => i.FUNCTION_ID.Substring(0, 6).Contains(prefix)) //if prefix not found function_id = 1(for the first time it will be 1), if prefix found function_id = 10101001(for ex.) <- 8 bit
                                      .Select(i => i.FUNCTION_ID).Cast<int?>().Max() ?? 0) + 1;

                    string function_id = string.Empty;
                    if (function_id1.ToString().Length < 8) //For the first time of a new prefix it will true
                    {
                        function_id = (prefix + "001");
                    }
                    else
                        function_id = Convert.ToString(function_id1).PadLeft(9, '0');

                    OBJ_LG_FNR_FUNCTION = new LG_FNR_FUNCTION();
                    OBJ_LG_FNR_FUNCTION_MAP = BooleanConversion.LG_FNR_FUNCTION_MAP_BOOL_TO_INT(pMAINT_CRT_FLAG_B, pMAINT_EDT_FLAG_B, pMAINT_DEL_FLAG_B, pMAINT_DTL_FLAG_B, pMAINT_INDX_FLAG_B, pMAINT_OTP_FLAG_B, pMAINT_2FA_FLAG_B, pMAINT_2FA_HARD_FLAG_B, pMAINT_2FA_SOFT_FLAG_B, pREPORT_VIEW_FLAG_B, pREPORT_PRINT_FLAG_B, pREPORT_GEN_FLAG_B, pMAINT_AUTH_FLAG_B, pMAINT_BIO_FLAG_B, pPROCESS_FLAG_B, pHO_FUNCTION_FLAG_B, pENABLED_FLAG_B);
                    Class_Conversion.LG_FNR_FUNCTION_CON(OBJ_LG_FNR_FUNCTION, OBJ_LG_FNR_FUNCTION_MAP);

                    OBJ_LG_FNR_FUNCTION.FUNCTION_ID = function_id;
                    OBJ_LG_FNR_FUNCTION.FUNCTION_NM = pFUNCTION_NM;
                    OBJ_LG_FNR_FUNCTION.APPLICATION_ID = pAPPLICATION_ID;
                    OBJ_LG_FNR_FUNCTION.SERVICE_ID = pSERVICE_ID;
                    OBJ_LG_FNR_FUNCTION.MODULE_ID = pMODULE_ID;
                    OBJ_LG_FNR_FUNCTION.ITEM_TYPE = pITEM_TYPE;
                    if (!string.IsNullOrEmpty(pAUTH_LEVEL))
                    {
                        OBJ_LG_FNR_FUNCTION.AUTH_LEVEL = Convert.ToInt16(pAUTH_LEVEL);
                    }
                    else
                        OBJ_LG_FNR_FUNCTION.AUTH_LEVEL = 0;
                    /*
                    if (pMAINT_AUTH_FLAG_B.ToLower() == "true")
                    {
                        OBJ_LG_FNR_FUNCTION.AUTH_STATUS_ID = "U";
                    }
                    else
                        OBJ_LG_FNR_FUNCTION.AUTH_STATUS_ID = "A"; */
                    OBJ_LG_FNR_FUNCTION.AUTH_STATUS_ID = "U";
                    OBJ_LG_FNR_FUNCTION.LAST_ACTION = "ADD";
                    OBJ_LG_FNR_FUNCTION.MAKE_DT = System.DateTime.Now;

                    OBJ_LG_FNR_FUNCTION.FAST_PATH_NO = pFAST_PATH_NO;
                    OBJ_LG_FNR_FUNCTION.TARGET_PATH = string.IsNullOrWhiteSpace(pTARGET_PATH) ? pTARGET_PATH : CryptorEngine.ConvertHexToString(pTARGET_PATH, System.Text.Encoding.Unicode);
                    OBJ_LG_FNR_FUNCTION.DB_ROLE_NAME = pDB_ROLE_NAME;

                    Obj_DBModelEntities.LG_FNR_FUNCTION.Add(OBJ_LG_FNR_FUNCTION);
                    Obj_DBModelEntities.SaveChanges();
                    Class_Conversion.LG_FNR_FUNCTION_REVERSE_CON(OBJ_LG_FNR_FUNCTION_MAP, OBJ_LG_FNR_FUNCTION);

                    #region Add in "PermissionDetails" Table

                    int permission_id = (Obj_DBModelEntities.LG_FNR_ROLE_PERMISSION_DETAILS
                                        .Select(i => i.PERMISSION_ID).Cast<int?>().Max() ?? 0);

                    if (pMAINT_CRT_FLAG_B.ToLower() == "true")
                    {
                        permission_id++;
                        string permission_details = pFUNCTION_NM + "-" + "Create";
                        Add_PermissionDetail(pMAINT_AUTH_FLAG_B, function_id, permission_id, permission_details);
                    }
                    if (pMAINT_EDT_FLAG_B.ToLower() == "true")
                    {
                        permission_id++;
                        string permission_details = pFUNCTION_NM + "-" + "Edit";
                        Add_PermissionDetail(pMAINT_AUTH_FLAG_B, function_id, permission_id, permission_details);
                    }
                    if (pMAINT_DEL_FLAG_B.ToLower() == "true")
                    {
                        permission_id++;
                        string permission_details = pFUNCTION_NM + "-" + "Delete";
                        Add_PermissionDetail(pMAINT_AUTH_FLAG_B, function_id, permission_id, permission_details);
                    }
                    if (pMAINT_DTL_FLAG_B.ToLower() == "true")
                    {
                        permission_id++;
                        string permission_details = pFUNCTION_NM + "-" + "Details";
                        Add_PermissionDetail(pMAINT_AUTH_FLAG_B, function_id, permission_id, permission_details);
                    }
                    if (pMAINT_INDX_FLAG_B.ToLower() == "true")
                    {
                        permission_id++;
                        string permission_details = pFUNCTION_NM + "-" + "Index";
                        Add_PermissionDetail(pMAINT_AUTH_FLAG_B, function_id, permission_id, permission_details);
                    }

                    #endregion Add in "PermissionDetails" Table

                    #region Auth log

                    LG_AA_NFT_AUTH_LOG_MAP OBJ_LG_AA_NFT_AUTH_LOG_MAP = new LG_AA_NFT_AUTH_LOG_MAP();
                    FUNC_ID = OBJ_LG_AA_NFT_AUTH_LOG_MAP.FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "Function").Select(x => x.FUNCTION_ID).SingleOrDefault();
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_NAME = "LG_FNR_FUNCTION";
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_NM = "FUNCTION_ID";
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_VAL = OBJ_LG_FNR_FUNCTION.FUNCTION_ID;
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
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.MAKE_BY = psession_user;
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.MAKE_DT = DateTime.Now;
                    LG_AA_NFT_AUTH_LOG_MAP.AddNftAuthLog(OBJ_LG_FNR_FUNCTION_MAP, OBJ_LG_AA_NFT_AUTH_LOG_MAP);

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
                            result = "Can't Add Function(Db) ";
                        }
                    }
                    LG_ERROR_LOG_MAP.Add_Error_Log(FUNC_ID, dbEx.Source, "ERR_APP_TYPE", "AddFunction",
                                              "0000000000", dbEx.Message, inner, dbEx.StackTrace, psession_user, dateTime);
                    return result;
                }
                catch (Exception ex)
                {
                    ts.Dispose();
                    string dateTime = Convert.ToString(System.DateTime.Now);
                    string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);
                    LG_ERROR_LOG_MAP.Add_Error_Log(FUNC_ID, ex.Source, "ERR_APP_TYPE", "AddFunction",
                                              "0000000000", ex.Message, inner4, ex.StackTrace, psession_user, dateTime);
                    result = "Can't Add Function.";
                    return result;
                }
            }
        }

        #endregion Add New

        #region Update

        public static string UpdateFunction(string pFUNCTION_ID, string pFUNCTION_NM, string pAPPLICATION_ID, string pSERVICE_ID, string pMODULE_ID, string pITEM_TYPE, string pMAINT_CRT_FLAG_B, string pMAINT_EDT_FLAG_B, string pMAINT_DEL_FLAG_B, string pMAINT_DTL_FLAG_B, string pMAINT_INDX_FLAG_B, string pMAINT_OTP_FLAG_B, string pMAINT_2FA_FLAG_B, string pMAINT_2FA_HARD_FLAG_B, string pMAINT_2FA_SOFT_FLAG_B, string pREPORT_VIEW_FLAG_B, string pREPORT_PRINT_FLAG_B, string pREPORT_GEN_FLAG_B, string pAUTH_LEVEL, string pMAINT_AUTH_FLAG_B, string psession_user, string pMAINT_BIO_FLAG_B, string pPROCESS_FLAG_B, string pHO_FUNCTION_FLAG_B, string pFAST_PATH_NO, string pTARGET_PATH, string pDB_ROLE_NAME, string pENABLED_FLAG_B)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
                LG_FNR_FUNCTION OBJ_LG_FNR_FUNCTION = new LG_FNR_FUNCTION();
                List<LG_FNR_ROLE_PERMISSION_DETAILS> LIST_LG_FNR_ROLE_PERMISSION_DETAILS = new List<LG_FNR_ROLE_PERMISSION_DETAILS>();
                LG_FNR_FUNCTION_MAP OBJ_LG_FNR_FUNCTION_MAP_OLD = new LG_FNR_FUNCTION_MAP();
                LG_FNR_FUNCTION_MAP OBJ_LG_FNR_FUNCTION_MAP_NEW = new LG_FNR_FUNCTION_MAP();
                string result = string.Empty;
                try
                {
                    string format = OutgoingResponseFormat.GetFormat();
                    OutgoingResponseFormat.SetResponseFormat(format);

                    OBJ_LG_FNR_FUNCTION = Obj_DBModelEntities.LG_FNR_FUNCTION
                                          .Where(f => f.FUNCTION_ID == pFUNCTION_ID).SingleOrDefault();
                    Class_Conversion.LG_FNR_FUNCTION_REVERSE_CON(OBJ_LG_FNR_FUNCTION_MAP_OLD, OBJ_LG_FNR_FUNCTION); //OLD

                    OBJ_LG_FNR_FUNCTION_MAP_NEW = BooleanConversion.LG_FNR_FUNCTION_MAP_BOOL_TO_INT(pMAINT_CRT_FLAG_B, pMAINT_EDT_FLAG_B, pMAINT_DEL_FLAG_B, pMAINT_DTL_FLAG_B, pMAINT_INDX_FLAG_B, pMAINT_OTP_FLAG_B, pMAINT_2FA_FLAG_B, pMAINT_2FA_HARD_FLAG_B, pMAINT_2FA_SOFT_FLAG_B, pREPORT_VIEW_FLAG_B, pREPORT_PRINT_FLAG_B, pREPORT_GEN_FLAG_B, pMAINT_AUTH_FLAG_B, pMAINT_BIO_FLAG_B, pPROCESS_FLAG_B, pHO_FUNCTION_FLAG_B, pENABLED_FLAG_B);

                    /*
                    Class_Conversion.LG_FNR_FUNCTION_CON(OBJ_LG_FNR_FUNCTION, OBJ_LG_FNR_FUNCTION_MAP);
                    OBJ_LG_FNR_FUNCTION.FUNCTION_NM = pFUNCTION_NM;
                    OBJ_LG_FNR_FUNCTION.APPLICATION_ID = pAPPLICATION_ID;
                    OBJ_LG_FNR_FUNCTION.SERVICE_ID = pAPPLICATION_ID;
                    OBJ_LG_FNR_FUNCTION.MODULE_ID = pSERVICE_ID;
                    OBJ_LG_FNR_FUNCTION.ITEM_TYPE = pITEM_TYPE;
                    if (!string.IsNullOrEmpty(pAUTH_LEVEL) && pAUTH_LEVEL.Any(char.IsNumber))
                    {
                        OBJ_LG_FNR_FUNCTION.AUTH_LEVEL = Convert.ToInt16(pAUTH_LEVEL);
                    }
                    else
                        OBJ_LG_FNR_FUNCTION.AUTH_LEVEL = 0;  */

                    //if (pMAINT_AUTH_FLAG_B.ToLower() == "true")
                    //{
                    //    OBJ_LG_FNR_FUNCTION.AUTH_STATUS_ID = "U";
                    //}
                    //else
                    //    OBJ_LG_FNR_FUNCTION.AUTH_STATUS_ID = "A";
                    OBJ_LG_FNR_FUNCTION.AUTH_STATUS_ID = "U";
                    OBJ_LG_FNR_FUNCTION.LAST_ACTION = "EDT";
                    OBJ_LG_FNR_FUNCTION.LAST_UPDATE_DT = System.DateTime.Now;
                    Obj_DBModelEntities.SaveChanges();

                    //New
                    OBJ_LG_FNR_FUNCTION_MAP_NEW.FUNCTION_NM = pFUNCTION_NM;
                    OBJ_LG_FNR_FUNCTION_MAP_NEW.APPLICATION_ID = pAPPLICATION_ID;
                    OBJ_LG_FNR_FUNCTION_MAP_NEW.SERVICE_ID = pSERVICE_ID;
                    OBJ_LG_FNR_FUNCTION_MAP_NEW.MODULE_ID = pMODULE_ID;
                    OBJ_LG_FNR_FUNCTION_MAP_NEW.ITEM_TYPE = pITEM_TYPE;
                    if (!string.IsNullOrEmpty(pAUTH_LEVEL) && pAUTH_LEVEL.Any(char.IsNumber))
                    {
                        OBJ_LG_FNR_FUNCTION_MAP_NEW.AUTH_LEVEL = Convert.ToInt16(pAUTH_LEVEL);
                    }
                    else
                        OBJ_LG_FNR_FUNCTION_MAP_NEW.AUTH_LEVEL = 0;
                    OBJ_LG_FNR_FUNCTION_MAP_NEW.AUTH_STATUS_ID = "U";
                    OBJ_LG_FNR_FUNCTION_MAP_NEW.LAST_ACTION = "EDT";
                    OBJ_LG_FNR_FUNCTION_MAP_NEW.LAST_UPDATE_DT = System.DateTime.Now;
                    OBJ_LG_FNR_FUNCTION_MAP_NEW.MAKE_DT = OBJ_LG_FNR_FUNCTION.MAKE_DT;

                    OBJ_LG_FNR_FUNCTION_MAP_NEW.FAST_PATH_NO = pFAST_PATH_NO;
                    OBJ_LG_FNR_FUNCTION_MAP_NEW.TARGET_PATH = string.IsNullOrWhiteSpace(pTARGET_PATH) ? pTARGET_PATH : CryptorEngine.ConvertHexToString(pTARGET_PATH, System.Text.Encoding.Unicode);
                    OBJ_LG_FNR_FUNCTION_MAP_NEW.DB_ROLE_NAME = pDB_ROLE_NAME;

                    #region Update in "PermissionDetails" Table

                    List<string> LIST_TRUE_FLAG_PERMISN_DTL = new List<string>();
                    string crt_permission_details = pFUNCTION_NM + "-" + "Create";
                    string edt_permission_details = pFUNCTION_NM + "-" + "Edit";
                    string del_permission_details = pFUNCTION_NM + "-" + "Delete";
                    string dtl_permission_details = pFUNCTION_NM + "-" + "Details";
                    string indx_permission_details = pFUNCTION_NM + "-" + "Index";
                    bool exists = false;

                    if (pMAINT_CRT_FLAG_B.ToLower() == "true")
                    {
                        LIST_TRUE_FLAG_PERMISN_DTL.Add(crt_permission_details);
                    }
                    if (pMAINT_EDT_FLAG_B.ToLower() == "true")
                    {
                        LIST_TRUE_FLAG_PERMISN_DTL.Add(edt_permission_details);
                    }
                    if (pMAINT_DEL_FLAG_B.ToLower() == "true")
                    {
                        LIST_TRUE_FLAG_PERMISN_DTL.Add(del_permission_details);
                    }
                    if (pMAINT_DTL_FLAG_B.ToLower() == "true")
                    {
                        LIST_TRUE_FLAG_PERMISN_DTL.Add(dtl_permission_details);
                    }
                    if (pMAINT_INDX_FLAG_B.ToLower() == "true")
                    {
                        LIST_TRUE_FLAG_PERMISN_DTL.Add(indx_permission_details);
                    }
                    LIST_LG_FNR_ROLE_PERMISSION_DETAILS = Obj_DBModelEntities.LG_FNR_ROLE_PERMISSION_DETAILS
                                                         .Where(f => f.FUNCTION_ID == pFUNCTION_ID).ToList();

                    //if already exits in child then update data to child table row
                    foreach (LG_FNR_ROLE_PERMISSION_DETAILS ITEM_LG_FNR_ROLE_PERMISSION_DETAILS in LIST_LG_FNR_ROLE_PERMISSION_DETAILS)
                    {
                        if (ITEM_LG_FNR_ROLE_PERMISSION_DETAILS.PERMISSION_DETAILS.Contains("Create") && ((string.Compare(ITEM_LG_FNR_ROLE_PERMISSION_DETAILS.PERMISSION_DETAILS, crt_permission_details) != 0) || (string.Compare(((ITEM_LG_FNR_ROLE_PERMISSION_DETAILS.FUNCTION_PERMISSION_FLAG == 1) ? "true" : "false"), pMAINT_CRT_FLAG_B.ToLower()) != 0)))
                        {
                            result = Update_PermissionDetail(pMAINT_AUTH_FLAG_B, pFUNCTION_ID, ITEM_LG_FNR_ROLE_PERMISSION_DETAILS.PERMISSION_ID, crt_permission_details, pMAINT_CRT_FLAG_B);
                            if (result.ToLower() != "true")
                                return result;
                        }
                        if (ITEM_LG_FNR_ROLE_PERMISSION_DETAILS.PERMISSION_DETAILS.Contains("Edit") && ((string.Compare(ITEM_LG_FNR_ROLE_PERMISSION_DETAILS.PERMISSION_DETAILS, edt_permission_details) != 0) || (string.Compare(((ITEM_LG_FNR_ROLE_PERMISSION_DETAILS.FUNCTION_PERMISSION_FLAG == 1) ? "true" : "false"), pMAINT_EDT_FLAG_B.ToLower()) != 0)))
                        {
                            result = Update_PermissionDetail(pMAINT_AUTH_FLAG_B, pFUNCTION_ID, ITEM_LG_FNR_ROLE_PERMISSION_DETAILS.PERMISSION_ID, edt_permission_details, pMAINT_EDT_FLAG_B);
                            if (result.ToLower() != "true")
                                return result;
                        }
                        if (ITEM_LG_FNR_ROLE_PERMISSION_DETAILS.PERMISSION_DETAILS.Contains("Delete") && ((string.Compare(ITEM_LG_FNR_ROLE_PERMISSION_DETAILS.PERMISSION_DETAILS, del_permission_details) != 0) || (string.Compare(((ITEM_LG_FNR_ROLE_PERMISSION_DETAILS.FUNCTION_PERMISSION_FLAG == 1) ? "true" : "false"), pMAINT_DEL_FLAG_B.ToLower()) != 0)))
                        {
                            result = Update_PermissionDetail(pMAINT_AUTH_FLAG_B, pFUNCTION_ID, ITEM_LG_FNR_ROLE_PERMISSION_DETAILS.PERMISSION_ID, del_permission_details, pMAINT_DEL_FLAG_B);
                            if (result.ToLower() != "true")
                                return result;
                        }
                        if (ITEM_LG_FNR_ROLE_PERMISSION_DETAILS.PERMISSION_DETAILS.Contains("Details") && ((string.Compare(ITEM_LG_FNR_ROLE_PERMISSION_DETAILS.PERMISSION_DETAILS, dtl_permission_details) != 0) || (string.Compare(((ITEM_LG_FNR_ROLE_PERMISSION_DETAILS.FUNCTION_PERMISSION_FLAG == 1) ? "true" : "false"), pMAINT_DTL_FLAG_B.ToLower()) != 0)))
                        {
                            result = Update_PermissionDetail(pMAINT_AUTH_FLAG_B, pFUNCTION_ID, ITEM_LG_FNR_ROLE_PERMISSION_DETAILS.PERMISSION_ID, dtl_permission_details, pMAINT_DTL_FLAG_B);
                            if (result.ToLower() != "true")
                                return result;
                        }
                        if (ITEM_LG_FNR_ROLE_PERMISSION_DETAILS.PERMISSION_DETAILS.Contains("Index") && ((string.Compare(ITEM_LG_FNR_ROLE_PERMISSION_DETAILS.PERMISSION_DETAILS, indx_permission_details) != 0) || (string.Compare(((ITEM_LG_FNR_ROLE_PERMISSION_DETAILS.FUNCTION_PERMISSION_FLAG == 1) ? "true" : "false"), pMAINT_INDX_FLAG_B.ToLower()) != 0)))
                        {
                            result = Update_PermissionDetail(pMAINT_AUTH_FLAG_B, pFUNCTION_ID, ITEM_LG_FNR_ROLE_PERMISSION_DETAILS.PERMISSION_ID, indx_permission_details, pMAINT_INDX_FLAG_B);
                            if (result.ToLower() != "true")
                                return result;
                        }
                    }

                    //if not already exits in child then save new row to child table
                    foreach (string ITEM_PERMISN_DTL in LIST_TRUE_FLAG_PERMISN_DTL)
                    {
                        Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
                        LIST_LG_FNR_ROLE_PERMISSION_DETAILS = Obj_DBModelEntities.LG_FNR_ROLE_PERMISSION_DETAILS
                                                             .Where(f => f.FUNCTION_ID == pFUNCTION_ID).ToList();
                        exists = false;
                        foreach (LG_FNR_ROLE_PERMISSION_DETAILS ITEM_LG_FNR_ROLE_PERMISSION_DETAILS in LIST_LG_FNR_ROLE_PERMISSION_DETAILS)
                        {
                            if (string.Compare(ITEM_PERMISN_DTL, ITEM_LG_FNR_ROLE_PERMISSION_DETAILS.PERMISSION_DETAILS) == 0)
                            {
                                exists = true;
                            }
                        }
                        if (!exists)
                        {
                            //int permission_id = Convert.ToInt32(Obj_DBModelEntities.LG_FNR_ROLE_PERMISSION_DETAILS
                            //        .Max(x => x.PERMISSION_ID)) + 1;
                            int permission_id = (Obj_DBModelEntities.LG_FNR_ROLE_PERMISSION_DETAILS
                                                .Select(i => i.PERMISSION_ID).Cast<int?>().Max() ?? 0) + 1;
                            result = Add_PermissionDetail(pMAINT_AUTH_FLAG_B, pFUNCTION_ID, permission_id, ITEM_PERMISN_DTL);
                            if (result.ToLower() != "true")
                                return result;
                        }
                    }

                    #endregion Update in "PermissionDetails" Table

                    #region Auth log

                    LG_AA_NFT_AUTH_LOG_MAP OBJ_LG_AA_NFT_AUTH_LOG_MAP = new LG_AA_NFT_AUTH_LOG_MAP();
                    FUNC_ID = OBJ_LG_AA_NFT_AUTH_LOG_MAP.FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "Function").Select(x => x.FUNCTION_ID).SingleOrDefault();
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_NAME = "LG_FNR_FUNCTION";
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_NM = "FUNCTION_ID";
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_VAL = OBJ_LG_FNR_FUNCTION.FUNCTION_ID;
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
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.MAKE_BY = psession_user;
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.MAKE_DT = DateTime.Now;
                    LG_AA_NFT_AUTH_LOG_MAP.AddNftAuthLog(OBJ_LG_FNR_FUNCTION_MAP_OLD, OBJ_LG_FNR_FUNCTION_MAP_NEW, OBJ_LG_AA_NFT_AUTH_LOG_MAP);

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
                            result = "Can't Update Function(Db) " + validationError.ErrorMessage;
                        }
                    }
                    LG_ERROR_LOG_MAP.Add_Error_Log(pFUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "UpdateFunction",
                                                   "0000000000", dbEx.Message, inner, dbEx.StackTrace, psession_user, dateTime);
                    return result;
                }
                catch (Exception ex)
                {
                    ts.Dispose();
                    string dateTime = Convert.ToString(System.DateTime.Now);
                    string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);
                    LG_ERROR_LOG_MAP.Add_Error_Log(pFUNCTION_ID, ex.Source, "ERR_APP_TYPE", "UpdateFunction",
                                                   "0000000000", ex.Message, inner4, ex.StackTrace, psession_user, dateTime);
                    result = "Can't Update Function ";
                    return result;
                }
            }
        }

        #endregion Update

        #region Delete

        public static string DeleteFunction(string pFUNCTION_ID, string psession_user)
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            string result = string.Empty;
            try
            {
                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                LG_FNR_FUNCTION OBJ_LG_FNR_FUNCTION = (from f in Obj_DBModelEntities.LG_FNR_FUNCTION
                                                       where f.FUNCTION_ID == pFUNCTION_ID
                                                       select f).SingleOrDefault();

                if (OBJ_LG_FNR_FUNCTION != null)
                {
                    OBJ_LG_FNR_FUNCTION.AUTH_STATUS_ID = "U";
                    OBJ_LG_FNR_FUNCTION.LAST_ACTION = "DEL";
                    OBJ_LG_FNR_FUNCTION.LAST_UPDATE_DT = System.DateTime.Now;
                    Obj_DBModelEntities.SaveChanges();

                    string result1 = Delete_PermissionDetail(pFUNCTION_ID);
                    if (result1.ToLower() != "true")
                    {
                        return result1;
                    }
                    result = "True";
                    return result;
                }
                else
                    return "Can't find the selected function in DB.";
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
                        result = "Can't Delete Function(Db) " + validationError.ErrorMessage;
                    }
                }
                LG_ERROR_LOG_MAP.Add_Error_Log(pFUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "DeleteFunction",
                       "0000000000", dbEx.Message, inner, dbEx.StackTrace, psession_user, dateTime);
                return result;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);
                LG_ERROR_LOG_MAP.Add_Error_Log(pFUNCTION_ID, ex.Source, "ERR_APP_TYPE", "DeleteFunction",
                                               "0000000000", ex.Message, inner4, ex.StackTrace, psession_user, dateTime);
                result = "Can't Delete Function " + ex.Message;
                return result;
            }
        }

        #endregion Delete

        #region Fetch Single

        public static LG_FNR_FUNCTION_MAP GetFunctionByFunctionId(string pFUNCTION_ID)
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            LG_FNR_FUNCTION_MAP OBJ_LG_FNR_FUNCTION_MAP = new LG_FNR_FUNCTION_MAP();

            try
            {
                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                OBJ_LG_FNR_FUNCTION_MAP = (from f in Obj_DBModelEntities.LG_FNR_FUNCTION
                                           //join a in Obj_DBModelEntities.LG_FNR_APPLICATION
                                           //on f.APPLICATION_ID equals a.APPLICATION_ID
                                           //join s in Obj_DBModelEntities.LG_FNR_SERVICE
                                           //on f.SERVICE_ID equals s.SERVICE_ID
                                           //join m in Obj_DBModelEntities.LG_FNR_MODULE
                                           //on new { f.MODULE_ID, f.SERVICE_ID } equals new { m.MODULE_ID,m.SERVICE_ID }
                                           where f.FUNCTION_ID == pFUNCTION_ID
                                           select new LG_FNR_FUNCTION_MAP
                                           {
                                               FUNCTION_ID = f.FUNCTION_ID,
                                               FUNCTION_NM = f.FUNCTION_NM,

                                               APPLICATION_ID = f.APPLICATION_ID,
                                               //APPLICATION_NAME = a.APPLICATION_NAME,
                                               SERVICE_ID = f.SERVICE_ID,
                                               //SERVICE_NM = s.SERVICE_NM,
                                               MODULE_ID = f.MODULE_ID,
                                               //MODULE_NM = m.MODULE_NM,
                                               ITEM_TYPE = f.ITEM_TYPE,

                                               MAINT_CRT_FLAG = f.MAINT_CRT_FLAG,
                                               MAINT_EDT_FLAG = f.MAINT_EDT_FLAG,
                                               MAINT_DEL_FLAG = f.MAINT_DEL_FLAG,
                                               MAINT_DTL_FLAG = f.MAINT_DTL_FLAG,
                                               MAINT_INDX_FLAG = f.MAINT_INDX_FLAG,

                                               MAINT_OTP_FLAG = f.MAINT_OTP_FLAG,
                                               MAINT_BIO_FLAG = f.MAINT_BIO_FLAG,
                                               MAINT_2FA_FLAG = f.MAINT_2FA_FLAG,
                                               MAINT_2FA_HARD_FLAG = f.MAINT_2FA_HARD_FLAG,
                                               MAINT_2FA_SOFT_FLAG = f.MAINT_2FA_SOFT_FLAG,
                                               MAINT_AUTH_FLAG = f.MAINT_AUTH_FLAG,
                                               AUTH_LEVEL = f.AUTH_LEVEL,

                                               REPORT_VIEW_FLAG = f.REPORT_VIEW_FLAG,
                                               REPORT_PRINT_FLAG = f.REPORT_PRINT_FLAG,
                                               REPORT_GEN_FLAG = f.REPORT_GEN_FLAG,

                                               PROCESS_FLAG = f.PROCESS_FLAG,
                                               ENABLED_FLAG = f.ENABLED_FLAG,

                                               AUTH_STATUS_ID = f.AUTH_STATUS_ID,
                                               LAST_ACTION = f.LAST_ACTION,
                                               LAST_UPDATE_DT = f.LAST_UPDATE_DT,
                                               MAKE_DT = f.MAKE_DT
                                           }).SingleOrDefault();

                OBJ_LG_FNR_FUNCTION_MAP.APPLICATION_NAME = Obj_DBModelEntities.LG_FNR_APPLICATION.Where(x => x.APPLICATION_ID == OBJ_LG_FNR_FUNCTION_MAP.APPLICATION_ID).Select(x => x.APPLICATION_NAME).SingleOrDefault();
                OBJ_LG_FNR_FUNCTION_MAP.SERVICE_NM = Obj_DBModelEntities.LG_FNR_SERVICE.Where(x => x.SERVICE_ID == OBJ_LG_FNR_FUNCTION_MAP.SERVICE_ID && x.APPLICATION_ID == OBJ_LG_FNR_FUNCTION_MAP.APPLICATION_ID).Select(x => x.SERVICE_NM).SingleOrDefault();
                OBJ_LG_FNR_FUNCTION_MAP.MODULE_NM = Obj_DBModelEntities.LG_FNR_MODULE.Where(x => x.MODULE_ID == OBJ_LG_FNR_FUNCTION_MAP.MODULE_ID && x.SERVICE_ID == OBJ_LG_FNR_FUNCTION_MAP.SERVICE_ID && x.APPLICATION_ID == OBJ_LG_FNR_FUNCTION_MAP.APPLICATION_ID).Select(x => x.MODULE_NM).SingleOrDefault();

                BooleanConversion.LG_FNR_FUNCTION_MAP_INT_TO_BOOL(OBJ_LG_FNR_FUNCTION_MAP);
                OBJ_LG_FNR_FUNCTION_MAP.APPLICATION_LIST_FOR_DD = DropDown.GetApplicationForDD();
                OBJ_LG_FNR_FUNCTION_MAP.SERVICE_LIST_FOR_DD = DropDown.GetServiceForDD();
                OBJ_LG_FNR_FUNCTION_MAP.MODULE_LIST_FOR_DD = DropDown.GetModuleForDD();
                OBJ_LG_FNR_FUNCTION_MAP.ITEM_TYPE_LIST_FOR_DD = DropDown.GetItemtypeForDD();
                return OBJ_LG_FNR_FUNCTION_MAP;
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
                        OBJ_LG_FNR_FUNCTION_MAP.ERROR = validationError.ErrorMessage;
                    }
                }
                LG_ERROR_LOG_MAP.Add_Error_Log(pFUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "GetFunctionByFunctionId",
                                               "0000000000", dbEx.Message, inner, dbEx.StackTrace, null, dateTime);
                return OBJ_LG_FNR_FUNCTION_MAP;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);
                LG_ERROR_LOG_MAP.Add_Error_Log(pFUNCTION_ID, ex.Source, "ERR_APP_TYPE", "GetFunctionByFunctionId",
                                               "0000000000", ex.Message, inner4, ex.StackTrace, null, dateTime);

                OBJ_LG_FNR_FUNCTION_MAP.ERROR = ex.Message;
                return OBJ_LG_FNR_FUNCTION_MAP;
            }
        }
        public static LG_FNR_FUNCTION_MAP GetFunctionDetailsByFunctionId(string pFUNCTION_ID)
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            LG_FNR_FUNCTION_MAP OBJ_LG_FNR_FUNCTION_MAP = new LG_FNR_FUNCTION_MAP();

            try
            {
                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                OBJ_LG_FNR_FUNCTION_MAP = (from f in Obj_DBModelEntities.LG_FNR_FUNCTION
                                           where f.FUNCTION_ID == pFUNCTION_ID
                                           select new LG_FNR_FUNCTION_MAP
                                           {
                                               FUNCTION_ID = f.FUNCTION_ID,
                                               FUNCTION_NM = f.FUNCTION_NM,

                                               APPLICATION_ID = f.APPLICATION_ID,
                                               //APPLICATION_NAME = a.APPLICATION_NAME,
                                               SERVICE_ID = f.SERVICE_ID,
                                               //SERVICE_NM = s.SERVICE_NM,
                                               MODULE_ID = f.MODULE_ID,
                                               //MODULE_NM = m.MODULE_NM,
                                               ITEM_TYPE = f.ITEM_TYPE,

                                               MAINT_CRT_FLAG = f.MAINT_CRT_FLAG,
                                               MAINT_EDT_FLAG = f.MAINT_EDT_FLAG,
                                               MAINT_DEL_FLAG = f.MAINT_DEL_FLAG,
                                               MAINT_DTL_FLAG = f.MAINT_DTL_FLAG,
                                               MAINT_INDX_FLAG = f.MAINT_INDX_FLAG,

                                               MAINT_OTP_FLAG = f.MAINT_OTP_FLAG,
                                               MAINT_BIO_FLAG = f.MAINT_BIO_FLAG,
                                               MAINT_2FA_FLAG = f.MAINT_2FA_FLAG,
                                               MAINT_2FA_HARD_FLAG = f.MAINT_2FA_HARD_FLAG,
                                               MAINT_2FA_SOFT_FLAG = f.MAINT_2FA_SOFT_FLAG,
                                               MAINT_AUTH_FLAG = f.MAINT_AUTH_FLAG,
                                               AUTH_LEVEL = f.AUTH_LEVEL,

                                               REPORT_VIEW_FLAG = f.REPORT_VIEW_FLAG,
                                               REPORT_PRINT_FLAG = f.REPORT_PRINT_FLAG,
                                               REPORT_GEN_FLAG = f.REPORT_GEN_FLAG,

                                               PROCESS_FLAG = f.PROCESS_FLAG,
                                               ENABLED_FLAG = f.ENABLED_FLAG,

                                               AUTH_STATUS_ID = f.AUTH_STATUS_ID,
                                               LAST_ACTION = f.LAST_ACTION,
                                               LAST_UPDATE_DT = f.LAST_UPDATE_DT,
                                               MAKE_DT = f.MAKE_DT
                                           }).SingleOrDefault();

                OBJ_LG_FNR_FUNCTION_MAP.APPLICATION_NAME = Obj_DBModelEntities.LG_FNR_APPLICATION.Where(x => x.APPLICATION_ID == OBJ_LG_FNR_FUNCTION_MAP.APPLICATION_ID).Select(x => x.APPLICATION_NAME).SingleOrDefault();
                OBJ_LG_FNR_FUNCTION_MAP.SERVICE_NM = Obj_DBModelEntities.LG_FNR_SERVICE.Where(x => x.SERVICE_ID == OBJ_LG_FNR_FUNCTION_MAP.SERVICE_ID && x.APPLICATION_ID == OBJ_LG_FNR_FUNCTION_MAP.APPLICATION_ID).Select(x => x.SERVICE_NM).SingleOrDefault();
                OBJ_LG_FNR_FUNCTION_MAP.MODULE_NM = Obj_DBModelEntities.LG_FNR_MODULE.Where(x => x.MODULE_ID == OBJ_LG_FNR_FUNCTION_MAP.MODULE_ID && x.SERVICE_ID == OBJ_LG_FNR_FUNCTION_MAP.SERVICE_ID && x.APPLICATION_ID == OBJ_LG_FNR_FUNCTION_MAP.APPLICATION_ID).Select(x => x.MODULE_NM).SingleOrDefault();

                BooleanConversion.LG_FNR_FUNCTION_MAP_INT_TO_BOOL(OBJ_LG_FNR_FUNCTION_MAP);
                return OBJ_LG_FNR_FUNCTION_MAP;
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
                        OBJ_LG_FNR_FUNCTION_MAP.ERROR = validationError.ErrorMessage;
                    }
                }
                LG_ERROR_LOG_MAP.Add_Error_Log(pFUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "GetFunctionByFunctionId",
                                               "0000000000", dbEx.Message, inner, dbEx.StackTrace, null, dateTime);
                return OBJ_LG_FNR_FUNCTION_MAP;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);
                LG_ERROR_LOG_MAP.Add_Error_Log(pFUNCTION_ID, ex.Source, "ERR_APP_TYPE", "GetFunctionByFunctionId",
                                               "0000000000", ex.Message, inner4, ex.StackTrace, null, dateTime);

                OBJ_LG_FNR_FUNCTION_MAP.ERROR = ex.Message;
                return OBJ_LG_FNR_FUNCTION_MAP;
            }
        }
        public static LG_FNR_FUNCTION_MAP GetReportFunctionIds(string pAPPLICATION_ID)
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            LG_FNR_FUNCTION_MAP OBJ_LG_FNR_FUNCTION_MAP = new LG_FNR_FUNCTION_MAP();

            try
            {
                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                OBJ_LG_FNR_FUNCTION_MAP = (from f in Obj_DBModelEntities.LG_FNR_FUNCTION
                                           where f.APPLICATION_ID == pAPPLICATION_ID &&
                                                 f.ITEM_TYPE == "R" &&
                                                 f.AUTH_STATUS_ID == "A" &&
                                                 f.LAST_ACTION != "DEL"
                                           select new LG_FNR_FUNCTION_MAP
                                           {
                                               FUNCTION_ID = f.FUNCTION_ID,
                                               FUNCTION_NM = f.FUNCTION_NM,
                                           }).SingleOrDefault();
                return OBJ_LG_FNR_FUNCTION_MAP;
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
                        OBJ_LG_FNR_FUNCTION_MAP.ERROR = validationError.ErrorMessage;
                    }
                }
                LG_ERROR_LOG_MAP.Add_Error_Log(null, dbEx.Source, "ERR_APP_TYPE", "GetReportFunctionIds",
                                               "0000000000", dbEx.Message, inner, dbEx.StackTrace, null, dateTime);
                return OBJ_LG_FNR_FUNCTION_MAP;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);
                LG_ERROR_LOG_MAP.Add_Error_Log(null, ex.Source, "ERR_APP_TYPE", "GetReportFunctionIds",
                                               "0000000000", ex.Message, inner4, ex.StackTrace, null, dateTime);

                OBJ_LG_FNR_FUNCTION_MAP.ERROR = ex.Message;
                return OBJ_LG_FNR_FUNCTION_MAP;
            }
        }
        #endregion Fetch Single

        #region Fetch all

        public static List<LG_FNR_FUNCTION_MAP> GetFunctions()
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            List<LG_FNR_FUNCTION_MAP> LIST_LG_FNR_FUNCTION_MAP = null;
            LG_FNR_FUNCTION_MAP OBJ_LG_FNR_FUNCTION_MAP = new LG_FNR_FUNCTION_MAP();

            try
            {
                Obj_DBModelEntities.Configuration.LazyLoadingEnabled = false;
                Obj_DBModelEntities.Configuration.ProxyCreationEnabled = false;

                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                LIST_LG_FNR_FUNCTION_MAP = (from f in Obj_DBModelEntities.LG_FNR_FUNCTION
                                            join a in Obj_DBModelEntities.LG_FNR_APPLICATION
                                            on f.APPLICATION_ID equals a.APPLICATION_ID
                                            join s in Obj_DBModelEntities.LG_FNR_SERVICE
                                            //on f.SERVICE_ID equals s.SERVICE_ID
                                            on new { f.SERVICE_ID, f.APPLICATION_ID } equals new { s.SERVICE_ID, s.APPLICATION_ID }
                                            join m in Obj_DBModelEntities.LG_FNR_MODULE
                                            on new { f.MODULE_ID, f.SERVICE_ID, f.APPLICATION_ID } equals new { m.MODULE_ID, m.SERVICE_ID, m.APPLICATION_ID }
                                            where f.LAST_ACTION != "DEL" && f.AUTH_STATUS_ID != "U"
                                            orderby f.FUNCTION_ID ascending
                                            select new LG_FNR_FUNCTION_MAP
                                            {
                                                FUNCTION_ID = f.FUNCTION_ID,
                                                FUNCTION_NM = f.FUNCTION_NM,

                                                APPLICATION_NAME = a.APPLICATION_NAME,
                                                SERVICE_NM = s.SERVICE_NM,
                                                MODULE_NM = m.MODULE_NM,

                                                AUTH_STATUS_ID = f.AUTH_STATUS_ID,
                                                LAST_ACTION = f.LAST_ACTION,
                                                LAST_UPDATE_DT = f.LAST_UPDATE_DT,
                                                MAKE_DT = f.MAKE_DT
                                            }).ToList();
                return LIST_LG_FNR_FUNCTION_MAP;
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
                        OBJ_LG_FNR_FUNCTION_MAP.ERROR = validationError.ErrorMessage;
                        LIST_LG_FNR_FUNCTION_MAP.Add(OBJ_LG_FNR_FUNCTION_MAP);
                    }
                }
                LG_ERROR_LOG_MAP.Add_Error_Log(FUNC_ID, dbEx.Source, "ERR_APP_TYPE", "GetFunctions",
                                          "0000000000", dbEx.Message, inner, dbEx.StackTrace, null, dateTime);

                return LIST_LG_FNR_FUNCTION_MAP;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);
                LG_ERROR_LOG_MAP.Add_Error_Log(FUNC_ID, ex.Source, "ERR_APP_TYPE", "GetFunctions",
                                          "0000000000", ex.Message, inner4, ex.StackTrace, null, dateTime);
                OBJ_LG_FNR_FUNCTION_MAP.ERROR = ex.Message;
                LIST_LG_FNR_FUNCTION_MAP.Add(OBJ_LG_FNR_FUNCTION_MAP);
                return LIST_LG_FNR_FUNCTION_MAP;
            }
        }

        #endregion Fetch all

        #region custom methods

        private static string Add_PermissionDetail(string pMAINT_AUTH_FLAG_B, string function_id, int permission_id, string permission_details)
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            string result = string.Empty;
            try
            {
                LG_FNR_ROLE_PERMISSION_DETAILS OBJ_LG_FNR_ROLE_PERMISSION_DETAILS = new LG_FNR_ROLE_PERMISSION_DETAILS();
                OBJ_LG_FNR_ROLE_PERMISSION_DETAILS.FUNCTION_ID = function_id;
                OBJ_LG_FNR_ROLE_PERMISSION_DETAILS.PERMISSION_ID = permission_id.ToString();
                OBJ_LG_FNR_ROLE_PERMISSION_DETAILS.PERMISSION_DETAILS = permission_details;
                if (pMAINT_AUTH_FLAG_B.ToLower() == "true")
                {
                    OBJ_LG_FNR_ROLE_PERMISSION_DETAILS.AUTH_STATUS_ID = "A";
                }
                else
                    OBJ_LG_FNR_ROLE_PERMISSION_DETAILS.AUTH_STATUS_ID = "A";
                OBJ_LG_FNR_ROLE_PERMISSION_DETAILS.LAST_ACTION = "ADD";
                OBJ_LG_FNR_ROLE_PERMISSION_DETAILS.FUNCTION_PERMISSION_FLAG = 1;
                OBJ_LG_FNR_ROLE_PERMISSION_DETAILS.MAKE_DT = System.DateTime.Now;
                Obj_DBModelEntities.LG_FNR_ROLE_PERMISSION_DETAILS.Add(OBJ_LG_FNR_ROLE_PERMISSION_DETAILS);
                Obj_DBModelEntities.SaveChanges();
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
                        result = "Can't Add PermissionDetails(Db) " + validationError.ErrorMessage;
                    }
                }
                LG_ERROR_LOG_MAP.Add_Error_Log(FUNC_ID, dbEx.Source, "ERR_APP_TYPE", "Add_PermissionDetail",
                                          "0000000000", dbEx.Message, inner, dbEx.StackTrace, null, dateTime);
                return result;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);
                LG_ERROR_LOG_MAP.Add_Error_Log(FUNC_ID, ex.Source, "ERR_APP_TYPE", "Add_PermissionDetail",
                                          "0000000000", ex.Message, inner4, ex.StackTrace, null, dateTime);
                result = "Can't Add PermissionDetails " + ex.Message;
                return result;
            }
            result = "true";
            return result;
        }

        private static string Update_PermissionDetail(string pMAINT_AUTH_FLAG_B, string pFUNCTION_ID, string pPERMISSION_ID, string permission_details, string function_permission_flag)
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            string result = string.Empty;
            int function_permission_flag_val;
            try
            {
                LG_FNR_ROLE_PERMISSION_DETAILS OBJ_LG_FNR_ROLE_PERMISSION_DETAILS = Obj_DBModelEntities.LG_FNR_ROLE_PERMISSION_DETAILS
                                                                                   .Where(f => f.PERMISSION_ID == pPERMISSION_ID).SingleOrDefault();
                //update data in row
                if (string.Compare(OBJ_LG_FNR_ROLE_PERMISSION_DETAILS.PERMISSION_DETAILS, permission_details) != 0)
                {
                    OBJ_LG_FNR_ROLE_PERMISSION_DETAILS.PERMISSION_DETAILS = permission_details;
                }

                function_permission_flag_val = ((function_permission_flag.ToLower() == "true") ? 1 : 0);
                if (string.Compare(function_permission_flag_val.ToString(), OBJ_LG_FNR_ROLE_PERMISSION_DETAILS.FUNCTION_PERMISSION_FLAG.ToString()) != 0)
                {
                    OBJ_LG_FNR_ROLE_PERMISSION_DETAILS.FUNCTION_PERMISSION_FLAG = (short)function_permission_flag_val;
                }

                if (pMAINT_AUTH_FLAG_B.ToLower() == "true")
                {
                    OBJ_LG_FNR_ROLE_PERMISSION_DETAILS.AUTH_STATUS_ID = "U";
                }
                else
                    OBJ_LG_FNR_ROLE_PERMISSION_DETAILS.AUTH_STATUS_ID = "A";

                OBJ_LG_FNR_ROLE_PERMISSION_DETAILS.LAST_ACTION = "EDT";
                OBJ_LG_FNR_ROLE_PERMISSION_DETAILS.MAKE_DT = System.DateTime.Now;
                Obj_DBModelEntities.SaveChanges();
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
                        result = "Can't Update PermissionDetails(Db) " + validationError.ErrorMessage;
                    }
                }
                LG_ERROR_LOG_MAP.Add_Error_Log(FUNC_ID, dbEx.Source, "ERR_APP_TYPE", "Update_PermissionDetail",
                                          "0000000000", dbEx.Message, inner, dbEx.StackTrace, null, dateTime);
                return result;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);
                LG_ERROR_LOG_MAP.Add_Error_Log(FUNC_ID, ex.Source, "ERR_APP_TYPE", "Update_PermissionDetail",
                                          "0000000000", ex.Message, inner4, ex.StackTrace, null, dateTime);
                result = "Can't Update PermissionDetails " + ex.Message;
                return result;
            }
            result = "true";
            return result;
        }

        private static string Delete_PermissionDetail(string pFUNCTION_ID)
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            List<LG_FNR_ROLE_PERMISSION_DETAILS> LIST_LG_FNR_ROLE_PERMISSION_DETAILS = new List<LG_FNR_ROLE_PERMISSION_DETAILS>();
            string result = string.Empty;
            try
            {
                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                LIST_LG_FNR_ROLE_PERMISSION_DETAILS = Obj_DBModelEntities.LG_FNR_ROLE_PERMISSION_DETAILS
                                                     .Where(f => f.FUNCTION_ID == pFUNCTION_ID).ToList();

                if (LIST_LG_FNR_ROLE_PERMISSION_DETAILS != null)
                {
                    foreach (LG_FNR_ROLE_PERMISSION_DETAILS ITEM_LG_FNR_ROLE_PERMISSION_DETAILS in LIST_LG_FNR_ROLE_PERMISSION_DETAILS)
                    {
                        ITEM_LG_FNR_ROLE_PERMISSION_DETAILS.AUTH_STATUS_ID = "U";
                        ITEM_LG_FNR_ROLE_PERMISSION_DETAILS.LAST_ACTION = "DEL";
                        ITEM_LG_FNR_ROLE_PERMISSION_DETAILS.LAST_UPDATE_DT = System.DateTime.Now;
                        ITEM_LG_FNR_ROLE_PERMISSION_DETAILS.FUNCTION_PERMISSION_FLAG = 0;
                        Obj_DBModelEntities.SaveChanges();
                    }
                }
                result = "true";
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
                        result = "Can't Delete Service(Db) " + validationError.ErrorMessage;
                    }
                }
                LG_ERROR_LOG_MAP.Add_Error_Log(FUNC_ID, dbEx.Source, "ERR_APP_TYPE", "Delete_PermissionDetail",
                                          "0000000000", dbEx.Message, inner, dbEx.StackTrace, null, dateTime);
                return result;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);
                LG_ERROR_LOG_MAP.Add_Error_Log(FUNC_ID, ex.Source, "ERR_APP_TYPE", "Delete_PermissionDetail",
                                          "0000000000", ex.Message, inner4, ex.StackTrace, null, dateTime);
                result = "Can't Delete Service " + ex.Message;
                return result;
            }
        }

        public static IEnumerable<LG_FNR_FUNCTION_MAP> GetAllFunctionsByAppId(string pAPPLICATION_ID, string pUSERID, string pPROCESS_FLAG)
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            List<LG_FNR_FUNCTION_MAP> LIST_LG_FNR_FUNCTION_MAP = null;
            LG_FNR_FUNCTION_MAP OBJ_LG_FNR_FUNCTION_MAP = new LG_FNR_FUNCTION_MAP();

            try
            {
                Obj_DBModelEntities.Configuration.LazyLoadingEnabled = false;
                Obj_DBModelEntities.Configuration.ProxyCreationEnabled = false;

                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                if (string.IsNullOrEmpty(pPROCESS_FLAG) || pPROCESS_FLAG == "null")
                {
                    LIST_LG_FNR_FUNCTION_MAP = (from f in Obj_DBModelEntities.LG_FNR_FUNCTION
                                                where f.LAST_ACTION != "DEL" &&
                                                      f.AUTH_STATUS_ID == "A" &&
                                                      f.APPLICATION_ID == pAPPLICATION_ID
                                                orderby f.FUNCTION_ID ascending
                                                select new LG_FNR_FUNCTION_MAP
                                                {
                                                    FUNCTION_ID = f.FUNCTION_ID,
                                                    FUNCTION_NM = f.FUNCTION_NM,
                                                    MAINT_AUTH_FLAG = f.MAINT_AUTH_FLAG,
                                                    AUTH_LEVEL = f.AUTH_LEVEL,
                                                    AUTH_STATUS_ID = f.AUTH_STATUS_ID
                                                }).ToList();
                }
                else
                {
                    int _process_flag = Convert.ToInt16(pPROCESS_FLAG);
                    LIST_LG_FNR_FUNCTION_MAP = (from f in Obj_DBModelEntities.LG_FNR_FUNCTION
                                                where f.LAST_ACTION != "DEL" &&
                                                      f.AUTH_STATUS_ID == "A" &&
                                                      f.APPLICATION_ID == pAPPLICATION_ID &&
                                                      f.PROCESS_FLAG == _process_flag
                                                orderby f.FUNCTION_ID ascending
                                                select new LG_FNR_FUNCTION_MAP
                                                {
                                                    FUNCTION_ID = f.FUNCTION_ID,
                                                    FUNCTION_NM = f.FUNCTION_NM,
                                                    MAINT_AUTH_FLAG = f.MAINT_AUTH_FLAG,
                                                    AUTH_LEVEL = f.AUTH_LEVEL,
                                                    AUTH_STATUS_ID = f.AUTH_STATUS_ID
                                                }).ToList();
                }

                
                if (string.IsNullOrEmpty(pUSERID) || pUSERID == "null")
                {
                    return LIST_LG_FNR_FUNCTION_MAP;
                }

                LG_USER_SETUP_PROFILE_MAP OBJ_LG_USER_SETUP_PROFILE_MAP = (from u in Obj_DBModelEntities.LG_USER_SETUP_PROFILE
                                                                           where u.USER_ID == pUSERID &&
                                                                                 u.AUTH_STATUS_ID == "A" &&
                                                                                 u.LAST_ACTION != "DEL"
                                                                           select new LG_USER_SETUP_PROFILE_MAP
                                                                           {
                                                                               USER_CLASSIFICATION_ID = u.USER_CLASSIFICATION_ID,
                                                                               USER_AREA_ID = u.USER_AREA_ID,
                                                                               USER_AREA_ID_VALUE = u.USER_AREA_ID_VALUE
                                                                           }).SingleOrDefault();

                if (OBJ_LG_USER_SETUP_PROFILE_MAP.USER_CLASSIFICATION_ID == "3" && OBJ_LG_USER_SETUP_PROFILE_MAP.USER_AREA_ID == "7")
                {
                    List<LG_FNR_FUNCTION_MAP> SELECTED_FUNCTION_LIST_FOR_AGENT = new List<LG_FNR_FUNCTION_MAP>();
                    //List<string> FuntionsForAgents = new List<string>() { "020203005", "020205001", "020205002", "020205003", "020205004", "020205005" };

                    List<string> FuntionsForAgents = new List<string>();

                    FuntionsForAgents = Obj_DBModelEntities.LG_USER_CLASS_AREA_FUNCTION
                                       .Where(t => t.CLASSIFICATION_ID == "3" &&
                                                   t.AREA_ID == "7")
                                       .Select(t => t.FUNCTION_ID).ToList();

                    foreach (var func in FuntionsForAgents.ToList())
                    {
                        foreach (var item in LIST_LG_FNR_FUNCTION_MAP)
                        {
                            if (func == item.FUNCTION_ID)
                            {
                                FuntionsForAgents.Remove(func);
                                SELECTED_FUNCTION_LIST_FOR_AGENT.Add(item);
                                break;
                            }
                        }
                    }
                    return SELECTED_FUNCTION_LIST_FOR_AGENT;
                }

                if (OBJ_LG_USER_SETUP_PROFILE_MAP.USER_CLASSIFICATION_ID == "1" && OBJ_LG_USER_SETUP_PROFILE_MAP.USER_AREA_ID == "3")
                {
                    List<LG_FNR_FUNCTION_MAP> SELECTED_FUNCTION_LIST_FOR_AGENT = new List<LG_FNR_FUNCTION_MAP>();
                    //List<string> FuntionsForAgents = new List<string>() { "020203005", "020205001", "020205002", "020205003", "020205004", "020205005" };

                    List<string> FuntionsForAgents = new List<string>();

                    FuntionsForAgents = Obj_DBModelEntities.LG_USER_CLASS_AREA_FUNCTION
                                       .Where(t => t.CLASSIFICATION_ID == "3" &&
                                                   t.AREA_ID == "7")
                                       .Select(t => t.FUNCTION_ID).ToList();

                    foreach (var func in FuntionsForAgents.ToList())
                    {
                        foreach (var item in LIST_LG_FNR_FUNCTION_MAP.ToList())
                        {
                            if (func == item.FUNCTION_ID)
                            {
                                FuntionsForAgents.Remove(func);
                                LIST_LG_FNR_FUNCTION_MAP.Remove(item);
                                break;
                            }
                        }
                    }
                    return LIST_LG_FNR_FUNCTION_MAP;
                }

                return LIST_LG_FNR_FUNCTION_MAP;
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
                        OBJ_LG_FNR_FUNCTION_MAP.ERROR = validationError.ErrorMessage;
                        LIST_LG_FNR_FUNCTION_MAP.Add(OBJ_LG_FNR_FUNCTION_MAP);
                    }
                }
                LG_ERROR_LOG_MAP.Add_Error_Log(FUNC_ID, dbEx.Source, "ERR_APP_TYPE", "GetAllFunctionsByAppId",
                                          "0000000000", dbEx.Message, inner, dbEx.StackTrace, null, dateTime);
                return LIST_LG_FNR_FUNCTION_MAP;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);
                LG_ERROR_LOG_MAP.Add_Error_Log(FUNC_ID, ex.Source, "ERR_APP_TYPE", "GetAllFunctionsByAppId",
                                          "0000000000", ex.Message, inner4, ex.StackTrace, null, dateTime);
                OBJ_LG_FNR_FUNCTION_MAP.ERROR = ex.Message;
                LIST_LG_FNR_FUNCTION_MAP.Add(OBJ_LG_FNR_FUNCTION_MAP);
                return LIST_LG_FNR_FUNCTION_MAP;
            }
        }

        public static IEnumerable<LG_FNR_FUNCTION_MAP> GetAllFunctions_ByFunctionGroupId(string pAPPLICATION_ID, string pGROUP_ID)
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            List<LG_FNR_FUNCTION_MAP> LIST_LG_FNR_FUNCTION_MAP = new List<LG_FNR_FUNCTION_MAP>();
            LG_FNR_FUNCTION_MAP OBJ_LG_FNR_FUNCTION_MAP = new LG_FNR_FUNCTION_MAP();

            try
            {
                Obj_DBModelEntities.Configuration.LazyLoadingEnabled = false;
                Obj_DBModelEntities.Configuration.ProxyCreationEnabled = false;

                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                int group_id = !string.IsNullOrWhiteSpace(pGROUP_ID) ? Convert.ToInt32(pGROUP_ID) : 0;

                LIST_LG_FNR_FUNCTION_MAP = (from f in Obj_DBModelEntities.LG_FNR_FUNCTION
                                            join g in Obj_DBModelEntities.LG_FNR_FUNCTION_GROUP
                                            on f.FUNCTION_ID equals g.FUNCTION_ID
                                            where f.LAST_ACTION != "DEL" &&
                                                  f.AUTH_STATUS_ID == "A" &&
                                                  f.APPLICATION_ID == pAPPLICATION_ID &&
                                                  g.GROUP_ID == group_id
                                            orderby f.FUNCTION_ID ascending
                                            select new LG_FNR_FUNCTION_MAP
                                            {
                                                FUNCTION_ID = f.FUNCTION_ID,
                                                FUNCTION_NM = f.FUNCTION_NM,
                                                MAINT_AUTH_FLAG = f.MAINT_AUTH_FLAG,
                                                AUTH_LEVEL = f.AUTH_LEVEL,
                                                AUTH_STATUS_ID = f.AUTH_STATUS_ID
                                            }).ToList();

                return LIST_LG_FNR_FUNCTION_MAP;
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
                        OBJ_LG_FNR_FUNCTION_MAP.ERROR = validationError.ErrorMessage;
                        LIST_LG_FNR_FUNCTION_MAP.Add(OBJ_LG_FNR_FUNCTION_MAP);
                    }
                }
                LG_ERROR_LOG_MAP.Add_Error_Log(FUNC_ID, dbEx.Source, "ERR_APP_TYPE", "GetAllFunctions_ByFunctionGroupId",
                                          "0000000000", dbEx.Message, inner, dbEx.StackTrace, null, dateTime);
                return LIST_LG_FNR_FUNCTION_MAP;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);
                LG_ERROR_LOG_MAP.Add_Error_Log(FUNC_ID, ex.Source, "ERR_APP_TYPE", "GetAllFunctions_ByFunctionGroupId",
                                          "0000000000", ex.Message, inner4, ex.StackTrace, null, dateTime);
                OBJ_LG_FNR_FUNCTION_MAP.ERROR = ex.Message;
                LIST_LG_FNR_FUNCTION_MAP.Add(OBJ_LG_FNR_FUNCTION_MAP);
                return LIST_LG_FNR_FUNCTION_MAP;
            }
        }

        public static int? GetAppTypeByAppId(string pAPPLICATION_ID)
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            try
            {
                Obj_DBModelEntities.Configuration.LazyLoadingEnabled = false;
                Obj_DBModelEntities.Configuration.ProxyCreationEnabled = false;

                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                var app_type_id = Obj_DBModelEntities.LG_FNR_APPLICATION.Where(a => a.APPLICATION_ID == pAPPLICATION_ID).Select(a => a.APP_TYPE_ID).SingleOrDefault();
                return app_type_id;
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
                LG_ERROR_LOG_MAP.Add_Error_Log(FUNC_ID, dbEx.Source, "ERR_APP_TYPE", "GetAppTypeByAppId",
                                          "0000000000", dbEx.Message, inner, dbEx.StackTrace, null, dateTime);
                return 0;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);
                LG_ERROR_LOG_MAP.Add_Error_Log(FUNC_ID, ex.Source, "ERR_APP_TYPE", "GetAppTypeByAppId",
                                          "0000000000", ex.Message, inner4, ex.StackTrace, null, dateTime);
                return 0;
            }
        }

        public static string GetAuthPermissionByFunctionId(string pFUNCTION_ID, string Function_Name)
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            LG_FNR_FUNCTION_MAP OBJ_LG_FNR_FUNCTION_MAP = new LG_FNR_FUNCTION_MAP();
            string vMsg = "null";

            try
            {
                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                if (pFUNCTION_ID != "null")
                {
                    OBJ_LG_FNR_FUNCTION_MAP = (from f in Obj_DBModelEntities.LG_FNR_FUNCTION
                                               where f.FUNCTION_ID == pFUNCTION_ID
                                               select new LG_FNR_FUNCTION_MAP
                                               {
                                                   FUNCTION_ID = f.FUNCTION_ID,
                                                   MAINT_AUTH_FLAG = f.MAINT_AUTH_FLAG,
                                                   AUTH_LEVEL = f.AUTH_LEVEL,
                                               }).SingleOrDefault();
                }
                else if (Function_Name != "null")
                {
                    OBJ_LG_FNR_FUNCTION_MAP = (from f in Obj_DBModelEntities.LG_FNR_FUNCTION
                                               where f.FUNCTION_NM == Function_Name
                                               select new LG_FNR_FUNCTION_MAP
                                               {
                                                   FUNCTION_ID = f.FUNCTION_ID,
                                                   MAINT_AUTH_FLAG = f.MAINT_AUTH_FLAG,
                                                   AUTH_LEVEL = f.AUTH_LEVEL,
                                               }).SingleOrDefault();
                }

                if (OBJ_LG_FNR_FUNCTION_MAP != null)
                {
                    vMsg = OBJ_LG_FNR_FUNCTION_MAP.MAINT_AUTH_FLAG.ToString();
                }

                return vMsg;
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
                        OBJ_LG_FNR_FUNCTION_MAP.ERROR = validationError.ErrorMessage;
                    }
                }
                LG_ERROR_LOG_MAP.Add_Error_Log(pFUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "GetAuthPermissionByFunctionId",
                                               "0000000000", dbEx.Message, inner, dbEx.StackTrace, null, dateTime);
                return vMsg;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);
                LG_ERROR_LOG_MAP.Add_Error_Log(pFUNCTION_ID, ex.Source, "ERR_APP_TYPE", "GetAuthPermissionByFunctionId",
                                               "0000000000", ex.Message, inner4, ex.StackTrace, null, dateTime);

                OBJ_LG_FNR_FUNCTION_MAP.ERROR = ex.Message;
                return vMsg;
            }
        }

        #endregion custom methods

        public static LG_FNR_FUNCTION_MAP GetPermittedFunctionsListByUser(string pUSER_ID, string pAPP_ID, string pFUNCTION_GROUP_ID)
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            LG_FNR_FUNCTION_MAP OBJ_LG_FNR_FUNCTION_MAP = new LG_FNR_FUNCTION_MAP();
            try
            {
                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);
                var obj_lg_user_setup_profile = (from u in Obj_DBModelEntities.LG_USER_SETUP_PROFILE
                                                 where u.USER_ID == pUSER_ID &&
                                                       u.AUTH_STATUS_ID != "U" &&
                                                       u.LAST_ACTION != "DEL"
                                                 select new LG_USER_SETUP_PROFILE_MAP()
                                                 {
                                                     USER_ID = u.USER_ID,
                                                     USER_NAME = u.USER_NM,
                                                 });
                if (obj_lg_user_setup_profile == null)
                {
                    return null;
                }

                var ROLES = (from role_a in Obj_DBModelEntities.LG_USER_ROLE_ASSIGN
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

                if (app_type == 2 && ROLES.Count() > 0)
                {
                    List<LG_FNR_FUNCTION_MAP> LIST_LG_FNR_FUNCTION_MAP = new List<LG_FNR_FUNCTION_MAP>();
                    bool exists1, first_time1 = true;
                    if (pFUNCTION_GROUP_ID == "3")
                    {
                        foreach (LG_USER_ROLE_ASSIGN_MAP OBJ_LG_USER_ROLE_ASSIGN_MAP in ROLES)
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
                                                                  menu.MENU_ENABLE_FLAG == 1 &&
                                                                  role_d.MAINT_INDX_FLAG == 1 && (role_d.FUNCTION_ID.StartsWith("00") || role_d.FUNCTION_ID.StartsWith("04") || role_d.FUNCTION_ID.StartsWith("50") || role_d.FUNCTION_ID.StartsWith("54"))
                                                            select new LG_FNR_FUNCTION_MAP
                                                            {
                                                                //MENU_ID = menu.MENU_ID,
                                                                ITEM_TYPE = f.ITEM_TYPE,
                                                                //MENU_NM = menu.NAME,
                                                                //MENU_LEVEL = menu.MENU_LEVEL,
                                                                //PARENT_MENU_ID = menu.PARENTID,
                                                                //FUNCTION_ASSIGN_FLAG = menu.FUNCTION_ASSIGN_FLAG,
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
                                OBJ_LG_FNR_FUNCTION_MAP.GetPermittedFunctionsList = List_lg_fnr_function_map;
                            }

                            //From Second time if function doesn't exists to List then add
                            if (first_time1 == false && List_lg_fnr_function_map.Count() > 0)
                            {
                                for (int i = 0; i < List_lg_fnr_function_map.Count(); i++)
                                {
                                    exists1 = false;
                                    for (int j = 0; j < OBJ_LG_FNR_FUNCTION_MAP.GetPermittedFunctionsList.Count(); j++)
                                    {
                                        if (List_lg_fnr_function_map[i].FUNCTION_ID == OBJ_LG_FNR_FUNCTION_MAP.GetPermittedFunctionsList[j].FUNCTION_ID)  //Bug for AuditTrail as same name exists in 2 application
                                        {
                                            exists1 = true;
                                            break;
                                        }
                                    }
                                    if (!exists1)
                                    {
                                        OBJ_LG_FNR_FUNCTION_MAP.GetPermittedFunctionsList.Add(List_lg_fnr_function_map[i]);
                                    }
                                }
                            }
                            if (first_time1 == true && List_lg_fnr_function_map.Count > 0)
                            {
                                first_time1 = false;
                            }
                        }

                        //var List_Lg_Parent_Menu = (from menu in Obj_DBModelEntities.LG_MENU
                        //                                where menu.FUNCTION_ASSIGN_FLAG == 0
                        //                                select new LG_FNR_FUNCTION_MAP
                        //                                {
                        //                                    MENU_ID = menu.MENU_ID,
                        //                                    ITEM_TYPE = null,
                        //                                    MENU_NM = menu.NAME,
                        //                                    MENU_LEVEL = menu.MENU_LEVEL,
                        //                                    PARENT_MENU_ID = menu.PARENTID,
                        //                                    FUNCTION_ASSIGN_FLAG = menu.FUNCTION_ASSIGN_FLAG,
                        //                                    FAST_PATH_NO = null,
                        //                                    FUNCTION_ID = null,
                        //                                    FUNCTION_NM = null,
                        //                                    HO_FUNCTION_FLAG = 0,
                        //                                    TARGET_PATH = null,
                        //                                    DB_ROLE_NAME = null,
                        //                                    SERVICE_ID = null,
                        //                                    MODULE_ID = null,
                        //                                    MAINT_CRT_FLAG = 0, //create == add
                        //                                    MAINT_EDT_FLAG = 0,
                        //                                    MAINT_DEL_FLAG = 0,
                        //                                    MAINT_INDX_FLAG = 0, // index == view
                        //                                    MAINT_AUTH_FLAG = 0,
                        //                                    PROCESS_FLAG = 0,
                        //                                    REPORT_VIEW_FLAG = 0,
                        //                                    REPORT_PRINT_FLAG = 0,
                        //                                    REPORT_GEN_FLAG = 0
                        //                                }).ToList();

                        //OBJ_LG_FNR_FUNCTION_MAP.GetPermittedFunctionsList.AddRange(List_Lg_Parent_Menu);

                        #region raf

                        int k = 0;
                        List<LG_MENU_TEMP> List_OBJ_LG_MENU_TEMP = new List<LG_MENU_TEMP>();
                        foreach (var item in OBJ_LG_FNR_FUNCTION_MAP.GetPermittedFunctionsList)
                        {
                            LG_MENU_TEMP OBJ_LG_MENU_TEMP = new LG_MENU_TEMP();
                            OBJ_LG_MENU_TEMP.MENU_ID = k++; //item.MENU_ID??0;
                            OBJ_LG_MENU_TEMP.ITEM_TYPE = item.ITEM_TYPE;
                            OBJ_LG_MENU_TEMP.MENU_NM = item.MENU_NM;
                            OBJ_LG_MENU_TEMP.MENU_LEVEL = item.MENU_LEVEL;
                            OBJ_LG_MENU_TEMP.PARENT_MENU_ID = item.PARENT_MENU_ID;
                            OBJ_LG_MENU_TEMP.FUNCTION_ASSIGN_FLAG = item.FUNCTION_ASSIGN_FLAG;
                            OBJ_LG_MENU_TEMP.FIRST_PATH_NO = item.FAST_PATH_NO;
                            OBJ_LG_MENU_TEMP.FUNCTION_ID = item.FUNCTION_ID;
                            OBJ_LG_MENU_TEMP.FUNCTION_NM = item.FUNCTION_NM;
                            OBJ_LG_MENU_TEMP.HO_FUNCTION_FLAG = item.HO_FUNCTION_FLAG;
                            OBJ_LG_MENU_TEMP.TARGET_PATH = item.TARGET_PATH;
                            OBJ_LG_MENU_TEMP.DB_ROLE_NAME = item.DB_ROLE_NAME;
                            OBJ_LG_MENU_TEMP.SERVICE_ID = item.SERVICE_ID;
                            OBJ_LG_MENU_TEMP.MODULE_ID = item.MODULE_ID;
                            OBJ_LG_MENU_TEMP.ALLOW_MAINT_ADD_FLAG = item.MAINT_CRT_FLAG;
                            OBJ_LG_MENU_TEMP.ALLOW_MAINT_EDIT_FLAG = item.MAINT_EDT_FLAG;
                            OBJ_LG_MENU_TEMP.ALLOW_MAINT_DEL_FLAG = item.MAINT_DEL_FLAG;
                            OBJ_LG_MENU_TEMP.ALLOW_MAINT_VIEW_FLAG = item.MAINT_INDX_FLAG;
                            OBJ_LG_MENU_TEMP.ALLOW_MAINT_AUTH_FLAG = item.MAINT_AUTH_FLAG;
                            OBJ_LG_MENU_TEMP.ALLOW_PROCESS_FLAG = item.PROCESS_FLAG;
                            OBJ_LG_MENU_TEMP.ALLOW_REPORT_VIEW_FLAG = item.REPORT_VIEW_FLAG;
                            OBJ_LG_MENU_TEMP.ALLOW_REPORT_PRINT_FLAG = item.REPORT_PRINT_FLAG;
                            OBJ_LG_MENU_TEMP.ALLOW_REPORT_GEN_FLAG = item.REPORT_GEN_FLAG;
                            OBJ_LG_MENU_TEMP.HELP_PATH = string.Empty;

                            //List_OBJ_LG_MENU_TEMP.Add(OBJ_LG_MENU_TEMP);
                            Obj_DBModelEntities.LG_MENU_TEMP.Add(OBJ_LG_MENU_TEMP);
                        }
                        Obj_DBModelEntities.SaveChanges();

                        #endregion raf
                    }
                    else
                    {
                        bool exists, first_time = true;
                        foreach (LG_USER_ROLE_ASSIGN_MAP OBJ_LG_USER_ROLE_ASSIGN_MAP in ROLES)
                        {
                            var List_lg_fnr_function_map = (from f in Obj_DBModelEntities.LG_FNR_FUNCTION
                                                            join menu in Obj_DBModelEntities.LG_MENU
                                                            on f.FUNCTION_ID equals menu.FUNCTION_ID
                                                            where f.APPLICATION_ID == pAPP_ID && 
                                                                  //f.ENABLED_FLAG == 1 &&   //salekin commented
                                                                  f.AUTH_STATUS_ID != "U" && f.LAST_ACTION != "DEL" &&
                                                                  f.FUNCTION_GROUP_ID == pFUNCTION_GROUP_ID &&
                                                                  menu.MENU_ENABLE_FLAG == 1
                                                            orderby menu.MENU_ID ascending
                                                            select new LG_FNR_FUNCTION_MAP
                                                            {
                                                                MENU_ID = menu.MENU_ID,
                                                                ITEM_TYPE = f.ITEM_TYPE,
                                                                MENU_NM = menu.NAME,
                                                                MENU_LEVEL = menu.MENU_LEVEL,
                                                                PARENT_MENU_ID = menu.PARENTID,
                                                                FUNCTION_ASSIGN_FLAG = menu.FUNCTION_ASSIGN_FLAG,
                                                                FAST_PATH_NO = f.FAST_PATH_NO,
                                                                FUNCTION_ID = f.FUNCTION_ID,
                                                                FUNCTION_NM = f.FUNCTION_NM,
                                                                HO_FUNCTION_FLAG = f.HO_FUNCTION_FLAG,
                                                                TARGET_PATH = f.TARGET_PATH,
                                                                DB_ROLE_NAME = f.DB_ROLE_NAME,
                                                                SERVICE_ID = f.SERVICE_ID,
                                                                MODULE_ID = f.MODULE_ID,
                                                                MAINT_CRT_FLAG = f.MAINT_CRT_FLAG, //create == add
                                                                MAINT_EDT_FLAG = f.MAINT_EDT_FLAG,
                                                                MAINT_DEL_FLAG = f.MAINT_DEL_FLAG,
                                                                MAINT_INDX_FLAG = f.MAINT_INDX_FLAG, // index == view
                                                                MAINT_AUTH_FLAG = f.MAINT_AUTH_FLAG,
                                                                PROCESS_FLAG = f.PROCESS_FLAG,
                                                                REPORT_VIEW_FLAG = f.REPORT_VIEW_FLAG,
                                                                REPORT_PRINT_FLAG = f.REPORT_PRINT_FLAG,
                                                                REPORT_GEN_FLAG = f.REPORT_GEN_FLAG
                                                            }).ToList();

                            //First time all function will be added to List
                            if (first_time == true && List_lg_fnr_function_map.Count > 0)
                            {
                                OBJ_LG_FNR_FUNCTION_MAP.GetPermittedFunctionsList = List_lg_fnr_function_map;
                            }

                            //From Second time if function doesn't exists to List then add
                            if (first_time == false && List_lg_fnr_function_map.Count() > 0)
                            {
                                for (int i = 0; i < List_lg_fnr_function_map.Count(); i++)
                                {
                                    exists = false;
                                    for (int j = 0; j < OBJ_LG_FNR_FUNCTION_MAP.GetPermittedFunctionsList.Count(); j++)
                                    {
                                        if (List_lg_fnr_function_map[i].FUNCTION_ID == OBJ_LG_FNR_FUNCTION_MAP.GetPermittedFunctionsList[j].FUNCTION_ID)  //Bug for AuditTrail as same name exists in 2 application
                                        {
                                            exists = true;
                                            break;
                                        }
                                    }
                                    if (!exists)
                                    {
                                        OBJ_LG_FNR_FUNCTION_MAP.GetPermittedFunctionsList.Add(List_lg_fnr_function_map[i]);
                                    }
                                }
                            }
                            if (first_time == true && List_lg_fnr_function_map.Count > 0)
                            {
                                first_time = false;
                            }
                        }
                        var List_Lg_Parent_Menu1 = (from menu in Obj_DBModelEntities.LG_MENU
                                                    where menu.FUNCTION_ASSIGN_FLAG == 0 &&
                                                          menu.MENU_ENABLE_FLAG == 1
                                                    select new LG_FNR_FUNCTION_MAP
                                                    {
                                                        MENU_ID = menu.MENU_ID,
                                                        ITEM_TYPE = null,
                                                        MENU_NM = menu.NAME,
                                                        MENU_LEVEL = menu.MENU_LEVEL,
                                                        PARENT_MENU_ID = menu.PARENTID,
                                                        FUNCTION_ASSIGN_FLAG = menu.FUNCTION_ASSIGN_FLAG,
                                                        FAST_PATH_NO = null,
                                                        FUNCTION_ID = null,
                                                        FUNCTION_NM = null,
                                                        HO_FUNCTION_FLAG = 0,
                                                        TARGET_PATH = null,
                                                        DB_ROLE_NAME = null,
                                                        SERVICE_ID = null,
                                                        MODULE_ID = null,
                                                        MAINT_CRT_FLAG = 0, //create == add
                                                        MAINT_EDT_FLAG = 0,
                                                        MAINT_DEL_FLAG = 0,
                                                        MAINT_INDX_FLAG = 0, // index == view
                                                        MAINT_AUTH_FLAG = 0,
                                                        PROCESS_FLAG = 0,
                                                        REPORT_VIEW_FLAG = 0,
                                                        REPORT_PRINT_FLAG = 0,
                                                        REPORT_GEN_FLAG = 0
                                                    }).ToList();

                        OBJ_LG_FNR_FUNCTION_MAP.GetPermittedFunctionsList.AddRange(List_Lg_Parent_Menu1);
                    }
                }

                #endregion For Web Form Applications

                return OBJ_LG_FNR_FUNCTION_MAP;
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

                LG_ERROR_LOG_MAP.Add_Error_Log(null, dbEx.Source, "ERR_APP_TYPE", "GetPermittedFunctionsListByUser",
                                       "0000000000", dbEx.Message, inner, dbEx.StackTrace, "RequestedUserId:" + pUSER_ID, dateTime);

                return OBJ_LG_FNR_FUNCTION_MAP;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                LG_ERROR_LOG_MAP.Add_Error_Log(null, ex.Source, "ERR_APP_TYPE", "GetPermittedFunctionsListByUser",
                                       "0000000000", ex.Message, inner4, ex.StackTrace, "RequestedUserId:" + pUSER_ID, dateTime);
                return OBJ_LG_FNR_FUNCTION_MAP;
            }
        }

        #endregion Events
    }
}