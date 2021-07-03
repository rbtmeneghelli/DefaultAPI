using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace DefaultAPI.Configuration
{
    /// <summary>
    /// Exemplo utilizando esses metodos na controller
    /// HttpContext.Session.Get<DateTime>(SessionKeyTime) >> Resgatando o valor da Session
    /// if (HttpContext.Session.Get<DateTime>(SessionKeyTime) == default) >> Verificando se a session ta nula
    /// HttpContext.Session.Set<DateTime>(SessionKeyTime, currentTime >> Armazenando valor na session
    /// </summary>
    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }
        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }
    }
}
