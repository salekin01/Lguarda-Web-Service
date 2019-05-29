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

namespace Model.EntityModel.LGModel
{
    [DataContract]
    public class LG_USER_AD_BINDING_MAP
    {
        
        #region Properties
        
        [DataMember]
        public int SL { get; set; }

        [DataMember]
        public string USER_ID { get; set; }

        [DataMember]
        public string DOMAIN_ID { get; set; }

        [DataMember]
        public string DOMAIN { get; set; }
        
        [DataMember]
        public Int32 AD_ACTIVE_FLAG { get; set; }

        [DataMember]
        public string AUTH_STATUS_ID { get; set; }

        [DataMember]
        public string LAST_ACTION { get; set; }

        [DataMember]
        public DateTime? LAST_UPDATE_DT { get; set; }

        [DataMember]
        public DateTime? MAKE_DT { get; set; }

        [DataMember]
        public string ERROR { get; set; }

        [DataMember]
        public bool AD_ACTIVE_FLAG_B { get; set; }

        #endregion



        #region Events
        public static string FUNC_ID = null;
        public static DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);

        public static string FUNCTION_ID = "010102006";

        #region Add New
        public static string BindADUser(string pUSER_ID, string pDOMAIN_ID, string pDOMAIN, string pAD_ACTIVE_FLAG, string Psession_user)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
                string result = string.Empty;
                try
                {
                    
                    string format = OutgoingResponseFormat.GetFormat();
                    OutgoingResponseFormat.SetResponseFormat(format);

                    LG_USER_AD_BINDING OBJ_LG_USER_AD_BINDING = new LG_USER_AD_BINDING();
                    OBJ_LG_USER_AD_BINDING = Obj_DBModelEntities.LG_USER_AD_BINDING
                                            .Where(a => a.DOMAIN_ID == pDOMAIN_ID).SingleOrDefault();
                    if (OBJ_LG_USER_AD_BINDING != null)
                    {
                        return "This domain user already exists. Please add a new one.";
                    }

              
                    int id = (Obj_DBModelEntities.LG_USER_AD_BINDING
                             .Select(i => i.SL).Cast<int?>().Max() ?? 0) + 1;

                    OBJ_LG_USER_AD_BINDING = new LG_USER_AD_BINDING();
                    BooleanConversion.LG_USER_AD_BINDING_MAP_BOOL_TO_INT(pAD_ACTIVE_FLAG, OBJ_LG_USER_AD_BINDING);

                    OBJ_LG_USER_AD_BINDING.SL = id;
                    OBJ_LG_USER_AD_BINDING.DOMAIN_ID = pDOMAIN_ID;
                    OBJ_LG_USER_AD_BINDING.DOMAIN = pDOMAIN;
                    OBJ_LG_USER_AD_BINDING.USER_ID = pUSER_ID;
                    //OBJ_LG_USER_AD_BINDING.AD_ACTIVE_FLAG = (short)(string.IsNullOrWhiteSpace(pAD_ACTIVE_FLAG) ? 0 : Convert.ToInt32(pAD_ACTIVE_FLAG));
                    OBJ_LG_USER_AD_BINDING.AUTH_STATUS_ID = "U";
                    OBJ_LG_USER_AD_BINDING.LAST_ACTION = "ADD";
                    //OBJ_LG_USER_AD_BINDING.LAST_UPDATE_DT = System.DateTime.Now;
                    OBJ_LG_USER_AD_BINDING.MAKE_DT = System.DateTime.Now;

                    Obj_DBModelEntities.LG_USER_AD_BINDING.Add(OBJ_LG_USER_AD_BINDING);
                    Obj_DBModelEntities.SaveChanges();

                    LG_USER_AD_BINDING_MAP OBJ_LG_USER_AD_BINDING_MAP = new LG_USER_AD_BINDING_MAP();

                    Class_Conversion.LG_USER_AD_BINDING_REVERSE_CON(OBJ_LG_USER_AD_BINDING_MAP, OBJ_LG_USER_AD_BINDING);

                    #region Auth log
                    LG_AA_NFT_AUTH_LOG_MAP OBJ_LG_AA_NFT_AUTH_LOG_MAP = new LG_AA_NFT_AUTH_LOG_MAP();
                    FUNCTION_ID = OBJ_LG_AA_NFT_AUTH_LOG_MAP.FUNCTION_ID = "010102006"; //Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "Application").Select(x => x.FUNCTION_ID).SingleOrDefault();
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_NAME = "LG_USER_AD_BINDING";
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_NM = "SL";
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_VAL = OBJ_LG_USER_AD_BINDING.SL.ToString();
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
                    LG_AA_NFT_AUTH_LOG_MAP.AddNftAuthLog(OBJ_LG_USER_AD_BINDING_MAP, OBJ_LG_AA_NFT_AUTH_LOG_MAP);
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
                            result = "Can't bind Ad user(Db).";
                        }
                    }
                    LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "BindADUser",
                                                  "0000000000", dbEx.Message, inner, dbEx.StackTrace, Psession_user, dateTime);
                    return result;
                }
                catch (Exception ex)
                {
                    ts.Dispose();
                    string dateTime = Convert.ToString(System.DateTime.Now);
                    string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                    LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "BindADUser",
                                                  "0000000000", ex.Message, inner4, ex.StackTrace, Psession_user, dateTime);
                    result = "Can't bind Ad user.";
                    return result;
                }
            }
        }
        #endregion

        #region Fetch all
        public static IEnumerable<LG_USER_AD_BINDING_MAP> GetallAdUser()
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            List<LG_USER_AD_BINDING_MAP> LIST_LG_USER_AD_BINDING_MAP = null;
            LG_USER_AD_BINDING_MAP OBJ_LG_USER_AD_BINDING_MAP = new LG_USER_AD_BINDING_MAP();

            try
            {
                Obj_DBModelEntities.Configuration.LazyLoadingEnabled = false;
                Obj_DBModelEntities.Configuration.ProxyCreationEnabled = false;

                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);


                LIST_LG_USER_AD_BINDING_MAP = (from a in Obj_DBModelEntities.LG_USER_AD_BINDING
                                               where (a.DOMAIN_ID != null && a.AUTH_STATUS_ID == "A" && (a.LAST_ACTION != "DEL"))
                                               orderby a.SL descending
                                               select new LG_USER_AD_BINDING_MAP
                                               {
                                                   SL = a.SL,
                                                   USER_ID = a.USER_ID,
                                                   DOMAIN = a.DOMAIN,
                                                   DOMAIN_ID = a.DOMAIN_ID,
                                                   AUTH_STATUS_ID = a.AUTH_STATUS_ID,
                                                   LAST_ACTION = a.LAST_ACTION,
                                                   LAST_UPDATE_DT= a.LAST_UPDATE_DT,
                                                   MAKE_DT = a.MAKE_DT
                                               }).ToList();
                return LIST_LG_USER_AD_BINDING_MAP;
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
                        OBJ_LG_USER_AD_BINDING_MAP.ERROR = validationError.ErrorMessage;
                        LIST_LG_USER_AD_BINDING_MAP.Add(OBJ_LG_USER_AD_BINDING_MAP);
                    }
                }
                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "GetallAdUser",
                                              "0000000000", dbEx.Message, inner, dbEx.StackTrace, null, dateTime);
                return LIST_LG_USER_AD_BINDING_MAP;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "GetallAdUser",
                                              "0000000000", ex.Message, inner4, ex.StackTrace, null, dateTime);
                OBJ_LG_USER_AD_BINDING_MAP.ERROR = ex.Message;
                LIST_LG_USER_AD_BINDING_MAP.Add(OBJ_LG_USER_AD_BINDING_MAP);
                return LIST_LG_USER_AD_BINDING_MAP;
            }
        }
        #endregion

        #region Fetch single
        public static LG_USER_AD_BINDING_MAP GetBindUser(string SL)
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            LG_USER_AD_BINDING_MAP OBJ_LG_USER_AD_BINDING_MAP = new LG_USER_AD_BINDING_MAP();

            try
            {
                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                int sl_no = Convert.ToInt32(SL);

                OBJ_LG_USER_AD_BINDING_MAP = (from a in Obj_DBModelEntities.LG_USER_AD_BINDING
                                              where a.SL == sl_no
                                              select new LG_USER_AD_BINDING_MAP
                                              {
                                                  SL = a.SL,
                                                  USER_ID = a.USER_ID,
                                                  DOMAIN = a.DOMAIN,
                                                  DOMAIN_ID = a.DOMAIN_ID,
                                                  AD_ACTIVE_FLAG = a.AD_ACTIVE_FLAG,
                                                  AUTH_STATUS_ID = a.AUTH_STATUS_ID,
                                                  LAST_ACTION = a.LAST_ACTION,
                                                  LAST_UPDATE_DT = a.LAST_UPDATE_DT,
                                                  MAKE_DT = a.MAKE_DT
                                              }).SingleOrDefault();
                BooleanConversion.LG_USER_AD_BINDING_MAP_INT_TO_BOOL(OBJ_LG_USER_AD_BINDING_MAP);
                return OBJ_LG_USER_AD_BINDING_MAP;
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
                        OBJ_LG_USER_AD_BINDING_MAP.ERROR = validationError.ErrorMessage;
                    }
                }
                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "GetBindUser",
                                              "0000000000", dbEx.Message, inner, dbEx.StackTrace, null, dateTime);
                return OBJ_LG_USER_AD_BINDING_MAP;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "GetBindUser",
                                              "0000000000", ex.Message, inner4, ex.StackTrace, null, dateTime);
                OBJ_LG_USER_AD_BINDING_MAP.ERROR = ex.Message;
                return OBJ_LG_USER_AD_BINDING_MAP;
            }
        }
        public static string GetBindUserByDomainId(string pDOMAIN_ID)
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            LG_USER_AD_BINDING_MAP OBJ_LG_USER_AD_BINDING_MAP = new LG_USER_AD_BINDING_MAP();
            string pD_ENABLE_FLAG = string.Empty;
            try
            {
                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                pD_ENABLE_FLAG = ConfigurationManager.AppSettings["AD_ENABLE_FLAG"].ToString();
                if (!string.IsNullOrWhiteSpace(pD_ENABLE_FLAG) && pD_ENABLE_FLAG != "1")
                {
                    return "";
                }
                OBJ_LG_USER_AD_BINDING_MAP = (from a in Obj_DBModelEntities.LG_USER_AD_BINDING
                                              where a.DOMAIN_ID == pDOMAIN_ID
                                              select new LG_USER_AD_BINDING_MAP
                                              {
                                                  SL = a.SL,
                                                  USER_ID = a.USER_ID,
                                                  DOMAIN = a.DOMAIN,
                                                  DOMAIN_ID = a.DOMAIN_ID,
                                                  AD_ACTIVE_FLAG = a.AD_ACTIVE_FLAG,
                                                  AUTH_STATUS_ID = a.AUTH_STATUS_ID,
                                                  LAST_ACTION = a.LAST_ACTION,
                                                  LAST_UPDATE_DT = a.LAST_UPDATE_DT,
                                                  MAKE_DT = a.MAKE_DT
                                              }).SingleOrDefault();
                if (OBJ_LG_USER_AD_BINDING_MAP != null)
                {
                    BooleanConversion.LG_USER_AD_BINDING_MAP_INT_TO_BOOL(OBJ_LG_USER_AD_BINDING_MAP);
                    return OBJ_LG_USER_AD_BINDING_MAP.USER_ID;
                }
                else
                    return "";
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
                        OBJ_LG_USER_AD_BINDING_MAP.ERROR = validationError.ErrorMessage;
                    }
                }
                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "GetBindUserByDomainId",
                                              "0000000000", dbEx.Message, inner, dbEx.StackTrace, null, dateTime);
                return "";
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "GetBindUserByDomainId",
                                              "0000000000", ex.Message, inner4, ex.StackTrace, null, dateTime);
                OBJ_LG_USER_AD_BINDING_MAP.ERROR = ex.Message;
                return "";
            }
        }
        #endregion

        #region Update
        public static string UpdateADUser(string SL, string pDOMAIN_ID, string pUSER_ID, string pDOMAIN, string pAD_ACTIVE_FLAG, string psession_user)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
                string result = string.Empty;
                LG_USER_AD_BINDING_MAP OBJ_LG_USER_AD_BINDING_MAP_OLD = new LG_USER_AD_BINDING_MAP();
                LG_USER_AD_BINDING_MAP OBJ_LG_USER_AD_BINDING_MAP_NEW = new LG_USER_AD_BINDING_MAP();
                try
                {
                    string format = OutgoingResponseFormat.GetFormat();
                    OutgoingResponseFormat.SetResponseFormat(format);

                    int sl_no = Convert.ToInt32(SL);
                    LG_USER_AD_BINDING OBJ_LG_USER_AD_BINDING = Obj_DBModelEntities.LG_USER_AD_BINDING
                                                               .Where(m => m.SL == sl_no).SingleOrDefault();

                    if (OBJ_LG_USER_AD_BINDING == null)
                    {
                        return "False";
                    }

                    int AD_ACTIVE_FLAG = string.IsNullOrWhiteSpace(pAD_ACTIVE_FLAG) ? OBJ_LG_USER_AD_BINDING.AD_ACTIVE_FLAG : (pAD_ACTIVE_FLAG.ToLower() == "true" ? 1 : 0);
                    if (OBJ_LG_USER_AD_BINDING.DOMAIN_ID == pDOMAIN_ID && OBJ_LG_USER_AD_BINDING.DOMAIN == pDOMAIN && OBJ_LG_USER_AD_BINDING.USER_ID == pUSER_ID && OBJ_LG_USER_AD_BINDING.AD_ACTIVE_FLAG == AD_ACTIVE_FLAG)
                    {
                        return "no changes made";
                    }

                    Class_Conversion.LG_USER_AD_BINDING_REVERSE_CON(OBJ_LG_USER_AD_BINDING_MAP_OLD, OBJ_LG_USER_AD_BINDING); //OLD

                    OBJ_LG_USER_AD_BINDING.AUTH_STATUS_ID = "U";
                    OBJ_LG_USER_AD_BINDING.LAST_ACTION = "EDT";
                    OBJ_LG_USER_AD_BINDING.LAST_UPDATE_DT = System.DateTime.Now;
                    Obj_DBModelEntities.SaveChanges();
                    Class_Conversion.LG_USER_AD_BINDING_REVERSE_CON(OBJ_LG_USER_AD_BINDING_MAP_NEW, OBJ_LG_USER_AD_BINDING); //NEW
                    OBJ_LG_USER_AD_BINDING_MAP_NEW.USER_ID = pUSER_ID;
                    OBJ_LG_USER_AD_BINDING_MAP_NEW.DOMAIN = pDOMAIN;
                    OBJ_LG_USER_AD_BINDING_MAP_NEW.DOMAIN_ID = pDOMAIN_ID;
                    OBJ_LG_USER_AD_BINDING_MAP_NEW.AD_ACTIVE_FLAG = AD_ACTIVE_FLAG;


                    #region Auth log
                    LG_AA_NFT_AUTH_LOG_MAP OBJ_LG_AA_NFT_AUTH_LOG_MAP = new LG_AA_NFT_AUTH_LOG_MAP();
                    FUNCTION_ID = OBJ_LG_AA_NFT_AUTH_LOG_MAP.FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "Application").Select(x => x.FUNCTION_ID).SingleOrDefault();
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_NAME = "LG_USER_AD_BINDING";
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_NM = "SL";
                    OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_VAL = OBJ_LG_USER_AD_BINDING.SL.ToString();
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
                    LG_AA_NFT_AUTH_LOG_MAP.AddNftAuthLog(OBJ_LG_USER_AD_BINDING_MAP_OLD, OBJ_LG_USER_AD_BINDING_MAP_NEW, OBJ_LG_AA_NFT_AUTH_LOG_MAP);
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
                            result = "Can't update AD user(Db)";
                        }
                    }
                    LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "UpdateADUser",
                                                  "0000000000", dbEx.Message, inner, dbEx.StackTrace, psession_user, dateTime);
                    return result;
                }
                catch (Exception ex)
                {
                    ts.Dispose();
                    string dateTime = Convert.ToString(System.DateTime.Now);
                    string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                    LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "UpdateADUser",
                                                  "0000000000", ex.Message, inner4, ex.StackTrace, psession_user, dateTime);

                    result = "Can't update AD user";
                    return result;
                }
            }
        }
        #endregion



        #endregion

    }
}
