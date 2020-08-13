using System.Collections.Generic;
using System.Linq;

namespace Tools
{
    public class Pager<T> : List<T>, IPager<T>
    {
        /// <summary>
        /// 使用完整列表分页
        /// </summary>
        /// <param name="all">完整列表</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="pageSize">页尺码</param>
        public Pager(IEnumerable<T> all, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            var list = all as IList<T> ?? all.ToList();
            RecordTotal = list.Count();

            AddRange(list.Skip(RecordBegin - 1).Take(PageSize));
        }

        /// <summary>
        /// 使用当前列表分页
        /// </summary>
        /// <param name="list">当前列表</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="pageSize">页尺码</param>
        /// <param name="recordTotal">记录总数</param>
        public Pager(IEnumerable<T> list, int pageIndex, int pageSize, int recordTotal)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            RecordTotal = recordTotal;

            AddRange(list);
        }

        /// <summary>
        /// 使用完整列表分页
        /// </summary>
        /// <param name="all">完整列表</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="pageSize">页尺码</param>
        public Pager(IQueryable<T> all, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            RecordTotal = all.Count();

            AddRange(all.Skip((PageIndex - 1) * PageSize).Take(PageSize));
        }

        /// <summary>
        /// 使用当前列表分页
        /// </summary>
        /// <param name="list">当前列表</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="pageSize">页尺码</param>
        /// <param name="recordTotal">记录总数</param>
        public Pager(IQueryable<T> list, int pageIndex, int pageSize, int recordTotal)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            RecordTotal = recordTotal;

            AddRange(list);
        }

        /// <summary>
        /// 页索引
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 页尺码
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 页总数
        /// </summary>
        public int PageTotal { get { return (int)System.Math.Ceiling((decimal)RecordTotal / PageSize); } }

        /// <summary>
        /// 记录总数
        /// </summary>
        public int RecordTotal { get; set; }

        /// <summary>
        /// 记录开始索引
        /// </summary>
        public int RecordBegin { get { return (PageIndex - 1) * PageSize + 1; } }

        /// <summary>
        /// 记录结束索引
        /// </summary>
        public int RecordEnd { get { return RecordTotal > PageIndex * PageSize ? PageIndex * PageSize : RecordTotal; } }
    }
}