﻿namespace SachHayBlog.Core.Models
{
    public abstract class PgaeResultBase
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int RowCount { get; set; }

        public int PageCount
        {
            get
            {
                var PageCount = (double)RowCount / PageSize;
                return (int)Math.Ceiling(PageCount);
            }
            set
            {
                if (value <= 0) throw new ArgumentOutOfRangeException(nameof(value));
                PageCount = value;
            }
        }
        public int FristRowOnPage => (CurrentPage - 1) * PageSize + 1;
        public int LastRowOnPage => Math.Min(CurrentPage * PageSize, RowCount);
        public string? AddionalData { get; set;}
        }

    }
}
