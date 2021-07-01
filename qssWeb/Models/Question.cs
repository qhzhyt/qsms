using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Net;

namespace qssWeb.Models
{
    public class Question
    {
        [Key]
        public int qId { get; set; }
        public string question { get; set; }
        public int type { get; set; }
                
        public int qnId { get; set; }
        
        public string required { get; set; }
        [NotMapped]
        public ICollection<Option> options { get; set; }
        [NotMapped]
        public ICollection<string> results { get; set; }
        [NotMapped]
        public int resultCount { set; get; }


        public Question()
        {
            
        }
        
        public void save()
        {
            if (required == null)
               return;
            var db=new QssDBContext();
            db.Questions.Add(this);
            db.SaveChanges();
            foreach (var option in options)
            {
                option.qId = qId;
                option.save();
            }
        }

        public void LoadOptions()
        {
            
            options=new List<Option>();
            var optionList = new QssDBContext().Options;

            var results=from option in optionList where option.qId == qId select option;

            foreach (var result in results)
            {
                options.Add(result);
            }

        }
        
        
    }
}