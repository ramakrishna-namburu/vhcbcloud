﻿CREATE VIEW dbo.VW_RPT_Cons_Violations
AS
SELECT     dbo.ConserveViolations.ConserveViolationsID, dbo.Conserve.ProjectID, dbo.Project.Proj_num AS [Project Number], 
                      dbo.VWLK_ProjectNames.Description AS [Project Name], dbo.LookupValues.Description AS Violation, LookupValues_1.Description AS Disposition, 
                      dbo.ConserveViolations.ReqDate AS [Required Date], dbo.ConserveViolations.DispDate AS [Disposition Date], dbo.ConserveViolations.Comments, 
                      dbo.ConserveViolations.URL
FROM         dbo.LookupValues AS LookupValues_1 RIGHT OUTER JOIN
                      dbo.Project INNER JOIN
                      dbo.Conserve ON dbo.Project.ProjectId = dbo.Conserve.ProjectID INNER JOIN
                      dbo.VWLK_ProjectNames ON dbo.Project.ProjectId = dbo.VWLK_ProjectNames.ProjectID INNER JOIN
                      dbo.ConserveViolations ON dbo.Conserve.ConserveID = dbo.ConserveViolations.ConserveID LEFT OUTER JOIN
                      dbo.LookupValues ON dbo.ConserveViolations.LkConsViol = dbo.LookupValues.TypeID ON LookupValues_1.TypeID = dbo.ConserveViolations.LkDisp
WHERE     (dbo.VWLK_ProjectNames.DefName = 1) AND (dbo.ConserveViolations.RowIsActive = 1)
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'VW_RPT_Cons_Violations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'00
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 2160
         Alias = 1680
         Table = 2190
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'VW_RPT_Cons_Violations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Conserve"
            Begin Extent = 
               Top = 6
               Left = 263
               Bottom = 114
               Right = 414
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Project"
            Begin Extent = 
               Top = 6
               Left = 452
               Bottom = 114
               Right = 603
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "VWLK_ProjectNames"
            Begin Extent = 
               Top = 6
               Left = 641
               Bottom = 114
               Right = 793
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "LookupValues_1"
            Begin Extent = 
               Top = 6
               Left = 831
               Bottom = 114
               Right = 982
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "LookupValues"
            Begin Extent = 
               Top = 6
               Left = 1020
               Bottom = 114
               Right = 1171
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ConserveViolations"
            Begin Extent = 
               Top = 159
               Left = 29
               Bottom = 267
               Right = 211
            End
            DisplayFlags = 280
            TopColumn = 6
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 11
         Width = 284
         Width = 15', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'VW_RPT_Cons_Violations';

