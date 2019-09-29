using System;
using AndroidX.Work;
using Java.Lang;
using Xamarin.Forms;

namespace NativeBackground.Droid.Worker
{
    public class RandomWorkerObserver : Java.Lang.Object, Android.Arch.Lifecycle.IObserver, IUploadStateMessenger
    {
        protected IMessagingCenter MessagingCenter { get; }

        public RandomWorkerObserver()
        {
            MessagingCenter = Xamarin.Forms.MessagingCenter.Instance;
        }

        public void OnChanged(Java.Lang.Object workInfo)
        {
			if (workInfo is AndroidX.Work.WorkInfo result)
			{
				WorkInfo.State state = result.GetState();

				var id = result.OutputData.GetString("id");
				if (state == WorkInfo.State.Running)
				{
					MessagingCenter.Send<IUploadStateMessenger, string>(this, "Running", id);
				}
				else if (state == WorkInfo.State.Blocked)
				{
					MessagingCenter.Send<IUploadStateMessenger, string>(this, "Blocked", id);
				}
				else if (state == WorkInfo.State.Enqueued)
				{
					MessagingCenter.Send<IUploadStateMessenger, string>(this, "Enqueued", id);
				}
				else if (state == WorkInfo.State.Cancelled)
				{
					MessagingCenter.Send<IUploadStateMessenger, string>(this, "Cancelled", id);
				}
				else if (state == WorkInfo.State.Failed)
				{
					MessagingCenter.Send<IUploadStateMessenger, string>(this, "Failed", id);
				}
				else if (state == WorkInfo.State.Succeeded)
				{
					MessagingCenter.Send<IUploadStateMessenger, string>(this, "Succeeded", id);
				}
			}
		}
    }
}
