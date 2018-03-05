/*
 * Created by Ranorex
 * User: wonga01
 * Date: 9/6/2016
 * Time: 12:55
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace Sage50.Types
{
	/// <summary>
	/// Description of PAYROLL_ENTITLEMENT_SETTINGS.
	/// </summary>
	public class PAYROLL_ENTITLEMENT_SETTINGS
	{
		public PAYROLL_ENTITLEMENT_SETTINGS()
		{
		}
		
		public List<PAYROLL_ENTITLEMENTS> Entitlements = new List<PAYROLL_ENTITLEMENTS>();
		public string numOfHrsInTheWorkDay { get; set; }
	}
}
