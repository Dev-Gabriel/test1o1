Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports INI_DLL


' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class Service
    Inherits System.Web.Services.WebService


    <WebMethod()> _
    Public Function SendMessage(ByVal receiver As String, ByVal msg As String) As Boolean

        Try
            Dim SMS As New SunWebRequest.SendSMS()
            Dim sent As New SunWebRequest.smsResponse()
            sent = SMS.send(receiver, msg)

            If sent.respcode <> 0 Then
                Return sent.sent
            Else
                Return sent.sent
            End If

        Catch ex As Exception
            Return False

        End Try
        'SendMessage = False
        ''''''READ INI------------------------------------
        'Dim var As New variables
        'var = New variables
        'Dim ini As New ReadWriteINI
        'var.strPath = AppDomain.CurrentDomain.BaseDirectory + "TEXTSERVER.ini"
        'var.strServer = ini.readINI("TEXT SERVER", "SERVER", False, var.strPath)
        'var.strDb = ini.readINI("TEXT SERVER", "DBNAME", False, var.strPath)
        'var.strUName = ini.readINI("TEXT SERVER", "USERNAME", False, var.strPath)
        'var.strPass = ini.readINI("TEXT SERVER", "PASSWORD", False, var.strPath)
        ''------------------------------------------------


        ''---INSERT TO SMS DATABASE------------------------------------------
        'Dim sql As String = "INSERT INTO ozekimessageout (receiver,msg,status) " & vbNewLine & _
        '"VALUES ('" & receiver & "','" & msg & "','SEND');" & vbNewLine & _
        '"END;" & vbNewLine & _
        '"DELIMITER ; "
        ''------------------------------------------------------------------

        'Dim db As New clsMySql

        'If db.isMYSQLConnect Then
        '    db.MYSQLClose()
        'End If

        'db.SetConnectionString(var.strServer, var.strDb, var.strUName, var.strPass)

        'If Not db.MYSQLConnect Then
        '    Exit Function
        'End If

        'db.MYSQLExecuteSQL(sql)

        'SendMessage = True


    End Function
End Class
