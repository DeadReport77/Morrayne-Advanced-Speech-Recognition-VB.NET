Public Class Terminal
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

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        DesktopIconsHide()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        DesktopIconShow()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim intReturn As Integer = FindWindow("Shell_traywnd", "")
        SetWindowPos(intReturn, 0, 0, 0, 0, 0, SWP_HIDEWINDOW)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim intReturn As Integer = FindWindow("Shell_traywnd", "")
        SetWindowPos(intReturn, 0, 0, 0, 0, 0, SWP_SHOWWINDOW)
    End Sub
End Class