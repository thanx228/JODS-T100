Public Class VarIni 'variable initial

    'var for sql
    Public A As String = " and "
    Public B As String = " * " 'all data from select
    Public C As String = "," 'comma
    Public E As String = " = " 'equal
    Public F As String = " from "
    Public G As String = " group by "
    Public L As String = " left join "
    Public O As String = " order by "
    Public O2 As String = " on "
    Public S As String = "select "
    Public SQ As String = "'" 'single qout
    Public W As String = " where "

    'val
    Public EntV As String = "3"
    Public SiteV As String = "JINPAO"
    Public Prefix As String = "JP"
    Public enUS_V As String = "en_US"

    'field name in postfix
    Public EntF As String = "ent"
    Public SiteF As String = "site"
    Public DocNoF As String = "docno"
    Public DocSeqF As String = "seq"
    Public DocDateF As String = "docdt"
    Public StatusF As String = "stus"
    Public OwnerF As String = "ownid"
    Public OwnerDeptF As String = "owndp"
    Public CreatedByF As String = "crtid"
    Public CreatedDeptF As String = "crtdp"
    Public CreatedDateF As String = "crtdt"
    Public ModifiedByF As String = "modid"
    Public ModifiedDateF As String = "moddt"
    Public ConfirmedByF As String = "cnfid"
    Public ConfirmedDateF As String = "cnfdt"
    Public PostedByF As String = "pstid"
    Public PostedDateF As String = "pstdt"
    Public endTable As String = "_t"

    'table name prefix
    Public SFAA As String = "sfaa" 'asft300 mo Head
    Public SFBA As String = "sfba" 'asft300 mo Body
    Public SFCB As String = "sfcb" 'asft301 mo Body

    Public SFFB As String = "sffb" 'asft335 transfer order
    Public SFEA As String = "sfea" 'asft340 mo receipt Head #1 
    Public SFEB As String = "sfeb" 'asft340 mo receipt body #Stock in request 

    Public IMAA As String = "imaa" 'aimm200 item
    Public IMAAL As String = "imaal" 'aimm200 item for name
    Public IMAF As String = "imaf" 'aimm212 item for control lot
    Public OOBX As String = "oobx" 'aooi199 doc type
    Public OOBXL As String = "oobxl" 'aooi199 doc type lang

    Public XMDA As String = "xmda" 'axmt500 sale order head
    Public XMDC As String = "xmdc" 'axmt500 sale order body
    Public PMAA As String = "pmaa" 'axmm200 customer and vender
    Public PMAAL As String = "pmaal" 'axmm200 customer and vendors lang

    Public PMDS As String = "pmds" 'apmt520 purchase receipt head
    Public PMDT As String = "pmdt" 'apmt520 purchase receipt body



    Public Function getFldDoc(fldcode As String, Optional getNo As Boolean = False) As String
        'default get type
        Dim sPos As String = "3"
        Dim len As String = "4"
        If getNo Then 'number
            sPos = "8"
            len = "11"
        End If
        Return "SUBSTR(" & fldcode & "," & sPos & "," & len & ") "
    End Function

    Function getWhrFirst(tc As String, Optional showSite As Boolean = True) As String 'tc=table code
        'Dim VarIni As New VarIni
        Dim strWhr As String = W & tc & EntF & E & EntV
        If showSite Then
            strWhr &= A & tc & SiteF & E & SQ & SiteV & SQ & " "
        End If
        Return strWhr
    End Function

    Public Function getLeftjoinFirst(T1 As String, T2 As String, showSite As Boolean, addLeftjoin As String) As String 'T1=table target,T2=table refer
        'Dim VarIni As New VarIni
        Dim ent As String = EntF
        Dim site_val As String = SiteF
        Dim strLeftJoin As String = L & T1 & endTable & O2 & T1 & ent & E & T2 & ent
        If showSite Then
            strLeftJoin &= A & T1 & SiteF & E & T2 & SiteF
        End If

        If addLeftjoin <> "" Then
            Dim sepaFld() As String = addLeftjoin.Trim.Split(",")
            For Each fld1 As String In sepaFld
                If fld1.Trim <> "" Then
                    Dim fld2() As String = fld1.Split(":")
                    If fld2.Length > 1 Then
                        Dim showSQ As Boolean = False
                        If fld2.Length > 2 Then
                            showSQ = True
                        End If

                        strLeftJoin &= getEqual(fld2(0), fld2(1), showSQ)
                    End If

                End If
            Next

        End If
        Return strLeftJoin
    End Function

    Public Function getLeftjoin(F1 As String, F2 As String) As String
        Return A & F1 & E & F2
    End Function

    Function getEqual(fld As String, val As String, Optional singleQuot As Boolean = False, Optional showAnd As Boolean = True) As String
        Dim andShow As String = ""
        If showAnd Then
            andShow = A
        End If
        If singleQuot Then
            val = " " & SQ & val & SQ & " "
        End If
        Return andShow & fld & E & val
    End Function

    Function getOrderBy(fldOrder As String) As String
        Return O & fldOrder
    End Function

    'Public Function SQL(sel As String, leftjoin As String, orderBy As String, groupBy As String) As String

    'End Function


End Class
