-- phpMyAdmin SQL Dump
-- version 2.9.0.2
-- http://www.phpmyadmin.net
-- 
-- Хост: localhost
-- Время создания: Окт 19 2008 г., 18:10
-- Версия сервера: 5.0.45
-- Версия PHP: 5.2.3
-- 
-- База данных: `aver`
-- 

-- --------------------------------------------------------

-- 
-- Структура таблицы `extensions`
-- 

CREATE TABLE `extensions` (
  `IdSign` int(11) NOT NULL auto_increment,
  `FirstBytes` varchar(225) NOT NULL,
  `Offset` int(11) NOT NULL,
  `FriendlyText` varchar(255) NOT NULL,
  `Extension` varchar(255) NOT NULL,
  `ExtensionText` varchar(255) NOT NULL,
  UNIQUE KEY `IdSign` (`IdSign`),
  KEY `FirstBytes` (`FirstBytes`)
) ENGINE=MyISAM  DEFAULT CHARSET=utf8 ;

-- --------------------------------------------------------

-- 
-- Структура таблицы `fileinfo`
-- 

CREATE TABLE `fileinfo` (
  `IdFile` int(11) NOT NULL auto_increment,
  `isChecked` tinyint(1) NOT NULL default '0',
  `CheckedDate` datetime NOT NULL,
  `ShortName` varchar(1000) default NULL,
  `CreationTime` datetime default NULL,
  `CreationTimeUtc` datetime default NULL,
  `Directory` varchar(1000) default NULL,
  `DirectoryName` varchar(1000) default NULL,
  `Extension` varchar(255) default NULL,
  `FullName` varchar(1000) default NULL,
  `IsReadOnly` tinyint(1) default '0',
  `LastAccessTime` datetime default NULL,
  `LastAccessTimeUtc` datetime default NULL,
  `LastWriteTime` datetime default NULL,
  `LastWriteTimeUtc` datetime default NULL,
  `Length` int(11) default NULL,
  `Attributes` varchar(255) default NULL,
  `Hash` varchar(255) default NULL,
  `Comments` varchar(1000) default NULL,
  `CompanyName` varchar(255) default NULL,
  `FileBuildPart` int(11) default '0',
  `FileDescription` varchar(255) default NULL,
  `FileMajorPart` int(11) default '0',
  `FileMinorPart` int(11) default '0',
  `FileName` varchar(1024) default NULL,
  `FilePrivatePart` int(11) default '0',
  `FileVersion` varchar(255) default NULL,
  `InternalName` varchar(255) default NULL,
  `IsDebug` tinyint(1) default '0',
  `IsPatched` tinyint(1) default '0',
  `IsPreRelease` tinyint(1) default '0',
  `IsPrivateBuild` tinyint(1) default '0',
  `IsSpecialBuild` tinyint(1) default '0',
  `Language` varchar(255) default NULL,
  `LegalCopyright` varchar(255) default NULL,
  `LegalTrademarks` varchar(255) default NULL,
  `OriginalFilename` varchar(255) default NULL,
  `PrivateBuild` varchar(255) default NULL,
  `ProductBuildPart` int(11) default '0',
  `ProductMajorPart` int(11) default '0',
  `ProductMinorPart` int(11) default '0',
  `ProductName` varchar(255) default NULL,
  `ProductPrivatePart` int(11) default '0',
  `ProductVersion` varchar(255) default NULL,
  `SpecialBuild` varchar(255) default NULL,
  UNIQUE KEY `IdFile` (`IdFile`)
) ENGINE=MyISAM  DEFAULT CHARSET=utf8 ;
