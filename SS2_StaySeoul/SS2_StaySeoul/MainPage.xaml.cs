using SS2_StaySeoul.Model;
using SS2_StaySeoul.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SS2_StaySeoul
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        ApiService apiService = new ApiService();
        private async void Button_Clicked(object sender, EventArgs e)
        {
            User user = new User();
            user.Username = uname.Text;
            user.Password = pwd.Text;
            var checkUser = await apiService.LoginService(user);
            if (checkUser == null)
            {
                errorLbl.IsVisible = true;
            }
            else
            {
                errorLbl.IsVisible = false;
                Application.Current.MainPage = new NavigationPage(new PropertyPage());
            }
        }
    }
}
