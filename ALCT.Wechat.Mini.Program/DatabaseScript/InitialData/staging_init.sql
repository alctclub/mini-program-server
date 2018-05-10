use miniprogram;

insert into system_config(`key`, `value`, `desc`,`created_date`)
values ('ALCTConfiguration', '{"EnterpriseCode":"E0000101","AppIdentity":"072dffe08b9d412b99eafab3e2f02c93","AppKey":"1b1b734be0b64b549968ae8058c3b4af","OpenApiHost":"http://local.alct.com:4009/"}', 'ALCT Config', now()),
('WechatConfiguration', '{"AppId":"AppSecret","AppSecret":"AppSecret"}', 'Wechat config', now());