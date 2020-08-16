using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RPizza.Shared
{
    public class Address
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Ingrese el Nombre"), MaxLength(100)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Ingrese la Dirección"), MaxLength(100)]
        public string Line1 { get; set; }
        [MaxLength(100)]
        public string Line2 { get; set; }
        [Required(ErrorMessage = "Ingrese la Ciudad"), MaxLength(50)]        
        public string City { get; set; }
        [Required(ErrorMessage = "Ingrese la {0}"), MaxLength(20)]
        public string Region { get; set; }
        [Required(ErrorMessage = "Ingrese el {0}"), MaxLength(20)]
        public string PostalCode { get; set; }
    }
}
