   M S B A M L   `   `   `  ����L  HUniversityManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:UniversityManagerUniversityManager  5   	   #    UniversityManager.EmailSystem   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:UniversityManager     5   	   H DMahApps.Metro, Version=1.6.0.0, Culture=neutral, PublicKeyToken=null;controls,http://metro.mahapps.com/winfx/xaml/controls  5   	     � Title$  EmailSystem��5	   	   $	��450��6   $
��1000��6*   .��5
      � .+�5   
   ,  &MahApps.Metro.Controls.MetroTabControl  -   5          Name$ 
tabControl��5   #   .@�5      �� -   5         ��Name$ inbox��5      $��My Inbox��6&   $
��Left=�68   $��39��6S   $��Bottom=�6_   $	��123��6z   $��0,0,0,-0.333q�6�    J� RenderTransformOrigin$ 	0.5,0.509&�6�   .��5      � ���	#FFE5E5E56   5      �� 5      -   5        $
 send��5      $��Send An Email��6$   $
��Left=�6;   $��39��6V   $	��Top=�6b   $	��190��6z   $��2,0,0,0q�6�   .��5      � ���	#FFE5E5E56   5      �� 5      -   5        $ contacts��5      $��Contacts List��6(   $
��Left=�6?   $��39��6Z   $	��Top=�6f   $	��166��6~   $��-1,0,0,0q�6�   .��5      � ���	#FFE5E5E56   5      �� 5      -   5        $ 
sentFolder��5      $��SentMail��6*   $
��Left=�6<   $��39��6W   $	��Top=�6c   $	��127��6{   $��0.333,0,-19.333,-0.333q�6�   .��5      � ���	#FFE5E5E55      5      �� 5      $��Logout��6   $��39��6&   $	��Top=�62   $��269,0,-280,0q�6J   .��5      � ���	#FFE5E5E56   5      �� 5      -   5        $ announcement��5      $��Make Announcement��6,   $��39��6G   $	��Top=�6S   $��-104,0,93,0q�6k   .��5      � ���	#FFE5E5E5.+�5      �� -   5         ��Name$ annoucementText��5      $
��Left=�67   *  $MahApps.Metro.Controls.TextBoxHelper   	Watermark$ Announcement��6R   $��18(�6�   $	��209��6�    �� TextWrapping$
 Wrap=�6�   $	��Top=�6�   $	��400��6�   $��298,172,0,0q�6�   �� 5      -   5        $ 
courseCode��5      $ 
CourseCode��63   $��20(�6a   $
��Left=�6o   $��40��6�   $
 Wrap=�6�   $	��Top=�6�   $	��158��6�   $��
298,78,0,0q�6�   !  MahApps.Metro.Controls.Tile  5       -	   5          Name$ submit��5    $   $
��SEND��64   $
��Auto��6C   $
��Auto��6Q   $��25(�6^   $��541,78,294,220q�6l   $y�Visible=�6�   5!      5"      5$      �� 5&   
   -
   5        $
 from��5&      {�.  $
��Left=�62   $��41��6M   $
 Wrap=�6Y   $	��Top=�6m   $	��160��6�   $��
136,80,0,0q�6�   $��14(�6�   $y�Hidden=�6�   �� 5'   
   -   5        $ to��5'      $ Username of Recipient��6   $
��Left=�6W   $��41��6r   $
 Wrap=�6~   $	��Top=�6�   $	��160��6�   $��136,150,0,0q�6�   $y�Hidden=�6�   $��14(�6�   �� 5(   
   -   5        $ subject��5(      $ Subject��6#   $
��Left=�6N   $��41��6i   $
 Wrap=�6u   $	��Top=�6�   $	��160��6�   $��136,220,0,0q�6�   $��14(�6�   $y�Hidden=�6�   �� 5)   
   -   5        $	 msg��5)      $ Message Content��6   $
��Left=�6R   $	��126��6m   $
 Wrap=�6z   $	��Top=�6�   $	��421��6�   $��136,284,0,0q�6�   $��14(�6�   $y�Hidden=�6�   6�     5*   
   -   5        $ 	sendEmail��5*      $
��SEND��6+   $
��Auto��6:   $��25(�6H   $��400,80,235,222q�6V   $
��Auto��6n   $y�Hidden=�6�    � Triggers 5+      :� 5,      	 :� RoutedEvent$	 Button.Click��6   .[�5-      �� .��5.      �� -   5        
 ��Name$
 
discoStory��5.   %   .��5/      ��  �� TargetProperty$ Background.Color�50   %    �� From$ BlackX�51   %   	 �� To$
 BlueX�52   %    g� FillBehavior$
 Stop=�53   %    g� Duration$ 0:0:1D�54   %    g� 	BeginTime$ 0:0:0X�55   %   �� 56      $ Background.Color�57   %   $
 BlueX�58   %   $	 RedX�59   %   $
 Stop=�5:   %   $ 0:0:1D�5;   %   $ 0:0:1X�5<   !   �� 5>      $ Background.Color�5?   %   $	 RedX�5@   %   $ GreenX�5A   %   $
 Stop=�5B   %   $ 0:0:1D�5C   %   $ 0:0:2X�5D   !   �� 5F      $ Background.Color�5G   %   $ GreenX�5H   %   $ BlackX�5I   %   $
 Stop=�5J   %   $ 0:0:1D�5K   %   $ 0:0:3X�5L   !   5M      5N      5O      5Q        5R   
   -   5        $
 back��5R      $��LOGOUT��6&   $	��200��67   $	��200��6D   $��25(�6P   $��269,138,267,98q�6^   $y�Hidden=�6�   5S      5T      