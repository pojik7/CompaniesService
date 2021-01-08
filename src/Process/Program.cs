
using Companies.Container;
using System;
﻿

namespace Process
{
    class Program
    {
		static void Main(string[] args)
		{
			try
			{
				var task = (new CompaniesProcess()).RunAsync(args);
				task.Wait();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				Console.ReadLine();
			}
		}
	
    }
}
