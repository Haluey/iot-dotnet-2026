CREATE TABLE products (
    product_id INT NOT NULL AUTO_INCREMENT,
    product_name VARCHAR(100) NOT NULL,
    category VARCHAR(50) NULL,
    price DECIMAL(10,0) NOT NULL,
    stock INT NOT NULL,
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (product_id)
);

INSERT INTO products(product_name, category, price, stock)
VALUES
('무선 마우스', '전자기기', 55000, 30),
('기계식 키보드', '전자기기', 89000, 15),
('텀블러', '생활용품', 18000, 50),
('노트북 거치대', '사무용품', 32000, 20),
('핸드폰 충전기', '전자기기', 18000, 10);

SELECT * FROM products;