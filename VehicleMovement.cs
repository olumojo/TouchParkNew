using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TouchPark
{
    public class VehicleMovement
    {
        public long ID { get; set; }
        public string SiteCode { get; set; }
        public string VRN { get; set; }
        public DateTime TimeStamp { get; set; }
        public int Direction { get; set; }
        public string Lane { get; set; }
        public int Confidence { get; set; }
        public string MachineName { get; set; }
        public string PlateImage { get; set; }
        public string OverviewImage { get; set; }
        public string InfraredImage { get; set; }
    }
}
