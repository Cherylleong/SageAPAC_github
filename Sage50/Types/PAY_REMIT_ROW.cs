/*
 * Created by Ranorex
 * User: wonga01
 * Date: 9/29/2017
 * Time: 14:50
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Sage50.Types
{
	/// <summary>
	/// Description of PAY_REMIT_ROW.
	/// </summary>
	public class PAY_REMIT_ROW
	{
		public PAY_REMIT_ROW()
		{
		}
		
		public string remitName {get; set;}
		public string amountOwing {get; set;}
		public string adjustAccount {get; set;}
		public string adjustment {get; set;}
		public string amount {get; set;}
		
	}
}
