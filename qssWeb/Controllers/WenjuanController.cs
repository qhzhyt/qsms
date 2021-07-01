using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using qssWeb.Models;

namespace qssWeb.Controllers
{
    public class WenjuanController : BaseController
    {
        private QssDBContext db = new QssDBContext();

        // GET
        public ActionResult Index()
        {
            Response.Redirect("/User");
            return new ContentResult();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Save()
        {
            var sucess = "{\"status\":1}";
            var sr = new StreamReader(Request.InputStream);
            var stream = sr.ReadToEnd();
            var json = new JavaScriptSerializer();
            var wenjuan = json.Deserialize<Models.Wenjuan>(stream);

            var id = wenjuan.qnId;

            if (id != 0)
            {
                var old = Wenjuan.GetWenjuanById(id);
                if (old.userId != (int) Session["userId"])
                {
                    Response.Redirect("/User/");
                    return null;
                }
                Wenjuan.DeleteWenjuanById(id);
            }
            Console.WriteLine(wenjuan.description);
            wenjuan.userId = (int) Session["userId"];
            wenjuan.save();
            return Json(sucess);
        }

        public ActionResult Edit()
        {
            var id = int.Parse(Request.QueryString["id"]);
            if (id == 0)
            {
                Response.Redirect("/User");
                return null;
            }

            var wenjuan = Wenjuan.GetWenjuanById(id);

            if (wenjuan == null)
            {
                Response.Redirect("/User");
                return null;
            }

            if (!wenjuan.userId.Equals(Session["userId"]))
            {
                Response.Redirect("/User");
                return null;
            }

            var json = new JavaScriptSerializer();

            ViewData["wenjuan"] = json.Serialize(wenjuan);
            return View();
        }

        [HttpPost]
        public JsonResult Edit(FormCollection form)
        {
            var action = form["action"];
            var id = int.Parse(form["id"]);
            var sucess = "{\"status\":1}";
            var fail = "{\"status\":0}";
            if (action == null || id == null)
            {
                return Json(fail);
            }

            var rs = from q in db.Questionnaires where q.qnId == id select q;
            if (!rs.Any())
            {
                return Json(fail);
            }

            switch (action)
            {
                case "release":


                    rs.First().released = "Y";
                    db.SaveChanges();

                    return Json(sucess);
                case "delete":
                    //db.Questionnaires
//                    rs=from q in db.Questionnaires where q.qnId == id select q;
//                    if (!rs.Any())
//                    {
//                        return Json(fail);
//                    }
                    db.Questionnaires.Remove(rs.First());
                    db.SaveChanges();
                    break;
                case "change":
                    break;
                case "stop":
                    rs.First().released = "N";
                    db.SaveChanges();
                    return Json(sucess);
                    break;
            }
            return Json(sucess);
        }
        public ActionResult Result()
        {
            var id = int.Parse(Request.QueryString["id"]);
            if (id == 0)
            {
                Response.Redirect("/User");
                return null;
            }
            var wenjuan = Wenjuan.GetWenjuanById(id);
            if (wenjuan == null)
            {
                Response.Redirect("/User");
                return null;
            }
            if (!wenjuan.userId.Equals(Session["userId"]))
            {
                Response.Redirect("/User");
                return null;
            }
            var qssDbContext=new QssDBContext();
            
            var sql1 = "select count(*) from resultItems where content in (select oId in options where qId=@qId)";
            var sql2 = "select count(*) from resultItems where content=@content";
            var sql3 = "select concat(content,':::',resultId) from resultItems where qId=@qId and content is not null";
            var sql0 = "select count(*) from (select distinct qId,resultId from resultItems where qId=@qId and content is not null) as temp";
            
            foreach (var question in wenjuan.questions)
            {
                var qId = question.qId;
                var re=qssDbContext.Database.SqlQuery<int>(sql0,new SqlParameter[]
                {
                    new SqlParameter("qId",qId)
                });
                question.resultCount = re.First();
                if (question.type!=0)
                {
                    foreach (var option in question.options)
                    {
                        var re1=qssDbContext.Database.SqlQuery<int>(sql2,new SqlParameter[]
                        {
                            new SqlParameter("content",""+option.oId)
                        });
                        //Console.WriteLine(str.First()+","+option.oId);
                        option.count = re1.First();
                    }
                }
                else
                {
                    var re2=qssDbContext.Database.SqlQuery<string>(sql3,new SqlParameter[]
                    {
                        new SqlParameter("qId",qId)
                    });
                    question.results = re2.ToList();
                }
                
                

            }

            //qssDbContext.Database.SqlQuery<string>("");
            var json = new JavaScriptSerializer();

            ViewData["wenjuan"] = json.Serialize(wenjuan);
            ViewData["title"] = wenjuan.title;
            ViewData["replyCount"] = wenjuan.GetResultCount();
            return View();
        }

        public ActionResult ViewResults()
        {
            var id = int.Parse(Request.QueryString["id"]);
            if (id == 0)
            {
                Response.Redirect("/User");
                return null;
            }
            var wenjuan = Wenjuan.GetWenjuanById(id);
            if (wenjuan == null)
            {
                Response.Redirect("/User");
                return null;
            }
            if (!wenjuan.userId.Equals(Session["userId"]))
            {
                Response.Redirect("/User");
                return null;
            }

            var results = Models.Result.GetResultsByWenjuanId(id);

            ViewData["results"] = results;
            ViewData["wName"] = wenjuan.title;

            return View();
        }
        
        public ActionResult ViewResult()
        {
            var id = int.Parse(Request.QueryString["id"]);
            if (id == 0)
            {
                Response.Redirect("/User");
                return null;
            }
            var result = Models.Result.GetResultById(id);
            
            //var wenjuan = Wenjuan.GetWenjuanById(id);
            if (result == null)
            {
                Response.Redirect("/User");
                return null;
            }
            var wenjuan = Wenjuan.GetWenjuanById(result.qnId);
            if (!wenjuan.userId.Equals(Session["userId"]))
            {
                Response.Redirect("/User");
                return null;
            }

            //var results = Models.Result.GetResultsByWenjuanId(id);

            var questions = wenjuan.questions;
            
            var db=new QssDBContext();
            var resultItems = db.ResultItems;

            foreach (var question in questions)
            {
                var rs = from resultItem in resultItems
                    where resultItem.qId == question.qId && resultItem.resultId == result.resultId
                    select resultItem.content;
                question.results = rs.ToList();
                foreach (var option in question.options)
                {
                    option.isChecked = false;
                }
                foreach (var r in rs)
                {
                    if (question.type == 0) continue;
                    foreach (var option in question.options)
                    {
                        if (option.oId==int.Parse(r))
                        {
                            option.isChecked = true;
                        }
                        
                    }
                }
            }

            ViewData["wenjuan"] = wenjuan;
            ViewData["resultId"] = id;

            return View();
        }
        
    }
}