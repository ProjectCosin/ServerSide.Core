��ӭ���� COSIN���������� �� http://connect.cosin.io/forum.php?mod=forumdisplay&fid=63

ServerSide.Core ���REPO��Ҫ���������CCU�豸��Server�˽������ݽ���������Դ�롣��ϵͳ����Microsoft .Net Frameworkƽ̨����C#Ϊ�������ԡ������߿��Խ�����Դ��������Լ��ķ������ˣ���CCU�˵�Դ���У�ͬ����ServerPath���ĳ��Լ��������˵ĵ�ַ����ʵ��˽�������ơ�����REPO�в������ݿ⣩ 

����Դ������Ƚ���Ŀ����Ŀ¼�� Web.Config�ļ�������ݿ������ַ��������������Լ������ݿ�����������ַ����������

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

