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
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=latin1;

/*Data for the table `cities` */

insert  into `cities`(`City_ID`,`Village_Name`,`City_Name`,`State`,`Pincode`) values (1,'CHELUR','chlur','Karnataka','563124'),(2,'MUGIREDDYPALLI','venkatapur post','Karnataka','563124'),(3,'BANGLORE','banglore','Karnataka','560000'),(4,'pullagal','pullagal post ,bagepalli tq','Karnataka','563124'),(5,'p.t.m (at&post)','PTM .mandal, Chittur dist','Andra Pradesh','00000'),(6,'Bagepalli','Bagepalli tq','Karnataka','00000'),(7,'Yalahanka','Banglore','Karnataka','00000'),(8,'Kandukur','PTM .mandal, Chittur dist','Andra Pradesh','00000'),(9,'Chinthamani','Chinthamani tq ,Kolar dist','Karnataka','0000'),(10,'Ananthapur town','ananthapur dist','Andra Pradesh','0000'),(11,'Rascheruvu @&post','Bagepalli tq','Karnataka','563124');

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
) ENGINE=InnoDB AUTO_INCREMENT=63 DEFAULT CHARSET=latin1;

/*Data for the table `customers` */

insert  into `customers`(`Customer_id`,`Customer_name`,`Son_husband`,`Account_no`,`Res_address`,`Res_village`,`Res_phone`) values (1,'v.s.sathyanarayanarao','v.s.sheshagirirao','1','mugyreddipalli',2,'9480238041'),(2,'p.c.swathy','Ravikiran','2','k.r.puram',3,'9448536062'),(3,'P.Sunilkumar','P.R.Chalam','3','chelur',1,'08150280051'),(4,'LORD venkateshwara','c/o P.R.Chalam','4','Chelur',1,'280051'),(5,'Bramhana sanga','CHELUR','5','Chelur',1,'280294'),(6,'Kodandaramaswami temple','Chelur','6','Chelur',1,'0000'),(7,'S.B.Veena','S.P.Balaramaiah','7','Chelur',1,'000'),(8,'S.B.Sudharani','S.P.Balaramaiah','8','Chelur',1,'0000'),(9,'S.B.Padmaja','S.P.Balaramaiah','9','Chelur',1,'0000'),(10,'Gaddam Family','c/o P.R.Chalam','11','p.t.m',5,'0000'),(11,'Nagasaroja','B.L.V.Prasad','12','Anantapur',10,'0000'),(12,'J.Sreedar(kand)','000','13','Kandukur',8,'0000'),(13,'Nagamma','Istri venkataravana','14','Chelur',1,'000'),(14,'Anjanamma','Dobhi Ananda','15','Chelur',1,'000'),(15,'Narasamma','late Sangappa','16','pnp chelur',1,'000'),(16,'Subadramma','Late meda rathanamaiah','10','Chelur',1,'0000'),(17,'ARYA VYSYA MAHILA MANDALI','Vysya','17','Chelur',1,'0000'),(18,'Indiramma','s/o p.r.chalam','18','Bangalore',3,'0000'),(19,'P.R.Chalam','Pola ramaiah shetty','19','Chelur',1,'280051'),(20,'P.C.Prabha','p.r.Chalam','20','Chelur',1,'280051'),(21,'Subbarayappa','Yalagalapalli Narasimhappa','21','Buradagunta',1,'000'),(22,'P.Sunil kumar','p.r.Chalam','22','Chelur',1,'280051'),(23,'P.Usha sunil','p.sunil kumar','23','Chelur',1,'280051'),(24,'P.S.Nihar','p.sunil kumar','24','Chelur',1,'280051'),(25,'Nagaraj (krp)','c/o Ravikiran','25','k.r.puram',3,'000'),(26,'Ramalakshamma','V.S.Sheshagiri rao','26','mugireddipalli',2,'280156'),(27,'B.S.Banuprakash','Subbishetty','27','Chelur',1,'000'),(28,'Thapilamma(mg th)','c/o kunda yamanna','28','Chelur',1,'0000'),(29,'P.R.Subbannaswamy','c/oShobha','29','Chelur',1,'280094'),(30,'Ravikiran(prc)','Sathyanarayanaiah','30','k.r.puram',3,'0000'),(31,'Dr..Byreddy','father','31','Malleswaram',3,'000'),(32,'Dr..Rajareddy','father','32','Bangalore',3,'000'),(33,'Harish','father','33','Bangalore',3,'000'),(34,'A.Vijayakumar','Jayaramagupta','34','pathapalya',6,'000'),(35,'M.V.Reddappashetty','father','35','Girinagar',3,'000'),(36,'Venkatamma(aaya)','Husband','36','pnp chelur',1,'000'),(37,'L.Kiran','D.Lakshminarayana shetty','37','yalahanka',7,'000'),(38,'B.A.Radha','B.AswathiahShetty','38','Pullagal',4,'000'),(39,'V.S.Hanumesh','V.S.Sheshagiri rao','39','mugireddipalli',2,'000'),(40,'D.Amaranath','D.Rathnaiah shetty','40','yalahanka',7,'0000'),(41,'H.SathyanarayanaSharma','father','41','Rascheruvu',11,'000'),(42,'G.Chandra shekar(ptm)','father','42','p.t.m',3,'000'),(43,'K.G.Venkataravana','father','43','Chelur',1,'0000'),(44,'Vanishree(iyer)','G.N.Ravishankar','44','Bangalore',3,'0000'),(45,'M.P.Manojkumar','father','45','Girinagar',3,'0000'),(46,'S.Sudarshan sharma','SathyanarayanaSharma','46','Bangalore',3,'0000'),(47,'Kavitha(iyer)','Venkatesh iyer','47','Bangalore',3,'000'),(48,'Venkataravanappa(j.e)','father','48','Bagepalli',6,'0000'),(49,'N.G.Chandrashekar','N.S.Guruprasad','49','Bangalore',3,'0000'),(50,'Kapali babu','Reddapashetty','50','Girinagar',3,'0000'),(51,'Subbarao','G.SundaraKrishna','51','Venkatapur',1,'0000'),(52,'Babji(bagepalli)','Appiswamy','52','Bagepalli',6,'0000'),(53,'D.Lakshminarayana shetty','father','53','yalahanka',3,'0000'),(54,'Babureddy(bagepalli)','father','54','Bagepalli',6,'0000'),(55,'Dr.Ramesh','father','55','Bagepalli',6,'0000'),(56,'Sreenivasa(blr)','father','56','Bangalore',3,'0000'),(57,'B.S.Nagaraj','V.Sathyanarayana rao','57','Chelur',1,'0000'),(58,'A.V.Sreenath','A.R.Vishwanath(late)','58','k.r.puram',3,'0000'),(59,'K.Sathyanarayanashetty','father','59','Chinthamani',9,'000'),(60,'S.P.Balaramaiah','Padmanabaiah','60','Chelur',1,'0000'),(61,'D.V.Prasad','D.Venkateshiah','61','Chelur',1,'0000'),(62,'A.R.Pandurangaiah','A.Rathnam shetty','62','Bangalore',3,'000');

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

