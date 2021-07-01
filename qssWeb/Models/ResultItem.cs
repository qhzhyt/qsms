using System.ComponentModel.DataAnnotations;

namespace qssWeb.Models
{
    public class ResultItem
    {
        [Key]
        public int rpId { set; get; }
        public int qId { set; get; }
        public int resultId { set; get; }
        public string content { set; get; }
    }
}