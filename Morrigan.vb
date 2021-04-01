Imports System.Speech
Imports System.Speech.Synthesis
Imports System.Threading
Imports System.IO

Public Class Morrigan
    Private WithEvents Oracle As New Recognition.SpeechRecognitionEngine
    Dim QuestionEvent As String
    Dim synth As New Synthesis.SpeechSynthesizer
    Private Declare Function FindWindow Lib "user32.dll" Alias "FindWindowA" (ByVal IpClassName As String, ByVal IpWindowName As String) As Int32
    Private Declare Function ShowWindow Lib "user32.dll" (ByVal hwnd As IntPtr, ByVal nCmdShow As Int32) As Int32
    Private Const SW_HIDE As Int32 = 0
    Private Const SW_RESTORE As Int32 = 9
    Private Declare Function SetWindowPos Lib "user32" (ByVal hwnd As Integer, ByVal hWndInsertAfter As Integer, ByVal x As Integer, ByVal y As Integer, ByVal cx As Integer, ByVal cy As Integer, ByVal wFlags As Integer) As Integer
    Const SWP_HIDEWINDOW = &H80
    Const SWP_SHOWWINDOW = &H40

    Private Sub DesktopIconsHide()
        Dim hwnd As IntPtr
        hwnd = FindWindow(vbNullString, "Program Manager")
        If Not hwnd = 0 Then
            ShowWindow(hwnd, SW_HIDE)
        End If
    End Sub

    Private Sub DesktopIconShow()
        Dim hwnd As IntPtr
        hwnd = FindWindow(vbNullString, "Program Manager")
        If Not hwnd = 0 Then
            ShowWindow(hwnd, SW_RESTORE)
        End If
    End Sub

    Private Sub Morrigan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Oracle.SetInputToDefaultAudioDevice()
        Dim gram As New Recognition.SrgsGrammar.SrgsDocument
        Dim speechRule As New Recognition.SrgsGrammar.SrgsRule("speech")
        Dim Commands() As String = System.IO.File.ReadAllLines("C:\Users\Leeraoy.Jenkins\source\repos\Morrayne\Commands.txt")
        Dim speechList As New Recognition.SrgsGrammar.SrgsOneOf("WHAT DAY IS IT", "MORRAYNE CLOSE", "HOWS THE WEATHER", "OPEN CHROME", "REPOSITORY PLEASE", "MOVE PLEASE", "OUT OF THE WAY", "SHOW YOURSELF", "COME BACK",
        "LOAD TERMINAL", "YES", "NO", "HIDE DESKTOP", "RESTORE DESKTOP", "DAY", "WEATHER", "CHROME", "YOUTUBE", "PLEASE", "TERMINAL", "BLIND", "SIGHT", "PLAY PIANO", "PIANO", "CLOSE PIANO", "END")
        speechRule.Add(speechList)
        gram.Rules.Add(speechRule)
        synth.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Adult)
        'Dim Commands() As String = System.IO.File.ReadAllLines("Commands.txt")
        For i As Integer = 0 To Commands.Count - 1
            lstboxCommands.Items.Add(Commands(i))
        Next
        gram.Root = speechRule
        Oracle.LoadGrammar(New Recognition.Grammar(gram))
        Oracle.RecognizeAsync()
    End Sub

    'This section below deletes recent activity
    Public Sub Delrecent()
        Dim di As New DirectoryInfo(
            Environment.GetFolderPath(Environment.SpecialFolder.Recent))
        On Error GoTo err
        ' Create the directory only if it does not already exist.
        If di.Exists = False Then
            di.Create()
        End If
        '//Set folder to normal attributes to allow easy deletion (and to get 
        '//rid of any read-only attributes, which make it hard to delete the 
        '//files/folder)
        System.IO.File.SetAttributes(Environment.GetFolderPath(
            Environment.SpecialFolder.Recent).ToString, FileAttributes.Normal)
        Dim recentk As String
        Dim Recentl() As String

        Recentl = IO.Directory.GetFiles(Environment.GetFolderPath(
            Environment.SpecialFolder.Recent))
        For Each recentk In Recentl '//Get all files in recent folder and then
            '//set their attribute to normal, and then delete them.
            IO.File.SetAttributes(recentk, FileAttributes.Normal)
            IO.File.Delete(recentk)
        Next
        ' The true indicates that if subdirectories
        ' or files are in this directory, they are to be deleted as well.
        ' Delete the directory.
        di.Delete(True)

