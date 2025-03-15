﻿using System;
using System.Collections.Generic;

namespace Q1.Models
{
    public partial class BookCopy
    {
        public BookCopy()
        {
            BorrowTransactions = new HashSet<BorrowTransaction>();
        }

        public int CopyId { get; set; }
        public int? BookId { get; set; }
        public string? Isbn { get; set; }
        public int? EditionNumber { get; set; }
        public int? PrintYear { get; set; }
        public string? Condition { get; set; }

        public virtual Book? Book { get; set; }
        public virtual ICollection<BorrowTransaction> BorrowTransactions { get; set; }
    }
}
