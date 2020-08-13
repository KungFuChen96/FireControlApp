/*******************************************************************************
BATMES上位机软件框架

吴剑	2019-08-27	创建
吴剑	2019-09-10	添加用户、角色、功能、授权、用户日志相关表
陈冠宇	2019-12-05	将工艺工序等字段改为battery表中的主键，以适应同一台机器
					同一个电芯在不同时间做不同工序时可以分开产生数据记录
吴剑	2020-01-15	优化工序表ops

*******************************************************************************/

CREATE DATABASE IF NOT EXISTS batmes_client;
USE batmes_client;

/***************************************************************************
0.分页存储过程
***************************************************************************/

DROP PROCEDURE IF EXISTS pager;
DELIMITER //
CREATE PROCEDURE pager(

	IN	in_fields			TEXT,				/*查询字段*/
	IN	in_tables			TEXT,				/*表名*/
	IN	in_where			TEXT,				/*WHERE 语句(包含WHERE关键字,可为空,GROUP BY必须包含在此参数中)*/
	IN	in_order			TEXT,				/*ORDER BY 语句(包含ORDER BY关键字,可为空)*/
	IN	in_pagesize			INT,				/*每页记录数*/
	IN	in_pageindex		INT,				/*当前页*/
	OUT	out_rows			INT					/*输出记录总数*/
)
	NOT DETERMINISTIC
	SQL SECURITY DEFINER
	COMMENT '分页存储过程'
	
BEGIN

	/*定义变量*/
	DECLARE m_begin_row INT DEFAULT 0;
	DECLARE m_limit TEXT;

	/*构造语句*/	
	SET m_begin_row = (in_pageindex - 1) * in_pagesize;
	SET m_limit = CONCAT(' LIMIT ', m_begin_row, ', ', in_pagesize);
	
	SET @COUNT_STRING = CONCAT('SELECT COUNT(*) INTO @ROWS_TOTAL FROM (SELECT ', in_fields, ' FROM ', in_tables, ' ', in_where, ') temp_table_pager');
	SET @MAIN_STRING = CONCAT('SELECT ', in_fields, ' FROM ', in_tables, ' ', in_where, ' ', in_order, m_limit);

	/*预处理*/
	PREPARE count_stmt FROM @COUNT_STRING;
	EXECUTE count_stmt;
	DEALLOCATE PREPARE count_stmt;
	SET out_rows = @ROWS_TOTAL;

	PREPARE main_stmt FROM @MAIN_STRING;
	EXECUTE main_stmt;
	DEALLOCATE PREPARE main_stmt;
	
END; //

/*******************************************************************************
1.系统参数分类
*******************************************************************************/

DROP TABLE IF EXISTS sys_para_sort;
CREATE TABLE IF NOT EXISTS sys_para_sort (

	sort_id					INT NOT NULL AUTO_INCREMENT						COMMENT '分类ID',
	name					VARCHAR(128) NOT NULL							COMMENT '分类名称',
	remark					VARCHAR(1024) NOT NULL DEFAULT ''				COMMENT '备注',
	show_idx				INT NOT NULL DEFAULT 100						COMMENT '显示排序(ASC)',

	PRIMARY KEY (sort_id),
	INDEX (show_idx)

) COMMENT '系统参数分类';

/*******************************************************************************
2.系统参数
*******************************************************************************/

DROP TABLE IF EXISTS sys_para;
CREATE TABLE IF NOT EXISTS sys_para (

	para_id					VARCHAR(64) NOT NULL							COMMENT '参数ID',
	para_type				INT NOT NULL DEFAULT 1							COMMENT '参数类型(见SysParaType枚举)',
	para_val				MEDIUMTEXT NOT NULL								COMMENT '参数值',
	name					VARCHAR(128) NOT NULL							COMMENT '参数名称',
	remark					VARCHAR(1024) NOT NULL DEFAULT ''				COMMENT '备注',
	is_show					BIT(1) NOT NULL DEFAULT b'0'					COMMENT '是否软件可见',
	show_idx				INT NOT NULL DEFAULT 100						COMMENT '显示排序(ASC)',
	sort_id					INT NOT NULL									COMMENT '分类ID',

	PRIMARY KEY (para_id),
	INDEX (sort_id, show_idx)

) COMMENT '系统参数';

