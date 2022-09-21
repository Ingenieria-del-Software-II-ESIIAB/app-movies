using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace AppForMovies.API.Models
{
    public class Purchase
    {
        [Key]
        public int PurchaseId
        {
            get;
            set;
        }


        public double TotalPrice
        {
            get;
            set;
        }

        public DateTime PurchaseDate
        {
            get;
            set;
        }


        [DataType(DataType.MultilineText)]
        [Display(Name = "Delivery Address")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please, set your address for delivery")]

        public String DeliveryAddress
        {
            get;
            set;
        }

        public virtual IList<PurchaseItem> PurchaseItems
        {
            get;
            set;
        }
        public Purchase()
        {

            PurchaseItems = new List<PurchaseItem>();
        }
        [ForeignKey("CustomerId")]
        public virtual Customer Customer
        {
            get;
            set;
        }

        //It will be necessary whenever we need a relationship with ApplicationUser or any child class
        public string CustomerId
        {
            get;
            set;
        }

        [Display(Name = "Payment Method")]
        [Required()]
        public PaymentMethod PaymentMethod
        {
            get;
            set;
        }

        public override bool Equals(object obj)
        {
            return obj is Purchase purchase &&
                   PurchaseId == purchase.PurchaseId &&
                   TotalPrice == purchase.TotalPrice &&
                   (this.PurchaseDate.Subtract(purchase.PurchaseDate) < new TimeSpan(0, 1, 0)) &&
                   DeliveryAddress == purchase.DeliveryAddress &&
                   EqualityComparer<Customer>.Default.Equals(Customer, purchase.Customer) &&
                   CustomerId == purchase.CustomerId &&
                   EqualityComparer<PaymentMethod>.Default.Equals(PaymentMethod, purchase.PaymentMethod);
        }
    }

}

