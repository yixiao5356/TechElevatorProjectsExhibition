/****** Script for SelectTopNRows command from SSMS  ******/
USE master
GO

IF DB_ID('final_capstone') IS NOT NULL
BEGIN
	ALTER DATABASE final_capstone SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
	DROP DATABASE final_capstone;
END

CREATE DATABASE final_capstone
GO

USE final_capstone
GO

--create tables
CREATE TABLE users (
	user_id int IDENTITY(1,1) NOT NULL,
	username varchar(50) NOT NULL,
	password_hash varchar(200) NOT NULL,
	salt varchar(200) NOT NULL,
	user_role varchar(50) NOT NULL
	CONSTRAINT PK_user PRIMARY KEY (user_id)
)

--populate default data: 'password'
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('user','Jg45HuwT7PZkfuKTz6IB90CtWY4=','LHxP4Xh7bN0=','user');
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('admin','YhyGVQ+Ch69n4JMBncM4lNF/i9s=', 'Ar/aB2thQTI=','admin');

CREATE TABLE category(
id int IDENTITY (1,1) NOT NULL,
name varchar(100)  NOT NULL,
description varchar(1000),
categoryID int NOT NULL
)

INSERT INTO category (name, description, categoryID) VALUES ('Pathway', 'Pathway refers to anything related to finding or securing a job.', 1);
INSERT INTO category (name, description, categoryID) VALUES ('Curriculum', 'Curriculum refers to anything covered in class at Tech Elevator.', 2);
INSERT INTO category (name, description, categoryID) VALUES ('Quotes', 'Quotes to keep you motivated!', 3);
INSERT INTO category (name, description, categoryID) VALUES ('Positions', 'Any current open positions that we know of.', 4);

CREATE TABLE pathway(
id int IDENTITY (1,1) NOT NULL,
name varchar(100)  NOT NULL,
description varchar(1000),
website varchar(1000),
categoryID int,
weight int DEFAULT 0,
)

CREATE TABLE curriculum(
id int IDENTITY (1,1) NOT NULL,
name varchar(100)  NOT NULL,
description varchar(1000),
website varchar(1000),
categoryID int,
weight int DEFAULT 0,
)

CREATE TABLE quotes(
id int IDENTITY (1,1) NOT NULL,
name varchar(100)  NOT NULL,
description varchar(1000),
categoryID int,
weight int DEFAULT 0,
)

CREATE TABLE positions(
id int IDENTITY (1,1) NOT NULL,
name varchar(100)  NOT NULL,
description varchar(1000),
website varchar(1000),
categoryID int,
weight int DEFAULT 0,
)

CREATE TABLE companies(
id int IDENTITY (1,1) NOT NULL,
name varchar(100)  NOT NULL,
description varchar(1000),
website varchar(1000),
location varchar(100),
number_of_employees varchar(100),
number_of_grads int,
names_of_grads varchar(500),
glassdoor_rating decimal(2,1)
)

CREATE TABLE requests(
id int IDENTITY (1,1) NOT NULL,
name varchar(100)  NOT NULL,
)

ALTER TABLE category
ADD PRIMARY KEY(categoryid)

ALTER TABLE requests
ADD PRIMARY KEY(id)

ALTER TABLE pathway
ADD PRIMARY KEY (id)

ALTER TABLE curriculum
ADD PRIMARY KEY (id)

ALTER TABLE quotes
ADD PRIMARY KEY (id)

ALTER TABLE positions
ADD PRIMARY KEY (id)

ALTER TABLE companies
ADD PRIMARY KEY (id)

ALTER TABLE pathway
ADD FOREIGN KEY (categoryID) REFERENCES category(categoryID)

ALTER TABLE curriculum
ADD FOREIGN KEY (categoryID) REFERENCES category(categoryID)

ALTER TABLE quotes
ADD FOREIGN KEY (categoryID) REFERENCES category(categoryID)

ALTER TABLE positions
ADD FOREIGN KEY (categoryID) REFERENCES category(categoryID)

