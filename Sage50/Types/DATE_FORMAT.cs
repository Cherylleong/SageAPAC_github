/*
 * Created by Ranorex
 * User: wonga01
 * Date: 9/6/2016
 * Time: 10:49
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Sage50.Types
{
	/// <summary>
	/// Description of DATE_FORMAT.
	/// </summary>
	public class DATE_FORMAT
	{
		public DATE_FORMAT()
		{
		}
		
		 public DATE_FORMAT(string shortDateFormat, string shortDateSeparator, string longDateFormat, DATE_SHORT_LONG onScreenUse, DATE_SHORT_LONG inReportsUse, DATE_WEEK WeekBeginsOn)
        {
            this.shortDateFormat = shortDateFormat;
            this.shortDateSeparator = shortDateSeparator;
            this.longDateFormat = longDateFormat;
            this.onScreenUse = onScreenUse;
            this.inReportsUse = inReportsUse;
            this.WeekBeginsOn = WeekBeginsOn;
        }

        public string shortDateFormat { get; set; }
        public string shortDateSeparator { get; set; }
        public string longDateFormat { get; set; }
		public DATE_SHORT_LONG onScreenUse = new DATE_SHORT_LONG();
		public DATE_SHORT_LONG inReportsUse = new DATE_SHORT_LONG();
		public DATE_WEEK WeekBeginsOn = new DATE_WEEK();
	}
}
