using Entitys.DBConfig;
using IRepository.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Messaging;

namespace Repository.Base
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private MySqlDBContext db
        {
            get
            {
                //先从线程缓存CallContext中根据key查找EF容器对象，如果没有则创建,同时保存到缓存中
                object obj = CallContext.GetData(typeof(MySqlDBContext).FullName);
                if (obj == null)
                {
                    //例化EF的上下文容器对象
                    obj = new MySqlDBContext();
                    //将EF的上下文容器对象存入线程缓存CallContext中
                    CallContext.SetData(typeof(MySqlDBContext).FullName, obj);
                }
                //将当前的EF上下文对象返回
                return obj as MySqlDBContext;

            }
        }

        private DbSet<TEntity> _dbSet;

        public BaseRepository()
        {
            _dbSet = db.Set<TEntity>();
        }

        #region 查询
        #region Find 查找实体
        /// <summary>
        /// 查找实体
        /// </summary>
        /// <param name="ID">实体主键值</param>
        /// <returns></returns>
        public TEntity Find(int ID)
        {
            return _dbSet.Find(ID);
        }

        /// <summary>
        /// 查找实体
        /// </summary>
        /// <param name="where">查询Lambda表达式</param>
        /// <returns></returns>
        public TEntity Find(Expression<Func<TEntity, bool>> where)
        {
            return _dbSet.SingleOrDefault(where);
        }
        #endregion

        /// <summary>
        /// 查找实体列表
        /// </summary>
        /// <returns></returns>
        public List<TEntity> FindList()
        {
            return _dbSet.ToList();
        }

        /// <summary>
        /// 查找实体列表
        /// </summary>
        /// <param name="where">查询Lambda表达式</param>
        /// <param name="number">获取的记录数量</param>
        /// <returns></returns>
        public IQueryable<TEntity> FindList(Expression<Func<TEntity, bool>> where, int number)
        {
            return _dbSet.Where(where).Take(number);
        }

        /// <summary>
        /// 单表查询
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public List<TEntity> QueryWhere(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate).ToList();
        }

        /// <summary>
        /// 多表关联查询
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="tableNames"></param>
        /// <returns></returns>
        public List<TEntity> QueryJoin(Expression<Func<TEntity, bool>> predicate, string[] tableNames)
        {
            if (tableNames == null && tableNames.Any() == false)
            {
                throw new Exception("缺少连表名称");
            }

            DbQuery<TEntity> query = _dbSet;

            foreach (var table in tableNames)
            {
                query = query.Include(table);
            }

            return query.Where(predicate).ToList();
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
            if (IsQueryOrderBy)
            {
                return _dbSet.Where(predicate).OrderBy(keySelector).ToList();
            }
            return _dbSet.Where(predicate).OrderByDescending(keySelector).ToList();

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
            rowcount = _dbSet.Count(predicate);
            if (IsQueryOrderBy)
            {
                return _dbSet.Where(predicate).OrderBy(keySelector).Skip((pageIndex - 1) * pagesize).Take(pagesize).ToList();
            }
            else
            {
                return _dbSet.Where(predicate).OrderByDescending(keySelector).Skip((pageIndex - 1) * pagesize).Take(pagesize).ToList();
            }
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
            rowcount = _dbSet.Count(predicate);
            if (IsQueryOrderBy)
            {
                return _dbSet.Where(predicate).OrderBy(keySelector).Skip(pageIndex).Take(pagesize).ToList();
            }
            else
            {
                return _dbSet.Where(predicate).OrderByDescending(keySelector).Skip(pageIndex).Take(pagesize).ToList();
            }
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
            if (model == null)
            {
                throw new Exception("实体不能为空");
            }

            if (propertys.Any() == false)
            {
                throw new Exception("要修改的属性至少要有一个");
            }

            //将model追击到EF容器
            DbEntityEntry entry = db.Entry(model);

            if (entry.State == EntityState.Detached)
            {
                entry.State = EntityState.Unchanged;

                foreach (var item in propertys)
                {
                    entry.Property(item).IsModified = true;
                }

                //关闭EF对于实体的合法性验证参数
                db.Configuration.ValidateOnSaveEnabled = false;
            }
        }

        /// <summary>
        /// 直接查询之后再修改
        /// </summary>
        /// <param name="model"></param>
        public void Edit(TEntity model)
        {
            db.Entry(model).State = EntityState.Modified;
        }

        public void UpdateEntity(TEntity model)
        {
            _dbSet.Attach(model);
            db.Entry(model).State = EntityState.Modified;
            //return db.SaveChanges() > 0;
        }
        #endregion

        #region 删除
        public void Delete(TEntity model, bool isadded)
        {
            if (!isadded)
            {
                _dbSet.Attach(model);
            }
            _dbSet.Remove(model);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="isSave">是否立即保存</param>
        /// <returns>在“isSave”为True时返回受影响的对象的数目，为False时直接返回0</returns>
        public int DeleteEntity(TEntity entity, bool isSave)
        {
            db.Set<TEntity>().Attach(entity);
            db.Entry<TEntity>(entity).State = EntityState.Deleted;
            return isSave ? db.SaveChanges() : 0;
        }

        /// <summary>
        /// 批量删除实体
        /// </summary>
        /// <param name="entities">实体集合</param>
        /// <returns>受影响的对象的数目</returns>
        public int Delete(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
            return db.SaveChanges();
        }
        #endregion

        #region 新增
        public void Add(TEntity model)
        {
            _dbSet.Add(model);
        }
        #endregion

        #region 统一提交
        public int SaverChanges()
        {
            return db.SaveChanges();

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
            return _dbSet.Count();
        }

        /// <summary>
        /// 记录数
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <returns></returns>
        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Count(predicate);
        }
        #endregion

        /// <summary>
        /// 记录是否存在
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <returns></returns>
        public bool IsContains(Expression<Func<TEntity, bool>> predicate)
        {
            return Count(predicate) > 0;
        }

        #region 调用存储过程返回一个指定的TResult
        public List<TResult> RunProc<TResult>(string sql, params object[] pamrs)
        {
            return db.Database.SqlQuery<TResult>(sql, pamrs).ToList();
        }
        #endregion
    }
}
