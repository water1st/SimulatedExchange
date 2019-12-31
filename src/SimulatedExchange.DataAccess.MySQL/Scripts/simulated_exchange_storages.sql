/*
 Navicat Premium Data Transfer

 Source Server         : 192.168.82.37
 Source Server Type    : MySQL
 Source Server Version : 80015
 Source Host           : 192.168.82.37:3306
 Source Schema         : simulated_exchange_storages

 Target Server Type    : MySQL
 Target Server Version : 80015
 File Encoding         : 65001

 Date: 18/12/2019 11:48:05
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for events_storage
-- ----------------------------
DROP TABLE IF EXISTS `events_storage`;
CREATE TABLE `events_storage`  (
  `Id` varchar(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `AggregateId` varchar(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `Event` text CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `EventType` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `Version` int(11) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for memento_storage
-- ----------------------------
DROP TABLE IF EXISTS `memento_storage`;
CREATE TABLE `memento_storage`  (
  `Id` varchar(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `AggregateId` varchar(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `Memento` text CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL,
  `MementoType` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `Version` int(11) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_unicode_ci ROW_FORMAT = Dynamic;

SET FOREIGN_KEY_CHECKS = 1;
