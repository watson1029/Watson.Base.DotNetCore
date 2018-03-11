# ConfigurationHelper
## 引用
    Install-Package Watson.Base.DotNetCore -Version
    using Watson.Base.DotNetCore
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
## 引用
    Install-Package Watson.Base.DotNetCore -Version
    using Watson.Base.DotNetCore
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
    var resultForGet = ProtoBuf.Serializer.Deserialize<DataClass>(
                responseForGet.Result.Content.ReadAsStreamAsync().Result);
    //HttpPost
    ProtoBuf.Serializer.Serialize(stream, _dataClass);
    var httpContent = new StreamContent(stream);
    var responseForPost = await _client.PostAsync(url, httpContent);
    var resultForPost = ProtoBuf.Serializer.Deserialize<DataClass>(
                responseForPost.Result.Content.ReadAsStreamAsync().Result);
```
## 参考资料
  [Zaabee.AspNetCoreProtobuf](https://github.com/Mutuduxf/Zaabee.AspNetCoreProtobuf)
---
# LogHelper
---
# EncryptHelper
---
