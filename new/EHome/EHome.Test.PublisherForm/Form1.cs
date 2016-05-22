using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using uPLibrary.Networking.M2Mqtt;

namespace EHome.Test.PublisherForm
{
    public partial class Form1 : Form
    {
        private MqttClient _client;

        public Form1()
        {
            InitializeComponent();
            _client = new MqttClient("192.168.1.17");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _client.Connect("publishertest");
        }

        private void btnPublish_Click(object sender, EventArgs e)
        {
            var msg = txtMsg.Text;
            var channelId = msg.Substring(0,2).ToString();
            var bytes = UTF8Encoding.UTF8.GetBytes(msg);
            _client.Publish(txtTopic.Text, bytes);
        }
    }
}
