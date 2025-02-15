USE [i2ms2]
GO
/****** Object:  StoredProcedure [dbo].[sp_add_asset]    Script Date: 2015-02-02 오후 7:04:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  User [i2ms2]    Script Date: 2014-06-26 오후 3:10:07 ******/



CREATE PROC [dbo].[sp_add_asset]

    @CATALOG_ID INT
	,@LOC_ID INT
	,@ASSET_NAME VARCHAR(80)
	,@SERIAL_NO VARCHAR(40)
	,@IPV4 VARCHAR(15)
	,@REMARKS VARCHAR(80)
	,@INSTALL_USER_NAME VARCHAR(40)
	,@INSTALL_DATE DATE
	,@USER_ID INT
	,@ASSET_ID INT OUTPUT
AS

--DECLARE @SITE_ID INT
--DECLARE @BUILDING_ID INT
--DECLARE @FLOOR_ID INT
--DECLARE @ROOM_ID INT
--DECLARE @RACK_ID INT
--DECLARE @DISP_LEVEL VARCHAR(40) 
--DECLARE @DISP_NAME VARCHAR(40) 
--DECLARE @PREV_ID INT
--DECLARE @NEXT_ID INT

DECLARE @IMAGE_ID INT
DECLARE @CATALOG_GROUP_ID INT
DECLARE @LEVEL2_CATALOG_GROUP_ID INT
DECLARE @IC_NUM_OF_MAX_PP INT
DECLARE @SW_NUM_OF_SLOTS INT
DECLARE @NUM_OF_PORTS INT
DECLARE @MAX_CELLS INT
DECLARE @CNT INT
DECLARE @CNT2 INT
DECLARE @CELL_NO INT
DECLARE @SW_FIGURE_TYPE CHAR(1)

BEGIN TRAN

SET @MAX_CELLS = 15

---- location 테이블에서 기본 정보를 추출
--SELECT 
--    @SITE_ID = site_id, 
--	@BUILDING_ID = building_id, 
--	@FLOOR_ID = floor_id, 
--	@ROOM_ID = room_id, 
--	@RACK_ID = rack_id, 
--	@DISP_LEVEL = location_level
--FROM 
--    location
--WHERE
--    location_id = @LOC_ID

-- catalog 및 catalog_group 테이블에서 기본 정보를 추출
SELECT
	@CATALOG_ID = c.catalog_id
	,@CATALOG_GROUP_ID = c.catalog_group_id
	,@LEVEL2_CATALOG_GROUP_ID = cg.level2_catalog_group_id
	,@IC_NUM_OF_MAX_PP = c.ic_num_of_max_pp
	,@SW_NUM_OF_SLOTS = c.sw_num_of_slots
	,@NUM_OF_PORTS = c.num_of_ports
	,@IMAGE_ID = c.icon_16_image_id
	,@SW_FIGURE_TYPE = c.sw_figure_type
FROM
	catalog c, catalog_group cg
WHERE
	c.catalog_group_id = cg.catalog_group_id AND
	catalog_id = @CATALOG_ID

-- 새시형 자산트리의 경우 슬롯형스위치에 추가해야함.
DECLARE @SW_CARD_FLAG INT
DECLARE @SW_SLOT_FLAG INT
SET @SW_CARD_FLAG = 0
SET @SW_SLOT_FLAG = 0
IF (@CATALOG_GROUP_ID IN (3310, 3320, 3330))
BEGIN
	IF (@SW_FIGURE_TYPE = 'C') 
	BEGIN
		SET @SW_CARD_FLAG = 1
	END
	IF (@SW_FIGURE_TYPE = 'S') 
	BEGIN
		SET @SW_SLOT_FLAG = 1
	END
END

-- Step1. asset 테이블에 레코드 추가
INSERT INTO asset
	(catalog_id, location_id, asset_name, serial_no, ipv4, install_user_name, install_date, user_id, remarks)
VALUES
	(@CATALOG_ID, @LOC_ID, @ASSET_NAME, @SERIAL_NO, @IPV4, @INSTALL_USER_NAME, @INSTALL_DATE, @USER_ID, @REMARKS)

SET @ASSET_ID = @@IDENTITY

IF @@ERROR <> 0
BEGIN
ROLLBACK;
RETURN;
END

-- Step2. asset_aux 테이블에 레코드 추가
INSERT INTO asset_aux
	(asset_id)
VALUES
	(@ASSET_ID)

IF @@ERROR <> 0
BEGIN
ROLLBACK;
RETURN;
END

-- Step3. asset_port_link 테이블에 레코드 추가
SET @CNT = 1
WHILE (@CNT <= @NUM_OF_PORTS)
BEGIN
    INSERT INTO asset_port_link
		(asset_id, port_no)
	VALUES
		(@ASSET_ID, @CNT)
	SET @CNT = @CNT + 1
END

IF @@ERROR <> 0
BEGIN
ROLLBACK;
RETURN;
END

-- Step4. ipp_connect_status 테이블에 레코드 추가
IF ((@CATALOG_GROUP_ID IN (3130, 3140)) OR
	(@LEVEL2_CATALOG_GROUP_ID IN (3130, 3140)))
BEGIN
	INSERT INTO ipp_connect_status
		(ipp_asset_id)
	VALUES
		(@ASSET_ID)
END

IF @@ERROR <> 0
BEGIN
ROLLBACK;
RETURN;
END

-- Step5. asset_ipp_port_link 테이블에 레코드 추가
IF ((@CATALOG_GROUP_ID IN (3130, 3140)) OR
	(@LEVEL2_CATALOG_GROUP_ID IN (3130, 3140)))
BEGIN
	SET @CNT = 1
	WHILE (@CNT <= @NUM_OF_PORTS)
	BEGIN
		INSERT INTO asset_ipp_port_link
			(ipp_asset_id, port_no)
		VALUES
			(@ASSET_ID, @CNT)
		SET @CNT = @CNT + 1
	END
END

IF @@ERROR <> 0
BEGIN
ROLLBACK;
RETURN;
END

-- Step7. sw_card_config 테이블에 레코드 추가
IF (@CATALOG_GROUP_ID IN (3310, 3320, 3330))
BEGIN
	IF (@SW_SLOT_FLAG = 1)
	BEGIN
		SET @CNT = 1
		WHILE (@CNT <= @SW_NUM_OF_SLOTS)
		BEGIN
			INSERT INTO sw_card_config
				(sw_asset_id, slot_no)
			VALUES
				(@ASSET_ID, @CNT)
			SET @CNT = @CNT + 1
		END
	END
END
	

IF @@ERROR <> 0
BEGIN
ROLLBACK;
RETURN;
END

-- Step8. ic_ipp_config 테이블에 레코드 추가
IF (@CATALOG_GROUP_ID IN (3110))
BEGIN
	SET @CNT = 1
	WHILE (@CNT <= @IC_NUM_OF_MAX_PP)
	BEGIN
		INSERT INTO ic_ipp_config
			(ic_asset_id, ipp_connect_no)
		VALUES
			(@ASSET_ID, @CNT)
		SET @CNT = @CNT + 1
	END
END

IF @@ERROR <> 0
BEGIN
ROLLBACK;
RETURN;
END

-- Step9. ic_connect_status 테이블에 레코드 추가
IF (@CATALOG_GROUP_ID IN (3110))
BEGIN
	INSERT INTO ic_connect_status
		(ic_asset_id, ic_connect_status, last_updated)
	VALUES
		(@ASSET_ID, '-', GETDATE())
END

IF @@ERROR <> 0
BEGIN
ROLLBACK;
RETURN;
END

-- Step10. user_port_layout 테이블에 레코드 추가
IF (@CATALOG_GROUP_ID IN (3420, 3430))
BEGIN
	SET @CNT = 1
	WHILE (@CNT <= @NUM_OF_PORTS)
	BEGIN
		INSERT INTO user_port_layout
			(asset_id, port_no)
		VALUES
			(@ASSET_ID, @CNT)
		SET @CNT = @CNT + 1
	END
END

IF @@ERROR <> 0
BEGIN
ROLLBACK;
RETURN;
END

-- Step11. eb_port_config 테이블에 레코드 추가
IF (@CATALOG_GROUP_ID IN (3120))
BEGIN
	DECLARE @EB_PORT_TYPE CHAR(1)
	SET @CNT = 1
	WHILE (@CNT <= @NUM_OF_PORTS)
	BEGIN
		SET @EB_PORT_TYPE = '-'
		IF (@CNT = 1)
			SET @EB_PORT_TYPE = 'P'
		IF (@CNT = 5)
			SET @EB_PORT_TYPE = 'T'
		IF (@CNT = 9)
			SET @EB_PORT_TYPE = 'D'
		INSERT INTO eb_port_config
			(asset_id, port_no, port_type, alarm, last_updated)
		VALUES
			(@ASSET_ID, @CNT, @EB_PORT_TYPE, '-', GETDATE())
		SET @CNT = @CNT + 1
	END
END

IF @@ERROR <> 0
BEGIN
ROLLBACK;
RETURN;
END

-- Step12. eb_port_data_cur 테이블에 레코드 추가
IF (@CATALOG_GROUP_ID IN (3120))
BEGIN
	SET @CNT = 1
	WHILE (@CNT <= @NUM_OF_PORTS)
	BEGIN
		INSERT INTO eb_port_data_cur
			(asset_id, port_no, last_updated)
		VALUES
			(@ASSET_ID, @CNT, GETDATE())
		SET @CNT = @CNT + 1
	END
END

IF @@ERROR <> 0
BEGIN
ROLLBACK;
RETURN;
END

-- Step13. asset_ext 테이블에 레코드 추가
INSERT INTO asset_ext
	(asset_id, ext_id, catalog_id)
SELECT 
	@ASSET_ID, ext_id, @CATALOG_ID
FROM
	catalog_ext
WHERE
	catalog_id = @CATALOG_ID

IF @@ERROR <> 0
BEGIN
ROLLBACK;
RETURN;
END


-- PC의 경우 자산트리에 추가할 수 없음.
IF (@CATALOG_GROUP_ID NOT IN (3340))
BEGIN
-- Step 14. asset_tree 테이블에 레코드 추가
EXEC sp_add_asset_to_asset_tree @ASSET_ID, @ASSET_NAME, @SW_CARD_FLAG, @LOC_ID, @IMAGE_ID
END

IF @@ERROR <> 0
BEGIN
ROLLBACK;
RETURN;
END
COMMIT



