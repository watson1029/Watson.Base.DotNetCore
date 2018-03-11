Use Instructions
=====
# ConfigurationHelper
## 引用
    using Watson.Base.DotNetCore
## 调用
### 调用appsetting.json数据库连接字符串
```CSharp
    ConfigurationHelper.Configuration.GetConnectionString("ConnectionString");
```
### 调用appsetting.json一级节点
```CSharp
    ConfigurationHelper.Configuration.GetSection("Section").ToString();
```
### 调用appsetting.json二级节点
```CSharp
    ConfigurationHelper.Configuration.GetSection("SectionOne")["SectionTwo"];
    ConfigurationHelper.Configuration.GetSection("SectionOne:SectionTwo").ToString();
```
---
# ProtoBufHelper
---
# LogHelper
---
# EncryptHelper
---
