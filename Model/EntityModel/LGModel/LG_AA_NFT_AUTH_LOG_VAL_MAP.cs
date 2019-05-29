using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using Model.EDMX;
using Model.EntityModel.Common;
using System.Configuration;


namespace Model.EntityModel.LGModel
{
    public class LG_AA_NFT_AUTH_LOG_VAL_MAP
    {
        #region Properties
        [DataMember]
        [Required(ErrorMessage = "Log Value ID is required")]
        public Int64 LOG_VAL_ID { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Log ID is required")]
        public Int64 LOG_ID { get; set; }
        
        [DataMember]
        [Required(ErrorMessage = "Column Name is required")]
        public string COLUMN_NAME { get; set; }

        [DataMember]
        public string OLD_VALUE { get; set; }

        [DataMember]
        [Required(ErrorMessage = "New Value is required")]
        public string NEW_VALUE { get; set; }

        [DataMember]
        public string ERROR { get; set; }
        #endregion Properties

        #region Fetch All
        public static List<LG_AA_NFT_AUTH_LOG_VAL_MAP> GetNftAuthLogValByLogID(string pLOG_ID, string pUSER_ID, string pFUNCTION_ID)
        {
            Int64 logId = Convert.ToInt64(pLOG_ID);
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            List<LG_AA_NFT_AUTH_LOG_VAL_MAP> LIST_LG_AA_NFT_AUTH_LOG_VAL_MAP = null;
            LG_AA_NFT_AUTH_LOG_VAL_MAP OBJ_LG_AA_NFT_AUTH_LOG_VAL_MAP = new LG_AA_NFT_AUTH_LOG_VAL_MAP();

            try
            {
                Obj_DBModelEntities.Configuration.LazyLoadingEnabled = false;
                Obj_DBModelEntities.Configuration.ProxyCreationEnabled = false;

                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                LIST_LG_AA_NFT_AUTH_LOG_VAL_MAP = (from a in Obj_DBModelEntities.LG_AA_NFT_AUTH_LOG_VAL
                                                    where a.LOG_ID == logId
                                                    orderby a.LOG_VAL_ID ascending
                                                    select new LG_AA_NFT_AUTH_LOG_VAL_MAP
                                                    {
                                                        LOG_ID = a.LOG_ID,
                                                        LOG_VAL_ID = a.LOG_VAL_ID,
                                                        COLUMN_NAME = a.COLUMN_NAME,
                                                        OLD_VALUE = a.OLD_VALUE,
                                                        NEW_VALUE = a.NEW_VALUE
                                                    }).ToList();

                return LIST_LG_AA_NFT_AUTH_LOG_VAL_MAP;
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
                        OBJ_LG_AA_NFT_AUTH_LOG_VAL_MAP.ERROR = validationError.ErrorMessage;
                        LIST_LG_AA_NFT_AUTH_LOG_VAL_MAP.Add(OBJ_LG_AA_NFT_AUTH_LOG_VAL_MAP);
                    }
                }

                LG_ERROR_LOG_MAP.Add_Error_Log(pFUNCTION_ID, dbEx.Source, "ERR_APP_TYPE", "GetNftAuthLogValByLogID",
                                              "0000000000", dbEx.Message, inner, dbEx.StackTrace, pUSER_ID, dateTime);

                return LIST_LG_AA_NFT_AUTH_LOG_VAL_MAP;
            }
            catch (Exception ex)
            {
                string dateTime = Convert.ToString(System.DateTime.Now);
                string inner4 = ExceptionExtendedMethods.GetInnerExceptions(ex);

                LG_ERROR_LOG_MAP.Add_Error_Log(pFUNCTION_ID, ex.Source, "ERR_APP_TYPE", "GetNftAuthLogValByLogID",
                                               "0000000000", ex.Message, inner4, ex.StackTrace, pUSER_ID, dateTime);

                OBJ_LG_AA_NFT_AUTH_LOG_VAL_MAP.ERROR = ex.Message;
                LIST_LG_AA_NFT_AUTH_LOG_VAL_MAP.Add(OBJ_LG_AA_NFT_AUTH_LOG_VAL_MAP);
                return LIST_LG_AA_NFT_AUTH_LOG_VAL_MAP;
            }
        }
        #endregion Fetch All
    }
}
