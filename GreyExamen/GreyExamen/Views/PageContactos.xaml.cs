using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GreyExamen.Models;
using GreyExamen.Controllers;
using System.IO;

namespace GreyExamen.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageContactos : ContentPage
    {
        public PageContactos()
        {
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();

            CancellationTokenSource cts;

            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location != null)
                {
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                    latitud.Text = Convert.ToString(location.Latitude);
                    longitud.Text = Convert.ToString(location.Longitude);
                }
                else
                {
                    var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                    cts = new CancellationTokenSource();
                    location = await Geolocation.GetLocationAsync(request, cts.Token);

                    if (location != null)
                    {
                        latitud.Text = Convert.ToString(location.Latitude);
                        longitud.Text = Convert.ToString(location.Longitude);
                    }
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                fnsEx.ToString();

            }
        }



        private void btnagregar_Clicked(object sender, EventArgs e, object Contactos)
        {


            var contactos = new Contactos()
            {
                Nombres = nombre.Text,
                Apellidos = apellido.Text,
                Longitud = Convert.ToDouble(longitud.Text),
                Latitud = Convert.ToDouble(latitud.Text),
                Edad = (int)Convert.ToDouble(edad.Text),
                Pais = (string)pais.Value,
                Nota = nota.Text,
            };





            _ = App.InitDB.AddContactos(contactos);
        }
    }
}