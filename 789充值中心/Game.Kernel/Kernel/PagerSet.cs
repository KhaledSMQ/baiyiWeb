namespace Game.Kernel
{
    using Game.Utils;
    using System;
    using System.Data;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class PagerSet
    {
        public PagerSet()
        {
            this.PageIndex = 1;
            this.PageSize = 10;
            this.PageCount = 0;
            this.RecordCount = 0;
            this.PageSet = new DataSet("PagerSet");
        }

        public PagerSet(int pageIndex, int pageSize, int pageCount, int recordCount, DataSet pageSet)
        {
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
            this.PageCount = pageCount;
            this.RecordCount = recordCount;
            this.PageSet = pageSet;
        }

        public bool CheckedPageSet()
        {
            return Validate.CheckedDataSet(this.PageSet);
        }

        public int PageCount { get; set; }

        public int PageIndex { get; set; }

        public DataSet PageSet { get; set; }

        public int PageSize { get; set; }

        public int RecordCount { get; set; }
    }
}

