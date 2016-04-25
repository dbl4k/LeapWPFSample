Imports Leap
Imports ev = LeapWPFSample.LeapEvents

Public Class LeapEventListener
    Inherits Leap.Listener

    Public Property eventDelegate As ILeapEventDelegate = Nothing

    Public Sub New(delegateObject As ILeapEventDelegate)
        eventDelegate = delegateObject
    End Sub

    Public Overrides Sub OnFrame(controller As Controller)
        eventDelegate.LeapEventNotification(ev.onFrame)
    End Sub

    Public Overrides Sub OnInit(controller As Controller)
        eventDelegate.LeapEventNotification(ev.onInit)
    End Sub

    Public Overrides Sub OnConnect(controller As Controller)
        controller.SetPolicy(controller.PolicyFlag.POLICY_IMAGES)
        controller.EnableGesture(Gesture.GestureType.TYPE_SWIPE)
        eventDelegate.LeapEventNotification(ev.onConnect)
    End Sub

    Public Overrides Sub OnExit(controller As Controller)
        eventDelegate.LeapEventNotification(ev.onExit)
    End Sub

End Class