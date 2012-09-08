Module mainmenu

    Sub Main()
        Do
            Dim intInput As Integer = 0
            Console.Clear()
            Console.WriteLine("Main Menu")
            Console.WriteLine("==========================")
            Console.WriteLine("1. Delete Windows Temporary Files")
            Console.WriteLine("2. Exit" & vbNewLine)
            Console.Write("Enter your choice: ")
            If Integer.TryParse(Console.ReadLine(), intInput) Then
                Select Case intInput
                    Case 1
                        windir.windir()
                    Case 2
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
        counter = My.Computer.FileSystem.GetFiles(wintemp)
        If counter.Count <= 0 Then
            Console.WriteLine("There are no files to delete")
            Console.WriteLine("Press Enter to return to the main menu")
            Console.ReadLine()
            Exit Sub
            mainmenu.Main()
        Else
            Console.WriteLine("You have selected option 1")
            Kill(wintempfiles)
            Console.WriteLine("Windows Temporary Files Deleted")
        End If
    End Sub

End Module