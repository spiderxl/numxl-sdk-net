using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NumXLAPI;

namespace DIFF
{
  class Program
  {
    static private double[] data =  {112,118,132,129,121,135,148,148,136,119,104,118,
                                        			115,126,141,135,125,149,170,170,158,133,114,140,
                                        			145,150,178,163,172,178,199,199,184,162,146,166,
                                        			171,180,193,181,183,218,230,242,209,191,172,194,
                                        			196,196,236,235,229,243,264,272,237,211,180,201,
                                        			204,188,235,227,234,264,302,293,259,229,203,229,
                                        			242,233,267,269,270,315,364,347,312,274,237,278,
                                        			284,277,317,313,318,374,413,405,355,306,271,306,
                                        			315,301,356,348,355,422,465,467,404,347,305,336,
                                        			340,318,362,348,363,435,491,505,404,359,310,337,
                                        			360,342,406,396,420,472,548,559,463,407,362,405,
                                        			417,391,419,461,472,535,622,606,508,461,390,432,
                                        			444,416,472,499,497,579,667,657,557,492,425,481};

    static void Main(string[] args)
    {
      NDK_RETCODE nRet = NDK_RETCODE.NDK_FAILED;

      Console.WriteLine("(c) 2009-2014 Spider Financial Corp.");
      Console.WriteLine("All rights reserved.");
      Console.WriteLine();
      Console.WriteLine("Phone:    (312) 324-0366");
      Console.WriteLine("E-Mail:   support@numxl.com");
      Console.WriteLine("Website:  www.numxl.com");
      Console.WriteLine();
      Console.WriteLine("*******************************************************");

      string szAppName;
      String szMsg;

      szAppName = "TestApp";
      nRet = SFSDK.Init(szAppName, null, null, null);
      if (nRet < NDK_RETCODE.NDK_SUCCESS)
      {
        szMsg = "NDK Initialization Failed";
        SFLOG.LogMsg(SFLOG_LEVEL.SFLOG_INFO, new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName(),
                                                            new System.Diagnostics.StackFrame(1, true).GetMethod().Name, "",
                                                            new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileLineNumber(), szMsg);
        Console.WriteLine(szMsg);
      }
      else
      {
        UIntPtr nCount = (UIntPtr)156;		// Use the 1st 144 observations in the data set

        // Difference the time series by (1,1)

        nRet = (NDK_RETCODE)NumXLAPI.SFSDK.NDK_DIFF(data, nCount, (UIntPtr)1, (UIntPtr)1);

        if (nRet >= NDK_RETCODE.NDK_SUCCESS)
        {
          // SUCCESS
          Console.WriteLine("NDK_DIFF SUCCEEDED.");
        }

        nRet = SFSDK.Shutdown();
        if (nRet < NDK_RETCODE.NDK_SUCCESS)
        {
          szMsg = "NDK Shutdown failed";
          SFLOG.LogMsg(SFLOG_LEVEL.SFLOG_INFO, new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName(),
                                                              new System.Diagnostics.StackFrame(1, true).GetMethod().Name, "",
                                                              new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileLineNumber(), szMsg);
        }
      }
    }
  }
}
