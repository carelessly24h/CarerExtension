using CarerExtension.Extensions;
using CarerExtension.IO.Excel.Attributes;
using CarerExtension.IO.Excel.Extensions;
using NPOI.SS.UserModel;
using NPOI.SS.Util;

namespace CarerExtension.IO.Excel;

/// <summary>
/// Excelシートの入出力を行うクラス。
/// </summary>
/// <param name="workbook">操作中のワークシートに関連付けられたワークブック。</param>
public abstract class ExcelSheetIO(IWorkbook workbook)
{
    /// <summary>
    /// ワークシート内のセルの位置、値、オプションのデータ形式などを表します。
    /// </summary>
    /// <param name="RowIndex">セルの行インデックス。</param>
    /// <param name="ColumnIndex">セルの列インデックス。</param>
    /// <param name="Value">セルの値。</param>
    /// <param name="DataFormat">セルの内容のデータ形式を指定するオプションの文字列。</param>
    public readonly record struct ExcelCell(int RowIndex, int ColumnIndex, object? Value, string? DataFormat = null);

    #region variable
    /// <summary>
    /// ワークシートに関連付けられたワークブック。
    /// </summary>
    protected readonly IWorkbook workbook = workbook;

    /// <summary>
    /// 操作中のワークシートを表します。
    /// </summary>
    protected ISheet? worksheet;
    #endregion

    #region property
    /// <summary>
    /// 操作中のワークシートを取得します。
    /// </summary>
    /// <exception cref="InvalidOperationException">ワークシートが選択されていない場合にスローされます。</exception>"
    protected ISheet Worksheet => worksheet ?? throw new InvalidOperationException("Worksheet is not selected.");

    /// <summary>
    /// ワークシートのインデックスを取得します。
    /// </summary>
    public int SheetIndex => workbook.GetSheetIndex(Worksheet);

    /// <summary>
    /// ワークシートの名前を取得または設定します。
    /// </summary>
    public string SheetName
    {
        get => Worksheet.SheetName;
        set => workbook.SetSheetName(SheetIndex, value);
    }
    #endregion

    #region method
    #region sheet-cells-writing
    /// <summary>
    /// オブジェクトに定義した値をワークシートに書き込みます。
    /// </summary>
    /// <remarks>
    /// リフレクションを利用して、オブジェクトのプロパティの値をワークシートに書き込みます。
    /// </remarks>
    protected virtual void WriteCells() => WriteCells(this);

    /// <summary>
    /// オブジェクトに定義した値をワークシートに書き込みます。
    /// </summary>
    /// <remarks>
    /// リフレクションを利用して、オブジェクトのプロパティの値をワークシートに書き込みます。
    /// </remarks>
    /// <param name="target">書き込むデータをプロパティに持つオブジェクト。</param>
    /// <param name="offsetRowIndex">書き込む先の基準となるセルの行インデックスを指定します。通常は0を指定します。</param>
    /// <param name="offsetColumnIndex">書き込む先の基準となるセルの列インデックスを指定します。通常は0を指定します。</param>
    protected virtual void WriteCells(object target, int offsetRowIndex = 0, int offsetColumnIndex = 0)
    {
        WriteCellValues(target, offsetRowIndex, offsetColumnIndex);
        WriteAreaValues(target, offsetRowIndex, offsetColumnIndex);
        WriteListValues(target, offsetRowIndex, offsetColumnIndex);
    }

    /// <summary>
    /// セルに値を書き込みます。
    /// </summary>
    /// <param name="target">書き込むデータをプロパティに持つオブジェクト。</param>
    /// <param name="offsetRowIndex">書き込む先の基準となるセルの行インデックスを指定します。通常は0を指定します。</param>
    /// <param name="offsetColumnIndex">書き込む先の基準となるセルの列インデックスを指定します。通常は0を指定します。</param>

    private void WriteCellValues(object target, int offsetRowIndex, int offsetColumnIndex)
    {
        foreach (var (property, attribute) in target.GetPropertySets<ExcelCellAttribute>())
        {
            var rowIndex = attribute.RowIndex + offsetRowIndex;
            var columnIndex = attribute.ColumnIndex + offsetColumnIndex;

            var cell = new ExcelCell(rowIndex, columnIndex, property.GetValue(target), attribute.DataFormat);
            EditCell(cell);
        }
    }

