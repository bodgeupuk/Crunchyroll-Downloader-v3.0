﻿Imports Gecko.Events
Imports Gecko
Imports System.IO
Imports Gecko.Cache
Imports System.ComponentModel
Imports System.Threading
Imports System.Net
Imports System.Net.WebUtility
Imports System.IO.Compression
Imports System.Text

Public Class GeckoFX
    Public keks As String = Nothing
    'Public c As Boolean = True
    Dim t As Thread
    Dim ScanTrue As Boolean = False
    Private Sub GeckoWebBrowser1_DocumentCompleted(sender As Object, e As EventArgs) Handles WebBrowser1.DocumentCompleted
        'MsgBox("loaded!")
        If ScanTrue = False Then
            Button2.Enabled = True
        End If
        If Main.LoginOnly = "US_UnBlock" Then
            Main.LoginOnly = "US_UnBlock_Wait"
            If CBool(InStr(WebBrowser1.Document.Body.OuterHtml, "waiting for reCAPTCHA . . .")) Then
                Pause(4)
                Main.LoginOnly = "US_UnBlock"
            Else
                Try

                    If CBool(InStr(WebBrowser1.Document.Uri, "https://api.crunchyroll.com/login.0.json")) Then
                        Main.LoginOnly = "US_UnBlock_Finsihed"
                    Else
                        Dim cookieGrapp As String = WebBrowser1.Document.Body.OuterHtml '.Replace(vbTab, "").Replace(" ", "")
                        If Main.Debug2 = True Then
                            MsgBox(cookieGrapp)
                        End If
                        Dim cookieGrapp2() As String = cookieGrapp.Split(New String() {"<a class=" + Chr(34) + "cookie" + Chr(34) + ">"}, System.StringSplitOptions.RemoveEmptyEntries)
                        Dim cookieGrapp3() As String = cookieGrapp2(1).Split(New String() {"</a>"}, System.StringSplitOptions.RemoveEmptyEntries)
                        keks = cookieGrapp3(0)
                        If Main.Debug2 = True Then
                            MsgBox(keks)
                        End If

                        WebBrowser1.Navigate("https://www.crunchyroll.com/logout")
                        Pause(5)
                        WebBrowser1.Navigate("javascript:document.cookie =" + Chr(34) + "session_id=" + keks + "; expires=Thu, 04 Jan 2022 00:00:00 UTC; path=/;" + Chr(34) + ";")
                        Pause(1)
                        WebBrowser1.Navigate("javascript:document.cookie =" + Chr(34) + "sess_id=" + keks + "; expires=Thu, 04 Jan 2022 00:00:00 UTC; path=/;" + Chr(34) + ";")
                        Pause(1)
                        WebBrowser1.Navigate("https://www.crunchyroll.com/")
                        Main.LoginOnly = "US_UnBlock_Finsihed"
                    End If

                Catch ex As Exception
                    If Main.LoginOnly = "US_UnBlock_Finsihed" And Main.UserBowser = False Then
                        Me.Close()
                    End If
                    'MsgBox(ex.ToString)
                End Try

            End If
        ElseIf Main.LoginOnly = "US_UnBlock_Finsihed" And Main.UserBowser = False Then
            Me.Close()
        Else


            If CBool(InStr(WebBrowser1.Url.ToString, "crunchyroll.com")) Then

                If Main.b = False Then
                    Try
                        If WebBrowser1.Url.ToString = "https://www.crunchyroll.com/" Then
                            Main.b = True
                        ElseIf WebBrowser1.Url.ToString = "https://www.crunchyroll.com/en-gb" Then
                            Main.b = True
                        ElseIf WebBrowser1.Url.ToString = "https://www.crunchyroll.com/es" Then
                            Main.b = True
                        ElseIf WebBrowser1.Url.ToString = "https://www.crunchyroll.com/es-es" Then
                            Main.b = True
                        ElseIf WebBrowser1.Url.ToString = "https://www.crunchyroll.com/pt-br" Then
                            Main.b = True
                        ElseIf WebBrowser1.Url.ToString = "https://www.crunchyroll.com/pt-pt" Then
                            Main.b = True
                        ElseIf WebBrowser1.Url.ToString = "https://www.crunchyroll.com/fr" Then
                            Main.b = True
                        ElseIf WebBrowser1.Url.ToString = "https://www.crunchyroll.com/de" Then
                            Main.b = True
                        ElseIf WebBrowser1.Url.ToString = "https://www.crunchyroll.com/ar" Then
                            Main.b = True
                        ElseIf WebBrowser1.Url.ToString = "https://www.crunchyroll.com/it" Then
                            Main.b = True
                        ElseIf WebBrowser1.Url.ToString = "https://www.crunchyroll.com/ru" Then
                            Main.b = True
                        ElseIf CBool(InStr(WebBrowser1.Document.Body.OuterHtml, "hardsub_lang")) Then
                            Main.WebbrowserURL = WebBrowser1.Url.ToString
                            Main.WebbrowserText = WebBrowser1.Document.Body.OuterHtml
                            Main.WebbrowserTitle = WebBrowser1.DocumentTitle
                            Main.WebbrowserHeadText = WebBrowser1.Document.Head.InnerHtml
                            Main.b = True
                            If Main.d = False Then
                                Main.d = True
                                t = New Thread(AddressOf Main.DownloadSubsOnly)
                                t.Priority = ThreadPriority.Normal
                                t.IsBackground = True
                                t.Start()
                            Else
                                t = New Thread(AddressOf Main.GrappURL)
                                t.Priority = ThreadPriority.Normal
                                t.IsBackground = True
                                t.Start()
                            End If



                        ElseIf CBool(InStr(WebBrowser1.Document.Body.OuterHtml, "season-dropdown content-menu block")) Then
                            Main.b = True
                            Anime_Add.textBox2.Text = "Name of the Anime"
                            Main.WebbrowserURL = WebBrowser1.Url.ToString
                            Main.WebbrowserText = WebBrowser1.Document.Body.OuterHtml
                            Main.WebbrowserTitle = WebBrowser1.DocumentTitle
                            Main.WebbrowserHeadText = WebBrowser1.Document.Head.InnerHtml
                            If Main.d = False Then
                                Main.d = True
                                Main.SeasonDropdownGrappSubs()
                                Einstellungen.StatusLabel.Text = "Status: Multi Download detected!"
                            Else
                                Main.SeasonDropdownGrapp()
                            End If
                        ElseIf CBool(InStr(WebBrowser1.Document.Body.OuterHtml, "wrapper container-shadow hover-classes")) Then
                            Main.b = True
                            Anime_Add.textBox2.Text = "Name of the Anime"
                            Main.WebbrowserURL = WebBrowser1.Url.ToString
                            Main.WebbrowserText = WebBrowser1.Document.Body.OuterHtml
                            Main.WebbrowserTitle = WebBrowser1.DocumentTitle
                            Main.WebbrowserHeadText = WebBrowser1.Document.Head.InnerHtml
                            If Main.d = False Then
                                Main.d = True
                                Main.MassGrappSubs()
                                Einstellungen.StatusLabel.Text = "Status: Multi Download detected!"

                            Else
                                Main.MassGrapp()
                            End If
                        Else
                            Main.b = True
                            MsgBox(Main.No_Stream, MsgBoxStyle.OkOnly)
                            Anime_Add.StatusLabel.Text = "Status: idle"
                        End If
                    Catch ex As Exception
                        MsgBox(ex.ToString)
                        Anime_Add.StatusLabel.Text = "Status: idle"
                    End Try
                ElseIf Main.c = False Then
                    If CBool(InStr(WebBrowser1.Document.Body.OuterHtml, "hardsub_lang")) Then
                        Main.c = True
                        Main.WebbrowserURL = WebBrowser1.Url.ToString
                        Main.WebbrowserText = WebBrowser1.Document.Body.OuterHtml
                        Main.WebbrowserTitle = WebBrowser1.DocumentTitle
                        Main.WebbrowserHeadText = WebBrowser1.Document.Head.InnerHtml
                        'SoftSub.DownloadSubs()
                        Me.Close()
                    End If


                End If
                If Main.UserBowser = False Then
                    Try
                        Main.WebbrowserURL = WebBrowser1.Url.ToString
                        Main.WebbrowserText = WebBrowser1.Document.Body.OuterHtml
                        Main.WebbrowserTitle = WebBrowser1.DocumentTitle
                        Main.WebbrowserHeadText = WebBrowser1.Document.Head.InnerHtml
                    Catch ex As Exception
                    End Try

                    Me.Close()
                End If
                'ElseIf CBool(InStr(WebBrowser1.Url.ToString, "https://www.anime-on-demand.de/anime/")) Then

                'MsgBox(Main.WebbrowserSoftSubURL)
                ' Anime_Add.StatusLabel.Text = 
            ElseIf CBool(InStr(WebBrowser1.Url.ToString, "funimation.com")) Then
                If Main.b = False Then

                    If InStr(WebBrowser1.Document.Body.OuterHtml, My.Resources.Funimation_Player_ID) Then
                        Main.WebbrowserURL = WebBrowser1.Url.ToString
                        Main.WebbrowserText = WebBrowser1.Document.Body.OuterHtml
                        Main.WebbrowserTitle = WebBrowser1.DocumentTitle
                        Main.WebbrowserHeadText = WebBrowser1.Document.Head.InnerHtml
                        Main.WebbrowserCookie = WebBrowser1.Document.Cookie
                        Main.b = True

                        t = New Thread(AddressOf Main.Funitmation_Grapp)
                        t.Priority = ThreadPriority.Normal
                        t.IsBackground = True
                        t.Start()

                    Else
                        Main.Text = "Status: no video found"
                        Anime_Add.StatusLabel.Text = "fail?"
                    End If
                End If
            ElseIf CBool(InStr(WebBrowser1.Url.ToString, "anime-on-demand.de")) Then
                If Main.b = False Then
                    Main.b = True
                    Main.WebbrowserURL = WebBrowser1.Url.ToString
                    Main.WebbrowserText = WebBrowser1.Document.Body.OuterHtml
                    Main.WebbrowserTitle = WebBrowser1.DocumentTitle
                    Anime_Add.AoDHTML = WebBrowser1.Document.Body.OuterHtml
                    Exit Sub


                End If

            Else
                If Main.b = False Then
                    Main.b = True
                    Main.UserBowser = True
                    For i As Integer = 20 To 0 Step -1
                        Pause(1)
                        Anime_Add.StatusLabel.Text = "Status: scanning network traffic " + Math.Abs(i).ToString
                    Next

                    Main.WebbrowserURL = WebBrowser1.Url.ToString
                    Main.WebbrowserText = WebBrowser1.Document.Body.OuterHtml
                    Main.WebbrowserTitle = WebBrowser1.DocumentTitle


                    Main.LogBrowserData = False
                    GeckoPreferences.Default("logging.config.LOG_FILE") = "gecko-network.txt"
                    GeckoPreferences.Default("logging.nsHttp") = 0



                    Dim SubtitleName As String = Main.WebbrowserTitle.Replace(" - Watch on VRV", "").Replace("Free Streaming", "").Replace("Tubi", "")
                    SubtitleName = Main.RemoveExtraSpaces(String.Join(" ", SubtitleName.Split(Main.invalids, StringSplitOptions.RemoveEmptyEntries)).TrimEnd("."c)) 'System.Text.RegularExpressions.Regex.Replace(SubtitleName, "[^\w\\-]", " "))
                    Dim SubtitlePfad As String = Main.Pfad + "\" + SubtitleName

                    Dim logFileStream As FileStream = New FileStream(Main.GeckoLogFile, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite)
                    Dim logFileReader As StreamReader = New StreamReader(logFileStream)

                    Dim Requests As New List(Of String)
                    Dim LogText As String = Nothing
                    Dim line As String = logFileReader.ReadLine

                    While (line IsNot Nothing)
                        line = logFileReader.ReadLine

                        If InStr(line, "I/nsHttp http request [") Then
                            For i As Integer = 0 To 25
                                line = logFileReader.ReadLine
                                If InStr(line, "I/nsHttp ]") Then
                                    Exit For
                                Else
                                    LogText = LogText + line + vbNewLine
                                End If

                            Next
                            Requests.Add(LogText)
                            LogText = Nothing
                        End If

                    End While


                    logFileReader.Dispose()
                    logFileStream.Dispose()
                    'Dim Requests() As String = LogText.Split(New String() {"I/nsHttp http request ["}, System.StringSplitOptions.RemoveEmptyEntries)
                    Dim client0 As New WebClient
                    client0.Encoding = Encoding.UTF8
                    If WebBrowser1.Document.Cookie = Nothing Then
                    Else
                        client0.Headers.Add(HttpRequestHeader.Cookie, WebBrowser1.Document.Cookie)
                    End If
                    For i As Integer = 1 To Requests.Count - 1
                        'Dim Requests2() As String = Requests.Item(i).Split(New String() {"I/nsHttp ]"}, System.StringSplitOptions.RemoveEmptyEntries)

                        If InStr(Requests.Item(i), "  GET ") Then


                            Dim URLPath() As String = Requests.Item(i).Split(New String() {"  GET "}, System.StringSplitOptions.RemoveEmptyEntries)
                            Dim URLPath2() As String = URLPath(1).Split(New String() {" HTTP/"}, System.StringSplitOptions.RemoveEmptyEntries)
                            Dim URLHost1() As String = Requests.Item(i).Split(New String() {" Host: "}, System.StringSplitOptions.RemoveEmptyEntries)
                            Dim URLHost2() As String = URLHost1(1).Split(New String() {vbNewLine}, System.StringSplitOptions.RemoveEmptyEntries)

                            Dim RequestURL As String = "https://" + URLHost2(0) + URLPath2(0)



                            If InStr(RequestURL, ".m3u8") Then

                                Dim str0 As String = client0.DownloadString(RequestURL)
                                If InStr(str0, "#EXTM3U") Then
                                    Main.m3u8List.Add(RequestURL)

                                End If
                            ElseIf InStr(RequestURL, ".mpd") Then
                                Main.mpdList.Add(RequestURL)

                            ElseIf InStr(RequestURL, ".txt") Then
                                Main.txtList.Add(RequestURL)
                                client0.DownloadFileAsync(New Uri(RequestURL), SubtitlePfad + ".txt")
                            ElseIf InStr(RequestURL, ".vtt") Then
                                Main.txtList.Add(RequestURL)
                                client0.DownloadFileAsync(New Uri(RequestURL), SubtitlePfad + ".vtt")
                            ElseIf InStr(RequestURL, ".srt") Then
                                Main.txtList.Add(RequestURL)
                                client0.DownloadFileAsync(New Uri(RequestURL), SubtitlePfad + ".srt")
                            ElseIf InStr(RequestURL, ".ass") Then
                                Main.txtList.Add(RequestURL)
                                client0.DownloadFileAsync(New Uri(RequestURL), SubtitlePfad + ".ass")
                            ElseIf InStr(RequestURL, ".ssa") Then
                                Main.txtList.Add(RequestURL)
                                client0.DownloadFileAsync(New Uri(RequestURL), SubtitlePfad + ".ssa")
                            ElseIf InStr(RequestURL, ".dfxp") Then
                                Main.txtList.Add(RequestURL)
                                client0.DownloadFileAsync(New Uri(RequestURL), SubtitlePfad + ".dfxp")
                            End If
                        End If
                    Next

                    If Main.m3u8List.Count > 0 Then
                        Main.NonCR_URL = Main.m3u8List.Item(0)
                        Main.FFMPEG_Reso(Main.NonCR_URL)
                        t = New Thread(AddressOf Main.Grapp_non_CR)
                        t.Priority = ThreadPriority.Normal
                        t.IsBackground = True
                        t.Start()


                    ElseIf Main.mpdList.Count > 0 Then
                        Main.NonCR_URL = Main.mpdList.Item(0)
                        Main.FFMPEG_Reso(Main.NonCR_URL)
                        t = New Thread(AddressOf Main.Grapp_non_CR)
                        t.Priority = ThreadPriority.Normal
                        t.IsBackground = True
                        t.Start()

                    Else
                        Anime_Add.StatusLabel.Text = "Status: no m3u8 found, analyzing HTML content"
                        WebBrowser1.Navigate("view-source:" + Main.WebbrowserURL)
                        Pause(3)
                        If CBool(InStr(WebBrowser1.Document.Body.OuterHtml, ".m3u8")) Then
