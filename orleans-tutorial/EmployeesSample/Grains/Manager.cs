using System.Collections.Generic;
using System.Threading.Tasks;
using GrainInterfaces;
using Orleans;

namespace Grains
{
    public class Manager : Grain, IManager
    {
        public override Task OnActivateAsync()
        {
            _me = this.GrainFactory.GetGrain<IEmployee>(this.GetPrimaryKey());
            return base.OnActivateAsync();
        }

        public Task<List<IEmployee>> GetDirectReports()
        {
            return Task.FromResult(_reports);
        }

        public async Task AddDirectReport(IEmployee employee)
        {
            _reports.Add(employee);
            await employee.SetManager(this);
            await employee.Greeting(_me, "Welcome to my team!");
        }

        public Task<IEmployee> AsEmployee()
        {
            return Task.FromResult(_me);
        }

        private IEmployee _me;
        private List<IEmployee> _reports = new List<IEmployee>();
    }
}