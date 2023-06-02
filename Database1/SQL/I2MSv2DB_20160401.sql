/***************************************************************************************
 *
 * I2MS base data script for I2MS v2.0 
 *
 * Release v0.1
 *
 * Date 2014.12.22
 *
 * by Jake, Moon
 *
 ***************************************************************************************/

USE [i2ms2]
GO

DELETE [dbo].[changed_link_hist_cell]
GO

DELETE [dbo].[changed_link_hist]
GO

DELETE [dbo].[work_order_task]
GO

DELETE [dbo].[work_order]
GO

DELETE [dbo].[template_column]
GO

DELETE [dbo].[template]
GO

DELETE [dbo].[report_lang_column]
GO

DELETE [dbo].[report_lang]
GO

DELETE [dbo].[report]
GO

DELETE [dbo].[asset_terminal_ip]
GO

DELETE [dbo].[asset_terminal]
GO

DELETE [dbo].[net_scan_scheduler]
GO

DELETE [dbo].[net_scan_sw]
GO

DELETE [dbo].[net_scan]
GO

DELETE [dbo].event_hist
GO

DELETE [dbo].[event_lang]
GO

DELETE [dbo].[event]
GO

DELETE [dbo].language
GO

DELETE [dbo].fw_upgrade_hist
GO

DELETE [dbo].fw_upgrade
GO

DELETE [dbo].asset_ext
GO

DELETE [dbo].catalog_ext
GO

DELETE [dbo].[ext_property_ans]
GO

DELETE [dbo].[ext_property]
GO

DELETE [dbo].[favorite_tree]
GO

DELETE [dbo].[location]
GO

DELETE [dbo].[eb_port_data_hour]
GO

DELETE [dbo].[eb_port_data_cur]
GO

DELETE [dbo].[eb_port_config]
GO

DELETE [dbo].user_port_layout
GO

DELETE [dbo].asset_ipp_port_link
GO

DELETE [dbo].asset_port_link
GO

DELETE [dbo].ipp_connect_status
GO

DELETE [dbo].ic_connect_status
GO

DELETE [dbo].ic_ipp_config
GO

DELETE [dbo].sw_card_config
GO

DELETE [dbo].asset_aux
GO

DELETE [dbo].asset
GO

DELETE [dbo].catalog
GO

DELETE [dbo].[contact]
GO

DELETE [dbo].[manufacture]
GO

DELETE [dbo].[asset_tree]
GO

DELETE [dbo].[site_user]
GO

DELETE [dbo].[user]
GO

DELETE [dbo].rack_config
GO

DELETE [dbo].rack
GO

DELETE [dbo].room
GO

DELETE [dbo].floor
GO

DELETE [dbo].building
GO

DELETE [dbo].site
GO

DELETE [dbo].region2
GO

DELETE [dbo].region1
GO

DELETE [dbo].drawing_3d
GO

DELETE [dbo].[image]
GO

DELETE [dbo].[image_type]
GO

DELETE [dbo].[catalog_group]
GO

DELETE [dbo].code
GO

DELETE [dbo].update_func
GO



/************************************************************************
 *
 * code
 *
 ************************************************************************/
--1 01 xxxx 		19010001 : contact
--1 02 xxxx			19020001 : manufacture
--1 03 xxxx 		19030001 : location
--1 04 xxxx			19040001 : drawing_3d
--1 05 xxxx			19050001 : ext_property
--1 06 xxxx			19060001 : fw_upgrade_hist
--1 07 xxxx 		19070001 : fw_upgrade
--1 08 xxxx			19080001 : language
--1 09 xxxx			19090001 : net_scheduler
--1 10 xxxx			19100001 : event
--1 11 xxxx			19110001 : event_hist
--1 12 xxxx			19120001 : report
--1 13 xxxx			19130001 : template
--1 14 xxxx			19140001 : work_order
--1 15 xxxx			19150001 : changed_link_hist
--1 16 xxxx			19160001 : image_type

--1 20 xxxx			19200001 : asset_tree
--1 21 xxxx			19210001 : favoriate
--1 22 xxxx			19220001 : favoriate_tree

INSERT INTO [dbo].code ([fixed_code_no], [start_code_no], [code_name], [remarks])	VALUES (1010001,	19010001, 'contact', '연락처');
INSERT INTO [dbo].code ([fixed_code_no], [start_code_no], [code_name], [remarks])	VALUES (1020001,	19020001, 'manual', '제조사');
INSERT INTO [dbo].code ([fixed_code_no], [start_code_no], [code_name], [remarks])	VALUES (1030001,	19030001, 'location', '위치');
INSERT INTO [dbo].code ([fixed_code_no], [start_code_no], [code_name], [remarks])	VALUES (1040001,	19040001,	'drawing_3d', '3D 도면');
INSERT INTO [dbo].code ([fixed_code_no], [start_code_no], [code_name], [remarks])	VALUES (1050001,	19050001,	'ext_property', '확장속성');
INSERT INTO [dbo].code ([fixed_code_no], [start_code_no], [code_name], [remarks])	VALUES (1060001,	19060001,	'fw_upgrade_hist', '펌웨어업그레이드 히스토리');
INSERT INTO [dbo].code ([fixed_code_no], [start_code_no], [code_name], [remarks])	VALUES (1070001,	19070001,	'fw_upgrade', '펌웨어업그레이드 목록');
INSERT INTO [dbo].code ([fixed_code_no], [start_code_no], [code_name], [remarks])	VALUES (1080001,	19080001,	'language', '언어설정');
INSERT INTO [dbo].code ([fixed_code_no], [start_code_no], [code_name], [remarks])	VALUES (1090001,	19090001,	'event', '이벤트');
INSERT INTO [dbo].code ([fixed_code_no], [start_code_no], [code_name], [remarks])	VALUES (1100001,	19100001,	'event_hist', '이벤트 히스토리');
INSERT INTO [dbo].code ([fixed_code_no], [start_code_no], [code_name], [remarks])	VALUES (1110001,	19110001,	'net_scheduler', '네트워크 스케쥴러');
INSERT INTO [dbo].code ([fixed_code_no], [start_code_no], [code_name], [remarks])	VALUES (1120001,	19120001,	'report', '리포트');
INSERT INTO [dbo].code ([fixed_code_no], [start_code_no], [code_name], [remarks])	VALUES (1130001,	19130001,	'template', '템플릿');
INSERT INTO [dbo].code ([fixed_code_no], [start_code_no], [code_name], [remarks])	VALUES (1140001,	19140001,	'work_order', '작업지시');
INSERT INTO [dbo].code ([fixed_code_no], [start_code_no], [code_name], [remarks])	VALUES (1150001,	19150001,	'changed_link_hist', '변경된 링크다이어그램 히스토리');
INSERT INTO [dbo].code ([fixed_code_no], [start_code_no], [code_name], [remarks])	VALUES (1160001,	19160001,	'image_type', '이미지 타입');
INSERT INTO [dbo].code ([fixed_code_no], [start_code_no], [code_name], [remarks])	VALUES (1200001,	19200001, 'asset_tree', '자산트리');
INSERT INTO [dbo].code ([fixed_code_no], [start_code_no], [code_name], [remarks])	VALUES (1210001,	19210001, 'favoriate', '즐겨찾기');
INSERT INTO [dbo].code ([fixed_code_no], [start_code_no], [code_name], [remarks])	VALUES (1220001,	19220001, 'favoriate_tree', '즐겨찾기트리');
INSERT INTO [dbo].code ([fixed_code_no], [start_code_no], [code_name], [remarks])	VALUES (1230001,	19230001, 'asset_terminal_ip', '단말 IP');

--2 xx xxxx			29000001 : 이미지
INSERT INTO [dbo].code ([fixed_code_no], [start_code_no], [code_name], [remarks])	VALUES (2000001, 29000001, 'image', '이미지');

 --3 x x x			39000001 : 카탈로그 그룹 (대분류-중분류-소분류)
INSERT INTO [dbo].code ([fixed_code_no], [start_code_no], [code_name], [remarks])	VALUES (3001, 39000001, 'catalog group', '카탈로그 그룹');

 --4 x x x xx		49000001 : 카탈로그 (대분류-중분류-소분류-카탈로그)
INSERT INTO [dbo].code ([fixed_code_no], [start_code_no], [code_name], [remarks])	VALUES (400001, 49000001, 'catalog', '카탈로그');

 --5 x x x xx xxx	59000001 : 자산 (대분류-중분류-소분류-카탈로그-자산)
INSERT INTO [dbo].code ([fixed_code_no], [start_code_no], [code_name], [remarks])	VALUES (500000001, 59000001, 'asset', '자산');

 --6 xxxxxxx			69000001 : 단말
INSERT INTO [dbo].code ([fixed_code_no], [start_code_no], [code_name], [remarks])	VALUES (60000001, 69000001, 'asset_terminal', '단말');

 --7 xx 00 00		79100001 : 지역1
 --7 xx xx 00		79200001 : 지역2
 --7 xx xx xx		79300001 : 사이트
INSERT INTO [dbo].code ([fixed_code_no], [start_code_no], [code_name], [remarks])	VALUES (7000000, 79100001, 'region1', '지역선택-1');
INSERT INTO [dbo].code ([fixed_code_no], [start_code_no], [code_name], [remarks])	VALUES (7000000, 79200001, 'region2', '지역선택-2');
INSERT INTO [dbo].code ([fixed_code_no], [start_code_no], [code_name], [remarks])	VALUES (7000000, 79300001, 'site', '사이트');

 --8 xx 00 00 00		89100001 : 빌딩
 --8 xx xx 00 00		89200001 : 층
 --8 xx xx 00 00		89300001 : 룸
 --8 xx xx xx 00		89400001 : 랙
INSERT INTO [dbo].code ([fixed_code_no], [start_code_no], [code_name], [remarks])	VALUES (800000000, 89100001, 'building', '빌딩');
INSERT INTO [dbo].code ([fixed_code_no], [start_code_no], [code_name], [remarks])	VALUES (800000000, 89200001, 'floor', '층');
INSERT INTO [dbo].code ([fixed_code_no], [start_code_no], [code_name], [remarks])	VALUES (800000000, 89300001, 'room', '룸');
INSERT INTO [dbo].code ([fixed_code_no], [start_code_no], [code_name], [remarks])	VALUES (800000000, 89400001, 'rack', '랙');

 --9 xxxx		99000001 : 사용자
INSERT INTO [dbo].code ([fixed_code_no], [start_code_no], [code_name], [remarks])	VALUES (90001, 99000001, 'user', '사용자');



/************************************************************************
 *
 * language
 *
 ************************************************************************/

SET IDENTITY_INSERT [dbo].language ON
INSERT INTO [dbo].language ([lang_id], [lang_name])	VALUES (1080001, '한국어');
INSERT INTO [dbo].language ([lang_id], [lang_name])	VALUES (1080002, '영어');
SET IDENTITY_INSERT [dbo].language OFF



/************************************************************************
 *
 * event
 *
 ************************************************************************/

 SET IDENTITY_INSERT [dbo].[event] ON 
INSERT [dbo].[event] ([event_id], [event_type], [popup_screen], [send_email], [send_sms]) VALUES (1090001, N'I', N'N', N'N', N'N')
INSERT [dbo].[event] ([event_id], [event_type], [popup_screen], [send_email], [send_sms]) VALUES (1090002, N'I', N'N', N'N', N'N')
INSERT [dbo].[event] ([event_id], [event_type], [popup_screen], [send_email], [send_sms]) VALUES (1090003, N'I', N'N', N'N', N'N')
INSERT [dbo].[event] ([event_id], [event_type], [popup_screen], [send_email], [send_sms]) VALUES (1090004, N'I', N'N', N'N', N'N')
INSERT [dbo].[event] ([event_id], [event_type], [popup_screen], [send_email], [send_sms]) VALUES (1090005, N'I', N'N', N'N', N'N')
INSERT [dbo].[event] ([event_id], [event_type], [popup_screen], [send_email], [send_sms]) VALUES (1090006, N'I', N'N', N'N', N'N')
INSERT [dbo].[event] ([event_id], [event_type], [popup_screen], [send_email], [send_sms]) VALUES (1090007, N'I', N'N', N'N', N'N')
INSERT [dbo].[event] ([event_id], [event_type], [popup_screen], [send_email], [send_sms]) VALUES (1090021, N'I', N'N', N'N', N'N')
INSERT [dbo].[event] ([event_id], [event_type], [popup_screen], [send_email], [send_sms]) VALUES (1090022, N'I', N'N', N'N', N'N')
INSERT [dbo].[event] ([event_id], [event_type], [popup_screen], [send_email], [send_sms]) VALUES (1090024, N'I', N'N', N'N', N'N')
INSERT [dbo].[event] ([event_id], [event_type], [popup_screen], [send_email], [send_sms]) VALUES (1090025, N'I', N'N', N'N', N'N')
INSERT [dbo].[event] ([event_id], [event_type], [popup_screen], [send_email], [send_sms]) VALUES (1090027, N'I', N'N', N'N', N'N')
INSERT [dbo].[event] ([event_id], [event_type], [popup_screen], [send_email], [send_sms]) VALUES (1090028, N'I', N'N', N'N', N'N')
INSERT [dbo].[event] ([event_id], [event_type], [popup_screen], [send_email], [send_sms]) VALUES (1090030, N'I', N'N', N'N', N'N')
INSERT [dbo].[event] ([event_id], [event_type], [popup_screen], [send_email], [send_sms]) VALUES (1090031, N'I', N'N', N'N', N'N')
INSERT [dbo].[event] ([event_id], [event_type], [popup_screen], [send_email], [send_sms]) VALUES (1090033, N'I', N'N', N'N', N'N')
INSERT [dbo].[event] ([event_id], [event_type], [popup_screen], [send_email], [send_sms]) VALUES (1090034, N'I', N'N', N'N', N'N')
INSERT [dbo].[event] ([event_id], [event_type], [popup_screen], [send_email], [send_sms]) VALUES (1090036, N'I', N'N', N'N', N'N')
INSERT [dbo].[event] ([event_id], [event_type], [popup_screen], [send_email], [send_sms]) VALUES (1090037, N'I', N'N', N'N', N'N')
INSERT [dbo].[event] ([event_id], [event_type], [popup_screen], [send_email], [send_sms]) VALUES (1090039, N'I', N'N', N'N', N'N')
INSERT [dbo].[event] ([event_id], [event_type], [popup_screen], [send_email], [send_sms]) VALUES (1090040, N'I', N'N', N'N', N'N')
INSERT [dbo].[event] ([event_id], [event_type], [popup_screen], [send_email], [send_sms]) VALUES (1090051, N'I', N'N', N'N', N'N')
INSERT [dbo].[event] ([event_id], [event_type], [popup_screen], [send_email], [send_sms]) VALUES (1090052, N'I', N'N', N'N', N'N')
INSERT [dbo].[event] ([event_id], [event_type], [popup_screen], [send_email], [send_sms]) VALUES (1090061, N'I', N'N', N'N', N'N')
INSERT [dbo].[event] ([event_id], [event_type], [popup_screen], [send_email], [send_sms]) VALUES (1090062, N'I', N'N', N'N', N'N')
INSERT [dbo].[event] ([event_id], [event_type], [popup_screen], [send_email], [send_sms]) VALUES (1090071, N'I', N'N', N'N', N'N')
INSERT [dbo].[event] ([event_id], [event_type], [popup_screen], [send_email], [send_sms]) VALUES (1090072, N'I', N'N', N'N', N'N')
INSERT [dbo].[event] ([event_id], [event_type], [popup_screen], [send_email], [send_sms]) VALUES (1090081, N'I', N'Y', N'N', N'N')
INSERT [dbo].[event] ([event_id], [event_type], [popup_screen], [send_email], [send_sms]) VALUES (1090082, N'I', N'Y', N'N', N'N')
INSERT [dbo].[event] ([event_id], [event_type], [popup_screen], [send_email], [send_sms]) VALUES (1090083, N'I', N'Y', N'N', N'N')
INSERT [dbo].[event] ([event_id], [event_type], [popup_screen], [send_email], [send_sms]) VALUES (1090101, N'E', N'Y', N'N', N'N')
INSERT [dbo].[event] ([event_id], [event_type], [popup_screen], [send_email], [send_sms]) VALUES (1090102, N'I', N'Y', N'N', N'N')
INSERT [dbo].[event] ([event_id], [event_type], [popup_screen], [send_email], [send_sms]) VALUES (1090103, N'E', N'Y', N'N', N'N')
INSERT [dbo].[event] ([event_id], [event_type], [popup_screen], [send_email], [send_sms]) VALUES (1090104, N'I', N'Y', N'N', N'N')
INSERT [dbo].[event] ([event_id], [event_type], [popup_screen], [send_email], [send_sms]) VALUES (1090105, N'I', N'Y', N'N', N'N')
INSERT [dbo].[event] ([event_id], [event_type], [popup_screen], [send_email], [send_sms]) VALUES (1090106, N'I', N'N', N'N', N'N')
INSERT [dbo].[event] ([event_id], [event_type], [popup_screen], [send_email], [send_sms]) VALUES (1090107, N'I', N'N', N'N', N'N')
INSERT [dbo].[event] ([event_id], [event_type], [popup_screen], [send_email], [send_sms]) VALUES (1090111, N'I', N'Y', N'N', N'N')
INSERT [dbo].[event] ([event_id], [event_type], [popup_screen], [send_email], [send_sms]) VALUES (1090112, N'E', N'Y', N'N', N'N')
INSERT [dbo].[event] ([event_id], [event_type], [popup_screen], [send_email], [send_sms]) VALUES (1090113, N'I', N'Y', N'N', N'N')
INSERT [dbo].[event] ([event_id], [event_type], [popup_screen], [send_email], [send_sms]) VALUES (1090114, N'E', N'Y', N'N', N'N')
INSERT [dbo].[event] ([event_id], [event_type], [popup_screen], [send_email], [send_sms]) VALUES (1090121, N'I', N'Y', N'N', N'N')
INSERT [dbo].[event] ([event_id], [event_type], [popup_screen], [send_email], [send_sms]) VALUES (1090122, N'I', N'Y', N'N', N'N')
INSERT [dbo].[event] ([event_id], [event_type], [popup_screen], [send_email], [send_sms]) VALUES (1090123, N'I', N'Y', N'N', N'N')
INSERT [dbo].[event] ([event_id], [event_type], [popup_screen], [send_email], [send_sms]) VALUES (1090124, N'I', N'Y', N'N', N'N')
INSERT [dbo].[event] ([event_id], [event_type], [popup_screen], [send_email], [send_sms]) VALUES (1090125, N'I', N'Y', N'N', N'N')
INSERT [dbo].[event] ([event_id], [event_type], [popup_screen], [send_email], [send_sms]) VALUES (1090126, N'I', N'Y', N'N', N'N')
INSERT [dbo].[event] ([event_id], [event_type], [popup_screen], [send_email], [send_sms]) VALUES (1090127, N'I', N'Y', N'N', N'N')
INSERT [dbo].[event] ([event_id], [event_type], [popup_screen], [send_email], [send_sms]) VALUES (1090131, N'I', N'Y', N'N', N'N')
INSERT [dbo].[event] ([event_id], [event_type], [popup_screen], [send_email], [send_sms]) VALUES (1090132, N'I', N'Y', N'N', N'N')
INSERT [dbo].[event] ([event_id], [event_type], [popup_screen], [send_email], [send_sms]) VALUES (1090141, N'W', N'Y', N'Y', N'Y')
INSERT [dbo].[event] ([event_id], [event_type], [popup_screen], [send_email], [send_sms]) VALUES (1090142, N'W', N'N', N'N', N'N')
INSERT [dbo].[event] ([event_id], [event_type], [popup_screen], [send_email], [send_sms]) VALUES (1090143, N'W', N'Y', N'N', N'N')
INSERT [dbo].[event] ([event_id], [event_type], [popup_screen], [send_email], [send_sms]) VALUES (1090144, N'W', N'N', N'N', N'N')
INSERT [dbo].[event] ([event_id], [event_type], [popup_screen], [send_email], [send_sms]) VALUES (1090145, N'W', N'Y', N'N', N'N')
INSERT [dbo].[event] ([event_id], [event_type], [popup_screen], [send_email], [send_sms]) VALUES (1090146, N'W', N'N', N'N', N'N')
INSERT [dbo].[event] ([event_id], [event_type], [popup_screen], [send_email], [send_sms]) VALUES (1090147, N'W', N'Y', N'N', N'N')
INSERT [dbo].[event] ([event_id], [event_type], [popup_screen], [send_email], [send_sms]) VALUES (1090148, N'W', N'N', N'N', N'N')
INSERT [dbo].[event] ([event_id], [event_type], [popup_screen], [send_email], [send_sms]) VALUES (1090151, N'W', N'Y', N'N', N'N')
INSERT [dbo].[event] ([event_id], [event_type], [popup_screen], [send_email], [send_sms]) VALUES (1090152, N'W', N'N', N'N', N'N')
INSERT [dbo].[event] ([event_id], [event_type], [popup_screen], [send_email], [send_sms]) VALUES (1090153, N'W', N'Y', N'N', N'N')
INSERT [dbo].[event] ([event_id], [event_type], [popup_screen], [send_email], [send_sms]) VALUES (1090154, N'W', N'N', N'N', N'N')
INSERT [dbo].[event] ([event_id], [event_type], [popup_screen], [send_email], [send_sms]) VALUES (1090161, N'W', N'Y', N'N', N'N')
INSERT [dbo].[event] ([event_id], [event_type], [popup_screen], [send_email], [send_sms]) VALUES (1090162, N'W', N'N', N'N', N'N')
INSERT [dbo].[event] ([event_id], [event_type], [popup_screen], [send_email], [send_sms]) VALUES (1090163, N'W', N'Y', N'N', N'N')
INSERT [dbo].[event] ([event_id], [event_type], [popup_screen], [send_email], [send_sms]) VALUES (1090164, N'W', N'N', N'N', N'N')
INSERT [dbo].[event] ([event_id], [event_type], [popup_screen], [send_email], [send_sms]) VALUES (1090171, N'W', N'Y', N'N', N'N')
INSERT [dbo].[event] ([event_id], [event_type], [popup_screen], [send_email], [send_sms]) VALUES (1090172, N'I', N'Y', N'N', N'N')
INSERT [dbo].[event] ([event_id], [event_type], [popup_screen], [send_email], [send_sms]) VALUES (1090181, N'I', N'N', N'N', N'N')
INSERT [dbo].[event] ([event_id], [event_type], [popup_screen], [send_email], [send_sms]) VALUES (1090182, N'I', N'N', N'N', N'N')
INSERT [dbo].[event] ([event_id], [event_type], [popup_screen], [send_email], [send_sms]) VALUES (1090183, N'W', N'N', N'N', N'N')
SET IDENTITY_INSERT [dbo].[event] OFF



/************************************************************************
 *
 * event_lang
 *
 ************************************************************************/

