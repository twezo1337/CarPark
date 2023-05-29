--
-- Скрипт сгенерирован Devart dbForge Studio 2020 for MySQL, Версия 9.0.567.0
-- Домашняя страница продукта: http://www.devart.com/ru/dbforge/mysql/studio
-- Дата скрипта: 28.05.2023 17:07:29
-- Версия сервера: 5.7.11
-- Версия клиента: 4.1
--

-- 
-- Отключение внешних ключей
-- 
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;

-- 
-- Установить режим SQL (SQL mode)
-- 
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

-- 
-- Установка кодировки, с использованием которой клиент будет посылать запросы на сервер
--
SET NAMES 'utf8';

--
-- Установка базы данных по умолчанию
--
USE zi;

--
-- Удалить таблицу `log_in`
--
DROP TABLE IF EXISTS log_in;

--
-- Удалить представление `transportation_info_view`
--
DROP VIEW IF EXISTS transportation_info_view CASCADE;

--
-- Удалить таблицу `distribution`
--
DROP TABLE IF EXISTS distribution;

--
-- Удалить таблицу `route`
--
DROP TABLE IF EXISTS route;

--
-- Удалить представление `driver_info_view`
--
DROP VIEW IF EXISTS driver_info_view CASCADE;

--
-- Удалить таблицу `driver`
--
DROP TABLE IF EXISTS driver;

--
-- Удалить таблицу `profession`
--
DROP TABLE IF EXISTS profession;

--
-- Удалить таблицу `transportation`
--
DROP TABLE IF EXISTS transportation;

--
-- Удалить таблицу `transports`
--
DROP TABLE IF EXISTS transports;

--
-- Удалить таблицу `get_transport`
--
DROP TABLE IF EXISTS get_transport;

--
-- Удалить таблицу `marks`
--
DROP TABLE IF EXISTS marks;

--
-- Удалить таблицу `type_fuel`
--
DROP TABLE IF EXISTS type_fuel;

--
-- Удалить таблицу `type_transport`
--
DROP TABLE IF EXISTS type_transport;

--
-- Установка базы данных по умолчанию
--
USE zi;

--
-- Создать таблицу `type_transport`
--
CREATE TABLE type_transport (
  IDtypeT int(11) NOT NULL AUTO_INCREMENT,
  typeT varchar(255) DEFAULT NULL,
  PRIMARY KEY (IDtypeT)
)
ENGINE = INNODB,
AUTO_INCREMENT = 3,
AVG_ROW_LENGTH = 8192,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_general_ci;

--
-- Создать таблицу `type_fuel`
--
CREATE TABLE type_fuel (
  IDfuel int(11) NOT NULL AUTO_INCREMENT,
  typeFuel varchar(255) DEFAULT NULL,
  costFuel decimal(10, 2) DEFAULT 1.00,
  PRIMARY KEY (IDfuel)
)
ENGINE = INNODB,
AUTO_INCREMENT = 3,
AVG_ROW_LENGTH = 8192,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_general_ci;

--
-- Создать таблицу `marks`
--
CREATE TABLE marks (
  IDmark int(11) NOT NULL AUTO_INCREMENT,
  mark varchar(255) DEFAULT NULL,
  PRIMARY KEY (IDmark)
)
ENGINE = INNODB,
AUTO_INCREMENT = 5,
AVG_ROW_LENGTH = 5461,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_general_ci;

--
-- Создать таблицу `get_transport`
--
CREATE TABLE get_transport (
  IDget int(11) NOT NULL AUTO_INCREMENT,
  dateGet datetime DEFAULT NULL,
  PRIMARY KEY (IDget)
)
ENGINE = INNODB,
AUTO_INCREMENT = 4,
AVG_ROW_LENGTH = 8192,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_general_ci;

--
-- Создать таблицу `transports`
--
CREATE TABLE transports (
  IDt int(11) NOT NULL AUTO_INCREMENT COMMENT 'Код автомобиля',
  gosNum varchar(255) DEFAULT NULL COMMENT 'Гос номер',
  IDget int(11) DEFAULT NULL COMMENT 'Код получения',
  IDfuel int(11) DEFAULT NULL COMMENT 'Код топлива',
  IDmark int(11) DEFAULT NULL COMMENT 'Код марки',
  IDtypeT int(11) DEFAULT NULL COMMENT 'Код вида транспорта',
  PRIMARY KEY (IDt)
)
ENGINE = INNODB,
AUTO_INCREMENT = 19,
AVG_ROW_LENGTH = 8192,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_general_ci,
COMMENT = 'Автомобили';

--
-- Создать внешний ключ
--
ALTER TABLE transports
ADD CONSTRAINT FK_transports_IDfuel FOREIGN KEY (IDfuel)
REFERENCES type_fuel (IDfuel) ON DELETE NO ACTION;

--
-- Создать внешний ключ
--
ALTER TABLE transports
ADD CONSTRAINT FK_transports_IDget FOREIGN KEY (IDget)
REFERENCES get_transport (IDget) ON DELETE NO ACTION;

