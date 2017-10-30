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

namespace FrmClient
{
    public partial class Subscribe : Form
    {
        public Subscribe()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                //Create the Connection factory  
                IConnectionFactory factory = new ConnectionFactory("tcp://localhost:61616/");

                //Create the connection  
                using (IConnection connection = factory.CreateConnection())
                {
                    connection.ClientId = "pubsunListener1";
                    connection.Start();

                    //Create the Session  
                    using (ISession session = connection.CreateSession())
                    {
                        //Create the Consumer  
                        IMessageConsumer consumer = session.CreateDurableConsumer(new Apache.NMS.ActiveMQ.Commands.ActiveMQTopic("pubsub"), "pubsunListener1");

                        //consumer.Listener += new MessageListener(consumer_Listener);
                        //注册监听事件
                        consumer.Listener += new MessageListener(consumer_Listener);

                        Console.ReadLine();
                    }
                    connection.Stop();
                    connection.Close();
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        void consumer_Listener(IMessage message)
        {
            ITextMessage msg = (ITextMessage)message;
            //异步调用下，否则无法回归主线程
            textBox1.Invoke(new DelegateRevMessage(RevMessage), msg);

        }

        public delegate void DelegateRevMessage(ITextMessage message);

        public void RevMessage(ITextMessage message)
        {
            this.textBox1.Text += string.Format(@"接收到:{0}{1}", message.Text, Environment.NewLine);
        }
    }
}
