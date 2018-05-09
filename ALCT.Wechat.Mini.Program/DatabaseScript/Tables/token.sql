create table `token`
(
	`id` int not null auto_increment,
    `open_id` varchar(100) not null,
    `access_token` varchar(2000) not null,
    `refresh_token` varchar(2000) null,
    `created_date` datetime,
    `expiry_date` datetime,
    primary key(`id`)
);