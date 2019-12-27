# EjectGame
EjectGame 毕业设计

Unity3D+PhotonServer+MySQL开发

需要安装PhotonServer，并将PhotonServerConfig下的MyGameServer文件夹放在 "PhotonServer目录"/deploy/ 下，并在"PhotonServer目录"下的PhotonServer.Config文件完成加上服务器启动的配置。

配置内容如下：

<MyGameInstance
		MaxMessageSize="512000"
		MaxQueuedDataPerPeer="512000"
		PerPeerMaxReliableDataInTransit="51200"
		PerPeerTransmitRateLimitKBSec="256"
		PerPeerTransmitRatePeriodMilliseconds="200"
		MinimumTimeout="5000"
		MaximumTimeout="30000"
		DisplayName="Eject Game"
		>

	     <!-- 0.0.0.0 opens listeners on all available IPs. Machines with multiple IPs should define the correct one here. 
	     Port 5055 is Photon's default for UDP connections.  -->
	    <UDPListeners>
	      	<UDPListener
				IPAddress="0.0.0.0"
				Port="5055"
				OverrideApplication="EjectGame">
	      	</UDPListener>
	    </UDPListeners>

	    <!--  0.0.0.0 opens listeners on all available IPs. Machines with multiple IPs should define the correct one here. 
	     Port 4530 is Photon's default for TCP connecttions. 
	     A Policy application is defined in case that policy requests are sent to this listener (known bug of some some flash clients)  -->
	    <TCPListeners>
	      	<TCPListener
				IPAddress="0.0.0.0"
				Port="4530"
				PolicyFile="Policy\assets\socket-policy.xml"
				InactivityTimeout="10000"
				OverrideApplication="EjectGame"
				>
	      	</TCPListener>
	    </TCPListeners>

	    
	    <!--  Policy request listener for Unity and Flash (port 843) and Silverlight (port 943)   -->
	    <PolicyFileListeners>
	       <!-- multiple Listeners allowed for different ports  -->
	      	<PolicyFileListener
		        IPAddress="0.0.0.0"
		        Port="843"
		        PolicyFile="Policy\assets\socket-policy.xml"
		        InactivityTimeout="10000">
	      	</PolicyFileListener>
	      	<PolicyFileListener
		        IPAddress="0.0.0.0"
		        Port="943"
		        PolicyFile="Policy\assets\socket-policy-silverlight.xml"
		        InactivityTimeout="10000">
	      	</PolicyFileListener>
	    </PolicyFileListeners>

	    <!--  WebSocket (and Flash-Fallback) compatible listener  -->
	    <WebSocketListeners>
	      	<WebSocketListener
		        IPAddress="0.0.0.0"
		        Port="9090"
		        DisableNagle="true"
		        InactivityTimeout="10000"
		        OverrideApplication="EjectGame">
	      	</WebSocketListener>
	    </WebSocketListeners>


	    <!--  Defines the Photon Runtime Assembly to use.  -->
	    <Runtime
			Assembly="PhotonHostRuntime, Culture=neutral"
			Type="PhotonHostRuntime.PhotonDomainManager"
			UnhandledExceptionPolicy="Ignore">
	    </Runtime>


	    <!--  Defines which applications are loaded on start and which of them is used by default. Make sure the default application is defined. 
	     Application-folders must be located in the same folder as the bin_win32 folders. The BaseDirectory must include a "bin" folder.  -->
	    <Applications Default="EjectGame">

	      	<!--  My Game Application  -->
	      	<Application
				Name="EjectGame"
				BaseDirectory="EjectGameServer"
				Assembly="EjectGameServer"
				Type="EjectGameServer.EjectGameServer"
				ForceAutoRestart="true"
				WatchFiles="dll;config"
				ExcludeFiles="log4net.config">
	      	</Application>

	    </Applications>
  	</MyGameInstance>

开场动画

![image](https://github.com/H-J-F/EjectGame/blob/master/ReadmeImages/开场动画.png)

开场动画（遭遇危险）

![image](https://github.com/H-J-F/EjectGame/blob/master/ReadmeImages/开场动画（遭遇危险）.png)

登陆界面

![image](https://github.com/H-J-F/EjectGame/blob/master/ReadmeImages/登陆界面.png)

加载界面

![image](https://github.com/H-J-F/EjectGame/blob/master/ReadmeImages/加载界面.png)

主界面

![image](https://github.com/H-J-F/EjectGame/blob/master/ReadmeImages/主界面.png)

设置界面
![image](https://github.com/H-J-F/EjectGame/blob/master/ReadmeImages/设置界面.png)

更换语言等设置

![image](https://github.com/H-J-F/EjectGame/blob/master/ReadmeImages/更换语言等设置.png)

对战画面

![image](https://github.com/H-J-F/EjectGame/blob/master/ReadmeImages/对战画面.png)

手机端战斗界面

![image](https://github.com/H-J-F/EjectGame/blob/master/ReadmeImages/手机端战斗界面.jpg)

游戏胜利

![image](https://github.com/H-J-F/EjectGame/blob/master/ReadmeImages/游戏胜利.png)

游戏结束

![image](https://github.com/H-J-F/EjectGame/blob/master/ReadmeImages/游戏结束.png)
