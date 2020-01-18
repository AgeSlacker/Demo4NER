using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Demo4NER.Models;
using Demo4NER.Services;
using Demo4NER.ViewModels;
using Javax.Xml.Transform;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Demo4NER.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BusinessPage : ContentPage
    {
        public BusinessPageViewModel viewModel;
        public BusinessPage(Business selectedBusiness)
        {
            InitializeComponent();
            BindingContext = viewModel = new BusinessPageViewModel(selectedBusiness);
            viewModel.ReviewsUpdated += ViewModelOnReviewsUpdated;
            viewModel.UserAlreadyReviewed += ViewModelOnUserAlreadyReviewed;
        }

        private void ViewModelOnUserAlreadyReviewed(object sender, EventArgs e)
        {
            DisplayAlert("Erro",
                "Já existe um comentário deste utilizador neste negócio. A funcionalidade de alterar o comentário ainda não está implementada, virá numa versão futura!",
                "Ok");
        }

        private void ViewModelOnReviewsUpdated(object sender, EventArgs e)
        {
            ResizeLists();
        }


        private void EditBusiness(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new EditBusinessPage(viewModel.Business));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ResizeLists();
        }

        private void ResizeLists()
        {
            if (viewModel.Business.Links != null) // TODO make sure it's never null
            {
                int n = viewModel.Business.Links.Count;
                linksView.HeightRequest = n * 30;
            }
            if (viewModel.Business.Reviews != null)
            {
                int nr = viewModel.Business.Reviews.Count;
                ReviewList.HeightRequest = nr * 110;
            }
        }

        private async void OpenURLOnTap(object sender, EventArgs e)
        {
            String url = (sender as Label).Text;
            //Device.OpenUri(new Uri(url));
            if (!url.StartsWith("https://") && !url.StartsWith("http://"))
                url = "https://" + url;
            await Launcher.OpenAsync(url);
        }

        private void OnNavigateClicked(object sender, EventArgs e)
        {
            viewModel.NavigateToMapViewCommand.Execute(null);
        }

        private void PhoneImageButton_OnClicked(object sender, EventArgs e)
        {
            try
            {
                PhoneDialer.Open(viewModel.Business.Contact);
            }
            catch (ArgumentNullException argumentNullException)
            {

            }
            catch (FeatureNotSupportedException featureNotSupportedException)
            {

            }
            catch (Exception exception)
            {

            }
        }
    }
}