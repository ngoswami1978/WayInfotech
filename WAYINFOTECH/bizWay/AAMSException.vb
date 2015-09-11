Namespace bizShared
    Public Class AAMSException
        Inherits System.ApplicationException
        Private intErrorCode As Int32 = 0
        Private strErrorMsg As String = ""

        Public Sub New(ByVal Message As String)
            MyBase.New(Message)
            strErrorMsg = Message
        End Sub

        Public Sub New(ByVal intECode As Int32, ByVal strMsg As String)
            MyBase.New(strMsg)
            intErrorCode = intECode
            strErrorMsg = strMsg
        End Sub

        Public ReadOnly Property ErrorNumber() As Int32
            Get
                ErrorNumber = intErrorCode
            End Get
        End Property

        Public ReadOnly Property ErrorMessage() As String
            Get
                ErrorMessage = strErrorMsg
            End Get
        End Property
    End Class
End Namespace