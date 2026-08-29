Imports System.IO
Imports System.IO.Ports
Imports System.Threading
Imports Scale.Globals
Imports System.Drawing
Imports System.Windows.Forms

Public Class Scalefrm

    Private _stopThread As Boolean = False
    Event PortData(ByVal Message As String)
    Dim CancelRead As Integer = False
    Public Sub InitPort()
        'open port and begin thread to read scale data
        Try

            PortTest.PortName = ScalePort
            With PortTest
                'assigned by the system COM port selected
                .PortName = ScalePort
                'no extra bit is added to the data to check for transmission errors, handled by the software instead of the hardware
                .Parity = Parity.None
                ' 9600 bits of data per second
                .BaudRate = 9600
                'message size, modern industry standard
                .DataBits = 8
                'scale sends 1 bit of silence at the end of every 8-bit chunk to let the computer know that specific character is finished
                .StopBits = StopBits.One
            End With
            PortTest.Open()
            ReadScale = 1
            threadtest = New System.Threading.Thread(AddressOf ReadPort)
            threadtest.Start()

        Catch errors As IOException
            MsgBox("Check Port", errors.ToString)
            CustomError("InitPort function", "Input exception" & vbCrLf & errors.ToString & vbCrLf)
        Catch ex As TimeoutException
            MsgBox("Error: Serial Port read timed out.")
            CustomError("InitPort function", "TimeOut Exception" & vbCrLf & ex.ToString & vbCrLf)
        Catch ex As Exception
            MsgBox(ex.ToString)
            CustomError("InitPort function", "General Exception" & vbCrLf & ex.ToString & vbCrLf)
        End Try

    End Sub
    Public Function Converttext(inputstr As String)

        'sets the 0.00 reading to the inputstr value
        Try
            CurrentReadingtxt.Text = inputstr
        Catch ex As Exception
            'MsgBox(ex.ToString)
            CustomError("Converttext function", "error" & vbCrLf & ex.ToString & vbCrLf)
            Return False
        End Try

        Return True
    End Function

    Private Sub UpdateScaleTxt(ByVal ScaleData As String)
        'while reading the scale data, update the 0.00 reading
        If Me.InvokeRequired Then
            Me.Invoke(New Action(Of String)(AddressOf UpdateScaleTxt), ScaleData)
            Return
        End If
        CurrentReadingtxt.Text = ScaleData
    End Sub

    Private Sub ReadPort()
        ' this routine will continuously read the scale data
        Try
            'set the 0.00 text holder to blank
            InputData = ""

            Do While PortTest.IsOpen
                Try

                    'wipe the 0.00 reading memory
                    InputData = PortTest.ReadExisting
                    Do While ReadScale = 1
                        'read the scale data
                        InputData = PortTest.ReadLine
                        'strip out the extra formatting characters from the scale data, leaving only the numeric value
                        InputData = StripScaleData(InputData)

                        'this if block controls the printing button on scales
                        'this will cause the printer to print if set to 1, after the print it is set to 0
                        If Val(InputData.ToString) = 0.0 And Len(InputData.ToString) <> 0 Then

                            UpdateScaleTxt(InputData)
                            RaiseEvent PortData(InputData)
                            System.Threading.Thread.Sleep(0)
                            My.Application.DoEvents()

                        ElseIf InputData.ToString <> "" Then
                            'if scale reads default 0.00 then cancel the printing
                            MsgBox("There Is Nothing On the scale", vbOKOnly, "NEGATIVE OR ZERO BALANCE.")
                            CurrentReadingtxt.Text = InputData
                        End If

                    Loop

                Catch ex As Exception
                    'reset the function by changing read scale global flag to 0 or off. close the port, exit the routine
                    ReadScale = 0
                    If PortTest.IsOpen = True Then PortTest.Close()
                    Exit Sub
                End Try

            Loop

        Catch ex As Exception
            CustomError("ReadPort function", "error" & vbCrLf & ex.ToString & vbCrLf)
        End Try

    End Sub

    Private Sub Exitbtn_Click(sender As Object, e As EventArgs) Handles Exitbtn.Click
        'on program exit, kill all routines and close the serial port if open
        CancelRead = True
        If IsNothing(PortTest) = True Then Application.Exit()
        If PortTest.IsOpen = True Then PortTest.Close()
        Application.Exit()
    End Sub

    Private Sub TestConnectionbtn_Click(sender As Object, e As EventArgs) Handles TestConnectionbtn.Click
        'this routine will test the scale connection to the computer

        'open the port
        Try
            PortTest = New SerialPort
            PortTest.Open()
        Catch ex As Exception
            CustomError("TestConnectionbtn_Click", "Port opening failure" & vbCrLf & ex.ToString & vbCrLf)
        End Try

        'report results of test and close port if open
        Try
            If PortTest.IsOpen = True Then
                MsgBox("Scale is connected to port: " & PortTest.PortName & " and is functioning correctly")
                PortTest.Close()
            End If

        Catch ex As Exception
            CustomError("TestConnectionbtn_Click", "Port testing failure" & vbCrLf & ex.ToString & vbCrLf)
        End Try


    End Sub

    Private Sub ReadScalebtn_Click(sender As Object, e As EventArgs) Handles ReadScalebtn.Click
        'when print button is pressed on the scale device, force report the reading to the computer
        CurrentReadingtxt.Text = RecScaleData()

    End Sub

    Private Sub ResetScalebtn_Click(sender As Object, e As EventArgs) Handles ResetScalebtn.Click

        'add com ports
        Try

            'disconnect all the serial connections and reset the port objects to new instances

            If PortTest IsNot Nothing Then
                If PortTest.IsOpen Then PortTest.Close()
                PortTest.Dispose()
            End If

            PortTest = New SerialPort()
            'stop the scale reading thread
            ReadScale = 0
        Catch ex As Exception
            CustomError("ResetScalebtn_Click", "create new port object" & vbCrLf & ex.ToString & vbCrLf)
            Exit Sub
        End Try


    End Sub

    Private Sub ContinuousTestbtn_Click(sender As Object, e As EventArgs) Handles ContinuousTestbtn.Click
        Try
            'flip labels and functions
            If ContinuousTestbtn.Text = "Continuous Test" Then
                ContinuousTestbtn.Text = "Cancel"
                PortTest = New SerialPort
                InitPort()
            End If

            If ContinuousTestbtn.Text = "Cancel" Then
                ContinuousTestbtn.Text = "Continuous Test"
                If PortTest.IsOpen = True Then PortTest.Close()
                'close the serial connection if open, kill the port
                If Not IsNothing(threadtest) = True Then
                    'legacy
                    'threadtest.Abort()
                    System.Threading.Volatile.Write(_stopThread, True)
                    threadtest = Nothing
                End If
            End If

        Catch ex As Exception
            CustomError("ContinuousTestbtn_Click", "Continuous check error" & vbCrLf & ex.ToString & vbCrLf)
            threadtest = Nothing
        End Try

    End Sub

    Private Sub Scalefrm_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        'if application closes, kill the scale routines
        CancelRead = True
    End Sub

    Private Sub Scalefrm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'resize the controls to fit the form
        ResizeControlsToFitText(Me)
        'reset scale variables and clear list
        ReadScale = 0
        PortScale = Nothing
        ScalePortcombo.Items.Clear()

        'add com ports to the combo box for selection
        Try

            'grab a list of all serial ports on PC
            For Each SerialPortName As String In System.IO.Ports.SerialPort.GetPortNames()
                ScalePortcombo.Items.Add(SerialPortName)
            Next
        Catch ex As Exception
            CustomError("Settings_Load", "ADDING text file ITEMS" & vbCrLf & ex.ToString & vbCrLf)
        End Try

    End Sub

    Private Sub Scalefrm_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown

        'if form keypriview enabled then press esacpe key to close form
        If e.KeyCode = Keys.Escape Then Me.Close()

    End Sub

    Public Sub ResizeControlsToFitText(parent As Control)

        For Each ctrl As Control In parent.Controls

            ' Process child controls first
            If ctrl.HasChildren Then
                ResizeControlsToFitText(ctrl)
            End If

            ' Controls where text can be displayed
            If Not String.IsNullOrWhiteSpace(ctrl.Text) Then

                Using g As Graphics = ctrl.CreateGraphics()

                    Dim textSize As SizeF = g.MeasureString(ctrl.Text, ctrl.Font)

                    Dim padding As Integer = 12

                    ' Buttons, labels, checkboxes, radio buttons, etc.
                    If TypeOf ctrl Is Button OrElse
                       TypeOf ctrl Is CheckBox OrElse
                       TypeOf ctrl Is RadioButton Then

                        Dim newWidth As Integer =
                            CInt(Math.Ceiling(textSize.Width)) + padding

                        Dim newHeight As Integer =
                            Math.Max(ctrl.Height,
                                     CInt(Math.Ceiling(textSize.Height)) + padding)

                        ctrl.Width = newWidth
                        ctrl.Height = newHeight

                        ' Labels
                    ElseIf TypeOf ctrl Is Label Then

                        Dim newWidth As Integer =
                            CInt(Math.Ceiling(textSize.Width)) + padding

                        Dim newHeight As Integer =
                            CInt(Math.Ceiling(textSize.Height)) + padding

                        ctrl.AutoSize = False
                        ctrl.Width = newWidth
                        ctrl.Height = newHeight

                    End If

                End Using

            End If

        Next

    End Sub

End Class