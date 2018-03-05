/*
 * Created by Ranorex
 * User: wonga01
 * Date: 9/6/2016
 * Time: 12:59
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace Sage50.Types
{
	/// <summary>
	/// Description of PAYROLL_JOB.
	/// </summary>
	public class PAYROLL_JOB
	{
		public PAYROLL_JOB()
		{
		}
		
		public List<EMPLOYEE> EmployeesAssigned = new List<EMPLOYEE>();
		public string Category { get; set; }
		public Nullable<bool> SubmitTimeSlips { get; set; }
		public Nullable<bool> AreSalespersons { get; set; }
		public Nullable<bool> ActiveStatus { get; set; }
	}
}
