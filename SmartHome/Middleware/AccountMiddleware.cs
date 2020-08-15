using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Middleware{
    public class AccountMiddleware{
        private readonly RequestDelegate requestDelegate;

        public AccountMiddleware(RequestDelegate requestDelegate) {
            this.requestDelegate = requestDelegate;
        }

        public async Task InvokeAsync(HttpContext context) {
            Console.WriteLine("Account Middleware");
            Console.WriteLine(context.Response.StatusCode);
            Console.WriteLine(context.Response.ContentType);
            if (context.Response.StatusCode == 401) {
                context.Response.Redirect("/SmartHome/SignUp");
                return;
            }
            await requestDelegate(context);
        }
    }
}
