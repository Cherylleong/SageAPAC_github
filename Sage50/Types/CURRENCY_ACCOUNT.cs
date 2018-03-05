/*
 * Created by Ranorex
 * User: wonga01
 * Date: 9/6/2016
 * Time: 11:21
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Sage50.Types
{
	/// <summary>
	/// Description of CURRENCY_ACCOUNT.
	/// </summary>
	public class CURRENCY_ACCOUNT
	{
		public CURRENCY_ACCOUNT()
		{
		}
		
		public GL_ACCOUNT BankAccount = new GL_ACCOUNT();
		public string Currency { get; set; }
	}
}
