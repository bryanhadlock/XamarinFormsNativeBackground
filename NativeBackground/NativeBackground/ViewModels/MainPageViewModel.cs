using Acr.UserDialogs;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
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
			PushRandomCommand = new Command(() => Push());

			MessagingCenter.Subscribe<IUploadStateMessenger, string>(this, "Running", async (sender, id) =>
			{
				await UserDialogs.AlertAsync($"Message received {id}", "Running", "OK");

			});

			MessagingCenter.Subscribe<IUploadStateMessenger, string>(this, "Blocked", async (sender, id) =>
			{
				await UserDialogs.AlertAsync($"Message received {id}", "Blocked", "OK");
			});

			MessagingCenter.Subscribe<IUploadStateMessenger, string>(this, "Enqueued", async (sender, id) =>
			{
				await UserDialogs.AlertAsync($"Message received {id}", "Enqueued", "OK");
			});

			MessagingCenter.Subscribe<IUploadStateMessenger, string>(this, "Cancelled", async (sender, id) =>
			{
				await UserDialogs.AlertAsync($"Message received {id}", "Cancelled", "OK");
			});

			MessagingCenter.Subscribe<IUploadStateMessenger, string>(this, "Failed", async (sender, id) =>
			{
				await UserDialogs.AlertAsync($"Message received {id}", "Failed", "OK");
			});

			MessagingCenter.Subscribe<IUploadStateMessenger, string>(this, "Succeeded", async (sender, id) =>
			{
				await UserDialogs.AlertAsync($"Message received {id}", "Succeeded", "OK");
			});
		}

		public ICommand PushRandomCommand { get; }

		public void Push()
		{
			RandomUploadService.StartUploadForIdAsync(Guid.NewGuid());
		}


		

	}
}
