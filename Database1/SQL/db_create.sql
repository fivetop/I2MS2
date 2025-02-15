USE [i2ms2]
GO
/****** Object:  Table [dbo].[asset]    Script Date: 2015-01-22 오후 1:51:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[asset](
	[asset_id] [int] IDENTITY(59000001,1) NOT NULL,
	[catalog_id] [int] NOT NULL,
	[location_id] [int] NOT NULL,
	[asset_name] [varchar](80) NOT NULL,
	[serial_no] [varchar](40) NULL,
	[ipv4] [varchar](15) NULL,
	[ipv6] [varchar](39) NULL,
	[install_user_name] [varchar](40) NULL,
	[install_date] [datetime] NULL,
	[user_id] [int] NOT NULL,
	[remarks] [varchar](80) NULL,
	[is_layout] [char](1) NOT NULL,
	[pos_x] [int] NULL,
	[pos_y] [int] NULL,
	[last_updated] [datetime] NOT NULL,
 CONSTRAINT [PK_asset] PRIMARY KEY CLUSTERED 
(
	[asset_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[asset_aux]    Script Date: 2015-01-22 오후 1:51:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[asset_aux](
	[asset_id] [int] NOT NULL,
	[as_management_div] [varchar](40) NULL,
	[as_management_user_name] [varchar](40) NULL,
	[as_free_start_date] [date] NULL,
	[as_free_duration] [int] NULL,
	[as_free_end_date] [date] NULL,
	[as_start_date] [date] NULL,
	[as_duration] [int] NULL,
	[as_end_date] [date] NULL,
	[as_price] [int] NULL,
	[as_company] [varchar](40) NULL,
	[bu_purchase_date] [varchar](40) NULL,
	[bu_purchase_user_name] [varchar](40) NULL,
	[bu_depreciation_start_year] [int] NULL,
	[bu_depreciation_duration] [int] NULL,
	[bu_depreciation_end_year] [int] NULL,
	[snmp_get_community] [varchar](20) NULL,
	[snmp_set_community] [varchar](20) NULL,
	[snmp_trap_svr_ip] [varchar](15) NULL,
	[snmp_version] [char](1) NULL,
	[snmp_v3_user] [varchar](40) NULL,
	[snmp_v3_password] [varchar](40) NULL,
	[ic_con_id] [int] NULL,
	[sv_kind_of_os] [varchar](40) NULL,
	[sv_os_ver] [varchar](40) NULL,
	[sv_host_name] [varchar](40) NULL,
	[sv_num_of_nic] [int] NULL,
	[sv_tot_disk_amount] [int] NULL,
	[sv_num_of_disks] [int] NULL,
	[ra_vcm_type] [char](1) NULL,
	[ra_vcm_depth] [int] NULL,
	[st_cur_num_of_disks] [int] NULL,
	[st_cur_disk_amount] [int] NULL,
	[st_type] [char](1) NULL,
	[sw_vlan] [int] NULL,
	[sw_alias] [varchar](8) NULL,
 CONSTRAINT [PK_asset_aux] PRIMARY KEY CLUSTERED 
(
	[asset_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[asset_ext]    Script Date: 2015-01-22 오후 1:51:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[asset_ext](
	[asset_ext_id] [int] IDENTITY(1,1) NOT NULL,
	[asset_id] [int] NOT NULL,
	[ext_id] [int] NOT NULL,
	[catalog_id] [int] NOT NULL,
	[ans_string] [varchar](80) NULL,
	[ans_numeric] [int] NULL,
	[ans_date] [date] NULL,
	[ans_time] [time](7) NULL,
	[user_id] [int] NOT NULL,
	[last_updated] [timestamp] NOT NULL,
 CONSTRAINT [PK_asset_ext] PRIMARY KEY CLUSTERED 
(
	[asset_ext_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[asset_ipp_port_link]    Script Date: 2015-01-22 오후 1:51:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[asset_ipp_port_link](
	[asset_ipp_port_link_id] [int] IDENTITY(1,1) NOT NULL,
	[ipp_asset_id] [int] NOT NULL,
	[port_no] [int] NOT NULL,
	[alarm_status] [char](1) NOT NULL,
	[wo_status] [char](1) NOT NULL,
	[is_port_trace] [char](1) NOT NULL,
	[ipp_port_status] [char](1) NOT NULL,
	[remote_ic_asset_id] [int] NULL,
	[remote_pp_asset_id] [int] NULL,
	[remote_port_no] [int] NULL,
	[last_updated] [timestamp] NOT NULL,
 CONSTRAINT [PK_asset_ipp_port_link] PRIMARY KEY CLUSTERED 
(
	[asset_ipp_port_link_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[asset_port_link]    Script Date: 2015-01-22 오후 1:51:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[asset_port_link](
	[asset_port_link_id] [int] IDENTITY(1,1) NOT NULL,
	[asset_id] [int] NOT NULL,
	[port_no] [int] NOT NULL,
	[front_asset_id] [int] NULL,
	[front_port_no] [int] NULL,
	[front_plug_side] [char](1) NULL,
	[front_cable_catalog_id] [int] NULL,
	[rear_asset_id] [int] NULL,
	[rear_port_no] [int] NULL,
	[rear_plug_side] [char](1) NULL,
	[rear_cable_catalog_id] [int] NULL,
	[last_updated] [timestamp] NOT NULL,
 CONSTRAINT [PK_asset_port_link] PRIMARY KEY CLUSTERED 
(
	[asset_port_link_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[asset_terminal]    Script Date: 2015-01-22 오후 1:51:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[asset_terminal](
	[terminal_id] [int] IDENTITY(69000001,1) NOT NULL,
	[terminal_asset_id] [int] NOT NULL,
	[net_id] [int] NOT NULL,
	[mac] [char](17) NOT NULL,
	[cur_enable] [char](1) NOT NULL,
	[cur_net_bios_name] [varchar](40) NULL,
	[cur_sw_asset_id] [int] NULL,
	[cur_sw_port_no] [int] NULL,
	[cur_outlet_asset_id] [int] NULL,
	[cur_outlet_port_no] [int] NULL,
	[terminal_status] [char](1) NOT NULL,
	[new_enable] [char](1) NOT NULL,
	[new_net_bios_name] [varchar](40) NULL,
	[new_sw_asset_id] [int] NULL,
	[new_sw_port_no] [int] NULL,
	[new_outlet_asset_id] [int] NULL,
	[new_outlet_port_no] [int] NULL,
	[last_activated] [datetime] NOT NULL,
	[last_updated] [datetime] NOT NULL,
	[terminal_grant] [int] NULL,
 CONSTRAINT [PK_asset_terminal] PRIMARY KEY CLUSTERED 
(
	[terminal_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[asset_terminal_ip]    Script Date: 2015-01-22 오후 1:51:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[asset_terminal_ip](
	[ip_id] [int] IDENTITY(1,1) NOT NULL,
	[terminal_id] [int] NOT NULL,
	[cur_enable] [char](1) NOT NULL,
	[new_enable] [char](1) NOT NULL,
	[ipv4] [varchar](15) NULL,
 CONSTRAINT [PK_asset_terminal_ip] PRIMARY KEY CLUSTERED 
(
	[ip_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[asset_tree]    Script Date: 2015-01-22 오후 1:51:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[asset_tree](
	[asset_tree_id] [int] IDENTITY(19200001,1) NOT NULL,
	[disp_name] [varchar](40) NOT NULL,
	[disp_level] [int] NOT NULL,
	[is_alarm] [char](1) NOT NULL,
	[image_id] [int] NOT NULL,
	[asset_id] [int] NULL,
	[location_id] [int] NOT NULL,
	[disp_order] [int] NOT NULL,
 CONSTRAINT [PK_asset_tree] PRIMARY KEY CLUSTERED 
(
	[asset_tree_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[building]    Script Date: 2015-01-22 오후 1:51:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[building](
	[building_id] [int] IDENTITY(89100001,1) NOT NULL,
	[site_id] [int] NOT NULL,
	[building_name] [varchar](40) NOT NULL,
	[building_image_id] [int] NULL,
	[user_id] [int] NOT NULL,
	[last_updated] [timestamp] NOT NULL,
	[remarks] [varchar](80) NULL,
 CONSTRAINT [PK_building] PRIMARY KEY CLUSTERED 
(
	[building_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[catalog]    Script Date: 2015-01-22 오후 1:51:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[catalog](
	[catalog_id] [int] IDENTITY(49000001,1) NOT NULL,
	[catalog_group_id] [int] NOT NULL,
	[catalog_name] [varchar](80) NOT NULL,
	[catalog_full_name] [varchar](160) NULL,
	[manufacture_id] [int] NULL,
	[model_code] [varchar](40) NULL,
	[order_code] [varchar](40) NULL,
	[size_w] [int] NULL,
	[size_d] [int] NULL,
	[size_h] [int] NULL,
	[weight] [numeric](18, 0) NULL,
	[num_of_ports] [int] NOT NULL,
	[image_id] [int] NULL,
	[icon_16_image_id] [int] NULL,
	[icon_32_image_id] [int] NULL,
	[icon_48_image_id] [int] NULL,
	[icon_64_image_id] [int] NULL,
	[link_80_image_id] [int] NULL,
	[deletable] [char](1) NOT NULL,
	[pp_use_intelligent] [char](1) NULL,
	[pp_config_type] [char](1) NULL,
	[pp_media_type] [char](1) NULL,
	[pp_utp_jack_type] [char](1) NULL,
	[pp_utp_shield_type] [char](1) NULL,
	[pp_figure_type] [char](1) NULL,
	[ic_num_of_pp_connectors] [int] NULL,
	[ic_num_of_max_pp] [int] NULL,
	[ic_num_of_power] [int] NULL,
	[st_num_of_disk] [int] NULL,
	[sw_figure_type] [char](1) NULL,
	[sw_num_of_slots] [int] NULL,
	[cp_plug_side] [char](1) NULL,
	[ca_use_intelligent] [char](1) NULL,
	[ca_install_type] [char](1) NULL,
	[ca_for_army] [char](1) NULL,
	[ca_media_type] [char](1) NULL,
	[ca_is_utp_shield] [char](1) NULL,
	[ca_utp_cable_type] [varchar](2) NULL,
	[ca_fiber_cable_type] [varchar](2) NULL,
	[ca_fiber_connector_type] [varchar](2) NULL,
	[ca_disp_color_rgb] [int] NULL,
	[ca_disp_name] [varchar](20) NULL,
	[rm_is_rack_mount] [char](1) NULL,
	[rm_unit_size] [int] NULL,
	[rm_image_220_image_id] [int] NULL,
	[rm_image_440_image_id] [int] NULL,
	[rm_image_880_image_id] [int] NULL,
	[user_id] [int] NOT NULL,
	[remarks] [varchar](80) NULL,
	[last_updated] [datetime] NOT NULL,
	[sw_model_type] [char](1) NULL,
 CONSTRAINT [PK_catalog] PRIMARY KEY CLUSTERED 
(
	[catalog_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[catalog_ext]    Script Date: 2015-01-22 오후 1:51:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[catalog_ext](
	[catalog_ext_id] [int] IDENTITY(1,1) NOT NULL,
	[catalog_id] [int] NOT NULL,
	[ext_id] [int] NOT NULL,
	[user_id] [int] NOT NULL,
	[last_updated] [timestamp] NOT NULL,
 CONSTRAINT [PK_catalog_ext] PRIMARY KEY CLUSTERED 
(
	[catalog_ext_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[catalog_group]    Script Date: 2015-01-22 오후 1:51:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[catalog_group](
	[catalog_group_id] [int] IDENTITY(39000001,1) NOT NULL,
	[catalog_group_name] [varchar](40) NOT NULL,
	[catalog_level] [int] NULL,
	[enable] [varchar](1) NULL,
	[deletable] [varchar](1) NULL,
	[level1_catalog_group_id] [int] NULL,
	[level2_catalog_group_id] [int] NULL,
	[disp_order] [int] NOT NULL,
 CONSTRAINT [PK_catalog_group] PRIMARY KEY CLUSTERED 
(
	[catalog_group_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[changed_link_hist]    Script Date: 2015-01-22 오후 1:51:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[changed_link_hist](
	[changed_link_id] [int] IDENTITY(19150001,1) NOT NULL,
	[terminal_asset_id] [int] NOT NULL,
	[last_updated] [timestamp] NOT NULL,
 CONSTRAINT [PK_changed_link_hist] PRIMARY KEY CLUSTERED 
(
	[changed_link_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[changed_link_hist_cell]    Script Date: 2015-01-22 오후 1:51:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[changed_link_hist_cell](
	[changed_link_hist_cell_id] [int] IDENTITY(1,1) NOT NULL,
	[changed_link_id] [int] NOT NULL,
	[cell_pos] [int] NOT NULL,
	[cell_asset_id] [int] NULL,
	[cell_port_no] [int] NULL,
	[cell_cable_catalog_id] [int] NULL,
	[front_plug_status] [char](1) NOT NULL,
	[rear_plug_status] [char](1) NOT NULL,
	[last_updated] [timestamp] NOT NULL,
 CONSTRAINT [PK_link_hist_cell] PRIMARY KEY CLUSTERED 
(
	[changed_link_hist_cell_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[code]    Script Date: 2015-01-22 오후 1:51:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[code](
	[code_id] [int] IDENTITY(1,1) NOT NULL,
	[fixed_code_no] [int] NOT NULL,
	[start_code_no] [int] NOT NULL,
	[code_name] [varchar](80) NOT NULL,
	[remarks] [varchar](80) NULL,
 CONSTRAINT [PK_coderack] PRIMARY KEY CLUSTERED 
(
	[code_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[contact]    Script Date: 2015-01-22 오후 1:51:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[contact](
	[contact_id] [int] IDENTITY(19010001,1) NOT NULL,
	[contact_name] [varchar](30) NOT NULL,
	[manufacture_id] [int] NOT NULL,
	[duty] [varchar](20) NULL,
	[position] [varchar](20) NULL,
	[phone] [varchar](20) NULL,
	[mobile] [varchar](20) NULL,
	[email] [varchar](80) NULL,
	[remarks] [varchar](80) NULL,
 CONSTRAINT [PK_contact_1] PRIMARY KEY CLUSTERED 
(
	[contact_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[drawing_3d]    Script Date: 2015-01-22 오후 1:51:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[drawing_3d](
	[drawing_3d_id] [int] IDENTITY(19040001,1) NOT NULL,
	[file_name] [varchar](256) NOT NULL,
	[remarks] [varchar](80) NULL
) ON [PRIMARY]
SET ANSI_PADDING ON
ALTER TABLE [dbo].[drawing_3d] ADD [drawing_3d_name] [varchar](80) NULL
 CONSTRAINT [PK_drawing_3d] PRIMARY KEY CLUSTERED 
(
	[drawing_3d_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[eb_port_config]    Script Date: 2015-01-22 오후 1:51:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[eb_port_config](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[asset_id] [int] NOT NULL,
	[port_no] [int] NOT NULL,
	[port_type] [char](1) NOT NULL,
	[high_voltage_th] [nchar](10) NULL,
	[low_voltage_th] [nchar](10) NULL,
	[high_current_th] [int] NULL,
	[low_current_th] [int] NULL,
	[high_power_th] [int] NULL,
	[low_power_th] [int] NULL,
	[high_powerh_th] [int] NULL,
	[low_powerh_th] [int] NULL,
	[high_temp_th] [int] NULL,
	[low_temp_th] [int] NULL,
	[high_humi_th] [int] NULL,
	[low_humi_th] [int] NULL,
	[alarm] [char](1) NOT NULL,
	[last_updated] [datetime] NOT NULL,
 CONSTRAINT [PK_eb_port_config] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[eb_port_data_cur]    Script Date: 2015-01-22 오후 1:51:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[eb_port_data_cur](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[asset_id] [int] NOT NULL,
	[port_no] [int] NOT NULL,
	[power_v] [int] NULL,
	[power_i] [int] NULL,
	[power_p] [int] NULL,
	[power_ph] [int] NULL,
	[sensor_t] [int] NULL,
	[sensor_h] [int] NULL,
	[door] [int] NULL,
	[last_updated] [datetime] NOT NULL,
 CONSTRAINT [PK_eb_cur_port_data] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[eb_port_data_hour]    Script Date: 2015-01-22 오후 1:51:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[eb_port_data_hour](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[asset_id] [int] NOT NULL,
	[port_no] [int] NOT NULL,
	[date] [date] NOT NULL,
	[time_0_23] [int] NOT NULL,
	[power_v] [int] NULL,
	[power_v_cnt] [int] NULL,
	[power_i] [int] NULL,
	[power_i_cnt] [int] NULL,
	[power_p] [int] NULL,
	[power_p_cnt] [int] NULL,
	[power_ph] [int] NULL,
	[power_ph_cnt] [int] NULL,
	[sensor_t] [int] NULL,
	[sensor_t_cnt] [int] NULL,
	[sensor_h] [int] NULL,
	[sensor_h_cnt] [int] NULL,
	[door] [int] NULL,
	[power_peek_v] [int] NULL,
	[power_peek_i] [int] NULL,
	[power_peek_p] [int] NULL,
	[power_peek_ph] [int] NULL,
	[sensor_peek_t] [int] NULL,
	[sensor_peek_h] [int] NULL,
	[last_updated] [datetime] NOT NULL,
 CONSTRAINT [PK_eb_port_data_hour] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[event]    Script Date: 2015-01-22 오후 1:51:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[event](
	[event_id] [int] IDENTITY(19090001,1) NOT NULL,
	[event_type] [char](1) NOT NULL,
	[popup_screen] [char](1) NOT NULL,
	[send_email] [char](1) NOT NULL,
	[send_sms] [char](1) NOT NULL,
 CONSTRAINT [PK_event] PRIMARY KEY CLUSTERED 
(
	[event_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[event_hist]    Script Date: 2015-01-22 오후 1:51:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[event_hist](
	[event_hist_id] [int] IDENTITY(19100001,1) NOT NULL,
	[event_type] [char](1) NOT NULL,
	[event_id] [int] NOT NULL,
	[event_text] [varchar](160) NOT NULL,
	[location_id] [int] NULL,
	[catalog_group_id] [int] NULL,
	[catalog_id] [int] NULL,
	[asset_id] [int] NULL,
	[port_no] [int] NULL,
	[terminal_asset_id] [int] NULL,
	[mac] [char](17) NULL,
	[ipv4] [varchar](15) NULL,
	[user_id] [int] NULL,
	[is_confirm] [char](1) NOT NULL,
	[confirm_user_id] [int] NULL,
	[write_time] [datetime] NOT NULL,
	[wo_id] [int] NULL,
 CONSTRAINT [PK_event_hist] PRIMARY KEY CLUSTERED 
(
	[event_hist_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[event_lang]    Script Date: 2015-01-22 오후 1:51:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[event_lang](
	[event_lang_id] [int] IDENTITY(1,1) NOT NULL,
	[event_id] [int] NOT NULL,
	[lang_id] [int] NOT NULL,
	[event_group] [varchar](80) NOT NULL,
	[event_format] [varchar](160) NOT NULL
) ON [PRIMARY]
SET ANSI_PADDING ON
ALTER TABLE [dbo].[event_lang] ADD [event_desc] [varchar](160) NULL
 CONSTRAINT [PK_event_lang] PRIMARY KEY CLUSTERED 
(
	[event_lang_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ext_property]    Script Date: 2015-01-22 오후 1:51:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ext_property](
	[ext_id] [int] IDENTITY(19050001,1) NOT NULL,
	[ext_name] [varchar](40) NOT NULL,
	[ext_length] [int] NOT NULL,
	[ext_type] [char](1) NOT NULL,
	[num_of_ans] [int] NOT NULL,
	[user_id] [int] NULL,
	[last_updated] [timestamp] NULL,
	[remarks] [varchar](80) NULL,
 CONSTRAINT [PK_ext_property] PRIMARY KEY CLUSTERED 
(
	[ext_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ext_property_ans]    Script Date: 2015-01-22 오후 1:51:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[ext_property_ans](
	[ext_property_ans_id] [int] IDENTITY(1,1) NOT NULL,
	[ext_id] [int] NOT NULL,
	[ans_no] [int] NOT NULL,
	[ans_name] [varchar](40) NOT NULL,
	[user_id] [int] NULL,
	[last_updated] [timestamp] NULL,
	[remarks] [varchar](80) NULL,
 CONSTRAINT [PK_ext_property_ans] PRIMARY KEY CLUSTERED 
(
	[ext_property_ans_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[favorite_tree]    Script Date: 2015-01-22 오후 1:51:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[favorite_tree](
	[favorite_tree_id] [int] IDENTITY(19220001,1) NOT NULL,
	[disp_name] [varchar](40) NOT NULL,
	[disp_level] [int] NOT NULL,
	[is_alarm] [char](1) NOT NULL,
	[image_id] [int] NOT NULL,
	[location_id] [int] NULL,
	[asset_id] [int] NULL,
	[reg_user_id] [int] NOT NULL,
	[disp_order] [int] NOT NULL,
 CONSTRAINT [PK_favorite_tree] PRIMARY KEY CLUSTERED 
(
	[favorite_tree_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[floor]    Script Date: 2015-01-22 오후 1:51:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[floor](
	[floor_id] [int] IDENTITY(89200001,1) NOT NULL,
	[building_id] [int] NOT NULL,
	[floor_no] [int] NOT NULL,
	[drawing_3d_id] [int] NULL,
	[user_id] [int] NOT NULL,
	[last_updated] [timestamp] NOT NULL,
	[remarks] [varchar](80) NULL,
	[floor_name] [varchar](40) NULL,
 CONSTRAINT [PK_floor] PRIMARY KEY CLUSTERED 
(
	[floor_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[fw_upgrade]    Script Date: 2015-01-22 오후 1:51:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[fw_upgrade](
	[fw_id] [int] IDENTITY(19060001,1) NOT NULL,
	[fw_name] [varchar](80) NOT NULL,
	[fw_version] [varchar](20) NOT NULL,
	[fw_file_name] [varchar](256) NOT NULL,
	[user_id] [int] NOT NULL,
	[remarks] [varchar](80) NULL,
	[last_updated] [datetime] NOT NULL,
 CONSTRAINT [PK_fw_upgrade] PRIMARY KEY CLUSTERED 
(
	[fw_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[fw_upgrade_hist]    Script Date: 2015-01-22 오후 1:51:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[fw_upgrade_hist](
	[fw_hist_id] [int] IDENTITY(19070001,1) NOT NULL,
	[fw_id] [int] NOT NULL,
	[ic_asset_id] [int] NOT NULL,
	[result] [char](1) NOT NULL,
	[user_id] [int] NOT NULL,
	[last_updated] [datetime] NOT NULL,
 CONSTRAINT [PK_fw_upgrade_hist] PRIMARY KEY CLUSTERED 
(
	[fw_hist_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ic_connect_status]    Script Date: 2015-01-22 오후 1:51:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ic_connect_status](
	[ic_asset_id] [int] NOT NULL,
	[ic_connect_status] [varchar](20) NULL,
	[fw_version] [varchar](20) NULL,
	[serial_no] [varchar](20) NULL,
	[fw_update_date] [datetime] NULL,
	[sys_uptime] [int] NULL,
	[ftp_server_ip] [varchar](15) NULL,
	[last_updated] [datetime] NOT NULL,
 CONSTRAINT [PK_ic_connect_status] PRIMARY KEY CLUSTERED 
(
	[ic_asset_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ic_ipp_config]    Script Date: 2015-01-22 오후 1:51:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ic_ipp_config](
	[ic_ipp_config_id] [int] IDENTITY(1,1) NOT NULL,
	[ic_asset_id] [int] NOT NULL,
	[ipp_connect_no] [int] NOT NULL,
	[ipp_asset_id] [int] NULL,
 CONSTRAINT [PK_asset_ic] PRIMARY KEY CLUSTERED 
(
	[ic_ipp_config_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[image]    Script Date: 2015-01-22 오후 1:51:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[image](
	[image_id] [int] IDENTITY(29000001,1) NOT NULL,
	[image_name] [varchar](80) NOT NULL,
	[file_name] [varchar](256) NOT NULL,
	[image_type_id] [int] NOT NULL,
	[size_x] [int] NULL,
	[size_y] [int] NULL,
	[drawing_3d_id] [int] NULL,
	[deletable] [char](1) NOT NULL,
	[remarks] [varchar](80) NULL,
 CONSTRAINT [PK_image] PRIMARY KEY CLUSTERED 
(
	[image_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[image_type]    Script Date: 2015-01-22 오후 1:51:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[image_type](
	[image_type_id] [int] IDENTITY(19160001,1) NOT NULL,
	[image_type_name] [varchar](30) NOT NULL,
	[folder_name] [varchar](40) NOT NULL,
	[size_x] [int] NOT NULL,
	[size_y] [int] NOT NULL,
	[remarks] [varchar](80) NULL,
 CONSTRAINT [PK_image_type] PRIMARY KEY CLUSTERED 
(
	[image_type_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ipp_connect_status]    Script Date: 2015-01-22 오후 1:51:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ipp_connect_status](
	[ipp_asset_id] [int] NOT NULL,
	[ic_asset_id] [int] NULL,
	[connect_status] [char](1) NULL,
 CONSTRAINT [PK_asset_ipp] PRIMARY KEY CLUSTERED 
(
	[ipp_asset_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[language]    Script Date: 2015-01-22 오후 1:51:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[language](
	[lang_id] [int] IDENTITY(19080001,1) NOT NULL,
	[lang_name] [varchar](20) NOT NULL,
	[remarks] [varchar](80) NULL,
 CONSTRAINT [PK_language] PRIMARY KEY CLUSTERED 
(
	[lang_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[location]    Script Date: 2015-01-22 오후 1:51:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[location](
	[location_id] [int] IDENTITY(19030001,1) NOT NULL,
	[location_name] [varchar](40) NOT NULL,
	[location_level] [int] NOT NULL,
	[location_path] [varchar](160) NULL,
	[site_id] [int] NULL,
	[building_id] [int] NULL,
	[floor_id] [int] NULL,
	[room_id] [int] NULL,
	[rack_id] [int] NULL,
	[remarks] [varchar](80) NULL,
	[region1_id] [int] NULL,
	[region2_id] [int] NULL,
	[disp_order] [int] NOT NULL,
 CONSTRAINT [PK_location] PRIMARY KEY CLUSTERED 
(
	[location_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[manufacture]    Script Date: 2015-01-22 오후 1:51:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[manufacture](
	[manufacture_id] [int] IDENTITY(19020001,1) NOT NULL,
	[manufacture_name] [varchar](80) NOT NULL,
	[phone] [varchar](30) NULL,
	[address] [varchar](120) NULL,
	[post] [char](7) NULL,
	[homepage_url] [varchar](80) NULL,
	[ceo_name] [varchar](30) NULL,
	[remarks] [varchar](80) NULL,
 CONSTRAINT [PK_manufacture] PRIMARY KEY CLUSTERED 
(
	[manufacture_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[net_scan]    Script Date: 2015-01-22 오후 1:51:42 ******/
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
 CONSTRAINT [PK_net_scan] PRIMARY KEY CLUSTERED 
