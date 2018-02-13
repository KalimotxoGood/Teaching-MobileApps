using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using System.Collections.Generic;
using Android.Content.PM;
using Android.Provider;
using Android.Graphics;
using Java.IO;
using System;

namespace CaseysCamera
{
    [Activity(Label = "CaseysCamera", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        /// <summary>
        /// Used to track the file that we're manipulating between functions
        /// </summary>
        public static Java.IO.File _file;

        /// <summary>
        /// Used to track the directory that we'll be writing to between functions
        /// </summary>
        public static Java.IO.File _dir;
        public static string mCurrentPhotoPath;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            if (IsThereAnAppToTakePictures() == true)
            {
                CreateDirectoryForPictures();
                FindViewById<Button>(Resource.Id.launchCameraButton).Click += TakePicture;
                FindViewById<Button>(Resource.Id.red).Click += NoRed;
                FindViewById<Button>(Resource.Id.blue).Click += NoBlue;
                FindViewById<Button>(Resource.Id.green).Click += NoGreen;
                FindViewById<Button>(Resource.Id.save).Click += SaveThis;
                FindViewById<Button>(Resource.Id.negBlue).Click += NegateBlue;
                FindViewById<Button>(Resource.Id.negGreen).Click += NegateGreen;
                FindViewById<Button>(Resource.Id.negRed).Click += NegateRed;
                //FindViewById<TextView>(Resource.Id.respondText).Click += NoRed;
            }
        }

        /// <summary>
        /// Apparently, some android devices do not have a camera.  To guard against this,
        /// we need to make sure that we can take pictures before we actually try to take a picture.
        /// </summary>
        /// <returns></returns>
        private bool IsThereAnAppToTakePictures()
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            IList<ResolveInfo> availableActivities =
                PackageManager.QueryIntentActivities
                (intent, PackageInfoFlags.MatchDefaultOnly);
            return availableActivities != null && availableActivities.Count > 0;
        }

        /// <summary>
        /// Creates a directory on the phone that we can place our images
        /// </summary>
        private void CreateDirectoryForPictures()
        {
            _dir = new Java.IO.File(
                Android.OS.Environment.GetExternalStoragePublicDirectory(
                    Android.OS.Environment.DirectoryPictures), "CaseysCamera");
            if (!_dir.Exists())
            {
                _dir.Mkdirs();
            }
        }

        private void TakePicture(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            _file = new Java.IO.File(_dir, string.Format("myPhoto_{0}.jpg", System.Guid.NewGuid()));
            //android.support.v4.content.FileProvider
            //getUriForFile(getContext(), "com.mydomain.fileprovider", newFile);
            //FileProvider.GetUriForFile
            intent.PutExtra(MediaStore.ExtraOutput, Android.Net.Uri.FromFile(_file));
            StartActivityForResult(intent, 0);
        }


        //Create the remove red button
        private void NoRed(object sender, System.EventArgs e)
        {


            TextView tv_saved = FindViewById<TextView>(Resource.Id.respondText);
            ImageView imageView = FindViewById<ImageView>(Resource.Id.takenPictureImageView);
            int height = Resources.DisplayMetrics.HeightPixels;
            int width = imageView.Height;
            Android.Graphics.Bitmap bitmap = _file.Path.LoadAndResizeBitmap(width, height);
            Android.Graphics.Bitmap copyBitmap = bitmap.Copy(Android.Graphics.Bitmap.Config.Argb8888, true);
            for (int i = 0; i < copyBitmap.Width; i++)
            {
                for (int j = 0; j < copyBitmap.Height; j++)
                {
                    int p = copyBitmap.GetPixel(i, j);
                    //00000000 00000000 00000000 00000000
                    //long mask = (long)0xFF00FFFF;
                    //p = p & (int)mask;
                    Android.Graphics.Color c = new Android.Graphics.Color(p);
                    c.R = 0;
                    copyBitmap.SetPixel(i, j, c);
                }
            }
            if (bitmap != null)
            {
                imageView.SetImageBitmap(copyBitmap);
                imageView.Visibility = Android.Views.ViewStates.Visible;

            }

            System.GC.Collect();
            tv_saved.SetTextKeepState("Red Removed!");
        }

