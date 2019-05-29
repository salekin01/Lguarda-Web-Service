using Model.EDMX;
using Model.EntityModel.Common;
using Model.Mapper;
using System;
using System.Collections.Generic;
using System.Configuration;
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
    public class LG_USER_ROLE_ASSIGN_MAP
    {
        #region Properties
        [DataMember]
        public int SL_NO { get; set; }
        [DataMember]
        public string USER_ID { get; set; }
        [DataMember]
        public string ROLE_ID { get; set; }
        [DataMember]
        public string ROLE_NAME { get; set; }
        [DataMember]
        public string MAKE_BY { get; set; }
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public DateTime MAKE_DT { get; set; }
        [DataMember]
        public string APPLICATION_ID { get; set; }
        [DataMember]
        public string ERROR { get; set; }
        [DataMember]
        public string ROLE_ID_FOR_IND_USER { get; set; }
        [DataMember]
        public string AUTH_STATUS_ID { get; set; }
        [DataMember]
        public string LAST_ACTION { get; set; }
        [DataMember]
        public DateTime? LAST_UPDATE_DT { get; set; }
        [DataMember]
        public string USER_NM { get; set; }
        [DataMember]
        public string APPLICATION_NAME { get; set; }
        [DataMember]
        public string ROLE_ASSIGN_COMMAND { get; set; }

        [DataMember]
        public List<SelectListItem> APPLICATION_LIST_FOR_DD { get; set; }
        [DataMember]
        public List<SelectListItem> ROLE_LIST_FOR_IND_USER { get; set; }
        #endregion


        #region Events
        public static string FUNC_ID = null;
        #region Add New
        public static string AddRoleAssign(string ROLE_ID_FOR_IND_USER, string pROLE_ASSIGN_COMMAND, string pUSER_ID, string pMAKE_BY)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
                LG_USER_ROLE_ASSIGN OBJ_LG_USER_ROLE_ASSIGN = new LG_USER_ROLE_ASSIGN();
                string result = string.Empty;
                string FUNCTION_ID = string.Empty;
                try
                {
                    FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "RoleAssign").Select(x => x.FUNCTION_ID).SingleOrDefault();
                    string format = OutgoingResponseFormat.GetFormat();
                    OutgoingResponseFormat.SetResponseFormat(format);

                    if (string.IsNullOrWhiteSpace(pROLE_ASSIGN_COMMAND) && pROLE_ASSIGN_COMMAND.ToLower() == "save" && (Obj_DBModelEntities.LG_USER_ROLE_ASSIGN.Where(a => a.USER_ID == pUSER_ID).ToList().Count() > 0))  //closed for deployment
                    {
                        return "User Role already assigned";
                    }

                    string[] Arr_Role_Id = ROLE_ID_FOR_IND_USER.Split(',');
                    foreach (string ind_role_id in Arr_Role_Id)
                    {
                        var List_appid_under_single_roleid = Obj_DBModelEntities.LG_FNR_ROLE_DEFINE
                                                            .Where(a => a.ROLE_ID == ind_role_id &&
                                                                        a.AUTH_STATUS_ID == "A" &&
                                                                        a.LAST_ACTION != "DEL")
                                                            .GroupBy(a => a.APPLICATION_ID)
                                                            .Select(a => a.Key)
                                                            .ToList();

                        for (int i = 0; i < List_appid_under_single_roleid.Count(); i++) //when there are multiple application's function permission were setted in a single role
                        {
                            //single row insert for different applicationId
                            OBJ_LG_USER_ROLE_ASSIGN = new LG_USER_ROLE_ASSIGN();
                            OBJ_LG_USER_ROLE_ASSIGN.ROLE_ID = ind_role_id;

                            int serial_id = Convert.ToInt32(Obj_DBModelEntities.LG_USER_ROLE_ASSIGN
                                           .Max(x => (int?)x.SL_NO) ?? 0) + 1;

                            OBJ_LG_USER_ROLE_ASSIGN.SL_NO = serial_id;
                            OBJ_LG_USER_ROLE_ASSIGN.USER_ID = pUSER_ID;
                            OBJ_LG_USER_ROLE_ASSIGN.ROLE_ID = ind_role_id;
                            OBJ_LG_USER_ROLE_ASSIGN.APPLICATION_ID = List_appid_under_single_roleid[i];
                            OBJ_LG_USER_ROLE_ASSIGN.ROLE_NAME = (Obj_DBModelEntities.LG_FNR_ROLE.Where(r => r.ROLE_ID == ind_role_id).Select(r => r.ROLE_NAME)).SingleOrDefault();
                            OBJ_LG_USER_ROLE_ASSIGN.AUTH_STATUS_ID = "U";
                            OBJ_LG_USER_ROLE_ASSIGN.LAST_ACTION = "ADD";
                            OBJ_LG_USER_ROLE_ASSIGN.MAKE_DT = System.DateTime.Now;
                            OBJ_LG_USER_ROLE_ASSIGN.ROLE_ASSIGN_FLAG = 1;
                            Obj_DBModelEntities.LG_USER_ROLE_ASSIGN.Add(OBJ_LG_USER_ROLE_ASSIGN);
                            Obj_DBModelEntities.SaveChanges();

                            #region Auth log
                            LG_AA_NFT_AUTH_LOG_MAP OBJ_LG_AA_NFT_AUTH_LOG_MAP = new LG_AA_NFT_AUTH_LOG_MAP();
                            OBJ_LG_AA_NFT_AUTH_LOG_MAP.FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "RoleAssign").Select(x => x.FUNCTION_ID).SingleOrDefault();
                            OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_NAME = "LG_USER_ROLE_ASSIGN";
                            OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_NM = "SL_NO";
                            OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_VAL = OBJ_LG_USER_ROLE_ASSIGN.SL_NO.ToString();
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
                            OBJ_LG_AA_NFT_AUTH_LOG_MAP.MAKE_BY = pMAKE_BY;
                            OBJ_LG_AA_NFT_AUTH_LOG_MAP.MAKE_DT = DateTime.Now;
                            LG_AA_NFT_AUTH_LOG_MAP.AddNftAuthLog(OBJ_LG_USER_ROLE_ASSIGN, OBJ_LG_AA_NFT_AUTH_LOG_MAP);
                            #endregion
                        }
                    }
                    ts.Complete();
                    result = "true";
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
                            result = "Can't add User Role(Db).";
                        }
                    }

                    LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "AddRoleAssign",
                                                  "0000000000", dbEx.Message, inner, dbEx.StackTrace, pMAKE_BY, dateTime);

                    return result;
                }
                catch (Exception ex)
                {
                    ts.Dispose();
                    string dateTime = Convert.ToString(System.DateTime.Now);
                    string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                    LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "AddRoleAssign",
                                                  "0000000000", ex.Message, inner4, ex.StackTrace, pMAKE_BY, dateTime);

                    result = "Can't add User Role";
                    return result;
                }
            }
        }
        public static string AddRoleAssignForAuthorizedUser(string ROLE_ID_FOR_IND_USER, string pROLE_ASSIGN_COMMAND, string pUSER_ID, string pMAKE_BY, DBModelEntities Obj_DBModelEntities)
        {
            LG_USER_ROLE_ASSIGN OBJ_LG_USER_ROLE_ASSIGN = new LG_USER_ROLE_ASSIGN();
            string result = string.Empty;
            string FUNCTION_ID = string.Empty;
            try
            {
                FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "RoleAssign").Select(x => x.FUNCTION_ID).SingleOrDefault();
                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                if (string.IsNullOrWhiteSpace(pROLE_ASSIGN_COMMAND) && pROLE_ASSIGN_COMMAND.ToLower() == "save" && (Obj_DBModelEntities.LG_USER_ROLE_ASSIGN.Where(a => a.USER_ID == pUSER_ID).ToList().Count() > 0))  //closed for deployment
                {
                    return "User Role already assigned";
                }

                string[] Arr_Role_Id = ROLE_ID_FOR_IND_USER.Split(',');
                foreach (string ind_role_id in Arr_Role_Id)
                {
                    var List_appid_under_single_roleid = Obj_DBModelEntities.LG_FNR_ROLE_DEFINE
                                                        .Where(a => a.ROLE_ID == ind_role_id &&
                                                                    a.AUTH_STATUS_ID == "A" &&
                                                                    a.LAST_ACTION != "DEL")
                                                        .GroupBy(a => a.APPLICATION_ID)
                                                        .Select(a => a.Key)
                                                        .ToList();

                    for (int i = 0; i < List_appid_under_single_roleid.Count(); i++) //when there are multiple application's function permission were setted in a single role
                    {
                        //single row insert for different applicationId
                        OBJ_LG_USER_ROLE_ASSIGN = new LG_USER_ROLE_ASSIGN();
                        OBJ_LG_USER_ROLE_ASSIGN.ROLE_ID = ind_role_id;

                        int serial_id = Convert.ToInt32(Obj_DBModelEntities.LG_USER_ROLE_ASSIGN
                                       .Max(x => (int?)x.SL_NO) ?? 0) + 1;

                        OBJ_LG_USER_ROLE_ASSIGN.SL_NO = serial_id;
                        OBJ_LG_USER_ROLE_ASSIGN.USER_ID = pUSER_ID;
                        OBJ_LG_USER_ROLE_ASSIGN.ROLE_ID = ind_role_id;
                        OBJ_LG_USER_ROLE_ASSIGN.APPLICATION_ID = List_appid_under_single_roleid[i];
                        OBJ_LG_USER_ROLE_ASSIGN.ROLE_NAME = (Obj_DBModelEntities.LG_FNR_ROLE.Where(r => r.ROLE_ID == ind_role_id).Select(r => r.ROLE_NAME)).SingleOrDefault();
                        OBJ_LG_USER_ROLE_ASSIGN.AUTH_STATUS_ID = "A";
                        OBJ_LG_USER_ROLE_ASSIGN.LAST_ACTION = "ADD";
                        OBJ_LG_USER_ROLE_ASSIGN.MAKE_DT = System.DateTime.Now;
                        OBJ_LG_USER_ROLE_ASSIGN.ROLE_ASSIGN_FLAG = 1;
                        Obj_DBModelEntities.LG_USER_ROLE_ASSIGN.Add(OBJ_LG_USER_ROLE_ASSIGN);
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
                        result = "Can't add User Role(Db).";
                    }
                }

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "AddRoleAssignForAuthorizedUser",
                                              "0000000000", dbEx.Message, inner, dbEx.StackTrace, pMAKE_BY, dateTime);

                return result;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "AddRoleAssignForAuthorizedUser",
                                              "0000000000", ex.Message, inner4, ex.StackTrace, pMAKE_BY, dateTime);

                result = "Can't add User Role";
                return result;
            }
        }
        #endregion

        #region Update
        public static string UpdateRoleAssign(string pROLE_ID_FOR_IND_USER, string pROLE_ASSIGN_COMMAND, string pUSER_ID, string pMAKE_BY)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
                List<string> LIST_NEW_SELECTED_ROLE_ID = new List<string>();
                List<string> LIST_OLD_REMOVED_ROLE_ID = new List<string>();
                string ITEM_LIST_NEW_LG_USER_ROLE_ASSIGN = string.Empty;
                string result = string.Empty;
                string FUNCTION_ID = string.Empty;
                bool exists;

                try
                {
                    FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "RoleAssign").Select(x => x.FUNCTION_ID).SingleOrDefault();
                    string format = OutgoingResponseFormat.GetFormat();
                    OutgoingResponseFormat.SetResponseFormat(format);


                    List<string> LIST_OLD_LG_USER_ROLE_ASSIGN = (Obj_DBModelEntities.LG_USER_ROLE_ASSIGN
                                                                        .Where(role_a => role_a.USER_ID == pUSER_ID
                                                                                        && role_a.LAST_ACTION != "DEL")
                        //used distinct as there can be multiple application's function permission setted in a single role & if so then the role'll be exists more than one time
                                                                        .Select(role_a => role_a.ROLE_ID)).ToList();

                    LIST_OLD_LG_USER_ROLE_ASSIGN = LIST_OLD_LG_USER_ROLE_ASSIGN.Distinct().ToList();

                    string[] ARR_NEW_LIST;
                    if (pROLE_ID_FOR_IND_USER.Any(char.IsLetterOrDigit))
                    {
                        ARR_NEW_LIST = pROLE_ID_FOR_IND_USER.Split(',');
                    }
                    else
                        ARR_NEW_LIST = null;


                    List<string> LIST_NEW_LG_USER_ROLE_ASSIGN = new List<string>();
                    if (ARR_NEW_LIST != null)
                    {
                        LIST_NEW_LG_USER_ROLE_ASSIGN = (ARR_NEW_LIST.ToArray() as string[]).ToList();
                    }

                    foreach (string ITEM_LIST_OLD_LG_USER_ROLE_ASSIGN in LIST_OLD_LG_USER_ROLE_ASSIGN.ToList()) //after completing 2 foreach loop iteration, the remaining "old list" will be the deleted roles
                    {
                        exists = false;
                        foreach (string ITEM_LIST_NEW_LG_USER_ROLE_ASSIGN1 in LIST_NEW_LG_USER_ROLE_ASSIGN) //after completing 2 foreach loop iteration, the remaining "selected list" will be the New selected roles
                        {
                            if (ITEM_LIST_OLD_LG_USER_ROLE_ASSIGN == ITEM_LIST_NEW_LG_USER_ROLE_ASSIGN1)
                            {
                                exists = true;
                                ITEM_LIST_NEW_LG_USER_ROLE_ASSIGN = ITEM_LIST_NEW_LG_USER_ROLE_ASSIGN1;
                                var itemToRemove = LIST_NEW_LG_USER_ROLE_ASSIGN.Single(list => list.Contains(ITEM_LIST_NEW_LG_USER_ROLE_ASSIGN));
                                LIST_NEW_LG_USER_ROLE_ASSIGN.Remove(itemToRemove);  //Minimizing the List if true, so that foreach loop will iterate less
                                var itemToRemove1 = LIST_OLD_LG_USER_ROLE_ASSIGN.Single(list => list.Contains(ITEM_LIST_OLD_LG_USER_ROLE_ASSIGN));
                                LIST_OLD_LG_USER_ROLE_ASSIGN.Remove(itemToRemove1);
                                break;
                            }
                        }
                        if (exists)
                        {
                            var List_appid_under_single_roleid = Obj_DBModelEntities.LG_USER_ROLE_ASSIGN
                                                                .Where(a => a.ROLE_ID == ITEM_LIST_OLD_LG_USER_ROLE_ASSIGN &&
                                                                        a.USER_ID == pUSER_ID &&
                                                                        a.AUTH_STATUS_ID == "A" &&
                                                                        a.LAST_ACTION != "DEL")
                                                                .GroupBy(a => a.APPLICATION_ID)
                                                                .Select(a => a.Key)
                                                                .ToList();

                            string appid_under_single_roleid = string.Empty;
                            for (int i = 0; i < List_appid_under_single_roleid.Count(); i++) //when there are multiple application's function permission were setted in a single role
                            {
                                appid_under_single_roleid = List_appid_under_single_roleid[i].ToString();
                                DBModelEntities Obj_DBModelEntities1 = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
                                LG_USER_ROLE_ASSIGN OBJ_LG_USER_ROLE_ASSIGN_OLD = Obj_DBModelEntities1.LG_USER_ROLE_ASSIGN
                                                                             .Where(role_a => role_a.ROLE_ID == ITEM_LIST_OLD_LG_USER_ROLE_ASSIGN &&
                                                                                              role_a.LAST_ACTION != "DEL" &&
                                                                                              role_a.USER_ID == pUSER_ID &&
                                                                                              role_a.APPLICATION_ID == appid_under_single_roleid)
                                                                             .Select(role_a => role_a).SingleOrDefault();


                                LG_USER_ROLE_ASSIGN OBJ_LG_USER_ROLE_ASSIGN = Obj_DBModelEntities.LG_USER_ROLE_ASSIGN
                                                                             .Where(role_a => role_a.ROLE_ID == ITEM_LIST_OLD_LG_USER_ROLE_ASSIGN &&
                                                                                              role_a.LAST_ACTION != "DEL" &&
                                                                                              role_a.USER_ID == pUSER_ID &&
                                                                                              role_a.APPLICATION_ID == appid_under_single_roleid)
                                                                             .Select(role_a => role_a).SingleOrDefault();
                                if (OBJ_LG_USER_ROLE_ASSIGN.ROLE_ASSIGN_FLAG != 1)
                                {
                                    OBJ_LG_USER_ROLE_ASSIGN.ROLE_ASSIGN_FLAG = 1;
                                    OBJ_LG_USER_ROLE_ASSIGN.LAST_ACTION = "EDT";
                                    OBJ_LG_USER_ROLE_ASSIGN.AUTH_STATUS_ID = "U";
                                    OBJ_LG_USER_ROLE_ASSIGN.LAST_UPDATE_DT = System.DateTime.Now;
                                    Obj_DBModelEntities.SaveChanges();

                                    #region Auth log
                                    LG_AA_NFT_AUTH_LOG_MAP OBJ_LG_AA_NFT_AUTH_LOG_MAP = new LG_AA_NFT_AUTH_LOG_MAP();
                                    FUNC_ID = OBJ_LG_AA_NFT_AUTH_LOG_MAP.FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "RoleAssign").Select(x => x.FUNCTION_ID).SingleOrDefault();
                                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_NAME = "LG_USER_ROLE_ASSIGN";
                                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_NM = "SL_NO";
                                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_VAL = OBJ_LG_USER_ROLE_ASSIGN.SL_NO.ToString();
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
                                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.MAKE_BY = pMAKE_BY;
                                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.MAKE_DT = DateTime.Now;
                                    //LG_AA_NFT_AUTH_LOG_MAP.AddNftAuthLog(OBJ_LG_USER_ROLE_ASSIGN, OBJ_LG_AA_NFT_AUTH_LOG_MAP);
                                    LG_AA_NFT_AUTH_LOG_MAP.AddNftAuthLog(OBJ_LG_USER_ROLE_ASSIGN_OLD, OBJ_LG_USER_ROLE_ASSIGN, OBJ_LG_AA_NFT_AUTH_LOG_MAP);
                                    #endregion
                                }
                            }
                        }
                    }

                    LIST_OLD_REMOVED_ROLE_ID = LIST_OLD_LG_USER_ROLE_ASSIGN;            //Store all old removed role
                    LIST_NEW_SELECTED_ROLE_ID = LIST_NEW_LG_USER_ROLE_ASSIGN;           //Store all newly added role 

                    //remove old unselect role
                    if (LIST_OLD_REMOVED_ROLE_ID.Count() > 0)
                    {
                        //for (int i = 0; i < LIST_OLD_REMOVED_ROLE_ID.Count(); i++)
                        //{
                        string ROLE_ID_FOR_IND_USER = string.Join(",", LIST_OLD_REMOVED_ROLE_ID.ToArray());
                        result = RemoveRoleAssign(ROLE_ID_FOR_IND_USER, pUSER_ID, pMAKE_BY);
                        if (result.ToLower() != "true")
                            return result;
                        //}
                    }

                    //Add newly added role
                    if (LIST_NEW_SELECTED_ROLE_ID.Count() > 0)
                    {
                        string ROLE_ID_FOR_IND_USER = string.Join(",", LIST_NEW_SELECTED_ROLE_ID.ToArray());
                        result = AddRoleAssign(ROLE_ID_FOR_IND_USER, pROLE_ASSIGN_COMMAND, pUSER_ID, pMAKE_BY);
                        if (result.ToLower() != "true")
                            return result;
                    }
                    ts.Complete();
                    result = "true";
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
                            result = "Can't update User Role(Db).";
                        }
                    }

                    LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "UpdateRoleAssign",
                                                  "0000000000", dbEx.Message, inner, dbEx.StackTrace, pMAKE_BY, dateTime);

                    return result;
                }
                catch (Exception ex)
                {
                    ts.Dispose();
                    string dateTime = Convert.ToString(System.DateTime.Now);
                    string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                    LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "UpdateRoleAssign",
                                                  "0000000000", ex.Message, inner4, ex.StackTrace, pMAKE_BY, dateTime);

                    result = "Can't update User Role.";
                    return result;
                }
            }
        }
        #endregion

        #region Delete
        #endregion

        #region Fetch Single
        public static LG_USER_ROLE_ASSIGN_MAP GetRoleAssignInfoByUserId(string pUSER_ID)// Edit Role assign
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            LG_USER_ROLE_ASSIGN_MAP OBJ_LG_USER_ROLE_ASSIGN_MAP = new LG_USER_ROLE_ASSIGN_MAP();
            string FUNCTION_ID = string.Empty;

            try
            {
                FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "RoleAssign").Select(x => x.FUNCTION_ID).SingleOrDefault();
                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                OBJ_LG_USER_ROLE_ASSIGN_MAP = (from u in Obj_DBModelEntities.LG_USER_SETUP_PROFILE
                                               join role_a in Obj_DBModelEntities.LG_USER_ROLE_ASSIGN
                                               on u.USER_ID equals role_a.USER_ID

                                               //join a in Obj_DBModelEntities.LG_FNR_APPLICATION
                                               //on role_a.APPLICATION_ID equals a.APPLICATION_ID
                                               where role_a.USER_ID == pUSER_ID &&
                                                     role_a.AUTH_STATUS_ID == "A" &&
                                                     role_a.LAST_ACTION != "DEL"

                                               select new LG_USER_ROLE_ASSIGN_MAP
                                               {
                                                   USER_ID = pUSER_ID,
                                                   USER_NM = u.USER_NM,

                                                   //APPLICATION_ID = role_a.APPLICATION_ID,
                                                   //APPLICATION_NAME = a.APPLICATION_NAME,
                                               }).FirstOrDefault();
                return OBJ_LG_USER_ROLE_ASSIGN_MAP;
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
                        OBJ_LG_USER_ROLE_ASSIGN_MAP.ERROR = validationError.ErrorMessage;
                    }
                }

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "GetRoleAssignInfoByUserId",
                                              "0000000000", dbEx.Message, inner, dbEx.StackTrace, null, dateTime);

                return OBJ_LG_USER_ROLE_ASSIGN_MAP;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "GetRoleAssignInfoByUserId",
                                              "0000000000", ex.Message, inner4, ex.StackTrace, null, dateTime);

                OBJ_LG_USER_ROLE_ASSIGN_MAP.ERROR = ex.Message;
                return OBJ_LG_USER_ROLE_ASSIGN_MAP;
            }
        }
        #endregion

        #region Fetch all
        public static IEnumerable<LG_USER_ROLE_ASSIGN_MAP> GetAllRoleAssignedInfo() //For Index
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            LG_USER_ROLE_ASSIGN_MAP OBJ_LG_USER_ROLE_ASSIGN_MAP = new LG_USER_ROLE_ASSIGN_MAP();
            List<LG_USER_ROLE_ASSIGN_MAP> LIST_MAX_LG_USER_ROLE_ASSIGN_MAP = new List<LG_USER_ROLE_ASSIGN_MAP>();
            List<LG_USER_ROLE_ASSIGN_MAP> LIST_MIN_LG_USER_ROLE_ASSIGN_MAP = new List<LG_USER_ROLE_ASSIGN_MAP>();
            List<LG_USER_ROLE_ASSIGN_MAP> LIST_USER_WISE_ROLES = new List<LG_USER_ROLE_ASSIGN_MAP>();
            string FUNCTION_ID = string.Empty;
            bool exists;
            try
            {
                FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "RoleAssign").Select(x => x.FUNCTION_ID).SingleOrDefault();
                Obj_DBModelEntities.Configuration.LazyLoadingEnabled = false;
                Obj_DBModelEntities.Configuration.ProxyCreationEnabled = false;

                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                //Fetch same Users with multiple row
                LIST_MAX_LG_USER_ROLE_ASSIGN_MAP = Obj_DBModelEntities.LG_USER_ROLE_ASSIGN
                                                  .Join(Obj_DBModelEntities.LG_USER_SETUP_PROFILE, role_a => role_a.USER_ID, u => u.USER_ID, (role_a, u) => new { role_a, u })
                                                  .Where(T => T.u.USER_ID != null /*&& T.u.LAST_ACTION != "DEL"*/ &&
                                                      //T.role_a.AUTH_STATUS_ID == "A" && 
                                                         T.role_a.LAST_ACTION != "DEL")
                                                  .OrderBy(T => T.role_a.USER_ID)
                                                  .Select(T =>
                                                  new LG_USER_ROLE_ASSIGN_MAP
                                                  {
                                                      USER_ID = T.role_a.USER_ID,
                                                      USER_NM = T.u.USER_NM,
                                                      ROLE_ID = T.role_a.ROLE_ID,
                                                      AUTH_STATUS_ID = T.role_a.AUTH_STATUS_ID,
                                                  }).ToList();

                //Fetch same Users with only one row
                LIST_MIN_LG_USER_ROLE_ASSIGN_MAP = Obj_DBModelEntities.LG_USER_ROLE_ASSIGN
                    //.Where(T => T.USER_ID != null && T.AUTH_STATUS_ID == "A" && T.LAST_ACTION != "DEL")
                                                  .GroupBy(T => T.USER_ID)
                                                  .Select(T =>
                                                  new LG_USER_ROLE_ASSIGN_MAP
                                                  {
                                                      USER_ID = T.Key
                                                  }).OrderByDescending(T => T.USER_ID).ToList();

                //if any role of a single user is Unauthorized then the user will be shown unauthorized in Index List(Total Index list == LIST_MIN_LG_USER_ROLE_ASSIGN_MAP List )
                List<LG_USER_ROLE_ASSIGN_MAP> LIST_MIN_LG_USER_ROLE_ASSIGN_MAP_ORDER_ASC = new List<LG_USER_ROLE_ASSIGN_MAP>();
                if (LIST_MIN_LG_USER_ROLE_ASSIGN_MAP.Count() > 0)
                {
                    for (int i = 0; i < LIST_MIN_LG_USER_ROLE_ASSIGN_MAP.Count(); i++)
                    {
                        string user_id = LIST_MIN_LG_USER_ROLE_ASSIGN_MAP[i].USER_ID;
                        LIST_USER_WISE_ROLES = (List<LG_USER_ROLE_ASSIGN_MAP>)LIST_MAX_LG_USER_ROLE_ASSIGN_MAP.Where(role_a => role_a.USER_ID == user_id).ToList();
                        exists = false;
                        int j;
                        for (j = 0; j < LIST_USER_WISE_ROLES.Count(); j++)
                        {
                            if (LIST_USER_WISE_ROLES[j].AUTH_STATUS_ID == "U")
                            {
                                exists = true;
                                break;
                            }
                        }
                        if (exists)
                        {
                            LIST_MIN_LG_USER_ROLE_ASSIGN_MAP.RemoveAll(t => t.USER_ID == LIST_USER_WISE_ROLES[j].USER_ID);  //unauthorized user won't show in index
                            i = i - 1;
                            //LIST_MIN_LG_USER_ROLE_ASSIGN_MAP[i].AUTH_STATUS_ID = "U";
                            //LIST_MIN_LG_USER_ROLE_ASSIGN_MAP[i].USER_NM = LIST_USER_WISE_ROLES[j].USER_NM;
                        }
                        else
                        {
                            LIST_MIN_LG_USER_ROLE_ASSIGN_MAP[i].AUTH_STATUS_ID = "A";
                            LIST_MIN_LG_USER_ROLE_ASSIGN_MAP[i].USER_NM = LIST_USER_WISE_ROLES[j - 1].USER_NM;
                        }
                    }
                    LIST_MIN_LG_USER_ROLE_ASSIGN_MAP_ORDER_ASC = (List<LG_USER_ROLE_ASSIGN_MAP>)LIST_MIN_LG_USER_ROLE_ASSIGN_MAP.OrderBy(r => r.USER_ID).ToList();
                }
                return LIST_MIN_LG_USER_ROLE_ASSIGN_MAP_ORDER_ASC;
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
                        OBJ_LG_USER_ROLE_ASSIGN_MAP.ERROR = validationError.ErrorMessage;
                        LIST_MIN_LG_USER_ROLE_ASSIGN_MAP.Add(OBJ_LG_USER_ROLE_ASSIGN_MAP);
                    }
                }

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "GetAllRoleAssignedInfo",
                                              "0000000000", dbEx.Message, inner, dbEx.StackTrace, null, dateTime);

                return LIST_MIN_LG_USER_ROLE_ASSIGN_MAP;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "GetAllRoleAssignedInfo",
                                              "0000000000", ex.Message, inner4, ex.StackTrace, null, dateTime);

                OBJ_LG_USER_ROLE_ASSIGN_MAP.ERROR = ex.Message;
                LIST_MIN_LG_USER_ROLE_ASSIGN_MAP.Add(OBJ_LG_USER_ROLE_ASSIGN_MAP);
                return LIST_MIN_LG_USER_ROLE_ASSIGN_MAP;
            }
        }
        #endregion

        #region Custom Methods
        public static IEnumerable<SelectListItem> GetRolesByApplicationID(string pAPPLICATION_ID)
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            string FUNCTION_ID = string.Empty;
            try
            {
                FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "RoleAssign").Select(x => x.FUNCTION_ID).SingleOrDefault();
                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);
                var selectList = new List<SelectListItem>();

                if (string.IsNullOrEmpty(pAPPLICATION_ID))
                {
                    return null;
                }

                var List_Roles = Obj_DBModelEntities.LG_FNR_ROLE_DEFINE
                                                  .Join(Obj_DBModelEntities.LG_FNR_ROLE, role_d => role_d.ROLE_ID, role => role.ROLE_ID, (role_d, role) => new { role_d, role })
                                                  .Where(T => T.role_d.APPLICATION_ID == pAPPLICATION_ID &&
                                                              T.role_d.AUTH_STATUS_ID == "A" &&
                                                              T.role_d.LAST_ACTION != "DEL" &&
                                                              T.role_d.ROLE_DEFINE_FLAG == 1)
                                                  .GroupBy(T => new { T.role_d.ROLE_ID, T.role.ROLE_NAME })
                                                  .Select(T => new
                                                  {
                                                      ROLE_ID = T.Key.ROLE_ID,
                                                      ROLE_NM = T.Key.ROLE_NAME,
                                                  }).ToList();

                if (List_Roles.Count() > 0)
                {
                    foreach (var element in List_Roles)
                    {
                        selectList.Add(new SelectListItem
                        {
                            Value = element.ROLE_ID.ToString(),
                            Text = element.ROLE_NM
                        });
                    }
                }
                else
                    return null;

                return selectList;
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
                        string error = validationError.ErrorMessage;
                    }
                }

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "GetRolesByApplicationID",
                                              "0000000000", dbEx.Message, inner, dbEx.StackTrace, null, dateTime);

                return null;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "GetRolesByApplicationID",
                                              "0000000000", ex.Message, inner4, ex.StackTrace, null, dateTime);

                return null;
            }
        }
        public static IEnumerable<SelectListItem> GetAllRoles()
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            string FUNCTION_ID = string.Empty;
            try
            {
                FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "RoleAssign").Select(x => x.FUNCTION_ID).SingleOrDefault();
                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);
                var selectList = new List<SelectListItem>();

                var List_Roles = Obj_DBModelEntities.LG_FNR_ROLE_DEFINE
                                                  .Join(Obj_DBModelEntities.LG_FNR_ROLE, role_d => role_d.ROLE_ID, role => role.ROLE_ID, (role_d, role) => new { role_d, role })
                                                  .Where(T => T.role_d.AUTH_STATUS_ID == "A" &&
                                                              T.role_d.LAST_ACTION != "DEL" &&
                                                      //T.role_d.APPLICATION_ID == pAPPLICATION_ID &&
                                                              T.role_d.ROLE_DEFINE_FLAG == 1)
                                                  .GroupBy(T => new { T.role_d.ROLE_ID, T.role.ROLE_NAME })
                                                  .Select(T => new
                                                  {
                                                      ROLE_ID = T.Key.ROLE_ID,
                                                      ROLE_NM = T.Key.ROLE_NAME,
                                                  }).OrderBy(T => T.ROLE_ID).ToList();

                if (List_Roles.Count() > 0)
                {
                    foreach (var element in List_Roles)
                    {
                        selectList.Add(new SelectListItem
                        {
                            Value = element.ROLE_ID.ToString(),
                            Text = element.ROLE_NM
                        });
                    }
                }
                else
                    return null;

                return selectList;
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
                        string error = validationError.ErrorMessage;
                    }
                }

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "GetAllRoles",
                                              "0000000000", dbEx.Message, inner, dbEx.StackTrace, null, dateTime);

                return null;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "GetAllRoles",
                                              "0000000000", ex.Message, inner4, ex.StackTrace, null, dateTime);

                return null;
            }
        }
        public static IEnumerable<SelectListItem> Get_AllAuthorizedRoles_ByUserId(string pUSER_ID)
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            string FUNCTION_ID = string.Empty;
            try
            {
                FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "RoleAssign").Select(x => x.FUNCTION_ID).SingleOrDefault();
                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);
                var selectList = new List<SelectListItem>();

                var List_Roles = Obj_DBModelEntities.LG_FNR_ROLE_DEFINE
                                                  .Join(Obj_DBModelEntities.LG_FNR_ROLE, role_d => role_d.ROLE_ID, role => role.ROLE_ID, (role_d, role) => new { role_d, role })
                                                  .Where(T => T.role_d.AUTH_STATUS_ID == "A" &&
                                                              T.role_d.LAST_ACTION != "DEL" &&
                                                      //T.role_d.APPLICATION_ID == pAPPLICATION_ID &&
                                                              T.role_d.ROLE_DEFINE_FLAG == 1)
                                                  .GroupBy(T => new { T.role_d.ROLE_ID, T.role.ROLE_NAME })
                                                  .Select(T => new
                                                  {
                                                      ROLE_ID = T.Key.ROLE_ID,
                                                      ROLE_NM = T.Key.ROLE_NAME,
                                                  }).OrderBy(T => T.ROLE_ID).ToList();

                if (List_Roles.Count() > 0)
                {
                    foreach (var element in List_Roles)
                    {
                        var unAuthorized_List_Roles = Obj_DBModelEntities.LG_USER_ROLE_ASSIGN
                                                     .Where(T => T.AUTH_STATUS_ID == "U" &&
                                                            T.USER_ID == pUSER_ID)
                                                     .Select(T => T.ROLE_ID).ToList();

                        bool unAuthorized_Role_found = false;
                        foreach (var item in unAuthorized_List_Roles)
                        {
                            if (item == element.ROLE_ID)
                            {
                                unAuthorized_Role_found = true;
                                break;
                            }
                        }
                        if (unAuthorized_Role_found == false)
                        {
                            selectList.Add(new SelectListItem
                            {
                                Value = element.ROLE_ID.ToString(),
                                Text = element.ROLE_NM
                            });
                        }
                    }
                }
                else
                    return null;

                return selectList;
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
                        string error = validationError.ErrorMessage;
                    }
                }

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "Get_AllAuthorizedRoles_ByUserId",
                                              "0000000000", dbEx.Message, inner, dbEx.StackTrace, null, dateTime);

                return null;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "Get_AllAuthorizedRoles_ByUserId",
                                              "0000000000", ex.Message, inner4, ex.StackTrace, null, dateTime);

                return null;
            }
        }
        public static LG_USER_ROLE_ASSIGN_MAP GetUserInfoByUserId(string pUSER_ID)
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            LG_USER_ROLE_ASSIGN_MAP OBJ_LG_USER_ROLE_ASSIGN_MAP = new LG_USER_ROLE_ASSIGN_MAP();
            string FUNCTION_ID = string.Empty;
            try
            {
                FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "RoleAssign").Select(x => x.FUNCTION_ID).SingleOrDefault();
                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                OBJ_LG_USER_ROLE_ASSIGN_MAP = (from u in Obj_DBModelEntities.LG_USER_SETUP_PROFILE
                                               where u.USER_ID == pUSER_ID && u.LAST_ACTION != "DEL"
                                               select new LG_USER_ROLE_ASSIGN_MAP
                                               {
                                                   USER_ID = u.USER_ID,
                                                   USER_NM = u.USER_NM,
                                                   AUTH_STATUS_ID = u.AUTH_STATUS_ID,
                                                   LAST_ACTION = u.LAST_ACTION,
                                                   LAST_UPDATE_DT = u.LAST_UPDATE_DT,
                                                   //MAKE_DT = u.MAKE_DT
                                               }).SingleOrDefault();
                return OBJ_LG_USER_ROLE_ASSIGN_MAP;
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
                        OBJ_LG_USER_ROLE_ASSIGN_MAP.ERROR = validationError.ErrorMessage;
                    }
                }

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "GetUserInfoByUserId",
                                              "0000000000", dbEx.Message, inner, dbEx.StackTrace, null, dateTime);

                return OBJ_LG_USER_ROLE_ASSIGN_MAP;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "GetUserInfoByUserId",
                                              "0000000000", ex.Message, inner4, ex.StackTrace, null, dateTime);

                OBJ_LG_USER_ROLE_ASSIGN_MAP.ERROR = ex.Message;
                return OBJ_LG_USER_ROLE_ASSIGN_MAP;
            }
        }

        public static List<string> GetRoleIdsByUserID(string pUSER_ID)
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            string FUNCTION_ID = string.Empty;
            try
            {
                FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "RoleAssign").Select(x => x.FUNCTION_ID).SingleOrDefault();
                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                List<string> List_RoleIds = (from role_a in Obj_DBModelEntities.LG_USER_ROLE_ASSIGN
                                             where role_a.USER_ID == pUSER_ID &&
                                                   role_a.AUTH_STATUS_ID == "A" &&
                                                   role_a.LAST_ACTION != "DEL" &&
                                                   role_a.ROLE_ASSIGN_FLAG == 1
                                             select role_a.ROLE_ID).ToList();
                if (List_RoleIds != null)
                    //return List_RoleIds;
                    return List_RoleIds.Distinct().ToList();   //used distinct as there can be multiple application's function permission setted in a single role & if so then the role'll be exists more than one time 
                else
                    return null;
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

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "GetUserInfoByUserId",
                                              "0000000000", dbEx.Message, inner, dbEx.StackTrace, null, dateTime);

                return null;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "GetUserInfoByUserId",
                                              "0000000000", ex.Message, inner4, ex.StackTrace, null, dateTime);

                return null;
            }
        }
        public static string RemoveRoleAssign(string pROLE_ID_FOR_IND_USER, string pUSER_ID, string pMAKE_BY)
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            LG_USER_ROLE_ASSIGN OBJ_LG_USER_ROLE_ASSIGN = new LG_USER_ROLE_ASSIGN();
            string FUNCTION_ID = string.Empty;
            string result = string.Empty;
            try
            {
                FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "RoleAssign").Select(x => x.FUNCTION_ID).SingleOrDefault();
                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                string[] Arr_Role_Id = pROLE_ID_FOR_IND_USER.Split(',');
                foreach (string ind_role_id in Arr_Role_Id)
                {
                    var List_appid_under_single_roleid = Obj_DBModelEntities.LG_USER_ROLE_ASSIGN
                                                                .Where(a => a.ROLE_ID == ind_role_id &&
                                                                        a.USER_ID == pUSER_ID &&
                                                                        a.AUTH_STATUS_ID == "A" &&
                                                                        a.LAST_ACTION != "DEL")
                                                                .GroupBy(a => a.APPLICATION_ID)
                                                                .Select(a => a.Key)
                                                                .ToList();

                    string appid_under_single_roleid = string.Empty;
                    for (int i = 0; i < List_appid_under_single_roleid.Count(); i++) //when there are multiple application's function permission were setted in a single role
                    {
                        appid_under_single_roleid = List_appid_under_single_roleid[i].ToString();
                        DBModelEntities Obj_DBModelEntities1 = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
                        LG_USER_ROLE_ASSIGN OBJ_LG_USER_ROLE_ASSIGN_OLD = Obj_DBModelEntities1.LG_USER_ROLE_ASSIGN
                                                                         .Where(role_a => role_a.ROLE_ID == ind_role_id &&
                                                                                role_a.USER_ID == pUSER_ID &&
                                                                                role_a.LAST_ACTION != "DEL" &&
                                                                                role_a.ROLE_ASSIGN_FLAG == 1 &&
                                                                                role_a.APPLICATION_ID == appid_under_single_roleid)
                                                                         .Select(rola_a => rola_a).SingleOrDefault();

                        if (OBJ_LG_USER_ROLE_ASSIGN_OLD != null)
                        {
                            OBJ_LG_USER_ROLE_ASSIGN = new LG_USER_ROLE_ASSIGN();
                            OBJ_LG_USER_ROLE_ASSIGN = Obj_DBModelEntities.LG_USER_ROLE_ASSIGN
                                                     .Where(role_a => role_a.ROLE_ID == ind_role_id &&
                                                            role_a.USER_ID == pUSER_ID &&
                                                            role_a.LAST_ACTION != "DEL" &&
                                                            role_a.ROLE_ASSIGN_FLAG == 1 &&
                                                            role_a.APPLICATION_ID == appid_under_single_roleid)
                                                     .Select(rola_a => rola_a).SingleOrDefault();

                            OBJ_LG_USER_ROLE_ASSIGN.AUTH_STATUS_ID = "U";
                            OBJ_LG_USER_ROLE_ASSIGN.LAST_ACTION = "EDT";
                            OBJ_LG_USER_ROLE_ASSIGN.LAST_UPDATE_DT = System.DateTime.Now;
                            OBJ_LG_USER_ROLE_ASSIGN.ROLE_ASSIGN_FLAG = 0;
                            Obj_DBModelEntities.SaveChanges();

                            #region Auth log
                            LG_AA_NFT_AUTH_LOG_MAP OBJ_LG_AA_NFT_AUTH_LOG_MAP = new LG_AA_NFT_AUTH_LOG_MAP();
                            FUNC_ID = OBJ_LG_AA_NFT_AUTH_LOG_MAP.FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "RoleAssign").Select(x => x.FUNCTION_ID).SingleOrDefault();
                            OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_NAME = "LG_USER_ROLE_ASSIGN";
                            OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_NM = "SL_NO";
                            OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_VAL = OBJ_LG_USER_ROLE_ASSIGN.SL_NO.ToString();
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
                            OBJ_LG_AA_NFT_AUTH_LOG_MAP.MAKE_BY = pMAKE_BY;
                            OBJ_LG_AA_NFT_AUTH_LOG_MAP.MAKE_DT = DateTime.Now;
                            //LG_AA_NFT_AUTH_LOG_MAP.AddNftAuthLog(OBJ_LG_USER_ROLE_ASSIGN, OBJ_LG_AA_NFT_AUTH_LOG_MAP);
                            LG_AA_NFT_AUTH_LOG_MAP.AddNftAuthLog(OBJ_LG_USER_ROLE_ASSIGN_OLD, OBJ_LG_USER_ROLE_ASSIGN, OBJ_LG_AA_NFT_AUTH_LOG_MAP);
                            #endregion
                        }
                    }
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
                        result = "Can't remove Role Assign permission(Db).";
                    }
                }

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "RemoveRoleAssign",
                                              "0000000000", dbEx.Message, inner, dbEx.StackTrace, null, dateTime);

                return result;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "RemoveRoleAssign",
                                              "0000000000", ex.Message, inner4, ex.StackTrace, null, dateTime);

                result = "Can't remove Role Assign permission.";
                return result;
            }
            result = "true";
            return result;
        }
        #endregion

        #region Validate
        #endregion

        #endregion
    }
}
