using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerMsgApp.Model
{
    [Table("EmailLogReport")]
    public class EmailLogReport
    {
        [Key]
        public int Id { get; set; }
        public int Type { get; set; }
        [StringLength(100)]
        public string Email { get; set; }
        public string Message { get; set; }
        public DateTime CreateDate { get; set; }

        public EmailLogReport()
        {
            CreateDate = DateTime.Now;
        }
    }
}
