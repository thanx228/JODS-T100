Imports System
Imports System.Data
Public Class StatusT100

    '###################### Convert String To Status T100 ###########################
    Public Shared Function MO_Normal(Stus As String) As String
        Dim ShowStatus As String = String.Empty
        Select Case Stus
            'Case "A"
            '    ShowStatus = "A : Approved"
            Case "C"
                ShowStatus = "C : Closed"
            Case "D"
                ShowStatus = "D : Widthdraw"
            Case "E"
                ShowStatus = "E : termaination"
            Case "F"
                ShowStatus = "F : Released"
            Case "H"
                ShowStatus = "H : Hold"
            Case "M"
                ShowStatus = "M : Cost Close"
            Case "N"
                ShowStatus = "N : Unapproved"
            Case "O"
                ShowStatus = "O : Confrim Transfer-Out"
            Case "P"
                ShowStatus = "P : Confirmation for Transfer-in"
            Case "R"
                ShowStatus = "R : Rejected"
            Case "W"
                ShowStatus = "W : Approving"
            Case "X"
                ShowStatus = "X : Voided"
            Case "Y"
                ShowStatus = "Y : Approved"
            Case "Z"
                ShowStatus = "Z : Restore Deduction"
        End Select
        Return ShowStatus
    End Function
    Public Shared Function BOM_ItemMaster(Stus As String) As String
        Dim ShowStatus As String = String.Empty
        Select Case Stus
            Case "N"
                ShowStatus = "N : Unapproved"
            Case "X"
                ShowStatus = "X : Invaild"
            Case "Y"
                ShowStatus = "Y : Approved"
        End Select
        Return ShowStatus
    End Function
    Public Shared Function ItemRoutingChange(Stus As String) As String
        Dim ShowStatus As String = String.Empty
        Select Case Stus
            'Case "A"
            '    ShowStatus = "A : Approved"
            Case "D"
                ShowStatus = "D : Widthdarw"
            Case "N"
                ShowStatus = "N : Unapproved"
            Case "R"
                ShowStatus = "R : Rejected"
            Case "W"
                ShowStatus = "W : Approving"
            Case "X"
                ShowStatus = "X : Voided"
            Case "Y"
                ShowStatus = "Y : Approved"
        End Select
        Return ShowStatus
    End Function
    Public Shared Function Store_IQC(Stus As String) As String
        Dim ShowStatus As String = String.Empty
        Select Case Stus
            Case "X"
                ShowStatus = "X : Invaild"
            Case "Y"
                ShowStatus = "Y : Validity"
        End Select
        Return ShowStatus
    End Function
    Public Shared Function MaterailIssue(Stus As String) As String
        Dim ShowStatus As String = String.Empty
        Select Case Stus
            Case "N"
                ShowStatus = "X : Unapproved"
            Case "Y"
                ShowStatus = "Y : Approved"
            Case "S"
                ShowStatus = "S : Posted"
        End Select
        Return ShowStatus
    End Function
    Public Shared Function Purchase(Stus As String) As String
        Dim ShowStatus As String = String.Empty
        Select Case Stus
            Case "A"
                ShowStatus = "A : Approved"
            Case "C"
                ShowStatus = "C : Closed"
            Case "D"
                ShowStatus = "D : Withdraw"
            Case "N"
                ShowStatus = "N : Unconfirmed"
            Case "R"
                ShowStatus = "R : Rejected"
            Case "W"
                ShowStatus = "W : Approving"
            Case "X"
                ShowStatus = "X : Voided"
            Case "Y"
                ShowStatus = "Y : Confirmed"
            Case "H"
                ShowStatus = "H : Retention"
            Case "UH"
                ShowStatus = "UH : Undo holding"
        End Select
        Return ShowStatus
    End Function
End Class
