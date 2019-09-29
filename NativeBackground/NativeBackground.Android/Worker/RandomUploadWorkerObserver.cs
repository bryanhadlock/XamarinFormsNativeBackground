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

				if (state == WorkInfo.State.Running)
				{
					MessagingCenter.Send<IUploadStateMessenger, string>(this, "Running", result.Id.ToString());
				}
				else if (state == WorkInfo.State.Blocked)
				{
					MessagingCenter.Send<IUploadStateMessenger, string>(this, "Blocked", result.Id.ToString());
				}
				else if (state == WorkInfo.State.Enqueued)
				{
					MessagingCenter.Send<IUploadStateMessenger, string>(this, "Enqueued", result.Id.ToString());
				}
				else if (state == WorkInfo.State.Cancelled)
				{
					MessagingCenter.Send<IUploadStateMessenger, string>(this, "Cancelled", result.Id.ToString());
				}
				else if (state == WorkInfo.State.Failed)
				{
					MessagingCenter.Send<IUploadStateMessenger, string>(this, "Failed", result.Id.ToString());
				}
				else if (state == WorkInfo.State.Succeeded)
				{
					MessagingCenter.Send<IUploadStateMessenger, string>(this, "Succeeded", result.Id.ToString());
				}
			}
		}
    }
}
