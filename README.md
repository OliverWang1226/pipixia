[TOC]

# 皮皮虾

## 一. 项目展示



### 1.游戏登录界面:

![微信图片_20180619220704](C:\Users\wangt\Desktop\微信图片_20180619220704.jpg)



### 2.游戏界面:

![微信图片_20180619220842](C:\Users\wangt\Desktop\微信图片_20180619220842.jpg)



### 3.游戏排行榜

![微信图片_20180619220745](C:\Users\wangt\Desktop\微信图片_20180619220745.jpg)













## 二. 项目功能

### 1.用户的登录和注册：

#### 	1) 点击主界面右上角 ***点我登录***  按钮， 弹出登录对话框

​		登录界面输入手机号（正则检测）,  手机号或密码错误时弹出 ***“用户名或密码错误 ”***  提示

​		登陆成功后，弹出 ***”登陆成功 “*** 提示，右上角 ***点我登录***  按钮转换为 ***退出登录***  按钮  

#### 	2) 在登录界面点击 ***点击注册***  按钮，弹出注册对话框

​		注册界面输入手机号， 密码，确认密码，检测两次输入密码是否一致，错误时弹出相应提示

​		两次输入一致后，在后端检测手机号是否已被注册，错误时弹出相应提示

​		注册成功后，弹出 ***”注册成功 “*** 提示， 注册对话框隐藏，弹出登录对话框



### 2.显示用户排名和历史最高分：

#### 	1)  获取排名及历史最高分：

​		在登陆成功后，服务器同时会返回该用户的当前排名和历史最高分，并保存在cache中

​		在GameOver后，向服务器发送本次成绩时，服务器返回当前排名和历史最高分，并更新cache中的值

#### 	2）显示用户排行榜（Top 10):

​		在主界面点击查看排行后，进入排行榜界面

​		在GameOver后，点击确认进入排行榜界面

​		每次进入排行榜界面时，从服务器取回排名前十的用户分数，显示在排行榜上

​		进入排行榜界面时，下方显示我的历史最高分，我的当前排名



### 3.查看操作指南：

​	在主界面点击操作指南，进入操作指南页面，可以看到游戏方法的介绍，左上角按钮可返回主界面





## 三. 项目结构

### 1.frontend

frontend0文件夹可直接作为Unity项目打开

C#脚本文件：frontend/Assets/目录下

```
frontend
	├─Assets
		├─backToStart.cs
		├─backToStartBtn.cs
		├─Btn.cs
		├─intrBack.cs 
		├─LoginBtn.cs
		├─LoginDialog.cs
		├─LogoutBtn.cs
		├─overCfm.cs
		├─RankScene.cs
		├─RegisterDialog.cs
		└─startBtn.cs
```

游戏逻辑实现：Btn.cs

注册功能实现：RegisterDialog.cs

登录功能实现：LoginDialog.cs

排行榜功能实现：RankScene.cs

游戏结束对话框：OverCfm.cs

退出登录功能实现：LogoutBtn.cs

返回主界面对话框：backToStart.cs

主界面登录按钮功能：LoginBtn.cs

游戏返回主界面跳转：backToStartBtn.css

排行榜返回主界面跳转：intrBack.cs

主界面进入游戏/排行榜跳转：startBtn.cs



### 2.backend

backend目录下的package.json中注明了依赖包

mobile目录：

```
mobile
│  app.js
│  npm-debug.log
│  package.json
│
├─bin
│ 
├─DB
│      dbConfig.js
│      usersql.js
│
├─node_modules
│ 
├─public
│  ├─images
│  ├─javascripts
│  └─stylesheets
│          style.css
│
├─routes
│      addUser.js
│      getRank.js
│      index.js
│      Login.js
│      npm-debug.log
│      updateScore.js
│      users.js
│      usersql.js
│
└─views
        error.jade
        index.jade
        layout.jade


```



app.js中定义了一级路由，将不同的url请求分发到routes目录下的js文件内函数中

​	/add-user	用户注册请求地址		对应addUser.js

​	/login		用户登录请求地址 	对应Login.js

​	/update		成绩更新请求地址		对应updateScore.js

​	/get-rank	获取排行请求地址		对应getRank.js



DB中 connection.js储存数据库信息

​	usersql.js中封装数据库操作

​	

## 四. 项目部署

### 1.前端

#### 1） Unity 2017年以后版本

#### 2）下载安装后打开frontend项目



### 2.后端

#### 	1）安装node.js(含npm)

#### 	2）安装mysql-server

​		创建数据库：`mysql-> create databse mobileApp;`

​		创建数据表：			

```
userInfo：
+----------+-------------+------+-----+---------+-------+
| Field    | Type        | Null | Key | Default | Extra |
+----------+-------------+------+-----+---------+-------+
| phoneNum | varchar(11) | NO   | PRI | NULL    |       |
| password | varchar(20) | NO   |     | NULL    |       |
+----------+-------------+------+-----+---------+-------+
```

```
userBest：
+-----------+-------------+------+-----+---------+-------+
| Field     | Type        | Null | Key | Default | Extra |
+-----------+-------------+------+-----+---------+-------+
| phoneNum  | varchar(20) | NO   |     | NULL    |       |
| bestScore | int(20)     | NO   |     | NULL    |       |
+-----------+-------------+------+-----+---------+-------+
```



#### 	3）安装express

​		创建工作目录：`$ mkdir myapp`

​		进入工作目录：`$ cd myapp`

​		初始化npm环境：`$ npm init`    此步骤下，选项全部默认回车即可

​		安装express: `$ npm install express --save`  使用--save，保存到依赖表

​		安装应用生成器： `$ npm install express-generator -g`

​		创建应用: `$ express mobile`

​		用git仓库中的backend/mobile目录替换掉创建的mobile

​		开始运行：`$ DEBUG=mobile:* npm start`