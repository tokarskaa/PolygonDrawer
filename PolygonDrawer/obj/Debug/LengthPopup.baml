   M S B A M L   `   `   `  ����H  DPolygonDrawer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:PolygonDrawerPolygonDrawer  5   	       PolygonDrawer.LengthPopup   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:PolygonDrawer     5   	     � WindowStartupLocation$  CenterScreen=�5   	    � Title$ Edge length��5	   	    � SizeToContent$ Height=�6   $	��200��64   .��5
      � $��15q�6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� -   5        $��lblQuestion��5      $��1��6#   .��63   Set edge length:6E   � 5   
   $��1��6   $��1��6$   $��Right=�61   .+�5      �� -   5        $
��text��5      $��80��6"   �� 5      .��6   px6   5      � 5   
   $��2��6   $��2��6!   $��Right=�65   $��0,15,0,0q�6Q   .+�5      �� -   5        $��btnDialogOk��5   &    �� 	IsDefault . $��60��6S   $��0,0,10,0q�6a   .��6s   _Ok6x   �� 5       �� IsCancel . $��60��6%   .��63   	_Cancel6<   5      5      5       