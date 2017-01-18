Module ADFTest

  Sub Main()

    Dim retVal As Double = Double.NaN
    Dim argMethod As Short = 1
    Dim retType As Short = 1
    Dim nCount As UIntPtr
    Dim target As Double = 500.0
    Dim i As Integer = 0
    Dim nRet As NumXLAPI.NDK_RETCODE
    Dim alpha As Double
    Dim maxOrder As UShort
    Dim szApp As String

    szApp = "TestApp"
    nCount = 144

    ' Test dataset 
    Dim data() As Double = {112, 118, 132, 129, 121, 135,
                            148, 148, 136, 119, 104, 118,
                            115, 126, 141, 135,
                            125, 149, 170, 170,
                            158, 133, 114, 140,
                            145, 150, 178, 163,
                            172, 178, 199, 199,
                            184, 162, 146, 166,
                            171, 180, 193, 181,
                            183, 218, 230, 242,
                            209, 191, 172, 194,
                            196, 196, 236, 235,
                            229, 243, 264, 272,
                            237, 211, 180, 201,
                            204, 188, 235, 227,
                            234, 264, 302, 293,
                            259, 229, 203, 229,
                            242, 233, 267, 269,
                            270, 315, 364, 347,
                            312, 274, 237, 278,
                            284, 277, 317, 313,
                            318, 374, 413, 405,
                            355, 306, 271, 306,
                            315, 301, 356, 348,
                            355, 422, 465, 467,
                            404, 347, 305, 336,
                            340, 318, 362, 348,
                            363, 435, 491, 505,
                            404, 359, 310, 337,
                            360, 342, 406, 396,
                            420, 472, 548, 559,
                            463, 407, 362, 405,
                            417, 391, 419, 461,
                            472, 535, 622, 606,
                            508, 461, 390, 432,
                            444, 416, 472, 499,
                            497, 579, 667, 657,
                            557, 492, 425, 481}


    nRet = NumXLAPI.SFSDK.Init(szApp, Nothing, Nothing, Nothing)

    alpha = 0.05
    ' nRet = NumXLAPI.SFSDK.NDK_STDEVTEST(data, nCount, target, alpha, argMethod, retType, retVal)

    maxOrder = 6  ' cubic root of the time series length
    nRet = NumXLAPI.SFSDK.NDK_ADFTEST(data, nCount, maxOrder, 1, 1, alpha, 1, retType, retVal)

    'nRet = NumXLAPI.SFSDK.NDK_SKEWTEST(data, nCount, target, argMethod, retType, retVal) 

    If (nRet < NumXLAPI.NDK_RETCODE.NDK_SUCCESS) Then
      ' MiDAS7.addtoEventLog("STDEV Failed :: " + nRet.ToString)
    Else
      ' MiDAS7.addtoEventLog("Dataset Std Deviation is " + retVal.ToString)
    End If


    NumXLAPI.SFSDK.Shutdown()

  End Sub

End Module
