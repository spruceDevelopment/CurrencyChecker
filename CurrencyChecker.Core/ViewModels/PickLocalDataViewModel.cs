using CurrencyChecker.Core.Contracts;
using CurrencyChecker.Core.Helpers;
using CurrencyChecker.Core.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyChecker.Core.ViewModels
{
    public class PickLocalDataViewModel : BaseViewModel
    {
        private readonly ILocalCurrencyService _localCurrencyService;
        private readonly IErrorHandler _errorHandler;

        public ObservableCollection<LocalDataRecordViewModel> Items { get; } = new ObservableCollection<LocalDataRecordViewModel>();

        public IAsyncCommand<LocalDataRecordViewModel> ItemTappedCommand { get; }
        public bool IsEmpty => Items.Count == 0 && !IsBusy;
        public PickLocalDataViewModel(ILocalCurrencyService localCurrencyService, IErrorHandler errorHandler)
        {
            Title = "Dane lokalne";
            Items.CollectionChanged += (a,b) => RaisePropertyChanged(nameof(IsEmpty));
            PropertyChanged += (s, arg) => { if (arg.PropertyName == nameof(IsBusy)) RaisePropertyChanged(nameof(IsEmpty)); };
            _localCurrencyService = localCurrencyService;
            _errorHandler = errorHandler;
            ItemTappedCommand = new AsyncCommand<LocalDataRecordViewModel>(ItemTapped, errorHandler: errorHandler);
        }

        private async Task ItemTapped(LocalDataRecordViewModel arg)
        {
            await Navigator.PushAsync("localDataDetails", arg);
        }

        public override async Task Init()
        {
            try
            {
                IsBusy = true;
                Items.Clear();
                var result = await _localCurrencyService.GetAllRecordsNamesAsync();
                if (result == null)
                    return;
                foreach (var item in result)
                {
                    if (item != null)
                        Items.Add(new LocalDataRecordViewModel(item, this, _errorHandler));
                }
            }
            catch(Exception ex)
            {
                var test = ex.Message;
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async Task RemoveDataRecord(string key)
        {
            try
            {
                IsBusy = true;
                await _localCurrencyService.DeleteRecordAsync(key);
                await Init();
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
