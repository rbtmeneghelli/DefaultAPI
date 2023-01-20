using System;
using System.Collections.Generic;
using System.Text;

namespace DefaultAPI.Infra.CrossCutting
{
    public interface IThreadService : IDisposable
    {
        bool RunMethodWithThreadPool(int value);

        bool RunMethodWithThreadParallel(List<int> list);
    }
}
