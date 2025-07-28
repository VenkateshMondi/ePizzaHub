using Azure;
using ePizzaHub.Models.ApiModels.Response;
using System.Text.Json;

namespace ePizzaHub.API.Middleware
{
    public class CommonResponseMiddleware
    {
        private readonly RequestDelegate _next;

        #region --Steps to create a Middleware--
        //Before making any changes in the below it's just a class but when wants to make a class as a middleware
        //we have to create two things
        //1.First of all need to "inject" a "RequestDelegate" in a "constructor" //upto this point it's just a class file
        //Normally this "requestDelegate" obj is named as "next" so changed at all the places
        //2. Now with this next we have to create a method InvokeAsync
        //3. Register the middleware in Program.cs using app.Middleware<MiddlewareName>();
        #endregion

        public CommonResponseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            //await _next(context);
            #region --Starting comments--
            //right now it's not doing anything but need to write my logic inside this middleware
            //to convert all my API responses into a format of an APIResponseModel
            //Instead of writing in each and every layer / controller , I need to write it in a common location is a middleware i.e. this middleware
            #endregion

            var originalBodyStream = context.Response.Body;
            using (var memoryStream = new MemoryStream())//always a API response comes in the form of memory stream
            {
                context.Response.Body = memoryStream;
                try
                {
                    await _next(context);
                    //Logic to convert the Api Response in the desired format

                    if (context.Response.ContentType != null && context.Response.ContentType.Contains("application/json"))
                    {
                        memoryStream.Seek(0, SeekOrigin.Begin);//i.e start reading the data from the begining of the memory stream and memroyStream = getting a value from an API
                        var responseBody = await new StreamReader(memoryStream).ReadToEndAsync();

                        //ApiResponseModel<int> responseFormat = new ApiResponseModel<int>(true, itemCount, "Record Fetched");

                        var responseObj = new ApiResponseModel<object>(
                            success: context.Response.StatusCode >= 200 && context.Response.StatusCode < 299,
                            data: JsonSerializer.Deserialize<object>(responseBody)!,
                            message: "Request completed successfully.");

                        var jsonResponse = JsonSerializer.Serialize(responseObj);
                        context.Response.Body = originalBodyStream;
                        await context.Response.WriteAsync(jsonResponse);

                    }
                }
                catch (Exception ex)
                {
                    context.Response.StatusCode = 500;
                    var errorResponse = new ApiResponseModel<object>(
                            success: false,
                            data: (object)null,
                            message: ex.Message);

                    var jsonResponse = JsonSerializer.Serialize(errorResponse);
                    context.Response.Body = originalBodyStream;
                    await context.Response.WriteAsync(jsonResponse);
                }
            }
        }
    }
}
