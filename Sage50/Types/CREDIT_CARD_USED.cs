/*
 * Created by Ranorex
 * User: wonga01
 * Date: 9/6/2016
 * Time: 10:52
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Sage50.Types
{
	/// <summary>
	/// Description of CREDIT_CARD_USED.
	/// </summary>
	public class CREDIT_CARD_USED
	{
		public CREDIT_CARD_USED()
		{
		}
		
		public GL_ACCOUNT PayableAccount = new GL_ACCOUNT();
		public GL_ACCOUNT ExpenseAccount = new GL_ACCOUNT();
		public string CardName { get; set; }
	}
}