--
-- Создать внешний ключ
--
ALTER TABLE transports
ADD CONSTRAINT FK_transports_IDmark FOREIGN KEY (IDmark)
REFERENCES marks (IDmark) ON DELETE NO ACTION;

--
-- Создать внешний ключ
--
ALTER TABLE transports
ADD CONSTRAINT FK_transports_IDtypeT FOREIGN KEY (IDtypeT)
REFERENCES type_transport (IDtypeT) ON DELETE NO ACTION;

--
-- Создать таблицу `transportation`
--
CREATE TABLE transportation (
  IDtation int(11) NOT NULL AUTO_INCREMENT,
  dateTation datetime DEFAULT NULL,
  numPas int(11) DEFAULT NULL,
  IDt int(11) DEFAULT NULL,
  PRIMARY KEY (IDtation)
)
ENGINE = INNODB,
AUTO_INCREMENT = 17,
AVG_ROW_LENGTH = 5461,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_general_ci;

--
-- Создать внешний ключ
--
ALTER TABLE transportation
ADD CONSTRAINT FK_transportation_IDt FOREIGN KEY (IDt)
REFERENCES transports (IDt) ON DELETE NO ACTION;

--
-- Создать таблицу `profession`
--
CREATE TABLE profession (
  IDprof int(11) NOT NULL AUTO_INCREMENT,
  nameProf varchar(255) DEFAULT NULL,
  PRIMARY KEY (IDprof)
)
ENGINE = INNODB,
AUTO_INCREMENT = 3,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_general_ci;

--
-- Создать таблицу `driver`
--
CREATE TABLE driver (
  IDdriver int(11) NOT NULL,
  IDprof int(11) DEFAULT NULL,
  surname varchar(255) DEFAULT NULL,
  name varchar(50) DEFAULT NULL,
  patronymic varchar(255) DEFAULT NULL COMMENT 'Отчество',
  IDt int(11) DEFAULT NULL,
  PRIMARY KEY (IDdriver)
)
ENGINE = INNODB,
AVG_ROW_LENGTH = 16384,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_general_ci;

--
-- Создать внешний ключ
--
ALTER TABLE driver
ADD CONSTRAINT FK_driver_IDprof FOREIGN KEY (IDprof)
REFERENCES profession (IDprof) ON DELETE NO ACTION;

--
-- Создать внешний ключ
--
ALTER TABLE driver
ADD CONSTRAINT FK_driver_IDt FOREIGN KEY (IDt)
REFERENCES transports (IDt) ON DELETE NO ACTION;

--
-- Создать представление `driver_info_view`
--
CREATE
DEFINER = 'root'@'localhost'
VIEW driver_info_view
AS
SELECT
  `type_transport`.`typeT` AS `typeT`,
  `transports`.`gosNum` AS `gosNum`,
  `driver`.`surname` AS `surname`,
  `driver`.`name` AS `name`,
  `driver`.`patronymic` AS `patronymic`,
  `marks`.`mark` AS `mark`
FROM (((`driver`
  JOIN `transports`
    ON ((`driver`.`IDt` = `transports`.`IDt`)))
  JOIN `marks`
    ON ((`transports`.`IDmark` = `marks`.`IDmark`)))
  JOIN `type_transport`
    ON ((`transports`.`IDtypeT` = `type_transport`.`IDtypeT`)));

--
-- Создать таблицу `route`
--
CREATE TABLE route (
  IDroute int(11) NOT NULL AUTO_INCREMENT,
  lengthRoute decimal(10, 2) DEFAULT 1.00,
  nameRoute varchar(255) DEFAULT NULL,
  PRIMARY KEY (IDroute)
)
ENGINE = INNODB,
AUTO_INCREMENT = 4,
AVG_ROW_LENGTH = 8192,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_general_ci;

--
-- Создать таблицу `distribution`
--
CREATE TABLE distribution (
  IDdis int(11) NOT NULL AUTO_INCREMENT,
  dateDis datetime DEFAULT NULL,
  IDt int(11) DEFAULT NULL,
  IDroute int(11) DEFAULT NULL,
  PRIMARY KEY (IDdis)
)
ENGINE = INNODB,
AUTO_INCREMENT = 17,
AVG_ROW_LENGTH = 5461,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_general_ci;

--
-- Создать внешний ключ
--
ALTER TABLE distribution
ADD CONSTRAINT FK_distribution_IDroute FOREIGN KEY (IDroute)
REFERENCES route (IDroute) ON DELETE NO ACTION;

--
-- Создать внешний ключ
--
ALTER TABLE distribution
ADD CONSTRAINT FK_distribution_IDt FOREIGN KEY (IDt)
REFERENCES transports (IDt) ON DELETE NO ACTION;

