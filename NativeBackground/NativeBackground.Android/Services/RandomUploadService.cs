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

        public Task StartUploadForIdAsync(Guid id)
        {
			Data.Builder data = new Data.Builder();
			data.PutString("id", id.ToString());
			OneTimeWorkRequest taxWorkRequest = OneTimeWorkRequest.Builder.From<RandomUploadWorker>()
				.SetInputData(data.Build())
				.Build();
			WorkManager.Instance.Enqueue(taxWorkRequest);

			ILifecycleOwner owner = ProcessLifecycleOwner.Get();

			//Observe what happened to this request so we can notify the UI
			WorkManager.Instance.GetWorkInfoByIdLiveData(taxWorkRequest.Id).Observe(owner, new RandomWorkerObserver());
			return Task.CompletedTask;
		}
    }
}