/*******************************************************************************
3.系统事件(面向开发，用于故障定位)
*******************************************************************************/

DROP TABLE IF EXISTS sys_event;
CREATE TABLE IF NOT EXISTS sys_event (

	event_id			INT NOT NULL AUTO_INCREMENT							COMMENT '事件ID',
	title				VARCHAR(128) NOT NULL DEFAULT ''					COMMENT '标题',
	cont				TEXT NOT NULL										COMMENT '内容',
	level				INT NOT NULL DEFAULT 1								COMMENT '级别(见SysEventLevel枚举)',
	create_time			DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP()		COMMENT '生成时间',
	
	PRIMARY KEY (event_id),
	INDEX (level),
	INDEX (create_time)

) COMMENT '系统事件';

/*******************************************************************************
4.系统日志(面向用户，记录用户需要的日志)
*******************************************************************************/

DROP TABLE IF EXISTS logs;
CREATE TABLE IF NOT EXISTS logs (

	log_id				INT NOT NULL AUTO_INCREMENT					        COMMENT '日志ID',
	title				VARCHAR(128) NOT NULL DEFAULT ''					COMMENT '标题',
	cont				MEDIUMTEXT NOT NULL									COMMENT '内容',
	type				INT NOT NULL DEFAULT 0								COMMENT '类型(见LogType枚举)',
	create_time			DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP()		COMMENT '生成时间',
	
	PRIMARY KEY (log_id),
	INDEX (type),
	INDEX (create_time)

) COMMENT '系统日志';

/*******************************************************************************
5.用户
*******************************************************************************/

DROP TABLE IF EXISTS user;
CREATE TABLE IF NOT EXISTS user (

	user_id				INT NOT NULL AUTO_INCREMENT					        COMMENT '用户ID',
	account				VARCHAR(128) NOT NULL						        COMMENT '登录账号',
	pswd				VARCHAR(512) NOT NULL								COMMENT '登录密码(SHA2)',
	name				VARCHAR(32) NOT NULL						        COMMENT '姓名',
	gender				INT NOT NULL DEFAULT 0								COMMENT '性别(见Gender枚举)',
	tel					VARCHAR(32) NOT NULL DEFAULT ''                     COMMENT '联系电话',
	im					VARCHAR(128) NOT NULL DEFAULT ''                    COMMENT '联系IM',
	email				VARCHAR(128) NOT NULL DEFAULT ''                    COMMENT '电子邮件',
	remark				VARCHAR(1024) NOT NULL DEFAULT ''                   COMMENT '备注',

	login_time			DATETIME NULL					   		            COMMENT '最后登录时间',
	login_count			INT NOT NULL DEFAULT 0								COMMENT '登录总次数',

	status				INT NOT NULL DEFAULT 1								COMMENT '状态(见UserStatus枚举)',
	status_time			DATETIME NOT NULL									COMMENT '状态变更时间',
    create_time			DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP()		COMMENT '创建时间',
	
	PRIMARY KEY (user_id),
	UNIQUE KEY (account),
	INDEX (status)

) COMMENT '用户';

/*******************************************************************************
6.角色
*******************************************************************************/

DROP TABLE IF EXISTS role;
CREATE TABLE IF NOT EXISTS role (

	role_id				INT NOT NULL AUTO_INCREMENT							COMMENT '角色ID',
	name				VARCHAR(32) NOT NULL						        COMMENT '名称',
	remark				VARCHAR(1024) NOT NULL DEFAULT ''				    COMMENT '备注',

	PRIMARY KEY (role_id)

) COMMENT '角色';

/*******************************************************************************
7.用户角色
*******************************************************************************/

DROP TABLE IF EXISTS user_role;
CREATE TABLE IF NOT EXISTS user_role (

	user_id				INT NOT NULL										COMMENT '用户ID',
    role_id				INT NOT NULL										COMMENT '角色ID',
	
	PRIMARY KEY (user_id, role_id)

) COMMENT '用户角色';

