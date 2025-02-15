
USE [i2ms2]
GO

/*************************************************************************************************/
/*************************************************************************************************/
/*  2016.04.01 기존 테이블 지우기 먼저 처리  ************************************************************************************************/
/****** romee 스위치 처리  지원용  ******/
/****** DROP TABLE [dbo].[net_scan_scheduler] ******/
/****** Object:  Table [dbo].[net_scan_sw]  ******/  
/******  DROP TABLE [dbo].[net_scan] ******/
/*************************************************************************************************/
/*************************************************************************************************/


EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'net_scan_scheduler'

GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'net_scan_scheduler', @level2type=N'COLUMN',@level2name=N'repeat_day0'

GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'net_scan_scheduler', @level2type=N'COLUMN',@level2name=N'repeat_pattern'

GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'net_scan_scheduler', @level2type=N'COLUMN',@level2name=N'schedule_time'

GO

ALTER TABLE [dbo].[net_scan_scheduler] DROP CONSTRAINT [DF_net_scan_scheduler_9]
GO

ALTER TABLE [dbo].[net_scan_scheduler] DROP CONSTRAINT [DF_net_scan_scheduler_7]
GO

ALTER TABLE [dbo].[net_scan_scheduler] DROP CONSTRAINT [DF_net_scan_scheduler_6]
GO

ALTER TABLE [dbo].[net_scan_scheduler] DROP CONSTRAINT [DF_net_scan_scheduler_5]
GO

ALTER TABLE [dbo].[net_scan_scheduler] DROP CONSTRAINT [DF_net_scan_scheduler_4]
GO

ALTER TABLE [dbo].[net_scan_scheduler] DROP CONSTRAINT [DF_net_scan_scheduler_3]
GO

ALTER TABLE [dbo].[net_scan_scheduler] DROP CONSTRAINT [DF_net_scan_scheduler_2]
GO

ALTER TABLE [dbo].[net_scan_scheduler] DROP CONSTRAINT [DF_net_scan_scheduler_1]
GO

ALTER TABLE [dbo].[net_scan_scheduler] DROP CONSTRAINT [DF_net_scan_scheduler_8]
GO

/****** Object:  Table [dbo].[net_scan_scheduler]    Script Date: 2016-04-01 오후 2:14:18 ******/
DROP TABLE [dbo].[net_scan_scheduler]
GO

ALTER TABLE [dbo].[net_scan_sw] DROP CONSTRAINT [FK_net_scan_sw_net_scan]
GO

/****** Object:  Table [dbo].[net_scan_sw]    Script Date: 2016-04-01 오후 2:22:50 ******/
DROP TABLE [dbo].[net_scan_sw]
GO


/****** Object:  Table [dbo].[net_scan]    Script Date: 2016-04-01 오후 2:14:24 ******/
DROP TABLE [dbo].[net_scan]
GO

/*************************************************************************************************/
/*************************************************************************************************/
/*  2016.04.01 생성하기   ************************************************************************************************/
/****** romee 스위치 처리  지원용  ******/
/****** [dbo].[net_scan_scheduler] ******/
/****** [dbo].[net_scan_sw]  ******/  
/****** [dbo].[net_scan] ******/
/*************************************************************************************************/
/*************************************************************************************************/


