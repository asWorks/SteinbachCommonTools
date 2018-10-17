using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using C1.WPF;
using C1.WPF.DataGrid;
using System.Xml.Serialization;
using System.IO;
using System.Windows.Threading;

namespace libc1DatagridTools
{
    public class ManageC1GridColumns
    {
        //bool doneLoading;
        //private DispatcherTimer _saveSettingTimer;
        C1DataGrid grid;

        public ManageC1GridColumns(C1DataGrid c1Grid)
        {
            grid = c1Grid;
        }


        //private void SaveDataGridSettingsAsync()
        //{
        //    if (!doneLoading)
        //        return;
        //    _saveSettingTimer.Start();
        //}


        public void LoadGridSettings(string Settings)
        {
            //c1DataGrid1.Columns["ImageUrl"].DisplayIndex = 0;
            if (HasDataGridSettings(Settings))
            {
                var dataGridSettings = GetDataGridSettings(Settings);
                grid.FrozenColumnCount = dataGridSettings.FrozenColumnsCount;
                var sortDict = new Dictionary<C1.WPF.DataGrid.DataGridColumn, UserColumnSettings.SortGroupState>();
                var groupDict = new Dictionary<C1.WPF.DataGrid.DataGridColumn, UserColumnSettings.SortGroupState>();
                var filterList = new List<KeyValuePair<C1.WPF.DataGrid.DataGridColumn, DataGridFilterState>>();
                foreach (var columnSetting in dataGridSettings.ColumnSettings)
                {
                    var column = grid.Columns[columnSetting.ColumnName];
                    if (column != null)
                    {
                        column.DisplayIndex = columnSetting.DisplayIndex;
                        sortDict[column] = columnSetting.Sort;
                        groupDict[column] = columnSetting.Group;
                        filterList.Add(new KeyValuePair<C1.WPF.DataGrid.DataGridColumn, C1.WPF.DataGrid.DataGridFilterState>(column, columnSetting.Filter));
                        if (columnSetting.Width > 0)
                        {
                            column.Width = new C1.WPF.DataGrid.DataGridLength(columnSetting.Width);
                        }
                    }
                }
                if (filterList.Count > 0)
                {
                    try
                    {
                        if (grid.IsFilteringRowsAllowed)
                        {
                            grid.FilterBy(filterList.ToArray());
                        }
                        
                    }
                    catch (Exception)
                    {
                    }

                }
                if (sortDict.Count > 0)
                {
                    //var sortparam = sortDict.OrderBy(pair => pair.Value.Index).Select(pair => new KeyValuePair<C1.WPF.DataGrid.DataGridColumn, DataGridSortDirection>(pair.Key, pair.Value.Direction)).ToArray();
                    try
                    {
                        if (grid.IsSortingRowsAllowed)
                        {
                             grid.SortBy(sortDict.OrderBy(pair => pair.Value.Index).Select(pair => new KeyValuePair<C1.WPF.DataGrid.DataGridColumn, DataGridSortDirection>(pair.Key, pair.Value.Direction)).ToArray());
                        }
                       
                    }
                    catch (Exception)
                    {
                    }

                }
                if (groupDict.Count > 0)
                {

                    //var groupparam = groupDict.OrderBy(pair => pair.Value.Index).Select(pair => new KeyValuePair<C1.WPF.DataGrid.DataGridColumn, DataGridSortDirection>(pair.Key, pair.Value.Direction)).ToArray();
                    //var x = groupDict.OrderBy(pair => pair.Value.Index).Where(pair=>pair.Value.Direction!= DataGridSortDirection.None).Select(pair => new KeyValuePair<C1.WPF.DataGrid.DataGridColumn, DataGridSortDirection>(pair.Key, pair.Value.Direction)).ToArray();

                    try
                    {
                        if (grid.IsGroupingRowsAllowed)
                        {
                             grid.GroupBy(groupDict.OrderBy(pair => pair.Value.Index).
                                               Where(pair => pair.Value.Direction != DataGridSortDirection.None)
                                               .Select(pair => new KeyValuePair<C1.WPF.DataGrid.DataGridColumn, DataGridSortDirection>(pair.Key, pair.Value.Direction)).ToArray());
                        }
                       
                    }
                    catch (Exception)
                    {

                    }


                }
            }
            else
            {
                // SaveDataGridSettingsAsync();
                SaveDataGridSettings();
            }
            //foreach (var column in grid.Columns)
            //{
            //    column.WidthChanged += new EventHandler<C1.WPF.PropertyChangedEventArgs<C1.WPF.DataGrid.DataGridLength>>(column_WidthChanged);
            //}
        }


        public string SaveDataGridSettings()
        {
            //_saveSettingTimer.Stop();
            DataGridSettings settings = new DataGridSettings();

            // get state from grid
            settings.FrozenColumnsCount = grid.FrozenColumnCount;
            foreach (var column in grid.Columns)
            {
                UserColumnSettings columnSetting = new UserColumnSettings();
                columnSetting.ColumnName = column.Name;
                columnSetting.DisplayIndex = column.DisplayIndex;
                columnSetting.Sort = new UserColumnSettings.SortGroupState() { Direction = column.SortState.Direction, Index = column.SortState.Index };
                columnSetting.Group = new UserColumnSettings.SortGroupState() { Direction = column.GroupState.Direction, Index = column.GroupState.Index };
                columnSetting.Filter = column.FilterState;
                columnSetting.Width = column.Width.IsAbsolute ? column.Width.Value : -1;

                settings.ColumnSettings.Add(columnSetting);
            }

            // serialize DataGridSettings class to XML
            XmlSerializer ser = CreateSerializer();
            StringWriter sw = new StringWriter();
            ser.Serialize(sw, settings);

            // save to user settings
            //   Properties.Settings.Default.DataGridSettings = sw.ToString();


            // save all settings
            //  Properties.Settings.Default.Save();

            return sw.ToString();
        }

        private DataGridSettings GetDataGridSettings(string GridSettings)
        {
            // StringReader sr = new StringReader(Properties.Settings.Default.DataGridSettings);
            StringReader sr = new StringReader(GridSettings);
            try
            {
                XmlSerializer ser = CreateSerializer();
                return (DataGridSettings)ser.Deserialize(sr);
            }
            catch
            {

            }

            return new DataGridSettings();
        }

        private static XmlSerializer CreateSerializer()
        {
            // DataGridComboBoxFilter needs List<object>
           
            return new XmlSerializer(typeof(DataGridSettings), new[] { typeof(List<object>) });

        }

        private bool HasDataGridSettings(string GridSettings)
        {
            return !string.IsNullOrEmpty(GridSettings);
        }


    }

    public class UserColumnSettings
    {
        public class SortGroupState
        {
            public DataGridSortDirection Direction { get; set; }
            public int Index { get; set; }
        }
        public string ColumnName { get; set; }
        public int DisplayIndex { get; set; }
        public SortGroupState Sort { get; set; }
        public SortGroupState Group { get; set; }
        public DataGridFilterState Filter { get; set; }
        public double Width { get; set; }
    }

    public class DataGridSettings
    {
        public DataGridSettings()
        {
            ColumnSettings = new List<UserColumnSettings>();
        }
        public List<UserColumnSettings> ColumnSettings { get; set; }
        public int FrozenColumnsCount { get; set; }
    }
}
