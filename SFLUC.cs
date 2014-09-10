#region License
// <copyright file="SFLUC.cs" company="Spider Financial Corp">
//  (c) 2007-2014 Spider Financial Corp.
//  All rights reserved.
// </copyright>
//  
//
#endregion

#region Using Directives
using System;
using System.Text;
using System.Runtime.InteropServices;
#endregion

namespace NumXLAPI
{
  /// <summary>
  /// Warpper class for C-API in SFLUC.dll
  /// SFLUC provides access to NumXL SDK licensing functionality.
  /// </summary>
  public class SFLUC
  {
    const string DLLName = "SFLUC.dll";

    /// <summary> Initialize the NumXL SDK license system </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#100", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    private static extern int SFLUC_Init(string szAppName, string szLogDir, Boolean bEnableGracePeriod);
    /// <summary>
    /// Wrap the NDK_INIT API function with a CLS compliant function
    /// </summary>
    /// <param name="szAppName"></param>
    /// <param name="szLogDir"></param>
    /// <param name="bEnableGracePeriod"></param>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    public static NDK_RETCODE Init(string szAppName, string szLogDir, Boolean bEnableGracePeriod)
    {
      int nRet = SFLUC_Init(szAppName, szLogDir, bEnableGracePeriod);

      return (NDK_RETCODE)nRet;
    }

    /// <summary>Shutdown and release resources used by the NumXL SDK License system.</summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#105", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    private static extern int SFLUC_Shutdown();
    /// <summary>
    /// Shutdown and release resources used by the NumXL SDK License system
    /// </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    public static NDK_RETCODE Shutdown()
    {
      int nRet = SFLUC_Shutdown();

      return (NDK_RETCODE)nRet;
    }

    /// <summary>Verify the license key and activation code on the machine.</summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#200", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    private static extern int SFLUC_CHECK_LICENSE();
    /// <summary>
    /// Verify the license key and activation code on the machine.
    /// </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    public static NDK_RETCODE CHECK_LICENSE()
    {
      int nRet = SFLUC_CHECK_LICENSE();

      return (NDK_RETCODE)nRet;
    }

    /// <summary>Returns the effective license level.</summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#205", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    private static extern int SFLUC_LICENSE_LEVEL(out int level);
    /// <summary>
    /// Returns the effective license level.
    /// </summary>
    /// <returns> an integer value for the LICENSE LEVEL.</returns>
    public static int LICENSE_LEVEL()
    {
      int nLevel = 1;

      int nRet = SFLUC_LICENSE_LEVEL(out nLevel);

      return nLevel;
    }

