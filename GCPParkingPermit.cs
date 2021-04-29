using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TouchPark
{
    public class GCPParkingPermit
    {
        public string PermitType { get; set; }

        public DateTime CaptureDate { get; set; }

        public string VehicleRegMark { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string PaymentType { get; set; }

        public Decimal Amount { get; set; }

        public string AuthCode { get; set; }

        public Decimal Paid { get; set; }

        public string MachineName { get; set; }

        public string UserName { get; set; }

        public int UserID { get; set; }

        public string PassCode { get; set; }
    }
}
