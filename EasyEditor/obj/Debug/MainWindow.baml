   M S B A M L   `   `   `  ����E  AEasyEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:EasyEditor.ControlsEasyEditor.Controls  5   	   'clr-namespace:EasyEditor
EasyEditor  5   	       EasyEditor.MainWindow   5   	   .Controls!clr-namespace:EasyEditor.Controls  R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:EasyEditor     5   	   -   5          �Name$  window��5   <      NoneWindowBackground#��[  5       � Title$ 
EasyEditor��5	   	   $	��800��6   $
��1200��6)   .��5      � -   5         �Name$ 
grid_main_��5      .+�5   
   "  EasyEditor.Controls.__grid__     Class$ EDITORS��6   $��	0,30,0,30q�6,   .+�5      � -   5        $ editros��5      .+�5          EasyEditor.Controls.Editor  5      5        5   
   $ LISTS EVENTS��6   .+�5      (   "EasyEditor.Controls.MiltiChSelList  -   5          Name$ multi_file_events��5   &   5      �� 5   
   -   5         ��Name$ buttom_border��5      $��Bottom=�6(   $��30��6C     	ba_window#��[ 6O    �� CornerRadius$ 0,0,7,7��6w   ._�5      ��  �� Orientation$ 
Horizontal=�6   $��Right=�62   $��0,0,10,0q�6N   .+�5      �� -   5        $
 Lang��5        style_border_hover#��[ 6'   $��0,0,5,0q�6S   ._�5      �� -   5         ��Name$ 	LangLabel��5      $��Text ��60   6B   5      �� 5      -   5        $ count_lines��5      $��Char:0 Lines:1��6-   6H   5      5      �� 5   
   -	   5        $ b_move��5      $	��Top=�6!   $��30��69   #��[ 6E   $ 7,7,0,0��6m   ._�5       � .+�5!      .   (EasyEditor.Controls.WindowControlButtons  -
   5        	  Name$		 wcb��5!   0   $��Right=�6=   �� 5"      $ 
Horizontal=�6   $
��Left=�66   .+�5#      �� $��	10,0,10,0q�6   ��6/   �� *Title+
 �� ElementName$
 window��6_   �� 5$      -   5        $ _file_��5$      #��[ 6-   ._�5%      �� $
��File��6    61   5&      �� 5'      -   5        $ 
_terminal_��5'      #��[ 61   ._�5(      �� $��Terminal��6    65   5)      5*      5+      5,      )   #EasyEditor.Controls.WResizerControl  5-   
   -   5          Name$ res_win��5-   #   5.      5/      