using System.Web.Http;
using Owin;

namespace RtlAPI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //GlobalConfiguration.Configuration.User
            //app.UseHangfireDashboard();
            //app.UseHangfireServer();

            //RecurringJob.AddOrUpdate(() => ValuesController.FetchData(), Cron.Hourly);
        }
    }
}