INSERT INTO pathway(name, description, website, categoryID) VALUES ('Resume', 'Your resume is the first thing a recruiter will see, so it''s critical that you utilize it to get yourself noticed: one page, white space and technical experience at the top. For more information follow this link: ', 'https://docs.google.com/document/d/1Em8-swvCIHBvAi3vdaOU76FGDL-q_lSuYyeYxuOmtVU/edit', 1);
INSERT INTO pathway(name, description, website, categoryID) VALUES ('LinkedIn', 'LinkedIn is the top online site for professional, social and career networking. The site functions as an online directory of individual professionals and organizations, and facilitates the process of professional networking without having to leave your office. For more information follow this link: ',
'https://docs.google.com/document/d/1bQQ9jusneGeh0zJXjNnG5SgVhkSuyXit3cGsP3bm6n0/edit', 1);
INSERT INTO pathway(name, description, website, categoryID) VALUES ('Interview', 'Interviewing can be stressful. Try to relax and think of it as a conversation. Dress to impress, practice STAR and technical questions. Ask me about "STAR questions" or "follow up" for information about those topics; for more information about interviews in general follow this link: ',
'https://www.indeed.com/career-advice/interviewing/how-to-ace-your-next-interview' ,1)
INSERT INTO pathway(name, description, website, categoryID) VALUES ('Star Questions','STAR stands for situation, task, action and result - when asked a STAR question, your answer should describe the situation - what happened? The task - what was required? The action - what did you do? And the result - what was the outcome? For STAR examples, follow this link: ', 'https://docs.google.com/document/d/1FOI7cM88J01UfdgSghaNLYh2-DjIskUYoaYG_XbqDSo/edit' ,1)
INSERT INTO pathway(name, description, website, categoryID) VALUES ('Interview Follow Up','After an interview, make sure to follow up and thank your interviewer(s). Ask for their email at the end of the interview, or reach out to the recruiter and ask for them so that you can send a thank you. People have been passed up for not sending thank you notes. For more information on follow up, follow this link: ', 'https://docs.google.com/document/d/14OBiOmeOQRAcFRGcFrdY_NVuWVQVf0SxeoDyRDVo62E/edit' ,1)
INSERT INTO pathway(name, description, website, categoryID) VALUES ('Elevator Pitch', 'Your elevator pitch is your chance to introduce yourself and give a memorable first impression. It should be around 30 seconds and should answer the following: what do you do (well)? What is your greatest strength? What would you like to do? What’s your why (motivation)? Practicing this (and recording yourself) might be a good idea. For more information on elevator pitches, follow this link: ', 'https://drive.google.com/drive/folders/0B-Xlc61CFPaTZk94c1VfMXlZenM' ,1)
INSERT INTO pathway(name, description, website, categoryID) VALUES ('Imposter Syndrome', 'Imposter syndrome is inevitable, particularly when attending a coding bootcamp. The feeling of not being good enough or feeling like a “fraud” is part of this process - the trick is realizing that everyone deals with it, regardless of experience or skill level. For more information on imposter syndrome, follow this link: ', 'https://www.themuse.com/advice/5-different-types-of-imposter-syndrome-and-5-ways-to-battle-each-one' ,1)
INSERT INTO pathway(name, description, website, categoryID) VALUES ('Side Projects', 'How will I have time to do a side project? You probably won’t. If you do, awesome. But what you need to do, at minimum, is to have an idea for a side project that you can talk about. Ideally utilizing everything that you’ve learned at Tech Elevator. Employers want to know that you’re passionate, and in a perfect world, you would work on a side project. It shows that this is more than just a career for you. For more information, follow this link: ', 'https://docs.google.com/document/d/1LPWSUwohawOe9Y2jMvlEXNYE2CDjZbQvS_v0KEEw4lI/edit' ,1)
INSERT INTO pathway(name, description, website, categoryID) VALUES ('Huntr', 'Huntr is a website that you may be asked to use to track what jobs you’ve applied to - as you begin applying for jobs, you can easily lose track of who you’ve applied to, and you don’t want to apply for the same job twice! ', 'https://huntr.co/' ,1)
INSERT INTO pathway(name, description, website, categoryID) VALUES ('Technical Questions', 'Technical interviews can be scary. You will be asked questions like “what are the pillars of object oriented programming?”, and at some point you will be asked to write code. For questions you will be asked, take notes during class and study before interviews. You can look up popular interview questions, but expect to be asked about it if it’s on your resume. If you don’t know something, it’s better to admit that than trying to fake an answer. For “whiteboarding”, or writing code, there are numerous websites that give you practice “katas” that can help you prepare for this in an interview. For more in depth help with technical interview questions, type “technical interview help please”. For practice coding, try this: ', 'https://www.codewars.com/' ,1)

