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
                BarTextColor = Color.Orange
            };
            navigationPage.On<iOS>().SetPrefersLargeTitles(true);

            var tabbedPage = new Xamarin.Forms.TabbedPage
            {
                Children =
                {
                    navigationPage,
                    new ContentPage{ Title = "Tab 2" },
                    new ContentPage{ Title = "Tab 3" }
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

    //how about adding a searchbar on a masterdetail page, with a tabbed page inside and showing
    //the search bar only on 1 of the pages inside the tabbedpage? got this working on Android, but no luk yet with iOS

    public class MySearchPage : ContentPage, ISearchPage
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
}

