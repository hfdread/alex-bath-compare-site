-- ----------------------------------------------------------------------------
-- MySQL Workbench Migration
-- Migrated Schemata: alex_items_db
-- Source Schemata: alex_items_db
-- Created: Sat Nov 29 00:25:06 2014
-- ----------------------------------------------------------------------------

SET FOREIGN_KEY_CHECKS = 0;;

-- ----------------------------------------------------------------------------
-- Schema alex_items_db
-- ----------------------------------------------------------------------------
DROP SCHEMA IF EXISTS `alex_items_db` ;
CREATE SCHEMA IF NOT EXISTS `alex_items_db` ;

-- ----------------------------------------------------------------------------
-- Table alex_items_db.AspNetRoles
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `alex_items_db`.`AspNetRoles` (
  `Id` VARCHAR(128) NOT NULL,
  `Name` VARCHAR(256) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE INDEX `RoleNameIndex` (`Name`(255) ASC));

-- ----------------------------------------------------------------------------
-- Table alex_items_db.AspNetUserRoles
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `alex_items_db`.`AspNetUserRoles` (
  `UserId` VARCHAR(128) NOT NULL,
  `RoleId` VARCHAR(128) NOT NULL,
  PRIMARY KEY (`UserId`, `RoleId`),
  INDEX `IX_UserId` (`UserId` ASC),
  INDEX `IX_RoleId` (`RoleId` ASC),
  CONSTRAINT `FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId`
    FOREIGN KEY (`RoleId`)
    REFERENCES `alex_items_db`.`AspNetRoles` (`Id`)
    ON DELETE CASCADE
    ON UPDATE NO ACTION,
  CONSTRAINT `FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId`
    FOREIGN KEY (`UserId`)
    REFERENCES `alex_items_db`.`AspNetUsers` (`Id`)
    ON DELETE CASCADE
    ON UPDATE NO ACTION);

-- ----------------------------------------------------------------------------
-- Table alex_items_db.AspNetUsers
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `alex_items_db`.`AspNetUsers` (
  `Id` VARCHAR(128) NOT NULL,
  `Email` VARCHAR(256) NULL,
  `EmailConfirmed` TINYINT(1) NOT NULL,
  `PasswordHash` LONGTEXT NULL,
  `SecurityStamp` LONGTEXT NULL,
  `PhoneNumber` LONGTEXT NULL,
  `PhoneNumberConfirmed` TINYINT(1) NOT NULL,
  `TwoFactorEnabled` TINYINT(1) NOT NULL,
  `LockoutEndDateUtc` DATETIME NULL,
  `LockoutEnabled` TINYINT(1) NOT NULL,
  `AccessFailedCount` INT NOT NULL,
  `UserName` VARCHAR(256) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE INDEX `UserNameIndex` (`UserName`(255) ASC));

-- ----------------------------------------------------------------------------
-- Table alex_items_db.AspNetUserClaims
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `alex_items_db`.`AspNetUserClaims` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `UserId` VARCHAR(128) NOT NULL,
  `ClaimType` LONGTEXT NULL,
  `ClaimValue` LONGTEXT NULL,
  PRIMARY KEY (`Id`),
  INDEX `IX_UserId` (`UserId` ASC),
  CONSTRAINT `FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId`
    FOREIGN KEY (`UserId`)
    REFERENCES `alex_items_db`.`AspNetUsers` (`Id`)
    ON DELETE CASCADE
    ON UPDATE NO ACTION);

-- ----------------------------------------------------------------------------
-- Table alex_items_db.AspNetUserLogins
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `alex_items_db`.`AspNetUserLogins` (
  `LoginProvider` VARCHAR(128) NOT NULL,
  `ProviderKey` VARCHAR(128) NOT NULL,
  `UserId` VARCHAR(128) NOT NULL,
  PRIMARY KEY (`LoginProvider`, `ProviderKey`, `UserId`),
  INDEX `IX_UserId` (`UserId` ASC),
  CONSTRAINT `FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId`
    FOREIGN KEY (`UserId`)
    REFERENCES `alex_items_db`.`AspNetUsers` (`Id`)
    ON DELETE CASCADE
    ON UPDATE NO ACTION);

-- ----------------------------------------------------------------------------
-- Table alex_items_db.__MigrationHistory
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `alex_items_db`.`__MigrationHistory` (
  `MigrationId` VARCHAR(150) NOT NULL,
  `ContextKey` VARCHAR(300) NOT NULL,
  `Model` LONGBLOB NOT NULL,
  `ProductVersion` VARCHAR(32) NOT NULL,
  PRIMARY KEY (`MigrationId`, `ContextKey`(255)));
SET FOREIGN_KEY_CHECKS = 1;;
