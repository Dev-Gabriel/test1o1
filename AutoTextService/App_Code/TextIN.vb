Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports INI_DLL


' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class TextIN
    Inherits System.Web.Services.WebService

    <WebMethod()> _
    Public Function InsertIN(ByVal sender As String, ByVal msg As String) As String
        Try
            If Trim(sender) = "" Then
                Return "Please input Sender"
            ElseIf Trim(msg) = "" Then
                Return "Please input msg"
            End If
            Dim var As New variables
            var = New variables
            Dim ini As New ReadWriteINI
            var.strPath = AppDomain.CurrentDomain.BaseDirectory + "TEXTSERVER.ini"
            var.strServer = ini.readINI("TEXT SERVER", "SERVER", False, var.strPath)
            var.strDb = ini.readINI("TEXT SERVER", "DBNAME", False, var.strPath)
            var.strUName = ini.readINI("TEXT SERVER", "USERNAME", False, var.strPath)
            var.strPass = ini.readINI("TEXT SERVER", "PASSWORD", False, var.strPath)
            '------------------------------------------------

            '---LOOKUP TO MFWEBVISMIN------------------------------------------
            Dim Sql As String = "CALL insertData(" & sender & ",'" & msg & "');"
            '------------------------------------------------------------------

            Dim db As New clsMySql

            If db.isMYSQLConnect Then
                db.MYSQLClose()
            End If

            db.SetConnectionString(var.strServer, var.strDb, var.strUName, var.strPass)

            If Not db.MYSQLConnect Then
                Return "Cannot connect to DB"
            End If

            db.MYSQLExecuteSQL(Sql)

            Return ("Message Sent")
        Catch ex As Exception
            Return ex.Message
        End Try
        '''''READ INI------------------------------------

    End Function

    <WebMethod()> _
    Public Function ValidateSenderInfo(ByVal _info As String) As String
        Dim var As New variables
        var = New variables
        Dim ini As New ReadWriteINI
        var.strPath = AppDomain.CurrentDomain.BaseDirectory + "TEXTSERVER.ini"
        var.strServer = ini.readINI("TEXT SERVER", "SERVER", False, var.strPath)
        var.strDb = ini.readINI("TEXT SERVER", "DBNAME", False, var.strPath)
        var.strUName = ini.readINI("TEXT SERVER", "USERNAME", False, var.strPath)
        var.strPass = ini.readINI("TEXT SERVER", "PASSWORD", False, var.strPath)
        '------------------------------------------------

        Dim sql As String
        If Mid(_info, 1, 1) = 1 Then
            '---LOOKUP TO MFWEBVISMIN------------------------------------------
            sql = "SELECT bi.cellNo from mfwebvismin.branch_info as bi " & vbNewLine & _
            "WHERE  bi.bcCode = " & Mid(_info, 2, 3) & ";"
            '------------------------------------------------------------------

        Else
            '---LOOKUP TO MFWEBLUZON------------------------------------------
            sql = "SELECT bi.cellNo from mfwebluzon.branch_info as bi " & vbNewLine & _
            "WHERE  bi.bcCode = " & Mid(_info, 2, 3) & ";"
            '------------------------------------------------------------------

        End If

        Dim ret As String
        Dim db As New clsMySql

        If db.isMYSQLConnect Then
            db.MYSQLClose()
        End If

        db.SetConnectionString(var.strServer, var.strDb, var.strUName, var.strPass)

        If Not db.MYSQLConnect Then
            Return "Cannot connect to DB"
        End If

        ret = db.MYSQLExecuteSQLReader(sql)
       
        Return ret
    End Function

End Class
