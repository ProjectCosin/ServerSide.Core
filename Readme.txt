欢迎访问 COSIN开发者社区 ： http://connect.cosin.io/forum.php?mod=forumdisplay&fid=63

ServerSide.Core 这个REPO主要包含了针对CCU设备与Server端进行数据交互的所有源码。此系统基于Microsoft .Net Framework平台，以C#为开发语言。开发者可以将这套源码架设在自己的服务器端，在CCU端的源码中，同样将ServerPath更改成自己服务器端的地址，可实现私有物联云。（本REPO中不含数据库） 

下载源码后，请先将项目工程目录中 Web.Config文件里的数据库连接字符串，更换成您自己的数据库服务器连接字符串。详见：

<connectionStrings>
		<clear/>
		<add name="CENTRAL" connectionString="Data Source=.;Initial Catalog=DB_COS_CENTRAL;uid=;password=" providerName="System.Data.SqlClient"/>
		<add name="PROPERTY" connectionString="Data Source=.;Initial Catalog=DB_COS_PROPERTY;uid=;password=" providerName="System.Data.SqlClient"/>
</connectionStrings>

<appSettings>
        <clear/>
        <add key="DB_COS_CENTRAL_connstr" value="Data Source=.;Initial Catalog=DB_COS_CENTRAL;uid=;password=" />
        <add key="DB_COS_PROPERTY_connstr" value="Data Source=.;Initial Catalog=DB_COS_PROPERTY;uid=;password=" />
        <add key="CENTRAL" value="Data Source=.;Initial Catalog=DB_COS_CENTRAL;uid=;password="/>
        <add key="PROPERTY" value="Data Source=.;Initial Catalog=DB_COS_PROPERTY;uid=;password="/>
</appSettings>

