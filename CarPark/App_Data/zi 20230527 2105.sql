-- Скрипт сгенерирован Devart dbForge Studio for MySQL, Версия 6.0.441.0
-- Домашняя страница продукта: http://www.devart.com/ru/dbforge/mysql/studio
-- Дата скрипта: 27.05.2023 21:05:58
-- Версия сервера: 5.7.11
-- Версия клиента: 4.1

-- 
-- Отключение внешних ключей
-- 
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;

-- 
-- Установка кодировки, с использованием которой клиент будет посылать запросы на сервер
--
SET NAMES 'utf8';

-- 
-- Установка базы данных по умолчанию
--
USE zi;

--
-- Описание для таблицы get_transport
--
DROP TABLE IF EXISTS get_transport;
CREATE TABLE get_transport (
  IDget INT(11) NOT NULL,
  dateGet DATETIME NOT NULL,
  PRIMARY KEY (IDget)
)
ENGINE = INNODB
CHARACTER SET utf8mb4
COLLATE utf8mb4_general_ci
ROW_FORMAT = DYNAMIC;

--
-- Описание для таблицы marks
--
DROP TABLE IF EXISTS marks;
CREATE TABLE marks (
  IDmark INT(11) NOT NULL,
  mark VARCHAR(255) NOT NULL,
  PRIMARY KEY (IDmark)
)
ENGINE = INNODB
CHARACTER SET utf8mb4
COLLATE utf8mb4_general_ci
ROW_FORMAT = DYNAMIC;

--
-- Описание для таблицы profession
--
DROP TABLE IF EXISTS profession;
CREATE TABLE profession (
  IDprof INT(11) NOT NULL,
  nameProf VARCHAR(255) DEFAULT NULL,
  PRIMARY KEY (IDprof)
)
ENGINE = INNODB
CHARACTER SET utf8mb4
COLLATE utf8mb4_general_ci
ROW_FORMAT = DYNAMIC;

--
-- Описание для таблицы route
--
DROP TABLE IF EXISTS route;
CREATE TABLE route (
  IDroute INT(11) NOT NULL AUTO_INCREMENT,
  lengthRoute DECIMAL(10, 2) DEFAULT 1.00,
  nameRoute VARCHAR(255) DEFAULT NULL,
  PRIMARY KEY (IDroute)
)
ENGINE = INNODB
AUTO_INCREMENT = 1
CHARACTER SET utf8mb4
COLLATE utf8mb4_general_ci
ROW_FORMAT = DYNAMIC;

--
-- Описание для таблицы type_fuel
--
DROP TABLE IF EXISTS type_fuel;
CREATE TABLE type_fuel (
  IDfuel INT(11) NOT NULL,
  typeFuel VARCHAR(255) DEFAULT NULL,
  costFuel DECIMAL(10, 2) DEFAULT 1.00,
  PRIMARY KEY (IDfuel)
)
ENGINE = INNODB
CHARACTER SET utf8mb4
COLLATE utf8mb4_general_ci
ROW_FORMAT = DYNAMIC;

--
-- Описание для таблицы type_transport
--
DROP TABLE IF EXISTS type_transport;
CREATE TABLE type_transport (
  IDtypeT INT(11) NOT NULL AUTO_INCREMENT,
  typeT VARCHAR(255) NOT NULL,
  PRIMARY KEY (IDtypeT)
)
ENGINE = INNODB
AUTO_INCREMENT = 2
CHARACTER SET utf8mb4
COLLATE utf8mb4_general_ci
ROW_FORMAT = DYNAMIC;

