create table `system_config` ( 
    `id`  int unsigned not null auto_increment,            
    `key` varchar(200) not null,
    `value` varchar(5000) not null,
    `desc` varchar(200) not null,
    `created_date` datetime not null,     
  primary key (`id`)
) engine=innodb;