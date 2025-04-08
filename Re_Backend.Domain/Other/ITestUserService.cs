using Re_Backend.Common.Attributes;
using Re_Backend.Common.SqlConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Re_Backend.Domain.Other
{
    public interface ITestUserService
    {
        void AddUser(TestUser user);
        List<TestUser> GetAllUsers();
        void DoSomethingWithTransaction();
        DbContext GetDbContext();
    }

    [Injectable(IsSingleton = true)]
    public class TestUserService : ITestUserService
    {
        private readonly DbContext _dbContext;

        public DbContext DbContext => _dbContext;

        public TestUserService(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddUser(TestUser user)
        {
            DbContext.Db.Insertable(user).ExecuteCommand();
        }

        public List<TestUser> GetAllUsers()
        {
            return DbContext.Db.Queryable<TestUser>().ToList();
        }

        [UseTran]
        public void DoSomethingWithTransaction()
        {
            // 模拟一些数据库操作
            //DbContext.Db.Insertable(new User { Name = "User2" , Age = 33}).ExecuteCommand();
            // 场景1：插入第一条数据后抛出异常（验证插入回滚）
            DbContext.Db.Insertable(new TestUser { Name = "User36", Age = 10 }).ExecuteCommand();
            DoSomethingWithTransaction2();
            //throw new DivideByZeroException("模拟第一个异常");
            //throw new DivideByZeroException("模拟第一个异常");

        }

        [UseTran]
        private void DoSomethingWithTransaction2()
        {
            // 模拟一些数据库操作
            //DbContext.Db.Insertable(new User { Name = "User2" , Age = 33}).ExecuteCommand();
            // 场景1：插入第一条数据后抛出异常（验证插入回滚）
            DbContext.Db.Insertable(new TestUser { Name = "User39", Age = 37 }).ExecuteCommand();
            

        }

        public DbContext GetDbContext()
        {
            return DbContext;
        }
    }
}
