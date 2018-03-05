/*
 * Created by Ranorex
 * User: wonga01
 * Date: 5/19/2017
 * Time: 16:14
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Sage50.Types
{
	/// <summary>
	/// Description of COMPANY.
	/// </summary>
	public class COMPANY
	{
		public COMPANY()
		{
		}
		
		public EDITION edition = new EDITION();
		public COMPANY_INFO companyInformation = new COMPANY_INFO();
		public OWNERSHIP ownership = new OWNERSHIP();
		public INDUSTRY_TYPE industryType = new INDUSTRY_TYPE();
		public COMPANY_ACCOUNT AccountDetails = new COMPANY_ACCOUNT();
		public string companyType {get; set;}
		public string companyNameFile {get; set;}
		public string companyFileLocation {get; set;}
		public ACCOUNT_LIST accountList = new ACCOUNT_LIST();
		
	}
}
