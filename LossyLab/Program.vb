
    Imports System
    Imports System.IO

Module Module1
    Dim board(2, 2) As String
    Dim playerTurn As String = "X"
    Dim score As New Dictionary(Of String, Integer) From {{"X", 0}, {"O", 0}}
    Dim filePath As String = "gameState.txt"

    Sub Main()
        If IO.File.Exists(filePath) Then
            LoadGameState()
        Else
            ' Initialize the board
            For i As Integer = 0 To 2
                For j As Integer = 0 To 2
                    board(i, j) = " "
                Next
            Next
        End If

        ' Game loop
        While True
            ' Display the board
            Console.Clear()
            For i As Integer = 0 To 2
                For j As Integer = 0 To 2
                    Console.Write(board(i, j) & " ")
                Next
                Console.WriteLine()
            Next

            ' Get the player's move
            Console.WriteLine("Player " & playerTurn & ", enter your move (row column):")
            Dim move As String = Console.ReadLine()
            Dim parts() As String = move.Split(" ")
            Dim row As Integer = Integer.Parse(parts(0))
            Dim column As Integer = Integer.Parse(parts(1))

            ' Update the board
            board(row, column) = playerTurn

            ' Check for game over
            If CheckGameOver(board) Then
                Console.WriteLine("Player " & playerTurn & " wins!")
                score(playerTurn) += 1
                Console.WriteLine("Score: X - " & score("X") & ", O - " & score("O"))
                Console.WriteLine("Press any key to start a new game...")
                Console.ReadKey()

                ' Reset the board
                For i As Integer = 0 To 2
                    For j As Integer = 0 To 2
                        board(i, j) = " "
                    Next
                Next
            End If

            ' Switch players
            If playerTurn = "X" Then
                playerTurn = "O"
            Else
                playerTurn = "X"
            End If

            ' Save the game state
            SaveGameState()
        End While
    End Sub

    Function CheckGameOver(board(,) As String) As Boolean
        ' Check rows
        For i As Integer = 0 To 2
            If board(i, 0) <> " " And board(i, 0) = board(i, 1) And board(i, 1) = board(i, 2) Then
                Return True
            End If
        Next

        ' Check columns
        For i As Integer = 0 To 2
            If board(0, i) <> " " And board(0, i) = board(1, i) And board(1, i) = board(2, i) Then
                Return True
            End If
        Next

        ' Check diagonals
        If board(0, 0) <> " " And board(0, 0) = board(1, 1) And board(1, 1) = board(2, 2) Then
            Return True
        End If
        If board(0, 2) <> " " And board(0, 2) = board(1, 1) And board(1, 1) = board(2, 0) Then
            Return True
        End If

        Return False
    End Function

    Sub SaveGameState()
        Using sw As StreamWriter = New StreamWriter(filePath)
            For i As Integer = 0 To 2
                For j As Integer = 0 To 2
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
            For i As Integer = 0 To 2
                For j As Integer = 0 To 2
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