using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using NotifyIntrusion.Common;
using System.IO;
using System.Diagnostics;
using NotifyIntrusion.Models;

namespace NotifyIntrusion.BL
{
    public class Notifyer
    {
        private static Logger logger = new Logger();
        private static List<Models.CamRequest> CamRequests = new List<Models.CamRequest>();

        public void Notify(string camId)
        {
            if (ShouldNotify(camId))
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

        private bool ShouldNotify(string camId)
        {
            CleanUpCamRequests();

            CamRequests.Add(new CamRequest { AlertTIme = DateTime.Now, CamId = CamIdToEnum(camId) });

            var counts = new List<int>();
            foreach (var cam in Enum.GetValues(typeof(CamRequest.Cam)))
            {
                counts.Add(CamRequests.Select(c => c.CamId == (CamRequest.Cam)cam).Count());
            }

            foreach (var count in counts)
                if (count >= 2)
                    return true;

            return false;
        }

        private void CleanUpCamRequests()
        {
            foreach(var cam in CamRequests)
            {
                if ((DateTime.Now - cam.AlertTIme).TotalSeconds >= 3)
                    CamRequests.Remove(cam);
            }
        }

        private CamRequest.Cam CamIdToEnum(string camId)
        {
            switch (camId)
            {
                case "facecam":
                    return CamRequest.Cam.FaceCam;
                case "laptopcam":
                    return CamRequest.Cam.LaptopCam;
                case "microsoftcam":
                    return CamRequest.Cam.MicrosoftCam;
                default:
                    return CamRequest.Cam.NA;
            }
        }
    }
}
