Imports System
Imports System.Collections
Imports System.IO
Imports System.IO.Ports
Imports System.Text.RegularExpressions
Imports System.Threading
Imports System.Timers


Module Globals

    Public ErrorReason As String
    Public ErrorLogLocation As String = Application.StartupPath
    Public InputData As String = ""
    Public PortScale, PortTest As System.IO.Ports.SerialPort
    Public rdthread, threadtest, statusthread As System.Threading.Thread

    Public ReadScale As Integer = 0
    Public ScalePort As String = ""

    Public Function StripScaleData(ByVal Inputstr As String)

        'strip out all blank spaces
        InputData = InputData.Replace(" ", "")
        'strip out all characters that are not numbers, +, -, or .
        InputData = System.Text.RegularExpressions.Regex.Replace(InputData, "[^0-9.+-]", "")
        'strip out all instances of []
        InputData = System.Text.RegularExpressions.Regex.Replace(InputData, "\[\]", "")
        'strip out all carriage returns and line feeds
        InputData = System.Text.RegularExpressions.Regex.Replace(InputData, vbCr, "")
        InputData = System.Text.RegularExpressions.Regex.Replace(InputData, vbCrLf, "")

        Return Inputstr
    End Function

    Public Function RecScaleData() As String
        'Receive scale data from a serial port.

        Dim returnStr As String = ""

        Dim com1 As IO.Ports.SerialPort = Nothing
        Try
            'depreciated
            'com1 = My.Computer.Ports.OpenSerialPort(ScalePort)

            'intialize the port and open it
            com1 = New System.IO.Ports.SerialPort(ScalePort)
            com1.Open()

            'timeout after 5 seconds if no data is received
            com1.ReadTimeout = 5000

            'begin reading each line of data(only one line is expected)
            Dim InputData As String = StripScaleData(com1.ReadLine())

            'if scale data is not correct or present, return a default value "---"
            If InputData = "" Then
                returnStr = "---"
            Else 'if scale data is present, return stripped and formatted scale data
                returnStr = InputData
            End If

        Catch ex As TimeoutException
            returnStr = "Time out"
            CustomError("RecScaleData", "Time Out" & vbCrLf & ex.ToString & vbCrLf)
        Catch ex As Exception
            returnStr = "---"
            'MsgBox(ex.ToString)
            CustomError("RecScaleData", "exception" & vbCrLf & ex.ToString & vbCrLf)
        Finally
            If com1 IsNot Nothing Then com1.Close()
        End Try

        Return returnStr

    End Function
    Public Function CustomError(ByVal CurrentProcedureName As String, Optional ByVal ExtraMessage As String = "")
        Try


            Dim message As String = "An error occured:  " & vbObjectError & vbCrLf

            'add optional extra message if passed
            If Len(ExtraMessage) <> 0 Then
                message = message & ExtraMessage & vbCrLf
            End If

            'list procedure name and error description. remove next line for production code
            message = message & "Procedure " & CurrentProcedureName & vbCrLf & vbCrLf & Err.Description 'remove this line from production code

            'show warning
            MsgBox(message, vbCritical + vbOKOnly, "ERROR!")


            'append the error to the log file
            Dim FileString As String = ErrorLogLocation & "\ErrorLog.txt"

            'if the file doesn't exist then create it
            If My.Computer.FileSystem.FileExists(FileString) = False Then File.Create(FileString).Dispose()

            'Write the error to the log
            Using sw As StreamWriter = File.AppendText(FileString)
                sw.WriteLine("Time: " & Date.Now & vbCrLf & "Routine: " & CurrentProcedureName & vbCrLf & ExtraMessage)
            End Using

        Catch ex As Exception
            MsgBox("Custom error function failed", vbOKOnly)
            Return 0
        End Try

        Return 0
    End Function

End Module