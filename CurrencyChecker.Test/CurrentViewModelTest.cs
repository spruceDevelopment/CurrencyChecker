using CurrencyChecker.Models;
using CurrencyChecker.Services;
using CurrencyChecker.ViewModels;
using NUnit.Framework;
using Rhino.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyChecker.Test
{
    [TestFixture]
    public class CurrentViewModelTest
    {
        CurrentViewModel _currentViewModel;
        [SetUp]
        public void Setup()
        {
            ICurrencyService currencyService = MockRepository.GenerateMock<ICurrencyService>();
            currencyService.Stub(x => x.GetCurrentRates()).Return(Task.FromResult(new FixerioRates()
            {
                Base = "EUR",
                Rates = new Dictionary<string, float>()
                {
                    {"PLN", 4.31484865f },
                    {"USD", 1.05874658f },
                    {"CZK", 25.7744486f },
                }
            })).Repeat.Any();
            _currentViewModel = new CurrentViewModel(currencyService);
        }

        [Test]
        public async Task InitialState()
        {
            await _currentViewModel.Init();
            Assert.AreEqual(3, _currentViewModel.Items.Count);
            Assert.AreEqual("USD", _currentViewModel.Items[1].Key);
            Assert.AreEqual("1.0587", _currentViewModel.Items[1].DisplayValue);
        }


        [Test]
        public async Task SearchTextChanged_ResultsFiltered()
        {
            await _currentViewModel.Init();
            _currentViewModel.SearchText = "cz";
            Assert.AreEqual(1, _currentViewModel.Items.Count);
            Assert.AreEqual("CZK", _currentViewModel.Items[0].Key);
        }
    }
}
