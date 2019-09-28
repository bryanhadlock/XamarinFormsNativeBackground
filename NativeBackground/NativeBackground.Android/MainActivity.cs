﻿using Acr.UserDialogs;
using Android.App;
using Android.Content.PM;
using Android.OS;
using NativeBackground.Droid.Services;
using Prism;
using Prism.Ioc;

namespace NativeBackground.Droid
{
    [Activity(Label = "NativeBackground", Icon = "@mipmap/ic_launcher", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            UserDialogs.Init(this);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App(new AndroidInitializer()));
        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IRandomUploadService, RandomUploadService>();
        }
    }
}

