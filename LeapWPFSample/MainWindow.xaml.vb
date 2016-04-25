Imports Leap
Imports LeapWPFSample
Imports ev = LeapWPFSample.LeapEvents

Class MainWindow
    Inherits Window
    Implements ILeapEventDelegate

    Public listener As LeapEventListener = Nothing
    Public controller As Controller = Nothing
    Private isClosing As Boolean = False

    Public Sub New()
        InitializeComponent()
        listener = New LeapEventListener(Me)
        controller = New Controller(listener)
    End Sub

    Delegate Sub LeapEventDelegate(eventName As String)

    Public Sub LeapEventNotification(eventName As String) Implements ILeapEventDelegate.LeapEventNotification

        If CheckAccess() Then

            Select Case eventName
                Case ev.onInit
                    ' Do nothing
                Case ev.onConnect
                    connectHandler()
                Case ev.onFrame
                    If Not isClosing Then
                        newFrameHandler(controller.Frame)
                    End If
                Case Else
                    ' Do Nothing
            End Select

        Else
            Dispatcher.Invoke(New LeapEventDelegate(AddressOf LeapEventNotification), eventName)
        End If

    End Sub

    Public Sub newFrameHandler(frame As Frame)

        ' This is where you can access the frame object

        Dim builder As New Text.StringBuilder(String.Empty)
        frame.Hands.ToList.ForEach(Function(n) _
                                       builder.AppendFormat("Hand {0} = {1}",
                                                            n.Id,
                                                            n.PalmPosition()))

    End Sub

    Public Sub connectHandler()
        'controller.SetPolicy(Controller.PolicyFlag.POLICY_DEFAULT)
        'controller.EnableGesture(Gesture.GestureType.TYPE_SWIPE)
        'controller.Config.SetFloat("Gesture.Swipe.MinLength", 100.0F)
    End Sub

    Sub Window_Close(sender As Object, e As EventArgs) Handles Me.Closing
        'controller.RemoveListener(listener)
        'controller.Dispose()
        isClosing = True
    End Sub

End Class
