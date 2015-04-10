using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

// A bare-bones, how many pages can I access per second, test.

namespace PerformanceTester
{
	class Program
	{
		static int n = 0;

		static void Main(string[] args)
		{
			List<Thread> threads = new List<Thread>();

			for (int i = 0; i < 10; i++)
			{
				Thread thread = new Thread(new ParameterizedThreadStart(RunForOneSecond));
				thread.IsBackground = true;
				threads.Add(thread);
			}

			threads.ForEach(t => t.Start());

			Thread.Sleep(1250);

			Console.WriteLine("Made {0} requests.", n);
			Console.WriteLine("Press ENTER to exit.");
			Console.ReadLine();
		}

		static void RunForOneSecond(object state)
		{
			DateTime now = DateTime.Now;
			WebClient client = new WebClient();
			client.Proxy = null;

			try
			{
				while ((DateTime.Now - now).TotalMilliseconds < 1000)
				{
					Interlocked.Increment(ref n);
					string downloadString = client.DownloadString("http://192.168.1.21/");
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}
	}
}
