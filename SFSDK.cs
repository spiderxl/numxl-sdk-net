#region License
// <copyright file="SFSDK.cs" company="Spider Financial Corp">
//  (c) 2007-2014 Spider Financial Corp.
//  All rights reserved.
// </copyright>
//  
//  $Revision: 14819 $
//  $Date: 2014-07-25 16:59:22 -0500 (Fri, 25 Jul 2014) $
//  $Author: mohamad $
//  $Id: SFSDK.cs 14819 2014-07-25 21:59:22Z mohamad $ 
//  $HeadURL: https://secure.svnrepository.com/s_oliver/spiderxl/Branches/1.63SHAMROCK/products/NumXL-SDK.Net/NumXLAPI/SFSDK.cs $
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
  /// Supported statistical test outputs
  /// </summary>
  public enum TEST_RETURN
  {
    TEST_PVALUE=1,        
    TEST_SCORE=2,         
    TEST_CRITICALVALUE=3  
  }
  /// <summary>
  /// multi-colinearity test method
  /// </summary>
  public enum COLNRTY_TEST_TYPE
  {
    COLNRTY_CN = 1,   
    COLNRTY_VIF = 2,  
    COLNRTY_DET = 3,  
    COLNRTY_EIGEN = 4 
  }

   /// <summary>
  /// NDK_ARMA_GOF
  /// </summary>
  public enum GOODNESS_OF_FIT_FUNC
  {
    GOF_LLF=1,  
    GOF_AIC=2,  
    GOF_BIC=3,  
    GOF_HQC=4,  
    GOF_RSQ=5,  
    GOF_ARSQ=6  
  };

  /// <summary>
  /// NDK_ARMA_FIT
  /// </summary>
  public enum FIT_RETVAL_FUNC
  {
    FIT_MEAN=1,     
    FIT_STDEV=2,    
    FIT_RESID=3,    
    FIT_STD_RESID=4 
  };

  /// <summary>
  /// NDK_ARMA_RESID
  /// </summary>
  public enum RESID_RETVAL_FUNC
  {
    RESIDS_STD=1,   
    RESIDS_RAW=2    
  }

  /// <summary>
  /// NDK_ARMA_PARAM
  /// </summary>
  public enum MODEL_RETVAL_FUNC
  {
    PARAM_GUESS=1,      
    PARAM_CALIBRATE=2,  
    PARAM_ERROR=3       
  };

  /// <summary>
  /// NDK_ARMA_FORE
  /// </summary>
  public enum FORECAST_RETVAL_FUNC
  {
    FORECAST_MEAN=1,      
    FORECAST_STDEV=2,     
    FORECAST_TS_STDEV=3,  
    FORECAST_LL=4,        
    FORECAST_UL=5         
  };

  /// <summary>
  /// NDK_NORMALTEST
  /// </summary>
  public enum NORMALTEST_METHOD
  {
    NORMALTEST_JB=1,      
    NORMALTEST_WS=2,      
    NORMALTEST_CHISQ=3    
  };

  /// <summary>
  /// NDK_XCFTEST and NDK_XCF
  /// </summary>
  public enum CORRELATION_METHOD
  {
    XCF_PEARSON=1,      
    XCF_SPEARMAN=2,     
    XCF_KENDALL=3       
  }


  /// <summary>
  /// NDK_GLM_GOF
  /// </summary>
  public enum GLM_LINK_FUNC
  {
    GLM_LVK_IDENTITY=1,     
    GLM_LVK_LOG=2,          
    GLM_LVK_LOGIT=3,        
    GLM_LVK_PROBIT=4,       
    GLM_LVK_CLOGLOG=5       
  };

  /// <summary>
  /// NDK_GARCH_PARAM
  /// </summary>
  public enum INNOVATION_TYPE
  {
    INNOVATION_GAUSSIAN=1,  
    INNOVATION_TDIST=2,     
    INNOVATION_GED=3        
  };

  /// <summary>
  /// NDK_TREND
  /// </summary>
  public enum TREND_TYPE
  {
    TREND_LINEAR=1,       
    TREND_POLYNOMIAL=2,   
    TREND_EXPONENTIAL=3,  
    TREND_LOGARITHMIC=4,  
    TREND_POWER=5         
  }


  /// <summary>
  /// Warpper class for C-API in SFSDK.dll
  /// SFSDK provides access to NumXL SDK time series and statistics functionality.
  /// </summary>
  public class SFSDK
  {
    const string DLLName = "SFSDK.dll";

    /// <summary> Initializes the SFSDK dll and verifies the license key for the current host</summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#100", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    private static extern int NDK_Init(string szAppName,   /// <param name="szAppName">User-defined application name</param>
                                        string szKey,      /// <param name="szKey">NumXL License key</param>
                                        string szActCode, 
                                        string szLogDir);

    /// <summary>
    /// Wrap the NDK_INIT API function with a CLS compliant function
    /// </summary>
    /// <param name="szAppName">User-defined application name</param>
    /// <param name="szKey">NumXL License key</param>
    /// <param name="szActCode">Activation code</param>
    /// <param name="szLogDir">Temporary files and Logging directory</param>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    public static NDK_RETCODE Init(string szAppName, string szKey, string szActCode, string szLogDir)
    {
      int nRet = NDK_Init(szAppName, szKey, szActCode, szLogDir);

      return (NDK_RETCODE)nRet;
    }

    /// <summary> shutdown the SFSDK module and release allocated resources.</summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#105", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    private static extern int NDK_Shutdown();
    /// <summary>
    /// Wrap the NDK_Shutdown API function with a CLS compliant function
    /// </summary>
    /// <returns></returns>
    public static NDK_RETCODE Shutdown()
    {
      int nRet = NDK_Shutdown();

      return (NDK_RETCODE)nRet;
    }

    /// <summary> Query NumXL SDK for environment information.</summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#110", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int  NDK_INFO(int nRetType, StringBuilder  szMsg, int nSize);


    /// <summary> Returns an array of cells for the backward shifted, backshifted or lagged time series.</summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    /// <seealso cref="NDK_DIFF"/>
    [DllImport(DLLName, EntryPoint = "#1000", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_LAG( [MarshalAs(UnmanagedType.LPArray)] double[] data, IntPtr nLen, IntPtr lag);

    /// <summary> Returns an array of cells for the differenced time series (i.e. (1-L^S)^D).</summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    /// <seealso cref="NDK_LAG"/>
    /// <seealso cref="NDK_INTEG"/>
    [DllImport(DLLName, EntryPoint = "#1005", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_DIFF([MarshalAs(UnmanagedType.LPArray)] double[] data, IntPtr nSize, IntPtr nLag, IntPtr nDifference);

    /// <summary> Returns an array of cells for the integrated time series (inverse operator of NDK_DIFF). </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    /// <seealso cref="NDK_DIFF"/>
    /// <seealso cref="NDK_LAG"/>
    [DllImport(DLLName, EntryPoint = "#1010", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_INTEG([MarshalAs(UnmanagedType.LPArray)] double[] data, IntPtr nSize, IntPtr nLag, IntPtr nDifference, [MarshalAs(UnmanagedType.LPArray)] double[] pX0, IntPtr nX0Len);

    /// <summary> Returns an array of cells of a time series after removing all missing values.</summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#1010", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_RMNA([MarshalAs(UnmanagedType.LPArray)] double[] data, out IntPtr nSize);

    /// <summary> Returns the time-reversed order time series (i.e. the first observation is swapped with the last observation, etc.): both missing and non-missing values.</summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#1024", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_REVERSE([MarshalAs(UnmanagedType.LPArray)] double[] data, IntPtr nSize);

    /// <summary> Returns an array of cells for the scaled time series.</summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#1023", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_SCALE([MarshalAs(UnmanagedType.LPArray)] double[] data, IntPtr nSize, double factor);

    /// <summary> Returns an array of the difference between two time series.</summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#1022", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_SUB([MarshalAs(UnmanagedType.LPArray)] double[] data, IntPtr nSize1, [MarshalAs(UnmanagedType.LPArray)] double[] data2, IntPtr nSize2);

    /// <summary>Returns an array of cells for the sum of two time series..</summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#1021", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_ADD([MarshalAs(UnmanagedType.LPArray)] double[] data1, IntPtr nSize1, [MarshalAs(UnmanagedType.LPArray)] double[] data2, IntPtr nSize2);

    /// <summary> Time series convolution orderator.</summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#1032", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_CONVOLUTION([MarshalAs(UnmanagedType.LPArray)] double[] pData1, IntPtr nSize1, [MarshalAs(UnmanagedType.LPArray)] double[] pData2, IntPtr nSize2,  out double pResult, out IntPtr nWindowSize);

    /// <summary> Inverse discrete fourier transform.</summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#1031", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_IDFT([MarshalAs(UnmanagedType.LPArray)] double[] amp,[MarshalAs(UnmanagedType.LPArray)] double[]  phase, IntPtr nSize,  [MarshalAs(UnmanagedType.LPArray)] double[] data, IntPtr nWindowSize);

    /// <summary> discrete fourier transform.</summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#1030", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_DFT([MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize, short  component, short  argRetType, out double retVal);

    /// <summary> Computes the complementary log-log transformation, including its inverse.</summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#4005", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_CLOGLOG([MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize, short argRetType);

    /// <summary> Computes the probit transformation, including its inverse.</summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#4004", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_PROBIT([MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize, short argRetType);

    /// <summary> Computes the logit transformation, including its inverse.</summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#4003", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_LOGIT([MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize, short argRetType);

    /// <summary> Computes the Box-Cox transformation, including its inverse.</summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#4002", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_BOXCOX([MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize, out double lambda, out double fAlpha, int argRetType, out double retVal);

    /// <summary> Detrends a time series using a regression of y against a polynomial time trend of order p. </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#4010", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_DETREND([MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize, short polyOrder);

    /// <summary> Returns an array of the deseasonalized time series, assuming a linear model. </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#4017", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_RMSEASONAL([MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize, IntPtr period);

    /// <summary> Returns an array of a time series after substituting all missing values with the mean/median. </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#4000", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int	NDK_INTERP_NAN([MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize, short  nMethod, double plug);

    /// <summary> Imput missing values using brownian bridge </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#4015", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_INTERP_BROWN([MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize);

    /// <summary> Examine whether the given array has one or more missing values. </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#4018", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_HASNA([MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize, short intermediate);

    /// <summary> Calculates the sample autocorrelation function (ACF) of a stationary time series  </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#200", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_ACF([MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize, int nLag, out double retVal);

    /// <summary> Calculates the standard error in the sample autocorrelation function. </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#205", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_ACF_ERROR([MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize, int nLag, out double retVal);

    /// <summary> Calculates the confidence interval limits (upper/lower) for the autocorrelation function. </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#210", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_ACFCI([MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize, int nLag, double alpha, out double retUpper, out double retLower);

    /// <summary>  Calculates the sample partial autocorrelation function (PACF).  </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#215", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_PACF([MarshalAs(UnmanagedType.LPArray)] double[]  pData, IntPtr nSize, int nLag, out double retVal);

    /// <summary> Calculates the standard error of the sample partial autocorrelation function (PACF). </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#220", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_PACF_ERROR([MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize, int nLag, out double retVal);

    /// <summary> Calculates the confidence interval limits (upper/lower) for the partial-autocorrelation function. </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#225", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_PACFCI([MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize, int nLag, double alpha, out double retUpper, out double retLower);

    /// <summary> Calculates the estimated value of the exponential-weighted volatility (EWV). </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#1015", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_EWMA([MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize, double lambda, IntPtr nStep, out double retVal);

    /// <summary> Computes the correlation factor using the exponential-weighted correlation function. </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#1020", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_EWXCF([MarshalAs(UnmanagedType.LPArray)] double[] pData1, [MarshalAs(UnmanagedType.LPArray)] double[] pData2, IntPtr nSize, double lambda, IntPtr nStep, out double retVal);

    /// <summary> Interpolate function </summary>
    [DllImport(DLLName, EntryPoint = "#3000", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int	NDK_INTERPOLATE([MarshalAs(UnmanagedType.LPArray)] double[] pXData, IntPtr nXSize, [MarshalAs(UnmanagedType.LPArray)] double[] pYData, IntPtr nYSize, [MarshalAs(UnmanagedType.LPArray)] double[] pXTargets, IntPtr nXTargetSize, short  nMethod, bool allowExtrp,[MarshalAs(UnmanagedType.LPArray)] double[] pYTargets, IntPtr nYTargetSize);

    // Statistical testing
    /// <summary> Calculates the p-value of the statistical test for the population autocorrelation function. </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#300", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_ACFTEST([MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize, int nLag, double targetVal, double alpha, short method, short retType, out double retVal);

    /// <summary> Returns the p-value of the normality test (i.e. whether a data set is well-modeled by a normal distribution). </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#301", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_NORMALTEST([MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize, short method, short retType, out double retVal);

    /// <summary> Computes the p-value of the statistical portmanteau test (i.e. whether any of a group of autocorrelations of a time series are different from zero). </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#302", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_WNTEST([MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize, int nLag, short argMethod, short retType, out double retVal);

    /// <summary> Calculates the p-value of the ARCH effect test (i.e. the white-noise test for the squared time series). </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#303", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_ARCHTEST([MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize, int nLag, short argMethod, short retType, out double retVal);

    /// <summary> Calculates the p-value of the statistical test for the population mean. </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#304", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_MEANTEST([MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize, double target, short argMethod, short retType, out double retVal);

    /// <summary> Calculates the p-value of the statistical test for the population standard deviation. </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#305", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_STDEVTEST([MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize, double target, short argMethod, short retType, out double retVal);

    /// <summary> Calculates the p-value of the statistical test for the population skew (i.e. 3rd moment) </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#306", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_SKEWTEST([MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize, double target, short argMethod, short retType, out double retVal);

    /// <summary> Calculates the p-value of the statistical test for the population excess kurtosis (4th moment). </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#307", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_XKURTTEST([MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize, double target, short argMethod, short retType, out double retVal);

    /// <summary> Calculates the test stats, p-value or critical value of the correlation test. </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#308", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_XCFTEST([MarshalAs(UnmanagedType.LPArray)] double[] pData1, [MarshalAs(UnmanagedType.LPArray)] double[] pData2, IntPtr nSize, int nLag, short method, double target, short retTYpe, double alpha, out double retVal);

    /// <summary> Returns the p-value of the Augmented Dickey-Fuller (ADF) test, which tests for a unit root in the time series sample. </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#309", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_ADFTEST([MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize, short maxOrder, short option, bool testDown, short argMethod, short retType, double alpha, out double retVal);

    /// <summary> KPSS (stationary) test function </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#310", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_KPSSTEST([MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize, short maxOrder, short option, bool testDown, short argMethod, short retType, double alpha, out double retVal);

    /// <summary> Returns the Johansen (cointegration) test statistics for two or more time series. </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    // TODO: We need to check this function declaration
    [DllImport(DLLName, EntryPoint = "#311", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_JOHANSENTEST(ref IntPtr pData, IntPtr nSize, IntPtr nVars, short maxOrder, short nPolyOrder, bool tracetest, short nNoRelations, short retType, double alpha, ref double retVal);

    /// <summary> Returns the Johansen (cointegration) test statistics for two or more time series. </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    // TODO: We need to check this function declaration
    [DllImport(DLLName, EntryPoint = "#312", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_COLNRTY_TEST(ref IntPtr pData, IntPtr nSize, IntPtr nVars,
                                              [MarshalAs(UnmanagedType.LPArray)] Byte[] mask, IntPtr nMaskLen,
                                              COLNRTY_TEST_TYPE nMethod, short nColIndex, ref double retVal);


    /// <summary> Returns the p-value of the regression stability test (i.e. whether the coefficients in two linear regressions on different data sets are equal). </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#313", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_CHOWTEST(ref IntPtr XX1, 
                                          IntPtr M,
                                          [MarshalAs(UnmanagedType.LPArray)] double[] Y1,
                                          IntPtr N1,
                                          ref IntPtr XX2,
                                          [MarshalAs(UnmanagedType.LPArray)] double[] Y2,
                                          IntPtr N2,
                                          [MarshalAs(UnmanagedType.LPArray)] Byte[] mask, IntPtr nMaskLen,
                                          double intercept,
                                          TEST_RETURN retType,
                                          ref double retVal);



    // Statistical distribution
    /// <summary> Calculates the excess kurtosis of the generalized error distribution (GED) </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#500", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_GED_XKURT(double df, ref double retVal);

    /// <summary> Calculates the excess kurtosis of the student's t-distribution. </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#501", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_TDIST_XKURT(double df, ref double retVal);

    /// <summary> Calculates the empirical distribution function (or empirical cdf) of the sample data. </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#502", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_EDF([MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize, double targetVal, short retType, ref double retVal);

    /// <summary> Returns the number of histogram bins using a given method. </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#503", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_HIST_BINS([MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize, short argMethod, ref IntPtr retVal);

    /// <summary> Returns the upper/lower limit or center value of the k-th histogram bin.</summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#504", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_HIST_BIN_LIMIT([MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize, IntPtr nBins, IntPtr index, short argRetTYpe, ref double retVal);

    /// <summary> Calculates the histogram or cumulative histogram function for a given bin. </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#505", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_HISTOGRAM([MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize, IntPtr nBins, IntPtr index, short argRetTYpe, ref double retVal);

    /// <summary> Returns the upper/lower limit or center value of the k-th histogram bin. </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#506", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_KERNEL_DENSITY_ESTIMATE([MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize, double targetVal, double bandwidth, short argKernelFunc, ref double retVal);

    /// <summary> Returns a sequence of random numbers drawn from Normal distribution. </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#520", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_GAUSS_RNG(double mean, double stdev, UIntPtr seed, [MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize);

    /// <summary> Returns the upper & lower limit of the confidence interval for the Gaussian distribution. </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#507", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_GAUSS_FORECI(double mean, double stdev, double alpha, short upper, ref double retVal);

    /// <summary> Returns the upper & lower limit of the confidence interval for the student\'s t-distribution. </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#508", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_TSTUDENT_FORECI(double mean, double stdev, double df, double alpha, short upper, ref double retVal);

    /// <summary> Returns the upper & lower limit of the confidence interval for the Generalized Error Distribution (GED) distribution. </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#509", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_GED_FORECI(double mean, double stdev, double df, double alpha, short upper, ref double retVal);


    // General Statistics
    /// <summary> Compute the kernel density distribution function </summary>
    [DllImport(DLLName, EntryPoint = "#403", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_XKURT([MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize, short argMenthod, ref double retVal);

    /// <summary> Compute sample skewness function </summary>
    [DllImport(DLLName, EntryPoint = "#404", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_SKEW([MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize, short argMenthod, ref double retVal);

    /// <summary> Compute sample skewness function </summary>
    [DllImport(DLLName, EntryPoint = "#405", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_AVERAGE([MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize, short argMenthod, ref double retVal);

    /// <summary> Compute sample skewness function </summary>
    [DllImport(DLLName, EntryPoint = "#406", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_VARIANCE([MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize, short argMenthod, ref double retVal);

    /// <summary> Compute sample skewness function </summary>
    [DllImport(DLLName, EntryPoint = "#407", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_MIN([MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize, short argMenthod, ref double retVal);

    /// <summary> Compute sample skewness function </summary>
    [DllImport(DLLName, EntryPoint = "#408", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_MAX([MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize, short argMenthod, ref double retVal);

    /// <summary> Compute sample skewness function </summary>
    [DllImport(DLLName, EntryPoint = "#410", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_QUANTILE([MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize, double argPct, ref double retVal);

    /// <summary> Compute sample skewness function </summary>
    [DllImport(DLLName, EntryPoint = "#411", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_IQR([MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize, ref double retVal);

    /// <summary> Returns the sorted sample data </summary>
    /// <param name="pData">is the input data sample (a one dimensional array)</param>
    /// <param name="nSize">is the number of observations in pData</param>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#422", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_SORT_ASC([MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize);

    /// <summary> Calculates the Hurst exponent (a measure of persistence or long memory) for time series. </summary>
    /// <param name="pData">is the input data sample (a one dimensional array)</param>
    /// <param name="nSize">is the number of observations in pData</param>
    /// <param name="alpha">is the statistical significance level (1%, 5%, 10%). If missing, a default of 5% is assumed.</param>
    /// <param name="retType">is a number that determines the type of return value:
    /// <list type="number">
    /// <item>1 = Empirical Hurst exponent (R/S method)</item>
    /// <item>2 = Anis-Lloyd/Peters corrected Hurst exponent</item>
    /// <item>3 = Theoretical Hurst exponent</item>
    /// <item>4 = Upper limit of the confidence interval</item>
    /// <item>5 = Lower limit of the confidence interval</item>
    /// </list>
    /// </param>
    /// <param name="retVal">is the calculated value of this function.</param>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#409", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_HURST_EXPONENT([MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize, double alpha, short retType, ref double retVal);

    /// <summary> Returns the sample Gini coefficient, a measure of statistical dispersion. </summary>
    /// <param name="pData">is the input data sample (a one dimensional array)</param>
    /// <param name="nSize">is the number of observations in pData</param>
    /// <param name="retVal">is the calculated value of this function.</param>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    /// <remarks>
    /// <list type="bullet">
    /// <item><description>A low Gini coefficient indicates a more equal distribution, with 0 corresponding to complete equality. Higher Gini coefficients indicate more unequal distributions, with 1 corresponding to complete inequality.</description></item>
    /// <item><description>The input data series may include missing values (NaN), but they will not be included in the calculations.</description></item>
    /// <item><description>The values in the input data series must be non-negative.</description></item>
    /// </list>
    /// </remarks>
    [DllImport(DLLName, EntryPoint = "#400", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_GINI([MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize, ref double retVal);

    /// <summary> Calculates the cross-correlation function between two time series </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#401", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_XCF([MarshalAs(UnmanagedType.LPArray)] double[] pData1, [MarshalAs(UnmanagedType.LPArray)] double[] pData2, IntPtr nSize, IntPtr nLag, short nMethod, short retType, ref double retVal);

    /// <summary> Returns the sample root mean square (RMS). </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#412", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_RMS([MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize, short argMenthod, ref double retVal);

    /// <summary> Returns the mean difference of the input data series. </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#413", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_MD([MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize, short argMenthod, ref double retVal);

    /// <summary> Returns the sample relative mean difference. </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#414", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_RMD([MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize, short argMenthod, ref double retVal);

    /// <summary> Returns the sample median of absolute deviation (MAD) </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#415", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_MAD([MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize, short argMenthod, ref double retVal);

    /// <summary> Returns the long-run variance using a Bartlett kernel with window size k. </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#416", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_LRVAR([MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize, IntPtr argWindow, ref double retVal);

    /// <summary>  Calculates the sum of absolute errors (SAE) between the forecast and the eventual outcomes. </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#417", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_SAD([MarshalAs(UnmanagedType.LPArray)] double[] pData1, [MarshalAs(UnmanagedType.LPArray)] double[] pData2, IntPtr nSize, ref double retVal);

    /// <summary> Calculates the mean absolute error function for the forecast and the eventual outcomes. </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#418", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_MAE([MarshalAs(UnmanagedType.LPArray)] double[] pData1, [MarshalAs(UnmanagedType.LPArray)] double[] pData2, IntPtr nSize, ref double retVal);

    /// <summary> Calculates the mean absolute percentage error (deviation) function for the forecast and the eventual outcomes. </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#419", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_MAPE([MarshalAs(UnmanagedType.LPArray)] double[] pData1, [MarshalAs(UnmanagedType.LPArray)] double[] pData2, IntPtr nSize, short retType, ref double retVal);

    /// <summary> Calculates the root mean squared error (aka root mean squared deviation (RMSD)) function. </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#420", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_RMSE([MarshalAs(UnmanagedType.LPArray)] double[] pData1, [MarshalAs(UnmanagedType.LPArray)] double[] pData2, IntPtr nSize, short retType, ref double retVal);

    /// <summary> Calculates the sum of the squared errors of the prediction function. </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#421", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_SSE([MarshalAs(UnmanagedType.LPArray)] double[] pData1, [MarshalAs(UnmanagedType.LPArray)] double[] pData2, IntPtr nSize, ref double retVal);

    // Smoothing API functions calls
    /// <summary> 
    /// Returns the weighted moving (rolling/running) average using the previous m data points. 
    /// </summary>
    /// 
    /// <seealso cref="NDK_SESMTH"/>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#2000", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_WMA([MarshalAs(UnmanagedType.LPArray)] double[] pData, int nSize, bool bAscending, [MarshalAs(UnmanagedType.LPArray)] double[] pWeights, int nwSize, int nHorizon, ref double retVal);

    /// <summary> Returns the (Brown's) simple exponential (EMA) smoothing estimate of the value of X at time t+m (based on the raw data up to time t). </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#2005", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_SESMTH([MarshalAs(UnmanagedType.LPArray)] double[] pData, int nSize, bool bAscending, ref double alpha, int nHorizon, bool bOptimize, ref double retVal);

    /// <summary> Returns the (Holt-Winter's) double exponential smoothing estimate of the value of X at time T+m. </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#2010", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_DESMTH([MarshalAs(UnmanagedType.LPArray)] double[] pData, int nSize, bool bAscending, ref double alpha, ref double beta, int xlHorizon, bool bOptimize, ref double retVal);

    /// <summary> Returns the (Brown's) linear exponential smoothing estimate of the value of X at time T+m (based on the raw data up to time t). </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#2015", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_LESMTH([MarshalAs(UnmanagedType.LPArray)] double[] pData, int nSize, bool bAscending, ref double alpha, int xlHorizon, bool bOptimize, ref double retVal);

    /// <summary> Returns the (Winters's) triple exponential smoothing estimate of the value of X at time T+m. </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#2020", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_TESMTH([MarshalAs(UnmanagedType.LPArray)] double[] pData, int nSize, bool bAscending, ref double alpha, ref double beta, ref double gamma, int seasonLength, int nHorizon, bool bOptimize, ref double retVal);

    /// <summary> Returns values along a trend curve (e.g. linear, quadratic, exponential, etc.) at time T+m. </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#2021", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_TREND([MarshalAs(UnmanagedType.LPArray)] double[] pData, int nSize, bool bAscending, short nTrendType, short argPolyOrder, bool AllowIntercep, double InterceptVal, int nHorizon, short argRetType, double argAlpha, ref double retVal);

    // SLR
    /// <summary> Calculates the OLS regression coefficients values </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#720", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_SLR_PARAM( [MarshalAs(UnmanagedType.LPArray)] double[] pXData, IntPtr nXSize,
                                            [MarshalAs(UnmanagedType.LPArray)] double[] pYData, IntPtr nYSize,
                                            double intercept,
                                            double alpha,
                                            short nRetType,
                                            short ParamIndex,
                                            ref double retVal);

    /// <summary> Calculates the forecast mean, error and confidence interval. </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#721", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_SLR_FORE([MarshalAs(UnmanagedType.LPArray)] double[] pXData, IntPtr nXSize,
                                           [MarshalAs(UnmanagedType.LPArray)] double[] pYData, IntPtr nYSize,
                                           double intercept,
                                           double target,
                                           double alpha,
                                           short nRetType,
                                           ref double retVal);

    /// <summary> Returns the fitted values of the conditional mean, residuals or leverage measures. </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#722", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_SLR_FITTED([MarshalAs(UnmanagedType.LPArray)] double[] pXData, IntPtr nXSize,
                                           [MarshalAs(UnmanagedType.LPArray)] double[] pYData, IntPtr nYSize,
                                           double intercept,
                                           short nRetType);


    /// <summary> Returns the fitted values of the conditional mean, residuals or leverage measures. </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#723", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_SLR_ANOVA([MarshalAs(UnmanagedType.LPArray)] double[] pXData, IntPtr nXSize,
                                           [MarshalAs(UnmanagedType.LPArray)] double[] pYData, IntPtr nYSize,
                                           double intercept,
                                           short nRetType);


    /// <summary> Calculates a measure for the goodness of fit (e.g. R^2). </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#724", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_SLR_GOF([MarshalAs(UnmanagedType.LPArray)] double[] pXData, IntPtr nXSize,
                                           [MarshalAs(UnmanagedType.LPArray)] double[] pYData, IntPtr nYSize,
                                           double intercept,
                                           short nRetType,
                                           ref double retVal);



    // MLR
    /// <summary> Calculates the OLS regression coefficients values </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#730", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_MLR_PARAM([MarshalAs(UnmanagedType.LPArray)] double[] pXData, IntPtr nXSize,IntPtr nXVars,
                                           [MarshalAs(UnmanagedType.LPArray)] byte[] mask, IntPtr nMaskLen,
                                           [MarshalAs(UnmanagedType.LPArray)] double[] pYData, IntPtr nYSize,
                                           double intercept,
                                           double alpha,
                                           short nRetType,
                                           short ParamIndex,
                                           ref double retVal);

    /// <summary> Calculates the forecast mean, error and confidence interval. </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#731", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_MLR_FORE([MarshalAs(UnmanagedType.LPArray)] double[] pXData, IntPtr nXSize, IntPtr nXVars,
                                          [MarshalAs(UnmanagedType.LPArray)] byte[] mask, IntPtr nMaskLen,
                                           [MarshalAs(UnmanagedType.LPArray)] double[] pYData, IntPtr nYSize,
                                           double intercept,
                                           double target,
                                           double alpha,
                                           short nRetType,
                                           ref double retVal);

    /// <summary> Returns the fitted values of the conditional mean, residuals or leverage measures. </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#732", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_MLR_FITTED([MarshalAs(UnmanagedType.LPArray)] double[] pXData, IntPtr nXSize, IntPtr nXVars,
                                            [MarshalAs(UnmanagedType.LPArray)] byte[] mask, IntPtr nMaskLen,
                                            [MarshalAs(UnmanagedType.LPArray)] double[] pYData, IntPtr nYSize,
                                            double intercept,
                                            short nRetType);

    /// <summary> Returns the fitted values of the conditional mean, residuals or leverage measures. </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#733", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_MLR_ANOVA([MarshalAs(UnmanagedType.LPArray)] double[] pXData, IntPtr nXSize, IntPtr nXVars,
                                           [MarshalAs(UnmanagedType.LPArray)] byte[] mask, IntPtr nMaskLen,
                                           [MarshalAs(UnmanagedType.LPArray)] double[] pYData, IntPtr nYSize,
                                           double intercept,
                                           short nRetType);

    /// <summary> Calculates a measure for the goodness of fit (e.g. R^2). </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#734", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_SLR_GOF( [MarshalAs(UnmanagedType.LPArray)] double[] pXData, IntPtr nXSize, IntPtr nXVars,
                                          [MarshalAs(UnmanagedType.LPArray)] byte[] mask, IntPtr nMaskLen,
                                          [MarshalAs(UnmanagedType.LPArray)] double[] pYData, IntPtr nYSize,
                                          double intercept,
                                          short nRetType,
                                          ref double retVal);

    /// <summary> Calculates the p-value and related statistics of the partial f-test (used for testing the inclusion/exclusion variables). </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#736", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_MLR_PRFTest([MarshalAs(UnmanagedType.LPArray)] double[] pXData, IntPtr nXSize, IntPtr nXVars,                                         
                                         [MarshalAs(UnmanagedType.LPArray)] double[] pYData, IntPtr nYSize,
                                         double intercept,
                                         [MarshalAs(UnmanagedType.LPArray)] byte[] mask1, IntPtr nMaskLen1,
                                         [MarshalAs(UnmanagedType.LPArray)] byte[] mask2, IntPtr nMaskLen2,
                                         double alpha,
                                         short nRetType,
                                         ref double retVal);


    /// <summary> Returns a list of the selected variables after performing the stepwise regression. </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#735", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_MLR_STEPWISE([MarshalAs(UnmanagedType.LPArray)] double[] pXData, IntPtr nXSize, IntPtr nXVars,
                                         [MarshalAs(UnmanagedType.LPArray)] byte[] mask, IntPtr nMaskLen,
                                         [MarshalAs(UnmanagedType.LPArray)] double[] pYData, IntPtr nYSize,
                                         double intercept,
                                         double alpha, 
                                         short nMode);



    // PCA

    /// <summary> Returns an array of cells for the i-th principal component (or residuals). </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#740", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_PCA_COMP([MarshalAs(UnmanagedType.LPArray)] double[] pXData, IntPtr nXSize, IntPtr nXVars,
                                         [MarshalAs(UnmanagedType.LPArray)] byte[] mask, IntPtr nMaskLen,
                                         short standardize,
                                         short nCompIndex,
                                         short retType,
                                         [MarshalAs(UnmanagedType.LPArray)] double[] retVal, IntPtr nOutSize);


    /// <summary> Returns an array of cells for the fitted values of the i-th input variable. </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#741", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_PCA_COMP([MarshalAs(UnmanagedType.LPArray)] double[] pXData, IntPtr nXSize, IntPtr nXVars,
                                         [MarshalAs(UnmanagedType.LPArray)] byte[] mask, IntPtr nMaskLen,
                                         short standardize,
                                         short nVarIndex,
                                         short wMaxPC, 
                                         short retType,
                                         [MarshalAs(UnmanagedType.LPArray)] double[] retVal, IntPtr nOutSize);


    // PCR
    // TODO


    // GLM Model
    // TODO



    // ARMA Functions
    /// <summary>Calculates one of the goodness-of-fit functions (e.g. LLF, AIC, BIC, etc.) of the given estimated ARIMA model</summary>
    /// <param name="pData">is the univariate time series data (one dimensional array)</param>
    /// <param name="nSize">is number of observations in the time series input array</param>
    /// <param name="mean">is the ARMA model mean (i.e. mu).</param>
    /// <param name="sigma">is the standard deviation of the model's residuals/innovations </param>
    /// <param name="phis">are the parameters of the AR(p) component model (starting with the lowest lag) </param>
    /// <param name="p">is the order of the AR componenet </param>
    /// <param name="thetas">are the parameters of the MA(q) component model (starting with the lowest lag). </param>
    /// <param name="q">is the order of the MA component model. </param>
    /// <param name="retType"> is a number that determines the type of return value: 1=LLF (default), 2=AIC, 3=BIC, 4=HQC, 5=R-Squared, 6=Adjusted R-Squared. </param>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="SFSDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#600", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_ARMA_GOF( [MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize, double mean, double sigma, [MarshalAs(UnmanagedType.LPArray)] double[] phis, IntPtr p, [MarshalAs(UnmanagedType.LPArray)] double[] thetas, IntPtr q,  short retType, ref double retVal);

    /// <summary> Compute standardized residuals of an ARMA model function </summary>
    /// <param name="pData">is the univariate time series data (one dimensional array)</param>
    /// <param name="nSize">is number of observations in the time series input array</param>
    /// <param name="mean">is the ARMA model mean (i.e. mu).</param>
    /// <param name="sigma">is the standard deviation of the model's residuals/innovations </param>
    /// <param name="phis">are the parameters of the AR(p) component model (starting with the lowest lag) </param>
    /// <param name="p">is the order of the AR componenet </param>
    /// <param name="thetas">are the parameters of the MA(q) component model (starting with the lowest lag). </param>
    /// <param name="q">is the order of the MA component model. </param>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="SFSDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#601", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_ARMA_RESID( [MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize, double mean, double sigma, [MarshalAs(UnmanagedType.LPArray)] double[] phis, IntPtr p, [MarshalAs(UnmanagedType.LPArray)] double[] thetas, IntPtr q,  short retType);

    /// <summary> Returns an array of cells for the initial (non-optimal), optimal or standard errors of the model's parameters. </summary>
    /// <param name="pData">is the univariate time series data (one dimensional array)</param>
    /// <param name="nSize">is number of observations in the time series input array</param>
    /// <param name="mean">is the ARMA model mean (i.e. mu).</param>
    /// <param name="sigma">is the standard deviation of the model's residuals/innovations </param>
    /// <param name="phis">are the parameters of the AR(p) component model (starting with the lowest lag) </param>
    /// <param name="p">is the order of the AR componenet </param>
    /// <param name="thetas">are the parameters of the MA(q) component model (starting with the lowest lag). </param>
    /// <param name="q">is the order of the MA component model. </param>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="SFSDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#605", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_ARMA_PARAM([MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize, ref double mean, ref double sigma, [MarshalAs(UnmanagedType.LPArray)] double[] phis, IntPtr p, [MarshalAs(UnmanagedType.LPArray)] double[] thetas, IntPtr q, MODEL_RETVAL_FUNC retType, IntPtr maxIter);

    /// <summary> Calculates the out-of-sample forecast statistics. </summary>
    /// <param name="pData">is the univariate time series data (one dimensional array)</param>
    /// <param name="nSize">is number of observations in the time series input array</param>
    /// <param name="mean">is the ARMA model mean (i.e. mu).</param>
    /// <param name="sigma">is the standard deviation of the model's residuals/innovations </param>
    /// <param name="phis">are the parameters of the AR(p) component model (starting with the lowest lag) </param>
    /// <param name="p">is the order of the AR componenet </param>
    /// <param name="thetas">are the parameters of the MA(q) component model (starting with the lowest lag). </param>
    /// <param name="q">is the order of the MA component model. </param>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="SFSDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#603", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_ARMA_FORE([MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize, double mean, double sigma, [MarshalAs(UnmanagedType.LPArray)] double[] phis, IntPtr p, [MarshalAs(UnmanagedType.LPArray)] double[] thetas, IntPtr q, IntPtr nStep, FORECAST_RETVAL_FUNC retType, double alpha, ref double retVal);

    /// <summary> Returns an array of cells for the fitted values (i.e. mean, volatility and residuals) </summary>
    /// <param name="pData">is the univariate time series data (one dimensional array)</param>
    /// <param name="nSize">is number of observations in the time series input array</param>
    /// <param name="mean">is the ARMA model mean (i.e. mu).</param>
    /// <param name="sigma">is the standard deviation of the model's residuals/innovations </param>
    /// <param name="phis">are the parameters of the AR(p) component model (starting with the lowest lag) </param>
    /// <param name="p">is the order of the AR componenet </param>
    /// <param name="thetas">are the parameters of the MA(q) component model (starting with the lowest lag). </param>
    /// <param name="q">is the order of the MA component model. </param>
    /// <param name="retType">is a number that determines the type of return value: 1 (or missing)=Fitted Mean,2=STDEV/VOL, 3=Residuals, 4=Standardized residuals.</param>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="SFSDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#602", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_ARMA_FITTED([MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize, double mean, double sigma, [MarshalAs(UnmanagedType.LPArray)] double[] phis, IntPtr p, [MarshalAs(UnmanagedType.LPArray)] double[] thetas, IntPtr q, FIT_RETVAL_FUNC retType);

    /// <summary> Examines the model's parameters for stability constraints (e.g. stationary, etc.).  </summary>
    /// <param name="mean">is the ARMA model mean (i.e. mu).</param>
    /// <param name="sigma">is the standard deviation of the model's residuals/innovations </param>
    /// <param name="phis">are the parameters of the AR(p) component model (starting with the lowest lag) </param>
    /// <param name="p">is the order of the AR componenet </param>
    /// <param name="thetas">are the parameters of the MA(q) component model (starting with the lowest lag). </param>
    /// <param name="q">is the order of the MA component model. </param>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="SFSDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#606", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_ARMA_VALIDATE(double mean, double sigma, [MarshalAs(UnmanagedType.LPArray)] double[] phis, IntPtr p, [MarshalAs(UnmanagedType.LPArray)] double[] thetas, IntPtr q);

    /// <summary> Calculates the out-of-sample forecast statistics. </summary>
    /// <param name="pData">is the univariate time series data (one dimensional array)</param>
    /// <param name="nSize">is number of observations in the time series input array</param>
    /// <param name="mean">is the ARMA model mean (i.e. mu).</param>
    /// <param name="sigma">is the standard deviation of the model's residuals/innovations </param>
    /// <param name="phis">are the parameters of the AR(p) component model (starting with the lowest lag) </param>
    /// <param name="p">is the order of the AR componenet </param>
    /// <param name="thetas">are the parameters of the MA(q) component model (starting with the lowest lag). </param>
    /// <param name="q">is the order of the MA component model. </param>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="SFSDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#604", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_ARMA_SIM(double mean, double sigma, [MarshalAs(UnmanagedType.LPArray)] double[] phis, IntPtr p, [MarshalAs(UnmanagedType.LPArray)] double[] thetas, IntPtr q,
                                          [MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize,
                                          int seed,
                                          [MarshalAs(UnmanagedType.LPArray)] double[] retArray, IntPtr nSteps);

    // ARIMA Functions

    /// <summary> Examines the model's parameters for stability constraints (e.g. stationary, etc.).  </summary>
    /// <param name="mean">is the ARMA model mean (i.e. mu).</param>
    /// <param name="sigma">is the standard deviation of the model's residuals/innovations </param>
    /// <param name="phis">are the parameters of the AR(p) component model (starting with the lowest lag) </param>
    /// <param name="p">is the order of the AR componenet </param>
    /// <param name="thetas">are the parameters of the MA(q) component model (starting with the lowest lag). </param>
    /// <param name="q">is the order of the MA component model. </param>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="SFSDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#606", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_ARIMA_VALIDATE(double mean, double sigma, short nIntegral, [MarshalAs(UnmanagedType.LPArray)] double[] phis, IntPtr p, [MarshalAs(UnmanagedType.LPArray)] double[] thetas, IntPtr q);

    /// <summary> Computes the log-likelihood ((LLF), Akaike Information Criterion (AIC) or other goodness of fit function of the ARIMA model.  </summary>
    /// <param name="mean">is the ARMA model mean (i.e. mu).</param>
    /// <param name="sigma">is the standard deviation of the model's residuals/innovations </param>
    /// <param name="phis">are the parameters of the AR(p) component model (starting with the lowest lag) </param>
    /// <param name="p">is the order of the AR componenet </param>
    /// <param name="thetas">are the parameters of the MA(q) component model (starting with the lowest lag). </param>
    /// <param name="q">is the order of the MA component model. </param>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="SFSDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#610", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_ARIMA_GOF( [MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize,  double mean, double sigma, short nIntegral,
                                 [MarshalAs(UnmanagedType.LPArray)] double[] phis, IntPtr p, [MarshalAs(UnmanagedType.LPArray)] double[] thetas, IntPtr q, GOODNESS_OF_FIT_FUNC retType, ref double retVal);

    /// <summary> Returns an array of cells for the initial (non-optimal), optimal or standard errors of the model's parameters.  </summary>
    [DllImport(DLLName, EntryPoint = "#615", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_ARIMA_PARAM( [MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize,  ref double mean, ref double sigma, short nIntegral,
                                [MarshalAs(UnmanagedType.LPArray)] double[] phis, IntPtr p, [MarshalAs(UnmanagedType.LPArray)] double[] thetas, IntPtr q, MODEL_RETVAL_FUNC retType, IntPtr maxIter);

    /// <summary>
    /// Returns an array of cells for the simulated values 
    /// </summary>
    /// <param name="pData"></param>
    /// <param name="nSize"></param>
    /// <param name="mean"></param>
    /// <param name="sigma"></param>
    /// <param name="nIntegral"></param>
    /// <param name="phis"></param>
    /// <param name="p"></param>
    /// <param name="thetas"></param>
    /// <param name="q"></param>
    /// <param name="nStep"></param>
    /// <param name="nSeed"></param>
    /// <param name="retVal"></param>
    /// <param name="nSteps"></param>
    /// <returns></returns>
    [DllImport(DLLName, EntryPoint = "#614", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_ARIMA_SIM(  [MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize, double mean, double sigma, short nIntegral, 
                                [MarshalAs(UnmanagedType.LPArray)] double[] phis, IntPtr p, [MarshalAs(UnmanagedType.LPArray)] double[] thetas, IntPtr q,  IntPtr nStep , IntPtr nSeed, [MarshalAs(UnmanagedType.LPArray)] double[] retVal, IntPtr nSteps);
    /// <summary>
    /// Calculates the out-of-sample forecast statistics.
    /// </summary>
    /// <param name="pData"></param>
    /// <param name="nSize"></param>
    /// <param name="mean"></param>
    /// <param name="sigma"></param>
    /// <param name="nIntegral"></param>
    /// <param name="phis"></param>
    /// <param name="p"></param>
    /// <param name="thetas"></param>
    /// <param name="q"></param>
    /// <param name="nStep"></param>
    /// <param name="retType"></param>
    /// <param name="alpha"></param>
    /// <param name="retVal"></param>
    /// <returns></returns>
    [DllImport(DLLName, EntryPoint = "#613", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_ARIMA_FORE( [MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize, double mean, double sigma, short nIntegral,
                                [MarshalAs(UnmanagedType.LPArray)] double[] phis, IntPtr p, [MarshalAs(UnmanagedType.LPArray)] double[] thetas, IntPtr q, IntPtr nStep, FORECAST_RETVAL_FUNC retType, double alpha, ref double retVal);

    /// <summary>
    /// Returns an array of cells for the fitted values (i.e. mean, volatility and residuals)
    /// </summary>
    /// <param name="pData"></param>
    /// <param name="nSize"></param>
    /// <param name="mean"></param>
    /// <param name="sigma"></param>
    /// <param name="nIntegral"></param>
    /// <param name="phis"></param>
    /// <param name="p"></param>
    /// <param name="thetas"></param>
    /// <param name="q"></param>
    /// <param name="retType"></param>
    /// <returns></returns>
    [DllImport(DLLName, EntryPoint = "#612", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_ARIMA_FITTED( [MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize, double mean, double sigma, short nIntegral, 
                                [MarshalAs(UnmanagedType.LPArray)] double[] phis, IntPtr p, [MarshalAs(UnmanagedType.LPArray)] double[] thetas, IntPtr q,  short retType);


    // SARIMA
    /// <summary> Calculates one of the goodness-of-fit functions (e.g. LLF, AIC, BIC, etc.) of the given estimated FARIMA model </summary>
    [DllImport(DLLName, EntryPoint = "#630", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_SARIMA_GOF( [MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize, 
                              double mean, double sigma,
                              short nIntegral,
                              [MarshalAs(UnmanagedType.LPArray)] double[] phis, IntPtr p,
                              [MarshalAs(UnmanagedType.LPArray)] double[] thetas, IntPtr q, 
                              short nSIntegral,
                              [MarshalAs(UnmanagedType.LPArray)] double[] sPhis, IntPtr sP,
                              [MarshalAs(UnmanagedType.LPArray)] double[] sThetas, IntPtr sQ,
                              GOODNESS_OF_FIT_FUNC retType,
                              ref double retVal);

    /// <summary> Returns an array for the standardized residuals of a given SARIMA model </summary>
    [DllImport(DLLName, EntryPoint = "#631", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_SARIMA_RESID( [MarshalAs(UnmanagedType.LPArray)] double[] pData/*IN-OUT*/, IntPtr nSize, 
                              double mean, double sigma,
                              short nIntegral,
                              [MarshalAs(UnmanagedType.LPArray)] double[] phis, IntPtr p,
                              [MarshalAs(UnmanagedType.LPArray)] double[] thetas, IntPtr q, 
                              short nSIntegral,
                              [MarshalAs(UnmanagedType.LPArray)] double[] sPhis, IntPtr sP,
                              [MarshalAs(UnmanagedType.LPArray)] double[] sThetas, IntPtr sQ, 
                              short retType);

    [DllImport(DLLName, EntryPoint = "#635", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_SARIMA_PARAM( [MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize, 
                                ref double mean, ref double sigma,
                                short nIntegral,
                                [MarshalAs(UnmanagedType.LPArray)] double[] phis, IntPtr p,
                                [MarshalAs(UnmanagedType.LPArray)] double[] thetas, IntPtr q, 
                                short nSIntegral,
                                [MarshalAs(UnmanagedType.LPArray)] double[] sPhis, IntPtr sP,
                                [MarshalAs(UnmanagedType.LPArray)] double[] sThetas, IntPtr sQ,
                                MODEL_RETVAL_FUNC retType, 
                                IntPtr maxIter);

    [DllImport(DLLName, EntryPoint = "#634", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_SARIMA_SIM([MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize, double mean, double sigma, 
                                short nIntegral,
                                [MarshalAs(UnmanagedType.LPArray)] double[] phis, IntPtr p,
                                [MarshalAs(UnmanagedType.LPArray)] double[] thetas, IntPtr q, 
                                short nSIntegral,
                                [MarshalAs(UnmanagedType.LPArray)] double[] sPhis, IntPtr sP,
                                [MarshalAs(UnmanagedType.LPArray)] double[] sThetas, IntPtr sQ,                                 
                                IntPtr nSeed,
                                [MarshalAs(UnmanagedType.LPArray)] double[] retVal, IntPtr nStep);

    [DllImport(DLLName, EntryPoint = "#633", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_SARIMA_FORE( [MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize, double mean, double sigma, 
                                short nIntegral,
                                [MarshalAs(UnmanagedType.LPArray)] double[] phis, IntPtr p,
                                [MarshalAs(UnmanagedType.LPArray)] double[] thetas, IntPtr q, 
                                short nSIntegral,
                                [MarshalAs(UnmanagedType.LPArray)] double[] sPhis, IntPtr sP,
                                [MarshalAs(UnmanagedType.LPArray)] double[] sThetas, IntPtr sQ,
                                IntPtr nStep, FORECAST_RETVAL_FUNC retType, double alpha, ref double retVal);

    [DllImport(DLLName, EntryPoint = "#632", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_SARIMA_FITTED( [MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize, double mean, double sigma, 
                                short nIntegral,
                                [MarshalAs(UnmanagedType.LPArray)] double[] phis, IntPtr p,
                                [MarshalAs(UnmanagedType.LPArray)] double[] thetas, IntPtr q, 
                                short nSIntegral,
                                [MarshalAs(UnmanagedType.LPArray)] double[] sPhis, IntPtr sP,
                                [MarshalAs(UnmanagedType.LPArray)] double[] sThetas, IntPtr sQ,
                                FIT_RETVAL_FUNC retType);


    [DllImport(DLLName, EntryPoint = "#636", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_SARIMA_VALIDATE(double mean, double sigma,
                               short nIntegral,
                               [MarshalAs(UnmanagedType.LPArray)] double[] phis, IntPtr p,
                               [MarshalAs(UnmanagedType.LPArray)] double[] thetas, IntPtr q,
                               short nSIntegral,
                               [MarshalAs(UnmanagedType.LPArray)] double[] sPhis, IntPtr sP,
                               [MarshalAs(UnmanagedType.LPArray)] double[] sThetas, IntPtr sQ);




    // AirLine Function
    /// <summary>Calculates one of the goodness-of-fit functions (e.g. LLF, AIC, BIC, etc.) of the given estimated ARIMA model </summary>
    /// <param name="pData">is the univariate time series data (one dimensional array)</param>
    /// <param name="nSize">is number of observations in the time series input array</param>
    /// <param name="mean">is the ARMA model mean (i.e. mu).</param>
    /// <param name="sigma">is the standard deviation of the model's residuals/innovations </param>
    /// <param name="theta">is the coefficient of first-lagged innovation (see model description)</param>
    /// <param name="theta2">is the coefficient of s-lagged innovation (see model description)</param>
    /// <param name="dSeason">is the length of seasonality (expressed in terms of lags, where s > 1) </param>
    /// <param name="retType"> is a number that determines the type of return value: 1=standardized (default), 2=non-standardized. </param>    
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="SFSDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#640", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_AIRLINE_GOF([MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize, double mean, double sigma, short dSeason, double theta, double theta2, GOODNESS_OF_FIT_FUNC retType, ref double retVal);

    
    /// <summary>Calculates one of the goodness-of-fit functions (e.g. LLF, AIC, BIC, etc.) of the given estimated ARIMA model </summary>
    /// <param name="pData">is the univariate time series data (one dimensional array)</param>
    /// <param name="nSize">is number of observations in the time series input array</param>
    /// <param name="mean">is the ARMA model mean (i.e. mu).</param>
    /// <param name="sigma">is the standard deviation of the model's residuals/innovations </param>
    /// <param name="theta">is the coefficient of first-lagged innovation (see model description)</param>
    /// <param name="theta2">is the coefficient of s-lagged innovation (see model description)</param>
    /// <param name="dSeason">is the length of seasonality (expressed in terms of lags, where s > 1) </param>
    /// <param name="retType"> is a number that determines the type of return value: 1=LLF (default), 2=AIC, 3=BIC, 4=HQC, 5=R-Squared, 6=Adjusted R-Squared. </param>    
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="SFSDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#641", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_AIRLINE_RESID([MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize, double mean, double sigma, short dSeason, double theta, double theta2, RESID_RETVAL_FUNC retType);

    
    
    /// <summary>Calculates one of the goodness-of-fit functions (e.g. LLF, AIC, BIC, etc.) of the given estimated ARIMA model </summary>
    /// <param name="pData">is the univariate time series data (one dimensional array)</param>
    /// <param name="nSize">is number of observations in the time series input array</param>
    /// <param name="mean">is the ARMA model mean (i.e. mu).</param>
    /// <param name="sigma">is the standard deviation of the model's residuals/innovations </param>
    /// <param name="theta">is the coefficient of first-lagged innovation (see model description)</param>
    /// <param name="theta2">is the coefficient of s-lagged innovation (see model description)</param>
    /// <param name="dSeason">is the length of seasonality (expressed in terms of lags, where s > 1) </param>
    /// <param name="retType"> is a number that determines the type of return value: 1=Guess (default), 2=calibrate </param>    
    /// <param name="maxIter"> is a the maximum iterations used during model's calibration. </param>    
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="SFSDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#645", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_AIRLINE_PARAM([MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize, ref double mean, ref double sigma, short dSeason, ref double theta, ref double theta2, MODEL_RETVAL_FUNC retType, IntPtr maxIter);

    /// <summary>Calculates one of the goodness-of-fit functions (e.g. LLF, AIC, BIC, etc.) of the given estimated ARIMA model </summary>
    /// <param name="pData">is the univariate time series data (one dimensional array)</param>
    /// <param name="nSize">is number of observations in the time series input array</param>
    /// <param name="mean">is the ARMA model mean (i.e. mu).</param>
    /// <param name="sigma">is the standard deviation of the model's residuals/innovations </param>
    /// <param name="theta">is the coefficient of first-lagged innovation (see model description)</param>
    /// <param name="theta2">is the coefficient of s-lagged innovation (see model description)</param>
    /// <param name="dSeason">is the length of seasonality (expressed in terms of lags, where s > 1) </param>
    /// <param name="retType"> is a number that determines the type of return value: 1=Guess (default), 2=calibrate </param>    
    /// <param name="maxIter"> is a the maximum iterations used during model's calibration. </param>    
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="SFSDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#643", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_AIRLINE_FORE([MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize, double mean, double sigma, short dSeason, double theta, double theta2, IntPtr nStep, FORECAST_RETVAL_FUNC retType, double alpha, ref double retVal);


    [DllImport(DLLName, EntryPoint = "#644", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_AIRLINE_SIM([MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize, double mean, double sigma, short dSeason, double theta, double theta2, int nSeed, [MarshalAs(UnmanagedType.LPArray)] double[] retVal, IntPtr nStep);

    
    /// <summary>Calculates one of the goodness-of-fit functions (e.g. LLF, AIC, BIC, etc.) of the given estimated ARIMA model </summary>
    /// <param name="pData">is the univariate time series data (one dimensional array)</param>
    /// <param name="nSize">is number of observations in the time series input array</param>
    /// <param name="mean">is the ARMA model mean (i.e. mu).</param>
    /// <param name="sigma">is the standard deviation of the model's residuals/innovations </param>
    /// <param name="theta">is the coefficient of first-lagged innovation (see model description)</param>
    /// <param name="theta2">is the coefficient of s-lagged innovation (see model description)</param>
    /// <param name="dSeason">is the length of seasonality (expressed in terms of lags, where s > 1) </param>
    /// <param name="retType"> is a number that determines the type of return value: 1=Guess (default), 2=calibrate </param>    
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="SFSDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#642", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_AIRLINE_FITTED([MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize, double mean, double sigma, short dSeason, double theta, double theta2, FIT_RETVAL_FUNC retType);

    /// <summary>Calculates one of the goodness-of-fit functions (e.g. LLF, AIC, BIC, etc.) of the given estimated ARIMA model </summary>
    /// <param name="mean">is the ARMA model mean (i.e. mu).</param>
    /// <param name="sigma">is the standard deviation of the model's residuals/innovations </param>
    /// <param name="theta">is the coefficient of first-lagged innovation (see model description)</param>
    /// <param name="theta2">is the coefficient of s-lagged innovation (see model description)</param>
    /// <param name="dSeason">is the length of seasonality (expressed in terms of lags, where s > 1) </param>
    /// <param name="retType"> is a number that determines the type of return value: 1=Guess (default), 2=calibrate </param>    
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="SFSDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#646", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)] 
    public static extern int NDK_AIRLINE_VALIDATE(double mean, double sigma, short dSeason, double theta, double theta2);



    // GARCH Functions
    /// <summary>Calculates one of the goodness-of-fit functions (e.g. LLF, AIC, BIC, etc.) of the given estimated ARIMA model </summary>
    /// <param name="pData">is the univariate time series data (one dimensional array)</param>
    /// <param name="nSize">is number of observations in the time series input array</param>
    /// <param name="mu">is the GARCH model mean (i.e. mu).</param>
    /// <param name="Alphas">are the parameters of the ARCH(p) component model (starting with the lowest lag) </param>
    /// <param name="p">is the order of the ARCH component model </param>
    /// <param name="Betas">are the parameters of the GARCH(q) component model (starting with the lowest lag)</param>
    /// <param name="q">are the order of the GARCH component model </param>
    /// <param name="nInnovationType">is the probability distribution function of the innovations/residuals (1=Gaussian (default), 2=t-Distribution, 3=GED)</param>
    /// <param name="nu">is the shape factor (or degrees of freedom) of the innovations/residuals probability distribution function</param>
    /// <param name="retType"> is a number that determines the type of return value: 1=Guess (default), 2=calibrate </param>    
    /// <param name="retVal"> is the output value </param>    
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="SFSDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#650", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)] 
    public static extern int NDK_GARCH_GOF( [MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize,  double mu, 
                              [MarshalAs(UnmanagedType.LPArray)] double[] Alphas, IntPtr p,
                              [MarshalAs(UnmanagedType.LPArray)] double[] Betas, IntPtr q,
                              short  nInnovationType,
                              double  nu,
                              short retType,
                              ref double retVal);

    /// <summary>Returns an array of the standardized residuals for the fitted GARCH model </summary>
    /// <param name="pData">is the univariate time series data (one dimensional array)</param>
    /// <param name="nSize">is number of observations in the time series input array</param>
    /// <param name="mu">is the GARCH model mean (i.e. mu).</param>
    /// <param name="Alphas">are the parameters of the ARCH(p) component model (starting with the lowest lag) </param>
    /// <param name="p">is the order of the ARCH component model </param>
    /// <param name="Betas">are the parameters of the GARCH(q) component model (starting with the lowest lag)</param>
    /// <param name="q">are the order of the GARCH component model </param>
    /// <param name="nInnovationType">is the probability distribution function of the innovations/residuals (1=Gaussian (default), 2=t-Distribution, 3=GED)</param>
    /// <param name="nu">is the shape factor (or degrees of freedom) of the innovations/residuals probability distribution function</param>
    /// <param name="retType"> is a number that determines the type of return value: 1=Guess (default), 2=calibrate </param>    
    /// <param name="retVal"> is the output value </param>    
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="SFSDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#651", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)] 
    public static extern int NDK_GARCH_RESID( [MarshalAs(UnmanagedType.LPArray)] double[] pData/*IN-OUT*/, IntPtr nSize, double mu, 
                                              [MarshalAs(UnmanagedType.LPArray)] double[] Alphas, IntPtr p,
                                              [MarshalAs(UnmanagedType.LPArray)] double[] Betas, IntPtr q,
                                              short  nInnovationType, double  nu,
                                              short retType);

    /// <summary>Returns an array of the standardized residuals for the fitted GARCH model </summary>
    /// <param name="pData">is the univariate time series data (one dimensional array)</param>
    /// <param name="nSize">is number of observations in the time series input array</param>
    /// <param name="mu">is the GARCH model mean (i.e. mu).</param>
    /// <param name="Alphas">are the parameters of the ARCH(p) component model (starting with the lowest lag) </param>
    /// <param name="p">is the order of the ARCH component model </param>
    /// <param name="Betas">are the parameters of the GARCH(q) component model (starting with the lowest lag)</param>
    /// <param name="q">are the order of the GARCH component model </param>
    /// <param name="nInnovationType">is the probability distribution function of the innovations/residuals (1=Gaussian (default), 2=t-Distribution, 3=GED)</param>
    /// <param name="nu">is the shape factor (or degrees of freedom) of the innovations/residuals probability distribution function</param>
    /// <param name="retType"> is a number that determines the type of return value: 1=Guess (default), 2=calibrate </param>    
    /// <param name="maxIter"> is maximum number of iterations </param>    
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="SFSDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#655", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)] 
    public static extern int NDK_GARCH_PARAM( [MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize, 
                              ref double mu, 
                              [MarshalAs(UnmanagedType.LPArray)] double[] Alphas, IntPtr p,
                              [MarshalAs(UnmanagedType.LPArray)] double[] Betas, IntPtr q,
                              short  nInnovationType, ref double  nu,
                              short retType, IntPtr maxIter);

    /// <summary>Returns an array of the standardized residuals for the fitted GARCH model </summary>
    /// <param name="pData">is the univariate time series data (one dimensional array)</param>
    /// <param name="nSize">is number of observations in the time series input array</param>
    /// <param name="mu">is the GARCH model mean (i.e. mu).</param>
    /// <param name="Alphas">are the parameters of the ARCH(p) component model (starting with the lowest lag) </param>
    /// <param name="p">is the order of the ARCH component model </param>
    /// <param name="Betas">are the parameters of the GARCH(q) component model (starting with the lowest lag)</param>
    /// <param name="q">are the order of the GARCH component model </param>
    /// <param name="nInnovationType">is the probability distribution function of the innovations/residuals (1=Gaussian (default), 2=t-Distribution, 3=GED)</param>
    /// <param name="nu">is the shape factor (or degrees of freedom) of the innovations/residuals probability distribution function</param>
    /// <param name="retType"> is a number that determines the type of return value: 1=Guess (default), 2=calibrate </param>    
    /// <param name="maxIter"> is maximum number of iterations </param>    
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="SFSDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#654", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)] 
    public static extern int NDK_GARCH_SIM( [MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize, 
                                            double mu, 
                                            [MarshalAs(UnmanagedType.LPArray)] double[] Alphas, IntPtr p,
                                            [MarshalAs(UnmanagedType.LPArray)] double[] Betas, IntPtr q,
                                            short  nInnovationType, double  nu,
                                            IntPtr nStep , IntPtr nSeed, ref double retVal);

    /// <summary>Returns an array of the standardized residuals for the fitted GARCH model </summary>
    /// <param name="pData">is the univariate time series data (one dimensional array)</param>
    /// <param name="nSize">is number of observations in the time series input array</param>
    /// <param name="mu">is the GARCH model mean (i.e. mu).</param>
    /// <param name="Alphas">are the parameters of the ARCH(p) component model (starting with the lowest lag) </param>
    /// <param name="p">is the order of the ARCH component model </param>
    /// <param name="Betas">are the parameters of the GARCH(q) component model (starting with the lowest lag)</param>
    /// <param name="q">are the order of the GARCH component model </param>
    /// <param name="nInnovationType">is the probability distribution function of the innovations/residuals (1=Gaussian (default), 2=t-Distribution, 3=GED)</param>
    /// <param name="nu">is the shape factor (or degrees of freedom) of the innovations/residuals probability distribution function</param>
    /// <param name="retType"> is a number that determines the type of return value: 1=Guess (default), 2=calibrate </param>    
    /// <param name="retVal"> is the output value </param>    
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="SFSDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#653", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)] 
    public static extern int NDK_GARCH_FORE( [MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize,  
                              [MarshalAs(UnmanagedType.LPArray)] double[] pVols, IntPtr nVolSize,
                              double mu, 
                              [MarshalAs(UnmanagedType.LPArray)] double[] Alphas, IntPtr p,
                              [MarshalAs(UnmanagedType.LPArray)] double[] Betas, IntPtr q,
                              short  nInnovationType, double nu,
                              IntPtr nSteps, short retType, ref double retVal);

    /// <summary>Returns an array of the standardized residuals for the fitted GARCH model </summary>
    /// <param name="pData">is the univariate time series data (one dimensional array)</param>
    /// <param name="nSize">is number of observations in the time series input array</param>
    /// <param name="mu">is the GARCH model mean (i.e. mu).</param>
    /// <param name="Alphas">are the parameters of the ARCH(p) component model (starting with the lowest lag) </param>
    /// <param name="p">is the order of the ARCH component model </param>
    /// <param name="Betas">are the parameters of the GARCH(q) component model (starting with the lowest lag)</param>
    /// <param name="q">are the order of the GARCH component model </param>
    /// <param name="nInnovationType">is the probability distribution function of the innovations/residuals (1=Gaussian (default), 2=t-Distribution, 3=GED)</param>
    /// <param name="nu">is the shape factor (or degrees of freedom) of the innovations/residuals probability distribution function</param>
    /// <param name="retType"> is a number that determines the type of return value: 1=Guess (default), 2=calibrate </param>    
    /// <param name="retVal"> is the output value </param>    
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="SFSDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#652", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)] 
    public static extern int NDK_GARCH_FITTED( [MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize, 
                              double mu, 
                              [MarshalAs(UnmanagedType.LPArray)] double[] Alphas, IntPtr p,
                              [MarshalAs(UnmanagedType.LPArray)] double[] Betas, IntPtr q,
                              short  nInnovationType,
                              double  nu,
                              short retType);

    /// <summary>Returns an array of the standardized residuals for the fitted GARCH model </summary>
    /// <param name="pData">is the univariate time series data (one dimensional array)</param>
    /// <param name="nSize">is number of observations in the time series input array</param>
    /// <param name="mu">is the GARCH model mean (i.e. mu).</param>
    /// <param name="Alphas">are the parameters of the ARCH(p) component model (starting with the lowest lag) </param>
    /// <param name="p">is the order of the ARCH component model </param>
    /// <param name="Betas">are the parameters of the GARCH(q) component model (starting with the lowest lag)</param>
    /// <param name="q">are the order of the GARCH component model </param>
    /// <param name="nInnovationType">is the probability distribution function of the innovations/residuals (1=Gaussian (default), 2=t-Distribution, 3=GED)</param>
    /// <param name="nu">is the shape factor (or degrees of freedom) of the innovations/residuals probability distribution function</param>
    /// <param name="retType"> is a number that determines the type of return value: 1=Guess (default), 2=calibrate </param>    
    /// <param name="retVal"> is the output value </param>    
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="SFSDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#657", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)] 
    public static extern int NDK_GARCH_LRVAR(double mu,  [MarshalAs(UnmanagedType.LPArray)] double[] Alphas, IntPtr p,
                              [MarshalAs(UnmanagedType.LPArray)] double[] Betas, IntPtr q,
                              short  nInnovationType,
                              double  nu, 
                              ref double retVal);
    /// <summary>Returns an array of the standardized residuals for the fitted GARCH model </summary>
    /// <param name="pData">is the univariate time series data (one dimensional array)</param>
    /// <param name="nSize">is number of observations in the time series input array</param>
    /// <param name="mu">is the GARCH model mean (i.e. mu).</param>
    /// <param name="Alphas">are the parameters of the ARCH(p) component model (starting with the lowest lag) </param>
    /// <param name="p">is the order of the ARCH component model </param>
    /// <param name="Betas">are the parameters of the GARCH(q) component model (starting with the lowest lag)</param>
    /// <param name="q">are the order of the GARCH component model </param>
    /// <param name="nInnovationType">is the probability distribution function of the innovations/residuals (1=Gaussian (default), 2=t-Distribution, 3=GED)</param>
    /// <param name="nu">is the shape factor (or degrees of freedom) of the innovations/residuals probability distribution function</param>
    /// <param name="retType"> is a number that determines the type of return value: 1=Guess (default), 2=calibrate </param>    
    /// <param name="retVal"> is the output value </param>    
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="SFSDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#656", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)] 
    public static extern int NDK_GARCH_VALIDATE(double mu,  [MarshalAs(UnmanagedType.LPArray)] double[] alpha, IntPtr p, 
                                                            [MarshalAs(UnmanagedType.LPArray)] double[] betas, IntPtr q, 
                                                            short  nInnovationType, double  nu);

    // EGARCH Functions
    /// <summary>Returns an array of the standardized residuals for the fitted GARCH model </summary>
    /// <param name="pData">is the univariate time series data (one dimensional array)</param>
    /// <param name="nSize">is number of observations in the time series input array</param>
    /// <param name="mu">is the GARCH model mean (i.e. mu).</param>
    /// <param name="Alphas">are the parameters of the ARCH(p) component model (starting with the lowest lag) </param>
    /// <param name="Gammas">are the leverage parameters (starting with the lowest lag). The number of gamma-coefficients must match the number of alpha-coefficients </param>
    /// <param name="p">is the order of the ARCH component model </param>
    /// <param name="Betas">are the parameters of the GARCH(q) component model (starting with the lowest lag)</param>
    /// <param name="q">are the order of the GARCH component model </param>
    /// <param name="nInnovationType">is the probability distribution function of the innovations/residuals (1=Gaussian (default), 2=t-Distribution, 3=GED)</param>
    /// <param name="nu">is the shape factor (or degrees of freedom) of the innovations/residuals probability distribution function</param>
    /// <param name="retType"> is a number that determines the type of return value: 1=Guess (default), 2=calibrate </param>    
    /// <param name="retVal"> is the output value </param>    
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="SFSDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#660", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)] 
    public static extern int NDK_EGARCH_GOF( [MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize, 
                              double mu, 
                              [MarshalAs(UnmanagedType.LPArray)] double[] Alphas, [MarshalAs(UnmanagedType.LPArray)] double[] Gammas, IntPtr p,
                              [MarshalAs(UnmanagedType.LPArray)] double[] Betas, IntPtr q,
                              short  nInnovationType,
                              double  nu,
                              short retType,
                              ref double retVal);

    /// <summary>Returns an array of the standardized residuals for the fitted GARCH model </summary>
    /// <param name="pData">is the univariate time series data (one dimensional array)</param>
    /// <param name="nSize">is number of observations in the time series input array</param>
    /// <param name="mu">is the GARCH model mean (i.e. mu).</param>
    /// <param name="Alphas">are the parameters of the ARCH(p) component model (starting with the lowest lag) </param>
    /// <param name="Gammas">are the leverage parameters (starting with the lowest lag). The number of gamma-coefficients must match the number of alpha-coefficients </param>
    /// <param name="p">is the order of the ARCH component model </param>
    /// <param name="Betas">are the parameters of the GARCH(q) component model (starting with the lowest lag)</param>
    /// <param name="q">are the order of the GARCH component model </param>
    /// <param name="nInnovationType">is the probability distribution function of the innovations/residuals (1=Gaussian (default), 2=t-Distribution, 3=GED)</param>
    /// <param name="nu">is the shape factor (or degrees of freedom) of the innovations/residuals probability distribution function</param>
    /// <param name="retType"> is a number that determines the type of return value: 1=Guess (default), 2=calibrate </param>    
    /// <param name="retVal"> is the output value </param>    
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="SFSDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#661", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)] 
    public static extern int NDK_EGARCH_RESID( [MarshalAs(UnmanagedType.LPArray)] double[] pData/*IN-OUT*/, IntPtr nSize, 
                              double mu, 
                              [MarshalAs(UnmanagedType.LPArray)] double[] Alphas, [MarshalAs(UnmanagedType.LPArray)] double[] Gammas, IntPtr p,
                              [MarshalAs(UnmanagedType.LPArray)] double[] Betas, IntPtr q,
                              short  nInnovationType,
                              double  nu,
                              short retType);


    /// <summary>Returns an array of the standardized residuals for the fitted GARCH model </summary>
    /// <param name="pData">is the univariate time series data (one dimensional array)</param>
    /// <param name="nSize">is number of observations in the time series input array</param>
    /// <param name="mu">is the GARCH model mean (i.e. mu).</param>
    /// <param name="Alphas">are the parameters of the ARCH(p) component model (starting with the lowest lag) </param>
    /// <param name="Gammas">are the leverage parameters (starting with the lowest lag). The number of gamma-coefficients must match the number of alpha-coefficients </param>
    /// <param name="p">is the order of the ARCH component model </param>
    /// <param name="Betas">are the parameters of the GARCH(q) component model (starting with the lowest lag)</param>
    /// <param name="q">are the order of the GARCH component model </param>
    /// <param name="nInnovationType">is the probability distribution function of the innovations/residuals (1=Gaussian (default), 2=t-Distribution, 3=GED)</param>
    /// <param name="nu">is the shape factor (or degrees of freedom) of the innovations/residuals probability distribution function</param>
    /// <param name="retType"> is a number that determines the type of return value: 1=Guess (default), 2=calibrate </param>    
    /// <param name="retVal"> is the output value </param>    
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="SFSDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#665", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)] 
    public static extern int NDK_EGARCH_PARAM( [MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize, 
                              ref double mean, 
                              [MarshalAs(UnmanagedType.LPArray)] double[] Alphas, 
                              [MarshalAs(UnmanagedType.LPArray)] double[] Gammas, IntPtr p,
                              [MarshalAs(UnmanagedType.LPArray)] double[] Betas, IntPtr q,
                              short  nInnovationType,
                              ref double  nu,
                              short retType, IntPtr maxIter);


    /// <summary>Returns an array of the standardized residuals for the fitted GARCH model </summary>
    /// <param name="pData">is the univariate time series data (one dimensional array)</param>
    /// <param name="nSize">is number of observations in the time series input array</param>
    /// <param name="mu">is the GARCH model mean (i.e. mu).</param>
    /// <param name="Alphas">are the parameters of the ARCH(p) component model (starting with the lowest lag) </param>
    /// <param name="Gammas">are the leverage parameters (starting with the lowest lag). The number of gamma-coefficients must match the number of alpha-coefficients </param>
    /// <param name="p">is the order of the ARCH component model </param>
    /// <param name="Betas">are the parameters of the GARCH(q) component model (starting with the lowest lag)</param>
    /// <param name="q">are the order of the GARCH component model </param>
    /// <param name="nInnovationType">is the probability distribution function of the innovations/residuals (1=Gaussian (default), 2=t-Distribution, 3=GED)</param>
    /// <param name="nu">is the shape factor (or degrees of freedom) of the innovations/residuals probability distribution function</param>
    /// <param name="retType"> is a number that determines the type of return value: 1=Guess (default), 2=calibrate </param>    
    /// <param name="retVal"> is the output value </param>    
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="SFSDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#664", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)] 
    public static extern int NDK_EGARCH_SIM([MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize, 
                              double mu, 
                              [MarshalAs(UnmanagedType.LPArray)] double[] Alphas, 
                              [MarshalAs(UnmanagedType.LPArray)] double[] Gammas, IntPtr p,
                              [MarshalAs(UnmanagedType.LPArray)] double[] Betas, IntPtr q,
                              short  nInnovationType,
                              double  nu,
                              IntPtr nStep , IntPtr nSeed, ref double retVal);

    /// <summary>Returns an array of the standardized residuals for the fitted GARCH model </summary>
    /// <param name="pData">is the univariate time series data (one dimensional array)</param>
    /// <param name="nSize">is number of observations in the time series input array</param>
    /// <param name="mu">is the GARCH model mean (i.e. mu).</param>
    /// <param name="Alphas">are the parameters of the ARCH(p) component model (starting with the lowest lag) </param>
    /// <param name="Gammas">are the leverage parameters (starting with the lowest lag). The number of gamma-coefficients must match the number of alpha-coefficients </param>
    /// <param name="p">is the order of the ARCH component model </param>
    /// <param name="Betas">are the parameters of the GARCH(q) component model (starting with the lowest lag)</param>
    /// <param name="q">are the order of the GARCH component model </param>
    /// <param name="nInnovationType">is the probability distribution function of the innovations/residuals (1=Gaussian (default), 2=t-Distribution, 3=GED)</param>
    /// <param name="nu">is the shape factor (or degrees of freedom) of the innovations/residuals probability distribution function</param>
    /// <param name="retType"> is a number that determines the type of return value: 1=Guess (default), 2=calibrate </param>    
    /// <param name="retVal"> is the output value </param>    
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="SFSDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#663", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)] 
    public static extern int NDK_EGARCH_FORE( [MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize, 
                              [MarshalAs(UnmanagedType.LPArray)] double[] pVols, IntPtr nVolSize,
                              double mu, 
                              [MarshalAs(UnmanagedType.LPArray)] double[] Alphas, 
                              [MarshalAs(UnmanagedType.LPArray)] double[] Gammas, IntPtr p,
                              [MarshalAs(UnmanagedType.LPArray)] double[] Betas, IntPtr q,
                              short  nInnovationType,
                              double  nu,
                              IntPtr nSteps , short retType, ref double retVal);

    /// <summary>Returns an array of the standardized residuals for the fitted GARCH model </summary>
    /// <param name="pData">is the univariate time series data (one dimensional array)</param>
    /// <param name="nSize">is number of observations in the time series input array</param>
    /// <param name="mu">is the GARCH model mean (i.e. mu).</param>
    /// <param name="Alphas">are the parameters of the ARCH(p) component model (starting with the lowest lag) </param>
    /// <param name="Gammas">are the leverage parameters (starting with the lowest lag). The number of gamma-coefficients must match the number of alpha-coefficients </param>
    /// <param name="p">is the order of the ARCH component model </param>
    /// <param name="Betas">are the parameters of the GARCH(q) component model (starting with the lowest lag)</param>
    /// <param name="q">are the order of the GARCH component model </param>
    /// <param name="nInnovationType">is the probability distribution function of the innovations/residuals (1=Gaussian (default), 2=t-Distribution, 3=GED)</param>
    /// <param name="nu">is the shape factor (or degrees of freedom) of the innovations/residuals probability distribution function</param>
    /// <param name="retType"> is a number that determines the type of return value: 1=Guess (default), 2=calibrate </param>    
    /// <param name="retVal"> is the output value </param>    
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="SFSDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#662", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)] 
    public static extern int NDK_EGARCH_FITTED( [MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize, 
                              double mu, 
                              [MarshalAs(UnmanagedType.LPArray)] double[] Alphas, [MarshalAs(UnmanagedType.LPArray)] double[] Gammas, IntPtr p,
                              [MarshalAs(UnmanagedType.LPArray)] double[] Betas, IntPtr q,
                              short  nInnovationType,
                              double  nu,
                              short retType);

    /// <summary>Returns an array of the standardized residuals for the fitted GARCH model </summary>
    /// <param name="mu">is the GARCH model mean (i.e. mu).</param>
    /// <param name="Alphas">are the parameters of the ARCH(p) component model (starting with the lowest lag) </param>
    /// <param name="Gammas">are the leverage parameters (starting with the lowest lag). The number of gamma-coefficients must match the number of alpha-coefficients </param>
    /// <param name="p">is the order of the ARCH component model </param>
    /// <param name="Betas">are the parameters of the GARCH(q) component model (starting with the lowest lag)</param>
    /// <param name="q">are the order of the GARCH component model </param>
    /// <param name="nInnovationType">is the probability distribution function of the innovations/residuals (1=Gaussian (default), 2=t-Distribution, 3=GED)</param>
    /// <param name="nu">is the shape factor (or degrees of freedom) of the innovations/residuals probability distribution function</param>
    /// <param name="retType"> is a number that determines the type of return value: 1=Guess (default), 2=calibrate </param>    
    /// <param name="retVal"> is the output value </param>    
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="SFSDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#667", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)] 
    public static extern int NDK_EGARCH_LRVAR(double mu,  [MarshalAs(UnmanagedType.LPArray)] double[] Alphas, [MarshalAs(UnmanagedType.LPArray)] double[] Gammas, IntPtr p,
                              [MarshalAs(UnmanagedType.LPArray)] double[] Betas, IntPtr q,
                              short  nInnovationType,
                              double  nu, ref double retVal);

    /// <summary>Returns an array of the standardized residuals for the fitted GARCH model </summary>
    /// <param name="mu">is the GARCH model mean (i.e. mu).</param>
    /// <param name="Alphas">are the parameters of the ARCH(p) component model (starting with the lowest lag) </param>
    /// <param name="Gammas">are the leverage parameters (starting with the lowest lag). The number of gamma-coefficients must match the number of alpha-coefficients </param>
    /// <param name="p">is the order of the ARCH component model </param>
    /// <param name="Betas">are the parameters of the GARCH(q) component model (starting with the lowest lag)</param>
    /// <param name="q">are the order of the GARCH component model </param>
    /// <param name="nInnovationType">is the probability distribution function of the innovations/residuals (1=Gaussian (default), 2=t-Distribution, 3=GED)</param>
    /// <param name="nu">is the shape factor (or degrees of freedom) of the innovations/residuals probability distribution function</param>
    /// <param name="retType"> is a number that determines the type of return value: 1=Guess (default), 2=calibrate </param>    
    /// <param name="retVal"> is the output value </param>    
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="SFSDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#666", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)] 
    public static extern int NDK_EGARCHM_VALIDATE(double mu,  [MarshalAs(UnmanagedType.LPArray)] double[] alpha, [MarshalAs(UnmanagedType.LPArray)] double[] gammas, IntPtr p, 
            [MarshalAs(UnmanagedType.LPArray)] double[] betas, IntPtr q, short  nInnovationType, double  nu);


    // GARCH-M Functions
    /// <summary>Returns an array of the standardized residuals for the fitted GARCH model </summary>
    /// <param name="mu">is the GARCH model mean (i.e. mu).</param>
    /// <param name="Alphas">are the parameters of the ARCH(p) component model (starting with the lowest lag) </param>
    /// <param name="Gammas">are the leverage parameters (starting with the lowest lag). The number of gamma-coefficients must match the number of alpha-coefficients </param>
    /// <param name="p">is the order of the ARCH component model </param>
    /// <param name="Betas">are the parameters of the GARCH(q) component model (starting with the lowest lag)</param>
    /// <param name="q">are the order of the GARCH component model </param>
    /// <param name="nInnovationType">is the probability distribution function of the innovations/residuals (1=Gaussian (default), 2=t-Distribution, 3=GED)</param>
    /// <param name="nu">is the shape factor (or degrees of freedom) of the innovations/residuals probability distribution function</param>
    /// <param name="retType"> is a number that determines the type of return value: 1=Guess (default), 2=calibrate </param>    
    /// <param name="retVal"> is the output value </param>    
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="SFSDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#670", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)] 
    public static extern int NDK_GARCHM_GOF( [MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize, 
                              double mu, 
                              double flambda,
                              [MarshalAs(UnmanagedType.LPArray)] double[] Alphas, IntPtr p,
                              [MarshalAs(UnmanagedType.LPArray)] double[] Betas, IntPtr q,
                              short  nInnovationType,
                              double  nu,
                              short retType,
                              ref double retVal);

    /// <summary>Returns an array of the standardized residuals for the fitted GARCH model </summary>
    /// <param name="mu">is the GARCH model mean (i.e. mu).</param>
    /// <param name="Alphas">are the parameters of the ARCH(p) component model (starting with the lowest lag) </param>
    /// <param name="Gammas">are the leverage parameters (starting with the lowest lag). The number of gamma-coefficients must match the number of alpha-coefficients </param>
    /// <param name="p">is the order of the ARCH component model </param>
    /// <param name="Betas">are the parameters of the GARCH(q) component model (starting with the lowest lag)</param>
    /// <param name="q">are the order of the GARCH component model </param>
    /// <param name="nInnovationType">is the probability distribution function of the innovations/residuals (1=Gaussian (default), 2=t-Distribution, 3=GED)</param>
    /// <param name="nu">is the shape factor (or degrees of freedom) of the innovations/residuals probability distribution function</param>
    /// <param name="retType"> is a number that determines the type of return value: 1=Guess (default), 2=calibrate </param>    
    /// <param name="retVal"> is the output value </param>    
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="SFSDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#671", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)] 
    public static extern int NDK_GARCHM_RESID([MarshalAs(UnmanagedType.LPArray)] double[] pData/*IN-OUT*/, IntPtr nSize, 
                              double mu, 
                              double flambda,
                              [MarshalAs(UnmanagedType.LPArray)] double[] Alphas, IntPtr p,
                              [MarshalAs(UnmanagedType.LPArray)] double[] Betas, IntPtr q,
                              short  nInnovationType,
                              double  nu,
                              short retType);

    /// <summary>Returns an array of the standardized residuals for the fitted GARCH model </summary>
    /// <param name="mu">is the GARCH model mean (i.e. mu).</param>
    /// <param name="Alphas">are the parameters of the ARCH(p) component model (starting with the lowest lag) </param>
    /// <param name="Gammas">are the leverage parameters (starting with the lowest lag). The number of gamma-coefficients must match the number of alpha-coefficients </param>
    /// <param name="p">is the order of the ARCH component model </param>
    /// <param name="Betas">are the parameters of the GARCH(q) component model (starting with the lowest lag)</param>
    /// <param name="q">are the order of the GARCH component model </param>
    /// <param name="nInnovationType">is the probability distribution function of the innovations/residuals (1=Gaussian (default), 2=t-Distribution, 3=GED)</param>
    /// <param name="nu">is the shape factor (or degrees of freedom) of the innovations/residuals probability distribution function</param>
    /// <param name="retType"> is a number that determines the type of return value: 1=Guess (default), 2=calibrate </param>    
    /// <param name="retVal"> is the output value </param>    
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="SFSDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#675", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)] 
    public static extern int NDK_GARCHM_PARAM( [MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize, 
                              ref double mu, 
                              ref double flambda,
                              [MarshalAs(UnmanagedType.LPArray)] double[] Alphas, IntPtr p,
                              [MarshalAs(UnmanagedType.LPArray)] double[] Betas, IntPtr q,
                              short  nInnovationType,
                              ref double  nu,
                              short retType, IntPtr maxIter);

    /// <summary>Returns an array of the standardized residuals for the fitted GARCH model </summary>
    /// <param name="mu">is the GARCH model mean (i.e. mu).</param>
    /// <param name="Alphas">are the parameters of the ARCH(p) component model (starting with the lowest lag) </param>
    /// <param name="Gammas">are the leverage parameters (starting with the lowest lag). The number of gamma-coefficients must match the number of alpha-coefficients </param>
    /// <param name="p">is the order of the ARCH component model </param>
    /// <param name="Betas">are the parameters of the GARCH(q) component model (starting with the lowest lag)</param>
    /// <param name="q">are the order of the GARCH component model </param>
    /// <param name="nInnovationType">is the probability distribution function of the innovations/residuals (1=Gaussian (default), 2=t-Distribution, 3=GED)</param>
    /// <param name="nu">is the shape factor (or degrees of freedom) of the innovations/residuals probability distribution function</param>
    /// <param name="retType"> is a number that determines the type of return value: 1=Guess (default), 2=calibrate </param>    
    /// <param name="retVal"> is the output value </param>    
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="SFSDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#674", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)] 
    public static extern int NDK_GARCHM_SIM(  [MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize, 
                              double mu, 
                              double flambda,
                              [MarshalAs(UnmanagedType.LPArray)] double[] Alphas, IntPtr p,
                              [MarshalAs(UnmanagedType.LPArray)] double[] Betas, IntPtr q,
                              short  nInnovationType,
                              double  nu,
                              IntPtr nStep , IntPtr nSeed, ref double retVal);

    /// <summary>Returns an array of the standardized residuals for the fitted GARCH model </summary>
    /// <param name="mu">is the GARCH model mean (i.e. mu).</param>
    /// <param name="Alphas">are the parameters of the ARCH(p) component model (starting with the lowest lag) </param>
    /// <param name="Gammas">are the leverage parameters (starting with the lowest lag). The number of gamma-coefficients must match the number of alpha-coefficients </param>
    /// <param name="p">is the order of the ARCH component model </param>
    /// <param name="Betas">are the parameters of the GARCH(q) component model (starting with the lowest lag)</param>
    /// <param name="q">are the order of the GARCH component model </param>
    /// <param name="nInnovationType">is the probability distribution function of the innovations/residuals (1=Gaussian (default), 2=t-Distribution, 3=GED)</param>
    /// <param name="nu">is the shape factor (or degrees of freedom) of the innovations/residuals probability distribution function</param>
    /// <param name="retType"> is a number that determines the type of return value: 1=Guess (default), 2=calibrate </param>    
    /// <param name="retVal"> is the output value </param>    
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="SFSDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#673", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)] 
    public static extern int NDK_GARCHM_FORE([MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize, 
                                [MarshalAs(UnmanagedType.LPArray)] double[] pVols, IntPtr npVolSize,
                              double mu, 
                              double flambda,
                              [MarshalAs(UnmanagedType.LPArray)] double[] Alphas, IntPtr p,
                              [MarshalAs(UnmanagedType.LPArray)] double[] Betas, IntPtr q,
                              short  nInnovationType,
                              double  nu,
                              IntPtr nStep , short retType, ref double retVal);

    /// <summary>Returns an array of the standardized residuals for the fitted GARCH model </summary>
    /// <param name="mu">is the GARCH model mean (i.e. mu).</param>
    /// <param name="Alphas">are the parameters of the ARCH(p) component model (starting with the lowest lag) </param>
    /// <param name="Gammas">are the leverage parameters (starting with the lowest lag). The number of gamma-coefficients must match the number of alpha-coefficients </param>
    /// <param name="p">is the order of the ARCH component model </param>
    /// <param name="Betas">are the parameters of the GARCH(q) component model (starting with the lowest lag)</param>
    /// <param name="q">are the order of the GARCH component model </param>
    /// <param name="nInnovationType">is the probability distribution function of the innovations/residuals (1=Gaussian (default), 2=t-Distribution, 3=GED)</param>
    /// <param name="nu">is the shape factor (or degrees of freedom) of the innovations/residuals probability distribution function</param>
    /// <param name="retType"> is a number that determines the type of return value: 1=Guess (default), 2=calibrate </param>    
    /// <param name="retVal"> is the output value </param>    
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="SFSDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#672", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)] 
    public static extern int NDK_GARCHM_FITTED( [MarshalAs(UnmanagedType.LPArray)] double[] pData, IntPtr nSize, 
                              double mu, 
                              double flambda,
                              [MarshalAs(UnmanagedType.LPArray)] double[] Alphas, IntPtr p,
                              [MarshalAs(UnmanagedType.LPArray)] double[] Betas, IntPtr q,
                              short  nInnovationType,
                              double  nu,
                              short retType);

    /// <summary>Returns an array of the standardized residuals for the fitted GARCH model </summary>
    /// <param name="mu">is the GARCH model mean (i.e. mu).</param>
    /// <param name="Alphas">are the parameters of the ARCH(p) component model (starting with the lowest lag) </param>
    /// <param name="Gammas">are the leverage parameters (starting with the lowest lag). The number of gamma-coefficients must match the number of alpha-coefficients </param>
    /// <param name="p">is the order of the ARCH component model </param>
    /// <param name="Betas">are the parameters of the GARCH(q) component model (starting with the lowest lag)</param>
    /// <param name="q">are the order of the GARCH component model </param>
    /// <param name="nInnovationType">is the probability distribution function of the innovations/residuals (1=Gaussian (default), 2=t-Distribution, 3=GED)</param>
    /// <param name="nu">is the shape factor (or degrees of freedom) of the innovations/residuals probability distribution function</param>
    /// <param name="retType"> is a number that determines the type of return value: 1=Guess (default), 2=calibrate </param>    
    /// <param name="retVal"> is the output value </param>    
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="SFSDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#677", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)] 
    public static extern int NDK_GARCHM_LRVAR(double mu, double lambda, [MarshalAs(UnmanagedType.LPArray)] double[] Alphas, IntPtr p,
                              [MarshalAs(UnmanagedType.LPArray)] double[] Betas, IntPtr q,
                              short  nInnovationType,
                              double  nu, ref double retVal);

    /// <summary>Returns an array of the standardized residuals for the fitted GARCH model </summary>
    /// <param name="mu">is the GARCH model mean (i.e. mu).</param>
    /// <param name="Alphas">are the parameters of the ARCH(p) component model (starting with the lowest lag) </param>
    /// <param name="Gammas">are the leverage parameters (starting with the lowest lag). The number of gamma-coefficients must match the number of alpha-coefficients </param>
    /// <param name="p">is the order of the ARCH component model </param>
    /// <param name="Betas">are the parameters of the GARCH(q) component model (starting with the lowest lag)</param>
    /// <param name="q">are the order of the GARCH component model </param>
    /// <param name="nInnovationType">is the probability distribution function of the innovations/residuals (1=Gaussian (default), 2=t-Distribution, 3=GED)</param>
    /// <param name="nu">is the shape factor (or degrees of freedom) of the innovations/residuals probability distribution function</param>
    /// <param name="retType"> is a number that determines the type of return value: 1=Guess (default), 2=calibrate </param>    
    /// <param name="retVal"> is the output value </param>    
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="SFSDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#676", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int NDK_GARCHM_VALIDATE(double mu, double lambda, [MarshalAs(UnmanagedType.LPArray)] double[] alpha, IntPtr p, [MarshalAs(UnmanagedType.LPArray)] double[] betas, IntPtr q, short nInnovationType, double nu);



    // Utilities


    [DllImport(DLLName, EntryPoint = "#3015", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern NDK_RETCODE NDK_REGEX_REPLACE(String szLine, string szKey, string szValue, Boolean ignoreCase, Boolean global, StringBuilder pRetVal, IntPtr nSize);




    /// <summary>
    /// calculates the value of the regression function for an intermediate x-value
    /// </summary>
    /// <param name="X"></param>
    /// <param name="nX"></param>
    /// <param name="Y"></param>
    /// <param name="nY"></param>
    /// <param name="nRegressType">is the model description flag for the trend function (1 = Linear (default), 2 = Polynomial, 3 = Exponential, 4 = Logarithmic, 5 = Power). </param>
    /// <param name="POrder">is the polynomial order. This is only relevant for a polynomial type of trend and is ignored for all others. If missing, POrder = 1.</param>
    /// <param name="intercept">is the constant or the intercept value to fix (e.g. zero). If missing (NaN), an intercept will not be fixed and is computed normally.</param>
    /// <param name="target"></param>
    /// <param name="nRetType">is a switch to select the return output (1 = Forecast value (default), 2 = Upper limit, 3 = Lower Limit, 4 = R-Squared). </param>
    /// <param name="alpha"></param>
    /// <param name="retVal"></param>
    /// <returns></returns>
    [DllImport(DLLName, EntryPoint = "#3005", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto)]
    public static extern NDK_RETCODE NDK_REGRESSION(double[] X, IntPtr nX, 
                                            double[] Y, IntPtr nY,
                                            UInt16 nRegressType,
                                            UInt16 POrder,
                                            double intercept,
                                            double target,
                                            UInt16 nRetType,
                                            double alpha,
                                            ref double retVal);
      



  }

}
