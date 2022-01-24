using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Gateways.Domaine
{
    public class Gateway//: IGateway
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string SerialNumber { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [RegularExpression("^((25[0-5])|(2[0-4][0-9])|(1[0-9]{2})|([1-9][0-9]?))(\\.((25[0-5])|(2[0-4][0-9])|(1[0-9]{2})|([0-9][0-9]?))){3}$|localhost",
            ErrorMessage = "Incorrect IP Address.")]
        public string IPAddress { get; set; }
        
        [MaxLength(10, ErrorMessage = "Limit exceded max peripheral devices supported.")]
        public virtual ICollection<PeripheralDevice> PeripheralDevices { get; set; }

        public Gateway()
        {
            //PeripheralDevices = new HashSet<PeripheralDevice>();
        }
    }
}