--
-- Создать представление `transportation_info_view`
--
CREATE
DEFINER = 'root'@'localhost'
VIEW transportation_info_view
AS
SELECT
  `transports`.`gosNum` AS `gosNum`,
  `marks`.`mark` AS `mark`,
  `type_transport`.`typeT` AS `typeT`,
  `route`.`nameRoute` AS `nameRoute`,
  `route`.`lengthRoute` AS `lengthRoute`,
  `distribution`.`dateDis` AS `dateDis`,
  `transportation`.`numPas` AS `numPas`
FROM (((((`transports`
  JOIN `marks`
    ON ((`transports`.`IDmark` = `marks`.`IDmark`)))
  JOIN `type_transport`
    ON ((`transports`.`IDtypeT` = `type_transport`.`IDtypeT`)))
  JOIN `transportation`
    ON ((`transportation`.`IDt` = `transports`.`IDt`)))
  JOIN `distribution`
    ON ((`distribution`.`IDt` = `transports`.`IDt`)))
  JOIN `route`
    ON ((`distribution`.`IDroute` = `route`.`IDroute`)));

--
-- Создать таблицу `log_in`
--
CREATE TABLE log_in (
  IDdriver int(11) NOT NULL AUTO_INCREMENT,
  IDlog tinyint(1) NOT NULL,
  login varchar(255) NOT NULL,
  password varchar(255) NOT NULL,
  PRIMARY KEY (IDdriver)
)
ENGINE = INNODB,
AUTO_INCREMENT = 2,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_general_ci;

-- 
-- Вывод данных для таблицы type_transport
--
INSERT INTO type_transport VALUES
(1, 'Легковой'),
(2, 'Пассажирский автобус');

-- 
-- Вывод данных для таблицы type_fuel
--
INSERT INTO type_fuel VALUES
(1, 'Бензин ', 2005.00),
(2, 'Дизель', 2423.00);

-- 
-- Вывод данных для таблицы marks
--
INSERT INTO marks VALUES
(1, 'ГАЗель'),
(3, 'Mercedes'),
(4, 'Hyundai');

-- 
-- Вывод данных для таблицы get_transport
--
INSERT INTO get_transport VALUES
(1, '2023-05-28 00:00:00'),
(2, '2023-05-29 00:00:00'),
(3, '2023-06-09 00:00:00');

-- 
-- Вывод данных для таблицы profession
--
INSERT INTO profession VALUES
(1, 'Таксист'),
(2, 'Водитель автобуса');

-- 
-- Вывод данных для таблицы transports
--
INSERT INTO transports VALUES
(17, 'а534до', 1, 1, 1, 2),
(18, 'п648', 2, 2, 4, 2);

-- 
-- Вывод данных для таблицы route
--
INSERT INTO route VALUES
(1, 382.00, 'Красноярск-Саяногорск'),
(2, 84.00, 'Красноярск-Железногорск'),
(3, 403.00, 'Красноярск-Ачинск');

-- 
-- Вывод данных для таблицы transportation
--
INSERT INTO transportation VALUES
(1, '2023-05-30 00:00:00', 33, 17),
(2, '2023-06-02 00:00:00', 28, 18),
(3, '2023-06-09 00:00:00', 44, 17);

-- 
-- Вывод данных для таблицы log_in
--
INSERT INTO log_in VALUES
(1, 1, 'fsddf', 'sdfsd');

-- 
-- Вывод данных для таблицы driver
--
INSERT INTO driver VALUES
(1, 2, 'Иванов', 'Иван', 'Иванович', 17);

-- 
-- Вывод данных для таблицы distribution
--
INSERT INTO distribution VALUES
(1, '2023-05-27 00:00:00', 17, 1),
(2, '2023-05-30 00:00:00', 18, 2),
(3, '2023-05-29 00:00:00', NULL, 3);

--
-- Установка базы данных по умолчанию
--
USE zi;

--
-- Удалить триггер `tr_add_driver_in_driver`
--
DROP TRIGGER IF EXISTS tr_add_driver_in_driver;

--
-- Удалить триггер `tr_delete_driver_from_driver`
--
DROP TRIGGER IF EXISTS tr_delete_driver_from_driver;

--
-- Установка базы данных по умолчанию
--
USE zi;

DELIMITER $$

--
-- Создать триггер `tr_delete_driver_from_driver`
--
CREATE
DEFINER = 'root'@'localhost'
TRIGGER tr_delete_driver_from_driver
BEFORE DELETE
ON log_in
FOR EACH ROW
BEGIN
  DELETE
    FROM driver
  WHERE IDdriver = OLD.IDdriver;
END
$$

--
-- Создать триггер `tr_add_driver_in_driver`
--
CREATE
DEFINER = 'root'@'localhost'
TRIGGER tr_add_driver_in_driver
AFTER INSERT
ON log_in
FOR EACH ROW
BEGIN
  INSERT INTO driver
  SET IDdriver = NEW.IDdriver;
END
$$

DELIMITER ;

-- 
-- Восстановить предыдущий режим SQL (SQL mode)
--
/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;

-- 
-- Включение внешних ключей
-- 
/*!40014 SET FOREIGN_KEY_CHECKS = @OLD_FOREIGN_KEY_CHECKS */;