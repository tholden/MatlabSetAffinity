using System;
using System.Diagnostics;

namespace MatlabSetAffinity
{
    class Program
    {
        static void Main( string[ ] args )
        {
            Process[] laProcesses = Process.GetProcessesByName( "MATLAB" );
            DateTime lEarliestStartTime = DateTime.Now;
            long lnEarliestStartID = 0;
            for ( long i = 0; i < laProcesses.LongLength; ++i )
            {
                DateTime lTemp = laProcesses[ i ].StartTime;
                if ( lTemp < lEarliestStartTime )
                {
                    lEarliestStartTime = lTemp;
                    lnEarliestStartID = i;
                }
            }
            ulong j = 3;
            for ( long i = 0; i < laProcesses.LongLength; ++i )
            {
                if ( i == lnEarliestStartID ) continue;
                IntPtr lNewAffinity;
                unchecked
                {
                    lNewAffinity = ( IntPtr ) j;
                }
                laProcesses[ i ].ProcessorAffinity = lNewAffinity;
                unchecked
                {
                    j <<= 2;
                }
                if ( j == 0 ) j = 3;
            }
        }
    }
}