/****** Object:  Table [dbo].[net_scan_scheduler]    Script Date: 2016-04-01 오후 2:14:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[net_scan_scheduler](
	[net_scan_scheduler_id] [int] IDENTITY(1,1) NOT NULL,
	[schedule_time] [varchar](5) NOT NULL,
	[repeat_pattern] [char](1) NOT NULL,
	[repeat_day0] [char](1) NOT NULL,
	[repeat_day1] [char](1) NOT NULL,
	[repeat_day2] [char](1) NOT NULL,
	[repeat_day3] [char](1) NOT NULL,
	[repeat_day4] [char](1) NOT NULL,
	[repeat_day5] [char](1) NOT NULL,
	[repeat_day6] [char](1) NOT NULL,
	[user_id] [int] NOT NULL,
	[remarks] [varchar](80) NULL,
	[last_updated] [datetime] NOT NULL,
	[enabled] [char](1) NOT NULL,
	[repeat_minute] [int] NOT NULL,
	[dates] [date] NULL,
 CONSTRAINT [PK_net_scan_scheduler] PRIMARY KEY CLUSTERED 
(
	[net_scan_scheduler_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[net_scan_scheduler] ADD  CONSTRAINT [DF_net_scan_scheduler_8]  DEFAULT ('E') FOR [repeat_pattern]
GO

ALTER TABLE [dbo].[net_scan_scheduler] ADD  CONSTRAINT [DF_net_scan_scheduler_1]  DEFAULT ('N') FOR [repeat_day0]
GO

ALTER TABLE [dbo].[net_scan_scheduler] ADD  CONSTRAINT [DF_net_scan_scheduler_2]  DEFAULT ('N') FOR [repeat_day1]
GO

ALTER TABLE [dbo].[net_scan_scheduler] ADD  CONSTRAINT [DF_net_scan_scheduler_3]  DEFAULT ('N') FOR [repeat_day2]
GO

ALTER TABLE [dbo].[net_scan_scheduler] ADD  CONSTRAINT [DF_net_scan_scheduler_4]  DEFAULT ('N') FOR [repeat_day3]
GO

ALTER TABLE [dbo].[net_scan_scheduler] ADD  CONSTRAINT [DF_net_scan_scheduler_5]  DEFAULT ('N') FOR [repeat_day4]
GO

ALTER TABLE [dbo].[net_scan_scheduler] ADD  CONSTRAINT [DF_net_scan_scheduler_6]  DEFAULT ('N') FOR [repeat_day5]
GO

ALTER TABLE [dbo].[net_scan_scheduler] ADD  CONSTRAINT [DF_net_scan_scheduler_7]  DEFAULT ('N') FOR [repeat_day6]
GO

ALTER TABLE [dbo].[net_scan_scheduler] ADD  CONSTRAINT [DF_net_scan_scheduler_9]  DEFAULT ((90001)) FOR [user_id]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'스케쥴 시작 예약일, NULL=당장.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'net_scan_scheduler', @level2type=N'COLUMN',@level2name=N'schedule_time'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'반복 패턴: E=Everyday, D:Dedicate Days(일월화수목금토 선택).' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'net_scan_scheduler', @level2type=N'COLUMN',@level2name=N'repeat_pattern'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'일요일, Y=Yes, N=No' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'net_scan_scheduler', @level2type=N'COLUMN',@level2name=N'repeat_day0'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'단말 검색을 위한 구성' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'net_scan_scheduler'
GO



EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'net_scan'

GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'net_scan', @level2type=N'COLUMN',@level2name=N'end_ipv4'

GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'net_scan', @level2type=N'COLUMN',@level2name=N'start_ipv4'

GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'net_scan', @level2type=N'COLUMN',@level2name=N'subnet'

GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'net_scan', @level2type=N'COLUMN',@level2name=N'net_addr'

GO

/****** Object:  Table [dbo].[net_scan]    Script Date: 2016-04-01 오후 2:14:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING OFF
GO

CREATE TABLE [dbo].[net_scan](
	[net_id] [int] IDENTITY(19110001,1) NOT NULL,
	[net_name] [varchar](40) NOT NULL,
	[net_addr] [varchar](15) NOT NULL,
	[subnet] [varchar](15) NOT NULL,
	[start_ipv4] [varchar](15) NOT NULL,
	[end_ipv4] [varchar](15) NOT NULL,
	[user_id] [int] NOT NULL,
	[remarks] [varchar](80) NULL,
	[last_updated] [datetime] NOT NULL,
	[l3_sw_asset_id] [int] NULL,
	[net_scan_scheduler_id] [int] NULL,
 CONSTRAINT [PK_net_scan] PRIMARY KEY CLUSTERED 
(
	[net_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[net_scan] ADD  CONSTRAINT [DF_net_scan_1]  DEFAULT ((90001)) FOR [user_id]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'네트워크 주소로 (192.168.10.0)과 같이 입력.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'net_scan', @level2type=N'COLUMN',@level2name=N'net_addr'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'서브넷 주소로 (255.255.255.0)과 같이 입력.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'net_scan', @level2type=N'COLUMN',@level2name=N'subnet'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'IP 시작 주소로 (192.168.10.11)과 같이 입력.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'net_scan', @level2type=N'COLUMN',@level2name=N'start_ipv4'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'IP 종료 주소로 (192.168.10.99)와 같이 입력.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'net_scan', @level2type=N'COLUMN',@level2name=N'end_ipv4'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'단말 검색을 위한 구성' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'net_scan'
GO




/****** Object:  Table [dbo].[net_scan_sw]    Script Date: 2016-04-01 오후 2:22:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[net_scan_sw](
	[net_scan_sw_id] [int] IDENTITY(1,1) NOT NULL,
	[net_id] [int] NOT NULL,
	[sw_asset_id] [int] NOT NULL,
 CONSTRAINT [PK_net_scan_sw] PRIMARY KEY CLUSTERED 
(
	[net_scan_sw_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[net_scan_sw]  WITH CHECK ADD  CONSTRAINT [FK_net_scan_sw_net_scan] FOREIGN KEY([net_id])
REFERENCES [dbo].[net_scan] ([net_id])
GO

ALTER TABLE [dbo].[net_scan_sw] CHECK CONSTRAINT [FK_net_scan_sw_net_scan]
GO


/*************************************************************************************************/
/*************************************************************************************************/
/*************************************************************************************************/
/****** romee 스위치 처리  지원용  ******/
/*************************************************************************************************/
/*************************************************************************************************/



