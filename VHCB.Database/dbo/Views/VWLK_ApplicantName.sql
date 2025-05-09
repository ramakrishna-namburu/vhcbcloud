﻿CREATE VIEW dbo.VWLK_ApplicantName
AS
SELECT DISTINCT 
                      dbo.ProjectApplicant.ProjectId, dbo.ProjectApplicant.ApplicantId, dbo.AppName.Applicantname, dbo.AppName.ApplicantAbbrv, dbo.ProjectApplicant.LkApplicantRole, 
                      dbo.LookupValues.Description, dbo.ApplicantAppName.DefName
FROM         dbo.AppName INNER JOIN
                      dbo.ApplicantAppName ON dbo.AppName.AppNameID = dbo.ApplicantAppName.AppNameID INNER JOIN
                      dbo.ProjectApplicant ON dbo.ApplicantAppName.ApplicantID = dbo.ProjectApplicant.ApplicantId LEFT OUTER JOIN
                      dbo.LookupValues ON dbo.ProjectApplicant.LkApplicantRole = dbo.LookupValues.TypeID
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'VWLK_ApplicantName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'= 1350
      End
   End
End
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'VWLK_ApplicantName';


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
         Begin Table = "AppName"
            Begin Extent = 
               Top = 6
               Left = 464
               Bottom = 114
               Right = 617
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "ApplicantAppName"
            Begin Extent = 
               Top = 6
               Left = 245
               Bottom = 114
               Right = 426
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "ProjectApplicant"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 114
               Right = 207
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "LookupValues"
            Begin Extent = 
               Top = 175
               Left = 24
               Bottom = 283
               Right = 175
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 3660
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'VWLK_ApplicantName';

