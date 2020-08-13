/*
Navicat MySQL Data Transfer

Source Server         : MyDataBase
Source Server Version : 80019
Source Host           : 127.0.0.1:3306
Source Database       : weilan_fire_control

Target Server Type    : MYSQL
Target Server Version : 80019
File Encoding         : 65001

Date: 2020-08-12 17:01:59
*/
CREATE DATABASE IF NOT EXISTS weilan_fire_control;
USE weilan_fire_control;
SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for battery
-- ----------------------------
DROP TABLE IF EXISTS `battery`;
CREATE TABLE `battery` (
  `battery_code` varchar(64) NOT NULL COMMENT '电池条码',
  `test_no` int NOT NULL DEFAULT '1' COMMENT '测试次数',
  `ocv` decimal(18,6) NOT NULL DEFAULT '0.000000' COMMENT '开路电压(mV)',
  `shell_v` decimal(18,6) NOT NULL DEFAULT '0.000000' COMMENT '壳体电压(mV)',
  `dv` decimal(18,6) NOT NULL DEFAULT '0.000000' COMMENT '放电电压(mV)',
  `acir` decimal(18,6) NOT NULL DEFAULT '0.000000' COMMENT '交流内阻(mΩ)',
  `dcir` decimal(18,6) NOT NULL DEFAULT '0.000000' COMMENT '直流内阻(mΩ)',
  `temp` decimal(9,3) NOT NULL DEFAULT '0.000' COMMENT '环境温度(℃)',
  `kval` decimal(18,6) NOT NULL DEFAULT '0.000000' COMMENT 'K值',
  `proc_id` varchar(32) NOT NULL DEFAULT '' COMMENT '工艺编号',
  `ops_id` varchar(32) NOT NULL DEFAULT '' COMMENT '工序编号',
  `proj_id` varchar(32) NOT NULL DEFAULT '' COMMENT '项目编号',
  `batch_id` varchar(32) NOT NULL DEFAULT '' COMMENT '批次编号',
  `battery_type_id` varchar(32) NOT NULL DEFAULT '' COMMENT '电池型号编号',
  `tray_code` varchar(64) NOT NULL DEFAULT '' COMMENT '托盘条码',
  `create_time` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `result` int NOT NULL DEFAULT '0' COMMENT '测试结果(见BatteryResult枚举)',
  `result_time` datetime DEFAULT NULL COMMENT '结果时间',
  PRIMARY KEY (`battery_code`,`test_no`),
  KEY `create_time` (`create_time`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='电池测试数据';

-- ----------------------------
-- Records of battery
-- ----------------------------

-- ----------------------------
-- Table structure for battery_result_code
-- ----------------------------
DROP TABLE IF EXISTS `battery_result_code`;
CREATE TABLE `battery_result_code` (
  `result_code` varchar(64) NOT NULL COMMENT '结果代码',
  `custom_code` varchar(64) NOT NULL DEFAULT '' COMMENT '自定义代码',
  `remark` varchar(1024) NOT NULL DEFAULT '' COMMENT '备注',
  PRIMARY KEY (`result_code`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='电池自定义结果代码';

-- ----------------------------
-- Records of battery_result_code
-- ----------------------------

-- ----------------------------
-- Table structure for cell
-- ----------------------------
DROP TABLE IF EXISTS `cell`;
CREATE TABLE `cell` (
  `cell_id` int NOT NULL COMMENT '库位编号',
  `product_line` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL COMMENT '生产线',
  `type` int NOT NULL COMMENT '库位当前类型（1-电池托盘，2-校准工装，3-空托盘）',
  `cell_status` int NOT NULL COMMENT '库位状态(0-工作中， 1-出盘中，2-完成， 3-有异常, 4-空, 5-其他托盘，6-数据已上传MES)',
  `row` int DEFAULT NULL COMMENT '在产线中属于第几号',
  `col` int DEFAULT NULL COMMENT '在产线中属于第几列',
  `lay` int DEFAULT NULL,
  `last_update_time` datetime NOT NULL COMMENT '最后一次更新时间',
  `remark` varchar(255) DEFAULT NULL COMMENT '备注',
  `extend_field1` varchar(255) DEFAULT NULL COMMENT '扩展字段1',
  `extend_field2` varchar(255) DEFAULT NULL COMMENT '扩展字段2',
  `extend_field3` varchar(255) DEFAULT NULL COMMENT '扩展字段3',
  PRIMARY KEY (`cell_id`),
  KEY `rcl_index` (`row`,`col`,`lay`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of cell
-- ----------------------------
INSERT INTO `cell` VALUES ('52', null, '1', '0', '1', '7', '1', '2020-07-16 13:52:14', null, null, null, null);
INSERT INTO `cell` VALUES ('53', null, '1', '0', '1', '7', '2', '2020-07-16 13:52:14', null, null, null, null);
INSERT INTO `cell` VALUES ('54', null, '1', '0', '1', '7', '3', '2020-07-16 13:52:14', null, null, null, null);
INSERT INTO `cell` VALUES ('55', null, '1', '0', '1', '7', '4', '2020-07-16 13:52:14', null, null, null, null);
INSERT INTO `cell` VALUES ('56', null, '1', '0', '1', '6', '1', '2020-07-16 13:52:14', null, null, null, null);
INSERT INTO `cell` VALUES ('57', null, '1', '0', '1', '6', '2', '2020-07-16 13:52:14', null, null, null, null);
INSERT INTO `cell` VALUES ('58', null, '1', '0', '1', '6', '3', '2020-07-16 13:52:14', null, null, null, null);
INSERT INTO `cell` VALUES ('59', null, '1', '0', '1', '6', '4', '2020-07-16 13:52:14', null, null, null, null);
INSERT INTO `cell` VALUES ('60', null, '1', '0', '1', '5', '1', '2020-07-16 13:52:14', null, null, null, null);
INSERT INTO `cell` VALUES ('61', null, '1', '0', '1', '5', '2', '2020-07-16 17:13:33', null, null, null, null);
INSERT INTO `cell` VALUES ('62', null, '1', '0', '1', '5', '3', '2020-07-16 17:13:38', null, null, null, null);
INSERT INTO `cell` VALUES ('63', null, '1', '0', '1', '5', '4', '2020-07-16 13:52:14', null, null, null, null);
INSERT INTO `cell` VALUES ('64', null, '1', '0', '1', '4', '1', '2020-07-16 13:52:14', null, null, null, null);
INSERT INTO `cell` VALUES ('65', null, '1', '0', '1', '4', '2', '2020-07-16 13:52:14', null, null, null, null);
INSERT INTO `cell` VALUES ('66', null, '2', '0', '1', '4', '3', '2020-07-30 19:03:18', null, null, null, null);
INSERT INTO `cell` VALUES ('67', null, '1', '0', '1', '4', '4', '2020-07-16 13:52:14', null, null, null, null);
INSERT INTO `cell` VALUES ('68', null, '1', '0', '1', '3', '1', '2020-07-16 13:52:14', null, null, null, null);
INSERT INTO `cell` VALUES ('69', null, '1', '0', '1', '3', '2', '2020-07-16 13:52:14', null, null, null, null);
INSERT INTO `cell` VALUES ('70', null, '1', '0', '1', '3', '3', '2020-07-16 13:52:14', null, null, null, null);
INSERT INTO `cell` VALUES ('71', null, '1', '0', '1', '3', '4', '2020-07-16 13:52:14', null, null, null, null);
INSERT INTO `cell` VALUES ('72', null, '1', '0', '1', '2', '1', '2020-07-16 13:52:14', null, null, null, null);
INSERT INTO `cell` VALUES ('73', null, '1', '0', '1', '2', '2', '2020-07-16 13:52:14', null, null, null, null);
INSERT INTO `cell` VALUES ('74', null, '1', '0', '1', '2', '3', '2020-07-16 16:39:56', null, null, null, null);
INSERT INTO `cell` VALUES ('75', null, '1', '0', '1', '2', '4', '2020-07-16 13:52:14', null, null, null, null);
INSERT INTO `cell` VALUES ('76', null, '1', '0', '1', '1', '1', '2020-07-17 16:37:08', null, null, null, null);
INSERT INTO `cell` VALUES ('77', null, '1', '0', '1', '1', '2', '2020-07-16 13:52:14', null, null, null, null);
INSERT INTO `cell` VALUES ('78', null, '1', '0', '1', '1', '3', '2020-07-16 13:52:14', null, null, null, null);
INSERT INTO `cell` VALUES ('79', null, '1', '0', '1', '1', '4', '2020-08-03 14:09:23', null, null, null, null);

-- ----------------------------
-- Table structure for func
-- ----------------------------
DROP TABLE IF EXISTS `func`;
CREATE TABLE `func` (
  `func_id` int NOT NULL AUTO_INCREMENT COMMENT '功能ID',
  `func_code` varchar(64) NOT NULL COMMENT '功能代码',
  `name` varchar(64) NOT NULL COMMENT '名称',
  `remark` varchar(1024) NOT NULL DEFAULT '' COMMENT '说明',
  `order_idx` int NOT NULL DEFAULT '100' COMMENT '排序(ASC)',
  `parent_id` int NOT NULL DEFAULT '0' COMMENT '父ID',
  PRIMARY KEY (`func_id`),
  UNIQUE KEY `func_code` (`func_code`),
  KEY `parent_id` (`parent_id`,`order_idx`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='功能';

-- ----------------------------
-- Records of func
-- ----------------------------

-- ----------------------------
-- Table structure for logs
-- ----------------------------
DROP TABLE IF EXISTS `logs`;
CREATE TABLE `logs` (
  `log_id` int NOT NULL AUTO_INCREMENT COMMENT '日志ID',
  `title` varchar(128) NOT NULL DEFAULT '' COMMENT '标题',
  `cont` mediumtext NOT NULL COMMENT '内容',
  `type` int NOT NULL DEFAULT '0' COMMENT '类型(见LogType枚举)',
  `create_time` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '生成时间',
  PRIMARY KEY (`log_id`),
  KEY `type` (`type`),
  KEY `create_time` (`create_time`)
) ENGINE=InnoDB AUTO_INCREMENT=168 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='系统日志';

-- ----------------------------
-- Records of logs
-- ----------------------------
-- ----------------------------
-- Table structure for ops
-- ----------------------------
DROP TABLE IF EXISTS `ops`;
CREATE TABLE `ops` (
  `ops_id` int NOT NULL AUTO_INCREMENT COMMENT '工序ID',
  `ops_name` varchar(64) NOT NULL COMMENT '工序名称',
  `ops_val` varchar(64) NOT NULL COMMENT '工序值',
  `remark` varchar(1024) NOT NULL DEFAULT '' COMMENT '备注',
  PRIMARY KEY (`ops_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='工序';

-- ----------------------------
-- Records of ops
-- ----------------------------

-- ----------------------------
-- Table structure for role
-- ----------------------------
DROP TABLE IF EXISTS `role`;
CREATE TABLE `role` (
  `role_id` int NOT NULL AUTO_INCREMENT COMMENT '角色ID',
  `name` varchar(32) NOT NULL COMMENT '名称',
  `remark` varchar(1024) NOT NULL DEFAULT '' COMMENT '备注',
  PRIMARY KEY (`role_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='角色';

-- ----------------------------
-- Records of role
-- ----------------------------

-- ----------------------------
-- Table structure for role_func
-- ----------------------------
DROP TABLE IF EXISTS `role_func`;
CREATE TABLE `role_func` (
  `role_id` int NOT NULL COMMENT '角色ID',
  `func_id` int NOT NULL COMMENT '功能ID',
  `is_select` bit(1) NOT NULL DEFAULT b'0' COMMENT '是否拥有读取权限',
  `is_insert` bit(1) NOT NULL DEFAULT b'0' COMMENT '是否拥有添加权限',
  `is_update` bit(1) NOT NULL DEFAULT b'0' COMMENT '是否拥有更新权限',
  `is_delete` bit(1) NOT NULL DEFAULT b'0' COMMENT '是否拥有删除权限',
  PRIMARY KEY (`role_id`,`func_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='角色授权';

-- ----------------------------
-- Records of role_func
-- ----------------------------

-- ----------------------------
-- Table structure for station
-- ----------------------------
DROP TABLE IF EXISTS `station`;
CREATE TABLE `station` (
  `station_id` varchar(32) NOT NULL COMMENT '工位ID',
  `name` varchar(32) NOT NULL COMMENT '名称(用于显示)',
  `type` int NOT NULL COMMENT '类型(见StationType枚举)',
  `location` int NOT NULL DEFAULT '0' COMMENT '位置序号(起始1递增1,近上下料水车值小)',
  `remark` varchar(1024) NOT NULL DEFAULT '' COMMENT '备注',
  `is_disable` bit(1) NOT NULL DEFAULT b'0' COMMENT '是否停用',
  `is_lock_fire` bit(1) NOT NULL DEFAULT b'0' COMMENT '是否消防锁定',
  `is_lock_real` bit(1) NOT NULL DEFAULT b'0' COMMENT '是否实时交互锁定',
  `real_fail_times` int NOT NULL DEFAULT '0' COMMENT '实时交互失败次数',
  `status` int NOT NULL DEFAULT '0' COMMENT '工位状态(见StationStatus枚举)',
  `status_time` datetime NOT NULL COMMENT '工位状态变更时间',
  PRIMARY KEY (`station_id`),
  KEY `type` (`type`,`location`),
  KEY `status` (`status`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='工位';

-- ----------------------------
-- Records of station
-- ----------------------------

-- ----------------------------
-- Table structure for station_cell
-- ----------------------------
DROP TABLE IF EXISTS `station_cell`;
CREATE TABLE `station_cell` (
  `station_id` varchar(32) NOT NULL COMMENT '工位ID',
  `cell_no` int NOT NULL COMMENT '单元编号(起始1递增1)',
  `battery_code` varchar(32) NOT NULL DEFAULT '' COMMENT '单元绑定电池',
  `battery_time` datetime DEFAULT NULL COMMENT '电池绑定时间',
  `is_fake` bit(1) NOT NULL DEFAULT b'0' COMMENT '是否为假电池',
  PRIMARY KEY (`station_id`,`cell_no`),
  KEY `battery_code` (`battery_code`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='工位单元';

-- ----------------------------
-- Records of station_cell
-- ----------------------------

-- ----------------------------
-- Table structure for sys_event
-- ----------------------------
DROP TABLE IF EXISTS `sys_event`;
CREATE TABLE `sys_event` (
  `event_id` int NOT NULL AUTO_INCREMENT COMMENT '事件ID',
  `title` varchar(128) NOT NULL DEFAULT '' COMMENT '标题',
  `cont` text NOT NULL COMMENT '内容',
  `level` int NOT NULL DEFAULT '1' COMMENT '级别(见SysEventLevel枚举)',
  `create_time` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '生成时间',
  PRIMARY KEY (`event_id`),
  KEY `level` (`level`),
  KEY `create_time` (`create_time`)
) ENGINE=InnoDB AUTO_INCREMENT=153 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='系统事件';

-- ----------------------------
-- Records of sys_event
-- ----------------------------
-- ----------------------------
-- Table structure for sys_para
-- ----------------------------
DROP TABLE IF EXISTS `sys_para`;
CREATE TABLE `sys_para` (
  `para_id` varchar(64) NOT NULL COMMENT '参数ID',
  `para_type` int NOT NULL DEFAULT '1' COMMENT '参数类型(见SysParaType枚举)',
  `para_val` mediumtext NOT NULL COMMENT '参数值',
  `name` varchar(128) NOT NULL COMMENT '参数名称',
  `remark` varchar(1024) NOT NULL DEFAULT '' COMMENT '备注',
  `is_show` bit(1) NOT NULL DEFAULT b'0' COMMENT '是否软件可见',
  `show_idx` int NOT NULL DEFAULT '100' COMMENT '显示排序(ASC)',
  `sort_id` int NOT NULL COMMENT '分类ID',
  PRIMARY KEY (`para_id`),
  KEY `sort_id` (`sort_id`,`show_idx`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='系统参数';

-- ----------------------------
-- Records of sys_para
-- ----------------------------
INSERT INTO `sys_para` VALUES ('AddrByModBus', '1', 'R0', 'Modbus读取地址', 'Modbus读取地址', '', '1', '3');
INSERT INTO `sys_para` VALUES ('BtsRemoteAddress', '1', 'net.tcp://localhost:8014/TcpServiceForBTS', 'BTS提供的WCF地址', 'BTS提供的WCF地址', '', '4', '4');
INSERT INTO `sys_para` VALUES ('cellMakerCode', '1', 'cellMakerCode', '工位厂商编码', '工位厂商编码', '', '5', '7');
INSERT INTO `sys_para` VALUES ('ComTitle', '1', '卫蓝消防', '软件名称', '界面显示的软件名称', '', '0', '1');
INSERT INTO `sys_para` VALUES ('CurrentProcesses', '2', '0', '当前设备在MES的工序', '当前设备在MES的工序', '', '0', '7');
INSERT INTO `sys_para` VALUES ('CurrentUser', '2', '0', '当前用户ID', '当前用户ID，调用MES接口时用到', '', '1', '7');
INSERT INTO `sys_para` VALUES ('FireIpAddrModbus', '1', '127.0.0.1', '温度检测Modbus服务器Ip地址', '温度检测Modbus服务器Ip地址', '', '1', '3');
INSERT INTO `sys_para` VALUES ('FirePortAddrModbus', '1', '8888', '温度检测Modbus端口', '温度检查Modbus端口', '', '1', '3');
INSERT INTO `sys_para` VALUES ('HttpServiceForBTS', '1', 'http://localhost:8013/FcServiceForBTS', '和BTS通讯的HTTP地址', '和BTS通讯的HTTP地址', '', '1', '4');
INSERT INTO `sys_para` VALUES ('HttpServiceForMES', '1', 'http://localhost:10301/FcService', '和MES通讯的HTTP地址', '和MES通讯的HTTP地址', '', '3', '4');
INSERT INTO `sys_para` VALUES ('IsDebug', '4', 'True', '调试模式', 'True:设备调试模式，不涉及第三方系统。False:关闭调试模式。', '', '0', '1');
INSERT INTO `sys_para` VALUES ('OpenFaultMsg', '4', 'False', '是否开启故障信息提示', '是否开启故障信息提示', '', '0', '3');
INSERT INTO `sys_para` VALUES ('OpenFireFlag', '4', 'False', '是否开启消防判断', '是否开启消防判断', '', '0', '3');
INSERT INTO `sys_para` VALUES ('OpsFileType', '2', '1', '工步文件存放类型', '工步文件存放类型：1-本地路径 2-局域网 3-URL', '', '3', '7');
INSERT INTO `sys_para` VALUES ('OutByInterface', '4', 'False', '出盘是否通过WCF接口调用', '出盘是否通过WCF接口调用', '\0', '7', '7');
INSERT INTO `sys_para` VALUES ('PlcTcpIpHost', '1', '127.0.0.1', 'PLC网络IP地址', 'PLC网络IP地址', '', '10', '2');
INSERT INTO `sys_para` VALUES ('PlcTcpIpPost', '2', '8845', 'PLC网络主机端口', 'PLC网络主机端口', '', '11', '2');
INSERT INTO `sys_para` VALUES ('ProcID', '2', '1', '工艺ID', '工艺ID', '', '0', '7');
INSERT INTO `sys_para` VALUES ('SlaveAddress', '2', '127', '从站地址', '从站地址(范围0-255)', '', '1', '3');
INSERT INTO `sys_para` VALUES ('TcpServiceForBTS', '1', 'net.tcp://localhost:8012/FcServiceForBTS', '和BTS通讯的TCP地址', '和BTS通讯的TCP地址', '', '0', '4');
INSERT INTO `sys_para` VALUES ('TcpServiceForMES', '1', 'net.tcp://localhost:10300/FcService', '和MES通讯的TCP地址', '和MES通讯的TCP地址', '', '2', '4');

-- ----------------------------
-- Table structure for sys_para_sort
-- ----------------------------
DROP TABLE IF EXISTS `sys_para_sort`;
CREATE TABLE `sys_para_sort` (
  `sort_id` int NOT NULL AUTO_INCREMENT COMMENT '分类ID',
  `name` varchar(128) NOT NULL COMMENT '分类名称',
  `remark` varchar(1024) NOT NULL DEFAULT '' COMMENT '备注',
  `show_idx` int NOT NULL DEFAULT '100' COMMENT '显示排序(ASC)',
  PRIMARY KEY (`sort_id`),
  KEY `show_idx` (`show_idx`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='系统参数分类';

-- ----------------------------
-- Records of sys_para_sort
-- ----------------------------
INSERT INTO `sys_para_sort` VALUES ('1', '通用参数', '通用参数相关配置', '0');
INSERT INTO `sys_para_sort` VALUES ('2', 'PLC', 'PLC相关配置', '0');
INSERT INTO `sys_para_sort` VALUES ('3', '消防', '消防（感温光纤等）配置', '0');
INSERT INTO `sys_para_sort` VALUES ('4', 'WCF', 'WCF相关配置', '0');
INSERT INTO `sys_para_sort` VALUES ('7', 'MES', 'MES相关配置', '0');

-- ----------------------------
-- Table structure for timed_task
-- ----------------------------
DROP TABLE IF EXISTS `timed_task`;
CREATE TABLE `timed_task` (
  `task_id` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '主键',
  `task_status` int NOT NULL COMMENT '任务状态（0 - 运行中 1- 结束 2 - 异常）',
  `tray_code` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL COMMENT '托盘类型',
  `cell_id` int NOT NULL COMMENT '库位编号',
  `processes` int DEFAULT NULL COMMENT '工序id',
  `create_time` datetime NOT NULL COMMENT '创建日期',
  `last_update_time` datetime NOT NULL COMMENT '最后更新时间',
  PRIMARY KEY (`task_id`),
  KEY `index1` (`cell_id`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of timed_task
-- ----------------------------

-- ----------------------------
-- Table structure for tray_map
-- ----------------------------
DROP TABLE IF EXISTS `tray_map`;
CREATE TABLE `tray_map` (
  `tray_code` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '托盘编码',
  `cell_id` int NOT NULL COMMENT '库位ID',
  `processes` int NOT NULL COMMENT '工序ID',
  `exec_status` int NOT NULL COMMENT '库位状态(0-工作中， 1-出盘中，2-完成， 3-有异常, 4-空, 5-其他托盘，6-数据已上传MES)',
  `type` int NOT NULL COMMENT '托盘类型（1 - 电池托盘 2- 校准 3-其他）',
  `mes_model` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '从MES获取的数据模型',
  `bts_result` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci COMMENT 'BTS返回的数据模型',
  `last_update_time` datetime NOT NULL COMMENT '最后更新时间',
  PRIMARY KEY (`tray_code`,`cell_id`),
  KEY `cell_index` (`cell_id`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of tray_map
-- ----------------------------
INSERT INTO `tray_map` VALUES ('1', '62', '1', '4', '1', '1', null, '2020-07-16 17:13:38');
INSERT INTO `tray_map` VALUES ('trayCode_0716', '66', '1', '1', '1', '{\"TrayCode\":\"trayCode_0716\",\"TrayAttr\":1,\"CellID\":79,\"OpsValue\":1,\"BatteryCodes\":null,\"Channels\":null,\"FilePath\":null,\"FilePathType\":1,\"FileSetting\":null,\"Times\":1,\"Enabled\":true,\"Message\":null,\"CreateTime\":\"2020-07-16T16:53:09.6501125+08:00\",\"UserId\":0}', '{\"TrayCode\":\"trayCode_0716\",\"TrayAttr\":1,\"CellID\":79,\"OpsValue\":1,\"Times\":0,\"UserID\":0,\"TestResult\":{\"Cell\":\"1\",\"Ops\":null,\"StartTime\":\"0001-01-01T00:00:00\",\"OverTime\":\"0001-01-01T00:00:00\",\"TrayCode\":\"trayCode_0716\",\"CellTemps\":null,\"ChannelResult\":{\"1\":{\"BatteryCode\":\"1212111\",\"ChargeCap\":0,\"DischargeCap\":0,\"StopCap\":0,\"StartVol\":0,\"StopVol\":0,\"ChargeCurrentVolRate\":0.0,\"StopCurrent\":0,\"Dcir\":0.0,\"DcirCom\":0.0,\"V1\":0,\"V2\":0,\"ChargeEnergy\":0,\"DischargeEnergy\":0,\"ChargeCurrentTime\":0,\"ChargeVolTime\":0,\"ChargeTime\":0,\"DischargeTime\":0,\"StartTime\":\"0001-01-01T00:00:00\",\"OverTime\":\"0001-01-01T00:00:00\",\"Status\":null,\"Location\":null,\"Temp\":0.0,\"StepResult\":[]}}},\"ComleteModels\":[],\"CreateTime\":\"2020-08-03T14:09:22.7208184+08:00\"}', '2020-08-11 11:41:50');
INSERT INTO `tray_map` VALUES ('trayCode_07166', '76', '1', '0', '1', '{\"TrayCode\":\"trayCode_07166\",\"TrayAttr\":1,\"CellID\":76,\"OpsValue\":1,\"BatteryCodes\":null,\"Channels\":null,\"FilePath\":null,\"FilePathType\":1,\"FileSetting\":null,\"Times\":1,\"Enabled\":true,\"Message\":null,\"CreateTime\":\"2020-07-17T16:35:43.435212+08:00\",\"UserId\":0}', null, '2020-07-17 16:36:47');

-- ----------------------------
-- Table structure for user
-- ----------------------------
DROP TABLE IF EXISTS `user`;
CREATE TABLE `user` (
  `user_id` int NOT NULL AUTO_INCREMENT COMMENT '用户ID',
  `account` varchar(128) NOT NULL COMMENT '登录账号',
  `pswd` varchar(512) NOT NULL COMMENT '登录密码(SHA2)',
  `name` varchar(32) NOT NULL COMMENT '姓名',
  `gender` int NOT NULL DEFAULT '0' COMMENT '性别(见Gender枚举)',
  `tel` varchar(32) NOT NULL DEFAULT '' COMMENT '联系电话',
  `im` varchar(128) NOT NULL DEFAULT '' COMMENT '联系IM',
  `email` varchar(128) NOT NULL DEFAULT '' COMMENT '电子邮件',
  `remark` varchar(1024) NOT NULL DEFAULT '' COMMENT '备注',
  `login_time` datetime DEFAULT NULL COMMENT '最后登录时间',
  `login_count` int NOT NULL DEFAULT '0' COMMENT '登录总次数',
  `status` int NOT NULL DEFAULT '1' COMMENT '状态(见UserStatus枚举)',
  `status_time` datetime NOT NULL COMMENT '状态变更时间',
  `create_time` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  PRIMARY KEY (`user_id`),
  UNIQUE KEY `account` (`account`),
  KEY `status` (`status`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='用户';

-- ----------------------------
-- Records of user
-- ----------------------------

-- ----------------------------
-- Table structure for user_log
-- ----------------------------
DROP TABLE IF EXISTS `user_log`;
CREATE TABLE `user_log` (
  `log_id` int NOT NULL AUTO_INCREMENT COMMENT '日志ID',
  `user_id` int NOT NULL COMMENT '用户ID',
  `func_id` int NOT NULL COMMENT '功能ID',
  `ops_type` int NOT NULL COMMENT '操作类型(见OpsType枚举)',
  `cont` varchar(4096) NOT NULL DEFAULT '' COMMENT '操作内容',
  `create_time` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '操作时间',
  PRIMARY KEY (`log_id`),
  KEY `user_id` (`user_id`),
  KEY `func_id` (`func_id`),
  KEY `ops_type` (`ops_type`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='用户日志';

-- ----------------------------
-- Records of user_log
-- ----------------------------

-- ----------------------------
-- Table structure for user_role
-- ----------------------------
DROP TABLE IF EXISTS `user_role`;
CREATE TABLE `user_role` (
  `user_id` int NOT NULL COMMENT '用户ID',
  `role_id` int NOT NULL COMMENT '角色ID',
  PRIMARY KEY (`user_id`,`role_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='用户角色';

-- ----------------------------
-- Records of user_role
-- ----------------------------

-- ----------------------------
-- Procedure structure for pager
-- ----------------------------
DROP PROCEDURE IF EXISTS `pager`;
DELIMITER ;;
CREATE DEFINER=`root`@`%` PROCEDURE `pager`(

	IN	in_fields			TEXT,				/*查询字段*/
	IN	in_tables			TEXT,				/*表名*/
	IN	in_where			TEXT,				/*WHERE 语句(包含WHERE关键字,可为空,GROUP BY必须包含在此参数中)*/
	IN	in_order			TEXT,				/*ORDER BY 语句(包含ORDER BY关键字,可为空)*/
	IN	in_pagesize			INT,				/*每页记录数*/
	IN	in_pageindex		INT,				/*当前页*/
	OUT	out_rows			INT					/*输出记录总数*/
)
    COMMENT '分页存储过程'
BEGIN

	/*定义变量*/
	DECLARE m_begin_row INT DEFAULT 0;
	DECLARE m_limit TEXT;

	/*构造语句*/	
	SET m_begin_row = (in_pageindex - 1) * in_pagesize;
	SET m_limit = CONCAT(' LIMIT ', m_begin_row, ', ', in_pagesize);
	
	SET @COUNT_STRING = CONCAT('SELECT COUNT(*) INTO @ROWS_TOTAL FROM (SELECT ', in_fields, ' FROM ', in_tables, ' ', in_where, ') temp_table_pager');
	SET @MAIN_STRING = CONCAT('SELECT ', in_fields, ' FROM ', in_tables, ' ', in_where, ' ', in_order, m_limit);

	/*预处理*/
	PREPARE count_stmt FROM @COUNT_STRING;
	EXECUTE count_stmt;
	DEALLOCATE PREPARE count_stmt;
	SET out_rows = @ROWS_TOTAL;

	PREPARE main_stmt FROM @MAIN_STRING;
	EXECUTE main_stmt;
	DEALLOCATE PREPARE main_stmt;
	
END
;;
DELIMITER ;