    /// <summary>
    /// セルにエリアとして定義した値を書き込みます。
    /// </summary>
    /// <param name="target">書き込むデータをプロパティに持つオブジェクト。</param>
    /// <param name="offsetRowIndex">書き込む先の基準となるセルの行インデックスを指定します。通常は0を指定します。</param>
    /// <param name="offsetColumnIndex">書き込む先の基準となるセルの列インデックスを指定します。通常は0を指定します。</param>
    private void WriteAreaValues(object target, int offsetRowIndex, int offsetColumnIndex)
    {
        foreach (var (property, attribute) in target.GetPropertySets<ExcelAreaAttribute>())
        {
            var area = property.GetValue(target);
            if (area != null)
            {
                var rowIndex = attribute.TopRowIndex + offsetRowIndex;
                var columnIndex = attribute.TopColumnIndex + offsetColumnIndex;
                WriteCells(area, rowIndex, columnIndex);
            }
        }
    }

    /// <summary>
    /// セルにリストとして定義した値を書き込みます。
    /// </summary>
    /// <param name="target">書き込むデータをプロパティに持つオブジェクト。</param>
    /// <param name="offsetRowIndex">書き込む先の基準となるセルの行インデックスを指定します。通常は0を指定します。</param>
    /// <param name="offsetColumnIndex">書き込む先の基準となるセルの列インデックスを指定します。通常は0を指定します。</param>
    private void WriteListValues(object target, int offsetRowIndex, int offsetColumnIndex)
    {
        foreach (var (property, attribute) in target.GetPropertySets<ExcelAreaListAttribute>())
        {
            var value = property.GetValue(target);
            if (value is IEnumerable list)
            {
                WriteListCells(attribute, offsetRowIndex, offsetColumnIndex, list);
            }
        }
    }

    /// <summary>
    /// セルにリストとして定義した値を書き込みます。
    /// </summary>
    /// <param name="attribute">リストのパラメータを定義する<see cref="ExcelAreaListAttribute"/>。</param>
    /// <param name="offsetRowIndex">書き込む先の基準となるセルの行インデックスを指定します。通常は0を指定します。</param>
    /// <param name="offsetColumnIndex">書き込む先の基準となるセルの列インデックスを指定します。通常は0を指定します。</param>
    /// <param name="list">セルに書き込むデータのコレクション。</param>
    private void WriteListCells(ExcelAreaListAttribute attribute, int offsetRowIndex, int offsetColumnIndex, IEnumerable list)
    {
        // IEnumerable is not have Count.
        var index = 0;

        foreach (var row in list)
        {
            var rowIndex = attribute.TopRowIndex + offsetRowIndex + (attribute.ListRowSize * index);
            var columnIndex = attribute.TopColumnIndex + offsetColumnIndex + (attribute.ListColumnSize * index);

            WriteCells(row, rowIndex, columnIndex);
            index++;
        }
    }
    #endregion

    #region sheet-cells-reading
    /// <summary>
    /// セルの値を読み込み、オブジェクトのプロパティにセットします。
    /// </summary>
    /// <remarks>
    /// リフレクションを利用して、セルの値をオブジェクトのプロパティにセットします。
    /// </remarks>
    protected virtual void ReadCells() => ReadCells(this);

    /// <summary>
    /// セルの値を読み込み、オブジェクトのプロパティにセットします。
    /// </summary>
    /// <remarks>
    /// リフレクションを利用して、セルの値をオブジェクトのプロパティにセットします。
    /// </remarks>
    /// <param name="target">読み込んだデータをセットするオブジェクト。</param>
    /// <param name="offsetRowIndex">読み込み時に基準となるセルの行インデックスを指定します。通常は0を指定します。</param>
    /// <param name="offsetColumnIndex">読み込み時の基準となるセルの列インデックスを指定します。通常は0を指定します。</param>
    protected virtual void ReadCells(object target, int offsetRowIndex = 0, int offsetColumnIndex = 0)
    {
        ReadCellValues(target, offsetRowIndex, offsetColumnIndex);
        ReadAreaValues(target, offsetRowIndex, offsetColumnIndex);
        ReadListValues(target, offsetRowIndex, offsetColumnIndex);
    }

