Imports MySql.Data.MySqlClient
Imports System.Data
Public Class clsMySql
    Private strCon As String
    Dim MyADOConnection As MySqlConnection
    Private MYSQLCommand As MySqlCommand  'System.Data.SqlClient.SqlCommand
    Private MYSQLTransaction As MySqlTransaction  'System.Data.SqlClient.SqlTransaction

    Public Sub SetConnectionString(ByVal sqlServer As String, ByVal sqlDb As String, ByVal sqlUid As String, ByVal sqlPwd As String, Optional ByVal sqlTimeout As Integer = 0)

        strCon = "Server='" & sqlServer & "';" & _
                 "Database='" & sqlDb & "';" & _
                "Uid='" & sqlUid & "';" & _
                "Pwd='" & sqlPwd & "';" & _
                "Pooling=False;" '& _
        '"Connect Timeout= " & sqlTimeout & ";"
    End Sub

    Public Function MYSQLConnect() As Boolean
        MYSQLConnect = False
        If strCon = "" Then
            Exit Function
        End If
        Try
            If isMYSQLConnect() Then
                MyADOConnection.Close()
            End If

            MyADOConnection = New MySqlConnection(strCon)
            MyADOConnection.Open()
            MYSQLConnect = True

        Catch ex As Exception
            MYSQLClose()
        End Try
    End Function

    Public Function isMYSQLConnect() As Boolean
        isMYSQLConnect = False
        If Not IsNothing(MyADOConnection) Then
            If MyADOConnection.State = ConnectionState.Open Then
                isMYSQLConnect = True
            End If
        End If
    End Function

    Public Function MYSQLClose() As Boolean
        Try
            MYSQLClose = False
            If Not IsNothing(MyADOConnection) Then
                If MyADOConnection.State = ConnectionState.Open Then
                    MyADOConnection.Close()
                End If
                MyADOConnection = Nothing
            End If
            MYSQLClose = True
        Catch ex As Exception
        End Try

    End Function

    Public Function MYSQLExecuteSQL(ByVal sql As String) As Integer

        Try
            MYSQLExecuteSQL = Nothing
            If isMYSQLConnect() Then
                MYSQLCommand = New MySqlCommand(sql, MyADOConnection)
                MYSQLCommand.Transaction = MYSQLTransaction
                MYSQLCommand.CommandTimeout = 0
                Dim res As Integer
                res = MYSQLCommand.ExecuteNonQuery
                If res = 0 Then
                    res = 1
                End If
                MYSQLExecuteSQL = res
            End If
        Catch ex As Exception
            MYSQLExecuteSQL = Nothing
        End Try
    End Function


    Public Function MYSQLExecuteSQLReader(ByVal sql As String) As String

        Try
            MYSQLExecuteSQLReader = Nothing
            If isMYSQLConnect() Then
                MYSQLCommand = New MySqlCommand(sql, MyADOConnection)
                MYSQLCommand.Transaction = MYSQLTransaction
                MYSQLCommand.CommandTimeout = 0
                Dim dr As MySqlDataReader
                dr = MYSQLCommand.ExecuteReader
                If (dr.Read) Then
                    MYSQLExecuteSQLReader = dr.Item(0)
                Else
                    MYSQLExecuteSQLReader = Nothing
                End If
            End If

        Catch ex As Exception
            MYSQLExecuteSQLReader = Nothing
        End Try
    End Function
End Class