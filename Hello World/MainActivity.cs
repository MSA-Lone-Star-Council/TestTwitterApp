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
            ArrayAdapter<string> arrayAdapter = new ArrayAdapter<string>(this, Resource.Layout.list, Resource.Id.text_view, strings);
            listView.Adapter = arrayAdapter;
            FetchTweets fetch = new FetchTweets(arrayAdapter);
            fetch.Execute();
        }
    }

    public class FetchTweets : AsyncTask<String, String, String>
    {
        ArrayAdapter<String> arrayAdapter;
        public FetchTweets(ArrayAdapter<String> adapter)
        {
            arrayAdapter = adapter;
        }

        protected override void OnPreExecute()
        {
            var creds = new TwitterCredentials("9F7DBm2zPBJCmdEhO8yUli3XD",
            "NnnokdK9DMmWofBca1cC9k5MevVXeKWh8715lFoVCR3sxtCGX1", "108787452-7MmrOsXkf434VtkfGE4B7nU0xtXRJXLVP3dhOjMI", "lgCEwGrytO5BlvYm1u1RfzXBrGhYr4XGJtFLDksa5pWag")
            {
                ApplicationOnlyBearerToken = "AAAAAAAAAAAAAAAAAAAAAB%2BZ4gAAAAAApjYchPEhQT9fqmGPNzmyYMGjfwI%3DbZqkVAx0Ll8ua6oWaSVczncyXr3q45hNEfgH56F8m6yMnazhGY"
            };
            Auth.SetCredentials(creds);
            base.OnPreExecute();
        }

        protected override void OnProgressUpdate(params string[] values)
        {
            List<String> items = new List<String>(values);
            arrayAdapter.AddAll(items);
            arrayAdapter.NotifyDataSetChanged();
        }

        protected override String RunInBackground(params String[] @params)
        {
            List<string> strings = new List<string>
            {
                "test"
            };
            var stream = Stream.CreateFilteredStream();
            stream.AddTrack("#blackpanther");
            stream.MatchingTweetReceived += (sender, arg) =>
            {
                strings.Add(arg.Tweet.Text);
                PublishProgress(strings.ToArray());
            };
            stream.StartStreamMatchingAnyCondition();

            return null;
        }
    }
}

