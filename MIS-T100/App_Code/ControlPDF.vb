Imports iTextSharp.text
Imports iTextSharp.text.pdf

Public Class ControlPDF
    Dim Conn_SQL As New ConnSQL
    Public aa As String = ""

    Public Sub DrawLine(writer As PdfWriter, x1 As Single, y1 As Single, x2 As Single, y2 As Single, color As BaseColor)
        Dim contentByte As PdfContentByte = writer.DirectContent
        contentByte.SetColorStroke(color)
        contentByte.MoveTo(x1, y1)
        contentByte.LineTo(x2, y2)
        contentByte.Stroke()
    End Sub

    Public Function PhraseCell(phrase As Phrase, alignH As Integer, Optional alignV As Integer = PdfPCell.ALIGN_TOP) As PdfPCell
        Dim cell As New PdfPCell(phrase)
        cell.BorderColor = BaseColor.WHITE
        cell.VerticalAlignment = alignV
        cell.HorizontalAlignment = alignH
        cell.PaddingBottom = 2.0F
        cell.PaddingTop = 0.0F
        Return cell
    End Function

    Public Function ImageCell(path As String, scale As Single, align As Integer) As PdfPCell
        Dim image As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath(path))
        image.ScalePercent(scale)
        Dim cell As New PdfPCell(image)
        cell.BorderColor = BaseColor.WHITE
        cell.VerticalAlignment = PdfPCell.ALIGN_CENTER
        cell.HorizontalAlignment = align
        cell.PaddingBottom = 0.0F
        cell.PaddingTop = 0.0F
        Return cell
    End Function

    Function rowPrintData(colCnt As Integer, tableWid As Single, colWid() As Single, dataShow As ArrayList, Optional fontSize As Integer = 10, Optional borColorBlack As Boolean = False, Optional PaddingBottom As Single = 7.0F) As PdfPTable
        'text/path◘col◘I=image,''=text◘scal for image
        Dim font As Font = FontFactory.GetFont("Arial", fontSize, font.NORMAL, BaseColor.BLACK)

        Dim table As PdfPTable = New PdfPTable(colCnt)
        table.LockedWidth = True
        table.SpacingBefore = 1.0F
        table.TotalWidth = tableWid
        table.SetWidths(colWid)

        Dim cell As PdfPCell = Nothing
        For i = 0 To dataShow.Count - 1
            Dim showImage = False,
                col As Integer = 1,
                scal As Single = 20.0F
            Dim prt() As String = dataShow(i).ToString.Split("◘")
            If prt.Count > 2 Then
                If prt(2) = "I" Then
                    showImage = True
                End If

            End If
            If prt.Count > 1 Then
                If prt(1) <> "" And IsNumeric(prt(1)) Then
                    col = CInt(prt(1))
                End If

            End If
            If prt.Count > 3 Then
                If prt(3) <> "" And IsNumeric(prt(3)) Then
                    col = CInt(prt(3))
                End If
            End If
            If showImage Then
                cell = ImageCell(prt(0), scal, PdfPCell.ALIGN_CENTER)
            Else
                cell = PhraseCell(New Phrase(prt(0), font), PdfPCell.ALIGN_LEFT)
            End If
            cell.Colspan = col
            If borColorBlack Then
                cell.BorderColor = BaseColor.BLACK
            End If
            cell.PaddingBottom = PaddingBottom

            table.AddCell(cell)
        Next
        Return table

    End Function

    Function rowPrintData(colCnt As Integer, tableWid As Single, colWid() As Single, dataShow As ArrayList, font As Font, Optional borColorBlack As Boolean = False, Optional PaddingBottom As Single = 7.0F) As PdfPTable
        'text/path◘col◘I=image,''=text◘scal for image
        'Dim font As Font = FontFactory.GetFont("Arial", FontSize, font.NORMAL, BaseColor.BLACK)

        Dim table As PdfPTable = New PdfPTable(colCnt)
        table.LockedWidth = True
        table.SpacingBefore = 1.0F
        table.TotalWidth = tableWid
        table.SetWidths(colWid)

        Dim cell As PdfPCell = Nothing
        For i = 0 To dataShow.Count - 1
            Dim showImage = False,
                col As Integer = 1,
                scal As Single = 20.0F
            Dim prt() As String = dataShow(i).ToString.Split("◘")
            If prt.Count > 2 Then
                If prt(2) = "I" Then
                    showImage = True
                End If

            End If
            If prt.Count > 1 Then
                If prt(1) <> "" And IsNumeric(prt(1)) Then
                    col = CInt(prt(1))
                End If

            End If
            If prt.Count > 3 Then
                If prt(3) <> "" And IsNumeric(prt(3)) Then
                    col = CInt(prt(3))
                End If
            End If
            If showImage Then
                cell = ImageCell(prt(0), scal, PdfPCell.ALIGN_CENTER)
            Else
                cell = PhraseCell(New Phrase(prt(0), font), PdfPCell.ALIGN_LEFT)
            End If
            cell.Colspan = col
            If borColorBlack Then
                cell.BorderColor = BaseColor.BLACK
            End If
            cell.PaddingBottom = PaddingBottom

            table.AddCell(cell)
        Next
        Return table

    End Function

    Function limitText(txt As String, limitLen As Integer) As String
        Return If(txt.Length > limitLen, txt.Substring(0, limitLen), txt)
    End Function

End Class
