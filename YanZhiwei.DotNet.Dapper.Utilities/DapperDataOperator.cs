﻿namespace YanZhiwei.DotNet.Dapper.Utilities
{
    using DotNet2.Utilities.Operator;
    using global::Dapper;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    
    /// <summary>
    /// Dapper 数据库操作帮助类，默认是sql Server
    /// </summary>
    /// 时间：2016-01-19 16:21
    /// 备注：
    public abstract class DapperDataOperator
    {
        #region Fields
        
        /// <summary>
        /// 连接字符串
        /// </summary>
        public readonly string ConnectString = string.Empty;
        
        #endregion Fields
        
        #region Constructors
        
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connectString">连接字符串</param>
        /// 时间：2016-01-19 16:21
        /// 备注：
        public DapperDataOperator(string connectString)
        {
            ValidateOperator.Begin().NotNullOrEmpty(connectString, "连接字符串");
            ConnectString = connectString;
        }
        
        #endregion Constructors
        
        #region Methods
        
        /// <summary>
        /// 创建SqlConnection连接对象，需要打开
        /// </summary>
        /// <returns>IDbConnection</returns>
        /// 时间：2016-01-19 16:22
        /// 备注：
        public abstract IDbConnection CreateConnection();
        
        /// <summary>
        /// ExecuteDataTable
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="sql">sql 语句</param>
        /// <param name="parameters">查询参数</param>
        /// <returns>DataTable</returns>
        /// 时间：2016-01-19 16:22
        /// 备注：
        public virtual DataTable ExecuteDataTable<T>(string sql, T parameters)
        where T : class
        {
            using(IDbConnection sqlConnection = CreateConnection())
            {
                DataTable _table = new DataTable();
                _table.Load(sqlConnection.ExecuteReader(sql, parameters));
                return _table;
            }
        }
        
        /// <summary>
        /// ExecuteDataTable
        /// </summary>
        /// <param name="sql">sql 语句</param>
        /// <returns>DataTable</returns>
        /// 时间:2016/10/15 20:07
        /// 备注:
        public virtual DataTable ExecuteDataTable(string sql)
        {
            using(IDbConnection sqlConnection = CreateConnection())
            {
                DataTable _table = new DataTable();
                _table.Load(sqlConnection.ExecuteReader(sql, null));
                return _table;
            }
        }
        
        /// <summary>
        /// ExecuteNonQuery
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="sql">sql 语句</param>
        /// <param name="parameters">查询参数</param>
        /// <returns>影响行数</returns>
        /// 时间：2016-01-19 16:23
        /// 备注：
        public virtual int ExecuteNonQuery<T>(string sql, T parameters)
        where T : class
        {
            using(IDbConnection sqlConnection = CreateConnection())
            {
                return sqlConnection.Execute(sql, parameters);
            }
        }
        
        /// <summary>
        /// ExecuteNonQuery
        /// </summary>
        /// <param name="sql">sql 语句</param>
        /// <returns>影响行数</returns>
        public virtual int ExecuteNonQuery(string sql)
        {
            using(IDbConnection sqlConnection = CreateConnection())
            {
                return sqlConnection.Execute(sql, null);
            }
        }
        
        /// <summary>
        /// ExecuteReader
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="sql">sql 语句</param>
        /// <param name="parameters">查询参数</param>
        /// <returns>IDataReader</returns>
        /// 时间：2016-01-19 16:24
        /// 备注：
        public virtual IDataReader ExecuteReader<T>(string sql, T parameters)
        where T : class
        {
            IDbConnection sqlConnection = CreateConnection();
            
            return sqlConnection.ExecuteReader(sql, parameters);
        }
        
        /// <summary>
        /// ExecuteReader
        /// </summary>
        /// <param name="sql">sql 语句</param>
        /// <returns>IDataReader</returns>
        public virtual IDataReader ExecuteReader(string sql)
        {
            IDbConnection sqlConnection = CreateConnection();
            return sqlConnection.ExecuteReader(sql, null);
        }
        
        /// <summary>
        /// ExecuteScalar
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="sql">sql 语句</param>
        /// <param name="parameters">查询参数</param>
        /// <returns>返回对象</returns>
        /// 时间：2016-01-19 16:25
        /// 备注：
        public virtual object ExecuteScalar<T>(string sql, T parameters)
        where T : class
        {
            using(IDbConnection sqlConnection = CreateConnection())
            {
                return sqlConnection.ExecuteScalar(sql, parameters, null, null, null);
            }
        }
        
        /// <summary>
        /// ExecuteScalar
        /// </summary>
        /// <param name="sql">sql 语句</param>
        /// <returns>返回对象</returns>
        public virtual object ExecuteScalar(string sql)
        {
            using(IDbConnection sqlConnection = CreateConnection())
            {
                return sqlConnection.ExecuteScalar(sql, null, null, null, null);
            }
        }
        
        /// <summary>
        /// 返回实体类
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="sql">sql 语句</param>
        /// <param name="parameters">查询参数</param>
        /// <returns>实体类</returns>
        /// 时间：2016-01-19 16:25
        /// 备注：
        public virtual T Query<T>(string sql, T parameters)
        where T : class
        {
            T _result = null;
            using(IDbConnection sqlConnection = CreateConnection())
            {
                _result = sqlConnection.Query<T>(sql, parameters).FirstOrDefault();
            }
            
            return _result;
        }
        
        /// <summary>
        /// 返回集合
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="sql">sql 语句</param>
        /// <param name="parameters">查询参数</param>
        /// <returns>集合</returns>
        /// 时间：2016-01-19 16:25
        /// 备注：
        public virtual List<T> QueryList<T>(string sql, T parameters)
        where T : class
        {
            List<T> _result = null;
            using(IDbConnection sqlConnection = CreateConnection())
            {
                _result = sqlConnection.Query<T>(sql, parameters).ToList();
            }
            
            return _result;
        }
        
        #endregion Methods
    }
}