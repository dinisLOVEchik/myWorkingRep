CREATE TABLE rates (curr1 nchar(3), curr2 nchar(3), rate decimal(18,2));

INSERT INTO rates (curr1, curr2, rate) VALUES ('RUB', 'EUR', ROUND(RAND()*(50-100), 2)), ('USD', 'CAD', ROUND(RAND()*(9-1)+0.8, 2)), ('JPY', 'SAR', ROUND(RAND()*(100-10), 2));

SELECT * FROM rates;