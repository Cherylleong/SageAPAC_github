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
	/// Description of INCOME_NAME.
	/// </summary>
	public class INCOME_NAME
	{
		public INCOME_NAME()
		{
		}
		
		public GL_ACCOUNT LinkedAccount = new GL_ACCOUNT();
		public string Income { get; set; }
		public string Name { get; set; }
	}
}
