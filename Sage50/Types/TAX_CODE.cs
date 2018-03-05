/*
 * Created by Ranorex
 * User: wonda05
 * Date: 8/5/2016
 * Time: 11:15 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace Sage50.Types
{
	/// <summary>
	/// Description of Tax_code.
	/// </summary>
	public class TAX_CODE
	{

        public TAX_CODE()
        {}

		public TAX_CODE (string code, string description, TAX_USED_IN useIn, List<TAX_DETAIL> TaxDetails)
		{   
            this.code = code;
            this.description = description;
            this.useIn = useIn;
            this.TaxDetails = TaxDetails;
        }
        
        public string code { get; set; }
        public string description { get; set; }
		public TAX_USED_IN useIn = new TAX_USED_IN();
		public List<TAX_DETAIL> TaxDetails = new List<TAX_DETAIL>();
	}
}