--
-- Описание для таблицы transports
--
DROP TABLE IF EXISTS transports;
CREATE TABLE transports (
  IDt INT(11) NOT NULL AUTO_INCREMENT COMMENT 'Код автомобиля',
  gosNum VARCHAR(255) NOT NULL COMMENT 'Гос номер',
  IDget INT(11) NOT NULL COMMENT 'Код получения',
  IDfuel INT(11) NOT NULL COMMENT 'Код топлива',
  IDmark INT(11) NOT NULL COMMENT 'Код марки',
  IDtypeT INT(11) NOT NULL COMMENT 'Код вида транспорта',
  PRIMARY KEY (IDt),
  CONSTRAINT FK_transports_IDfuel FOREIGN KEY (IDfuel)
    REFERENCES type_fuel(IDfuel) ON DELETE NO ACTION ON UPDATE RESTRICT,
  CONSTRAINT FK_transports_IDget FOREIGN KEY (IDget)
    REFERENCES get_transport(IDget) ON DELETE NO ACTION ON UPDATE RESTRICT,
  CONSTRAINT FK_transports_IDmark FOREIGN KEY (IDmark)
    REFERENCES marks(IDmark) ON DELETE NO ACTION ON UPDATE RESTRICT,
  CONSTRAINT FK_transports_IDtypeT FOREIGN KEY (IDtypeT)
    REFERENCES type_transport(IDtypeT) ON DELETE NO ACTION ON UPDATE RESTRICT
)
ENGINE = INNODB
AUTO_INCREMENT = 2
CHARACTER SET utf8mb4
COLLATE utf8mb4_general_ci
COMMENT = 'Автомобили'
ROW_FORMAT = DYNAMIC;

--
-- Описание для таблицы distribution
--
DROP TABLE IF EXISTS distribution;
CREATE TABLE distribution (
  IDdis INT(11) NOT NULL,
  dateDis DATETIME NOT NULL,
  IDt INT(11) NOT NULL,
  IDroute INT(11) NOT NULL,
  PRIMARY KEY (IDdis),
  CONSTRAINT FK_distribution_IDroute FOREIGN KEY (IDroute)
    REFERENCES route(IDroute) ON DELETE NO ACTION ON UPDATE RESTRICT,
  CONSTRAINT FK_distribution_IDt FOREIGN KEY (IDt)
    REFERENCES transports(IDt) ON DELETE NO ACTION ON UPDATE RESTRICT
)
ENGINE = INNODB
CHARACTER SET utf8mb4
COLLATE utf8mb4_general_ci
ROW_FORMAT = DYNAMIC;

--
-- Описание для таблицы driver
--
DROP TABLE IF EXISTS driver;
CREATE TABLE driver (
  IDdriver INT(11) NOT NULL,
  IDprof INT(11) NOT NULL,
  surname VARCHAR(255) DEFAULT NULL,
  name VARCHAR(50) DEFAULT NULL,
  patronymic VARCHAR(255) DEFAULT NULL COMMENT 'Отчество',
  IDt INT(11) NOT NULL,
  PRIMARY KEY (IDdriver),
  CONSTRAINT FK_driver_IDprof FOREIGN KEY (IDprof)
    REFERENCES profession(IDprof) ON DELETE NO ACTION ON UPDATE RESTRICT,
  CONSTRAINT FK_driver_IDt FOREIGN KEY (IDt)
    REFERENCES transports(IDt) ON DELETE NO ACTION ON UPDATE RESTRICT
)
ENGINE = INNODB
CHARACTER SET utf8mb4
COLLATE utf8mb4_general_ci
ROW_FORMAT = DYNAMIC;

--
-- Описание для таблицы transportation
--
DROP TABLE IF EXISTS transportation;
CREATE TABLE transportation (
  IDtation INT(11) NOT NULL,
  dateTation DATETIME NOT NULL,
  numPas INT(11) DEFAULT NULL,
  IDt INT(11) NOT NULL,
  PRIMARY KEY (IDtation),
  CONSTRAINT FK_transportation_IDt FOREIGN KEY (IDt)
    REFERENCES transports(IDt) ON DELETE NO ACTION ON UPDATE RESTRICT
)
ENGINE = INNODB
CHARACTER SET utf8mb4
COLLATE utf8mb4_general_ci
ROW_FORMAT = DYNAMIC;

