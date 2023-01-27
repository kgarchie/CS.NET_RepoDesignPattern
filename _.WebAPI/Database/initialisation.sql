CREATE TABLE Users
(
    DbUserId      INT AUTO_INCREMENT NOT NULL,
    Balance       INT NOT NULL DEFAULT 0,
    DbTransaction INT,
    AccountStatus TINYINT,
    PRIMARY KEY (DbUserId)
);

CREATE TABLE UsersInfo
(
    DBUserId       INT,
    FirstName      VARCHAR(30) NOT NULL,
    MiddleName     VARCHAR(30),
    LastName       VARCHAR(30) NOT NULL,
    NationalUserId INT         NOT NULL,
    LastTopUp      DATETIME,
    Joined         DATETIME,
    PRIMARY KEY (NationalUserId),
    FOREIGN KEY (DBUserId) REFERENCES Users (DbUserId)
);

CREATE TABLE Transactions
(
    DbTransactionId     INT AUTO_INCREMENT NOT NULL,
    TransactionType     TINYINT,
    TransactionAmount   INT NOT NULL,
    FromUserId          INT,
    ToUserId            INT,
    TransactionDate     DATETIME DEFAULT CURRENT_TIMESTAMP,
    SystemTransactionId VARCHAR(30),
    PRIMARY KEY (DbTransactionId),
    FOREIGN KEY (FromUserId) REFERENCES Users (DbUserId),
    FOREIGN KEY (ToUserId) REFERENCES Users (DbUserId),
    UNIQUE (DbTransactionId, FromUserId, ToUserId)
);

ALTER TABLE Transactions
ADD CONSTRAINT UNIQUE(SystemTransactionId, FromUserId, ToUserId)

ALTER TABLE UsersInfo
ADD COLUMN Password VARCHAR(255) DEFAULT NULL

ALTER TABLE Users
DROP COLUMN `DbTransaction`

INSERT INTO Users (Balance, AccountStatus)
VALUES (1000, 1), (2000, 1), (3000, 1), (4000, 1), (5000, 1), (6000, 1), (7000, 1), (8000, 1), (9000, 1), (10000, 1), (11000, 1), (12000, 1), (13000, 1), (14000, 1), (15000, 1), (16000, 1), (17000, 1), (18000, 1), (19000, 1), (20000, 1);

INSERT INTO UsersInfo (DBUserId, FirstName, LastName, NationalUserId, LastTopUp, Joined)
VALUES (1, 'John', 'Doe', 1001, '2022-01-01', '2022-01-01'), (2, 'Jane', 'Doe', 1002, '2022-01-01', '2022-01-01'), (3, 'Bob', 'Smith', 1003, '2022-01-01', '2022-01-01'), (4, 'Emily', 'Johnson', 1004, '2022-01-01', '2022-01-01'), (5, 'Michael', 'Williams', 1005, '2022-01-01', '2022-01-01'), (6, 'Emily', 'Jones', 1006, '2022-01-01', '2022-01-01'), (7, 'Jacob', 'Brown', 1007, '2022-01-01', '2022-01-01'), (8, 'Nicholas', 'Miller', 1008, '2022-01-01', '2022-01-01'), (9, 'Alexander', 'Davis', 1009, '2022-01-01', '2022-01-01'), (10, 'William', 'Garcia', 1010, '2022-01-01', '2022-01-01'), (11, 'Joshua', 'Rodriguez', 1011, '2022-01-01', '2022-01-01'), (12, 'Matthew', 'Martinez', 1012, '2022-01-01', '2022-01-01'), (13, 'Andrew', 'Hernandez', 1013, '2022-01-01', '2022-01-01'), (14, 'Daniel', 'Lopez', 1014, '2022-01-01', '2022-01-01'), (15, 'Joseph', 'Gonzalez', 1015, '2022-01-01', '2022-01-01'), (16, 'Brian', 'Wilson', 1016, '2022-01-01', '2022-01-01'), (17, 'Adam', 'Anderson', 1017, '2022-01-01', '2022-01-01'), (18, 'Karen', 'Thomas', 1018, '2022-01-01', '2022-01-01'), (19, 'Natalie', 'Jackson', 1019, '2022-01-01', '2022-01-01'), (20, 'Samantha', 'White', 1020, '2022-01-01', '2022-01-01');

INSERT INTO Transactions (TransactionType, TransactionAmount, FromUserId, ToUserId, SystemTransactionId)
VALUES (1, 100, 1, 2, 'T0001'), (1, 50, 2, 3, 'T0002'), (1, 25, 3, 4, 'T0003'), (2, 200, 4, 5, 'T0004'), (2, 150, 5, 6, 'T0005'), (2, 75, 6, 7, 'T0006'), (1, 250, 7, 8, 'T0007'), (1, 125, 8, 9, 'T0008'), (2, 300, 9, 10, 'T0009'), (1, 50, 10, 11, 'T0010'), (2, 350, 11, 12, 'T0011'), (2, 175, 12, 13, 'T0012'), (1, 400, 13, 14, 'T0013'), (1, 200, 14, 15, 'T0014'), (2, 450, 15, 16, 'T0015'), (1, 225, 16, 17, 'T0016'), (2, 500, 17, 18, 'T0017'), (1, 250, 18, 19, 'T0018'), (2, 550, 19, 20, 'T0019');

ALTER TABLE users
    MODIFY COLUMN AccountStatus TINYINT NOT NULL DEFAULT 1;