using CurrencyChecker.Core.ViewModels;
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
    public class RateViewModelTest
    {
        RateViewModel _rateViewModel;
        [SetUp]
        public void Setup()
        {
            _rateViewModel = new RateViewModel("EUR", "PLN", 4.30 );
        }

        [Test]
        public async Task InitialState()
        {
            await _rateViewModel.Init();

            Assert.AreEqual("4.3000", _rateViewModel.DisplayValue);
        }
    }
}
