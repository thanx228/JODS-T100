Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.IO

Public Class labelFGReceipt
    Inherits System.Web.UI.Page
    Dim Conn_SQL As New ConnSQL
    Dim ConfigDate As New ConfigDate
    Dim controlPDF As New ControlPDF
    Dim tableTrace As New createTableTrace
    Dim varIni As New VarIni

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'printLabel()
        tableTrace.createLabelLog()
        Dim SQL As String,
            dt As DataTable
        Dim prtFor As String = Request.QueryString("prtFor").ToString.Trim,
            tabIndex As String = Request.QueryString("tindex").ToString.Trim,
            docName As String = "labelFGLabel" & Session("UserName") & ".pdf",
            docNo As String = Request.QueryString("docno").ToString.Trim
        Dim document As Document = New Document(New Rectangle(288.0F, 216.0F), 5.0F, 5.0F, 1.0F, 1.0F) '4*3

        'Dim document As Document
        'If prtFor = "N" Then 'general
        '    document = New Document(New Rectangle(288.0F, 216.0F), 5.0F, 5.0F, 1.0F, 1.0F) '4*3
        'Else
        '    'document = New Document(New Rectangle(288.0F, 180.0F), 5.0F, 5.0F, 1.0F, 1.0F) '4*2.5
        '    document = New Document(New Rectangle(254.0F, 169.0F), 5.0F, 5.0F, 1.0F, 1.0F) '3.54*2.36
        'End If

        'Dim document As Document = New Document(New Rectangle(288.0F, 180.0F), 5.0F, 5.0F, 1.0F, 1.0F) '4*2.5

        Using memoryStream As New System.IO.MemoryStream()
            Dim writer As PdfWriter = PdfWriter.GetInstance(document, memoryStream)
            document.Open()

            Dim packBy As String = ""
            Dim custPO As String = ""
            Dim serailNo As String = ""
            Dim qty As Decimal = 0
            Dim qtyCnt As Decimal = 0
            Dim wghCarton As Decimal = 0
            Dim tranNumber As String = ""
            Dim tranSeq As String = ""

            'Dim itemWgh As Decimal = 0

            If tabIndex = "0" Then 'mo
                SQL = "select tranNo,tranSeq,qty,qtyCtn,CtnWgh,PackBy,custPO,serailNo from FGLabel where DocNo='" & docNo & "'"
                Dim dt0 As DataTable = Conn_SQL.Get_DataReader(SQL, Conn_SQL.MIS_ConnectionString)
                If dt0.Rows.Count > 0 Then
                    With dt0.Rows(0)
                        Dim WHR As String = ""
                        Dim fldName As New ArrayList
                        tranNumber = .Item("tranNo")
                        tranSeq = Trim(.Item("tranSeq"))
                        fldName.Add(IMAAL.ProductItem)
                        fldName.Add(IMAAL.ProductName)
                        fldName.Add(IMAAL.Specifaction)
                        fldName.Add("nvl(" & IMAA.NetWeight & ",0):" & IMAA.NetWeight)
                        fldName.Add(SFAA.EstimatedStorgeBacthNo)
                        fldName.Add(SFAA.EstimatedStorgeLocation)
                        fldName.Add(PMAAL.ContactName)

                        If tranSeq = "0" Then 'asft335
                            'fldName.Add(SFFB.DocNo)
                            fldName.Add(SFFB.WONo & ":MO")
                            fldName.Add("to_char(" & SFFB.DocumentDate & ",'" & ConfigDate.FormatShow2 & "'):DT")
                            'fldName.Add(SFFB.WorkReportItemNo)

                            WHR = varIni.getWhrFirst(varIni.SFFB)
                            WHR &= Conn_SQL.Where(SFFB.DocNo, tranNumber,, False)

                            dt = SFFB.getTransferData_Process_MO_Item(fldName, WHR)

                        Else 'asft340
                            'fldName.Add(SFFB.DocNo)
                            fldName.Add(SFEB.WONo & ":MO")
                            fldName.Add("to_char(" & SFEA.DocumentDate & ",'" & ConfigDate.FormatShow2 & "'):DT")
                            'fldName.Add(SFEB.ItemNo)

                            WHR = varIni.getWhrFirst(varIni.SFEB)
                            WHR &= Conn_SQL.Where(SFEB.DocNo, tranNumber,, False)
                            WHR &= Conn_SQL.Where(SFEB.LineNo, tranSeq,, False)

                            dt = SFEB.getMoReceiptData_Process_MO_Item(fldName, WHR)

                        End If
                        packBy = .Item("PackBy")
                        custPO = .Item("custPO")
                        serailNo = .Item("serailNo")
                        qty = .Item("qty")
                        qtyCnt = .Item("qtyCtn")
                        wghCarton = .Item("CtnWgh")
                        'itemWgh = .Item("serailNo")

                    End With

                End If

                'SQL = " select Rtrim(MOCTA.TA001)+'-'+MOCTA.TA002 C, Rtrim(MOCTA.TA026)+'-'+RTRIM (MOCTA.TA027)+'-'+RTRIM(MOCTA.TA028) D," &
                '      "  isnull(custPO,'') E,MOCTA.TA006 F,MOCTA.TA034 G, COPMA.MA002 L ," &
                '      " case when isnull(COPMG.MG006,'')='' then SFCTC.TC049 else COPMG.MG006 end  H, " &
                '      " INVMB.MB014  N,SFCTC.TC036 O,SFCTB.TB015 X," &
                '      " case when F.DocNo = '' then INVMB.MB073 else F.qtyCtn end  P," &
                '      " case when F.DocNo = '' then INVMB.MB075 else F.CtnWgh end  Q,  MOCTA.UDF01 T,F.PackBy Z," &
                '      " case when rtrim(SFCTC.TC056)='' then INVMC.MC015 else SFCTC.TC056 end Y, " &
                '      " SFCTC.TC001,SFCTC.TC002,SFCTC.TC003,serialNo ZZ from MOCTA " &
                '      " left join COPTD on COPTD.TD001 = MOCTA.TA026 and COPTD.TD002 = MOCTA.TA027 and COPTD.TD003 = MOCTA.TA028 " &
                '      " left join COPTC on COPTC.TC001 = MOCTA.TA026 and COPTC.TC002 = MOCTA.TA027" &
                '      " left join COPMA on COPMA.MA001 = COPTC.TC004" &
                '      " left join COPMG On COPMG.MG001 = COPTC.TC004 and COPMG.MG002 = COPTD.TD004 and COPMG.MG003 = COPTD.TD014 " &
                '      " left join SFCTC on SFCTC.TC004 = MOCTA.TA001 and SFCTC.TC005 = MOCTA.TA002" &
                '      " left join SFCTB on SFCTB.TB001 = SFCTC.TC001 and SFCTB.TB002 = SFCTC.TC002" &
                '      " left join " & Conn_SQL.DBReport & "..FGLabel F on F.moType = SFCTB.TB001 and F.moNo = SFCTB.TB002 and F.moSeq = SFCTC.TC003 " &
                '      " left join INVMB on INVMB.MB001 = MOCTA.TA006 " &
                '      " left join INVMC on INVMC.MC001 = MOCTA.TA006 and INVMC.MC002=MOCTA.TA020" &
                '      " where  F.DocNo='" & Request.QueryString("docno").ToString.Trim & "' " &
                '      " order by SFCTB.TB001,SFCTB.TB002,SFCTB.TB003 "
            Else 'sale del
                SQL = ""
                'SQL = "(select top 1 rtrim(TA001)+'-'+TA002 from MOCTA where TA026=TH014 and TA027 =TH015 and TA028=TH016 and TA006=TH004 and TA057=TH017 order by TA040 desc)"
                'SQL = " select TH001 TC001,TH002 TC002,TH003 TC003 ,isnull(" & SQL & ",'') C,TH030 E,TH004 F,TH005 G,COPMG.MG006 H,MB014 N, " &
                '      " case when isnull(F.DocNo,'') <> '' then F.qtyCtn else INVMB.UDF51 end P," &
                '      " case when isnull(F.DocNo,'') <> '' then F.CtnWgh else INVMB.MB075 end Q, " &
                '      " replace(TH017,'*','') T ,isnull(F.PackBy,'') Z,isnull(serialNo,'') ZZ ,MA002 L ,TH008 O ,TG042 X,TH056 Y " &
                '      " from COPTH left join COPTG on TG001=TH001 and TG002=TH002 " &
                '      " Left join COPTD On TD001 = TH014 And TD002 = TH015 And TD003 = TH016 " &
                '      " left join COPTC On TC001 = TH014 And TC002 = TH015 " &
                '      " left join COPMA On MA001 = TG004" &
                '      " left join COPMG On COPMG.MG001 = COPTG.TG004 and COPMG.MG002 = COPTH.TH004 and COPMG.MG003 = COPTH.TH019 " &
                '      " left join " & Conn_SQL.DBReport & "..FGLabel F On F.moType = TH001 And F.moNo = TH002 And F.moSeq = TH003 " &
                '      " left join INVMB On MB001 = TH004 where F.DocNo='" & Request.QueryString("docno").ToString.Trim & "' " &
                '      " order by TH001,TH002,TH003 "
            End If

            'dt = Conn_SQL.Get_DataReader(SQL, Conn_SQL.ERP_ConnectionString)
            If dt.Rows.Count > 0 Then
                With dt.Rows(0)
                    'Dim qty As Decimal = .Item("O")
                    'Dim qtyCnt As Decimal = .Item("P")
                    'Dim wghCarton As Decimal = .Item("Q")
                    Dim itemWgh As Decimal = .Item(IMAA.NetWeight)
                    Dim full As Integer = 0
                    Dim notFull As Integer = qty
                    Dim notFullGross As Decimal = 0
                    If qtyCnt > 0 Then
                        full = Math.Floor(qty / qtyCnt)
                        notFull = qty Mod qtyCnt
                    End If
                    For i As Integer = 1 To full
                        If prtFor <> "2" Then 'general==>N
                            printLabelGeneral(document, dt.Rows(0), qtyCnt, (qtyCnt * itemWgh), (qtyCnt * itemWgh) + wghCarton, packBy)
                        Else
                            printLabelAMP(document, dt.Rows(0), qtyCnt, (qtyCnt * itemWgh), (qtyCnt * itemWgh) + wghCarton, packBy, custPO, serailNo)
                        End If
                    Next
                    If notFull > 0 Then
                        If prtFor <> "2" Then 'general==>N
                            printLabelGeneral(document, dt.Rows(0), notFull, (notFull * itemWgh), (notFull * itemWgh) + wghCarton, packBy)
                        Else
                            printLabelAMP(document, dt.Rows(0), notFull, (notFull * itemWgh), (notFull * itemWgh) + wghCarton, packBy, custPO, serailNo)
                        End If
                    End If
                    'record when open for print 
                    Dim fld As Hashtable = New Hashtable,
                    whr As Hashtable = New Hashtable
                    'Dim moRcv() As String = Trim(dt.Rows(0).Item("N")).Split("-")
                    'fld.Add("docType", Trim(dt.Rows(0).Item("TC001")))
                    fld.Add("docNo", tranNumber)
                    fld.Add("docSeq", tranSeq)
                    fld.Add("fullBox", full)
                    fld.Add("qtyBox", qtyCnt)
                    fld.Add("qtyLast", notFull)
                    fld.Add("CreateBy", Session("UserName"))
                    fld.Add("CreateDate", DateTime.Today.ToString("yyyyMMdd hhmmss"))
                    Conn_SQL.Exec_Sql(Conn_SQL.GetSQL("LabelLog", fld, whr, "I"), Conn_SQL.MIS_ConnectionString)
                End With
            Else
                document.NewPage()
                Dim phrase As Phrase = Nothing
                Dim cell As PdfPCell = Nothing
                Dim table As PdfPTable = Nothing
                Dim color__1 As BaseColor = Nothing
                Dim font As Font = Nothing

                table = New PdfPTable(1)
                table.TotalWidth = 280.0F
                table.LockedWidth = True
                table.SetWidths(New Single() {1.0F})

                font = FontFactory.GetFont("Arial", 16, Font.BOLD, BaseColor.BLACK)
                phrase = New Phrase()
                phrase.Add(New Chunk("No data Print", font))
                cell = controlPDF.PhraseCell(phrase, PdfPCell.ALIGN_CENTER, PdfPCell.ALIGN_MIDDLE)

                table.AddCell(cell)
                document.Add(table)
            End If

            'document.Add(table)
            document.Close()
            Dim bytes As Byte() = memoryStream.ToArray()
            memoryStream.Close()
            Response.ClearContent()
            Response.ClearHeaders()
            Response.AddHeader("content-disposition", "inline;filename=" & docName) 'attachment
            Response.ContentType = "application/pdf"
            Response.BinaryWrite(bytes)
            Response.Flush()
            Response.Clear()
        End Using

    End Sub

    Sub printLabelGeneral(ByRef document As Document, dr As DataRow, qty As Integer, netWgh As Decimal, grossWgh As Decimal, packBy As String)
        Dim phrase As Phrase = Nothing
        Dim cell As PdfPCell = Nothing
        Dim table As PdfPTable = Nothing
        Dim color__1 As BaseColor = Nothing
        Dim fontHead As Font = FontFactory.GetFont("Arial", 14, Font.BOLD, BaseColor.BLACK)
        Dim fontNormal As Font = FontFactory.GetFont("Arial", 11, Font.NORMAL, BaseColor.BLACK)
        Dim fontBold As Font = FontFactory.GetFont("Arial", 12, Font.BOLD, BaseColor.BLACK)
        document.NewPage()

        table = New PdfPTable(2)
        table.TotalWidth = 280.0F
        table.LockedWidth = True
        table.SetWidths(New Single() {0.7F, 1.0F})
        Dim prtFor As String = Request.QueryString("prtFor").ToString.Trim

        'Company Logo
        cell = controlPDF.ImageCell("~/Images/logo-jp2.jpg", 20.0F, PdfPCell.ALIGN_CENTER)
        table.AddCell(cell)

        'font = FontFactory.GetFont("Arial", 14, font.BOLD, BaseColor.BLACK)
        phrase = New Phrase()
        phrase.Add(New Chunk("FINISHED GOODS LABEL", fontHead))
        cell = controlPDF.PhraseCell(phrase, PdfPCell.ALIGN_CENTER, PdfPCell.ALIGN_MIDDLE)

        table.AddCell(cell)
        document.Add(table)
        'body
        Dim colData As ArrayList = New ArrayList
        colData.Add("Cust Name :" & dr.Item(PMAAL.ContactName).ToString.Trim & "◘2") '
        colData.Add("Item:" & dr.Item(IMAAL.ProductItem).ToString.Trim & "◘2")
        'font = FontFactory.GetFont("Arial", 10, font.NORMAL, BaseColor.BLACK)
        table = controlPDF.rowPrintData(2, 280.0F, New Single() {5.0F, 5.0F}, colData, fontNormal, False, 4.0F)
        document.Add(table)
        colData = New ArrayList

        colData.Add("Part No: " & dr.Item(IMAAL.Specifaction).ToString.Trim & "◘2")
        'font = FontFactory.GetFont("Arial", 12, font.BOLD, BaseColor.BLACK)
        table = controlPDF.rowPrintData(2, 280.0F, New Single() {5.0F, 5.0F}, colData, fontBold, False, 4.0F)
        document.Add(table)
        colData = New ArrayList
        Dim txt As String = dr.Item(IMAAL.ProductName).ToString.Trim
        colData.Add("Part Name: " & If(txt.Length > 30, txt.Substring(0, 30), txt) & "◘2")
        'Dim batchTxt As String = ""
        'Dim label As String = "SO No:"
        'Dim val As String = "D"
        'If prtFor <> "N" Then
        '    batchTxt = " Batch No: " & dr.Item("T").ToString.Trim
        '    label = "PO :"
        '    val = "E"
        'End If
        colData.Add("MFG No:" & dr.Item("MO").ToString.Trim & "◘2")
        table = controlPDF.rowPrintData(2, 280.0F, New Single() {5.0F, 5.0F}, colData, fontNormal, False, 4.0F)
        document.Add(table)
        'colData.Add(label & " : " & dr.Item(val).ToString.Trim & "◘2")
        colData = New ArrayList
        colData.Add("Qty/Carton: " & qty.ToString("#,##0") & " Pcs◘2")
        table = controlPDF.rowPrintData(2, 280.0F, New Single() {5.0F, 5.0F}, colData, fontBold, False, 4.0F)
        document.Add(table)
        colData = New ArrayList
        colData.Add("Net Weight: " & netWgh.ToString("#,##0.00") & " Kgs")
        colData.Add("Gross Weight: " & grossWgh.ToString("#,##0.00") & " Kgs")
        'colData.Add("Net Weight: 100.00 Kgs")
        'colData.Add("Gross Weight: 100.50 Kgs")
        colData.Add("Packing Date:" & dr.Item("DT").ToString.Trim & "◘2")
        colData.Add("Pack By: " & packBy & "◘2") '& dr.Item("Z").ToString.Trim
        'colData.Add("Date of Delivery: ◘2")
        'colData = New ArrayList
        colData.Add("Bin: " & dr.Item(SFAA.EstimatedStorgeLocation).ToString.Trim) '
        colData.Add("../Images/RoHS.png◘1◘I◘20.0F")
        table = controlPDF.rowPrintData(2, 276.0F, New Single() {5.0F, 5.0F}, colData, fontNormal, False, 4.0F)
        document.Add(table)

    End Sub

    Sub printLabelAMP(ByRef document As Document, dr As DataRow, qty As Integer, netWgh As Decimal, grossWgh As Decimal, packBy As String, custPO As String, serailNo As String)
        Dim phrase As Phrase = Nothing
        Dim cell As PdfPCell = Nothing
        Dim table As PdfPTable = Nothing
        Dim color__1 As BaseColor = Nothing
        'Dim font As Font = Nothing
        'Dim fontHead As Font = FontFactory.GetFont("Arial", 14, font.BOLD, BaseColor.BLACK)
        'Dim fontNormal As Font = FontFactory.GetFont("Arial", 9, Font.NORMAL, BaseColor.BLACK)
        'Dim fontBold As Font = FontFactory.GetFont("Arial", 11, Font.BOLD, BaseColor.BLACK)

        Dim fontHead As Font = FontFactory.GetFont("Arial", 14, Font.BOLD, BaseColor.BLACK)
        Dim fontNormal As Font = FontFactory.GetFont("Arial", 10, Font.NORMAL, BaseColor.BLACK)
        Dim fontBold As Font = FontFactory.GetFont("Arial", 12, Font.BOLD, BaseColor.BLACK)

        'table.TotalWidth = 280.0F
        Dim paddingButton As Single = 0.6F
        Dim totalWidth As Single = 280.0F

        document.NewPage()

        table = New PdfPTable(2)
        table.TotalWidth = totalWidth
        table.LockedWidth = True
        table.SetWidths(New Single() {0.7F, 1.0F})
        Dim prtFor As String = Request.QueryString("prtFor").ToString.Trim

        'Company Logo
        cell = controlPDF.ImageCell("~/Images/logo-jp2.jpg", 20.0F, PdfPCell.ALIGN_CENTER)
        table.AddCell(cell)

        'font = FontFactory.GetFont("Arial", 14, font.BOLD, BaseColor.BLACK)
        phrase = New Phrase()
        phrase.Add(New Chunk("FINISHED GOODS LABEL", fontHead))
        cell = controlPDF.PhraseCell(phrase, PdfPCell.ALIGN_CENTER, PdfPCell.ALIGN_MIDDLE)

        table.AddCell(cell)
        document.Add(table)
        'body
        Dim colData As ArrayList = New ArrayList
        colData.Add("Cust Name :" & dr.Item(PMAAL.ContactName).ToString.Trim & "◘2") '" & dr.Item("L").ToString.Trim & "
        document.Add(controlPDF.rowPrintData(2, totalWidth, New Single() {5.0F, 5.0F}, colData, fontNormal, False, paddingButton))
        'fldName.Add()
        'fldName.Add()

        colData = New ArrayList
        colData.Add("Part No: " & dr.Item(IMAAL.Specifaction).ToString.Trim & "◘2")
        document.Add(controlPDF.rowPrintData(2, totalWidth, New Single() {5.0F, 5.0F}, colData, fontBold, False, paddingButton))

        colData = New ArrayList
        Dim txt As String = dr.Item(IMAAL.ProductName).ToString.Trim
        colData.Add("Part Name: " & If(txt.Length > 40, txt.Substring(0, 40), txt) & "◘2")
        'colData.Add("Part Name: " & dr.Item("G").ToString.Trim & "◘2")
        document.Add(controlPDF.rowPrintData(2, totalWidth, New Single() {5.0F, 5.0F}, colData, fontNormal, False, paddingButton))

        colData = New ArrayList
        colData.Add("Part Item:" & dr.Item(IMAAL.ProductItem).ToString.Trim & " Batch No: " & dr.Item(SFAA.EstimatedStorgeBacthNo).ToString.Trim & "◘2")
        document.Add(controlPDF.rowPrintData(2, totalWidth, New Single() {5.0F, 5.0F}, colData, fontBold, False, paddingButton))

        colData = New ArrayList
        colData.Add("MFG No:" & dr.Item("MO").ToString.Trim & "◘2")
        colData.Add("Cust PO : " & custPO & "◘2") 'dr.Item("E").ToString.Trim
        document.Add(controlPDF.rowPrintData(2, totalWidth, New Single() {5.0F, 5.0F}, colData, fontNormal, False, paddingButton))

        colData = New ArrayList
        colData.Add("Qty/Carton: " & qty.ToString("#,##0") & " Pcs◘2")
        document.Add(controlPDF.rowPrintData(2, totalWidth, New Single() {5.0F, 5.0F}, colData, fontBold, False, paddingButton))

        colData = New ArrayList
        colData.Add("Net Weight: " & netWgh.ToString("#,##0.00") & " Kgs")
        colData.Add("Gross Weight: " & grossWgh.ToString("#,##0.00") & " Kgs")
        colData.Add("Packing Date:" & dr.Item("DT").ToString.Trim)
        colData.Add("Date of Delivery:")
        colData.Add("Pack By: " & packBy & "◘2") 'dr.Item("Z").ToString.Trim
        'colData.Add("../Images/RoHS.png◘1◘I◘20.0F")
        colData.Add("Serail No.: " & serailNo & "◘2") 'dr.Item("ZZ").ToString.Trim
        document.Add(controlPDF.rowPrintData(2, totalWidth, New Single() {4.5F, 5.5F}, colData, fontNormal, False, paddingButton))
    End Sub

End Class