/*
 * Created by Ranorex
 * User: wonga01
 * Date: 10/11/2017
 * Time: 16:32
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace Sage50.Types
{
	/// <summary>
	/// Description of PAYCHEQUE_DEDUCTIONS.
	/// </summary>
	public class PAYCHEQUE_DEDUCTIONS
	{
		public PAYCHEQUE_DEDUCTIONS()
		{
		}
		
		public string deductionName { get; set; }
		public string Amount { get; set; }
	}
}
