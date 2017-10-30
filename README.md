# ActiveMQForDoNetDemo
ActiveMQ Demo

Server,Client,WebApi 使用TCP方式连接。
Html 使用 stomp 方式连接。

使用方法：
1，去ActiveMQ官网下载 ActiveMQ。连接：http://activemq.apache.org/
2，运行 ActiveMQ bin 文件夹下的exe文件作为服务器。
3，（可选配置）修改ActiveMQ帐号密码。
	在conf中找到Activemq.xml，在标签<broker>中添加一下内容：
		<!-- 添加访问ActiveMQ的账号密码 -->  
        <plugins>  
            <simpleAuthenticationPlugin>  
                <users>  
                    <authenticationUser username="admin" password="123456" groups="users,admins"/>  
                </users>  
            </simpleAuthenticationPlugin>  
        </plugins>  
		
此时，运行程序中对应模块即可。

vs项目中的依赖DLL，已放在DLL文件夹中。

@zhangxiaobai