/****** romee 스위치 처리  지원용  ******/
alter table asset_aux alter column sw_vlan varchar(50) 

/****** romee 인가/비안가 처리  지원용  ******/
alter table asset_terminal alter column [terminal_grant] int


/****** romee 스마트폰 지원용  ******/
DROP TABLE [dbo].[work_order_task]
GO
/****** Object:  Table [dbo].[work_order_task]    Script Date: 2016-01-06 오전 8:23:08 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[work_order_task](
	[work_order_task_id] [int] IDENTITY(1,1) NOT NULL,
	[wo_id] [int] NOT NULL,
	[task_no] [int] NOT NULL,
	[remote_asset_type] [char](1) NOT NULL,
	[remote_ic_asset_id] [int] NOT NULL,
	[remote_asset_id] [int] NOT NULL,
	[remote_port_no] [int] NOT NULL,
	[command_type] [char](1) NOT NULL,
	[task_result] [char](1) NOT NULL,
	[write_time] [datetime] NOT NULL,
	[port_no] [int] NOT NULL,
	[smartphone] [int] NULL,
 CONSTRAINT [PK_work_order_task] PRIMARY KEY CLUSTERED 
(
	[work_order_task_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[work_order_task] ADD  CONSTRAINT [DF_work_order_task_2]  DEFAULT ('P') FOR [remote_asset_type]
GO

ALTER TABLE [dbo].[work_order_task] ADD  CONSTRAINT [DF_work_order_task_1]  DEFAULT ('R') FOR [task_result]
GO

ALTER TABLE [dbo].[work_order_task] ADD  DEFAULT ((1)) FOR [smartphone]
GO

ALTER TABLE [dbo].[work_order_task]  WITH CHECK ADD  CONSTRAINT [FK_work_order_task_work_order] FOREIGN KEY([wo_id])
REFERENCES [dbo].[work_order] ([wo_id])
GO

ALTER TABLE [dbo].[work_order_task] CHECK CONSTRAINT [FK_work_order_task_work_order]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'P:Patch Panel for XC, S:Switch for IC' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'work_order_task', @level2type=N'COLUMN',@level2name=N'remote_asset_type'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'C:Connect, D:Disconnect' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'work_order_task', @level2type=N'COLUMN',@level2name=N'command_type'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'처리결과: R=Registered(등록), S=Successed(성공), C=Canceled(취소)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'work_order_task', @level2type=N'COLUMN',@level2name=N'task_result'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'작업 지시에 대한 각 태스크(한 레코드는 한 개의 연결에 대한 정보를 가지고 있다.)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'work_order_task'
GO


/****** romee 스마트폰 지원용  ******/
DROP PROCEDURE [dbo].[sp_list_work_order1]
GO
/****** Object:  StoredProcedure [dbo].[sp_list_work_order1]    Script Date: 2016-01-06 오전 8:24:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[sp_list_work_order1]
	@iwo_id INT = 0
as
select a.*, b.smartphone from work_order a, work_order_task b 
where a.wo_id = @iwo_id
and a.wo_id = b.wo_id
and b.task_no =1
 
GO


/****** Object:  StoredProcedure [dbo].[sp_list_work_order2]    Script Date: 2016-01-06 오전 8:24:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP PROCEDURE [dbo].[sp_list_work_order2]
GO


CREATE proc [dbo].[sp_list_work_order2]
as
select a.*, b.smartphone from work_order a, work_order_task b 
where a.wo_id = b.wo_id
and b.task_no =1
 




GO


/****** romee 네트웍 스캔 처리  지원용  ******/
DROP PROCEDURE [dbo].[sp_list_asset_port_link_sw]
GO

