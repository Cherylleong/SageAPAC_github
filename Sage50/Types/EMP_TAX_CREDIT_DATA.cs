/*
 * Created by Ranorex
 * User: wonga01
 * Date: 9/6/2016
 * Time: 13:22
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Sage50.Types
{
	/// <summary>
	/// Description of EMP_TAX_CREDIT_DATA.
	/// </summary>
	public class EMP_TAX_CREDIT_DATA
	{
		public EMP_TAX_CREDIT_DATA()
		{
		}
		
		public string taxCreditType { get; set; }
		public string basicPersonalAmount { get; set; }
		public string indexedAmount { get; set; }
		public string nonIndexedAmount { get; set; }
	}
}
