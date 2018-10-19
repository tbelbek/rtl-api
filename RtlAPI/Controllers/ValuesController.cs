using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using LightInject;
using RtlAPI.Data.Entity;
using RtlAPI.Helper;
using RtlAPI.Services;

namespace RtlAPI.Controllers
{
    public class ValuesController : ApiController
    {
        [Inject]
        public IDataService DataService { get; set; }
        // GET api/values
        [System.Web.Http.Route("get")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }

        [System.Web.Http.HttpGet]
        public JsonResult<string> FetchData()
        {
            var shows = RequestHelper.Get<ConcurrentBag<TvShow>>("http://api.tvmaze.com/shows").Select(t =>
            {
                t.TvMazeId = t.Id;
                return t;
            }).OrderBy(t => t.TvMazeId).Take(20);

            //to not tire the api.
            var ids = shows.Select(t => t.TvMazeId).ToList();

            Parallel.ForEach(ids, t =>
            {
                var crew = RequestHelper.Get<List<CastPerson>>($"http://api.tvmaze.com/shows/{t}/cast");
                var show = shows.FirstOrDefault(p => p.TvMazeId == t);
                if (show == null) return;

                crew = crew.Select(c => { c.ShowId = t; return c; }).ToList();
                show.Crew = crew;
            });
            
            DataService.InsertWithCheck(shows.ToList());
            return Json("Done.");
        }

        [System.Web.Http.HttpGet]
        public JsonResult<List<TvShow>> GetData(int id, int pageCount)
        {
            return Json(DataService.GetListPaginatedWithId(id, pageCount).Data);
        }

    }
}
