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
    public class LG_FNR_APPLICATION_MAP
    {
        #region Properties

        [DataMember]
        public string APPLICATION_ID { get; set; }

        [DataMember]
        public string APPLICATION_NAME { get; set; }

        [DataMember]
        public string AUTH_STATUS_ID { get; set; }

        [DataMember]
        public string LAST_ACTION { get; set; }

        [DataMember]
        public DateTime? LAST_UPDATE_DT { get; set; }

        [DataMember]
        public DateTime MAKE_DT { get; set; }

        [DataMember]
        public string ERROR { get; set; }

        [DataMember]
        public int? APP_TYPE_ID { get; set; }

        [DataMember]
        public string APP_TYPE_NM { get; set; }

        [DataMember]
        public IEnumerable<SelectListItem> LIST_APP_TYPE { get; set; }

        #endregion


        #region Events

        public static string FUNCTION_ID = "010101001";

        #region Add New
        public static string AddApplication(string pAPPLICATION_NAME, string Psession_user, string pAPP_TYPE_ID)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
                Obj_DBModelEntities.Configuration.LazyLoadingEnabled = false;
                Obj_DBModelEntities.Configuration.ProxyCreationEnabled = false;
                string result = string.Empty;
                try
                {
                    Obj_DBModelEntities.Database.Connection.Open();
                    string format = OutgoingResponseFormat.GetFormat();
                    OutgoingResponseFormat.SetResponseFormat(format);

                    LG_FNR_APPLICATION OBJ_LG_FNR_APPLICATION = new LG_FNR_APPLICATION();
                    OBJ_LG_FNR_APPLICATION = Obj_DBModelEntities.LG_FNR_APPLICATION
                                            .Where(a => a.APPLICATION_NAME == pAPPLICATION_NAME).SingleOrDefault();
                    if (OBJ_LG_FNR_APPLICATION != null)
                    {
                        return "Application name already exists";
                    }
                    else
                        OBJ_LG_FNR_APPLICATION = new LG_FNR_APPLICATION();

                    //int serial_no = Convert.ToInt32(Obj_DBModelEntities.LG_FNR_APPLICATION
                    //                   .Max(x => (int?)x.APPLICATION_ID) ?? 0) + 1;

                    int id = (Obj_DBModelEntities.LG_FNR_APPLICATION
                             .Select(i => i.APPLICATION_ID).Cast<int?>().Max() ?? 0) + 1;

                    OBJ_LG_FNR_APPLICATION.APPLICATION_ID = id.ToString().PadLeft(2, '0');
                    OBJ_LG_FNR_APPLICATION.APPLICATION_NAME = pAPPLICATION_NAME;
                    OBJ_LG_FNR_APPLICATION.APP_TYPE_ID = string.IsNullOrWhiteSpace(pAPP_TYPE_ID) ? 0 : Convert.ToInt32(pAPP_TYPE_ID);
                    OBJ_LG_FNR_APPLICATION.AUTH_STATUS_ID = "U";
                    OBJ_LG_FNR_APPLICATION.LAST_ACTION = "ADD";
                    OBJ_LG_FNR_APPLICATION.MAKE_DT = System.DateTime.Now;
                    Obj_DBModelEntities.LG_FNR_APPLICATION.Add(OBJ_LG_FNR_APPLICATION);
                    Obj_DBModelEntities.SaveChanges();

                    LG_FNR_APPLICATION_MAP OBJ_LG_FNR_APPLICATION_MAP = new LG_FNR_APPLICATION_MAP();
                    Class_Conversion.LG_FNR_APPLICATION_REVERSE_CON(OBJ_LG_FNR_APPLICATION_MAP, OBJ_LG_FNR_APPLICATION);

                    #region Auth log
                    LG_AA_NFT_AUTH_LOG_MAP OBJ_LG_AA_NFT_AUTH_LOG_MAP = new LG_AA_NFT_AUTH_LOG_MAP();
                    FUNCTION_ID = OBJ_LG_AA_NFT_AUTH_LOG_MAP.FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "Application").Select(x => x.FUNCTION_ID).SingleOrDefault();
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_NAME = "LG_FNR_APPLICATION";
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_NM = "APPLICATION_ID";
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_VAL = OBJ_LG_FNR_APPLICATION.APPLICATION_ID;
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
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.MAKE_BY = Psession_user;
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.MAKE_DT = DateTime.Now;
                    LG_AA_NFT_AUTH_LOG_MAP.AddNftAuthLog(OBJ_LG_FNR_APPLICATION_MAP, OBJ_LG_AA_NFT_AUTH_LOG_MAP);
                    #endregion

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
                            result = "Can't Add Application(Db).";
                        }
                    }
                    LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "AddApplication",
                                                  "0000000000", dbEx.Message, inner, dbEx.StackTrace, Psession_user, dateTime);
                    return result;
                }
                catch (Exception ex)
                {
                    ts.Dispose();
                    string dateTime = Convert.ToString(System.DateTime.Now);
                    string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                    LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "AddApplication",
                                                  "0000000000", ex.Message, inner4, ex.StackTrace, Psession_user, dateTime);
                    result = "Can't Add Application.";
                    return result;
                }
                finally
                {
                    // Close the opened connection
                    if (Obj_DBModelEntities.Database.Connection.State == ConnectionState.Open)
                    {
                        Obj_DBModelEntities.Database.Connection.Close();
                    }
                }
            }
        }
        #endregion

        #region Update
        public static string UpdateApplication(string pAPPLICATION_ID, string pAPPLICATION_NAME, string psession_user, string pAPP_TYPE_ID)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
                string result = string.Empty;
                LG_FNR_APPLICATION_MAP OBJ_LG_FNR_APPLICATION_MAP_OLD = new LG_FNR_APPLICATION_MAP();
                LG_FNR_APPLICATION_MAP OBJ_LG_FNR_APPLICATION_MAP_NEW = new LG_FNR_APPLICATION_MAP();
                try
                {
                    string format = OutgoingResponseFormat.GetFormat();
                    OutgoingResponseFormat.SetResponseFormat(format);

                    LG_FNR_APPLICATION OBJ_LG_FNR_APPLICATION = Obj_DBModelEntities.LG_FNR_APPLICATION
                                                               .Where(m => m.APPLICATION_ID == pAPPLICATION_ID).SingleOrDefault();

                    if (OBJ_LG_FNR_APPLICATION == null)
                    {
                        return "False";
                    }

                    if (OBJ_LG_FNR_APPLICATION.APPLICATION_NAME == pAPPLICATION_NAME && (OBJ_LG_FNR_APPLICATION.APP_TYPE_ID == ( string.IsNullOrWhiteSpace(pAPP_TYPE_ID) ? 0 : Convert.ToInt32(pAPP_TYPE_ID))))
                    {
                        return "no changes made";
                    }
                    Class_Conversion.LG_FNR_APPLICATION_REVERSE_CON(OBJ_LG_FNR_APPLICATION_MAP_OLD, OBJ_LG_FNR_APPLICATION); //OLD

                    OBJ_LG_FNR_APPLICATION.AUTH_STATUS_ID = "U";
                    OBJ_LG_FNR_APPLICATION.LAST_ACTION = "EDT";
                    OBJ_LG_FNR_APPLICATION.LAST_UPDATE_DT = System.DateTime.Now;
                    Obj_DBModelEntities.SaveChanges();
                    Class_Conversion.LG_FNR_APPLICATION_REVERSE_CON(OBJ_LG_FNR_APPLICATION_MAP_NEW, OBJ_LG_FNR_APPLICATION); //NEW
                    OBJ_LG_FNR_APPLICATION_MAP_NEW.APPLICATION_NAME = pAPPLICATION_NAME;
                    OBJ_LG_FNR_APPLICATION_MAP_NEW.APP_TYPE_ID = string.IsNullOrWhiteSpace(pAPP_TYPE_ID) ? 0 : Convert.ToInt32(pAPP_TYPE_ID);

                    #region Auth log
                    LG_AA_NFT_AUTH_LOG_MAP OBJ_LG_AA_NFT_AUTH_LOG_MAP = new LG_AA_NFT_AUTH_LOG_MAP();
                    FUNCTION_ID = OBJ_LG_AA_NFT_AUTH_LOG_MAP.FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "Application").Select(x => x.FUNCTION_ID).SingleOrDefault();
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_NAME = "LG_FNR_APPLICATION";
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_NM = "APPLICATION_ID";
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_VAL = OBJ_LG_FNR_APPLICATION.APPLICATION_ID;
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
                    LG_AA_NFT_AUTH_LOG_MAP.AddNftAuthLog(OBJ_LG_FNR_APPLICATION_MAP_OLD, OBJ_LG_FNR_APPLICATION_MAP_NEW, OBJ_LG_AA_NFT_AUTH_LOG_MAP);
                    #endregion

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
                            result = "Can't Update Application(Db)";
                        }
                    }
                    LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "UpdateApplication",
                                                  "0000000000", dbEx.Message, inner, dbEx.StackTrace, psession_user, dateTime);
                    return result;
                }
                catch (Exception ex)
                {
                    ts.Dispose();
                    string dateTime = Convert.ToString(System.DateTime.Now);
                    string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                    LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "UpdateApplication",
                                                  "0000000000", ex.Message, inner4, ex.StackTrace, psession_user, dateTime);

                    result = "Can't Update Application";
                    return result;
                }
            }
        }
        #endregion

        #region Delete
        public static string DeleteApplication(string pAPPLICATION_ID, string psession_user)
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            string result = string.Empty;
            try
            {
                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                //LG_FNR_APPLICATION OBJ_LG_FNR_APPLICATION = Obj_DBModelEntities.LG_FNR_APPLICATION
                //                                           .Where(m => m.APPLICATION_ID == pAPPLICATION_ID).SingleOrDefault();

                LG_FNR_APPLICATION OBJ_LG_FNR_APPLICATION = (from a in Obj_DBModelEntities.LG_FNR_APPLICATION
                                                             where !(from s in Obj_DBModelEntities.LG_FNR_SERVICE
                                                                     select s.APPLICATION_ID).Contains(a.APPLICATION_ID)
                                                             && a.APPLICATION_ID == pAPPLICATION_ID
                                                             select a).SingleOrDefault();

                if (OBJ_LG_FNR_APPLICATION != null)
                {
                    OBJ_LG_FNR_APPLICATION.AUTH_STATUS_ID = "U";
                    OBJ_LG_FNR_APPLICATION.LAST_ACTION = "DEL";
                    OBJ_LG_FNR_APPLICATION.LAST_UPDATE_DT = System.DateTime.Now;
                    //Obj_DBModelEntities.LG_FNR_APPLICATION.Remove(OBJ_LG_FNR_APPLICATION);
                    Obj_DBModelEntities.SaveChanges();

                    #region Auth log
                    LG_AA_NFT_AUTH_LOG_MAP OBJ_LG_AA_NFT_AUTH_LOG_MAP = new LG_AA_NFT_AUTH_LOG_MAP();
                    FUNCTION_ID = OBJ_LG_AA_NFT_AUTH_LOG_MAP.FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "Application").Select(x => x.FUNCTION_ID).SingleOrDefault();
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_NAME = "LG_FNR_APPLICATION";
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_NM = "APPLICATION_ID";
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_VAL = OBJ_LG_FNR_APPLICATION.APPLICATION_ID;
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.ACTION_STATUS = "DEL";
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
                    LG_AA_NFT_AUTH_LOG_MAP.AddNftAuthLog(OBJ_LG_FNR_APPLICATION, OBJ_LG_AA_NFT_AUTH_LOG_MAP);
                    #endregion

                    result = "true";
                    return result;
                }
                else
                    return "Can't delete as this Application contains services.";
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
                        result = "Can't Delete Application(Db) " + validationError.ErrorMessage;
                    }
                }
                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "DeleteApplication",
                                              "0000000000", dbEx.Message, inner, dbEx.StackTrace, psession_user, dateTime);
                return result;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "DeleteApplication",
                                              "0000000000", ex.Message, inner4, ex.StackTrace, psession_user, dateTime);
                result = "Can't Delete Applicantion";
                return result;
            }
        }
        #endregion

        #region Fetch Single
        public static LG_FNR_APPLICATION_MAP GetApplicationByAppId(string pAPPLICATION_ID)
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            LG_FNR_APPLICATION_MAP OBJ_LG_FNR_APPLICATION_MAP = new LG_FNR_APPLICATION_MAP();

            try
            {
                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                OBJ_LG_FNR_APPLICATION_MAP = (from a in Obj_DBModelEntities.LG_FNR_APPLICATION
                                              where a.APPLICATION_ID == pAPPLICATION_ID
                                              select new LG_FNR_APPLICATION_MAP
                                              {
                                                  APPLICATION_ID = a.APPLICATION_ID,
                                                  APPLICATION_NAME = a.APPLICATION_NAME,
                                                  AUTH_STATUS_ID = a.AUTH_STATUS_ID,
                                                  LAST_ACTION = a.LAST_ACTION,
                                                  LAST_UPDATE_DT = a.LAST_UPDATE_DT,
                                                  MAKE_DT = a.MAKE_DT,
                                                  APP_TYPE_ID = a.APP_TYPE_ID
                                              }).SingleOrDefault();
                OBJ_LG_FNR_APPLICATION_MAP.LIST_APP_TYPE = DropDown.GetAppTypesForDD();
                return OBJ_LG_FNR_APPLICATION_MAP;
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
                        OBJ_LG_FNR_APPLICATION_MAP.ERROR = validationError.ErrorMessage;
                    }
                }
                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "GetApplicationByAppId",
                                              "0000000000", dbEx.Message, inner, dbEx.StackTrace, null, dateTime);
                return OBJ_LG_FNR_APPLICATION_MAP;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "GetApplicationByAppId",
                                              "0000000000", ex.Message, inner4, ex.StackTrace, null, dateTime);
                OBJ_LG_FNR_APPLICATION_MAP.ERROR = ex.Message;
                return OBJ_LG_FNR_APPLICATION_MAP;
            }
        }
        #endregion

        #region Fetch all
        public static IEnumerable<LG_FNR_APPLICATION_MAP> GetApplications()
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            List<LG_FNR_APPLICATION_MAP> LIST_LG_FNR_APPLICATION_MAP = null;
            LG_FNR_APPLICATION_MAP OBJ_LG_FNR_APPLICATION_MAP = new LG_FNR_APPLICATION_MAP();

            try
            {
                Obj_DBModelEntities.Configuration.LazyLoadingEnabled = false;
                Obj_DBModelEntities.Configuration.ProxyCreationEnabled = false;

                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);


                LIST_LG_FNR_APPLICATION_MAP = (from a in Obj_DBModelEntities.LG_FNR_APPLICATION
                                               where (a.APPLICATION_ID != null && a.AUTH_STATUS_ID == "A" && (a.LAST_ACTION != "DEL"))
                                               orderby a.APPLICATION_ID descending
                                               select new LG_FNR_APPLICATION_MAP
                                               {
                                                   APPLICATION_ID = a.APPLICATION_ID,
                                                   APPLICATION_NAME = a.APPLICATION_NAME,
                                                   AUTH_STATUS_ID = a.AUTH_STATUS_ID,
                                                   LAST_ACTION = a.LAST_ACTION,
                                                   LAST_UPDATE_DT = a.LAST_UPDATE_DT,
                                                   MAKE_DT = a.MAKE_DT,
                                                   APP_TYPE_ID = a.APP_TYPE_ID
                                               }).ToList();

                if (LIST_LG_FNR_APPLICATION_MAP != null)
                {
                    LIST_LG_FNR_APPLICATION_MAP.ForEach(a =>
                    {
                        a.APP_TYPE_NM = Obj_DBModelEntities.LG_FNR_APPLICATION_TYPE.Where(m => m.APP_TYPE_ID == a.APP_TYPE_ID).Select(m => m.APP_NM).SingleOrDefault();
                    });
                }
                return LIST_LG_FNR_APPLICATION_MAP;
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
                        OBJ_LG_FNR_APPLICATION_MAP.ERROR = validationError.ErrorMessage;
                        LIST_LG_FNR_APPLICATION_MAP.Add(OBJ_LG_FNR_APPLICATION_MAP);
                    }
                }
                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "GetApplications",
                      "0000000000", dbEx.Message, inner, dbEx.StackTrace, null, dateTime);
                return LIST_LG_FNR_APPLICATION_MAP;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "GetApplicationByAppId",
                                              "0000000000", ex.Message, inner4, ex.StackTrace, null, dateTime);
                OBJ_LG_FNR_APPLICATION_MAP.ERROR = ex.Message;
                LIST_LG_FNR_APPLICATION_MAP.Add(OBJ_LG_FNR_APPLICATION_MAP);
                return LIST_LG_FNR_APPLICATION_MAP;
            }
        }
        #endregion

        #region Validate
        public static LG_FNR_APPLICATION_MAP GetApplicationByAppName(string pAPPLICATION_NAME)
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            LG_FNR_APPLICATION_MAP OBJ_LG_FNR_APPLICATION_MAP = new LG_FNR_APPLICATION_MAP();

            try
            {
                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                OBJ_LG_FNR_APPLICATION_MAP = Obj_DBModelEntities.LG_FNR_APPLICATION
                                             .Where(a => a.APPLICATION_NAME == pAPPLICATION_NAME)
                                             .Select(a => new LG_FNR_APPLICATION_MAP
                                              {
                                                  APPLICATION_ID = a.APPLICATION_ID,
                                                  APPLICATION_NAME = a.APPLICATION_NAME,
                                                  AUTH_STATUS_ID = a.AUTH_STATUS_ID,
                                                  LAST_ACTION = a.LAST_ACTION,
                                                  LAST_UPDATE_DT = a.LAST_UPDATE_DT,
                                                  MAKE_DT = a.MAKE_DT,
                                                  APP_TYPE_ID = a.APP_TYPE_ID
                                              }).SingleOrDefault();
                return OBJ_LG_FNR_APPLICATION_MAP;
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
                        OBJ_LG_FNR_APPLICATION_MAP.ERROR = validationError.ErrorMessage;
                    }
                }
                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "GetApplications",
                                              "0000000000", dbEx.Message, inner, dbEx.StackTrace, null, dateTime);
                return OBJ_LG_FNR_APPLICATION_MAP;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "GetApplicationByAppId",
                                              "0000000000", ex.Message, inner4, ex.StackTrace, null, dateTime);
                OBJ_LG_FNR_APPLICATION_MAP.ERROR = ex.Message;
                return OBJ_LG_FNR_APPLICATION_MAP;
            }
        }
        #endregion

        #endregion
    }
}
