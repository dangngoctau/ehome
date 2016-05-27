using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using uPLibrary.Networking.M2Mqtt;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

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
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("http://dns.xedap629.vn");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await client.GetAsync(string.Format("domain/{0}/ip", "yennehouse.com"));
                    response.EnsureSuccessStatusCode();
                    var domainJson = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<Domain>(domainJson);
                }
                catch (HttpRequestException ex)
                {
                    return null;
                }
            }
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
                var msg = new byte[2];
                msg[0] = 1;
                msg[1] = 0;
                _client.Publish("esp1", msg);
            };

            Button button1 = FindViewById<Button>(Resource.Id.OffBtn);

            button1.Click += delegate
            {
                var msg = new byte[2];
                msg[0] = 1;
                msg[1] = 1;
                _client.Publish("esp1", msg);
            };

            var domain = await GetIpAddressInfo();
            if (domain != null)
            {
                var ips = domain.Address.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                _client = new MqttClient(ips[0]);
                _client.Connect("console");
                if (_client.IsConnected)
                {
                    Toast.MakeText(ApplicationContext, "Ready to use", ToastLength.Long);
                }
                else
                {
                    Toast.MakeText(ApplicationContext, "Check network again", ToastLength.Long);
                }
            }
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

