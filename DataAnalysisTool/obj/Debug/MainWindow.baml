   M S B A M L   `   `   `  ����[  WSystem.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089Y@clr-namespace:System.Windows.Forms;assembly=System.Windows.FormsSystem.Windows.Forms  5   	   H DTOSOHSoftware, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:DataAnalysisToolDataAnalysisTool 5   	   !   DataAnalysisTool.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Xwinformhost@clr-namespace:System.Windows.Forms;assembly=System.Windows.Forms     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   R NOxyPlot.Wpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=75e952ba404cdbb0 oxyhttp://oxyplot.org/wpf  5   	   Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5   	   *localclr-namespace:DataAnalysisTool  5   	   -   5          � WindowState$  	Maximized=�5	       � Title$ DataAnalysisTool��5
   	   $	��500��6"   $	��800��6/   
������� � Icon$ Image/TPAC.png��5   I   .��5      � -   5         �Name$ 
GRIDWINDOW��5       � RowDefinitions 5   
   �� 5      $	��65* �6   �� 5      $	��35* �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $	��10* �6   $	��200��6+   $	��200��6:   �� 5      $	��90* �6   .+�5   
   �� $��0��6   $��0��6'   $��2��64   $
��Auto=�6E   .��5      �� $��Stretch=�6   $��Stretch=�65   $	��382��6S   .+�5      �� $#��Загрузка данных��6   $��5q�64   $��1q�6?   $	��200��6T   $��64��6c   .��5      ��  �� Orientation$ 
Horizontal=�6!   $��0��6:   $��0��6J   .+�5      �� -   5         ��Name$ btnLoadData��5   !   $	��172��66   $��5,8,5,10q�6B   $��Открыть файл��6T   $��Center=�6k   6�   5      5!      �� 5#      $��Шкала времени��6   $��5q�62   $��1q�6=   $	��200��6R   .��5$      �� -   5         ��Name$ slider��5$      $��0,12,0,0q�6-   {�.  6?   5%      5'      5)        OxyPlot.Wpf.PlotView  5+   
   -   5        	  Name$	 	CscanView��5+      $��0��6*   $��1��67   $��0q�6H   $��Stretch=�6S   $��Stretch=�6q   
�������
   Model
 5,   .   �� *oxyPlot.CScanModel+6S     5/   
   -   5        $	 	DscanView��5/      $��1��6*   $��1��67   $��0q�6G   
�������
 50   .   �� *oxyPlot.DScanModel+52      53      