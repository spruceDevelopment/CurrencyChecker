using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CurrencyChecker.Views;
using GalaSoft.MvvmLight.Ioc;
using CurrencyChecker.Services;
using CurrencyChecker.ViewModels;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace CurrencyChecker
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            ConfigureIoc();

            MainPage = new MainPage();
            SimpleIoc.Default.Register(() => MainPage.Navigation);
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

            SimpleIoc.Default.Register<ICurrencyService, FixerioService>();
            SimpleIoc.Default.Register<CurrentViewModel>();
        }
    }
}
