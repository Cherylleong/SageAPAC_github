/*
 * Created by Ranorex
 * User: wonga01
 * Date: 10/13/2017
 * Time: 13:18
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace Sage50.Types
{
	/// <summary>
	/// Description of PAYCHEQUE_RUN.
	/// </summary>
	public class PAYCHEQUE_RUN
	{
		public PAYCHEQUE_RUN()
		{
		}
		
		public GL_ACCOUNT accountPaidFrom = new GL_ACCOUNT();
		public List<PAYCHEQUE> payCheques = new List<PAYCHEQUE>();
		public string department { get; set; }
		public string payPeriodFrequency { get; set; }
		public string chequeNumber { get; set; }
		public string directDepositNumber { get; set; }
		public string periodEndDate { get; set; }
		public string chequeDate { get; set; }
	}
}
