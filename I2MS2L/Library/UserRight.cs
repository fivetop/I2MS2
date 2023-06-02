using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using I2MS2.Models;

namespace I2MS2.Library
{
    // 유저 권한 
    public enum eUserRight
    {
        eLineLinkList,              // 선번장 목록 조회/출력
        eAssetList,                 // 자산 목록 조회/출력
        eLocationList,              // 위치 목록 조회/출력
        eEnvironmentList,            // 환경 정보 조회/출력
        eCatalogList,               // 카탈로그 목록 조회/출력
        eManufactureList,           // 제조사 목록 조회/출력
        eUserList,                  // 연락처 목록 조회/출력
        eLinkDiagramChangeHistList, // 링크 다이어그램 변경 내역 조회
        eTerminalView,              // 네트워크 검색 뷰
        eEventList,                 // 알람/이벤트 목록 조회/출력
        eLogdbList,                 // 알람/이벤트 목록 조회/출력
        eWorkOrderList,             // 작업지시 목록

        eStatTerminal1,              // 터미날 통계 
        eStatTerminal2,              // 터미날 통계 
        eStatTerminal3,              // 터미날 통계 
        eStatEvent1,                 // 이벤트 통계 
        eStatEvent2,                 // 이벤트 통계 
        eStatEvent3,                 // 이벤트 통계 

        eUserManager,               // 사용자 관리
        eSiteUserManager,           // 사이트별 사용자 허가 등록 관리
        eDrawingsManager,           // 이미지 등록 관리
        eDrawings3DManager,         // 3D 도면 등록 관리
        eManufactureManager,        // 제조사 및 연락처 등록 관리
        eCatalogManager,            // 카탈로그 등록 관리
        eCatalogGroupManager,       // 카탈로그 그룹 등록 관리
        eExtendedPropertyManager,   // 확장 속성 등록 관리
        eExtAssignManager,          // 확장 속성 할당 관리
        eNetworkSchedulerManager,   // 네트워크 스케쥴 등록 관리
        eGrantManager,              // 인가/비인가 관리
        eExportManager,             // 데이터 저장(Export)
        eImportManger,              // 데이터 불러오기(Import)
        eAlarmEventSetup,           // 알람/이벤트 설정
        eICFwUpgrade,               // 컨트롤러 펌웨어 업그레이드
        eEtcViewOption,             // 기타 표시 옵션 설정
        ePrintTemplate,             // 출력 템플릿 관리
        eEnvironmentSetting,        // 모니터링 정보 설정
        eLanguageSetting,           // 언어설정
        eScanAll_IC,                // 전체 컨트롤러 스캔
        eMailSMS,                   // 이메닐, SMS 

        eCopy,
        eClone,
        eDelete,
        eAddBuilding,
        eEditBuilding,
        eAddFloor,
        eEditFloor,
        eAddRoom,
        eEditRoom,
        eAddRack,
        eEditRack,
        eConfigRackMount,
        eAddAsset,
        eEditAsset,
        eMoveTree,
        eScanIC,
        eConfigIC,
        eConfigSW,
        eViewRack,
        eViewIC,
        eViewPP,
        eViewSW,
        eViewLinkDiagram,
        eViewForm,                  // 성적서 
        eViewFloorIPM,
        eViewIPM,
        eRoomRackLayoutMount,
        // 배선관리
        eAddCable,                  // 케이블 추가
        eTurnAsset,                 // 자산 회전
        eWorkOrder,                 // 작업지시
        eSaveConnect,               // 저장
        eAcceptConnection,          // 연결 상태 허용
        eCopy2,
        ePaste2,
        eDelete2,
        eStart,
    }



    public class UserRight
    {
        // 수퍼관리자가 사용할 수 있는 메뉴 및 기능(사이트 관리자 기능이 기본)
        //----------------------------------------------------------------------
        // eCatalogGroupManager,   // 카탈로그 그룹 관리
        // eImportManger,          // 데이터 불러오기(Import)
        // eIcFwUpgrade,           // 컨트롤러 펌웨어 업그레이드


