using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using System.Collections.Generic;
using Android.Content.PM;
using Android.Provider;
using Google.Apis.Vision.v1.Data;
using System;

namespace GoogleApiExample
{
    [Activity(Label = "GoogleApiExample", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            if (IsThereAnAppToTakePictures() == true)
            {
                FindViewById<Button>(Resource.Id.launchCameraButton).Click += TakePicture;

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


        private void TakePicture(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            StartActivityForResult(intent, 100);
            //SetContentView(Resource.Layout.IsThis); This causes onClick to be unhandled.
        }
        
        //Success Activity will prompt the user of the success and give the option to replay
        private void SuccessActivity(object sender, System.EventArgs e)
        {
            SetContentView(Resource.Layout.Success);
            FindViewById<Button>(Resource.Id.cancelButton).Click += OnCancelClick;

        }

        private void DarnActivityClick(object sender, System.EventArgs e)
        {
            SetContentView(Resource.Layout.Darn);
            var submitBtn = FindViewById<Button>(Resource.Id.submit);
            //string theAnswer = FindViewById<EditText>(Resource.Id.answer).Text;
            FindViewById<Button>(Resource.Id.cancelButton).Click += OnCancelClick;
            FindViewById<Button>(Resource.Id.submit).Click += InGoogle;
            

            

        }

        //This is the
        private void InGoogle(object sender, System.EventArgs e)
        {
            var intent = new Intent();
            string theAnswer = FindViewById<EditText>(Resource.Id.answer).Text;
            //intent.PutExtra("newAnswer", theAnswer);
            //
            // Send the result code and data back (this does not end the current Activity)
            //
            SetResult(Result.FirstUser, intent);


            SetContentView(Resource.Layout.GoogleResponse);
            var googlyResponse = FindViewById<TextView>(Resource.Id.googleResponse);
            FindViewById<Button>(Resource.Id.cancelButton).Click += OnCancelClick;
            googlyResponse.Text = theAnswer;

            
            // create the google api result again and recall it.
            //var apiResult = client.Images.Annotate(batch).Execute();
            //String whatBe = "Is this a " + apiResult.Responses[0].LabelAnnotations[0].Description + " ?!";
        }


        void OnCancelClick(object sender, EventArgs e)
        {
            //
            // If this were all not in the same activity then we would end the current Activity.
            // However, we will just set ContentView back to main if user wishes to cancel!
            // The Result Code will default to Result.Canceled.
            //
            var tempBundle = new Bundle();
            OnCreate(tempBundle);
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
            if (requestCode==100 )
            {
                SetContentView(Resource.Layout.IsThis);



            }
            if (resultCode == Result.FirstUser)
            {
                data.GetStringExtra("newAnswer");
                var myintent = new Intent(this, typeof(InGoogle));
                StartActivity(myintent);
            }
            // Display in ImageView. We will resize the bitmap to fit the display.
            // Loading the full sized image will consume too much memory
            // and cause the application to crash.
            ImageView imageView = FindViewById<ImageView>(Resource.Id.takenPictureImageView);
            //TextView googleResponse = FindViewById<TextView>(Resource.Id.whatBe);
            //TextView googleResp1 = FindViewById<TextView>(Resource.Id.whatBe1);
            //TextView googleResp2 = FindViewById<TextView>(Resource.Id.whatBe2);
            //var isItString = FindViewById<TextView>(Resource.Id.isThis); //don't need this var
            int height = Resources.DisplayMetrics.HeightPixels;
            int width = imageView.Height;

            //AC: workaround for not passing actual files
            Android.Graphics.Bitmap bitmap = (Android.Graphics.Bitmap)data.Extras.Get("data");

            //convert bitmap into stream to be sent to Google API
            string bitmapString = "";
            using (var stream = new System.IO.MemoryStream())
            {
                bitmap.Compress(Android.Graphics.Bitmap.CompressFormat.Jpeg, 100, stream);

                var bytes = stream.ToArray();
                bitmapString = System.Convert.ToBase64String(bytes);
            }

            //credential is stored in "assets" folder
            string credPath = "google_api.json";
            Google.Apis.Auth.OAuth2.GoogleCredential cred;

            //Load credentials into object form
            using (var stream = Assets.Open(credPath))
            {
                cred = Google.Apis.Auth.OAuth2.GoogleCredential.FromStream(stream);
            }
            cred = cred.CreateScoped(Google.Apis.Vision.v1.VisionService.Scope.CloudPlatform);

            // By default, the library client will authenticate 
            // using the service account file (created in the Google Developers 
            // Console) specified by the GOOGLE_APPLICATION_CREDENTIALS 
            // environment variable. We are specifying our own credentials via json file.
            var client = new Google.Apis.Vision.v1.VisionService(new Google.Apis.Services.BaseClientService.Initializer()
            {
                ApplicationName = "subtle-isotope-190917",
                HttpClientInitializer = cred
            });

            //set up request
            var request = new Google.Apis.Vision.v1.Data.AnnotateImageRequest();
            request.Image = new Google.Apis.Vision.v1.Data.Image();
            request.Image.Content = bitmapString;

            //tell google that we want to perform label detection
            request.Features = new List<Google.Apis.Vision.v1.Data.Feature>();
            request.Features.Add(new Google.Apis.Vision.v1.Data.Feature() { Type = "LABEL_DETECTION" });
            var batch = new Google.Apis.Vision.v1.Data.BatchAnnotateImagesRequest();
            batch.Requests = new List<Google.Apis.Vision.v1.Data.AnnotateImageRequest>();
            batch.Requests.Add(request);

            //send request.  Note that I'm calling execute() here, but you might want to use
            //ExecuteAsync instead
            var apiResult = client.Images.Annotate(batch).Execute();
           
            //googleResponse.Text = apiResult.Responses[0].LabelAnnotations[0].Description;
            //googleResp1.Text = apiResult.Responses[0].LabelAnnotations[1].Description;
            //googleResp2.Text = apiResult.Responses[0].LabelAnnotations[2].Description;

            String whatBe = "Is this a " + apiResult.Responses[0].LabelAnnotations[0].Description + " at " + apiResult.Responses[0].LabelAnnotations[0].Score + " ?!";
            // turn confidence float into decimal notation and then to string in percentage.

            float percentConfident = (float)apiResult.Responses[0].LabelAnnotations[0].Score * 100;
            int confidence = (int)percentConfident;
            string myConfidence = confidence.ToString() + "&";

            var txtName = FindViewById<TextView>(Resource.Id.isThis);  //these are the variables for the IsThis layout
            var yesbtn = FindViewById<Button>(Resource.Id.ybtn);
            var nobtn = FindViewById<Button>(Resource.Id.nbtn);
            txtName.Text = whatBe;

            var intent = new Intent(this, typeof(IsItActivity));
            intent.PutExtra("apiResult", myConfidence);
            StartActivity(intent);
            MainActivity.


            FindViewById<Button>(Resource.Id.nbtn).Click += DarnActivityClick;
            FindViewById<Button>(Resource.Id.ybtn).Click += SuccessActivity;
            // Below are the button onClick methods to direct to the Succeed or Darn layouts.

            //whatBe = apiResult.Responses[0].LabelAnnotations[0].Description;
            if (bitmap != null)
            {
                imageView.SetImageBitmap(bitmap);
                imageView.Visibility = Android.Views.ViewStates.Visible;
                bitmap = null;
            }

            // Dispose of the Java side bitmap.
            System.GC.Collect();
        }
    }
}

