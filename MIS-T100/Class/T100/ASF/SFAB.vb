Public Class SFAB
    '''<reamrks>Table Production Detail</reamrks>
    Public Shared tblMOSoureFile As String = "sfab_t"
    '''<reamrks> Column </reamrks>
    Public Shared DocNo As String = "sfabdocno" '--MO
    Public Shared Soure As String = "sfab001"
    Public Shared SoureDocno As String = "sfab002" '--SOChag.
    Public Shared LineNo As String = "sfab003"
    Public Shared SoureItemSeq As String = "sfab004" '--SeqItemSOChag.
    Public Shared ent As String = "sfabent"
    Public Shared Site As String = "sfabsite"
    '''<reamrks> Condition Field </reamrks>
    Public Shared wStandard As String = Site & "='JINPAO' and " & ent & "='3' "

    '--Page SalesOrderChangeStatus
    '--SelectDocNoMOOperatLine where DocNo / No Rrfesh DataTable
    Private Shared SelectSoureItemSeq As String = "select " & DocNo & "," & SoureItemSeq & " from " & tblMOSoureFile & " " &
        " where " & wStandard & "and  " & DocNo & " ='@docNo'"
    Public Shared Function GetDocNoMOLine(ByVal DocNoMOLine As String)
        Dim Oral As String = SelectSoureItemSeq
        Dim tempDataTable As New DataTable
        Oral = Oral.Replace("@docNo", DocNoMOLine)
        GetData.Get_DataReaderOracle(Oral, clsDBConnect.strT100ConnectionString)
        Return tempDataTable
    End Function
End Class
