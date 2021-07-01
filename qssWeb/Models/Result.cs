using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace qssWeb.Models
{
    public class Result
    {
        [Key]
        public int resultId { set; get; }
        public int qnId { set; get; }
        public string investigatorIp { set; get; }
        public string replyTime { set; get; }
        [NotMapped]
        public ICollection<ResultItem> resultItems { set; get; }

        public void save()
        {
            var db=new QssDBContext();
            var results = db.Results;

            replyTime = DateTime.Now.ToString("s");
            
            results.Add(this);
            db.SaveChanges();
            foreach (var resultItem in resultItems)
            {
                resultItem.resultId = resultId;
                db.ResultItems.Add(resultItem);
            }

            db.SaveChanges();



        }

        public static ICollection<Result> GetResultsByWenjuanId(int wId)
        {
            var db=new QssDBContext();
            var results = db.Results;
            var rs = from result in results where result.qnId == wId select result;
            return rs.ToList();
        }
        
        public static Result GetResultById(int id)
        {
            var db=new QssDBContext();
            var results = db.Results;
            var rs = from result in results where result.resultId == id select result;
            return rs.First();
        }
        
    }
}