Imports System.Speech
Imports System.Speech.Synthesis
Imports System.Threading
Imports System.IO

Public Class Morrigan
    Private WithEvents Oracle As New Recognition.SpeechRecognitionEngine
    Dim QuestionEvent As String
    Dim synth As New Synthesis.SpeechSynthesizer

    Private Sub Morrigan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Oracle.SetInputToDefaultAudioDevice()
        Dim gram As New Recognition.SrgsGrammar.SrgsDocument
        Dim speechRule As New Recognition.SrgsGrammar.SrgsRule("speech")
        Dim Commands() As String = System.IO.File.ReadAllLines("C:\Users\Leeraoy.Jenkins\source\repos\Morrayne\Commands.txt")
        Dim speechList As New Recognition.SrgsGrammar.SrgsOneOf("WHAT DAY IS IT", "MORRAYNE CLOSE", "HOW'S THE WEATHER", "OPEN CHROME", "REPOSITORY PLEASE", "MOVE PLEASE", "OUT OF THE WAY", "SHOW YOURSELF", "COME BACK",
        "LOAD TERMINAL", "YES", "NO")
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
        Dim Number As Integer = Random.Next(1, 10)
        Dim Number2 As Integer = Random.Next(1, 4)
        Select Case e.Result.Text.ToUpper
            'GREETINGS
            Case Is = "WHAT DAY IS IT"
                My.Computer.Audio.Play(My.Resources.day, AudioPlayMode.Background)
            Case "YES"
                synth.SpeakAsync(Format(Now, "Long Date"))
            Case "NO"
                My.Computer.Audio.Play(My.Resources.sorry, AudioPlayMode.Background)
                Thread.Sleep(3000)



            Case Is = "MORRAYNE CLOSE"
                My.Computer.Audio.Play(My.Resources.close, AudioPlayMode.Background)
                Thread.Sleep(5000)
                Application.Exit()

            Case Is = "HOW'S THE WEATHER"
                Process.Start("https://weather.com/weather/today/l/43b1a1257e742e6e96a02e71de5bd387e1693b773d5c492e15efd908b2d78c2b")
                My.Computer.Audio.Play(My.Resources.forecast, AudioPlayMode.Background)

            Case Is = "OPEN CHROME"
                synth.SpeakAsync("Right Away")
                Process.Start("chrome")

            Case Is = "OPEN YOUTUBE"
                My.Computer.Audio.Play(My.Resources.youtube, AudioPlayMode.Background)
                Process.Start("https://www.youtube.com/")

            Case Is = "REPOSITORY PLEASE"
                My.Computer.Audio.Play(My.Resources.repository, AudioPlayMode.Background)
                Process.Start("https://github.com/deadreport77")

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

            Case "LOAD TERMINAL"
                My.Computer.Audio.Play(My.Resources.terminal, AudioPlayMode.Background)
                Terminal.Show()
                Me.Hide()
        End Select
    End Sub
End Class