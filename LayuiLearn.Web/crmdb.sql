/*
 Navicat Premium Data Transfer

 Source Server         : localhost
 Source Server Type    : MySQL
 Source Server Version : 80012
 Source Host           : localhost:3306
 Source Schema         : crmdb

 Target Server Type    : MySQL
 Target Server Version : 80012
 File Encoding         : 65001

 Date: 19/09/2018 18:13:42
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for department
-- ----------------------------
DROP TABLE IF EXISTS `department`;
CREATE TABLE `department`  (
  `DeptCode` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '部门编号',
  `DeptName` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '部门名称',
  `Remark` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '备注',
  `ParentCode` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '上级部门编号',
  `DeptLevel` int(11) NOT NULL COMMENT '部门级别',
  `Lmid` varchar(25) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '最后编辑人',
  `Lmdt` datetime(0) NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP(0) COMMENT '最后编辑时间',
  `StopFlag` bit(1) NULL DEFAULT NULL COMMENT '停用状态 默认0 未停用 1 停用',
  PRIMARY KEY (`DeptCode`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of department
-- ----------------------------
INSERT INTO `department` VALUES ('D000001', '总部', NULL, NULL, 0, '-', '2018-05-11 17:35:07', b'0');
INSERT INTO `department` VALUES ('D000002', '珍茗', '方法试试11', 'D000001', 0, '-', '2018-05-12 09:20:55', b'0');
INSERT INTO `department` VALUES ('D000003', 'It部', 'dd', 'D000002', 0, '-', '2018-05-12 11:34:52', b'0');
INSERT INTO `department` VALUES ('D000004', 'CRM', NULL, 'D000002', 1, '000001-Admin', '2018-04-09 17:20:26', b'0');
INSERT INTO `department` VALUES ('D000005', '博华', NULL, 'D000001', 0, '-', '2018-05-12 11:35:13', b'0');
INSERT INTO `department` VALUES ('D000008', '订单', '搜索ss', 'D000001', 0, '-', '2018-04-19 15:47:07', b'1');
INSERT INTO `department` VALUES ('D000009', '搜索', NULL, 'D000001', 0, '-', '2018-04-16 09:48:30', b'1');
INSERT INTO `department` VALUES ('D000011', '试试', NULL, 'D000001', 0, '-', '2018-04-16 09:55:51', b'1');
INSERT INTO `department` VALUES ('D000012', '订单', NULL, 'D000003', 0, '-', '2018-04-19 13:31:59', b'1');
INSERT INTO `department` VALUES ('D000013', '订单', NULL, 'D000001', 0, '-', '2018-04-19 13:34:18', b'1');
INSERT INTO `department` VALUES ('D000014', '是是是', NULL, 'D000001', 0, '-', '2018-04-19 13:34:23', b'1');
INSERT INTO `department` VALUES ('D000015', 'dd', NULL, 'D000003', 0, '-', '2018-04-19 14:47:38', b'1');
INSERT INTO `department` VALUES ('D000016', '1232', '123试试', 'D000003', 0, '-', '2018-04-19 15:23:51', b'1');
INSERT INTO `department` VALUES ('D000017', 'ss', 'ff', 'D000001', 0, '-', '2018-04-20 13:15:48', b'1');
INSERT INTO `department` VALUES ('D000018', 'sss', 'ddd', 'D000002', 0, '-', '2018-04-20 13:23:07', b'1');
INSERT INTO `department` VALUES ('D000019', 'sssfsdf', NULL, 'D000002', 0, '-', '2018-04-20 13:36:29', b'1');
INSERT INTO `department` VALUES ('D000020', 'ss', 'dd', NULL, 0, '-', '2018-04-23 13:17:31', b'1');
INSERT INTO `department` VALUES ('D000021', 'ss4', 'dd', 'D000001', 0, '-', '2018-05-12 10:06:21', b'0');
INSERT INTO `department` VALUES ('D000022', '试试', NULL, 'D000005', 0, '-', '2018-05-12 09:22:34', b'0');
INSERT INTO `department` VALUES ('D000023', 'tt2', '试试', 'D000001', 0, '-', '2018-05-12 09:24:57', b'0');
INSERT INTO `department` VALUES ('D000024', '订单', '搜索', 'D000001', 0, '-', '2018-04-27 10:02:32', b'0');
INSERT INTO `department` VALUES ('D000025', '是是是', '订单', 'D000001', 0, '-', '2018-04-27 10:02:55', b'0');
INSERT INTO `department` VALUES ('D000026', '搜索3', 's', 'D000001', 0, '-', '2018-05-12 09:20:15', b'0');
INSERT INTO `department` VALUES ('D000027', 'dd', '1111', 'D000001', 0, '-', '2018-04-27 10:05:02', b'1');
INSERT INTO `department` VALUES ('D000028', 'ss', NULL, 'D000001', 0, '-', '2018-04-27 10:07:47', b'1');
INSERT INTO `department` VALUES ('D000029', '孙菲菲', NULL, 'D000001', 0, '-', '2018-04-27 10:08:12', b'1');
INSERT INTO `department` VALUES ('D000030', 'd1', NULL, 'D000001', 0, '-', '2018-04-27 10:13:25', b'1');
INSERT INTO `department` VALUES ('D000031', 'ss', '11', 'D000003', 0, '-', '2018-04-27 10:13:38', b'1');
INSERT INTO `department` VALUES ('D000032', '111', NULL, 'D000001', 0, '-', '2018-04-27 10:13:50', b'1');
INSERT INTO `department` VALUES ('D000033', 'ss2', 'dd', 'D000023', 0, '-', '2018-05-12 11:35:20', b'0');
INSERT INTO `department` VALUES ('D000034', 'fff', 'ss', 'D000023', 0, '-', '2018-05-12 09:28:39', b'0');
INSERT INTO `department` VALUES ('D000035', 'fsss', 'ddd', 'D000023', 0, '-', '2018-05-11 14:46:05', b'1');
INSERT INTO `department` VALUES ('D000036', 'sf', 'ss', 'D000023', 0, '-', '2018-05-11 14:54:34', b'1');
INSERT INTO `department` VALUES ('D000037', 'ss3', NULL, 'D000003', 0, '-', '2018-05-12 10:15:20', b'0');
INSERT INTO `department` VALUES ('D000038', 'ss', '111', 'D000003', 0, '-', '2018-05-11 17:16:12', b'1');
INSERT INTO `department` VALUES ('D000039', '111', NULL, 'D000003', 0, '-', '2018-05-12 10:16:00', b'0');

-- ----------------------------
-- Table structure for function
-- ----------------------------
DROP TABLE IF EXISTS `function`;
CREATE TABLE `function`  (
  `FunctionCode` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '权限编号',
  `FunctionEnglish` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '英文编号',
  `FunctionChina` varchar(80) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '中文名称',
  `FunctionDescrip` varchar(80) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '说明',
  `ParentCode` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '父节点',
  `MenuFlag` bit(1) NOT NULL COMMENT '是否为菜单 1菜单 0权限',
  `StopFlag` bit(1) NULL DEFAULT NULL COMMENT '是否停用',
  `URLString` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '链接URL',
  `editdate` datetime(0) NULL DEFAULT NULL COMMENT '创建或修改时间',
  `editor` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '修改人 名字加工号 张三(000001)',
  `sortidx` int(11) NULL DEFAULT NULL COMMENT '排序字段',
  `target` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT 'navTab 嵌套  _blank 新窗口 dialog 弹出窗',
  `MenuIcon` varchar(30) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '菜单图标class',
  PRIMARY KEY (`FunctionCode`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of function
-- ----------------------------
INSERT INTO `function` VALUES ('FC001', 'BASEINFO', '基础信息', NULL, '0', b'1', b'0', NULL, '2018-04-04 00:00:00', NULL, 1, 'navTab', NULL);
INSERT INTO `function` VALUES ('FC001001', 'USERINFO', '用户信息', NULL, 'FC001', b'1', b'0', '../User/Index', '2018-05-12 11:19:49', '-', 1, NULL, NULL);
INSERT INTO `function` VALUES ('FC001001001', 'USERINFOADD', '添加', NULL, 'FC001001', b'0', b'0', NULL, '2018-05-12 11:21:13', '-', 99, NULL, NULL);
INSERT INTO `function` VALUES ('FC001001002', 'USERINFOEDIT', '编辑', NULL, 'FC001001', b'0', b'0', NULL, '2018-05-14 13:41:28', '-', 99, NULL, NULL);
INSERT INTO `function` VALUES ('FC001001003', 'USERINFOREMOVE', '删除', NULL, 'FC001001', b'0', b'0', NULL, '2018-05-14 13:42:07', '-', 99, NULL, NULL);
INSERT INTO `function` VALUES ('FC001001004', 'USERINFOAUTH', '授权', NULL, 'FC001001', b'0', b'0', NULL, '2018-05-14 13:45:13', '-', 99, NULL, NULL);
INSERT INTO `function` VALUES ('FC001001005', 'USERINFOLIST', '查看', NULL, 'FC001001', b'0', b'0', NULL, '2018-05-14 13:48:11', '-', 98, NULL, NULL);
INSERT INTO `function` VALUES ('FC001002', 'ROLEINFO', '角色信息', NULL, 'FC001', b'1', b'0', '../Role/Index', '2018-04-04 00:00:00', NULL, 2, 'navTab', NULL);
INSERT INTO `function` VALUES ('FC001002001', 'ROLEINFOLIST', '查看', NULL, 'FC001002', b'0', b'0', NULL, '2018-05-15 13:09:51', '00000002-admin', 99, NULL, NULL);
INSERT INTO `function` VALUES ('FC001002002', 'ROLEINFOADD', '添加', NULL, 'FC001002', b'0', b'0', NULL, '2018-05-15 13:10:29', '00000002-admin', 99, NULL, NULL);
INSERT INTO `function` VALUES ('FC001002003', 'ROLEINFOEDIT', '编辑', NULL, 'FC001002', b'0', b'0', NULL, '2018-05-15 13:11:29', '00000002-admin', 99, NULL, NULL);
INSERT INTO `function` VALUES ('FC001002004', 'ROLEINFOREMOVE', '删除', NULL, 'FC001002', b'0', b'0', NULL, '2018-05-15 13:12:04', '00000002-admin', 99, NULL, NULL);
INSERT INTO `function` VALUES ('FC001002005', 'ROLEINFOAUTH', '授权', NULL, 'FC001002', b'0', b'0', NULL, '2018-05-15 13:13:19', '00000002-admin', 99, NULL, NULL);
INSERT INTO `function` VALUES ('FC001003', 'FUNCTION', '权限信息', NULL, 'FC001', b'1', b'0', '../Function/Index', '2018-05-15 13:18:40', '00000002-admin', 3, NULL, NULL);
INSERT INTO `function` VALUES ('FC001003001', 'FUNCTIONLIST', '查看', NULL, 'FC001003', b'0', b'0', NULL, '2018-05-15 13:20:01', '00000002-admin', 99, NULL, NULL);
INSERT INTO `function` VALUES ('FC001003002', 'FUNCTIONADD', '添加', NULL, 'FC001003', b'0', b'0', NULL, '2018-05-15 13:20:21', '00000002-admin', 99, NULL, NULL);
INSERT INTO `function` VALUES ('FC001003003', 'FUNCTIONEDIT', '编辑', NULL, 'FC001003', b'0', b'0', NULL, '2018-05-15 13:20:39', '00000002-admin', 99, NULL, NULL);
INSERT INTO `function` VALUES ('FC001003004', 'FUNCTIONREMOVE', '删除', NULL, 'FC001003', b'0', b'0', NULL, '2018-05-15 13:21:03', '00000002-admin', 99, NULL, NULL);
INSERT INTO `function` VALUES ('FC001004', 'DEPARTMENT', '组织信息', NULL, 'FC001', b'1', b'0', '../Department/Index', '2018-05-15 13:22:19', '00000002-admin', 4, NULL, NULL);
INSERT INTO `function` VALUES ('FC001004001', 'DEPARTMENTLIST', '查看', NULL, 'FC001004', b'0', b'0', NULL, '2018-05-15 13:22:40', '00000002-admin', 99, NULL, NULL);
INSERT INTO `function` VALUES ('FC001004002', 'DEPARTMENTADD', '添加', NULL, 'FC001004', b'0', b'0', NULL, '2018-05-15 13:23:00', '00000002-admin', 99, NULL, NULL);
INSERT INTO `function` VALUES ('FC001004003', 'DEPARTMENTEDIT', '编辑', NULL, 'FC001004', b'0', b'0', NULL, '2018-05-15 13:23:17', '00000002-admin', 99, NULL, NULL);
INSERT INTO `function` VALUES ('FC001004004', 'DEPARTMENTREMOVE', '删除', NULL, 'FC001004', b'0', b'0', NULL, '2018-05-15 13:23:37', '00000002-admin', 99, NULL, NULL);
INSERT INTO `function` VALUES ('FC002', 'BUSINESSINFO', '业务信息', NULL, '0', b'1', b'0', NULL, '2018-04-04 11:36:56', NULL, 2, 'navTab', NULL);

-- ----------------------------
-- Table structure for pub_user
-- ----------------------------
DROP TABLE IF EXISTS `pub_user`;
CREATE TABLE `pub_user`  (
  `Id` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `UserCode` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `UserName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `RealName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `UserPwd` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Sex` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `IdentityNo` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Birthday` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `DeptCode` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `ManagerFlag` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Tel` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `EMail` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `QQ` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Remark` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `StopFlag` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Crid` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Crdt` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Lmid` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Lmdt` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `LoginDate` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `ProvinceCode` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `CityCode` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `RegionCode` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `UserAddress` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Wxcode` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `HeadUrl` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of pub_user
-- ----------------------------
INSERT INTO `pub_user` VALUES ('1.0', '00000001', 'chi', '迟', '123123', '1.0', NULL, '00:00:00.000', 'D000001', '0.0', '15288133116', NULL, NULL, NULL, '0.0', NULL, '00:00:00.000', NULL, '2018-01-23', '00:00:00.000', NULL, NULL, NULL, NULL, NULL, NULL);
INSERT INTO `pub_user` VALUES ('2.0', '00000002', 'admin', 'admin', '123123', '0.0', NULL, '00:00:00.000', 'D000002', '0.0', '15288133116', NULL, NULL, '                        \n                    ', '0.0', NULL, '00:00:00.000', '00000002-admin', '2018-05-14 17:10:20.69', '00:00:00.000', NULL, NULL, NULL, NULL, NULL, NULL);

-- ----------------------------
-- Table structure for role
-- ----------------------------
DROP TABLE IF EXISTS `role`;
CREATE TABLE `role`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `RoleCode` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '角色编号',
  `RoleName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '角色名称',
  `Remark` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '备注',
  `StopFlag` bit(1) NOT NULL COMMENT '停用状态 默认0  未停用 1 停用',
  `Crid` varchar(15) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '创建人',
  `Crdt` datetime(0) NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP(0) COMMENT '创建时间',
  `Lmid` varchar(15) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '最后更新人',
  `Lmdt` datetime(0) NULL DEFAULT NULL COMMENT '最后更新时间',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of role
-- ----------------------------
INSERT INTO `role` VALUES (1, 'RC000001', '超级管理员', NULL, b'0', '00000001', '2018-04-04 00:00:00', '00000001', '2018-04-04 00:00:00');
INSERT INTO `role` VALUES (2, 'RC000002', '普通管理员', '222', b'0', NULL, '2018-05-10 17:27:25', '-', '2018-05-10 17:33:11');
INSERT INTO `role` VALUES (3, 'RC000003', '客服经理', '111122', b'0', NULL, '2018-05-10 17:27:25', '-', '2018-05-10 17:33:37');
INSERT INTO `role` VALUES (4, '000004', '销售经理', '12345', b'1', '-', '2018-05-10 17:27:25', '-', '2018-05-10 17:27:25');
INSERT INTO `role` VALUES (5, '000005', '客服专员', NULL, b'1', '-', '2018-05-11 09:45:59', '-', '2018-05-11 09:45:59');
INSERT INTO `role` VALUES (6, 'RC000006', '客服专员', '22221', b'0', NULL, '2018-05-10 17:27:25', '-', '2018-05-11 09:47:24');
INSERT INTO `role` VALUES (7, 'RC000007', 'tt', 'ss', b'1', '-', '2018-05-11 16:27:18', '-', '2018-05-11 16:27:18');
INSERT INTO `role` VALUES (8, 'RC000008', 'tt', '111', b'1', '-', '2018-05-14 10:50:43', '-', '2018-05-14 10:50:43');
INSERT INTO `role` VALUES (9, 'RC000009', 'SEO专员', NULL, b'0', '00000002-admin', '2018-09-18 16:52:55', '00000002-admin', '2018-09-18 16:52:55');
INSERT INTO `role` VALUES (10, 'RC000010', '竞价专员', NULL, b'0', '00000002-admin', '2018-09-18 17:08:47', '00000002-admin', '2018-09-18 17:08:47');
INSERT INTO `role` VALUES (11, 'RC000011', '前台', NULL, b'0', '00000002-admin', '2018-09-18 17:08:59', '00000002-admin', '2018-09-18 17:08:59');
INSERT INTO `role` VALUES (12, 'RC000012', '项目经理', NULL, b'0', '00000002-admin', '2018-09-18 17:09:13', '00000002-admin', '2018-09-18 17:09:13');
INSERT INTO `role` VALUES (13, 'RC000013', '项目总监', NULL, b'0', '00000002-admin', '2018-09-18 17:09:30', '00000002-admin', '2018-09-18 17:09:30');
INSERT INTO `role` VALUES (14, 'RC000014', '总经理', NULL, b'0', '00000002-admin', '2018-09-18 17:09:38', '00000002-admin', '2018-09-18 17:09:38');
INSERT INTO `role` VALUES (15, 'RC000015', '技术总监', NULL, b'0', '00000002-admin', '2018-09-18 17:09:54', '00000002-admin', '2018-09-18 17:09:54');

-- ----------------------------
-- Table structure for rolefunction
-- ----------------------------
DROP TABLE IF EXISTS `rolefunction`;
CREATE TABLE `rolefunction`  (
  `Id` int(11) NOT NULL,
  `RoleCode` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '角色编号',
  `FunctionCode` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '权限编号',
  `Lmid` varchar(15) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '最后编辑人',
  `Lmdt` datetime(0) NULL DEFAULT NULL COMMENT '最后编辑时间',
  `StopFlag` bit(1) NULL DEFAULT NULL COMMENT '最后编辑时间',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of rolefunction
-- ----------------------------
INSERT INTO `rolefunction` VALUES (1, '111', 'dddd', NULL, '2018-01-01 00:00:00', b'0');
INSERT INTO `rolefunction` VALUES (9, 'RC000006', 'FC001', NULL, '2018-01-01 00:00:00', b'0');
INSERT INTO `rolefunction` VALUES (10, 'RC000006', 'FC001001', NULL, '2018-01-01 00:00:00', b'0');
INSERT INTO `rolefunction` VALUES (11, 'RC000006', 'FC001001001', NULL, '2018-01-01 00:00:00', b'0');
INSERT INTO `rolefunction` VALUES (319, 'RC000001', 'FC001', NULL, '2018-01-01 00:00:00', b'0');
INSERT INTO `rolefunction` VALUES (320, 'RC000001', 'FC001001', NULL, '2018-01-01 00:00:00', b'0');
INSERT INTO `rolefunction` VALUES (321, 'RC000001', 'FC001001005', NULL, '2018-01-01 00:00:00', b'0');
INSERT INTO `rolefunction` VALUES (322, 'RC000001', 'FC001001001', NULL, '2018-01-01 00:00:00', b'0');
INSERT INTO `rolefunction` VALUES (323, 'RC000001', 'FC001001002', NULL, '2018-01-01 00:00:00', b'0');
INSERT INTO `rolefunction` VALUES (324, 'RC000001', 'FC001001003', NULL, '2018-01-01 00:00:00', b'0');
INSERT INTO `rolefunction` VALUES (325, 'RC000001', 'FC001001004', NULL, '2018-01-01 00:00:00', b'0');
INSERT INTO `rolefunction` VALUES (326, 'RC000001', 'FC001002', NULL, '2018-01-01 00:00:00', b'0');
INSERT INTO `rolefunction` VALUES (327, 'RC000001', 'FC001002001', NULL, '2018-01-01 00:00:00', b'0');
INSERT INTO `rolefunction` VALUES (328, 'RC000001', 'FC001002002', NULL, '2018-01-01 00:00:00', b'0');
INSERT INTO `rolefunction` VALUES (329, 'RC000001', 'FC001002003', NULL, '2018-01-01 00:00:00', b'0');
INSERT INTO `rolefunction` VALUES (330, 'RC000001', 'FC001002004', NULL, '2018-01-01 00:00:00', b'0');
INSERT INTO `rolefunction` VALUES (331, 'RC000001', 'FC001002005', NULL, '2018-01-01 00:00:00', b'0');
INSERT INTO `rolefunction` VALUES (332, 'RC000001', 'FC001003', NULL, '2018-01-01 00:00:00', b'0');
INSERT INTO `rolefunction` VALUES (333, 'RC000001', 'FC001003001', NULL, '2018-01-01 00:00:00', b'0');
INSERT INTO `rolefunction` VALUES (334, 'RC000001', 'FC001003002', NULL, '2018-01-01 00:00:00', b'0');
INSERT INTO `rolefunction` VALUES (335, 'RC000001', 'FC001003003', NULL, '2018-01-01 00:00:00', b'0');
INSERT INTO `rolefunction` VALUES (336, 'RC000001', 'FC001003004', NULL, '2018-01-01 00:00:00', b'0');
INSERT INTO `rolefunction` VALUES (337, 'RC000001', 'FC001004', NULL, '2018-01-01 00:00:00', b'0');
INSERT INTO `rolefunction` VALUES (338, 'RC000001', 'FC001004001', NULL, '2018-01-01 00:00:00', b'0');
INSERT INTO `rolefunction` VALUES (339, 'RC000001', 'FC001004002', NULL, '2018-01-01 00:00:00', b'0');
INSERT INTO `rolefunction` VALUES (340, 'RC000001', 'FC001004003', NULL, '2018-01-01 00:00:00', b'0');
INSERT INTO `rolefunction` VALUES (341, 'RC000001', 'FC001004004', NULL, '2018-01-01 00:00:00', b'0');
INSERT INTO `rolefunction` VALUES (342, 'RC000001', 'FC002', NULL, '2018-01-01 00:00:00', b'0');

-- ----------------------------
-- Table structure for user
-- ----------------------------
DROP TABLE IF EXISTS `user`;
CREATE TABLE `user`  (
  `Id` int(11) NOT NULL COMMENT '自增主键',
  `Account` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '登录用户名',
  `UserName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '昵称/用户名',
  `RealName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '真实姓名',
  `UserPwd` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '登录密码',
  `Sex` bit(1) NOT NULL COMMENT '性别',
  `IdentityNo` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '身份证号码',
  `Birthday` datetime(0) NULL DEFAULT NULL COMMENT '生日',
  `DeptCode` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '部门编号',
  `ManagerFlag` bit(1) NOT NULL COMMENT '是否是管理员 默认不是 0  是1',
  `Tel` varchar(25) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '电话',
  `EMail` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '邮箱',
  `QQ` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT 'QQ',
  `Remark` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '备注',
  `StopFlag` bit(1) NOT NULL COMMENT '停用状态 默认0 未停用 1停用',
  `Crid` varchar(15) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '创建人',
  `Crdt` datetime(0) NULL DEFAULT NULL COMMENT '创建时间',
  `Lmid` varchar(15) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '最后更新人',
  `Lmdt` datetime(0) NULL DEFAULT NULL COMMENT '最后更新时间',
  `LoginDate` datetime(0) NULL DEFAULT NULL COMMENT '最后登录时间',
  `ProvinceCode` varchar(15) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '省份编号',
  `CityCode` varchar(15) CHARACTER SET ascii COLLATE ascii_general_ci NULL DEFAULT NULL COMMENT '城市编号',
  `RegionCode` varchar(15) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '区域编号',
  `UserAddress` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '地址',
  `Wxcode` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '微信openid',
  `HeadUrl` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '头像地址',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of user
-- ----------------------------
INSERT INTO `user` VALUES (1, 'chi', 'chi', '迟', '123123', b'1', NULL, '2018-05-14 17:10:21', 'D000001', b'0', '15288133116', NULL, NULL, NULL, b'0', NULL, '2018-05-14 17:10:21', NULL, '2018-01-23 00:00:00', '2018-05-14 17:10:21', NULL, NULL, NULL, NULL, NULL, NULL);
INSERT INTO `user` VALUES (2, 'admin', 'admin', 'admin', '123123', b'0', NULL, '2018-05-14 17:10:21', 'D000002', b'0', '15288133116', NULL, NULL, '                        \n                    ', b'0', NULL, '2018-05-14 17:10:21', '00000002-admin', '2018-05-14 17:10:21', '2018-05-14 17:10:21', NULL, NULL, NULL, NULL, NULL, NULL);

-- ----------------------------
-- Table structure for userfunction
-- ----------------------------
DROP TABLE IF EXISTS `userfunction`;
CREATE TABLE `userfunction`  (
  `Id` int(11) NOT NULL,
  `UserCode` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '用户编号',
  `FunctionCode` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '权限编号',
  `Lmid` varchar(15) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '最后编辑人',
  `Lmdt` datetime(0) NULL DEFAULT NULL COMMENT '最后编辑时间',
  `StopFlag` bit(1) NULL DEFAULT NULL COMMENT '停用状态 默认0 未停用 1 停用',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of userfunction
-- ----------------------------
INSERT INTO `userfunction` VALUES (11, '00000003', 'FC001', NULL, '2018-01-01 00:00:00', b'0');
INSERT INTO `userfunction` VALUES (12, '00000003', 'FC001002', NULL, '2018-01-01 00:00:00', b'0');
INSERT INTO `userfunction` VALUES (17, '00000005', '', NULL, '2018-01-01 00:00:00', b'0');
INSERT INTO `userfunction` VALUES (30, '00000001', 'FC001', NULL, '2018-01-01 00:00:00', b'0');
INSERT INTO `userfunction` VALUES (31, '00000001', 'FC001001', NULL, '2018-01-01 00:00:00', b'0');
INSERT INTO `userfunction` VALUES (32, '00000001', 'FC001001005', NULL, '2018-01-01 00:00:00', b'0');
INSERT INTO `userfunction` VALUES (33, '00000001', 'FC001001001', NULL, '2018-01-01 00:00:00', b'0');

-- ----------------------------
-- Table structure for userrole
-- ----------------------------
DROP TABLE IF EXISTS `userrole`;
CREATE TABLE `userrole`  (
  `Id` int(11) NOT NULL,
  `UserCode` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '用户编号',
  `RoleCode` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '角色编号',
  `Lmid` varchar(15) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '最后编辑人',
  `Lmdt` datetime(0) NULL DEFAULT NULL COMMENT '最后编辑时间',
  `StopFlag` bit(1) NULL DEFAULT NULL COMMENT '停用状态 默认0 未停用 1 停用',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of userrole
-- ----------------------------
INSERT INTO `userrole` VALUES (11, '00000003', 'RC000001', NULL, '2018-01-01 00:00:00', b'0');
INSERT INTO `userrole` VALUES (12, '00000003', 'RC000002', NULL, '2018-01-01 00:00:00', b'0');
INSERT INTO `userrole` VALUES (13, '00000004', 'RC000003', NULL, '2018-01-01 00:00:00', b'0');
INSERT INTO `userrole` VALUES (14, '00000005', 'RC000002', NULL, '2018-01-01 00:00:00', b'0');
INSERT INTO `userrole` VALUES (16, '00000006', 'RC000002', NULL, '2018-01-01 00:00:00', b'0');
INSERT INTO `userrole` VALUES (17, '00000006', 'RC000003', NULL, '2018-01-01 00:00:00', b'0');
INSERT INTO `userrole` VALUES (18, '00000007', 'RC000001', NULL, '2018-01-01 00:00:00', b'0');
INSERT INTO `userrole` VALUES (19, '00000002', 'RC000001', NULL, '2018-01-01 00:00:00', b'0');

SET FOREIGN_KEY_CHECKS = 1;
