using ATM_Management_CoreRestApi.Controllers;
using ATM_Management_CoreRestApi.Data.Interface;
using System;
using System.Linq;

using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ATM_Managment_CoreRestApi.Test
{
   public class TerminalControllerTest
    {
       // private readonly ITerminalRepository _TermRepo;

        [Fact]
        public void GetTerminalCountTest()
        {
            var Controller = new DefaultController();
            var Result = Controller.Get();
            Assert.Equal(3, Result.Count());           


        }

    }
}
