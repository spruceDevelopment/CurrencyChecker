using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CurrencyChecker.Forms.Views;
using GalaSoft.MvvmLight.Ioc;
using CurrencyChecker.Core.Services;
using CurrencyChecker.Core.Contracts;
using CurrencyChecker.Forms.Services;
using CurrencyChecker.Core.ViewModels;
using CurrencyChecker.Core.Helpers;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace CurrencyChecker.Forms
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            ConfigureIoc();

            var mainVm = SimpleIoc.Default.GetInstance<MainViewModel>();
            MainPage = new NavigationPage(new MainPage(mainVm));
            mainVm.Init().FireAndForgetSafeAsync(SimpleIoc.Default.GetInstance<IErrorHandler>());
            SimpleIoc.Default.Register<INavigation>(()=>MainPage.Navigation);
            SimpleIoc.Default.Register<INavigator, Navigator>();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        private void ConfigureIoc()
        {

            SimpleIoc.Default.Register<ILocalCurrencyService, LocalCurrencyService>();
            SimpleIoc.Default.Register<IExternalCurrencyService, ExternalCurrencyService>();
            SimpleIoc.Default.Register<IErrorHandler, AlertErrorHandler>();
            SimpleIoc.Default.Register<ISettingsProvider, ApplicationStorageSettingsProvider>();
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<PickLocalDataViewModel>();
        }
    }
}
