   M S B A M L   `   `   `  ����  ImageComparisonWpfGui&     ImageComparisonWpfGui.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Image comparison in .net��5   	   $	��350��6*   $	��700��67   .��5      �  � UseLayoutRounding .  � RowDefinitions 5   
   �� 5      $	��80* �6   �� 5      $	��20* �6    � ColumnDefinitions 5
   
   �� 5      �� 5      �� 5      .+�5   
   �� -   5        $
��img1��5   .   $��Stretch=�6   $��Uniform=�6:   $��Stretch=�6L   $��0��6h   $��0��6x   ��6�   �� *Image1+�� 5   
   -   5        $��imgDifference��5   .   $��Stretch=�6   $��Uniform=�6C   $��Stretch=�6U   $��1��6q   $��0��6�   ��6�   �� *DifferenceImage+�� 5   
   -   5        $
��img2��5   .   $��Stretch=�6   $��Uniform=�6:   $��Stretch=�6L   $��2��6h   $��0��6x   ��6�   �� *Image2+�� 5   
   -   5        $��btnOpenImage1��5   d   $��Open image 1��6   $��1��6(   $��0��66   $��Stretch=�6F   $��Stretch=�6y   $	��300��6�   $��60��6�   $��Normal&�6�   �� 5   
   -   5        $��btnOpenImage2��5   d   $��Open image 2��6   $��1��6(   $��2��66   $��Stretch=�6F   $��Stretch=�6y   $	��300��6�   $��60��6�   $��Normal&�6�   5      5      