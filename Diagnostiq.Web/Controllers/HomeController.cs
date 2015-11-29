using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Chart.Mvc.ComplexChart;
using Diagnostiq.Web.Models;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Diagnostiq.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
            IDatabase database = redis.GetDatabase();
            var values = database.ListRange("counters:cpu");
            List<Moment> moments = new List<Moment>();
            List<double> cpuData = new List<double>();
            List<ComplexDataset> dataset = new List<ComplexDataset>();

            foreach (string value in values)
            {
                Moment moment = JsonConvert.DeserializeObject<Moment>(value);
                moments.Add(moment);

            }
            moments = moments.OrderBy(m => m.TimeStamp).ToList();

            foreach (Moment moment in moments) {
                cpuData.Add(moment.Value);
            }

            ComplexDataset complexDataset = new ComplexDataset() {
                Data = cpuData,
                Label = "My First dataset",
                FillColor = "rgba(220,220,220,0.2)",
                StrokeColor = "rgba(220,220,220,1)",
                PointColor = "rgba(220,220,220,1)",
                PointStrokeColor = "#fff",
                PointHighlightFill = "#fff",
                PointHighlightStroke = "rgba(220,220,220,1)",
            };
            


            ViewBag.Title = "Home Page";

            return View(complexDataset);
        }
    }
}
