using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace TouchPark.Services
{
    public class MovementService
    {
        private readonly WebServiceClient _webServiceClient;
        private string _vehicleImageCaptureUrl;

        public MovementService()
        {
            _webServiceClient = new WebServiceClient();
            _vehicleImageCaptureUrl = ConfigurationManager.AppSettings["VehicleImageCaptureUrl"].ToString();
        }
        public List<VehicleMovement> SearchVehicleMovements(string vrm)
        {
            if (string.IsNullOrEmpty(vrm))
            {
                throw new ArgumentOutOfRangeException($"VRM must cannot be empty");
            }

            vrm = vrm.ToUpper().RemoveWhitespace().Trim();
            var url = _vehicleImageCaptureUrl + "/" + vrm ;
            var result = new List<VehicleMovement>();
            var vehicleMovements = _webServiceClient.PostWebResponse(url, null);
            result = JsonConvert.DeserializeObject<List<VehicleMovement>>(vehicleMovements);
            return result;
        }
    }
}
