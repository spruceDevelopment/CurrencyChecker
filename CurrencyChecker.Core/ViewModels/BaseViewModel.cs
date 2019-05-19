using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;


using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using System.Threading.Tasks;
using CurrencyChecker.Core.Contracts;
using GalaSoft.MvvmLight.Messaging;

namespace CurrencyChecker.Core.ViewModels
{
    public class BaseViewModel : ViewModelBase
    {
        protected INavigator Navigator => SimpleIoc.Default.GetInstance<INavigator>();
        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action? onChanged = null
        )
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            RaisePropertyChanged(propertyName);
            return true;
        }

        virtual public Task Init()
        {
            return Task.CompletedTask;
        }

    }
}
