CREATE DATABASE  IF NOT EXISTS `prodavnica` /*!40100 DEFAULT CHARACTER SET utf8mb3 */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `prodavnica`;
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
  `idDobavljac` int NOT NULL,
  `Ime` varchar(45) NOT NULL,
  PRIMARY KEY (`idDobavljac`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `jezik`
--

DROP TABLE IF EXISTS `jezik`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `jezik` (
  `idJezik` int NOT NULL,
  `Naziv` varchar(45) NOT NULL,
  PRIMARY KEY (`idJezik`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `kategorija`
--

DROP TABLE IF EXISTS `kategorija`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `kategorija` (
  `Naziv` varchar(45) NOT NULL,
  `idKategorija` int NOT NULL,
  PRIMARY KEY (`idKategorija`),
  UNIQUE KEY `Naziv_UNIQUE` (`Naziv`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `korisnik`
--

DROP TABLE IF EXISTS `korisnik`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `korisnik` (
  `idKorisnik` int NOT NULL,
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
  KEY `fk_Korisnik_Jezik1_idx` (`idJezik`),
  KEY `fk_Korisnik_Tema1_idx` (`idTema`),
  CONSTRAINT `fk_Korisnik_Jezik1` FOREIGN KEY (`idJezik`) REFERENCES `jezik` (`idJezik`),
  CONSTRAINT `fk_Korisnik_Tema1` FOREIGN KEY (`idTema`) REFERENCES `tema` (`idTema`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `nabavka`
--

DROP TABLE IF EXISTS `nabavka`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `nabavka` (
  `idNabavka` int NOT NULL,
  `idDobavljač` int NOT NULL,
  `Kolicina` decimal(4,2) NOT NULL,
  `Cijena` decimal(4,2) NOT NULL,
  PRIMARY KEY (`idNabavka`,`idDobavljač`),
  KEY `fk_Dobavljač_has_PROIZVOD_Dobavljač1_idx` (`idDobavljač`),
  CONSTRAINT `fk_Dobavljač_has_PROIZVOD_Dobavljač1` FOREIGN KEY (`idDobavljač`) REFERENCES `dobavljac` (`idDobavljac`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

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
-- Table structure for table `proizvod`
--

DROP TABLE IF EXISTS `proizvod`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `proizvod` (
  `idPROIZVOD` int NOT NULL AUTO_INCREMENT,
  `Naziv` varchar(45) NOT NULL,
  `Cijena` decimal(4,2) NOT NULL,
  `Zalihe` int NOT NULL,
  `DatumIsteka` datetime NOT NULL,
  `BarKod` varchar(45) NOT NULL,
  `idPROIZVODJAC` int NOT NULL,
  `Opis` varchar(255) DEFAULT NULL,
  `idKategorija` int NOT NULL,
  PRIMARY KEY (`idPROIZVOD`),
  UNIQUE KEY `BarKod_UNIQUE` (`BarKod`),
  KEY `fk_PROIZVOD_PROIZVODJAC1_idx` (`idPROIZVODJAC`),
  KEY `fk_PROIZVOD_KATEGORIJA1_idx` (`idKategorija`),
  CONSTRAINT `fk_PROIZVOD_KATEGORIJA1` FOREIGN KEY (`idKategorija`) REFERENCES `kategorija` (`idKategorija`),
  CONSTRAINT `fk_PROIZVOD_PROIZVODJAC1` FOREIGN KEY (`idPROIZVODJAC`) REFERENCES `proizvodjac` (`idPROIZVODJAC`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `proizvod_has_popust`
--

DROP TABLE IF EXISTS `proizvod_has_popust`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `proizvod_has_popust` (
  `idPROIZVOD` int NOT NULL,
  `idPopust` int NOT NULL,
  PRIMARY KEY (`idPROIZVOD`,`idPopust`),
  KEY `fk_PROIZVOD_has_Popust_Popust1_idx` (`idPopust`),
  KEY `fk_PROIZVOD_has_Popust_PROIZVOD1_idx` (`idPROIZVOD`),
  CONSTRAINT `fk_PROIZVOD_has_Popust_Popust1` FOREIGN KEY (`idPopust`) REFERENCES `popust` (`idPopust`),
  CONSTRAINT `fk_PROIZVOD_has_Popust_PROIZVOD1` FOREIGN KEY (`idPROIZVOD`) REFERENCES `proizvod` (`idPROIZVOD`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `proizvodjac`
--

DROP TABLE IF EXISTS `proizvodjac`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `proizvodjac` (
  `idPROIZVODJAC` int NOT NULL AUTO_INCREMENT,
  `Ime` varchar(45) NOT NULL,
  `Email` varchar(45) NOT NULL,
  PRIMARY KEY (`idPROIZVODJAC`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `racun`
--

DROP TABLE IF EXISTS `racun`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `racun` (
  `idRacun` int NOT NULL,
  `DatumKupovine` datetime NOT NULL,
  PRIMARY KEY (`idRacun`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `stavka`
--

DROP TABLE IF EXISTS `stavka`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `stavka` (
  `idRacun` int NOT NULL,
  `idPROIZVOD` int NOT NULL,
  `Kolicina` decimal(4,2) NOT NULL,
  `Cijena` decimal(4,2) NOT NULL,
  PRIMARY KEY (`idRacun`,`idPROIZVOD`),
  KEY `fk_Racun_has_PROIZVOD_PROIZVOD1_idx` (`idPROIZVOD`),
  KEY `fk_Racun_has_PROIZVOD_Racun1_idx` (`idRacun`),
  CONSTRAINT `fk_Racun_has_PROIZVOD_PROIZVOD1` FOREIGN KEY (`idPROIZVOD`) REFERENCES `proizvod` (`idPROIZVOD`),
  CONSTRAINT `fk_Racun_has_PROIZVOD_Racun1` FOREIGN KEY (`idRacun`) REFERENCES `racun` (`idRacun`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `stavka_nabvke`
--

DROP TABLE IF EXISTS `stavka_nabvke`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `stavka_nabvke` (
  `Nabavka_idNabavka` int NOT NULL,
  `Nabavka_idDobavljač` int NOT NULL,
  `PROIZVOD_idPROIZVOD` int NOT NULL,
  PRIMARY KEY (`Nabavka_idNabavka`,`Nabavka_idDobavljač`,`PROIZVOD_idPROIZVOD`),
  KEY `fk_Nabavka_has_PROIZVOD_PROIZVOD1_idx` (`PROIZVOD_idPROIZVOD`),
  KEY `fk_Nabavka_has_PROIZVOD_Nabavka1_idx` (`Nabavka_idNabavka`,`Nabavka_idDobavljač`),
  CONSTRAINT `fk_Nabavka_has_PROIZVOD_Nabavka1` FOREIGN KEY (`Nabavka_idNabavka`, `Nabavka_idDobavljač`) REFERENCES `nabavka` (`idNabavka`, `idDobavljač`),
  CONSTRAINT `fk_Nabavka_has_PROIZVOD_PROIZVOD1` FOREIGN KEY (`PROIZVOD_idPROIZVOD`) REFERENCES `proizvod` (`idPROIZVOD`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tema`
--

DROP TABLE IF EXISTS `tema`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tema` (
  `idTema` int NOT NULL,
  PRIMARY KEY (`idTema`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-11-18  8:14:06
