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
        ICurrencyService _currencyService;
        ISettingsProvider _settingsProvider;
        [SetUp]
        public void Setup()
        {
            SimpleIoc.Default.Reset();
            DateTime startDate = new DateTime(2019, 4, 20);
            DateTime endDate = new DateTime(2019, 5, 19);
            _currencyService = MockRepository.GenerateMock<ICurrencyService>();
            _currencyService.Stub(x => x.GetHistoryRates("EUR", "PLN", startDate, endDate)).Repeat.Any().Return(Task.FromResult(new HistoryRatesDataObject(
                new Dictionary<string, KeyValuePair<string, float>>()
                {
                    {"2019-04-20", new KeyValuePair<string, float>("PLN",0.21054f) },
                    {"2019-04-27", new KeyValuePair<string, float>("PLN",0.21164f) },
                    {"2019-05-04", new KeyValuePair<string, float>("PLN",0.21004f) },
                    {"2019-05-11", new KeyValuePair<string, float>("PLN",0.21210f) },
                    {"2019-05-18", new KeyValuePair<string, float>("PLN",0.21698f) },
                })
            ));

            

            _settingsProvider = MockRepository.GenerateMock<ISettingsProvider>();
            _settingsProvider.Stub(x => x.GetValue<DateTime>("startDate")).Repeat.Any().Return(startDate);
            _settingsProvider.Stub(x => x.GetValue<DateTime>("endDate")).Repeat.Any().Return(endDate);

            _rateViewModel = new RateViewModel("EUR", "PLN", 4.30f, _currencyService, _settingsProvider);
        }

        [Test]
        public void ObjectConstructed()
        {
            Assert.AreEqual("4.3000", _rateViewModel.DisplayValue);
            Assert.AreEqual("1 EUR = x PLN", _rateViewModel.Title);
        }


        [Test]
        public async Task Initialization_ChartEntriesSortedAndFilled()
        {
            await _rateViewModel.Init();

            Assert.AreEqual(5, _rateViewModel.Chart.Entries.Count());
            Assert.AreEqual(0.21210f, _rateViewModel.Chart.Entries.ElementAt(_rateViewModel.Chart.Entries.Count() - 4).Value);
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
    }
}
