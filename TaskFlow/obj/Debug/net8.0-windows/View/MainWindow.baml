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
��Left=�5F      #��[ 5G      $ 15��5H      ._�5I      �� $��0,7,0,0q�6   .+�5J      	� -   5          MenuBtn#��[ 5J   :   .��5K      �� $��/Images/TasksIcon.png��6   5L      	� 5M      -   5        #��[ 5M   8   .��5N      �� $ ��/Images/CreateTaskIcon.png��6   5O      	� 5P      -   5        #��[ 5P   :   .��5Q      �� $��/Images/PomodoroIcon.png��6   5R      5S      5T       � 5V   
   -   5        $��	MainFrame��5W      $��1��5X      $	��640��5Y      $	��452��5Z      $��Center=�5[      $��Center=�5\      
������ $|�False��5^      	  � NavigationUIVisibility$	 Hidden=�5_      �� 5b   
   $��2��5c      #��[ 5d      $ 	0,0,15,15��5e      � 5g   
   $��2��6    5h      �� 5i      $	��155 �6#   �� 5j      $��* �6#   �� 5k      $	��100 �6#   .+�5p      �� $��
11,12,13,0q�5q      $��16(�5r      $��everyday 1% better��5s      �� 5w      $��2��6   $��0,2,0,0q�6)   .+�5x      �� $��39��5y      $
��Bold"�5z      ��5{      �� *CurrentTime+�� 5|      $��69��6   ��6'   �� *CurrentDate+5}      5~      5      5�      