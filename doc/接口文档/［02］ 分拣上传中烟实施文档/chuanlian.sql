CREATE VIEW ThOne
AS
SELECT a.PicklineID AS SORTING_CODE, a.PicklineName AS SORTING_NAME, 
       2 AS SORTING_TYPE,a.IsValid AS ISACTIVE, LEFT(b.ImportTime, 8) 
      AS UPDATE_DATE, 0 AS IS_IMPORT 
FROM  OPENDATASOURCE(
	'SQLOLEDB',
	'DATA SOURCE=192.168.1.63; USER ID=sa;PASSWORD=sa'
	).XTDPS.DBO.BS_PICKLINEINFO a LEFT   JOIN
      OPENDATASOURCE(
	'SQLOLEDB',
	'DATA SOURCE=192.168.1.63; USER ID=sa;PASSWORD=sa'
	).XTDPS.DBO.PI_BORROW_MASTER b ON a.PICKLINEID=b.PICKLINEID

GO

CREATE VIEW Thtwo
AS
SELECT a.PicklineID AS SORTING_CODE, a.PicklineName AS SORTING_NAME, 
       2 AS SORTING_TYPE,a.IsValid AS ISACTIVE, LEFT(b.ImportTime, 8) 
      AS UPDATE_DATE, 0 AS IS_IMPORT 
from
OPENDATASOURCE(
	'SQLOLEDB',
	'DATA SOURCE=192.168.1.156; USER ID=sa;PASSWORD='
	).XTDPS.DBO.BS_PICKLINEINFO  a LEFT   JOIN
OPENDATASOURCE(
	'SQLOLEDB',
	'DATA SOURCE=192.168.1.156; USER ID=sa;PASSWORD='
	).XTDPS.DBO.PI_BORROW_MASTER b ON a.PICKLINEID=b.PICKLINEID
GO
CREATE VIEW dbo.DWV_IDPS_SORTING
AS
SELECT * FROM THONE
UNION
SELECT * FROM THTWO
GO

CREATE VIEW dbo.THONE_STATUS
AS
SELECT right(BorrowID,10) AS SORT_BILL_ID, CompanyID AS ORG_CODE, 
      PicklineID AS SORTING_CODE, LEFT(ImportTime, 8) AS SORT_DATE, 
      SORT_SPEC=(select count(distinct productid) from
OPENDATASOURCE(
	'SQLOLEDB',
	'DATA SOURCE=192.168.1.63; USER ID=sa;PASSWORD=sa'
	).pi_order_detail_pick b left join
OPENDATASOURCE(
	'SQLOLEDB',
	'DATA SOURCE=192.168.1.63; USER ID=sa;PASSWORD=sa'
	).pi_order_master c on  b.orderid=c.orderid where
c.borrowid=a.BorrowID), 
      cast(TotalQuantity as decimal(8,2)) AS SORT_QUANTITY, 
      Receipts AS SORT_ORDER_NUM, left(replace(replace(replace(CONVERT(varchar(30),startdate,120),' ',''),'-',''),':',''),14) 
      AS SORT_BEGIN_DATE, left(replace(replace(replace(CONVERT(varchar(30),enddate,120),' ',''),'-',''),':',''),14) AS SORT_END_DATE, 
      DATEDIFF(n, StartDate, EndDate)*60  AS SORT_COST_TIME, 0 AS IS_IMPORT
FROM OPENDATASOURCE(
	'SQLOLEDB',
	'DATA SOURCE=192.168.1.63; USER ID=sa;PASSWORD=sa'
	).XTDPS.DBO.PI_Borrow_Master as a 
GO

CREATE VIEW dbo.THTWO_STATUS
AS
SELECT right(BorrowID,10) AS SORT_BILL_ID, CompanyID AS ORG_CODE, 
      PicklineID AS SORTING_CODE, LEFT(ImportTime, 8) AS SORT_DATE, 
      SORT_SPEC=(select count(distinct productid) from
OPENDATASOURCE(
	'SQLOLEDB',
	'DATA SOURCE=192.168.1.156; USER ID=sa;PASSWORD=sa'
	).pi_order_detail_pick b left join
OPENDATASOURCE(
	'SQLOLEDB',
	'DATA SOURCE=192.168.1.156; USER ID=sa;PASSWORD=sa'
	).pi_order_master c on  b.orderid=c.orderid where
c.borrowid=a.BorrowID), 
      cast(TotalQuantity as decimal(8,2)) AS SORT_QUANTITY, 
      Receipts AS SORT_ORDER_NUM, left(replace(replace(replace(CONVERT(varchar(30),startdate,120),' ',''),'-',''),':',''),14) 
      AS SORT_BEGIN_DATE, left(replace(replace(replace(CONVERT(varchar(30),enddate,120),' ',''),'-',''),':',''),14) AS SORT_END_DATE, 
      DATEDIFF(n, StartDate, EndDate)*60  AS SORT_COST_TIME, 0 AS IS_IMPORT
FROM OPENDATASOURCE(
	'SQLOLEDB',
	'DATA SOURCE=192.168.1.156; USER ID=sa;PASSWORD=sa'
	).XTDPS.DBO.PI_Borrow_Master as a 

GO
CREATE VIEW dbo.DWV_IORD_SORT_STATUS
AS
SELECT * FROM THONE_STATUS
UNION
SELECT * FROM THTWO_STATUS

GO
SET ANSI_NULLS ON 
GO


