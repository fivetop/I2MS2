using I2MS2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WebApi.Models;

namespace I2MS2.Library
{
    // 트리뷰중 미접속 단말 뷰 모델 
    // ConnectionlessTreeViewManager 는 트리 형태가 아니므로 별도 처리 없음 
    public class ConnectionlessTreeViewManager
    {
        TreeView tv;
        AssetTreeViewManager astMgr;
        List<AssetTreeVM> conless_vm_list = new List<AssetTreeVM>();
        
        public ConnectionlessTreeViewManager(TreeView _tv)
        {
            tv = _tv;
            astMgr = new AssetTreeViewManager();
            initConlessTree();
        }
        // 초기화 
        private void initConlessTree()
        {
            List<asset> ast_list = g.asset_list.FindAll(at=> (at.location_id==0)&&(at.asset_name!="HUB")); // 미접속 단말인경우 : 0 , 허브는 제외 
            List<AssetTreeVM> list = new List<AssetTreeVM>();
            foreach(var ast in ast_list)
            {
                AssetTreeVM ast_vm = astMgr.makeAssetTreeVM(ast); // 뷰 모델 만들기 
                var at = g.asset_terminal_list.Find(p => (p.terminal_asset_id == ast.asset_id) && (p.terminal_status != "E")); // Expired 는 제외 
                if (at != null)
                {
                    ast_vm.last_activated = (DateTime?) at.last_activated;
                    list.Add(ast_vm);
                }
            }
            conless_vm_list = list.OrderBy(p => p.last_activated).ToList();
            tv.ItemsSource = conless_vm_list;
        }
        // PC 추가
        public void addPC(asset ast)
        {
            AssetTreeVM ast_vm = astMgr.makeAssetTreeVM(ast);
            var node = g.asset_terminal_list.Find(p => p.terminal_asset_id == ast.asset_id);
            ast_vm.last_activated = node != null ? (DateTime?) node.last_activated : null;
            conless_vm_list.Add(ast_vm);
            tv.ItemsSource = null;
            tv.ItemsSource = conless_vm_list;
        }
        // PC 삭제 
        public void removePC(int asset_id)
        {
            AssetTreeVM ast_vm = conless_vm_list.Find(at => at.asset_id == asset_id);
            if (ast_vm == null) return;
            conless_vm_list.Remove(ast_vm);
            tv.ItemsSource = null;
            tv.ItemsSource = conless_vm_list;
        }

    }
}
