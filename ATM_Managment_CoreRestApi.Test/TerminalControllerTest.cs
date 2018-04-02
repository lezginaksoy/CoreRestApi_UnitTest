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
        private readonly ITerminalRepository _TermRepo;


      
        public  TerminalControllerTest(ITerminalRepository repository)
        {
            _TermRepo = repository;

        }

        [Fact]
        public void GetTerminalCountTest()
        {
            var Controller = new TerminalsController(_TermRepo);

            //var Controller = new DefaultController();
            var Result = Controller.Get();


            Assert.Equal(4,Result.Count());           


        }

    }
}
