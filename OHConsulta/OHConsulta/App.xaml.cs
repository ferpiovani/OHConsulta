using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace OHConsulta
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
 /*           DTO.Config cfg = new DAL.Config().PesquisarPorId(1);
            if (cfg != null && cfg.ConfigLogou)
            {
                MainPage = new NavigationPage(new OHConsulta.UI.Paginas.TanquesLista());
            } 
            else
            {*/
                MainPage = new NavigationPage(new OHConsulta.UI.Paginas.Login())
                {
                    BarBackgroundColor = Color.FromHex("#290e72")
                };
            
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