INSERT INTO curriculum(name, description, website, categoryID) VALUES ('C#','C# (C-Sharp) is a programming language developed by Microsoft that runs on the .NET Framework. Learn more about C# here -', 'https://www.w3schools.com/cs/#:~:text=%E2%9D%AE%20Home%20Next%20%E2%9D%AF,Start%20learning%20C%23%20now%20%C2%BB', 2)
INSERT INTO curriculum(name, description, website, categoryID) VALUES ('Java', 'Java is a general-purpose programming language that is class-based, object-oriented, and designed to have as few implementation dependencies as possible. Learn more about Java here -', 'https://www.w3schools.com/java/default.asp', 2)
INSERT INTO curriculum(name, description, website, categoryID) VALUES ('.NET', '.NET is a free, cross-platform, open source developer platform for building many different types of applications. With .NET, you can use multiple languages, editors, and libraries to build for web, mobile, desktop, games, and IoT. Learn more about .NET here -', 'https://dotnet.microsoft.com/learn/dotnet/what-is-dotnet', 2)
INSERT INTO curriculum(name, description, website, categoryID) VALUES ('SQL', 'SQL is a standard language for storing, manipulating and retrieving data in databases. Learn more about SQL here -', 'https://www.w3schools.com/sql/', 2)
INSERT INTO curriculum(name, description, website, categoryID) VALUES ('HTML', 'HTML, Hypertext Markup Language, is the standard markup language for documents designed to be displayed in a web browser. Learn more about HTML here -', 'https://book.techelevator.com/content/intro-html-css.html', 2)
INSERT INTO curriculum(name, description, website, categoryID) VALUES ('CSS', 'CSS, Cascading Style Sheets, is a style sheet language used for describing the presentation of a document written in a markup language like HTML. Learn more about HTML here -', 'https://book.techelevator.com/content/intro-html-css.html', 2)
INSERT INTO curriculum(name, description, website, categoryID) VALUES ('JavaScript', 'JavaScript is a scripting or programming language that allows you to implement complex features on web pages — every time a web page does more than just sit there and display static information for you to look at — displaying timely content updates, interactive maps, animated 2D/3D graphics, scrolling video jukeboxes, etc. — you can bet that JavaScript is probably involved. Learn more about JavaScript here -', 'https://book.techelevator.com/content/variables-and-datatypes-javascript.html#introduction', 2)
INSERT INTO curriculum(name, description, website, categoryID) VALUES ('Vue.js', 'Vue.js is an open-source model–view–viewmodel JavaScript framework for building user interfaces and single-page applications. Learn more about Vue.js here -', 'https://book.techelevator.com/content/intro-to-vue-and-data-binding.html', 2)
INSERT INTO curriculum(name, description, website, categoryID) VALUES ('OOP', 'OOP stands for Object-Oriented Programming. Procedural programming is about writing procedures or methods that perform operations on the data, while object-oriented programming is about creating objects that contain both data and methods. Learn more about object oriented programming here -', 'https://book.techelevator.com/content/introduction-to-objects-ool.html#objects', 2)
INSERT INTO curriculum(name, description, website, categoryID) VALUES ('Encapsulation', 'Encapsulation is one of the fundamental concepts in object-oriented programming (OOP). It describes the idea of bundling data and methods that work on that data within one unit, e.g., a class in C#. Learn more about Encapsulation here -', 'https://www.w3schools.com/cpp/cpp_encapsulation.asp', 2)
INSERT INTO curriculum(name, description, website, categoryID) VALUES ('Polymorphism', 'Polymorphism is an object-oriented programming concept that refers to the ability of a variable, function or object to take on multiple forms. In a programming language exhibiting polymorphism, class objects belonging to the same hierarchical tree (inherited from a common parent class) may have functions with the same name, but with different behaviors. Learn more about Polymorphism here -', 'https://book.techelevator.com/content/polymorphism-ool.html#what-is-polymorphism', 2)
INSERT INTO curriculum(name, description, website, categoryID) VALUES ('Inheritance', 'Inheritance is one of the core concepts of object-oriented programming (OOP) languages. It is a mechanism where you can derive a class from another class for a hierarchy of classes that share a set of attributes and methods. You can use it to declare different kinds of exceptions, add custom logic to existing frameworks, and even map your domain model to a database. Learn more about Inheritance here -', 'https://book.techelevator.com/content/inheritance-ool.html', 2)
INSERT INTO curriculum(name, description, website, categoryID) VALUES ('Abstraction', 'Abstractions main goal is to handle complexity by hiding unnecessary details from the user. That enables the user to implement more complex logic on top of the provided abstraction without understanding or even thinking about all the hidden complexity. Learn more about Abstraction here -', 'https://www.w3schools.com/cs/cs_abstract.asp', 2)
INSERT INTO curriculum(name, description, website, categoryID) VALUES ('LEFT OUTER JOIN', 'A left outer join returns all the values from an inner join plus all values in the left table that do not match to the right table, including rows with NULL (empty) values in the link column. Learn more about all things joining in SQL here -', 'https://book.techelevator.com/content/sql-joins.html#joins', 2)
INSERT INTO curriculum(name, description, website, categoryID) VALUES ('Array', 'An array is a data structure consisting of a collection of elements (values or variables), each identified by at least one array index or key. Learn more about Arrays (and Loops!) here -', 'https://book.techelevator.com/content/arrays-and-loops-javascript.html#arrays', 2)
INSERT INTO curriculum(name, description, website, categoryID) VALUES ('List', 'A list  is a tool that can be used to store multiple pieces of information at once. It can also be defined as a variable containing multiple other variables. A list consists of a numbers paired with items. Each item can be retrieved by its paired number. Learn more about Lists here -', 'https://book.techelevator.com/content/linear-data-structures-ool.html#list', 2)
INSERT INTO curriculum(name, description, website, categoryID) VALUES ('Stack', 'A stack is an array or list structure of function calls and parameters used in modern computer programming and CPU architecture. Similar to a stack of plates at a buffet restaurant or cafeteria, elements in a stack are added or removed from the top of the stack, in a “last in first, first out” or LIFO order. - Learn more about Stacks here -', 'https://book.techelevator.com/content/linear-data-structures-ool.html#stack', 2)
INSERT INTO curriculum(name, description, website, categoryID) VALUES ('Queue', 'A queue is a first-in first-out (FIFO) abstract data type that is heavily used in computing. Uses for queues involve anything where you want things to happen in the order that they were called, but where the computer can''t keep up to speed. Learn more about Queues here -', 'https://book.techelevator.com/content/linear-data-structures-ool.html#queue', 2)
INSERT INTO curriculum(name, description, website, categoryID) VALUES ('Dictionary', 'A dictionary is a general-purpose data structure for storing a group of objects. A dictionary has a set of keys and each key has a single associated value. Learn more about Dictionaries here -', 'https://book.techelevator.com/content/non-linear-data-structures-ool.html#associative-collections', 2)
INSERT INTO curriculum(name, description, website, categoryID) VALUES ('MVC', 'Model–view–controller (usually known as MVC) is a software design pattern commonly used for developing user interfaces that divides the related program logic into three interconnected elements. Learn more about Model View Controllers here -', 'https://book.techelevator.com/content/server-side-api-1-dotnet.html#introduction-to-mvc', 2)
INSERT INTO curriculum(name, description, website, categoryID) VALUES ('File I/O', 'File I/O, also known as File Input / File Output, refers to Input/Output operations such as open, close, read, write and append, all of which deal with standard disk or tape files. The term would be used to refer to regular file operations in contrast to low-level system I/O such as dealing with virtual memory pages or OS tables of contents. Learn more about File Input / Output here -', 'https://book.techelevator.com/content/file-io-ool.html#reading-files', 2)
INSERT INTO curriculum(name, description, website, categoryID) VALUES ('GET/PUT/POST/DELETE', 'The HTTP verbs Get, Put, Post and Delete correspond to CRUD (Create, read, update, and delete) operations respectively. Get is used to retrieve a representation of a resource and returns XML or JSON and a HTTP response code. Put is utilized mostly for updating, but can be used for creating a resource as long as the ID is not unique or already in use. Post is used to create a new resource. Delete, well, deletes the resource. Learn more about Get here -', 'https://book.techelevator.com/content/web-services-consuming-get.html#using-a-library-to-make-web-api-calls, and more about put, post and delete here - https://book.techelevator.com/content/web-services-consuming-post.html', 2)
INSERT INTO curriculum(name, description, website, categoryID) VALUES ('Overriding', 'Method overriding, in object-oriented programming, is a language feature that allows a subclass or child class to provide a specific implementation of a method that is already provided by one of its superclasses or parent classes. Learn more about Overriding here -', 'https://www.w3schools.com/cs/cs_polymorphism.asp', 2)
INSERT INTO curriculum(name, description, website, categoryID) VALUES ('Overloading', 'Function overloading or method overloading is the ability to create multiple functions of the same name with different implementations. Learn more about Overloading a method here -', 'https://book.techelevator.com/content/introduction-to-classes-ool.html#overloading-methods-and-constructors', 2)
INSERT INTO curriculum(name, description, website, categoryID) VALUES ('Method', 'A method in object-oriented programming (OOP) is a procedure associated with a message and an object. This allows the sending objects to invoke behaviors and to delegate the implementation of those behaviors to the receiving object. Learn more about Methods here -', 'https://book.techelevator.com/content/introduction-to-classes-ool.html#methods', 2)
INSERT INTO curriculum(name, description, website, categoryID) VALUES ('Constructor', 'In class-based object-oriented programming, a constructor (abbreviation: ctor) is a special type of subroutine called to create an object. It prepares the new object for use, often accepting arguments that the constructor uses to set required member variables. Learn more about Constructors here -', 'https://book.techelevator.com/content/introduction-to-classes-ool.html#constructors', 2)
INSERT INTO curriculum(name, description, website, categoryID) VALUES ('Class', 'In object-oriented programming, a class is an extensible program-code-template for creating objects, providing initial values for state (member variables) and implementations of behavior (member functions or methods). Learn more about Classes here -', 'https://book.techelevator.com/content/introduction-to-classes-ool.html#classes', 2)
INSERT INTO curriculum(name, description, website, categoryID) VALUES ('Try/Catch', 'When executing C# code, different errors can occur: coding errors made by the programmer, errors due to wrong input, or other unforeseeable things. When an error occurs, C# will normally stop and generate an error message. The technical term for this is: C# will throw an exception (throw an error).The try statement allows you to define a block of code to be tested for errors while it is being executed.The catch statement allows you to define a block of code to be executed, if an error occurs in the try block. Learn more about Try / Catch Loops here -', 'https://book.techelevator.com/content/exception-handling-ool.html#try-catch', 2)
INSERT INTO curriculum(name, description, website, categoryID) VALUES ('CLR', 'Common Language Runtime (CLR) manages the execution of . NET programs. The just-in-time compiler converts the compiled code into machine instructions. This is what the computer executes. The services provided by CLR include memory management, exception handling, type safety, etc. Learn more about Common Language Runtime here -', 'https://www.c-sharpcorner.com/UploadFile/9582c9/what-is-common-language-runtime-in-C-Sharp/', 2)
INSERT INTO curriculum(name, description, website, categoryID) VALUES ('DOM', 'The DOM, or Document Object Model, is a cross-platform and language-independent interface that treats an XML or HTML document as a tree structure wherein each node is an object representing a part of the document. The DOM represents a document with a logical tree. Learn more about the DOM here -', 'https://book.techelevator.com/content/dom-api-javascript.html#what-is-the-dom', 2)
INSERT INTO curriculum(name, description, website, categoryID) VALUES ('XML', 'XML stands for eXtensible Markup Language and is used for storing and transporting data. XML is more secure than JSON and supports various encoding formats. Learn more about XML here -', 'https://www.w3schools.com/xml/xml_whatis.asp', 2)
INSERT INTO curriculum(name, description, website, categoryID) VALUES ('JSON', 'JSON stands for JavaScript Object Notation and is a lightweight format for storing and transporting data. It is the ‘’common currency’’ used by most applications. Learn more about JSON here -', 'https://www.w3schools.com/whatis/whatis_json.asp', 2)
INSERT INTO curriculum(name, description, website, categoryID) VALUES ('Interface', 'Interfaces contain definitions for a group of related functionalities (methods and/or properties) that a non-abstract class or struct must implement (think contract). Learn more about interfaces here -', 'https://www.w3schools.com/cs/cs_interface.asp or here - https://www.w3schools.com/java/java_interface.asp', 2)
INSERT INTO curriculum(name, description, website, categoryID) VALUES ('DAO', 'A data access object is an object or interface that provides access to an underlying database, used to separate the access to the database from business services. Learn more about data access objects here -', 'https://en.wikipedia.org/wiki/Data_access_object', 2)
INSERT INTO curriculum(name, description, website, categoryID) VALUES ('ASP.NET', 'ASP.NET is a set of Web development tools offered by Microsoft. Programs like Visual Studio .NET and Visual Web Developer allow Web developers to create dynamic websites using a visual interface. Learn more about ASP.NET here -', 'https://dotnet.microsoft.com/learn/aspnet/what-is-aspnet', 2)
INSERT INTO curriculum(name, description, website, categoryID) VALUES ('Object', 'An object is an instance of a class (a class is like a blueprint and an object is like a house built from that blueprint). Learn more about objects here -', 'https://book.techelevator.com/content/introduction-to-objects-ool.html#what-is-an-object', 2)