--
-- Описание для таблицы log_in
--
DROP TABLE IF EXISTS log_in;
CREATE TABLE log_in (
  IDlog INT(11) NOT NULL,
  login VARCHAR(255) NOT NULL,
  password VARCHAR(255) NOT NULL,
  IDdriver INT(11) NOT NULL,
  PRIMARY KEY (IDlog),
  CONSTRAINT FK_log_in_IDdriver FOREIGN KEY (IDdriver)
    REFERENCES driver(IDdriver) ON DELETE NO ACTION ON UPDATE RESTRICT
)
ENGINE = INNODB
AVG_ROW_LENGTH = 8192
CHARACTER SET utf8mb4
COLLATE utf8mb4_general_ci
ROW_FORMAT = DYNAMIC;

--
-- Описание для представления driver_info_view
--
DROP VIEW IF EXISTS driver_info_view CASCADE;
CREATE OR REPLACE 
	DEFINER = 'root'@'localhost'
VIEW driver_info_view
AS
	select `type_transport`.`typeT` AS `typeT`,`transports`.`gosNum` AS `gosNum`,`driver`.`surname` AS `surname`,`driver`.`name` AS `name`,`driver`.`patronymic` AS `patronymic`,`marks`.`mark` AS `mark` from (((`driver` join `transports` on((`driver`.`IDt` = `transports`.`IDt`))) join `marks` on((`transports`.`IDmark` = `marks`.`IDmark`))) join `type_transport` on((`transports`.`IDtypeT` = `type_transport`.`IDtypeT`)));

--
-- Описание для представления transportation_info_view
--
DROP VIEW IF EXISTS transportation_info_view CASCADE;
CREATE OR REPLACE 
	DEFINER = 'root'@'localhost'
VIEW transportation_info_view
AS
	select `transports`.`gosNum` AS `gosNum`,`marks`.`mark` AS `mark`,`type_transport`.`typeT` AS `typeT`,`route`.`nameRoute` AS `nameRoute`,`route`.`lengthRoute` AS `lengthRoute`,`distribution`.`dateDis` AS `dateDis`,`transportation`.`numPas` AS `numPas` from (((((`transports` join `marks` on((`transports`.`IDmark` = `marks`.`IDmark`))) join `type_transport` on((`transports`.`IDtypeT` = `type_transport`.`IDtypeT`))) join `transportation` on((`transportation`.`IDt` = `transports`.`IDt`))) join `distribution` on((`distribution`.`IDt` = `transports`.`IDt`))) join `route` on((`distribution`.`IDroute` = `route`.`IDroute`)));

-- 
-- Вывод данных для таблицы get_transport
--
INSERT INTO get_transport VALUES
(1, '2023-05-27 20:11:45');

-- 
-- Вывод данных для таблицы marks
--
INSERT INTO marks VALUES
(1, 'toyota');

-- 
-- Вывод данных для таблицы profession
--
INSERT INTO profession VALUES
(1, 'Taxi');

-- 
-- Вывод данных для таблицы route
--

-- Таблица zi.route не содержит данных

-- 
-- Вывод данных для таблицы type_fuel
--
INSERT INTO type_fuel VALUES
(1, '92', 46.25);

-- 
-- Вывод данных для таблицы type_transport
--
INSERT INTO type_transport VALUES
(1, 'Gruzovoe');

-- 
-- Вывод данных для таблицы transports
--
INSERT INTO transports VALUES
(1, '228', 1, 1, 1, 1);

-- 
-- Вывод данных для таблицы distribution
--

-- Таблица zi.distribution не содержит данных

-- 
-- Вывод данных для таблицы driver
--
INSERT INTO driver VALUES
(1, 1, 'sukha', 'chev', 'qwae', 1);

-- 
-- Вывод данных для таблицы transportation
--

-- Таблица zi.transportation не содержит данных

-- 
-- Вывод данных для таблицы log_in
--
INSERT INTO log_in VALUES
(1, '123', '333', 1),
(2, '123', '333', 1);

-- 
-- Включение внешних ключей
-- 
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;