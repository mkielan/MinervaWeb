using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Src = Minerva.Entities.Sources;
using Api = Minerva.Models.Api;

namespace Minerva.Helpers
{
    public static class ObjectConverter
    {
        #region Directory
        public static Api.Directory.View DirToApiView(Src.DiskStructure dir) {
            return new Api.Directory.View
                {
                    Id = dir.Id,
                    Name = dir.Name,
                    Description = dir.Description,
                    Image = @"\Content\Image\dir.jpg" //dir.Icon == null ? @"\Content\Image\dir.jpg" : dir.Icon.Name
                };
        }

        public static IEnumerable<Api.Directory.View> ManyDirToApiView(IEnumerable<Src.DiskStructure> dirs)
        {
            return from d in dirs
                   where d.File == null
                   select new Api.Directory.View
                   {
                       Id = d.Id,
                       Name = d.Name,
                       Description = d.Description,
                       Image = @"\Content\Image\dir.jpg"//d.Icon == null ? @"\Content\Image\dir.jpg" : d.Icon.Name
                   };
        }

        #endregion

        #region File
        public static Api.File.View FileToApiView(Src.DiskStructure file)
        {
            return new Api.File.View
            {
                Id = file.Id,
                Name = file.Name,
                Description = file.Description,
                //Image = file.Icon == null ? @"\Content\Image\dir.jpg" : file.Icon.Name
            };
        }

        public static IEnumerable<Api.File.View> ManyFilesToApiView(IEnumerable<Src.DiskStructure> file)
        {
            return from d in file
                   where d.File != null
                   select new Api.File.View
                   {
                       Id = d.Id,
                       Name = d.Name,
                       Description = d.Description,
                       //Image = d.Icon == null ? @"\Content\Image\dir.jpg" : d.Icon.Name
                   };
        }
        #endregion
    }
}