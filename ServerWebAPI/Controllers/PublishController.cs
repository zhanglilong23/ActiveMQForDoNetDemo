using Apache.NMS;
using Apache.NMS.ActiveMQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ServerWebAPI.Controllers
{
    /// <summary>
    /// 发布
    /// </summary>
    public class PublishController : ApiController
    {
        /// <summary>
        /// 给养殖场发送消息
        /// </summary>
        /// <param name="project">项目名称:Collect</param>
        /// <param name="role">角色：Farmer、Collect、Government、Insurance</param>
        /// <param name="addressCode">地址码，空或者null时为全国</param>
        /// <param name="farmerID">养殖户ID</param>
        /// <param name="content">内容</param>
        [HttpGet]
        [Route("api/Publish/Farmer/Send")]
        public void Farmer(string project,string role,string addressCode="",int? farmerID=null,string content="")
        {
            if (string.IsNullOrEmpty(project) || string.IsNullOrEmpty(project))
                throw new Exception("项目与角色不能为空！");
            var urlAddress = project + "/" + role;
            if (!string.IsNullOrEmpty(addressCode))
                urlAddress += "/" + addressCode;
            if (farmerID != null && farmerID > 0)
                urlAddress += "/" + farmerID;

           var factory = new ConnectionFactory("tcp://localhost:61616");
           try
            {
                using (IConnection connection = factory.CreateConnection())
                {
                    //Create the Session  
                    using (ISession session = connection.CreateSession())
                    {
                        //Create the Producer for the topic/queue  
                        IMessageProducer prod = session.CreateProducer(
                            new Apache.NMS.ActiveMQ.Commands.ActiveMQTopic(urlAddress));
                        ITextMessage msg = prod.CreateTextMessage();
                        msg.Text = content;
                        prod.Send(msg, Apache.NMS.MsgDeliveryMode.NonPersistent, Apache.NMS.MsgPriority.Normal, TimeSpan.MinValue);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}", ex.Message);
            }          
                                    
        }
    }
}
