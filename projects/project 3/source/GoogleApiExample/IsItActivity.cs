﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace CameraSkills
{
    [Activity(Label = "IsItActivity")]
    public class IsItActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.IsThis);
            //string isIt = Intent.GetStringExtra("isIt");
            

            //var txtName = FindViewById<TextView>(Resource.Id.isThis);
            //var yesbtn = FindViewById<Button>(Resource.Id.ybtn);
            //var nobtn = FindViewById<Button>(Resource.Id.nbtn);

            //txtName.Text = isIt;

            //var intent = new Intent(this, typeof(IsItActivity));
            //intent.PutExtra("apiResult", apiResult);
            //StartActivity(intent);



        }
    }
}