using System;
using System.IO;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using qssWeb.Models;

namespace qssWeb.Controllers
{
    public class SurveyController : Controller
    {
        // GET
        public ActionResult Index()
        {
            var id = int.Parse(Request.QueryString["id"]);
            
            var wenjuan = Wenjuan.GetWenjuanById(id);
            if (wenjuan==null||wenjuan.released=="N")
            {
                Response.Redirect("/User");
            }
            ViewData["wenjuan"] = wenjuan;
            var json = new JavaScriptSerializer();

            ViewData["questionList"] = json.Serialize(wenjuan.questions);
            return View();
        }
        
        public ActionResult Success()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Submit()
        {
            
            var sr = new StreamReader(Request.InputStream);
            var stream = sr.ReadToEnd();
            var json = new JavaScriptSerializer();

            var result = json.Deserialize<Result>(stream);
            
            result.save();

            //Console.WriteLine(result.qnId);

            if (result.resultId!=0)
            {
                return Json("{status:1}");
            }
            
            return Json("{status:0}");
        }
        
        
    }
}