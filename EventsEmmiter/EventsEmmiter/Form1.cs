using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EventsSourcing;
using EventsSourcing.Configs;
using EventsSourcing.Helpers;
using EventsSourcing.Models;

namespace EventsEmmiter
{
    public partial class Form1 : Form
    {
        private RabbitMqConnector _rabbitMqConnector;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Task.Run(() => ConnectToRabbitMq());
        }

        private void ConnectToRabbitMq()
        {
            var config = new RabbitMqConfig(
                new RabbitMqInstance[] { new RabbitMqInstance("127.0.0.1", 5672) },
                "user",
                "password",
                new string[] { });

            this._rabbitMqConnector = new RabbitMqConnector(config, new BasicJsonSerializer());
            this._rabbitMqConnector.Initialize();
        }

        private void btnSystemEvent_Click(object sender, EventArgs e)
        {
            this._rabbitMqConnector.Send(new BasicEvent() { Message = this.tbMessage.Text}, "SystemEvents");
        }

        private void btnUserEvent_Click(object sender, EventArgs e)
        {
            this._rabbitMqConnector.Send(new BasicEvent() { Message = this.tbMessage.Text }, "UsersEvents");
        }
    }
}