SET IDENTITY_INSERT [dbo].[event_lang] ON
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1216, 1090001, 1080001, N'사용자 정보', N'사용자가 로그인하였습니다. 사용자={0}', N'사용자가 로그인하였습니다.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1217, 1090002, 1080001, N'사용자 정보', N'사용자가 로그아웃하였습니다. 사용자={0}', N'사용자가 로그아웃하였습니다.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1218, 1090003, 1080001, N'사용자 정보', N'사용자가 추가되었습니다. 사용자={0}', N'사용자가 추가되었습니다.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1219, 1090004, 1080001, N'사용자 정보', N'사용자가 삭제되었습니다. 사용자={0}', N'사용자가 삭제되었습니다.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1220, 1090021, 1080001, N'위치 정보', N'지역1 위치 항목이 추가되었습니다. 지역1={0}', N'지역1 위치 항목이 추가되었습니다.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1221, 1090022, 1080001, N'위치 정보', N'지역1 위치 항목이 삭제되었습니다. 지역1={0}', N'지역1 위치 항목이 삭제되었습니다.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1222, 1090024, 1080001, N'위치 정보', N'지역2 위치 항목이 추가되었습니다. 지역2={0}', N'지역2 위치 항목이 추가되었습니다.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1223, 1090025, 1080001, N'위치 정보', N'지역2 위치 항목이 삭제되었습니다. 지역2={0}', N'지역2 위치 항목이 삭제되었습니다.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1224, 1090027, 1080001, N'위치 정보', N'사이트 위치 항목이 추가되었습니다. 사이트={0}', N'사이트 위치 항목이 추가되었습니다.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1225, 1090028, 1080001, N'위치 정보', N'사이트 위치 항목이 삭제되었습니다. 사이트={0}', N'사이트 위치 항목이 삭제되었습니다.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1226, 1090030, 1080001, N'위치 정보', N'빌딩 위치 항목이 추가되었습니다. 빌딩={0}', N'빌딩 위치 항목이 추가되었습니다.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1227, 1090031, 1080001, N'위치 정보', N'빌딩 위치 항목이 삭제되었습니다. 빌딩={0}', N'빌딩 위치 항목이 삭제되었습니다.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1228, 1090033, 1080001, N'위치 정보', N'층 위치 항목이 추가되었습니다. 층={0}', N'층 위치 항목이 추가되었습니다.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1229, 1090034, 1080001, N'위치 정보', N'층 위치 항목이 삭제되었습니다. 층={0}', N'층 위치 항목이 삭제되었습니다.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1230, 1090036, 1080001, N'위치 정보', N'룸 위치 항목이 추가되었습니다. 룸={0}', N'룸 위치 항목이 추가되었습니다.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1231, 1090037, 1080001, N'위치 정보', N'룸 위치 항목이 삭제되었습니다. 룸={0}', N'룸 위치 항목이 삭제되었습니다.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1232, 1090039, 1080001, N'위치 정보', N'랙 위치 항목이 추가되었습니다. 랙={0}', N'랙 위치 항목이 추가되었습니다.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1233, 1090040, 1080001, N'위치 정보', N'랙 위치 항목이 삭제되었습니다. 랙={0}', N'랙 위치 항목이 삭제되었습니다.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1234, 1090051, 1080001, N'카탈로그 정보', N'카탈로그 그룹 항목이 추가되었습니다. 카탈로그 그룹={0}', N'카탈로그 그룹 항목이 추가되었습니다.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1235, 1090052, 1080001, N'카탈로그 정보', N'카탈로그 그룹 항목이 삭제되었습니다. 카탈로그 그룹={0}', N'카탈로그 그룹 항목이 삭제되었습니다.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1236, 1090061, 1080001, N'카탈로그 정보', N'카탈로그 항목이 추가되었습니다. 카탈로그={0}', N'카탈로그 항목이 추가되었습니다.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1237, 1090062, 1080001, N'카탈로그 정보', N'카탈로그 항목이 삭제되었습니다. 카탈로그={0}', N'카탈로그 항목이 삭제되었습니다.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1238, 1090071, 1080001, N'자산 정보', N'자산 항목이 추가되었습니다. 위치={0}, 자산={1}', N'자산 항목이 추가되었습니다.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1239, 1090072, 1080001, N'자산 정보', N'자산 항목이 삭제되었습니다. 위치={0}, 자산={1}', N'자산 항목이 삭제되었습니다.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1240, 1090081, 1080001, N'작업 지시', N'작업 지시가 시작되었습니다. 위치={0}, 자산={1}, 작업={2}', N'작업 지시가 시작되었습니다.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1241, 1090082, 1080001, N'작업 지시', N'작업 지시가 종료되었습니다. 위치={0}, 자산={1}, 작업={2}', N'작업 지시가 종료되었습니다.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1242, 1090083, 1080001, N'작업 지시', N'작업 지시가 취소되었습니다. 위치={0}, 자산={1}, 작업={2}', N'작업 지시가 취소되었습니다.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1243, 1090101, 1080001, N'지능형 포트', N'미허가 패치 코드 연결이 발생하였습니다. 위치={0}, 패치 패널={1}/{2}', N'미허가 패치 코드 연결이 발생하였습니다.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1244, 1090102, 1080001, N'지능형 포트', N'패치 코드를 제거하여 원상태로 복원하었습니다. 위치={0}, 패치 패널={1}/{2}', N'패치 코드를 제거하여 원상태로 복원하었습니다.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1245, 1090103, 1080001, N'지능형 포트', N'미허가 패치 코드 제거가 발생하였습니다. 위치={0}, 패치 패널={1}/{2}', N'미허가 패치 코드 제거가 발생하였습니다.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1246, 1090104, 1080001, N'지능형 포트', N'패치 코드를 연결하여 원상태로 복원하었습니다. 위치={0}, 패치 패널={1}/{2}', N'패치 코드를 연결하여 원상태로 복원하었습니다.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1247, 1090105, 1080001, N'지능형 포트', N'패치 포트 스캐닝이 완료 되었습니다. 위치={0}, 컨트롤러={1}', N'패치 포트 스캐닝이 완료 되었습니다.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1248, 1090111, 1080001, N'지능형 장치', N'패치 패널이 컨트롤러 장비와 연결 되었습니다. 위치={0}, 패치 패널={1}', N'패치 패널이 컨트롤러 장비와 연결 되었습니다.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1249, 1090112, 1080001, N'지능형 장치', N'패치 패널이 컨트롤러 장비와 연결이 끊어졌습니다. 위치={0}, 패치 패널={1}', N'패치 패널이 컨트롤러 장비와 연결이 끊어졌습니다.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1250, 1090113, 1080001, N'지능형 장치', N'컨트롤러 장비와 서버가 연결 되었습니다. 위치={0}, 컨트롤러={1}', N'컨트롤러 장비와 서버가 연결 되었습니다.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1251, 1090114, 1080001, N'지능형 장치', N'컨트롤러 장비와 서버의 연결이 끊어졌습니다. 위치={0}, 컨트롤러={1}', N'컨트롤러 장비와 서버의 연결이 끊어졌습니다.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1252, 1090121, 1080001, N'단말 정보', N'새로운 단말이 검색되었습니다. 위치={0}, 아울렛={1}/{2}, 단말={3}', N'새로운 단말이 검색되었습니다.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1253, 1090122, 1080001, N'단말 정보', N'단말 자산이 추가되었습니다. 위치={0}, 아울렛={1}/{2}, 단말={3}', N'단말 자산이 추가되었습니다.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1254, 1090123, 1080001, N'단말 정보', N'단말의 위치가 이동되었습니다. 이동전위치={0}, 이동전아울렛={1}/{2}, 이동후위치={3}, 이동후아울렛={4}/{5}, 단말={6}', N'단말의 위치가 이동되었습니다.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1255, 1090124, 1080001, N'단말 정보', N'단말의 위치가 장기미접속 위치로 이동되었습니다. 기존위치={0}, 아울렛={1}/{2}, 단말={3}', N'단말의 위치가 미확인위치로 이동되었습니다.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1256, 1090125, 1080001, N'단말 정보', N'단말이 삭제되었습니다. 위치={0}, 아울렛={1}/{2}, 단말={3}', N'단말이 삭제되었습니다.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1257, 1090126, 1080001, N'단말 정보', N'네트웍 검색이 시작되었습니다.', N'네트웍 검색이 시작되었습니다.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1258, 1090127, 1080001, N'단말 정보', N'네트웍 검색이 종료되었습니다.', N'네트웍 검색이 종료되었습니다.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1259, 1090131, 1080001, N'스위치 제어', N'스위치 포트가 ON 되었습니다. 위치={0}, 스위치={1}/{2},', N'스위치 포트가 ON 되었습니다.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1260, 1090132, 1080001, N'스위치 제어', N'스위치 포트가 OFF 되었습니다. 위치={0}, 스위치={1}/{2}', N'스위치 포트가 OFF 되었습니다.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1261, 1090106, 1080001, N'지능형 포트', N'알람을 제거하고 연결 상태를 허용하였습니다. 위치={0}, 패치 패널={1}/{2}', N'알람을 제거하고 연결 상태를 허용하였습니다.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1262, 1090107, 1080001, N'지능형 포트', N'알람을 제거하고 단절 상태를 허용하였습니다. 위치={0}, 패치 패널={1}/{2}', N'알람을 제거하고 단절 상태를 허용하였습니다.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1268, 1090141, 1080001, N'환경 정보', N'전압 장애 제한 허용치를 초과하였습니다. 위치={0}, 자산={1}, 값={2}V', N'전압 장애 제한 허용치를 초과하였습니다.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1271, 1090142, 1080001, N'환경 정보', N'전압 경고 제한 허용치를 초과하였습니다. 위치={0}, 자산={1}, 값={2}V', N'전압 경고 제한 허용치를 초과하였습니다.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1272, 1090143, 1080001, N'환경 정보', N'전류 장애 제한 허용치를 초과하였습니다. 위치={0}, 자산={1}, 값={2}A', N'전류 장애 제한 허용치를 초과하였습니다.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1274, 1090144, 1080001, N'환경 정보', N'전류 경고 제한 허용치를 초과하였습니다. 위치={0}, 자산={1}, 값={2}A', N'전류 경고 제한 허용치를 초과하였습니다.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1277, 1090145, 1080001, N'환경 정보', N'전력 장애 제한 허용치를 초과하였습니다. 위치={0}, 자산={1}, 값={2}KW', N'전력 장애 제한 허용치를 초과하였습니다.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1281, 1090146, 1080001, N'환경 정보', N'전력 경고 제한 허용치를 초과하였습니다. 위치={0}, 자산={1}, 값={2}KW', N'전력 경고 제한 허용치를 초과하였습니다.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1283, 1090147, 1080001, N'환경 정보', N'전력량 장애 제한 허용치를 초과하였습니다. 위치={0}, 자산={1}, 값={2}KWh', N'전력량 장애 제한 허용치를 초과하였습니다.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1284, 1090148, 1080001, N'환경 정보', N'전력량 경고 제한 허용치를 초과하였습니다. 위치={0}, 자산={1}, 값={2}KWh', N'전력량 경고 제한 허용치를 초과하였습니다.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1285, 1090151, 1080001, N'환경 정보', N'온도 최대 장애 제한 허용치를 초과하였습니다. 위치={0}, 자산={1}, 값={2}도', N'온도 최대 장애 제한 허용치를 초과하였습니다.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1286, 1090152, 1080001, N'환경 정보', N'온도 최대 경고 제한 허용치를 초과하였습니다. 위치={0}, 자산={1}, 값={2}도', N'온도 최대 경고 제한 허용치를 초과하였습니다.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1287, 1090153, 1080001, N'환경 정보', N'온도 최소 장애 제한 허용치를 초과하였습니다. 위치={0}, 자산={1}, 값={2}도', N'온도 최소 장애 제한 허용치를 초과하였습니다.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1288, 1090154, 1080001, N'환경 정보', N'온도 최소 경고 제한 허용치를 초과하였습니다. 위치={0}, 자산={1}, 값={2}도', N'온도 최소 경고 제한 허용치를 초과하였습니다.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1289, 1090161, 1080001, N'환경 정보', N'습도 최대 장애 제한 허용치를 초과하였습니다. 위치={0}, 자산={1}, 값={2}%', N'습도 최대 장애 제한 허용치를 초과하였습니다.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1291, 1090162, 1080001, N'환경 정보', N'습도 최대 경고 제한 허용치를 초과하였습니다. 위치={0}, 자산={1}, 값={2}%', N'습도 최대 경고 제한 허용치를 초과하였습니다.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1292, 1090163, 1080001, N'환경 정보', N'습도 최소 장애 제한 허용치를 초과하였습니다. 위치={0}, 자산={1}, 값={2}%', N'습도 최소 장애 제한 허용치를 초과하였습니다.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1293, 1090164, 1080001, N'환경 정보', N'습도 최소 경고 제한 허용치를 초과하였습니다. 위치={0}, 자산={1}, 값={2}%', N'습도 최소 경고 제한 허용치를 초과하였습니다.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1294, 1090171, 1080001, N'환경 정보', N'도어가 열렸습니다. 위치={0}, 자산={1}', N'도어가 열렸습니다.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1295, 1090172, 1080001, N'환경 정보', N'도어가 닫혔습니다. 위치={0}, 자산={1}', N'도어가 닫혔습니다.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1307, 1090181, 1080001, N'펌웨어', N'컨트롤러 펌웨어 업그레이드 요청을 하였습니다. 위치={0}, 컨트롤러={1}, 버전={2}', N'컨트롤러 펌웨어 업그레이드 요청을 하였습니다.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1315, 1090182, 1080001, N'펌웨어', N'컨트롤러 펌웨어의 업그레이드가 성공하였습니다. 위치={0}, 컨트롤러={1}', N'컨트롤러 펌웨어의 업그레이드가 성공하였습니다.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1316, 1090183, 1080001, N'펌웨어', N'컨트롤러 펌웨어의 업그레이드가 실패하였습니다. 위치={0}, 컨트롤러={1}', N'컨트롤러 펌웨어의 업그레이드가 실패하였습니다.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1317, 1090001, 1080002, N'User Information', N'The user was logged in. user={0}', N'The user was logged in.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1318, 1090002, 1080002, N'User Information', N'The user was logged out. user={0}', N'The user was logged out.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1319, 1090003, 1080002, N'User Information', N'The user was added. user={0}', N'The user was added.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1320, 1090004, 1080002, N'User Information', N'The user was deleted. user={0}', N'The user was deleted.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1321, 1090021, 1080002, N'Location Information', N'The region1 was addeed. region1={0}', N'The region1 was addeed.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1322, 1090022, 1080002, N'Location Information', N'The region1 was deleted. region1={0}', N'The region1 was deleted.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1323, 1090024, 1080002, N'Location Information', N'The region2 was added. region2={0}', N'The region2 was added.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1324, 1090025, 1080002, N'Location Information', N'The region2 was deleted. region2={0}', N'The region2 was deleted.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1325, 1090027, 1080002, N'Location Information', N'The site was added. site={0}', N'The site was added.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1326, 1090028, 1080002, N'Location Information', N'The site was deleted. site={0}', N'The site was deleted.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1327, 1090030, 1080002, N'Location Information', N'The building was added. building={0}', N'The building was added.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1328, 1090031, 1080002, N'Location Information', N'The building was deleted. building={0}', N'The building was deleted.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1329, 1090033, 1080002, N'Location Information', N'The floor was added. floor={0}', N'The floor was added.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1330, 1090034, 1080002, N'Location Information', N'The floor was deleted. floor={0}', N'The floor was deleted.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1331, 1090036, 1080002, N'Location Information', N'The room was added. room={0}', N'The room was added.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1332, 1090037, 1080002, N'Location Information', N'The room was deleted. room={0}', N'The room was deleted.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1333, 1090039, 1080002, N'Location Information', N'The rack was added. rack={0}', N'The rack was added.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1334, 1090040, 1080002, N'Location Information', N'The rack was deleted. rack={0}', N'The rack was deleted.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1335, 1090051, 1080002, N'Catalog Information', N'A catalog group was added. catalog_group={0}', N'A catalog group was added.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1336, 1090052, 1080002, N'Catalog Information', N'A catalog group was deleted. catalog_group={0}', N'A catalog group was deleted.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1337, 1090061, 1080002, N'Catalog Information', N'A catalog was added. catalog={0}', N'A catalog was added.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1338, 1090062, 1080002, N'Catalog Information', N'A catalog was deleted. catalog={0}', N'A catalog was deleted.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1339, 1090071, 1080002, N'Asset Information', N'A asset was added. location={0}, asset={1}', N'A asset was added.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1340, 1090072, 1080002, N'Asset Information', N'A asset was deleted. location={0}, asset={1}', N'A asset was deleted.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1341, 1090081, 1080002, N'Work Order', N'The work order is started. location={0}, asset={1}, wo={2}', N'The work order is started.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1342, 1090082, 1080002, N'Work Order', N'The work order is finished. location={0}, asset={1}, wo={2}', N'The work order is finished.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1343, 1090083, 1080002, N'Work Order', N'The work order is canceled. location={0}, asset={1}, wo={2}', N'The work order is canceled.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1344, 1090101, 1080002, N'Intelligent Port', N'Discovered unauthorized patch cord connection. location={0}, patch_panel={1}/{2}', N'Discovered unauthorized patch cord connection.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1345, 1090102, 1080002, N'Intelligent Port', N'Discovered unauthorzed patch cord connection to original status. location={0}, patch_panel={1}/{2}', N'Discovered unauthorzed patch cord connection to original status.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1346, 1090103, 1080002, N'Intelligent Port', N'Discovered unauthorized patch cord removal. location={0}, patch_panel={1}/{2}', N'Discovered unauthorized patch cord removal.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1347, 1090104, 1080002, N'Intelligent Port', N'Discovered unauthorzed patch cord removal to original status. location={0}, patch_panel={1}/{2}', N'Discovered unauthorzed patch cord removal to original status.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1348, 1090105, 1080002, N'Intelligent Port', N'Patch port scanning has been completed. location={0}, controller={1}', N'Patch port scanning has been completed.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1349, 1090111, 1080002, N'Intelligent Device', N'This patch panel is connected to the controller. location={0}, patch_panel={1}', N'This patch panel is connected to the controller.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1350, 1090112, 1080002, N'Intelligent Device', N'This patch panel is disconnected to the controller. location={0}, patch_panel={1}', N'This patch panel is disconnected to the controller.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1351, 1090113, 1080002, N'Intelligent Device', N'This controller is connected to the server. location={0}, controller={1}', N'This controller is connected to the server.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1352, 1090114, 1080002, N'Intelligent Device', N'This controller is disconnected to the server. location={0}, controller={1}', N'This controller is disconnected to the server.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1353, 1090121, 1080002, N'Terminal Information', N'Discovered new terminal. location={3}, outlet={4}/{5}, terminal={6}', N'Discovered new terminal.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1354, 1090122, 1080002, N'Terminal Information', N'The terminal asset was added. location={0}, outlet={1}/{2}, terminal={3}', N'The terminal asset was added.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1355, 1090123, 1080002, N'Terminal Information', N'The terminal was moved to new location. old_location={0}, old_outlet={1}/{2}, new_location={3}, new_outlet={4}/{5}, terminal={6}', N'The terminal was moved to new location.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1356, 1090124, 1080002, N'Terminal Information', N'The terminal was moved to unknowned location. original_location={0}, outlet={1}/{2}, terminal={3}', N'The terminal was moved to unknowned location.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1357, 1090125, 1080002, N'Terminal Information', N'The terminal was deleted. location={0}, outlet={1}/{2}, terminal={3}', N'The terminal was deleted.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1358, 1090126, 1080002, N'Terminal Information', N'Network searching was started.', N'Network searching was started.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1359, 1090127, 1080002, N'Terminal Information', N'Network searching was finished.', N'Network searching was finished.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1360, 1090131, 1080002, N'Switch Control', N'The switch port was on. location={0}, switch={1}/{2},', N'The switch port was on.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1361, 1090132, 1080002, N'Switch Control', N'The switch port was off. location={0}, switch={1}/{2},', N'The switch port was off.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1362, 1090106, 1080002, N'Intelligent Port', N'Has removed the alarm and allow the connection state. location={0}, patch_panel={1}/{2}', N'Has removed the alarm and allow the connection state.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1363, 1090107, 1080002, N'Intelligent Port', N'Has removed the alarm and allow the disconnection state. location={0}, patch_panel={1}/{2}', N'Has removed the alarm and allow the disconnection state.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1364, 1090141, 1080002, N'Environment Information', N'The voltage was been exceeded failure limit of tolerances. location={0}, asset={1}, value={2}V', N'The voltage was been exceeded failure limit of tolerances.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1365, 1090142, 1080002, N'Environment Information', N'The voltage was been exceeded warning limit of tolerances. location={0}, asset={1}, value={2}V', N'The voltage was been exceeded warning limit of tolerances.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1366, 1090143, 1080002, N'Environment Information', N'The power currrent was been exceeded failure limit of tolerances. location={0}, asset={1}, value={2}A', N'The power currrent was been exceeded failure limit of tolerances.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1367, 1090144, 1080002, N'Environment Information', N'The power current was been exceeded warning limit of tolerances. location={0}, asset={1}, value={2}A', N'The power current was been exceeded warning limit of tolerances.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1368, 1090145, 1080002, N'Environment Information', N'The power was been exceeded failure limit of tolerances. location={0}, asset={1}, value={2}KW', N'The power was been exceeded failure limit of tolerances.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1369, 1090146, 1080002, N'Environment Information', N'The power was been exceeded warning limit of tolerances. location={0}, asset={1}, value={2}KW', N'The power was been exceeded warning limit of tolerances.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1370, 1090147, 1080002, N'Environment Information', N'The power consumption was been exceeded failure limit of tolerances. location={0}, asset={1}, value={2}KWh', N'The power consumption was been exceeded failure limit of tolerances.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1371, 1090148, 1080002, N'Environment Information', N'The power consumption was been exceeded warning limit of tolerances. location={0}, asset={1}, value={2}KWh', N'The power consumption was been exceeded warning limit of tolerances.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1372, 1090151, 1080002, N'Environment Information', N'The temperature was been exceeded max limit. location={0}, asset={1}, value={2}C', N'The temperature was been exceeded max limit.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1373, 1090152, 1080002, N'Environment Information', N'The temperature was been exceeded max warning limit. location={0}, asset={1}, value={2}C', N'The temperature was been exceeded max warning limit.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1374, 1090153, 1080002, N'Environment Information', N'The temperature was been exceeded minimum limit. location={0}, asset={1}, value={2}C', N'The temperature was been exceeded minimum limit.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1375, 1090154, 1080002, N'Environment Information', N'The temperature was been exceeded minimum warning limit. location={0}, asset={1}, value={2}C', N'The temperature was been exceeded minimum warning limit.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1376, 1090161, 1080002, N'Environment Information', N'The humidity was been exceeded max limit. location={0}, asset={1}, value={2}%', N'The humidity was been exceeded max limit.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1377, 1090162, 1080002, N'Environment Information', N'The humidity was been exceeded max warning limit. location={0}, asset={1}, value={2}%', N'The humidity was been exceeded max warning limit.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1378, 1090163, 1080002, N'Environment Information', N'The humidity was been exceeded minimum warning limit. location={0}, asset={1}, value={2}%', N'The humidity was been exceeded minimum warning limit.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1379, 1090164, 1080002, N'Environment Information', N'The humidity was been exceeded minimum warning limit. location={0}, asset={1}, value={2}%', N'The humidity was been exceeded minimum warning limit.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1380, 1090171, 1080002, N'Environment Information', N'The door was been opened. location={0}, asset={1}', N'The door was been opened.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1381, 1090172, 1080002, N'Environment Information', N'The door was been closed. location={0}, asset={1}', N'The door was been closed.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1382, 1090181, 1080002, N'Firmware', N'Requested to the controller firmware upgrade. location={0}, controller={1}, version={2}', N'Requested to the controller firmware upgrade.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1383, 1090182, 1080002, N'Firmware', N'The upgrade of the controller firmware was successful. location={0}, controller={1}', N'The upgrade of the controller firmware was successful.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1384, 1090183, 1080002, N'Firmware', N'The upgrade of the controller firmware was failed. location={0}, controller={1}', N'The upgrade of the controller firmware was failed.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1385, 1090005, 1080001, N'사용자 정보', N'사용자가 변경되었습니다. 사용자={0}', N'사용자가 변경되었습니다.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1386, 1090005, 1080002, N'User Information', N'The user was updated. user={0}', N'The user was updated.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1387, 1090006, 1080001, N'서버 정보', N'서버가 연결되었습니다. 서버={0}', N'서버가 연결되었습니다.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1388, 1090006, 1080002, N'Server Information', N'The server was linked. user={0}', N'The server was linked.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1389, 1090007, 1080001, N'서버 정보', N'서버가 단절되었습니다. 서버={0}', N'서버가 단절되었습니다.')
INSERT [dbo].[event_lang] ([event_lang_id], [event_id], [lang_id], [event_group], [event_format], [event_desc]) VALUES (1390, 1090007, 1080002, N'Server Information', N'The server was unlinked. user={0}', N'The server was unlinked.')
SET IDENTITY_INSERT [dbo].[event_lang] OFF



/************************************************************************
 *
 * user
 *
 ************************************************************************/

SET IDENTITY_INSERT [dbo].[user] ON
INSERT INTO [dbo].[user] ([user_id], [user_group], [user_name], [login_id], [login_password], [email], [use_email], [phone], [mobile], [use_sms], [remarks], deletable)
	VALUES (90001, 'S', '시스템', 'system', 'ZxWrsQHMfWzoXyG5YdLxws4nM7aQ/6844tTlsv9P/+Iqc2j+wAFbd3s7JnvzD8HG', 'na5top@aa.com', 'Y', '02-111-2222', '010-1111-2222', 'Y', '수퍼운영자', 'N');
SET IDENTITY_INSERT [dbo].[user] OFF



/************************************************************************
 *
 * image_type
 *
 ************************************************************************/

SET IDENTITY_INSERT [dbo].[image_type] ON
INSERT INTO [dbo].[image_type] ([image_type_id], [image_type_name], [folder_name], [size_x],  [size_y], [remarks]) 	VALUES (1160001, '지도 이미지', 'map', 1024, 768, '');
INSERT INTO [dbo].[image_type] ([image_type_id], [image_type_name], [folder_name], [size_x],  [size_y], [remarks])	VALUES (1160002, '사이트 이미지', 'site', 1024, 768, '');
INSERT INTO [dbo].[image_type] ([image_type_id], [image_type_name], [folder_name], [size_x],  [size_y], [remarks])	VALUES (1160003, '빌딩 이미지', 'building', 1024, 768, '');
INSERT INTO [dbo].[image_type] ([image_type_id], [image_type_name], [folder_name], [size_x],  [size_y], [remarks])	VALUES (1160004, '평면도 이미지', 'drawing', 1024, 768, '2D 층별 평면도');
INSERT INTO [dbo].[image_type] ([image_type_id], [image_type_name], [folder_name], [size_x],  [size_y], [remarks])	VALUES (1160005, '카탈로그 이미지', 'catalog', 320, 200, '');
INSERT INTO [dbo].[image_type] ([image_type_id], [image_type_name], [folder_name], [size_x],  [size_y], [remarks])	VALUES (1160006, '랙마운트 이미지 220x20', 'rack_220', 220, 20, '');
INSERT INTO [dbo].[image_type] ([image_type_id], [image_type_name], [folder_name], [size_x],  [size_y], [remarks])	VALUES (1160007, '랙마운트 이미지 440x40', 'rack_440', 440, 40, '');
INSERT INTO [dbo].[image_type] ([image_type_id], [image_type_name], [folder_name], [size_x],  [size_y], [remarks])	VALUES (1160008, '랙마운트 이미지 880x80', 'rack_880', 880, 80, '');
INSERT INTO [dbo].[image_type] ([image_type_id], [image_type_name], [folder_name], [size_x],  [size_y], [remarks])	VALUES (1160009, '링크다이어그램 이미지', 'link', 80, 40, '투명바탕 필요');
INSERT INTO [dbo].[image_type] ([image_type_id], [image_type_name], [folder_name], [size_x],  [size_y], [remarks])	VALUES (1160010, '기타 이미지', 'etc', 0, 0, '사이즈 제한 없음');
INSERT INTO [dbo].[image_type] ([image_type_id], [image_type_name], [folder_name], [size_x],  [size_y], [remarks])	VALUES (1160011, 'ICON 16x16', 'icon_16', 16, 16, '');
INSERT INTO [dbo].[image_type] ([image_type_id], [image_type_name], [folder_name], [size_x],  [size_y], [remarks])	VALUES (1160012, 'ICON 32x32', 'icon_32', 32, 32, '');
INSERT INTO [dbo].[image_type] ([image_type_id], [image_type_name], [folder_name], [size_x],  [size_y], [remarks])	VALUES (1160013, 'ICON 48x48', 'icon_48', 48, 48, '');
INSERT INTO [dbo].[image_type] ([image_type_id], [image_type_name], [folder_name], [size_x],  [size_y], [remarks])	VALUES (1160014, 'ICON 64x64', 'icon_64', 64, 64, '');
SET IDENTITY_INSERT [dbo].[image_type] OFF



