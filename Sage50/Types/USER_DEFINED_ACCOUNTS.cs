/*
 * Created by Ranorex
 * User: wonga01
 * Date: 9/6/2016
 * Time: 13:09
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Sage50.Types
{
	/// <summary>
	/// Description of USER_DEFINED_ACCOUNTS.
	/// </summary>
	public class USER_DEFINED_ACCOUNTS
	{
		public USER_DEFINED_ACCOUNTS()
		{
		}
		
		public GL_ACCOUNT payable1Acct = new GL_ACCOUNT();
		public GL_ACCOUNT payable2Acct = new GL_ACCOUNT();
		public GL_ACCOUNT payable3Acct = new GL_ACCOUNT();
		public GL_ACCOUNT payable4Acct = new GL_ACCOUNT();
		public GL_ACCOUNT payable5Acct = new GL_ACCOUNT();
		public GL_ACCOUNT expense1Acct = new GL_ACCOUNT();
		public GL_ACCOUNT expense2Acct = new GL_ACCOUNT();
		public GL_ACCOUNT expense3Acct = new GL_ACCOUNT();
		public GL_ACCOUNT expense4Acct = new GL_ACCOUNT();
		public GL_ACCOUNT expense5Acct = new GL_ACCOUNT();
	}
}