        private void NoBlue(object sender, System.EventArgs e)
        {
            TextView tv_saved = FindViewById<TextView>(Resource.Id.respondText);
            ImageView imageView = FindViewById<ImageView>(Resource.Id.takenPictureImageView);
            int height = Resources.DisplayMetrics.HeightPixels;
            int width = imageView.Height;
            Android.Graphics.Bitmap bitmap = _file.Path.LoadAndResizeBitmap(width, height);
            Android.Graphics.Bitmap copyBitmap = bitmap.Copy(Android.Graphics.Bitmap.Config.Argb8888, true);
            for (int i = 0; i < copyBitmap.Width; i++)
            {
                for (int j = 0; j < copyBitmap.Height; j++)
                {
                    int p = copyBitmap.GetPixel(i, j);
                    //00000000 00000000 00000000 00000000
                    //long mask = (long)0xFF00FFFF;
                    //p = p & (int)mask;
                    Android.Graphics.Color c = new Android.Graphics.Color(p);
                    c.B = 0;
                    copyBitmap.SetPixel(i, j, c);
                }
            }
            if (bitmap != null)
            {
                imageView.SetImageBitmap(copyBitmap);
                imageView.Visibility = Android.Views.ViewStates.Visible;

            }

            System.GC.Collect();
            tv_saved.SetTextKeepState("Blue Removed!");
        }

        

        private void NoGreen(object sender, System.EventArgs e)
        {


            TextView tv_saved = FindViewById<TextView>(Resource.Id.respondText);
            ImageView imageView = FindViewById<ImageView>(Resource.Id.takenPictureImageView);
            int height = Resources.DisplayMetrics.HeightPixels;
            int width = imageView.Height;
            Android.Graphics.Bitmap bitmap = _file.Path.LoadAndResizeBitmap(width, height);
            Android.Graphics.Bitmap copyBitmap = bitmap.Copy(Android.Graphics.Bitmap.Config.Argb8888, true);
            for (int i = 0; i < copyBitmap.Width; i++)
            {
                for (int j = 0; j < copyBitmap.Height; j++)
                {
                    int p = copyBitmap.GetPixel(i, j);
                    //00000000 00000000 00000000 00000000
                    //long mask = (long)0xFF00FFFF;
                    //p = p & (int)mask;
                    Android.Graphics.Color c = new Android.Graphics.Color(p);
                    c.G = 0;
                    copyBitmap.SetPixel(i, j, c);
                }
            }
            if (bitmap != null)
            {
                imageView.SetImageBitmap(copyBitmap);
                imageView.Visibility = Android.Views.ViewStates.Visible;

            }

            System.GC.Collect();
            tv_saved.SetTextKeepState("Green Removed!");
        }

        private void NegateBlue(object sender, System.EventArgs e)
        {
            TextView tv_saved = FindViewById<TextView>(Resource.Id.respondText);
            ImageView imageView = FindViewById<ImageView>(Resource.Id.takenPictureImageView);
            int height = Resources.DisplayMetrics.HeightPixels;
            int width = imageView.Height;
            Android.Graphics.Bitmap bitmap = _file.Path.LoadAndResizeBitmap(width, height);
            Android.Graphics.Bitmap copyBitmap = bitmap.Copy(Android.Graphics.Bitmap.Config.Argb8888, true);
            for (int i = 0; i < copyBitmap.Width; i++)
            {
                for (int j = 0; j < copyBitmap.Height; j++)
                {
                    int p = copyBitmap.GetPixel(i, j);
                    //00000000 00000000 00000000 00000000
                    //long mask = (long)0xFF00FFFF;
                    //p = p & (int)mask;
                    Color myColor = new Color(copyBitmap.GetPixel(i, j));
                    byte myColorRedValue = myColor.R;
                    byte myColorBlueValue = myColor.B;
                    byte myColorGreenValue = myColor.G;
                    byte myColorAlphaValue = myColor.A;
                    Android.Graphics.Color c = new Android.Graphics.Color(p);
                    c.B = (byte)(255 - myColor);
                    copyBitmap.SetPixel(i, j, c);
                }
            }
            if (bitmap != null)
            {
                imageView.SetImageBitmap(copyBitmap);
                imageView.Visibility = Android.Views.ViewStates.Visible;

            }

            System.GC.Collect();
            tv_saved.SetTextKeepState("Blue Negated!");
        }


