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
using System.Web.Mvc;

namespace Model.EntityModel.LGModel
{

     [DataContract]
   public class LG_SYS_CLD_TYPE_MAP
   {
       #region Properties

       [DataMember]
        public int CLD_TYPE_ID { get; set; }
        [DataMember]
        public string CLD_TYPE_NM { get; set; }
        [DataMember]
        public short DEFAULT_CLD { get; set; }
        [DataMember]
        public string BASED_ON_CLD { get; set; }
        [DataMember]
        public string MAKE_BY { get; set; }
        [DataMember]
        public Nullable<System.DateTime> MAKE_DT { get; set; }
        [DataMember]
        public string AUTH_STATUS_ID { get; set; }
        [DataMember]
        public string LAST_ACTION { get; set; }
        [DataMember]

        public Nullable<System.DateTime> LAST_UPDATE_DT { get; set; }
          [DataMember]
        public string ERROR { get; set; }
      //    public List<LG_SYS_CLD_TYPE_MAP> LIST_OF_ALL_CALENDAR { get; set; }
          [DataMember]
         public List<SelectListItem> LIST_OF_ALL_CALENDAR { get; set; }
        
       //   public IEnumerable<SelectListItem> LIST_OF_ALL_CALENDAR { get; set; }


       #endregion

       #region event

        #region Add New
          public static string AddCalendarType(string pCALENDAR_NM, string PDEFAULT_CLD, string pBASED_ON_CALENDAR, string pMAKE_BY)
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            string FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "CalendarType").Select(x => x.FUNCTION_ID).SingleOrDefault();

