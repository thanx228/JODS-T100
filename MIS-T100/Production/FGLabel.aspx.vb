Imports System.Data.OracleClient

Public Class FGLabel
    Inherits System.Web.UI.Page
    Dim ConnT100 As New clsDBConnect
    Dim ControlForm As New ControlDataForm
    Dim Conn_SQL As New ConnSQL
    Dim configDate As New ConfigDate
    Dim CreateTable As New CreateTable
    Dim VarIni As New VarIni
    Dim tableLable As String = "FGLabel"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("UserName") = "" Then
                'Response.Redirect(Server.UrlPathEncode("../LoginT100.aspx"))
            End If
            CreateTable.CreateFGLabel()
            ucHeader.HeaderLable = ControlForm.nameHeader(Request.CurrentExecutionFilePath.ToString)
            Dim SQL As String = ""
            MultiView1.SetActiveView(View1)
            TabContainer1.ActiveTabIndex = 0
            ddlProcessMO_SelectedIndexChanged(sender, e)

        End If
    End Sub

    Function getDataFromLable(transNumber As String, Optional transSeq As String = "0") As DataTable
        Dim SQL As String = "select DocNo,qty,qtyCtn,CtnNo,CtnSpec,CtnWgh,PackBy,custPO,serailNo from " & tableLable & " where tranNo='" & transNumber & "' and tranSeq='" & transSeq & "' "
        Dim dt As DataTable = Conn_SQL.Get_DataReader(SQL, Conn_SQL.MIS_ConnectionString)
        Return dt
    End Function

    Protected Sub btSearch_Click(sender As Object, e As EventArgs) Handles btSearch.Click
        Dim SQL As String = "",
            WHR As String

        If TabContainer1.ActiveTabIndex = 0 Then ' MO
            Dim valType As String = ddlProcessMO.Text.Trim
            Dim fldDocType As String = ""
            Dim listFld As New Hashtable
            'Dim fld As String = ""
            Dim dtShow As DataTable
            Dim dt As DataTable
            Dim DocNo As String = ""
            Dim qty As Decimal = 0
            Dim qtyCtn As Decimal = 0
            Dim CtnNo As String = ""
            Dim CtnSpec As String = ""
            Dim CtnWgh As Decimal = 0
            Dim PackBy As String = ""
            Dim custPO As String = ""
            Dim serailNo As String = ""
            If valType = "asft335" Then 'transfer
                WHR = VarIni.getWhrFirst(VarIni.SFFB)
                WHR &= Conn_SQL.Where(VarIni.getFldDoc(SFFB.DocNo), ddlMoRType)
                WHR &= Conn_SQL.Where(VarIni.getFldDoc(SFFB.DocNo, True), tbMoRNo)
                WHR &= Conn_SQL.Where(SFFB.Workstation, ucWC.getObject)
                WHR &= " and " & SFFB.Status & " in ('" & ddlStatus.Text.Trim.Replace(",", "','") & "')"
                WHR &= Conn_SQL.Where(VarIni.getFldDoc(SFFB.WONo), ddlMoType.getObject)
                WHR &= Conn_SQL.Where(VarIni.getFldDoc(SFFB.WONo, True), tbMoNo)
                'WHR &= Conn_SQL.Where("SFCTA.TA003", tbMOSeq)
                'WHR &= Conn_SQL.Where("COPTC.TC004", tbCustCode)
                WHR &= Conn_SQL.Where(SFAA.ProductItem, tbItem)
                WHR &= Conn_SQL.Where(IMAAL.Specifaction, tbSpec)
                WHR &= Conn_SQL.Where("SUBSTR(" & SFFB.DocNo & ",3,2)", "D2",, False)
                WHR &= Conn_SQL.Where("to_char(" & SFFB.DocumentDate & ",'yyyymmdd')", ucDate.dateVal, ucDate.dateVal)
                Dim colName As New ArrayList
                Dim fldName As New ArrayList
                'field list
                'A docno for print
                colName.Add(":A")

                'B transfer number-seq
                fldName.Add(SFFB.DocNo)
                colName.Add(":B")

                'C mo type number seq 
                fldName.Add(SFFB.WONo & "||'-'||" & SFCB.LineNo & ":" & SFFB.WONo)
                colName.Add(":C")

                'D so type number seq 
                fldName.Add(SFAA.OldRefereanceDocNo)
                colName.Add(":D")

                'E Cust PO
                'fldName.Add(custPO)
                colName.Add(":E")

                'F Item
                fldName.Add(SFFB.WorkReportItemNo)
                colName.Add(":F")

                'G  item desc
                fldName.Add(IMAAL.ProductName)
                colName.Add(":G")

                'H item Spec
                fldName.Add(IMAAL.Specifaction)
                colName.Add(":H")

                'I mo qty
                fldName.Add(SFAA.ProductionQty)
                colName.Add(":I:0")

                'J Complete qty
                fldName.Add(SFAA.StoredPassQuantity)
                colName.Add(":J")

                'K scrap qty
                fldName.Add(SFAA.ScarpQty)
                colName.Add(":K")

                'L customer code-name
                'U Customer
                fldName.Add(PMAAL.ContactName)
                colName.Add(":L")

                'M MO Status
                fldName.Add(SFAA.Status)
                colName.Add(":M")

                'N wgh per pcs
                fldName.Add(IMAA.NetWeight)
                colName.Add(":N:3")

                'O transfer qty
                fldName.Add(SFFB.NoOfGoodItem)
                colName.Add(":O:2")

                'P qty per box
                fldName.Add(" NVL(" & IMAA.VolumeUnit & ",0):" & IMAA.VolumeUnit)
                colName.Add(":P:0")

                'Q weight of box
                fldName.Add(" NVL(" & IMAA.GrossWeight & ",0):" & IMAA.GrossWeight)
                colName.Add(":Q:3")

                'R box code
                colName.Add(":R")

                'S box Name
                colName.Add(":S")

                'T Batch Number
                fldName.Add(SFAA.EstimatedStorgeBacthNo)
                colName.Add(":T")

                'Z pack by
                colName.Add(":Z") 'pack by

                'SERAIL 
                colName.Add(":SERAIL")


                dt = SFFB.getTransferData_Process_MO_Item(fldName, WHR)
                dtShow = ControlForm.setColDatatable(colName)

                For i As Integer = 0 To dt.Rows.Count - 1
                    With dt.Rows(i)
                        Dim dataHash As New Hashtable
                        Dim transferNumber As String = .Item(SFFB.DocNo)

                        DocNo = ""
                        qty = .Item(SFFB.NoOfGoodItem)
                        qtyCtn = Conn_SQL.checkNumberic(.Item(IMAA.VolumeUnit))
                        CtnNo = ""
                        CtnSpec = ""
                        CtnWgh = .Item(IMAA.GrossWeight)
                        PackBy = ""
                        custPO = ""
                        serailNo = ""

                        Dim sdt As DataTable = getDataFromLable(transferNumber)
                        If sdt.Rows.Count > 0 Then
                            With sdt.Rows(0)
                                'DocNo,qty,qtyCtn,CtnNo,CtnSpec,CtnWgh,PackBy,custPO,serailNo
                                DocNo = Trim(.Item("DocNo"))
                                qty = .Item("qty")
                                qtyCtn = .Item("qtyCtn")
                                CtnNo = .Item("CtnNo")
                                CtnSpec = .Item("CtnSpec")
                                CtnWgh = .Item("CtnWgh")
                                custPO = .Item("custPO")
                                PackBy = .Item("PackBy")
                                serailNo = .Item("serailNo")
                            End With
                        End If

                        'doc no
                        'get docno from fglable
                        dataHash.Add("A", DocNo)

                        'B transfer number-seq
                        dataHash.Add("B", .Item(SFFB.DocNo))

                        'C mo type number seq 
                        dataHash.Add("C", .Item(SFFB.WONo))

                        'D so type number seq 
                        dataHash.Add("D", .Item(SFAA.OldRefereanceDocNo))

                        'E Cust PO
                        dataHash.Add("E", .Item(SFAA.OldRefereanceDocNo))

                        'F Item
                        dataHash.Add("F", .Item(SFFB.WorkReportItemNo))

                        'G  item desc
                        dataHash.Add("G", .Item(IMAAL.ProductName))

                        'H item Spec
                        dataHash.Add("H", .Item(IMAAL.Specifaction))

                        'I mo qty
                        dataHash.Add("I", .Item(SFAA.ProductionQty))

                        'J Complete qty
                        dataHash.Add("J", .Item(SFAA.StoredPassQuantity))

                        'K scrap qty
                        dataHash.Add("K", .Item(SFAA.ScarpQty))

                        'L customer code-name
                        dataHash.Add("L", .Item(PMAAL.ContactName))

                        'M MO Status
                        dataHash.Add("M", .Item(SFAA.Status))

                        'N wgh per pcs
                        dataHash.Add("N", .Item(IMAA.NetWeight))

                        'O transfer qty
                        dataHash.Add("O", qty)

                        'P qty per box
                        dataHash.Add("P", qtyCtn)

                        'Q weight of box
                        dataHash.Add("Q", CtnWgh)

                        'R box code
                        dataHash.Add("R", CtnNo)

                        'S box Name
                        dataHash.Add("S", CtnSpec)

                        'T cust po
                        dataHash.Add("T", .Item(SFAA.EstimatedStorgeBacthNo))

                        'Z pack by
                        dataHash.Add("Z", PackBy)

                        'SERAIL
                        dataHash.Add("SERAIL", serailNo)

                        ControlForm.addDataRow(dtShow, dataHash)

                    End With
                Next
            Else 'receipt
                WHR = VarIni.getWhrFirst(VarIni.SFEB)
                WHR &= Conn_SQL.Where(VarIni.getFldDoc(SFEB.DocNo), ddlMoRType)
                WHR &= Conn_SQL.Where(VarIni.getFldDoc(SFEB.DocNo, True), tbMoRNo)
                'WHR &= Conn_SQL.Where(SFCB.WorkStation, ucWC.getObject)
                WHR &= " and " & SFEA.Status & " in ('" & ddlStatus.Text.Trim.Replace(",", "','") & "')"
                WHR &= Conn_SQL.Where(VarIni.getFldDoc(SFEB.WONo), ddlMoType.getObject)
                WHR &= Conn_SQL.Where(VarIni.getFldDoc(SFEB.WONo, True), tbMoNo)
                'WHR &= Conn_SQL.Where("SFCTA.TA003", tbMOSeq)
                'WHR &= Conn_SQL.Where("COPTC.TC004", tbCustCode)
                WHR &= Conn_SQL.Where(SFAA.ProductItem, tbItem)
                WHR &= Conn_SQL.Where(IMAAL.Specifaction, tbSpec)
                WHR &= Conn_SQL.Where("SUBSTR(" & SFEA.DocNo & ",3,2)", "D3",, False)
                WHR &= Conn_SQL.Where(SFEA.DocumentDate, ucDate.dateVal, ucDate.dateVal, True)

                Dim colName As New ArrayList
                Dim fldName As New ArrayList
                'field list
                'A docno for print
                colName.Add(":A")

                'B transfer number-seq
                fldName.Add(SFEB.DocNo)
                fldName.Add(SFEB.LineNo)
                fldName.Add(SFEB.DocNo & "||'-'||" & SFEB.LineNo & ":" & SFEB.DocNo & SFEB.LineNo)
                colName.Add(":B")

                'C mo (type-number-runcard) 
                fldName.Add(SFEB.WONo & "||'-'||" & SFEB.Runcard & ":" & SFEB.WONo)
                colName.Add(":C")

                'D so type number seq 
                fldName.Add(SFAA.OldRefereanceDocNo)
                colName.Add(":D")

                'E Cust PO
                'fldName.Add(SFAA.OldRefereanceDocNo)
                colName.Add(":E")

                'F Item
                fldName.Add(SFEB.ItemNo)
                colName.Add(":F")

                'G  item desc
                fldName.Add(IMAAL.ProductName)
                colName.Add(":G")

                'H item Spec
                fldName.Add(IMAAL.Specifaction)
                colName.Add(":H")

                'I mo qty
                fldName.Add(SFAA.ProductionQty)
                colName.Add(":I:0")

                'J Complete qty
                fldName.Add(SFAA.StoredPassQuantity)
                colName.Add(":J")

                'K scrap qty
                fldName.Add(SFAA.ScarpQty)
                colName.Add(":K")

                'L customer code-name
                'fldName.Add(SFAA.ReferanceCustomer)
                fldName.Add(PMAAL.ContactName)
                colName.Add(":L")

                'M MO Status
                fldName.Add(SFAA.Status)
                colName.Add(":M")

                'N wgh per pcs
                fldName.Add(IMAA.NetWeight)
                colName.Add(":N:3")

                'O transfer qty
                fldName.Add(SFEB.ActualQty)
                colName.Add(":O:2")

                'P qty per box
                'fldName.Add(IMAA.VolumeUnit)
                fldName.Add(" NVL(" & IMAA.VolumeUnit & ",0):" & IMAA.VolumeUnit)
                colName.Add(":P:0")

                'Q weight of box
                fldName.Add(" NVL(" & IMAA.GrossWeight & ",0):" & IMAA.GrossWeight)
                colName.Add(":Q:3")

                'R box code
                colName.Add(":R")

                'S box Name
                colName.Add(":S")

                'T Batche Number
                fldName.Add(SFAA.EstimatedStorgeBacthNo)
                colName.Add(":T")

                'Z pack by
                colName.Add(":Z") 'pack by

                'SERAIL 
                colName.Add(":SERAIL")

                dt = SFEB.getMoReceiptData_Process_MO_Item(fldName, WHR)
                dtShow = ControlForm.setColDatatable(colName)

                For i As Integer = 0 To dt.Rows.Count - 1
                    With dt.Rows(i)
                        Dim dataHash As New Hashtable
                        Dim moReceipNumber As String = .Item(SFEB.DocNo)
                        Dim moReceiptSeq As String = .Item(SFEB.LineNo)

                        Dim aa As String = .Item(IMAA.VolumeUnit)

                        DocNo = ""
                        qty = .Item(SFEB.ActualQty)
                        qtyCtn = Conn_SQL.checkNumberic(aa)
                        CtnNo = ""
                        CtnSpec = ""
                        CtnWgh = .Item(IMAA.GrossWeight)
                        PackBy = ""
                        custPO = ""
                        serailNo = ""

                        Dim sdt As DataTable = getDataFromLable(moReceipNumber, moReceiptSeq)
                        If sdt.Rows.Count > 0 Then
                            With sdt.Rows(0)
                                'DocNo,qty,qtyCtn,CtnNo,CtnSpec,CtnWgh,PackBy,custPO,serailNo
                                DocNo = Trim(.Item("DocNo"))
                                qty = .Item("qty")
                                qtyCtn = .Item("qtyCtn")
                                CtnNo = .Item("CtnNo")
                                CtnSpec = .Item("CtnSpec")
                                CtnWgh = .Item("CtnWgh")
                                custPO = .Item("custPO")
                                PackBy = .Item("PackBy")
                                serailNo = .Item("serailNo")
                            End With
                        End If

                        'doc no
                        dataHash.Add("A", DocNo)

                        'B transfer number-seq
                        dataHash.Add("B", .Item(SFEB.DocNo & SFEB.LineNo))

                        'C mo type number seq 
                        dataHash.Add("C", .Item(SFEB.WONo))

                        'D so type number seq 
                        dataHash.Add("D", .Item(SFAA.OldRefereanceDocNo))

                        'E Cust PO
                        dataHash.Add("E", custPO)

                        'F Item
                        dataHash.Add("F", .Item(SFEB.ItemNo))

                        'G  item desc
                        dataHash.Add("G", .Item(IMAAL.ProductName))

                        'H item Spec
                        dataHash.Add("H", .Item(IMAAL.Specifaction))

                        'I mo qty
                        dataHash.Add("I", .Item(SFAA.ProductionQty))

                        'J Complete qty
                        dataHash.Add("J", .Item(SFAA.StoredPassQuantity))

                        'K scrap qty
                        dataHash.Add("K", .Item(SFAA.ScarpQty))

                        'L customer code-name
                        dataHash.Add("L", .Item(PMAAL.ContactName))

                        'M MO Status
                        dataHash.Add("M", .Item(SFAA.Status))

                        'N wgh per pcs
                        dataHash.Add("N", .Item(IMAA.NetWeight))

                        'O transfer qty
                        dataHash.Add("O", qty)

                        'P qty per box
                        dataHash.Add("P", qtyCtn)

                        'Q weight of box
                        dataHash.Add("Q", CtnWgh)

                        'R box code
                        dataHash.Add("R", CtnNo)

                        'S box Name
                        dataHash.Add("S", CtnSpec)

                        'T Batch Number
                        dataHash.Add("T", .Item(SFAA.EstimatedStorgeBacthNo))

                        'Z pack by
                        dataHash.Add("Z", PackBy)

                        'SERAIL
                        dataHash.Add("SERAIL", serailNo)

                        ControlForm.addDataRow(dtShow, dataHash)

                    End With
                Next

            End If
            ControlForm.ShowGridView(gvShowNew, dtShow)
            ucRowCount.RowCount = ControlForm.rowGridview(gvShowNew)
            gvShowNew.Visible = True
            gvShowDel.Visible = False
            gvShowDel.DataSource = ""
            gvShowDel.DataBind()

        Else 'sale delivery
            'WHR &= Conn_SQL.Where("TH001", ddlMoRType)
            'WHR &= Conn_SQL.Where("TH002", tbMoRNo)
            'WHR &= Conn_SQL.Where("TH003", tbMoRSeq)
            'WHR &= Conn_SQL.Where("TG004", tbCustCode)
            'WHR &= " and TG023 in ('" & ddlStatus.Text.Trim.Replace(",", "','") & "')"
            'WHR &= Conn_SQL.Where("TH004", tbItem)
            'WHR &= Conn_SQL.Where("TH019", tbSpec)
            'WHR &= Conn_SQL.Where("TG042", ucDate.dateVal, ucDate.dateVal)

            'SQL = " select isnull(F.DocNo,'') DocNo,TH001+'-'+TH002+'-'+TH003 TH00123,TH014+'-'+TH015+'-'+TH016 TH01456,TH030,TH004,TH005,COPMG.MG006 TH019,MB014, " &
            '      " case when isnull(F.DocNo,'') <> '' then F.qtyCtn else INVMB.UDF51 end qtyCtn," &
            '      " case when isnull(F.DocNo,'') <> '' then F.CtnWgh else INVMB.MB075 end CtnWgh, " &
            '      " case when isnull(F.DocNo,'') <> '' then F.CtnNo  else INVMB.UDF01 end CtnNo, " &
            '      " case when isnull(F.DocNo,'') <> '' then F.CtnSpec else INVMB.UDF02 end CtnSpec," &
            '      " replace(TH017,'*','') TH017 ,isnull(F.PackBy,'') PackBy,isnull(serialNo,'') SERAIL ,Rtrim(TG004)+'-'+MA002 TG004 ,TH008" &
            '      " from COPTH left join COPTG on TG001=TH001 and TG002=TH002 " &
            '      " Left join COPTD On TD001 = TH014 And TD002 = TH015 And TD003 = TH016 " &
            '      " left join COPTC On TC001 = TH014 And TC002 = TH015 " &
            '      " left join COPMA On MA001 = TG004" &
            '      " left join COPMG On COPMG.MG001 = COPTG.TG004 and COPMG.MG002 = COPTH.TH004 and COPMG.MG003 = COPTH.TH019 " &
            '      " left join " & Conn_SQL.DBReport & "..FGLabel F On F.moType = TH001 And F.moNo = TH002 And F.moSeq = TH003 " &
            '      " left join INVMB On MB001 = TH004 where 1=1 " & WHR &
            '      " order by TH001,TH002,TH003 "

            'ControlForm.ShowGridView(gvShowDel, SQL, Conn_SQL.ERP_ConnectionString)
            'ucRowCount.RowCount = ControlForm.rowGridview(gvShowDel)
            'gvShowDel.Visible = False
            'gvShow.Visible = False
            'gvShow.DataSource = ""
            'gvShow.DataBind()
        End If
        ucRowCount.RowCount = ControlForm.rowGridview(gvShowNew)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollShow", "gridviewScrollShow();", True)
        System.Threading.Thread.Sleep(1000)
    End Sub

    Protected Sub ddlProcessMO_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlProcessMO.SelectedIndexChanged
        Dim SQL As String = "select MQ001,MQ001+' : '+MQ002 as MQ002 from CMSMQ where MQ003 in ('" & ddlProcessMO.Text.Trim & "') order by MQ002"
        SQL = "select oobx001,oobx001 ||'-'|| oobxl003 oobxl003 from " & OOBX.TblAllType &
              VarIni.getLeftjoinFirst(VarIni.OOBXL, VarIni.OOBX, False, OOBXL.DocTypeId & ":" & OOBX.DocTypeNo & "," & OOBXL.Language & ":" & VarIni.enUS_V & ":") &
              VarIni.getWhrFirst(VarIni.OOBX, False) & Conn_SQL.Where(OOBX.DocTypePage, ddlProcessMO) &
              " order by " & OOBX.DocTypeNo
        Dim dt As DataTable = GetData.GetDataReaderOracle(SQL, GetData.WhoCalledMe)

        ControlForm.showDDL(ddlMoRType, dt, "oobxl003", "oobx001", True)

        Dim txt As String = IIf(ddlProcessMO.Text.Trim = "asft335", "Transfer", "MO Receipt")
        lbDocType.Text = txt & " No"
        lbDocNo.Text = txt & " Type"
        lbDocSeq.Text = txt & " Seq"
        lbDate.Text = txt & " Date"
    End Sub

    Sub calCarton()
        'Dim qtyCnt As Decimal = CDec(tbQtyCtn.Text.Trim)
        Dim qtyCnt As Decimal = Conn_SQL.checkNumberic(tbQtyCtn)
        Dim qty As Decimal = Conn_SQL.checkNumberic(lbQty.Text.Trim)

        'Dim qty = CDec(lbQty.Text.Trim)
        Dim full As Integer = 0,
            notfull As Integer = qty

        If qtyCnt > 0 Then
            full = Math.Floor(qty / qtyCnt)
            notfull = qty Mod qtyCnt
        End If

        lbFull.Text = full
        lbNotFull.Text = notfull
        Dim itemWgh As Decimal = Conn_SQL.checkNumberic(lbItemWgh.Text.Trim) 'CDec(lbItemWgh.Text.Trim)
        Dim fullNest As Decimal = qtyCnt * itemWgh
        Dim notFullNest As Decimal = notfull * itemWgh
        'Dim fullGross As Decimal = fullNest + CDec(tbCtnWgh.Text)
        Dim notFullGross As Decimal = 0
        lbFullN.Text = fullNest
        lbNotFullN.Text = notFullNest

        Dim ctnWgh As Decimal = Conn_SQL.checkNumberic(tbCtnWgh)

        lbFullG.Text = fullNest + ctnWgh
        If notfull > 0 Then
            notFullGross = notFullNest + ctnWgh
        End If
        lbNotFullG.Text = notFullGross

    End Sub

    Protected Sub btCal1_Click(sender As Object, e As EventArgs) Handles btCal1.Click
        calCarton()
    End Sub

    Private Sub gvShowNew_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvShowNew.RowCommand
        If e.CommandName = "OnView2" Then
            Dim i As Integer = e.CommandArgument
            With gvShowNew.Rows(i)

                lbRef.Text = IIf(ddlProcessMO.Text.Trim = "D2", "Trasnsfer", "MO Receipt") & " Type-No-Seq"

                MultiView1.SetActiveView(View2)
                lbDateRec.Text = .Cells(1).Text.Replace(" ", "").Replace("&nbsp;", "")
                lbMoR.Text = .Cells(2).Text.Replace(" ", "")
                lbMo.Text = .Cells(3).Text.Replace(" ", "")
                lbSo.Text = .Cells(4).Text.Replace(" ", "")
                Dim custPO As String = .Cells(5).Text.Replace(" ", "").Replace("&nbsp;", "")
                lbPO.Text = custPO
                tbPO.Text = custPO

                lbQty.Text = .Cells(15).Text.Replace(" ", "").Replace("&nbsp;", "0")
                tbQtyCtn.Text = .Cells(16).Text.Replace(" ", "").Replace("&nbsp;", "0")
                tbCtnNo.Text = .Cells(18).Text.Replace("&nbsp;", "")
                lbBatch.Text = .Cells(20).Text.Replace(" ", "")

                lbItem.Text = .Cells(6).Text.Replace(" ", "")
                lbDesc.Text = .Cells(7).Text.Replace("&nbsp;", "")
                lbSpec.Text = .Cells(8).Text.Replace("&nbsp;", "")
                lbItemWgh.Text = .Cells(14).Text.Replace(" ", "").Replace("&nbsp;", "0")
                tbSerialNo.Text = .Cells(22).Text.Replace("&nbsp;", "")
                'lbItemWgh.Text = 0.352
                tbCtnWgh.Text = .Cells(17).Text.Replace(" ", "").Replace("&nbsp;", "0")
                tbPack.Text = .Cells(21).Text.Replace("&nbsp;", "")
                lbCtnSpec.Text = .Cells(19).Text.Replace(" ", "").Replace("&nbsp;", "")

                calCarton()
                btCancel.Visible = True

                'showElement()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "gridviewScrollShow", "gridviewScrollShow();", True)
            End With
        End If
    End Sub

    Protected Sub btCancel_Click(sender As Object, e As EventArgs) Handles btCancel.Click
        Clear()
        If MultiView1.ActiveViewIndex <> 0 Then
            MultiView1.SetActiveView(View1)
        End If
    End Sub

    Private Sub Clear()
        lbBatch.Text = ""
        ucRowCount.RowCount = 0
        'lbCount.Text = ""
        lbDateRec.Text = ""
        lbDesc.Text = ""
        lbFull.Text = ""
        lbFullG.Text = ""
        lbFullN.Text = ""
        lbItem.Text = ""
        lbItemWgh.Text = ""
        lbMo.Text = ""
        lbMoR.Text = ""
        lbNotFull.Text = ""
        lbNotFullG.Text = ""
        lbNotFullN.Text = ""
        lbQty.Text = ""
        lbSo.Text = ""
        lbSpec.Text = ""

        tbCtnNo.Text = ""
        lbCtnSpec.Text = ""
        tbCtnWgh.Text = ""
        tbQtyCtn.Text = ""
        lbError.Text = ""
    End Sub

    Sub print()
        'Dim SQL As String = " select MB022 from INVMB where MB001='" & lbItem.Text.Trim.Replace("-", "") & "'"

        Dim fldName As New ArrayList
        Dim WHR As String = ""
        fldName.Add(IMAA.ProductClassification)
        WHR = VarIni.getWhrFirst(VarIni.IMAA, False)
        WHR &= Conn_SQL.Where(IMAA.ItemNo, lbItem.Text.Trim.Replace("-", ""), , False)
        Dim dt As DataTable = IMAA.getItemInfo(fldName, WHR)
        Dim codeControl As String = ""
        If dt.Rows.Count > 0 Then
            codeControl = Trim(dt.Rows(0).Item(IMAA.ProductClassification))
        End If
        Dim print As Boolean = True

        If print Then
            lbError.Text = ""
            ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('../PDF/labelFGReceipt.aspx?docno=" & lbDateRec.Text.Trim & "&prtfor=" & codeControl & "&tindex=" & TabContainer1.ActiveTabIndex.ToString & "');", True) '0=mo/transfer,1=sale delivery
        Else
            lbError.Text = "can't print cause mat issue less assembly record please check.!!!"
        End If
    End Sub

    Protected Sub btSave_Click(sender As Object, e As EventArgs) Handles btSave.Click
        SQLPrintSave()
        btSearch_Click(sender, e)
    End Sub

    Private Sub SQLPrintSave()
        Dim MO As String = lbMoR.Text.Trim,
            Qty As String = lbQty.Text.Replace(",", ""),
            QtyCtn As String = tbQtyCtn.Text.Trim.Replace(",", ""),
            CtnNo As String = tbCtnNo.Text.Trim,
            CtnSpec As String = "",
            CtnWgh As String = tbCtnWgh.Text.Trim,
            CrBy As String = Session("UserName"),
            CrDate As String = Date.Today.ToString("yyyyMMdd hhmmss"),
            PackBy As String = tbPack.Text.Trim,
            serialNo As String = tbSerialNo.Text
        Dim custPO As String = tbPO.Text

        Dim tranNumber As String = MO.Substring(0, 18),
            tranSeq As String = "0"
        If ddlProcessMO.Text.Trim = "asft340" Then
            tranSeq = MO.Substring(19, MO.Length - 19)
        End If

        'CtnSpec = Conn_SQL.Get_value("Select MB003 from INVMB where MB001='" & CtnNo & "' ", Conn_SQL.ERP_ConnectionString)

        Dim newDoc As Boolean = False
        If lbDateRec.Text.Trim = "" Then
            lbDateRec.Text = getDocNo()
            newDoc = True
        End If
        Dim DocNo As String = lbDateRec.Text.Trim

        If lbMo.Text <> "" Then
            Dim fld As Hashtable = New Hashtable
            Dim whr As Hashtable = New Hashtable

            whr.Add("DocNo", DocNo)
            whr.Add("tranNo", tranNumber)
            whr.Add("tranSeq", tranSeq)

            fld.Add("qty", Qty)
            fld.Add("qtyCtn", QtyCtn)
            fld.Add("CtnNo", CtnNo)
            fld.Add("CtnSpec", CtnSpec)
            fld.Add("CtnWgh", CtnWgh)
            fld.Add("PackBy", PackBy)
            fld.Add("custPO", custPO)
            fld.Add("serailNo", serialNo)
            fld.Add(If(newDoc, "CreateBy", "ChangeBy"), CrBy)
            fld.Add(If(newDoc, "CreateDate", "ChangeDate"), CrDate)
            Dim SQL As String = Conn_SQL.GetSQL("FGLabel", fld, whr, If(newDoc, "I", "U"))
            Conn_SQL.Exec_Sql(SQL, Conn_SQL.MIS_ConnectionString)
        End If
        print()
        showElement()
        Clear()
        'ScriptManager.RegisterStartupScript(Page, Me.GetType(), "OpenWindow", "window.open('../ShowCrystalReport.aspx?dbName=ERP&ReportName=FGLabel.rpt&encode=1');", True)
    End Sub

    Private Function getDocNo() As String
        Dim DocNo As String = ""
        Dim DateDoc As String = Date.Today.ToString("yyyyMMdd")
        'Dim ymd As String = configDate.dateFormat2(tbDate.Text.Trim)
        Dim SQL As String = "select substring(DocNo,1,8),max(DocNo) as DocNo  from FGLabel where DocNo like '" & DateDoc & "%' group by substring(DocNo,1,8)"
        Dim Program As New Data.DataTable
        Program = Conn_SQL.Get_DataReader(SQL, Conn_SQL.MIS_ConnectionString)
        If Program.Rows.Count > 0 Then
            DocNo = CDec(Program.Rows(0).Item("DocNo")) + 1
        Else
            DocNo = DateDoc & "001"
        End If
        Return DocNo
    End Function

    Sub showElement()
        Dim lotCon As String = Conn_SQL.Get_value("select MB022 from INVMB where MB001='" & lbItem.Text.Trim.Replace("-", "") & "' ", Conn_SQL.ERP_ConnectionString)

        Dim showLable As Boolean = False
        If lotCon = "N" Or lotCon = Nothing Or lotCon = "" Then
            showLable = True
        End If
        lbPO.Visible = showLable
        tbPO.Visible = Not showLable
        tbSerialNo.Enabled = Not showLable
        btPrint.Visible = True
        If lbDateRec.Text.Trim = "" Then
            btPrint.Visible = False
        End If
    End Sub

    Protected Sub btPrint_Click(sender As Object, e As EventArgs) Handles btPrint.Click
        print()
    End Sub
End Class