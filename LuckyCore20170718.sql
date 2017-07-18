/*
Navicat MySQL Data Transfer



Target Server Type    : MYSQL
Target Server Version : 50716
File Encoding         : 65001

Date: 2017-07-18 09:11:23
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for AspNetRoleClaims
-- ----------------------------
DROP TABLE IF EXISTS `AspNetRoleClaims`;
CREATE TABLE `AspNetRoleClaims` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `ClaimType` longtext,
  `ClaimValue` longtext,
  `RoleId` varchar(255) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_AspNetRoleClaims_RoleId` (`RoleId`),
  CONSTRAINT `FK_AspNetRoleClaims_Sys_Roles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `Sys_Roles` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of AspNetRoleClaims
-- ----------------------------
INSERT INTO `AspNetRoleClaims` VALUES ('1', 'Home', 'Index', '546695139276357632');

-- ----------------------------
-- Table structure for AspNetUserClaims
-- ----------------------------
DROP TABLE IF EXISTS `AspNetUserClaims`;
CREATE TABLE `AspNetUserClaims` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `ClaimType` longtext,
  `ClaimValue` longtext,
  `UserId` varchar(255) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_AspNetUserClaims_UserId` (`UserId`),
  CONSTRAINT `FK_AspNetUserClaims_Sys_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `Sys_Users` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of AspNetUserClaims
-- ----------------------------
INSERT INTO `AspNetUserClaims` VALUES ('1', 'Home', 'Index', '00054917-04ba-3ae6-0000-000000000000');

-- ----------------------------
-- Table structure for AspNetUserLogins
-- ----------------------------
DROP TABLE IF EXISTS `AspNetUserLogins`;
CREATE TABLE `AspNetUserLogins` (
  `LoginProvider` varchar(255) NOT NULL,
  `ProviderKey` varchar(255) NOT NULL,
  `ProviderDisplayName` longtext,
  `UserId` varchar(255) NOT NULL,
  PRIMARY KEY (`LoginProvider`,`ProviderKey`),
  KEY `IX_AspNetUserLogins_UserId` (`UserId`),
  CONSTRAINT `FK_AspNetUserLogins_Sys_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `Sys_Users` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of AspNetUserLogins
-- ----------------------------

-- ----------------------------
-- Table structure for AspNetUserRoles
-- ----------------------------
DROP TABLE IF EXISTS `AspNetUserRoles`;
CREATE TABLE `AspNetUserRoles` (
  `UserId` varchar(255) NOT NULL,
  `RoleId` varchar(255) NOT NULL,
  PRIMARY KEY (`UserId`,`RoleId`),
  KEY `IX_AspNetUserRoles_RoleId` (`RoleId`),
  CONSTRAINT `FK_AspNetUserRoles_Sys_Roles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `Sys_Roles` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_AspNetUserRoles_Sys_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `Sys_Users` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of AspNetUserRoles
-- ----------------------------
INSERT INTO `AspNetUserRoles` VALUES ('00054917-04ba-3ae6-0000-000000000000', '546695139276357632');

-- ----------------------------
-- Table structure for AspNetUserTokens
-- ----------------------------
DROP TABLE IF EXISTS `AspNetUserTokens`;
CREATE TABLE `AspNetUserTokens` (
  `UserId` varchar(255) NOT NULL,
  `LoginProvider` varchar(255) NOT NULL,
  `Name` varchar(255) NOT NULL,
  `Value` longtext,
  PRIMARY KEY (`UserId`,`LoginProvider`,`Name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of AspNetUserTokens
-- ----------------------------

-- ----------------------------
-- Table structure for Category
-- ----------------------------
DROP TABLE IF EXISTS `Category`;
CREATE TABLE `Category` (
  `CategoryID` varchar(30) NOT NULL,
  `CategoryType` varchar(50) DEFAULT NULL,
  `CreateDate` datetime(6) NOT NULL,
  `Description` varchar(50) DEFAULT NULL,
  `DisplayOrder` int(11) NOT NULL,
  `HyperLink` varchar(250) DEFAULT NULL,
  `ParentID` varchar(30) NOT NULL,
  `SortCode` varchar(10) DEFAULT NULL,
  `Title` varchar(150) NOT NULL,
  PRIMARY KEY (`CategoryID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of Category
-- ----------------------------
INSERT INTO `Category` VALUES ('591678553146261505', null, '2017-04-20 15:38:18.000000', '国际新闻', '0', '国际新闻', '0', null, '国际新闻');

-- ----------------------------
-- Table structure for Department
-- ----------------------------
DROP TABLE IF EXISTS `Department`;
CREATE TABLE `Department` (
  `DepartmentId` varchar(50) NOT NULL,
  `DepartmentName` varchar(50) NOT NULL,
  `Description` varchar(500) NOT NULL,
  `DistributorId` int(11) NOT NULL,
  `ParentId` varchar(50) NOT NULL,
  `Sort` int(11) NOT NULL,
  `State` int(11) NOT NULL,
  PRIMARY KEY (`DepartmentId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of Department
-- ----------------------------
INSERT INTO `Department` VALUES ('591346418392760320', '上海总部', '上海总部', '0', '0', '0', '0');

-- ----------------------------
-- Table structure for Links
-- ----------------------------
DROP TABLE IF EXISTS `Links`;
CREATE TABLE `Links` (
  `LinkID` char(36) NOT NULL,
  `CreateDate` datetime(6) NOT NULL,
  `DisplayOrder` int(11) NOT NULL,
  `ImageUrl` varchar(150) DEFAULT NULL,
  `IsImage` bit(1) NOT NULL,
  `IsLock` bit(1) NOT NULL,
  `Title` varchar(150) DEFAULT NULL,
  `UserEmail` varchar(150) DEFAULT NULL,
  `UserName` varchar(50) DEFAULT NULL,
  `UserTel` varchar(50) DEFAULT NULL,
  `WebUrl` varchar(150) NOT NULL,
  PRIMARY KEY (`LinkID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of Links
-- ----------------------------
INSERT INTO `Links` VALUES ('00054d94-d4eb-b7a3-0000-000000000000', '2017-04-20 16:21:46.272602', '0', '/Uploads/68797ed045194719b7c4aaa2617db039.png', '\0', '\0', '中国新网', null, null, null, 'www.news.cn');

-- ----------------------------
-- Table structure for NewsArticles
-- ----------------------------
DROP TABLE IF EXISTS `NewsArticles`;
CREATE TABLE `NewsArticles` (
  `ArticleID` char(36) NOT NULL,
  `Author` varchar(50) DEFAULT NULL,
  `CategoryID` varchar(30) NOT NULL,
  `ClickNum` int(11) NOT NULL,
  `CreateDate` datetime NOT NULL,
  `Editor` varchar(50) DEFAULT NULL,
  `ImgUrl` varchar(250) DEFAULT NULL,
  `IsCommend` bit(1) NOT NULL,
  `IsComment` bit(1) NOT NULL,
  `IsHot` bit(1) NOT NULL,
  `IsImage` bit(1) NOT NULL,
  `IsLock` bit(1) NOT NULL,
  `IsSlide` bit(1) NOT NULL,
  `IsTop` bit(1) NOT NULL,
  `KeyWord` varchar(150) DEFAULT NULL,
  `OrgID` int(11) DEFAULT NULL,
  `Source` varchar(150) DEFAULT NULL,
  `Summarize` varchar(500) DEFAULT NULL,
  `Title` varchar(250) NOT NULL,
  `UpdateDate` datetime NOT NULL,
  `UserID` varchar(128) DEFAULT NULL,
  PRIMARY KEY (`ArticleID`),
  KEY `IX_NewsArticles_CategoryID` (`CategoryID`),
  CONSTRAINT `FK_NewsArticles_Category_CategoryID` FOREIGN KEY (`CategoryID`) REFERENCES `Category` (`CategoryID`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of NewsArticles
-- ----------------------------
INSERT INTO `NewsArticles` VALUES ('00054d94-ec55-7846-0000-000000000000', null, '591678553146261505', '0', '2017-04-20 16:28:19', null, '/Uploads/805a97a976674d2abc11b0ec6521ea61.png', '\0', '\0', '\0', '\0', '\0', '\0', '\0', null, null, null, null, 'asdfasdfasdf', '0001-01-01 00:00:00', null);

-- ----------------------------
-- Table structure for NewsArticleText
-- ----------------------------
DROP TABLE IF EXISTS `NewsArticleText`;
CREATE TABLE `NewsArticleText` (
  `ArticleTextID` bigint(20) NOT NULL AUTO_INCREMENT,
  `ArticleID` char(36) NOT NULL,
  `ArticleText` text,
  `NoHtml` text,
  PRIMARY KEY (`ArticleTextID`),
  KEY `IX_NewsArticleText_ArticleID` (`ArticleID`),
  CONSTRAINT `FK_NewsArticleText_NewsArticles_ArticleID` FOREIGN KEY (`ArticleID`) REFERENCES `NewsArticles` (`ArticleID`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=591691256409620481 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of NewsArticleText
-- ----------------------------
INSERT INTO `NewsArticleText` VALUES ('591691256409620480', '00054d94-ec55-7846-0000-000000000000', '<p>\r\n	asdfasdf\r\n</p>\r\n<p>\r\n	asdfasdf\r\n</p>\r\n<p>\r\n	asdfasdfas\r\n</p>', null);

-- ----------------------------
-- Table structure for NewsBanner
-- ----------------------------
DROP TABLE IF EXISTS `NewsBanner`;
CREATE TABLE `NewsBanner` (
  `Id` char(36) NOT NULL,
  `CreateTime` datetime(6) NOT NULL,
  `ImageUrl` varchar(256) DEFAULT NULL,
  `IsDeleted` bit(1) NOT NULL,
  `Sort` int(11) NOT NULL,
  `Title` varchar(128) NOT NULL,
  `Url` varchar(256) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of NewsBanner
-- ----------------------------
INSERT INTO `NewsBanner` VALUES ('00054d94-ec55-7c2e-0000-000000000000', '2017-04-20 16:29:44.357074', '/Uploads/4aa53b8c5dd247ffa91bbc9a15c6eee3.png', '\0', '0', 'asdfasdf', 'asdfasdf');
INSERT INTO `NewsBanner` VALUES ('00054de3-b0e1-1758-0000-000000000002', '2017-04-24 16:03:45.204098', null, '\0', '0', 'Test1', 'Test1Test1Test1Test1');
INSERT INTO `NewsBanner` VALUES ('00054de3-b0e1-1758-0000-000000000003', '2017-04-24 16:03:54.061236', null, '\0', '0', 'Test2', 'Test2Test2Test2');
INSERT INTO `NewsBanner` VALUES ('00054de3-b0e1-1758-0000-000000000004', '2017-04-24 16:30:38.364979', null, '\0', '0', 'Test3', 'Test3Test3Test3');
INSERT INTO `NewsBanner` VALUES ('00054de3-b0e1-1758-0000-000000000005', '2017-04-24 16:30:46.456517', null, '\0', '0', 'Test4', 'Test4Test4Test4');
INSERT INTO `NewsBanner` VALUES ('00054de9-470c-b9b7-0000-000000000000', '2017-04-24 21:06:38.290318', null, '\0', '0', '测试', 'www.text.com');
INSERT INTO `NewsBanner` VALUES ('00054de9-470c-b9b7-0000-000000000001', '2017-04-24 21:06:48.901935', null, '\0', '0', 'test', 'testtesttest');
INSERT INTO `NewsBanner` VALUES ('00054de9-470c-b9b7-0000-000000000002', '2017-04-24 21:06:53.663134', null, '\0', '0', 'testtesttest', 'testtesttest');
INSERT INTO `NewsBanner` VALUES ('00054df3-08c8-8303-0000-000000000000', '2017-04-25 08:45:03.307329', null, '\0', '0', 'Red Hat JBoss Data Grid 7.1 的新特性', 'Red Hat JBoss Data Grid 7.1 的新特性');
INSERT INTO `NewsBanner` VALUES ('00054df3-08c8-86cf-0000-000000000000', '2017-04-25 08:45:13.209927', null, '\0', '0', 'AppCode 2017.1.2 EAP 发布，Bug 修复版本', 'AppCode 2017.1.2 EAP 发布，Bug 修复版本');
INSERT INTO `NewsBanner` VALUES ('00054df3-08c8-86cf-0000-000000000001', '2017-04-25 08:45:22.569550', null, '\0', '0', 'Foreman 1.15.0-RC2，数据中心生命周期管理工具', 'Foreman 1.15.0-RC2，数据中心生命周期管理工具');
INSERT INTO `NewsBanner` VALUES ('00054df3-08c8-86cf-0000-000000000002', '2017-04-25 08:45:31.460417', null, '\0', '0', 'Ceph v12.0.2 Luminous (dev) 发布，开发者版本', 'Ceph v12.0.2 Luminous (dev) 发布，开发者版本');
INSERT INTO `NewsBanner` VALUES ('00054df3-08c8-86cf-0000-000000000003', '2017-04-25 08:45:42.217523', null, '\0', '0', 'Spring REST Docs 1.2.0 和 1.1.3 发布', 'Spring REST Docs 1.2.0 和 1.1.3 发布');
INSERT INTO `NewsBanner` VALUES ('00054df3-08c8-86cf-0000-000000000004', '2017-04-25 08:46:00.646726', null, '\0', '0', 'Linux Kernel 4.11-rc8 和 3.18.50 发布', null);
INSERT INTO `NewsBanner` VALUES ('00054df3-08c8-86cf-0000-000000000005', '2017-04-25 08:46:10.207527', null, '\0', '0', 'Android-x86 6.0 R3 发布，PC 上的安卓系统', 'Android-x86 6.0 R3 发布，PC 上的安卓系统');
INSERT INTO `NewsBanner` VALUES ('00054df3-08c8-86cf-0000-000000000006', '2017-04-25 08:46:19.064191', null, '\0', '0', 'NutzWk 4.1.1 发布，Java 企业级开源开发框架', 'NutzWk 4.1.1 发布，Java 企业级开源开发框架');
INSERT INTO `NewsBanner` VALUES ('00054df3-08c8-86cf-0000-000000000007', '2017-04-25 08:47:52.060767', null, '\0', '0', 'Jenkins 2.56 发布，可扩展的持续集成引擎', 'Jenkins 2.56 发布，可扩展的持续集成引擎');
INSERT INTO `NewsBanner` VALUES ('00054df3-08c8-86cf-0000-000000000008', '2017-04-25 08:48:00.650833', null, '\0', '0', 'Spring Integration 4.3.9 发布，Spring 消息通信', 'Spring Integration 4.3.9 发布，Spring 消息通信');
INSERT INTO `NewsBanner` VALUES ('00054df3-08c8-86cf-0000-000000000009', '2017-04-25 08:48:09.339541', null, '\0', '0', 'wfs 文件存储系统 0.0.2 版本发布', 'wfs 文件存储系统 0.0.2 版本发布');
INSERT INTO `NewsBanner` VALUES ('00054df3-08c8-86cf-0000-00000000000a', '2017-04-25 08:48:18.363220', null, '\0', '0', 'binder-swagger-java v0.8.0 发布', 'binder-swagger-java v0.8.0 发布');
INSERT INTO `NewsBanner` VALUES ('00054df3-08c8-86cf-0000-00000000000b', '2017-04-25 08:48:26.749173', null, '', '0', 'fdslight v4.0.2 发布，Linux 的 IP 层代理软件', 'fdslight v4.0.2 发布，Linux 的 IP 层代理软件');
INSERT INTO `NewsBanner` VALUES ('00054df3-08c8-86cf-0000-00000000000c', '2017-04-25 08:48:34.587271', null, '\0', '0', 'UCKeFu 1.3.0 发布，增加运营监控 API', 'UCKeFu 1.3.0 发布，增加运营监控 API');
INSERT INTO `NewsBanner` VALUES ('00054df3-08c8-86cf-0000-00000000000d', '2017-04-25 08:48:43.410742', null, '\0', '0', '禅道', '禅道 ');
INSERT INTO `NewsBanner` VALUES ('00054df3-08c8-86cf-0000-00000000000e', '2017-04-25 08:48:54.864170', null, '\0', '0', '然之协同 4.2.1 版本正式发布，bug 修复及优化性能', '然之协同 4.2.1 版本正式发布，bug 修复及优化性能');
INSERT INTO `NewsBanner` VALUES ('00054df3-08c8-86cf-0000-00000000000f', '2017-04-25 08:49:05.332214', null, '', '0', 'Hamsters.js 4.1.0，Javascript 多线程和并行执行库', 'Hamsters.js 4.1.0，Javascript 多线程和并行执行库');
INSERT INTO `NewsBanner` VALUES ('00054df3-08c8-86cf-0000-000000000010', '2017-04-25 08:49:13.000000', null, '\0', '3', 'uptimey 1.0.0 发布，服务器正常运行时间监视器', 'uptimey 1.0.0 发布，服务器正常运行时间监视器');
INSERT INTO `NewsBanner` VALUES ('00054df3-08c8-86cf-0000-000000000011', '2017-04-25 08:49:21.017195', null, '\0', '0', 'MySQL 8 中新的复制功能', 'MySQL 8 中新的复制功能');
INSERT INTO `NewsBanner` VALUES ('00054e0d-2749-f5bd-0000-000000000000', '2017-04-26 15:54:44.256117', null, '', '111', '哈哈哈', 'URL');
INSERT INTO `NewsBanner` VALUES ('00054e1b-e10c-2e13-0000-000000000000', '2017-04-27 09:28:50.313110', '/Uploads/2fc9ad522ddb4a78895f50189e06993c.jpg', '', '1', 'TestTestTestTestTest', '1Test');
INSERT INTO `NewsBanner` VALUES ('00054e21-cf85-d49a-0000-000000000000', '2017-04-27 16:33:26.100002', null, '', '1', 'DSDSDSD', '11DSDSDS');
INSERT INTO `NewsBanner` VALUES ('00054e27-c256-f5d9-0000-000000000000', '2017-04-27 23:39:14.727709', '/Uploads/210cde6eccf64335886a06caf880f292.png', '', '1', 'TestFuck', 'http://fuck.com');
INSERT INTO `NewsBanner` VALUES ('00054e2f-90c6-26d2-0000-000000000000', '2017-04-28 08:58:02.891900', '/Uploads/b0940859abd04846bc2b97b1257224e8.png', '\0', '1', '嘻嘻嘻', '');
INSERT INTO `NewsBanner` VALUES ('00054e2f-90c6-26d2-0000-000000000001', '2017-04-28 08:58:34.574896', '/Uploads/01bd96a6723e4d778baffe369d03111b.png', '\0', '1', 'asdfadsf', 'asdfasdf');

-- ----------------------------
-- Table structure for Sys_Application
-- ----------------------------
DROP TABLE IF EXISTS `Sys_Application`;
CREATE TABLE `Sys_Application` (
  `Id` varchar(128) NOT NULL,
  `ApplicationName` varchar(256) NOT NULL,
  `ApplicationUrl` varchar(256) DEFAULT NULL,
  `CreateTime` datetime(6) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of Sys_Application
-- ----------------------------
INSERT INTO `Sys_Application` VALUES ('543781988595662850', '基础应用', 'www.luckearth.cn', '2017-02-20 00:00:00.000000');
INSERT INTO `Sys_Application` VALUES ('590980354702049280', '测试应用', '测试应用', '2017-04-18 17:23:26.902305');
INSERT INTO `Sys_Application` VALUES ('593850657966915584', '测试应用1', '测试应用1', '2017-04-26 15:29:00.529424');
INSERT INTO `Sys_Application` VALUES ('593850657971109888', '测试应用2', '测试应用2', '2017-04-26 15:29:10.346424');
INSERT INTO `Sys_Application` VALUES ('593857002497638400', '测试应用2', '测试应用2', '2017-04-26 15:54:13.183858');
INSERT INTO `Sys_Application` VALUES ('593857002497638401', '测试应用2', '测试应用2', '2017-04-26 15:54:19.297233');
INSERT INTO `Sys_Application` VALUES ('593872833105362944', '测试应用2', '测试应用2', '2017-04-26 16:57:07.494349');
INSERT INTO `Sys_Application` VALUES ('593872833109557248', '测试应用2', '测试应用2', '2017-04-26 16:57:12.413418');
INSERT INTO `Sys_Application` VALUES ('593872833109557249', '测试应用2', '测试应用2', '2017-04-26 16:57:16.779418');
INSERT INTO `Sys_Application` VALUES ('593872833109557250', '测试应用2', '测试应用2', '2017-04-26 16:57:22.117484');
INSERT INTO `Sys_Application` VALUES ('593872833109557251', '测试应用2', '测试应用2', '2017-04-26 16:57:26.407482');
INSERT INTO `Sys_Application` VALUES ('596327270025527296', '测试应用12', '测试应用12', '2017-05-03 11:30:10.854906');
INSERT INTO `Sys_Application` VALUES ('596332626252398592', '测试应用13', '测试应用13', '2017-05-03 11:51:27.882717');
INSERT INTO `Sys_Application` VALUES ('596332626256592896', '测试应用13', '测试应用1', '2017-05-03 11:53:11.172840');
INSERT INTO `Sys_Application` VALUES ('596332626256592897', '测试应用14', '测试应用14', '2017-05-03 12:17:57.387101');
INSERT INTO `Sys_Application` VALUES ('596332626256592898', '测试应用15', '测试应用16', '2017-05-03 12:24:21.867824');

-- ----------------------------
-- Table structure for Sys_Modules
-- ----------------------------
DROP TABLE IF EXISTS `Sys_Modules`;
CREATE TABLE `Sys_Modules` (
  `Id` varchar(255) NOT NULL,
  `ActionName` longtext,
  `ApplicationId` varchar(255) DEFAULT NULL,
  `AreaName` longtext,
  `ControllerName` longtext,
  `CreateTime` datetime(6) NOT NULL,
  `Icon` longtext,
  `IsDelete` bit(1) NOT NULL,
  `IsExpand` bit(1) NOT NULL,
  `IsValidPurView` bit(1) NOT NULL,
  `ModuleDescription` longtext,
  `ModuleLayer` smallint(6) NOT NULL,
  `ModuleName` longtext,
  `ModuleType` int(11) NOT NULL,
  `ParentId` longtext,
  `PurviewNum` int(11) NOT NULL,
  `PurviewSum` bigint(20) NOT NULL,
  `Sort` int(11) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Sys_Modules_ApplicationId` (`ApplicationId`),
  CONSTRAINT `FK_Sys_Modules_Sys_Application_ApplicationId` FOREIGN KEY (`ApplicationId`) REFERENCES `Sys_Application` (`Id`) ON DELETE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of Sys_Modules
-- ----------------------------
INSERT INTO `Sys_Modules` VALUES ('547570545873387520', null, '543781988595662850', 'SysManager', '', '2016-12-19 22:28:21.000000', 'fa fa-gears', '\0', '', '', null, '0', '系统管理', '0', '0', '0', '0', '0');
INSERT INTO `Sys_Modules` VALUES ('547572040001912832', 'Index', '543781988595662850', 'SysManager', 'SysApplication', '2016-12-19 22:34:18.000000', 'iconfont icon-yingyong', '\0', '\0', '', null, '0', '应用程序', '0', '547570545873387520', '1', '0', '1');
INSERT INTO `Sys_Modules` VALUES ('547759560589312000', 'Create', '543781988595662850', 'SysManager', 'SysApplication', '2016-12-20 10:59:26.000000', null, '\0', '\0', '', null, '0', '添加应用', '1', '547570545873387520', '3', '8', '0');
INSERT INTO `Sys_Modules` VALUES ('547762503128449024', 'Edit', '543781988595662850', 'SysManager', 'SysApplication', '2016-12-20 11:11:08.000000', null, '\0', '\0', '', null, '0', '编辑应用', '1', '547570545873387520', '4', '16', '0');
INSERT INTO `Sys_Modules` VALUES ('547765923524640768', 'GetListViewModel', '543781988595662850', 'SysManager', 'SysApplication', '2016-12-20 11:24:44.000000', null, '\0', '\0', '', null, '0', 'Ajax获取列表', '2', '547570545873387520', '2', '4', '0');
INSERT INTO `Sys_Modules` VALUES ('547765923524640769', 'Delete', '543781988595662850', 'SysManager', 'SysApplication', '2016-12-20 11:26:10.000000', null, '\0', '\0', '', null, '0', '删除应用', '1', '547570545873387520', '5', '32', '0');
INSERT INTO `Sys_Modules` VALUES ('547770545626677248', null, '543781988595662850', 'SysManager', '', '2016-12-20 11:43:05.000000', null, '\0', '', '', null, '0', '资讯管理', '0', '0', '0', '0', '0');
INSERT INTO `Sys_Modules` VALUES ('547770545626677249', 'Index', '543781988595662850', 'SysManager', 'Category', '2016-12-20 11:44:35.000000', 'glyphicon glyphicon-paperclip', '\0', '\0', '', null, '0', '资讯分类', '0', '547770545626677248', '1', '0', '0');
INSERT INTO `Sys_Modules` VALUES ('547814827716771840', 'Index', '543781988595662850', 'SysManager', 'SysDepartment', '2016-12-20 14:39:03.000000', 'glyphicon glyphicon-tag', '\0', '\0', '', null, '0', '部门管理', '0', '547570545873387520', '1', '0', '2');
INSERT INTO `Sys_Modules` VALUES ('548185298446581760', 'Index', '543781988595662850', 'SysManager', 'SysModules', '2016-12-21 15:11:10.000000', 'glyphicon glyphicon-menu-hamburger', '\0', '\0', '', null, '0', '系统菜单', '0', '547570545873387520', '1', '2', '3');
INSERT INTO `Sys_Modules` VALUES ('548185298450776064', 'Index', '543781988595662850', 'SysManager', 'SysRoles', '2016-12-21 15:12:12.000000', 'iconfont icon-jiaoseguanli', '\0', '\0', '', null, '0', '角色管理', '0', '547570545873387520', '1', '2', '4');
INSERT INTO `Sys_Modules` VALUES ('548185298450776065', 'Index', '543781988595662850', 'SysManager', 'SysUsers', '2016-12-21 15:12:51.000000', 'glyphicon glyphicon-user', '\0', '\0', '', null, '0', '用户管理', '0', '547570545873387520', '1', '2', '5');
INSERT INTO `Sys_Modules` VALUES ('548287136848150528', 'GetListViewModel', '543781988595662850', 'SysManager', 'SysDepartment', '2016-12-21 21:55:50.000000', null, '\0', '\0', '', null, '0', 'Ajax获取列表', '2', '547570545873387520', '2', '0', '0');
INSERT INTO `Sys_Modules` VALUES ('548287136852344832', 'Create', '543781988595662850', 'SysManager', 'SysDepartment', '2016-12-21 21:56:38.000000', null, '\0', '\0', '', null, '0', '添加部门', '1', '547570545873387520', '3', '8', '0');
INSERT INTO `Sys_Modules` VALUES ('548287136852344833', 'Edit', '543781988595662850', 'SysManager', 'SysDepartment', '2016-12-21 21:57:35.000000', null, '\0', '\0', '', null, '0', '编辑部门', '1', '547570545873387520', '4', '16', '0');
INSERT INTO `Sys_Modules` VALUES ('548287136852344834', 'Delete', '543781988595662850', 'SysManager', 'SysDepartment', '2016-12-21 21:59:18.000000', null, '\0', '\0', '', null, '0', '删除部门', '1', '547570545873387520', '5', '32', '0');
INSERT INTO `Sys_Modules` VALUES ('548287136852344841', 'GetListViewModel', '543781988595662850', 'SysManager', 'SysModules', '2016-12-21 22:07:25.000000', null, '\0', '\0', '', null, '0', 'Ajax获取列表', '2', '547570545873387520', '2', '4', '0');
INSERT INTO `Sys_Modules` VALUES ('548287136852344842', 'Create', '543781988595662850', 'SysManager', 'SysModules', '2016-12-21 22:08:11.000000', null, '\0', '\0', '', null, '0', '添加模块', '1', '547570545873387520', '3', '8', '0');
INSERT INTO `Sys_Modules` VALUES ('548287136852344843', 'Edit', '543781988595662850', 'SysManager', 'SysModules', '2016-12-21 22:08:42.000000', null, '\0', '\0', '', null, '0', '编辑模块', '1', '547570545873387520', '4', '16', '0');
INSERT INTO `Sys_Modules` VALUES ('548287136852344844', 'Delete', '543781988595662850', 'SysManager', 'SysModules', '2016-12-21 22:09:21.000000', null, '\0', '\0', '', null, '0', '删除模块', '1', '547570545873387520', '5', '32', '0');
INSERT INTO `Sys_Modules` VALUES ('548287136852344845', 'ModuleSort', '543781988595662850', 'SysManager', 'SysModules', '2016-12-21 22:09:49.000000', null, '\0', '\0', '', null, '0', '模块排序', '1', '547570545873387520', '6', '64', '0');
INSERT INTO `Sys_Modules` VALUES ('548287136852344846', 'GetListViewModel', '543781988595662850', 'SysManager', 'SysUsers', '2016-12-21 22:10:53.000000', null, '\0', '\0', '', null, '0', 'Ajax获取列表', '2', '547570545873387520', '2', '4', '0');
INSERT INTO `Sys_Modules` VALUES ('548287136852344847', 'Create', '543781988595662850', 'SysManager', 'SysUsers', '2016-12-21 22:11:53.000000', null, '\0', '\0', '', null, '0', '添加用户', '1', '547570545873387520', '3', '8', '0');
INSERT INTO `Sys_Modules` VALUES ('548287136852344848', 'Edit', '543781988595662850', 'SysManager', 'SysUsers', '2016-12-21 22:13:02.000000', null, '\0', '\0', '', null, '0', '编辑用户', '1', '547570545873387520', '4', '0', '0');
INSERT INTO `Sys_Modules` VALUES ('548287136852344849', 'ChangePassword', '543781988595662850', 'SysManager', 'SysUsers', '2016-12-21 22:14:12.000000', null, '\0', '\0', '', null, '0', '修改密码', '1', '547570545873387520', '5', '32', '0');
INSERT INTO `Sys_Modules` VALUES ('548287136852344850', 'ValidateUserName', '543781988595662850', 'SysManager', 'SysUsers', '2016-12-21 22:14:59.000000', null, '\0', '\0', '', null, '0', '验证用户名', '2', '547570545873387520', '6', '64', '0');
INSERT INTO `Sys_Modules` VALUES ('548287136852344851', 'GetListViewModel', '543781988595662850', 'SysManager', 'SysRoles', '2016-12-21 22:15:58.000000', null, '\0', '\0', '', null, '0', 'Ajax获取列表', '2', '547570545873387520', '2', '4', '0');
INSERT INTO `Sys_Modules` VALUES ('548287136852344852', 'Create', '543781988595662850', 'SysManager', 'SysRoles', '2016-12-21 22:16:23.000000', null, '\0', '\0', '', null, '0', '添加角色', '1', '547570545873387520', '3', '8', '0');
INSERT INTO `Sys_Modules` VALUES ('548287136852344853', 'Edit', '543781988595662850', 'SysManager', 'SysRoles', '2016-12-21 22:16:53.000000', null, '\0', '\0', '', null, '0', '编辑角色', '1', '547570545873387520', '4', '16', '0');
INSERT INTO `Sys_Modules` VALUES ('548287136852344854', 'Delete', '543781988595662850', 'SysManager', 'SysRoles', '2016-12-21 22:17:22.000000', null, '\0', '\0', '', null, '0', '删除角色', '1', '547570545873387520', '5', '32', '0');
INSERT INTO `Sys_Modules` VALUES ('548287136852344855', 'EditPurview', '543781988595662850', 'SysManager', 'SysRoles', '2016-12-21 22:17:52.000000', null, '\0', '\0', '', null, '0', '角色权限', '1', '547570545873387520', '6', '0', '0');
INSERT INTO `Sys_Modules` VALUES ('548566221105135616', 'ValidateActionName', '543781988595662850', 'SysManager', 'SysModules', '2016-12-22 16:24:49.000000', null, '\0', '\0', '', null, '0', '验证操作名', '2', '547570545873387520', '7', '0', '0');
INSERT INTO `Sys_Modules` VALUES ('549649110777462784', 'Create', '543781988595662850', 'SysManager', 'Category', '2016-12-25 16:07:50.000000', null, '\0', '\0', '', null, '0', '资讯分类添加', '1', '547770545626677248', '2', '4', '0');
INSERT INTO `Sys_Modules` VALUES ('549649110777462785', 'Edit', '543781988595662850', 'SysManager', 'Category', '2016-12-25 16:08:31.000000', null, '\0', '\0', '', null, '0', '资讯分类编辑', '1', '547770545626677248', '3', '8', '0');
INSERT INTO `Sys_Modules` VALUES ('549649110777462786', 'Delete', '543781988595662850', 'SysManager', 'Category', '2016-12-25 16:09:04.000000', null, '\0', '\0', '', null, '0', '资讯分类删除', '1', '547770545626677248', '4', '16', '0');
INSERT INTO `Sys_Modules` VALUES ('549649110777462787', 'GetListViewModel', '543781988595662850', 'SysManager', 'Category', '2016-12-25 16:10:21.000000', null, '\0', '\0', '', null, '0', 'Ajax获取列表', '2', '547770545626677248', '5', '32', '0');
INSERT INTO `Sys_Modules` VALUES ('549695441390796805', 'Index', '543781988595662850', 'SysManager', 'Links', '2016-12-25 19:13:29.000000', 'glyphicon glyphicon-link', '\0', '\0', '', null, '0', '友情链接', '0', '547770545626677248', '1', '0', '0');
INSERT INTO `Sys_Modules` VALUES ('549695441390796813', 'GetListViewModel', '543781988595662850', 'SysManager', 'Links', '2016-12-25 19:19:22.000000', null, '\0', '\0', '', null, '0', 'Ajax获取列表', '2', '547770545626677248', '2', '4', '0');
INSERT INTO `Sys_Modules` VALUES ('549695441390796814', 'Create', '543781988595662850', 'SysManager', 'Links', '2016-12-25 19:20:02.000000', null, '\0', '\0', '', null, '0', '链接添加', '1', '547770545626677248', '3', '8', '0');
INSERT INTO `Sys_Modules` VALUES ('549695441390796815', 'Edit', '543781988595662850', 'SysManager', 'Links', '2016-12-25 19:21:10.000000', null, '\0', '\0', '', null, '0', '链接编辑', '1', '547770545626677248', '4', '16', '0');
INSERT INTO `Sys_Modules` VALUES ('549695441390796816', 'Delete', '543781988595662850', 'SysManager', 'Links', '2016-12-25 19:23:55.000000', null, '\0', '\0', '', null, '0', '删除链接', '1', '547770545626677248', '5', '32', '0');
INSERT INTO `Sys_Modules` VALUES ('549695441390796817', 'Index', '543781988595662850', 'SysManager', 'NewsArticle', '2016-12-25 19:28:35.000000', null, '\0', '\0', '', null, '0', '资讯管理', '0', '547770545626677248', '1', '2', '0');
INSERT INTO `Sys_Modules` VALUES ('549695441390796818', 'Index', '543781988595662850', 'SysManager', 'NewsBanner', '2016-12-25 19:29:17.000000', null, '\0', '\0', '', null, '0', 'Banner管理', '0', '547770545626677248', '1', '2', '0');
INSERT INTO `Sys_Modules` VALUES ('549695441390796837', 'Create', '543781988595662850', 'SysManager', 'NewsBanner', '2016-12-25 19:33:19.000000', null, '\0', '\0', '', null, '0', 'Banner添加', '1', '547770545626677248', '2', '4', '0');
INSERT INTO `Sys_Modules` VALUES ('549695441390796838', 'Edit', '543781988595662850', 'SysManager', 'NewsBanner', '2016-12-25 19:33:53.000000', null, '\0', '\0', '', null, '0', 'Banner编辑', '1', '547770545626677248', '3', '8', '0');
INSERT INTO `Sys_Modules` VALUES ('549695441390796839', 'GetListViewModel', '543781988595662850', 'SysManager', 'NewsBanner', '2016-12-25 19:34:18.000000', null, '\0', '\0', '', null, '0', 'Ajax获取列表', '2', '547770545626677248', '4', '16', '0');
INSERT INTO `Sys_Modules` VALUES ('549695441390796840', 'Delete', '543781988595662850', 'SysManager', 'NewsBanner', '2016-12-25 19:37:29.000000', null, '\0', '\0', '', null, '0', 'Banner删除', '1', '547770545626677248', '5', '32', '0');

-- ----------------------------
-- Table structure for Sys_RoleModules
-- ----------------------------
DROP TABLE IF EXISTS `Sys_RoleModules`;
CREATE TABLE `Sys_RoleModules` (
  `ModuleId` varchar(255) NOT NULL,
  `RoleId` varchar(255) NOT NULL,
  `ApplicationId` longtext,
  `ControllerName` longtext,
  `PurviewSum` bigint(20) NOT NULL,
  PRIMARY KEY (`ModuleId`,`RoleId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of Sys_RoleModules
-- ----------------------------
INSERT INTO `Sys_RoleModules` VALUES ('591585785950502912', '546695139276357632', '', 'SysApplication', '60');
INSERT INTO `Sys_RoleModules` VALUES ('591585785950502913', '546695139276357632', '', 'SysDepartment', '56');
INSERT INTO `Sys_RoleModules` VALUES ('591585785950502914', '546695139276357632', '', 'SysModules', '126');
INSERT INTO `Sys_RoleModules` VALUES ('591585785950502915', '546695139276357632', '', 'SysRoles', '62');
INSERT INTO `Sys_RoleModules` VALUES ('591585785950502916', '546695139276357632', '', 'SysUsers', '110');
INSERT INTO `Sys_RoleModules` VALUES ('591585785950502917', '546695139276357632', '', 'Category', '60');
INSERT INTO `Sys_RoleModules` VALUES ('591585785950502918', '546695139276357632', '', 'Links', '60');
INSERT INTO `Sys_RoleModules` VALUES ('591585785950502919', '546695139276357632', '', 'NewsArticle', '2');
INSERT INTO `Sys_RoleModules` VALUES ('591585785950502920', '546695139276357632', '', 'NewsBanner', '62');

-- ----------------------------
-- Table structure for Sys_Roles
-- ----------------------------
DROP TABLE IF EXISTS `Sys_Roles`;
CREATE TABLE `Sys_Roles` (
  `Id` varchar(255) NOT NULL,
  `ConcurrencyStamp` longtext,
  `CreateTime` datetime(6) NOT NULL,
  `IsAllowDelete` bit(1) NOT NULL,
  `IsDelete` bit(1) NOT NULL,
  `Name` varchar(256) DEFAULT NULL,
  `NormalizedName` varchar(256) DEFAULT NULL,
  `RoleDescription` longtext,
  `RoleName` longtext,
  `RoleType` int(11) NOT NULL,
  `Sort` int(11) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `RoleNameIndex` (`NormalizedName`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of Sys_Roles
-- ----------------------------
INSERT INTO `Sys_Roles` VALUES ('546695139276357632', null, '2017-02-22 00:00:00.000000', '\0', '\0', null, '管理员', null, '管理员', '1', '1');

-- ----------------------------
-- Table structure for Sys_Users
-- ----------------------------
DROP TABLE IF EXISTS `Sys_Users`;
CREATE TABLE `Sys_Users` (
  `Id` varchar(255) NOT NULL,
  `AccessFailedCount` int(11) NOT NULL,
  `ConcurrencyStamp` longtext,
  `CreateTime` datetime(6) NOT NULL,
  `DepartmentId` longtext,
  `Email` varchar(256) DEFAULT NULL,
  `EmailConfirmed` bit(1) NOT NULL,
  `FullName` longtext,
  `IsLock` bit(1) NOT NULL,
  `LockoutEnabled` bit(1) NOT NULL,
  `LockoutEnd` datetime(6) DEFAULT NULL,
  `NormalizedEmail` varchar(256) DEFAULT NULL,
  `NormalizedUserName` varchar(256) DEFAULT NULL,
  `PasswordHash` longtext,
  `PhoneNumber` longtext,
  `PhoneNumberConfirmed` bit(1) NOT NULL,
  `SecurityStamp` longtext,
  `TwoFactorEnabled` bit(1) NOT NULL,
  `UserName` varchar(256) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `UserNameIndex` (`NormalizedUserName`),
  KEY `EmailIndex` (`NormalizedEmail`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of Sys_Users
-- ----------------------------
INSERT INTO `Sys_Users` VALUES ('00054917-04ba-3ae6-0000-000000000000', '0', '75a90037-4831-47b6-ad54-6797f9e39c42', '2017-02-22 13:33:39.000000', null, 'luckearth@luckearth.cn', '\0', '管理员', '\0', '', null, 'LUCKEARTH@LUCKEARTH.CN', 'ADMIN', 'AQAAAAEAACcQAAAAED5283OeGThqRkYMtRlEMYqwm0zZg9O10Hak+8/FXjo7gmHTIL9AISlltHxVaaURdQ==', null, '\0', '24946247-3651-44c9-9bc2-03a80f00e75a', '\0', 'admin');

-- ----------------------------
-- Table structure for __EFMigrationsHistory
-- ----------------------------
DROP TABLE IF EXISTS `__EFMigrationsHistory`;
CREATE TABLE `__EFMigrationsHistory` (
  `MigrationId` varchar(95) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of __EFMigrationsHistory
-- ----------------------------
INSERT INTO `__EFMigrationsHistory` VALUES ('20170222032033_Init', '1.1.0-rtm-22752');
INSERT INTO `__EFMigrationsHistory` VALUES ('20170420015050_deleteDepartmentindex', '1.1.1');
INSERT INTO `__EFMigrationsHistory` VALUES ('20170420021838_AddNewSystem', '1.1.1');
SET FOREIGN_KEY_CHECKS=1;