    /// <summary>
    /// セルの値を読み込みます。
    /// </summary>
    /// <param name="target">読み込んだデータをセットするオブジェクト。</param>
    /// <param name="offsetRowIndex">読み込み時に基準となるセルの行インデックスを指定します。通常は0を指定します。</param>
    /// <param name="offsetColumnIndex">読み込み時の基準となるセルの列インデックスを指定します。通常は0を指定します。</param>
    private void ReadCellValues(object target, int offsetRowIndex, int offsetColumnIndex)
    {
        foreach (var (property, attr) in target.GetPropertySets<ExcelCellAttribute>())
        {
            var rowIndex = attr.RowIndex + offsetRowIndex;
            var columnIndex = attr.ColumnIndex + offsetColumnIndex;

            var value = GetValue(property.PropertyType, rowIndex, columnIndex);
            property.SetValue(target, value);
        }
    }

    /// <summary>
    /// エリアとして定義した値を、セルから読み取ります。
    /// </summary>
    /// <param name="target">読み込んだデータをセットするオブジェクト。</param>
    /// <param name="offsetRowIndex">読み込み時に基準となるセルの行インデックスを指定します。通常は0を指定します。</param>
    /// <param name="offsetColumnIndex">読み込み時の基準となるセルの列インデックスを指定します。通常は0を指定します。</param>
    /// <exception cref="InvalidOperationException"><see cref="ExcelAreaAttribute"/>を付与したプロパティのインスタンス生成に失敗した場合にスローされます。</exception>"
    private void ReadAreaValues(object target, int offsetRowIndex, int offsetColumnIndex)
    {
        foreach (var (property, attr) in target.GetPropertySets<ExcelAreaAttribute>())
        {
            var rowIndex = attr.TopRowIndex + offsetRowIndex;
            var columnIndex = attr.TopColumnIndex + offsetColumnIndex;

            var area = property.GetOrCreateValue(target) ?? throw new InvalidOperationException($"Failed to create an instance of the property for {nameof(ExcelAreaAttribute)}");
            ReadCells(area, rowIndex, columnIndex);
            property.SetValue(target, area);
        }
    }

    /// <summary>
    /// リストとして定義した値を、セルから読み取ります。
    /// </summary>
    /// <param name="target">読み込んだデータをセットするオブジェクト。</param>
    /// <param name="offsetRowIndex">読み込み時に基準となるセルの行インデックスを指定します。通常は0を指定します。</param>
    /// <param name="offsetColumnIndex">読み込み時の基準となるセルの列インデックスを指定します。通常は0を指定します。</param>
    /// <exception cref="InvalidOperationException"><see cref="ExcelAreaAttribute"/>を付与したプロパティのインスタンス生成に失敗した場合にスローされます。</exception>"
    private void ReadListValues(object target, int offsetRowIndex, int offsetColumnIndex)
    {
        foreach (var (property, attr) in target.GetPropertySets<ExcelAreaListAttribute>())
        {
            var list = property.GetOrCreateValue<IList>(target) ?? throw new InvalidOperationException($"Failed to create an instance of the property for {nameof(ExcelAreaListAttribute)}");

            var rows = CreateListItems(target, property);
            list.AddRange(rows);

            ReadListCells(attr, offsetRowIndex, offsetColumnIndex, list);
            property.SetValue(target, list);
        }
    }

    /// <summary>
    /// リストで定義したデータを、セルから読み取ります。
    /// </summary>
    /// <param name="target">値をセットするオブジェクト。</param>
    /// <param name="property">値をセットするプロパティ。</param>
    /// <returns>読み取ったデータの一覧。</returns>
    private static IEnumerable<object> CreateListItems(object target, PropertyInfo property)
    {
        var count = GetListCount(target);

        for (var i = 0; i < count; i++)
        {
            var row = CreateListItem(property);
            if (row != null)
            {
                yield return row;
            }
        }
    }

