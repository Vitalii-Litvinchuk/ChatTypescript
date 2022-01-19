using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Message.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
 
        [Required]
        public string Data { get; set; }

        
    }
}
