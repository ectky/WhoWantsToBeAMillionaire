   M S B A M L   `   `   `  ����S  OWhoWantsToBeAMillionaire, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:Game.AppGame.App  5   	       Game.App.EndGame   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:Game.App     5   	   -   5          � Title$  EndGame��5   	    � WindowState$ 	Maximized=�5	   	   ��5
      �� 5   
    �� ImageSource$5 /pack://application:,,,/Resources/background.jpg��6   6U   .��5      � .+�5   
   �� $/��)pack://application:,,,/Resources/logo.png��6   $��0��5      $
��Left=�6   $	��630��68   $��10,0,0,0q�6E   $	��Top=�6W   $	��646��6o   �� 5   
   $7��1pack://application:,,,/Resources/logoTvoqChas.jpg��6   $��0��6K   $
��Left=�5      $	��101��6+   $	��Top=�68   $	��261��6P   $��
1057,0,0,0q�6\   &   System.Windows.Controls.DataGrid  5   
   -   5        $��dataGridTable��5         AutoGenerateColumns .  $	��258��5      $
��Left=�5      $	��Top=�5      $	��595��5      $��723,127,0,0q�6      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   MinWidth$ 50Q�6C   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6%   ��  �� Path$
 Rank�6C     5      $ 50Q�6G    6%   �� $ Username�6G     5      $ 50Q�6D    6%   �� $ Score�6D     5      $ 50Q�6G    6%   �� $ PlayedOn�6G   5      �� 5   
   -   5        $��Играй отново��5      $��Center=�6(   $��723,464,144,66q�6E   $��Center=�5       $	��595��6,   $	��206��68   
���   �
�������$��36(�5!   7   6E   �� 5"   
   -   5        $��Button��5"      $
��Left=�6"   $��1369,71,0,0q�6=   $	��Top=�6R   $��75��6j   6u   5#      5$      