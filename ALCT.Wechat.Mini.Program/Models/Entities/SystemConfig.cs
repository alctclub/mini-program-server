using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ALCT.Wechat.Mini.Program.Models
{
    [Table("system_config")]
    public class SystemConfig
    {
        public int Id {get; set;}
        public string Key {get; set;}
        public string Value {get; set;}
        public string Desc {get; set;}

        [Column("created_date")]
        public DateTime CreatedDate {get; set;}
    }
}