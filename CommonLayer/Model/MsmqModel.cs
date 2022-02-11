using Experimental.System.Messaging;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.Model
{
    public class MsmqModel
    {
        MessageQueue msmq = new MessageQueue();
        public void MsmqSender(string token)
        {
            msmq.Path = @".\private$\FundooToken";
            if (!MessageQueue.Exists(msmq.Path))
            {
                MessageQueue.Create(msmq.Path);
            }

            msmq.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            msmq.ReceiveCompleted += MsmqReceiver;
            msmq.Send(token);
            msmq.BeginReceive();
            msmq.Close();
        }

        private void MsmqReceiver(object sender, ReceiveCompletedEventArgs e)
        {
            var message = msmq.EndReceive(e.AsyncResult);
            string token = message.Body.ToString();
            string Subject = "Fundoo Notes Application Password Reset";
            string Body = $"<a href = http://localhost:4200/resetPassword/{token} click me </a>" + "Token ->:" + token;
            string jwt = DecodeJwt(token);




            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("noormd6400@gmail.com", "NoorMohd@43"),
                EnableSsl = true,
            };

            smtpClient.Send("noormd6400@gmail.com", jwt, Subject, Body);


            msmq.BeginReceive();
        }
        public static string DecodeJwt(string token)
        {
            try
            {
                var decodeToken = token;
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadJwtToken((decodeToken));
                var result = jsonToken.Claims.FirstOrDefault().Value;
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
