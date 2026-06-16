CREATE TABLE sensor_data
(
    sensor_id BIGINT AUTO_INCREMENT PRIMARY KEY,
    home_id VARCHAR(50) NOT NULL,
    room_name VARCHAR(50) NOT NULL,
    sensing_datetime DATETIME NOT NULL,
    temp DOUBLE NOT NULL,
    humid DOUBLE NOT NULL,
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP
);

SELECT COUNT(*) FROM sensor_data;