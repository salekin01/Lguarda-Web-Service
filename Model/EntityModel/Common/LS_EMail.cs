using Model.EDMX;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Model.EntityModel.Common
{
    public class LS_EMail
    {
        /// <summary>
        /// Basic send mail function. Just call and mail
        /// <remarks>Can not handle a list of recivers - only one in each of the fields To, CC, BCC</remarks>
        /// </summary>
        /// <param name="ToDisplayName"></param>
        /// <param name="ToAdr"></param>
        /// <param name="FromDisplayName"></param>
        /// <param name="FromAdr"></param>
        /// <param name="CcDisplayName"></param>
        /// <param name="CcAdr"></param>
        /// <param name="BccAdr"></param>
        /// <param name="Subject"></param>
        /// <param name="BodyText"></param>
        /// <param name="AttachmentFileName"></param>
        /// <param name="VerboseLevel">Default logging errors and warnings.</param>
        public static void BasicSendMail(string ToDisplayName, string ToAdr
            , string FromDisplayName, string FromAdr
            , string CcDisplayName, string CcAdr
            , string BccAdr
            , string Subject, string BodyText, string AttachmentFileName)
        {
            MailMessage mes = new MailMessage();
            char[] sepr = new char[] { ',', ';' };
            string _fromadd = "";
            string _fromdisp = "";
            string[] _toadd = new string[50];
            string[] _todisp = new string[50];
            string[] _ccadd = new string[50];
            string[] _ccdisp = new string[50];
            string[] _bccadd = new string[50];
            string[] _bccdisp = new string[50];
            string[] _attachFile = new string[50];

            #region FROM

            if (!String.IsNullOrEmpty(FromAdr))
                _fromadd = FromAdr;
            if (!String.IsNullOrEmpty(FromDisplayName))
                _fromdisp = FromDisplayName;
            if (string.IsNullOrEmpty(_fromdisp))
                mes.From = new MailAddress(_fromadd);
            else
                mes.From = new MailAddress(_fromadd, _fromdisp);

            #endregion FROM

            #region TO

            if (!String.IsNullOrEmpty(ToAdr))
            {
                _toadd = ToAdr.Split(sepr);
            }
            if (!String.IsNullOrEmpty(ToDisplayName))
            {
                _todisp = ToDisplayName.Split(sepr);
            }
            for (int i = 0; i < _toadd.Length; i++)
            {
                if (!String.IsNullOrEmpty(_toadd[i]))
                    mes.To.Add(_toadd[i]);
            }

            #endregion TO

            #region CC

            /*if (!String.IsNullOrEmpty(CcAdr))
            {
                string _ccdisp = !String.IsNullOrEmpty(CcDisplayName) ? CcDisplayName : CcAdr;
                mes.CC.Add(new MailAddress(CcAdr, _ccdisp));
            }*/
            if (!String.IsNullOrEmpty(CcAdr))
            {
                _ccadd = CcAdr.Split(sepr);
            }
            if (!String.IsNullOrEmpty(CcDisplayName))
            {
                _ccdisp = CcDisplayName.Split(sepr);
            }
            //for (int i = 0; i < _ccadd.Length; i++)
            //{
            //    if (!String.IsNullOrEmpty(_ccadd[i]))
            //        mes.CC.Add(new MailAddress(_ccadd[i], _ccdisp[i]));
            //}

            #endregion CC

            #region BCC

            /*if (!String.IsNullOrEmpty(BccAdr))
                mes.Bcc.Add(new MailAddress(BccAdr, BccAdr));*/
            if (!String.IsNullOrEmpty(BccAdr))
            {
                _bccadd = BccAdr.Split(sepr);
            }
            /*if (!String.IsNullOrEmpty(BccDisplayName))
            {
                _bccdisp = BccDisplayName.Split(sepr);
            }*/
            //for (int i = 0; i < _bccadd.Length; i++)
            //{
            //    if (!String.IsNullOrEmpty(_bccadd[i]))
            //        mes.Bcc.Add(new MailAddress(_bccadd[i]));
            //}

            #endregion BCC

            #region Subject & Body

            mes.Subject = Subject;
            //mes.SubjectEncoding = System.Text.Encoding.UTF8;
            mes.IsBodyHtml = true;
            mes.Body = BodyText;
            mes.BodyEncoding = System.Text.Encoding.UTF8;

            #endregion Subject & Body

            #region Atachment

            if (!String.IsNullOrEmpty(AttachmentFileName))
            {
                _attachFile = AttachmentFileName.Split(sepr);
            }
            for (int i = 0; i < _attachFile.Length; i++)
            {
                if (!String.IsNullOrEmpty(_attachFile[i]))
                    mes.Attachments.Add(new Attachment(_attachFile[i]));
                //mes.Attachments.Add(new MailAttachment(_attachFile[i]));
            }

            #endregion Atachment

            string LS_MailServerIP = ConfigurationManager.AppSettings["LS_MailServerIP"];
            string LS_MailSenderAddress = ConfigurationManager.AppSettings["LS_MailSenderAddress"];
            string LS_MailSenderAddressPassword = ConfigurationManager.AppSettings["LS_MailSenderAddressPassword"];

            //SmtpClient smtpcli = new SmtpClient(LS_MailServerIP, 587);

           // System.Net.ServicePointManager.ServerCertificateValidationCallback += delegate { return true; };
            SmtpClient smtpcli = new SmtpClient(LS_MailServerIP);

            //smtpcli.EnableSsl = false;
            //smtpcli.DeliveryMethod = SmtpDeliveryMethod.Network;
            //smtpcli.Credentials = new NetworkCredential(LS_MailSenderAddress, LS_MailSenderAddressPassword);

            //SmtpClient sm = new SmtpClient(LS_MailServerIP);
            //SmtpMail.SmtpServer = LS_MailServerIP;

            //SmtpMail.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis;
            try
            {
                //SmtpMail.Send(mes);
                smtpcli.Send(mes);
            }
            catch (System.Exception ex)
            {
                //mes.To.Add("mahbub@premierbankltd.com");
                //mes.Body = System.Web.HttpContext.Current.Server.HtmlEncode(ex.Message).ToString();
                //smtpcli.Send(mes);
                string errmes = "LM_EMail.BasicSendMail() error. Mail post attemt: " +
                    "\nToDisplayName=" + ToDisplayName +
                    "\nToAdr=" + ToAdr +
                    "\nFromDisplayName=" + FromDisplayName +
                    "\nFromAdr=" + FromAdr +
                    "\nCcDisplayName=" + CcDisplayName +
                    "\nCcAdr=" + CcAdr +
                    "\nBccAdr=" + BccAdr +
                    "\nSubject=" + Subject +
                    "\nBodyText=" + BodyText +
                    "\nAttachmentFileName=" + AttachmentFileName +
                    "\nErrormessage:" + ex.Message +
                    "\nSource:" + ex.Source +
                    "\nStack:" + ex.StackTrace +
                    "\nInnerMessage:" + ex.InnerException != null ? ex.InnerException.Message : "No inner exception exist" +
                    "\nMainMessage:" + ex.Message;

                LogError(errmes);
                throw (new System.Exception(errmes));
            }
            finally
            {
            }
        }

        public static void BasicSendMail(string ToDisplayName, string ToAdr
            , string FromDisplayName, string FromAdr
            , string CcDisplayName, string CcAdr
            , string BccAdr
            , string Subject, string BodyText, Attachment AttachmentFileName)
        {
            MailMessage mes = new MailMessage();
            char[] sepr = new char[] { ',', ';' };
            string _fromadd = "";
            string _fromdisp = "";
            string[] _toadd = new string[50];
            string[] _todisp = new string[50];
            string[] _ccadd = new string[50];
            string[] _ccdisp = new string[50];
            string[] _bccadd = new string[50];
            string[] _bccdisp = new string[50];
            string[] _attachFile = new string[50];

            #region FROM

            if (!String.IsNullOrEmpty(FromAdr))
                _fromadd = FromAdr;
            if (!String.IsNullOrEmpty(FromDisplayName))
                _fromdisp = FromDisplayName;
            if (string.IsNullOrEmpty(_fromdisp))
                mes.From = new MailAddress(_fromadd);
            else
                mes.From = new MailAddress(_fromadd, _fromdisp);

            #endregion FROM

            #region TO

            if (!String.IsNullOrEmpty(ToAdr))
            {
                _toadd = ToAdr.Split(sepr);
            }
            if (!String.IsNullOrEmpty(ToDisplayName))
            {
                _todisp = ToDisplayName.Split(sepr);
            }
            for (int i = 0; i < _toadd.Length; i++)
            {
                if (!String.IsNullOrEmpty(_toadd[i]))
                    mes.To.Add(_toadd[i]);
            }

            #endregion TO

            #region CC

            /*if (!String.IsNullOrEmpty(CcAdr))
            {
                string _ccdisp = !String.IsNullOrEmpty(CcDisplayName) ? CcDisplayName : CcAdr;
                mes.CC.Add(new MailAddress(CcAdr, _ccdisp));
            }*/
            if (!String.IsNullOrEmpty(CcAdr))
            {
                _ccadd = CcAdr.Split(sepr);
            }
            if (!String.IsNullOrEmpty(CcDisplayName))
            {
                _ccdisp = CcDisplayName.Split(sepr);
            }
            //for (int i = 0; i < _ccadd.Length; i++)
            //{
            //    if (!String.IsNullOrEmpty(_ccadd[i]))
            //        mes.CC.Add(new MailAddress(_ccadd[i], _ccdisp[i]));
            //}

            #endregion CC

            #region BCC

            /*if (!String.IsNullOrEmpty(BccAdr))
                mes.Bcc.Add(new MailAddress(BccAdr, BccAdr));*/
            if (!String.IsNullOrEmpty(BccAdr))
            {
                _bccadd = BccAdr.Split(sepr);
            }
            /*if (!String.IsNullOrEmpty(BccDisplayName))
            {
                _bccdisp = BccDisplayName.Split(sepr);
            }*/
            //for (int i = 0; i < _bccadd.Length; i++)
            //{
            //    if (!String.IsNullOrEmpty(_bccadd[i]))
            //        mes.Bcc.Add(new MailAddress(_bccadd[i]));
            //}

            #endregion BCC

            #region Subject & Body

            mes.Subject = Subject;
            //mes.SubjectEncoding = System.Text.Encoding.UTF8;
            mes.IsBodyHtml = true;
            mes.Body = BodyText;
            mes.BodyEncoding = System.Text.Encoding.UTF8;

            #endregion Subject & Body

            #region Atachment

            mes.Attachments.Add(AttachmentFileName);

            #endregion Atachment

            string LS_MailServerIP = ConfigurationManager.AppSettings["LS_MailServerIP"];
            string LS_MailSenderAddress = ConfigurationManager.AppSettings["LS_MailSenderAddress"];
            string LS_MailSenderAddressPassword = ConfigurationManager.AppSettings["LS_MailSenderAddressPassword"];
            //System.Net.ServicePointManager.ServerCertificateValidationCallback += delegate { return true; };
            SmtpClient smtpcli = new SmtpClient(LS_MailServerIP);
            smtpcli.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpcli.Credentials = new NetworkCredential(LS_MailSenderAddress, LS_MailSenderAddressPassword);
            try
            {
                smtpcli.Send(mes);
            }
            catch (System.Exception ex)
            {
                //mes.To.Add("mahbub@premierbankltd.com");
                //mes.Body = System.Web.HttpContext.Current.Server.HtmlEncode(ex.Message).ToString();
                //smtpcli.Send(mes);
                string errmes = "LM_EMail.BasicSendMail() error. Mail post attemt: " +
                    "\nToDisplayName=" + ToDisplayName +
                    "\nToAdr=" + ToAdr +
                    "\nFromDisplayName=" + FromDisplayName +
                    "\nFromAdr=" + FromAdr +
                    "\nCcDisplayName=" + CcDisplayName +
                    "\nCcAdr=" + CcAdr +
                    "\nBccAdr=" + BccAdr +
                    "\nSubject=" + Subject +
                    "\nBodyText=" + BodyText +
                    "\nAttachmentFileName=" + AttachmentFileName +
                    "\nErrormessage:" + ex.Message +
                    "\nSource:" + ex.Source +
                    "\nStack:" + ex.StackTrace +
                    "\nInnerMessage:" + ex.InnerException != null ? ex.InnerException.Message : "No inner exception exist" +
                    "\nMainMessage:" + ex.Message;

                LogError(errmes);
                throw (new System.Exception(errmes));
            }
            finally
            {
            }
        }

        private static void LogError(string errorMsg)
        {
            string appPath = HttpContext.Current.Request.PhysicalApplicationPath;
            string filePath = appPath + "errorOfEmail.txt";
            StreamWriter w;
            try
            {
                w = File.AppendText(filePath);
                w.WriteLine(errorMsg);
                w.Flush();
                w.Close();
            }
            catch (Exception ex)
            {
            }
        }

        //salekin added bellow
        public static string BasicSendMail(string ToDisplayName, string ToAdr, string Subject, string BodyText, string app_id)
        {
            string FromDisplayName = string.Empty;
            string FromAdr = string.Empty;
            string CcDisplayName = string.Empty;
            string CcAdr = string.Empty;
            string BccAdr = string.Empty;

            DBModelEntities Obj_DBModelEntities = new DBModelEntities(ConfigurationManager.ConnectionStrings["DBModelEntities"].ConnectionString);
            var MAIL_SERVER_CONFIG_INFO = (Obj_DBModelEntities.LG_SYS_MAIL_SERVER_CONFIG
                                          .Where(u => u.APPLICATION_ID == app_id &&
                                                      u.AUTH_STATUS_ID != "U")
                                          .Select(u => u)).SingleOrDefault();

            FromAdr = MAIL_SERVER_CONFIG_INFO.MAIL_SENDER_ADDRESS;
            string LS_MailServerIP = MAIL_SERVER_CONFIG_INFO.MAIL_SENDER_IP;
            string LS_MailSenderAddressPassword = MAIL_SERVER_CONFIG_INFO.MAIL_SENDER_PASSWORD;

            MailMessage mes = new MailMessage();
            char[] sepr = new char[] { ',', ';' };
            string _fromadd = "";
            string _fromdisp = "";
            string[] _toadd = new string[50];
            string[] _todisp = new string[50];
            string[] _ccadd = new string[50];
            string[] _ccdisp = new string[50];
            string[] _bccadd = new string[50];
            string[] _bccdisp = new string[50];
            string[] _attachFile = new string[50];

            #region FROM

            if (!String.IsNullOrEmpty(FromAdr))
                _fromadd = FromAdr;
            if (!String.IsNullOrEmpty(FromDisplayName))
                _fromdisp = FromDisplayName;
            if (string.IsNullOrEmpty(_fromdisp))
                mes.From = new MailAddress(_fromadd);
            else
                mes.From = new MailAddress(_fromadd, _fromdisp);

            #endregion FROM

            #region TO

            if (!String.IsNullOrEmpty(ToAdr))
            {
                _toadd = ToAdr.Split(sepr);
            }
            if (!String.IsNullOrEmpty(ToDisplayName))
            {
                _todisp = ToDisplayName.Split(sepr);
            }
            for (int i = 0; i < _toadd.Length; i++)
            {
                if (!String.IsNullOrEmpty(_toadd[i]))
                    mes.To.Add(_toadd[i]);
            }

            #endregion TO

            #region CC

            if (!String.IsNullOrEmpty(CcAdr))
            {
                _ccadd = CcAdr.Split(sepr);
            }
            if (!String.IsNullOrEmpty(CcDisplayName))
            {
                _ccdisp = CcDisplayName.Split(sepr);
            }

            #endregion CC

            #region BCC

            if (!String.IsNullOrEmpty(BccAdr))
            {
                _bccadd = BccAdr.Split(sepr);
            }

            #endregion BCC

            #region Subject & Body

            mes.Subject = Subject;
            mes.IsBodyHtml = true;
            mes.Body = BodyText;
            mes.BodyEncoding = System.Text.Encoding.UTF8;

            #endregion Subject & Body

            //ServicePointManager.ServerCertificateValidationCallback += delegate { return true; };

            SmtpClient smtpcli = new SmtpClient(LS_MailServerIP);
            //smtpcli.DeliveryMethod = SmtpDeliveryMethod.Network;
            //smtpcli.Credentials = (ICredentialsByHost)new NetworkCredential(FromAdr, LS_MailSenderAddressPassword);

            if (Convert.ToInt32(ConfigurationManager.AppSettings["SMTP_PORT"].ToString()) != 0)
            {
                smtpcli.Port = Convert.ToInt32(ConfigurationManager.AppSettings["SMTP_PORT"].ToString());
            }

            try
            {
                //ServicePointManager.ServerCertificateValidationCallback += delegate { return true; };
                smtpcli.Send(mes);
            }
            catch (System.Exception ex)
            {
                string errmes = "LM_EMail.BasicSendMail() error. Mail post attemt: " +
                    "\nToDisplayName=" + ToDisplayName +
                    "\nToAdr=" + ToAdr +
                    "\nFromDisplayName=" + FromDisplayName +
                    "\nFromAdr=" + FromAdr +
                    "\nCcDisplayName=" + CcDisplayName +
                    "\nCcAdr=" + CcAdr +
                    "\nBccAdr=" + BccAdr +
                    "\nSubject=" + Subject +
                    "\nBodyText=" + BodyText +
                    "\nErrormessage:" + ex.Message +
                    "\nSource:" + ex.Source +
                    "\nStack:" + ex.StackTrace +
                    "\nInnerMessage:" + ex.InnerException != null ? ex.InnerException.Message : "No inner exception exist" +
                    "\nMainMessage:" + ex.Message;

                return errmes;
            }
            return "1";
        }
    }
}