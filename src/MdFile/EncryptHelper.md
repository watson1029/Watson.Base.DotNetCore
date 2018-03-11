EncryptHelper
===
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