    /// <summary>
    /// 属性で指定したプロパティ、または属性自体から、リストのアイテム数を取得します。
    /// </summary>
    /// <param name="target">アイテム数を定義しているオブジェクト。</param>
    /// <returns>属性、またはプロパティで指定されたリストのアイテム数。</returns>
    /// <exception cref="InvalidOperationException">対象のオブジェクトに<see cref="ExcelAreaListCountAttribute"/>を付与したプロパティがない場合、または付与したプロパティの型が数値でない場合にスローされます。</exception>
    private static int GetListCount(object target)
    {
        var (property, attr) = target.GetPropertySets<ExcelAreaListCountAttribute>().First();

        // The priority is
        // 1. property value.
        // 2. attribute count.
        // 3. exception throw.
        var count = property?.GetValue(target) switch
        {
            int v => v,
            long v => (int?)v,
            float v => (int?)v,
            double v => (int?)v,
            decimal v => (int?)v,
            null => null,
            _ => throw new InvalidOperationException($"The value specified for {nameof(ExcelAreaListCountAttribute)} is not a number."),
        };
        return count ?? attr.Count ?? throw new InvalidOperationException($"{nameof(ExcelAreaListCountAttribute)} does not exist.");
    }

    /// <summary>
    /// リストのアイテムを作成します。
    /// </summary>
    /// <param name="property">リストを定義しているプロパティ。</param>
    /// <returns>リストのアイテムのインスタンス。</returns>
    private static object? CreateListItem(PropertyInfo property)
    {
        var rowTypes = property.PropertyType.GetGenericArguments();
        return Activator.CreateInstance(rowTypes[0]);
    }

    /// <summary>
    /// リストの各行のセルを読み取ります。
    /// </summary>
    /// <param name="attribute">リストのパラメータを定義する<see cref="ExcelAreaListAttribute"/>。</param>
    /// <param name="offsetRowIndex">リスト行の読み取り開始位置を計算する際に適用する行オフセット。</param>
    /// <param name="offsetColumnIndex">リスト列の読み取り開始位置を計算する際に適用する列オフセット。</param>
    /// <param name="list">Excel領域内の対応するセルからデータを取り込むリスト。</param>
    private void ReadListCells(ExcelAreaListAttribute attribute, int offsetRowIndex, int offsetColumnIndex, IList list)
    {
        for (var index = 0; index < list.Count; index++)
        {
            var rowIndex = attribute.TopRowIndex + offsetRowIndex + (attribute.ListRowSize * index);
            var columnIndex = attribute.TopColumnIndex + offsetColumnIndex + (attribute.ListColumnSize * index);

            if (list[index] is object row)
            {
                ReadCells(row, rowIndex, columnIndex);
            }
        }
    }
    #endregion

    #region sheet-operation
    /// <summary>
    /// シート名を指定してシートを作成します。
    /// </summary>
    /// <param name="sheetName">シート名。</param>
    public virtual void CreateSheet(string sheetName)
    {
        workbook.CreateSheet(sheetName);
    }

    /// <summary>
    /// インデックスを指定してシートを選択します。
    /// </summary>
    /// <param name="index">選択するシートのインデックス。</param>
    /// <exception cref="ArgumentException">インデックスが一致するシートが見つからない場合にスローされます。</exception>
    public virtual void SelectSheet(int index)
    {
        worksheet = workbook.GetSheetAt(index) ?? throw new ArgumentException($"{index} is illegal index.");
    }

    /// <summary>
    /// シート名を指定してシートを選択します。
    /// </summary>
    /// <param name="sheetName">選択するシートのシート名。</param>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="ArgumentException">シート名が一致するシートが見つからない場合にスローされます。</exception>
    public virtual void SelectSheet(string sheetName)
    {
        worksheet = workbook.GetSheet(sheetName) ?? throw new ArgumentException($"{sheetName} is not found.");
    }

    /// <summary>
    /// シートを書き込みます。
    /// </summary>
    /// <exception cref="NotImplementedException">継承先のクラスで上書きされませんでした。書き込み時の処理を定義してください。</exception>
    public virtual void Write() => throw new NotImplementedException();

    /// <summary>
    /// シートを読み込みます。
    /// </summary>
    /// <exception cref="NotImplementedException">継承先のクラスで上書きされませんでした。読み込み時の処理を定義してください。</exception>
    public virtual void Read() => throw new NotImplementedException();

    /// <summary>
    /// シートをコピーします。
    /// </summary>
    /// <param name="destinationSheetName">コピー先のシート名。</param>
    protected virtual void Copy(string destinationSheetName) =>
        Worksheet.CopyTo(workbook, destinationSheetName, true, true);
    #endregion

