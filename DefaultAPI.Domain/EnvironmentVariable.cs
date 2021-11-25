using System;
using System.Collections.Generic;
using System.Text;

namespace DefaultAPI.Domain
{
    public static class EnvironmentVariables
    {
        // Lendo variáveis de ambiente do JSON
        public readonly static string varProcess = Environment.GetEnvironmentVariable("variavelJson", EnvironmentVariableTarget.Process);

        // Lendo variáveis de ambiente da maquina
        public readonly static string varMachine = Environment.GetEnvironmentVariable("variavelMachine", EnvironmentVariableTarget.User);

        public static void SetEnvironmentVariables()
        {
            // Escrevendo variáveis de ambiente do JSON
            Environment.SetEnvironmentVariable("variavelJson", "valor da variavel json", EnvironmentVariableTarget.Process);

            // Lendo variáveis de ambiente da maquina
            Environment.SetEnvironmentVariable("variavelMachine", "valor da variavel maquina", EnvironmentVariableTarget.User);
        }
    }
}
