﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

using NumXLAPI;


namespace JohansenTest
{
  class Program
  {

    static private double[,] US_MINING_EMPLOYMENT= {
      {283.0,99.0,565.0,120.0,312.0,481.0,92.0,697.0},
      {282.0,99.0,558.0,121.0,309.0,472.0,92.0,693.0},
      {277.0,101.0,560.0,117.0,306.0,475.0,96.0,696.0},
      {280.0,106.0,563.0,114.0,310.0,487.0,98.0,689.0},
      {280.0,108.0,549.0,110.0,314.0,476.0,97.0,667.0},
      {273.0,108.0,545.0,103.0,315.0,471.0,97.0,653.0},
      {274.0,108.0,518.0,100.0,309.0,458.0,93.0,640.0},
      {269.0,105.0,509.0,97.0,296.0,447.0,91.0,623.0},
      {263.0,104.0,505.0,95.0,303.0,455.0,89.0,601.0},
      {260.0,103.0,506.0,92.0,295.0,449.0,88.0,581.0},
      {256.0,101.0,491.0,87.0,286.0,421.0,86.0,551.0},
      {251.0,100.0,472.0,93.0,282.0,390.0,85.0,525.0},
      {236.0,93.0,439.0,86.0,266.0,389.0,77.0,499.0},
      {233.0,92.0,428.0,81.0,254.0,358.0,76.0,483.0},
      {238.0,95.0,420.0,82.0,250.0,379.0,77.0,490.0},
      {243.0,96.0,413.0,84.0,251.0,384.0,80.0,492.0},
      {244.0,97.0,418.0,91.0,255.0,387.0,80.0,486.0},
      {249.0,98.0,415.0,94.0,269.0,399.0,80.0,487.0},
      {253.0,100.0,420.0,92.0,272.0,400.0,77.0,474.0},
      {250.0,100.0,422.0,96.0,272.0,397.0,79.0,480.0},
      {251.0,101.0,426.0,95.0,273.0,403.0,81.0,487.0},
      {251.0,99.0,424.0,94.0,275.0,400.0,81.0,485.0},
      {253.0,99.0,426.0,95.0,276.0,398.0,80.0,486.0},
      {249.0,96.0,427.0,89.0,273.0,389.0,80.0,482.0},
      {246.0,93.0,423.0,88.0,270.0,379.0,75.0,497.0},
      {245.0,93.0,425.0,87.0,267.0,373.0,75.0,494.0},
      {252.0,95.0,431.0,89.0,267.0,379.0,77.0,499.0},
      {258.0,100.0,439.0,100.0,270.0,390.0,81.0,500.0},
      {267.0,103.0,450.0,104.0,278.0,395.0,81.0,503.0},
      {273.0,106.0,466.0,107.0,284.0,402.0,83.0,515.0},
      {269.0,108.0,476.0,106.0,291.0,405.0,83.0,518.0},
      {275.0,109.0,483.0,106.0,289.0,404.0,82.0,511.0},
      {277.0,110.0,489.0,104.0,287.0,405.0,81.0,509.0},
      {274.0,106.0,479.0,105.0,284.0,381.0,82.0,463.0},
      {274.0,105.0,451.0,103.0,279.0,366.0,82.0,428.0},
      {271.0,101.0,454.0,98.0,273.0,347.0,80.0,422.0},
      {263.0,97.0,425.0,93.0,265.0,359.0,73.0,437.0},
      {263.0,94.0,417.0,92.0,258.0,351.0,72.0,437.0},
      {265.0,97.0,429.0,92.0,260.0,364.0,77.0,445.0},
      {277.0,103.0,442.0,97.0,262.0,373.0,78.0,451.0},
      {283.0,105.0,445.0,103.0,264.0,373.0,78.0,453.0},
      {287.0,106.0,447.0,105.0,265.0,378.0,79.0,458.0},
      {284.0,107.0,441.0,104.0,265.0,374.0,78.0,453.0},
      {282.0,105.0,445.0,106.0,259.0,362.0,78.0,452.0},
      {284.0,105.0,445.0,95.0,260.0,360.0,79.0,453.0},
      {280.0,100.0,443.0,96.0,258.0,363.0,78.0,450.0},
      {276.0,98.0,438.0,103.0,258.0,357.0,80.0,444.0},
      {271.0,93.0,429.0,101.0,254.0,344.0,79.0,446.0},
      {260.0,89.0,412.0,96.0,247.0,336.0,75.0,429.0},
      {250.0,88.0,405.0,98.0,238.0,335.0,73.0,417.0},
      {246.0,89.0,407.0,96.0,231.0,338.0,75.0,418.0},
      {250.0,89.0,407.0,101.0,226.0,342.0,69.0,411.0},
      {251.0,91.0,407.0,103.0,225.0,341.0,70.0,407.0},
      {252.0,91.0,403.0,101.0,225.0,342.0,70.0,407.0},
      {254.0,92.0,406.0,98.0,224.0,339.0,71.0,406.0},
      {252.0,90.0,401.0,92.0,219.0,334.0,70.0,401.0},
      {251.0,90.0,400.0,91.0,218.0,336.0,70.0,399.0},
      {251.0,89.0,392.0,92.0,218.0,332.0,72.0,391.0},
      {247.0,87.0,388.0,92.0,219.0,326.0,72.0,387.0},
      {240.0,85.0,386.0,92.0,218.0,319.0,71.0,383.0},
      {232.0,80.0,382.0,87.0,214.0,312.0,66.0,376.0},
      {231.0,80.0,381.0,91.0,211.0,305.0,66.0,363.0},
      {232.0,83.0,384.0,91.0,207.0,305.0,68.0,363.0},
      {232.0,85.0,385.0,97.0,206.0,307.0,67.0,357.0},
      {241.0,87.0,387.0,103.0,206.0,309.0,69.0,356.0},
      {239.0,89.0,390.0,104.0,209.0,310.0,69.0,359.0},
      {239.0,89.0,389.0,106.0,208.0,305.0,67.0,362.0},
      {239.0,89.0,390.0,105.0,208.0,308.0,67.0,359.0},
      {239.0,89.0,393.0,105.0,208.0,307.0,68.0,365.0},
      {234.0,89.0,394.0,103.0,210.0,307.0,69.0,374.0},
      {229.0,88.0,392.0,102.0,210.0,305.0,69.0,372.0},
      {223.0,87.0,390.0,99.0,208.0,299.0,69.0,363.0},
      {210.0,80.0,378.0,96.0,194.0,291.0,65.0,344.0},
      {209.0,79.0,374.0,97.0,189.0,286.0,65.0,340.0},
      {208.0,80.0,372.0,98.0,188.0,289.0,66.0,342.0},
      {211.0,81.0,362.0,106.0,188.0,290.0,68.0,345.0},
      {214.0,84.0,361.0,108.0,187.0,291.0,68.0,345.0},
      {217.0,85.0,363.0,111.0,191.0,292.0,68.0,348.0},
      {218.0,84.0,357.0,112.0,191.0,289.0,68.0,348.0},
      {218.0,83.0,355.0,111.0,192.0,285.0,67.0,348.0},
      {216.0,83.0,353.0,109.0,191.0,289.0,67.0,350.0},
      {217.0,83.0,349.0,106.0,188.0,283.0,66.0,348.0},
      {215.0,82.0,346.0,105.0,185.0,284.0,64.0,350.0},
      {212.0,79.0,346.0,101.0,183.0,280.0,63.0,349.0},
      {194.0,75.0,340.0,98.0,177.0,272.0,61.0,353.0},
      {192.0,74.0,332.0,99.0,176.0,269.0,61.0,347.0},
      {194.0,76.0,339.0,101.0,177.0,272.0,62.0,350.0},
      {199.0,77.0,337.0,101.0,178.0,276.0,64.0,346.0},
      {202.0,79.0,341.0,103.0,178.0,279.0,64.0,349.0},
      {206.0,79.0,348.0,107.0,182.0,284.0,65.0,324.0},
      {205.0,76.0,350.0,108.0,166.0,222.0,64.0,180.0},
      {205.0,81.0,354.0,107.0,186.0,281.0,64.0,346.0},
      {204.0,81.0,354.0,106.0,187.0,285.0,64.0,350.0},
      {201.0,83.0,351.0,106.0,187.0,281.0,64.0,349.0},
      {201.0,82.0,350.0,104.0,180.0,281.0,64.0,352.0},
      {200.0,81.0,351.0,101.0,177.0,276.0,63.0,350.0},
      {192.0,76.0,348.0,97.0,171.0,267.0,61.0,352.0},
      {190.0,76.0,350.0,96.0,170.0,266.0,57.0,349.0},
      {192.0,78.0,356.0,95.0,172.0,271.0,62.0,350.0},
      {192.0,80.0,357.0,97.0,174.0,273.0,62.0,351.0},
      {194.0,82.0,361.0,104.0,177.0,277.0,63.0,355.0},
      {199.0,83.0,361.0,106.0,179.0,279.0,63.0,361.0},
      {200.0,84.0,354.0,105.0,181.0,279.0,63.0,359.0},
      {200.0,84.0,357.0,84.0,181.0,281.0,63.0,358.0},
      {199.0,83.0,360.0,83.0,179.0,280.0,62.0,358.0},
      {199.0,83.0,353.0,82.0,177.0,277.0,62.0,357.0},
      {197.0,82.0,350.0,81.0,176.0,268.0,61.0,361.0},
      {196.0,82.0,353.0,96.0,171.0,264.0,59.0,360.0},
      {181.0,78.0,343.0,91.0,165.0,254.0,56.0,350.0},
      {179.0,76.0,338.0,89.0,162.0,251.0,56.0,345.0},
      {182.0,77.0,341.0,90.0,165.0,249.0,56.0,344.0},
      {186.0,77.0,333.0,92.0,164.0,254.0,57.0,339.0},
      {188.0,79.0,330.0,95.0,163.0,255.0,57.0,338.0},
      {191.0,80.0,329.0,97.0,164.0,256.0,56.0,339.0},
      {190.0,79.0,320.0,94.0,165.0,255.0,55.0,333.0},
      {189.0,77.0,318.0,94.0,163.0,255.0,55.0,333.0},
      {186.0,77.0,317.0,92.0,161.0,253.0,55.0,331.0},
      {183.0,76.0,313.0,87.0,156.0,251.0,55.0,331.0},
      {183.0,74.0,309.0,89.0,152.0,244.0,53.0,331.0},
      {178.0,73.0,307.0,86.0,149.0,236.0,51.0,329.0},
      {171.0,69.0,299.0,80.0,143.0,230.0,47.0,325.0},
      {169.0,69.0,300.0,82.0,141.0,229.0,48.0,318.0},
      {171.0,70.0,302.0,83.0,142.0,232.0,49.0,314.0},
      {173.0,72.0,302.0,85.0,146.0,235.0,51.0,315.0},
      {176.0,73.0,305.0,90.0,147.0,238.0,51.0,315.0},
      {178.0,74.0,305.0,92.0,149.0,241.0,51.0,312.0},
      {177.0,73.0,297.0,93.0,150.0,241.0,50.0,313.0},
      {176.0,73.0,300.0,94.0,150.0,239.0,50.0,312.0},
      {174.0,72.0,301.0,93.0,148.0,239.0,51.0,312.0},
      {173.0,71.0,298.0,94.0,148.0,236.0,49.0,312.0},
      {173.0,71.0,294.0,91.0,145.0,235.0,49.0,311.0},
      {170.0,69.0,294.0,88.0,144.0,225.0,49.0,310.0},
      {165.0,65.0,289.0,84.0,138.0,214.0,47.0,304.0},
      {162.0,65.0,278.0,82.0,136.0,213.0,47.0,271.0},
      {162.0,66.0,281.0,83.0,136.0,210.0,48.0,291.0},
      {163.0,68.0,278.0,88.0,139.0,214.0,48.0,289.0},
      {164.0,69.0,277.0,90.0,141.0,219.0,48.0,284.0},
      {161.0,71.0,281.0,93.0,142.0,210.0,48.0,252.0},
      {150.0,63.0,274.0,94.0,146.0,210.0,47.0,244.0},
      {145.0,62.0,272.0,74.0,143.0,210.0,45.0,232.0},
      {147.0,61.0,275.0,93.0,141.0,211.0,45.0,235.0},
      {148.0,61.0,273.0,92.0,142.0,210.0,47.0,231.0},
      {148.0,61.0,274.0,91.0,142.0,208.0,46.0,230.0},
      {148.0,68.0,275.0,88.0,143.0,211.0,46.0,277.0},
      {153.0,65.0,273.0,81.0,138.0,200.0,43.0,274.0},
      {151.0,64.0,275.0,81.0,138.0,197.0,44.0,272.0},
      {147.0,65.0,280.0,82.0,142.0,198.0,46.0,274.0},
      {150.0,68.0,278.0,85.0,145.0,206.0,46.0,275.0},
      {152.0,69.0,279.0,90.0,147.0,209.0,46.0,277.0},
      {155.0,70.0,279.0,91.0,148.0,212.0,48.0,280.0},
      {155.0,70.0,277.0,92.0,149.0,213.0,47.0,283.0},
      {153.0,69.0,279.0,91.0,149.0,213.0,47.0,284.0},
      {152.0,69.0,279.0,89.0,150.0,212.0,47.0,284.0},
      {147.0,69.0,275.0,88.0,149.0,209.0,46.0,281.0},
      {146.0,69.0,275.0,88.0,146.0,207.0,46.0,282.0},
      {144.0,68.0,276.0,85.0,145.0,202.0,46.0,280.0},
      {136.0,64.0,269.0,80.0,137.0,191.0,44.0,277.0},
      {133.0,64.0,263.0,79.0,134.0,188.0,44.0,271.0},
      {137.0,65.0,262.0,82.0,137.0,191.0,46.0,275.0},
      {139.0,65.0,255.0,84.0,139.0,197.0,46.0,274.0},
      {140.0,65.0,253.0,87.0,138.0,200.0,46.0,272.0},
      {141.0,67.0,251.0,88.0,140.0,200.0,46.0,271.0},
      {141.0,67.0,249.0,88.0,145.0,198.0,46.0,270.0},
      {136.0,67.0,250.0,89.0,144.0,198.0,46.0,273.0},
      {136.0,67.0,245.0,85.0,142.0,196.0,46.0,273.0},
      {133.0,67.0,245.0,78.0,142.0,192.0,46.0,269.0},
      {132.0,66.0,246.0,77.0,142.0,193.0,46.0,268.0},
      {131.0,65.0,246.0,74.0,141.0,190.0,45.0,259.0},
      {125.0,61.0,236.0,71.0,136.0,178.0,44.0,255.0},
      {125.0,61.0,237.0,71.0,136.0,180.0,44.0,256.0},
      {129.0,64.0,239.0,73.0,138.0,184.0,45.0,255.0},
      {131.0,64.0,242.0,77.0,140.0,188.0,46.0,255.0},
      {132.0,65.0,244.0,80.0,141.0,191.0,46.0,256.0}};



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
        // Ready to call Johansen test
        double alpha = 0.05;
        double retStat = double.NaN;
        double retCV = double.NaN;

