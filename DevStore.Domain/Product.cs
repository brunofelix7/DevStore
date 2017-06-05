using System;
using System.ComponentModel.DataAnnotations;

namespace DevStore.Domain {

    public class Product {

        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public DateTime AcquiredDate { get; set; }
        public bool IsActive { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public Product() {
            this.AcquiredDate = DateTime.Now;
        }

        public override string ToString() {
            return this.Title;
        }
    }
}
