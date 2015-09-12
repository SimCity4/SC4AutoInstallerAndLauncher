﻿''' <summary>ModuleInstallModule 模块提供安装各种组件的方法</summary>
Module ModuleInstallModule

    ''' <summary>判断某个文件是否正在使用</summary>
    ''' <param name="path">要判断的文件的路径</param>
    ''' <returns>如果文件正在被使用，则为True；否则为False</returns>
    Private Function IsFileUsing(path As String) As Boolean
        Try
            Using IO.File.Open(path, IO.FileMode.Open)
                Return False
            End Using
        Catch
            Return True
        End Try
    End Function

    ''' <summary>在指定的路径安装DAEMON Tools Lite</summary>
    ''' <param name="InstallDir">指定安装路径</param>
    ''' <returns>InstallResult.Result 的值之一，如果安装成功，则为InstallResult.Result.Success；否则为InstallResult.Result.Fail</returns>
    Public Function InstallDAEMONTools(ByVal InstallDir As String) As InstallResult.Result
        Try
            Do Until IsFileUsing("Data\DAEMON Tools Lite 5.0.exe") = False : Loop
            '以管理员权限启动DAEMON Tools Lite安装程序以便静默安装DAEMON Tools Lite，并等待其安装完成
            Process.Start(New ProcessStartInfo With {.FileName = "Data\DAEMON Tools Lite 5.0.exe", .Arguments = "/S /nogadget /path """ & InstallDir & """", .Verb = "runas"}).WaitForExit()
            Return IIf(My.Computer.FileSystem.FileExists(InstallDir & "\DTLite.exe"), InstallResult.Result.Success, InstallResult.Result.Fail)
        Catch
            Return InstallResult.Result.Fail
        End Try
    End Function

    ''' <summary>FindWindow函数检索处理顶级窗口的类名和窗口名称匹配指定的字符串。该函数不搜索子窗口。</summary>
    ''' <param name="lpClassName">指向一个以null结尾的、用来指定类名的字符串或一个可以确定类名字符串的原子。</param>
    ''' <param name="lpWindowName">指向一个以null结尾的、用来指定窗口名（即窗口标题）的字符串。如果此参数为NULL，则匹配所有窗口名。</param>
    ''' <returns>如果函数执行成功，则返回值是拥有指定窗口类名或窗口名的窗口的句柄。</returns>
    Public Declare Function FindWindow Lib "user32" Alias "FindWindowA" (ByVal lpClassName As String, ByVal lpWindowName As String) As IntPtr
    ''' <summary>在窗口列表中寻找与指定条件相符的第一个子窗口。</summary>
    ''' <param name="parentHandle">要查找的子窗口所在的父窗口的句柄。</param> 
    ''' <param name="childAfter">子窗口句柄。查找从在Z序中的下一个子窗口开始。</param>
    ''' <param name="lclassName">指向一个指定了类名的空结束字符串，或一个标识类名字符串的成员的指针。</param>
    ''' <param name="windowTitle">指向一个指定了窗口名（窗口标题）的空结束字符串。如果该参数为 NULL，则为所有窗口全匹配。</param>
    ''' <returns>如果函数成功，返回值为具有指定类名和窗口名的窗口句柄。如果函数失败，返回值为NULL。</returns>
    Public Declare Function FindWindowEx Lib "user32" Alias "FindWindowExA" (ByVal parentHandle As IntPtr, ByVal childAfter As IntPtr, ByVal lclassName As String, ByVal windowTitle As String) As IntPtr
    ''' <summary>该函数将指定的消息发送到一个或多个窗口。此函数为指定的窗口调用窗口程序，直到窗口程序处理完消息再返回。</summary>
    ''' <param name="hWnd">其窗口程序将接收消息的窗口的句柄。如果此参数为HWND_BROADCAST，则消息将被发送到系统中所有顶层窗口，包括无效或不可见的非自身拥有的窗口、被覆盖的窗口和弹出式窗口，但消息不被发送到子窗口。</param>
    ''' <param name="Msg">指定被发送的消息。</param>
    ''' <param name="wParam">指定附加的消息特定信息。</param>
    ''' <param name="lParam">指定附加的消息特定信息。</param>
    ''' <returns>返回值指定消息处理的结果，依赖于所发送的消息。</returns>
    ''' <remarks>需要用HWND_BROADCAST通信的应用程序应当使用函数RegisterWindowMessage来为应用程序间的通信取得一个唯一的消息。</remarks>
    Public Declare Function SendMessage Lib "user32" Alias "SendMessageA" (ByVal hWnd As IntPtr, ByVal Msg As UInteger, ByVal wParam As IntPtr, ByVal lParam As String) As IntPtr
    ''' <summary>该函数将一个消息放入（寄送）到与指定窗口创建的线程相联系消息队列里，不等待线程处理消息就返回，是异步消息模式。</summary>
    ''' <param name="hWnd">其窗口程序接收消息的窗口的句柄。</param>
    ''' <param name="Msg">指定被寄送的消息。</param>
    ''' <param name="wParam">指定附加的消息特定的信息。</param>
    ''' <param name="lParam">指定附加的消息特定的信息。</param>
    ''' <returns>如果函数调用成功，返回非零，否则返回值为零</returns>
    Public Declare Function PostMessage Lib "user32" Alias "PostMessageA" (ByVal hWnd As IntPtr, ByVal Msg As UInteger, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As Boolean
    ''' <summary>当用户在window客户区域点击鼠标左键的时候发送。如果当前鼠标没有被捕捉，消息发送给鼠标下面的window窗体。否则，消息发送给当前捕捉鼠标消息的方法。</summary>
    Public Const WM_LBUTTONDOWN = &H201
    ''' <summary>当光标在窗口客户区时，用户释放鼠标左键时发出的消息。如果鼠标没有捕获，这个消息被送到光标下的窗口。否则，该消息发布到捕获鼠标的窗口。</summary>
    Public Const WM_LBUTTONUP = &H202
    ''' <summary>Sets the text of a window.</summary>
    Public Const WM_SETTEXT = &HC

    ''' <summary>递归查询一个文件夹内所有的文件和文件夹的数量</summary>
    ''' <param name="path">要查询的文件夹的路径</param>
    ''' <returns>返回文件夹内所有的文件和文件夹的数量</returns>
    Private Function GetFolderCount(ByVal path As String) As Long
        Dim count As Long
        count += New IO.DirectoryInfo(path).GetFiles.Count
        For Each i As IO.DirectoryInfo In New IO.DirectoryInfo(path).GetDirectories
            count += GetFolderCount(i.FullName) '递归返回子文件夹的大小
        Next
        Return count
    End Function

    ''' <summary>在指定的路径安装指定版本的模拟城市4并在指定的System.Windows.Forms.ProgressBar实例上显示安装进度</summary>
    ''' <param name="SC4InstallDir">指定安装路径</param>
    ''' <param name="DAEMONInstallDir">DAEMON Tools Lite的安装路径，用于安装镜像版模拟城市4</param>
    ''' <param name="InstallType">InstallOptions.SC4InstallType 的值之一，指定要安装的版本</param>
    ''' <param name="ProgressBar">指定用于显示安装进度的System.Windows.Forms.ProgressBar实例</param>
    ''' <returns>InstallResult.Result 的值之一，如果安装成功，则为InstallResult.Result.Success；否则为InstallResult.Result.Fail</returns>
    Public Function InstallSC4(ByVal SC4InstallDir As String, ByVal DAEMONInstallDir As String, ByVal InstallType As InstallOptions.SC4InstallType, ByVal ProgressBar As ProgressBar) As InstallResult.Result
        Try
            My.Computer.FileSystem.CopyFile(Application.ExecutablePath, SC4InstallDir & "\Setup.exe", True) '将安装程序复制到游戏安装目录下
            Dim _7zaProcess As Process '声明一个System.Diagnostics.Process类的实例以便记录7za.exe进程的退出代码
            ProgressBar.Style = ProgressBarStyle.Continuous : ProgressBar.Maximum = 546 '初始化ProgressBar参数实例的进度条类型和最大值
            If InstallType = InstallOptions.SC4InstallType.ISO Then
                Dim StartupPath As String = Application.StartupPath '声明一个用于存储程序当前所在目录的字符串变量
                If Application.StartupPath.EndsWith("\") = True Then StartupPath = Application.StartupPath.Remove(Application.StartupPath.LastIndexOf("\"), 1) '如果程序存储在分区根目录下则去掉结尾的\
                Do Until My.Computer.FileSystem.FileExists("X:\AutoRun.exe") '将CD1虚拟光驱镜像文件加载到X盘符上
                    '以管理员权限启动DAEMON Tools Lite以便加载CD1，并等待其加载完成
                    Process.Start(New ProcessStartInfo With {.FileName = DAEMONInstallDir & "\DTLite.exe", .Arguments = "-mount dt, X, """ & StartupPath & "\Data\SC4\CD\CD1.mdf""", .Verb = "runas"}).WaitForExit()
                Loop
                Do Until My.Computer.FileSystem.FileExists("Y:\RunGame.exe") '将CD2虚拟光驱镜像文件加载到Y盘符上
                    '以管理员权限启动DAEMON Tools Lite以便加载CD2，并等待其加载完成
                    Process.Start(New ProcessStartInfo With {.FileName = DAEMONInstallDir & "\DTLite.exe", .Arguments = "-mount dt, Y, """ & StartupPath & "\Data\SC4\CD\CD2.mdf""", .Verb = "runas"}).WaitForExit()
                Loop

                '如果安装程序正在运行则结束安装程序的进程
                If Process.GetProcessesByName("AutoRun").Length <> 0 Then Process.GetProcessesByName("AutoRun")(0).Kill()
                If Process.GetProcessesByName("SimCity 4 Deluxe_Code").Length <> 0 Then Process.GetProcessesByName("SimCity 4 Deluxe_Code")(0).Kill()
                If Process.GetProcessesByName("SimCity 4 Deluxe_eReg").Length <> 0 Then Process.GetProcessesByName("SimCity 4 Deluxe_eReg")(0).Kill()

                Dim TempFolder As String = IO.Path.GetTempPath '声明一个用于存储临时文件目录的路径的字符串变量
                If My.Computer.FileSystem.FileExists(TempFolder & "\AutoRun.exe") = False Or My.Computer.FileSystem.FileExists(TempFolder & "\AutoRunGUI.dll") = False Then
                    '将X:\AutoRun.exe文件和X:\AutoRunGUI.dll文件复制到临时文件目录下
                    My.Computer.FileSystem.CopyFile("X:\AutoRun.exe", TempFolder & "\AutoRun.exe", FileIO.UIOption.OnlyErrorDialogs, FileIO.UICancelOption.DoNothing)
                    My.Computer.FileSystem.CopyFile("X:\AutoRunGUI.dll", TempFolder & "\AutoRunGUI.dll", FileIO.UIOption.OnlyErrorDialogs, FileIO.UICancelOption.DoNothing)
                    Process.Start(New ProcessStartInfo With {.FileName = "regsvr32.exe", .Arguments = "/s """ & TempFolder & "\AutoRunGUI.dll""", .Verb = "runas"}).WaitForExit() '以管理器权限启动regsvr32.exe以便注册AutoRunGUI.dll文件
                End If

                Dim SetupProcess As Process '声明一个System.Diagnostics.Process类的实例以便记录AutoRun.exe进程的退出代码
                SetupProcess = Process.Start(New ProcessStartInfo With {.FileName = TempFolder & "\AutoRun.exe", .Arguments = "-restart -dir X:\", .Verb = "runas"}) '以管理员权限启动临时文件目录下的安装程序

                Do Until FindWindow("#32770", "SimCity 4 Deluxe") <> Nothing : Loop '等待主窗口的出现
                PostMessage(FindWindowEx(FindWindow("#32770", "SimCity 4 Deluxe"), 0, "Button", "Install"), WM_LBUTTONDOWN, 0, 0) '模拟点击安装按钮
                PostMessage(FindWindowEx(FindWindow("#32770", "SimCity 4 Deluxe"), 0, "Button", "Install"), WM_LBUTTONUP, 0, 0)

                Threading.Thread.Sleep(100) : Do Until Process.GetProcessesByName("SimCity 4 Deluxe_Code").Length <> 0 And FindWindow("#32770", "SimCity 4 Deluxe") <> Nothing : Loop '等待SimCity 4 Deluxe_Code.exe进程和序列号窗口的出现
                Dim key As String() = {"CX9H", "498A", "MHSS", "8QXD", "TXJB"} '声明一个用于存储要在序列号窗口里输入的序列号的字符串数组
                Dim TextBoxs(4) As IntPtr '声明一个用于存储序列号窗口的5个序列号文本框的句柄的字符串数组
                For i As Integer = 0 To 4
                    If i = 0 Then TextBoxs(i) = FindWindowEx(FindWindow("#32770", "SimCity 4 Deluxe"), 0, "Edit", "")
                    If i <> 0 Then TextBoxs(i) = FindWindowEx(FindWindow("#32770", "SimCity 4 Deluxe"), TextBoxs(i - 1), "Edit", "")
                    SendMessage(TextBoxs(i), WM_SETTEXT, 0, key(i)) '通过SendMessage向第i个序列号文本框发送WM_SETTEXT消息（lParam附加值为key(i)的值）来模拟输入序列号
                Next
                '通过PostMessage API向序列号对话框发送WM_LBUTTONDOWN消息和WM_LBUTTONUP来模拟点击下一步按钮
                PostMessage(FindWindowEx(FindWindow("#32770", "SimCity 4 Deluxe"), 0, "Button", "&Next>"), WM_LBUTTONDOWN, 0, 0)
                PostMessage(FindWindowEx(FindWindow("#32770", "SimCity 4 Deluxe"), 0, "Button", "&Next>"), WM_LBUTTONUP, 0, 0)

                Threading.Thread.Sleep(100) : Do Until FindWindow("#32770", "SimCity 4 Deluxe") <> Nothing : Loop '等待安装路径对话框的出现
                '通过SendMessage向安装路径文本框发送WM_SETTEXT消息（lParam附加值为InstallDir参数的值）来模拟输入安装路径
                SendMessage(FindWindowEx(FindWindow("#32770", "SimCity 4 Deluxe"), 0, "Edit", ""), WM_SETTEXT, 0, SC4InstallDir)
                '通过PostMessage API向安装路径对话框发送WM_LBUTTONDOWN消息和WM_LBUTTONUP来模拟点击下一步按钮
                PostMessage(FindWindowEx(FindWindow("#32770", "SimCity 4 Deluxe"), 0, "Button", "&Next>"), WM_LBUTTONDOWN, 0, 0)
                PostMessage(FindWindowEx(FindWindow("#32770", "SimCity 4 Deluxe"), 0, "Button", "&Next>"), WM_LBUTTONUP, 0, 0)

                Threading.Thread.Sleep(100) : Do Until Process.GetProcessesByName("SimCity 4 Deluxe_eReg").Length <> 0 And FindWindow("#32770", "Electronic Registration") <> Nothing : Loop '等待SimCity 4 Deluxe_eReg.exe进程和注册EA账号对话框的出现
                PostMessage(FindWindowEx(FindWindow("#32770", "Electronic Registration"), 0, "Button", "Register Later"), WM_LBUTTONDOWN, 0, 0) '模拟点击以后注册按钮
                PostMessage(FindWindowEx(FindWindow("#32770", "Electronic Registration"), 0, "Button", "Register Later"), WM_LBUTTONUP, 0, 0)
                Threading.Thread.Sleep(100) : Do Until FindWindow("#32770", "") <> Nothing : Loop '等待确定不注册EA账号对话框的出现
                '通过PostMessage API向注册EA账号对话框发送WM_LBUTTONDOWN消息和WM_LBUTTONUP来模拟点击不注册按钮
                PostMessage(FindWindowEx(FindWindow("#32770", ""), 0, "Button", "Ok"), WM_LBUTTONDOWN, 0, 0)
                PostMessage(FindWindowEx(FindWindow("#32770", ""), 0, "Button", "Ok"), WM_LBUTTONUP, 0, 0)

                Do Until SetupProcess.HasExited = True '在ProgressBar参数实例上显示安装进度并等待安装程序退出
                    ProgressBar.Value = GetFolderCount(SC4InstallDir)
                    Threading.Thread.Sleep(500) '挂起当前线程500毫秒以便让用户看到安装进度
                Loop
                'SetupProcess.WaitForExit() '等待安装程序完成安装并退出
                If Process.GetProcessesByName("SimCity 4").Length <> 0 Then Process.GetProcessesByName("SimCity 4")(0).Kill() '结束安装完成后自动启动的游戏进程
                ProgressBar.Style = ProgressBarStyle.Marquee '将ProgressBar参数实例的进度条类型改为循环滚动

                '将GOG版模拟城市4的Graphics Rules.sgr文件解压到游戏安装目录下
                '以管理员权限启动7za.exe并隐藏进程窗口以便将Data\SC4\NoInstall.7z压缩包的Graphics Rules.sgr文件解压到游戏安装目录下替换源文件并等待其完成解压缩
                _7zaProcess = Process.Start(New ProcessStartInfo With {.FileName = "7za.exe", .Arguments = "x Data\SC4\NoInstall.7z -aoa ""Graphics Rules.sgr"" -o""" & SC4InstallDir & """" _
                                                                          , .Verb = "runas", .WindowStyle = ProcessWindowStyle.Hidden}) : _7zaProcess.WaitForExit()
                Return IIf(_7zaProcess.ExitCode = 0 Or My.Computer.FileSystem.FileExists(SC4InstallDir & "\Apps\SimCity 4.exe"), InstallResult.Result.Success, InstallResult.Result.Fail)
            ElseIf InstallType = InstallOptions.SC4InstallType.NoInstall Then
                Do Until IsFileUsing("Data\SC4\NoInstall.7z") = False : Loop '确保没有进程正在使用Data\SC4\NoInstall.7z文件
                '以管理员权限启动7za.exe并隐藏进程窗口以便将Data\SC4\NoInstall.7z压缩包的所有文件解压到游戏安装目录下替换源文件
                _7zaProcess = Process.Start(New ProcessStartInfo With {.FileName = "7za.exe", .Arguments = "x Data\SC4\NoInstall.7z -aoa -o""" & SC4InstallDir & """", .Verb = "runas", .WindowStyle = ProcessWindowStyle.Hidden})
                Do Until _7zaProcess.HasExited = True '在ProgressBar参数实例上显示安装进度并等待解压程序退出
                    ProgressBar.Value = GetFolderCount(SC4InstallDir)
                    Threading.Thread.Sleep(500) '挂起当前线程500毫秒以便让用户看到安装进度
                Loop
                Return IIf(_7zaProcess.ExitCode = 0, InstallResult.Result.Success, InstallResult.Result.Fail)
            End If
            ProgressBar.Value = 0 : ProgressBar.Style = ProgressBarStyle.Marquee '将ProgressBar参数实例的进度和进度条类型改为0和循环滚动
        Catch
            Return InstallResult.Result.Fail '如果在安装过程中遇到异常则返回安装失败
        End Try
    End Function

    ''' <summary>在指定路径安装或卸载638补丁并在指定的System.Windows.Forms.ProgressBar实例上显示安装进度</summary>
    ''' <param name="InstallDir">指定安装路径</param>
    ''' <param name="IsUninstall">指定是否卸载638补丁</param>
    ''' ''' <param name="ProgressBar">指定用于显示安装或卸载进度的System.Windows.Forms.ProgressBar实例</param>
    ''' <returns>InstallResult.Result 的值之一，如果安装或卸载成功，则为InstallResult.Result.Success；否则为InstallResult.Result.Fail</returns>
    Public Function Install638Patch(ByVal InstallDir As String, ByVal IsUninstall As Boolean, ByVal ProgressBar As ProgressBar) As InstallResult.Result
        Try
            Dim _7zaProcess As Process '声明一个System.Diagnostics.Process类的实例以便记录7za.exe进程的退出代码
            ProgressBar.Style = ProgressBarStyle.Continuous : ProgressBar.Maximum = 4 : ProgressBar.Value = 0
            If IsUninstall = False Then
                Dim Files As String() = {"Data\Patch\638.7z", InstallDir & "\Apps\SimCity 4.exe", InstallDir & "\SimCity_1.dat", InstallDir & "\SimCity_2.dat" _
                                        , InstallDir & "\SimCity_3.dat", InstallDir & "\SimCity_4.dat"} '声明一个用于存储要验证是否正在被进程使用的文件列表的字符串数组
                For Each i As String In Files : Do Until IsFileUsing(i) = False : Loop : Next '确保没有进程正在使用Files字符串数组所存储的文件
                '以管理员权限启动7za.exe并隐藏进程窗口以便将Data\Patch\638.7z压缩包的所有文件解压到游戏安装目录下替换源文件
                _7zaProcess = Process.Start(New ProcessStartInfo With {.FileName = "7za.exe", .Arguments = "x Data\Patch\638.7z -aoa -o""" & InstallDir & """", .Verb = "runas", .WindowStyle = ProcessWindowStyle.Hidden})
                Do Until _7zaProcess.HasExited = True '在ProgressBar参数实例上显示安装进度并等待解压程序退出
                    '声明一个用于存储当前游戏安装目录\SimCity_1到4.dat文件和638补丁安装后的游戏安装目录\SimCity_1到4.dat文件的最后修改日期的字符串数组
                    Dim FilesLastWriteTime As Date() = {New IO.FileInfo(InstallDir & "\SimCity_1.dat").LastWriteTime, #2003-11-14 16:17:23# _
                                                       , New IO.FileInfo(InstallDir & "\SimCity_2.dat").LastWriteTime, #2003-11-14 16:17:22# _
                                                       , New IO.FileInfo(InstallDir & "\SimCity_3.dat").LastWriteTime, #2003-11-14 16:17:22# _
                                                       , New IO.FileInfo(InstallDir & "\SimCity_4.dat").LastWriteTime, #2003-11-14 16:17:22#}
                    Dim progress As Integer = 0 '声明一个用于存储当前安装进度的整形变量
                    For i As Integer = 0 To FilesLastWriteTime.Count - 1 Step 2 '确定安装进度
                        If FilesLastWriteTime(i) = FilesLastWriteTime(i + 1) Then progress += 1
                    Next
                    ProgressBar.Value = progress '在ProgressBar参数实例上显示安装进度
                    Threading.Thread.Sleep(500) '挂起当前线程500毫秒以便让用户看到安装进度
                Loop
            Else
                Dim Files As String() = {"Data\SC4\NoInstall.7z", InstallDir & "\Apps\SimCity 4.exe", InstallDir & "\SimCity_1.dat", InstallDir & "\SimCity_2.dat", InstallDir & "\SimCity_3.dat" _
                                        , InstallDir & "\SimCity_4.dat", InstallDir & "\SimCity_5.dat"} '声明一个用于存储要验证是否正在被进程使用的文件列表的字符串数组
                For Each i As String In Files : Do Until IsFileUsing(i) = False : Loop : Next '确保没有进程正在使用Files字符串数组所存储的文件
                '以管理员权限启动7za.exe并隐藏进程窗口以便将Data\SC4\NoInstall.7z压缩包的Apps\SimCity 4.exe和SimCity_1到5.dat文件解压到游戏安装目录下替换源文件并等待其完成解压缩
                _7zaProcess = Process.Start(New ProcessStartInfo With {.FileName = "7za.exe", .Arguments = "x Data\SC4\NoInstall.7z ""Apps\SimCity 4.exe"" ""SimCity_*.dat"" -aoa -o""" & InstallDir & """", .Verb = "runas", .WindowStyle = ProcessWindowStyle.Hidden})
                Do Until _7zaProcess.HasExited = True '在ProgressBar参数实例上显示安装进度并等待解压程序退出
                    ProgressBar.Value = 0 '初始化ProgressBar参数实例的进度
                    '声明一个用于存储当前游戏安装目录\SimCity_1到4.dat文件和638补丁卸载后的游戏安装目录\SimCity_1到4.dat文件的最后修改日期的字符串数组
                    Dim FilesLastWriteTime As Date() = {New IO.FileInfo(InstallDir & "\SimCity_1.dat").LastWriteTime, #2003-08-27 16:12:04# _
                                                       , New IO.FileInfo(InstallDir & "\SimCity_2.dat").LastWriteTime, #2003-08-27 16:12:04# _
                                                       , New IO.FileInfo(InstallDir & "\SimCity_3.dat").LastWriteTime, #2003-08-27 16:12:04# _
                                                       , New IO.FileInfo(InstallDir & "\SimCity_4.dat").LastWriteTime, #2003-08-27 16:12:04#}
                    Dim progress As Integer = 0 '声明一个用于存储当前安装进度的整形变量
                    For i As Integer = 0 To FilesLastWriteTime.Count - 1 Step 2 '确定安装进度
                        If FilesLastWriteTime(i) = FilesLastWriteTime(i + 1) Then progress += 1
                    Next
                    ProgressBar.Value = progress '在ProgressBar参数实例上显示安装进度
                    Threading.Thread.Sleep(500) '挂起当前线程500毫秒以便让用户看到安装进度
                Loop
            End If
            ProgressBar.Value = 0 : ProgressBar.Style = ProgressBarStyle.Marquee '将ProgressBar参数实例的进度和进度条类型改为0和循环滚动
            Return IIf(_7zaProcess.ExitCode = 0, InstallResult.Result.Success, InstallResult.Result.Fail)
        Catch
            Return InstallResult.Result.Fail '如果在安装过程中遇到异常则返回安装失败
        End Try
    End Function

    ''' <summary>在指定路径安装或卸载640补丁</summary>
    ''' <param name="InstallDir">指定安装路径</param>
    ''' <param name="IsUninstall">指定是否卸载640补丁</param>
    ''' <param name="ProgressBar">指定用于显示卸载（安装638补丁）进度的System.Windows.Forms.ProgressBar实例</param>
    ''' <returns>InstallResult.Result 的值之一，如果安装或卸载成功，则为InstallResult.Result.Success；否则为InstallResult.Result.Fail</returns>
    Public Function Install640Patch(ByVal InstallDir As String, ByVal IsUninstall As Boolean, ByVal ProgressBar As ProgressBar) As InstallResult.Result
        Try
            Dim _7zaProcess As Process '声明一个System.Diagnostics.Process类的实例以便记录7za.exe进程的退出代码
            If IsUninstall = False Then
                Dim Files As String() = {"Data\Patch\640.7z", InstallDir & "\Apps\SimCity 4.exe", InstallDir & "\SimCity_1.dat"} '声明一个用于存储要验证是否正在被进程使用的文件列表的字符串数组
                For Each i As String In Files : Do Until IsFileUsing(i) = False : Loop : Next '确保没有进程正在使用Files字符串数组所存储的文件
                '以管理员权限启动7za.exe并隐藏进程窗口以便将Data\Patch\640.7z压缩包的所有文件解压到游戏安装目录下替换源文件并等待其完成解压缩
                _7zaProcess = Process.Start(New ProcessStartInfo With {.FileName = "7za.exe", .Arguments = "x Data\Patch\640.7z -aoa -o""" & InstallDir & """", .Verb = "runas", .WindowStyle = ProcessWindowStyle.Hidden})
                _7zaProcess.WaitForExit()
            Else
                Return Install638Patch(InstallDir, False, ProgressBar) '直接调用安装638补丁的方法
            End If
            Return IIf(_7zaProcess.ExitCode = 0, InstallResult.Result.Success, InstallResult.Result.Fail)
        Catch
            Return InstallResult.Result.Fail '如果在安装过程中遇到异常则返回安装失败
        End Try
    End Function

    ''' <summary>在指定路径安装或卸载641补丁</summary>
    ''' <param name="InstallDir">指定安装路径</param>
    ''' <param name="IsUninstall">指定是否卸载641补丁</param>
    ''' <returns>InstallResult.Result 的值之一，如果安装或卸载成功，则为InstallResult.Result.Success；否则为InstallResult.Result.Fail</returns>
    Public Function Install641Patch(ByVal InstallDir As String, ByVal IsUninstall As Boolean) As InstallResult.Result
        Try
            If IsUninstall = False Then
                Do Until IsFileUsing(InstallDir & "\Apps\SimCity 4.exe") = False : Loop  '确保没有进程正在使用游戏安装目录\Apps\SimCity_4.exe文件
                My.Computer.FileSystem.CopyFile("Data\Patch\SimCity 4 641.exe", InstallDir & "\Apps\SimCity 4.exe", True) '将Data\Patch\SimCity 4 641.exe复制到游戏安装目录\Apps目录下并重命名为SimCity 4.exe替换源文件
                Return IIf(My.Computer.FileSystem.GetFileInfo(InstallDir & "\Apps\SimCity 4.exe").Length = 7524352, InstallResult.Result.Success, InstallResult.Result.Fail)
            Else
                Return Install640Patch(InstallDir, False, Nothing) '直接调用安装640补丁的方法
            End If
        Catch
            Return InstallResult.Result.Fail '如果在安装过程中遇到异常则返回安装失败
        End Try
    End Function

    ''' <summary>在指定路径安装或卸载4GB补丁</summary>
    ''' <param name="InstallDir">指定安装路径</param>
    ''' <param name="IsUninstall">指定是否卸载4GB补丁</param>
    ''' <param name="InstallOptions">一个InstallOptions类的实例，用于判断应回滚至什么版本</param>
    ''' <returns>InstallResult.Result 的值之一，如果安装或卸载成功，则为InstallResult.Result.Success；否则为InstallResult.Result.Fail</returns>
    Public Function Install4GBPatch(ByVal InstallDir As String, ByVal IsUninstall As Boolean, ByVal InstallOptions As InstallOptions) As InstallResult.Result
        With InstallOptions
            Try
                If IsUninstall = False Then
                    Do Until IsFileUsing(InstallDir & "\Apps\SimCity 4.exe") = False : Loop '确保没有进程正在使用游戏安装目录\Apps\SimCity_4.exe文件
                    Process.Start(New ProcessStartInfo With {.FileName = "Data\Patch\4GB.exe", .Arguments = """" & InstallDir & "\Apps\SimCity 4.exe""", .Verb = "runas"}).WaitForExit() '以管理员权限启动4GB.exe以便安装4GB补丁并等待其完成安装
                    '声明一个用于计算MD5值的System.Security.Cryptography.MD5CryptoServiceProvider类实例和一个用于存储游戏安装目录\Apps\SimCity 4.exe文件的MD5值的字符串变量
                    Dim MD5CSP As New Security.Cryptography.MD5CryptoServiceProvider, MD5 As String
                    MD5 = BitConverter.ToString(MD5CSP.ComputeHash(New IO.FileStream(InstallDir & "\Apps\SimCity 4.exe", IO.FileMode.Open))).Replace("-", "") '将游戏安装目录\Apps\SimCity 4.exe文件的MD5值存储到变量MD5里
                    '验证游戏安装目录\Apps\SimCity 4.exe文件的MD5值来确定是否成功安装4GB补丁
                    Return IIf(MD5 = "78202C3EF76988BD2BF05F8D223BE7A3" Or MD5 = "2F2BD7D9A76E85320A26D7BD7530DCAE" Or MD5 = "1C18B7DC760EDADD2C2EFAF33F60F150" _
                               Or MD5 = "1414E70EB5CE22DB37D22CB99439D012" Or MD5 = "AADC5464919FBDC0F8E315FA51582126", InstallResult.Result.Success, InstallResult.Result.Fail)
                Else
                    Dim _7zaProcess As Process = Nothing '声明一个System.Diagnostics.Process类的实例以便记录7za.exe进程的退出代码
                    If .Install638Patch = True And .Install640Patch = False And .Install641Patch = False Then
                        Do Until IsFileUsing("Data\Patch\638.7z") = False And IsFileUsing(InstallDir & "\Apps\SimCity 4.exe") = False : Loop '确保没有进程正在使用Data\Patch\638.7z和游戏安装目录\Apps\SimCity 4.exe文件
                        '以管理员权限启动7za.exe并隐藏进程窗口以便将Data\Patch\638.7z压缩包的Apps\SimCity 4.exe文件解压到游戏安装目录下替换源文件并等待其完成解压缩
                        _7zaProcess = Process.Start(New ProcessStartInfo With {.FileName = "7za.exe", .Arguments = "x Data\Patch\638.7z ""Apps\SimCity 4.exe"" -aoa -o""" & InstallDir & """", .Verb = "runas", .WindowStyle = ProcessWindowStyle.Hidden})
                        _7zaProcess.WaitForExit()
                        Return IIf(_7zaProcess.ExitCode = 0, InstallResult.Result.Success, InstallResult.Result.Fail)
                    ElseIf .Install638Patch = True And .Install640Patch = True And .Install641Patch = False Then
                        Do Until IsFileUsing("Data\Patch\640.7z") = False And IsFileUsing(InstallDir & "\Apps\SimCity 4.exe") = False : Loop '确保没有进程正在使用Data\Patch\640.7z文件
                        '以管理员权限启动7za.exe并隐藏进程窗口以便将Data\Patch\640.7z压缩包的Apps\SimCity 4.exe文件解压到游戏安装目录下替换源文件并等待其完成解压缩
                        _7zaProcess = Process.Start(New ProcessStartInfo With {.FileName = "7za.exe", .Arguments = "x Data\Patch\640.7z ""Apps\SimCity 4.exe"" -aoa -o""" & InstallDir & """", .Verb = "runas", .WindowStyle = ProcessWindowStyle.Hidden})
                        _7zaProcess.WaitForExit()
                        Return IIf(_7zaProcess.ExitCode = 0, InstallResult.Result.Success, InstallResult.Result.Fail)
                    ElseIf .Install638Patch = True And .Install640Patch = True And .Install641Patch = True Then
                        Return Install641Patch(InstallDir, False) '直接调用安装641补丁的方法
                    ElseIf .Install638Patch = False And .Install640Patch = False And .Install641Patch = False Then
                        Do Until IsFileUsing("Data\SC4\NoInstall.7z") = False And IsFileUsing(InstallDir & "\Apps\SimCity 4.exe") = False : Loop '确保没有进程正在使用Data\SC4\NoInstall.7z和游戏安装目录\Apps\SimCity 4.exe文件
                        '以管理员权限启动7za.exe并隐藏进程窗口以便将Data\SC4\NoInstall.7z压缩包的Apps\SimCity 4.exe文件解压到游戏安装目录下替换源文件并等待其完成解压缩
                        _7zaProcess = Process.Start(New ProcessStartInfo With {.FileName = "7za.exe", .Arguments = "x Data\SC4\NoInstall.7z ""Apps\SimCity 4.exe"" -aoa -o""" & InstallDir & """", .Verb = "runas", .WindowStyle = ProcessWindowStyle.Hidden})
                        _7zaProcess.WaitForExit()
                        Return IIf(_7zaProcess.ExitCode = 0, InstallResult.Result.Success, InstallResult.Result.Fail)
                    ElseIf .InstallNoCDPatch = True Then
                        Return InstallNoCDPatch(InstallDir, False, Nothing) '直接调用安装免CD补丁的方法
                    End If
                End If
            Catch
                Return InstallResult.Result.Fail '如果在安装过程中遇到异常则返回安装失败
            End Try
        End With
    End Function

    ''' <summary>在指定路径安装或卸载免CD补丁</summary>
    ''' <param name="InstallDir">指定安装路径</param>
    ''' <param name="IsUninstall">指定是否卸载免CD补丁</param>
    ''' <param name="ProgressBar">指定用于显示卸载（卸载638补丁）进度的System.Windows.Forms.ProgressBar实例</param>
    ''' <returns>InstallResult.Result 的值之一，如果安装或卸载成功，则为InstallResult.Result.Success；否则为InstallResult.Result.Fail</returns>
    Public Function InstallNoCDPatch(ByVal InstallDir As String, ByVal IsUninstall As Boolean, ByVal ProgressBar As ProgressBar) As InstallResult.Result
        Try
            If IsUninstall = False Then
                Do Until IsFileUsing(InstallDir & "\Apps\SimCity 4.exe") = False : Loop '确保没有进程正在使用游戏安装目录\Apps\SimCity_4.exe文件
                My.Computer.FileSystem.CopyFile("Data\Patch\SimCity 4 NoCD.exe", InstallDir & "\Apps\SimCity 4.exe", True) '将Data\Patch\SimCity 4 NoCD.exe复制到游戏安装目录\Apps目录下并重命名为SimCity 4.exe替换源文件
                Return IIf(My.Computer.FileSystem.GetFileInfo(InstallDir & "\Apps\SimCity 4.exe").Length = 7524352, InstallResult.Result.Success, InstallResult.Result.Fail)
            Else
                Return Install638Patch(InstallDir, True, ProgressBar) '直接调用卸载638补丁的方法
            End If
        Catch
            Return InstallResult.Result.Fail '如果在安装过程中遇到异常则返回安装失败
        End Try
    End Function

    ''' <summary>在指定路径安装或卸载模拟城市4 启动器</summary>
    ''' <param name="InstallDir">指定安装路径</param>
    ''' <param name="IsUninstall">指定是否卸载模拟城市4 启动器</param>
    ''' <returns>InstallResult.Result 的值之一，如果安装或卸载成功，则为InstallResult.Result.Success；否则为InstallResult.Result.Fail</returns>
    Public Function InstallSC4Launcher(ByVal InstallDir As String, ByVal IsUninstall As Boolean) As InstallResult.Result
        Try
            If IsUninstall = False Then
                My.Computer.FileSystem.CopyFile("Data\SC4Launcher.exe", InstallDir & "\SC4Launcher.exe", True) '将Data\SC4Launcher.exe复制到游戏安装目录下替换源文件
                Return IIf(My.Computer.FileSystem.FileExists(InstallDir & "\SC4Launcher.exe"), InstallResult.Result.Success, InstallResult.Result.Fail)
            Else
                Do Until IsFileUsing(InstallDir & "\SC4Launcher.exe") = False : Loop '确保没有进程正在使用游戏安装目录\SC4Launcher.exe文件
                My.Computer.FileSystem.DeleteFile(InstallDir & "\SC4Launcher.exe") '删除游戏安装目录\SC4Launcher.exe文件
                Return IIf(My.Computer.FileSystem.FileExists(InstallDir & "\SC4Launcher.exe") = False, InstallResult.Result.Success, InstallResult.Result.Fail)
            End If
        Catch
            Return InstallResult.Result.Fail '如果在安装过程中遇到异常则返回安装失败
        End Try
    End Function

    ''' <summary>在指定路径安装指定的语言的语言补丁</summary>
    ''' <param name="InstallDir">指定安装路径</param>
    ''' <param name="LanguagePatch">InstallOptions.Language 的值之一，指定要安装的语言补丁的语言</param>
    ''' <returns>InstallResult.Result 的值之一，如果安装或卸载成功，则为InstallResult.Result.Success；否则为InstallResult.Result.Fail</returns>
    Public Function InstallLanguagePatch(ByVal InstallDir As String, ByVal LanguagePatch As InstallOptions.Language) As InstallResult.Result
        With My.Computer.Registry
            Try
                Dim LanguageRegKeyName As String = Nothing '声明一个用于存储模拟城市4的语言设置的注册表键值的字符串变量
                If Environment.Is64BitOperatingSystem = True Then LanguageRegKeyName = "HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Maxis\SimCity 4\1.0"
                If Environment.Is64BitOperatingSystem = False Then LanguageRegKeyName = "HKEY_LOCAL_MACHINE\SOFTWARE\Maxis\SimCity 4\1.0"
                Select Case LanguagePatch
                    Case InstallOptions.Language.TraditionalChinese
                        My.Computer.FileSystem.CopyDirectory("Data\Patch\Language\TChinese", InstallDir & "\TChinese", True) '将Data\Patch\Language\TChinese文件夹复制到游戏安装目录下替换源文件
                        .SetValue(LanguageRegKeyName, "Language", 18, Microsoft.Win32.RegistryValueKind.DWord) '设置繁体中文语言补丁的注册表值
                        .SetValue(LanguageRegKeyName, "DisplayName", "SimCity 4 Deluxe", Microsoft.Win32.RegistryValueKind.String)
                        .SetValue(LanguageRegKeyName, "LanguageName", "Chinese (Traditional)", Microsoft.Win32.RegistryValueKind.String)
                        Return IIf(My.Computer.FileSystem.DirectoryExists(InstallDir & "\TChinese") = True And
                                   My.Computer.Registry.GetValue(LanguageRegKeyName, "Language", Nothing) = 18, InstallResult.Result.Success, InstallResult.Result.Fail)
                    Case InstallOptions.Language.SimplifiedChinese
                        My.Computer.FileSystem.CopyDirectory("Data\Patch\Language\SChinese", InstallDir & "\SChinese", True) '将Data\Patch\Language\SChinese文件夹复制到游戏安装目录下替换源文件
                        .SetValue(LanguageRegKeyName, "Language", 17, Microsoft.Win32.RegistryValueKind.DWord) '设置简体中文语言补丁的注册表值
                        .SetValue(LanguageRegKeyName, "DisplayName", "SimCity 4 Deluxe", Microsoft.Win32.RegistryValueKind.String)
                        .SetValue(LanguageRegKeyName, "LanguageName", "Chinese (Simplified)", Microsoft.Win32.RegistryValueKind.String)
                        Return IIf(My.Computer.FileSystem.DirectoryExists(InstallDir & "\SChinese") = True And
                                   My.Computer.Registry.GetValue(LanguageRegKeyName, "Language", Nothing) = 17, InstallResult.Result.Success, InstallResult.Result.Fail)
                    Case Else
                        My.Computer.FileSystem.CopyDirectory("Data\Patch\Language\English", InstallDir & "\English", True) '将Data\Patch\Language\English文件夹复制到游戏安装目录下替换源文件
                        .SetValue(LanguageRegKeyName, "Language", 1, Microsoft.Win32.RegistryValueKind.DWord) '设置英语语言补丁的注册表值
                        .SetValue(LanguageRegKeyName, "DisplayName", "SimCity 4 Deluxe", Microsoft.Win32.RegistryValueKind.String)
                        .SetValue(LanguageRegKeyName, "LanguageName", "English US", Microsoft.Win32.RegistryValueKind.String)
                        Return IIf(My.Computer.FileSystem.DirectoryExists(InstallDir & "\English") = True And
                                   My.Computer.Registry.GetValue(LanguageRegKeyName, "Language", Nothing) = 1, InstallResult.Result.Success, InstallResult.Result.Fail)
                End Select
            Catch
                Return InstallResult.Result.Fail '如果在安装过程中遇到异常则返回安装失败
            End Try
        End With
    End Function

    ''' <summary>在桌面上添加一个快捷方式</summary>
    ''' <returns>InstallResult.Result 的值之一，如果添加成功，则为InstallResult.Result.Success；否则为InstallResult.Result.Fail</returns>
    ''' <remarks>如果游戏安装目录下有名为 SC4Launcher.exe 的程序，则快捷方式会导向该文件</remarks>
    Public Function AddDestopIcon(ByVal SC4InstallDir As String) As InstallResult.Result
        Try
            '声明一个IWshRuntimeLibrary.WshShell接口的实例和一个IWshRuntimeLibrary.IWshShortcut接口的实例（引用自Windows Script Host Object Model）
            Dim wshshell As New IWshRuntimeLibrary.WshShell, shortcut As IWshRuntimeLibrary.IWshShortcut
            Dim DesktopPath As String = Environment.GetFolderPath(Environment.SpecialFolder.CommonDesktopDirectory) '声明一个用于存储公用桌面目录路径的字符串变量
            If My.Computer.FileSystem.FileExists(SC4InstallDir & "\SC4Launcher.exe") = True Then
                shortcut = wshshell.CreateShortcut(DesktopPath & "\模拟城市4 启动器.lnk")
                shortcut.TargetPath = SC4InstallDir & "\SC4Launcher.exe" : shortcut.Description = "使用模拟城市4 启动器来运行模拟城市4 豪华版"
                shortcut.WindowStyle = 1 : shortcut.IconLocation = SC4InstallDir & "\SC4.ico" : shortcut.Save()
            Else
                shortcut = wshshell.CreateShortcut(DesktopPath & "\模拟城市4 豪华版.lnk")
                shortcut.TargetPath = SC4InstallDir & "\Apps\SimCity 4.exe" : shortcut.Description = "运行模拟城市4 豪华版"
                shortcut.WindowStyle = 1 : shortcut.IconLocation = SC4InstallDir & "\SC4.ico" : shortcut.Save()
            End If
            shortcut.WindowStyle = 1 : shortcut.IconLocation = SC4InstallDir & "\SC4.ico" : shortcut.Save() '设置快捷方式的窗口启动方式、图标路径并保存快捷方式
            Return IIf(My.Computer.FileSystem.FileExists(SC4InstallDir & "\SC4Launcher.exe"),
                       IIf(My.Computer.FileSystem.FileExists(DesktopPath & "\模拟城市4 启动器.lnk"), InstallResult.Result.Success, InstallResult.Result.Fail),
                       IIf(My.Computer.FileSystem.FileExists(DesktopPath & "\模拟城市4 豪华版.lnk"), InstallResult.Result.Success, InstallResult.Result.Fail))
        Catch
            Return InstallResult.Result.Fail '如果在安装过程中遇到异常则返回安装失败
        End Try
    End Function

    ''' <summary>在开始菜单\Maxis\SimCity 4 Deluxe文件夹内添加快捷方式</summary>
    ''' <returns>InstallResult.Result 的值之一，如果添加成功，则为InstallResult.Result.Success；否则为InstallResult.Result.Fail</returns>
    ''' <remarks>如果游戏安装目录下有名为 SC4Launcher.exe 的程序，则快捷方式会导向该文件</remarks>
    Public Function AddStartMenuItems(ByVal SC4InstallDir As String) As InstallResult.Result
        Try
            '声明一个IWshRuntimeLibrary.WshShell接口的实例和一个IWshRuntimeLibrary.IWshShortcut接口的实例（引用自Windows Script Host Object Model）
            Dim wshshell As New IWshRuntimeLibrary.WshShell, shortcut As IWshRuntimeLibrary.IWshShortcut
            Dim StartMenuPath As String = Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu) & "\Programs" '声明一个用于存储公用开始菜单目录路径的字符串变量
            My.Computer.FileSystem.CreateDirectory(StartMenuPath & "\Maxis\SimCity 4 Deluxe") '在公用开始菜单\Maxis下创建一个名为SimCity 4 Deluxe的文件夹
            If My.Computer.FileSystem.FileExists(SC4InstallDir & "\SC4Launcher.exe") = True Then
                shortcut = wshshell.CreateShortcut(StartMenuPath & "\Maxis\SimCity 4 Deluxe\模拟城市4 启动器.lnk")
                shortcut.TargetPath = SC4InstallDir & "\SC4Launcher.exe" : shortcut.Description = "使用模拟城市4 启动器来运行模拟城市4 豪华版"
            Else
                shortcut = wshshell.CreateShortcut(StartMenuPath & "\Maxis\SimCity 4 Deluxe\模拟城市4 豪华版.lnk")
                shortcut.TargetPath = SC4InstallDir & "\Apps\SimCity 4.exe" : shortcut.Description = "运行模拟城市4 豪华版"
            End If
            shortcut.WindowStyle = 1 : shortcut.IconLocation = SC4InstallDir & "\SC4.ico" : shortcut.Save() '设置快捷方式的窗口启动方式、图标路径并保存快捷方式
            shortcut = wshshell.CreateShortcut(StartMenuPath & "\Maxis\SimCity 4 Deluxe\卸载或更改模拟城市4 豪华版.lnk") '新建另外一个快捷方式
            shortcut.TargetPath = SC4InstallDir & "\Setup.exe" : shortcut.Description = "使用模拟城市4 自动安装程序以卸载或更改模拟城市4 豪华版" '设置快捷方式的目标和说明
            shortcut.WindowStyle = 1 : shortcut.IconLocation = SC4InstallDir & "\Setup.exe" : shortcut.Save() '设置快捷方式的窗口启动方式、图标路径并保存快捷方式
            Return IIf(My.Computer.FileSystem.FileExists(SC4InstallDir & "\SC4Launcher.exe"),
                       IIf(My.Computer.FileSystem.FileExists(StartMenuPath & "\Maxis\SimCity 4 Deluxe\模拟城市4 启动器.lnk") And
                       My.Computer.FileSystem.FileExists(StartMenuPath & "\Maxis\SimCity 4 Deluxe\卸载或更改模拟城市4 豪华版.lnk"), InstallResult.Result.Success, InstallResult.Result.Fail),
                       IIf(My.Computer.FileSystem.FileExists(StartMenuPath & "\Maxis\SimCity 4 Deluxe\模拟城市4 豪华版.lnk") And
                       My.Computer.FileSystem.FileExists(StartMenuPath & "\Maxis\SimCity 4 Deluxe\卸载或更改模拟城市4 豪华版.lnk"), InstallResult.Result.Success, InstallResult.Result.Fail))
        Catch
            Return InstallResult.Result.Fail '如果在安装过程中遇到异常则返回安装失败
        End Try
    End Function

    ''' <summary>递归查询一个文件夹内所有的文件和文件夹的大小</summary>
    ''' <param name="path">要查询的文件夹的路径</param>
    ''' <returns>返回文件夹内所有的文件和文件夹的大小</returns>
    Private Function GetFolderSize(ByVal path As String) As Long
        Dim size As Long
        For Each i As IO.FileInfo In My.Computer.FileSystem.GetDirectoryInfo(path).GetFiles
            size += i.Length
        Next
        For Each i As IO.DirectoryInfo In My.Computer.FileSystem.GetDirectoryInfo(path).GetDirectories
            size += GetFolderSize(i.FullName) '递归返回子文件夹的大小
        Next
        Return size
    End Function

    ''' <summary>在控制面板的卸载或更改程序内添加模拟城市4 豪华版 自动安装程序项</summary>
    ''' ''' <param name="SC4InstallDir">模拟城市4的安装路径</param>
    Public Sub SetControlPanelProgramItemRegValue(ByVal SC4InstallDir As String)
        Dim ProgramItemRegKeyName As String = Nothing '声明一个用于存储控制面板的卸载或更改程序里的模拟城市4 豪华版 自动安装程序项的注册表键名的字符串变量
        If Environment.Is64BitOperatingSystem = True Then ProgramItemRegKeyName = "HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall\SC4AutoInstaller"
        If Environment.Is64BitOperatingSystem = False Then ProgramItemRegKeyName = "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\SC4AutoInstaller"
        With My.Computer.Registry
            '在控制面板的卸载或更改程序里添加模拟城市4 豪华版 自动安装程序项
            .SetValue(ProgramItemRegKeyName, "DisplayIcon", SC4InstallDir & "\SC4.ico", Microsoft.Win32.RegistryValueKind.String)
            .SetValue(ProgramItemRegKeyName, "DisplayName", "模拟城市4 豪华版 自动安装程序", Microsoft.Win32.RegistryValueKind.String)
            .SetValue(ProgramItemRegKeyName, "DisplayVersion", My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Revision, Microsoft.Win32.RegistryValueKind.String)
            .SetValue(ProgramItemRegKeyName, "EstimatedSize", GetFolderSize(SC4InstallDir) / 1024, Microsoft.Win32.RegistryValueKind.DWord)
            .SetValue(ProgramItemRegKeyName, "InstallLocation", SC4InstallDir, Microsoft.Win32.RegistryValueKind.String)
            .SetValue(ProgramItemRegKeyName, "Publisher", "n0099", Microsoft.Win32.RegistryValueKind.String)
            .SetValue(ProgramItemRegKeyName, "UninstallString", SC4InstallDir & "\Setup.exe", Microsoft.Win32.RegistryValueKind.String)
            .SetValue(ProgramItemRegKeyName, "URLInfoAbout", "http://tieba.baidu.com/p/3802761033", Microsoft.Win32.RegistryValueKind.String)
            .SetValue(ProgramItemRegKeyName, "URLUpdateInfo", "http://n0099.sinaapp.com", Microsoft.Win32.RegistryValueKind.String)
        End With
    End Sub

    ''' <summary>导入镜像版模拟城市4安装程序所添加或更改的注册表键、项和值</summary>
    ''' <param name="SC4InstallDir">模拟城市4的安装路径</param>
    Public Sub SetNoInstallSC4RegValue(ByVal SC4InstallDir As String)
        Try
            Dim SC4RegKeyName As String, ergcRegKeyName As String '声明两个用于存储模拟城市4所产生的注册表键名的字符串变量
            If Environment.Is64BitOperatingSystem = True Then
                SC4RegKeyName = "HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Maxis\SimCity 4"
                ergcRegKeyName = "HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Electronic Arts\Maxis\SimCity 4 Deluxe\ergc"
            Else
                ergcRegKeyName = "HKEY_LOCAL_MACHINE\SOFTWARE\Electronic Arts\Maxis\SimCity 4 Deluxe\ergc"
                SC4RegKeyName = "HKEY_LOCAL_MACHINE\SOFTWARE\Maxis\SimCity 4"
            End If
            With My.Computer.Registry '导入镜像版模拟城市4的安装程序所添加、更改或删除的注册表键、项和值
                .SetValue(ergcRegKeyName, "", "CX9H498AMHSS8QXDTXJB", Microsoft.Win32.RegistryValueKind.String)
                .SetValue(SC4RegKeyName, "CacheSize", 1196879, Microsoft.Win32.RegistryValueKind.DWord)
                .SetValue(SC4RegKeyName, "CD Drive", ".\\", Microsoft.Win32.RegistryValueKind.String)
                .SetValue(SC4RegKeyName, "DisplayName", "SimCity 4 Deluxe", Microsoft.Win32.RegistryValueKind.String)
                .SetValue(SC4RegKeyName, "Install Dir", SC4InstallDir, Microsoft.Win32.RegistryValueKind.String)
                .SetValue(SC4RegKeyName, "Installed From", ".\\", Microsoft.Win32.RegistryValueKind.String)
                .SetValue(SC4RegKeyName, "IsDeluxe", 1, Microsoft.Win32.RegistryValueKind.DWord)
                .SetValue(SC4RegKeyName, "Language", 1, Microsoft.Win32.RegistryValueKind.DWord)
                .SetValue(SC4RegKeyName, "Locale", "en-us", Microsoft.Win32.RegistryValueKind.String)
                .SetValue(SC4RegKeyName, "Patch URL", "http://simcity.ea.com/update/", Microsoft.Win32.RegistryValueKind.String)
                .SetValue(SC4RegKeyName, "Product GUID", "{A7A34FC9-DF24-4A36-00AD-D4EFE94CC116}", Microsoft.Win32.RegistryValueKind.String)
                .SetValue(SC4RegKeyName, "Region", "NA", Microsoft.Win32.RegistryValueKind.String)
                .SetValue(SC4RegKeyName, "Registration", ergcRegKeyName, Microsoft.Win32.RegistryValueKind.String)
                .SetValue(SC4RegKeyName, "SwapSize", 0, Microsoft.Win32.RegistryValueKind.DWord)
                .SetValue(SC4RegKeyName, "Folder", Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu) & "\Programs\Maxis\SimCity 4 Deluxe", Microsoft.Win32.RegistryValueKind.String)
                .SetValue(SC4RegKeyName & "\EP1", "", "5ZH4HSUIYKHTPFPN7Q30", Microsoft.Win32.RegistryValueKind.String)
                .SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\SimCity 4.exe", "", SC4InstallDir & "\Apps\SimCity 4.exe", Microsoft.Win32.RegistryValueKind.String)
                .SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\SimCity 4.exe", "Path", SC4InstallDir, Microsoft.Win32.RegistryValueKind.String)
                .SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\SimCity 4.exe", "Restart", 0, Microsoft.Win32.RegistryValueKind.DWord)
                .SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\SimCity 4.exe", "Game Registry", SC4RegKeyName.Replace("\1.0", ""), Microsoft.Win32.RegistryValueKind.String)
                .SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Internet Settings\ZoneMap", "UNCAsIntranet", 0, Microsoft.Win32.RegistryValueKind.DWord)
                .SetValue("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Internet Settings\ZoneMap", "AutoDetect", 1, Microsoft.Win32.RegistryValueKind.DWord)
                .CurrentUser.DeleteSubKey("Software\Microsoft\Windows\CurrentVersion\Internet Settings\ZoneMap\ProxyBypass")
                .CurrentUser.DeleteSubKey("Software\Microsoft\Windows\CurrentVersion\Internet Settings\ZoneMap\IntranetName")
                .LocalMachine.DeleteSubKey("Software\Microsoft\Windows\CurrentVersion\Internet Settings\ZoneMap\ProxyBypass")
                .LocalMachine.DeleteSubKey("Software\Microsoft\Windows\CurrentVersion\Internet Settings\ZoneMap\IntranetName")
            End With
        Catch
        End Try
    End Sub

End Module