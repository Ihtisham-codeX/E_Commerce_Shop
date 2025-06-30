DELIMITER $$

DROP PROCEDURE IF EXISTS ConfirmPurchase;
CREATE PROCEDURE ConfirmPurchase(IN in_CustomerID INT)
BEGIN
    DECLARE v_CartID INT;
    DECLARE v_OrderID INT;

    -- Step 1: Get the Cart ID
    SELECT CartID INTO v_CartID FROM Carts WHERE CustomerID = in_CustomerID;

    -- Step 2: Check if any product's quantity in the cart exceeds stock
    IF EXISTS (
        SELECT 1
        FROM CartItems ci
        JOIN Products p ON ci.ProductID = p.ProductID
        WHERE ci.CartID = v_CartID AND ci.Quantity > p.Quantity
    ) THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'One or more items in your cart exceed available stock.';
    END IF;

    -- Step 3: Insert Order
    INSERT INTO Orders (CustomerID) VALUES (in_CustomerID);
    SET v_OrderID = LAST_INSERT_ID();

    -- Step 4: Insert OrderItems from CartItems
    INSERT INTO OrderItems (OrderID, ProductID, Quantity, Price)
    SELECT 
        v_OrderID,
        ci.ProductID,
        ci.Quantity,
        p.Price
    FROM CartItems ci
    JOIN Products p ON ci.ProductID = p.ProductID
    WHERE ci.CartID = v_CartID;

    -- Step 5: Deduct stock from Products table
    UPDATE Products p
    JOIN CartItems ci ON p.ProductID = ci.ProductID
    SET p.Quantity = p.Quantity - ci.Quantity
    WHERE ci.CartID = v_CartID;

    -- Step 6: Clear CartItems
    DELETE FROM CartItems WHERE CartID = v_CartID;
END$$

DELIMITER ;
