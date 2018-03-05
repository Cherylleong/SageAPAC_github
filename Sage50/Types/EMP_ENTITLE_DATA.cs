/*
 * Created by Ranorex
 * User: wonga01
 * Date: 9/6/2016
 * Time: 13:32
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Sage50.Types
{
	/// <summary>
	/// Description of EMP_ENTITLE_DATA.
	/// </summary>
	public class EMP_ENTITLE_DATA
	{
		public EMP_ENTITLE_DATA()
		{
		}
		
		public string entitlementName { get; set; }
		public string percentageHoursWorked { get; set; }
		public string maximumDays { get; set; }
		public Nullable<bool> clearDays { get; set; }
		public string historicalDays { get; set; }
	}
}