        UIntPtr nCount = (UIntPtr)144;
        UIntPtr nVars = (UIntPtr)8;

        UIntPtr wMaxOrder = (UIntPtr)9;
        short nPolyOrder=0;
        UInt16 nNoRelations = 0;
        bool traceTest=false;


        // Passing double pointers
        int iRow = 144;
        int iCol = 8;
        IntPtr[] p2dArray = new IntPtr[iRow];
        for (int i = 0; i < iRow; i++)
        {
          double[] pdArray = new double[iCol];

          // Fill row (array)
          for (int j = 0; j < iCol; j++)
          {
            pdArray[j] = US_MINING_EMPLOYMENT[i,j];
          }

          p2dArray[i] = Marshal.AllocCoTaskMem(Marshal.SizeOf(pdArray[0])* iCol);

          
          Marshal.Copy(pdArray, 0, p2dArray[i], iCol);
        }

        IntPtr vars=(IntPtr)0;
        IntPtr buffer = Marshal.AllocCoTaskMem(Marshal.SizeOf(vars) * iRow);
        Marshal.Copy(p2dArray, 0, buffer, iRow);

        nRet = (NDK_RETCODE)SFSDK.NDK_JOHANSENTEST(buffer, 
                                       nCount, nVars, wMaxOrder, 
                                       nPolyOrder, traceTest, nNoRelations, alpha, ref retStat, ref retCV);

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
