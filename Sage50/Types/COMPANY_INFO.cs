/*
 * Created by Ranorex
 * User: wonga01
 * Date: 9/2/2016
 * Time: 15:44
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Sage50.Types
{
	/// <summary>
	/// Description of COMPANY_INFO.
	/// </summary>
	public class COMPANY_INFO
	{
		public COMPANY_INFO()
		{
		}
		
		public ADDRESS Address = new ADDRESS();
		public string companyName { get; set; }
		public string businessNum { get; set; }
		public string companyType { get; set; }
		public string fiscalStart { get; set; }
		public string fiscalEnd { get; set; }
		public string earliestTransaction { get; set; }
		public string sessionDate { get; set; }
		public string latestTransaction { get; set; }
		public string federalID { get; set; }
		public string stateID { get; set; }
	}
}
