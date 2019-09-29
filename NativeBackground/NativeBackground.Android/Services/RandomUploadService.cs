using System;
using System.Threading.Tasks;
using Android.App;
using Android.Arch.Lifecycle;
using AndroidX.Work;
using NativeBackground.Droid.Worker;

namespace NativeBackground.Droid.Services
{
	public class RandomUploadService : IRandomUploadService
	{
		public RandomUploadService()
		{
		}

		public async Task<string> StartUploadForIdAsync(Guid id)
		{
			Data.Builder data = new Data.Builder();
			data.PutString("id", id.ToString());
			OneTimeWorkRequest oneTimeWorkRequest = OneTimeWorkRequest.Builder.From<RandomUploadWorker>()
				.SetInputData(data.Build())
				.Build();
			WorkManager.Instance.Enqueue(oneTimeWorkRequest);

			ILifecycleOwner owner = ProcessLifecycleOwner.Get();

			//Observe what happened to this request so we can notify the UI
			WorkManager.Instance.GetWorkInfoByIdLiveData(oneTimeWorkRequest.Id).Observe(owner, new RandomWorkerObserver());
			return oneTimeWorkRequest.Id.ToString();
		}
	}
}
