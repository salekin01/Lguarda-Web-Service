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
using System.Web;
using System.Web.Mvc;

namespace Model.EntityModel.LGModel
{
    public class LG_USER_FILE_UPLOAD_MAP
    {
        #region Properties

        [DataMember]
        public short FILE_ID { get; set; }

        [DataMember]
        public short FILE_TYPE { get; set; }

        [DataMember]
        public short USER_TYPE { get; set; }
      
        [DataMember]
        public string USER_ID { get; set; }
      
        [DataMember]
        public string APPLICATION_ID { get; set; }
     
        [DataMember]
        //[ValidateUploadingFileAttribute(ErrorMessage = "Please select a file smaller than 2MB")]
        public HttpPostedFileBase FILE { get; set; }

        [DataMember]
        public byte[] imageByte { get; set; }

        [DataMember]
        public string USER_NAME { get; set; }

        [DataMember]
        public string AUTH_STATUS_ID { get; set; }

        [DataMember]
        public string ERROR { get; set; }

        [DataMember]
        public List<SelectListItem> LIST_USER_FOR_DD { get; set; }

        [DataMember]
        public string CLASSIFICATION_ID { get; set; }
        [DataMember]
        public string AREA_ID { get; set; }
        [DataMember]
        public string CUSTOMER_ID { get; set; }
        [DataMember]
        public string AGENT_ID { get; set; }
        [DataMember]
        public string ACC_NO { get; set; }

        [DataMember]
        public string USER_SESSION_ID { get; set; }

        #endregion
        
        #region Events

        #region Add File

        public static string AddFile(LG_USER_FILE_UPLOAD_MAP pLG_USER_FILE_UPLOAD_MAP)
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            string result = string.Empty;

            string FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "UserFileUpload").Select(x => x.FUNCTION_ID).SingleOrDefault();
            try
            {

                int id = Convert.ToInt32(Obj_DBModelEntities.LG_USER_FILES
                                        .Max(x => (int?)x.FILE_ID) ?? 0) + 1;

                LG_USER_FILES OBJ_LG_USER_FILES = new LG_USER_FILES();

                OBJ_LG_USER_FILES.FILE_ID = id;
                OBJ_LG_USER_FILES.DATA = pLG_USER_FILE_UPLOAD_MAP.imageByte;
                OBJ_LG_USER_FILES.FILE_TYPE = pLG_USER_FILE_UPLOAD_MAP.FILE_TYPE;
                OBJ_LG_USER_FILES.USER_ID = pLG_USER_FILE_UPLOAD_MAP.USER_ID.ToString();
                //OBJ_LG_USER_FILES.APPLICATION_ID = "01";
                OBJ_LG_USER_FILES.AUTH_STATUS_ID = "U";
                OBJ_LG_USER_FILES.LAST_ACTION = "ADD";
                OBJ_LG_USER_FILES.MAKE_DT = System.DateTime.Now;

                Obj_DBModelEntities.LG_USER_FILES.Add(OBJ_LG_USER_FILES);
                Obj_DBModelEntities.SaveChanges();

                #region Auth log
                LG_AA_NFT_AUTH_LOG_MAP OBJ_LG_AA_NFT_AUTH_LOG_MAP = new LG_AA_NFT_AUTH_LOG_MAP();
                OBJ_LG_AA_NFT_AUTH_LOG_MAP.FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "UserFileUpload").Select(x => x.FUNCTION_ID).SingleOrDefault();
                OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_NAME = "LG_USER_FILES";
                OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_NM = "FILE_ID";
                OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_VAL = OBJ_LG_USER_FILES.FILE_ID.ToString();
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
                OBJ_LG_AA_NFT_AUTH_LOG_MAP.MAKE_BY = pLG_USER_FILE_UPLOAD_MAP.USER_SESSION_ID;
                OBJ_LG_AA_NFT_AUTH_LOG_MAP.MAKE_DT = DateTime.Now;
                LG_AA_NFT_AUTH_LOG_MAP.AddNftAuthLog(OBJ_LG_USER_FILES, OBJ_LG_AA_NFT_AUTH_LOG_MAP);
                #endregion