INSERT INTO quotes(name, description, categoryID) VALUES('William W. Purkey','“You’ve gotta dance like there’s nobody watching, love like you’ll never be hurt, sing like there’s nobody listening, and live like it’s heaven on earth.” - William W. Purkey',3)
INSERT INTO quotes(name, description, categoryID) VALUES('Neil Gaiman','“Fairy tales are more than true: not because they tell us that dragons exist, but because they tell us that dragons can be beaten.” - Neil Gaiman',3)
INSERT INTO quotes(name, description, categoryID) VALUES('Pablo Picasso','“Everything you can imagine is real.” - Pablo Picasso',3)
INSERT INTO quotes(name, description, categoryID) VALUES('Helen Keller','“When one door of happiness closes, another opens; but often we look so long at the closed door that we do not see the one which has been opened for us.” - Helen Keller',3)
INSERT INTO quotes(name, description, categoryID) VALUES('Eleanor Roosevelt','“Do one thing every day that scares you.” - Eleanor Roosevelt',3)
INSERT INTO quotes(name, description, categoryID) VALUES('Lewis Carroll','“It''s no use going back to yesterday, because I was a different person then. - Lewis Carroll',3)
INSERT INTO quotes(name, description, categoryID) VALUES('Socrates','“Smart people learn from everything and everyone, average people from their experiences, stupid people already have all the answers.” – Socrates',3)
INSERT INTO quotes(name, description, categoryID) VALUES('Eleanor Roosevelt','“Do what you feel in your heart to be right – for you’ll be criticized anyway.” - Eleanor Roosevelt',3)
INSERT INTO quotes(name, description, categoryID) VALUES('Dalai Lama XIV','“Happiness is not something ready made. It comes from your own actions.” - Dalai Lama XIV',3)
INSERT INTO quotes(name, description, categoryID) VALUES('Abraham Lincoln','“Whatever you are, be a good one.” - Abraham Lincoln',3)
INSERT INTO quotes(name, description, categoryID) VALUES('Unknown','“The same boiling water that softens the potato hardens the egg. It’s what you’re made of. Not the circumstances.” – Unknown',3)
INSERT INTO quotes(name, description, categoryID) VALUES('Catherine Pulsifier','“If we have the attitude that it’s going to be a great day it usually is.” – Catherine Pulsifier',3)
INSERT INTO quotes(name, description, categoryID) VALUES('Unknown','“You can either experience the pain of discipline or the pain of regret. The choice is yours.” – Unknown',3)
INSERT INTO quotes(name, description, categoryID) VALUES('Johann Wolfgang Von Goethe','“Magic is believing in yourself. If you can make that happen, you can make anything happen.” – Johann Wolfgang Von Goethe',3)
INSERT INTO quotes(name, description, categoryID) VALUES('Elon Musk','“If something is important enough, even if the odds are stacked against you, you should still do it.” – Elon Musk',3)
INSERT INTO quotes(name, description, categoryID) VALUES('Unknown','“Hold the vision, trust the process.” – Unknown',3)
INSERT INTO quotes(name, description, categoryID) VALUES('John D. Rockefeller','“Don’t be afraid to give up the good to go for the great.” – John D. Rockefeller',3)
INSERT INTO quotes(name, description, categoryID) VALUES('Unknown','“People who wonder if the glass is half empty or full miss the point. The glass is refillable.” – Unknown',3)
INSERT INTO quotes(name, description, categoryID) VALUES('Jaymin Shah','“No one is to blame for your future situation but yourself. If you want to be successful, then become “Successful.” - Jaymin Shah',3)
INSERT INTO quotes(name, description, categoryID) VALUES('Abraham Lincoln','“Things may come to those who wait, but only the things left by those who hustle.” - Abraham Lincoln',3)
INSERT INTO quotes(name, description, categoryID) VALUES('Thomas Edison','“Everything comes to him who hustles while he waits.” - Thomas Edison',3)
INSERT INTO quotes(name, description, categoryID) VALUES('K’wan','“Every sucessful person in the world is a hustler one way or another. We all hustle to get where we need to be. Only a fool would sit around and wait on another man to feed him.” - K’wan',3)
INSERT INTO quotes(name, description, categoryID) VALUES('Unknown','“Invest in your dreams. Grind now. Shine later.” – Unknown',3)
INSERT INTO quotes(name, description, categoryID) VALUES('Unknown','“Hustlers don’t sleep, they nap.” – Unknown',3)
INSERT INTO quotes(name, description, categoryID) VALUES('Ross Simmonds','“Greatness only comes before hustle in the dictionary.” – Ross Simmonds',3)
INSERT INTO quotes(name, description, categoryID) VALUES('Gary Vaynerchuk','“Without hustle, talent will only carry you so far.” – Gary Vaynerchuk',3)
INSERT INTO quotes(name, description, categoryID) VALUES('Mark Cuban','“Work like there is someone working twenty four hours a day to take it away from you.” – Mark Cuban',3)
INSERT INTO quotes(name, description, categoryID) VALUES('Unknown','“Hustle in silence and let your success make the noise.” – Unknown',3)
INSERT INTO quotes(name, description, categoryID) VALUES('Aly Raisman','“The hard days are what make you stronger.” – Aly Raisman',3)
INSERT INTO quotes(name, description, categoryID) VALUES('Wayne Dyer','“If you believe it’ll work out, you’ll see opportunities. If you don’t believe it’ll work out, you’ll see obstacles.” – Wayne Dyer',3)
INSERT INTO quotes(name, description, categoryID) VALUES('Theodore Roosevelt','Keep your eyes on the stars, and your feet on the ground.” – Theodore Roosevelt',3)
INSERT INTO quotes(name, description, categoryID) VALUES('Shonda Rhimes','“You can waste your lives drawing lines. Or you can live your life crossing them.” – Shonda Rhimes',3)
INSERT INTO quotes(name, description, categoryID) VALUES('George Lorimer','“You’ve got to get up every morning with determination if you’re going to go to bed with satisfaction.” – George Lorimer',3)
INSERT INTO quotes(name, description, categoryID) VALUES('Michelle Obama','“I now tried a new hypothesis: It was possible that I was more in charge of my happiness than I was allowing myself to be.” – Michelle Obama',3)
INSERT INTO quotes(name, description, categoryID) VALUES('Mahatma Gandhi','“In a gentle way, you can shake the world.” – Mahatma Gandhi',3)
INSERT INTO quotes(name, description, categoryID) VALUES('Kurt Cobain','“If opportunity doesn’t knock, build a door.” – Kurt Cobain',3)
INSERT INTO quotes(name, description, categoryID) VALUES('Roy T. Bennett','“Don’t be pushed around by the fears in your mind. Be led by the dreams in your heart.” – Roy T. Bennett',3)
INSERT INTO quotes(name, description, categoryID) VALUES('Frank Ocean','“Work hard in silence, let your success be the noise.” – Frank Ocean',3)
INSERT INTO quotes(name, description, categoryID) VALUES('H. Jackson Brown Jr.','“Don’t say you don’t have enough time. You have exactly the same number of hours per day that were given to Helen Keller, Pasteur, Michelangelo, Mother Teresa, Leonardo Da Vinci, Thomas Jefferson, and Albert Einstein.” – H. Jackson Brown Jr.',3)
INSERT INTO quotes(name, description, categoryID) VALUES('Tim Notke','“Hard work beats talent when talent doesn’t work hard.” – Tim Notke',3)

