/*
 * Created by Ranorex
 * User: wonga01
 * Date: 9/6/2016
 * Time: 13:28
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Sage50.Types
{
	/// <summary>
	/// Description of EMP_EXPENSE.
	/// </summary>
	public class EMP_EXPENSE
	{
		public EMP_EXPENSE()
		{
		}
		
		public EMP_EXPENSE_DATA[] ExpenseRows = new EMP_EXPENSE_DATA[5] { new EMP_EXPENSE_DATA(), new EMP_EXPENSE_DATA(), new EMP_EXPENSE_DATA(), new EMP_EXPENSE_DATA(), new EMP_EXPENSE_DATA()};
		public string wcbRate { get; set; }
	}
}
