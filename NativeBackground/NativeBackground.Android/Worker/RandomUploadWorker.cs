using System;
using System.Diagnostics;
using Android.Content;
using AndroidX.Work;
using Xamarin.Forms;

namespace NativeBackground.Droid.Worker
{
    public class RandomUploadWorker : AndroidX.Work.Worker, IUploadStateMessenger
	{
		protected IMessagingCenter MessagingCenter { get; }
		protected WorkerParameters WorkerParameters { get; }

		public RandomUploadWorker(Context context, WorkerParameters workerParameters) : base(context, workerParameters)
		{
			MessagingCenter = Xamarin.Forms.MessagingCenter.Instance;
			WorkerParameters = workerParameters;
		}

		public override Result DoWork()
		{
			Data taskData = WorkerParameters.InputData;
			var taskDataString = taskData.GetString("id");

			var outputData = new Data.Builder().PutString("id", taskDataString);
			Random random = new Random();
			int val = random.Next(1, 4);
			if (val == 1)
			{
				Debug.WriteLine("Fail");
				return Result.InvokeFailure(outputData.Build());
			}
			if (val == 2)
			{
				Debug.WriteLine("Retry");
				return Result.InvokeRetry();
			}
			Debug.WriteLine("Success");
			return Result.InvokeSuccess(outputData.Build());
		}
	}
}
