-- UP_SER_MS_STYLE
-- UP_SRO_MS_STYLE
-- UP_SER_W_EMPLOYEES
-- UP_SRO_MS_W_EMPLOYEES
-- UP_SRO_MS_STYLE_ORDER
-- UP_OTH_MS_W_USER_AUTHENTICATION
-- UP_LST_MS_W_PERMISSION
-- UP_LST_MS_W_SECURITY_OPTION_GROUP
-- UP_LST_MS_W_SECURITYNAME
-- UP_GET_TA_W_PAGES_CONTROL_IMAGE
-- UP_SRO_MS_W_EMP_ACCESSLEVEL
-- UP_SRO_MS_W_EMP_ACCESSLEVEL_LOG
-- UP_OTH_MS_W_RESERVE_USERNAME_PASSWORD
-- UP_SER_MS_W_EMP_ACCESSLEVEL_LOG


-- W_G_EMPLOYEES
-- W_G_ACCESSLEVEL
-- W_G_SECURITY_OPTION_GROUP
-- W_G_SECURITY_OPTIONS
-- W_STYLE_ORDERS
-- W_G_ACCESSLEVEL_LOG
-- W_G_EmployeePasswordLog
-- W_STYLE_ORDERS


SELECT * FROM W_G_EMPLOYEES
SELECT * FROM W_STYLE_ORDERS
SELECT * FROM W_STYLE

SELECT * FROM  W_G_SECURITY_OPTION_GROUP
SELECT * FROM W_G_SECURITY_OPTIONS
SELECT * FROM W_G_ACCESSLEVEL

--CREATE TABLE W_STYLE
--(
--	W_StyleId int identity (1,1),
--	BarcodeNo VARCHAR(150),
--	StyleName VARCHAR(50),
--	DesignNo VARCHAR(50),
--	ShadeNo VARCHAR(25),
--	MRP INT
--)

--SELECT * FROM W_STYLE
--INSERT INTO W_STYLE (BarcodeNo ,StyleName ,DesignNo ,ShadeNo ,MRP )
--SELECT 'D-12302021','STYLE1','DESIGN1','SHADENO1',1000
--UNION 
--SELECT 'D-12302022','STYLE2','DESIGN2','SHADENO2',899
--UNION 
--SELECT 'D-12302023','STYLE3','DESIGN3','SHADENO3',199
--UNION 
--SELECT 'D-12302024','STYLE4','DESIGN4','SHADENO4',456
--UNION 
--SELECT 'D-12302025','STYLE5','DESIGN5','SHADENO5',399
--UNION 
--SELECT 'D-12302026','STYLE6','DESIGN6','SHADENO6',499
--UNION 
--SELECT 'D-12302027','STYLE7','DESIGN6','SHADENO7',599


--SELECT * FROM W_G_SECURITY_OPTION_GROUP
--INSERT INTO W_G_SECURITY_OPTION_GROUP (Sec_Group) VALUES ('Setup')

--select * from W_G_SECURITY_OPTIONS
--INSERT INTO W_G_SECURITY_OPTIONS (SecurityOptionSubName,Sec_Group_ID) VALUES ('Style Master',1)
--INSERT INTO W_G_SECURITY_OPTIONS (SecurityOptionSubName,Sec_Group_ID) VALUES ('Style Order',1)


--select * from W_G_EMPLOYEES 
--INSERT INTO W_G_EMPLOYEES (FAX,Cell_Phone,Phone,Login,Email,Employee_Name,IPRestriction,Password,FirstForm,Deleted,ChangePassword,PwdLastChanged,PwdExpire,FirstLoginDone)
--SELECT FAX=NULL,Cell_Phone=NULL,Phone=NULL,Login='Admin',Email='admin@gmail.com',Employee_Name='Administrator',IPRestriction=null,Password='admin',FirstForm=null,Deleted=0,ChangePassword=null,PwdLastChanged=null,PwdExpire=null,FirstLoginDone=null
--union
--SELECT FAX=NULL,Cell_Phone=NULL,Phone=NULL,Login='neeraj',Email='admin@gmail.com',Employee_Name='Neeraj Goswami',IPRestriction=null,Password='test',FirstForm=null,Deleted=0,ChangePassword=null,PwdLastChanged=null,PwdExpire=null,FirstLoginDone=null
--union
--SELECT FAX=NULL,Cell_Phone=NULL,Phone=NULL,Login='ashish',Email='admin@gmail.com',Employee_Name='Ashish Srivastava',IPRestriction=null,Password='test',FirstForm=null,Deleted=0,ChangePassword=null,PwdLastChanged=null,PwdExpire=null,FirstLoginDone=null
--union
--SELECT FAX=NULL,Cell_Phone=NULL,Phone=NULL,Login='amrit',Email='admin@gmail.com',Employee_Name='Amrit Kumar',IPRestriction=null,Password='test',FirstForm=null,Deleted=0,ChangePassword=null,PwdLastChanged=null,PwdExpire=null,FirstLoginDone=null



--create table W_STYLE_ORDERS
--(
--	W_StyleOrderID INT IDENTITY(1,1),
--	W_StyleId INT,
--	Qty INT,
--	Remarks Varchar(500)	
--CONSTRAINT [PK__W_STYLE_ORDERS__08EA5793] PRIMARY KEY NONCLUSTERED 
--(
--	[W_StyleOrderID] ASC
--)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
--) ON [PRIMARY]


SELECT * FROM W_STYLE_ORDERS


