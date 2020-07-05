  IF EXISTS(SELECT 1 FROM information_schema.tables 
  WHERE table_name = '
'__EFMigrationsHistory'' AND table_schema = DATABASE()) 
BEGIN
CREATE TABLE `__EFMigrationsHistory` (
    `MigrationId` varchar(150) NOT NULL,
    `ProductVersion` varchar(32) NOT NULL,
    PRIMARY KEY (`MigrationId`)
);

END;

CREATE DATABASE IF NOT EXISTS `review_application`
CREATE TABLE `review_application`.`company` (
    `Id` bigint NOT NULL AUTO_INCREMENT,
    `name` varchar(150) NOT NULL,
    `description` varchar(250) NULL,
    PRIMARY KEY (`Id`)
);

CREATE TABLE `review_application`.`product` (
    `Id` bigint NOT NULL AUTO_INCREMENT,
    `name` varchar(150) NOT NULL,
    `description` varchar(250) NULL,
    `company_id` bigint NOT NULL,
    `image_url` text NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_product_company_company_id` FOREIGN KEY (`company_id`) REFERENCES `review_application`.`company` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `review_application`.`review` (
    `Id` bigint NOT NULL AUTO_INCREMENT,
    `review_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    `stars` int NOT NULL,
    `title` varchar(150) NOT NULL,
    `content` text NOT NULL,
    `product_id` bigint NOT NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_review_product_product_id` FOREIGN KEY (`product_id`) REFERENCES `review_application`.`product` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `review_application`.`text_analysis` (
    `Id` bigint NOT NULL AUTO_INCREMENT,
    `review_id` bigint NOT NULL,
    `query_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    `language` varchar(50) NULL,
    `language_score` double NOT NULL,
    `sentiment` varchar(50) NULL,
    `positive_score` double NOT NULL,
    `neutral_score` double NOT NULL,
    `negative_score` double NOT NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_text_analysis_review_review_id` FOREIGN KEY (`review_id`) REFERENCES `review_application`.`review` (`Id`) ON DELETE CASCADE
);

CREATE UNIQUE INDEX `uq_company_name` ON `review_application`.`company` (`name`);

CREATE INDEX `IX_product_company_id` ON `review_application`.`product` (`company_id`);

CREATE UNIQUE INDEX `uq_product_name` ON `review_application`.`product` (`name`);

CREATE INDEX `IX_review_product_id` ON `review_application`.`review` (`product_id`);

CREATE INDEX `idx_review_date` ON `review_application`.`review` (`review_date`);

CREATE INDEX `idx_review_starts` ON `review_application`.`review` (`stars`);

CREATE INDEX `idx_review_title` ON `review_application`.`review` (`title`);

CREATE INDEX `idx_text_analysis_query_date` ON `review_application`.`text_analysis` (`query_date`);

CREATE UNIQUE INDEX `IX_text_analysis_review_id` ON `review_application`.`text_analysis` (`review_id`);

CREATE INDEX `idx_text_analysis_sentiment` ON `review_application`.`text_analysis` (`sentiment`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20200704212347_InitialMigration', '3.1.5');

