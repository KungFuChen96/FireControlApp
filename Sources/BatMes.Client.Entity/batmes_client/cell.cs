//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace BatMes.Client.Entity.batmes_client
{
    using System;
    using System.Collections.Generic;
    
    public partial class cell
    {
        public int cell_id { get; set; }
        public string product_line { get; set; }
        public int type { get; set; }
        public int cell_status { get; set; }
        public Nullable<int> row { get; set; }
        public Nullable<int> col { get; set; }
        public Nullable<int> lay { get; set; }
        public System.DateTime last_update_time { get; set; }
        public string remark { get; set; }
        public string extend_field1 { get; set; }
        public string extend_field2 { get; set; }
        public string extend_field3 { get; set; }
    }
}
