using ATM_Management_CoreRestApi.Data.Enums;
using ATM_Management_CoreRestApi.Data.Interface;
using ATM_Management_CoreRestApi.Data.Model;
using System;
using System.Linq;

namespace ATM_Management_CoreRestApi.Services
{
    public class AtmService
    {
        private IHsmService _hsmService;
        private AtmManagmentContext _dbcontext;
        private IAccountRepository _accRepo;
        public AtmService(IHsmService hsmService, AtmManagmentContext dbcontext, IAccountRepository accountRepository)
        {
            _hsmService = hsmService;
            _dbcontext = dbcontext;
            _accRepo = accountRepository;
        }
         
        //public List<Transactions> GetTxnsByAccount(string accountNo)
        //{
        //    throw new NotImplementedException();
        //}

        public bool WithdrawalMoney(decimal amount, string accountNo)
        {
            var account = _accRepo.GetAccount(accountNo);
            if (account == null)
                throw new Exception("hesap bulunamadı");

            if (account.Balance < amount)
                return false;

            account.Balance = account.Balance - amount;
            _accRepo.Update(account);

            //todo: add txn

            return true;
        }

        public PinResult CheckPin(string pinBlock)
        {
            var pinResult = _hsmService.CheckPin(pinBlock);

            if (pinResult == PinResult.WrongPin)
                pinResult = PinResult.ExpiredPin;

            return pinResult;
        }

    }
}
