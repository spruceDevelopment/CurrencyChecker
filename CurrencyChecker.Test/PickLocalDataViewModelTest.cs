
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
    public class PickLocalDataViewModelTest
    {
        PickLocalDataViewModel _testViewModel;
        IErrorHandler _errorHandler;
        ILocalCurrencyService _localCurrencyService;
        INavigator _navigator;
        [SetUp]
        public void Setup()
        {
            SimpleIoc.Default.Reset();
            Messenger.Reset();
            

            _errorHandler = MockRepository.GenerateMock<IErrorHandler>();

            _localCurrencyService = MockRepository.GenerateMock<ILocalCurrencyService>();
            _localCurrencyService.Stub(x => x.GetAllRecordsNamesAsync()).Repeat.Any().Return(Task.FromResult(new List<string> {
                "test1",
                "test2",
                "test3"
            }));

            _navigator = MockRepository.GenerateMock<INavigator>();
            SimpleIoc.Default.Register(() => _navigator);

            _testViewModel = new PickLocalDataViewModel(_localCurrencyService, _errorHandler);
        }

        [Test]
        public async Task InitialState()
        {
            await _testViewModel.Init();

            Assert.AreEqual(3, _testViewModel.Items.Count);
            Assert.AreEqual("test2", _testViewModel.Items[1].DisplayName);
        }


        [Test]
        public async Task ItemDeleteCommand_ViewRefreshed()
        {
            await _testViewModel.Init();
            _localCurrencyService.ClearBehavior();
            _localCurrencyService.Expect(x => x.DeleteRecordAsync("test2")).Return(Task.CompletedTask);
            _localCurrencyService.Stub(x => x.GetAllRecordsNamesAsync()).Return(Task.FromResult(new List<string> {
                "test1",
                "test3"
            }));

            await _testViewModel.Items[1].RemoveDataRecordCommand.ExecuteAsync();

            Assert.AreEqual(2, _testViewModel.Items.Count);
            Assert.AreEqual("test3", _testViewModel.Items[1].DisplayName);
            _localCurrencyService.VerifyAllExpectations();
        }


        [Test]
        public async Task ItemTapped_Navigated()
        {
            await _testViewModel.Init();
            _navigator.Expect(x => x.PushAsync("localDataDetails", _testViewModel.Items[2])).Return(Task.CompletedTask);

            await _testViewModel.ItemTappedCommand.ExecuteAsync(_testViewModel.Items[2]);

            _navigator.VerifyAllExpectations();
        }



    }
}
