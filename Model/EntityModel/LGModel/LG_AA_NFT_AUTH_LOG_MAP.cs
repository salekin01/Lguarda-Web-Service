using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.Metadata.Edm;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Model.EDMX;
using Model.EntityModel.Common;
using System.Configuration;

namespace Model.EntityModel.LGModel
{
    [DataContract]
    //public class LG_AA_NFT_AUTH_LOG_MAP : List<LG_AA_NFT_AUTH_LOG_VAL>
    public class LG_AA_NFT_AUTH_LOG_MAP
    {
        #region Properties

        [DataMember]
        [Required(ErrorMessage = "Log ID is required")]
        public Int64 LOG_ID { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Function ID is required")]
        public string FUNCTION_ID { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Table Name is required")]
        public string TABLE_NAME { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Table Primary Column Name is required")]
        public string TABLE_PK_COL_NM { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Table Primary Column Value is required")]
        public string TABLE_PK_COL_VAL { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Action Status is required")]
        public string ACTION_STATUS { get; set; }

        [DataMember]
        public string REMARKS { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Primary Table Flag is required")]
        public Int16 PRIMARY_TABLE_FLAG { get; set; }

        [DataMember]
        public Nullable<Int64> PARENT_TABLE_PK_VAL { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Auth Status ID is required")]
        public string AUTH_STATUS_ID { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Auth Level Max is required")]
        public Int16 AUTH_LEVEL_MAX { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Auth Level Pneding is required")]
        public Int16 AUTH_LEVEL_PENDING { get; set; }

        [DataMember]
        public string REASON_DECLINE { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Make By is required")]
        public string MAKE_BY { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Make Date is required")]
        public DateTime MAKE_DT { get; set; }

        [DataMember]
        public string ERROR { get; set; }

        public List<LG_AA_NFT_AUTH_LOG_DTLS_MAP> LG_AA_NFT_AUTH_LOG_DTLS_MAP;
        public List<LG_AA_NFT_AUTH_LOG_VAL_MAP> LG_AA_NFT_AUTH_LOG_VAL_MAP;
        //public List<LG_AA_NFT_AUTH_LOG_VAL> LG_AA_NFT_AUTH_LOG_VAL;

        #endregion Properties

        #region Methods
        #region Add
        public static string AddNftAuthLog(string FUNCTION_ID, string TABLE_NAME, string TABLE_PK_COL_NM,
            string TABLE_PK_COL_VAL, string OLD_VALUE, string NEW_VALUE, string ACTION_STATUS,
            string REMARKS, string PRIMARY_TABLE_FLAG, string PARENT_TABLE_PK_VAL, string AUTH_STATUS_ID,
            string AUTH_LEVEL_MAX, string AUTH_LEVEL_PENDING, string REASON_DECLINE, string MAKE_BY, string MAKE_DT)
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            string result = string.Empty;
            try
            {
                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                Int64 id = Convert.ToInt64(Obj_DBModelEntities.LG_AA_NFT_AUTH_LOG.DefaultIfEmpty().Max(p => p == null ? 0 : p.LOG_ID)) + 1;

                LG_AA_NFT_AUTH_LOG OBJ_LG_AA_NFT_AUTH_LOG = new LG_AA_NFT_AUTH_LOG();
                OBJ_LG_AA_NFT_AUTH_LOG.LOG_ID = id;
                OBJ_LG_AA_NFT_AUTH_LOG.FUNCTION_ID = FUNCTION_ID;
                OBJ_LG_AA_NFT_AUTH_LOG.TABLE_NAME = TABLE_NAME;
                OBJ_LG_AA_NFT_AUTH_LOG.TABLE_PK_COL_NM = TABLE_PK_COL_NM;
                OBJ_LG_AA_NFT_AUTH_LOG.TABLE_PK_COL_VAL = TABLE_PK_COL_VAL.ToString();
                OBJ_LG_AA_NFT_AUTH_LOG.ACTION_STATUS = ACTION_STATUS;
                OBJ_LG_AA_NFT_AUTH_LOG.REMARKS = REMARKS;
                OBJ_LG_AA_NFT_AUTH_LOG.PRIMARY_TABLE_FLAG = Convert.ToInt16(PRIMARY_TABLE_FLAG);
                OBJ_LG_AA_NFT_AUTH_LOG.PARENT_TABLE_PK_VAL = Convert.ToInt64(PARENT_TABLE_PK_VAL);
                OBJ_LG_AA_NFT_AUTH_LOG.AUTH_STATUS_ID = AUTH_STATUS_ID;
                OBJ_LG_AA_NFT_AUTH_LOG.AUTH_LEVEL_MAX = Convert.ToInt16(AUTH_LEVEL_MAX);
                OBJ_LG_AA_NFT_AUTH_LOG.AUTH_LEVEL_PENDING = Convert.ToInt16(AUTH_LEVEL_PENDING);
                OBJ_LG_AA_NFT_AUTH_LOG.REASON_DECLINE = REASON_DECLINE;
                OBJ_LG_AA_NFT_AUTH_LOG.MAKE_BY = MAKE_BY;
                OBJ_LG_AA_NFT_AUTH_LOG.MAKE_DT = Convert.ToDateTime(MAKE_DT);

                Obj_DBModelEntities.LG_AA_NFT_AUTH_LOG.Add(OBJ_LG_AA_NFT_AUTH_LOG);
                Obj_DBModelEntities.SaveChanges();
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
                        result = "Can't Add Authorization(Db).";
                    }
                }

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "AddNftAuthLog(string parameters)",
                                              "0000000000", dbEx.Message, inner, dbEx.StackTrace, MAKE_BY, dateTime);

                return result;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "AddNftAuthLog(string parameters)",
                                              "0000000000", ex.Message, inner4, ex.StackTrace, MAKE_BY, dateTime);

                result = "Can't Add Authorization.";
                return result;
            }
        }

        public static string AddNftAuthLog(Object anObject)
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            string result = string.Empty;
            string FUNCTION_ID = "";
            string MAKE_BY = "";
            try
            {
                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                LG_AA_NFT_AUTH_LOG OBJ_LG_AA_NFT_AUTH_LOG = new LG_AA_NFT_AUTH_LOG();
                Int64 id = Convert.ToInt64(Obj_DBModelEntities.LG_AA_NFT_AUTH_LOG.Max(x => x.LOG_ID)) + 1;
                OBJ_LG_AA_NFT_AUTH_LOG.LOG_ID = id;

                Type myType = anObject.GetType();
                IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());

                foreach (PropertyInfo prop in props)
                {
                    object propValue = prop.GetValue(anObject, null);

                    if (prop.Name == "ACTION_STATUS")
                    {
                        OBJ_LG_AA_NFT_AUTH_LOG.ACTION_STATUS = propValue.ToString();
                    }

                    if (prop.Name == "FUNCTION_ID")
                    {
                        FUNCTION_ID = OBJ_LG_AA_NFT_AUTH_LOG.FUNCTION_ID = propValue.ToString();
                    }
                    if (prop.Name == "TABLE_NAME")
                    {
                        OBJ_LG_AA_NFT_AUTH_LOG.TABLE_NAME = propValue.ToString();
                    }
                    if (prop.Name == "TABLE_PK_COL_NM")
                    {
                        OBJ_LG_AA_NFT_AUTH_LOG.TABLE_PK_COL_NM = propValue.ToString();
                    }
                    if (prop.Name == "TABLE_PK_COL_VAL")
                    {
                        OBJ_LG_AA_NFT_AUTH_LOG.TABLE_PK_COL_VAL = propValue.ToString();
                    }
                    if (prop.Name == "REMARKS")
                    {
                        OBJ_LG_AA_NFT_AUTH_LOG.REMARKS = propValue.ToString();
                    }
                    if (prop.Name == "PRIMARY_TABLE_FLAG")
                    {
                        OBJ_LG_AA_NFT_AUTH_LOG.PRIMARY_TABLE_FLAG = Convert.ToInt16(propValue);
                    }
                    if (prop.Name == "PRIMARY_TABLE_FLAG")
                    {
                        OBJ_LG_AA_NFT_AUTH_LOG.PRIMARY_TABLE_FLAG = Convert.ToInt16(propValue);
                    }
                    if (prop.Name == "PARENT_TABLE_PK_VAL")
                    {
                        OBJ_LG_AA_NFT_AUTH_LOG.PARENT_TABLE_PK_VAL = Convert.ToInt64(propValue);
                    }
                    if (prop.Name == "AUTH_STATUS_ID")
                    {
                        OBJ_LG_AA_NFT_AUTH_LOG.AUTH_STATUS_ID = propValue.ToString();
                    }
                    if (prop.Name == "AUTH_LEVEL_MAX")
                    {
                        OBJ_LG_AA_NFT_AUTH_LOG.AUTH_LEVEL_MAX = Convert.ToInt16(propValue);
                    }
                    if (prop.Name == "AUTH_LEVEL_PENDING")
                    {
                        OBJ_LG_AA_NFT_AUTH_LOG.AUTH_LEVEL_PENDING = Convert.ToInt16(propValue);
                    }
                    if (prop.Name == "REASON_DECLINE")
                    {
                        OBJ_LG_AA_NFT_AUTH_LOG.REASON_DECLINE = propValue.ToString();
                    }
                    if (prop.Name == "MAKE_BY")
                    {
                        MAKE_BY = OBJ_LG_AA_NFT_AUTH_LOG.MAKE_BY = propValue.ToString();
                    }
                    if (prop.Name == "MAKE_DT")
                    {
                        OBJ_LG_AA_NFT_AUTH_LOG.MAKE_DT = Convert.ToDateTime(propValue);
                    }
                }

                Obj_DBModelEntities.LG_AA_NFT_AUTH_LOG.Add(OBJ_LG_AA_NFT_AUTH_LOG);
                Obj_DBModelEntities.SaveChanges();
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
                        result = "Can't Add Authorization(Db).";
                    }
                }

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "AddNftAuthLog(one object)",
                                              "0000000000", dbEx.Message, inner, dbEx.StackTrace, MAKE_BY, dateTime);

                return result;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "AddNftAuthLog(one object)",
                                             "0000000000", ex.Message, inner4, ex.StackTrace, MAKE_BY, dateTime);

                result = "Can't Add Authorization.";
                return result;
            }
        }

