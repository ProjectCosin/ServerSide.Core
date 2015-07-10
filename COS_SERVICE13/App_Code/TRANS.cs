using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using com.cooshare.os.dev;
using com.cooshare.os.sec;

/// <summary>
///TRANS 的摘要说明
/// </summary>
[WebService(Namespace = "http://com.cooshare.os.dev")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class TRANS : System.Web.Services.WebService
{

    public TRANS()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string Service_ReadMe()
    {
        return "COS V1.0 WebService For com.cooshare.os.dev.COS_WEBSERVICE_TRANS";
    }

    /// <summary>
    /// 增加控制命令事务日志
    /// </summary>
    /// <param name="TARGET_EP_ID">目标控制EP的EP_ID</param>
    /// <param name="TRIGGER_TYPE_ID">触发类型编码</param>
    /// <param name="MISC">事务日志备注</param>
    /// <param name="DT">指令执行时间</param>
    /// <param name="SK">安全码</param>
    /// <returns>
    /// 返回值为1 说明添加成功 
    /// 返回值为-1 说明方法执行异常 
    /// 返回值为-2 说明参数不符合标准 
    /// 返回值为-4 说明安全验证失败
    /// </returns>
    [WebMethod]
    public string TRANS_Add(string TARGET_EP_ID, string TRIGGER_TYPE_ID, string MISC, string DT, string SK)
    {
        string[,] p = new string[2, 4];
        p[0, 0] = "TARGET_EP_ID";
        p[1, 0] = TARGET_EP_ID;
        p[0, 1] = "TRIGGER_TYPE_ID";
        p[1, 1] = TRIGGER_TYPE_ID;
        p[0, 2] = "MISC";
        p[1, 2] = MISC;
        p[0, 3] = "DT";
        p[1, 3] = DT;

        if (!COS_SECURITY_TOOL.SECURITY_RequestDecrypt(p, SK)) return COS_SECURITY_TOOL.SECURITY_ContentEncrypt("-4");

        TARGET_EP_ID = COS_SECURITY_TOOL.SECURITY_ContentDecrypt(TARGET_EP_ID);
        TRIGGER_TYPE_ID = COS_SECURITY_TOOL.SECURITY_ContentDecrypt(TRIGGER_TYPE_ID);
        MISC = COS_SECURITY_TOOL.SECURITY_ContentDecrypt(MISC);
        DT = COS_SECURITY_TOOL.SECURITY_ContentDecrypt(DT);

        COS_WEBSERVICE_TRANS cos_w_trans = new COS_WEBSERVICE_TRANS();
        return COS_SECURITY_TOOL.SECURITY_ContentEncrypt(cos_w_trans.TRANS_Add(TARGET_EP_ID, TRIGGER_TYPE_ID, MISC, DT).ToString());

    }

}
