using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ALCT.Wechat.Mini.Program.Models
{
    [Table("member")]
    public class Member
    {
        [Column("id")]
        public int Id {get; set;}

        [Column("open_id")]
        public string OpenId {get; set;}

        [Column("driver_identification")]
        public string DriverIdentification {get; set;}

        [Column("created_date")]
        public DateTime CreatedDate {get; set;}
    }
}