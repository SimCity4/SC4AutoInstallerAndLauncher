﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOptions
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意:  以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。  
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim TreeNode1 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("模拟城市4 豪华版 镜像版")
        Dim TreeNode2 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("模拟城市4 豪华版 硬盘版")
        Dim TreeNode3 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("DAEMON Tools Lite")
        Dim TreeNode4 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("必选组件", New System.Windows.Forms.TreeNode() {TreeNode1, TreeNode2, TreeNode3})
        Dim TreeNode5 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("638补丁")
        Dim TreeNode6 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("640补丁")
        Dim TreeNode7 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("4GB补丁")
        Dim TreeNode8 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("免CD补丁")
        Dim TreeNode9 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("模拟城市4 启动器")
        Dim TreeNode10 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("繁体中文")
        Dim TreeNode11 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("简体中文")
        Dim TreeNode12 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("英文")
        Dim TreeNode13 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("语言补丁", New System.Windows.Forms.TreeNode() {TreeNode10, TreeNode11, TreeNode12})
        Dim TreeNode14 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("可选组件", New System.Windows.Forms.TreeNode() {TreeNode5, TreeNode6, TreeNode7, TreeNode8, TreeNode9, TreeNode13})
        Dim TreeNode15 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("添加桌面图标")
        Dim TreeNode16 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("添加开始菜单项")
        Dim TreeNode17 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("附加任务", New System.Windows.Forms.TreeNode() {TreeNode15, TreeNode16})
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmOptions))
        Me.txtSC4InstallDir = New System.Windows.Forms.TextBox()
        Me.btnSC4InstallDir = New System.Windows.Forms.Button()
        Me.lblSC4lInstallDir = New System.Windows.Forms.Label()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnInstall = New System.Windows.Forms.Button()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.tvwOptions = New System.Windows.Forms.TreeView()
        Me.imgOptions = New System.Windows.Forms.ImageList(Me.components)
        Me.grpOptionsDetail = New System.Windows.Forms.GroupBox()
        Me.btnDAEMONlInstallDir = New System.Windows.Forms.Button()
        Me.lblDAEMONlInstallDir = New System.Windows.Forms.Label()
        Me.txtDAEMONlInstallDir = New System.Windows.Forms.TextBox()
        Me.lblOptionsDiskSpace = New System.Windows.Forms.Label()
        Me.lblOptionsDetail = New System.Windows.Forms.Label()
        Me.lblNeedsDiskSpace = New System.Windows.Forms.Label()
        Me.fbdSC4InstallDir = New System.Windows.Forms.FolderBrowserDialog()
        Me.fbdDAEMONlInstallDir = New System.Windows.Forms.FolderBrowserDialog()
        Me.chkEAEULA = New System.Windows.Forms.CheckBox()
        Me.llbEAEULA = New System.Windows.Forms.LinkLabel()
        Me.lblTitle2 = New System.Windows.Forms.Label()
        Me.cmbOptions = New System.Windows.Forms.ComboBox()
        Me.grpOptionsDetail.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtSC4InstallDir
        '
        Me.txtSC4InstallDir.Location = New System.Drawing.Point(12, 360)
        Me.txtSC4InstallDir.Name = "txtSC4InstallDir"
        Me.txtSC4InstallDir.Size = New System.Drawing.Size(514, 21)
        Me.txtSC4InstallDir.TabIndex = 6
        '
        'btnSC4InstallDir
        '
        Me.btnSC4InstallDir.Location = New System.Drawing.Point(532, 358)
        Me.btnSC4InstallDir.Name = "btnSC4InstallDir"
        Me.btnSC4InstallDir.Size = New System.Drawing.Size(80, 23)
        Me.btnSC4InstallDir.TabIndex = 7
        Me.btnSC4InstallDir.Text = "浏览(&B)..."
        Me.btnSC4InstallDir.UseVisualStyleBackColor = True
        '
        'lblSC4lInstallDir
        '
        Me.lblSC4lInstallDir.AutoSize = True
        Me.lblSC4lInstallDir.Location = New System.Drawing.Point(12, 339)
        Me.lblSC4lInstallDir.Name = "lblSC4lInstallDir"
        Me.lblSC4lInstallDir.Size = New System.Drawing.Size(281, 12)
        Me.lblSC4lInstallDir.TabIndex = 5
        Me.lblSC4lInstallDir.Text = "安装程序将安装 模拟城市4 豪华版 到下列文件夹中"
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(537, 407)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 13
        Me.btnCancel.Text = "取消(&C)"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnInstall
        '
        Me.btnInstall.Enabled = False
        Me.btnInstall.Location = New System.Drawing.Point(451, 407)
        Me.btnInstall.Name = "btnInstall"
        Me.btnInstall.Size = New System.Drawing.Size(80, 23)
        Me.btnInstall.TabIndex = 12
        Me.btnInstall.Text = "安装(&N) >"
        Me.btnInstall.UseVisualStyleBackColor = True
        '
        'btnBack
        '
        Me.btnBack.Location = New System.Drawing.Point(365, 407)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(80, 23)
        Me.btnBack.TabIndex = 11
        Me.btnBack.Text = "< 返回(&B)"
        Me.btnBack.UseVisualStyleBackColor = True
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("微软雅黑", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.lblTitle.Location = New System.Drawing.Point(7, 9)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(92, 27)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "安装选项"
        '
        'tvwOptions
        '
        Me.tvwOptions.ImageKey = "rootnodebackground.png"
        Me.tvwOptions.ImageList = Me.imgOptions
        Me.tvwOptions.Location = New System.Drawing.Point(12, 80)
        Me.tvwOptions.Name = "tvwOptions"
        TreeNode1.ImageKey = "radiochecked"
        TreeNode1.Name = "模拟城市4 豪华版 镜像版"
        TreeNode1.SelectedImageKey = "radiochecked"
        TreeNode1.Text = "模拟城市4 豪华版 镜像版"
        TreeNode2.ImageKey = "radiounchecked"
        TreeNode2.Name = "模拟城市4 豪华版 硬盘版"
        TreeNode2.SelectedImageKey = "radiounchecked"
        TreeNode2.Text = "模拟城市4 豪华版 硬盘版"
        TreeNode3.ImageKey = "checked"
        TreeNode3.Name = "DAEMON Tools Lite"
        TreeNode3.SelectedImageKey = "checked"
        TreeNode3.Text = "DAEMON Tools Lite"
        TreeNode4.ImageKey = "rootnodebackground"
        TreeNode4.Name = "必选组件"
        TreeNode4.SelectedImageKey = "rootnodebackground"
        TreeNode4.Text = "必选组件"
        TreeNode5.ImageKey = "checked"
        TreeNode5.Name = "638补丁"
        TreeNode5.SelectedImageKey = "checked"
        TreeNode5.Text = "638补丁"
        TreeNode6.ImageKey = "checked"
        TreeNode6.Name = "640补丁"
        TreeNode6.SelectedImageKey = "checked"
        TreeNode6.Text = "640补丁"
        TreeNode7.ImageKey = "unchecked"
        TreeNode7.Name = "4GB补丁"
        TreeNode7.SelectedImageKey = "unchecked"
        TreeNode7.Text = "4GB补丁"
        TreeNode8.ImageKey = "unchecked"
        TreeNode8.Name = "免CD补丁"
        TreeNode8.SelectedImageKey = "unchecked"
        TreeNode8.Text = "免CD补丁"
        TreeNode9.ImageKey = "checked"
        TreeNode9.Name = "模拟城市4 启动器"
        TreeNode9.SelectedImageKey = "checked"
        TreeNode9.Text = "模拟城市4 启动器"
        TreeNode10.ImageKey = "radiochecked"
        TreeNode10.Name = "繁体中文"
        TreeNode10.SelectedImageKey = "radiochecked"
        TreeNode10.Text = "繁体中文"
        TreeNode11.ImageKey = "radiounchecked"
        TreeNode11.Name = "简体中文"
        TreeNode11.SelectedImageKey = "radiounchecked"
        TreeNode11.Text = "简体中文"
        TreeNode12.ImageKey = "radiounchecked"
        TreeNode12.Name = "英文"
        TreeNode12.SelectedImageKey = "radiounchecked"
        TreeNode12.Text = "英文"
        TreeNode13.ImageKey = "nodebackground"
        TreeNode13.Name = "语言补丁"
        TreeNode13.SelectedImageKey = "nodebackground"
        TreeNode13.Text = "语言补丁"
        TreeNode14.ImageKey = "rootnodebackground"
        TreeNode14.Name = "可选组件"
        TreeNode14.SelectedImageKey = "rootnodebackground"
        TreeNode14.Text = "可选组件"
        TreeNode15.ImageKey = "checked"
        TreeNode15.Name = "添加桌面图标"
        TreeNode15.SelectedImageKey = "checked"
        TreeNode15.Text = "添加桌面图标"
        TreeNode16.ImageKey = "checked"
        TreeNode16.Name = "添加开始菜单项"
        TreeNode16.SelectedImageKey = "checked"
        TreeNode16.Text = "添加开始菜单项"
        TreeNode17.ImageKey = "rootnodebackground"
        TreeNode17.Name = "附加任务"
        TreeNode17.SelectedImageKey = "rootnodebackground"
        TreeNode17.Text = "附加任务"
        Me.tvwOptions.Nodes.AddRange(New System.Windows.Forms.TreeNode() {TreeNode4, TreeNode14, TreeNode17})
        Me.tvwOptions.SelectedImageIndex = 0
        Me.tvwOptions.ShowPlusMinus = False
        Me.tvwOptions.ShowRootLines = False
        Me.tvwOptions.Size = New System.Drawing.Size(300, 248)
        Me.tvwOptions.TabIndex = 3
        '
        'imgOptions
        '
        Me.imgOptions.ImageStream = CType(resources.GetObject("imgOptions.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgOptions.TransparentColor = System.Drawing.Color.Transparent
        Me.imgOptions.Images.SetKeyName(0, "rootnodebackground")
        Me.imgOptions.Images.SetKeyName(1, "nodebackground")
        Me.imgOptions.Images.SetKeyName(2, "checked")
        Me.imgOptions.Images.SetKeyName(3, "unchecked")
        Me.imgOptions.Images.SetKeyName(4, "radiochecked")
        Me.imgOptions.Images.SetKeyName(5, "radiounchecked")
        '
        'grpOptionsDetail
        '
        Me.grpOptionsDetail.Controls.Add(Me.btnDAEMONlInstallDir)
        Me.grpOptionsDetail.Controls.Add(Me.lblDAEMONlInstallDir)
        Me.grpOptionsDetail.Controls.Add(Me.txtDAEMONlInstallDir)
        Me.grpOptionsDetail.Controls.Add(Me.lblOptionsDiskSpace)
        Me.grpOptionsDetail.Controls.Add(Me.lblOptionsDetail)
        Me.grpOptionsDetail.Location = New System.Drawing.Point(318, 60)
        Me.grpOptionsDetail.Name = "grpOptionsDetail"
        Me.grpOptionsDetail.Size = New System.Drawing.Size(294, 268)
        Me.grpOptionsDetail.TabIndex = 4
        Me.grpOptionsDetail.TabStop = False
        Me.grpOptionsDetail.Text = "说明"
        '
        'btnDAEMONlInstallDir
        '
        Me.btnDAEMONlInstallDir.Location = New System.Drawing.Point(208, 188)
        Me.btnDAEMONlInstallDir.Name = "btnDAEMONlInstallDir"
        Me.btnDAEMONlInstallDir.Size = New System.Drawing.Size(80, 23)
        Me.btnDAEMONlInstallDir.TabIndex = 3
        Me.btnDAEMONlInstallDir.Text = "浏览(&B)..."
        Me.btnDAEMONlInstallDir.UseVisualStyleBackColor = True
        Me.btnDAEMONlInstallDir.Visible = False
        '
        'lblDAEMONlInstallDir
        '
        Me.lblDAEMONlInstallDir.AutoSize = True
        Me.lblDAEMONlInstallDir.Location = New System.Drawing.Point(6, 146)
        Me.lblDAEMONlInstallDir.Name = "lblDAEMONlInstallDir"
        Me.lblDAEMONlInstallDir.Size = New System.Drawing.Size(257, 12)
        Me.lblDAEMONlInstallDir.TabIndex = 1
        Me.lblDAEMONlInstallDir.Text = "安装程序将安装 DAEMON Tools 到下列文件夹中"
        Me.lblDAEMONlInstallDir.Visible = False
        '
        'txtDAEMONlInstallDir
        '
        Me.txtDAEMONlInstallDir.Location = New System.Drawing.Point(6, 161)
        Me.txtDAEMONlInstallDir.Name = "txtDAEMONlInstallDir"
        Me.txtDAEMONlInstallDir.Size = New System.Drawing.Size(282, 21)
        Me.txtDAEMONlInstallDir.TabIndex = 2
        Me.txtDAEMONlInstallDir.Visible = False
        '
        'lblOptionsDiskSpace
        '
        Me.lblOptionsDiskSpace.Location = New System.Drawing.Point(6, 214)
        Me.lblOptionsDiskSpace.Name = "lblOptionsDiskSpace"
        Me.lblOptionsDiskSpace.Size = New System.Drawing.Size(282, 31)
        Me.lblOptionsDiskSpace.TabIndex = 4
        Me.lblOptionsDiskSpace.Text = "此组件需要 0KB 硬盘空间"
        '
        'lblOptionsDetail
        '
        Me.lblOptionsDetail.Location = New System.Drawing.Point(8, 27)
        Me.lblOptionsDetail.Name = "lblOptionsDetail"
        Me.lblOptionsDetail.Size = New System.Drawing.Size(279, 119)
        Me.lblOptionsDetail.TabIndex = 0
        Me.lblOptionsDetail.Text = "组件说明"
        '
        'lblNeedsDiskSpace
        '
        Me.lblNeedsDiskSpace.Location = New System.Drawing.Point(12, 388)
        Me.lblNeedsDiskSpace.Name = "lblNeedsDiskSpace"
        Me.lblNeedsDiskSpace.Size = New System.Drawing.Size(600, 12)
        Me.lblNeedsDiskSpace.TabIndex = 8
        Me.lblNeedsDiskSpace.Text = "安装目录至少需要 0KB 的硬盘空间"
        Me.lblNeedsDiskSpace.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'fbdSC4InstallDir
        '
        Me.fbdSC4InstallDir.Description = "选择 模拟城市4 豪华版 的安装目录"
        Me.fbdSC4InstallDir.RootFolder = System.Environment.SpecialFolder.CommonProgramFilesX86
        '
        'fbdDAEMONlInstallDir
        '
        Me.fbdDAEMONlInstallDir.Description = "选择 DAEMON Tools 的安装目录"
        Me.fbdDAEMONlInstallDir.RootFolder = System.Environment.SpecialFolder.CommonProgramFilesX86
        '
        'chkEAEULA
        '
        Me.chkEAEULA.AutoSize = True
        Me.chkEAEULA.Location = New System.Drawing.Point(12, 411)
        Me.chkEAEULA.Name = "chkEAEULA"
        Me.chkEAEULA.Size = New System.Drawing.Size(48, 16)
        Me.chkEAEULA.TabIndex = 9
        Me.chkEAEULA.Text = "同意"
        Me.chkEAEULA.UseVisualStyleBackColor = True
        '
        'llbEAEULA
        '
        Me.llbEAEULA.AutoSize = True
        Me.llbEAEULA.Location = New System.Drawing.Point(57, 412)
        Me.llbEAEULA.Name = "llbEAEULA"
        Me.llbEAEULA.Size = New System.Drawing.Size(197, 12)
        Me.llbEAEULA.TabIndex = 10
        Me.llbEAEULA.TabStop = True
        Me.llbEAEULA.Text = "Electronic Arts 最终用户许可协议"
        '
        'lblTitle2
        '
        Me.lblTitle2.AutoSize = True
        Me.lblTitle2.Location = New System.Drawing.Point(12, 45)
        Me.lblTitle2.Name = "lblTitle2"
        Me.lblTitle2.Size = New System.Drawing.Size(113, 12)
        Me.lblTitle2.TabIndex = 1
        Me.lblTitle2.Text = "请选择要安装的组件"
        '
        'cmbOptions
        '
        Me.cmbOptions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbOptions.FormattingEnabled = True
        Me.cmbOptions.Items.AddRange(New Object() {"完全安装", "推荐安装", "精简安装", "自定义"})
        Me.cmbOptions.Location = New System.Drawing.Point(12, 60)
        Me.cmbOptions.Name = "cmbOptions"
        Me.cmbOptions.Size = New System.Drawing.Size(300, 20)
        Me.cmbOptions.TabIndex = 2
        '
        'frmOptions
        '
        Me.AcceptButton = Me.btnInstall
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(624, 442)
        Me.Controls.Add(Me.cmbOptions)
        Me.Controls.Add(Me.lblTitle2)
        Me.Controls.Add(Me.llbEAEULA)
        Me.Controls.Add(Me.chkEAEULA)
        Me.Controls.Add(Me.lblNeedsDiskSpace)
        Me.Controls.Add(Me.grpOptionsDetail)
        Me.Controls.Add(Me.tvwOptions)
        Me.Controls.Add(Me.txtSC4InstallDir)
        Me.Controls.Add(Me.btnSC4InstallDir)
        Me.Controls.Add(Me.lblTitle)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.btnInstall)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.lblSC4lInstallDir)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmOptions"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "模拟城市4 豪华版 自动安装程序"
        Me.grpOptionsDetail.ResumeLayout(False)
        Me.grpOptionsDetail.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtSC4InstallDir As System.Windows.Forms.TextBox
    Friend WithEvents btnSC4InstallDir As System.Windows.Forms.Button
    Friend WithEvents lblSC4lInstallDir As System.Windows.Forms.Label
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnInstall As System.Windows.Forms.Button
    Friend WithEvents btnBack As System.Windows.Forms.Button
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents tvwOptions As System.Windows.Forms.TreeView
    Friend WithEvents grpOptionsDetail As System.Windows.Forms.GroupBox
    Friend WithEvents lblOptionsDiskSpace As System.Windows.Forms.Label
    Friend WithEvents lblOptionsDetail As System.Windows.Forms.Label
    Friend WithEvents lblNeedsDiskSpace As System.Windows.Forms.Label
    Friend WithEvents btnDAEMONlInstallDir As System.Windows.Forms.Button
    Friend WithEvents lblDAEMONlInstallDir As System.Windows.Forms.Label
    Friend WithEvents txtDAEMONlInstallDir As System.Windows.Forms.TextBox
    Friend WithEvents fbdSC4InstallDir As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents fbdDAEMONlInstallDir As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents imgOptions As System.Windows.Forms.ImageList
    Friend WithEvents chkEAEULA As System.Windows.Forms.CheckBox
    Friend WithEvents llbEAEULA As System.Windows.Forms.LinkLabel
    Friend WithEvents lblTitle2 As System.Windows.Forms.Label
    Friend WithEvents cmbOptions As System.Windows.Forms.ComboBox
End Class
