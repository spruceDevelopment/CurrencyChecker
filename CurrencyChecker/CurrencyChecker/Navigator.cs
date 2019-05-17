using CurrencyChecker.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CurrencyChecker
{
    public class Navigator
    {
        private readonly INavigation _navigation;

        public Navigator(INavigation navigation)
        {
            _navigation = navigation;
        }

        public async Task Push(Page page)
        {
            await _navigation.PushAsync(page);
            if (page.BindingContext is BaseViewModel vm)
                await vm.Init();
        }
    }
}
