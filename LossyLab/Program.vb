Imports System
Imports System.IO

Module Program
    Sub Main(args As String())
        ' Set the default file name
        Dim fileName As String = "userInput.txt"
        Dim userInput As String = ""
        Dim line As String

        Console.WriteLine("Enter text (type 'END' to finish):")

        ' Read lines from the console until the user types 'END'
        Do
            line = Console.ReadLine()
            If line <> "END" Then
                userInput &= line & Environment.NewLine
            End If
        Loop Until line = "END"

        ' Write the user input to a file
        File.WriteAllText(fileName, userInput)

        ' Read the file and display its contents
        Dim fileContents As String = File.ReadAllText(fileName)
        Console.WriteLine("File contents:")
        Console.WriteLine(fileContents)
    End Sub
End Module