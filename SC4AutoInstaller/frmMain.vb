﻿Public Class frmMain

    Private Sub bgwComputeMD5_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwComputeMD5.DoWork
        Dim MD5CSP As New Security.Cryptography.MD5CryptoServiceProvider, MD5(4) As String
        Dim SC4FilesMD5() As String = {"C05406B02449540328DBB4B741E0A81D", "E2976161D7EC772893D273FF753D08F6", "3E660755D70543D2222BD46B5A6F22C4", "6DB4F1F9F1A1EC45B22E35827073FBA2"}
        Dim _638FilesMD5() As String = {"A9E238946A8C8C479DD368EC4581B77A", "2CFD520899786AEF47C728B123EBCF05", "7FE6E6678FBBA581092473C5F4C35331", "CB2C26A9C4BC9B8E53709380B64B805C"}
        With ModuleMain.InstalledModule
            Dim FilesStream() As IO.FileStream = {New IO.FileStream(.SC4InstallDir & "\Apps\SimCity 4.exe", IO.FileMode.Open) _
                                                 , New IO.FileStream(.SC4InstallDir & "\SimCity_1.dat", IO.FileMode.Open) _
                                                 , New IO.FileStream(.SC4InstallDir & "\SimCity_2.dat", IO.FileMode.Open) _
                                                 , New IO.FileStream(.SC4InstallDir & "\SimCity_3.dat", IO.FileMode.Open) _
                                                 , New IO.FileStream(.SC4InstallDir & "\SimCity_4.dat", IO.FileMode.Open)}
            For i As Integer = 0 To 4
                If e.Cancel = True Then
                    For Each j As IO.FileStream In FilesStream
                        j.Close()
                    Next
                    Exit Sub
                End If
                MD5(i) = BitConverter.ToString(MD5CSP.ComputeHash(FilesStream(i))).Replace("-", "") : FilesStream(i).Close()
            Next
            For i As Integer = 1 To 4
                If MD5(i) = _638FilesMD5(i - 1) Then .Is638PatchInstalled = True Else .Is638PatchInstalled = False
            Next
            Select Case MD5(0)
                Case "6159A4036F451BEA1740DDB05C32494A" : .Is640PatchInstalled = True
                Case "53D2AE4FA9114B88AD91ECF32A7F16A4" : .Is641PatchInstalled = True
                Case "2F2BD7D9A76E85320A26D7BD7530DCAE", "1C18B7DC760EDADD2C2EFAF33F60F150" : .Is4GBPatchInstalled = True
                Case "1414E70EB5CE22DB37D22CB99439D012" : .Is4GBPatchInstalled = True : .Is641PatchInstalled = True
                Case "AADC5464919FBDC0F8E315FA51582126" : .Is4GBPatchInstalled = True : .IsNoCDPatchInstalled = True
                Case "B57B5B03C4854C194CE8BEBD173F3483" : .IsNoCDPatchInstalled = True
            End Select
            If My.Computer.FileSystem.FileExists(.SC4InstallDir & "\SC4Launcher.exe") = True Then .IsSC4LauncherInstalled = True
        End With
    End Sub

    Private Sub bgwComputeMD5_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwComputeMD5.RunWorkerCompleted
        If e.Cancelled = False Then btnChangeModule.Enabled = True : Cursor = Cursors.Default
    End Sub

    Private Sub frmMain_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If e.CloseReason = CloseReason.ApplicationExitCall Then
            If MessageBox.Show("确定要退出安装程序吗？", "确认退出", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.No Then e.Cancel = True
        End If
    End Sub

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        With ModuleMain.InstalledModule
            Dim SC4InstallDir As String = Nothing
            If Environment.Is64BitOperatingSystem = True Then SC4InstallDir = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Maxis\SimCity 4\1.0", "Install Dir", Nothing)
            If Environment.Is64BitOperatingSystem = False Then SC4InstallDir = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Maxis\SimCity 4\1.0", "Install Dir", Nothing)
            If SC4InstallDir <> Nothing AndAlso My.Computer.FileSystem.FileExists(SC4InstallDir & "\Apps\SimCity 4.exe") = True Then
                If SC4InstallDir.EndsWith("\") = True Or My.Computer.FileSystem.FileExists(SC4InstallDir & "Apps\SimCity 4.exe") = True Then
                    .SC4InstallDir = SC4InstallDir.Substring(0, SC4InstallDir.Length - 1)
                ElseIf SC4InstallDir.EndsWith("\") = False Or My.Computer.FileSystem.FileExists(SC4InstallDir & "\Apps\SimCity 4.exe") = True Then
                    .SC4InstallDir = SC4InstallDir
                End If
                bgwComputeMD5.RunWorkerAsync() : Cursor = Cursors.WaitCursor
                Dim LanguageRegKeyName As String = Nothing
                If Environment.Is64BitOperatingSystem = True Then LanguageRegKeyName = "HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Maxis\SimCity 4\1.0"
                If Environment.Is64BitOperatingSystem = False Then LanguageRegKeyName = "HKEY_LOCAL_MACHINE\SOFTWARE\Maxis\SimCity 4\1.0"
                Select Case My.Computer.Registry.GetValue(LanguageRegKeyName, "Language", Nothing)
                    Case "18" : .LanguagePatch = InstalledModule.Language.TraditionalChinese
                    Case "17" : .LanguagePatch = InstalledModule.Language.SimplifiedChinese
                    Case "1", "English US", Nothing : .LanguagePatch = InstalledModule.Language.English
                End Select
                btnInstall.Visible = False : btnChangeModule.Visible = True : btnUninstall.Visible = True
                btnAbout.Location = New Point(270, 285) : btnExit.Location = New Point(270, 330)
            Else : ModuleMain.InstalledModule = Nothing
            End If
        End With
        Text &= " " & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Revision & " By n0099"
    End Sub

    Private Sub btnInstall_Click(sender As Object, e As EventArgs) Handles btnInstall.Click
        frmLicenses.Show()
        RemoveHandler Me.FormClosing, AddressOf frmMain_FormClosing
        Close()
    End Sub

    Private Sub btnChangeModule_Click(sender As Object, e As EventArgs) Handles btnChangeModule.Click
        If My.Computer.FileSystem.DirectoryExists("Data") = True Then
            frmModuleChangeOption.Show()
            RemoveHandler Me.FormClosing, AddressOf frmMain_FormClosing
            Close()
        Else
            MessageBox.Show("请使用原始安装程序以添加或删除组件。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub btnUninstall_Click(sender As Object, e As EventArgs) Handles btnUninstall.Click
        frmUninstalling.Show()
        RemoveHandler Me.FormClosing, AddressOf frmMain_FormClosing
        bgwComputeMD5.CancelAsync()
        Close()
    End Sub

    Private Sub btnAbout_Click(sender As Object, e As EventArgs) Handles btnAbout.Click
        frmAbout.ShowDialog()
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Application.Exit()
    End Sub

End Class