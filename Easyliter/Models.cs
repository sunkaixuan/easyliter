﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EL
{
    public class El_Queryable<T>
    {
        public List<EL_QueryItem> QueryItemList { get; set; }
        public Easyliter e { get; set; }
    }

    public class EL_QueryItem
    {
        public string TableName { get; set; }
        public string SelectRow { get; set; }
        public List<string> AppendValues { get; set; }
        public string OrderBy { get; set; }
        public int? Take { get; set; }
        public int? Skip { get; set; }
    }
    /// <summary>
    /// 排序类型
    /// </summary>
    public enum El_Sort
    {
        asc = 0,
        desc = 1
    }
}