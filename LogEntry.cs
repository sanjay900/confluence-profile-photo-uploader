using System;
using System.Collections.Generic;

namespace VanClinic.Libraries.LogWriter
{
	public class LogEntry
	{
		// Private Variables
		private List<LogEntryItem> logEntryItems = new List<LogEntryItem>();

		// Properties
		public List<LogEntryItem> Items
		{
			get { return logEntryItems; }
			set { logEntryItems = value; }
		}

		// Constructors
		public LogEntry()
		{
		}
	}
}
