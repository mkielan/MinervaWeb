using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Minerva.Models.Web.Storage;
using Minerva.Entities.Sources;
using Minerva.Entities;
using Minerva.Repositories;

namespace Minerva.Helpers
{
    public static class DiskStructureHelper
    {
        public static ICollection<BreadcrumbItem> GetBarecrumbParentFor(DiskStructure ds)
        {
            var items = new List<BreadcrumbItem>();

            var dsTmp = ds;
            while (dsTmp.Parent != null)
            {
                items.Insert(0, new BreadcrumbItem
                {
                    Id = ds.Id,
                    Name = ds.Name
                });

                dsTmp = ds.Parent;
            }

            return items;
        }
      
        public static bool ShareWith(IDiskStructureRepository<MinervaDbContext> rep, DiskStructure entity, ApplicationUser user)
        {
            if (user == null) return false;

            try
            {
                var dsa = rep.Context.DiskStructureAccess.FirstOrDefault(d => d.UserId == user.Id);

                if (dsa == null)
                {
                    dsa = new DiskStructureAccess {
                        User = user
                    };
                }

                if(!entity.AvailableFor.Select(u => u.User.UserName).Contains(user.UserName))
                    entity.AvailableFor.Add(dsa);

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

        public static bool ShareWith(IDiskStructureRepository<MinervaDbContext> rep, DiskStructure entity, string username)
        {
            var user = rep.Context.Users.FirstOrDefault(u => u.UserName == username);

            return ShareWith(rep, entity, user);
        }

        /// <summary>
        /// For many
        /// </summary>
        /// <param name="rep"></param>
        /// <param name="entity"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public static bool ShareWith(IDiskStructureRepository<MinervaDbContext> rep, DiskStructure entity, string[] username)
        {
            try
            {
                var users = rep.Context.Users.Where(u => username.Contains(u.UserName));
                
                foreach(var user in users) {
                    var dsa = rep.Context.DiskStructureAccess.FirstOrDefault(d => d.UserId == user.Id);

                    if (dsa == null)
                    {
                        dsa = new DiskStructureAccess
                        {
                            User = user
                        };
                    }

                    entity.AvailableFor.Add(dsa);
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

        public static DiskStructure GetFile<A>(IDiskStructureRepository<A> rep, int id) {
            return rep.FindBy(f =>
                f.DeletedBy == null
                && f.Id == id
                && f.File != null).FirstOrDefault();
        }

        public static IEnumerable<DiskStructure> GetFiles(IDiskStructureRepository<MinervaDbContext> rep, string username)
        {
            return rep.FindBy(f =>
                f.CreatedBy.UserName == username
                && f.File != null
                && f.DeletedTime == null
                );
        }

        public static IEnumerable<DiskStructure> GetDirectories(IDiskStructureRepository<MinervaDbContext> rep, string username)
        {
            return rep.FindBy(f =>
                f.CreatedBy.UserName == username
                && f.File == null
                && f.DeletedTime == null
                );
        }

        public static IEnumerable<DiskStructure> GetFiles(IDiskStructureRepository<MinervaDbContext> rep, string username, int parentId)
        {
            return rep.FindBy(f =>
                f.CreatedBy.UserName == username
                && f.Parent.Id == parentId
                && f.File != null
                && f.DeletedTime == null
                );
        }

        public static IEnumerable<DiskStructure> GetDirectories(IDiskStructureRepository<MinervaDbContext> rep, string username, int parentId)
        {
            return rep.FindBy(f =>
                f.CreatedBy.UserName == username
                && f.Parent.Id == parentId
                && f.File == null
                && f.DeletedTime == null
                );
        }

        public static IEnumerable<DiskStructure> FindShared(IDiskStructureRepository<MinervaDbContext> rep, string username, DiskItemType type = DiskItemType.All, PrivilageToDiskItemType privilage = PrivilageToDiskItemType.Owner)
        {
            return rep.FindBy(
                ds => ds.DeletedTime == null
                    && (
                        type == DiskItemType.All
                        || (type == DiskItemType.File && ds.File != null)
                        || (type == DiskItemType.Directory && ds.File == null)
                    )
                && (
                    (privilage == PrivilageToDiskItemType.Owner && ds.CreatedBy.UserName == username && ds.AvailableFor.Any())
                    || (privilage == PrivilageToDiskItemType.Shared && ds.AvailableFor.Select(a => a.User.UserName).Contains(username))
                    )
                );
        }

        public static IEnumerable<DiskStructure> FindByTag(IDiskStructureRepository<MinervaDbContext> rep, string tag, DiskItemType type = DiskItemType.All, string username = null, PrivilageToDiskItemType privilage = PrivilageToDiskItemType.Owner)
        {
            ApplicationUser user = null;

            if (privilage == PrivilageToDiskItemType.Shared)
            {
                if (username == null)
                {
                    throw new Exception("You should put username if you want see shared files for him");
                }
                user = rep.Context.Users.First(u => u.UserName == username);
            }

            return rep.Context.DiskStructures.Where(
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
                        || (privilage == PrivilageToDiskItemType.Shared && ds.AvailableFor.Select(a => a.User.UserName).Contains(username))
                    )
                );
        }
    }
}