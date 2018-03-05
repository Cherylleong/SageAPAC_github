/*
 * Created by Ranorex
 * User: wonga01
 * Date: 5/25/2017
 * Time: 17:12
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Sage50.Types
{
	/// <summary>
	/// Description of RegInc.
	/// </summary>
	public class RegInc
	{
		public RegInc()
		{
		}
		
		public const uint HKEY_CLASSES_ROOT = 0x80000000;
        public const uint HKEY_CURRENT_USER = 0x80000001;
        public const uint HKEY_LOCAL_MACHINE = 0x80000002;
        public const uint HKEY_USERS = 0x80000003;
        public const uint HKEY_PERFORMANCE_DATA = 0x80000004;
        public const uint HKEY_CURRENT_CONFIG = 0x80000005;
        public const uint HKEY_DYN_DATA = 0x80000006;
	}
}
