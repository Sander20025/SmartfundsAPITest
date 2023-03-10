using SmartFunds.Core;
using SmartFunds.Model;

namespace SmartFunds.Services
{
    public class OrganizationService
    {
        private readonly SmartFundsDbContext _dbContext;

        public OrganizationService(SmartFundsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Find
        public IList<Organization> Find()
        {
            return _dbContext.Organizations.ToList();
        }

        //Get
        public Organization? Get(int id)
        {
            return _dbContext.Organizations
                .SingleOrDefault(o => o.Id == id);
        }

        //Create
        public Organization Create(Organization organization)
        {
            _dbContext.Organizations.Add(organization);
            _dbContext.SaveChanges();

            return organization;
        }

        //Update
        public Organization? Update(int id, Organization organization)
        {
            var dbOrganization = _dbContext
                .Organizations
                .SingleOrDefault(o => o.Id == id);

            if (dbOrganization is null)
            {
                return null;
            }

            dbOrganization.Name = organization.Name;
            dbOrganization.Type = organization.Type;
            dbOrganization.CompanyNumber = organization.CompanyNumber;
            dbOrganization.Email = organization.Email;

            _dbContext.SaveChanges();

            return dbOrganization;
        }
        
        //Delete
        public void Delete(int id)
        {
            var dbOrganization = _dbContext
                .Organizations
                .SingleOrDefault(o => o.Id == id);

            if (dbOrganization is null)
            {
                return;
            }

            _dbContext.Organizations.Remove(dbOrganization);
            _dbContext.SaveChanges();
        }

    }
}
