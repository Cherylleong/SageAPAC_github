/*
 * Created by Ranorex
 * User: wonga01
 * Date: 9/6/2016
 * Time: 13:33
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace Sage50.Types
{
	/// <summary>
	/// Description of EMP_DIRECT_DEP.
	/// </summary>
	public class EMP_DIRECT_DEP
	{
		public EMP_DIRECT_DEP()
		{
		}
		
		public List<EMP_DIRECT_DEP_DATA> DepositAccounts = new List<EMP_DIRECT_DEP_DATA>();
		public Nullable<bool> directDepositCheckBox { get; set; }
	
	}
}
