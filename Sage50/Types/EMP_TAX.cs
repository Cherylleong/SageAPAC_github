/*
 * Created by Ranorex
 * User: wonga01
 * Date: 9/6/2016
 * Time: 13:22
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace Sage50.Types
{
	/// <summary>
	/// Description of EMP_TAX.
	/// </summary>
	public class EMP_TAX
	{
		public EMP_TAX()
		{
		}
		
		public List<EMP_TAX_CREDIT_DATA> TaxCredits = new List<EMP_TAX_CREDIT_DATA>();
		public List<EMP_TAX_HIST> HistoricalAmounts = new List<EMP_TAX_HIST>();
		public string taxTable { get; set; }
		// STRING federalClaim
		// STRING federalClaimSubjectToIndexing
		// STRING provincialClaim
		// STRING provincialClaimSubjectToIndexing
		public string additionalFederalTax { get; set; }
		public Nullable<bool> deductionEICheckBox { get; set; }
		public string eiRate { get; set; }
		public Nullable<bool> deductCPPQPPCheckBox { get; set; }
		public Nullable<bool> deductQpipCheckBox { get; set; }
		// US version
		public string federalTax { get; set; }
		public string stateTax { get; set; }
		public string fedAllowance { get; set; }
		public string fedStatus { get; set; }
		public string stateAllowance1 { get; set; }
		public string stateAllowance2 { get; set; }
		public string stateStatus { get; set; }
		public string dependents { get; set; }
		public Nullable<bool> SocSecCheckbox { get; set; }
		public string dependentClaim { get; set; }
	}
}