/*******************************************************************************
8.功能
*******************************************************************************/

DROP TABLE IF EXISTS func;
CREATE TABLE IF NOT EXISTS func (

	func_id				INT NOT NULL AUTO_INCREMENT							COMMENT '功能ID',
	func_code			VARCHAR(64) NOT NULL								COMMENT '功能代码',
	name			    VARCHAR(64) NOT NULL								COMMENT '名称',
	remark			    VARCHAR(1024) NOT NULL DEFAULT ''					COMMENT '说明',
	order_idx			INT NOT NULL DEFAULT 100							COMMENT '排序(ASC)',
	parent_id			INT NOT NULL DEFAULT 0								COMMENT '父ID',
	
	PRIMARY KEY (func_id),
	UNIQUE KEY (func_code),
	INDEX (parent_id, order_idx)

) COMMENT '功能';

/*******************************************************************************
9.角色授权
*******************************************************************************/

DROP TABLE IF EXISTS role_func;
CREATE TABLE IF NOT EXISTS role_func (

	role_id				INT NOT NULL										COMMENT '角色ID',
	func_id				INT NOT NULL										COMMENT '功能ID',

	is_select			BIT(1) NOT NULL DEFAULT b'0'						COMMENT '是否拥有读取权限',
	is_insert			BIT(1) NOT NULL DEFAULT b'0'						COMMENT '是否拥有添加权限',
	is_update			BIT(1) NOT NULL DEFAULT b'0'						COMMENT '是否拥有更新权限',
	is_delete			BIT(1) NOT NULL DEFAULT b'0'						COMMENT '是否拥有删除权限',

	PRIMARY KEY (role_id, func_id)

) COMMENT '角色授权';

/*******************************************************************************
10.用户日志
*******************************************************************************/

DROP TABLE IF EXISTS user_log;
CREATE TABLE IF NOT EXISTS user_log (

	log_id				INT NOT NULL AUTO_INCREMENT							COMMENT '日志ID',
	
	user_id				INT NOT NULL										COMMENT '用户ID',
	func_id				INT NOT NULL										COMMENT '功能ID',
	ops_type			INT NOT NULL										COMMENT '操作类型(见OpsType枚举)',
	cont				VARCHAR(4096) NOT NULL DEFAULT ''					COMMENT '操作内容',
	create_time			DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP()		COMMENT '操作时间',

	PRIMARY KEY (log_id),
	INDEX (user_id),
	INDEX (func_id),
	INDEX (ops_type)

) COMMENT '用户日志';

/*******************************************************************************
11.工序
*******************************************************************************/

DROP TABLE IF EXISTS ops;
CREATE TABLE IF NOT EXISTS ops (

	ops_id				INT NOT NULL AUTO_INCREMENT							COMMENT '工序ID',
	ops_name			VARCHAR(64) NOT NULL								COMMENT '工序名称',
	ops_val				VARCHAR(64) NOT NULL 								COMMENT '工序值',	
	remark				VARCHAR(1024) NOT NULL DEFAULT ''					COMMENT '备注',
		
	PRIMARY KEY (ops_id)

) COMMENT '工序';

/*******************************************************************************
12.电池测试数据(OCV)
*******************************************************************************/

