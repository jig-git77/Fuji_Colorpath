''' <summary>
''' Structure to define CIE L*a*b*.
''' </summary>
Public Structure CIELab
	''' <summary>
	''' Gets an empty CIELab structure.
	''' </summary>
	Public Shared ReadOnly Empty As New CIELab()

	Private _l As Double
	Private _a As Double
	Private _b As Double


	Public Shared Operator =(ByVal item1 As CIELab, ByVal item2 As CIELab) As Boolean
		Return (item1.L = item2.L AndAlso item1.A = item2.A AndAlso item1.B = item2.B)
	End Operator

	Public Shared Operator <>(ByVal item1 As CIELab, ByVal item2 As CIELab) As Boolean
		Return (item1.L <> item2.L OrElse item1.A <> item2.A OrElse item1.B <> item2.B)
	End Operator


	''' <summary>
	''' Gets or sets L component.
	''' </summary>
	Public Property L() As Double
		Get
			Return Me._l
		End Get
		Set
			Me._l = value
		End Set
	End Property

	''' <summary>
	''' Gets or sets a component.
	''' </summary>
	Public Property A() As Double
		Get
			Return Me._a
		End Get
		Set
			Me._a = value
		End Set
	End Property

	''' <summary>
	''' Gets or sets a component.
	''' </summary>
	Public Property B() As Double
		Get
			Return Me._b
		End Get
		Set
			Me._b = value
		End Set
	End Property

	Public Sub New(ByVal l As Double, ByVal a As Double, ByVal b As Double)
		Me._l = l
		Me._a = a
		Me._b = b
	End Sub

	Public Overloads Overrides Function Equals(ByVal obj As Object) As Boolean
		If obj = Nothing Or (Me.GetType() IsNot obj.GetType())
			Return False
		End If

		Return (Me = DirectCast(obj, CIELab))
	End Function

	Public Overloads Overrides Function GetHashCode() As Integer
		Return L.GetHashCode() Xor a.GetHashCode() Xor b.GetHashCode()
	End Function

End Structure