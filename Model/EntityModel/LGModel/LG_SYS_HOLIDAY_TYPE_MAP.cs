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
  public class LG_SYS_HOLIDAY_TYPE_MAP
    {
        #region Properties


        public int HOLIDAY_TYPE_ID { get; set; }

        public string HOLIDAY_TYPE_NM { get; set; }

        public short WEEKEND { get; set; }
        public string WEEKEND_TEXT { get; set; }
        public bool WEEKEND_B { get; set; }


        public string MAKE_BY { get; set; }

        public Nullable<System.DateTime> MAKE_DT { get; set; }

        public string AUTH_STATUS_ID { get; set; }

        public string LAST_ACTION { get; set; }

        public string ERROR { get; set; }

        public Nullable<System.DateTime> LAST_UPDATE_DT { get; set; }

        public List<LG_SYS_HOLIDAY_TYPE_MAP> obj_LIST_LG_SYS_HOLIDAY_TYPE_MAP = new List<LG_SYS_HOLIDAY_TYPE_MAP>();
        #endregion

        #region event

        #region Add New
        public static string AddHoliday(string PHOLIDAY_TYPE_NM, string WEEKEND_B, string PMAKE_BY)
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            string FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "HolidayType").Select(x => x.FUNCTION_ID).SingleOrDefault();


            LG_SYS_CLD_HOLIDAY_TYPE OBJ_LG_SYS_HOLIDAY_TYPE = new LG_SYS_CLD_HOLIDAY_TYPE();
            string result = string.Empty;
            try
            {

                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                OBJ_LG_SYS_HOLIDAY_TYPE = Obj_DBModelEntities.LG_SYS_CLD_HOLIDAY_TYPE
                                        .Where(m => m.HOLIDAY_TYPE_NM == PHOLIDAY_TYPE_NM).SingleOrDefault();
                if (OBJ_LG_SYS_HOLIDAY_TYPE != null)
                {
                    return "Holiday name already exists";
                }


                int id = (Obj_DBModelEntities.LG_SYS_CLD_HOLIDAY_TYPE
                          .Select(i => i.HOLIDAY_TYPE_ID).Cast<int?>().Max() ?? 0) + 1;

                OBJ_LG_SYS_HOLIDAY_TYPE = new LG_SYS_CLD_HOLIDAY_TYPE();
                OBJ_LG_SYS_HOLIDAY_TYPE.HOLIDAY_TYPE_ID = (short)id;
                OBJ_LG_SYS_HOLIDAY_TYPE.HOLIDAY_TYPE_NM = PHOLIDAY_TYPE_NM;
                if (WEEKEND_B.ToLower() == "true")
                {
                    OBJ_LG_SYS_HOLIDAY_TYPE.WEEKEND = 1;
                }
                else
                    OBJ_LG_SYS_HOLIDAY_TYPE.WEEKEND = 0;




             
                
                OBJ_LG_SYS_HOLIDAY_TYPE.AUTH_STATUS_ID = "U";
                OBJ_LG_SYS_HOLIDAY_TYPE.LAST_ACTION = "ADD";
                OBJ_LG_SYS_HOLIDAY_TYPE.MAKE_BY = PMAKE_BY;
                OBJ_LG_SYS_HOLIDAY_TYPE.MAKE_DT = System.DateTime.Now;
                OBJ_LG_SYS_HOLIDAY_TYPE.LAST_UPDATE_DT = System.DateTime.Now;
                Obj_DBModelEntities.LG_SYS_CLD_HOLIDAY_TYPE.Add(OBJ_LG_SYS_HOLIDAY_TYPE);



                Obj_DBModelEntities.SaveChanges();
                result = "True";
        

                #region Auth log
                LG_AA_NFT_AUTH_LOG_MAP OBJ_LG_AA_NFT_AUTH_LOG_MAP = new LG_AA_NFT_AUTH_LOG_MAP();
                FUNCTION_ID = OBJ_LG_AA_NFT_AUTH_LOG_MAP.FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "HolidayType").Select(x => x.FUNCTION_ID).SingleOrDefault();
                OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_NAME = "LG_SYS_CLD_HOLIDAY_TYPE";
                OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_NM = "HOLIDAY_TYPE_ID";
                OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_VAL = OBJ_LG_SYS_HOLIDAY_TYPE.HOLIDAY_TYPE_ID.ToString();
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
                OBJ_LG_AA_NFT_AUTH_LOG_MAP.MAKE_BY = PMAKE_BY;
                OBJ_LG_AA_NFT_AUTH_LOG_MAP.MAKE_DT = DateTime.Now;
                LG_AA_NFT_AUTH_LOG_MAP.AddNftAuthLog(OBJ_LG_SYS_HOLIDAY_TYPE, OBJ_LG_AA_NFT_AUTH_LOG_MAP);
                #endregion


                return result;
            }

            #region catch


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
                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "AddHoliday",
                       "0000000000", dbEx.Message, inner, dbEx.StackTrace, PMAKE_BY, dateTime);
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


                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "AddHoliday",
                       "0000000000", ex.Message, inner4, ex.StackTrace, PMAKE_BY, dateTime);
                result = "Can't Add holiday ";
                return result;
            }
            #endregion
        }
        #endregion

       
        #region Fetch all
        
        public static IEnumerable<LG_SYS_HOLIDAY_TYPE_MAP> GetAllHoliday()
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            string FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "HolidayType").Select(x => x.FUNCTION_ID).SingleOrDefault();

            List<LG_SYS_HOLIDAY_TYPE_MAP> LIST_LG_SYS_HOLIDAY_TYPE_MAP = null;
            LG_SYS_HOLIDAY_TYPE_MAP OBJ_LG_SYS_HOLIDAY_TYPE_MAP = new LG_SYS_HOLIDAY_TYPE_MAP();

            try
            {
                Obj_DBModelEntities.Configuration.LazyLoadingEnabled = false;
                Obj_DBModelEntities.Configuration.ProxyCreationEnabled = false;

                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);


                LIST_LG_SYS_HOLIDAY_TYPE_MAP = (from m in Obj_DBModelEntities.LG_SYS_CLD_HOLIDAY_TYPE

                                            where m.AUTH_STATUS_ID != "U" && m.LAST_ACTION != "DEL"
                                            orderby m.HOLIDAY_TYPE_NM ascending
                                            select new LG_SYS_HOLIDAY_TYPE_MAP
                                            {
                                               HOLIDAY_TYPE_ID = m.HOLIDAY_TYPE_ID,
                                                HOLIDAY_TYPE_NM = m.HOLIDAY_TYPE_NM,
                                                WEEKEND = m.WEEKEND,
                                               MAKE_BY = m.MAKE_BY


                                            }).ToList();

                foreach (LG_SYS_HOLIDAY_TYPE_MAP list_LG_SYS_HOLIDAY_TYPE_MAP_new in LIST_LG_SYS_HOLIDAY_TYPE_MAP)
                {
                    //LIST_LG_SYS_HOLIDAY_TYPE_MAP = new List<LG_SYS_HOLIDAY_TYPE_MAP>();

                    if (list_LG_SYS_HOLIDAY_TYPE_MAP_new.WEEKEND == 1)
                    {
                        list_LG_SYS_HOLIDAY_TYPE_MAP_new.WEEKEND_TEXT = "Weekend";
                    }
                    else
                    {

                        list_LG_SYS_HOLIDAY_TYPE_MAP_new.WEEKEND_TEXT = "Not Weekend";

                    }


                  //  LIST_LG_SYS_HOLIDAY_TYPE_MAP.Add(list_LG_SYS_HOLIDAY_TYPE_MAP_new);

                }
                return LIST_LG_SYS_HOLIDAY_TYPE_MAP;
            }

            #region

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
                        OBJ_LG_SYS_HOLIDAY_TYPE_MAP.ERROR = validationError.ErrorMessage;
                        LIST_LG_SYS_HOLIDAY_TYPE_MAP.Add(OBJ_LG_SYS_HOLIDAY_TYPE_MAP);
                    }
                }
                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "GetAllCalendarType",
                     "0000000000", dbEx.Message, inner, dbEx.StackTrace, null, dateTime);
                return LIST_LG_SYS_HOLIDAY_TYPE_MAP;
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
                OBJ_LG_SYS_HOLIDAY_TYPE_MAP.ERROR = ex.Message;
                LIST_LG_SYS_HOLIDAY_TYPE_MAP.Add(OBJ_LG_SYS_HOLIDAY_TYPE_MAP);
                return LIST_LG_SYS_HOLIDAY_TYPE_MAP;
            }

            #endregion
        }
       
        #endregion

        #region fetch single

        public static LG_SYS_HOLIDAY_TYPE_MAP GetHolidayById(string pHOLIDAY_TYPE_ID)
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            LG_SYS_HOLIDAY_TYPE_MAP OBJ_LG_SYS_HOLIDAY_TYPE_MAP = new LG_SYS_HOLIDAY_TYPE_MAP();
            string FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "HolidayType").Select(x => x.FUNCTION_ID).SingleOrDefault();

            try
            {
                int HOLIDAY_TYPE_ID = Convert.ToInt16(pHOLIDAY_TYPE_ID);
                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                OBJ_LG_SYS_HOLIDAY_TYPE_MAP = (from m in Obj_DBModelEntities.LG_SYS_CLD_HOLIDAY_TYPE

                                               where m.HOLIDAY_TYPE_ID == HOLIDAY_TYPE_ID
                                               select new LG_SYS_HOLIDAY_TYPE_MAP
                                           {
                                               HOLIDAY_TYPE_ID = m.HOLIDAY_TYPE_ID,
                                               HOLIDAY_TYPE_NM = m.HOLIDAY_TYPE_NM,
                                               WEEKEND = m.WEEKEND,
                                              
                                               AUTH_STATUS_ID = m.AUTH_STATUS_ID,
                                               LAST_ACTION = m.LAST_ACTION,
                                               LAST_UPDATE_DT = m.LAST_UPDATE_DT,
                                               MAKE_DT = m.MAKE_DT,

                                           }).SingleOrDefault();

               
                return OBJ_LG_SYS_HOLIDAY_TYPE_MAP;
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
                        OBJ_LG_SYS_HOLIDAY_TYPE_MAP.ERROR = validationError.ErrorMessage;
                    }
                }
                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "GetHolidayById",
                       "0000000000", dbEx.Message, inner, dbEx.StackTrace, null, dateTime);
                return OBJ_LG_SYS_HOLIDAY_TYPE_MAP;
            }

            #region
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
                OBJ_LG_SYS_HOLIDAY_TYPE_MAP.ERROR = ex.Message;
                return OBJ_LG_SYS_HOLIDAY_TYPE_MAP;
            }

            #endregion
        }

        #endregion

        #region Update
        public static string UpdateHolidayType(string pHOLIDAY_TYPE_ID, string pHOLIDAY_TYPE_NM, string pWEEKEND_B, string psession_user)
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            DBModelEntities Obj_DBModelEntities_old = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
           
            string result = string.Empty;
            string FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "HolidayType").Select(x => x.FUNCTION_ID).SingleOrDefault();

            try
            {

                int HOLIDAY_TYPE_ID = Convert.ToInt16(pHOLIDAY_TYPE_ID);

                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                LG_SYS_CLD_HOLIDAY_TYPE OBJ_LG_SYS_CLD_HOLIDAY_TYPE_OLD = Obj_DBModelEntities_old.LG_SYS_CLD_HOLIDAY_TYPE
                                                    .Where(s => s.HOLIDAY_TYPE_ID == HOLIDAY_TYPE_ID).SingleOrDefault();

                LG_SYS_CLD_HOLIDAY_TYPE OBJ_LG_SYS_CLD_HOLIDAY_TYPE = Obj_DBModelEntities.LG_SYS_CLD_HOLIDAY_TYPE
                                                    .Where(s => s.HOLIDAY_TYPE_ID == HOLIDAY_TYPE_ID).SingleOrDefault();

                OBJ_LG_SYS_CLD_HOLIDAY_TYPE.HOLIDAY_TYPE_NM = pHOLIDAY_TYPE_NM;

                if (pWEEKEND_B.ToLower() == "true")
                {
                    OBJ_LG_SYS_CLD_HOLIDAY_TYPE.WEEKEND = 1;
                }
                else
                    OBJ_LG_SYS_CLD_HOLIDAY_TYPE.WEEKEND = 0;


                OBJ_LG_SYS_CLD_HOLIDAY_TYPE.AUTH_STATUS_ID = "U";
                OBJ_LG_SYS_CLD_HOLIDAY_TYPE.LAST_ACTION = "EDT";
                OBJ_LG_SYS_CLD_HOLIDAY_TYPE.LAST_UPDATE_DT = System.DateTime.Now;
             

                #region Auth log
                LG_AA_NFT_AUTH_LOG_MAP OBJ_LG_AA_NFT_AUTH_LOG_MAP = new LG_AA_NFT_AUTH_LOG_MAP();
                FUNCTION_ID = OBJ_LG_AA_NFT_AUTH_LOG_MAP.FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "HolidayType").Select(x => x.FUNCTION_ID).SingleOrDefault();
                OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_NAME = "LG_SYS_CLD_HOLIDAY_TYPE";
                OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_NM = "HOLIDAY_TYPE_ID";
                OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_VAL = OBJ_LG_SYS_CLD_HOLIDAY_TYPE.HOLIDAY_TYPE_ID.ToString();
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
                LG_AA_NFT_AUTH_LOG_MAP.AddNftAuthLog(OBJ_LG_SYS_CLD_HOLIDAY_TYPE_OLD,OBJ_LG_SYS_CLD_HOLIDAY_TYPE, OBJ_LG_AA_NFT_AUTH_LOG_MAP);
                result = "True";
                #endregion

                if(result == "True")
                {

                Obj_DBModelEntities.SaveChanges();
              }
                return result;
            }

            #region catch
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

            #endregion
        }
        #endregion

        #endregion

    }
}
