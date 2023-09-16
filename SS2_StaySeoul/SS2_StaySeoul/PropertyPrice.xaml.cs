using SS2_StaySeoul.Model;
using SS2_StaySeoul.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SS2_StaySeoul
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PropertyPricexaml : ContentPage
	{
		ApiService apiService = new ApiService();
		List<PropertyPrice> propertyPrices = new List<PropertyPrice>();
        List<string> rulesList = new List<string>() { "Flexible", "Moderate", "Strict" };
        public static string title;
        public PropertyPricexaml (string itemtitle)
		{
			InitializeComponent ();
            title = itemtitle;
            loadData();
			this.BindingContext = this;
            hoidayPicker.ItemsSource = rulesList;
            normalPicker.ItemsSource = rulesList;
            WeekendPicker.ItemsSource = rulesList;
            contentpage.Title = title + " Prices";
            holidayEntry.Text = "0";
            normalEntry.Text = "0";
            WeekendEntry.Text = "0";
            WeekendPicker.SelectedIndex = 0;
            normalPicker.SelectedIndex = 0;
            hoidayPicker.SelectedIndex = 0;
        }
		public async void loadData()
		{
            fromPicker.MinimumDate = DateTime.Now;
            toPicker.MinimumDate = DateTime.Now;
            PropertyPrice p = new PropertyPrice();
            p.title = title;
            propertyPrices.Clear();
            propertyPrices = await apiService.GetPriceListService(p);
            priceList.ItemsSource = propertyPrices;
            
        }

        private void fromPicker_DateSelected(object sender, DateChangedEventArgs e)
        {
			fromPicker.MaximumDate =  toPicker.Date;
        }

        private  void toPicker_DateSelected(object sender, DateChangedEventArgs e)
        {
			toPicker.MinimumDate =  fromPicker.Date;
            fromPicker.MaximumDate = toPicker.Date;
        }

        private async void SwipeGestureRecognizer_Left(object sender, SwipedEventArgs e)
        {
			string classID =((Grid)(sender)).ClassId;
            PropertyPrice price = new PropertyPrice();
            price.ID =long.Parse(classID);
            string color = ((Grid)(sender)).BackgroundColor.ToHex().ToString();
            Console.WriteLine(color);
            if (color.Equals("#FFBEBBBB"))
            {
                await Application.Current.MainPage.DisplayAlert("Cannot be removed", "Item is booked", "OK");
            }
            else
            {
                string sucessState = await apiService.DeletePropertyPriceService(price);
                if (sucessState == "true")
                {
                    loadData();
                    await Application.Current.MainPage.DisplayAlert("Alert", "Removed!", "OK");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Alert", "Failed to remove data", "OK");
                }
            }
        }

        private async void SwipeGestureRecognizer_Right(object sender, SwipedEventArgs e)
        {
            string classID = ((Grid)(sender)).ClassId;
            string color = ((Grid)(sender)).BackgroundColor.ToHex().ToString();
            if (color == "#FFBEBBBB")
            {
                await Application.Current.MainPage.DisplayAlert("Cannot be removed", "Item is booked", "OK");
            }
            else
            {
                DayOfWeek sunday = DayOfWeek.Sunday;
                DayOfWeek saturday = DayOfWeek.Saturday;
                var selectedPrice = propertyPrices.Where(x => x.ID == long.Parse(classID)).FirstOrDefault();
                if (selectedPrice != null)
                {
                    DateTime date = selectedPrice.date.AddDays(1).Date;
                    toPicker.Date = date;
                    fromPicker.Date = date;
                    if (selectedPrice.iconName == "mug.png")
                    {
                        holidayEntry.Text = selectedPrice.price.ToString();
                        hoidayPicker.SelectedItem = selectedPrice.rules;
                        WeekendEntry.Text = "0";
                        normalEntry.Text = "0";
                        normalEntry.Text = "0";
                        WeekendPicker.SelectedIndex = 0;
                        normalPicker.SelectedIndex = 0;
                    }

                    if (date.DayOfWeek == saturday || date.DayOfWeek == sunday)
                    {
                        WeekendEntry.Text = selectedPrice.price.ToString();
                        WeekendPicker.SelectedItem = selectedPrice.rules;
                        normalPicker.SelectedIndex = 0;
                        hoidayPicker.SelectedIndex = 0;
                        normalEntry.Text = "0";
                        holidayEntry.Text = "0";
                    }
                    else
                    {
                        normalEntry.Text = selectedPrice.price.ToString();
                        normalPicker.SelectedItem = selectedPrice.rules;
                        hoidayPicker.SelectedIndex = 0;
                        WeekendPicker.SelectedIndex =0;
                        WeekendEntry.Text = "0";
                        holidayEntry.Text = "0";
                    }
                }
               
            }
            
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            DateTime startDate = fromPicker.Date;
            DateTime endDate = toPicker.Date;
            TimeSpan dayDifference = endDate-startDate;
            Console.WriteLine(dayDifference.Days);
            for(int i =0; i <=dayDifference.Days; i++)
            {
                ItemPrice iP = new ItemPrice();
                iP.Price = Decimal.Parse(normalEntry.Text);
                iP.itemTitle = title;
                iP.Date = startDate.AddDays(i).Date;
                Console.WriteLine(iP.Date);
                iP.holidayPrice = Decimal.Parse(holidayEntry.Text);
                iP.weekendPrice = Decimal.Parse(WeekendEntry.Text);
                iP.weekendCancellationPolicy = WeekendPicker.SelectedIndex + 1;
                Console.WriteLine(iP.weekendCancellationPolicy);
                iP.normalCancellationPolicy = normalPicker.SelectedIndex + 1;
                iP.weekendCancellationPolicy = WeekendPicker.SelectedIndex + 1;
                iP.holidayCancellationPolicy = hoidayPicker.SelectedIndex + 1;
                string success = await apiService.AddPropertyPriceService(iP);
                if(success == "true")
                {
                    await Application.Current.MainPage.DisplayAlert("Success", "Price has been updated in the list", "OK");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Fail", "Price not added", "OK");
                }
            }
            loadData();
            
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new PropertyPage());
        }
    }
}
