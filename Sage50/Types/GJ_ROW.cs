/*
 * Created by Ranorex
 * User: wonda05
 * Date: 10/24/2017
 * Time: 2:21 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace Sage50.Types
{
    /// <summary>
    /// Description of GJ_ROW.
    /// </summary>
    public class GJ_ROW
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public GJ_ROW()
        {
            // Do not delete - a parameterless constructor is required!
        }

        
        public GJ_ROW(GL_ACCOUNT Account, string debitAmt, string creditAmt)
        {
            this.Account = Account;
            this.debitAmt = debitAmt;
            this.creditAmt = creditAmt;
        }

		public GL_ACCOUNT Account = new GL_ACCOUNT();
		public string debitAmt { get; set; }			// debit amount. 2nd field of container
		public string creditAmt { get; set; }		// credit amount. 3rd field of container
		public string lineComment { get; set; }	// comment in each detail line. 4th field of container
		public Nullable<bool> alloFlag { get; set; }
        public List<PROJECT_ALLOCATION> Projects = new List<PROJECT_ALLOCATION>();
        
        
    }
}
