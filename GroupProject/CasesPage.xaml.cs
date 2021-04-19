using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GroupProject
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CasesPage : Page
    {
        public ProvinceViewModel viewModel { get; set; }
        public CasesPage()
        {
            this.InitializeComponent();
            this.viewModel = new ProvinceViewModel();
        }

        private void BackClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage), viewModel);
        }
        private void ForwardClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(VaccinationPage), viewModel);
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            viewModel = (ProvinceViewModel)e.Parameter;
        }

        private void HomeClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage), viewModel);
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(About));
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Exit();
        }
    }
}
