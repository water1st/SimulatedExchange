/*
 Navicat Premium Data Transfer

 Source Server         : 192.168.0.244
 Source Server Type    : MySQL
 Source Server Version : 80015
 Source Host           : 192.168.0.244:3306
 Source Schema         : simulated_exchange_reporting_storage

 Target Server Type    : MySQL
 Target Server Version : 80015
 File Encoding         : 65001

 Date: 06/01/2020 14:42:59
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for orders
-- ----------------------------
DROP TABLE IF EXISTS `orders`;
CREATE TABLE `orders`  (
  `Id` varchar(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `FromCurrencySymbol` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `ToCurrencySymbol` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NULL DEFAULT NULL,
  `Price` decimal(10, 0) NULL DEFAULT NULL,
  `Volume` decimal(10, 0) NULL DEFAULT NULL,
  `TotalAmount` decimal(10, 0) NULL DEFAULT NULL,
  `Exchange` int(11) NULL DEFAULT NULL,
  `Type` int(11) NULL DEFAULT NULL,
  `Status` int(11) NULL DEFAULT NULL,
  `CreatedTimeUtc` datetime(0) NULL DEFAULT NULL,
  `ModifyedTimeUtc` datetime(0) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_unicode_ci ROW_FORMAT = Dynamic;

SET FOREIGN_KEY_CHECKS = 1;
