using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using TimeFunction.model;
using TimeFunction.PowerAppsHelper;

namespace TimeFunction
{
    public static class DateFunction
    {
        [FunctionName("CreateTimeEntry")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)]HttpRequestMessage req)
        {
            TimeEntryRequest requestData = await req.Content.ReadAsAsync<TimeEntryRequest>();

            if (string.Concat(requestData.StartOn) == "" || string.Concat(requestData.EndOn) == "")
                return req.CreateResponse(HttpStatusCode.BadRequest, "Dates is empty");

            DateTime startOn;
            DateTime endOn;

            if (DateTime.TryParse(requestData.StartOn, CultureInfo.CreateSpecificCulture("en-US"), DateTimeStyles.None, out startOn) &&
                DateTime.TryParse(requestData.EndOn, CultureInfo.CreateSpecificCulture("en-US"), DateTimeStyles.None, out endOn))
            {
                List<DateTime> dates = new List<DateTime>();
                for (var date = startOn; date <= endOn; date = date.AddDays(1))
                {
                    dates.Add(date);
                }

                try
                {
                    Helper.CreateTimeEntry(dates);
                }
                catch (Exception ex)
                {
                    return req.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                }
            }
            else
            {
                return req.CreateResponse(HttpStatusCode.BadRequest, "Dates not Valid");
            }
            
            return req.CreateResponse(HttpStatusCode.OK);
        }
    }
}
