using AOP.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using EasyCaching.Core;
using Microsoft.AspNetCore.Mvc;

namespace MedBook_RazorPages.Resources
{
	class DBLoggingAspect<T> : Aspect<T>, IBeforeAdvice, IAroundAdvice, IAfterAdvice, IAfterThrowAdvice where T : class
	{

		private readonly ILogger<PageModel> _logger;
		private IEasyCachingProvider _easyCachingProvider;
		private IEasyCachingProviderFactory _easyCachingFactory;
		public DBLoggingAspect(IEasyCachingProviderFactory easyCachingFactory, ILogger<PageModel> logger)
		{
			_logger = logger;

			/* Chaching */
			this._easyCachingFactory = easyCachingFactory;
			this._easyCachingProvider = this._easyCachingFactory.GetCachingProvider("redis1");
		}

		public void OnBefore(ExecutionContext context)
		{
			var str = context.ToString();
			_logger.LogInformation("OnBefore timestamp: " + context.StartTimestamp.ToString() + ", UID:" + context.Uid);
			
		}

		public AroundExecutionResult OnAround(ExecutionContext context)
		{
			AroundExecutionResult retAction;
			var chacedList = this._easyCachingProvider.Get<string>("email");
			if (chacedList.IsNull)
			{
				_logger.LogWarning("Warning- " + context.StartTimestamp.ToString() + ", UID:" + context.Uid +"\nDatabase accessed without login.");
			}
			retAction = new AroundExecutionResult
			{
				Proceed = true,
				OverwrittenResult = null
			};
			
			return retAction;
		}

        public void OnAfter(ExecutionContext context, object result)
		{
			_logger.LogInformation("OnAfter timestamp: " + context.StartTimestamp.ToString() + ", UID:" + context.Uid);
		}

		public void OnThrow(ExecutionContext context, Exception exception)
		{
			_logger.LogError("Error received: "+ exception.ToString());
		}
	}
}
