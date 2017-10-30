using Apache.NMS;
using Apache.NMS.ActiveMQ;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrmServer
{
    public partial class Publish : Form
    {
        public Publish()
        {
            InitProducer();
            InitializeComponent();
        }

        private IConnectionFactory factory;

        public void InitProducer()
        {
            try
            {
                //初始化工厂，这里默认的URL是不需要修改的
                factory = new ConnectionFactory("tcp://localhost:61616");

            }
            catch
            {
                this.label1.Text = "初始化失败!!";
            }
        }

        private void Publish_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (IConnection connection = factory.CreateConnection())
                {
                    //Create the Session  
                    using (ISession session = connection.CreateSession())
                    {
                        //Create the Producer for the topic/queue  
                        IMessageProducer prod = session.CreateProducer(
                            new Apache.NMS.ActiveMQ.Commands.ActiveMQTopic("chat.general"));
                        ITextMessage msg = prod.CreateTextMessage();
                        msg.Text = this.textBox1.Text;
                        prod.Send(msg, Apache.NMS.MsgDeliveryMode.NonPersistent, Apache.NMS.MsgPriority.Normal, TimeSpan.MinValue);
                        this.textBox1.Text = "";
                        this.textBox1.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}", ex.Message);
            }
        }

        void dosomething()
        {

        }

    }
}