INSERT INTO quotes(name, description, categoryID) VALUES('Thomas Edison','“Opportunity is missed by most people because it is dressed in overalls and looks like work.” – Thomas Edison',3)
INSERT INTO quotes(name, description, categoryID) VALUES('Jimmy Johnson','“The only difference between ordinary and extraordinary is that little extra.” – Jimmy Johnson',3)
INSERT INTO quotes(name, description, categoryID) VALUES('Oscar Wilde','“The best way to appreciate your job is to imagine yourself without one.” – Oscar Wilde',3)
INSERT INTO quotes(name, description, categoryID) VALUES('Benjamin Hardy','“Unsuccessful people make their decisions based on their current situations. Successful people make their decisions based on where they want to be.” – Benjamin Hardy',3)
INSERT INTO quotes(name, description, categoryID) VALUES('Kamari aka Lyrikal','“Never stop doing your best just because someone doesn’t give you credit.” – Kamari aka Lyrikal',3)
INSERT INTO quotes(name, description, categoryID) VALUES('Leah LaBelle','“Work hard for what you want because it won’t come to you without a fight. You have to be strong and courageous and know that you can do anything you put your mind to. If somebody puts you down or criticizes you, just keep on believing in yourself and turn it into something positive.” – Leah LaBelle',3)
INSERT INTO quotes(name, description, categoryID) VALUES('Conan O’Brien','“Work hard, be kind, and amazing things will happen.” – Conan O’Brien',3)
INSERT INTO quotes(name, description, categoryID) VALUES('John Wooden','“Do the best you can. No one can do more than that.” – John Wooden',3)
INSERT INTO quotes(name, description, categoryID) VALUES('Theodore Roosevelt','“Do what you can, with what you have, where you are.” – Theodore Roosevelt',3)
INSERT INTO quotes(name, description, categoryID) VALUES('George Eliot','‘It’s never too late to be what you might’ve been.” – George Eliot',3)
INSERT INTO quotes(name, description, categoryID) VALUES('Walt Disney','“If you can dream it, you can do it.” – Walt Disney',3)
INSERT INTO quotes(name, description, categoryID) VALUES('Baz Luhrmann','“Trust yourself that you can do it and get it.” – Baz Luhrmann',3)
INSERT INTO quotes(name, description, categoryID) VALUES('Unknown','“Don’t let what you can’t do interfere with what you can do.” – Unknown',3)
INSERT INTO quotes(name, description, categoryID) VALUES('Benjamin Franklin','“You can do anything you set your mind to.” – Benjamin Franklin',3)
INSERT INTO quotes(name, description, categoryID) VALUES('David Axelrod','“All we can do is the best we can do.” – David Axelrod',3)
INSERT INTO quotes(name, description, categoryID) VALUES('William Cobbett','“You never know what you can do until you try.” – William Cobbett',3)
INSERT INTO quotes(name, description, categoryID) VALUES('Mark Twain','“Twenty years from now you’ll be more disappointed by the things you did not do than the ones you did.” – Mark Twain',3)

