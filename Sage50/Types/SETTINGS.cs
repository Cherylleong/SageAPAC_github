/*
 * Created by Ranorex
 * User: wonga01
 * Date: 9/2/2016
 * Time: 15:39
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Sage50.Types
{
	/// <summary>
	/// Description of SETTINGS.
	/// </summary>
	public class SETTINGS
	{
		public SETTINGS()
		{
		}
		
		public COMPANY_INFO CompanyInformation = new COMPANY_INFO();
		public COMPANY_SETTINGS CompanySettings = new COMPANY_SETTINGS();
		public GENERAL_SETTINGS GeneralSettings = new GENERAL_SETTINGS();
		public PAYABLE_SETTINGS PayableSettings = new PAYABLE_SETTINGS();
		public RECEIVABLE_SETTINGS ReceivableSettings = new RECEIVABLE_SETTINGS();
		public PAYROLL_SETTINGS PayrollSettings = new PAYROLL_SETTINGS();
		public INVENTORY_SETTINGS InventorySettings = new INVENTORY_SETTINGS();
		public PROJECT_SETTINGS ProjectSettings = new PROJECT_SETTINGS();
	}
}
