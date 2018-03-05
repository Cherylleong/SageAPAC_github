/*
 * Created by Ranorex
 * User: wonga01
 * Date: 9/6/2016
 * Time: 13:07
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Sage50.Types
{
	/// <summary>
	/// Description of TAXES_LINKED_ACCOUNTS.
	/// </summary>
	public class TAXES_LINKED_ACCOUNTS
	{
		public TAXES_LINKED_ACCOUNTS()
		{
		}
		
		public GL_ACCOUNT payEI = new GL_ACCOUNT();
		public GL_ACCOUNT payCPP = new GL_ACCOUNT();
		public GL_ACCOUNT payTax = new GL_ACCOUNT();
		public GL_ACCOUNT payWCB = new GL_ACCOUNT();
		public GL_ACCOUNT payEHT = new GL_ACCOUNT();
		public GL_ACCOUNT payQueTax = new GL_ACCOUNT();
		public GL_ACCOUNT payQPP = new GL_ACCOUNT();
		public GL_ACCOUNT payQHSF = new GL_ACCOUNT();
		public GL_ACCOUNT exEI = new GL_ACCOUNT();
		public GL_ACCOUNT exCPP = new GL_ACCOUNT();
		public GL_ACCOUNT exWCB = new GL_ACCOUNT();
		public GL_ACCOUNT exEHT = new GL_ACCOUNT();
		public GL_ACCOUNT exQPP = new GL_ACCOUNT();
		public GL_ACCOUNT exQHSF = new GL_ACCOUNT();
		public GL_ACCOUNT payQPIP = new GL_ACCOUNT();
		public GL_ACCOUNT exQPIP = new GL_ACCOUNT();
		// US
        public GL_ACCOUNT payFIT = new GL_ACCOUNT();
		public GL_ACCOUNT paySIT = new GL_ACCOUNT();
		public GL_ACCOUNT payMedTax = new GL_ACCOUNT();
		public GL_ACCOUNT paySSTax = new GL_ACCOUNT();
		public GL_ACCOUNT payFUTA = new GL_ACCOUNT();
		public GL_ACCOUNT paySUTA = new GL_ACCOUNT();
		public GL_ACCOUNT paySDI = new GL_ACCOUNT();
		public GL_ACCOUNT payLocalTax = new GL_ACCOUNT();
		public GL_ACCOUNT exMedTax = new GL_ACCOUNT();
		public GL_ACCOUNT exSSTax = new GL_ACCOUNT();
		public GL_ACCOUNT exFUTA = new GL_ACCOUNT();
		public GL_ACCOUNT exSUTA = new GL_ACCOUNT();
		public GL_ACCOUNT exSDI = new GL_ACCOUNT();
	}
}
