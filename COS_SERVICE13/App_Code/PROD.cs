using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using com.cooshare.os.dev;
using com.cooshare.os.sec;

/// <summary>
///PROD 的摘要说明
/// </summary>
[WebService(Namespace = "http://com.cooshare.os.dev")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class PROD : System.Web.Services.WebService {

    public PROD () {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string Service_ReadMe()
    {
        return "COS V1.0 WebService For com.cooshare.os.dev.COS_WEBSERVICE_PROD";
    }

    /// <summary>
    /// 增加EP产品
    /// </summary>
    /// <param name="EP_PRODUCT_NAME">EP产品名称</param>
    /// <param name="EP_PRODUCT_DESCRIPTION">EP产品描述</param>
    /// <param name="DEV_ID">开发者ID</param>
    /// <param name="SK">安全码</param>
    /// <returns>
    /// 返回值为 [{产品属性表的OBJECT_ID},{EP_PRODUCT_ID}] 
    /// 返回值为 [0,0] 说明该DEV_ID下，有同名EP产品 
    /// 返回值为 [-1,-1] 说明方法执行异常 
    /// 返回值为 [-2,-2] 说明参数不符合标准 
    /// 返回值为 [-4,-4] 说明安全验证失败
    /// </returns>
    [WebMethod]
    public string PROD_Add(string EP_PRODUCT_NAME, string EP_PRODUCT_DESCRIPTION, string DEV_ID,string SK)
    {
        string[,] p = new string[2, 3];
        p[0, 0] = "EP_PRODUCT_NAME";
        p[1, 0] = EP_PRODUCT_NAME;
        p[0, 1] = "EP_PRODUCT_DESCRIPTION";
        p[1, 1] = EP_PRODUCT_DESCRIPTION;
        p[0, 2] = "DEV_ID";
        p[1, 2] = DEV_ID;

        if (!COS_SECURITY_TOOL.SECURITY_RequestDecrypt(p, SK)) return  COS_SECURITY_TOOL.SECURITY_ContentEncrypt("-4,-4");

        EP_PRODUCT_NAME = COS_SECURITY_TOOL.SECURITY_ContentDecrypt(EP_PRODUCT_NAME);
        EP_PRODUCT_DESCRIPTION = COS_SECURITY_TOOL.SECURITY_ContentDecrypt(EP_PRODUCT_DESCRIPTION);
        DEV_ID = COS_SECURITY_TOOL.SECURITY_ContentDecrypt(DEV_ID);


        COS_WEBSERVICE_PROD cos_w_prod = new COS_WEBSERVICE_PROD();
        int[] s = new int[2];
        s = cos_w_prod.PROD_Add(EP_PRODUCT_NAME, EP_PRODUCT_DESCRIPTION, DEV_ID);
        return  COS_SECURITY_TOOL.SECURITY_ContentEncrypt(s[0].ToString() + "," + s[1].ToString());
    }

    /// <summary>
    /// 删除EP产品
    /// </summary>
    /// <param name="EP_PRODUCT_ID">被删除的EP产品ID</param>
    /// <param name="SK">安全码</param>
    /// <returns>
    /// 返回值为 1 说明删除成功 
    /// 返回值为 -1 说明方法执行异常 
    /// 返回值为 -2 说明参数不符合标准 
    /// 返回值为 -4 说明安全验证失败
    /// </returns>
    [WebMethod]
    public string PROD_Delete(string EP_PRODUCT_ID,string SK)
    {
        string[,] p = new string[2, 1];
        p[0, 0] = "EP_PRODUCT_ID";
        p[1, 0] = EP_PRODUCT_ID;

        if (!COS_SECURITY_TOOL.SECURITY_RequestDecrypt(p, SK)) return  COS_SECURITY_TOOL.SECURITY_ContentEncrypt("-4");

        EP_PRODUCT_ID = COS_SECURITY_TOOL.SECURITY_ContentDecrypt(EP_PRODUCT_ID);

        COS_WEBSERVICE_PROD cos_w_prod = new COS_WEBSERVICE_PROD();
        return  COS_SECURITY_TOOL.SECURITY_ContentEncrypt(cos_w_prod.PROD_Delete(EP_PRODUCT_ID).ToString());  
    }

    /// <summary>
    /// 修改EP产品
    /// </summary>
    /// <param name="EP_PRODUCT_ID">EP产品ID</param>
    /// <param name="EP_PRODUCT_NAME">EP产品名称</param>
    /// <param name="EP_PRODUCT_DESCRIPTION">EP产品描述</param>
    /// <param name="DEV_ID">开发者ID</param>
    /// <param name="SK">安全码</param>
    /// <returns>
    /// 返回值为 1 说明信息更新成功 
    /// 返回值为-1 说明方法执行异常 
    /// 返回值为-2 说明参数不符合标准 
    /// 返回值为-4 说明安全验证失败
    /// </returns>
    [WebMethod]
    public string PROD_Edit(string EP_PRODUCT_ID, string EP_PRODUCT_NAME, string EP_PRODUCT_DESCRIPTION, string DEV_ID,string SK)
    {
        string[,] p = new string[2, 4];
        p[0, 0] = "EP_PRODUCT_ID";
        p[1, 0] = EP_PRODUCT_ID;
        p[0, 1] = "EP_PRODUCT_NAME";
        p[1, 1] = EP_PRODUCT_NAME;
        p[0, 2] = "EP_PRODUCT_DESCRIPTION";
        p[1, 2] = EP_PRODUCT_DESCRIPTION;
        p[0, 3] = "DEV_ID";
        p[1, 3] = DEV_ID;

        if (!COS_SECURITY_TOOL.SECURITY_RequestDecrypt(p, SK)) return  COS_SECURITY_TOOL.SECURITY_ContentEncrypt("-4");

        EP_PRODUCT_ID = COS_SECURITY_TOOL.SECURITY_ContentDecrypt(EP_PRODUCT_ID);
        EP_PRODUCT_NAME = COS_SECURITY_TOOL.SECURITY_ContentDecrypt(EP_PRODUCT_NAME);
        EP_PRODUCT_DESCRIPTION = COS_SECURITY_TOOL.SECURITY_ContentDecrypt(EP_PRODUCT_DESCRIPTION);
        DEV_ID = COS_SECURITY_TOOL.SECURITY_ContentDecrypt(DEV_ID);

        COS_WEBSERVICE_PROD cos_w_prod = new COS_WEBSERVICE_PROD();
        return COS_SECURITY_TOOL.SECURITY_ContentEncrypt(cos_w_prod.PROD_Edit(EP_PRODUCT_ID, EP_PRODUCT_NAME, EP_PRODUCT_DESCRIPTION, DEV_ID).ToString());
    
    
    }
}
