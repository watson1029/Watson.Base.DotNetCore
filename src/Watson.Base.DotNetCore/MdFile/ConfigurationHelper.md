ConfigurationHelper
===
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
