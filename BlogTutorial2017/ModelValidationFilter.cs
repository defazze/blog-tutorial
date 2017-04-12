using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogTutorial2017
{
    public class ModelValidationFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if(!context.ModelState.IsValid)
            {
                var result = new { Errors = context.ModelState
                                            .SelectMany(m => m.Value.Errors)
                                            .Select(e => e.ErrorMessage)
                                            .ToArray() };

                context.Result = new JsonResult(result);
            }
        }
    }
}
