using Newtonsoft.Json;
using Backend.Application.DTOs;
using Backend.Domain.DTOs.Requests;
using Backend.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Infrastructure.Services
{
    public class NotificationService : INotificationService
    {
        private const string RequestUriString = "https://fcm.googleapis.com/fcm/send";
        public bool SendPushNotification(PushNotificationModel notification)
        {
            try
            {
                var senderId = "547148223120";
                var serverKey = "AAAAf2STUpA:APA91bGttv2uO2Sla3bXOrenk83j4N2Ed4vcc1ydbDGJFOCSaWx71FSjliGtn-y3666Y6lkZmZNpHCIw7q6ur56cZcgV6nTHZdpNmxzzAWobtgZGWRQTd6XBDYq7xgqi2HIH7Kco5vHU";
                var deviceId = notification.To;
                var push = new NotificationModel()
                {
                    DeviceId = notification.To,
                    IsAndroiodDevice = true,
                    SenderId = senderId,
                    ServerKey = serverKey,
                    Title = notification.Title,
                    Body = notification.Body
                };
                var send = SendNotification(push);
                return send;
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        private bool SendNotification(NotificationModel notificationModel)
        {
            //ResponseModel response = new ResponseModel();
            var response = string.Empty;
            try
            {
                if (notificationModel.IsAndroiodDevice)
                {
                    /* FCM Sender (Android Device) */
                    WebRequest tRequest = WebRequest.Create(RequestUriString);
                    tRequest.Method = "post";
                    tRequest.ContentType = "application/json";
                    var data = new
                    {
                        to = notificationModel.DeviceId,
                        notification = new
                        {
                            body = notificationModel.Body,
                            title = notificationModel.Title,
                            icon = "",
                            priority = "high"
                        }
                    };

                    var json = JsonConvert.SerializeObject(data);
                    Byte[] byteArray = Encoding.UTF8.GetBytes(json);
                    tRequest.Headers.Add(string.Format("Authorization: key={0}", notificationModel.ServerKey));
                    tRequest.Headers.Add(string.Format("Sender: id={0}", notificationModel.SenderId));
                    tRequest.ContentLength = byteArray.Length;
                    using (Stream dataStream = tRequest.GetRequestStream())
                    {
                        dataStream.Write(byteArray, 0, byteArray.Length);
                        using (WebResponse tResponse = tRequest.GetResponse())
                        {
                            using (Stream dataStreamResponse = tResponse.GetResponseStream())
                            {
                                using (StreamReader tReader = new StreamReader(dataStreamResponse))
                                {
                                    String sResponseFromServer = tReader.ReadToEnd();
                                    response = sResponseFromServer;
                                }
                            }
                        }
                    }
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        public bool SendEmailNotification(EmailNotifictionModel notification)
        {
            var send = SendEmail(notification.To, notification.Subject, notification.Body, notification.Attachment, notification.Cc, notification.Cco);
            return send;
        }

        private bool SendEmail(string to, string subject, string body, List<string> attachment, string cc = "", string bcc = "")
        {
            try
            {
                var configurationEmail = new EmailConfigurationModel();
                var mail = new System.Net.Mail.MailMessage();
                mail.From = new System.Net.Mail.MailAddress(configurationEmail.EmailFrom);
                mail.To.Add(to);

                if (!string.IsNullOrEmpty(cc))
                    mail.CC.Add(cc);

                if (!string.IsNullOrEmpty(bcc))
                    mail.Bcc.Add(bcc);

                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;
                mail.Priority = System.Net.Mail.MailPriority.Normal;

                #region Adjuntos

                if (attachment != null)
                {
                    foreach (var pathFile in attachment)
                    {
                        //Agrego el archivo que puse en la ruta anterior "PathFile", y su tipo.
                        var Data = new Attachment(pathFile);
                        //Obtengo las propiedades del archivo.
                        ContentDisposition disposition = Data.ContentDisposition;
                        disposition.CreationDate = System.IO.File.GetCreationTime(pathFile);
                        disposition.ModificationDate = System.IO.File.GetLastWriteTime(pathFile);
                        disposition.ReadDate = System.IO.File.GetLastAccessTime(pathFile);
                        //Agrego el archivo al mensaje
                        mail.Attachments.Add(Data);
                    }
                }

                IList<string> Att = new List<string>();
                #endregion


                var smtp1 = new SmtpClient();
                var credential = new NetworkCredential
                {
                    UserName = configurationEmail.EmailUser,  // replace with valid value
                    Password = configurationEmail.EmailPass   // replace with valid value
                };
                smtp1.Credentials = credential;
                smtp1.Host = configurationEmail.EmailServer;
                smtp1.Port = configurationEmail.EmailPort;
                smtp1.EnableSsl = configurationEmail.EmailEnabledSsl;
                smtp1.UseDefaultCredentials = false;
                smtp1.Send(mail);

            }
            catch (Exception e)
            {
                return false;
            }
            return true;

        }
    }
}
