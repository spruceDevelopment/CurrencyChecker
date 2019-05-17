using CurrencyChecker.Models;
using System.Threading.Tasks;

namespace CurrencyChecker.Services
{
    public interface ICurrencyService
    {
        Task<FixerioRates> GetCurrentRates();


    }
}