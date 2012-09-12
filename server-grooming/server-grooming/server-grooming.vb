Imports System
Imports System.Management
Module mainmenu
    'Global Variables
    Dim objcounter As ObjectModel.ReadOnlyCollection(Of String) 'Counter variable
    Dim objsysenv As String = My.Application.GetEnvironmentVariable("windir") 'System directory variable
    Dim objuserenv As String = My.Application.GetEnvironmentVariable("temp") 'Temp directory variable
    Dim objprofenv As String = My.Application.GetEnvironmentVariable("userprofile") 'User profile variable
    Function getosname() As String
        Dim computer As String = String.Empty
        Dim strResult As String = String.Empty
        Dim OSVersion As String
        Try
            Dim searcher As New ManagementObjectSearcher(computer & "root\CIMV2", "SELECT * FROM Win32_OperatingSystem")
            Dim colItems = searcher.Get()
            For Each queryObj As ManagementObject In colItems
                OSVersion = Left(queryObj("Version"), 3)
                Select Case OSVersion
                    Case "6.1"
                        strResult = "Windows 7"
                    Case "6.0"
                        strResult = "Windows Vista"
                    Case "5.2"
                        strResult = "Windows 2003"
                    Case "5.1"
                        strResult = "Windows XP"
                    Case "5.0"
                        strResult = "Windows 2000"
                    Case Else
                        strResult = "Other Operating System"
                End Select
            Next
            Return strResult
        Catch err As ManagementException
            Return "An error occurred while querying WMI: " & err.Message
        Catch err As System.Runtime.InteropServices.COMException
            If Not String.IsNullOrEmpty(computer) Then
                Return ("An error occurred while connecting to computer " & computer.Replace("\", String.Empty))
            Else
                Return ("An error occurred: " & err.Message)
            End If
        End Try
    End Function
    '=============== Main Menu Start ===============
    Sub Main()
        Do
            Dim intInput As Integer = 0
            'Clear console from previous commands
            Console.Clear()
            Console.WriteLine("Main Menu")
            Console.WriteLine("==========================")
            Console.WriteLine("1. Delete Windows Temporary Files")
            Console.WriteLine("2. Delete User Profile Temporary Files")
            Console.WriteLine("3. Delete Temporary Internet Files")
            Console.WriteLine("4. Exit" & vbNewLine)
            Console.Write("Enter your choice: ")
            If Integer.TryParse(Console.ReadLine(), intInput) Then
                Select Case intInput
                    Case 1
                        'Option 1
                        mainmenu.subsystemp()
                    Case 2
                        'Option 2
                        mainmenu.subusertemp()
                    Case 3
                        mainmenu.subtempintfiles()
                    Case 4
                        'Option 4
                        Exit Sub
                End Select
            Else
                Console.WriteLine("Please select from 1-2")
            End If
        Loop
        '=============== Main Menu Finish ===============
    End Sub
    Sub subsystemp()
        Dim objsysenvdir As String = objsysenv & "\Temp\"
        Dim Files As String() = IO.Directory.GetFiles(objsysenvdir)
        objcounter = My.Computer.FileSystem.GetFiles(objsysenvdir)
        If objcounter.Count <= 0 Then
            Console.WriteLine("There are no files to delete")
            Console.WriteLine("Press Enter to return to the main menu")
            Console.ReadLine()
            Exit Sub
            mainmenu.Main()
        Else
            Console.WriteLine("Deleting Windows Temporary Files")
            For Each foundFile As String In My.Computer.FileSystem.GetFiles(objsysenvdir)
                Console.WriteLine(foundFile)
                Try
                Catch ex As System.IO.IOException
                    System.IO.File.Delete(foundFile)
                End Try
            Next
            Console.WriteLine("Windows Temporary Files Deleted")
            Console.WriteLine("Press Enter to return to the main menu")
            Console.ReadLine()
        End If
        'Clear the memory
        objsysenvdir = Nothing
        Files = Nothing
        objcounter = Nothing
    End Sub
    Sub subusertemp()
        Dim Files As String() = IO.Directory.GetFiles(objuserenv)
        objcounter = My.Computer.FileSystem.GetFiles(objuserenv)
        If objcounter.Count <= 0 Then
            Console.WriteLine("There are no files to delete")
            Console.WriteLine("Press Enter to return to the main menu")
            Console.ReadLine()
            Exit Sub
            mainmenu.Main()
        Else
            Console.WriteLine("Deleting Current User Profile Temporary Files")
            For Each foundFile As String In My.Computer.FileSystem.GetFiles(objuserenv)
                Console.WriteLine(foundFile)
                Try
                Catch ex As System.IO.IOException
                    System.IO.File.Delete(foundFile)
                End Try
            Next
            Console.WriteLine("Current User Profile Temporary Files Deleted")
            Console.WriteLine("Press Enter to return to the main menu")
            Console.ReadLine()
        End If
        'Clear the memory
        Files = Nothing
        objcounter = Nothing
    End Sub
    Sub subtempintfiles()
        Dim OSType = getosname()
        Dim tempintfiles = Nothing
        'Find the temporary internet file path for different operating systems
        If OSType = "Windows 7" Or OSType = "Windows Vista" Then
            tempintfiles = objprofenv & "\AppData\Local\Microsoft\Windows\Temporary Internet Files"
        ElseIf OSType = "Windows 2003" Or OSType = "Windows XP" Then
            tempintfiles = objprofenv & "\Local Settings\Temporary Internet Files"
        End If
        Dim Files As String() = IO.Directory.GetFiles(tempintfiles)
        objcounter = My.Computer.FileSystem.GetFiles(tempintfiles)
        If objcounter.Count <= 0 Then
            Console.WriteLine("There are no files to delete")
            Console.WriteLine("Press Enter to return to the main menu")
            Console.ReadLine()
            Exit Sub
            mainmenu.Main()
        Else
            Console.WriteLine("Deleting Current User Profile Temporary Files")
            For Each foundFile As String In My.Computer.FileSystem.GetFiles(tempintfiles)
                Console.WriteLine(foundFile)
                Try
                Catch ex As System.IO.IOException
                    System.IO.File.Delete(foundFile)
                End Try
            Next
            Console.WriteLine("Current User Profile Temporary Files Deleted")
            Console.WriteLine("Press Enter to return to the main menu")
            Console.ReadLine()
        End If
        'Clear the memory
        Files = Nothing
        objcounter = Nothing
    End Sub
End Module