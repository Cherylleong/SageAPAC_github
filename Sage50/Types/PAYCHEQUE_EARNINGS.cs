/*
 * Created by Ranorex
 * User: wonga01
 * Date: 10/11/2017
 * Time: 16:28
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace Sage50.Types
{
	/// <summary>
	/// Description of PAYCHEQUE_EARNINGS.
	/// </summary>
	public class PAYCHEQUE_EARNINGS
	{
		public PAYCHEQUE_EARNINGS()
		{
		}
		
		public string earningName { get; set; }
		public string hours { get; set; }
		public string pieces { get; set; }
		public string Amount { get; set; }
	}	
}
