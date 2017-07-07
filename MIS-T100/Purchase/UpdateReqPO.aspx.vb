Public Class UpdateReqPO
    Inherits System.Web.UI.Page
    Dim clsDBConnect As New clsDBConnectT100
    Dim Conn_SQL As New ConnSQL
    Dim ControlForm As New ControlDataForm
    Dim CreateTable As New CreateTempTableT100
    Dim tableLog As String = "RequirePrToPoLog"
    Dim AssetPRType As String = "'CA01','CA02','CA03','CA04','CA05','CA06'"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                Response.Redirect(Server.UrlPathEncode("../LoginT100.aspx"))
            End If
            Dim SQL As String = ""
            SQL = "Select oobxl001 Code,oobxl001||' : '||oobxl003 CodeName From oobxl_t where oobxlent='3' and oobxl001 in (" & AssetPRType & ")"
            showDDL(ddlType, SQL, "CodeName", "Code", True, clsDBConnect.T100)
            CreateTable.CreateRequirePrToPoLog()
            changeLoc()
        End If
    End Sub
    Public Sub showDDL(ByRef ControlDDL As DropDownList, ByVal str_sqlcommand As String, ByVal fldText As String, ByVal fldValue As String, Optional ByVal setAll As Boolean = False, Optional ByVal connectSting As String = "", Optional ByVal headVal As String = "ALL", Optional dbType As String = "A")
        Dim dt As New DataTable
        dt = clsDBConnect.QueryDataTable(str_sqlcommand, connectSting)
        clsDBConnect.Close(connectSting)

        With ControlDDL
            .DataSource = dt
            .DataTextField = fldText
            .DataValueField = fldValue
            .DataBind()
            If setAll = True Then
                .Items.Insert(0, headVal)
            End If
        End With
    End Sub
    'Protected Sub changeLocNew()
    '    Dim prType As String = "CA"
    '    If ddlPrLoc.Text.Trim = "2" Then
    '        prType = "31"
    '    End If

    'End Sub


    Protected Sub changeLoc() Handles ddlPrLoc.SelectedIndexChanged
        reset()
        'changeLocNew()
    End Sub

    Protected Sub reset() Handles btReset.Click
        'changeLocNew()
        tbPrNo.Text = ""
        tbPrSeq.Text = ""
        tbRequire1.Text = ""
        tbRequire2.Text = ""
        tbRequire3.Text = ""
        lbPoTypeNo.Text = ""
    End Sub

    Protected Sub btCheck_Click(sender As Object, e As EventArgs) Handles btCheck.Click
        If tbPrNo.Text = "" Then
            tbPrNo.Focus()
            Exit Sub
        End If

        Dim SQL As String,
            dt As DataTable,
            WHR As String = ""

        'Fix asset + pr
        WHR &= Conn_SQL.Where("SUBSTR(pmdadocno,3,4)", ddlType)
        WHR &= Conn_SQL.Where("SUBSTR(pmdadocno,8,11)", tbPrNo, False)
        'WHR &= Conn_SQL.Where("pmdoseq", tbPrSeq, False)

        SQL = "select pmdldocno ,NVL('','') Remark1,NVL('','') Remark2,NVL('','') Remark3 from pmdl_t" &
                " left join pmdo_t on pmdldocno=pmdodocno " &
                " left join pmdb_t on pmdb004=pmdo001 " &
                " left join pmda_t on pmdadocno=pmdbdocno and pmdadocno=pmdl008 " &
                " where pmdlent='3' and pmdoent='3' and pmdbent='3' and pmdaent='3' " &
                " " & WHR
        dt = clsDBConnect.QueryDataTable(SQL, clsDBConnect.T100)
        clsDBConnect.Close(clsDBConnect.T100)

        If dt.Rows.Count = 0 Then
            tbPrNo.Focus()
            lbPoTypeNo.Text = ""
            tbRequire1.Text = ""
            tbRequire2.Text = ""
            tbRequire3.Text = ""
            Exit Sub
        Else
            With dt.Rows(0)
                lbPoTypeNo.Text = Trim(.Item(0))
                'tbRequire1.Text = Trim(.Item(1))
                'tbRequire2.Text = Trim(.Item(2))
                'tbRequire3.Text = Trim(.Item(3)
            End With
        End If
    End Sub

    Protected Sub btUpdate_Click(sender As Object, e As EventArgs) Handles btUpdate.Click
        Dim fld As Hashtable = New Hashtable,
            whr As Hashtable = New Hashtable

        'log
        fld.Add("prNo", ddlType.SelectedValue.Trim & "-" & tbPrNo.Text.Trim & If(tbPrSeq.Text.Trim = "", "", "-" & tbPrSeq.Text.Trim))
        fld.Add("poNo", lbPoTypeNo.Text.Trim)
        fld.Add("require1", tbRequire1.Text.Trim)
        fld.Add("require2", tbRequire2.Text.Trim)
        fld.Add("require3", tbRequire3.Text.Trim)
        fld.Add("CreateBy", Session("UserName"))
        fld.Add("CreateDate", DateTime.Now.ToString("yyyyMMdd HHmmss"))
        clsDBConnect.QueryExecuteScalar(Conn_SQL.GetSQL(tableLog, fld, whr, "I"), clsDBConnect.MIS2)
        clsDBConnect.Close(clsDBConnect.MIS2)

        'update to po
        'fld = New Hashtable
        'whr = New Hashtable
        'Dim tablePO As String,
        '    poType As String = lbPoTypeNo.Text.Trim
        'whr.Add("pmdldocno", "")
        'fld.Add("", tbRequire1.Text.Trim)
        'fld.Add("", tbRequire2.Text.Trim)
        'fld.Add("", tbRequire3.Text.Trim)

        'If ddlPrLoc.Text.Trim = "1" Then
        '    tablePO = "ASTTM"
        '    whr.Add("TM001", poType)
        '    whr.Add("TM002", poNo)
        'Else
        '    tablePO = "PURTC"
        '    whr.Add("TC001", poType)
        '    whr.Add("TC002", poNo)
        'End If
        'Conn_SQL.Exec_Sql(Conn_SQL.GetSQL(tablePO, fld, whr, "U"), Conn_SQL.ERP_ConnectionString)
        'changeLoc()
    End Sub
End Class

