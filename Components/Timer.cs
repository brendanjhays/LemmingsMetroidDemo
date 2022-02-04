using System;
namespace LemmingsMetroid
{
	public class Timer
	{
		public int length;
		public int maxLength;

		public Timer()
		{
		}

		public void Update()
		{
			if (length > 0) length--;
		}
	}
}