INSERT INTO positions(name, description, website, categoryID) VALUES('Software Developer', 'Software Developer', 'https://apply.workable.com/olive/j/FB0509EB70/', 4)
INSERT INTO positions(name, description, website, categoryID) VALUES('Software Engineer', 'Software Engineer', 'https://www.joinroot.com/careers/ade4a1fc-3480-4520-8038-318988b28ff3/?lever-source=Indeed | 
https://coastalridge.com/careers/?gnk=job&gni=8a7887a872e8b16c0173245c66f45e80&gns=LinkedIn |
https://jpmc.fa.oraclecloud.com/hcmUI/CandidateExperience/en/sites/CX_1001/job/210013457/?utm_medium=jobshare&src=LinkedIn_Slots |
https://neuvoo.com/view/?id=42af466ac348&source=linkedin&utm_source=partner&utm_medium=linkedin&puid=fddcaddebadfaadaaddccddaadda7adfbaddfddd3deccdc8fec3ddcg3e&splitab=1&action=emailAlert', 4)
INSERT INTO positions(name, description, website, categoryID) VALUES('Full Stack Developer', 'Full Stack Developer', 'https://jpmc.fa.oraclecloud.com/hcmUI/CandidateExperience/en/sites/CX_1001/job/210014133/?utm_medium=jobshare&src=LinkedIn_Slots |
https://www.linkedin.com/jobs/view/1945989398/?alternateChannel=search', 4)
INSERT INTO positions(name, description, website, categoryID) VALUES('SQL Developer', 'SQL Developer', 'https://www.ziprecruiter.com/jobs/firebrand-technologies-daa245d6/sql-developer-remote-d00df59e?tsid=122001595&utm_campaign=1595 |
https://www.linkedin.com/jobs/view/1910370881/?alternateChannel=search |
https://www.linkedin.com/jobs/view/1967808627/?alternateChannel=search', 4)
INSERT INTO positions(name, description, website, categoryID) VALUES('QA Automation Engineer', 'QA Automation Engineer', 'https://www.linkedin.com/jobs/view/1972046985/?alternateChannel=search |
https://www.linkedin.com/jobs/view/1976501795/?alternateChannel=search', 4)
INSERT INTO positions(name, description, website, categoryID) VALUES('Programmer', 'Programmer', 'https://www.linkedin.com/jobs/view/1911134549/?alternateChannel=search |
https://www.linkedin.com/jobs/view/1953573665/?alternateChannel=search |
https://huntington.wd5.myworkdayjobs.com/en-US/HNBcareers/job/Columbus-OH/Programmer-Analyst-Senior_R0011948?source=LinkedIn_Corporate_Page', 4)
INSERT INTO positions(name, description, website, categoryID) VALUES('Test Automation Developer', 'Test Automation Developer', 'https://www.linkedin.com/jobs/view/1976988162/?alternateChannel=search |
https://www.linkedin.com/jobs/view/1687811033/?alternateChannel=search', 4)
INSERT INTO positions(name, description, website, categoryID) VALUES('BI Developer', 'BI Developer', 'https://www.linkedin.com/jobs/view/1732723901/?alternateChannel=search |
https://www.linkedin.com/jobs/view/1974585566/?alternateChannel=search |
https://uscareers-medpace.icims.com/jobs/4303/microsoft-business-intelligence-developer/job?mode=job&iis=Job+Board&iisn=LinkedIn&mobile=false&width=1200&height=500&bga=true&needsRedirect=false&jan1offset=-300&jun1offset=-240', 4)
INSERT INTO positions(name, description, website, categoryID) VALUES('.NET Developer', '.NET Developer', 'https://www.linkedin.com/jobs/view/1974749451/?alternateChannel=search |
https://www.linkedin.com/jobs/view/1907257321/?alternateChannel=search |
https://www.linkedin.com/jobs/view/1976087161/?alternateChannel=search', 4)
INSERT INTO positions(name, description, website, categoryID) VALUES('Java Developer', 'Java Developer', 'https://www.linkedin.com/jobs/view/1940173787/?alternateChannel=search |
https://www.linkedin.com/jobs/view/1973148596/?alternateChannel=search |
https://www.linkedin.com/jobs/view/1957620924/?alternateChannel=search', 4)
INSERT INTO positions(name, description, website, categoryID) VALUES('Google', 'Google', 'https://www.google.com/search?sxsrf=ALeKk03psba78DPtPrA4vQC5twKTAJlDsw%3A1596571300968&source=hp&ei=pL4pX9WuOMKqtQbZm6b4BQ&q=software+developer+jobs+near+me&oq=software+developer+job&gs_lcp=CgZwc3ktYWIQAxgDMgUIABCxAzICCAAyAggAMgUIABCxAzICCAAyAggAMgIIADICCAAyAggAMgIIADoECCMQJzoFCAAQkQI6BwgAEBQQhwI6BQguELEDOgoIABCxAxAUEIcCOggILhDHARCvAToICAAQsQMQgwE6BwgjELECECc6BAgAEApQngRY-zNghFFoAnAAeACAAdIBiAHBEpIBBjE2LjYuMZgBAKABAaoBB2d3cy13aXo&sclient=psy-ab', 4)

INSERT INTO companies(name, description, website, location, number_of_employees, number_of_grads, names_of_grads, glassdoor_rating) VALUES ('JPMorgan Chase & Co', 'JPMorgan Chase & Co. is an American multinational investment bank and financial services holding company. J.P. Morgan helps businesses, markets and communities grow and develop in more than 100 countries. Through our Corporate and Investment Bank, we provide banking, markets and investor services, treasury services and more for the world''s most important corporations, governments and institutions. Our Asset and Wealth Management business provides global market insights and a range of investment capabilities for individuals and families, advisors and institutions.','https://www.jpmorganchase.com/','Multiple Locations Worldwide', '10000+',5,'Bob Smith, Michelle Jones, James Roberts, Mary Smith, Patricia Williams', 3.9)
INSERT INTO companies(name, description, website, location, number_of_employees, number_of_grads, names_of_grads, glassdoor_rating) VALUES ('Nationwide','Nationwide is one of America’s most diversified financial services organizations. We’re trusted advisers for our members, offering solutions like home and auto insurance, life insurance, retirement savings tools, business insurance and pet insurance.','https://www.nationwide.com/','Multiple Locations Worldwide', '34000+',7,'Jocelynn Hammond,Olive Pugh,Omari Haley,Douglas Merritt,Davon Knox,Kaitlin Robertson,Lila Booker',3.6)
INSERT INTO companies(name, description, website, location, number_of_employees, number_of_grads, names_of_grads, glassdoor_rating) VALUES ('L Brands','Our brands are world-renowned; they are household names. Through Victoria''s Secret, PINK and Bath & Body Works, L Brands is an international company that sells lingerie, personal care and beauty products, apparel and accessories. The company operates more than 3,000 company-owned specialty stores in the United States, Canada, the United Kingdom, Ireland and Greater China, and its brands are sold in more than 800 franchised locations worldwide.','https://www.lb.com/','Multiple Locations Worldwide','10000+',2,'Margert Amos, Chi Senter',3.8)
INSERT INTO companies(name, description, website, location, number_of_employees, number_of_grads, names_of_grads, glassdoor_rating) VALUES ('Bold Penguin','Bold Penguin is where technology meets the human touch in commercial insurance. With a heavy focus on agents, Bold Penguin pushes the boundaries of user experience for businesses, is an easy tool for agents, and offers a streamlined process of underwriting for carriers. Founded by a group of entrepreneurs who spent their early days working with Allstate, Nationwide, Progressive, regional carriers and established insurance agencies.','https://www.boldpenguin.com/', 'Columbus, OH','51-200',3,'Iris Hoisington,Tristan Amaro,Dalton Ardis',3.6)
INSERT INTO companies(name, description, website, location, number_of_employees, number_of_grads, names_of_grads, glassdoor_rating) VALUES ('Root Insurance','Root insurance is the first licensed insurance carrier powered entirely by mobile. We were founded on the belief that the services you need for everyday life should serve you better. We started by tackling the archaic car insurance industry. Unlike traditional insurance companies that base rates on demographics, we use data and technology to base rates primarily on how people drive. That means with Root, better drivers pay less.','https://www.joinroot.com/','Columbus, OH','501-1000',6,'Maynard Viers,Joaquin Darby,Karol Shinkle,Janella Greenberg,Rachele Kucharski,Yu Lumpkin',3.8)
INSERT INTO companies(name, description, website, location, number_of_employees, number_of_grads, names_of_grads, glassdoor_rating) VALUES ('Accenture','Growing customer expectations. Market-Shaping AI. Self-Optimizing systems. The post-digital age shows no signs of slow down, and the need for the new ideas powered by intelligent technologies has never been greater.','https://www.accenture.com/us-en','Multiple Locations Worldwide','10000+',5, 'Cristi Heinz,Carolynn Santamaria,Jaime Farrow,Charlette Wehner,Tessa Hartsoe',3.9)
INSERT INTO companies(name, description, website, location, number_of_employees, number_of_grads, names_of_grads, glassdoor_rating) VALUES ('Huntington National Bank','Huntington Bancshares Incorporated is a regional bank holding company headquartered in Columbus, Ohio, with $106 billion of assets and a network of 970 branches and 1,860 ATMs across eight Midwestern states. Founded in 1866, The Huntington National Bank and its affiliates provide consumer, small business, commercial, treasury management, wealth management, brokerage, trust, and insurance services. Huntington also provides auto dealer, equipment finance, national settlement and capital market services that extend beyond its core states. Visit huntington.com for more information.','https://www.huntington.com/','Multiple States in the Midwest','10000+',1,'Veronica Galvan',3.5)
INSERT INTO companies(name, description, website, location, number_of_employees, number_of_grads, names_of_grads, glassdoor_rating) VALUES ('Olive','Olive builds artificial intelligence and RPA solutions that empower healthcare organizations to improve efficiency and patient care while reducing costly administrative errors.','https://oliveai.com/','Columbus, OH','201 - 500',4,'Emmy Hankin,Sherryl Mcneese,Elliott Ewing,Foster Cornish',3.7)
INSERT INTO companies(name, description, website, location, number_of_employees, number_of_grads, names_of_grads, glassdoor_rating) VALUES ('CapTech','CapTech is a team of master builders, creators, and problem solvers who help clients grow efficient, successful businesses. We unite diverse skills and perspectives to transform how data, systems, and ingenuity enable each client to advance what’s possible in a changing world.','https://www.captechconsulting.com/','Multiple Locations in the United States','1001 - 5000',3,'Bart Bond,Ashley Becerril,Lucy Coonrod',3.9)
INSERT INTO companies(name, description, website, location, number_of_employees, number_of_grads, names_of_grads, glassdoor_rating) VALUES ('Veeva Systems', 'Veeva Systems Inc. is the leader in cloud-based software for the global life sciences industry. Committed to innovation, product excellence, and customer success, Veeva serves more than 700 customers, ranging from the world’s largest pharmaceutical companies to emerging biotechs. Veeva is headquartered in the San Francisco Bay Area, with offices throughout North America, Europe, Asia, and Latin America. For more information, visit veeva.com.','https://www.veeva.com/','Multiple Locations Worldwide','1001-5000',4,'Mckenzie Zelaya,Felix Burgener,Jon Tietjen,Samuel Xavier',3.6)

GO
