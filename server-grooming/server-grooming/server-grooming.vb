Module mainmenu
    Sub Main()
        Do
            Dim intInput As Integer = 0
            Console.Clear()
            Console.WriteLine("Main Menu")
            Console.WriteLine("==========================")
            Console.WriteLine("1. Delete Windows Temporary Files")
            Console.WriteLine("2. Delete User Profile Temporary Files")
            Console.WriteLine("3. Exit" & vbNewLine)
            Console.Write("Enter your choice: ")
            If Integer.TryParse(Console.ReadLine(), intInput) Then
                Select Case intInput
                    Case 1
                        windir.windir()
                    Case 2
                        profiledir.profiledir()
                    Case 3
                        Exit Sub
                End Select
            Else
                Console.WriteLine("Please select from 1-2")
            End If
        Loop
    End Sub
End Module
Module windir
    Sub windir()
        Dim counter As System.Collections.ObjectModel.ReadOnlyCollection(Of String)
        Dim windirenv As String = My.Application.GetEnvironmentVariable("windir")
        Dim wintempfiles As String = windirenv & "\Temp\*"
        Dim wintemp As String = windirenv & "\Temp\"
        Dim Files As String() = IO.Directory.GetFiles(wintemp)
        counter = My.Computer.FileSystem.GetFiles(wintemp)
        If counter.Count <= 0 Then
            Console.WriteLine("There are no files to delete")
            Console.WriteLine("Press Enter to return to the main menu")
            Console.ReadLine()
            Exit Sub
            mainmenu.Main()
        Else
            Console.WriteLine("Deleting Windows Temporary Files")
            For Each foundFile As String In My.Computer.FileSystem.GetFiles(wintemp)
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
    End Sub
End Module
Module profiledir
    Sub profiledir()
        Dim counter As System.Collections.ObjectModel.ReadOnlyCollection(Of String)
        Dim profenv As String = My.Application.GetEnvironmentVariable("temp")
        Dim proftempfiles As String = profenv & "\*"
        Dim proftemp As String = profenv
        Dim Files As String() = IO.Directory.GetFiles(proftemp)
        counter = My.Computer.FileSystem.GetFiles(proftemp)
        If counter.Count <= 0 Then
            Console.WriteLine("There are no files to delete")
            Console.WriteLine("Press Enter to return to the main menu")
            Console.ReadLine()
            Exit Sub
            mainmenu.Main()
        Else
            Console.WriteLine("Deleting Current User Profile Temporary Files")
            For Each foundFile As String In My.Computer.FileSystem.GetFiles(proftemp)
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
    End Sub
End Module