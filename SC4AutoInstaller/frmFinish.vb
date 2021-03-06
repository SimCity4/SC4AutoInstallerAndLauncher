﻿Public Class frmFinish

    Private Sub SubassemblyInstallFail(item As ListViewItem)
        item.ImageKey = "fail"
        item.Group = lvwSubassembly.Groups.Item("lvwGroupFail")
        lblTitle2.Text = "部分组件安装失败"
    End Sub

    Private Sub llbBlog_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbBlog.LinkClicked
        Process.Start("http://n0099.sinaapp.com")
    End Sub

    Private Sub llbReportBug_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbReportBug.LinkClicked
        Process.Start("http://tieba.baidu.com/p/3802761033")
    End Sub

    Private Sub llbSCB_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbSCB.LinkClicked
        Process.Start("http://tieba.baidu.com/f?kw=%C4%A3%C4%E2%B3%C7%CA%D0")
    End Sub

    Private Sub llbSCCN_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llbSCCN.LinkClicked
        Process.Start("http://www.simcity.cn")
    End Sub

    Private Sub frmFinish_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim DAEMONItem As ListViewItem = lvwSubassembly.FindItemWithText("DAEMON Tools Lite")
        Dim SC4Item As ListViewItem = lvwSubassembly.FindItemWithText("模拟城市4 豪华版")
        Dim _638PatchItem As ListViewItem = lvwSubassembly.FindItemWithText("638补丁")
        Dim _640PatchItem As ListViewItem = lvwSubassembly.FindItemWithText("640补丁")
        Dim _641PatchItem As ListViewItem = lvwSubassembly.FindItemWithText("641补丁")
        Dim _4GBPatchItem As ListViewItem = lvwSubassembly.FindItemWithText("4GB补丁")
        Dim NoCDPatchItem As ListViewItem = lvwSubassembly.FindItemWithText("免CD补丁")
        Dim SC4LauncherItem As ListViewItem = lvwSubassembly.FindItemWithText("模拟城市4 启动器")
        Dim LanguagePatchItem As ListViewItem = lvwSubassembly.FindItemWithText("语言补丁")
        With ModuleMain.InstallOptions
            If IsNothing(ModuleMain.InstalledModule) = True Then
                If .IsInstallDAEMONTools = False Then DAEMONItem.Remove()
                If .IsInstall638Patch = False Then _638PatchItem.Remove()
                If .IsInstall640Patch = False Then _640PatchItem.Remove()
                If .IsInstall641Patch = False Then _641PatchItem.Remove()
                If .IsInstall4GBPatch = False Then _4GBPatchItem.Remove()
                If .IsInstallNoCDPatch = False Then NoCDPatchItem.Remove()
                If .IsInstallSC4Launcher = False Then SC4LauncherItem.Remove()
                If .IsInstallDAEMONTools = True Then If ModuleMain.InstallResult.DAEMONToolsInstallResult = InstallResult.Result.Fail Then SubassemblyInstallFail(DAEMONItem)
                If ModuleMain.InstallResult.SC4InstallResult = InstallResult.Result.Success AndAlso _
                    My.Computer.FileSystem.FileExists(.SC4InstallDir & "\Apps\SimCity 4.exe") = True Then btnRunSC4.Enabled = True Else btnRunSC4.Enabled = False
                If ModuleMain.InstallResult.SC4InstallResult = InstallResult.Result.Fail Then SubassemblyInstallFail(SC4Item)
                If .SC4Type = InstallOptions.SC4InstallType.ISO Then SC4Item.Text = "模拟城市4 豪华版 镜像版"
                If .SC4Type = InstallOptions.SC4InstallType.NoInstall Then SC4Item.Text = "模拟城市4 豪华版 硬盘版"
            Else
                DAEMONItem.Remove() : SC4Item.Remove()
                If ModuleMain.InstalledModule.Is638PatchInstalled = .IsInstall638Patch Then _638PatchItem.Remove()
                If ModuleMain.InstalledModule.Is640PatchInstalled = .IsInstall640Patch Then _640PatchItem.Remove()
                If ModuleMain.InstalledModule.Is641PatchInstalled = .IsInstall641Patch Then _641PatchItem.Remove()
                If ModuleMain.InstalledModule.Is4GBPatchInstalled = .IsInstall4GBPatch Then _4GBPatchItem.Remove()
                If ModuleMain.InstalledModule.IsNoCDPatchInstalled = .IsInstallNoCDPatch Then NoCDPatchItem.Remove()
                If ModuleMain.InstalledModule.IsSC4LauncherInstalled = .IsInstallSC4Launcher Then SC4LauncherItem.Remove()
                If ModuleMain.InstalledModule.LanguagePatch = .LanguagePatch Then LanguagePatchItem.Remove()
            End If
            With ModuleMain.InstallResult
                If ._638PatchInstallResult = InstallResult.Result.Fail Then SubassemblyInstallFail(_638PatchItem)
                If ._640PatchInstallResult = InstallResult.Result.Fail Then SubassemblyInstallFail(_640PatchItem)
                If ._641PatchInstallResult = InstallResult.Result.Fail Then SubassemblyInstallFail(_641PatchItem)
                If ._4GBPatchInstallResult = InstallResult.Result.Fail Then SubassemblyInstallFail(_4GBPatchItem)
                If .NoCDPatchInstallResult = InstallResult.Result.Fail Then SubassemblyInstallFail(NoCDPatchItem)
                If .SC4LauncherInstallResult = InstallResult.Result.Fail Then SubassemblyInstallFail(SC4LauncherItem)
                If .LanguagePatchInstallResult = InstallResult.Result.Fail Then SubassemblyInstallFail(LanguagePatchItem)
            End With
            Select Case .LanguagePatch
                Case InstallOptions.Language.TraditionalChinese : LanguagePatchItem.Text = "繁体中文语言补丁"
                Case InstallOptions.Language.SimplifiedChinese : LanguagePatchItem.Text = "简体中文语言补丁"
                Case InstallOptions.Language.English : LanguagePatchItem.Remove()
            End Select
        End With
        Dim FlashInfo As New FLASHINFO With {.cbSize = Convert.ToInt32(Runtime.InteropServices.Marshal.SizeOf(FlashInfo)) _
                                             , .uCount = 5, .dwTimeout = 0, .hwnd = Me.Handle, .dwFlags = FLASHW_ALL}
        FlashWindowEx(FlashInfo)
        Text &= " " & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Revision & " By n0099"
    End Sub

    Private Sub btnRunSC4_Click(sender As Object, e As EventArgs) Handles btnRunSC4.Click
        If My.Computer.FileSystem.FileExists(ModuleMain.InstallOptions.SC4InstallDir & "\SC4Launcher.exe") = True Then
            Process.Start(ModuleMain.InstallOptions.SC4InstallDir & "\SC4Launcher.exe")
        Else
            Process.Start(ModuleMain.InstallOptions.SC4InstallDir & "\Apps\SimCity 4.exe")
        End If
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Application.Exit()
    End Sub

End Class