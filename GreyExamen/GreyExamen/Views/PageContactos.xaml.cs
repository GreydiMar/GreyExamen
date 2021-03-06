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

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageContactos());
        }

        private void listacontactos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                Models.Contactos contactos = (Models.Contactos)e.CurrentSelection.FirstOrDefault();
            }
        }

        private void ToolDeleContactos_Clicked(object sender, EventArgs e)
        {

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

            listacontactos.ItemsSource = await App.InitDB.ObtenerListaContactos();
        }

        private async Task<bool> validarFormulario()
        {
            if (string.IsNullOrWhiteSpace(nombre.Text))
            {
                await this.DisplayAlert("Advertencia", "Debe llenar el campo nombre", "ok");
                return false;
            }
            if (string.IsNullOrWhiteSpace(nombre.Text))
            {
                await this.DisplayAlert("Advertencia", "Debe llenar el campo telefono", "ok");
                return false;
            }
            if (string.IsNullOrWhiteSpace(nombre.Text))
            {
                await this.DisplayAlert("Advertencia", "Debe llenar el campo edad", "ok");
                return false;
            }
            if (string.IsNullOrWhiteSpace(nombre.Text))
            {
                await this.DisplayAlert("Advertencia", "Debe llenar el campo nota", "ok");
                return false;
            }
            return true;
        }

        private async void btnagregar_Clicked(object sender, EventArgs e)
        {
            if (await validarFormulario())
            {
                await DisplayAlert("Listo", "Campos llenos", "Ok");
                var contactos = new Contactos()
                {
                    Nombre = nombre.Text,
                    Telefono = (int)Convert.ToDouble(telefono.Text),
                    Longitud = Convert.ToDouble(longitud.Text),
                    Latitud = Convert.ToDouble(latitud.Text),
                    Edad = (int)Convert.ToDouble(edad.Text),
                    Pais = (string)pais.Value,
                    Nota = nota.Text,
                };

                await App.InitDB.AddContactos(contactos);
                if (await App.InitDB.AddContactos(contactos) > 0)
                {
                    await DisplayAlert("Aviso", "Contacto agregado", "Ok");
                }
                else
                {
                    await DisplayAlert("Aviso", "Algo ha salido mal", "Ok");
                }

            }
        }
        private async void btneliminar_Clicked(object sender, EventArgs e)
        {
            var contactos = new Contactos()
            {
                Nombre = nombre.Text,
                Telefono = (int)Convert.ToDouble(telefono.Text),
                Longitud = Convert.ToDouble(longitud.Text),
                Latitud = Convert.ToDouble(latitud.Text),
                Edad = (int)Convert.ToDouble(edad.Text),
                Pais = (string)pais.Value,
                Nota = nota.Text,
            };
            await App.InitDB.DelContactos(contactos);
        }
    }
}