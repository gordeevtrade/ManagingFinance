﻿using System.ComponentModel.DataAnnotations.Schema;

namespace FamilyBudjetAPI
{
    public class FinanceTransaction
    {
        public int Id { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }

        public DateTime Date { get; set; }
        public string Note { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}