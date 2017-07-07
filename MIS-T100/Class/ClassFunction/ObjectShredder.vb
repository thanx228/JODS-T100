Imports System.Reflection
Namespace classObject
    Public Class ObjectShredder(Of T)
        Private _fi As FieldInfo()
        Private _pi As PropertyInfo()
        Private _ordinalMap As Dictionary(Of String, Integer)
        Private _type As Type

        Public Sub New()
            _type = GetType(T)
            _fi = _type.GetFields()
            _pi = _type.GetProperties()
            _ordinalMap = New Dictionary(Of String, Integer)()
        End Sub

        Public Function Shred(source As IEnumerable(Of T), table As DataTable, options As System.Nullable(Of LoadOption)) As DataTable
            If GetType(T).IsPrimitive Then
                Return ShredPrimitive(source, table, options)
            End If


            If table Is Nothing Then
                table = New DataTable(GetType(T).Name)
            End If

            ' now see if need to extend datatable base on the type T + build ordinal map
            table = ExtendTable(table, GetType(T))

            table.BeginLoadData()
            Using e As IEnumerator(Of T) = source.GetEnumerator()
                While e.MoveNext()
                    If options IsNot Nothing Then
                        table.LoadDataRow(ShredObject(table, e.Current), CType(options, LoadOption))
                    Else
                        table.LoadDataRow(ShredObject(table, e.Current), True)
                    End If
                End While
            End Using
            table.EndLoadData()
            Return table
        End Function

        Public Function ShredPrimitive(source As IEnumerable(Of T), table As DataTable, options As System.Nullable(Of LoadOption)) As DataTable
            If table Is Nothing Then
                table = New DataTable(GetType(T).Name)
            End If

            If Not table.Columns.Contains("Value") Then
                table.Columns.Add("Value", GetType(T))
            End If

            table.BeginLoadData()
            Using e As IEnumerator(Of T) = source.GetEnumerator()
                Dim values As [Object]() = New Object(table.Columns.Count) {}
                While e.MoveNext()
                    values(table.Columns("Value").Ordinal) = e.Current

                    If options IsNot Nothing Then
                        table.LoadDataRow(values, CType(options, LoadOption))
                    Else
                        table.LoadDataRow(values, True)
                    End If
                End While
            End Using
            table.EndLoadData()
            Return table
        End Function

        Public Function ExtendTable(table As DataTable, type As Type) As DataTable
            ' value is type derived from T, may need to extend table.
            For Each f As FieldInfo In type.GetFields()
                If Not _ordinalMap.ContainsKey(f.Name) Then
                    Dim dc As DataColumn = If(table.Columns.Contains(f.Name), table.Columns(f.Name), table.Columns.Add(f.Name, f.FieldType))
                    _ordinalMap.Add(f.Name, dc.Ordinal)
                End If
            Next
            For Each p As PropertyInfo In type.GetProperties()
                If Not _ordinalMap.ContainsKey(p.Name) Then
                    Dim dc As DataColumn = If(table.Columns.Contains(p.Name), table.Columns(p.Name), table.Columns.Add(p.Name, p.PropertyType))
                    _ordinalMap.Add(p.Name, dc.Ordinal)
                End If
            Next
            Return table
        End Function

        Public Function ShredObject(table As DataTable, instance As T) As Object()

            Dim fi As FieldInfo() = _fi
            Dim pi As PropertyInfo() = _pi

            If instance.[GetType]() <> GetType(T) Then
                ExtendTable(table, instance.[GetType]())
                fi = instance.[GetType]().GetFields()
                pi = instance.[GetType]().GetProperties()
            End If

            Dim values As [Object]() = New Object(table.Columns.Count) {}
            For Each f As FieldInfo In fi
                values(_ordinalMap(f.Name)) = f.GetValue(instance)
            Next

            For Each p As PropertyInfo In pi
                values(_ordinalMap(p.Name)) = p.GetValue(instance, Nothing)
            Next
            Return values
        End Function



    End Class

    Public Class DataTableMerge
        ''' <summary>
        ''' ##############  Routing2 #######################################
        ''' </summary>
        ''' <param name="dt1"></param>
        ''' <param name="dt2"></param>
        ''' <returns name= "dt3"></returns>
        Public Shared Function CompareTwoDataTable(ByVal dt1 As DataTable, ByVal dt2 As DataTable) As DataTable
            dt1.Merge(dt2)
            Dim d3 As DataTable = dt2.GetChanges()
            Return d3
        End Function
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="tbl1"></param>
        ''' <param name="tbl2"></param>
        ''' <returns></returns>
        Public Shared Function AreTablesTheSame(tbl1 As DataTable, tbl2 As DataTable) As Boolean
            If tbl1.Rows.Count <> tbl2.Rows.Count OrElse tbl1.Columns.Count <> tbl2.Columns.Count Then
                Return False
            End If
            Dim i As Integer = 0
            While i < tbl1.Rows.Count
                Dim c As Integer = 0
                While c < tbl1.Columns.Count
                    If Not Equals(tbl1.Rows(i)(c), tbl2.Rows(i)(c)) Then
                        Return False
                    End If
                    c += 1
                End While
                i += 1
            End While
            Return True
        End Function

    End Class


End Namespace