코딩 교칙

*c#
1. 클래스명  AaaBbbCcc   => 모두 명사형태로
    - WIndow  : AaaBbbWindow
    - Page : AaaBbbPage
    - UserControl : AaaBbbControl
    - Models : aaabbb  => webApi쪽에서 자동으로 생성된것을 쓰므로 소문자로 통일한다
    - Controller : 
    - Api :  

2. 메서드 aaaBbbCcc  => aaa는 동사
                   (ex: getProduct, get)
3. 프로퍼티 AaaBbbCcc
4. 클래스 변수 aaa_bbb_ccc

5. 오토변수(메서드 내의 변수) : aaa_bbb_ccc
       -  i, j, k 는 loop형태안에서 int 타입만 사용
       - r, r1, r2 는 result형태 등으로
6. static 변수 : aaa_bbb (g 라고 명명된 클래스내에 있으므로 g.aaa_bb 형태로 사용된다)


*wpf
1. Name : _{형식}AaaBbbCcc
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
3. 스타일명: I2MS_AaaBbbCccStyle
4. 기타리소스 : _brushAaaBbbCcc, _colorAaaBbbCcc
                        _brushAaaBbbCcc2
5. 기타규칙
    - 바인딩 같은 중요한 요소는 앞쪽에서 기록해야함(쉽게 볼수 있도록)
    - Name을 가급적 앞으로
	- MainWindow 외에 SubWindow들은 I2MS_SubWindowStyle 을 적용해서 구현할 수 있자
		=> 종료, DragMove를 사용하기 위해서는 Name을 반드시 _window로 해줘야만 한다(중요)

*c# 코딩 규칙
, 의 사용형태: Abc, Def (, 뒤에는 항상 스패이스)
()의 사용형태 : 메소드명(인수, 인수)
비교 사용형태 : a == b  or a > b
대입 사용형태 : a = b
&&, || 사용형태 : (조건1) && (조건2), (조건1) || (조건2)
! 사용형태 : !abc



*주석의 규칙
1. 메서드에 대한 설명 (필요한 경우)
2. 메서드 인수에 대한 설명 (필요한 경우)



*단어 축약
1. OK case
temp => tmp
result => r, ret, res
animation => ani

2. Not OK case
temp => t
