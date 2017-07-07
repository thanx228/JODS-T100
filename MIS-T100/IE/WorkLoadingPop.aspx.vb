Public Class WorkLoadingPop
    Inherits System.Web.UI.Page

    '---------------- Production Module - Work Center Loading (Popup) ---------------
    '                          Original Code Module - JODS
    '                     Version 1 by Pattavee Narumonchavalit
    '--------------------------------------------------------------------------------

    'This Program no need any temptable to restore data
    'Declare Class

    'Dim XMDA As New XMDC      'Sales Line , Can Refer to  Production Item , Specification & Qty

    Dim clsConnect As New clsDBConnect    'DBconnection Engine
    Dim SFCB As New SFCB                  'MO Operation Line
    Dim SFCA As New SFCA                  'MO Operation Header
    Dim SFAA As New SFAA                  'Maintain Header
    Dim OOCQL As New OOCQL                'Operation Header
    Dim IMAAL As New IMAAL                'Item Master

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Response.Write("WC Value is " & wc & " AND fromdate is " & fdate & " AND todate is " & tdate & " ")
        Dim wc As String = ""
        Dim fdate As String = Session("sfdate")
        Dim tdate As String = Session("stdate")
        wc = Request.QueryString("wc")
        Dim dss As DataSet
        Dim dsdt As DataSet

        dss = getOperationsummary(fdate, tdate, wc)
        dsdt = getOperationdetail(fdate, tdate, wc)
        gvOperSum.DataSource = dss
        gvOperSum.DataBind()
        gvOperDetail.DataSource = dsdt
        gvOperDetail.DataBind()

    End Sub

    Private Function getOperationsummary(ByVal fdate As String, ByVal tdate As String, ByVal wc As String) As DataSet
        Dim ds As DataSet
        Dim row As Integer = 0
        Dim sql As String = "SELECT " & SFCB.OperationID & " AS Operation_ID," & OOCQL.Operation & " AS Operation_Name ,count(" & SFCB.WONo & ") AS Total_MO, TO_CHAR(max(" & SFCB.PlannedCompletionDate & "),'dd/MM/yyyy') AS Load_End_Date " &
                            "FROM " & SFCB.tblMOprocessItem_SFCB & " " &
                            "LEFT JOIN " & SFAA.tblMO & " ON " & SFCB.WONo & "= " & SFAA.DocNo & " " &
                            "LEFT JOIN " & SFCA.tblMO_Detail & " ON " & SFCB.WONo & "=" & SFCA.DocNo & " " &
                            "Left JOIN " & ECAA.tblWorkcenter & " ON " & SFCB.WorkStation & "=" & ECAA.WorkcenterID & " " &
                            "LEFT JOIN " & OOCQL.tblOperation & " ON " & SFCB.OperationID & "=" & OOCQL.OperationID & " " &
                            "LEFT JOIN " & IMAAL.tblProductionDetail & " ON " & SFAA.ProductItem & "=" & IMAAL.ProductItem & " " &
                            "WHERE " & SFCA.ent & " ='3' AND " & IMAAL.ent & "='3' AND " & OOCQL.ent & "='3' AND " & IMAAL.ent & "='3' AND " & ECAA.ent & "='3' AND " & OOCQL.IssueSite & "='221' " &
                            "AND " & OOCQL.Language & "='en_US' AND " & SFCB.PlanStartDate & " BETWEEN TO_DATE('" & fdate & "','dd/MM/yyyy') AND TO_DATE('" & tdate & "','dd/MM/yyyy') AND " & SFCB.WorkStation & "='" & wc & "' " &
                            "GROUP BY " & SFCB.OperationID & "," & OOCQL.Operation & ""

        ds = clsConnect.QueryDataSet(sql, clsConnect.T100)
        Return ds
    End Function

    Private Function getOperationdetail(ByVal fdate As String, ByVal tdate As String, ByVal wc As String) As DataSet
        Dim ds As DataSet
        Dim row As Integer = 0
        Dim sql As String = "SELECT " & ECAA.Workcenter & " AS WorkcenterID, " & OOCQL.Operation & " AS OperationName, " & SFCB.WONo & " as MONumber, " &
                            "" & IMAAL.Specifaction & " AS Spec, " & SFCA.ProductionQty & " AS PlanQty, " & SFCB.WIP & " as WIPQty, " &
                            "" & SFCB.GoodTransferIn & " AS InputQty, " & SFCB.GoodTransferOut & " AS FinishQty, " & SFCB.DirectScarp & " as ScrapQty, " &
                            "" & SFCB.StandradLaborHours2 & " AS Man_Time, " & SFCB.StandradMachineHours2 & " AS Machine_Time, TO_CHAR(" & SFCB.PlannedCompletionDate & ",'dd/MM/yyyy') as PlanFinishDate " &
                            "FROM " & SFCB.tblMOprocessItem_SFCB & " " &
                            "LEFT JOIN " & SFAA.tblMO & " ON " & SFCB.WONo & "= " & SFAA.DocNo & " " &
                            "LEFT JOIN " & SFCA.tblMO_Detail & " ON " & SFCB.WONo & "=" & SFCA.DocNo & " " &
                            "Left JOIN " & ECAA.tblWorkcenter & " ON " & SFCB.WorkStation & "=" & ECAA.WorkcenterID & " " &
                            "LEFT JOIN " & OOCQL.tblOperation & " ON " & SFCB.OperationID & "=" & OOCQL.OperationID & " " &
                            "LEFT JOIN " & IMAAL.tblProductionDetail & " ON " & SFAA.ProductItem & "=" & IMAAL.ProductItem & " " &
                            "WHERE " & SFCA.ent & " ='3' AND " & IMAAL.ent & "='3' AND " & OOCQL.ent & "='3' AND " & IMAAL.ent & "='3' AND " & ECAA.ent & "='3' AND " & OOCQL.IssueSite & "='221' " &
                            "AND " & OOCQL.Language & "='en_US' AND " & SFCB.PlanStartDate & " BETWEEN TO_DATE('" & fdate & "','dd/MM/yyyy') AND TO_DATE('" & tdate & "','dd/MM/yyyy') AND " & SFCB.WorkStation & "='" & wc & "' "
        ds = clsConnect.QueryDataSet(sql, clsConnect.T100)
        Return ds
    End Function

End Class