#Region "m3u8 suche"
                            Main.WebbrowserText = UrlDecode(WebBrowser1.Document.Body.OuterHtml)
                            If InStr(Main.WebbrowserText, ".m3u8") Then 'm3u8?
                            Else
                                Anime_Add.StatusLabel.Text = "Status: no m3u8 found"
                                Main.UserBowser = False
                                Me.Close()
                                Exit Sub
                            End If
                            Dim ii As Integer = 0
                            Dim Video_URI_Master As String = Nothing
                            Dim Video_URI_Master_Split1 As String() = Main.WebbrowserText.Split(New String() {".m3u8"}, System.StringSplitOptions.RemoveEmptyEntries) 'm3u8?
                            Dim m3u8Link As String = Nothing
                            For i As Integer = 0 To Video_URI_Master_Split1.Count - 2
                                Dim Video_URI_Master_Split_Top As String() = Video_URI_Master_Split1(i).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                                Dim Video_URI_Master_Split_Bottom As String() = Video_URI_Master_Split1(i + 1).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
                                m3u8Link = Video_URI_Master_Split_Top(Video_URI_Master_Split_Top.Count - 1) + ".m3u8" + Video_URI_Master_Split_Bottom(0) 'm3u8?
                                Exit For
                            Next
                            m3u8Link = m3u8Link.Replace("&amp;", "&").Replace("/u0026", "&").Replace("\u002F", "/")
                            Dim req As WebRequest
                            Dim res As WebResponse

                            req = WebRequest.Create(m3u8Link)

                            Try
                                res = req.GetResponse()
                                Dim ResponseStreamReader As StreamReader = New StreamReader(res.GetResponseStream)
                                Dim ResponseStreamString As String = ResponseStreamReader.ReadToEnd
                                If InStr(ResponseStreamString, "drm") Then
                                    Anime_Add.StatusLabel.Text = "Status: m3u8 found, but looks like it is DRM protected"
                                Else
                                    Anime_Add.StatusLabel.Text = "Status: m3u8 found, looks good"
                                    Pause(1)
                                    Main.NonCR_URL = m3u8Link
                                    t = New Thread(AddressOf Main.Grapp_non_CR)
                                    t.Priority = ThreadPriority.Normal
                                    t.IsBackground = True
                                    t.Start()
                                    Me.Close()
                                End If
                            Catch ee As WebException
                                Anime_Add.StatusLabel.Text = "Status: error while loading m3u8"
                                Main.UserBowser = False
                                Me.Close()
                                Exit Sub
                                ' URL doesn't exists
                            Catch eee As Exception
                                'MsgBox(eee.ToString + vbNewLine + m3u8Link)
                            End Try
