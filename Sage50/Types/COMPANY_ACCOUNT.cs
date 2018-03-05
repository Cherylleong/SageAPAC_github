/*
 * Created by Ranorex
 * User: wonga01
 * Date: 5/19/2017
 * Time: 16:07
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Sage50.Types
{
	/// <summary>
	/// Description of COMPANY_ACCOUNT.
	/// </summary>
	public class COMPANY_ACCOUNT
	{
		public COMPANY_ACCOUNT()
		{
		}
		
		public int accountNumberDigits {get; set;}
		public string startingAssetAccountNumber {get; set;}
		public string endingAssetAccountNumber {get; set;}
		public string startingLiabilityAccountNumber {get; set;}
		public string endingLiabilityAccountNumber {get; set;}
		public string startingEquityAccountNumber {get; set;}
		public string endingEquityAccountNumber {get; set;}
		public string startingRevenueAccountNumber {get; set;}
		public string endingRevenueAccountNumber {get; set;}
		public string startingExpenseAccountNumber {get; set;}
		public string endingExpenseAccountNumber {get; set;}
		
	}
}
