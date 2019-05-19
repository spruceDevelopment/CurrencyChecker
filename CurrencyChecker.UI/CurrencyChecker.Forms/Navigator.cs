
using CurrencyChecker.Core.Contracts;
using CurrencyChecker.Core.ViewModels;
using CurrencyChecker.Forms.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CurrencyChecker.Forms
{
    public class Navigator : INavigator
    {
        private readonly INavigation _navigation;
        private Dictionary<string, Type> _pagesDictionary = new Dictionary<string, Type>();

        public Navigator(INavigation navigation)
        {
            _navigation = navigation;
            _pagesDictionary.Add("details", typeof(DetailsPage));
            _pagesDictionary.Add("pickLocalData", typeof(PickLocalDataPage));
            //_pagesDictionary.Add("localDataDetails", typeof(?));

        }

        public async Task GoBackAsync()
        {
            await _navigation.PopAsync();
        }

        public async Task PushAsync(string pageKey, BaseViewModel? viewModel)
        {
            if (!string.IsNullOrWhiteSpace(pageKey))
            {
                Page page;
                if (viewModel != null)
                    page = (Page)Activator.CreateInstance(_pagesDictionary[pageKey], args: viewModel);
                else
                    page = (Page)Activator.CreateInstance(_pagesDictionary[pageKey]);
                await _navigation.PushAsync(page);
                if(page?.BindingContext is BaseViewModel bvm)
                    bvm?.Init();
            }
        }

        
    }
}
