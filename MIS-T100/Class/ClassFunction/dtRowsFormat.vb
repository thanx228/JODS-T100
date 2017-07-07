Imports System
Imports System.Data
Imports System.Web.UI.Control
Public Class dtRowsFormat
    '################################# DatTable Bind Rows Format Type.  ################################################
    '# Bind DataRows to Format String
    Public Shared Function FormatString(ByVal dt As DataTable, ByVal ValueString As String)
        Dim ShowValueString As String = IIf(IsDBNull(dt.Rows(0).Item(ValueString)), String.Empty, dt.Rows(0).Item(ValueString))
        Return ShowValueString
    End Function
    '# Bind DataRows to Format String + String
    Public Shared Function FormatSumString(ByVal dt As DataTable, ByVal ValueString1 As String, ByVal ValueString2 As String)
        Dim ShowValueSumString As String = IIf(IsDBNull(dt.Rows(0).Item(ValueString1)), String.Empty, dt.Rows(0).Item(ValueString1)) & " : " & IIf(IsDBNull(dt.Rows(0).Item(ValueString2)), String.Empty, dt.Rows(0).Item(ValueString2))
        Return ShowValueSumString
    End Function
    '# Bind DataRows to Format Decimal 
    Public Shared Function FormatDecimal(ByVal dt As DataTable, ByVal ValueDecimal As String)
        Dim ShowValueUnit As String = IIf(IsDBNull(dt.Rows(0)(ValueDecimal)), String.Empty, [String].Format("{0:0.00}", dt.Rows(0)(ValueDecimal)))
        Return ShowValueUnit
    End Function
    '# Bind DT DataRows to Currency Unit 10,540.00 บาท ( Unit Price ,Quantity, Amount,Discount )
    Public Shared Function FormatUnit(ByVal dt As DataTable, ByVal ValueUnit As String)
        Dim ShowValueUnit As String = IIf(IsDBNull(dt.Rows(0)(ValueUnit)), String.Empty, dt.Rows(0)(ValueUnit))
        'If ShowValueUnit <> "" Then
        '    Dim intMinShoot As Integer = CInt(ShowValueUnit)
        '    Dim StrFromatMin As Double = intMinShoot
        '    Dim stMin As String = [String].Format("{0:F2}", StrFromatMin)
        '    ShowValueUnit = stMin
        '    ShowValueUnit = Format(CInt(ShowValueUnit), "#,000")
        'End If
        If ShowValueUnit <> String.Empty Then
            Dim intVloume As Integer = CInt(ShowValueUnit)
            Dim StrFromatMin As Double = intVloume
            Dim stMin As String = [String].Format("{0:F2}", StrFromatMin)
            ShowValueUnit = stMin
            ShowValueUnit = Format(CInt(ShowValueUnit), "#,000")
        Else
            ShowValueUnit = "0.00"
        End If
        Return ShowValueUnit
    End Function
    '# Bind DataRows to Currency Unit 10,540.00 บาท ( Unit Price ,Quantity, Amount,Discount )
    Public Shared Function FormatStringUnit(ByVal ValueUnit As String)
        Dim ShowValueUnit As String = ValueUnit
        If ShowValueUnit <> String.Empty Then
            Dim intVloume As Integer = CInt(ShowValueUnit)
            Dim StrFromatMin As Double = intVloume
            Dim stMin As String = [String].Format("{0:F2}", StrFromatMin)
            ShowValueUnit = stMin
            ShowValueUnit = Format(CInt(ShowValueUnit), "#,000")
        Else
            ShowValueUnit = "0.00"
        End If
        Return ShowValueUnit
    End Function
    '# Bind DataRows to Format Date ( MM/dd/yyyy )
    Public Shared Function FormatDateMMddyyyy(ByVal dt As DataTable, ByVal ValueDate As String)
        Dim ShowValueDate As String = IIf(IsDBNull(dt.Rows(0)(ValueDate)), String.Empty, [String].Format("{0:MM/dd/yyyy}", dt.Rows(0)(ValueDate)))
        Return ShowValueDate
    End Function
    '# Bind DataRows to Format Date ( yyyy-MM-dd)
    Public Shared Function FormatDateyyyyMMdd(ByVal dt As DataTable, ByVal ValueDate As String)
        Dim ShowValueDate As String = IIf(IsDBNull(dt.Rows(0)(ValueDate)), String.Empty, [String].Format("{0:yyyy-MM-dd}", dt.Rows(0)(ValueDate)))
        Return ShowValueDate
    End Function
    '# Bind DataRows to Format DateTime (yyyy-MM-dd hh:mm:ss:tt AM,PM)
    Public Shared Function FormatDateTime(ByVal dt As DataTable, ByVal ValueDateTime As String)
        Dim ShowValueDateTime As String = IIf(IsDBNull(dt.Rows(0)(ValueDateTime)), String.Empty, [String].Format("{yyyy-MM-dd hh:mm:ss:tt}", dt.Rows(0)(ValueDateTime)))
        Return ShowValueDateTime
    End Function
    '# Bind DataRows to Format : DateTime convert to Time (hh:mm:ss)
    Public Shared Function FormatDateTimeToTime(ByVal dt As DataTable, ByVal ValueDateTime As String)
        Dim ShowValueDateTime As String = IIf(IsDBNull(dt.Rows(0)(ValueDateTime)), String.Empty, [String].Format("{0:hh:mm:ss}", dt.Rows(0)(ValueDateTime)))
        Return ShowValueDateTime
    End Function

    Public Shared Function AutoRunNumber(dt As DataTable, strDocNo_DB As String)
        Dim dd, mm, yyyy As Integer
        yyyy = System.DateTime.Now.Year.ToString("0000")
        mm = System.DateTime.Now.Month.ToString("00")
        dd = System.DateTime.Now.Day.ToString("00")
        'AutiRun Now
        Dim RunNum_New As String
        ' New
        Dim DocNo, DayNew, yymmddNew As String
        Dim s_Run As String
        Dim tmpMemberID As Integer = 0
        Dim DocNo_DB, DocDate_DB, DocRunNum_DB As String
        If dt.Rows.Count > 0 Then

            DocNo_DB = IIf(IsDBNull(dt.Rows(0).Item("yymmdd")), String.Empty, dt.Rows(0).Item("yymmdd"))
            DocDate_DB = IIf(IsDBNull(dt.Rows(0).Item("Days")), String.Empty, dt.Rows(0).Item("Days"))
            DocRunNum_DB = IIf(IsDBNull(dt.Rows(0).Item("RunNum")), String.Empty, dt.Rows(0).Item("RunNum"))
            Dim sys_yymmdd As String = System.DateTime.Now.ToString("yyyyMMdd")
            Dim integer_yymmdd As Integer = CInt(sys_yymmdd)
            Dim integer_yymmdd_DB As Integer = CInt(DocNo_DB)

            If integer_yymmdd_DB = integer_yymmdd Then
                tmpMemberID = CInt(DocRunNum_DB) + 1
                s_Run = tmpMemberID.ToString("0000")
                DocNo = yyyy & mm & dd & s_Run
                DayNew = dd
                yymmddNew = yyyy & mm & dd
            ElseIf integer_yymmdd_DB <> integer_yymmdd Then
                tmpMemberID = CInt("0000") + 1
                s_Run = tmpMemberID.ToString("0000")
                DocNo = yyyy & mm & dd & s_Run
                DayNew = DocDate_DB
                yymmddNew = DocNo_DB
            End If
            RunNum_New = s_Run
        Else
            Dim sys_yymmdd As String = System.DateTime.Now.ToString("yyyyMMdd")
            Dim integer_yymmdd As Integer = CInt(sys_yymmdd)
            tmpMemberID = CInt(tmpMemberID) + 1
            s_Run = tmpMemberID.ToString("0000")
            DocNo = yyyy & mm & dd & s_Run
            DayNew = dd
            yymmddNew = yyyy & mm & dd
            RunNum_New = s_Run

            DocNo_DB = System.DateTime.Now.ToString("yyyyMMdd")
            DocDate_DB = System.DateTime.Now.Day.ToString()
            DocRunNum_DB = "0000"
        End If
        Return RunNum_New ' yyyy + mm + dd + 0001
    End Function

    Public Shared Function GetBatchNo(txt As String)
        Dim BarNo As String = txt
        If BarNo = "NULL" Or BarNo = "" Or Left(BarNo, 1) = "*" Then
            BarNo = "0A"
        Else
            Dim strL As String = BarNo.Substring(0, 1) 'left char
            Dim strR As String = BarNo.Substring(1, 1) 'right char
            If strL = "9" And strR = "Z" Then
                strL = "A"
                strR = "0"
            Else
                'check numberic for char left
                If IsNumeric(strL) Then 'left isnumric = true
                    If strL = "9" Then 'new char right and zero
                        strL = "0"
                        strR = nextChar(strR)
                    Else 'run number continue
                        strL = nextChar(strL)
                    End If
                Else 'right isnumberic= true
                    If strR = "9" Then
                        strL = nextChar(strL)
                        strR = "0"
                    Else
                        strR = nextChar(strR)
                    End If
                End If
            End If
            BarNo = strL & strR
        End If
        'If cbHardTool.Checked Then
        '    BarNo &= "H"
        'End If
        Return BarNo
    End Function
    Private Shared Function getBatchNoFA(Str As String, cbHardTool As CheckBox)
        Dim BarNo As String
        'gen new batch no
        BarNo = Str
        If BarNo = String.Empty Or BarNo = "" Or Left(BarNo, 1) = "*" Then
            BarNo = "FA1"
        Else
            Dim lastNo As Integer = CInt(BarNo.Replace("FA", "").Replace("H", "")) + 1
            BarNo = "FA" & lastNo.ToString
        End If
        If cbHardTool.Checked Then
            BarNo &= "H"
        End If
        Return BarNo
    End Function
    'Private Shared Function nextChar(strL As String, strR As String) As String
    Private Shared Function nextChar(strR As String) As String
        Dim StrReturn As String
        'If strL = "Z" Then
        '    strL = "A"
        '    StrReturn = Chr(1 + Asc(strR))
        'Else

        'End If
        StrReturn = Chr(Asc(strR) + 1)
        Return StrReturn
    End Function
End Class
