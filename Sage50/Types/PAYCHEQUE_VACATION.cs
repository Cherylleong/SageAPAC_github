/*
 * Created by Ranorex
 * User: wonga01
 * Date: 10/11/2017
 * Time: 16:31
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace Sage50.Types
{
	/// <summary>
	/// Description of PAYCHEQUE_VACATION.
	/// </summary>
	public class PAYCHEQUE_VACATION
	{
		public PAYCHEQUE_VACATION()
		{
		}
		
		public string vacationType { get; set; }
		public string hours { get; set; }
        public string amount { get; set; }
        public string ytdAmt { get; set; }
	}
}
