   M S B A M L   `   `   `  ����H  DPolygonDrawer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:PolygonDrawerPolygonDrawer  5   	       PolygonDrawer.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:PolygonDrawer     5   	   -   5        $��
mainWindow��5   	     � WindowState$  	Maximized=�5   	    � DataContext$ local:MainWindowViewModel��5	   	    � Title$ 
MainWindow��5
   	   $	��350��6   $	��525��6)    � 	Resources 5         
vertexMenu  lineMenu �� Path  constrainedMenu��%~  &	        &	 X     &	 �    �� 5   
   .@�5      m� -   5        $��Delete vertex��5      6/   5      �� 5   
   .@�5      m� -   5        $��
Add vertex��5      6+   m� 5      $��verticalItem��6   -   5        $��Set edge vertically��5   +   {�6_   �� $ selectedLine.Vertical�m� 5      $��horizontalItem��6   -   5        $��Set edge horizontally��5   -   6L   m� 5      -   5        $��
Set length��5      6+   5      �� 5   
   .@�5      m� -   5        $��
Add vertex��5      6+   m� 5      -   5        $��Remove constraint��5      62   5      5      .��5      � -	   5        $��MainGrid��5       � ColumnDefinitions 5   
   �� 5      $��* �6   6+   �� 5      $��5* �6   6,   .+�5    
   �� 
�������$��0��6(   
�������$��3q�6O   �� 5!   
   $��0��6   $��5q�6%   .+�5"      �� -
   5        $��DeleteButton��5"      $��40��6>   $��15q�6J   .��6V   delete polygon6f   �� 5#      -   5        $��
DrawButton��5#      $��40��6:   $��15q�6F   .��6R   draw polygon6`   5$      � 5&   
   -   5        $��	ImageGrid��5&      $��1��6   .+�5'      �� -   5        $��LabelCanvas��5'       K� ZIndex$ 2��6(   
������ 5(      �� 5)      -   5        $��	MainImage��5)      $
��Fill=�6&   $ 1��65   6H   5+      5-      5.      