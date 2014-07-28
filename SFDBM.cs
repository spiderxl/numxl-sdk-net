#region License
// <copyright file="SFDBM.cs" company="Spider Financial Corp">
//  (c) 2007-2014 Spider Financial Corp.
//  All rights reserved.
// </copyright>
//  
//  $Revision: 14816 $
//  $Date: 2014-07-23 19:34:13 -0500 (Wed, 23 Jul 2014) $
//  $Author: mohamad $
//  $Id: SFDBM.cs 14816 2014-07-24 00:34:13Z mohamad $ 
//  $HeadURL: https://secure.svnrepository.com/s_oliver/spiderxl/Branches/1.63SHAMROCK/products/NumXL-SDK.Net/NumXLAPI/SFDBM.cs $
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
  /// Warpper class for C-API in SFDBM.dll.
  /// The SFDBM API provide calendar and holidays support function.
  /// </summary>
  public class SFDBM
  {
    const string DLLName="SFDBM.dll";


    /// <summary>
    /// Initialize and validate license for the SFDBM DLL.
    /// </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#100", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    private static extern int SFDB_Init(string szAppName, string szKey, string szActCode, string szLogDir);

    /// <summary>
    /// Wrap the NDK_INIT API function with a CLS compliant function
    /// Initialize and validate license for the SFDBM DLL.
    /// </summary>
    /// <param name="szAppName">User-defined application name</param>
    /// <param name="szKey">NumXL License key</param>
    /// <param name="szActCode">Activation code</param>
    /// <param name="szLogDir">Temporary files and Logging directory</param>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    public static NDK_RETCODE Init(string szAppName, string szKey, string szActCode, string szLogDir)
    {
      int nRet = SFDB_Init(szAppName, szKey, szActCode, szLogDir);

      return (NDK_RETCODE)nRet;
    }


    /// <summary>
    /// Shutdown and release any resources allocated for SFDBM DLL.
    /// </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#105", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    private static extern int SFDB_Shutdown();


    /// <summary>
    /// Wrap the SFDB_Shutdown API function with a CLS compliant function
    /// shutdown and release resources used by SFDBM Library.
    /// </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    public static NDK_RETCODE Shutdown()
    {
      int nRet = SFDB_Shutdown();

      return (NDK_RETCODE)nRet;
    }

    /// <summary>
    /// compute the date after a given a period (e.g. 1w, 1m, 3w, etc.).
    /// </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#200", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int SFDB_EDATE(long argDate, string szPeriod, ref int retVal);

    /// <summary>
    /// Wrap the SFDB_EDATE API function with a CLS compliant function
    /// compute the date after a given a period (e.g. 1w, 1m, 3w, etc.).
    /// </summary>
    /// <param name="argDate">start date</param>
    /// <param name="szPeriod">description of a period range (e.g. "5-days", "1 month", "1m", "3 w", "-1 year").</param>
    /// <param name="retVal">the calculated date that falls after the given period.</param>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    public static NDK_RETCODE EDATE(System.DateTime argDate, string szPeriod, ref System.DateTime retVal)
    {
      int startdt = (int)(argDate.ToOADate());
      int endDate = 0;

      int nRet = SFDB_EDATE(startdt, szPeriod, ref endDate);
      if (nRet >= (int)NDK_RETCODE.NDK_SUCCESS)
      {
        retVal = System.DateTime.FromOADate((double)(endDate));
      }

      return (NDK_RETCODE)nRet;
    }


    /// <summary>
    /// Returns the serial number of the n-th weekday in a month.
    /// </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#201", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    private static extern int SFDB_NWKDAY(UInt16 weekdy, short order, UInt16 mnth, UInt16 year, ref long retVal);


    /// <summary>
    /// Wrap the SFDB_NWKDAY API function with a CLS compliant function
    /// Returns the date of the n-th weekday in a month.
    /// </summary>
    /// <param name="weekdy">the weekday (i.e. 1=Sunday, 2=Monday, 3=Tuesday, ..., 7=Saturday)</param>
    /// <param name="order">the n-th occurence in the month (i.e. 1=First, 2=Second, 3=Third, 4=Fourth and -1=Last).</param>
    /// <param name="mnth">the month in the year expressed as a number (i.e. 1=Jan, 2=Feb, ..., 12=Dec).</param>
    /// <param name="year">the target year (2 or 4 digits) (e.g. 99, 2007, 2008, 09, etc.).</param>
    /// <param name="retVal">the calculated date of the n-th weekday in a month/year</param>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    public static NDK_RETCODE NWKDAY(UInt16 weekdy, short order, UInt16 mnth, UInt16 year, ref System.DateTime retVal)
    {
      long endDate = 0;

      int nRet = SFDB_NWKDAY(weekdy,order,mnth,year, ref endDate);
      if (nRet >= (int)NDK_RETCODE.NDK_SUCCESS)
      {
        retVal = System.DateTime.FromOADate((double)(endDate));
      }

      return (NDK_RETCODE)nRet;
    }


    /// <summary>
    /// Returns the order of the weekday in the month for a given date. This is the inverse operator of SFDB_NWKDAY().
    /// </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#202", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    private static extern int SFDB_WKDYOrder(long argDate, ref short retVal);


    /// <summary>
    /// Wrap the SFDB_WKDYOrder API function with a CLS compliant function
    /// Returns the order of the weekday in the month for a given date. This is the inverse operator of NWKDAY().
    /// </summary>
    /// <param name="argDate">the given date</param>
    /// <param name="retVal">s the order of the weekday in the month for a given date</param>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    public static NDK_RETCODE NWKDAY(System.DateTime argDate, ref short retVal)
    {
      int dt = (int)(argDate.ToOADate());

      int nRet = SFDB_WKDYOrder(dt, ref retVal);

      return (NDK_RETCODE)nRet;
    }


    /// <summary>
    /// Returns the day of the week corresponding to a date. The day is given as an integer, ranging from 1 (Sunday) to 7 (Saturday), by default.
    /// </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#203", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    private static extern int SFDB_WEEKDAY(long argDate, UInt16 argReturnType, ref UInt16  retVal);


    /// <summary>
    /// Wrap the SFDB_WEEKDAY API function with a CLS compliant function
    /// Returns the day of the week corresponding to a date. The day is given as an integer, ranging from 1 (Sunday) to 7 (Saturday), by default.
    /// </summary>
    /// <param name="argDate">the given date</param>
    /// <param name="argReturnType">is a number that designates the weekdays coding (or ordering) scheme. If missing, convention=1 (Sun=1,..,Sat=7) is assumed.</param>
    /// <param name="retVal">s the order of the weekday in the month for a given date</param>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    public static NDK_RETCODE WEEKDAY(System.DateTime argDate, UInt16 argReturnType, ref UInt16 retVal)
    {
      int dt = (int)(argDate.ToOADate());

      int nRet = SFDB_WEEKDAY(dt,argReturnType, ref retVal);

      return (NDK_RETCODE)nRet;
    }

    /// <summary>
    /// Examines whether the given date falls on a weekend or a holiday (i.e. non-working day), and returns the nearest working business day using a Business Day Convention (BDC).
    /// </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#204", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    private static extern int SFDB_DTADJUST(long argDate, UInt16 argNextPrev,
                              string holidays,
                              long[]  zDates, 
                              UIntPtr nSize,
                              UInt16   wkndNo,
                              ref long retVal);

    /// <summary>
    /// Wrap the SFDB_DTADJUST API function with a CLS compliant function
    /// Examines whether the given date falls on a weekend or a holiday (i.e. non-working day), and returns the nearest working business day using a Business Day Convention (BDC).
    /// </summary>
    /// <param name="argDate">the given date</param>
    /// <param name="argNextPrev">is the Business Day Convention (BDC): 1=Following, 2=Following Modified, 3=Preceding, 4=Preceding Modified, 5=Unadjusted (default</param>
    /// <param name="holidays">is a (:_:) separated list of holiday names, calendars, countries or currency</param>
    /// <param name="zDates">is an array of holidays dates</param>
    /// <param name="wkndNo">is the weekend number (1-7,11-17). If missing, the western weekend (i.e. 1, "Saturday, Sunday") is used</param>
    /// <param name="retVal">is the date of the nearest business day</param>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    public static NDK_RETCODE DTADJUST(System.DateTime argDate, UInt16 argNextPrev, string holidays, System.DateTime[] zDates, UInt16 wkndNo, ref System.DateTime retVal)
    {
      long dt = (int)(argDate.ToOADate());
      long endDate = 0;
      long[] holdys=null;

      UIntPtr nCount = (UIntPtr)0;
      if( zDates.Length > 0){
        holdys= new long[zDates.Length];

        nCount = (UIntPtr)zDates.Length;
        for (int i = 0; i < zDates.Length; i++)
        {
          holdys[i] = (int)(zDates[i].ToOADate());
        }
      }

      int nRet = SFDB_DTADJUST(dt, argNextPrev, holidays, holdys, nCount, wkndNo, ref endDate);
      if (nRet >= (int)NDK_RETCODE.NDK_SUCCESS)
      {
        retVal = System.DateTime.FromOADate((double)(endDate));
      }
      return (NDK_RETCODE)nRet;
    }



    /// <summary>
    /// Examines a given date for weekends and holidays (non-working days), and returns FALSE if it falls on a non-working day; otherwise it returns TRUE
    /// </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#205", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int SFDB_ISWRKDY(long argDate,
                             string holidays,               // comma separated list of holiday names, calendars, countries or currency
                             long[] zDates,
                             UIntPtr nSize,                 // Array of dates
                             UInt16 wkndNo);               // Weekend code (can be a number, code, calendar name, country, or currency)



    /// <summary>
    /// Returns the number of whole working days between two dates (inclusive). Working days exclude weekends and any date identified as a holiday.
    /// </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#206", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int SFDB_NETWRKDYS(long argStartDate,
                                            long argEndDate,
                                            string holidays,
                                            long[] zDates,  
                                            UIntPtr nSize,     // Array of dates
                                            UInt16   wkndNo,                // Weekend code (can be a number, code, calendar name, country, or currency)
                                            ref int retVal);

    /// <summary>
    /// Returns the serial date number that represents the date that falls after the start date by a given number of working days.
    /// </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#207", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int SFDB_WORKDAY( long argDate,
                              short days,
                              string holidays,
                              long[] zDates, 
                              UIntPtr nSize,            // Array of dates
                              UInt16   wkndNo,          // Weekend code (can be a number, code, calendar name, country, or currency)
                              ref long retVal);

    // Holidays API
    /// <summary>
    /// Examine the given code as a valid holiday code.
    /// </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#226", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int SFDB_ISVALIDHLDYCODE(string argCode);
    
    
    /// <summary>
    /// Returns an array of the matching supported holiday codes.
    /// </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#220", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int SFDB_HLDYS(string argHolidays, StringBuilder retVal, ref UIntPtr nLen);


    /// <summary>
    /// Returns the holiday code that falls on the given date.
    /// </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#221", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int SFDB_FindHLDY(long argDate, string argHolidays, StringBuilder retVal, ref UIntPtr nLen);

    /// <summary>
    /// Returns the full name of the holiday that corresponds to the given short code.
    /// </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#222", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int SFDB_HLDYName(string code, StringBuilder retVal, ref UIntPtr nLen);


    /// <summary>
    /// Returns the date serial number that represents the holiday in the given year.
    /// </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#223", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int SFDB_HLDYDate(long argDate, long[] zDates, UIntPtr nSize, string szHolidays, UInt16 retType, ref long retVal);


    /// <summary>
    /// Returns TRUE when the start date falls on a holiday.
    /// </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#224", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int SFDB_ISHLDY(int argDate, long [] zDates, UIntPtr nSize, string szHolidays);

    /// <summary>
    /// Returns an array of serial date numbers that represent observed holidays between the two given dates.
    /// </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#225", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int SFDB_HLDYDates(long argStartDate, long argEndDate, long[] zDates, UIntPtr nlen, string szHolidays, int[] retVal, ref UIntPtr nSize);

    // Weekend API
    /// <summary>
    /// examines whether the given code corresponds to valid weekend convention.
    /// </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#245", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int SFDB_ISVALIDWKND(string argCode);


    /// <summary>
    /// Returns the seven-character string code for a weekend-number (1-7, 11-17).
    /// </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#240", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int SFDB_WKNDCode(UInt16 argNumber, StringBuilder retVal, ref UIntPtr nLen);

    /// <summary>
    /// Returns the weekend-number (1-7 or 11-17) for a given country or a weekend code.
    /// </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#241", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int SFDB_WKNDNo(string argCode, ref UInt16 nNo);

    /// <summary>
    /// Returns TRUE if the reference date falls on a (short/long) weekend.
    /// </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#242", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int SFDB_ISWKND(long argDate, string holidays, long[] zDates, UIntPtr nSize, UInt16 nWkndNo, UInt16 argOptions);

    /// <summary>
    /// Returns the duration (in calendar days) of the (long) weekend that a given day falls on.
    /// </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#243", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int SFDB_WKNDur(long argDate, string holidays, long [] zDates, UIntPtr nSize, UInt16 nWkndNo, ref UInt16 retVal);

    /// <summary>
    /// Returns the serial date number that corresponds to the first (last) day in the next (last) weekend.
    /// </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#244", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int SFDB_WKNDate(long argDate, string holidays, long[] zDates, UIntPtr nSize, UInt16 wkndNo, UInt16 wkdOption, UInt16 direction, ref long retVal);

    // Calendar API
    /// <summary>
    /// Examine the given code as a valid/supported calendar code.
    /// </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#264", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int SFDB_ISVALIDCALCODE(string argCode);

    /// <summary>
    /// Returns the calendar name and description, given the calendar's short code (e.g., "US" will return "US Government Holidays").
    /// </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#260", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int SFDB_CALNAME(string argCode, StringBuilder retVal, ref UIntPtr nLen);

    /// <summary>
    /// Returns an array of names and codes for the supported calendars.
    /// </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#261", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int SFDB_CALENDARS(string argName, StringBuilder retVal, ref UIntPtr nLen, ref UInt16 nNumber);

    /// <summary>
    /// Returns an array of the holidays' names and codes as defined for the given calendar.
    /// </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#262", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int SFDB_CALHLDYS(string argCalCode, StringBuilder retVal, ref UIntPtr nLen, ref UInt16 nNumber);

    /// <summary>
    /// Returns the weekend number associated with the given calendar.
    /// </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#262", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int SFDB_CALWKND( string argCalCode, ref UInt16 nWKNDNo);

    // Country API

    /// <summary>
    /// Return TRUE if the country code is a valid ISO country code (2 or 3 characters).
    /// </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#300", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int SFDB_ISVALIDCNTRYCODE(string argCode);


    /// <summary>
    /// Return the weekend code (i.e. 7 characters)  associated with the given country.
    /// </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#301", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int SFDB_GETWKNDFROMCNTRY(string argCode, StringBuilder szBuffer, out UIntPtr nLen);

    /// <summary>
    /// Return the calendar code associated with the given country.
    /// </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#302", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int SFDB_GETCALFROMCNTRY(string argCode, StringBuilder szBuffer, out UIntPtr nLen);


    // Currency
    /// <summary>
    /// examine the validity of the currency iso code.
    /// </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#320", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int SFDB_ISVALIDCCYCODE(string argCode);


    /// <summary>
    /// Return the weekend code (i.e. 7 characters)  associated with the given currency.
    /// </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#322", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int SFDB_GETWKNDFROMCCY(string argCode, StringBuilder szBuffer, out UIntPtr nLen);

    /// <summary>
    /// Return the calendar code associated with the given currency.
    /// </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#323", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int SFDB_GETCALFROMCCY(string argCode, StringBuilder szBuffer, out UIntPtr nLen);


    // Currency Pair
    /// <summary>
    /// return a currency pair ordered in FX mkt convention.
    /// </summary>
    /// <returns> an integer value for the status of the call. For a full list, see <see cref="NDK_RETCODE"/>.</returns>
    [DllImport(DLLName, EntryPoint = "#340", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public static extern int SFDB_GETVALIDCCYPAIR(string argCCY1, string argCCY2, StringBuilder szPair, out UIntPtr nLen);



  }// End of class SFDBM
}