        // 사이트 관리자가 사용할 수 있는 메뉴 및 기능
        static List<eUserRight> _admin_func_list = new List<eUserRight>() { 
        eUserRight.eLineLinkList,
        eUserRight.eAssetList,
        eUserRight.eLocationList,
        eUserRight.eEnvironmentList,
        eUserRight.eCatalogList,
        eUserRight.eManufactureList,
        eUserRight.eUserList,
        eUserRight.eLinkDiagramChangeHistList,
        eUserRight.eTerminalView,
        eUserRight.eEventList,
        eUserRight.eLogdbList,
        eUserRight.eWorkOrderList,

        eUserRight.eStatTerminal1,              // 터미날 통계 
        eUserRight.eStatTerminal2,              // 터미날 통계 
        eUserRight.eStatTerminal3,              // 터미날 통계 
        eUserRight.eStatEvent1,                 // 이벤트 통계 
        eUserRight.eStatEvent2,                 // 이벤트 통계 
        eUserRight.eStatEvent3,                 // 이벤트 통계 
        
        eUserRight.eUserManager,
        eUserRight.eSiteUserManager,
        eUserRight.eDrawingsManager,
        eUserRight.eDrawings3DManager,
        eUserRight.eManufactureManager,
        eUserRight.eCatalogManager,
        eUserRight.eExtendedPropertyManager,
        eUserRight.eExtAssignManager,
        eUserRight.eNetworkSchedulerManager,
        eUserRight.eGrantManager,              // 인가/비인가 관리

        eUserRight.eExportManager,
        eUserRight.eAlarmEventSetup,
        eUserRight.eEtcViewOption,         
        eUserRight.ePrintTemplate,
        eUserRight.eEnvironmentSetting,
        eUserRight.eLanguageSetting,
        eUserRight.eScanAll_IC,                // 전체 컨트롤러 스캔
        eUserRight.eMailSMS,

        eUserRight.eCopy,
        eUserRight.eClone,
        eUserRight.eDelete,
        eUserRight.eAddBuilding,
        eUserRight.eEditBuilding,
        eUserRight.eAddFloor,
        eUserRight.eEditFloor,
        eUserRight.eAddRoom,
        eUserRight.eEditRoom,
        eUserRight.eAddRack,
        eUserRight.eEditRack,
        eUserRight.eConfigRackMount,
        eUserRight.eAddAsset,
        eUserRight.eEditAsset,
        eUserRight.eMoveTree,
        eUserRight.eScanIC,
        eUserRight.eConfigIC,
        eUserRight.eConfigSW,
        eUserRight.eViewRack,
        eUserRight.eViewIC,
        eUserRight.eViewPP,
        eUserRight.eViewSW,
        eUserRight.eViewLinkDiagram,
        eUserRight.eViewFloorIPM,
        eUserRight.eViewIPM,
        eUserRight.eRoomRackLayoutMount,
        eUserRight.eViewForm,

        eUserRight.eAddCable,
        eUserRight.eTurnAsset,
        eUserRight.eWorkOrder,
        eUserRight.eSaveConnect,
        eUserRight.eAcceptConnection,
        eUserRight.eCopy2,
        eUserRight.ePaste2,
        eUserRight.eDelete2,
        eUserRight.eStart,
};

        // 사이트 유저가 사용할 수 있는 메뉴 및 기능
        static List<eUserRight> _user_func_list = new List<eUserRight>() { 
        eUserRight.eViewRack,
        eUserRight.eViewIC,
        eUserRight.eViewPP,
        eUserRight.eViewSW,
        eUserRight.eViewLinkDiagram,
        eUserRight.eViewFloorIPM,
        eUserRight.eViewIPM,
        eUserRight.eViewForm,

        eUserRight.eStatTerminal1,              // 터미날 통계 
        eUserRight.eStatTerminal2,              // 터미날 통계 
        eUserRight.eStatTerminal3,              // 터미날 통계 
        eUserRight.eStatEvent1,                 // 이벤트 통계 
        eUserRight.eStatEvent2,                 // 이벤트 통계 
        eUserRight.eStatEvent3,                 // 이벤트 통계 

        eUserRight.eLineLinkList,
        eUserRight.eAssetList,
        eUserRight.eLocationList,
        eUserRight.eEnvironmentList,
        eUserRight.eCatalogList,
        eUserRight.eManufactureList,
        eUserRight.eUserList,
        eUserRight.eLinkDiagramChangeHistList,
        eUserRight.eEventList,
        eUserRight.eLanguageSetting,
        };

        public static bool is_ok(eUserRight right)
        {
            int user_id = g.login_user_id;
            int site_id = g.selected_site_id;
            if (site_id == 0)
                return false;
            var u = g.user_list.Find(p => p.user_id == user_id);
            if (u == null)
                return false;
            // 수퍼 관리자 권한을 가지고 있으면 무조건 Pass
            if (u.user_group == "S")
                return true;
            // 사이트별 사용자리스트에 없으면 False
            var su = g.site_user_list.Find(p => (p.site_id == site_id) && (p.user_id == user_id));
            if (su == null)
                return false;
            if (su.user_right == "A")
            {
                var list = _admin_func_list.Where(p => p == right);
                return list.Count() != 0;
            }
            else
            {
                var list = _user_func_list.Where(p => p == right);
                return list.Count() != 0;
            }
        }

        public static bool is_site_admin()
        {
            int user_id = g.login_user_id;
            int site_id = g.selected_site_id;
            var u = g.user_list.Find(p => p.user_id == user_id);
            if (u == null)
                return false;
            // 수퍼 관리자 권한을 가지고 있으면 무조건 Pass
            if (u.user_group == "S")
                return true;
            // 사이트별 사용자리스트에 없으면 False
            var su = g.site_user_list.Find(p => (p.site_id == site_id) && (p.user_id == user_id));
            if (su == null)
                return false;
            return su.user_right == "A";
        }

        public static bool is_site_user_right(int site_id)
        {
            int user_id = g.login_user_id;
            //int site_id = g.selected_site_id;
            var u = g.user_list.Find(p => p.user_id == user_id);
            if (u == null)
                return false;
            // 수퍼 관리자 권한을 가지고 있으면 무조건 Pass
            if (u.user_group == "S")
                return true;
            // 사이트별 사용자리스트에 없으면 False
            var su = g.site_user_list.Find(p => (p.site_id == site_id) && (p.user_id == user_id));
            return su != null;
        }

        // 수퍼 관리자 권한?
        public static bool is_super_user()
        {
            int user_id = g.login_user_id;
            int site_id = g.selected_site_id;
            var u = g.user_list.Find(p => p.user_id == user_id);
            if (u == null)
                return false;
            return u.user_group == "S";
        }

    }
}
