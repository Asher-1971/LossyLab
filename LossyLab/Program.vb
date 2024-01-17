
    Imports System
    Imports System.IO

    Module Program
        Dim board(3, 3) As Char
        Dim playerTurn As Char = "X"c
        Dim score As New Dictionary(Of Char, Integer) From {{"X", 0}, {"O", 0}}
        Dim filePath As String = "gameState.txt"

        Sub Main(args As String())
            If File.Exists(filePath) Then
                LoadGameState()
            Else
                InitializeBoard()
            End If

            While True
                PrintBoard()
                Console.WriteLine("Player " & playerTurn & "'s turn. Enter row and column (1-3):")
                Dim input As String = Console.ReadLine()
                Dim row As Integer = Integer.Parse(input.Split(" "c)(0))
                Dim col As Integer = Integer.Parse(input.Split(" "c)(1))
                If board(row, col) = " "c Then
                    board(row, col) = playerTurn
                    If CheckWin(playerTurn) Then
                        Console.WriteLine("Player " & playerTurn & " wins!")
                        score(playerTurn) += 1
                        InitializeBoard()
                    ElseIf CheckDraw() Then
                        Console.WriteLine("It's a draw!")
                        InitializeBoard()
                    Else
                        playerTurn = If(playerTurn = "X"c, "O"c, "X"c)
                    End If
                Else
                    Console.WriteLine("Invalid move!")
                End If
                SaveGameState()
            End While
        End Sub

        Sub InitializeBoard()
            For i As Integer = 1 To 3
                For j As Integer = 1 To 3
                    board(i, j) = " "c
                Next
            Next
        End Sub

        Sub PrintBoard()
            For i As Integer = 1 To 3
                For j As Integer = 1 To 3
                    Console.Write(board(i, j) & " ")
                Next
                Console.WriteLine()
            Next
        End Sub

        Function CheckWin(player As Char) As Boolean
            For i As Integer = 1 To 3
                If board(i, 1) = player And board(i, 2) = player And board(i, 3) = player Then
                    Return True
                End If
                If board(1, i) = player And board(2, i) = player And board(3, i) = player Then
                    Return True
                End If
            Next
            If board(1, 1) = player And board(2, 2) = player And board(3, 3) = player Then
                Return True
            End If
            If board(1, 3) = player And board(2, 2) = player And board(3, 1) = player Then
                Return True
            End If
            Return False
        End Function

        Function CheckDraw() As Boolean
            For i As Integer = 1 To 3
                For j As Integer = 1 To 3
                    If board(i, j) = " "c Then
                        Return False
                    End If
                Next
            Next
            Return True
        End Function

        Sub SaveGameState()
            Using sw As StreamWriter = New StreamWriter(filePath)
                For i As Integer = 1 To 3
                    For j As Integer = 1 To 3
                        sw.Write(board(i, j))
                    Next
                    sw.WriteLine()
                Next
                sw.WriteLine(playerTurn)
                sw.WriteLine("X: " & score("X"))
                sw.WriteLine("O: " & score("O"))
            End Using
        End Sub

        Sub LoadGameState()
            Using sr As StreamReader = New StreamReader(filePath)
                For i As Integer = 1 To 3
                    For j As Integer = 1 To 3
                        board(i, j) = Convert.ToChar(sr.Read())
                    Next
                    sr.ReadLine()
                Next
                playerTurn = Convert.ToChar(sr.ReadLine())
                score("X") = Integer.Parse(sr.ReadLine().Split(": "c)(1))
                score("O") = Integer.Parse(sr.ReadLine().Split(": "c)(1))
            End Using
        End Sub
    End Module
