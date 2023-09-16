using SS2_StaySeoul.Model;
using SS2_StaySeoul.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace SS2_StaySeoul
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PropertyPage : ContentPage
    {
        ApiService apiService = new ApiService();
        List<Property> PropertyList = new List<Property>();
        public static string itemTitle = "";
        public PropertyPage()
        {
            InitializeComponent();
            loadData();
            this.BindingContext = this;
        }
        public async void loadData()
        {
            PropertyList = await apiService.GetPropertyListService();
            propertyList.ItemsSource = PropertyList;
            
        }


        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            string itemTitle = ((ImageButton)(sender)).ClassId;
            Console.WriteLine("The title in property" + itemTitle);
            Application.Current.MainPage = new NavigationPage(new PropertyPricexaml(itemTitle));
        }

        private void ImageButton_Clicked_1(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new MainPage());
        }
    }
}