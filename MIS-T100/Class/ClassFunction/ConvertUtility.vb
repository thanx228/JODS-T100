Public Class ConvertUtility
    '# Convert String to (Integer or Numberice)
    Public Shared Function FormatInteger(ByVal ValueString As String)
        Dim ConvertToNum As Integer = CInt(ValueString)
        Return ConvertToNum
    End Function
    '# Convert String To  Double
    Public Shared Function FormatDouble(ByVal ValueString As String)
        Dim ConvertToDuble As Double = CDbl(ValueString)
        Return ConvertToDuble
    End Function
    Public Function ToDecimalFromString(str As String)
        Dim temp As String = String.Format("{0}{2}", ".", str)
        Dim dec As Decimal
        Decimal.TryParse(temp, dec)
        Return dec
    End Function
    '# Convert String To  Decimal
    Public Shared Function FormatDecimal(ByVal ValueString As String)
        Dim ConvertToDecimal As Decimal = CDbl(ValueString)
        Return ConvertToDecimal
    End Function

    '# Convert String to  Format  DateTime
    Public Shared Function StringToDateTime(ByVal StringDateTime As String) As DateTime
        Dim ToDateTime As DateTime = DateTime.Parse(StringDateTime)
        Return ToDateTime
    End Function
    '# Convert String DateTime to  Format  Date
    Public Shared Function StringDateTimeToDate(ByVal StringDate As String) As Date
        Dim convertedDate As Date = DateTime.Parse(StringDate)
        Return convertedDate
    End Function
    '# Convert String  to  Format  Date
    Public Shared Function StringToDate(ByVal StringDate As String) As Date
        Dim convertedDate As Date = Date.Parse(StringDate)
        Return convertedDate
    End Function
    '# Style 1  Convert Second  Format  hh:mm:ss
    Public Shared Function FieldSecondTohhmmss(ByVal Second As String)
        Dim ToDecimal As Decimal = CDbl(Second)
        If ToDecimal > 0 Then
            Dim MoSplit As String = Second
            Dim hhmmss = MoSplit.Split(":")
            Dim S_hh As Integer = CInt(hhmmss(0))
            Dim S_mm As Integer = CInt(hhmmss(1))
            Dim S_ss As Integer = CInt(hhmmss(2))
            Second = S_hh.ToString("00") & ":" & S_mm.ToString("00") & ":" & S_ss.ToString("00")
        End If
        Return Second
    End Function
    '# Style 2  Convert Decimal (1.5, 2.5) Fromat TimeSpan  hh:mm:ss
    Public Shared Function DecimalFromatTimeSpan(ByVal Parameter As String)
        Dim ToDecimal As Decimal = CDbl(Parameter)
        If ToDecimal > 0 Then
            Dim shww As String = Parameter
            Dim string_hh = shww.ToString().Split(".")
            Dim hh As Integer = CInt(string_hh(0))
            Dim strmm As String = "0." & string_hh(1)
            Dim tmm As Decimal = Convert.ToDecimal(strmm)
            Dim ttmm As Decimal = tmm * 60
            Dim sttmm = ttmm.ToString().Split(".")
            Dim shttmm As Integer = CInt(sttmm(0))
            Dim stss As String = "0." & sttmm(1)
            Dim tss As Decimal = Convert.ToDecimal(stss)
            Dim ttss As Decimal = tss * 60
            Parameter = hh.ToString("00") & ":" & shttmm.ToString("00") & ":" & ttss.ToString("00")
        End If
        Return Parameter
    End Function
    '# Convert OT  Format  TimeSpan
    Public Shared Function OT_TimeSpan(ByVal TimeSpan As String)
        Dim hhmmss = TimeSpan.Split(":")
        Dim S_hh As Integer = CInt(hhmmss(0))
        Dim S_mm As Integer = CInt(hhmmss(1))
        Dim S_ss As Integer = CInt(hhmmss(2))
        Dim oTS As New TimeSpan(S_hh, S_mm, S_ss)
        Dim startime As DateTime = Date.Now
        Dim newtime As DateTime = startime + oTS
        TimeSpan = newtime.ToString("HH:mm")
        TimeSpan = Format(oTS.Hours, "00") & ":" & Format(oTS.Minutes, "00")
        Return TimeSpan
    End Function
    '# Convert String to Currency Unit 10,540.00 บาท ( Unit Price ,Quantity, Amount,Discount )
    Public Shared Function StringToCurrencyTH(ByVal ValueUnit As String)
        If ValueUnit <> "0" Or ValueUnit <> "" Then
            Dim intMinShoot As Integer = CInt(ValueUnit)
            Dim StrFromatMin As Double = intMinShoot
            Dim stMin As String = [String].Format("{0:F2}", StrFromatMin)
            ValueUnit = stMin
            ValueUnit = Format(CInt(ValueUnit), "#,000")
        End If
        Return ValueUnit
    End Function

    ''' <summary>
    '''  # Dela Percent *** Page >> SaleOrderDelay.aspx
    ''' </summary>
    ''' <param name="Delay"></param>
    ''' <param name="DelayAll"></param>
    ''' <returns></returns>
    Public Shared Function CalPercent(Delay As Integer, DelayAll As Integer) As Double
        Dim Percent As Double = 0
        If Delay > 0 Then
            Percent = (Delay / DelayAll) * 100
        End If
        Return Math.Round(Percent, 2)
    End Function
    ''' <summary>
    ''' Claculator Percent Standard
    ''' </summary>
    ''' <param name="Filed"></param>
    ''' <param name="Sum"></param>
    ''' <returns></returns>
    Public Shared Function Calc_Percent(Filed As Integer, Sum As Integer) As Double
        Dim Percent As Double = 0
        If Filed > 0 Then
            Percent = (Filed / Sum) * 100
        End If
        Return Math.Round(Percent, 2)
    End Function
    ''' <summary>
    ''' Calculator Count Day
    ''' </summary>
    ''' <param name="StartDate"></param>
    ''' <param name="EndDate"></param>
    ''' <returns> Count Days </returns>
    Public Shared Function CalculatorCountDay(StartDate As String, EndDate As String) As Integer
        Dim strSdate = StartDate.Split("/")
        Dim S_yyyy As Integer = CInt(strSdate(2))
        Dim S_mm As Integer = CInt(strSdate(1))
        Dim S_dd As Integer = CInt(strSdate(0))
        Dim sDate As New Date(S_yyyy, S_mm, S_dd)
        Dim strEdate = EndDate.Split("/")
        Dim E_yyyy As Integer = CInt(strEdate(2))
        Dim E_mm As Integer = CInt(strEdate(1))
        Dim E_dd As Integer = CInt(strEdate(0))
        Dim EDate As New Date(E_yyyy, E_mm, E_dd)
        Dim CountDays = DateDiff(DateInterval.Day, sDate, EDate)
        Return CountDays
    End Function
    ''' <summary>
    ''' Between Date
    ''' </summary>
    ''' <param name="StartDate"></param>
    ''' <param name="EndDate"></param>
    ''' <returns></returns>
    Public Shared Function BetweenDate(StartDate As String, EndDate As String) As Date
        Dim Sdd As Date = Date.Parse(StartDate)
        Dim cStrDate As String = Sdd.ToString("dd/MM/yyyy")
        Dim Edd As Date = Date.Parse(EndDate)
        Dim cEtrDate As String = Edd.ToString("dd/MM/yyyy")
        Dim strSdate = cStrDate.Split("/")
        Dim S_yyyy As Integer = CInt(strSdate(2))
        Dim S_mm As Integer = CInt(strSdate(1))
        Dim S_dd As Integer = CInt(strSdate(0))
        Dim sDate As New Date(S_yyyy, S_mm, S_dd)
        Dim strEdate = cEtrDate.Split("/")
        Dim E_yyyy As Integer = CInt(strEdate(2))
        Dim E_mm As Integer = CInt(strEdate(1))
        Dim E_dd As Integer = CInt(strEdate(0))
        Dim EDate As New Date(E_yyyy, E_mm, E_dd)
        Dim RangeDate As New Date(sDate.Subtract(EDate).Days)
        Return RangeDate
    End Function
    Public Shared Function StToDate(StrDate As String) As Date
        Dim dd As Date = Date.Parse(StrDate)
        Dim cStrDate As String = dd.ToString("dd/MM/yyyy")
        Dim strSdate = cStrDate.Split("/")
        Dim S_yyyy As Integer = CInt(strSdate(2))
        Dim S_mm As Integer = CInt(strSdate(1))
        Dim S_dd As Integer = CInt(strSdate(0))
        Dim sDate As New Date(S_yyyy, S_mm, S_dd)
        Return sDate
    End Function
    ''' <summary>
    ''' Left  Next Charator A-Z (Charator + 1)
    ''' </summary>
    ''' <param name="strL"></param>
    ''' <returns></returns>
    Public Shared Function nextCharLeft(strL As String) As String
        Return Chr(Asc(strL) + 1)
    End Function
    ''' <summary>
    ''' Right Next Charator A-Z (Charator + 1)
    ''' </summary>
    ''' <param name="strR"></param>
    ''' <returns></returns>
    Public Shared Function nextCharRight(strR As String) As String
        Return Chr(Asc(strR) + 1)
    End Function
    Public Shared Function CalcHHMM(Min As String) As String
        Dim showHHMM As String = String.Empty
        Dim iRowMc As Integer = CInt(Int(Min))
        If iRowMc > 0 Then
            Dim hours As Integer = iRowMc \ 60
            Dim minutes As Integer = iRowMc - (hours * 60)
            Dim timeElapsed As String = CType(hours, String) & ":" & CType(minutes.ToString("00"), String)
            showHHMM = timeElapsed
        Else
            showHHMM = "00:00"
        End If
        Return showHHMM
    End Function
End Class
