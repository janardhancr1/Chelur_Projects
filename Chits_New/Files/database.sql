/*
SQLyog Community v8.32 
MySQL - 5.0.41-community-nt : Database - chits
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
CREATE DATABASE /*!32312 IF NOT EXISTS*/`chits` /*!40100 DEFAULT CHARACTER SET latin1 */;

USE `chits`;

/*Table structure for table `at_kt` */

DROP TABLE IF EXISTS `at_kt`;

CREATE TABLE `at_kt` (
  `Record_ID` int(11) NOT NULL auto_increment,
  `ATKT_No` varchar(10) default NULL,
  `Party_Name` varchar(50) default NULL,
  `ATKT_Date` datetime default NULL,
  `Tran_Type` varchar(20) default NULL,
  `Amount` float default NULL,
  `Remarks` varchar(255) default NULL,
  `Closed` char(1) default NULL,
  `Closed_Date` datetime default NULL,
  PRIMARY KEY  (`Record_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `cities` */

DROP TABLE IF EXISTS `cities`;

CREATE TABLE `cities` (
  `City_ID` int(11) NOT NULL auto_increment,
  `Village_Name` varchar(50) default NULL,
  `City_Name` varchar(50) default NULL,
  `State` varchar(50) default NULL,
  `Pincode` varchar(10) default NULL,
  PRIMARY KEY  (`City_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `customers` */

DROP TABLE IF EXISTS `customers`;

CREATE TABLE `customers` (
  `Customer_id` int(11) NOT NULL auto_increment,
  `Customer_name` varchar(50) default NULL,
  `Son_husband` varchar(50) default NULL,
  `Account_no` varchar(10) default NULL,
  `Res_address` varchar(50) default NULL,
  `Res_village` int(11) default NULL,
  `Res_phone` varchar(20) default NULL,
  PRIMARY KEY  (`Customer_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `fixeddeposit` */

DROP TABLE IF EXISTS `fixeddeposit`;

CREATE TABLE `fixeddeposit` (
  `FD_No` varchar(10) NOT NULL default '',
  `Customer_ID` int(11) default NULL,
  `Start_Date` datetime default NULL,
  `Nominee_Name` varchar(100) default NULL,
  `Relationship` varchar(100) default NULL,
  `Amount` float default NULL,
  `Rate` float default NULL,
  `Closed` char(1) default NULL,
  PRIMARY KEY  (`FD_No`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `fixedinterest` */

DROP TABLE IF EXISTS `fixedinterest`;

CREATE TABLE `fixedinterest` (
  `Record_ID` int(11) NOT NULL auto_increment,
  `FD_No` varchar(10) default NULL,
  `Paid_Date` datetime default NULL,
  `Interest_Amount` float default NULL,
  `Voucher_No` varchar(10) default NULL,
  `Interest_Upto` datetime default NULL,
  PRIMARY KEY  (`Record_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `fixedtrans` */

DROP TABLE IF EXISTS `fixedtrans`;

CREATE TABLE `fixedtrans` (
  `Record_ID` int(11) NOT NULL auto_increment,
  `FD_No` varchar(10) default NULL,
  `Paid_Date` datetime default NULL,
  `Amount` float default NULL,
  `Receipt_No` varchar(10) default NULL,
  PRIMARY KEY  (`Record_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `groups` */

DROP TABLE IF EXISTS `groups`;

CREATE TABLE `groups` (
  `Group_ID` int(11) NOT NULL auto_increment,
  `Group_Name` varchar(25) default NULL,
  `Main_Group` varchar(15) default NULL,
  `Sub_Group` varchar(25) default NULL,
  `Cr_Dr` varchar(3) default NULL,
  PRIMARY KEY  (`Group_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `hundiinterest` */

DROP TABLE IF EXISTS `hundiinterest`;

CREATE TABLE `hundiinterest` (
  `Record_id` int(11) NOT NULL auto_increment,
  `Hl_loanno` varchar(10) default NULL,
  `Interest_amount` float default NULL,
  `Receipt_no` varchar(10) default NULL,
  `Paid_date` datetime default NULL,
  `Interest_upto` datetime default NULL,
  PRIMARY KEY  (`Record_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `hundiloans` */

DROP TABLE IF EXISTS `hundiloans`;

CREATE TABLE `hundiloans` (
  `Hl_loanno` varchar(10) NOT NULL default '',
  `Customer_id` int(11) default NULL,
  `Co_obligent` varchar(50) default NULL,
  `Coobligent_address` varchar(50) default NULL,
  `Loan_amount` float default NULL,
  `Loan_date` datetime default NULL,
  `Closed` char(1) default NULL,
  `Rate` float default NULL,
  `Pay_mode` int(11) default NULL,
  `Cheque_no` varchar(10) default NULL,
  `Bank_id` int(11) default NULL,
  PRIMARY KEY  (`Hl_loanno`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `hunditrans` */

DROP TABLE IF EXISTS `hunditrans`;

CREATE TABLE `hunditrans` (
  `Record_id` int(11) NOT NULL auto_increment,
  `Hl_loanno` varchar(10) default NULL,
  `Paid_date` datetime default NULL,
  `Receipt_no` varchar(10) default NULL,
  `Amount` float default NULL,
  PRIMARY KEY  (`Record_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `ledgers` */

DROP TABLE IF EXISTS `ledgers`;

CREATE TABLE `ledgers` (
  `Ledger_ID` int(11) NOT NULL auto_increment,
  `Ledger_Name` varchar(60) default NULL,
  `Opening_Balance` double(10,2) default NULL,
  `Balance_Type` varchar(3) default NULL,
  `Group_ID` int(11) default NULL,
  PRIMARY KEY  (`Ledger_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `users` */

DROP TABLE IF EXISTS `users`;

CREATE TABLE `users` (
  `User_ID` int(11) NOT NULL auto_increment,
  `User_Name` varchar(30) default NULL,
  `Password` varchar(255) default NULL,
  PRIMARY KEY  (`User_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `voucher_types` */

DROP TABLE IF EXISTS `voucher_types`;

CREATE TABLE `voucher_types` (
  `VoucherType_ID` int(11) NOT NULL auto_increment,
  `VoucherType_Name` varchar(25) default NULL,
  PRIMARY KEY  (`VoucherType_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Table structure for table `vouchers` */

DROP TABLE IF EXISTS `vouchers`;

CREATE TABLE `vouchers` (
  `Voucher_ID` int(11) NOT NULL auto_increment,
  `Voucher_Date` datetime default NULL,
  `Voucher_Type` int(11) default NULL,
  `Voucher_No` int(11) default NULL,
  `From_Ledger` int(11) default NULL,
  `To_Ledger` int(11) default NULL,
  `Amount` double(10,2) default NULL,
  `Narration` text,
  PRIMARY KEY  (`Voucher_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
