using SqlSugar;


namespace Re_Backend.Common.Transactions
{
    public class TransactionHandler
    {
        private readonly SqlSugarClient _db;

        public TransactionHandler(SqlSugarClient db)
        {
            _db = db;
        }

        public void BeginTran()
        {
            lock (this)
            {
                try
                {
                    if (!IsTransactionActive())
                    {
                        _db.Ado.BeginTran();
                    }
                    // 如果已经有事务，不做任何操作，即加入现有事务
                }
                catch
                {
                    // 可根据需要记录日志
                    throw; // 重新抛出异常，确保调用方知晓事务开启失败
                }
            }
        }

        public void Rollback()
        {
            lock (this)
            {
                if (IsTransactionActive())
                {
                    try
                    {
                        _db.Ado.RollbackTran();
                    }
                    catch
                    {
                        // 回滚失败时强制清理事务对象
                        _db.Ado.Transaction = null;
                        // 记录日志或处理异常
                        //TODO
                        throw;
                    }
                }
            }
        }

        public void Commit()
        {
            lock (this)
            {
                if (IsTransactionActive())
                {
                    try
                    {
                        _db.Ado.CommitTran();
                    }
                    catch
                    {
                        // 提交失败时尝试回滚
                        _db.Ado.RollbackTran();
                        throw; // 抛出异常通知调用方
                    }
                    finally
                    {
                        // 确保事务对象被清理
                        _db.Ado.Transaction = null;
                    }
                }
            }
        }

        // 新增私有方法：通过检查 Transaction 对象是否为 null 判断事务状态
        private bool IsTransactionActive()
        {
            return _db.Ado.Transaction != null;
        }
    }
}
