using CurrencyChecker.Core.Contracts;
using CurrencyChecker.Core.Messages;
using CurrencyChecker.Core.Models;
using CurrencyChecker.Core.Services;
using CurrencyChecker.Core.ViewModels;
using GalaSoft.MvvmLight.Ioc;
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
    public class RateViewModelTest
    {
        RateViewModel _rateViewModel;
        IExternalCurrencyService _currencyService;
        ISettingsProvider _settingsProvider;
        ILocalCurrencyService _localCurrencyService;
        [SetUp]
        public void Setup()
        {
            SimpleIoc.Default.Reset();
            _currencyService = MockRepository.GenerateMock<IExternalCurrencyService>();
            _currencyService.Stub(x => x.GetHistoryRates("EUR", "PLN", DateTime.Today.AddDays(-30), DateTime.Today)).Repeat.Any().Return(Task.FromResult(new HistoryRatesDataObject
            {
                HistoryRates =
                new Dictionary<string,  float>()
                {
                    {"2019-04-20", 0.21054f },
                    {"2019-04-27", 0.21164f },
                    {"2019-05-04", 0.21004f },
                    {"2019-05-11", 0.21210f },
                    {"2019-05-18", 0.21698f },
                }
            }
            ));

            

            _settingsProvider = MockRepository.GenerateMock<ISettingsProvider>();

            _localCurrencyService = MockRepository.GenerateMock<ILocalCurrencyService>();

            _rateViewModel = new RateViewModel("EUR", "PLN", 4.30f, _currencyService, _settingsProvider, _localCurrencyService);
        }

        [Test]
        public void ObjectConstructed()
        {
            Assert.AreEqual("4.3000", _rateViewModel.DisplayValue);
            Assert.AreEqual("1 EUR = x PLN", _rateViewModel.Title);
        }


        [Test]
        public async Task Initialization_ChartEntriesFilled()
        {
            await _rateViewModel.Init();

            Assert.AreEqual(5, _rateViewModel.Chart.Entries.Count());
            Assert.AreEqual(0.21210f, _rateViewModel.Chart.Entries.ElementAt(3).Value);
        }


        [Test]
        public async Task SetAsBaseCurrency_ChangedAndNavigatedBack()
        {
            await _rateViewModel.Init();
            bool called = false;
            Messenger.Default.Register<BaseCurrencyChangedMessage>(this, (arg) => called = true);
            var navigatorMock = MockRepository.GenerateMock<INavigator>();
            navigatorMock.Stub(x => x.GoBackAsync()).Return(Task.CompletedTask);
            SimpleIoc.Default.Register(()=> navigatorMock);

            await _rateViewModel.SetAsBaseCurrencyCommand.ExecuteAsync();

            _settingsProvider.AssertWasCalled(x => x.SetValue("baseCurrency", "PLN"));
            Assert.IsTrue(called);
            navigatorMock.AssertWasCalled(x=>x.GoBackAsync());
        }


        [Test]
        public async Task SaveDataCommand_DatabaseSaved()
        {
            await _rateViewModel.Init();

            _localCurrencyService.Expect(x => x.AddRecordAsync(null)).IgnoreArguments().Return(Task.CompletedTask);

            await _rateViewModel.SaveDataCommand.ExecuteAsync();

            _localCurrencyService.VerifyAllExpectations();
        }
    }
}
