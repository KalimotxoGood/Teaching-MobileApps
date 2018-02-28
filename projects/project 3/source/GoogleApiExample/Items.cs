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

namespace CameraSkills
{
   public class Items
    {

        public Items(string thing, string percent)
        {
            Thing = thing;
            Percent = percent;

        }

        public string Thing { get; set; }

        public string Percent { get; set; }

        public override string ToString()
        {
            return Thing;

        }
    }
}