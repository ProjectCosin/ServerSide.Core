using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using com.cooshare.os;
using com.cooshare.os.sec;

/// <summary>
///EVENTS 的摘要说明
/// </summary>
[WebService(Namespace = "http://com.cooshare.os")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class EVENTS : System.Web.Services.WebService {

    public EVENTS () {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string Service_ReadMe()
    {
        return "COS V1.0 WebService For com.cooshare.os.COS_WEBSERVICE_EVENTS";
    }

  
    /// <summary>
    /// 获取某一HCCU下的所有EP事件信息
    /// </summary>
    /// <param name="HCCU_ID">HCCU编码</param>
    /// <param name="SK">安全码</param>
    /// <returns>
    /// 分隔符1：=======A308899ASFFFDSDFAALSDFSFCXXCVASDFASDFAAASDFFD=====
    /// 分隔符2：=======B5526421ASFFFDSDFAALSDFSFCXXCVASDFASDFAAJJID=====
    /// 返回内容格式：EP_ID{分隔符1}PROPERTYNAME{分隔符1}TRIGGERCONTENT{分隔符1}EVENTID{分隔符1}STATUS{分隔符2}
    /// 返回值为 -1 说明无任何EP事件
    /// 返回值为-2 说明参数不符合标准
    /// 返回值为-4 说明安全验证失败
    /// </returns>
    /// </returns>
    [WebMethod]
    public string EVENTS_Sync(string HCCU_ID,string SK) {

        string[,] p = new string[2, 1];
        p[0, 0] = "HCCU_ID";
        p[1, 0] = HCCU_ID;

        if (!COS_SECURITY_TOOL.SECURITY_RequestDecrypt(p, SK)) return COS_SECURITY_TOOL.SECURITY_ContentEncrypt("-4");

        HCCU_ID = COS_SECURITY_TOOL.SECURITY_ContentDecrypt(HCCU_ID);

        COS_WEBSERVICE_EVENTS cos_w_ev = new COS_WEBSERVICE_EVENTS();
        return COS_SECURITY_TOOL.SECURITY_ContentEncrypt(cos_w_ev.EVENTS_Sync(HCCU_ID));
    }


    /// <summary>
    /// 通知服务器Event已同步
    /// </summary>
    /// <param name="epid_Collection">已同步的EVENT_ID序列</param>
    /// <returns>
    /// 1：说明通知成功
    /// -1：说明通知失败
    /// -2：说明参数不符合规范
    /// -4: 说明安全验证失败
    /// </returns>
    [WebMethod]
    public string NotifyEventUpdate(string EVENT_ID_Collection,string SK)
    {
        string[,] p = new string[2, 1];
        p[0, 0] = "EVENT_ID_Collection";
        p[1, 0] = EVENT_ID_Collection;

        if (!COS_SECURITY_TOOL.SECURITY_RequestDecrypt(p, SK)) return COS_SECURITY_TOOL.SECURITY_ContentEncrypt("-4");

        EVENT_ID_Collection = COS_SECURITY_TOOL.SECURITY_ContentDecrypt(EVENT_ID_Collection);

        COS_WEBSERVICE_EVENTS cos_w_ev = new COS_WEBSERVICE_EVENTS();
        return COS_SECURITY_TOOL.SECURITY_ContentEncrypt(cos_w_ev.NotifyEventUpdate(EVENT_ID_Collection));
    }


}