/****** Object:  StoredProcedure [dbo].[sp_list_asset_port_link_sw]    Script Date: 2016-01-26 오전 9:14:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Batch submitted through debugger: SQLQuery25.sql|7|0|C:\Users\jake\AppData\Local\Temp\~vsD4AA.sql










CREATE proc [dbo].[sp_list_asset_port_link_sw]
	@floor_id INT = 0
as
BEGIN






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
DECLARE @NUM_PORT INT
DECLARE @ROW_NUM INT

DECLARE @Table TABLE
(
	[asset_id] [int] NOT NULL,
	[asset_name] [varchar](80) NOT NULL,
	[port_no] [int] NOT NULL,
	[catalog_id] [int] NOT NULL,
	[catalog_group_id] [int] NOT NULL,
	[front_asset_id] [int] NULL,
	[front_port_no] [int] NULL,
	[rear_asset_id] [int] NULL,
	[rear_port_no] [int] NULL,
	[pos] [int] NULL,
	[ipp_port_status] [char](1) NULL,
	[alarm_status] [char](1) NULL,
	[wo_status] [char](1) NULL
);


SET @ROW_NUM = 0
delete from xtable_lo

DECLARE	@return_value	int
DECLARE	@asset_id_out	int

DECLARE MAIN_C CURSOR FOR 
SELECT a.asset_id
FROM asset a, catalog b , location c
where catalog_group_id = 3320 
and a.catalog_id = b.catalog_id
and a.location_id = c.location_id
and c.floor_id = @floor_id
-- and c.floor_id = 89200008

OPEN MAIN_C
FETCH NEXT FROM MAIN_C INTO @asset_id_out


WHILE(@@FETCH_STATUS = 0)
BEGIN
	SELECT @FIND2_ASSET_ID = asset_id, @NUM_PORT = b.num_of_ports FROM asset a, catalog b WHERE asset_id = @asset_id_out and a.catalog_id = b.catalog_id

	IF (@FIND2_ASSET_ID IS NULL)
	BEGIN
		SELECT 'Asset id does not found'
		RETURN
	END

	IF (@NUM_PORT IS NULL)
	BEGIN
		SELECT 'Asset port num_port does not found'
		RETURN
	END

	SET @FIND2_PORT_NO = 1

	while @FIND2_PORT_NO < @NUM_PORT+1
		BEGIN
		-- 스위치로 돌기 
			SET @FIND_ASSET_ID = @FIND2_ASSET_ID
			SET @FIND_PORT_NO = @FIND2_PORT_NO
			SET @FIND_PLUG_SIDE = 'F'
			WHILE (1=1)
   				BEGIN
				 insert into @table
				 SELECT 
					a.asset_id, 
					a.asset_name, 
					apl.port_no, 
					c.catalog_id, 
					c.catalog_group_id, 
					apl.front_asset_id,
					apl.front_port_no,
					apl.rear_asset_id,
					apl.rear_port_no,
					case when catalog_group_id=3320 then 1  
						 when catalog_group_id=3130 then 2  
						 when catalog_group_id=3130 then 3  
						 when catalog_group_id=3420 then 4  
						 when catalog_group_id=3340 then 5 else 0 end  pos,  
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

		-- 아웃렛으로 돌기 
			SET @FIND_ASSET_ID = @FIND2_ASSET_ID
			SET @FIND_PORT_NO = @FIND2_PORT_NO
			SET @FIND_PLUG_SIDE = 'R'
			WHILE (1=1)
   				BEGIN
				 insert into @table
				 SELECT 
					a.asset_id, 
					a.asset_name, 
					apl.port_no, 
					c.catalog_id, 
					c.catalog_group_id, 
					apl.front_asset_id,
					apl.front_port_no,
					apl.rear_asset_id,
					apl.rear_port_no,
					case when catalog_group_id=3320 then 1  
						 when catalog_group_id=3130 then 2  
						 when catalog_group_id=3130 then 3  
						 when catalog_group_id=3420 then 4  
						 when catalog_group_id=3340 then 5 else 0 end  pos,  
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

	--	select * from @Table a order by pos -- debug
		SET @ROW_NUM = @ROW_NUM + 1
		insert into xtable_lo (id) values(@ROW_NUM);

		DECLARE @id int   -- 커서에서 사용할 변수 선언
		DECLARE @id_name varchar(80)   -- 커서에서 사용할 변수 선언
		DECLARE @rid int   -- 커서에서 사용할 변수 선언
		DECLARE @no int   -- 커서에서 사용할 변수 선언
		DECLARE @pos int   -- 커서에서 사용할 변수 선언
		DECLARE @sw int   -- 커서에서 사용할 변수 선언
		DECLARE @fid int   -- 커서에서 사용할 변수 선언

		set @rid = 0
		set @sw = 0

		DECLARE tmp_Cursor CURSOR FOR   -- 커서를 아래 select문에 해당하는 커서 선언
			select asset_id, port_no, pos, rear_asset_id, asset_name, front_asset_id from @Table a order by pos
		OPEN tmp_Cursor                   -- 커서 오픈
		FETCH next FROM tmp_Cursor INTO @id, @no, @pos, @rid, @id_name, @fid          -- fetch를 통해 레코드 이동 후 변수에 삽입
		WHILE @@FETCH_STATUS = 0           -- 0은 성공, -1은 실패(커서위치잘못), -2는 실패(레코드없음)
		BEGIN    
			if @pos=1
				begin 
					update xtable_lo set id1 = @id, no1= @no, id_name1 = @id_name where id = @ROW_NUM 
					set @sw = @fid
				end
			else if @pos= 2
				begin
					if @sw = @id
						update xtable_lo set id2 = @id, no2= @no, id_name2 = @id_name where id = @ROW_NUM
					else
						update xtable_lo set id3 = @id, no3= @no, id_name3 = @id_name where id = @ROW_NUM
					if @sw = @id
						update xtable_lo set rid = @id where id = @ROW_NUM
				end
			else if @pos= 4
				update xtable_lo set id4 = @id, no4= @no, id_name4 = @id_name where id = @ROW_NUM 
			else if @pos= 5
				update xtable_lo set id5 = @id, no4= @no, id_name5 = @id_name where id = @ROW_NUM
		 
		 -- SELECT @id, @no, @pos, @rid
		 FETCH next FROM tmp_Cursor INTO @id, @no, @pos, @rid, @id_name, @fid  
		END
		CLOSE tmp_Cursor                                        -- 커서 클로즈
		DEALLOCATE tmp_Cursor                              -- 커서 메모리 반환

		delete from @Table
		SET @FIND2_PORT_NO=@FIND2_PORT_NO+1
	END  -- 24 while


	FETCH NEXT FROM MAIN_C INTO @asset_id_out
END
CLOSE MAIN_C
DEALLOCATE MAIN_C

select xtable_lo.*, asset_tree.location_id, location.floor_id, location.room_id, location.rack_id, location.location_path 
from xtable_lo, asset_tree, location
where  xtable_lo.id1 = asset_tree.asset_id
and asset_tree.location_id = location.location_id  
order by xtable_lo.id1, xtable_lo.no1  



END



/****** romee 네트웍 스캔 처리  지원용  ******/


