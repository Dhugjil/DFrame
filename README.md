# DFrame

DFrame是一个轻量级ORM框架。它内部集成SQLHelper组件和Dapper框架。

DFrame.Common命名空间集成:
1:EncryptDecrypt(AES/Base64/DES/HmacSha/MD5/SHA/RSA);
2:HttpService 服务类;
3:ImageClass 图片操作;
4:Json json操作;
5:ListComparer 按实体字段去重;
6:Mail 电子邮件操作;
7:QRCode 生成二维码和解读二维码;
8:SMS 短信接口 阿里云;
9:Tools 其他工具包;
10:UserInfo 获取用户信息。

DFrame.DAL命名空间集成:
1:Access 数据库操作类;
2:MySQL 数据库操作类;
3:SQLServer 数据库操作类;
4:生成MySQL数据库方法;
5:生成SQLServer数据库方法;
  例如,执行以下代码:
    Assembly ass = Assembly.Load("Models");
    DFrame.DAL.SQLFactory.Create(DFrame.DAL.SQLFactory.DatabaseType.MSSQLServer, "test1", ass);
    将会“Models”命名空间下的所有已集成DFrame.Model.DBModel抽象类的实体类,写入到MSSQLServer的"test1"数据库。
  实体字段设定：
    [DFrame.Model.DBField(NotNull = true,DBFieldKey =DFrame.Model.Enums.DBFieldKey.Unique,DefaultValue ="默认",ForeignKey =typeof(Person),Lenght =4001)]
    public string Text { get; set; }
    //其中NotNull设定是否可空;
    //其中DBFieldKey键类型;
    //其中DefaultValue设定默认值;
    //其中ForeignKey设定外键;
    //其中Lenght设定长度;
    
DFrame.Model命名空间:
1:提供DFrame.DAL下的生成数据库用的一些抽象类;
2:命名空间内部已集成简单的SQLHelper操作类。并对继承DFrame.Model.DBModel抽象类的实体提供lambda语句查询方法(目前只支持MSSQLServer数据库)。
  lambda查询支持的方法有：
  1: long Count()
  2: List<TResult> ToList<TResult>() 
  3: int Delete()
