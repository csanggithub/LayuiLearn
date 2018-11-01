using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace IServices.Base
{
    public interface IBaseServices<TEntity> where TEntity : class
    {
        #region 查询

        /// <summary>
        /// 查找实体
        /// </summary>
        /// <param name="ID">实体主键值</param>
        /// <returns></returns>
        TEntity Find(int ID);

        /// <summary>
        /// 查找实体
        /// </summary>
        /// <param name="where">查询Lambda表达式</param>
        /// <returns></returns>
        TEntity Find(Expression<Func<TEntity, bool>> where);

        /// <summary>
        /// 查找实体列表
        /// </summary>
        /// <returns></returns>
        List<TEntity> FindList();

        /// <summary>
        /// 查找实体列表
        /// </summary>
        /// <param name="where">查询Lambda表达式</param>
        /// <param name="number">获取的记录数量</param>
        /// <returns></returns>
        IQueryable<TEntity> FindList(Expression<Func<TEntity, bool>> where, int number);

        /// <summary>
        /// 单表查询
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        List<TEntity> QueryWhere(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 多表关联查询
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="tableNames"></param>
        /// <returns></returns>
        List<TEntity> QueryJoin(Expression<Func<TEntity, bool>> predicate, string[] tableNames);
        /// <summary>
        /// 升序查询还是降序查询
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="keySelector"></param>
        /// <param name="IsQueryOrderBy"></param>
        /// <returns></returns>
        List<TEntity> QueryOrderBy<TKey>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TKey>> keySelector, bool IsQueryOrderBy);

        /// <summary>
        /// 升序分页查询还是降序分页
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="pageIndex">第几页</param>
        /// <param name="pagesize">一页多少条</param>
        /// <param name="rowcount">返回共多少条</param>
        /// <param name="predicate">查询条件</param>
        /// <param name="keySelector">排序字段</param>
        /// <param name="IsQueryOrderBy">true为升序 false为降序</param>
        /// <returns></returns>
        List<TEntity> QueryByPage<TKey>(int pageIndex, int pagesize, out int rowcount, Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TKey>> keySelector, bool IsQueryOrderBy);
        /// <summary>
        /// 从第几条开始用于DataTables
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="pageIndex">第几页开始</param>
        /// <param name="pagesize">一页几条</param>
        /// <param name="rowcount">一共多少条</param>
        /// <param name="predicate">条件</param>
        /// <param name="keySelector">排序关键字</param>
        /// <param name="IsQueryOrderBy">升序还是降序</param>
        /// <returns></returns>
        List<TEntity> QueryByBeginPage<TKey>(int pageIndex, int pagesize, out int rowcount, Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TKey>> keySelector, bool IsQueryOrderBy);
        #endregion

        #region 编辑
        /// <summary>
        /// 通过传入的model加需要修改的字段来更改数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="propertys"></param>
        void Edit(TEntity model, string[] propertys);

        /// <summary>
        /// 直接查询之后再修改
        /// </summary>
        /// <param name="model"></param>
        void Edit(TEntity model);

        void UpdateEntity(TEntity model);
        #endregion

        #region 删除
        void Delete(TEntity model, bool isadded);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="isSave">是否立即保存</param>
        /// <returns>在“isSave”为True时返回受影响的对象的数目，为False时直接返回0</returns>
        int DeleteEntity(TEntity entity, bool isSave);

        /// <summary>
        /// 批量删除实体
        /// </summary>
        /// <param name="entities">实体集合</param>
        /// <returns>受影响的对象的数目</returns>
        int Delete(IEnumerable<TEntity> entities);

        #endregion

        #region 新增
        void Add(TEntity model);
        #endregion

        #region 统一提交
        int SaverChanges();
        #endregion

        /// <summary>
        /// 记录数
        /// </summary>
        /// <returns></returns>
        int Count();

        /// <summary>
        /// 记录数
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <returns></returns>
        int Count(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 记录是否存在
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <returns></returns>
        bool IsContains(Expression<Func<TEntity, bool>> predicate);

        #region 调用存储过程返回一个指定的TResult
        List<TResult> RunProc<TResult>(string sql, params object[] pamrs);
        #endregion
    }
}
