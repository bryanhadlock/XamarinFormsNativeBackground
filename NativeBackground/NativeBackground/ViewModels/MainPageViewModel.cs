using Acr.UserDialogs;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace NativeBackground.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        protected IUserDialogs UserDialogs { get; }
        protected IMessagingCenter MessagingCenter { get; }
        protected IRandomUploadService RandomUploadService { get; }

        public MainPageViewModel(INavigationService navigationService, IUserDialogs userDialogs, IMessagingCenter messagingCenter, IRandomUploadService randomUploadService)
            : base(navigationService)
        {
            Title = "Main Page";

            UserDialogs = userDialogs;
            MessagingCenter = messagingCenter;
            RandomUploadService = randomUploadService;
        }
    }
}