        private void NegateGreen(object sender, System.EventArgs e)
        {
            TextView tv_saved = FindViewById<TextView>(Resource.Id.respondText);
            ImageView imageView = FindViewById<ImageView>(Resource.Id.takenPictureImageView);
            int height = Resources.DisplayMetrics.HeightPixels;
            int width = imageView.Height;
            Android.Graphics.Bitmap bitmap = _file.Path.LoadAndResizeBitmap(width, height);
            Android.Graphics.Bitmap copyBitmap = bitmap.Copy(Android.Graphics.Bitmap.Config.Argb8888, true);
            for (int i = 0; i < copyBitmap.Width; i++)
            {
                for (int j = 0; j < copyBitmap.Height; j++)
                {
                    int p = copyBitmap.GetPixel(i, j);
                    //00000000 00000000 00000000 00000000
                    //long mask = (long)0xFF00FFFF;
                    //p = p & (int)mask;
                    Color myColor = new Color(copyBitmap.GetPixel(i, j));
                    //byte myColorRedValue = myColor.R;
                    //byte myColorBlueValue = myColor.B;
                    //byte myColorGreenValue = myColor.G;
                    //byte myColorAlphaValue = myColor.A;
                    Android.Graphics.Color c = new Android.Graphics.Color(p);
                    c.G = (byte)(255 - myColor);
                    copyBitmap.SetPixel(i, j, c);
                }
            }
            if (bitmap != null)
            {
                imageView.SetImageBitmap(copyBitmap);
                imageView.Visibility = Android.Views.ViewStates.Visible;

            }

            System.GC.Collect();
            tv_saved.SetTextKeepState("Green Negated!");
        }

        private void NegateRed(object sender, System.EventArgs e)
        {
            TextView tv_saved = FindViewById<TextView>(Resource.Id.respondText);
            ImageView imageView = FindViewById<ImageView>(Resource.Id.takenPictureImageView);
            int height = Resources.DisplayMetrics.HeightPixels;
            int width = imageView.Height;
            Android.Graphics.Bitmap bitmap = _file.Path.LoadAndResizeBitmap(width, height);
            Android.Graphics.Bitmap copyBitmap = bitmap.Copy(Android.Graphics.Bitmap.Config.Argb8888, true);
            for (int i = 0; i < copyBitmap.Width; i++)
            {
                for (int j = 0; j < copyBitmap.Height; j++)
                {
                    int p = copyBitmap.GetPixel(i, j);
                    //00000000 00000000 00000000 00000000
                    //long mask = (long)0xFF00FFFF;
                    //p = p & (int)mask;
                    Color myColor = new Color(copyBitmap.GetPixel(i, j));
                    byte myColorRedValue = myColor.R;
                    byte myColorBlueValue = myColor.B;
                    byte myColorGreenValue = myColor.G;
                    byte myColorAlphaValue = myColor.A;
                    Android.Graphics.Color c = new Android.Graphics.Color(p);
                    c.R = (byte)(255 - myColor);
                    copyBitmap.SetPixel(i, j, c);
                }
            }
            if (bitmap != null)
            {
                imageView.SetImageBitmap(copyBitmap);
                imageView.Visibility = Android.Views.ViewStates.Visible;

            }

            System.GC.Collect();
            tv_saved.SetTextKeepState("Red Negated!");
        }
        private void SaveThis(object sender, System.EventArgs e)
        {
            TextView tv_saved = FindViewById<TextView>(Resource.Id.respondText);
            //ImageView imageView = FindViewById<ImageView>(Resource.Id.takenPictureImageView);
            //imageView.BuildDrawingCache(true);
            //Bitmap bitmap = imageView.GetDrawingCache(true);
            //_file = new Java.IO.File(_dir, string.Format("myPhoto_{0}.jpg", System.Guid.NewGuid()));
            //ByteArrayOutputStream stream = new ByteArrayOutputStream();
            //bitmap.Compress(Bitmap.CompressFormat.Png, 100, stream);

      
            //imageView.setDrawingCacheEnabled(true);
            //Bitmap bmap = imageView.getDrawingCache();
            //_file = new Java.IO.File(_dir, string.Format("myPhoto_{0}.jpg", System.Guid.NewGuid()));
            //bmap.Save(_file, jpg);

            tv_saved.SetTextKeepState("Take a screenshot to save and share this cool image!");
        }

        // <summary>
        // Called automatically whenever an activity finishes
        // </summary>
        // <param name = "requestCode" ></ param >
        // < param name="resultCode"></param>
        /// <param name="data"></param>
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            //Make image available in the gallery
            Intent mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
            var contentUri = Android.Net.Uri.FromFile(_file);
            mediaScanIntent.SetData(contentUri);
            SendBroadcast(mediaScanIntent);


            // Display in ImageView. We will resize the bitmap to fit the display.
            // Loading the full sized image will consume too much memory
            // and cause the application to crash.
            ImageView imageView = FindViewById<ImageView>(Resource.Id.takenPictureImageView);
            int height = Resources.DisplayMetrics.HeightPixels;
            int width = imageView.Height;
            Android.Graphics.Bitmap bitmap = _file.Path.LoadAndResizeBitmap(width, height);

            
            if (bitmap != null)
            {
                imageView.SetImageBitmap(bitmap);
                imageView.Visibility = Android.Views.ViewStates.Visible;
                
            }

            // Dispose of the Java side bitmap.
            System.GC.Collect();
        }
    }
}

