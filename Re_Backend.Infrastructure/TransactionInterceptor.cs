using Castle.DynamicProxy;
using Microsoft.Extensions.Logging;
using Re_Backend.Common.Attributes;
using Re_Backend.Common.Transactions;
using System.Reflection;

namespace Re_Backend.Infrastructure
{
    public class TransactionInterceptor : IInterceptor
    {
        private readonly TransactionHandler _handler;
        private readonly ILogger _logger;

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
                    var returnType = method.ReturnType;

                    if (returnType == typeof(Task))
                    {
                        invocation.ReturnValue = InterceptAsync((Task)invocation.ReturnValue);
                    }
                    else // Task<T>
                    {
                        var resultType = returnType.GetGenericArguments()[0];
                        var methodInfo = typeof(TransactionInterceptor)
                            .GetMethod(nameof(InterceptAsyncGeneric), BindingFlags.NonPublic | BindingFlags.Instance)!
                            .MakeGenericMethod(resultType);
                        invocation.ReturnValue = methodInfo.Invoke(this, new object[] { invocation.ReturnValue });
                    }
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

        private bool IsAsyncMethod(MethodInfo method)
        {
            return method.ReturnType == typeof(Task) || (method.ReturnType.IsGenericType && method.ReturnType.GetGenericTypeDefinition() == typeof(Task<>));
        }

        private async Task InterceptAsync(Task task)
        {
            _handler.BeginTran();
            try
            {
                await task;
                _handler.Commit();
            }
            catch
            {
                _handler.Rollback();
                throw;
            }
        }

        private async Task<T> InterceptAsyncGeneric<T>(Task<T> task)
        {
            _handler.BeginTran();
            try
            {
                var result = await task;
                _handler.Commit();
                return result;
            }
            catch
            {
                _handler.Rollback();
                throw;
            }
        }
    }
}