    #region cell-operation
    /// <summary>
    /// 行をコピーします。
    /// </summary>
    /// <param name="sourceIndex">コピー元の行インデックス。</param>
    /// <param name="destinationIndex">コピー先の行インデックス。</param>
    public virtual void CopyRow(int sourceIndex, int destinationIndex) =>
        SheetUtil.CopyRow(Worksheet, sourceIndex, destinationIndex);
    #endregion

    #region cell-reading
    /// <summary>
    /// 指定されたセルから値を取得します。
    /// </summary>
    /// <param name="dataType">指定されたセルのデータの型を表します。</param>
    /// <param name="rowIndex">値を取得するセルの行インデックス。</param>
    /// <param name="columnIndex">値を取得するセルの列インデックス。</param>
    /// <returns>指定されたセルの値。</returns>
    /// <exception cref="InvalidOperationException">未対応の型が<paramref name="dataType"/>に指定された場合にスローされます。</exception>
    private object? GetValue(Type dataType, int rowIndex, int columnIndex) =>
        dataType switch
        {
            Type t when t == typeof(bool?) => GetBooleanValue(rowIndex, columnIndex),
            Type t when t == typeof(DateTime?) => GetDateTimeValue(rowIndex, columnIndex),
            Type t when t == typeof(double?) => GetDoubleValue(rowIndex, columnIndex),
            Type t when t == typeof(int?) => GetInt32Value(rowIndex, columnIndex),
            Type t when t == typeof(long?) => GetInt64Value(rowIndex, columnIndex),
            Type t when t == typeof(string) => GetStringValue(rowIndex, columnIndex),
            _ => throw new InvalidOperationException($"{dataType} is not supported."),
        };

    /// <summary>
    /// 指定されたセルの値を取得します。
    /// </summary>
    /// <param name="rowIndex">値を取得するセルの行インデックス。</param>
    /// <param name="columnIndex">値を取得するセルの列インデックス。</param>
    /// <returns>指定されたセルの値。</returns>
    protected virtual bool? GetBooleanValue(int rowIndex, int columnIndex) =>
        GetCellValue<bool?>(Worksheet.GetCell(rowIndex, columnIndex));

    /// <summary>
    /// 指定されたセルの値を取得します。
    /// </summary>
    /// <param name="rowIndex">値を取得するセルの行インデックス。</param>
    /// <param name="columnIndex">値を取得するセルの列インデックス。</param>
    /// <returns>指定されたセルの値。</returns>
    protected virtual DateTime? GetDateTimeValue(int rowIndex, int columnIndex) =>
        GetDoubleValue(rowIndex, columnIndex) switch
        {
            double v => DateTime.FromOADate(v),
            _ => null,
        };

    /// <summary>
    /// 指定されたセルの値を取得します。
    /// </summary>
    /// <param name="rowIndex">値を取得するセルの行インデックス。</param>
    /// <param name="columnIndex">値を取得するセルの列インデックス。</param>
    /// <returns>指定されたセルの値。</returns>
    protected virtual double? GetDoubleValue(int rowIndex, int columnIndex) =>
        GetCellValue<double?>(Worksheet.GetCell(rowIndex, columnIndex));

    /// <summary>
    /// 指定されたセルの値を取得します。
    /// </summary>
    /// <param name="rowIndex">値を取得するセルの行インデックス。</param>
    /// <param name="columnIndex">値を取得するセルの列インデックス。</param>
    /// <returns>指定されたセルの値。</returns>
    protected virtual int? GetInt32Value(int rowIndex, int columnIndex) =>
        (int?)GetCellValue<double?>(Worksheet.GetCell(rowIndex, columnIndex));

    /// <summary>
    /// 指定されたセルの値を取得します。
    /// </summary>
    /// <param name="rowIndex">値を取得するセルの行インデックス。</param>
    /// <param name="columnIndex">値を取得するセルの列インデックス。</param>
    /// <returns>指定されたセルの値。</returns>
    protected virtual long? GetInt64Value(int rowIndex, int columnIndex) =>
        (long?)GetCellValue<double?>(Worksheet.GetCell(rowIndex, columnIndex));

