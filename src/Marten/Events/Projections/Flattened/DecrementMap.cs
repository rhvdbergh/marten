using System;
using JasperFx.CodeGeneration.Model;
using Weasel.Postgresql.Tables;

namespace Marten.Events.Projections.Flattened;

internal class DecrementMap: IColumnMap
{
    private TableColumn? _column;

    public DecrementMap(string columnName)
    {
        ColumnName = columnName;
    }

    public Table.ColumnExpression ResolveColumn(Table table)
    {
        _column = table.ColumnFor(ColumnName);
        return _column == null ? table.AddColumn<int>(ColumnName) : new Table.ColumnExpression(table, _column);
    }

    public string UpdateFieldSql(Table table)
    {
        return $"{ColumnName} = {table.Identifier.Name}.{ColumnName} - 1";
    }

    public bool RequiresInput { get; } = false;

    public string ToInsertExpression(Table table)
    {
        return "0";
    }

    public string ColumnName { get; }

    public string ToValueAccessorCode(Variable eventVariable)
    {
        throw new NotSupportedException();
    }
}
