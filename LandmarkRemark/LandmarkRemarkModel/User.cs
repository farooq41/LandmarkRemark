using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LandmarkRemarkModel
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }

        public IEnumerable<Remark> Remarks { get; set; }

        [NotMapped]
        public string Token { get; set; }
    }
}
