using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrashCollector3.Models
{
    public class CustomerAddress
    {
        [Key]
        public int CustomerAddressID { get; set; }

        public int AddressID { get; set; }

        public int CustomerID { get;  set;}
    }
}