(
	[net_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[net_scan_scheduler]    Script Date: 2015-01-22 오후 1:51:42 ******/
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
 CONSTRAINT [PK_net_scan_scheduler] PRIMARY KEY CLUSTERED 
(
	[net_scan_scheduler_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[net_scan_sw]    Script Date: 2015-01-22 오후 1:51:42 ******/
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
/****** Object:  Table [dbo].[rack]    Script Date: 2015-01-22 오후 1:51:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[rack](
	[rack_id] [int] IDENTITY(89400001,1) NOT NULL,
	[room_id] [int] NOT NULL,
	[rack_row] [varchar](1) NULL,
	[rack_no] [int] NOT NULL,
	[pos_x] [int] NULL,
	[pos_y] [int] NULL,
	[angle] [int] NOT NULL,
	[rack_catalog_id] [int] NULL,
	[vcm_l_catalog_id] [int] NULL,
	[vcm_r_catalog_id] [int] NULL,
	[user_id] [int] NOT NULL,
	[last_updated] [timestamp] NOT NULL,
	[remarks] [varchar](80) NULL,
	[rack_name] [varchar](40) NULL,
 CONSTRAINT [PK_rack] PRIMARY KEY CLUSTERED 
(
	[rack_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[rack_config]    Script Date: 2015-01-22 오후 1:51:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[rack_config](
	[rack_config_id] [int] IDENTITY(1,1) NOT NULL,
	[rack_id] [int] NOT NULL,
	[rack_mount_type] [char](1) NOT NULL,
	[slot_no] [int] NOT NULL,
	[catalog_id] [int] NULL,
	[asset_id] [int] NULL,
 CONSTRAINT [PK_rack_mount] PRIMARY KEY CLUSTERED 
(
	[rack_config_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[region1]    Script Date: 2015-01-22 오후 1:51:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[region1](
	[region1_id] [int] IDENTITY(79100001,1) NOT NULL,
	[region1_name] [varchar](40) NOT NULL,
	[region1_image_id] [int] NULL,
	[pos_x] [int] NULL,
	[pos_y] [int] NULL,
	[user_id] [int] NOT NULL,
	[last_updated] [timestamp] NOT NULL,
	[remarks] [varchar](80) NULL,
 CONSTRAINT [PK_region1] PRIMARY KEY CLUSTERED 
(
	[region1_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[region2]    Script Date: 2015-01-22 오후 1:51:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[region2](
	[region2_id] [int] IDENTITY(79200001,1) NOT NULL,
	[region1_id] [int] NOT NULL,
	[region2_name] [varchar](40) NOT NULL,
	[pos_x] [int] NULL,
	[pos_y] [int] NULL,
	[user_id] [int] NOT NULL,
	[last_updated] [timestamp] NOT NULL,
	[remarks] [varchar](80) NULL,
 CONSTRAINT [PK_region2] PRIMARY KEY CLUSTERED 
(
	[region2_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[report]    Script Date: 2015-01-22 오후 1:51:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[report](
	[report_id] [int] IDENTITY(1120001,1) NOT NULL,
	[report_desc] [varchar](80) NOT NULL,
	[num_of_report_column] [int] NOT NULL,
 CONSTRAINT [PK_report] PRIMARY KEY CLUSTERED 
(
	[report_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[report_lang]    Script Date: 2015-01-22 오후 1:51:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[report_lang](
	[report_lang_id] [int] IDENTITY(1,1) NOT NULL,
	[report_id] [int] NOT NULL,
	[lang_id] [int] NOT NULL,
	[report_name] [varchar](80) NOT NULL,
 CONSTRAINT [PK_report_lang] PRIMARY KEY CLUSTERED 
(
	[report_lang_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[report_lang_column]    Script Date: 2015-01-22 오후 1:51:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[report_lang_column](
	[report_lang_column_id] [int] IDENTITY(1,1) NOT NULL,
	[report_id] [int] NOT NULL,
	[lang_id] [int] NOT NULL,
	[report_column_no] [int] NOT NULL,
	[report_column_name] [varchar](20) NOT NULL,
	[column_width] [int] NULL,
 CONSTRAINT [PK_report_lang_column] PRIMARY KEY CLUSTERED 
(
	[report_lang_column_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[room]    Script Date: 2015-01-22 오후 1:51:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[room](
	[room_id] [int] IDENTITY(89300001,1) NOT NULL,
	[floor_id] [int] NOT NULL,
	[room_name] [varchar](40) NOT NULL,
	[square_x1] [int] NULL,
	[square_y1] [int] NULL,
	[square_x2] [int] NULL,
	[square_y2] [int] NULL,
	[user_id] [int] NOT NULL,
	[last_updated] [timestamp] NOT NULL,
	[remarks] [varchar](80) NULL,
	[flag_x] [int] NULL,
	[flag_y] [int] NULL,
 CONSTRAINT [PK_room] PRIMARY KEY CLUSTERED 
(
	[room_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[site]    Script Date: 2015-01-22 오후 1:51:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[site](
	[site_id] [int] IDENTITY(79300001,1) NOT NULL,
	[region2_id] [int] NOT NULL,
	[site_name] [varchar](40) NOT NULL,
	[site_image_id] [int] NULL,
	[user_id] [int] NOT NULL,
	[last_updated] [timestamp] NOT NULL,
	[remarks] [varchar](80) NULL,
 CONSTRAINT [PK_site] PRIMARY KEY CLUSTERED 
(
	[site_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[site_environment]    Script Date: 2015-01-22 오후 1:51:42 ******/
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
 CONSTRAINT [PK_site_environment] PRIMARY KEY CLUSTERED 
(
	[site_environment_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[site_user]    Script Date: 2015-01-22 오후 1:51:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[site_user](
	[site_user_id] [int] IDENTITY(79100001,1) NOT NULL,
	[site_id] [int] NOT NULL,
	[user_id] [int] NOT NULL,
	[user_right] [char](1) NOT NULL,
	[reg_user_id] [int] NOT NULL,
	[last_updated] [timestamp] NOT NULL,
 CONSTRAINT [PK_site_user] PRIMARY KEY CLUSTERED 
(
	[site_user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[sw_card_config]    Script Date: 2015-01-22 오후 1:51:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sw_card_config](
	[sw_card_config_id] [int] IDENTITY(1,1) NOT NULL,
	[sw_asset_id] [int] NOT NULL,
	[slot_no] [int] NOT NULL,
	[sw_card_asset_id] [int] NULL,
 CONSTRAINT [PK_asset_sw] PRIMARY KEY CLUSTERED 
(
	[sw_card_config_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[template]    Script Date: 2015-01-22 오후 1:51:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[template](
	[template_id] [int] IDENTITY(19130001,1) NOT NULL,
	[report_id] [int] NOT NULL,
	[template_name] [varchar](80) NOT NULL,
	[num_of_template_column] [int] NOT NULL,
	[user_id] [int] NOT NULL,
	[last_updated] [timestamp] NOT NULL,
	[remarks] [varchar](80) NULL,
	[last_updated2] [datetime] NOT NULL,
 CONSTRAINT [PK_template] PRIMARY KEY CLUSTERED 
(
	[template_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[template_column]    Script Date: 2015-01-22 오후 1:51:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[template_column](
	[template_column_id] [int] IDENTITY(1,1) NOT NULL,
	[template_id] [int] NOT NULL,
	[template_column_no] [int] NOT NULL,
	[report_column_no] [int] NOT NULL,
	[column_width] [int] NULL,
 CONSTRAINT [PK_template_column] PRIMARY KEY CLUSTERED 
(
	[template_column_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[terminal_data]    Script Date: 2015-01-22 오후 1:51:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[terminal_data](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[location_id] [int] NOT NULL,
	[date] [date] NOT NULL,
	[act_terminal] [int] NOT NULL,
	[tot_terminal] [int] NOT NULL,
	[last_updated] [datetime] NOT NULL,
 CONSTRAINT [PK_terminal_data] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[terminal_data_hour]    Script Date: 2015-01-22 오후 1:51:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[terminal_data_hour](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[site_id] [int] NOT NULL,
	[date] [date] NOT NULL,
	[time_0_23] [int] NOT NULL,
	[act_terminal] [int] NOT NULL,
	[tot_terminal] [int] NOT NULL,
	[last_updated] [datetime] NOT NULL,
 CONSTRAINT [PK_terminal_data_hour] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[update_func]    Script Date: 2015-01-22 오후 1:51:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[update_func](
	[update_func_id] [int] NOT NULL,
	[update_func_desc] [varchar](80) NOT NULL,
	[invoked_cnt] [int] NOT NULL,
	[last_updated] [timestamp] NOT NULL,
 CONSTRAINT [PK_update_func] PRIMARY KEY CLUSTERED 
(
	[update_func_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[user]    Script Date: 2015-01-22 오후 1:51:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[user](
	[user_id] [int] IDENTITY(99000001,1) NOT NULL,
	[user_group] [char](1) NOT NULL,
	[user_name] [varchar](40) NOT NULL,
	[login_id] [varchar](20) NOT NULL,
	[login_password] [varchar](256) NOT NULL,
	[email] [varchar](80) NULL,
	[use_email] [char](1) NOT NULL,
	[phone] [varchar](30) NULL,
	[mobile] [varchar](30) NULL,
	[use_sms] [char](1) NOT NULL,
	[reg_user_id] [int] NOT NULL,
	[last_updated] [timestamp] NOT NULL,
	[deletable] [char](1) NOT NULL,
	[remarks] [varchar](80) NULL,
	[last_updated2] [datetime] NOT NULL,
 CONSTRAINT [PK_user] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[user_port_layout]    Script Date: 2015-01-22 오후 1:51:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[user_port_layout](
	[user_port_layout_id] [int] IDENTITY(1,1) NOT NULL,
	[asset_id] [int] NOT NULL,
	[port_no] [int] NOT NULL,
	[is_layout] [char](1) NOT NULL,
	[pos_x] [int] NULL,
	[pos_y] [int] NULL,
	[last_updated] [timestamp] NOT NULL,
 CONSTRAINT [PK_asset_user_port] PRIMARY KEY CLUSTERED 
(
	[user_port_layout_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[work_order]    Script Date: 2015-01-22 오후 1:51:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[work_order](
	[wo_id] [int] IDENTITY(19140001,1) NOT NULL,
	[wo_name] [varchar](80) NOT NULL,
	[pp_asset_id] [int] NOT NULL,
	[wo_xc_connect_type] [char](1) NOT NULL,
	[wo_result] [char](1) NOT NULL,
	[reg_user_id] [int] NOT NULL,
	[process_user_id] [int] NOT NULL,
	[remarks] [varchar](80) NULL,
	[write_date] [datetime] NOT NULL,
	[reserve_flag] [char](1) NOT NULL,
	[reserved_date] [datetime] NULL,
	[tot_task_cnt] [int] NOT NULL,
 CONSTRAINT [PK_work_order] PRIMARY KEY CLUSTERED 
(
	[wo_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[work_order_task]    Script Date: 2015-01-22 오후 1:51:42 ******/
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
 CONSTRAINT [PK_work_order_task] PRIMARY KEY CLUSTERED 
(
	[work_order_task_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[asset] ADD  CONSTRAINT [DF_asset_3]  DEFAULT (getdate()) FOR [install_date]
GO
ALTER TABLE [dbo].[asset] ADD  CONSTRAINT [DF_asset_1]  DEFAULT ((90001)) FOR [user_id]
GO
ALTER TABLE [dbo].[asset] ADD  CONSTRAINT [DF_asset_5]  DEFAULT ('N') FOR [is_layout]
GO
ALTER TABLE [dbo].[asset] ADD  CONSTRAINT [DF_asset_last_updated2]  DEFAULT (getdate()) FOR [last_updated]
GO
ALTER TABLE [dbo].[asset_aux] ADD  CONSTRAINT [DF_asset_aux_1]  DEFAULT ('2') FOR [snmp_version]
GO
ALTER TABLE [dbo].[asset_ext] ADD  CONSTRAINT [DF_asset_ext_1]  DEFAULT ((90001)) FOR [user_id]
GO
ALTER TABLE [dbo].[asset_ipp_port_link] ADD  CONSTRAINT [DF_asset_ipp_port_link_1]  DEFAULT ('-') FOR [alarm_status]
GO
ALTER TABLE [dbo].[asset_ipp_port_link] ADD  CONSTRAINT [DF_asset_ipp_port_link_2]  DEFAULT ('-') FOR [wo_status]
GO
ALTER TABLE [dbo].[asset_ipp_port_link] ADD  CONSTRAINT [DF_asset_ipp_port_link_3]  DEFAULT ('-') FOR [is_port_trace]
GO
ALTER TABLE [dbo].[asset_ipp_port_link] ADD  CONSTRAINT [DF_asset_ipp_port_link_4]  DEFAULT ('-') FOR [ipp_port_status]
GO
ALTER TABLE [dbo].[asset_terminal] ADD  CONSTRAINT [DF_asset_terminal_4]  DEFAULT ('N') FOR [cur_enable]
GO
ALTER TABLE [dbo].[asset_terminal] ADD  CONSTRAINT [DF_asset_terminal_5]  DEFAULT ('N') FOR [terminal_status]
GO
ALTER TABLE [dbo].[asset_terminal] ADD  CONSTRAINT [DF_asset_terminal_6]  DEFAULT ('Y') FOR [new_enable]
GO
ALTER TABLE [dbo].[asset_terminal] ADD  CONSTRAINT [DF_asset_terminal_1]  DEFAULT (getdate()) FOR [last_activated]
GO
ALTER TABLE [dbo].[asset_terminal] ADD  CONSTRAINT [DF_asset_terminal_terminal_grant]  DEFAULT ((1)) FOR [terminal_grant]
GO
ALTER TABLE [dbo].[asset_tree] ADD  CONSTRAINT [DF_asset_tree_1]  DEFAULT ('-') FOR [is_alarm]
GO
ALTER TABLE [dbo].[building] ADD  CONSTRAINT [DF_building_1]  DEFAULT ((90001)) FOR [user_id]
GO
ALTER TABLE [dbo].[catalog] ADD  CONSTRAINT [DF_catalog_1]  DEFAULT ('Y') FOR [deletable]
GO
ALTER TABLE [dbo].[catalog] ADD  CONSTRAINT [DF_catalog_2]  DEFAULT ((90001)) FOR [user_id]
GO
ALTER TABLE [dbo].[catalog] ADD  CONSTRAINT [DF_catalog_last_updated2]  DEFAULT (getdate()) FOR [last_updated]
GO
ALTER TABLE [dbo].[catalog_ext] ADD  CONSTRAINT [DF_catalog_ext_1]  DEFAULT ((90001)) FOR [user_id]
GO
ALTER TABLE [dbo].[catalog_group] ADD  CONSTRAINT [DF_catalog_group_1]  DEFAULT ('Y') FOR [enable]
GO
ALTER TABLE [dbo].[catalog_group] ADD  CONSTRAINT [DF_catalog_group_2]  DEFAULT ('Y') FOR [deletable]
GO
ALTER TABLE [dbo].[catalog_group] ADD  CONSTRAINT [DF_catalog_group_3]  DEFAULT ((0)) FOR [level1_catalog_group_id]
GO
ALTER TABLE [dbo].[catalog_group] ADD  CONSTRAINT [DF_catalog_group_4]  DEFAULT ((0)) FOR [level2_catalog_group_id]
GO
ALTER TABLE [dbo].[event] ADD  CONSTRAINT [DF_event_1]  DEFAULT ('N') FOR [popup_screen]
GO
ALTER TABLE [dbo].[event] ADD  CONSTRAINT [DF_event_2]  DEFAULT ('N') FOR [send_email]
GO
ALTER TABLE [dbo].[event] ADD  CONSTRAINT [DF_event_3]  DEFAULT ('N') FOR [send_sms]
GO
ALTER TABLE [dbo].[event_hist] ADD  CONSTRAINT [DF_event_hist_1]  DEFAULT ('N') FOR [is_confirm]
GO
ALTER TABLE [dbo].[event_hist] ADD  CONSTRAINT [DF_event_hist_write_time]  DEFAULT (getdate()) FOR [write_time]
GO
ALTER TABLE [dbo].[ext_property] ADD  CONSTRAINT [DF_ext_property_2]  DEFAULT ((0)) FOR [num_of_ans]
GO
ALTER TABLE [dbo].[ext_property] ADD  CONSTRAINT [DF_ext_property_1]  DEFAULT ((90001)) FOR [user_id]
GO
ALTER TABLE [dbo].[ext_property_ans] ADD  CONSTRAINT [DF_ext_property_ans_1]  DEFAULT ((90001)) FOR [user_id]
GO
ALTER TABLE [dbo].[favorite_tree] ADD  CONSTRAINT [DF_favorite_tree_1]  DEFAULT ('-') FOR [is_alarm]
GO
ALTER TABLE [dbo].[floor] ADD  CONSTRAINT [DF_floor_1]  DEFAULT ((90001)) FOR [user_id]
GO
ALTER TABLE [dbo].[fw_upgrade] ADD  CONSTRAINT [DF_fw_upgrade_1]  DEFAULT ((90001)) FOR [user_id]
GO
ALTER TABLE [dbo].[image] ADD  CONSTRAINT [DF_image_1]  DEFAULT ('Y') FOR [deletable]
GO
ALTER TABLE [dbo].[ipp_connect_status] ADD  CONSTRAINT [DF_asset_ipp_1]  DEFAULT ('-') FOR [connect_status]
GO
ALTER TABLE [dbo].[net_scan] ADD  CONSTRAINT [DF_net_scan_1]  DEFAULT ((90001)) FOR [user_id]
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
ALTER TABLE [dbo].[rack] ADD  CONSTRAINT [DF_rack_1]  DEFAULT ((0)) FOR [angle]
GO
ALTER TABLE [dbo].[rack] ADD  CONSTRAINT [DF_rack_2]  DEFAULT ((90001)) FOR [user_id]
GO
ALTER TABLE [dbo].[rack_config] ADD  CONSTRAINT [DF_rack_mount_1]  DEFAULT ('S') FOR [rack_mount_type]
GO
ALTER TABLE [dbo].[region1] ADD  CONSTRAINT [DF_region1_1]  DEFAULT ((90001)) FOR [user_id]
GO
ALTER TABLE [dbo].[region2] ADD  CONSTRAINT [DF_region2_1]  DEFAULT ((90001)) FOR [user_id]
GO
ALTER TABLE [dbo].[room] ADD  CONSTRAINT [DF_room_1]  DEFAULT ((90001)) FOR [user_id]
GO
ALTER TABLE [dbo].[site] ADD  CONSTRAINT [DF_site_1]  DEFAULT ((90001)) FOR [user_id]
GO
ALTER TABLE [dbo].[site_user] ADD  CONSTRAINT [DF_site_user_2]  DEFAULT ('U') FOR [user_right]
GO
ALTER TABLE [dbo].[site_user] ADD  CONSTRAINT [DF_site_user_1]  DEFAULT ((90001)) FOR [reg_user_id]
GO
ALTER TABLE [dbo].[template] ADD  CONSTRAINT [DF_template_1]  DEFAULT ((90001)) FOR [user_id]
GO
ALTER TABLE [dbo].[template] ADD  CONSTRAINT [DF_template_last_updated2]  DEFAULT (getdate()) FOR [last_updated2]
GO
ALTER TABLE [dbo].[user] ADD  CONSTRAINT [DF_user_3]  DEFAULT ('N') FOR [use_email]
GO
ALTER TABLE [dbo].[user] ADD  CONSTRAINT [DF_user_4]  DEFAULT ('N') FOR [use_sms]
GO
ALTER TABLE [dbo].[user] ADD  CONSTRAINT [DF_user_1]  DEFAULT ((90001)) FOR [reg_user_id]
GO
ALTER TABLE [dbo].[user] ADD  CONSTRAINT [DF_user_2]  DEFAULT ('Y') FOR [deletable]
GO
ALTER TABLE [dbo].[user] ADD  CONSTRAINT [DF_user_last_updated2]  DEFAULT (getdate()) FOR [last_updated2]
GO
ALTER TABLE [dbo].[user_port_layout] ADD  CONSTRAINT [DF_asset_user_port_1]  DEFAULT ('N') FOR [is_layout]
GO
ALTER TABLE [dbo].[work_order] ADD  CONSTRAINT [DF_work_order_4]  DEFAULT ('V') FOR [wo_xc_connect_type]
GO
ALTER TABLE [dbo].[work_order] ADD  CONSTRAINT [DF_work_order_3]  DEFAULT ('R') FOR [wo_result]
GO
ALTER TABLE [dbo].[work_order] ADD  CONSTRAINT [DF_work_order_1]  DEFAULT ((90001)) FOR [reg_user_id]
GO
ALTER TABLE [dbo].[work_order] ADD  CONSTRAINT [DF_work_order_2]  DEFAULT ((90001)) FOR [process_user_id]
GO
ALTER TABLE [dbo].[work_order_task] ADD  CONSTRAINT [DF_work_order_task_2]  DEFAULT ('P') FOR [remote_asset_type]
GO
ALTER TABLE [dbo].[work_order_task] ADD  CONSTRAINT [DF_work_order_task_1]  DEFAULT ('R') FOR [task_result]
GO
ALTER TABLE [dbo].[asset_aux]  WITH CHECK ADD  CONSTRAINT [FK_asset_aux_asset] FOREIGN KEY([asset_id])
REFERENCES [dbo].[asset] ([asset_id])
GO
ALTER TABLE [dbo].[asset_aux] CHECK CONSTRAINT [FK_asset_aux_asset]
GO
ALTER TABLE [dbo].[asset_ext]  WITH CHECK ADD  CONSTRAINT [FK_asset_ext_asset] FOREIGN KEY([asset_id])
REFERENCES [dbo].[asset] ([asset_id])
GO
ALTER TABLE [dbo].[asset_ext] CHECK CONSTRAINT [FK_asset_ext_asset]
GO
ALTER TABLE [dbo].[asset_ipp_port_link]  WITH CHECK ADD  CONSTRAINT [FK_asset_ipp_port_link_asset] FOREIGN KEY([ipp_asset_id])
REFERENCES [dbo].[asset] ([asset_id])
GO
ALTER TABLE [dbo].[asset_ipp_port_link] CHECK CONSTRAINT [FK_asset_ipp_port_link_asset]
GO
ALTER TABLE [dbo].[asset_port_link]  WITH CHECK ADD  CONSTRAINT [FK_asset_port_link_asset] FOREIGN KEY([asset_id])
REFERENCES [dbo].[asset] ([asset_id])
GO
ALTER TABLE [dbo].[asset_port_link] CHECK CONSTRAINT [FK_asset_port_link_asset]
GO
ALTER TABLE [dbo].[asset_terminal_ip]  WITH CHECK ADD  CONSTRAINT [FK_asset_terminal_ip_asset_terminal] FOREIGN KEY([terminal_id])
REFERENCES [dbo].[asset_terminal] ([terminal_id])
GO
ALTER TABLE [dbo].[asset_terminal_ip] CHECK CONSTRAINT [FK_asset_terminal_ip_asset_terminal]
GO
ALTER TABLE [dbo].[building]  WITH CHECK ADD  CONSTRAINT [FK_building_site] FOREIGN KEY([site_id])
REFERENCES [dbo].[site] ([site_id])
GO
ALTER TABLE [dbo].[building] CHECK CONSTRAINT [FK_building_site]
GO
ALTER TABLE [dbo].[catalog]  WITH CHECK ADD  CONSTRAINT [FK_catalog_catalog_group] FOREIGN KEY([catalog_group_id])
REFERENCES [dbo].[catalog_group] ([catalog_group_id])
GO
ALTER TABLE [dbo].[catalog] CHECK CONSTRAINT [FK_catalog_catalog_group]
GO
ALTER TABLE [dbo].[catalog]  WITH CHECK ADD  CONSTRAINT [FK_catalog_manufacture] FOREIGN KEY([manufacture_id])
REFERENCES [dbo].[manufacture] ([manufacture_id])
GO
ALTER TABLE [dbo].[catalog] CHECK CONSTRAINT [FK_catalog_manufacture]
GO
ALTER TABLE [dbo].[catalog_ext]  WITH CHECK ADD  CONSTRAINT [FK_catalog_ext_catalog] FOREIGN KEY([catalog_id])
REFERENCES [dbo].[catalog] ([catalog_id])
GO
ALTER TABLE [dbo].[catalog_ext] CHECK CONSTRAINT [FK_catalog_ext_catalog]
GO
ALTER TABLE [dbo].[catalog_ext]  WITH CHECK ADD  CONSTRAINT [FK_catalog_ext_ext_property] FOREIGN KEY([ext_id])
REFERENCES [dbo].[ext_property] ([ext_id])
GO
ALTER TABLE [dbo].[catalog_ext] CHECK CONSTRAINT [FK_catalog_ext_ext_property]
GO
ALTER TABLE [dbo].[changed_link_hist_cell]  WITH CHECK ADD  CONSTRAINT [FK_changed_link_hist_cell_changed_link_hist] FOREIGN KEY([changed_link_id])
REFERENCES [dbo].[changed_link_hist] ([changed_link_id])
GO
ALTER TABLE [dbo].[changed_link_hist_cell] CHECK CONSTRAINT [FK_changed_link_hist_cell_changed_link_hist]
GO
ALTER TABLE [dbo].[contact]  WITH CHECK ADD  CONSTRAINT [FK_contact_manufacture] FOREIGN KEY([manufacture_id])
REFERENCES [dbo].[manufacture] ([manufacture_id])
GO
ALTER TABLE [dbo].[contact] CHECK CONSTRAINT [FK_contact_manufacture]
GO
ALTER TABLE [dbo].[event_lang]  WITH CHECK ADD  CONSTRAINT [FK_event_lang_event] FOREIGN KEY([event_id])
REFERENCES [dbo].[event] ([event_id])
GO
ALTER TABLE [dbo].[event_lang] CHECK CONSTRAINT [FK_event_lang_event]
GO
ALTER TABLE [dbo].[event_lang]  WITH CHECK ADD  CONSTRAINT [FK_event_lang_language] FOREIGN KEY([lang_id])
REFERENCES [dbo].[language] ([lang_id])
GO
ALTER TABLE [dbo].[event_lang] CHECK CONSTRAINT [FK_event_lang_language]
GO
ALTER TABLE [dbo].[ext_property_ans]  WITH CHECK ADD  CONSTRAINT [FK_ext_property_ans_ext_property] FOREIGN KEY([ext_id])
REFERENCES [dbo].[ext_property] ([ext_id])
GO
ALTER TABLE [dbo].[ext_property_ans] CHECK CONSTRAINT [FK_ext_property_ans_ext_property]
GO
ALTER TABLE [dbo].[floor]  WITH CHECK ADD  CONSTRAINT [FK_floor_building] FOREIGN KEY([building_id])
REFERENCES [dbo].[building] ([building_id])
GO
ALTER TABLE [dbo].[floor] CHECK CONSTRAINT [FK_floor_building]
GO
ALTER TABLE [dbo].[fw_upgrade_hist]  WITH CHECK ADD  CONSTRAINT [FK_fw_upgrade_hist_fw_upgrade] FOREIGN KEY([fw_id])
REFERENCES [dbo].[fw_upgrade] ([fw_id])
GO
ALTER TABLE [dbo].[fw_upgrade_hist] CHECK CONSTRAINT [FK_fw_upgrade_hist_fw_upgrade]
GO
ALTER TABLE [dbo].[ic_connect_status]  WITH CHECK ADD  CONSTRAINT [FK_ic_connect_status_asset] FOREIGN KEY([ic_asset_id])
REFERENCES [dbo].[asset] ([asset_id])
GO
ALTER TABLE [dbo].[ic_connect_status] CHECK CONSTRAINT [FK_ic_connect_status_asset]
GO
ALTER TABLE [dbo].[ic_ipp_config]  WITH CHECK ADD  CONSTRAINT [FK_asset_ic_asset] FOREIGN KEY([ic_asset_id])
REFERENCES [dbo].[asset] ([asset_id])
GO
ALTER TABLE [dbo].[ic_ipp_config] CHECK CONSTRAINT [FK_asset_ic_asset]
GO
ALTER TABLE [dbo].[image]  WITH CHECK ADD  CONSTRAINT [FK_image_drawing_3d] FOREIGN KEY([drawing_3d_id])
REFERENCES [dbo].[drawing_3d] ([drawing_3d_id])
GO
ALTER TABLE [dbo].[image] CHECK CONSTRAINT [FK_image_drawing_3d]
GO
ALTER TABLE [dbo].[image]  WITH CHECK ADD  CONSTRAINT [FK_image_image_type] FOREIGN KEY([image_type_id])
REFERENCES [dbo].[image_type] ([image_type_id])
GO
ALTER TABLE [dbo].[image] CHECK CONSTRAINT [FK_image_image_type]
GO
ALTER TABLE [dbo].[ipp_connect_status]  WITH CHECK ADD  CONSTRAINT [FK_asset_ipp_asset] FOREIGN KEY([ipp_asset_id])
REFERENCES [dbo].[asset] ([asset_id])
GO
ALTER TABLE [dbo].[ipp_connect_status] CHECK CONSTRAINT [FK_asset_ipp_asset]
GO
ALTER TABLE [dbo].[net_scan_sw]  WITH CHECK ADD  CONSTRAINT [FK_net_scan_sw_net_scan] FOREIGN KEY([net_id])
REFERENCES [dbo].[net_scan] ([net_id])
GO
ALTER TABLE [dbo].[net_scan_sw] CHECK CONSTRAINT [FK_net_scan_sw_net_scan]
GO
ALTER TABLE [dbo].[rack]  WITH CHECK ADD  CONSTRAINT [FK_rack_room] FOREIGN KEY([room_id])
REFERENCES [dbo].[room] ([room_id])
GO
ALTER TABLE [dbo].[rack] CHECK CONSTRAINT [FK_rack_room]
GO
ALTER TABLE [dbo].[rack_config]  WITH CHECK ADD  CONSTRAINT [FK_rack_mount_rack] FOREIGN KEY([rack_id])
REFERENCES [dbo].[rack] ([rack_id])
GO
ALTER TABLE [dbo].[rack_config] CHECK CONSTRAINT [FK_rack_mount_rack]
GO
ALTER TABLE [dbo].[region2]  WITH CHECK ADD  CONSTRAINT [FK_region2_region1] FOREIGN KEY([region1_id])
REFERENCES [dbo].[region1] ([region1_id])
GO
ALTER TABLE [dbo].[region2] CHECK CONSTRAINT [FK_region2_region1]
GO
ALTER TABLE [dbo].[report_lang]  WITH CHECK ADD  CONSTRAINT [FK_report_lang_language] FOREIGN KEY([lang_id])
REFERENCES [dbo].[language] ([lang_id])
GO
ALTER TABLE [dbo].[report_lang] CHECK CONSTRAINT [FK_report_lang_language]
GO
ALTER TABLE [dbo].[report_lang]  WITH CHECK ADD  CONSTRAINT [FK_report_lang_report] FOREIGN KEY([report_id])
REFERENCES [dbo].[report] ([report_id])
GO
ALTER TABLE [dbo].[report_lang] CHECK CONSTRAINT [FK_report_lang_report]
GO
ALTER TABLE [dbo].[report_lang_column]  WITH CHECK ADD  CONSTRAINT [FK_report_lang_column_lang] FOREIGN KEY([lang_id])
REFERENCES [dbo].[language] ([lang_id])
GO
ALTER TABLE [dbo].[report_lang_column] CHECK CONSTRAINT [FK_report_lang_column_lang]
GO
ALTER TABLE [dbo].[report_lang_column]  WITH CHECK ADD  CONSTRAINT [FK_report_lang_column_report] FOREIGN KEY([report_id])
REFERENCES [dbo].[report] ([report_id])
GO
ALTER TABLE [dbo].[report_lang_column] CHECK CONSTRAINT [FK_report_lang_column_report]
GO
ALTER TABLE [dbo].[room]  WITH CHECK ADD  CONSTRAINT [FK_room_floor] FOREIGN KEY([floor_id])
REFERENCES [dbo].[floor] ([floor_id])
GO
ALTER TABLE [dbo].[room] CHECK CONSTRAINT [FK_room_floor]
GO
ALTER TABLE [dbo].[site]  WITH CHECK ADD  CONSTRAINT [FK_site_region2] FOREIGN KEY([region2_id])
REFERENCES [dbo].[region2] ([region2_id])
GO
ALTER TABLE [dbo].[site] CHECK CONSTRAINT [FK_site_region2]
GO
ALTER TABLE [dbo].[site_user]  WITH CHECK ADD  CONSTRAINT [FK_site_user_site] FOREIGN KEY([site_id])
REFERENCES [dbo].[site] ([site_id])
GO
ALTER TABLE [dbo].[site_user] CHECK CONSTRAINT [FK_site_user_site]
GO
ALTER TABLE [dbo].[site_user]  WITH CHECK ADD  CONSTRAINT [FK_site_user_user] FOREIGN KEY([user_id])
REFERENCES [dbo].[user] ([user_id])
GO
ALTER TABLE [dbo].[site_user] CHECK CONSTRAINT [FK_site_user_user]
GO
ALTER TABLE [dbo].[sw_card_config]  WITH CHECK ADD  CONSTRAINT [FK_asset_sw_asset] FOREIGN KEY([sw_asset_id])
REFERENCES [dbo].[asset] ([asset_id])
GO
ALTER TABLE [dbo].[sw_card_config] CHECK CONSTRAINT [FK_asset_sw_asset]
GO
ALTER TABLE [dbo].[template]  WITH CHECK ADD  CONSTRAINT [FK_template_report] FOREIGN KEY([report_id])
REFERENCES [dbo].[report] ([report_id])
GO
ALTER TABLE [dbo].[template] CHECK CONSTRAINT [FK_template_report]
GO
ALTER TABLE [dbo].[template_column]  WITH CHECK ADD  CONSTRAINT [FK_template_column_template] FOREIGN KEY([template_id])
REFERENCES [dbo].[template] ([template_id])
GO
ALTER TABLE [dbo].[template_column] CHECK CONSTRAINT [FK_template_column_template]
GO
ALTER TABLE [dbo].[user_port_layout]  WITH CHECK ADD  CONSTRAINT [FK_asset_user_port_asset] FOREIGN KEY([asset_id])
REFERENCES [dbo].[asset] ([asset_id])
GO
ALTER TABLE [dbo].[user_port_layout] CHECK CONSTRAINT [FK_asset_user_port_asset]
GO
ALTER TABLE [dbo].[work_order_task]  WITH CHECK ADD  CONSTRAINT [FK_work_order_task_work_order] FOREIGN KEY([wo_id])
REFERENCES [dbo].[work_order] ([wo_id])
GO
ALTER TABLE [dbo].[work_order_task] CHECK CONSTRAINT [FK_work_order_task_work_order]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'자산 도면 배치 여부: Y=Yes, N=No' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'asset', @level2type=N'COLUMN',@level2name=N'is_layout'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'자산 도면 배치 좌표 X' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'asset', @level2type=N'COLUMN',@level2name=N'pos_x'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'자산 도면 배치 좌표 Y' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'asset', @level2type=N'COLUMN',@level2name=N'pos_y'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'자산' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'asset'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'유지 보수 담당 부서' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'asset_aux', @level2type=N'COLUMN',@level2name=N'as_management_div'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'유지 보수 담당자' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'asset_aux', @level2type=N'COLUMN',@level2name=N'as_management_user_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'무상유지보수시작일' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'asset_aux', @level2type=N'COLUMN',@level2name=N'as_free_start_date'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'무상유지보수기간(개월)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'asset_aux', @level2type=N'COLUMN',@level2name=N'as_free_duration'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'무상유지보수종료일' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'asset_aux', @level2type=N'COLUMN',@level2name=N'as_free_end_date'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'유상유지보수시작일' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'asset_aux', @level2type=N'COLUMN',@level2name=N'as_start_date'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'유상유지보수기간(개월)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'asset_aux', @level2type=N'COLUMN',@level2name=N'as_duration'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'유상유지보수종료일' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'asset_aux', @level2type=N'COLUMN',@level2name=N'as_end_date'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'유상유지보수금액(계약기간)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'asset_aux', @level2type=N'COLUMN',@level2name=N'as_price'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'유지보수회사' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'asset_aux', @level2type=N'COLUMN',@level2name=N'as_company'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'구매일' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'asset_aux', @level2type=N'COLUMN',@level2name=N'bu_purchase_date'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'구매담당자' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'asset_aux', @level2type=N'COLUMN',@level2name=N'bu_purchase_user_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'감가상각시작년도' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'asset_aux', @level2type=N'COLUMN',@level2name=N'bu_depreciation_start_year'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'감가상각기간(년)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'asset_aux', @level2type=N'COLUMN',@level2name=N'bu_depreciation_duration'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'감가상각종료년도' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'asset_aux', @level2type=N'COLUMN',@level2name=N'bu_depreciation_end_year'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'SNMP 트랩 서버 IP(랙매니저 장치에서 사용)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'asset_aux', @level2type=N'COLUMN',@level2name=N'snmp_trap_svr_ip'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'SNMP 버전: 1=v1, 2=v2, 3=v3' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'asset_aux', @level2type=N'COLUMN',@level2name=N'snmp_version'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'추가 속성 타입이 S(String)인 경우 사용' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'asset_ext', @level2type=N'COLUMN',@level2name=N'ans_string'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'추가 속성 타입이 R:싱글선택1(라디오박스) 및 L:싱글선택2(리스트박스)인 경우 1부터 시작, 멀티선택형(체크박스)인 경우 비트연산으로 답변에 사용' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'asset_ext', @level2type=N'COLUMN',@level2name=N'ans_numeric'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'추가 속성 타입이 D(Date)인 경우 사용' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'asset_ext', @level2type=N'COLUMN',@level2name=N'ans_date'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'추가 속성 타입이 T(Time)인 경우 사용' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'asset_ext', @level2type=N'COLUMN',@level2name=N'ans_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'자산 추가 속성: 자산의 추가 속성에 대한 답변' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'asset_ext'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'알람 발생 여부: Y=Yes, N=No, -=모름' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'asset_ipp_port_link', @level2type=N'COLUMN',@level2name=N'alarm_status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'작업지시 발생 여부: Y=Yes, N=No, -=모름' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'asset_ipp_port_link', @level2type=N'COLUMN',@level2name=N'wo_status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'포트 트레이스 발생 여부: Y=Yes, N=No, -=모름' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'asset_ipp_port_link', @level2type=N'COLUMN',@level2name=N'is_port_trace'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'포트 상태: U=Unplugged, P=Plugged, L=Linked, -=모름' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'asset_ipp_port_link', @level2type=N'COLUMN',@level2name=N'ipp_port_status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'인텔리전트 패치 패널의 각 포트별 링크 상태' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'asset_ipp_port_link'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'전면 포트 상태: U=Unplugged, P=Plugged, L=Linked, -=모름' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'asset_port_link', @level2type=N'COLUMN',@level2name=N'front_plug_side'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'후면 포트 상태: U=Unplugged, P=Plugged, L=Linked, -=모름' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'asset_port_link', @level2type=N'COLUMN',@level2name=N'rear_plug_side'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'모든 자산의 포트 연결' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'asset_port_link'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'사용가능 플래그: Y=Yes(사용가능), N=No(사용불능), -=모름' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'asset_terminal', @level2type=N'COLUMN',@level2name=N'cur_enable'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'검출된 스위치' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'asset_terminal', @level2type=N'COLUMN',@level2name=N'cur_sw_asset_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'검출된 스위치 포트' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'asset_terminal', @level2type=N'COLUMN',@level2name=N'cur_sw_port_no'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Outlet 자산 ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'asset_terminal', @level2type=N'COLUMN',@level2name=N'cur_outlet_asset_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Outlet 포트' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'asset_terminal', @level2type=N'COLUMN',@level2name=N'cur_outlet_port_no'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'터미널 상태: Y=Yes(동작), N=No(비동작), -=모름' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'asset_terminal', @level2type=N'COLUMN',@level2name=N'terminal_status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'사용가능 플래그(new, 스케쥴러 검색종료 전 잠시 사용): Y=Yes(사용가능), N=No(사용불능), -=모름' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'asset_terminal', @level2type=N'COLUMN',@level2name=N'new_enable'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'최종 활성화 일시' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'asset_terminal', @level2type=N'COLUMN',@level2name=N'last_activated'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'자산 트리: 위치 + 각 자산으로 구성' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'asset_tree'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'내용이 길어서 속성 뷰에서만 표시' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'catalog', @level2type=N'COLUMN',@level2name=N'catalog_full_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'모델명' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'catalog', @level2type=N'COLUMN',@level2name=N'model_code'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'주문코드(파트번호)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'catalog', @level2type=N'COLUMN',@level2name=N'order_code'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'너비(Width, 단위:mm)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'catalog', @level2type=N'COLUMN',@level2name=N'size_w'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'깊이(Depth, 단위:mm)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'catalog', @level2type=N'COLUMN',@level2name=N'size_d'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'높이(Height, 단위:mm)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'catalog', @level2type=N'COLUMN',@level2name=N'size_h'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'무게(Weight, 단위:kg)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'catalog', @level2type=N'COLUMN',@level2name=N'weight'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'포트수, (링크가 하나라도 존재하면 1이상)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'catalog', @level2type=N'COLUMN',@level2name=N'num_of_ports'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'카탈로그(상품) 이미지로 사이즈는 무관' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'catalog', @level2type=N'COLUMN',@level2name=N'image_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'좌측 트리 및 3D 도면에 출력할 ICON 16x16 size' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'catalog', @level2type=N'COLUMN',@level2name=N'icon_16_image_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'좌측 트리 및 3D 도면에 출력할 ICON 32x32 size' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'catalog', @level2type=N'COLUMN',@level2name=N'icon_32_image_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'좌측 트리 및 3D 도면에 출력할 ICON 48x48 size' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'catalog', @level2type=N'COLUMN',@level2name=N'icon_48_image_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'좌측 트리 및 3D 도면에 출력할 ICON 64x64 size' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'catalog', @level2type=N'COLUMN',@level2name=N'icon_64_image_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'링크 다이어그램에 출력할 이미지 80x40 size' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'catalog', @level2type=N'COLUMN',@level2name=N'link_80_image_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Y=삭제 가능(사용자 추가항목), N=삭제 불가능(시스템 사용항목)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'catalog', @level2type=N'COLUMN',@level2name=N'deletable'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'패치 패널-타입: Y=지능형 패치/FDF, N=일반 패치/FDF, Null' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'catalog', @level2type=N'COLUMN',@level2name=N'pp_use_intelligent'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'패치 패널-연결 타입: X=Cross-connect, I=Inter-connect, Null' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'catalog', @level2type=N'COLUMN',@level2name=N'pp_config_type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'패치 패널-미디어 타입: U=UTP, F=Fiber, Null' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'catalog', @level2type=N'COLUMN',@level2name=N'pp_media_type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'패치 패널-UTP 잭 타입: E=Embedded jack, M=Modula jack, Null' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'catalog', @level2type=N'COLUMN',@level2name=N'pp_utp_jack_type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'패치 패널-UTP 쉴드 타입: N=None, S=Shield, Null' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'catalog', @level2type=N'COLUMN',@level2name=N'pp_utp_shield_type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'패치 패널-외형 타입: F=Flat, A=Angled, Null' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'catalog', @level2type=N'COLUMN',@level2name=N'pp_figure_type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'컨트롤러-패치 패널 커넥터 수(1~10), Null' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'catalog', @level2type=N'COLUMN',@level2name=N'ic_num_of_pp_connectors'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'컨트롤러-패치 패널 연결 가능 수(1~40), Null' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'catalog', @level2type=N'COLUMN',@level2name=N'ic_num_of_max_pp'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'컨트롤러-전원 장치 수(1~2), Null' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'catalog', @level2type=N'COLUMN',@level2name=N'ic_num_of_power'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'스토리지-최대 디스크 수, Null' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'catalog', @level2type=N'COLUMN',@level2name=N'st_num_of_disk'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'스위치-외형 타입: E=Embedded(일체형) type, S:Slot(새시형) type, C:Card Type, Null' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'catalog', @level2type=N'COLUMN',@level2name=N'sw_figure_type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'스위치-슬롯 수: 1~(새시형의 경우), 0=(임베디드형, 카드형), Null' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'catalog', @level2type=N'COLUMN',@level2name=N'sw_num_of_slots'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Consolidation Point-플러그 위치: N=None, R:Right(우측), B=Both(양쪽), Null' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'catalog', @level2type=N'COLUMN',@level2name=N'cp_plug_side'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Cable-연결 타입: F=Fixed(Permarnent Link, P=Patch Cord, Null' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'catalog', @level2type=N'COLUMN',@level2name=N'ca_install_type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Cable-군용 여부: Y=군용, N=민수용, Null' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'catalog', @level2type=N'COLUMN',@level2name=N'ca_for_army'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Cable-미디어 타입: U=UTP, F=Fiber, Null' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'catalog', @level2type=N'COLUMN',@level2name=N'ca_media_type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Cable-UTP 쉴드 여부: Y=쉴드, N=쉴드 없음, UTP, Null' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'catalog', @level2type=N'COLUMN',@level2name=N'ca_is_utp_shield'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Cable-UTP Cable 타입: -=Etc, 5=Cat. 5A=Cat. 5, 6=Cat. 6, 6A=Cat. 6a, 7=Cat. 7, 8=Cat. 8, Null' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'catalog', @level2type=N'COLUMN',@level2name=N'ca_utp_cable_type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Cable-Fiber Cable 타입: -=Etc, S0=OS. S1=OS-1. S2=OS-2, SP=PM Single Mode, M0=OM, M1=OM-1, M2=OM-2, M3=OM-3, M4=OM-4, Null' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'catalog', @level2type=N'COLUMN',@level2name=N'ca_fiber_cable_type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Cable-Fiber Connector 타입: -=Etc, FC, LC, SC, ST, MT:MT-RJ, MP=MPO, Null' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'catalog', @level2type=N'COLUMN',@level2name=N'ca_fiber_connector_type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Cable-케이블 색상으로 RGB 컬러 값, Null' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'catalog', @level2type=N'COLUMN',@level2name=N'ca_disp_color_rgb'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Cable-케이블 선택 메뉴에서 사용할 표시명, Null' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'catalog', @level2type=N'COLUMN',@level2name=N'ca_disp_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'랙마운트-랙마운트 제품 여부: Y=Yes, N=No, Null' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'catalog', @level2type=N'COLUMN',@level2name=N'rm_is_rack_mount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'랙마운트-슬롯형 제품 유닛 사이즈(U), Null' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'catalog', @level2type=N'COLUMN',@level2name=N'rm_unit_size'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'랙마운트-랙표시용 이미지 (222x20), Null' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'catalog', @level2type=N'COLUMN',@level2name=N'rm_image_220_image_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'랙마운트-랙표시용 이미지 (444x40), Null' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'catalog', @level2type=N'COLUMN',@level2name=N'rm_image_440_image_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'랙마운트-랙표시용 이미지 (888x80), Null' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'catalog', @level2type=N'COLUMN',@level2name=N'rm_image_880_image_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'카탈로그' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'catalog'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'카탈로그에 등록한 추가 속성' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'catalog_ext'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1=대분류, 2=중분류, 3=소분류' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'catalog_group', @level2type=N'COLUMN',@level2name=N'catalog_level'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Y=사용, N=비사용' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'catalog_group', @level2type=N'COLUMN',@level2name=N'enable'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Y=삭제 가능(사용자 추가항목), N=삭제 불가능(시스템 사용항목)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'catalog_group', @level2type=N'COLUMN',@level2name=N'deletable'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'카탈로그 그룹' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'catalog_group'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'단말에 대한 변동 내역(한 레코드가 한 개의 링크 다이어 그램을 의미)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'changed_link_hist'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'셀 번호는 1번부터 시작' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'changed_link_hist_cell', @level2type=N'COLUMN',@level2name=N'cell_pos'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'포트 상태(자산 및 케이블 상태에서도 동일하게 표현): U=Unplugged, P=Plugged, -=모름' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'changed_link_hist_cell', @level2type=N'COLUMN',@level2name=N'front_plug_status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'포트 상태(자산 및 케이블 상태에서도 동일하게 표현): U=Unplugged, P=Plugged, -=모름' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'changed_link_hist_cell', @level2type=N'COLUMN',@level2name=N'rear_plug_status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'단말에 대한 변경 내역을 횡으로 된 셀에 표시' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'changed_link_hist_cell'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'고정 데이터 시작 번호' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'code', @level2type=N'COLUMN',@level2name=N'fixed_code_no'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'유동 데이터 시작 번호' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'code', @level2type=N'COLUMN',@level2name=N'start_code_no'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'코드: 시스템에서 사용하지 않고 단순 참조용으로만 사용' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'code'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'직책' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'contact', @level2type=N'COLUMN',@level2name=N'duty'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'직위' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'contact', @level2type=N'COLUMN',@level2name=N'position'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'연락처' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'contact'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'3d 도면이 저장될 화일명(서버에 저장됨)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'drawing_3d', @level2type=N'COLUMN',@level2name=N'drawing_3d_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'각 층에 대한 평면도에 대응하는 3D 도면이 등록' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'drawing_3d'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'P=전력, S=온습도센서, D=도어센서' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'eb_port_config', @level2type=N'COLUMN',@level2name=N'port_type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Y=스레졸드에의한알람사용, N=알람없음' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'eb_port_config', @level2type=N'COLUMN',@level2name=N'alarm'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Y=door open, N=door close' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'eb_port_data_cur', @level2type=N'COLUMN',@level2name=N'door'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'이벤트 타입: I=Information, W=Warnning, E=Error' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'event', @level2type=N'COLUMN',@level2name=N'event_type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'이벤트 화면 팝업 기능 사용 여부, Y=Yes, N=No, Null' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'event', @level2type=N'COLUMN',@level2name=N'popup_screen'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'이메일 전송 기능 사용 여부, Y=Yes, N=No, Null' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'event', @level2type=N'COLUMN',@level2name=N'send_email'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'SMS 전송 기능 사용 여부, Y=Yes, N=No, Null' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'event', @level2type=N'COLUMN',@level2name=N'send_sms'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'이벤트' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'event'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'이벤트 타입: I=Information, W=Warnning, E=Error' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'event_hist', @level2type=N'COLUMN',@level2name=N'event_type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'이벤트 내용(로그)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'event_hist', @level2type=N'COLUMN',@level2name=N'event_text'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'이벤트 발생 자산 ID(무결성 연결 없음)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'event_hist', @level2type=N'COLUMN',@level2name=N'asset_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'이벤트 발생 포트(무결성 연결 없음)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'event_hist', @level2type=N'COLUMN',@level2name=N'port_no'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'확인여부: Y=Yes, N=No' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'event_hist', @level2type=N'COLUMN',@level2name=N'is_confirm'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'확인자, Null' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'event_hist', @level2type=N'COLUMN',@level2name=N'confirm_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'이벤트그룹: 이벤트들의 공통 그룹을 묶는다.(현재 출력목적으로만 사용)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'event_lang', @level2type=N'COLUMN',@level2name=N'event_group'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'이벤트포맷: 이벤트를 출력할 내용을 조합하기 위해 사용. {0}-> 인수1, {1}->인수2번' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'event_lang', @level2type=N'COLUMN',@level2name=N'event_format'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'이벤트를 각 언어별로 등록' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'event_lang'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'추가 속성 길이' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ext_property', @level2type=N'COLUMN',@level2name=N'ext_length'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'S:문자열, N:숫자, D:날짜, T:시간, R:싱글선택1(라디오박스), L:싱글선택2(리스트박스), C:멀티선택(체크박스)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ext_property', @level2type=N'COLUMN',@level2name=N'ext_type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'선택형의 경우 답안 갯 수, 그 외는 0' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ext_property', @level2type=N'COLUMN',@level2name=N'num_of_ans'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'추가 속성' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ext_property'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'답안 순번으로 1부터 시작' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ext_property_ans', @level2type=N'COLUMN',@level2name=N'ans_no'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'답안명' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ext_property_ans', @level2type=N'COLUMN',@level2name=N'ans_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'추가 속성에 대한 선택형 답' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ext_property_ans'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'트리에 출력할 내용(위치 명 or 자산 명)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'favorite_tree', @level2type=N'COLUMN',@level2name=N'disp_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0=가장 좌측부터' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'favorite_tree', @level2type=N'COLUMN',@level2name=N'disp_level'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Y=알람 출력을 해야 하는 경우, N=알람 없음' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'favorite_tree', @level2type=N'COLUMN',@level2name=N'is_alarm'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'출력 내용이 자산인 경우, 위치인 경우 NULL' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'favorite_tree', @level2type=N'COLUMN',@level2name=N'asset_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'즐겨찾기 트리: 필요 여부는 추가 검토 필요' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'favorite_tree'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'층번호로 지하층은 음수, 0은 로비층, 정수는 지상층' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'floor', @level2type=N'COLUMN',@level2name=N'floor_no'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'3D 도면 ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'floor', @level2type=N'COLUMN',@level2name=N'drawing_3d_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'펌웨어 업그레이드: 펌웨어 등록' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fw_upgrade'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'펌웨어 기록 결과: P=Progressing(진행중), S=Successed(기록성공), F:Failed(기록실패), C=Canceled(기록취소)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fw_upgrade_hist', @level2type=N'COLUMN',@level2name=N'result'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'펌웨어 업그레이드 내역' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fw_upgrade_hist'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'컨트롤러 연결 상태: Y=정상연결, N=연결안됨, -=모름' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ic_connect_status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'컨트롤러에 연결 구성될 패치 패널로 연결 번호는 1부터 최대 40까지' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ic_ipp_config', @level2type=N'COLUMN',@level2name=N'ipp_connect_no'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'컨트롤러와 인텔리전트 패치 패널 구성' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ic_ipp_config'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'01=map image, 02=site image, 03=building image, 04=drawing(floor) image, 05=catalog image, 06=rack-mount image(222x20), 07=rack-mount image(444x40), 08=rack-mount image(888x80), 09=rack-mount image(222x20), 09=link diagram image(80x40), 10=etc image, 11=icon(16x16), 12=icon(32x32), 13=icon(48x48), 14=icon(64x64)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'image', @level2type=N'COLUMN',@level2name=N'image_type_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'이미지의 가로 픽셀 수(서버가 업로드된 화일을 분석하여 입력)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'image', @level2type=N'COLUMN',@level2name=N'size_x'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'이미지의 가로 픽셀 수(서버가 업로드된 화일을 분석하여 입력)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'image', @level2type=N'COLUMN',@level2name=N'size_y'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'이미지 타입이 4번(평면도)인 경우 drawing_3d_id 테이블을 가리키는 ID가 입력되며, 도면에 대해 3차원 도면을 아직 준비하지 않는 경우 NULL' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'image', @level2type=N'COLUMN',@level2name=N'drawing_3d_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'이미지' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'image'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'01=map image, 02=site image, 03=building image, 04=drawing(floor) image, 05=catalog image, 06=rack-mount image(222x20), 07=rack-mount image(444x40), 08=rack-mount image(888x80), 09=rack-mount image(222x20), 09=link diagram image(80x40), 10=etc image, 11=icon(16x16), 12=icon(32x32), 13=icon(48x48), 14=icon(64x64)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'image_type', @level2type=N'COLUMN',@level2name=N'image_type_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'서버에 저장할 상대폴더명(full path 아님)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'image_type', @level2type=N'COLUMN',@level2name=N'folder_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'이미지의 가로 픽셀 수, 0인경우 사이즈 크기 제한 없음' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'image_type', @level2type=N'COLUMN',@level2name=N'size_x'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'이미지의 세로 픽셀 수, 0인경우 사이즈 크기 제한 없음' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'image_type', @level2type=N'COLUMN',@level2name=N'size_y'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'이미지 타입(14가지)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'image_type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'인텔리전트 패치 패널이 연결된 컨트롤러 자산 ID, 연결 구성이 되지 않았거나 일반 패치 패널의 경우 NULL' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ipp_connect_status', @level2type=N'COLUMN',@level2name=N'ic_asset_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'C=Connect, D=Disconnect, -=모름' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ipp_connect_status', @level2type=N'COLUMN',@level2name=N'connect_status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'인텔리전트 패치 패널 연결 상태' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ipp_connect_status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'가장 좌측이 0부터 시작' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'location', @level2type=N'COLUMN',@level2name=N'location_level'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'위치 명이 사이트부터 랙단위 까지 표시된다. (예: ABC Site -> AAA Building, ER -> Rack A001' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'location', @level2type=N'COLUMN',@level2name=N'location_path'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'위치: 지역1 부터 랙까지의 구성 정보를 모두 담음' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'location'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'제조사' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'manufacture'
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
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'스케쥴 시작 예약일, NULL=당장.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'net_scan_scheduler', @level2type=N'COLUMN',@level2name=N'schedule_time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'반복 패턴: E=Everyday, D:Dedicate Days(일월화수목금토 선택).' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'net_scan_scheduler', @level2type=N'COLUMN',@level2name=N'repeat_pattern'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'일요일, Y=Yes, N=No' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'net_scan_scheduler', @level2type=N'COLUMN',@level2name=N'repeat_day0'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'단말 검색을 위한 구성' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'net_scan_scheduler'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'데이터센터에 랙 열 번호가 필요한 경우 A~Z까지 사용. 필요없는 경우 NULL' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'rack', @level2type=N'COLUMN',@level2name=N'rack_row'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'랙 표시 번호(같은 방의 같은 랙 열 내에서 유니크)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'rack', @level2type=N'COLUMN',@level2name=N'rack_no'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'랙 위치 표시용 좌표 X' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'rack', @level2type=N'COLUMN',@level2name=N'pos_x'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'랙 위치 표시용 좌표 X' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'rack', @level2type=N'COLUMN',@level2name=N'pos_y'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'랙 위치 표시용 각도' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'rack', @level2type=N'COLUMN',@level2name=N'angle'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1:1로 매핑할 랙 카탈로그 ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'rack', @level2type=N'COLUMN',@level2name=N'rack_catalog_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1:1로 매핑할 랙의 좌측에 설치할 Vertical Cable Manager 카탈로그 ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'rack', @level2type=N'COLUMN',@level2name=N'vcm_l_catalog_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1:1로 매핑할 랙의 우측에 설치할 Vertical Cable Manager 카탈로그 ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'rack', @level2type=N'COLUMN',@level2name=N'vcm_r_catalog_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'S:슬롯, L:좌측, R:우측, T:천정, B:바닥, F:전면, B:후면' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'rack_config', @level2type=N'COLUMN',@level2name=N'rack_mount_type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'타입에 따라 위치가 결정됨. S:슬롯-1번부터, 타입이 L/R/T/B: 1=중앙, 2=전면, 3=후면, 4~=기타, 타입이 F/B:1=중앙, 2=상부, 3=하부, 4~=기타' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'rack_config', @level2type=N'COLUMN',@level2name=N'slot_no'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'asset_id가 NULL이 들어가는 경우 사용되며, 자산이 아닌 카탈로그를 사용해야 하는 경우가 있다. (예: Empty Panel)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'rack_config', @level2type=N'COLUMN',@level2name=N'catalog_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'국가 또는 기타 지역 명' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'region1', @level2type=N'COLUMN',@level2name=N'region1_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'지역1에 해당하는 이미지 ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'region1', @level2type=N'COLUMN',@level2name=N'region1_image_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'세계 지도에 표시할 현 지역의 X 좌표 위치' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'region1', @level2type=N'COLUMN',@level2name=N'pos_x'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'세계 지도에 표시할 현 지역의 Y 좌표 위치' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'region1', @level2type=N'COLUMN',@level2name=N'pos_y'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'지역2 명' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'region2', @level2type=N'COLUMN',@level2name=N'region2_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'각 지역1 지도에 표시할 현 지역의 X 좌표 위치' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'region2', @level2type=N'COLUMN',@level2name=N'pos_x'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'각 지역1 지도에 표시할 현 지역의 Y 좌표 위치' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'region2', @level2type=N'COLUMN',@level2name=N'pos_y'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'리포트의 설명으로 실제 문서에 출력되지 않음' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'report', @level2type=N'COLUMN',@level2name=N'report_desc'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'리포트가 출력할 컬럼 수' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'report', @level2type=N'COLUMN',@level2name=N'num_of_report_column'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'리포트' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'report'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'리포트제목으로 언어 코드에 맞게 입력' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'report_lang', @level2type=N'COLUMN',@level2name=N'report_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'각 언어별로 리포트 구성' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'report_lang'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'리포트 컬럼 순번으로 1부터 시작' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'report_lang_column', @level2type=N'COLUMN',@level2name=N'report_column_no'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'리포트 컬럼 제목으로 언어 코드에 맞게 입력' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'report_lang_column', @level2type=N'COLUMN',@level2name=N'report_column_name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'각 리포트에 대한 세부 컬럼명과 번호' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'report_lang_column'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'3D 도면에서 해당 룸을 보여주기 위한 영역 중 좌측 상단 모서리의 X 값' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'room', @level2type=N'COLUMN',@level2name=N'square_x1'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'3D 도면에서 해당 룸을 보여주기 위한 영역 중 좌측 상단 모서리의 Y 값' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'room', @level2type=N'COLUMN',@level2name=N'square_y1'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'3D 도면에서 해당 룸을 보여주기 위한 영역 중 우측 하단 모서리의 X 값' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'room', @level2type=N'COLUMN',@level2name=N'square_x2'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'3D 도면에서 해당 룸을 보여주기 위한 영역 중 우측 하단 모서리의 Y 값' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'room', @level2type=N'COLUMN',@level2name=N'square_y2'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'사이트 접근 사용자 권한: A=Admin, U=User' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'site_user', @level2type=N'COLUMN',@level2name=N'user_right'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'사이트별 접근 허용된 유저 등록' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'site_user'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'새시용 네트워크 스위치 자산 ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sw_card_config', @level2type=N'COLUMN',@level2name=N'sw_asset_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'슬롯 번호: 1번 부터' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sw_card_config', @level2type=N'COLUMN',@level2name=N'slot_no'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'카드용 네트워크 스위치 자산 ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sw_card_config', @level2type=N'COLUMN',@level2name=N'sw_card_asset_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'스위치 카드 구성' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'sw_card_config'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'템플릿이 출력할 컬럼 수' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'template', @level2type=N'COLUMN',@level2name=N'num_of_template_column'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'리포트를 커스텀하기 위한 템플릿' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'template'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'템플릿 컬럼 번호로 1번부터 순서대로 시작' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'template_column', @level2type=N'COLUMN',@level2name=N'template_column_no'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'템플릿 컬럼에 대응하는 리포트 컬럼 번호, 참고로 템플릿 컬럼 번호 <= 리포트 컬럼 번호' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'template_column', @level2type=N'COLUMN',@level2name=N'report_column_no'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'템플릿 출력시 제한된 컬럼 정보' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'template_column'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1=Location, 2=Asset, 3=Link Diagram, 4=Terminal, 5=IPP, 6=Event 값이 바뀐 경우' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'update_func', @level2type=N'COLUMN',@level2name=N'update_func_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'각 ID에 대한 설명' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'update_func', @level2type=N'COLUMN',@level2name=N'update_func_desc'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'각 ID에 대해 변경 될 때마다 1씩 증가' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'update_func', @level2type=N'COLUMN',@level2name=N'invoked_cnt'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'클라이언트에서 검색을 줄이기 위해 폴링 루틴에서 사용할 테이블' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'update_func'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'S=수퍼운영자, A=Admin, U=User' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'user', @level2type=N'COLUMN',@level2name=N'user_group'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'이메일 수신 여부, Y=Yes, N=No' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'user', @level2type=N'COLUMN',@level2name=N'use_email'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'SMS 수신 여부, Y=Yes, N=No' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'user', @level2type=N'COLUMN',@level2name=N'use_sms'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Y=삭제 가능, N=삭제 불가능(시스템 사용)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'user', @level2type=N'COLUMN',@level2name=N'deletable'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'자산 도면 배치 여부: Y=Yes, N=No' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'user_port_layout', @level2type=N'COLUMN',@level2name=N'is_layout'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'자산 도면 배치 좌표 X' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'user_port_layout', @level2type=N'COLUMN',@level2name=N'pos_x'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'자산 도면 배치 좌표 Y' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'user_port_layout', @level2type=N'COLUMN',@level2name=N'pos_y'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'사용자 포트 구성(Face Plate와 MUTOA Box의 포트에 대해 1:1)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'user_port_layout'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'XC 연결 타입: V=수직 연결(포트 한 쌍씩 연결), H=수평 연결(PP한쪽을 작업 후 다른 쪽 PP 작업)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'work_order', @level2type=N'COLUMN',@level2name=N'wo_xc_connect_type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'처리결과: R=Registered(등록진행중), S=Successed(성공), C=Canceled(취소)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'work_order', @level2type=N'COLUMN',@level2name=N'wo_result'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'작업을 수행할 사람' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'work_order', @level2type=N'COLUMN',@level2name=N'process_user_id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'작업 지시' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'work_order'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'P:Patch Panel for XC, S:Switch for IC' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'work_order_task', @level2type=N'COLUMN',@level2name=N'remote_asset_type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'C:Connect, D:Disconnect' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'work_order_task', @level2type=N'COLUMN',@level2name=N'command_type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'처리결과: R=Registered(등록), S=Successed(성공), C=Canceled(취소)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'work_order_task', @level2type=N'COLUMN',@level2name=N'task_result'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'작업 지시에 대한 각 태스크(한 레코드는 한 개의 연결에 대한 정보를 가지고 있다.)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'work_order_task'
GO