GO

DROP PROCEDURE [dbo].[sp_list_asset_port_link3]
GO

/****** Object:  StoredProcedure [dbo].[sp_list_asset_port_link3]    Script Date: 2016-01-26 오전 9:14:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO









CREATE proc [dbo].[sp_list_asset_port_link3]
	@ASSET_ID INT = 0
as
BEGIN


-- DECLARE @ASSET_ID INT
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


DECLARE @Table TABLE
(
	[asset_id] [int] NOT NULL,
	[asset_name] [varchar](80) NOT NULL,
	[port_no] [int] NOT NULL,
	[catalog_id] [int] NOT NULL,
	[catalog_group_id] [int] NOT NULL,
	[front_asset_id] [int] NULL,
	[front_port_no] [int] NULL,
	[rear_asset_id] [int] NULL,
	[rear_port_no] [int] NULL,
	[pos] [int] NULL,
	[ipp_port_status] [char](1) NULL,
	[alarm_status] [char](1) NULL,
	[wo_status] [char](1) NULL
);

--DECLARE @Table2 char(30)
--SET  @Table2 = 'xtable'

--DECLARE @Table2 TABLE
--(
--	[id] [int] NULL,
--	[id1] [int] NULL,
--	[no1] [int] NULL,
--	[id2] [int] NULL,
--	[no2] [int] NULL,
--	[id3] [int] NULL,
--	[no3] [int] NULL,
--	[id4] [int] NULL,
--	[no4] [int] NULL,
--	[id5] [int] NULL,
--	[no5] [int] NULL,
--	[rid] [int] NULL
--);

-- SELECT @FIND2_ASSET_ID = asset_id FROM asset WHERE asset_name = 'Test IPP-102'
-- SET @ASSET_ID = 59002909
SELECT @FIND2_ASSET_ID = asset_id FROM asset WHERE asset_id = @ASSET_ID
SET @FIND2_PORT_NO = 1

IF (@FIND2_ASSET_ID IS NULL)
BEGIN
    SELECT 'Asset name does not found'
	RETURN
END

delete from xtable

while @FIND2_PORT_NO < 25
	BEGIN
	-- 스위치로 돌기 
		SET @FIND_ASSET_ID = @ASSET_ID
		SET @FIND_PORT_NO = @FIND2_PORT_NO
		SET @FIND_PLUG_SIDE = 'F'
	    WHILE (1=1)
   			BEGIN
			 insert into @table
			 SELECT 
				a.asset_id, 
				a.asset_name, 
				apl.port_no, 
				c.catalog_id, 
				c.catalog_group_id, 
				apl.front_asset_id,
				apl.front_port_no,
				apl.rear_asset_id,
				apl.rear_port_no,
				case when catalog_group_id=3320 then 1  
					 when catalog_group_id=3130 then 2  
					 when catalog_group_id=3130 then 3  
					 when catalog_group_id=3420 then 4  
					 when catalog_group_id=3340 then 5 else 0 end  pos,  
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

	-- 아웃렛으로 돌기 
		SET @FIND_ASSET_ID = @ASSET_ID
		SET @FIND_PORT_NO = @FIND2_PORT_NO
		SET @FIND_PLUG_SIDE = 'R'
	    WHILE (1=1)
   			BEGIN
			 insert into @table
			 SELECT 
				a.asset_id, 
				a.asset_name, 
				apl.port_no, 
				c.catalog_id, 
				c.catalog_group_id, 
				apl.front_asset_id,
				apl.front_port_no,
				apl.rear_asset_id,
				apl.rear_port_no,
				case when catalog_group_id=3320 then 1  
					 when catalog_group_id=3130 then 2  
					 when catalog_group_id=3130 then 3  
					 when catalog_group_id=3420 then 4  
					 when catalog_group_id=3340 then 5 else 0 end  pos,  
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

--	select * from @Table a order by pos -- debug

	insert into xtable (id) values(@FIND2_PORT_NO);

	DECLARE @id int   -- 커서에서 사용할 변수 선언
	DECLARE @id_name varchar(80)   -- 커서에서 사용할 변수 선언
	DECLARE @rid int   -- 커서에서 사용할 변수 선언
	DECLARE @no int   -- 커서에서 사용할 변수 선언
	DECLARE @pos int   -- 커서에서 사용할 변수 선언
	DECLARE @sw int   -- 커서에서 사용할 변수 선언

	set @rid = 0
	set @sw = 0

	DECLARE tmp_Cursor CURSOR FOR   -- 커서를 아래 select문에 해당하는 커서 선언
		select asset_id, port_no, pos, rear_asset_id, asset_name from @Table a order by pos
	OPEN tmp_Cursor                   -- 커서 오픈
	FETCH next FROM tmp_Cursor INTO @id, @no, @pos, @rid, @id_name          -- fetch를 통해 레코드 이동 후 변수에 삽입
	WHILE @@FETCH_STATUS = 0           -- 0은 성공, -1은 실패(커서위치잘못), -2는 실패(레코드없음)
	BEGIN    
		if @pos=1
			begin 
				update xtable set id1 = @id, no1= @no, id_name1 = @id_name where id = @FIND2_PORT_NO 
				set @sw = @id
			end
		else if @pos= 2
			begin
				if @FIND2_ASSET_ID = @id
					update xtable set id2 = @id, no2= @no, id_name2 = @id_name where id = @FIND2_PORT_NO
				else
					update xtable set id3 = @id, no3= @no, id_name3 = @id_name where id = @FIND2_PORT_NO
				if @sw = @rid
					update xtable set rid = @id where id = @FIND2_PORT_NO
			end
		else if @pos= 4
			update xtable set id4 = @id, no4= @no, id_name4 = @id_name where id = @FIND2_PORT_NO 
		else if @pos= 5
			update xtable set id5 = @id, no4= @no, id_name5 = @id_name where id = @FIND2_PORT_NO
		 
	 -- SELECT @id, @no, @pos, @rid
	 FETCH next FROM tmp_Cursor INTO @id, @no, @pos, @rid, @id_name  
	END
	CLOSE tmp_Cursor                                        -- 커서 클로즈
	DEALLOCATE tmp_Cursor                              -- 커서 메모리 반환

	delete from @Table
	SET @FIND2_PORT_NO=@FIND2_PORT_NO+1
END  -- 24 while

select * from xtable 
where id2 = @ASSET_ID

END


/****** romee 네트웍 스캔 처리  지원용  ******/


