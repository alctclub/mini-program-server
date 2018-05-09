create table `member`
(
	`id` int not null auto_increment,
    `open_id` varchar(100) not null,
    `driver_identification` varchar(36) not null,
    `created_date` datetime,
    primary key(`id`)
);