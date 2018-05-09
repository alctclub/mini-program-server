using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ALCT.Wechat.Mini.Program.Models
{
    [Table("token")]
    public class Token
    {
        [Column("id")]
        public int Id {get; set;}

        [Column("open_id")]
        public string OpenId {get; set;}

        [Column("access_token")]
        public string AccessToken {get; set;}

        [Column("refresh_token")]
        public string RefreshToken {get; set;}
        
        [Column("created_date")]
        public DateTime CreatedDate {get; set;}

        [Column("expiry_date")]
        public DateTime ExpiryDate {get; set;}
    }
}