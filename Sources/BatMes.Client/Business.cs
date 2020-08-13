using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using MySql.Data.MySqlClient;
using XY.Pager;
using BatMes.Client.Entity.batmes_client;
using System.Threading.Tasks;
using System.Data.Entity;

namespace BatMes.Client
{
    /// <summary>
    /// 业务逻辑
    /// </summary>
    public class Business
    {
        #region 单例

        private static volatile Business mInstance = null;
        private static readonly object syncLock = new Object();

        private Business() { }

        public static Business Instance
        {
            get
            {
                if (mInstance == null)
                {
                    lock (syncLock)
                    {
                        if (mInstance == null)
                            mInstance = new Business();
                    }
                }
                return mInstance;
            }
        }

        #endregion

        #region 系统参数

        /// <summary>
        /// 保存系统参数值
        /// </summary>
        /// <typeparam name="T">指定返回的参数值类型</typeparam>
        /// <param name="parameterID">参数ID</param>
        /// <returns></returns>
        public bool SysParaAddOrUpdate(string paraID, string value)
        {
            if (paraID.Length > 0)
            {
                using (batmes_client_entity entity = new batmes_client_entity())
                {
                    try
                    {
                        var info = entity.sys_para.Where(m => m.para_id == paraID).FirstOrDefault();
                        if (info == null)
                            entity.sys_para.Add(new sys_para { para_id = paraID, para_val = value, remark = string.Empty });
                        else
                            info.para_val = value;
                        entity.SaveChanges();
                        return true;
                    }
                    catch (Exception e)
                    {
                        return false;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 获取系统参数值
        /// </summary>
        /// <param name="parameterID">参数ID</param>
        /// <returns></returns>
        public string SysPara(string paraID)
        {
            if (paraID.Length > 0)
            {
                using (batmes_client_entity entity = new batmes_client_entity())
                {
                    var info = entity.sys_para.Where(m => m.para_id == paraID).FirstOrDefault();
                    if (info != null)
                    {
                        return info.para_val;
                    }
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// 系统参数分类列表
        /// </summary>
        /// <returns></returns>
        public List<sys_para_sort> SysParaSortList()
        {
            using (batmes_client_entity entity = new batmes_client_entity(ConnectionStrings.BATMES_CLIENT_ENTITY))
            {
                return entity.sys_para_sort.OrderBy(o => o.show_idx).ThenBy(t => t.sort_id).ToList();
            }
        }

        /// <summary>
        /// 获取系统参数值(指定类型)
        /// </summary>
        /// <typeparam name="T">指定返回的参数值类型</typeparam>
        /// <param name="paraID">参数ID</param>
        /// <returns></returns>
        public T SysPara<T>(string paraID) where T : IConvertible
        {
            paraID = XY.Text.Trim(paraID);
            if (paraID.Length > 0)
            {
                using (batmes_client_entity entity = new batmes_client_entity(ConnectionStrings.BATMES_CLIENT_ENTITY))
                {
                    var info = entity.sys_para.Where(m => m.para_id == paraID).FirstOrDefault();
                    if (info != null)
                    {
                        try
                        {
                            var typeConverter = TypeDescriptor.GetConverter(typeof(T));
                            if (typeConverter != null)
                            {
                                return (T)typeConverter.ConvertFromString(info.para_val);
                            }
                        }
                        catch // (NotSupportedException)
                        {
                            return default(T);
                        }
                    }
                }
            }

            return default(T);
        }

        /// <summary>
        /// 获取系统参数信息
        /// </summary>
        /// <param name="paraID"></param>
        /// <returns></returns>
        public sys_para SysParaInfo(string paraID)
        {
            if (paraID.Length > 0)
            {
                using (batmes_client_entity entity = new batmes_client_entity(ConnectionStrings.BATMES_CLIENT_ENTITY))
                {
                    return entity.sys_para.Where(m => m.para_id == paraID).FirstOrDefault();
                }
            }
            return null;
        }

        /// <summary>
        /// 所有系统参数列表
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> SysParaList()
        {
            //
            using (batmes_client_entity entity = new batmes_client_entity(ConnectionStrings.BATMES_CLIENT_ENTITY))
            {
                return entity.sys_para.ToDictionary(k => k.para_id, v => v.para_val);
            }
        }

        /// <summary>
        /// 系统参数分页列表
        /// </summary>
        /// <param name="pageSize">页尺寸</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="sort">分类（0：全部）</param>
        /// <returns></returns>
        public IPager<sys_para> SysParaList(int pageSize, int pageIndex, int sort = 0)
        {
            using (batmes_client_entity entity = new batmes_client_entity(ConnectionStrings.BATMES_CLIENT_ENTITY))
            {
                if (sort > 0)
                    return new Pager<sys_para>(entity.sys_para.Where(m => m.is_show == true && m.sort_id == sort)
                        .OrderBy(o => o.show_idx)
                        .ThenBy(t => t.para_id).ToList(), pageIndex, pageSize);
                else
                    return new Pager<sys_para>(entity.sys_para.Where(m => m.is_show == true)
                        .OrderBy(o => o.show_idx)
                        .ThenBy(t => t.para_id).ToList(), pageIndex, pageSize);
            }
        }

        /// <summary>
        /// 编辑系统参数
        /// </summary>
        /// <param name="paraID">参数ID</param>
        /// <param name="paraVal">参数值</param>
        /// <returns></returns>
        public bool SysParaEdit(string paraID, string paraVal)
        {
            if (paraID.Length > 0)
            {
                using (batmes_client_entity entity = new batmes_client_entity(ConnectionStrings.BATMES_CLIENT_ENTITY))
                {
                    var info = entity.sys_para.Where(m => m.para_id == paraID).FirstOrDefault();
                    if (info != null)
                    {
                        info.para_val = paraVal;
                        entity.SaveChanges();
                        return true;
                    }
                }
            }

            return false;
        }

        #endregion

        #region 系统事件

        /// <summary>
        /// 系统事件分页列表
        /// </summary>
        /// <param name="pageSize">页尺寸</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="beginTime">开始时间(yyyy-MM-dd HH:mm:ss)</param>
        /// <param name="endTime">结束时间(yyyy-MM-dd HH:mm:ss)</param>
        /// <param name="level">事件级别(0:全部)</param>
        /// <returns></returns>
        public IPager<sys_event> SysEventList(int pageSize, int pageIndex, DateTime beginTime, DateTime endTime, int level = 0)
        {
            MySqlParameter[] parameters = {
                new MySqlParameter("?in_fields", MySqlDbType.Text),
                new MySqlParameter("?in_tables", MySqlDbType.Text),
                new MySqlParameter("?in_where", MySqlDbType.Text),
                new MySqlParameter("?in_order", MySqlDbType.Text),
                new MySqlParameter("?in_pagesize", MySqlDbType.Int32),
                new MySqlParameter("?in_pageindex", MySqlDbType.Int32),
                new MySqlParameter("?out_rows", MySqlDbType.Int32)
            };

            //查询字段
            parameters[0].Value = "*";
            //表名
            parameters[1].Value = "sys_event";
            //条件
            StringBuilder sbWhere = new StringBuilder("WHERE create_time BETWEEN '" + beginTime.ToString("yyyy-MM-dd HH:mm:ss") + "' AND '" + endTime.ToString("yyyy-MM-dd HH:mm:ss") + "'");
            if (level > 0)
                sbWhere.Append(" AND level = " + level);
            parameters[2].Value = sbWhere.ToString();
            //排序
            parameters[3].Value = "ORDER BY event_id DESC";
            //页尺寸
            parameters[4].Value = pageSize;
            //页索引
            parameters[5].Value = pageIndex;
            //输出记录总数
            parameters[6].Direction = ParameterDirection.Output;

            //构造结果
            List<sys_event> list = new List<sys_event>();
            using (MySqlDataReader dr = XY.NetF.DBUtil.MySql.ExecuteReader(ConnectionStrings.BATMES_CLIENT, CommandType.StoredProcedure, "pager", parameters))
            {
                while (dr.Read())
                {
                    list.Add(new sys_event
                    {
                        event_id = (int)dr["event_id"],
                        title = dr["title"].ToString(),
                        cont = dr["cont"].ToString(),
                        level = (int)dr["level"],
                        create_time = DateTime.Parse(dr["create_time"].ToString())
                    });
                }
            }
            //返回结果
            return new Pager<sys_event>(list, pageIndex, pageSize, (int)parameters[6].Value);
        }

        /// <summary>
        /// 系统事件列表（用于导出）
        /// </summary>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public MySqlDataReader SysEventList(DateTime beginTime, DateTime endTime, int level = 0)
        {
            StringBuilder sb = new StringBuilder($"SELECT * FROM sys_event WHERE create_time BETWEEN '{beginTime.ToString("yyyy-MM-dd HH:mm:ss")}' AND '{endTime.ToString("yyyy-MM-dd HH:mm:ss")}'");
            if (level > 0)
                sb.Append($" AND level = {level}");
            sb.Append(" ORDER BY event_id DESC");

            return XY.NetF.DBUtil.MySql.ExecuteReader(ConnectionStrings.BATMES_CLIENT, CommandType.Text, sb.ToString(), null);
        }

        /// <summary>
        /// 新增系统事件
        /// </summary>
        /// <param name="title">事件标题</param>
        /// <param name="cont">事件内容</param>
        /// <param name="level">事件级别</param>
        /// <returns></returns>
        public bool SysEventAdd(string title, string cont, Enums.SysEventLevel level)
        {
            if (title.Length > 0)
            {
                using (batmes_client_entity entity = new batmes_client_entity(ConnectionStrings.BATMES_CLIENT_ENTITY))
                {
                    entity.sys_event.Add(new sys_event
                    {
                        title = title,
                        cont = cont,
                        level = (int)level,
                        create_time = DateTime.Now
                    });

                    entity.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region 系统日志

        /// <summary>
        /// 系统日志分页列表
        /// </summary>
        /// <param name="pageSize">页尺寸</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="beginTime">开始时间(yyyy-MM-dd HH:mm:ss)</param>
        /// <param name="endTime">结束时间(yyyy-MM-dd HH:mm:ss)</param>
        /// <param name="logType">日志类型(0:全部)</param>
        /// <returns></returns>
        public IPager<logs> LogList(int pageSize, int pageIndex, DateTime beginTime, DateTime endTime, int logType = 0)
        {
            MySqlParameter[] parameters = {
                new MySqlParameter("?in_fields", MySqlDbType.Text),
                new MySqlParameter("?in_tables", MySqlDbType.Text),
                new MySqlParameter("?in_where", MySqlDbType.Text),
                new MySqlParameter("?in_order", MySqlDbType.Text),
                new MySqlParameter("?in_pagesize", MySqlDbType.Int32),
                new MySqlParameter("?in_pageindex", MySqlDbType.Int32),
                new MySqlParameter("?out_rows", MySqlDbType.Int32)
            };

            //查询字段
            parameters[0].Value = "*";
            //表名
            parameters[1].Value = "logs";
            //条件
            StringBuilder sbWhere = new StringBuilder("WHERE create_time BETWEEN '" + beginTime.ToString("yyyy-MM-dd HH:mm:ss") + "' AND '" + endTime.ToString("yyyy-MM-dd HH:mm:ss") + "'");
            if (logType > 0)
                sbWhere.Append(" AND type = " + logType);
            parameters[2].Value = sbWhere.ToString();
            //排序
            parameters[3].Value = "ORDER BY log_id DESC";
            //页尺寸
            parameters[4].Value = pageSize;
            //页索引
            parameters[5].Value = pageIndex;
            //输出记录总数
            parameters[6].Direction = ParameterDirection.Output;

            //构造结果
            List<logs> list = new List<logs>();
            using (MySqlDataReader dr = XY.NetF.DBUtil.MySql.ExecuteReader(ConnectionStrings.BATMES_CLIENT, CommandType.StoredProcedure, "pager", parameters))
            {
                while (dr.Read())
                {
                    list.Add(new logs
                    {
                        log_id = (int)dr["log_id"],
                        title = dr["title"].ToString(),
                        cont = dr["cont"].ToString(),
                        type = (int)dr["type"],
                        create_time = DateTime.Parse(dr["create_time"].ToString())
                    });
                }
            }
            //返回结果
            return new Pager<logs>(list, pageIndex, pageSize, (int)parameters[6].Value);
        }

        /// <summary>
        /// 系统日志列表（用于导出）
        /// </summary>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="logType"></param>
        /// <returns></returns>
        public MySqlDataReader LogList(DateTime beginTime, DateTime endTime, int logType = 0)
        {
            StringBuilder sb = new StringBuilder($"SELECT * FROM logs WHERE create_time BETWEEN '{beginTime.ToString("yyyy-MM-dd HH:mm:ss")}' AND '{endTime.ToString("yyyy-MM-dd HH:mm:ss")}'");
            if (logType > 0)
                sb.Append($" AND type = {logType}");
            sb.Append(" ORDER BY log_id DESC");

            return XY.NetF.DBUtil.MySql.ExecuteReader(ConnectionStrings.BATMES_CLIENT, CommandType.Text, sb.ToString(), null);
        }

        /// <summary>
        /// 新增日志
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="cont">内容</param>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public bool LogAdd(string title, string cont, Enums.LogType type)
        {
            title = XY.Text.Trim(title);
            if (title.Length == 0)
                return false;
            cont = XY.Text.Trim(cont);

            using (batmes_client_entity entity = new batmes_client_entity(ConnectionStrings.BATMES_CLIENT_ENTITY))
            {
                entity.logs.Add(new logs
                {
                    title = title,
                    cont = cont,
                    type = (int)type,
                    create_time = DateTime.Now
                });

                entity.SaveChanges();
                return true;
            }
        }

        #endregion

        #region 电池测试数据

        /// <summary>
        /// 电池测试数据分页列表
        /// </summary>
        /// <param name="pageSize">页尺寸</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="beginTime">开始时间(yyyy-MM-dd HH:mm:ss)</param>
        /// <param name="endTime">结束时间(yyyy-MM-dd HH:mm:ss)</param>
        /// <param name="batteryCode">电池条码（模糊查询）</param>
        /// <returns></returns>
        public IPager<battery> BatteryList(int pageSize, int pageIndex, DateTime beginTime, DateTime endTime, string batteryCode)
        {
            batteryCode = XY.Text.Trim(batteryCode);
            if (batteryCode.Length > 0)
                return BatterySearch(pageSize, pageIndex, batteryCode);
            else
                return BatteryList(pageSize, pageIndex, beginTime, endTime);
        }

        public IPager<battery> BatteryList(int pageSize, int pageIndex, DateTime beginTime, DateTime endTime)
        {
            MySqlParameter[] parameters = {
                new MySqlParameter("?in_fields", MySqlDbType.Text),
                new MySqlParameter("?in_tables", MySqlDbType.Text),
                new MySqlParameter("?in_where", MySqlDbType.Text),
                new MySqlParameter("?in_order", MySqlDbType.Text),
                new MySqlParameter("?in_pagesize", MySqlDbType.Int32),
                new MySqlParameter("?in_pageindex", MySqlDbType.Int32),
                new MySqlParameter("?out_rows", MySqlDbType.Int32)
            };

            //查询字段
            parameters[0].Value = "*";
            //表名
            parameters[1].Value = "battery";
            //条件
            StringBuilder sbWhere = new StringBuilder("WHERE create_time BETWEEN '" + beginTime.ToString("yyyy-MM-dd HH:mm:ss") + "' AND '" + endTime.ToString("yyyy-MM-dd HH:mm:ss") + "'");
            parameters[2].Value = sbWhere.ToString();
            //排序
            parameters[3].Value = "ORDER BY create_time DESC";
            //页尺寸
            parameters[4].Value = pageSize;
            //页索引
            parameters[5].Value = pageIndex;
            //输出记录总数
            parameters[6].Direction = ParameterDirection.Output;

            //构造结果
            List<battery> list = new List<battery>();
            using (MySqlDataReader dr = XY.NetF.DBUtil.MySql.ExecuteReader(ConnectionStrings.BATMES_CLIENT, CommandType.StoredProcedure, "pager", parameters))
            {
                while (dr.Read())
                {
                    list.Add(new battery
                    {
                        battery_code = dr["battery_code"].ToString(),
                        test_no = (int)dr["test_no"],

                        ocv = (decimal)dr["ocv"],
                        shell_v = (decimal)dr["shell_v"],
                        dv = (decimal)dr["dv"],
                        acir = (decimal)dr["acir"],
                        dcir = (decimal)dr["dcir"],
                        temp = (decimal)dr["temp"],
                        kval = (decimal)dr["kval"],

                        create_time = DateTime.Parse(dr["create_time"].ToString()),
                        result = (int)dr["result"],
                        result_time = dr["result_time"].ToString().Length == 0 ? (DateTime?)null : DateTime.Parse(dr["result_time"].ToString())
                    });
                }
            }
            //返回结果
            return new Pager<battery>(list, pageIndex, pageSize, (int)parameters[6].Value);
        }

        public IPager<battery> BatterySearch(int pageSize, int pageIndex, string batteryCode)
        {
            MySqlParameter[] parameters = {
                new MySqlParameter("?in_fields", MySqlDbType.Text),
                new MySqlParameter("?in_tables", MySqlDbType.Text),
                new MySqlParameter("?in_where", MySqlDbType.Text),
                new MySqlParameter("?in_order", MySqlDbType.Text),
                new MySqlParameter("?in_pagesize", MySqlDbType.Int32),
                new MySqlParameter("?in_pageindex", MySqlDbType.Int32),
                new MySqlParameter("?out_rows", MySqlDbType.Int32)
            };

            //查询字段
            parameters[0].Value = "*";
            //表名
            parameters[1].Value = "battery";
            //条件
            parameters[2].Value = $"WHERE battery_code LIKE '%{XY.Security.SqlSafe(batteryCode)}%'";
            //排序
            parameters[3].Value = "ORDER BY create_time DESC";
            //页尺寸
            parameters[4].Value = pageSize;
            //页索引
            parameters[5].Value = pageIndex;
            //输出记录总数
            parameters[6].Direction = ParameterDirection.Output;

            //构造结果
            List<battery> list = new List<battery>();
            using (MySqlDataReader dr = XY.NetF.DBUtil.MySql.ExecuteReader(ConnectionStrings.BATMES_CLIENT, CommandType.StoredProcedure, "pager", parameters))
            {
                while (dr.Read())
                {
                    list.Add(new battery
                    {
                        battery_code = dr["battery_code"].ToString(),
                        test_no = (int)dr["test_no"],

                        ocv = (decimal)dr["ocv"],
                        shell_v = (decimal)dr["shell_v"],
                        dv = (decimal)dr["dv"],
                        acir = (decimal)dr["acir"],
                        dcir = (decimal)dr["dcir"],
                        temp = (decimal)dr["temp"],
                        kval = (decimal)dr["kval"],

                        create_time = DateTime.Parse(dr["create_time"].ToString()),
                        result = (int)dr["result"],
                        result_time = dr["result_time"].ToString().Length == 0 ? (DateTime?)null : DateTime.Parse(dr["result_time"].ToString())
                    });
                }
            }
            //返回结果
            return new Pager<battery>(list, pageIndex, pageSize, (int)parameters[6].Value);
        }

        /// <summary>
        /// 电池测试数据列表（用于导出）
        /// </summary>
        /// <param name="beginTime">开始时间(yyyy-MM-dd HH:mm:ss)</param>
        /// <param name="endTime">结束时间(yyyy-MM-dd HH:mm:ss)</param>
        /// <param name="batteryCode">电池条码（模糊查询）</param>
        /// <returns></returns>
        public MySqlDataReader BatteryList(DateTime beginTime, DateTime endTime, string batteryCode)
        {
            batteryCode = XY.Text.Trim(batteryCode);
            if (batteryCode.Length > 0)
                return BatterySearch(batteryCode);
            else
                return BatteryList(beginTime, endTime);
        }

        public MySqlDataReader BatteryList(DateTime beginTime, DateTime endTime)
        {
            string cmd = $"SELECT * FROM battery WHERE create_time BETWEEN '{beginTime.ToString("yyyy-MM-dd HH:mm:ss")}' AND '{endTime.ToString("yyyy-MM-dd HH:mm:ss")}' ORDER BY create_time DESC";
            return XY.NetF.DBUtil.MySql.ExecuteReader(ConnectionStrings.BATMES_CLIENT, CommandType.Text, cmd, null);
        }

        public MySqlDataReader BatterySearch(string batteryCode)
        {
            string cmd = $"SELECT * FROM battery WHERE battery_code LIKE '%{XY.Security.SqlSafe(batteryCode)}%' ORDER BY create_time DESC";
            return XY.NetF.DBUtil.MySql.ExecuteReader(ConnectionStrings.BATMES_CLIENT, CommandType.Text, cmd, null);
        }

        /// <summary>
        /// 新增电池测试数据(如果已存在修改)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool BatteryAdd(battery battery)
        {
            using (batmes_client_entity entity = new batmes_client_entity(ConnectionStrings.BATMES_CLIENT_ENTITY))
            {
                battery.create_time = DateTime.Now;
                //电芯首次测试
                var info = entity.battery.Where(o => o.battery_code == battery.battery_code).OrderByDescending(o => o.test_no).FirstOrDefault();
                if (info == null)
                {
                    battery.test_no = 1;
                    entity.battery.Add(battery);
                    return entity.SaveChanges() > 0;
                }

                //仅单次测试模式
                bool comIsSingleTest = SysPara<bool>("ComIsSingleTest");
                if (comIsSingleTest)
                {
                    battery.test_no = info.test_no;
                    //因为测试次数为主键，只有先删除再添加
                    entity.battery.Remove(info);
                    entity.battery.Add(battery);
                }
                //多次测试模式
                else
                {
                    //单次测试延时(秒)
                    var comSingleTestDelay = SysPara<double>("comSingleTestDelay");
                    var createTime = info.create_time;
                    //覆盖数据的时间范围等于最后创建的测试数据时间加上延时时间
                    var newTime = createTime.AddSeconds(comSingleTestDelay);
                    //该次创建是否在范围内
                    if (battery.create_time > newTime)
                    {
                        //获取数据库表内该样品的最大测试次数
                        battery.test_no = info.test_no + 1;
                        entity.battery.Add(battery);
                    }
                    else
                    {
                        //以该次测试数据替换最后创建的测试数据
                        battery.test_no = info.test_no;
                        entity.battery.Remove(info);
                        entity.battery.Add(battery);
                    }
                }

                return entity.SaveChanges() > 0;
            }
        }

        #endregion

        #region 工序管理

        /// <summary>
        /// 工序信息
        /// </summary>
        /// <param name="opsID"></param>
        /// <returns></returns>
        public ops OpsInfo(int opsID)
        {
            if (opsID > 0)
            {
                using (batmes_client_entity entity = new batmes_client_entity(ConnectionStrings.BATMES_CLIENT_ENTITY))
                {
                    return entity.ops.Where(m => m.ops_id == opsID).FirstOrDefault();
                }
            }
            return null;
        }

        /// <summary>
        /// 工序分页列表
        /// </summary>
        /// <param name="pageSize">页尺寸</param>
        /// <param name="pageIndex">页索引</param>
        /// <returns></returns>
        public IPager<ops> OpsList(int pageSize, int pageIndex)
        {
            MySqlParameter[] parameters = {
                new MySqlParameter("?in_fields", MySqlDbType.Text),
                new MySqlParameter("?in_tables", MySqlDbType.Text),
                new MySqlParameter("?in_where", MySqlDbType.Text),
                new MySqlParameter("?in_order", MySqlDbType.Text),
                new MySqlParameter("?in_pagesize", MySqlDbType.Int32),
                new MySqlParameter("?in_pageindex", MySqlDbType.Int32),
                new MySqlParameter("?out_rows", MySqlDbType.Int32)
            };

            //查询字段
            parameters[0].Value = "*";
            //表名
            parameters[1].Value = "ops";
            //条件
            parameters[2].Value = string.Empty;
            //排序
            parameters[3].Value = "ORDER BY ops_id DESC";
            //页尺寸
            parameters[4].Value = pageSize;
            //页索引
            parameters[5].Value = pageIndex;
            //输出记录总数
            parameters[6].Direction = ParameterDirection.Output;

            //构造结果
            List<ops> list = new List<ops>();
            using (MySqlDataReader dr = XY.NetF.DBUtil.MySql.ExecuteReader(ConnectionStrings.BATMES_CLIENT, CommandType.StoredProcedure, "pager", parameters))
            {
                while (dr.Read())
                {
                    list.Add(new ops
                    {
                        ops_id = (int)dr["ops_id"],
                        ops_name = dr["ops_name"].ToString(),
                        ops_val = dr["ops_val"].ToString(),
                        remark = dr["remark"].ToString()
                    });
                }
            }
            //返回结果
            return new Pager<ops>(list, pageIndex, pageSize, (int)parameters[6].Value);
        }

        /// <summary>
        /// 所有工序列表
        /// </summary>
        /// <returns></returns>
        public List<ops> OpsList()
        {
            using (batmes_client_entity entity = new batmes_client_entity(ConnectionStrings.BATMES_CLIENT_ENTITY))
            {
                return entity.ops.ToList();
            }
        }

        /// <summary>
        /// 新增工序
        /// </summary>
        /// <param name="name"></param>
        /// <param name="val"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public bool OpsAdd(string name, string val, string remark)
        {
            name = XY.Text.Trim(name);
            val = XY.Text.Trim(val);
            remark = XY.Text.Trim(remark);
            if (name.Length > 0 && val.Length > 0)
            {
                using (batmes_client_entity entity = new batmes_client_entity(ConnectionStrings.BATMES_CLIENT_ENTITY))
                {
                    entity.ops.Add(new ops
                    {
                        ops_name = name,
                        ops_val = val,
                        remark = remark
                    });
                    if (entity.SaveChanges() > 0)
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 编辑工序
        /// </summary>
        /// <param name="opsID"></param>
        /// <param name="name"></param>
        /// <param name="val"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public bool OpsEdit(int opsID, string name, string val, string remark)
        {
            if (opsID > 0)
            {
                name = XY.Text.Trim(name);
                val = XY.Text.Trim(val);
                remark = XY.Text.Trim(remark);

                if (name.Length > 0 && val.Length > 0)
                {
                    using (batmes_client_entity entity = new batmes_client_entity(ConnectionStrings.BATMES_CLIENT_ENTITY))
                    {
                        var info = entity.ops.Where(m => m.ops_id == opsID).FirstOrDefault();
                        if (info != null)
                        {
                            info.ops_name = name;
                            info.ops_val = val;
                            info.remark = remark;
                            entity.SaveChanges();
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 删除工序
        /// </summary>
        /// <param name="opsID"></param>
        /// <returns></returns>
        public bool OpsRemove(int opsID)
        {
            if (opsID > 0)
            {
                using (batmes_client_entity entity = new batmes_client_entity(ConnectionStrings.BATMES_CLIENT_ENTITY))
                {
                    var info = entity.ops.Where(m => m.ops_id == opsID).FirstOrDefault();
                    if (info != null)
                    {
                        entity.ops.Remove(info);
                        entity.SaveChanges();
                        return true;
                    }
                }
            }
            return false;
        }

        #endregion

        #region 结果代码

        /// <summary>
        /// 结果代码信息
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public battery_result_code BatteryResultCodeInfo(string code)
        {
            if (code.Length > 0)
            {
                using (batmes_client_entity entity = new batmes_client_entity(ConnectionStrings.BATMES_CLIENT_ENTITY))
                {
                    return entity.battery_result_code.Where(m => m.result_code == code).FirstOrDefault();
                }
            }
            return null;
        }

        /// <summary>
        /// 结果代码分页列表
        /// </summary>
        /// <param name="pageSize">页尺寸</param>
        /// <param name="pageIndex">页索引</param>
        /// <returns></returns>
        public IPager<battery_result_code> BatteryResultCodeList(int pageSize, int pageIndex)
        {
            MySqlParameter[] parameters = {
                new MySqlParameter("?in_fields", MySqlDbType.Text),
                new MySqlParameter("?in_tables", MySqlDbType.Text),
                new MySqlParameter("?in_where", MySqlDbType.Text),
                new MySqlParameter("?in_order", MySqlDbType.Text),
                new MySqlParameter("?in_pagesize", MySqlDbType.Int32),
                new MySqlParameter("?in_pageindex", MySqlDbType.Int32),
                new MySqlParameter("?out_rows", MySqlDbType.Int32)
            };

            //查询字段
            parameters[0].Value = "*";
            //表名
            parameters[1].Value = "battery_result_code";
            //条件
            parameters[2].Value = string.Empty;
            //排序
            parameters[3].Value = "ORDER BY result_code ASC";
            //页尺寸
            parameters[4].Value = pageSize;
            //页索引
            parameters[5].Value = pageIndex;
            //输出记录总数
            parameters[6].Direction = ParameterDirection.Output;

            //构造结果
            List<battery_result_code> list = new List<battery_result_code>();
            using (MySqlDataReader dr = XY.NetF.DBUtil.MySql.ExecuteReader(ConnectionStrings.BATMES_CLIENT, CommandType.StoredProcedure, "pager", parameters))
            {
                while (dr.Read())
                {
                    list.Add(new battery_result_code
                    {
                        result_code = dr["result_code"].ToString(),
                        custom_code = dr["custom_code"].ToString(),
                        remark = dr["remark"].ToString()
                    });
                }
            }
            //返回结果
            return new Pager<battery_result_code>(list, pageIndex, pageSize, (int)parameters[6].Value);
        }

        /// <summary>
        /// 所有结果代码列表
        /// </summary>
        /// <returns></returns>
        public List<battery_result_code> BatteryResultCodeList()
        {
            using (batmes_client_entity entity = new batmes_client_entity(ConnectionStrings.BATMES_CLIENT_ENTITY))
            {
                return entity.battery_result_code.OrderBy(o => o.result_code).ToList();
            }
        }

        /// <summary>
        /// 新增结果代码
        /// </summary>
        /// <param name="resultCode"></param>
        /// <param name="customCode"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public bool BatteryResultCodeAdd(string resultCode, string customCode, string remark)
        {
            if (resultCode.Length > 0 && customCode.Length > 0)
            {
                using (batmes_client_entity entity = new batmes_client_entity(ConnectionStrings.BATMES_CLIENT_ENTITY))
                {
                    var info = entity.battery_result_code.Where(m => m.result_code == resultCode).FirstOrDefault();
                    if (info == null)
                    {
                        entity.battery_result_code.Add(new battery_result_code
                        {
                            result_code = resultCode,
                            custom_code = customCode,
                            remark = remark
                        });
                        if (entity.SaveChanges() > 0)
                            return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 编辑结果代码
        /// </summary>
        /// <param name="resultCode"></param>
        /// <param name="customCode"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public bool BatteryResultCodeEdit(string resultCode, string customCode, string remark)
        {
            if (resultCode.Length > 0 && customCode.Length > 0)
            {
                using (batmes_client_entity entity = new batmes_client_entity(ConnectionStrings.BATMES_CLIENT_ENTITY))
                {
                    var info = entity.battery_result_code.Where(m => m.result_code == resultCode).FirstOrDefault();
                    if (info != null)
                    {
                        info.custom_code = customCode;
                        info.remark = remark;
                        entity.SaveChanges();
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 删除结果代码
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool BatteryResultCodeRemove(string code)
        {
            if (code.Length > 0)
            {
                using (batmes_client_entity entity = new batmes_client_entity(ConnectionStrings.BATMES_CLIENT_ENTITY))
                {
                    var info = entity.battery_result_code.Where(m => m.result_code == code).FirstOrDefault();
                    if (info != null)
                    {
                        entity.battery_result_code.Remove(info);
                        entity.SaveChanges();
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 获取日志
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public IEnumerable<logs> GetLogs(DateTime startTime, DateTime endTime, string keyWord)
        {
            using (batmes_client_entity entity = new batmes_client_entity())
            {
                return entity.logs.Where(
                    m => m.create_time >= startTime
                    && m.create_time <= endTime
                    && (string.IsNullOrEmpty(keyWord) || m.title.Contains(keyWord) || m.cont.Contains(keyWord))).ToList();
            }
        }
        #endregion

        #region 工位

        /// <summary>
        /// 获取工位信息
        /// </summary>
        /// <param name="stationID">工位ID</param>
        /// <returns></returns>
        public station StationInfo(string stationID)
        {
            if (stationID == null || stationID.Length == 0)
                return null;

            using (batmes_client_entity entity = new batmes_client_entity(ConnectionStrings.BATMES_CLIENT_ENTITY))
            {
                return entity.station.Where(m => m.station_id == stationID).FirstOrDefault();
            }
        }

        /// <summary>
        /// 获取工位单元列表
        /// </summary>
        /// <param name="stationID"></param>
        /// <returns></returns>
        public List<station_cell> StationCellList(string stationID)
        {
            if (stationID == null || stationID.Length == 0)
                return null;

            using (batmes_client_entity entity = new batmes_client_entity(ConnectionStrings.BATMES_CLIENT_ENTITY))
            {
                return entity.station_cell.Where(m => m.station_id == stationID).ToList();
            }
        }

        /// <summary>
        /// 为调度分配一个最佳的目标工位
        /// </summary>
        /// <param name="stationType">目标工位类型</param>
        /// <returns></returns>
        public station StationInfoWithHoistDest(Enums.StationType stationType)
        {
            using (batmes_client_entity entity = new batmes_client_entity(ConnectionStrings.BATMES_CLIENT_ENTITY))
            {
                return (from station in entity.station
                        where station.type == (int)stationType && //匹配工位类型
                        station.status == (int)Enums.StationStatus.Empty && //空
                        station.is_lock_fire == false && //非消防锁定
                        station.is_lock_real == false && //非实时交互锁定
                        station.is_disable == false //非停用
                        select station).OrderBy(m => m.location) //高位置优先级
                        .FirstOrDefault();
            }
        }

        /// <summary>
        /// 向指定工位单元中绑定一个电芯
        /// </summary>
        /// <param name="stationID">工位ID</param>
        /// <param name="cellNo">单元编号</param>
        /// <param name="batteryCode">电芯条码</param>
        /// <param name="isFake">是否为假电芯</param>
        /// <returns></returns>
        public bool StationBatteryCode(string stationID, int cellNo, string batteryCode, bool isFake = false)
        {
            if (stationID == null || stationID.Length == 0 || cellNo <= 0 || batteryCode == null || batteryCode.Length == 0)
                return false;

            using (batmes_client_entity entity = new batmes_client_entity(ConnectionStrings.BATMES_CLIENT_ENTITY))
            {
                var info = entity.station_cell.Where(m => m.station_id == stationID && m.cell_no == cellNo).FirstOrDefault();
                if (info == null)
                    return false;

                info.battery_code = batteryCode;
                info.battery_time = DateTime.Now;
                info.is_fake = isFake;
                entity.SaveChanges();
                return true;
            }
        }

        #endregion

        #region 调度

        /// <summary>
        /// 调度预约一个工位
        /// </summary>
        /// <param name="stationID">工位ID</param>
        /// <returns></returns>
        public bool HoistStationBook(string stationID)
        {
            if (stationID == null || stationID.Length == 0)
                return false;

            using (batmes_client_entity entity = new batmes_client_entity(ConnectionStrings.BATMES_CLIENT_ENTITY))
            {
                var stationInfo = entity.station.Where(m => m.station_id == stationID).FirstOrDefault();
                //上料工位不可预约；非空工位不可预约
                if (stationInfo == null || stationInfo.type == (int)Enums.StationType.Load || stationInfo.status != (int)Enums.StationStatus.Empty)
                    return false;

                stationInfo.status = (int)Enums.StationStatus.Booked;
                stationInfo.status_time = DateTime.Now;
                entity.SaveChanges();
                return true;
            }
        }

        /// <summary>
        /// 下料水车释放（清空）工位
        /// </summary>
        /// <param name="stationID">下料工位ID</param>
        /// <returns></returns>
        public bool HoistStationRelease(string stationID)
        {
            if (stationID == null || stationID.Length == 0)
                return false;

            using (batmes_client_entity entity = new batmes_client_entity(ConnectionStrings.BATMES_CLIENT_ENTITY))
            {
                var stationInfo = entity.station.Where(m => m.station_id == stationID).FirstOrDefault();
                if (stationInfo == null || stationInfo.type != (int)Enums.StationType.Unload)
                    return false;

                //变更工位状态
                stationInfo.status = (int)Enums.StationStatus.Empty;
                stationInfo.status_time = DateTime.Now;
                //清除工位单元绑定的电芯
                entity.Database.ExecuteSqlCommand(@"UPDATE station_cell 
                                                    SET battery_code = '', battery_time = NULL, is_fake = b'0' 
                                                    WHERE station_id = ?station_id",
                                                    new MySqlParameter("?station_id", stationID));

                entity.SaveChanges();
                return true;
            }
        }

        /// <summary>
        /// 完成一个调度动作逻辑
        /// </summary>
        /// <param name="fromStation">起始工位</param>
        /// <param name="toStation">目标工位</param>
        /// <returns></returns>
        public bool HoistMoveFinish(string fromStation, string toStation)
        {
            if (fromStation == null || fromStation.Length == 0 || toStation == null || toStation.Length == 0)
                return false;

            using (batmes_client_entity entity = new batmes_client_entity(ConnectionStrings.BATMES_CLIENT_ENTITY))
            {
                var fromStationInfo = entity.station.Where(m => m.station_id == fromStation).FirstOrDefault();
                var toStationInfo = entity.station.Where(m => m.station_id == toStation).FirstOrDefault();
                if (fromStationInfo == null || toStationInfo == null || fromStationInfo.type == (int)Enums.StationType.Unload || toStationInfo.type == (int)Enums.StationType.Load)
                    return false;

                //转移电芯条码
                var fromCellList = entity.station_cell.Where(m => m.station_id == fromStation).ToList();
                var toCellList = entity.station_cell.Where(m => m.station_id == toStation).ToList();
                if (fromCellList == null || fromCellList.Count == 0 || toCellList == null || toCellList.Count == 0 || fromCellList.Count != toCellList.Count)
                    return false;
                foreach (var fromCell in fromCellList)
                {
                    var toCell = toCellList.Where(m => m.cell_no == fromCell.cell_no).FirstOrDefault();
                    toCell.battery_code = fromCell.battery_code;
                    toCell.is_fake = fromCell.is_fake;
                    toCell.battery_time = DateTime.Now;

                    fromCell.battery_code = string.Empty;
                    fromCell.is_fake = false;
                    fromCell.battery_time = null;
                }

                //变更工位状态
                fromStationInfo.status = (int)Enums.StationStatus.Empty;
                fromStationInfo.status_time = DateTime.Now;

                toStationInfo.status = (int)Enums.StationStatus.Full;
                toStationInfo.status_time = DateTime.Now;

                entity.SaveChanges();
                return true;
            }
        }

        #endregion

        #region 持久化Quartz任务
        /// <summary>
        /// 新增一条任务记录
        /// </summary>
        /// <param name="oneStandy"></param>
        /// <returns></returns>
        public bool AddOneTask(timed_task hasOne)
        {
            using (batmes_client_entity entity = new batmes_client_entity(ConnectionStrings.BATMES_CLIENT_ENTITY))
            {
                entity.timed_task.Add(hasOne);
                return entity.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 获取持久化的任务
        /// </summary>
        /// <param name="vaildFlag">是否只获取未完成的任务</param>
        /// <returns></returns>
        public async Task<List<timed_task>> GetInitStandTaskAsync(bool vaildFlag = false, int? status = null)
        {
            using (batmes_client_entity entity = new batmes_client_entity(ConnectionStrings.BATMES_CLIENT_ENTITY))
            {
                if (vaildFlag)
                {
                    var taskStatus = status;
                    return await entity.timed_task.Where(t => t.task_status == taskStatus).ToListAsync();
                }
                else
                    return await entity.timed_task.ToListAsync();
            }
        }

        /// <summary>
        /// 删除任务
        /// </summary>
        /// <param name="trayCode"></param>
        /// <param name="opsValue"></param>
        /// <param name="cellId"></param>
        public void TaskRemove(string taskId)
        {
            if (string.IsNullOrEmpty(taskId))
                return;
            using (batmes_client_entity entity = new batmes_client_entity(ConnectionStrings.BATMES_CLIENT_ENTITY))
            {
                var hasOne = entity.timed_task.Where(m => m.task_id == taskId).FirstOrDefault();
                if (hasOne != null)
                {
                    entity.timed_task.Remove(hasOne);
                    entity.SaveChanges();
                }
            }
        }

        /// <summary>
        /// 根据库位号删除任务
        /// </summary>
        /// <param name="cellId"></param>
        /// <returns></returns>
        public bool DeleteTaskByCell(int cellId)
        {
            using (batmes_client_entity entity = new batmes_client_entity(ConnectionStrings.BATMES_CLIENT_ENTITY))
            {
                var hasManys = entity.timed_task.Where(t => t.cell_id == cellId);
                if (hasManys.Any())
                {
                    entity.timed_task.RemoveRange(hasManys);
                    return entity.SaveChanges() > 0;
                }
                return false;
            }
        }
        #endregion 静置任务

        #region 库位
        /// <summary>
        /// 获取所有库位信息
        /// </summary>
        /// <returns></returns>
        public async Task<List<cell>> GetCellsAsync()
        {
            using (batmes_client_entity entity = new batmes_client_entity(ConnectionStrings.BATMES_CLIENT_ENTITY))
            {
                return await entity.cell.ToListAsync();
            }
        }

        /// <summary>
        /// 更新库位状态
        /// </summary>
        /// <param name="uptCell"></param>
        /// <returns></returns>
        public bool UpdateCellStatus(cell uptCell)
        {
            using (batmes_client_entity entity = new batmes_client_entity(ConnectionStrings.BATMES_CLIENT_ENTITY))
            {
                var hasOne = entity.cell.Where(t => t.cell_id == uptCell.cell_id).FirstOrDefault();
                if (hasOne != null)
                {
                    hasOne.cell_status = uptCell.cell_status;
                    hasOne.last_update_time = uptCell.last_update_time;
                    //hasOne.remark = uptCell.remark;
                    entity.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// 新增一个库位
        /// </summary>
        /// <param name="insCell"></param>
        /// <returns></returns>
        public bool AddCell(cell insCell)
        {
            using (batmes_client_entity entity = new batmes_client_entity(ConnectionStrings.BATMES_CLIENT_ENTITY))
            {
                var hasOne = entity.cell.Where(t => t.cell_id == insCell.cell_id).FirstOrDefault();
                if (hasOne != null)
                {
                    entity.cell.Remove(hasOne);
                    entity.cell.Add(insCell);
                    entity.SaveChanges();
                    return true;
                }
                else
                {
                    entity.cell.Add(insCell);
                    return entity.SaveChanges() > 0;
                }
            }
        }

        /// <summary>
        /// 通过主键获取库位信息
        /// </summary>
        /// <param name="cellID"></param>
        /// <returns></returns>
        public cell GetCellByID(int cellID)
        {
            using (batmes_client_entity entity = new batmes_client_entity(ConnectionStrings.BATMES_CLIENT_ENTITY))
            {
                return entity.cell.Where(t => t.cell_id == cellID).FirstOrDefault();
            }
        }

        /// <summary>
        /// 通过主键删除库位信息
        /// </summary>
        /// <param name="cellID"></param>
        /// <returns></returns>
        public bool RemoveCellByID(int cellID)
        {
            using (batmes_client_entity entity = new batmes_client_entity(ConnectionStrings.BATMES_CLIENT_ENTITY))
            {
                var hasOne = entity.cell.Where(t => t.cell_id == cellID).FirstOrDefault();
                if (hasOne != null)
                {
                    entity.cell.Remove(hasOne);
                    return entity.SaveChanges() > 0;
                }
            }
            return false;
        }

        /// <summary>
        /// 更新库位信息
        /// </summary>
        /// <param name="uptModel"></param>
        /// <returns></returns>
        public bool AddOrUpdateCell(cell uptModel)
        {
            using (batmes_client_entity entity = new batmes_client_entity(ConnectionStrings.BATMES_CLIENT_ENTITY))
            {
                var hasOne = entity.cell.Where(t => t.cell_id == uptModel.cell_id).FirstOrDefault();
                if (hasOne != null)
                {
                    hasOne.cell_status = uptModel.cell_status;
                    hasOne.type = uptModel.type;
                    hasOne.last_update_time = uptModel.last_update_time;
                    hasOne.remark = uptModel.remark;
                    entity.SaveChanges();
                    return true;
                }
                else
                {
                    entity.cell.Add(uptModel);
                    entity.SaveChanges();
                }
            }
            return false;
        }
        #endregion

        #region 持久化数据模型
        /// <summary>
        /// 根据复合主键获取托盘持久化数据
        /// </summary>
        /// <param name="trayCode"></param>
        /// <param name="cellID"></param>
        /// <returns></returns>
        public tray_map GetTrayMap(string trayCode, int? cellID = null)
        {
            using (batmes_client_entity entity = new batmes_client_entity(ConnectionStrings.BATMES_CLIENT_ENTITY))
            {
                if(cellID != null && cellID > 0)
                    return entity.tray_map.Where(t => t.tray_code == trayCode && t.cell_id == cellID).FirstOrDefault();
                else
                    return entity.tray_map.Where(t => t.tray_code == trayCode).FirstOrDefault();
            }
        }

        /// <summary>
        /// 根据库位ID获取托盘信息
        /// </summary>
        /// <param name="cellID"></param>
        /// <returns></returns>
        public tray_map GetTrayMapByCell(int cellID)
        {
            using (batmes_client_entity entity = new batmes_client_entity(ConnectionStrings.BATMES_CLIENT_ENTITY))
            {
                return entity.tray_map.Where(t => t.cell_id == cellID).FirstOrDefault();
            }
        }

        /// <summary>
        /// 新增托盘映射模型
        /// </summary>
        /// <param name="insModel"></param>
        /// <returns></returns>
        public bool AddTrayMap(tray_map insModel)
        {
            using (batmes_client_entity entity = new batmes_client_entity(ConnectionStrings.BATMES_CLIENT_ENTITY))
            {
                var hasOne = entity.tray_map.Where(t => t.tray_code == insModel.tray_code && t.cell_id == insModel.cell_id).FirstOrDefault();
                if (hasOne != null)
                {
                    entity.tray_map.Remove(hasOne);
                    entity.tray_map.Add(insModel);
                    entity.SaveChanges();
                    return true;
                }
                else
                {
                    entity.tray_map.Add(insModel);
                    return entity.SaveChanges() > 0;
                }
            }
        }

        /// <summary>
        /// 更新托盘映射模型
        /// </summary>
        /// <param name="uptModel"></param>
        /// <returns></returns>
        public bool UpdateTrayMap(tray_map uptModel)
        {
            using (batmes_client_entity entity = new batmes_client_entity(ConnectionStrings.BATMES_CLIENT_ENTITY))
            {
                var hasOne = entity.tray_map.Where(t => t.tray_code == uptModel.tray_code && t.cell_id == uptModel.cell_id).FirstOrDefault();
                if (hasOne != null)
                {
                    hasOne.exec_status = uptModel.exec_status;
                    hasOne.bts_result = uptModel.bts_result;
                    hasOne.last_update_time = uptModel.last_update_time;
                    entity.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 更新托盘模型：如工步文件
        /// </summary>
        /// <param name="uptModel"></param>
        /// <returns></returns>
        public bool UpdateTrayModel(tray_map uptModel)
        {
            using (batmes_client_entity entity = new batmes_client_entity(ConnectionStrings.BATMES_CLIENT_ENTITY))
            {
                var hasOne = entity.tray_map.Where(t => t.cell_id == uptModel.cell_id).FirstOrDefault();
                if (hasOne != null)
                {
                    hasOne.mes_model = uptModel.mes_model;
                    hasOne.last_update_time = uptModel.last_update_time;
                    entity.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="uptModel"></param>
        /// <returns></returns>
        public bool UpdateTrayMapStatus(tray_map uptModel)
        {
            using (batmes_client_entity entity = new batmes_client_entity(ConnectionStrings.BATMES_CLIENT_ENTITY))
            {
                var hasOne = entity.tray_map.Where(t => t.tray_code == uptModel.tray_code && t.cell_id == uptModel.cell_id).FirstOrDefault();
                if (hasOne != null)
                {
                    hasOne.exec_status = uptModel.exec_status;
                    hasOne.last_update_time = uptModel.last_update_time;
                    entity.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 通过工序获取一个持久化信息
        /// </summary>
        /// <param name="opsValue"></param>
        /// <returns></returns>
        public tray_map GetTrayMapByOps(int opsValue)
        {
            using (batmes_client_entity entity = new batmes_client_entity(ConnectionStrings.BATMES_CLIENT_ENTITY))
            {
                return entity.tray_map.Where(t => t.processes == opsValue).FirstOrDefault();
            }
        }

        /// <summary>
        /// 通过主键删除托盘持久化数据
        /// </summary>
        /// <param name="trayCode"></param>
        /// <param name="cellID"></param>
        /// <returns></returns>
        public bool RemoveTrayMap(string trayCode, int? cellID = null)
        {
            using (batmes_client_entity entity = new batmes_client_entity(ConnectionStrings.BATMES_CLIENT_ENTITY))
            {
                var hasOne = default(tray_map);
                if(cellID != null && cellID > 0)
                    hasOne = entity.tray_map.Where(t => t.tray_code == trayCode && t.cell_id == cellID).FirstOrDefault();
                else
                    hasOne = entity.tray_map.Where(t => t.tray_code == trayCode).FirstOrDefault();
                if (hasOne != null)
                {
                    entity.tray_map.Remove(hasOne);
                    return entity.SaveChanges() > 0;
                }
            }
            return false;
        }

        /// <summary>
        /// 根据库位号删除
        /// </summary>
        /// <param name="cellID"></param>
        /// <returns></returns>
        public bool RemoveTrayMapByCell(int cellID)
        {
            using (batmes_client_entity entity = new batmes_client_entity(ConnectionStrings.BATMES_CLIENT_ENTITY))
            {
                var hasManys = entity.tray_map.Where(t => t.cell_id == cellID);
                if (hasManys.Any())
                {
                    entity.tray_map.RemoveRange(hasManys);
                    return entity.SaveChanges() > 0;
                }
            }
            return false;
        }

        /// <summary>
        /// 更新状态通过库位ID
        /// </summary>
        /// <param name="cellID"></param>
        /// <param name="cellStatus"></param>
        /// <returns></returns>
        public bool UptStatusMapByCell(int cellID, int cellStatus)
        {
            using (batmes_client_entity entity = new batmes_client_entity(ConnectionStrings.BATMES_CLIENT_ENTITY))
            {
                var hasManys = entity.tray_map.Where(t => t.cell_id == cellID).FirstOrDefault();
                if(hasManys != null)
                {
                    hasManys.exec_status = cellStatus;
                    hasManys.last_update_time = DateTime.Now;
                    entity.SaveChanges();
                    return true;
                }
            }
            return false;
        }
        #endregion
    }//end class
}