            LG_SYS_CLD_TYPE OBJ_LG_SYS_CLD_TYPE = new LG_SYS_CLD_TYPE();
            string result = string.Empty;
            try
            {
               
                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                 OBJ_LG_SYS_CLD_TYPE = Obj_DBModelEntities.LG_SYS_CLD_TYPE
                                         .Where(m => m.CLD_TYPE_NM == pCALENDAR_NM).SingleOrDefault();
                 if (OBJ_LG_SYS_CLD_TYPE != null)
                {
                    return "Calendar name already exists";
                }


                int id = (Obj_DBModelEntities.LG_SYS_CLD_TYPE
                          .Select(i => i.CLD_TYPE_ID).Cast<int?>().Max() ?? 0) + 1;

                 OBJ_LG_SYS_CLD_TYPE = new LG_SYS_CLD_TYPE();
                 OBJ_LG_SYS_CLD_TYPE.CLD_TYPE_ID = (short)id;
                OBJ_LG_SYS_CLD_TYPE.CLD_TYPE_NM = pCALENDAR_NM;
                if (PDEFAULT_CLD.ToLower() == "true")
                {
                    OBJ_LG_SYS_CLD_TYPE.DEFAULT_CLD = 1;
                }
                else
                    OBJ_LG_SYS_CLD_TYPE.DEFAULT_CLD = 0;

              
               

                if (String.IsNullOrWhiteSpace(PDEFAULT_CLD))
                {
                    OBJ_LG_SYS_CLD_TYPE.BASED_ON_CLD = null;
                }
                else
                {
                    OBJ_LG_SYS_CLD_TYPE.BASED_ON_CLD = pBASED_ON_CALENDAR;
                }
                OBJ_LG_SYS_CLD_TYPE.AUTH_STATUS_ID = "U";
                OBJ_LG_SYS_CLD_TYPE.LAST_ACTION = "ADD";
                OBJ_LG_SYS_CLD_TYPE.MAKE_BY = pMAKE_BY;
                OBJ_LG_SYS_CLD_TYPE.MAKE_DT = System.DateTime.Now;
                OBJ_LG_SYS_CLD_TYPE.LAST_UPDATE_DT =System.DateTime.Now;
                Obj_DBModelEntities.LG_SYS_CLD_TYPE.Add(OBJ_LG_SYS_CLD_TYPE);

              

                Obj_DBModelEntities.SaveChanges();
                result = "True";

                #region Auth log
                LG_AA_NFT_AUTH_LOG_MAP OBJ_LG_AA_NFT_AUTH_LOG_MAP = new LG_AA_NFT_AUTH_LOG_MAP();
                FUNCTION_ID = OBJ_LG_AA_NFT_AUTH_LOG_MAP.FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "CalendarType").Select(x => x.FUNCTION_ID).SingleOrDefault();
                OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_NAME = "LG_SYS_CLD_TYPE";
                OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_NM = "CLD_TYPE_ID";
                OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_VAL = OBJ_LG_SYS_CLD_TYPE.CLD_TYPE_ID.ToString();
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
                LG_AA_NFT_AUTH_LOG_MAP.AddNftAuthLog(OBJ_LG_SYS_CLD_TYPE, OBJ_LG_AA_NFT_AUTH_LOG_MAP);
                #endregion


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
                        result = "Can't Add Module(Db) ";
                    }
                }
                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "AddModule",
                       "0000000000", dbEx.Message, inner, dbEx.StackTrace, pMAKE_BY, dateTime);
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


                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "AddModule",
                       "0000000000", ex.Message, inner4, ex.StackTrace, pMAKE_BY, dateTime);
                result = "Can't Add Module ";
                return result;
            }
        }
        #endregion

          #region Update
          public static string UpdateCalendarType(string pCALENDAR_ID, string PCLD_TYPE_NM, string PDEFAULT_CLD, string PBASED_ON_CLD, string psession_user)
          {
              DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
              string result = string.Empty;
              string FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "CalendarType").Select(x => x.FUNCTION_ID).SingleOrDefault();

              try
              {

                 int CALENDAR_ID = Convert.ToInt16(pCALENDAR_ID);
                
                  string format = OutgoingResponseFormat.GetFormat();
                  OutgoingResponseFormat.SetResponseFormat(format);

                  LG_SYS_CLD_TYPE OBJ_LG_SYS_CLD_TYPE_OLD = Obj_DBModelEntities.LG_SYS_CLD_TYPE
                                                      .Where(s => s.CLD_TYPE_ID == CALENDAR_ID).SingleOrDefault();

                  LG_SYS_CLD_TYPE OBJ_LG_SYS_CLD_TYPE = Obj_DBModelEntities.LG_SYS_CLD_TYPE
                                                      .Where(s => s.CLD_TYPE_ID == CALENDAR_ID).SingleOrDefault();

                  OBJ_LG_SYS_CLD_TYPE.CLD_TYPE_NM = PCLD_TYPE_NM;

                  if (PDEFAULT_CLD.ToLower() == "true")
                  {
                      OBJ_LG_SYS_CLD_TYPE.DEFAULT_CLD = 1;
                  }
                  else
                      OBJ_LG_SYS_CLD_TYPE.DEFAULT_CLD = 0;


                 
                  OBJ_LG_SYS_CLD_TYPE.BASED_ON_CLD = PBASED_ON_CLD;

                  OBJ_LG_SYS_CLD_TYPE.AUTH_STATUS_ID = "U";
                  OBJ_LG_SYS_CLD_TYPE.LAST_ACTION = "EDT";
                  OBJ_LG_SYS_CLD_TYPE.LAST_UPDATE_DT = System.DateTime.Now;
                  Obj_DBModelEntities.SaveChanges();

                  #region Auth log
                  LG_AA_NFT_AUTH_LOG_MAP OBJ_LG_AA_NFT_AUTH_LOG_MAP = new LG_AA_NFT_AUTH_LOG_MAP();
                  FUNCTION_ID = OBJ_LG_AA_NFT_AUTH_LOG_MAP.FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "Module").Select(x => x.FUNCTION_ID).SingleOrDefault();
                  OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_NAME = "LG_SYS_CLD_TYPE";
                  OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_NM = "CLD_TYPE_ID";
                  OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_VAL = OBJ_LG_SYS_CLD_TYPE.CLD_TYPE_ID.ToString();
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
                  LG_AA_NFT_AUTH_LOG_MAP.AddNftAuthLog(OBJ_LG_SYS_CLD_TYPE_OLD, OBJ_LG_SYS_CLD_TYPE, OBJ_LG_AA_NFT_AUTH_LOG_MAP);
                  #endregion

                  result = "True";
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
                          result = "Can't Update Calendar(Db) ";
                      }
                  }
                  LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "UpdateCalendarType",
                         "0000000000", dbEx.Message, inner, dbEx.StackTrace, psession_user, dateTime);
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


                  LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "UpdateCalendarType",
                         "0000000000", ex.Message, inner4, ex.StackTrace, psession_user, dateTime);

                  result = "Can't Update Calendar " + ex.Message;
                  return result;
              }
          }
          #endregion

        //#region Delete
        //public static string DeleteModule(string pMODULE_ID, string psession_user)
        //{
        //    DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
        //    string result = string.Empty;
        //    try
        //    {
        //        string format = OutgoingResponseFormat.GetFormat();
        //        OutgoingResponseFormat.SetResponseFormat(format);

        //        LG_FNR_MODULE OBJ_LG_FNR_MODULE = (from m in Obj_DBModelEntities.LG_FNR_MODULE
        //                                           where !(from f in Obj_DBModelEntities.LG_FNR_FUNCTION
        //                                                   select f.MODULE_ID).Contains(m.MODULE_ID)
        //                                           && m.MODULE_ID == pMODULE_ID
        //                                           select m).SingleOrDefault();
        //        if (OBJ_LG_FNR_MODULE != null)
        //        {
        //            Obj_DBModelEntities.LG_FNR_MODULE.Remove(OBJ_LG_FNR_MODULE);
        //            Obj_DBModelEntities.SaveChanges();

        //            #region Auth log
        //            LG_AA_NFT_AUTH_LOG_MAP OBJ_LG_AA_NFT_AUTH_LOG_MAP = new LG_AA_NFT_AUTH_LOG_MAP();
        //            FUNCTION_ID = OBJ_LG_AA_NFT_AUTH_LOG_MAP.FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "Module").Select(x => x.FUNCTION_ID).SingleOrDefault();
        //            OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_NAME = "LG_FNR_MODULE";
        //            OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_NM = "MODULE_ID";
        //            OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_VAL = OBJ_LG_FNR_MODULE.MODULE_ID;
        //            OBJ_LG_AA_NFT_AUTH_LOG_MAP.ACTION_STATUS = "DEL";
        //            OBJ_LG_AA_NFT_AUTH_LOG_MAP.REMARKS = "";
        //            OBJ_LG_AA_NFT_AUTH_LOG_MAP.PRIMARY_TABLE_FLAG = 0;
        //            OBJ_LG_AA_NFT_AUTH_LOG_MAP.PARENT_TABLE_PK_VAL = null;
        //            OBJ_LG_AA_NFT_AUTH_LOG_MAP.AUTH_STATUS_ID = "U";
        //            int? auth_level_max = LG_AA_NFT_AUTH_LOG_MAP.GetNftAuthLevelMaxFromFunction(OBJ_LG_AA_NFT_AUTH_LOG_MAP.FUNCTION_ID);
        //            if (auth_level_max.HasValue)
        //            {
        //                OBJ_LG_AA_NFT_AUTH_LOG_MAP.AUTH_LEVEL_MAX = (short)auth_level_max;
        //            }
        //            else
        //                OBJ_LG_AA_NFT_AUTH_LOG_MAP.AUTH_LEVEL_MAX = 0;

        //            OBJ_LG_AA_NFT_AUTH_LOG_MAP.AUTH_LEVEL_PENDING = OBJ_LG_AA_NFT_AUTH_LOG_MAP.AUTH_LEVEL_MAX;
        //            OBJ_LG_AA_NFT_AUTH_LOG_MAP.REASON_DECLINE = "";
        //            OBJ_LG_AA_NFT_AUTH_LOG_MAP.MAKE_BY = psession_user;
        //            OBJ_LG_AA_NFT_AUTH_LOG_MAP.MAKE_DT = DateTime.Now;
        //            LG_AA_NFT_AUTH_LOG_MAP.AddNftAuthLog(OBJ_LG_FNR_MODULE, OBJ_LG_AA_NFT_AUTH_LOG_MAP);
        //            #endregion

        //            result = "True";
        //            return result;
        //        }
        //        else
        //            return "Can't delete as this Module contains Functions.";
        //    }
        //    catch (DbEntityValidationException dbEx)
        //    {
        //        string dateTime = Convert.ToString(System.DateTime.Now);
        //        string inner = "";

        //        foreach (var validationErrors in dbEx.EntityValidationErrors)
        //        {
        //            foreach (var validationError in validationErrors.ValidationErrors)
        //            {
        //                Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
        //                inner = validationError.PropertyName + "::" + validationError.ErrorMessage + ";;" + inner;
        //                result = "Can't Delete Module(Db) ";
        //            }
        //        }
        //        LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "DeleteModule",
        //               "0000000000", dbEx.Message, inner, dbEx.StackTrace, psession_user, dateTime);
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        string dateTime = Convert.ToString(System.DateTime.Now);
        //        string inner1;
        //        string inner2;
        //        string inner3;
        //        string inner4;
        //        if (ex.InnerException != null)
        //        {
        //            inner1 = ex.InnerException.ToString() + ";;";
        //        }
        //        else
        //        {
        //            inner1 = ";;";
        //        }
        //        if (ex.InnerException != null)
        //        {
        //            inner2 = inner1 + ex.InnerException.InnerException.ToString() + ";;";
        //        }
        //        else
        //        {
        //            inner2 = inner1 + ";;";
        //        }
        //        if (ex.InnerException != null)
        //        {
        //            inner3 = inner2 + ex.InnerException.InnerException.InnerException.ToString() + ";;";
        //        }
        //        else
        //        {
        //            inner3 = inner2 + ";;";
        //        }
        //        if (ex.InnerException != null)
        //        {
        //            inner4 = inner3 + ex.InnerException.InnerException.InnerException.InnerException.ToString() + ";;";
        //        }
        //        else
        //        {
        //            inner4 = inner3 + ";;";
        //        }


        //        LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "DeleteModule",
        //               "0000000000", ex.Message, inner4, ex.StackTrace, psession_user, dateTime);
        //        result = "Can't Delete Module ";
        //        return result;
        //    }
        //}
        //#endregion

          //#region Fetch Single
          //public static LG_FNR_MODULE_MAP GetModuleByModuleId(string pMODULE_ID)
          //{
          //    DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
          //    LG_FNR_MODULE_MAP OBJ_LG_FNR_MODULE_MAP = new LG_FNR_MODULE_MAP();

          //    try
          //    {
          //        string format = OutgoingResponseFormat.GetFormat();
          //        OutgoingResponseFormat.SetResponseFormat(format);

          //        OBJ_LG_FNR_MODULE_MAP = (from a in Obj_DBModelEntities.LG_FNR_APPLICATION
          //                                 join s in Obj_DBModelEntities.LG_FNR_SERVICE
          //                                 on a.APPLICATION_ID equals s.APPLICATION_ID
          //                                 join m in Obj_DBModelEntities.LG_FNR_MODULE
          //                                 on s.SERVICE_ID equals m.SERVICE_ID
          //                                 where m.MODULE_ID == pMODULE_ID
          //                                 select new LG_FNR_MODULE_MAP
          //                                 {
          //                                     MODULE_ID = m.MODULE_ID,
          //                                     MODULE_NM = m.MODULE_NM,
          //                                     MODULE_SH_NM = m.MODULE_SH_NM,
          //                                     AUTH_STATUS_ID = s.AUTH_STATUS_ID,
          //                                     LAST_ACTION = s.LAST_ACTION,
          //                                     LAST_UPDATE_DT = s.LAST_UPDATE_DT,
          //                                     MAKE_DT = s.MAKE_DT,
          //                                     APPLICATION_ID = s.APPLICATION_ID,
          //                                     APPLICATION_NAME = a.APPLICATION_NAME,
          //                                     SERVICE_ID = m.SERVICE_ID,
          //                                     SERVICE_NM = s.SERVICE_NM
          //                                 }).SingleOrDefault();

          //        OBJ_LG_FNR_MODULE_MAP.APPLICATION_LIST_FOR_DD = DropDown.GetApplicationForDD();
          //        OBJ_LG_FNR_MODULE_MAP.SERVICE_LIST_FOR_DD = DropDown.GetServiceForDD();
          //        return OBJ_LG_FNR_MODULE_MAP;
          //    }

          //    catch (DbEntityValidationException dbEx)
          //    {
          //        string dateTime = Convert.ToString(System.DateTime.Now);
          //        string inner = "";

          //        foreach (var validationErrors in dbEx.EntityValidationErrors)
          //        {
          //            foreach (var validationError in validationErrors.ValidationErrors)
          //            {
          //                Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
          //                inner = validationError.PropertyName + "::" + validationError.ErrorMessage + ";;" + inner;
          //                OBJ_LG_FNR_MODULE_MAP.ERROR = validationError.ErrorMessage;
          //            }
          //        }
          //        LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "GetModuleByModuleId",
          //               "0000000000", dbEx.Message, inner, dbEx.StackTrace, null, dateTime);
          //        return OBJ_LG_FNR_MODULE_MAP;
          //    }
          //    catch (Exception ex)
          //    {
          //        string dateTime = Convert.ToString(System.DateTime.Now);
          //        string inner1;
          //        string inner2;
          //        string inner3;
          //        string inner4;
          //        if (ex.InnerException != null)
          //        {
          //            inner1 = ex.InnerException.ToString() + ";;";
          //        }
          //        else
          //        {
          //            inner1 = ";;";
          //        }
          //        if (ex.InnerException != null)
          //        {
          //            inner2 = inner1 + ex.InnerException.InnerException.ToString() + ";;";
          //        }
          //        else
          //        {
          //            inner2 = inner1 + ";;";
          //        }
          //        if (ex.InnerException != null)
          //        {
          //            inner3 = inner2 + ex.InnerException.InnerException.InnerException.ToString() + ";;";
          //        }
          //        else
          //        {
          //            inner3 = inner2 + ";;";
          //        }
          //        if (ex.InnerException != null)
          //        {
          //            inner4 = inner3 + ex.InnerException.InnerException.InnerException.InnerException.ToString() + ";;";
          //        }
          //        else
          //        {
          //            inner4 = inner3 + ";;";
          //        }


          //        LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "GetModuleByModuleId",
          //               "0000000000", ex.Message, inner4, ex.StackTrace, null, dateTime);
          //        OBJ_LG_FNR_MODULE_MAP.ERROR = ex.Message;
          //        return OBJ_LG_FNR_MODULE_MAP;
          //    }
          //}
          //#endregion

        #region Fetch all
        public static IEnumerable<LG_SYS_CLD_TYPE_MAP> GetAllCalendarType()
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            string FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "CalendarType").Select(x => x.FUNCTION_ID).SingleOrDefault();

            List<LG_SYS_CLD_TYPE_MAP> LIST_LG_SYS_CLD_TYPE_MAP = null;
            LG_SYS_CLD_TYPE_MAP OBJ_LG_SYS_CLD_TYPE_MAP = new LG_SYS_CLD_TYPE_MAP();

            try
            {
                Obj_DBModelEntities.Configuration.LazyLoadingEnabled = false;
                Obj_DBModelEntities.Configuration.ProxyCreationEnabled = false;

                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);


                LIST_LG_SYS_CLD_TYPE_MAP = (from m in Obj_DBModelEntities.LG_SYS_CLD_TYPE
                                        
                                          where m.AUTH_STATUS_ID != "U" && m.LAST_ACTION != "DEL"
                                          orderby m.CLD_TYPE_NM ascending
                                        select new LG_SYS_CLD_TYPE_MAP
                                          {
                                              CLD_TYPE_ID = m.CLD_TYPE_ID,
                                              CLD_TYPE_NM = m.CLD_TYPE_NM,
                                              BASED_ON_CLD = m.BASED_ON_CLD,
                                              DEFAULT_CLD = m.DEFAULT_CLD,
                                              AUTH_STATUS_ID = m.AUTH_STATUS_ID,
                                              LAST_ACTION = m.LAST_ACTION,
                                              LAST_UPDATE_DT = m.LAST_UPDATE_DT,
                                              MAKE_DT = m.MAKE_DT
                                            
                                             
                                          }).ToList();
                return LIST_LG_SYS_CLD_TYPE_MAP;
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
                        OBJ_LG_SYS_CLD_TYPE_MAP.ERROR = validationError.ErrorMessage;
                        LIST_LG_SYS_CLD_TYPE_MAP.Add(OBJ_LG_SYS_CLD_TYPE_MAP);
                    }
                }
                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "GetAllCalendarType",
                     "0000000000", dbEx.Message, inner, dbEx.StackTrace, null, dateTime);
                return LIST_LG_SYS_CLD_TYPE_MAP;
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


                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "GetModules",
                       "0000000000", ex.Message, inner4, ex.StackTrace, null, dateTime);
                OBJ_LG_SYS_CLD_TYPE_MAP.ERROR = ex.Message;
                LIST_LG_SYS_CLD_TYPE_MAP.Add(OBJ_LG_SYS_CLD_TYPE_MAP);
                return LIST_LG_SYS_CLD_TYPE_MAP;
            }
        }
        #endregion
      
         
         #region bind calendar dropdown

        public static IEnumerable<SelectListItem> GetCalendarNameForDD()
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);

            string format = OutgoingResponseFormat.GetFormat();
            OutgoingResponseFormat.SetResponseFormat(format);

            var List_calendar_name = (from m in Obj_DBModelEntities.LG_SYS_CLD_TYPE
                                                select m);

            var selectList = new List<SelectListItem>();

            foreach (var element in List_calendar_name)
            {
                selectList.Add(new SelectListItem
                {
                    Value = element.CLD_TYPE_ID.ToString(),
                    Text = element.CLD_TYPE_NM
                });
            }
            if (selectList != null)
                return selectList;
            else
                throw new Exception("Invalid");
        }
      
         #endregion

        #region fetch single

        public static LG_SYS_CLD_TYPE_MAP GetCalendarByCalendarlId(string pCALENDAR_ID)
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            LG_SYS_CLD_TYPE_MAP OBJ_LG_SYS_CLD_TYPE_MAP = new LG_SYS_CLD_TYPE_MAP();
            string FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "CalendarType").Select(x => x.FUNCTION_ID).SingleOrDefault();

            try
            {
                int CALENDAR_ID = Convert.ToInt16(pCALENDAR_ID);
                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                OBJ_LG_SYS_CLD_TYPE_MAP = (from m in Obj_DBModelEntities.LG_SYS_CLD_TYPE

                                           where m.CLD_TYPE_ID == CALENDAR_ID
                                           select new LG_SYS_CLD_TYPE_MAP
                                         {
                                             CLD_TYPE_ID = m.CLD_TYPE_ID,
                                             CLD_TYPE_NM = m.CLD_TYPE_NM,
                                             BASED_ON_CLD = m.BASED_ON_CLD,
                                             DEFAULT_CLD = m.DEFAULT_CLD,
                                             AUTH_STATUS_ID = m.AUTH_STATUS_ID,
                                             LAST_ACTION = m.LAST_ACTION,
                                             LAST_UPDATE_DT = m.LAST_UPDATE_DT,
                                             MAKE_DT = m.MAKE_DT,
                                           
                                         }).SingleOrDefault();

              //  OBJ_LG_SYS_CLD_TYPE_MAP.LIST_OF_ALL_CALENDAR = (List<LG_SYS_CLD_TYPE_MAP>)GetCalendarNameForDD();
                OBJ_LG_SYS_CLD_TYPE_MAP.LIST_OF_ALL_CALENDAR = (List<SelectListItem>)GetCalendarNameForDD();

                return OBJ_LG_SYS_CLD_TYPE_MAP;
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
                        OBJ_LG_SYS_CLD_TYPE_MAP.ERROR = validationError.ErrorMessage;
                    }
                }
                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "GetModuleByModuleId",
                       "0000000000", dbEx.Message, inner, dbEx.StackTrace, null, dateTime);
                return OBJ_LG_SYS_CLD_TYPE_MAP;
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


                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "GetModuleByModuleId",
                       "0000000000", ex.Message, inner4, ex.StackTrace, null, dateTime);
                OBJ_LG_SYS_CLD_TYPE_MAP.ERROR = ex.Message;
                return OBJ_LG_SYS_CLD_TYPE_MAP;
            }
        }

       #endregion
       #endregion


   }
}

