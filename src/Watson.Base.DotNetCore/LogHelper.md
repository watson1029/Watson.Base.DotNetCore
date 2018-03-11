LogHelper
===
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
