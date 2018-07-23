using ATM_Management_CoreRestApi.Data.Enums;
using ATM_Management_CoreRestApi.Data.Interface;
using System;

namespace ATM_Management_CoreRestApi.Services
{
    public class HsmService : IHsmService
    {
        public PinResult CheckPin(string pin)
        {
            throw new Exception("Kablo takılı değil");
        }
    }
}
