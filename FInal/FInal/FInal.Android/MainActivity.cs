﻿using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Java.Security;
using Plugin.FacebookClient;
using Prism;
using Prism.Ioc;
using System;

namespace FInal.Droid
{
    [Activity(Theme = "@style/MainTheme",
              ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            FacebookClientManager.Initialize(this);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
#if DEBUG
            GetAppHash();
#endif
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(true);
            LoadApplication(new App(new AndroidInitializer()));
        }
        private void GetAppHash()
        {
            try
            {
                PackageInfo info = Application.Context.PackageManager.GetPackageInfo(Application.Context.PackageName, PackageInfoFlags.Signatures);
                foreach (Android.Content.PM.Signature signature in info.Signatures)
                {
                    MessageDigest md = MessageDigest.GetInstance("SHA");
                    md.Update(signature.ToByteArray());

                    var hash = Convert.ToBase64String(md.Digest());
                }
            }
            catch (NoSuchAlgorithmException e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            FacebookClientManager.OnActivityResult(requestCode, resultCode, data);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);

        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register any platform specific implementations
        }
    }
}

