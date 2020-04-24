
	1.	安装VS2015环境安装包(vcredist_x64_vs2015.exe)、VS2017环境安装包（vcredist_x64_vs2017.exe）
	
	2.	从官网申请sdk  http://www.arcsoft.com.cn/ai/arcface.html  ，下载对应的sdk版本(x86或x64)并解压
	
	3.	将libs中的“libarcsoft_face.dll”、“libarcsoft_face_engine.dll”拷贝到工程bin目录的对应平台的debug或release目录下
	
	4.	将对应appid和appkey替换App.config文件中对应内容
	
	5.	在Debug或者Release中选择配置管理器，选择对应的平台
	
	6.	在相应的.cs文件中，配置MySQL数据库信息
