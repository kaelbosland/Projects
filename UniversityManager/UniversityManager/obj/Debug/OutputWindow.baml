   M S B A M L   `   `   `  ����L  HUniversityManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:UniversityManagerUniversityManager  5   	   $    UniversityManager.OutputWindow   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:UniversityManager     5   	   H DMahApps.Metro, Version=1.6.0.0, Culture=neutral, PublicKeyToken=null;controls,http://metro.mahapps.com/winfx/xaml/controls  5   	   -   5        (  "MahApps.Metro.Controls.MetroWindow   Name$  OutputWindow1��5       � Title$ Output Page��5	   	   $��364.454��6   $��280.989��6.   .��5
      � $��0,0,2,0q�6    � ColumnDefinitions 5   
   �� 5      $��5* �6   �� 5      $	��83* �6   .+�5   
   �� -   5         ��Name$ title��5      $&�� Univeristy Manager - Output Page��6   $
��Left=�6J   $	��Top=�6e   $��32��6}   $	��250��6�   
���   �
�������$��Calibri)�6�   $��17(�6�   $��8,0,0,0q�6�   $��2��6�   �� 5   
   -   5         ��Name$ _switch��5      $��Switch Forms��6"   $
��Left=�69   $	��Top=�6T   $��92��6l   $��	4,118,0,0q�6w   $y�Visible=�6�   $��32��6�   $��Calibri)�6�   $��1��6�   o� 5   
   $��2��6   $
��Left=�6#   $��81��6>   $	��Top=�6J   $	��245��6b   $��	11,32,0,0q�6n   .@�5      m� -   5         m�Name$ 	tableMenu��5      $��Choose Table��6*   $	��110��6@   .@�5      m� -   5        $ person��5      $��Person��6+    m� IsCheckable . {�.  6b   m� 5      -   5        $ student��5      $��Student��6,    . {�.  6d   m� 5      -   5        $
 prof��5      $��	Professor��6)    . {�.  6c   m� 5      -   5        $ classes��5      $��Class��6,    . {�.  6b   m� 5      -	   5        $
 dept��5      $��
Department��6)    . {�.  6d   m� 5      -
   5        $
 prog��5      $��Program��6)    . {�.  6a   5      m� 5      -   5        $ columnsMenu��5      $��Choose Column(s)��6,    J� RenderTransformOrigin$ 0.5,0.5&�6F   $��28��6f   $	��134��6r   z�5      V� 5      .~�5      �� �� 5      �� 5       �� Angle$ 0.138Q�6*   T� 5       5!      5#      5$      �� 5%   
   -   5        $	 run��5%      $
��Left=�6   $	��Top=�69   $��93��6Q   $��144,118,0,0q�6\   $��1��6�   $��32��6�   $��Run Query
��6�   �� 5&   
   -   5        	 ��Name$	 results��5&      {�.  $
��Left=�65   $��79��6P   
 �� TextWrapping$

 Wrap=�6\   $�� ��6p   $	��Top=�6x   $	��237��6�   $��1��6�   $��	0,226,0,0q�6�   5'      5(      