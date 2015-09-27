using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotifyIntrusion.Models
{
    public class CamRequest
    {
        public enum Cam
        {
            FaceCam,
            LaptopCam,
            MicrosoftCam,

            NA
        }

        public Cam CamId { get; set; }
        public DateTime AlertTIme { get; set; }
    }
}
