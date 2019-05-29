using Model.EDMX;
using Model.EntityModel.Common;
using Model.Mapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
    public class LG_FNR_ROLE_DEFINE_MAP
    {
        #region Properties
        [DataMember]
        public string ROLE_ID { get; set; }
        [DataMember]
        public string ROLE_NM { get; set; }
        [DataMember]
        public string FUNCTION_ID { get; set; }
        [DataMember]
        public string FUNCTION_NM { get; set; }
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
        public short? MAINT_OTP_FLAG { get; set; }
        [DataMember]
        public short? MAINT_2FA_FLAG { get; set; }
        [DataMember]
        public short? MAINT_2FA_HARD_FLAG { get; set; }
        [DataMember]
        public short? MAINT_2FA_SOFT_FLAG { get; set; }
        [DataMember]
        public short REPORT_VIEW_FLAG { get; set; }
        [DataMember]
        public short REPORT_PRINT_FLAG { get; set; }
        [DataMember]
        public short REPORT_GEN_FLAG { get; set; }
        [DataMember]
        public string MAKE_BY { get; set; }
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public DateTime? MAKE_DT { get; set; }
        [DataMember]
        public string FUNCTION_IDs_FOR_IND_ROLE { get; set; }
        [DataMember]
        public string AUTH_STATUS_ID { get; set; }
        [DataMember]
        public string LAST_ACTION { get; set; }
        [DataMember]
        public DateTime? LAST_UPDATE_DT { get; set; }
        [DataMember]
        public int? AUTH_LEVEL { get; set; }
        [DataMember]
        public string ROLE_DESCRIP { get; set; }
        [DataMember]
        public string ROLE_DEFINE_COMMAND { get; set; }


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
        [DataMember]
        public List<LG_FNR_ROLE_DEFINE_MAP> LIST_SELECTED_FUNCTION_DETAILS { get; set; }


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
        public string APPLICATION_ID { get; set; }
        [DataMember]
        public string SERVICE_ID { get; set; }
        [DataMember]
        public string MODULE_ID { get; set; }
        [DataMember]
        public string FUNCTION_GROUP_ID { get; set; }
        [DataMember]
        public string ITEM_TYPE { get; set; }
        [DataMember]
        public string ERROR { get; set; }

        [DataMember]
        public string APPLICATION_NAME { get; set; }
        [DataMember]
        public string SERVICE_NM { get; set; }
        [DataMember]
        public string MODULE_NM { get; set; }
        [DataMember]
        public string FUNCTION_GROUP_NAME { get; set; }

        //[DataMember]
        //public virtual ICollection<LG_FNR_ROLE_PERMISSION_DETAILS_MAP> PERMISSIONS { get; set; }
        //[DataMember]
        //public virtual ICollection<LG_USER_SETUP_PROFILE_MAP> USERS { get; set; }

        #endregion


        #region Events
        public static string FUNC_ID = "010101006";
        public static DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
        #region Add New
        public static string AddRoleDefine(LG_FNR_ROLE_DEFINE_MAP pLG_FNR_ROLE_DEFINE_MAP)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                LG_FNR_ROLE_DEFINE_MAP OBJ_LG_FNR_ROLE_DEFINE_MAP = new LG_FNR_ROLE_DEFINE_MAP();
                LG_FNR_ROLE_DEFINE OBJ_LG_FNR_ROLE_DEFINE = new LG_FNR_ROLE_DEFINE();
                string result = string.Empty;
                try
                {
                    string format = OutgoingResponseFormat.GetFormat();
                    OutgoingResponseFormat.SetResponseFormat(format);

                    if (pLG_FNR_ROLE_DEFINE_MAP.ROLE_DEFINE_COMMAND == "Save" && (Obj_DBModelEntities.LG_FNR_ROLE_DEFINE.Where(r => r.ROLE_ID == pLG_FNR_ROLE_DEFINE_MAP.ROLE_ID).ToList().Count() > 0))
                    {
                        return "Role already defined.";
                    }

                    foreach (LG_FNR_ROLE_DEFINE_MAP ITEM_LG_FNR_ROLE_DEFINE_MAP in pLG_FNR_ROLE_DEFINE_MAP.LIST_SELECTED_FUNCTION_DETAILS)
                    {
                        int serial_no = Convert.ToInt32(Obj_DBModelEntities.LG_FNR_ROLE_DEFINE
                                       .Max(x => (int?)x.SL_NO) ?? 0) + 1;

                        OBJ_LG_FNR_ROLE_DEFINE = new LG_FNR_ROLE_DEFINE();
                        Class_Conversion.LG_FNR_ROLE_DEFINE_CON(OBJ_LG_FNR_ROLE_DEFINE, ITEM_LG_FNR_ROLE_DEFINE_MAP);
                        OBJ_LG_FNR_ROLE_DEFINE.SL_NO = serial_no;
                        OBJ_LG_FNR_ROLE_DEFINE.ROLE_ID = pLG_FNR_ROLE_DEFINE_MAP.ROLE_ID;
                        OBJ_LG_FNR_ROLE_DEFINE.AUTH_STATUS_ID = "U";
                        OBJ_LG_FNR_ROLE_DEFINE.LAST_ACTION = "ADD";
                        OBJ_LG_FNR_ROLE_DEFINE.MAKE_DT = System.DateTime.Now;
                        OBJ_LG_FNR_ROLE_DEFINE.ROLE_DEFINE_FLAG = 1;
                        //OBJ_LG_FNR_ROLE_DEFINE.APPLICATION_ID = ITEM_LG_FNR_ROLE_DEFINE_MAP.APPLICATION_ID;
                        Obj_DBModelEntities.LG_FNR_ROLE_DEFINE.Add(OBJ_LG_FNR_ROLE_DEFINE);
                        Obj_DBModelEntities.SaveChanges();


                        #region Add in "RolePermission" Table
                        int serial_id = (Obj_DBModelEntities.LG_FNR_ROLE_PERMISSION
                                        .Select(i => i.SL_ID).Cast<int?>().Max() ?? 0);

                        if (ITEM_LG_FNR_ROLE_DEFINE_MAP.MAINT_CRT_FLAG == 1)
                        {
                            serial_id++;
                            string permission_details = ITEM_LG_FNR_ROLE_DEFINE_MAP.FUNCTION_NM + "-" + "Create";
                            AddRolePermission(serial_id, pLG_FNR_ROLE_DEFINE_MAP.ROLE_ID, ITEM_LG_FNR_ROLE_DEFINE_MAP.FUNCTION_ID, permission_details);
                        }
                        if (ITEM_LG_FNR_ROLE_DEFINE_MAP.MAINT_EDT_FLAG == 1)
                        {
                            serial_id++;
                            string permission_details = ITEM_LG_FNR_ROLE_DEFINE_MAP.FUNCTION_NM + "-" + "Edit";
                            AddRolePermission(serial_id, pLG_FNR_ROLE_DEFINE_MAP.ROLE_ID, ITEM_LG_FNR_ROLE_DEFINE_MAP.FUNCTION_ID, permission_details);
                        }
                        if (ITEM_LG_FNR_ROLE_DEFINE_MAP.MAINT_DEL_FLAG == 1)
                        {
                            serial_id++;
                            string permission_details = ITEM_LG_FNR_ROLE_DEFINE_MAP.FUNCTION_NM + "-" + "Delete";
                            AddRolePermission(serial_id, pLG_FNR_ROLE_DEFINE_MAP.ROLE_ID, ITEM_LG_FNR_ROLE_DEFINE_MAP.FUNCTION_ID, permission_details);
                        }
                        if (ITEM_LG_FNR_ROLE_DEFINE_MAP.MAINT_DTL_FLAG == 1)
                        {
                            serial_id++;
                            string permission_details = ITEM_LG_FNR_ROLE_DEFINE_MAP.FUNCTION_NM + "-" + "Details";
                            AddRolePermission(serial_id, pLG_FNR_ROLE_DEFINE_MAP.ROLE_ID, ITEM_LG_FNR_ROLE_DEFINE_MAP.FUNCTION_ID, permission_details);
                        }
                        if (ITEM_LG_FNR_ROLE_DEFINE_MAP.MAINT_INDX_FLAG == 1)
                        {
                            serial_id++;
                            string permission_details = ITEM_LG_FNR_ROLE_DEFINE_MAP.FUNCTION_NM + "-" + "Index";
                            AddRolePermission(serial_id, pLG_FNR_ROLE_DEFINE_MAP.ROLE_ID, ITEM_LG_FNR_ROLE_DEFINE_MAP.FUNCTION_ID, permission_details);
                        }
                        #endregion


                        #region Auth log
                        LG_AA_NFT_AUTH_LOG_MAP OBJ_LG_AA_NFT_AUTH_LOG_MAP = new LG_AA_NFT_AUTH_LOG_MAP();
                        FUNC_ID = OBJ_LG_AA_NFT_AUTH_LOG_MAP.FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "RoleDefine").Select(x => x.FUNCTION_ID).SingleOrDefault();
                        OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_NAME = "LG_FNR_ROLE_DEFINE";
                        OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_NM = "SL_NO";
                        OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_VAL = OBJ_LG_FNR_ROLE_DEFINE.SL_NO.ToString();
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
                        OBJ_LG_AA_NFT_AUTH_LOG_MAP.MAKE_BY = pLG_FNR_ROLE_DEFINE_MAP.MAKE_BY;
                        OBJ_LG_AA_NFT_AUTH_LOG_MAP.MAKE_DT = DateTime.Now;
                        LG_AA_NFT_AUTH_LOG_MAP.AddNftAuthLog(OBJ_LG_FNR_ROLE_DEFINE, OBJ_LG_AA_NFT_AUTH_LOG_MAP);
                        #endregion
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
                            result = "Can't Add Role Define(Db) " + validationError.ErrorMessage;
                        }
                    }
                    LG_ERROR_LOG_MAP.Add_Error_Log(FUNC_ID, dbEx.Source, "ERR_APP_TYPE", "AddRoleDefine",
                                              "0000000000", dbEx.Message, inner, dbEx.StackTrace, null, dateTime);
                    return result;
                }
                catch (Exception ex)
                {
                    ts.Dispose();
                    string dateTime = Convert.ToString(System.DateTime.Now);
                    string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                    LG_ERROR_LOG_MAP.Add_Error_Log(FUNC_ID, ex.Source, "ERR_APP_TYPE", "AddRoleDefine",
                                              "0000000000", ex.Message, inner4, ex.StackTrace, null, dateTime);
                    result = "Can't Add Role Define " + ex.Message;
                    return result;
                }
            }
        }
        #endregion

        #region Update
        public static string UpdateRoleDefine(LG_FNR_ROLE_DEFINE_MAP pLG_FNR_ROLE_DEFINE_MAP)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                //LG_FNR_ROLE_DEFINE_MAP OBJ_OLD_LG_FNR_ROLE_DEFINE_MAP = new LG_FNR_ROLE_DEFINE_MAP();
                LG_FNR_ROLE_DEFINE_MAP ITEM_NEW_LIST_LG_FNR_ROLE_DEFINE_MAP = new LG_FNR_ROLE_DEFINE_MAP();
                LG_FNR_ROLE_DEFINE_MAP OBJ_NEW_LG_FNR_ROLE_DEFINE_MAP = new LG_FNR_ROLE_DEFINE_MAP();
                List<LG_FNR_ROLE_DEFINE_MAP> LIST_NEW_SELECTED_FUNCTION_DETAILS = new List<LG_FNR_ROLE_DEFINE_MAP>();
                List<LG_FNR_ROLE_DEFINE> LIST_OLD_REMOVED_FUNCTION_DETAILS = new List<LG_FNR_ROLE_DEFINE>();
                string result = string.Empty;
                //string permission_details = string.Empty;
                bool exists;
                try
                {
                    FUNC_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "RoleDefine").Select(x => x.FUNCTION_ID).SingleOrDefault();
                    string format = OutgoingResponseFormat.GetFormat();
                    OutgoingResponseFormat.SetResponseFormat(format);


                    List<LG_FNR_ROLE_DEFINE> OLD_LIST_LG_FNR_ROLE_DEFINE = Obj_DBModelEntities.LG_FNR_ROLE_DEFINE
                                                                          .Where(role_d => role_d.ROLE_ID == pLG_FNR_ROLE_DEFINE_MAP.ROLE_ID && role_d.ROLE_DEFINE_FLAG == 1).ToList();

                    List<LG_FNR_ROLE_PERMISSION_MAP> LIST_LG_FNR_ROLE_PERMISSION_MAP = (from role_per in Obj_DBModelEntities.LG_FNR_ROLE_PERMISSION
                                                                                        join role_per_dtl in Obj_DBModelEntities.LG_FNR_ROLE_PERMISSION_DETAILS
                                                                                        on role_per.PERMISSION_ID equals role_per_dtl.PERMISSION_ID
                                                                                        where role_per.ROLE_ID == pLG_FNR_ROLE_DEFINE_MAP.ROLE_ID
                                                                                        select new LG_FNR_ROLE_PERMISSION_MAP
                                                                                        {
                                                                                            ROLE_ID = role_per.ROLE_ID,
                                                                                            PERMISSION_ID = role_per.PERMISSION_ID,
                                                                                            PERMISSION_DETAILS = role_per_dtl.PERMISSION_DETAILS
                                                                                        }).ToList();

                    List<LG_FNR_ROLE_DEFINE> OLD_INACTIVE_LIST_LG_FNR_ROLE_DEFINE = Obj_DBModelEntities.LG_FNR_ROLE_DEFINE
                                                                                   .Where(role_d => role_d.ROLE_ID == pLG_FNR_ROLE_DEFINE_MAP.ROLE_ID && 
                                                                                                    role_d.LAST_ACTION != "DEL" && 
                                                                                                    role_d.AUTH_STATUS_ID == "A" && 
                                                                                                    role_d.ROLE_DEFINE_FLAG == 0).ToList();


                    foreach (LG_FNR_ROLE_DEFINE ITEM_OLD_LIST_LG_FNR_ROLE_DEFINE in OLD_LIST_LG_FNR_ROLE_DEFINE.ToList()) //after completing 2 foreach loop iteration, the remaining "old list" will be the deleted functions
                    {
                        exists = false;
                        foreach (LG_FNR_ROLE_DEFINE_MAP ITEM_NEW_LIST_LG_FNR_ROLE_DEFINE_MAP1 in pLG_FNR_ROLE_DEFINE_MAP.LIST_SELECTED_FUNCTION_DETAILS) //after completing 2 foreach loop iteration, the remaining "selected list" will be the New selected functions
                        {
                            if (ITEM_OLD_LIST_LG_FNR_ROLE_DEFINE.FUNCTION_ID == ITEM_NEW_LIST_LG_FNR_ROLE_DEFINE_MAP1.FUNCTION_ID)
                            {
                                exists = true;
                                ITEM_NEW_LIST_LG_FNR_ROLE_DEFINE_MAP = ITEM_NEW_LIST_LG_FNR_ROLE_DEFINE_MAP1;
                                var itemToRemove = pLG_FNR_ROLE_DEFINE_MAP.LIST_SELECTED_FUNCTION_DETAILS.Single(r => r.FUNCTION_ID == ITEM_NEW_LIST_LG_FNR_ROLE_DEFINE_MAP1.FUNCTION_ID);
                                pLG_FNR_ROLE_DEFINE_MAP.LIST_SELECTED_FUNCTION_DETAILS.Remove(itemToRemove); //Minimizing the List if true, so that foreach loop will iterate less
                                var itemToRemove1 = OLD_LIST_LG_FNR_ROLE_DEFINE.Single(r => r.FUNCTION_ID == ITEM_OLD_LIST_LG_FNR_ROLE_DEFINE.FUNCTION_ID);
                                OLD_LIST_LG_FNR_ROLE_DEFINE.Remove(itemToRemove1);
                                break;
                            }
                        }
                        //For Parent - (Old Function,but Function has updated)
                        if (exists)
                        {
                            result = TableConditionCheck(ITEM_OLD_LIST_LG_FNR_ROLE_DEFINE, ITEM_NEW_LIST_LG_FNR_ROLE_DEFINE_MAP, LIST_LG_FNR_ROLE_PERMISSION_MAP, pLG_FNR_ROLE_DEFINE_MAP);
                            if (result.ToLower() != "true")
                                return result;
                        }
                    }
                    LIST_OLD_REMOVED_FUNCTION_DETAILS = OLD_LIST_LG_FNR_ROLE_DEFINE;                             //Store all old removed functions
                    LIST_NEW_SELECTED_FUNCTION_DETAILS = pLG_FNR_ROLE_DEFINE_MAP.LIST_SELECTED_FUNCTION_DETAILS; //Store all newly added functions 

                    //remove old function
                    if (LIST_OLD_REMOVED_FUNCTION_DETAILS.Count() > 0)
                    {
                        for (int i = 0; i < LIST_OLD_REMOVED_FUNCTION_DETAILS.Count(); i++)
                        {
                            //LIST_OLD_REMOVED_FUNCTION_DETAILS[i].MAKE_BY = pLG_FNR_ROLE_DEFINE_MAP.MAKE_BY;   //salekin commented as table has no "MAKE_BY" column 
                            result = RemoveFunctionPermission(LIST_OLD_REMOVED_FUNCTION_DETAILS[i], pLG_FNR_ROLE_DEFINE_MAP.MAKE_BY);
                            if (result.ToLower() != "true")
                                return result;
                        }
                    }

                    //Add newly added function
                    if (LIST_NEW_SELECTED_FUNCTION_DETAILS.Count() > 0)
                    {
                        //function added but this function was added before and it exists in the table
                        foreach (LG_FNR_ROLE_DEFINE_MAP new_item in LIST_NEW_SELECTED_FUNCTION_DETAILS.ToList())
                        {
                            foreach(LG_FNR_ROLE_DEFINE old_item in OLD_INACTIVE_LIST_LG_FNR_ROLE_DEFINE)
                            {
                                if (pLG_FNR_ROLE_DEFINE_MAP.ROLE_ID == old_item.ROLE_ID && new_item.FUNCTION_ID == old_item.FUNCTION_ID)
                                {
                                    var itemToRemove = LIST_NEW_SELECTED_FUNCTION_DETAILS.Single(r => r.FUNCTION_ID == new_item.FUNCTION_ID);
                                    LIST_NEW_SELECTED_FUNCTION_DETAILS.Remove(itemToRemove);  //remaining function will be added for the first time and it doesn't exist in the table

                                    result = TableConditionCheck(old_item, new_item, LIST_LG_FNR_ROLE_PERMISSION_MAP, pLG_FNR_ROLE_DEFINE_MAP);
                                    if (result.ToLower() != "true")
                                        return result;

                                    break;
                                }
                            }
                        }


                        //function added & this function wasn't added before and it doesn't exist in the table
                        OBJ_NEW_LG_FNR_ROLE_DEFINE_MAP.LIST_SELECTED_FUNCTION_DETAILS = LIST_NEW_SELECTED_FUNCTION_DETAILS;
                        OBJ_NEW_LG_FNR_ROLE_DEFINE_MAP.APPLICATION_ID = pLG_FNR_ROLE_DEFINE_MAP.APPLICATION_ID;
                        OBJ_NEW_LG_FNR_ROLE_DEFINE_MAP.ROLE_ID = pLG_FNR_ROLE_DEFINE_MAP.ROLE_ID;
                        OBJ_NEW_LG_FNR_ROLE_DEFINE_MAP.ROLE_DEFINE_COMMAND = pLG_FNR_ROLE_DEFINE_MAP.ROLE_DEFINE_COMMAND;
                        OBJ_NEW_LG_FNR_ROLE_DEFINE_MAP.MAKE_BY = pLG_FNR_ROLE_DEFINE_MAP.MAKE_BY;
                        if (OBJ_NEW_LG_FNR_ROLE_DEFINE_MAP.LIST_SELECTED_FUNCTION_DETAILS.Count() > 0)
                        {
                            result = AddRoleDefine(OBJ_NEW_LG_FNR_ROLE_DEFINE_MAP);
                            if (result.ToLower() != "true")
                                return result;
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
                            result = "Can't Update Role Define(Db) " + validationError.ErrorMessage;
                        }
                    }
                    LG_ERROR_LOG_MAP.Add_Error_Log(FUNC_ID, dbEx.Source, "ERR_APP_TYPE", "UpdateRoleDefine",
                                              "0000000000", dbEx.Message, inner, dbEx.StackTrace, null, dateTime);
                    return result;
                }
                catch (Exception ex)
                {
                    ts.Dispose();
                    string dateTime = Convert.ToString(System.DateTime.Now);
                    string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                    LG_ERROR_LOG_MAP.Add_Error_Log(FUNC_ID, ex.Source, "ERR_APP_TYPE", "UpdateRoleDefine",
                                              "0000000000", ex.Message, inner4, ex.StackTrace, null, dateTime);
                    result = "Can't Update Role Define " + ex.Message;
                    return result;
                }
            }
        }
        #endregion

        public static string TableConditionCheck(LG_FNR_ROLE_DEFINE ITEM_OLD_LIST_LG_FNR_ROLE_DEFINE, LG_FNR_ROLE_DEFINE_MAP ITEM_NEW_LIST_LG_FNR_ROLE_DEFINE_MAP, List<LG_FNR_ROLE_PERMISSION_MAP> LIST_LG_FNR_ROLE_PERMISSION_MAP, LG_FNR_ROLE_DEFINE_MAP pLG_FNR_ROLE_DEFINE_MAP)
        {  
            LG_FNR_ROLE_DEFINE_MAP OBJ_OLD_LG_FNR_ROLE_DEFINE_MAP = new LG_FNR_ROLE_DEFINE_MAP();
            string permission_details = string.Empty;
            string result = string.Empty;

            //role is not deleted previously and modify in form selection
            if ((ITEM_OLD_LIST_LG_FNR_ROLE_DEFINE.MAINT_CRT_FLAG != ITEM_NEW_LIST_LG_FNR_ROLE_DEFINE_MAP.MAINT_CRT_FLAG ||
                                ITEM_OLD_LIST_LG_FNR_ROLE_DEFINE.MAINT_EDT_FLAG != ITEM_NEW_LIST_LG_FNR_ROLE_DEFINE_MAP.MAINT_EDT_FLAG ||
                                ITEM_OLD_LIST_LG_FNR_ROLE_DEFINE.MAINT_DEL_FLAG != ITEM_NEW_LIST_LG_FNR_ROLE_DEFINE_MAP.MAINT_DEL_FLAG ||
                                ITEM_OLD_LIST_LG_FNR_ROLE_DEFINE.MAINT_DTL_FLAG != ITEM_NEW_LIST_LG_FNR_ROLE_DEFINE_MAP.MAINT_DTL_FLAG ||
                                ITEM_OLD_LIST_LG_FNR_ROLE_DEFINE.MAINT_INDX_FLAG != ITEM_NEW_LIST_LG_FNR_ROLE_DEFINE_MAP.MAINT_INDX_FLAG ||
                                ITEM_OLD_LIST_LG_FNR_ROLE_DEFINE.REPORT_VIEW_FLAG != ITEM_NEW_LIST_LG_FNR_ROLE_DEFINE_MAP.REPORT_VIEW_FLAG ||
                                ITEM_OLD_LIST_LG_FNR_ROLE_DEFINE.REPORT_PRINT_FLAG != ITEM_NEW_LIST_LG_FNR_ROLE_DEFINE_MAP.REPORT_PRINT_FLAG ||
                                ITEM_OLD_LIST_LG_FNR_ROLE_DEFINE.REPORT_GEN_FLAG != ITEM_NEW_LIST_LG_FNR_ROLE_DEFINE_MAP.REPORT_GEN_FLAG) && ITEM_OLD_LIST_LG_FNR_ROLE_DEFINE.ROLE_DEFINE_FLAG == 1)
            {
                OBJ_OLD_LG_FNR_ROLE_DEFINE_MAP = new LG_FNR_ROLE_DEFINE_MAP();
                Class_Conversion.LG_FNR_ROLE_DEFINE_REVERSE_CON(OBJ_OLD_LG_FNR_ROLE_DEFINE_MAP, ITEM_OLD_LIST_LG_FNR_ROLE_DEFINE); //Storing the OLD ROLE_DEFINE table data
                Class_Conversion.LG_FNR_ROLE_DEFINE_CON(ITEM_OLD_LIST_LG_FNR_ROLE_DEFINE, ITEM_NEW_LIST_LG_FNR_ROLE_DEFINE_MAP);
                ITEM_OLD_LIST_LG_FNR_ROLE_DEFINE.ROLE_DEFINE_FLAG = 1;
                ITEM_OLD_LIST_LG_FNR_ROLE_DEFINE.AUTH_STATUS_ID = "U";
                ITEM_OLD_LIST_LG_FNR_ROLE_DEFINE.LAST_ACTION = "EDT";
                ITEM_OLD_LIST_LG_FNR_ROLE_DEFINE.LAST_UPDATE_DT = System.DateTime.Now;
                ITEM_OLD_LIST_LG_FNR_ROLE_DEFINE.APPLICATION_ID = ITEM_OLD_LIST_LG_FNR_ROLE_DEFINE.APPLICATION_ID;
                Obj_DBModelEntities.SaveChanges();

                #region Update in "RolePermission" table
                if (OBJ_OLD_LG_FNR_ROLE_DEFINE_MAP.MAINT_CRT_FLAG != ITEM_NEW_LIST_LG_FNR_ROLE_DEFINE_MAP.MAINT_CRT_FLAG)
                {
                    permission_details = ITEM_NEW_LIST_LG_FNR_ROLE_DEFINE_MAP.FUNCTION_NM + "-" + "Create";
                    result = UpdateRolePermissionCheck(LIST_LG_FNR_ROLE_PERMISSION_MAP, permission_details, pLG_FNR_ROLE_DEFINE_MAP.ROLE_ID, ITEM_NEW_LIST_LG_FNR_ROLE_DEFINE_MAP.FUNCTION_ID, ITEM_NEW_LIST_LG_FNR_ROLE_DEFINE_MAP.MAINT_CRT_FLAG);
                    if (result.ToLower() != "true")
                        return result;
                }
                if (OBJ_OLD_LG_FNR_ROLE_DEFINE_MAP.MAINT_EDT_FLAG != ITEM_NEW_LIST_LG_FNR_ROLE_DEFINE_MAP.MAINT_EDT_FLAG)
                {
                    permission_details = ITEM_NEW_LIST_LG_FNR_ROLE_DEFINE_MAP.FUNCTION_NM + "-" + "Edit";
                    result = UpdateRolePermissionCheck(LIST_LG_FNR_ROLE_PERMISSION_MAP, permission_details, pLG_FNR_ROLE_DEFINE_MAP.ROLE_ID, ITEM_NEW_LIST_LG_FNR_ROLE_DEFINE_MAP.FUNCTION_ID, ITEM_NEW_LIST_LG_FNR_ROLE_DEFINE_MAP.MAINT_EDT_FLAG);
                    if (result.ToLower() != "true")
                        return result;
                }
                if (OBJ_OLD_LG_FNR_ROLE_DEFINE_MAP.MAINT_DEL_FLAG != ITEM_NEW_LIST_LG_FNR_ROLE_DEFINE_MAP.MAINT_DEL_FLAG)
                {
                    permission_details = ITEM_NEW_LIST_LG_FNR_ROLE_DEFINE_MAP.FUNCTION_NM + "-" + "Delete";
                    result = UpdateRolePermissionCheck(LIST_LG_FNR_ROLE_PERMISSION_MAP, permission_details, pLG_FNR_ROLE_DEFINE_MAP.ROLE_ID, ITEM_NEW_LIST_LG_FNR_ROLE_DEFINE_MAP.FUNCTION_ID, ITEM_NEW_LIST_LG_FNR_ROLE_DEFINE_MAP.MAINT_DEL_FLAG);
                    if (result.ToLower() != "true")
                        return result;
                }
                if (OBJ_OLD_LG_FNR_ROLE_DEFINE_MAP.MAINT_DTL_FLAG != ITEM_NEW_LIST_LG_FNR_ROLE_DEFINE_MAP.MAINT_DTL_FLAG)
                {
                    permission_details = ITEM_NEW_LIST_LG_FNR_ROLE_DEFINE_MAP.FUNCTION_NM + "-" + "Details";
                    result = UpdateRolePermissionCheck(LIST_LG_FNR_ROLE_PERMISSION_MAP, permission_details, pLG_FNR_ROLE_DEFINE_MAP.ROLE_ID, ITEM_NEW_LIST_LG_FNR_ROLE_DEFINE_MAP.FUNCTION_ID, ITEM_NEW_LIST_LG_FNR_ROLE_DEFINE_MAP.MAINT_DTL_FLAG);
                    if (result.ToLower() != "true")
                        return result;
                }
                if (OBJ_OLD_LG_FNR_ROLE_DEFINE_MAP.MAINT_INDX_FLAG != ITEM_NEW_LIST_LG_FNR_ROLE_DEFINE_MAP.MAINT_INDX_FLAG)
                {
                    permission_details = ITEM_NEW_LIST_LG_FNR_ROLE_DEFINE_MAP.FUNCTION_NM + "-" + "Index";
                    result = UpdateRolePermissionCheck(LIST_LG_FNR_ROLE_PERMISSION_MAP, permission_details, pLG_FNR_ROLE_DEFINE_MAP.ROLE_ID, ITEM_NEW_LIST_LG_FNR_ROLE_DEFINE_MAP.FUNCTION_ID, ITEM_NEW_LIST_LG_FNR_ROLE_DEFINE_MAP.MAINT_INDX_FLAG);
                    if (result.ToLower() != "true")
                        return result;
                }
                #endregion

                #region Auth log
                LG_AA_NFT_AUTH_LOG_MAP OBJ_LG_AA_NFT_AUTH_LOG_MAP = new LG_AA_NFT_AUTH_LOG_MAP();
                OBJ_LG_AA_NFT_AUTH_LOG_MAP.FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "RoleDefine").Select(x => x.FUNCTION_ID).SingleOrDefault();
                OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_NAME = "LG_FNR_ROLE_DEFINE";
                OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_NM = "SL_NO";
                OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_VAL = ITEM_OLD_LIST_LG_FNR_ROLE_DEFINE.SL_NO != null ? ITEM_OLD_LIST_LG_FNR_ROLE_DEFINE.SL_NO.ToString() : null;
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
                OBJ_LG_AA_NFT_AUTH_LOG_MAP.MAKE_BY = pLG_FNR_ROLE_DEFINE_MAP.MAKE_BY;
                OBJ_LG_AA_NFT_AUTH_LOG_MAP.MAKE_DT = DateTime.Now;
                result = LG_AA_NFT_AUTH_LOG_MAP.AddNftAuthLog(OBJ_OLD_LG_FNR_ROLE_DEFINE_MAP, ITEM_NEW_LIST_LG_FNR_ROLE_DEFINE_MAP, OBJ_LG_AA_NFT_AUTH_LOG_MAP);           
                if (result.ToLower() != "true")
                    return result;
                #endregion

                return "true";
            }

            //previously deleted role is added and modify in form selection
            else if ((ITEM_OLD_LIST_LG_FNR_ROLE_DEFINE.MAINT_CRT_FLAG != ITEM_NEW_LIST_LG_FNR_ROLE_DEFINE_MAP.MAINT_CRT_FLAG ||
                            ITEM_OLD_LIST_LG_FNR_ROLE_DEFINE.MAINT_EDT_FLAG != ITEM_NEW_LIST_LG_FNR_ROLE_DEFINE_MAP.MAINT_EDT_FLAG ||
                            ITEM_OLD_LIST_LG_FNR_ROLE_DEFINE.MAINT_DEL_FLAG != ITEM_NEW_LIST_LG_FNR_ROLE_DEFINE_MAP.MAINT_DEL_FLAG ||
                            ITEM_OLD_LIST_LG_FNR_ROLE_DEFINE.MAINT_DTL_FLAG != ITEM_NEW_LIST_LG_FNR_ROLE_DEFINE_MAP.MAINT_DTL_FLAG ||
                            ITEM_OLD_LIST_LG_FNR_ROLE_DEFINE.MAINT_INDX_FLAG != ITEM_NEW_LIST_LG_FNR_ROLE_DEFINE_MAP.MAINT_INDX_FLAG ||
                            ITEM_OLD_LIST_LG_FNR_ROLE_DEFINE.REPORT_VIEW_FLAG != ITEM_NEW_LIST_LG_FNR_ROLE_DEFINE_MAP.REPORT_VIEW_FLAG ||
                            ITEM_OLD_LIST_LG_FNR_ROLE_DEFINE.REPORT_PRINT_FLAG != ITEM_NEW_LIST_LG_FNR_ROLE_DEFINE_MAP.REPORT_PRINT_FLAG ||
                            ITEM_OLD_LIST_LG_FNR_ROLE_DEFINE.REPORT_GEN_FLAG != ITEM_NEW_LIST_LG_FNR_ROLE_DEFINE_MAP.REPORT_GEN_FLAG) && ITEM_OLD_LIST_LG_FNR_ROLE_DEFINE.ROLE_DEFINE_FLAG == 0)
            {
                OBJ_OLD_LG_FNR_ROLE_DEFINE_MAP = new LG_FNR_ROLE_DEFINE_MAP();
                Class_Conversion.LG_FNR_ROLE_DEFINE_REVERSE_CON(OBJ_OLD_LG_FNR_ROLE_DEFINE_MAP, ITEM_OLD_LIST_LG_FNR_ROLE_DEFINE); //Storing the OLD ROLE_DEFINE table data
                Class_Conversion.LG_FNR_ROLE_DEFINE_CON(ITEM_OLD_LIST_LG_FNR_ROLE_DEFINE, ITEM_NEW_LIST_LG_FNR_ROLE_DEFINE_MAP);
                ITEM_OLD_LIST_LG_FNR_ROLE_DEFINE.ROLE_DEFINE_FLAG = 1;
                ITEM_OLD_LIST_LG_FNR_ROLE_DEFINE.AUTH_STATUS_ID = "U";
                ITEM_OLD_LIST_LG_FNR_ROLE_DEFINE.LAST_ACTION = "ADD";
                ITEM_OLD_LIST_LG_FNR_ROLE_DEFINE.LAST_UPDATE_DT = System.DateTime.Now;
                ITEM_OLD_LIST_LG_FNR_ROLE_DEFINE.APPLICATION_ID = ITEM_OLD_LIST_LG_FNR_ROLE_DEFINE.APPLICATION_ID;
                Obj_DBModelEntities.SaveChanges();

                #region Update in "RolePermission" table
                if (OBJ_OLD_LG_FNR_ROLE_DEFINE_MAP.MAINT_CRT_FLAG != ITEM_NEW_LIST_LG_FNR_ROLE_DEFINE_MAP.MAINT_CRT_FLAG)
                {
                    permission_details = ITEM_NEW_LIST_LG_FNR_ROLE_DEFINE_MAP.FUNCTION_NM + "-" + "Create";
                    result = UpdateRolePermissionCheck(LIST_LG_FNR_ROLE_PERMISSION_MAP, permission_details, pLG_FNR_ROLE_DEFINE_MAP.ROLE_ID, ITEM_NEW_LIST_LG_FNR_ROLE_DEFINE_MAP.FUNCTION_ID, ITEM_NEW_LIST_LG_FNR_ROLE_DEFINE_MAP.MAINT_CRT_FLAG);
                    if (result.ToLower() != "true")
                        return result;
                }
                if (OBJ_OLD_LG_FNR_ROLE_DEFINE_MAP.MAINT_EDT_FLAG != ITEM_NEW_LIST_LG_FNR_ROLE_DEFINE_MAP.MAINT_EDT_FLAG)
                {
                    permission_details = ITEM_NEW_LIST_LG_FNR_ROLE_DEFINE_MAP.FUNCTION_NM + "-" + "Edit";
                    result = UpdateRolePermissionCheck(LIST_LG_FNR_ROLE_PERMISSION_MAP, permission_details, pLG_FNR_ROLE_DEFINE_MAP.ROLE_ID, ITEM_NEW_LIST_LG_FNR_ROLE_DEFINE_MAP.FUNCTION_ID, ITEM_NEW_LIST_LG_FNR_ROLE_DEFINE_MAP.MAINT_EDT_FLAG);
                    if (result.ToLower() != "true")
                        return result;
                }
                if (OBJ_OLD_LG_FNR_ROLE_DEFINE_MAP.MAINT_DEL_FLAG != ITEM_NEW_LIST_LG_FNR_ROLE_DEFINE_MAP.MAINT_DEL_FLAG)
                {
                    permission_details = ITEM_NEW_LIST_LG_FNR_ROLE_DEFINE_MAP.FUNCTION_NM + "-" + "Delete";
                    result = UpdateRolePermissionCheck(LIST_LG_FNR_ROLE_PERMISSION_MAP, permission_details, pLG_FNR_ROLE_DEFINE_MAP.ROLE_ID, ITEM_NEW_LIST_LG_FNR_ROLE_DEFINE_MAP.FUNCTION_ID, ITEM_NEW_LIST_LG_FNR_ROLE_DEFINE_MAP.MAINT_DEL_FLAG);
                    if (result.ToLower() != "true")
                        return result;
                }
                if (OBJ_OLD_LG_FNR_ROLE_DEFINE_MAP.MAINT_DTL_FLAG != ITEM_NEW_LIST_LG_FNR_ROLE_DEFINE_MAP.MAINT_DTL_FLAG)
                {
                    permission_details = ITEM_NEW_LIST_LG_FNR_ROLE_DEFINE_MAP.FUNCTION_NM + "-" + "Details";
                    result = UpdateRolePermissionCheck(LIST_LG_FNR_ROLE_PERMISSION_MAP, permission_details, pLG_FNR_ROLE_DEFINE_MAP.ROLE_ID, ITEM_NEW_LIST_LG_FNR_ROLE_DEFINE_MAP.FUNCTION_ID, ITEM_NEW_LIST_LG_FNR_ROLE_DEFINE_MAP.MAINT_DTL_FLAG);
                    if (result.ToLower() != "true")
                        return result;
                }
                if (OBJ_OLD_LG_FNR_ROLE_DEFINE_MAP.MAINT_INDX_FLAG != ITEM_NEW_LIST_LG_FNR_ROLE_DEFINE_MAP.MAINT_INDX_FLAG)
                {
                    permission_details = ITEM_NEW_LIST_LG_FNR_ROLE_DEFINE_MAP.FUNCTION_NM + "-" + "Index";
                    result = UpdateRolePermissionCheck(LIST_LG_FNR_ROLE_PERMISSION_MAP, permission_details, pLG_FNR_ROLE_DEFINE_MAP.ROLE_ID, ITEM_NEW_LIST_LG_FNR_ROLE_DEFINE_MAP.FUNCTION_ID, ITEM_NEW_LIST_LG_FNR_ROLE_DEFINE_MAP.MAINT_INDX_FLAG);
                    if (result.ToLower() != "true")
                        return result;
                }
                #endregion

                #region Auth log
                LG_AA_NFT_AUTH_LOG_MAP OBJ_LG_AA_NFT_AUTH_LOG_MAP = new LG_AA_NFT_AUTH_LOG_MAP();
                OBJ_LG_AA_NFT_AUTH_LOG_MAP.FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "RoleDefine").Select(x => x.FUNCTION_ID).SingleOrDefault();
                OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_NAME = "LG_FNR_ROLE_DEFINE";
                OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_NM = "SL_NO";
                OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_VAL = ITEM_OLD_LIST_LG_FNR_ROLE_DEFINE.SL_NO != null ? ITEM_OLD_LIST_LG_FNR_ROLE_DEFINE.SL_NO.ToString() : null;
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
                OBJ_LG_AA_NFT_AUTH_LOG_MAP.MAKE_BY = pLG_FNR_ROLE_DEFINE_MAP.MAKE_BY;
                OBJ_LG_AA_NFT_AUTH_LOG_MAP.MAKE_DT = DateTime.Now;
                result = LG_AA_NFT_AUTH_LOG_MAP.AddNftAuthLog(ITEM_OLD_LIST_LG_FNR_ROLE_DEFINE, OBJ_LG_AA_NFT_AUTH_LOG_MAP);
                if (result.ToLower() != "true")
                    return result;
                #endregion

                return "true";
            }

            //previously deleted role is added but no modification in form selection
            else
            {
                //Only Role Define Flag is updated & Other pages are same as it is
                if (ITEM_OLD_LIST_LG_FNR_ROLE_DEFINE.ROLE_DEFINE_FLAG == 0)
                {
                    OBJ_OLD_LG_FNR_ROLE_DEFINE_MAP = new LG_FNR_ROLE_DEFINE_MAP();
                    LG_FNR_ROLE_DEFINE_MAP OBJ_NEW_LG_FNR_ROLE_DEFINE_MAP = new LG_FNR_ROLE_DEFINE_MAP();
                    Class_Conversion.LG_FNR_ROLE_DEFINE_REVERSE_CON(OBJ_OLD_LG_FNR_ROLE_DEFINE_MAP, ITEM_OLD_LIST_LG_FNR_ROLE_DEFINE);

                    ITEM_OLD_LIST_LG_FNR_ROLE_DEFINE.ROLE_DEFINE_FLAG = 1;
                    ITEM_OLD_LIST_LG_FNR_ROLE_DEFINE.AUTH_STATUS_ID = "U";
                    ITEM_OLD_LIST_LG_FNR_ROLE_DEFINE.LAST_ACTION = "ADD";
                    ITEM_OLD_LIST_LG_FNR_ROLE_DEFINE.LAST_UPDATE_DT = System.DateTime.Now;
                    ITEM_OLD_LIST_LG_FNR_ROLE_DEFINE.APPLICATION_ID = ITEM_OLD_LIST_LG_FNR_ROLE_DEFINE.APPLICATION_ID;
                    Obj_DBModelEntities.SaveChanges();
                    Class_Conversion.LG_FNR_ROLE_DEFINE_REVERSE_CON(OBJ_NEW_LG_FNR_ROLE_DEFINE_MAP, ITEM_OLD_LIST_LG_FNR_ROLE_DEFINE);

                    result = UpdateRolePermissionFlag(ITEM_OLD_LIST_LG_FNR_ROLE_DEFINE.ROLE_ID, ITEM_OLD_LIST_LG_FNR_ROLE_DEFINE.FUNCTION_ID, 1);
                    if (result.ToLower() != "true")
                        return result;

                    #region Auth log
                    LG_AA_NFT_AUTH_LOG_MAP OBJ_LG_AA_NFT_AUTH_LOG_MAP = new LG_AA_NFT_AUTH_LOG_MAP();
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "RoleDefine").Select(x => x.FUNCTION_ID).SingleOrDefault();
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_NAME = "LG_FNR_ROLE_DEFINE";
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_NM = "SL_NO";
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_VAL = ITEM_OLD_LIST_LG_FNR_ROLE_DEFINE.SL_NO != null ? ITEM_OLD_LIST_LG_FNR_ROLE_DEFINE.SL_NO.ToString() : null;
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
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.MAKE_BY = pLG_FNR_ROLE_DEFINE_MAP.MAKE_BY;
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.MAKE_DT = DateTime.Now;
                    result = LG_AA_NFT_AUTH_LOG_MAP.AddNftAuthLog(ITEM_OLD_LIST_LG_FNR_ROLE_DEFINE, OBJ_LG_AA_NFT_AUTH_LOG_MAP);
                    if (result.ToLower() != "true")
                        return result;
                    #endregion
                }
                return "true";            
            }
        }

        #region Delete
        #endregion

        #region Fetch Single
        public static LG_FNR_ROLE_DEFINE_MAP GetRoleDefineInfoByRoleId(string pROLE_ID)// Edit Role
        {
            LG_FNR_ROLE_DEFINE_MAP OBJ_LG_FNR_ROLE_DEFINE_MAP = new LG_FNR_ROLE_DEFINE_MAP();
            try
            {
                FUNC_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "RoleDefine").Select(x => x.FUNCTION_ID).SingleOrDefault();
                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                OBJ_LG_FNR_ROLE_DEFINE_MAP = (from role_d in Obj_DBModelEntities.LG_FNR_ROLE_DEFINE
                                              //group role_dd by role_dd.ROLE_ID into role_d
                                              join role in Obj_DBModelEntities.LG_FNR_ROLE
                                              on role_d.ROLE_ID equals role.ROLE_ID
                                              //on role_d.ROLE_ID equals role.ROLE_ID
                                              //join f in Obj_DBModelEntities.LG_FNR_FUNCTION
                                              //on role_d.FUNCTION_ID equals f.FUNCTION_ID

                                              //join a in Obj_DBModelEntities.LG_FNR_APPLICATION
                                              //on f.APPLICATION_ID equals a.APPLICATION_ID
                                              //join s in Obj_DBModelEntities.LG_FNR_SERVICE
                                              //on f.SERVICE_ID equals s.SERVICE_ID
                                              //join m in Obj_DBModelEntities.LG_FNR_MODULE
                                              //on f.MODULE_ID equals m.MODULE_ID

                                              where role_d.ROLE_ID == pROLE_ID

                                              select new LG_FNR_ROLE_DEFINE_MAP
                                              {
                                                  ROLE_ID = pROLE_ID,
                                                  ROLE_NM = role.ROLE_NAME,
                                                  ROLE_DESCRIP = role.ROLE_DESCRIP,

                                                  //APPLICATION_ID = f.APPLICATION_ID,
                                                  //APPLICATION_NAME = a.APPLICATION_NAME,
                                                  //SERVICE_ID = f.SERVICE_ID,
                                                  //SERVICE_NM = s.SERVICE_NM,
                                                  //MODULE_ID = f.MODULE_ID,
                                                  //MODULE_NM = m.MODULE_NM,
                                                  //ITEM_TYPE = f.ITEM_TYPE,
                                              }).FirstOrDefault();
                OBJ_LG_FNR_ROLE_DEFINE_MAP.APPLICATION_LIST_FOR_DD = DropDown.GetApplicationForDD();
                return OBJ_LG_FNR_ROLE_DEFINE_MAP;
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
                        OBJ_LG_FNR_ROLE_DEFINE_MAP.ERROR = validationError.ErrorMessage;
                    }
                }
                LG_ERROR_LOG_MAP.Add_Error_Log(FUNC_ID, dbEx.Source, "ERR_APP_TYPE", "GetRoleDefineInfoByRoleId",
                                              "0000000000", dbEx.Message, inner, dbEx.StackTrace, null, dateTime);
                return OBJ_LG_FNR_ROLE_DEFINE_MAP;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNC_ID, ex.Source, "ERR_APP_TYPE", "GetRoleDefineInfoByRoleId",
                                              "0000000000", ex.Message, inner4, ex.StackTrace, null, dateTime);
                OBJ_LG_FNR_ROLE_DEFINE_MAP.ERROR = ex.Message;
                return OBJ_LG_FNR_ROLE_DEFINE_MAP;
            }
        }
        #endregion

        #region Fetch all
        public static IEnumerable<LG_FNR_ROLE_DEFINE_MAP> GetAllRoleDefinedInfo()
        {
            LG_FNR_ROLE_DEFINE_MAP OBJ_LG_FNR_ROLE_DEFINE_MAP = new LG_FNR_ROLE_DEFINE_MAP();
            List<LG_FNR_ROLE_DEFINE_MAP> LIST_MAX_LG_FNR_ROLE_DEFINE_MAP = new List<LG_FNR_ROLE_DEFINE_MAP>();
            List<LG_FNR_ROLE_DEFINE_MAP> LIST_MIN_LG_FNR_ROLE_DEFINE_MAP = new List<LG_FNR_ROLE_DEFINE_MAP>();
            List<LG_FNR_ROLE_DEFINE_MAP> LIST_ROLE_WISE_FUNCTIONS = new List<LG_FNR_ROLE_DEFINE_MAP>();
            bool exists;
            try
            {
                FUNC_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "BindADUser").Select(x => x.FUNCTION_ID).SingleOrDefault();
                Obj_DBModelEntities.Configuration.LazyLoadingEnabled = false;
                Obj_DBModelEntities.Configuration.ProxyCreationEnabled = false;

                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                LIST_MAX_LG_FNR_ROLE_DEFINE_MAP = Obj_DBModelEntities.LG_FNR_ROLE_DEFINE
                                              .Join(Obj_DBModelEntities.LG_FNR_ROLE, role_d => role_d.ROLE_ID, role => role.ROLE_ID, (role_d, role) => new { role_d, role })
                                              .Where(T => T.role_d.ROLE_ID != null && 
                                                          //T.role_d.AUTH_STATUS_ID == "A" && 
                                                          T.role_d.LAST_ACTION != "DEL")
                                              .OrderByDescending(T => T.role_d.ROLE_ID)                //.GroupBy(T => T.ROLE_ID)
                                              .Select(T =>
                                              new LG_FNR_ROLE_DEFINE_MAP
                                              {
                                                  ROLE_ID = T.role_d.ROLE_ID,
                                                  ROLE_NM = T.role.ROLE_NAME,
                                                  AUTH_STATUS_ID = T.role_d.AUTH_STATUS_ID,
                                                  FUNCTION_ID = T.role_d.FUNCTION_ID
                                              }).ToList();

                LIST_MIN_LG_FNR_ROLE_DEFINE_MAP = Obj_DBModelEntities.LG_FNR_ROLE_DEFINE
                                              .Where(T => T.ROLE_ID != null && 
                                                          //T.AUTH_STATUS_ID == "A" && 
                                                          T.LAST_ACTION != "DEL")
                                              .GroupBy(T => T.ROLE_ID)
                                              .Select(T =>
                                              new LG_FNR_ROLE_DEFINE_MAP
                                              {
                                                  ROLE_ID = T.Key
                                              }).OrderByDescending(T => T.ROLE_ID).ToList();

                if (LIST_MIN_LG_FNR_ROLE_DEFINE_MAP.Count() > 0)
                {
                    for (int i = 0; i < LIST_MIN_LG_FNR_ROLE_DEFINE_MAP.Count(); i++)
                    {
                        string role_id = LIST_MIN_LG_FNR_ROLE_DEFINE_MAP[i].ROLE_ID;
                        LIST_ROLE_WISE_FUNCTIONS = (List<LG_FNR_ROLE_DEFINE_MAP>)LIST_MAX_LG_FNR_ROLE_DEFINE_MAP.Where(role_d => role_d.ROLE_ID == role_id).ToList();
                        exists = false;
                        int j;
                        for (j = 0; j < LIST_ROLE_WISE_FUNCTIONS.Count(); j++)
                        {
                            if (LIST_ROLE_WISE_FUNCTIONS[j].AUTH_STATUS_ID == "U")
                            {
                                exists = true;
                                break;
                            }
                        }
                        if (exists)
                        {
                            LIST_MIN_LG_FNR_ROLE_DEFINE_MAP.RemoveAll(t => t.ROLE_ID == LIST_ROLE_WISE_FUNCTIONS[j].ROLE_ID);  //unauthorized user won't show in index

                            i = i - 1;
                            //LIST_MIN_LG_FNR_ROLE_DEFINE_MAP[i].AUTH_STATUS_ID = "U";
                            //LIST_MIN_LG_FNR_ROLE_DEFINE_MAP[i].ROLE_NM = LIST_ROLE_WISE_FUNCTIONS[j].ROLE_NM;
                        }
                        else
                        {
                            LIST_MIN_LG_FNR_ROLE_DEFINE_MAP[i].AUTH_STATUS_ID = "A";
                            LIST_MIN_LG_FNR_ROLE_DEFINE_MAP[i].ROLE_NM = LIST_ROLE_WISE_FUNCTIONS[j - 1].ROLE_NM;
                        }
                    }
                }
                return LIST_MIN_LG_FNR_ROLE_DEFINE_MAP.OrderByDescending(T => T.MAKE_DT).ThenBy(m => m.APPLICATION_ID).ToList();  //workable
                //return LIST_MIN_LG_FNR_ROLE_DEFINE_MAP;
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
                        OBJ_LG_FNR_ROLE_DEFINE_MAP.ERROR = validationError.ErrorMessage;
                        LIST_MIN_LG_FNR_ROLE_DEFINE_MAP.Add(OBJ_LG_FNR_ROLE_DEFINE_MAP);
                    }
                }
                LG_ERROR_LOG_MAP.Add_Error_Log(FUNC_ID, dbEx.Source, "ERR_APP_TYPE", "GetAllRoleDefinedInfo",
                                          "0000000000", dbEx.Message, inner, dbEx.StackTrace, null, dateTime);

                return LIST_MIN_LG_FNR_ROLE_DEFINE_MAP;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNC_ID, ex.Source, "ERR_APP_TYPE", "GetAllRoleDefinedInfo",
                                          "0000000000", ex.Message, inner4, ex.StackTrace, null, dateTime);
                OBJ_LG_FNR_ROLE_DEFINE_MAP.ERROR = ex.Message;
                LIST_MIN_LG_FNR_ROLE_DEFINE_MAP.Add(OBJ_LG_FNR_ROLE_DEFINE_MAP);
                return LIST_MIN_LG_FNR_ROLE_DEFINE_MAP;
            }
        }
        #endregion

        #region Custom Methods
        public static LG_FNR_ROLE_MAP GetRoleByRoleName(string pROLE_NAME)
        {
            LG_FNR_ROLE_MAP OBJ_LG_FNR_ROLE_MAP = new LG_FNR_ROLE_MAP();

            try
            {
                FUNC_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "RoleDefine").Select(x => x.FUNCTION_ID).SingleOrDefault();
                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                OBJ_LG_FNR_ROLE_MAP = (from r in Obj_DBModelEntities.LG_FNR_ROLE
                                       where r.ROLE_NAME == pROLE_NAME
                                       select new LG_FNR_ROLE_MAP
                                       {
                                           ROLE_ID = r.ROLE_ID,
                                           ROLE_NAME = r.ROLE_NAME,
                                           ROLE_DESCRIP = r.ROLE_DESCRIP,
                                           AUTH_STATUS_ID = r.AUTH_STATUS_ID,
                                           LAST_ACTION = r.LAST_ACTION,
                                           LAST_UPDATE_DT = r.LAST_UPDATE_DT,
                                           MAKE_DT = r.MAKE_DT
                                       }).SingleOrDefault();
                return OBJ_LG_FNR_ROLE_MAP;
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
                        OBJ_LG_FNR_ROLE_MAP.ERROR = validationError.ErrorMessage;
                    }
                }

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNC_ID, dbEx.Source, "ERR_APP_TYPE", "GetRoleByRoleName",
                                          "0000000000", dbEx.Message, inner, dbEx.StackTrace, null, dateTime);
                return OBJ_LG_FNR_ROLE_MAP;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNC_ID, ex.Source, "ERR_APP_TYPE", "GetRoleByRoleName",
                                          "0000000000", ex.Message, inner4, ex.StackTrace, null, dateTime);
                OBJ_LG_FNR_ROLE_MAP.ERROR = ex.Message;
                return OBJ_LG_FNR_ROLE_MAP;
            }
        }
        public static IEnumerable<LG_FNR_ROLE_DEFINE_MAP> GetFunctionsByModuleIdAndItemtype(string app_id, string service_id, string module_id, string item_type)
        {
            List<LG_FNR_ROLE_DEFINE_MAP> LIST_LG_FNR_ROLE_DEFINE_MAP = null;

            string format = OutgoingResponseFormat.GetFormat();
            OutgoingResponseFormat.SetResponseFormat(format);

            LIST_LG_FNR_ROLE_DEFINE_MAP = (from f in Obj_DBModelEntities.LG_FNR_FUNCTION
                                           where f.APPLICATION_ID == app_id &&
                                                 f.SERVICE_ID == service_id &&
                                                 f.MODULE_ID == module_id &&
                                                 f.ITEM_TYPE == item_type
                                           orderby f.FUNCTION_ID ascending
                                           select new LG_FNR_ROLE_DEFINE_MAP
                                           {
                                               FUNCTION_ID = f.FUNCTION_ID,
                                               FUNCTION_NM = f.FUNCTION_NM,
                                               MAINT_CRT_FLAG = f.MAINT_CRT_FLAG,
                                               MAINT_EDT_FLAG = f.MAINT_EDT_FLAG,
                                               MAINT_DEL_FLAG = f.MAINT_DEL_FLAG,
                                               MAINT_DTL_FLAG = f.MAINT_DTL_FLAG,
                                               MAINT_INDX_FLAG = f.MAINT_INDX_FLAG,

                                               MAINT_OTP_FLAG = f.MAINT_OTP_FLAG,
                                               MAINT_2FA_FLAG = f.MAINT_2FA_FLAG,
                                               MAINT_2FA_HARD_FLAG = f.MAINT_2FA_HARD_FLAG,
                                               MAINT_2FA_SOFT_FLAG = f.MAINT_2FA_SOFT_FLAG,
                                               MAINT_AUTH_FLAG = f.MAINT_AUTH_FLAG,
                                               AUTH_LEVEL = f.AUTH_LEVEL,

                                               REPORT_VIEW_FLAG = f.REPORT_VIEW_FLAG,
                                               REPORT_PRINT_FLAG = f.REPORT_PRINT_FLAG,
                                               REPORT_GEN_FLAG = f.REPORT_GEN_FLAG,

                                               AUTH_STATUS_ID = f.AUTH_STATUS_ID,
                                               LAST_ACTION = f.LAST_ACTION,
                                               //LAST_UPDATE_DT = f.LAST_UPDATE_DT,
                                               //MAKE_DT = f.MAKE_DT
                                           }).ToList();
            foreach (LG_FNR_ROLE_DEFINE_MAP ITEM_LG_FNR_ROLE_DEFINE_MAP in LIST_LG_FNR_ROLE_DEFINE_MAP)
            {
                BooleanConversion.LG_FNR_ROLE_DEFINE_MAP_INT_TO_BOOL(ITEM_LG_FNR_ROLE_DEFINE_MAP);
            }
            return LIST_LG_FNR_ROLE_DEFINE_MAP;
        }
        public static List<LG_FNR_ROLE_DEFINE_MAP> GetSelectedFunctionsByRoleId(string pROLE_ID) //method for gridEdit ajax call
        {
            List<LG_FNR_ROLE_DEFINE_MAP> LIST_LG_FNR_ROLE_DEFINE_MAP = null;

            string format = OutgoingResponseFormat.GetFormat();
            OutgoingResponseFormat.SetResponseFormat(format);

            LIST_LG_FNR_ROLE_DEFINE_MAP = (from role_d in Obj_DBModelEntities.LG_FNR_ROLE_DEFINE
                                           join role in Obj_DBModelEntities.LG_FNR_FUNCTION
                                           on role_d.FUNCTION_ID equals role.FUNCTION_ID
                                           where role_d.ROLE_ID == pROLE_ID &&
                                                 role_d.ROLE_DEFINE_FLAG == 1 &&
                                                 role_d.AUTH_STATUS_ID == "A"
                                           orderby role.FUNCTION_ID ascending
                                           select new LG_FNR_ROLE_DEFINE_MAP
                                           {
                                               FUNCTION_ID = role_d.FUNCTION_ID,
                                               FUNCTION_NM = role.FUNCTION_NM,
                                               MAINT_CRT_FLAG = role_d.MAINT_CRT_FLAG,
                                               MAINT_EDT_FLAG = role_d.MAINT_EDT_FLAG,
                                               MAINT_DEL_FLAG = role_d.MAINT_DEL_FLAG,
                                               MAINT_DTL_FLAG = role_d.MAINT_DTL_FLAG,
                                               MAINT_INDX_FLAG = role_d.MAINT_INDX_FLAG,

                                               MAINT_OTP_FLAG = role_d.MAINT_OTP_FLAG,
                                               MAINT_2FA_FLAG = role_d.MAINT_2FA_FLAG,
                                               MAINT_2FA_HARD_FLAG = role_d.MAINT_2FA_HARD_FLAG,
                                               MAINT_2FA_SOFT_FLAG = role_d.MAINT_2FA_SOFT_FLAG,
                                               MAINT_AUTH_FLAG = role_d.MAINT_AUTH_FLAG,
                                               AUTH_LEVEL = role_d.AUTH_LEVEL,

                                               REPORT_VIEW_FLAG = role_d.REPORT_VIEW_FLAG,
                                               REPORT_PRINT_FLAG = role_d.REPORT_PRINT_FLAG,
                                               REPORT_GEN_FLAG = role_d.REPORT_GEN_FLAG,

                                               AUTH_STATUS_ID = role_d.AUTH_STATUS_ID,
                                               LAST_ACTION = role_d.LAST_ACTION,    
                                               //LAST_UPDATE_DT = f.LAST_UPDATE_DT,
                                               //MAKE_DT = f.MAKE_DT,
                                               APPLICATION_ID = role_d.APPLICATION_ID
                                           }).ToList();
            return LIST_LG_FNR_ROLE_DEFINE_MAP;
        }
        public static List<LG_FNR_ROLE_DEFINE_MAP> GetAllAndSelectedFunctionsByRoleId(string app_id, string service_id, string module_id, string item_type, string pROLE_ID)
        {
            //LG_FNR_ROLE_DEFINE_MAP OBJ_MODIFIED_LG_FNR_ROLE_DEFINE_MAP = new LG_FNR_ROLE_DEFINE_MAP();
            List<LG_FNR_ROLE_DEFINE_MAP> LIST_ALL_FUNCTIONS = new List<LG_FNR_ROLE_DEFINE_MAP>();
            List<LG_FNR_ROLE_DEFINE_MAP> LIST_SELECTED_FUNCTIONS = new List<LG_FNR_ROLE_DEFINE_MAP>();
            List<LG_FNR_ROLE_DEFINE_MAP> LIST_MODIFIED_FUNCTIONS = new List<LG_FNR_ROLE_DEFINE_MAP>();
            LIST_ALL_FUNCTIONS = (List<LG_FNR_ROLE_DEFINE_MAP>)GetFunctionsByModuleIdAndItemtype(app_id, service_id, module_id, item_type);
            LIST_SELECTED_FUNCTIONS = (List<LG_FNR_ROLE_DEFINE_MAP>)GetSelectedFunctionsByRoleId(pROLE_ID);
            bool exists;

            foreach (LG_FNR_ROLE_DEFINE_MAP ITEM_OF_LIST_ALL_FUNCTIONS in LIST_ALL_FUNCTIONS)
            {
                exists = false;
                foreach (LG_FNR_ROLE_DEFINE_MAP ITEM_OF_LIST_SELECTED_FUNCTIONS in LIST_SELECTED_FUNCTIONS)
                {
                    if (ITEM_OF_LIST_ALL_FUNCTIONS.FUNCTION_ID == ITEM_OF_LIST_SELECTED_FUNCTIONS.FUNCTION_ID)
                    {
                        exists = true;
                        //OBJ_MODIFIED_LG_FNR_ROLE_DEFINE_MAP = ITEM_OF_LIST_SELECTED_FUNCTIONS;
                        var itemToRemove = LIST_SELECTED_FUNCTIONS.Single(r => r.FUNCTION_ID == ITEM_OF_LIST_SELECTED_FUNCTIONS.FUNCTION_ID);
                        LIST_SELECTED_FUNCTIONS.Remove(itemToRemove); //Minimizing the List if true, so that foreach loop will iterate less
                        break;
                    }
                }
                if (exists)
                {
                    LIST_MODIFIED_FUNCTIONS.Add(ITEM_OF_LIST_ALL_FUNCTIONS);
                }
                else
                {
                    //OBJ_MODIFIED_LG_FNR_ROLE_DEFINE_MAP = ITEM_OF_LIST_ALL_FUNCTIONS;
                    LIST_MODIFIED_FUNCTIONS.Add(ITEM_OF_LIST_ALL_FUNCTIONS);
                }
            }
            return LIST_MODIFIED_FUNCTIONS;
        }
        public static List<string> GetFunctionIdsByRoleID(string pROLE_ID)
        {
            string format = OutgoingResponseFormat.GetFormat();
            OutgoingResponseFormat.SetResponseFormat(format);

            List<string> List_Function_Ids = (from role_d in Obj_DBModelEntities.LG_FNR_ROLE_DEFINE
                                              where role_d.ROLE_ID == pROLE_ID &&
                                                    role_d.AUTH_STATUS_ID == "A" &&
                                                    role_d.ROLE_DEFINE_FLAG == 1
                                              select role_d.FUNCTION_ID).ToList();
            if (List_Function_Ids != null)
                return List_Function_Ids;
            else
                return null;
        }


        private static string AddRolePermission(int serial_id, string role_id, string function_id, string permission_details)
        {
            string result = string.Empty;
            try
            {
                FUNC_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "RoleDefine").Select(x => x.FUNCTION_ID).SingleOrDefault();
                LG_FNR_ROLE_PERMISSION OBJ_LG_FNR_ROLE_PERMISSION = new LG_FNR_ROLE_PERMISSION();

                string permission_id = string.Empty;
                if(string.IsNullOrWhiteSpace(function_id))
                {
                    permission_id = Obj_DBModelEntities.LG_FNR_ROLE_PERMISSION_DETAILS
                                   .Where(pd => pd.PERMISSION_DETAILS == permission_details)
                                   .Select(pd => pd.PERMISSION_ID).SingleOrDefault();
                }
                else
                {
                    permission_id = Obj_DBModelEntities.LG_FNR_ROLE_PERMISSION_DETAILS
                                   .Where(pd => pd.PERMISSION_DETAILS == permission_details &&
                                                pd.FUNCTION_ID == function_id)
                                   .Select(pd => pd.PERMISSION_ID).SingleOrDefault();
                }
                

                //var permission_id = Obj_DBModelEntities.LG_FNR_ROLE_PERMISSION_DETAILS.Single(x => x.PERMISSION_DETAILS == permission_details).PERMISSION_ID;

                if (permission_id != null)
                {
                    OBJ_LG_FNR_ROLE_PERMISSION.SL_ID = serial_id.ToString();
                    OBJ_LG_FNR_ROLE_PERMISSION.ROLE_ID = role_id;
                    OBJ_LG_FNR_ROLE_PERMISSION.PERMISSION_ID = permission_id;

                    OBJ_LG_FNR_ROLE_PERMISSION.AUTH_STATUS_ID = "A";
                    OBJ_LG_FNR_ROLE_PERMISSION.LAST_ACTION = "ADD";
                    OBJ_LG_FNR_ROLE_PERMISSION.ROLE_DEFINE_PERMISSION_FLAG = 1; //CRT, EDT, DEL, DTL, INDEX permission flag
                    OBJ_LG_FNR_ROLE_PERMISSION.FUNCTION_ID = function_id;
                    OBJ_LG_FNR_ROLE_PERMISSION.MAKE_DT = System.DateTime.Now;
                    Obj_DBModelEntities.LG_FNR_ROLE_PERMISSION.Add(OBJ_LG_FNR_ROLE_PERMISSION);
                    Obj_DBModelEntities.SaveChanges();
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
                        result = "Can't Add RolePermission(Db) " + validationError.ErrorMessage;
                    }
                }
                LG_ERROR_LOG_MAP.Add_Error_Log(FUNC_ID, dbEx.Source, "ERR_APP_TYPE", "AddRolePermission",
                                          "0000000000", dbEx.Message, inner, dbEx.StackTrace, null, dateTime);

                return result;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNC_ID, ex.Source, "ERR_APP_TYPE", "AddRolePermission",
                                          "0000000000", ex.Message, inner4, ex.StackTrace, null, dateTime);

                result = "Can't Add RolePermission " + ex.Message;
                return result;
            }   
        }
        private static string UpdateRolePermissionCheck(List<LG_FNR_ROLE_PERMISSION_MAP> LIST_LG_FNR_ROLE_PERMISSION_MAP, string permission_details, string pROLE_ID, string pFUNCTION_ID, int role_define_permission_flag)
        {
            string result = string.Empty;
            bool exists = false;
            try
            {
                foreach (LG_FNR_ROLE_PERMISSION_MAP ITEM_LG_FNR_ROLE_PERMISSION_MAP in LIST_LG_FNR_ROLE_PERMISSION_MAP)
                {
                    //already exits in child,so update data to child table row
                    if (ITEM_LG_FNR_ROLE_PERMISSION_MAP.PERMISSION_DETAILS == permission_details)
                    {
                        exists = true;
                        result = UpdateRolePermission(pROLE_ID, ITEM_LG_FNR_ROLE_PERMISSION_MAP.PERMISSION_ID, role_define_permission_flag);
                        return result;
                    }
                }
                //if not already exits in child then save new row to child table
                if (!exists)
                {
                    int serial_id = (Obj_DBModelEntities.LG_FNR_ROLE_PERMISSION
                                    .Select(i => i.SL_ID).Cast<int?>().Max() ?? 0) + 1;

                    result = AddRolePermission(serial_id, pROLE_ID, pFUNCTION_ID, permission_details);
                    return result;
                }
            }
            catch (Exception ex)
            {
                result = "Can't update role permission " + ex.Message;
                return result;
            }
            return result;
        }
        private static string UpdateRolePermission(string role_id, string permission_id, int role_define_permission_flag)
        {
            string result = string.Empty;
            try
            {
                FUNC_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "RoleDefine").Select(x => x.FUNCTION_ID).SingleOrDefault();
                LG_FNR_ROLE_PERMISSION OBJ_LG_FNR_ROLE_PERMISSION = (from role_per in Obj_DBModelEntities.LG_FNR_ROLE_PERMISSION
                                                                     where role_per.ROLE_ID == role_id &&
                                                                           role_per.PERMISSION_ID == permission_id
                                                                     select role_per).SingleOrDefault();
                OBJ_LG_FNR_ROLE_PERMISSION.AUTH_STATUS_ID = "A";
                OBJ_LG_FNR_ROLE_PERMISSION.LAST_ACTION = "EDT";
                OBJ_LG_FNR_ROLE_PERMISSION.LAST_UPDATE_DT = System.DateTime.Now;
                OBJ_LG_FNR_ROLE_PERMISSION.ROLE_DEFINE_PERMISSION_FLAG = (short)role_define_permission_flag;
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
                        result = "Can't update RolePermission(Db) " + validationError.ErrorMessage;
                    }
                }
                LG_ERROR_LOG_MAP.Add_Error_Log(FUNC_ID, dbEx.Source, "ERR_APP_TYPE", "UpdateRolePermission",
                                          "0000000000", dbEx.Message, inner, dbEx.StackTrace, null, dateTime);

                return result;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNC_ID, ex.Source, "ERR_APP_TYPE", "UpdateRolePermission",
                                          "0000000000", ex.Message, inner4, ex.StackTrace, null, dateTime);

                result = "Can't update role permission " + ex.Message;
                return result;
            }
            return "true";
        }
        private static string UpdateRolePermissionFlag(string role_id, string function_id, int role_define_permission_flag)
        {
            string result = string.Empty;
            try
            {
                FUNC_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "RoleDefine").Select(x => x.FUNCTION_ID).SingleOrDefault();
                List<LG_FNR_ROLE_PERMISSION> OBJ_LG_FNR_ROLE_PERMISSION = (from role_per in Obj_DBModelEntities.LG_FNR_ROLE_PERMISSION
                                                                           where role_per.ROLE_ID == role_id &&
                                                                                 role_per.FUNCTION_ID == function_id
                                                                           select role_per).ToList();
                if (OBJ_LG_FNR_ROLE_PERMISSION.Count() > 0)
                {
                    OBJ_LG_FNR_ROLE_PERMISSION.ForEach(a =>
                    {
                        a.AUTH_STATUS_ID = "A";
                        a.LAST_ACTION = "EDT";
                        a.LAST_UPDATE_DT = System.DateTime.Now;
                        a.ROLE_DEFINE_PERMISSION_FLAG = (short)role_define_permission_flag;
                    });
                    Obj_DBModelEntities.SaveChanges();
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
                        result = "Can't update RolePermission(Db) " + validationError.ErrorMessage;
                    }
                }
                LG_ERROR_LOG_MAP.Add_Error_Log(FUNC_ID, dbEx.Source, "ERR_APP_TYPE", "UpdateRolePermissionFlag",
                                          "0000000000", dbEx.Message, inner, dbEx.StackTrace, null, dateTime);

                return result;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNC_ID, ex.Source, "ERR_APP_TYPE", "UpdateRolePermissionFlag",
                                          "0000000000", ex.Message, inner4, ex.StackTrace, null, dateTime);

                result = "Can't update role permission " + ex.Message;
                return result;
            }
            return "true";
        }
        private static string RemoveFunctionPermission(LG_FNR_ROLE_DEFINE pLG_FNR_ROLE_DEFINE, string pMAKE_BY)
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            DBModelEntities Obj_DBModelEntities1 = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            string result = string.Empty;
            try
            {
                FUNC_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "RoleDefine").Select(x => x.FUNCTION_ID).SingleOrDefault();
                //Remove permission from "Role Define" table
                LG_FNR_ROLE_DEFINE OBJ_LG_FNR_ROLE_DEFINE_OLD = (from role_d in Obj_DBModelEntities1.LG_FNR_ROLE_DEFINE
                                                                 where role_d.ROLE_ID == pLG_FNR_ROLE_DEFINE.ROLE_ID &&
                                                                       role_d.FUNCTION_ID == pLG_FNR_ROLE_DEFINE.FUNCTION_ID
                                                                 select role_d).SingleOrDefault();

                LG_FNR_ROLE_DEFINE OBJ_LG_FNR_ROLE_DEFINE = (from role_d in Obj_DBModelEntities.LG_FNR_ROLE_DEFINE
                                                             where role_d.ROLE_ID == pLG_FNR_ROLE_DEFINE.ROLE_ID &&
                                                                   role_d.FUNCTION_ID == pLG_FNR_ROLE_DEFINE.FUNCTION_ID
                                                             select role_d).SingleOrDefault();
                OBJ_LG_FNR_ROLE_DEFINE.AUTH_STATUS_ID = "U";
                OBJ_LG_FNR_ROLE_DEFINE.LAST_ACTION = "EDT";
                OBJ_LG_FNR_ROLE_DEFINE.LAST_UPDATE_DT = System.DateTime.Now;
                OBJ_LG_FNR_ROLE_DEFINE.ROLE_DEFINE_FLAG = 0;
                Obj_DBModelEntities.SaveChanges();

                #region Auth log
                LG_AA_NFT_AUTH_LOG_MAP OBJ_LG_AA_NFT_AUTH_LOG_MAP = new LG_AA_NFT_AUTH_LOG_MAP();
                OBJ_LG_AA_NFT_AUTH_LOG_MAP.FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "RoleDefine").Select(x => x.FUNCTION_ID).SingleOrDefault();
                OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_NAME = "LG_FNR_ROLE_DEFINE";
                OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_NM = "SL_NO";
                OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_VAL = OBJ_LG_FNR_ROLE_DEFINE.SL_NO != null ? OBJ_LG_FNR_ROLE_DEFINE.SL_NO.ToString() : null;
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
                LG_AA_NFT_AUTH_LOG_MAP.AddNftAuthLog(OBJ_LG_FNR_ROLE_DEFINE_OLD, OBJ_LG_FNR_ROLE_DEFINE, OBJ_LG_AA_NFT_AUTH_LOG_MAP);
                #endregion

                //Remove permission from "Role Permission" table (workable-Can be used in time of need)
                result = RemoveFormPermission(pLG_FNR_ROLE_DEFINE.ROLE_ID, pLG_FNR_ROLE_DEFINE.FUNCTION_ID);
                if (result.ToLower() != "true")
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
                        result = "Can't remove function permission(Db) " + validationError.ErrorMessage;
                    }
                }
                LG_ERROR_LOG_MAP.Add_Error_Log(FUNC_ID, dbEx.Source, "ERR_APP_TYPE", "RemoveFunctionPermission",
                                          "0000000000", dbEx.Message, inner, dbEx.StackTrace, null, dateTime);

                return result;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNC_ID, ex.Source, "ERR_APP_TYPE", "RemoveFunctionPermission",
                                          "0000000000", ex.Message, inner4, ex.StackTrace, null, dateTime);

                result = "Can't remove function permission(Db) " + ex.Message;
                return result;
            }
            return "true";
        }
        private static string RemoveFormPermission(string pROLE_ID, string pFUNCTION_ID)
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            string result = string.Empty;
            try
            {
                FUNC_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "RoleDefine").Select(x => x.FUNCTION_ID).SingleOrDefault();
                LG_FNR_ROLE_PERMISSION OBJ_LG_FNR_ROLE_PERMISSION = null;

                List<LG_FNR_ROLE_PERMISSION> LIST_LG_FNR_ROLE_PERMISSION = (from role_p in Obj_DBModelEntities.LG_FNR_ROLE_PERMISSION
                                                                            where role_p.ROLE_ID == pROLE_ID &&
                                                                                  role_p.FUNCTION_ID == pFUNCTION_ID
                                                                            select role_p).ToList();
                if (LIST_LG_FNR_ROLE_PERMISSION.Count() > 0)
                {
                    //Option-1
                    LIST_LG_FNR_ROLE_PERMISSION.ForEach(a =>
                    {
                        a.AUTH_STATUS_ID = "A";
                        a.LAST_ACTION = "EDT";
                        a.LAST_UPDATE_DT = System.DateTime.Now;
                        a.ROLE_DEFINE_PERMISSION_FLAG = 0;
                    });
                    Obj_DBModelEntities.SaveChanges();

                    //Option-2
                    /*for (int i = 0; i < LIST_LG_FNR_ROLE_PERMISSION.Count(); i++)
                      {
                          OBJ_LG_FNR_ROLE_PERMISSION = new LG_FNR_ROLE_PERMISSION();
                          OBJ_LG_FNR_ROLE_PERMISSION = LIST_LG_FNR_ROLE_PERMISSION[i];
                          OBJ_LG_FNR_ROLE_PERMISSION.AUTH_STATUS_ID = "A";
                          OBJ_LG_FNR_ROLE_PERMISSION.LAST_ACTION = "EDT";
                          OBJ_LG_FNR_ROLE_PERMISSION.LAST_UPDATE_DT = System.DateTime.Now;
                          OBJ_LG_FNR_ROLE_PERMISSION.ROLE_DEFINE_PERMISSION_FLAG = 0;
                          Obj_DBModelEntities.SaveChanges();
                      }*/
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
                        result = "Can't remove form permission(Db) " + validationError.ErrorMessage;
                    }
                }
                LG_ERROR_LOG_MAP.Add_Error_Log(FUNC_ID, dbEx.Source, "ERR_APP_TYPE", "RemoveFormPermission",
                                          "0000000000", dbEx.Message, inner, dbEx.StackTrace, null, dateTime);

                return result;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNC_ID, ex.Source, "ERR_APP_TYPE", "RemoveFormPermission",
                                          "0000000000", ex.Message, inner4, ex.StackTrace, null, dateTime);

                result = "Can't remove form permission" + ex.Message;
                return result;
            }
            return "true";
        }
        #endregion

        #region Validate
        #endregion

        #endregion
    }
}
