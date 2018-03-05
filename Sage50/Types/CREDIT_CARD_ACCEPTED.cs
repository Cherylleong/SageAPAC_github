/*
 * Created by Ranorex
 * User: wonga01
 * Date: 9/6/2016
 * Time: 10:53
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Sage50.Types
{
	/// <summary>
	/// Description of CREDIT_CARD_ACCEPTED.
	/// </summary>
	public class CREDIT_CARD_ACCEPTED
	{
		public CREDIT_CARD_ACCEPTED()
		{
		}
		
		public GL_ACCOUNT ExpenseAccount = new GL_ACCOUNT();
		public GL_ACCOUNT AssetAccount = new GL_ACCOUNT();
		public string CardName { get; set; }
		public string Discount { get; set; }
	}
}
