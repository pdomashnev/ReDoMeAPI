/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2008                    */
/* Created on:     20.06.2020 11:25:51                          */
/*==============================================================*/


if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Barber') and o.name = 'FK_BARBER_WORKS_SALON')
alter table Barber
   drop constraint FK_BARBER_WORKS_SALON
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Offer') and o.name = 'FK_OFFER_OFFERM_BARBER')
alter table Offer
   drop constraint FK_OFFER_OFFERM_BARBER
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Offer') and o.name = 'FK_OFFER_OFFERS_SALON')
alter table Offer
   drop constraint FK_OFFER_OFFERS_SALON
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Offer') and o.name = 'FK_OFFER_OFFERSFOR_REQUEST')
alter table Offer
   drop constraint FK_OFFER_OFFERSFOR_REQUEST
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Photo') and o.name = 'FK_PHOTO_CLIENTPHO_REQUEST')
alter table Photo
   drop constraint FK_PHOTO_CLIENTPHO_REQUEST
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Photo') and o.name = 'FK_PHOTO_OFFERSAMP_OFFER')
alter table Photo
   drop constraint FK_PHOTO_OFFERSAMP_OFFER
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Photo') and o.name = 'FK_PHOTO_PORTFOLIO_BARBER')
alter table Photo
   drop constraint FK_PHOTO_PORTFOLIO_BARBER
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Photo') and o.name = 'FK_PHOTO_PORTFOLIO_SALON')
alter table Photo
   drop constraint FK_PHOTO_PORTFOLIO_SALON
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('Barber')
            and   name  = 'Works_FK'
            and   indid > 0
            and   indid < 255)
   drop index Barber.Works_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Barber')
            and   type = 'U')
   drop table Barber
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('Offer')
            and   name  = 'OfferS_FK'
            and   indid > 0
            and   indid < 255)
   drop index Offer.OfferS_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('Offer')
            and   name  = 'OfferM_FK'
            and   indid > 0
            and   indid < 255)
   drop index Offer.OfferM_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('Offer')
            and   name  = 'OffersForReq_FK'
            and   indid > 0
            and   indid < 255)
   drop index Offer.OffersForReq_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Offer')
            and   type = 'U')
   drop table Offer
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('Photo')
            and   name  = 'PortfolioS_FK'
            and   indid > 0
            and   indid < 255)
   drop index Photo.PortfolioS_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('Photo')
            and   name  = 'ClientPhotos_FK'
            and   indid > 0
            and   indid < 255)
   drop index Photo.ClientPhotos_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('Photo')
            and   name  = 'OfferSamples_FK'
            and   indid > 0
            and   indid < 255)
   drop index Photo.OfferSamples_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('Photo')
            and   name  = 'PortfolioM_FK'
            and   indid > 0
            and   indid < 255)
   drop index Photo.PortfolioM_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Photo')
            and   type = 'U')
   drop table Photo
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Request')
            and   type = 'U')
   drop table Request
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Salon')
            and   type = 'U')
   drop table Salon
go

/*==============================================================*/
/* Table: Barber                                                */
/*==============================================================*/
create table Barber (
   Bar_VK_ID            varchar(30)          not null,
   Sal_ID               int                  null,
   Bar_Name             varchar(50)          not null,
   Bar_Spec             varchar(100)         not null,
   Bar_City             varchar(30)          not null,
   Bar_Address          varchar(100)         null,
   Bar_Phone            varchar(20)          null,
   Bar_About            varchar(200)         null,
   Bar_Certs            varchar(200)         null,
   Bar_Raiting          int                  not null,
   constraint PK_BARBER primary key nonclustered (Bar_VK_ID)
)
go

/*==============================================================*/
/* Index: Works_FK                                              */
/*==============================================================*/
create index Works_FK on Barber (
Sal_ID ASC
)
go

/*==============================================================*/
/* Table: Offer                                                 */
/*==============================================================*/
create table Offer (
   Offer_ID             bigint               identity,
   Bar_VK_ID            varchar(30)          not null,
   Sal_ID               int                  null,
   Req_ID               bigint               not null,
   Offer_Cost           float                not null,
   Offer_ForDate        datetime             not null,
   Offer_Selected       bit                  not null default 0,
   Offer_Comment        varchar(1000)        null,
   constraint PK_OFFER primary key nonclustered (Offer_ID)
)
go

/*==============================================================*/
/* Index: OffersForReq_FK                                       */
/*==============================================================*/
create index OffersForReq_FK on Offer (
Req_ID ASC
)
go

