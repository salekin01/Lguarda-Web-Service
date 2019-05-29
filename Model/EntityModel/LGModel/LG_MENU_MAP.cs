using Model.EDMX;
using Model.EntityModel.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model.EntityModel.LGModel
{
    [DataContract]
    public class LG_MENU_MAP
    {
        //public int MenuId { get; set; }
        //public string Name { get; set; }
        //public string Description { get; set; }
        //public string Action { get; set; }
        //public string Controller { get; set; }
        //public string Url { get; set; }
        //public Nullable<int> ParentId { get; set; }

        //public IEnumerable<LG_MENU_MAP> Children { get; set; }


        [DataMember]
        public decimal SL_ID { get; set; }
        [DataMember]
        public string APP_ID { get; set; }
        [DataMember]
        public Nullable<decimal> MENU_ID { get; set; }
        [DataMember]
        public string NAME { get; set; }
        [DataMember]
        public string DESCRIPTION { get; set; }
        [DataMember]
        public string CONTROLLER { get; set; }
        [DataMember]
        public string ACTION { get; set; }
        [DataMember]
        public string URL { get; set; }
        [DataMember]
        public Nullable<decimal> PARENTID { get; set; }
        [DataMember]
        public string FUNCTION_ID { get; set; }

        [DataMember]
        public IEnumerable<LG_MENU_MAP> Children { get; set; }
          

        #region gET mfs MENU
        public static IEnumerable<LG_MENU_MAP> GetMFSMenu(string APP_ID)
        {            
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);

            string format = OutgoingResponseFormat.GetFormat();
            OutgoingResponseFormat.SetResponseFormat(format);

            LG_MENU_MAP obj_LG_MENU_MAP = new LG_MENU_MAP();
            List<LG_MENU_MAP> OBJ_LIST_LG_MENU_MAP = null;                    
                   
          
            
                OBJ_LIST_LG_MENU_MAP = (from menu in Obj_DBModelEntities.LG_MENU
                                            where menu.APP_ID == APP_ID &&
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
                                            }).ToList();

                return OBJ_LIST_LG_MENU_MAP;
        }
        #endregion

        #region Binding Menu
        public static string BindingMenu(string pAppId, string pMenuNm, string pDescription, string pController, string pAction, string pUrl, string pFunctionId, short? pMenuLevel, short? pFunctionAssignFlag, short? pMenuEnableFlag, string pServiceId, string pModuleId, string USER_ID, DBModelEntities Obj_DBModelEntities, short? pMENU_ENABLE_FLAG)
        {
            string result = string.Empty;
            try
            {
                int _parentid = 0;
                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                LG_MENU OBJ_LG_MENU = new LG_MENU();
                OBJ_LG_MENU = Obj_DBModelEntities.LG_MENU
                             .Where(a => a.FUNCTION_ID == pFunctionId).SingleOrDefault();
                if (OBJ_LG_MENU != null) //Menu Update 
                {
                    if (OBJ_LG_MENU.MENU_ENABLE_FLAG != pMENU_ENABLE_FLAG)
                    {
                        OBJ_LG_MENU.MENU_ENABLE_FLAG = pMENU_ENABLE_FLAG;
                        Obj_DBModelEntities.SaveChanges();
                    }
                }
                else  //Add Menu
                    AddMenu(pAppId, pMenuNm, pDescription, pController, pAction, pUrl, pFunctionId, pMenuLevel, pFunctionAssignFlag, pMenuEnableFlag, pServiceId, pModuleId, USER_ID, Obj_DBModelEntities);

               
                result = "true";
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
                        result = "Can't Bind Menu(Db).";
                    }
                }
                LG_ERROR_LOG_MAP.Add_Error_Log("111111111", dbEx.Source, "ERR_APP_TYPE", "BindingMenu",
                                              "0000000000", dbEx.Message, inner, dbEx.StackTrace, USER_ID, dateTime);
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                LG_ERROR_LOG_MAP.Add_Error_Log("111111111", ex.Source, "ERR_APP_TYPE", "BindingMenu",
                                              "0000000000", ex.Message, inner4, ex.StackTrace, USER_ID, dateTime);
                result = "Can't Bind Menu.";
            }
            return result;
        }
        #endregion

        #region Add
        public static string AddMenu(string pAppId, string pMenuNm, string pDescription, string pController, string pAction, string pUrl, string pFunctionId, short? pMenuLevel, short? pFunctionAssignFlag, short? pMenuEnableFlag, string pServiceId, string pModuleId, string USER_ID, DBModelEntities Obj_DBModelEntities)
        {
            string result = string.Empty;
            try
            {
                int _parentid = 0;
                //string format = OutgoingResponseFormat.GetFormat();
                //OutgoingResponseFormat.SetResponseFormat(format);

                LG_MENU OBJ_LG_MENU = new LG_MENU();
                OBJ_LG_MENU = Obj_DBModelEntities.LG_MENU
                             .Where(a => a.CONTROLLER == pController).SingleOrDefault();
                if (OBJ_LG_MENU != null)
                {
                    return "Menu already exists";
                }
                else
                    OBJ_LG_MENU = new LG_MENU();

                int id = (Obj_DBModelEntities.LG_MENU
                         .Select(i => i.SL_ID).Cast<int?>().Max() ?? 0) + 1;

                if (!string.IsNullOrWhiteSpace(pAppId) && !string.IsNullOrWhiteSpace(pServiceId) && !string.IsNullOrWhiteSpace(pModuleId))
                {
                    string _moduleNm = Obj_DBModelEntities.LG_FNR_MODULE
                                      .Where(a => a.MODULE_ID == pModuleId &&
                                                  a.SERVICE_ID == pServiceId &&
                                                  a.APPLICATION_ID == pAppId)
                                      .Select(a => a.MODULE_NM).FirstOrDefault();

                    if (!string.IsNullOrWhiteSpace(_moduleNm))
                    {
                        var _menuId = Obj_DBModelEntities.LG_MENU
                                     .Where(a => a.CONTROLLER == _moduleNm)
                                     .Select(a => a.MENU_ID).FirstOrDefault();
                        _parentid = _menuId != null ? Convert.ToInt32(_menuId) : _parentid;
                    }
                }

                OBJ_LG_MENU.SL_ID = id;
                OBJ_LG_MENU.MENU_ID = id;
                OBJ_LG_MENU.APP_ID = pAppId;
                OBJ_LG_MENU.NAME = pController;
                OBJ_LG_MENU.DESCRIPTION = pDescription;
                OBJ_LG_MENU.CONTROLLER = pController;
                OBJ_LG_MENU.ACTION = pAction;
                OBJ_LG_MENU.URL = pUrl;
                OBJ_LG_MENU.PARENTID = _parentid;
                OBJ_LG_MENU.FUNCTION_ID = pFunctionId;
                OBJ_LG_MENU.MENU_LEVEL = pMenuLevel;
                OBJ_LG_MENU.FUNCTION_ASSIGN_FLAG = pFunctionAssignFlag;
                OBJ_LG_MENU.MENU_ENABLE_FLAG = pMenuEnableFlag;
                Obj_DBModelEntities.LG_MENU.Add(OBJ_LG_MENU);
                result = "true";
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
                        result = "Can't Add Menu(Db).";
                    }
                }
                LG_ERROR_LOG_MAP.Add_Error_Log("111111111", dbEx.Source, "ERR_APP_TYPE", "AddMenu",
                                              "0000000000", dbEx.Message, inner, dbEx.StackTrace, USER_ID, dateTime);
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                LG_ERROR_LOG_MAP.Add_Error_Log("111111111", ex.Source, "ERR_APP_TYPE", "AddMenu",
                                              "0000000000", ex.Message, inner4, ex.StackTrace, USER_ID, dateTime);
                result = "Can't Add Menu.";
            }
            return result;
        }
        #endregion

        #region Update
        public static string UpdateMenu(string pFunctionId, short? pMENU_ENABLE_FLAG, string USER_ID, DBModelEntities Obj_DBModelEntities)
        {
            string result = string.Empty;
            try
            {
                //string format = OutgoingResponseFormat.GetFormat();
                //OutgoingResponseFormat.SetResponseFormat(format);

                LG_MENU OBJ_LG_MENU = new LG_MENU();
                OBJ_LG_MENU = Obj_DBModelEntities.LG_MENU
                             .Where(a => a.FUNCTION_ID == pFunctionId).SingleOrDefault();
                if (OBJ_LG_MENU == null)
                {
                    return "Menu doesn't exists";
                }

                OBJ_LG_MENU.MENU_ENABLE_FLAG = pMENU_ENABLE_FLAG;
                Obj_DBModelEntities.LG_MENU.Add(OBJ_LG_MENU);
                result = "true";
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
                        result = "Can't Update Menu(Db).";
                    }
                }
                LG_ERROR_LOG_MAP.Add_Error_Log("111111111", dbEx.Source, "ERR_APP_TYPE", "UpdateMenu",
                                              "0000000000", dbEx.Message, inner, dbEx.StackTrace, USER_ID, dateTime);
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                LG_ERROR_LOG_MAP.Add_Error_Log("111111111", ex.Source, "ERR_APP_TYPE", "AddMenu",
                                              "0000000000", ex.Message, inner4, ex.StackTrace, USER_ID, dateTime);
                result = "Can't Update Menu.";
            }
            return result;
        }
        #endregion
    }
}
