create database Etiqa
go

use Etiqa
go


create table [User]
(
	Id bigint IDENTITY(1,1) primary key,
	Username varchar(20) NOT NULL unique,
	Email varchar(100) NOT NULL unique,
	PhoneNo varchar(20) NOT NULL,
	Hobby varchar(100)
)

create table [UserSkill]
(
	Id bigint IDENTITY(1,1) primary key,
	UserId bigint NOT NULL,
	Skill varchar(100)
)


alter table UserSkill add constraint FK_UserId_UserId foreign key ([UserId]) references [User](Id)