using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace qssWeb.Models
{
    public class Option
    {
        [Key]
        public int oId { set; get; }
        public string optionContent { set; get; }
        public int qId { set; get; }
        [NotMapped] public int count { set; get; }
        [NotMapped] public bool isChecked { set; get; }
        public void save()
        {
            var db = new QssDBContext();
            db.Options.Add(this);
            db.SaveChanges();
        }
    }
}