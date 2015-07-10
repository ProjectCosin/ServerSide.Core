using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using com.cooshare.os;
using com.cooshare.os.sec;

[WebService(Namespace = "http://com.cooshare.os")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]

public class HCCU : System.Web.Services.WebService
{
    public HCCU()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string Service_ReadMe() {
        return "COS V1.0 WebService For com.cooshare.os.COS_WEBSERVICE_HCCU";
    }

    /// <summary>
    /// 注册HCCU账户
    /// </summary>
    /// <param name="HCCU_UID">HCCU用户名</param>
    /// <param name="HCCU_PSW">HCCU密码</param>
    /// <param name="HCCU_MAC_ID">HCCU MAC地址唯一标识</param>
    /// <param name="SK">安全码</param>
    /// <returns>
    /// 返回新注册账户的HCCU_ID 
    /// 返回值为0 说明用户名已被注册 
    /// 返回值为-1 说明方法执行异常 
    /// 返回值为-2 说明参数不符合标准 
    /// 返回值为-4 说明安全验证失败
    /// </returns>
    [WebMethod]
    public string HCCU_Register(string HCCU_UID,string HCCU_PSW,string HCCU_MAC_ID,string SK) {

        string[,] p = new string[2, 3];
        p[0, 0] = "HCCU_UID";
        p[1, 0] = HCCU_UID;
        p[0, 1] = "HCCU_PSW";
        p[1, 1] = HCCU_PSW;
        p[0, 2] = "HCCU_MAC_ID";
        p[1, 2] = HCCU_MAC_ID;

        if (!COS_SECURITY_TOOL.SECURITY_RequestDecrypt(p, SK)) return  COS_SECURITY_TOOL.SECURITY_ContentEncrypt("-4");

        HCCU_UID = COS_SECURITY_TOOL.SECURITY_ContentDecrypt(HCCU_UID);
        HCCU_MAC_ID = COS_SECURITY_TOOL.SECURITY_ContentDecrypt(HCCU_MAC_ID);


        COS_WEBSERVICE_HCCU cos_w_h = new COS_WEBSERVICE_HCCU();
        return  COS_SECURITY_TOOL.SECURITY_ContentEncrypt(cos_w_h.HCCU_Register(HCCU_UID, HCCU_PSW, HCCU_MAC_ID).ToString());
    }

    /// <summary>
    /// 登录HCCU账户
    /// </summary>
    /// <param name="HCCU_UID">HCCU用户名</param>
    /// <param name="HCCU_PSW">HCCU密码</param>
    /// <param name="HCCU_MAC_ID">HCCU MAC地址唯一标识</param>
    /// <param name="SK">安全码</param>
    /// <returns>
    /// 返回值为 HCCU_ID(Pending) 
    /// 返回值为0 说明用户名或密码错误 
    /// 返回值为-1 说明方法执行异常 
    /// 返回值为-2 说明参数不符合标准 
    /// 返回值为-4 说明安全验证失败
    /// </returns>
    [WebMethod]
    public string HCCU_Login(string HCCU_UID, string HCCU_PSW, string HCCU_MAC_ID,string SK) {

        string[,] p = new string[2, 3];
        p[0, 0] = "HCCU_UID";
        p[1, 0] = HCCU_UID;
        p[0, 1] = "HCCU_PSW";
        p[1, 1] = HCCU_PSW;
        p[0, 2] = "HCCU_MAC_ID";
        p[1, 2] = HCCU_MAC_ID;

        if (!COS_SECURITY_TOOL.SECURITY_RequestDecrypt(p, SK)) return  COS_SECURITY_TOOL.SECURITY_ContentEncrypt("-4");

        HCCU_UID = COS_SECURITY_TOOL.SECURITY_ContentDecrypt(HCCU_UID);
        HCCU_MAC_ID = COS_SECURITY_TOOL.SECURITY_ContentDecrypt(HCCU_MAC_ID);

        COS_WEBSERVICE_HCCU cos_w_h = new COS_WEBSERVICE_HCCU();
        return  COS_SECURITY_TOOL.SECURITY_ContentEncrypt(cos_w_h.HCCU_Login(HCCU_UID, HCCU_PSW, HCCU_MAC_ID));
    }


