using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PostSharp.Aspects;

namespace MedBook_RazorPages.Resources.MOP
{
    [Serializable]
    public class PageSecurityMonitor : OnMethodBoundaryAspect

    {
        [NonSerialized]
        Stopwatch stopWatch;

        public override void OnEntry(MethodExecutionArgs args)

        {
            stopWatch = Stopwatch.StartNew();
            
            var module = args.Instance as PageModel;
            if (module != null)
            {
                var cookie = module.HttpContext?.Request?.Cookies?.ContainsKey("email");
                if (cookie == null || !cookie.Value)
                {
                    args.ReturnValue = new RedirectToPageResult("Index");
                }
            }
            base.OnEntry(args);
        }

        public override void OnExit(MethodExecutionArgs args)

        {
            string method = new
            StackTrace().GetFrame(1).GetMethod().Name;
            string message = string.Format("The method: [{0}] took {1} ms to execute.", method, stopWatch.ElapsedMilliseconds);
            Console.WriteLine(message);
            base.OnExit(args);

        }

    }
}
