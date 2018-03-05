/*
 * Created by Ranorex
 * User: wonga01
 * Date: 9/6/2016
 * Time: 13:05
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Sage50.Types
{
	/// <summary>
	/// Description of INCOME_SPECIFIC_ACCOUNTS.
	/// </summary>
	public class INCOME_SPECIFIC_ACCOUNTS
	{
		public INCOME_SPECIFIC_ACCOUNTS()
		{
		}
		
		public GL_ACCOUNT principalBank = new GL_ACCOUNT();
		public GL_ACCOUNT vacation = new GL_ACCOUNT();
		public GL_ACCOUNT advances = new GL_ACCOUNT();
		public GL_ACCOUNT vacationEarnedLinkedAccount = new GL_ACCOUNT();
		public GL_ACCOUNT regularWageLinkedAccount = new GL_ACCOUNT();
		public GL_ACCOUNT ot1LinkedAccount = new GL_ACCOUNT();
		public GL_ACCOUNT ot2LinkedAccount = new GL_ACCOUNT();
	}
}
