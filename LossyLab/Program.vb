Imports System
Imports System.IO

Module Program
    Sub Main(args As String())
        Dim board As Char(,) = New Char(2, 2) {}
        Dim currentPlayer As Char = "X"c
        Dim gameOver As Boolean = False

        ' Load previous game state from file
        Dim filePath As String = "game.txt"
        If File.Exists(filePath) Then
            Dim lines As String() = File.ReadAllLines(filePath)
            For i As Integer = 0 To 2
                For j As Integer = 0 To 2
                    board(i, j) = lines(i)(j)
                Next
            Next
        End If

        ' Game loop
        While Not gameOver
            ' Display the board
            Console.Clear()
            For i As Integer = 0 To 2
                For j As Integer = 0 To 2
                    Console.Write(board(i, j) & " ")
                Next
                Console.WriteLine()
            Next

            ' Get the player's move
            Console.WriteLine("Player " & currentPlayer & ", enter your move (row column):")
            Dim move As String = Console.ReadLine()
            Dim moveParts As String() = move.Split(" "c)
            Dim row As Integer = Integer.Parse(moveParts(0))
            Dim col As Integer = Integer.Parse(moveParts(1))

            ' Update the board
            board(row, col) = currentPlayer

            ' Check for a win or draw
            If CheckWin(board, currentPlayer) Then
                Console.WriteLine("Player " & currentPlayer & " wins!")
                gameOver = True
            ElseIf CheckDraw(board) Then
                Console.WriteLine("It's a draw!")
                gameOver = True
            End If

            ' Switch players
            currentPlayer = If(currentPlayer = "X"c, "O"c, "X"c)
        End While

        ' Save game state to file
        Dim output As String = ""
        For i As Integer = 0 To 2
            For j As Integer = 0 To 2
                output &= board(i, j)
            Next
            output &= Environment.NewLine
        Next
        File.WriteAllText(filePath, output)
    End Sub

    Function CheckWin(board As Char(,), player As Char) As Boolean
        ' Check rows
        For i As Integer = 0 To 2
            If board(i, 0) = player AndAlso board(i, 1) = player AndAlso board(i, 2) = player Then
                Return True
            End If
        Next

        ' Check columns
        For j As Integer = 0 To 2
            If board(0, j) = player AndAlso board(1, j) = player AndAlso board(2, j) = player Then
                Return True
            End If
        Next

        ' Check diagonals
        If board(0, 0) = player AndAlso board(1, 1) = player AndAlso board(2, 2) = player Then
            Return True
        End If
        If board(0, 2) = player AndAlso board(1, 1) = player AndAlso board(2, 0) = player Then
            Return True
        End If

        Return False
    End Function

    Function CheckDraw(board As Char(,)) As Boolean
        For i As Integer = 0 To 2
            For j As Integer = 0 To 2
                If board(i, j) = ControlChars.NullChar Then
                    Return False
                End If
            Next
        Next
        Return True
    End Function
End Module