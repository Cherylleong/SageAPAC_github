/*
 * Created by Ranorex
 * User: wonga01
 * Date: 9/2/2016
 * Time: 16:20
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace Sage50.Types
{
	/// <summary>
	/// Description of GENERAL_SETTINGS.
	/// </summary>
	public class GENERAL_SETTINGS
	{
		public GENERAL_SETTINGS()
		{
		}
		
		 public GENERAL_SETTINGS(Nullable<bool> budgetRevAndExAccts, BUDGET_FREQUENCY budgetFrequency, GENERAL_NUMBERING Numbering, Nullable<bool> UseDepartmentalAcounting, List<DEPT_ACCT> DepartmentalAccounting, ADDITIONAL_FIELD_NAMES AdditionalFields, GL_ACCOUNT RetainedEarnings, Nullable<bool> RecordRetainedEarningsBalance)
        {
            this.budgetRevAndExAccts = budgetRevAndExAccts;
            this.budgetFrequency = budgetFrequency;
		    this.Numbering = Numbering;
            this.UseDepartmentalAcounting = UseDepartmentalAcounting;
		    this.DepartmentalAccounting = DepartmentalAccounting;
		    this.AdditionalFields = AdditionalFields;
		    this.RetainedEarnings= RetainedEarnings;
		    this.RecordRetainedEarningsBalance  = RecordRetainedEarningsBalance;
        }

        public Nullable<bool> budgetRevAndExAccts { get; set; }
        public BUDGET_FREQUENCY budgetFrequency = new BUDGET_FREQUENCY();
		public GENERAL_NUMBERING Numbering = new GENERAL_NUMBERING();
        public Nullable<bool> UseDepartmentalAcounting { get; set; }
		public List<DEPT_ACCT> DepartmentalAccounting = new List<DEPT_ACCT>();
		public ADDITIONAL_FIELD_NAMES AdditionalFields = new ADDITIONAL_FIELD_NAMES();
		public GL_ACCOUNT RetainedEarnings = new GL_ACCOUNT();
        public Nullable<bool> RecordRetainedEarningsBalance { get; set; }
	}
}
