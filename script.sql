USE master
GO
IF NOT EXISTS (
    SELECT name
FROM sys.databases
WHERE name = N'TestDb'
)
CREATE DATABASE TestDb
GO

use TestDb;

GO
CREATE SCHEMA TestSchema;
GO

GO
create table TestSchema.Product
(
    ID UNIQUEIDENTIFIER primary key DEFAULT newsequentialid(),
    Name NVARCHAR(255) not null,
    Description NVARCHAR(MAX)
);

create unique nonclustered index IX_Product_Name on TestSchema.Product(Name) with (ALLOW_ROW_LOCKS = on, ALLOW_PAGE_LOCKS =on) ;
GO

GO
create table TestSchema.ProductVersion
(
    ID UNIQUEIDENTIFIER primary key DEFAULT newsequentialid(),
    ProductID UNIQUEIDENTIFIER not null,
    Name NVARCHAR(255) not null,
    Description NVARCHAR(MAX),
    CreatingDate DATE DEFAULT GETDATE(),
    Width REAL not null,
    Height REAL not null,
    Length REAL not null,
    FOREIGN KEY (ProductID)  REFERENCES TestSchema.Product (ID) on delete CASCADE
);

create  nonclustered index IX_ProductVersion_CreatingDate on TestSchema.ProductVersion(CreatingDate) with (ALLOW_ROW_LOCKS = on, ALLOW_PAGE_LOCKS =on);
create  nonclustered index IX_ProductVersion_Width on TestSchema.ProductVersion(Width) with (ALLOW_ROW_LOCKS = on, ALLOW_PAGE_LOCKS =on);
create  nonclustered index IX_ProductVersion_Height on TestSchema.ProductVersion(Height) with (ALLOW_ROW_LOCKS = on, ALLOW_PAGE_LOCKS =on);
create   nonclustered index IX_ProductVersion_Length on TestSchema.ProductVersion(Length) with (ALLOW_ROW_LOCKS = on, ALLOW_PAGE_LOCKS =on);
create  nonclustered index IX_ProductVersion_Name on TestSchema.ProductVersion(Name) with (ALLOW_ROW_LOCKS = on, ALLOW_PAGE_LOCKS =on);
GO

GO
create table TestSchema.EventLog
(
    ID UNIQUEIDENTIFIER primary key DEFAULT newsequentialid(),
    EventDate DATE DEFAULT GETDATE(),
    Description NVARCHAR(MAX)
);
create  nonclustered index IX_EventLog_EventDate on TestSchema.EventLog(EventDate) with (ALLOW_ROW_LOCKS = on, ALLOW_PAGE_LOCKS =on);
GO

GO
create trigger Product_Trigger on TestSchema.Product
after insert, update, delete
as 
insert into TestSchema.EventLog
    (Description)
    select ID
    from inserted
union all
    select ID
    from deleted;
GO

GO
create trigger ProductVersion_Trigger on TestSchema.ProductVersion
after insert, update, delete
as 
insert into TestSchema.EventLog
    (Description)
    select ID
    from inserted
union all
    select ID
    from deleted;
GO

GO
CREATE FUNCTION TestSchema.ProductSearch(
    @search NVARCHAR(255),
    @minSize REAL = -1,
    @maxSize REAL = -1
)
RETURNS table
AS
RETURN (
select v.ID as ID, p.Name, v.Name as ProductVersionName, Height, Length, Width
from TestSchema.Product as p
    join TestSchema.ProductVersion as v on v.ProductID=p.ID
where (@maxSize - @minSize > 0 and Height * Length * Width BETWEEN @minSize and @maxSize)
    or
    (LEN(@search) > 0 and ((LOWER(v.Name) LIKE CONCAT('%',LOWER(@search),'%')) or (LOWER(p.Name) LIKE CONCAT('%',LOWER(@search),'%'))))
);
GO

-- generate data
GO
DECLARE @i int = 0, @v int=0, @n NVARCHAR(6)
WHILE @i < 50
BEGIN
    set @n = CONCAT('name',CAST(@i AS varchar));
    insert into TestSchema.Product
        (Name, Description)
    VALUES(@n, 'description');

    set @v =0;
    WHILE @v < 5
    BEGIN
        INSERT into TestSchema.ProductVersion
            (ProductID, Name, Width,Length,Height)
        VALUES(
                (select ID
                from TestSchema.Product
                where Name=@n),
                CONCAT(@n,' ', left(NEWID(),5)),
                floor(rand()* (100-1 + 1) + 1),
                floor(rand()* (100-1 + 1) + 1),
                floor(rand()* (100-1 + 1) + 1)
);
        set @v = @v + 1
    end
    SET @i = @i + 1;
END
GO