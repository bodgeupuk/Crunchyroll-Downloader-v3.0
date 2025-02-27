﻿Imports System.IO
Imports System.Text

Module GetData

#Region "curl"

    Public Function Curl(ByVal Url As String) As String

        'MsgBox(Url)

        Dim exepath As String = Path.Combine(Application.StartupPath, "lib", "curl.exe")
        Dim startinfo As New System.Diagnostics.ProcessStartInfo
        Dim sr As StreamReader
        Dim sr2 As StreamReader
        Dim cmd As String = ""
        If Main.Curl_insecure = True Then
            cmd = "--insecure "
        End If
        cmd = cmd + "--no-alpn -fsSLm 15 -A " + My.Settings.User_Agend.Replace("User-Agent: ", "") + " " + Chr(34) + Url + Chr(34)
        Dim Proc As New Process
        'MsgBox(cmd)
        Dim CurlOutput As String = Nothing
        Dim CurlError As String = Nothing
        ' all parameters required to run the process
        startinfo.FileName = exepath
        startinfo.Arguments = cmd
        startinfo.UseShellExecute = False
        startinfo.WindowStyle = ProcessWindowStyle.Normal
        startinfo.RedirectStandardError = True
        startinfo.RedirectStandardOutput = True
        startinfo.CreateNoWindow = True
        startinfo.StandardOutputEncoding = Encoding.UTF8
        startinfo.StandardErrorEncoding = Encoding.UTF8
        Proc.StartInfo = startinfo
        Proc.Start() ' start the process
        sr = Proc.StandardOutput 'standard error is used by ffmpeg
        sr2 = Proc.StandardError
        'sw = proc.StandardInput

        Dim start, finish, pau As Double
        start = CSng(Microsoft.VisualBasic.DateAndTime.Timer)
        pau = 5
        finish = start + pau

        Do
            CurlOutput = CurlOutput + sr.ReadToEnd
            CurlError = CurlError + sr2.ReadToEnd
            'ffmpegOutput2 = sr.ReadLine
            'Debug.WriteLine(CurlOutput)

        Loop Until Proc.HasExited Or Microsoft.VisualBasic.DateAndTime.Timer < finish


        If CBool(InStr(CurlError, "curl:")) Then
            Debug.WriteLine(CurlError)
            Throw New System.Exception("Error - Getting" + vbNewLine + CurlError)
            Return Nothing
        ElseIf CBool(InStr(CurlOutput, "curl:")) Then
            Debug.WriteLine(CurlOutput)
            Throw New System.Exception("Error - Getting" + vbNewLine + CurlError)
            Return Nothing
        Else
            Return CurlOutput
        End If


    End Function

    Public Function CurlPost(ByVal Url As String, ByVal Cookies As String, ByVal Auth As String, ByVal Post As String, ByVal Sender As String) As String


        Dim exepath As String = Path.Combine(Application.StartupPath, "lib", "curl.exe")

        Dim startinfo As New System.Diagnostics.ProcessStartInfo
        Dim sr As StreamReader
        Dim sr2 As StreamReader


        Dim cmd As String = ""
        If Main.Curl_insecure = True Then
            cmd = "--insecure "
        End If
        cmd = cmd + "--no-alpn -fsSLm 15 -A " + My.Settings.User_Agend.Replace("User-Agent: ", "") + Cookies + Auth + Post + " " + Chr(34) + Url + Chr(34)
        Dim Proc As New Process
        'Debug.WriteLine("CurlPost: " + cmd)
        Dim CurlOutput As String = Nothing
        Dim CurlError As String = Nothing
        ' all parameters required to run the process
        startinfo.FileName = exepath
        startinfo.Arguments = cmd
        startinfo.UseShellExecute = False
        startinfo.WindowStyle = ProcessWindowStyle.Normal
        startinfo.RedirectStandardError = True
        startinfo.RedirectStandardOutput = True
        startinfo.CreateNoWindow = True
        startinfo.StandardOutputEncoding = Encoding.UTF8
        startinfo.StandardErrorEncoding = Encoding.UTF8
        Proc.StartInfo = startinfo
        Proc.Start() ' start the process
        sr = Proc.StandardOutput 'standard error is used by ffmpeg
        sr2 = Proc.StandardError
        'sw = proc.StandardInput
        Dim start, finish, pau As Double
        start = CSng(Microsoft.VisualBasic.DateAndTime.Timer)
        pau = 5
        finish = start + pau

        Do
            CurlOutput = CurlOutput + sr.ReadToEnd
            CurlError = CurlError + sr2.ReadToEnd
            'ffmpegOutput2 = sr.ReadLine
            'Debug.WriteLine(CurlOutput)

        Loop Until Proc.HasExited Or Microsoft.VisualBasic.DateAndTime.Timer < finish

        If CBool(InStr(CurlOutput, "curl:")) = True And CBool(InStr(CurlOutput, "400")) = True Then
            Return CurlOutput
        ElseIf CBool(InStr(CurlError, "curl:")) = True And CBool(InStr(CurlError, "400")) = True Then
            Return CurlError
        ElseIf CBool(InStr(CurlError, "curl:")) Then
            Debug.WriteLine(CurlError)
            Throw New System.Exception("Error - Getting" + Sender + vbNewLine + CurlError)
            Return Nothing

        ElseIf CBool(InStr(CurlOutput, "curl:")) Then
            Debug.WriteLine(CurlOutput)
            Throw New System.Exception("Error - Getting" + Sender + vbNewLine + CurlError)
            Return Nothing
        Else
            Return CurlOutput
        End If

    End Function


    Public Function CurlAuthNew(ByVal Url As String, ByVal Cookies As String, ByVal Auth As String, Optional ByVal Test As Boolean = False) As String
        If Test = True Then
            Throw New System.Exception("Error - Getting" + vbNewLine + "Test")

        End If


        Dim exepath As String = Path.Combine(Application.StartupPath, "lib", "curl.exe")

        Dim startinfo As New System.Diagnostics.ProcessStartInfo
        Dim sr As StreamReader
        Dim sr2 As StreamReader



        Dim cmd As String = ""
        If Main.Curl_insecure = True Then
            cmd = "--insecure "
        End If
        cmd = cmd + "--no-alpn -fsSLm 15 -A " + My.Settings.User_Agend.Replace("User-Agent: ", "") + Cookies + Auth + " " + Chr(34) + Url + Chr(34)
        Dim Proc As New Process
        'MsgBox(cmd)
        Dim CurlOutput As String = Nothing
        Dim CurlError As String = Nothing
        ' all parameters required to run the process
        startinfo.FileName = exepath
        startinfo.Arguments = cmd
        startinfo.UseShellExecute = False
        startinfo.WindowStyle = ProcessWindowStyle.Normal
        startinfo.RedirectStandardError = True
        startinfo.RedirectStandardOutput = True
        startinfo.CreateNoWindow = True
        startinfo.StandardOutputEncoding = Encoding.UTF8
        startinfo.StandardErrorEncoding = Encoding.UTF8
        Proc.StartInfo = startinfo
        Proc.Start() ' start the process
        sr = Proc.StandardOutput 'standard error is used by ffmpeg
        sr2 = Proc.StandardError
        'sw = proc.StandardInput

        Dim start, finish, pau As Double
        start = CSng(Microsoft.VisualBasic.DateAndTime.Timer)
        pau = 5
        finish = start + pau

        Do
            CurlOutput = CurlOutput + sr.ReadToEnd
            CurlError = CurlError + sr2.ReadToEnd
            'ffmpegOutput2 = sr.ReadLine
            'Debug.WriteLine(CurlOutput)

        Loop Until Proc.HasExited Or Microsoft.VisualBasic.DateAndTime.Timer < finish



        If CBool(InStr(CurlError, "curl:")) Then
            Debug.WriteLine(CurlError)
            Throw New System.Exception("Error - Getting" + vbNewLine + CurlError)
            Return Nothing
        ElseIf CBool(InStr(CurlOutput, "curl:")) Then
            Debug.WriteLine(CurlOutput)
            Throw New System.Exception("Error - Getting" + vbNewLine + CurlError)
            Return Nothing
        Else
            Return CurlOutput
        End If

    End Function




#End Region


    Function FindExistingVideo(ByVal rootPath As String, ByVal searchString As String, ByVal original As String) As String
        Dim Tracks As Integer = 0
        Dim File As String = original
        ' Durchsuchen aller Dateien im angegebenen Verzeichnis und seinen Unterverzeichnissen
        For Each filePath As String In Directory.GetFiles(rootPath, "*", SearchOption.AllDirectories)
            ' Überprüfen, ob der Dateiname den gesuchten Text enthält
            If Path.GetFileName(filePath).Contains(searchString) Then

                If FFMPEG_Audio(filePath) > Tracks Then
                    File = filePath
                End If

            End If
        Next


        Return File
    End Function




End Module