                result = "True";
                return result;
            }
            catch (DbEntityValidationException dbEx)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner="";               

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                        inner = validationError.PropertyName + "::" + validationError.ErrorMessage + ";;" + inner;
                        result = "Can't Add File(Db).";
                    }
                }

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "AddFile",
       "0000000000", dbEx.Message, inner, dbEx.StackTrace, pLG_USER_FILE_UPLOAD_MAP.USER_SESSION_ID, dateTime);

                return result;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner1;
                string inner2;
                string inner3;
                string inner4;
                if (ex.InnerException != null)
                {
                    inner1 = ex.InnerException.ToString() + ";;";
                }
                else
                {
                    inner1 = ";;";
                }
                if (ex.InnerException != null)
                {
                    inner2 = inner1 + ex.InnerException.InnerException.ToString() + ";;";
                }
                else
                {
                    inner2 = inner1 + ";;";
                }
                if (ex.InnerException != null)
                {
                    inner3 = inner2 + ex.InnerException.InnerException.InnerException.ToString() + ";;";
                }
                else
                {
                    inner3 = inner2 + ";;";
                }
                if (ex.InnerException != null)
                {
                    inner4 = inner3 + ex.InnerException.InnerException.InnerException.InnerException.ToString() + ";;";
                }
                else
                {
                    inner4 = inner3 + ";;";
                }


                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "AddFile",
                       "0000000000", ex.Message, inner4, ex.StackTrace, pLG_USER_FILE_UPLOAD_MAP.USER_SESSION_ID, dateTime);

                result = "Can't Add File,";
                return result;
            }
        }

        #endregion


        #region Fetch Single
        public static LG_USER_FILE_UPLOAD_MAP Get_UserUploadFile_ByUserId(string pUSER_ID)
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            LG_USER_FILE_UPLOAD_MAP OBJ_LG_USER_FILE_UPLOAD_MAP = new LG_USER_FILE_UPLOAD_MAP();

            try
            {
                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                OBJ_LG_USER_FILE_UPLOAD_MAP = (from a in Obj_DBModelEntities.LG_USER_FILES
                                               where a.USER_ID == pUSER_ID
                                              select new LG_USER_FILE_UPLOAD_MAP
                                              {
                                                  USER_ID = a.USER_ID,
                                                  imageByte = a.DATA,
                                                  AUTH_STATUS_ID = a.AUTH_STATUS_ID,
                                                  //LAST_ACTION = a.LAST_ACTION,
                                                  //LAST_UPDATE_DT = a.LAST_UPDATE_DT,
                                                  //MAKE_DT = a.MAKE_DT
                                              }).SingleOrDefault();
                return OBJ_LG_USER_FILE_UPLOAD_MAP;
            }

            catch (DbEntityValidationException dbEx)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner="";               

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                        inner = validationError.PropertyName + "::" + validationError.ErrorMessage + ";;" + inner;
                        OBJ_LG_USER_FILE_UPLOAD_MAP.ERROR = validationError.ErrorMessage;
                    }
                }

                LG_ERROR_LOG_MAP.Add_Error_Log(null, dbEx.Source, "ERR_APP_TYPE", "Get_UserUploadFile_ByUserId",
       "0000000000", dbEx.Message, inner, dbEx.StackTrace, "FileUploadedTo:" + pUSER_ID, dateTime);

                return OBJ_LG_USER_FILE_UPLOAD_MAP;
            }
            catch (Exception ex)
            {

                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner1;
                string inner2;
                string inner3;
                string inner4;
                if (ex.InnerException != null)
                {
                    inner1 = ex.InnerException.ToString() + ";;";
                }
                else
                {
                    inner1 = ";;";
                }
                if (ex.InnerException != null)
                {
                    inner2 = inner1 + ex.InnerException.InnerException.ToString() + ";;";
                }
                else
                {
                    inner2 = inner1 + ";;";
                }
                if (ex.InnerException != null)
                {
                    inner3 = inner2 + ex.InnerException.InnerException.InnerException.ToString() + ";;";
                }
                else
                {
                    inner3 = inner2 + ";;";
                }
                if (ex.InnerException != null)
                {
                    inner4 = inner3 + ex.InnerException.InnerException.InnerException.InnerException.ToString() + ";;";
                }
                else
                {
                    inner4 = inner3 + ";;";
                }

                LG_ERROR_LOG_MAP.Add_Error_Log(null, ex.Source, "ERR_APP_TYPE", "Get_UserUploadFile_ByUserId",
                       "0000000000", ex.Message, inner4, ex.StackTrace, "FileUploadedTo:" + pUSER_ID, dateTime);

                OBJ_LG_USER_FILE_UPLOAD_MAP.ERROR = ex.Message;
                return OBJ_LG_USER_FILE_UPLOAD_MAP;
            }
        }
        #endregion

        #region dropdown

        //public static IEnumerable<SelectListItem> GetUserAreaIdForDD(string id)
        //{
        //    DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);

        //    string format = OutgoingResponseFormat.GetFormat();
        //    OutgoingResponseFormat.SetResponseFormat(format);
        //    string pid = id;
        //    var List_Sys_User_Area = (from s in Obj_DBModelEntities.LG_USER_AREA
        //                              where s.CLASSIFICATION_ID == pid
        //                              select s);
        //    var selectList = new List<SelectListItem>();
        //    foreach (var element in List_Sys_User_Area)
        //    {
        //        selectList.Add(new SelectListItem
        //        {
        //            Value = element.AREA_ID.ToString(),
        //            Text = element.AREA_NAME
        //        });
        //    }
        //    if (selectList != null)
        //        return selectList;
        //    else
        //        throw new Exception("Invalid");
        //}

        public static IEnumerable<SelectListItem> GetAllUseIdForDD()
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);

            string format = OutgoingResponseFormat.GetFormat();
            OutgoingResponseFormat.SetResponseFormat(format);

            var List_Lg_All_User = (from s in Obj_DBModelEntities.LG_USER_SETUP_PROFILE
                                      select s);
            var selectList = new List<SelectListItem>();
            foreach (var element in List_Lg_All_User)
            {
                selectList.Add(new SelectListItem
                {
                    Value = element.USER_ID.ToString(),
                    Text = element.USER_ID
                });
            }
            if (selectList != null)
                return selectList;
            else
                throw new Exception("Invalid");
        }

        #endregion

        #region Custom Method

        public static LG_USER_FILE_UPLOAD_MAP Get_UserInfoByUserId(string pUSER_ID)
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            LG_USER_FILE_UPLOAD_MAP OBJ_LG_USER_FILE_UPLOAD_MAP = new LG_USER_FILE_UPLOAD_MAP();

            try
            {
                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                OBJ_LG_USER_FILE_UPLOAD_MAP = (from u in Obj_DBModelEntities.LG_USER_SETUP_PROFILE
                                               where u.USER_ID == pUSER_ID && u.LAST_ACTION != "DEL"
                                               select new LG_USER_FILE_UPLOAD_MAP
                                               {
                                                   USER_ID = u.USER_ID,
                                                   USER_NAME = u.USER_NM,
                                                   AUTH_STATUS_ID = u.AUTH_STATUS_ID,
                                                   //LAST_ACTION = u.LAST_ACTION,
                                                   //LAST_UPDATE_DT = u.LAST_UPDATE_DT,
                                                   //MAKE_DT = u.MAKE_DT
                                               }).SingleOrDefault();
                return OBJ_LG_USER_FILE_UPLOAD_MAP;
            }

            catch (DbEntityValidationException dbEx)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner="";               

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                        inner = validationError.PropertyName + "::" + validationError.ErrorMessage + ";;" + inner;
                        OBJ_LG_USER_FILE_UPLOAD_MAP.ERROR = validationError.ErrorMessage;
                    }
                }

                LG_ERROR_LOG_MAP.Add_Error_Log(null, dbEx.Source, "ERR_APP_TYPE", "Get_UserInfoByUserId",
       "0000000000", dbEx.Message, inner, dbEx.StackTrace, "GetInforFor:" + pUSER_ID, dateTime);

                return OBJ_LG_USER_FILE_UPLOAD_MAP;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner1;
                string inner2;
                string inner3;
                string inner4;
                if (ex.InnerException != null)
                {
                    inner1 = ex.InnerException.ToString() + ";;";
                }
                else
                {
                    inner1 = ";;";
                }
                if (ex.InnerException != null)
                {
                    inner2 = inner1 + ex.InnerException.InnerException.ToString() + ";;";
                }
                else
                {
                    inner2 = inner1 + ";;";
                }
                if (ex.InnerException != null)
                {
                    inner3 = inner2 + ex.InnerException.InnerException.InnerException.ToString() + ";;";
                }
                else
                {
                    inner3 = inner2 + ";;";
                }
                if (ex.InnerException != null)
                {
                    inner4 = inner3 + ex.InnerException.InnerException.InnerException.InnerException.ToString() + ";;";
                }
                else
                {
                    inner4 = inner3 + ";;";
                }

                LG_ERROR_LOG_MAP.Add_Error_Log(null, ex.Source, "ERR_APP_TYPE", "Get_UserInfoByUserId",
                       "0000000000", ex.Message, inner4, ex.StackTrace, "GetInforFor:" + pUSER_ID, dateTime);

                OBJ_LG_USER_FILE_UPLOAD_MAP.ERROR = ex.Message;
                return OBJ_LG_USER_FILE_UPLOAD_MAP;
            }
        }

        #endregion

        #endregion

    }
}
