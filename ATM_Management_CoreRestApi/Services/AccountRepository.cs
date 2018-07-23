using ATM_Management_CoreRestApi.Data.Interface;
using ATM_Management_CoreRestApi.Data.Model;
using Payment_Transactions.Model.Repository;
using System.Linq;

namespace ATM_Management_CoreRestApi.Services
{
    public class AccountRepository : Repository<Account>, IAccountRepository
    {

        public AccountRepository(AtmManagmentContext Context) : base(Context)
        {
        }

        public Account GetAccount(string accountNo)
        {
            var account = _context.Account.FirstOrDefault(x => x.AccountNo == accountNo);

            return account;
        }
    }
}
