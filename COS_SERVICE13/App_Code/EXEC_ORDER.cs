using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using com.cooshare.os;
using com.cooshare.os.sec;


/// <summary>
///EXEC_ORDER 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class EXEC_ORDER : System.Web.Services.WebService {

    public EXEC_ORDER () {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld() {
        return "COS V1.0 WebService For com.cooshare.os.dev.COS_WEBSERVICE_PROP";
    }


    /// <summary>
    /// 添加待执行指令
    /// </summary>
    /// <param name="EP_ID">指令执行对象EP编码</param>
    /// <param name="PROP">指令属性名</param>
    /// <param name="VALUE">指令属性值</param>
    /// <param name="SK">安全码</param>
    /// <returns>
    /// 返回值为 1 说明添加成功
    /// 返回值为 -1 说明方法执行异常
    /// 返回值为 -2 说明参数不符合标准
    /// 返回值为 -4 说明安全验证失败
    /// </returns>
    [WebMethod]
    public string EXEC_ORDER_Add(string EP_ID, string PROP, string VALUE, string SK)
    {
        string[,] p = new string[2, 3];
        p[0, 0] = "EP_ID";
        p[1, 0] = EP_ID;
        p[0, 1] = "PROP";
        p[1, 1] = PROP;
        p[0, 2] = "VALUE";
        p[1, 2] = VALUE;

        if (!COS_SECURITY_TOOL.SECURITY_RequestDecrypt(p, SK)) return COS_SECURITY_TOOL.SECURITY_ContentEncrypt("-4");

        EP_ID = COS_SECURITY_TOOL.SECURITY_ContentDecrypt(EP_ID);
        PROP = COS_SECURITY_TOOL.SECURITY_ContentDecrypt(PROP);
        VALUE = COS_SECURITY_TOOL.SECURITY_ContentDecrypt(VALUE);

        COS_WEBSERVICE_EXEC_ORDER cos_w_exec = new COS_WEBSERVICE_EXEC_ORDER();
        return COS_SECURITY_TOOL.SECURITY_ContentEncrypt(cos_w_exec.EXEC_ORDER_Add(EP_ID,PROP,VALUE).ToString());
    }



    /// <summary>
    /// 获取待执行指令
    /// </summary>
    /// <param name="EP_ID">指令执行对象EP编码</param>
    /// <returns>
    /// 返回内容格式 {EP_ID},{PROP},{VALUE}|
    /// 返回值为 -1 说明没有待执行指令
    /// 返回值为 -2 说明参数不符合标准
    /// 返回值为 -4 说明安全验证失败
    /// </returns>
    [WebMethod]
    public string EXEC_ORDER_Get(string EP_ID,string SK) {

        string[,] p = new string[2, 1];
        p[0, 0] = "EP_ID";
        p[1, 0] = EP_ID;

        if (!COS_SECURITY_TOOL.SECURITY_RequestDecrypt(p, SK)) return COS_SECURITY_TOOL.SECURITY_ContentEncrypt("-4");

        EP_ID = COS_SECURITY_TOOL.SECURITY_ContentDecrypt(EP_ID);

        COS_WEBSERVICE_EXEC_ORDER cos_w_exec = new COS_WEBSERVICE_EXEC_ORDER();
        return COS_SECURITY_TOOL.SECURITY_ContentEncrypt(cos_w_exec.EXEC_ORDER_Get(EP_ID).ToString());

    }

    /// <summary>
    /// 获取待执行指令
    /// </summary>
    /// <param name="EP_ID">指令执行对象EP编码</param>
    /// <returns>
    /// 返回内容格式 {IP},{PROP},{VALUE}|
    /// 返回值为 -1 说明没有待执行指令
    /// 返回值为 -2 说明参数不符合标准
    /// 返回值为 -4 说明安全验证失败
    /// </returns>
    [WebMethod]
    public string EXEC_ORDER_Get_WithIP(string EP_ID, string SK)
    {

        string[,] p = new string[2, 1];
        p[0, 0] = "EP_ID";
        p[1, 0] = EP_ID;

        if (!COS_SECURITY_TOOL.SECURITY_RequestDecrypt(p, SK)) return COS_SECURITY_TOOL.SECURITY_ContentEncrypt("-4");

        EP_ID = COS_SECURITY_TOOL.SECURITY_ContentDecrypt(EP_ID);

        COS_WEBSERVICE_EXEC_ORDER cos_w_exec = new COS_WEBSERVICE_EXEC_ORDER();
        return COS_SECURITY_TOOL.SECURITY_ContentEncrypt(cos_w_exec.EXEC_ORDER_Get(EP_ID).ToString());

    }
    
}
