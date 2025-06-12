
CREATE TABLE Users (
    UserId INT PRIMARY KEY IDENTITY(1,1),
    MasterUsername NVARCHAR(100) NOT NULL,
    MasterPasswordHash NVARCHAR(256) NOT NULL -- Hashed master password
);

