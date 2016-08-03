SELECT C.CustomerID, C.Name, P.Number, P.Name, P.PhoneID, E.[Address], E.EmailID 
FROM Customers C
	LEFT OUTER JOIN PhoneNumber  P
	ON C.CustomerID = P.CustomerID
	LEFT OUTER JOIN Email E
	ON C.CustomerID = E.CustomerID
	