    /// <summary>
    /// 更换HCCU用户密码
    /// </summary>
    /// <param name="HCCU_ID">HCCU_ID编码</param>
    /// <param name="Old_Password">老的HCCU用户密码</param>
    /// <param name="New_Password">新的HCCU用户密码</param>
    /// <param name="SK">安全码</param>
    /// <returns>
    /// 返回值为1 说明修改成功 
    /// 返回值为0 说明原用户名或密码错误 
    /// 返回值为-1 说明方法执行异常 
    /// 返回值为-2 说明参数不符合标准 
    /// 返回值为-4 说明安全验证失败
    /// </returns>
    [WebMethod]
    public string HCCU_ChangePassword(string HCCU_ID, string Old_Password, string New_Password,string SK)
    {
        string[,] p = new string[2, 3];
        p[0, 0] = "HCCU_ID";
        p[1, 0] = HCCU_ID;
        p[0, 1] = "Old_Password";
        p[1, 1] = Old_Password;
        p[0, 2] = "New_Password";
        p[1, 2] = New_Password;

        if (!COS_SECURITY_TOOL.SECURITY_RequestDecrypt(p, SK)) return  COS_SECURITY_TOOL.SECURITY_ContentEncrypt("-4");

        HCCU_ID = COS_SECURITY_TOOL.SECURITY_ContentDecrypt(HCCU_ID);


        COS_WEBSERVICE_HCCU cos_w_h = new COS_WEBSERVICE_HCCU();
        return COS_SECURITY_TOOL.SECURITY_ContentEncrypt(cos_w_h.HCCU_ChangePassword(HCCU_ID, Old_Password, New_Password)); 
    }

    /// <summary>
    /// 获取HCCU及旗下所有EP的信息
    /// </summary>
    /// <param name="HCCU_ID">HCCU_ID编码</param>
    /// <param name="HCCU_MAC">HCCU_MAC</param>
    /// <param name="SK">安全码</param>
    /// <returns>
    /// 返回值为 MAC,IP,PORT,HCCU_ACC_STATUS^EP_ID,EP_TYPEID,EP_USERDEFINED_ALIAS,EP_PRODUCTID,EP_MAC_ID|{Repeart}
    /// 返回值为 -1 说明方法执行异常 
    /// 返回值为 -2 说明参数不符合标准 
    /// 返回值为 -4 说明安全验证失败
    /// </returns>
    [WebMethod]
    public string HCCU_EP_Get_Info(string HCCU_ID,string HCCU_MAC,string SK)
    {

        string[,] p = new string[2, 2];
        p[0, 0] = "HCCU_ID";
        p[1, 0] = HCCU_ID;
        p[0, 1] = "HCCU_MAC";
        p[1, 1] = HCCU_MAC;


        if (!COS_SECURITY_TOOL.SECURITY_RequestDecrypt(p, SK)) return  COS_SECURITY_TOOL.SECURITY_ContentEncrypt("-4");

        HCCU_ID = COS_SECURITY_TOOL.SECURITY_ContentDecrypt(HCCU_ID);
        HCCU_MAC = COS_SECURITY_TOOL.SECURITY_ContentDecrypt(HCCU_MAC);

        COS_WEBSERVICE_HCCU cos_w_h = new COS_WEBSERVICE_HCCU();
        COS_WEBSERVICE_EP cos_w_e = new COS_WEBSERVICE_EP();

        return COS_SECURITY_TOOL.SECURITY_ContentEncrypt(cos_w_h.HCCU_Get_Info(HCCU_ID, HCCU_MAC) + "^" + cos_w_e.EP_Sync(HCCU_ID));

    }


    /// <summary>
    /// 获取HCCU的信息
    /// </summary>
    /// <param name="HCCU_ID">HCCU_ID编码</param>
    /// <param name="HCCU_MAC">HCCU_MAC</param>
    /// <param name="SK">安全码</param>
    /// <returns>
    /// 返回值为 MAC,IP,PORT|
    /// 返回值为 -1 说明方法执行异常 
    /// 返回值为 -2 说明参数不符合标准 
    /// 返回值为 -4 说明安全验证失败
    /// </returns>
    [WebMethod]
    public string HCCU_Get_Info(string HCCU_ID, string HCCU_MAC,string SK)
    {

        string[,] p = new string[2, 2];
        p[0, 0] = "HCCU_ID";
        p[1, 0] = HCCU_ID;
        p[0, 1] = "HCCU_MAC";
        p[1, 1] = HCCU_MAC;


        if (!COS_SECURITY_TOOL.SECURITY_RequestDecrypt(p, SK)) return COS_SECURITY_TOOL.SECURITY_ContentEncrypt("-4");

        HCCU_ID = COS_SECURITY_TOOL.SECURITY_ContentDecrypt(HCCU_ID);
        HCCU_MAC = COS_SECURITY_TOOL.SECURITY_ContentDecrypt(HCCU_MAC);

        COS_WEBSERVICE_HCCU cos_w_h = new COS_WEBSERVICE_HCCU();

        return COS_SECURITY_TOOL.SECURITY_ContentEncrypt(cos_w_h.HCCU_Get_Info(HCCU_ID, HCCU_MAC));

    }

