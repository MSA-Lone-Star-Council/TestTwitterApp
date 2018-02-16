using Android.App;
using Android.Widget;
using Android.OS;
using Tweetinvi;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using Tweetinvi.Models;

namespace Hello_World
{
    [Activity(Label = "Hello_World", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            List<string> strings = new List<string>
            {
                "test"
            };
            ListView listView = FindViewById<ListView>(Resource.Id.list_view);
            Button button = FindViewById<Button>(Resource.Id.button);
            ArrayAdapter<string> arrayAdapter = new ArrayAdapter<string>(this, Resource.Layout.list, Resource.Id.text_view, strings);
            listView.Adapter = arrayAdapter;
            //Auth.SetApplicationOnlyCredentials(
            //"SPaMo4BqMrz02pokDrWVy96GK",
            // "01cfPNH0hkba8SIzgOQ96M5pDj1itJmTy41IiYpvSOYEu6vXll",
            //"AAAAAAAAAAAAAAAAAAAAAMra2wAAAAAAmKhNN9QZ%2FApXfJI1y1MeDXBqf10%3DpxHG3S1TflbZH0N4aFgEATeupOyAnzU4lH0mnNAdy0Df7jUsWo");
            var creds = new TwitterCredentials("SPaMo4BqMrz02pokDrWVy96GK",
            "01cfPNH0hkba8SIzgOQ96M5pDj1itJmTy41IiYpvSOYEu6vXll", "418354168-mDL5TskWTvJ7pa3fjn5jKvMdwzThnQHa5Q6qutPO", "pmQuHLiMShCtd7PzwBi72lKaXPV2VgTiGwxViFzfihB13")
            {
                ApplicationOnlyBearerToken = "AAAAAAAAAAAAAAAAAAAAAMra2wAAAAAAmKhNN9QZ%2FApXfJI1y1MeDXBqf10%3DpxHG3S1TflbZH0N4aFgEATeupOyAnzU4lH0mnNAdy0Df7jUsWo"
            };
            Auth.SetCredentials(creds);
            var stream = Stream.CreateFilteredStream();
            var user = User.GetUserFromScreenName("alinaqvi97");
            
            stream.AddTrack("#thefour");

            stream.MatchingTweetReceived += (sender, arg) =>
            {
                Console.WriteLine(arg.Tweet.Text);
                strings.Add(arg.Tweet.Text);
            };

            button.Click += delegate
            {
                stream.StopStream();
                arrayAdapter.NotifyDataSetChanged();
                try
                {
                    stream.StartStreamMatchingAnyCondition();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            };
            
        }
    }
}

