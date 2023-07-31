USE BlogDB;

CREATE TABLE Users
(
	userID INT IDENTITY(1,1) PRIMARY KEY,
	userName VARCHAR(20) NOT NULL,
	email VARCHAR(50) NOT NULL,
	password VARCHAR(50) NOT NULL,
)

CREATE TABLE Categories
(
	category VARCHAR(20) NOT NULL 
)

CREATE TABLE Posts
(
	postID INT IDENTITY(1,1) PRIMARY KEY,
	userID INT,
	userName VARCHAR(20),
	category VARCHAR(20),
	Title VARCHAR(20),
	Body VARCHAR(200),
	comment VARCHAR(200),
	dateTime DATETIME,
	FOREIGN KEY (userID) REFERENCES users(userID),
	FOREIGN KEY (category) REFERENCES categories(category)
)
CREATE TABLE Comments
(
	commentID INT IDENTITY(1,1) PRIMARY KEY,
	userID INT,
	userName VARCHAR(20),
	postID INT,
	comment VARCHAR(200),
	dateTime DATETIME,
	FOREIGN KEY (userID) REFERENCES users(userID),
	FOREIGN KEY (postID) REFERENCES Posts(postID),
)

