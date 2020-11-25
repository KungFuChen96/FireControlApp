/*
Navicat MySQL Data Transfer

Source Server         : MyDataBase
Source Server Version : 80019
Source Host           : 127.0.0.1:3306
Source Database       : weilan_fire_control

Target Server Type    : MYSQL
Target Server Version : 80019
File Encoding         : 65001

Date: 2020-08-27 17:27:40
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
  `type` int NOT NULL COMMENT '库位当前类型（1-分容静置 2-分容压床 3-高温静置）',
  `cell_status` int NOT NULL COMMENT '库位状态(0-正常， 1-温度异常，2-烟雾报警，3-故障，4-其他异常)',
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
INSERT INTO `cell` VALUES ('52', null, '2', '0', '1', '7', '1', '2020-08-21 11:15:18', null, '7-1', null, '25');
INSERT INTO `cell` VALUES ('53', null, '2', '0', '1', '7', '2', '2020-07-16 13:52:14', null, '7-2', null, '26');
INSERT INTO `cell` VALUES ('54', null, '2', '0', '1', '7', '3', '2020-08-21 09:53:20', null, '7-3', null, '27');
INSERT INTO `cell` VALUES ('55', null, '2', '0', '1', '7', '4', '2020-07-16 13:52:14', null, '7-4', null, '28');
INSERT INTO `cell` VALUES ('56', null, '2', '0', '1', '6', '1', '2020-08-21 14:11:27', null, '6-1', null, '21');
INSERT INTO `cell` VALUES ('57', null, '2', '0', '1', '6', '2', '2020-07-16 13:52:14', null, '6-2', null, '22');
INSERT INTO `cell` VALUES ('58', null, '2', '0', '1', '6', '3', '2020-08-21 09:53:20', null, '6-3', null, '23');
INSERT INTO `cell` VALUES ('59', null, '2', '0', '1', '6', '4', '2020-07-16 13:52:14', null, '6-4', null, '24');
INSERT INTO `cell` VALUES ('60', null, '2', '0', '1', '5', '1', '2020-08-21 10:55:34', null, '5-1', null, '17');
INSERT INTO `cell` VALUES ('61', null, '2', '0', '1', '5', '2', '2020-07-16 17:13:33', null, '5-2', null, '18');
INSERT INTO `cell` VALUES ('62', null, '2', '0', '1', '5', '3', '2020-08-21 09:53:20', null, '5-3', null, '19');
INSERT INTO `cell` VALUES ('63', null, '2', '0', '1', '5', '4', '2020-07-16 13:52:14', null, '5-4', null, '20');
INSERT INTO `cell` VALUES ('64', null, '2', '0', '1', '4', '1', '2020-08-21 10:55:32', null, '4-1', null, '13');
INSERT INTO `cell` VALUES ('65', null, '2', '0', '1', '4', '2', '2020-07-16 13:52:14', null, '4-2', null, '14');
INSERT INTO `cell` VALUES ('66', null, '2', '0', '1', '4', '3', '2020-08-21 09:53:20', null, '4-3', null, '15');
INSERT INTO `cell` VALUES ('67', null, '2', '0', '1', '4', '4', '2020-08-21 10:55:29', null, '4-4', null, '16');
INSERT INTO `cell` VALUES ('68', null, '2', '0', '1', '3', '1', '2020-08-21 10:26:29', null, '3-1', null, '9');
INSERT INTO `cell` VALUES ('69', null, '2', '0', '1', '3', '2', '2020-07-16 13:52:14', null, '3-2', null, '10');
INSERT INTO `cell` VALUES ('70', null, '2', '0', '1', '3', '3', '2020-08-21 09:53:13', null, '3-3', null, '11');
INSERT INTO `cell` VALUES ('71', null, '2', '0', '1', '3', '4', '2020-07-16 13:52:14', null, '3-4', null, '12');
INSERT INTO `cell` VALUES ('72', null, '2', '0', '1', '2', '1', '2020-08-21 09:53:13', null, '2-1', null, '5');
INSERT INTO `cell` VALUES ('73', null, '2', '0', '1', '2', '2', '2020-07-16 13:52:14', null, '2-2', null, '6');
INSERT INTO `cell` VALUES ('74', null, '2', '0', '1', '2', '3', '2020-08-21 09:53:13', null, '2-3', null, '7');
INSERT INTO `cell` VALUES ('75', null, '2', '0', '1', '2', '4', '2020-08-12 17:08:34', null, '2-4', null, '8');
INSERT INTO `cell` VALUES ('76', null, '2', '0', '1', '1', '1', '2020-08-21 09:53:13', null, '1-1', null, '1');
INSERT INTO `cell` VALUES ('77', null, '2', '0', '1', '1', '2', '2020-07-16 13:52:14', null, '1-2', null, '2');
INSERT INTO `cell` VALUES ('78', null, '2', '0', '1', '1', '3', '2020-08-21 11:15:15', null, '1-3', null, '3');
INSERT INTO `cell` VALUES ('79', null, '2', '0', '1', '1', '4', '2020-08-03 14:09:23', null, '1-4', null, '4');
INSERT INTO `cell` VALUES ('330', null, '1', '0', '1', '9', '1', '2020-08-26 10:05:26', null, '300', null, null);
INSERT INTO `cell` VALUES ('331', null, '1', '0', '1', '8', '1', '2020-08-26 10:05:26', null, '301', null, null);
INSERT INTO `cell` VALUES ('332', null, '1', '0', '1', '7', '1', '2020-08-26 10:05:26', null, '302', null, null);
INSERT INTO `cell` VALUES ('333', null, '1', '0', '1', '6', '1', '2020-08-26 10:05:26', null, '303', null, null);
INSERT INTO `cell` VALUES ('334', null, '1', '0', '1', '5', '1', '2020-08-26 10:05:26', null, '304', null, null);
INSERT INTO `cell` VALUES ('335', null, '1', '0', '1', '4', '1', '2020-08-26 10:05:26', null, '305', null, null);
INSERT INTO `cell` VALUES ('336', null, '1', '0', '1', '3', '1', '2020-08-26 10:05:26', null, '306', null, null);
INSERT INTO `cell` VALUES ('337', null, '1', '0', '1', '2', '1', '2020-08-26 10:05:26', null, '307', null, null);
INSERT INTO `cell` VALUES ('338', null, '1', '0', '1', '1', '1', '2020-08-26 10:05:26', null, '308', null, null);
INSERT INTO `cell` VALUES ('339', null, '1', '0', '1', '9', '2', '2020-08-26 10:05:26', null, '317', null, null);
INSERT INTO `cell` VALUES ('340', null, '1', '0', '1', '8', '2', '2020-08-26 10:05:26', null, '316', null, null);
INSERT INTO `cell` VALUES ('341', null, '1', '0', '1', '7', '2', '2020-08-26 10:05:26', null, '315', null, null);
INSERT INTO `cell` VALUES ('342', null, '1', '0', '1', '6', '2', '2020-08-26 10:05:26', null, '314', null, null);
INSERT INTO `cell` VALUES ('343', null, '1', '0', '1', '5', '2', '2020-08-26 10:05:26', null, '313', null, null);
INSERT INTO `cell` VALUES ('344', null, '1', '0', '1', '4', '2', '2020-08-26 10:05:26', null, '312', null, null);
INSERT INTO `cell` VALUES ('345', null, '1', '0', '1', '3', '2', '2020-08-26 10:05:26', null, '311', null, null);
INSERT INTO `cell` VALUES ('346', null, '1', '0', '1', '2', '2', '2020-08-26 10:05:26', null, '310', null, null);
INSERT INTO `cell` VALUES ('347', null, '1', '0', '1', '1', '2', '2020-08-26 10:05:26', null, '309', null, null);
INSERT INTO `cell` VALUES ('348', null, '1', '0', '1', '9', '3', '2020-08-26 10:05:26', null, '318', null, null);
INSERT INTO `cell` VALUES ('349', null, '1', '0', '1', '8', '3', '2020-08-26 10:05:26', null, '319', null, null);
INSERT INTO `cell` VALUES ('350', null, '1', '0', '1', '7', '3', '2020-08-26 10:05:26', null, '320', null, null);
INSERT INTO `cell` VALUES ('351', null, '1', '0', '1', '6', '3', '2020-08-26 10:05:26', null, '321', null, null);
INSERT INTO `cell` VALUES ('352', null, '1', '0', '1', '5', '3', '2020-08-26 10:05:26', null, '322', null, null);
INSERT INTO `cell` VALUES ('353', null, '1', '0', '1', '4', '3', '2020-08-26 10:05:26', null, '323', null, null);
INSERT INTO `cell` VALUES ('354', null, '1', '0', '1', '3', '3', '2020-08-26 10:05:26', null, '324', null, null);
INSERT INTO `cell` VALUES ('355', null, '1', '0', '1', '2', '3', '2020-08-26 10:05:26', null, '325', null, null);
INSERT INTO `cell` VALUES ('356', null, '1', '0', '1', '1', '3', '2020-08-26 10:05:26', null, '326', null, null);
INSERT INTO `cell` VALUES ('357', null, '1', '0', '1', '9', '4', '2020-08-26 10:05:26', null, '335', null, null);
INSERT INTO `cell` VALUES ('358', null, '1', '0', '1', '8', '4', '2020-08-26 10:05:26', null, '334', null, null);
INSERT INTO `cell` VALUES ('359', null, '1', '0', '1', '7', '4', '2020-08-26 10:05:26', null, '333', null, null);
INSERT INTO `cell` VALUES ('360', null, '1', '0', '1', '6', '4', '2020-08-26 10:05:26', null, '332', null, null);
INSERT INTO `cell` VALUES ('361', null, '1', '0', '1', '5', '4', '2020-08-26 10:05:26', null, '331', null, null);
INSERT INTO `cell` VALUES ('362', null, '1', '0', '1', '4', '4', '2020-08-26 10:05:26', null, '330', null, null);
INSERT INTO `cell` VALUES ('363', null, '1', '0', '1', '3', '4', '2020-08-26 10:05:26', null, '329', null, null);
INSERT INTO `cell` VALUES ('364', null, '1', '0', '1', '2', '4', '2020-08-26 10:05:26', null, '328', null, null);
INSERT INTO `cell` VALUES ('365', null, '1', '0', '1', '1', '4', '2020-08-26 10:05:26', null, '327', null, null);
INSERT INTO `cell` VALUES ('366', null, '1', '0', '1', '9', '5', '2020-08-26 10:05:26', null, '336', null, null);
INSERT INTO `cell` VALUES ('367', null, '1', '0', '1', '8', '5', '2020-08-26 10:05:26', null, '337', null, null);
INSERT INTO `cell` VALUES ('368', null, '1', '0', '1', '7', '5', '2020-08-26 10:05:26', null, '338', null, null);
INSERT INTO `cell` VALUES ('369', null, '1', '0', '1', '6', '5', '2020-08-26 10:05:26', null, '339', null, null);
INSERT INTO `cell` VALUES ('370', null, '1', '0', '1', '5', '5', '2020-08-26 10:05:26', null, '340', null, null);
INSERT INTO `cell` VALUES ('371', null, '1', '0', '1', '4', '5', '2020-08-26 10:05:26', null, '341', null, null);
INSERT INTO `cell` VALUES ('372', null, '1', '0', '1', '3', '5', '2020-08-26 10:05:26', null, '342', null, null);
INSERT INTO `cell` VALUES ('373', null, '1', '0', '1', '2', '5', '2020-08-26 10:05:26', null, '343', null, null);
INSERT INTO `cell` VALUES ('374', null, '1', '0', '1', '1', '5', '2020-08-26 10:05:26', null, '344', null, null);
INSERT INTO `cell` VALUES ('375', null, '1', '0', '1', '9', '6', '2020-08-26 10:05:26', null, '353', null, null);
INSERT INTO `cell` VALUES ('376', null, '1', '0', '1', '8', '6', '2020-08-26 10:05:26', null, '352', null, null);
INSERT INTO `cell` VALUES ('377', null, '1', '0', '1', '7', '6', '2020-08-26 10:05:26', null, '351', null, null);
INSERT INTO `cell` VALUES ('378', null, '1', '0', '1', '6', '6', '2020-08-26 10:05:26', null, '350', null, null);
INSERT INTO `cell` VALUES ('379', null, '1', '0', '1', '5', '6', '2020-08-26 10:05:26', null, '349', null, null);
INSERT INTO `cell` VALUES ('380', null, '1', '0', '1', '4', '6', '2020-08-26 10:05:26', null, '348', null, null);
INSERT INTO `cell` VALUES ('381', null, '1', '0', '1', '3', '6', '2020-08-26 10:05:26', null, '347', null, null);
INSERT INTO `cell` VALUES ('382', null, '1', '0', '1', '2', '6', '2020-08-26 10:05:26', null, '346', null, null);
INSERT INTO `cell` VALUES ('383', null, '1', '0', '1', '1', '6', '2020-08-26 10:05:26', null, '345', null, null);
INSERT INTO `cell` VALUES ('384', null, '1', '0', '1', '9', '7', '2020-08-26 10:05:26', null, '354', null, null);
INSERT INTO `cell` VALUES ('385', null, '1', '0', '1', '8', '7', '2020-08-26 10:05:26', null, '355', null, null);
INSERT INTO `cell` VALUES ('386', null, '1', '0', '1', '7', '7', '2020-08-26 10:05:26', null, '356', null, null);
INSERT INTO `cell` VALUES ('387', null, '1', '0', '1', '6', '7', '2020-08-26 10:05:26', null, '357', null, null);
INSERT INTO `cell` VALUES ('388', null, '1', '0', '1', '5', '7', '2020-08-26 10:05:26', null, '358', null, null);
INSERT INTO `cell` VALUES ('389', null, '1', '0', '1', '4', '7', '2020-08-26 10:05:26', null, '359', null, null);
INSERT INTO `cell` VALUES ('390', null, '1', '0', '1', '3', '7', '2020-08-26 10:05:26', null, '360', null, null);
INSERT INTO `cell` VALUES ('391', null, '1', '0', '1', '2', '7', '2020-08-26 10:05:26', null, '361', null, null);
INSERT INTO `cell` VALUES ('392', null, '1', '0', '1', '1', '7', '2020-08-26 10:05:26', null, '362', null, null);
INSERT INTO `cell` VALUES ('393', null, '1', '0', '2', '36', '1', '2020-08-26 10:06:01', null, '85', null, null);
INSERT INTO `cell` VALUES ('394', null, '1', '0', '2', '35', '1', '2020-08-26 10:06:01', null, '84', null, null);
INSERT INTO `cell` VALUES ('395', null, '1', '0', '2', '34', '1', '2020-08-26 10:06:01', null, '83', null, null);
INSERT INTO `cell` VALUES ('396', null, '1', '0', '2', '33', '1', '2020-08-26 10:06:01', null, '82', null, null);
INSERT INTO `cell` VALUES ('397', null, '1', '0', '2', '32', '1', '2020-08-26 10:06:01', null, '81', null, null);
INSERT INTO `cell` VALUES ('398', null, '1', '0', '2', '31', '1', '2020-08-26 10:06:01', null, '80', null, null);
INSERT INTO `cell` VALUES ('399', null, '1', '0', '2', '30', '1', '2020-08-26 10:06:01', null, '79', null, null);
INSERT INTO `cell` VALUES ('400', null, '1', '0', '2', '29', '1', '2020-08-26 10:06:01', null, '78', null, null);
INSERT INTO `cell` VALUES ('401', null, '1', '0', '2', '28', '1', '2020-08-26 10:06:01', null, '77', null, null);
INSERT INTO `cell` VALUES ('402', null, '1', '0', '2', '27', '1', '2020-08-26 10:06:01', null, '76', null, null);
INSERT INTO `cell` VALUES ('403', null, '1', '0', '2', '26', '1', '2020-08-26 10:06:01', null, '75', null, null);
INSERT INTO `cell` VALUES ('404', null, '1', '0', '2', '25', '1', '2020-08-26 10:06:01', null, '74', null, null);
INSERT INTO `cell` VALUES ('405', null, '1', '0', '2', '24', '1', '2020-08-26 10:06:01', null, '73', null, null);
INSERT INTO `cell` VALUES ('406', null, '1', '0', '2', '23', '1', '2020-08-26 10:06:01', null, '72', null, null);
INSERT INTO `cell` VALUES ('407', null, '1', '0', '2', '22', '1', '2020-08-26 10:06:01', null, '71', null, null);
INSERT INTO `cell` VALUES ('408', null, '1', '0', '2', '21', '1', '2020-08-26 10:06:01', null, '70', null, null);
INSERT INTO `cell` VALUES ('409', null, '1', '0', '2', '20', '1', '2020-08-26 10:06:02', null, '69', null, null);
INSERT INTO `cell` VALUES ('410', null, '1', '0', '2', '19', '1', '2020-08-26 10:06:02', null, '68', null, null);
INSERT INTO `cell` VALUES ('411', null, '1', '0', '2', '18', '1', '2020-08-26 10:06:02', null, '67', null, null);
INSERT INTO `cell` VALUES ('412', null, '1', '0', '2', '17', '1', '2020-08-26 10:06:02', null, '66', null, null);
INSERT INTO `cell` VALUES ('413', null, '1', '0', '2', '16', '1', '2020-08-26 10:06:02', null, '65', null, null);
INSERT INTO `cell` VALUES ('414', null, '1', '0', '2', '15', '1', '2020-08-26 10:06:02', null, '64', null, null);
INSERT INTO `cell` VALUES ('415', null, '1', '0', '2', '14', '1', '2020-08-26 10:06:02', null, '63', null, null);
INSERT INTO `cell` VALUES ('416', null, '1', '0', '2', '13', '1', '2020-08-26 10:06:02', null, '62', null, null);
INSERT INTO `cell` VALUES ('417', null, '1', '0', '2', '12', '1', '2020-08-26 10:06:02', null, '61', null, null);
INSERT INTO `cell` VALUES ('418', null, '1', '0', '2', '11', '1', '2020-08-26 10:06:02', null, '60', null, null);
INSERT INTO `cell` VALUES ('419', null, '1', '0', '2', '10', '1', '2020-08-26 10:06:02', null, '59', null, null);
INSERT INTO `cell` VALUES ('420', null, '1', '0', '2', '9', '1', '2020-08-26 10:06:02', null, '58', null, null);
INSERT INTO `cell` VALUES ('421', null, '1', '0', '2', '8', '1', '2020-08-26 10:06:02', null, '57', null, null);
INSERT INTO `cell` VALUES ('422', null, '1', '0', '2', '7', '1', '2020-08-26 10:06:02', null, '56', null, null);
INSERT INTO `cell` VALUES ('423', null, '1', '0', '2', '6', '1', '2020-08-26 10:06:02', null, '55', null, null);
INSERT INTO `cell` VALUES ('424', null, '1', '0', '2', '5', '1', '2020-08-26 10:06:02', null, '54', null, null);
INSERT INTO `cell` VALUES ('425', null, '1', '0', '2', '0', '0', '2020-08-26 10:06:02', null, '', null, null);
INSERT INTO `cell` VALUES ('426', null, '1', '0', '2', '0', '0', '2020-08-26 10:06:02', null, '', null, null);
INSERT INTO `cell` VALUES ('427', null, '1', '0', '2', '2', '1', '2020-08-26 10:06:02', null, '118', null, null);
INSERT INTO `cell` VALUES ('428', null, '1', '0', '2', '1', '1', '2020-08-26 10:06:02', null, '119', null, null);
INSERT INTO `cell` VALUES ('429', null, '1', '0', '2', '36', '2', '2020-08-26 10:06:02', null, '86', null, null);
INSERT INTO `cell` VALUES ('430', null, '1', '0', '2', '35', '2', '2020-08-26 10:06:02', null, '87', null, null);
INSERT INTO `cell` VALUES ('431', null, '1', '0', '2', '34', '2', '2020-08-26 10:06:02', null, '88', null, null);
INSERT INTO `cell` VALUES ('432', null, '1', '0', '2', '33', '2', '2020-08-26 10:06:02', null, '89', null, null);
INSERT INTO `cell` VALUES ('433', null, '1', '0', '2', '32', '2', '2020-08-26 10:06:02', null, '90', null, null);
INSERT INTO `cell` VALUES ('434', null, '1', '0', '2', '31', '2', '2020-08-26 10:06:02', null, '91', null, null);
INSERT INTO `cell` VALUES ('435', null, '1', '0', '2', '30', '2', '2020-08-26 10:06:02', null, '92', null, null);
INSERT INTO `cell` VALUES ('436', null, '1', '0', '2', '29', '2', '2020-08-26 10:06:02', null, '93', null, null);
INSERT INTO `cell` VALUES ('437', null, '1', '0', '2', '28', '2', '2020-08-26 10:06:02', null, '94', null, null);
INSERT INTO `cell` VALUES ('438', null, '1', '0', '2', '27', '2', '2020-08-26 10:06:02', null, '95', null, null);
INSERT INTO `cell` VALUES ('439', null, '1', '0', '2', '26', '2', '2020-08-26 10:06:02', null, '96', null, null);
INSERT INTO `cell` VALUES ('440', null, '1', '0', '2', '25', '2', '2020-08-26 10:06:02', null, '97', null, null);
INSERT INTO `cell` VALUES ('441', null, '1', '0', '2', '24', '2', '2020-08-26 10:06:02', null, '98', null, null);
INSERT INTO `cell` VALUES ('442', null, '1', '0', '2', '23', '2', '2020-08-26 10:06:02', null, '99', null, null);
INSERT INTO `cell` VALUES ('443', null, '1', '0', '2', '22', '2', '2020-08-26 10:06:02', null, '100', null, null);
INSERT INTO `cell` VALUES ('444', null, '1', '0', '2', '21', '2', '2020-08-26 10:06:02', null, '101', null, null);
INSERT INTO `cell` VALUES ('445', null, '1', '0', '2', '20', '2', '2020-08-26 10:06:02', null, '102', null, null);
INSERT INTO `cell` VALUES ('446', null, '1', '0', '2', '19', '2', '2020-08-26 10:06:02', null, '103', null, null);
INSERT INTO `cell` VALUES ('447', null, '1', '0', '2', '18', '2', '2020-08-26 10:06:02', null, '104', null, null);
INSERT INTO `cell` VALUES ('448', null, '1', '0', '2', '17', '2', '2020-08-26 10:06:02', null, '105', null, null);
INSERT INTO `cell` VALUES ('449', null, '1', '0', '2', '16', '2', '2020-08-26 10:06:02', null, '106', null, null);
INSERT INTO `cell` VALUES ('450', null, '1', '0', '2', '15', '2', '2020-08-26 10:06:02', null, '107', null, null);
INSERT INTO `cell` VALUES ('451', null, '1', '0', '2', '14', '2', '2020-08-26 10:06:02', null, '108', null, null);
INSERT INTO `cell` VALUES ('452', null, '1', '0', '2', '13', '2', '2020-08-26 10:06:02', null, '109', null, null);
INSERT INTO `cell` VALUES ('453', null, '1', '0', '2', '12', '2', '2020-08-26 10:06:02', null, '110', null, null);
INSERT INTO `cell` VALUES ('454', null, '1', '0', '2', '11', '2', '2020-08-26 10:06:02', null, '111', null, null);
INSERT INTO `cell` VALUES ('455', null, '1', '0', '2', '10', '2', '2020-08-26 10:06:02', null, '112', null, null);
INSERT INTO `cell` VALUES ('456', null, '1', '0', '2', '9', '2', '2020-08-26 10:06:01', null, '113', null, null);
INSERT INTO `cell` VALUES ('457', null, '1', '0', '2', '8', '2', '2020-08-26 10:06:01', null, '114', null, null);
INSERT INTO `cell` VALUES ('458', null, '1', '0', '2', '7', '2', '2020-08-26 10:06:01', null, '115', null, null);
INSERT INTO `cell` VALUES ('459', null, '1', '0', '2', '6', '2', '2020-08-26 10:06:01', null, '116', null, null);
INSERT INTO `cell` VALUES ('460', null, '1', '0', '2', '5', '2', '2020-08-26 10:06:01', null, '117', null, null);
INSERT INTO `cell` VALUES ('461', null, '1', '0', '2', '0', '0', '2020-08-26 10:06:01', null, '', null, null);
INSERT INTO `cell` VALUES ('462', null, '1', '0', '2', '0', '0', '2020-08-26 10:06:01', null, '', null, null);
INSERT INTO `cell` VALUES ('463', null, '1', '0', '2', '2', '2', '2020-08-26 10:06:01', null, '121', null, null);
INSERT INTO `cell` VALUES ('464', null, '1', '0', '2', '1', '2', '2020-08-26 10:06:01', null, '120', null, null);
INSERT INTO `cell` VALUES ('465', null, '1', '0', '2', '36', '3', '2020-08-26 10:06:01', null, '155', null, null);
INSERT INTO `cell` VALUES ('466', null, '1', '0', '2', '35', '3', '2020-08-26 10:06:01', null, '154', null, null);
INSERT INTO `cell` VALUES ('467', null, '1', '0', '2', '34', '3', '2020-08-26 10:06:01', null, '153', null, null);
INSERT INTO `cell` VALUES ('468', null, '1', '0', '2', '33', '3', '2020-08-26 10:06:01', null, '152', null, null);
INSERT INTO `cell` VALUES ('469', null, '1', '0', '2', '32', '3', '2020-08-26 10:06:01', null, '151', null, null);
INSERT INTO `cell` VALUES ('470', null, '1', '0', '2', '31', '3', '2020-08-26 10:06:01', null, '150', null, null);
INSERT INTO `cell` VALUES ('471', null, '1', '0', '2', '30', '3', '2020-08-26 10:06:01', null, '149', null, null);
INSERT INTO `cell` VALUES ('472', null, '1', '0', '2', '29', '3', '2020-08-26 10:06:02', null, '148', null, null);
INSERT INTO `cell` VALUES ('473', null, '1', '0', '2', '28', '3', '2020-08-26 10:06:02', null, '147', null, null);
INSERT INTO `cell` VALUES ('474', null, '1', '0', '2', '27', '3', '2020-08-26 10:06:02', null, '146', null, null);
INSERT INTO `cell` VALUES ('475', null, '1', '0', '2', '26', '3', '2020-08-26 10:06:02', null, '145', null, null);
INSERT INTO `cell` VALUES ('476', null, '1', '0', '2', '25', '3', '2020-08-26 10:06:02', null, '144', null, null);
INSERT INTO `cell` VALUES ('477', null, '1', '0', '2', '24', '3', '2020-08-26 10:06:02', null, '143', null, null);
INSERT INTO `cell` VALUES ('478', null, '1', '0', '2', '23', '3', '2020-08-26 10:06:02', null, '142', null, null);
INSERT INTO `cell` VALUES ('479', null, '1', '0', '2', '22', '3', '2020-08-26 10:06:02', null, '141', null, null);
INSERT INTO `cell` VALUES ('480', null, '1', '0', '2', '21', '3', '2020-08-26 10:06:02', null, '140', null, null);
INSERT INTO `cell` VALUES ('481', null, '1', '0', '2', '20', '3', '2020-08-26 10:06:02', null, '139', null, null);
INSERT INTO `cell` VALUES ('482', null, '1', '0', '2', '19', '3', '2020-08-26 10:06:02', null, '138', null, null);
INSERT INTO `cell` VALUES ('483', null, '1', '0', '2', '18', '3', '2020-08-26 10:06:02', null, '137', null, null);
INSERT INTO `cell` VALUES ('484', null, '1', '0', '2', '17', '3', '2020-08-26 10:06:02', null, '136', null, null);
INSERT INTO `cell` VALUES ('485', null, '1', '0', '2', '16', '3', '2020-08-26 10:06:02', null, '135', null, null);
INSERT INTO `cell` VALUES ('486', null, '1', '0', '2', '15', '3', '2020-08-26 10:06:02', null, '134', null, null);
INSERT INTO `cell` VALUES ('487', null, '1', '0', '2', '14', '3', '2020-08-26 10:06:02', null, '133', null, null);
INSERT INTO `cell` VALUES ('488', null, '1', '0', '2', '13', '3', '2020-08-26 10:06:02', null, '132', null, null);
INSERT INTO `cell` VALUES ('489', null, '1', '0', '2', '12', '3', '2020-08-26 10:06:02', null, '131', null, null);
INSERT INTO `cell` VALUES ('490', null, '1', '0', '2', '11', '3', '2020-08-26 10:06:02', null, '130', null, null);
INSERT INTO `cell` VALUES ('491', null, '1', '0', '2', '10', '3', '2020-08-26 10:06:02', null, '129', null, null);
INSERT INTO `cell` VALUES ('492', null, '1', '0', '2', '9', '3', '2020-08-26 10:06:02', null, '128', null, null);
INSERT INTO `cell` VALUES ('493', null, '1', '0', '2', '8', '3', '2020-08-26 10:06:02', null, '127', null, null);
INSERT INTO `cell` VALUES ('494', null, '1', '0', '2', '7', '3', '2020-08-26 10:06:02', null, '126', null, null);
INSERT INTO `cell` VALUES ('495', null, '1', '0', '2', '6', '3', '2020-08-26 10:06:02', null, '125', null, null);
INSERT INTO `cell` VALUES ('496', null, '1', '0', '2', '5', '3', '2020-08-26 10:06:02', null, '124', null, null);
INSERT INTO `cell` VALUES ('497', null, '1', '0', '2', '0', '0', '2020-08-26 10:06:02', null, '', null, null);
INSERT INTO `cell` VALUES ('498', null, '1', '0', '2', '0', '0', '2020-08-26 10:06:02', null, '', null, null);
INSERT INTO `cell` VALUES ('499', null, '1', '0', '2', '2', '3', '2020-08-26 10:06:02', null, '123', null, null);
INSERT INTO `cell` VALUES ('500', null, '1', '0', '2', '1', '3', '2020-08-26 10:06:02', null, '122', null, null);
INSERT INTO `cell` VALUES ('501', null, '1', '0', '2', '36', '4', '2020-08-26 10:06:02', null, '156', null, null);
INSERT INTO `cell` VALUES ('502', null, '1', '0', '2', '35', '4', '2020-08-26 10:06:02', null, '157', null, null);
INSERT INTO `cell` VALUES ('503', null, '1', '0', '2', '34', '4', '2020-08-26 10:06:02', null, '158', null, null);
INSERT INTO `cell` VALUES ('504', null, '1', '0', '2', '33', '4', '2020-08-26 10:06:02', null, '159', null, null);
INSERT INTO `cell` VALUES ('505', null, '1', '0', '2', '32', '4', '2020-08-26 10:06:02', null, '160', null, null);
INSERT INTO `cell` VALUES ('506', null, '1', '0', '2', '31', '4', '2020-08-26 10:06:02', null, '161', null, null);
INSERT INTO `cell` VALUES ('507', null, '1', '0', '2', '30', '4', '2020-08-26 10:06:02', null, '162', null, null);
INSERT INTO `cell` VALUES ('508', null, '1', '0', '2', '29', '4', '2020-08-26 10:06:02', null, '163', null, null);
INSERT INTO `cell` VALUES ('509', null, '1', '0', '2', '28', '4', '2020-08-26 10:06:02', null, '164', null, null);
INSERT INTO `cell` VALUES ('510', null, '1', '0', '2', '27', '4', '2020-08-26 10:06:02', null, '165', null, null);
INSERT INTO `cell` VALUES ('511', null, '1', '0', '2', '26', '4', '2020-08-26 10:06:02', null, '166', null, null);
INSERT INTO `cell` VALUES ('512', null, '1', '0', '2', '25', '4', '2020-08-26 10:06:02', null, '167', null, null);
INSERT INTO `cell` VALUES ('513', null, '1', '0', '2', '24', '4', '2020-08-26 10:06:02', null, '168', null, null);
INSERT INTO `cell` VALUES ('514', null, '1', '0', '2', '23', '4', '2020-08-26 10:06:02', null, '169', null, null);
INSERT INTO `cell` VALUES ('515', null, '1', '0', '2', '22', '4', '2020-08-26 10:06:02', null, '170', null, null);
INSERT INTO `cell` VALUES ('516', null, '1', '0', '2', '21', '4', '2020-08-26 10:06:02', null, '171', null, null);
INSERT INTO `cell` VALUES ('517', null, '1', '0', '2', '20', '4', '2020-08-26 10:06:02', null, '172', null, null);
INSERT INTO `cell` VALUES ('518', null, '1', '0', '2', '19', '4', '2020-08-26 10:06:02', null, '173', null, null);
INSERT INTO `cell` VALUES ('519', null, '1', '0', '2', '18', '4', '2020-08-26 10:06:02', null, '174', null, null);
INSERT INTO `cell` VALUES ('520', null, '1', '0', '2', '17', '4', '2020-08-26 10:06:02', null, '175', null, null);
INSERT INTO `cell` VALUES ('521', null, '1', '0', '2', '16', '4', '2020-08-26 10:06:02', null, '176', null, null);
INSERT INTO `cell` VALUES ('522', null, '1', '0', '2', '15', '4', '2020-08-26 10:06:02', null, '177', null, null);
INSERT INTO `cell` VALUES ('523', null, '1', '0', '2', '14', '4', '2020-08-26 10:06:02', null, '178', null, null);
INSERT INTO `cell` VALUES ('524', null, '1', '0', '2', '13', '4', '2020-08-26 10:06:02', null, '179', null, null);
INSERT INTO `cell` VALUES ('525', null, '1', '0', '2', '12', '4', '2020-08-26 10:06:02', null, '180', null, null);
INSERT INTO `cell` VALUES ('526', null, '1', '0', '2', '11', '4', '2020-08-26 10:06:02', null, '181', null, null);
INSERT INTO `cell` VALUES ('527', null, '1', '0', '2', '10', '4', '2020-08-26 10:06:02', null, '182', null, null);
INSERT INTO `cell` VALUES ('528', null, '1', '0', '2', '9', '4', '2020-08-26 10:06:02', null, '183', null, null);
INSERT INTO `cell` VALUES ('529', null, '1', '0', '2', '8', '4', '2020-08-26 10:06:02', null, '184', null, null);
INSERT INTO `cell` VALUES ('530', null, '1', '0', '2', '7', '4', '2020-08-26 10:06:02', null, '185', null, null);
INSERT INTO `cell` VALUES ('531', null, '1', '0', '2', '6', '4', '2020-08-26 10:06:02', null, '186', null, null);
INSERT INTO `cell` VALUES ('532', null, '1', '0', '2', '5', '4', '2020-08-26 10:06:02', null, '187', null, null);
INSERT INTO `cell` VALUES ('533', null, '1', '0', '2', '4', '4', '2020-08-26 10:06:02', null, '188', null, null);
INSERT INTO `cell` VALUES ('534', null, '1', '0', '2', '3', '4', '2020-08-26 10:06:02', null, '189', null, null);
INSERT INTO `cell` VALUES ('535', null, '1', '0', '2', '2', '4', '2020-08-26 10:06:02', null, '190', null, null);
INSERT INTO `cell` VALUES ('536', null, '1', '0', '2', '1', '4', '2020-08-26 10:06:02', null, '191', null, null);
INSERT INTO `cell` VALUES ('537', null, '1', '0', '2', '36', '5', '2020-08-26 10:06:02', null, '227', null, null);
INSERT INTO `cell` VALUES ('538', null, '1', '0', '2', '35', '5', '2020-08-26 10:06:02', null, '226', null, null);
INSERT INTO `cell` VALUES ('539', null, '1', '0', '2', '34', '5', '2020-08-26 10:06:02', null, '225', null, null);
INSERT INTO `cell` VALUES ('540', null, '1', '0', '2', '33', '5', '2020-08-26 10:06:02', null, '224', null, null);
INSERT INTO `cell` VALUES ('541', null, '1', '0', '2', '32', '5', '2020-08-26 10:06:02', null, '223', null, null);
INSERT INTO `cell` VALUES ('542', null, '1', '0', '2', '31', '5', '2020-08-26 10:06:02', null, '222', null, null);
INSERT INTO `cell` VALUES ('543', null, '1', '0', '2', '30', '5', '2020-08-26 10:06:02', null, '221', null, null);
INSERT INTO `cell` VALUES ('544', null, '1', '0', '2', '29', '5', '2020-08-26 10:06:02', null, '220', null, null);
INSERT INTO `cell` VALUES ('545', null, '1', '0', '2', '28', '5', '2020-08-26 10:06:02', null, '219', null, null);
INSERT INTO `cell` VALUES ('546', null, '1', '0', '2', '27', '5', '2020-08-26 10:06:02', null, '218', null, null);
INSERT INTO `cell` VALUES ('547', null, '1', '0', '2', '26', '5', '2020-08-26 10:06:02', null, '217', null, null);
INSERT INTO `cell` VALUES ('548', null, '1', '0', '2', '25', '5', '2020-08-26 10:06:02', null, '216', null, null);
INSERT INTO `cell` VALUES ('549', null, '1', '0', '2', '24', '5', '2020-08-26 10:06:02', null, '215', null, null);
INSERT INTO `cell` VALUES ('550', null, '1', '0', '2', '23', '5', '2020-08-26 10:06:02', null, '214', null, null);
INSERT INTO `cell` VALUES ('551', null, '1', '0', '2', '22', '5', '2020-08-26 10:06:02', null, '213', null, null);
INSERT INTO `cell` VALUES ('552', null, '1', '0', '2', '21', '5', '2020-08-26 10:06:02', null, '212', null, null);
INSERT INTO `cell` VALUES ('553', null, '1', '0', '2', '20', '5', '2020-08-26 10:06:02', null, '211', null, null);
INSERT INTO `cell` VALUES ('554', null, '1', '0', '2', '19', '5', '2020-08-26 10:06:02', null, '210', null, null);
INSERT INTO `cell` VALUES ('555', null, '1', '0', '2', '18', '5', '2020-08-26 10:06:02', null, '209', null, null);
INSERT INTO `cell` VALUES ('556', null, '1', '0', '2', '17', '5', '2020-08-26 10:06:02', null, '208', null, null);
INSERT INTO `cell` VALUES ('557', null, '1', '0', '2', '16', '5', '2020-08-26 10:06:02', null, '207', null, null);
INSERT INTO `cell` VALUES ('558', null, '1', '0', '2', '15', '5', '2020-08-26 10:06:02', null, '206', null, null);
INSERT INTO `cell` VALUES ('559', null, '1', '0', '2', '14', '5', '2020-08-26 10:06:02', null, '205', null, null);
INSERT INTO `cell` VALUES ('560', null, '1', '0', '2', '13', '5', '2020-08-26 10:06:02', null, '204', null, null);
INSERT INTO `cell` VALUES ('561', null, '1', '0', '2', '12', '5', '2020-08-26 10:06:02', null, '203', null, null);
INSERT INTO `cell` VALUES ('562', null, '1', '0', '2', '11', '5', '2020-08-26 10:06:02', null, '202', null, null);
INSERT INTO `cell` VALUES ('563', null, '1', '0', '2', '10', '5', '2020-08-26 10:06:02', null, '201', null, null);
INSERT INTO `cell` VALUES ('564', null, '1', '0', '2', '9', '5', '2020-08-26 10:06:02', null, '200', null, null);
INSERT INTO `cell` VALUES ('565', null, '1', '0', '2', '8', '5', '2020-08-26 10:06:02', null, '199', null, null);
INSERT INTO `cell` VALUES ('566', null, '1', '0', '2', '7', '5', '2020-08-26 10:06:02', null, '198', null, null);
INSERT INTO `cell` VALUES ('567', null, '1', '0', '2', '6', '5', '2020-08-26 10:06:02', null, '197', null, null);
INSERT INTO `cell` VALUES ('568', null, '1', '0', '2', '5', '5', '2020-08-26 10:06:02', null, '196', null, null);
INSERT INTO `cell` VALUES ('569', null, '1', '0', '2', '4', '5', '2020-08-26 10:06:03', null, '195', null, null);
INSERT INTO `cell` VALUES ('570', null, '1', '0', '2', '3', '5', '2020-08-26 10:06:03', null, '194', null, null);
INSERT INTO `cell` VALUES ('571', null, '1', '0', '2', '2', '5', '2020-08-26 10:06:03', null, '193', null, null);
INSERT INTO `cell` VALUES ('572', null, '1', '0', '2', '1', '5', '2020-08-26 10:06:03', null, '192', null, null);
INSERT INTO `cell` VALUES ('573', null, '1', '0', '2', '36', '6', '2020-08-26 10:06:03', null, '228', null, null);
INSERT INTO `cell` VALUES ('574', null, '1', '0', '2', '35', '6', '2020-08-26 10:06:03', null, '229', null, null);
INSERT INTO `cell` VALUES ('575', null, '1', '0', '2', '34', '6', '2020-08-26 10:06:03', null, '230', null, null);
INSERT INTO `cell` VALUES ('576', null, '1', '0', '2', '33', '6', '2020-08-26 10:06:03', null, '231', null, null);
INSERT INTO `cell` VALUES ('577', null, '1', '0', '2', '32', '6', '2020-08-26 10:06:03', null, '232', null, null);
INSERT INTO `cell` VALUES ('578', null, '1', '0', '2', '31', '6', '2020-08-26 10:06:03', null, '233', null, null);
INSERT INTO `cell` VALUES ('579', null, '1', '0', '2', '30', '6', '2020-08-26 10:06:03', null, '234', null, null);
INSERT INTO `cell` VALUES ('580', null, '1', '0', '2', '29', '6', '2020-08-26 10:06:03', null, '235', null, null);
INSERT INTO `cell` VALUES ('581', null, '1', '0', '2', '28', '6', '2020-08-26 10:06:03', null, '236', null, null);
INSERT INTO `cell` VALUES ('582', null, '1', '0', '2', '27', '6', '2020-08-26 10:06:01', null, '237', null, null);
INSERT INTO `cell` VALUES ('583', null, '1', '0', '2', '26', '6', '2020-08-26 10:06:01', null, '238', null, null);
INSERT INTO `cell` VALUES ('584', null, '1', '0', '2', '25', '6', '2020-08-26 10:06:01', null, '239', null, null);
INSERT INTO `cell` VALUES ('585', null, '1', '0', '2', '24', '6', '2020-08-26 10:06:01', null, '240', null, null);
INSERT INTO `cell` VALUES ('586', null, '1', '0', '2', '23', '6', '2020-08-26 10:06:01', null, '241', null, null);
INSERT INTO `cell` VALUES ('587', null, '1', '0', '2', '22', '6', '2020-08-26 10:06:01', null, '242', null, null);
INSERT INTO `cell` VALUES ('588', null, '1', '0', '2', '21', '6', '2020-08-26 10:06:01', null, '243', null, null);
INSERT INTO `cell` VALUES ('589', null, '1', '0', '2', '20', '6', '2020-08-26 10:06:01', null, '244', null, null);
INSERT INTO `cell` VALUES ('590', null, '1', '0', '2', '19', '6', '2020-08-26 10:06:01', null, '245', null, null);
INSERT INTO `cell` VALUES ('591', null, '1', '0', '2', '18', '6', '2020-08-26 10:06:01', null, '246', null, null);
INSERT INTO `cell` VALUES ('592', null, '1', '0', '2', '17', '6', '2020-08-26 10:06:01', null, '247', null, null);
INSERT INTO `cell` VALUES ('593', null, '1', '0', '2', '16', '6', '2020-08-26 10:06:01', null, '248', null, null);
INSERT INTO `cell` VALUES ('594', null, '1', '0', '2', '15', '6', '2020-08-26 10:06:01', null, '249', null, null);
INSERT INTO `cell` VALUES ('595', null, '1', '0', '2', '14', '6', '2020-08-26 10:06:01', null, '250', null, null);
INSERT INTO `cell` VALUES ('596', null, '1', '0', '2', '13', '6', '2020-08-26 10:06:01', null, '251', null, null);
INSERT INTO `cell` VALUES ('597', null, '1', '0', '2', '12', '6', '2020-08-26 10:06:01', null, '252', null, null);
INSERT INTO `cell` VALUES ('598', null, '1', '0', '2', '11', '6', '2020-08-26 10:06:02', null, '253', null, null);
INSERT INTO `cell` VALUES ('599', null, '1', '0', '2', '10', '6', '2020-08-26 10:06:02', null, '254', null, null);
INSERT INTO `cell` VALUES ('600', null, '1', '0', '2', '9', '6', '2020-08-26 10:06:02', null, '255', null, null);
INSERT INTO `cell` VALUES ('601', null, '1', '0', '2', '8', '6', '2020-08-26 10:06:02', null, '256', null, null);
INSERT INTO `cell` VALUES ('602', null, '1', '0', '2', '7', '6', '2020-08-26 10:06:02', null, '257', null, null);
INSERT INTO `cell` VALUES ('603', null, '1', '0', '2', '6', '6', '2020-08-26 10:06:02', null, '258', null, null);
INSERT INTO `cell` VALUES ('604', null, '1', '0', '2', '5', '6', '2020-08-26 10:06:02', null, '259', null, null);
INSERT INTO `cell` VALUES ('605', null, '1', '0', '2', '4', '6', '2020-08-26 10:06:02', null, '260', null, null);
INSERT INTO `cell` VALUES ('606', null, '1', '0', '2', '3', '6', '2020-08-26 10:06:02', null, '261', null, null);
INSERT INTO `cell` VALUES ('607', null, '1', '0', '2', '2', '6', '2020-08-26 10:06:02', null, '262', null, null);
INSERT INTO `cell` VALUES ('608', null, '1', '0', '2', '1', '6', '2020-08-26 10:06:02', null, '263', null, null);
INSERT INTO `cell` VALUES ('609', null, '1', '0', '2', '36', '7', '2020-08-26 10:06:02', null, '299', null, null);
INSERT INTO `cell` VALUES ('610', null, '1', '0', '2', '35', '7', '2020-08-26 10:06:02', null, '298', null, null);
INSERT INTO `cell` VALUES ('611', null, '1', '0', '2', '34', '7', '2020-08-26 10:06:02', null, '297', null, null);
INSERT INTO `cell` VALUES ('612', null, '1', '0', '2', '33', '7', '2020-08-26 10:06:02', null, '296', null, null);
INSERT INTO `cell` VALUES ('613', null, '1', '0', '2', '32', '7', '2020-08-26 10:06:02', null, '295', null, null);
INSERT INTO `cell` VALUES ('614', null, '1', '0', '2', '31', '7', '2020-08-26 10:06:02', null, '294', null, null);
INSERT INTO `cell` VALUES ('615', null, '1', '0', '2', '30', '7', '2020-08-26 10:06:02', null, '293', null, null);
INSERT INTO `cell` VALUES ('616', null, '1', '0', '2', '29', '7', '2020-08-26 10:06:02', null, '292', null, null);
INSERT INTO `cell` VALUES ('617', null, '1', '0', '2', '28', '7', '2020-08-26 10:06:02', null, '291', null, null);
INSERT INTO `cell` VALUES ('618', null, '1', '0', '2', '27', '7', '2020-08-26 10:06:02', null, '290', null, null);
INSERT INTO `cell` VALUES ('619', null, '1', '0', '2', '26', '7', '2020-08-26 10:06:02', null, '289', null, null);
INSERT INTO `cell` VALUES ('620', null, '1', '0', '2', '25', '7', '2020-08-26 10:06:02', null, '288', null, null);
INSERT INTO `cell` VALUES ('621', null, '1', '0', '2', '24', '7', '2020-08-26 10:06:02', null, '287', null, null);
INSERT INTO `cell` VALUES ('622', null, '1', '0', '2', '23', '7', '2020-08-26 10:06:02', null, '286', null, null);
INSERT INTO `cell` VALUES ('623', null, '1', '0', '2', '22', '7', '2020-08-26 10:06:02', null, '285', null, null);
INSERT INTO `cell` VALUES ('624', null, '1', '0', '2', '21', '7', '2020-08-26 10:06:02', null, '284', null, null);
INSERT INTO `cell` VALUES ('625', null, '1', '0', '2', '20', '7', '2020-08-26 10:06:02', null, '283', null, null);
INSERT INTO `cell` VALUES ('626', null, '1', '0', '2', '19', '7', '2020-08-26 10:06:02', null, '282', null, null);
INSERT INTO `cell` VALUES ('627', null, '1', '0', '2', '18', '7', '2020-08-26 10:06:02', null, '281', null, null);
INSERT INTO `cell` VALUES ('628', null, '1', '0', '2', '17', '7', '2020-08-26 10:06:02', null, '280', null, null);
INSERT INTO `cell` VALUES ('629', null, '1', '0', '2', '16', '7', '2020-08-26 10:06:02', null, '279', null, null);
INSERT INTO `cell` VALUES ('630', null, '1', '0', '2', '15', '7', '2020-08-26 10:06:02', null, '278', null, null);
INSERT INTO `cell` VALUES ('631', null, '1', '0', '2', '14', '7', '2020-08-26 10:06:02', null, '277', null, null);
INSERT INTO `cell` VALUES ('632', null, '1', '0', '2', '13', '7', '2020-08-26 10:06:02', null, '276', null, null);
INSERT INTO `cell` VALUES ('633', null, '1', '0', '2', '12', '7', '2020-08-26 10:06:02', null, '275', null, null);
INSERT INTO `cell` VALUES ('634', null, '1', '0', '2', '11', '7', '2020-08-26 10:06:02', null, '274', null, null);
INSERT INTO `cell` VALUES ('635', null, '1', '0', '2', '10', '7', '2020-08-26 10:06:02', null, '273', null, null);
INSERT INTO `cell` VALUES ('636', null, '1', '0', '2', '9', '7', '2020-08-26 10:06:02', null, '272', null, null);
INSERT INTO `cell` VALUES ('637', null, '1', '0', '2', '8', '7', '2020-08-26 10:06:02', null, '271', null, null);
INSERT INTO `cell` VALUES ('638', null, '1', '0', '2', '7', '7', '2020-08-26 10:06:02', null, '270', null, null);
INSERT INTO `cell` VALUES ('639', null, '1', '0', '2', '6', '7', '2020-08-26 10:06:02', null, '269', null, null);
INSERT INTO `cell` VALUES ('640', null, '1', '0', '2', '5', '7', '2020-08-26 10:06:02', null, '268', null, null);
INSERT INTO `cell` VALUES ('641', null, '1', '0', '2', '4', '7', '2020-08-26 10:06:02', null, '267', null, null);
INSERT INTO `cell` VALUES ('642', null, '1', '0', '2', '3', '7', '2020-08-26 10:06:02', null, '266', null, null);
INSERT INTO `cell` VALUES ('643', null, '1', '0', '2', '2', '7', '2020-08-26 10:06:02', null, '265', null, null);
INSERT INTO `cell` VALUES ('644', null, '1', '0', '2', '1', '7', '2020-08-26 10:06:02', null, '264', null, null);
INSERT INTO `cell` VALUES ('673', null, '3', '0', '1', '21', '1', '2020-11-11 18:27:16', null, '477', null, null);
INSERT INTO `cell` VALUES ('674', null, '3', '0', '1', '20', '1', '2020-11-11 18:27:16', null, '478', null, null);
INSERT INTO `cell` VALUES ('675', null, '3', '0', '1', '19', '1', '2020-11-11 18:27:16', null, '479', null, null);
INSERT INTO `cell` VALUES ('676', null, '3', '0', '1', '18', '1', '2020-11-11 18:27:16', null, '480', null, null);
INSERT INTO `cell` VALUES ('677', null, '3', '0', '1', '17', '1', '2020-11-11 18:27:16', null, '481', null, null);
INSERT INTO `cell` VALUES ('678', null, '3', '0', '1', '16', '1', '2020-11-11 18:27:16', null, '482', null, null);
INSERT INTO `cell` VALUES ('679', null, '3', '0', '1', '15', '1', '2020-11-11 18:27:16', null, '483', null, null);
INSERT INTO `cell` VALUES ('680', null, '3', '0', '1', '14', '1', '2020-11-11 18:27:16', null, '484', null, null);
INSERT INTO `cell` VALUES ('681', null, '3', '0', '1', '13', '1', '2020-11-11 18:27:16', null, '485', null, null);
INSERT INTO `cell` VALUES ('682', null, '3', '0', '1', '12', '1', '2020-11-11 18:27:17', null, '486', null, null);
INSERT INTO `cell` VALUES ('683', null, '3', '0', '1', '11', '1', '2020-11-11 18:27:17', null, '487', null, null);
INSERT INTO `cell` VALUES ('684', null, '3', '0', '1', '10', '1', '2020-11-11 18:27:17', null, '488', null, null);
INSERT INTO `cell` VALUES ('685', null, '3', '0', '1', '9', '1', '2020-11-11 18:27:17', null, '489', null, null);
INSERT INTO `cell` VALUES ('686', null, '3', '0', '1', '8', '1', '2020-11-11 18:27:17', null, '490', null, null);
INSERT INTO `cell` VALUES ('687', null, '3', '0', '1', '7', '1', '2020-11-11 18:27:17', null, '491', null, null);
INSERT INTO `cell` VALUES ('688', null, '3', '0', '1', '6', '1', '2020-11-11 18:27:17', null, '492', null, null);
INSERT INTO `cell` VALUES ('689', null, '3', '0', '1', '5', '1', '2020-11-11 18:27:17', null, '493', null, null);
INSERT INTO `cell` VALUES ('690', null, '3', '0', '1', '4', '1', '2020-11-11 18:27:17', null, '494', null, null);
INSERT INTO `cell` VALUES ('691', null, '3', '0', '1', '3', '1', '2020-11-11 18:27:17', null, '495', null, null);
INSERT INTO `cell` VALUES ('692', null, '3', '0', '1', '2', '1', '2020-11-11 18:27:17', null, '496', null, null);
INSERT INTO `cell` VALUES ('693', null, '3', '0', '1', '21', '2', '2020-11-11 18:27:17', null, '516', null, null);
INSERT INTO `cell` VALUES ('694', null, '3', '0', '1', '20', '2', '2020-11-11 18:27:17', null, '515', null, null);
INSERT INTO `cell` VALUES ('695', null, '3', '0', '1', '19', '2', '2020-11-11 18:27:17', null, '514', null, null);
INSERT INTO `cell` VALUES ('696', null, '3', '0', '1', '18', '2', '2020-11-11 18:27:17', null, '513', null, null);
INSERT INTO `cell` VALUES ('697', null, '3', '0', '1', '17', '2', '2020-11-11 18:27:17', null, '512', null, null);
INSERT INTO `cell` VALUES ('698', null, '3', '0', '1', '16', '2', '2020-11-11 18:27:17', null, '511', null, null);
INSERT INTO `cell` VALUES ('699', null, '3', '0', '1', '15', '2', '2020-11-11 18:27:17', null, '510', null, null);
INSERT INTO `cell` VALUES ('700', null, '3', '0', '1', '14', '2', '2020-11-11 18:27:17', null, '509', null, null);
INSERT INTO `cell` VALUES ('701', null, '3', '0', '1', '13', '2', '2020-11-11 18:27:17', null, '508', null, null);
INSERT INTO `cell` VALUES ('702', null, '3', '0', '1', '12', '2', '2020-11-11 18:27:17', null, '507', null, null);
INSERT INTO `cell` VALUES ('703', null, '3', '0', '1', '11', '2', '2020-11-11 18:27:16', null, '506', null, null);
INSERT INTO `cell` VALUES ('704', null, '3', '0', '1', '10', '2', '2020-11-11 18:27:16', null, '505', null, null);
INSERT INTO `cell` VALUES ('705', null, '3', '0', '1', '9', '2', '2020-11-11 18:27:16', null, '504', null, null);
INSERT INTO `cell` VALUES ('706', null, '3', '0', '1', '8', '2', '2020-11-11 18:27:16', null, '503', null, null);
INSERT INTO `cell` VALUES ('707', null, '3', '0', '1', '7', '2', '2020-11-11 18:27:16', null, '502', null, null);
INSERT INTO `cell` VALUES ('708', null, '3', '0', '1', '6', '2', '2020-11-11 18:27:16', null, '501', null, null);
INSERT INTO `cell` VALUES ('709', null, '3', '0', '1', '5', '2', '2020-11-11 18:27:16', null, '500', null, null);
INSERT INTO `cell` VALUES ('710', null, '3', '0', '1', '4', '2', '2020-11-11 18:27:16', null, '499', null, null);
INSERT INTO `cell` VALUES ('711', null, '3', '0', '1', '3', '2', '2020-11-11 18:27:16', null, '498', null, null);
INSERT INTO `cell` VALUES ('712', null, '3', '0', '1', '2', '2', '2020-11-11 18:27:16', null, '497', null, null);
INSERT INTO `cell` VALUES ('713', null, '3', '0', '1', '21', '3', '2020-11-11 18:27:17', null, '517', null, null);
INSERT INTO `cell` VALUES ('714', null, '3', '0', '1', '20', '3', '2020-11-11 18:27:17', null, '518', null, null);
INSERT INTO `cell` VALUES ('715', null, '3', '0', '1', '19', '3', '2020-11-11 18:27:17', null, '519', null, null);
INSERT INTO `cell` VALUES ('716', null, '3', '0', '1', '18', '3', '2020-11-11 18:27:17', null, '520', null, null);
INSERT INTO `cell` VALUES ('717', null, '3', '0', '1', '17', '3', '2020-11-11 18:27:17', null, '521', null, null);
INSERT INTO `cell` VALUES ('718', null, '3', '0', '1', '16', '3', '2020-11-11 18:27:17', null, '522', null, null);
INSERT INTO `cell` VALUES ('719', null, '3', '0', '1', '15', '3', '2020-11-11 18:27:17', null, '523', null, null);
INSERT INTO `cell` VALUES ('720', null, '3', '0', '1', '14', '3', '2020-11-11 18:27:17', null, '524', null, null);
INSERT INTO `cell` VALUES ('721', null, '3', '0', '1', '13', '3', '2020-11-11 18:27:17', null, '525', null, null);
INSERT INTO `cell` VALUES ('722', null, '3', '0', '1', '12', '3', '2020-11-11 18:27:17', null, '526', null, null);
INSERT INTO `cell` VALUES ('723', null, '3', '0', '1', '11', '3', '2020-11-11 18:27:17', null, '527', null, null);
INSERT INTO `cell` VALUES ('724', null, '3', '0', '1', '10', '3', '2020-11-11 18:27:17', null, '528', null, null);
INSERT INTO `cell` VALUES ('725', null, '3', '0', '1', '9', '3', '2020-11-11 18:27:17', null, '529', null, null);
INSERT INTO `cell` VALUES ('726', null, '3', '0', '1', '8', '3', '2020-11-11 18:27:17', null, '530', null, null);
INSERT INTO `cell` VALUES ('727', null, '3', '0', '1', '7', '3', '2020-11-11 18:27:17', null, '531', null, null);
INSERT INTO `cell` VALUES ('728', null, '3', '0', '1', '6', '3', '2020-11-11 18:27:17', null, '532', null, null);
INSERT INTO `cell` VALUES ('729', null, '3', '0', '1', '5', '3', '2020-11-11 18:27:17', null, '533', null, null);
INSERT INTO `cell` VALUES ('730', null, '3', '0', '1', '4', '3', '2020-11-11 18:27:17', null, '534', null, null);
INSERT INTO `cell` VALUES ('731', null, '3', '0', '1', '3', '3', '2020-11-11 18:27:17', null, '535', null, null);
INSERT INTO `cell` VALUES ('732', null, '3', '0', '1', '2', '3', '2020-11-11 18:27:17', null, '536', null, null);
INSERT INTO `cell` VALUES ('733', null, '3', '0', '1', '21', '4', '2020-11-11 18:27:16', null, '556', null, null);
INSERT INTO `cell` VALUES ('734', null, '3', '0', '1', '20', '4', '2020-11-11 18:27:16', null, '555', null, null);
INSERT INTO `cell` VALUES ('735', null, '3', '0', '1', '19', '4', '2020-11-11 18:27:16', null, '554', null, null);
INSERT INTO `cell` VALUES ('736', null, '3', '0', '1', '18', '4', '2020-11-11 18:27:16', null, '553', null, null);
INSERT INTO `cell` VALUES ('737', null, '3', '0', '1', '17', '4', '2020-11-11 18:27:16', null, '552', null, null);
INSERT INTO `cell` VALUES ('738', null, '3', '0', '1', '16', '4', '2020-11-11 18:27:16', null, '551', null, null);
INSERT INTO `cell` VALUES ('739', null, '3', '0', '1', '15', '4', '2020-11-11 18:27:16', null, '550', null, null);
INSERT INTO `cell` VALUES ('740', null, '3', '0', '1', '14', '4', '2020-11-11 18:27:16', null, '549', null, null);
INSERT INTO `cell` VALUES ('741', null, '3', '0', '1', '13', '4', '2020-11-11 18:27:16', null, '548', null, null);
INSERT INTO `cell` VALUES ('742', null, '3', '0', '1', '12', '4', '2020-11-11 18:27:17', null, '547', null, null);
INSERT INTO `cell` VALUES ('743', null, '3', '0', '1', '11', '4', '2020-11-11 18:27:17', null, '546', null, null);
INSERT INTO `cell` VALUES ('744', null, '3', '0', '1', '10', '4', '2020-11-11 18:27:17', null, '545', null, null);
INSERT INTO `cell` VALUES ('745', null, '3', '0', '1', '9', '4', '2020-11-11 18:27:17', null, '544', null, null);
INSERT INTO `cell` VALUES ('746', null, '3', '0', '1', '8', '4', '2020-11-11 18:27:17', null, '543', null, null);
INSERT INTO `cell` VALUES ('747', null, '3', '0', '1', '7', '4', '2020-11-11 18:27:17', null, '542', null, null);
INSERT INTO `cell` VALUES ('748', null, '3', '0', '1', '6', '4', '2020-11-11 18:27:17', null, '541', null, null);
INSERT INTO `cell` VALUES ('749', null, '3', '0', '1', '5', '4', '2020-11-11 18:27:17', null, '540', null, null);
INSERT INTO `cell` VALUES ('750', null, '3', '0', '1', '4', '4', '2020-11-11 18:27:17', null, '539', null, null);
INSERT INTO `cell` VALUES ('751', null, '3', '0', '1', '3', '4', '2020-11-11 18:27:17', null, '538', null, null);
INSERT INTO `cell` VALUES ('752', null, '3', '0', '1', '2', '4', '2020-11-11 18:27:17', null, '537', null, null);
INSERT INTO `cell` VALUES ('753', null, '3', '0', '1', '21', '5', '2020-11-11 18:27:17', null, '557', null, null);
INSERT INTO `cell` VALUES ('754', null, '3', '0', '1', '20', '5', '2020-11-11 18:27:17', null, '558', null, null);
INSERT INTO `cell` VALUES ('755', null, '3', '0', '1', '19', '5', '2020-11-11 18:27:17', null, '559', null, null);
INSERT INTO `cell` VALUES ('756', null, '3', '0', '1', '18', '5', '2020-11-11 18:27:17', null, '560', null, null);
INSERT INTO `cell` VALUES ('757', null, '3', '0', '1', '17', '5', '2020-11-11 18:27:17', null, '561', null, null);
INSERT INTO `cell` VALUES ('758', null, '3', '0', '1', '16', '5', '2020-11-11 18:27:17', null, '562', null, null);
INSERT INTO `cell` VALUES ('759', null, '3', '0', '1', '15', '5', '2020-11-11 18:27:17', null, '563', null, null);
INSERT INTO `cell` VALUES ('760', null, '3', '0', '1', '14', '5', '2020-11-11 18:27:17', null, '564', null, null);
INSERT INTO `cell` VALUES ('761', null, '3', '0', '1', '13', '5', '2020-11-11 18:27:17', null, '565', null, null);
INSERT INTO `cell` VALUES ('762', null, '3', '0', '1', '12', '5', '2020-11-11 18:27:17', null, '566', null, null);
INSERT INTO `cell` VALUES ('763', null, '3', '0', '1', '11', '5', '2020-11-11 18:27:16', null, '567', null, null);
INSERT INTO `cell` VALUES ('764', null, '3', '0', '1', '10', '5', '2020-11-11 18:27:16', null, '568', null, null);
INSERT INTO `cell` VALUES ('765', null, '3', '0', '1', '9', '5', '2020-11-11 18:27:16', null, '569', null, null);
INSERT INTO `cell` VALUES ('766', null, '3', '0', '1', '8', '5', '2020-11-11 18:27:16', null, '570', null, null);
INSERT INTO `cell` VALUES ('767', null, '3', '0', '1', '7', '5', '2020-11-11 18:27:16', null, '571', null, null);
INSERT INTO `cell` VALUES ('768', null, '3', '0', '1', '6', '5', '2020-11-11 18:27:16', null, '572', null, null);
INSERT INTO `cell` VALUES ('769', null, '3', '0', '1', '5', '5', '2020-11-11 18:27:16', null, '573', null, null);
INSERT INTO `cell` VALUES ('770', null, '3', '0', '1', '4', '5', '2020-11-11 18:27:16', null, '574', null, null);
INSERT INTO `cell` VALUES ('771', null, '3', '0', '1', '3', '5', '2020-11-11 18:27:17', null, '575', null, null);
INSERT INTO `cell` VALUES ('772', null, '3', '0', '1', '2', '5', '2020-11-11 18:27:17', null, '576', null, null);
INSERT INTO `cell` VALUES ('773', null, '3', '0', '1', '21', '6', '2020-11-11 18:27:17', null, '596', null, null);
INSERT INTO `cell` VALUES ('774', null, '3', '0', '1', '20', '6', '2020-11-11 18:27:17', null, '595', null, null);
INSERT INTO `cell` VALUES ('775', null, '3', '0', '1', '19', '6', '2020-11-11 18:27:17', null, '594', null, null);
INSERT INTO `cell` VALUES ('776', null, '3', '0', '1', '18', '6', '2020-11-11 18:27:17', null, '593', null, null);
INSERT INTO `cell` VALUES ('777', null, '3', '0', '1', '17', '6', '2020-11-11 18:27:17', null, '592', null, null);
INSERT INTO `cell` VALUES ('778', null, '3', '0', '1', '16', '6', '2020-11-11 18:27:17', null, '591', null, null);
INSERT INTO `cell` VALUES ('779', null, '3', '0', '1', '15', '6', '2020-11-11 18:27:17', null, '590', null, null);
INSERT INTO `cell` VALUES ('780', null, '3', '0', '1', '14', '6', '2020-11-11 18:27:17', null, '589', null, null);
INSERT INTO `cell` VALUES ('781', null, '3', '0', '1', '13', '6', '2020-11-11 18:27:17', null, '588', null, null);
INSERT INTO `cell` VALUES ('782', null, '3', '0', '1', '12', '6', '2020-11-11 18:27:17', null, '587', null, null);
INSERT INTO `cell` VALUES ('783', null, '3', '0', '1', '11', '6', '2020-11-11 18:27:17', null, '586', null, null);
INSERT INTO `cell` VALUES ('784', null, '3', '0', '1', '10', '6', '2020-11-11 18:27:17', null, '585', null, null);
INSERT INTO `cell` VALUES ('785', null, '3', '0', '1', '9', '6', '2020-11-11 18:27:17', null, '584', null, null);
INSERT INTO `cell` VALUES ('786', null, '3', '0', '1', '8', '6', '2020-11-11 18:27:17', null, '583', null, null);
INSERT INTO `cell` VALUES ('787', null, '3', '0', '1', '7', '6', '2020-11-11 18:27:17', null, '582', null, null);
INSERT INTO `cell` VALUES ('788', null, '3', '0', '1', '6', '6', '2020-11-11 18:27:17', null, '581', null, null);
INSERT INTO `cell` VALUES ('789', null, '3', '0', '1', '5', '6', '2020-11-11 18:27:17', null, '580', null, null);
INSERT INTO `cell` VALUES ('790', null, '3', '0', '1', '4', '6', '2020-11-11 18:27:17', null, '579', null, null);
INSERT INTO `cell` VALUES ('791', null, '3', '0', '1', '3', '6', '2020-11-11 18:27:17', null, '578', null, null);
INSERT INTO `cell` VALUES ('792', null, '3', '0', '1', '2', '6', '2020-11-11 18:27:17', null, '577', null, null);
INSERT INTO `cell` VALUES ('793', null, '3', '0', '2', '19', '1', '2020-11-11 18:27:35', null, '363', null, null);
INSERT INTO `cell` VALUES ('794', null, '3', '0', '2', '18', '1', '2020-11-11 18:27:35', null, '364', null, null);
INSERT INTO `cell` VALUES ('795', null, '3', '0', '2', '17', '1', '2020-11-11 18:27:35', null, '365', null, null);
INSERT INTO `cell` VALUES ('796', null, '3', '0', '2', '16', '1', '2020-11-11 18:27:35', null, '366', null, null);
INSERT INTO `cell` VALUES ('797', null, '3', '0', '2', '15', '1', '2020-11-11 18:27:35', null, '367', null, null);
INSERT INTO `cell` VALUES ('798', null, '3', '0', '2', '14', '1', '2020-11-11 18:27:35', null, '368', null, null);
INSERT INTO `cell` VALUES ('799', null, '3', '0', '2', '13', '1', '2020-11-11 18:27:35', null, '369', null, null);
INSERT INTO `cell` VALUES ('800', null, '3', '0', '2', '12', '1', '2020-11-11 18:27:35', null, '370', null, null);
INSERT INTO `cell` VALUES ('801', null, '3', '0', '2', '11', '1', '2020-11-11 18:27:35', null, '371', null, null);
INSERT INTO `cell` VALUES ('802', null, '3', '0', '2', '10', '1', '2020-11-11 18:27:35', null, '372', null, null);
INSERT INTO `cell` VALUES ('803', null, '3', '0', '2', '9', '1', '2020-11-11 18:27:35', null, '373', null, null);
INSERT INTO `cell` VALUES ('804', null, '3', '0', '2', '8', '1', '2020-11-11 18:27:35', null, '374', null, null);
INSERT INTO `cell` VALUES ('805', null, '3', '0', '2', '7', '1', '2020-11-11 18:27:35', null, '375', null, null);
INSERT INTO `cell` VALUES ('806', null, '3', '0', '2', '6', '1', '2020-11-11 18:27:35', null, '376', null, null);
INSERT INTO `cell` VALUES ('807', null, '3', '0', '2', '5', '1', '2020-11-11 18:27:35', null, '377', null, null);
INSERT INTO `cell` VALUES ('808', null, '3', '0', '2', '4', '1', '2020-11-11 18:27:35', null, '378', null, null);
INSERT INTO `cell` VALUES ('809', null, '3', '0', '2', '3', '1', '2020-11-11 18:27:35', null, '379', null, null);
INSERT INTO `cell` VALUES ('810', null, '3', '0', '2', '2', '1', '2020-11-11 18:27:35', null, '380', null, null);
INSERT INTO `cell` VALUES ('811', null, '3', '0', '2', '1', '1', '2020-11-11 18:27:35', null, '381', null, null);
INSERT INTO `cell` VALUES ('812', null, '3', '0', '2', '19', '2', '2020-11-11 18:27:35', null, '400', null, null);
INSERT INTO `cell` VALUES ('813', null, '3', '0', '2', '18', '2', '2020-11-11 18:27:35', null, '399', null, null);
INSERT INTO `cell` VALUES ('814', null, '3', '0', '2', '17', '2', '2020-11-11 18:27:35', null, '398', null, null);
INSERT INTO `cell` VALUES ('815', null, '3', '0', '2', '16', '2', '2020-11-11 18:27:35', null, '397', null, null);
INSERT INTO `cell` VALUES ('816', null, '3', '0', '2', '15', '2', '2020-11-11 18:27:35', null, '396', null, null);
INSERT INTO `cell` VALUES ('817', null, '3', '0', '2', '14', '2', '2020-11-11 18:27:35', null, '395', null, null);
INSERT INTO `cell` VALUES ('818', null, '3', '0', '2', '13', '2', '2020-11-11 18:27:35', null, '394', null, null);
INSERT INTO `cell` VALUES ('819', null, '3', '0', '2', '12', '2', '2020-11-11 18:27:35', null, '393', null, null);
INSERT INTO `cell` VALUES ('820', null, '3', '0', '2', '11', '2', '2020-11-11 18:27:35', null, '392', null, null);
INSERT INTO `cell` VALUES ('821', null, '3', '0', '2', '10', '2', '2020-11-11 18:27:35', null, '391', null, null);
INSERT INTO `cell` VALUES ('822', null, '3', '0', '2', '9', '2', '2020-11-11 18:27:35', null, '390', null, null);
INSERT INTO `cell` VALUES ('823', null, '3', '0', '2', '8', '2', '2020-11-11 18:27:35', null, '389', null, null);
INSERT INTO `cell` VALUES ('824', null, '3', '0', '2', '7', '2', '2020-11-11 18:27:35', null, '388', null, null);
INSERT INTO `cell` VALUES ('825', null, '3', '0', '2', '6', '2', '2020-11-11 18:27:35', null, '387', null, null);
INSERT INTO `cell` VALUES ('826', null, '3', '0', '2', '5', '2', '2020-11-11 18:27:35', null, '386', null, null);
INSERT INTO `cell` VALUES ('827', null, '3', '0', '2', '4', '2', '2020-11-11 18:27:35', null, '385', null, null);
INSERT INTO `cell` VALUES ('828', null, '3', '0', '2', '3', '2', '2020-11-11 18:27:35', null, '384', null, null);
INSERT INTO `cell` VALUES ('829', null, '3', '0', '2', '2', '2', '2020-11-11 18:27:35', null, '383', null, null);
INSERT INTO `cell` VALUES ('830', null, '3', '0', '2', '1', '2', '2020-11-11 18:27:35', null, '382', null, null);
INSERT INTO `cell` VALUES ('831', null, '3', '0', '2', '19', '3', '2020-11-11 18:27:35', null, '401', null, null);
INSERT INTO `cell` VALUES ('832', null, '3', '0', '2', '18', '3', '2020-11-11 18:27:35', null, '402', null, null);
INSERT INTO `cell` VALUES ('833', null, '3', '0', '2', '17', '3', '2020-11-11 18:27:35', null, '403', null, null);
INSERT INTO `cell` VALUES ('834', null, '3', '0', '2', '16', '3', '2020-11-11 18:27:35', null, '404', null, null);
INSERT INTO `cell` VALUES ('835', null, '3', '0', '2', '15', '3', '2020-11-11 18:27:35', null, '405', null, null);
INSERT INTO `cell` VALUES ('836', null, '3', '0', '2', '14', '3', '2020-11-11 18:27:35', null, '406', null, null);
INSERT INTO `cell` VALUES ('837', null, '3', '0', '2', '13', '3', '2020-11-11 18:27:35', null, '407', null, null);
INSERT INTO `cell` VALUES ('838', null, '3', '0', '2', '12', '3', '2020-11-11 18:27:35', null, '408', null, null);
INSERT INTO `cell` VALUES ('839', null, '3', '0', '2', '11', '3', '2020-11-11 18:27:35', null, '409', null, null);
INSERT INTO `cell` VALUES ('840', null, '3', '0', '2', '10', '3', '2020-11-11 18:27:35', null, '410', null, null);
INSERT INTO `cell` VALUES ('841', null, '3', '0', '2', '9', '3', '2020-11-11 18:27:35', null, '411', null, null);
INSERT INTO `cell` VALUES ('842', null, '3', '0', '2', '8', '3', '2020-11-11 18:27:35', null, '412', null, null);
INSERT INTO `cell` VALUES ('843', null, '3', '0', '2', '7', '3', '2020-11-11 18:27:35', null, '413', null, null);
INSERT INTO `cell` VALUES ('844', null, '3', '0', '2', '6', '3', '2020-11-11 18:27:35', null, '414', null, null);
INSERT INTO `cell` VALUES ('845', null, '3', '0', '2', '5', '3', '2020-11-11 18:27:35', null, '415', null, null);
INSERT INTO `cell` VALUES ('846', null, '3', '0', '2', '4', '3', '2020-11-11 18:27:35', null, '416', null, null);
INSERT INTO `cell` VALUES ('847', null, '3', '0', '2', '3', '3', '2020-11-11 18:27:35', null, '417', null, null);
INSERT INTO `cell` VALUES ('848', null, '3', '0', '2', '2', '3', '2020-11-11 18:27:35', null, '418', null, null);
INSERT INTO `cell` VALUES ('849', null, '3', '0', '2', '1', '3', '2020-11-11 18:27:35', null, '419', null, null);
INSERT INTO `cell` VALUES ('850', null, '3', '0', '2', '19', '4', '2020-11-11 18:27:35', null, '438', null, null);
INSERT INTO `cell` VALUES ('851', null, '3', '0', '2', '18', '4', '2020-11-11 18:27:35', null, '437', null, null);
INSERT INTO `cell` VALUES ('852', null, '3', '0', '2', '17', '4', '2020-11-11 18:27:35', null, '436', null, null);
INSERT INTO `cell` VALUES ('853', null, '3', '0', '2', '16', '4', '2020-11-11 18:27:35', null, '435', null, null);
INSERT INTO `cell` VALUES ('854', null, '3', '0', '2', '15', '4', '2020-11-11 18:27:35', null, '434', null, null);
INSERT INTO `cell` VALUES ('855', null, '3', '0', '2', '14', '4', '2020-11-11 18:27:35', null, '433', null, null);
INSERT INTO `cell` VALUES ('856', null, '3', '0', '2', '13', '4', '2020-11-11 18:27:35', null, '432', null, null);
INSERT INTO `cell` VALUES ('857', null, '3', '0', '2', '12', '4', '2020-11-11 18:27:35', null, '431', null, null);
INSERT INTO `cell` VALUES ('858', null, '3', '0', '2', '11', '4', '2020-11-11 18:27:35', null, '430', null, null);
INSERT INTO `cell` VALUES ('859', null, '3', '0', '2', '10', '4', '2020-11-11 18:27:35', null, '429', null, null);
INSERT INTO `cell` VALUES ('860', null, '3', '0', '2', '9', '4', '2020-11-11 18:27:35', null, '428', null, null);
INSERT INTO `cell` VALUES ('861', null, '3', '0', '2', '8', '4', '2020-11-11 18:27:35', null, '427', null, null);
INSERT INTO `cell` VALUES ('862', null, '3', '0', '2', '7', '4', '2020-11-11 18:27:35', null, '426', null, null);
INSERT INTO `cell` VALUES ('863', null, '3', '0', '2', '6', '4', '2020-11-11 18:27:35', null, '425', null, null);
INSERT INTO `cell` VALUES ('864', null, '3', '0', '2', '5', '4', '2020-11-11 18:27:35', null, '424', null, null);
INSERT INTO `cell` VALUES ('865', null, '3', '0', '2', '4', '4', '2020-11-11 18:27:35', null, '423', null, null);
INSERT INTO `cell` VALUES ('866', null, '3', '0', '2', '3', '4', '2020-11-11 18:27:35', null, '422', null, null);
INSERT INTO `cell` VALUES ('867', null, '3', '0', '2', '2', '4', '2020-11-11 18:27:35', null, '421', null, null);
INSERT INTO `cell` VALUES ('868', null, '3', '0', '2', '1', '4', '2020-11-11 18:27:35', null, '420', null, null);
INSERT INTO `cell` VALUES ('869', null, '3', '0', '2', '19', '5', '2020-11-11 18:27:35', null, '439', null, null);
INSERT INTO `cell` VALUES ('870', null, '3', '0', '2', '18', '5', '2020-11-11 18:27:35', null, '440', null, null);
INSERT INTO `cell` VALUES ('871', null, '3', '0', '2', '17', '5', '2020-11-11 18:27:35', null, '441', null, null);
INSERT INTO `cell` VALUES ('872', null, '3', '0', '2', '16', '5', '2020-11-11 18:27:35', null, '442', null, null);
INSERT INTO `cell` VALUES ('873', null, '3', '0', '2', '15', '5', '2020-11-11 18:27:35', null, '443', null, null);
INSERT INTO `cell` VALUES ('874', null, '3', '0', '2', '14', '5', '2020-11-11 18:27:35', null, '444', null, null);
INSERT INTO `cell` VALUES ('875', null, '3', '0', '2', '13', '5', '2020-11-11 18:27:35', null, '445', null, null);
INSERT INTO `cell` VALUES ('876', null, '3', '0', '2', '12', '5', '2020-11-11 18:27:35', null, '446', null, null);
INSERT INTO `cell` VALUES ('877', null, '3', '0', '2', '11', '5', '2020-11-11 18:27:35', null, '447', null, null);
INSERT INTO `cell` VALUES ('878', null, '3', '0', '2', '10', '5', '2020-11-11 18:27:35', null, '448', null, null);
INSERT INTO `cell` VALUES ('879', null, '3', '0', '2', '9', '5', '2020-11-11 18:27:35', null, '449', null, null);
INSERT INTO `cell` VALUES ('880', null, '3', '0', '2', '8', '5', '2020-11-11 18:27:35', null, '450', null, null);
INSERT INTO `cell` VALUES ('881', null, '3', '0', '2', '7', '5', '2020-11-11 18:27:35', null, '451', null, null);
INSERT INTO `cell` VALUES ('882', null, '3', '0', '2', '6', '5', '2020-11-11 18:27:35', null, '452', null, null);
INSERT INTO `cell` VALUES ('883', null, '3', '0', '2', '5', '5', '2020-11-11 18:27:35', null, '453', null, null);
INSERT INTO `cell` VALUES ('884', null, '3', '0', '2', '4', '5', '2020-11-11 18:27:35', null, '454', null, null);
INSERT INTO `cell` VALUES ('885', null, '3', '0', '2', '3', '5', '2020-11-11 18:27:35', null, '455', null, null);
INSERT INTO `cell` VALUES ('886', null, '3', '0', '2', '2', '5', '2020-11-11 18:27:35', null, '456', null, null);
INSERT INTO `cell` VALUES ('887', null, '3', '0', '2', '1', '5', '2020-11-11 18:27:35', null, '457', null, null);
INSERT INTO `cell` VALUES ('888', null, '3', '0', '2', '19', '6', '2020-11-11 18:27:35', null, '476', null, null);
INSERT INTO `cell` VALUES ('889', null, '3', '0', '2', '18', '6', '2020-11-11 18:27:35', null, '475', null, null);
INSERT INTO `cell` VALUES ('890', null, '3', '0', '2', '17', '6', '2020-11-11 18:27:35', null, '474', null, null);
INSERT INTO `cell` VALUES ('891', null, '3', '0', '2', '16', '6', '2020-11-11 18:27:35', null, '473', null, null);
INSERT INTO `cell` VALUES ('892', null, '3', '0', '2', '15', '6', '2020-11-11 18:27:35', null, '472', null, null);
INSERT INTO `cell` VALUES ('893', null, '3', '0', '2', '14', '6', '2020-11-11 18:27:35', null, '471', null, null);
INSERT INTO `cell` VALUES ('894', null, '3', '0', '2', '13', '6', '2020-11-11 18:27:35', null, '470', null, null);
INSERT INTO `cell` VALUES ('895', null, '3', '0', '2', '12', '6', '2020-11-11 18:27:35', null, '469', null, null);
INSERT INTO `cell` VALUES ('896', null, '3', '0', '2', '11', '6', '2020-11-11 18:27:35', null, '468', null, null);
INSERT INTO `cell` VALUES ('897', null, '3', '0', '2', '10', '6', '2020-11-11 18:27:35', null, '467', null, null);
INSERT INTO `cell` VALUES ('898', null, '3', '0', '2', '9', '6', '2020-11-11 18:27:35', null, '466', null, null);
INSERT INTO `cell` VALUES ('899', null, '3', '0', '2', '8', '6', '2020-11-11 18:27:35', null, '465', null, null);
INSERT INTO `cell` VALUES ('900', null, '3', '0', '2', '7', '6', '2020-11-11 18:27:35', null, '464', null, null);
INSERT INTO `cell` VALUES ('901', null, '3', '0', '2', '6', '6', '2020-11-11 18:27:35', null, '463', null, null);
INSERT INTO `cell` VALUES ('902', null, '3', '0', '2', '5', '6', '2020-11-11 18:27:35', null, '462', null, null);
INSERT INTO `cell` VALUES ('903', null, '3', '0', '2', '4', '6', '2020-11-11 18:27:35', null, '461', null, null);
INSERT INTO `cell` VALUES ('904', null, '3', '0', '2', '3', '6', '2020-11-11 18:27:35', null, '460', null, null);
INSERT INTO `cell` VALUES ('905', null, '3', '0', '2', '2', '6', '2020-11-11 18:27:35', null, '459', null, null);
INSERT INTO `cell` VALUES ('906', null, '3', '0', '2', '1', '6', '2020-11-11 18:27:35', null, '458', null, null);

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
) ENGINE=InnoDB AUTO_INCREMENT=479 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='系统日志';

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
INSERT INTO `sys_para` VALUES ('AddrByModBus', '1', 'R0', 'Modbus读取地址', 'Modbus读取地址', '', '2', '3');
INSERT INTO `sys_para` VALUES ('BtsRemoteAddress', '1', 'net.tcp://localhost:8014/TcpServiceForBTS', 'BTS提供的WCF地址', 'BTS提供的WCF地址', '\0', '4', '4');
INSERT INTO `sys_para` VALUES ('ComTitle', '1', '卫蓝消防', '软件名称', '界面显示的软件名称', '', '0', '1');
INSERT INTO `sys_para` VALUES ('CurrentUserID', '2', '0', '当前用户ID', '当前用户ID，调用MES接口时用到', '', '4', '7');
INSERT INTO `sys_para` VALUES ('FcRgvId', '2', '2', '分容柜RGV在MES中的ID', '分容柜RGV在MES中的ID', '', '2', '7');
INSERT INTO `sys_para` VALUES ('FcStandbyRgvId', '2', '2', '常温静置RGV在MES中的ID', '常温静置RGV在MES中的ID', '', '1', '7');
INSERT INTO `sys_para` VALUES ('FcUploadCount', '2', '6', '判断分容传感器温度过高的数量', '判断分容传感器温度过高的数量，总共12个温度感应器，值范围1-12', '', '3', '5');
INSERT INTO `sys_para` VALUES ('FcUploadVal', '2', '0', '分容上传MES温度值类型', '分容上传MES温度值类型。0：库位最高值 1：库位平均值', '', '2', '5');
INSERT INTO `sys_para` VALUES ('FireIpAddrModbus', '1', '127.0.0.1', 'Modbus服务器Ip地址', 'Modbus服务器Ip地址', '', '1', '3');
INSERT INTO `sys_para` VALUES ('FirePortAddrModbus', '1', '60010', 'Modbus端口', 'Modbus端口，多个端口用英文逗号分隔', '', '1', '3');
INSERT INTO `sys_para` VALUES ('HotStandbyRgvId', '2', '3', '高温静置RGV在MES中的ID', '高温静置RGV在MES中的ID', '', '3', '7');
INSERT INTO `sys_para` VALUES ('HttpServiceForBTS', '1', 'http://localhost:8013/FcServiceForBTS', '和BTS通讯的HTTP地址', '和BTS通讯的HTTP地址', '\0', '1', '4');
INSERT INTO `sys_para` VALUES ('HttpServiceForMES', '1', 'http://localhost:10301/FcService', '和MES通讯的HTTP地址', '和MES通讯的HTTP地址', '\0', '3', '4');
INSERT INTO `sys_para` VALUES ('IpAddrForFc', '1', '127.0.0.1', '分容压床PLC的Ip地址', '分容压床PLC的Ip地址', '', '14', '2');
INSERT INTO `sys_para` VALUES ('IpAddrForFcStandby', '1', '127.0.0.1', '分容静置PLC的IP地址', '分容静置PLC的IP地址', '', '10', '2');
INSERT INTO `sys_para` VALUES ('IpAddrForHotStandby', '1', '127.0.0.1', '高温静置PLC的Ip地址', '高温静置PLC的Ip地址', '', '12', '2');
INSERT INTO `sys_para` VALUES ('IpPortForFc', '1', '60010', '分容压床PLC的网络端口', '分容压床PLC的网络端口，多个端口用英文逗号分隔', '', '15', '2');
INSERT INTO `sys_para` VALUES ('IpPortForFcStandby', '1', '60010', '分容静置PLC网络端口', '分容静置PLC网络端口，多个端口用英文逗号分隔', '', '11', '2');
INSERT INTO `sys_para` VALUES ('IpPortForHotStandby', '1', '60010', '高温静置PLC的网络端口', '高温静置PLC的网络端口，多个端口用英文逗号分隔', '', '13', '2');
INSERT INTO `sys_para` VALUES ('IsDebug', '4', 'False', '调试模式', 'True:开启设备调试模式，不涉及第三方系统，False:关闭调试模式。实际生产请关闭调试模型。', '', '0', '1');
INSERT INTO `sys_para` VALUES ('IsSimSpray', '4', 'False', '是否模拟喷淋', '开启调试模式下，若有烟雾或温度升高，是否要喷淋。True:喷淋，False:无。', '', '2', '1');
INSERT INTO `sys_para` VALUES ('NeedInitCell', '4', 'False', '是否初始化库位', '根据MES信息初始化库位，生产环境仅初始化一次，永久置为False', '\0', '0', '1');
INSERT INTO `sys_para` VALUES ('OpenFireFlag', '4', 'False', '是否开启消防判断', '是否开启消防判断', '\0', '0', '1');
INSERT INTO `sys_para` VALUES ('SlaveAddress', '2', '1', '从站地址', '从站地址(范围0-255)', '', '2', '3');
INSERT INTO `sys_para` VALUES ('TcpServiceForBTS', '1', 'net.tcp://localhost:8012/FcServiceForBTS', '和BTS通讯的TCP地址', '和BTS通讯的TCP地址', '\0', '0', '4');
INSERT INTO `sys_para` VALUES ('TcpServiceForMES', '1', 'net.tcp://localhost:10300/FcService', '和MES通讯的TCP地址', '和MES通讯的TCP地址', '\0', '2', '4');
INSERT INTO `sys_para` VALUES ('WithoutByHand', '2', '120', '自动执行消防延迟时间（秒）', '若发生火警且执行喷淋，隔多少秒无人处理，则自动执行消防（单位秒）', '', '1', '5');
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
INSERT INTO `sys_para_sort` VALUES ('3', 'Modbus', '感温光纤配置', '0');
INSERT INTO `sys_para_sort` VALUES ('4', '其他', '其他配置', '1');
INSERT INTO `sys_para_sort` VALUES ('5', '喷淋条件', '喷淋条件相关配置', '0');
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
