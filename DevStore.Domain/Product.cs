using System;

namespace DevStore.Domain {

    public class Product {

        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public DateTime AcquiredDate { get; set; }
        public bool IsActive { get; set; }
        public int CategoryId { get; set; }

        //  Virtual - Marca os métodos e propriedades que podem ser extendidos por uma sub-classe, ou seja, 
        //  que permite ter o comportamento alterado através de um override.
        //  Permite que seja sobrescrito em tempo de execução
        public virtual Category Category { get; set; }

        public Product() {
            this.AcquiredDate = DateTime.Now;
        }

        public override string ToString() {
            return this.Title;
        }
    }
}
