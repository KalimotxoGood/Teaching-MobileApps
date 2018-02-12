using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using Android.Net;

namespace cameraAppCasey2
{
    using Android.Content.PM;
    using Android.Graphics;
    using Android.Provider;
    using Java.IO;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Environment = Android.OS.Environment;
    using Uri = Android.Net.Uri;
    public static class App
    {
        public static File _file;
        public static File _dir;
        public static Bitmap bitmap;
    }


    [Activity(Label = "cameraAppCasey2", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private ImageView _imageView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            if (IsThereAnAppToTakePictures())
            {
                CreateDirectoryForPictures();
                Button button = FindViewById<Button>(Resource.Id.launchCamera);
                _imageView = FindViewById<ImageView>(Resource.Id.imageTaken);
                button.Click += TakeAPicture;
            }


        }
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            Intent mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
            Uri contentUri = Uri.FromFile(App._file);
            mediaScanIntent.SetData(contentUri);
            SendBroadcast(mediaScanIntent);
            int height = Resources.DisplayMetrics.HeightPixels;
            int width = _imageView.Height;
            App.bitmap = App._file.Path.LoadAndResizeBitmap(width, height);
            if (App.bitmap != null)
            {
                _imageView.SetImageBitmap(App.bitmap);
                App.bitmap = null;
            }
            GC.Collect();

        } 

        private void CreateDirectoryForPictures()
        {
            App._dir = new File(
                Environment.GetExternalStoragePublicDirectory(
                    Environment.DirectoryPictures), "CameraAppDemo");
            if (!App._dir.Exists())
            {
                App._dir.Mkdirs();
            }
        }
        private bool IsThereAnAppToTakePictures()
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            IList <ResolveInfo> availableActivities =
                PackageManager.QueryIntentActivities(intent, Android.Content.PM.PackageInfoFlags.MatchDefaultOnly);
            return availableActivities != null && availableActivities.Count > 0;
        }
        private void TakeAPicture(object sender, EventArgs eventArgs)
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            App._file = new File(App._dir, string.Format("myPhoto_{0}.jpg", Guid.NewGuid()));
            intent.PutExtra(MediaStore.ExtraOutput, Uri.FromFile(App._file));
            StartActivityForResult(intent, 0);
        }


    }
}

