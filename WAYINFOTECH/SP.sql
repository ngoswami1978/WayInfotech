USE [AAMS_TEST]
GO

/****** Object:  StoredProcedure [dbo].[UP_OTH_MS_W_USER_CHANGE_PASSWORD]    Script Date: 10/11/2011 15:17:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[UP_OTH_MS_W_USER_CHANGE_PASSWORD]    
@EMPLOYEEID INT=NULL,    
@USER_OLD_PASSWORD VARCHAR(100)=NULL,    
@USER_NEW_PASSWORD VARCHAR(100)=NULL,    
@RETURNID INT=-1 OUTPUT    
AS    
BEGIN    
  IF(Exists(SELECT * FROM W_G_EMPLOYEES WHERE EMPLOYEEID=@EMPLOYEEID AND W_G_EMPLOYEES.PASSWORD=@USER_OLD_PASSWORD AND ISNULL(Deleted,0)=0))    
   Begin       
    IF @USER_OLD_PASSWORD =  @USER_NEW_PASSWORD  
    BEGIN  
      Select @RETURNID = -100  
      RETURN @RETURNID     
    END  
    ELSE  
    BEGIN  
      UPDATE W_G_EMPLOYEES SET   
      PASSWORD=@USER_NEW_PASSWORD,
      PWDLASTCHANGED = GETDATE()  
      WHERE EMPLOYEEID=@EMPLOYEEID     
      
      SELECT @RETURNID = @@ROWCOUNT  
        
      if @@ROWCOUNT >0 
          BEGIN
                   Insert into  W_G_EmployeePasswordLog (EmployeeId,OldPassword,NewPassword )
                    Values(@EMPLOYEEID,@USER_OLD_PASSWORD,@USER_NEW_PASSWORD)
         END     
      
      RETURN @RETURNID
    END  
   End    
  else    
  BEGIN    
    Select @RETURNID = -1    
    RETURN @RETURNID     
  END    
END    
    
  
  
GO

/****** Object:  StoredProcedure [dbo].[UP_SER_MS_W_EMP_ACCESSLEVEL_LOG]    Script Date: 10/11/2011 15:18:00 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


  
      
      
CREATE PROCEDURE [dbo].[UP_SER_MS_W_EMP_ACCESSLEVEL_LOG]      
@ACTION CHAR(1),      
@EMPLOYEEID INT,      
---PAGING SORTING      
@PAGE_NO INT=NULL,              
@PAGE_SIZE INT=NULL,              
@SORT_BY VARCHAR(100) = NULL,              
@DESC BIT=0,              
@TOTALROWS BIGINT=0 OUTPUT              
---END HERE    
AS       
    
IF @ACTION='S'    
 BEGIN    
--SET NOCOUNT ON              
 DECLARE @TEMPPAGING TABLE               
 (           
  ROWNUMBER INT IDENTITY(1,1),    
SEC_GROUP VARCHAR(100),  
SEC_GROUP_ID BIGINT,      
SECURITYOPTIONSUBNAME VARCHAR(100),  
SECURITYOPTIONID BIGINT ,    
LOGDATE varchar(100),    
CHANGEDDATA VARCHAR(1000),      
   [USER] VARCHAR(100)  
     
  )              
      
 INSERT INTO @TEMPPAGING (SEC_GROUP,SEC_GROUP_ID,SECURITYOPTIONSUBNAME,SECURITYOPTIONID,LOGDATE,CHANGEDDATA,[USER])          
   SELECT TGS.SEC_GROUP,TG.SEC_GROUP_ID ,TG.SECURITYOPTIONSUBNAME,TG.SECURITYOPTIONID ,'LOGDATE'=[dbo].FN_FORMAT_DATETIME(DATE,1),CHANGEDDATA,'USER'=EMPLOYEE_NAME    
    FROM W_G_ACCESSLEVEL_LOG TGL WITH(NOLOCK)    
   INNER JOIN W_G_SECURITY_OPTIONS TG WITH(NOLOCK)  ON TG.SECURITYOPTIONID=TGL.SECURITYOPTIONID    
   LEFT OUTER JOIN W_G_SECURITY_OPTION_GROUP TGS WITH(NOLOCK) ON TGS.SEC_GROUP_ID=TG.SEC_GROUP_ID          
   INNER JOIN W_G_EMPLOYEES TE WITH(NOLOCK) ON TE.EMPLOYEEID=TGL.MODIFYBY    
   WHERE TGL.EMPLOYEEID=@EMPLOYEEID  ORDER BY   
  -- ORDER BY TG.SEC_GROUP_ID, SECURITYOPTIONSUBNAME    
  
------------------------------------DESC ORDER---------------------------------------------------      
CASE WHEN @DESC=1 THEN               
   CASE WHEN @SORT_BY = 'SEC_GROUP' THEN  TGS.SEC_GROUP  END               
   END DESC,    
  
CASE WHEN @DESC=1 THEN               
   CASE WHEN @SORT_BY = 'SECURITYOPTIONSUBNAME' THEN  TG.SECURITYOPTIONSUBNAME  END               
   END DESC,      
  
CASE WHEN @DESC=1 THEN               
   CASE WHEN @SORT_BY = 'USER' THEN  TE.EMPLOYEE_NAME  END               
   END DESC,              
                 
  CASE WHEN @DESC=1 THEN               
   CASE WHEN @SORT_BY = 'LOGDATE' THEN  TGL.[DATE]  END               
   END DESC,              
         
  CASE WHEN @DESC=1 THEN               
   CASE WHEN @SORT_BY = 'CHANGEDDATA' THEN  TGL.CHANGEDDATA  END               
   END DESC,              
 -----------------------------------ASC ORDER-----------------------------------------------------      
CASE WHEN @DESC=0 THEN               
   CASE WHEN @SORT_BY = 'SEC_GROUP' THEN  TGS.SEC_GROUP  END               
  END ASC,   
  
CASE WHEN @DESC=0 THEN               
   CASE WHEN @SORT_BY = 'SECURITYOPTIONSUBNAME' THEN  TG.SECURITYOPTIONSUBNAME  END               
  END ASC,   
  
  CASE WHEN @DESC=0 THEN               
   CASE WHEN @SORT_BY = 'USER' THEN  TE.EMPLOYEE_NAME  END               
  END ASC,              
               
  CASE WHEN @DESC=0 THEN               
   CASE WHEN @SORT_BY = 'LOGDATE' THEN  TGL.[DATE]  END               
  END ASC,              
      
  CASE WHEN @DESC=0 THEN               
   CASE WHEN @SORT_BY = 'CHANGEDDATA' THEN  TGL.CHANGEDDATA  END               
  END ASC              
        
  SET @TOTALROWS=@@ROWCOUNT         
      
   IF NOT (ISNULL(@PAGE_NO,0)=0 AND ISNULL(@PAGE_SIZE,0)=0)                
    BEGIN              
   SELECT * FROM @TEMPPAGING WHERE ROWNUMBER BETWEEN ((@PAGE_NO-1) * @PAGE_SIZE) + 1  AND (@PAGE_NO*@PAGE_SIZE)               
    END              
  ELSE              
    BEGIN              
   SELECT * FROM @TEMPPAGING               
    END              
 END    
     
      
      
      
      
      
      
      
      
      
    
      
      
      
    



GO

/****** Object:  StoredProcedure [dbo].[UP_OTH_MS_W_RESERVE_USERNAME_PASSWORD]    Script Date: 10/11/2011 15:18:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 

CREATE PROC [dbo].[UP_OTH_MS_W_RESERVE_USERNAME_PASSWORD]
AS
BEGIN
	SELECT FIELD_VALUE='PASSWORD'
END




 
GO

/****** Object:  StoredProcedure [dbo].[UP_SRO_MS_W_EMP_ACCESSLEVEL_LOG]    Script Date: 10/11/2011 15:18:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


    
    
    
    
    
    
CREATE PROCEDURE [dbo].[UP_SRO_MS_W_EMP_ACCESSLEVEL_LOG]    
@ACTION CHAR(1),    
@EMPLOYEEID INT=NULL,    
@MODIFYBY INT=NULL,    
@SECURITYOPTIONID INT=NULL,    
@CHANGEDDATA VARCHAR(1000)=NULL,  
@RETUNID INT=NULL OUTPUT    
AS     
 IF @ACTION='I'    
 BEGIN    
      
    INSERT W_G_ACCESSLEVEL_LOG(EMPLOYEEID,MODIFYBY,SECURITYOPTIONID,[DATE],CHANGEDDATA)    
    VALUES(@EMPLOYEEID,@MODIFYBY,@SECURITYOPTIONID,GETDATE(),@CHANGEDDATA)    
        
    SET @RETUNID=@@ROWCOUNT    
    RETURN @RETUNID    
      
 END    
    

    
    
    
    
    
    
    
    
    
  
    
    
    
    
    
    
    



GO

/****** Object:  StoredProcedure [dbo].[UP_SRO_MS_W_EMP_ACCESSLEVEL]    Script Date: 10/11/2011 15:18:04 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[UP_SRO_MS_W_EMP_ACCESSLEVEL]
@ACTION CHAR(1),
@EMPLOYEEID INT=NULL,
@SECURITYOPTIONID INT=NULL,
@VALUE INT=NULL,
@RETUNID INT=NULL OUTPUT
AS	
	IF @ACTION='I'
	BEGIN
		IF NOT EXISTS (SELECT * FROM W_G_ACCESSLEVEL WHERE EMPLOYEEID=@EMPLOYEEID AND SECURITYOPTIONID=@SECURITYOPTIONID)
			BEGIN 
				INSERT W_G_ACCESSLEVEL(EMPLOYEEID,SECURITYOPTIONID,[VALUE])
				VALUES(@EMPLOYEEID,@SECURITYOPTIONID,@VALUE)
				
				SET @RETUNID=@@ROWCOUNT
				RETURN @RETUNID
			END 
		ELSE
			BEGIN 
					SET @RETUNID=0				
					RETURN @RETUNID
			END
	END

	IF @ACTION='D'
	BEGIN
			DELETE W_G_ACCESSLEVEL
			WHERE EMPLOYEEID=@EMPLOYEEID
			SET @RETUNID=@@ROWCOUNT
			RETURN @RETUNID
	END
	IF @ACTION='S'
	BEGIN
			SELECT SECURITYOPTIONID,[VALUE] FROM W_G_ACCESSLEVEL
			WHERE EMPLOYEEID=@EMPLOYEEID
			Order by SECURITYOPTIONID
	END
























GO

/****** Object:  StoredProcedure [dbo].[UP_GET_TA_W_PAGES_CONTROL_IMAGE]    Script Date: 10/11/2011 15:18:04 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


    
CREATE PROCEDURE [dbo].[UP_GET_TA_W_PAGES_CONTROL_IMAGE]
@SecurityOptionID INT=null

AS      
 
SELECT SecurityOptionID,[Image]	  From W_G_SECURITY_OPTIONS WITH (NOLOCK)     WHERE  SecurityOptionID =@SecurityOptionID
--SELECT SecurityOptionID = IMAGEID ,[Image]=IMAGEDATA From Image_Store WITH (NOLOCK) WHERE  IMAGEID =@SecurityOptionID




  


GO

/****** Object:  StoredProcedure [dbo].[UP_LST_MS_W_SECURITYNAME]    Script Date: 10/11/2011 15:18:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


      
CREATE PROCEDURE [dbo].[UP_LST_MS_W_SECURITYNAME]      
@SEC_GROUP_ID INT

AS       
	SELECT  SECURITYOPTIONID,SECURITYOPTIONSUBNAME  FROM  W_G_SECURITY_OPTIONS WHERE SEC_GROUP_ID=@SEC_GROUP_ID

GO

/****** Object:  StoredProcedure [dbo].[UP_LST_MS_W_SECURITY_OPTION_GROUP]    Script Date: 10/11/2011 15:18:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[UP_LST_MS_W_SECURITY_OPTION_GROUP]
AS       
BEGIN
	SELECT Sec_Group_ID,Sec_Group FROM W_G_SECURITY_OPTION_GROUP
	order by  Sec_Group
END






















GO

/****** Object:  StoredProcedure [dbo].[UP_LST_MS_W_PERMISSION]    Script Date: 10/11/2011 15:18:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




        
CREATE PROCEDURE [dbo].[UP_LST_MS_W_PERMISSION]
As       
select TGS.Sec_Group,TG.Sec_Group_Id ,TG.SecurityOptionSubName,TG.securityOptionID   
from W_G_SECURITY_OPTIONS TG WITH(NOLOCK)
LEFT OUTER JOIN W_G_SECURITY_OPTION_GROUP TGS WITH(NOLOCK)
on TGs.Sec_Group_Id=TG.Sec_Group_Id   
order by  TG.Sec_Group_Id,SecurityOptionSubName













GO

/****** Object:  StoredProcedure [dbo].[UP_OTH_MS_W_USER_AUTHENTICATION]    Script Date: 10/11/2011 15:18:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

       
   
          
CREATE PROCEDURE [dbo].[UP_OTH_MS_W_USER_AUTHENTICATION]
@USER_LOGIN VARCHAR(25),                                
@USER_PASSWORD VARCHAR(100)=NULL,                                
@IPADDRESS VARCHAR(15)=NULL
AS                 
 DECLARE @STRADMINUSER AS VARCHAR(40)           
 DECLARE @EMPLOYEEID INT          
 DECLARE @CHECKIPRESTRICTION BIT           
 DECLARE @LOGINSUCCESS BIT          
          
 DECLARE @LASTPASSWORDLOGINCHANGEDATE AS INT          
 DECLARE @PASSWORDEXPIRE AS BIT           
 DECLARE @FORCETOCHANGEPASSWORD AS BIT          
 DECLARE @PASSWORDDURATIONCONGIF AS bigint          
 DECLARE @Ismanager bit
      
       
 DECLARE @PASSWORDDURATION AS INT          
 DECLARE @REQUEST AS INT           
          
 SET @LOGINSUCCESS=0          
         


   IF EXISTS (SELECT * FROM W_G_EMPLOYEES WHERE LOGIN=@USER_LOGIN AND W_G_EMPLOYEES.PASSWORD=@USER_PASSWORD AND ISNULL(Deleted,0)=0)           
    BEGIN          
     SET @LOGINSUCCESS=1          
     print @LOGINSUCCESS
    END
           
           
 IF @LOGINSUCCESS=1              
  BEGIN          
          
	SELECT @STRADMINUSER=FIELD_VALUE FROM T_G_CONFIG WHERE FIELD_NAME='ADMIN_USER' AND FIELD_VALUE=@USER_LOGIN AND ISNULL(ACTIVE,0)=1          
	
		
    SELECT @EMPLOYEEID=EMPLOYEEID FROM W_G_EMPLOYEES           
    WHERE LOGIN=@USER_LOGIN AND W_G_EMPLOYEES.PASSWORD=@USER_PASSWORD
    AND ISNULL(Deleted,0)=0                      
    
              
    SELECT 
    CASE WHEN @STRADMINUSER IS NULL THEN '0' ELSE '1' END AS 'Administrator',
    EMPLOYEEID,
    Email,LOGIN,
    EMPLOYEE_NAME,
    'ChangePassword'=isnull(ChangePassword,0)
    FROM W_G_EMPLOYEES               
    WHERE LOGIN=@USER_LOGIN  AND W_G_EMPLOYEES.PASSWORD=@USER_PASSWORD          
    AND ISNULL(Deleted,0)=0          
          
    SELECT @EMPLOYEEID=EMPLOYEEID FROM W_G_EMPLOYEES           
    WHERE LOGIN=@USER_LOGIN AND W_G_EMPLOYEES.PASSWORD=@USER_PASSWORD          
    AND ISNULL(Deleted,0)=0          
          
    SELECT A.SECURITYOPTIONID,A.SECURITYOPTIONSUBNAME,ISNULL(B.VALUE,0) AS VALUE          
    FROM W_G_SECURITY_OPTIONS A          
    LEFT OUTER JOIN (SELECT SECURITYOPTIONID,[VALUE] FROM W_G_ACCESSLEVEL WHERE EMPLOYEEID=@EMPLOYEEID ) AS B          
    ON A.SECURITYOPTIONID=B.SECURITYOPTIONID          
    ORDER BY A.SECURITYOPTIONID          
END          
          
          
          

          
          
          



  
  
  
  
















USE [AAMS_TEST]
GO
/****** Object:  StoredProcedure [dbo].[UP_SER_W_EMPLOYEES]    Script Date: 10/14/2011 15:57:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
ALTER PROCEDURE [dbo].[UP_SER_W_EMPLOYEES]  
@EMPLOYEE_NAME VARCHAR(40)=NULL,  
@PhoneNo varchar(50)=NULL,  
@Email varchar(50)=NULL,  
@PAGE_NO INT=NULL,  
@PAGE_SIZE INT=NULL,  
@SORT_BY VARCHAR(100) = NULL,  
@DESC BIT=0,  
@INC CHAR(1) = NULL,  
@TOTALROWS BIGINT=0 OUTPUT  
AS      
BEGIN  
  
 DECLARE @TEMPPAGING TABLE   
 (  
  ROWNUMBER INT IDENTITY(1,1),  
  EMPLOYEEID INT,    
  EMPLOYEE_NAME VARCHAR(70),  
  LOGIN CHAR(20),  
  IPRESTRICTION BIT,  
  CELL_PHONE VARCHAR(30),  
  Email VARCHAR(100)  
 )   
  BEGIN  
    INSERT INTO @TEMPPAGING  
    (EMPLOYEEID,EMPLOYEE_NAME,LOGIN,IPRESTRICTION,CELL_PHONE,Email)  
    SELECT E.EMPLOYEEID,E.EMPLOYEE_NAME,[LOGIN],ISNULL(IPRESTRICTION,0),E.CELL_PHONE,EMAIL  
    FROM  W_G_EMPLOYEES  E WITH (NOLOCK)  
    WHERE 
    (@EMPLOYEE_NAME IS NULL OR EMPLOYEE_NAME LIKE '%' + @EMPLOYEE_NAME + '%')  
    AND (@PhoneNo IS NULL OR CELL_PHONE=@PhoneNo)  
    AND (@Email IS NULL OR Email LIKE '%' + @Email + '%')  
    
    AND ISNULL (DELETED,0)=0      
    ORDER BY   
      
    CASE WHEN @DESC=1 THEN   
     CASE WHEN @SORT_BY = 'EMPLOYEE_NAME' THEN  EMPLOYEE_NAME END   
    END DESC,      
    CASE WHEN @DESC=1 THEN   
     CASE WHEN @SORT_BY = 'LOGIN' THEN  LOGIN END   
    END DESC,  
    CASE WHEN @DESC=1 THEN   
     CASE WHEN @SORT_BY = 'CELL_PHONE' THEN  CELL_PHONE END   
    END DESC,  
    CASE WHEN @DESC=1 THEN   
     CASE WHEN @SORT_BY = 'Email' THEN  Email END   
    END DESC,          
    
    CASE WHEN @DESC=0 THEN   
     CASE WHEN @SORT_BY = 'EMPLOYEE_NAME' THEN  EMPLOYEE_NAME END   
    END ASC,      
    CASE WHEN @DESC=0 THEN   
     CASE WHEN @SORT_BY = 'LOGIN' THEN  LOGIN END   
    END ASC,  
    CASE WHEN @DESC=0 THEN   
     CASE WHEN @SORT_BY = 'CELL_PHONE' THEN  CELL_PHONE END   
    END ASC,  
    CASE WHEN @DESC=0 THEN   
     CASE WHEN @SORT_BY = 'Email' THEN  Email END   
    END ASC  ,
    CASE WHEN @DESC=0 THEN   
     CASE WHEN @SORT_BY IS NULL THEN EMPLOYEE_NAME  END   
    END ASC  
    
  END  
  
  SET @TOTALROWS=@@ROWCOUNT  
  IF NOT (ISNULL(@PAGE_NO,0)=0 AND ISNULL(@PAGE_SIZE,0)=0)    
   BEGIN  
    SELECT * FROM @TEMPPAGING WHERE  
    ROWNUMBER BETWEEN ((@PAGE_NO-1) * @PAGE_SIZE) + 1  
    AND (@PAGE_NO*@PAGE_SIZE)   
   END  
  ELSE  
   BEGIN  
    SELECT * FROM @TEMPPAGING   
   END  
END  
   

 
 
GO

/****** Object:  StoredProcedure [dbo].[UP_SRO_MS_W_EMPLOYEES]    Script Date: 10/11/2011 15:18:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[UP_SRO_MS_W_EMPLOYEES]  
@ACTION     CHAR(1),  
@EMPLOYEEID    INT=NULL,  
@CELL_PHONE    VARCHAR(30)=NULL,  
@LOGIN     CHAR(20)=NULL,  
@PASSWORD    VARCHAR(300)=NULL,  
@EMAIL     VARCHAR(100)=NULL,  
@EMPLOYEE_NAME   CHAR(40)=NULL,  
@FIRSTFORM    VARCHAR(100)=NULL,  
@CHANGEPASSWORD   INT=NULL,  
@PWDEXPIRE    INT=NULL,  
@IPRESTRICTION   BIT=NULL,  
@IPADDRESS    VARCHAR(20)=NULL, --Address  
@RETUNID    INT = NULL OUTPUT  
  
AS  
 IF @ACTION='I'  
 BEGIN  
  IF NOT EXISTS (SELECT * FROM  W_G_EMPLOYEES  WHERE [LOGIN]=@LOGIN AND [LOGIN]<>'')  
  BEGIN   
   INSERT W_G_EMPLOYEES(CELL_PHONE,[LOGIN],EMAIL,EMPLOYEE_NAME,PASSWORD,FIRSTFORM,CHANGEPASSWORD , PWDEXPIRE ,IPRESTRICTION)  
   VALUES(@CELL_PHONE,@LOGIN,@EMAIL,@EMPLOYEE_NAME,@PASSWORD,@FIRSTFORM,@CHANGEPASSWORD ,@PWDEXPIRE ,@IPRESTRICTION)  
   SET @RETUNID=@@IDENTITY  
     
   RETURN @RETUNID  
  END   
  BEGIN   
    SET @RETUNID=-1     
    RETURN @RETUNID  
  END  
    
 END  
  
 IF @ACTION='U'  
 BEGIN  
  IF NOT EXISTS (SELECT * FROM  W_G_EMPLOYEES WHERE [LOGIN]=@LOGIN AND [LOGIN]<>'' AND EMPLOYEEID<>@EMPLOYEEID)  
  BEGIN   
   UPDATE W_G_EMPLOYEES  
   SET   
    CELL_PHONE=@CELL_PHONE,      
    PASSWORD=@PASSWORD,  
    [LOGIN]=@LOGIN,  
    EMAIL=@EMAIL,  
    EMPLOYEE_NAME=@EMPLOYEE_NAME,  
    FIRSTFORM=@FIRSTFORM,  
    ChangePassword  = @ChangePassword,  
    PwdExpire = @PwdExpire,  
    IPRESTRICTION=@IPRESTRICTION      
   WHERE EMPLOYEEID=@EMPLOYEEID     
   SET @RETUNID=@@rowcount  
     
   RETURN @RETUNID  
  END  
  BEGIN  
   SET @RETUNID=-1     
   RETURN @RETUNID  
  END  
 END  
  
 IF @ACTION='D'  
 BEGIN  
  UPDATE W_G_EMPLOYEES  SET DELETED=1  WHERE EMPLOYEEID=@EMPLOYEEID  
 END  
  
 IF @ACTION='S'  
 BEGIN  
  SELECT   
  E.EMPLOYEEID,'CELL_PHONE'=ISNULL(CELL_PHONE,''),[LOGIN],'EMAIL'=ISNULL(EMAIL,''),EMPLOYEE_NAME,IPRESTRICTION,PASSWORD,FIRSTFORM,ChangePassword,PwdExpire   
  FROM W_G_EMPLOYEES E WITH(NOLOCK)    
  WHERE E.EMPLOYEEID=@EMPLOYEEID  
 END  
  

GO


USE [AAMS_TEST]
GO

/****** Object:  StoredProcedure [dbo].[UP_SRO_MS_STYLE_ORDER]    Script Date: 10/11/2011 15:18:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[UP_SRO_MS_STYLE_ORDER]
@ACTION CHAR(1),
@W_StyleOrderID INT= NULL,
@W_StyleId INT= NULL,
@BarcodeNo VARCHAR(150) = NULL,
@StyleName VARCHAR(50) = NULL,
@DesignNo VARCHAR(50) = NULL,
@ShadeNo VARCHAR(25) = NULL,
@MRP INT = NULL,
@QTY INT = NULL,
@Remarks varchar(1000),
@RETURNID INT =NULL OUTPUT
AS
	IF @ACTION='I'
	BEGIN		
		INSERT INTO W_STYLE_ORDERS (Qty ,Remarks,W_StyleId,LOGDATE)
		VALUES (@QTY,@Remarks,@W_StyleId,GETDATE())
		SET @RETURNID=@@IDENTITY
		RETURN @RETURNID
	END
	
	IF @ACTION='U'
	BEGIN
		UPDATE W_STYLE_ORDERS	SET 
		Qty = @QTY ,
		Remarks=@Remarks,
		W_StyleId =@W_StyleId
		where W_StyleOrderID=@W_StyleOrderID				
		SET @RETURNID = @@ROWCOUNT
	END
	
	IF @ACTION='D'
	BEGIN
		DELETE W_STYLE_ORDERS  where W_StyleOrderID=@W_StyleOrderID
	END
	
	IF @ACTION='S'
	BEGIN
		SELECT * FROM W_STYLE_ORDERS where W_StyleOrderID=@W_StyleOrderID
		SET @RETURNID = @@ROWCOUNT
	END













GO

/****** Object:  StoredProcedure [dbo].[UP_SRO_MS_STYLE]    Script Date: 10/11/2011 15:18:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[UP_SRO_MS_STYLE]
@ACTION CHAR(1),
@W_StyleId INT= NULL,
@BarcodeNo VARCHAR(150) = NULL,
@StyleName VARCHAR(50) = NULL,
@DesignNo VARCHAR(50) = NULL,
@ShadeNo VARCHAR(25) = NULL,
@MRP INT = NULL,
@RETURNID INT =null OUTPUT
AS
	IF @ACTION='I'
	BEGIN
		IF NOT EXISTS (SELECT * FROM W_STYLE WHERE BarcodeNo=@BarcodeNo )
			BEGIN 
				INSERT INTO W_STYLE (BarcodeNo ,StyleName ,DesignNo ,ShadeNo ,MRP)
				VALUES (@BarcodeNo ,@StyleName ,@DesignNo ,@ShadeNo ,@MRP)
				SET @RETURNID=@@IDENTITY				
				RETURN @RETURNID
			END
		ELSE 
			BEGIN 
					SET @RETURNID=0				
					RETURN @RETURNID
			END
	END

	IF @ACTION='U'
	BEGIN
		UPDATE W_STYLE	SET 
		BarcodeNo =@BarcodeNo
		,StyleName=@StyleName
		,DesignNo =@DesignNo
		,ShadeNo  =@ShadeNo
		,MRP	  =@MRP
		where W_StyleId=@W_StyleId
				
		SET @RETURNID = @@ROWCOUNT
	END
	
	IF @ACTION='D'
	BEGIN
		DELETE W_STYLE  WHERE W_StyleId=@W_StyleId
	END
	
	IF @ACTION='S'
	BEGIN
		SELECT * FROM W_STYLE WHERE W_StyleId=@W_StyleId
		SET @RETURNID = @@ROWCOUNT
	END













GO

/****** Object:  StoredProcedure [dbo].[UP_SER_MS_STYLE]    Script Date: 10/11/2011 15:18:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


      
CREATE PROCEDURE [dbo].[UP_SER_MS_STYLE]
@BarcodeNo VARCHAR(150) = NULL,
@StyleName VARCHAR(50) = NULL,
@DesignNo VARCHAR(50) = NULL,
@ShadeNo VARCHAR(25) = NULL,
@MRP INT = NULL,
@PAGE_NO INT=NULL,
@PAGE_SIZE INT=NULL,
@SORT_BY VARCHAR(100) = NULL,
@DESC BIT=0,
@TOTALROWS BIGINT=0 OUTPUT
AS       

BEGIN

	DECLARE @TEMPPAGING TABLE
	(
		ROWNUMBER INT IDENTITY(1,1),		
		W_StyleId int ,
		BarcodeNo VARCHAR(150),
		StyleName VARCHAR(50),
		DesignNo VARCHAR(50),
		ShadeNo VARCHAR(25),
		MRP INT
	)

	INSERT INTO @TEMPPAGING
	(W_StyleId,BarcodeNo ,StyleName ,DesignNo ,ShadeNo ,MRP)
	
	SELECT W_StyleId,BarcodeNo ,StyleName ,DesignNo ,ShadeNo ,MRP FROM 
	W_STYLE ST  WITH(NOLOCK)  	
	WHERE 	
	(@BarcodeNo IS NULL OR ST.BarcodeNo LIKE '%' + @BarcodeNo + '%') 
	AND (@StyleName IS NULL OR ST.StyleName LIKE '%' + @StyleName + '%') 
	AND (@DesignNo IS NULL OR ST.DesignNo LIKE '%' + @DesignNo + '%') 
	AND (@ShadeNo IS NULL OR ST.ShadeNo LIKE '%' + @ShadeNo + '%') 
	AND (@MRP IS NULL OR ST.MRP=@MRP) 		
	ORDER BY 
	CASE WHEN @DESC=1 THEN CASE WHEN @SORT_BY = 'BarcodeNo' THEN ST.BarcodeNo END END DESC,				
	CASE WHEN @DESC=1 THEN CASE WHEN @SORT_BY = 'StyleName' THEN ST.StyleName END END DESC,				
	CASE WHEN @DESC=1 THEN CASE WHEN @SORT_BY = 'DesignNo' THEN ST.DesignNo END END DESC,				
	CASE WHEN @DESC=1 THEN CASE WHEN @SORT_BY = 'ShadeNo' THEN ST.ShadeNo END END DESC,				
	CASE WHEN @DESC=1 THEN CASE WHEN @SORT_BY = 'MRP' THEN ST.MRP END END DESC,

	CASE WHEN @DESC=0 THEN CASE WHEN @SORT_BY = 'BarcodeNo' THEN ST.BarcodeNo END END ASC,				
	CASE WHEN @DESC=0 THEN CASE WHEN @SORT_BY = 'StyleName' THEN ST.StyleName END END ASC,				
	CASE WHEN @DESC=0 THEN CASE WHEN @SORT_BY = 'DesignNo' THEN ST.DesignNo END END ASC,				
	CASE WHEN @DESC=0 THEN CASE WHEN @SORT_BY = 'ShadeNo' THEN ST.ShadeNo END END ASC,				
	CASE WHEN @DESC=0 THEN CASE WHEN @SORT_BY = 'MRP' THEN ST.MRP END END ASC

	SET @TOTALROWS=@@ROWCOUNT
	IF NOT (ISNULL(@PAGE_NO,0)=0 AND ISNULL(@PAGE_SIZE,0)=0)		
	   BEGIN			

		SELECT * FROM @TEMPPAGING	
		WHERE ROWNUMBER BETWEEN ((@PAGE_NO-1) * @PAGE_SIZE) + 1 AND (@PAGE_NO*@PAGE_SIZE)									

		END
	ELSE
		BEGIN
			SELECT * FROM @TEMPPAGING	
		END

END














GO



