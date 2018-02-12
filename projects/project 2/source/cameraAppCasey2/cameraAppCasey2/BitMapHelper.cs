using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace cameraAppCasey2
{
    using System.IO;
    using Android.Graphics;
    
    public static class BitMapHelper
    {
        public static Bitmap LoadAndResizeBitmap(this string filename, int width, int height)
        {
            BitmapFactory.Options options = new BitmapFactory.Options
            {
                InJustDecodeBounds = true
            };
            BitmapFactory.DecodeFile(filename, options);
            int outHeight = options.OutHeight;
            int outWidth = options.OutWidth;
            int inSampleSize = 1;
            if (outHeight > height || outWidth > width)
            {
                inSampleSize = outWidth > outHeight ? outHeight / height : outWidth / width;
            }
            options.InSampleSize = inSampleSize;
            options.InJustDecodeBounds = false;
            Bitmap resizedBitmap = BitmapFactory.DecodeFile(filename, options);
            return resizedBitmap;

        }

    }
}