/*
 * Created by Ranorex
 * User: wonga01
 * Date: 9/6/2016
 * Time: 14:05
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Sage50.Types
{
	/// <summary>
	/// Description of PROJECT_SETTINGS.
	/// </summary>
	public class PROJECT_SETTINGS
	{
		public PROJECT_SETTINGS()
		{
		}
		
		public PROJECT_SETTINGS(Nullable<bool> budgetProjects, BUDGET_FREQUENCY budgetPeriodFrequency, ALLOCATE_PAYROLL payrollAllocationMethod, ALLOCATE_TRANSACTIONS otherAllocationMethod, Nullable<bool> warnIfAllocationIsNotComplete, Nullable<bool> allowAccessToAllocateFieldUsingTab, string ProjectTitle, ADDITIONAL_FIELD_NAMES AdditionalFields)
        {
            this.budgetProjects = budgetProjects;
            this.budgetPeriodFrequency = budgetPeriodFrequency;
            this.payrollAllocationMethod = payrollAllocationMethod;
		    this.otherAllocationMethod = otherAllocationMethod;
            this.warnIfAllocationIsNotComplete = warnIfAllocationIsNotComplete;
            this.allowAccessToAllocateFieldUsingTab = allowAccessToAllocateFieldUsingTab;
            this.ProjectTitle = ProjectTitle;
		    this.AdditionalFields = AdditionalFields;
        }

        public Nullable<bool> budgetProjects { get; set; }
		public BUDGET_FREQUENCY budgetPeriodFrequency = new BUDGET_FREQUENCY();
		public ALLOCATE_PAYROLL payrollAllocationMethod = new ALLOCATE_PAYROLL();
		public ALLOCATE_TRANSACTIONS otherAllocationMethod = new ALLOCATE_TRANSACTIONS();
        public Nullable<bool> warnIfAllocationIsNotComplete { get; set; }
        public Nullable<bool> allowAccessToAllocateFieldUsingTab { get; set; }
        public string ProjectTitle { get; set; }
		public ADDITIONAL_FIELD_NAMES AdditionalFields = new ADDITIONAL_FIELD_NAMES();
	}
}