/*Data for the table `fixeddeposit` */

insert  into `fixeddeposit`(`FD_No`,`Customer_ID`,`Start_Date`,`Nominee_Name`,`Relationship`,`Amount`,`Rate`,`Closed`) values ('FD001',4,'2009-08-01 00:00:00','p.r.chalam','bakta',150000,18,'n'),('FD002',5,'2007-06-24 00:00:00','President','president',30000,10,'n'),('FD003',6,'2009-12-31 00:00:00','Vysyas','genral',5390,10,'n'),('FD004',7,'2008-04-01 00:00:00','s.p,B','father',45000,10,'n'),('FD005',8,'2008-04-01 00:00:00','s.p,B','father',45000,10,'n'),('FD006',9,'2008-04-01 00:00:00','s.p,B','father',45000,10,'n'),('FD007',10,'2009-03-31 00:00:00','P.c.prabha','doughter',35500,10,'n'),('FD008',11,'2008-06-20 00:00:00','B,L.V.Prasad','Husband',15500,10,'n'),('FD009',12,'2009-05-30 00:00:00','wife','wife',100000,10,'n'),('FD010',12,'2009-05-30 00:00:00','wife','wife',120000,12,'n'),('FD011',13,'2009-09-05 00:00:00','istri venkataravana','Husband',6000,10,'n'),('FD012',14,'2005-10-05 00:00:00','Dhobi Ananda','Husband',5000,10,'n'),('FD013',15,'2009-11-10 00:00:00','son','son',15000,10,'n'),('FD014',16,'2007-02-23 00:00:00','Bramhanasanga','santharpana',5000,10,'n'),('FD015',17,'2007-01-01 00:00:00','Vysyas','vysyas',20000,10,'n'),('FD016',18,'2009-03-31 00:00:00','Manoj','son',90000,10,'n'),('FD017',19,'2009-12-31 00:00:00','P.c.prabha','wife',2.3e+006,18,'n'),('FD018',20,'2009-12-31 00:00:00','p.r.chalam','Husband',900000,18,'n'),('FD019',21,'2009-05-27 00:00:00','wife','wife',21000,10,'n'),('FD020',22,'2009-09-30 00:00:00','p.ushasunil','Husband',700000,18,'n'),('FD021',23,'2009-12-31 00:00:00','p.sunil kumar','Husband',100000,18,'n'),('FD022',24,'2009-12-31 00:00:00','p.ushasunil','mother',20000,18,'n'),('FD023',25,'2009-05-13 00:00:00','wife','wife',11500,10,'n'),('FD024',26,'2009-12-01 00:00:00','V.S.Raghavendran','son',320000,10,'n'),('FD025',2,'2009-12-01 00:00:00','Ravikiran','Husband',225000,18,'n'),('FD026',2,'2009-09-25 00:00:00','Sathyanarayanaiah','Father in law',375000,18,'n'),('FD027',27,'2009-12-01 00:00:00','wife','wife',175000,12,'n'),('FD028',28,'2009-07-20 00:00:00','Peddabalaji &Chinnabalaji','son',8500,10,'n'),('FD029',29,'2009-12-15 00:00:00','Shobha','doughter',450000,10,'n'),('FD030',30,'2009-11-22 00:00:00','p.c.swathy','wife',22190,0,'n'),('FD031',31,'2009-08-01 00:00:00','wife','wife',237500,10,'n'),('FD032',19,'2009-03-31 00:00:00','P.c.prabha','wife',360000,12,'n'),('FD033',58,'2009-03-31 00:00:00','wife','wife',360000,12,'n'),('FD034',3,'2009-03-31 00:00:00','p.ushasunil','wife',180000,12,'n'),('FD035',59,'2009-03-31 00:00:00','son','son',180000,12,'n'),('FD036',60,'2009-03-31 00:00:00','S.B.Jayapradamma','wife',234000,12,'n'),('FD037',61,'2009-03-31 00:00:00','Krishnaveni','wife',180000,12,'n'),('FD038',34,'2009-03-31 00:00:00','wife','wife',126000,12,'n'),('FD039',62,'2009-10-05 00:00:00','wife','wife',500000,12,'n');

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
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;

