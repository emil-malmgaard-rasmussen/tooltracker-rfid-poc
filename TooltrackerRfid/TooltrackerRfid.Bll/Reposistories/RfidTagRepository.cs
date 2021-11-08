using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Smoerfugl.Database.Postgres;
using TooltrackerRfid.Database;
using TooltrackerRfid.Database.Entities;

namespace TooltrackerRfid.Bll.Reposistories
{
    public interface IRfidTagRepository : IRepository<RfidTag, Guid>
    {
        
    }
    
    public class RfidTagRepository : IRfidTagRepository
    {
        private readonly TooltrackerRfidDbContext _dbContext;

        public RfidTagRepository(TooltrackerRfidDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<PaginatedList<RfidTag>> GetAsPaginatedList(int index, int pageSize)
        {
            return await _dbContext
                .RfidTags
                .ToPaginatedListAsync(index, pageSize);
        }

        public async Task<IList<RfidTag>> GetList()
        {
            var tag = await _dbContext
                .RfidTags
                .Where(d => d.DeletedAt == null)
                .ToListAsync();
            return tag;
        }

        public async Task<RfidTag> GetById(Guid id)
        {
            var tag = await _dbContext
                .RfidTags
                .Where(d => d.DeletedAt == null)
                .SingleOrDefaultAsync(c => c.Id == id);
            return tag;
        }

        public async Task<RfidTag> Add(RfidTag t)
        {
            var entity = await _dbContext.RfidTags.AddAsync(t);
            return entity.Entity;
        }

        public void Update(RfidTag t)
        {
            _dbContext.Update(t);
        }

        public Task Save()
        {
            return _dbContext.SaveChangesAsync();
        }

        public void Delete(RfidTag rfidTag)
        {
            _dbContext.RfidTags.Remove(rfidTag);
        }
    }
}