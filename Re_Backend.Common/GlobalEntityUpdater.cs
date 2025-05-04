using System.Reflection;

namespace Re_Backend.Common
{
    // 通用的实体属性更新工具类
    public static class GlobalEntityUpdater
    {
        // 泛型方法，用于更新实体对象的属性
        public static void UpdateEntity<T>(T oldEntity, T newEntity) where T : class
        {
            if (oldEntity == null || newEntity == null)
            {
                return;
            }

            // 获取实体类型
            Type type = typeof(T);
            // 获取实体的所有属性
            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                // 假设主键属性名为 "Id"，不更新主键
                if (property.CanRead && property.CanWrite)
                {
                    // 获取新对象的属性值
                    object newValue = property.GetValue(newEntity);
                    // 获取旧对象的属性值
                    object oldValue = property.GetValue(oldEntity);

                    // 检查新值是否为空，如果为空则使用旧值
                    if (newValue == null || (newValue is string str && string.IsNullOrEmpty(str)))
                    {
                        continue;
                    }

                    // 对比属性值，如果不同则更新
                    if (!Equals(newValue, oldValue))
                    {
                        property.SetValue(oldEntity, newValue);
                    }
                }
            }
        }
    }
}
