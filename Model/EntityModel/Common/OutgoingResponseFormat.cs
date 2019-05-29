using Model.EntityModel.LGModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using System.Web;


namespace Model.EntityModel.Common
{
    public static class OutgoingResponseFormat
    {
        public static string SetResponseFormat(string format)
        {
            string retMessage = string.Empty;

            try
            {
                if (string.Equals("json", format.ToLower(), StringComparison.OrdinalIgnoreCase))
                {
                    WebOperationContext.Current.OutgoingResponse.Format = WebMessageFormat.Json;
                }
                else
                {
                    WebOperationContext.Current.OutgoingResponse.Format = WebMessageFormat.Xml;

                }
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

                LG_ERROR_LOG_MAP.Add_Error_Log(null, ex.Source, "ERR_APP_TYPE", "SetResponseFormat",
                       "0000000000", ex.Message, inner4, ex.StackTrace, null, dateTime);

                retMessage = ex.ToString();
            }
            return retMessage;
        }

        public static string GetFormat()
        {
            string format = string.Empty;
            if (string.IsNullOrEmpty(HttpContext.Current.Request["format"]))
            {
                format = "xml";
                return format;
            }
            else
            {
                format = HttpContext.Current.Request["format"].ToString();
                return format;
            }

        }
    }

}
