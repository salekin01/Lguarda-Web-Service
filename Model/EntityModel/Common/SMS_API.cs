using System;
using System.Configuration;
using System.Net;

namespace Model.EntityModel.Common
{
    public class SMS_API
    {
        public string user { get; set; }
        public string pass { get; set; }
        public string sid { get; set; }
        public string receiverCellNo { get; set; }
        public string sms { get; set; }
        public string reffCellNo { get; set; }

        public static bool PushSMS(string RecieverCellno, string SMS_TEXT)
        {
            string HtmlResult;
            String sid = ConfigurationManager.AppSettings["SMS__SID"].ToString();
            String user = ConfigurationManager.AppSettings["SMS__USER"].ToString();
            String pass = ConfigurationManager.AppSettings["SMS__PASS"].ToString();
            String Masking = ConfigurationManager.AppSettings["SMS__URI"].ToString();
            String URI = ConfigurationManager.AppSettings["SMS__URI"].ToString();
            String REFFNO = ConfigurationManager.AppSettings["REFNO"].ToString();            
            
            String myParameters =
                "user=" + user +
                "&pass=" + pass +                
                "&sms[0][0]= " + RecieverCellno +
                "&sms[0][1]=" + SMS_TEXT +
                "&sms[0][2]=" + REFFNO +
                "&sid=" + sid ;

            using (WebClient wc = new WebClient())
            {
                wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                HtmlResult = wc.UploadString(URI, myParameters);              
            }

            return true;
        }
    }
}