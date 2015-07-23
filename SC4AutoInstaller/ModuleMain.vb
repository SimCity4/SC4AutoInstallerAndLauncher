''' <summary>ModuleMain 模块提供各种类的实例和Windows API方法、结构和常数</summary>
Module ModuleMain

    ''' <summary>一个用于获取或设置安装选项的类的实例</summary>
    Public InstallOptions As New InstallOptions
    ''' <summary>一个用于获取或设置已安装的组件的类的实例</summary>
    Public InstalledModule As New InstalledModule
    ''' <summary>一个用于获取或设置安装结果的类的实例</summary>
    Public InstallResult As New InstallResult
    Public Declare Function GetSystemMenu Lib "user32" (ByVal hWnd As IntPtr, ByVal bRevert As Boolean) As Integer
    Public Declare Function RemoveMenu Lib "user32" (ByVal hMenu As IntPtr, ByVal uPosition As UInt32, ByVal uFlags As UInt32) As Integer
    Public Declare Function DrawMenuBar Lib "user32" (ByVal hWnd As IntPtr) As Integer
    Public Declare Function GetMenuItemCount Lib "user32" (ByVal hMenu As IntPtr) As Integer
    Public Declare Function SendMessage Lib "user32" Alias "SendMessageA" (ByVal hWnd As IntPtr, ByVal Msg As UInteger, ByVal wParam As IntPtr, ByVal lParam As String) As IntPtr
    Public Declare Function PostMessage Lib "user32" Alias "PostMessageA" (ByVal hWnd As IntPtr, ByVal Msg As UInteger, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As Boolean
    Public Declare Function FindWindow Lib "user32" Alias "FindWindowA" (ByVal lpClassName As String, ByVal lpWindowName As String) As IntPtr
    Public Declare Function FindWindowEx Lib "user32" Alias "FindWindowExA" (ByVal parentHandle As IntPtr, ByVal childAfter As IntPtr, ByVal lclassName As String, ByVal windowTitle As String) As IntPtr
    Public Declare Function SetWindowPos Lib "user32" (ByVal hWnd As IntPtr, ByVal hWndInsertAfter As IntPtr, ByVal X As Integer, ByVal Y As Integer, ByVal cx As Integer, ByVal cy As Integer, ByVal uFlags As UInteger) As Boolean
    Public Declare Function FlashWindowEx Lib "user32" (ByRef pwfi As FLASHINFO) As Boolean
    Public Structure FLASHINFO
        Public cbSize As UInt32
        Public hwnd As IntPtr
        Public dwFlags As UInt32
        Public uCount As UInt32
        Public dwTimeout As UInt32
    End Structure
    Public Const WM_KEYDOWN = &H100
    Public Const WM_KEYUP = &H101
    Public Const VK_TAB = &H9
    Public Const WM_LBUTTONDOWN = &H201
    Public Const WM_LBUTTONUP = &H202
    Public Const WM_SETTEXT = &HC
    Public Const HWND_TOP = 0
    Public Const SWP_NOSIZE = &H1
    Public Const SWP_NOMOVE = &H2
    Public Const MF_BYPOSITION = &H400&
    Public Const MF_DISABLED = &H2&
    Public Const FLASHW_ALL = 3

End Module