using System;
using ApiProject.Services.Repositories;

namespace ApiProject.Services.UnitsOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IMechaSwitchRepository Switches { get; }
        int Complete();
    }

}