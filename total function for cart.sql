DELIMITER $$

CREATE FUNCTION GetCartTotal(in_CustomerID INT) 
RETURNS DECIMAL(10,2)
DETERMINISTIC
BEGIN
    DECLARE v_CartID INT;
    DECLARE v_Total DECIMAL(10,2);

    SELECT CartID INTO v_CartID FROM Carts WHERE CustomerID = in_CustomerID;

    SELECT SUM(ci.Quantity * p.Price) INTO v_Total
    FROM CartItems ci
    JOIN Products p ON ci.ProductID = p.ProductID
    WHERE ci.CartID = v_CartID;

    RETURN IFNULL(v_Total, 0.00);
END$$

DELIMITER ;
