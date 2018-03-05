/*
 * Created by Ranorex
 * User: wonga01
 * Date: 9/29/2017
 * Time: 14:42
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace Sage50.Types
{
	/// <summary>
	/// Description of PAY_OTHER_ROW.
	/// </summary>
	public class PAY_OTHER_ROW
	{
		public PAY_OTHER_ROW()
		{
		}
		
		public GL_ACCOUNT account = new GL_ACCOUNT();
		public TAX_CODE taxCode = new TAX_CODE();
		public List<PROJECT_ALLOCATION> Projects = new List<PROJECT_ALLOCATION>();
		public string description {get; set;}
		public string amount {get; set;}
		
	}
}
