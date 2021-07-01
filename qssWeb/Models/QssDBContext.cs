using System.Data.Entity;

namespace qssWeb.Models
{
    public class QssDBContext : DbContext
    {
        public QssDBContext()
        { }
        public DbSet<User> Users { get; set; }
        public DbSet<Questionnaire> Questionnaires { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<ResultItem> ResultItems { set; get; }
        
        private static QssDBContext qssDBContext;

        public static QssDBContext GetInstance()
        {
            if (qssDBContext==null)
            {
                qssDBContext=new QssDBContext();
            }

            return qssDBContext;
        }

    }
}