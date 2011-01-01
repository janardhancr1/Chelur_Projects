/*
SQLyog Community Edition- MySQL GUI v8.17 
MySQL - 5.0.87-community-nt : Database - chits
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

/*Table structure for table `cities` */

DROP TABLE IF EXISTS `cities`;

CREATE TABLE `cities` (
  `City_ID` int(11) NOT NULL auto_increment,
  `Village_Name` varchar(50) default NULL,
  `City_Name` varchar(50) default NULL,
  `State` varchar(50) default NULL,
  `Pincode` varchar(10) default NULL,
  PRIMARY KEY  (`City_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1;

/*Data for the table `cities` */

insert  into `cities`(`City_ID`,`Village_Name`,`City_Name`,`State`,`Pincode`) values (1,'CHELUR','chlur','Karnataka','563124'),(2,'MUGIREDDYPALLI','venkatapur post','Karnataka','563124'),(3,'BANGLORE','banglore','Karnataka','560000');

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
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;

/*Data for the table `customers` */

insert  into `customers`(`Customer_id`,`Customer_name`,`Son_husband`,`Account_no`,`Res_address`,`Res_village`,`Res_phone`) values (1,'v.s.sathyanarayanarao','v.s.sheshagirirao','1','mugyreddipalli',2,'9480238041'),(2,'p.c.swathy','Ravikiran','2','k.r.puram',3,'9448536062');

/*Table structure for table `groups` */

DROP TABLE IF EXISTS `groups`;

CREATE TABLE `groups` (
  `Group_ID` int(11) NOT NULL auto_increment,
  `Group_Name` varchar(25) default NULL,
  `Main_Group` varchar(15) default NULL,
  `Sub_Group` varchar(25) default NULL,
  `Cr_Dr` varchar(3) default NULL,
  PRIMARY KEY  (`Group_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=29 DEFAULT CHARSET=latin1;

/*Data for the table `groups` */

insert  into `groups`(`Group_ID`,`Group_Name`,`Main_Group`,`Sub_Group`,`Cr_Dr`) values (1,'CAPITAL ACCOUNT','LIABILITIES','CAPITAL ACCOUNT','Cr'),(2,'RESERVES & SURPLUS','LIABILITIES','CAPITAL ACCOUNT','Cr'),(3,'LOANS (LIABILITIES)','LIABILITIES','LOANS (LIABILITIES)','Cr'),(4,'BANK OD A/C','LIABILITIES','LOANS (LIABILITIES)','Cr'),(5,'SECURED LOANS','LIABILITIES','LOANS (LIABILITIES)','Cr'),(6,'UNSECURED LOANS','LIABILITIES','LOANS (LIABILITIES)','Cr'),(7,'CURRENT LIABILITIES','LIABILITIES','CURRENT LIABILITIES','Cr'),(8,'DUTIES & TAXES','LIABILITIES','CURRENT LIABILITIES','Cr'),(9,'PROVISIONS','LIABILITIES','CURRENT LIABILITIES','Cr'),(10,'SUNDRY CREDITORS','LIABILITIES','CURRENT LIABILITIES','Cr'),(11,'FIXED ASSETS','ASSETS','FIXED ASSETS','Dr'),(12,'INVESTMENTS','ASSETS','INVESTMENTS','Dr'),(13,'CURRENT ASSETS','ASSETS','CURRENT ASSETS','Dr'),(14,'BANK ACCOUNTS','ASSETS','CURRENT ASSETS','Dr'),(15,'CASH-IN-HAND','ASSETS','CURRENT ASSETS','Dr'),(16,'DEPOSITS (ASSETS)','ASSETS','CURRENT ASSETS','Dr'),(17,'LOANS & ADVANCES (ASSETS)','ASSETS','CURRENT ASSETS','Dr'),(18,'STOCK-IN-HAND','ASSETS','CURRENT ASSETS','Dr'),(19,'SUNDRY DEBTORS','ASSETS','CURRENT ASSETS','Dr'),(20,'BRANCH / DIVISIONS','ASSETS','BRANCH / DIVISIONS','Dr'),(21,'MISC EXPENSES (ASSETS)','ASSETS','MISC EXPENSES (ASSETS)','Dr'),(22,'SUSPENSE A/C','ASSETS','SUSPENSE A/C','Dr'),(23,'SALES ACCOUNT','INCOME','SALES ACCOUNT','Cr'),(24,'DIRECT INCOMES','INCOME','DIRECT INCOMES','Cr'),(25,'INDIRECT INCOMES','INCOME','INDIRECT INCOMES','Cr'),(26,'PURCHASE ACCOUNTS','EXPENSE','PURCHASE ACCOUNTS','Dr'),(27,'DIRECT EXPENSES','EXPENSE','DIRECT EXPENSES','Dr'),(28,'INDIRECT EXPENSES','EXPENSE','INDIRECT EXPENSES','Dr');

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

/*Data for the table `hundiloans` */

insert  into `hundiloans`(`Hl_loanno`,`Customer_id`,`Co_obligent`,`Coobligent_address`,`Loan_amount`,`Loan_date`,`Closed`,`Rate`,`Pay_mode`,`Cheque_no`,`Bank_id`) values ('HL001',2,'','',50000,'2009-12-08 00:00:00','n',16,1,'',0);

/*Table structure for table `ledgers` */

DROP TABLE IF EXISTS `ledgers`;

CREATE TABLE `ledgers` (
  `Ledger_ID` int(11) NOT NULL auto_increment,
  `Ledger_Name` varchar(60) default NULL,
  `Opening_Balance` double(10,2) default NULL,
  `Balance_Type` varchar(3) default NULL,
  `Group_ID` int(11) default NULL,
  PRIMARY KEY  (`Ledger_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=latin1;

/*Data for the table `ledgers` */

insert  into `ledgers`(`Ledger_ID`,`Ledger_Name`,`Opening_Balance`,`Balance_Type`,`Group_ID`) values (1,'INTREST',0.00,'Cr',24),(2,'PATA(Exp)',0.00,'Dr',27),(3,'SALARIES',0.00,'Dr',27),(4,'A=T=K=T',0.00,'Cr',7),(5,'C-H-I-T-S',0.00,'Cr',7),(6,'CASH',0.00,'Cr',15),(7,'P.R.C.(RENTaccount)',0.00,'Cr',7),(8,'personal acc(dummy)',0.00,'Cr',7),(9,'H.L,INTREST',0.00,'Cr',24),(10,'P.R.CHLAM(personal)',0.00,'Dr',7),(11,'FIXED DEPOSIT',0.00,'Dr',7),(12,'SreeMantham(swathy)',0.00,'Dr',7),(13,'TA_TA sumo acc',0.00,'Cr',7);

/*Table structure for table `users` */

DROP TABLE IF EXISTS `users`;

CREATE TABLE `users` (
  `User_ID` int(11) NOT NULL auto_increment,
  `User_Name` varchar(30) default NULL,
  `Password` varchar(255) default NULL,
  PRIMARY KEY  (`User_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;

/*Data for the table `users` */

insert  into `users`(`User_ID`,`User_Name`,`Password`) values (1,'admin','21232f297a57a5a743894a0e4a801fc3');

/*Table structure for table `voucher_types` */

DROP TABLE IF EXISTS `voucher_types`;

CREATE TABLE `voucher_types` (
  `VoucherType_ID` int(11) NOT NULL auto_increment,
  `VoucherType_Name` varchar(25) default NULL,
  PRIMARY KEY  (`VoucherType_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1;

/*Data for the table `voucher_types` */

insert  into `voucher_types`(`VoucherType_ID`,`VoucherType_Name`) values (1,'PAYMENT'),(2,'RECEIPT'),(3,'CONTRA');

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
) ENGINE=InnoDB AUTO_INCREMENT=65 DEFAULT CHARSET=latin1;

/*Data for the table `vouchers` */

insert  into `vouchers`(`Voucher_ID`,`Voucher_Date`,`Voucher_Type`,`Voucher_No`,`From_Ledger`,`To_Ledger`,`Amount`,`Narration`) values (1,'2009-11-21 00:00:00',2,1,4,6,1500.00,'VSSR'),(2,'2009-11-21 00:00:00',2,2,5,6,13220.00,'14'),(3,'2009-11-21 00:00:00',2,3,1,6,1200.00,'HUSSENSAB'),(4,'2009-11-21 00:00:00',2,4,5,6,7940.00,'15'),(5,'2009-11-21 00:00:00',2,5,1,6,315.00,'gl'),(6,'2009-11-21 00:00:00',2,6,5,6,3680.00,'17'),(7,'2009-11-21 00:00:00',2,7,1,6,182.00,'gl'),(8,'2009-11-21 00:00:00',2,8,5,6,1480.00,'13'),(9,'2009-11-21 00:00:00',2,9,5,6,3960.00,'14'),(10,'2009-11-21 00:00:00',2,10,5,6,8220.00,'16'),(11,'2009-11-21 00:00:00',2,11,5,6,1480.00,'13'),(12,'2009-11-21 00:00:00',2,12,5,6,1875.00,'1'),(13,'2009-11-21 00:00:00',2,13,5,6,1940.00,'12'),(14,'2009-11-21 00:00:00',1,1,6,1,66.00,'gl'),(15,'2009-11-21 00:00:00',1,2,6,1,36.00,'gl'),(16,'2009-11-21 00:00:00',1,3,6,7,12000.00,'bank'),(17,'2009-11-21 00:00:00',1,4,6,7,7000.00,'p.c.p.bank'),(18,'2009-11-21 00:00:00',1,5,6,7,5500.00,'p.s.k.bank'),(19,'2009-11-21 00:00:00',1,6,6,7,2500.00,'usha bank'),(20,'2009-11-21 00:00:00',2,14,8,6,27000.00,'bank paid'),(21,'2009-11-21 00:00:00',2,15,5,6,3640.00,'27'),(22,'2009-11-21 00:00:00',2,16,5,6,3680.00,'17'),(23,'2009-11-21 00:00:00',2,17,5,6,1830.00,'18'),(24,'2009-11-21 00:00:00',2,18,9,6,470.00,'Mukbal upto30/11/09'),(25,'2009-11-21 00:00:00',1,7,6,2,105.00,'sunil tiff=etc'),(26,'2009-11-21 00:00:00',2,19,4,6,15000.00,'kapali babu'),(27,'2009-11-21 00:00:00',2,20,9,6,35000.00,'b.narayanappa'),(28,'2009-11-21 00:00:00',2,21,5,6,18560.00,'20'),(29,'2009-11-21 00:00:00',2,22,5,6,3500.00,'6'),(30,'2009-11-21 00:00:00',2,23,5,6,1480.00,'13'),(31,'2009-11-21 00:00:00',1,8,6,2,900.00,'ganesh'),(32,'2009-11-21 00:00:00',1,9,6,2,9700.00,'silver boul'),(33,'2009-11-21 00:00:00',1,10,6,2,590.00,'blr+light'),(34,'2009-11-21 00:00:00',2,24,5,6,9850.00,'7'),(35,'2009-11-21 00:00:00',1,11,6,10,600.00,'p.o.m'),(64,'2009-11-22 00:00:00',2,25,5,6,9505.00,'5');

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
