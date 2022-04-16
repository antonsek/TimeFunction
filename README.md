# TimeFunction
Time Entry Integration in .net Framework 4.8

Azure Function for parse Data - "CreateTimeEntry".

Http Trigger Function.

For Trigger You can use Postman.

HttpMethodType - POST.

Body content acceptable format

{
  "$schema": "http://json-schema.org/draft-04/schema#",
  "type": "object",
  "properties": {
    "StartOn": {
      "type": "string",
      "format": "date"
    },
    "EndOn": {
      "type": "string",
      "format": "date"
    }
  },
  "required": [
    "StartOn",
    "EndOn"
  ]
}

{"StartOn":"04-20-2022", "EndOn":"04-21-2022"}
Culture Dates is en-US

For Connection to TimeEntry Service.

Change connection string in TimeFunction\TimeFunction\PowerAppsHelper\Helper.cs

Replace your Username=LOGIN;Password=PASSCODE;Url=ENVIROMENTURL

Publish the project to Azure https://docs.microsoft.com/en-us/azure/azure-functions/functions-create-your-first-function-visual-studio#publish-the-project-to-azure
