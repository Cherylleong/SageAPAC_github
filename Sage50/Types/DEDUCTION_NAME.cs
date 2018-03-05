/*
 * Created by Ranorex
 * User: wonga01
 * Date: 9/6/2016
 * Time: 13:06
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Sage50.Types
{
	/// <summary>
	/// Description of DEDUCTION_NAME.
	/// </summary>
	public class DEDUCTION_NAME
	{
		public DEDUCTION_NAME()
		{
		}
		
		public GL_ACCOUNT LinkedAccount = new GL_ACCOUNT();
		public string Deduction { get; set; }
		public string Name { get; set; }
	}
}
