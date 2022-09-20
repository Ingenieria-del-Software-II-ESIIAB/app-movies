using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppForMovies.Models
{
    public abstract class PaymentMethod
    {
        [Key]
        public virtual int ID
        {
            get;
            set;
        }

        public override bool Equals(object obj)
        {
            return obj is PaymentMethod method &&
                   ID == method.ID;
        }
    }
}
