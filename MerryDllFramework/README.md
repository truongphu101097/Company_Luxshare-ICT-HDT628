# 工程DLL文档说明


#### 介绍
工程产测程序软体4.0版本


#### 使用说明
1.右键点击MerryDllFramework类库，选择属性，将其中程序集名称修改为对应机型名称，并保存 例：HDT668
2.进入MerryDllFramework_Debug主控台程序根目录，创建对应机型文件，并且在其中复制一份Message.txt以及Config.txt 例：HDT668
3.打开Data.cs文件。修改其中_type为当前机型名 例：HDT668
4.打开MerryDll，在被指令调用方法区增加方法并且在Invokes方法中增加其调用 例： case "BootUp":return BootUp().ToString();    private bool BootUp();
5.在主控台程序的Program文件中，向匿名集合添加需要调试的方法对应参数 例：BootUp


#### 参与贡献
### 主程序与DLL程序
1.  张晓龙
2.  王菅物
### 通用DLL
1.  张晓龙
2.  王菅物
3.  欧阳凯
4.  邱航宇
### 文档撰写
1.王菅物


#### 主要负责人
1.  张晓龙
2.  王菅物


