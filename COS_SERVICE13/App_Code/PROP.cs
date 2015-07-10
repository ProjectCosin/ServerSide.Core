using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using com.cooshare.os.dev;
using com.cooshare.os.sec;

/// <summary>
///PROP 的摘要说明
/// </summary>
[WebService(Namespace = "http://com.cooshare.os.dev")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class PROP : System.Web.Services.WebService {

    public PROP () {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string Service_ReadMe()
    {
        return "COS V1.0 WebService For com.cooshare.os.dev.COS_WEBSERVICE_PROP";
    }

    /// <summary>
    /// 增加EP产品属性
    /// </summary>
    /// <param name="EP_PROPERTY_TABLE_ID">EP产品所对应的属性表ID</param>
    /// <param name="EP_PROPERTY_NAME">EP产品属性名称</param>
    /// <param name="SK">安全码</param>
    /// <returns>
    /// 返回值为1 说明属性添加成功 
    /// 返回值为0 说明该属性已经被添加过 
    /// 返回值为-1 说明方法执行异常 
    /// 返回值为-2 说明参数不符合标准
    /// 返回值为-4 说明安全验证失败
    /// </returns>
    [WebMethod]
    public string PROP_Add(string EP_PROPERTY_TABLE_ID, string EP_PROPERTY_NAME,string SK)
    {
        string[,] p = new string[2, 2];
        p[0, 0] = "EP_PROPERTY_TABLE_ID";
        p[1, 0] = EP_PROPERTY_TABLE_ID;
        p[0, 1] = "EP_PROPERTY_NAME";
        p[1, 1] = EP_PROPERTY_NAME;

        if (!COS_SECURITY_TOOL.SECURITY_RequestDecrypt(p, SK)) return  COS_SECURITY_TOOL.SECURITY_ContentEncrypt("-4");

        EP_PROPERTY_TABLE_ID = COS_SECURITY_TOOL.SECURITY_ContentDecrypt(EP_PROPERTY_TABLE_ID);
        EP_PROPERTY_NAME = COS_SECURITY_TOOL.SECURITY_ContentDecrypt(EP_PROPERTY_NAME);

        COS_WEBSERVICE_PROP cos_w_prop = new COS_WEBSERVICE_PROP();
        return COS_SECURITY_TOOL.SECURITY_ContentEncrypt(cos_w_prop.PROP_Add(EP_PROPERTY_TABLE_ID, EP_PROPERTY_NAME).ToString());    
    }


    /// <summary>
    /// 删除EP产品属性
    /// </summary>
    /// <param name="EP_PROPERTY_ID">EP产品属性ID</param>
    /// <param name="SK">安全码</param>
    /// <returns>
    /// 返回值为1 说明删除成功 
    /// 返回值为-1 说明方法执行异常 
    /// 返回值为-2 说明参数不符合标准
    /// 返回值为-4 说明安全验证失败
    /// </returns>
    [WebMethod]
    public string PROP_Delete(string EP_PROPERTY_ID,string SK)
    {
        string[,] p = new string[2, 1];
        p[0, 0] = "EP_PROPERTY_ID";
        p[1, 0] = EP_PROPERTY_ID;

        if (!COS_SECURITY_TOOL.SECURITY_RequestDecrypt(p, SK)) return  COS_SECURITY_TOOL.SECURITY_ContentEncrypt("-4");

        EP_PROPERTY_ID = COS_SECURITY_TOOL.SECURITY_ContentDecrypt(EP_PROPERTY_ID);

        COS_WEBSERVICE_PROP cos_w_prop = new COS_WEBSERVICE_PROP();
        return COS_SECURITY_TOOL.SECURITY_ContentEncrypt(cos_w_prop.PROP_Delete(EP_PROPERTY_ID).ToString());


    }


    /// <summary>
    /// 获取某一HCCU下所有EP的属性状态信息
    /// </summary>
    /// <param name="HCCU_ID">HCCU编码</param>
    /// <param name="SK">安全码</param>
    /// 返回内容格式：  {EP_ID}|{PropertyName},{PropertyValue}*{PropertyName},{PropertyValue}^{EP_ID}|{PropertyName},{PropertyValue}*{PropertyName},{PropertyValue}^
    /// 返回值为 -1 说明无EP属性信息或EP无效注册
    /// 返回值为 -2 说明参数不符合标准
    /// 返回值为 -4 说明安全验证失败
    [WebMethod]
    public string EP_PROPERTYDATA_Sync(string HCCU_ID,string SK) {

        string[,] p = new string[2, 1];
        p[0, 0] = "HCCU_ID";
        p[1, 0] = HCCU_ID;

        if (!COS_SECURITY_TOOL.SECURITY_RequestDecrypt(p, SK)) return COS_SECURITY_TOOL.SECURITY_ContentEncrypt("-4");

        HCCU_ID = COS_SECURITY_TOOL.SECURITY_ContentDecrypt(HCCU_ID);

        COS_WEBSERVICE_PROP cos_w_prop = new COS_WEBSERVICE_PROP();
        return COS_SECURITY_TOOL.SECURITY_ContentEncrypt(cos_w_prop.EP_PROPERTYDATA_Sync(HCCU_ID));
    
    
    
    }

    /// <summary>
    /// 上行EP属性状态信息
    /// </summary>
    /// <param name="HCCU_ID">HCCU编码</param>
    /// <param name="EP_ID">EP编码</param>
    /// <param name="PROPERTY_COLLECTION">属性集 PROPERTY,VALUE|</param>
    /// <param name="SK">安全码</param>
    /// <returns>
    /// 返回值为 1 说明上行信息成功
    /// 返回值为 -1 说明方法执行异常
    /// 返回值为 -2 说明参数不符合标准
    /// 返回值为 -4 说明安全验证失败
    /// </returns>
    [WebMethod]
    public string EP_PROPERTYDATA_Uplink(string HCCU_ID, string EP_ID, string PROPERTY_COLLECTION, string SK)
    {

        string[,] p = new string[2, 3];
        p[0, 0] = "HCCU_ID";
        p[1, 0] = HCCU_ID;
        p[0, 1] = "EP_ID";
        p[1, 1] = EP_ID;
        p[0, 2] = "PROPERTY_COLLECTION";
        p[1, 2] = PROPERTY_COLLECTION;

        if (!COS_SECURITY_TOOL.SECURITY_RequestDecrypt(p, SK)) return COS_SECURITY_TOOL.SECURITY_ContentEncrypt("-4");

        HCCU_ID = COS_SECURITY_TOOL.SECURITY_ContentDecrypt(HCCU_ID);
        EP_ID = COS_SECURITY_TOOL.SECURITY_ContentDecrypt(EP_ID);
        PROPERTY_COLLECTION = COS_SECURITY_TOOL.SECURITY_ContentDecrypt(PROPERTY_COLLECTION);

        COS_WEBSERVICE_PROP cos_w_prop = new COS_WEBSERVICE_PROP();
        return COS_SECURITY_TOOL.SECURITY_ContentEncrypt(cos_w_prop.EP_PROPERTYDATA_Uplink(HCCU_ID, EP_ID, PROPERTY_COLLECTION).ToString());
    
    }


    
    /// <summary>
    /// 获取EP属性注册信息
    /// </summary>
    /// <param name="HCCU_ID">HCCU编码</param>
    /// <param name="SK">安全码</param>
    /// <returns>
    /// 返回内容格式：{EP_PROPERTY_ID},{EP_PROPERTY_NAME}|
    /// 返回值为 -1 说明无注册信息
    /// </returns>
    [WebMethod]
    public string EP_PROPERTYFACT_Sync(string HCCU_ID,string SK) {

        string[,] p = new string[2, 1];
        p[0, 0] = "HCCU_ID";
        p[1, 0] = HCCU_ID;

        if (!COS_SECURITY_TOOL.SECURITY_RequestDecrypt(p, SK)) return COS_SECURITY_TOOL.SECURITY_ContentEncrypt("-4");

        HCCU_ID = COS_SECURITY_TOOL.SECURITY_ContentDecrypt(HCCU_ID);

        COS_WEBSERVICE_PROP cos_w_prop = new COS_WEBSERVICE_PROP();
        return COS_SECURITY_TOOL.SECURITY_ContentEncrypt(cos_w_prop.EP_PROPERTYFACT_Sync().ToString());
    
    }

}