GO
/****** Object:  StoredProcedure [dbo].[sp_list_event_hist2]    Script Date: 2016-01-26 오전 9:14:15 ******/

DROP PROCEDURE [dbo].[sp_list_event_hist2]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE proc [dbo].[sp_list_event_hist2]
as
select * from event_hist
where DATEDIFF ( month , write_time , getdate())  = DATEDIFF ( month , getdate() , getdate())


GO
/****** Object:  StoredProcedure [dbo].[sp_list_event_hist3]    Script Date: 2016-01-26 오전 9:14:15 ******/

DROP PROCEDURE [dbo].[sp_list_event_hist3]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


/****** romee 네트웍 스캔 처리  지원용  ******/


CREATE proc [dbo].[sp_list_event_hist3]
as
select * from event_hist 
where (CONVERT(varchar(10), write_time,111) = CONVERT(varchar(10), GETDATE(),111))
order by event_hist_id desc

GO
/****** Object:  StoredProcedure [dbo].[sp_list_event_hist4]    Script Date: 2016-01-26 오전 9:14:15 ******/

DROP PROCEDURE [dbo].[sp_list_event_hist4]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



/****** romee 네트웍 스캔 처리  지원용  ******/


CREATE proc [dbo].[sp_list_event_hist4]
	@ievent_hist_id INT = 0
