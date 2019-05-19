
using CurrencyChecker.Core.Contracts;
using CurrencyChecker.Core.ViewModels;
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

        public Navigator(INavigation navigation)
        {
            _navigation = navigation;
        }

        public async Task Push(object page)
        {
            if (page is Page xamPage)
            {
                await _navigation.PushAsync(xamPage);
                if (xamPage.BindingContext is BaseViewModel vm)
                    await vm.Init();
            }
        }
    }
}
