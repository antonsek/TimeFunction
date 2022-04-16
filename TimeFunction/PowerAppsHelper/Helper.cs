using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace TimeFunction.PowerAppsHelper
{
    public static class Helper
    {
        public static string connectionString = "AuthType=OAuth;Username=YOURLOGIN;Password=YOURPASSWORD;Url=ENVIROMENTURL;AppId=51f81489-12ee-4a9e-aaae-a2591f45987d;RedirectUri=app://58145B91-0C36-4500-8554-080854F2AC97;TokenCacheStorePath=c:\\MyTokenCache;LoginPrompt=Auto";
        public static bool IsExistCurrentDate(DateTime date, CrmServiceClient crmContext)
        {
            string fetchXML =
                        @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false' returntotalrecordcount='true'>  
                            <entity name='msdyn_timeentry'>
                                <attribute name='msdyn_start'/>
                                <filter type='and'>   
                                    <condition attribute='msdyn_start' operator='eq' value='" + date.Date.ToString("g", CultureInfo.GetCultureInfo("en-US")) + @"'/>
                                </filter>
                            </entity>
                        </fetch>";

            return crmContext.GetEntityDataByFetchSearchEC(fetchXML).TotalRecordCount > 0;
        }

        public static void CreateTimeEntry(List<DateTime> dates)
        {
            CrmServiceClient crmContext = new CrmServiceClient(connectionString);
            
            if (crmContext != null && crmContext.IsReady)
            {
                foreach (var currentDate in dates)
                {
                    if (!IsExistCurrentDate(currentDate, crmContext))
                    {
                        // Create an msdyn_timeentry record  
                        Dictionary<string, CrmDataTypeWrapper> inData = new Dictionary<string, CrmDataTypeWrapper>();

                        inData.Add("msdyn_start", new CrmDataTypeWrapper(DateTime.SpecifyKind(currentDate, DateTimeKind.Utc).Date, CrmFieldType.CrmDateTime));
                        inData.Add("msdyn_end", new CrmDataTypeWrapper(DateTime.SpecifyKind(currentDate, DateTimeKind.Utc).Date, CrmFieldType.CrmDateTime));
                        
                        crmContext.CreateNewRecord("msdyn_timeentry", inData);
                    }
                }
            }
        }
    }
}
