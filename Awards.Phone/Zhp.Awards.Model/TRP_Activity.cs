//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Zhp.Awards.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class TRP_Activity
    {
        public int Id { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public string Description { get; set; }
        public Nullable<bool> DeleteMark { get; set; }
        public Nullable<bool> Enable { get; set; }
        public string ActivityName { get; set; }
        public Nullable<System.DateTime> BeginTime { get; set; }
        public Nullable<System.DateTime> EndTime { get; set; }
        public Nullable<int> TimeLength { get; set; }
        public Nullable<int> Repertory { get; set; }
        public Nullable<int> Use { get; set; }
        public Nullable<int> TotalCount { get; set; }
        public Nullable<int> Expiration { get; set; }
        public Nullable<int> TimeSplit { get; set; }
        public Nullable<System.DateTime> LastEndTime { get; set; }
        public Nullable<int> Recycling { get; set; }
    }
}
