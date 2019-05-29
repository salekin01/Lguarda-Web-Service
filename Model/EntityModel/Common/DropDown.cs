
using Model.EDMX;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Model.EntityModel.Common
{
    public class DropDown
    {
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
            try
            {
                DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);

                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                selectList.Add(new SelectListItem { Value = "1", Text = "Fixed" });
                selectList.Add(new SelectListItem { Value = "2", Text = "Day Long" });
                return selectList;
            }
            catch (Exception ex)
            {
                return selectList;
            }

        }
        public static IEnumerable<SelectListItem> GetTwoFAtypeForDD()
        {
            var selectList = new List<SelectListItem>();
            try
            {
                DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);

                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                selectList.Add(new SelectListItem { Value = "1", Text = "Software" });
                selectList.Add(new SelectListItem { Value = "2", Text = "Hardware" });
                return selectList;
            }
            catch (Exception ex)
            {
                return selectList;
            }

        }


        public static IEnumerable<SelectListItem> GetFunctionsFromAuthLogForDD()
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);

            string format = OutgoingResponseFormat.GetFormat();
            OutgoingResponseFormat.SetResponseFormat(format);

            var List_LG_FNR_FUNCTION = (from f in Obj_DBModelEntities.LG_FNR_FUNCTION
                                        join a in Obj_DBModelEntities.LG_AA_NFT_AUTH_LOG
                                            on f.FUNCTION_ID equals a.FUNCTION_ID
                                        where a.AUTH_STATUS_ID == "U"
                                        select f).Distinct();

            var selectList = new List<SelectListItem>();
            foreach (var element in List_LG_FNR_FUNCTION)
            {
                selectList.Add(new SelectListItem
                {
                    Value = element.FUNCTION_ID,
                    Text = element.FUNCTION_NM
                });
            }
            if (selectList != null)
                return selectList;
            else
                throw new Exception("Invalid");
        }
        public static IEnumerable<SelectListItem> GetApplicationForDD()
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);

            string format = OutgoingResponseFormat.GetFormat();
            OutgoingResponseFormat.SetResponseFormat(format);

            var List_Sys_Application = (from p in Obj_DBModelEntities.LG_FNR_APPLICATION
                                        where p.APPLICATION_ID != null & p.AUTH_STATUS_ID == "A" & p.LAST_ACTION != "DEL"
                                        select p);
            var selectList = new List<SelectListItem>();
            foreach (var element in List_Sys_Application)
            {
                selectList.Add(new SelectListItem
                {
                    Value = element.APPLICATION_ID.ToString(),
                    Text = element.APPLICATION_NAME
                });
            }
            return selectList;
        }
        public static IEnumerable<SelectListItem> GetServiceForDD()
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);

            string format = OutgoingResponseFormat.GetFormat();
            OutgoingResponseFormat.SetResponseFormat(format);

            var List_Sys_Service = (from s in Obj_DBModelEntities.LG_FNR_SERVICE
                                    select s);
            var selectList = new List<SelectListItem>();
            foreach (var element in List_Sys_Service)
            {
                selectList.Add(new SelectListItem
                {
                    Value = element.SERVICE_ID.ToString(),
                    Text = element.SERVICE_NM
                });
            }
            return selectList;
        }
        public static IEnumerable<SelectListItem> GetModuleForDD()
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);

            string format = OutgoingResponseFormat.GetFormat();
            OutgoingResponseFormat.SetResponseFormat(format);

            var List_Sys_Module = (from m in Obj_DBModelEntities.LG_FNR_MODULE
                                    select m);
            var selectList = new List<SelectListItem>();
            foreach (var element in List_Sys_Module)
            {
                selectList.Add(new SelectListItem
                {
                    Value = element.MODULE_ID.ToString(),
                    Text = element.MODULE_NM
                });
            }
            return selectList;
        }
        public static IEnumerable<SelectListItem> GetOTPFormatForDD()
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);

            string format = OutgoingResponseFormat.GetFormat();
            OutgoingResponseFormat.SetResponseFormat(format);

            var selectList = new List<SelectListItem>();
            selectList.Add(new SelectListItem { Value = "1", Text = "Number only" });
            selectList.Add(new SelectListItem { Value = "2", Text = "Alphanumeric" });
            selectList.Add(new SelectListItem { Value = "3", Text = "Alphanumeric with special charecter" });
            if (selectList != null)
                return selectList;
            else
                return null;
        }


        public static IEnumerable<SelectListItem> GetServiceByAppIdForDD(string pid)
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);

            string format = OutgoingResponseFormat.GetFormat();
            OutgoingResponseFormat.SetResponseFormat(format);
            var List_Sys_Service = (from s in Obj_DBModelEntities.LG_FNR_SERVICE
                                    where s.APPLICATION_ID == pid
                                    select s);
            var selectList = new List<SelectListItem>();
            foreach (var element in List_Sys_Service)
            {
                selectList.Add(new SelectListItem
                {
                    Value = element.SERVICE_ID.ToString(),
                    Text = element.SERVICE_NM
                });
            }
            if (selectList != null)
                return selectList;
            else
                throw new Exception("Invalid");
        }
        public static IEnumerable<SelectListItem> GetModuleByServiceIdForDD(string pservice_id, string papp_id)
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);

            string format = OutgoingResponseFormat.GetFormat();
            OutgoingResponseFormat.SetResponseFormat(format);
            var List_Sys_Module = (from m in Obj_DBModelEntities.LG_FNR_MODULE
                                   where m.SERVICE_ID == pservice_id &&
                                         m.APPLICATION_ID == papp_id 
                                   select m);
            var selectList = new List<SelectListItem>();
            foreach (var element in List_Sys_Module)
            {
                selectList.Add(new SelectListItem
                {
                    Value = element.MODULE_ID.ToString(),
                    Text = element.MODULE_NM
                });
            }
            if (selectList != null)
                return selectList;
            else
                throw new Exception("Invalid");
        }
        public static IEnumerable<SelectListItem> GetFunctionByModuleIdForDD(string pid)
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);

            string format = OutgoingResponseFormat.GetFormat();
            OutgoingResponseFormat.SetResponseFormat(format);
            var List_Sys_Role_Function = (from f in Obj_DBModelEntities.LG_FNR_FUNCTION
                                          where f.MODULE_ID == pid
                                          select f);
            var selectList = new List<SelectListItem>();
            foreach (var element in List_Sys_Role_Function)
            {
                selectList.Add(new SelectListItem
                {
                    Value = element.FUNCTION_ID.ToString(),
                    Text = element.FUNCTION_NM
                });
            }
            if (selectList != null)
                return selectList;
            else
                throw new Exception("Invalid");
        }
        public static IEnumerable<SelectListItem> GetFunctionByModuleIdAndItemtypeForDD(string module_id, string item_type)
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);

            string format = OutgoingResponseFormat.GetFormat();
            OutgoingResponseFormat.SetResponseFormat(format);

            var List_Sys_Role_Function = (from f in Obj_DBModelEntities.LG_FNR_FUNCTION
                                          where f.MODULE_ID == module_id && f.ITEM_TYPE == item_type
                                          select f);
            var selectList = new List<SelectListItem>();
            foreach (var element in List_Sys_Role_Function)
            {
                selectList.Add(new SelectListItem
                {
                    Value = element.FUNCTION_ID.ToString(),
                    Text = element.FUNCTION_NM
                });
            }
            if (selectList != null)
                return selectList;
            else
                return null;
        }
        public static IEnumerable<SelectListItem> GetItemtypeForDD()
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);

            string format = OutgoingResponseFormat.GetFormat();
            OutgoingResponseFormat.SetResponseFormat(format);

            var selectList = new List<SelectListItem>();

            selectList.Add(new SelectListItem { Value = "F", Text = "Form" });
            selectList.Add(new SelectListItem { Value = "R", Text = "Report" });
            if (selectList != null)
                return selectList;
            else
                throw new Exception("Invalid");
        }

        public static IEnumerable<SelectListItem> GetUserUploadFileTypeForDD()
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);

            string format = OutgoingResponseFormat.GetFormat();
            OutgoingResponseFormat.SetResponseFormat(format);
            var List_User_file_type = (from m in Obj_DBModelEntities.LG_USER_FILE_TYPE
                                                select m);
            var selectList = new List<SelectListItem>();
            foreach (var element in List_User_file_type)
            {
                selectList.Add(new SelectListItem
                {
                    Value = element.FILE_TYPE_ID.ToString(),
                    Text = element.FILE_TYPE_NAME
                });
            }
            if (selectList != null)
                return selectList;
            else
                throw new Exception("Invalid");
        }
        public static IEnumerable<SelectListItem> GetAppTypesForDD()
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);

            string format = OutgoingResponseFormat.GetFormat();
            OutgoingResponseFormat.SetResponseFormat(format);

            var List_App_Types = (from s in Obj_DBModelEntities.LG_FNR_APPLICATION_TYPE
                                    select s);
            var selectList = new List<SelectListItem>();
            foreach (var element in List_App_Types)
            {
                selectList.Add(new SelectListItem
                {
                    Value = element.APP_TYPE_ID.ToString(),
                    Text = element.APP_NM
                });
            }
            return selectList;
        }
        
    }

}
