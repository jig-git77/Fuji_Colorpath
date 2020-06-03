Partial Public Class _Default
    Inherits System.Web.UI.Page
    
#Region "Variables"

    Dim intLen As Integer = 0
    Dim iCounter As Integer = 0
    Dim strFileContent As String = String.Empty

    Dim objEncoding As New System.Text.ASCIIEncoding

    Dim objMeasurementFile As New MeasurementFile
     
    Dim objColorMeasurement As ColorMeasurement 
    Dim objPlateMeasurement As PlateMeasurement 

    Dim idxStart As Integer    'Index of the row where Patch Data Starts
    Dim idxEnd As Integer      'Index of the row where Patch Data Ends  

    Public Enum Type
        Color = 0
        Plate = 1
        DigitalPress = 2
    End Enum

    Dim MeasurementType As Type



#End Region

    'Reference: http://msdn2.microsoft.com/en-us/library/system.web.httprequest.inputstream.aspx
    'Author: Chirag Shah
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then

            If UCase(Request.ServerVariables("Request_Method")) <> "POST" Then
                Response.Write("No Data Posted")
                HttpContext.Current.ApplicationInstance.CompleteRequest()
                Exit Sub
            End If
            Try

                    '' Number of binary data
                    intLen = Request.TotalBytes
                    If intLen = 0 Then
                        Response.Write("No Data Posted")
                        HttpContext.Current.ApplicationInstance.CompleteRequest()
                        Exit Sub
                    ElseIf (intLen <= 175) Then
                        Response.Write("OK")
                        HttpContext.Current.ApplicationInstance.CompleteRequest()
                        Exit Sub
                    Else

                        Dim strbyte() As Byte = Request.BinaryRead(intLen)

                        'strFileContent = My.Computer.FileSystem.ReadAllText("C:\Users\shah_c\Desktop\c93105a2-ec55-4007-819f-26965a937c21.txt")



                        'Convert a byte array to a string:
                        strFileContent = objEncoding.GetString(strbyte)

                        If (strFileContent.IndexOf("BEGIN_DATA", StringComparison.Ordinal) = -1) Then
                            Response.Write("OK")
                            HttpContext.Current.ApplicationInstance.CompleteRequest()
                            Exit Sub
                        End If

                        If (strFileContent.IndexOf("END_DATA", StringComparison.Ordinal) = -1) Then
                            Response.Write("OK")
                            HttpContext.Current.ApplicationInstance.CompleteRequest()
                            Exit Sub
                        End If

                        'strFileContent = My.Computer.FileSystem.ReadAllText("c:\Targets.txt")
                        'strFileContent = My.Computer.FileSystem.ReadAllText("C:\Users\shah_c\Desktop\06232010104733691.txt")

                        'Save the posted measurement file
                        If Not IsNothing(strFileContent) AndAlso strFileContent <> String.Empty Then
                            SaveMeasurementFile(strFileContent)
                        End If

                        'Parse the measurement File
                        ParseMeasurementData(strFileContent)

                        Response.Write("OK")
                  End If

            Catch ex As Exception
                Response.Write("OK")
                'Response.Write(ex.ToString)
                If ConfigurationManager.AppSettings("ErrorLogType") = "E" Then
                    'Send the exception through the e-mail
                    HandleException.SendExceptionByEmail(ex, strFileContent)
                End If

                ' ''find guid from text file 
                ' strFileContent = strFileContent.Replace(Chr(34), String.Empty)
                'Dim i As Integer = strFileContent.IndexOf("GUID" & vbTab)
                'Dim MeasurementGuid As String = strFileContent.Substring(i + 5, 36)

                ''If execption is thrown we delete the measurement
                ''Type of Measurement Color Or Plate
                'If MeasurementType = Type.Plate Then

                '    objPlateMeasurement = New PlateMeasurement()
                '    objPlateMeasurement.Measurement_GUID = New Guid(MeasurementGuid)
                '    objPlateMeasurement.Delete()

                'Else

                '    objColorMeasurement = New ColorMeasurement()
                '    objColorMeasurement.Measurement_GUID = New Guid(MeasurementGuid)
                '    objColorMeasurement.Delete()

                'End If



            End Try

        End If

    End Sub

    
    ''' <summary>
    ''' Save Measurement File
    ''' </summary>
    ''' <param name="strFileContent"></param>
    ''' <remarks></remarks>
    Private Sub SaveMeasurementFile(ByVal strFileContent As String)

        'If GUID has double quotes surrounding it
        strFileContent = strFileContent.Replace(Chr(34),String.Empty)

        dim i As Integer = strFileContent.IndexOf("GUID" & vbTab)
        Dim FileGuid As String = strFileContent.Substring(i+5,36)    

        Try
            With objMeasurementFile
                .MeasurementFile = strFileContent
                .MeasurementGUID = New Guid(FileGuid)
            End With
            objMeasurementFile.Save()
        Catch ex As Exception
            Throw New Exception("Exception in SaveMeasurementFile: " & ex.ToString)
        End Try
    End Sub

    ''' <summary>
    ''' Parse the content of the measurement file, 
    ''' </summary>
    ''' <param name="strFileContent"></param>
    ''' <remarks></remarks>
    Private Sub ParseMeasurementData(ByVal strFileContent As String)

        Try

             'Code to cleanpu CGS IT8

            strFileContent = strFileContent.Replace("    " & Chr(9) & Chr(9), Chr(9))                  'Remove four white spaces from Job tab              


             strFileContent = strFileContent.Replace(Chr(9) & Chr(9),Chr(9))
             strFileContent = strFileContent.Replace(Chr(9) & Chr(9),Chr(9))   
             strFileContent = strFileContent.Replace(Chr(9) & Chr(9),Chr(9))
            
             strFileContent = strFileContent.Replace( vbTab & Chr(10), Chr(10))

             strFileContent = strFileContent.Replace( Chr(10) & vbTab, Chr(10))
                

             strFileContent = strFileContent.Replace(Chr(9) & Chr(9) & Chr(9),String.Empty)
             
             strFileContent = strFileContent.Replace(Chr(34), String.Empty)


             'Type of Measurement Color Or Plate
             If strFileContent.Contains("AVERAGE_NEGATIVE_LIMIT") = True Then
                MeasurementType = Type.Plate
                objPlateMeasurement = New PlateMeasurement()
             Else
                MeasurementType = Type.Color
                objColorMeasurement = New ColorMeasurement()
             End If

             'First we separate the content of the file into rows using the carriage return and newline as line separator
             Dim SplitLineAt() As Char = {Chr(13), Chr(10)}    'Returns and newlines 
             Dim Rows() As String = strFileContent.Split(SplitLineAt, StringSplitOptions.RemoveEmptyEntries)

             'Iterate through the rows and find out the start and end position of the patch data
             For iCount As Integer = 0 To Rows.Length - 1
                   If String.Compare(Rows(iCount).Replace(vbTab,String.Empty), "BEGIN_DATA", False) = 0 Then
                       idxStart = iCount + 1
                   End If
                   If String.Compare(Rows(iCount).Replace(vbTab,String.Empty), "END_DATA", False) = 0 Then
                        idxEnd = iCount - 1
                   End If
             Next iCount

             'iterate through the rows to parse the file content, exit when patch data information begins   
             For Each CurrentRow As String In Rows
                
                If String.Compare(CurrentRow.Replace(vbTab,String.Empty), "BEGIN_DATA" , False) = 0 Then
                    Exit For
                Else
                    If MeasurementType = Type.Color Then
                        Call ExtractColorMeasurement(CurrentRow)
                    Else
                        Call ExtractPlateMeasurement(CurrentRow)
                    End If
                   
                End If
             Next

             'based on the start and end position extract the patch information from the file
             For iCount As Integer = idxStart To idxEnd
                ExtractPatchData(Rows(iCount))
             Next iCount

             If Not IsNothing(objColorMeasurement) = True Then
                objColorMeasurement.Save()               
             Else
                objPlateMeasurement.Save()
             End If
            
        Catch ex As Exception
            Throw New Exception("Exception in ParseMeasurementData: " & ex.ToString)
        End Try
    End Sub



    ''' <summary>
    ''' This method parses tab delimited row from the measurement file and extract values from it
    ''' </summary>
    ''' <param name="CurrentRow"></param>
    ''' <remarks></remarks>
    Private Sub ExtractColorMeasurement(ByVal CurrentRow As String)
        Try
            Dim SplitFieldsAt() As Char = {Chr(9)}   'Tab
            Dim Fields() As String = CurrentRow.Split(SplitFieldsAt, StringSplitOptions.RemoveEmptyEntries)

     
            If Fields.Length > 0 Then

                Select Case Fields(0)
                    Case "BEGIN_DATA_TOLERANCES","END_DATA_TOLERANCES","BEGIN_DATA_ACTUAL_VALUES", "END_DATA_ACTUAL_VALUES", "SAMPLE_ID"
                        Exit Sub
                End Select
    
                If Fields.Contains("ORIGINATOR") = True Then
                    If Fields.Length = 2 AndAlso Fields(1) <> String.Empty Then
                        objColorMeasurement.Originator = Fields(1)
                    End If
                    Return
                ElseIf Fields.Contains("CREATED") = True Then
                    Dim dt As DateTime
                    'date received is in European Format
                    dt = DateTime.Parse(Fields(1),System.Globalization.CultureInfo.GetCultureInfo("en-GB"))
                    objColorMeasurement.MeasurementDT = dt
                    Return
                ElseIf Fields.Contains("CREATEDGMT") = True Then
                    Dim dt As DateTime
                    dt = DateTime.Parse(Fields(1),System.Globalization.CultureInfo.GetCultureInfo("en-GB"))
                    If IsDate(dt) = True Then
                         objColorMeasurement.MeasurementGMT = dt        
                    End If
                    Return
                ElseIf Fields.Contains("TARGET_NAME") = True Then
                    objColorMeasurement.TargetName = Fields(1)
                    Return
                ElseIf Fields.Contains("PROOF_SUPPLIER") = True Then
                    If Fields.Length = 2 AndAlso Fields(1) <> String.Empty Then
                        objColorMeasurement.ProofSupplier = Fields(1)        
                    End If
                    Return
                ElseIf Fields.Contains("TARGET_FILE") = True Then
                    If Fields.Length = 2 AndAlso Fields(1) <> String.Empty Then
                        objColorMeasurement.TargetFile = Fields(1)
                    End If
                    Return
                ElseIf Fields.Contains("CP_SERIAL_NUM") = True Then
                    objColorMeasurement.DongleNumber = Fields(1)
                    Return
                ElseIf Fields.Contains("GUID") = True Then
                    objColorMeasurement.Measurement_GUID =  New Guid(Fields(1).Replace("""", String.Empty))
                    Return        
                ElseIf ( (InStr(CurrentRow,"ID" & vbTab,CompareMethod.Binary)> 0 ) OR ( InStr(CurrentRow,"ID  " & vbTab,CompareMethod.Binary) > 0 ) ) AndAlso ( InStr(CurrentRow,"SAMPLE_NAME",CompareMethod.Text) = 0   ) Then
                    objColorMeasurement.CPVClientID =  Convert.ToInt32(Fields(1))
                    Return               
                ElseIf Fields.Contains("DE_FORMULA") = True Then
                    If Fields(1) = "dE" Then
                        objColorMeasurement.dEType = ColorMeasurement.DeltaEType.LAB
                    ElseIf Fields(1) = "dE94" Then
                        objColorMeasurement.dEType = ColorMeasurement.DeltaEType.dE94
                    ElseIf Fields(1) = "dE2000" Then
                        objColorMeasurement.dEType = ColorMeasurement.DeltaEType.dE2000
                    ElseIf Fields(1) = "dE CMC 2:1" Then
                        objColorMeasurement.dEType = ColorMeasurement.DeltaEType.CMC
                    End If
                    Return
                ElseIf Instr(CurrentRow,"PAPER_LIMIT",CompareMethod.Text) > 0  Then
                    If Fields.Length > 1 Then
                        Dim charSeparators() As Char = {" "c}
                        Dim Values As String() = Fields(1).Split(charSeparators, StringSplitOptions.RemoveEmptyEntries)
                        objColorMeasurement.Targ_Paper_dE = Convert.ToDecimal(Values(0))
                        If Values.Length > 1 Then
                            objColorMeasurement.TargStrict_Paper_dE = Convert.ToDecimal(Values(1))
                        End If
                    End If
                    Return
                ElseIf (Instr(CurrentRow,"AVERAGE_LIMIT", CompareMethod.Text) > 0) AndAlso ( InStr(CurrentRow,"GRAY",CompareMethod.Text) = 0  )  Then
                    If Fields.Length > 1 Then
                         Dim charSeparators() As Char = {" "c}
                         Dim Values As String()  = Fields(1).Split(charSeparators,StringSplitOptions.RemoveEmptyEntries)
                         objColorMeasurement.Targ_Avg_dE = Convert.ToDecimal(Values(0))
                         If Values.Length > 1 Then
                            objColorMeasurement.TargStrict_Avg_dE = Convert.ToDecimal(Values(1))
                         End If
                    End IF
                    Return
                ElseIf InStr(CurrentRow,"MAXIMUM_LIMIT", CompareMethod.Text) > 0 Then
                    If Fields.Length > 1 Then
                         Dim charSeparators() As Char = {" "c}
                         Dim Values As String()  = Fields(1).Split(charSeparators,StringSplitOptions.RemoveEmptyEntries)
                         objColorMeasurement.Targ_Maximum_dE = Convert.ToDecimal(Values(0))
                         If Values.Length > 1 Then
                            objColorMeasurement.TargStrict_Maximum_dE = Convert.ToDecimal(Values(1))
                         End If
                    End If
                    Return
                ElseIf (InStr(CurrentRow,"PRIMARY_LIMIT", CompareMethod.Text)> 0 ) AndAlso (InStr(CurrentRow, "_DH", CompareMethod.Text) =  0 )Then
                    If Fields.Length > 1 Then
                         Dim charSeparators() As Char = {" "c}
                         Dim Values As String()  = Fields(1).Split(charSeparators,StringSplitOptions.RemoveEmptyEntries)
                         objColorMeasurement.Targ_Primary_dE = Convert.ToDecimal(Values(0))
                         If Values.Length > 1 Then
                            objColorMeasurement.TargStrict_Primary_dE = Convert.ToDecimal(Values(1))
                         End If
                    End If
                    Return
                ElseIf InStr(CurrentRow,"PRIMARY_LIMIT_DH", CompareMethod.Text) > 0  Then
                    If Fields.Length > 1 Then
                         Dim charSeparators() As Char = {" "c}
                         Dim Values As String()  = Fields(1).Split(charSeparators,StringSplitOptions.RemoveEmptyEntries)
                         objColorMeasurement.Targ_Primary_DH = Convert.ToDecimal(Values(0))
                         If Values.Length > 1 Then
                            objColorMeasurement.TargStrict_Primary_DH = Convert.ToDecimal(Values(1))
                         End If
                    End If
                    Return
                ElseIf Instr(CurrentRow,"GRAY_AVERAGE_LIMIT_DH",CompareMethod.Text) > 0 Then
                    If Fields.Length > 1 Then
                         Dim charSeparators() As Char = {" "c}
                         Dim Values As String()  = Fields(1).Split(charSeparators,StringSplitOptions.RemoveEmptyEntries)
                         objColorMeasurement.Targ_Gray_DH = Convert.ToDecimal(Values(0))
                         If Values.Length > 1 Then
                            objColorMeasurement.TargStrict_Gray_DH = Convert.ToDecimal(Values(1))
                         End If
                    End If
                    Return
                ElseIf Fields.Contains("LIMIT_95%_QUANTILE") = True Then
                    objColorMeasurement.Targ_95PCT_Quantile = Convert.ToDecimal(Fields(1))
                    Return
                ElseIf Fields.Contains("AVERAGE_LIMIT_OUTER_AREA") = True Then
                    objColorMeasurement.Targ_Avg_OuterArea = Convert.ToDecimal(Fields(1))
                    Return
                
                ElseIf (InStr(CurrentRow, "PAPER", CompareMethod.Binary) > 0  AndAlso InStr(CurrentRow,"PRINTER_PAPER") = 0 ) AndAlso IsNumeric(Fields(1)) = True Then
                    If Fields.Length = 2  Then
                         objColorMeasurement.Paper_dE = Convert.ToDecimal(Fields(1))
                         Return
                    End If
                   
                ElseIf Fields.Contains("AVERAGE") = True Then
                    objColorMeasurement.AVG_dE = Convert.ToDecimal(Fields(1))
                    Return
                ElseIf Fields.Contains("MAXIMUM") = True Then
                    Dim Values() As String = Fields(1).Trim.Split(CChar(" "))
                    objColorMeasurement.MAX_dE = Convert.ToDecimal( Values(0) )
                    If Values.Length = 2 Then
                        objColorMeasurement.PatchMaxdE = Values(1)
                    End If
                    Return
                ElseIf Fields.Contains("PRIMARY") = True Then
                     Dim Values() As String = Fields(1).Trim.Split(CChar(" "))
                     objColorMeasurement.Primary_dE = Convert.ToDecimal(Values(0))
                     If Values.Length   = 2 Then
                        objColorMeasurement.PatchPrimarydE = Values(1)
                     End If
                     Return
                ElseIf Fields.Contains("PRIMARY_MAX_DH") = True Then
                    Dim Values() As String = Fields(1).Trim.Split(CChar(" "))
                    objColorMeasurement.MAX_Primary_DH = Convert.ToDecimal(Values(0))
                    If Values.Length = 2 Then
                        objColorMeasurement.PatchMaxdE = Values(1)
                    End IF
                    Return
                ElseIf Fields.Contains("GRAY_AVERAGE_DH") = True Then
                    objColorMeasurement.AVG_Gray_DH = Convert.ToDecimal(Fields(1))
                    Return
                ElseIf Fields.Contains("95%_QUANTILE") = True Then
                    objColorMeasurement.NineFive_Quantile = Convert.ToDecimal(Fields(1))
                    Return
                ElseIf Fields.Contains("AVERAGE_OUTER_AREA") = True Then
                    objColorMeasurement.Avg_OuterArea = Convert.ToDecimal(Fields(1))
                    Return
                ElseIf (InStr(CurrentRow, "USER", CompareMethod.Binary) > 0 AndAlso InStr(CurrentRow,"_USER") = 0 AndAlso Instr(CurrentRow.ToLower(),"\user") = 0 ) Then
                    If Fields.Length = 2 AndAlso Fields(1) <> String.Empty Then
                        objColorMeasurement.User = Fields(1)
                    End If
                    Return
                'ElseIf Fields.Contains("DEVICE") = True Then
                '    If Fields.Length > 1 Then
                '        Dim Values() As String = Fields(1).Trim.Split(CChar(" ")) 
                '        objColorMeasurement.Instrument = Values(0)
                '        objColorMeasurement.Instrument_SerialNum = Values(1)       
                '    End If

                '    Return
                ElseIf Fields.Contains("MEASUREMENT") = True Then
                    If Fields.Length = 2 AndAlso Fields(1) <> String.Empty Then
                        If Fields.Contains("Observer") = True Then
                            Dim Values() As String = Fields(1).Trim.Split(CChar(","))
                            objColorMeasurement.ObserverType = Values(0)
                            objColorMeasurement.Illuminant = Values(1)
                            objColorMeasurement.Filter = Values(2)
                            Return
                        End If
                    End If
                ElseIf Fields.Contains("CUSTOMER") = True Then
                    If Fields.Length = 2 AndAlso Fields(1) <> String.Empty Then
                        objColorMeasurement.Customer = Fields(1)
                    End If

                    Return
                ElseIf Fields.Contains("JOB") = True Then
                    If Fields.Length = 2 AndAlso Fields(1) <> String.Empty Then
                        objColorMeasurement.Job = Fields(1)
                    End If
                    Return
                ElseIf Fields.Contains("PROOFER") = True Then
                    If Fields.Length = 2 AndAlso Fields(1) <> String.Empty Then
                        objColorMeasurement.Proofer = Fields(1)
                    End If
                    Return
                ElseIf Fields.Contains("INFO ") = True Then
                    If Fields.Length > 1 Then
                        objColorMeasurement.Info = Fields(1)
                    End If

                    Return
                ElseIf Fields.Contains("PROOF_OK") = True Then
                    If Convert.ToInt16(Fields(1)) = 0 Then
                        objColorMeasurement.ProofSatus = ColorMeasurement.ProofStaus.Failed
                    ElseIf Convert.ToInt16(Fields(1)) = 1 Then
                        objColorMeasurement.ProofSatus = ColorMeasurement.ProofStaus.Caution
                    Else
                        objColorMeasurement.ProofSatus = ColorMeasurement.ProofStaus.Passed
                    End If
                    Return
                ElseIf Fields.Contains("OUTPUT_SUBSTRATE") = True Then
                    If Fields.Length = 2 AndAlso Fields(1) <> String.Empty Then
                        objColorMeasurement.Substrate = Fields(1)
                    End If
                    Return
                ElseIf Fields.Contains("OUTPUT_SUBSTRATE_SKU") = True Then
                    If Fields.Length = 2 AndAlso Fields(1) <> String.Empty Then
                         objColorMeasurement.SubstrateSKU = Fields(1)
                    End If
                    Return
                ElseIf Fields.Contains("OUTPUT_SUBSTRATE_BATCH") = True Then
                    If Fields.Length > 1 Then
                        objColorMeasurement.SubstrateBatch = Fields(1)
                    End If
                    Return
                ElseIf Fields.Contains("OUTPUT_MANUFACTURER") = True Then
                    If Fields.Length = 2 AndAlso Fields(1) <> String.Empty Then
                         objColorMeasurement.PrinterMake = Fields(1)
                    End If
                    Return
                ElseIf Fields.Contains("OUTPUT_MODEL") = True Then
                    If Fields.Length = 2 AndAlso Fields(1) <> String.Empty Then
                        objColorMeasurement.PrinterModel = Fields(1)
                    End If
                    Return
                ElseIf Fields.Contains("OUTPUT_SERIAL_NUMBER") = True Then
                    If Fields.Length = 2 AndAlso Fields(1) <> String.Empty Then
                         objColorMeasurement.PrinterSerialNum = Fields(1)
                    End If
                    Return
                 ElseIf Fields.Contains("PRINTER_TIME") = True Then
                    If Fields.Length = 2 AndAlso IsDate(Fields(1)) = True Then
                         objColorMeasurement.PrinterTime = Convert.ToDateTime(Fields(1))
                    End If
                    Return

                 ElseIf Fields.Contains("PRINTER_PAPER") = True Then
                    If Fields.Length = 2 AndAlso Fields(1) <> String.Empty Then
                         objColorMeasurement.PrinterPaper = Fields(1)
                    End If
                    Return
                 ElseIf Fields.Contains("PRINTER_CC_TABLE") = True Then
                    If Fields.Length = 2 AndAlso Fields(1) <> String.Empty Then
                         objColorMeasurement.PrinterCCTable = Fields(1)
                    End If
                    Return
                 ElseIf Fields.Contains("PRINTER_STATE") = True Then
                    If Fields.Length = 2 AndAlso Fields(1) <> String.Empty Then
                         objColorMeasurement.PrinterState = Fields(1)
                    End If
                    Return
                 ElseIf Fields.Contains("PRINTER_ICC_TARGET") = True Then
                    If Fields.Length = 2 AndAlso Fields(1) <> String.Empty Then
                         objColorMeasurement.PrinterICCTarget = Fields(1)
                    End If
                    Return
                  ElseIf Fields.Contains("PRINTER_USER") = True Then
                    If Fields.Length = 2 AndAlso Fields(1) <> String.Empty Then
                         objColorMeasurement.PrinterUser = Fields(1)
                    End If
                    Return
                 ElseIf Fields.Contains("PRINTER_HOST") = True Then
                    If Fields.Length = 2 AndAlso Fields(1) <> String.Empty Then
                         objColorMeasurement.PrinterHost = Fields(1)
                    End If
                    Return
                 ElseIf InStr(CurrentRow, "PRINTER_INK" & vbTab) > 0 Then

                     If InStr(CurrentRow, "PRINTER_INK" & vbTab & "0", CompareMethod.Text) > 0 Then

                        Dim objInkLevel As New InkLevel()
                        With objInkLevel
                            .InkPos = "PRINTER_INK 0"
                            .InkName = Fields(2)
                            .CurrLevel = Convert.ToDecimal(Fields(3))
                            .MaxLevel = Convert.ToDecimal(Fields(4))
                            .Measurement_GUID = objColorMeasurement.Measurement_GUID
                         End With
                         objColorMeasurement.InkLevels.Add(objInkLevel)
                         Return

                     ElseIf InStr(CurrentRow, "PRINTER_INK" & vbTab & "1", CompareMethod.Text) > 0 Then

                        Dim objInkLevel As New InkLevel()
                        With objInkLevel
                            .InkPos = "PRINTER_INK 1"
                            .InkName = Fields(2)
                            .CurrLevel = Convert.ToDecimal(Fields(3))
                            .MaxLevel = Convert.ToDecimal(Fields(4))
                            .Measurement_GUID = objColorMeasurement.Measurement_GUID
                        End With
                        objColorMeasurement.InkLevels.Add(objInkLevel)
                        Return
                     ElseIf InStr(CurrentRow, "PRINTER_INK" & vbTab & "2", CompareMethod.Text) > 0 Then
                        Dim objInkLevel As New InkLevel()
                        With objInkLevel
                            .InkPos = "PRINTER_INK 2"
                            .InkName = Fields(2)
                            .CurrLevel = Convert.ToDecimal(Fields(3))
                            .MaxLevel = Convert.ToDecimal(Fields(4))
                            .Measurement_GUID = objColorMeasurement.Measurement_GUID
                        End With
                        objColorMeasurement.InkLevels.Add(objInkLevel)
                        Return
                     ElseIf InStr(CurrentRow, "PRINTER_INK" & vbTab & "3", CompareMethod.Text) > 0 Then
                        Dim objInkLevel As New InkLevel()
                        With objInkLevel
                            .InkPos = "PRINTER_INK 3"
                            .InkName = Fields(2)
                            .CurrLevel = Convert.ToDecimal(Fields(3))
                            .MaxLevel = Convert.ToDecimal(Fields(4))
                            .Measurement_GUID = objColorMeasurement.Measurement_GUID
                        End With
                        objColorMeasurement.InkLevels.Add(objInkLevel)
                        Return
                     ElseIf InStr(CurrentRow, "PRINTER_INK" & vbTab & "4", CompareMethod.Text) > 0 Then
                        Dim objInkLevel As New InkLevel()
                        With objInkLevel
                            .InkPos = "PRINTER_INK 4"
                            .InkName = Fields(2)
                            .CurrLevel = Convert.ToDecimal(Fields(3))
                            .MaxLevel = Convert.ToDecimal(Fields(4))
                            .Measurement_GUID = objColorMeasurement.Measurement_GUID
                        End With
                        objColorMeasurement.InkLevels.Add(objInkLevel)
                        Return
                     ElseIf InStr(CurrentRow, "PRINTER_INK" & vbTab & "5", CompareMethod.Text) > 0 Then
                        Dim objInkLevel As New InkLevel()
                        With objInkLevel
                            .InkPos = "PRINTER_INK 5"
                            .InkName = Fields(2)
                            .CurrLevel = Convert.ToDecimal(Fields(3))
                            .MaxLevel = Convert.ToDecimal(Fields(4))
                            .Measurement_GUID = objColorMeasurement.Measurement_GUID
                        End With
                        objColorMeasurement.InkLevels.Add(objInkLevel)
                        Return
                     ElseIf InStr(CurrentRow, "PRINTER_INK" & vbTab & "6", CompareMethod.Text) > 0 Then
                        Dim objInkLevel As New InkLevel()
                        With objInkLevel
                            .InkPos = "PRINTER_INK 6"
                            .InkName = Fields(2)
                            .CurrLevel = Convert.ToDecimal(Fields(3))
                            .MaxLevel = Convert.ToDecimal(Fields(4))
                            .Measurement_GUID = objColorMeasurement.Measurement_GUID
                        End With
                        objColorMeasurement.InkLevels.Add(objInkLevel)
                        Return
                     ElseIf InStr(CurrentRow, "PRINTER_INK" & vbTab & "5", CompareMethod.Text) > 0 Then
                        Dim objInkLevel As New InkLevel()
                        With objInkLevel
                            .InkPos = "PRINTER_INK 5"
                            .InkName = Fields(2)
                            .CurrLevel = Convert.ToDecimal(Fields(3))
                            .MaxLevel = Convert.ToDecimal(Fields(4))
                            .Measurement_GUID = objColorMeasurement.Measurement_GUID
                        End With
                        objColorMeasurement.InkLevels.Add(objInkLevel)
                        Return
                     ElseIf InStr(CurrentRow, "PRINTER_INK" & vbTab & "6", CompareMethod.Text) > 0 Then
                        Dim objInkLevel As New InkLevel()
                        With objInkLevel
                            .InkPos = "PRINTER_INK 6"
                            .InkName = Fields(2)
                            .CurrLevel = Convert.ToDecimal(Fields(3))
                            .MaxLevel = Convert.ToDecimal(Fields(4))
                            .Measurement_GUID = objColorMeasurement.Measurement_GUID
                        End With
                        objColorMeasurement.InkLevels.Add(objInkLevel)
                        Return
                     ElseIf InStr(CurrentRow, "PRINTER_INK" & vbTab & "7", CompareMethod.Text) > 0 Then
                        Dim objInkLevel As New InkLevel()
                        With objInkLevel
                            .InkPos = "PRINTER_INK 7"
                            .InkName = Fields(2)
                            .CurrLevel = Convert.ToDecimal(Fields(3))
                            .MaxLevel = Convert.ToDecimal(Fields(4))
                            .Measurement_GUID = objColorMeasurement.Measurement_GUID
                        End With
                        objColorMeasurement.InkLevels.Add(objInkLevel)
                        Return
                     ElseIf InStr(CurrentRow, "PRINTER_INK" & vbTab & "8", CompareMethod.Text) > 0 Then
                        Dim objInkLevel As New InkLevel()
                        With objInkLevel
                            .InkPos = "PRINTER_INK 8"
                            .InkName = Fields(2)
                            .CurrLevel = Convert.ToDecimal(Fields(3))
                            .MaxLevel = Convert.ToDecimal(Fields(4))
                            .Measurement_GUID = objColorMeasurement.Measurement_GUID
                        End With
                        objColorMeasurement.InkLevels.Add(objInkLevel)
                        Return
                     ElseIf InStr(CurrentRow, "PRINTER_INK" & vbTab & "9", CompareMethod.Text) > 0 Then
                        Dim objInkLevel As New InkLevel()
                        With objInkLevel
                            .InkPos = "PRINTER_INK 9"
                            .InkName = Fields(2)
                            .CurrLevel = Convert.ToDecimal(Fields(3))
                            .MaxLevel = Convert.ToDecimal(Fields(4))
                            .Measurement_GUID = objColorMeasurement.Measurement_GUID
                        End With
                        objColorMeasurement.InkLevels.Add(objInkLevel)
                        Return
                     End If
                ElseIf Fields.Contains("OUTPUT_RIP_TYPE") = True Then
                    If Fields.Length = 2 AndAlso Fields(1) <> String.Empty Then
                        objColorMeasurement.RipType = Fields(1)
                    End If
                    Return

                '*********************************************************
                ElseIf Fields.Contains("OUTPUT_TARGET_DENSITY_C") = True Then
                    objColorMeasurement.TargetDensity_C = Convert.ToDecimal(Fields(1))
                    Return
                ElseIf Fields.Contains("OUTPUT_TARGET_DENSITY_M") = True Then
                    objColorMeasurement.TargetDensity_M = Convert.ToDecimal(Fields(1))
                    Return
                ElseIf Fields.Contains("OUTPUT_TARGET_DENSITY_Y") = True Then
                    objColorMeasurement.TargetDensity_Y = Convert.ToDecimal(Fields(1))
                    Return
                ElseIf Fields.Contains("OUTPUT_TARGET_DENSITY_K") = True Then
                    objColorMeasurement.TargetDensity_K = Convert.ToDecimal(Fields(1))
                    Return
                ElseIf Fields.Contains("OUTPUT_TARGET_DENSITY_THRESHOLD") = True Then
                    objColorMeasurement.TargetDensity_Th = Convert.ToDecimal(Fields(1))
                    Return
                ElseIf Fields.Contains("OUTPUT_TARGET_DOT_GAIN_C") = True Then
                    objColorMeasurement.TargetDotGain_C = Convert.ToDecimal(Fields(1))
                    Return
                ElseIf Fields.Contains("OUTPUT_TARGET_DOT_GAIN_M") = True Then
                    objColorMeasurement.TargetDotGain_M = Convert.ToDecimal(Fields(1))
                    Return
                ElseIf Fields.Contains("OUTPUT_TARGET_DOT_GAIN_Y") = True Then
                    objColorMeasurement.TargetDotGain_Y = Convert.ToDecimal(Fields(1))
                    Return
                ElseIf Fields.Contains("OUTPUT_TARGET_DOT_GAIN_K") = True Then
                    objColorMeasurement.TargetDotGain_K = Convert.ToDecimal(Fields(1))
                    Return
                ElseIf Fields.Contains("OUTPUT_TARGET_DOT_GAIN_THRESHOLD") = True Then
                    objColorMeasurement.TargetDotGain_Th = Convert.ToDecimal(Fields(1))
                    Return
                ElseIf Fields.Contains("OUTPUT_TARGET_PRINT_CONTRAST_C") = True Then
                    objColorMeasurement.TargetPrintContrast_C = Convert.ToDecimal(Fields(1))
                    Return
                ElseIf Fields.Contains("OUTPUT_TARGET_PRINT_CONTRAST_M") = True Then
                    objColorMeasurement.TargetPrintContrast_M = Convert.ToDecimal(Fields(1))
                    Return
                ElseIf Fields.Contains("OUTPUT_TARGET_PRINT_CONTRAST_Y") = True Then
                    objColorMeasurement.TargetPrintContrast_Y = Convert.ToDecimal(Fields(1))
                    Return
                ElseIf Fields.Contains("OUTPUT_TARGET_PRINT_CONTRAST_K") = True Then
                    objColorMeasurement.TargetPrintContrast_K = Convert.ToDecimal(Fields(1))
                    Return
                ElseIf Fields.Contains("OUTPUT_TARGET_PRINT_CONTRAST_THRESHOLD") = True Then
                    objColorMeasurement.TargetPrintContrast_Th = Convert.ToDecimal(Fields(1))
                    Return
                ElseIf Fields.Contains("OUTPUT_TARGET_TRAP_R") = True Then
                    objColorMeasurement.TargetTrap_R = Convert.ToDecimal(Fields(1))
                    Return
                ElseIf Fields.Contains("OUTPUT_TARGET_TRAP_G") = True Then
                    objColorMeasurement.TargetTrap_G = Convert.ToDecimal(Fields(1))
                    Return
                ElseIf Fields.Contains("OUTPUT_TARGET_TRAP_B") = True Then
                    objColorMeasurement.TargetTrap_B = Convert.ToDecimal(Fields(1))
                    Return
                ElseIf Fields.Contains("OUTPUT_TARGET_TRAP_THRESHOLD") = True Then
                    objColorMeasurement.TargetTrap_Th = Convert.ToDecimal(Fields(1))
                    Return
                '***********************************************************
                ElseIf Fields.Contains("LGOROWLENGTH") = True Then
                    If Fields.Length = 2 Then
                         objColorMeasurement.NumOfPatchLines = CType(Fields(1), Integer)
                    Else
                        Throw New Exception("Number of Patch Lines Not Found")
                    End If
                    Return
                ElseIf Fields.Contains("NUMBER_OF_SETS") = True Then
                    objColorMeasurement.NumOfPatches = CType(Fields(1), Integer)
                End If

            End If

        Catch ex As Exception
            
            Throw New Exception("Exception thrown in ExtractColorMeasurement : " & ex.ToString)
        End Try
    End Sub



    ''' <summary>
    ''' This method parses tab delimited row from the measurement file and extract values from it
    ''' </summary>
    ''' <param name="CurrentRow"></param>
    ''' <remarks></remarks>
    Private Sub ExtractPlateMeasurement(ByVal CurrentRow As String)
        Try
            Dim SplitFieldsAt() As Char = {Chr(9)}   'Tab
            Dim Fields() As String = CurrentRow.Split(SplitFieldsAt, StringSplitOptions.RemoveEmptyEntries)
            If Fields.Length > 0 Then

                Select Case Fields(0)
                    Case "BEGIN_DATA_TOLERANCES","END_DATA_TOLERANCES","BEGIN_DATA_ACTUAL_VALUES", "END_DATA_ACTUAL_VALUES", "SAMPLE_ID"
                        Exit Sub
                End Select

    
                If Fields.Contains("ORIGINATOR") = True Then
                    If Fields.Length = 2 AndAlso Fields(1) <> String.Empty Then
                         objPlateMeasurement.Originator = Fields(1)
                    End If
                    Return
                ElseIf Fields.Contains("CREATED") = True Then
                    Dim dt As DateTime
                    'date received is in European Format
                    'dt = Convert.ToDateTime(Fields(1), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"))
                    'Convert it to the US format
                    'dt = Convert.ToDateTime(dt, System.Globalization.CultureInfo.CreateSpecificCulture("en-US"))
             
                    dt = DateTime.Parse(Fields(1),System.Globalization.CultureInfo.GetCultureInfo("en-GB"))
                    objPlateMeasurement.MeasurementDT = dt
                    Return
                ElseIf Fields.Contains("CREATEDGMT") = True Then
                    Dim dt As DateTime
                    'dt = Convert.ToDateTime(Fields(1), System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"))
                    'dt = Convert.ToDateTime(dt, System.Globalization.CultureInfo.CreateSpecificCulture("en-US"))
                    dt = DateTime.Parse(Fields(1),System.Globalization.CultureInfo.GetCultureInfo("en-GB"))
                    objPlateMeasurement.MeasurementGMT = dt
                    Return
                ElseIf Fields.Contains("TARGET_NAME") = True Then
                    If Fields.Length = 2 AndAlso Fields(1) <> String.Empty Then
                        objPlateMeasurement.TargetName = Fields(1)
                    End If
                    Return
                ElseIf Fields.Contains("TARGET_FILE") = True Then
                    If Fields.Length = 2 AndAlso Fields(1) <> String.Empty Then
                        objPlateMeasurement.TargetFile = Fields(1)
                    End If
                    Return
                ElseIf Fields.Contains("PROOF_SUPPLIER") = True Then
                    If Fields.Length = 2 AndAlso Fields(1) <> String.Empty Then
                        objPlateMeasurement.ProofSupplier = Fields(1)        
                    End If
                    Return
                ElseIf Fields.Contains("CP_SERIAL_NUM") = True Then
                    If Fields.Length = 2 AndAlso Fields(1) <> String.Empty Then
                         objPlateMeasurement.DongleNumber = Fields(1)
                    Else
                        Throw New Exception("CP Serial Number not found in the passed data")
                    End If
                    Return
                ElseIf Fields.Contains("GUID") = True Then
                    If Fields.Length = 2 AndAlso Fields(1) <> String.Empty Then
                         objPlateMeasurement.Measurement_GUID =  New Guid(Fields(1))
                    End If
                    Return
                ElseIf Fields.Contains("ID") = True OR Fields.Contains("ID  ") = True Then
                    objPlateMeasurement.CPVClientID =  Convert.ToInt32(Fields(1))
                    Return
                '******************************************************
                '************** BEGIN PLATE TOLERANCES
                ElseIf Fields.Contains("AVERAGE_NEGATIVE_LIMIT" ) = True Then
                    objPlateMeasurement.Targ_AvgNeg_DotArea =  Convert.ToDecimal(Fields(1))
                    Return                   
                ElseIf  (InStr(Fields(0),"AVERAGE_POSITIV_LIMIT",CompareMethod.Text) > 0  OR InStr(Fields(0),"AVERAGE_POSITIVE_LIMIT",CompareMethod.Text) > 0 ) Then
                    objPlateMeasurement.Targ_AvgPos_DotArea =  Convert.ToDecimal(Fields(1))
                    Return
                ElseIf Fields.Contains("MAXIMUM_NEGATIVE_LIMIT") = True  Then
                    objPlateMeasurement.Targ_MaxNeg_DotArea = Convert.ToDecimal(Fields(1))
                    Return
                ElseIf (InStr(Fields(0),"MAXIMUM_POSITIVE_LIMIT",CompareMethod.Text) > 0  OR InStr(Fields(0),"MAXIMUM_POSITIV_LIMIT",CompareMethod.Text) > 0 ) Then
                    objPlateMeasurement.Targ_MaxPos_DotArea = Convert.ToDecimal(Fields(1))
                    Return

               
                '*************** END PLATE TOLERANCES
                '*******************************************************

               
                '*******************************************************************
                '****************** BEGIN MEASURED PLATE VALUES ******************** 
                ElseIf Fields.Contains("AVERAGE_NEGATIVE") = True Then
                    objPlateMeasurement.AvgNeg_DotArea = Convert.ToDecimal(Fields(1))
                    Return
                ElseIf Microsoft.VisualBasic.InStr(Fields(0),"AVERAGE_POSITIV",CompareMethod.Text) > 0  Then
                    If Decimal.TryParse(Fields(1),System.Globalization.NumberStyles.Float) Then
                        objPlateMeasurement.AvgPos_DotArea = Convert.ToDecimal(Fields(1))
                    End If
                    Return
                ElseIf Fields.Contains("MAXIMUM_NEGATIVE") = True  Then
                    Dim Values() As String = Fields(1).Trim.Split(CChar(" "))
                    objPlateMeasurement.MaxNeg_DotArea = Convert.ToDecimal( Values(0) )
                    If Values.Length = 2 Then
                        objPlateMeasurement.MaxNeg_DotAreaPatch = Convert.ToString( Values(1) )
                    End If
                    Return
                ElseIf Microsoft.VisualBasic.InStr(Fields(0),"MAXIMUM_POSITIV",CompareMethod.Text) > 0 Then    
                    Dim Values() As String = Fields(1).Trim.Split(CChar(" "))
                    objPlateMeasurement.MaxPos_DotArea = Convert.ToDecimal( Values(0) )
                    If Values.Length = 2 Then
                        objPlateMeasurement.MaxPos_DotAreaPatch = Convert.ToString( Values(1) )
                    End If
                    Return
                '******************* END MEASURED PLATE VALUES *********************
                '*******************************************************************
               
                ElseIf (InStr(CurrentRow, "USER", CompareMethod.Binary) > 0 AndAlso InStr(CurrentRow,"_USER") = 0 AndAlso Instr(CurrentRow.ToLower(),"\user") = 0 )  Then
                    If Fields.Length = 2 AndAlso Fields(1) <> String.Empty Then
                         objPlateMeasurement.User = Fields(1)    
                    End If
                    Return
                ElseIf Fields.Contains("DEVICE") = True Then
                    If Fields.Length =2 AndAlso Fields(1) <> String.Empty Then
                        'Get Last Occurance of Space - that will give us Measurement Device  
                        'for e.g. icPLATE II 456546  
                        Dim i As Integer = Fields(1).LastIndexOf(CChar(" "))
                        objPlateMeasurement.Instrument =  Fields(1).Substring(0,i)   'icPLATE II 
                        objPlateMeasurement.Instrument_SerialNum = Fields(1).Substring(i,Fields(1).Length-i)    '456546   
                    End If
                    Return
                ElseIf Fields.Contains("MEASUREMENT") = True Then
                    If Fields.Length = 2 AndAlso Fields(1) <> String.Empty Then
                        Dim Values() As String = Fields(1).Trim.Split(CChar(","))
                        objPlateMeasurement.ObserverType = Values(0)
                        objPlateMeasurement.Illuminant = Values(1)
                        objPlateMeasurement.Filter = Values(2)
                    End If
                    Return
                ElseIf Fields.Contains("CUSTOMER") = True Then
                    If Fields.Length = 2 AndAlso Fields(1) <> String.Empty Then
                         objPlateMeasurement.Customer = Fields(1)
                    End If
                    Return
                ElseIf Fields.Contains("JOB") = True Then
                    If Fields.Length = 2 AndAlso Fields(1) <> String.Empty Then
                        objPlateMeasurement.Job = Fields(1)
                    End If
                    Return
                ElseIf Fields.Contains("PROOFER") = True Then
                    If Fields.Length = 2 AndAlso Fields(1) <> String.Empty Then
                        objPlateMeasurement.Proofer = Fields(1)
                    End If
                    Return
                ElseIf Fields.Contains("INFO ") = True Then
                    If Fields.Length = 2 AndAlso Fields(1) <> String.Empty Then
                         objPlateMeasurement.Info = Fields(1)
                    End If
                    Return
                ElseIf Fields.Contains("PROOF_OK") = True Then
                    If Convert.ToInt16(Fields(1)) = 0 Then
                        objPlateMeasurement.ProofSatus = PlateMeasurement.ProofStaus.Failed
                    ElseIf Convert.ToInt16(Fields(1)) = 1 Then
                        objPlateMeasurement.ProofSatus = PlateMeasurement.ProofStaus.Caution
                    Else
                        objPlateMeasurement.ProofSatus = PlateMeasurement.ProofStaus.Passed
                    End If
                    Return
                ElseIf Fields.Contains("OUTPUT_SUBSTRATE") = True Then
                    If Fields.Length = 2 AndAlso Fields(1) <> String.Empty Then
                         objPlateMeasurement.Substrate = Fields(1)
                    End If
                    Return
                ElseIf Fields.Contains("OUTPUT_SUBSTRATE_SKU") = True Then
                    If Fields.Length = 2 AndAlso Fields(1) <> String.Empty Then
                         objPlateMeasurement.SubstrateSKU = Fields(1)
                    End If
                    Return
                ElseIf Fields.Contains("OUTPUT_SUBSTRATE_BATCH") = True Then
                    If Fields.Length = 2 AndAlso Fields(1) <> String.Empty Then
                        objPlateMeasurement.SubstrateBatch = Fields(1)
                    End If
                    Return
                ElseIf Fields.Contains("OUTPUT_MANUFACTURER") = True Then
                    If Fields.Length = 2 AndAlso Fields(1) <> String.Empty Then
                        objPlateMeasurement.PrinterMake = Fields(1)
                    End If
                    Return
                ElseIf Fields.Contains("OUTPUT_MODEL") = True Then
                    If Fields.Length = 2 AndAlso Fields(1) <> String.Empty Then
                         objPlateMeasurement.PrinterModel = Fields(1)
                    End If
                    Return
                ElseIf Fields.Contains("OUTPUT_SERIAL_NUMBER") = True Then
                    If Fields.Length = 2 AndAlso Fields(1) <> String.Empty Then
                        objPlateMeasurement.PrinterSerialNum = Fields(1)
                    End If
                    Return
                ElseIf Fields.Contains("OUTPUT_RIP_TYPE") = True Then
                    If Fields.Length = 2 AndAlso Fields(1) <> String.Empty Then
                        objPlateMeasurement.RipType = Fields(1)
                    End If
                    Return
                 ElseIf Fields.Contains("OUTPUT_CATEGORY") = True Then
                    If Fields.Length = 2 AndAlso Fields(1) <> String.Empty Then
                        objPlateMeasurement.OutputCategory = Fields(1)
                    End If
                    Return
                ElseIf Fields.Contains("OUTPUT_SEPARATION") = True Then
                    If Fields.Length = 2 AndAlso Fields(1) <> String.Empty Then
                        objPlateMeasurement.OuputSeparation = Fields(1)
                        Return
                    End If
                ElseIf Fields.Contains("OUTPUT_RESOLUTION") = True Then
                    If Fields.Length = 2 AndAlso Fields(1) <> String.Empty Then
                        objPlateMeasurement.OutputResolution = Fields(1)
                    End If
                    Return
                ElseIf Fields.Contains("OUTPUT_LINE_SCREEN") = True Then
                    If Fields.Length = 2 AndAlso Fields(1) <> String.Empty Then
                        objPlateMeasurement.OutputLineScreen = Fields(1)
                    End If
                    Return
                ElseIf Fields.Contains("OUTPUT_DOT_SHAPE") = True Then
                    If Fields.Length = 2 AndAlso Fields(1) <> String.Empty Then
                        objPlateMeasurement.OutputDotShape = Fields(1)
                    End If
                    Return
                ElseIf Microsoft.VisualBasic.InStr(Fields(0), "LGOROWLENGTH", CompareMethod.Text) > 0 Then  ''NO TABS HERE then perform a partial search
                    Dim Values() As String = Fields(0).Trim.Split(CChar(" "))
                    If Integer.TryParse(Values(1), System.Globalization.NumberStyles.None) = True Then
                         objPlateMeasurement.NumOfPatchLines = CType(Values(1), Integer)
                    End If
                    Return
                ElseIf Fields.Contains("NUMBER_OF_SETS") = True Then
                    If Fields.Length = 2 Then
                        objPlateMeasurement.NumOfPatches = CType(Fields(1), Integer)
                    End If
                End If

            End If

        Catch ex As Exception
            Throw New Exception("Exception thrwon in ExtractPlateMeasurement : " & ex.ToString)
        End Try
    End Sub


    ''' <summary>
    ''' Extract Individual Patch Data From IT-8 File
    ''' </summary>
    ''' <param name="CurrentRow"></param>
    ''' <remarks>cshah@fujifilmgs.com</remarks>
    Private Sub ExtractPatchData(ByVal CurrentRow As String)
       
        Dim SplitFieldsAt() As Char = {Chr(9)}   'Tab
        Dim Items() As String = CurrentRow.Split(SplitFieldsAt, StringSplitOptions.RemoveEmptyEntries)

        Try

            If MeasurementType = Type.Plate Then
                Dim objMeasuredPatch As New MeasuredPatch_Plate()
                objMeasuredPatch.Measurement_GUID = objPlateMeasurement.Measurement_GUID
                If Items.Length = 7 Then   'Tol Strict are now always in the file
                    For i As Integer = 0 To Items.Length - 1
                    Select Case i
                        Case 0 'First Item is Sample_ID
                            objMeasuredPatch.SampleID = CType(Items(i), Integer)
                        Case 1 'Sample Name
                            objMeasuredPatch.SampleName = CType(Items(i), String)
                        Case 2 'Target Dot Area
                            objMeasuredPatch.TargetDotArea = CType(Items(i), Decimal)
                        Case 3 'Actual Dot Area
                            objMeasuredPatch.ActualDotArea = CType(Items(i), Decimal)
                        Case 4 'Deviation
                            objMeasuredPatch.Deviation = CType(Items(i), Decimal)
                        Case 5 'Tol Strict
                            objMeasuredPatch.TolStrict = CType(Items(i), Decimal)
                        Case 6 'Dot Area Reference
                            objMeasuredPatch.ReferenceDotArea = CType(Items(i), Decimal)
                    End Select 
                    Next     
                Else
                    For i As Integer = 0 To Items.Length - 1
                    Select Case i
                        Case 0 'First Item is Sample_ID
                            objMeasuredPatch.SampleID = CType(Items(i), Integer)
                        Case 1 'Sample Name
                            objMeasuredPatch.SampleName = CType(Items(i), String)
                        Case 2 'Target Dot Area
                            objMeasuredPatch.TargetDotArea = CType(Items(i), Decimal)
                        Case 3 'Actual Dot Area
                            objMeasuredPatch.ActualDotArea = CType(Items(i), Decimal)
                        Case 4 'Deviation
                            objMeasuredPatch.Deviation = CType(Items(i), Decimal)
                        Case 5 'Dot Area Reference
                            objMeasuredPatch.ReferenceDotArea = CType(Items(i), Decimal)
                     
                    End Select 
                    Next     

                End If
                objPlateMeasurement.MeasuredPatches.Add(objMeasuredPatch)        'Add to collection
            Else            
                Dim objMeasuredPatch As New MeasuredPatch_Color()
                objMeasuredPatch.Measurement_GUID = objColorMeasurement.Measurement_GUID
                For i As Integer = 0 To Items.Length - 1
    
                    Select Case i
    
                        Case 0 'First Item is Sample_ID
                             objMeasuredPatch.SampleID = CType(Items(i), Integer)
                        Case 1
                             objMeasuredPatch.SampleName = CType(Items(i), String)
                        Case 2
                             objMeasuredPatch.CMYK_C = CType(Items(i), Integer)
                        Case 3
                             objMeasuredPatch.CMYK_M = CType(Items(i), Integer)
                        Case 4
                             objMeasuredPatch.CMYK_Y = CType(Items(i), Integer)
                        Case 5
                             objMeasuredPatch.CMYK_K = CType(Items(i), Integer)
                        Case 6
                             objMeasuredPatch.CIELAB_L_Target = CType(Items(i), Decimal)
                        Case 7
                             objMeasuredPatch.CIELAB_a_Target = CType(Items(i), Decimal)
                        Case 8
                             objMeasuredPatch.CIELAB_b_Target = CType(Items(i), Decimal)
                        Case 9
                             objMeasuredPatch.CIELAB_L = CType(Items(i), Decimal)
                        Case 10
                             objMeasuredPatch.CIELAB_a = CType(Items(i), Decimal)
                        Case 11
                             objMeasuredPatch.CIELAB_b = CType(Items(i), Decimal)
                        Case 12
                            objMeasuredPatch.deltaE = CType(Items(i), Decimal)
                        Case 13
                            objMeasuredPatch.dL = CType(Items(i), Decimal)
                        Case 14
                            objMeasuredPatch.dA = CType(Items(i), Decimal)
                        Case 15
                            objMeasuredPatch.dB = CType(Items(i), Decimal)
                        Case 16
                            objMeasuredPatch.dH = CType(Items(i), Decimal)
                        Case 17
                            objMeasuredPatch.dC = CType(Items(i), Decimal)
                        Case 18
                            objMeasuredPatch.Density = CType(Items(i), Decimal)
                        Case 19
                            If Items.Length = 20 Then
                                 objMeasuredPatch.TolCaution = CType(Items(i), Decimal)
                            End If
                        Case 20
                            If Items.Length = 21 Then
                            objMeasuredPatch.TolStrict = CType(Items(i), Decimal)
                             End If
                    End Select
        
                Next i

                'objMeasuredPatch.Measurement_GUID = Me.MeasurementGUID
                objColorMeasurement.MeasuredPatches.Add(objMeasuredPatch)        'Add to collection
            
            End If
    Catch ex As Exception
        Throw New Exception("Exception thrown in ExtractPatchData Method: " & ex.ToString)
    End Try

    End Sub
    
End Class