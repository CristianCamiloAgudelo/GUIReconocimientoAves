using Plugin.Media;
using Plugin.Media.Abstractions;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GUIReconocimientoAves.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Inicio : ContentPage
    {
        public Inicio()
        {
            InitializeComponent();
        }

        private async void TomarFoto_OnClicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable ||
                !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No hay Camara", " Camara no disponible", "OK");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(
                new StoreCameraMediaOptions
                {
                    SaveToAlbum = true,
                    //Directory = "images",
                    //Name = "test.jpg"
                }
                );

            if (file == null)
            {
                return;
            }

            PathLabel.Text = file.AlbumPath;

            MainImage.Source = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    file.Dispose();
                    return stream;
                }
             );
        }

        private async void ElegirFoto_OnClicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Ooops", " La foto elegida no es soportada !", "OK");
                return;
            }

            var file = await CrossMedia.Current.PickPhotoAsync();

            if (file == null)
            {
                return;
            }

            PathLabel.Text = "Ruta de la imagen: " + file.Path;

            MainImage.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                file.Dispose();
                return stream;
            }
             );
        }

        private async void TomarVideo_OnClicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable ||
                !CrossMedia.Current.IsTakeVideoSupported)
            {
                await DisplayAlert("No hay Camara", " Camara no disponible", "OK");
                return;
            }

            var file = await CrossMedia.Current.TakeVideoAsync(
                new StoreVideoOptions
                {
                    SaveToAlbum = true,
                    Quality = VideoQuality.Medium,
                    //Directory = "videos",
                    //Name = "test.mp4"
                }
                );

            if (file == null)
            {
                return;
            }

            PathLabel.Text = "Ruta video: " + file.Path;

            MainImage.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                file.Dispose();
                return stream;
            }
            );
        }

    }
}