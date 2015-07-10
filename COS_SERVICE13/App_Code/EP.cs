using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using com.cooshare.os;
using com.cooshare.os.sec;
using com.cooshare.os.dev;

/// <summary>
///EP 的摘要说明
/// </summary>
[WebService(Namespace = "http://com.cooshare.os")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class EP : System.Web.Services.WebService {

    public EP () {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string Service_ReadMe()
    {
        return "COS V1.0 WebService For com.cooshare.os.COS_WEBSERVICE_EP";
    }

    /// <summary>
    /// [WEBSERVICE] 添加EP
    /// </summary>
    /// <param name="EP_TypeId">EP类型ID</param>
    /// <param name="EP_UserDefined_Alias">用户定义的EP别名</param>
    /// <param name="EP_ProductId">EP产品编码</param>
    /// <param name="HCCU_Id">HCCU编码</param>
    /// <param name="EP_MAC_Id">EP MAC编码</param>
    /// <param name="PROPFORMAT">EP实际上传参数集</param>
    /// <param name="SK">安全码</param>
    /// <returns>
    /// 返回值为 [{EP_ID},1,0|property1,property2,..propertyN,] 说明添加成功，元素1为添加的EP_ID，元素2代表添加类型为新增, 后续property为该EP模型的属性枚举
    /// 返回值为 [{EP_ID},2,0|property1,property2,..propertyN,] 说明添加成功，元素1为添加的EP_ID，元素2代表添加类型为之前已添加在同一HCCU网络中，返回的EP_ID是之前添加所的 , 后续property为该EP模型的属性枚举
    /// 返回值为 [{EP_ID},3,{Original HCCU_ID}|property1,property2,..propertyN,] 说明添加成功，元素1为添加的EP_ID，元素2代表添加类型为之前已添加在另一个HCCU网络中，目前所属关系已更新到新HCCU_ID中，元素3代表旧的HCCU_ID , 后续property为该EP模型的属性枚举
    /// 返回值为 [-1,-1,-1|] 说明方法执行异常 
    /// 返回值为 [-2,-2,-2|] 说明参数不符合标准 
    /// 返回值为 -4 说明安全验证失败
    /// </returns>
    [WebMethod]
    public string EP_Add(string EP_TypeId, string EP_UserDefined_Alias, string EP_ProductId, string HCCU_Id, string EP_MAC_Id, string PROPFORMAT, string SK)
    {
        string[,] p = new string[2, 6];
        p[0, 0] = "EP_TypeId";
        p[1, 0] = EP_TypeId;
        p[0, 1] = "EP_UserDefined_Alias";
        p[1, 1] = EP_UserDefined_Alias;
        p[0, 2] = "EP_ProductId";
        p[1, 2] = EP_ProductId;
        p[0, 3] = "HCCU_Id";
        p[1, 3] = HCCU_Id;
        p[0, 4] = "EP_MAC_Id";
        p[1, 4] = EP_MAC_Id;
        p[0, 5] = "PROPFORMAT";
        p[1, 5] = PROPFORMAT;

        if (!COS_SECURITY_TOOL.SECURITY_RequestDecrypt(p, SK)) return  COS_SECURITY_TOOL.SECURITY_ContentEncrypt("-4");

        EP_TypeId = COS_SECURITY_TOOL.SECURITY_ContentDecrypt(EP_TypeId);
        EP_UserDefined_Alias = COS_SECURITY_TOOL.SECURITY_ContentDecrypt(EP_UserDefined_Alias);
        EP_ProductId = COS_SECURITY_TOOL.SECURITY_ContentDecrypt(EP_ProductId);
        HCCU_Id = COS_SECURITY_TOOL.SECURITY_ContentDecrypt(HCCU_Id);
        EP_MAC_Id = COS_SECURITY_TOOL.SECURITY_ContentDecrypt(EP_MAC_Id);

        COS_WEBSERVICE_EP cos_w_e = new COS_WEBSERVICE_EP();
        int[] ret = cos_w_e.EP_Add(EP_TypeId, EP_UserDefined_Alias, EP_ProductId, HCCU_Id, EP_MAC_Id, PROPFORMAT);

        string ret2 = "";
        if (ret[0] > 0) {
            COS_WEBSERVICE_PROD cos_w_p = new COS_WEBSERVICE_PROD();
            ret2 = cos_w_p.GET_PROD_ByEPID(ret[0].ToString());
        }

        return COS_SECURITY_TOOL.SECURITY_ContentEncrypt(ret[0].ToString() + "," + ret[1].ToString() + "," + ret[2].ToString() + "|" + ret2);
    }


    /// <summary>
    /// [WEBSERVICE] 添加EP_20140320启用
    /// </summary>
    /// <param name="EP_TypeId">EP类型ID</param>
    /// <param name="EP_UserDefined_Alias">用户定义的EP别名</param>
    /// <param name="EP_ProductId">EP产品编码</param>
    /// <param name="HCCU_Id">HCCU编码</param>
    /// <param name="EP_MAC_Id">EP MAC编码</param>
    /// <param name="PROPFORMAT">EP实际上传参数集</param>
    /// <param name="SK">安全码</param>
    /// <returns>
    /// 返回值为 [{EP_ID},1,0|property1,property2,..propertyN,] 说明添加成功，元素1为添加的EP_ID，元素2代表添加类型为新增, 后续property为该EP模型的属性枚举
    /// 返回值为 [{EP_ID},2,0|property1,property2,..propertyN,] 说明添加成功，元素1为添加的EP_ID，元素2代表添加类型为之前已添加在同一HCCU网络中，返回的EP_ID是之前添加所的 , 后续property为该EP模型的属性枚举
    /// 返回值为 [{EP_ID},3,{Original HCCU_ID}|property1,property2,..propertyN,] 说明添加成功，元素1为添加的EP_ID，元素2代表添加类型为之前已添加在另一个HCCU网络中，目前所属关系已更新到新HCCU_ID中，元素3代表旧的HCCU_ID , 后续property为该EP模型的属性枚举
    /// 返回值为 [-1,-1,-1|] 说明方法执行异常 
    /// 返回值为 [-2,-2,-2|] 说明参数不符合标准 
    /// 返回值为 -4 说明安全验证失败
    /// </returns>
    [WebMethod]
    public string EP_Add_20140320(string EP_TypeId, string EP_UserDefined_Alias, string EP_ProductId, string HCCU_Id, string EP_MAC_Id, string PROPFORMAT, string IP, string SK)
    {
        string[,] p = new string[2, 7];
        p[0, 0] = "EP_TypeId";
        p[1, 0] = EP_TypeId;
        p[0, 1] = "EP_UserDefined_Alias";
        p[1, 1] = EP_UserDefined_Alias;
        p[0, 2] = "EP_ProductId";
        p[1, 2] = EP_ProductId;
        p[0, 3] = "HCCU_Id";
        p[1, 3] = HCCU_Id;
        p[0, 4] = "EP_MAC_Id";
        p[1, 4] = EP_MAC_Id;
        p[0, 5] = "PROPFORMAT";
        p[1, 5] = PROPFORMAT;
        p[0, 6] = "IP";
        p[1, 6] = IP;

        if (!COS_SECURITY_TOOL.SECURITY_RequestDecrypt(p, SK)) return COS_SECURITY_TOOL.SECURITY_ContentEncrypt("-4");

        EP_TypeId = COS_SECURITY_TOOL.SECURITY_ContentDecrypt(EP_TypeId);
        EP_UserDefined_Alias = COS_SECURITY_TOOL.SECURITY_ContentDecrypt(EP_UserDefined_Alias);
        EP_ProductId = COS_SECURITY_TOOL.SECURITY_ContentDecrypt(EP_ProductId);
        HCCU_Id = COS_SECURITY_TOOL.SECURITY_ContentDecrypt(HCCU_Id);
        EP_MAC_Id = COS_SECURITY_TOOL.SECURITY_ContentDecrypt(EP_MAC_Id);
        IP = COS_SECURITY_TOOL.SECURITY_ContentDecrypt(IP);


        COS_WEBSERVICE_EP cos_w_e = new COS_WEBSERVICE_EP();
        int[] ret = cos_w_e.EP_Add_20140320(EP_TypeId, EP_UserDefined_Alias, EP_ProductId, HCCU_Id, EP_MAC_Id, PROPFORMAT, IP);

        string ret2 = "";
        if (ret[0] > 0)
        {
            COS_WEBSERVICE_PROD cos_w_p = new COS_WEBSERVICE_PROD();
            ret2 = cos_w_p.GET_PROD_ByEPID(ret[0].ToString());
        }

        return COS_SECURITY_TOOL.SECURITY_ContentEncrypt(ret[0].ToString() + "," + ret[1].ToString() + "," + ret[2].ToString() + "|" + ret2);
    }
    

    /// <summary>
    /// [WEBSERVICE] 删除EP
    /// </summary>
    /// <param name="EP_Id">被删除的EP_ID</param>
    /// <param name="COSID">COSID</param>
    /// <param name="SK">安全码</param>
    /// <returns>
    /// 返回值为1 说明删除成功 
    /// 返回值为-1 说明方法执行异常 
    /// 返回值为-2 说明参数不符合标准 
    /// 返回值为-4 说明安全验证失败
    /// </returns>
    [WebMethod]
    public string EP_Delete(string EP_Id,string COSID,string SK)
    {
        string[,] p = new string[2, 2];
        p[0, 0] = "EP_Id";
        p[1, 0] = EP_Id;
        p[0,1]="COSID";
        p[1,1]=COSID;

        if (!COS_SECURITY_TOOL.SECURITY_RequestDecrypt(p, SK)) return  COS_SECURITY_TOOL.SECURITY_ContentEncrypt("-4");

        EP_Id = COS_SECURITY_TOOL.SECURITY_ContentDecrypt(EP_Id);
        COSID = COS_SECURITY_TOOL.SECURITY_ContentDecrypt(COSID);

        COS_WEBSERVICE_EP cos_w_e = new COS_WEBSERVICE_EP();
        return COS_SECURITY_TOOL.SECURITY_ContentEncrypt(cos_w_e.EP_Delete(EP_Id, COSID).ToString());
    }


    /// <summary>
    /// [WEBSERVICE] 获取某一HCCU下的所有EP信息
    /// </summary>
    /// <param name="HCCU_Id">HCCU编码</param>
    /// <param name="SK">安全码</param>
    /// <returns>
    /// 返回格式 EP_ID,EP_TYPEID,EP_USERDEFINED_ALIAS,EP_PRODUCTID,HCCU_ID,EP_MAC_ID|
    /// 返回值为 -1 说明没有任何EP注册信息
    /// 返回值为 -2 说明参数不符合标准 
    /// 返回值为 -4 说明安全验证失败
    /// </returns>
   [WebMethod]
    public string EP_Sync(string HCCU_Id, string SK) {

        string[,] p = new string[2, 1];
        p[0, 0] = "HCCU_Id";
        p[1, 0] = HCCU_Id;

        if (!COS_SECURITY_TOOL.SECURITY_RequestDecrypt(p, SK)) return COS_SECURITY_TOOL.SECURITY_ContentEncrypt("-4");

        HCCU_Id = COS_SECURITY_TOOL.SECURITY_ContentDecrypt(HCCU_Id);

        COS_WEBSERVICE_EP cos_w_e = new COS_WEBSERVICE_EP();
        return COS_SECURITY_TOOL.SECURITY_ContentEncrypt(cos_w_e.EP_Sync(HCCU_Id));

    
    }

   /// <summary>
   /// 修改EP信息(初始化EP用户属性表)
   /// </summary>
   /// <param name="EP_ID">EP编码</param>
   /// <param name="EP_TYPEID">用户定义的EP类型编码</param>
   /// <param name="EP_USERDEFINED_ALIAS">用户定义的EP别称</param>
   /// <param name="EP_PRODUCT_ID">EP产品类型编码</param>
   /// <param name="SK">安全码</param>
   /// <returns>
   /// 返回值为 1 说明更新成功
   /// 返回值为 -1 说明方法执行异常
   /// 返回值为 -2 说明参数不符合标准
   /// 返回值为 -4 说明安全验证失败
   /// </returns>
   [WebMethod]
   public string EP_Edit(string EP_ID, string EP_TYPEID, string EP_USERDEFINED_ALIAS, string EP_PRODUCT_ID, string SK)
   {
       string[,] p = new string[2, 4];
       p[0, 0] = "EP_ID";
       p[1, 0] = EP_ID;
       p[0, 1] = "EP_TYPEID";
       p[1, 1] = EP_TYPEID;
       p[0, 2] = "EP_USERDEFINED_ALIAS";
       p[1, 2] = EP_USERDEFINED_ALIAS;
       p[0, 3] = "EP_PRODUCT_ID";
       p[1, 3] = EP_PRODUCT_ID;


       if (!COS_SECURITY_TOOL.SECURITY_RequestDecrypt(p, SK)) return COS_SECURITY_TOOL.SECURITY_ContentEncrypt("-4");

       EP_ID = COS_SECURITY_TOOL.SECURITY_ContentDecrypt(EP_ID);
       EP_TYPEID = COS_SECURITY_TOOL.SECURITY_ContentDecrypt(EP_TYPEID);
       EP_USERDEFINED_ALIAS = COS_SECURITY_TOOL.SECURITY_ContentDecrypt(EP_USERDEFINED_ALIAS);
       EP_PRODUCT_ID = COS_SECURITY_TOOL.SECURITY_ContentDecrypt(EP_PRODUCT_ID);

       COS_WEBSERVICE_EP cos_w_e = new COS_WEBSERVICE_EP();
       return COS_SECURITY_TOOL.SECURITY_ContentEncrypt(cos_w_e.EP_Edit(EP_ID, EP_TYPEID, EP_USERDEFINED_ALIAS, EP_PRODUCT_ID));

   }


}
