﻿using Microsoft.Extensions.Logging;
using XmasBlazor.Shared.Configuration;

namespace XmasBlazor.Shared.Concretes
{
	public abstract class BaseService
	{
		protected ILogger Logger;
		protected AppConfiguration AppConfiguration;

		protected BaseService(ILoggerFactory loggerFactory,
			AppConfiguration appConfiguration)
		{
			AppConfiguration = appConfiguration;
			Logger = loggerFactory.CreateLogger(GetType());
		}
	}
}