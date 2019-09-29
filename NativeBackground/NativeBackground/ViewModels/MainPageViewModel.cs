using Acr.UserDialogs;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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


		//TODO: Show this list on the screen
		public List<ImageUpload> ImageUploads = new List<ImageUpload>();

		public MainPageViewModel(INavigationService navigationService, IUserDialogs userDialogs, IMessagingCenter messagingCenter, IRandomUploadService randomUploadService)
			: base(navigationService)
		{
			Title = "Main Page";

			UserDialogs = userDialogs;
			MessagingCenter = messagingCenter;
			RandomUploadService = randomUploadService;
			PushRandomCommand = new Command(() => Push());


			// TODO: It is obvious that I only need one of these. Please fixe, I started out with only 2
			MessagingCenter.Subscribe<IUploadStateMessenger, string>(this, Constants.Running, async (sender, id) =>
			{
				await UserDialogs.AlertAsync($"Message received {id}", Constants.Running, "OK");
			});

			MessagingCenter.Subscribe<IUploadStateMessenger, string>(this, Constants.Blocked, async (sender, id) =>
			{
				await UserDialogs.AlertAsync($"Message received {id}", Constants.Blocked, "OK");
			});

			MessagingCenter.Subscribe<IUploadStateMessenger, string>(this, Constants.Enqueued, async (sender, id) =>
			{
				await UserDialogs.AlertAsync($"Message received {id}", Constants.Enqueued, "OK");
			});

			MessagingCenter.Subscribe<IUploadStateMessenger, string>(this, Constants.Cancelled, async (sender, id) =>
			{
				await UserDialogs.AlertAsync($"Message received {id}", Constants.Cancelled, "OK");
			});

			MessagingCenter.Subscribe<IUploadStateMessenger, string>(this, Constants.Failed, async (sender, id) =>
			{
				await UserDialogs.AlertAsync($"Message received {id}", Constants.Failed, "OK");
			});

			MessagingCenter.Subscribe<IUploadStateMessenger, string>(this, Constants.Succeeded, async (sender, id) =>
			{
				var imageUpload = ImageUploads.SingleOrDefault(x => x.TaskId == id);
				if(imageUpload != null)
				{
					await UserDialogs.AlertAsync($"Message received {id}", Constants.Succeeded, "OK");
				}
			});
		}

		public ICommand PushRandomCommand { get; }

		public async Task Push()
		{
			// I don't know the difference between a UUID in android and a GUID in C#.
			// I just want a way to match up the message back with sent data.
			// This needs to be duplicated on iOS
			var localId = Guid.NewGuid();
			var taskId = await RandomUploadService.StartUploadForIdAsync(localId);
			ImageUploads.Add(new ImageUpload { Id = localId, TaskId = taskId });
		}

	}

	public class ImageUpload
	{
		public Guid Id { get; set; }

		// Need to store somewhere
		public string TaskId { get; set; }
	}
}
