using FitnessTracker.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace FitnessTracker.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}