   M S B A M L   `   `   `  ����L  HUniversityManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:UniversityManagerUniversityManager  5   	   #    UniversityManager.LoginWindow   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:UniversityManager     5   	   H DMahApps.Metro, Version=1.6.0.0, Culture=neutral, PublicKeyToken=null;controls,http://metro.mahapps.com/winfx/xaml/controls  5   	     � Title$  LoginWindow��5	   	   $��336.93��6   $��309.022��6-      AccentColorBrush(  "MahApps.Metro.Controls.MetroWindow   	GlowBrush# �   5
   	   .��5      � $��	0,0,456,9q�6   .+�5   
   �� $��	Username:��6   $
��Left=�6$   $��52,65,-117,0q�6?   $	��Top=�6U   $��65��6m   �� 5   
   $��	Password:��6   $
��Left=�6$   $��52,108,-117,0q�6?   $	��Top=�6V   $��65��6n   �� 5   
   -   5         ��Name$	 pID��5      $
��Left=�6   $��23��6:    �� TextWrapping$
 Wrap=�6F   $	��Top=�6Z   $	��118��6r   $��122,25,-240,0q�6~   �� 5   
   -   5        $ username��5      $
��Left=�6$   $��23��6?   $
 Wrap=�6K   $	��Top=�6_   $	��118��6w   $��122,68,-240,0q�6�   �� 5   
   -   5        $ password��5      $
��Left=�6$   $��23��6?   $
 Wrap=�6K   $	��Top=�6_   $	��118��6w   $��122,111,-240,0q�6�   
�������6�   �� 5   
   -   5        $
 type��5      $
��Left=�6    $��23��6;   $
 Wrap=�6G   $	��Top=�6[   $	��118��6s   $��122,150,-240,0q�6   $y�Hidden=�6�   �� 5   
   -   5          AccentedSquareButtonStyle#��[ 5      $��Sign In��6D   $
��Left=�6V   $	��Top=�6q   $��91��6�   $��43,191,-134,0q�6�   $��30��6�   6�   �� 5   
   -   5        #��[ 5      $��Sign Up��6D   $
��Left=�6V   $	��Top=�6q   $��91��6�   $��161,191,-252,0q�6�   $��30��6�   6�   �� 5   
   -   5        #��[ 5      $��Email��6D   $
��Left=�6T   $	��Top=�6o   $��91��6�   $��106,223,-197,0q�6�   $��30��6�   6�   �� 5   
   $	��pID��6   $
��Left=�6   $��52,22,-117,0q�69   $	��Top=�6O   $��65��6g    J� RenderTransformOrigin$ 
0.306,0.46&�6r   �� 5   
   -   5         ��Name$ 	typeLabel��5      $��Type:��6#   $
��Left=�63   $��52,147,-117,0q�6N   $	��Top=�6e   $��65��6}   $y�Hidden=�6�   �� 5   
   -	   5        $ result��5      $
��Left=�6"   $��42��6=   $
 Wrap=�6I   $�� ��6]   $	��Top=�6e   $	��274��6}   $��14,256,-288,0q�6�   {�.  6�   5      5      