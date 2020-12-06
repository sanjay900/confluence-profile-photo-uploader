using System;
using System.Collections.Generic;


namespace VanClinic.Libraries.LogWriter
{
	public class LogEntryItem
	{
		// Private Variables
		private String itemName = String.Empty;
		private String itemValue = String.Empty;

		// Properties
		public String ItemName
		{
			get { return itemName; }
			set { itemName = value; }
		}
		public String ItemValue
		{
			get { return itemValue; }
			set { itemValue = value; }
		}

		// Constructors
		public LogEntryItem()
		{
		}
		public LogEntryItem(String ItemName, String ItemValue)
		{
			itemName = ItemName;
			itemValue = ItemValue;
		}
	}
}
