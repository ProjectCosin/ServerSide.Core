using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using com.cooshare.os.dev;
using com.cooshare.os.sec;

/// <summary>
///REQUESTLOGS 的摘要说明
/// </summary>
[WebService(Namespace = "http://com.cooshare.os.dev")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class REQUESTLOGS : System.Web.Services.WebService {

    public REQUESTLOGS () {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string Service_ReadMe()
    {
        return "COS V1.0 WebService For com.cooshare.os.dev.COS_WEBSERVICE_REQUESTLOGS";
    }

    /// <summary>
    /// 增加网络请求日志
    /// </summary>
    /// <param name="REQUEST_HCCU_ID">请求主体的HCCU_ID</param>
    /// <param name="REQUEST_IP">请求IP地址</param>
    /// <param name="REQUEST_DATETIME">请求时间</param>
    /// <param name="REQUEST_TYPE_ID">请求类型编码</param>
    /// <param name="REQUEST_RESULT_ID">返回类型编码</param>
    /// <param name="REQUEST_METHOD_NAME">请求方法名称</param>
    /// <param name="SK">安全码</param>
    /// <returns>
    /// 返回值为1 说明添加成功 
    /// 返回值为-1 说明方法执行异常 
    /// 返回值为-2 说明参数不符合标准 
    /// 返回值为-4 说明安全验证失败
    /// </returns>
    [WebMethod]
    public string REQUESTLOGS_Add(string REQUEST_HCCU_ID, string REQUEST_IP, string REQUEST_DATETIME, string REQUEST_TYPE_ID, string REQUEST_RESULT_ID,string REQUEST_METHOD_NAME,string SK)
    {
        string[,] p = new string[2, 6];
        p[0, 0] = "REQUEST_HCCU_ID";
        p[1, 0] = REQUEST_HCCU_ID;
        p[0, 1] = "REQUEST_IP";
        p[1, 1] = REQUEST_IP;
        p[0, 2] = "REQUEST_DATETIME";
        p[1, 2] = REQUEST_DATETIME;
        p[0, 3] = "REQUEST_TYPE_ID";
        p[1, 3] = REQUEST_TYPE_ID;
        p[0, 4] = "REQUEST_RESULT_ID";
        p[1, 4] = REQUEST_RESULT_ID;
        p[0, 5] = "REQUEST_METHOD_NAME";
        p[1, 5] = REQUEST_METHOD_NAME;

        if (!COS_SECURITY_TOOL.SECURITY_RequestDecrypt(p, SK)) return  COS_SECURITY_TOOL.SECURITY_ContentEncrypt("-4");

        REQUEST_HCCU_ID = COS_SECURITY_TOOL.SECURITY_ContentDecrypt(REQUEST_HCCU_ID);
        REQUEST_IP = COS_SECURITY_TOOL.SECURITY_ContentDecrypt(REQUEST_IP);
        REQUEST_DATETIME = COS_SECURITY_TOOL.SECURITY_ContentDecrypt(REQUEST_DATETIME);
        REQUEST_TYPE_ID = COS_SECURITY_TOOL.SECURITY_ContentDecrypt(REQUEST_TYPE_ID);
        REQUEST_RESULT_ID = COS_SECURITY_TOOL.SECURITY_ContentDecrypt(REQUEST_RESULT_ID);

        COS_WEBSERVICE_REQUESTLOGS cos_w_r = new COS_WEBSERVICE_REQUESTLOGS();
        return COS_SECURITY_TOOL.SECURITY_ContentEncrypt(cos_w_r.REQUESTLOGS_Add(REQUEST_HCCU_ID, REQUEST_IP, DateTime.Parse(REQUEST_DATETIME), REQUEST_TYPE_ID, REQUEST_RESULT_ID, REQUEST_METHOD_NAME).ToString());

    }
    
}
