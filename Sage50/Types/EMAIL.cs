/*
 * Created by Ranorex
 * User: wonga01
 * Date: 9/6/2016
 * Time: 10:48
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace Sage50.Types
{
	/// <summary>
	/// Description of EMAIL.
	/// </summary>
	public class EMAIL
	{
		public EMAIL()
		{
		}
		
		public EMAIL(List<EMAIL_MESSAGES> Messages, EMAIL_ATTACH AttachmentFormat)
        {
            this.Messages = Messages;
            this.AttachmentFormat = AttachmentFormat;
        }

		public List<EMAIL_MESSAGES> Messages = new List<EMAIL_MESSAGES>();
		public EMAIL_ATTACH AttachmentFormat = new EMAIL_ATTACH();
	}
}
