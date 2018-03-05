/*
 * Created by Ranorex
 * User: wonga01
 * Date: 9/6/2016
 * Time: 10:42
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Sage50.Types
{
	/// <summary>
	/// Description of BACKUP.
	/// </summary>
	public class BACKUP
	{
		public BACKUP()
		{
		}
		
		 public BACKUP(Nullable<bool> displayReminderOnSession, string reminderFrequency, Nullable<bool> displayReminderWhenClosing)
        {
            this.displayReminderOnSession = displayReminderOnSession;
            this.reminderFrequency = reminderFrequency;
            this.displayReminderWhenClosing = displayReminderWhenClosing;
 
        }

		public Nullable<bool> displayReminderOnSession		 { get; set; }
		public string reminderFrequency						 { get; set; }
		public Nullable<bool> displayReminderWhenClosing	 { get; set; }
	}
}
