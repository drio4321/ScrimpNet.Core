using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScrimpNet.Diagnostics
{
	public partial class Log
	{
		public void SetDispatcher(ILogDispatcher dispatcher)
		{

		}

		ILogDispatcher _dispatcher;
		ILogDispatcher ActiveDispatcher
		{
			get
			{
				return _dispatcher;
			}
		}
	}
}
