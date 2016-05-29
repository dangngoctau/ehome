using System;
using Android.App;
using Android.Widget;
using Android.OS;
using uPLibrary.Networking.M2Mqtt;
using System.Threading.Tasks;
using EHome.Fragments;
using RestSharp;

namespace EHome
{
    [Activity(Label = "EHome", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private MqttClient _client;

        int count = 1;

        public MainActivity()
        {
        }

        public async Task<Domain> GetIpAddressInfo()
        {
            var client = new RestClient("http://dns.xedap629.vn");
            var request = new RestRequest(string.Format("domain/{0}/ip", "yennehouse.com"));
            request.AddHeader("Content-Type", "application/json");
            var result = await client.ExecuteGetTaskAsync<Domain>(request);

            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return result.Data;
            }

            return null;
        }

        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.MyButton);

            button.Click += delegate
            {
                var msg = new byte[5];
                msg[0] = 12;
                msg[1] = 1;
                msg[2] = 1;
                msg[3] = 1;
                msg[4] = 0;
                _client.Publish("eventbus", msg);
            };

            Button button1 = FindViewById<Button>(Resource.Id.OffBtn);

            button1.Click += delegate
            {
                var msg = new byte[5];
                msg[0] = 12;
                msg[1] = 1;
                msg[2] = 1;
                msg[3] = 1;
                msg[4] = 1;
                _client.Publish("eventbus", msg);
            };

            var domain = await GetIpAddressInfo();
            if (domain != null)
            {
                var ips = domain.Address.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                _client = new MqttClient(ips[0]);
                _client.Connect("console");
            }

            FragmentTransaction fragmentTx = this.FragmentManager.BeginTransaction();
            var relay = new Relay();
            fragmentTx.Add(Resource.Id.linearLayout1, relay);
            fragmentTx.Commit();
        }

        public class Domain
        {
            public int DomainId { get; set; }
            public string Name { get; set; }
            public string Address { get; set; }
            public DateTime LastUpdated { get; set; }
        }
    }
}

