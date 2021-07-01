-- we don't know how to generate schema dbo (class Schema) :(
create table users
(
	userId int identity(10001, 1)
		primary key,
	userName nvarchar(60) not null,
	password varchar(32) not null
)
go

create table questionnaires
(
	qnId int identity
		primary key,
	userId int not null
		constraint fk_questionnaires_users_1
			references users,
	title nvarchar(256) not null,
	description nvarchar(512),
	released char(1) default 'N',
	createTime datetime2,
	lastEditTime datetime
)
go

create table questions
(
	qId int identity
		primary key,
	qnId int not null
		constraint fk_questions_questionnaires_1
			references questionnaires
				on delete cascade,
	question nvarchar(256) not null,
	type int not null,
	required char(1) not null
)
go

create table results
(
	resultId int identity
		primary key,
	qnId int not null
		constraint fk_results_questionnaires_1
			references questionnaires
				on delete cascade,
	investigatorIp varchar(16),
	replyTime datetime not null
)
go

create table resultItems
(
	rpId decimal(20) identity
		primary key,
	qId int not null
		constraint fk_resultItems_questions_1
			references questions
				on delete cascade,
	resultId int not null,
	content nvarchar(512)
)
go

create table options
(
	oId int identity
		primary key,
	optionContent nvarchar(256) not null,
	qId int not null
		constraint options_questions_qId_fk
			references questions
				on delete cascade
)
go

create unique index options_oId_uindex
	on options (oId)
go