    /// <summary>
    /// 指定されたセルの値を取得します。
    /// </summary>
    /// <param name="rowIndex">値を取得するセルの行インデックス。</param>
    /// <param name="columnIndex">値を取得するセルの列インデックス。</param>
    /// <returns>指定されたセルの値。</returns>
    protected virtual string? GetStringValue(int rowIndex, int columnIndex) =>
        GetCellValue<string?>(Worksheet.GetCell(rowIndex, columnIndex));

    /// <summary>
    /// セルの値を取得します。
    /// </summary>
    /// <remarks>
    /// セルの値がnull、またはセルが空の場合は、<typeparamref name="T"/>のデフォルト値を返します。
    /// </remarks>
    /// <typeparam name="T">セルの値の型。</typeparam>
    /// <param name="cell">データを取得するセル。</param>
    /// <returns>Excelのシートから取得したセルの値。</returns>
    /// <exception cref="FormatException">セルのデータが指定された<typeparamref name="T"/>の型と異なる場合にスローされます。</exception>
    private static T? GetCellValue<T>(ICell cell) =>
        GetCellValue(cell.CellType, cell) switch
        {
            T v => v,
            "" => default,
            null => default,
            var v => throw new FormatException($"Invalid value type. T: ${typeof(T)} Cell-Type: ${v.GetType()} "),
        };

    /// <summary>
    /// セルの値を取得します。
    /// </summary>
    /// <remarks>
    /// セルの型に応じた値を取得します。
    /// </remarks>
    /// <param name="type">セルの型。</param>
    /// <param name="cell">値を取得するセル。</param>
    /// <returns>セルの値。</returns>
    private static object? GetCellValue(CellType type, ICell cell) =>
        type switch
        {
            CellType.Boolean => cell.BooleanCellValue,
            CellType.Numeric => cell.NumericCellValue,
            CellType.String => cell.StringCellValue,
            CellType.Blank => "",
            CellType.Formula => GetCellValue(cell.CachedFormulaResultType, cell),
            _ => null,
        };
    #endregion

    #region cell-writing
    /// <summary>
    /// 複数のセルに値を設定します。
    /// </summary>
    /// <param name="cells">設定する値のパラメータのコレクション。</param>
    protected virtual void EditCell(IEnumerable<ExcelCell> cells)
    {
        foreach (var cell in cells)
        {
            EditCell(cell);
        }
    }

    /// <summary>
    /// セルに値を設定します。
    /// </summary>
    /// <param name="cell">設定する値のパラメータ。</param>
    protected virtual void EditCell(ExcelCell cell)
    {
        var c = Worksheet.GetCell(cell.RowIndex, cell.ColumnIndex);
        EditCell(c, cell.Value, cell.DataFormat);
    }

    /// <summary>
    /// セルに値を設定します。
    /// </summary>
    /// <param name="rowIndex">値を設定するセルの行インデックス。</param>
    /// <param name="columnIndex">値を設定するセルの列インデックス。</param>
    /// <param name="value">セル設定する値。</param>
    /// <param name="dataFormat">セルに設定するデータ形式。</param>
    protected virtual void EditCell(int rowIndex, int columnIndex, object? value, string? dataFormat = null)
    {
        var c = Worksheet.GetCell(rowIndex, columnIndex);
        EditCell(c, value, dataFormat);
    }

    /// <summary>
    /// セルに値を設定します。
    /// </summary>
    /// <param name="cell">設定する対象のセル。</param>
    /// <param name="value">セルに設定する値。</param>
    /// <param name="dataFormat">セルに設定するデータ形式</param>
    protected virtual void EditCell(ICell cell, object? value, string? dataFormat = null)
    {
        cell.SetCellValue(value);

        var format = dataFormat ?? GetDefaultFormat(value);
        if (format != null)
        {
            workbook.SetCellFormat(cell, format);
        }
    }

    /// <summary>
    /// セルに設定するデフォルトのデータ形式を取得します。
    /// </summary>
    /// <param name="value">セルに設定する値</param>
    /// <returns>データ形式</returns>
    protected virtual string? GetDefaultFormat(object? value) =>
        value switch
        {
            DateTime => "YYYY/M/D",
            double => "#.0",
            _ => null,
        };
    #endregion
    #endregion
}
