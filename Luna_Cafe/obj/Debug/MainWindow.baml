   M S B A M L   `   `   `  ����D  @Luna_Cafe, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:Luna_Cafe	Luna_Cafe  5   	       Luna_Cafe.MainWindow   5   	   c _MaterialDesignThemes.MahApps, Version=5.2.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962a_ [MaterialDesignThemes.Wpf, Version=5.2.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aHmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:Luna_Cafe     5   	     � Title$  
MainWindow��5	   	   $	��450��6   $	��800��6)   .��5
      � ���#f1e0cd � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $	�� 2* �6   .+�5   
   �� $��0��6   $��20q�6%   .+�5      �� -   5        $'��!ДОДАТИ ЗАМОВЛЕННЯ��5      ���#8b523d$��20 10q�5      $��12(�5      
�������    MaterialDesignOutlinedButton#��[  5      5      �� 5      -   5        $/��)РЕДАГУВАТИ ЗАМОВЛЕННЯ��5      ���#8b523d$
��20 5q�5      
�������$��11(�5      #��[  5       {�5!      �� *CanEditOrder+5"      �� 5$      -   5        $��ВИДАЛИТИ��5$      ���#a71d27$
��20 5q�5'      
�������$��11(�5)      #��[  5*      {�5+      �� *CanDeleteOrder+5,      �� 5.      -   5        $��ЗБЕРЕГТИ��5.      ���#39080c$��20 40q�50      
�������$��11(�52      #��[  53      55      56      �� 58   
   $��1��6    �� CornerRadius$ 20��6!   ���#d5a586$��20q�6H   ._�5;      &   System.Windows.Controls.DataGrid  -   5          Name$ OrdersTable��5;         AutoGenerateColumns .     
IsReadOnly .    CanUserAddRows .  ��5=      �� *Orders+ �� SelectedItem 5>      �� *SelectedOrder+ �� Mode$ TwoWay=�	 � 	Resources	 5C      )  #System.Windows.Controls.DataGridRow
 �� 
TargetType �� Property �� Value �� Triggers P� Property P� Value��%3  '        �� 5D      "
  6   .�5E      ��  � �� �#d5a58667   �� 5F       � ��
 �   �67   �� 5G       � ��$ 14(�65    5H      P� 5I       �@ 
IsSelected . . �5J   "   ��  � �� �#8d6b5f6?   �� 5K   "    � ��
 �����6?   5L      5N      5O         Columns 5P      0  *System.Windows.Controls.DataGridTextColumn  5Q      ,  &System.Windows.Controls.DataGridColumn   Header$ 
Назва��6)   1  +System.Windows.Controls.DataGridBoundColumn   Binding 68   �� *
CafeName+  5R      $ Дата��6)    67   �� *Date+  5S      $% Час приготування��6)    6C   �� *CookingTimeDisplay+  5T      $ Сума��6)    67   �� *
TotalSum+5V      5W      5X      5Y      