/*==============================================================*/
/* Index: OfferM_FK                                             */
/*==============================================================*/
create index OfferM_FK on Offer (
Bar_VK_ID ASC
)
go

/*==============================================================*/
/* Index: OfferS_FK                                             */
/*==============================================================*/
create index OfferS_FK on Offer (
Sal_ID ASC
)
go

/*==============================================================*/
/* Table: Photo                                                 */
/*==============================================================*/
create table Photo (
   Photo_ID             bigint               identity,
   Req_ID               bigint               null,
   Bar_VK_ID            varchar(30)          null,
   Offer_ID             bigint               null,
   Sal_ID               int                  null,
   Photo_Goal           smallint             not null,
   Photo_VK_Link        varchar(1000)        null,
   Photo_Content        varchar(MAX)         null,
   Photo_Comment        varchar(1000)        null,
   constraint PK_PHOTO primary key nonclustered (Photo_ID)
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '1 - Портфолио салона
2 - Портфолио мастера
3 - Клиент
4 - Желание клиента
5 - Предложение
6 - Фото после',
   'user', @CurrentUser, 'table', 'Photo', 'column', 'Photo_Goal'
go

/*==============================================================*/
/* Index: PortfolioM_FK                                         */
/*==============================================================*/
create index PortfolioM_FK on Photo (
Bar_VK_ID ASC
)
go

/*==============================================================*/
/* Index: OfferSamples_FK                                       */
/*==============================================================*/
create index OfferSamples_FK on Photo (
Offer_ID ASC
)
go

/*==============================================================*/
/* Index: ClientPhotos_FK                                       */
/*==============================================================*/
create index ClientPhotos_FK on Photo (
Req_ID ASC
)
go

/*==============================================================*/
/* Index: PortfolioS_FK                                         */
/*==============================================================*/
create index PortfolioS_FK on Photo (
Sal_ID ASC
)
go

/*==============================================================*/
/* Table: Request                                               */
/*==============================================================*/
create table Request (
   Req_ID               bigint               identity,
   Req_VK_ID            varchar(30)          not null,
   Req_ClientName       varchar(50)          not null,
   Req_City             varchar(30)          not null,
   Req_Type             smallint             not null,
   Req_Status           smallint             not null,
   Work_Score           smallint             null,
   Req_Comment          varchar(1000)        null,
   constraint PK_REQUEST primary key nonclustered (Req_ID)
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '1 - только по моему фото
2 - по моему и желаемому фото
3 - хочу как звезда',
   'user', @CurrentUser, 'table', 'Request', 'column', 'Req_Type'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '1 - Новая
2 - На исполнении
3 - Завершено',
   'user', @CurrentUser, 'table', 'Request', 'column', 'Req_Status'
go

/*==============================================================*/
/* Table: Salon                                                 */
/*==============================================================*/
create table Salon (
   Sal_ID               int                  identity,
   Sal_VK_ID            varchar(30)          null,
   Sal_Name             varchar(100)         not null,
   Sal_City             varchar(30)          not null,
   Sal_Address          varchar(100)         null,
   Sal_Phone            varchar(20)          not null,
   Sal_Raiting          int                  not null,
   constraint PK_SALON primary key nonclustered (Sal_ID)
)
go

alter table Barber
   add constraint FK_BARBER_WORKS_SALON foreign key (Sal_ID)
      references Salon (Sal_ID)
go

alter table Offer
   add constraint FK_OFFER_OFFERM_BARBER foreign key (Bar_VK_ID)
      references Barber (Bar_VK_ID)
go

alter table Offer
   add constraint FK_OFFER_OFFERS_SALON foreign key (Sal_ID)
      references Salon (Sal_ID)
go

alter table Offer
   add constraint FK_OFFER_OFFERSFOR_REQUEST foreign key (Req_ID)
      references Request (Req_ID)
go

alter table Photo
   add constraint FK_PHOTO_CLIENTPHO_REQUEST foreign key (Req_ID)
      references Request (Req_ID)
go

alter table Photo
   add constraint FK_PHOTO_OFFERSAMP_OFFER foreign key (Offer_ID)
      references Offer (Offer_ID)
go

alter table Photo
   add constraint FK_PHOTO_PORTFOLIO_BARBER foreign key (Bar_VK_ID)
      references Barber (Bar_VK_ID)
go

alter table Photo
   add constraint FK_PHOTO_PORTFOLIO_SALON foreign key (Sal_ID)
      references Salon (Sal_ID)
go

