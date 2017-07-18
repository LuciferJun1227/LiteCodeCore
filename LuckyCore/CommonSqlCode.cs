using CodeSmith.Engine;
using SchemaExplorer;
using System;
using System.Windows.Forms.Design;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Diagnostics;
namespace Shoucao.Templates
{
	[DefaultProperty("ChooseSourceDatabase")]
	public class CommonSqlCode : CodeTemplate
	{
		public string GetMemberVariableDeclarationStatement(ColumnSchema column)
		{
			return GetMemberVariableDeclarationStatement("private", column);
		}
		public string GetMemberVariableDeclarationStatementProc(ParameterSchema param)
		{
			return GetMemberVariableDeclarationStatementProc("private", param);
		}
		public string GetMemberVariableDeclarationStatementProc(string protectionLevel, ParameterSchema param)
{
	string statement = protectionLevel + " ";
	statement += GetCSharpVariableTypeProc(param) + " " + GetMemberVariableNameProc(param);
	string defaultValue = GetMemberVariableDefaultValueProc(param);
	if (defaultValue != "")
	{
		statement += " = " + defaultValue;
	}
	statement += ";";
	return statement;
}
public string GetMemberVariableDeclarationStatementView(ViewColumnSchema column)
		{
			return GetMemberVariableDeclarationStatementView("private", column);
		}
public string GetMemberVariableDeclarationStatementView(string protectionLevel, ViewColumnSchema column)
{
	string statement = protectionLevel + " ";
	statement += GetCSharpVariableTypeView(column) + " " + GetMemberVariableNameView(column);
	string defaultValue = GetMemberVariableDefaultValueView(column);
	if (defaultValue != "")
	{
		statement += " = " + defaultValue;
	}
	statement += ";";
	return statement;
}
public string GetMemberVariableDeclarationStatement(string protectionLevel, ColumnSchema column)
{
	string statement = protectionLevel + " ";
	statement += GetCSharpVariableType(column) + " " + GetMemberVariableName(column);
	string defaultValue = GetMemberVariableDefaultValue(column);
	if (defaultValue != "")
	{
		statement += " = " + defaultValue;
	}
	statement += ";";
	return statement;
}
//取得存储过程参数
public string GetProcConstructorParameters(SchemaExplorer.CommandSchema param)
{
	string res="";
	foreach(ParameterSchema perameter in param.InputParameters)
	{
		res+=GetCSharpVariableTypeProc(perameter)+" "+GetCamelCaseName(perameter.Name.Substring(1,perameter.Name.Length-1))+",\n\t\t\t";
	}
	return res.Substring(0, res.Length - 5);
	}
public string GetConstructorParameters(SchemaExplorer.TableSchema SourceTable)
{
	string ret = "";
	foreach(ColumnSchema column in SourceTable.Columns)
	{
		ret += GetCSharpVariableType(column) + " " + GetCamelCaseName(GetPropertyName(column)) + ",\n\t\t\t";
	}
	return ret.Substring(0, ret.Length - 5);
}
public string GetProcAssignValue(SchemaExplorer.CommandSchema param)
{
	string res="";
	foreach(ParameterSchema perameter in param.InputParameters)
	{
		res+=GetMemberVariableNameProc(perameter)+"="+GetCamelCaseName(perameter.Name.Substring(1,perameter.Name.Length-1))+";\n\t\t\t";
	
	}
	return res;
}
public string GetAssignValue(SchemaExplorer.TableSchema SourceTable)
{
	string ret = "";
	foreach(ColumnSchema column in SourceTable.Columns)
	{
		ret += GetMemberVariableName(column) +  (new String(' ', GetPropertyNameMaxLength(SourceTable) - GetPropertyNameLength(column))) + " = " + GetCamelCaseName(GetPropertyName(column)) + ";\n\t\t\t";
	}
	return ret;
}

public string GetReaderAssignmentStatement(ColumnSchema column, int index)
{
	string statement = "if (!reader.IsDBNull(" + index.ToString() + ")) ";
	statement += GetMemberVariableName(column) + " = ";
	if (column.Name.EndsWith("TypeCode")) statement += "(" + column.Name + ")";
	statement += "reader." + GetReaderMethod(column) + "(" + index.ToString() + ");";
	return statement;
}

public string GetCamelCaseName(string value)
{
	return value.Substring(0, 1).ToLower() + value.Substring(1);
}
public string GetSupperCaseName(string value)
{
	return value.Substring(0, 1).ToUpper() + value.Substring(1);
}
public string GetPrimaryKey(string value)
{
	return "_"+GetCamelCaseName(value);
}
public string GetPrimaryKeyName(string value)
{
	return GetCamelCaseName(value);
}
public string ProcEntityProperty(ParameterSchema param)
{
	string propertyName = param.Name.Substring(1);
	return propertyName.Substring(0,1).ToUpper()+propertyName.Substring(1);
}
public string GetMemberVariableNameProc(ParameterSchema column)
{
	//string propertyName = GetPropertyName(column);
	string propertyName = column.Name.Substring(1);
	string memberVariableName = "_" +propertyName.Substring(0,1).ToLower()+propertyName.Substring(1);
	return memberVariableName;
}
public string GetMemberVariableNameView(ViewColumnSchema column)
{
	//string propertyName = GetPropertyName(column);
	string propertyName = column.Name;
	string memberVariableName = "_" + GetCamelCaseName(propertyName);
	return memberVariableName;
}
public string GetMemberVariableName(ColumnSchema column)
{
	//string propertyName = GetPropertyName(column);
	string propertyName = column.Name;
	string memberVariableName = "_" + GetCamelCaseName(propertyName);
	return memberVariableName;
}
public string GetTableName(SchemaExplorer.TableSchema SourceTable)
{
	return "_"+GetCamelCaseName(SourceTable.Name);
}
public string SetParameters(ColumnSchema column,string database,string command ,string entity)
{
	return database+".AddInParameter("+command+",\""+GetParameter(column)+"\",DbType."+column.DataType+","+entity+"."+column.Name+");";
}
public string GetParameter(ColumnSchema column)
{
	return "@"+column.Name;
	//return "\"@"+column.Name+"\"";
}
public string GetPropertyName(ColumnSchema column)
{
	string propertyName = column.Name;
	//if (propertyName == column.Table.Name + "Name") return "Name";
	//if (propertyName == column.Table.Name + "Description") return "Description";
	//if (propertyName.EndsWith("TypeCode")) propertyName = propertyName.Substring(0, propertyName.Length - 4);
	return propertyName;
}

public int GetPropertyNameLength(ColumnSchema column)
{
	return (GetPropertyName(column)).Length;
}
public int GetPropertyNameMaxLength(SchemaExplorer.TableSchema SourceTable)
{
	int ret = 0;
	foreach(ColumnSchema column in SourceTable.Columns)
	{
		ret = ret < GetPropertyNameLength(column)?GetPropertyNameLength(column):ret;
	}
	return ret;
}
public string GetMemberVariableDefaultValueProc(ParameterSchema column)
{
	
	switch (column.DataType)
	{
		case DbType.Guid:
		{
			return "Guid.Empty";
		}
		case DbType.AnsiString:
		case DbType.AnsiStringFixedLength:
		case DbType.String:
		case DbType.StringFixedLength:
		{
			return "String.Empty";
		}
		case DbType.Boolean:
		return "false";
		case DbType.Int32:
		return "0";
		case DbType.DateTime:
		return "new DateTime(1900,1,1)";
		case DbType.Single:
		return "0";
		case DbType.Double:
		return "0";
		default:
		{
			return "";
		}
	}
}
public string GetCSharpVariableTypeProc(ParameterSchema column)
{
	if (column.Name.EndsWith("TypeCode")) return column.Name;
	switch (column.DataType)
	{
		case DbType.AnsiString: return "string";
		case DbType.AnsiStringFixedLength: return "string";
		case DbType.Binary: return "byte[]";
		case DbType.Boolean: return "bool";
		case DbType.Byte: return "byte";
		case DbType.Currency: return "decimal";
		case DbType.Date: return "DateTime";
		case DbType.DateTime: return "DateTime";
		case DbType.Decimal: return "decimal";
		case DbType.Double: return "double";
		case DbType.Guid: return "Guid";
		case DbType.Int16: return "short";
		case DbType.Int32: return "int";
		case DbType.Int64: return "long";
		case DbType.Object: return "object";
		case DbType.SByte: return "sbyte";
		case DbType.Single: return "float";
		case DbType.String: return "string";
		case DbType.StringFixedLength: return "string";
		case DbType.Time: return "TimeSpan";
		case DbType.UInt16: return "ushort";
		case DbType.UInt32: return "uint";
		case DbType.UInt64: return "ulong";
		case DbType.VarNumeric: return "decimal";
		case DbType.Xml: return "string";
		default:
		{
			return "__UNKNOWN__" + column.NativeType;
		}
	}
}
public string GetSqlDbTypeProce(ParameterSchema peramete)
{
	switch (peramete.NativeType)
	{
		case "bigint": return "BigInt";
		case "binary": return "Binary";
		case "bit": return "Bit";
		case "char": return "Char";
		case "datetime": return "DateTime";
		case "decimal": return "Decimal";
		case "float": return "Float";
		case "image": return "Image";
		case "int": return "Int";
		case "money": return "Money";
		case "nchar": return "NChar";
		case "ntext": return "NText";
		case "numeric": return "Decimal";
		case "nvarchar": return "NVarChar";
		case "real": return "Real";
		case "smalldatetime": return "SmallDateTime";
		case "smallint": return "SmallInt";
		case "smallmoney": return "SmallMoney";
		case "sql_variant": return "Variant";
		case "sysname": return "NChar";
		case "text": return "Text";
		case "timestamp": return "Timestamp";
		case "tinyint": return "TinyInt";
		case "uniqueidentifier": return "UniqueIdentifier";
		case "varbinary": return "VarBinary";
		case "varchar": return "VarChar";
		default: return "__UNKNOWN__" + peramete.NativeType;
	}
}
public string GetMemberVariableDefaultValue(ColumnSchema column)
{
	if(column.IsPrimaryKeyMember)
	{
	    return "";
	}
	switch (column.DataType)
	{
		case DbType.Guid:
		{
			return "Guid.Empty";
		}
		case DbType.AnsiString:
		case DbType.AnsiStringFixedLength:
		case DbType.String:
		case DbType.StringFixedLength:
		{
			return "String.Empty";
		}
		case DbType.Boolean:
		return "false";
		case DbType.Int32:
		return "0";
		case DbType.DateTime:
		return "new DateTime(1900,1,1)";
		case DbType.Single:
		return "0";
		case DbType.Double:
		return "0";
		default:
		{
			return "";
		}
	}
}
public string GetMemberVariableDefaultValueView(ViewColumnSchema column)
{
	
	switch (column.DataType)
	{
		case DbType.Guid:
		{
			return "Guid.Empty";
		}
		case DbType.AnsiString:
		case DbType.AnsiStringFixedLength:
		case DbType.String:
		case DbType.StringFixedLength:
		{
			return "String.Empty";
		}
		case DbType.Boolean:
		return "false";
		case DbType.Int32:
		return "0";
		case DbType.DateTime:
		return "new DateTime(1900,1,1)";
		case DbType.Single:
		return "0";
		case DbType.Double:
		return "0";
		default:
		{
			return "";
		}
	}
}
public string GetStorePreceduresParamer( ParameterSchema perameter)
{
	return perameter.Name;
}
public string GetStorePreceduresParamerName(ParameterSchema perameter)
{
  return "obj"+perameter.Name.Replace('@','.');
}
public string GetStorePreceduresParamerDataType(ParameterSchema perameter)
{
	switch (perameter.DataType)
	{
		case DbType.AnsiString: return "string";
		case DbType.AnsiStringFixedLength: return "string";
		case DbType.Binary: return "byte[]";
		case DbType.Boolean: return "bool";
		case DbType.Byte: return "byte";
		case DbType.Currency: return "decimal";
		case DbType.Date: return "DateTime";
		case DbType.DateTime: return "DateTime";
		case DbType.Decimal: return "decimal";
		case DbType.Double: return "double";
		case DbType.Guid: return "Guid";
		case DbType.Int16: return "short";
		case DbType.Int32: return "int";
		case DbType.Int64: return "long";
		case DbType.Object: return "object";
		case DbType.SByte: return "sbyte";
		case DbType.Single: return "float";
		case DbType.String: return "string";
		case DbType.StringFixedLength: return "string";
		case DbType.Time: return "TimeSpan";
		case DbType.UInt16: return "ushort";
		case DbType.UInt32: return "uint";
		case DbType.UInt64: return "ulong";
		case DbType.VarNumeric: return "decimal";
		case DbType.Xml: return "string";
		default:
		{
			return "__UNKNOWN__" + perameter.NativeType;
		}
	}
	
	}
public string GetCSharpVariableType(ColumnSchema column)
{
	if (column.Name.EndsWith("TypeCode")) return column.Name;
    if(column.AllowDBNull)
    {
	switch (column.DataType)
	{
		case DbType.AnsiString: return "string";
		case DbType.AnsiStringFixedLength: return "string";
		case DbType.Binary: return "byte[]";
		case DbType.Boolean: return "bool?";
		case DbType.Byte: return "byte?";
		case DbType.Currency: return "decimal?";
		case DbType.Date: return "DateTime?";
		case DbType.DateTime: return "DateTime?";
		case DbType.Decimal: return "decimal?";
		case DbType.Double: return "double?";
		case DbType.Guid: return "Guid?";
		case DbType.Int16: return "short?";
		case DbType.Int32: return "int?";
		case DbType.Int64: return "long?";
		case DbType.Object: return "object?";
		case DbType.SByte: return "sbyte?";
		case DbType.Single: return "float?";
		case DbType.String: return "string";
		case DbType.StringFixedLength: return "string";
		case DbType.Time: return "TimeSpan";
		case DbType.UInt16: return "ushort?";
		case DbType.UInt32: return "uint?";
		case DbType.UInt64: return "ulong?";
		case DbType.VarNumeric: return "decimal?";
		case DbType.Xml: return "string";
		default:
		{
			return "__UNKNOWN__" + column.NativeType;
		}
	}
    }
    else
    {
        switch (column.DataType)
	{
		case DbType.AnsiString: return "string";
		case DbType.AnsiStringFixedLength: return "string";
		case DbType.Binary: return "byte[]";
		case DbType.Boolean: return "bool";
		case DbType.Byte: return "byte";
		case DbType.Currency: return "decimal";
		case DbType.Date: return "DateTime";
		case DbType.DateTime: return "DateTime";
		case DbType.Decimal: return "decimal";
		case DbType.Double: return "double";
		case DbType.Guid: return "Guid";
		case DbType.Int16: return "short";
		case DbType.Int32: return "int";
		case DbType.Int64: return "long";
		case DbType.Object: return "object";
		case DbType.SByte: return "sbyte";
		case DbType.Single: return "float";
		case DbType.String: return "string";
		case DbType.StringFixedLength: return "string";
		case DbType.Time: return "TimeSpan";
		case DbType.UInt16: return "ushort";
		case DbType.UInt32: return "uint";
		case DbType.UInt64: return "ulong";
		case DbType.VarNumeric: return "decimal";
		case DbType.Xml: return "string";
		default:
		{
			return "__UNKNOWN__" + column.NativeType;
		}
	}
    }
}
public string GetCSharpVariableTypeView(ViewColumnSchema column)
{
	if (column.Name.EndsWith("TypeCode")) return column.Name;
	switch (column.DataType)
	{
		case DbType.AnsiString: return "string";
		case DbType.AnsiStringFixedLength: return "string";
		case DbType.Binary: return "byte[]";
		case DbType.Boolean: return "bool";
		case DbType.Byte: return "byte";
		case DbType.Currency: return "decimal";
		case DbType.Date: return "DateTime";
		case DbType.DateTime: return "DateTime";
		case DbType.Decimal: return "decimal";
		case DbType.Double: return "double";
		case DbType.Guid: return "Guid";
		case DbType.Int16: return "short";
		case DbType.Int32: return "int";
		case DbType.Int64: return "long";
		case DbType.Object: return "object";
		case DbType.SByte: return "sbyte";
		case DbType.Single: return "float";
		case DbType.String: return "string";
		case DbType.StringFixedLength: return "string";
		case DbType.Time: return "TimeSpan";
		case DbType.UInt16: return "ushort";
		case DbType.UInt32: return "uint";
		case DbType.UInt64: return "ulong";
		case DbType.VarNumeric: return "decimal";
		case DbType.Xml: return "string";
		default:
		{
			return "__UNKNOWN__" + column.NativeType;
		}
	}
}
public string GetReaderMethod(ColumnSchema column)
{
	switch (column.DataType)
	{
		case DbType.Byte:
		{
			return "GetByte";
		}
		case DbType.Int16:
		{
			return "GetInt16";
		}
		case DbType.Int32:
		{
			return "GetInt32";
		}
		case DbType.Int64:
		{
			return "GetInt64";
		}
		case DbType.AnsiStringFixedLength:
		case DbType.AnsiString:
		case DbType.String:
		case DbType.StringFixedLength:
		{
			return "GetString";
		}
		case DbType.Boolean:
		{
			return "GetBoolean";
		}
		case DbType.Guid:
		{
			return "GetGuid";
		}
		case DbType.Currency:
		case DbType.Decimal:
		{
			return "GetDecimal";
		}
		case DbType.DateTime:
		case DbType.Date:
		{
			return "GetDateTime";
		}
		case DbType.Binary:
		{
			return "GetBytes";
		}
        case DbType.Double:
        {
            return "GetDouble";
        }
        case DbType.Single:
        {
            return "GetFloat";
        }
		default:
		{
			return "__SQL__" + column.DataType;
		}
	}
}
public string GetClassNameView(ViewSchema view)
{
	if(view==null)
	return "";
	return view.Name;
}
public string GetClassName(TableSchema table)
{
	if(table==null)
	{
	    return null;
	}
	return table.Name ;
}

public string GetSqlDbType(ColumnSchema column)
{
	switch (column.NativeType)
	{
		case "bigint": return "BigInt";
		case "binary": return "Binary";
		case "bit": return "Bit";
		case "char": return "Char";
		case "datetime": return "DateTime";
		case "decimal": return "Decimal";
		case "float": return "Float";
		case "image": return "Image";
		case "int": return "Int";
		case "money": return "Money";
		case "nchar": return "NChar";
		case "ntext": return "NText";
		case "numeric": return "Decimal";
		case "nvarchar": return "NVarChar";
		case "real": return "Real";
		case "smalldatetime": return "SmallDateTime";
		case "smallint": return "SmallInt";
		case "smallmoney": return "SmallMoney";
		case "sql_variant": return "Variant";
		case "sysname": return "NChar";
		case "text": return "Text";
		case "timestamp": return "Timestamp";
		case "tinyint": return "TinyInt";
		case "uniqueidentifier": return "UniqueIdentifier";
		case "varbinary": return "VarBinary";
		case "varchar": return "VarChar";
		default: return "__UNKNOWN__" + column.NativeType;
	}
}

public string GetPrimaryKeyType(TableSchema table)
{
	if (table.PrimaryKey != null)
	{
		if (table.PrimaryKey.MemberColumns.Count == 1)
		{
			return GetCSharpVariableType(table.PrimaryKey.MemberColumns[0]);
		}
		else
		{
			throw new ApplicationException("This template will not work on primary keys with more than one member column.");
		}
	}
	else
	{
		throw new ApplicationException("This template will only work on tables with a primary key.");
	}
}

	public  string GetFileName(SchemaExplorer.TableSchema SourceTable)
	{
		return this.GetClassName(SourceTable) + ".cs";
	}
}
}