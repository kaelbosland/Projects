USE [KB_Database]
GO
/****** Object:  Table [dbo].[MSG_Follows]    Script Date: 2018-05-16 9:31:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MSG_Follows](
	[username] [varchar](20) NOT NULL,
	[follows] [varchar](20) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MSG_Message]    Script Date: 2018-05-16 9:31:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MSG_Message](
	[message] [varchar](100) NOT NULL,
	[username] [varchar](20) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MSG_User]    Script Date: 2018-05-16 9:31:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MSG_User](
	[firstName] [varchar](30) NOT NULL,
	[lastName] [varchar](30) NOT NULL,
	[username] [varchar](20) NOT NULL,
	[password] [varchar](20) NOT NULL,
 CONSTRAINT [PK_MSG_User] PRIMARY KEY CLUSTERED 
(
	[username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UNI_Announcement]    Script Date: 2018-05-16 9:31:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UNI_Announcement](
	[announcement] [varchar](240) NOT NULL,
	[courseCode] [varchar](10) NOT NULL,
	[professorID] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UNI_Class]    Script Date: 2018-05-16 9:31:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UNI_Class](
	[className] [varchar](30) NOT NULL,
	[courseCode] [varchar](10) NOT NULL,
	[numEnrolled] [int] NOT NULL,
	[maxEnroll] [int] NOT NULL,
	[department] [varchar](30) NOT NULL,
	[professorID] [int] NULL,
 CONSTRAINT [PK_UNI_Class] PRIMARY KEY CLUSTERED 
(
	[courseCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UNI_Department]    Script Date: 2018-05-16 9:31:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UNI_Department](
	[departmentName] [varchar](30) NOT NULL,
 CONSTRAINT [PK_UNI_Department] PRIMARY KEY CLUSTERED 
(
	[departmentName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UNI_Email]    Script Date: 2018-05-16 9:31:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UNI_Email](
	[subject] [varchar](50) NULL,
	[message] [varchar](240) NOT NULL,
	[sender] [varchar](20) NOT NULL,
	[reciever] [varchar](20) NOT NULL,
	[emailID] [int] NOT NULL,
	[status] [varchar](30) NOT NULL,
 CONSTRAINT [PK_UNI_Email] PRIMARY KEY CLUSTERED 
(
	[emailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UNI_LoginInformation]    Script Date: 2018-05-16 9:31:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UNI_LoginInformation](
	[username] [varchar](20) NOT NULL,
	[password] [varchar](20) NOT NULL,
	[pID] [int] NOT NULL,
	[type] [varchar](20) NOT NULL,
 CONSTRAINT [PK_UNI_LoginInformation] PRIMARY KEY CLUSTERED 
(
	[username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UNI_Person]    Script Date: 2018-05-16 9:31:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UNI_Person](
	[pID] [int] NOT NULL,
	[firstName] [varchar](20) NOT NULL,
	[lastName] [varchar](20) NOT NULL,
 CONSTRAINT [PK_UNI_Person] PRIMARY KEY CLUSTERED 
(
	[pID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UNI_Professor]    Script Date: 2018-05-16 9:31:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UNI_Professor](
	[pID] [int] NOT NULL,
	[firstName] [varchar](20) NOT NULL,
	[lastName] [varchar](20) NOT NULL,
	[department] [varchar](30) NOT NULL,
	[classOne] [varchar](10) NULL,
	[classTwo] [varchar](10) NULL,
 CONSTRAINT [PK_UNI_Professor] PRIMARY KEY CLUSTERED 
(
	[pID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UNI_Program]    Script Date: 2018-05-16 9:31:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UNI_Program](
	[programName] [varchar](30) NOT NULL,
	[programLength] [int] NOT NULL,
	[department] [varchar](30) NOT NULL,
 CONSTRAINT [PK_UNI_Program] PRIMARY KEY CLUSTERED 
(
	[programName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UNI_Student]    Script Date: 2018-05-16 9:31:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UNI_Student](
	[pID] [int] NOT NULL,
	[firstName] [varchar](20) NOT NULL,
	[lastName] [varchar](20) NOT NULL,
	[year] [int] NOT NULL,
	[major] [varchar](30) NOT NULL,
	[classOne] [varchar](10) NULL,
	[classTwo] [varchar](10) NULL,
	[classThree] [varchar](10) NULL,
	[classFour] [varchar](10) NULL,
 CONSTRAINT [PK_UNI_Student] PRIMARY KEY CLUSTERED 
(
	[pID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[MSG_FollowUser]    Script Date: 2018-05-16 9:31:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[MSG_FollowUser]
	@user varchar(20),
	@userToFollow varchar(20)
AS
BEGIN
	INSERT INTO dbo.MSG_Follows (username, follows)
	VALUES (@user, @userToFollow)
END
GO
/****** Object:  StoredProcedure [dbo].[MSG_ShowUsers]    Script Date: 2018-05-16 9:31:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[MSG_ShowUsers]
	@username varchar(20)
AS
BEGIN
	SELECT firstName, lastName, username FROM MSG_User
	WHERE (username NOT IN (SELECT follows FROM MSG_Follows WHERE username = @username ))
END
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[MSG_SignIn]    Script Date: 2018-05-16 9:31:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[MSG_SignIn]
	@username VARCHAR(20),
	@password VARCHAR(20)
AS
BEGIN
	IF (@username IN (SELECT username FROM dbo.MSG_User))
		BEGIN
			IF (@password = (SELECT password FROM dbo.Msg_User WHERE username = @username))
				RETURN 5
			ELSE
				RETURN 1
		END
	ELSE 
		RETURN 0
END
GO
/****** Object:  StoredProcedure [dbo].[MSG_SignUp]    Script Date: 2018-05-16 9:31:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[MSG_SignUp]
	@username varchar(20),
	@password varchar(20),
	@firstName varchar(30),
	@lastName varchar(30)
AS
BEGIN
	IF (@userName not in (SELECT userName FROM MSG_User)) 
		BEGIN
			INSERT INTO Msg_User (firstName, lastName, username, password)
			VALUES (@firstName, @lastName, @username, @password)
			RETURN 5
		END
	ELSE
		RETURN 0 --error code for username already taken
END
GO
/****** Object:  StoredProcedure [dbo].[UNI_CheckEmail]    Script Date: 2018-05-16 9:31:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UNI_CheckEmail]
	@username varchar(20)
AS
	SELECT emailID, subject, message, sender, status FROM dbo.UNI_Email WHERE reciever = @username ORDER BY emailID DESC
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[UNI_CheckSentMail]    Script Date: 2018-05-16 9:31:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UNI_CheckSentMail]
	@username varchar(20)
AS
	SELECT emailID, reciever, subject, message, status FROM dbo.UNI_Email WHERE sender = @username
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[UNI_CheckType]    Script Date: 2018-05-16 9:31:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kael Bosland
-- Description:	Stored procedure checking the type of user associated with an account
-- =============================================

CREATE PROCEDURE [dbo].[UNI_CheckType]

@pid int

AS
BEGIN
	-- checks if the user is a student 
	IF ('Student' in (SELECT type FROM dbo.UNI_LoginInformation WHERE pid = @pid))
		return 0 --if so, returns 0 which is used as the access level for the student
	--checks if the user is a professor
	ELSE IF ('Professor' in (SELECT type FROM dbo.UNI_LoginInformation WHERE pid = @pid)) 
		return 1 --if so, returns 1 which is used as the access level for the professor
	ELSE 
		return 2 --since student or professor are the only two options, if neither of previous statements are true then assumes admin priveleges
END
GO
/****** Object:  StoredProcedure [dbo].[UNI_CheckUsername]    Script Date: 2018-05-16 9:31:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kael Bosland
-- Description:	Stored procedure for checking if the username, pID and password provided during login are valid
-- =============================================

CREATE PROCEDURE [dbo].[UNI_CheckUsername]
	@username varchar(20),
	@pid int,
	@password varchar(20)
AS
BEGIN
	--if the pID is registered in the login table
	IF (@pid in (SELECT pID FROM dbo.UNI_LoginInformation))
		BEGIN
		--if the username corresponds to the same entry in the login table as the pID
			IF (@username = (SELECT userName FROM dbo.UNI_LoginInformation WHERE pid = @pid))
				BEGIN
				-- if the password corresponds to the same entry in the login table as the pID and the username
					IF (@password = (SELECT password from dbo.UNI_LoginInformation WHERE pid = @pid))
						return 1 --sucessfull login
					ELSE
						return 2 --error code for invalid password
				END
			ELSE
				return 0 --error code for invalid username
		END
	ELSE
		return 3 --error code for invalid pID
END
GO
/****** Object:  StoredProcedure [dbo].[UNI_CreateProfile]    Script Date: 2018-05-16 9:31:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kael Bosland
-- Description:	Stored procedure for creating a profile to be added to the login table
-- =============================================

CREATE PROCEDURE [dbo].[UNI_CreateProfile]
	@username varchar(20),
	@password varchar(20),
	@pid int,
	@type varchar(20)
AS
BEGIN
	--if the username is not already taken
	IF (@username not in (SELECT userName FROM dbo.UNI_LoginInformation))
		BEGIN
			--if the pID type and the type inputted by the user matches
			IF ( (@pid in (SELECT piD FROM dbo.UNI_Professor) and @type = 'Professor') or (@pid in (SELECT piD FROM dbo.UNI_Student) and @type = 'Student') )
				--add a new entry in the login table for the user to login, account created successfully
				insert into dbo.UNI_LoginInformation (username, password, pID, type)
				values (@username, @password, @pID, @type)
			ELSE
				return 3 --error code for profile not corresponding to valid pID
		END
	ELSE
		return 1 --error code for username already taken
END
RETURN 5 --safely completed procedure
GO
/****** Object:  StoredProcedure [dbo].[UNI_DropClass]    Script Date: 2018-05-16 9:31:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kael Bosland
-- Description:	Stored procedure for creating a profile to be added to the login table
-- =============================================

CREATE PROCEDURE [dbo].[UNI_DropClass]
	@pID int,
	@courseCode varchar(10)
	
AS
BEGIN
	--if the pID corresponds to a student and the course code is valid
	IF (@pID IN (SELECT pID FROM dbo.UNI_Student) AND @courseCode IN (SELECT courseCode FROM dbo.UNI_Class))
		BEGIN
			--if the course code is listed in the classOne slot for the student
			IF (@courseCode = (SELECT classOne FROM UNI_Student WHERE pID = @pID))
				BEGIN
					--set the classOne entry to null to show class is dropped, and decrement number of students enrolled in the class
					UPDATE dbo.UNI_Student
					SET classOne = Null
					WHERE pID = @pID

					UPDATE dbo.UNI_Class
					SET numEnrolled = numEnrolled - 1
					WHERE courseCode = @courseCode
				END
			--if the course code is listed in the classTwo slot for the student
			ELSE IF @courseCode = (SELECT classTwo FROM UNI_Student WHERE pID = @pID)
				BEGIN
					--set the classTwo entry to null to show class is dropped, and decrement number of students enrolled in the class
					UPDATE dbo.UNI_Student
					SET classTwo = Null
					WHERE pID = @pID

					UPDATE dbo.UNI_Class
					SET numEnrolled = numEnrolled - 1
					WHERE courseCode = @courseCode
				END
			--if the course code is listed in the classThree slot for the student
			ELSE IF @courseCode = (SELECT classThree FROM UNI_Student WHERE pID = @pID)
				BEGIN
					--set the classThree entry to null to show class is dropped, and decrement number of students enrolled in the class
					UPDATE dbo.UNI_Student
					SET classThree = Null
					WHERE pID = @pID

					UPDATE dbo.UNI_Class
					SET numEnrolled = numEnrolled - 1
					WHERE courseCode = @courseCode
				END
			--if the course code is listed in the classFour slot for the student
			ELSE IF @courseCode = (SELECT classFour FROM UNI_Student WHERE pID = @pID)
				BEGIN
					--set the classThree entry to null to show class is dropped, and decrement number of students enrolled in the class
					UPDATE dbo.UNI_Student
					SET classFour = Null
					WHERE pID = @pID

					UPDATE dbo.UNI_Class
					SET numEnrolled = numEnrolled - 1
					WHERE courseCode = @courseCode
				END
			ELSE
				return 1 --error code for the student is not enrolled in the class they are trying to drop
		END
	ELSE
		return 2 --invalid pID or courseCode entered
END
RETURN 5 --successfully completed
GO
/****** Object:  StoredProcedure [dbo].[UNI_EnrollStudentInClass]    Script Date: 2018-05-16 9:31:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--===============================
-- Author: Kael Bosland
-- Description: Stored Procedure to allow user to enroll a student in a class
--===============================

CREATE PROCEDURE [dbo].[UNI_EnrollStudentInClass]
	@pID int,
	@courseCode varchar(10)
AS
BEGIN
	SET NOCOUNT ON;

	--checks if the pID is in the Student table, and courseCode is in the Class table
	IF (@pid in (SELECT pid FROM dbo.UNI_Student)) AND (@courseCode in (SELECT courseCode FROM dbo.UNI_Class))
		BEGIN
			--declared a few constants instead of having to use subqueries constantly in our code, increases readability
			DECLARE @A varchar(10), @B varchar(10), @C varchar(10), @D varchar(10);
			SET @A = (SELECT classOne FROM dbo.UNI_Student WHERE pID = @pID);
			SET @B = (SELECT classTwo FROM dbo.UNI_Student WHERE pID = @pID);
			SET @C = (SELECT classThree FROM dbo.UNI_Student WHERE pID = @pID);
			SET @D = (SELECT classFour FROM dbo.UNI_Student WHERE pID = @pID);

			--checks if the class is full
			IF ((SELECT numEnrolled FROM dbo.UNI_Class WHERE courseCode = @courseCode) + 1 <= (SELECT maxEnroll FROM dbo.UNI_Class WHERE courseCode = @courseCode))
				BEGIN
					--if the classOne slot for the student is open
					IF ((SELECT classOne FROM dbo.UNI_Student WHERE pID = @pID) is Null)
						--ensures that the student is not already enrolled in the class by checking if the courseCode is equivalent to the classTwo, classThree or ClassFour
						IF  ((@courseCode != @B OR @B is null) AND (@courseCode != @C OR @C is null) AND (@courseCode != @D OR @D is null))
							--updating  the classOne slot with the course code 
							UPDATE dbo.UNI_STUDENT
							SET classOne = @courseCode
							WHERE pid = @pID
						ELSE
							return 6 --error code for student already enrolled in the class
					--if the classTwo slot for the student is open
					ELSE IF ((SELECT classTwo FROM dbo.UNI_Student WHERE pID = @pID) is Null)
						IF  ((@courseCode != @A OR @A is null) AND (@courseCode != @C OR @C is null) AND (@courseCode != @D OR @D is null))
							--updating  the classTwo slot with the course code 
							UPDATE dbo.UNI_STUDENT
							SET classTwo = @courseCode
							WHERE pid = @pID
						ELSE	
							return 6 --error code for student already enrolled in the class
					--if the classThree slot for the student is open
					ELSE IF ((SELECT classThree FROM dbo.UNI_Student WHERE pID = @pID) is Null)
						IF  ((@courseCode != @B OR @B is null) AND (@courseCode != @A OR @A is null) AND (@courseCode != @D OR @D is null))
							--updating  the classThree slot with the course code 
							UPDATE dbo.UNI_STUDENT
							SET classThree = @courseCode
							WHERE pid = @pID
						ELSE 
							return 6 --error code for student already enrolled in the class
					--if the classFour slot for the student is open
					ELSE IF ((SELECT classFour FROM dbo.UNI_Student WHERE pID = @pID) is Null)
						IF  ((@courseCode != @B OR @B is null) AND (@courseCode != @C OR @C is null) AND (@courseCode != @A OR @A is null))
							--updating  the classFour slot with the course code 
							UPDATE dbo.UNI_STUDENT
							SET classFour = @courseCode
							WHERE pid = @pID
						ELSE
							return 6 --error code for student already enrolled in the class
					ELSE	
						--no empty classes, must drop one first
						return 1

					--incrementing the number of enrolled students in the specified class after the enrolling was succcessful
					UPDATE dbo.UNI_Class
						SET
							numEnrolled = numEnrolled + 1
						WHERE 
							courseCode = @courseCode
				END
			ELSE
				--class is full, raising exception to tell user class is full
				return 2
		END
	ELSE
		return 3 --error code for invalid pID or course code
	
END
RETURN 5
GO
/****** Object:  StoredProcedure [dbo].[UNI_GetAnnouncements]    Script Date: 2018-05-16 9:31:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UNI_GetAnnouncements]
	@classOne varchar(10),
	@classTwo varchar(10),
	@classThree varchar(10),
	@classFour varchar(10)
AS
	(SELECT announcement, courseCode FROM UNI_Announcement WHERE courseCode = @classOne) UNION
	(SELECT announcement, courseCode FROM UNI_Announcement WHERE courseCode = @classTwo) UNION
	(SELECT announcement, courseCode FROM UNI_Announcement WHERE courseCode = @classThree) UNION
	(SELECT announcement, courseCode FROM UNI_Announcement WHERE courseCode = @classFour)
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[UNI_GetCurrentEmailID]    Script Date: 2018-05-16 9:31:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UNI_GetCurrentEmailID]
AS
	DECLARE @currentID int
	SET @currentID = (SELECT TOP 1 emailID FROM UNI_Email ORDER BY emailID DESC) + 1
RETURN @currentID
GO
/****** Object:  StoredProcedure [dbo].[UNI_GetCurrentPID]    Script Date: 2018-05-16 9:31:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--===============================
-- Author: Kael Bosland
-- Description: Stored Procedure to allow user to enroll a student in a class
--===============================

CREATE PROCEDURE [dbo].[UNI_GetCurrentPID]
	
AS
	--returning the amount of users currently registered in the system so program knows what pID to register to new users
	DECLARE @currentPID int
	SET @currentPID = (SELECT COUNT(*) FROM UNI_PERSON);
RETURN @currentPID
GO
/****** Object:  StoredProcedure [dbo].[UNI_InsertPerson]    Script Date: 2018-05-16 9:31:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--===============================
-- Author: Kael Bosland
-- Create Date: May 3rd/ 2018
-- Description: Stored Procedure to allow user to add Persons to the Person table
--===============================

CREATE PROCEDURE [dbo].[UNI_InsertPerson]
	@pID int,
	@firstName varchar(20),
	@lastName varchar(20)
AS
BEGIN

	SET NOCOUNT ON;
	BEGIN TRY
			--insert a new person into the database
			INSERT INTO dbo.UNI_Person (pID, firstName, lastName)
			VALUES (@pID, @firstName, @lastName)

	END TRY
	BEGIN CATCH 
		SELECT ERROR_MESSAGE() AS ErrorMessage
		PRINT(ERROR_MESSAGE());
	END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[UNI_InsertProf]    Script Date: 2018-05-16 9:31:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--===============================
-- Author: Kael Bosland
-- Create Date: May 3rd/ 2018
-- Description: Stored Procedure to allow user to add a Professor to the Professor table
--===============================

CREATE PROCEDURE [dbo].[UNI_InsertProf]
	@pID int,
	@firstName varchar(20),
	@lastName varchar(20),
	@department varchar(30),
	@classOne varchar(10),
	@classTwo varchar(10)
AS
BEGIN

	SET NOCOUNT ON;
	--if the department is valid, and the classOne and classTwo slots are either valid classes or empty
	IF (@department in (SELECT departmentName from dbo.UNI_Department)) AND
	(@classOne in (SELECT courseCode from dbo.UNI_Class) OR @classOne is null) AND
	(@classTwo in (SELECT courseCode from dbo.UNI_Class) OR @classTwo is null)
		BEGIN
				--ensuring that the classOne and classTwo slots are not the same
				IF ( (@classOne is null) OR (@classOne != @classTwo OR @classTwo is null))
					BEGIN
						--if the classOne slot has a course code, this checks if the class is already being taught
						IF ((SELECT professorID FROM Uni_Class WHERE courseCode = @classOne) is null)
							--if not, then it updates the profID on the classOne slot to signify that this prof now teaches this class
							UPDATE dbo.UNI_Class
								SET professorID = @pID
								WHERE courseCode = @classOne
						ELSE
							--otherwise, returns 2 which is the error code for classOne is already being taught
							return 2

						--if the classTwo slot has a course code, this checks if the class is already being taught
						IF ((SELECT professorID FROM Uni_Class WHERE courseCode = @classTwo) is null)
							--if not, then it updates the profID on the classTwo slot to signify that this prof now teaches this class
							UPDATE dbo.UNI_Class
								SET professorID = @pID
								WHERE courseCode = @classTwo
						ELSE
							--otherwise, returns 3 which is the error code for classTwo is already being taught
							return 3

						INSERT INTO dbo.UNI_Professor (pID, firstName, lastName, department, classOne, classTwo)
						VALUES (@pID, @firstName, @lastName, @department, @classOne, @classTwo)
					END
				ELSE
					return 4 --error code returned for classOne and classTwo slots being the same
		END
	ELSE
		--error code returned for invalid department or invalid classOne or classTwo course code
		return 1

END
RETURN 5
GO
/****** Object:  StoredProcedure [dbo].[UNI_InsertStudent]    Script Date: 2018-05-16 9:31:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--===============================
-- Author: Kael Bosland
-- Create Date: May 3rd/ 2018
-- Description: Stored Procedure to allow user to add Students to the Student table
--===============================

CREATE PROCEDURE [dbo].[UNI_InsertStudent]
	@pID int,
	@firstName varchar(20),
	@lastName varchar(20),
	@year int,
	@major varchar(30),
	@classOne varchar(10),
	@classTwo varchar(10),
	@classThree varchar(10),
	@classFour varchar(10)
AS
BEGIN

	SET NOCOUNT ON;
		--checks if the major is valid, as well as if the classOne, Two, Three and Four entries are valid
		IF (@major in (SELECT programName FROM dbo.UNI_Program)) AND
		(@classOne in (SELECT courseCode FROM dbo.UNI_Class) OR @classOne is null) AND
		(@classTwo in (SELECT courseCode FROM dbo.UNI_Class) OR @classTwo is null) AND
		(@classThree in (SELECT courseCode FROM dbo.UNI_Class) OR @classThree is null) AND
		(@classFour in (SELECT courseCode FROM dbo.UNI_Class) OR @classFour is null) 
			BEGIN
				--checks if any of the classes entered have the same coursecode
				IF ( ((@classOne is null) OR ( (@classOne != @classTwo OR @classTwo is null) AND (@classOne != @classThree OR @classThree is null) AND (@classOne != @classFour OR @classFour is null))) AND
				( (@classTwo is null) OR ((@classTwo != @classThree OR @classThree is null) AND (@classTwo != @classFour OR @classFour is null))) AND
				( (@classThree is null) OR ((@classThree != @classFour OR @classFour is null))) )
					BEGIN
						--checks if the classOne slot is either null or not full
						IF (@classOne is null OR (@classOne is not null AND (SELECT numEnrolled FROM dbo.UNI_Class WHERE courseCode = @classOne) + 1 <= (SELECT maxEnroll FROM dbo.UNI_Class WHERE courseCode = @classOne)))
							BEGIN
								--checks if the classTwo slot is either null or not full
								IF (@classTwo is null) OR (@classTwo is not null AND ((SELECT numEnrolled FROM dbo.UNI_Class WHERE courseCode = @classTwo) + 1 <= (SELECT maxEnroll FROM dbo.UNI_Class WHERE courseCode = @classTwo)))
									BEGIN
										--checks if the classThree slot is either null or not full
										IF (@classThree is null) OR (@classThree is not null AND ((SELECT numEnrolled FROM dbo.UNI_Class WHERE courseCode = @classThree) + 1 <= (SELECT maxEnroll FROM dbo.UNI_Class WHERE courseCode = @classThree)))
											BEGIN
												--checks if the classFour slot is either null or not full
												IF (@classFour is null) OR (@classFour is not null AND ((SELECT numEnrolled FROM dbo.UNI_Class WHERE courseCode = @classFour) + 1 <= (SELECT maxEnroll FROM dbo.UNI_Class WHERE courseCode = @classFour)))
													BEGIN
														--for each of the class slots that are not null, inrements the number of enrolled students in the class
														IF (@classOne is not null)
															UPDATE dbo.UNI_Class
															SET numEnrolled = numEnrolled + 1
															WHERE courseCode = @classOne
														IF (@classTwo is not null)
															UPDATE dbo.UNI_Class
															SET numEnrolled = numEnrolled + 1
															WHERE courseCode = @classTwo
														IF (@classThree is not null)
															UPDATE dbo.UNI_Class
															SET numEnrolled = numEnrolled + 1
															WHERE courseCode = @classThree
														IF (@classFour is not null)
															UPDATE dbo.UNI_Class
															SET numEnrolled = numEnrolled + 1
															WHERE courseCode = @classFour

														--adds the new student into the students table with all the given information
														INSERT INTO dbo.UNI_Student (pID, firstName, lastName, year, major, classOne, classTwo, classThree, classFour)
														VALUES (@pID, @firstName, @lastName, @year, @major, @classOne, @classTwo, @classThree, @classFour)
													END
												ELSE
													return 4 --error code returned for classfour being full
											END
										ELSE
											return 3 --error code returned for classThree being full
									END
								ELSE	
									return 2 --error code returned for classTwo being full
							END
						ELSE
							return 1 --error code returned for classOne being full
					END
				ELSE
					return 6 --error code returned for entering the same class 2 or more times in class slots
			END
		ELSE
			return 0 --error code returned for an invalid major or courseCode

END
RETURN 5 --returned for successful addition of new student
GO
/****** Object:  StoredProcedure [dbo].[UNI_MakeAnnouncement]    Script Date: 2018-05-16 9:31:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UNI_MakeAnnouncement]
	@professorID int,
	@courseCode varchar(10),
	@announcement varchar(240)
AS
BEGIN
	IF ((@courseCode = (SELECT classOne FROM UNI_Professor WHERE pID = @professorID)) OR
	   (@courseCode = (SELECT classTwo FROM UNI_Professor WHERE pID = @professorID)))
	   INSERT INTO UNI_Announcement (announcement, courseCode, professorID)
	   VALUES
	   (@announcement, @courseCode, @professorID)
	ELSE
		return 1 --error code for professor not teaching the class hes trying to make an announcement for
END
RETURN 5 --completed successfully
GO
/****** Object:  StoredProcedure [dbo].[UNI_MakeProfTeachClass]    Script Date: 2018-05-16 9:31:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--===============================
-- Author: Kael Bosland
-- Create Date: May 3rd/ 2018
-- Description: Stored Procedure to assign a professor to teach a class
--===============================

CREATE PROCEDURE [dbo].[UNI_MakeProfTeachClass]
	@pID int,
	@courseCode varchar(10)
AS
BEGIN
	SET NOCOUNT ON;

		--checks if the pID is for a valid professor, and the courseCode is valid
		IF (@pid IN (SELECT pID FROM dbo.UNI_Professor)) AND (@courseCode IN (SELECT courseCode FROM dbo.UNI_Class)) 
			BEGIN
				--checks if the class is not already being taught
				IF ((SELECT professorID FROM dbo.UNI_Class WHERE courseCode = @courseCOde) is null) 
					BEGIN
						--if the classOne slot of the professor's profile is free, adds the course to the classOne slot
						IF ((SELECT classOne FROM dbo.UNI_Professor WHERE pID = @pID) is null)
							BEGIN
								UPDATE dbo.UNI_Professor
								SET
									classOne = @courseCode
								WHERE
									piD = @pID

								UPDATE dbo.UNI_Class
								SET
									professorID = @pID
								WHERE 
									courseCode = @courseCode
							END
						--if the classTwo slot of the professor's profile is free, adds the course to the classTwo slot
						ELSE IF ((SELECT classTwo FROM dbo.UNI_Professor WHERE pID = @pID) is null) 
			 				BEGIN
								UPDATE dbo.UNI_Professor
									SET
										classTwo = @courseCode
									WHERE
										piD = @pID

								UPDATE dbo.UNI_Class
									SET
										professorID = @pID
									WHERE 
										courseCode = @courseCode
							END
						ELSE 
							--error code returned if the prof has no valid spots open to add a class to teach
							return 0
					END
				ELSE
					-- error code returned if there is already a prof teaching this class
					return 1
			END
		ELSE
			-- error code returned if there is an invalid course code or pID provided
			return 2
END
RETURN 5
GO
/****** Object:  StoredProcedure [dbo].[UNI_OutputClasses]    Script Date: 2018-05-16 9:31:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--===============================
-- Author: Kael Bosland
-- Description: Stored Procedure to output all the classes that are offereewd
--===============================

CREATE PROCEDURE [dbo].[UNI_OutputClasses]

AS
BEGIN
	SELECT * FROM UNI_Class
END
GO
/****** Object:  StoredProcedure [dbo].[UNI_OutputProfessors]    Script Date: 2018-05-16 9:31:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--===============================
-- Author: Kael Bosland
-- Description: Stored Procedure to output all the professors
--===============================

CREATE PROCEDURE [dbo].[UNI_OutputProfessors]

AS
BEGIN
	DECLARE @numProfs int
	SET @numProfs = (SELECT COUNT(*) FROM UNI_Professor)
	SELECT * FROM UNI_Professor
END
RETURN @numProfs
GO
/****** Object:  StoredProcedure [dbo].[UNI_OutputStudents]    Script Date: 2018-05-16 9:31:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--===============================
-- Author: Kael Bosland
-- Description: Stored Procedure to output all the Students who are enrolled
--===============================

CREATE PROCEDURE [dbo].[UNI_OutputStudents]

AS
BEGIN
	DECLARE @numStudents int
	SET @numStudents = (SELECT COUNT(*) FROM UNI_Student)
	SELECT * FROM UNI_Student
END
RETURN @numStudents
GO
/****** Object:  StoredProcedure [dbo].[UNI_QuitTeachingClass]    Script Date: 2018-05-16 9:31:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--===============================
-- Author: Kael Bosland
-- Description: Stored Procedure to let a teacher drop teaching a class
--===============================

CREATE PROCEDURE [dbo].[UNI_QuitTeachingClass]
	@pID int,
	@courseCode varchar(10)
AS
BEGIN

	-- if the pID is valid
	IF (@pID in (SELECT pID FROM dbo.UNI_Professor))
		BEGIN
			--if the course code is valid
			IF (@courseCode in (SELECT courseCode FROM dbo.UNI_Class))
				BEGIN
					--if the courseCode corresponds to the classOne slot in the Professor entry
					IF (@courseCode = (SELECT classOne FROM dbo.UNI_Professor WHERE pID = @pID))
						BEGIN
							--set the classOne slot to null, and set the professorID for the class to null
							update dbo.UNI_Professor 
							set classOne = null
							where pID = @pID

							update dbo.uni_class
							set professorID = null
							where courseCode = @courseCode
							
							return 5
						END

					--if the courseCode corresponds to the classTwo slot in the Professor entry
					ELSE IF (@courseCode = (SELECT classTwo FROM dbo.UNI_Professor WHERE pID = @pID))
						BEGIN
							--set the classTwo slot to null, and set the professorID for the class to null
							update dbo.UNI_Professor
							set classTwo = null
							where pID = @pID

							update dbo.uni_class
							set professorID = null
							where courseCode = @courseCode
							return 5
						END
					ELSE
						return 2 --error code for prof not even teaching this class	
				END
			ELSE
				return 1 --error code for invalid coursecode
		END
	ELSE
		return 0 --error code for invalid pid, not a professor
END
GO
/****** Object:  StoredProcedure [dbo].[UNI_ReadEmail]    Script Date: 2018-05-16 9:31:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UNI_ReadEmail]
	@emailID int,
	@sender varchar(20)
AS
	IF ( @sender != (SELECT sender FROM UNI_Email WHERE  emailID = @emailID) )
		update UNI_Email
		SET status = 'READ AT: ' + CONVERT(varchar(5), getDate(), 108) + ' on ' + CONVERT(varchar(10), CONVERT (date, SYSDATETIME()))
		WHERE emailID = @emailID
		
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[UNI_SendEmail]    Script Date: 2018-05-16 9:31:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UNI_SendEmail]
	@emailID int,
	@subject varchar(50),
	@message varchar(240),
	@sender varchar(20),
	@reciever varchar(20),
	@status varchar(10)
AS
BEGIN
	--checking if the reciever is a valid, registered user
	IF (@reciever IN (SELECT username FROM dbo.UNI_LoginInformation))
		insert into dbo.UNI_Email (emailID, subject, message, sender, reciever, status)
		values (@emailID, @subject, @message, @sender, @reciever, @status)
	ELSE
		return 1 --error code for an invalid username to send email to
END
RETURN 5 --successful message send
GO
/****** Object:  StoredProcedure [dbo].[UNI_ShowContactList]    Script Date: 2018-05-16 9:31:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UNI_ShowContactList]
	@username varchar(20)
AS
	SELECT username, type FROM dbo.UNI_LoginInformation WHERE username != @username
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[UNI_WhatClasses]    Script Date: 2018-05-16 9:31:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UNI_WhatClasses]
	@pID int
AS
	SELECT classOne, classTwo, classThree, classFour FROM dbo.UNI_Student WHERE pID = @pID
RETURN 0
GO
