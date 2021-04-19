using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
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
    public sealed partial class About : Page
    {
        public About()
        {
            this.InitializeComponent();

            //information on app to display inside the new about screen
            Package package = Package.Current;
            PackageId id = package.Id;
            PackageVersion version = id.Version;
            string nameOfApp = package.DisplayName;
            string pubName = package.PublisherDisplayName;
            appData.Text = string.Format("Application Name: " + nameOfApp +
                "\nPublished by: " + pubName +
                "\nVersion: {0}.{1}.{2}.{3}", version.Major, version.Minor, version.Build, version.Revision+
                "\n Developed by Ashraf, Connor and Charles." +
                "\n Flag image credits: www.creazilla.com" +
                "\n Covid API Data aquired: https://api.opencovid.ca/summary"            );
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            SystemNavigationManager.GetForCurrentView().BackRequested += About_BackRequested;
        }
        private void About_BackRequested(object sender, BackRequestedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
            e.Handled = true;
        }


    }
}
