using Minerva.Entities;
using Minerva.Entities.Sources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Minerva.Repositories
{   
    public class DiskStructureRepository : 
        GenericFullRepository<MinervaDbContext, DiskStructure>, 
        IDiskStructureRepository<MinervaDbContext>
    {
        public DiskStructureRepository(MinervaDbContext context)
            : base(context)
        {
        }
        /*
        public bool ShareWith(DiskStructure entity, string username)
        {
            try
            {
                var user = Context.Users.Where(u => u.UserName == username).ToList();

                if (entity.AvailableFor == null)
                {
                    entity.AvailableFor = user;
                }
                else
                {
                    entity.AvailableFor.Add(user.First());
                }

                return true;
            }
            catch (ArgumentNullException)
            {

            }
            catch (InvalidOperationException)
            {

            }

            return false;
        }

        public IEnumerable<DiskStructure> GetFiles(string username)
        {
            return FindBy(f => 
                f.CreatedBy.UserName == username
                && f.File != null
                && f.DeletedTime == null
                );
        }

        public IEnumerable<DiskStructure> GetDirectories(string username)
        {
            return FindBy(f =>
                f.CreatedBy.UserName == username
                && f.File == null
                && f.DeletedTime == null
                );
        }

        public IEnumerable<DiskStructure> GetFiles(string username, int parentId)
        {
            return FindBy(f =>
                f.CreatedBy.UserName == username
                && f.Parent.Id == parentId
                && f.File != null
                && f.DeletedTime == null
                );
        }

        public IEnumerable<DiskStructure> GetDirectories(string username, int parentId)
        {
            return FindBy(f =>
                f.CreatedBy.UserName == username
                && f.Parent.Id == parentId
                && f.File == null
                && f.DeletedTime == null
                );
        }

        public virtual IEnumerable<DiskStructure> FindShared(string username, DiskItemType type = DiskItemType.All, PrivilageToDiskItemType privilage = PrivilageToDiskItemType.Owner)
        {
            return FindBy(
                ds => ds.DeletedTime == null
                    && (
                        type == DiskItemType.All
                        || (type == DiskItemType.File && ds.File != null)
                        || (type == DiskItemType.Directory && ds.File == null)
                    )
                && (
                    (privilage == PrivilageToDiskItemType.Owner && ds.CreatedBy.UserName == username)// && ds.AvailableFor.Any())
                    //|| (privilage == PrivilageToDiskItemType.Shared && ds.AvailableFor.Select(a => a.UserName).Contains(username))
                    )
                );
        }

        public IEnumerable<DiskStructure> FindByTag(string tag, DiskItemType type = DiskItemType.All, string username = null, PrivilageToDiskItemType privilage = PrivilageToDiskItemType.Owner)
        {
            ApplicationUser user = null;

            if (privilage == PrivilageToDiskItemType.Shared)
            {
                if (username == null)
                {
                    throw new Exception("You should put username if you want see shared files for him");
                }
                user = Context.Users.First(u => u.UserName == username);
            }

            return FindBy(
                ds =>
                    ds.DeletedTime == null
                    && ( // ograniczenie co do typu zasobu
                        type == DiskItemType.All
                        || (ds.File == null && type == DiskItemType.Directory)
                        || (ds.File != null && type == DiskItemType.File)
                        )
                    && ( // ograniczenie co do właściela
                        username == null //wszystkie
                        || (privilage == PrivilageToDiskItemType.Owner && ds.CreatedBy.UserName == username)
                        || (privilage == PrivilageToDiskItemType.Shared && ds.AvailableFor.Contains(user))
                    )
                );
        }*/
    }
}
