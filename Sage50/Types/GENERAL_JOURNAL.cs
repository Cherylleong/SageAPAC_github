/*
 * Created by Ranorex
 * User: wonda05
 * Date: 10/24/2017
 * Time: 9:55 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace Sage50.Types
{
    /// <summary>
    /// Description of GENERAL_JOURNAL.
    /// </summary>

    public class GENERAL_JOURNAL
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public GENERAL_JOURNAL()
        {
            // Do not delete - a parameterless constructor is required!
        }
        
        public List<GJ_ROW> GridRows = new List<GJ_ROW>();
		public string source { get; set; }			// source code
		public string journalDate { get; set; }		// journal date
		public string comment { get; set; }		// comments
		public string currCode { get; set; }			// currency code
		public string exchRate { get; set; }		// exchange rate
		public string action { get; set; } 			// create, adjust, or store/recall recurring
		public string recurrName { get; set; }		// recurring entry name
		public string recurrFrequency { get; set; }	// recurring frequency


    }
}
