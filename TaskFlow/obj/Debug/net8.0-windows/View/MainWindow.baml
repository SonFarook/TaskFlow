   M S B A M L   `   `   `  ����C  ?TaskFlow, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:TaskFlowTaskFlow  5      7 clr-namespace:TaskFlow.ViewModelTaskFlow.ViewModel  5          TaskFlow.View.MainWindow   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      (localclr-namespace:TaskFlow     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      /vm clr-namespace:TaskFlow.ViewModel     5      -   5          � Title$  TaskFlow��5	      $	��800��5
      $	��600��5       � AllowsTransparency$
 True��5      
������ 
������  � 
ResizeMode$ NoResize=�5       � WindowStyle$
 None=�5      .��5      �  � RowDefinitions 5   
   �� 5      $��22 �6   �� 5      $��* �6   �� 5      $��44 �6   .+�5   
   �� $��3��5         DarkGray#��[  5       �� CornerRadius$ 15��5      �� 5!   
   $��0��5"       	 Green#��[ 5#      $ 	15,15,0,0��5$      � 5&   
   $��0��6    � ColumnDefinitions 5'      �� 5(      $��* �6#   �� 5)      $	��150 �6#   .+�5,      � $��1��6   .+�5-      �� $	��150��6    �� Orientation$ 
Horizontal=�6)   .+�5.      �� -   5        $��30,0,0,0q�5/        MinimizeWindowBtn#��[ 51      .��52      �� $ ��/Images/MinimizeWindow.png��6    53      �� 55      -   5          CloseWindowBtn#��[ 55   3   .��56      �� $��/Images/CloseWindow.png��6    57      59      5:      5;      �� 5>   
   $��1��5?      $��44��5@      $	��130��5A      $��11,0,0,0q�5B      $
��Left=�5C      #��[ 5D      $ 15��5E      ._�5F      �� $��0,7,0,0q�6   .+�5G      	� -   5          MenuBtn#��[ 5G   :   .��5H      �� $��/Images/TasksIcon.png��6   5I      	� 5J      -   5        #��[ 5J   8   .��5K      �� $ ��/Images/CreateTaskIcon.png��6   5L      	� 5M      -   5        #��[ 5M   :   .��5N      �� $��/Images/PomodoroIcon.png��6   5O      5P      5Q       � 5S   
   -   5        $��	MainFrame��5T      $��1��5U      $	��640��5V      $	��452��5W      $��Center=�5X      $��Center=�5Y      
������ $|�False��5[        � NavigationUIVisibility$ Hidden=�5\      �� 5_   
   $��2��5`      #��[ 5a      $ 	0,0,15,15��5b      � 5d   
   $��2��6    5e      �� 5f      $	��155 �6#   �� 5g      $��* �6#   �� 5h      $	��100 �6#   .+�5m      �� $��0��5n      $��
11,12,13,0q�5o      $��16(�5p      $��everyday 1% better��5q      �� 5u      $��1��5v      $��Center=�5w      $��Center=�5x      $��24(�5y      ��5z      �� *	TimerText+	 J� Effect	 5{      3  -System.Windows.Media.Effects.DropShadowEffect  5|      
   	Direction$	
 240Q�5}         Opacity$	 0.6Q�5~         ShadowDepth$ 3Q�5      5�      �� 5�      $��2��6   $��0,2,0,0q�6)   .+�5�      �� $��39��5�      $
��Bold"�5�      ��5�      �� *CurrentTime+�� 5�      $��69��6   ��6'   �� *CurrentDate+5�      5�      5�      5�      