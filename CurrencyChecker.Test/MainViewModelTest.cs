
using CurrencyChecker.Core.Contracts;
using CurrencyChecker.Core.Messages;
using CurrencyChecker.Core.Models;
using CurrencyChecker.Core.Services;
using CurrencyChecker.Core.ViewModels;
using GalaSoft.MvvmLight.Messaging;
using NUnit.Framework;
using Rhino.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyChecker.Core.Test
{
    [TestFixture]
    public class MainViewModelTest
    {
        MainViewModel _mainViewModel;
        ICurrencyService _currencyService;
        ISettingsProvider _settingsProvider;
        IErrorHandler _errorHandler;
        [SetUp]
        public void Setup()
        {
            Messenger.Reset();
            _currencyService = MockRepository.GenerateMock<ICurrencyService>();
            _currencyService.Stub(x => x.GetCurrentRates("EUR")).Repeat.Any().Return(Task.FromResult(new RatesDataObject(
                 new Dictionary<string, double>()
                {
                    {"PLN", 4.31484865 },
                    {"USD", 1.05874658 },
                    {"CZK", 25.7744486 },
                })
            ));

            _settingsProvider = MockRepository.GenerateMock<ISettingsProvider>();
            _settingsProvider.Stub(x => x.GetStringValue("baseCurrency")).Repeat.Any().Return("EUR");

            _errorHandler = MockRepository.GenerateMock<IErrorHandler>();

            _mainViewModel = new MainViewModel(_currencyService, _settingsProvider, _errorHandler);
        }

        [Test]
        public async Task InitialState()
        {
            await _mainViewModel.Init();

            Assert.AreEqual(3, _mainViewModel.Items.Count);
            Assert.AreEqual("USD", _mainViewModel.Items[1].TargetName);
            Assert.AreEqual("1.0587", _mainViewModel.Items[1].DisplayValue);
        }


        [Test]
        public async Task SearchTextChanged_ResultsFiltered()
        {
            await _mainViewModel.Init();

            _mainViewModel.SearchText = "cz";

            Assert.AreEqual(1, _mainViewModel.Items.Count);
            Assert.AreEqual("CZK", _mainViewModel.Items[0].TargetName);
        }


        [Test]
        public async Task RefreshedWithNewData_DisplayedDataChanged()
        {
            await _mainViewModel.Init();
            _currencyService.ClearBehavior();
            _currencyService.Stub(x => x.GetCurrentRates("EUR")).Repeat.Any().Return(Task.FromResult(new RatesDataObject(new Dictionary<string, double> {
                    { "PLN", 4.20f },
                    {"USD", 1.15f },
                    {"CZK", 27.05f }, })));

            await _mainViewModel.RefreshCommand.ExecuteAsync();

            Assert.AreEqual("1.1500", _mainViewModel.Items[1].DisplayValue);
        }


        [Test]
        public async Task BaseCurrencyChangedMessageSent_Start()
        {
            await _mainViewModel.Init();
            _settingsProvider.ClearBehavior();
            _settingsProvider.Stub(x => x.GetStringValue("baseCurrency")).Repeat.Any().Return("PLN");

            Messenger.Default.Send(new BaseCurrencyChangedMessage());
            _currencyService.AssertWasCalled(x => x.GetCurrentRates("PLN"));

        }
    }
}
