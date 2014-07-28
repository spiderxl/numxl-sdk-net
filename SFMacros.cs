#region License
// <copyright file="SFLogger.cs" company="Spider Financial Corp">
//  (c) 2007-2013 Spider Financial Corp.
//  All rights reserved.
// </copyright>
//  
//  $Revision: 14819 $
//  $Date: 2014-07-25 16:59:22 -0500 (Fri, 25 Jul 2014) $
//  $Author: mohamad $
//  $Id: SFMacros.cs 14819 2014-07-25 21:59:22Z mohamad $ 
//  $HeadURL: https://secure.svnrepository.com/s_oliver/spiderxl/Branches/1.63SHAMROCK/products/NumXL-SDK.Net/NumXLAPI/SFMacros.cs $
//
#endregion


namespace NumXLAPI
{
  /// <summary>
  /// Defines the possible API return values.
  /// </summary>
  public enum NDK_RETCODE
  {
    /// <summary> SUCCESS </summary>
    NDK_SUCCESS =                 0,   
    /// <summary> FAILED: Internal error occured </summary>
    NDK_FAILED =                 -1,   

    // TRUE/FALSE aliases
    /// <summary> SUCCESS: return value is TRUE. </summary>
    NDK_TRUE=                     0,    
    /// <summary> SUCCESS: return value is FALSE. </summary>
    NDK_FALSE   =                 1,
    /// <summary> FAILED: The API NDK_INIT has not yet been called. </summary>
    NDK_SDK_UNINITIALIZED =      -10,  
    ///<summary> FAILED: The API NDK_INIT has not yet been called</summary>
    NDK_LOG_UNINITIALIZED=       -11,
    ///<summary> FAILED: The API NDK_INIT has not yet been called</summary>
    NDK_LUC_UNINITIALIZED=       -12  ,
    ///<summary> FAILED: The API NDK_INIT has not yet been called</summary>
    NDK_DBM_UNINITIALIZED=       -13  ,
    ///<summary> FAILED: The logging system failed during initialization, check the configuration settings</summary>
    NDK_LOG_INIT_FAILED=         -20,
    ///<summary> FAILED: Missing or failed to open the database file</summary>
    NDK_DB_INIT_FAILED=          -21,
    ///<summary> FAILED: Missing or failed to open the database file</summary>
    NDK_LUC_INIT_FAILED=         -22,

    // Initialization error codes 
    ///<summary> FAILED: The configuration file is missing</summary>
    NDK_MISSING_CONF=            -100  ,
    ///<summary> FAILED: Access denied or corrupted file</summary>
    NDK_BAD_CONF=                -101,
    ///<summary> FAILED: Invalid datapath value in the configuration file</summary>
    NDK_CONF_DATAPATH_INVALID=   -102,
    ///<summary> FAILED: failed to retrieve/construct a temp path for logs and intermediate calculation  </summary>
    NDK_DATAPATH_INVALID=        -103,
    ///<summary> FAILED: Invalid value for [GLOBALS][PRODUCTID] entry in the conf file</summary>
    NDK_CONF_PRODID_INVALID=     -104,
    ///<summary> FAILED: Failed to open the logfile for writing (permission error or file in use)</summary>
    NDK_LOGFILE_INUSE=           -105,
    ///<summary> FAILED: invalid or Null argument (e.g. AppName for return value)</summary>
    NDK_MISSING_APP_ARG=         -106,
    ///<summary> FAILED: The product license ket is invalid</summary>
    NDK_MISSING_LICENSE_KEY=     -107,
    ///<summary> FAILED: The product license ket is invalid</summary>
    NDK_INVALID_LICENSE_KEY=     -108,
    ///<summary> FAILED: The license key has yet to be activated</summary>
    NDK_INACTIVE_LICENSE_KEY=    -109,
    ///<summary> FAILED: The license key and code are not valid</summary>
    NDK_INVALID_KEY_CODE=        -110,
    ///<summary> FAILED: The license key has expired</summary>
    NDK_EXPIRED_LICENSE_KEY=     -111,
    ///<summary> FAILED: The required license level is not met by current license</summary>
    NDK_LOW_LICENSE_LEVEL=       -112,

    // Runtime error codes
    ///<summary> FAILED: an input argument with unexpected or invalid value.</summary>
    NDK_INVALID_ARG=             -300,
    ///<summary> FAILED: The user's buffer is not big enough or Insufficient input data</summary>
    NDK_LENGTH_ERROR=            -301,
    ///<summary> FAILED: Invalid value of an argument</summary>
    NDK_INVALID_VALUE=           -302,
    ///<summary> FAILED: number of non-missing values is zero</summary>
    NDK_EMPTY_TIME_SERIES=       -303,
    ///<summary> FAILED: number of non-missing values is zero</summary>
    NDK_ZERO_INVALID_VARIANCE=   -304,
    ///<summary> FAILED: The optimizer failed to converge to a unique solution.</summary>
    NDK_CALIBRATION_ERROR=       -305,
    ///<summary> FAILED: The model's parameters values did not pass the stability test.</summary>
    NDK_INVALID_MODEL=           -306,
    ///<summary> FAILED: The number of non-missing observations is not sufficient to carry on the requested function. </summary>
    NDK_INSUFFICIENT_OBS=        -307,

    // Implementation status
    ///<summary> FAILED: The required operation is not currently implemented/supported</summary>
    NDK_NOTSUPPORTED    =        -400,

    // Warnings codes
    ///<summary> WARNING: The function returns an invalid (i.e. missing) value</summary>
    NDK_RET_NAN        =         100,
    ///<summary> WARNING: The DLL is already initialize, skipping !</summary>
    NDK_SKIP_INIT       =        105,

    ///<summary> INFORMATION: the trial license key is in the 7-day grace period</summary>
    NDK_KEY_IN_GRACE_PEROD   =   1000,
    ///<summary> INFORMATION: the trial license key is in the free trial period</summary>
    NDK_KEY_IN_TRIAL_PEROD  =    1005,
    ///<summary> INFORMATION: the trial license key is not in the free trial period</summary>
    NDK_KEY_NOT_IN_TRIAL_PEROD  = 1010,
    ///<summary> INFORMATION: the perpetual license key is activated</summary>
    NDK_PERP_KEY_ACTIVE    =     1015,
    ///<summary> INFORMATION: the perpetual license key is not activated</summary>
    NDK_PERP_KEY_INACTIVE =      1020,
    ///<summary> INFORMATION: the subscription license key is activated</summary>
    NDK_SUB_KEY_ACTIVE=          1025,
    ///<summary> INFORMATION: the subscription license key is not activated. </summary>
    NDK_SUB_KEY_INACTIVE=        1030,  
  }
}
