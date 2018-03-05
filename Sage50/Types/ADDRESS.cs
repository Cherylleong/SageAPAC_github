/*
 * Created by Ranorex
 * User: wonda05
 * Date: 8/5/2016
 * Time: 10:52 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using Sage50.Shared;

namespace Sage50.Types
{
	/// <summary>
	/// Description of Address.
	/// </summary>
	public class ADDRESS
    {
        #region Public Properties
        public string contact { get; set; }
        public string street1 { get; set; }
        public string street2 { get; set; }
        public string city { get; set; }
        public string province { get; set; }
        public string provinceCode { get; set; }
        public string postalCode { get; set; }
        public string country { get; set; }
        public string phone1 { get; set; }
        public string phone2 { get; set; }
        public string fax { get; set; }
        public string email { get; set; }
        public string webSite { get; set; }
        #endregion

        /// <summary>
        /// Fill with random information
        /// </summary>
        public void FillRandom()
        {
            contact     = StringFunctions.RandStr("A(6)");
            street1     = StringFunctions.RandStr("A(6)");
            street2     = StringFunctions.RandStr("A(6)");
            city        = StringFunctions.RandStr("A(6)");
            province    = StringFunctions.RandStr("A(6)");
            postalCode  = StringFunctions.RandStr("A(6)");
            country     = StringFunctions.RandStr("A(6)");
            phone1      = StringFunctions.RandStr("9(10)");
            phone2      = StringFunctions.RandStr("9(10)");
            fax         = StringFunctions.RandStr("9(10)");
            email       = StringFunctions.RandStr("A(6)");
            webSite     = StringFunctions.RandStr("A(6)");
        }
    }
}
