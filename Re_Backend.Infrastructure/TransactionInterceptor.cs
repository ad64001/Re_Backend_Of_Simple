using Castle.DynamicProxy;
using Re_Backend.Common.Attributes;
using Re_Backend.Common.Transactions;
using System.Reflection;

namespace Re_Backend.Infrastructure
{
    public class TransactionInterceptor : IInterceptor
    {
        private readonly TransactionHandler _handler;

        public TransactionInterceptor(TransactionHandler handler)
        {
            _handler = handler;
        }

        public void Intercept(IInvocation invocation)
        {
            var method = invocation.MethodInvocationTarget ?? invocation.Method;
            var attribute = method.GetCustomAttribute<UseTranAttribute>();

            if (attribute == null)
            {
                invocation.Proceed();
                return;
            }

            try
            {
                Before(invocation);
                if (IsAsyncMethod(method))
                {
                    invocation.Proceed();
                    var task = (Task)invocation.ReturnValue;
                    task.ContinueWith(t =>
                    {
                        if (t.IsFaulted)
                        {
                            _handler.Rollback();
                        }
                        else
                        {
                            After(invocation);
                        }
                    }).Wait();
                }
                else
                {
                    invocation.Proceed();
                    After(invocation);
                }
            }
            catch (Exception ex)
            {
                _handler.Rollback();
                throw;
            }
        }

        private void Before(IInvocation invocation)
        {
            _handler.BeginTran();
        }

        private void After(IInvocation invocation)
        {
            _handler.Commit();
        }

        private bool IsAsyncMethod(MethodInfo method)
        {
            return method.ReturnType == typeof(Task) || (method.ReturnType.IsGenericType && method.ReturnType.GetGenericTypeDefinition() == typeof(Task<>));
        }
    }
}
