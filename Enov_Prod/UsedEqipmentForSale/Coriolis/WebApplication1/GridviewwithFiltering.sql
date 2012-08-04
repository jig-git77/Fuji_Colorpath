/****** Use this script to create a table of test data ******/
/****** Bartek Marnane ******/
/****** Evonet Consulting ******/
/****** http://www.evonet.com.au ******/

Use [Your database nane here]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_Employees](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](20) NULL,
	[LastName] [nvarchar](20) NULL,
	[Department] [nvarchar](40) NULL,
	[Location] [nvarchar](40) NULL
) ON [PRIMARY]

INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Guy', 'Gilbert', 'Production', 'Queensland');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Kevin', 'Brown', 'Marketing', 'Victoria');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Roberto', 'Tamburello', 'Engineering', 'Tasmania');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Rob', 'Walters', 'Tool Design', 'New South Wales');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Thierry', 'Deahers', 'Tool Design', 'New South Wales');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('David', 'Bradley', 'Marketing', 'Queensland');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('JoLynn', 'Dobney', 'Production', 'New South Wales');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Ruth', 'Ellerbrock', 'Production', 'Victoria');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Gail', 'Erickson', 'Engineering', 'Victoria');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Barry', 'Johnson', 'Production', 'Queensland');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Jossef', 'Goldberg', 'Engineering', 'Queensland');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Terri', 'Duffy', 'Engineering', 'Queensland');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Sidney', 'Higa', 'Production', 'Washington');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Taylor', 'Maxwell', 'Production', 'Oregon');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Jeffrey', 'Ford', 'Production', 'Washington');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Jo', 'Brown', 'Production', 'California');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Doris', 'Hartwig', 'Production', 'California');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('John', 'Campbell', 'Production', 'Victoria');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Diane', 'Glimp', 'Production', 'Victoria');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Steven', 'Selikoff', 'Production', 'British Columbia');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Peter', 'Krebs', 'Production Control', 'British Columbia');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Stuart', 'Munson', 'Production', 'Oregon');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Greg', 'Alderson', 'Production', 'Washington');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('David', 'Johnson', 'Production', 'California');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Zheng', 'Mu', 'Production', 'California');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Ivo', 'Salmre', 'Production', 'Queensland');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Paul', 'Komosinski', 'Production', 'New South Wales');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Ashvini', 'Sharma', 'Information Services', 'Victoria');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Kendall', 'Keil', 'Production', 'New South Wales');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Paula', 'Barreto de Mattos', 'Human Resources', 'Victoria');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Alejandro', 'McGuel', 'Production', 'South Australia');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Garrett', 'Young', 'Production', 'New South Wales');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Jian Shuo', 'Wang', 'Production', 'Victoria');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Susan', 'Eaton', 'Shipping and Receiving', 'New South Wales');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Vamsi', 'Kuppa', 'Shipping and Receiving', 'New South Wales');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Alice', 'Ciccu', 'Production', 'Victoria');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Simon', 'Rapier', 'Production', 'California');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Jinghao', 'Liu', 'Production', 'British Columbia');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Michael', 'Hines', 'Production', 'New South Wales');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Yvonne', 'McKay', 'Production', 'New South Wales');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Peng', 'Wu', 'Quality Assurance', 'Washington');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Jean', 'Trenary', 'Information Services', 'California');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Russell', 'Hunter', 'Production', 'California');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('A. Scott', 'Wright', 'Production Control', 'California');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Fred', 'Northup', 'Production', 'Queensland');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Sariya', 'Harnpadoungsataya', 'Marketing', 'New South Wales');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Willis', 'Johnson', 'Human Resources', 'New South Wales');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Jun', 'Cao', 'Production', 'Victoria');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Christian', 'Kleinerman', 'Facilities and Maintenance', 'Victoria');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Susan', 'Metters', 'Production', 'California');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Reuben', 'Dosa', 'Production', 'Tasmania');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Kirk', 'Koenigsbauer', 'Production', 'Queensland');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('David', 'Ortiz', 'Production', 'New South Wales');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Tengiz', 'Kharatishvili', 'Document Control', 'California');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Hanying', 'Feng', 'Production', 'Victoria');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Kevin', 'Liu', 'Production', 'Queensland');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Annik', 'Stahl', 'Production', 'New South Wales');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Suroor', 'Fatima', 'Production', 'New South Wales');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Deborah', 'Poe', 'Finance', 'South Australia');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Jim', 'Scardelis', 'Production', 'Queensland');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Carole', 'Poland', 'Production', 'Victoria');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('George', 'Li', 'Production', 'Queensland');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Gary', 'Yukish', 'Production', 'Oregon');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Cristian', 'Petculescu', 'Production', 'Washington');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Raymond', 'Sam', 'Production', 'California');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Janaina', 'Bueno', 'Information Services', 'California');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Bob', 'Hohman', 'Production', 'Oregon');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Shammi', 'Mohamed', 'Production', 'Washington');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Linda', 'Moschell', 'Production', 'Victoria');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Mindy', 'Martin', 'Human Resources', 'Queensland');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Wendy', 'Kahn', 'Finance', 'Victoria');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Kim', 'Ralls', 'Shipping and Receiving', 'Queensland');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Sandra', 'Reátegui Alayo', 'Production', 'New South Wales');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Kok-Ho', 'Loh', 'Production', 'New South Wales');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Douglas', 'Hite', 'Production', 'South Australia');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('James', 'Kramer', 'Production', 'Victoria');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Sean', 'Alexander', 'Quality Assurance', 'New South Wales');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Nitin', 'Mirchandani', 'Production', 'Victoria');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Diane', 'Margheim', 'Research and Development', 'British Columbia');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Rebecca', 'Laszlo', 'Production', 'Victoria');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Rajesh', 'Patel', 'Production', 'South Australia');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Vidur', 'Luthra', 'Human Resources', 'California');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('John', 'Evans', 'Production', 'California');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Nancy', 'Anderson', 'Production', 'California');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Pilar', 'Ackerman', 'Shipping and Receiving', 'Oregon');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('David', 'Yalovsky', 'Production', 'California');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('David', 'Hamilton', 'Production', 'California');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Laura', 'Steele', 'Production', 'California');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Margie', 'Shoop', 'Production', 'California');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Zainal', 'Arifin', 'Document Control', 'California');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Lorraine', 'Nay', 'Production', 'California');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Fadi', 'Fakhouri', 'Production', 'British Columbia');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Ryan', 'Cornelsen', 'Production', 'Queensland');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Candy', 'Spoon', 'Finance', 'New South Wales');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Nuan', 'Yu', 'Production', 'Victoria');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('William', 'Vong', 'Production Control', 'New South Wales');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Bjorn', 'Rettig', 'Production', 'New South Wales');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Scott', 'Gode', 'Production', 'Victoria');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Michael', 'Rothkugel', 'Production', 'New South Wales');
INSERT INTO [T_Employees] (FirstName,LastName,Department, Location) VALUES ('Lane', 'Sacksteder', 'Production', 'New South Wales');

