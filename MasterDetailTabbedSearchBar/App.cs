using System;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace MasterDetailTabbedSearchBar
{
    public class App : Xamarin.Forms.Application
    {
        public App()
        {
            var navigationPage = new Xamarin.Forms.NavigationPage(new MySearchPage())
            {
                Title = "Tab 1",
                BarBackgroundColor = Color.SteelBlue,
                BarTextColor = Color.Orange
            };
            navigationPage.On<iOS>().SetPrefersLargeTitles(true);

            var tabbedPage = new Xamarin.Forms.TabbedPage
            {
                Children =
                {
                    navigationPage,
                    new DarkGrayContentPage{ Title = "Tab 2" },
                    new DarkGrayContentPage{ Title = "Tab 3" }
                }
            };
            tabbedPage.On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);

            MainPage = new MasterDetailPage
            {
                Master = new ContentPage
                {
                    Title = "☰",
                    BackgroundColor = Color.LightGray
                },
                Detail = tabbedPage
            };
        }
    }

    public class MySearchPage : DarkGrayContentPage, ISearchPage
    {
        public MySearchPage()
        {
            Title = "Search Page";
            SearchBarTextChanged += HandleSearchBarTextChanged;
        }

        public event EventHandler<string> SearchBarTextChanged;

        public void OnSearchBarTextChanged(string text) => SearchBarTextChanged?.Invoke(this, text);

        void HandleSearchBarTextChanged(object sender, string searchBarText)
        {
            //Logic to handle updated search bar text
        }
    }

    public interface ISearchPage
    {
        void OnSearchBarTextChanged(string text);
        event EventHandler<string> SearchBarTextChanged;
    }

    public class DarkGrayContentPage : ContentPage
    {
        public DarkGrayContentPage() => BackgroundColor = Color.DarkGray;
    }
}