        public static string AddNftAuthLog(Object anObject, LG_AA_NFT_AUTH_LOG_MAP OBJ_LG_AA_NFT_AUTH_LOG_MAP)
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            string result = string.Empty;
            string FUNCTION_ID = "";
            string MAKE_BY = "";
            try
            {
                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                Int64 id = Convert.ToInt64(Obj_DBModelEntities.LG_AA_NFT_AUTH_LOG.DefaultIfEmpty().Max(p => p == null ? 0 : p.LOG_ID)) + 1;

                LG_AA_NFT_AUTH_LOG OBJ_LG_AA_NFT_AUTH_LOG = new LG_AA_NFT_AUTH_LOG();
                OBJ_LG_AA_NFT_AUTH_LOG.LOG_ID = OBJ_LG_AA_NFT_AUTH_LOG_MAP.LOG_ID = id;
                FUNCTION_ID = OBJ_LG_AA_NFT_AUTH_LOG.FUNCTION_ID = OBJ_LG_AA_NFT_AUTH_LOG_MAP.FUNCTION_ID;
                OBJ_LG_AA_NFT_AUTH_LOG.TABLE_NAME = OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_NAME;
                OBJ_LG_AA_NFT_AUTH_LOG.TABLE_PK_COL_NM = OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_NM;
                OBJ_LG_AA_NFT_AUTH_LOG.TABLE_PK_COL_VAL = OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_VAL.ToString();
                OBJ_LG_AA_NFT_AUTH_LOG.ACTION_STATUS = OBJ_LG_AA_NFT_AUTH_LOG_MAP.ACTION_STATUS;
                OBJ_LG_AA_NFT_AUTH_LOG.REMARKS = OBJ_LG_AA_NFT_AUTH_LOG_MAP.REMARKS;
                OBJ_LG_AA_NFT_AUTH_LOG.PRIMARY_TABLE_FLAG = OBJ_LG_AA_NFT_AUTH_LOG_MAP.PRIMARY_TABLE_FLAG;
                OBJ_LG_AA_NFT_AUTH_LOG.PARENT_TABLE_PK_VAL = OBJ_LG_AA_NFT_AUTH_LOG_MAP.PARENT_TABLE_PK_VAL;
                OBJ_LG_AA_NFT_AUTH_LOG.AUTH_STATUS_ID = OBJ_LG_AA_NFT_AUTH_LOG_MAP.AUTH_STATUS_ID;
                OBJ_LG_AA_NFT_AUTH_LOG.AUTH_LEVEL_MAX = OBJ_LG_AA_NFT_AUTH_LOG_MAP.AUTH_LEVEL_MAX;
                OBJ_LG_AA_NFT_AUTH_LOG.AUTH_LEVEL_PENDING = OBJ_LG_AA_NFT_AUTH_LOG_MAP.AUTH_LEVEL_PENDING;
                OBJ_LG_AA_NFT_AUTH_LOG.REASON_DECLINE = OBJ_LG_AA_NFT_AUTH_LOG_MAP.REASON_DECLINE;
                MAKE_BY = OBJ_LG_AA_NFT_AUTH_LOG.MAKE_BY = OBJ_LG_AA_NFT_AUTH_LOG_MAP.MAKE_BY;
                OBJ_LG_AA_NFT_AUTH_LOG.MAKE_DT = OBJ_LG_AA_NFT_AUTH_LOG_MAP.MAKE_DT;

                PrepareAuthLogValues(anObject, OBJ_LG_AA_NFT_AUTH_LOG_MAP, OBJ_LG_AA_NFT_AUTH_LOG);

                Obj_DBModelEntities.LG_AA_NFT_AUTH_LOG.Add(OBJ_LG_AA_NFT_AUTH_LOG);
                Obj_DBModelEntities.SaveChanges();

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
                        result = "Can't Add Authorization(Db).";
                    }
                }


                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "AddNftAuthLog(two objects)",
                                               "0000000000", dbEx.Message, inner, dbEx.StackTrace, MAKE_BY, dateTime);


                return result;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "AddNftAuthLog(two objects)",
                                              "0000000000", ex.Message, inner4, ex.StackTrace, MAKE_BY, dateTime);

                result = "Can't Add Authorization.";
                return result;
            }
        }

        private static void PrepareAuthLogValues(object myObject, LG_AA_NFT_AUTH_LOG_MAP OBJ_LG_AA_NFT_AUTH_LOG_MAP, LG_AA_NFT_AUTH_LOG OBJ_LG_AA_NFT_AUTH_LOG)
        {
            try
            {
                DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
                Int64 maxId = Convert.ToInt64(Obj_DBModelEntities.LG_AA_NFT_AUTH_LOG_VAL.DefaultIfEmpty()
                                            .Max(p => p == null ? 0 : p.LOG_VAL_ID));

                Type myType = myObject.GetType();
                IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());

                foreach (PropertyInfo prop in props)
                {
                    if (prop.Name == "ERROR")
                        continue;
                    if (prop.Name == "LAST_UPDATE_DT")
                        continue;
                    if (prop.PropertyType.Name == "ICollection`1")
                        continue;
                    //if (prop.GetValue(myObject, null) == null)
                    if (prop.GetValue(myObject, null) == null || prop.GetValue(myObject, null) == "")
                        continue;


                    LG_AA_NFT_AUTH_LOG_VAL OBJ_LG_AA_NFT_AUTH_LOG_VAL = new LG_AA_NFT_AUTH_LOG_VAL();

                    object propValue = prop.GetValue(myObject, null);
                    OBJ_LG_AA_NFT_AUTH_LOG_VAL.LOG_ID = OBJ_LG_AA_NFT_AUTH_LOG_MAP.LOG_ID;
                    OBJ_LG_AA_NFT_AUTH_LOG_VAL.LOG_VAL_ID = ++maxId;
                    OBJ_LG_AA_NFT_AUTH_LOG_VAL.COLUMN_NAME = prop.Name;
                    OBJ_LG_AA_NFT_AUTH_LOG_VAL.NEW_VALUE = Convert.ToString(propValue);

                    OBJ_LG_AA_NFT_AUTH_LOG.LG_AA_NFT_AUTH_LOG_VAL.Add(OBJ_LG_AA_NFT_AUTH_LOG_VAL);
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
                        string result = "Can't Add Authorization(Db).";
                    }
                }

                LG_ERROR_LOG_MAP.Add_Error_Log(OBJ_LG_AA_NFT_AUTH_LOG_MAP.FUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "PrepareAuthLogValues(three objects)",
                                               "0000000000", dbEx.Message, inner, dbEx.StackTrace, OBJ_LG_AA_NFT_AUTH_LOG_MAP.MAKE_BY, dateTime);
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                LG_ERROR_LOG_MAP.Add_Error_Log(OBJ_LG_AA_NFT_AUTH_LOG_MAP.FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "PrepareAuthLogValues(three objects)",
                                              "0000000000", ex.Message, inner4, ex.StackTrace, OBJ_LG_AA_NFT_AUTH_LOG_MAP.MAKE_BY, dateTime);

                string result = "Can't Add Authorization.";
            }

        }

        public static string AddNftAuthLog(Object oldObject, Object newObject, LG_AA_NFT_AUTH_LOG_MAP OBJ_LG_AA_NFT_AUTH_LOG_MAP)
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            string result = string.Empty;
            string FUNCTION_ID = "";
            string MAKE_BY = "";
            try
            {
                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                Int64 id = Convert.ToInt64(Obj_DBModelEntities.LG_AA_NFT_AUTH_LOG.DefaultIfEmpty()
                                         .Max(p => p == null ? 0 : p.LOG_ID)) + 1;

                LG_AA_NFT_AUTH_LOG OBJ_LG_AA_NFT_AUTH_LOG = new LG_AA_NFT_AUTH_LOG();
                OBJ_LG_AA_NFT_AUTH_LOG.LOG_ID = OBJ_LG_AA_NFT_AUTH_LOG_MAP.LOG_ID = id;
                FUNCTION_ID = OBJ_LG_AA_NFT_AUTH_LOG.FUNCTION_ID = OBJ_LG_AA_NFT_AUTH_LOG_MAP.FUNCTION_ID;
                OBJ_LG_AA_NFT_AUTH_LOG.TABLE_NAME = OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_NAME;
                OBJ_LG_AA_NFT_AUTH_LOG.TABLE_PK_COL_NM = OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_NM;
                OBJ_LG_AA_NFT_AUTH_LOG.TABLE_PK_COL_VAL = OBJ_LG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_VAL.ToString();
                OBJ_LG_AA_NFT_AUTH_LOG.ACTION_STATUS = OBJ_LG_AA_NFT_AUTH_LOG_MAP.ACTION_STATUS;
                OBJ_LG_AA_NFT_AUTH_LOG.REMARKS = OBJ_LG_AA_NFT_AUTH_LOG_MAP.REMARKS;
                OBJ_LG_AA_NFT_AUTH_LOG.PRIMARY_TABLE_FLAG = OBJ_LG_AA_NFT_AUTH_LOG_MAP.PRIMARY_TABLE_FLAG;
                OBJ_LG_AA_NFT_AUTH_LOG.PARENT_TABLE_PK_VAL = OBJ_LG_AA_NFT_AUTH_LOG_MAP.PARENT_TABLE_PK_VAL;
                OBJ_LG_AA_NFT_AUTH_LOG.AUTH_STATUS_ID = OBJ_LG_AA_NFT_AUTH_LOG_MAP.AUTH_STATUS_ID;
                OBJ_LG_AA_NFT_AUTH_LOG.AUTH_LEVEL_MAX = OBJ_LG_AA_NFT_AUTH_LOG_MAP.AUTH_LEVEL_MAX;
                OBJ_LG_AA_NFT_AUTH_LOG.AUTH_LEVEL_PENDING = OBJ_LG_AA_NFT_AUTH_LOG_MAP.AUTH_LEVEL_PENDING;
                OBJ_LG_AA_NFT_AUTH_LOG.REASON_DECLINE = OBJ_LG_AA_NFT_AUTH_LOG_MAP.REASON_DECLINE;
                MAKE_BY = OBJ_LG_AA_NFT_AUTH_LOG.MAKE_BY = OBJ_LG_AA_NFT_AUTH_LOG_MAP.MAKE_BY;
                OBJ_LG_AA_NFT_AUTH_LOG.MAKE_DT = OBJ_LG_AA_NFT_AUTH_LOG_MAP.MAKE_DT;

                PrepareAuthLogValues(oldObject, newObject, OBJ_LG_AA_NFT_AUTH_LOG_MAP, OBJ_LG_AA_NFT_AUTH_LOG);

                Obj_DBModelEntities.LG_AA_NFT_AUTH_LOG.Add(OBJ_LG_AA_NFT_AUTH_LOG);
                Obj_DBModelEntities.SaveChanges();

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
                        result = "Can't Add Authorization(Db).";
                    }
                }
                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "AddNftAuthLog(three objects)",
                                             "0000000000", dbEx.Message, inner, dbEx.StackTrace, MAKE_BY, dateTime);
                return result;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "AddNftAuthLog(three objects)",
                                              "0000000000", ex.Message, inner4, ex.StackTrace, MAKE_BY, dateTime);

                result = "Can't Add Authorization ";
                return result;
            }
        }

        private static void PrepareAuthLogValues(object oldObject, object newObject, LG_AA_NFT_AUTH_LOG_MAP OBJ_LG_AA_NFT_AUTH_LOG_MAP, LG_AA_NFT_AUTH_LOG OBJ_LG_AA_NFT_AUTH_LOG)
        {
            string FUNCTION_ID = "";
            string MAKE_BY = "";

            try
            {
                FUNCTION_ID = OBJ_LG_AA_NFT_AUTH_LOG_MAP.FUNCTION_ID;
                MAKE_BY = OBJ_LG_AA_NFT_AUTH_LOG_MAP.MAKE_BY;

                DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
                Int64 maxId =
                        Convert.ToInt64(
                            Obj_DBModelEntities.LG_AA_NFT_AUTH_LOG_VAL.DefaultIfEmpty()
                                .Max(p => p == null ? 0 : p.LOG_VAL_ID));

                Type oldObjectType = oldObject.GetType();
                Type newObjectType = newObject.GetType();

                if (oldObjectType == newObjectType)
                {
                    IList<PropertyInfo> oldObjectProps = new List<PropertyInfo>(oldObjectType.GetProperties());
                    IList<PropertyInfo> newObjectProps = new List<PropertyInfo>(newObjectType.GetProperties());

                    foreach (PropertyInfo oldObjectProp in oldObjectProps)
                    {
                        if (oldObjectProp.PropertyType.Name == "ICollection`1")
                            continue;
                        foreach (PropertyInfo newObjectProp in newObjectProps)
                        {
                            if (newObjectProp.Name == oldObjectProp.Name)
                            {
                                object oldObjectPropValue = oldObjectProp.GetValue(oldObject, null);
                                object newObjectPropValue = newObjectProp.GetValue(newObject, null);
                                string displayName = string.Empty;
                                string oldVal = string.Empty;
                                string newVal = string.Empty;
                                if (oldObjectPropValue != null)
                                {
                                    oldVal = oldObjectPropValue.ToString();
                                }
                                //else
                                //{
                                //    oldVal = "";
                                //}
                                if (newObjectPropValue != null)
                                {
                                    newVal = newObjectPropValue.ToString();
                                }
                                //else
                                //{
                                //    newVal = "";
                                //}
                                if ((oldObjectPropValue == null && newObjectPropValue == null) || newObjectPropValue == null)
                                {

                                }
                                else
                                {
                                    if (oldVal != newVal)
                                    {
                                        /*IEnumerable<CustomAttributeData> attribute = newObjectProp.CustomAttributes;
                                          foreach (var item in attribute)
                                          {
                                              if (item.AttributeType.FullName == "System.ComponentModel.DataAnnotations.DisplayAttribute")
                                              {
                                                  IList<CustomAttributeNamedArgument> namedArguments = item.NamedArguments;
                                                  displayName = namedArguments[0].TypedValue.Value.ToString();
                                              }
                                          }*/

                                        LG_AA_NFT_AUTH_LOG_VAL OBJ_LG_AA_NFT_AUTH_LOG_VAL = new LG_AA_NFT_AUTH_LOG_VAL();

                                        OBJ_LG_AA_NFT_AUTH_LOG_VAL.LOG_ID = OBJ_LG_AA_NFT_AUTH_LOG_MAP.LOG_ID;
                                        OBJ_LG_AA_NFT_AUTH_LOG_VAL.LOG_VAL_ID = ++maxId;

                                        OBJ_LG_AA_NFT_AUTH_LOG_VAL.COLUMN_NAME = oldObjectProp.Name;
                                        OBJ_LG_AA_NFT_AUTH_LOG_VAL.OLD_VALUE = Convert.ToString(oldObjectPropValue);
                                        OBJ_LG_AA_NFT_AUTH_LOG_VAL.NEW_VALUE = Convert.ToString(newObjectPropValue);

                                        OBJ_LG_AA_NFT_AUTH_LOG.LG_AA_NFT_AUTH_LOG_VAL.Add(OBJ_LG_AA_NFT_AUTH_LOG_VAL);
                                        //Obj_nCoreEntities.SaveChanges();
                                    }
                                }

                            }
                        }
                    }

                    /* //Old
                     foreach (PropertyInfo oldObjectProp in oldObjectProps)
                      {
                          if (oldObjectProp.PropertyType.Name == "ICollection`1")
                              continue;
                          foreach (PropertyInfo newObjectProp in newObjectProps)
                          {
                              if (newObjectProp.Name == oldObjectProp.Name)
                              {
                                  object oldObjectPropValue = oldObjectProp.GetValue(oldObject, null);
                                  object newObjectPropValue = newObjectProp.GetValue(newObject, null);

                                  if (oldObjectPropValue.ToString() != newObjectPropValue.ToString())
                                  {
                                      LG_AA_NFT_AUTH_LOG_VAL OBJ_LG_AA_NFT_AUTH_LOG_VAL = new LG_AA_NFT_AUTH_LOG_VAL();

                                      OBJ_LG_AA_NFT_AUTH_LOG_VAL.LOG_ID = OBJ_LG_AA_NFT_AUTH_LOG_MAP.LOG_ID;
                                      OBJ_LG_AA_NFT_AUTH_LOG_VAL.LOG_VAL_ID = ++maxId;

                                      OBJ_LG_AA_NFT_AUTH_LOG_VAL.COLUMN_NAME = oldObjectProp.Name;
                                      OBJ_LG_AA_NFT_AUTH_LOG_VAL.OLD_VALUE = Convert.ToString(oldObjectPropValue);
                                      OBJ_LG_AA_NFT_AUTH_LOG_VAL.NEW_VALUE = Convert.ToString(newObjectPropValue);

                                      OBJ_LG_AA_NFT_AUTH_LOG.LG_AA_NFT_AUTH_LOG_VAL.Add(OBJ_LG_AA_NFT_AUTH_LOG_VAL);
                                  }
                              }
                          }
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
                        string result = "Can't Add Authorization(Db).";
                    }
                }
                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "PrepareAuthLogValues(four objects)",
                                             "0000000000", dbEx.Message, inner, dbEx.StackTrace, MAKE_BY, dateTime);
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                LG_ERROR_LOG_MAP.Add_Error_Log(FUNCTION_ID, ex.Source, "ERR_APP_TYPE", "PrepareAuthLogValues(four objects)",
                                              "0000000000", ex.Message, inner4, ex.StackTrace, MAKE_BY, dateTime);
            }
        }

        #endregion Add

        #region Update
        public static string UpdateNftAuthLogPending(string pLOG_ID)
        {
            Int64 LOG_ID = Convert.ToInt64(pLOG_ID);

            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            string result = string.Empty;
            try
            {
                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                LG_AA_NFT_AUTH_LOG OBJ_LG_AA_NFT_AUTH_LOG = Obj_DBModelEntities.LG_AA_NFT_AUTH_LOG
                                                           .Where(m => m.LOG_ID == LOG_ID).SingleOrDefault();

                OBJ_LG_AA_NFT_AUTH_LOG.AUTH_LEVEL_PENDING = Convert.ToInt16(OBJ_LG_AA_NFT_AUTH_LOG.AUTH_LEVEL_PENDING - 1);

                Obj_DBModelEntities.SaveChanges();
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
                        result = "Can't Update Authorization(Db).";
                    }
                }

                LG_ERROR_LOG_MAP.Add_Error_Log(null, dbEx.Source, "ERR_APP_TYPE", "UpdateNftAuthLogPending",
                                          "0000000000", dbEx.Message, inner, dbEx.StackTrace, null, dateTime);

                return result;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                LG_ERROR_LOG_MAP.Add_Error_Log(null, ex.Source, "ERR_APP_TYPE", "UpdateNftAuthLogPending",
                                              "0000000000", ex.Message, inner4, ex.StackTrace, null, dateTime);

                result = "Can't Update Authorization.";
                return result;
            }
        }
        public static string UpdateNftAuthLog(string pLOG_ID, string pAuthorizeStatus, string reasonDecline)
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            string result = string.Empty;
            try
            {
                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);
                Int64 LOG_ID = Convert.ToInt64(pLOG_ID);

                LG_AA_NFT_AUTH_LOG OBJ_LG_AA_NFT_AUTH_LOG = Obj_DBModelEntities.LG_AA_NFT_AUTH_LOG
                                                           .Where(m => m.LOG_ID == LOG_ID).SingleOrDefault();

                OBJ_LG_AA_NFT_AUTH_LOG.AUTH_STATUS_ID = pAuthorizeStatus;
                OBJ_LG_AA_NFT_AUTH_LOG.REASON_DECLINE = reasonDecline;
                if (pAuthorizeStatus == "D")
                {
                    OBJ_LG_AA_NFT_AUTH_LOG.AUTH_LEVEL_PENDING = 0;
                }
                else
                {
                    OBJ_LG_AA_NFT_AUTH_LOG.AUTH_LEVEL_PENDING =
                    Convert.ToInt16(OBJ_LG_AA_NFT_AUTH_LOG.AUTH_LEVEL_PENDING - 1);
                }

                Obj_DBModelEntities.SaveChanges();
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
                        result = "Can't Update Authorization(Db).";
                    }
                }

                LG_ERROR_LOG_MAP.Add_Error_Log("010106001", dbEx.Source, "ERR_APP_TYPE", "UpdateNftAuthLog",
                                              "0000000000", dbEx.Message, inner, dbEx.StackTrace, null, dateTime);

                return result;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                LG_ERROR_LOG_MAP.Add_Error_Log("010106001", ex.Source, "ERR_APP_TYPE", "UpdateNftAuthLog",
                                              "0000000000", ex.Message, inner4, ex.StackTrace, null, dateTime);

                result = "Can't Update Authorization.";
                return result;
            }
        }
        #endregion Update

        #region Fetch all
        public static IEnumerable<LG_AA_NFT_AUTH_LOG_MAP> GetNftAuthLogs()
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            List<LG_AA_NFT_AUTH_LOG_MAP> LIST_LG_AA_NFT_AUTH_LOG_MAP = null;
            LG_AA_NFT_AUTH_LOG_MAP OBJ_LG_AA_NFT_AUTH_LOG_MAP = new LG_AA_NFT_AUTH_LOG_MAP();

            try
            {
                Obj_DBModelEntities.Configuration.LazyLoadingEnabled = false;
                Obj_DBModelEntities.Configuration.ProxyCreationEnabled = false;

                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);


                LIST_LG_AA_NFT_AUTH_LOG_MAP = (from a in Obj_DBModelEntities.LG_AA_NFT_AUTH_LOG
                                               orderby a.LOG_ID descending
                                               select new LG_AA_NFT_AUTH_LOG_MAP
                                               {
                                                   LOG_ID = a.LOG_ID,
                                                   FUNCTION_ID = a.FUNCTION_ID,
                                                   TABLE_NAME = a.TABLE_NAME,
                                                   TABLE_PK_COL_NM = a.TABLE_PK_COL_NM,
                                                   TABLE_PK_COL_VAL = a.TABLE_PK_COL_VAL,
                                                   ACTION_STATUS = a.ACTION_STATUS,
                                                   REMARKS = a.REMARKS,
                                                   PRIMARY_TABLE_FLAG = a.PRIMARY_TABLE_FLAG,
                                                   PARENT_TABLE_PK_VAL = a.PARENT_TABLE_PK_VAL,
                                                   AUTH_STATUS_ID = a.AUTH_STATUS_ID,
                                                   AUTH_LEVEL_MAX = a.AUTH_LEVEL_MAX,
                                                   AUTH_LEVEL_PENDING = a.AUTH_LEVEL_PENDING,
                                                   REASON_DECLINE = a.REASON_DECLINE,
                                                   MAKE_BY = a.MAKE_BY,
                                                   MAKE_DT = a.MAKE_DT
                                               }).ToList();

                return LIST_LG_AA_NFT_AUTH_LOG_MAP;
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
                        OBJ_LG_AA_NFT_AUTH_LOG_MAP.ERROR = validationError.ErrorMessage;
                        LIST_LG_AA_NFT_AUTH_LOG_MAP.Add(OBJ_LG_AA_NFT_AUTH_LOG_MAP);
                    }
                }

                LG_ERROR_LOG_MAP.Add_Error_Log(null, dbEx.Source, "ERR_APP_TYPE", "GetNftAuthLogs",
                                              "0000000000", dbEx.Message, inner, dbEx.StackTrace, null, dateTime);

                return LIST_LG_AA_NFT_AUTH_LOG_MAP;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                LG_ERROR_LOG_MAP.Add_Error_Log(null, ex.Source, "ERR_APP_TYPE", "GetNftAuthLogs",
                                              "0000000000", ex.Message, inner4, ex.StackTrace, null, dateTime);

                OBJ_LG_AA_NFT_AUTH_LOG_MAP.ERROR = ex.Message;
                LIST_LG_AA_NFT_AUTH_LOG_MAP.Add(OBJ_LG_AA_NFT_AUTH_LOG_MAP);
                return LIST_LG_AA_NFT_AUTH_LOG_MAP;
            }
        }
        #endregion Fetch all

        #region Fetch Single
        public static IEnumerable<LG_AA_NFT_AUTH_LOG_MAP> GetNftAuthLogsByFunctionID(string fuctionID, string pMakeBy)
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            List<LG_AA_NFT_AUTH_LOG_MAP> LIST_LG_AA_NFT_AUTH_LOG_MAP = null;
            LG_AA_NFT_AUTH_LOG_MAP OBJ_LG_AA_NFT_AUTH_LOG_MAP = new LG_AA_NFT_AUTH_LOG_MAP();

            try
            {
                Obj_DBModelEntities.Configuration.LazyLoadingEnabled = false;
                Obj_DBModelEntities.Configuration.ProxyCreationEnabled = false;

                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);


                LIST_LG_AA_NFT_AUTH_LOG_MAP = (from a in Obj_DBModelEntities.LG_AA_NFT_AUTH_LOG
                                               where a.FUNCTION_ID == fuctionID && a.AUTH_STATUS_ID == "U" && a.MAKE_BY != pMakeBy
                                               orderby a.LOG_ID descending
                                               select new LG_AA_NFT_AUTH_LOG_MAP
                                               {
                                                   LOG_ID = a.LOG_ID,
                                                   FUNCTION_ID = a.FUNCTION_ID,
                                                   TABLE_NAME = a.TABLE_NAME,
                                                   TABLE_PK_COL_NM = a.TABLE_PK_COL_NM,
                                                   TABLE_PK_COL_VAL = a.TABLE_PK_COL_VAL,
                                                   ACTION_STATUS = a.ACTION_STATUS,
                                                   REMARKS = a.REMARKS,
                                                   PRIMARY_TABLE_FLAG = a.PRIMARY_TABLE_FLAG,
                                                   PARENT_TABLE_PK_VAL = a.PARENT_TABLE_PK_VAL,
                                                   AUTH_STATUS_ID = a.AUTH_STATUS_ID,
                                                   AUTH_LEVEL_MAX = a.AUTH_LEVEL_MAX,
                                                   AUTH_LEVEL_PENDING = a.AUTH_LEVEL_PENDING,
                                                   REASON_DECLINE = a.REASON_DECLINE,
                                                   MAKE_BY = a.MAKE_BY,
                                                   MAKE_DT = a.MAKE_DT
                                                   //,LG_AA_NFT_AUTH_LOG_VAL_MAP = TableToObjectList(a.LG_AA_NFT_AUTH_LOG_VAL.ToList())
                                                   //LG_AA_NFT_AUTH_LOG_VAL = a.LG_AA_NFT_AUTH_LOG_VAL.ToList()
                                               }).ToList();

                return LIST_LG_AA_NFT_AUTH_LOG_MAP;
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
                        OBJ_LG_AA_NFT_AUTH_LOG_MAP.ERROR = validationError.ErrorMessage;
                        LIST_LG_AA_NFT_AUTH_LOG_MAP.Add(OBJ_LG_AA_NFT_AUTH_LOG_MAP);
                    }
                }

                LG_ERROR_LOG_MAP.Add_Error_Log(null, dbEx.Source, "ERR_APP_TYPE", "GetNftAuthLogsByFunctionID",
                                    "0000000000", dbEx.Message, inner, dbEx.StackTrace, null, dateTime);

                return LIST_LG_AA_NFT_AUTH_LOG_MAP;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                LG_ERROR_LOG_MAP.Add_Error_Log(null, ex.Source, "ERR_APP_TYPE", "GetNftAuthLogsByFunctionID",
                                         "0000000000", ex.Message, inner4, ex.StackTrace, null, dateTime);

                OBJ_LG_AA_NFT_AUTH_LOG_MAP.ERROR = ex.Message;
                LIST_LG_AA_NFT_AUTH_LOG_MAP.Add(OBJ_LG_AA_NFT_AUTH_LOG_MAP);
                return LIST_LG_AA_NFT_AUTH_LOG_MAP;
            }
        }

        private static List<LG_AA_NFT_AUTH_LOG_VAL_MAP> TableToObjectList(List<LG_AA_NFT_AUTH_LOG_VAL> list)
        {
            List<LG_AA_NFT_AUTH_LOG_VAL_MAP> newMapList = new List<LG_AA_NFT_AUTH_LOG_VAL_MAP>();

            foreach (LG_AA_NFT_AUTH_LOG_VAL lgAaNftAuthLogVal in list)
            {
                LG_AA_NFT_AUTH_LOG_VAL_MAP lgAaNftAuthLogValMap = new LG_AA_NFT_AUTH_LOG_VAL_MAP();
                lgAaNftAuthLogValMap.LOG_ID = lgAaNftAuthLogVal.LOG_ID;
                lgAaNftAuthLogValMap.LOG_VAL_ID = lgAaNftAuthLogVal.LOG_VAL_ID;
                lgAaNftAuthLogValMap.COLUMN_NAME = lgAaNftAuthLogVal.COLUMN_NAME;
                lgAaNftAuthLogValMap.OLD_VALUE = lgAaNftAuthLogVal.OLD_VALUE;
                lgAaNftAuthLogValMap.NEW_VALUE = lgAaNftAuthLogVal.NEW_VALUE;
                newMapList.Add(lgAaNftAuthLogValMap);
            }
            return newMapList;
        }

        public static IEnumerable<LG_AA_NFT_AUTH_LOG_MAP> GetNftAuthLogsByLogID(string logID)
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            List<LG_AA_NFT_AUTH_LOG_MAP> LIST_LG_AA_NFT_AUTH_LOG_MAP = null;
            LG_AA_NFT_AUTH_LOG_MAP OBJ_LG_AA_NFT_AUTH_LOG_MAP = new LG_AA_NFT_AUTH_LOG_MAP();

            try
            {
                Obj_DBModelEntities.Configuration.LazyLoadingEnabled = false;
                Obj_DBModelEntities.Configuration.ProxyCreationEnabled = false;

                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);


                Int64 logId = Convert.ToInt64(logID);
                LIST_LG_AA_NFT_AUTH_LOG_MAP = (from a in Obj_DBModelEntities.LG_AA_NFT_AUTH_LOG
                                               where a.LOG_ID == logId
                                               orderby a.LOG_ID ascending
                                               select new LG_AA_NFT_AUTH_LOG_MAP
                                               {
                                                   LOG_ID = a.LOG_ID,
                                                   FUNCTION_ID = a.FUNCTION_ID,
                                                   TABLE_NAME = a.TABLE_NAME,
                                                   TABLE_PK_COL_NM = a.TABLE_PK_COL_NM,
                                                   TABLE_PK_COL_VAL = a.TABLE_PK_COL_VAL,
                                                   ACTION_STATUS = a.ACTION_STATUS,
                                                   REMARKS = a.REMARKS,
                                                   PRIMARY_TABLE_FLAG = a.PRIMARY_TABLE_FLAG,
                                                   PARENT_TABLE_PK_VAL = a.PARENT_TABLE_PK_VAL,
                                                   AUTH_STATUS_ID = a.AUTH_STATUS_ID,
                                                   AUTH_LEVEL_MAX = a.AUTH_LEVEL_MAX,
                                                   AUTH_LEVEL_PENDING = a.AUTH_LEVEL_PENDING,
                                                   REASON_DECLINE = a.REASON_DECLINE,
                                                   MAKE_BY = a.MAKE_BY,
                                                   MAKE_DT = a.MAKE_DT
                                               }).ToList();

                return LIST_LG_AA_NFT_AUTH_LOG_MAP;
            }

            catch (DbEntityValidationException dbEx)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner = "";

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName,
                            validationError.ErrorMessage);
                        inner = validationError.PropertyName + "::" + validationError.ErrorMessage + ";;" + inner;
                        OBJ_LG_AA_NFT_AUTH_LOG_MAP.ERROR = validationError.ErrorMessage;

                        LIST_LG_AA_NFT_AUTH_LOG_MAP.Add(OBJ_LG_AA_NFT_AUTH_LOG_MAP);
                    }
                }

                LG_ERROR_LOG_MAP.Add_Error_Log(null, dbEx.Source, "ERR_APP_TYPE", "GetNftAuthLogsByLogID",
                                              "0000000000", dbEx.Message, inner, dbEx.StackTrace, null, dateTime);

                return LIST_LG_AA_NFT_AUTH_LOG_MAP;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                LG_ERROR_LOG_MAP.Add_Error_Log(null, ex.Source, "ERR_APP_TYPE", "GetNftAuthLogsByLogID",
                                              "0000000000", ex.Message, inner4, ex.StackTrace, null, dateTime);

                OBJ_LG_AA_NFT_AUTH_LOG_MAP.ERROR = ex.Message;
                LIST_LG_AA_NFT_AUTH_LOG_MAP.Add(OBJ_LG_AA_NFT_AUTH_LOG_MAP);
                return LIST_LG_AA_NFT_AUTH_LOG_MAP;
            }
        }

        public static Int16 GetNftAuthLevelMaxFromAuthLog(string logID)
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            LG_AA_NFT_AUTH_LOG_MAP OBJ_LG_AA_NFT_AUTH_LOG_MAP = new LG_AA_NFT_AUTH_LOG_MAP();

            try
            {
                Obj_DBModelEntities.Configuration.LazyLoadingEnabled = false;
                Obj_DBModelEntities.Configuration.ProxyCreationEnabled = false;

                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);


                Int64 logId = Convert.ToInt64(logID);
                OBJ_LG_AA_NFT_AUTH_LOG_MAP = (from a in Obj_DBModelEntities.LG_AA_NFT_AUTH_LOG
                                              where a.LOG_ID == logId

                                              select new LG_AA_NFT_AUTH_LOG_MAP
                                              {
                                                  AUTH_LEVEL_MAX = a.AUTH_LEVEL_MAX
                                              }).Single();

                return OBJ_LG_AA_NFT_AUTH_LOG_MAP.AUTH_LEVEL_MAX;
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

                LG_ERROR_LOG_MAP.Add_Error_Log(null, dbEx.Source, "ERR_APP_TYPE", "GetNftAuthLevelMaxFromAuthLog",
                                              "0000000000", dbEx.Message, inner, dbEx.StackTrace, null, dateTime);

                return -2;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                LG_ERROR_LOG_MAP.Add_Error_Log(null, ex.Source, "ERR_APP_TYPE", "GetNftAuthLevelMaxFromAuthLog",
                                              "0000000000", ex.Message, inner4, ex.StackTrace, null, dateTime);

                return -1;
            }
        }
        public static int? GetNftAuthLevelMaxFromFunction(string functionId)
        {
            /*
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            LG_AA_NFT_AUTH_LOG_MAP OBJ_LG_AA_NFT_AUTH_LOG_MAP = new LG_AA_NFT_AUTH_LOG_MAP();

            try
            {
                Obj_DBModelEntities.Configuration.LazyLoadingEnabled = false;
                Obj_DBModelEntities.Configuration.ProxyCreationEnabled = false;

                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);


                Int64 id = Convert.ToInt64(functionId);
                OBJ_LG_AA_NFT_AUTH_LOG_MAP = (from a in Obj_DBModelEntities.LG_AA_NFT_AUTH_LOG
                                              where a.FUNCTION_ID == functionId

                                              select new LG_AA_NFT_AUTH_LOG_MAP
                                              {
                                                  AUTH_LEVEL_MAX = a.AUTH_LEVEL_MAX   
                                              }).Single();

                return OBJ_LG_AA_NFT_AUTH_LOG_MAP.AUTH_LEVEL_MAX;*/

            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            try
            {
                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                Int64 id = Convert.ToInt64(functionId);
                var AUTH_LEVEL_MAX = (from a in Obj_DBModelEntities.LG_FNR_FUNCTION
                                      where a.FUNCTION_ID == functionId
                                      select a.AUTH_LEVEL).SingleOrDefault();


                return AUTH_LEVEL_MAX;
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

                LG_ERROR_LOG_MAP.Add_Error_Log(null, dbEx.Source, "ERR_APP_TYPE", "GetNftAuthLevelMaxFromFunction",
                                             "0000000000", dbEx.Message, inner, dbEx.StackTrace, null, dateTime);

                return -2;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                LG_ERROR_LOG_MAP.Add_Error_Log(null, ex.Source, "ERR_APP_TYPE", "GetNftAuthLevelMaxFromFunction",
                                              "0000000000", ex.Message, inner4, ex.StackTrace, null, dateTime);

                return -1;
            }
        }

        #endregion Fetch Single

        #endregion Method


        internal static string UpdateNftAuthLogObjectTable(string pLOG_ID, string AUTH_STATUS_ID, string USER_ID, DBModelEntities Obj_DBModelEntities) //salekin changed 19.12.2017
        {
            //DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            string result = string.Empty;
            try
            {
                Int64 LOG_ID = Convert.ToInt64(pLOG_ID);

                LG_AA_NFT_AUTH_LOG OBJ_LG_AA_NFT_AUTH_LOG = Obj_DBModelEntities.LG_AA_NFT_AUTH_LOG
                                                           .Where(m => m.LOG_ID == LOG_ID).SingleOrDefault();
                TableSelectionConditions(AUTH_STATUS_ID, USER_ID, OBJ_LG_AA_NFT_AUTH_LOG, Obj_DBModelEntities);
                //Obj_DBModelEntities.SaveChanges();
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
                        result = "Can't Update Authorization process(Db).";
                    }
                }

                LG_ERROR_LOG_MAP.Add_Error_Log("010106001", dbEx.Source, "ERR_APP_TYPE", "UpdateNftAuthLogObjectTable",
                                              "0000000000", dbEx.Message, inner, dbEx.StackTrace, USER_ID, dateTime);

                return result;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                LG_ERROR_LOG_MAP.Add_Error_Log("010106001", ex.Source, "ERR_APP_TYPE", "UpdateNftAuthLogObjectTable",
                                              "0000000000", ex.Message, inner4, ex.StackTrace, USER_ID, dateTime);

                result = "Can't Update Authorization process.";
                return result;
            }
            return result;
        }

        private static void TableSelectionConditions(string AUTH_STATUS_ID, string USER_ID, LG_AA_NFT_AUTH_LOG OBJ_LG_AA_NFT_AUTH_LOG, DBModelEntities Obj_DBModelEntities)
        {
            string TABLE_PK_COL_VAL = OBJ_LG_AA_NFT_AUTH_LOG.TABLE_PK_COL_VAL.ToString();

            if (OBJ_LG_AA_NFT_AUTH_LOG.TABLE_NAME == "LG_FNR_APPLICATION")
            {
                LG_FNR_APPLICATION OBJ = Obj_DBModelEntities.LG_FNR_APPLICATION.SingleOrDefault(m => m.APPLICATION_ID == TABLE_PK_COL_VAL);
                SetTableObject(OBJ, AUTH_STATUS_ID, OBJ_LG_AA_NFT_AUTH_LOG, Obj_DBModelEntities);
            }
            if (OBJ_LG_AA_NFT_AUTH_LOG.TABLE_NAME == "LG_FNR_SERVICE")
            {
                string[] stringSeparators = new string[] { ";;" };
                string[] List_composit_primary_key_values;
                List_composit_primary_key_values = TABLE_PK_COL_VAL.Split(stringSeparators, StringSplitOptions.None);
                string application_id = List_composit_primary_key_values[0];
                string service_id = List_composit_primary_key_values[1];
                LG_FNR_SERVICE OBJ = Obj_DBModelEntities.LG_FNR_SERVICE.SingleOrDefault(m => m.APPLICATION_ID == application_id && m.SERVICE_ID == service_id);
                SetTableObject(OBJ, AUTH_STATUS_ID, OBJ_LG_AA_NFT_AUTH_LOG, Obj_DBModelEntities);
            }
            if (OBJ_LG_AA_NFT_AUTH_LOG.TABLE_NAME == "LG_FNR_MODULE")
            {
                string[] stringSeparators = new string[] { ";;" };
                string[] List_composit_primary_key_values;
                List_composit_primary_key_values = TABLE_PK_COL_VAL.Split(stringSeparators, StringSplitOptions.None);
                string application_id = List_composit_primary_key_values[0];
                string service_id = List_composit_primary_key_values[1];
                string module_id = List_composit_primary_key_values[2];
                LG_FNR_MODULE OBJ = Obj_DBModelEntities.LG_FNR_MODULE.SingleOrDefault(m => m.APPLICATION_ID == application_id && m.SERVICE_ID == service_id && m.MODULE_ID == module_id);
                SetTableObject(OBJ, AUTH_STATUS_ID, OBJ_LG_AA_NFT_AUTH_LOG, Obj_DBModelEntities);

                LG_MENU_MAP.BindingMenu(OBJ.APPLICATION_ID, OBJ.MODULE_NM, string.Empty, OBJ.MODULE_NM, string.Empty, string.Empty, string.Empty, null, null, 1, string.Empty, string.Empty, USER_ID, Obj_DBModelEntities, 1);
            }
            if (OBJ_LG_AA_NFT_AUTH_LOG.TABLE_NAME == "LG_FNR_FUNCTION")
            {
                LG_FNR_FUNCTION OBJ = Obj_DBModelEntities.LG_FNR_FUNCTION.SingleOrDefault(m => m.FUNCTION_ID == TABLE_PK_COL_VAL);
                SetTableObject(OBJ, AUTH_STATUS_ID, OBJ_LG_AA_NFT_AUTH_LOG, Obj_DBModelEntities);

                LG_MENU_MAP.BindingMenu(OBJ.APPLICATION_ID, OBJ.FUNCTION_NM, string.Empty, OBJ.FUNCTION_NM, "Index", string.Empty, OBJ.FUNCTION_ID, null, null, 1, OBJ.SERVICE_ID, OBJ.MODULE_ID, USER_ID, Obj_DBModelEntities, OBJ.ENABLED_FLAG);

                //if (OBJ.ENABLED_FLAG == 1)
                //{
                //    LG_MENU_MAP.AddMenu(OBJ.APPLICATION_ID, OBJ.FUNCTION_NM, string.Empty, OBJ.FUNCTION_NM, "Index", string.Empty, OBJ.FUNCTION_ID, null, null, 1, OBJ.SERVICE_ID, OBJ.MODULE_ID, USER_ID, Obj_DBModelEntities);
                //}
            }
            if (OBJ_LG_AA_NFT_AUTH_LOG.TABLE_NAME == "LG_FNR_ROLE")
            {
                LG_FNR_ROLE OBJ = Obj_DBModelEntities.LG_FNR_ROLE.SingleOrDefault(m => m.ROLE_ID == TABLE_PK_COL_VAL);
                SetTableObject(OBJ, AUTH_STATUS_ID, OBJ_LG_AA_NFT_AUTH_LOG, Obj_DBModelEntities);
            }
            if (OBJ_LG_AA_NFT_AUTH_LOG.TABLE_NAME == "LG_FNR_ROLE_DEFINE")
            {
                int TABLE_PK_COL_VAL_new = Convert.ToInt16(OBJ_LG_AA_NFT_AUTH_LOG.TABLE_PK_COL_VAL);
                LG_FNR_ROLE_DEFINE OBJ = Obj_DBModelEntities.LG_FNR_ROLE_DEFINE.SingleOrDefault(m => m.SL_NO == TABLE_PK_COL_VAL_new);
                SetTableObject(OBJ, AUTH_STATUS_ID, OBJ_LG_AA_NFT_AUTH_LOG, Obj_DBModelEntities);
            }
            if (OBJ_LG_AA_NFT_AUTH_LOG.TABLE_NAME == "LG_USER_SETUP_PROFILE")
            {
                LG_USER_SETUP_PROFILE OBJ = Obj_DBModelEntities.LG_USER_SETUP_PROFILE.SingleOrDefault(m => m.USER_ID == TABLE_PK_COL_VAL);
                SetTableObject(OBJ, AUTH_STATUS_ID, OBJ_LG_AA_NFT_AUTH_LOG, Obj_DBModelEntities);

                try
                {
                    string Pass = Security.GetPlainText(OBJ.PASSWORD);
                    string appId = ConfigurationManager.AppSettings["appID"].ToString();

                    if (OBJ_LG_AA_NFT_AUTH_LOG.ACTION_STATUS == "ADD" && OBJ_LG_AA_NFT_AUTH_LOG.AUTH_STATUS_ID == "A")
                    {
                        if (!string.IsNullOrEmpty(OBJ.USER_ID) && !string.IsNullOrEmpty(OBJ.MAIL_ADDRESS))
                        {
                            EntityModel.TwoFactorAuth.OTP.SendEmailAfterUserCreate(OBJ.USER_ID, OBJ.MAIL_ADDRESS, Pass.ToString(), appId);
                        }
                    }
                    else if (OBJ_LG_AA_NFT_AUTH_LOG.ACTION_STATUS == "EDT" && OBJ_LG_AA_NFT_AUTH_LOG.AUTH_STATUS_ID == "A")
                    {
                        string FUNCTION_ID = Obj_DBModelEntities.LG_FNR_FUNCTION.Where(x => x.FUNCTION_NM == "PasswordReset").Select(x => x.FUNCTION_ID).SingleOrDefault();

                        if (!string.IsNullOrEmpty(FUNCTION_ID) && FUNCTION_ID == OBJ_LG_AA_NFT_AUTH_LOG.FUNCTION_ID)
                        {
                            EntityModel.TwoFactorAuth.OTP.SendEmailAfterPasswordChange(OBJ.USER_ID, OBJ.MAIL_ADDRESS, Pass.ToString(), appId);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            if (OBJ_LG_AA_NFT_AUTH_LOG.TABLE_NAME == "LG_USER_ROLE_ASSIGN")
            {
                int TABLE_PK_COL_VAL_new = Convert.ToInt16(OBJ_LG_AA_NFT_AUTH_LOG.TABLE_PK_COL_VAL);
                LG_USER_ROLE_ASSIGN OBJ = Obj_DBModelEntities.LG_USER_ROLE_ASSIGN.SingleOrDefault(m => m.SL_NO == TABLE_PK_COL_VAL_new);

                if (AUTH_STATUS_ID == "D")
                {
                    if (OBJ.LAST_ACTION == "ADD")
                    {
                        Obj_DBModelEntities.LG_USER_ROLE_ASSIGN.Remove(OBJ);
                    }
                    else if (OBJ.LAST_ACTION == "EDT")
                    {
                        SetTableObjectCommonProperty(OBJ, "A");
                    }
                    else if (OBJ.LAST_ACTION == "DEL")
                    {
                        SetTableObjectCommonProperty(OBJ, "A");
                        OBJ.GetType().GetProperty("LAST_ACTION").SetValue(OBJ, "EDT", null);
                    }
                }
                else
                {
                    if (OBJ.LAST_ACTION == "ADD")
                    {
                        SetTableObjectCommonProperty(OBJ, "A");
                    }
                    else if (OBJ.LAST_ACTION == "EDT")
                    {
                        SetTableObjectProperty(OBJ, OBJ_LG_AA_NFT_AUTH_LOG.LOG_ID, Obj_DBModelEntities);
                        SetTableObjectCommonProperty(OBJ, "A");
                    }
                    else if (OBJ.LAST_ACTION == "DEL")
                    {
                        SetTableObjectCommonProperty(OBJ, "D");
                    }
                }
                //commented by Shohan for declined role assign 
                //SetTableObject(OBJ, AUTH_STATUS_ID, OBJ_LG_AA_NFT_AUTH_LOG, Obj_DBModelEntities);
            }
            if (OBJ_LG_AA_NFT_AUTH_LOG.TABLE_NAME == "LG_CRD_PASSWORD_POLICY")
            {
                LG_CRD_PASSWORD_POLICY OBJ = Obj_DBModelEntities.LG_CRD_PASSWORD_POLICY.SingleOrDefault(m => m.SL_NO == TABLE_PK_COL_VAL);
                SetTableObject(OBJ, AUTH_STATUS_ID, OBJ_LG_AA_NFT_AUTH_LOG, Obj_DBModelEntities);
            }
            if (OBJ_LG_AA_NFT_AUTH_LOG.TABLE_NAME == "LG_2FA_OTP_CONFIG")
            {
                LG_2FA_OTP_CONFIG OBJ = Obj_DBModelEntities.LG_2FA_OTP_CONFIG.SingleOrDefault(m => m.OTP_ID == TABLE_PK_COL_VAL);
                SetTableObject(OBJ, AUTH_STATUS_ID, OBJ_LG_AA_NFT_AUTH_LOG, Obj_DBModelEntities);
            }
            if (OBJ_LG_AA_NFT_AUTH_LOG.TABLE_NAME == "LG_SYS_MAIL_SERVER_CONFIG")
            {
                LG_SYS_MAIL_SERVER_CONFIG OBJ = Obj_DBModelEntities.LG_SYS_MAIL_SERVER_CONFIG.SingleOrDefault(m => m.MAIL_ID == TABLE_PK_COL_VAL);
                SetTableObject(OBJ, AUTH_STATUS_ID, OBJ_LG_AA_NFT_AUTH_LOG, Obj_DBModelEntities);
            }
            if (OBJ_LG_AA_NFT_AUTH_LOG.TABLE_NAME == "LG_USER_AD_BINDING")
            {
                int sl_no = Convert.ToInt32(TABLE_PK_COL_VAL);
                LG_USER_AD_BINDING OBJ = Obj_DBModelEntities.LG_USER_AD_BINDING.SingleOrDefault(m => m.SL == sl_no);
                SetTableObject(OBJ, AUTH_STATUS_ID, OBJ_LG_AA_NFT_AUTH_LOG, Obj_DBModelEntities);
            }



            /*
            if (OBJ_LG_AA_NFT_AUTH_LOG.TABLE_NAME == "LG_AA_AUTHENTICATION_TYPE")
            {
                LG_AA_AUTHENTICATION_TYPE OBJ = Obj_DBModelEntities.LG_AA_AUTHENTICATION_TYPE.SingleOrDefault(m => m.AUTHENTICATION_ID == TABLE_PK_COL_VAL);
                OBJ.AUTH_STATUS_ID = AUTH_STATUS_ID;
                OBJ.LAST_UPDATE_DT = DateTime.Now;
            }
            if (OBJ_LG_AA_NFT_AUTH_LOG.TABLE_NAME == "LG_FNR_ROLE_PERMISSION")
            {
                LG_FNR_ROLE_PERMISSION OBJ = Obj_DBModelEntities.LG_FNR_ROLE_PERMISSION.SingleOrDefault(m => m.PERMISSION_ID == TABLE_PK_COL_VAL);
                OBJ.AUTH_STATUS_ID = AUTH_STATUS_ID;
                OBJ.LAST_UPDATE_DT = DateTime.Now;
            }
            if (OBJ_LG_AA_NFT_AUTH_LOG.TABLE_NAME == "LG_FNR_ROLE_PERMISSION_DETAILS")
            {
                LG_FNR_ROLE_PERMISSION_DETAILS OBJ = Obj_DBModelEntities.LG_FNR_ROLE_PERMISSION_DETAILS.SingleOrDefault(m => m.PERMISSION_ID == TABLE_PK_COL_VAL);
                OBJ.AUTH_STATUS_ID = AUTH_STATUS_ID;
                OBJ.LAST_UPDATE_DT = DateTime.Now;
            }
            if (OBJ_LG_AA_NFT_AUTH_LOG.TABLE_NAME == "LG_SYS_BRANCH_HOME_BANK")
            {
                LG_SYS_BRANCH_HOME_BANK OBJ = Obj_DBModelEntities.LG_SYS_BRANCH_HOME_BANK.SingleOrDefault(m => m.BRANCH_ID == TABLE_PK_COL_VAL);
                OBJ.AUTH_STATUS_ID = AUTH_STATUS_ID;
                OBJ.LAST_UPDATE_DT = DateTime.Now;
            }
            if (OBJ_LG_AA_NFT_AUTH_LOG.TABLE_NAME == "LG_USER_AREA")
            {
                LG_USER_AREA OBJ = Obj_DBModelEntities.LG_USER_AREA.SingleOrDefault(m => m.AREA_ID == TABLE_PK_COL_VAL);
                OBJ.AUTH_STATUS_ID = AUTH_STATUS_ID;
                OBJ.LAST_UPDATE_DT = DateTime.Now;
            }
            if (OBJ_LG_AA_NFT_AUTH_LOG.TABLE_NAME == "LG_USER_CLASSIFACTION")
            {
                LG_USER_CLASSIFACTION OBJ = Obj_DBModelEntities.LG_USER_CLASSIFACTION.SingleOrDefault(m => m.CLASSIFICATION_ID == TABLE_PK_COL_VAL);
                OBJ.AUTH_STATUS_ID = AUTH_STATUS_ID;
                OBJ.LAST_UPDATE_DT = DateTime.Now;
            }
            if (OBJ_LG_AA_NFT_AUTH_LOG.TABLE_NAME == "LG_2FA_OTP_SMS_MSG")
            {
                LG_2FA_OTP_SMS_MSG OBJ = Obj_DBModelEntities.LG_2FA_OTP_SMS_MSG.SingleOrDefault(m => m.SMS_ID == Convert.ToDecimal(OBJ_LG_AA_NFT_AUTH_LOG.TABLE_PK_COL_VAL));
                OBJ.AUTH_STATUS_ID = AUTH_STATUS_ID;
                OBJ.LAST_UPDATE_DT = DateTime.Now;
            }
            if (OBJ_LG_AA_NFT_AUTH_LOG.TABLE_NAME == "LG_SYS_CLD_TYPE")
            {
                int TABLE_PK_COL_VAL_new = Convert.ToInt16(OBJ_LG_AA_NFT_AUTH_LOG.TABLE_PK_COL_VAL);
                LG_SYS_CLD_TYPE OBJ = Obj_DBModelEntities.LG_SYS_CLD_TYPE.SingleOrDefault(m => m.CLD_TYPE_ID == (short)TABLE_PK_COL_VAL_new);
                OBJ.AUTH_STATUS_ID = AUTH_STATUS_ID;
                OBJ.LAST_UPDATE_DT = DateTime.Now;
            }
            if (OBJ_LG_AA_NFT_AUTH_LOG.TABLE_NAME == "LG_SYS_CLD_HOLIDAY_TYPE")
            {
                int TABLE_PK_COL_VAL_new = Convert.ToInt16(OBJ_LG_AA_NFT_AUTH_LOG.TABLE_PK_COL_VAL);
                LG_SYS_CLD_HOLIDAY_TYPE OBJ = Obj_DBModelEntities.LG_SYS_CLD_HOLIDAY_TYPE.SingleOrDefault(m => m.HOLIDAY_TYPE_ID == (short)TABLE_PK_COL_VAL_new);
                OBJ.AUTH_STATUS_ID = AUTH_STATUS_ID;
                OBJ.LAST_UPDATE_DT = DateTime.Now;
            }*/
        }

        public static void SetTableObject<TObject>(TObject OBJ, string AUTH_STATUS_ID, LG_AA_NFT_AUTH_LOG OBJ_LG_AA_NFT_AUTH_LOG, DBModelEntities Obj_DBModelEntities) //salekin added 10.01.2017
        {
            if (AUTH_STATUS_ID == "D")
            {
                if (OBJ_LG_AA_NFT_AUTH_LOG.ACTION_STATUS == "ADD")
                {
                    SetTableObjectCommonProperty(OBJ, "D");
                }
                else if (OBJ_LG_AA_NFT_AUTH_LOG.ACTION_STATUS == "EDT")
                {
                    SetTableObjectCommonProperty(OBJ, "A");
                }
                else if (OBJ_LG_AA_NFT_AUTH_LOG.ACTION_STATUS == "DEL")
                {
                    SetTableObjectCommonProperty(OBJ, "A");
                    OBJ.GetType().GetProperty("LAST_ACTION").SetValue(OBJ, "EDT", null);
                }
            }
            else
            {
                if (OBJ_LG_AA_NFT_AUTH_LOG.ACTION_STATUS == "ADD")
                {
                    SetTableObjectCommonProperty(OBJ, "A");
                }
                else if (OBJ_LG_AA_NFT_AUTH_LOG.ACTION_STATUS == "EDT")
                {
                    SetTableObjectProperty(OBJ, OBJ_LG_AA_NFT_AUTH_LOG.LOG_ID, Obj_DBModelEntities);
                    SetTableObjectCommonProperty(OBJ, "A");
                }
                else if (OBJ_LG_AA_NFT_AUTH_LOG.ACTION_STATUS == "DEL")
                {
                    SetTableObjectCommonProperty(OBJ, "D");
                }
            }

            #region Old Process
            /* 
            if (AUTH_STATUS_ID == "D")
            {
                if (OBJ_LG_AA_NFT_AUTH_LOG.ACTION_STATUS == "ADD")
                {
                    OBJ.AUTH_STATUS_ID = "D";
                    OBJ.LAST_UPDATE_DT = DateTime.Now;
                    //Obj_DBModelEntities.LG_FNR_APPLICATION.Remove(OBJ);
                    //Add in Delete table.
                }
                else if (OBJ_LG_AA_NFT_AUTH_LOG.ACTION_STATUS == "EDT")
                {
                    OBJ.AUTH_STATUS_ID = "A";
                    OBJ.LAST_UPDATE_DT = DateTime.Now;
                }
                else if (OBJ_LG_AA_NFT_AUTH_LOG.ACTION_STATUS == "DEL")
                {
                    OBJ.AUTH_STATUS_ID = "A";
                    OBJ.LAST_UPDATE_DT = DateTime.Now;
                }
            }
            else
            {
                if (OBJ_LG_AA_NFT_AUTH_LOG.ACTION_STATUS == "ADD")
                {
                    OBJ.AUTH_STATUS_ID = AUTH_STATUS_ID;
                    OBJ.LAST_UPDATE_DT = DateTime.Now;
                }
                else if (OBJ_LG_AA_NFT_AUTH_LOG.ACTION_STATUS == "EDT")
                {
                    SetTableObjectProperty(OBJ, OBJ_LG_AA_NFT_AUTH_LOG.LOG_ID, Obj_DBModelEntities);

                    OBJ.AUTH_STATUS_ID = AUTH_STATUS_ID;
                    OBJ.LAST_UPDATE_DT = DateTime.Now;
                }
                else if (OBJ_LG_AA_NFT_AUTH_LOG.ACTION_STATUS == "DEL")
                {
                    OBJ.AUTH_STATUS_ID = "D";
                    OBJ.LAST_UPDATE_DT = DateTime.Now;
                    //Obj_DBModelEntities.LG_FNR_APPLICATION.Remove(OBJ);
                    //Add in Delete table.
                }
            }*/
            #endregion
        }
        public static TObject SetTableObjectProperty<TObject>(TObject OBJ, long LOG_ID, DBModelEntities Obj_DBModelEntities) //salekin added 10.01.2017
        {
            IEnumerable<LG_AA_NFT_AUTH_LOG_VAL> LIST_LOG_ITEM = Obj_DBModelEntities.LG_AA_NFT_AUTH_LOG_VAL
                                                               .Where(m => m.LOG_ID == LOG_ID).ToList();
            //Type ObjectType = OBJ.GetType();
            //IList<PropertyInfo> ObjectProps = new List<PropertyInfo>(ObjectType.GetProperties());

            foreach (var log_item in LIST_LOG_ITEM)
            {
                if (OBJ.GetType().GetProperty(log_item.COLUMN_NAME) != null) //true if the property exists.
                {
                    Type ObjectPropertyType = OBJ.GetType().GetProperty(log_item.COLUMN_NAME).PropertyType;
                    if (ObjectPropertyType.IsGenericType && ObjectPropertyType.GetGenericTypeDefinition() == typeof(Nullable<>)) //when property type is nullable
                    {
                        if (ObjectPropertyType.Module.ScopeName == "CommonLanguageRuntimeLibrary" && ObjectPropertyType.Name != "List`1" && ObjectPropertyType.Name != "IList`1" && ObjectPropertyType.Name != "ICollection`1" && ObjectPropertyType.Name != "IEnumerable`1") //check if property type is a class type || is a List type
                        {
                            OBJ.GetType().GetProperty(log_item.COLUMN_NAME).SetValue(OBJ, Convert.ChangeType(log_item.NEW_VALUE, ObjectPropertyType.GetGenericArguments()[0]), null);
                        }
                    }
                    else
                    {
                        if (ObjectPropertyType.Module.ScopeName == "CommonLanguageRuntimeLibrary" && ObjectPropertyType.Name != "List`1" && ObjectPropertyType.Name != "IList`1" && ObjectPropertyType.Name != "ICollection`1" && ObjectPropertyType.Name != "IEnumerable`1") //check if property type is a class type || is a List type
                        {
                            OBJ.GetType().GetProperty(log_item.COLUMN_NAME).SetValue(OBJ, Convert.ChangeType(log_item.NEW_VALUE, ObjectPropertyType), null);
                        }
                    }
                }
            }
            return OBJ;
        }
        public static TObject SetTableObjectCommonProperty<TObject>(TObject OBJ, string ASSIGNED_AUTH_STATUS_ID) //salekin added 10.01.2017
        {
            if (OBJ.GetType().GetProperty("AUTH_STATUS_ID") != null)
                OBJ.GetType().GetProperty("AUTH_STATUS_ID").SetValue(OBJ, ASSIGNED_AUTH_STATUS_ID, null);
            if (OBJ.GetType().GetProperty("LAST_UPDATE_DT") != null)
                OBJ.GetType().GetProperty("LAST_UPDATE_DT").SetValue(OBJ, DateTime.Now, null);
            return OBJ;
        }
    }
}