GO
/****** Object:  StoredProcedure [dbo].[sp_add_asset_terminal]    Script Date: 2015-02-02 오후 7:04:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO








CREATE proc [dbo].[sp_add_asset_terminal]
    @MAC char(17)
	,@IPV4 VARCHAR(15)
	,@NET_BIOS_NAME VARCHAR(20)
	,@SW_ASSET_ID INT
	,@SW_PORT_NO INT
	,@NET_ID INT
	,@TERMINAL_ID INT OUT
as
BEGIN

	DECLARE @TERMINAL_ASSET_ID INT
	DECLARE @NAME_TYPE CHAR(1)
	DECLARE @TERMINAL_NAME VARCHAR(40)
	DECLARE @OUTLET_ASSET_ID INT
	DECLARE @OUTLET_PORT_NO INT
	DECLARE @TERMINAL_ALIAS VARCHAR(40)
	DECLARE @CATALOG_ID INT
	DECLARE @LOC_ID INT
	DECLARE @IP_ID INT
	DECLARE @INST_DATE DATE

	SELECT @INST_DATE = GETDATE()
	
	SET @CATALOG_ID = 434002	-- General PC

	-- Virtual MAC 인경우 처리하지 않고 빠져나간다.

	-- 링크다이어그램의 끝에 존재하는 아울렛 검색
	
	EXEC sp_find_outlet_in_asset_port_link @SW_ASSET_ID, @SW_PORT_NO, @OUTLET_ASSET_ID OUT, @OUTLET_PORT_NO OUT

	IF (@OUTLET_ASSET_ID = 0 OR @OUTLET_ASSET_ID IS NULL)
	BEGIN
		SELECT 'Does not found Outlet in asset_port_link'
		RETURN
	END

	SELECT @LOC_ID = location_id FROM asset WHERE asset_id = @OUTLET_ASSET_ID

	--단말테이블 갱신(new로 시작하는 항목)

	SELECT
		@TERMINAL_ID = terminal_id
		,@TERMINAL_ALIAS = terminal_alias
	FROM
		asset_terminal
	WHERE
		mac = @MAC

	-- NAME_TYPE과 TERMINAL_NAME을 결정한다. (ALIAS <- NET_BIOS <- MAC 순서)
	IF (RTRIM(@TERMINAL_ALIAS) = '' OR
		@TERMINAL_ALIAS IS NULL)
	BEGIN
		IF (RTRIM(@NET_BIOS_NAME) = '' OR @NET_BIOS_NAME IS NULL)
		BEGIN
			SET @NAME_TYPE = 'M'	-- MAC
			SET @TERMINAL_NAME = @MAC
		END
		ELSE
		BEGIN
			SET @NAME_TYPE = 'N'	-- NET BIOS
			SET @TERMINAL_NAME = @NET_BIOS_NAME
		END
	END
	ELSE
	BEGIN
		SET @NAME_TYPE = 'A'	-- ALIAS
		SET @TERMINAL_NAME = @TERMINAL_ALIAS
	END

	IF (@TERMINAL_ID = 0 OR @TERMINAL_ID IS NULL)
	BEGIN
		-- asset 테이블에 레코드 추가

		EXEC sp_add_asset		
			@CATALOG_ID	
			,@LOC_ID
			,@TERMINAL_NAME
			,NULL
			,NULL
			,'Remarks...'
			,90001
			,@INST_DATE
			,90001
			,@TERMINAL_ASSET_ID OUT

		INSERT INTO asset_terminal
			(mac
			,terminal_asset_id
			,net_id
			,name_type
			,terminal_name
			,new_enable
			,terminal_status
			,is_unlocated
			,new_net_bios_name
			,new_outlet_asset_id
			,new_outlet_port_no
			,new_sw_asset_id
			,new_sw_port_no)
		VALUES
			(@MAC
			,@TERMINAL_ASSET_ID
			,@NET_ID
			,@NAME_TYPE
			,@TERMINAL_NAME
			,'Y'
			,'Y'
			,'N'
			,@NET_BIOS_NAME
			,@OUTLET_ASSET_ID
			,@OUTLET_PORT_NO
			,@SW_ASSET_ID
			,@SW_PORT_NO)

		SET @TERMINAL_ID = @@IDENTITY

		INSERT INTO asset_terminal_ip
			(terminal_id, ipv4, cur_enable, new_enable)
		VALUES
			(@TERMINAL_ID, @IPV4, 'N', 'Y')

	END
	ELSE
	BEGIN
		UPDATE 
			asset
		SET
			catalog_id = @CATALOG_ID
			,location_id = @LOC_ID
			,asset_name = @TERMINAL_NAME
		WHERE 
			asset_id = (SELECT terminal_asset_id FROM asset_terminal WHERE mac = @MAC)

		SELECT @TERMINAL_ID = terminal_id FROM asset_terminal WHERE mac = @MAC

		UPDATE
			asset_terminal
		SET
			net_id = @NET_ID
			,name_type = @NAME_TYPE
			,terminal_name = @TERMINAL_NAME
			,new_enable = 'Y'
			,terminal_status = 'Y'
			,is_unlocated = 'N'
			,new_net_bios_name = @NET_BIOS_NAME
			,new_outlet_asset_id = @OUTLET_ASSET_ID
			,new_outlet_port_no = @OUTLET_PORT_NO
			,new_sw_asset_id = @SW_ASSET_ID
			,new_sw_port_no = @SW_PORT_NO
		WHERE
			terminal_id = @TERMINAL_ID

		-- IP에 값이 있는 경우 INSERT 시도 (terminal_id + ipv4는 unique)
		IF (RTRIM(@IPV4) != '' AND @IPV4 IS NOT NULL)
		BEGIN
			SELECT @IP_ID = ip_id FROM asset_terminal_ip WHERE terminal_id = @TERMINAL_ID AND ipv4 = @IPV4

			IF (@IP_ID IS NULL)
			BEGIN
				INSERT INTO asset_terminal_ip
					(terminal_id, ipv4, cur_enable, new_enable)
				VALUES
					(@TERMINAL_ID, @IPV4, 'N', 'Y')
			END
			ELSE
			BEGIN
				UPDATE asset_terminal_ip 
				SET
					new_enable = 'Y'
				WHERE ip_id = @IP_ID
			END
		END

	END
END









GO
/****** Object:  StoredProcedure [dbo].[sp_add_asset_to_asset_tree]    Script Date: 2015-02-02 오후 7:04:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROC [dbo].[sp_add_asset_to_asset_tree]
    @ASSET_ID INT
	,@ASSET_NAME VARCHAR(40)
	,@SW_CARD_FLAG INT
    ,@LOC_ID INT
    ,@IMAGE_ID INT
AS

DECLARE @DISP_LEVEL VARCHAR(40) 
DECLARE @DISP_NAME VARCHAR(40) 

IF (@LOC_ID = 0)
	RETURN

SELECT 
	@DISP_LEVEL = location_level
FROM 
    location
WHERE
    location_id = @LOC_ID

-- 스위치카드의 경우...
IF (@SW_CARD_FLAG = 1)
	SET @DISP_LEVEL = @DISP_LEVEL + 1

-- 표시순서를 가장 마지막에 추가
DECLARE @DISP_ORDER INT
SET @DISP_ORDER = (
	SELECT
		MAX(disp_order) + 1
	FROM
		asset_tree
	WHERE
		(location_id = @LOC_ID) AND (asset_id is not null) AND (disp_level = (@DISP_LEVEL+1))
)
IF @DISP_ORDER IS NULL
   SET @DISP_ORDER = 1


-- asset_tree 테이블에 레코드 추가
INSERT INTO [dbo].asset_tree
    ([asset_id]
	,[disp_name]
    ,[disp_level]
    ,[image_id]
	,[disp_order]
	,[location_id])
VALUES
    (@ASSET_ID
	,@ASSET_NAME
    ,@DISP_LEVEL+1
    ,@IMAGE_ID
	,@DISP_ORDER
	,@LOC_ID)









GO
/****** Object:  StoredProcedure [dbo].[sp_add_asset2]    Script Date: 2015-02-02 오후 7:04:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[sp_add_asset2]

    @CATALOG_ID INT
	,@LOC_ID INT
	,@ASSET_NAME VARCHAR(80)
	,@SERIAL_NO VARCHAR(40)
	,@IPV4 VARCHAR(15)
	,@REMARKS VARCHAR(80)
	,@INSTALL_USER_NAME VARCHAR(40)
	,@INSTALL_DATE DATE
	,@USER_ID INT
	,@ASSET_ID INT OUTPUT
AS

--DECLARE @SITE_ID INT
--DECLARE @BUILDING_ID INT
--DECLARE @FLOOR_ID INT
--DECLARE @ROOM_ID INT
--DECLARE @RACK_ID INT
--DECLARE @DISP_LEVEL VARCHAR(40) 
--DECLARE @DISP_NAME VARCHAR(40) 
--DECLARE @PREV_ID INT
--DECLARE @NEXT_ID INT

DECLARE @IMAGE_ID INT
DECLARE @CATALOG_GROUP_ID INT
DECLARE @LEVEL2_CATALOG_GROUP_ID INT
DECLARE @IC_NUM_OF_MAX_PP INT
DECLARE @SW_NUM_OF_SLOTS INT
DECLARE @NUM_OF_PORTS INT
DECLARE @MAX_CELLS INT
DECLARE @CNT INT
DECLARE @CNT2 INT
DECLARE @CELL_NO INT

BEGIN TRAN

SET @MAX_CELLS = 15

---- location 테이블에서 기본 정보를 추출
--SELECT 
--    @SITE_ID = site_id, 
--	@BUILDING_ID = building_id, 
--	@FLOOR_ID = floor_id, 
--	@ROOM_ID = room_id, 
--	@RACK_ID = rack_id, 
--	@DISP_LEVEL = location_level
--FROM 
--    location
--WHERE
--    location_id = @LOC_ID

-- catalog 및 catalog_group 테이블에서 기본 정보를 추출
SELECT
	@CATALOG_ID = c.catalog_id
	,@CATALOG_GROUP_ID = c.catalog_group_id
	,@LEVEL2_CATALOG_GROUP_ID = cg.level2_catalog_group_id
	,@IC_NUM_OF_MAX_PP = c.ic_num_of_max_pp
	,@SW_NUM_OF_SLOTS = c.sw_num_of_slots
	,@NUM_OF_PORTS = c.num_of_ports
	,@IMAGE_ID = c.icon_16_image_id
FROM
	catalog c, catalog_group cg
WHERE
	c.catalog_group_id = cg.catalog_group_id AND
	catalog_id = @CATALOG_ID

-- Step1. asset 테이블에 레코드 추가
INSERT INTO asset
	(catalog_id, location_id, asset_name, serial_no, ipv4, install_user_name, install_date, user_id, remarks)
VALUES
	(@CATALOG_ID, @LOC_ID, @ASSET_NAME, @SERIAL_NO, @IPV4, @INSTALL_USER_NAME, @INSTALL_DATE, @USER_ID, @REMARKS)

SET @ASSET_ID = @@IDENTITY

IF @@ERROR <> 0
BEGIN
ROLLBACK;
RETURN;
END

-- Step2. asset_aux 테이블에 레코드 추가
INSERT INTO asset_aux
	(asset_id)
VALUES
	(@ASSET_ID)

IF @@ERROR <> 0
BEGIN
ROLLBACK;
RETURN;
END

-- Step3. asset_port_link 테이블에 레코드 추가
SET @CNT = 1
WHILE (@CNT <= @NUM_OF_PORTS)
BEGIN
    INSERT INTO asset_port_link
		(asset_id, port_no)
	VALUES
		(@ASSET_ID, @CNT)
	SET @CNT = @CNT + 1
END

IF @@ERROR <> 0
BEGIN
ROLLBACK;
RETURN;
END

-- Step4. ipp_connect_status 테이블에 레코드 추가
IF ((@CATALOG_GROUP_ID IN (3130, 3140)) OR
	(@LEVEL2_CATALOG_GROUP_ID IN (3130, 3140)))
BEGIN
	INSERT INTO ipp_connect_status
		(ipp_asset_id)
	VALUES
		(@ASSET_ID)
END

IF @@ERROR <> 0
BEGIN
ROLLBACK;
RETURN;
END

-- Step5. asset_ipp_port_link 테이블에 레코드 추가
IF ((@CATALOG_GROUP_ID IN (3130, 3140)) OR
	(@LEVEL2_CATALOG_GROUP_ID IN (3130, 3140)))
BEGIN
	SET @CNT = 1
	WHILE (@CNT <= @NUM_OF_PORTS)
	BEGIN
		INSERT INTO asset_ipp_port_link
			(ipp_asset_id, port_no)
		VALUES
			(@ASSET_ID, @CNT)
		SET @CNT = @CNT + 1
	END
END

IF @@ERROR <> 0
BEGIN
ROLLBACK;
RETURN;
END

-- Step7. sw_card_config 테이블에 레코드 추가
IF ((@CATALOG_GROUP_ID IN (3310, 3320, 3330)) OR
	(@LEVEL2_CATALOG_GROUP_ID IN (3310, 3320, 3330)))
BEGIN
	SET @CNT = 1
	WHILE (@CNT <= @SW_NUM_OF_SLOTS)
	BEGIN
		INSERT INTO sw_card_config
			(sw_asset_id, slot_no)
		VALUES
			(@ASSET_ID, @CNT)
		SET @CNT = @CNT + 1
	END
END

IF @@ERROR <> 0
BEGIN
ROLLBACK;
RETURN;
END

-- Step8. ic_ipp_config 테이블에 레코드 추가
IF (@CATALOG_GROUP_ID IN (3110))
BEGIN
	SET @CNT = 1
	WHILE (@CNT <= @IC_NUM_OF_MAX_PP)
	BEGIN
		INSERT INTO ic_ipp_config
			(ic_asset_id, ipp_connect_no)
		VALUES
			(@ASSET_ID, @CNT)
		SET @CNT = @CNT + 1
	END
END

IF @@ERROR <> 0
BEGIN
ROLLBACK;
RETURN;
END

-- Step9. ic_connect_status 테이블에 레코드 추가
IF (@CATALOG_GROUP_ID IN (3110))
BEGIN
	INSERT INTO ic_connect_status
		(ic_asset_id, ic_connect_status)
	VALUES
		(@ASSET_ID, '-')
END

IF @@ERROR <> 0
BEGIN
ROLLBACK;
RETURN;
END

-- Step10. user_port_layout 테이블에 레코드 추가
IF (@CATALOG_GROUP_ID IN (3420, 3430))
BEGIN
	SET @CNT = 1
	WHILE (@CNT <= @NUM_OF_PORTS)
	BEGIN
		INSERT INTO user_port_layout
			(asset_id, port_no)
		VALUES
			(@ASSET_ID, @CNT)
		SET @CNT = @CNT + 1
	END
END

IF @@ERROR <> 0
BEGIN
ROLLBACK;
RETURN;
END

-- Step11. eb_port_config 테이블에 레코드 추가
IF (@CATALOG_GROUP_ID IN (3120))
BEGIN
	SET @CNT = 1
	WHILE (@CNT <= @NUM_OF_PORTS)
	BEGIN
		INSERT INTO eb_port_config
			(asset_id, port_no, port_type, alarm)
		VALUES
			(@ASSET_ID, @CNT, '-', '-')
		SET @CNT = @CNT + 1
	END
END

IF @@ERROR <> 0
BEGIN
ROLLBACK;
RETURN;
END

-- Step12. eb_port_data_cur 테이블에 레코드 추가
IF (@CATALOG_GROUP_ID IN (3120))
BEGIN
	SET @CNT = 1
	WHILE (@CNT <= @NUM_OF_PORTS)
	BEGIN
		INSERT INTO eb_port_data_cur
			(asset_id, port_no)
		VALUES
			(@ASSET_ID, @CNT)
		SET @CNT = @CNT + 1
	END
END

IF @@ERROR <> 0
BEGIN
ROLLBACK;
RETURN;
END

-- Step13. asset_ext 테이블에 레코드 추가
INSERT INTO asset_ext
	(asset_id, ext_id, catalog_id)
SELECT 
	@ASSET_ID, ext_id, @CATALOG_ID
FROM
	catalog_ext
WHERE
	catalog_id = @CATALOG_ID

IF @@ERROR <> 0
BEGIN
ROLLBACK;
RETURN;
END

-- Step 14. asset_tree 테이블에 레코드 추가
EXEC sp_add_asset_to_asset_tree @ASSET_ID, @ASSET_NAME, @LOC_ID, @IMAGE_ID

-- Step 15. favorite_tree 테이블에 레코드 추가
--EXEC sp_add_asset_to_favorite_tree @ASSET_ID, @ASSET_NAME, @LOC_ID, @IMAGE_ID

IF @@ERROR <> 0
BEGIN
ROLLBACK;
RETURN;
END
COMMIT




GO
/****** Object:  StoredProcedure [dbo].[sp_add_building]    Script Date: 2015-02-02 오후 7:04:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO








CREATE PROC [dbo].[sp_add_building]
   @SITE_ID INT
   ,@BUILDING_NAME VARCHAR(40)
   ,@IMAGE_ID INT
   ,@USER_ID INT
   ,@BUILDING_ID INT OUT
   ,@LOC_ID INT OUT
AS

BEGIN TRAN

-- Step 1. 빌딩 테이블에 레코드 추가
INSERT INTO [dbo].building 
    ([site_id]
    ,[building_name]
    ,[building_image_id]
    ,[user_id])
VALUES
    (@SITE_ID
    ,@BUILDING_NAME
    ,@IMAGE_ID
    ,@USER_ID)

IF @@ERROR <> 0
BEGIN
ROLLBACK;
RETURN;
END

SET @BUILDING_ID = @@IDENTITY

-- Step 2. 위치 데이터를 추가
EXEC sp_add_building_to_location @BUILDING_ID, @LOC_ID OUT

IF @@ERROR <> 0
BEGIN
ROLLBACK;
RETURN;
END

-- Step 3. 자산 트리에 추가
EXEC sp_add_building_to_asset_tree @LOC_ID

-- Step 4. 즐겨찾기 트리에 추가
--EXEC sp_add_building_to_favorite_tree @LOC_ID

IF @@ERROR <> 0
BEGIN
ROLLBACK;
RETURN;
END

COMMIT







GO
/****** Object:  StoredProcedure [dbo].[sp_add_building_to_asset_tree]    Script Date: 2015-02-02 오후 7:04:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE PROC [dbo].[sp_add_building_to_asset_tree]
   @LOC_ID INT
AS

DECLARE @DISP_NAME VARCHAR(40) 
DECLARE @IMAGE_ID INT

SET @IMAGE_ID = 2110002
SELECT 
	@DISP_NAME = location_name
FROM 
    location
WHERE
    location_id = @LOC_ID

-- asset_tree 테이블에 레코드 추가
INSERT INTO [dbo].asset_tree
    ([disp_name]
    ,[disp_level]
    ,[image_id]
	,[disp_order]
	,[location_id])
VALUES
    (@DISP_NAME
    ,4
    ,@IMAGE_ID
	,(SELECT disp_order FROM location WHERE location_id = @LOC_ID)
	,@LOC_ID)








GO
/****** Object:  StoredProcedure [dbo].[sp_add_building_to_location]    Script Date: 2015-02-02 오후 7:04:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO









CREATE PROC [dbo].[sp_add_building_to_location]
   @BUILDING_ID INT
   ,@LOC_ID INT OUTPUT
AS

DECLARE @REGION1_ID INT
DECLARE @REGION2_ID INT
DECLARE @SITE_ID INT
DECLARE @BUILDING_NAME VARCHAR(40)

-- 최상위 부터 ID 값을 알아온다.
SELECT 
	@SITE_ID = site_id 
	,@BUILDING_NAME = building_name
FROM 
	building
WHERE 
	building_id = @BUILDING_ID

SELECT 
	@REGION1_ID = region1_id 
	,@REGION2_ID = region2_id 
FROM 
	location 
WHERE 
	site_id = @SITE_ID

-- 표시순서를 가장 마지막에 추가
DECLARE @DISP_ORDER INT
SET @DISP_ORDER = (
	SELECT
		MAX(disp_order) + 1
	FROM
		location
	WHERE
		site_id = @SITE_ID AND location_level = 4
)

IF @DISP_ORDER IS NULL
    SET @DISP_ORDER = 1

-- location 테이블에 레코드 추가
INSERT INTO [dbo].[location]
    ([location_name]
    ,[location_level]
    ,[location_path]
	,[region1_id]
	,[region2_id]
    ,[site_id]
    ,[building_id]
	,[disp_order])
VALUES
    (@BUILDING_NAME
    ,4
    ,(SELECT RTRIM(site_name) FROM site WHERE site_id = @SITE_ID)
	+' -> ' 
	+@BUILDING_NAME
	,@REGION1_ID
	,@REGION2_ID
    ,@SITE_ID
    ,@BUILDING_ID
	,@DISP_ORDER)

SET @LOC_ID = @@IDENTITY





GO
/****** Object:  StoredProcedure [dbo].[sp_add_changed_hist]    Script Date: 2015-02-02 오후 7:04:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[sp_add_changed_hist] 
    @CUR_ASSET_ID INT
	,@ADD_FLAG INT
AS




GO
/****** Object:  StoredProcedure [dbo].[sp_add_event_1090021]    Script Date: 2015-02-02 오후 7:04:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[sp_add_event_1090021]
	@LANG_ID INT
	,@USER_ID INT
	,@REGION1_ID INT
	,@REGION1_NAME VARCHAR(40)
AS
	DECLARE @EVENT_ID INT
	DECLARE @EVENT_TEXT VARCHAR(160)
	DECLARE @LOCATION_ID INT

	SET @EVENT_ID = 1090021
	SET @EVENT_TEXT = '지역1 위치 항목이 추가되었습니다. 지역1=' + @REGION1_NAME
	SELECT @LOCATION_ID = location_id FROM location WHERE region1_id = @REGION1_ID AND region2_id IS NULL

	INSERT INTO event_hist
		(event_id, event_type, event_text, user_id, location_id)
	VALUES
		(@EVENT_ID
		,(SELECT event_type FROM event WHERE event_id = @EVENT_ID)
		,@EVENT_TEXT
		,@USER_ID
		,@LOCATION_ID)




GO
/****** Object:  StoredProcedure [dbo].[sp_add_floor]    Script Date: 2015-02-02 오후 7:04:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO











CREATE PROC [dbo].[sp_add_floor]
   @BUILDING_ID INT
   ,@FLOOR_NO INT
   ,@FLOOR_NAME VARCHAR(40)
   ,@DRAWING_3D_ID INT
   ,@USER_ID INT
   ,@FLOOR_ID INT OUT
   ,@LOC_ID INT OUT
AS

BEGIN TRAN

-- Step 1. floor 테이블에 레코드 추가
INSERT INTO [dbo].floor
    ([building_id]
    ,[floor_no]
    ,[floor_name]
    ,[drawing_3d_id]
    ,[user_id])
VALUES
    (@BUILDING_ID
    ,@FLOOR_NO
    ,@FLOOR_NAME
    ,@DRAWING_3D_ID
    ,@USER_ID)

IF @@ERROR <> 0
BEGIN
ROLLBACK;
RETURN;
END

SET @FLOOR_ID = @@IDENTITY

-- Step 2. 위치 데이터를 추가
EXEC sp_add_floor_to_location @FLOOR_ID ,@LOC_ID OUTPUT

IF @@ERROR <> 0
BEGIN
ROLLBACK;
RETURN;
END

-- Step 3. 자산 트리에 추가
EXEC sp_add_floor_to_asset_tree @LOC_ID

-- Step 4. 즐겨찾기 트리에 추가
--EXEC sp_add_floor_to_favorite_tree @LOC_ID

IF @@ERROR <> 0
BEGIN
ROLLBACK;
RETURN;
END

COMMIT









GO
/****** Object:  StoredProcedure [dbo].[sp_add_floor_to_asset_tree]    Script Date: 2015-02-02 오후 7:04:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO











CREATE PROC [dbo].[sp_add_floor_to_asset_tree]
   @LOC_ID INT
AS

DECLARE @DISP_NAME VARCHAR(40) 
DECLARE @IMAGE_ID INT

SET @IMAGE_ID = 2110003
SELECT 
	@DISP_NAME = location_name
FROM 
    location
WHERE
    location_id = @LOC_ID


-- asset_tree 테이블에 레코드 추가
INSERT INTO [dbo].asset_tree
    ([disp_name]
    ,[disp_level]
    ,[image_id]
	,[disp_order]
	,[location_id])
VALUES
    (@DISP_NAME
    ,5
    ,@IMAGE_ID
	,(SELECT disp_order FROM location WHERE location_id = @LOC_ID)
	,@LOC_ID)







GO
/****** Object:  StoredProcedure [dbo].[sp_add_floor_to_location]    Script Date: 2015-02-02 오후 7:04:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO










CREATE PROC [dbo].[sp_add_floor_to_location]
   @FLOOR_ID INT
   ,@LOC_ID INT OUTPUT
AS

DECLARE @REGION1_ID INT
DECLARE @REGION2_ID INT
DECLARE @SITE_ID INT
DECLARE @BUILDING_ID INT
DECLARE @FLOOR_NAME VARCHAR(40)

-- 최상위 부터 ID 값을 알아온다.
SELECT 
	@BUILDING_ID = building_id 
	,@FLOOR_NAME = floor_name
FROM 
	floor 
WHERE 
	floor_id = @FLOOR_ID

SELECT 
	@REGION1_ID = region1_id 
	,@REGION2_ID = region2_id 
	,@SITE_ID = site_id
FROM 
	location 
WHERE 
	building_id = @BUILDING_ID


-- 표시순서를 가장 마지막에 추가
DECLARE @DISP_ORDER INT
SET @DISP_ORDER = (
	SELECT
		MAX(disp_order) + 1
	FROM
		location
	WHERE
		building_id = @BUILDING_ID AND location_level = 5
)
IF @DISP_ORDER IS NULL
    SET @DISP_ORDER = 1

-- location 테이블에 레코드 추가
INSERT INTO [dbo].[location]
    ([location_name]
    ,[location_level]
    ,[location_path]
	,[region1_id]
	,[region2_id]
    ,[site_id]
    ,[building_id]
    ,[floor_id]
	,[disp_order])
VALUES
    (@FLOOR_NAME
    ,5
    ,(SELECT RTRIM(site_name) FROM site WHERE site_id = @SITE_ID)
	+' -> ' 
    +(SELECT RTRIM(building_name) FROM building WHERE building_id = @BUILDING_ID)
	+' -> ' 
	+@FLOOR_NAME
	,@REGION1_ID
	,@REGION2_ID
    ,@SITE_ID
    ,@BUILDING_ID
    ,@FLOOR_ID
	,@DISP_ORDER)

SET @LOC_ID = @@IDENTITY









GO
/****** Object:  StoredProcedure [dbo].[sp_add_rack]    Script Date: 2015-02-02 오후 7:04:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO















CREATE PROC [dbo].[sp_add_rack]
   @ROOM_ID INT
   ,@RACK_ROW CHAR(1)
   ,@RACK_NO INT
   ,@RACK_NAME VARCHAR(40)
   ,@USER_ID INT
   ,@RACK_ID INT OUT
   ,@LOC_ID INT OUT
AS

BEGIN TRAN

-- Step 1. rack 테이블에 레코드 추가
INSERT INTO [dbo].rack
    ([room_id]
    ,[rack_row]
    ,[rack_no]
    ,[rack_name]
    ,[user_id])
VALUES
    (@ROOM_ID
    ,@RACK_ROW
    ,@RACK_NO
    ,@RACK_NAME
    ,@USER_ID)

IF @@ERROR <> 0
BEGIN
ROLLBACK;
RETURN;
END

SET @RACK_ID = @@IDENTITY

-- Step 2. 위치 데이터를 추가
EXEC sp_add_rack_to_location @RACK_ID, @LOC_ID OUTPUT

IF @@ERROR <> 0
BEGIN
ROLLBACK;
RETURN;
END

-- Step 3. 자산 트리에 추가
EXEC sp_add_rack_to_asset_tree @LOC_ID

-- Step 4. 즐겨찾기 트리에 추가
--EXEC sp_add_rack_to_favorite_tree @LOC_ID

IF @@ERROR <> 0
BEGIN
ROLLBACK;
RETURN;
END

COMMIT









GO
/****** Object:  StoredProcedure [dbo].[sp_add_rack_to_asset_tree]    Script Date: 2015-02-02 오후 7:04:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO











CREATE PROC [dbo].[sp_add_rack_to_asset_tree]
   @LOC_ID INT
AS

DECLARE @DISP_NAME VARCHAR(40) 
DECLARE @IMAGE_ID INT

SET @IMAGE_ID = 2110005
SELECT 
	@DISP_NAME = location_name
FROM 
    location
WHERE
    location_id = @LOC_ID


-- asset_tree 테이블에 레코드 추가
INSERT INTO [dbo].asset_tree
    ([disp_name]
    ,[disp_level]
    ,[image_id]
	,[disp_order]
	,[location_id])
VALUES
    (@DISP_NAME
    ,7
    ,@IMAGE_ID
	,(SELECT disp_order FROM location WHERE location_id = @LOC_ID)
	,@LOC_ID)

GO
/****** Object:  StoredProcedure [dbo].[sp_add_rack_to_location]    Script Date: 2015-02-02 오후 7:04:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO









CREATE PROC [dbo].[sp_add_rack_to_location]
    @RACK_ID INT
   ,@LOC_ID INT OUTPUT
AS

DECLARE @REGION1_ID INT
DECLARE @REGION2_ID INT
DECLARE @SITE_ID INT
DECLARE @BUILDING_ID INT
DECLARE @FLOOR_ID INT
DECLARE @ROOM_ID INT
DECLARE @RACK_NAME VARCHAR(40)

-- 최상위 부터 ID 값을 알아온다.

SELECT 
	@ROOM_ID = room_id 
	,@RACK_NAME = rack_name
FROM 
	rack
WHERE 
	rack_id = @RACK_ID

SELECT 
	@REGION1_ID = region1_id 
	,@REGION2_ID = region2_id 
	,@SITE_ID = site_id
	,@BUILDING_ID = building_id
	,@FLOOR_ID = floor_id
FROM 
	location 
WHERE 
	room_id = @ROOM_ID


-- 표시순서를 가장 마지막에 추가
DECLARE @DISP_ORDER INT
SET @DISP_ORDER = (
	SELECT
		MAX(disp_order) + 1
	FROM
		location
	WHERE
		room_id = @ROOM_ID AND location_level = 7
)
IF @DISP_ORDER IS NULL
    SET @DISP_ORDER = 1

-- location 테이블에 레코드 추가
INSERT INTO [dbo].[location]
    ([location_name]
    ,[location_level]
    ,[location_path]
	,[region1_id]
	,[region2_id]
    ,[site_id]
    ,[building_id]
    ,[floor_id]
    ,[room_id]
    ,[rack_id]
	,[disp_order])
VALUES
    (@RACK_NAME
    ,7
    ,(SELECT RTRIM(site_name) FROM site WHERE site_id = @SITE_ID)
	+' -> ' 
    +(SELECT RTRIM(building_name) FROM building WHERE building_id = @BUILDING_ID)
	+' -> ' 
    +(SELECT RTRIM(floor_name) FROM floor WHERE floor_id = @FLOOR_ID)
	+' -> ' 
    +(SELECT RTRIM(room_name) FROM room WHERE room_id = @ROOM_ID)
	+' -> ' 
	+@RACK_NAME
    ,@REGION1_ID
    ,@REGION2_ID
    ,@SITE_ID
    ,@BUILDING_ID
    ,@FLOOR_ID
	,@ROOM_ID
	,@RACK_ID
	,@DISP_ORDER)

SET @LOC_ID = @@IDENTITY










GO
/****** Object:  StoredProcedure [dbo].[sp_add_region1]    Script Date: 2015-02-02 오후 7:04:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO











CREATE PROC [dbo].[sp_add_region1]
   @REGION1_NAME VARCHAR(40)
   ,@IMAGE_ID INT
   ,@POS_X INT
   ,@POS_Y INT
   ,@USER_ID INT
   ,@REGION1_ID INT OUT
   ,@LOC_ID INT OUT
AS

BEGIN TRAN

-- Step 1. 지역1 테이블에 레코드 추가
INSERT INTO [dbo].region1
    ([region1_name]
    ,[region1_image_id]
	,[pos_x]
	,[pos_y]
    ,[user_id])
VALUES
    (@REGION1_NAME
    ,@IMAGE_ID
    ,@POS_X
	,@POS_Y
    ,@USER_ID)

IF @@ERROR <> 0
BEGIN
ROLLBACK;
RETURN;
END

SET @REGION1_ID = @@IDENTITY

-- Step 2. 위치 데이터를 추가
EXEC sp_add_region1_to_location @REGION1_ID, @LOC_ID OUT

IF @@ERROR <> 0
BEGIN
ROLLBACK;
RETURN;
END
	
-- Step 3. 자산 트리에 추가
EXEC sp_add_region1_to_asset_tree @LOC_ID

-- Step 4. 즐겨찾기 트리에 추가
--EXEC sp_add_region1_to_favorite_tree @LOC_ID

IF @@ERROR <> 0
BEGIN
ROLLBACK;
RETURN;
END

COMMIT





GO
/****** Object:  StoredProcedure [dbo].[sp_add_region1_to_asset_tree]    Script Date: 2015-02-02 오후 7:04:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





CREATE PROC [dbo].[sp_add_region1_to_asset_tree]
   @LOC_ID INT
AS

DECLARE @DISP_NAME VARCHAR(40) 
DECLARE @IMAGE_ID INT

SET @IMAGE_ID = 2110099
SELECT 
	@DISP_NAME = location_name
FROM 
    location
WHERE
    location_id = @LOC_ID


-- asset_tree 테이블에 레코드 추가
INSERT INTO [dbo].asset_tree
    ([disp_name]
    ,[disp_level]
    ,[image_id]
	,[disp_order]
	,[location_id])
VALUES
    (@DISP_NAME
    ,1
    ,@IMAGE_ID
	,(SELECT disp_order FROM location WHERE location_id = @LOC_ID)
	,@LOC_ID)

GO
/****** Object:  StoredProcedure [dbo].[sp_add_region1_to_location]    Script Date: 2015-02-02 오후 7:04:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO








CREATE PROC [dbo].[sp_add_region1_to_location]
   @REGION1_ID INT
   ,@LOC_ID INT OUT
AS

DECLARE @REGION1_NAME VARCHAR(40)


-- 최상위 부터 ID 값을 알아온다.

SELECT 
	@REGION1_NAME = region1_name
FROM 
	region1
WHERE 
	region1_id = @REGION1_ID


-- 표시순서를 가장 마지막에 추가
DECLARE @DISP_ORDER INT
SET @DISP_ORDER = (
	SELECT
		MAX(disp_order) + 1
	FROM
		location
	WHERE
		location_level = 1
)
IF @DISP_ORDER IS NULL
    SET @DISP_ORDER = 1
	

-- location 테이블에 레코드 추가
INSERT INTO [dbo].[location]
    ([location_name]
    ,[location_level]
    ,[location_path]
    ,[region1_id]
	,[disp_order])    
VALUES
    (@REGION1_NAME
    ,1
    ,@REGION1_NAME
    ,@REGION1_ID
	,@DISP_ORDER)	

SET @LOC_ID = @@IDENTITY




GO
/****** Object:  StoredProcedure [dbo].[sp_add_region2]    Script Date: 2015-02-02 오후 7:04:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO









CREATE PROC [dbo].[sp_add_region2]
   @REGION1_ID INT
   ,@REGION2_NAME VARCHAR(40)
   ,@POS_X INT
   ,@POS_Y INT
   ,@USER_ID INT
   ,@REGION2_ID INT OUT
   ,@LOC_ID INT OUT
AS

BEGIN TRAN

-- Step 1. 지역2 테이블에 레코드 추가
INSERT INTO [dbo].region2
    ([region1_id]
    ,[region2_name]
    ,[pos_x]
	,[pos_y]
    ,[user_id])
VALUES
    (@REGION1_ID
    ,@REGION2_NAME
	,@POS_X
	,@POS_Y
    ,@USER_ID)

IF @@ERROR <> 0
BEGIN
ROLLBACK;
RETURN;
END

SET @REGION2_ID = @@IDENTITY

-- Step 2. 위치 데이터를 추가
EXEC sp_add_region2_to_location @REGION2_ID, @LOC_ID OUT

IF @@ERROR <> 0
BEGIN
ROLLBACK;
RETURN;
END

-- Step 3. 자산 트리에 추가
EXEC sp_add_region2_to_asset_tree @LOC_ID

-- Step 4. 즐겨찾기 트리에 추가
--EXEC sp_add_region2_to_favorite_tree @LOC_ID

IF @@ERROR <> 0
BEGIN
ROLLBACK;
RETURN;
END

COMMIT



GO
/****** Object:  StoredProcedure [dbo].[sp_add_region2_to_asset_tree]    Script Date: 2015-02-02 오후 7:04:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





CREATE PROC [dbo].[sp_add_region2_to_asset_tree]
   @LOC_ID INT
AS

DECLARE @IMAGE_ID INT
DECLARE @DISP_NAME VARCHAR(40)

SET @IMAGE_ID = 2110099
SELECT 
	@DISP_NAME = location_name
FROM 
    location
WHERE
    location_id = @LOC_ID


-- asset_tree 테이블에 레코드 추가
INSERT INTO [dbo].asset_tree
    ([disp_name]
    ,[disp_level]
    ,[image_id]
	,[disp_order]
	,[location_id])
VALUES
    (@DISP_NAME
    ,2
    ,@IMAGE_ID
	,(SELECT disp_order FROM location WHERE location_id = @LOC_ID)
	,@LOC_ID)



GO
/****** Object:  StoredProcedure [dbo].[sp_add_region2_to_location]    Script Date: 2015-02-02 오후 7:04:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO







CREATE PROC [dbo].[sp_add_region2_to_location]
   @REGION2_ID INT
   ,@LOC_ID INT OUT
AS

DECLARE @REGION1_ID INT
DECLARE @REGION2_NAME VARCHAR(40)


-- 최상위 부터 ID 값을 알아온다.
SELECT 
	@REGION1_ID = region1_id 
	,@REGION2_NAME = region2_name
FROM 
	region2
WHERE 
	region2_id = @REGION2_ID


-- 표시순서를 가장 마지막에 추가
DECLARE @DISP_ORDER INT
SET @DISP_ORDER = (
	SELECT
		MAX(disp_order) + 1
	FROM
		location
	WHERE
		region1_id = @REGION1_ID AND location_level = 2
)
IF @DISP_ORDER IS NULL
    SET @DISP_ORDER = 1


-- location 테이블에 레코드 추가
INSERT INTO [dbo].[location]
    ([location_name]
    ,[location_level]
    ,[location_path]
    ,[region1_id]
    ,[region2_id]
	,[disp_order])
VALUES
    (@REGION2_NAME
    ,2
    ,(SELECT RTRIM(region1_name) FROM region1 WHERE region1_id = @REGION1_ID)
	+' -> ' 
	+@REGION2_NAME
    ,@REGION1_ID
    ,@REGION2_ID
	,@DISP_ORDER)

SET @LOC_ID = @@IDENTITY








GO
/****** Object:  StoredProcedure [dbo].[sp_add_room]    Script Date: 2015-02-02 오후 7:04:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO











CREATE PROC [dbo].[sp_add_room]
   @FLOOR_ID INT
   ,@ROOM_NAME VARCHAR(40)
   ,@USER_ID INT
   ,@ROOM_ID INT OUT
   ,@LOC_ID INT OUT
AS

BEGIN TRAN

-- Step 1. room 테이블에 레코드 추가
INSERT INTO [dbo].room
    ([floor_id]
    ,[room_name]
    ,[user_id])
VALUES
    (@FLOOR_ID
    ,@ROOM_NAME
    ,@USER_ID)

IF @@ERROR <> 0
BEGIN
ROLLBACK;
RETURN;
END

SET @ROOM_ID = @@IDENTITY

-- Step 2. 위치 데이터를 추가
EXEC sp_add_room_to_location @ROOM_ID ,@LOC_ID OUTPUT

IF @@ERROR <> 0
BEGIN
ROLLBACK;
RETURN;
END

-- Step 3. 자산 트리에 추가
EXEC sp_add_room_to_asset_tree @LOC_ID

-- Step 4. 즐겨찾기 트리에 추가
--EXEC sp_add_room_to_favorite_tree @LOC_ID

IF @@ERROR <> 0
BEGIN
ROLLBACK;
RETURN;
END

COMMIT









GO
/****** Object:  StoredProcedure [dbo].[sp_add_room_to_asset_tree]    Script Date: 2015-02-02 오후 7:04:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO










CREATE PROC [dbo].[sp_add_room_to_asset_tree]
   @LOC_ID INT
AS

DECLARE @DISP_NAME VARCHAR(40) 
DECLARE @IMAGE_ID INT

SET @IMAGE_ID = 2110004
SELECT 
	@DISP_NAME = location_name
FROM 
    location
WHERE
    location_id = @LOC_ID


-- asset_tree 테이블에 레코드 추가
INSERT INTO [dbo].asset_tree
    ([disp_name]
    ,[disp_level]
    ,[image_id]
	,[disp_order]
	,[location_id])
VALUES
    (@DISP_NAME
    ,6
    ,@IMAGE_ID
	,(SELECT disp_order FROM location WHERE location_id = @LOC_ID)
	,@LOC_ID)






GO
/****** Object:  StoredProcedure [dbo].[sp_add_room_to_location]    Script Date: 2015-02-02 오후 7:04:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO









CREATE PROC [dbo].[sp_add_room_to_location]
   @ROOM_ID INT
   ,@LOC_ID INT OUTPUT
AS

DECLARE @REGION1_ID INT
DECLARE @REGION2_ID INT
DECLARE @SITE_ID INT
DECLARE @BUILDING_ID INT
DECLARE @FLOOR_ID INT
DECLARE @ROOM_NAME VARCHAR(40)

-- 최상위 부터 ID 값을 알아온다.
SELECT 
	@FLOOR_ID = floor_id 
	,@ROOM_NAME = room_name
FROM 
	room
WHERE 
	room_id = @ROOM_ID

SELECT 
	@REGION1_ID = region1_id 
	,@REGION2_ID = region2_id 
	,@SITE_ID = site_id
	,@BUILDING_ID = building_id
FROM 
	location 
WHERE 
	floor_id = @FLOOR_ID

-- 표시순서를 가장 마지막에 추가
DECLARE @DISP_ORDER INT
SET @DISP_ORDER = (
	SELECT
		MAX(disp_order) + 1
	FROM
		location
	WHERE
		floor_id = @FLOOR_ID AND location_level = 6
)
IF @DISP_ORDER IS NULL
    SET @DISP_ORDER = 1

-- location 테이블에 레코드 추가
INSERT INTO [dbo].[location]
    ([location_name]
    ,[location_level]
    ,[location_path]
	,[region1_id]
	,[region2_id]
    ,[site_id]
    ,[building_id]
    ,[floor_id]
	,[room_id]
	,[disp_order])
VALUES
    (@ROOM_NAME
    ,6
    ,(SELECT RTRIM(site_name) FROM site WHERE site_id = @SITE_ID)
	+' -> ' 
    +(SELECT RTRIM(building_name) FROM building WHERE building_id = @BUILDING_ID)
	+' -> ' 
    +(SELECT RTRIM(floor_name) FROM floor WHERE floor_id = @FLOOR_ID)
	+' -> ' 
	+@ROOM_NAME
    ,@REGION1_ID
    ,@REGION2_ID
    ,@SITE_ID
    ,@BUILDING_ID
    ,@FLOOR_ID
	,@ROOM_ID
	,@DISP_ORDER)

SET @LOC_ID = @@IDENTITY


GO
/****** Object:  StoredProcedure [dbo].[sp_add_site]    Script Date: 2015-02-02 오후 7:04:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO









CREATE PROC [dbo].[sp_add_site]
   @REGION2_ID INT
   ,@SITE_NAME VARCHAR(40)
   ,@IMAGE_ID INT
   ,@USER_ID INT
   ,@SITE_ID INT OUT
   ,@LOC_ID INT OUT
AS

BEGIN TRAN

-- Step 1. 사이트 테이블에 레코드 추가
INSERT INTO [dbo].site
    ([region2_id]
    ,[site_name]
    ,[site_image_id]
    ,[user_id])
VALUES
    (@REGION2_ID
    ,@SITE_NAME
    ,@IMAGE_ID
    ,@USER_ID)

IF @@ERROR <> 0
BEGIN
ROLLBACK;
RETURN;
END

SET @SITE_ID = @@IDENTITY

-- Step 2. 환경 데이터를 추가

INSERT INTO [dbo].site_environment
    (site_id)
VALUES
	(@SITE_ID)

IF @@ERROR <> 0
BEGIN
ROLLBACK;
RETURN;
END


-- Step 3. 위치 데이터를 추가
EXEC sp_add_site_to_location @SITE_ID ,@LOC_ID OUTPUT

IF @@ERROR <> 0
BEGIN
ROLLBACK;
RETURN;
END

-- Step 4. 자산 트리에 추가
EXEC sp_add_site_to_asset_tree @LOC_ID

IF @@ERROR <> 0
BEGIN
ROLLBACK;
RETURN;
END

COMMIT




GO
/****** Object:  StoredProcedure [dbo].[sp_add_site_to_asset_tree]    Script Date: 2015-02-02 오후 7:04:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





CREATE PROC [dbo].[sp_add_site_to_asset_tree]
   @LOC_ID INT
AS

DECLARE @IMAGE_ID INT
DECLARE @DISP_NAME VARCHAR(40)

SET @IMAGE_ID = 2110001
SELECT 
	@DISP_NAME = location_name
FROM 
    location
WHERE
    location_id = @LOC_ID


-- asset_tree 테이블에 레코드 추가
INSERT INTO [dbo].asset_tree
    ([disp_name]
    ,[disp_level]
    ,[image_id]
	,[disp_order]
	,[location_id])
VALUES
    (@DISP_NAME
    ,3
    ,@IMAGE_ID
	,(SELECT disp_order FROM location WHERE location_id = @LOC_ID)
	,@LOC_ID)


GO
/****** Object:  StoredProcedure [dbo].[sp_add_site_to_location]    Script Date: 2015-02-02 오후 7:04:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO









CREATE PROC [dbo].[sp_add_site_to_location]
   @SITE_ID INT
   ,@LOC_ID INT OUT
AS

DECLARE @REGION1_ID INT
DECLARE @REGION2_ID INT
DECLARE @BUILDING_ID INT
DECLARE @SITE_NAME VARCHAR(40)

-- 최상위 부터 ID 값을 알아온다.
SELECT 
	@REGION2_ID = region2_id 
	,@SITE_NAME = site_name
FROM 
	site 
WHERE 
	site_id = @SITE_ID

SELECT 
	@REGION1_ID = region1_id 
FROM 
	location 
WHERE 
	region2_id = @REGION2_ID


-- 표시순서를 가장 마지막에 추가
DECLARE @DISP_ORDER INT
SET @DISP_ORDER = (
	SELECT
		MAX(disp_order) + 1
	FROM
		location
	WHERE
		region2_id = @REGION2_ID AND location_level = 3
)
IF @DISP_ORDER IS NULL
    SET @DISP_ORDER = 1

-- location 테이블에 레코드 추가
INSERT INTO [dbo].[location]
    ([location_name]
    ,[location_level]
    ,[location_path]
    ,[region1_id]
    ,[region2_id]
    ,[site_id]
	,[disp_order])
VALUES
    (@SITE_NAME
    ,3
	,@SITE_NAME
    ,@REGION1_ID
    ,@REGION2_ID
    ,@SITE_ID
	,@DISP_ORDER)

SET @LOC_ID = @@IDENTITY








GO
/****** Object:  StoredProcedure [dbo].[sp_del_all_asset]    Script Date: 2015-02-02 오후 7:04:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO







CREATE PROC [dbo].[sp_del_all_asset]
@SITE_ID INT
AS

BEGIN TRAN

-- Step 1. favorite_tree 테이블 처리

DELETE FROM 
    [dbo].favorite_tree 
	WHERE asset_id in (SELECT aa.asset_id 
					   FROM [dbo].asset AS aa, [dbo].location as cc
					   WHERE aa.location_id = cc.location_id AND
							cc.site_id = @SITE_ID)

-- Step 2. asset_tree 테이블 처리

DELETE FROM 
    [dbo].asset_tree 
	WHERE asset_id in (SELECT aa.asset_id 
					   FROM [dbo].asset AS aa, [dbo].location as cc
					   WHERE aa.location_id = cc.location_id AND
							cc.site_id = @SITE_ID)

-- Step 2.1. rack_config 테이블 처리

DELETE FROM
	[dbo].rack_config
	WHERE asset_id in (SELECT aa.asset_id 
					   FROM [dbo].asset AS aa, [dbo].location as cc
					   WHERE aa.location_id = cc.location_id AND
							cc.site_id = @SITE_ID)

-- Step 3. eb_port_data_cur 테이블 처리

DELETE FROM
	[dbo].eb_port_data_cur
	WHERE asset_id in (SELECT aa.asset_id 
					   FROM [dbo].asset AS aa, [dbo].location as cc
					   WHERE aa.location_id = cc.location_id AND
							cc.site_id = @SITE_ID)

-- Step 4. eb_port_config 테이블 처리

DELETE FROM
	[dbo].eb_port_config
	WHERE asset_id in (SELECT aa.asset_id 
					   FROM [dbo].asset AS aa, [dbo].location as cc
					   WHERE aa.location_id = cc.location_id AND
							cc.site_id = @SITE_ID)

-- Step 5. asset_ext 테이블 처리

DELETE FROM
	[dbo].asset_ext
	WHERE asset_id in (SELECT aa.asset_id 
					   FROM [dbo].asset AS aa, [dbo].location as cc
					   WHERE aa.location_id = cc.location_id AND
							cc.site_id = @SITE_ID)

-- Step 6. user_port_layout 테이블 처리

DELETE FROM
	[dbo].user_port_layout
	WHERE asset_id in (SELECT aa.asset_id 
					   FROM [dbo].asset AS aa, [dbo].location as cc
					   WHERE aa.location_id = cc.location_id AND
							cc.site_id = @SITE_ID)

-- Step 7. ic_connect_status 테이블 처리

DELETE FROM
	[dbo].ic_connect_status
	WHERE ic_asset_id in (SELECT aa.asset_id 
					   FROM [dbo].asset AS aa, [dbo].location as cc
					   WHERE aa.location_id = cc.location_id AND
							cc.site_id = @SITE_ID)

-- Step 8. ic_ipp_config 테이블 처리

DELETE FROM
	[dbo].ic_ipp_config
	WHERE ic_asset_id in (SELECT aa.asset_id 
					   FROM [dbo].asset AS aa, [dbo].location as cc
					   WHERE aa.location_id = cc.location_id AND
							cc.site_id = @SITE_ID)

UPDATE 
	[dbo].ic_ipp_config
	SET ipp_asset_id = NULL
	WHERE ipp_asset_id in (SELECT aa.asset_id 
					   FROM [dbo].asset AS aa, [dbo].location as cc
					   WHERE aa.location_id = cc.location_id AND
							cc.site_id = @SITE_ID)

-- Step 9. sw_card_config 테이블 처리

DELETE FROM
	[dbo].sw_card_config
	WHERE sw_asset_id in (SELECT aa.asset_id 
					   FROM [dbo].asset AS aa, [dbo].location as cc
					   WHERE aa.location_id = cc.location_id AND
							cc.site_id = @SITE_ID)

UPDATE 
	[dbo].sw_card_config
	SET sw_card_asset_id = NULL
	WHERE sw_card_asset_id in (SELECT aa.asset_id 
					   FROM [dbo].asset AS aa, [dbo].location as cc
					   WHERE aa.location_id = cc.location_id AND
							cc.site_id = @SITE_ID)

-- Step 10. link_diagram 테이블 처리

--DELETE FROM
--	[dbo].link_diagram
--	WHERE asset_id in (SELECT aa.asset_id 
--					   FROM [dbo].asset AS aa, [dbo].location as cc
--					   WHERE aa.location_id = cc.location_id AND
--							cc.site_id = @SITE_ID)

-- Step 11. asset_ipp_port_link 테이블 처리

DELETE FROM
	[dbo].asset_ipp_port_link
	WHERE ipp_asset_id in (SELECT aa.asset_id 
					   FROM [dbo].asset AS aa, [dbo].location as cc
					   WHERE aa.location_id = cc.location_id AND
							cc.site_id = @SITE_ID)

-- Step 12. ipp_connect_status 테이블 처리

DELETE FROM
	[dbo].ipp_connect_status
	WHERE ipp_asset_id in (SELECT aa.asset_id 
					   FROM [dbo].asset AS aa, [dbo].location as cc
					   WHERE aa.location_id = cc.location_id AND
							cc.site_id = @SITE_ID)

-- Step 13. asset_port_link 테이블 처리

DELETE FROM
	[dbo].asset_port_link
	WHERE asset_id in (SELECT aa.asset_id 
					   FROM [dbo].asset AS aa, [dbo].location as cc
					   WHERE aa.location_id = cc.location_id AND
							cc.site_id = @SITE_ID)

-- Step 14. asset_aux 테이블 처리

DELETE FROM
	[dbo].asset_aux
	WHERE asset_id in (SELECT aa.asset_id 
					   FROM [dbo].asset AS aa, [dbo].location as cc
					   WHERE aa.location_id = cc.location_id AND
							cc.site_id = @SITE_ID)

-- Step 15. asset 테이블 처리

DELETE FROM
	[dbo].asset
	WHERE asset_id in (SELECT aa.asset_id 
					   FROM [dbo].asset AS aa, [dbo].location as cc
					   WHERE aa.location_id = cc.location_id AND
							cc.site_id = @SITE_ID)
	
IF @@ERROR <> 0
BEGIN
ROLLBACK;
RETURN;
END
COMMIT





GO
/****** Object:  StoredProcedure [dbo].[sp_del_all_terminal]    Script Date: 2015-02-02 오후 7:04:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_del_all_terminal]
AS
BEGIN
	DECLARE @ASSET_ID INT
	DECLARE cur CURSOR FOR 
	SELECT 
		terminal_asset_id
	FROM 
		asset_terminal at
	OPEN cur
	FETCH NEXT FROM cur INTO @ASSET_ID

	WHILE @@FETCH_STATUS = 0
	BEGIN
		EXEC sp_del_asset @ASSET_ID
		FETCH NEXT FROM cur INTO @ASSET_ID
	END

	CLOSE cur
	DEALLOCATE cur
END

GO
/****** Object:  StoredProcedure [dbo].[sp_del_asset]    Script Date: 2015-02-02 오후 7:04:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO






CREATE PROC [dbo].[sp_del_asset]
   @ASSET_ID INT
AS
DECLARE @TREE_ID INT

BEGIN TRAN

-- Step 1. favorite_tree 테이블 처리

SELECT 
     @TREE_ID = favorite_tree_id
FROM [dbo].favorite_tree 
WHERE asset_id = @ASSET_ID

DELETE FROM 
    [dbo].favorite_tree 
WHERE 
    favorite_tree_id = @TREE_ID

-- Step 2. asset_tree 테이블 처리

SELECT 
     @TREE_ID = asset_tree_id
FROM [dbo].asset_tree 
WHERE asset_id = @ASSET_ID

DELETE FROM 
    [dbo].asset_tree 
WHERE 
    asset_tree_id = @TREE_ID



-- Step 2.1. rack_config 테이블 처리

DELETE FROM
	[dbo].rack_config
WHERE
	asset_id = @ASSET_ID

-- Step 3. eb_port_data_cur 테이블 처리

DELETE FROM
	[dbo].eb_port_data_cur
WHERE
	asset_id = @ASSET_ID

-- Step 4. eb_port_config 테이블 처리

DELETE FROM
	[dbo].eb_port_config
WHERE
	asset_id = @ASSET_ID


-- Step 4.1 asset_terminal_ip

DELETE FROM
	[dbo].asset_terminal_ip
WHERE
	terminal_id = (SELECT terminal_id FROM asset_terminal WHERE terminal_asset_id = @ASSET_ID)

-- Step 4.2 asset_terminal

DELETE FROM
	[dbo].asset_terminal
WHERE
	terminal_asset_id = @ASSET_ID



-- Step 5. asset_ext 테이블 처리

DELETE FROM
	[dbo].asset_ext
WHERE
	asset_id = @ASSET_ID

-- Step 6. user_port_layout 테이블 처리

DELETE FROM
	[dbo].user_port_layout
WHERE
	asset_id = @ASSET_ID

-- Step 7. ic_connect_status 테이블 처리

DELETE FROM
	[dbo].ic_connect_status
WHERE
	ic_asset_id = @ASSET_ID

-- Step 8. ic_ipp_config 테이블 처리

DELETE FROM
	[dbo].ic_ipp_config
WHERE
	ic_asset_id = @ASSET_ID

UPDATE 
	[dbo].ic_ipp_config
	SET ipp_asset_id = NULL
	WHERE ipp_asset_id = @ASSET_ID

-- Step 9. sw_card_config 테이블 처리

DELETE FROM
	[dbo].sw_card_config
WHERE
	sw_asset_id = @ASSET_ID

UPDATE 
	[dbo].sw_card_config
	SET sw_card_asset_id = NULL
	WHERE sw_card_asset_id = @ASSET_ID



-- Step 11. asset_ipp_port_link 테이블 처리

DELETE FROM
	[dbo].asset_ipp_port_link
WHERE
	ipp_asset_id = @ASSET_ID

-- Step 12. ipp_connect_status 테이블 처리

DELETE FROM
	[dbo].ipp_connect_status
WHERE
	ipp_asset_id = @ASSET_ID

-- Step 13. asset_port_link 테이블 처리

UPDATE 
	[dbo].asset_port_link
SET
	front_asset_id = null,
	front_port_no = null,
	front_plug_side = null,
	front_cable_catalog_id = null
WHERE 
	front_asset_id = @ASSET_ID		

UPDATE 
	[dbo].asset_port_link
SET
	rear_asset_id = null,
	rear_port_no = null,
	rear_plug_side = null,
	rear_cable_catalog_id = null
WHERE 
	rear_asset_id = @ASSET_ID		

DELETE FROM
	[dbo].asset_port_link
WHERE
	asset_id = @ASSET_ID

-- Step 14. asset_aux 테이블 처리

DELETE FROM
	[dbo].asset_aux
WHERE
	asset_id = @ASSET_ID

-- Step 15. asset 테이블 처리

DELETE FROM
	[dbo].asset
WHERE
	asset_id = @ASSET_ID
	
IF @@ERROR <> 0
BEGIN
ROLLBACK;
RETURN;
END
COMMIT




GO
/****** Object:  StoredProcedure [dbo].[sp_del_building]    Script Date: 2015-02-02 오후 7:04:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[sp_del_building]
   @BUILDING_ID INT
AS
DECLARE @TREE_ID INT
DECLARE @LOC_ID INT
DECLARE @PREV_ID INT
DECLARE @NEXT_ID INT

BEGIN TRAN

-- Step 2. asset_tree 테이블 처리

SELECT 
     @TREE_ID = asset_tree_id
    ,@LOC_ID = location_id
FROM [dbo].asset_tree 
WHERE location_id = 
    (SELECT location_id FROM location WHERE building_id = @BUILDING_ID)

DELETE FROM 
    [dbo].asset_tree 
WHERE 
    asset_tree_id = @TREE_ID

-- Step 3. location 테이블 처리

DELETE FROM 
    [dbo].location 
WHERE 
    location_id = @LOC_ID
    
-- Step 4. building 테이블 처리

-- building 테이블에서 레코드 삭제
DELETE FROM 
    [dbo].building 
WHERE 
    building_id = @BUILDING_ID

IF @@ERROR <> 0
BEGIN
ROLLBACK;
RETURN;
END
COMMIT




GO
/****** Object:  StoredProcedure [dbo].[sp_del_catalog]    Script Date: 2015-02-02 오후 7:04:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO







CREATE PROC [dbo].[sp_del_catalog]
   @CATALOG_ID INT
AS

BEGIN TRAN

-- Step 1. catalog_ext 테이블 처리

DELETE FROM
	[dbo].catalog_ext 
WHERE 
	catalog_id = @CATALOG_ID

-- Step 2. catalog 테이블 처리

DELETE FROM
	[dbo].catalog
WHERE 
	catalog_id = @CATALOG_ID
	
IF @@ERROR <> 0
BEGIN
ROLLBACK;
RETURN;
END
COMMIT


GO
/****** Object:  StoredProcedure [dbo].[sp_del_ext_property]    Script Date: 2015-02-02 오후 7:04:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO








CREATE PROC [dbo].[sp_del_ext_property]
   @EXT_ID INT
AS

BEGIN TRAN

-- Step 1. catalog_ext 테이블 처리

DELETE FROM
	[dbo].asset_ext
WHERE 
	ext_id = @EXT_ID

-- Step 2. catalog_ext 테이블 처리

DELETE FROM
	[dbo].catalog_ext 
WHERE 
	ext_id = @EXT_ID

-- Step 3. ext_property_ans 테이블 처리

DELETE FROM
	[dbo].ext_property_ans
WHERE 
	ext_id = @EXT_ID

-- Step 4. ext_property 테이블 처리

DELETE FROM
	[dbo].ext_property
WHERE 
	ext_id = @EXT_ID
	
IF @@ERROR <> 0
BEGIN
ROLLBACK;
RETURN;
END
COMMIT



GO
/****** Object:  StoredProcedure [dbo].[sp_del_floor]    Script Date: 2015-02-02 오후 7:04:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROC [dbo].[sp_del_floor]
   @FLOOR_ID INT
AS
DECLARE @TREE_ID INT
DECLARE @LOC_ID INT

BEGIN TRAN


-- Step 2. asset_tree 테이블 처리

SELECT 
     @TREE_ID = asset_tree_id
    ,@LOC_ID = location_id
FROM [dbo].asset_tree 
WHERE location_id = 
    (SELECT location_id FROM location WHERE floor_id = @FLOOR_ID)

DELETE FROM 
    [dbo].asset_tree 
WHERE 
    asset_tree_id = @TREE_ID

-- Step 3. location 테이블 처리

DELETE FROM 
    [dbo].location 
WHERE 
    location_id = @LOC_ID
    
-- Step 4. floor 테이블 처리

-- floor 테이블에서 레코드 삭제
DELETE FROM 
    [dbo].floor
WHERE 
    floor_id = @FLOOR_ID

IF @@ERROR <> 0
BEGIN
ROLLBACK;
RETURN;
END
COMMIT






GO
/****** Object:  StoredProcedure [dbo].[sp_del_rack]    Script Date: 2015-02-02 오후 7:04:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[sp_del_rack]
   @RACK_ID INT
AS
DECLARE @TREE_ID INT
DECLARE @LOC_ID INT
DECLARE @PREV_ID INT
DECLARE @NEXT_ID INT

BEGIN TRAN

-- Step 1. rack_config 테이블 처리

DELETE FROM 
    [dbo].rack_config 
WHERE 
    rack_id = @RACK_ID

-- Step 3. asset_tree 테이블 처리

-- asset_tree 테이블에서 해당 룸 검색
SELECT 
     @TREE_ID = asset_tree_id
    ,@LOC_ID = location_id
FROM [dbo].asset_tree 
WHERE location_id = 
    (SELECT location_id FROM location WHERE rack_id = @RACK_ID)

-- asset_tree 테이블에서 레코드 삭제
DELETE FROM 
    [dbo].asset_tree 
WHERE 
    asset_tree_id = @TREE_ID

-- Step 4. location 테이블 처리

DELETE FROM 
    [dbo].location 
WHERE 
    location_id = @LOC_ID
    
-- Step 5. rack 테이블 처리

-- rack 테이블에서 레코드 삭제
DELETE FROM 
    [dbo].rack
WHERE 
    rack_id = @RACK_ID

IF @@ERROR <> 0
BEGIN
ROLLBACK;
RETURN;
END
COMMIT





GO
/****** Object:  StoredProcedure [dbo].[sp_del_rack_config]    Script Date: 2015-02-02 오후 7:04:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO










CREATE PROC [dbo].[sp_del_rack_config]
   @RACK_ID INT
AS

BEGIN TRAN

DELETE FROM
	[dbo].rack_config
WHERE 
	rack_id = @RACK_ID

IF @@ERROR <> 0
BEGIN
ROLLBACK;
RETURN;
END
COMMIT





GO
/****** Object:  StoredProcedure [dbo].[sp_del_region1]    Script Date: 2015-02-02 오후 7:04:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROC [dbo].[sp_del_region1]
   @REGION1_ID INT
AS
DECLARE @TREE_ID INT
DECLARE @LOC_ID INT

BEGIN TRAN

-- Step 2. asset_tree 테이블 처리

SELECT 
     @TREE_ID = asset_tree_id
    ,@LOC_ID = location_id
FROM [dbo].asset_tree 
WHERE location_id = 
    (SELECT location_id FROM location WHERE region1_id = @REGION1_ID)

DELETE FROM 
    [dbo].asset_tree 
WHERE 
    asset_tree_id = @TREE_ID

-- Step 3. location 테이블 처리

DELETE FROM 
    [dbo].location 
WHERE 
    location_id = @LOC_ID
    
-- Step 4. region1 테이블 처리

-- building 테이블에서 레코드 삭제
DELETE FROM 
    [dbo].region1
WHERE 
    region1_id = @REGION1_ID

IF @@ERROR <> 0
BEGIN
ROLLBACK;
RETURN;
END
COMMIT






GO
/****** Object:  StoredProcedure [dbo].[sp_del_region2]    Script Date: 2015-02-02 오후 7:04:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROC [dbo].[sp_del_region2]
   @REGION2_ID INT
AS
DECLARE @TREE_ID INT
DECLARE @LOC_ID INT
DECLARE @PREV_ID INT
DECLARE @NEXT_ID INT

BEGIN TRAN

-- Step 2. asset_tree 테이블 처리

SELECT 
     @TREE_ID = asset_tree_id
    ,@LOC_ID = location_id
FROM [dbo].asset_tree 
WHERE location_id = 
    (SELECT location_id FROM location WHERE region2_id = @REGION2_ID)

DELETE FROM 
    [dbo].asset_tree 
WHERE 
    asset_tree_id = @TREE_ID

-- Step 3. location 테이블 처리

DELETE FROM 
    [dbo].location 
WHERE 
    location_id = @LOC_ID
    
-- Step 4. region2 테이블 처리

-- 지역2 테이블에서 레코드 삭제
DELETE FROM 
    [dbo].region2
WHERE 
    region2_id = @REGION2_ID

IF @@ERROR <> 0
BEGIN
ROLLBACK;
RETURN;
END
COMMIT






GO
/****** Object:  StoredProcedure [dbo].[sp_del_room]    Script Date: 2015-02-02 오후 7:04:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[sp_del_room]
   @ROOM_ID INT
AS
DECLARE @TREE_ID INT
DECLARE @LOC_ID INT

BEGIN TRAN


-- Step 2. asset_tree 테이블 처리

SELECT 
     @TREE_ID = asset_tree_id
    ,@LOC_ID = location_id
FROM [dbo].asset_tree 
WHERE location_id = 
    (SELECT location_id FROM location WHERE room_id = @ROOM_ID)

DELETE FROM 
    [dbo].asset_tree 
WHERE 
    asset_tree_id = @TREE_ID

-- Step 3. location 테이블 처리

DELETE FROM 
    [dbo].location 
WHERE 
    location_id = @LOC_ID
    
-- Step 4. room 테이블 처리

-- room 테이블에서 레코드 삭제
DELETE FROM 
    [dbo].room
WHERE 
    room_id = @ROOM_ID

IF @@ERROR <> 0
BEGIN
ROLLBACK;
RETURN;
END
COMMIT





GO
/****** Object:  StoredProcedure [dbo].[sp_del_site]    Script Date: 2015-02-02 오후 7:04:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROC [dbo].[sp_del_site]
   @SITE_ID INT
AS
DECLARE @TREE_ID INT
DECLARE @LOC_ID INT
DECLARE @PREV_ID INT
DECLARE @NEXT_ID INT

BEGIN TRAN


-- Step 2. asset_tree 테이블 처리

SELECT 
     @TREE_ID = asset_tree_id
    ,@LOC_ID = location_id
FROM [dbo].asset_tree 
WHERE location_id = 
    (SELECT location_id FROM location WHERE site_id = @SITE_ID)

DELETE FROM 
    [dbo].asset_tree 
WHERE 
    asset_tree_id = @TREE_ID

-- Step 3. location 테이블 처리

DELETE FROM 
    [dbo].location 
WHERE 
    location_id = @LOC_ID

-- Step 4. site_environment 테이블 처리

DELETE FROM
	[dbo].site_environment
WHERE 
	site_id = @SITE_ID
    
-- Step5 site_user 테이블에서 레코드 삭제
DELETE FROM 
    [dbo].site_user
WHERE 
    site_id = @SITE_ID
    
-- Step 6. site 테이블 처리

-- building 테이블에서 레코드 삭제
DELETE FROM 
    [dbo].site
WHERE 
    site_id = @SITE_ID

IF @@ERROR <> 0
BEGIN
ROLLBACK;
RETURN;
END
COMMIT






GO
/****** Object:  StoredProcedure [dbo].[sp_del_template_column]    Script Date: 2015-02-02 오후 7:04:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO









CREATE PROC [dbo].[sp_del_template_column]
   @TEMPLATE_ID INT
AS

BEGIN TRAN

DELETE FROM
	[dbo].template_column
WHERE 
	template_id = @TEMPLATE_ID

IF @@ERROR <> 0
BEGIN
ROLLBACK;
RETURN;
END
COMMIT




GO
/****** Object:  StoredProcedure [dbo].[sp_fin_wo_task]    Script Date: 2015-02-02 오후 7:04:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[sp_fin_wo_task]
	@ASSET_ID int
	,@PORT_NO int
	,@PORT_STATUS char(1) = '-'
	,@REMOTE_IC_ASSET_ID int
	,@REMOTE_PP_ASSET_ID int
	,@REMOTE_PORT_NO int

as

-- 작업 지시된 포트가 이벤트를 받아 처리

UPDATE
	asset_ipp_port_link
SET
	alarm_status = '-',
	wo_status = '-',
	ipp_port_status = @PORT_STATUS,
	remote_ic_asset_id = @REMOTE_IC_ASSET_ID,
	remote_pp_asset_id = @REMOTE_PP_ASSET_ID,
	remote_port_no = @REMOTE_PORT_NO
WHERE
	ipp_asset_id = @ASSET_ID AND
	port_no = @PORT_NO





GO
/****** Object:  StoredProcedure [dbo].[sp_find_outlet_in_asset_port_link]    Script Date: 2015-02-02 오후 7:04:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO








CREATE PROC [dbo].[sp_find_outlet_in_asset_port_link]
    @ASSET_ID INT
	,@PORT_NO INT = 1
	,@FOUND_ASSET_ID INT OUT
	,@FOUND_PORT_NO INT OUT
AS

DECLARE @FIND_ASSET_ID INT
DECLARE @FIND_PORT_NO INT
DECLARE @FIND_PLUG_SIDE CHAR(1)
DECLARE @FRONT_ASSET_ID INT
DECLARE @FRONT_PORT_NO INT
DECLARE @FRONT_PLUG_SIDE CHAR(1)
DECLARE @REAR_ASSET_ID INT
DECLARE @REAR_PORT_NO INT
DECLARE @REAR_PLUG_SIDE CHAR(1)

SELECT @FIND_ASSET_ID = asset_id FROM asset WHERE asset_id = @ASSET_ID
SET @FIND_PORT_NO = @PORT_NO
SET @FOUND_ASSET_ID = NULL
SET @FOUND_PORT_NO = NULL

IF (@FIND_ASSET_ID IS NULL)
BEGIN
    SELECT 'Asset does not found'
	RETURN
END

EXEC sp_get_first_asset @ASSET_ID, @PORT_NO, @FIND_ASSET_ID OUT, @FIND_PORT_NO OUT, @FIND_PLUG_SIDE OUT

WHILE (1=1)
BEGIN

	-- 카탈로그 그룹에서 Outlet(Face Plate and MUTOA Box)인 것만 찾는다.
	SELECT 
		@FOUND_ASSET_ID = a.asset_id, @FOUND_PORT_NO = @FIND_PORT_NO 
	FROM 
		asset a, catalog c, catalog_group cg
	WHERE 
		a.catalog_id = c.catalog_id AND
		c.catalog_group_id = cg.catalog_group_id AND
		a.asset_id = @FIND_ASSET_ID AND 
		(cg.catalog_group_id IN (3420, 3430) OR cg.level2_catalog_group_id IN (3420, 3430))

	IF (@FOUND_ASSET_ID IS NOT NULL)
	BEGIN
		RETURN
	END

	SELECT 
		@FRONT_ASSET_ID = front_asset_id
		,@FRONT_PORT_NO = front_port_no
		,@FRONT_PLUG_SIDE = front_plug_side
		,@REAR_ASSET_ID = rear_asset_id
		,@REAR_PORT_NO = rear_port_no
		,@REAR_PLUG_SIDE = rear_plug_side
	FROM 
		asset_port_link 
	WHERE 
		asset_id = @FIND_ASSET_ID and port_no = @FIND_PORT_NO

	IF (@FIND_PLUG_SIDE = 'F')
	BEGIN
		IF (@REAR_ASSET_ID IS NULL)
		BEGIN
			BREAK
		END
		SET @FIND_ASSET_ID = @REAR_ASSET_ID
		SET @FIND_PORT_NO = @REAR_PORT_NO
		SET @FIND_PLUG_SIDE = @REAR_PLUG_SIDE
	END
	ELSE
	BEGIN
		IF (@FRONT_ASSET_ID IS NULL)
		BEGIN
			BREAK
		END
		SET @FIND_ASSET_ID = @FRONT_ASSET_ID
		SET @FIND_PORT_NO = @FRONT_PORT_NO
		SET @FIND_PLUG_SIDE = @FRONT_PLUG_SIDE
	END
END





GO
/****** Object:  StoredProcedure [dbo].[sp_find_port_link_info]    Script Date: 2015-02-02 오후 7:04:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO






CREATE PROC [dbo].[sp_find_port_link_info] 
	@ASSET_ID INT
	,@PORT_NO INT
	,@FIRST_ASSET_ID INT OUT
	,@FIRST_PORT_NO INT OUT
	,@OUTLET_ASSET_ID INT OUT
	,@OUTLET_PORT_NO INT OUT
	,@OUTLET_CABLE_CATALOG_ID INT OUT
	,@LAST_ASSET_ID INT OUT
	,@LAST_PORT_NO INT OUT
AS

DECLARE @FIND_ASSET_ID INT
DECLARE @FIND_PORT_NO INT
DECLARE @FIND_PLUG_SIDE CHAR(1)
DECLARE @FIND_CABLE_CATALOG_ID INT
DECLARE @FRONT_ASSET_ID INT
DECLARE @FRONT_PORT_NO INT
DECLARE @FRONT_PLUG_SIDE CHAR(1)
DECLARE @FRONT_CABLE_CATALOG_ID INT
DECLARE @REAR_ASSET_ID INT
DECLARE @REAR_PORT_NO INT
DECLARE @REAR_PLUG_SIDE CHAR(1)
DECLARE @REAR_CABLE_CATALOG_ID INT
DECLARE @CUR_ASSET_ID INT
DECLARE @CUR_PORT_NO INT

SELECT @FIND_ASSET_ID = asset_id FROM asset WHERE asset_id = @ASSET_ID
SET @FIND_PORT_NO = @PORT_NO

IF (@FIND_ASSET_ID IS NULL)
BEGIN
    SELECT 'Asset does not found'
	RETURN
END

EXEC sp_get_first_asset @ASSET_ID, @PORT_NO, @FIRST_ASSET_ID OUT, @FIRST_PORT_NO OUT, @FIND_PLUG_SIDE OUT

SET @FIND_ASSET_ID = @FIRST_ASSET_ID
SET @FIND_PORT_NO = @FIRST_PORT_NO

WHILE (1=1)
BEGIN

	SELECT 
		@FRONT_ASSET_ID = front_asset_id 
		,@FRONT_PORT_NO = front_port_no
		,@FRONT_PLUG_SIDE = front_plug_side
		,@FRONT_CABLE_CATALOG_ID = front_cable_catalog_id
		,@REAR_ASSET_ID = rear_asset_id
		,@REAR_PORT_NO = rear_port_no
		,@REAR_PLUG_SIDE = rear_plug_side
		,@REAR_CABLE_CATALOG_ID = rear_cable_catalog_id
		,@CUR_PORT_NO = port_no
	FROM 
		asset_port_link 
	WHERE 
		asset_id = @FIND_ASSET_ID and port_no = @FIND_PORT_NO

	-- 카탈로그 그룹에서 Outlet(Face Plate and MUTOA Box)인 것만 찾는다.
	SET @CUR_ASSET_ID = NULL
	SELECT 
		@CUR_ASSET_ID = a.asset_id
	FROM 
		asset a, catalog c, catalog_group cg
	WHERE 
		a.catalog_id = c.catalog_id AND
		c.catalog_group_id = cg.catalog_group_id AND
		a.asset_id = @FIND_ASSET_ID AND 
		(cg.catalog_group_id IN (3420, 3430) OR cg.level2_catalog_group_id IN (3420, 3430))

	-- 현재 자산이 아울렛인 경우
	IF (@CUR_ASSET_ID IS NOT NULL)
	BEGIN
		SET @OUTLET_ASSET_ID = @CUR_ASSET_ID
		SET @OUTLET_PORT_NO = @CUR_PORT_NO

		--SELECT '아울렛 포트 지정---->', 
		--	@OUTLET_ASSET_ID as '아울렛자산ID'
		--	,(SELECT asset_name FROM asset WHERE asset_id = @OUTLET_ASSET_ID) as '아울렛자산명'
		--	,@OUTLET_PORT_NO as '아울렛포트번호'

		IF (@FIND_PLUG_SIDE = 'F')
		BEGIN
			SET @OUTLET_CABLE_CATALOG_ID = @REAR_CABLE_CATALOG_ID
		END
		ELSE
		BEGIN
			SET @OUTLET_CABLE_CATALOG_ID = @FRONT_CABLE_CATALOG_ID
		END

	END

	--IF  (@PORT_NO = 7)
	--BEGIN
	--	select '분석---', 
	--		@ASSET_ID as '스위치자산ID', 
	--		@PORT_NO as '스위치포트번호', 
	--		@FIND_ASSET_ID as '현자산ID', 
	--		(SELECT asset_name FROM asset WHERE asset_id = @FIND_ASSET_ID) as '현자산명', 
	--		@FIND_PORT_NO as '현포트번호', 
	--		@FRONT_ASSET_ID as '전면자산ID', 
	--		(SELECT asset_name FROM asset WHERE asset_id = @FRONT_ASSET_ID) as '전면자산명', 
	--		@FRONT_PORT_NO as '전면포트번호', 
	--		@REAR_ASSET_ID as '후면자산ID', 
	--		(SELECT asset_name FROM asset WHERE asset_id = @REAR_ASSET_ID) as '후면자산명', 
	--		@REAR_PORT_NO as '후면포트번호', 
	--		@FIND_PLUG_SIDE as '플러그사이드'
	--END

	IF (@FIND_PLUG_SIDE = 'F')
	BEGIN

		IF (@REAR_ASSET_ID IS NULL)
		BEGIN
			SET @LAST_ASSET_ID = @FIND_ASSET_ID
			SET @LAST_PORT_NO = @FIND_PORT_NO
			BREAK
		END

		SET @FIND_ASSET_ID = @REAR_ASSET_ID
		SET @FIND_PORT_NO = @REAR_PORT_NO
		SET @FIND_PLUG_SIDE = @REAR_PLUG_SIDE
	END
	ELSE
	BEGIN
		IF (@FRONT_ASSET_ID IS NULL)
		BEGIN
			SET @LAST_ASSET_ID = @FIND_ASSET_ID
			SET @LAST_PORT_NO = @FIND_PORT_NO
			BREAK
		END

		SET @FIND_ASSET_ID = @FRONT_ASSET_ID
		SET @FIND_PORT_NO = @FRONT_PORT_NO
		SET @FIND_PLUG_SIDE = @FRONT_PLUG_SIDE
	END
END






GO
/****** Object:  StoredProcedure [dbo].[sp_get_first_asset]    Script Date: 2015-02-02 오후 7:04:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE proc [dbo].[sp_get_first_asset] 
	@ASSET_ID INT
	,@PORT_NO INT
	,@FIND_ASSET_ID INT OUT
	,@FIND_PORT_NO INT OUT
	,@FIND_PLUG_SIDE CHAR(1) OUT
AS

DECLARE @FRONT_ASSET_ID INT
DECLARE @FRONT_PORT_NO INT
DECLARE @FRONT_PLUG_SIDE CHAR(1)
DECLARE @REAR_ASSET_ID INT
DECLARE @REAR_PORT_NO INT
DECLARE @REAR_PLUG_SIDE CHAR(1)

EXEC sp_get_plug_side_for_last @ASSET_ID, @PORT_NO, @FIND_PLUG_SIDE OUT

SET @FIND_ASSET_ID = @ASSET_ID
SET @FIND_PORT_NO = @PORT_NO

-- Find First
WHILE (1=1)
BEGIN
	SELECT 
		@FRONT_ASSET_ID = front_asset_id
		,@FRONT_PORT_NO = front_port_no
		,@FRONT_PLUG_SIDE = front_plug_side
		,@REAR_ASSET_ID = rear_asset_id
		,@REAR_PORT_NO = rear_port_no
		,@REAR_PLUG_SIDE = rear_plug_side
	FROM 
		asset_port_link 
	WHERE 
		asset_id = @FIND_ASSET_ID and 
		port_no = @FIND_PORT_NO

	IF (@FIND_PLUG_SIDE = 'F')
	BEGIN
		IF (@REAR_ASSET_ID IS NULL)
		BEGIN
			SET @FIND_PLUG_SIDE = @FRONT_PLUG_SIDE
			BREAK
		END
		SET @FIND_ASSET_ID = @REAR_ASSET_ID
		SET @FIND_PORT_NO = @REAR_PORT_NO
		SET @FIND_PLUG_SIDE = @REAR_PLUG_SIDE
	END
	ELSE
	BEGIN
		IF (@FRONT_ASSET_ID IS NULL)
		BEGIN
			SET @FIND_PLUG_SIDE = @REAR_PLUG_SIDE
			BREAK
		END
		SET @FIND_ASSET_ID = @FRONT_ASSET_ID
		SET @FIND_PORT_NO = @FRONT_PORT_NO
		SET @FIND_PLUG_SIDE = @FRONT_PLUG_SIDE
	END
END

RETURN








GO
/****** Object:  StoredProcedure [dbo].[sp_get_plug_side_for_last]    Script Date: 2015-02-02 오후 7:04:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROC [dbo].[sp_get_plug_side_for_last]
	@FIND_ASSET_ID INT
	,@FIND_PORT_NO INT
	,@FIND_PORT_SIDE CHAR(1) OUT
AS
	-- 스위치인경우 Rear?
	SET @FIND_PORT_SIDE = 'F'






GO
/****** Object:  StoredProcedure [dbo].[sp_get_terminal_cable]    Script Date: 2015-02-02 오후 7:04:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_get_terminal_cable]
	@OUTLET_CABLE_CATALOG_ID INT,
	@CABLE_CATALOG_ID INT OUT
AS	
	SET @CABLE_CATALOG_ID = 446001




GO
/****** Object:  StoredProcedure [dbo].[sp_list_asset]    Script Date: 2015-02-02 오후 7:04:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[sp_list_asset]
    @ASSET_NAME VARCHAR(80) = NULL
AS

IF (@ASSET_NAME IS NULL)
BEGIN
    SELECT 
		a.asset_id, 
		a.asset_name, 
		cg.catalog_group_id, 
		cg.catalog_group_name, 
		c.catalog_id, 
		c.catalog_name, 
		l.location_id, 
		l.location_name, 
		serial_no, 
		ipv4, 
		ipv6, 
		install_user_name, 
		u.user_id, 
		u.user_name, 
		is_layout, 
		pos_x, 
		pos_y
	FROM 
		asset a, 
		catalog c, 
		catalog_group cg, 
		[user] u, 
		location l
	WHERE 
		a.catalog_id = c.catalog_id AND
		c.catalog_group_id = cg.catalog_group_id AND
		a.user_id = u.user_id AND
		a.location_id = l.location_id
	RETURN
END

SELECT * FROM asset WHERE asset_name LIKE @ASSET_NAME + '%'






GO
/****** Object:  StoredProcedure [dbo].[sp_list_asset_port_link]    Script Date: 2015-02-02 오후 7:04:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO











CREATE PROC [dbo].[sp_list_asset_port_link]
    @ASSET_NAME VARCHAR(80)
	,@PORT_NO INT = 1
AS

DECLARE @FIND_ASSET_ID INT
DECLARE @FIND_PORT_NO INT
DECLARE @FIND_PLUG_SIDE CHAR(1)
DECLARE @FIND2_ASSET_ID INT
DECLARE @FIND2_PORT_NO INT
DECLARE @FRONT_ASSET_ID INT
DECLARE @FRONT_PORT_NO INT
DECLARE @FRONT_PLUG_SIDE CHAR(1)
DECLARE @REAR_ASSET_ID INT
DECLARE @REAR_PORT_NO INT
DECLARE @REAR_PLUG_SIDE CHAR(1)

SELECT @FIND2_ASSET_ID = asset_id FROM asset WHERE asset_name = @ASSET_NAME
SET @FIND2_PORT_NO = @PORT_NO

IF (@FIND2_ASSET_ID IS NULL)
BEGIN
    SELECT 'Asset name does not found'
	RETURN
END

EXEC sp_get_first_asset @FIND2_ASSET_ID, @FIND2_PORT_NO, @FIND_ASSET_ID OUT, @FIND_PORT_NO OUT, @FIND_PLUG_SIDE OUT

WHILE (1=1)
BEGIN
	SELECT 
		a.asset_id, 
		a.asset_name, 
		apl.port_no, 
		c.catalog_id, 
		c.catalog_name, 
		l.location_id, 
		l.location_name, 
		apl.front_cable_catalog_id,
		(SELECT TOP 1 catalog_name FROM catalog WHERE apl.front_cable_catalog_id = catalog_id) AS front_cable_catalog_name,
		(SELECT TOP 1 ipp_port_status FROM asset_ipp_port_link WHERE ipp_asset_id = @FIND_ASSET_ID AND port_no = @FIND_PORT_NO) AS ipp_port_status,
		(SELECT TOP 1 alarm_status FROM asset_ipp_port_link WHERE ipp_asset_id = @FIND_ASSET_ID AND port_no = @FIND_PORT_NO) AS alarm_status,
		(SELECT TOP 1 wo_status FROM asset_ipp_port_link WHERE ipp_asset_id = @FIND_ASSET_ID AND port_no = @FIND_PORT_NO) AS wo_status
	FROM 
		asset a, catalog c, location l, asset_port_link apl
	WHERE 
		a.catalog_id = c.catalog_id AND 
		a.location_id = l.location_id AND
		a.asset_id = apl.asset_id AND
		a.asset_id = @FIND_ASSET_ID AND
		apl.port_no = @FIND_PORT_NO

	IF (@@ROWCOUNT = 0)
	BEGIN
		BREAK
	END

	SELECT 
		@FRONT_ASSET_ID = front_asset_id
		,@FRONT_PORT_NO = front_port_no
		,@FRONT_PLUG_SIDE = front_plug_side
		,@REAR_ASSET_ID = rear_asset_id
		,@REAR_PORT_NO = rear_port_no
		,@REAR_PLUG_SIDE = rear_plug_side
	FROM 
		asset_port_link 
	WHERE 
		asset_id = @FIND_ASSET_ID and port_no = @FIND_PORT_NO

	IF (@FIND_PLUG_SIDE = 'F')
	BEGIN
		IF (@REAR_ASSET_ID IS NULL)
		BEGIN
			BREAK
		END
		SET @FIND_ASSET_ID = @REAR_ASSET_ID
		SET @FIND_PORT_NO = @REAR_PORT_NO
		SET @FIND_PLUG_SIDE = @REAR_PLUG_SIDE
	END
	ELSE
	BEGIN
		IF (@FRONT_ASSET_ID IS NULL)
		BEGIN
			BREAK
		END

		SET @FIND_ASSET_ID = @FRONT_ASSET_ID
		SET @FIND_PORT_NO = @FRONT_PORT_NO
		SET @FIND_PLUG_SIDE = @FRONT_PLUG_SIDE
	END
END















GO
/****** Object:  StoredProcedure [dbo].[sp_list_asset_terminal]    Script Date: 2015-02-02 오후 7:04:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
	create proc [dbo].[sp_list_asset_terminal] 
	as

	select * from asset_terminal at left outer join asset_terminal_ip ati on at.terminal_id = ati.terminal_id




GO
/****** Object:  StoredProcedure [dbo].[sp_list_asset_tree]    Script Date: 2015-02-02 오후 7:04:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE PROC [dbo].[sp_list_asset_tree]
AS

	DECLARE @FIND_ID INT

    SELECT 
		asset_tree_id, 
		disp_name,
		disp_level,
		is_alarm,
		image_id,
		(SELECT image_name FROM image WHERE image_id = at.image_id) AS image_name,
		asset_id,
		(SELECT asset_name FROM asset WHERE asset_id = at.asset_id) AS asset_name,
		location_id,
		(SELECT location_name FROM location WHERE location_id = at.location_id) AS location_name,
		prev_asset_tree_id,
		next_asset_tree_id
	FROM 
		asset_tree at 

	-- Find First
	SELECT TOP 1 @FIND_ID = asset_tree_id FROM asset_tree WHERE prev_asset_tree_id IS NULL

	WHILE (1=1)
	BEGIN
		SELECT 
			asset_tree_id, 
			disp_name,
			disp_level,
			is_alarm,
			image_id,
			(SELECT image_name FROM image WHERE image_id = at.image_id) AS image_name,
			asset_id,
			(SELECT asset_name FROM asset WHERE asset_id = at.asset_id) AS asset_name,
			location_id,
			(SELECT location_name FROM location WHERE location_id = at.location_id) AS location_name,
			prev_asset_tree_id,
			next_asset_tree_id
		FROM 
			asset_tree at 
		WHERE
			asset_tree_id = @FIND_ID

		SELECT @FIND_ID = next_asset_tree_id FROM asset_tree WHERE asset_tree_id = @FIND_ID

		IF (@FIND_ID IS NULL)
		BEGIN
			BREAK
		END
	END




GO
/****** Object:  StoredProcedure [dbo].[sp_list_cable_catalog]    Script Date: 2015-02-02 오후 7:04:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROC [dbo].[sp_list_cable_catalog]
AS

SELECT 
	catalog_id, 
	catalog_name,
	cg.catalog_group_id,
	cg.catalog_group_name,
	ca_disp_name,
	c.deletable,
	ca_use_intelligent,
	ca_install_type,
	ca_for_army,
	ca_media_type,
	ca_is_utp_shield,
	ca_utp_cable_type,
	ca_fiber_cable_type,
	ca_fiber_connector_type,
	ca_disp_color_rgb
FROM 
	catalog c, 
	catalog_group cg
WHERE 
	c.catalog_group_id = cg.catalog_group_id AND
	((cg.catalog_group_id IN (3150, 3460, 3470)) OR
	 (cg.level2_catalog_group_id IN (3150, 3460, 3470)))




GO
/****** Object:  StoredProcedure [dbo].[sp_list_catalog]    Script Date: 2015-02-02 오후 7:04:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[sp_list_catalog]
    @CATALOG_NAME VARCHAR(80) = NULL
AS

IF (@CATALOG_NAME IS NULL)
BEGIN
    SELECT 
		*
	FROM 
		catalog c, 
		catalog_group cg
	WHERE 
		c.catalog_group_id = cg.catalog_group_id 
	RETURN
END

SELECT 
	*
FROM 
	catalog c, 
	catalog_group cg
WHERE 
	c.catalog_group_id = cg.catalog_group_id AND
	c.catalog_name LIKE @CATALOG_NAME + '%'






GO
/****** Object:  StoredProcedure [dbo].[sp_list_catalog_group]    Script Date: 2015-02-02 오후 7:04:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[sp_list_catalog_group]
    @CATALOG_GROUP_NAME VARCHAR(80) = NULL
AS

IF (@CATALOG_GROUP_NAME IS NULL)
BEGIN
    SELECT 
		catalog_group_id,
		catalog_group_name,
		catalog_level,
		level1_catalog_group_id,
		(SELECT catalog_group_name FROM catalog_group WHERE catalog_group_id = cg.level1_catalog_group_id) AS level1_catalog_group_name,
		level2_catalog_group_id,
		(SELECT catalog_group_name FROM catalog_group WHERE catalog_group_id = cg.level2_catalog_group_id) AS level2_catalog_group_name,
		enable,
		deletable,
		prev_catalog_group_id,
		next_catalog_group_id
	FROM 
		catalog_group cg
	RETURN
END

SELECT 
		catalog_group_id,
		catalog_group_name,
		catalog_level,
		level1_catalog_group_id,
		(SELECT catalog_group_name FROM catalog_group WHERE catalog_group_id = cg.level1_catalog_group_id) AS level1_catalog_group_name,
		level2_catalog_group_id,
		(SELECT catalog_group_name FROM catalog_group WHERE catalog_group_id = cg.level2_catalog_group_id) AS level2_catalog_group_name,
		enable,
		deletable,
		prev_catalog_group_id,
		next_catalog_group_id
FROM 
	catalog_group cg
WHERE 
	cg.catalog_group_name LIKE @CATALOG_GROUP_NAME + '%'






GO
/****** Object:  StoredProcedure [dbo].[sp_list_event]    Script Date: 2015-02-02 오후 7:04:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[sp_list_event]
as

SELECT
	e.event_id, event_type, event_group, event_format, popup_screen, send_email, send_sms, event_desc, l.lang_id, lang_name, remarks
FROM
	event e, event_lang el, language l 
WHERE
	e.event_id = el.event_id AND
	el.lang_id = l.lang_id




GO
/****** Object:  StoredProcedure [dbo].[sp_list_event_hist]    Script Date: 2015-02-02 오후 7:04:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[sp_list_event_hist]
as
select * from event_hist




GO
/****** Object:  StoredProcedure [dbo].[sp_list_image]    Script Date: 2015-02-02 오후 7:04:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[sp_list_image]
as
SELECT
    i.image_id,
	i.image_name,
	i.file_name,
	it.image_type_id,
	it.image_type_name,
	i.size_x,
	i.size_y,
	i.drawing_3d_id,
	(SELECT file_name FROM drawing_3d WHERE drawing_3d_id = i.drawing_3d_id) AS drawing_3d_file_name,
	i.deletable,
	i.remarks,
	it.folder_name,
	it.size_x AS recomend_size_x,
	it.size_y AS recomend_size_y
FROM
	image i, image_type it
WHERE 
	i.image_type_id = it.image_type_id




GO
/****** Object:  StoredProcedure [dbo].[sp_list_image_type]    Script Date: 2015-02-02 오후 7:04:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_list_image_type]
as
SELECT
    *
FROM
	image_type




GO
/****** Object:  StoredProcedure [dbo].[sp_list_location]    Script Date: 2015-02-02 오후 7:04:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO






CREATE PROC [dbo].[sp_list_location]
AS

BEGIN

	DECLARE @FIND_LOC_ID INT

	SELECT 
		location_id, 
		location_name,
		location_level,
		location_path,
		region1_id,
		(SELECT region1_name FROM region1 WHERE region1_id = l.region1_id) AS region1_name,
		region2_id,
		(SELECT region2_name FROM region2 WHERE region2_id = l.region2_id) AS region2_name,
		site_id,
		(SELECT site_name FROM site WHERE site_id = l.site_id) AS site_name,
		building_id,
		(SELECT building_name FROM building WHERE building_id = l.building_id) AS building_name,
		floor_id,
		(SELECT floor_name FROM floor WHERE floor_id = l.floor_id) AS floor_name,
		room_id,
		(SELECT room_name FROM room WHERE room_id = l.room_id) AS room_name,
		rack_id,
		(SELECT rack_name FROM rack WHERE rack_id = l.rack_id) AS rack_name,
		prev_location_id,
		next_location_id
	FROM 
		location l

	-- Find First
	SELECT TOP 1 @FIND_LOC_ID = location_id FROM location WHERE prev_location_id IS NULL

	WHILE (1=1)
	BEGIN
		SELECT 
			location_id, 
			location_name,
			location_level,
			location_path,
			region1_id,
			(SELECT region1_name FROM region1 WHERE region1_id = l.region1_id) AS region1_name,
			region2_id,
			(SELECT region2_name FROM region2 WHERE region2_id = l.region2_id) AS region2_name,
			site_id,
			(SELECT site_name FROM site WHERE site_id = l.site_id) AS site_name,
			building_id,
			(SELECT building_name FROM building WHERE building_id = l.building_id) AS building_name,
			floor_id,
			(SELECT floor_name FROM floor WHERE floor_id = l.floor_id) AS floor_name,
			room_id,
			(SELECT room_name FROM room WHERE room_id = l.room_id) AS room_name,
			rack_id,
			(SELECT rack_name FROM rack WHERE rack_id = l.rack_id) AS rack_name,
			prev_location_id,
			next_location_id
		FROM 
			location l
		WHERE
			location_id = @FIND_LOC_ID

		SELECT @FIND_LOC_ID = next_location_id FROM location WHERE location_id = @FIND_LOC_ID

		IF (@FIND_LOC_ID IS NULL)
		BEGIN
			BREAK
		END
	END
END






GO
/****** Object:  StoredProcedure [dbo].[sp_list_rack]    Script Date: 2015-02-02 오후 7:04:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[sp_list_rack]
as
SELECT
    s.site_id, 
	s.site_name, 
	b.building_id,
	b.building_name,
	f.floor_id,
	f.floor_name,
	r.room_id,
	r.room_name,
	r2.rack_id,
	r2.rack_name,
	(SELECT '   ') AS ___,
	s.site_image_id, 
	(SELECT image_name FROM image WHERE image_id = s.site_image_id) AS site_image_name,
	b.building_image_id, 
	(SELECT image_name FROM image WHERE image_id = b.building_image_id) AS building_image_name,
	f.floor_no, 
	f.drawing_3d_id,
	(SELECT file_name FROM drawing_3d WHERE drawing_3d_id = f.drawing_3d_id) AS drawing_3d_file_name,
	r.square_x1, 
	r.square_y1, 
	r.square_x2, 
	r.square_y2, 
	r2.rack_no,
	r2.rack_row,
	r2.pos_x,
	r2.pos_y,
	r2.angle,
	r2.rack_catalog_id,
	(SELECT catalog_name FROM catalog WHERE catalog_id = r2.rack_catalog_id) AS rack_catalog_name,
	r2.vcm_l_catalog_id,
	(SELECT catalog_name FROM catalog WHERE catalog_id = r2.vcm_l_catalog_id) AS vcm_l_catalog_name,
	r2.vcm_r_catalog_id,
	(SELECT catalog_name FROM catalog WHERE catalog_id = r2.vcm_r_catalog_id) AS vcm_r_catalog_name
FROM
	site s
LEFT OUTER JOIN building b
	ON s.site_id = b.site_id
LEFT OUTER JOIN floor f
	ON b.building_id = f.building_id
LEFT OUTER JOIN room r
	ON f.floor_id = r.floor_id
LEFT OUTER JOIN rack r2
	ON r.room_id = r2.room_id
	





GO
/****** Object:  StoredProcedure [dbo].[sp_list_site]    Script Date: 2015-02-02 오후 7:04:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_list_site]
as
SELECT
    r1.region1_id, r1.region1_name, r2.region2_id, r2.region2_name, s.site_id, s.site_name, 
	(SELECT '   ') AS ___,
	r1.region1_image_id, 
	(SELECT image_name FROM image WHERE image_id = r1.region1_image_id) AS region1_image_name,
	r1.pos_x AS r1_pos_x, r1.pos_y AS r1_pos_y, 
	r2.pos_x AS r2_pos_x, r2.pos_y AS r2_pos_y,
	s.site_image_id, 
	(SELECT image_name FROM image WHERE image_id = s.site_image_id) AS site_image_name
FROM
	region1 r1 
LEFT OUTER JOIN region2 r2
	ON r1.region1_id = r2.region1_id
LEFT OUTER JOIN site s
	ON r2.region2_id = s.region2_id





GO
/****** Object:  StoredProcedure [dbo].[sp_list_unlocated_terminal]    Script Date: 2015-02-02 오후 7:04:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_list_unlocated_terminal]
as

select * from asset_terminal at
inner join asset a on at.terminal_asset_id = a.asset_id
where location_id = 0

GO
/****** Object:  StoredProcedure [dbo].[sp_set_alarm]    Script Date: 2015-02-02 오후 7:04:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_set_alarm]
	@ASSET_ID int
	,@PORT_NO int
	,@PORT_STATUS char(1) = '-'
AS
UPDATE
	asset_ipp_port_link
SET
	alarm_status = 'U',
	wo_status = '-',
	ipp_port_status = @PORT_STATUS
WHERE
	ipp_asset_id = @ASSET_ID AND
	port_no = @PORT_NO




GO
/****** Object:  StoredProcedure [dbo].[sp_start_net_scan]    Script Date: 2015-02-02 오후 7:04:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[sp_start_net_scan]
as

UPDATE 
	asset_terminal
SET
	new_enable = 'N'

UPDATE 
	asset_terminal_ip
SET
	new_enable = 'N'





GO
/****** Object:  StoredProcedure [dbo].[sp_stat_eb_day]    Script Date: 2015-02-02 오후 7:04:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[sp_stat_eb_day]
	@LOC_ID INT = 0,
	@YEAR INT = 0,
	@MONTH INT = 0
AS
BEGIN
	DECLARE @LOCATION_LEVEL INT 
	DECLARE @SITE_ID INT
	DECLARE @BUILDING_ID INT
	DECLARE @FLOOR_ID INT
	DECLARE @ROOM_ID INT

	IF @LOC_ID = 0
	BEGIN
		-- ----------------------
		-- 전체/해당년에 대한 검색
		-- ----------------------
		IF @MONTH = 0
		BEGIN
			SELECT
				DATEPART(dd, t.date) as day,
				SUM(power_v) / SUM(power_v_cnt) as voltage,
				SUM(power_i) / SUM(power_i_cnt) as current2,
				SUM(power_p) / SUM(power_p_cnt) as power,
				SUM(power_ph) / SUM(power_ph_cnt) as powerh,
				SUM(sensor_t) / SUM(sensor_t_cnt) as temperature,
				SUM(sensor_h) / SUM(sensor_h_cnt) as humidity,
				SUM(door) as door,
				MAX(power_peek_v) as voltage_peek,
				MAX(power_peek_i) as current_peek,
				MAX(power_peek_p) as power_peek,
				MAX(power_peek_ph) as powerh_peek,
				MAX(sensor_peek_t) as temperature_peek,
				MAX(sensor_peek_h) as humidity_peek
			FROM
				eb_port_data_hour t
			WHERE
				DATEPART(yy, t.date) = @YEAR
			GROUP BY
				DATEPART(dd, t.date)
			RETURN	
		END

		-- ----------------------
		-- 전체/해당월에 대한 검색
		-- ----------------------

		ELSE
		BEGIN
			SELECT
				DATEPART(dd, t.date) as day,
				SUM(power_v) / SUM(power_v_cnt) as voltage,
				SUM(power_i) / SUM(power_i_cnt) as current2,
				SUM(power_p) / SUM(power_p_cnt) as power,
				SUM(power_ph) / SUM(power_ph_cnt) as powerh,
				SUM(sensor_t) / SUM(sensor_t_cnt) as temperature,
				SUM(sensor_h) / SUM(sensor_h_cnt) as humidity,
				SUM(door) as door,
				MAX(power_peek_v) as voltage_peek,
				MAX(power_peek_i) as current_peek,
				MAX(power_peek_p) as power_peek,
				MAX(power_peek_ph) as powerh_peek,
				MAX(sensor_peek_t) as temperature_peek,
				MAX(sensor_peek_h) as humidity_peek
			FROM
				eb_port_data_hour t
			WHERE
				DATEPART(yy, t.date) = @YEAR AND
				DATEPART(mm, t.date) = @MONTH
			GROUP BY
				DATEPART(dd, t.date)
			RETURN
		END
	END
	ELSE
	BEGIN
		SELECT 
			@LOCATION_LEVEL = location_level, 
			@SITE_ID = site_id, 
			@BUILDING_ID = building_id, 
			@FLOOR_ID = floor_id,
			@ROOM_ID = room_id
		FROM 
			location 
		WHERE 
			location_id = @LOC_ID 

		IF @LOCATION_LEVEL IS NULL
			RETURN

		-- ----------------------
		-- 해당년에 대한 검색
		-- ----------------------
		IF @MONTH = 0
		BEGIN
			IF @LOCATION_LEVEL = 3
			BEGIN

				-- 사이트로 검색
				SELECT
					DATEPART(dd, t.date) as day,
					SUM(power_v) / SUM(power_v_cnt) as voltage,
					SUM(power_i) / SUM(power_i_cnt) as current2,
					SUM(power_p) / SUM(power_p_cnt) as power,
					SUM(power_ph) / SUM(power_ph_cnt) as powerh,
					SUM(sensor_t) / SUM(sensor_t_cnt) as temperature,
					SUM(sensor_h) / SUM(sensor_h_cnt) as humidity,
					SUM(door) as door,
					MAX(power_peek_v) as voltage_peek,
					MAX(power_peek_i) as current_peek,
					MAX(power_peek_p) as power_peek,
					MAX(power_peek_ph) as powerh_peek,
					MAX(sensor_peek_t) as temperature_peek,
					MAX(sensor_peek_h) as humidity_peek
				FROM
					eb_port_data_hour t, location l, asset a
				WHERE
					t.asset_id = a.asset_id AND
					a.location_id = l.location_id AND
					l.site_id = @SITE_ID AND
					DATEPART(yy, t.date) = @YEAR
				GROUP BY
					DATEPART(dd, t.date)
			END
			IF @LOCATION_LEVEL = 4
			BEGIN
				-- 빌딩으로 검색
				SELECT
					DATEPART(dd, t.date) as day,
					SUM(power_v) / SUM(power_v_cnt) as voltage,
					SUM(power_i) / SUM(power_i_cnt) as current2,
					SUM(power_p) / SUM(power_p_cnt) as power,
					SUM(power_ph) / SUM(power_ph_cnt) as powerh,
					SUM(sensor_t) / SUM(sensor_t_cnt) as temperature,
					SUM(sensor_h) / SUM(sensor_h_cnt) as humidity,
					SUM(door) as door,
					MAX(power_peek_v) as voltage_peek,
					MAX(power_peek_i) as current_peek,
					MAX(power_peek_p) as power_peek,
					MAX(power_peek_ph) as powerh_peek,
					MAX(sensor_peek_t) as temperature_peek,
					MAX(sensor_peek_h) as humidity_peek
				FROM
					eb_port_data_hour t, location l, asset a
				WHERE
					t.asset_id = a.asset_id AND
					a.location_id = l.location_id AND
					l.building_id = @BUILDING_ID AND
					DATEPART(yy, t.date) = @YEAR
				GROUP BY
					DATEPART(dd, t.date)
			END
			IF @LOCATION_LEVEL = 5
			BEGIN
				-- 층으로 검색
				SELECT
					DATEPART(dd, t.date) as day,
					SUM(power_v) / SUM(power_v_cnt) as voltage,
					SUM(power_i) / SUM(power_i_cnt) as current2,
					SUM(power_p) / SUM(power_p_cnt) as power,
					SUM(power_ph) / SUM(power_ph_cnt) as powerh,
					SUM(sensor_t) / SUM(sensor_t_cnt) as temperature,
					SUM(sensor_h) / SUM(sensor_h_cnt) as humidity,
					SUM(door) as door,
					MAX(power_peek_v) as voltage_peek,
					MAX(power_peek_i) as current_peek,
					MAX(power_peek_p) as power_peek,
					MAX(power_peek_ph) as powerh_peek,
					MAX(sensor_peek_t) as temperature_peek,
					MAX(sensor_peek_h) as humidity_peek
				FROM
					eb_port_data_hour t, location l, asset a
				WHERE
					t.asset_id = a.asset_id AND
					a.location_id = l.location_id AND
					l.floor_id = @FLOOR_ID AND
					DATEPART(yy, t.date) = @YEAR
				GROUP BY
					DATEPART(dd, t.date)
			END
			IF @LOCATION_LEVEL = 6
			BEGIN
				-- 룸으로 검색
				SELECT
					DATEPART(dd, t.date) as day,
					SUM(power_v) / SUM(power_v_cnt) as voltage,
					SUM(power_i) / SUM(power_i_cnt) as current2,
					SUM(power_p) / SUM(power_p_cnt) as power,
					SUM(power_ph) / SUM(power_ph_cnt) as powerh,
					SUM(sensor_t) / SUM(sensor_t_cnt) as temperature,
					SUM(sensor_h) / SUM(sensor_h_cnt) as humidity,
					SUM(door) as door,
					MAX(power_peek_v) as voltage_peek,
					MAX(power_peek_i) as current_peek,
					MAX(power_peek_p) as power_peek,
					MAX(power_peek_ph) as powerh_peek,
					MAX(sensor_peek_t) as temperature_peek,
					MAX(sensor_peek_h) as humidity_peek
				FROM
					eb_port_data_hour t, location l, asset a
				WHERE
					t.asset_id = a.asset_id AND
					a.location_id = l.location_id AND
					l.room_id = @ROOM_ID AND
					DATEPART(yy, t.date) = @YEAR
				GROUP BY
					DATEPART(dd, t.date)
			END
		END

		-- ----------------------
		-- 해당월에 대한 검색
		-- ----------------------
		ELSE
		BEGIN
			IF @LOCATION_LEVEL = 3
			BEGIN
				-- 사이트로 검색
				SELECT
					DATEPART(dd, t.date) as day,
					SUM(power_v) / SUM(power_v_cnt) as voltage,
					SUM(power_i) / SUM(power_i_cnt) as current2,
					SUM(power_p) / SUM(power_p_cnt) as power,
					SUM(power_ph) / SUM(power_ph_cnt) as powerh,
					SUM(sensor_t) / SUM(sensor_t_cnt) as temperature,
					SUM(sensor_h) / SUM(sensor_h_cnt) as humidity,
					SUM(door) as door,
					MAX(power_peek_v) as voltage_peek,
					MAX(power_peek_i) as current_peek,
					MAX(power_peek_p) as power_peek,
					MAX(power_peek_ph) as powerh_peek,
					MAX(sensor_peek_t) as temperature_peek,
					MAX(sensor_peek_h) as humidity_peek
				FROM
					eb_port_data_hour t, location l, asset a
				WHERE
					t.asset_id = a.asset_id AND
					a.location_id = l.location_id AND
					l.site_id = @SITE_ID AND
					DATEPART(yy, t.date) = @YEAR AND
					DATEPART(mm, t.date) = @MONTH
				GROUP BY
					DATEPART(dd, t.date)
			END
			IF @LOCATION_LEVEL = 4
			BEGIN
				-- 빌딩으로 검색
				SELECT
					DATEPART(dd, t.date) as day,
					SUM(power_v) / SUM(power_v_cnt) as voltage,
					SUM(power_i) / SUM(power_i_cnt) as current2,
					SUM(power_p) / SUM(power_p_cnt) as power,
					SUM(power_ph) / SUM(power_ph_cnt) as powerh,
					SUM(sensor_t) / SUM(sensor_t_cnt) as temperature,
					SUM(sensor_h) / SUM(sensor_h_cnt) as humidity,
					SUM(door) as door,
					MAX(power_peek_v) as voltage_peek,
					MAX(power_peek_i) as current_peek,
					MAX(power_peek_p) as power_peek,
					MAX(power_peek_ph) as powerh_peek,
					MAX(sensor_peek_t) as temperature_peek,
					MAX(sensor_peek_h) as humidity_peek
				FROM
					eb_port_data_hour t, location l, asset a
				WHERE
					t.asset_id = a.asset_id AND
					a.location_id = l.location_id AND
					l.building_id = @BUILDING_ID AND
					DATEPART(yy, t.date) = @YEAR AND
					DATEPART(mm, t.date) = @MONTH
				GROUP BY
					DATEPART(dd, t.date)
			END
			IF @LOCATION_LEVEL = 5
			BEGIN
				-- 층으로 검색
				SELECT
					DATEPART(dd, t.date) as day,
					SUM(power_v) / SUM(power_v_cnt) as voltage,
					SUM(power_i) / SUM(power_i_cnt) as current2,
					SUM(power_p) / SUM(power_p_cnt) as power,
					SUM(power_ph) / SUM(power_ph_cnt) as powerh,
					SUM(sensor_t) / SUM(sensor_t_cnt) as temperature,
					SUM(sensor_h) / SUM(sensor_h_cnt) as humidity,
					SUM(door) as door,
					MAX(power_peek_v) as voltage_peek,
					MAX(power_peek_i) as current_peek,
					MAX(power_peek_p) as power_peek,
					MAX(power_peek_ph) as powerh_peek,
					MAX(sensor_peek_t) as temperature_peek,
					MAX(sensor_peek_h) as humidity_peek
				FROM
					eb_port_data_hour t, location l, asset a
				WHERE
					t.asset_id = a.asset_id AND
					a.location_id = l.location_id AND
					l.floor_id = @FLOOR_ID AND
					DATEPART(yy, t.date) = @YEAR AND
					DATEPART(mm, t.date) = @MONTH
				GROUP BY
					DATEPART(dd, t.date)
			END
			IF @LOCATION_LEVEL = 6
			BEGIN
				-- 룸으로 검색
				SELECT
					DATEPART(dd, t.date) as day,
					SUM(power_v) / SUM(power_v_cnt) as voltage,
					SUM(power_i) / SUM(power_i_cnt) as current2,
					SUM(power_p) / SUM(power_p_cnt) as power,
					SUM(power_ph) / SUM(power_ph_cnt) as powerh,
					SUM(sensor_t) / SUM(sensor_t_cnt) as temperature,
					SUM(sensor_h) / SUM(sensor_h_cnt) as humidity,
					SUM(door) as door,
					MAX(power_peek_v) as voltage_peek,
					MAX(power_peek_i) as current_peek,
					MAX(power_peek_p) as power_peek,
					MAX(power_peek_ph) as powerh_peek,
					MAX(sensor_peek_t) as temperature_peek,
					MAX(sensor_peek_h) as humidity_peek
				FROM
					eb_port_data_hour t, location l, asset a
				WHERE
					t.asset_id = a.asset_id AND
					a.location_id = l.location_id AND
					l.room_id = @ROOM_ID AND
					DATEPART(yy, t.date) = @YEAR AND
					DATEPART(mm, t.date) = @MONTH
				GROUP BY
					DATEPART(dd, t.date)
			END
		END
	END
END
GO
/****** Object:  StoredProcedure [dbo].[sp_stat_eb_hour]    Script Date: 2015-02-02 오후 7:04:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_stat_eb_hour]
	@LOC_ID INT = 0,
	@YEAR INT = 0,
	@MONTH INT = 0,
	@DAY INT = 0
AS
BEGIN
	DECLARE @LOCATION_LEVEL INT 
	DECLARE @SITE_ID INT
	DECLARE @BUILDING_ID INT
	DECLARE @FLOOR_ID INT
	DECLARE @ROOM_ID INT
	
	IF @LOC_ID = 0
	BEGIN
		IF @YEAR = 0
			SELECT
				time_0_23 as time,
				SUM(power_v) / SUM(power_v_cnt) as voltage,
				SUM(power_i) / SUM(power_i_cnt) as current2,
				SUM(power_p) / SUM(power_p_cnt) as power,
				SUM(power_ph) / SUM(power_ph_cnt) as powerh,
				SUM(sensor_t) / SUM(sensor_t_cnt) as temperature,
				SUM(sensor_h) / SUM(sensor_h_cnt) as humidity,
				SUM(door) as door,
				MAX(power_peek_v) as voltage_peek,
				MAX(power_peek_i) as current_peek,
				MAX(power_peek_p) as power_peek,
				MAX(power_peek_ph) as powerh_peek,
				MAX(sensor_peek_t) as temperature_peek,
				MAX(sensor_peek_h) as humidity_peek
			FROM
				eb_port_data_hour
			GROUP BY
				time_0_23
		ELSE
			IF @MONTH = 0
				SELECT
					time_0_23 as time,
					SUM(power_v) / SUM(power_v_cnt) as voltage,
					SUM(power_i) / SUM(power_i_cnt) as current2,
					SUM(power_p) / SUM(power_p_cnt) as power,
					SUM(power_ph) / SUM(power_ph_cnt) as powerh,
					SUM(sensor_t) / SUM(sensor_t_cnt) as temperature,
					SUM(sensor_h) / SUM(sensor_h_cnt) as humidity,
					SUM(door) as door,
					MAX(power_peek_v) as voltage_peek,
					MAX(power_peek_i) as current_peek,
					MAX(power_peek_p) as power_peek,
					MAX(power_peek_ph) as powerh_peek,
					MAX(sensor_peek_t) as temperature_peek,
					MAX(sensor_peek_h) as humidity_peek
				FROM
					eb_port_data_hour
				WHERE
					DATEPART(yy, date) = @YEAR
				GROUP BY
					time_0_23
			ELSE
				IF @DAY = 0
					SELECT
						time_0_23 as time,
						SUM(power_v) / SUM(power_v_cnt) as voltage,
						SUM(power_i) / SUM(power_i_cnt) as current2,
						SUM(power_p) / SUM(power_p_cnt) as power,
						SUM(power_ph) / SUM(power_ph_cnt) as powerh,
						SUM(sensor_t) / SUM(sensor_t_cnt) as temperature,
						SUM(sensor_h) / SUM(sensor_h_cnt) as humidity,
						SUM(door) as door,
						MAX(power_peek_v) as voltage_peek,
						MAX(power_peek_i) as current_peek,
						MAX(power_peek_p) as power_peek,
						MAX(power_peek_ph) as powerh_peek,
						MAX(sensor_peek_t) as temperature_peek,
						MAX(sensor_peek_h) as humidity_peek
					FROM
						eb_port_data_hour
					WHERE
						DATEPART(yy, date) = @YEAR AND
						DATEPART(mm, date) = @MONTH
					GROUP BY
						time_0_23
				ELSE
					SELECT
						time_0_23 as time,
						SUM(power_v) / SUM(power_v_cnt) as voltage,
						SUM(power_i) / SUM(power_i_cnt) as current2,
						SUM(power_p) / SUM(power_p_cnt) as power,
						SUM(power_ph) / SUM(power_ph_cnt) as powerh,
						SUM(sensor_t) / SUM(sensor_t_cnt) as temperature,
						SUM(sensor_h) / SUM(sensor_h_cnt) as humidity,
						SUM(door) as door,
						MAX(power_peek_v) as voltage_peek,
						MAX(power_peek_i) as current_peek,
						MAX(power_peek_p) as power_peek,
						MAX(power_peek_ph) as powerh_peek,
						MAX(sensor_peek_t) as temperature_peek,
						MAX(sensor_peek_h) as humidity_peek
					FROM
						eb_port_data_hour
					WHERE
						DATEPART(yy, date) = @YEAR AND
						DATEPART(mm, date) = @MONTH AND
						DATEPART(dd, date) = @DAY 
					GROUP BY
						time_0_23
	END
	ELSE
	BEGIN
		SELECT 
			@LOCATION_LEVEL = location_level, 
			@SITE_ID = site_id, 
			@BUILDING_ID = building_id, 
			@FLOOR_ID = floor_id,
			@ROOM_ID = room_id
		FROM 
			location 
		WHERE 
			location_id = @LOC_ID 

		IF @LOCATION_LEVEL IS NULL
			RETURN

		IF @YEAR = 0
		BEGIN
			IF @LOCATION_LEVEL = 3
				SELECT
					time_0_23 as time,
					SUM(power_v) / SUM(power_v_cnt) as voltage,
					SUM(power_i) / SUM(power_i_cnt) as current2,
					SUM(power_p) / SUM(power_p_cnt) as power,
					SUM(power_ph) / SUM(power_ph_cnt) as powerh,
					SUM(sensor_t) / SUM(sensor_t_cnt) as temperature,
					SUM(sensor_h) / SUM(sensor_h_cnt) as humidity,
					SUM(door) as door,
					MAX(power_peek_v) as voltage_peek,
					MAX(power_peek_i) as current_peek,
					MAX(power_peek_p) as power_peek,
					MAX(power_peek_ph) as powerh_peek,
					MAX(sensor_peek_t) as temperature_peek,
					MAX(sensor_peek_h) as humidity_peek
				FROM
					eb_port_data_hour t, location l, asset a
				WHERE
					t.asset_id = a.asset_id AND
					a.location_id = l.location_id AND
					l.site_id = @SITE_ID
				GROUP BY
					time_0_23
			IF @LOCATION_LEVEL = 4
				SELECT
					time_0_23 as time,
					SUM(power_v) / SUM(power_v_cnt) as voltage,
					SUM(power_i) / SUM(power_i_cnt) as current2,
					SUM(power_p) / SUM(power_p_cnt) as power,
					SUM(power_ph) / SUM(power_ph_cnt) as powerh,
					SUM(sensor_t) / SUM(sensor_t_cnt) as temperature,
					SUM(sensor_h) / SUM(sensor_h_cnt) as humidity,
					SUM(door) as door,
					MAX(power_peek_v) as voltage_peek,
					MAX(power_peek_i) as current_peek,
					MAX(power_peek_p) as power_peek,
					MAX(power_peek_ph) as powerh_peek,
					MAX(sensor_peek_t) as temperature_peek,
					MAX(sensor_peek_h) as humidity_peek
				FROM
					eb_port_data_hour t, location l, asset a
				WHERE
					t.asset_id = a.asset_id AND
					a.location_id = l.location_id AND
					l.building_id = @BUILDING_ID
				GROUP BY
					time_0_23
			IF @LOCATION_LEVEL = 5
				SELECT
					time_0_23 as time,
					SUM(power_v) / SUM(power_v_cnt) as voltage,
					SUM(power_i) / SUM(power_i_cnt) as current2,
					SUM(power_p) / SUM(power_p_cnt) as power,
					SUM(power_ph) / SUM(power_ph_cnt) as powerh,
					SUM(sensor_t) / SUM(sensor_t_cnt) as temperature,
					SUM(sensor_h) / SUM(sensor_h_cnt) as humidity,
					SUM(door) as door,
					MAX(power_peek_v) as voltage_peek,
					MAX(power_peek_i) as current_peek,
					MAX(power_peek_p) as power_peek,
					MAX(power_peek_ph) as powerh_peek,
					MAX(sensor_peek_t) as temperature_peek,
					MAX(sensor_peek_h) as humidity_peek
				FROM
					eb_port_data_hour t, location l, asset a
				WHERE
					t.asset_id = a.asset_id AND
					a.location_id = l.location_id AND
					l.floor_id = @FLOOR_ID
				GROUP BY
					time_0_23
			IF @LOCATION_LEVEL = 6
				SELECT
					time_0_23 as time,
					SUM(power_v) / SUM(power_v_cnt) as voltage,
					SUM(power_i) / SUM(power_i_cnt) as current2,
					SUM(power_p) / SUM(power_p_cnt) as power,
					SUM(power_ph) / SUM(power_ph_cnt) as powerh,
					SUM(sensor_t) / SUM(sensor_t_cnt) as temperature,
					SUM(sensor_h) / SUM(sensor_h_cnt) as humidity,
					SUM(door) as door,
					MAX(power_peek_v) as voltage_peek,
					MAX(power_peek_i) as current_peek,
					MAX(power_peek_p) as power_peek,
					MAX(power_peek_ph) as powerh_peek,
					MAX(sensor_peek_t) as temperature_peek,
					MAX(sensor_peek_h) as humidity_peek
				FROM
					eb_port_data_hour t, location l, asset a
				WHERE
					t.asset_id = a.asset_id AND
					a.location_id = l.location_id AND
					l.room_id = @ROOM_ID
				GROUP BY
					time_0_23
		END
		ELSE
			IF @MONTH = 0
			BEGIN
				-- 해당년에 대한 검색
				IF @LOCATION_LEVEL = 3
					SELECT
						time_0_23 as time,
						SUM(power_v) / SUM(power_v_cnt) as voltage,
						SUM(power_i) / SUM(power_i_cnt) as current2,
						SUM(power_p) / SUM(power_p_cnt) as power,
						SUM(power_ph) / SUM(power_ph_cnt) as powerh,
						SUM(sensor_t) / SUM(sensor_t_cnt) as temperature,
						SUM(sensor_h) / SUM(sensor_h_cnt) as humidity,
						SUM(door) as door,
						MAX(power_peek_v) as voltage_peek,
						MAX(power_peek_i) as current_peek,
						MAX(power_peek_p) as power_peek,
						MAX(power_peek_ph) as powerh_peek,
						MAX(sensor_peek_t) as temperature_peek,
						MAX(sensor_peek_h) as humidity_peek
					FROM
						eb_port_data_hour t, location l, asset a
					WHERE
						t.asset_id = a.asset_id AND
						a.location_id = l.location_id AND
						l.site_id = @SITE_ID AND
						DATEPART(yy, t.date) = @YEAR
					GROUP BY
						time_0_23
				IF @LOCATION_LEVEL = 4
					SELECT
						time_0_23 as time,
						SUM(power_v) / SUM(power_v_cnt) as voltage,
						SUM(power_i) / SUM(power_i_cnt) as current2,
						SUM(power_p) / SUM(power_p_cnt) as power,
						SUM(power_ph) / SUM(power_ph_cnt) as powerh,
						SUM(sensor_t) / SUM(sensor_t_cnt) as temperature,
						SUM(sensor_h) / SUM(sensor_h_cnt) as humidity,
						SUM(door) as door,
						MAX(power_peek_v) as voltage_peek,
						MAX(power_peek_i) as current_peek,
						MAX(power_peek_p) as power_peek,
						MAX(power_peek_ph) as powerh_peek,
						MAX(sensor_peek_t) as temperature_peek,
						MAX(sensor_peek_h) as humidity_peek
					FROM
						eb_port_data_hour t, location l, asset a
					WHERE
						t.asset_id = a.asset_id AND
						a.location_id = l.location_id AND
						l.building_id = @BUILDING_ID AND
						DATEPART(yy, t.date) = @YEAR
					GROUP BY
						time_0_23
				IF @LOCATION_LEVEL = 5
					SELECT
						time_0_23 as time,
						SUM(power_v) / SUM(power_v_cnt) as voltage,
						SUM(power_i) / SUM(power_i_cnt) as current2,
						SUM(power_p) / SUM(power_p_cnt) as power,
						SUM(power_ph) / SUM(power_ph_cnt) as powerh,
						SUM(sensor_t) / SUM(sensor_t_cnt) as temperature,
						SUM(sensor_h) / SUM(sensor_h_cnt) as humidity,
						SUM(door) as door,
						MAX(power_peek_v) as voltage_peek,
						MAX(power_peek_i) as current_peek,
						MAX(power_peek_p) as power_peek,
						MAX(power_peek_ph) as powerh_peek,
						MAX(sensor_peek_t) as temperature_peek,
						MAX(sensor_peek_h) as humidity_peek
					FROM
						eb_port_data_hour t, location l, asset a
					WHERE
						t.asset_id = a.asset_id AND
						a.location_id = l.location_id AND
						l.floor_id = @FLOOR_ID AND
						DATEPART(yy, t.date) = @YEAR
					GROUP BY
						time_0_23
				IF @LOCATION_LEVEL = 6
					SELECT
						time_0_23 as time,
						SUM(power_v) / SUM(power_v_cnt) as voltage,
						SUM(power_i) / SUM(power_i_cnt) as current2,
						SUM(power_p) / SUM(power_p_cnt) as power,
						SUM(power_ph) / SUM(power_ph_cnt) as powerh,
						SUM(sensor_t) / SUM(sensor_t_cnt) as temperature,
						SUM(sensor_h) / SUM(sensor_h_cnt) as humidity,
						SUM(door) as door,
						MAX(power_peek_v) as voltage_peek,
						MAX(power_peek_i) as current_peek,
						MAX(power_peek_p) as power_peek,
						MAX(power_peek_ph) as powerh_peek,
						MAX(sensor_peek_t) as temperature_peek,
						MAX(sensor_peek_h) as humidity_peek
					FROM
						eb_port_data_hour t, location l, asset a
					WHERE
						t.asset_id = a.asset_id AND
						a.location_id = l.location_id AND
						l.room_id = @ROOM_ID AND
						DATEPART(yy, t.date) = @YEAR
					GROUP BY
						time_0_23
			END
			ELSE
				IF @DAY = 0
				BEGIN
					-- 해당월에 대한 검색
					IF @LOCATION_LEVEL = 3
						SELECT
							time_0_23 as time,
							SUM(power_v) / SUM(power_v_cnt) as voltage,
							SUM(power_i) / SUM(power_i_cnt) as current2,
							SUM(power_p) / SUM(power_p_cnt) as power,
							SUM(power_ph) / SUM(power_ph_cnt) as powerh,
							SUM(sensor_t) / SUM(sensor_t_cnt) as temperature,
							SUM(sensor_h) / SUM(sensor_h_cnt) as humidity,
							SUM(door) as door,
							MAX(power_peek_v) as voltage_peek,
							MAX(power_peek_i) as current_peek,
							MAX(power_peek_p) as power_peek,
							MAX(power_peek_ph) as powerh_peek,
							MAX(sensor_peek_t) as temperature_peek,
							MAX(sensor_peek_h) as humidity_peek
						FROM
							eb_port_data_hour t, location l, asset a
						WHERE
							t.asset_id = a.asset_id AND
							a.location_id = l.location_id AND
							l.site_id = @SITE_ID AND
							DATEPART(yy, date) = @YEAR AND
							DATEPART(mm, date) = @MONTH
						GROUP BY
							time_0_23
					IF @LOCATION_LEVEL = 4
						SELECT
							time_0_23 as time,
							SUM(power_v) / SUM(power_v_cnt) as voltage,
							SUM(power_i) / SUM(power_i_cnt) as current2,
							SUM(power_p) / SUM(power_p_cnt) as power,
							SUM(power_ph) / SUM(power_ph_cnt) as powerh,
							SUM(sensor_t) / SUM(sensor_t_cnt) as temperature,
							SUM(sensor_h) / SUM(sensor_h_cnt) as humidity,
							SUM(door) as door,
							MAX(power_peek_v) as voltage_peek,
							MAX(power_peek_i) as current_peek,
							MAX(power_peek_p) as power_peek,
							MAX(power_peek_ph) as powerh_peek,
							MAX(sensor_peek_t) as temperature_peek,
							MAX(sensor_peek_h) as humidity_peek
						FROM
							eb_port_data_hour t, location l, asset a
						WHERE
							t.asset_id = a.asset_id AND
							a.location_id = l.location_id AND
							l.building_id = @BUILDING_ID AND
							DATEPART(yy, date) = @YEAR AND
							DATEPART(mm, date) = @MONTH
						GROUP BY
							time_0_23
					IF @LOCATION_LEVEL = 5
						SELECT
							time_0_23 as time,
							SUM(power_v) / SUM(power_v_cnt) as voltage,
							SUM(power_i) / SUM(power_i_cnt) as current2,
							SUM(power_p) / SUM(power_p_cnt) as power,
							SUM(power_ph) / SUM(power_ph_cnt) as powerh,
							SUM(sensor_t) / SUM(sensor_t_cnt) as temperature,
							SUM(sensor_h) / SUM(sensor_h_cnt) as humidity,
							SUM(door) as door,
							MAX(power_peek_v) as voltage_peek,
							MAX(power_peek_i) as current_peek,
							MAX(power_peek_p) as power_peek,
							MAX(power_peek_ph) as powerh_peek,
							MAX(sensor_peek_t) as temperature_peek,
							MAX(sensor_peek_h) as humidity_peek
						FROM
							eb_port_data_hour t, location l, asset a
						WHERE
							t.asset_id = a.asset_id AND
							a.location_id = l.location_id AND
							l.floor_id = @FLOOR_ID AND
							DATEPART(yy, date) = @YEAR AND
							DATEPART(mm, date) = @MONTH
						GROUP BY
							time_0_23
					IF @LOCATION_LEVEL = 6
						SELECT
							time_0_23 as time,
							SUM(power_v) / SUM(power_v_cnt) as voltage,
							SUM(power_i) / SUM(power_i_cnt) as current2,
							SUM(power_p) / SUM(power_p_cnt) as power,
							SUM(power_ph) / SUM(power_ph_cnt) as powerh,
							SUM(sensor_t) / SUM(sensor_t_cnt) as temperature,
							SUM(sensor_h) / SUM(sensor_h_cnt) as humidity,
							SUM(door) as door,
							MAX(power_peek_v) as voltage_peek,
							MAX(power_peek_i) as current_peek,
							MAX(power_peek_p) as power_peek,
							MAX(power_peek_ph) as powerh_peek,
							MAX(sensor_peek_t) as temperature_peek,
							MAX(sensor_peek_h) as humidity_peek
						FROM
							eb_port_data_hour t, location l, asset a
						WHERE
							t.asset_id = a.asset_id AND
							a.location_id = l.location_id AND
							l.room_id = @ROOM_ID AND
							DATEPART(yy, date) = @YEAR AND
							DATEPART(mm, date) = @MONTH
						GROUP BY
							time_0_23
				END
				ELSE
				BEGIN
					-- 해당일로 검색
					IF @LOCATION_LEVEL = 3
						SELECT
							time_0_23 as time,
							SUM(power_v) / SUM(power_v_cnt) as voltage,
							SUM(power_i) / SUM(power_i_cnt) as current2,
							SUM(power_p) / SUM(power_p_cnt) as power,
							SUM(power_ph) / SUM(power_ph_cnt) as powerh,
							SUM(sensor_t) / SUM(sensor_t_cnt) as temperature,
							SUM(sensor_h) / SUM(sensor_h_cnt) as humidity,
							SUM(door) as door,
							MAX(power_peek_v) as voltage_peek,
							MAX(power_peek_i) as current_peek,
							MAX(power_peek_p) as power_peek,
							MAX(power_peek_ph) as powerh_peek,
							MAX(sensor_peek_t) as temperature_peek,
							MAX(sensor_peek_h) as humidity_peek
						FROM
							eb_port_data_hour t, location l, asset a
						WHERE
							t.asset_id = a.asset_id AND
							a.location_id = l.location_id AND
							l.site_id = @SITE_ID AND
							DATEPART(yy, date) = @YEAR AND
							DATEPART(mm, date) = @MONTH AND
							DATEPART(dd, date) = @DAY
						GROUP BY
							time_0_23
					IF @LOCATION_LEVEL = 4
						SELECT
							time_0_23 as time,
							SUM(power_v) / SUM(power_v_cnt) as voltage,
							SUM(power_i) / SUM(power_i_cnt) as current2,
							SUM(power_p) / SUM(power_p_cnt) as power,
							SUM(power_ph) / SUM(power_ph_cnt) as powerh,
							SUM(sensor_t) / SUM(sensor_t_cnt) as temperature,
							SUM(sensor_h) / SUM(sensor_h_cnt) as humidity,
							SUM(door) as door,
							MAX(power_peek_v) as voltage_peek,
							MAX(power_peek_i) as current_peek,
							MAX(power_peek_p) as power_peek,
							MAX(power_peek_ph) as powerh_peek,
							MAX(sensor_peek_t) as temperature_peek,
							MAX(sensor_peek_h) as humidity_peek
						FROM
							eb_port_data_hour t, location l, asset a
						WHERE
							t.asset_id = a.asset_id AND
							a.location_id = l.location_id AND
							l.building_id = @BUILDING_ID AND
							DATEPART(yy, date) = @YEAR AND
							DATEPART(mm, date) = @MONTH AND
							DATEPART(dd, date) = @DAY
						GROUP BY
							time_0_23
					IF @LOCATION_LEVEL = 5
						SELECT
							time_0_23 as time,
							SUM(power_v) / SUM(power_v_cnt) as voltage,
							SUM(power_i) / SUM(power_i_cnt) as current2,
							SUM(power_p) / SUM(power_p_cnt) as power,
							SUM(power_ph) / SUM(power_ph_cnt) as powerh,
							SUM(sensor_t) / SUM(sensor_t_cnt) as temperature,
							SUM(sensor_h) / SUM(sensor_h_cnt) as humidity,
							SUM(door) as door,
							MAX(power_peek_v) as voltage_peek,
							MAX(power_peek_i) as current_peek,
							MAX(power_peek_p) as power_peek,
							MAX(power_peek_ph) as powerh_peek,
							MAX(sensor_peek_t) as temperature_peek,
							MAX(sensor_peek_h) as humidity_peek
						FROM
							eb_port_data_hour t, location l, asset a
						WHERE
							t.asset_id = a.asset_id AND
							a.location_id = l.location_id AND
							l.floor_id = @FLOOR_ID AND
							DATEPART(yy, date) = @YEAR AND
							DATEPART(mm, date) = @MONTH AND
							DATEPART(dd, date) = @DAY
						GROUP BY
							time_0_23
					IF @LOCATION_LEVEL = 6
						SELECT
							time_0_23 as time,
							SUM(power_v) / SUM(power_v_cnt) as voltage,
							SUM(power_i) / SUM(power_i_cnt) as current2,
							SUM(power_p) / SUM(power_p_cnt) as power,
							SUM(power_ph) / SUM(power_ph_cnt) as powerh,
							SUM(sensor_t) / SUM(sensor_t_cnt) as temperature,
							SUM(sensor_h) / SUM(sensor_h_cnt) as humidity,
							SUM(door) as door,
							MAX(power_peek_v) as voltage_peek,
							MAX(power_peek_i) as current_peek,
							MAX(power_peek_p) as power_peek,
							MAX(power_peek_ph) as powerh_peek,
							MAX(sensor_peek_t) as temperature_peek,
							MAX(sensor_peek_h) as humidity_peek
						FROM
							eb_port_data_hour t, location l, asset a
						WHERE
							t.asset_id = a.asset_id AND
							a.location_id = l.location_id AND
							l.room_id = @ROOM_ID AND
							DATEPART(yy, date) = @YEAR AND
							DATEPART(mm, date) = @MONTH AND
							DATEPART(dd, date) = @DAY
						GROUP BY
							time_0_23
				END
	END
END

GO
/****** Object:  StoredProcedure [dbo].[sp_stat_eb_month]    Script Date: 2015-02-02 오후 7:04:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[sp_stat_eb_month]
	@LOC_ID INT = 0,
	@YEAR INT = 0
AS
BEGIN
	DECLARE @LOCATION_LEVEL INT 
	DECLARE @SITE_ID INT
	DECLARE @BUILDING_ID INT
	DECLARE @FLOOR_ID INT
	DECLARE @ROOM_ID INT

	IF @LOC_ID = 0
	BEGIN
		IF @YEAR = 0
		BEGIN
			-- ----------------------
			-- 전체에 대한 검색
			-- ----------------------
			SELECT
				DATEPART(mm, t.date) as month,
				SUM(power_v) / SUM(power_v_cnt) as voltage,
				SUM(power_i) / SUM(power_i_cnt) as current2,
				SUM(power_p) / SUM(power_p_cnt) as power,
				SUM(power_ph) / SUM(power_ph_cnt) as powerh,
				SUM(sensor_t) / SUM(sensor_t_cnt) as temperature,
				SUM(sensor_h) / SUM(sensor_h_cnt) as humidity,
				SUM(door) as door,
				MAX(power_peek_v) as voltage_peek,
				MAX(power_peek_i) as current_peek,
				MAX(power_peek_p) as power_peek,
				MAX(power_peek_ph) as powerh_peek,
				MAX(sensor_peek_t) as temperature_peek,
				MAX(sensor_peek_h) as humidity_peek
			FROM
				eb_port_data_hour t
			GROUP BY
				DATEPART(mm, t.date)
			RETURN	
		END
		ELSE
		BEGIN
			-- ----------------------
			-- 전체/해당년에 대한 검색
			-- ----------------------
			SELECT
				DATEPART(mm, t.date) as month,
				SUM(power_v) / SUM(power_v_cnt) as voltage,
				SUM(power_i) / SUM(power_i_cnt) as current2,
				SUM(power_p) / SUM(power_p_cnt) as power,
				SUM(power_ph) / SUM(power_ph_cnt) as powerh,
				SUM(sensor_t) / SUM(sensor_t_cnt) as temperature,
				SUM(sensor_h) / SUM(sensor_h_cnt) as humidity,
				SUM(door) as door,
				MAX(power_peek_v) as voltage_peek,
				MAX(power_peek_i) as current_peek,
				MAX(power_peek_p) as power_peek,
				MAX(power_peek_ph) as powerh_peek,
				MAX(sensor_peek_t) as temperature_peek,
				MAX(sensor_peek_h) as humidity_peek
			FROM
				eb_port_data_hour t
			WHERE
				DATEPART(yy, t.date) = @YEAR
			GROUP BY
				DATEPART(mm, t.date)
			RETURN	
		END
	END
	ELSE
	BEGIN
		SELECT 
			@LOCATION_LEVEL = location_level, 
			@SITE_ID = site_id, 
			@BUILDING_ID = building_id, 
			@FLOOR_ID = floor_id,
			@ROOM_ID = room_id
		FROM 
			location 
		WHERE 
			location_id = @LOC_ID 

		IF @LOCATION_LEVEL IS NULL
			RETURN

		-- ----------------------
		-- 해당년에 대한 검색
		-- ----------------------
		IF @LOCATION_LEVEL = 3
		BEGIN
			-- 사이트로 검색
			SELECT
				DATEPART(mm, t.date) as month,
				SUM(power_v) / SUM(power_v_cnt) as voltage,
				SUM(power_i) / SUM(power_i_cnt) as current2,
				SUM(power_p) / SUM(power_p_cnt) as power,
				SUM(power_ph) / SUM(power_ph_cnt) as powerh,
				SUM(sensor_t) / SUM(sensor_t_cnt) as temperature,
				SUM(sensor_h) / SUM(sensor_h_cnt) as humidity,
				SUM(door) as door,
				MAX(power_peek_v) as voltage_peek,
				MAX(power_peek_i) as current_peek,
				MAX(power_peek_p) as power_peek,
				MAX(power_peek_ph) as powerh_peek,
				MAX(sensor_peek_t) as temperature_peek,
				MAX(sensor_peek_h) as humidity_peek
			FROM
				eb_port_data_hour t, location l, asset a
			WHERE
				t.asset_id = a.asset_id AND
				a.location_id = l.location_id AND
				l.site_id = @SITE_ID AND
				DATEPART(yy, t.date) = @YEAR
			GROUP BY
				DATEPART(mm, t.date)
		END
		IF @LOCATION_LEVEL = 4
		BEGIN
			-- 빌딩으로 검색
			SELECT
				DATEPART(mm, t.date) as month,
				SUM(power_v) / SUM(power_v_cnt) as voltage,
				SUM(power_i) / SUM(power_i_cnt) as current2,
				SUM(power_p) / SUM(power_p_cnt) as power,
				SUM(power_ph) / SUM(power_ph_cnt) as powerh,
				SUM(sensor_t) / SUM(sensor_t_cnt) as temperature,
				SUM(sensor_h) / SUM(sensor_h_cnt) as humidity,
				SUM(door) as door,
				MAX(power_peek_v) as voltage_peek,
				MAX(power_peek_i) as current_peek,
				MAX(power_peek_p) as power_peek,
				MAX(power_peek_ph) as powerh_peek,
				MAX(sensor_peek_t) as temperature_peek,
				MAX(sensor_peek_h) as humidity_peek
			FROM
				eb_port_data_hour t, location l, asset a
			WHERE
				t.asset_id = a.asset_id AND
				a.location_id = l.location_id AND
				l.building_id = @BUILDING_ID AND
				DATEPART(yy, t.date) = @YEAR
			GROUP BY
				DATEPART(mm, t.date)
		END
		IF @LOCATION_LEVEL = 5
		BEGIN
			-- 층으로 검색
			SELECT
				DATEPART(mm, t.date) as month,
				SUM(power_v) / SUM(power_v_cnt) as voltage,
				SUM(power_i) / SUM(power_i_cnt) as current2,
				SUM(power_p) / SUM(power_p_cnt) as power,
				SUM(power_ph) / SUM(power_ph_cnt) as powerh,
				SUM(sensor_t) / SUM(sensor_t_cnt) as temperature,
				SUM(sensor_h) / SUM(sensor_h_cnt) as humidity,
				SUM(door) as door,
				MAX(power_peek_v) as voltage_peek,
				MAX(power_peek_i) as current_peek,
				MAX(power_peek_p) as power_peek,
				MAX(power_peek_ph) as powerh_peek,
				MAX(sensor_peek_t) as temperature_peek,
				MAX(sensor_peek_h) as humidity_peek
			FROM
				eb_port_data_hour t, location l, asset a
			WHERE
				t.asset_id = a.asset_id AND
				a.location_id = l.location_id AND
				l.floor_id = @FLOOR_ID AND
				DATEPART(yy, t.date) = @YEAR
			GROUP BY
				DATEPART(mm, t.date)
		END
		IF @LOCATION_LEVEL = 6
		BEGIN
			-- 룸으로 검색
			SELECT
				DATEPART(mm, t.date) as month,
				SUM(power_v) / SUM(power_v_cnt) as voltage,
				SUM(power_i) / SUM(power_i_cnt) as current2,
				SUM(power_p) / SUM(power_p_cnt) as power,
				SUM(power_ph) / SUM(power_ph_cnt) as powerh,
				SUM(sensor_t) / SUM(sensor_t_cnt) as temperature,
				SUM(sensor_h) / SUM(sensor_h_cnt) as humidity,
				SUM(door) as door,
				MAX(power_peek_v) as voltage_peek,
				MAX(power_peek_i) as current_peek,
				MAX(power_peek_p) as power_peek,
				MAX(power_peek_ph) as powerh_peek,
				MAX(sensor_peek_t) as temperature_peek,
				MAX(sensor_peek_h) as humidity_peek
			FROM
				eb_port_data_hour t, location l, asset a
			WHERE
				t.asset_id = a.asset_id AND
				a.location_id = l.location_id AND
				l.room_id = @ROOM_ID AND
				DATEPART(yy, t.date) = @YEAR
			GROUP BY
				DATEPART(mm, t.date)
		END
	END
END
GO
/****** Object:  StoredProcedure [dbo].[sp_stat_terminal_data_day]    Script Date: 2015-02-02 오후 7:04:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[sp_stat_terminal_data_day]
	@LOC_ID INT = 0,
	@YEAR INT = 0,
	@MONTH INT = 0
AS
BEGIN
	DECLARE @LOCATION_LEVEL INT 
	DECLARE @SITE_ID INT
	DECLARE @BUILDING_ID INT
	DECLARE @FLOOR_ID INT
	DECLARE @ROOM_ID INT

	IF @LOC_ID = 0
	BEGIN
		-- ----------------------
		-- 전체/해당년에 대한 검색
		-- ----------------------
		IF @MONTH = 0
		BEGIN
			SELECT
				DATEPART(dd, t.date) as day,
				AVG(act_terminal) as avg_of_act_terminal,
				AVG(tot_terminal) as avg_of_tot_terminal
			FROM
				terminal_data t
			WHERE
				DATEPART(yy, t.date) = @YEAR
			GROUP BY
				DATEPART(dd, t.date)
			RETURN	
		END

		-- ----------------------
		-- 전체/해당월에 대한 검색
		-- ----------------------

		ELSE
		BEGIN
			SELECT
				DATEPART(dd, t.date) as day,
				AVG(act_terminal) as avg_of_act_terminal,
				AVG(tot_terminal) as avg_of_tot_terminal
			FROM
				terminal_data t
			WHERE
				DATEPART(yy, t.date) = @YEAR AND
				DATEPART(mm, t.date) = @MONTH
			GROUP BY
				DATEPART(dd, t.date)
			RETURN
		END
	END
	ELSE
	BEGIN
		SELECT 
			@LOCATION_LEVEL = location_level, 
			@SITE_ID = site_id, 
			@BUILDING_ID = building_id, 
			@FLOOR_ID = floor_id,
			@ROOM_ID = room_id
		FROM 
			location 
		WHERE 
			location_id = @LOC_ID 

		IF @LOCATION_LEVEL IS NULL
			RETURN

		-- ----------------------
		-- 해당년에 대한 검색
		-- ----------------------
		IF @MONTH = 0
		BEGIN
			IF @LOCATION_LEVEL = 3
			BEGIN

				-- 사이트로 검색
				SELECT
					DATEPART(dd, t.date) as day,
					AVG(act_terminal) as avg_of_act_terminal,
					AVG(tot_terminal) as avg_of_tot_terminal
				FROM
					terminal_data t, location l
				WHERE
					t.location_id = l.location_id AND
					l.site_id = @SITE_ID AND
					DATEPART(yy, t.date) = @YEAR
				GROUP BY
					DATEPART(dd, t.date)
			END
			IF @LOCATION_LEVEL = 4
			BEGIN
				-- 빌딩으로 검색
				SELECT
					DATEPART(dd, t.date) as day,
					AVG(act_terminal) as avg_of_act_terminal,
					AVG(tot_terminal) as avg_of_tot_terminal
				FROM
					terminal_data t, location l
				WHERE
					t.location_id = l.location_id AND
					l.building_id = @BUILDING_ID AND
					DATEPART(yy, t.date) = @YEAR
				GROUP BY
					DATEPART(dd, t.date)
			END
			IF @LOCATION_LEVEL = 5
			BEGIN
				-- 층으로 검색
				SELECT
					DATEPART(dd, t.date) as day,
					AVG(act_terminal) as avg_of_act_terminal,
					AVG(tot_terminal) as avg_of_tot_terminal
				FROM
					terminal_data t, location l
				WHERE
					t.location_id = l.location_id AND
					l.floor_id = @FLOOR_ID AND
					DATEPART(yy, t.date) = @YEAR
				GROUP BY
					DATEPART(dd, t.date)
			END
			IF @LOCATION_LEVEL = 6
			BEGIN
				-- 룸으로 검색
				SELECT
					DATEPART(dd, t.date) as day,
					AVG(act_terminal) as avg_of_act_terminal,
					AVG(tot_terminal) as avg_of_tot_terminal
				FROM
					terminal_data t, location l
				WHERE
					t.location_id = l.location_id AND
					l.room_id = @ROOM_ID AND
					DATEPART(yy, t.date) = @YEAR
				GROUP BY
					DATEPART(dd, t.date)
			END
		END

		-- ----------------------
		-- 해당월에 대한 검색
		-- ----------------------
		ELSE
		BEGIN
			IF @LOCATION_LEVEL = 3
			BEGIN
				-- 사이트로 검색
				SELECT
					DATEPART(dd, t.date) as day,
					AVG(act_terminal) as avg_of_act_terminal,
					AVG(tot_terminal) as avg_of_tot_terminal
				FROM
					terminal_data t, location l
				WHERE
					t.location_id = l.location_id AND
					l.site_id = @SITE_ID AND
					DATEPART(yy, t.date) = @YEAR AND
					DATEPART(mm, t.date) = @MONTH
				GROUP BY
					DATEPART(dd, t.date)
			END
			IF @LOCATION_LEVEL = 4
			BEGIN
				-- 빌딩으로 검색
				SELECT
					DATEPART(dd, t.date) as day,
					AVG(act_terminal) as avg_of_act_terminal,
					AVG(tot_terminal) as avg_of_tot_terminal
				FROM
					terminal_data t, location l
				WHERE
					t.location_id = l.location_id AND
					l.building_id = @BUILDING_ID AND
					DATEPART(yy, t.date) = @YEAR AND
					DATEPART(mm, t.date) = @MONTH
				GROUP BY
					DATEPART(dd, t.date)
			END
			IF @LOCATION_LEVEL = 5
			BEGIN
				-- 층으로 검색
				SELECT
					DATEPART(dd, t.date) as day,
					AVG(act_terminal) as avg_of_act_terminal,
					AVG(tot_terminal) as avg_of_tot_terminal
				FROM
					terminal_data t, location l
				WHERE
					t.location_id = l.location_id AND
					l.floor_id = @FLOOR_ID AND
					DATEPART(yy, t.date) = @YEAR AND
					DATEPART(mm, t.date) = @MONTH
				GROUP BY
					DATEPART(dd, t.date)
			END
			IF @LOCATION_LEVEL = 6
			BEGIN
				-- 룸으로 검색
				SELECT
					DATEPART(dd, t.date) as day,
					AVG(act_terminal) as avg_of_act_terminal,
					AVG(tot_terminal) as avg_of_tot_terminal
				FROM
					terminal_data t, location l
				WHERE
					t.location_id = l.location_id AND
					l.room_id = @ROOM_ID AND
					DATEPART(yy, t.date) = @YEAR AND
					DATEPART(mm, t.date) = @MONTH
				GROUP BY
					DATEPART(dd, t.date)
			END
		END
	END
END
GO
/****** Object:  StoredProcedure [dbo].[sp_stat_terminal_data_hour]    Script Date: 2015-02-02 오후 7:04:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[sp_stat_terminal_data_hour]
	@SITE_ID INT = 0,
	@YEAR INT = 0,
	@MONTH INT = 0,
	@DAY INT = 0
AS
BEGIN
	IF @SITE_ID = 0
		IF @YEAR = 0
			SELECT
				time_0_23 as time,
				AVG(act_terminal) as avg_of_act_terminal,
				AVG(tot_terminal) as avg_of_tot_terminal
			FROM
				terminal_data_hour
			GROUP BY
				time_0_23
		ELSE
			IF @MONTH = 0
				SELECT
					time_0_23 as time,
					AVG(act_terminal) as avg_of_act_terminal,
					AVG(tot_terminal) as avg_of_tot_terminal
				FROM
					terminal_data_hour
				WHERE
					DATEPART(yy, date) = @YEAR
				GROUP BY
					time_0_23
			ELSE
				IF @DAY = 0
					SELECT
						time_0_23 as time,
						AVG(act_terminal) as avg_of_act_terminal,
						AVG(tot_terminal) as avg_of_tot_terminal
					FROM
						terminal_data_hour
					WHERE
						DATEPART(yy, date) = @YEAR AND
						DATEPART(mm, date) = @MONTH
					GROUP BY
						time_0_23
				ELSE
					SELECT
						time_0_23 as time,
						AVG(act_terminal) as avg_of_act_terminal,
						AVG(tot_terminal) as avg_of_tot_terminal
					FROM
						terminal_data_hour
					WHERE
						DATEPART(yy, date) = @YEAR AND
						DATEPART(mm, date) = @MONTH AND
						DATEPART(dd, date) = @DAY 
					GROUP BY
						time_0_23
	ELSE
		IF @YEAR = 0
			-- 해당년에 대한 검색
			SELECT
				time_0_23 as time,
				AVG(act_terminal) as avg_of_act_terminal,
				AVG(tot_terminal) as avg_of_tot_terminal
			FROM
				terminal_data_hour
			WHERE
				site_id = @SITE_ID
			GROUP BY
				time_0_23
		ELSE
			IF @MONTH = 0
				-- 해당년에 대한 검색
				SELECT
					time_0_23 as time,
					AVG(act_terminal) as avg_of_act_terminal,
					AVG(tot_terminal) as avg_of_tot_terminal
				FROM
					terminal_data_hour
				WHERE
					site_id = @SITE_ID AND
					DATEPART(yy, date) = @YEAR
				GROUP BY
					time_0_23
			ELSE
				IF @DAY = 0
					-- 해당월에 대한 검색
					SELECT
						time_0_23 as time,
						AVG(act_terminal) as avg_of_act_terminal,
						AVG(tot_terminal) as avg_of_tot_terminal
					FROM
						terminal_data_hour
					WHERE
						site_id = @SITE_ID AND
						DATEPART(yy, date) = @YEAR AND
						DATEPART(mm, date) = @MONTH
					GROUP BY
						time_0_23
				ELSE
					-- 해당일로 검색
					SELECT
						time_0_23 as time,
						AVG(act_terminal) as avg_of_act_terminal,
						AVG(tot_terminal) as avg_of_tot_terminal
					FROM
						terminal_data_hour
					WHERE
						site_id = @SITE_ID AND
						DATEPART(yy, date) = @YEAR AND
						DATEPART(mm, date) = @MONTH AND
						DATEPART(dd, date) = @DAY
					GROUP BY
						time_0_23

END

GO
/****** Object:  StoredProcedure [dbo].[sp_stat_terminal_data_month]    Script Date: 2015-02-02 오후 7:04:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[sp_stat_terminal_data_month]
	@LOC_ID INT = 0,
	@YEAR INT = 0
AS
BEGIN
	DECLARE @LOCATION_LEVEL INT 
	DECLARE @SITE_ID INT
	DECLARE @BUILDING_ID INT
	DECLARE @FLOOR_ID INT
	DECLARE @ROOM_ID INT

	IF @LOC_ID = 0
	BEGIN
		IF @YEAR = 0
		BEGIN
			-- ----------------------
			-- 전체에 대한 검색
			-- ----------------------
			SELECT
				DATEPART(mm, t.date) as month,
				AVG(act_terminal) as avg_of_act_terminal,
				AVG(tot_terminal) as avg_of_tot_terminal
			FROM
				terminal_data t
			GROUP BY
				DATEPART(mm, t.date)
			RETURN	
		END
		ELSE
		BEGIN
			-- ----------------------
			-- 전체/해당년에 대한 검색
			-- ----------------------
			SELECT
				DATEPART(mm, t.date) as month,
				AVG(act_terminal) as avg_of_act_terminal,
				AVG(tot_terminal) as avg_of_tot_terminal
			FROM
				terminal_data t
			WHERE
				DATEPART(yy, t.date) = @YEAR
			GROUP BY
				DATEPART(mm, t.date)
			RETURN	
		END
	END
	ELSE
	BEGIN
		SELECT 
			@LOCATION_LEVEL = location_level, 
			@SITE_ID = site_id, 
			@BUILDING_ID = building_id, 
			@FLOOR_ID = floor_id,
			@ROOM_ID = room_id
		FROM 
			location 
		WHERE 
			location_id = @LOC_ID 

		IF @LOCATION_LEVEL IS NULL
			RETURN

		-- ----------------------
		-- 해당년에 대한 검색
		-- ----------------------
		IF @LOCATION_LEVEL = 3
		BEGIN
			-- 사이트로 검색
			SELECT
				DATEPART(mm, t.date) as month,
				AVG(act_terminal) as avg_of_act_terminal,
				AVG(tot_terminal) as avg_of_tot_terminal
			FROM
				terminal_data t, location l
			WHERE
				t.location_id = l.location_id AND
				l.site_id = @SITE_ID AND
				DATEPART(yy, t.date) = @YEAR
			GROUP BY
				DATEPART(mm, t.date)
		END
		IF @LOCATION_LEVEL = 4
		BEGIN
			-- 빌딩으로 검색
			SELECT
				DATEPART(mm, t.date) as month,
				AVG(act_terminal) as avg_of_act_terminal,
				AVG(tot_terminal) as avg_of_tot_terminal
			FROM
				terminal_data t, location l
			WHERE
				t.location_id = l.location_id AND
				l.building_id = @BUILDING_ID AND
				DATEPART(yy, t.date) = @YEAR
			GROUP BY
				DATEPART(mm, t.date)
		END
		IF @LOCATION_LEVEL = 5
		BEGIN
			-- 층으로 검색
			SELECT
				DATEPART(mm, t.date) as month,
				AVG(act_terminal) as avg_of_act_terminal,
				AVG(tot_terminal) as avg_of_tot_terminal
			FROM
				terminal_data t, location l
			WHERE
				t.location_id = l.location_id AND
				l.floor_id = @FLOOR_ID AND
				DATEPART(yy, t.date) = @YEAR
			GROUP BY
				DATEPART(mm, t.date)
		END
		IF @LOCATION_LEVEL = 6
		BEGIN
			-- 룸으로 검색
			SELECT
				DATEPART(mm, t.date) as month,
				AVG(act_terminal) as avg_of_act_terminal,
				AVG(tot_terminal) as avg_of_tot_terminal
			FROM
				terminal_data t, location l
			WHERE
				t.location_id = l.location_id AND
				l.room_id = @ROOM_ID AND
				DATEPART(yy, t.date) = @YEAR
			GROUP BY
				DATEPART(mm, t.date)
		END
	END
END
GO
/****** Object:  StoredProcedure [dbo].[sp_stop_net_scan]    Script Date: 2015-02-02 오후 7:04:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO









CREATE proc [dbo].[sp_stop_net_scan]
as
BEGIN

DECLARE @TERMINAL_ID INT
DECLARE @FIRST_ASSET_ID INT
DECLARE @OUTLET_ASSET_ID INT
DECLARE @HUB_ASSET_ID INT

	SELECT TOP 1 @HUB_ASSET_ID = asset_id FROM asset WHERE asset_name = 'HUB'

    -- Step 1. cur=1, new=0 : 기존 MAC이 발견되고, 신규 MAC이 발견되지 않은 단말에 대해 모두 전원 off 처리
	UPDATE asset_terminal
	SET
		terminal_status = 'N'
	WHERE  
		new_enable = 'N' AND terminal_status = 'Y'

	-- Step 2. 각 포트의 링크를 완성한다. (내부에서 트리거에 의해 Unlocated로 옮겨지는 단말도 있음)

	EXEC sp_stop_net_scan_sub @HUB_ASSET_ID

    -- Step 3. cur <-- new 복사

	-- cur_enable = 'N'모두 바꾼다. --> 기존 MAC은 cur_enable=N가 된다.
	UPDATE asset_terminal
	SET
		cur_enable = 'N'
	WHERE
		cur_enable = 'Y'

	-- 신규 MAC에 대해 cur_enable = 'Y', new_enable = 'N'로 바꾼다.
	UPDATE asset_terminal
	SET
		cur_enable = 'Y'
		,terminal_status = 'Y'
		,cur_net_bios_name = new_net_bios_name
		,cur_outlet_asset_id = new_outlet_asset_id
		,cur_outlet_port_no = new_outlet_port_no
		,cur_sw_asset_id = new_sw_asset_id
		,cur_sw_port_no = new_sw_port_no
		,new_enable = 'N'
	WHERE
		new_enable = 'Y'

	UPDATE asset_terminal_ip
	SET	
		cur_enable = 'Y'
		,new_enable = 'N'
	WHERE
		new_enable = 'Y'

END













GO
/****** Object:  StoredProcedure [dbo].[sp_stop_net_scan_sub]    Script Date: 2015-02-02 오후 7:04:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO














CREATE proc [dbo].[sp_stop_net_scan_sub]

	@HUB_ASSET_ID INT
as
	-- 스위치 만큼 루프 (임베디드형 과 카드형만)


DECLARE @ASSET_ID INT
DECLARE @NUM_OF_PORTS INT
DECLARE @CNT INT
DECLARE @NUM_OF_MAC INT
DECLARE @OLD_NUM_OF_MAC INT
DECLARE @NEW_NUM_OF_MAC INT

DECLARE @FIRST_ASSET_ID INT
DECLARE @FIRST_PORT_NO INT
DECLARE @OUTLET_ASSET_ID INT
DECLARE @OUTLET_PORT_NO INT
DECLARE @LAST_ASSET_ID INT
DECLARE @LAST_PORT_NO INT
DECLARE @CHANGED_FLAG INT
DECLARE @TERMINAL_ASSET_ID INT
DECLARE @OUTLET_CABLE_CATALOG_ID INT
DECLARE @CABLE_CATALOG_ID INT

DECLARE cur CURSOR FOR
SELECT 
	asset_id, c.num_of_ports
FROM 
	asset a, catalog c
WHERE 
	a.catalog_id = c.catalog_id AND 
	a.asset_id in (SELECT sw_asset_id FROM net_scan_sw GROUP BY sw_asset_id) 
OPEN cur
FETCH NEXT FROM cur INTO @ASSET_ID, @NUM_OF_PORTS

WHILE @@FETCH_STATUS = 0
BEGIN

	SET @CNT = 1
	WHILE (@CNT <= @NUM_OF_PORTS)
	BEGIN
		-- 파워가 OFF된 항목 중 날짜가 지난것들을 모두 Unlocated 처리
		EXEC sp_stop_net_scan_sub_a @ASSET_ID, @CNT

		-- cur_enable='N' 이면서 new_enable='N'인 것은 Unlocated 항목이므로 count되어선 안됨.
		SELECT 
			@OLD_NUM_OF_MAC = count(terminal_id) 
		FROM 
			asset_terminal 
		WHERE 
			(cur_sw_asset_id = @ASSET_ID AND cur_sw_port_no = @CNT) AND 
			is_unlocated = 'N' AND 
			cur_enable = 'Y' 

		SELECT 
			@NEW_NUM_OF_MAC = count(terminal_id) 
		FROM asset_terminal 
		WHERE 
			new_sw_asset_id = @ASSET_ID AND new_sw_port_no = @CNT AND 
			is_unlocated = 'N' AND 
			cur_enable = 'N' AND new_enable = 'Y'

		SET @NUM_OF_MAC = @OLD_NUM_OF_MAC + @NEW_NUM_OF_MAC

		SET @FIRST_ASSET_ID = NULL
		SET @FIRST_PORT_NO = NULL
		SET @OUTLET_ASSET_ID = NULL
		SET @OUTLET_PORT_NO = NULL
		SET @LAST_ASSET_ID = NULL
		SET @LAST_PORT_NO = NULL

		EXEC sp_find_port_link_info @ASSET_ID, @CNT, @FIRST_ASSET_ID OUT, @FIRST_PORT_NO OUT, @OUTLET_ASSET_ID OUT, @OUTLET_PORT_NO OUT, @OUTLET_CABLE_CATALOG_ID OUT, @LAST_ASSET_ID OUT, @LAST_PORT_NO OUT

		IF (@OUTLET_ASSET_ID IS NOT NULL)
		BEGIN
			EXEC sp_get_terminal_cable @OUTLET_CABLE_CATALOG_ID, @CABLE_CATALOG_ID OUT

			--IF (@OUTLET_PORT_NO = 1)
			--BEGIN
				--SELECT 
				--	@ASSET_ID as 스위치자산ID
				--	,@CNT as 포트번호
				--	,@NUM_OF_MAC as 전체MAC수
				--	,@OLD_NUM_OF_MAC as 기존MAC수
				--	,@NEW_NUM_OF_MAC as 새로운MAC수
				--	,@FIRST_ASSET_ID as 첫자산ID
				--	,@FIRST_PORT_NO as 첫자산번호
				--	,@OUTLET_ASSET_ID as 아울렛ID
				--	,@OUTLET_PORT_NO as 아울렛포트번호
				--	,@OUTLET_CABLE_CATALOG_ID as 아울렛케이블카탈로그ID
				--	,@LAST_ASSET_ID as 마지막자산ID
				--	,@LAST_PORT_NO as 마지막자산번호
				--	,@CABLE_CATALOG_ID as 케이블카탈로그ID

					--select '아울렛 포트링크', * from asset_port_link WHERE asset_id = @OUTLET_ASSET_ID AND port_no = @OUTLET_PORT_NO
					--select @TERMINAL_ASSET_ID as 터미널자산ID, @OUTLET_ASSET_ID as 아울렛자산ID, @OUTLET_PORT_NO as 아울렛포트번호
					--select '포트링크', * from asset_port_link where asset_id = @TERMINAL_ASSET_ID
					--select '자산', * from asset where asset_id = @TERMINAL_ASSET_ID
					--select '터미널', * from asset_terminal where terminal_asset_id = @TERMINAL_ASSET_ID
			--END

			EXEC sp_stop_net_scan_sub_b @ASSET_ID, @CNT, @OUTLET_ASSET_ID, @OUTLET_PORT_NO, @CABLE_CATALOG_ID, @HUB_ASSET_ID, @NUM_OF_MAC, @NEW_NUM_OF_MAC

		END
		SET @CNT = @CNT + 1
	END

    FETCH NEXT FROM cur INTO @ASSET_ID, @NUM_OF_PORTS
END

CLOSE cur
DEALLOCATE cur











GO
/****** Object:  StoredProcedure [dbo].[sp_stop_net_scan_sub_a]    Script Date: 2015-02-02 오후 7:04:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_stop_net_scan_sub_a]
	@ASSET_ID INT
	,@PORT_NO INT
AS

DECLARE @CUR_ASSET_ID INT
DECLARE	@DUR_DAY INT

-- 2일간 전원이 꺼진 단말에 대해 Unlocated로 이주 시킨다.
SET @DUR_DAY = 7

-- 기존 다이어그램이 HUB이므로 MAC이 없는 단말은 모두 제거

DECLARE cur3 CURSOR FOR 
SELECT 
	terminal_asset_id
FROM 
	asset_terminal
WHERE 
	new_sw_asset_id = @ASSET_ID AND 
	new_sw_port_no = @PORT_NO AND	
	terminal_status = 'N' AND 
	cur_enable = 'Y' AND
	is_unlocated = 'N' AND
	GETDATE() + @DUR_DAY < last_activated

OPEN cur3
FETCH NEXT FROM cur3 INTO @CUR_ASSET_ID

WHILE @@FETCH_STATUS = 0
BEGIN
	UPDATE 
		asset_port_link
	SET 
		front_asset_id = NULL
		,front_port_no = NULL
		,front_plug_side = NULL
		,front_cable_catalog_id = NULL
	WHERE 
		asset_id = @CUR_ASSET_ID AND port_no = 1

	UPDATE
		asset
	SET
		location_id = 0
	WHERE
		asset_id = @CUR_ASSET_ID

	UPDATE
		asset_terminal
	SET
		new_enable = 'N'
		,cur_enable = 'N'
		,is_unlocated = 'Y'
	WHERE
		terminal_asset_id = @CUR_ASSET_ID

	UPDATE
		asset_terminal_ip
	SET
		new_enable = 'N'
		,cur_enable = 'N'
	WHERE
		terminal_id IN (SELECT terminal_id FROM asset_terminal WHERE terminal_asset_id = @CUR_ASSET_ID)

	-- 삭제된 단말에 대해 변경 히스토리를 저장한다.
	EXEC sp_add_changed_hist @CUR_ASSET_ID, 0

	FETCH NEXT FROM cur3 INTO @CUR_ASSET_ID
END

CLOSE cur3
DEALLOCATE cur3



GO
/****** Object:  StoredProcedure [dbo].[sp_stop_net_scan_sub_b]    Script Date: 2015-02-02 오후 7:04:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO















CREATE PROC  [dbo].[sp_stop_net_scan_sub_b] 
	@ASSET_ID INT
	,@PORT_NO INT
	,@OUTLET_ASSET_ID INT
	,@OUTLET_PORT_NO INT
	,@CABLE_CATALOG_ID INT
	,@HUB_ASSET_ID INT
	,@NUM_OF_MAC INT 
	,@NEW_NUM_OF_MAC INT 
AS

DECLARE @TERMINAL_ASSET_ID INT
DECLARE @CUR_ASSET_ID INT
DECLARE @CUR_ENABLE CHAR(1)

	--IF (@OUTLET_PORT_NO = 2)
	--BEGIN
	--	SELECT '맥이 0이라서 링크를 지워야 함', @OUTLET_ASSET_ID as '아울렛자산ID', @OUTLET_PORT_NO as '아울렛포트번호' 

	--	SELECT * FROM asset_port_link
	--	WHERE 
	--		asset_id = @OUTLET_ASSET_ID AND
	--		port_no = @OUTLET_PORT_NO 
	--		--AND
	--		--front_asset_id = @HUB_ASSET_ID
	--END

IF (@NUM_OF_MAC = 0)
BEGIN
	-- 기존 링크D의 종단에 있는 단말 또는 허브가 있는 경우 제거한다.
	-- 실제 단말쪽의 링크는 트리거로 인해 단말이 Unlocated로 옮겨질 때 단말쪽 링크D의 아울렛과의 연결 부분을 제거한다.
	UPDATE asset_port_link
	SET front_asset_id = NULL
		,front_port_no = NULL
		,front_plug_side = NULL
		,front_cable_catalog_id = NULL
	WHERE 
		asset_id = @OUTLET_ASSET_ID AND
		port_no = @OUTLET_PORT_NO AND
		front_asset_id IS NOT NULL



END
ELSE IF (@NUM_OF_MAC = 1)
BEGIN

	-- 마지막에 연결하기 위해 1대의 PC를 찾는다.
	SELECT  TOP 1
		@TERMINAL_ASSET_ID = terminal_asset_id
		,@CUR_ENABLE = cur_enable
	FROM 
		asset_terminal 
	WHERE 
		new_sw_asset_id = @ASSET_ID AND
		new_sw_port_no = @PORT_NO AND
		(cur_enable = 'Y' OR new_enable = 'Y')

	-- PC를 연결
	UPDATE asset_port_link
	SET front_asset_id = @TERMINAL_ASSET_ID
		,front_port_no = 1
		,front_plug_side = 'F'
		,front_cable_catalog_id = @CABLE_CATALOG_ID
	WHERE 
		asset_id = @OUTLET_ASSET_ID AND
		port_no = @OUTLET_PORT_NO AND 
		(front_asset_id IS NULL OR front_asset_id != @TERMINAL_ASSET_ID)

	IF (@NEW_NUM_OF_MAC = 1)
	BEGIN
		--SELECT 'Add PC', @ASSET_ID as '스위치자산ID', @PORT_NO as '스위치포트번호', @TERMINAL_ASSET_ID as '터미널자산ID'


		UPDATE asset_port_link
		SET front_asset_id = @OUTLET_ASSET_ID
			,front_port_no = @OUTLET_PORT_NO
			,front_plug_side = 'F'
			,front_cable_catalog_id = @CABLE_CATALOG_ID
		WHERE 
			asset_id = @TERMINAL_ASSET_ID AND
			port_no = 1

		-- 새롭게 추가된 단말에 대해 변경 히스토리를 저장한다.
		EXEC sp_add_changed_hist @TERMINAL_ASSET_ID, 1
	END

	--select '아울렛 포트링크', * from asset_port_link WHERE asset_id = @OUTLET_ASSET_ID AND port_no = @OUTLET_PORT_NO
	--select @TERMINAL_ASSET_ID as 터미널자산ID, @OUTLET_ASSET_ID as 아울렛자산ID, @OUTLET_PORT_NO as 아울렛포트번호
	--select '포트링크', * from asset_port_link where asset_id = @TERMINAL_ASSET_ID
	--select '자산', * from asset where asset_id = @TERMINAL_ASSET_ID
	--select '터미널', * from asset_terminal where terminal_asset_id = @TERMINAL_ASSET_ID
END
ELSE IF (@NUM_OF_MAC > 1)
BEGIN
	-- 링크D에서 종단이 허브로 연결되어 있지 않은 경우 허브로 연결한다.
	UPDATE asset_port_link
	SET front_asset_id = @HUB_ASSET_ID
		,front_port_no = 1
		,front_plug_side = 'F'
		,front_cable_catalog_id = @CABLE_CATALOG_ID
	WHERE 
		asset_id = @OUTLET_ASSET_ID AND
		port_no = @OUTLET_PORT_NO AND
		(front_asset_id != @HUB_ASSET_ID OR front_asset_id IS NULL)

	--SELECT 'Add HUB', @ASSET_ID as '스위치자산ID', @PORT_NO as '스위치포트번호', @OUTLET_ASSET_ID as 아울렛자산ID, @OUTLET_PORT_NO 아울렛포트번호, @HUB_ASSET_ID as '허브자산ID'

	-- MAC이 새롭게 추가된 단말에 대해 링크를 수정한다.
	DECLARE cur2 CURSOR FOR 
	SELECT 
		terminal_asset_id
	FROM 
		asset_terminal
	WHERE 
		new_sw_asset_id = @ASSET_ID AND 
		new_sw_port_no = @PORT_NO AND	
		cur_enable = 'N' AND new_enable = 'Y'

	OPEN cur2
	FETCH NEXT FROM cur2 INTO @CUR_ASSET_ID

	WHILE @@FETCH_STATUS = 0
	BEGIN

		UPDATE asset_port_link
		SET front_asset_id = @OUTLET_ASSET_ID
			,front_port_no = @OUTLET_PORT_NO
			,front_plug_side = 'F'
			,front_cable_catalog_id = @CABLE_CATALOG_ID
		WHERE 
			asset_id = @CUR_ASSET_ID

		-- 새롭게 추가된 단말에 대해 변경 히스토리를 저장한다.
		EXEC sp_add_changed_hist @CUR_ASSET_ID, 1

		FETCH NEXT FROM cur2 INTO @CUR_ASSET_ID
	END
	
	CLOSE cur2
	DEALLOCATE cur2
END








GO
/****** Object:  StoredProcedure [dbo].[sp_unset_alarm]    Script Date: 2015-02-02 오후 7:04:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[sp_unset_alarm]
	@ASSET_ID int
	,@PORT_NO int
	,@PORT_STATUS char(1) = '-'
AS
UPDATE
	asset_ipp_port_link
SET
	alarm_status = '-',
	wo_status = '-',
	ipp_port_status = @PORT_STATUS
WHERE
	ipp_asset_id = @ASSET_ID AND
	port_no = @PORT_NO





GO
/****** Object:  StoredProcedure [dbo].[sp_upd_asset_port_link]    Script Date: 2015-02-02 오후 7:04:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO






CREATE PROC [dbo].[sp_upd_asset_port_link]
   @PORT_SIDE CHAR(1)
   ,@ASSET_ID INT
   ,@PORT_NO INT
   ,@REMOTE_ASSET_ID INT
   ,@REMOTE_PORT_NO INT
   ,@CABLE_CATALOG_ID INT
   ,@PLUG_SIDE CHAR(1)
AS

DECLARE @CATALOG_GROUP_ID INT
DECLARE @CNT INT

-- asset_port_link 테이블 수정
IF (@PORT_SIDE = 'F')
BEGIN
	UPDATE 
		asset_port_link
	SET
		front_plug_side = @PLUG_SIDE
		,front_asset_id = @REMOTE_ASSET_ID
		,front_port_no = @REMOTE_PORT_NO
		,front_cable_catalog_id = @CABLE_CATALOG_ID
	WHERE
		asset_id = @ASSET_ID AND 
		port_no = @PORT_NO
END
ELSE
BEGIN
	UPDATE 
		asset_port_link
	SET
		rear_plug_side = @PLUG_SIDE
		,rear_asset_id = @REMOTE_ASSET_ID
		,rear_port_no = @REMOTE_PORT_NO
		,rear_cable_catalog_id = @CABLE_CATALOG_ID
	WHERE
		asset_id = @ASSET_ID AND 
		port_no = @PORT_NO
END











GO
/****** Object:  StoredProcedure [dbo].[sp_upd_update_func]    Script Date: 2015-02-02 오후 7:04:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[sp_upd_update_func]
	@FUNC_ID int
as
BEGIN
	UPDATE
		update_func
	SET
		invoked_cnt = invoked_cnt + 1
	WHERE
		update_func_id = @FUNC_ID
END



GO
