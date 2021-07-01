using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web.DynamicData;

namespace qssWeb.Models
{
    public class Wenjuan
    {
        public string title { get; set; }
        public string description { get; set; }
        public Questionnaire Questionnaire { get; set; }
        public int userId { get; set; }
        [Key] public int qnId { get; set; }
        [NotMapped] public ICollection<Question> questions { get; set; }
        [NotMapped] private QssDBContext qssDbContext = new QssDBContext();
        public string createTime { set; get; }
        public string released { set; get; }
        [NotMapped] public int questionCount { set; get; }
        public Wenjuan()
        {
            
        }

        public Wenjuan(Questionnaire questionnaire)
        {
            qnId = questionnaire.qnId;
            description = questionnaire.description;
            title = questionnaire.title;
            userId = questionnaire.userId;
            createTime = questionnaire.createTime;
            released = questionnaire.released;
            questions = new List<Question>();
            Questionnaire = questionnaire;
            
            var questionList = qssDbContext.Questions;
            var results = from question in questionList where question.qnId == qnId select question;
            foreach (var result in results)
            {
                result.LoadOptions();
                questions.Add(result);
            }
        }

        public IEnumerator<Question> GetQuestions()
        {
            var questions = qssDbContext.Questions;
            var list = from question in questions where question.qnId == qnId select question;
            return list.GetEnumerator();
        }

        public void save()
        {
            
            var q = new Questionnaire
            {
                title = title,
                userId = userId,
                description = description
            };
            
            var questionnaireses = qssDbContext.Questionnaires;
            q.createTime=DateTime.Now.ToString("s");
            q.released = "N";
            q = questionnaireses.Add(q);

            qssDbContext.SaveChanges();

            qnId = q.qnId;

            foreach (var question in questions)
            {
                question.qnId = qnId;
                question.save();
            }
        }

        private static QssDBContext db = new QssDBContext();

        public static Wenjuan GetWenjuanById(int id)
        {
            var questionnaires = new QssDBContext().Questionnaires;

            var results = from Questionnaire questionnaire in questionnaires
                where questionnaire.qnId == id
                select questionnaire;
            if (!results.Any())
            {
                return null;
            }
            var result = results.First();

            var wenjuan = new Wenjuan(result);

            return wenjuan;
            //return results.First()
        }

        public static void DeleteWenjuanById(int id)
        {
            var db = new QssDBContext();
            var questionnaires = db.Questionnaires;

            var results = from Questionnaire questionnaire in questionnaires
                where questionnaire.qnId == id
                select questionnaire;
            if (!results.Any())
            {
                return;
            }
            var result = results.First();
            questionnaires.Remove(result);
            db.SaveChanges();
        }

        public static ICollection<Wenjuan> GetWenjuanInfosByUserId(int userId)
        {
            var questionnaires = new QssDBContext().Questionnaires;
            var questionList = new QssDBContext().Questions;
            var results = from Questionnaire questionnaire in questionnaires
                where questionnaire.userId == userId
                select questionnaire;
            ICollection<Wenjuan> wenjuans = new List<Wenjuan>();
            foreach (var result in results)
            {
                var qr = from question in questionList where question.qnId == result.qnId select question;

                var count = qr.Count();
                
                var wenjuan=new Wenjuan()
                {
                    qnId = result.qnId,description = result.description,title = result.title,released = result.released,createTime = result.createTime
                };
                wenjuan.questionCount = count;
                wenjuans.Add(wenjuan);
            }

            return wenjuans;
        }

        public int GetResultCount()
        {

            var results = from r in db.Results where r.qnId == qnId select r;

            return results.Count();
        }
        
    }
}