using System;
using System.ComponentModel.DataAnnotations;

namespace qssWeb.Models
{
    public class Questionnaire
    {
        public string title { get; set; }
        public string description { get; set; }
        public int userId { get; set; }
        [Key]
        public int qnId { get; set; }
        
        public string createTime { set; get; }
        public string released { set; get; }
    }
}