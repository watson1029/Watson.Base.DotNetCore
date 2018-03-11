# ConfigurationHelper
## 引用
        Install-Package Watson.Base.DotNetCore
```CSharp
    using Watson.Base.DotNetCore
```
## 调用appsetting.json
### 数据库连接字符串
```CSharp
    ConfigurationHelper.Configuration.GetConnectionString("ConnectionString");
```
### 一级节点
```CSharp
    ConfigurationHelper.Configuration.GetSection("Section").ToString();
```
### 二级节点
```CSharp
    ConfigurationHelper.Configuration.GetSection("SectionOne")["SectionTwo"];
    ConfigurationHelper.Configuration.GetSection("SectionOne:SectionTwo").ToString();
```
---
# ProtoBufHelper
base on protobuf-net
## 引用
        Install-Package Watson.Base.DotNetCore
```CSharp
    using Watson.Base.DotNetCore
```
## 修改Startup.cs
```CSharp
    services.AddMvc(options => { options.AddProtobufSupport(); });
```
## 实体类添加ProtoBuf关键字
```CSharp
    [ProtoContract]
    public partial class DataClass
    {
        private string _dataparam1;
        private string _dataparam2;
        [ProtoMember(1)]
        public string DataParam1
        {
            set { _dataparam1 = value; }
            get { return _dataparam1; }
        }
        [ProtoMember(2)]
        public string DataParam2
        {
            set { _dataparam2 = value; }
            get { return _dataparam2; }
        }
    }
```
## 使用application/x-protobuf协议
```CSharp
    HttpClient _client = new HttpClient();
    _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-protobuf"));
    
    //HttpGet
    var responseForGet = await _client.GetAsync(url);
    var resultForGet = ProtoBuf.Serializer.Deserialize<DataClass>
                        (responseForGet.Result.Content.ReadAsStreamAsync().Result);
    
    //HttpPost
    ProtoBuf.Serializer.Serialize(stream, _dataClass);
    var httpContent = new StreamContent(stream);
    var responseForPost = await _client.PostAsync(url, httpContent);
    var resultForPost = ProtoBuf.Serializer.Deserialize<DataClass>
                        (responseForPost.Result.Content.ReadAsStreamAsync().Result);
```
## 参考资料
  [Zaabee.AspNetCoreProtobuf](https://github.com/Mutuduxf/Zaabee.AspNetCoreProtobuf)
---
# LogHelper
## 引用
        Install-Package Watson.Base.DotNetCore
```CSharp
    using Watson.Base.DotNetCore
```
## 添加appsetting.json节点
```json
    "WatsonBaseSettings" : {
        "SysLog" : "SqlConnectionString",
        "LogFile" : "LogFilePath"
    }
```
## 写数据库日志
### 数据库脚本
```Sql
    use [YourDataBase]
    Create Table SysLog
    (
        LogID Guid primary key,
        LogMsg nvarchar(512),
        LogDetail nvarchar(max),
        LogTime datetime
    )
```
### 调用
```CSharp
    LogHelper.WriteDbAsync(_exception);
    LogHelper.WriteDbAsync(_strLogMsg, _strLogDetail);
```
## 写文件日志
```CSharp
    LogHelper.WriteFile(_exception);
    LogHelper.WriteFile(_strLogMsg, _strLogDetail);
```
---
# EncryptHelper
## 引用
        Install-Package Watson.Base.DotNetCore
```CSharp
    using Watson.Base.DotNetCore
```
## DES
### 实例化
```CSharp
    //KEY和IV随机生成
    EncryptHelper.DES des = new EncryptHelper.DES();
    //KEY自定义，IV随机生成
    EncryptHelper.DES des = new EncryptHelper.DES(_sKey);
    //KEY和IV自定义
    EncryptHelper.DES des = new EncryptHelper.DES(_sKey, sIV);
```
### 获取Key和IV
```CSharp
    EncryptHelper.DES des = new EncryptHelper.DES();
    string _key = des.getKey;
    string _iv = des.getIV;
```
### 加密与解密
```CSharp
    EncryptHelper.DES des = new EncryptHelper.DES();
    //加密
    string _cipher = des.Encrypt(plain);
    //解密
    string _plain = des.Decrypt(_cipher);
    
    Assert.AreEqual(plain, _plain);
```
## TripleDes
### 实例化
```CSharp
    //KEY和IV随机生成
    EncryptHelper.TripleDES tripleDes = new EncryptHelper.TripleDES();
    //KEY自定义，IV随机生成
    EncryptHelper.TripleDES tripleDes = new EncryptHelper.TripleDES(_sKey);
    //KEY和IV自定义
    EncryptHelper.TripleDES tripleDes = new EncryptHelper.TripleDES(_sKey, sIV);
```
### 获取Key和IV
```CSharp
    EncryptHelper.TripleDES tripleDes = new EncryptHelper.TripleDES();
    string _key = tripleDes.getKey;
    string _iv = tripleDes.getIV;
```
### 加密与解密
```CSharp
    EncryptHelper.TripleDES tripleDes = new EncryptHelper.TripleDES();
    //加密
    string _cipher = tripleDes.Encrypt(plain);
    //解密
    string _plain = tripleDes.Decrypt(_cipher);
    
    Assert.AreEqual(plain, _plain);
```
## AES
### 实例化
```CSharp
    //KEY和IV随机生成
    EncryptHelper.AES aes = new EncryptHelper.AES();
    //KEY自定义，IV随机生成
    EncryptHelper.AES aes = new EncryptHelper.AES(_sKey);
    //KEY和IV自定义
    EncryptHelper.AES aes = new EncryptHelper.AES(_sKey, sIV);
```
### 获取Key和IV
```CSharp
    EncryptHelper.AES aes = new EncryptHelper.AES();
    string _key = aes.getKey;
    string _iv = aes.getIV;
```
### string加密与解密
```CSharp
    EncryptHelper.AES aes = new EncryptHelper.AES();
    //加密
    string _cipher = aes.Encrypt(plain);
    //解密
    string _plain = aes.Decrypt(_cipher);
    
    Assert.AreEqual(plain, _plain);
```
### byte[]加密与解密
```CSharp
    EncryptHelper.AES aes = new EncryptHelper.AES();
    //加密
    byte[] _cipher = aes.Encrypt(plain);
    //解密
    byte[] _plain = aes.Decrypt(_cipher);
    
    Assert.AreEqual(plain, _plain);
```
### 文件加密与解密
```CSharp
    EncryptHelper.AES aes = new EncryptHelper.AES();
    //加密
    aes.Encrypt(plain, _cipher);
    //解密
    aes.Decrypt(_cipher, _plain);
    
    Assert.AreEqual(plain, _plain);
```
## RC2
### 实例化
```CSharp
    //KEY和IV随机生成
    EncryptHelper.RC2 rc2 = new EncryptHelper.RC2();
    //KEY自定义，IV随机生成
    EncryptHelper.RC2 rc2 = new EncryptHelper.RC2(_sKey);
    //KEY和IV自定义
    EncryptHelper.RC2 rc2 = new EncryptHelper.RC2(_sKey, sIV);
```
### 获取Key和IV
```CSharp
    EncryptHelper.RC2 rc2 = new EncryptHelper.RC2();
    string _key = rc2.getKey;
    string _iv = rc2.getIV;
```
### 加密与解密
```CSharp
    EncryptHelper.RC2 rc2 = new EncryptHelper.RC2();
    //加密
    string _cipher = rc2.Encrypt(plain);
    //解密
    string _plain = rc2.Decrypt(_cipher);
    
    Assert.AreEqual(plain, _plain);
```
## RSA
### 生成公钥和私钥
```CSharp
    EncryptHelper.RSA rsa = new EncryptHelper.RSA();
    rsa.RSAKey(out keys, out publicKeys);
```

## MD5
## SHA1
---