DROP TABLE IF EXISTS battery;
CREATE TABLE IF NOT EXISTS battery (

	battery_code		VARCHAR(64) NOT NULL								COMMENT '电池条码',
	test_no				INT NOT NULL DEFAULT 1								COMMENT '测试次数',

	ocv					DECIMAL(18, 6) NOT NULL DEFAULT 0					COMMENT '开路电压(mV)',
	shell_v				DECIMAL(18, 6) NOT NULL DEFAULT 0					COMMENT '壳体电压(mV)',
	dv					DECIMAL(18, 6) NOT NULL DEFAULT 0					COMMENT '放电电压(mV)',
	acir				DECIMAL(18, 6) NOT NULL DEFAULT 0					COMMENT '交流内阻(mΩ)',
	dcir				DECIMAL(18, 6) NOT NULL DEFAULT 0					COMMENT '直流内阻(mΩ)',
	temp				DECIMAL(9, 3) NOT NULL DEFAULT 0					COMMENT '环境温度(℃)',
	kval				DECIMAL(18, 6) NOT NULL DEFAULT 0					COMMENT 'K值',

	proc_id				VARCHAR(32) NOT NULL DEFAULT ''						COMMENT '工艺编号',
	ops_id				VARCHAR(32) NOT NULL DEFAULT ''						COMMENT '工序编号',
	proj_id				VARCHAR(32) NOT NULL DEFAULT ''						COMMENT '项目编号',
	batch_id			VARCHAR(32) NOT NULL DEFAULT ''						COMMENT '批次编号',
	battery_type_id		VARCHAR(32) NOT NULL DEFAULT ''						COMMENT '电池型号编号',
	tray_code			VARCHAR(64) NOT NULL DEFAULT ''						COMMENT '托盘条码',
	create_time			DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP()		COMMENT '创建时间',
	result				INT NOT NULL DEFAULT 0 								COMMENT '测试结果(见BatteryResult枚举)',
	result_time			DATETIME NULL										COMMENT '结果时间',
	
	PRIMARY KEY (battery_code, test_no),
	INDEX (create_time)

) COMMENT '电池测试数据';

/*******************************************************************************
13.电池自定义结果代码(OCV)
*******************************************************************************/

DROP TABLE IF EXISTS battery_result_code;
CREATE TABLE IF NOT EXISTS battery_result_code (

	result_code			VARCHAR(64) NOT NULL								COMMENT '结果代码',
	custom_code			VARCHAR(64) NOT NULL DEFAULT ''						COMMENT '自定义代码',
	remark				VARCHAR(1024) NOT NULL DEFAULT ''					COMMENT '备注',
	
	PRIMARY KEY (result_code)

) COMMENT '电池自定义结果代码';

/*******************************************************************************
14.工位(PIEF)
*******************************************************************************/

DROP TABLE IF EXISTS station;
CREATE TABLE IF NOT EXISTS station (

	station_id			VARCHAR(32) NOT NULL								COMMENT '工位ID',
	name				VARCHAR(32) NOT NULL								COMMENT '名称(用于显示)',
	type				INT NOT NULL										COMMENT '类型(见StationType枚举)',
	location			INT NOT NULL DEFAULT 0								COMMENT '位置序号(起始1递增1,近上下料水车值小)',
	remark				VARCHAR(1024) NOT NULL DEFAULT ''					COMMENT '备注',

	is_disable			BIT(1) NOT NULL DEFAULT b'0'						COMMENT '是否停用',
	is_lock_fire		BIT(1) NOT NULL DEFAULT b'0'						COMMENT '是否消防锁定',
	is_lock_real		BIT(1) NOT NULL DEFAULT b'0'						COMMENT '是否实时交互锁定',
	real_fail_times		INT NOT NULL DEFAULT 0								COMMENT '实时交互失败次数',
	status				INT NOT NULL DEFAULT 0								COMMENT '工位状态(见StationStatus枚举)',
	status_time			DATETIME NOT NULL									COMMENT '工位状态变更时间',
	
	PRIMARY KEY (station_id),
	INDEX (type, location),
	INDEX (status)

) COMMENT '工位';

/*******************************************************************************
15.工位单元(PIEF)
*******************************************************************************/

DROP TABLE IF EXISTS station_cell;
CREATE TABLE IF NOT EXISTS station_cell (

	station_id			VARCHAR(32) NOT NULL								COMMENT '工位ID',
	cell_no				INT NOT NULL										COMMENT '单元编号(起始1递增1)',

	battery_code		VARCHAR(32) NOT NULL DEFAULT ''						COMMENT '单元绑定电池',
	battery_time		DATETIME NULL										COMMENT '电池绑定时间',
	is_fake				BIT(1) NOT NULL DEFAULT b'0'						COMMENT '是否为假电池',
	
	PRIMARY KEY (station_id, cell_no),
	INDEX (battery_code)

) COMMENT '工位单元';