/************************************************************************
 *
 * image
 *
 ************************************************************************/

SET IDENTITY_INSERT [dbo].[image] ON 
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2010001, N'세계지도 이미지', N'world_map.png', 1160001, 1024, 768, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2010002, N'한국 ', N'skorea_1600x1000.png', 1160001, 1024, 768, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2110001, N'Site ICON for tree', N'center_16.png', 1160011, 16, 16, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2110002, N'building ICON for tree', N'building_16.png', 1160011, 16, 16, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2110003, N'floor ICON for tree', N'floor_16.png', 1160011, 16, 16, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2110004, N'room ICON for tree', N'room_16.png', 1160011, 16, 16, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2110005, N'rack ICON for tree', N'rack_16.png', 1160011, 16, 16, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2110006, N'IC ICON for tree', N'ic_16.png', 1160011, 16, 16, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2110007, N'ipp ICON for tree', N'ipp_16.png', 1160011, 16, 16, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2110008, N'Switch ICON for tree', N'l2_sw_16.png', 1160011, 16, 16, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2110010, N'PC for tree', N'pc_16.png', 1160011, 16, 16, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2110011, N'NB for tree', N'nb_16.png', 1160011, 16, 16, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2110012, N'Server for tree', N'server_16.png', 1160011, 16, 16, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2110013, N'Workstation for tree', N'workstation_16.png', 1160011, 16, 16, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2110014, N'Storage for tree', N'storage_16.png', 1160011, 16, 16, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2110015, N'CP for tree', N'cp_16.png', 1160011, 16, 16, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2110016, N'FacePlate for tree', N'fp_16.png', 1160011, 16, 16, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2110017, N'MUTOA Box for tree', N'mb_16.png', 1160011, 16, 16, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2110018, N'pp ICON for tree', N'pp_16.png', 1160011, 16, 16, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2110019, N'HUB ICON for tree', N'hub_16.png', 1160011, 16, 16, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2110020, N'L3 Switch ICON for tree', N'l3_sw_16.png', 1160011, 16, 16, NULL, N'N', N'image inclueded')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2110021, N'Back-bone Switch ICON for tree', N'bb_sw_16.png', 1160011, 16, 16, NULL, N'N', N'image inclueded')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2110022, N'Environment ICON for tree', N'eb_16.png', 1160011, 16, 16, NULL, N'N', N'image inclueded')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2110023, N'Phone ICON for tree', N'phone_16.png', 1160011, 16, 16, NULL, N'N', N'image inclueded')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2110024, N'Printer ICON for tree', N'printer_16.png', 1160011, 16, 16, NULL, N'N', N'image inclueded')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2110099, N'etc ICON for tree', N'etc_16.png', 1160011, 16, 16, NULL, N'N', N'')

INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2120001, N'ic for link diagram', N'ic_80.png', 1160009, 80, 60, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2120002, N'ipp for link diagram', N'ipp_80.png', 1160009, 80, 60, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2120003, N'ifdf for link diagram', N'ifdf_80.png', 1160009, 80, 60, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2120004, N'back bone switchp for link diagram', N'bb_sw_80.png', 1160009, 80, 60, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2120005, N'l2 switch for link diagram', N'l2_sw_80.png', 1160009, 80, 60, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2120006, N'l3 switch for link diagram', N'l3_sw_80.png', 1160009, 80, 60, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2120007, N'pc for link diagram', N'pc_80.png', 1160009, 80, 60, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2120008, N'notebook for link diagram', N'nb_80.png', 1160009, 80, 60, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2120009, N'server for link diagram', N'server_80.png', 1160009, 80, 60, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2120010, N'workstation for link diagram', N'workstation_80.png', 1160009, 80, 60, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2120011, N'storage for link diagram', N'storage_80.png', 1160009, 80, 60, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2120012, N'phone for link diagram', N'phone_80.png', 1160009, 80, 60, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2120013, N'printer for link diagram', N'printer_80.png', 1160009, 80, 60, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2120014, N'consolidation point for link diagram', N'cp_80.png', 1160009, 80, 60, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2120015, N'face plate for link diagram', N'fp_80.png', 1160009, 80, 60, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2120016, N'mutoa box for link diagram', N'mb_80.png', 1160009, 80, 60, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2120017, N'patch panel for link diagram', N'pp_80.png', 1160009, 80, 60, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2120018, N'fdf for link diagram', N'fdf_80.png', 1160009, 80, 60, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2120097, N'eb for link diagram', N'eb_80.png', 1160009, 80, 60, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2120098, N'hub for link diagram', N'hub_80.png', 1160009, 80, 60, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2120099, N'etc for link diagram', N'etc_80.png', 1160009, 80, 60, NULL, N'N', N'')

INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2130001, N'LS SimpleWin Intelligent Controller', N'LS_SimpleWin_IC_220x20.png', 1160006, 220, 20, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2130002, N'ATEN Energy Box', N'ATEN_Energy_Box_220x20.png', 1160006, 220, 20, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2130003, N'LS SimpleWin Intelligent Patch Panel', N'LS_SimpleWin_IPP_220x20.png', 1160006, 220, 20, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2130004, N'LS SimpleWin Intelligent Patch Panel (Angled)', N'LS_SimpleWin_IPPA_220x20.png', 1160006, 220, 20, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2130005, N'General Blank 1U', N'General_Blank_1U_220x20.png', 1160006, 220, 20, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2130006, N'General Blank 2U', N'General_Blank_2U_220x40.png', 1160006, 220, 40, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2130007, N'General Blank 3U', N'General_Blank_3U_220x60.png', 1160006, 220, 60, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2130008, N'General Blank 4U', N'General_Blank_4U_220x80.png', 1160006, 220, 80, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2130009, N'General Entry type-1 1U', N'General_Entry1_220x20.png', 1160006, 220, 20, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2130010, N'General Entry type-2 1U', N'General_Entry2_220x20.png', 1160006, 220, 20, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2130011, N'Cisco 4503 7U', N'Cisco_4503_7U_220x140.png', 1160006, 220, 140, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2130012, N'Cisco 4506 11U', N'Cisco_4506_11U_220x220.png', 1160006, 220, 220, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2130013, N'Cisco 4507 12U', N'Cisco_4507_12U_220x240.png', 1160006, 220, 240, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2130014, N'Cisco 4510 15U', N'Cisco_4510_15U_220x300.png', 1160006, 220, 300, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2130015, N'Cisco 6503 4U', N'Cisco_6503_4U_220x80.png', 1160006, 220, 80, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2130016, N'Cisco 6504 5U', N'Cisco_6504_5U_220x100.png', 1160006, 220, 100, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2130017, N'Cisco 6506 11U', N'Cisco_6506_11U_220x220.png', 1160006, 220, 220, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2130018, N'Cisco 6509 14U', N'Cisco_6509_14U_220x280.png', 1160006, 220, 280, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2130019, N'Cisco 6509 21U', N'Cisco_6509v_21U_220x420.png', 1160006, 220, 420, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2130020, N'Cisco 6513 19U', N'Cisco_6513_19U_220x380.png', 1160006, 220, 380, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2130042, N'General L2 Switch (24 ports)', N'General_SW24_220x20.png', 1160006, 220, 20, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2130021, N'General L2 Switch (24+2 ports)', N'General_SW24_2_220x20.png', 1160006, 220, 20, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2130022, N'General L2 Switch (24+4 ports)', N'General_SW24_4_220x20.png', 1160006, 220, 20, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2130023, N'General L2 Switch (48 ports)', N'General_SW48_220x20.png', 1160006, 220, 20, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2130024, N'General L2 Switch (48+2 ports)', N'General_SW48_2_220x20.png', 1160006, 220, 20, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2130025, N'General L2 Switch (48+4 ports)', N'General_SW48_4_220x20.png', 1160006, 220, 20, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2130043, N'General L3 Switch (24 ports)', N'General_L3SW24_220x20.png', 1160006, 220, 20, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2130026, N'General L3 Switch (24+2 ports)', N'General_L3SW24_2_220x20.png', 1160006, 220, 20, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2130027, N'General L3 Switch (24+4 ports)', N'General_L3SW24_4_220x20.png', 1160006, 220, 20, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2130028, N'General Router', N'General_Router_1U_220x20.png', 1160006, 220, 20, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2130029, N'General Server 1U', N'General_Server_1U_220x20.png', 1160006, 220, 20, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2130030, N'General Server 1.5U', N'General_Server_1.5U_220x40.png', 1160006, 220, 40, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2130031, N'General Server 2U', N'General_Server_2U_220x40.png', 1160006, 220, 40, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2130032, N'General Server 3U', N'General_Server_3U_220x60.png', 1160006, 220, 60, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2130033, N'General Server 4U', N'General_Server_4U_220x80.png', 1160006, 220, 80, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2130034, N'IBM Blade Server 4U', N'IBM_blade_server_4U_220x80.png', 1160006, 220, 80, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2130035, N'IBM Blade Server 6U', N'IBM_blade_server_6U_220x120.png', 1160006, 220, 120, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2130036, N'HP Blade Server 10U', N'HP_blade_server_10U_220x200.png', 1160006, 220, 200, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2130037, N'IBM Storage 2U', N'IBM_storage_2U_220x40.png', 1160006, 220, 40, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2130038, N'IBM Storage-2 2U', N'IBM_storage2_2U_220x40.png', 1160006, 220, 40, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2130039, N'EMC Storage 2U', N'EMC_storage_2U_220x40.png', 1160006, 220, 40, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2130040, N'General Patch Panel', N'General_PP_220x20.png', 1160006, 220, 20, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2130041, N'General Patch Panel (Angled)', N'General_PPA_220x20.png', 1160006, 220, 20, NULL, N'N', N'')

INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2140001, N'LS SimpleWin Intelligent Controller', N'LS_SimpleWin_IC_440x40.png', 1160007, 440, 40, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2140002, N'ATEN Energy Box', N'ATEN_Energy_Box_440x40.png', 1160007, 440, 40, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2140003, N'LS SimpleWin Intelligent Patch Panel', N'LS_SimpleWin_IPP_440x40.png', 1160007, 440, 40, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2140004, N'LS SimpleWin Intelligent Patch Panel (Angled)', N'LS_SimpleWin_IPPA_440x40.png', 1160007, 440, 40, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2140005, N'General Blank 1U', N'General_Blank_1U_440x40.png', 1160007, 440, 40, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2140006, N'General Blank 2U', N'General_Blank_2U_440x80.png', 1160007, 440, 80, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2140007, N'General Blank 3U', N'General_Blank_3U_440x120.png', 1160007, 440, 120, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2140008, N'General Blank 4U', N'General_Blank_4U_440x160.png', 1160007, 440, 160, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2140009, N'General Entry type-1 1U', N'General_Entry1_440x40.png', 1160007, 440, 40, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2140010, N'General Entry type-2 1U', N'General_Entry2_440x40.png', 1160007, 440, 40, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2140011, N'Cisco 4503 7U', N'Cisco_4503_7U_440x280.png', 1160007, 440, 280, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2140012, N'Cisco 4506 11U', N'Cisco_4506_11U_440x440.png', 1160007, 440, 440, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2140013, N'Cisco 4507 12U', N'Cisco_4507_12U_440x480.png', 1160007, 440, 480, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2140014, N'Cisco 4510 15U', N'Cisco_4510_15U_440x600.png', 1160007, 440, 600, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2140015, N'Cisco 6503 4U', N'Cisco_6503_4U_440x160.png', 1160007, 440, 160, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2140016, N'Cisco 6504 5U', N'Cisco_6504_5U_440x200.png', 1160007, 440, 200, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2140017, N'Cisco 6506 11U', N'Cisco_6506_11U_440x440.png', 1160007, 440, 440, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2140018, N'Cisco 6509 14U', N'Cisco_6509_14U_440x560.png', 1160007, 440, 560, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2140019, N'Cisco 6509 21U', N'Cisco_6509v_21U_440x840.png', 1160007, 440, 840, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2140020, N'Cisco 6513 19U', N'Cisco_6513_19U_440x760.png', 1160007, 440, 760, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2140042, N'General L2 Switch (24 ports)', N'General_SW24_440x40.png', 1160007, 440, 40, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2140021, N'General L2 Switch (24+2 ports)', N'General_SW24_2_440x40.png', 1160007, 440, 40, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2140022, N'General L2 Switch (24+4 ports)', N'General_SW24_4_440x40.png', 1160007, 440, 40, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2140023, N'General L2 Switch (48 ports)', N'General_SW48_440x40.png', 1160007, 440, 40, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2140024, N'General L2 Switch (48+2 ports)', N'General_SW48_2_440x40.png', 1160007, 440, 40, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2140025, N'General L2 Switch (48+4 ports)', N'General_SW48_4_440x40.png', 1160007, 440, 40, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2140043, N'General L3 Switch (24 ports)', N'General_L3SW24_440x40.png', 1160007, 440, 40, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2140026, N'General L3 Switch (24+2 ports)', N'General_L3SW24_2_440x40.png', 1160007, 440, 40, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2140027, N'General L3 Switch (24+4 ports)', N'General_L3SW24_4_440x40.png', 1160007, 440, 40, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2140028, N'General Router', N'General_Router_1U_440x40.png', 1160007, 440, 40, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2140029, N'General Server 1U', N'General_Server_1U_440x40.png', 1160007, 440, 40, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2140030, N'General Server 1.5U', N'General_Server_1.5U_440x80.png', 1160007, 440, 80, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2140031, N'General Server 2U', N'General_Server_2U_440x80.png', 1160007, 440, 80, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2140032, N'General Server 3U', N'General_Server_3U_440x120.png', 1160007, 440, 120, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2140033, N'General Server 4U', N'General_Server_4U_440x160.png', 1160007, 440, 160, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2140034, N'IBM Blade Server 4U', N'IBM_blade_server_4U_440x160.png', 1160007, 440, 160, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2140035, N'IBM Blade Server 6U', N'IBM_blade_server_6U_440x240.png', 1160007, 440, 240, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2140036, N'HP Blade Server 10U', N'HP_blade_server_10U_440x400.png', 1160007, 440, 400, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2140037, N'IBM Storage 2U', N'IBM_storage_2U_440x80.png', 1160007, 440, 80, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2140038, N'IBM Storage-2 2U', N'IBM_storage2_2U_440x80.png', 1160007, 440, 80, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2140039, N'EMC Storage 2U', N'EMC_storage_2U_440x80.png', 1160007, 440, 80, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2140040, N'General Patch Panel', N'General_PP_440x40.png', 1160007, 440, 40, NULL, N'N', N'')
INSERT [dbo].[image] ([image_id], [image_name], [file_name], [image_type_id], [size_x], [size_y], [drawing_3d_id], [deletable], [remarks]) VALUES (2140041, N'General Patch Panel (Angled)', N'General_PPA_440x40.png', 1160007, 440, 40, NULL, N'N', N'')

SET IDENTITY_INSERT [dbo].[image] OFF



/************************************************************************
 *
 * region1
 *
 ************************************************************************/

DECLARE @REGION1_ID_KOREA INT
DECLARE @LOC_ID_KOREA INT
EXEC sp_add_region1 '한국', 2010002, 1328, 370, 90001, @REGION1_ID_KOREA OUT, @LOC_ID_KOREA OUT



/************************************************************************
 *
 * region2
 *
 ************************************************************************/

DECLARE @REGION2_ID_SEOUL INT
DECLARE @LOC_ID_SEOUL INT
EXEC sp_add_region2 @REGION1_ID_KOREA, '서울', 743, 229, 90001, @REGION2_ID_SEOUL OUT, @LOC_ID_SEOUL OUT




/************************************************************************
 *
 * manufacture
 *
 ************************************************************************/

SET IDENTITY_INSERT [dbo].[manufacture] ON
INSERT INTO [dbo].[manufacture] ([manufacture_id], [manufacture_name], [phone], [address], [post], [homepage_url], [ceo_name])	VALUES (10010001, 'LS전선(주)', '123-1234-1234', '경기도 안양시', '123-123', 'www.lscns.com', '구자은');
SET IDENTITY_INSERT [dbo].[manufacture] OFF




/************************************************************************
 *
 * catalog_group
 *
 ************************************************************************/
 
SET IDENTITY_INSERT [dbo].[catalog_group] ON 
INSERT [dbo].[catalog_group] ([catalog_group_id], [catalog_group_name], [catalog_level], [enable], [deletable], [level1_catalog_group_id], [level2_catalog_group_id], [disp_order]) 
VALUES (3100, N'SimpleWin Device', 1, N'Y', N'N', 0, 0, 1)
INSERT [dbo].[catalog_group] ([catalog_group_id], [catalog_group_name], [catalog_level], [enable], [deletable], [level1_catalog_group_id], [level2_catalog_group_id], [disp_order]) 
VALUES (3110, N'I2MS Controller', 2, N'Y', N'N', 3100, 0, 2)
INSERT [dbo].[catalog_group] ([catalog_group_id], [catalog_group_name], [catalog_level], [enable], [deletable], [level1_catalog_group_id], [level2_catalog_group_id], [disp_order]) 
VALUES (3120, N'Environment Device', 2, N'Y', N'N', 3100, 0, 3)
INSERT [dbo].[catalog_group] ([catalog_group_id], [catalog_group_name], [catalog_level], [enable], [deletable], [level1_catalog_group_id], [level2_catalog_group_id], [disp_order]) 
VALUES (3130, N'i-Patch Panel', 2, N'Y', N'N', 3100, 0, 4)
INSERT [dbo].[catalog_group] ([catalog_group_id], [catalog_group_name], [catalog_level], [enable], [deletable], [level1_catalog_group_id], [level2_catalog_group_id], [disp_order]) 
VALUES (3140, N'i-FDF', 2, N'Y', N'N', 3100, 0, 5)
INSERT [dbo].[catalog_group] ([catalog_group_id], [catalog_group_name], [catalog_level], [enable], [deletable], [level1_catalog_group_id], [level2_catalog_group_id], [disp_order]) 
VALUES (3150, N'i-Patch Cord', 2, N'Y', N'N', 3100, 0, 6)
INSERT [dbo].[catalog_group] ([catalog_group_id], [catalog_group_name], [catalog_level], [enable], [deletable], [level1_catalog_group_id], [level2_catalog_group_id], [disp_order]) 
VALUES (3200, N'Rack Device', 1, N'Y', N'N', 0, 0, 7)
INSERT [dbo].[catalog_group] ([catalog_group_id], [catalog_group_name], [catalog_level], [enable], [deletable], [level1_catalog_group_id], [level2_catalog_group_id], [disp_order]) 
VALUES (3210, N'Rack', 2, N'Y', N'N', 3200, 0, 8)
INSERT [dbo].[catalog_group] ([catalog_group_id], [catalog_group_name], [catalog_level], [enable], [deletable], [level1_catalog_group_id], [level2_catalog_group_id], [disp_order]) 
VALUES (3220, N'Vertical Cable Manager', 2, N'Y', N'N', 3200, 0, 9)
INSERT [dbo].[catalog_group] ([catalog_group_id], [catalog_group_name], [catalog_level], [enable], [deletable], [level1_catalog_group_id], [level2_catalog_group_id], [disp_order]) 
VALUES (3230, N'Entry Panel', 2, N'Y', N'N', 3200, 0, 10)
INSERT [dbo].[catalog_group] ([catalog_group_id], [catalog_group_name], [catalog_level], [enable], [deletable], [level1_catalog_group_id], [level2_catalog_group_id], [disp_order]) 
VALUES (3300, N'Network Device', 1, N'Y', N'N', 0, 0, 11)
INSERT [dbo].[catalog_group] ([catalog_group_id], [catalog_group_name], [catalog_level], [enable], [deletable], [level1_catalog_group_id], [level2_catalog_group_id], [disp_order]) 
VALUES (3310, N'Backbone Switch&Router', 2, N'Y', N'N', 3300, 0, 12)
INSERT [dbo].[catalog_group] ([catalog_group_id], [catalog_group_name], [catalog_level], [enable], [deletable], [level1_catalog_group_id], [level2_catalog_group_id], [disp_order]) 
VALUES (3320, N'L2 Switch', 2, N'Y', N'N', 3300, 0, 13)
INSERT [dbo].[catalog_group] ([catalog_group_id], [catalog_group_name], [catalog_level], [enable], [deletable], [level1_catalog_group_id], [level2_catalog_group_id], [disp_order]) 
VALUES (3330, N'L3 Switch', 2, N'Y', N'N', 3300, 0, 14)
INSERT [dbo].[catalog_group] ([catalog_group_id], [catalog_group_name], [catalog_level], [enable], [deletable], [level1_catalog_group_id], [level2_catalog_group_id], [disp_order]) 
VALUES (3340, N'PC', 2, N'Y', N'N', 3300, 0, 15)
INSERT [dbo].[catalog_group] ([catalog_group_id], [catalog_group_name], [catalog_level], [enable], [deletable], [level1_catalog_group_id], [level2_catalog_group_id], [disp_order]) 
VALUES (3350, N'Server', 2, N'Y', N'N', 3300, 0, 16)
INSERT [dbo].[catalog_group] ([catalog_group_id], [catalog_group_name], [catalog_level], [enable], [deletable], [level1_catalog_group_id], [level2_catalog_group_id], [disp_order]) 
VALUES (3360, N'Workstation', 2, N'Y', N'N', 3300, 0, 17)
INSERT [dbo].[catalog_group] ([catalog_group_id], [catalog_group_name], [catalog_level], [enable], [deletable], [level1_catalog_group_id], [level2_catalog_group_id], [disp_order]) 
VALUES (3370, N'Storage', 2, N'Y', N'N', 3300, 0, 18)
INSERT [dbo].[catalog_group] ([catalog_group_id], [catalog_group_name], [catalog_level], [enable], [deletable], [level1_catalog_group_id], [level2_catalog_group_id], [disp_order]) 
VALUES (3380, N'IP Phone', 2, N'Y', N'N', 3300, 0, 19)
INSERT [dbo].[catalog_group] ([catalog_group_id], [catalog_group_name], [catalog_level], [enable], [deletable], [level1_catalog_group_id], [level2_catalog_group_id], [disp_order]) 
VALUES (3390, N'IP Printer', 2, N'Y', N'N', 3300, 0, 20)
INSERT [dbo].[catalog_group] ([catalog_group_id], [catalog_group_name], [catalog_level], [enable], [deletable], [level1_catalog_group_id], [level2_catalog_group_id], [disp_order]) 
VALUES (3400, N'Connectivity', 1, N'Y', N'N', 0, 0, 21)
INSERT [dbo].[catalog_group] ([catalog_group_id], [catalog_group_name], [catalog_level], [enable], [deletable], [level1_catalog_group_id], [level2_catalog_group_id], [disp_order]) 
VALUES (3410, N'Consolidation Point', 2, N'Y', N'N', 3400, 0, 22)
INSERT [dbo].[catalog_group] ([catalog_group_id], [catalog_group_name], [catalog_level], [enable], [deletable], [level1_catalog_group_id], [level2_catalog_group_id], [disp_order]) 
VALUES (3420, N'Face Plate(Outlet)', 2, N'Y', N'N', 3400, 0, 23)
INSERT [dbo].[catalog_group] ([catalog_group_id], [catalog_group_name], [catalog_level], [enable], [deletable], [level1_catalog_group_id], [level2_catalog_group_id], [disp_order]) 
VALUES (3430, N'MUTOA Box(Outlet)', 2, N'Y', N'N', 3400, 0, 24)
INSERT [dbo].[catalog_group] ([catalog_group_id], [catalog_group_name], [catalog_level], [enable], [deletable], [level1_catalog_group_id], [level2_catalog_group_id], [disp_order])
VALUES (3440, N'Patch Panel', 2, N'Y', N'N', 3400, 0, 25)
INSERT [dbo].[catalog_group] ([catalog_group_id], [catalog_group_name], [catalog_level], [enable], [deletable], [level1_catalog_group_id], [level2_catalog_group_id], [disp_order]) 
VALUES (3450, N'FDF', 2, N'Y', N'N', 3400, 0, 26)
INSERT [dbo].[catalog_group] ([catalog_group_id], [catalog_group_name], [catalog_level], [enable], [deletable], [level1_catalog_group_id], [level2_catalog_group_id], [disp_order]) 
VALUES (3460, N'Patch Cord', 2, N'Y', N'N', 3400, 0, 27)
INSERT [dbo].[catalog_group] ([catalog_group_id], [catalog_group_name], [catalog_level], [enable], [deletable], [level1_catalog_group_id], [level2_catalog_group_id], [disp_order]) 
VALUES (3470, N'Permanent Link', 2, N'Y', N'N', 3400, 0, 28)
INSERT [dbo].[catalog_group] ([catalog_group_id], [catalog_group_name], [catalog_level], [enable], [deletable], [level1_catalog_group_id], [level2_catalog_group_id], [disp_order]) 
VALUES (3500, N'Etc.', 1, N'Y', N'N', 0, 0, 29)
SET IDENTITY_INSERT [dbo].[catalog_group] OFF



