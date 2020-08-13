USE batmes_client;

/*
	【系统参数分类】
*/
TRUNCATE TABLE sys_para_sort;
INSERT INTO sys_para_sort (sort_id, name, remark, show_idx) VALUES
	(1,		'通用参数',		'通用参数相关配置',			0),
	(2,		'PLC',			'PLC相关配置',				0),
	(3,		'扫码枪',		'扫码枪相关配置',			0),
	(4,		'电压测试',		'电压测试相关配置',			0),
	(5,		'电阻测试',		'电阻测试相关配置',			0),
	(6,		'温度测试',		'温度测试相关配置',			0),
	(7,		'MES',			'MES相关配置',				0);

/*
	【系统参数】

	参数类型
	1:文本
	2:整数
	3:浮点数
	4:布尔值
*/
TRUNCATE TABLE sys_para;
INSERT INTO sys_para 
	(para_id,					para_type,	para_val,		name,								remark,																					is_show,	show_idx,	sort_id) VALUES 
	/*通用参数*/
	('ComUserLoginMode',		1,			'0',			'用户登录模式',						'0：无需登录使用软件\r\n1：需要登录使用软件',											b'0',		0,			1),
	('ComTitle',				1,			'OCV',			'软件名称',							'界面显示的软件名称',																	b'1',		1,			1),
	('ComIsDebug',				4,			'True',			'是否为调试模式',					'调试模式将使用模拟器',																	b'1',		2,			1),

	('ComIsSingleTest',			4,			'True',			'是否仅支持单次测试',				'一个条码只允许一条测试数据并永远对数据进行覆盖',										b'1',		3,			1),
	('ComSingleTestDelay',		2,			'3600',			'单次测试延时(秒)',					'非单次测试模式生效，在指定时间范围内不论多少次测试均算一次并覆盖数据',					b'1',		4,			1),
	('ComVolUnit',				2,			'2',			'电压显示单位',						'1：毫伏\r\n2：伏',																		b'1',		5,			1),
	('ComVolDecimal',			2,			'3',			'电压显示精度',						'电压显示小数位数',																		b'1',		6,			1),
	('ComIrUnit',				2,			'1',			'电阻显示单位',						'1：毫欧\r\n2：欧',																		b'1',		7,			1),
	('ComIrDecimal',			2,			'0',			'电阻显示精度',						'电阻显示小数位数',																		b'1',		8,			1),
	/*PLC*/
	('PlcSerialPort',			1,			'COM1',			'PLC串口通信端口',					'PLC串口通信的COM端口',																	b'1',		1,			2),
	('PlcSerialBaud',			2,			'115200',		'PLC串口通信波特率',				'PLC串口通信的波特率',																	b'1',		2,			2),
	('PlcSerialParity',			2,			'2',			'PLC串口通信奇偶校检位',			'参见System.IO.Ports.Parity枚举\r\n0：None，1：Odd，2：Even，3：Mark，4：Space',		b'1',		3,			2),
	('PlcSerialDataBits',		2,			'7',			'PLC串口通信数据位',				'PLC串口通信的数据位',																	b'1',		4,			2),
	('PlcSerialStopBits',		2,			'3',			'PLC串口通信停止位',				'参见System.IO.Ports.StopBits枚举\r\n1：None，2：One，3：Two，4：OnePointFive',			b'1',		5,			2),
	/*扫码枪*/
	('ScanSerialPort',			1,			'COM2',			'扫码枪串口通信端口',				'扫码枪串口通信的COM端口',																b'1',		1,			3),
	('ScanSerialBaud',			2,			'115200',		'扫码枪串口通信波特率',				'扫码枪串口通信的波特率',																b'1',		2,			3),
	('ScanSerialParity',		2,			'2',			'扫码枪串口通信奇偶校检位',			'参见System.IO.Ports.Parity枚举\r\n0：None，1：Odd，2：Even，3：Mark，4：Space',		b'1',		3,			3),
	('ScanSerialDataBits',		2,			'7',			'扫码枪串口通信数据位',				'扫码枪串口通信的数据位',																b'1',		4,			3),
	('ScanSerialStopBits',		2,			'3',			'扫码枪串口通信停止位',				'参见System.IO.Ports.StopBits枚举\r\n1：None，2：One，3：Two，4：OnePointFive',			b'1',		5,			3),
	('ComScanEndFlag',			2,			'13',			'扫码结束字符',						'ASCII字符，10：换行\r\n13：回车',														b'1',		6,			3),
	/*电压测试*/
	('VolIsEnabled',			4,			'True',			'是否启用电压测试',					'是否开启电压测试',																		b'1',		1,			4),
	('VolIsUpload',				4,			'True',			'是否启用电压上传',					'是否将电压测试结果上传至MES',															b'1',		2,			4),
	('VolUploadName',			1,			'OCV',			'电压上传参数名称',					'电压上传至MES的参数名称',																b'1',		3,			4),
	('VolDefaultValue',			3,			'0',			'电压默认值',						'未启用电压测试时上传至MES的默认值',													b'1',		4,			4),
	('VolOffset',				3,			'0',			'电压补偿值',						'电压最终测试结果会加上补偿值',															b'1',		5,			4),
	('VolOkMin',				3,			'3',			'电压合格条件最小值',				'电压测试本地合格判定条件最小值',														b'1',		6,			4),
	('VolOkMax',				3,			'4',			'电压合格条件最大值',				'电压测试本地合格判定条件最大值',														b'1',		7,			4),
	('VolMeterIP',				1,			'127.0.0.1',	'电压表IP',							'电压表连接IP地址',																		b'1',		8,			4),
	('VolMeterPort',			2,			'5025',			'电压表端口',						'电压表连接端口',																		b'1',		9,			4),
	/*电阻测试*/
	('IrIsEnabled',				4,			'True',			'是否启用电阻测试',					'是否开启电阻测试',																		b'1',		1,			5),
	('IrIsUpload',				4,			'True',			'是否启用电阻上传',					'是否将电阻测试结果上传至MES',															b'1',		2,			5),
	('IrUploadName',			1,			'IMP',			'电阻上传参数名称',					'电阻上传至MES的参数名称',																b'1',		3,			5),
	('IrDefaultValue',			3,			'0',			'电阻默认值',						'未启用电阻测试时上传至MES的默认值',													b'1',		4,			5),
	('IrOffset',				3,			'0',			'电阻补偿值',						'电阻最终测试结果会加上补偿值',															b'1',		5,			5),
	('IrGeneralMin',			3,			'0',			'电阻常规测试/校准最小值',			'通过常规最小值与最大值防呆接触故障、仪表故障等',										b'1',		6,			5),
	('IrGeneralMax',			3,			'100000',		'电阻常规测试/校准最大值',			'通过常规最小值与最大值防呆接触故障、仪表故障等',										b'1',		7,			5),
	('IrOkMin',					3,			'0.1',			'电阻合格条件最小值',				'电阻测试本地合格判定条件最小值',														b'1',		8,			5),
	('IrOkMax',					3,			'0.5',			'电阻合格条件最大值',				'电阻测试本地合格判定条件最大值',														b'1',		9,			5),
	('IrMasterMin',				3,			'0',			'电阻Master合格最小值',				'电阻表校准合格判定条件最小值',															b'1',		10,			5),
	('IrMasterMax',				3,			'0',			'电阻Master合格最大值',				'电阻表校准合格判定条件最大值',															b'1',		11,			5),
	('IrMasterTimes',			2,			'10',			'电阻Master次数',					'电阻表校准总次数',																		b'1',		12,			5),
	('IrSerialPort',			1,			'COM3',			'电阻串口通信端口',					'电阻表串口通信的COM端口',																b'1',		13,			5),
	('IrSerialBaud',			2,			'9600',			'电阻串口通信波特率',				'电阻表串口通信的波特率',																b'1',		14,			5),
	('IrSerialParity',			2,			'2',			'电阻串口通信奇偶校检位',			'参见System.IO.Ports.Parity枚举\r\n0：None，1：Odd，2：Even，3：Mark，4：Space',		b'1',		15,			5),
	('IrSerialDataBits',		2,			'8',			'电阻串口通信数据位',				'电阻表串口通信的数据位',																b'1',		16,			5),
	('IrSerialStopBits',		2,			'3',			'电阻串口通信停止位',				'参见System.IO.Ports.StopBits枚举\r\n1：None，2：One，3：Two，4：OnePointFive',			b'1',		17,			5),
	/*温度测试*/
	('TempIsEnabled',			4,			'True',			'是否启用温度测试',					'是否开启温度测试',																		b'1',		1,			6),
	('TempIsUpload',			4,			'True',			'是否启用温度上传',					'是否将温度测试结果上传至MES',															b'1',		2,			6),
	('TempUploadName',			1,			'TEMPERATURE',	'温度上传参数名称',					'温度上传至MES的参数名称',																b'1',		3,			6),
	('TempDefaultValue',		3,			'20',			'温度默认值',						'未启用温度测试时上传至MES的默认值',													b'1',		4,			6),
	('TempOffset',				3,			'0',			'温度补偿值',						'温度最终测试结果会加上补偿值',															b'1',		5,			6),
	('TempOkMin',				3,			'15',			'温度合格条件最小值',				'温度测试本地合格判定条件最小值',														b'1',		6,			6),
	('TempOkMax',				3,			'30',			'温度合格条件最大值',				'温度测试本地合格判定条件最大值',														b'1',		7,			6),
	('TempSerialPort',			1,			'COM4',			'温度串口通信端口',					'温度表串口通信的COM端口',																b'1',		8,			6),
	('TempSerialBaud',			2,			'9600',			'温度串口通信波特率',				'温度表串口通信的波特率',																b'1',		9,			6),
	('TempSerialParity',		2,			'0',			'温度串口通信奇偶校检位',			'参见System.IO.Ports.Parity枚举\r\n0：None，1：Odd，2：Even，3：Mark，4：Space',		b'1',		10,			6),
	('TempSerialDataBits',		2,			'8',			'温度串口通信数据位',				'温度表串口通信的数据位',																b'1',		11,			6),
	('TempSerialStopBits',		2,			'1',			'温度串口通信停止位',				'参见System.IO.Ports.StopBits枚举\r\n1：None，2：One，3：Two，4：OnePointFive',			b'1',		12,			6),
	/*MES*/
	('MesResource',				1,			'',				'MES资源号',						'请向MES主管部门咨询获取',																b'1',		1,			7),
	('MesSite',					1,			'',				'MES站号',							'请向MES主管部门咨询获取',																b'1',		2,			7),
	('MesUserName',				1,			'',				'MES接口访问用户名',				'请向MES主管部门咨询获取',																b'1',		3,			7),
	('MesPassword',				1,			'',				'MES接口访问密码',					'请向MES主管部门咨询获取',																b'1',		4,			7),
	('MesActivityID',			1,			'',				'MES系统ActivityID',				'请向MES主管部门咨询获取',																b'1',		5,			7),
	('MesTimeout',				2,			'10000',		'MES接口连接超时时间',				'访问MES接口超时时间，单位毫秒',														b'1',		6,			7);
