CREATE TABLE "Company" (    
    "Id" SERIAL PRIMARY KEY,
    "Title" VARCHAR(255) NOT NULL,
    "CEO" VARCHAR(255),
    "Capital" decimal);
CREATE TABLE "Phones" (
    "Id" SERIAL PRIMARY KEY,
    "Title" VARCHAR(255) NOT NULL,
    "CompanyId" INT REFERENCES "Company"("Id"),
    "Price" DECIMAL(18, 2)
);
INSERT INTO "Company" ("Title", "CEO", "Capital")VALUES
('Apple', 'Гутман Артем', 400000000000),
('Samsung', 'Вадим Адильшаевич ', 90000000000000),
('Nokia', 'Тетервак', 500000000000000),
('Asus', 'Данилов', 5001000000),
('Xiaomi', 'Анна Вячеславовна', 900000);
INSERT INTO "Phones" ("Title", "CompanyId", "Price")VALUES
('iPhone 10', 1, 58000),
('Redmi Note 10S', 5, 28000),
('iPhone 12 Pro Max Super Idol', 1, 158000),
('Rog Phone', 4, 100000),
('Galaxy S24 Ulatra SuperPuperDuperShmuper', 2, 1000000);\