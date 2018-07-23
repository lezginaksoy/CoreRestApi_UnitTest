using ATM_Management_CoreRestApi.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ATM_Management_CoreRestApi.Data.Interface
{
    public interface IHsmService
    {
        PinResult CheckPin(string pin);
    }

}
