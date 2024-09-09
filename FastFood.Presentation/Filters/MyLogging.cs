using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace FastFood.Filters
{
    // done for specific calls/controller
    public class MyLogging : Attribute, IActionFilter
    {
        private readonly string _name;

        public MyLogging(string name)
        {
            _name = name;
            
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine("Filter executed before" + _name);
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine("Filter executed after" + _name);
        }

    }
}
