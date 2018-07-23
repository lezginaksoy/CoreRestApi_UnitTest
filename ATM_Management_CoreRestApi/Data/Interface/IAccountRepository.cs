using ATM_Management_CoreRestApi.Data.Model;
using Payment_Transactions.Model.Interface;

namespace ATM_Management_CoreRestApi.Data.Interface
{
    public interface IAccountRepository : IRepository<Account>
    {
        Account GetAccount(string accountNo);
    }
}
