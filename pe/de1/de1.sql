USE [LibraryDB]
GO
/****** Object:  Table [dbo].[Authors]    Script Date: 3/15/2025 10:35:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Authors](
	[AuthorID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Bio] [text] NULL,
PRIMARY KEY CLUSTERED 
(
	[AuthorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BookAuthors]    Script Date: 3/15/2025 10:35:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BookAuthors](
	[BookID] [int] NOT NULL,
	[AuthorID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[BookID] ASC,
	[AuthorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BookCopies]    Script Date: 3/15/2025 10:35:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BookCopies](
	[CopyID] [int] IDENTITY(1,1) NOT NULL,
	[BookID] [int] NULL,
	[ISBN] [nvarchar](20) NULL,
	[EditionNumber] [int] NULL,
	[PrintYear] [int] NULL,
	[Condition] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[CopyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Books]    Script Date: 3/15/2025 10:35:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Books](
	[BookID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](255) NOT NULL,
	[Publisher] [nvarchar](255) NULL,
	[PublicationYear] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[BookID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BorrowTransaction]    Script Date: 3/15/2025 10:35:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BorrowTransaction](
	[TransactionID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[CopyID] [int] NULL,
	[BorrowDate] [date] NOT NULL,
	[ReturnDate] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[TransactionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 3/15/2025 10:35:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](255) NOT NULL,
	[Email] [nvarchar](255) NOT NULL,
	[MembershipDate] [date] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Authors] ON 

INSERT [dbo].[Authors] ([AuthorID], [Name], [Bio]) VALUES (1, N'J.K. Rowling', N'Tác gi? c?a lo?t truy?n Harry Potter.')
INSERT [dbo].[Authors] ([AuthorID], [Name], [Bio]) VALUES (2, N'George Orwell', N'Tác gi? c?a 1984 và Animal Farm.')
INSERT [dbo].[Authors] ([AuthorID], [Name], [Bio]) VALUES (3, N'J.R.R. Tolkien', N'Tác gi? c?a The Lord of the Rings.')
SET IDENTITY_INSERT [dbo].[Authors] OFF
GO
INSERT [dbo].[BookAuthors] ([BookID], [AuthorID]) VALUES (1, 1)
INSERT [dbo].[BookAuthors] ([BookID], [AuthorID]) VALUES (2, 2)
INSERT [dbo].[BookAuthors] ([BookID], [AuthorID]) VALUES (3, 3)
GO
SET IDENTITY_INSERT [dbo].[BookCopies] ON 

INSERT [dbo].[BookCopies] ([CopyID], [BookID], [ISBN], [EditionNumber], [PrintYear], [Condition]) VALUES (1, 1, N'978-0747532699', 1, 1997, N'New')
INSERT [dbo].[BookCopies] ([CopyID], [BookID], [ISBN], [EditionNumber], [PrintYear], [Condition]) VALUES (2, 1, N'978-0439554930', 2, 1998, N'Good')
INSERT [dbo].[BookCopies] ([CopyID], [BookID], [ISBN], [EditionNumber], [PrintYear], [Condition]) VALUES (3, 2, N'978-0451524935', 1, 1950, N'Old')
INSERT [dbo].[BookCopies] ([CopyID], [BookID], [ISBN], [EditionNumber], [PrintYear], [Condition]) VALUES (4, 3, N'978-0547928227', 3, 2012, N'New')
SET IDENTITY_INSERT [dbo].[BookCopies] OFF
GO
SET IDENTITY_INSERT [dbo].[Books] ON 

INSERT [dbo].[Books] ([BookID], [Title], [Publisher], [PublicationYear]) VALUES (1, N'Harry Potter and the Sorcerer''s Stone', N'Bloomsbury', 1997)
INSERT [dbo].[Books] ([BookID], [Title], [Publisher], [PublicationYear]) VALUES (2, N'1984', N'Secker & Warburg', 1949)
INSERT [dbo].[Books] ([BookID], [Title], [Publisher], [PublicationYear]) VALUES (3, N'The Hobbit', N'George Allen & Unwin', 1937)
SET IDENTITY_INSERT [dbo].[Books] OFF
GO
SET IDENTITY_INSERT [dbo].[BorrowTransaction] ON 

INSERT [dbo].[BorrowTransaction] ([TransactionID], [UserID], [CopyID], [BorrowDate], [ReturnDate]) VALUES (1, 1, 1, CAST(N'2024-03-01' AS Date), CAST(N'2024-03-10' AS Date))
INSERT [dbo].[BorrowTransaction] ([TransactionID], [UserID], [CopyID], [BorrowDate], [ReturnDate]) VALUES (2, 2, 2, CAST(N'2024-03-05' AS Date), NULL)
INSERT [dbo].[BorrowTransaction] ([TransactionID], [UserID], [CopyID], [BorrowDate], [ReturnDate]) VALUES (3, 3, 3, CAST(N'2024-03-07' AS Date), CAST(N'2024-03-15' AS Date))
SET IDENTITY_INSERT [dbo].[BorrowTransaction] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([UserID], [FullName], [Email], [MembershipDate]) VALUES (1, N'Nguyen Van A', N'nguyenvana@example.com', CAST(N'2022-01-15' AS Date))
INSERT [dbo].[Users] ([UserID], [FullName], [Email], [MembershipDate]) VALUES (2, N'Tran Thi B', N'tranthib@example.com', CAST(N'2023-02-20' AS Date))
INSERT [dbo].[Users] ([UserID], [FullName], [Email], [MembershipDate]) VALUES (3, N'Le Van C', N'levanc@example.com', CAST(N'2021-11-10' AS Date))
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Users__A9D1053452445E70]    Script Date: 3/15/2025 10:35:05 PM ******/
ALTER TABLE [dbo].[Users] ADD UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[BookAuthors]  WITH CHECK ADD FOREIGN KEY([AuthorID])
REFERENCES [dbo].[Authors] ([AuthorID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BookAuthors]  WITH CHECK ADD FOREIGN KEY([BookID])
REFERENCES [dbo].[Books] ([BookID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BookCopies]  WITH CHECK ADD FOREIGN KEY([BookID])
REFERENCES [dbo].[Books] ([BookID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BorrowTransaction]  WITH CHECK ADD FOREIGN KEY([CopyID])
REFERENCES [dbo].[BookCopies] ([CopyID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BorrowTransaction]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
ON DELETE CASCADE
GO