#End Region
                        End If
                        Anime_Add.StatusLabel.Text = "Status: idle"
                        Me.Close()
                        Main.UserBowser = False
                    End If
                End If
            End If
        End If
            If Main.UserBowser = False Then
                If Main.b = True Then
                    Anime_Add.StatusLabel.Text = "Status: idle"
                    Me.Close()
                End If
            End If
        'End If
    End Sub

    Private Sub GeckoFX_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Me.Width > My.Computer.Screen.Bounds.Width Then
            Me.Width = My.Computer.Screen.Bounds.Width
            WebBrowser1.Width = Me.Size.Width - 15 ', Me.Size.Height - 69)
            WebBrowser1.Location = New Point(0, 30)
            TextBox1.Width = My.Computer.Screen.Bounds.Width - 435

        End If

        If Me.Size.Height > My.Computer.Screen.Bounds.Height Then
            Me.Height = My.Computer.Screen.Bounds.Height
            WebBrowser1.Height = Me.Size.Height - 69
            WebBrowser1.Location = New Point(0, 30)
        End If

        If Main.Debug2 = True Then
            Debug_Mode.Show()
            Debug_Mode.Location = New Point(Me.Location.X + Me.Width - 15, Me.Location.Y)
        End If

        If WebBrowser1.Url.ToString = "about:blank" Then
            If Main.LoginOnly = "US_UnBlock" Then
                WebBrowser1.Navigate("https://www.crunchyroll.com/login")
            Else
                WebBrowser1.Navigate(Main.Startseite)
            End If
        End If
        Try
            Me.Icon = My.Resources.icon
        Catch ex As Exception

        End Try

        Main.UserBowser = True
        'Main.Pause(15)
        'For ii As Integer = 19 To 46
        '    WebBrowser1.Navigate("https://proxer.me/read/22459/" + ii.ToString + "/en/1")
        '    Main.Pause(15)
        '    Main.WebbrowserURL = WebBrowser1.Url.ToString
        '    Dim NameDLFinal As String = Nothing
        '    Dim NameDL As String() = WebBrowser1.Document.Body.OuterHtml.Split(New String() {"<div id=" + Chr(34) + "breadcrumb" + Chr(34) + ">"}, System.StringSplitOptions.RemoveEmptyEntries)
        '    Dim NameDL2 As String() = NameDL(1).Split(New String() {"<div>"}, System.StringSplitOptions.RemoveEmptyEntries)
        '    Dim NameDL3 As String() = NameDL2(0).Split(New String() {Chr(34) + "true" + Chr(34) + ">"}, System.StringSplitOptions.RemoveEmptyEntries)
        '    For i As Integer = 0 To NameDL3.Count - 1
        '        If InStr(NameDL3(i), "</a>") Then
        '            Dim NameDL4 As String() = NameDL3(i).Split(New String() {"</a>"}, System.StringSplitOptions.RemoveEmptyEntries)
        '            If NameDLFinal = Nothing Then
        '                NameDLFinal = NameDL4(0)
        '            Else
        '                NameDLFinal = NameDLFinal + " " + NameDL4(0)
        '            End If
        '        End If
        '    Next
        '    NameDLFinal = System.Text.RegularExpressions.Regex.Replace(NameDLFinal, "[^\w\\-]", " ")
        '    If Main.Debug2 = True Then
        '        MsgBox(NameDLFinal)
        '    End If
        '    Dim SiteData As String() = WebBrowser1.Document.Body.OuterHtml.Split(New String() {"var pages ="}, System.StringSplitOptions.RemoveEmptyEntries)
        '    Dim SiteData2 As String() = SiteData(1).Split(New String() {"</script>"}, System.StringSplitOptions.RemoveEmptyEntries)
        '    Dim ImageNumbers As String() = SiteData2(0).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
        '    Dim ImageList As New List(Of String)
        '    Dim ImageListString As String = Nothing
        '    For i As Integer = 0 To ImageNumbers.Count - 1
        '        If InStr(ImageNumbers(i), ".jpg") Then
        '            ImageList.Add(ImageNumbers(i).Replace(vbNewLine, ""))
        '            ImageListString = ImageListString + vbNewLine + ImageNumbers(i).Replace(vbNewLine, "")
        '        ElseIf InStr(ImageNumbers(i), ".png") Then
        '            ImageList.Add(ImageNumbers(i).Replace(vbNewLine, ""))
        '            ImageListString = ImageListString + vbNewLine + ImageNumbers(i).Replace(vbNewLine, "")
        '        End If
        '    Next
        '    If Main.Debug2 = True Then
        '        MsgBox(ImageListString)
        '    End If
        '    Dim BaseURL As String() = SiteData2(0).Split(New String() {"var serverurl = '"}, System.StringSplitOptions.RemoveEmptyEntries)
        '    Dim BaseURL2 As String() = BaseURL(1).Split(New String() {"';"}, System.StringSplitOptions.RemoveEmptyEntries)
        '    Dim BaseURL3 As String = "https:" + BaseURL2(0)
        '    If Main.Debug2 = True Then
        '        MsgBox(BaseURL3)
        '    End If
        '    AsyncWorkerX.RunAsync(AddressOf Main.DownloadMangaPages, BaseURL3, ImageList, Main.RemoveExtraSpaces(NameDLFinal))
        'Next
    End Sub

    Private Sub GeckoFX_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        Main.UserBowser = False
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'Main.WebbrowserURL = WebBrowser1.Url.ToString
        'Main.WebbrowserText = WebBrowser1.Document.Body.OuterHtml
        'Main.WebbrowserTitle = WebBrowser1.DocumentTitle
        'Main.GrappURL()
        Try
            My.Computer.Clipboard.SetText(WebBrowser1.Url.ToString)
            'My.Computer.Clipboard.SetText(WebBrowser1.Document.Cookie)

            MsgBox("copied: " + Chr(34) + WebBrowser1.Url.ToString + Chr(34))
        Catch ex As Exception
        End Try

        'MsgBox(WebBrowser1.Document.Cookie)
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        Try
            If e.KeyCode = Keys.Return Then
                e.SuppressKeyPress = True
                WebBrowser1.Navigate(TextBox1.Text)
            End If

        Catch ex As Exception
            MsgBox("Error in URL", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub WebBrowser1_DocumentTitleChanged(sender As Object, e As EventArgs) Handles WebBrowser1.DocumentTitleChanged
        Try
            TextBox1.Text = WebBrowser1.Url.ToString
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If InStr(WebBrowser1.Url.ToString, "https://proxer.me/read/") Then
            Main.WebbrowserURL = WebBrowser1.Url.ToString
            Dim NameDLFinal As String = Nothing
            Dim NameDL As String() = WebBrowser1.Document.Body.OuterHtml.Split(New String() {"<div id=" + Chr(34) + "breadcrumb" + Chr(34) + ">"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim NameDL2 As String() = NameDL(1).Split(New String() {"<div>"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim NameDL3 As String() = NameDL2(0).Split(New String() {Chr(34) + "true" + Chr(34) + ">"}, System.StringSplitOptions.RemoveEmptyEntries)
            For i As Integer = 0 To NameDL3.Count - 1
                If InStr(NameDL3(i), "</a>") Then
                    Dim NameDL4 As String() = NameDL3(i).Split(New String() {"</a>"}, System.StringSplitOptions.RemoveEmptyEntries)
                    If NameDLFinal = Nothing Then
                        NameDLFinal = NameDL4(0)
                    Else
                        NameDLFinal = NameDLFinal + " " + NameDL4(0)
                    End If
                End If
            Next
            NameDLFinal = String.Join(" ", NameDLFinal.Split(Main.invalids, StringSplitOptions.RemoveEmptyEntries)).TrimEnd("."c) ''System.Text.RegularExpressions.Regex.Replace(NameDLFinal, "[^\w\\-]", " ")
            If Main.Debug2 = True Then
                MsgBox(NameDLFinal)
            End If
            Dim SiteData As String() = WebBrowser1.Document.Body.OuterHtml.Split(New String() {"var pages ="}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim SiteData2 As String() = SiteData(1).Split(New String() {"</script>"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim ImageNumbers As String() = SiteData2(0).Split(New String() {Chr(34)}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim ImageList As New List(Of String)
            Dim ImageListString As String = Nothing
            For i As Integer = 0 To ImageNumbers.Count - 1
                If InStr(ImageNumbers(i), ".jpg") Then
                    ImageList.Add(ImageNumbers(i).Replace(vbNewLine, ""))
                    ImageListString = ImageListString + vbNewLine + ImageNumbers(i).Replace(vbNewLine, "")
                ElseIf InStr(ImageNumbers(i), ".png") Then
                    ImageList.Add(ImageNumbers(i).Replace(vbNewLine, ""))
                    ImageListString = ImageListString + vbNewLine + ImageNumbers(i).Replace(vbNewLine, "")
                End If
            Next
            If Main.Debug2 = True Then
                MsgBox(ImageListString)
            End If
            Dim BaseURL As String() = SiteData2(0).Split(New String() {"var serverurl = '"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim BaseURL2 As String() = BaseURL(1).Split(New String() {"';"}, System.StringSplitOptions.RemoveEmptyEntries)
            Dim BaseURL3 As String = "https:" + BaseURL2(0)
            If Main.Debug2 = True Then
                MsgBox(BaseURL3)
            End If
            'AsyncWorkerX.RunAsync(AddressOf Main.DownloadMangaPages, BaseURL3, ImageList, Main.RemoveExtraSpaces(NameDLFinal))
            Dim Thumbnail As String = BaseURL3 + ImageList(0)
            Main.MangaListItemAdd(Main.RemoveExtraSpaces(NameDLFinal), Thumbnail, BaseURL3, ImageList)

        ElseIf InStr(WebBrowser1.Url.ToString, "cr-cookie-ui.php") Then
            MsgBox(WebBrowser1.Document.Body.InnerHtml)

        Else
            Try

                Main.m3u8List.Clear()
                Main.mpdList.Clear()
                Main.txtList.Clear()
                Button2.Enabled = False
                ScanTrue = True

                GeckoPreferences.Default("logging.config.LOG_FILE") = "gecko-network.txt"
                GeckoPreferences.Default("logging.nsHttp") = 3
                Main.LogBrowserData = True
                Dim FileLocation As DirectoryInfo = New DirectoryInfo(Application.StartupPath)
                Dim CurrentFile As String = Nothing
                For Each File In FileLocation.GetFiles()
                    If InStr(File.FullName, "gecko-network.txt") Then
                        CurrentFile = File.FullName
                        Exit For
                    End If
                Next
                Dim logFileStream As FileStream = New FileStream(CurrentFile, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite)
                Dim logFileReader As StreamReader = New StreamReader(logFileStream)
                logFileStream.SetLength(0)
                'WebBrowser1.Navigate(TextBox1.Text)
                Main.b = True
                Main.UserBowser = True
                For i As Integer = 20 To 0 Step -1
                    Pause(1)
                    Button2.Text = "network scan is in progess " + Math.Abs(i).ToString
                Next

                Main.LogBrowserData = False
                GeckoPreferences.Default("logging.config.LOG_FILE") = "gecko-network.txt"
                GeckoPreferences.Default("logging.nsHttp") = 0

                Main.WebbrowserURL = WebBrowser1.Url.ToString
                Main.WebbrowserText = WebBrowser1.Document.Body.OuterHtml
                Main.WebbrowserTitle = WebBrowser1.DocumentTitle
                Main.WebbrowserCookie = WebBrowser1.Document.Cookie

                Dim Requests As New List(Of String)
                Dim LogText As String = Nothing
                Dim line As String = logFileReader.ReadLine

                While (line IsNot Nothing)
                    line = logFileReader.ReadLine

                    If InStr(line, "I/nsHttp http request [") Then
                        For i As Integer = 0 To 25
                            line = logFileReader.ReadLine
                            If InStr(line, "I/nsHttp ]") Then
                                Exit For
                            Else
                                LogText = LogText + line + vbNewLine
                            End If

                        Next
                        Requests.Add(LogText)
                        LogText = Nothing
                    End If

                End While


                logFileReader.Dispose()
                logFileStream.Dispose()

                Dim client0 As New WebClient
                client0.Encoding = Encoding.UTF8
                If WebBrowser1.Document.Cookie = Nothing Then
                Else
                    client0.Headers.Add(HttpRequestHeader.Cookie, WebBrowser1.Document.Cookie)
                End If
                For i As Integer = 1 To Requests.Count - 1

                    If InStr(Requests.Item(i), "  GET ") Then


                        Dim URLPath() As String = Requests.Item(i).Split(New String() {"  GET "}, System.StringSplitOptions.RemoveEmptyEntries)
                        Dim URLPath2() As String = URLPath(1).Split(New String() {" HTTP/"}, System.StringSplitOptions.RemoveEmptyEntries)
                        Dim URLHost1() As String = Requests.Item(i).Split(New String() {" Host: "}, System.StringSplitOptions.RemoveEmptyEntries)
                        Dim URLHost2() As String = URLHost1(1).Split(New String() {vbNewLine}, System.StringSplitOptions.RemoveEmptyEntries)

                        Dim RequestURL As String = "https://" + URLHost2(0) + URLPath2(0)



                        If InStr(RequestURL, ".m3u8") Then

                            Dim str0 As String = client0.DownloadString(RequestURL)
                            If InStr(str0, "#EXTM3U") Then
                                Main.m3u8List.Add(RequestURL)

                            End If
                        ElseIf InStr(RequestURL, ".mpd") Then
                            Main.mpdList.Add(RequestURL)

                        ElseIf InStr(RequestURL, ".txt") Then
                            Main.txtList.Add(RequestURL)
                            ' client0.DownloadFileAsync(New Uri(RequestURL), SubtitlePfad + ".txt")
                        ElseIf InStr(RequestURL, ".vtt") Then
                            Main.txtList.Add(RequestURL)
                            ' client0.DownloadFileAsync(New Uri(RequestURL), SubtitlePfad + ".vtt")
                        ElseIf InStr(RequestURL, ".srt") Then
                            Main.txtList.Add(RequestURL)
                            ' client0.DownloadFileAsync(New Uri(RequestURL), SubtitlePfad + ".srt")
                        ElseIf InStr(RequestURL, ".ass") Then
                            Main.txtList.Add(RequestURL)
                            ' client0.DownloadFileAsync(New Uri(RequestURL), SubtitlePfad + ".ass")
                        ElseIf InStr(RequestURL, ".ssa") Then
                            Main.txtList.Add(RequestURL)
                            'client0.DownloadFileAsync(New Uri(RequestURL), SubtitlePfad + ".ssa")
                        ElseIf InStr(RequestURL, ".dfxp") Then
                            Main.txtList.Add(RequestURL)
                            ' client0.DownloadFileAsync(New Uri(RequestURL), SubtitlePfad + ".dfxp")
                        End If
                    End If
                Next
                Button2.Text = "use network scan dialog"
                network_scan.ShowDialog()
                'If Main.m3u8List.Count > 0 Then
                '    Main.NonCR_URL = Main.m3u8List.Item(0)
                '    Main.FFMPEG_Reso(Main.NonCR_URL)
                '    t = New Thread(AddressOf Main.Grapp_non_CR)
                '    t.Priority = ThreadPriority.Normal
                '    t.IsBackground = True
                '    t.Start()
                '    Button2.Text = "Start network scan"

                'ElseIf Main.mpdList.Count > 0 Then
                '    Main.NonCR_URL = Main.mpdList.Item(0)
                '    Main.FFMPEG_Reso(Main.NonCR_URL)
                '    t = New Thread(AddressOf Main.Grapp_non_CR)
                '    t.Priority = ThreadPriority.Normal
                '    t.IsBackground = True
                '    t.Start()
                '    Button2.Text = "Start network scan"
                'End If


                Button2.Text = "Start network scan"
                ScanTrue = False
                Button2.Enabled = True
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.OkOnly)
                Button2.Enabled = True
                ScanTrue = False
            End Try
        End If
    End Sub

    Private Sub GeckoFX_LocationChanged(sender As Object, e As EventArgs) Handles Me.LocationChanged
        If Main.Debug2 = True Then
            Debug_Mode.Location = New Point(Me.Location.X + Me.Width - 15, Me.Location.Y)
        End If
    End Sub

    Private Sub WebBrowser1_GotFocus(sender As Object, e As EventArgs) Handles WebBrowser1.GotFocus
        'Debug_Mode.ActiveForm = True
    End Sub

    Private Sub WebBrowser1_LostFocus(sender As Object, e As EventArgs) Handles WebBrowser1.LostFocus
        'Debug_Mode.TopMost = False
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If InStr(WebBrowser1.Url.ToString, "funimation.com") Then
            Dim Funimation_List As New List(Of String)
            Dim Funimation_list1() As String = WebBrowser1.Document.Body.OuterHtml.Split(New String() {My.Resources.Funimation_Split_1}, System.StringSplitOptions.RemoveEmptyEntries)

            For i As Integer = 1 To Funimation_list1.Count - 1
                Dim Funimation_list2() As String = Funimation_list1(i).Split(New String() {My.Resources.Funimation_Split_2}, System.StringSplitOptions.RemoveEmptyEntries)
                Funimation_List.Add("https://www.funimation.com" + Funimation_list2(0))
                Main.ListBoxList.Add("https://www.funimation.com" + Funimation_list2(0))
            Next
            MsgBox(Funimation_List.Count.ToString + " episodes added to Download queue")
            'For ii As Integer = 0 To Funimation_List.Count - 1
            '    MsgBox(Funimation_List.Item(ii))
            'Next
        End If
    End Sub
End Class