/************************************************************************
 *
 * catalog
 *
 ************************************************************************/

SET IDENTITY_INSERT [dbo].[catalog] ON 
INSERT [dbo].[catalog] ([catalog_id], [catalog_group_id], [catalog_name], [catalog_full_name], [manufacture_id], [model_code], [order_code], [size_w], [size_d], [size_h], [weight], [num_of_ports], [image_id], [icon_16_image_id], [icon_32_image_id], [icon_48_image_id], [icon_64_image_id], [link_80_image_id], [deletable], [pp_use_intelligent], [pp_config_type], [pp_media_type], [pp_utp_jack_type], [pp_utp_shield_type], [pp_figure_type], [ic_num_of_pp_connectors], [ic_num_of_max_pp], [ic_num_of_power], [st_num_of_disk], [sw_figure_type], [sw_num_of_slots], [cp_plug_side], [ca_use_intelligent], [ca_install_type], [ca_for_army], [ca_media_type], [ca_is_utp_shield], [ca_utp_cable_type], [ca_fiber_cable_type], [ca_fiber_connector_type], [ca_disp_color_rgb], [ca_disp_name], [rm_is_rack_mount], [rm_unit_size], [rm_image_220_image_id], [rm_image_440_image_id], [rm_image_880_image_id], [user_id], [remarks], [last_updated], [sw_model_type]) 
VALUES (411001, 3110, N'i-Controller (40 PP, 2 Power)', N'Intelligent Controller (40 PP, 2 PO)', 10010001, N'', N'', 450, 300, 45, CAST(4 AS Numeric(18, 0)), 4, 0, 2110006, NULL, NULL, NULL, 2120001, N'N', N'N', N'-', N'-', N'N', N'N', N'-', 10, 40, 2, 0, N'-', 0, N'-', N'N', N'-', N'N', N'-', N'-', N'', N'', N'', 16777215, N'', N'Y', 1, 2130001, 2140001, 0, 90001, N'풀실장 가능 컨트롤러', CAST(0x0000A406011C4CD4 AS DateTime), N'-')
INSERT [dbo].[catalog] ([catalog_id], [catalog_group_id], [catalog_name], [catalog_full_name], [manufacture_id], [model_code], [order_code], [size_w], [size_d], [size_h], [weight], [num_of_ports], [image_id], [icon_16_image_id], [icon_32_image_id], [icon_48_image_id], [icon_64_image_id], [link_80_image_id], [deletable], [pp_use_intelligent], [pp_config_type], [pp_media_type], [pp_utp_jack_type], [pp_utp_shield_type], [pp_figure_type], [ic_num_of_pp_connectors], [ic_num_of_max_pp], [ic_num_of_power], [st_num_of_disk], [sw_figure_type], [sw_num_of_slots], [cp_plug_side], [ca_use_intelligent], [ca_install_type], [ca_for_army], [ca_media_type], [ca_is_utp_shield], [ca_utp_cable_type], [ca_fiber_cable_type], [ca_fiber_connector_type], [ca_disp_color_rgb], [ca_disp_name], [rm_is_rack_mount], [rm_unit_size], [rm_image_220_image_id], [rm_image_440_image_id], [rm_image_880_image_id], [user_id], [remarks], [last_updated], [sw_model_type]) 
VALUES (411002, 3110, N'i-Controller (40 PP, 1 PO)', N'', 10010001, N'', N'', 450, 300, 45, CAST(0 AS Numeric(18, 0)), 4, 0, 2110006, NULL, NULL, NULL, 2120001, N'N', N'N', N'-', N'-', N'N', N'N', N'-', 10, 40, 1, 0, N'-', 0, N'-', N'N', N'-', N'N', N'-', N'-', N'', N'', N'', 16777215, N'', N'Y', 1, 2130001, 2140001, 0, 90001, N'', CAST(0x0000A406011C6C67 AS DateTime), N'-')
INSERT [dbo].[catalog] ([catalog_id], [catalog_group_id], [catalog_name], [catalog_full_name], [manufacture_id], [model_code], [order_code], [size_w], [size_d], [size_h], [weight], [num_of_ports], [image_id], [icon_16_image_id], [icon_32_image_id], [icon_48_image_id], [icon_64_image_id], [link_80_image_id], [deletable], [pp_use_intelligent], [pp_config_type], [pp_media_type], [pp_utp_jack_type], [pp_utp_shield_type], [pp_figure_type], [ic_num_of_pp_connectors], [ic_num_of_max_pp], [ic_num_of_power], [st_num_of_disk], [sw_figure_type], [sw_num_of_slots], [cp_plug_side], [ca_use_intelligent], [ca_install_type], [ca_for_army], [ca_media_type], [ca_is_utp_shield], [ca_utp_cable_type], [ca_fiber_cable_type], [ca_fiber_connector_type], [ca_disp_color_rgb], [ca_disp_name], [rm_is_rack_mount], [rm_unit_size], [rm_image_220_image_id], [rm_image_440_image_id], [rm_image_880_image_id], [user_id], [remarks], [last_updated], [sw_model_type]) 
VALUES (411003, 3110, N'i-Controller (16 PP, 1 PO)', N'', 10010001, N'', N'', 450, 300, 45, CAST(0 AS Numeric(18, 0)), 4, 0, 2110006, NULL, NULL, NULL, 2120001, N'N', N'N', N'-', N'-', N'N', N'N', N'-', 4, 16, 1, 0, N'-', 0, N'-', N'N', N'-', N'N', N'-', N'-', N'', N'', N'', 16777215, N'', N'Y', 1, 2130001, 2140001, 0, 90001, N'', CAST(0x0000A406011C8163 AS DateTime), N'-')
INSERT [dbo].[catalog] ([catalog_id], [catalog_group_id], [catalog_name], [catalog_full_name], [manufacture_id], [model_code], [order_code], [size_w], [size_d], [size_h], [weight], [num_of_ports], [image_id], [icon_16_image_id], [icon_32_image_id], [icon_48_image_id], [icon_64_image_id], [link_80_image_id], [deletable], [pp_use_intelligent], [pp_config_type], [pp_media_type], [pp_utp_jack_type], [pp_utp_shield_type], [pp_figure_type], [ic_num_of_pp_connectors], [ic_num_of_max_pp], [ic_num_of_power], [st_num_of_disk], [sw_figure_type], [sw_num_of_slots], [cp_plug_side], [ca_use_intelligent], [ca_install_type], [ca_for_army], [ca_media_type], [ca_is_utp_shield], [ca_utp_cable_type], [ca_fiber_cable_type], [ca_fiber_connector_type], [ca_disp_color_rgb], [ca_disp_name], [rm_is_rack_mount], [rm_unit_size], [rm_image_220_image_id], [rm_image_440_image_id], [rm_image_880_image_id], [user_id], [remarks], [last_updated], [sw_model_type]) 
VALUES (412001, 3120, N'Energy Box EC2004', N'Energy Box EC2004', null, N'EC2004', N'EC2004', 0, 0, 0, CAST(14 AS Numeric(18, 0)), 9, 0, 2110022, NULL, NULL, NULL, 2120097, N'N', N'N', N'-', N'-', N'N', N'N', N'-', 0, NULL, 2, 0, N'-', 0, N'-', N'N', N'-', N'N', N'-', N'-', N'', N'', N'', 16777215, N'', N'Y', 1, 2130002, 2140002, 0, 0, N'', CAST(0x0000A406011C9F59 AS DateTime), N'-')
INSERT [dbo].[catalog] ([catalog_id], [catalog_group_id], [catalog_name], [catalog_full_name], [manufacture_id], [model_code], [order_code], [size_w], [size_d], [size_h], [weight], [num_of_ports], [image_id], [icon_16_image_id], [icon_32_image_id], [icon_48_image_id], [icon_64_image_id], [link_80_image_id], [deletable], [pp_use_intelligent], [pp_config_type], [pp_media_type], [pp_utp_jack_type], [pp_utp_shield_type], [pp_figure_type], [ic_num_of_pp_connectors], [ic_num_of_max_pp], [ic_num_of_power], [st_num_of_disk], [sw_figure_type], [sw_num_of_slots], [cp_plug_side], [ca_use_intelligent], [ca_install_type], [ca_for_army], [ca_media_type], [ca_is_utp_shield], [ca_utp_cable_type], [ca_fiber_cable_type], [ca_fiber_connector_type], [ca_disp_color_rgb], [ca_disp_name], [rm_is_rack_mount], [rm_unit_size], [rm_image_220_image_id], [rm_image_440_image_id], [rm_image_880_image_id], [user_id], [remarks], [last_updated], [sw_model_type]) 
VALUES (413001, 3130, N'i-Patch Panel (UTP, Cat. 6, XC)', N'', 10010001, N'', N'', 0, 0, 0, CAST(0 AS Numeric(18, 0)), 24, 29001061, 2110007, NULL, NULL, NULL, 2120002, N'N', N'Y', N'X', N'U', N'M', N'N', N'F', 0, NULL, 2, 0, N'-', 0, N'-', N'N', N'-', N'N', N'-', N'-', N'', N'', N'', 16777215, N'', N'Y', 1, 2130003, 2140003, 0, 90001, N'', CAST(0x0000A406011CB160 AS DateTime), N'-')
INSERT [dbo].[catalog] ([catalog_id], [catalog_group_id], [catalog_name], [catalog_full_name], [manufacture_id], [model_code], [order_code], [size_w], [size_d], [size_h], [weight], [num_of_ports], [image_id], [icon_16_image_id], [icon_32_image_id], [icon_48_image_id], [icon_64_image_id], [link_80_image_id], [deletable], [pp_use_intelligent], [pp_config_type], [pp_media_type], [pp_utp_jack_type], [pp_utp_shield_type], [pp_figure_type], [ic_num_of_pp_connectors], [ic_num_of_max_pp], [ic_num_of_power], [st_num_of_disk], [sw_figure_type], [sw_num_of_slots], [cp_plug_side], [ca_use_intelligent], [ca_install_type], [ca_for_army], [ca_media_type], [ca_is_utp_shield], [ca_utp_cable_type], [ca_fiber_cable_type], [ca_fiber_connector_type], [ca_disp_color_rgb], [ca_disp_name], [rm_is_rack_mount], [rm_unit_size], [rm_image_220_image_id], [rm_image_440_image_id], [rm_image_880_image_id], [user_id], [remarks], [last_updated], [sw_model_type]) 
VALUES (413002, 3130, N'i-Patch Panel (Angled, UTP, Cat. 6, XC)', N'', 10010001, N'', N'', 0, 0, 0, CAST(0 AS Numeric(18, 0)), 24, 0, 2110007, NULL, NULL, NULL, 2120002, N'N', N'Y', N'X', N'U', N'M', N'N', N'A', 0, NULL, 2, 0, N'-', 0, N'-', N'N', N'-', N'N', N'-', N'-', N'', N'', N'', 16777215, N'', N'Y', 1, 2130004, 2140004, 0, 90001, N'', CAST(0x0000A406011CC125 AS DateTime), N'-')
INSERT [dbo].[catalog] ([catalog_id], [catalog_group_id], [catalog_name], [catalog_full_name], [manufacture_id], [model_code], [order_code], [size_w], [size_d], [size_h], [weight], [num_of_ports], [image_id], [icon_16_image_id], [icon_32_image_id], [icon_48_image_id], [icon_64_image_id], [link_80_image_id], [deletable], [pp_use_intelligent], [pp_config_type], [pp_media_type], [pp_utp_jack_type], [pp_utp_shield_type], [pp_figure_type], [ic_num_of_pp_connectors], [ic_num_of_max_pp], [ic_num_of_power], [st_num_of_disk], [sw_figure_type], [sw_num_of_slots], [cp_plug_side], [ca_use_intelligent], [ca_install_type], [ca_for_army], [ca_media_type], [ca_is_utp_shield], [ca_utp_cable_type], [ca_fiber_cable_type], [ca_fiber_connector_type], [ca_disp_color_rgb], [ca_disp_name], [rm_is_rack_mount], [rm_unit_size], [rm_image_220_image_id], [rm_image_440_image_id], [rm_image_880_image_id], [user_id], [remarks], [last_updated], [sw_model_type]) 
VALUES (413003, 3130, N'i-Patch Panel (Shield, UTP, Cat. 6, XC)', N'', 10010001, N'', N'', 0, 0, 0, CAST(0 AS Numeric(18, 0)), 24, 0, 2110007, NULL, NULL, NULL, 2120002, N'N', N'Y', N'X', N'U', N'M', N'N', N'F', 0, NULL, 2, 0, N'-', 0, N'-', N'N', N'-', N'N', N'-', N'-', N'', N'', N'', 16777215, N'', N'Y', 1, 2130003, 2140003, 0, 90001, N'', CAST(0x0000A406011CD027 AS DateTime), N'-')
INSERT [dbo].[catalog] ([catalog_id], [catalog_group_id], [catalog_name], [catalog_full_name], [manufacture_id], [model_code], [order_code], [size_w], [size_d], [size_h], [weight], [num_of_ports], [image_id], [icon_16_image_id], [icon_32_image_id], [icon_48_image_id], [icon_64_image_id], [link_80_image_id], [deletable], [pp_use_intelligent], [pp_config_type], [pp_media_type], [pp_utp_jack_type], [pp_utp_shield_type], [pp_figure_type], [ic_num_of_pp_connectors], [ic_num_of_max_pp], [ic_num_of_power], [st_num_of_disk], [sw_figure_type], [sw_num_of_slots], [cp_plug_side], [ca_use_intelligent], [ca_install_type], [ca_for_army], [ca_media_type], [ca_is_utp_shield], [ca_utp_cable_type], [ca_fiber_cable_type], [ca_fiber_connector_type], [ca_disp_color_rgb], [ca_disp_name], [rm_is_rack_mount], [rm_unit_size], [rm_image_220_image_id], [rm_image_440_image_id], [rm_image_880_image_id], [user_id], [remarks], [last_updated], [sw_model_type]) 
VALUES (413004, 3130, N'i-Patch Panel (Angled, Shield, UTP, Cat. 6, XC)', N'', 10010001, N'', N'', 0, 0, 0, CAST(0 AS Numeric(18, 0)), 24, 0, 2110007, NULL, NULL, NULL, 2120002, N'N', N'Y', N'X', N'U', N'M', N'N', N'A', 0, NULL, 2, 0, N'-', 0, N'-', N'N', N'-', N'N', N'-', N'-', N'', N'', N'', 16777215, N'', N'Y', 1, 2130004, 2140004, 0, 90001, N'', CAST(0x0000A406011CEF40 AS DateTime), N'-')
INSERT [dbo].[catalog] ([catalog_id], [catalog_group_id], [catalog_name], [catalog_full_name], [manufacture_id], [model_code], [order_code], [size_w], [size_d], [size_h], [weight], [num_of_ports], [image_id], [icon_16_image_id], [icon_32_image_id], [icon_48_image_id], [icon_64_image_id], [link_80_image_id], [deletable], [pp_use_intelligent], [pp_config_type], [pp_media_type], [pp_utp_jack_type], [pp_utp_shield_type], [pp_figure_type], [ic_num_of_pp_connectors], [ic_num_of_max_pp], [ic_num_of_power], [st_num_of_disk], [sw_figure_type], [sw_num_of_slots], [cp_plug_side], [ca_use_intelligent], [ca_install_type], [ca_for_army], [ca_media_type], [ca_is_utp_shield], [ca_utp_cable_type], [ca_fiber_cable_type], [ca_fiber_connector_type], [ca_disp_color_rgb], [ca_disp_name], [rm_is_rack_mount], [rm_unit_size], [rm_image_220_image_id], [rm_image_440_image_id], [rm_image_880_image_id], [user_id], [remarks], [last_updated], [sw_model_type]) 
VALUES (413005, 3130, N'i-Patch Panel (UTP, Cat. 6, IC)', N'', 10010001, N'', N'', 0, 0, 0, CAST(0 AS Numeric(18, 0)), 24, 0, 2110007, NULL, NULL, NULL, 2120002, N'N', N'Y', N'I', N'U', N'M', N'N', N'F', 0, NULL, 2, 0, N'-', 0, N'-', N'N', N'-', N'N', N'-', N'-', N'', N'', N'', 16777215, N'', N'Y', 1, 2130003, 2140003, 0, 90001, N'', CAST(0x0000A406011CFFAB AS DateTime), N'-')
INSERT [dbo].[catalog] ([catalog_id], [catalog_group_id], [catalog_name], [catalog_full_name], [manufacture_id], [model_code], [order_code], [size_w], [size_d], [size_h], [weight], [num_of_ports], [image_id], [icon_16_image_id], [icon_32_image_id], [icon_48_image_id], [icon_64_image_id], [link_80_image_id], [deletable], [pp_use_intelligent], [pp_config_type], [pp_media_type], [pp_utp_jack_type], [pp_utp_shield_type], [pp_figure_type], [ic_num_of_pp_connectors], [ic_num_of_max_pp], [ic_num_of_power], [st_num_of_disk], [sw_figure_type], [sw_num_of_slots], [cp_plug_side], [ca_use_intelligent], [ca_install_type], [ca_for_army], [ca_media_type], [ca_is_utp_shield], [ca_utp_cable_type], [ca_fiber_cable_type], [ca_fiber_connector_type], [ca_disp_color_rgb], [ca_disp_name], [rm_is_rack_mount], [rm_unit_size], [rm_image_220_image_id], [rm_image_440_image_id], [rm_image_880_image_id], [user_id], [remarks], [last_updated], [sw_model_type]) 
VALUES (413006, 3130, N'i-Patch Panel (Angled, UTP, Cat. 6, IC)', N'', 10010001, N'', N'', 0, 0, 0, CAST(0 AS Numeric(18, 0)), 24, 0, 2110007, NULL, NULL, NULL, 2120002, N'N', N'Y', N'I', N'U', N'M', N'N', N'A', 0, NULL, 2, 0, N'-', 0, N'-', N'N', N'-', N'N', N'-', N'-', N'', N'', N'', 16777215, N'', N'Y', 1, 2130004, 2140004, 0, 90001, N'', CAST(0x0000A406011F3989 AS DateTime), N'-')
INSERT [dbo].[catalog] ([catalog_id], [catalog_group_id], [catalog_name], [catalog_full_name], [manufacture_id], [model_code], [order_code], [size_w], [size_d], [size_h], [weight], [num_of_ports], [image_id], [icon_16_image_id], [icon_32_image_id], [icon_48_image_id], [icon_64_image_id], [link_80_image_id], [deletable], [pp_use_intelligent], [pp_config_type], [pp_media_type], [pp_utp_jack_type], [pp_utp_shield_type], [pp_figure_type], [ic_num_of_pp_connectors], [ic_num_of_max_pp], [ic_num_of_power], [st_num_of_disk], [sw_figure_type], [sw_num_of_slots], [cp_plug_side], [ca_use_intelligent], [ca_install_type], [ca_for_army], [ca_media_type], [ca_is_utp_shield], [ca_utp_cable_type], [ca_fiber_cable_type], [ca_fiber_connector_type], [ca_disp_color_rgb], [ca_disp_name], [rm_is_rack_mount], [rm_unit_size], [rm_image_220_image_id], [rm_image_440_image_id], [rm_image_880_image_id], [user_id], [remarks], [last_updated], [sw_model_type]) 
VALUES (415001, 3150, N'i-Patch Cord (UTP, XC)', N'', 10010001, N'', N'', 0, 0, 0, CAST(0 AS Numeric(18, 0)), 0, NULL, 2110099, NULL, NULL, NULL, 2120099, N'Y', N'N', N'-', N'-', N'N', N'N', N'-', 0, NULL, 1, 0, N'-', 0, N'-', N'Y', N'X', N'N', N'U', N'-', N'6', N'-', N'-', -250882597, N'i-패치코드', NULL, NULL, NULL, NULL, 0, 90001, N'', CAST(0x0000A39A009812B8 AS DateTime), NULL)
INSERT [dbo].[catalog] ([catalog_id], [catalog_group_id], [catalog_name], [catalog_full_name], [manufacture_id], [model_code], [order_code], [size_w], [size_d], [size_h], [weight], [num_of_ports], [image_id], [icon_16_image_id], [icon_32_image_id], [icon_48_image_id], [icon_64_image_id], [link_80_image_id], [deletable], [pp_use_intelligent], [pp_config_type], [pp_media_type], [pp_utp_jack_type], [pp_utp_shield_type], [pp_figure_type], [ic_num_of_pp_connectors], [ic_num_of_max_pp], [ic_num_of_power], [st_num_of_disk], [sw_figure_type], [sw_num_of_slots], [cp_plug_side], [ca_use_intelligent], [ca_install_type], [ca_for_army], [ca_media_type], [ca_is_utp_shield], [ca_utp_cable_type], [ca_fiber_cable_type], [ca_fiber_connector_type], [ca_disp_color_rgb], [ca_disp_name], [rm_is_rack_mount], [rm_unit_size], [rm_image_220_image_id], [rm_image_440_image_id], [rm_image_880_image_id], [user_id], [remarks], [last_updated], [sw_model_type]) 
VALUES (415002, 3150, N'i-Patch Cord (Shield, UTP, XC)', N'', 10010001, N'', NULL, NULL, NULL, NULL, NULL, 0, NULL, 2110099, NULL, NULL, NULL, 2120099, N'Y', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Y', N'X', N'N', N'U', N'Y', N'6', NULL, NULL, 255, N'i-Patch Cord', NULL, NULL, NULL, NULL, 0, 90001, NULL, CAST(0x0000A39A009812B8 AS DateTime), NULL)
INSERT [dbo].[catalog] ([catalog_id], [catalog_group_id], [catalog_name], [catalog_full_name], [manufacture_id], [model_code], [order_code], [size_w], [size_d], [size_h], [weight], [num_of_ports], [image_id], [icon_16_image_id], [icon_32_image_id], [icon_48_image_id], [icon_64_image_id], [link_80_image_id], [deletable], [pp_use_intelligent], [pp_config_type], [pp_media_type], [pp_utp_jack_type], [pp_utp_shield_type], [pp_figure_type], [ic_num_of_pp_connectors], [ic_num_of_max_pp], [ic_num_of_power], [st_num_of_disk], [sw_figure_type], [sw_num_of_slots], [cp_plug_side], [ca_use_intelligent], [ca_install_type], [ca_for_army], [ca_media_type], [ca_is_utp_shield], [ca_utp_cable_type], [ca_fiber_cable_type], [ca_fiber_connector_type], [ca_disp_color_rgb], [ca_disp_name], [rm_is_rack_mount], [rm_unit_size], [rm_image_220_image_id], [rm_image_440_image_id], [rm_image_880_image_id], [user_id], [remarks], [last_updated], [sw_model_type]) 
VALUES (415003, 3150, N'i-Patch Cord (UTP, IC)', N'', 10010001, N'', NULL, NULL, NULL, NULL, NULL, 0, NULL, 2110099, NULL, NULL, NULL, 2120099, N'Y', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Y', N'I', N'N', N'U', N'N', N'6A', NULL, NULL, 255, N'i-Patch Cord', NULL, NULL, NULL, NULL, 0, 90001, NULL, CAST(0x0000A39A009812B8 AS DateTime), NULL)

INSERT [dbo].[catalog] ([catalog_id], [catalog_group_id], [catalog_name], [catalog_full_name], [manufacture_id], [model_code], [order_code], [size_w], [size_d], [size_h], [weight], [num_of_ports], [image_id], [icon_16_image_id], [icon_32_image_id], [icon_48_image_id], [icon_64_image_id], [link_80_image_id], [deletable], [pp_use_intelligent], [pp_config_type], [pp_media_type], [pp_utp_jack_type], [pp_utp_shield_type], [pp_figure_type], [ic_num_of_pp_connectors], [ic_num_of_max_pp], [ic_num_of_power], [st_num_of_disk], [sw_figure_type], [sw_num_of_slots], [cp_plug_side], [ca_use_intelligent], [ca_install_type], [ca_for_army], [ca_media_type], [ca_is_utp_shield], [ca_utp_cable_type], [ca_fiber_cable_type], [ca_fiber_connector_type], [ca_disp_color_rgb], [ca_disp_name], [rm_is_rack_mount], [rm_unit_size], [rm_image_220_image_id], [rm_image_440_image_id], [rm_image_880_image_id], [user_id], [remarks], [last_updated], [sw_model_type]) 
VALUES (421001, 3210, N'General 19" Standard Rack (20U)', NULL, 10010001, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, 2110005, NULL, NULL, NULL, 2120099, N'N', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 20, NULL, NULL, 0, 90001, NULL, CAST(0x0000A39A009812B8 AS DateTime), NULL)
INSERT [dbo].[catalog] ([catalog_id], [catalog_group_id], [catalog_name], [catalog_full_name], [manufacture_id], [model_code], [order_code], [size_w], [size_d], [size_h], [weight], [num_of_ports], [image_id], [icon_16_image_id], [icon_32_image_id], [icon_48_image_id], [icon_64_image_id], [link_80_image_id], [deletable], [pp_use_intelligent], [pp_config_type], [pp_media_type], [pp_utp_jack_type], [pp_utp_shield_type], [pp_figure_type], [ic_num_of_pp_connectors], [ic_num_of_max_pp], [ic_num_of_power], [st_num_of_disk], [sw_figure_type], [sw_num_of_slots], [cp_plug_side], [ca_use_intelligent], [ca_install_type], [ca_for_army], [ca_media_type], [ca_is_utp_shield], [ca_utp_cable_type], [ca_fiber_cable_type], [ca_fiber_connector_type], [ca_disp_color_rgb], [ca_disp_name], [rm_is_rack_mount], [rm_unit_size], [rm_image_220_image_id], [rm_image_440_image_id], [rm_image_880_image_id], [user_id], [remarks], [last_updated], [sw_model_type]) 
VALUES (421002, 3210, N'General 19" Standard Rack (22U)', N'', 10010001, N'', N'', 0, 0, 0, CAST(0 AS Numeric(18, 0)), 0, 0, 2110005, NULL, NULL, NULL, 2120099, N'N', N'N', N'-', N'-', N'N', N'N', N'-', 0, NULL, 2, 0, N'-', 0, N'-', N'N', NULL, N'N', N'-', N'-', N'-', N'-', N'-', 16777215, N'', N'N', 22, 0, 0, 0, 0, N'', CAST(0x0000A39A009812B8 AS DateTime), NULL)
INSERT [dbo].[catalog] ([catalog_id], [catalog_group_id], [catalog_name], [catalog_full_name], [manufacture_id], [model_code], [order_code], [size_w], [size_d], [size_h], [weight], [num_of_ports], [image_id], [icon_16_image_id], [icon_32_image_id], [icon_48_image_id], [icon_64_image_id], [link_80_image_id], [deletable], [pp_use_intelligent], [pp_config_type], [pp_media_type], [pp_utp_jack_type], [pp_utp_shield_type], [pp_figure_type], [ic_num_of_pp_connectors], [ic_num_of_max_pp], [ic_num_of_power], [st_num_of_disk], [sw_figure_type], [sw_num_of_slots], [cp_plug_side], [ca_use_intelligent], [ca_install_type], [ca_for_army], [ca_media_type], [ca_is_utp_shield], [ca_utp_cable_type], [ca_fiber_cable_type], [ca_fiber_connector_type], [ca_disp_color_rgb], [ca_disp_name], [rm_is_rack_mount], [rm_unit_size], [rm_image_220_image_id], [rm_image_440_image_id], [rm_image_880_image_id], [user_id], [remarks], [last_updated], [sw_model_type]) 
VALUES (421003, 3210, N'General 19" Standard Rack (42U)', NULL, 10010001, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, 2110005, NULL, NULL, NULL, 2120099, N'N', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 40, NULL, NULL, 0, 90001, NULL, CAST(0x0000A39A009812B8 AS DateTime), NULL)
INSERT [dbo].[catalog] ([catalog_id], [catalog_group_id], [catalog_name], [catalog_full_name], [manufacture_id], [model_code], [order_code], [size_w], [size_d], [size_h], [weight], [num_of_ports], [image_id], [icon_16_image_id], [icon_32_image_id], [icon_48_image_id], [icon_64_image_id], [link_80_image_id], [deletable], [pp_use_intelligent], [pp_config_type], [pp_media_type], [pp_utp_jack_type], [pp_utp_shield_type], [pp_figure_type], [ic_num_of_pp_connectors], [ic_num_of_max_pp], [ic_num_of_power], [st_num_of_disk], [sw_figure_type], [sw_num_of_slots], [cp_plug_side], [ca_use_intelligent], [ca_install_type], [ca_for_army], [ca_media_type], [ca_is_utp_shield], [ca_utp_cable_type], [ca_fiber_cable_type], [ca_fiber_connector_type], [ca_disp_color_rgb], [ca_disp_name], [rm_is_rack_mount], [rm_unit_size], [rm_image_220_image_id], [rm_image_440_image_id], [rm_image_880_image_id], [user_id], [remarks], [last_updated], [sw_model_type]) 
VALUES (421004, 3210, N'General 19" Standard Rack (42U)', NULL, 10010001, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, 2110005, NULL, NULL, NULL, 2120099, N'N', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 42, NULL, NULL, 0, 90001, NULL, CAST(0x0000A39A009812B8 AS DateTime), NULL)
INSERT [dbo].[catalog] ([catalog_id], [catalog_group_id], [catalog_name], [catalog_full_name], [manufacture_id], [model_code], [order_code], [size_w], [size_d], [size_h], [weight], [num_of_ports], [image_id], [icon_16_image_id], [icon_32_image_id], [icon_48_image_id], [icon_64_image_id], [link_80_image_id], [deletable], [pp_use_intelligent], [pp_config_type], [pp_media_type], [pp_utp_jack_type], [pp_utp_shield_type], [pp_figure_type], [ic_num_of_pp_connectors], [ic_num_of_max_pp], [ic_num_of_power], [st_num_of_disk], [sw_figure_type], [sw_num_of_slots], [cp_plug_side], [ca_use_intelligent], [ca_install_type], [ca_for_army], [ca_media_type], [ca_is_utp_shield], [ca_utp_cable_type], [ca_fiber_cable_type], [ca_fiber_connector_type], [ca_disp_color_rgb], [ca_disp_name], [rm_is_rack_mount], [rm_unit_size], [rm_image_220_image_id], [rm_image_440_image_id], [rm_image_880_image_id], [user_id], [remarks], [last_updated], [sw_model_type]) 
VALUES (421005, 3210, N'General 19" Standard Rack (44U)', NULL, 10010001, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, 2110005, NULL, NULL, NULL, 2120099, N'N', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 44, NULL, NULL, 0, 90001, NULL, CAST(0x0000A39A009812B8 AS DateTime), NULL)
INSERT [dbo].[catalog] ([catalog_id], [catalog_group_id], [catalog_name], [catalog_full_name], [manufacture_id], [model_code], [order_code], [size_w], [size_d], [size_h], [weight], [num_of_ports], [image_id], [icon_16_image_id], [icon_32_image_id], [icon_48_image_id], [icon_64_image_id], [link_80_image_id], [deletable], [pp_use_intelligent], [pp_config_type], [pp_media_type], [pp_utp_jack_type], [pp_utp_shield_type], [pp_figure_type], [ic_num_of_pp_connectors], [ic_num_of_max_pp], [ic_num_of_power], [st_num_of_disk], [sw_figure_type], [sw_num_of_slots], [cp_plug_side], [ca_use_intelligent], [ca_install_type], [ca_for_army], [ca_media_type], [ca_is_utp_shield], [ca_utp_cable_type], [ca_fiber_cable_type], [ca_fiber_connector_type], [ca_disp_color_rgb], [ca_disp_name], [rm_is_rack_mount], [rm_unit_size], [rm_image_220_image_id], [rm_image_440_image_id], [rm_image_880_image_id], [user_id], [remarks], [last_updated], [sw_model_type]) 
VALUES (421006, 3210, N'General 19" Standard Rack (46U)', NULL, 10010001, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, 2110005, NULL, NULL, NULL, 2120099, N'N', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 46, NULL, NULL, 0, 90001, NULL, CAST(0x0000A39A009812B8 AS DateTime), NULL)
INSERT [dbo].[catalog] ([catalog_id], [catalog_group_id], [catalog_name], [catalog_full_name], [manufacture_id], [model_code], [order_code], [size_w], [size_d], [size_h], [weight], [num_of_ports], [image_id], [icon_16_image_id], [icon_32_image_id], [icon_48_image_id], [icon_64_image_id], [link_80_image_id], [deletable], [pp_use_intelligent], [pp_config_type], [pp_media_type], [pp_utp_jack_type], [pp_utp_shield_type], [pp_figure_type], [ic_num_of_pp_connectors], [ic_num_of_max_pp], [ic_num_of_power], [st_num_of_disk], [sw_figure_type], [sw_num_of_slots], [cp_plug_side], [ca_use_intelligent], [ca_install_type], [ca_for_army], [ca_media_type], [ca_is_utp_shield], [ca_utp_cable_type], [ca_fiber_cable_type], [ca_fiber_connector_type], [ca_disp_color_rgb], [ca_disp_name], [rm_is_rack_mount], [rm_unit_size], [rm_image_220_image_id], [rm_image_440_image_id], [rm_image_880_image_id], [user_id], [remarks], [last_updated], [sw_model_type]) 
VALUES (421007, 3210, N'General 19" Standard Rack (48U)', NULL, 10010001, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, 2110005, NULL, NULL, NULL, 2120099, N'N', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 48, NULL, NULL, 0, 90001, NULL, CAST(0x0000A39A009812B8 AS DateTime), NULL)
INSERT [dbo].[catalog] ([catalog_id], [catalog_group_id], [catalog_name], [catalog_full_name], [manufacture_id], [model_code], [order_code], [size_w], [size_d], [size_h], [weight], [num_of_ports], [image_id], [icon_16_image_id], [icon_32_image_id], [icon_48_image_id], [icon_64_image_id], [link_80_image_id], [deletable], [pp_use_intelligent], [pp_config_type], [pp_media_type], [pp_utp_jack_type], [pp_utp_shield_type], [pp_figure_type], [ic_num_of_pp_connectors], [ic_num_of_max_pp], [ic_num_of_power], [st_num_of_disk], [sw_figure_type], [sw_num_of_slots], [cp_plug_side], [ca_use_intelligent], [ca_install_type], [ca_for_army], [ca_media_type], [ca_is_utp_shield], [ca_utp_cable_type], [ca_fiber_cable_type], [ca_fiber_connector_type], [ca_disp_color_rgb], [ca_disp_name], [rm_is_rack_mount], [rm_unit_size], [rm_image_220_image_id], [rm_image_440_image_id], [rm_image_880_image_id], [user_id], [remarks], [last_updated], [sw_model_type]) 
VALUES (421008, 3210, N'General 19" Standard Rack (50U)', NULL, 10010001, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, 2110005, NULL, NULL, NULL, 2120099, N'N', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 50, NULL, NULL, 0, 90001, NULL, CAST(0x0000A39A009812B8 AS DateTime), NULL)
INSERT [dbo].[catalog] ([catalog_id], [catalog_group_id], [catalog_name], [catalog_full_name], [manufacture_id], [model_code], [order_code], [size_w], [size_d], [size_h], [weight], [num_of_ports], [image_id], [icon_16_image_id], [icon_32_image_id], [icon_48_image_id], [icon_64_image_id], [link_80_image_id], [deletable], [pp_use_intelligent], [pp_config_type], [pp_media_type], [pp_utp_jack_type], [pp_utp_shield_type], [pp_figure_type], [ic_num_of_pp_connectors], [ic_num_of_max_pp], [ic_num_of_power], [st_num_of_disk], [sw_figure_type], [sw_num_of_slots], [cp_plug_side], [ca_use_intelligent], [ca_install_type], [ca_for_army], [ca_media_type], [ca_is_utp_shield], [ca_utp_cable_type], [ca_fiber_cable_type], [ca_fiber_connector_type], [ca_disp_color_rgb], [ca_disp_name], [rm_is_rack_mount], [rm_unit_size], [rm_image_220_image_id], [rm_image_440_image_id], [rm_image_880_image_id], [user_id], [remarks], [last_updated], [sw_model_type]) 
VALUES (421009, 3210, N'General 19" Standard Rack (52U)', N'', 10010001, N'', N'', 0, 0, 0, CAST(0 AS Numeric(18, 0)), 0, 0, 2110005, NULL, NULL, NULL, 2120099, N'N', N'N', N'-', N'-', N'N', N'N', N'-', 0, NULL, 1, 0, N'-', 0, N'-', N'N', NULL, N'N', N'-', N'-', N'-', N'-', N'-', 16777215, N'', N'N', 52, 0, 0, 0, 0, N'', CAST(0x0000A39A009812B8 AS DateTime), NULL)
INSERT [dbo].[catalog] ([catalog_id], [catalog_group_id], [catalog_name], [catalog_full_name], [manufacture_id], [model_code], [order_code], [size_w], [size_d], [size_h], [weight], [num_of_ports], [image_id], [icon_16_image_id], [icon_32_image_id], [icon_48_image_id], [icon_64_image_id], [link_80_image_id], [deletable], [pp_use_intelligent], [pp_config_type], [pp_media_type], [pp_utp_jack_type], [pp_utp_shield_type], [pp_figure_type], [ic_num_of_pp_connectors], [ic_num_of_max_pp], [ic_num_of_power], [st_num_of_disk], [sw_figure_type], [sw_num_of_slots], [cp_plug_side], [ca_use_intelligent], [ca_install_type], [ca_for_army], [ca_media_type], [ca_is_utp_shield], [ca_utp_cable_type], [ca_fiber_cable_type], [ca_fiber_connector_type], [ca_disp_color_rgb], [ca_disp_name], [rm_is_rack_mount], [rm_unit_size], [rm_image_220_image_id], [rm_image_440_image_id], [rm_image_880_image_id], [user_id], [remarks], [last_updated], [sw_model_type]) 
VALUES (422001, 3220, N'General Vertical Cable Manager (150)', N'', 10010001, N'', N'', 0, 0, 0, CAST(0 AS Numeric(18, 0)), 0, 0, 2110099, NULL, NULL, NULL, 2120099, N'N', N'N', N'-', N'-', N'N', N'N', N'-', 0, NULL, 2, 0, N'-', 0, N'-', N'N', N'F', N'N', N'-', N'-', N'', N'', N'', 16777215, N'', N'N', 1, 0, 0, 0, 90001, N'', CAST(0x0000A39A009812B8 AS DateTime), NULL)
INSERT [dbo].[catalog] ([catalog_id], [catalog_group_id], [catalog_name], [catalog_full_name], [manufacture_id], [model_code], [order_code], [size_w], [size_d], [size_h], [weight], [num_of_ports], [image_id], [icon_16_image_id], [icon_32_image_id], [icon_48_image_id], [icon_64_image_id], [link_80_image_id], [deletable], [pp_use_intelligent], [pp_config_type], [pp_media_type], [pp_utp_jack_type], [pp_utp_shield_type], [pp_figure_type], [ic_num_of_pp_connectors], [ic_num_of_max_pp], [ic_num_of_power], [st_num_of_disk], [sw_figure_type], [sw_num_of_slots], [cp_plug_side], [ca_use_intelligent], [ca_install_type], [ca_for_army], [ca_media_type], [ca_is_utp_shield], [ca_utp_cable_type], [ca_fiber_cable_type], [ca_fiber_connector_type], [ca_disp_color_rgb], [ca_disp_name], [rm_is_rack_mount], [rm_unit_size], [rm_image_220_image_id], [rm_image_440_image_id], [rm_image_880_image_id], [user_id], [remarks], [last_updated], [sw_model_type]) 
VALUES (422002, 3220, N'General Vertical Cable Manager (200)', N'', 10010001, N'', N'', 0, 0, 0, CAST(0 AS Numeric(18, 0)), 0, 0, 2110099, NULL, NULL, NULL, 2120099, N'N', N'N', N'-', N'-', N'N', N'N', N'-', 0, NULL, 2, 0, N'-', 0, N'-', N'N', N'F', N'N', N'-', N'-', N'', N'', N'', 16777215, N'', N'N', 1, 0, 0, 0, 90001, N'', CAST(0x0000A39A009812B8 AS DateTime), NULL)
INSERT [dbo].[catalog] ([catalog_id], [catalog_group_id], [catalog_name], [catalog_full_name], [manufacture_id], [model_code], [order_code], [size_w], [size_d], [size_h], [weight], [num_of_ports], [image_id], [icon_16_image_id], [icon_32_image_id], [icon_48_image_id], [icon_64_image_id], [link_80_image_id], [deletable], [pp_use_intelligent], [pp_config_type], [pp_media_type], [pp_utp_jack_type], [pp_utp_shield_type], [pp_figure_type], [ic_num_of_pp_connectors], [ic_num_of_max_pp], [ic_num_of_power], [st_num_of_disk], [sw_figure_type], [sw_num_of_slots], [cp_plug_side], [ca_use_intelligent], [ca_install_type], [ca_for_army], [ca_media_type], [ca_is_utp_shield], [ca_utp_cable_type], [ca_fiber_cable_type], [ca_fiber_connector_type], [ca_disp_color_rgb], [ca_disp_name], [rm_is_rack_mount], [rm_unit_size], [rm_image_220_image_id], [rm_image_440_image_id], [rm_image_880_image_id], [user_id], [remarks], [last_updated], [sw_model_type]) 
VALUES (422003, 3220, N'General Vertical Cable Manager (250)', N'', 10010001, N'', N'', 0, 0, 0, CAST(0 AS Numeric(18, 0)), 0, 0, 2110099, NULL, NULL, NULL, 2120099, N'N', N'N', N'-', N'-', N'N', N'N', N'-', 0, NULL, 2, 0, N'-', 0, N'-', N'N', N'F', N'N', N'-', N'-', N'', N'', N'', 16777215, N'', N'N', 1, 0, 0, 0, 90001, N'', CAST(0x0000A39A009812B8 AS DateTime), NULL)
INSERT [dbo].[catalog] ([catalog_id], [catalog_group_id], [catalog_name], [catalog_full_name], [manufacture_id], [model_code], [order_code], [size_w], [size_d], [size_h], [weight], [num_of_ports], [image_id], [icon_16_image_id], [icon_32_image_id], [icon_48_image_id], [icon_64_image_id], [link_80_image_id], [deletable], [pp_use_intelligent], [pp_config_type], [pp_media_type], [pp_utp_jack_type], [pp_utp_shield_type], [pp_figure_type], [ic_num_of_pp_connectors], [ic_num_of_max_pp], [ic_num_of_power], [st_num_of_disk], [sw_figure_type], [sw_num_of_slots], [cp_plug_side], [ca_use_intelligent], [ca_install_type], [ca_for_army], [ca_media_type], [ca_is_utp_shield], [ca_utp_cable_type], [ca_fiber_cable_type], [ca_fiber_connector_type], [ca_disp_color_rgb], [ca_disp_name], [rm_is_rack_mount], [rm_unit_size], [rm_image_220_image_id], [rm_image_440_image_id], [rm_image_880_image_id], [user_id], [remarks], [last_updated], [sw_model_type]) 
VALUES (422004, 3220, N'General Vertical Cable Manager (300)', N'', 10010001, N'', N'', 0, 0, 0, CAST(0 AS Numeric(18, 0)), 0, 0, 2110099, NULL, NULL, NULL, 2120099, N'N', N'N', N'-', N'-', N'N', N'N', N'-', 0, NULL, 2, 0, N'-', 0, N'-', N'N', N'F', N'N', N'-', N'-', N'', N'', N'', 16777215, N'', N'N', 1, 0, 0, 0, 90001, N'', CAST(0x0000A39A009812B8 AS DateTime), NULL)
INSERT [dbo].[catalog] ([catalog_id], [catalog_group_id], [catalog_name], [catalog_full_name], [manufacture_id], [model_code], [order_code], [size_w], [size_d], [size_h], [weight], [num_of_ports], [image_id], [icon_16_image_id], [icon_32_image_id], [icon_48_image_id], [icon_64_image_id], [link_80_image_id], [deletable], [pp_use_intelligent], [pp_config_type], [pp_media_type], [pp_utp_jack_type], [pp_utp_shield_type], [pp_figure_type], [ic_num_of_pp_connectors], [ic_num_of_max_pp], [ic_num_of_power], [st_num_of_disk], [sw_figure_type], [sw_num_of_slots], [cp_plug_side], [ca_use_intelligent], [ca_install_type], [ca_for_army], [ca_media_type], [ca_is_utp_shield], [ca_utp_cable_type], [ca_fiber_cable_type], [ca_fiber_connector_type], [ca_disp_color_rgb], [ca_disp_name], [rm_is_rack_mount], [rm_unit_size], [rm_image_220_image_id], [rm_image_440_image_id], [rm_image_880_image_id], [user_id], [remarks], [last_updated], [sw_model_type]) 
VALUES (423001, 3230, N'General 19" Blank Panel (1U)', N'', 10010001, N'', N'', 0, 0, 0, CAST(0 AS Numeric(18, 0)), 0, 0, 2110099, NULL, NULL, NULL, 2120099, N'N', N'N', N'-', N'-', N'N', N'N', N'-', 0, NULL, 1, 0, N'-', 0, N'-', N'N', N'-', N'N', N'-', N'-', N'', N'', N'', 16777215, N'', N'Y', 1, 2130005, 2140005, 0, 90001, N'', CAST(0x0000A406011D1F69 AS DateTime), N'-')
INSERT [dbo].[catalog] ([catalog_id], [catalog_group_id], [catalog_name], [catalog_full_name], [manufacture_id], [model_code], [order_code], [size_w], [size_d], [size_h], [weight], [num_of_ports], [image_id], [icon_16_image_id], [icon_32_image_id], [icon_48_image_id], [icon_64_image_id], [link_80_image_id], [deletable], [pp_use_intelligent], [pp_config_type], [pp_media_type], [pp_utp_jack_type], [pp_utp_shield_type], [pp_figure_type], [ic_num_of_pp_connectors], [ic_num_of_max_pp], [ic_num_of_power], [st_num_of_disk], [sw_figure_type], [sw_num_of_slots], [cp_plug_side], [ca_use_intelligent], [ca_install_type], [ca_for_army], [ca_media_type], [ca_is_utp_shield], [ca_utp_cable_type], [ca_fiber_cable_type], [ca_fiber_connector_type], [ca_disp_color_rgb], [ca_disp_name], [rm_is_rack_mount], [rm_unit_size], [rm_image_220_image_id], [rm_image_440_image_id], [rm_image_880_image_id], [user_id], [remarks], [last_updated], [sw_model_type]) 
VALUES (423002, 3230, N'General 19" Blank Panel (2U)', N'', 10010001, N'', N'', 0, 0, 0, CAST(0 AS Numeric(18, 0)), 0, 0, 2110099, NULL, NULL, NULL, 2120099, N'N', N'N', N'-', N'-', N'N', N'N', N'-', 0, NULL, 1, 0, N'-', 0, N'-', N'N', N'-', N'N', N'-', N'-', N'', N'', N'', 16777215, N'', N'Y', 2, 2130006, 2140006, 0, 90001, N'', CAST(0x0000A406011D2CEE AS DateTime), N'-')
INSERT [dbo].[catalog] ([catalog_id], [catalog_group_id], [catalog_name], [catalog_full_name], [manufacture_id], [model_code], [order_code], [size_w], [size_d], [size_h], [weight], [num_of_ports], [image_id], [icon_16_image_id], [icon_32_image_id], [icon_48_image_id], [icon_64_image_id], [link_80_image_id], [deletable], [pp_use_intelligent], [pp_config_type], [pp_media_type], [pp_utp_jack_type], [pp_utp_shield_type], [pp_figure_type], [ic_num_of_pp_connectors], [ic_num_of_max_pp], [ic_num_of_power], [st_num_of_disk], [sw_figure_type], [sw_num_of_slots], [cp_plug_side], [ca_use_intelligent], [ca_install_type], [ca_for_army], [ca_media_type], [ca_is_utp_shield], [ca_utp_cable_type], [ca_fiber_cable_type], [ca_fiber_connector_type], [ca_disp_color_rgb], [ca_disp_name], [rm_is_rack_mount], [rm_unit_size], [rm_image_220_image_id], [rm_image_440_image_id], [rm_image_880_image_id], [user_id], [remarks], [last_updated], [sw_model_type]) 
VALUES (423003, 3230, N'General 19" Blank Panel (3U)', N'', 10010001, N'', N'', 0, 0, 0, CAST(0 AS Numeric(18, 0)), 0, 0, 2110099, NULL, NULL, NULL, 2120099, N'N', N'N', N'-', N'-', N'N', N'N', N'-', 0, NULL, 1, 0, N'-', 0, N'-', N'N', N'-', N'N', N'-', N'-', N'', N'', N'', 16777215, N'', N'Y', 3, 2130007, 2140007, 0, 90001, N'', CAST(0x0000A406011D447A AS DateTime), N'-')
INSERT [dbo].[catalog] ([catalog_id], [catalog_group_id], [catalog_name], [catalog_full_name], [manufacture_id], [model_code], [order_code], [size_w], [size_d], [size_h], [weight], [num_of_ports], [image_id], [icon_16_image_id], [icon_32_image_id], [icon_48_image_id], [icon_64_image_id], [link_80_image_id], [deletable], [pp_use_intelligent], [pp_config_type], [pp_media_type], [pp_utp_jack_type], [pp_utp_shield_type], [pp_figure_type], [ic_num_of_pp_connectors], [ic_num_of_max_pp], [ic_num_of_power], [st_num_of_disk], [sw_figure_type], [sw_num_of_slots], [cp_plug_side], [ca_use_intelligent], [ca_install_type], [ca_for_army], [ca_media_type], [ca_is_utp_shield], [ca_utp_cable_type], [ca_fiber_cable_type], [ca_fiber_connector_type], [ca_disp_color_rgb], [ca_disp_name], [rm_is_rack_mount], [rm_unit_size], [rm_image_220_image_id], [rm_image_440_image_id], [rm_image_880_image_id], [user_id], [remarks], [last_updated], [sw_model_type]) 
VALUES (423004, 3230, N'General 19" Blank Panel (4U)', N'', 10010001, N'', N'', 0, 0, 0, CAST(0 AS Numeric(18, 0)), 0, 0, 2110099, NULL, NULL, NULL, 2120099, N'N', N'N', N'-', N'-', N'N', N'N', N'-', 0, NULL, 1, 0, N'-', 0, N'-', N'N', N'-', N'N', N'-', N'-', N'', N'', N'', 16777215, N'', N'Y', 4, 2130008, 2140008, 0, 90001, N'', CAST(0x0000A406011D525F AS DateTime), N'-')
INSERT [dbo].[catalog] ([catalog_id], [catalog_group_id], [catalog_name], [catalog_full_name], [manufacture_id], [model_code], [order_code], [size_w], [size_d], [size_h], [weight], [num_of_ports], [image_id], [icon_16_image_id], [icon_32_image_id], [icon_48_image_id], [icon_64_image_id], [link_80_image_id], [deletable], [pp_use_intelligent], [pp_config_type], [pp_media_type], [pp_utp_jack_type], [pp_utp_shield_type], [pp_figure_type], [ic_num_of_pp_connectors], [ic_num_of_max_pp], [ic_num_of_power], [st_num_of_disk], [sw_figure_type], [sw_num_of_slots], [cp_plug_side], [ca_use_intelligent], [ca_install_type], [ca_for_army], [ca_media_type], [ca_is_utp_shield], [ca_utp_cable_type], [ca_fiber_cable_type], [ca_fiber_connector_type], [ca_disp_color_rgb], [ca_disp_name], [rm_is_rack_mount], [rm_unit_size], [rm_image_220_image_id], [rm_image_440_image_id], [rm_image_880_image_id], [user_id], [remarks], [last_updated], [sw_model_type]) 
VALUES (423005, 3230, N'General 19" Cable Organizer (1U)', N'', 10010001, N'', N'', 0, 0, 0, CAST(0 AS Numeric(18, 0)), 0, 0, 2110099, NULL, NULL, NULL, 2120099, N'N', N'N', N'-', N'-', N'N', N'N', N'-', 0, NULL, 2, 0, N'-', 0, N'-', N'N', NULL, N'N', N'-', N'-', N'-', N'-', N'-', 16777215, N'', N'Y', 1, 29001067, 0, 0, 90001, N'', CAST(0x0000A39A009812B8 AS DateTime), NULL)
INSERT [dbo].[catalog] ([catalog_id], [catalog_group_id], [catalog_name], [catalog_full_name], [manufacture_id], [model_code], [order_code], [size_w], [size_d], [size_h], [weight], [num_of_ports], [image_id], [icon_16_image_id], [icon_32_image_id], [icon_48_image_id], [icon_64_image_id], [link_80_image_id], [deletable], [pp_use_intelligent], [pp_config_type], [pp_media_type], [pp_utp_jack_type], [pp_utp_shield_type], [pp_figure_type], [ic_num_of_pp_connectors], [ic_num_of_max_pp], [ic_num_of_power], [st_num_of_disk], [sw_figure_type], [sw_num_of_slots], [cp_plug_side], [ca_use_intelligent], [ca_install_type], [ca_for_army], [ca_media_type], [ca_is_utp_shield], [ca_utp_cable_type], [ca_fiber_cable_type], [ca_fiber_connector_type], [ca_disp_color_rgb], [ca_disp_name], [rm_is_rack_mount], [rm_unit_size], [rm_image_220_image_id], [rm_image_440_image_id], [rm_image_880_image_id], [user_id], [remarks], [last_updated], [sw_model_type]) 
VALUES (432001, 3320, N'General L2 Switch (24 ports)', N'', 10010001, N'', N'', 0, 0, 0, CAST(0 AS Numeric(18, 0)), 24, 0, 2110008, NULL, NULL, NULL, 2120005, N'N', N'N', N'-', N'-', N'N', N'N', N'-', 0, NULL, 2, 0, N'E', 0, N'-', N'N', N'-', N'N', N'-', N'-', N'', N'', N'', 16777215, N'', N'Y', 1, 2130042, 2140042, 0, 90001, N'', CAST(0x0000A406011D73B1 AS DateTime), N'C')
INSERT [dbo].[catalog] ([catalog_id], [catalog_group_id], [catalog_name], [catalog_full_name], [manufacture_id], [model_code], [order_code], [size_w], [size_d], [size_h], [weight], [num_of_ports], [image_id], [icon_16_image_id], [icon_32_image_id], [icon_48_image_id], [icon_64_image_id], [link_80_image_id], [deletable], [pp_use_intelligent], [pp_config_type], [pp_media_type], [pp_utp_jack_type], [pp_utp_shield_type], [pp_figure_type], [ic_num_of_pp_connectors], [ic_num_of_max_pp], [ic_num_of_power], [st_num_of_disk], [sw_figure_type], [sw_num_of_slots], [cp_plug_side], [ca_use_intelligent], [ca_install_type], [ca_for_army], [ca_media_type], [ca_is_utp_shield], [ca_utp_cable_type], [ca_fiber_cable_type], [ca_fiber_connector_type], [ca_disp_color_rgb], [ca_disp_name], [rm_is_rack_mount], [rm_unit_size], [rm_image_220_image_id], [rm_image_440_image_id], [rm_image_880_image_id], [user_id], [remarks], [last_updated], [sw_model_type]) 
VALUES (432002, 3320, N'General L2 Switch (24+2 ports)', N'', 10010001, N'', N'', 0, 0, 0, CAST(0 AS Numeric(18, 0)), 26, 0, 2110008, NULL, NULL, NULL, 2120005, N'N', N'N', N'-', N'-', N'N', N'N', N'-', 0, NULL, 1, 0, N'E', 0, N'-', N'N', N'F', N'N', N'-', N'-', N'', N'', N'', 16777215, N'', N'Y', 1, 2130021, 2140021, 0, 90001, N'', CAST(0x0000A406011D9306 AS DateTime), N'-')
INSERT [dbo].[catalog] ([catalog_id], [catalog_group_id], [catalog_name], [catalog_full_name], [manufacture_id], [model_code], [order_code], [size_w], [size_d], [size_h], [weight], [num_of_ports], [image_id], [icon_16_image_id], [icon_32_image_id], [icon_48_image_id], [icon_64_image_id], [link_80_image_id], [deletable], [pp_use_intelligent], [pp_config_type], [pp_media_type], [pp_utp_jack_type], [pp_utp_shield_type], [pp_figure_type], [ic_num_of_pp_connectors], [ic_num_of_max_pp], [ic_num_of_power], [st_num_of_disk], [sw_figure_type], [sw_num_of_slots], [cp_plug_side], [ca_use_intelligent], [ca_install_type], [ca_for_army], [ca_media_type], [ca_is_utp_shield], [ca_utp_cable_type], [ca_fiber_cable_type], [ca_fiber_connector_type], [ca_disp_color_rgb], [ca_disp_name], [rm_is_rack_mount], [rm_unit_size], [rm_image_220_image_id], [rm_image_440_image_id], [rm_image_880_image_id], [user_id], [remarks], [last_updated], [sw_model_type]) 
VALUES (432003, 3320, N'General L2 Switch (24+4 ports)', N'', 10010001, N'', N'', 0, 0, 0, CAST(0 AS Numeric(18, 0)), 28, 0, 2110008, NULL, NULL, NULL, 2120005, N'N', N'N', N'-', N'-', N'N', N'N', N'-', 0, NULL, 1, 0, N'E', 1, N'-', N'N', N'-', N'N', N'-', N'-', N'', N'', N'', 16777215, N'', N'Y', 1, 2130022, 2140022, 0, 90001, N'', CAST(0x0000A406011DAFF9 AS DateTime), N'D')
INSERT [dbo].[catalog] ([catalog_id], [catalog_group_id], [catalog_name], [catalog_full_name], [manufacture_id], [model_code], [order_code], [size_w], [size_d], [size_h], [weight], [num_of_ports], [image_id], [icon_16_image_id], [icon_32_image_id], [icon_48_image_id], [icon_64_image_id], [link_80_image_id], [deletable], [pp_use_intelligent], [pp_config_type], [pp_media_type], [pp_utp_jack_type], [pp_utp_shield_type], [pp_figure_type], [ic_num_of_pp_connectors], [ic_num_of_max_pp], [ic_num_of_power], [st_num_of_disk], [sw_figure_type], [sw_num_of_slots], [cp_plug_side], [ca_use_intelligent], [ca_install_type], [ca_for_army], [ca_media_type], [ca_is_utp_shield], [ca_utp_cable_type], [ca_fiber_cable_type], [ca_fiber_connector_type], [ca_disp_color_rgb], [ca_disp_name], [rm_is_rack_mount], [rm_unit_size], [rm_image_220_image_id], [rm_image_440_image_id], [rm_image_880_image_id], [user_id], [remarks], [last_updated], [sw_model_type]) 
VALUES (432011, 3320, N'General L2 Switch (48 ports)', N'', 10010001, N'', N'', 0, 0, 0, CAST(0 AS Numeric(18, 0)), 48, 0, 2110008, NULL, NULL, NULL, 2120005, N'N', N'N', N'-', N'-', N'N', N'N', N'-', 0, NULL, 1, 0, N'E', 0, N'-', N'N', N'F', N'N', N'-', N'-', N'', N'', N'', 16777215, N'', N'Y', 1, 2130024, 2140024, 0, 90001, N'', CAST(0x0000A406011DC8B4 AS DateTime), N'-')
INSERT [dbo].[catalog] ([catalog_id], [catalog_group_id], [catalog_name], [catalog_full_name], [manufacture_id], [model_code], [order_code], [size_w], [size_d], [size_h], [weight], [num_of_ports], [image_id], [icon_16_image_id], [icon_32_image_id], [icon_48_image_id], [icon_64_image_id], [link_80_image_id], [deletable], [pp_use_intelligent], [pp_config_type], [pp_media_type], [pp_utp_jack_type], [pp_utp_shield_type], [pp_figure_type], [ic_num_of_pp_connectors], [ic_num_of_max_pp], [ic_num_of_power], [st_num_of_disk], [sw_figure_type], [sw_num_of_slots], [cp_plug_side], [ca_use_intelligent], [ca_install_type], [ca_for_army], [ca_media_type], [ca_is_utp_shield], [ca_utp_cable_type], [ca_fiber_cable_type], [ca_fiber_connector_type], [ca_disp_color_rgb], [ca_disp_name], [rm_is_rack_mount], [rm_unit_size], [rm_image_220_image_id], [rm_image_440_image_id], [rm_image_880_image_id], [user_id], [remarks], [last_updated], [sw_model_type]) 
VALUES (432012, 3320, N'General L2 Switch (48+2 ports)', N'', 10010001, N'', N'', 0, 0, 0, CAST(0 AS Numeric(18, 0)), 50, 0, 2110008, NULL, NULL, NULL, 2120005, N'N', N'N', N'-', N'-', N'N', N'N', N'-', 0, NULL, 1, 0, N'E', 0, N'-', N'N', N'F', N'N', N'-', N'-', N'', N'', N'', 16777215, N'', N'Y', 1, 2130024, 2140024, 0, 90001, N'', CAST(0x0000A406011DC8B4 AS DateTime), N'-')
INSERT [dbo].[catalog] ([catalog_id], [catalog_group_id], [catalog_name], [catalog_full_name], [manufacture_id], [model_code], [order_code], [size_w], [size_d], [size_h], [weight], [num_of_ports], [image_id], [icon_16_image_id], [icon_32_image_id], [icon_48_image_id], [icon_64_image_id], [link_80_image_id], [deletable], [pp_use_intelligent], [pp_config_type], [pp_media_type], [pp_utp_jack_type], [pp_utp_shield_type], [pp_figure_type], [ic_num_of_pp_connectors], [ic_num_of_max_pp], [ic_num_of_power], [st_num_of_disk], [sw_figure_type], [sw_num_of_slots], [cp_plug_side], [ca_use_intelligent], [ca_install_type], [ca_for_army], [ca_media_type], [ca_is_utp_shield], [ca_utp_cable_type], [ca_fiber_cable_type], [ca_fiber_connector_type], [ca_disp_color_rgb], [ca_disp_name], [rm_is_rack_mount], [rm_unit_size], [rm_image_220_image_id], [rm_image_440_image_id], [rm_image_880_image_id], [user_id], [remarks], [last_updated], [sw_model_type]) 
VALUES (432013, 3320, N'General L2 Switch (48+4 ports)', N'', 10010001, N'', N'', 0, 0, 0, CAST(0 AS Numeric(18, 0)), 52, 0, 2110008, NULL, NULL, NULL, 2120005, N'N', N'N', N'-', N'-', N'N', N'N', N'-', 0, NULL, 1, 0, N'E', 0, N'-', N'N', N'F', N'N', N'-', N'-', N'', N'', N'', 16777215, N'', N'Y', 1, 2130025, 2140025, 0, 90001, N'', CAST(0x0000A406011DDF39 AS DateTime), N'-')
INSERT [dbo].[catalog] ([catalog_id], [catalog_group_id], [catalog_name], [catalog_full_name], [manufacture_id], [model_code], [order_code], [size_w], [size_d], [size_h], [weight], [num_of_ports], [image_id], [icon_16_image_id], [icon_32_image_id], [icon_48_image_id], [icon_64_image_id], [link_80_image_id], [deletable], [pp_use_intelligent], [pp_config_type], [pp_media_type], [pp_utp_jack_type], [pp_utp_shield_type], [pp_figure_type], [ic_num_of_pp_connectors], [ic_num_of_max_pp], [ic_num_of_power], [st_num_of_disk], [sw_figure_type], [sw_num_of_slots], [cp_plug_side], [ca_use_intelligent], [ca_install_type], [ca_for_army], [ca_media_type], [ca_is_utp_shield], [ca_utp_cable_type], [ca_fiber_cable_type], [ca_fiber_connector_type], [ca_disp_color_rgb], [ca_disp_name], [rm_is_rack_mount], [rm_unit_size], [rm_image_220_image_id], [rm_image_440_image_id], [rm_image_880_image_id], [user_id], [remarks], [last_updated], [sw_model_type]) 
VALUES (432021, 3320, N'General L2 Switch (4U, 4 slot)', N'', 10010001, N'', N'', 0, 0, 0, CAST(0 AS Numeric(18, 0)), 0, 0, 2110008, NULL, NULL, NULL, 2120005, N'N', N'N', N'-', N'-', N'N', N'N', N'-', 0, NULL, 1, 0, N'S', 4, N'-', N'N', NULL, N'N', N'-', N'-', N'-', N'-', N'-', 16777215, N'', N'Y', 4, 29001062, 29001058, 0, 90001, N'', CAST(0x0000A39A009812B8 AS DateTime), N'D')
INSERT [dbo].[catalog] ([catalog_id], [catalog_group_id], [catalog_name], [catalog_full_name], [manufacture_id], [model_code], [order_code], [size_w], [size_d], [size_h], [weight], [num_of_ports], [image_id], [icon_16_image_id], [icon_32_image_id], [icon_48_image_id], [icon_64_image_id], [link_80_image_id], [deletable], [pp_use_intelligent], [pp_config_type], [pp_media_type], [pp_utp_jack_type], [pp_utp_shield_type], [pp_figure_type], [ic_num_of_pp_connectors], [ic_num_of_max_pp], [ic_num_of_power], [st_num_of_disk], [sw_figure_type], [sw_num_of_slots], [cp_plug_side], [ca_use_intelligent], [ca_install_type], [ca_for_army], [ca_media_type], [ca_is_utp_shield], [ca_utp_cable_type], [ca_fiber_cable_type], [ca_fiber_connector_type], [ca_disp_color_rgb], [ca_disp_name], [rm_is_rack_mount], [rm_unit_size], [rm_image_220_image_id], [rm_image_440_image_id], [rm_image_880_image_id], [user_id], [remarks], [last_updated], [sw_model_type]) 
VALUES (432031, 3320, N'General L2 Switch Card (24 ports)', N'', 10010001, N'', N'', 0, 0, 0, CAST(0 AS Numeric(18, 0)), 24, 0, 2110008, NULL, NULL, NULL, 2120005, N'N', N'N', N'-', N'-', N'N', N'N', N'-', 0, NULL, 2, 0, N'C', 0, N'-', N'N', N'F', N'N', N'-', N'-', N'', N'', N'', 16777215, N'', N'N', 0, NULL, NULL, 0, 90001, N'', CAST(0x0000A3CB011697FE AS DateTime), N'D')
INSERT [dbo].[catalog] ([catalog_id], [catalog_group_id], [catalog_name], [catalog_full_name], [manufacture_id], [model_code], [order_code], [size_w], [size_d], [size_h], [weight], [num_of_ports], [image_id], [icon_16_image_id], [icon_32_image_id], [icon_48_image_id], [icon_64_image_id], [link_80_image_id], [deletable], [pp_use_intelligent], [pp_config_type], [pp_media_type], [pp_utp_jack_type], [pp_utp_shield_type], [pp_figure_type], [ic_num_of_pp_connectors], [ic_num_of_max_pp], [ic_num_of_power], [st_num_of_disk], [sw_figure_type], [sw_num_of_slots], [cp_plug_side], [ca_use_intelligent], [ca_install_type], [ca_for_army], [ca_media_type], [ca_is_utp_shield], [ca_utp_cable_type], [ca_fiber_cable_type], [ca_fiber_connector_type], [ca_disp_color_rgb], [ca_disp_name], [rm_is_rack_mount], [rm_unit_size], [rm_image_220_image_id], [rm_image_440_image_id], [rm_image_880_image_id], [user_id], [remarks], [last_updated], [sw_model_type]) 
VALUES (432032, 3320, N'General L2 Switch Card (48 ports)', N'', 10010001, N'', N'', 0, 0, 0, CAST(0 AS Numeric(18, 0)), 48, 0, 2110008, NULL, NULL, NULL, 2120005, N'N', N'N', N'-', N'-', N'N', N'N', N'-', 0, NULL, 2, 0, N'C', 0, N'-', N'N', N'F', N'N', N'-', N'-', N'-', N'-', N'-', 16777215, N'', N'N', 0, NULL, NULL, 0, 90001, N'', CAST(0x0000A39A009812B8 AS DateTime), N'D')
INSERT [dbo].[catalog] ([catalog_id], [catalog_group_id], [catalog_name], [catalog_full_name], [manufacture_id], [model_code], [order_code], [size_w], [size_d], [size_h], [weight], [num_of_ports], [image_id], [icon_16_image_id], [icon_32_image_id], [icon_48_image_id], [icon_64_image_id], [link_80_image_id], [deletable], [pp_use_intelligent], [pp_config_type], [pp_media_type], [pp_utp_jack_type], [pp_utp_shield_type], [pp_figure_type], [ic_num_of_pp_connectors], [ic_num_of_max_pp], [ic_num_of_power], [st_num_of_disk], [sw_figure_type], [sw_num_of_slots], [cp_plug_side], [ca_use_intelligent], [ca_install_type], [ca_for_army], [ca_media_type], [ca_is_utp_shield], [ca_utp_cable_type], [ca_fiber_cable_type], [ca_fiber_connector_type], [ca_disp_color_rgb], [ca_disp_name], [rm_is_rack_mount], [rm_unit_size], [rm_image_220_image_id], [rm_image_440_image_id], [rm_image_880_image_id], [user_id], [remarks], [last_updated], [sw_model_type]) 
VALUES (433001, 3330, N'General L3 Switch (24 ports)', N'', 10010001, N'', N'', 0, 0, 0, CAST(0 AS Numeric(18, 0)), 24, 0, 2110008, NULL, NULL, NULL, 2120006, N'N', N'N', N'-', N'-', N'N', N'N', N'-', 0, NULL, 1, 0, N'E', 0, N'-', N'N', N'-', N'N', N'-', N'-', N'', N'', N'', 16777215, N'', N'Y', 1, 2130043, 2140043, 0, 90001, N'', CAST(0x0000A406011DFF8E AS DateTime), N'D')
INSERT [dbo].[catalog] ([catalog_id], [catalog_group_id], [catalog_name], [catalog_full_name], [manufacture_id], [model_code], [order_code], [size_w], [size_d], [size_h], [weight], [num_of_ports], [image_id], [icon_16_image_id], [icon_32_image_id], [icon_48_image_id], [icon_64_image_id], [link_80_image_id], [deletable], [pp_use_intelligent], [pp_config_type], [pp_media_type], [pp_utp_jack_type], [pp_utp_shield_type], [pp_figure_type], [ic_num_of_pp_connectors], [ic_num_of_max_pp], [ic_num_of_power], [st_num_of_disk], [sw_figure_type], [sw_num_of_slots], [cp_plug_side], [ca_use_intelligent], [ca_install_type], [ca_for_army], [ca_media_type], [ca_is_utp_shield], [ca_utp_cable_type], [ca_fiber_cable_type], [ca_fiber_connector_type], [ca_disp_color_rgb], [ca_disp_name], [rm_is_rack_mount], [rm_unit_size], [rm_image_220_image_id], [rm_image_440_image_id], [rm_image_880_image_id], [user_id], [remarks], [last_updated], [sw_model_type]) 
VALUES (433002, 3330, N'General L3 Switch (24+2 ports)', N'', 10010001, N'', N'', 0, 0, 0, CAST(0 AS Numeric(18, 0)), 26, 0, 2110008, NULL, NULL, NULL, 2120006, N'N', N'N', N'-', N'-', N'N', N'N', N'-', 0, NULL, 1, 0, N'E', 0, N'-', N'N', N'-', N'N', N'-', N'-', N'', N'', N'', 16777215, N'', N'Y', 1, 2130026, 2140026, 0, 90001, N'', CAST(0x0000A406011DFF8E AS DateTime), N'D')
INSERT [dbo].[catalog] ([catalog_id], [catalog_group_id], [catalog_name], [catalog_full_name], [manufacture_id], [model_code], [order_code], [size_w], [size_d], [size_h], [weight], [num_of_ports], [image_id], [icon_16_image_id], [icon_32_image_id], [icon_48_image_id], [icon_64_image_id], [link_80_image_id], [deletable], [pp_use_intelligent], [pp_config_type], [pp_media_type], [pp_utp_jack_type], [pp_utp_shield_type], [pp_figure_type], [ic_num_of_pp_connectors], [ic_num_of_max_pp], [ic_num_of_power], [st_num_of_disk], [sw_figure_type], [sw_num_of_slots], [cp_plug_side], [ca_use_intelligent], [ca_install_type], [ca_for_army], [ca_media_type], [ca_is_utp_shield], [ca_utp_cable_type], [ca_fiber_cable_type], [ca_fiber_connector_type], [ca_disp_color_rgb], [ca_disp_name], [rm_is_rack_mount], [rm_unit_size], [rm_image_220_image_id], [rm_image_440_image_id], [rm_image_880_image_id], [user_id], [remarks], [last_updated], [sw_model_type]) 
VALUES (433003, 3330, N'General L3 Switch (24+4 ports)', N'', 10010001, N'', N'', 0, 0, 0, CAST(0 AS Numeric(18, 0)), 28, 0, 2110008, NULL, NULL, NULL, 2120006, N'N', N'N', N'-', N'-', N'N', N'N', N'-', 0, NULL, 1, 0, N'E', 0, N'-', N'N', N'-', N'N', N'-', N'-', N'', N'', N'', 16777215, N'', N'Y', 1, 2130027, 2140027, 0, 90001, N'', CAST(0x0000A406011DFF8E AS DateTime), N'D')
INSERT [dbo].[catalog] ([catalog_id], [catalog_group_id], [catalog_name], [catalog_full_name], [manufacture_id], [model_code], [order_code], [size_w], [size_d], [size_h], [weight], [num_of_ports], [image_id], [icon_16_image_id], [icon_32_image_id], [icon_48_image_id], [icon_64_image_id], [link_80_image_id], [deletable], [pp_use_intelligent], [pp_config_type], [pp_media_type], [pp_utp_jack_type], [pp_utp_shield_type], [pp_figure_type], [ic_num_of_pp_connectors], [ic_num_of_max_pp], [ic_num_of_power], [st_num_of_disk], [sw_figure_type], [sw_num_of_slots], [cp_plug_side], [ca_use_intelligent], [ca_install_type], [ca_for_army], [ca_media_type], [ca_is_utp_shield], [ca_utp_cable_type], [ca_fiber_cable_type], [ca_fiber_connector_type], [ca_disp_color_rgb], [ca_disp_name], [rm_is_rack_mount], [rm_unit_size], [rm_image_220_image_id], [rm_image_440_image_id], [rm_image_880_image_id], [user_id], [remarks], [last_updated], [sw_model_type]) 
VALUES (434001, 3340, N'Virtual HUB', NULL, 10010001, NULL, NULL, NULL, NULL, NULL, NULL, 1, NULL, 2110019, NULL, NULL, NULL, 2120098, N'N', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 90001, NULL, CAST(0x0000A39A009812B8 AS DateTime), NULL)
INSERT [dbo].[catalog] ([catalog_id], [catalog_group_id], [catalog_name], [catalog_full_name], [manufacture_id], [model_code], [order_code], [size_w], [size_d], [size_h], [weight], [num_of_ports], [image_id], [icon_16_image_id], [icon_32_image_id], [icon_48_image_id], [icon_64_image_id], [link_80_image_id], [deletable], [pp_use_intelligent], [pp_config_type], [pp_media_type], [pp_utp_jack_type], [pp_utp_shield_type], [pp_figure_type], [ic_num_of_pp_connectors], [ic_num_of_max_pp], [ic_num_of_power], [st_num_of_disk], [sw_figure_type], [sw_num_of_slots], [cp_plug_side], [ca_use_intelligent], [ca_install_type], [ca_for_army], [ca_media_type], [ca_is_utp_shield], [ca_utp_cable_type], [ca_fiber_cable_type], [ca_fiber_connector_type], [ca_disp_color_rgb], [ca_disp_name], [rm_is_rack_mount], [rm_unit_size], [rm_image_220_image_id], [rm_image_440_image_id], [rm_image_880_image_id], [user_id], [remarks], [last_updated], [sw_model_type]) 
VALUES (434002, 3340, N'General PC', NULL, 10010001, NULL, NULL, NULL, NULL, NULL, NULL, 1, NULL, 2110010, NULL, NULL, NULL, 2120007, N'N', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 90001, NULL, CAST(0x0000A39A009812B8 AS DateTime), NULL)
INSERT [dbo].[catalog] ([catalog_id], [catalog_group_id], [catalog_name], [catalog_full_name], [manufacture_id], [model_code], [order_code], [size_w], [size_d], [size_h], [weight], [num_of_ports], [image_id], [icon_16_image_id], [icon_32_image_id], [icon_48_image_id], [icon_64_image_id], [link_80_image_id], [deletable], [pp_use_intelligent], [pp_config_type], [pp_media_type], [pp_utp_jack_type], [pp_utp_shield_type], [pp_figure_type], [ic_num_of_pp_connectors], [ic_num_of_max_pp], [ic_num_of_power], [st_num_of_disk], [sw_figure_type], [sw_num_of_slots], [cp_plug_side], [ca_use_intelligent], [ca_install_type], [ca_for_army], [ca_media_type], [ca_is_utp_shield], [ca_utp_cable_type], [ca_fiber_cable_type], [ca_fiber_connector_type], [ca_disp_color_rgb], [ca_disp_name], [rm_is_rack_mount], [rm_unit_size], [rm_image_220_image_id], [rm_image_440_image_id], [rm_image_880_image_id], [user_id], [remarks], [last_updated], [sw_model_type]) 
VALUES (434003, 3340, N'General Notebook', NULL, 10010001, NULL, NULL, NULL, NULL, NULL, NULL, 1, NULL, 2110011, NULL, NULL, NULL, 2120008, N'N', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 90001, NULL, CAST(0x0000A39A009812B8 AS DateTime), NULL)
INSERT [dbo].[catalog] ([catalog_id], [catalog_group_id], [catalog_name], [catalog_full_name], [manufacture_id], [model_code], [order_code], [size_w], [size_d], [size_h], [weight], [num_of_ports], [image_id], [icon_16_image_id], [icon_32_image_id], [icon_48_image_id], [icon_64_image_id], [link_80_image_id], [deletable], [pp_use_intelligent], [pp_config_type], [pp_media_type], [pp_utp_jack_type], [pp_utp_shield_type], [pp_figure_type], [ic_num_of_pp_connectors], [ic_num_of_max_pp], [ic_num_of_power], [st_num_of_disk], [sw_figure_type], [sw_num_of_slots], [cp_plug_side], [ca_use_intelligent], [ca_install_type], [ca_for_army], [ca_media_type], [ca_is_utp_shield], [ca_utp_cable_type], [ca_fiber_cable_type], [ca_fiber_connector_type], [ca_disp_color_rgb], [ca_disp_name], [rm_is_rack_mount], [rm_unit_size], [rm_image_220_image_id], [rm_image_440_image_id], [rm_image_880_image_id], [user_id], [remarks], [last_updated], [sw_model_type]) 
VALUES (441001, 3420, N'General Face Plate (1 utp)', NULL, 10010001, NULL, NULL, NULL, NULL, NULL, NULL, 1, NULL, 2110016, NULL, NULL, NULL, 2120015, N'N', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 90001, NULL, CAST(0x0000A39A009812B8 AS DateTime), NULL)
INSERT [dbo].[catalog] ([catalog_id], [catalog_group_id], [catalog_name], [catalog_full_name], [manufacture_id], [model_code], [order_code], [size_w], [size_d], [size_h], [weight], [num_of_ports], [image_id], [icon_16_image_id], [icon_32_image_id], [icon_48_image_id], [icon_64_image_id], [link_80_image_id], [deletable], [pp_use_intelligent], [pp_config_type], [pp_media_type], [pp_utp_jack_type], [pp_utp_shield_type], [pp_figure_type], [ic_num_of_pp_connectors], [ic_num_of_max_pp], [ic_num_of_power], [st_num_of_disk], [sw_figure_type], [sw_num_of_slots], [cp_plug_side], [ca_use_intelligent], [ca_install_type], [ca_for_army], [ca_media_type], [ca_is_utp_shield], [ca_utp_cable_type], [ca_fiber_cable_type], [ca_fiber_connector_type], [ca_disp_color_rgb], [ca_disp_name], [rm_is_rack_mount], [rm_unit_size], [rm_image_220_image_id], [rm_image_440_image_id], [rm_image_880_image_id], [user_id], [remarks], [last_updated], [sw_model_type]) 
VALUES (441002, 3420, N'General Face Plate (2 utp)', NULL, 10010001, NULL, NULL, NULL, NULL, NULL, NULL, 2, NULL, 2110016, NULL, NULL, NULL, 2120015, N'N', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 90001, NULL, CAST(0x0000A39A009812B8 AS DateTime), NULL)
INSERT [dbo].[catalog] ([catalog_id], [catalog_group_id], [catalog_name], [catalog_full_name], [manufacture_id], [model_code], [order_code], [size_w], [size_d], [size_h], [weight], [num_of_ports], [image_id], [icon_16_image_id], [icon_32_image_id], [icon_48_image_id], [icon_64_image_id], [link_80_image_id], [deletable], [pp_use_intelligent], [pp_config_type], [pp_media_type], [pp_utp_jack_type], [pp_utp_shield_type], [pp_figure_type], [ic_num_of_pp_connectors], [ic_num_of_max_pp], [ic_num_of_power], [st_num_of_disk], [sw_figure_type], [sw_num_of_slots], [cp_plug_side], [ca_use_intelligent], [ca_install_type], [ca_for_army], [ca_media_type], [ca_is_utp_shield], [ca_utp_cable_type], [ca_fiber_cable_type], [ca_fiber_connector_type], [ca_disp_color_rgb], [ca_disp_name], [rm_is_rack_mount], [rm_unit_size], [rm_image_220_image_id], [rm_image_440_image_id], [rm_image_880_image_id], [user_id], [remarks], [last_updated], [sw_model_type]) 
VALUES (441003, 3420, N'General Face Plate (3 utp)', NULL, 10010001, NULL, NULL, NULL, NULL, NULL, NULL, 3, NULL, 2110016, NULL, NULL, NULL, 2120015, N'N', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 90001, NULL, CAST(0x0000A39A009812B8 AS DateTime), NULL)
INSERT [dbo].[catalog] ([catalog_id], [catalog_group_id], [catalog_name], [catalog_full_name], [manufacture_id], [model_code], [order_code], [size_w], [size_d], [size_h], [weight], [num_of_ports], [image_id], [icon_16_image_id], [icon_32_image_id], [icon_48_image_id], [icon_64_image_id], [link_80_image_id], [deletable], [pp_use_intelligent], [pp_config_type], [pp_media_type], [pp_utp_jack_type], [pp_utp_shield_type], [pp_figure_type], [ic_num_of_pp_connectors], [ic_num_of_max_pp], [ic_num_of_power], [st_num_of_disk], [sw_figure_type], [sw_num_of_slots], [cp_plug_side], [ca_use_intelligent], [ca_install_type], [ca_for_army], [ca_media_type], [ca_is_utp_shield], [ca_utp_cable_type], [ca_fiber_cable_type], [ca_fiber_connector_type], [ca_disp_color_rgb], [ca_disp_name], [rm_is_rack_mount], [rm_unit_size], [rm_image_220_image_id], [rm_image_440_image_id], [rm_image_880_image_id], [user_id], [remarks], [last_updated], [sw_model_type]) 
VALUES (441004, 3420, N'General Face Plate (4 utp)', NULL, 10010001, NULL, NULL, NULL, NULL, NULL, NULL, 4, NULL, 2110016, NULL, NULL, NULL, 2120015, N'N', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 90001, NULL, CAST(0x0000A39A009812B8 AS DateTime), NULL)
INSERT [dbo].[catalog] ([catalog_id], [catalog_group_id], [catalog_name], [catalog_full_name], [manufacture_id], [model_code], [order_code], [size_w], [size_d], [size_h], [weight], [num_of_ports], [image_id], [icon_16_image_id], [icon_32_image_id], [icon_48_image_id], [icon_64_image_id], [link_80_image_id], [deletable], [pp_use_intelligent], [pp_config_type], [pp_media_type], [pp_utp_jack_type], [pp_utp_shield_type], [pp_figure_type], [ic_num_of_pp_connectors], [ic_num_of_max_pp], [ic_num_of_power], [st_num_of_disk], [sw_figure_type], [sw_num_of_slots], [cp_plug_side], [ca_use_intelligent], [ca_install_type], [ca_for_army], [ca_media_type], [ca_is_utp_shield], [ca_utp_cable_type], [ca_fiber_cable_type], [ca_fiber_connector_type], [ca_disp_color_rgb], [ca_disp_name], [rm_is_rack_mount], [rm_unit_size], [rm_image_220_image_id], [rm_image_440_image_id], [rm_image_880_image_id], [user_id], [remarks], [last_updated], [sw_model_type]) 
VALUES (441005, 3420, N'General Face Plate (6 utp)', NULL, 10010001, NULL, NULL, NULL, NULL, NULL, NULL, 6, NULL, 2110016, NULL, NULL, NULL, 2120015, N'N', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 90001, NULL, CAST(0x0000A39A009812B8 AS DateTime), NULL)
INSERT [dbo].[catalog] ([catalog_id], [catalog_group_id], [catalog_name], [catalog_full_name], [manufacture_id], [model_code], [order_code], [size_w], [size_d], [size_h], [weight], [num_of_ports], [image_id], [icon_16_image_id], [icon_32_image_id], [icon_48_image_id], [icon_64_image_id], [link_80_image_id], [deletable], [pp_use_intelligent], [pp_config_type], [pp_media_type], [pp_utp_jack_type], [pp_utp_shield_type], [pp_figure_type], [ic_num_of_pp_connectors], [ic_num_of_max_pp], [ic_num_of_power], [st_num_of_disk], [sw_figure_type], [sw_num_of_slots], [cp_plug_side], [ca_use_intelligent], [ca_install_type], [ca_for_army], [ca_media_type], [ca_is_utp_shield], [ca_utp_cable_type], [ca_fiber_cable_type], [ca_fiber_connector_type], [ca_disp_color_rgb], [ca_disp_name], [rm_is_rack_mount], [rm_unit_size], [rm_image_220_image_id], [rm_image_440_image_id], [rm_image_880_image_id], [user_id], [remarks], [last_updated], [sw_model_type]) 
VALUES (441006, 3420, N'General Face Plate (3 utp)', NULL, 10010001, NULL, NULL, NULL, NULL, NULL, NULL, 1, NULL, 2110016, NULL, NULL, NULL, 2120015, N'N', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 90001, NULL, CAST(0x0000A39A009812B8 AS DateTime), NULL)
INSERT [dbo].[catalog] ([catalog_id], [catalog_group_id], [catalog_name], [catalog_full_name], [manufacture_id], [model_code], [order_code], [size_w], [size_d], [size_h], [weight], [num_of_ports], [image_id], [icon_16_image_id], [icon_32_image_id], [icon_48_image_id], [icon_64_image_id], [link_80_image_id], [deletable], [pp_use_intelligent], [pp_config_type], [pp_media_type], [pp_utp_jack_type], [pp_utp_shield_type], [pp_figure_type], [ic_num_of_pp_connectors], [ic_num_of_max_pp], [ic_num_of_power], [st_num_of_disk], [sw_figure_type], [sw_num_of_slots], [cp_plug_side], [ca_use_intelligent], [ca_install_type], [ca_for_army], [ca_media_type], [ca_is_utp_shield], [ca_utp_cable_type], [ca_fiber_cable_type], [ca_fiber_connector_type], [ca_disp_color_rgb], [ca_disp_name], [rm_is_rack_mount], [rm_unit_size], [rm_image_220_image_id], [rm_image_440_image_id], [rm_image_880_image_id], [user_id], [remarks], [last_updated], [sw_model_type]) 
VALUES (441007, 3420, N'General Face Plate (3 utp)', NULL, 10010001, NULL, NULL, NULL, NULL, NULL, NULL, 2, NULL, 2110016, NULL, NULL, NULL, 2120015, N'N', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 90001, NULL, CAST(0x0000A39A009812B8 AS DateTime), NULL)
INSERT [dbo].[catalog] ([catalog_id], [catalog_group_id], [catalog_name], [catalog_full_name], [manufacture_id], [model_code], [order_code], [size_w], [size_d], [size_h], [weight], [num_of_ports], [image_id], [icon_16_image_id], [icon_32_image_id], [icon_48_image_id], [icon_64_image_id], [link_80_image_id], [deletable], [pp_use_intelligent], [pp_config_type], [pp_media_type], [pp_utp_jack_type], [pp_utp_shield_type], [pp_figure_type], [ic_num_of_pp_connectors], [ic_num_of_max_pp], [ic_num_of_power], [st_num_of_disk], [sw_figure_type], [sw_num_of_slots], [cp_plug_side], [ca_use_intelligent], [ca_install_type], [ca_for_army], [ca_media_type], [ca_is_utp_shield], [ca_utp_cable_type], [ca_fiber_cable_type], [ca_fiber_connector_type], [ca_disp_color_rgb], [ca_disp_name], [rm_is_rack_mount], [rm_unit_size], [rm_image_220_image_id], [rm_image_440_image_id], [rm_image_880_image_id], [user_id], [remarks], [last_updated], [sw_model_type]) 
VALUES (442001, 3430, N'General MUTOA Box (1 utp)', NULL, 10010001, NULL, NULL, NULL, NULL, NULL, NULL, 1, NULL, 2110017, NULL, NULL, NULL, 2120016, N'N', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 90001, NULL, CAST(0x0000A39A009812B8 AS DateTime), NULL)
INSERT [dbo].[catalog] ([catalog_id], [catalog_group_id], [catalog_name], [catalog_full_name], [manufacture_id], [model_code], [order_code], [size_w], [size_d], [size_h], [weight], [num_of_ports], [image_id], [icon_16_image_id], [icon_32_image_id], [icon_48_image_id], [icon_64_image_id], [link_80_image_id], [deletable], [pp_use_intelligent], [pp_config_type], [pp_media_type], [pp_utp_jack_type], [pp_utp_shield_type], [pp_figure_type], [ic_num_of_pp_connectors], [ic_num_of_max_pp], [ic_num_of_power], [st_num_of_disk], [sw_figure_type], [sw_num_of_slots], [cp_plug_side], [ca_use_intelligent], [ca_install_type], [ca_for_army], [ca_media_type], [ca_is_utp_shield], [ca_utp_cable_type], [ca_fiber_cable_type], [ca_fiber_connector_type], [ca_disp_color_rgb], [ca_disp_name], [rm_is_rack_mount], [rm_unit_size], [rm_image_220_image_id], [rm_image_440_image_id], [rm_image_880_image_id], [user_id], [remarks], [last_updated], [sw_model_type]) 
VALUES (442002, 3430, N'General MUTOA Box (2 utp)', NULL, 10010001, NULL, NULL, NULL, NULL, NULL, NULL, 2, NULL, 2110017, NULL, NULL, NULL, 2120016, N'N', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 90001, NULL, CAST(0x0000A39A009812B8 AS DateTime), NULL)
INSERT [dbo].[catalog] ([catalog_id], [catalog_group_id], [catalog_name], [catalog_full_name], [manufacture_id], [model_code], [order_code], [size_w], [size_d], [size_h], [weight], [num_of_ports], [image_id], [icon_16_image_id], [icon_32_image_id], [icon_48_image_id], [icon_64_image_id], [link_80_image_id], [deletable], [pp_use_intelligent], [pp_config_type], [pp_media_type], [pp_utp_jack_type], [pp_utp_shield_type], [pp_figure_type], [ic_num_of_pp_connectors], [ic_num_of_max_pp], [ic_num_of_power], [st_num_of_disk], [sw_figure_type], [sw_num_of_slots], [cp_plug_side], [ca_use_intelligent], [ca_install_type], [ca_for_army], [ca_media_type], [ca_is_utp_shield], [ca_utp_cable_type], [ca_fiber_cable_type], [ca_fiber_connector_type], [ca_disp_color_rgb], [ca_disp_name], [rm_is_rack_mount], [rm_unit_size], [rm_image_220_image_id], [rm_image_440_image_id], [rm_image_880_image_id], [user_id], [remarks], [last_updated], [sw_model_type]) 
VALUES (442003, 3430, N'General MUTOA Box (4 utp)', NULL, 10010001, NULL, NULL, NULL, NULL, NULL, NULL, 4, NULL, 2110017, NULL, NULL, NULL, 2120016, N'N', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 90001, NULL, CAST(0x0000A39A009812B8 AS DateTime), NULL)
INSERT [dbo].[catalog] ([catalog_id], [catalog_group_id], [catalog_name], [catalog_full_name], [manufacture_id], [model_code], [order_code], [size_w], [size_d], [size_h], [weight], [num_of_ports], [image_id], [icon_16_image_id], [icon_32_image_id], [icon_48_image_id], [icon_64_image_id], [link_80_image_id], [deletable], [pp_use_intelligent], [pp_config_type], [pp_media_type], [pp_utp_jack_type], [pp_utp_shield_type], [pp_figure_type], [ic_num_of_pp_connectors], [ic_num_of_max_pp], [ic_num_of_power], [st_num_of_disk], [sw_figure_type], [sw_num_of_slots], [cp_plug_side], [ca_use_intelligent], [ca_install_type], [ca_for_army], [ca_media_type], [ca_is_utp_shield], [ca_utp_cable_type], [ca_fiber_cable_type], [ca_fiber_connector_type], [ca_disp_color_rgb], [ca_disp_name], [rm_is_rack_mount], [rm_unit_size], [rm_image_220_image_id], [rm_image_440_image_id], [rm_image_880_image_id], [user_id], [remarks], [last_updated], [sw_model_type]) 
VALUES (442004, 3430, N'General MUTOA Box (6 utp)', NULL, 10010001, NULL, NULL, NULL, NULL, NULL, NULL, 6, NULL, 2110017, NULL, NULL, NULL, 2120016, N'N', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 90001, NULL, CAST(0x0000A39A009812B8 AS DateTime), NULL)
INSERT [dbo].[catalog] ([catalog_id], [catalog_group_id], [catalog_name], [catalog_full_name], [manufacture_id], [model_code], [order_code], [size_w], [size_d], [size_h], [weight], [num_of_ports], [image_id], [icon_16_image_id], [icon_32_image_id], [icon_48_image_id], [icon_64_image_id], [link_80_image_id], [deletable], [pp_use_intelligent], [pp_config_type], [pp_media_type], [pp_utp_jack_type], [pp_utp_shield_type], [pp_figure_type], [ic_num_of_pp_connectors], [ic_num_of_max_pp], [ic_num_of_power], [st_num_of_disk], [sw_figure_type], [sw_num_of_slots], [cp_plug_side], [ca_use_intelligent], [ca_install_type], [ca_for_army], [ca_media_type], [ca_is_utp_shield], [ca_utp_cable_type], [ca_fiber_cable_type], [ca_fiber_connector_type], [ca_disp_color_rgb], [ca_disp_name], [rm_is_rack_mount], [rm_unit_size], [rm_image_220_image_id], [rm_image_440_image_id], [rm_image_880_image_id], [user_id], [remarks], [last_updated], [sw_model_type]) 
VALUES (442005, 3430, N'General MUTOA Box (8 utp)', NULL, 10010001, NULL, NULL, NULL, NULL, NULL, NULL, 8, NULL, 2110017, NULL, NULL, NULL, 2120016, N'N', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 90001, NULL, CAST(0x0000A39A009812B8 AS DateTime), NULL)
INSERT [dbo].[catalog] ([catalog_id], [catalog_group_id], [catalog_name], [catalog_full_name], [manufacture_id], [model_code], [order_code], [size_w], [size_d], [size_h], [weight], [num_of_ports], [image_id], [icon_16_image_id], [icon_32_image_id], [icon_48_image_id], [icon_64_image_id], [link_80_image_id], [deletable], [pp_use_intelligent], [pp_config_type], [pp_media_type], [pp_utp_jack_type], [pp_utp_shield_type], [pp_figure_type], [ic_num_of_pp_connectors], [ic_num_of_max_pp], [ic_num_of_power], [st_num_of_disk], [sw_figure_type], [sw_num_of_slots], [cp_plug_side], [ca_use_intelligent], [ca_install_type], [ca_for_army], [ca_media_type], [ca_is_utp_shield], [ca_utp_cable_type], [ca_fiber_cable_type], [ca_fiber_connector_type], [ca_disp_color_rgb], [ca_disp_name], [rm_is_rack_mount], [rm_unit_size], [rm_image_220_image_id], [rm_image_440_image_id], [rm_image_880_image_id], [user_id], [remarks], [last_updated], [sw_model_type]) 
VALUES (442006, 3430, N'General MUTOA Box (10 utp)', NULL, 10010001, NULL, NULL, NULL, NULL, NULL, NULL, 10, NULL, 2110017, NULL, NULL, NULL, 2120016, N'N', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 90001, NULL, CAST(0x0000A39A009812B8 AS DateTime), NULL)
INSERT [dbo].[catalog] ([catalog_id], [catalog_group_id], [catalog_name], [catalog_full_name], [manufacture_id], [model_code], [order_code], [size_w], [size_d], [size_h], [weight], [num_of_ports], [image_id], [icon_16_image_id], [icon_32_image_id], [icon_48_image_id], [icon_64_image_id], [link_80_image_id], [deletable], [pp_use_intelligent], [pp_config_type], [pp_media_type], [pp_utp_jack_type], [pp_utp_shield_type], [pp_figure_type], [ic_num_of_pp_connectors], [ic_num_of_max_pp], [ic_num_of_power], [st_num_of_disk], [sw_figure_type], [sw_num_of_slots], [cp_plug_side], [ca_use_intelligent], [ca_install_type], [ca_for_army], [ca_media_type], [ca_is_utp_shield], [ca_utp_cable_type], [ca_fiber_cable_type], [ca_fiber_connector_type], [ca_disp_color_rgb], [ca_disp_name], [rm_is_rack_mount], [rm_unit_size], [rm_image_220_image_id], [rm_image_440_image_id], [rm_image_880_image_id], [user_id], [remarks], [last_updated], [sw_model_type]) 
VALUES (442007, 3430, N'General MUTOA Box (12 utp)', NULL, 10010001, NULL, NULL, NULL, NULL, NULL, NULL, 12, NULL, 2110017, NULL, NULL, NULL, 2120016, N'N', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 90001, NULL, CAST(0x0000A39A009812B8 AS DateTime), NULL)
INSERT [dbo].[catalog] ([catalog_id], [catalog_group_id], [catalog_name], [catalog_full_name], [manufacture_id], [model_code], [order_code], [size_w], [size_d], [size_h], [weight], [num_of_ports], [image_id], [icon_16_image_id], [icon_32_image_id], [icon_48_image_id], [icon_64_image_id], [link_80_image_id], [deletable], [pp_use_intelligent], [pp_config_type], [pp_media_type], [pp_utp_jack_type], [pp_utp_shield_type], [pp_figure_type], [ic_num_of_pp_connectors], [ic_num_of_max_pp], [ic_num_of_power], [st_num_of_disk], [sw_figure_type], [sw_num_of_slots], [cp_plug_side], [ca_use_intelligent], [ca_install_type], [ca_for_army], [ca_media_type], [ca_is_utp_shield], [ca_utp_cable_type], [ca_fiber_cable_type], [ca_fiber_connector_type], [ca_disp_color_rgb], [ca_disp_name], [rm_is_rack_mount], [rm_unit_size], [rm_image_220_image_id], [rm_image_440_image_id], [rm_image_880_image_id], [user_id], [remarks], [last_updated], [sw_model_type]) 
VALUES (442008, 3430, N'General MUTOA Box (16 utp)', NULL, 10010001, NULL, NULL, NULL, NULL, NULL, NULL, 16, NULL, 2110017, NULL, NULL, NULL, 2120016, N'N', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 90001, NULL, CAST(0x0000A39A009812B8 AS DateTime), NULL)
INSERT [dbo].[catalog] ([catalog_id], [catalog_group_id], [catalog_name], [catalog_full_name], [manufacture_id], [model_code], [order_code], [size_w], [size_d], [size_h], [weight], [num_of_ports], [image_id], [icon_16_image_id], [icon_32_image_id], [icon_48_image_id], [icon_64_image_id], [link_80_image_id], [deletable], [pp_use_intelligent], [pp_config_type], [pp_media_type], [pp_utp_jack_type], [pp_utp_shield_type], [pp_figure_type], [ic_num_of_pp_connectors], [ic_num_of_max_pp], [ic_num_of_power], [st_num_of_disk], [sw_figure_type], [sw_num_of_slots], [cp_plug_side], [ca_use_intelligent], [ca_install_type], [ca_for_army], [ca_media_type], [ca_is_utp_shield], [ca_utp_cable_type], [ca_fiber_cable_type], [ca_fiber_connector_type], [ca_disp_color_rgb], [ca_disp_name], [rm_is_rack_mount], [rm_unit_size], [rm_image_220_image_id], [rm_image_440_image_id], [rm_image_880_image_id], [user_id], [remarks], [last_updated], [sw_model_type]) 
VALUES (442009, 3430, N'General MUTOA Box (18 utp)', NULL, 10010001, NULL, NULL, NULL, NULL, NULL, NULL, 18, NULL, 2110017, NULL, NULL, NULL, 2120016, N'N', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 90001, NULL, CAST(0x0000A39A009812B8 AS DateTime), NULL)
INSERT [dbo].[catalog] ([catalog_id], [catalog_group_id], [catalog_name], [catalog_full_name], [manufacture_id], [model_code], [order_code], [size_w], [size_d], [size_h], [weight], [num_of_ports], [image_id], [icon_16_image_id], [icon_32_image_id], [icon_48_image_id], [icon_64_image_id], [link_80_image_id], [deletable], [pp_use_intelligent], [pp_config_type], [pp_media_type], [pp_utp_jack_type], [pp_utp_shield_type], [pp_figure_type], [ic_num_of_pp_connectors], [ic_num_of_max_pp], [ic_num_of_power], [st_num_of_disk], [sw_figure_type], [sw_num_of_slots], [cp_plug_side], [ca_use_intelligent], [ca_install_type], [ca_for_army], [ca_media_type], [ca_is_utp_shield], [ca_utp_cable_type], [ca_fiber_cable_type], [ca_fiber_connector_type], [ca_disp_color_rgb], [ca_disp_name], [rm_is_rack_mount], [rm_unit_size], [rm_image_220_image_id], [rm_image_440_image_id], [rm_image_880_image_id], [user_id], [remarks], [last_updated], [sw_model_type]) 
VALUES (443001, 3410, N'General Consolidation Point (None, 24 ports)', NULL, 10010001, NULL, NULL, NULL, NULL, NULL, NULL, 24, NULL, 2110015, NULL, NULL, NULL, 2120014, N'N', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 90001, NULL, CAST(0x0000A39A009812B8 AS DateTime), NULL)
INSERT [dbo].[catalog] ([catalog_id], [catalog_group_id], [catalog_name], [catalog_full_name], [manufacture_id], [model_code], [order_code], [size_w], [size_d], [size_h], [weight], [num_of_ports], [image_id], [icon_16_image_id], [icon_32_image_id], [icon_48_image_id], [icon_64_image_id], [link_80_image_id], [deletable], [pp_use_intelligent], [pp_config_type], [pp_media_type], [pp_utp_jack_type], [pp_utp_shield_type], [pp_figure_type], [ic_num_of_pp_connectors], [ic_num_of_max_pp], [ic_num_of_power], [st_num_of_disk], [sw_figure_type], [sw_num_of_slots], [cp_plug_side], [ca_use_intelligent], [ca_install_type], [ca_for_army], [ca_media_type], [ca_is_utp_shield], [ca_utp_cable_type], [ca_fiber_cable_type], [ca_fiber_connector_type], [ca_disp_color_rgb], [ca_disp_name], [rm_is_rack_mount], [rm_unit_size], [rm_image_220_image_id], [rm_image_440_image_id], [rm_image_880_image_id], [user_id], [remarks], [last_updated], [sw_model_type]) 
VALUES (443002, 3410, N'General Consolidation Point (Right, 24 ports)', NULL, 10010001, NULL, NULL, NULL, NULL, NULL, NULL, 24, NULL, 2110015, NULL, NULL, NULL, 2120014, N'N', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 90001, NULL, CAST(0x0000A39A009812B8 AS DateTime), NULL)
INSERT [dbo].[catalog] ([catalog_id], [catalog_group_id], [catalog_name], [catalog_full_name], [manufacture_id], [model_code], [order_code], [size_w], [size_d], [size_h], [weight], [num_of_ports], [image_id], [icon_16_image_id], [icon_32_image_id], [icon_48_image_id], [icon_64_image_id], [link_80_image_id], [deletable], [pp_use_intelligent], [pp_config_type], [pp_media_type], [pp_utp_jack_type], [pp_utp_shield_type], [pp_figure_type], [ic_num_of_pp_connectors], [ic_num_of_max_pp], [ic_num_of_power], [st_num_of_disk], [sw_figure_type], [sw_num_of_slots], [cp_plug_side], [ca_use_intelligent], [ca_install_type], [ca_for_army], [ca_media_type], [ca_is_utp_shield], [ca_utp_cable_type], [ca_fiber_cable_type], [ca_fiber_connector_type], [ca_disp_color_rgb], [ca_disp_name], [rm_is_rack_mount], [rm_unit_size], [rm_image_220_image_id], [rm_image_440_image_id], [rm_image_880_image_id], [user_id], [remarks], [last_updated], [sw_model_type]) 
VALUES (443003, 3410, N'General Consolidation Point (Both, 24 ports)', NULL, 10010001, NULL, NULL, NULL, NULL, NULL, NULL, 24, NULL, 2110015, NULL, NULL, NULL, 2120014, N'N', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 90001, NULL, CAST(0x0000A39A009812B8 AS DateTime), NULL)
INSERT [dbo].[catalog] ([catalog_id], [catalog_group_id], [catalog_name], [catalog_full_name], [manufacture_id], [model_code], [order_code], [size_w], [size_d], [size_h], [weight], [num_of_ports], [image_id], [icon_16_image_id], [icon_32_image_id], [icon_48_image_id], [icon_64_image_id], [link_80_image_id], [deletable], [pp_use_intelligent], [pp_config_type], [pp_media_type], [pp_utp_jack_type], [pp_utp_shield_type], [pp_figure_type], [ic_num_of_pp_connectors], [ic_num_of_max_pp], [ic_num_of_power], [st_num_of_disk], [sw_figure_type], [sw_num_of_slots], [cp_plug_side], [ca_use_intelligent], [ca_install_type], [ca_for_army], [ca_media_type], [ca_is_utp_shield], [ca_utp_cable_type], [ca_fiber_cable_type], [ca_fiber_connector_type], [ca_disp_color_rgb], [ca_disp_name], [rm_is_rack_mount], [rm_unit_size], [rm_image_220_image_id], [rm_image_440_image_id], [rm_image_880_image_id], [user_id], [remarks], [last_updated], [sw_model_type]) 
VALUES (444001, 3440, N'Patch Panel (UTP)', N'', 10010001, N'', N'', 0, 0, 0, CAST(0 AS Numeric(18, 0)), 24, 29001061, 2110018, NULL, NULL, NULL, 2120017, N'N', N'N', N'-', N'U', N'N', N'N', N'F', 0, NULL, 1, 0, N'-', 0, N'-', N'N', N'-', N'N', N'-', N'-', N'', N'', N'', 16777215, N'', N'Y', 1, 2130040, 2140040, 0, 90001, N'', CAST(0x0000A406011E32EA AS DateTime), N'-')
INSERT [dbo].[catalog] ([catalog_id], [catalog_group_id], [catalog_name], [catalog_full_name], [manufacture_id], [model_code], [order_code], [size_w], [size_d], [size_h], [weight], [num_of_ports], [image_id], [icon_16_image_id], [icon_32_image_id], [icon_48_image_id], [icon_64_image_id], [link_80_image_id], [deletable], [pp_use_intelligent], [pp_config_type], [pp_media_type], [pp_utp_jack_type], [pp_utp_shield_type], [pp_figure_type], [ic_num_of_pp_connectors], [ic_num_of_max_pp], [ic_num_of_power], [st_num_of_disk], [sw_figure_type], [sw_num_of_slots], [cp_plug_side], [ca_use_intelligent], [ca_install_type], [ca_for_army], [ca_media_type], [ca_is_utp_shield], [ca_utp_cable_type], [ca_fiber_cable_type], [ca_fiber_connector_type], [ca_disp_color_rgb], [ca_disp_name], [rm_is_rack_mount], [rm_unit_size], [rm_image_220_image_id], [rm_image_440_image_id], [rm_image_880_image_id], [user_id], [remarks], [last_updated], [sw_model_type]) 
VALUES (444002, 3440, N'Patch Panel (UTP, Angled, Module, Cat. 6)', N'', 10010001, N'', N'', 0, 0, 0, CAST(0 AS Numeric(18, 0)), 24, 0, 2110018, NULL, NULL, NULL, 2120017, N'N', N'N', N'-', N'U', N'M', N'N', N'A', 0, NULL, 2, 0, N'-', 0, N'-', N'N', N'-', N'N', N'-', N'-', N'', N'', N'', 16777215, N'', N'Y', 1, 2130041, 2140041, 0, 90001, N'', CAST(0x0000A406011E4F9C AS DateTime), N'-')
INSERT [dbo].[catalog] ([catalog_id], [catalog_group_id], [catalog_name], [catalog_full_name], [manufacture_id], [model_code], [order_code], [size_w], [size_d], [size_h], [weight], [num_of_ports], [image_id], [icon_16_image_id], [icon_32_image_id], [icon_48_image_id], [icon_64_image_id], [link_80_image_id], [deletable], [pp_use_intelligent], [pp_config_type], [pp_media_type], [pp_utp_jack_type], [pp_utp_shield_type], [pp_figure_type], [ic_num_of_pp_connectors], [ic_num_of_max_pp], [ic_num_of_power], [st_num_of_disk], [sw_figure_type], [sw_num_of_slots], [cp_plug_side], [ca_use_intelligent], [ca_install_type], [ca_for_army], [ca_media_type], [ca_is_utp_shield], [ca_utp_cable_type], [ca_fiber_cable_type], [ca_fiber_connector_type], [ca_disp_color_rgb], [ca_disp_name], [rm_is_rack_mount], [rm_unit_size], [rm_image_220_image_id], [rm_image_440_image_id], [rm_image_880_image_id], [user_id], [remarks], [last_updated], [sw_model_type]) 
VALUES (490001, 3460, N'General Patch Cord', N'Patch Cord', 10010001, N'', N'', 0, 0, 0, CAST(0 AS Numeric(18, 0)), 0, NULL, 211099, NULL, NULL, NULL, 2120099, N'Y', N'N', N'-', N'-', N'-', N'-', N'-', 0, NULL, 1, 0, N'-', 0, N'-', N'N', N'P', N'N', N'U', N'N', N'5a', N'-', N'-', -15689897, N'Patch Cord', NULL, NULL, NULL, NULL, 0, 0, N'', CAST(0x0000A39A009812B8 AS DateTime), NULL)
INSERT [dbo].[catalog] ([catalog_id], [catalog_group_id], [catalog_name], [catalog_full_name], [manufacture_id], [model_code], [order_code], [size_w], [size_d], [size_h], [weight], [num_of_ports], [image_id], [icon_16_image_id], [icon_32_image_id], [icon_48_image_id], [icon_64_image_id], [link_80_image_id], [deletable], [pp_use_intelligent], [pp_config_type], [pp_media_type], [pp_utp_jack_type], [pp_utp_shield_type], [pp_figure_type], [ic_num_of_pp_connectors], [ic_num_of_max_pp], [ic_num_of_power], [st_num_of_disk], [sw_figure_type], [sw_num_of_slots], [cp_plug_side], [ca_use_intelligent], [ca_install_type], [ca_for_army], [ca_media_type], [ca_is_utp_shield], [ca_utp_cable_type], [ca_fiber_cable_type], [ca_fiber_connector_type], [ca_disp_color_rgb], [ca_disp_name], [rm_is_rack_mount], [rm_unit_size], [rm_image_220_image_id], [rm_image_440_image_id], [rm_image_880_image_id], [user_id], [remarks], [last_updated], [sw_model_type]) 
VALUES (447001, 3470, N'General Cable (UTP, 5e)', N'', 10010001, N'', N'', 0, 0, 0, CAST(0 AS Numeric(18, 0)), 0, NULL, 2110099, NULL, NULL, NULL, 2120099, N'Y', N'N', N'-', N'-', N'N', N'N', N'-', 0, NULL, 1, 0, N'-', 0, N'-', N'N', N'F', N'N', N'U', N'N', N'5', N'-', N'-', -580313471, N'고정링크', NULL, NULL, NULL, NULL, 0, 90001, N'', CAST(0x0000A39A009812B8 AS DateTime), NULL)
INSERT [dbo].[catalog] ([catalog_id], [catalog_group_id], [catalog_name], [catalog_full_name], [manufacture_id], [model_code], [order_code], [size_w], [size_d], [size_h], [weight], [num_of_ports], [image_id], [icon_16_image_id], [icon_32_image_id], [icon_48_image_id], [icon_64_image_id], [link_80_image_id], [deletable], [pp_use_intelligent], [pp_config_type], [pp_media_type], [pp_utp_jack_type], [pp_utp_shield_type], [pp_figure_type], [ic_num_of_pp_connectors], [ic_num_of_max_pp], [ic_num_of_power], [st_num_of_disk], [sw_figure_type], [sw_num_of_slots], [cp_plug_side], [ca_use_intelligent], [ca_install_type], [ca_for_army], [ca_media_type], [ca_is_utp_shield], [ca_utp_cable_type], [ca_fiber_cable_type], [ca_fiber_connector_type], [ca_disp_color_rgb], [ca_disp_name], [rm_is_rack_mount], [rm_unit_size], [rm_image_220_image_id], [rm_image_440_image_id], [rm_image_880_image_id], [user_id], [remarks], [last_updated], [sw_model_type]) 
VALUES (447002, 3470, N'General Cable (UTP, 6)', N'', 10010001, N'', N'', 0, 0, 0, CAST(0 AS Numeric(18, 0)), 0, NULL, 2110099, NULL, NULL, NULL, 2120099, N'Y', N'N', N'-', N'-', N'N', N'N', N'-', 0, NULL, 1, 0, N'-', 0, N'-', N'N', N'F', N'N', N'U', N'N', N'5', N'-', N'-', -580313471, N'고정링크', NULL, NULL, NULL, NULL, 0, 90001, N'', CAST(0x0000A39A009812B8 AS DateTime), NULL)
INSERT [dbo].[catalog] ([catalog_id], [catalog_group_id], [catalog_name], [catalog_full_name], [manufacture_id], [model_code], [order_code], [size_w], [size_d], [size_h], [weight], [num_of_ports], [image_id], [icon_16_image_id], [icon_32_image_id], [icon_48_image_id], [icon_64_image_id], [link_80_image_id], [deletable], [pp_use_intelligent], [pp_config_type], [pp_media_type], [pp_utp_jack_type], [pp_utp_shield_type], [pp_figure_type], [ic_num_of_pp_connectors], [ic_num_of_max_pp], [ic_num_of_power], [st_num_of_disk], [sw_figure_type], [sw_num_of_slots], [cp_plug_side], [ca_use_intelligent], [ca_install_type], [ca_for_army], [ca_media_type], [ca_is_utp_shield], [ca_utp_cable_type], [ca_fiber_cable_type], [ca_fiber_connector_type], [ca_disp_color_rgb], [ca_disp_name], [rm_is_rack_mount], [rm_unit_size], [rm_image_220_image_id], [rm_image_440_image_id], [rm_image_880_image_id], [user_id], [remarks], [last_updated], [sw_model_type]) 
VALUES (447003, 3470, N'General Cable (UTP, 6a)', N'', 10010001, N'', N'', 0, 0, 0, CAST(0 AS Numeric(18, 0)), 0, NULL, 2110099, NULL, NULL, NULL, 2120099, N'Y', N'N', N'-', N'-', N'N', N'N', N'-', 0, NULL, 1, 0, N'-', 0, N'-', N'N', N'F', N'N', N'U', N'N', N'5', N'-', N'-', -580313471, N'고정링크', NULL, NULL, NULL, NULL, 0, 90001, N'', CAST(0x0000A39A009812B8 AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[catalog] OFF




/************************************************************************
 *
 * asset
 *
 ************************************************************************/
 
DECLARE @INST_DATE DATE
SET @INST_DATE = GETDATE()
DECLARE @ASSET_ID_HUB INT
EXEC sp_add_asset		434001,		0,					'HUB',		NULL,		NULL,	'Remarks...',	90001,	@INST_DATE,		90001,		@ASSET_ID_HUB OUT



/************************************************************************
 *
 * location
 *
 ************************************************************************/

 SET IDENTITY_INSERT [dbo].[location] ON
 insert into location (location_id, location_name, location_level, location_path, remarks, disp_order)  values (0, 'World', 0, 'World', 'Virtual PC가 저장되는 곳', 1)
 SET IDENTITY_INSERT [dbo].[location] OFF



/************************************************************************
 *
 * net_scan_scheduler
 *
 ************************************************************************/

--INSERT INTO [dbo].[net_scan_scheduler] (user_id) VALUES(@USER_ID_ADMIN1)

INSERT INTO [dbo].[net_scan_scheduler]
           ([schedule_time]
           ,[repeat_pattern]
           ,[repeat_day0]
           ,[repeat_day1]
           ,[repeat_day2]
           ,[repeat_day3]
           ,[repeat_day4]
           ,[repeat_day5]
           ,[repeat_day6]
           ,[user_id]
           ,[remarks]
           ,[last_updated]
           ,[enabled]
           ,[repeat_minute])
     VALUES
           ('00:10', 'E', '-', '-', '-', '-', '-', '-', '-',  90001, '', getdate(), 'N', 30);



/************************************************************************
 *
 * report
 *
 ************************************************************************/

SET IDENTITY_INSERT [dbo].[report] ON 
INSERT [dbo].[report] ([report_id], [report_desc], [num_of_report_column]) VALUES (1120001, N'List of Manufacture', 8)
INSERT [dbo].[report] ([report_id], [report_desc], [num_of_report_column]) VALUES (1120002, N'List of Contects', 12)
INSERT [dbo].[report] ([report_id], [report_desc], [num_of_report_column]) VALUES (1120003, N'List of Catalog Group', 4)
INSERT [dbo].[report] ([report_id], [report_desc], [num_of_report_column]) VALUES (1120005, N'List of Catalog', 5)
INSERT [dbo].[report] ([report_id], [report_desc], [num_of_report_column]) VALUES (1120006, N'List of Asset', 5)
INSERT [dbo].[report] ([report_id], [report_desc], [num_of_report_column]) VALUES (1120008, N'List of LineLink', 5)
INSERT [dbo].[report] ([report_id], [report_desc], [num_of_report_column]) VALUES (1120010, N'List of Location', 6)
INSERT [dbo].[report] ([report_id], [report_desc], [num_of_report_column]) VALUES (1120011, N'List of User', 6)
INSERT [dbo].[report] ([report_id], [report_desc], [num_of_report_column]) VALUES (1121010, N'List of Event', 6)
INSERT [dbo].[report] ([report_id], [report_desc], [num_of_report_column]) VALUES (1121012, N'List of Environment', 6)
INSERT [dbo].[report] ([report_id], [report_desc], [num_of_report_column]) VALUES (1121014, N'List of Log', 6)
INSERT [dbo].[report] ([report_id], [report_desc], [num_of_report_column]) VALUES (1121016, N'List of Work Order', 6)
SET IDENTITY_INSERT [dbo].[report] OFF



/************************************************************************
 *
 * report_lang
 *
 ************************************************************************/
 
SET IDENTITY_INSERT [dbo].[report_lang] ON 
INSERT [dbo].[report_lang] ([report_lang_id], [report_id], [lang_id], [report_name]) VALUES (79, 1120001, 1080001, N'제조사 목록')
INSERT [dbo].[report_lang] ([report_lang_id], [report_id], [lang_id], [report_name]) VALUES (80, 1120002, 1080001, N'연락처 목록')
INSERT [dbo].[report_lang] ([report_lang_id], [report_id], [lang_id], [report_name]) VALUES (81, 1120003, 1080001, N'카탈로그 그룹 목록')
INSERT [dbo].[report_lang] ([report_lang_id], [report_id], [lang_id], [report_name]) VALUES (92, 1120005, 1080001, N'카탈로그 목록')
INSERT [dbo].[report_lang] ([report_lang_id], [report_id], [lang_id], [report_name]) VALUES (94, 1120006, 1080001, N'엣셋목록')
INSERT [dbo].[report_lang] ([report_lang_id], [report_id], [lang_id], [report_name]) VALUES (97, 1121010, 1080001, N'이벤트목록')
INSERT [dbo].[report_lang] ([report_lang_id], [report_id], [lang_id], [report_name]) VALUES (100, 1121012, 1080001, N'환경정보 목록')
INSERT [dbo].[report_lang] ([report_lang_id], [report_id], [lang_id], [report_name]) VALUES (104, 1121014, 1080001, N'로그 목록')
INSERT [dbo].[report_lang] ([report_lang_id], [report_id], [lang_id], [report_name]) VALUES (111, 1121016, 1080001, N'작업 목록')
SET IDENTITY_INSERT [dbo].[report_lang] OFF



/************************************************************************
 *
 * report_lang_column
 *
 ************************************************************************/
 
SET IDENTITY_INSERT [dbo].[report_lang_column] ON 
INSERT [dbo].[report_lang_column] ([report_lang_column_id], [report_id], [lang_id], [report_column_no], [report_column_name], [column_width]) VALUES (209, 1120001, 1080001, 1, N'순번', NULL)
INSERT [dbo].[report_lang_column] ([report_lang_column_id], [report_id], [lang_id], [report_column_no], [report_column_name], [column_width]) VALUES (210, 1120001, 1080001, 2, N'제조사ID', NULL)
INSERT [dbo].[report_lang_column] ([report_lang_column_id], [report_id], [lang_id], [report_column_no], [report_column_name], [column_width]) VALUES (211, 1120001, 1080001, 3, N'회사명', NULL)
INSERT [dbo].[report_lang_column] ([report_lang_column_id], [report_id], [lang_id], [report_column_no], [report_column_name], [column_width]) VALUES (212, 1120001, 1080001, 4, N'전화번호', NULL)
INSERT [dbo].[report_lang_column] ([report_lang_column_id], [report_id], [lang_id], [report_column_no], [report_column_name], [column_width]) VALUES (213, 1120001, 1080001, 5, N'우편번호', NULL)
INSERT [dbo].[report_lang_column] ([report_lang_column_id], [report_id], [lang_id], [report_column_no], [report_column_name], [column_width]) VALUES (214, 1120001, 1080001, 6, N'대표이사명', NULL)
INSERT [dbo].[report_lang_column] ([report_lang_column_id], [report_id], [lang_id], [report_column_no], [report_column_name], [column_width]) VALUES (215, 1120001, 1080001, 7, N'홈페이지', NULL)
INSERT [dbo].[report_lang_column] ([report_lang_column_id], [report_id], [lang_id], [report_column_no], [report_column_name], [column_width]) VALUES (216, 1120001, 1080001, 8, N'비고', NULL)
SET IDENTITY_INSERT [dbo].[report_lang_column] OFF



/************************************************************************
 *
 * update_func
 *
 ************************************************************************/

INSERT INTO [dbo].[update_func] ([update_func_id], [update_func_desc], [invoked_cnt]) 	VALUES (1, 'Location Updated.', 0)
INSERT INTO [dbo].[update_func] ([update_func_id], [update_func_desc], [invoked_cnt])	VALUES (2, 'Asset Updated.', 0)
INSERT INTO [dbo].[update_func] ([update_func_id], [update_func_desc], [invoked_cnt])	VALUES (3, 'Link Diagram Updated.', 0)
INSERT INTO [dbo].[update_func] ([update_func_id], [update_func_desc], [invoked_cnt])	VALUES (4, 'Terminal Updated.', 0)
INSERT INTO [dbo].[update_func] ([update_func_id], [update_func_desc], [invoked_cnt])	VALUES (5, 'i-PP Status Updated.', 0)
INSERT INTO [dbo].[update_func] ([update_func_id], [update_func_desc], [invoked_cnt])	VALUES (6, 'Event Updated.', 0)

