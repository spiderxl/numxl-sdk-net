#region Using Directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NumXLAPI;
#endregion


namespace TestApp
{
  class Program
  {
    /// <summary>
    /// The main console application
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
      NDK_RETCODE nRet = NDK_RETCODE.NDK_FAILED;

      Console.WriteLine("(c) 2009-2017 Spider Financial Corp.");
      Console.WriteLine("All rights reserved.");
      Console.WriteLine();
      Console.WriteLine("Phone:    (312) 324-0366");
      Console.WriteLine("E-Mail:   support@numxl.com");
      Console.WriteLine("Website:  www.numxl.com");
      Console.WriteLine();
      Console.WriteLine("*******************************************************");

      string szMachineId="";
      NumXLAPI.SFLUC.MACHINEID(ref szMachineId);
      Console.WriteLine("HostId: " + szMachineId );
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
