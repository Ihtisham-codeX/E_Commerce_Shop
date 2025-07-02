ALTER TABLE product_categories
ADD COLUMN Shop_ID INT NOT NULL,
ADD CONSTRAINT fk_product_categories_shop
    FOREIGN KEY (Shop_ID)
    REFERENCES shops(ShopID);
