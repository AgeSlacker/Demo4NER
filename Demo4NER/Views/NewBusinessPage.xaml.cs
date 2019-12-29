using Demo4NER.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo4NER.Services;
using Plugin.Media;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Demo4NER.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewBusinessPage : ContentPage
    {
        NewBusinessViewModel viewModel;
        public NewBusinessPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new NewBusinessViewModel();
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            viewModel.CreateNewBusinessCommand.Execute(null);
            Navigation.PopModalAsync();
        }

        private async void ButtonImageSelect_OnClicked(object sender, EventArgs e)
        {
            //(sender as Button).IsEnabled = false;

            //Stream stream = await DependencyService.Get<IPhotoPickerService>().GetImageStreamAsync();
            //MemoryStream memoryStream = new MemoryStream();
            //byte[] buffer = new byte[stream.Length + 10];
            //int numBytesRead = 0;
            //int numBytesToRead = (int)stream.Length;
            //do
            //{
            //    int n = stream.Read(buffer, numBytesRead, 10);
            //    numBytesToRead -= n;
            //    numBytesRead += n;
            //} while (numBytesToRead > 0);
            //stream.Close();
            //if (stream != null)
            //{
            //    businessImage.Source = ImageSource.FromStream(() => new MemoryStream(buffer));
            //}

            ////await stream.CopyToAsync(memoryStream);
            ////businessImage.
            //(sender as Button).IsEnabled = true;

            await CrossMedia.Current.Initialize();
            var image = await CrossMedia.Current.PickPhotoAsync();
            businessImage.Source = ImageSource.FromStream(() => image.GetStream());
            MemoryStream memoryStream = new MemoryStream();
            image.GetStream().CopyTo(memoryStream);
            viewModel.NewBusiness.BusinessImage = memoryStream.ToArray();
            viewModel.NewBusiness.Image = businessImage;
        }
    }

}
