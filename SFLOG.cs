#region License
// <copyright file="SFLogger.cs" company="Spider Financial Corp">
//  (c) 2007-2014 Spider Financial Corp.
//  All rights reserved.
// </copyright>
//  
//
#endregion

#region Using Directives
using System;
using System.Runtime.InteropServices;
using System.Linq.Expressions;
#endregion


namespace NumXLAPI
{
  /// <summary>
  /// SFLOG_LEVEL defines the supported log levels
  /// </summary>
  public enum SFLOG_LEVEL
  {
    /// <summary> All messages </summary>
    SFLOG_ALL=0,
    /// <summary> All logging messages including trace messages </summary>
    SFLOG_TRACE=1,
    /// <summary> debug, info, warning, error and fatal messages </summary>
    SFLOG_DEBUG=2,
    /// <summary> info, warning, error and fatal messages </summary>
    SFLOG_INFO=3,
    /// <summary> warning, error and fatal messages </summary>
    SFLOG_WARN=4,
    /// <summary> error and fatal messages </summary>
    SFLOG_ERROR=5,
    /// <summary> fatal messages </summary>
    SFLOG_FATAL=6,
    /// <summary> logging is off, no log messages is written to the log-file </summary>
    SFLOG_OFF=7
  }

  /// <summary>
  /// Warpper class for C-API in SFLOG.dll.
  /// Add logging functionality to a log4j-type text file.
  /// </summary>
  public class SFLOG
  {
    private const string DLLName = "SFLog.dll";

    /// <summary> Initialize the logging system. </summary>
    /// <param name="szAppName">Application name (e.g. NumXL).</param>
    /// <param name="szLogDir">Temprorary directory to use for storing logging file and intermediate files.</param>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#100",CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
    private static extern int SFLOG_Init(string szAppName, string szLogDir);

    /// <summary>
    /// Wrap the NDK_INIT API function with a CLS compliant function
    /// </summary>
    /// <param name="szAppName"></param>
    /// <param name="szLogDir"></param>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    public static NDK_RETCODE Init(string szAppName, string szLogDir)
    {
      int nRet = SFLOG_Init(szAppName, szLogDir);

      return (NDK_RETCODE)nRet;
    }


    /// <summary> Shutdown and release any resouces used by the logging system. </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#105",CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
    private static extern int SFLOG_Shutdown();

    /// <summary>
    /// Wrap the NDK_Shutdown API function with a CLS compliant function
    /// </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    public static NDK_RETCODE Shutdown()
    {
      int nRet = SFLOG_Shutdown();

      return (NDK_RETCODE)nRet;
    }


    /// <summary> Log a message to the log-file. </summary>
    /// <param name="nLevel">Logging level <see cref="SFLOG_LEVEL"/>.</param>
    /// <param name="szFilename">current source filename.</param>
    /// <param name="szFuncName">the function from which the method is called.</param>
    /// <param name="szFuncSig">the full function signature from which the method is called.</param>
    /// <param name="nLineNo">the line number where the log function is called from.</param>
    /// <param name="szMsg">the actual logging message.</param>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#110", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
    private static extern int SFLOG_LogMsg(int nLevel, string szFilename, string szFuncName,
      string szFuncSig, int nLineNo, string szMsg);

    /// <summary>
    /// Wrap the SFLOG_LogMsg API function with a CLS compliant function.  </summary>
    /// <param name="nLevel">Logging level <see cref="SFLOG_LEVEL"/>.</param>
    /// <param name="szFilename">current source filename.</param>
    /// <param name="szFuncName">the function from which the method is called.</param>
    /// <param name="szFuncSig">the full function signature from which the method is called.</param>
    /// <param name="nLineNo">the line number where the log function is called from.</param>
    /// <param name="szMsg">the actual logging message.</param>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    public static NDK_RETCODE LogMsg(SFLOG_LEVEL nLevel, string szFilename, string szFuncName, string szFuncSig, int nLineNo, string szMsg)
    {
      int nRet = SFLOG_LogMsg((int)nLevel, szFilename, szFuncName, szFuncSig, nLineNo, szMsg);

      return (NDK_RETCODE)nRet;
    }

    /// <summary> Returns current logging level. </summary>
    /// <param name="nLevel">current logging level <see cref="SFLOG_LEVEL"/>.</param>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#115", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
    private static extern int SFLOG_GETLEVEL(out int nLevel);

    /// <summary> query the current logging level in the SDK. </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="SFLOG_LEVEL"/>.</returns>
    public static SFLOG_LEVEL GETLEVEL()
    {
      int nLevel = (int)SFLOG_LEVEL.SFLOG_ALL;

      int nRet = SFLOG_GETLEVEL(out nLevel);

      return (SFLOG_LEVEL)nLevel;
    }

    /// <summary> Modify current logging level. </summary>
    /// <param name="nLevel">new logging level <see cref="SFLOG_LEVEL"/>.</param>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#120", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
    private static extern int SFLOG_SETLEVEL(int nLevel);

    /// <summary> set the new logging level in the SDK. </summary>
    /// <param name="nLevel">new logging level <see cref="SFLOG_LEVEL"/>.</param>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    public static NDK_RETCODE SETLEVEL(SFLOG_LEVEL nLevel)
    {
      int nRet = SFLOG_SETLEVEL((int)nLevel);

      return (NDK_RETCODE)nRet;
    }
  }
}
