using IRepository.Base;
using IServices.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Services.Base
{
    public class BaseServices<TEntity> : IBaseServices<TEntity> where TEntity : class
    {
        public IBaseRepository<TEntity> baseDal;

        #region 查询
        #region Find 查找实体
        /// <summary>
        /// 查找实体
        /// </summary>
        /// <param name="ID">实体主键值</param>
        /// <returns></returns>
        public TEntity Find(int ID)
        {
            return baseDal.Find(ID);
        }

        /// <summary>
        /// 查找实体
        /// </summary>
        /// <param name="where">查询Lambda表达式</param>
        /// <returns></returns>
        public TEntity Find(Expression<Func<TEntity, bool>> where)
        {
            return baseDal.Find(where);
        }
        #endregion

        /// <summary>
        /// 查找实体列表
        /// </summary>
        /// <returns></returns>
        public List<TEntity> FindList()
        {
            return baseDal.FindList();
        }

        /// <summary>
        /// 查找实体列表
        /// </summary>
        /// <param name="where">查询Lambda表达式</param>
        /// <param name="number">获取的记录数量</param>
        /// <returns></returns>
        public IQueryable<TEntity> FindList(Expression<Func<TEntity, bool>> where, int number)
        {
            return baseDal.FindList(where, number);
        }

        /// <summary>
        /// 单表查询
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public List<TEntity> QueryWhere(Expression<Func<TEntity, bool>> predicate)
        {
            return baseDal.QueryWhere(predicate);
        }

        /// <summary>
        /// 多表关联查询
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="tableNames"></param>
        /// <returns></returns>
        public List<TEntity> QueryJoin(Expression<Func<TEntity, bool>> predicate, string[] tableNames)
        {
            return baseDal.QueryJoin(predicate, tableNames);

        }

        /// <summary>
        /// 升序查询还是降序查询
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="keySelector"></param>
        /// <param name="IsQueryOrderBy"></param>
        /// <returns></returns>
        public List<TEntity> QueryOrderBy<TKey>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TKey>> keySelector, bool IsQueryOrderBy)
        {
            return baseDal.QueryOrderBy(predicate, keySelector, IsQueryOrderBy);
        }

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
        public List<TEntity> QueryByPage<TKey>(int pageIndex, int pagesize, out int rowcount, Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TKey>> keySelector, bool IsQueryOrderBy)
        {

            return baseDal.QueryByPage(pageIndex, pagesize, out rowcount, predicate, keySelector, IsQueryOrderBy);

        }

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
        public List<TEntity> QueryByBeginPage<TKey>(int pageIndex, int pagesize, out int rowcount, Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TKey>> keySelector, bool IsQueryOrderBy)
        {
            return baseDal.QueryByBeginPage(pageIndex, pagesize, out rowcount, predicate, keySelector, IsQueryOrderBy);
        }
        #endregion

        #region 编辑
        /// <summary>
        /// 通过传入的model加需要修改的字段来更改数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="propertys"></param>
        public void Edit(TEntity model, string[] propertys)
        {
            baseDal.Edit(model, propertys);
        }

        /// <summary>
        /// 直接查询之后再修改
        /// </summary>
        /// <param name="model"></param>
        public void Edit(TEntity model)
        {
            baseDal.Edit(model);
        }

        public void UpdateEntity(TEntity model)
        {
            baseDal.UpdateEntity(model);
        }
        #endregion

        #region 删除
        public void Delete(TEntity model, bool isadded)
        {
            baseDal.Delete(model, isadded);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="isSave">是否立即保存</param>
        /// <returns>在“isSave”为True时返回受影响的对象的数目，为False时直接返回0</returns>
        public int DeleteEntity(TEntity entity, bool isSave)
        {
            return baseDal.DeleteEntity(entity, isSave);
        }

        /// <summary>
        /// 批量删除实体
        /// </summary>
        /// <param name="entities">实体集合</param>
        /// <returns>受影响的对象的数目</returns>
        public int Delete(IEnumerable<TEntity> entities)
        {
            return baseDal.Delete(entities);
        }
        #endregion

        #region 新增
        public void Add(TEntity model)
        {
            baseDal.Add(model);
        }
        #endregion

        #region 统一提交
        public int SaverChanges()
        {
            return baseDal.SaverChanges();
        }
        #endregion

        //记录数
        #region Count

        /// <summary>
        /// 记录数
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            return baseDal.Count();
        }

        /// <summary>
        /// 记录数
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <returns></returns>
        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return baseDal.Count(predicate);
        }
        #endregion

        /// <summary>
        /// 记录是否存在
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <returns></returns>
        public bool IsContains(Expression<Func<TEntity, bool>> predicate)
        {
            return baseDal.Count(predicate) > 0;
        }

        #region 调用存储过程返回一个指定的TResult
        public List<TResult> RunProc<TResult>(string sql, params object[] pamrs)
        {
            return baseDal.RunProc<TResult>(sql, pamrs);
        }
        #endregion
    }
}
