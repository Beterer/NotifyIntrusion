using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using NotifyIntrusion.Common;
using System.IO;
using System.Diagnostics;

namespace NotifyIntrusion.BL
{
    public class Notifyer
    {
        private static Logger logger = new Logger();

        public void Notify()
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(Constants.NotificationUrl + Constants.PostData + DateTime.Now);

                request.Method = "POST";
                request.ContentType = "text/plain";
                request.ContentLength = 0;


                var response = (HttpWebResponse)request.GetResponse();

                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                    
                logger.LogInfo(String.Format("Notification pushed at {0} {1}", DateTime.Now, responseString));
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }
        }
    }
}
