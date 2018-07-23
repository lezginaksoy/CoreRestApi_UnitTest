using ATM_Management_CoreRestApi.Data.Enums;
using ATM_Management_CoreRestApi.Data.Interface;
using ATM_Management_CoreRestApi.Data.Model;
using ATM_Management_CoreRestApi.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace ATM_Management_CoreRestApi_Test
{
    public class TerminalsController_Test
    {
        public TerminalsController_Test()
        {
        }

        public AtmManagmentContext GetContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<AtmManagmentContext>();
            string connStr = "User ID = postgres;Password=1;Server=localhost;Port=5432;Database=atm;Integrated Security=true; Pooling=true;";
            var dbOpt = optionsBuilder.UseNpgsql(connStr);
            var _context = new AtmManagmentContext(optionsBuilder.Options);
            return _context;
        }

        [Fact]
        public void CheckPin_WithCorrectPinBlock_ReturnsSuccess()
        {
            //setup
            var hsmSrvMock = new Mock<IHsmService>();
            hsmSrvMock.Setup(x => x.CheckPin(It.IsAny<string>())).Returns(PinResult.SuccessfullPin);
            var atmService = new AtmService(hsmSrvMock.Object, null, null);

            //act
            PinResult pr = atmService.CheckPin("32423");

            //assert
            Assert.Equal((int)PinResult.SuccessfullPin, (int)pr);
        }

        [Fact]
        public void CheckPin_WithWrongPinBlock_ReturnsWrongPin()
        {
            //setup
            string pinBlock = "8778";

            var hsmSrvMock = new Mock<IHsmService>();
            hsmSrvMock.Setup(x => x.CheckPin(pinBlock)).Returns(PinResult.WrongPin);
            var atmService = new AtmService(hsmSrvMock.Object, null, null);

            //act
            PinResult pr = atmService.CheckPin(pinBlock);

            //assert
            Assert.Equal((int)PinResult.ExpiredPin, (int)pr);

        }

        [Fact]
        public void WithdrawalMoney_SubstractAccountBalance()
        {
            //setup
            var dbContext = GetContext();

            var accRepo = new AccountRepository(dbContext);
            var atmService = new AtmService(null, dbContext, accRepo);

            decimal balanceBefore = accRepo.GetAccount("A123456").Balance;

            //act
            bool result = atmService.WithdrawalMoney(120, "A123456");

            //assert
            decimal afterBalance = accRepo.GetAccount("A123456").Balance;
            Assert.True(result);
            Assert.Equal(balanceBefore - 120, afterBalance);
        }

        [Fact]
        public void WithdrawalMoney_SubstractAccountBalance_WithMock()
        {
            //setup
            var accRepoMock = new Mock<IAccountRepository>();
            accRepoMock.Setup(x => x.GetAccount("A123456")).Returns(new Account { Balance = 2000 });

            var atmService = new AtmService(null, null, accRepoMock.Object);

            decimal balanceBefore = accRepoMock.Object.GetAccount("A123456").Balance;

            //act
            bool result = atmService.WithdrawalMoney(120, "A123456");

            //assert
            decimal afterBalance = accRepoMock.Object.GetAccount("A123456").Balance;
            Assert.True(result);
            Assert.Equal(balanceBefore - 120, afterBalance);
            accRepoMock.Verify(mock => mock.Update(It.IsAny<Account>()), Times.Once());
        }

        [Fact]
        public void WithdrawalMoney_SubstractAccountBalance_ThrowsException()
        {
            //setup
            var accRepoMock = new Mock<IAccountRepository>();
            accRepoMock.Setup(x => x.GetAccount("A123456")).Returns<Account>(null);
 
            //todo: throws exception 
        }

        [Fact]
        public void WithdrawalMoney_AddsTxn()
        {

        }


    }
}
