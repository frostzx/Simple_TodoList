-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Server version:               8.0.38 - MySQL Community Server - GPL
-- Server OS:                    Win64
-- HeidiSQL Version:             12.7.0.6850
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- Dumping database structure for todolist
CREATE DATABASE IF NOT EXISTS `todolist` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `todolist`;

-- Dumping structure for procedure todolist.DeleteList
DELIMITER //
CREATE PROCEDURE `DeleteList`(
    IN p_ID_Show VARCHAR(25)
)
BEGIN
    DELETE FROM todolist
	 WHERE ID_Show = p_ID_Show;
END//
DELIMITER ;

-- Dumping structure for procedure todolist.InsertList
DELIMITER //
CREATE PROCEDURE `InsertList`(
	IN `p_Subject` VARCHAR(255),
	IN `p_Description` VARCHAR(255),
	IN `p_userId` VARCHAR(255)
)
BEGIN
    DECLARE new_id INT;

    INSERT INTO todolist (`Subject`,`Description`,`user_id`) VALUES (p_Subject, p_Description, p_userId);

    SET new_id = LAST_INSERT_ID();

    UPDATE todolist
    SET ID_Show = CONCAT('AC-', LPAD(new_id, 4, '0'))
    WHERE todolist_id = new_id;
END//
DELIMITER ;

-- Dumping structure for table todolist.todolist
CREATE TABLE IF NOT EXISTS `todolist` (
  `todolist_id` int NOT NULL AUTO_INCREMENT,
  `ID_Show` varchar(25) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `Subject` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '-',
  `Description` varchar(255) NOT NULL DEFAULT '-',
  `Status` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT 'Not Set',
  `user_id` varchar(255) DEFAULT NULL,
  `Mark` varchar(25) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT 'Unmarked',
  PRIMARY KEY (`todolist_id`),
  UNIQUE KEY `Column 2` (`ID_Show`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='Untuk Menyimpan To Do List';

-- Data exporting was unselected.

-- Dumping structure for procedure todolist.UpdateList
DELIMITER //
CREATE PROCEDURE `UpdateList`(
	IN `p_ID_Show` VARCHAR(25),
	IN `p_Subject` VARCHAR(255),
	IN `p_Description` VARCHAR(255),
	IN `p_Status` VARCHAR(255),
	IN `p_Mark` VARCHAR(25)
)
BEGIN
    UPDATE todolist 
    SET
        `Subject` = p_Subject,
        `Description` = p_Description,
        `Status` = p_Status,
        `Mark` = p_Mark
    WHERE ID_Show = p_ID_Show;
END//
DELIMITER ;

-- Dumping structure for table todolist.user_login
CREATE TABLE IF NOT EXISTS `user_login` (
  `user_id` varchar(255) NOT NULL,
  `password` varchar(255) NOT NULL,
  `full_name` varchar(255) NOT NULL,
  PRIMARY KEY (`user_id`),
  UNIQUE KEY `user_id` (`user_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='Menyimpan data Login';

-- Data exporting was unselected.

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
