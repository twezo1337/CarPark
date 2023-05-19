--
-- Скрипт сгенерирован Devart dbForge Studio 2020 for MySQL, Версия 9.0.567.0
-- Домашняя страница продукта: http://www.devart.com/ru/dbforge/mysql/studio
-- Дата скрипта: 17.05.2023 17:50:48
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
-- Удалить таблицу `log_in`
--
DROP TABLE IF EXISTS log_in;

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
  typeT varchar(255) NOT NULL,
  PRIMARY KEY (IDtypeT)
)
ENGINE = INNODB,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_general_ci;

--
-- Создать таблицу `type_fuel`
--
CREATE TABLE type_fuel (
  IDfuel int(11) NOT NULL,
  typeFuel varchar(255) DEFAULT NULL,
  costFuel decimal(10, 2) DEFAULT 1.00,
  PRIMARY KEY (IDfuel)
)
ENGINE = INNODB,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_general_ci;

--
-- Создать таблицу `marks`
--
CREATE TABLE marks (
  IDmark int(11) NOT NULL,
  mark varchar(255) NOT NULL,
  PRIMARY KEY (IDmark)
)
ENGINE = INNODB,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_general_ci;

--
-- Создать таблицу `get_transport`
--
CREATE TABLE get_transport (
  IDget int(11) NOT NULL,
  dateGet datetime NOT NULL,
  PRIMARY KEY (IDget)
)
ENGINE = INNODB,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_general_ci;

--
-- Создать таблицу `transports`
--
CREATE TABLE transports (
  IDt int(11) NOT NULL AUTO_INCREMENT COMMENT 'Код автомобиля',
  gosNum varchar(255) NOT NULL COMMENT 'Гос номер',
  IDget int(11) NOT NULL COMMENT 'Код получения',
  IDfuel int(11) NOT NULL COMMENT 'Код топлива',
  IDmark int(11) NOT NULL COMMENT 'Код марки',
  IDtypeT int(11) NOT NULL COMMENT 'Код вида транспорта',
  PRIMARY KEY (IDt)
)
ENGINE = INNODB,
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
  IDtation int(11) NOT NULL,
  dateTation datetime NOT NULL,
  numPas int(11) DEFAULT NULL,
  IDt int(11) NOT NULL,
  PRIMARY KEY (IDtation)
)
ENGINE = INNODB,
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
  IDprof int(11) NOT NULL,
  nameProf varchar(255) DEFAULT NULL,
  PRIMARY KEY (IDprof)
)
ENGINE = INNODB,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_general_ci;

--
-- Создать таблицу `driver`
--
CREATE TABLE driver (
  IDdriver int(11) NOT NULL,
  IDprof int(11) NOT NULL,
  surname varchar(255) DEFAULT NULL,
  name varchar(50) DEFAULT NULL,
  patronymic varchar(255) DEFAULT NULL COMMENT 'Отчество',
  IDt int(11) NOT NULL,
  PRIMARY KEY (IDdriver)
)
ENGINE = INNODB,
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
-- Создать таблицу `log_in`
--
CREATE TABLE log_in (
  IDlog int(11) NOT NULL,
  login varchar(255) NOT NULL,
  password varchar(255) NOT NULL,
  IDdriver int(11) NOT NULL,
  PRIMARY KEY (IDlog)
)
ENGINE = INNODB,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_general_ci;

--
-- Создать внешний ключ
--
ALTER TABLE log_in
ADD CONSTRAINT FK_log_in_IDdriver FOREIGN KEY (IDdriver)
REFERENCES driver (IDdriver) ON DELETE NO ACTION;

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
CHARACTER SET utf8mb4,
COLLATE utf8mb4_general_ci;

--
-- Создать таблицу `distribution`
--
CREATE TABLE distribution (
  IDdis int(11) NOT NULL,
  dateDis datetime NOT NULL,
  IDt int(11) NOT NULL,
  IDroute int(11) NOT NULL,
  PRIMARY KEY (IDdis)
)
ENGINE = INNODB,
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
-- Вывод данных для таблицы type_transport
--
-- Таблица zi.type_transport не содержит данных

-- 
-- Вывод данных для таблицы type_fuel
--
-- Таблица zi.type_fuel не содержит данных

-- 
-- Вывод данных для таблицы marks
--
-- Таблица zi.marks не содержит данных

-- 
-- Вывод данных для таблицы get_transport
--
-- Таблица zi.get_transport не содержит данных

-- 
-- Вывод данных для таблицы transports
--
-- Таблица zi.transports не содержит данных

-- 
-- Вывод данных для таблицы profession
--
-- Таблица zi.profession не содержит данных

-- 
-- Вывод данных для таблицы driver
--
-- Таблица zi.driver не содержит данных

-- 
-- Вывод данных для таблицы route
--
-- Таблица zi.route не содержит данных

-- 
-- Вывод данных для таблицы transportation
--
-- Таблица zi.transportation не содержит данных

-- 
-- Вывод данных для таблицы log_in
--
-- Таблица zi.log_in не содержит данных

-- 
-- Вывод данных для таблицы distribution
--
-- Таблица zi.distribution не содержит данных

-- 
-- Восстановить предыдущий режим SQL (SQL mode)
--
/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;

-- 
-- Включение внешних ключей
-- 
/*!40014 SET FOREIGN_KEY_CHECKS = @OLD_FOREIGN_KEY_CHECKS */;