   M S B A M L   `   `   `  ����F  BTaskManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:TaskManagerTaskManager  5   	       TaskManager.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:TaskManager     5   	     � Title$  Task Manager��5   	   $	��450��6   $	��780��6+   $	��780��67   $	��450��6F   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $	��300��6   �� 5      $��5 �6   �� 5      $��2* �6   $	��460��6*   .+�5   
   �� $��1��6   $��5��6'   $��Stretch=�61   � 5   
   $��0��6    � RowDefinitions 5      �� 5      $��60 �6    �� 5      $��50 �6    �� 5      $	��10* �6    .+�5      �� $��0��6   $��0��6%   .+�5      ��  �� Left$ 69��5      $��5q�5      $	��150��5      ��5      ��  �� Path$ Filter� �� UpdateSourceTrigger$ PropertyChanged=�5      �� 5      
 �� Top$ 20��5      $	��ADD��5       $��10q�5!      $��50��5"      ��5#      �� *
AddCommand+��5$      �� *Text+ �� ElementName$ NoteName���� 5%      $ 20��5&      $ 75��5'      $
��EDIT��5(      $��10q�5)      $��50��5*      ��5+      �� *EditCommand+��5,      �� $ ListNote��$ SelectedValue��� 5-      $ 20��5.      $	 150��5/      $��DELETE��50      $��10q�51      $��50��52      ��53      �� *DeleteCommand+��54      �� $ ListNote��$ SelectedValue��� 55      $ 20��56      $	 225��57      $	��LOG��58      $��10q�59      $��50��5:      ��5;      �� *
LogCommand+5<      �� 5>      $��1��6   $��1q�6"   
���   � �� CornerRadius$ 10��6J   	 �� Padding$	 2q�6\   $��5q�6h   
�������._�5?      �� -   5        
 ��Name$
 NoteName��5@       �� TextWrapping$ NoWrap=�5A      $��0q�5B      $��Center=�5C      $��18(�5D      
�������.��5E   1   5F      5G      �� 5I      -   5         ��Name$ ListNote��5I   0   $��2��6   $��5q�6$   $
��True��5L      $
��Auto=�5M      ��5J      �� *	AllNote+ �� SelectedItem 5K      �� *SelectedNote+��5N      �� 5O      .R�5P      ��  �� Orientation$ 
Horizontal=�6%   .+�5Q      �� $
��Wrap=�6<   $��40��6P   ��6(   �� *Id+6P   �� 5R      $
��Wrap=�6>   $��Stretch=�6R   $��Stretch=�6p   ��6(   �� *Name+6p   5S      5T      5V      5X      � 5Z   
   $��2��6   .+�5[      �� $��5q�5]      $��Disabled=�5^      $
��Auto=�5_       � DataContext 5\      �� *SelectedNote+.��5`      �  5a      �� 5b      $��1* �6(   �� 5c      $��70 �6(   �� 5d      $	��10* �6(   .+�5g      �� $��0��5i      $��50��5j      $��18(�5k      $
��Bold"�5l      $
 Wrap=�5m       J� 	AllowDrop$ False��5n      ��5h      �� *Name+$ PropertyChanged=�5n      �� 5p      $��1��6   .+�5q      �� $��Status��6$   $ 20��62   $ 17��6C   �� 5r      -   5         ��Name$ 
NoteStatus��5s   #   $	��100��5r   #   $ 67��6/   $ 14��6@    �� Text 5t   #   �� *Status+$ PropertyChanged=��� 5v      $��
Start Data��6$   $	 205��66   $ 10��6H   (  "System.Windows.Controls.DatePicker  5w      $ 5��6%   $	 270��5x   %   $	��172��67      SelectedDate 5y   %   �� *DateOfStart+ � 	Resources 5z      :  4System.Windows.Controls.Primitives.DatePickerTextBox �� 
TargetType �� Property �� Value �� RelativeSource �� AncestorType��%�   '        �� 5{   "   "  6(   .�5|   &   ��  � �� 5}   *   �� 5~   .   .R�5   2   �� $
 PART_TextBox��6:   ��5�   5   �� $ SelectedDate� �� "  5�   /   5�   '   5�   #   5�      5�      �� 5�      $��End Data��6$   $	 205��64    �� Bottom$ 10��6F     5�      $ 5��6%   $	 270��5�   %   $	��172��67    5�   %   �� *	DateOfEnd+ 5�      ��%�   '        �� 5�   "   "  6(   .�5�   &   ��  � �� 5�   *   �� 5�   .   .R�5�   2   �� $
 PART_TextBox��6:   ��5�   5   �� $ SelectedDate� �� "  5�   /   5�   '   5�   #   5�      5�      5�      �� 5�      -   5        $
 NoteInformationTb��5�      $��3��5�      $	��100��5�      $��Stretch=�5�      $
 Wrap=�5�       �� TextAlignment$ Justify=�5�      $ False��5�       �� AcceptsReturn$
 True��5�      ��5�      �� *Information+$ PropertyChanged=�5�      5�      5�      5�      5�      5�      