using Model.EDMX;
using Model.EntityModel.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.EntityModel.LGModel
{
    public class LG_ERROR_LOG_MAP
    {

        #region Properties

        public int SL { get; set; }

        public string FUNCTION_ID { get; set; }

        public string ERR_SOURCE { get; set; }  

        public string ERR_APP_TYPE { get; set; }

        public string ERR_METHOD { get; set; }

        public string ERR_CODE { get; set; }

        public string MESSEGE { get; set; }

        public string PREVIEW_MESSEGE { get; set; }

        public string STACK_TRACE { get; set; }

        public string MAKE_BY { get; set; }

        public DateTime? MAKE_DT { get; set; }

        #endregion

        #region Events

        #region Add 
        public static string Add_Error_Log(string FUNCTION_ID, string ERR_SOURCE, string ERR_APP_TYPE, string ERR_METHOD, string ERR_CODE, string MESSEGE, string PREVIEW_MESSEGE, string STACK_TRACE, string MAKE_BY, string MAKE_DT)
        {
            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);

            try
            {
                string format = OutgoingResponseFormat.GetFormat();
                OutgoingResponseFormat.SetResponseFormat(format);

                int id = (Obj_DBModelEntities.LG_ERROR_LOG.Select(i => i.SL).Cast<int?>().Max() ?? 0) + 1;

                LG_ERROR_LOG_MAP pLG_ERROR_LOG_MAP = new LG_ERROR_LOG_MAP();
                pLG_ERROR_LOG_MAP.SL = Convert.ToInt32(id);
                if (String.IsNullOrWhiteSpace(FUNCTION_ID))
                {
                    pLG_ERROR_LOG_MAP.FUNCTION_ID = null;
                }
                else
                {
                    pLG_ERROR_LOG_MAP.FUNCTION_ID = FUNCTION_ID;
                }
                if (String.IsNullOrWhiteSpace(ERR_SOURCE))
                {
                    pLG_ERROR_LOG_MAP.ERR_SOURCE = null;
                }
                else
                {
                    pLG_ERROR_LOG_MAP.ERR_SOURCE = ERR_SOURCE;
                }
                if (String.IsNullOrWhiteSpace(ERR_APP_TYPE))
                {
                    pLG_ERROR_LOG_MAP.ERR_APP_TYPE = null;
                }
                else
                {
                    pLG_ERROR_LOG_MAP.ERR_APP_TYPE = ERR_APP_TYPE;
                }
                if (String.IsNullOrWhiteSpace(ERR_METHOD))
                {
                    pLG_ERROR_LOG_MAP.ERR_METHOD = null;
                }
                else
                {
                    pLG_ERROR_LOG_MAP.ERR_METHOD = ERR_METHOD;
                }
                if (String.IsNullOrWhiteSpace(ERR_CODE))
                {
                    pLG_ERROR_LOG_MAP.ERR_CODE = null;
                }
                else
                {
                    pLG_ERROR_LOG_MAP.ERR_CODE = ERR_CODE;
                }
                if (String.IsNullOrWhiteSpace(MESSEGE))
                {
                    pLG_ERROR_LOG_MAP.MESSEGE = null;
                }
                else
                {
                    pLG_ERROR_LOG_MAP.MESSEGE = MESSEGE;
                }
                if (String.IsNullOrWhiteSpace(PREVIEW_MESSEGE))
                {
                    pLG_ERROR_LOG_MAP.PREVIEW_MESSEGE = null;
                }
                else
                {
                    pLG_ERROR_LOG_MAP.PREVIEW_MESSEGE = PREVIEW_MESSEGE;
                }
                if (String.IsNullOrWhiteSpace(STACK_TRACE))
                {
                    pLG_ERROR_LOG_MAP.STACK_TRACE = null;
                }
                else
                {
                    pLG_ERROR_LOG_MAP.STACK_TRACE = STACK_TRACE;
                }
                if (String.IsNullOrWhiteSpace(MAKE_BY))
                {
                    pLG_ERROR_LOG_MAP.MAKE_BY = null;
                }
                else
                {
                    pLG_ERROR_LOG_MAP.MAKE_BY = MAKE_BY;
                }
                if (String.IsNullOrWhiteSpace(MAKE_DT))
                {
                    pLG_ERROR_LOG_MAP.MAKE_DT = null;
                }
                else
                {
                    pLG_ERROR_LOG_MAP.MAKE_DT = Convert.ToDateTime(MAKE_DT);
                }
                LG_ERROR_LOG OBJ_LG_ERROR_LOG = new LG_ERROR_LOG();

                OBJ_LG_ERROR_LOG.SL = (short)id;
                OBJ_LG_ERROR_LOG.FUNCTION_ID = FUNCTION_ID;
                OBJ_LG_ERROR_LOG.ERR_SOURCE = ERR_SOURCE;
                OBJ_LG_ERROR_LOG.ERR_CODE = ERR_CODE;
                OBJ_LG_ERROR_LOG.ERR_METHOD = ERR_METHOD;
                OBJ_LG_ERROR_LOG.ERR_APP_TYPE = ERR_APP_TYPE;
                OBJ_LG_ERROR_LOG.MESSEGE = MESSEGE;
                OBJ_LG_ERROR_LOG.PREVIEW_MESSEGE = PREVIEW_MESSEGE;
                OBJ_LG_ERROR_LOG.STACK_TRACE = STACK_TRACE;
                OBJ_LG_ERROR_LOG.MAKE_BY = MAKE_BY;
                OBJ_LG_ERROR_LOG.MAKE_DT = System.DateTime.Now;

                Obj_DBModelEntities.LG_ERROR_LOG.Add(OBJ_LG_ERROR_LOG);
                Obj_DBModelEntities.SaveChanges();

                return "True";
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                       // Obj_Bool_Check.ERROR = validationError.ErrorMessage;
                    }
                }
               // Obj_Bool_Check.CHECK = false;
                return "False";
            }
            catch (Exception ex)
            {
              //  Obj_Bool_Check.ERROR = ex.Message;
              //  Obj_Bool_Check.CHECK = false;
                return "False";
            }
        }

        #endregion

        #endregion

    }
}