    /// <summary>Returns the unique identifier for current host/machine.</summary>
    [DllImport(DLLName, EntryPoint = "#300", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    private static extern int SFLUC_MACHINEID(StringBuilder szBuffer, out UIntPtr nLen);

    /// <summary>
    /// Returns the unique identifier for current host/machine.
    /// </summary>
    /// <param name="szMachineId"></param>
    /// <returns> an integer value for the LICENSE LEVEL.</returns>
    public static int MACHINEID(ref string szMachineId)
    {
      UIntPtr size = (UIntPtr)256;

      StringBuilder sysBuffer = new StringBuilder((int)size);
      size = (UIntPtr)sysBuffer.Capacity;
      int nRet = SFLUC_MACHINEID(sysBuffer, out size);
      if (nRet >= (int)NDK_RETCODE.NDK_SUCCESS)
      {
        szMachineId = sysBuffer.ToString(0, (int)size);
      }
      return nRet;
    }

    /// <summary>Returns the license key string.</summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#305", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    private static extern int SFLUC_LICENSE_KEY(StringBuilder szBuffer, out UIntPtr nLen);

    /// <summary>
    /// Wrap the SFLUC API function with a CLS compliant function
    /// </summary>
    /// <param name="szLicense"></param>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    public static NDK_RETCODE LICENSE_KEY(ref string szLicense)
    {
      UIntPtr size = (UIntPtr)256;

      StringBuilder sysBuffer = new StringBuilder((int)size);
      size = (UIntPtr)sysBuffer.Capacity;
      int nRet = SFLUC_LICENSE_KEY(sysBuffer, out size);
      if (nRet >= (int)NDK_RETCODE.NDK_SUCCESS)
      {
        szLicense = sysBuffer.ToString(0, (int)size);
      }
      return (NDK_RETCODE)nRet;
    }

    /// <summary>Returns the expiry date of current license key and activation code.</summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#310", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    private static extern int SFLUC_LICENSE_KEY_EXPIRY(ref UInt32 expiry);

    /// <summary>
    /// Wrap the SFLUC API function with a CLS compliant function
    /// </summary>
    /// <param name="zExpiry">expiration date expressed as number of days since January 1st, 1970</param>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    public static NDK_RETCODE LICENSE_KEY_EXPIRY(ref System.DateTime zExpiry)
    {
      NDK_RETCODE nRet = NDK_RETCODE.NDK_FAILED;

      UInt32 expiry = 0;
      nRet = (NDK_RETCODE) (SFLUC_LICENSE_KEY_EXPIRY(ref expiry));
      if (nRet >= NDK_RETCODE.NDK_SUCCESS)
      {
        zExpiry = DateTime.FromOADate(expiry);
      }

      return nRet;
    }


    /// <summary>Query and retrieve the expiration date of a given license key and activation code.</summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#320", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    private static extern int SFLUC_LICENSE_EXPIRY(string szKey, string szActivationCode, ref UInt32 expiry);

    /// <summary>
    /// Wrap the SFLUC API function with a CLS compliant function.
    /// Query and retrieve the expiration date of a given license key and activation code
    /// </summary>
    /// <param name="szLicense">License key</param>
    /// <param name="szCode">Activation code</param>
    /// <param name="zExpiry">expiration date expressed as number of days since January 1st, 1970 </param>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    public static NDK_RETCODE LICENSE_KEY_EXPIRY(string szLicense, string szCode, ref System.DateTime zExpiry)
    {
      NDK_RETCODE nRet = NDK_RETCODE.NDK_FAILED;

      UInt32 expiry = 0;
      nRet = (NDK_RETCODE)(SFLUC_LICENSE_EXPIRY(szLicense,szCode,ref expiry));
      if (nRet >= NDK_RETCODE.NDK_SUCCESS)
      {
        zExpiry = DateTime.FromOADate(expiry);
      }

      return nRet;
    }

    /// <summary> Query and retrieve the expiration date and license level of a license key and activation code.</summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#215", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    private static extern int SFLUC_CHECK_KEYCODE(string szPDKey, string szKey, string szActivationCode, ref UInt32 expiry, ref Int32 level);

    /// <summary>
    /// Wrap the SFLUC API function with a CLS compliant function.
    ///  Query and retrieve the expiration date and license level of a license key and activation code
    /// </summary>
    /// <param name="szPDKey">Product key (i.e. 31223-200-162)</param>
    /// <param name="szLicense">License key</param>
    /// <param name="szCode">Activation code</param>
    /// <param name="zExpiry">expiration date expressed as number of days since January 1st, 1970 </param>
    /// <param name="nLevel">Effective license level: 1=Limited, 2=Full/Pro </param>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    public static NDK_RETCODE CHECK_KEYCODE(string szPDKey, string szLicense, string szCode, ref System.DateTime zExpiry, ref int nLevel)
    {
      NDK_RETCODE nRet = NDK_RETCODE.NDK_FAILED;

      UInt32 expiry = 0;
      nRet = (NDK_RETCODE)(SFLUC_CHECK_KEYCODE(szPDKey, szLicense, szCode, ref expiry, ref nLevel));
      if (nRet >= NDK_RETCODE.NDK_SUCCESS)
      {
        zExpiry = DateTime.FromOADate(expiry);
      }

      return nRet;
    }


    /// <summary>Returns the activation code for the license key found on the machine.</summary>
    [DllImport(DLLName, EntryPoint = "#315", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    private static extern int SFLUC_ACTIVATION_CODE(StringBuilder szBuffer, out UIntPtr nLen);

    /// <summary>
    /// Wrap the SFLUC API function with a CLS compliant function.
    /// Returns the activation code for the license key found on the machine
    /// </summary>
    /// <param name="szActivationCode">The buffer that will receive the activation code</param>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    public static NDK_RETCODE ACTIVATION_CODE(ref string szActivationCode)
    {
      UIntPtr size = (UIntPtr)256;

      StringBuilder sysBuffer = new StringBuilder((int)size);
      size = (UIntPtr)sysBuffer.Capacity;
      int nRet = SFLUC_ACTIVATION_CODE(sysBuffer, out size);
      if (nRet >= (int)NDK_RETCODE.NDK_SUCCESS)
      {
        szActivationCode = sysBuffer.ToString(0, (int)size);
      }
      return (NDK_RETCODE)nRet;
    }

    /// <summary>Returns the license key status (e.g. grace period, trial, etc.).</summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#210", CallingConvention = CallingConvention.StdCall)]
    private static extern int SFLUC_LICENSE_STATUS();

    /// <summary>
    /// Wrap the SFLUC API function with a CLS compliant function
    /// Returns the license key status (e.g. grace period, trial, etc.).
    /// </summary>
    /// <returns> an integer value for the LICENSE LEVEL.</returns>
    public static int LICENSE_STATUS()
    {
      int nRet = SFLUC_LICENSE_STATUS();

      return nRet;
    }



    /// <summary>Query and retrieve the latest version available.</summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#225", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    private static extern int SFLUC_UPDATEVERSION(string szLicenseKey, string szFileVersion, StringBuilder updateVersion, out UIntPtr pVerSize, StringBuilder downloadURL, out UIntPtr pURLSize);

    /// <summary>
    /// Wrap the SFLUC API function with a CLS compliant function
    /// Query and retrieve the latest version available.
    /// </summary>
    /// <param name="szLicenseKey">License key</param>
    /// <param name="szFileVersion">is the long file version (1.XX.XXXX.X)</param>
    /// <param name="updateVersion">is the available most recent version available</param>
    /// <param name="downloadURL"> is the download URL version</param>
    /// <returns> an integer value for the LICENSE LEVEL.</returns>
    public static NDK_RETCODE UPDATEVERSION(string szLicenseKey, string szFileVersion, ref string updateVersion, ref string downloadURL)
    {
      UIntPtr verSize = (UIntPtr)256;
      UIntPtr urlSize = (UIntPtr)256;

      StringBuilder sysBuffer = new StringBuilder((int)verSize);
      StringBuilder sysBuffer2 = new StringBuilder((int)urlSize);

      verSize = (UIntPtr)sysBuffer.Capacity;
      urlSize = (UIntPtr)sysBuffer2.Capacity;
      int nRet = SFLUC_UPDATEVERSION(szLicenseKey, szFileVersion, sysBuffer, out verSize, sysBuffer2, out urlSize);
      if (nRet >= (int)NDK_RETCODE.NDK_SUCCESS)
      {
        updateVersion = sysBuffer.ToString(0, (int)verSize);
        downloadURL = sysBuffer2.ToString(0, (int)urlSize);
      }
      return (NDK_RETCODE)nRet;
    }

    /// <summary>Query and retrieve the service date for a given license.</summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#220", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    private static extern int SFLUC_SERVICEDATE(string szLicenseKey, ref UInt32 serviceDate);

    /// <summary>
    /// Wrap the SFLUC API function with a CLS compliant function.
    /// Query and retrieve the expiration date of a given license key and activation code
    /// </summary>
    /// <param name="szLicense">License key</param>
    /// <param name="serviceDate">is the serial date number of the service/support expiry date</param>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    public static NDK_RETCODE SERVICEDATE(string szLicense, ref System.DateTime serviceDate)
    {
      NDK_RETCODE nRet = NDK_RETCODE.NDK_FAILED;

      UInt32 expiry = 0;
      nRet = (NDK_RETCODE)(SFLUC_SERVICEDATE(szLicense, ref expiry));
      if (nRet >= NDK_RETCODE.NDK_SUCCESS)
      {
        serviceDate = DateTime.FromOADate(expiry);
      }

      return nRet;
    }
  }
}
