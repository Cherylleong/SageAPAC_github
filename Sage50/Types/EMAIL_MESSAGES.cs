/*
 * Created by Ranorex
 * User: wonga01
 * Date: 9/6/2016
 * Time: 11:14
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Sage50.Types
{
	/// <summary>
	/// Description of EMAIL_MESSAGES.
	/// </summary>
	public class EMAIL_MESSAGES
	{
		public EMAIL_MESSAGES()
		{
		}
		
		 public EMAIL_MESSAGES(EMAIL_FORM_TYPE Form, string Message)
        {
            this.Form = Form;
            this.Message = Message;
        }

		public EMAIL_FORM_TYPE Form = new EMAIL_FORM_TYPE();
		public string Message { get; set; }
	}
}
