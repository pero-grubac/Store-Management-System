-- MySQL dump 10.13  Distrib 8.0.34, for Win64 (x86_64)
--
-- Host: localhost    Database: prodavnica
-- ------------------------------------------------------
-- Server version	8.0.34

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `dobavljac`
--

DROP TABLE IF EXISTS `dobavljac`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `dobavljac` (
  `idDobavljac` int NOT NULL AUTO_INCREMENT,
  `Ime` varchar(45) NOT NULL,
  `Email` varchar(45) NOT NULL,
  PRIMARY KEY (`idDobavljac`),
  UNIQUE KEY `Ime_UNIQUE` (`Ime`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `dobavljac`
--

LOCK TABLES `dobavljac` WRITE;
/*!40000 ALTER TABLE `dobavljac` DISABLE KEYS */;
INSERT INTO `dobavljac` VALUES (1,'DobavljačiSrbija','dobavljacisrbija@gmail.com'),(2,'DobavljačiHercegovina','hercegovci@gmail.com'),(3,'DobavljačiCrnaGora','crnogorci@gmail.com'),(4,'DobavljačiBiH','dobavljacbih@gmail.com'),(5,'DobavljačiEvropa','dobavljaceu@gmail.com'),(7,'DobavljaciRusija','ussr@gmail.com');
/*!40000 ALTER TABLE `dobavljac` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `jezik`
--

DROP TABLE IF EXISTS `jezik`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `jezik` (
  `idJezik` int NOT NULL AUTO_INCREMENT,
  `Naziv` varchar(45) NOT NULL,
  PRIMARY KEY (`idJezik`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `jezik`
--

LOCK TABLES `jezik` WRITE;
/*!40000 ALTER TABLE `jezik` DISABLE KEYS */;
INSERT INTO `jezik` VALUES (1,'Srpski(Latinica)'),(2,'English'),(3,'Српски(Ћирилица)');
/*!40000 ALTER TABLE `jezik` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `kategorija`
--

DROP TABLE IF EXISTS `kategorija`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `kategorija` (
  `idKategorija` int NOT NULL AUTO_INCREMENT,
  `Naziv` varchar(45) NOT NULL,
  PRIMARY KEY (`idKategorija`),
  UNIQUE KEY `Naziv_UNIQUE` (`Naziv`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `kategorija`
--

LOCK TABLES `kategorija` WRITE;
/*!40000 ALTER TABLE `kategorija` DISABLE KEYS */;
INSERT INTO `kategorija` VALUES (3,'Čokolada'),(1,'Meso'),(2,'Mlijeko');
/*!40000 ALTER TABLE `kategorija` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `korisnik`
--

DROP TABLE IF EXISTS `korisnik`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `korisnik` (
  `idKorisnik` int NOT NULL AUTO_INCREMENT,
  `Ime` varchar(45) NOT NULL,
  `Prezime` varchar(45) NOT NULL,
  `KorisnickoIme` varchar(45) NOT NULL,
  `Lozinka` varchar(255) NOT NULL,
  `idJezik` int NOT NULL,
  `BrojTelefona` varchar(45) NOT NULL,
  `Email` varchar(45) NOT NULL,
  `isAdmin` tinyint NOT NULL,
  `idTema` int NOT NULL,
  PRIMARY KEY (`idKorisnik`),
  UNIQUE KEY `KorisnickoIme_UNIQUE` (`KorisnickoIme`),
  KEY `fk_Korisnik_Jezik1_idx` (`idJezik`),
  KEY `fk_Korisnik_Tema1_idx` (`idTema`),
  CONSTRAINT `fk_Korisnik_Jezik1` FOREIGN KEY (`idJezik`) REFERENCES `jezik` (`idJezik`),
  CONSTRAINT `fk_Korisnik_Tema1` FOREIGN KEY (`idTema`) REFERENCES `tema` (`idTema`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `korisnik`
--

LOCK TABLES `korisnik` WRITE;
/*!40000 ALTER TABLE `korisnik` DISABLE KEYS */;
INSERT INTO `korisnik` VALUES (1,'admin','adminovic','admin','$2a$11$.CE1.tVQlWfPlrK/pgWRBudp5S7gI2m.ActWIvGF2AJMGAx14dAm.',1,'1','admin@gmail.com',1,1),(3,'Mihajlo','Mihajlovic','miha','$2a$11$sORgq4TwhouPKQYCzxMxhO4o0e9Pk2y69HTOR6Hz0Kuohf/QSNFlS',2,'232323','miha@gmail.com',0,3),(4,'Vesna','Proljeće','vesna','$2a$11$7zIgKX.IapIz/rkcKcIFCehJP.fCLktiOaQ9hj0sixhGOKSmpacPC',2,'3434343','vesna@gmail.com',0,4),(5,'Pero','Perović','pero','$2a$11$nn.P3uP8wF3BKslvhNLEhORNSr3H4CGc6xUihMjHGAJRm/quE5Oxy',1,'4444','pero@gmail.com',0,5);
/*!40000 ALTER TABLE `korisnik` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `nabavka`
--

DROP TABLE IF EXISTS `nabavka`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `nabavka` (
  `idNabavka` int NOT NULL AUTO_INCREMENT,
  `idDobavljač` int NOT NULL,
  `DatumKupovine` datetime NOT NULL,
  PRIMARY KEY (`idNabavka`,`idDobavljač`),
  KEY `fk_Dobavljač_has_PROIZVOD_Dobavljač1_idx` (`idDobavljač`),
  CONSTRAINT `fk_Dobavljač_has_PROIZVOD_Dobavljač1` FOREIGN KEY (`idDobavljač`) REFERENCES `dobavljac` (`idDobavljac`)
) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `nabavka`
--

LOCK TABLES `nabavka` WRITE;
/*!40000 ALTER TABLE `nabavka` DISABLE KEYS */;
INSERT INTO `nabavka` VALUES (1,2,'2023-12-03 00:00:00'),(2,2,'2023-12-03 00:00:00'),(3,1,'2023-12-03 00:00:00'),(4,1,'2023-12-03 00:00:00'),(5,1,'2023-12-03 00:00:00'),(6,1,'2023-12-03 00:00:00'),(7,1,'2023-12-03 00:00:00'),(8,2,'2023-12-03 00:00:00'),(9,4,'2023-12-03 00:00:00'),(11,2,'2023-12-03 00:00:00'),(12,1,'2023-12-03 00:00:00'),(13,1,'2023-12-04 00:00:00'),(14,4,'2023-12-04 00:00:00'),(15,2,'2023-12-04 00:00:00'),(16,4,'2023-12-04 01:09:27');
/*!40000 ALTER TABLE `nabavka` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `popust`
--

DROP TABLE IF EXISTS `popust`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `popust` (
  `idPopust` int NOT NULL AUTO_INCREMENT,
  `Popust` int NOT NULL,
  `Start` date NOT NULL,
  `End` date NOT NULL,
  PRIMARY KEY (`idPopust`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `popust`
--

LOCK TABLES `popust` WRITE;
/*!40000 ALTER TABLE `popust` DISABLE KEYS */;
/*!40000 ALTER TABLE `popust` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `proizvod`
--

DROP TABLE IF EXISTS `proizvod`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `proizvod` (
  `idProizvod` int NOT NULL AUTO_INCREMENT,
  `Naziv` varchar(45) NOT NULL,
  `Cijena` decimal(10,2) NOT NULL,
  `Zalihe` decimal(10,2) NOT NULL,
  `DatumIsteka` date NOT NULL,
  `BarKod` varchar(45) NOT NULL,
  `idProizvodjac` int NOT NULL,
  `Opis` varchar(255) DEFAULT NULL,
  `idKategorija` int NOT NULL,
  PRIMARY KEY (`idProizvod`),
  UNIQUE KEY `BarKod_UNIQUE` (`BarKod`),
  KEY `fk_PROIZVOD_PROIZVODJAC1_idx` (`idProizvodjac`),
  KEY `fk_PROIZVOD_KATEGORIJA_idx` (`idKategorija`),
  CONSTRAINT `fk_PROIZVOD_KATEGORIJA` FOREIGN KEY (`idKategorija`) REFERENCES `kategorija` (`idKategorija`),
  CONSTRAINT `fk_PROIZVOD_PROIZVODJAC1` FOREIGN KEY (`idProizvodjac`) REFERENCES `proizvodjac` (`idProizvodjac`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `proizvod`
--

LOCK TABLES `proizvod` WRITE;
/*!40000 ALTER TABLE `proizvod` DISABLE KEYS */;
INSERT INTO `proizvod` VALUES (1,'Pršut',50.00,53.00,'2026-06-21','1111',1,'Svinjski pršut',1),(2,'Mlijeko 1l',2.00,52.00,'2026-06-21','1112',2,'Mlijeko',2),(6,'Šunka',9.88,0.00,'2024-11-24','1113',1,'Pica šunka',1),(7,'Slanina',15.00,100.00,'2024-11-29','1114',1,'Svinjska slanina',1),(8,'Čvarci',10.00,5.00,'2025-11-29','1115',1,'čvarci',1),(9,'Sir',8.00,200.00,'2025-11-29','1116',2,'sir',1),(10,'Milka oreo',4.00,100.00,'2023-11-29','1117',3,'milka oreo',1),(11,'Cokolada sa keksom',2.00,0.00,'2025-11-29','1118',4,'čokolada sa keksom',1);
/*!40000 ALTER TABLE `proizvod` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `proizvod_has_popust`
--

DROP TABLE IF EXISTS `proizvod_has_popust`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `proizvod_has_popust` (
  `idProizvod` int NOT NULL,
  `idPopust` int NOT NULL,
  PRIMARY KEY (`idProizvod`,`idPopust`),
  KEY `fk_PROIZVOD_has_Popust_Popust1_idx` (`idPopust`),
  KEY `fk_PROIZVOD_has_Popust_PROIZVOD1_idx` (`idProizvod`),
  CONSTRAINT `fk_PROIZVOD_has_Popust_Popust1` FOREIGN KEY (`idPopust`) REFERENCES `popust` (`idPopust`),
  CONSTRAINT `fk_PROIZVOD_has_Popust_PROIZVOD1` FOREIGN KEY (`idProizvod`) REFERENCES `proizvod` (`idProizvod`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `proizvod_has_popust`
--

LOCK TABLES `proizvod_has_popust` WRITE;
/*!40000 ALTER TABLE `proizvod_has_popust` DISABLE KEYS */;
/*!40000 ALTER TABLE `proizvod_has_popust` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `proizvodjac`
--

DROP TABLE IF EXISTS `proizvodjac`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `proizvodjac` (
  `idProizvodjac` int NOT NULL AUTO_INCREMENT,
  `Ime` varchar(45) NOT NULL,
  `Email` varchar(45) NOT NULL,
  PRIMARY KEY (`idProizvodjac`),
  UNIQUE KEY `Ime_UNIQUE` (`Ime`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `proizvodjac`
--

LOCK TABLES `proizvodjac` WRITE;
/*!40000 ALTER TABLE `proizvodjac` DISABLE KEYS */;
INSERT INTO `proizvodjac` VALUES (1,'Mesnica Tarana','tarana@gmail.com'),(2,'Pađani','pađani@gmai.com'),(3,'Milka','milka@gmail.com'),(4,'Štark','stark@gmail.com'),(5,'Vitinka','vitinka@gmail.com');
/*!40000 ALTER TABLE `proizvodjac` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `racun`
--

DROP TABLE IF EXISTS `racun`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `racun` (
  `idRacun` int NOT NULL AUTO_INCREMENT,
  `DatumKupovine` datetime NOT NULL,
  PRIMARY KEY (`idRacun`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `racun`
--

LOCK TABLES `racun` WRITE;
/*!40000 ALTER TABLE `racun` DISABLE KEYS */;
INSERT INTO `racun` VALUES (1,'2023-12-02 00:00:00'),(2,'2023-12-03 21:52:10');
/*!40000 ALTER TABLE `racun` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `stavka`
--

DROP TABLE IF EXISTS `stavka`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `stavka` (
  `idRacun` int NOT NULL,
  `idProizvod` int NOT NULL,
  `Kolicina` decimal(10,2) NOT NULL,
  `Cijena` decimal(10,2) NOT NULL,
  PRIMARY KEY (`idRacun`,`idProizvod`),
  KEY `fk_Racun_has_PROIZVOD_PROIZVOD1_idx` (`idProizvod`),
  KEY `fk_Racun_has_PROIZVOD_Racun1_idx` (`idRacun`),
  CONSTRAINT `fk_Racun_has_PROIZVOD_PROIZVOD1` FOREIGN KEY (`idProizvod`) REFERENCES `proizvod` (`idProizvod`),
  CONSTRAINT `fk_Racun_has_PROIZVOD_Racun1` FOREIGN KEY (`idRacun`) REFERENCES `racun` (`idRacun`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `stavka`
--

LOCK TABLES `stavka` WRITE;
/*!40000 ALTER TABLE `stavka` DISABLE KEYS */;
INSERT INTO `stavka` VALUES (1,1,1.00,50.00),(2,1,1.00,50.00);
/*!40000 ALTER TABLE `stavka` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `stavka_nabvke`
--

DROP TABLE IF EXISTS `stavka_nabvke`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `stavka_nabvke` (
  `idNabavka` int NOT NULL,
  `idDobavljač` int NOT NULL,
  `idProizvod` int NOT NULL,
  `Kolicina` decimal(10,2) NOT NULL,
  `Cijena` decimal(10,2) NOT NULL,
  PRIMARY KEY (`idNabavka`,`idDobavljač`,`idProizvod`),
  KEY `fk_Nabavka_has_PROIZVOD_PROIZVOD1_idx` (`idProizvod`),
  KEY `fk_Nabavka_has_PROIZVOD_Nabavka1_idx` (`idNabavka`,`idDobavljač`),
  CONSTRAINT `fk_Nabavka_has_PROIZVOD_Nabavka1` FOREIGN KEY (`idNabavka`, `idDobavljač`) REFERENCES `nabavka` (`idNabavka`, `idDobavljač`),
  CONSTRAINT `fk_Nabavka_has_PROIZVOD_PROIZVOD1` FOREIGN KEY (`idProizvod`) REFERENCES `proizvod` (`idProizvod`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `stavka_nabvke`
--

LOCK TABLES `stavka_nabvke` WRITE;
/*!40000 ALTER TABLE `stavka_nabvke` DISABLE KEYS */;
INSERT INTO `stavka_nabvke` VALUES (2,2,1,1.00,5.00),(7,1,7,100.00,5.00),(8,2,9,200.00,2.00),(9,4,10,100.00,2.00),(11,2,1,1.00,20.00),(12,1,2,1.00,20.00),(13,1,1,1.00,10.00),(13,1,2,1.00,1.00),(14,4,1,1.00,2.00),(15,2,1,1.00,1.00),(16,4,1,1.00,2.00);
/*!40000 ALTER TABLE `stavka_nabvke` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tema`
--

DROP TABLE IF EXISTS `tema`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tema` (
  `idTema` int NOT NULL AUTO_INCREMENT,
  `boja` varchar(45) NOT NULL,
  `font` varchar(45) NOT NULL,
  `veličina` varchar(45) NOT NULL,
  `fontStil` varchar(45) DEFAULT NULL,
  `strikeout` tinyint DEFAULT NULL,
  `underline` tinyint DEFAULT NULL,
  PRIMARY KEY (`idTema`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tema`
--

LOCK TABLES `tema` WRITE;
/*!40000 ALTER TABLE `tema` DISABLE KEYS */;
INSERT INTO `tema` VALUES (1,'255,0,128,128','Segoe UI','11','Regular',0,0),(2,'255,0,128,128','Segoe UI','11','Regular',0,0),(3,'255,0,128,192','Segoe UI','10','Regular',0,0),(4,'255,0,128,128','Segoe UI','11','Regular',0,0),(5,'255,0,128,128','Segoe UI','11','Regular',0,0);
/*!40000 ALTER TABLE `tema` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping routines for database 'prodavnica'
--
/*!50003 DROP PROCEDURE IF EXISTS `InsertNabavka` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `InsertNabavka`(
    IN idDobavljac_param INT,
    IN DatumKupovine_param DATETIME,
    IN stavke_list JSON
)
BEGIN
	DECLARE i INT DEFAULT 0;
    DECLARE num_items INT;
    DECLARE idProizvod_val INT;
    DECLARE Kolicina_val DECIMAL(10,2);
    DECLARE Cijena_val DECIMAL(10,2);
    DEClARE idNabavka INT;
    
    INSERT INTO nabavka (idDobavljač, DatumKupovine)
    VALUES (idDobavljac_param, DatumKupovine_param);

   
	SET idNabavka = LAST_INSERT_ID();
    SET num_items = JSON_LENGTH(stavke_list);

    WHILE i < num_items DO
        SET idProizvod_val = JSON_UNQUOTE(JSON_EXTRACT(stavke_list, CONCAT('$[', i, '].IdProduct')));
        SET Kolicina_val = JSON_UNQUOTE(JSON_EXTRACT(stavke_list, CONCAT('$[', i, '].Amount')));
        SET Cijena_val = JSON_UNQUOTE(JSON_EXTRACT(stavke_list, CONCAT('$[', i, '].Price')));

        INSERT INTO stavka_nabvke (idNabavka, idDobavljač, idProizvod, Kolicina, Cijena)
        VALUES (idNabavka, idDobavljac_param, idProizvod_val, Kolicina_val, Cijena_val);

		UPDATE proizvod
        SET Zalihe = Zalihe + Kolicina_val
        WHERE idProizvod = idProizvod_val;
        
        SET i = i + 1;
    END WHILE;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `InsertRacunWithStavka` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `InsertRacunWithStavka`(
    IN DatumKupovine_param DATETIME,
    IN stavke_list JSON
)
BEGIN
    DECLARE idRacun_param INT;
	DECLARE i INT DEFAULT 0;
    DECLARE num_items INT;
    DECLARE idProizvod_val INT;
    DECLARE Kolicina_val DECIMAL(10,2);
    DECLARE Cijena_val DECIMAL(10,2);
    
    INSERT INTO racun (DatumKupovine) VALUES (DatumKupovine_param);
    SET idRacun_param = LAST_INSERT_ID();

    SET num_items = JSON_LENGTH(stavke_list);

    WHILE i < num_items DO
        SET idProizvod_val = JSON_UNQUOTE(JSON_EXTRACT(stavke_list, CONCAT('$[', i, '].IdProduct')));
        SET Kolicina_val = JSON_UNQUOTE(JSON_EXTRACT(stavke_list, CONCAT('$[', i, '].Amount')));
        SET Cijena_val = JSON_UNQUOTE(JSON_EXTRACT(stavke_list, CONCAT('$[', i, '].Price')));

        INSERT INTO stavka (idRacun, idProizvod, Kolicina, Cijena)
        VALUES (idRacun_param, idProizvod_val, Kolicina_val, Cijena_val);

        UPDATE proizvod
        SET Zalihe = Zalihe - Kolicina_val
        WHERE idProizvod = idProizvod_val;

        SET i = i + 1;
    END WHILE;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-12-04 20:20:32