   /// <summary>
    /// 更新HCCU设备的网络信息
   /// </summary>
   /// <param name="MAC">HCCU MAC</param>
   /// <param name="IP">HCCU IP地址</param>
   /// <param name="Port">HCCU端口号</param>
   /// <param name="SK">安全码</param>
   /// <returns>
   /// 1：说明更新成功
   /// 0：说明方法执行异常
   /// -2：说明参数不符合规范
   /// -4：说明安全验证失败
   /// </returns>
    [WebMethod]
    public string UpdateHCCUNetworkInfo(string MAC,string IP,string Port,string SK) {

        string[,] p = new string[2, 3];
        p[0, 0] = "MAC";
        p[1, 0] = MAC;
        p[0, 1] = "IP";
        p[1, 1] = IP;
        p[0, 2] = "Port";
        p[1, 2] = Port;

        if (!COS_SECURITY_TOOL.SECURITY_RequestDecrypt(p, SK)) return COS_SECURITY_TOOL.SECURITY_ContentEncrypt("-4");

        MAC = COS_SECURITY_TOOL.SECURITY_ContentDecrypt(MAC);
        IP = COS_SECURITY_TOOL.SECURITY_ContentDecrypt(IP);
        Port = COS_SECURITY_TOOL.SECURITY_ContentDecrypt(Port);

        COS_WEBSERVICE_HCCU cos_w_hccu = new COS_WEBSERVICE_HCCU();
        return COS_SECURITY_TOOL.SECURITY_ContentEncrypt(cos_w_hccu.HCCU_NETWORKINFO_UPLOAD(MAC, IP, Port));
    
    }


    /// <summary>
    /// HCCU掉线通知
    /// </summary>
    /// <param name="MAC_ID">HCCU MAC</param>
    /// <param name="ifLogOutAccount">是否注销帐户 1代表下线且注销，0代表下线</param>
    /// <returns>
    /// 1：说明更新成功
    /// 0：说明方法执行异常
    /// -2：说明参数不符合规范 
    /// -4：说明安全验证失败
    /// </returns>
    [WebMethod]
    public string HCCU_Offline_Notify(string MAC_ID, string ifLogOutAccount,string SK)
    {
        string[,] p = new string[2, 2];
        p[0, 0] = "MAC_ID";
        p[1, 0] = MAC_ID;
        p[0, 1] = "ifLogOutAccount";
        p[1, 1] = ifLogOutAccount;

        if (!COS_SECURITY_TOOL.SECURITY_RequestDecrypt(p, SK)) return COS_SECURITY_TOOL.SECURITY_ContentEncrypt("-4");

        MAC_ID = COS_SECURITY_TOOL.SECURITY_ContentDecrypt(MAC_ID);
        ifLogOutAccount = COS_SECURITY_TOOL.SECURITY_ContentDecrypt(ifLogOutAccount);

        COS_WEBSERVICE_HCCU cos_w_hccu = new COS_WEBSERVICE_HCCU();
        return COS_SECURITY_TOOL.SECURITY_ContentEncrypt(cos_w_hccu.HCCU_Offline_Notify(MAC_ID, ifLogOutAccount));
    
    }


    /// <summary>
    /// HCCU帐户上线通知
    /// </summary>
    /// <param name="HCCU_ID">HCCU_ID</param>
    /// <param name="MAC">MAC</param>
    /// <returns>
    /// 0：代表方法执行异常
    /// 非0正整数：代表HCCU_MAC_ID
    /// -2：说明参数不符合规范 
    /// -4：说明安全验证失败
    /// </returns>
    [WebMethod]
    public string HCCU_Online_Notify(string HCCU_ID, string MAC,string SK)
    {
        string[,] p = new string[2, 2];
        p[0, 0] = "HCCU_ID";
        p[1, 0] = HCCU_ID;
        p[0, 1] = "MAC";
        p[1, 1] = MAC;

        if (!COS_SECURITY_TOOL.SECURITY_RequestDecrypt(p, SK)) return COS_SECURITY_TOOL.SECURITY_ContentEncrypt("-4");

        HCCU_ID = COS_SECURITY_TOOL.SECURITY_ContentDecrypt(HCCU_ID);
        MAC = COS_SECURITY_TOOL.SECURITY_ContentDecrypt(MAC);

        COS_WEBSERVICE_HCCU cos_w_hccu = new COS_WEBSERVICE_HCCU();
        return COS_SECURITY_TOOL.SECURITY_ContentEncrypt(cos_w_hccu.HCCU_Online_Notify(HCCU_ID, MAC));
    
    }


    
}