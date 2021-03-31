Imports System.Speech

Public Class Form1
    Private TargetDT As DateTime
    Private WithEvents Oracle As New Recognition.SpeechRecognitionEngine
    Dim QuestionEvent As String
    Dim synth As New Synthesis.SpeechSynthesizer
    Private CountDownFrom As TimeSpan = TimeSpan.FromSeconds(60)

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Interval = 35000
        TargetDT = DateTime.Now.Add(CountDownFrom)
        Timer1.Start()
        My.Computer.Audio.Play(My.Resources.exile, AudioPlayMode.Background)
        Oracle.SetInputToDefaultAudioDevice()
        Dim gram As New Recognition.SrgsGrammar.SrgsDocument
        Dim speechRule As New Recognition.SrgsGrammar.SrgsRule("speech")
        Dim Commands() As String = System.IO.File.ReadAllLines("C:\Users\Leeraoy.Jenkins\source\repos\Morrayne\Commands.txt")
        Dim speechList As New Recognition.SrgsGrammar.SrgsOneOf("THE NINTH WAVE")
        speechRule.Add(speechList)
        gram.Rules.Add(speechRule)
        'Dim Commands() As String = System.IO.File.ReadAllLines("Commands.txt")
        For i As Integer = 0 To Commands.Count - 1
            lstboxCommands.Items.Add(Commands(i))
        Next
        gram.Root = speechRule
        Oracle.LoadGrammar(New Recognition.Grammar(gram))
        Oracle.RecognizeAsync()
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
            Case Is = "THE NINTH WAVE"
                Morrigan.Show()
                Me.Hide()
        End Select
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim ts As TimeSpan = TargetDT.Subtract(DateTime.Now) 'Start of Timer1 Code>>>>
        If ts.TotalMilliseconds > 0 Then
            Label1.Text = ts.ToString("ss")
        Else
            Label1.Text = "00:00"
            Timer1.Stop()
        End If
        If Label1.Text = "00:00" Then
            Timer1.Stop() ' End of Timer1 Code>>>
            Return
            Application.Exit()
        End If
    End Sub
End Class
