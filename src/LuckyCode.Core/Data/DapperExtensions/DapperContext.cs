using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using Dapper;
using LuckyCode.Core.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LuckyCode.Core.Data.DapperExtensions
{
    public interface IDbCommand
    {
        int Execute(string sql, object param = null, CommandType? commandType = null, int? commandTimeout = 0);

        IEnumerable<T> Query<T>(string sql, object param = null, CommandType? commandType = null, int? commandTimeout = 0);

        IEnumerable<dynamic> Query(string sql, object param = null, CommandType? commandType = null, int? commandTimeout = 0);

        IEnumerable<dynamic> Query(System.Type type, string sql, object param = null, System.Data.CommandType? commandType = null, int? commandTimeout = 0);
    }
    
    public interface IDapperContext : IDbCommand
    {
        void Batch(Action<ISession> action);
        TResult Batch<TResult>(Func<ISession, TResult> func);
    }

    public interface ISession : IDbCommand
    {
        void BeginTransaction();
        void BeginTransaction(System.Data.IsolationLevel il);
        void CommitTransaction();
        void RollbackTransaction();

        IDbConnection Connection { get; }

        int Execute(CommandDefinition definition);
        
        IEnumerable<T> Query<T>(CommandDefinition definition);
        
        IEnumerable<TReturn> Query<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, object param = null, bool buffered = true, string splitOn = "Id", CommandType? commandType = null, int? commandTimeout = 0);
        IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TReturn>(string sql, Func<TFirst, TSecond, TThird, TReturn> map, object param = null, bool buffered = true, string splitOn = "Id", CommandType? commandType = null, int? commandTimeout = 0);
        IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TReturn> map, object param = null, bool buffered = true, string splitOn = "Id", CommandType? commandType = null, int? commandTimeout = 0);
        IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, object param = null, bool buffered = true, string splitOn = "Id", CommandType? commandType = null, int? commandTimeout = 0);

        SqlMapper.GridReader QueryMultiple(CommandDefinition command);
        SqlMapper.GridReader QueryMultiple(string sql, dynamic param = null, System.Data.CommandType? commandType = null, int? commandTimeout = 0);
    }

    public  class DapperContext : IDapperContext
    {
        private IMainContext _context;
        private static readonly ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>> KeyProperties = new ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>>();
        private static readonly ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>> TypeProperties = new ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>>();
        public DapperContext(IMainContext context)
        {
            _context = context;
        }

        public void Batch(Action<ISession> action)
        {
            var conn = _context.Database.GetDbConnection();
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                action(new Session(conn, _context));
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }
        }

        public TResult Batch<TResult>(Func<ISession, TResult> func)
        {
            var conn = _context.Database.GetDbConnection();
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                return func(new Session(conn, _context));
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }
        }

        class Session : ISession
        {
            readonly IDbConnection _connection;
            IDbTransaction _transaction;
            private IMainContext _context;
            public Session(IDbConnection connection, IMainContext context)
            {
                _connection = connection;
                _transaction = null;
                _context = context;
            }

            public void BeginTransaction()
            {
                if (_transaction == null)
                { _transaction = _connection.BeginTransaction(); }
                if (_transaction != null)
                {
                    _context.Database.UseTransaction((DbTransaction) _transaction);
                }
            }

            public void BeginTransaction(System.Data.IsolationLevel il)
            {
                if (_transaction == null)
                { _transaction = _connection.BeginTransaction(il); }
                if (_transaction != null)
                {
                    _context.Database.UseTransaction((DbTransaction)_transaction);
                }
            }

            public void CommitTransaction()
            {
                if (_transaction != null)
                {
                    _context.SaveChanges();
                    _transaction.Commit();
                }
                _transaction = null;
            }

            public void RollbackTransaction()
            {
                if (_transaction != null)
                {
                    _transaction.Rollback();
                }
                _transaction = null;
            }

            public IDbConnection Connection { get { return _connection; } }

            public int Execute(CommandDefinition command)
            {
                return _connection.Execute(command);
            }
           
            
            /// <summary>
            /// Insert a row into the db
            /// </summary>
            /// <param name="data">Either DynamicParameters or an anonymous type or concrete type</param>
            /// <returns></returns>
            public virtual int? Insert<T>(dynamic data)
            {
                var o = (object)data;
                List<string> paramNames = GetParamNames(o);
                paramNames.Remove("Id");
                string TableName = GetTableName(typeof(T));

                string cols = string.Join(",", paramNames);
                string cols_params = string.Join(",", paramNames.Select(p => "@" + p));
                var sql = "set nocount on insert " + TableName + " (" + cols + ") values (" + cols_params + ") select cast(scope_identity() as bigint)";

                return _connection.Query<int?>(sql, o).Single();
            }

            /// <summary>
            /// Update a record in the DB
            /// </summary>
            /// <param name="id"></param>
            /// <param name="data"></param>
            /// <returns></returns>
            public int Update<T>(dynamic data)
            {
                List<string> paramNames = GetParamNames((object)data);
                var keys = KeyPropertiesCache(typeof(T));
                if (keys==null)
                    throw new Exception("必须要有主键");
                string sql = "";
                sql = sql + String.Format("{0} = @{1}", keys, keys);
                
                string TableName = GetTableName(typeof(T));
                var builder = new StringBuilder();
                builder.Append("update ").Append(TableName).Append(" set ");
                builder.AppendLine(string.Join(",", paramNames.Where(a => a != keys).Select(p => p + "= @" + p)));
                builder.Append("where " + sql);

                DynamicParameters parameters = new DynamicParameters(data);
                return _connection.Execute(builder.ToString(), parameters);
            }
            /// <summary>
            /// Delete a record for the DB
            /// </summary>
            /// <param name="id"></param>
            /// <returns></returns>
            public bool Delete<T>(object id)
            {
                var keys = KeyPropertiesCache(typeof(T));

                if (keys==null)
                    throw new Exception("必须要有主键");
                string TableName = GetTableName(typeof(T));
                return _connection.Execute("delete from " + TableName + " where " + keys + " = @id", new { id }) > 0;
            }
            public IEnumerable<T> Query<T>(CommandDefinition command)
            {
                return _connection.Query<T>(command);
            }

            public IEnumerable<TReturn> Query<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, object param = null, bool buffered = true, string splitOn = "Id", CommandType? commandType = null, int? commandTimeout = 0)
            {
                return _connection.Query(sql, map, param, _transaction, buffered, splitOn, commandTimeout, commandType);
            }

            public IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TReturn>(string sql, Func<TFirst, TSecond, TThird, TReturn> map, object param = null, bool buffered = true, string splitOn = "Id", CommandType? commandType = null, int? commandTimeout = 0)
            {
                return _connection.Query(sql, map, param, _transaction, buffered, splitOn, commandTimeout, commandType);
            }

            public IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TReturn> map, object param = null, bool buffered = true, string splitOn = "Id", CommandType? commandType = null, int? commandTimeout = 0)
            {
                return _connection.Query(sql, map, param, _transaction, buffered, splitOn, commandTimeout, commandType);
            }

            public IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, object param = null, bool buffered = true, string splitOn = "Id", CommandType? commandType = null, int? commandTimeout = 0)
            {
                return _connection.Query(sql, map, param, _transaction, buffered, splitOn, commandTimeout, commandType);
            }

            public int Execute(string sql, object param = null, CommandType? commandType = null, int? commandTimeout = 0)
            {
                return _connection.Execute(sql, param, _transaction, commandTimeout, commandType);
            }

            public IEnumerable<T> Query<T>(string sql, object param = null, CommandType? commandType = null, int? commandTimeout = 0)
            {
                if (typeof(T) == typeof(IDictionary<string, object>))
                {
                    return _connection.Query(sql, param, _transaction, true, commandTimeout, commandType).OfType<T>();
                }
                return _connection.Query<T>(sql, param, _transaction, true, commandTimeout, commandType);
            }

            public IEnumerable<dynamic> Query(string sql, object param = null, CommandType? commandType = null, int? commandTimeout = 0)
            {
                return _connection.Query(sql, param, null, true, commandTimeout, commandType);
            }

            public IEnumerable<dynamic> Query(Type type, string sql, object param = null, CommandType? commandType = null, int? commandTimeout = 0)
            {
                return _connection.Query(type, sql, param, _transaction, true, commandTimeout, commandType);
            }

            public SqlMapper.GridReader QueryMultiple(CommandDefinition command)
            {
                return _connection.QueryMultiple(command);
            }

            public SqlMapper.GridReader QueryMultiple(string sql, dynamic param = null, System.Data.CommandType? commandType = null, int? commandTimeout = 0)
            {
                return _connection.QueryMultiple(new CommandDefinition(sql, param, _transaction, commandTimeout, commandType));
            }

            private string GetTableName(Type type)
            {
                string name="";
                name = _context.FindEntityType(type).Name;
                return name;
            }

            private List<string> GetParamNames(object o)
            {
                if (o is DynamicParameters)
                {
                    return (o as DynamicParameters).ParameterNames.ToList();
                }

                List<string> paramNames;
                if (!paramNameCache.TryGetValue(o.GetType(), out paramNames))
                {
                    paramNames = new List<string>();
                    foreach (IProperty prop in _context.GetProperties(o.GetType()))
                    {
                        paramNames.Add(prop.Name);
                    }
                    paramNameCache[o.GetType()] = paramNames;
                }
                return paramNames;
            }

            private string KeyPropertiesCache(Type type)
            {
                return _context.FindPrimaryKey(type).Name;
            }
        }

        public int Execute(string sql, object param = null, CommandType? commandType = null, int? commandTimeout = 0)
        {
            return Batch(s => s.Execute(sql, param, commandType, commandTimeout));
        }

        public IEnumerable<T> Query<T>(string sql, object param = null, CommandType? commandType = null, int? commandTimeout = 0)
        {
            return Batch(s => s.Query<T>(sql, param, commandType, commandTimeout));
        }

        public IEnumerable<dynamic> Query(string sql, object param = null, CommandType? commandType = null, int? commandTimeout = 0)
        {
            return Batch(s => s.Query(sql, param, commandType, commandTimeout));
        }

        public IEnumerable<object> Query(Type type, string sql, object param = null, CommandType? commandType = null, int? commandTimeout = 0)
        {
            return Batch(s => s.Query(type, sql, param, commandType, commandTimeout));
        }
        static ConcurrentDictionary<Type, List<string>> paramNameCache = new ConcurrentDictionary<Type, List<string>>();
        
        
        

        internal static List<PropertyInfo> TypePropertiesCache(Type type)
        {
            IEnumerable<PropertyInfo> pis;
            if (TypeProperties.TryGetValue(type.TypeHandle, out pis))
            {
                return pis.ToList();
            }

            var properties = type.GetProperties().ToArray();
            TypeProperties[type.TypeHandle] = properties;
            return properties.ToList();
        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class IgnorePropertyAttribute : Attribute
    {
        public IgnorePropertyAttribute(bool ignore)
        {
            Value = ignore;
        }

        public bool Value { get; set; }
    }
}
