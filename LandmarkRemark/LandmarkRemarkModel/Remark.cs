using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LandmarkRemarkModel
{
    public class Remark
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Note { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }        
        public DateTime CreatedDate { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }
    }
}
