Public Class MaterialsOverIssuePop
    Inherits System.Web.UI.Page

    '------------------ Production Module - Mat Over Issues (Popup) -----------------
    '                          Original Code Module - JODS
    '                     Version 1 by Pattavee Narumonchavalit
    '--------------------------------------------------------------------------------

    'This Program no need any temptable to restore data
    'Declare Class

    Dim SFDC As New SFDC
    Dim SFBA As New SFBA
    Dim SFAA As New SFAA
    Dim IMAAL As New IMAAL
    Dim clsConnect As New clsDBConnect

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim MO As String = Request.QueryString("mo")
        Dim rmItem As String = Request.QueryString("rm")
        Dim issueNo As String = Request.QueryString("issueno")
        Dim dsmo As DataSet
        Dim dsdt As DataSet

        dsmo = getMainMOshow(MO, rmItem)
        dsdt = getMatIssueBillshow(MO, rmItem, issueNo)
        gvMoDetail.DataSource = dsmo
        gvMoDetail.DataBind()
        gvIssueDetail.DataSource = dsdt
        gvIssueDetail.DataBind()

    End Sub

    Public Function getMainMOshow(ByVal MO As String, ByVal RawMat As String) As DataSet
        Dim ds As DataSet
        Dim sql As String = "SELECT " & SFBA.MODocNo & " as MONumber, " & SFBA.IssuedQty & " as IssuedQty, (" & SFBA.IssuedQty & "-" & SFBA.StandardIssuanceQuantity & ") as OverQty," &
                            "" & SFBA.IssueItem & " as RawMatCode FROM " & SFBA.tblManufactureOrder_Body & " WHERE " &
                            "" & SFBA.ent & "='3' AND " & SFBA.MODocNo & "='" & MO & "' AND " & SFBA.IssueItem & "='" & RawMat & "'"
        ds = clsConnect.QueryDataSet(sql, clsConnect.T100)
        Return ds
    End Function

    Public Function getMatIssueBillshow(ByVal MO As String, ByVal RawMat As String, ByVal issueNo As String) As DataSet
        Dim ds As DataSet
        'Specially define for FUCK OFF!!!!! TOS, (Kuy! Gosh I'm tired to have an argument with this retard)
        Dim approver As String = "sfdacnfid"
        Dim poster As String = "sfdapstid"
        Dim sql As String = "SELECT " & SFDC.IssueDocNo & " as IssueDocument, " & approver & " as Approver, " &
                            "" & poster & " as Poster, " & SFDC.AppliciedQty & " as Issuing, " & SFDC.Unit & " as Unit " &
                            "FROM " & SFDA.tblMatIssueHead & " LEFT JOIN " & SFDC.tblMatIssueDistribution & " " &
                            "ON " & SFDA.IssueDocNo & "=" & SFDC.IssueDocNo & " WHERE " & SFDA.ent & "='3' " &
                            "AND " & SFDC.ent & "='3' AND " & SFDC.WONo & "='" & MO & "' AND " & SFDC.RequirementItem_No & "='" & RawMat & "' " &
                            "AND " & SFDC.IssueDocNo & "='" & issueNo & "'"
        ds = clsConnect.QueryDataSet(sql, clsConnect.T100)
        Return ds
    End Function


End Class