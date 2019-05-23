using CurrencyChecker.Core.ViewModels;
using Microcharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CurrencyChecker.Forms.Views.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CurrencyDataGridControl : Grid
    {
        public CurrencyDataGridControl()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty ControlViewModelProperty =
            BindableProperty.Create("ControlViewModel", typeof(CurrencyDataGridViewModel), typeof(CurrencyDataGridControl),
            default(CurrencyDataGridViewModel), defaultBindingMode: BindingMode.OneWay, propertyChanged: OnControlViewModelChanged);

        public CurrencyDataGridViewModel ControlViewModel
        {
            get { return (CurrencyDataGridViewModel)GetValue(ControlViewModelProperty); }
            set { SetValue(ControlViewModelProperty, value); }
        }
        private static void OnControlViewModelChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var currencyDataGridControl = (CurrencyDataGridControl)bindable;
           
        }
    }
}