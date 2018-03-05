/*
 * Created by Ranorex
 * User: wonga01
 * Date: 9/6/2016
 * Time: 11:19
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Sage50.Types
{
	/// <summary>
	/// Description of DEPT_ACCT.
	/// </summary>
	public class DEPT_ACCT
	{
		public DEPT_ACCT()
		{
		}
		
		public string code { get; set; }
		public string description { get; set; }
		public Nullable<bool> ActiveStatus { get; set; }
	}
}
