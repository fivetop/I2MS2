�ڵ� ��Ģ

*c#
1. Ŭ������  AaaBbbCcc   => ��� ������·�
    - WIndow  : AaaBbbWindow
    - Page : AaaBbbPage
    - UserControl : AaaBbbControl
    - Models : aaabbb  => webApi�ʿ��� �ڵ����� �����Ȱ��� ���Ƿ� �ҹ��ڷ� �����Ѵ�
    - Controller : 
    - Api :  

2. �޼��� aaaBbbCcc  => aaa�� ����
                   (ex: getProduct, get)
3. ������Ƽ AaaBbbCcc
4. Ŭ���� ���� aaa_bbb_ccc

5. ���亯��(�޼��� ���� ����) : aaa_bbb_ccc
       -  i, j, k �� loop���¾ȿ��� int Ÿ�Ը� ���
       - r, r1, r2 �� result���� ������
6. static ���� : aaa_bbb (g ��� ���� Ŭ�������� �����Ƿ� g.aaa_bb ���·� ���ȴ�)


*wpf
1. Name : _{����}AaaBbbCcc
    - TextBlock : _lblAaaBbb
    - TextBox : _txtAaaBbb
    - ListView : _lvAaaBbb
    - TreeView: _tvAaaBbb
    - Grid :  _gridAaaBbb
    - Button : _btnAaaBbb
	- ToggleButton : _tgbAaaBbb
    - Border : _bdAaaBbb 
    - CheckBox : _chkAaaBbb
    - ComboBox : _cboAaaBbb
    - DataGrid :_dgAaaBbb
    - Image: _imgAaaBbb
    - RadioButton : _radioAaaBbb
    - Rectangle : _rectAaaBbb
    - StackPanel : _stackAaaBbb
    - TabControl : _tabAaaBbb
    - Ellipse : _ellipseAaaBbb
    - ScrollViewer : _scrollAaaBbb
	- UserControl : _ctlAaaBbb
2. Event : _aaaBbbCcc_Event
3. ��Ÿ�ϸ�: I2MS_AaaBbbCccStyle
4. ��Ÿ���ҽ� : _brushAaaBbbCcc, _colorAaaBbbCcc
                        _brushAaaBbbCcc2
5. ��Ÿ��Ģ
    - ���ε� ���� �߿��� ��Ҵ� ���ʿ��� ����ؾ���(���� ���� �ֵ���)
    - Name�� ������ ������
	- MainWindow �ܿ� SubWindow���� I2MS_SubWindowStyle �� �����ؼ� ������ �� ����
		=> ����, DragMove�� ����ϱ� ���ؼ��� Name�� �ݵ�� _window�� ����߸� �Ѵ�(�߿�)

*c# �ڵ� ��Ģ
, �� �������: Abc, Def (, �ڿ��� �׻� �����̽�)
()�� ������� : �޼ҵ��(�μ�, �μ�)
�� ������� : a == b  or a > b
���� ������� : a = b
&&, || ������� : (����1) && (����2), (����1) || (����2)
! ������� : !abc



*�ּ��� ��Ģ
1. �޼��忡 ���� ���� (�ʿ��� ���)
2. �޼��� �μ��� ���� ���� (�ʿ��� ���)



*�ܾ� ���
1. OK case
temp => tmp
result => r, ret, res
animation => ani

2. Not OK case
temp => t
