using CurrencyChecker.Core.Contracts;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace CurrencyChecker.Forms.Services
{
    public class AlertErrorHandler : IErrorHandler
    {
        public void HandleError(Exception ex)
        {
            Application.Current.MainPage.Navigation.NavigationStack.Last().DisplayAlert("Error",ex.Message, "OK");
        }
    }
}