err:    '///IGNORE ERROR///
    End Sub

    'This section below deletes all history from chrome, firefox, opera, IE Explorer, etc...
    Private Sub Clear_Temp_Files()
        Shell("RunDll32.exe InetCpl.cpl,ClearMyTracksByProcess 8 ")
    End Sub

    Private Sub Clear_Cookies()
        Shell("RunDll32.exe InetCpl.cpl,ClearMyTracksByProcess 2")
    End Sub

    Private Sub Clear_History()
        Shell("RunDll32.exe InetCpl.cpl,ClearMyTracksByProcess 1")
    End Sub

    Private Sub Clear_Form_Data()
        Shell("RunDll32.exe InetCpl.cpl,ClearMyTracksByProcess 16")
    End Sub

    Private Sub Clear_Saved_Passwords()
        Shell("RunDll32.exe InetCpl.cpl,ClearMyTracksByProcess 32")
    End Sub

    Private Sub Clear_All()
        Shell("RunDll32.exe InetCpl.cpl,ClearMyTracksByProcess 255")
    End Sub

    Private Sub Oracle_RecognizeCompleted(ByVal sender As Object, ByVal e As System.Speech.Recognition.RecognizeCompletedEventArgs) Handles Oracle.RecognizeCompleted
        Oracle.RecognizeAsync()
    End Sub

    Private Sub Oracle_SpeechRecognized(ByVal sender As Object, ByVal e As System.Speech.Recognition.RecognitionEventArgs) Handles Oracle.SpeechRecognized
        'MsgBox(e.Result.Text.ToUpper)
        Dim Random As New Random
        Dim Number As Integer = Random.Next(1, 20)
        Dim Number2 As Integer = Random.Next(1, 8)
        Select Case e.Result.Text.ToUpper
            'GREETINGS
            Case Is = "WHAT DAY IS IT"
                My.Computer.Audio.Play(My.Resources.day, AudioPlayMode.Background)
            Case "DAY"
                synth.SpeakAsync(Format(Now, "Long Date"))
            Case "NO"
                My.Computer.Audio.Play(My.Resources.sorry, AudioPlayMode.Background)
                Thread.Sleep(3000)
        End Select


        Select Case e.Result.Text.ToUpper
            Case Is = "MORRAYNE CLOSE"
                My.Computer.Audio.Play(My.Resources.close, AudioPlayMode.Background)
                Thread.Sleep(5000)
                Application.Exit()
        End Select


        Select Case e.Result.Text.ToUpper
            Case Is = "HOWS THE WEATHER"
                My.Computer.Audio.Play(My.Resources.weathers, AudioPlayMode.Background)
            Case "WEATHER"
                Select Case Number
                    Case Is < 9
                        Process.Start("https://weather.com/weather/today/l/43b1a1257e742e6e96a02e71de5bd387e1693b773d5c492e15efd908b2d78c2b")
                        My.Computer.Audio.Play(My.Resources.forecast, AudioPlayMode.Background)
                    Case "NO"
                    Case Is > 8
                        My.Computer.Audio.Play(My.Resources.sorry, AudioPlayMode.Background)
                        Thread.Sleep(3000)
                End Select

        End Select

        Select Case e.Result.Text.ToUpper
            Case Is = "OPEN CHROME"
                My.Computer.Audio.Play(My.Resources.chromes, AudioPlayMode.Background)
            Case "CHROME"
                Select Case Number
                    Case Is < 12
                        My.Computer.Audio.Play(My.Resources.rightaway, AudioPlayMode.Background)
                        Process.Start("chrome")
                    Case "NO"
                    Case Is > 11
                        My.Computer.Audio.Play(My.Resources.sorry, AudioPlayMode.Background)
                        Thread.Sleep(3000)
                End Select
        End Select

        Select Case e.Result.Text.ToUpper
            Case Is = "OPEN YOUTUBE"
                My.Computer.Audio.Play(My.Resources.askyoutube, AudioPlayMode.Background)
            Case "YOUTUBE"
                Select Case Number
                    Case Is < 14
                        My.Computer.Audio.Play(My.Resources.youtube, AudioPlayMode.Background)
                        Process.Start("https://www.youtube.com/")
                    Case "NO"
                    Case Is > 13
                        My.Computer.Audio.Play(My.Resources.sorry, AudioPlayMode.Background)
                        Thread.Sleep(3000)
                End Select
        End Select

        Select Case e.Result.Text.ToUpper
            Case Is = "REPOSITORY PLEASE"
                My.Computer.Audio.Play(My.Resources.askreposit, AudioPlayMode.Background)
            Case "PLEASE"
                Select Case Number
                    Case Is < 16
                        My.Computer.Audio.Play(My.Resources.repository, AudioPlayMode.Background)
                        Process.Start("https://github.com/deadreport77")
                    Case "NO"
                    Case Is > 15
                        My.Computer.Audio.Play(My.Resources.sorry, AudioPlayMode.Background)
                        Thread.Sleep(3000)
                End Select
        End Select

        Select Case e.Result.Text.ToUpper
            Case Is = "MOVE PLEASE", "OUT OF THE WAY"
                Select Case Number
                    Case Is < 6
                        My.Computer.Audio.Play(My.Resources.show, AudioPlayMode.Background)
                    Case Is > 5
                End Select
                Me.WindowState = FormWindowState.Minimized
            Case Is = "SHOW YOURSELF", "COME BACK"
                Me.WindowState = FormWindowState.Normal
                My.Computer.Audio.Play(My.Resources.asyouwish, AudioPlayMode.Background)
        End Select

        Select Case e.Result.Text.ToUpper
            Case Is = "LOAD TERMINAL"
                My.Computer.Audio.Play(My.Resources.termrequest, AudioPlayMode.Background)
            Case "TERMINAL"
                Select Case Number
                    Case Is < 20
                        My.Computer.Audio.Play(My.Resources.terminal, AudioPlayMode.Background)
                        Terminal.Show()
                        Me.Hide()
                    Case "NO"
                    Case Is > 19
                        My.Computer.Audio.Play(My.Resources.sorry, AudioPlayMode.Background)
                        Thread.Sleep(3000)
                End Select
        End Select

        Select Case e.Result.Text.ToUpper
            Case Is = "PLAY PIANO"
                My.Computer.Audio.Play(My.Resources.askpiano, AudioPlayMode.Background)
            Case "PIANO"
                My.Computer.Audio.Play(My.Resources.openingpiano, AudioPlayMode.Background)
                Piano.Show()
                Me.Hide()
        End Select

        Select Case e.Result.Text.ToUpper
            Case Is = "CLOSE PIANO"
                My.Computer.Audio.Play(My.Resources.session, AudioPlayMode.Background)
            Case "END"
                Piano.Hide()
                Me.Show()
        End Select


        Select Case e.Result.Text.ToUpper
            Case Is = "HIDE DESKTOP"
                My.Computer.Audio.Play(My.Resources.carryout, AudioPlayMode.Background)
            Case "BLIND"
                My.Computer.Audio.Play(My.Resources.stealthactivated, AudioPlayMode.Background)
                DesktopIconsHide()
                Dim intReturn As Integer = FindWindow("Shell_traywnd", "")
                SetWindowPos(intReturn, 0, 0, 0, 0, 0, SWP_HIDEWINDOW)
            Case "NO"
                My.Computer.Audio.Play(My.Resources.sorry, AudioPlayMode.Background)
                Thread.Sleep(3000)
        End Select


        Select Case e.Result.Text.ToUpper
            Case Is = "RESTORE DESKTOP"
                My.Computer.Audio.Play(My.Resources.deskback, AudioPlayMode.Background)
            Case "SIGHT"
                My.Computer.Audio.Play(My.Resources.restoredesk, AudioPlayMode.Background)
                DesktopIconShow()
                Dim intReturn As Integer = FindWindow("Shell_traywnd", "")
                SetWindowPos(intReturn, 0, 0, 0, 0, 0, SWP_SHOWWINDOW)
        End Select


    End Sub
End Class