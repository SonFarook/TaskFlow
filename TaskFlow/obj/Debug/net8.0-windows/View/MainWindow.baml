   M S B A M L   `   `   `  ����C  ?TaskFlow, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:TaskFlowTaskFlow  5      7 clr-namespace:TaskFlow.ViewModelTaskFlow.ViewModel  5          TaskFlow.View.MainWindow   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      (localclr-namespace:TaskFlow     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      /vm clr-namespace:TaskFlow.ViewModel     5      -   5          � Title$  TaskFlow��5	      $	��800��5
      $	��600��5       � AllowsTransparency$
 True��5      
������ 
������  � 
ResizeMode$ NoResize=�5       � WindowStyle$
 None=�5       � DataContext 5      &    TaskFlow.ViewModel.MainViewModel  5   
   .��5      �  � RowDefinitions 5   
   �� 5      $��22 �6   �� 5      $��* �6   �� 5      $��44 �6   .+�5   
   �� $��3��5         DarkGray#��[  5        �� CornerRadius$ 15��5!      �� 5$   
   $��0��5%       	 Green#��[ 5&      $ 	15,15,0,0��5'      � 5)   
   $��0��6    � ColumnDefinitions 5*      �� 5+      $��* �6#   �� 5,      $	��150 �6#   .+�5/      � $��1��6   .+�50      �� $	��150��6    �� Orientation$ 
Horizontal=�6)   .+�51      �� -   5        $��30,0,0,0q�52        MinimizeWindowBtn#��[ 54      .��55      �� $ ��/Images/MinimizeWindow.png��6    56      �� 58      -   5          CloseWindowBtn#��[ 58   3   .��59      �� $��/Images/CloseWindow.png��6    5:      5<      5=      5>      �� 5A   
   $��1��5B      $��44��5C      $	��130��5D      $��11,0,0,0q�5E      $
��Left=�5F      #��[ 5G      $ 15��5H      ._�5I      �� $��0,7,0,0q�6   .+�5J      	� -   5        $��0,0,0,5q�5K        MenuBtn#��[ 5M      .��5N      �� $��/Images/TasksIcon.png��6   5O      	� 5P      -   5        $��0,0,0,5q�5Q      #��[ 5S      .��5T      �� $ ��/Images/CreateTaskIcon.png��6   5U      	� 5V      $��0,0,0,5q�6   #��[ 6/   .��5W      �� $��/Images/PomodoroIcon.png��6   5X      5Y      5Z       � 5\   
   -   5        $��	MainFrame��5]      $��1��5^      $	��640��5_      $	��452��5`      $��Center=�5a      $��Center=�5b      
������ $|�False��5d      	  � NavigationUIVisibility$	 Hidden=�5e      �� 5h   
   $��2��5i      #��[ 5j      $ 	0,0,15,15��5k      � 5m   
   $��2��6    5n      �� 5o      $	��155 �6#   �� 5p      $��* �6#   �� 5q      $	��100 �6#   .+�5v      �� $��
11,12,13,0q�5w      $��16(�5x      $��everyday 1% better��5y      �� 5}      $��2��6   $��0,2,0,0q�6)   .+�5~      �� $��39��5      $
��Bold"�5�      ��5�      �� *CurrentTime+�� 5�      $��69��6   ��6'   �� *CurrentDate+5�      5�      5�      5�      