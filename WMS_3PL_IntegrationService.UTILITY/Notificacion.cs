using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace WMS_3PL_IntegrationService.UTILITY
{
    public class Notificacion
    {
        public static void MailNotification(string subjet, string body)
        {
            var cadenaDeConexion = string.Empty;
            try
            {


              

                string smtp = ConfigurationManager.AppSettings["SMTP"].ToString();
                int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["PUERTO"]); 
                string correo = ConfigurationManager.AppSettings["CORREO"].ToString(); 

                string password = ConfigurationManager.AppSettings["PASSWORD"].ToString(); 
                string correTo = ConfigurationManager.AppSettings["CORREOtoUser"].ToString(); 
              



                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                SmtpClient client = new SmtpClient();
                client.Port = puerto;
                // utilizamos el servidor SMTP de gmail
                client.Host = smtp;
                client.EnableSsl = true;
                client.Timeout = 10000000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                // nos autenticamos con nuestra cuenta de gmail
                client.Credentials = new NetworkCredential(correo, password);


                MailMessage mail = new MailMessage(correo, correTo, subjet, body);
                mail.BodyEncoding = UTF8Encoding.UTF8;
                mail.IsBodyHtml = true;
                
                mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;


                client.Send(mail);


            }
            catch (Exception ex)
            {
                var mensaje = ex.InnerException != null ? ex.Message + ", " + ex.InnerException.Message : ex.Message;
                var bd = "";
                
            }
        }
    }
}