/*Data for the table `fixedinterest` */

insert  into `fixedinterest`(`Record_ID`,`FD_No`,`Paid_Date`,`Interest_Amount`,`Voucher_No`,`Interest_Upto`) values (1,'FD003','2010-01-31 00:00:00',127,'001','2010-01-31 00:00:00');

/*Table structure for table `fixedtrans` */

DROP TABLE IF EXISTS `fixedtrans`;

CREATE TABLE `fixedtrans` (
  `Record_ID` int(11) NOT NULL auto_increment,
  `FD_No` varchar(10) default NULL,
  `Paid_Date` datetime default NULL,
  `Amount` float default NULL,
  `Receipt_No` varchar(10) default NULL,
  PRIMARY KEY  (`Record_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;

/*Data for the table `fixedtrans` */

insert  into `fixedtrans`(`Record_ID`,`FD_No`,`Paid_Date`,`Amount`,`Receipt_No`) values (1,'FD007','2009-05-30 00:00:00',5500,'1');

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
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=latin1;

/*Data for the table `hundiinterest` */

insert  into `hundiinterest`(`Record_id`,`Hl_loanno`,`Interest_amount`,`Receipt_no`,`Paid_date`,`Interest_upto`) values (1,'HL003',24,'1','2010-01-01 00:00:00','2010-01-01 00:00:00'),(2,'HL010',152,'002','2010-01-31 00:00:00','2010-01-31 00:00:00'),(3,'HL011',70000,'03','2009-10-03 00:00:00','2009-02-14 00:00:00'),(4,'HL014',9000,'04','2009-10-06 00:00:00','2010-01-07 00:00:00');

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

insert  into `hundiloans`(`Hl_loanno`,`Customer_id`,`Co_obligent`,`Coobligent_address`,`Loan_amount`,`Loan_date`,`Closed`,`Rate`,`Pay_mode`,`Cheque_no`,`Bank_id`) values ('HL001',1,'s.umadevi','bnglr',80000,'2009-06-30 00:00:00','n',0,1,'',0),('HL002',32,'p.r.chalam','chelur',249278,'2009-02-06 00:00:00','n',18,1,'',0),('HL003',33,'p.sunil kumar','chelur',18000,'2009-03-09 00:00:00','n',0,1,'',0),('HL004',34,'p.r.chalam','chelur',25000,'2009-03-31 00:00:00','n',12,1,'',0),('HL005',35,'Self','self',100000,'2008-12-01 00:00:00','n',12,1,'',0),('HL006',36,'Self','self',7200,'2009-12-04 00:00:00','n',0,1,'',0),('HL007',37,'Self','self',13500,'2009-10-12 00:00:00','n',0,1,'',0),('HL008',38,'B,A,Jagannatham','chelur',100000,'2009-12-31 00:00:00','n',18,1,'',0),('HL009',39,'V.s.s.rao','chelur',130000,'2009-03-31 00:00:00','n',15,1,'',0),('HL010',40,'Self','self',200000,'2009-10-31 00:00:00','n',18,1,'',0),('HL011',41,'Self','self',500000,'2008-05-14 00:00:00','n',18,1,'',0),('HL012',42,'Self','self',10000,'2008-01-30 00:00:00','n',0,1,'',0),('HL013',44,'Self','self',100000,'2009-12-20 00:00:00','n',18,1,'',0),('HL014',44,'Self','self',200000,'2009-10-07 00:00:00','n',18,1,'',0),('HL015',45,'Self','self',200000,'2009-10-31 00:00:00','n',18,1,'',0),('HL016',46,'Self','self',200000,'2009-12-19 00:00:00','n',18,1,'',0),('HL017',47,'Self','self',300000,'2009-12-20 00:00:00','n',18,1,'',0),('HL018',48,'Self','self',500000,'2009-11-30 00:00:00','n',24,1,'',0),('HL019',49,'V.s.s.rao','chelur',50000,'2009-12-10 00:00:00','n',24,1,'',0),('HL020',50,'Self','self',900000,'2009-12-01 00:00:00','n',18,1,'',0),('HL021',50,'Self','self',400000,'2009-10-06 00:00:00','n',12,1,'',0),('HL022',51,'Self','self',50000,'2009-07-18 00:00:00','n',0,1,'',0),('HL023',52,'Appiswamy','bagepalli',400000,'2009-10-08 00:00:00','n',18,1,'',0),('HL024',53,'Self','self',300000,'2009-10-05 00:00:00','n',18,1,'',0),('HL025',54,'Self','self',20000,'2009-11-18 00:00:00','n',24,1,'',0),('HL026',55,'Self','self',200000,'2009-12-23 00:00:00','n',18,1,'',0),('HL027',43,'p.r.chalam','chelur',40000,'2009-12-31 00:00:00','n',24,1,'',0);

/*Table structure for table `hunditrans` */

DROP TABLE IF EXISTS `hunditrans`;

CREATE TABLE `hunditrans` (
  `Record_id` int(11) NOT NULL auto_increment,
  `Hl_loanno` varchar(10) default NULL,
  `Paid_date` datetime default NULL,
  `Receipt_no` varchar(10) default NULL,
  `Amount` float default NULL,
  PRIMARY KEY  (`Record_id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1;

/*Data for the table `hunditrans` */

insert  into `hunditrans`(`Record_id`,`Hl_loanno`,`Paid_date`,`Receipt_no`,`Amount`) values (1,'HL001','2009-12-28 00:00:00','1',10000),(2,'HL007','2009-11-24 00:00:00','1',5000),(3,'HL003','2010-01-01 00:00:00','123',5000);

/*Table structure for table `ledgers` */

DROP TABLE IF EXISTS `ledgers`;

CREATE TABLE `ledgers` (
  `Ledger_ID` int(11) NOT NULL auto_increment,
  `Ledger_Name` varchar(60) default NULL,
  `Opening_Balance` double(10,2) default NULL,
  `Balance_Type` varchar(3) default NULL,
  `Group_ID` int(11) default NULL,
  PRIMARY KEY  (`Ledger_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=38 DEFAULT CHARSET=latin1;

/*Data for the table `ledgers` */

insert  into `ledgers`(`Ledger_ID`,`Ledger_Name`,`Opening_Balance`,`Balance_Type`,`Group_ID`) values (1,'Interest',0.00,'Cr',24),(2,'PATA(Exp)',0.00,'Dr',27),(3,'SALARIES',0.00,'Dr',27),(4,'A=T=K=T',0.00,'Cr',7),(5,'C-H-I-T-S',0.00,'Cr',7),(6,'Cash',0.00,'Cr',15),(7,'P.R.C.(RENTaccount)',0.00,'Cr',7),(8,'personal acc(dummy)',0.00,'Cr',7),(9,'H.L,INTREST',0.00,'Cr',24),(10,'P.R.CHLAM(personal)',0.00,'Dr',7),(12,'SreeMantham(swathy)',0.00,'Dr',7),(13,'TA_TA sumo acc',0.00,'Cr',7),(14,'Interest Paid',0.00,'Cr',24),(15,'God',0.00,'Cr',7),(16,'Intrest payable',0.00,'Cr',7),(17,'Intrest receivable',0.00,'Dr',7),(18,'RENTadvance',0.00,'Dr',7),(19,'Kapali =1st day',0.00,'Dr',12),(20,'Kapali =6 th day',0.00,'Dr',12),(21,'Kapali =15 th day',0.00,'Dr',12),(22,'Kapali =10 th day',0.00,'Cr',12),(23,'CHIT=profits',0.00,'Cr',24),(24,'Bagepalli chits 6 th day',0.00,'Dr',12),(25,'KNP chit',0.00,'Dr',12),(26,'PFC chit 9 th day',0.00,'Dr',12),(27,'PFC chit 29 th day',0.00,'Dr',12),(28,'PFC chit 22 th day',0.00,'Dr',12),(29,'Partners c/a @ ac/=',0.00,'Dr',7),(30,'Capital @ ac/=',0.00,'Dr',7),(31,'Deposits @ ac/=',0.00,'Dr',7),(32,'RESERVE fund',0.00,'Cr',7),(33,'VEHICLE @ac/=',0.00,'Cr',7),(34,'CHIT=commissions',0.00,'Cr',13),(35,'CHIT=adjustments',0.00,'Cr',7),(36,'CHIT=devidends',0.00,'Cr',7),(37,'CHIT=discounts',0.00,'Dr',7);

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
) ENGINE=InnoDB AUTO_INCREMENT=86 DEFAULT CHARSET=latin1;

/*Data for the table `vouchers` */

insert  into `vouchers`(`Voucher_ID`,`Voucher_Date`,`Voucher_Type`,`Voucher_No`,`From_Ledger`,`To_Ledger`,`Amount`,`Narration`) values (1,'2009-11-21 00:00:00',2,1,4,6,1500.00,'VSSR'),(2,'2009-11-21 00:00:00',2,2,5,6,13220.00,'14'),(3,'2009-11-21 00:00:00',2,3,1,6,1200.00,'HUSSENSAB'),(4,'2009-11-21 00:00:00',2,4,5,6,7940.00,'15'),(5,'2009-11-21 00:00:00',2,5,1,6,315.00,'gl'),(6,'2009-11-21 00:00:00',2,6,5,6,3680.00,'17'),(7,'2009-11-21 00:00:00',2,7,1,6,182.00,'gl'),(8,'2009-11-21 00:00:00',2,8,5,6,1480.00,'13'),(9,'2009-11-21 00:00:00',2,9,5,6,3960.00,'14'),(10,'2009-11-21 00:00:00',2,10,5,6,8220.00,'16'),(11,'2009-11-21 00:00:00',2,11,5,6,1480.00,'13'),(12,'2009-11-21 00:00:00',2,12,5,6,1875.00,'1'),(13,'2009-11-21 00:00:00',2,13,5,6,1940.00,'12'),(14,'2009-11-21 00:00:00',1,1,6,1,66.00,'gl'),(15,'2009-11-21 00:00:00',1,2,6,1,36.00,'gl'),(16,'2009-11-21 00:00:00',1,3,6,7,12000.00,'bank'),(17,'2009-11-21 00:00:00',1,4,6,7,7000.00,'p.c.p.bank'),(18,'2009-11-21 00:00:00',1,5,6,7,5500.00,'p.s.k.bank'),(19,'2009-11-21 00:00:00',1,6,6,7,2500.00,'usha bank'),(20,'2009-11-21 00:00:00',2,14,8,6,27000.00,'bank paid'),(21,'2009-11-21 00:00:00',2,15,5,6,3640.00,'27'),(22,'2009-11-21 00:00:00',2,16,5,6,3680.00,'17'),(23,'2009-11-21 00:00:00',2,17,5,6,1830.00,'18'),(24,'2009-11-21 00:00:00',2,18,9,6,470.00,'Mukbal upto30/11/09'),(25,'2009-11-21 00:00:00',1,7,6,2,105.00,'sunil tiff=etc'),(26,'2009-11-21 00:00:00',2,19,4,6,15000.00,'kapali babu'),(27,'2009-11-21 00:00:00',2,20,9,6,35000.00,'b.narayanappa'),(28,'2009-11-21 00:00:00',2,21,5,6,18560.00,'20'),(29,'2009-11-21 00:00:00',2,22,5,6,3500.00,'6'),(30,'2009-11-21 00:00:00',2,23,5,6,1480.00,'13'),(31,'2009-11-21 00:00:00',1,8,6,2,900.00,'ganesh'),(32,'2009-11-21 00:00:00',1,9,6,2,9700.00,'silver boul'),(33,'2009-11-21 00:00:00',1,10,6,2,590.00,'blr+light'),(34,'2009-11-21 00:00:00',2,24,5,6,9850.00,'7'),(35,'2009-11-21 00:00:00',1,11,6,10,600.00,'p.o.m'),(64,'2009-11-22 00:00:00',2,25,5,6,9505.00,'5'),(65,'2009-11-23 00:00:00',2,26,4,6,1000.00,'sample'),(66,'2009-12-31 00:00:00',2,1,15,6,101.00,'31/12/09'),(67,'2009-12-31 00:00:00',2,2,16,6,78852.00,'31/12/09'),(68,'2009-12-31 00:00:00',1,1,6,17,82180.00,'31/12/09'),(69,'2009-12-31 00:00:00',1,2,6,18,50000.00,'31/12/09'),(70,'2009-12-31 00:00:00',1,3,6,19,196632.00,'31/12/09(upto 18)'),(71,'2009-12-31 00:00:00',2,3,23,6,72736.00,'kapali 6thday old'),(72,'2009-12-31 00:00:00',1,4,6,20,87512.00,'31/12/09(upto 7)'),(73,'2009-12-31 00:00:00',1,5,6,21,88128.00,'31/12/09(upto 7)'),(74,'2009-12-31 00:00:00',2,4,22,6,42980.00,'31/12/09(upto 22)'),(75,'2009-12-31 00:00:00',1,6,6,24,224390.00,'31/12/09(upto 13)'),(76,'2009-12-31 00:00:00',2,5,23,6,18780.00,'BPchit 15th old'),(77,'2009-12-31 00:00:00',1,7,6,25,20236.00,'31/12/09(upto 5)'),(78,'2009-12-31 00:00:00',1,8,6,26,19098.00,'31/12/09(upto 22)'),(79,'2009-12-31 00:00:00',2,6,23,6,1891.00,'pfc chit 13th old'),(80,'2009-12-31 00:00:00',1,9,6,27,16176.00,'31/12/09(upto 19)'),(81,'2009-12-31 00:00:00',1,10,6,28,16750.00,'31/12/09(upto 10)'),(82,'2009-12-31 00:00:00',2,7,32,6,56425.00,'31/12/09'),(83,'2009-12-31 00:00:00',1,11,6,29,188775.00,'31/12/09'),(84,'2009-12-31 00:00:00',1,12,6,30,3129237.00,'31/12/09'),(85,'2009-12-31 00:00:00',1,13,6,31,4220000.00,'31/12/09');

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
