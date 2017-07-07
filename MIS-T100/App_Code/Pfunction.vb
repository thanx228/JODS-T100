Imports Microsoft.VisualBasic
Imports System.IO
Imports System.Drawing
Imports System.Drawing.Imaging

Public Class Pfunction
    '*********************************************************************   
    '   
    ' alex_file_upload() 函式   
    '   
    ' 公用函式，上傳檔案
    '
    ' path 上傳位置
    ' file_name Client端的檔案完整路徑
    ' file2 PAGE上的物件名稱
    ' limit 檔名限制
    ' store_sc_id 商家分類代號
    ' store_main_id 商家代碼
    ' folder 商家功能資料夾分類
    ' filesize 檔案大小上限
    '
    ' alex_file_upload 
    '               0 為上傳
    '               1 上傳成功
    '               2 檔案過大
    '               3 副黨名稱錯誤
    '********************************************************************* 
    Public Shared Function file_upload(ByVal path As String, ByVal Savefilename As String, ByVal file2 As FileUpload, ByVal limit As String, ByVal filesize As Integer) As Integer
        If file2.HasFile Then
            Dim UserFile As HttpPostedFile = file2.PostedFile()
            Dim file_name As String = UserFile.FileName
            Dim find_type As Integer = InStrRev(file_name, ".", Len(file_name), 0)
            Dim file_type As String = Right(file_name, Len(file_name) - find_type)
            If InStr(UCase(limit), UCase(file_type)) <> 0 Or limit = "" Then

                Dim find_str As Integer = InStrRev(file_name, "\", Len(file_name), 0)
                Dim right_path As String = path

                Dim UserFileLen As Integer = UserFile.ContentLength()
                If filesize <> 0 Then
                    If UserFileLen > filesize Then
                        file_upload = 2
                        Exit Function
                    End If
                End If
                If (UserFileLen > 0) Then
                    Dim fso, fldr As Object
                    fso = CreateObject("Scripting.FileSystemObject")
                    If Not fso.FolderExists(right_path) Then
                        fso = CreateObject("Scripting.FileSystemObject")
                        fldr = fso.CreateFolder(right_path)
                    End If

                    Dim FileBuf(UserFileLen) As Byte
                    UserFile.InputStream.Read(FileBuf, 0, UserFileLen)
                    Dim fs As New System.IO.FileStream(right_path & Savefilename & "." & UCase(file_type), System.IO.FileMode.Create)
                    'fs.
                    fs.Write(FileBuf, 0, FileBuf.Length)
                    fs.Close()
                    file_upload = 1
                End If
            Else
                file_upload = 3
            End If
        End If
    End Function

    Public Function getFileExtention(ByVal fileUpload As FileUpload) As String
        Dim myUpload As HttpPostedFile = fileUpload.PostedFile()
        Return UCase(Path.GetExtension(myUpload.FileName))
    End Function

    Public Function FileUpload(ByVal path As String, ByVal fileName As String, ByVal oldFileName As String, ByVal fileUplaod As FileUpload, Optional genThumbnail As Boolean = False) As Boolean
        Dim fileReturn As Boolean = True
        'Dim fileInfo
        If fileUplaod.HasFile Then

            'check folder exists
            Dim DirInfo As New DirectoryInfo(path)
            If Not DirInfo.Exists Then
                DirInfo.Create()
            End If

            'delete file if exists
            If oldFileName <> "" Then
                deleteFile(path, oldFileName)
            End If

            'upload file
            Dim myUpload As HttpPostedFile = fileUplaod.PostedFile()
            myUpload.SaveAs(path & fileName)
            If genThumbnail Then
                generateThumbnail(path, fileName)
            End If
        End If
        Return fileReturn
    End Function
    'add 2015-06-12 noi
    Sub generateThumbnail(path As String, strFileName As String)
        'Create a new Bitmap Image loading from location of origional file
        Dim bm As Bitmap = System.Drawing.Image.FromFile(path & strFileName)
        Dim newStrFileName As String = path & "T_" & strFileName

        If File.Exists(newStrFileName) Then
            File.Delete(newStrFileName)
        End If

        'Declare Thumbnails Height and Width
        'Dim newWidth As Integer = 300
        Dim newWidth As Integer = 120
        Dim newHeight As Integer = (newWidth / bm.Width) * bm.Height

        'Create the new image as a blank bitmap
        Dim resized As Bitmap = New Bitmap(newWidth, newHeight)

        'Create a new graphics object with the contents of the origional image
        Dim g As Graphics = Graphics.FromImage(resized)

        'Resize graphics object to fit onto the resized image
        g.DrawImage(bm, New Rectangle(0, 0, resized.Width, resized.Height), 0, 0, bm.Width, bm.Height, GraphicsUnit.Pixel)

        'Get rid of the evidence
        g.Dispose()

        'Create new path and filename for the resized image

        'Save the new image to the same folder as the origional
        resized.Save(newStrFileName, ImageFormat.Jpeg)

    End Sub

    Public Function getFileSize(ByVal fileUpload As FileUpload) As Integer
        Dim myUpload As HttpPostedFile = fileUpload.PostedFile()
        Return myUpload.ContentLength
    End Function

    Public Function deleteFile(ByVal path As String, ByVal fileName As String) As Boolean
        Dim FileInfo As New FileInfo(path & fileName)
        If FileInfo.Exists Then
            FileInfo.Delete()
        End If
        Return True
    End Function

    Shared Sub ShowGrid(ByRef GridName As GridView, ByVal DataRecord As Data.DataTable, ByVal ShowFiled As String, ByVal ShowPaging As Boolean, _
                     ByVal PageIndex As Integer, ByVal L_id As Integer, ByVal PageName As String, Optional ByVal SpecCellNo As Integer = 0, _
                     Optional ByVal SpecCellMethod As String = "", Optional ByVal HaveSerialNumber As Boolean = False _
                                   , Optional ByVal HaveCheckBox As Boolean = False _
                                   , Optional ByVal HaveDelete As Boolean = False _
                                   , Optional ByVal HaveEdit As Boolean = False _
                                   , Optional ByVal HaveShowPic As Boolean = False _
                                   , Optional ByVal EditButtonURL As String = "" _
                                   , Optional ByVal HaveFreeTemp As Boolean = False _
                                   , Optional ByVal HasShow As Boolean = True)
        'gridview要顯示哪些欄位

        Dim ArrayShowFiled() As String = Split(ShowFiled, ",")

        '設定DT3的資料結構

        GridName.Columns.Clear()

        'If SearchForm1.GetMaxPageIndex = "" And WebProperty.PageSize <> 0 Then          '設定最大頁面數
        '    SearchForm1.SetMaxPageIndex(DT3.Rows.Count / WebProperty.PageSize)
        'End If

        '設定gridview的資料結構
        Dim HaveFunctionCount As Integer = 0
        If HaveDelete Then
            HaveFunctionCount += 1
        End If
        If HaveEdit Then
            HaveFunctionCount += 1
        End If
        If HaveShowPic Then
            HaveFunctionCount += 1
        End If

        Dim ShowDataFiledName(ArrayShowFiled.Length - 1) As String
        Dim HiddenDataFiledName() As String = {}
        Dim GridHeadName(ArrayShowFiled.Length - 1 + HaveFunctionCount) As String
        Dim GridColumnsWidth(ArrayShowFiled.Length - 1 + HaveFunctionCount) As Integer
        For L_count As Integer = 0 To ArrayShowFiled.Length - 1
            ShowDataFiledName(L_count) = ArrayShowFiled(L_count)
            GridHeadName(L_count) = ArrayShowFiled(L_count)
            If HasShow = False Then
                GridColumnsWidth(L_count) = 200
            End If
        Next

        Dim NowFunctionCount As Integer = 0
        If HaveEdit Then
            NowFunctionCount += 1
            GridHeadName(ArrayShowFiled.Length - 1 + NowFunctionCount) = "Edit"
        End If
        If HaveDelete Then
            NowFunctionCount += 1
            GridHeadName(ArrayShowFiled.Length - 1 + NowFunctionCount) = "Delete"
        End If
        If HaveShowPic Then
            NowFunctionCount += 1
            GridHeadName(ArrayShowFiled.Length - 1 + NowFunctionCount) = "ShowDrawing"
        End If
        'GridHeadName(ArrayShowFiled.Length - 1 + 1) = "Edit"
        'GridHeadName(ArrayShowFiled.Length - 1 + 2) = "Print"
        '設定GridView的資料結構
        If HasShow Then
            GridViewFunction.GridViewColumns(GridName, ShowDataFiledName, HiddenDataFiledName, GridColumnsWidth, L_id, HaveSerialNumber, HaveCheckBox, HaveDelete, HaveEdit, HaveFreeTemp)
        Else
            GridViewFunction.GridViewColumns(GridName, ShowDataFiledName, HiddenDataFiledName, GridColumnsWidth, L_id, HaveSerialNumber, HaveCheckBox, HaveDelete, HaveEdit, HaveFreeTemp)
            'GridViewFunction.GridViewColumns(GridName, ShowDataFiledName, HiddenDataFiledName, GridColumnsWidth, L_id, HaveSerialNumber, HaveCheckBox, HaveDelete, HaveEdit, HaveFreeTemp, True)
        End If

        '設定head的顯示
        GridViewFunction.GridHeadTextSet(GridName, PageName, GridHeadName, L_id)
        'GridName.AutoGenerateEditButton = True
        GridName.DataSource = DataRecord.DefaultView
        GridName.HorizontalAlign = HorizontalAlign.Left

        If HasShow Then
            If ShowPaging Then          '允許分頁與否
                GridName.AllowPaging = True
                GridName.PageSize = WebProperty.PageSize
                GridName.PagerSettings.Visible = True

                If PageIndex <> -1 Then
                    GridName.PageIndex = PageIndex
                Else
                    GridName.PageIndex = 0
                End If
            Else
                GridName.AllowPaging = False
            End If

        End If

        GridName.DataBind()

        If HasShow Then
            For L_count As Integer = 0 To GridName.Rows.Count - 1
                Dim FunctionCount As Integer = 0
                If HaveEdit Then
                    Dim tempImageEdit As New ImageButton                                                '增加edit button
                    tempImageEdit = GridName.Rows(L_count).Cells(ArrayShowFiled.Length + FunctionCount).FindControl("ItemEdit")
                    tempImageEdit.CommandArgument = DataRecord.Rows(GridName.PageIndex * GridName.PageSize + L_count).Item(0)
                    FunctionCount += 1
                    'tempImageEdit = GridName.Rows(L_count).Cells(ArrayShowFiled.Length).FindControl("ItemEdit")
                    'tempImageEdit.Attributes("onmouseover") = "style.cursor='hand';"
                    'tempImageEdit.Attributes("Onclick") = "location.href='" & EditButtonURL & "=" & DataRecord.Rows(GridName.PageIndex * GridName.PageSize + L_count).Item(0) & "';"
                    ''                tempImageEdit.CommandArgument = DataRecord.Rows(GridName.PageIndex * GridName.PageSize + L_count).Item(0)
                End If

                If HaveDelete Then
                    Dim tempImageDelete As New ImageButton                                              '增加delete button
                    tempImageDelete = GridName.Rows(L_count).Cells(ArrayShowFiled.Length + FunctionCount).FindControl("ItemDelete")
                    tempImageDelete.CommandArgument = DataRecord.Rows(GridName.PageIndex * GridName.PageSize + L_count).Item(0)
                    FunctionCount += 1
                End If

                'Dim FullPicUrl As String = "/picfile/" & PersonalID.Text & "/" & DT3.Rows(GridName.PageIndex * GridName.PageSize + L_count).Item("PicURL")
                'Dim tempImagePrint As New ImageButton                                              '增加delete button
                'tempImagePrint = GridName.Rows(L_count).Cells(ArrayShowFiled.Length + 1).FindControl("ItemPrint")
                'tempImagePrint.Attributes.Add("onmousedown", "window.open('/printForm/PrintPic.aspx?PicUrl=" & FullPicUrl & "','','left=150,top=150,width=600,height=600,toolbar=0,resizable=0');")
                ''tempImagePrint.CommandArgument = DT3.Rows(GridName.PageIndex * GridName.PageSize + L_count).Item("PicURL")

                If SpecCellMethod <> "" Then
                    Dim strMethod As String = Replace(SpecCellMethod, "@ID", GridName.Rows(L_count).Cells(0).Text)
                    GridName.Rows(L_count).Cells(SpecCellNo).Attributes.Add("onmousedown", strMethod)
                    GridName.Rows(L_count).Cells(SpecCellNo).Attributes("onmouseover") = "style.color ='blue';style.cursor='hand';style.bold='true'"
                    GridName.Rows(L_count).Cells(SpecCellNo).Attributes("onmouseout") = "style.color ='black';"
                End If
                'GridName.Rows(L_count).Attributes("onmouseover") = "style.color ='blue';style.cursor='hand';"
                'GridName.Rows(L_count).Attributes.Add("onmousedown", "Alextest")
            Next
        Else
            'For L_count As Integer = 0 To GridName.Rows.Count - 1
            '    'If HaveDelete Then
            '    Dim tempImageDelete As New Image                                              '增加delete button
            '    tempImageDelete = GridName.Rows(L_count).Cells(6).FindControl("ItemQPicture")
            '    tempImageDelete.ImageUrl = "/QuestionPic/" & GridName.Rows(L_count).Cells(6).Text
            '    'End If
            'Next

        End If
        StyleGridView.GVHeadStyle(GridName)
        'DT3.Dispose()
        'ShowPanel.Visible = True
    End Sub

    Private Shared Function GridViewFunction() As Object
        Throw New NotImplementedException
    End Function

    Private Shared Function WebProperty() As Object
        Throw New NotImplementedException
    End Function

    Private Shared Function StyleGridView() As Object
        Throw New NotImplementedException
    End Function


End Class
