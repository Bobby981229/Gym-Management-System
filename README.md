
	1.	下载并安装VS2017或VS2019环境安装包 https://visualstudio.microsoft.com/zh-hans/downloads/
	
	2.	下载并安装MySQL数据库 https://dev.mysql.com/downloads/mysql/
	
	3.	配置MySQL环境，并在相应的.cs文件中，配置MySQL数据库用户和密码
	
	4.	从官网申请人脸识别SDK  http://www.arcsoft.com.cn/ai/arcface.html  ，下载对应的SDK版本(x86或x64)并解压
	
	5.	将libs中的“libarcsoft_face.dll”、“libarcsoft_face_engine.dll”拷贝到工程bin目录的对应平台的debug或release目录下
	
	6.	将对应appid和appkey替换App.config文件中对应内容
	
	7.      在Debug或者Release中选择配置管理器，选择对应的平台