as
select * from event_hist 
where event_hist_id = @ievent_hist_id


GO


DROP TABLE [dbo].[xtable_lo]
GO

/****** Object:  Table [dbo].[xtable_lo]    Script Date: 2016-01-25 오후 5:38:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[xtable_lo](
	[id] [int] NOT NULL,
	[id1] [int] NULL,
	[no1] [int] NULL,
	[id_name1] [varchar](80) NULL,
	[id2] [int] NULL,
	[no2] [int] NULL,
	[id_name2] [varchar](80) NULL,
	[id3] [int] NULL,
	[no3] [int] NULL,
	[id_name3] [varchar](80) NULL,
	[id4] [int] NULL,
	[no4] [int] NULL,
	[id_name4] [varchar](80) NULL,
	[id5] [int] NULL,
	[no5] [int] NULL,
	[id_name5] [varchar](80) NULL,
	[rid] [int] NULL,
 CONSTRAINT [PK_xtable_lo] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


/****** Object:  Table [dbo].[xtable_ts]    Script Date: 2016-01-25 오후 5:38:10 ******/
DROP TABLE [dbo].[xtable_ts]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[xtable_ts](
	[id] [int] NOT NULL,
	[id1] [int] NULL,
	[no1] [int] NULL,
	[id_name1] [varchar](80) NULL,
	[id2] [int] NULL,
	[no2] [int] NULL,
	[id_name2] [varchar](80) NULL,
	[id3] [int] NULL,
	[no3] [int] NULL,
	[id_name3] [varchar](80) NULL,
	[id4] [nvarchar](50) NULL,
	[no4] [int] NULL,
	[id_name4] [varchar](80) NULL,
	[id5] [int] NULL,
	[no5] [int] NULL,
	[id_name5] [varchar](80) NULL,
	[rid] [nchar](10) NULL,
 CONSTRAINT [PK_xtable_ts] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO



/****** Object:  Table [dbo].[xtable]    Script Date: 2016-01-25 오후 5:37:57 ******/
SET ANSI_NULLS ON
GO

DROP TABLE [dbo].[xtable]
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[xtable](
	[id] [int] NOT NULL,
	[id1] [int] NULL,
	[no1] [int] NULL,
	[id_name1] [varchar](80) NULL,
	[id2] [int] NULL,
	[no2] [int] NULL,
	[id_name2] [varchar](80) NULL,
	[id3] [int] NULL,
	[no3] [int] NULL,
	[id_name3] [varchar](80) NULL,
	[id4] [int] NULL,
	[no4] [int] NULL,
	[id_name4] [varchar](80) NULL,
	[id5] [int] NULL,
	[no5] [int] NULL,
	[id_name5] [varchar](80) NULL,
	[rid] [int] NULL,
 CONSTRAINT [PK_xtable] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

/****** romee 환경  지원용  ******/

/****** Object:  Table [dbo].[site_environment]    Script Date: 2016-03-31 오후 3:04:23 ******/
DROP TABLE [dbo].[site_environment]
GO

/****** Object:  Table [dbo].[site_environment]    Script Date: 2016-03-31 오후 3:04:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[site_environment](
	[site_environment_id] [int] IDENTITY(59000001,1) NOT NULL,
	[site_id] [int] NOT NULL,
	[high_power_color] [int] NULL,
	[low_power_color] [int] NULL,
	[high_temp_color] [int] NULL,
	[low_temp_color] [int] NULL,
	[high_humi_color] [int] NULL,
	[low_humi_color] [int] NULL,
	[high_current_th] [int] NULL,
	[high_power_th] [int] NULL,
	[high_powerh_th] [int] NULL,
	[high_temp_th] [int] NULL,
	[low_temp_th] [int] NULL,
	[high_humi_th] [int] NULL,
	[low_humi_th] [int] NULL,
	[asset_id] [int] NULL,
 CONSTRAINT [PK_site_environment] PRIMARY KEY CLUSTERED 
(
	[site_environment_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


